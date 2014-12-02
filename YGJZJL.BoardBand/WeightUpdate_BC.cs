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
using Infragistics.Win;
using System.Diagnostics;
using YGJZJL.PublicComponent;
using System.Data.SqlClient;

namespace YGJZJL.BoardBand
{
    public partial class WeightUpdate_BC : CoreFS.CA06.FrmBase
    {

        string m_szCurBC = "";
        string m_szCurBZ = "";
        BaseInfo m_BaseInfo = new BaseInfo();

        public WeightUpdate_BC()
        {
            InitializeComponent();
        }



        private bool CheckIsNumber(string strNum)
        {
            try
            {
                float i = Convert.ToSingle(strNum);
                return true;
            }
            catch (Exception exp)
            {
                WriteLog(exp.Message);
                return false;
            }
        }

        private void WriteLog(string str)
        {
            if (System.IO.Directory.Exists(System.Environment.CurrentDirectory + "\\log") == false)
            {
                System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\log");
            }

            string strDate = System.DateTime.Now.ToString("yyyyMMdd");
            System.IO.TextWriter tw = new System.IO.StreamWriter(System.Environment.CurrentDirectory + "\\log\\BCWeight_" + strDate + ".log", true);

            tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            tw.WriteLine(str);
            tw.WriteLine("\r\n");

            tw.Close();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (e.Tool.Key == "查询")
            {
                QueryBCWeightMainData();
            }
            if (e.Tool.Key == "修改主表")
            {
                UpdateBCWeightMain();
                //QueryBCWeightMainData();

            }
            if (e.Tool.Key == "修改从表")
            {
                UpdateBCWeightDetail();

            }
        }


