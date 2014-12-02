using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;
using System.Data.SqlClient;
using System.Threading;
using System.Collections;
using System.Data.OracleClient;
using System.IO;
using INI;

namespace YGJZJL.SquareBilletTransfer
{
    public partial class WeightPlan_BP : FrmBase
    {
        private Thread m_thread;
        private bool m_flag;
        public string PointID = "K19";
        string stoveno = "";
        string count = "";
        string m_query = "0";//预报查询刷新占用标志 1 为占用 2 空闲
        GetBaseInfo BaseInfo; //基础信息操作
        BaseInfo m_BaseInfo;
        private DataTable dt;
        private DataTable m_SteelTypeTable = new DataTable();//钢种数据表
        private DataTable tempSteelType = new DataTable();
        private string valueWL = "";
        private DataTable m_SpecTable = new DataTable();//规格数据表
        private DataTable tempSpec = new DataTable();
        bool m_hRunning = false;
        bool m_AutoRunning = false;
        public delegate void DataUpThreadDelegate();//绑定委托
        private DataUpThreadDelegate m_DataUpThreadDelegate;//建立委托变量

        public delegate void QueryYBThreadDelegate();//绑定委托
        private QueryYBThreadDelegate m_QueryYBThreadDelegate;//建立委托变量
        string m_insert_hxcf = "0";//获取化学成分占用标志 1 为占用 2 空闲

        bool _flashFlag1 = false;
        bool _flashFlag2 = false;
          
