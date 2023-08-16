using System;

namespace libplctag.DataTypes
{
    public class SBytePlcMapper : PlcMapperBase<sbyte> {
        public override int? ElementSize => 1;
        override public sbyte Decode(Tag tag, int offset) => tag.GetInt8(offset);
        override public void Encode(Tag tag, int offset, sbyte value) => tag.SetInt8(offset, value);
    }
    public class IntPlcMapper : PlcMapperBase<short>
    {
        public override int? ElementSize => 2;
        override public short Decode(Tag tag, int offset) => tag.GetInt16(offset);
        override public void Encode(Tag tag, int offset, short value) => tag.SetInt16(offset, value);
    }

    public class Int16Mapper : PlcMapperBase<Int16> {
        public override int? ElementSize => 2;
        override public Int16 Decode(Tag tag, int offset) => tag.GetInt16(offset);
        override public void Encode(Tag tag, int offset, Int16 value) => tag.SetInt16(offset, value);
    }
    public class UInt16Mapper : PlcMapperBase<UInt16> {
        public override int? ElementSize => 2;
        override public UInt16 Decode(Tag tag, int offset) => tag.GetUInt16(offset);
        override public void Encode(Tag tag, int offset, UInt16 value) => tag.SetUInt16(offset, value);
    }
    public class Int32Mapper : PlcMapperBase<Int32> {
        public override int? ElementSize => 4;
        override public Int32 Decode(Tag tag, int offset) => tag.GetInt32(offset);
        override public void Encode(Tag tag, int offset, Int32 value) => tag.SetInt32(offset, value);
    }
    public class UInt32Mapper : PlcMapperBase<UInt32> {
        public override int? ElementSize => 4;
        override public UInt32 Decode(Tag tag, int offset) => tag.GetUInt32(offset);
        override public void Encode(Tag tag, int offset, UInt32 value) => tag.SetUInt32(offset, value);
    }
    public class Int64Mapper : PlcMapperBase<Int64> {
        public override int? ElementSize => 8;
        override public Int64 Decode(Tag tag, int offset) => tag.GetInt64(offset);
        override public void Encode(Tag tag, int offset, Int64 value) => tag.SetInt64(offset, value);
    }
    public class UInt64Mapper : PlcMapperBase<UInt64> {
        public override int? ElementSize => 8;
        override public UInt64 Decode(Tag tag, int offset) => tag.GetUInt64(offset);
        override public void Encode(Tag tag, int offset, UInt64 value) => tag.SetUInt64(offset, value);
    }

}
