using libplctag;
using libplctag.DataTypes;
using libplctag.DataTypes.Simple;
using LotusAPI;
using LotusAPI.Controls.Editors;
using LotusAPI.HW.PLC;
using LotusAPI.Net;
using LotusAPI.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static LotusAPI.HW.IPLC;
using LibPLC = libplctag;

namespace LotusAPI.HW {
    public class PlcAB : PLCBase {
        public enum ElementType {
            Int8 =0,
            Int16,
            Int32,
        }

        public class AbPlcMemoryBlock:MemoryBlock {
            public ElementType ElementType { get; set; } = ElementType.Int16;
        }
        public class AbPlcMemoryBlockListEditor : UITypeEditor {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
                return UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) {
                //show openfile dialog
                var list = (value as List<AbPlcMemoryBlock>).ConvertAll(b=>b as IPLC.MemoryBlock);

                var f = new FormMemoryBlockListEditor(list);
                f.ShowDialog();
                if(f.DialogResult == System.Windows.Forms.DialogResult.OK) {
                    //var jblocks = JsonUtils.ToJson(f.MemoryBlocks);
                    //var blocks = JsonUtils.Read<List<IPLC.MemoryBlock>>(jblocks);
                    //return blocks;
                    return f.MemoryBlocks.ConvertAll(b=>b as AbPlcMemoryBlock);
                }
                return value;
            }
        }
        public class MySetting : SettingObject {
            public Property_<string> IP { get; } = new Property_<string>("192.168.1.100", "NETWORK", WriteProtectionType.AskForPermission);
            public Property_<string> Path { get; } = new Property_<string>("1,0", "NETWORK", WriteProtectionType.AskForPermission);
            public Property_<LibPLC.PlcType> PlcType { get; } = new Property_<LibPLC.PlcType>(LibPLC.PlcType.ControlLogix, "GENERAL", WriteProtectionType.AskForPermission);
            public Property_<LibPLC.Protocol> Protocol { get; } = new Property_<LibPLC.Protocol>(LibPLC.Protocol.ab_eip, "GENERAL", WriteProtectionType.AskForPermission);
            public Property_<ElementType> IOElemType { get; } = new Property_<ElementType>(ElementType.Int32, "IO", WriteProtectionType.AskForPermission);
           // public Property_<ElementType> BlockElemType { get; } = new Property_<ElementType>(ElementType.Int32, "IO", WriteProtectionType.AskForPermission);
            public Property_<bool> IOPolling { get; } = new Property_<bool>(true, "IO", WriteProtectionType.AskForPermission);
            public Property_<string> InputDevice { get; } = new Property_<string>("", "IO", WriteProtectionType.AskForPermission);
            public Property_<string> OutputDevice { get; } = new Property_<string>("", "IO", WriteProtectionType.AskForPermission);

            public Property_<List<AbPlcMemoryBlock>> MemoryBlocks { get; } = new Property_<List<AbPlcMemoryBlock>>(new List<AbPlcMemoryBlock>(), "IO",
                typeof(AbPlcMemoryBlockListEditor), WriteProtectionType.AskForPermission);
            public Property_<PinMap> PinMap { get; } = new Property_<PinMap>(new HW.PinMap(), "IO", typeof(PinMapEditor), WriteProtectionType.AskForPermission);

            public Property_<int> PollInterval { get; } = new Property_<int>(200, "GENERAL", WriteProtectionType.AskForConfirmation);
            public Property_<bool> Debug { get; } = new Property_<bool>(false, "GENERAL", WriteProtectionType.AskForPermission);
            public Property_<int> ReadWriteTimeout { get; } = new Property_<int>(1000, "GENERAL", WriteProtectionType.AskForConfirmation);
            public Property_<bool> AutoReconnect { get; } = new Property_<bool>(true, "NETWORK", WriteProtectionType.AskForPermission);
            public Property_<int> ReconnectInterval { get; } = new Property_<int>(5000, "NETWORK", WriteProtectionType.AskForPermission);
            //public Property_<int> ConnectingLingerTimeout { get; } = new Property_<int>(60000, "GENERAL", WriteProtectionType.AskForPermission);

