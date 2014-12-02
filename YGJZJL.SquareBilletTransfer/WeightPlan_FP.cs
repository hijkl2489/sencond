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
    public partial class WeightPlan_FP : FrmBase
    {
        private Thread m_thread;
        private bool m_flag;
        public string PointID = "K16";
        string stoveno = "";
        string count = "";
        string m_query = "0";//预报查询刷新占用标志 1 为占用 2 空闲
        GetBaseInfo BaseInfo; //基础信息操作
        BaseInfo m_BaseInfo;
        private DataTable dt;
        private DataTable m_SteelTypeTable = new DataTable();//钢种数据表
        private DataTable tempSteelType = new DataTable();
        private string valueWL = "";
        //private DataTable m_SpecTable = new DataTable();//规格数据表
        //private DataTable tempSpec = new DataTable();
        bool m_hRunning = false;
        bool m_AutoRunning = false;
        public delegate void DataUpThreadDelegate();//绑定委托
        private DataUpThreadDelegate m_DataUpThreadDelegate;//建立委托变量

        public delegate void QueryYBThreadDelegate();//绑定委托
        private QueryYBThreadDelegate m_QueryYBThreadDelegate;//建立委托变量
        string m_insert_hxcf = "0";//获取化学成分占用标志 1 为占用 2 空闲

        bool _flashFlag1 = false;
        bool _flashFlag2 = false;

        public WeightPlan_FP()
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
            if (!IsInt(this.tbCount.Text.Trim()) || Convert.ToInt32(this.tbCount.Text.Trim()) <= 0)
            {
                MessageBox.Show("请输入正确的条数！");
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
            string p_FN_GP_NI = tbNi.Text.Trim();

            string p_FN_GP_CR = tbCr.Text.Trim();
            string p_FN_GP_CU = tbCu.Text.Trim();
            string p_FN_GP_V = tbV.Text.Trim();
            string p_FN_GP_MO = tbMo.Text.Trim();
            string p_FN_GP_NB = tbNb.Text.Trim();
            string p_FN_GP_CEQ = tbCeq.Text.Trim();
            string p_FN_GP_AS = tbAs.Text.Trim();
            string p_FN_GP_TI= tbTi.Text.Trim();
            string p_FN_GP_SB= tbSb.Text.Trim();
            string p_FN_GP_ALS= tbAls.Text.Trim();
            int p_FN_GP_TOTALCOUNT = Convert.ToInt32(tbCount.Text.Trim());
            string p_FS_GP_MEMO = tbMemo.Text.Trim();
            string p_FS_GP_JUDGER = tbPerson.Text.Trim();
            string p_FS_ADVISESPEC = tbADVSPEC.Text.Trim();
            string p_FS_ZZJY = this.cbx_zzjy.Text.Trim();
            string p_FS_GDFLAG = Convert.ToInt32(checkBox1.Checked).ToString();
            string p_FS_QYG = "";
            if (chkQY.Checked == true)
                p_FS_QYG = "是";

            //20120406加，预报支数是否确定
            string p_FS_CountIsConfirm = string.Empty;
            if (this.cbCountIsConfirm.Checked)
            {
                p_FS_CountIsConfirm = "1";
            }
            else
            {
                p_FS_CountIsConfirm = "0";
            }

            //20120413加，化学成份是还否格
            string p_FS_UNQUALIFIED = string.Empty;
            if (this.cbUnquanified.Checked)
            {
                p_FS_UNQUALIFIED = "0";
            }
            else
            {
                p_FS_UNQUALIFIED = "1";
            }

            //新增是否组织利用字段
            string p_FS_ORGUSE = this.cbx_OrgUse.Text.Trim();
            string p_FS_STEELSENDTYPE = this.cbx_SteelSendType.Text.Trim();

            string strSql = " select FN_GP_TOTALCOUNT,FS_QYG,FS_ISORGUSE,FS_GP_STEELTYPE,FN_GP_LEN from IT_FP_TECHCARD where FS_CARDNO = '" + p_FS_CARDNO + "'";
            DataTable dts = new DataTable();
            dts = m_BaseInfo.QueryData(strSql);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "Update_FP_GP_ZJ";
            ccp.ServerParams = new object[] { p_FS_CARDNO, p_FS_GP_STEELTYPE, p_FS_GP_SPE, p_FN_GP_C, p_FN_GP_SI, 
                p_FN_GP_MN,  p_FN_GP_S, p_FN_GP_P, p_FN_GP_NI ,p_FN_GP_CR,
                p_FN_GP_CU,p_FN_GP_V,p_FN_GP_MO,p_FN_GP_NB,p_FN_GP_CEQ,

               p_FN_GP_AS, p_FN_GP_TI,p_FN_GP_SB,p_FN_GP_ALS,p_FS_GP_MEMO,
               p_FS_GP_JUDGER,p_FS_ADVISESPEC,p_FN_GP_LEN,p_FN_GP_TOTALCOUNT,p_FS_QYG,
               p_FS_ZZJY,p_FS_ORGUSE,p_FS_STEELSENDTYPE,p_FS_CountIsConfirm,p_FS_UNQUALIFIED};
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

                    if (p_FS_QYG != dts.Rows[0]["FS_QYG"].ToString().Trim())
                        m_Memo = m_Memo + " FS_QYG(是否取样钢) = " + dts.Rows[0]["FS_QYG"].ToString().Trim() + " ->" + p_FS_QYG;

                    if (p_FS_ORGUSE != dts.Rows[0]["FS_ISORGUSE"].ToString().Trim())
                        m_Memo = m_Memo + " FS_ISORGUSE(是否组织利用) = " + dts.Rows[0]["FS_ISORGUSE"].ToString().Trim() + " ->" + p_FS_ORGUSE;

                    if (p_FS_GP_STEELTYPE != dts.Rows[0]["FS_GP_STEELTYPE"].ToString().Trim())
                        m_Memo = m_Memo + " FS_GP_STEELTYPE(钢种) = " + dts.Rows[0]["FS_GP_STEELTYPE"].ToString().Trim() + " -> " + p_FS_GP_STEELTYPE;

                    if (p_FN_GP_LEN.ToString() != dts.Rows[0]["FN_GP_LEN"].ToString().Trim())
                        m_Memo = m_Memo + " FN_GP_LEN(定尺) = " + dts.Rows[0]["FN_GP_LEN"].ToString().Trim() + " -> " + p_FN_GP_LEN;

                    if (m_Memo != "")
                        m_BaseInfo.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, p_FS_CARDNO, "", "", "", "", "IT_FP_TECHCARD", "钢坯管理/红钢辊道计量预报/卡片修改按钮");

                }
            }
            //this.SaveWeedSteel(p_FS_CARDNO);
            this.ClearWeedSteel();
            this.QueryCard();
            ultraGrid1ActiveNewRow(this.tbStoveNo.Text.Trim().ToString());
            //this.tbStoveNo.Text = "";
            //this.tbCardNo.Text = "";
            this.chkQY.Checked = false;
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

            string sql = "SELECT A.FS_CARDNO,A.FS_GP_STOVENO,A.FS_GP_STEELTYPE,A.FS_GP_SPE,to_char(A.FN_GP_LEN) AS FN_LENGTH,";
            sql += "A.FN_GP_C,A.FN_GP_SI,A.FN_GP_MN,A.FN_GP_S,A.FN_GP_P,A.FN_GP_NI,A.FN_GP_CR,A.FN_GP_CU,A.FN_GP_V,A.FN_GP_AS,A.FN_GP_TI,A.FN_GP_SB,A.FN_GP_ALS,A.FS_GP_JUDGER,to_char(A.FD_GP_JUDGEDATE,'yyyy-MM-dd hh24:mi:ss') AS FD_GP_JUDGEDATE,";
            sql += "A.FN_GP_MO,A.FN_GP_NB,A.FN_GP_CEQ,A.FN_GP_TOTALCOUNT,FS_ADVISESPEC,FS_ISCOUNTCOMFIRM,decode(FS_UNQUALIFIED,'0','√','1','×','√') FS_UNQUALIFIED,decode(FS_GP_FLOW,'SH000098','棒材厂','SH000166','炼钢落地','棒材厂') FS_GP_FLOW, ";
            sql += "decode(FS_TRANSTYPE,'1','热送','2','冷送','热送') FS_TRANSTYPE,FS_GP_MEMO FROM IT_FP_TECHCARD A ";
            sql += "WHERE (a.FS_ISVALID = '0' or a.FS_ISVALID is null) and a.FS_GP_COMPLETEFLAG='0' and FS_CARDNO like 'FP10%'";
            sql += str + " order by a.FD_GP_JUDGEDATE desc";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            dataSet1.Tables[0].Clear();

            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];

                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    if (ultraGrid1.Rows[i].Cells["FS_UNQUALIFIED"].Value.ToString() == "×")
                    {
                        ultraGrid1.Rows[i].Appearance.ForeColor = Color.Red;
                    }
                }
            }
            Constant.RefreshAndAutoSize(ultraGrid1);
        }

        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null) return;

            //if (PointID != ultraGrid1.ActiveRow.Cells["FS_POINTID"].Text.Trim())
            //{
            //    PointID = ultraGrid1.ActiveRow.Cells["FS_POINTID"].Text.Trim();
            //    RefreshBaseInfo();
            //}

            this.tbCardNo.Text = ugr.Cells["FS_CARDNO"].Text.Trim();
            this.tbStoveNo.Text = ugr.Cells["FS_GP_STOVENO"].Text.Trim();
            this.cbSteelType.Text = ugr.Cells["FS_GP_STEELTYPE"].Text.Trim();
            if (ugr.Cells["FS_GP_SPE"].Text.Trim().ToString() != "")
                this.cbSpec.Text = ugr.Cells["FS_GP_SPE"].Text.Trim();
            else
                this.cbSpec.Text = "165";
            if (ugr.Cells["FN_LENGTH"].Text.Trim().ToString() != "")
                this.tbLength.Text = ugr.Cells["FN_LENGTH"].Text.Trim();

            this.tbCount.Text = ugr.Cells["FN_GP_TOTALCOUNT"].Text.Trim();

            this.tbC.Text = ugr.Cells["FN_GP_C"].Text.Trim();
            this.tbSi.Text = ugr.Cells["FN_GP_SI"].Text.Trim();
            this.tbMn.Text = ugr.Cells["FN_GP_MN"].Text.Trim();
            this.tbS.Text = ugr.Cells["FN_GP_S"].Text.Trim();
            this.tbP.Text = ugr.Cells["FN_GP_P"].Text.Trim();
            this.tbNi.Text = ugr.Cells["FN_GP_NI"].Text.Trim();
            this.tbCr.Text = ugr.Cells["FN_GP_CR"].Text.Trim();
            this.tbAs.Text = ugr.Cells["FN_GP_AS"].Text.Trim();

            this.tbCu.Text = ugr.Cells["FN_GP_CU"].Text.Trim();
            this.tbV.Text = ugr.Cells["FN_GP_V"].Text.Trim();
            this.tbMo.Text = ugr.Cells["FN_GP_MO"].Text.Trim();
            this.tbNb.Text = ugr.Cells["FN_GP_NB"].Text.Trim();
            this.tbCeq.Text = ugr.Cells["FN_GP_CEQ"].Text.Trim();
            this.tbTi.Text = ugr.Cells["FN_GP_TI"].Text.Trim();
            this.tbSb.Text = ugr.Cells["FN_GP_SB"].Text.Trim();
            this.tbAls.Text = ugr.Cells["FN_GP_ALS"].Text.Trim();
            if (ugr.Cells["FS_ADVISESPEC"].Text.Trim().ToString() != "")
                this.tbADVSPEC.Text = ugr.Cells["FS_ADVISESPEC"].Text.Trim();
            this.LxComb.Text = ugr.Cells["FS_GP_FLOW"].Text.Trim().ToString();
            this.cbTransType.Text = ugr.Cells["FS_TRANSTYPE"].Text.Trim().ToString();
            if (ugr.Cells["FS_UNQUALIFIED"].Text.Trim().ToString() == "√")
            {
                this.cbUnquanified.Checked = true;
            }
            else
            {
                this.cbUnquanified.Checked = false;
            }
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

            //预报支数是否确定
            if (ugr.Cells["FS_ISCOUNTCOMFIRM"].Text == "0")
            {
                this.cbCountIsConfirm.Checked = false;
            }
            else
            {
                this.cbCountIsConfirm.Checked = true;
            }


            if (ugr.Cells["FS_UNQUALIFIED"].Text == "√")
            {
                this.cbUnquanified.Checked = true;
            }
            else
            {
                this.cbUnquanified.Checked = false;
            }

            this.tbMemo.Text = ugr.Cells["FS_GP_MEMO"].Text.Trim();  

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
                string p_FS_GP_STEELTYPE = cbSteelType.Text.Trim();
                string p_FS_GP_SPE = cbSpec.Text.Trim();
                float p_FN_GP_LEN = Convert.ToSingle(tbLength.Text.Trim());
                string p_FN_GP_C = tbC.Text.Trim();

                string p_FN_GP_SI = tbSi.Text.Trim();
                string p_FN_GP_MN = tbMn.Text.Trim();
                string p_FN_GP_S = tbS.Text.Trim();
                string p_FN_GP_P = tbP.Text.Trim();
                string p_FN_GP_NI = tbNi.Text.Trim();

                string p_FN_GP_CR = tbCr.Text.Trim();
                string p_FN_GP_CU = tbCu.Text.Trim();
                string p_FN_GP_V = tbV.Text.Trim();
                string p_FN_GP_MO = tbMo.Text.Trim();
                string p_FN_GP_NB = tbNb.Text.Trim();
                string p_FN_GP_CEQ = tbCeq.Text.Trim();
                int p_FN_GP_TOTALCOUNT = 0;
                string p_FS_GP_MEMO = tbMemo.Text.Trim();
                string p_FS_TRANSTYPE = cbTransType.SelectedValue.ToString();
                string p_FS_GP_JUDGER = tbPerson.Text.Trim();
                string p_FS_ADVISESPEC = tbADVSPEC.Text.Trim();
                string p_FS_QYG = "";
                p_FS_QYG = "否";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
                ccp.MethodName = "Update_FP_GP_ZJ";
                ccp.ServerParams = new object[] { p_FS_CARDNO, p_FS_GP_STEELTYPE, p_FS_GP_SPE, p_FN_GP_C, p_FN_GP_SI, p_FN_GP_MN,
            p_FN_GP_S, p_FN_GP_P, p_FN_GP_NI ,p_FN_GP_CR,p_FN_GP_CU,p_FN_GP_V,p_FN_GP_MO,p_FN_GP_NB,p_FN_GP_CEQ,p_FS_GP_MEMO,p_FS_GP_JUDGER,p_FS_ADVISESPEC,p_FN_GP_LEN,p_FN_GP_TOTALCOUNT,p_FS_QYG};
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                this.QueryCard();
                ultraGrid1ActiveNewRow(this.tbStoveNo.Text.Trim().ToString());
                this.tbStoveNo.Text = "";
                this.tbCardNo.Text = "";
                this.chkQY.Checked = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 保存剔废信息
        /// </summary>
        /// <param name="StrCardNo"></param>
        private void SaveWeedSteel(string StrCardNo)
        {
            try
            {
                string strCardNo = StrCardNo;
                string strqj = this.tbx_qj.Text.Trim();
                string strlw = this.tbx_lw.Text.Trim();
                string strjz = this.tbx_jz.Text.Trim();
                string strtf = this.tbx_tf.Text.Trim();
                string strwq = this.tbx_wq.Text.Trim();
                string strqk = this.tbx_qk.Text.Trim();
                string strsk = this.tbx_sk.Text.Trim();
                string strqt = this.tbx_qt.Text.Trim();

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_PC";
                ccp.MethodName = "SaveWeedSteel";
                ccp.IfShowErrMsg = false;
                ccp.ServerParams = new object[] { strCardNo, strqj, strlw, strjz, strtf, strwq, strqk, strsk, strqt };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnCode != 0)
                {
                    MessageBox.Show(ccp.ReturnObject.ToString());
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 查询剔废信息
        /// </summary>
        /// <param name="strCardNo"></param>
        private void QueryWeedSteel(string strCardNo)
        {
            try
            {
                string strSql = "select fs_cardno,fn_qj,fn_lw,fn_jz,fn_tf,fn_wq,fn_qk,fn_sk,fn_qt from DT_STEELWEEDINFO where fs_cardno = '" + strCardNo + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;

                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                if (dt_temp.Rows.Count > 0)
                {
                    this.tbx_qj.Text = dt_temp.Rows[0]["FN_QJ"].ToString();
                    this.tbx_lw.Text = dt_temp.Rows[0]["FN_LW"].ToString();
                    this.tbx_jz.Text = dt_temp.Rows[0]["FN_JZ"].ToString();
                    this.tbx_tf.Text = dt_temp.Rows[0]["FN_TF"].ToString();
                    this.tbx_wq.Text = dt_temp.Rows[0]["FN_WQ"].ToString();
                    this.tbx_qk.Text = dt_temp.Rows[0]["FN_QK"].ToString();
                    this.tbx_sk.Text = dt_temp.Rows[0]["FN_SK"].ToString();
                    this.tbx_qt.Text = dt_temp.Rows[0]["FN_QT"].ToString();
                }
                else
                {
                    this.tbx_qj.Text = "0";
                    this.tbx_lw.Text = "0";
                    this.tbx_jz.Text = "0";
                    this.tbx_tf.Text = "0";
                    this.tbx_wq.Text = "0";
                    this.tbx_qk.Text = "0";
                    this.tbx_sk.Text = "0";
                    this.tbx_qt.Text = "0";
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 踢废信息置0
        /// </summary>
        private void ClearWeedSteel()
        {
            this.tbx_qj.Text = "0";
            this.tbx_lw.Text = "0";
            this.tbx_jz.Text = "0";
            this.tbx_tf.Text = "0";
            this.tbx_wq.Text = "0";
            this.tbx_qk.Text = "0";
            this.tbx_sk.Text = "0";
            this.tbx_qt.Text = "0";
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
                case "Delete":
                    {
                        string p_FS_STOVENO = "";

                        if (ugr == null)
                        {
                            MessageBox.Show("请选择需要删除的预报！");
                            break;
                        }
                        else
                        {
                            p_FS_STOVENO = ugr.Cells["FS_STOVENO"].Text.Trim();
                        }
                        if (DialogResult.Yes == MessageBox.Show("您确认要删除炉号为" + p_FS_STOVENO + "的该条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            this.DoDelete_YB(ugr);
                            this.QueryCard();
                            this.QueryYB();
                            ultraGrid1ActiveNewRow(p_FS_STOVENO);
                            break;
                        }
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
            
            if(!isBeginWeight(strStoveNo))
            {
                MessageBox.Show("该炉号需至少开始一条才能强制完炉");
                return;
            }


            if (DialogResult.No == MessageBox.Show("强制完炉将导致改炉无法继续计量，是否需要强制完炉？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                return;
            }

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
            ccp.MethodName = "completeStove";
            ccp.ServerParams = new object[] { strTechCard };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode == 0)
            {
                string strSql="update it_fp_techcard set FS_GP_COMPLETEFLAG='1' where FS_GP_STOVENO='"+strStoveNo+"'";
                
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { strSql };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                strSql = "update dt_steelweightmain set FS_COMPLETEFLAG='1' where FS_STOVENO='" + strStoveNo + "'";

                ccp.ServerParams = new object[] { strSql };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                MessageBox.Show("该炉已强制完炉成功！");
            }

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
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "Delete_FP_Card";
            ccp.ServerParams = new object[] { cardno };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
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
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
                        ccp.MethodName = "AddOneQSInfo";
                        ccp.ServerParams = new object[] { this.tbQueryStoveNo.Text.Trim() };
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                           
                        QueryCard();
                    }
                    //end
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

        private void DoAdd()
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (this.tbStoveNo.Text.Trim() == "")
            {
                MessageBox.Show("请选择工艺流动卡信息或手工录入数据！");
                return;
            }

            if (DoCheck())
            {
                return;
            }
            BaseOperation();
            QueryOldCardNo();

            string p_FS_CARDNO = "";
            if (this.tbCardNo.Text.Trim() != "")
            {
                p_FS_CARDNO = this.tbCardNo.Text.Trim();
            }
            else
            {
                p_FS_CARDNO = GetNewCardNumber();
                if (p_FS_CARDNO == string.Empty)
                {
                    MessageBox.Show("生成流动工艺卡号失败！");
                    return;
                }
            }

            string p_FS_STOVENO = this.tbStoveNo.Text.Trim();
            string p_FS_SPEC = this.cbSpec.Text.Trim();
            string p_FS_STEELTYPE = this.cbSteelType.Text.Trim();
            string p_FN_LENGTH = tbLength.Text.Trim();
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
            string p_FS_GP_MEMO = tbMemo.Text.Trim();
            string p_FS_GP_JUDGER = tbPerson.Text.Trim();
            string p_FS_RECEIVER = this.LxComb.SelectedValue.ToString();
            string p_FS_GDFLAG = Convert.ToInt32(checkBox1.Checked).ToString();
            string p_FS_ADVISESPEC = this.tbADVSPEC.Text.Trim();
            string p_FS_MATERIAL = "方坯";
            string p_FS_YLBB = this.cbYLBB.Text.Trim();

            //YY-2011-03-10新增提示
            if (CheckRollWeightRecode(p_FS_STOVENO) != "0")
            {
                if (DialogResult.No == MessageBox.Show("炉号" + p_FS_STOVENO + "此炉号已存在，并有过磅记录，继续操作将产生辊道预报，继续操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    return;
                }
            }


            int is_C_notnull = IsGpCompleted(p_FS_STOVENO);
            if (is_C_notnull == 0)
            {
                if (DialogResult.No == MessageBox.Show("炉号" + p_FS_STOVENO + "在工艺流动卡中不存在，继续操作将产生辊道预报和没有化学成分的卡片，继续操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    return;
                }
            }
            else if (is_C_notnull == 1)
            {
                if (DialogResult.No == MessageBox.Show("炉号" + p_FS_STOVENO + "的卡片状态为已完炉，用继续操作产生的辊道预报过磅，将影响该炉号以前的过磅记录，确认继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    return;
                }
            }

            p_FS_MATERIAL = cbMaterial.SelectedValue.ToString().Trim();//物料代码
       
            string p_FS_ITEMNO = this.txtDDXMH.Text.Trim();
            string p_FS_ORDERNO = this.txtDDH.Text.Trim();
            string p_FS_ORGUSE = this.cbx_OrgUse.Text.Trim();
            string p_FS_STEELSENDTYPE = this.cbx_SteelSendType.Text.Trim();
            string p_FS_ISCOUNTCOMFIRM = string.Empty;
            if (cbCountIsConfirm.Checked)
            {
                p_FS_ISCOUNTCOMFIRM = "1";
            }
            else
            {
                p_FS_ISCOUNTCOMFIRM = "0";
            }

            if (this.LxComb.Text == "")
            {
                MessageBox.Show("请选择发往单位！");
            }

            //20120310加
            string p_FS_TRANSTYPE = this.cbTransType.SelectedValue.ToString();

            if (p_FS_RECEIVER == "SH000166")
            {
                p_FS_TRANSTYPE = "2";
            }

            //20120413加，化学成份是还否格
            string p_FS_UNQUALIFIED = string.Empty;
            if (this.cbCountIsConfirm.Checked)
            {
                p_FS_UNQUALIFIED = "0";
            }
            else
            {
                p_FS_UNQUALIFIED = "1";
            }
            
            //20120403新增，新增预报提示
            if (MessageBox.Show("当前去向选择:" + this.LxComb.Text+ ",运输方式:"+this.cbTransType.Text+",确定？", "红钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            string strSql = " select FN_GP_TOTALCOUNT,FS_GP_STEELTYPE,FN_GP_LEN,FS_ISORGUSE from IT_FP_TECHCARD where FS_GP_STOVENO = '" + p_FS_STOVENO + "'";
            DataTable dts = new DataTable();
            dts = m_BaseInfo.QueryData(strSql);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "ADD_FP_PLAN";
            ccp.IfShowErrMsg = false;
            ccp.ServerParams = new object[] { p_FS_CARDNO, p_FS_STOVENO, p_FS_SPEC, p_FS_STEELTYPE, p_FS_GP_MEMO, p_FS_GP_JUDGER, p_FS_GDFLAG, p_FN_LENGTH, p_FN_TOTALCOUNT, p_FS_MATERIAL, p_FS_ITEMNO, p_FS_ORDERNO, p_FS_ADVISESPEC, p_FS_YLBB, p_FS_ORGUSE, p_FS_STEELSENDTYPE, p_FS_TRANSTYPE, p_FS_RECEIVER, p_FS_ISCOUNTCOMFIRM, p_FS_UNQUALIFIED};

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            string result = "";


            //if (ccp.ReturnCode != -1)
            //{
            //    string m_Memo = "";
            //    string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //    string strIP = m_BaseInfo.getIPAddress();
            //    string strMAC = m_BaseInfo.getMACAddress();
            //    string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            //    result = ccp.ReturnObject.ToString();
            //    if (result == "exist")
            //    {
            //        if (p_FS_GDFLAG == "0")
            //        //{
            //        //    m_Memo = "FS_GP_USED=1 ,FS_TRANSTYPE=1";
            //        //}
            //        //else
            //        {
            //            m_Memo = "FS_GP_USED=0,FS_TRANSTYPE=3";
            //        }
            //        if (dts.Rows.Count > 0)
            //        {
            //            if (p_FN_TOTALCOUNT.ToString().Trim() != dts.Rows[0]["FN_GP_TOTALCOUNT"].ToString().Trim())
            //                m_Memo = m_Memo + " FN_GP_TOTALCOUNT(钢坯条数) = " + dts.Rows[0]["FN_GP_TOTALCOUNT"].ToString().Trim() + " -> " + p_FN_TOTALCOUNT + "";

            //            if (p_FS_STEELTYPE != dts.Rows[0]["FS_GP_STEELTYPE"].ToString().Trim())
            //                m_Memo = m_Memo + " FS_GP_STEELTYPE(钢种) = " + dts.Rows[0]["FS_GP_STEELTYPE"].ToString().Trim() + " ->" + p_FS_STEELTYPE;

            //            if (p_FN_LENGTH.ToString() != dts.Rows[0]["FN_GP_LEN"].ToString().Trim())
            //                m_Memo = m_Memo + " FN_GP_LEN(定尺) = " + dts.Rows[0]["FN_GP_LEN"].ToString().Trim() + " -> " + p_FN_LENGTH;

            //            if (p_FS_ORGUSE != dts.Rows[0]["FS_ISORGUSE"].ToString().Trim())
            //                m_Memo = m_Memo + " FS_ISORGUSE(是否组织利用) = " + dts.Rows[0]["FS_ISORGUSE"].ToString().Trim() + " -> " + p_FS_ORGUSE;

            //            if (m_Memo != "")
            //                m_BaseInfo.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, p_FS_STOVENO, p_FS_STOVENO, "", "", "", "IT_FP_TECHCARD", "钢坯管理/红钢辊道计量预报/预报新增按钮");

            //        }

            //    }
            //    else
            //    {
            //        if (p_FS_GDFLAG == "1")
            //        {
            //            m_Memo = "FS_GP_USED=1,FN_GP_TOTALCOUNT=" + p_FN_TOTALCOUNT + ",FN_GP_LEN=" + p_FN_LENGTH + ",FS_GP_STEELTYPE=" + p_FS_STEELTYPE + ",FS_TRANSTYPE =" + p_FS_TRANSTYPE;
            //        }
            //        else
            //        {
            //            m_Memo = "FS_GP_USED=0,FN_GP_TOTALCOUNT=" + p_FN_TOTALCOUNT + ",FN_GP_LEN=" + p_FN_LENGTH + ",FS_GP_STEELTYPE=3";
            //        }

            //        m_BaseInfo.insertLog(strDate, "增加", p_UPDATER, strIP, strMAC, m_Memo, p_FS_STOVENO, p_FS_STOVENO, "", "", "", "IT_FP_TECHCARD", "钢坯管理/红钢辊道计量预报/预报新增按钮");
            //    }
            //}

            //this.SaveWeedSteel(p_FS_CARDNO);
            //ClearWeedSteel();
            this.QueryCard();
            this.QueryYB();
            ultraGrid1ActiveNewRow(this.tbStoveNo.Text.Trim().ToString());
        }

        private string GetNewCardNumber()
        {
            string strResult = string.Empty;
            string strSQL="SELECT 'FP10'||TO_CHAR(SYSDATE,'YYYYMMDD')||LPAD(NVL(TO_NUMBER(SUBSTR(MAX(FS_CARDNO),13,4)),0)+1,4,'0') as fs_cardno FROM IT_FP_TECHCARD ";
            strSQL += "WHERE FS_CARDNO LIKE 'BP10'||TO_CHAR(SYSDATE,'YYYYMMDD')||'%'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            DataTable dt = new DataTable();
            ccp.ServerParams = new object[] { strSQL };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                strResult = dt.Rows[0]["fs_cardno"].ToString();
            }

            return strResult;
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
                    "(SELECT COUNT(D.FS_STOVENO) FROM DT_STEELWEIGHTDETAILROLL D WHERE D.FS_STOVENO = A.FS_STOVENO) WEIGHEDCOUNT " +
                    "FROM DT_FP_PLAN A WHERE A.FS_COMPLETEFLAG <> '1' ";
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
                    this.ultraGroupBox4.Text = "当前计量  炉号：" + dt1.Rows[0]["FS_STOVENO"].ToString() + "： " + dt1.Rows[0]["FN_BILLETCOUNT"].ToString() + "/" + dt1.Rows[0]["FN_TOTALBILLET"].ToString() + "支";
                    ultraGroupBox4.Refresh();
                }
                else
                {
                    this.ultraGrid2.Text = string.Empty;
                }
            }
        }

        private void ultraGrid2_DoubleClick_1(object sender, EventArgs e)
        {
            //UltraGridRow ugr = this.ultraGrid2.ActiveRow;
            //if (ugr == null) return;

            //if (PointID != ultraGrid2.ActiveRow.Cells["FS_POINTID"].Text.Trim())
            //{
            //    PointID = ultraGrid2.ActiveRow.Cells["FS_POINTID"].Text.Trim();
            //    BandPointMaterial(PointID);
            //    BandPointSteelType(PointID);
            //}


            //this.tbCardNo.Text = ugr.Cells["FS_TECHCARDNO"].Text.Trim();
            //this.tbStoveNo.Text = ugr.Cells["FS_STOVENO"].Text.Trim();
            //this.cbSteelType.Text = ugr.Cells["FS_STEELTYPE"].Text.Trim();
            //this.cbSpec.Text = ugr.Cells["FS_SPEC"].Text.Trim();
            //this.tbLength.Text = ugr.Cells["FN_LENGTH"].Text.Trim();
            //this.tbCount.Text = ugr.Cells["FN_COUNT"].Text.Trim();
            //if (ugr.Cells["FS_ADVISESPEC"].Text.Trim().ToString() != "")
            //    this.tbADVSPEC.Text = ugr.Cells["FS_ADVISESPEC"].Text.Trim();
            //if (ugr.Cells["FS_GP_MEMO"].Text.Trim().ToString() != "")
            //this.tbMemo.Text = ugr.Cells["FS_GP_MEMO"].Text.Trim();
            //this.checkBox1.Checked = Convert.ToBoolean(Convert.ToInt32(ugr.Cells["FS_GDFLAG"].Text.Trim()));
            //if (ugr.Cells["FS_TRANSTYPE"].Text.Trim().ToString() != "")
            //{
            //    this.cbPoint.Text = ugr.Cells["FS_TRANSTYPE"].Text.Trim();
            //}
            //this.QueryWeedSteel(this.tbCardNo.Text);
        }

        private void WeightPlan_FP_Load(object sender, EventArgs e)
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
            //this.DownLoadSepc(); //下载磅房对应规格信息到内存表
            this.cbx_SteelSendType.SelectedIndex = 0;
            BandPointSteelType(PointID);
            //BandPointSpec(PointID);
            //BindTransType();
            m_query = "2";
            m_insert_hxcf = "2";
            m_flag = true;
            m_thread = new Thread(new ThreadStart(QueryTread));
            m_thread.Start();
            m_hRunning = true;
            m_AutoRunning = false;
            this.textBox1.Location = new System.Drawing.Point(-1, -27);
            //BindTranType();
            //BindTranType3();
            //BindLXTyep1();
            //BindLXTyep2();
            checkBox1.Checked = true;
            cbAnalysisTime.Checked = true;
            //timer1.Start();
            BindMaterial();
            BindTranType();
            BindLXType();
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
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "DoUp_FP_Plan";
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
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "DoDown_FP_Plan";
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
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "UpToTop_FP_Plan";
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
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "DownToBotton_FP_Plan";
            ccp.ServerParams = new object[] { p_FS_STOVENO, p_FS_ORDER };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.QueryYB();
            ultraGrid2ActiveNewRow(p_FS_STOVENO);
        }

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
            this.SaveWeedSteel(p_FS_CARDNO);
            this.ClearWeedSteel();
            this.QueryYB();
            ultraGrid1ActiveNewRow(this.tbStoveNo.Text.Trim().ToString());
            this.tbStoveNo.Text = "";
            this.tbCardNo.Text = "";
        }

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

        private void WeightPlan_FP_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_flag = false;
            System.Threading.Thread.Sleep(2000);
            m_thread.Abort();
            m_hRunning = false;
            m_AutoRunning = false;
        }

        private string QuerySingleYB(string p_FS_STOVENO)//移动前预报查询
        {
            string str = "";
            DataTable tbYB = new DataTable();
            str = " and A.FS_STOVENO like '%" + p_FS_STOVENO + "%'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
            ccp.MethodName = "Query_SINGLE_YB";
            ccp.ServerParams = new object[] { str };
            tbYB = dataSet1.Tables[1].Clone();
            tbYB.Clear();
            ccp.SourceDataTable = tbYB;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            return tbYB.Rows[0]["FS_COMPLETEFLAG"].ToString().Trim();
        }

        //private void QueryAndBindZBData()
        //{
        //    string str = "select FN_TOTALWEIGHT,FN_BILLETCOUNT,FS_STOVENO from DT_STEELWEIGHTMAIN where FS_COMPLETEFLAG ='2'";
        //    CoreClientParam ccp = new CoreClientParam();
        //    ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_GD";
        //    ccp.MethodName = "QueryJLCZData";
        //    ccp.ServerParams = new object[] { str };
        //    ccp.SourceDataTable = dataTable1;
        //    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        //}

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
                    string str = "select a.FN_TOTALWEIGHT,a.FN_BILLETCOUNT,a.FS_STOVENO,b.fn_count as FN_TOTALBILLET from DT_STEELWEIGHTMAIN a,DT_FP_PLAN b where a.fs_stoveno = b.fs_stoveno and a.FS_COMPLETEFLAG <>'0' and a.fs_stoveno = '" + this.ultraGrid2.Rows[0].Cells["FS_STOVENO"].Text.Trim() + "' order by a.FD_STARTTIME desc";
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

            ////磅房对应规格表
            //dc = new DataColumn("FS_POINTNO".ToUpper()); m_SpecTable.Columns.Add(dc);
            //dc = new DataColumn("FS_SPEC".ToUpper()); m_SpecTable.Columns.Add(dc);
            //dc = new DataColumn("FN_TIMES".ToUpper()); m_SpecTable.Columns.Add(dc);

        }
        
        //下载磅房对应钢种信息
        private void DownLoadSteelType()
        {
            string strSql = "SELECT A.FS_POINTNO,A.FS_STEELTYPE,A.FN_TIMES FROM BT_POINTSTEELTYPE A,BT_POINT B WHERE A.FS_POINTNO = B.FS_POINTCODE AND B.FS_POINTTYPE = 'GD' ORDER BY FN_TIMES DESC";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SteelTypeTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }
        ////下载磅房对应规格信息  
        //private void DownLoadSepc()
        //{
        //    string strSql = "SELECT A.FS_POINTNO,A.FS_SPEC,A.FN_TIMES FROM BT_POINTSPEC A,BT_POINT B WHERE A.FS_POINTNO = B.FS_POINTCODE AND B.FS_POINTTYPE = 'GC' ORDER BY FN_TIMES DESC";

        //    CoreClientParam ccp = new CoreClientParam();
        //    ccp.ServerName = "ygjzjl.storageweight.StoreageWeight_BC";
        //    ccp.MethodName = "QueryTableData";
        //    ccp.ServerParams = new object[] { strSql };
        //    ccp.SourceDataTable = this.m_SpecTable;

        //    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        //}
       
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

        ////按磅房筛选规格
        //private void BandPointSpec(string PointID)
        //{
        //    DataRow[] drs = null;

        //    this.tempSpec = this.m_SpecTable.Clone();

        //    drs = this.m_SpecTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

        //    this.tempSpec.Clear();
        //    foreach (DataRow dr in drs)
        //    {
        //        this.tempSpec.Rows.Add(dr.ItemArray);
        //    }

        //    DataRow drz = this.tempSpec.NewRow();
        //    this.tempSpec.Rows.InsertAt(drz, 0);
        //    this.cbSpec.DataSource = this.tempSpec;
        //    cbSpec.DisplayMember = "FS_SPEC";
        //    //cbSpec.ValueMember = "FS_SPEC";

        //}
        private void InitConfig()
        {

            cbMaterial.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbMaterial.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbSteelType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbSteelType.AutoCompleteSource = AutoCompleteSource.ListItems;

            //cbSpec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cbSpec.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        #endregion

        private void chkQY_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkQY.Checked == true)
            {
                this.tbC.ReadOnly = false;
                this.tbCeq.ReadOnly = false;
                this.tbCr.ReadOnly = false;
                this.tbCu.ReadOnly = false;
                this.tbMn.ReadOnly = false;
                this.tbMo.ReadOnly = false;
                this.tbNb.ReadOnly = false;
                this.tbNi.ReadOnly = false;
                this.tbP.ReadOnly = false;
                this.tbS.ReadOnly = false;
                this.tbSi.ReadOnly = false;
                this.tbV.ReadOnly = false;
                this.tbAs.ReadOnly = false;
                this.tbTi.ReadOnly = false;
                this.tbSb.ReadOnly = false;
                this.tbAls.ReadOnly = false;
            }
            else
            {
                this.tbC.ReadOnly = true;
                this.tbCeq.ReadOnly = true;
                this.tbCr.ReadOnly = true;
                this.tbCu.ReadOnly = true;
                this.tbMn.ReadOnly = true;
                this.tbMo.ReadOnly = true;
                this.tbNb.ReadOnly = true;
                this.tbNi.ReadOnly = true;
                this.tbP.ReadOnly = true;
                this.tbS.ReadOnly = true;
                this.tbSi.ReadOnly = true;
                this.tbV.ReadOnly = true;
                this.tbAs.ReadOnly = true;
                this.tbTi.ReadOnly = true;
                this.tbSb.ReadOnly = true;
                this.tbAls.ReadOnly = true;
            }

        }

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

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            this.textBox1.Visible = false;
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
            dt.Rows.Add("SH000166", "炼钢落地");
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

        private void BindMaterial()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            // dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows.Add("方坯", "方坯");
            cbMaterial.DataSource = dt;
            cbMaterial.ValueMember = "Value";
            cbMaterial.DisplayMember = "Text";
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_flashFlag1)
            {
                if (ultraTabControl1.Tabs[1].Appearance.BackColor == System.Drawing.Color.Red)
                {
                    ultraTabControl1.Tabs[1].Appearance.BackColor = System.Drawing.Color.Transparent;
                }
                else
                {
                    ultraTabControl1.Tabs[1].Appearance.BackColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                ultraTabControl1.Tabs[1].Appearance.BackColor = System.Drawing.Color.Transparent;
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

        private void BindLXType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            dt.Rows.Add("SH000098", "棒材厂");
            dt.Rows.Add("SH000166", "炼钢落地");
            LxComb.DataSource = dt;
            LxComb.ValueMember = "Value";
            LxComb.DisplayMember = "Text";
        }

        private void LxComb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LxComb.SelectedValue == "SH000166")
            {
                lbTransType.Visible = false;
                cbTransType.Visible = false;
            }
            else
            {
                lbTransType.Visible = true;
                cbTransType.Visible = true;
            }
        }
    }
}

