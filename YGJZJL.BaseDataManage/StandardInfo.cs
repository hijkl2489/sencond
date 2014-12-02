using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using Core.Sip.Client.App;

//using SerialCommlib;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using System.Threading;
using System.IO;


namespace YGJZJL.BaseDataManage
{
    public partial class StandardInfo : FrmBase
    {
        string strCurID = "";

        public StandardInfo()
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
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
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


                    Hashtable param = new Hashtable();
                    param.Add("I1", strId);
                    param.Add("I2", strType);
                    param.Add("I3", strStandard);
                    param.Add("I4", strSteelType);
                   


                    CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_HighWirePredictInfo.GXPrintStandardManage(?,?,?,?)}", param);


                   
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

              

                Hashtable param = new Hashtable();
                param.Add("I1", strId);
                param.Add("I2", strType);
                param.Add("I3", strStandard);
                param.Add("I4", strSteelType);



                CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_HighWirePredictInfo.GXPrintStandardManage(?,?,?,?)}", param);
                if (ccp.ReturnCode != 0)
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

           


                Hashtable param = new Hashtable();
                param.Add("I1", strId);
                param.Add("I2", strType);
                param.Add("I3", strStandard);
                param.Add("I4", strSteelType);



                CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_HighWirePredictInfo.GXPrintStandardManage(?,?,?,?)}", param);
                if (ccp.ReturnCode != 0)
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
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
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
                QueryStandard();
            }
            if (e.Tool.Key == "修改")
            {
                UpdateStandard();
                QueryStandard();
            }
            if (e.Tool.Key == "删除")
            {
                DeleteStandard();
                QueryStandard();
            }
        }

        /// <summary>
        /// 执行存储过程2
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected CoreClientParam excuteProcedure2(string sql, Hashtable param)
        {
            if (param == null)
            {
                param = new Hashtable();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "executeProcedureBySql2";
            ccp.ServerParams = new object[] { sql, param };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            return ccp;
        }
    }
}