            public MySetting() { }
            public MySetting(string path = "ABPLC") {
                //Serializer = new RegistrySerializer(path);
            }
        }

        ElementType IOElemType => ((MySetting)Setting).IOElemType;
       // ElementType BlockElemType => ((MySetting)Setting).BlockElemType;
        string IP => ((MySetting)Setting).IP;
        string Path => ((MySetting)Setting).Path;
        LibPLC.PlcType PlcType => ((MySetting)Setting).PlcType;
        LibPLC.Protocol Protocol => ((MySetting)Setting).Protocol;
        string InputDevice => ((MySetting)Setting).InputDevice;
        string OutputDevice => ((MySetting)Setting).OutputDevice;
        int Timeout => ((MySetting)Setting).ReadWriteTimeout;
        bool Debug => ((MySetting)Setting).Debug;
        bool AutoReconnect => (bool)((MySetting)Setting).AutoReconnect;
        int ReconnectInterval => ((MySetting)Setting).ReconnectInterval;
        //int ConnectionLingerTimeout => ((MySetting)Setting).ConnectingLingerTimeout;
        bool IOPolling => ((MySetting)Setting).IOPolling;
        public override List<IPLC.MemoryBlock> MemoryBlocks => (((MySetting)Setting).MemoryBlocks.Value as List<AbPlcMemoryBlock>).ConvertAll(b=>b as IPLC.MemoryBlock);

        Dictionary<int, Tag<BoolPlcMapper, bool>> _CreateTags(PinFunc func, string addr) {
            var pins = PinMap.Pins.FindAll(p => p.Function == func && p.Index >= 0 && p.Index < 32);
            Dictionary<int, Tag<BoolPlcMapper, bool>> tags = new Dictionary<int, Tag<BoolPlcMapper, bool>>();
            foreach(var pin in pins) {
                var tag = new Tag<BoolPlcMapper, bool> {
                    Name = addr + "." + pin.Name,
                    Gateway = this.IP,
                    Path = this.Path,
                    PlcType = this.PlcType,
                    Protocol = this.Protocol,
                    Timeout = TimeSpan.FromMilliseconds(this.Timeout)
                };
                tags.Add(pin.Index, tag);
                Logger.Debug("Initializing BOOL tag: " + tag.Name);
                tag?.Initialize();

            }
            return tags;
        }


        class TagSByteArr: TagSint1D{
            public bool IsArray => true;
            public int Length { get; set; } = 0;
        }

        class TagInt16Arr : TagInt1D {
            public bool IsArray => true;
            public int Length { get; set; } = 0;
        }
        class TagInt32Arr : TagDint1D {
            public bool IsArray => true;
            public int Length { get; set; } = 0;
        }
        //class TagInt16: TagInt {
        //    public bool IsArray => false;
        //    public int Length { get; set; } = 0;
        //}
        //class TagInt32: TagDint {
        //    public bool IsArray => false;
        //    public int Length { get; set; } = 0;
        //}


        ITag _CreateTag(ElementType elem_type, string addr, bool is_array = false, int length = 0) {
            ITag tag = null;
            if(is_array) {
                switch(elem_type) {
                    case ElementType.Int8:
                        tag = new TagSByteArr{ Name = addr, Gateway = this.IP, Path = this.Path, PlcType = this.PlcType, Protocol = this.Protocol, Timeout = TimeSpan.FromMilliseconds(this.Timeout), };
                        break;
                    case ElementType.Int16:
                        tag = new TagInt16Arr { Name = addr, Gateway = this.IP, Path = this.Path, PlcType = this.PlcType, Protocol = this.Protocol, Timeout = TimeSpan.FromMilliseconds(this.Timeout), };
                        break;
                    case ElementType.Int32:
                        tag = new TagInt32Arr { Name = addr, Gateway = this.IP, Path = this.Path, PlcType = this.PlcType, Protocol = this.Protocol, Timeout = TimeSpan.FromMilliseconds(this.Timeout), };
                        break;
                    default:
                        throw new Exception($"Invalid data type ({elem_type})");
                }
            }
            switch(elem_type) {
                case ElementType.Int8:
                    tag = new Tag<SBytePlcMapper, sbyte>() { Name = addr, Gateway = this.IP, Path = this.Path, PlcType = this.PlcType, Protocol = this.Protocol, Timeout = TimeSpan.FromMilliseconds(this.Timeout), };
                    break;
                case ElementType.Int16:
                    tag = new Tag<Int16Mapper, Int16>() { Name = addr, Gateway = this.IP, Path = this.Path, PlcType = this.PlcType, Protocol = this.Protocol, Timeout = TimeSpan.FromMilliseconds(this.Timeout), };
                    break;
                case ElementType.Int32:
                    tag = new Tag<Int32Mapper, Int32>() { Name = addr, Gateway = this.IP, Path = this.Path, PlcType = this.PlcType, Protocol = this.Protocol, Timeout = TimeSpan.FromMilliseconds(this.Timeout), };
                    break;
                default:
                    throw new Exception($"Invalid data type ({elem_type})");
            }
            Logger.Debug($"Initializing tag [{elem_type}: {addr}]");
            tag?.Initialize();
            return tag;
        }


