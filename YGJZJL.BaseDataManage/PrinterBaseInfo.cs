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
    public partial class PrinterBaseInfo : FrmBase
    {
        public PrinterBaseInfo()
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
            //if (tbPrinter.Text.Trim().Length > 0)
            //    str += "and FS_PRINTTYPECODE like '%" + tbPrinter.Text.Trim() + "%'";
            //if (tbDescribe.Text.Trim().Length > 0)
            //    str += "and  FS_PRINTTYPEDESCRIBE like '%" + tbDescribe.Text.Trim() + "%'";
                        
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PrinterBaseInfo";
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
                string p_Printer = tbPrinter.Text.Trim();
                string p_Describe = tbDescribe.Text.Trim();
                int p_PaperNum = Convert.ToInt32(tbPaperNum.Text.Trim());
                int p_InkNum = Convert.ToInt32(tbInkNum.Text.Trim());

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.PrinterBaseInfo";
                ccp.MethodName = "add";
                ccp.ServerParams = new object[] { p_Printer, p_Describe, p_PaperNum, p_InkNum };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoUpdate()
        {
          
            if (DoCheck())
            {
                  string p_OldPrinter ="";
                string p_Printer = tbPrinter.Text.Trim();
                string p_Describe = tbDescribe.Text.Trim();
                int p_PaperNum = Convert.ToInt32(tbPaperNum.Text.Trim());
                int p_InkNum = Convert.ToInt32(tbInkNum.Text.Trim());
                 
                UltraGridRow ugr = this.ultraGrid1.ActiveRow;
                if (ugr == null)
                    p_OldPrinter = p_Printer;
                else
                    p_OldPrinter = ugr.Cells["FS_PRINTTYPECODE"].Text.Trim(); 


                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.PrinterBaseInfo";
                ccp.MethodName = "update";
                ccp.ServerParams = new object[] {p_OldPrinter, p_Printer, p_Describe, p_PaperNum, p_InkNum };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoDelete()
        {
            if (tbPrinter.Text.Trim().Length <= 0)
            {
                MessageBox.Show("打印机类型不能为空！");
                tbPrinter.Focus();
                return;
            }
            string p_Printer = tbPrinter.Text.Trim();
           
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PrinterBaseInfo";
            ccp.MethodName = "delete";
            ccp.ServerParams = new object[] { p_Printer };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.DoClear();
            this.DoQuery();
        }

        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {

            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;

            tbPrinter.Text = ugr.Cells["FS_PRINTTYPECODE"].Text.Trim();
            tbDescribe.Text = ugr.Cells["FS_PRINTTYPEDESCRIBE"].Text.Trim();
            tbPaperNum.Text = ugr.Cells["FN_PAPERNUM"].Text.Trim();
            tbInkNum.Text = ugr.Cells["FN_INKNUM"].Text.Trim();
        }

        private void DoClear()
        {
            tbPrinter.Text = "";
            tbDescribe.Text = "";
            tbPaperNum.Text = "";
            tbInkNum.Text = "";
        }

        private bool DoCheck()
        {
            bool isCheck = true;
            int p_PaperNum = 0;
            int p_InkNum = 0;

            if (tbPrinter.Text.Trim().Length <= 0)
            {
                MessageBox.Show("打印机类型不能为空！");
                isCheck = false;
                tbPrinter.Focus();                
            }

            try
            {
                p_PaperNum = Convert.ToInt32(tbPaperNum.Text.Trim());
            }
            catch 
            {
                MessageBox.Show("纸张量必须为数字！");
                isCheck = false;
                tbPaperNum.Focus();               
            }

            try
            {
                p_InkNum = Convert.ToInt32(tbInkNum.Text.Trim());
            }
            catch
            {
                MessageBox.Show("碳带量必须为数字！");
                isCheck = false;
                tbInkNum.Focus();              
            }
            return isCheck;
        }
    }
}
