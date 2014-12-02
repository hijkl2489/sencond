using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using System.IO;
using System.Diagnostics;
using Infragistics.Win.UltraWinGrid;
using System.Collections;

namespace YGJZJL.Car
{
    public partial class MeasureKouZhaNew : FrmBase
    {
        string strZYBH = "";
        LimitQueryTime limitQueryTime = new LimitQueryTime();//为判断时间区间设定的变量
        DateTime beginTime;
        DateTime endTime;
        bool decisionResult;

        public MeasureKouZhaNew()
        {
            InitializeComponent();
            
        }

        private void MeasureDataOneQueryNew_Load(object sender, EventArgs e)
        {
            //DataGridInit();
            QueryYCBData();
            try
            {
                this.BeginTime.Value = DateTime.Today;
                this.EndTime.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            try
            {

                //string strKey =this.CustomInfo = "1";
                //string strKey = this.CustomInfo.ToUpper();
                switch (CustomInfo)
                {//this.SetToolButtonVisible("判定不合格", false);
                    case "A"://取样
                        //ultraToolbarsManager1.Toolbars[0].Tools["卸货确认"].SharedProps.Visible = false;
                        //ultraToolbarsManager1.Toolbars[0].Tools["取消卸货"].SharedProps.Visible = false;
                        //ultraToolbarsManager1.Toolbars[0].Tools["扣渣保存"].SharedProps.Visible = false;
                        this.ultraToolbarsManager1.Toolbars[0].Visible = true;
                        this.SetToolButtonVisible("卸货确认", false);
                        this.SetToolButtonVisible("取消卸货", false);
                        this.SetToolButtonVisible("扣渣保存", false);
                        this.SetToolButtonVisible("撤销扣渣", false);
                        this.SetToolButtonVisible("查询日期", false);
                        this.SetToolButtonVisible("卸货类型", false);
                        this.cmbChange.Text = "";
                        this.cmbChange.Visible = false;
                        this.ultraGroupBox2.Visible = true;
                        this.ultraGroupBox1.Visible = false;
                        this.ultraGroupBox2.Dock = DockStyle.Fill;
                        this.txtXHR.Visible = false;
                        this.txtXHSJ.Visible = false;
                        this.txtXHQR.Visible = false;
                        this.txtKHJZ.Visible = false;
                        this.txtKZQR.Visible = false;
                        this.txtKZR.Visible = false;
                        this.txtYKBL.Visible = false;
                        this.txtYKL.Visible = false;
                        this.label22.Visible = false;
                        this.label23.Visible = false;
                        this.label24.Visible = false;
                        this.label14.Visible = false;
                        this.label15.Visible = false;
                        this.label16.Visible = false;
                        this.label18.Visible = false;
                        this.label25.Visible = false;
                        this.label3.Visible = false;
                        this.label4.Visible = false;
                        this.panel3.Visible = false;
                        this.cmbUnloadStyle.Visible = false;
                        break;
                    case "B"://卸货
                        ultraToolbarsManager1.Toolbars[0].Tools["撤销取样"].SharedProps.Visible = false;
                        ultraToolbarsManager1.Toolbars[0].Tools["取样确认"].SharedProps.Visible = false;
                        ultraToolbarsManager1.Toolbars[0].Tools["扣渣保存"].SharedProps.Visible = false;
                        ultraToolbarsManager1.Toolbars[0].Tools["撤销扣渣"].SharedProps.Visible = false;

                        this.txtQYR.Visible = false;
                        this.txtQYSJ.Visible = false;
                        this.txtConfirm.Visible = false;
                        this.txtKHJZ.Visible = false;
                        this.txtKZQR.Visible = false;
                        this.txtKZR.Visible = false;
                        this.txtYKBL.Visible = false;
                        this.txtYKL.Visible = false;
                        this.label19.Visible = false;
                        this.label20.Visible = false;
                        this.label21.Visible = false;
                        this.label14.Visible = false;
                        this.label15.Visible = false;
                        this.label16.Visible = false;
                        this.label18.Visible = false;
                        this.label25.Visible = false;
                        this.label3.Visible = false;
                        this.label4.Visible = false;
                        this.panel3.Visible = false;
                        break;
                    case "C"://扣渣
                        ultraToolbarsManager1.Toolbars[0].Tools["撤销取样"].SharedProps.Visible = false;
                        ultraToolbarsManager1.Toolbars[0].Tools["取样确认"].SharedProps.Visible = false;
                        //ultraToolbarsManager1.Toolbars[0].Tools["查询日期"].SharedProps.Visible=false;
                        ultraToolbarsManager1.Toolbars[0].Tools["扣渣保存"].SharedProps.Visible = false;
                        ultraToolbarsManager1.Toolbars[0].Tools["撤销扣渣"].SharedProps.Visible = false;
                        //
                        SetButtonVisible();
                        this.txtKZR.Enabled = false;
                        this.txtYKBL.Enabled = false;
                        this.txtYKL.Enabled = true;
                        //ultraGrid3.DisplayLayout.Bands[0].Columns["choosen"].Hidden = true;
                        //ultraGrid1.DisplayLayout.Bands[0].Columns["choosen"].Hidden = true;
                        this.rdCancelAll.Visible = false;
                        this.rdChooseAll.Visible = false;
                        
                        //this.cmbChange.Text = "";
                        //this.cmbChange.Visible = false;
                        this.ultraGroupBox2.Visible = true;
                        this.ultraGroupBox1.Visible = false;
                        this.ultraGroupBox2.Dock = DockStyle.Fill;
                        //radioButton1.Visible = false;
                        //radioButton2.Visible = false;
                        this.radiobl.Visible = false;
                        this.radiozl.Visible = false;
                        this.txtYKBL.Enabled = false;
                        this.txtYKL.Enabled = false;
                        
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void SetButtonVisible()
        {
            this.txtQYR.Visible = false;
            this.txtQYSJ.Visible = false;
            this.txtConfirm.Visible = false;
            this.txtXHR.Visible = false;
            this.txtXHSJ.Visible = false;
            this.txtXHQR.Visible = false;
            this.label19.Visible = false;
            this.label20.Visible = false;
            this.label21.Visible = false;
            this.label22.Visible = false;
            this.label23.Visible = false;
            this.label24.Visible = false;
        }
        private void SetToolButtonVisible(string strKey, bool Visible)
        {
            try
            {
                this.ultraToolbarsManager1.Tools[strKey].SharedProps.Visible = Visible;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /////// <summary>
        /////// 网格显示设置
        /////// </summary>
        ////private void DataGridInit()
        ////{
        ////    //行编辑器显示序号
        ////    ultraGrid3.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
        ////    ultraGrid3.DisplayLayout.Override.RowSelectorWidth = 25;
        ////    ultraGrid3.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

        ////    for (int i = 1; i <= ultraGrid3.DisplayLayout.Bands[0].Columns.Count-2 ; i++)
        ////    {
        ////        ultraGrid3.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
        ////        ultraGrid3.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
        ////    }
        ////}
        /// <summary>
        /// 查询一次计量绑定数据
        /// </summary>
        private void QueryYCBData()
        {
            string strBeginTime = this.dateBegin.Value.ToString("yyyy-MM-dd HH:mm");
            string strEndTime = this.dateEnd.Value.ToString("yyyy-MM-dd HH:mm");
            if (this.cmbChange.Text == "没有净重")
            {
                this.ultraGroupBox2.Visible = false;
                this.ultraGroupBox1.Visible = true;
                this.ultraGroupBox1.Dock = DockStyle.Fill;
                string sql = "select 'false' choosen, A.FS_WEIGHTNO,A.FS_PLANCODE,A.FS_CONTRACTNO,A.FS_CONTRACTITEM,A.FS_STOVENO,Provider_BHTOMC(A.FS_Provider)FS_Provider,A.FS_BZ,A.FS_UNLOADMEMO,"
                + "A.FN_COUNT,A.FS_CARDNUMBER,A.FS_CARNO,A.FS_MATERIAL,A.FS_MATERIALNAME,A.FS_Sender,FHDW_BHTOMC(A.FS_Sender)FS_FHDW,"
                + "A.FS_SENDERSTORE,A.FS_TRANSNO,CYDW_BHTOMC(A.FS_TRANSNO)FS_CYDW,A.FS_RECEIVER,SHDW_BHTOMC(A.FS_RECEIVER)FS_SHDW,"
                + "A.FS_RECEIVERFACTORY,A.FS_WEIGHTTYPE,LX_BHTOMC(A.FS_WEIGHTTYPE)FS_LX,A.FS_POUNDTYPE,A.FN_SENDGROSSWEIGHT,"
                + "A.FN_SENDTAREWEIGHT,A.FN_SENDNETWEIGHT,to_char(A.FN_WEIGHT,'FM999.000') FN_WEIGHT,A.FS_POUND,A.FS_WEIGHTER,"
                + "to_char(A.FD_WEIGHTTIME,'yyyy-MM-dd HH24:mi:ss')as FD_WEIGHTTIME,A.FS_SHIFT,A.FS_TERM,";
                sql += "FS_LOADFLAG,FS_SAMPLEPERSON,FS_YCSFYC,"
                + "to_char(FD_SAMPLETIME,'yyyy-MM-dd HH24:mi:ss')as FD_SAMPLETIME,FS_SAMPLEPLACE,"
                + "decode(FS_SAMPLEFLAG,'0','','1','√')as FS_SAMPLEFLAG, FS_UNLOADPERSON,to_char(FD_UNLOADTIME,'yyyy-MM-dd HH24:mi:ss')as FD_UNLOADTIME,decode(fs_unloadflag,'0','','1','卸货确认','2','退货过磅','3','退货不过磅','4','复磅')as fs_unloadflag, "
                + "FS_UNLOADPLACE,FS_CHECKPERSON,to_char(FD_CHECKTIME,'yyyy-MM-dd HH24:mi:ss')as FD_CHECKTIME,"
                + "FS_CHECKPLACE,FS_CHECKFLAG,FS_IFSAMPLING,FS_IFACCEPT,FS_DRIVERNAME,FS_DRIVERIDCARD,FS_YKL,"
                + "FS_REWEIGHTFLAG,to_char(FD_REWEIGHTTIME,'yyyy-MM-dd HH24:mi:ss')as FD_REWEIGHTTIME,"
                + "FS_REWEIGHTPLACE,FS_REWEIGHTPERSON,FS_BILLNUMBER,FS_DFJZ,round(FS_YKBL,3)as FS_YKBL,FS_MEMO"
                + " from DT_FIRSTCARWEIGHT A where 1=1 ";



                
                sql += " and FD_WEIGHTTIME >= to_date('" + strBeginTime + "','yyyy-mm-dd hh24:mi:ss') ";
                sql += " and FS_FALG <> '1'";
                sql += " and FD_WEIGHTTIME <= to_date('" + strEndTime + "','yyyy-mm-dd hh24:mi:ss')";
                if (this.txtCarNo.Text.Trim().Length > 0)
                {
                    sql += " and upper(fs_carno) like '%'||upper('%" + txtCarNo.Text.Trim() + "%')||'%'";
                }
                sql += " order by FD_WEIGHTTIME DESC";
                // dataTable1.Rows.Clear();
                this.dataSet1.Tables[1].Rows.Clear();
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.CarCard";
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = this.dataSet1.Tables[1];

                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                Constant.RefreshAndAutoSize(ultraGrid3);
                try
                {
                    foreach (UltraGridRow ugr in ultraGrid3.Rows)
                    {
                        if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                        {
                            ugr.Appearance.ForeColor = Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else 
            {
                this.ultraGroupBox2.Visible = true;
                this.ultraGroupBox1.Visible = false;
                strBeginTime = this.dateBegin.Value.ToString("yyyy-MM-dd HH:mm");
                strEndTime = this.dateEnd.Value.ToString("yyyy-MM-dd HH:mm");
                string strSql = " ";
                strSql += "SELECT 'false' choosen,t.fs_cardnumber, t.FS_CARNO,t.FS_MATERIALNAME,t.FS_STOVENO,t.FS_SENDERNAME,t.FS_TRANSNAME,t.FS_RECEIVERNAME,t.FN_SENDGROSSWEIGHT,t.FN_SENDTAREWEIGHT,t.FS_SENDERSTORE,t.FS_RECEIVERSTORE,t.FS_WEIGHTNO,t.FS_CHECKFLAG,";
                strSql += "t.FN_SENDNETWEIGHT,LX_BHTOMC(FS_WEIGHTTYPE)FS_WEIGHTTYPE,to_char(t.FN_GROSSWEIGHT,'FM999.000') FN_GROSSWEIGHT,t.FS_GROSSPOINT,t.FS_GROSSPERSON,t.FS_GROSSSHIFT,to_char(t.FN_TAREWEIGHT,'FM999.000') FN_TAREWEIGHT,t.FS_CONTRACTNO,t.FS_REWEIGHTPERSON,t.FS_REWEIGHTPLACE,t.FS_REWEIGHTFLAG,";
                strSql += "t.FS_TAREPOINT,t.FS_TAREPERSON,t.FS_TARESHIFT,t.FS_SAMPLEPLACE,t.FS_UNLOADPERSON,to_char(t.FN_NETWEIGHT,'FM999.000') FN_NETWEIGHT,t.FS_SAMPLEPERSON,t.FS_MATERIAL,t.FS_GY,t.FS_CY,t.FS_MEMO,decode(nvl(t.FS_SAMPLEFLAG,'0'),'1','√','') FS_SAMPLEFLAG,decode(nvl(t.FS_UNLOADFLAG,'0'),'1','卸货确认','2','退货过磅','3','退货不过磅','4','复磅','') FS_UNLOADFLAG,t.FS_UNLOADMEMO, ";
                strSql += "t.FS_SH,t.FS_TYPECODE,t.FS_WXPAYFLAG,t.FS_UNLOADPLACE,t.FS_CHECKPERSON,t.FS_CHECKPLACE,t.FS_YKL,round(t.FS_YKBL,3)as FS_YKBL ,to_char(t.FS_KHJZ,'FM999.000') FS_KHJZ,t.FS_DATASTATE,t.FS_BZ,t.FS_PROVIDER,t.FS_PROVIDERNAME,";
                strSql += "t.FN_FHJZ,t.FN_SJTH,t.FN_CETH,t.FN_YFDJ,t.FN_YFJE,t.FN_KSDJ,t.FN_KTH,t.FN_SFJE,";
                strSql += "to_char(t.FD_FHRQ,'yyyy-MM-dd hh24:mi:ss')as FD_FHRQ,";
                strSql += "to_char(t.FD_REWEIGHTTIME,'yyyy-MM-dd hh24:mi:ss')as FD_REWEIGHTTIME,";
                strSql += "to_char(t.FD_CHECKTIME,'yyyy-MM-dd hh24:mi:ss')as FD_CHECKTIME,";
                strSql += "to_char(t.FD_UNLOADTIME,'yyyy-MM-dd hh24:mi:ss')as FD_UNLOADTIME,";
                strSql += "to_char(t.FD_SAMPLETIME,'yyyy-MM-dd hh24:mi:ss')as FD_SAMPLETIME,";
                strSql += "to_char(t.FD_GROSSDATETIME,'yyyy-MM-dd hh24:mi:ss')as FD_GROSSDATETIME,";
                strSql += "to_char(t.FD_TAREDATETIME,'yyyy-MM-dd hh24:mi:ss')as FD_TAREDATETIME,";
                strSql += "to_char(t.FD_TOCENTERTIME,'yyyy-MM-dd hh24:mi:ss')as FD_TOCENTERTIME,";
                strSql += "to_char(t.FD_ACCOUNTDATE,'yyyy-MM-dd hh24:mi:ss')as FD_ACCOUNTDATE,";
                strSql += "to_char(t.FS_JZDATE,'yyyy-MM-dd hh24:mi:ss')as FS_JZDATE,";
                strSql += "to_char(FS_CREATEJSRQ,'yyyy-MM-dd hh24:mi:ss')as FS_CREATEJSRQ,";
                strSql += "to_char(t.FD_TESTIFYDATE,'yyyy-MM-dd hh24:mi:ss')as FD_TESTIFYDATE ";
                strSql += " FROM DT_CARWEIGHTVIEW t ";
                strSql += " where fd_taredatetime >= to_date('" + strBeginTime + "','yyyy-mm-dd hh24:mi:ss') ";
                strSql += " and fd_taredatetime <= to_date('" + strEndTime + "','yyyy-mm-dd hh24:mi:ss')";
                if (this.txtCarNo.Text.Trim() !="" )
                {
                    strSql += " and upper(FS_CARNO) like '%'||upper('%" + txtCarNo.Text.Trim() + "%')||'%'";
                }
                strSql += " order by fd_taredatetime DESC";
                
                this.dataSet2.Tables[0].Rows.Clear();
                CoreClientParam ccp1 = new CoreClientParam();
                ccp1.ServerName = "ygjzjl.car.CarCard";
                ccp1.MethodName = "queryByClientSql";
                ccp1.ServerParams = new object[] { strSql };
                ccp1.SourceDataTable = this.dataSet2.Tables[0];

                this.ExecuteQueryToDataTable(ccp1, CoreInvokeType.Internal);
           
                Constant.RefreshAndAutoSize(ultraGrid1);
                try
                {
                    foreach (UltraGridRow ugr in ultraGrid1.Rows)
                    {
                        if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                        {
                            ugr.Appearance.ForeColor = Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
             }
            ClearControls();
        }
        private bool Decision()
        {
            beginTime = this.dateBegin.Value.Date;
            endTime = this.dateEnd.Value.Date;
            decisionResult = limitQueryTime.ParseTime(beginTime, endTime);
            if (!decisionResult)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        private void DateCheck()
        {
            try
            {
                this.dateBegin.Value = DateTime.Today;
                this.dateEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    if (!Decision())
                    {
                        DateCheck();
                        return;
                    }
                    QueryYCBData();
                    break;
               
                case "撤销取样":
                    CancelChooseSample();
                    break;
                case "取样确认":
                    SaveChooseSample();
                    break;
                case "取消卸货":
                    CancelDisCharge();
                    break;
                case "卸货确认":
                    SaveDischarge();
                    break;
                case "扣渣保存":
                    SaveKouZha();
                    break;
                case "撤销扣渣":
                    CancelKouZha();
                    break;
            }
        }
        /// <summary>
        /// 撤销取样
        /// </summary>
        private void CancelChooseSample()
        {
            string strSql = "";
            try
            {
                strSql += "update dt_carweight_weight t set t.fs_sampleflag='0',t.fs_sampleperson='',t.fd_sampletime=to_date('','yyyy-MM-dd HH24:mi:ss') where t.fs_weightno='{0}'";
                ultraGrid1.UpdateData();
                ArrayList weightnos = new ArrayList();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid1.Rows)
                {
                    if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                    {
                        weightnos.Add(uRow.Cells["fs_weightno"].Value.ToString());
                        if (uRow.Cells["fs_sampleflag"].Value.ToString().Trim() == "" || uRow.Cells["fs_sampleflag"].Value.ToString().Trim() == "0")
                        {
                            MessageBox.Show("该记录没有做过取样操作，请重新选择！");
                            return;
                        }
                    }
                }
                foreach (object weighno in weightnos)
                {
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.car.CarCard";
                    ccp.MethodName = "updateByClientSql";
                    ccp.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            QueryYCBData();
            ClearControls();
        }

        /// <summary>
        /// 取样确认
        /// </summary>
        private void SaveChooseSample()
        {
            string strSql = "";
            try
            {
                //strSql += "update dt_firstcarweight t set t.fs_sampleflag='1',t.fs_sampleperson='" + this.UserInfo.GetUserName() + "',t.fd_sampletime=sysdate where t.fs_weightno='{0}'";
                strSql += "update dt_carweight_weight t set t.fs_sampleflag='1',t.fs_sampleperson='" + this.UserInfo.GetUserName() + "',t.fd_sampletime=sysdate where t.fs_weightno='{0}'";

                ultraGrid1.UpdateData();
                ArrayList weightnos = new ArrayList();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid1.Rows)
                {
                    if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                    {
                        weightnos.Add(uRow.Cells["fs_weightno"].Value.ToString());
                        if (uRow.Cells["fs_sampleflag"].Value.ToString().Trim() == "1")
                        {
                            MessageBox.Show("该记录已经做过取样操作，请重新选择！");
                            return;
                        }
                    }
                }
                foreach (object weighno in weightnos)
                {
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.car.CarCard";
                    ccp.MethodName = "updateByClientSql";
                    ccp.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            QueryYCBData();
            ClearControls();
        }
        /// <summary>
        /// 取消卸货  包含扣渣
        /// </summary>
        private void CancelDisCharge()
        {
            string strSql = "";
            try
            {
                if (cmbChange.Text == "没有净重")
                {
                    //string ykl = ultraGrid3.ActiveRow.Cells["fs_ykl"].Text.Trim().ToString();
                    //string weight = ultraGrid3.ActiveRow.Cells["fn_weight"].Text.Trim().ToString();
                    //string netweight = ((double.Parse(ykl) / 1000) + double.Parse(weight)).ToString();

                    if (checkBox1.Checked == true)
                    {
                        strSql += "update dt_firstcarweight t set t.fs_unloadflag='',t.fs_unloadperson='',t.fd_unloadtime='',t.fs_ykl='0',t.fs_ykbl='',fs_unloadmemo='' where t.fs_weightno='{0}'";
                        ultraGrid3.UpdateData();
                        ArrayList weightnos = new ArrayList();
                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid3.Rows)
                        {
                            if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                            {
                                weightnos.Add(uRow.Cells["fs_weightno"].Value.ToString());
                                if (uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "" || uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "0")
                                {
                                    MessageBox.Show("该记录没有做过卸货操作，请重新选择！");
                                    return;
                                }
                            }
                        }
                        foreach (object weighno in weightnos)
                        {
                            CoreClientParam ccp4 = new CoreClientParam();
                            ccp4.ServerName = "ygjzjl.car.CarCard";
                            ccp4.MethodName = "updateByClientSql";
                            ccp4.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                            this.ExecuteNonQuery(ccp4, CoreInvokeType.Internal);
                        }
                    }
                    else
                    {
                        ultraGrid3.UpdateData();
                        strSql += "update dt_firstcarweight t set t.fs_unloadflag='',t.fs_unloadperson='',t.fd_unloadtime='',t.fs_ykl='0',t.fs_ykbl='',fs_unloadmemo='' where t.fs_weightno='{0}'";
                        ArrayList weightnosOne = new ArrayList();
                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid3.Rows)
                        {
                            if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                            {
                                weightnosOne.Add(uRow.Cells["fs_weightno"].Value.ToString());
                                if (uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "" || uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "0")
                                {
                                    MessageBox.Show("该记录没有做过卸货操作，请重新选择！");
                                    return;
                                }
                            }
                        }
                        foreach (object weighno in weightnosOne)
                        {
                            CoreClientParam ccp5 = new CoreClientParam();
                            ccp5.ServerName = "ygjzjl.car.CarCard";
                            ccp5.MethodName = "updateByClientSql";
                            ccp5.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                            this.ExecuteNonQuery(ccp5, CoreInvokeType.Internal);
                        }
                    }
                   
                   
                }
                else //已有净重
                {
                    
                    //string jz = ultraGrid1.ActiveRow.Cells["fn_netweight"].Text.Trim().ToString();
                    if (checkBox1.Checked == true)//要扣渣
                    {
                        strSql += "update dt_carweight_weight t set t.fs_unloadflag='0',t.fs_unloadperson='',t.fd_unloadtime=to_date('','yyyy-MM-dd HH24:mi:ss'),t.fs_ykl='0',t.fs_ykbl='',t.fs_KHJZ=fn_netweight,fs_unloadmemo=''  where t.fs_weightno='{0}'";
                        ultraGrid1.UpdateData();

                        ArrayList weightnos3 = new ArrayList();
                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid1.Rows)
                        {
                            if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                            {
                                weightnos3.Add(uRow.Cells["fs_weightno"].Value.ToString());
                                if (uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "" || uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "0")
                                {
                                    MessageBox.Show("该记录没有做过卸货操作，请重新选择！");
                                    return;
                                }
                            }
                        }
                        foreach (object weighno in weightnos3)
                        {
                            
                            CoreClientParam ccp3 = new CoreClientParam();
                            ccp3.ServerName = "ygjzjl.car.CarCard";
                            ccp3.MethodName = "updateByClientSql";
                            ccp3.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                            this.ExecuteNonQuery(ccp3, CoreInvokeType.Internal);
                        }
                    }
                    else//不要扣渣
                    {
                        string strSql1 = "";
                        strSql1 += "update dt_carweight_weight t set t.fs_unloadflag='0',t.fs_unloadperson='',t.fd_unloadtime=to_date('','yyyy-MM-dd HH24:mi:ss'),t.fs_ykl='0',t.fs_ykbl='',t.fs_KHJZ=fn_netweight,fs_unloadmemo=''  where t.fs_weightno='{0}'";
                        ultraGrid1.UpdateData();
                        ArrayList weightnos = new ArrayList();
                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid1.Rows)
                        {
                            if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                            {
                                weightnos.Add(uRow.Cells["fs_weightno"].Value.ToString());
                                if (uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "" || uRow.Cells["fs_unloadflag"].Value.ToString().Trim() == "0")
                                {
                                    MessageBox.Show("该记录没有做过卸货操作，请重新选择！");
                                    return;
                                }
                            }
                        }
                        foreach (object weighno in weightnos)
                        {
                           
                            CoreClientParam ccp = new CoreClientParam();
                            ccp.ServerName = "ygjzjl.car.CarCard";
                            ccp.MethodName = "updateByClientSql";
                            ccp.ServerParams = new object[] { string.Format(strSql1,weighno.ToString()) };
                            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        }
                    }
                    
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            QueryYCBData();
            ClearControls();
        }
        /// <summary>
        /// 卸货确认  包含扣渣
        /// </summary>
        private void SaveDischarge()
        {
            string strSql = "";
            try
            {
                if (cmbChange.Text == "没有净重")
                {
                    if (checkBox1.Checked == true)
                    {
                        if (this.radiozl.Checked == true)
                        {
                            strSql += "update dt_firstcarweight t set t.fs_ykl='" + this.txtYKL.Text.Trim() + "',t.fs_ykbl=" + double.Parse(this.txtYKL.Text) / 1000 * 100 + "/fn_weight,t.fs_unloadflag=decode('" + this.cmbUnloadStyle.Text.Trim() + "','卸货确认','1','退货且过磅','2','退货且不过磅','3','复磅','4'),t.fs_unloadperson='" + this.UserInfo.GetUserName() + "',t.fd_unloadtime=sysdate,t.fs_unloadmemo='" + this.txtMemoXiehuo.Text + "' where t.fs_weightno='{0}'";
                            
                        }
                        else 
                        {
                            strSql += "update dt_firstcarweight t set t.fs_unloadflag=decode('" + this.cmbUnloadStyle.Text.Trim() + "','卸货确认','1','退货且过磅','2','退货且不过磅','3','复磅','4'),t.fs_unloadperson='" + this.UserInfo.GetUserName() + "', t.fd_unloadtime=sysdate,t.fs_ykl=fn_weight*1000*'" + double.Parse(this.txtYKBL.Text) / 100 + "',t.fs_ykbl='" + this.txtYKBL.Text + "',t.fs_unloadmemo='" + this.txtMemoXiehuo.Text + "' where t.fs_weightno='{0}'";
                        }
                    }
                    else
                    {

                        strSql += "update dt_firstcarweight t set t.fs_unloadflag=decode('" + this.cmbUnloadStyle.Text.Trim() + "','卸货确认','1','退货且过磅','2','退货且不过磅','3','复磅','4'),t.fs_unloadperson='" + this.UserInfo.GetUserName() + "', t.fd_unloadtime=sysdate,t.fs_unloadmemo='" + this.txtMemoXiehuo.Text + "' where t.fs_weightno='{0}'";
                        
                    }
                    ultraGrid3.UpdateData();
                    ArrayList weightnos = new ArrayList();
                    foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid3.Rows)
                    {
                        if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                        {
                            weightnos.Add(uRow.Cells["fs_weightno"].Value.ToString());
                            if (uRow.Cells["fs_unloadflag"].Value.ToString().Trim() != "")
                            {
                                MessageBox.Show("该记录已经做过卸货操作，请重新选择！");
                                return;
                            }
                        }
                    }
                    foreach (object weighno in weightnos)
                    {
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.car.CarCard";
                        ccp.MethodName = "updateByClientSql";
                        ccp.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    }
                }

                else//已有净重
                {
                    if (checkBox1.Checked == true)//要扣渣
                    {
                            string yklgloble = (double.Parse(this.txtYKL.Text) / 1000).ToString();
                            if (this.radiozl.Checked == true)
                            {
                                strSql += "update dt_carweight_weight t set t.fs_unloadflag=decode('" + this.cmbUnloadStyle.Text.Trim() + "','卸货确认','1','退货且过磅','2','退货且不过磅','3','复磅','4'),t.fs_unloadperson='" + this.UserInfo.GetUserName() + "',t.fd_unloadtime=sysdate,t.FS_KHJZ=fn_netweight-" + double.Parse(yklgloble) + " ,t.fs_ykbl=" + double.Parse(yklgloble) * 100 + "/fn_netweight,t.fs_ykl='" + this.txtYKL.Text.Trim() + "',fs_unloadmemo='" + this.txtMemoXiehuo.Text + "' where t.fs_weightno='{0}'";
                            }
                            else
                            {
                                strSql += "update dt_carweight_weight t set t.fs_unloadflag=decode('" + this.cmbUnloadStyle.Text.Trim() + "','卸货确认','1','退货且过磅','2','退货且不过磅','3','复磅','4'),t.fs_unloadperson='" + this.UserInfo.GetUserName() + "',t.fd_unloadtime=sysdate,t.fs_ykl=fn_netweight*1000*'" + double.Parse(this.txtYKBL.Text) / 100 + "',t.FS_KHJZ=fn_netweight-fs_ykl/1000 ,t.fs_ykbl='" + double.Parse(this.txtYKBL.Text) + "',fs_unloadmemo='" + this.txtMemoXiehuo.Text + "' where t.fs_weightno='{0}'";
                            }
                            ultraGrid1.UpdateData();
                            ArrayList weightnosunload = new ArrayList();
                            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid1.Rows)
                            {
                                if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                                {
                                    weightnosunload.Add(uRow.Cells["fs_weightno"].Value.ToString());
                                    if (uRow.Cells["fs_unloadflag"].Value.ToString().Trim() != "")
                                    {
                                        MessageBox.Show("该记录已经做过卸货操作，请重新选择！");
                                        return;
                                    }
                                }
                            }
                            foreach (object weighno in weightnosunload)
                            {
                                CoreClientParam ccp0 = new CoreClientParam();
                                ccp0.ServerName = "ygjzjl.car.CarCard";
                                ccp0.MethodName = "updateByClientSql";
                                ccp0.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                                this.ExecuteNonQuery(ccp0, CoreInvokeType.Internal);
                            }
                   

                        
                    }
                    else//不要扣渣
                    {
                        if (this.ultraGrid1.Rows.Count != 0)
                        {
                            //if (ultraGrid1.ActiveRow.Cells["fs_unloadflag"].Text.Trim().ToString() == "1")//已经卸货
                            //{
                            //    return;
                            //}
                            //else
                            //{
                            strSql += "update dt_carweight_weight t set t.fs_unloadflag=decode('" + this.cmbUnloadStyle.Text.Trim() + "','卸货确认','1','退货且过磅','2','退货且不过磅','3','复磅','4'),t.fs_unloadperson='" + this.UserInfo.GetUserName() + "',t.fd_unloadtime=sysdate,t.fs_unloadmemo='" + this.txtMemoXiehuo.Text + "' where t.fs_weightno='{0}'";
                            //}
                        }
                        else { return; }
                    }
                    ultraGrid1.UpdateData();
                    ArrayList weightnos = new ArrayList();
                    foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid1.Rows)
                    {
                        if (Convert.ToBoolean(uRow.Cells["choosen"].Value))
                        {
                            weightnos.Add(uRow.Cells["fs_weightno"].Value.ToString());
                            if (uRow.Cells["fs_unloadflag"].Value.ToString().Trim() != "")
                            {
                                MessageBox.Show("该记录已经做过卸货操作，请重新选择！");
                                return;
                            }
                        }
                    }
                    foreach (object weighno in weightnos)
                    {
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.car.CarCard";
                        ccp.MethodName = "updateByClientSql";
                        ccp.ServerParams = new object[] { string.Format(strSql, weighno.ToString()) };
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    }
                    
                }
                QueryYCBData();
                ClearControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 保存扣渣
        /// </summary>
        private void SaveKouZha()
        {
            // string strSql = "";
            // try
            //    {
            //        if (this.txtCarEditNo.Text.Trim() == "")
            //        {
            //            return;
            //        }
            //        //string ykl = ultraGrid3.ActiveRow.Cells["fs_ykl"].Text.Trim().ToString();
            //        //string weight = ultraGrid3.ActiveRow.Cells["fn_weight"].Text.Trim().ToString();
            //        //string netweight = (double.Parse(weight) - double.Parse(ykl)).ToString();
                    
            //     //   if (ykl == "" || ykl=="0")
            //     //{
                    
            //     //}
            //        strSql += "update dt_firstcarweight t set t.fs_ykl='" + this.txtYKL.Text.Trim() + "',t.fs_ykbl='" + this.txtYKBL.Text + "',t.fn_weight='" + this.txtKHJZ.Text.Trim() + "'  where t.fs_weightno='" + this.txtweightno.Text + "'";       
            //        CoreClientParam ccp = new CoreClientParam();
            //        ccp.ServerName = "ygjzjl.car.CarCard";
            //        ccp.MethodName = "updateByClientSql";
            //        ccp.ServerParams = new object[] { strSql };
            //        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                   
            //    }
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
                
            //QueryYCBData();
            //ClearControls();
        }
        private void CancelKouZha()
        {
            //string strSql = "";
            //try
            //{
            //    if (this.txtCarEditNo.Text.Trim()=="")
            //    {
            //        return;
            //    }
            //    string ykl = ultraGrid3.ActiveRow.Cells["fs_ykl"].Text.Trim().ToString();
            //    string weight = ultraGrid3.ActiveRow.Cells["fn_weight"].Text.Trim().ToString();
            //    string netweight = ((double.Parse(ykl)/1000) + double.Parse(weight)).ToString();
            //    if (ykl == "" || ykl == "0")
            //    {
            //        return;
            //    }
            //    strSql += "update dt_firstcarweight t set t.fs_ykl='',t.fs_ykbl='',t.fn_weight='" + netweight + "'  where t.fs_weightno='" + this.txtweightno.Text + "'";
            //    CoreClientParam ccp = new CoreClientParam();
            //    ccp.ServerName = "ygjzjl.car.CarCard";
            //    ccp.MethodName = "updateByClientSql";
            //    ccp.ServerParams = new object[] { strSql };
            //    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //QueryYCBData();
            //ClearControls();
        }

        private void ClearControls()
        {
            this.txtQYR.Text = "";
            this.txtQYSJ.Text = "";
            this.txtConfirm.Text = "";
            this.txtXHR.Text = "";
            this.txtXHSJ.Text = "";

            this.txtXHQR.Text = "";
            this.txtKZR.Text = "";
            this.txtYKL.Text = "0";
            this.txtYKBL.Text = "0";
            this.txtCarEditNo.Text = "";
            this.txtKHJZ.Text = "";
            this.txtweightno.Text = "";
        }
        private void ultraGrid3_DoubleClick(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (ultraGrid3.Rows.Count > 0 && e.Row.Index != -1)
            {
                strZYBH = this.ultraGrid3.Rows[e.Row.Index].Cells["FS_WEIGHTNO"].Value.ToString();
                
            }
           
        }

        private void rdChooseAll_CheckedChanged(object sender, EventArgs e)
        {
           
                if (rdChooseAll.Checked == true)
                {

                    for (int i = 0; i < this.ultraGrid3.Rows.Count; i++)
                    {
                        ultraGrid3.Rows[i].Cells["choosen"].Value = true;
                    }

                }
                else
                {
                    for (int j = 0; j < this.ultraGrid3.Rows.Count; j++)
                    {
                        ultraGrid3.Rows[j].Cells["choosen"].Value = false;
                    }
                }
            
        }

        private void ultraGrid3_ClickCell(object sender, ClickCellEventArgs e)
        {
            this.txtQYR.Text = this.UserInfo.GetUserName();
            this.txtQYSJ.Text = ultraGrid3.ActiveRow.Cells["fd_sampletime"].Text.Trim().ToString();//
            this.txtConfirm.Text = ultraGrid3.ActiveRow.Cells["fs_sampleflag"].Text.Trim().ToString();
            this.txtXHR.Text = this.UserInfo.GetUserName();
            this.txtXHSJ.Text = ultraGrid3.ActiveRow.Cells["fd_unloadtime"].Text.Trim().ToString();

            this.txtXHQR.Text = ultraGrid3.ActiveRow.Cells["fs_unloadflag"].Text.Trim().ToString();
            this.txtweightno.Text = ultraGrid3.ActiveRow.Cells["fs_weightno"].Text.Trim().ToString();
            this.txtKZR.Text = this.UserInfo.GetUserName();

            this.txtYKL.Text = ultraGrid3.ActiveRow.Cells["fs_ykl"].Text.Trim().ToString();
            this.txtYKBL.Text = ultraGrid3.ActiveRow.Cells["fs_ykbl"].Text.Trim().ToString();
            this.txtCarEditNo.Text = ultraGrid3.ActiveRow.Cells["fs_carno"].Text.Trim().ToString();
            this.txtKHJZ.Text = ultraGrid3.ActiveRow.Cells["fn_weight"].Text.Trim().ToString();
            this.txtMemoXiehuo.Text = ultraGrid3.ActiveRow.Cells["fs_unloadmemo"].Text.Trim().ToString();
        }

        private void txtYKL_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private bool IsData(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"(^(\d*)$)");
        }

        private void txtYKL_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsData(this.txtYKL.Text.Trim()) == false)
            {
                this.txtYKL.Text = "";
                this.txtYKBL.Text = "";
                this.txtKHJZ.Text = "";
            }
            if (e.KeyValue == (char)8)
            {
                this.txtYKL.Text = "";
                this.txtYKBL.Text = "";
                this.txtKHJZ.Text = "";
                return;
            }
            if (e.KeyValue == (char)13)
            {
                return;
            }
            try
            {
                if (this.txtCarEditNo.Text == "")
                {
                    return;
                }

                if (this.txtYKL.Text.Trim() == "")
                {

                    return;
                }
                else
                {
                    if (this.cmbChange.Text=="已有净重")
                    {
                        double strYKL = Convert.ToDouble(this.txtYKL.Text);
                        double strJZ = Convert.ToDouble(this.ultraGrid1.ActiveRow.Cells["fn_netweight"].Text);
                        this.txtYKBL.Text = (((strYKL / 10) / strJZ)).ToString();
                        if (this.txtYKBL.Text.Trim().Length >= 5)
                        {
                            double ykbl = ((strYKL / 1000) / strJZ) * 100;
                            this.txtYKBL.Text = Math.Round(ykbl, 3).ToString();
                        }

                        this.txtYKBL.Enabled = false;
                        this.txtKHJZ.Text = (strJZ - strYKL / 1000).ToString();
                        this.txtYKBL.BackColor = Color.Bisque;
                    }
                    else 
                    {
                        double strYKL = Convert.ToDouble(this.txtYKL.Text);
                        double strJZ = Convert.ToDouble(this.ultraGrid3.ActiveRow.Cells["fn_weight"].Text);
                        this.txtYKBL.Text = (((strYKL / 10) / strJZ)).ToString();
                        if (this.txtYKBL.Text.Trim().Length >= 5)
                        {
                            double ykbl = ((strYKL / 1000) / strJZ) * 100;
                            this.txtYKBL.Text = Math.Round(ykbl, 3).ToString();
                        }

                        this.txtYKBL.Enabled = false;
                        this.txtKHJZ.Text = (strJZ - strYKL / 1000).ToString();
                        this.txtYKBL.BackColor = Color.Bisque;
                    }
                    

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SetData()
        {
            this.txtQYR.Text = this.UserInfo.GetUserName();
            this.txtQYSJ.Text = ultraGrid1.ActiveRow.Cells["fd_sampletime"].Text.Trim().ToString();//
            this.txtConfirm.Text = ultraGrid1.ActiveRow.Cells["fs_sampleflag"].Text.Trim().ToString();
            this.txtXHR.Text = this.UserInfo.GetUserName();
            this.txtXHSJ.Text = ultraGrid1.ActiveRow.Cells["fd_unloadtime"].Text.Trim().ToString();

            this.txtXHQR.Text = ultraGrid1.ActiveRow.Cells["fs_unloadflag"].Text.Trim().ToString();
            this.txtweightno.Text = ultraGrid1.ActiveRow.Cells["fs_weightno"].Text.Trim().ToString();
            this.txtKZR.Text = this.UserInfo.GetUserName();

            this.txtYKL.Text = ultraGrid1.ActiveRow.Cells["fs_ykl"].Text.Trim().ToString();
            this.txtYKBL.Text = ultraGrid1.ActiveRow.Cells["fs_ykbl"].Text.Trim().ToString();
            this.txtCarEditNo.Text = ultraGrid1.ActiveRow.Cells["fs_carno"].Text.Trim().ToString();
            this.txtKHJZ.Text = ultraGrid1.ActiveRow.Cells["fn_netweight"].Text.Trim().ToString();
            this.txtMemoXiehuo.Text = ultraGrid1.ActiveRow.Cells["fs_unloadmemo"].Text.Trim().ToString();
        }
        private void ultraGrid1_ClickCell(object sender, ClickCellEventArgs e)
        {
            SetData();                     
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
             
            if (radioButton2.Checked == true)
            {

                for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
                {
                    ultraGrid1.Rows[i].Cells["choosen"].Value = true;
                }

            }
            else
            {
                for (int j = 0; j < this.ultraGrid1.Rows.Count; j++)
                {
                    ultraGrid1.Rows[j].Cells["choosen"].Value = false;
                }
            }
        }
        /// <summary>
        /// 按比例扣渣
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radiobl_CheckedChanged(object sender, EventArgs e)
        {
            //this.txtYKL.Enabled = true;
            //this.txtYKBL.Enabled = false;
            //this.txtYKL.Text = "";
            //this.txtYKBL.Text = "";
            //this.txtKHJZ.Text = "";
        }

        private void txtYKBL_KeyUp(object sender, KeyEventArgs e)
        {
            if (IsData(this.txtYKBL.Text.Trim()) == false)
            {
                this.txtYKL.Text = "";
                this.txtYKBL.Text = "";
                this.txtKHJZ.Text = "";
            }
            if (e.KeyValue == (char)8)
            {
                this.txtYKL.Text = "";
                this.txtYKBL.Text = "";
                this.txtKHJZ.Text = "";
                return;
            }
            if (e.KeyValue == (char)13)
            {
                return;
            }
            try
            {
                if (this.txtCarEditNo.Text == "")
                {
                    return;
                }

                if (this.txtYKBL.Text.Trim() == "")
                {

                    return;
                }
                if(this.cmbChange.Text=="已有净重")
                {
                    if (this.ultraGrid1.Rows.Count == 0)
                    {
                        MessageBox.Show("请先查询数据!");
                        return;
                    }
                    else 
                    {
                        double strkhjz = Convert.ToDouble(this.ultraGrid1.ActiveRow.Cells["fn_netweight"].Text);
                        double strbl = Convert.ToDouble(this.txtYKBL.Text);

                        this.txtYKL.Text = ((strbl / 100) * (strkhjz * 1000)).ToString();
                        this.txtKHJZ.Text = (strkhjz - (Convert.ToDouble(this.txtYKL.Text) / 1000)).ToString();
                    }
                }
                if (this.cmbChange.Text == "没有净重")
                {
                    if (this.ultraGrid3.Rows.Count == 0)
                    {
                        MessageBox.Show("请先查询数据!");
                        return;
                    }
                    else
                    {
                        double strkhjz = Convert.ToDouble(this.ultraGrid3.ActiveRow.Cells["fn_weight"].Text);
                        double strbl = Convert.ToDouble(this.txtYKBL.Text);

                        this.txtYKL.Text = ((strbl / 100) * (strkhjz * 1000)).ToString();
                        this.txtKHJZ.Text = (strkhjz - (Convert.ToDouble(this.txtYKL.Text) / 1000)).ToString();
                    }
                }
                
                
               
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void radiozl_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cmbChange.Text == "已有净重")
            {
                if (checkBox1.Checked == true)
                {
                    this.radiozl.Checked = true;
                    this.radiozl.Visible = true;
                    this.radiobl.Visible = true;
                    this.txtYKL.Enabled = true;
                    this.txtYKBL.BackColor = Color.Bisque;
                    this.txtYKL.BackColor = Color.White;
                    //ultraGrid1.DisplayLayout.Bands[0].Columns["choosen"].Hidden = true;
                    //this.radioButton1.Visible = false;
                    //this.radioButton2.Visible = false;

                    //for (int j = 0; j < this.ultraGrid1.Rows.Count; j++)
                    //{
                    //    ultraGrid1.Rows[j].Cells["choosen"].Value = false;
                    //}
                    ultraGrid1.Refresh();
                }
                else
                {
                    this.radiozl.Visible = false;
                    this.radiobl.Visible = false;
                    this.txtYKBL.Enabled = false;
                    this.txtYKL.Enabled = false;
                    this.txtYKBL.Text = "0";
                    this.txtYKL.Text = "0";
                    this.txtKHJZ.Text = "";
                    this.txtYKBL.BackColor = Color.Bisque;
                    this.txtYKL.BackColor = Color.Bisque;
                    //ultraGrid1.DisplayLayout.Bands[0].Columns["choosen"].Hidden = false;
                    this.radioButton1.Visible = true;
                    this.radioButton2.Visible = true;
                    ultraGrid1.Refresh();
                }
             }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        this.radiozl.Checked = true;
                        this.radiozl.Visible = true;
                        this.radiobl.Visible = true;
                        this.txtYKL.Enabled = true;
                        this.txtYKBL.BackColor = Color.Bisque;
                        this.txtYKL.BackColor = Color.White;
                        //ultraGrid3.DisplayLayout.Bands[0].Columns["choosen"].Hidden = true;
                        //this.rdChooseAll.Visible = false;
                        //this.rdCancelAll.Visible = false;

                        //for (int j = 0; j < this.ultraGrid3.Rows.Count; j++)
                        //{
                        //    ultraGrid3.Rows[j].Cells["choosen"].Value = false;
                        //}
                        ultraGrid3.Refresh();
                    }
                    else
                    {
                        this.radiozl.Visible = false;
                        this.radiobl.Visible = false;
                        this.txtYKBL.Enabled = false;
                        this.txtYKL.Enabled = false;
                        this.txtYKBL.Text = "0";
                        this.txtYKL.Text = "0";
                        this.txtKHJZ.Text = "";
                        this.txtYKBL.BackColor = Color.Bisque;
                        this.txtYKL.BackColor = Color.Bisque;
                        //ultraGrid3.DisplayLayout.Bands[0].Columns["choosen"].Hidden = false;
                        //this.rdChooseAll.Visible = true;
                        //this.rdCancelAll.Visible = true;
                        ultraGrid3.Refresh();
                    }
                
               }
        }

        private void radiozl_CheckedChanged(object sender, EventArgs e)
        {
            
            if (this.radiozl.Checked == true)
            {
                this.txtYKL.Enabled = true;
                this.txtYKBL.Enabled = false;
                this.txtYKL.BackColor = Color.White;
                this.txtYKBL.BackColor = Color.Bisque;
                
            }
            else
            {
                this.txtYKL.Enabled = false;
                this.txtYKBL.Enabled = true;
                this.txtYKBL.BackColor = Color.White;
                this.txtYKL.BackColor = Color.Bisque;
               
            }
            this.txtYKL.Text = "0";
            this.txtYKBL.Text = "0";
            this.txtKHJZ.Text = "";
        }

        private void cmbChange_SelectedValueChanged(object sender, EventArgs e)
        {
            if(this.cmbChange.Text=="已有净重")
            {
                this.ultraGroupBox2.Dock = DockStyle.Fill;
                this.ultraGroupBox2.Visible = true;
                this.ultraGroupBox1.Visible = false;
                this.radioButton1.Visible = true;
                this.radioButton2.Visible = true;
                this.checkBox1.Checked = false;
                this.dataSet1.Tables[1].Clear();
            }
            else
            {
                this.ultraGroupBox1.Dock = DockStyle.Fill;
                this.ultraGroupBox1.Visible = true;
                this.ultraGroupBox2.Visible = false;
                this.rdCancelAll.Visible = true;
                this.rdChooseAll.Visible = true;
                this.checkBox1.Checked = false;
                this.dataSet2.Tables[0].Clear();
            }
        }

       
    }
}