        public WeightPlan_BP()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private bool IsInt(string str)
        {
            try
            {
                int i = Convert.ToInt32(str);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool IsNumber(string str)
        {
            try
            {
                double i = Convert.ToDouble(str);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool DoCheck()
        {
            bool reVal = false;
            if (this.tbStoveNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入炉号！");
                reVal = true;
            }
            else if(tbStoveNo.Text.Trim().Length!=9&&!tbStoveNo.Text.Trim().Contains("-"))
            {
                MessageBox.Show("输入的炉号格式不正确！");
                reVal = true;
            }
            else if (tbStoveNo.Text.Trim().Length == 9 && tbStoveNo.Text.Trim().Contains("-"))
            {
                MessageBox.Show("输入的炉号格式不正确！");
                reVal = true;
            }

            if (this.cbSteelType.Text.Trim() == "")
            {
                MessageBox.Show("请输入钢种！");
                reVal = true;
            }
            if (this.cbSpec.Text.Trim() == "")
            {
                MessageBox.Show("请输入规格！");
                reVal = true;
            }
            if (!IsNumber(this.tbLength.Text.Trim()))
            {
                MessageBox.Show("请输入正确的定尺长度！");
                reVal = true;
            }
           
            if (LxComb.Text == string.Empty)
            {
                MessageBox.Show("去向不能为空，请选择！");
                reVal = true;
            }
            return reVal;
        }

        private void Update_FP_Card()
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请选择需要修改的流动卡记录！");
                return;
            }

            if (isCompleteStove(ugr.Cells["FS_GP_STOVENO"].Text.ToString()))
            {
                MessageBox.Show("该炉已计量完，不能修改预报条数！");
                return;
            }

            if (!IsNumber(this.tbLength.Text.Trim()))
            {
                MessageBox.Show("请输入正确的定尺长度！");
                return;
            }

            if (!IsInt(this.tbCount.Text.Trim()))
            {
                MessageBox.Show("请输入正确的条数！");
                return;
            }

            BaseOperation();

            string p_FS_CARDNO = tbCardNo.Text.Trim();
            string p_FS_GP_STEELTYPE = cbSteelType.Text.Trim();
            string p_FS_GP_SPE = cbSpec.Text.Trim();
            string p_FN_GP_LEN = tbLength.Text.Trim();
            string p_FN_GP_C = tbC.Text.Trim();
            //return;

            string p_FN_GP_SI = tbSi.Text.Trim();
            string p_FN_GP_MN = tbMn.Text.Trim();
            string p_FN_GP_S = tbS.Text.Trim();
            string p_FN_GP_P = tbP.Text.Trim();
            string p_FN_GP_TI = tbTi.Text.Trim();
            string p_FN_GP_SB = tbSb.Text.Trim();
            string p_FN_GP_ALS = tbAls.Text.Trim();
           
            int p_FN_GP_TOTALCOUNT = Convert.ToInt32(tbCount.Text.Trim());
            
            string p_FS_GP_JUDGER = tbPerson.Text.Trim();
            string p_FS_ADVISESPEC = tbADVSPEC.Text.Trim();
            string p_FS_GDFLAG = "1";
            string p_FS_QYG = "";

            //新增是否组织利用字段
            string p_FS_STEELSENDTYPE = this.cbx_SteelSendType.Text.Trim();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "Update_FP_GP_ZJ";
            //ccp.ServerParams = new object[] { p_FS_CARDNO, p_FS_GP_STEELTYPE, p_FS_GP_SPE, p_FN_GP_C, p_FN_GP_SI, p_FN_GP_MN,
            //p_FN_GP_S, p_FN_GP_P, p_FN_GP_NI ,p_FN_GP_CR,p_FN_GP_CU,p_FN_GP_V,p_FN_GP_MO,p_FN_GP_NB,p_FN_GP_CEQ,p_FS_GP_MEMO,p_FS_GP_JUDGER,p_FS_ADVISESPEC,p_FN_GP_LEN,p_FN_GP_TOTALCOUNT,p_FS_QYG,p_FS_ZZJY,p_FS_ORGUSE,p_FS_STEELSENDTYPE,p_FS_CountIsConfirm,p_FS_UNQUALIFIED};
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            
            //this.SaveWeedSteel(p_FS_CARDNO);
            //this.ClearWeedSteel();
            this.QueryCard();
            ultraGrid1ActiveNewRow(this.tbStoveNo.Text.Trim().ToString());
            //this.tbStoveNo.Text = "";
            //this.tbCardNo.Text = "";
        }

        private bool isCompleteStove(string strStoveNo)
        {
            bool result = true;
            string strSql = "select count(*) as count from it_fp_techcard where FS_GP_STOVENO='" + strStoveNo + "' and FS_GP_COMPLETEFLAG='1'";
            DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt16(dt.Rows[0]["count"].ToString()) > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 查询有效的、未计量的卡片
        /// </summary>
        private void QueryCard()
        {
            string str = "";
            if (cbAnalysisTime.Checked)
            {
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("查询结束时间不能小于开始时间！");
                    return;
                }

                string startTime = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");
                string endTime = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");
                str += " and A.FD_GP_JUDGEDATE between  to_date('" + startTime + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + endTime + "','yyyy-MM-dd HH24:mi:ss')";
            }

           
            //string str = " and A.FD_GP_JUDGEDATE between  to_date('" + startTime + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + endTime + "','yyyy-MM-dd HH24:mi:ss')";
            //if (tbQueryStoveNo.Text.Trim().Length > 0)
            //    str += " and A.FS_GP_STOVENO like '%" + tbQueryStoveNo.Text.Trim() + "%'";
            
            if (tbQueryStoveNo.Text.Trim().Length > 0)
            {
                str += " and A.FS_GP_STOVENO like '%" + tbQueryStoveNo.Text.Trim() + "%'";
            }

            string sql = "SELECT A.FS_CARDNO,A.FS_GP_STOVENO,A.FS_GP_STEELTYPE,A.FS_GP_SPE,to_char(A.FN_GP_LEN) AS FN_LENGTH,'1' as FN_GP_TOTALCOUNT,";
            sql += "A.FN_GP_C,A.FN_GP_SI,A.FN_GP_MN,A.FN_GP_S,A.FN_GP_P,A.FN_GP_AS,A.FN_GP_TI,A.FN_GP_SB,A.FN_GP_ALS,A.FS_GP_JUDGER,to_char(A.FD_GP_JUDGEDATE,'yyyy-MM-dd hh24:mi:ss') AS FD_GP_JUDGEDATE,";
            sql += "FS_ADVISESPEC,decode(FS_GP_FLOW,'SH000100','中宽带厂','SH000167','炼钢落地','中宽带厂') FS_GP_FLOW, ";
            sql += "decode(FS_TRANSTYPE,'1','热送','2','冷送','热送') FS_TRANSTYPE FROM IT_FP_TECHCARD A ";
            sql += "WHERE (a.FS_ISVALID = '0' or a.FS_ISVALID is null) and a.FS_GP_COMPLETEFLAG='0' and FS_CARDNO like 'BP10%'";
            sql += str + " order by a.FS_CARDNO desc";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            dataSet1.Tables[0].Clear();

            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;

            this.tbCardNo.Text = ugr.Cells["FS_CARDNO"].Text.Trim();
            this.tbStoveNo.Text = ugr.Cells["FS_GP_STOVENO"].Text.Trim();
            this.cbSteelType.Text = ugr.Cells["FS_GP_STEELTYPE"].Text.Trim();
            if (ugr.Cells["FS_GP_SPE"].Text.Trim().ToString() != "")
            {
                this.cbSpec.Text = ugr.Cells["FS_GP_SPE"].Text.Trim();
            }

            if (ugr.Cells["FN_LENGTH"].Text.Trim().ToString() != "")
            {
                this.tbLength.Text = ugr.Cells["FN_LENGTH"].Text.Trim();
            }

            this.tbCount.Text = ugr.Cells["FN_GP_TOTALCOUNT"].Text.Trim();

            this.tbC.Text = ugr.Cells["FN_GP_C"].Text.Trim();
            this.tbSi.Text = ugr.Cells["FN_GP_SI"].Text.Trim();
            this.tbMn.Text = ugr.Cells["FN_GP_MN"].Text.Trim();
            this.tbS.Text = ugr.Cells["FN_GP_S"].Text.Trim();
            this.tbP.Text = ugr.Cells["FN_GP_P"].Text.Trim();
            this.tbAs.Text = ugr.Cells["FN_GP_AS"].Text.Trim();
            this.tbTi.Text = ugr.Cells["FN_GP_TI"].Text.Trim();
            this.tbSb.Text = ugr.Cells["FN_GP_SB"].Text.Trim();
            this.tbAls.Text = ugr.Cells["FN_GP_ALS"].Text.Trim();
            this.tbLength.Text = ugr.Cells["FN_LENGTH"].Text.Trim();

            if (ugr.Cells["FS_ADVISESPEC"].Text.Trim().ToString() != "")
            {
                this.tbADVSPEC.Text = ugr.Cells["FS_ADVISESPEC"].Text.Trim();
            }

            this.LxComb.Text = ugr.Cells["FS_GP_FLOW"].Text.Trim().ToString();
            this.cbTransType.Text = ugr.Cells["FS_TRANSTYPE"].Text.Trim().ToString();
            
            if (LxComb.Text == "炼钢落地")
            {
                lbTransType.Visible = false;
                cbTransType.Visible = false;
            }
            else
            {
                lbTransType.Visible = true;
                cbTransType.Visible = true;
            }

            //this.checkBox1.Checked = Convert.ToBoolean(Convert.ToInt32(ugr.Cells["FS_GDFLAG"].Text.Trim()));
            //this.QueryWeedSteel(this.tbCardNo.Text);
        }

        private void RefreshBaseInfo()
        {
            //if (PointID.Length > 0)
            //{
            //    BaseInfo.GetGZData(this.cbSteelType, PointID);
            //    BaseInfo.GetGGData(this.cbSpec, PointID);
            //}
        }

        /// <summary>
        /// 基础数据处理，有则更新数量，无则插入
        /// </summary>
        private void BaseOperation()
        {
            //钢种基础操作
            if (cbSteelType.Text.Trim().Length > 0)
                BaseInfo.SetNonBaseData(PointID, this.cbSteelType.Text.Trim(), "SetGZData");

            //规格基础操作
            if (cbSpec.Text.Trim().Length > 0)
                BaseInfo.SetNonBaseData(PointID, this.cbSpec.Text.Trim(), "SetGGData");
        }

        /// <summary>
        /// 作废卡片
        /// </summary>
        private void DeleteFPTechCard()
        {
            try
            {
                if (this.ultraGrid1.Rows.Count == 0 && this.ultraGrid1.ActiveRow == null)
                    return;
                if (tbCardNo.Text.Trim() == "")
                    return;
                if (tbCardNo.Text.Trim() != this.ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Text.Trim())
                {
                    MessageBox.Show("请确定要删除的卡片是你选择的卡片！");
                    return;
                }
                string p_FS_CARDNO = tbCardNo.Text.Trim();
                //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.QueryCard();
                this.tbStoveNo.Text = "";
                this.tbCardNo.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
     
        //卡操作
        private void ultraToolbarsManager1_ToolClick_1(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        this.QueryCard();
                        break;
                    }
                case "Update":
                    {
                        this.Update_FP_Card();
                        break;
                    }
                case "Delete":
                    UltraGridRow ugr = this.ultraGrid1.ActiveRow;
                    if (ugr == null)
                    {
                        MessageBox.Show("请选择需要删除的流动卡信息！");
                        break;
                    }
                    if (DialogResult.Yes == MessageBox.Show("您确认要删除该条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        this.DoDelete_Card();
                        this.QueryCard();
                        break;
                    }
                    break;
                case "Get":
                    {
                        this.Get_JY_Data();
                        break;
                    }
                case "ToExcel":
                    {
                        Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, ultraGrid1);
                        //Constant.WaitingForm.Close();
                        break;
                    }
                case "AddProdiction":
                    {
                        this.DoAdd();
                        break;
                    }
                case "printCard":
                    {
                        string cardNo = ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Text.Trim();
                        //string printModel = cbCardPrintModel.Text;
                        PrintTechCard("Model", cardNo);
                        break;
                    }
                default:
                    break;
            }
        }
        //预报操作

        private void ultraToolbarsManager2_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid2.ActiveRow;
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        QueryJLCZData();
                        this.QueryYB();
                        break;
                    }

                case "Up":
                    {
                        this.DoUp(ugr);
                        break;
                    }
                case "Down":
                    {
                        this.DoDown(ugr);

                        break;
                    }
                case "ToTop":
                    {
                        this.DoUp_ToTop(ugr);

                        break;
                    }
                case "ToBotton":
                    {
                        this.DoDown_ToBotton(ugr);

                        break;
                    }
                case "ToExcel":
                    {
                        Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, ultraGrid2);
                        Constant.WaitingForm.Close();
                        break;
                    }
                case "printCard":
                    {
                        string cardNo = ultraGrid2.ActiveRow.Cells["FS_TECHCARDNO"].Text.Trim();
                        //string printModel = cbCardPrintModel.Text;
                        PrintTechCard("Model", cardNo);
                        break;
                    }

                case "Modify":
                    {
                        ModifyPlanFlag(ultraGrid2);
                        break;
                    }
                case "delete":
                    {
                        if (this.ultraGrid2.Rows.Count == 0)
                        {
                            return;
                        }

                        UltraGridRow ugr2 = ultraGrid2.ActiveRow;
                        if (ugr2 == null)
                        {
                            MessageBox.Show("请先选择要删除的预报");
                            return;
                        }

                        string strStoveNo = ugr2.Cells["FS_STOVENO"].Text;
                        string strTechCard = ugr2.Cells["FS_TECHCARDNO"].Text;

                        if (MessageBox.Show("删除炉号：" + strStoveNo + " 的预报？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        string strSql = "delete dt_bp_plan where fs_stoveno='" + strStoveNo + "'";
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                        ccp.MethodName = "ExcuteNonQuery";
                        ccp.ServerParams = new object[] { strSql };
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                      

                        MessageBox.Show("炉号:"+strStoveNo+" 预报已删除成功！");
                        break;
                    }
                default:
                    break;
            }
        }

