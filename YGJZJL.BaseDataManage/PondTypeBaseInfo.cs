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
    public partial class PondTypeBaseInfo : FrmBase
    {
        public PondTypeBaseInfo()
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
            //if (tbPondTypeNo.Text.Trim().Length > 0)
            //    str += "and FS_PONDTYPENO like '%" + tbPondTypeNo.Text.Trim() + "%'";
            //if (tbPondTypeName.Text.Trim().Length > 0)
            //    str += "and  FS_PONDTYPENAME like '%" + tbPondTypeName.Text.Trim() + "%'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PondTypeBaseInfo";
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
                string p_PondTypeNo = tbPondTypeNo.Text.Trim();
                string p_PondTypeName = tbPondTypeName.Text.Trim();
              

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.PondTypeBaseInfo";
                ccp.MethodName = "add";
                ccp.ServerParams = new object[] { p_PondTypeNo, p_PondTypeName };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoUpdate()
        {

            if (DoCheck())
            {
                string p_OldPondTypeNo = "";
                string p_PondTypeNo = tbPondTypeNo.Text.Trim();
                string p_PondTypeName = tbPondTypeName.Text.Trim();

                UltraGridRow ugr = this.ultraGrid1.ActiveRow;
                if (ugr == null)
                    p_OldPondTypeNo = p_PondTypeNo;
                else
                    p_OldPondTypeNo = ugr.Cells["FS_PONDTYPENO"].Text.Trim();


                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.PondTypeBaseInfo";
                ccp.MethodName = "update";
                ccp.ServerParams = new object[] { p_OldPondTypeNo, p_PondTypeNo, p_PondTypeName };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.DoClear();
                this.DoQuery();
            }
        }

        private void DoDelete()
        {
            if (tbPondTypeNo.Text.Trim().Length <= 0)
            {
                MessageBox.Show("衡器类型代码不能为空！");
                tbPondTypeNo.Focus();
                return;
            }
            string p_PondTypeNo = tbPondTypeNo.Text.Trim();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.PondTypeBaseInfo";
            ccp.MethodName = "delete";
            ccp.ServerParams = new object[] { p_PondTypeNo };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.DoClear();
            this.DoQuery();
        }

        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {

            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;

            tbPondTypeNo.Text = ugr.Cells["FS_PONDTYPENO"].Text.Trim();
            tbPondTypeName.Text = ugr.Cells["FS_PONDTYPENAME"].Text.Trim();
   
        }

        private void DoClear()
        {
            tbPondTypeNo.Text = "";
            tbPondTypeName.Text = "";
          
        }

        private bool DoCheck()
        {
            bool isCheck = true;
            int p_PaperNum = 0;
            int p_InkNum = 0;

            if (tbPondTypeNo.Text.Trim().Length <= 0)
            {
                MessageBox.Show("衡器类型代码不能为空！");
                isCheck = false;
                tbPondTypeNo.Focus();
            }

            if (tbPondTypeName.Text.Trim().Length <= 0)
            {
                MessageBox.Show("衡器类型说明不能为空！");
                isCheck = false;
                tbPondTypeName.Focus();
            }

            return isCheck;
        }

        private void tbDescribe_TextChanged(object sender, EventArgs e)
        {

        }

        private void PondTypeBaseInfo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
