using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using System.Windows.Forms;
using Infragistics.Win.UltraWinEditors;
using System.Resources;
using System.IO;
namespace YGJZJL.BaseDataManage
{
    public partial class ChangePaper : FrmBase
    {
        string strPointCode = "";
        public ChangePaper()
        {
            InitializeComponent();
        }

        private void ChangePaper_Load(object sender, EventArgs e)
        {
            Query();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            { 
                case "Query":
                    Query();
                    break;
                case "ChangePaper":
                    Change();
                    Query();
                    break;
                default:
                    break;
            }
        }

        private void Query()
        {
            string strQuerySql = "select FS_POINTCODE,FS_POINTNAME,FN_USEDPRINTPAPER FROM BT_POINT t where t.FS_POINTTYPE='QC' order by T.FS_POINTCODE ASC ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strQuerySql };
            ccp.SourceDataTable = this.dataSet1.Tables[0];
            this.dataSet1.Tables[0].Clear();
            ccp = this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        private void Change()
        {
            if (this.tbBf.Text == "" || strPointCode == "")
            {
                MessageBox.Show("请双击选择需要换纸的数据记录行！");
                return;
            }
            if (DialogResult.Yes == MessageBox.Show("是否为 "+tbBf.Text+" 换纸", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                string strUpdateSql = "UPDATE BT_POINT T SET T.FN_USEDPRINTPAPER='0' where t.FS_POINTCODE='" + strPointCode + "'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { strUpdateSql };

                ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
           
        }

        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {
            if(ultraGrid1.ActiveRow.Index!=-1)
            {
                this.tbBf.Text = this.ultraGrid1.ActiveRow.Cells["FS_POINTNAME"].Text.ToString().Trim();
                strPointCode = this.ultraGrid1.ActiveRow.Cells["FS_POINTCODE"].Text.ToString().Trim();
            }
        }
    }
}
