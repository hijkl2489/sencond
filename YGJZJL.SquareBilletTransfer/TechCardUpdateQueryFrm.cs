using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using CoreFS.SA06;
using YGJZJL.PublicComponent;
using System.Net;
using System.Management;

namespace YGJZJL.SquareBilletTransfer
{
    public partial class TechCardUpdateQueryFrm : FrmBase
    {
        DataTable m_TechCardData = new DataTable("Table1");
        public TechCardUpdateQueryFrm()
        {
            InitializeComponent();
        }

        private void tbx_QBatchNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void QueryTechCard()
        {
            
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (Constant.WaitingForm == null)
            {
                Constant.WaitingForm = new WaitingForm();
            }
            Constant.WaitingForm.ShowToUser = true;
            Constant.WaitingForm.Show();
            Constant.WaitingForm.Update();

            try
            {

               /* string strSql = "select FS_CARDNO,FS_GP_STOVENO,FS_GP_STEELTYPE,FS_GP_SPE,to_char(FN_GP_LEN,'FM99990.00') as FN_GP_LEN,FS_GP_ORDERNO,FN_GP_C,FN_GP_SI,FN_GP_MN,FN_GP_S,FN_GP_P,FN_GP_NI,FN_GP_CR,FN_GP_CU,FN_GP_V,FN_GP_MO,FN_GP_NB,FN_GP_CEQ,FN_GP_TOTALCOUNT,FS_GP_MEMO,FS_GP_JUDGER,FD_GP_JUDGEDATE,FN_JJ_WEIGHT,FS_JJ_OPERATOR,FN_GPYS_NUMBER,FS_GPYS_RECEIVER,FS_ZC_BATCHNO,FS_ZC_ORDERNO,FD_ZC_ENTERDATETIME,FN_ZC_ENTERNUMBER,FS_ZC_OPERATOR,FN_ZZ_SPEC,FD_ZZ_DATE,FN_ZZ_WASTNUM,FN_ZZ_NUM,FS_ZZ_SHIFT,FS_ZZ_OPERATOR,FN_JZ_NUM,FN_JZ_WEIGHT,FN_JZ_RETURNWEIGHT,FN_JZ_WASTWEIGHT,FS_JZ_OPERATOR,FS_JZ_CHECKER,FS_MEMO,FS_GP_COMPLETEFLAG,FD_ANALYSISTIME,FS_ZC_MEMO,FS_ZZ_MEMO,FS_GP_USED,FS_DJH,FS_ADVISESPEC,FS_FREEZED,FN_GPYS_WEIGHT,FS_CHECKED,FS_GPYS_RESULT,FS_QYG,FS_YLBB,FN_JZ_THEORYWEIGHT,FS_GP_FLOW,FS_GP_PRODUCTOR,FS_GP_SEND,FS_GP_WG,FS_GP_HTH,FS_BC_WEIGHTFLAG,FS_GP_ITEMNO,FN_GP_AL,FN_GP_SN,FN_GP_TI,FN_GP_ALS,FN_GP_N,FN_GP_AS,FS_ISVALID,FN_GP_CA,FN_GP_B,FN_GP_ALT,FN_GP_SB,FS_GP_SOURCE,FS_GP_MATCH,'' fs_type from it_fp_techcard where FD_GP_JUDGEDATE between to_date('" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd HH24:mi:ss')";
                 */
                string strSql = "select to_char(FD_DATATIME,'yyyy-MM-dd HH24:mi:ss') as FD_DATATIME,FS_OPERATIONTYPE,FS_OPERATER,FS_OPERATIONIP,FS_OPERATIONMAC,FS_OPERATIONMEMO,FS_STOVENO,FS_BATCHNO,FS_KEYWORD,FS_CARNO,FS_BANDNO,FS_TABLENAME,FS_MODULENAME  from DT_TECHCARDOPERATION where FD_DATATIME between to_date('" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd HH24:mi:ss')";

                if (this.tbx_QBatchNo.Text.Trim() != "")
                {
                    strSql += " and FS_BATCHNO = '" + this.tbx_QBatchNo.Text.Trim() + "'";
                }
                if (this.tbx_QStoveNo.Text.Trim() != "")
                {
                    strSql += " and FS_STOVENO = '" + this.tbx_QStoveNo.Text.Trim() + "'";
                }

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt = new DataTable();
                //ccp.SourceDataTable = dataSet2.Tables[0];
                ccp.SourceDataTable = dataTable2;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                Constant.RefreshAndAutoSize(ultraGrid1);
                
                //ccp.SourceDataTable = this.dataSet2.Tables[0];
                //this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                
                //YGJZJL.PublicComponent.Constant.RefreshAndAutoSize(ultraGrid1);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            this.Cursor = Cursors.Default;
            Constant.WaitingForm.ShowToUser = false;
            Constant.WaitingForm.Close();
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        private void AddNewTechCard()
        {
            try
            {
                if (this.ultraGrid1.Rows.Count == 0)
                    return;
                if (this.ultraGrid1.ActiveRow == null)
                    return;

                string FS_CARDNO = this.ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Text;
                string FS_GP_STOVENO = this.ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Text;
                string FS_GP_STEELTYPE = this.ultraGrid1.ActiveRow.Cells["FS_GP_STEELTYPE"].Text;
                string FS_GP_SPE = this.ultraGrid1.ActiveRow.Cells["FS_GP_SPE"].Text;
                string FN_GP_LEN = this.ultraGrid1.ActiveRow.Cells["FN_GP_LEN"].Text;
                string FS_GP_ORDERNO = this.ultraGrid1.ActiveRow.Cells["FS_GP_ORDERNO"].Text;
                string FN_GP_C = this.ultraGrid1.ActiveRow.Cells["FN_GP_C"].Text;
                string FN_GP_SI = this.ultraGrid1.ActiveRow.Cells["FN_GP_SI"].Text;
                string FN_GP_MN = this.ultraGrid1.ActiveRow.Cells["FN_GP_MN"].Text;
                string FN_GP_S = this.ultraGrid1.ActiveRow.Cells["FN_GP_S"].Text;
                string FN_GP_P = this.ultraGrid1.ActiveRow.Cells["FN_GP_P"].Text;
                string FN_GP_NI = this.ultraGrid1.ActiveRow.Cells["FN_GP_NI"].Text;
                string FN_GP_CR = this.ultraGrid1.ActiveRow.Cells["FN_GP_CR"].Text;
                string FN_GP_CU = this.ultraGrid1.ActiveRow.Cells["FN_GP_CU"].Text;
                string FN_GP_V = this.ultraGrid1.ActiveRow.Cells["FN_GP_V"].Text;
                string FN_GP_MO = this.ultraGrid1.ActiveRow.Cells["FN_GP_MO"].Text;
                string FN_GP_NB = this.ultraGrid1.ActiveRow.Cells["FN_GP_NB"].Text;
                string FN_GP_CEQ = this.ultraGrid1.ActiveRow.Cells["FN_GP_CEQ"].Text;
                string FN_GP_TOTALCOUNT = this.ultraGrid1.ActiveRow.Cells["FN_GP_TOTALCOUNT"].Text;
                string FS_GP_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_GP_MEMO"].Text;
                string FS_GP_JUDGER = this.ultraGrid1.ActiveRow.Cells["FS_GP_JUDGER"].Text;
                string FD_GP_JUDGEDATE = this.ultraGrid1.ActiveRow.Cells["FD_GP_JUDGEDATE"].Text;
                string FN_JJ_WEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JJ_WEIGHT"].Text;
                string FS_JJ_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_JJ_OPERATOR"].Text;
                string FN_GPYS_NUMBER = this.ultraGrid1.ActiveRow.Cells["FN_GPYS_NUMBER"].Text;
                string FS_GPYS_RECEIVER = this.ultraGrid1.ActiveRow.Cells["FS_GPYS_RECEIVER"].Text;
                string FS_ZC_BATCHNO = this.ultraGrid1.ActiveRow.Cells["FS_ZC_BATCHNO"].Text;
                string FS_ZC_ORDERNO = this.ultraGrid1.ActiveRow.Cells["FS_ZC_ORDERNO"].Text;
                string FD_ZC_ENTERDATETIME = this.ultraGrid1.ActiveRow.Cells["FD_ZC_ENTERDATETIME"].Text;
                string FN_ZC_ENTERNUMBER = this.ultraGrid1.ActiveRow.Cells["FN_ZC_ENTERNUMBER"].Text;
                string FS_ZC_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_ZC_OPERATOR"].Text;
                string FN_ZZ_SPEC = this.ultraGrid1.ActiveRow.Cells["FN_ZZ_SPEC"].Text;
                string FD_ZZ_DATE = this.ultraGrid1.ActiveRow.Cells["FD_ZZ_DATE"].Text;
                string FN_ZZ_WASTNUM = this.ultraGrid1.ActiveRow.Cells["FN_ZZ_WASTNUM"].Text;
                string FN_ZZ_NUM = this.ultraGrid1.ActiveRow.Cells["FN_ZZ_NUM"].Text;
                string FS_ZZ_SHIFT = this.ultraGrid1.ActiveRow.Cells["FS_ZZ_SHIFT"].Text;
                string FS_ZZ_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_ZZ_OPERATOR"].Text;
                string FN_JZ_NUM = this.ultraGrid1.ActiveRow.Cells["FN_JZ_NUM"].Text;
                string FN_JZ_WEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_WEIGHT"].Text;
                string FN_JZ_RETURNWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_RETURNWEIGHT"].Text;
                string FN_JZ_WASTWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_WASTWEIGHT"].Text;
                string FS_JZ_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_JZ_OPERATOR"].Text;
                string FS_JZ_CHECKER = this.ultraGrid1.ActiveRow.Cells["FS_JZ_CHECKER"].Text;
                string FS_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_MEMO"].Text;
                string FS_GP_COMPLETEFLAG = this.ultraGrid1.ActiveRow.Cells["FS_GP_COMPLETEFLAG"].Text;
                string FD_ANALYSISTIME = this.ultraGrid1.ActiveRow.Cells["FD_ANALYSISTIME"].Text;
                string FS_ZC_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_ZC_MEMO"].Text;
                string FS_ZZ_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_ZZ_MEMO"].Text;
                string FS_GP_USED = this.ultraGrid1.ActiveRow.Cells["FS_GP_USED"].Text;
                string FS_DJH = this.ultraGrid1.ActiveRow.Cells["FS_DJH"].Text;
                string FS_ADVISESPEC = this.ultraGrid1.ActiveRow.Cells["FS_ADVISESPEC"].Text;
                string FS_FREEZED = this.ultraGrid1.ActiveRow.Cells["FS_FREEZED"].Text;
                string FN_GPYS_WEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_GPYS_WEIGHT"].Text;
                string FS_CHECKED = this.ultraGrid1.ActiveRow.Cells["FS_CHECKED"].Text;
                string FS_GPYS_RESULT = this.ultraGrid1.ActiveRow.Cells["FS_GPYS_RESULT"].Text;
                string FS_QYG = this.ultraGrid1.ActiveRow.Cells["FS_QYG"].Text;
                string FS_YLBB = this.ultraGrid1.ActiveRow.Cells["FS_YLBB"].Text;
                string FN_JZ_THEORYWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_THEORYWEIGHT"].Text;
                string FS_GP_FLOW = this.ultraGrid1.ActiveRow.Cells["FS_GP_FLOW"].Text;
                string FS_GP_PRODUCTOR = this.ultraGrid1.ActiveRow.Cells["FS_GP_PRODUCTOR"].Text;
                string FS_GP_SEND = this.ultraGrid1.ActiveRow.Cells["FS_GP_SEND"].Text;
                string FS_GP_WG = this.ultraGrid1.ActiveRow.Cells["FS_GP_WG"].Text;
                string FS_GP_HTH = this.ultraGrid1.ActiveRow.Cells["FS_GP_HTH"].Text;
                string FS_BC_WEIGHTFLAG = this.ultraGrid1.ActiveRow.Cells["FS_BC_WEIGHTFLAG"].Text;
                string FS_GP_ITEMNO = this.ultraGrid1.ActiveRow.Cells["FS_GP_ITEMNO"].Text;
                string FN_GP_AL = this.ultraGrid1.ActiveRow.Cells["FN_GP_AL"].Text;
                string FN_GP_SN = this.ultraGrid1.ActiveRow.Cells["FN_GP_SN"].Text;
                string FN_GP_TI = this.ultraGrid1.ActiveRow.Cells["FN_GP_TI"].Text;
                string FN_GP_ALS = this.ultraGrid1.ActiveRow.Cells["FN_GP_ALS"].Text;
                string FN_GP_N = this.ultraGrid1.ActiveRow.Cells["FN_GP_N"].Text;
                string FN_GP_AS = this.ultraGrid1.ActiveRow.Cells["FN_GP_AS"].Text;
                string FS_ISVALID = this.ultraGrid1.ActiveRow.Cells["FS_ISVALID"].Text;
                string FN_GP_CA = this.ultraGrid1.ActiveRow.Cells["FN_GP_CA"].Text;
                string FN_GP_B = this.ultraGrid1.ActiveRow.Cells["FN_GP_B"].Text;
                string FN_GP_ALT = this.ultraGrid1.ActiveRow.Cells["FN_GP_ALT"].Text;
                string FN_GP_SB = this.ultraGrid1.ActiveRow.Cells["FN_GP_SB"].Text;
                string FS_GP_SOURCE = this.ultraGrid1.ActiveRow.Cells["FS_GP_SOURCE"].Text;
                string FS_GP_MATCH = this.ultraGrid1.ActiveRow.Cells["FS_GP_MATCH"].Text;
                string FS_TYPE = "0"; //新增
                string FS_OPERATOR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_PC";
                ccp.MethodName = "VindicateTechCard";
                ccp.ServerParams = new object[] { 
                     FS_CARDNO,
                     FS_GP_STOVENO,
                     FS_GP_STEELTYPE,
                     FS_GP_SPE,
                     FN_GP_LEN,
                     FS_GP_ORDERNO,
                     FN_GP_C,
                     FN_GP_SI,
                     FN_GP_MN,
                     FN_GP_S,
                     FN_GP_P,
                     FN_GP_NI,
                     FN_GP_CR,
                     FN_GP_CU,
                     FN_GP_V,
                     FN_GP_MO,
                     FN_GP_NB,
                     FN_GP_CEQ,
                     FN_GP_TOTALCOUNT,
                     FS_GP_MEMO,
                     FS_GP_JUDGER,
                     FD_GP_JUDGEDATE,
                     FN_JJ_WEIGHT,
                     FS_JJ_OPERATOR,
                     FN_GPYS_NUMBER,
                     FS_GPYS_RECEIVER,
                     FS_ZC_BATCHNO,
                     FS_ZC_ORDERNO,
                     FD_ZC_ENTERDATETIME,
                     FN_ZC_ENTERNUMBER,
                     FS_ZC_OPERATOR,
                     FN_ZZ_SPEC,
                     FD_ZZ_DATE,
                     FN_ZZ_WASTNUM,
                     FN_ZZ_NUM,
                     FS_ZZ_SHIFT,
                     FS_ZZ_OPERATOR,
                     FN_JZ_NUM,
                     FN_JZ_WEIGHT,
                     FN_JZ_RETURNWEIGHT,
                     FN_JZ_WASTWEIGHT,
                     FS_JZ_OPERATOR,
                     FS_JZ_CHECKER,
                     FS_MEMO,
                     FS_GP_COMPLETEFLAG,
                     FD_ANALYSISTIME,
                     FS_ZC_MEMO,
                     FS_ZZ_MEMO,
                     FS_GP_USED,
                     FS_DJH,
                     FS_ADVISESPEC,
                     FS_FREEZED,
                     FN_GPYS_WEIGHT,
                     FS_CHECKED,
                     FS_GPYS_RESULT,
                     FS_QYG,
                     FS_YLBB,
                     FN_JZ_THEORYWEIGHT,
                     FS_GP_FLOW,
                     FS_GP_PRODUCTOR,
                     FS_GP_SEND,
                     FS_GP_WG,
                     FS_GP_HTH,
                     FS_BC_WEIGHTFLAG,
                     FS_GP_ITEMNO,
                     FN_GP_AL,
                     FN_GP_SN,
                     FN_GP_TI,
                     FN_GP_ALS,
                     FN_GP_N,
                     FN_GP_AS,
                     FS_ISVALID,
                     FN_GP_CA,
                     FN_GP_B,
                     FN_GP_ALT,
                     FN_GP_SB,
                     FS_GP_SOURCE,
                     FS_GP_MATCH,
                     FS_TYPE,
                     FS_OPERATOR};
                ccp.IfShowErrMsg = true;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnCode == 0)
                {
                    MessageBox.Show("新增成功！");
                    this.tbx_QStoveNo.Text = this.ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Text.Trim();
                    QueryTechCard();
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        /// <summary>
        /// 修改卡片
        /// </summary>
        private void UpdateTechCard()
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
                        string FS_CARDNO = this.ultraGrid1.Rows[i].Cells["FS_CARDNO"].Text;
                        string FS_GP_STOVENO = this.ultraGrid1.Rows[i].Cells["FS_GP_STOVENO"].Text;
                        string FS_GP_STEELTYPE = this.ultraGrid1.Rows[i].Cells["FS_GP_STEELTYPE"].Text;
                        string FS_GP_SPE = this.ultraGrid1.Rows[i].Cells["FS_GP_SPE"].Text;
                        string FN_GP_LEN = this.ultraGrid1.Rows[i].Cells["FN_GP_LEN"].Text;
                        string FS_GP_ORDERNO = this.ultraGrid1.Rows[i].Cells["FS_GP_ORDERNO"].Text;
                        string FN_GP_C = this.ultraGrid1.Rows[i].Cells["FN_GP_C"].Text;
                        string FN_GP_SI = this.ultraGrid1.Rows[i].Cells["FN_GP_SI"].Text;
                        string FN_GP_MN = this.ultraGrid1.Rows[i].Cells["FN_GP_MN"].Text;
                        string FN_GP_S = this.ultraGrid1.Rows[i].Cells["FN_GP_S"].Text;
                        string FN_GP_P = this.ultraGrid1.Rows[i].Cells["FN_GP_P"].Text;
                        string FN_GP_NI = this.ultraGrid1.Rows[i].Cells["FN_GP_NI"].Text;
                        string FN_GP_CR = this.ultraGrid1.Rows[i].Cells["FN_GP_CR"].Text;
                        string FN_GP_CU = this.ultraGrid1.Rows[i].Cells["FN_GP_CU"].Text;
                        string FN_GP_V = this.ultraGrid1.Rows[i].Cells["FN_GP_V"].Text;
                        string FN_GP_MO = this.ultraGrid1.Rows[i].Cells["FN_GP_MO"].Text;
                        string FN_GP_NB = this.ultraGrid1.Rows[i].Cells["FN_GP_NB"].Text;
                        string FN_GP_CEQ = this.ultraGrid1.Rows[i].Cells["FN_GP_CEQ"].Text;
                        string FN_GP_TOTALCOUNT = this.ultraGrid1.Rows[i].Cells["FN_GP_TOTALCOUNT"].Text;
                        string FS_GP_MEMO = this.ultraGrid1.Rows[i].Cells["FS_GP_MEMO"].Text;
                        string FS_GP_JUDGER = this.ultraGrid1.Rows[i].Cells["FS_GP_JUDGER"].Text;
                        string FD_GP_JUDGEDATE = this.ultraGrid1.Rows[i].Cells["FD_GP_JUDGEDATE"].Text;
                        string FN_JJ_WEIGHT = this.ultraGrid1.Rows[i].Cells["FN_JJ_WEIGHT"].Text;
                        string FS_JJ_OPERATOR = this.ultraGrid1.Rows[i].Cells["FS_JJ_OPERATOR"].Text;
                        string FN_GPYS_NUMBER = this.ultraGrid1.Rows[i].Cells["FN_GPYS_NUMBER"].Text;
                        string FS_GPYS_RECEIVER = this.ultraGrid1.Rows[i].Cells["FS_GPYS_RECEIVER"].Text;
                        string FS_ZC_BATCHNO = this.ultraGrid1.Rows[i].Cells["FS_ZC_BATCHNO"].Text;
                        string FS_ZC_ORDERNO = this.ultraGrid1.Rows[i].Cells["FS_ZC_ORDERNO"].Text;
                        string FD_ZC_ENTERDATETIME = this.ultraGrid1.Rows[i].Cells["FD_ZC_ENTERDATETIME"].Text;
                        string FN_ZC_ENTERNUMBER = this.ultraGrid1.Rows[i].Cells["FN_ZC_ENTERNUMBER"].Text;
                        string FS_ZC_OPERATOR = this.ultraGrid1.Rows[i].Cells["FS_ZC_OPERATOR"].Text;
                        string FN_ZZ_SPEC = this.ultraGrid1.Rows[i].Cells["FN_ZZ_SPEC"].Text;
                        string FD_ZZ_DATE = this.ultraGrid1.Rows[i].Cells["FD_ZZ_DATE"].Text;
                        string FN_ZZ_WASTNUM = this.ultraGrid1.Rows[i].Cells["FN_ZZ_WASTNUM"].Text;
                        string FN_ZZ_NUM = this.ultraGrid1.Rows[i].Cells["FN_ZZ_NUM"].Text;
                        string FS_ZZ_SHIFT = this.ultraGrid1.Rows[i].Cells["FS_ZZ_SHIFT"].Text;
                        string FS_ZZ_OPERATOR = this.ultraGrid1.Rows[i].Cells["FS_ZZ_OPERATOR"].Text;
                        string FN_JZ_NUM = this.ultraGrid1.Rows[i].Cells["FN_JZ_NUM"].Text;
                        string FN_JZ_WEIGHT = this.ultraGrid1.Rows[i].Cells["FN_JZ_WEIGHT"].Text;
                        string FN_JZ_RETURNWEIGHT = this.ultraGrid1.Rows[i].Cells["FN_JZ_RETURNWEIGHT"].Text;
                        string FN_JZ_WASTWEIGHT = this.ultraGrid1.Rows[i].Cells["FN_JZ_WASTWEIGHT"].Text;
                        string FS_JZ_OPERATOR = this.ultraGrid1.Rows[i].Cells["FS_JZ_OPERATOR"].Text;
                        string FS_JZ_CHECKER = this.ultraGrid1.Rows[i].Cells["FS_JZ_CHECKER"].Text;
                        string FS_MEMO = this.ultraGrid1.Rows[i].Cells["FS_MEMO"].Text;
                        string FS_GP_COMPLETEFLAG = this.ultraGrid1.Rows[i].Cells["FS_GP_COMPLETEFLAG"].Text;
                        string FD_ANALYSISTIME = this.ultraGrid1.Rows[i].Cells["FD_ANALYSISTIME"].Text;
                        string FS_ZC_MEMO = this.ultraGrid1.Rows[i].Cells["FS_ZC_MEMO"].Text;
                        string FS_ZZ_MEMO = this.ultraGrid1.Rows[i].Cells["FS_ZZ_MEMO"].Text;
                        string FS_GP_USED = this.ultraGrid1.Rows[i].Cells["FS_GP_USED"].Text;
                        string FS_DJH = this.ultraGrid1.Rows[i].Cells["FS_DJH"].Text;
                        string FS_ADVISESPEC = this.ultraGrid1.Rows[i].Cells["FS_ADVISESPEC"].Text;
                        string FS_FREEZED = this.ultraGrid1.Rows[i].Cells["FS_FREEZED"].Text;
                        string FN_GPYS_WEIGHT = this.ultraGrid1.Rows[i].Cells["FN_GPYS_WEIGHT"].Text;
                        string FS_CHECKED = this.ultraGrid1.Rows[i].Cells["FS_CHECKED"].Text;
                        string FS_GPYS_RESULT = this.ultraGrid1.Rows[i].Cells["FS_GPYS_RESULT"].Text;
                        string FS_QYG = this.ultraGrid1.Rows[i].Cells["FS_QYG"].Text;
                        string FS_YLBB = this.ultraGrid1.Rows[i].Cells["FS_YLBB"].Text;
                        string FN_JZ_THEORYWEIGHT = this.ultraGrid1.Rows[i].Cells["FN_JZ_THEORYWEIGHT"].Text;
                        string FS_GP_FLOW = this.ultraGrid1.Rows[i].Cells["FS_GP_FLOW"].Text;
                        string FS_GP_PRODUCTOR = this.ultraGrid1.Rows[i].Cells["FS_GP_PRODUCTOR"].Text;
                        string FS_GP_SEND = this.ultraGrid1.Rows[i].Cells["FS_GP_SEND"].Text;
                        string FS_GP_WG = this.ultraGrid1.Rows[i].Cells["FS_GP_WG"].Text;
                        string FS_GP_HTH = this.ultraGrid1.Rows[i].Cells["FS_GP_HTH"].Text;
                        string FS_BC_WEIGHTFLAG = this.ultraGrid1.Rows[i].Cells["FS_BC_WEIGHTFLAG"].Text;
                        string FS_GP_ITEMNO = this.ultraGrid1.Rows[i].Cells["FS_GP_ITEMNO"].Text;
                        string FN_GP_AL = this.ultraGrid1.Rows[i].Cells["FN_GP_AL"].Text;
                        string FN_GP_SN = this.ultraGrid1.Rows[i].Cells["FN_GP_SN"].Text;
                        string FN_GP_TI = this.ultraGrid1.Rows[i].Cells["FN_GP_TI"].Text;
                        string FN_GP_ALS = this.ultraGrid1.Rows[i].Cells["FN_GP_ALS"].Text;
                        string FN_GP_N = this.ultraGrid1.Rows[i].Cells["FN_GP_N"].Text;
                        string FN_GP_AS = this.ultraGrid1.Rows[i].Cells["FN_GP_AS"].Text;
                        string FS_ISVALID = this.ultraGrid1.Rows[i].Cells["FS_ISVALID"].Text;
                        string FN_GP_CA = this.ultraGrid1.Rows[i].Cells["FN_GP_CA"].Text;
                        string FN_GP_B = this.ultraGrid1.Rows[i].Cells["FN_GP_B"].Text;
                        string FN_GP_ALT = this.ultraGrid1.Rows[i].Cells["FN_GP_ALT"].Text;
                        string FN_GP_SB = this.ultraGrid1.Rows[i].Cells["FN_GP_SB"].Text;
                        string FS_GP_SOURCE = this.ultraGrid1.Rows[i].Cells["FS_GP_SOURCE"].Text;
                        string FS_GP_MATCH = this.ultraGrid1.Rows[i].Cells["FS_GP_MATCH"].Text;
                        string FS_TYPE = "1"; //修改
                        string FS_OPERATOR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        string strIP = getIPAddress();
                        string strMAC = getMACAddress();
                        
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_PC";
                        ccp.MethodName = "VindicateTechCard";
                        ccp.ServerParams = new object[] { 
                     FS_CARDNO,
                     FS_GP_STOVENO,
                     FS_GP_STEELTYPE,
                     FS_GP_SPE,
                     FN_GP_LEN,
                     FS_GP_ORDERNO,
                     FN_GP_C,
                     FN_GP_SI,
                     FN_GP_MN,
                     FN_GP_S,
                     FN_GP_P,
                     FN_GP_NI,
                     FN_GP_CR,
                     FN_GP_CU,
                     FN_GP_V,
                     FN_GP_MO,
                     FN_GP_NB,
                     FN_GP_CEQ,
                     FN_GP_TOTALCOUNT,
                     FS_GP_MEMO,
                     FS_GP_JUDGER,
                     FD_GP_JUDGEDATE,
                     FN_JJ_WEIGHT,
                     FS_JJ_OPERATOR,
                     FN_GPYS_NUMBER,
                     FS_GPYS_RECEIVER,
                     FS_ZC_BATCHNO,
                     FS_ZC_ORDERNO,
                     FD_ZC_ENTERDATETIME,
                     FN_ZC_ENTERNUMBER,
                     FS_ZC_OPERATOR,
                     FN_ZZ_SPEC,
                     FD_ZZ_DATE,
                     FN_ZZ_WASTNUM,
                     FN_ZZ_NUM,
                     FS_ZZ_SHIFT,
                     FS_ZZ_OPERATOR,
                     FN_JZ_NUM,
                     FN_JZ_WEIGHT,
                     FN_JZ_RETURNWEIGHT,
                     FN_JZ_WASTWEIGHT,
                     FS_JZ_OPERATOR,
                     FS_JZ_CHECKER,
                     FS_MEMO,
                     FS_GP_COMPLETEFLAG,
                     FD_ANALYSISTIME,
                     FS_ZC_MEMO,
                     FS_ZZ_MEMO,
                     FS_GP_USED,
                     FS_DJH,
                     FS_ADVISESPEC,
                     FS_FREEZED,
                     FN_GPYS_WEIGHT,
                     FS_CHECKED,
                     FS_GPYS_RESULT,
                     FS_QYG,
                     FS_YLBB,
                     FN_JZ_THEORYWEIGHT,
                     FS_GP_FLOW,
                     FS_GP_PRODUCTOR,
                     FS_GP_SEND,
                     FS_GP_WG,
                     FS_GP_HTH,
                     FS_BC_WEIGHTFLAG,
                     FS_GP_ITEMNO,
                     FN_GP_AL,
                     FN_GP_SN,
                     FN_GP_TI,
                     FN_GP_ALS,
                     FN_GP_N,
                     FN_GP_AS,
                     FS_ISVALID,
                     FN_GP_CA,
                     FN_GP_B,
                     FN_GP_ALT,
                     FN_GP_SB,
                     FS_GP_SOURCE,
                     FS_GP_MATCH,
                     FS_TYPE,
                     FS_OPERATOR,strIP,strMAC};
                        ccp.IfShowErrMsg = true;
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        if (ccp.ReturnCode != 0)
                        {
                            MessageBox.Show(ccp.ReturnObject.ToString());
                            return;
                        }
                    }
                    MessageBox.Show("修改成功！");
                    QueryTechCard();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 删除卡片
        /// </summary>
        private void DeleteTechCard()
        {
            try
            {
                DialogResult result = MessageBox.Show(this, "删除的卡片将无法修复，是否继续删除卡片？", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (this.ultraGrid1.Rows.Count == 0)
                        return;
                    if (this.ultraGrid1.ActiveRow == null)
                        return;

                    string FS_CARDNO = this.ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Text;
                    string FS_GP_STOVENO = this.ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Text;
                    string FS_GP_STEELTYPE = this.ultraGrid1.ActiveRow.Cells["FS_GP_STEELTYPE"].Text;
                    string FS_GP_SPE = this.ultraGrid1.ActiveRow.Cells["FS_GP_SPE"].Text;
                    string FN_GP_LEN = this.ultraGrid1.ActiveRow.Cells["FN_GP_LEN"].Text;
                    string FS_GP_ORDERNO = this.ultraGrid1.ActiveRow.Cells["FS_GP_ORDERNO"].Text;
                    string FN_GP_C = this.ultraGrid1.ActiveRow.Cells["FN_GP_C"].Text;
                    string FN_GP_SI = this.ultraGrid1.ActiveRow.Cells["FN_GP_SI"].Text;
                    string FN_GP_MN = this.ultraGrid1.ActiveRow.Cells["FN_GP_MN"].Text;
                    string FN_GP_S = this.ultraGrid1.ActiveRow.Cells["FN_GP_S"].Text;
                    string FN_GP_P = this.ultraGrid1.ActiveRow.Cells["FN_GP_P"].Text;
                    string FN_GP_NI = this.ultraGrid1.ActiveRow.Cells["FN_GP_NI"].Text;
                    string FN_GP_CR = this.ultraGrid1.ActiveRow.Cells["FN_GP_CR"].Text;
                    string FN_GP_CU = this.ultraGrid1.ActiveRow.Cells["FN_GP_CU"].Text;
                    string FN_GP_V = this.ultraGrid1.ActiveRow.Cells["FN_GP_V"].Text;
                    string FN_GP_MO = this.ultraGrid1.ActiveRow.Cells["FN_GP_MO"].Text;
                    string FN_GP_NB = this.ultraGrid1.ActiveRow.Cells["FN_GP_NB"].Text;
                    string FN_GP_CEQ = this.ultraGrid1.ActiveRow.Cells["FN_GP_CEQ"].Text;
                    string FN_GP_TOTALCOUNT = this.ultraGrid1.ActiveRow.Cells["FN_GP_TOTALCOUNT"].Text;
                    string FS_GP_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_GP_MEMO"].Text;
                    string FS_GP_JUDGER = this.ultraGrid1.ActiveRow.Cells["FS_GP_JUDGER"].Text;
                    string FD_GP_JUDGEDATE = this.ultraGrid1.ActiveRow.Cells["FD_GP_JUDGEDATE"].Text;
                    string FN_JJ_WEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JJ_WEIGHT"].Text;
                    string FS_JJ_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_JJ_OPERATOR"].Text;
                    string FN_GPYS_NUMBER = this.ultraGrid1.ActiveRow.Cells["FN_GPYS_NUMBER"].Text;
                    string FS_GPYS_RECEIVER = this.ultraGrid1.ActiveRow.Cells["FS_GPYS_RECEIVER"].Text;
                    string FS_ZC_BATCHNO = this.ultraGrid1.ActiveRow.Cells["FS_ZC_BATCHNO"].Text;
                    string FS_ZC_ORDERNO = this.ultraGrid1.ActiveRow.Cells["FS_ZC_ORDERNO"].Text;
                    string FD_ZC_ENTERDATETIME = this.ultraGrid1.ActiveRow.Cells["FD_ZC_ENTERDATETIME"].Text;
                    string FN_ZC_ENTERNUMBER = this.ultraGrid1.ActiveRow.Cells["FN_ZC_ENTERNUMBER"].Text;
                    string FS_ZC_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_ZC_OPERATOR"].Text;
                    string FN_ZZ_SPEC = this.ultraGrid1.ActiveRow.Cells["FN_ZZ_SPEC"].Text;
                    string FD_ZZ_DATE = this.ultraGrid1.ActiveRow.Cells["FD_ZZ_DATE"].Text;
                    string FN_ZZ_WASTNUM = this.ultraGrid1.ActiveRow.Cells["FN_ZZ_WASTNUM"].Text;
                    string FN_ZZ_NUM = this.ultraGrid1.ActiveRow.Cells["FN_ZZ_NUM"].Text;
                    string FS_ZZ_SHIFT = this.ultraGrid1.ActiveRow.Cells["FS_ZZ_SHIFT"].Text;
                    string FS_ZZ_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_ZZ_OPERATOR"].Text;
                    string FN_JZ_NUM = this.ultraGrid1.ActiveRow.Cells["FN_JZ_NUM"].Text;
                    string FN_JZ_WEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_WEIGHT"].Text;
                    string FN_JZ_RETURNWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_RETURNWEIGHT"].Text;
                    string FN_JZ_WASTWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_WASTWEIGHT"].Text;
                    string FS_JZ_OPERATOR = this.ultraGrid1.ActiveRow.Cells["FS_JZ_OPERATOR"].Text;
                    string FS_JZ_CHECKER = this.ultraGrid1.ActiveRow.Cells["FS_JZ_CHECKER"].Text;
                    string FS_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_MEMO"].Text;
                    string FS_GP_COMPLETEFLAG = this.ultraGrid1.ActiveRow.Cells["FS_GP_COMPLETEFLAG"].Text;
                    string FD_ANALYSISTIME = this.ultraGrid1.ActiveRow.Cells["FD_ANALYSISTIME"].Text;
                    string FS_ZC_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_ZC_MEMO"].Text;
                    string FS_ZZ_MEMO = this.ultraGrid1.ActiveRow.Cells["FS_ZZ_MEMO"].Text;
                    string FS_GP_USED = this.ultraGrid1.ActiveRow.Cells["FS_GP_USED"].Text;
                    string FS_DJH = this.ultraGrid1.ActiveRow.Cells["FS_DJH"].Text;
                    string FS_ADVISESPEC = this.ultraGrid1.ActiveRow.Cells["FS_ADVISESPEC"].Text;
                    string FS_FREEZED = this.ultraGrid1.ActiveRow.Cells["FS_FREEZED"].Text;
                    string FN_GPYS_WEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_GPYS_WEIGHT"].Text;
                    string FS_CHECKED = this.ultraGrid1.ActiveRow.Cells["FS_CHECKED"].Text;
                    string FS_GPYS_RESULT = this.ultraGrid1.ActiveRow.Cells["FS_GPYS_RESULT"].Text;
                    string FS_QYG = this.ultraGrid1.ActiveRow.Cells["FS_QYG"].Text;
                    string FS_YLBB = this.ultraGrid1.ActiveRow.Cells["FS_YLBB"].Text;
                    string FN_JZ_THEORYWEIGHT = this.ultraGrid1.ActiveRow.Cells["FN_JZ_THEORYWEIGHT"].Text;
                    string FS_GP_FLOW = this.ultraGrid1.ActiveRow.Cells["FS_GP_FLOW"].Text;
                    string FS_GP_PRODUCTOR = this.ultraGrid1.ActiveRow.Cells["FS_GP_PRODUCTOR"].Text;
                    string FS_GP_SEND = this.ultraGrid1.ActiveRow.Cells["FS_GP_SEND"].Text;
                    string FS_GP_WG = this.ultraGrid1.ActiveRow.Cells["FS_GP_WG"].Text;
                    string FS_GP_HTH = this.ultraGrid1.ActiveRow.Cells["FS_GP_HTH"].Text;
                    string FS_BC_WEIGHTFLAG = this.ultraGrid1.ActiveRow.Cells["FS_BC_WEIGHTFLAG"].Text;
                    string FS_GP_ITEMNO = this.ultraGrid1.ActiveRow.Cells["FS_GP_ITEMNO"].Text;
                    string FN_GP_AL = this.ultraGrid1.ActiveRow.Cells["FN_GP_AL"].Text;
                    string FN_GP_SN = this.ultraGrid1.ActiveRow.Cells["FN_GP_SN"].Text;
                    string FN_GP_TI = this.ultraGrid1.ActiveRow.Cells["FN_GP_TI"].Text;
                    string FN_GP_ALS = this.ultraGrid1.ActiveRow.Cells["FN_GP_ALS"].Text;
                    string FN_GP_N = this.ultraGrid1.ActiveRow.Cells["FN_GP_N"].Text;
                    string FN_GP_AS = this.ultraGrid1.ActiveRow.Cells["FN_GP_AS"].Text;
                    string FS_ISVALID = this.ultraGrid1.ActiveRow.Cells["FS_ISVALID"].Text;
                    string FN_GP_CA = this.ultraGrid1.ActiveRow.Cells["FN_GP_CA"].Text;
                    string FN_GP_B = this.ultraGrid1.ActiveRow.Cells["FN_GP_B"].Text;
                    string FN_GP_ALT = this.ultraGrid1.ActiveRow.Cells["FN_GP_ALT"].Text;
                    string FN_GP_SB = this.ultraGrid1.ActiveRow.Cells["FN_GP_SB"].Text;
                    string FS_GP_SOURCE = this.ultraGrid1.ActiveRow.Cells["FS_GP_SOURCE"].Text;
                    string FS_GP_MATCH = this.ultraGrid1.ActiveRow.Cells["FS_GP_MATCH"].Text;
                    string FS_TYPE = "2"; //delete
                    string FS_OPERATOR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    string strIP = getIPAddress();
                    string strMAC = getMACAddress();

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.squarebillettransfer.BilletInfo_PC";
                    ccp.MethodName = "VindicateTechCard";
                    ccp.ServerParams = new object[] { 
                     FS_CARDNO,
                     FS_GP_STOVENO,
                     FS_GP_STEELTYPE,
                     FS_GP_SPE,
                     FN_GP_LEN,
                     FS_GP_ORDERNO,
                     FN_GP_C,
                     FN_GP_SI,
                     FN_GP_MN,
                     FN_GP_S,
                     FN_GP_P,
                     FN_GP_NI,
                     FN_GP_CR,
                     FN_GP_CU,
                     FN_GP_V,
                     FN_GP_MO,
                     FN_GP_NB,
                     FN_GP_CEQ,
                     FN_GP_TOTALCOUNT,
                     FS_GP_MEMO,
                     FS_GP_JUDGER,
                     FD_GP_JUDGEDATE,
                     FN_JJ_WEIGHT,
                     FS_JJ_OPERATOR,
                     FN_GPYS_NUMBER,
                     FS_GPYS_RECEIVER,
                     FS_ZC_BATCHNO,
                     FS_ZC_ORDERNO,
                     FD_ZC_ENTERDATETIME,
                     FN_ZC_ENTERNUMBER,
                     FS_ZC_OPERATOR,
                     FN_ZZ_SPEC,
                     FD_ZZ_DATE,
                     FN_ZZ_WASTNUM,
                     FN_ZZ_NUM,
                     FS_ZZ_SHIFT,
                     FS_ZZ_OPERATOR,
                     FN_JZ_NUM,
                     FN_JZ_WEIGHT,
                     FN_JZ_RETURNWEIGHT,
                     FN_JZ_WASTWEIGHT,
                     FS_JZ_OPERATOR,
                     FS_JZ_CHECKER,
                     FS_MEMO,
                     FS_GP_COMPLETEFLAG,
                     FD_ANALYSISTIME,
                     FS_ZC_MEMO,
                     FS_ZZ_MEMO,
                     FS_GP_USED,
                     FS_DJH,
                     FS_ADVISESPEC,
                     FS_FREEZED,
                     FN_GPYS_WEIGHT,
                     FS_CHECKED,
                     FS_GPYS_RESULT,
                     FS_QYG,
                     FS_YLBB,
                     FN_JZ_THEORYWEIGHT,
                     FS_GP_FLOW,
                     FS_GP_PRODUCTOR,
                     FS_GP_SEND,
                     FS_GP_WG,
                     FS_GP_HTH,
                     FS_BC_WEIGHTFLAG,
                     FS_GP_ITEMNO,
                     FN_GP_AL,
                     FN_GP_SN,
                     FN_GP_TI,
                     FN_GP_ALS,
                     FN_GP_N,
                     FN_GP_AS,
                     FS_ISVALID,
                     FN_GP_CA,
                     FN_GP_B,
                     FN_GP_ALT,
                     FN_GP_SB,
                     FS_GP_SOURCE,
                     FS_GP_MATCH,
                     FS_TYPE,
                     FS_OPERATOR,strIP,strMAC};
                    ccp.IfShowErrMsg = true;
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ccp.ReturnCode == 0)
                    {
                        MessageBox.Show("删除成功！");
                        this.tbx_QStoveNo.Text = this.ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Text.Trim();
                        QueryTechCard();
                    }

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (e.Tool.Key == "查询")
            {
                QueryTechCard();
            }
            if (e.Tool.Key == "新增")
            {
                this.AddNewTechCard();
            }
            if (e.Tool.Key == "修改")
            {
                this.UpdateTechCard();
            }
            if (e.Tool.Key == "删除")
            {
                this.DeleteTechCard();
            }
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string getIPAddress()
        {
            IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
            string ip = ipEntry.AddressList[0].ToString();
            return ip;
        }

        /// <summary>
        /// 获取MAC地址
        /// </summary>
        /// <returns></returns>
        protected string getMACAddress()
        {
            string mac = "";
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return mac;
        }


        private void TechCardUpdateFrm_Load(object sender, EventArgs e)
        {
            //InitTechCardTable();
        }
    }
}
