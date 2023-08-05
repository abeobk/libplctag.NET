using libplctag;
using libplctag.DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LotusAPI.HW {
    public partial class ABPLCTagBrowser : Form {
        PlcAB _plc = null;
        public ABPLCTagBrowser(PlcAB plc) { InitializeComponent(); _plc = plc; }

        private void bt_ReadAllTags_Click(object sender, EventArgs e) {
            try {
                if(_plc == null) throw new Exception("No PLC");
                _plc.AssertConnected();
                var setting = (PlcAB.MySetting)_plc.Setting;
                var tags = new Tag<TagInfoPlcMapper, TagInfo[]>() {
                    Gateway = (string)setting.IP,
                    Path = (string)setting.Path,
                    PlcType = setting.PlcType,
                    Protocol = setting.Protocol,
                    Name = "@tags"
                };
                tags.Read();
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("TYPE", typeof(int));
                dt.Columns.Add("NAME", typeof(string));
                dt.Columns.Add("LENGTH", typeof(int));
                dt.Columns.Add("DIMS", typeof(string));
                foreach(var tag in tags.Value) {
                    dt.Rows.Add(
                        tag.Id,
                        $"0x{tag.Type:X}",
                        tag.Name,
                        tag.Length,
                        string.Join(",", tag.Dimensions)
                        );
                }
                dgv.DataSource = dt;
            } catch(Exception ex) { DialogUtils.ShowErr(ex); }
        }
    }
}