        private static readonly Regex regex_addressRange = new Regex(@"(?<=\[)[0-9]+~[0-9]+(?=\])");
        private static readonly Regex regex_addressName = new Regex(@".+(?=\[)");
        private static readonly Regex regex_addressListName = new Regex(@".+(?=\{)");
        private static readonly Regex regex_addressListRange = new Regex(@"(?<=\{).*(?=\})");
        private static readonly Regex regex_removeWhiteSpace = new Regex(@"\s+");

        List<ITag> _CreateTagList(ElementType elem_type, string addrs) {
            //parse address
            //tag1|tag2|tag3
            //TAG.TAG.{A,B,C,D}
            //TAG.TAG.[id0~id1]
            var addr = addrs.Replace(" ", "");
            var tag_addrs = addr.Split('|');
            var tags = new List<ITag>();

            foreach(var tag_addr in tag_addrs) {
                if(tag_addr.Contains("[")) {
                    var matches = regex_addressRange.Matches(tag_addr);
                    if(matches.Count == 0) { tags.Add(_CreateTag(elem_type, tag_addr)); }
                    else if(matches.Count == 1) {
                        var name_matches = regex_addressName.Matches(tag_addr);
                        if(name_matches.Count != 1) throw new Exception($"Invalid base address name: {tag_addr}");
                        var base_addr = name_matches[0].ToString();
                        var nums = matches[0].ToString().Split('~');
                        var id0 = int.Parse(nums[0]);
                        var id1 = int.Parse(nums[1]);
                        for(int id = id0; id <= id1; id++) {
                            tags.Add(_CreateTag(elem_type, $"{base_addr}[{id}]"));
                        }
                    }
                }
                else if(tag_addr.Contains("{")) {
                    var matches = regex_addressListRange.Matches(tag_addr);
                    if(matches.Count == 0) {
                        throw new Exception($"Invalid base address name: {tag_addr}");
                    }
                    else if(matches.Count == 1) {
                        var name_matches = regex_addressListName.Matches(tag_addr);
                        if(name_matches.Count != 1) throw new Exception($"Invalid base address name: {tag_addr}");
                        var base_addr = name_matches[0].ToString();
                        var ss = matches[0].Value.Split(',');
                        foreach(var str in ss) {
                            var s = str.Trim();
                            if(s.Length == 0) continue;
                            tags.Add(_CreateTag(elem_type, $"{base_addr}{s}"));
                        }
                    }
                }
                else { 
                    tags.Add(_CreateTag(elem_type, addr));
                }
            }
            return tags;
        }



        ITag _CreateInputTag() => _CreateTag(IOElemType, InputDevice);
        ITag _CreateOutputTag() => _CreateTag(IOElemType, OutputDevice);

        Dictionary<string, List<ITag>> _blockTags = new Dictionary<string, List<ITag>>();

        ITag InputTag = null;
        ITag OutputTag = null;
        Dictionary<int, Tag<BoolPlcMapper, bool>> InputTags = null;
        Dictionary<int, Tag<BoolPlcMapper, bool>> OutputTags = null;