        private void ModifyPlanFlag(UltraGrid ultragrid)
        {
            if (ultragrid.Rows.Count == 0)
            {
                return;
            }

            UltraGridRow ugr = ultragrid.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请先选择要强制完炉的记录");
                return;
            }

         
            string strStoveNo=ugr.Cells["FS_STOVENO"].Text;
            string strTechCard = ugr.Cells["FS_TECHCARDNO"].Text;

            if (MessageBox.Show("确定强制完炉炉号号：" + strStoveNo + " ？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            
            //if(!isBeginWeight(strStoveNo))
            //{
            //    MessageBox.Show("该炉号需至少开始一条才能强制完炉");
            //    return;
            //}

            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
            //ccp.MethodName = "completeStove";
            //ccp.ServerParams = new object[] { strTechCard };
            //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            //if (ccp.ReturnCode == 0)
            //{


                string strSql="update it_fp_techcard set FS_GP_COMPLETEFLAG='1' where FS_GP_STOVENO='"+strStoveNo+"'";
                
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { strSql };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                strSql = "update dt_bp_plan set FS_COMPLETEFLAG='1' where FS_STOVENO='" + strStoveNo + "'";

                ccp.ServerParams = new object[] { strSql };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                MessageBox.Show("该炉已强制完炉成功！");
            //}

            QueryYB();
        }

        //判断所选预报是否已开始计量
        private bool isBeginWeight(string strStoveNo)
        {
            bool result=true;
            string strSql="select count(*) as count from dt_steelweightdetailroll where fs_stoveno='"+strStoveNo+"'";
            DataTable dt=new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable=dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if(dt.Rows.Count>0)
            {
                if(Convert.ToInt16(dt.Rows[0]["count"].ToString())>0)
                {
                    result=true;
                }
                else
                {
                    result=false;
                }
            }
            else
            {
                result=false;
            }

            return result;
        }

        /// <summary>
        /// 删除预报，设置IT_FP_TECHCARD的FS_GP_USED = '0'
        /// </summary>
        private void DoDelete_YB(UltraGridRow ugr)
        {
            //UltraGridRow ugr = this.ultraGrid2.ActiveRow;
            //if (ugr == null)
            //{
            //    MessageBox.Show("请选择需要删除的预报！");
            //    return;
            //}
            string p_FS_STOVENO = ugr.Cells["FS_STOVENO"].Text.Trim();
            string cmflag = QuerySingleYB(p_FS_STOVENO);
            if (cmflag == "1" || cmflag == "2")
            {
                MessageBox.Show("该预报已经开始计量不能删除！");
                this.QueryYB();
                return;
            }
            string cardno = ugr.Cells["FS_TECHCARDNO"].Text.Trim();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "Delete_FP_PLAN";
            ccp.ServerParams = new object[] { cardno };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string strIP = m_BaseInfo.getIPAddress();
                string strMAC = m_BaseInfo.getMACAddress();
                string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                m_BaseInfo.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, "FS_GP_USED = 0", stoveno, "", "", "", "", "IT_FP_TECHCARD", "钢坯管理/辊道计量预报/预报删除按钮");

            }

        }
        public void PrintTechCard(string printModel, string cardNo)
        {
            if (cardNo.Length < 8)
            {
                MessageBox.Show("等待磅房处理！");
                return;
            }
            //1类型的固定报表测试
            String filename = "方柸按炉送钢流动卡";
            System.Collections.ArrayList headlist = new System.Collections.ArrayList();

            XMLReportTest test = new XMLReportTest(filename);
            System.Collections.ArrayList nodevaluelist = test.GetNodeValueListByXMLPath(@"Config/Head/Source/SqlLang");
            String selectsql = nodevaluelist[0].ToString() + " where FS_CARDNO='" + cardNo + "' "; //此只为测试
            DataTable testdatatable = new DataTable();

            CoreClientParam ccpquery = new CoreClientParam();
            ccpquery.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccpquery.MethodName = "ExcuteQuery";
            ccpquery.ServerParams = new object[] { selectsql };
            ccpquery.SourceDataTable = testdatatable;
            this.ExecuteQueryToDataTable(ccpquery, CoreInvokeType.Internal);

            if (testdatatable.Rows.Count < 1)
            {
                MessageBox.Show("没有数据！");
                return;
            }
            try
            {
                if (String.IsNullOrEmpty(printModel))
                {
                    test.CreateFixXMLReportFile(testdatatable);
                }
                else
                {
                    test.CreateFixXMLReportFile(printModel, testdatatable);
                }

                Thread.Sleep(3000);
                test.PrintReportXMLFile();

            }
            catch (Exception exp)
            {
                MessageBox.Show("找不到模板文件，请联系支持人员！" + exp.Message);
                return;
            }
        }