        private void QueryBCWeightMainData()
        {
            try
            {
                string strSql = "select FS_BATCHNO,FS_PRODUCTNO,FS_ITEMNO,FS_MATERIALNO,FS_MRP,FS_STEELTYPE,FS_SPEC,FS_STORE,FN_BANDCOUNT,FN_BILLETCOUNT,TO_CHAR(FN_TOTALWEIGHT,'FM99999.000') AS FN_TOTALWEIGHT,FD_STARTTIME,FD_ENDTIME,FS_COMPLETEFLAG,FS_AUDITOR,FD_AUDITTIME,FS_UPLOADFLAG,FD_TOCENTERTIME,FS_ACCOUNTDATE,FD_TESTIFYDATE,FN_SINGLEBANDWEIGHT,FS_TECHCARDNO,FS_MATERIALNAME,FS_FLOW,FN_DECWEIGHT,FN_THEORYTOTALWEIGHT,FS_ISMATCH,FS_SAPSTORE,FS_SPRUEFLOS,FS_LLJZ,FS_HEADER from dt_productweightmain where 1=1 ";

                if (dpk_begin.Value > dpk_end.Value)
                {
                    MessageBox.Show("查询结束时间不能小于开始时间！");
                    return;
                }

                string startTime = dpk_begin.Value.ToString("yyyy-MM-dd 00:00:00");
                string endTime = dpk_end.Value.ToString("yyyy-MM-dd 23:59:59");

                if (dpk_begin.Value < dpk_end.Value)
                {
                    strSql += " and FD_STARTTIME between to_date('" + startTime + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + endTime + "','yyyy-MM-dd HH24:mi:ss')";
                }

                if (this.tbx_BatchNo.Text.Trim() != "")
                {
                    strSql += " and fs_batchno like '%" + this.tbx_BatchNo.Text.Trim() + "%'";
                }
                strSql += " order by fs_batchno";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.dataSet1.Tables[0].Rows.Clear();
                this.dataSet2.Tables[0].Rows.Clear();
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        DataRow newRow = this.dataSet1.Tables[0].NewRow();
                        foreach (DataColumn dc in this.dataSet1.Tables[0].Columns)
                        {
                            newRow[dc.ColumnName] = dt_temp.Rows[i][dc.ColumnName].ToString();
                        }
                        this.dataSet1.Tables[0].Rows.Add(newRow);
                    }
                    YGJZJL.PublicComponent.Constant.RefreshAndAutoSize(this.ultraGrid1);
                    this.dataSet1.Tables[0].AcceptChanges();
                }
                else
                { }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 删除剁帐系统相应数据
        /// </summary>
        /// <param name="strBatchNo">轧制编号</param>
        private void DeleteStowData(string strWeightNo)
        {
            try
            {
                System.Data.SqlClient.SqlConnection m_SqlConnection = new System.Data.SqlClient.SqlConnection("Data Source=10.32.200.2;Initial Catalog=L3_DepotMngr;User ID=sa;Password =prodepote");
                m_SqlConnection.Open();
                string strSql = "delete ZTJ_THEORYTOTRUE from ZTJ_THEORYTOTRUE where id = '" + strWeightNo + "'";
                System.Data.SqlClient.SqlCommand scc = new SqlCommand(strSql, m_SqlConnection);
                scc.ExecuteNonQuery();
                m_SqlConnection.Close();
                WriteLog("轧制编号：" + strWeightNo + "    响应条数" + scc.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }
        }

        private void QueryBCWeightDetailData(string strBatchNo)
        {
            try
            {
                string strSql = "select FS_WEIGHTNO,FS_BATCHNO,FN_BANDNO,FN_HOOKNO,FN_BANDBILLETCOUNT,FS_TYPE,FN_LENGTH,FN_WEIGHT,FS_PERSON,FS_POINT,FD_DATETIME,FS_SHIFT,FS_TERM,FS_LABELID,FN_DECWEIGHT,FS_FLAG,FS_UPFLAG,FN_THEORYWEIGHT from dt_productweightdetail where ";

                strSql += " fs_batchno = '" + strBatchNo + "'";
                strSql += " order by fn_bandno,length(fn_bandno)";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.dataSet2.Tables[0].Rows.Clear();
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        DataRow newRow = this.dataSet2.Tables[0].NewRow();
                        foreach (DataColumn dc in this.dataSet2.Tables[0].Columns)
                        {
                            newRow[dc.ColumnName] = dt_temp.Rows[i][dc.ColumnName].ToString();
                        }
                        this.dataSet2.Tables[0].Rows.Add(newRow);
                    }

                    YGJZJL.PublicComponent.Constant.RefreshAndAutoSize(this.ultraGrid2);
                    this.dataSet2.Tables[0].AcceptChanges();
                }
                else
                { }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// update main
        /// </summary>
        private void UpdateBCWeightMain()
        {
            try
            {
                if (this.ultraGrid1.Rows.Count == 0)
                    return;

                this.ultraGrid1.UpdateData();
                DataTable dt = this.dataSet1.Tables[0].GetChanges();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string p_FS_BATCHNO = dt.Rows[i]["FS_BATCHNO"].ToString();
                        string p_FS_PRODUCTNO = dt.Rows[i]["FS_PRODUCTNO"].ToString();
                        string p_FS_STEELTYPE = dt.Rows[i]["FS_STEELTYPE"].ToString();
                        string p_FS_SPEC = dt.Rows[i]["FS_SPEC"].ToString();
                        string p_FN_BANDCOUNT = dt.Rows[i]["FN_BANDCOUNT"].ToString();
                        string p_FN_BILLETCOUNT = dt.Rows[i]["FN_BILLETCOUNT"].ToString();
                        string p_FN_TOTALWEIGHT = dt.Rows[i]["FN_TOTALWEIGHT"].ToString();
                        string p_FD_STARTTIME = dt.Rows[i]["FD_STARTTIME"].ToString();
                        string p_FD_ENDTIME = dt.Rows[i]["FD_ENDTIME"].ToString();
                        string p_FS_COMPLETEFLAG = dt.Rows[i]["FS_COMPLETEFLAG"].ToString();
                        string p_FS_UPLOADFLAG = "update";
                        string p_FN_THEORYTOTALWEIGHT = dt.Rows[i]["FN_THEORYTOTALWEIGHT"].ToString();
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        if (IsUpLoadForMain(p_FS_BATCHNO, p_FN_TOTALWEIGHT, p_FN_THEORYTOTALWEIGHT))
                        {
                            MessageBox.Show("该数据已经上传,重量不允许修改！");
                            continue;
                        }


                        string m_Memo = "";
                        string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        string strIP = m_BaseInfo.getIPAddress();
                        string strMAC = m_BaseInfo.getMACAddress();

                        string strSql = " select FS_STEELTYPE,FN_BANDCOUNT,FN_TOTALWEIGHT from DT_PRODUCTWEIGHTMAIN where FS_BATCHNO = '" + p_FS_BATCHNO + "'";
                        DataTable dts = new DataTable();
                        dts = m_BaseInfo.QueryData(strSql);

                        if (p_FS_STEELTYPE != dts.Rows[0]["FS_STEELTYPE"].ToString().Trim())
                            m_Memo = m_Memo + " FS_STEELTYPE(牌号) = " + dts.Rows[0]["FS_STEELTYPE"].ToString().Trim() + " -> " + p_FS_STEELTYPE + "";

                        if (p_FN_TOTALWEIGHT != dts.Rows[0]["FN_TOTALWEIGHT"].ToString().Trim())
                            m_Memo = m_Memo + " FN_TOTALWEIGHT(总重量) = " + dts.Rows[0]["FN_TOTALWEIGHT"].ToString().Trim() + " -> " + p_FN_TOTALWEIGHT + "";

                        if (p_FN_BANDCOUNT != dts.Rows[0]["FN_BANDCOUNT"].ToString().Trim())
                            m_Memo = m_Memo + " FN_BANDCOUNT(吊数) = " + dts.Rows[0]["FN_BANDCOUNT"].ToString().Trim() + " -> " + p_FN_BANDCOUNT + "";

                        if (m_Memo != "")
                            m_BaseInfo.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, p_FS_BATCHNO, "", p_FS_BATCHNO, "", "", "DT_PRODUCTWEIGHTMAIN", "棒材入库/计量数据维护");

                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                        ccp.MethodName = "UpdateBCWeightMain";
                        ccp.ServerParams = new object[] {p_FS_BATCHNO,
                            p_FS_PRODUCTNO,
                            p_FS_STEELTYPE,
                            p_FS_SPEC,
                            p_FN_BANDCOUNT,
                            p_FN_BILLETCOUNT,
                            p_FN_TOTALWEIGHT,
                            p_FD_STARTTIME,
                            p_FD_ENDTIME,
                            p_FS_COMPLETEFLAG,
                            p_FS_UPLOADFLAG,
                            p_FN_THEORYTOTALWEIGHT,
                            p_UPDATER};
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                        if (ret.ReturnCode != 0)
                        {
                            MessageBox.Show("修改保存失败！");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    this.QueryBCWeightMainData();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// delelte main
        /// </summary>
        private void DeleteBCWeightMain()
        {
            try
            {
                if (this.ultraGrid1.Rows.Count == 0)
                    return;
                if (this.ultraGrid1.ActiveRow == null)
                    return;

                DialogResult result = MessageBox.Show(this, "删除主表将直接删除从表且清空对应的流动卡重量数据，是否继续删除？", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string p_FS_BATCHNO = this.ultraGrid1.ActiveRow.Cells["FS_BATCHNO"].Text.Trim();
                    string p_FS_PRODUCTNO = this.ultraGrid1.ActiveRow.Cells["FS_PRODUCTNO"].Text.Trim();
                    string p_FS_STEELTYPE = this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim();
                    string p_FS_SPEC = this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text.Trim();
                    string p_FN_BANDCOUNT = this.ultraGrid1.ActiveRow.Cells["FN_BANDCOUNT"].Text.Trim();
                    string p_FN_BILLETCOUNT = this.ultraGrid1.ActiveRow.Cells["FN_BILLETCOUNT"].Text.Trim();
                    string p_FN_TOTALWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_TOTALWEIGHT"].Text.Trim();
                    string p_FD_STARTTIME = this.ultraGrid1.ActiveRow.Cells["FD_STARTTIME"].Text.Trim();
                    string p_FD_ENDTIME = this.ultraGrid1.ActiveRow.Cells["FD_ENDTIME"].Text.Trim();
                    string p_FS_COMPLETEFLAG = this.ultraGrid1.ActiveRow.Cells["FS_COMPLETEFLAG"].Text.Trim();
                    string p_FS_UPLOADFLAG = "delete";
                    string p_FN_THEORYTOTALWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_THEORYTOTALWEIGHT"].Text.Trim();
                    string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();


                    string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string strIP = m_BaseInfo.getIPAddress();
                    string strMAC = m_BaseInfo.getMACAddress();
                    //string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    m_BaseInfo.insertLog(strDate, "删除", p_UPDATER, strIP, strMAC, "", p_FS_BATCHNO, "", p_FS_BATCHNO, "", "", "DT_PRODUCTWEIGHTMAIN", "棒材入库/计量数据维护");

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                    ccp.MethodName = "UpdateBCWeightMain";
                    ccp.ServerParams = new object[] {p_FS_BATCHNO,
                            p_FS_PRODUCTNO,
                            p_FS_STEELTYPE,
                            p_FS_SPEC,
                            p_FN_BANDCOUNT,
                            p_FN_BILLETCOUNT,
                            p_FN_TOTALWEIGHT,
                            p_FD_STARTTIME,
                            p_FD_ENDTIME,
                            p_FS_COMPLETEFLAG,
                            p_FS_UPLOADFLAG,
                            p_FN_THEORYTOTALWEIGHT,
                            p_UPDATER};
                    CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    if (ret.ReturnCode != 0)
                    {
                        MessageBox.Show("删除保存失败！");
                    }
                    else
                    {
                        this.QueryBCWeightMainData();
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private bool CheckBatchNoIsExist(string strBatchNo)
        {
            try
            {
                string strSql = "select * from dt_productweightmain where fs_batchno = '" + strBatchNo + "'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
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

        /// <summary>
        /// add main
        /// </summary>
        private void AddBCWeightMain()
        {
            try
            {
                if (this.ultraGrid1.Rows.Count == 0)
                    return;
                if (this.ultraGrid1.ActiveRow == null)
                    return;

                if (this.tbx_hBatchNo.Text.Trim() == "" || this.tbx_hOrderNo.Text.Trim() == "")
                    return;

                string p_FS_BATCHNO = this.tbx_hBatchNo.Text.Trim();
                string p_FS_PRODUCTNO = this.tbx_hOrderNo.Text.Trim();
                string p_FS_STEELTYPE = this.tbx_hSteelType.Text.Trim();
                string p_FS_SPEC = this.tbx_hSpec.Text.Trim();
                string p_FN_BANDCOUNT = "0";
                string p_FN_BILLETCOUNT = "0";
                string p_FN_TOTALWEIGHT = "0";
                string p_FD_STARTTIME = this.dpk_hWeightTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string p_FD_ENDTIME = this.dpk_hWeightTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string p_FS_COMPLETEFLAG = "0";
                string p_FS_UPLOADFLAG = "add";
                string p_FN_THEORYTOTALWEIGHT = "0";
                string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string strIP = m_BaseInfo.getIPAddress();
                string strMAC = m_BaseInfo.getMACAddress();
                //string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

               // m_BaseInfo.insertLog(strDate, "增加", p_UPDATER, strIP, strMAC, "", p_FS_BATCHNO, "", p_FS_BATCHNO, "", "", "DT_PRODUCTWEIGHTMAIN", "棒材入库/计量数据维护");


                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "UpdateBCWeightMain";
                ccp.ServerParams = new object[] {p_FS_BATCHNO,
                            p_FS_PRODUCTNO,
                            p_FS_STEELTYPE,
                            p_FS_SPEC,
                            p_FN_BANDCOUNT,
                            p_FN_BILLETCOUNT,
                            p_FN_TOTALWEIGHT,
                            p_FD_STARTTIME,
                            p_FD_ENDTIME,
                            p_FS_COMPLETEFLAG,
                            p_FS_UPLOADFLAG,
                            p_FN_THEORYTOTALWEIGHT,
                            p_UPDATER};
                CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                if (ret.ReturnCode != 0)
                {
                    MessageBox.Show("新增保存失败！");
                }
                else
                {
                    this.QueryBCWeightMainData();
                }


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        /// <summary>
        /// 主表是否上传
        /// </summary>
        /// <param name="strBatchNo"></param>
        /// <returns></returns>
        private bool IsUpLoadForMain(string strBatchNo, string strWeight, string strTheoryWeight)
        {
            bool reVal = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = "select FS_UPLOADFLAG,to_char(FN_TOTALWEIGHT,'FM99999.000') AS FN_TOTALWEIGHT,TO_CHAR(FN_THEORYTOTALWEIGHT,'FM99999.000') AS FN_THEORYTOTALWEIGHT from DT_PRODUCTWEIGHTMAIN where FS_BATCHNO = '" + strBatchNo + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = dt;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt.Rows[0]["FS_UPLOADFLAG"].ToString() == "1" && (dt.Rows[0]["FN_TOTALWEIGHT"].ToString() != strWeight || dt.Rows[0]["FN_THEORYTOTALWEIGHT"].ToString() != strTheoryWeight))
                {
                    reVal = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return reVal;
        }

        /// <summary>
        /// 从表是否上传
        /// </summary>
        /// <param name="strBatchNo"></param>
        /// <param name="strBandNo"></param>
        /// <returns></returns>
        private bool IsUpLoadForDetail(string strWeightNo, string strBatchNo, string strBandNo, string strWeight, string strTheoryWeight)
        {
            bool reVal = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = "select FS_UPLOADFLAG,FS_BATCHNO,FN_BANDNO,to_char(FN_WEIGHT,'FM99999.000') AS FN_WEIGHT,TO_CHAR(FN_THEORYWEIGHT,'FM99999.000') AS FN_THEORYWEIGHT from DT_PRODUCTWEIGHTDETAIL where FS_WEIGHTNO = '" + strWeightNo + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = dt;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt.Rows[0]["FS_UPLOADFLAG"].ToString() == "1" && (dt.Rows[0]["FS_BATCHNO"].ToString() != strBatchNo || dt.Rows[0]["FN_BANDNO"].ToString() != strBandNo || dt.Rows[0]["FN_WEIGHT"].ToString() != strWeight || dt.Rows[0]["FN_THEORYWEIGHT"].ToString() != strTheoryWeight))
                {
                    reVal = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return reVal;
        }

        /// <summary>
        /// update detail
        /// </summary>
        private void UpdateBCWeightDetail()
        {
            try
            {
                if (this.ultraGrid2.Rows.Count == 0)
                    return;

                this.ultraGrid2.UpdateData();
                DataTable dt = this.dataSet2.Tables[0].GetChanges();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string p_FS_WEIGHTNO = dt.Rows[i]["FS_WEIGHTNO"].ToString();
                        string p_FS_BATCHNO = dt.Rows[i]["FS_BATCHNO"].ToString();
                        string p_FN_BANDNO = dt.Rows[i]["FN_BANDNO"].ToString();
                        string p_FN_BANDBILLETCOUNT = dt.Rows[i]["FN_BANDBILLETCOUNT"].ToString();
                        string p_FS_TYPE = dt.Rows[i]["FS_TYPE"].ToString();
                        string p_FN_LENGTH = dt.Rows[i]["FN_LENGTH"].ToString();
                        string p_FN_WEIGHT = dt.Rows[i]["FN_WEIGHT"].ToString();
                        string p_FD_DATETIME = dt.Rows[i]["FD_DATETIME"].ToString();
                        string p_FS_SHIFT = dt.Rows[i]["FS_SHIFT"].ToString();
                        string p_FS_TERM = dt.Rows[i]["FS_TERM"].ToString();
                        string p_FS_LABELID = dt.Rows[i]["FS_LABELID"].ToString();
                        string p_FS_UPFLAG = "update";
                        string p_FN_THEORYWEIGHT = dt.Rows[i]["FN_THEORYWEIGHT"].ToString();
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        if (IsUpLoadForDetail(p_FS_WEIGHTNO, p_FS_BATCHNO, p_FN_BANDNO, p_FN_WEIGHT, p_FN_THEORYWEIGHT))
                        {
                            MessageBox.Show("该数据已经上传，批次号、吊号、重量不允许修改！");
                            return;
                        }

                        string m_Memo = "";
                        string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        string strIP = m_BaseInfo.getIPAddress();
                        string strMAC = m_BaseInfo.getMACAddress();

                        string strSql = " select FS_BATCHNO,FN_BANDNO,FN_WEIGHT from DT_PRODUCTWEIGHTDETAIL where FS_WEIGHTNO = '" + p_FS_WEIGHTNO + "'";
                        DataTable dts = new DataTable();
                        dts = m_BaseInfo.QueryData(strSql);

                        if (p_FS_BATCHNO != dts.Rows[0]["FS_BATCHNO"].ToString().Trim())
                            m_Memo = m_Memo + " FS_BATCHNO(轧制编号) = " + dts.Rows[0]["FS_BATCHNO"].ToString().Trim() + " -> " + p_FS_BATCHNO + "";

                        if (p_FN_BANDNO != dts.Rows[0]["FN_BANDNO"].ToString().Trim())
                            m_Memo = m_Memo + " FN_BANDNO(吊号) = " + dts.Rows[0]["FN_BANDNO"].ToString().Trim() + " -> " + p_FN_BANDNO + "";

                        if (p_FN_WEIGHT != dts.Rows[0]["FN_WEIGHT"].ToString().Trim())
                            m_Memo = m_Memo + " FN_WEIGHT(重量) = " + dts.Rows[0]["FN_WEIGHT"].ToString().Trim() + " -> " + p_FN_WEIGHT + "";

                        if (m_Memo != "")
                            m_BaseInfo.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, p_FS_WEIGHTNO, "", dts.Rows[0]["FS_BATCHNO"].ToString().Trim(), "", dts.Rows[0]["FN_BANDNO"].ToString().Trim(), "DT_PRODUCTWEIGHTDETAIL", "棒材入库/计量数据维护");


                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                        ccp.MethodName = "UpdateBCWeightDetail";
                        ccp.ServerParams = new object[] {p_FS_WEIGHTNO,
                            p_FS_BATCHNO,
                            p_FN_BANDNO,
                            p_FN_BANDBILLETCOUNT,
                            p_FS_TYPE,
                            p_FN_LENGTH,
                            p_FN_WEIGHT,
                            p_FD_DATETIME,
                            p_FS_SHIFT,
                            p_FS_TERM,
                            p_FS_LABELID,
                            p_FS_UPFLAG,
                            p_FN_THEORYWEIGHT,
                            p_UPDATER};
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                        if (ret.ReturnCode != 0)
                        {
                            MessageBox.Show("修改保存失败！");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    this.QueryBCWeightMainData();
                    this.QueryBCWeightDetailData(dt.Rows[0]["FS_BATCHNO"].ToString());

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// delete detail
        /// </summary>
        private string DeleteBCWeightDetail()
        {
            string fs_weightNo = "";
            try
            {
                if (this.ultraGrid2.Rows.Count == 0)
                    return fs_weightNo;
                if (this.ultraGrid2.ActiveRow == null)
                    return fs_weightNo;
                DialogResult result = MessageBox.Show(this, "删除的明细数据不可恢复，是否继续删除？", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string p_FS_WEIGHTNO = this.ultraGrid2.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim();
                    string p_FS_BATCHNO = this.ultraGrid2.ActiveRow.Cells["FS_BATCHNO"].Text.Trim();
                    string p_FN_BANDNO = this.ultraGrid2.ActiveRow.Cells["FN_BANDNO"].Text.Trim();
                    string p_FN_BANDBILLETCOUNT = this.ultraGrid2.ActiveRow.Cells["FN_BANDBILLETCOUNT"].Text.Trim();
                    string p_FS_TYPE = this.ultraGrid2.ActiveRow.Cells["FS_TYPE"].Text.Trim();
                    string p_FN_LENGTH = this.ultraGrid2.ActiveRow.Cells["FN_LENGTH"].Text.Trim();
                    string p_FN_WEIGHT = this.ultraGrid2.ActiveRow.Cells["FN_WEIGHT"].Text.Trim();
                    string p_FD_DATETIME = this.ultraGrid2.ActiveRow.Cells["FD_DATETIME"].Text.Trim();
                    string p_FS_SHIFT = this.ultraGrid2.ActiveRow.Cells["FS_SHIFT"].Text.Trim();
                    string p_FS_TERM = this.ultraGrid2.ActiveRow.Cells["FS_TERM"].Text.Trim();
                    string p_FS_LABELID = this.ultraGrid2.ActiveRow.Cells["FS_LABELID"].Text.Trim();
                    string p_FS_UPFLAG = "delete";
                    string p_FN_THEORYWEIGHT = this.ultraGrid2.ActiveRow.Cells["FN_THEORYWEIGHT"].Text.Trim();
                    string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string strIP = m_BaseInfo.getIPAddress();
                    string strMAC = m_BaseInfo.getMACAddress();
                    //string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    //m_BaseInfo.insertLog(strDate, "删除", p_UPDATER, strIP, strMAC, "", p_FS_WEIGHTNO, "", p_FS_BATCHNO, "", p_FN_BANDNO, "DT_PRODUCTWEIGHTDETAIL", "棒材入库/计量数据维护");


                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                    ccp.MethodName = "UpdateBCWeightDetail";
                    ccp.ServerParams = new object[] {p_FS_WEIGHTNO,
                            p_FS_BATCHNO,
                            p_FN_BANDNO,
                            p_FN_BANDBILLETCOUNT,
                            p_FS_TYPE,
                            p_FN_LENGTH,
                            p_FN_WEIGHT,
                            p_FD_DATETIME,
                            p_FS_SHIFT,
                            p_FS_TERM,
                            p_FS_LABELID,
                            p_FS_UPFLAG,
                            p_FN_THEORYWEIGHT,
                            p_UPDATER};
                    CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    if (ret.ReturnCode != 0)
                    {
                        MessageBox.Show("删除保存失败！");
                    }
                    fs_weightNo = p_FS_WEIGHTNO;
                    this.QueryBCWeightMainData();
                    SelectActiveRow(p_FS_BATCHNO);
                    QueryBCWeightDetailData(p_FS_BATCHNO);
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return fs_weightNo;
        }

        /// <summary>
        /// add detail
        /// </summary>
        private void AddBCWeightDetail()
        {
            if (this.ultraGrid1.Rows.Count == 0 || this.ultraGrid1.ActiveRow == null)
                return;

            if (CheckIsNumber(this.tbx_RealWeight.Text.Trim()))
            {
                try
                {
                    if (this.ultraGrid2.Rows.Count == 0)
                    {
                        string p_FS_BATCHNO = this.ultraGrid1.ActiveRow.Cells["FS_BATCHNO"].Text.Trim();
                        string p_FS_PRODUCTNO = this.ultraGrid1.ActiveRow.Cells["FS_PRODUCTNO"].Text.Trim();
                        string p_FS_ITEMNO = this.ultraGrid1.ActiveRow.Cells["FS_ITEMNO"].Text.Trim();
                        string p_FS_MATERIALNO = this.ultraGrid1.ActiveRow.Cells["FS_MATERIALNO"].Text.Trim();
                        string p_FS_MRP = "";
                        string p_FS_STORE = "";
                        string p_FS_STEELTYPE = this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim();
                        string p_FS_SPEC = this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text.Trim();
                        string p_FS_WEIGHTNO = Guid.NewGuid().ToString();

                        string p_FS_TYPE = this.cbx_Type.Text.Trim();

                        float p_FN_LENGTH = 0;
                        int p_FN_BANDBILLETCOUNT = 0;
                        if (p_FS_TYPE == "非定尺")
                        {
                            p_FN_LENGTH = 0;
                            p_FN_BANDBILLETCOUNT = 0;
                        }
                        else
                        {
                            p_FN_LENGTH = Convert.ToSingle(this.tbx_length.Text.Trim());
                            p_FN_BANDBILLETCOUNT = Convert.ToInt32(this.tbx_BilletCount.Text.Trim());
                        }
                        double p_FN_WEIGHT = Convert.ToDouble(this.tbx_RealWeight.Text.Trim());
                        string p_FS_PERSON = "h" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                        string p_FS_POINT = "";
                        p_FS_POINT = "K17";
                        string p_FS_SHIFT = this.cbx_shift.Text.Trim();
                        string p_FS_TERM = this.cbx_term.Text.Trim();
                        string p_FS_LABELID = GetBarCode(p_FS_BATCHNO);
                        string p_FS_MATERIALNAME = "";
                        string p_FS_FLOW = "";
                        float p_FN_DECWEIGHT = 0;

                        string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        string strIP = m_BaseInfo.getIPAddress();
                        string strMAC = m_BaseInfo.getMACAddress();
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        m_BaseInfo.insertLog(strDate, "增加", p_UPDATER, strIP, strMAC, "", p_FS_WEIGHTNO, "", p_FS_BATCHNO, "", "", "DT_PRODUCTWEIGHTDETAIL", "棒材入库/计量数据维护");


                        CoreClientParam ccp = new CoreClientParam();
                        if (this.ultraGrid1.ActiveRow.Cells["FS_COMPLETEFLAG"].Text == "1")
                        {
                            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                            ccp.MethodName = "addBatchNoForHand";
                        }
                        else
                        {
                            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                            ccp.MethodName = "addBatchNo";
                        }
                        ccp.ServerParams = new object[] { p_FS_BATCHNO,
			                                    p_FS_PRODUCTNO,
                                                p_FS_ITEMNO,
		                                        p_FS_MATERIALNO,
		                                        p_FS_MRP,
		                                        p_FS_STORE,
		                                        p_FS_STEELTYPE,
		                                        p_FS_SPEC,
		                                        p_FS_WEIGHTNO,
		                                        p_FN_BANDBILLETCOUNT,
		                                        p_FS_TYPE,
		                                        p_FN_LENGTH,
		                                        p_FN_WEIGHT,
		                                        p_FS_PERSON,
		                                        p_FS_POINT,
		                                        p_FS_SHIFT,
                                                p_FS_TERM,
		                                        p_FS_LABELID,
                                                p_FS_MATERIALNAME,
                                                p_FS_FLOW,
                                                p_FN_DECWEIGHT};
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        if (ret.ReturnCode != 0)
                        {
                            MessageBox.Show("数据保存失败！");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("手工录入数据保存成功！");
                            //this.tbx_hWeight.Text = "";
                            this.QueryBCWeightMainData();
                            SelectActiveRow(p_FS_BATCHNO);
                            QueryBCWeightDetailData(p_FS_BATCHNO);
                            return;
                        }
                    }
                    else
                    {
                        string p_FS_BATCHNO = this.ultraGrid2.Rows[0].Cells["FS_BATCHNO"].Text.Trim();
                        string p_FS_PRODUCTNO = this.ultraGrid1.Rows[0].Cells["FS_PRODUCTNO"].Text.Trim();
                        string p_FS_ITEMNO = this.ultraGrid1.ActiveRow.Cells["FS_ITEMNO"].Text.Trim();
                        string p_FS_MATERIALNO = this.ultraGrid1.ActiveRow.Cells["FS_MATERIALNO"].Text.Trim();
                        string p_FS_MRP = "";
                        string p_FS_STORE = "";
                        string p_FS_STEELTYPE = this.ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Text.Trim();
                        string p_FS_SPEC = this.ultraGrid1.ActiveRow.Cells["FS_SPEC"].Text.Trim();
                        string p_FS_WEIGHTNO = Guid.NewGuid().ToString();

                        string p_FS_TYPE = this.cbx_Type.Text.Trim();

                        float p_FN_LENGTH = 0;
                        int p_FN_BANDBILLETCOUNT = 0;
                        if (p_FS_TYPE == "非定尺")
                        {
                            p_FN_LENGTH = 0;
                            p_FN_BANDBILLETCOUNT = 0;
                        }
                        else
                        {
                            p_FN_LENGTH = Convert.ToSingle(this.ultraGrid2.Rows[0].Cells["FN_LENGTH"].Text.Trim());
                            p_FN_BANDBILLETCOUNT = Convert.ToInt32(this.ultraGrid2.Rows[0].Cells["FN_BANDBILLETCOUNT"].Text.Trim());
                        }
                        double p_FN_WEIGHT = Convert.ToDouble(this.tbx_RealWeight.Text.Trim());
                        string p_FS_PERSON = "h" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                        string p_FS_POINT = "K17";
                        string p_FS_SHIFT = this.ultraGrid2.Rows[0].Cells["FS_SHIFT"].Text.Trim();
                        string p_FS_TERM = this.ultraGrid2.Rows[0].Cells["FS_TERM"].Text.Trim();
                        string p_FS_LABELID = GetBarCode(p_FS_BATCHNO);
                        string p_FS_MATERIALNAME = "";
                        string p_FS_FLOW = "";
                        float p_FN_DECWEIGHT = 0;

                        CoreClientParam ccp = new CoreClientParam();
                        if (this.ultraGrid1.ActiveRow.Cells["FS_COMPLETEFLAG"].Text == "1")
                        {
                            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                            ccp.MethodName = "addBatchNoForHand";
                        }
                        else
                        {
                            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                            ccp.MethodName = "addBatchNo";
                        }
                        ccp.ServerParams = new object[] { p_FS_BATCHNO,
			                                    p_FS_PRODUCTNO,
                                                p_FS_ITEMNO,
		                                        p_FS_MATERIALNO,
		                                        p_FS_MRP,
		                                        p_FS_STORE,
		                                        p_FS_STEELTYPE,
		                                        p_FS_SPEC,
		                                        p_FS_WEIGHTNO,
		                                        p_FN_BANDBILLETCOUNT,
		                                        p_FS_TYPE,
		                                        p_FN_LENGTH,
		                                        p_FN_WEIGHT,
		                                        p_FS_PERSON,
		                                        p_FS_POINT,
		                                        p_FS_SHIFT,
                                                p_FS_TERM,
		                                        p_FS_LABELID,
                                                p_FS_MATERIALNAME,
                                                p_FS_FLOW,
                                                p_FN_DECWEIGHT};
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        if (ret.ReturnCode != 0)
                        {
                            MessageBox.Show("数据保存失败！");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("手工录入数据保存成功！");
                            this.QueryBCWeightMainData();
                            SelectActiveRow(p_FS_BATCHNO);
                            QueryBCWeightDetailData(p_FS_BATCHNO);
                            return;
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            else
            {
                MessageBox.Show("手工录入的重量不是有效的数字！");
                return;
            }
        }

        private void SelectActiveRow(string strBatchNo)
        {
            if (this.ultraGrid1.Rows.Count == 0)
                return;
            for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
            {
                if (this.ultraGrid1.Rows[i].Cells["FS_BATCHNO"].Text.Trim() == strBatchNo)
                {
                    this.ultraGrid1.Rows[i].Selected = true;
                    this.ultraGrid1.Rows[i].Activated = true;
                    return;
                }
            }

        }
        /// <summary>
        /// 获取条形码
        /// </summary>
        /// <param name="strBatchNo">轧制编号</param>
        /// <returns>条行码</returns>
        private string GetBarCode(string strBatchNo)
        {
            DataTable dt = new DataTable();
            dt.Clear();

            string sql = "select max(FN_BandNo) + 1 as bandNo from dt_productweightdetail where fs_batchno = '" + strBatchNo + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            string strBandNo = dt.Rows[0]["bandNo"].ToString().Trim();

            if (strBandNo.Length == 1)
                strBandNo = "0" + strBandNo;
            else if (strBandNo.Length == 0)
                strBandNo = "01";

            string strYear = System.DateTime.Now.Year.ToString().Substring(2, 2);

            string strBarCode = "402" + "2" + strYear + strBatchNo.Substring(2, 6) + strBandNo;

            return strBarCode;
        }


        private void WeightRePrint_BC_Load(object sender, EventArgs e)
        {
            this.dpk_begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

            m_szCurBC = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            m_szCurBZ = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
        }

        private void ultraGrid1_Click(object sender, EventArgs e)
        {
            UIElement mainElement;
            UIElement element;
            Point screenPoint;
            Point clientPoint;
            UltraGridRow row;
            mainElement = this.ultraGrid1.DisplayLayout.UIElement;
            screenPoint = Control.MousePosition;
            clientPoint = this.ultraGrid1.PointToClient(screenPoint);
            element = mainElement.ElementFromPoint(clientPoint);
            if (element == null)
            {
                return;
            }
            row = element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
            if (row == null)
            {
                return;
            }
            if (this.ultraGrid1.ActiveRow == null || this.ultraGrid1.ActiveRow.Index < 0)
            {
                return;
            }
            if (this.ultraGrid1.ActiveRow == null)
            {
                return;
            }

            if (this.ultraGrid1.Rows.Count == 0 || this.ultraGrid1.ActiveRow == null)
            {
                return;
            }

            QueryBCWeightDetailData(this.ultraGrid1.ActiveRow.Cells["FS_BATCHNO"].Text.Trim());
        }
        /// <summary>
        /// 导出主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (this.ultraGrid1.Rows.Count > 0)
            {
                YGJZJL.PublicComponent.Constant.ExportGrid2Excel(this, ultraGridExcelExporter1, ultraGrid1);
            }
        }
        /// <summary>
        /// 导出从表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            if (this.ultraGrid2.Rows.Count > 0)
            {
                YGJZJL.PublicComponent.Constant.ExportGrid2Excel(this, ultraGridExcelExporter2, ultraGrid2);
            }
        }
        /// <summary>
        /// 新增主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            AddBCWeightMain();
        }
        /// <summary>
        /// 新增从表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            AddBCWeightDetail();
        }
        /// <summary>
        /// 修改主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            m_BaseInfo.ob = this.ob;
            UpdateBCWeightMain();
        }
        /// <summary>
        /// 修改从表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click_1(object sender, EventArgs e)
        {
            m_BaseInfo.ob = this.ob;
            UpdateBCWeightDetail();
        }
        /// <summary>
        /// 删除主表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click_1(object sender, EventArgs e)
        {
            DeleteBCWeightMain();
        }
        /// <summary>
        /// 删除从表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click_1(object sender, EventArgs e)
        {
            string weightno = DeleteBCWeightDetail();
            if (!weightno.Equals(""))
            {
                DeleteStowData(weightno);
            }
        }

        private void ultraGrid2_CellChange(object sender, CellEventArgs e)
        {
            int index = e.Cell.Row.Index;
            if (e.Cell == ultraGrid2.Rows[index].Cells["FN_BANDBILLETCOUNT"])
            {
                try
                {
                    int intBilletCount = Convert.ToInt16(ultraGrid2.Rows[index].Cells["FN_BANDBILLETCOUNT"].Text.ToString());
                    string strBatchNo = ultraGrid2.Rows[index].Cells["FS_BATCHNO"].Value.ToString();
                    string p_FS_SPEC = string.Empty;
                    string p_FN_LENGTH = ultraGrid2.Rows[index].Cells["FN_LENGTH"].Value.ToString();
                    for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                    {
                        if (strBatchNo == ultraGrid1.Rows[i].Cells["FS_BATCHNO"].Value.ToString())
                        {
                            p_FS_SPEC = ultraGrid1.Rows[i].Cells["FS_SPEC"].Value.ToString();
                        }
                    }
                    if (p_FS_SPEC != string.Empty && p_FN_LENGTH != string.Empty)
                    {
                        string strSql = "select fn_weight from bt_bctheoryweightinfo where fs_spec = '"+p_FS_SPEC+"' and fn_length = '"+p_FN_LENGTH+"'";
                        DataTable dt = new DataTable();
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                        ccp.MethodName = "ExcuteQuery";
                        ccp.ServerParams = new object[] { strSql };
                        ccp.SourceDataTable = dt;
                        this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                        if (dt.Rows.Count > 0)
                        {
                            decimal fn_weight = Convert.ToDecimal(dt.Rows[0]["fn_weight"].ToString());
                            string fn_singleTheoryWeight = Math.Round(fn_weight * intBilletCount / 1000, 3).ToString();
                            ultraGrid2.Rows[index].Cells["FN_THEORYWEIGHT"].Value = fn_singleTheoryWeight;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("支数应为数字类型！");
                    return;
                }
            }
        }
    }
}