        public PlcAB(string name = "ABPLC") {
            Setting = new MySetting(name);
            Name = name;

            //((MySetting)Setting).Name.Value = name;

            Setting.PropertyChangedEvent += Setting_PropertyChangedEvent;


            DIbits.BitChanged += (pin_id, old_value, new_value) => {
                var pin = PinMap[PinFunc.DI, pin_id];
                BitChangedEvent?.Invoke(pin != null ? pin : new PinInfo(PinFunc.DI, pin_id, "N/A", "Unmapped pin"), old_value, new_value);
            };

            DObits.BitChanged += (pin_id, old_value, new_value) => {
                var pin = PinMap[PinFunc.DO, pin_id];
                BitChangedEvent?.Invoke(pin != null ? pin : new PinInfo(PinFunc.DO, pin_id, "N/A", "Unmapped pin"), old_value, new_value);
            };

            Setting_PropertyChangedEvent(null, null);
        }


        private void Setting_PropertyChangedEvent(object sender, Property e) { Invalidate(); }

        public override void Invalidate() {
            lock(locker) {
                try {
                    InputTag = null;
                    OutputTag = null;
                    InputTags = null;
                    OutputTags = null;
                    Logger.Debug("Initializing PLC tags...");
                    Logger.Debug($"Creating input tag  ({InputDevice})...");
                    InputTag = _CreateInputTag();
                    Logger.Debug($"Creating output tag ({OutputDevice})...");
                    OutputTag = _CreateOutputTag();

                    int io_byte_cnt = IOElemType == ElementType.Int16 ? 2 : 4;
                    Logger.Debug($"IO byte count: {io_byte_cnt}");
                    DIbits.ByteCount = io_byte_cnt;
                    DObits.ByteCount = io_byte_cnt;


                    //pollRate
                    PollInterval.Set(((MySetting)Setting).PollInterval);

                    _blockTags.Clear();
                    foreach(var block in MemoryBlocks) {
                        if(_blockTags.ContainsKey(block.Name)) throw new Exception($"Duplicate block name [{block.Name}]");
                        Logger.Debug($"Creating memory block tags [{block.Name}: {block.BaseAddress}]...");
                        _blockTags.Add(block.Name, _CreateTagList((block as AbPlcMemoryBlock).ElementType, block.BaseAddress));
                    }
                } catch(Exception ex) {
                    Logger.Error(ex.Message);
                }
            }
        }

        object locker = new object();

        Thread pollThread = null;
        ThreadSafeFlag PollInterval = new ThreadSafeFlag(100);
        ThreadSafeFlag shouldStop = new ThreadSafeFlag(false);
        ThreadSafeFlag shouldPause = new ThreadSafeFlag(false);

        public override event IPLC.BitChangedEventHandler BitChangedEvent;
        public override event IPLC.MemoryBlockChangedEventHandler MemoryBlockChangedEvent;
        public override event IPLC.ConnectedEventHandler ConnectedEvent;
        public override event IPLC.DisconnectedEventHandler DisconnectedEvent;

        public ThreadSafeFlag Started { get; protected set; } = new ThreadSafeFlag(false);
        public ThreadSafeFlag Paused { get; protected set; } = new ThreadSafeFlag(false);

        public override PinMap PinMap {
            get => ((MySetting)Setting).PinMap;
            set => ((MySetting)Setting).PinMap.Value = value;
        }

