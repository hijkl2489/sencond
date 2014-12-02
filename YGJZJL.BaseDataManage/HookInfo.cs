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
    public partial class HookInfo : FrmBase
    {
        public HookInfo()
        {
            InitializeComponent();
        }

        #region  网格显示设置
        /// <summary>
        /// 网格显示设置
        /// </summary>
        private void DataGridInit()
        {
            for (int i = 0; i <= ultraGrid1.DisplayLayout.Bands[0].Columns.Count - 1; i++)
            {
                ultraGrid1.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ultraGrid1.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Equals;
            }
            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = null;
            }
            Constant.RefreshAndAutoSize(ultraGrid1);
        }
        #endregion

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
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.HookInfo";
            ccp.MethodName = "query";
            ccp.SourceDataTable = this.dataSet1.Tables[0];
          
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        private void DoAdd()
        {
            if (DoCheck())
            {
                string sGH = txtGH.Text.Trim();

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.HookInfo";
                ccp.MethodName = "add";
                ccp.ServerParams = new object[] { sGH };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnObject == null)
                {
                    this.DoClear();
                    this.DoQuery();
                }
                else
                {
                    string a = ccp.ReturnObject.ToString();
                    MessageBox.Show(a);
                }
            }
        }

        private void DoUpdate()
        {
            if (txtXH.Text == "")
            { 
                MessageBox.Show("请选择一条钩号信息！");
                return;
            }
            if (DoCheck())
            {
                string sID = txtXH.Text.Trim();
                string sGH = txtGH.Text.Trim();
                 
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.HookInfo";
                ccp.MethodName = "update";
                ccp.ServerParams = new object[] { sID, sGH };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnObject == null)
                {
                    this.DoClear();
                    this.DoQuery();
                }
                else
                {
                    string a = ccp.ReturnObject.ToString();
                    MessageBox.Show(a);
                }
            }
        }

        private void DoDelete()
        {
            string sID = txtXH.Text.Trim();
           
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.HookInfo";
            ccp.MethodName = "delete";
            ccp.ServerParams = new object[] { sID };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.DoClear();
            this.DoQuery();
        }

        //private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        //{

        //    UltraGridRow ugr = this.ultraGrid1.ActiveRow;
        //    if (ugr == null) return;

        //    txtXH.Text = ugr.Cells["FN_INDEX"].Text.Trim();
        //    txtGH.Text = ugr.Cells["FN_HOOKNO"].Text.Trim();
        //}

        private void DoClear()
        {
            txtXH.Text = "";
            txtGH.Text = "";
        }

        private bool DoCheck()
        {
            bool isCheck = true;
            int p_PaperNum = 0;

            try
            {
                p_PaperNum = Convert.ToInt32(txtGH.Text.Trim());
            }
            catch 
            {
                MessageBox.Show("钩号必须为数字！");
                isCheck = false;
                txtGH.Focus();               
            }

            return isCheck;
        }

        private void ultraGrid1_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;

            txtXH.Text = ugr.Cells["FN_INDEX"].Text.Trim();
            txtGH.Text = ugr.Cells["FN_HOOKNO"].Text.Trim();
        }

        private void HookInfo_Load(object sender, EventArgs e)
        {
            DataGridInit();
        }
    }
}
