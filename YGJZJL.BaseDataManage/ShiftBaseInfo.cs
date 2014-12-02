using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;

namespace YGJZJL.BaseDataManage
{
    public partial class ShiftBaseInfo : FrmBase
    {
        public ShiftBaseInfo()
        {
            InitializeComponent();
        }

        
        #region Toolbar按钮事件

        public override void ToolBar_Click(object sender, string ToolbarKey)
        {
            base.ToolBar_Click(sender, ToolbarKey);

            switch (ToolbarKey)
            {
                case "Query":
                    this.DoQuery();
                    break;
                case "Add":
                    this.DoAdd();
                    break;
                case "Update":
                    this.DoUpdate();
                    break;
                case "Delete":
                    this.DoDelete();
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void DoQuery()
        {
            string str = "";
            //if (tbShift.Text.Trim().Length > 0)
            //    str += "and FS_SHIFTNAME like '%" + tbShift.Text.Trim() + "%'";
            

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.ShiftBaseInfo";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { str };
            ccp.SourceDataTable = this.dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        private void DoAdd()
        {
            if (DoCheck())
            {
                string p_Shift = tbShift.Text.Trim();
                string p_StartTime = dateTimePicker1.Value.ToString("HH:mm:ss");
                string p_EndTime = dateTimePicker2.Value.ToString("HH:mm:ss");
               
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.ShiftBaseInfo";
                ccp.MethodName = "add";
                ccp.ServerParams = new object[] { p_Shift, p_StartTime, p_EndTime };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoUpdate()
        {

            if (DoCheck())
            {
                string p_OldShift = "";
                string p_Shift = tbShift.Text.Trim();
                string p_StartTime = dateTimePicker1.Value.ToString("HH:mm:ss");
                string p_EndTime = dateTimePicker2.Value.ToString("HH:mm:ss");

                UltraGridRow ugr = this.ultraGrid1.ActiveRow;
                if (ugr == null)
                    p_OldShift = p_Shift;
                else
                    p_OldShift = ugr.Cells["FS_SHIFTNAME"].Text.Trim();


                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.ShiftBaseInfo";
                ccp.MethodName = "update";
                ccp.ServerParams = new object[] { p_OldShift, p_Shift, p_StartTime, p_EndTime };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoDelete()
        {
            if (tbShift.Text.Trim().Length <= 0)
            {
                MessageBox.Show("班次名称不能为空！");
                tbShift.Focus();
                return;
            }
            string p_Shift = tbShift.Text.Trim();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.ShiftBaseInfo";
            ccp.MethodName = "delete";
            ccp.ServerParams = new object[] { p_Shift };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.DoClear();
            this.DoQuery();
        }


        private void DoClear()
        {
            tbShift.Text = "";
  
        }

        private bool DoCheck()
        {
            bool isCheck = true;
            int p_PaperNum = 0;
            int p_InkNum = 0;

            if (tbShift.Text.Trim().Length <= 0)
            {
                MessageBox.Show("班次名称不能为空！");
                isCheck = false;
                tbShift.Focus();
            }           

            return isCheck;
        }


        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;

            tbShift.Text = ugr.Cells["FS_SHIFTNAME"].Text.Trim();
            dateTimePicker1.Text = ugr.Cells["FD_STARTTIME"].Text.Trim();
            dateTimePicker2.Text = ugr.Cells["FD_ENDTIME"].Text.Trim();

        }

        private void tbDescribe_TextChanged(object sender, EventArgs e)
        {

        }

        private void PondTypeBaseInfo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