        private void DoDelete_Card()
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请选择需要删除的流动卡信息！");
                return;
            }
            string cardno = ugr.Cells["FS_CARDNO"].Text.Trim();
            string strSQL = "DELETE IT_FP_TECHCARD WHERE FS_CARDNO='" + cardno + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSQL };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            strSQL = "DELETE DT_BP_PLAN WHERE FS_TECHCARDNO='" + cardno + "'";
            ccp.ServerParams = new object[] { strSQL };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            MessageBox.Show("预报删除成功！");
        }

        /// <summary>
        /// 检查是否取样钢，不存在卡片返回0，是取样钢返回1，不是取样钢返回2,20110210彭海波增加
        /// </summary>
        private int IsStoveExisted(string str_stoveno)
        {
            int int_isqyg = -1;
            try
            {
                string strSql = "SELECT FS_QYG from IT_FP_TECHCARD WHERE FS_GP_STOVENO='" + str_stoveno.Trim() + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.StorageInfo";
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    string str_cardno = dt_temp.Rows[0]["FS_QYG"].ToString().Trim();
                    if (str_cardno.Equals("是"))
                    {
                        int_isqyg = 1;
                    }
                    else
                    {
                        int_isqyg = 2;
                    }
                }
                else
                {
                    int_isqyg = 0;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return int_isqyg;
            }
            return int_isqyg;
        }

        /// <summary>
        /// 检查是否已完炉，没找到卡片返回0，完炉返回1，没完炉返回2,20110210彭海波增加
        /// </summary>
        private int IsGpCompleted(string str_stoveno)
        {
            int int_isqyg = -1;
            try
            {
                string strSql = "SELECT fs_gp_completeflag from IT_FP_TECHCARD WHERE FS_GP_STOVENO='" + str_stoveno.Trim() + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.StorageInfo";
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    string str_cardno = dt_temp.Rows[0]["fs_gp_completeflag"].ToString().Trim();
                    if (str_cardno.Equals("1"))
                    {
                        int_isqyg = 1;
                    }
                    else
                    {
                        int_isqyg = 2;
                    }
                }
                else
                {
                    int_isqyg = 0;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return int_isqyg;
            }
            return int_isqyg;
        }

        //2011-02-01harpor修改
        private void Get_JY_Data()
        {
            if (m_insert_hxcf.Trim() == "2")
            {
                m_insert_hxcf = "1";
                //等待窗体
                Cursor oldCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                if (Constant.WaitingForm == null)
                {
                    Constant.WaitingForm = new WaitingForm();
                }
                Constant.WaitingForm.ShowToUser = true;
                Constant.WaitingForm.Show();
                Constant.WaitingForm.Update();

                m_flag = false;
                try
                {
                    if (string.IsNullOrEmpty(this.tbQueryStoveNo.Text.Trim()))
                    {
                        //暂时屏蔽
                        //CoreClientParam ccp = new CoreClientParam();
                        //ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
                        //ccp.MethodName = "FillQsData";
                        //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        //if (ccp.ReturnCode == 0)
                        //{
                        //    QueryQSData();
                        //}
                    }
                    else
                    {
                        if (tbQueryStoveNo.Text.ToString().Contains("-")||tbQueryStoveNo.Text.Length!=9)
                        {
                            MessageBox.Show("获取的炉号格式不正确");
                            this.Cursor = Cursors.Default;
                            Constant.WaitingForm.ShowToUser = false;
                            Constant.WaitingForm.Close();
                            return;
                        }

                        if (tbCount.Text.Trim() == string.Empty)
                        {
                            MessageBox.Show("块数不能为空！");
                            this.Cursor = Cursors.Default;
                            Constant.WaitingForm.ShowToUser = false;
                            Constant.WaitingForm.Close();
                            return;
                        }
                        else
                        {
                            if (!IsInt(tbCount.Text.ToString()))
                            {
                                MessageBox.Show("块数只能为数字！");
                                this.Cursor = Cursors.Default;
                                Constant.WaitingForm.ShowToUser = false;
                                Constant.WaitingForm.Close();
                                return;
                            }
                            else if(Convert.ToInt16(tbCount.Text.ToString())<=0)
                            {
                                MessageBox.Show("块数不能小于1！");
                                this.Cursor = Cursors.Default;
                                Constant.WaitingForm.ShowToUser = false;
                                Constant.WaitingForm.Close();
                                return;
                            }
                        }

                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
                        ccp.MethodName = "AddOneQSInfoBP";
                        string strStoveNo = this.tbQueryStoveNo.Text.Trim();
                        int intCount = int.Parse(tbCount.Text.ToString());
                        ccp.ServerParams = new object[] { strStoveNo, tbCount.Text.ToString() };
                        try
                        {
                            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                           
                        QueryCard();
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                    MessageBox.Show("QS系统信息获取失败！");
                }
                finally
                {
                    m_insert_hxcf = "2";
                }
                this.Cursor = Cursors.Default;
                Constant.WaitingForm.ShowToUser = false;
                Constant.WaitingForm.Close();
                m_flag = true;
            }
        }

        private string SubNumInStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            int num = 0;
            if (int.TryParse(str.Substring(str.Length - 1), out num))
            {
                str = str.Substring(0, str.Length - 1);
                str = SubNumInStr(str);
            }
            return str;
        }

        private void QueryQSData()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "WEIGHTPLAN_01.SELECT", new ArrayList() };
            dataSet1.Tables[0].Clear();
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        //string sqlcon = @"Data Source=10.7.1.51;Initial Catalog=LG2DB;User ID=sa;Password =sa;";
        //string sqlcon = @"Data Source=10.2.1.10;Initial Catalog=lg2db;User ID=jzjl;Password =jzjl;";
        //SqlConnection con = new SqlConnection(sqlcon);
        //con.Open();
        //string sql = "SELECT tid,lh_id,sample_id,gh,bc,sc_date,jy_time,czy_user,send_flag,hx_c,hx_Si,hx_Mn,hx_P,hx_S,hx_Cr,hx_Ni,hx_W,hx_V,hx_Mo,hx_Ti,hx_Cu,hx_Al,hx_B,hx_Co,hx_Sn,hx_Pb,hx_Sb,hx_Bi,hx_Nb,hx_Ca,hx_Mg,hx_Ceq,hx_N,hx_Zr,hx_Bs,hx_Ns,hx_Nt,hx_Fe,hx_as from lg2_hxsj_table where  (jl_flag =0 or jl_flag is null) and Len(sc_date)=8 and Len(jy_time)=5 order by tid asc";
        //SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
        //for (int i = 0; i < arrayList.Count; i++)
        //{
        //    string StoveNo = arrayList[i];

        //}

        //DataSet ds = new DataSet();

        ////adapter.Fill(ds, "lg2_hxsj_table");
        ////DataTable newTable = new DataTable();
        ////newTable = ds.Tables[0].Clone();
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    int i = 0;
        //    int j = 0;
        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        try
        //        {
        //            string p_FS_YLBB = this.cbYLBB.Text.Trim();
        //            string p_FN_GP_C = row["hx_C"].ToString().Trim();
        //            string p_FN_GP_SI = row["hx_Si"].ToString().Trim();
        //            string p_FN_GP_MN = row["hx_Mn"].ToString().Trim();
        //            string p_FN_GP_S = row["hx_S"].ToString().Trim();
        //            string p_FN_GP_P = row["hx_P"].ToString().Trim();
        //            string p_FN_GP_NI = row["hx_Ni"].ToString().Trim();
        //            string p_FN_GP_CR = row["hx_Cr"].ToString().Trim();
        //            string p_FN_GP_CU = row["hx_Cu"].ToString().Trim();
        //            string p_FN_GP_V = row["hx_V"].ToString().Trim();
        //            string p_FN_GP_MO = row["hx_Mo"].ToString().Trim();
        //            string p_FN_GP_NB = row["hx_Nb"].ToString().Trim();
        //            string p_FN_GP_AS = row["hx_as"].ToString().Trim();
        //            string p_FN_GP_CEQ = row["hx_Ceq"].ToString().Trim();
        //            string p_FS_GP_JUDGER = tbPerson.Text.Trim();
        //            string p_FD_GP_JUDGEDATE = System.DateTime.Now.Year.ToString().Substring(0, 2) + row["sc_date"].ToString().Trim() + " " + row["jy_time"].ToString().Trim();
        //            string date = row["sc_date"].ToString().Trim();
        //            string time = row["jy_time"].ToString().Trim();
        //            string p_FS_GP_STEELTYPE = row["gh"].ToString().Trim();
        //            string p_FS_GP_STOVENO = row["lh_id"].ToString().Trim();
        //            string p_FS_CARDNO = "FP10" + DateTime.Now.ToString("yyyyMMddHHmmss");

        //            int int_isqyg = IsStoveExisted(p_FS_GP_STOVENO);
        //            if (int_isqyg == 0)
        //            {//不存在卡片，则新增卡片
        //                string strTmpField, strTmpValue;
        //                strTmpField = "FN_GP_C,FN_GP_SI,FN_GP_MN,FN_GP_S,FN_GP_P,FN_GP_NI,FN_GP_CR,FN_GP_CU,FN_GP_V,"
        //                    + "FN_GP_MO,FN_GP_NB,FN_GP_AS,FN_GP_CEQ,FS_GP_JUDGER,FD_GP_JUDGEDATE,FS_GP_STEELTYPE,FS_GP_STOVENO,"
        //                    + "FS_CARDNO,FS_YLBB";
        //                strTmpValue = "'" + p_FN_GP_C + "','" + p_FN_GP_SI + "','" + p_FN_GP_MN + "','" + p_FN_GP_S + "','"
        //                    + p_FN_GP_P + "','" + p_FN_GP_NI + "','" + p_FN_GP_CR + "','" + p_FN_GP_CU + "','" + p_FN_GP_V + "','"
        //                    + p_FN_GP_MO + "','" + p_FN_GP_NB + "','" + p_FN_GP_AS + "','" + p_FN_GP_CEQ + "','" + p_FS_GP_JUDGER + "',TO_DATE('"
        //                    + p_FD_GP_JUDGEDATE + "','yyyy-MM-dd hh24:mi'),'" + p_FS_GP_STEELTYPE + "','" + p_FS_GP_STOVENO + "','"
        //                    + p_FS_CARDNO + "','" + p_FS_YLBB + "'";

        //                CoreClientParam ccp = new CoreClientParam();

        //                ccp.ServerName = "ygjzjl.statictrackweight.StaticWeight";
        //                ccp.MethodName = "insertDataInfo";
        //                ccp.ServerParams = new object[] { "IT_FP_TECHCARD", strTmpField, strTmpValue };

        //                //ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
        //                //ccp.MethodName = "Insert_ZJ_Data";
        //                //ccp.IfShowErrMsg = false;
        //                //ccp.ServerParams = new object[] { p_FN_GP_C, p_FN_GP_SI, p_FN_GP_MN, p_FN_GP_S, p_FN_GP_P, p_FN_GP_NI, p_FN_GP_CR, p_FN_GP_CU, p_FN_GP_V, p_FN_GP_MO, p_FN_GP_NB, p_FN_GP_CEQ, p_FS_GP_JUDGER, p_FD_GP_JUDGEDATE, p_FS_GP_STEELTYPE, p_FS_GP_STOVENO, p_FS_CARDNO, p_FS_YLBB };
        //                //ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";

        //                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        //                if (ccp.ReturnCode == 0)
        //                {
        //                    i++;
        //                    SqlCommand sqlup = con.CreateCommand();
        //                    string updstr = " update  lg2_hxsj_table set  jl_flag = 1 where tid = '" + row["tid"].ToString().Trim() + "'";

        //                    sqlup.CommandText = updstr;
        //                    sqlup.ExecuteNonQuery();
        //                    sqlup.Dispose();
        //                }
        //            }
        //            else if (int_isqyg == 1)
        //            {//是取样钢，不覆盖
        //                SqlCommand sqlup = con.CreateCommand();
        //                string updstr = " update  lg2_hxsj_table set  jl_flag = 1 where tid = '" + row["tid"].ToString().Trim() + "'";

        //                sqlup.CommandText = updstr;
        //                sqlup.ExecuteNonQuery();
        //                sqlup.Dispose();
        //            }
        //            else if (int_isqyg == 2)
        //            {//不是取样钢，则覆盖
        //                string updatesql = "update IT_FP_TECHCARD  set FN_GP_C='" + p_FN_GP_C + "',FN_GP_SI='" + p_FN_GP_SI + "',FN_GP_MN='" + p_FN_GP_MN
        //                    + "',FN_GP_S='" + p_FN_GP_S + "',FN_GP_P='" + p_FN_GP_P + "',FN_GP_NI='" + p_FN_GP_NI + "',FN_GP_CR='" + p_FN_GP_CR
        //                    + "',FN_GP_CU='" + p_FN_GP_CU + "',FN_GP_V='" + p_FN_GP_V + "',FN_GP_MO='" + p_FN_GP_MO + "',FN_GP_NB='" + p_FN_GP_NB
        //                    + "',FN_GP_AS='" + p_FN_GP_AS + "',FN_GP_CEQ='" + p_FN_GP_CEQ + "',FS_GP_JUDGER='" + p_FS_GP_JUDGER + "',FS_GP_STEELTYPE='" + p_FS_GP_STEELTYPE
        //                    + "',FS_YLBB='" + p_FS_YLBB + "',FD_GP_JUDGEDATE=TO_DATE('" + p_FD_GP_JUDGEDATE + "','yyyy-MM-dd hh24:mi') where FS_GP_STOVENO='" + p_FS_GP_STOVENO + "'";
        //                CoreClientParam ccp = new CoreClientParam();
        //                ccp.ServerName = "ygjzjl.car.StorageInfo";
        //                ccp.MethodName = "updateByClientSql";
        //                ccp.ServerParams = new object[] { updatesql };
        //                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        //                if (ccp.ReturnCode == 0) //更新成功
        //                {
        //                    j++;
        //                    SqlCommand sqlup = con.CreateCommand();
        //                    string updstr = " update  lg2_hxsj_table set  jl_flag = 1 where tid = '" + row["tid"].ToString().Trim() + "'";

        //                    sqlup.CommandText = updstr;
        //                    sqlup.ExecuteNonQuery();
        //                    sqlup.Dispose();
        //                    }
        //                }

        //            }
        //catch (Exception ex)
        //{
        //    MessageBox.Show("成功获取" + i + "条卡片数据,更新" + j + "条卡片数据,获取数据" + row[0].ToString() + "时出错!" + ex.ToString());

        //    continue;
        //}

        private void DoAdd()
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (this.tbStoveNo.Text.Trim() == "")
            {
                MessageBox.Show("请选择工艺流动卡信息或手工录入炉号！");
                return;
            }

            if (DoCheck())
            {
                return;
            }

            string cmflag = QuerySingleYB(this.tbStoveNo.Text.Trim());
            if (cmflag == "1")
            {
                MessageBox.Show("该预报已经计量完成不能修改！");
                this.QueryYB();
                return;
            }

            string p_FS_SPEC = this.cbSpec.Text.Trim();
            string p_FS_STEELTYPE = this.cbSteelType.Text.Trim();
            string p_FN_LENGTH = tbLength.Text.Trim();
            string p_FS_GP_JUDGER = tbPerson.Text.Trim();
            string p_FS_RECEIVER = this.LxComb.SelectedValue.ToString();
            string p_FS_GDFLAG = "1";
            string p_FS_ADVISESPEC = this.tbADVSPEC.Text.Trim();
            string p_FS_MATERIAL = "板坯";
            string p_FS_TRANSTYPE = this.cbTransType.SelectedValue.ToString();
            //20120403新增，新增预报提示
            if (MessageBox.Show("当前去向选择:" + this.LxComb.Text + ",运输方式:" + this.cbTransType.Text + ",确定？", "红钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            string p_FS_STOVENO = this.tbStoveNo.Text.Trim();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.WeightPlan_BP";
            ccp.MethodName = "ADD_BP_PLAN";

            if (p_FS_STOVENO.Length == 9)
            {
                int p_FN_TOTALCOUNT = 0;
                try
                {
                    p_FN_TOTALCOUNT = Convert.ToInt16(tbCount.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("条数只能为数字");
                    return;
                }

                for (int i = 1; i <= p_FN_TOTALCOUNT; i++)
                {
                    p_FS_STOVENO = tbStoveNo.Text + "-" + i.ToString();
                    ccp.ServerParams = new object[] { p_FS_STOVENO, p_FS_SPEC, p_FS_STEELTYPE, p_FN_LENGTH, p_FS_GP_JUDGER, p_FS_MATERIAL, p_FS_ADVISESPEC, p_FS_RECEIVER, p_FS_TRANSTYPE };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
            }
            else
            {
                ccp.ServerParams = new object[] { p_FS_STOVENO, p_FS_SPEC, p_FS_STEELTYPE, p_FN_LENGTH, p_FS_GP_JUDGER, p_FS_MATERIAL, p_FS_ADVISESPEC, p_FS_RECEIVER, p_FS_TRANSTYPE };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }

            string strSQL = "update dt_bp_plan set FN_ISRETURNBILLET='" + this.cbBilletReturn.SelectedValue.ToString() + "' where FS_STOVENO='" + p_FS_STOVENO + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSQL };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
    
            this.QueryCard();
            this.QueryYB();
            ultraGrid1ActiveNewRow(this.tbStoveNo.Text.Trim().ToString());
        }

        /// <summary>
        /// 查询正在计量和未计量的预报
        /// </summary>
        private void QueryYB()
        {
            if (m_query == "2")
            {//m_query = "1"使得正在查询时，线程不再查询
                m_query = "1";
                if (dateTimePicker3.Value > dateTimePicker4.Value)
                {
                    MessageBox.Show("查询结束时间不能小于开始时间！");
                    dateTimePicker3.Value = dateTimePicker4.Value;
                    return;
                }
                string startTime = dateTimePicker3.Value.ToString("yyyy-MM-dd 00:00:00");
                string endTime = dateTimePicker4.Value.ToString("yyyy-MM-dd 23:59:59");
                string str = "";

                str = " and A.FD_PLANTIME between  to_date('" + startTime + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + endTime + "','yyyy-MM-dd HH24:mi:ss')";

                if (txtStoveGD1.Text.Trim() != string.Empty)
                {
                    str += " and A.FS_STOVENO like '%" + txtStoveGD1.Text.Trim().Replace("'","''") + "%'";
                }
                string strSqlTemp = "SELECT A.FS_ORDER,A.FS_TECHCARDNO,A.FS_STOVENO,A.FS_STEELTYPE,A.FS_SPEC,A.FN_LENGTH," +
                    "A.FN_COUNT,A.FS_ORDERNO," +
                    "to_char(A.FD_PLANTIME,'yyyy-MM-dd hh24:mi:ss') AS FD_PLANTIME ,A.FS_COMPLETEFLAG,A.FS_PERSON, " +
                    "(SELECT COUNT(D.FS_STOVENO) FROM dt_boardweightmain D WHERE D.FS_STOVENO = A.FS_STOVENO) WEIGHEDCOUNT," +
                    "decode(FN_ISRETURNBILLET,'0','否','1','是',FN_ISRETURNBILLET) FN_ISRETURNBILLET " +
                    "FROM DT_BP_PLAN A WHERE A.FS_COMPLETEFLAG <> '1' ";
                string strSql = strSqlTemp + " order by a.FS_ORDER";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };

                dataSet1.Tables[1].Clear();

                ccp.SourceDataTable = dataSet1.Tables[1];

                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dataSet1.Tables[1].Rows.Count == 0)
                {
                    _flashFlag1 = false;
                }

                SelectWeightingPlan();

                RefreshGroupBox();
                Constant.RefreshAndAutoSize(ultraGrid2);

                m_query = "2";
            }
        }

        private void RefreshGroupBox()
        {
            if (ultraGrid2.Rows.Count > 0)
            {
                string str = "select a.FN_TOTALWEIGHT,a.FN_BILLETCOUNT,a.FS_STOVENO,b.fn_count as FN_TOTALBILLET from DT_STEELWEIGHTMAIN a,DT_FP_PLAN b where a.fs_stoveno = b.fs_stoveno and a.FS_COMPLETEFLAG <>'0' and a.fs_stoveno = '" + this.ultraGrid2.Rows[0].Cells["FS_STOVENO"].Text.Trim() + "' order by a.FD_STARTTIME desc";
                CoreClientParam ccp = new CoreClientParam();
                DataTable dt1 = new DataTable();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { str };
                ccp.SourceDataTable = dt1;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                ultraGroupBox4.Text = string.Empty;

                if (dt1.Rows.Count > 0)
                {
                    this.ultraGroupBox4.Text = "当前计量  炉号：" + dt1.Rows[0]["FS_STOVENO"].ToString() + "： " + dt1.Rows[0]["FN_BILLETCOUNT"].ToString() + "/" + dt1.Rows[0]["FN_TOTALBILLET"].ToString() + "块";
                    ultraGroupBox4.Refresh();
                }
                else
                {
                    this.ultraGrid2.Text = string.Empty;
                }
            }
        }

        private void WeightPlan_BP_Load(object sender, EventArgs e)
        {
            tbPerson.Text = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            BaseInfo = new GetBaseInfo();
            m_BaseInfo = new BaseInfo();
            BaseInfo.ob = this.ob;
            m_BaseInfo.ob = this.ob;
            RefreshBaseInfo();
            dt = new DataTable();
            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadSteelType();  //下载磅房对应钢种信息到内存表
            this.DownLoadSepc(); //下载磅房对应规格信息到内存表
            this.cbx_SteelSendType.SelectedIndex = 0;

            BandPointSteelType(PointID);
            BandPointSpec(PointID);
            //BindTransType();
            m_query = "2";
            m_insert_hxcf = "2";
            m_flag = true;
            m_thread = new Thread(new ThreadStart(QueryTread));
            m_thread.Start();
            m_hRunning = true;
            m_AutoRunning = false;
            //BindTranType();
            //BindTranType3();
            //BindLXTyep1();
            //BindLXTyep2();
            cbAnalysisTime.Checked = true;

            //timer1.Start();
            BindTranType();
            BindLXType();
            BindBilletReturn();
        }

        private void DoUp(UltraGridRow ugr)
        {
            if (ugr == null)
            {
                MessageBox.Show("请选择需要上移的预报！");
                return;
            }
            string p_FS_STOVENO = ugr.Cells["FS_STOVENO"].Text.Trim();
            int p_FS_ORDER = Convert.ToInt32(ugr.Cells["FS_ORDER"].Text.Trim());
            string cmflag = QuerySingleYB(p_FS_STOVENO);
            if (cmflag == "1" || cmflag == "2")
            {
                MessageBox.Show("该预报已经开始计量不能移动！");
                this.QueryYB();
                return;
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.WeightPlan_BP";
            ccp.MethodName = "DoUp_BP_Plan";
            ccp.ServerParams = new object[] { p_FS_STOVENO, p_FS_ORDER };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.QueryYB();
            ultraGrid2ActiveNewRow(p_FS_STOVENO);
        }

        private void DoDown(UltraGridRow ugr)
        {
            if (ugr == null)
            {
                MessageBox.Show("请选择需要下移的预报！");
                return;
            }
            string p_FS_STOVENO = ugr.Cells["FS_STOVENO"].Text.Trim();
            int p_FS_ORDER = Convert.ToInt32(ugr.Cells["FS_ORDER"].Text.Trim());
            string cmflag = QuerySingleYB(p_FS_STOVENO);
            if (cmflag == "1" || cmflag == "2")
            {
                MessageBox.Show("该预报已经开始计量不能移动！");
                this.QueryYB();
                return;
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.WeightPlan_BP";
            ccp.MethodName = "DoDown_BP_Plan";
            ccp.ServerParams = new object[] { p_FS_STOVENO, p_FS_ORDER };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.QueryYB();
            ultraGrid2ActiveNewRow(p_FS_STOVENO);
        }

        private void DoUp_ToTop(UltraGridRow ugr)
        {
            if (ugr == null)
            {
                MessageBox.Show("请选择需要置顶的预报！");
                return;
            }
            string p_FS_STOVENO = ugr.Cells["FS_STOVENO"].Text.Trim();
            int p_FS_ORDER = Convert.ToInt32(ugr.Cells["FS_ORDER"].Text.Trim());
            string cmflag = QuerySingleYB(p_FS_STOVENO);
            if (cmflag == "1" || cmflag == "2")
            {
                MessageBox.Show("该预报已经开始计量不能移动！");
                this.QueryYB();
                return;
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.WeightPlan_BP";
            ccp.MethodName = "UpToTop_BP_Plan";
            ccp.ServerParams = new object[] { p_FS_STOVENO, p_FS_ORDER };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.QueryYB();
            ultraGrid2ActiveNewRow(p_FS_STOVENO);
        }

        private void DoDown_ToBotton(UltraGridRow ugr)
        {
            if (ugr == null)
            {
                MessageBox.Show("请选择需要置底的预报！");
                return;
            }
            string p_FS_STOVENO = ugr.Cells["FS_STOVENO"].Text.Trim();
            int p_FS_ORDER = Convert.ToInt32(ugr.Cells["FS_ORDER"].Text.Trim());
            string cmflag = QuerySingleYB(p_FS_STOVENO);
            if (cmflag == "1" || cmflag == "2")
            {
                MessageBox.Show("该预报已经开始计量不能移动！");
                this.QueryYB();
                return;
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.WeightPlan_BP";
            ccp.MethodName = "DownToBotton_BP_Plan";
            ccp.ServerParams = new object[] { p_FS_STOVENO, p_FS_ORDER };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.QueryYB();
            ultraGrid2ActiveNewRow(p_FS_STOVENO);
        }

        /*
        private void Update_FP_Plan()
        {
            UltraGridRow ugr = this.ultraGrid2.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请选择需要修改的预报记录！");
                return;
            }
            string p_FS_STOVENO = ugr.Cells["FS_STOVENO"].Text.Trim();
            string cmflag = QuerySingleYB(p_FS_STOVENO);
            if (cmflag == "1")
            {
                MessageBox.Show("该预报已经计量完成不能修改！");
                this.QueryYB();
                return;
            }
            QueryJLCZData();
            if (cmflag == "2" && Convert.ToInt32(tbCount.Text.Trim()) <= Convert.ToInt32(dataTable3.Rows[0]["FN_BILLETCOUNT"].ToString()))
            {
                MessageBox.Show("修改条数只能大于已计量条数！");
                this.QueryYB();
                return;
            }
            if (DoCheck())
                return;

            BaseOperation();
            string p_FS_CARDNO = tbCardNo.Text.Trim();
            string p_FS_GP_STEELTYPE = cbSteelType.Text.Trim();
            string p_FS_GP_SPE = cbSpec.Text.Trim();
            string p_FN_GP_LEN = tbLength.Text.Trim();
            int p_FN_GP_TOTALCOUNT = Convert.ToInt32(tbCount.Text.Trim());
            string p_FS_GP_JUDGER = tbPerson.Text.Trim();
            string p_FS_GDFLAG = Convert.ToInt32(checkBox1.Checked).ToString();
            string p_FS_GP_MEMO = tbMemo.Text.Trim();
            string p_FS_TRANSTYPE = cbTransType.SelectedValue.ToString();
            string p_FS_MATERIAL =cbMaterial.SelectedValue.ToString().Trim();
            string p_FS_ITEMNO = this.txtDDXMH.Text.Trim();
            string p_FS_ORDERNO = this.txtDDH.Text.Trim();
            string p_FS_ADVISESPEC = this.tbADVSPEC.Text.Trim();

            string strSql = " select FN_GP_TOTALCOUNT,FS_GP_STEELTYPE,FN_GP_LEN from IT_FP_TECHCARD where FS_CARDNO = '" + p_FS_CARDNO + "'";
            DataTable dts = new DataTable();
            dts = m_BaseInfo.QueryData(strSql);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "Update_FP_PLAN";
            ccp.ServerParams = new object[] { p_FS_CARDNO, p_FS_GP_STEELTYPE, p_FS_GP_SPE, p_FN_GP_LEN, p_FN_GP_TOTALCOUNT, p_FS_GP_JUDGER, p_FS_GDFLAG, p_FS_GP_MEMO, p_FS_MATERIAL, p_FS_ITEMNO, p_FS_ORDERNO, p_FS_ADVISESPEC };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                string m_Memo = "";
                string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string strIP = m_BaseInfo.getIPAddress();
                string strMAC = m_BaseInfo.getMACAddress();
                string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                if (dts.Rows.Count > 0)
                {
                    if (p_FN_GP_TOTALCOUNT.ToString().Trim() != dts.Rows[0]["FN_GP_TOTALCOUNT"].ToString().Trim())
                        m_Memo = m_Memo + " FN_GP_TOTALCOUNT(钢坯条数) = " + dts.Rows[0]["FN_GP_TOTALCOUNT"].ToString().Trim() + " -> " + p_FN_GP_TOTALCOUNT + "";

                    if (p_FS_GP_STEELTYPE != dts.Rows[0]["FS_GP_STEELTYPE"].ToString().Trim())
                        m_Memo = m_Memo + " FS_GP_STEELTYPE(钢种) = " + dts.Rows[0]["FS_GP_STEELTYPE"].ToString().Trim() + " ->" + p_FS_GP_STEELTYPE;

                    if (p_FN_GP_LEN.ToString() != dts.Rows[0]["FN_GP_LEN"].ToString().Trim())
                        m_Memo = m_Memo + " FN_GP_LEN(定尺) = " + dts.Rows[0]["FN_GP_LEN"].ToString().Trim() + " -> " + p_FN_GP_LEN;

                    if (m_Memo != "")
                        m_BaseInfo.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, p_FS_CARDNO, "", "", "", "", "IT_FP_TECHCARD", "钢坯管理/辊道计量预报/预报修改按钮");

                }

            }
            this.QueryYB();
            ultraGrid2ActiveNewRow(this.tbStoveNo.Text.Trim().ToString());
            this.tbStoveNo.Text = "";
            this.tbCardNo.Text = "";
        }
         * */

        private void ultraGrid2ActiveNewRow(string p_FS_STOVENO)
        {
            if (ultraGrid2.Rows.Count > 0)
            {
                for (int i = 0; i < ultraGrid2.Rows.Count; i++)
                {
                    if (ultraGrid2.Rows[i].Cells["FS_STOVENO"].Text == p_FS_STOVENO)
                    {
                        ultraGrid2.Rows[i].Activated = true;
                        ultraGrid2.Rows[i].Selected = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 激活指定炉号所在的工艺流动卡行
        /// </summary>
        private void ultraGrid1ActiveNewRow(string p_FS_STOVENO)
        {
            if (ultraGrid1.Rows.Count > 0)
            {
                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    if (ultraGrid1.Rows[i].Cells["FS_GP_STOVENO"].Text == p_FS_STOVENO)
                    {
                        ultraGrid1.Rows[i].Activated = true;
                        ultraGrid1.Rows[i].Selected = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 将正在计量的辊道一号行变红
        /// </summary>
        private void SelectWeightingPlan()
        {
            if (ultraGrid2.Rows.Count > 0)
            {
                int count1 = 0, count2 = 0,count  = 0;
                for (int i = 0; i < ultraGrid2.Rows.Count; i++)
                {
                    int.TryParse(ultraGrid2.Rows[i].Cells["FN_COUNT"].Text.Trim(), out count2);
                    if (ultraGrid2.Rows[i].Cells["FS_COMPLETEFLAG"].Text.Trim() == "2")
                    {
                        ultraGrid2.Rows[i].Appearance.ForeColor = Color.Red;
                        int.TryParse(ultraGrid2.Rows[i].Cells["WEIGHEDCOUNT"].Text.Trim(), out count1);
                        
                        count += count2 - count1;
                    }
                    else
                    {
                        ultraGrid2.Rows[i].Appearance.ForeColor = Color.Black;
                        count += count2;

                        if (ultraGrid2.Rows[i].Cells["FN_ISRETURNBILLET"].Text.Trim() == "是")
                        {
                            ultraGrid2.Rows[i].Appearance.ForeColor = Color.YellowGreen;
                        }
                    }
                }

                if (count > 10)
                {
                    _flashFlag1 = false;
                }
                else
                {
                    _flashFlag1 = true;
                }
            }
        }

        private void QueryTread()
        {
            while (m_flag)
            {
                QueryJLCZData();

                m_QueryYBThreadDelegate = new QueryYBThreadDelegate(QueryYB);
                Invoke(m_QueryYBThreadDelegate);

                System.Threading.Thread.Sleep(20000);
            }

        }

        private void WeightPlan_BP_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_flag = false;
            System.Threading.Thread.Sleep(2000);
            m_thread.Abort();
            m_hRunning = false;
            m_AutoRunning = false;
        }

        private string QuerySingleYB(string p_FS_STOVENO)//移动前预报查询
        {
            string strSql = "SELECT FS_COMPLETEFLAG from dt_bp_plan where fs_stoveno = '" + p_FS_STOVENO + "'"; 
            DataTable tbYB = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = tbYB;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (tbYB.Rows.Count > 0)
            {
                return tbYB.Rows[0]["FS_COMPLETEFLAG"].ToString().Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// DT_STEELWEIGHTMAIN.FS_COMPLETEFLAG <>'0',应该改为FS_COMPLETEFLAG='2',表示正在计量，此处能运行是因为预报查询时限制了第一行的预报
        /// </summary>
        private void QueryJLCZData()
        {
            if (m_query == "2")
            {//m_query = "1"使得正在查询时，线程不再查询
                m_query = "1";
                if (this.ultraGrid2.Rows.Count > 0)
                {
                    string str = "select a.FN_TOTALWEIGHT,a.FN_BILLETCOUNT,a.FS_STOVENO,b.fn_count as FN_TOTALBILLET from DT_STEELWEIGHTMAIN a,DT_BP_PLAN b where a.fs_stoveno = b.fs_stoveno and a.FS_COMPLETEFLAG <>'0' and a.fs_stoveno = '" + this.ultraGrid2.Rows[0].Cells["FS_STOVENO"].Text.Trim() + "' order by a.FD_STARTTIME desc";
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteQuery";
                    ccp.ServerParams = new object[] { str };
                    dataTable3.Clear();
                    ccp.SourceDataTable = dataTable3;
                    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                    if (dataTable3.Rows.Count > 0)
                    {
                    }
                }
                m_query = "2";
            }
        }

        #region 7月4日
        //构建内存表格式
        private void BuildMyTable()
        {
            DataColumn dc;
            //磅房对应钢种表
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_SteelTypeTable.Columns.Add(dc);
            dc = new DataColumn("FS_STEELTYPE".ToUpper()); m_SteelTypeTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_SteelTypeTable.Columns.Add(dc);

            //磅房对应规格表
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_SpecTable.Columns.Add(dc);
            dc = new DataColumn("FS_SPEC".ToUpper()); m_SpecTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_SpecTable.Columns.Add(dc);

        }
        
        //下载磅房对应钢种信息
        private void DownLoadSteelType()
        {
            string strSql = "SELECT A.FS_POINTNO,A.FS_STEELTYPE,A.FN_TIMES FROM BT_POINTSTEELTYPE A,BT_POINT B WHERE A.FS_POINTNO = B.FS_POINTCODE AND B.FS_POINTTYPE = 'BP' ORDER BY FN_TIMES DESC";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SteelTypeTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }
        //下载磅房对应规格信息  
        private void DownLoadSepc()
        {
            string strSql = "SELECT A.FS_POINTNO,A.FS_SPEC,A.FN_TIMES FROM BT_POINTSPEC A,BT_POINT B WHERE A.FS_POINTNO = B.FS_POINTCODE AND B.FS_POINTTYPE = 'BP' ORDER BY FN_TIMES DESC";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SpecTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }
       
        //按磅房筛选钢种
        private void BandPointSteelType(string PointID)
        {
            DataRow[] drs = null;

            this.tempSteelType = this.m_SteelTypeTable.Clone();

            drs = this.m_SteelTypeTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempSteelType.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempSteelType.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempSteelType.NewRow();
            this.tempSteelType.Rows.InsertAt(drz, 0);
            this.cbSteelType.DataSource = this.tempSteelType;
            cbSteelType.DisplayMember = "FS_STEELTYPE";
            cbSteelType.ValueMember = "FS_STEELTYPE";
        }

        //按磅房筛选规格
        private void BandPointSpec(string PointID)
        {
            DataRow[] drs = null;

            this.tempSpec = this.m_SpecTable.Clone();

            drs = this.m_SpecTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempSpec.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempSpec.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempSpec.NewRow();
            this.tempSpec.Rows.InsertAt(drz, 0);
            this.cbSpec.DataSource = this.tempSpec;
            cbSpec.DisplayMember = "FS_SPEC";
            cbSpec.ValueMember = "FS_SPEC";

        }
        private void InitConfig()
        {
            cbSteelType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbSteelType.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbSpec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbSpec.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        #endregion

        /// <summary>
        /// 查询炉号对应的工艺流动卡号，未查到则将工艺流动卡文本框赋值为空
        /// </summary>
        private void QueryOldCardNo()
        {
            string p_FS_STOVENO = this.tbStoveNo.Text.Trim();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "QueryOldCardNo";
            ccp.IfShowErrMsg = false;
            ccp.ServerParams = new object[] { p_FS_STOVENO };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            string result = "";
            if (ccp.ReturnCode != -1)
            {
                result = ccp.ReturnObject.ToString();
                //如果存储过程未查到炉号，返回'none'，卡号赋值为空
                if (result == "none")
                    this.tbCardNo.Text = "";
            }
        }

        private string CheckRollWeightRecode(string p_stoveno)
        {
            string strSql = " select count(*) from dt_steelweightdetailroll where fs_stoveno = '" + p_stoveno + "'";
            DataTable dts = new DataTable();
            dts = m_BaseInfo.QueryData(strSql);
            return dts.Rows[0][0].ToString();

        }

        private void BindLXTyep1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            // dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows.Add("SH000098", "棒材厂");
            dt.Rows.Add("SH000100", "高线厂");
            LxComb.DataSource = dt;
            LxComb.ValueMember = "Value";
            LxComb.DisplayMember = "Text";
        }

        private void BindLXTyep2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            // dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows.Add("SH000098", "棒材厂");
            dt.Rows.Add("SH000167", "炼钢落地");
            dt.Rows.Add("4", "外卖");
            LxComb.DataSource = dt;
            LxComb.ValueMember = "Value";
            LxComb.DisplayMember = "Text";
        }

        private void ultraTabControl1_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            if (ultraTabControl1.SelectedTab.Index == 0)
            {
                //this.ultraGroupBox1.Visible = true;
                this.ultraPanel1.Height = 192;
                //this.ultraGroupBox2.Dock=DockStyle.Fill;
                //this.ultraGroupBox2.Visible = true;
            }
            else if (ultraTabControl1.SelectedTab.Index == 1)
            {
                this.ultraPanel1.Height = 1;
                //this.ultraGroupBox1.Visible = false;
                //this.ultraPanel1.Dock = DockStyle.Fill;
                //this.ultraGroupBox2.Dock=DockStyle.None;
                //this.ultraGroupBox2.Visible = false;
            }
            else
            {
                this.ultraPanel1.Height = 1;
            }
        }

        private void cbAnalysisTime_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAnalysisTime.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void BindTranType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            // dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows.Add("1", "热送");
            dt.Rows.Add("2", "冷送");
            this.cbTransType.DataSource = dt;
            cbTransType.ValueMember = "Value";
            cbTransType.DisplayMember = "Text";
        }

        private void BindBilletReturn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            // dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows.Add("0", "否");
            dt.Rows.Add("1", "是");
            this.cbBilletReturn.DataSource = dt;
            this.cbBilletReturn.ValueMember = "Value";
            cbBilletReturn.DisplayMember = "Text";
        }

        private void BindLXType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            dt.Rows.Add("SH000100", "中宽带厂");
            dt.Rows.Add("SH000167", "炼钢落地");
            LxComb.DataSource = dt;
            LxComb.ValueMember = "Value";
            LxComb.DisplayMember = "Text";
        }

        private void LxComb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LxComb.SelectedValue.ToString() == "SH000167")
            {
                cbTransType.SelectedValue = "2";
                lbTransType.Visible = false;
                cbTransType.Visible = false;
            }
            else
            {
                cbTransType.SelectedValue = "1";
                lbTransType.Visible = true;
                cbTransType.Visible = true;
            }
        }

        private DataSet CreateDataSet(DataTable dt)
        {
            DataSet ds = new DataSet();

            if (dt != null)
            {
                ds.Tables.Add(dt);
            }

            return ds;
        }

        private ArrayList ProcReturnDS(string ServerName, string MethodName, object[] obj, out string err)
        {
            try
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = ServerName;//服务端类名
                ccp.MethodName = MethodName;//上面类中的指定方法      
                ccp.ServerParams = obj;
                ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);//执行                
                err = "";
                return (ArrayList)ccp.ReturnObject;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }
    }
}