        Stopwatch _swConnectionPoll = new Stopwatch();
        public override bool Connect() {
            try {
                Disconnect();
                Logger.Info($"PLC[{Name}] Connecting to {IP}...");
                if(!NetUtils.PingHost(IP)) {
                    goto __CONNECT_EXIT;
                }

                Logger.Info($"PLC[{Name}] connected");
                this.ConnectedEvent?.Invoke($"PLC[{Name}]({IP}: Path=[{Path}])");
                IsConnected = true;
            } catch(Exception ex) {
                LotusAPI.Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
            __CONNECT_EXIT:
            if(!IsConnected) {
                Logger.Error($"PLC[{Name}] Failed to connect.");
            }
            return IsConnected;
        }

        public override void Disconnect() {
            if(!IsConnected) {
                DisconnectedEvent?.Invoke();
                return;
            }
            try {
                Stop();
            } catch(Exception ex) { LotusAPI.Logger.Error(ex.Message); }
            IsConnected = false;
            Logger.Debug($"PLC[{Name}] disconnected");
            DisconnectedEvent?.Invoke();
        }

        /// <summary>
        /// Start polling thread in highest priority, automatic connect if PLC has not been not connected yet.
        /// </summary>
        /// <returns>true if started</returns>
        public override bool Start() {

            if(Started) return true;
            Stop();
            shouldStop.Set(false);
            Paused.Set(false);

            Logger.Debug($"PLC[{Name}] Starting polling thread...");
            pollThread = new Thread(() => {
                Stopwatch sw = new Stopwatch();
                while(!shouldStop) {
                    try {
                        if(shouldStop) { goto ThreadExit_; }

                        //pausing
                        while(shouldPause) {
                            if(!Paused) Logger.Debug($"PLC[{Name}] Polling thread paused.");
                            if(!shouldStop) { goto ThreadExit_; }
                            Paused.Set(true);
                            Thread.Sleep(10);
                        }
                        if(Paused) {
                            Paused.Set(false);
                            Logger.Debug($"PLC[{Name}] Polling thread resumed.");
                        }

                        sw.Restart();
                        //monitor bit
                        if(IOPolling) ReadDI();

                        if(sw.ElapsedMilliseconds > 1000) {
                            Logger.Error($"PLC[{Name}] DI took long time to read! ({sw.ElapsedMilliseconds}ms)");
                        }

                        sw.Restart();
                        //monitor block
                        foreach(var blk in MemoryBlocks) {
                            if(blk.Monitoring) ReadBlock(blk);
                        }

                        if(sw.ElapsedMilliseconds > 5000) {
                            Logger.Error($"PLC[{Name}] Block took long time to read! ({sw.ElapsedMilliseconds}ms)");
                        }

                    } catch(Exception ex) {
                        LotusAPI.Logger.Error(ex.Message);
                        LotusAPI.Logger.Debug(ex.StackTrace);

                        Thread.Sleep(1000);
                        if(!NetUtils.PingHost(IP)) {
                            Logger.Error("Connection lost!");
                            IsConnected = false;
                            DisconnectedEvent?.Invoke();
                        }

                        if(!AutoReconnect) {
                            goto ThreadExit_;
                        }
                        else {
                            while(!NetUtils.PingHost(IP)) {
                                Logger.Debug($"PLC[{Name}] Trying to reconnect to {IP}...");
                                sw.Restart();
                                while(sw.ElapsedMilliseconds < ReconnectInterval) {
                                    if(shouldStop) { goto ThreadExit_; }
                                    Thread.Sleep(10);
                                }
                            }
                            if(NetUtils.PingHost(IP)) {
                                Logger.Info($"PLC[{Name}] connected");
                                this.ConnectedEvent?.Invoke($"PLC[{Name}]({IP}: Path=[{Path}])");
                                IsConnected = true;
                            }
                        }
                    }

                    System.Threading.Thread.Sleep(System.Math.Max(1, (int)(PollInterval - sw.ElapsedMilliseconds)));
                }

                ThreadExit_:
                Started.Set(false);
            });
            pollThread.Start();
            Started.Set(true);
            pollThread.Priority = ThreadPriority.Highest;

            Logger.Info($"PLC[{Name}] polling thread started.");
            return true;
        }

        public override void Stop() {
            if(!Started) return;
            Logger.Debug($"PLC[{Name}] Stopping polling thread...");
            shouldStop.Set(true);
            while(Started) { Thread.Sleep(50); }
            pollThread?.Join();
            Logger.Debug($"PLC[{Name}] Polling thread terminated!");
            pollThread = null;
        }

        public override void Pause() => shouldPause.Set(true);
        public override void Resume() => shouldPause.Set(false);

        public override void WaitUntilPaused() {
            shouldPause.Set(true);
            while(!Paused) { Thread.Sleep(50); }
        }

        public override uint ReadDI() {
            AssertConnected();
            lock(locker) {
                if(InputTag == null) {
                    DisconnectedEvent?.Invoke();
                    IsConnected = false;
                    throw new Exception("Input tag is null!");
                }
                //read DI tag
                var value = InputTag?.Read();

                switch(IOElemType) {
                    case ElementType.Int16: DIbits.Data = BitConverter.GetBytes((Int16)value); break;
                    case ElementType.Int32: DIbits.Data = BitConverter.GetBytes((Int32)value); break;
                }

                if(Debug) { Logger.Debug($"PLC[{Name}] PLC->PC [INPUT: {IOElemType}]: {value})"); }
                switch(DIbits.ByteCount) {
                    case 2: return (uint)BitConverter.ToUInt16(DIbits.Data, 0);
                    case 4: return (uint)BitConverter.ToUInt32(DIbits.Data, 0);
                    default: throw new Exception("Invalid input bit count. Only supported 16 or 32 bits");
                }
            }
        }

        public void WriteDO() {
            AssertConnected();
            lock(locker) {
                if(OutputTag == null) {
                    DisconnectedEvent?.Invoke();
                    IsConnected = false;
                    throw new Exception("Output tag is null!");
                }
                switch(IOElemType) {
                    case ElementType.Int16: OutputTag.Value = BitConverter.ToInt16(DObits.Data, 0); break;
                    case ElementType.Int32: OutputTag.Value = BitConverter.ToInt32(DObits.Data, 0); break;
                }
                if(Debug) { Logger.Debug($"PLC[{Name}] PLC->PC [OUTPUT: {IOElemType}]: {OutputTag.Value})"); }
                OutputTag.Write();
            }
        }

        public override void WriteDO(uint values) {
            DObits.Data = BitConverter.GetBytes(values);
            WriteDO();
        }


        public override void ReadBlock(IPLC.MemoryBlock block) {
            AssertConnected();
            lock(locker) {
                if(!_blockTags.ContainsKey(block.Name)) {
                    DisconnectedEvent?.Invoke();
                    IsConnected = false;
                    throw new Exception($"Invalid block tags ({block.Name})");
                }
                var elem_type = (block as AbPlcMemoryBlock).ElementType;
                var tags = _blockTags[block.Name];
                PacketBuilder pkt = new PacketBuilder();
                foreach(var tag in tags) {
                    var value = tag.Read();
                    switch(elem_type) {
                        case ElementType.Int8: pkt.AppendChar(Convert.ToChar((sbyte)value)); break;
                        case ElementType.Int16: pkt.AppendInt16((Int16)value); break;
                        case ElementType.Int32: pkt.AppendInt32((Int32)value); break;
                    }
                }

                bool changed = block.SetData(pkt.Buffer.ToArray(), 0);
                
                if(changed) {
                    MemoryBlockChangedEvent?.Invoke(block.Clone());
                }

                if(Debug) {
                    Logger.Debug($"PLC[{Name}] PLC->PC [{block.Name}]: {block.Contents} ({block.StringContents})");
                }
            }
        }

        public override void WriteBlock(IPLC.MemoryBlock block) {
            AssertConnected();
            lock(locker) {
                if(!_blockTags.ContainsKey(block.Name)) {
                    DisconnectedEvent?.Invoke();
                    IsConnected = false;
                    throw new Exception($"Invalid block tags ({block.Name})");
                }
                var tags = _blockTags[block.Name];
                PacketBuilder pkt = new PacketBuilder();
                pkt.Buffer = block.Data.ToList();
                pkt.BytePosition = 0;
                var elem_type = (block as AbPlcMemoryBlock).ElementType;
                foreach(var tag in tags) {
                    switch(elem_type) {
                        case ElementType.Int8: tag.Value = Convert.ToSByte(pkt.GetChar()); break;
                        case ElementType.Int16: tag.Value = pkt.GetInt16(); break;
                        case ElementType.Int32: tag.Value = pkt.GetInt32(); break;
                    }
                    tag.Write();
                }
                if(Debug) {
                    Logger.Debug($"PLC[{Name}] PLC->PC [{block.Name}]: {block.Contents} ({block.StringContents})");
                }
            }

        }
    }
}
