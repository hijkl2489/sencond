using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HGJZJL.PublicComponent;
using CoreFS.CA06;
using SDK_Com;
//using SerialCommlib;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using System.Threading;
using System.IO;


namespace HGJZJL.HighSpeedWire
{
    public partial class HighWareStandardInfo : FrmBase
    {
        string strCurID = "";

        public HighWareStandardInfo()
        {
            InitializeComponent();
        }

        private void QueryStandard()
        {
            try
            {
                string sql = "select fn_id,fs_SteelType,fs_Standard from BT_PRINTCARDSTANDARD";
                if (this.tbx_Qsteeltype.Text.Trim() != "")
                    sql += " where fs_steeltype = '" + this.tbx_Qsteeltype.Text.Trim() + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.highspeedwire.HighSpeedWireInfo";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { sql };
                this.dataSet1.Tables[0].Rows.Clear();
                ccp.SourceDataTable = this.dataSet1.Tables[0];
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            strCurID = this.ultraGrid1.ActiveRow.Cells["FN_ID"].Text.Trim();
            this.tbx_standard.Text = this.ultraGrid1.ActiveRow.Cells["FS_STANDARD"].Text.Trim();
            this.tbx_steeltype.Text = this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void AddNewStandard()
        {
            try
            {
                if (this.tbx_standard.Text.Trim() == "" || this.tbx_steeltype.Text.Trim() == "")
                    return;

                if (!CheckSteelTypeIsExist(this.tbx_steeltype.Text.Trim()))
                {
                    string strSteelType = this.tbx_steeltype.Text.Trim();
                    string strStandard = this.tbx_standard.Text.Trim();
                    string strId = "0";
                    string strType = "add";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "hgjzjl.highspeedwire.HighSpeedWireInfo";
                    ccp.MethodName = "GXPrintStandardManage";
                    ccp.ServerParams = new object[] {strId,strType,strStandard,strSteelType};

                    CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ret.ReturnCode != 0)
                    {
                        MessageBox.Show("数据保存失败！");
                        return;
                    }
                    else
                    {
                        QueryStandard();
                    }
                }
                else
                {
                    MessageBox.Show("已经存在该钢种的标准！");
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void UpdateStandard()
        {
            try
            {
                if (this.tbx_standard.Text.Trim() == "" || this.tbx_steeltype.Text.Trim() == "")
                    return;


                string strSteelType = this.tbx_steeltype.Text.Trim();
                string strStandard = this.tbx_standard.Text.Trim();
                string strId = this.strCurID;
                string strType = "update";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.highspeedwire.HighSpeedWireInfo";
                ccp.MethodName = "GXPrintStandardManage";
                ccp.ServerParams = new object[] { strId, strType, strStandard, strSteelType };

                CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ret.ReturnCode != 0)
                {
                    MessageBox.Show("数据保存失败！");
                    return;
                }
                else
                {
                    QueryStandard();
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void DeleteStandard()
        {
            try
            {
                if (this.tbx_steeltype.Text.Trim().ToString() != ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.ToString())
                {
                    MessageBox.Show("请双击要删除的行！");
                    return;
                }
                    
                if (this.tbx_standard.Text.Trim() == "" || this.tbx_steeltype.Text.Trim() == "")
                    return;


                string strSteelType = this.tbx_steeltype.Text.Trim();
                string strStandard = this.tbx_standard.Text.Trim();
                string strId = this.strCurID;
                string strType = "delete";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.highspeedwire.HighSpeedWireInfo";
                ccp.MethodName = "GXPrintStandardManage";
                ccp.ServerParams = new object[] { strId, strType, strStandard, strSteelType };

                CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ret.ReturnCode != 0)
                {
                    MessageBox.Show("数据删除失败！");
                    return;
                }
                else
                {
                    QueryStandard();
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private bool CheckSteelTypeIsExist(string strSteelType)
        {
            try
            {
                string sql = "select fn_id,fs_SteelType,fs_Standard from BT_PRINTCARDSTANDARD where fs_steeltype = '" + strSteelType + "'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "hgjzjl.highspeedwire.HighSpeedWireInfo";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { sql };

                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return false;
            }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (e.Tool.Key == "查询")
            {
                QueryStandard();
            }
            if (e.Tool.Key == "新增")
            {
                AddNewStandard();
            }
            if (e.Tool.Key == "修改")
            {
                UpdateStandard();
            }
            if (e.Tool.Key == "删除")
            {
                DeleteStandard();
            }
        }
    }
}
