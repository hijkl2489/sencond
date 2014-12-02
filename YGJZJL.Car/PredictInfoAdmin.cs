using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using Excel;


namespace YGJZJL.Car
{
    public partial class PredictInfoAdmin : FrmBase
    {
        private string s_FS_PLANCODE = ""; //计录预报号Guid，方便修改StartSign标志
        private System.Data.DataTable m_MaterialTable = new System.Data.DataTable();//磅房对应物料内存表;将所有磅房对应的物料下载到该表
        public System.Data.DataTable tempMaterial = new System.Data.DataTable();  //物料临时表；用于磅房筛选和助记码筛选

        private System.Data.DataTable m_ReveiverTable = new System.Data.DataTable();//收货方
        public System.Data.DataTable tempReveiver = new System.Data.DataTable();

        private System.Data.DataTable m_SenderTable = new System.Data.DataTable();//发货方
        public System.Data.DataTable tempSender = new System.Data.DataTable();

        private System.Data.DataTable m_ProviderTable = new System.Data.DataTable();//供应商
        public System.Data.DataTable tempProvider = new System.Data.DataTable();

        private System.Data.DataTable m_TransTable = new System.Data.DataTable();//承运方
        public System.Data.DataTable tempTrans = new System.Data.DataTable();

        private System.Data.DataTable m_CarNoTable = new System.Data.DataTable();//车号
        public System.Data.DataTable tempCarNo = new System.Data.DataTable();

        private System.Data.DataTable m_FlowTable = new System.Data.DataTable();//流向表


        private System.Windows.Forms.ListBox m_List = new System.Windows.Forms.ListBox(); //下拉列表框
        private string m_ListType = "";  //下拉列表框类型 
        private string str_DepartID = "";


        Excel.Application app;
        _Workbook workbook;
        System.Data.DataTable dtHTH = new System.Data.DataTable();//合同项目号变更，物料跟随变
        string htxmh = ""; //合同项目号变更，物料跟随变

        int index;   //索引关联删除

        //string sJLDID = "";//服务端返回值，计量点ID
        string sWLID = "";//服务端返回值，物料ID
        string sFHDWID = "";//服务端返回值，发货单位ID
        string sSHDWID = "";//服务端返回值，收货单位ID
        string sCYDWID = "";//服务端返回值，承运单位ID

        string strCarNo = ""; //导入的车证卡号

        string strExistFiles = "";
        string strExistSendPlaceFiles = "";
        string m_RunPath;

        System.Collections.ArrayList strDriverArray = new System.Collections.ArrayList(); //驾驶员姓名
        System.Collections.ArrayList DriverArray = new System.Collections.ArrayList(); //驾驶员姓名与身份证


        public PredictInfoAdmin()
        {
            InitializeComponent();
        }

        //构建内存表格式
        private void BuildMyTable()
        {
            DataColumn dc;
            //磅房对应物料
            dc = new DataColumn("FS_PointNo".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("FS_MATERIALNO".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("fs_materialname".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_MaterialTable.Columns.Add(dc);

            //磅房对应收货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("FS_Receiver".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("fs_memo".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_ReveiverTable.Columns.Add(dc);

            //磅房对应发货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_SUPPLIER".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_SUPPLIERName".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_SenderTable.Columns.Add(dc);

            //磅房对应供应单位单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_ProviderTable.Columns.Add(dc);
            dc = new DataColumn("FS_PROVIDER".ToUpper()); m_ProviderTable.Columns.Add(dc);
            dc = new DataColumn("FS_PROVIDERNAME".ToUpper()); m_ProviderTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_ProviderTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_ProviderTable.Columns.Add(dc);

            //磅房对应承运单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_TransNo".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_TRANSNAME".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_TransTable.Columns.Add(dc);

            //磅房对应车号表  FS_POINTNO, FS_CARNO, FN_TIMES
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_CarNoTable.Columns.Add(dc);
            dc = new DataColumn("FS_CARNO".ToUpper()); m_CarNoTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_CarNoTable.Columns.Add(dc);

            //流向表 FS_TYPECODE, FS_TYPENAME
            dc = new DataColumn("FS_TYPECODE".ToUpper()); m_FlowTable.Columns.Add(dc);
            dc = new DataColumn("FS_TYPENAME".ToUpper()); m_FlowTable.Columns.Add(dc);
        }

        //下载磅房对应物料基础信息  ,add by luobin 
        private void DownLoadMaterial()
        {
            string strSql = "select A.FS_PointNo, A.FS_MATERIALNO, b.fs_materialname, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_Pointmaterial A, It_Material B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.Fs_Materialno = B.Fs_Wl and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_MaterialTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对收货单位信息  ,add by luobin 
        private void DownLoadReceiver()
        {
            string strSql = "select A.FS_PointNo, A.FS_Receiver, b.fs_memo, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointReceiver A, It_Store B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_Receiver = B.Fs_SH and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ReveiverTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对发货单位信息  ,add by luobin 
        private void DownLoadSender()
        {
            string strSql = "select A.FS_PointNo, A.FS_SUPPLIER, b.FS_SUPPLIERNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_Pointsupplier A, IT_SUPPLIER B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_SUPPLIER = B.Fs_GY and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SenderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }


        //下载磅房对供应单位信息  ,add by luobin 
        private void DownLoadProvider()
        {
            string strSql = "select A.FS_PointNo, A.FS_PROVIDER, b.FS_PROVIDERNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from BT_POINTPROVIDER A, IT_PROVIDER B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_PROVIDER = B.FS_SP and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ProviderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对承运单位信息  ,add by luobin 
        private void DownLoadTrans()
        {
            string strSql = "select A.FS_PointNo, A.FS_TransNo, b.FS_TRANSNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointTrans A, BT_Trans B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_TransNo = B.Fs_CY and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_TransTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对应车号信息  ,add by luobin 
        private void DownLoadCarNo()
        {
            string strSql = "select FS_POINTNO, FS_CARNO, FN_TIMES From BT_POINTCARNO order by FN_TIMES desc ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_CarNoTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


        }

        //下载流向信息  ,add by luobin 
        private void DownLoadFlow()
        {
            string strSql = "select FS_TYPECODE, FS_TYPENAME From BT_WEIGHTTYPE order by FS_TYPECODE ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_FlowTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            cbLX.DataSource = this.m_FlowTable;
            cbLX.DisplayMember = "FS_TYPENAME";
            cbLX.ValueMember = "FS_TYPECODE";

        }

        //按磅房筛选物料信息
        private void BandPointMaterial(string PointID)
        {
            DataRow[] drs = null;

            tempMaterial = m_MaterialTable.Clone();

            drs = this.m_MaterialTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            tempMaterial.Clear();
            foreach (DataRow dr in drs)
            {
                tempMaterial.Rows.Add(dr.ItemArray);
            }

            DataRow drz = tempMaterial.NewRow();
            tempMaterial.Rows.InsertAt(drz, 0);
            cbWLMC.DataSource = tempMaterial;
            cbWLMC.DisplayMember = "fs_materialname";
            cbWLMC.ValueMember = "FS_MATERIALNO";
        }

        //按磅房筛选收货单位
        private void BandPointReceiver(string PointID)
        {
            DataRow[] drs = null;

            this.tempReveiver = this.m_ReveiverTable.Clone();

            drs = this.m_ReveiverTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempReveiver.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempReveiver.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempReveiver.NewRow();
            this.tempReveiver.Rows.InsertAt(drz, 0);
            this.cbSHDW.DataSource = this.tempReveiver;
            this.cbSHDW.DisplayMember = "FS_MEMO";
            this.cbSHDW.ValueMember = "FS_Receiver";
        }

        //按磅房筛选发货单位
        private void BandPointSender(string PointID)
        {
            DataRow[] drs = null;

            this.tempSender = this.m_SenderTable.Clone();

            drs = this.m_SenderTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempSender.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempSender.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempSender.NewRow();
            this.tempSender.Rows.InsertAt(drz, 0);
            this.cbFHDW.DataSource = this.tempSender;
            this.cbFHDW.DisplayMember = "FS_SUPPLIERName";
            this.cbFHDW.ValueMember = "FS_SUPPLIER";
        }


        //按磅房筛选供应单位
        private void BandPointProvider(string PointID)
        {
            DataRow[] drs = null;

            this.tempProvider = this.m_ProviderTable.Clone();

            drs = this.m_ProviderTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempProvider.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempProvider.Rows.Add(dr.ItemArray);
            }
              
            DataRow drz = this.tempProvider.NewRow();
            this.tempProvider.Rows.InsertAt(drz, 0);
            this.cbProvider.DataSource = this.tempProvider;
            this.cbProvider.DisplayMember = "FS_PROVIDERNAME";
            this.cbProvider.ValueMember = "FS_PROVIDER";
        }

        //按磅房筛选承运单位
        private void BandPointTrans(string PointID)
        {
            DataRow[] drs = null;

            this.tempTrans = this.m_TransTable.Clone();

            drs = this.m_TransTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempTrans.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempTrans.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempTrans.NewRow();
            this.tempTrans.Rows.InsertAt(drz, 0);
            this.cbCYDW.DataSource = this.tempTrans;
            cbCYDW.DisplayMember = "FS_TRANSNAME";
            cbCYDW.ValueMember = "FS_TRANSNO";

        }

        //按磅房筛选车号
        private void BandPointCarNo(string PointID)
        {
            DataRow[] drs = null;

            this.tempCarNo = this.m_CarNoTable.Clone();

            drs = this.m_CarNoTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempCarNo.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempCarNo.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempCarNo.NewRow();
            this.tempCarNo.Rows.InsertAt(drz, 0);
            this.cbCH1.DataSource = this.tempCarNo;
            cbCH1.DisplayMember = "FS_CARNO";
            //cbCH1.ValueMember = "FS_TRANSNO";

        }

        private void InitConfig()
        {
            //物料下拉框
            this.groupBox1.Controls.Add(m_List);
            m_List.Size = new Size(157, 80);
            m_List.Visible = false;
            m_List.ScrollAlwaysVisible = true;
            m_List.BringToFront();
            m_List.Click += new EventHandler(m_List_Click);
            m_List.KeyPress += new KeyPressEventHandler(m_List_KeyPress);
            m_List.Leave += new EventHandler(m_List_Leave);


        }

        private void PredictInfo_Load(object sender, EventArgs e)
        {

            m_RunPath = System.Environment.CurrentDirectory;

            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadTrans();  //下载磅房对应承运单位信息到内存表
            this.DownLoadCarNo(); //下载磅房对应车号信息到内存表
            this.DownLoadProvider();  //下载磅房对应供应单位信息到内存表

            DataGridInit();
            //GetCHData

            GetBFData();
            GetLXData();
            getDepartID();//获取部门ID
            QueryYBData();

            QueryDriverData();
            QuerySendPlaceData();


        }

        #region  网格显示设置
        /// <summary>
        /// 网格显示设置
        /// </summary>
        private void DataGridInit()
        {
            //行编辑器显示序号
            ultraGrid1.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
            ultraGrid1.DisplayLayout.Override.RowSelectorWidth = 28;
            ultraGrid1.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            for (int i = 0; i <= ultraGrid1.DisplayLayout.Bands[0].Columns.Count - 1; i++)
            {
                ultraGrid1.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ultraGrid1.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Equals;
                //ultraGrid1.DisplayLayout.Bands[0].Columns[i].FilterOperatorDropDownItems = Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals;
                //ultraGrid1.DisplayLayout.Bands[0].Columns[i].FilterOperatorDropDownItems = Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains;
            }
            ultraGrid1.DisplayLayout.Bands[0].Columns["FS_PLANCODE"].Hidden = true;
            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = null;
            }
            Constant.RefreshAndAutoSize(ultraGrid1);
        }
        #endregion


        /// <summary>
        /// 查询单个卡号预报总条数
        /// </summary>
        private bool QueryYBCount()
        {
            string strWhere = "and FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "Query";
            ccp.ServerParams = new object[] { strWhere };
            System.Data.DataTable dt = new System.Data.DataTable();
            ccp.SourceDataTable = dt;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 5)
            {
                MessageBox.Show("同一个卡号的预报信息已超过最大值5条，请修改或删除卡号为 " + txtCZH.Text.Trim() + " 的其它预报！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查询单个卡号预报处理
        /// </summary>
        private bool MoreYBDelete()
        {
            string strWhere = "A.FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "'";

            //预报数据
            string ServerYBstrSql = "SELECT A.FS_PLANCODE,A.FS_CARDNUMBER,A.FS_CARNO,A.FS_CONTRACTNO,A.FS_CONTRACTITEM,A.FS_SENDER,A.FS_SENDERSTORE,";
            ServerYBstrSql += "A.FS_MATERIAL,A.FS_MATERIALNAME,A.FS_RECEIVERFACTORY,A.FS_RECEIVERSTORE,A.FS_TRANSNO,A.FS_WEIGHTTYPE,A.FS_POUNDTYPE,";
            ServerYBstrSql += "to_char(A.FS_OVERTIME,'yyyy-MM-dd HH24:mi:ss')as FS_OVERTIME,A.FN_SENDGROSSWEIGHT,A.FN_SENDTAREWEIGHT,";
            ServerYBstrSql += "A.FN_SENDNETWEIGHT,A.FS_STOVENO,A.FN_BILLETCOUNT,A.FS_PERSON,A.FS_POINT,";
            ServerYBstrSql += "to_char(A.FS_DATETIME,'yyyy-MM-dd HH24:mi:ss')as FS_DATETIME,A.FS_SHIFT,A.FS_TERM,A.FS_STATUS,A.FN_TIMES,";
            ServerYBstrSql += "A.FN_WEIGHTTIMES,A.FS_PLANUSER,A.FS_PLANTEL,A.FS_LEVEL,A.FS_IFSAMPLING,A.FS_IFACCEPT,A.FS_DRIVERNAME,";
            ServerYBstrSql += "A.FS_DRIVERIDCARD,A.FS_DRIVERREMARK FROM DT_WEIGHTPLAN A WHERE " + strWhere;

            CoreClientParam ccpServerYB = new CoreClientParam();
            ccpServerYB.ServerName = "ygjzjl.base.QueryData";
            ccpServerYB.MethodName = "queryByClientSql";
            ccpServerYB.ServerParams = new object[] { ServerYBstrSql };
            System.Data.DataTable dtServerYB = new System.Data.DataTable();
            ccpServerYB.SourceDataTable = dtServerYB;
            this.ExecuteQueryToDataTable(ccpServerYB, CoreInvokeType.Internal);

            if (dtServerYB.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("系统中已存在该卡号对应预报，确认覆盖请点确定！", "提示！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dtServerYB.Rows.Count; i++)
                    {
                        //转移预报数据
                        string FS_PLANCODE = dtServerYB.Rows[i]["FS_PLANCODE"].ToString().Trim(); //预报号(Guid唯一标识符)
                        string FS_CARDNUMBER = dtServerYB.Rows[i]["FS_CARDNUMBER"].ToString().Trim(); //车证卡号
                        string FS_CARNO = dtServerYB.Rows[i]["FS_CARNO"].ToString().Trim(); //车号
                        string FS_CONTRACTNO = dtServerYB.Rows[i]["FS_CONTRACTNO"].ToString().Trim(); //引用采购合同表中的合同号，或空。
                        string FS_CONTRACTITEM = dtServerYB.Rows[i]["FS_CONTRACTITEM"].ToString().Trim(); //合同项目编号
                        string FS_SENDER = dtServerYB.Rows[i]["FS_SENDER"].ToString().Trim(); //发货方代码
                        string FS_SENDERSTORE = dtServerYB.Rows[i]["FS_SENDERSTORE"].ToString().Trim(); //发货方地点
                        string FS_MATERIAL = dtServerYB.Rows[i]["FS_MATERIAL"].ToString().Trim(); //物资代码
                        string FS_MATERIALNAME = dtServerYB.Rows[i]["FS_MATERIALNAME"].ToString().Trim(); //物料名称
                        string FS_RECEIVERFACTORY = dtServerYB.Rows[i]["FS_RECEIVERFACTORY"].ToString().Trim(); //收货方代码
                        string FS_RECEIVERSTORE = dtServerYB.Rows[i]["FS_RECEIVERSTORE"].ToString().Trim(); //收货库存点代码
                        string FS_TRANSNO = dtServerYB.Rows[i]["FS_TRANSNO"].ToString().Trim(); //承运方代码
                        string FS_WEIGHTTYPE = dtServerYB.Rows[i]["FS_WEIGHTTYPE"].ToString().Trim(); //流向代码
                        string FS_POUNDTYPE = dtServerYB.Rows[i]["FS_POUNDTYPE"].ToString().Trim(); //磅房编号
                        string FS_OVERTIME = dtServerYB.Rows[i]["FS_OVERTIME"].ToString().Trim(); //到期时间
                        string FN_SENDGROSSWEIGHT = dtServerYB.Rows[i]["FN_SENDGROSSWEIGHT"].ToString().Trim(); //预报总重
                        string FN_SENDTAREWEIGHT = dtServerYB.Rows[i]["FN_SENDTAREWEIGHT"].ToString().Trim(); //预报皮重
                        string FN_SENDNETWEIGHT = dtServerYB.Rows[i]["FN_SENDNETWEIGHT"].ToString().Trim(); //预报净量
                        string FS_STOVENO = dtServerYB.Rows[i]["FS_STOVENO"].ToString().Trim(); //炉号
                        string FN_BILLETCOUNT = dtServerYB.Rows[i]["FN_BILLETCOUNT"].ToString().Trim(); //支数,内部调拨钢坯时使用
                        string FS_PERSON = dtServerYB.Rows[i]["FS_PERSON"].ToString().Trim(); //录入员
                        string FS_POINT = dtServerYB.Rows[i]["FS_POINT"].ToString().Trim(); //录入点
                        string FS_DATETIME = dtServerYB.Rows[i]["FS_DATETIME"].ToString().Trim(); //录入时间
                        string FS_SHIFT = dtServerYB.Rows[i]["FS_SHIFT"].ToString().Trim(); //班次
                        string FS_TERM = dtServerYB.Rows[i]["FS_TERM"].ToString().Trim(); //班别
                        string FS_STATUS = dtServerYB.Rows[i]["FS_STATUS"].ToString().Trim(); //状态
                        string FN_TIMES = dtServerYB.Rows[i]["FN_TIMES"].ToString().Trim(); //计划车数
                        string FN_WEIGHTTIMES = dtServerYB.Rows[i]["FN_WEIGHTTIMES"].ToString().Trim(); //已计量车数
                        string FS_PLANUSER = dtServerYB.Rows[i]["FS_PLANUSER"].ToString().Trim(); //预报联系人
                        string FS_PLANTEL = dtServerYB.Rows[i]["FS_PLANTEL"].ToString().Trim(); //预报联系电话
                        string FS_LEVEL = dtServerYB.Rows[i]["FS_LEVEL"].ToString().Trim(); //是否允许保存汽车期限皮重 (1:为允许,0:为不允许)已改为是否需要卸货确认
                        string FS_IFSAMPLING = dtServerYB.Rows[i]["FS_IFSAMPLING"].ToString().Trim(); //是否需要取样确认 (1:为需要,0:为不需要)
                        string FS_IFACCEPT = dtServerYB.Rows[i]["FS_IFACCEPT"].ToString().Trim(); //是否需要验收确认 (1:为需要,0:为不需要)
                        string FS_DRIVERNAME = dtServerYB.Rows[i]["FS_DRIVERNAME"].ToString().Trim(); //驾驶员姓名
                        string FS_DRIVERIDCARD = dtServerYB.Rows[i]["FS_DRIVERIDCARD"].ToString().Trim(); //驾驶员身份证
                        string FS_DRIVERREMARK = dtServerYB.Rows[i]["FS_DRIVERREMARK"].ToString().Trim(); //备注(驾驶员等其它备注)

                        string DownYBSql = "insert into DT_WEIGHTPLAN_HISTORY(FS_PLANCODE,FS_CARDNUMBER,FS_CARNO,FS_CONTRACTNO,FS_CONTRACTITEM,FS_SENDER,FS_SENDERSTORE,";
                        DownYBSql += "FS_MATERIAL,FS_MATERIALNAME,FS_RECEIVERFACTORY,FS_RECEIVERSTORE,FS_TRANSNO,FS_WEIGHTTYPE,FS_POUNDTYPE,FS_OVERTIME,";
                        DownYBSql += "FN_SENDGROSSWEIGHT,FN_SENDTAREWEIGHT,FN_SENDNETWEIGHT,FS_STOVENO,FN_BILLETCOUNT,FS_PERSON,FS_POINT,FS_DATETIME,FS_SHIFT,";
                        DownYBSql += "FS_TERM,FS_STATUS,FN_TIMES,FN_WEIGHTTIMES,FS_PLANUSER,FS_PLANTEL,FS_LEVEL,FS_IFSAMPLING,FS_IFACCEPT,FS_DRIVERNAME,";
                        DownYBSql += "FS_DRIVERIDCARD,FS_DRIVERREMARK)";
                        DownYBSql += " VALUES('" + FS_PLANCODE + "','" + FS_CARDNUMBER + "','" + FS_CARNO + "','" + FS_CONTRACTNO + "',";
                        DownYBSql += "'" + FS_CONTRACTITEM + "','" + FS_SENDER + "','" + FS_SENDERSTORE + "','" + FS_MATERIAL + "','" + FS_MATERIALNAME + "',";
                        DownYBSql += "'" + FS_RECEIVERFACTORY + "','" + FS_RECEIVERSTORE + "','" + FS_TRANSNO + "','" + FS_WEIGHTTYPE + "','" + FS_POUNDTYPE + "',";
                        DownYBSql += "TO_DATE('" + FS_OVERTIME + "','yyyy-MM-dd HH24:mi:ss'),'" + FN_SENDGROSSWEIGHT + "','" + FN_SENDTAREWEIGHT + "',";
                        DownYBSql += "'" + FN_SENDNETWEIGHT + "','" + FS_STOVENO + "','" + FN_BILLETCOUNT + "','" + FS_PERSON + "',";
                        DownYBSql += "'" + FS_POINT + "',TO_DATE('" + FS_DATETIME + "','yyyy-MM-dd HH24:mi:ss'),'" + FS_SHIFT + "','" + FS_TERM + "',";
                        DownYBSql += "'" + FS_STATUS + "','" + FN_TIMES + "','" + FN_WEIGHTTIMES + "','" + FS_PLANUSER + "','" + FS_PLANTEL + "',";
                        DownYBSql += "'" + FS_LEVEL + "','" + FS_IFSAMPLING + "','" + FS_IFACCEPT + "','" + FS_DRIVERNAME + "','" + FS_DRIVERIDCARD + "',";
                        DownYBSql += "'" + FS_DRIVERREMARK + "')";

                        CoreClientParam ccpDownServerYB = new CoreClientParam();
                        ccpDownServerYB.ServerName = "ygjzjl.car.PredictInfo";
                        ccpDownServerYB.MethodName = "DownServerData";
                        ccpDownServerYB.ServerParams = new object[] { DownYBSql };
                        this.ExecuteNonQuery(ccpDownServerYB, CoreInvokeType.Internal);

                        if (ccpDownServerYB.ReturnCode != -1)
                        {
                            string strGuid = "'" + FS_PLANCODE + "'";
                            string strSql = "delete from DT_WEIGHTPLAN where  FS_PLANCODE = " + strGuid;
                            CoreClientParam ccpDelete = new CoreClientParam();
                            ccpDelete.ServerName = "ygjzjl.car.PredictInfo";
                            ccpDelete.MethodName = "DeleteData";
                            ccpDelete.ServerParams = new object[] { strSql };

                            this.ExecuteNonQuery(ccpDelete, CoreInvokeType.Internal);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }


            }
            return true;   
        }


        /// <summary>
        /// 查询单个车号预报处理
        /// </summary>
        private bool MoreNonCardYBDelete()
        {
            string strCarNo = this.cbCH1.Text.Trim() + this.cbCH2.Text.Trim();
            string strWhere = "A.FS_CARNO = '" + strCarNo + "'";

            //预报数据
            string ServerYBstrSql = "SELECT A.FS_PLANCODE,A.FS_CARDNUMBER,A.FS_CARNO,A.FS_CONTRACTNO,A.FS_CONTRACTITEM,A.FS_SENDER,A.FS_SENDERSTORE,";
            ServerYBstrSql += "A.FS_MATERIAL,A.FS_MATERIALNAME,A.FS_RECEIVERFACTORY,A.FS_RECEIVERSTORE,A.FS_TRANSNO,A.FS_WEIGHTTYPE,A.FS_POUNDTYPE,";
            ServerYBstrSql += "to_char(A.FS_OVERTIME,'yyyy-MM-dd HH24:mi:ss')as FS_OVERTIME,A.FN_SENDGROSSWEIGHT,A.FN_SENDTAREWEIGHT,";
            ServerYBstrSql += "A.FN_SENDNETWEIGHT,A.FS_STOVENO,A.FN_BILLETCOUNT,A.FS_PERSON,A.FS_POINT,";
            ServerYBstrSql += "to_char(A.FS_DATETIME,'yyyy-MM-dd HH24:mi:ss')as FS_DATETIME,A.FS_SHIFT,A.FS_TERM,A.FS_STATUS,A.FN_TIMES,";
            ServerYBstrSql += "A.FN_WEIGHTTIMES,A.FS_PLANUSER,A.FS_PLANTEL,A.FS_LEVEL,A.FS_IFSAMPLING,A.FS_IFACCEPT,A.FS_DRIVERNAME,";
            ServerYBstrSql += "A.FS_DRIVERIDCARD,A.FS_DRIVERREMARK FROM DT_WEIGHTPLAN A WHERE " + strWhere;

            CoreClientParam ccpServerYB = new CoreClientParam();
            ccpServerYB.ServerName = "ygjzjl.base.QueryData";
            ccpServerYB.MethodName = "queryByClientSql";
            ccpServerYB.ServerParams = new object[] { ServerYBstrSql };
            System.Data.DataTable dtServerYB = new System.Data.DataTable();
            ccpServerYB.SourceDataTable = dtServerYB;
            this.ExecuteQueryToDataTable(ccpServerYB, CoreInvokeType.Internal);

            if (dtServerYB.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("系统中已存在该车号的预报，确认覆盖请点击确认！", "提示！", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    for (int i = 0; i < dtServerYB.Rows.Count; i++)
                    {
                        //转移预报数据
                        string FS_PLANCODE = dtServerYB.Rows[i]["FS_PLANCODE"].ToString().Trim(); //预报号(Guid唯一标识符)
                        string FS_CARDNUMBER = dtServerYB.Rows[i]["FS_CARDNUMBER"].ToString().Trim(); //车证卡号
                        string FS_CARNO = dtServerYB.Rows[i]["FS_CARNO"].ToString().Trim(); //车号
                        string FS_CONTRACTNO = dtServerYB.Rows[i]["FS_CONTRACTNO"].ToString().Trim(); //引用采购合同表中的合同号，或空。
                        string FS_CONTRACTITEM = dtServerYB.Rows[i]["FS_CONTRACTITEM"].ToString().Trim(); //合同项目编号
                        string FS_SENDER = dtServerYB.Rows[i]["FS_SENDER"].ToString().Trim(); //发货方代码
                        string FS_SENDERSTORE = dtServerYB.Rows[i]["FS_SENDERSTORE"].ToString().Trim(); //发货方地点
                        string FS_MATERIAL = dtServerYB.Rows[i]["FS_MATERIAL"].ToString().Trim(); //物资代码
                        string FS_MATERIALNAME = dtServerYB.Rows[i]["FS_MATERIALNAME"].ToString().Trim(); //物料名称
                        string FS_RECEIVERFACTORY = dtServerYB.Rows[i]["FS_RECEIVERFACTORY"].ToString().Trim(); //收货方代码
                        string FS_RECEIVERSTORE = dtServerYB.Rows[i]["FS_RECEIVERSTORE"].ToString().Trim(); //收货库存点代码
                        string FS_TRANSNO = dtServerYB.Rows[i]["FS_TRANSNO"].ToString().Trim(); //承运方代码
                        string FS_WEIGHTTYPE = dtServerYB.Rows[i]["FS_WEIGHTTYPE"].ToString().Trim(); //流向代码
                        string FS_POUNDTYPE = dtServerYB.Rows[i]["FS_POUNDTYPE"].ToString().Trim(); //磅房编号
                        string FS_OVERTIME = dtServerYB.Rows[i]["FS_OVERTIME"].ToString().Trim(); //到期时间
                        string FN_SENDGROSSWEIGHT = dtServerYB.Rows[i]["FN_SENDGROSSWEIGHT"].ToString().Trim(); //预报总重
                        string FN_SENDTAREWEIGHT = dtServerYB.Rows[i]["FN_SENDTAREWEIGHT"].ToString().Trim(); //预报皮重
                        string FN_SENDNETWEIGHT = dtServerYB.Rows[i]["FN_SENDNETWEIGHT"].ToString().Trim(); //预报净量
                        string FS_STOVENO = dtServerYB.Rows[i]["FS_STOVENO"].ToString().Trim(); //炉号
                        string FN_BILLETCOUNT = dtServerYB.Rows[i]["FN_BILLETCOUNT"].ToString().Trim(); //支数,内部调拨钢坯时使用
                        string FS_PERSON = dtServerYB.Rows[i]["FS_PERSON"].ToString().Trim(); //录入员
                        string FS_POINT = dtServerYB.Rows[i]["FS_POINT"].ToString().Trim(); //录入点
                        string FS_DATETIME = dtServerYB.Rows[i]["FS_DATETIME"].ToString().Trim(); //录入时间
                        string FS_SHIFT = dtServerYB.Rows[i]["FS_SHIFT"].ToString().Trim(); //班次
                        string FS_TERM = dtServerYB.Rows[i]["FS_TERM"].ToString().Trim(); //班别
                        string FS_STATUS = dtServerYB.Rows[i]["FS_STATUS"].ToString().Trim(); //状态
                        string FN_TIMES = dtServerYB.Rows[i]["FN_TIMES"].ToString().Trim(); //计划车数
                        string FN_WEIGHTTIMES = dtServerYB.Rows[i]["FN_WEIGHTTIMES"].ToString().Trim(); //已计量车数
                        string FS_PLANUSER = dtServerYB.Rows[i]["FS_PLANUSER"].ToString().Trim(); //预报联系人
                        string FS_PLANTEL = dtServerYB.Rows[i]["FS_PLANTEL"].ToString().Trim(); //预报联系电话
                        string FS_LEVEL = dtServerYB.Rows[i]["FS_LEVEL"].ToString().Trim(); //是否允许保存汽车期限皮重 (1:为允许,0:为不允许)已改为是否需要卸货确认
                        string FS_IFSAMPLING = dtServerYB.Rows[i]["FS_IFSAMPLING"].ToString().Trim(); //是否需要取样确认 (1:为需要,0:为不需要)
                        string FS_IFACCEPT = dtServerYB.Rows[i]["FS_IFACCEPT"].ToString().Trim(); //是否需要验收确认 (1:为需要,0:为不需要)
                        string FS_DRIVERNAME = dtServerYB.Rows[i]["FS_DRIVERNAME"].ToString().Trim(); //驾驶员姓名
                        string FS_DRIVERIDCARD = dtServerYB.Rows[i]["FS_DRIVERIDCARD"].ToString().Trim(); //驾驶员身份证
                        string FS_DRIVERREMARK = dtServerYB.Rows[i]["FS_DRIVERREMARK"].ToString().Trim(); //备注(驾驶员等其它备注)

                        string DownYBSql = "insert into DT_WEIGHTPLAN_HISTORY(FS_PLANCODE,FS_CARDNUMBER,FS_CARNO,FS_CONTRACTNO,FS_CONTRACTITEM,FS_SENDER,FS_SENDERSTORE,";
                        DownYBSql += "FS_MATERIAL,FS_MATERIALNAME,FS_RECEIVERFACTORY,FS_RECEIVERSTORE,FS_TRANSNO,FS_WEIGHTTYPE,FS_POUNDTYPE,FS_OVERTIME,";
                        DownYBSql += "FN_SENDGROSSWEIGHT,FN_SENDTAREWEIGHT,FN_SENDNETWEIGHT,FS_STOVENO,FN_BILLETCOUNT,FS_PERSON,FS_POINT,FS_DATETIME,FS_SHIFT,";
                        DownYBSql += "FS_TERM,FS_STATUS,FN_TIMES,FN_WEIGHTTIMES,FS_PLANUSER,FS_PLANTEL,FS_LEVEL,FS_IFSAMPLING,FS_IFACCEPT,FS_DRIVERNAME,";
                        DownYBSql += "FS_DRIVERIDCARD,FS_DRIVERREMARK)";
                        DownYBSql += " VALUES('" + FS_PLANCODE + "','" + FS_CARDNUMBER + "','" + FS_CARNO + "','" + FS_CONTRACTNO + "',";
                        DownYBSql += "'" + FS_CONTRACTITEM + "','" + FS_SENDER + "','" + FS_SENDERSTORE + "','" + FS_MATERIAL + "','" + FS_MATERIALNAME + "',";
                        DownYBSql += "'" + FS_RECEIVERFACTORY + "','" + FS_RECEIVERSTORE + "','" + FS_TRANSNO + "','" + FS_WEIGHTTYPE + "','" + FS_POUNDTYPE + "',";
                        DownYBSql += "TO_DATE('" + FS_OVERTIME + "','yyyy-MM-dd HH24:mi:ss'),'" + FN_SENDGROSSWEIGHT + "','" + FN_SENDTAREWEIGHT + "',";
                        DownYBSql += "'" + FN_SENDNETWEIGHT + "','" + FS_STOVENO + "','" + FN_BILLETCOUNT + "','" + FS_PERSON + "',";
                        DownYBSql += "'" + FS_POINT + "',TO_DATE('" + FS_DATETIME + "','yyyy-MM-dd HH24:mi:ss'),'" + FS_SHIFT + "','" + FS_TERM + "',";
                        DownYBSql += "'" + FS_STATUS + "','" + FN_TIMES + "','" + FN_WEIGHTTIMES + "','" + FS_PLANUSER + "','" + FS_PLANTEL + "',";
                        DownYBSql += "'" + FS_LEVEL + "','" + FS_IFSAMPLING + "','" + FS_IFACCEPT + "','" + FS_DRIVERNAME + "','" + FS_DRIVERIDCARD + "',";
                        DownYBSql += "'" + FS_DRIVERREMARK + "')";

                        CoreClientParam ccpDownServerYB = new CoreClientParam();
                        ccpDownServerYB.ServerName = "ygjzjl.car.PredictInfo";
                        ccpDownServerYB.MethodName = "DownServerData";
                        ccpDownServerYB.ServerParams = new object[] { DownYBSql };
                        this.ExecuteNonQuery(ccpDownServerYB, CoreInvokeType.Internal);

                        if (ccpDownServerYB.ReturnCode != -1)
                        {
                            string strGuid = "'" + FS_PLANCODE + "'";
                            string strSql = "delete from DT_WEIGHTPLAN where  FS_PLANCODE = " + strGuid;
                            CoreClientParam ccpDelete = new CoreClientParam();
                            ccpDelete.ServerName = "ygjzjl.car.PredictInfo";
                            ccpDelete.MethodName = "DeleteData";
                            ccpDelete.ServerParams = new object[] { strSql };

                            this.ExecuteNonQuery(ccpDelete, CoreInvokeType.Internal);
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            return true;   
        }

        /// <summary>
        /// 查询预报信息
        /// </summary>
        private void QueryYBData()
        {

            //string strWhere = "select t.DEPARTID from CORE_APP_DEPARTMENT t ,CORE_APP_DEPARTMENT s where (t.PID = s.DEPARTID or t.PID = null) and t.PID = '" + str_DepartID + "'";

            string sql = "select FS_PlanCode,FS_CARDNUMBER,FS_CARNO,FS_ContractNo,FS_ContractItem,FHDW_BHTOMC(FS_SENDER) FS_SENDER,";
            sql += " Provider_BHTOMC(FS_Provider)FS_Provider,";
            sql += " FS_SenderStore,FS_Material,WL_BHTOMC(FS_Material) FS_MaterialName,SHDW_BHTOMC(FS_RECEIVERFACTORY) FS_RECEIVERFACTORY,";
            sql += " FS_ReceiverStore,CYDW_BHTOMC(FS_TransNo) FS_TransNo,";
            sql += " LX_BHTOMC(FS_WeightType) FS_WeightType,FS_PoundType,JLD_BHTOMC(FS_PoundType) FS_POINTNAME,";
            sql += " to_char(FS_OverTime,'yyyy-MM-dd HH24:mi:ss')as FS_OverTime,";
            sql += " FN_SendGrossWeight,FN_SendTareWeight,FN_SendNetWeight,FS_StoveNo,FN_BilletCount,FS_Person,FS_Point,";
            sql += " to_char(FS_Datetime,'yyyy-MM-dd HH24:mi:ss')as FS_Datetime,";
            sql += " FS_Shift,FS_Term,FS_Status,FN_Times,FN_WeightTimes,FS_PlanUser,FS_PlanTel,decode(FS_Level,0,'否',1,'是') FS_Level,";
            sql += " decode(FS_IFSAMPLING,0,'否',1,'是') FS_IFSAMPLING,";
            sql += " FS_IFACCEPT,FS_DRIVERNAME,FS_DRIVERIDCARD,FS_DRIVERREMARK,FS_STARTSIGN,FS_POINTID";
            sql += " from DT_WeightPlan where 1=1  ";

            DateTime beginTime = this.dateBegin.Value;
            DateTime endTime = this.dateEnd.Value;

            string beginTime_str = beginTime.ToString("yyyy-MM-dd 00:00:00");
            string endTime_str = endTime.ToString("yyyy-MM-dd 23:59:59");

            if (txt_HTH.Text.Trim() != "")
            {
                sql += " and FS_CARDNUMBER = '" + txt_HTH.Text.Trim() + "'"; //后改为卡号
            }
            sql += " and fs_datetime >= TO_DATE('" + beginTime_str + "','YYYY-MM-DD HH24:MI:SS')";
            sql += " and fs_datetime<= TO_DATE('" + endTime_str + "','YYYY-MM-DD HH24:MI:SS')";
            sql += " and fs_pointid is null";

            sql += " order by FS_Datetime desc";
            this.dataTable1.Rows.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { sql };
            //DataTable dt = new DataTable();
            //dt = dataTable1.Clone();
            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid1);
            try
            {

                foreach (UltraGridRow ugr in ultraGrid1.Rows)
                {
                    if (ugr.Cells["FN_TIMES"].Text.ToString() == "0")
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

        /// <summary>
        /// 染色表头编号
        /// </summary>
        /// <param name="markerGrid"></param>
        private void MarkGridCode(UltraGrid markerGrid)
        {
            for (int i = 0; i < markerGrid.Rows.Count; i++)
            {
                if (markerGrid.Rows[i].Cells["FS_STARTSIGN"].Text.Trim() == "1")
                {
                    markerGrid.Rows[i].Appearance.BackColor = Color.Yellow;
                }
                else
                {
                    markerGrid.Rows[i].Appearance.BackColor = Color.White;
                }
            }
        }

        private void btnXZYB_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"F:\昆钢项目\";  //指定打开文件默认路径
            openFileDialog.Filter = "Excel文件|*.xls";     //指定打开默认选择文件类型名
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                app = new Excel.Application();
                app.Visible = false;
                app.UserControl = true;
                Workbooks workbooks = app.Workbooks;
                object MissingValue = Type.Missing;
                workbook = workbooks.Add(openFileDialog.FileName);
                txtYBWJ.Text = openFileDialog.FileName;
            }
        }
        /// <summary>
        /// 读取并保存Excel中的信息到数据库中
        /// </summary>
        private void ReadAndSaveExcelInfo()
        {
            string val1 = "";
            string val2 = "";
            string val3 = "";
            string val4 = "";
            string val5 = "";
            string val6 = "";
            string val7 = "";
            string val8 = "";
            string val9 = "";

            string val10 = "";
            string val11 = "";
            string val12 = "";
            string val13 = "";
            string val14 = "";
            string val15 = "";
            string val16 = "";

            int num = 0;

            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
            Excel.Range range = worksheet.get_Range("A2", "P65535");//A2、O65535表示到Excel从第A列的第2行到第O列的第65535-1行  A2、P65535表示到Excel从第A列的第2行到第P列的第65535-1行 
            System.Array values = (System.Array)range.Formula;
            num = values.GetLength(0);

            //(OWC11.XlBorderWeight.xlThin);   //边框细线 
            //((Excel.Range)worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[3, 3])).Borders.Weight = 25; //单元格高度
            //((Excel.Range)worksheet.Cells[2, 2]).ColumnWidth = 20;  //单元格列宽度 
            //((Range)worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[3, 3])).MergeCells(true);  //合并单元格
            //((Range)worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[3, 3])).Font.Bold(true);   //字体粗体

            if (values.GetValue(1, 1).ToString().Trim() != "车证号" && values.GetValue(1, 2).ToString().Trim() != "车号" && values.GetValue(1, 3).ToString().Trim() != "合同号")
            {
                MessageBox.Show("预报选择错误，请重新选择Excel文件: '汽车衡预报模板'！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = 2; i <= num; i++)
            {
                //if (values.GetValue(i, 1).ToString().Trim() == "" && values.GetValue(i, 2).ToString().Trim() == "" && values.GetValue(i, 5).ToString().Trim() == "")
                //{
                //    num = i;
                //}
                if (values.GetValue(i, 1).ToString().Trim() == "" && values.GetValue(i, 2).ToString().Trim() == "")
                {
                    string b = values.GetValue(1, 2).ToString().Trim();
                    string a = values.GetValue(i, 2).ToString().Trim();
                    break;
                }
                else
                {
                    string sYBH = Guid.NewGuid().ToString();
                    val1 = values.GetValue(i, 1).ToString().Trim();//车证号
                    strCarNo = val1;  //用于车证卡验证
                    if (val1 != "" && !ValidateCarCardData())
                    {
                        MessageBox.Show("卡号" + "'" + val1 + "'" + "还未分配，请联系管理员或相关单位查明原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }

                    val2 = values.GetValue(i, 2).ToString().Trim();//车号
                    val3 = values.GetValue(i, 3).ToString().Trim();//合同号
                    val4 = values.GetValue(i, 4).ToString().Trim();//合同项目号
                    val5 = values.GetValue(i, 5).ToString().Trim();//物料名称
                    val6 = values.GetValue(i, 6).ToString().Trim();//发货单位
                    val7 = values.GetValue(i, 8).ToString().Trim();//收货单位
                    val8 = values.GetValue(i, 9).ToString().Trim();//承运单位
                    val9 = values.GetValue(i, 10).ToString().Trim();//流向

                    val10 = values.GetValue(i, 7).ToString().Trim();//发货地点
                    val11 = values.GetValue(i, 11).ToString().Trim();//是否保存期限皮重
                    val12 = values.GetValue(i, 12).ToString().Trim();//是否需要取样
                    val13 = values.GetValue(i, 13).ToString().Trim();//是否需要验收
                    val14 = values.GetValue(i, 14).ToString().Trim();//司机姓名
                    val15 = values.GetValue(i, 15).ToString().Trim();//司机身份证
                    val16 = values.GetValue(i, 16).ToString().Trim();//备注信息

                    GetBaseInfo a = new GetBaseInfo();
                    string strWLID = "";
                    string strFHDW = "";
                    string strSHDW = "";
                    string strCYDW = "";
                    if (values.GetValue(1, 5).ToString().Trim() == "物料名称")
                    {
                        Hashtable ht1 = new Hashtable();
                        ht1.Add("I1", "RGDR");
                        ht1.Add("I2", val5);
                        ht1.Add("I3", "SGLR");
                        ht1.Add("O4", "");
                        //a.ob = this.ob;
                        string sql = "{call KG_MCMS_BaseInfo.Get_WLCode(?,?,?,?)}";
                        CoreClientParam ccp1 = new CoreClientParam();
                        ccp1.ServerName = "com.dbComm.DBComm";
                        ccp1.MethodName = "executeProcedureBySql2";
                        ccp1.ServerParams = new object[] { sql, ht1 };
                        ccp1 = this.ExecuteNonQuery(ccp1, CoreInvokeType.Internal);

                        string strSql = "select t.* from it_material t where t.fs_materialname = '" + val5 + "'";
                        CoreClientParam ccp11 = new CoreClientParam();
                        ccp11.ServerName = "ygjzjl.car.CarCard";
                        ccp11.MethodName = "queryByClientSql";
                        ccp11.ServerParams = new Object[] { strSql };
                        System.Data.DataTable dt1 = new System.Data.DataTable();
                        ccp11.SourceDataTable = dt1;
                        this.ExecuteQueryToDataTable(ccp11, CoreInvokeType.Internal);
                        if (dt1.Rows.Count > 0)
                        {

                            strWLID = dt1.Rows[0]["FS_WL"].ToString();
                        }
                    }
                    if (values.GetValue(1, 6).ToString().Trim() == "发货单位")
                    {
                        Hashtable ht2 = new Hashtable();
                        ht2.Add("I1", "RGDR");
                        ht2.Add("I2", val6);
                        ht2.Add("I3", "SGLR");
                        ht2.Add("O4", "");
                        //a.ob = this.ob;
                        string sql = "{call KG_MCMS_BaseInfo.Get_GYCode(?,?,?,?)}";
                        CoreClientParam ccp2 = new CoreClientParam();
                        ccp2.ServerName = "com.dbComm.DBComm";
                        ccp2.MethodName = "executeProcedureBySql2";
                        ccp2.ServerParams = new object[] { sql, ht2 };
                        ccp2 = this.ExecuteNonQuery(ccp2, CoreInvokeType.Internal);

                        string strSql = "select t.* from it_supplier t where t.fs_suppliername  ='" + val6 + "'";
                        CoreClientParam ccp22 = new CoreClientParam();
                        ccp22.ServerName = "ygjzjl.car.CarCard";
                        ccp22.MethodName = "queryByClientSql";
                        ccp22.ServerParams = new Object[] { strSql };
                        System.Data.DataTable dt2 = new System.Data.DataTable();
                        ccp22.SourceDataTable = dt2;
                        this.ExecuteQueryToDataTable(ccp22, CoreInvokeType.Internal);
                        if (dt2.Rows.Count > 0)
                        {
                            strFHDW = dt2.Rows[0]["FS_GY"].ToString();
                        }
                    }
                    if (values.GetValue(1, 7).ToString().Trim() == "收货单位")
                    {
                        Hashtable ht3 = new Hashtable();
                        ht3.Add("I1", "RGDR");
                        ht3.Add("I2", val7);
                        ht3.Add("I3", "SGLR");
                        ht3.Add("O4", "");
                        //a.ob = this.ob;
                        string sql = "{call KG_MCMS_BaseInfo.Get_SHCode(?,?,?,?)}";
                        CoreClientParam ccp3 = new CoreClientParam();
                        ccp3.ServerName = "com.dbComm.DBComm";
                        ccp3.MethodName = "executeProcedureBySql2";
                        ccp3.ServerParams = new object[] { sql, ht3 };
                        ccp3 = this.ExecuteNonQuery(ccp3, CoreInvokeType.Internal);

                        string strSql = "select t.* from it_store t where t.fs_memo ='" + val7 + "'";
                        CoreClientParam ccp33 = new CoreClientParam();
                        ccp33.ServerName = "ygjzjl.car.CarCard";
                        ccp33.MethodName = "queryByClientSql";
                        ccp33.ServerParams = new Object[] { strSql };
                        System.Data.DataTable dt3 = new System.Data.DataTable();
                        ccp33.SourceDataTable = dt3;
                        this.ExecuteQueryToDataTable(ccp33, CoreInvokeType.Internal);
                        if (dt3.Rows.Count > 0)
                        {
                            strSHDW = dt3.Rows[0]["FS_SH"].ToString();
                        }
                    }
                    if (values.GetValue(1, 8).ToString().Trim() == "承运单位")
                    {
                        //SaveCYDWData();
                        Hashtable ht4 = new Hashtable();
                        ht4.Add("I1", "RGDR");
                        ht4.Add("I2", val8);
                        ht4.Add("I3", "SGLR");
                        ht4.Add("O4", "");
                        //a.ob = this.ob;
                        string sql = "{call KG_MCMS_BaseInfo.Get_CYCode(?,?,?,?)}";
                        CoreClientParam ccp4 = new CoreClientParam();
                        ccp4.ServerName = "com.dbComm.DBComm";
                        ccp4.MethodName = "executeProcedureBySql2";
                        ccp4.ServerParams = new object[] { sql, ht4 };
                        ccp4 = this.ExecuteNonQuery(ccp4, CoreInvokeType.Internal);

                        string strSql = "select t.* from bt_trans t where t.fs_transname  ='" + val8 + "'";
                        CoreClientParam ccp44 = new CoreClientParam();
                        ccp44.ServerName = "ygjzjl.car.CarCard";
                        ccp44.MethodName = "queryByClientSql";
                        ccp44.ServerParams = new Object[] { strSql };
                        System.Data.DataTable dt4 = new System.Data.DataTable();
                        ccp44.SourceDataTable = dt4;
                        this.ExecuteQueryToDataTable(ccp44, CoreInvokeType.Internal);
                        if (dt4.Rows.Count > 0)
                        {
                            strCYDW = dt4.Rows[0]["FS_CY"].ToString();
                        }
                    }



                    string strLX = "";
                    switch (val9)
                    {
                        case "进厂":
                            strLX = "001";
                            break;
                        case "厂际间发料":
                            strLX = "002";
                            break;
                        case "生产订单收货":
                            strLX = "003";
                            break;
                        case "出厂":
                            strLX = "004";
                            break;
                        case "厂际间转储":
                            strLX = "005";
                            break;
                        case "副产品收货":
                            strLX = "006";
                            break;
                        case "外协":
                            strLX = "007";
                            break;
                        case "其他":
                            strLX = "008";
                            break;

                        default:
                            strLX = "008";
                            break;
                    }

                    string strLRR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    string strLRD = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();

                    if (val11 == "否")
                    {
                        val11 = "0";
                    }
                    else
                    {
                        val11 = "1";
                    }

                    if (val12 == "否")
                    {
                        val12 = "0";
                    }
                    else
                    {
                        val12 = "1";
                    }

                    if (val13 == "否")
                    {
                        val13 = "0";
                    }
                    else
                    {
                        val13 = "1";
                    }


                    Hashtable param = new Hashtable();
                    param.Add("I1", sYBH);//预报号
                    param.Add("I2", val1);//卡号
                    param.Add("I3", val2);//车号
                    param.Add("I4", val3);//合同号
                    param.Add("I5", strWLID);//物料编号
                    param.Add("I6", strFHDW);//
                    param.Add("I7", strSHDW);//
                    param.Add("I8", strCYDW);//
                    param.Add("I9", strLX);//
                    param.Add("I10", val4);
                    param.Add("I11", strLRR);
                    param.Add("I12", str_DepartID);
                    param.Add("I13", strLRD);
                    param.Add("I14", val10);
                    param.Add("I15", val12);
                    param.Add("I16", val13);
                    param.Add("I17", val11);
                    param.Add("I18", val14);
                    param.Add("I19", val15);
                    param.Add("I20", val16);
                    param.Add("O21", val16);
                    //param.Add("I20", val5);//物料名称
                    string[] result = this.SaveExclDate(param);

                    //CoreClientParam ccp = new CoreClientParam();
                    //ccp.ServerName = "ygjzjl.car.PredictInfo";
                    //ccp.MethodName = "SaveExcelData";
                    //ccp.ServerParams = new object[] { sYBH, val1, val2, val3, strWLID, strFHDW, strSHDW, strCYDW, strLX, val4, strLRR, strLRD,
                    //val10, val11, val12, val13, val14, val15, val16};
                    //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
            }
        }

        public string[] SaveExclDate(Hashtable param)
        {
            CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_PredictInfo.SaveExcelData(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}", param);

            string result = "";
            string message = "";
            string weightNo = "";
            if (ccp.ReturnObject != null && !string.IsNullOrEmpty(ccp.ReturnObject.ToString()))
            {
                object[] results = (object[])ccp.ReturnObject;
                for (int i = 0; i < results.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            result = results[i] != null ? results[i].ToString() : "";
                            break;
                        case 1:
                            weightNo = results[i] != null ? results[i].ToString() : "";
                            break;
                        case 2:
                            message = results[i] != null ? results[i].ToString() : "";
                            break;
                    }
                }
            }
            return new string[] { result, weightNo, message };
        }

        //数据保存完整性检查
        private bool check()
        {

            if (this.cbCH1.Text.Trim() == "" && this.cbCH2.Text.Trim() == "" && this.txtCZH.Text == "")
            {
                MessageBox.Show("卡号和车号不能同时为空，请输入其中一项！");
                this.cbCH1.Focus();
                return false;
            }

            //if (this.cbCH1.Text.Trim() == "")
            //{
            //    MessageBox.Show("请填写车号");
            //    this.cbCH1.Focus();
            //    return false;
            //}

            //if (this.cbCH2.Text.Trim() == "")
            //{
            //    MessageBox.Show("请填写车号");
            //    this.cbCH2.Focus();
            //    return false;
            //}

            if (this.cbBF.SelectedValue == null || this.cbBF.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择磅房信息！");
                this.cbBF.Focus();
                return false;

            }

            if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
            {

                MessageBox.Show("请选择物料信息！");
                cbWLMC.Focus();
                return false;


            }

            if (this.cbLX.SelectedValue == null || this.cbLX.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择流向信息！");
                cbLX.Focus();
                return false;
            }
            if (this.cbFHDW.SelectedValue == null || this.cbFHDW.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择发货单位信息！");
                cbFHDW.Focus();
                return false;
            }
            if (this.cbSHDW.SelectedValue == null || this.cbSHDW.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择收货单位信息！");
                cbSHDW.Focus();
                return false;
            }
            if (this.cbCYDW.SelectedValue == null || this.cbCYDW.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择承运单位信息！");
                cbCYDW.Focus();
                return false;
            }
            if (this.txtJSYXM.Text.Trim() != "" && this.txtJSYSFZ.Text.Trim() == "")
            {
                MessageBox.Show("请输入身份证！");
                //if (!this.SFZNumber())
                //{
                //    MessageBox.Show("身份证正确！");
                //    txtJSYSFZ.Focus();
                //    return false;
                //}
                txtJSYSFZ.Focus();
                return false;
            }
            if (this.txtJSYXM.Text.Trim() == "" && this.txtJSYSFZ.Text.Trim() != "")
            {
                MessageBox.Show("请输入姓名！");
                txtJSYXM.Focus();
                return false;
            }
            if (this.txtJSYSFZ.Text.Trim() != "")
            {
                if (!this.SFZNumber(this.txtJSYSFZ.Text.Trim()))
                {
                    MessageBox.Show("身份证不正确！");
                    txtJSYSFZ.Focus();
                    return false;
                }
            }
            if (this.txtYBJZ.Text.Trim() != "")
            {
                if (!this.zlNumber(this.txtYBJZ.Text.Trim()))
                {
                    MessageBox.Show("重量输入有误，确保小数点后有两位数字");
                    txtYBJZ.Focus();
                    return false;
                }
            }
            return true;
        }

        //保存预报信息
        private void SavePredictInfo()
        {
            string m_FS_PLANCODE = DateTime.Now.ToString("yyyyMMddHHmmss").ToString();
            s_FS_PLANCODE = m_FS_PLANCODE;
            string m_FS_CARDNUMBER = this.txtCZH.Text.Trim();
            string m_FS_CARNO = this.cbCH1.Text.Trim() + this.cbCH2.Text.Trim();
            string m_FS_CONTRACTNO = this.txtHTH.Text.Trim();
            string m_FS_CONTRACTITEM = this.txtHTXMH.Text.Trim();

            string m_FS_SENDER = this.cbFHDW.SelectedValue.ToString().Trim();
            string m_FS_SENDERSTORE = this.cbFHDWKCD.Text.Trim();
            string m_FS_MATERIAL = "";

            m_FS_MATERIAL = this.cbWLMC.SelectedValue.ToString().Trim();

            string m_FS_MATERIALNAME = this.cbWLMC.Text.ToString().Trim();
            string m_FS_RECEIVERFACTORY = this.cbSHDW.SelectedValue.ToString().Trim();

            string m_FS_RECEIVERSTORE = this.cbReceiverStore.Text.Trim();
            string m_FS_TRANSNO = this.cbCYDW.SelectedValue.ToString().Trim();
            string m_FS_WEIGHTTYPE = this.cbLX.SelectedValue.ToString().Trim();
            string m_FS_POUNDTYPE = this.cbBF.SelectedValue.ToString().Trim();
            string m_FN_SENDGROSSWEIGHT = this.txtYBZZ.Text.Trim();

            string m_FN_SENDTAREWEIGHT = this.txtYBPZ.Text.Trim();
            string m_FN_SENDNETWEIGHT = this.txtYBJZ.Text.Trim();
            string m_FS_STOVENO = this.txtLH.Text.Trim();
            string m_FN_BILLETCOUNT = this.txtZS.Text.Trim();
            string m_FS_PERSON = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            string m_FS_POINT = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
            string m_FS_SHIFT = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder();//班次
            //string m_FS_SHIFT = "";
            string m_FS_TERM = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup();//班组
            string m_FN_TIMES = "1";
            string m_FN_WEIGHTTIMES = "";
            if (this.txt_jhcs.Text.Trim() != "")
            {
                m_FN_TIMES = this.txt_jhcs.Text.Trim();
                m_FN_WEIGHTTIMES = "";
            }
            string m_FS_PLANUSER = "";
            string m_FS_POINTID = "";
            string m_FS_STATUS = "0";
            if (this.cb_JQPLAN.Checked == true)
            {
                comboBox1.Enabled = true;
                this.comboBox1.Enabled = true;
                m_FS_STATUS = "3";
                if (this.comboBox1.Text.Trim() == "")
                {
                    MessageBox.Show("请输入预报使用编号！");
                    return;
                }
                m_FN_TIMES = "";
                m_FS_POINTID = this.comboBox1.Text.Trim();
                m_FS_PLANCODE = m_FS_PLANCODE + "-" + this.comboBox1.Text.Trim() + "#";
            }


            string m_FS_PLANTEL = "";

            string m_FS_LEVEL = "0"; //卸车确认 1为需要
            if (this.checkBox3.Checked == true)
            {
                m_FS_LEVEL = "1";
            }
            string m_FS_IFSAMPLING = "0";//是否使用期限皮重 1为使用
            if (this.checkBox1.Checked == true)
            {
                m_FS_IFSAMPLING = "1";
            }
            string m_FS_IFACCEPT = "";
            string m_FS_DRIVERNAME = this.txtJSYXM.Text.Trim();

            string m_FS_DRIVERIDCARD = this.txtJSYSFZ.Text.Trim();
            string m_FS_DRIVERREMARK = this.txtBZ.Text.Trim();

            string m_FS_PROVIDER = this.cbProvider.SelectedValue.ToString();
            string m_FS_DEPARTID = str_DepartID;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "SavePredictInfo";
            ccp.ServerParams = new object[] {
                                             m_FS_PLANCODE, m_FS_CARDNUMBER, m_FS_CARNO, m_FS_CONTRACTNO, m_FS_CONTRACTITEM,//1
                                             m_FS_SENDER, m_FS_SENDERSTORE, m_FS_MATERIAL, m_FS_MATERIALNAME, m_FS_RECEIVERFACTORY,//2
                                             m_FS_RECEIVERSTORE, m_FS_TRANSNO, m_FS_WEIGHTTYPE, m_FS_POUNDTYPE,m_FN_SENDGROSSWEIGHT,//3
                                             m_FN_SENDTAREWEIGHT, m_FN_SENDNETWEIGHT, m_FS_STOVENO,m_FN_BILLETCOUNT, m_FS_PERSON,//4
                                             m_FS_POINT, m_FS_SHIFT, m_FN_TIMES, m_FN_WEIGHTTIMES,m_FS_PLANUSER, //5
                                             m_FS_PLANTEL, m_FS_LEVEL, m_FS_IFSAMPLING, m_FS_IFACCEPT,m_FS_DRIVERNAME, //6
                                             m_FS_DRIVERIDCARD, m_FS_DRIVERREMARK,m_FS_PROVIDER,m_FS_STATUS,m_FS_TERM,//7
                                             m_FS_POINTID,m_FS_DEPARTID//8
                    };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //Hashtable param = new Hashtable();
            //param.Add("I1", m_FS_PLANCODE);
            //param.Add("I2", m_FS_CARDNUMBER);
            //param.Add("I3", m_FS_CARNO);
            //param.Add("I4", m_FS_CONTRACTNO);
            //param.Add("I5", m_FS_CONTRACTITEM);

            //param.Add("I6", m_FS_SENDER);
            //param.Add("I7", m_FS_SENDERSTORE);
            //param.Add("I8", m_FS_MATERIAL);
            //param.Add("I9", m_FS_MATERIALNAME);
            //param.Add("I10", m_FS_RECEIVERFACTORY);

            //param.Add("I11", m_FS_RECEIVERSTORE);
            //param.Add("I12", m_FS_TRANSNO);
            //param.Add("I13", m_FS_WEIGHTTYPE);
            //param.Add("I14", m_FS_POUNDTYPE);
            //param.Add("I15", m_FN_SENDGROSSWEIGHT);

            //param.Add("I16", m_FN_SENDTAREWEIGHT);
            //param.Add("I17", m_FN_SENDNETWEIGHT);
            //param.Add("I18", m_FS_STOVENO);
            //param.Add("I19", m_FN_BILLETCOUNT);
            //param.Add("I20", m_FS_PERSON);

            //param.Add("I21", m_FS_POINT);
            //param.Add("I22", m_FS_SHIFT);
            //param.Add("I23", m_FN_TIMES);
            //param.Add("I24", m_FN_WEIGHTTIMES);
            //param.Add("I25", m_FS_PLANUSER);

            //param.Add("I26", m_FS_PLANTEL);
            //param.Add("I27", m_FS_LEVEL);
            //param.Add("I28", m_FS_IFSAMPLING);
            //param.Add("I29", m_FS_IFACCEPT);
            //param.Add("I30", m_FS_DRIVERNAME);

            //param.Add("I31", m_FS_DRIVERIDCARD);
            //param.Add("I32", m_FS_DRIVERREMARK);
            //param.Add("I33", m_FS_PROVIDER);
            //param.Add("I34", m_FS_STATUS);
            //param.Add("I35", m_FS_TERM);
            //param.Add("I36", m_FS_POINTID);
            //string[] result = this.SaveWeightData(param);

        }

        /// <summary>
        /// 修改预报信息
        /// </summary>
        private void UpdateOldData()
        {

            if (this.cbBF.SelectedValue == null)
            {
                MessageBox.Show("请选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBF.Focus();
                return;
            }
            string strYBH = this.txtYBH.Text.Trim();
            s_FS_PLANCODE = this.txtYBH.Text.Trim();
            string strCZH = this.txtCZH.Text.Trim();
            string strCH = "";
            string strCH1 = this.cbCH1.Text.Trim();
            string strCH2 = this.cbCH2.Text.Trim();
            strCH = strCH1 + strCH2;
            string strHTH = this.txtHTH.Text.Trim();
            string strHTXMH = this.txtHTXMH.Text.Trim();
            if (txtHTXMH.Text == "" && txtHTH.Text != "")
            {
                strHTXMH = "00010";
            }
            string strWLID = "";

            if (cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
            {
                if (this.cbWLMC.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("请选择物料名称！");
                    return;
                }
            }
            else
            {
                strWLID = this.cbWLMC.SelectedValue.ToString().Trim();
            }
            string strWLMC = this.cbWLMC.Text.Trim();
            string strZS = this.txtZS.Text.Trim();

            string strFHDW = "";

            if (this.cbFHDW.SelectedValue == null || this.cbFHDW.SelectedValue.ToString().Trim() == "")
            {
                if (this.cbFHDW.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("请选择发货单位！");
                    return;
                }
            }
            else
            {
                strFHDW = this.cbFHDW.SelectedValue.ToString().Trim();
            }

            string strSHDW = "";

            if (cbSHDW.SelectedValue == null || this.cbSHDW.SelectedValue.ToString().Trim() == "")
            {
                if (this.cbFHDW.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("请选择收货单位！");
                    return;
                }
            }
            else
            {
                strSHDW = this.cbSHDW.SelectedValue.ToString().Trim();
            }

            string strCYDW = "";

            if (this.cbCYDW.SelectedValue == null || this.cbCYDW.SelectedValue.ToString().Trim() == "")
            {
                if (this.cbCYDW.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("请选择承运单位！");
                    return;
                }
            }
            else
            {
                strCYDW = this.cbCYDW.SelectedValue.ToString().Trim();
            }

            string strFHDWKCD = this.cbFHDWKCD.Text.Trim();
            //string strSHDWKCD = this.cbSHDWKCD.Text.Trim();
            string strSHDWKCD = this.cbReceiverStore.Text.Trim();
            string strLX = "";
            if (this.cbLX.SelectedValue == null)
            {
                strLX = "";
                MessageBox.Show("流向不能输入，请选择流向！");
                cbLX.Text = "";
                cbLX.Focus();
                return;
            }
            else
            {
                strLX = this.cbLX.SelectedValue.ToString().Trim();
            }
            string strHQLX = this.cbBF.SelectedValue.ToString().Trim();
            string strLH = this.txtLH.Text.Trim();
            string strYBZZ = this.txtYBZZ.Text.Trim();
            string strYBPZ = this.txtYBPZ.Text.Trim();
            string strYBJZ = this.txtYBJZ.Text.Trim();
            string strJHCS = this.txt_jhcs.Text.Trim();
            string strYJLCS = this.txtYJLCS.Text.Trim();
            string strBC = this.cbBC.Text.Trim();
            string strYBLXR = this.cbYBLXR.Text.Trim();
            string strYBLXDH = this.cbYBLXDH.Text.Trim();
            string strXHQR = this.cbYXJ.Text.Trim();
            string strLRY = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            string strLRD = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();

            string strDQSJ = "";
            string strLRSJ = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //卸货确认
            if (this.checkBox3.Checked == true)
            {
                strXHQR = "1";
            }
            else
            {
                strXHQR = "0";
            }

            string strSFQXPZ = "";
            string strSFYS = "";
            string strJSYXM = "";
            string strJSYSFZ = "";
            string strBZ = "";

            //期限皮重
            if (checkBox1.Checked == true)
            {
                strSFQXPZ = "1";
            }
            else
            {
                strSFQXPZ = "0";
            }


            strJSYXM = this.txtJSYXM.Text.Trim();
            strJSYSFZ = this.txtJSYSFZ.Text.Trim();
            strBZ = txtBZ.Text.Trim();

            string m_FS_PROVIDER = "";

            if (this.cbProvider.SelectedValue != null)
                m_FS_PROVIDER = this.cbProvider.SelectedValue.ToString();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "UpdateYBData";
            ccp.ServerParams = new object[] {strYBH, strCZH, strCH, strHTH, strHTXMH, strWLID, strWLMC, strZS, strFHDW, strSHDW, strCYDW, strFHDWKCD, strSHDWKCD, 
                strLX, strHQLX, strLH, strYBZZ, strYBPZ, strYBJZ, strJHCS, strYJLCS, strBC, strYBLXR, strYBLXDH, strXHQR, strLRY, strLRD, strDQSJ, strLRSJ,
                strSFQXPZ, strSFYS, strJSYXM, strJSYSFZ, strBZ, m_FS_PROVIDER};
            ccp.IfShowErrMsg = false;
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //string a = ccp.ReturnObject.ToString();

            string errInfo = "";
            if (ccp.ReturnCode == -1)
            {
                errInfo = ccp.ReturnInfo;
            }
            if (errInfo != "")
            {
                if (errInfo.IndexOf("ORA-01401") >= 0)
                {
                    MessageBox.Show("值输入过大，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbCH1.Focus();
                    return;
                }
            }

            //AddCarNO();
            SaveDriverData();
            SaveSendPlaceData();
            this.QueryYBData();
            ActiveNewRow();
            ClearControl();
        }

        /// <summary>
        /// 删除预报信息
        /// </summary>
        private void DeleteOldData()
        {
            string strYBH = ultraGrid1.ActiveRow.Cells["FS_PLANCODE"].Text.Trim();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "DeleteYBData";
            ccp.ServerParams = new object[] { strYBH };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            this.QueryYBData();
            ClearControl();
        }

        /// <summary>
        /// 新增修改显示新列
        /// </summary>
        /// <param  ></param>
        /// <returns></returns>
        private void ActiveNewRow()
        {
            if (ultraGrid1.Rows.Count > 0)
            {
                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    if (ultraGrid1.Rows[i].Cells["FS_PLANCODE"].Text == this.txtYBH.Text.Trim())
                    {
                        ultraGrid1.ActiveRow = ultraGrid1.Rows[i];
                        ultraGrid1.Rows[i].Selected = true;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 清空控件内容
        /// </summary>
        private void ClearControl()
        {
            this.txtCZH.Text = "";
            cbCH1.Text = "";
            cbCH2.Text = "";
            txtHTH.Text = "";
            this.cbBF.Text = "";
            this.cbProvider.Text = "";
            //txtHTXMH.SelectedText = "";
            dtHTH.Clear();
            txtHTXMH.Text = "";
            //chbQXPZ.Checked = false;
            checkBox1.Checked = false;
            checkBox3.Checked = false;
            //txtLH.Text = "";
            //txtZS.Text = "";
            cbFHDW.Text = "";
            cbSHDW.Text = "";
            cbCYDW.Text = "";
            cbWLMC.Text = "";
            cbLX.Text = "";
            txtYBJZ.Text = "";
            //txtYBPZ.Text = "";
            //txtYBZZ.Text = "";
            cbFHDWKCD.Text = "";
            txtJSYXM.Text = "";
            txtJSYSFZ.Text = "";
            txtBZ.Text = "";
            this.cbReceiverStore.Text = "";
            this.cb_JQPLAN.Checked = false;
            this.comboBox1.Text = "";
            this.txt_jhcs.Text = "";
        }

        #region 下拉框绑定
        /// <summary>
        /// 从数据库获取车号数据
        /// </summary>
        private void GetCHData()
        {
            string strJLDID = "";
            if (cbBF.Text.Trim() != "")
            {
                strJLDID = cbBF.SelectedValue.ToString().Trim();
            }
            string strSql = "select DISTINCT A.FS_CARNO,A.FN_TIMES FROM BT_POINTCARNO A WHERE A.FS_POINTNO = '" + strJLDID + "' ORDER BY A.FN_TIMES DESC";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryCHData";
            ccp.ServerParams = new object[] { strSql };

            System.Data.DataTable dtCH = new System.Data.DataTable();

            ccp.SourceDataTable = dtCH;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtCH.Rows.Count > 0)
            {

                DataRow dr = dtCH.NewRow();
                dtCH.Rows.InsertAt(dr, 0);

                cbCH1.DataSource = dtCH;
                cbCH1.DisplayMember = "FS_CARNO";

                cbCH1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbCH1.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }
        /// <summary>
        /// 从数据库获取车号2数据
        /// </summary>
        private void GetCH2Data()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryCHData";

            System.Data.DataTable dtCH2 = new System.Data.DataTable();

            ccp.SourceDataTable = dtCH2;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtCH2.Rows.Count > 0)
            {
                for (int i = 0; i <= dtCH2.Rows.Count - 1; i++)
                {
                    string CH = dtCH2.Rows[i]["FS_CARNO"].ToString().Trim();
                    string CH2 = CH.Substring(1);
                    dtCH2.Rows[i]["FS_CARNO"] = CH2;
                    dtCH2.AcceptChanges();
                }
                DataRow dr = dtCH2.NewRow();
                dtCH2.Rows.InsertAt(dr, 0);

                cbCH2.DataSource = dtCH2;
                cbCH2.DisplayMember = "FS_CARNO";

                cbCH2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbCH2.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }
        /// <summary>
        /// 从数据库获取磅房数据
        /// </summary>
        private void GetBFData()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryBFData";

            System.Data.DataTable dtBF = new System.Data.DataTable();

            ccp.SourceDataTable = dtBF;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtBF.Rows.Count > 0)
            {
                DataRow dr = dtBF.NewRow();
                dtBF.Rows.InsertAt(dr, 0);

                cbBF.DataSource = dtBF;
                cbBF.DisplayMember = "FS_POINTNAME";
                cbBF.ValueMember = "FS_POINTCODE";
            }
        }
        /// <summary>
        /// 从数据库获取物料数据
        /// </summary>
        private void GetWLData()
        {
            string strBFID = "";
            if (cbBF.SelectedValue.ToString().Trim() != "")
            {
                strBFID = cbBF.SelectedValue.ToString().Trim();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryWLData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtWL = new System.Data.DataTable();

            ccp.SourceDataTable = dtWL;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtWL.Rows.Count > 0)
            {
                DataRow dr = dtWL.NewRow();
                dtWL.Rows.InsertAt(dr, 0);

                cbWLMC.DataSource = dtWL;
                cbWLMC.DisplayMember = "FS_MATERIALNAME";
                cbWLMC.ValueMember = "FS_MATERIALNO";

                cbWLMC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbWLMC.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbWLMC.DataSource = dtWL;
            }
        }
        /// <summary>
        /// 从数据库获取流向数据
        /// </summary>
        private void GetLXData()
        {
            //string strBFID = cbBF.SelectedValue.ToString().Trim();
            string strBFID = "";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryLXData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtLX = new System.Data.DataTable();

            ccp.SourceDataTable = dtLX;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtLX.Rows.Count > 0)
            {
                DataRow dr = dtLX.NewRow();
                dtLX.Rows.InsertAt(dr, 0);

                cbLX.DataSource = dtLX;
                cbLX.DisplayMember = "FS_TYPENAME";
                cbLX.ValueMember = "FS_TYPECODE";

                cbLX.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbLX.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbLX.DataSource = dtLX;
            }
        }
        /// <summary>
        /// 从数据库获取发货单位数据
        /// </summary>
        private void GetFHDWData()
        {
            string strBFID = "";
            if (cbBF.SelectedValue.ToString().Trim() != "")
            {
                strBFID = cbBF.SelectedValue.ToString().Trim();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryFHDWData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtFHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtFHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtFHDW.Rows.Count > 0)
            {
                DataRow dr = dtFHDW.NewRow();
                dtFHDW.Rows.InsertAt(dr, 0);

                cbFHDW.DataSource = dtFHDW;
                cbFHDW.DisplayMember = "FS_SUPPLIERNAME";
                cbFHDW.ValueMember = "FS_SUPPLIER";

                cbFHDW.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbFHDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbFHDW.DataSource = dtFHDW;
            }
        }
        /// <summary>
        /// 从数据库获取收货单位数据
        /// </summary>
        private void GetSHDWData()
        {
            string strBFID = "";
            if (cbBF.SelectedValue.ToString().Trim() != "")
            {
                strBFID = cbBF.SelectedValue.ToString().Trim();
            }
            //string strBFID = cbBF.SelectedValue.ToString().Trim();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QuerySHDWData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtSHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtSHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtSHDW.Rows.Count > 0)
            {
                DataRow dr = dtSHDW.NewRow();
                dtSHDW.Rows.InsertAt(dr, 0);

                cbSHDW.DataSource = dtSHDW;
                cbSHDW.DisplayMember = "FS_MEMO";
                cbSHDW.ValueMember = "FS_RECEIVER";

                cbSHDW.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbSHDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbSHDW.DataSource = dtSHDW;
            }
        }
        /// <summary>
        /// 从数据库获取承运单位数据
        /// </summary>
        private void GetCYDWData()
        {
            string strBFID = "";
            if (cbBF.SelectedValue.ToString().Trim() != "")
            {
                strBFID = cbBF.SelectedValue.ToString().Trim();
            }
            //string strBFID = cbBF.SelectedValue.ToString().Trim();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryCYDWData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtCYDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtCYDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtCYDW.Rows.Count > 0)
            {
                DataRow dr = dtCYDW.NewRow();
                dtCYDW.Rows.InsertAt(dr, 0);

                cbCYDW.DataSource = dtCYDW;

                cbCYDW.DisplayMember = "FS_TRANSNAME";
                cbCYDW.ValueMember = "FS_TRANSNO";

                cbCYDW.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbCYDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbCYDW.DataSource = dtCYDW;
            }
        }
        /// <summary>
        /// 磅房选择框值发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBF.Text == "System.Data.DataRowView" || cbBF.Text == "" || cbBF.SelectedValue == null)
            {
                return;
            }
            if (this.cbWLMC.Text.Trim() == "")
            {
                string strPointID = cbBF.SelectedValue.ToString().Trim();
                this.BandPointMaterial(strPointID); //绑定磅房物料
                this.BandPointReceiver(strPointID); //绑定磅房收货单位
                this.BandPointSender(strPointID); //绑定磅房发货单位
                this.BandPointTrans(strPointID); //绑定磅房承运单位
                //this.BandPointCarNo(strPointID);//绑定磅房车号

                this.BandPointProvider(strPointID); //绑定磅房发货单位
            }
            //GetCHData();
            //GetWLData();
            //GetLXData();
            //GetFHDWData();
            //GetSHDWData();
            //GetCYDWData();
        }
        /// <summary>
        /// 回车键、删除键控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PredictInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //回车
            if (e.KeyChar == (char)13)
            {
                Control c = GetNextControl(this.ActiveControl, true);
                bool ok = SelectNextControl(this.ActiveControl, true, true, true, true);
                if (ok && c != null)
                {
                    if (c is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)c).SelectAll();
                    }
                }
            }
            //删除
            //if (e.KeyChar == (char)46)
            //{
            //    if (ultraGrid1.Rows.Count > 0)
            //    {
            //        if (ultraGrid1.ActiveRow != null || ultraGrid1.ActiveRow.Selected != false)
            //        {
            //            string strYBH = ultraGrid1.ActiveRow.Cells["FS_PLANCODE"].Text.Trim();

            //            CoreClientParam ccp = new CoreClientParam();
            //            ccp.ServerName = "ygjzjl.car.PredictInfo";
            //            ccp.MethodName = "DeleteYBData";
            //            ccp.ServerParams = new object[] { strYBH };

            //            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //            this.QueryYBData("1");
            //            ClearControl();
            //        }
            //    }
            //}
        }

        private bool ControlProve()
        {
            if (cbBF.Text == "")
            {
                MessageBox.Show("请选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbBF.Focus();
                return false;
            }
            if (cbWLMC.Text == "")
            {
                MessageBox.Show("请选择物料信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbWLMC.Focus();
                return false;
            }
            //if (cbWLMC.SelectedValue.ToString().Trim() == "")
            //{
            //    MessageBox.Show("请重新选择物料信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbWLMC.Focus();
            //    return false;
            //}
            if (cbLX.Text == "")
            {
                MessageBox.Show("请选择流向信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLX.Focus();
                return false;
            }
            if (cbFHDW.Text == "")
            {
                MessageBox.Show("请选择发货单位信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFHDW.Focus();
                return false;
            }
            if (cbSHDW.Text == "")
            {
                MessageBox.Show("请选择收货单位信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSHDW.Focus();
                return false;
            }
            if (cbCYDW.Text == "")
            {
                MessageBox.Show("请选择承运单位信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCYDW.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region 插入新数据
        /// <summary>
        /// 往表中插入新的物料信息
        /// </summary>
        private void SaveWLData()
        {
            string inPointID = this.cbBF.SelectedValue.ToString().Trim();
            string inWLMC = this.cbWLMC.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "SaveWLData";
            ccp.ServerParams = new object[] { inPointID, inWLMC, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sWLID = ccp.ReturnObject.ToString();
        }
        /// <summary>
        /// 往表中插入新的发货单位信息
        /// </summary>
        private void SaveFHDWData()
        {
            string inPointID = this.cbBF.SelectedValue.ToString().Trim();
            string inFHDW = this.cbFHDW.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "SaveFHDWData";
            ccp.ServerParams = new object[] { inPointID, inFHDW, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sFHDWID = ccp.ReturnObject.ToString();
        }
        /// <summary>
        /// 往表中插入新的收货单位信息
        /// </summary>
        private void SaveSHDWData()
        {
            string inPointID = this.cbBF.SelectedValue.ToString().Trim();
            string inSHDW = this.cbSHDW.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "SaveSHDWData";
            ccp.ServerParams = new object[] { inPointID, inSHDW, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sSHDWID = ccp.ReturnObject.ToString();
        }
        /// <summary>
        /// 往表中插入新的承运单位信息
        /// </summary>
        private void SaveCYDWData()
        {
            string inPointID = this.cbBF.SelectedValue.ToString().Trim();
            string inCYDW = this.cbCYDW.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "SaveCYDWData";
            ccp.ServerParams = new object[] { inPointID, inCYDW, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sCYDWID = ccp.ReturnObject.ToString();
        }
        #endregion

        #region 从SAP下载合同号、合同项目号、物料信息
        private void DownSapData()
        {
            htxmh = "";
            dtHTH.Rows.Clear();
            //string rfc = "ZJL_PO_DOWN";//sap方法名
            string hth = txtHTH.Text.ToString();

            CoreClientParam ccpHTH = new CoreClientParam();
            ccpHTH.ServerName = "ygjzjl.car.SapOperation";
            ccpHTH.MethodName = "queryContractNo";
            ccpHTH.ServerParams = new object[] { hth };

            ccpHTH.SourceDataTable = dtHTH;
            this.ExecuteQueryToDataTable(ccpHTH, CoreInvokeType.Internal);

            if (dtHTH.Rows.Count > 0)
            {
                txtHTXMH.DataSource = dtHTH;
                txtHTXMH.DisplayMember = "FS_ITEMNO";

                txtHTXMH.Text = dtHTH.Rows[0]["FS_ITEMNO"].ToString();
                cbWLMC.Text = dtHTH.Rows[0]["FS_MATERIALNAME"].ToString();
                //cbWLMC.SelectedValue = dtHTH.Rows[0]["FS_WL"].ToString();
                htxmh = "1";
            }
        }

        private void txtHTH_Leave(object sender, EventArgs e)
        {
            //DownSapData();//下载合同信息时报错
        }

        private void txtHTXMH_TextChanged(object sender, EventArgs e)
        {
            //int i = txtHTXMH.Text.IndexOf(txtHTXMH.Text);//在为DropDownList时能取到索引
            int i = txtHTXMH.SelectedIndex;
            //string aa = txtHTXMH.Text;
            //DataRow dr = dtHTH.NewRow();
            //dr[0] = aa; 
            //int k = dtHTH.Rows.IndexOf(dr);
            if (htxmh != "" && dtHTH.Rows.Count > 0 && i != -1)
            {
                cbWLMC.Text = dtHTH.Rows[i]["FS_MATERIALNAME"].ToString();
            }
            if (i == -1)
            {
                cbWLMC.Text = "";
            }
        }
        #endregion

        /// <summary>
        /// 车证卡验证
        /// </summary>
        private bool ValidateCarCardData()
        {
            string sql = "";
            if (txtCZH.Text.Trim() != "")
            {
                sql = "SELECT A.FS_CARDLEVEL,A.FS_ISVALID FROM BT_CARDMANAGE A WHERE A.FS_SEQUENCENO = '" + txtCZH.Text.Trim() + "'";
            }
            else
            {
                sql = "SELECT A.FS_CARDLEVEL,A.FS_ISVALID FROM BT_CARDMANAGE A WHERE A.FS_SEQUENCENO = '" + strCarNo + "'";
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "ValidateCarCardData";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtCarNo = new System.Data.DataTable();
            ccp.SourceDataTable = dtCarNo;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            string strBZ = "";
            if (dtCarNo.Rows.Count > 0)
            {
                strBZ = dtCarNo.Rows[0]["FS_ISVALID"].ToString();
            }
            if (strBZ == "2" || strBZ == "4")
            {
                return true;
            }
            return false;
        }

        private void PredictInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (app != null)
            {
                app.Quit();
            }
        }

        /// <summary>
        /// 保存驾驶员信息到本地
        /// </summary>
        private void SaveDriverData()
        {
            //string strExistFiles = "";
            //string m_RunPath = System.Environment.CurrentDirectory;
            if (txtJSYXM.Text.Length <= 0 || txtJSYSFZ.Text.Length <= 0)
            {
                return;
            }
            if (txtJSYSFZ.Text.Length != 15 && txtJSYSFZ.Text.Length != 18)
            {
                return;
            }

            if (System.IO.File.Exists(m_RunPath + "\\tempData\\DownLoadDriverFile.txt"))
            {
                strExistFiles = System.IO.File.ReadAllText(m_RunPath + "\\tempData\\DownLoadDriverFile.txt");
            }
            else
            {
                //System.IO.File.Create(m_RunPath + "\\tempData\\DownLoadDriverFile.txt");
                //strExistFiles = System.IO.File.ReadAllText(m_RunPath + "\\tempData\\DownLoadDriverFile.txt");
                return;
            }
            //如果txt文本中本来就有，就不再添加
            string strDriverInfo = txtJSYXM.Text.Trim() + txtJSYSFZ.Text.Trim();
            if (strExistFiles.ToString().Trim().Contains(strDriverInfo))
            {
                return;
            }

            if (strExistFiles.Trim().Length > 0)
            {
                //strExistFiles += ",'" + txtJSYXM.Text.Trim() + txtJSYSFZ.Text.Trim() + "'"; //带单引号
                strExistFiles += "," + txtJSYXM.Text.Trim() + "" + txtJSYSFZ.Text.Trim() + "";
            }
            else
            {
                strExistFiles += "" + txtJSYXM.Text.Trim() + "" + txtJSYSFZ.Text.Trim() + "";
            }
            if (strExistFiles.Length > 0)
            {
                System.IO.File.WriteAllText(m_RunPath + "\\tempData\\DownLoadDriverFile.txt", strExistFiles);
            }
        }
        /// <summary>
        /// 查询本地驾驶员信息并绑定
        /// </summary>
        private void QueryDriverData()
        {
            if (System.IO.File.Exists(m_RunPath + "\\tempData\\DownLoadDriverFile.txt"))
            {
                strExistFiles = System.IO.File.ReadAllText(m_RunPath + "\\tempData\\DownLoadDriverFile.txt");
            }

            string[] strDriver = strExistFiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < strDriver.Length; i++)
            {
                int k = 0; //添加驾驶员姓名条件
                int intDriver = 0;//添加驾驶员姓名与身份证条件
                string strDriverNew = strDriver[i].Replace(",", "").Trim();
                //添加驾驶员姓名与身份证,为选择姓名调出身份证用法
                for (int j = 0; j < DriverArray.Count; j++)
                {
                    if (strDriverNew == DriverArray[j].ToString().Trim())
                    {
                        intDriver = 1;
                        break;
                    }
                }
                if (intDriver == 0)
                {
                    DriverArray.Add(strDriverNew);
                }

                //筛选姓名
                int length = strDriverNew.Length;
                if (length > 19)//省份证为18位，因为15位的身份证加名字3位小于19
                {
                    strDriverNew = strDriverNew.Substring(0, length - 18);
                }
                else
                {
                    strDriverNew = strDriverNew.Substring(0, length - 15);
                }
                for (int j = 0; j < strDriverArray.Count; j++)
                {
                    if (strDriverNew == strDriverArray[j].ToString().Trim())
                    {
                        k = 1;
                        break;
                    }
                }
                if (k == 0)
                {
                    strDriverArray.Add(strDriverNew);
                }
            }
            strDriverArray.Remove("");
            strDriverArray.Insert(0, "");
            txtJSYXM.DataSource = strDriverArray;
        }

        /// <summary>
        /// 已计量次数清零
        /// </summary>
        private void UpdateData()
        {
            s_FS_PLANCODE = this.txtYBH.Text.Trim();
            string strTable = "DT_WEIGHTPLAN";
            string strFieldValue = "FN_WEIGHTTIMES = ''";
            string strWhere = "FS_PLANCODE ='" + s_FS_PLANCODE + "'";

            //string sql = "update DT_WEIGHTPLAN set FN_WEIGHTTIMES = '0' where FS_PLANCODE ='" + s_FS_PLANCODE + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "check_UpdateInfo";
            ccp.ServerParams = new object[] { strTable, strFieldValue, strWhere };
            ccp.IfShowErrMsg = false;
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 保存发货地点信息到本地
        /// </summary>
        private void SaveSendPlaceData()
        {
            if (cbFHDWKCD.Text.Length <= 0)
            {
                return;
            }
            //如果txt文本中本来就有，就不再添加
            string strSendPlaceInfo = cbFHDWKCD.Text.Trim();
            if (strExistSendPlaceFiles.ToString().Trim().Contains(strSendPlaceInfo))
            {
                return;
            }

            if (System.IO.File.Exists(m_RunPath + "\\tempData\\DownLoadSendPlaceFile.txt"))
            {
                strExistSendPlaceFiles = System.IO.File.ReadAllText(m_RunPath + "\\tempData\\DownLoadSendPlaceFile.txt");
            }
            else
            {
                return;
            }

            if (strExistSendPlaceFiles.Trim().Length > 0)
            {
                strExistSendPlaceFiles += "," + cbFHDWKCD.Text.Trim() + "";
            }
            else
            {
                strExistSendPlaceFiles += "" + cbFHDWKCD.Text.Trim() + "";
            }

            System.IO.File.WriteAllText(m_RunPath + "\\tempData\\DownLoadSendPlaceFile.txt", strExistSendPlaceFiles);
        }
        /// <summary>
        /// 查询本地发货地点信息并绑定
        /// </summary>
        private void QuerySendPlaceData()
        {
            if (System.IO.File.Exists(m_RunPath + "\\tempData\\DownLoadSendPlaceFile.txt"))
            {
                strExistSendPlaceFiles = System.IO.File.ReadAllText(m_RunPath + "\\tempData\\DownLoadSendPlaceFile.txt");
            }
            System.Collections.ArrayList SendPlace = new System.Collections.ArrayList();
            string[] strSendPlace = strExistSendPlaceFiles.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ////strSendPlace[0].Insert(0,"");
            //cbFHDWKCD.DataSource = strSendPlace;
            for (int i = 0; i < strSendPlace.Length; i++)
            {
                SendPlace.Add(strSendPlace[i]);
            }
            SendPlace.Insert(0, "");
            cbFHDWKCD.DataSource = SendPlace;
        }

        /// <summary>
        /// 保存车号简称 比如:云A
        /// </summary>
        private void AddCarNO()
        {
            string strJLDID = "";
            if (cbBF.Text.Trim() != "")
            {
                strJLDID = cbBF.SelectedValue.ToString().Trim();
            }
            string s_CarNo = "";
            if (cbCH1.Text.Trim() != "")
            {
                s_CarNo = cbCH1.Text.Trim();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveCHData";
            ccp.ServerParams = new object[] { strJLDID, s_CarNo };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 选择驾驶员姓名后自动绑定身份证号码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            System.Collections.ArrayList DriverSFZ = new System.Collections.ArrayList();//驾驶员身份证
            for (int i = 0; i < DriverArray.Count; i++)
            {
                string strDriverSFZ = "";
                string strDriverName = "";
                int length = DriverArray[i].ToString().Trim().Length;
                if (length > 19)
                {
                    strDriverName = DriverArray[i].ToString().Substring(0, length - 18);
                }
                else
                {
                    strDriverName = DriverArray[i].ToString().Substring(0, length - 15);
                }
                if (DriverArray[i].ToString().Trim().Contains(txtJSYXM.Text.Trim()) && strDriverName == txtJSYXM.Text.Trim())
                {
                    //strDriverArray[i].ToString().Trim();
                    strDriverSFZ = DriverArray[i].ToString().Substring(txtJSYXM.Text.Trim().Length);
                    DriverSFZ.Add(strDriverSFZ);
                }
            }
            txtJSYSFZ.DataSource = DriverSFZ;
        }

        /// <summary>
        /// 将全角转换为半角
        /// </summary>
        /// <param name="newPressChar"></param>
        /// <returns></returns>
        private string ChangeHalfCode(Char newPressChar)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(newPressChar.ToString());
            int aaa = Convert.ToInt32(bytes[0]);
            int bbb = Convert.ToInt32(bytes[1]);

            int cccc = bbb * 256 + aaa;
            if (cccc >= 65281 && cccc <= 65374)
            {
                bytes[1] = 0;
                bytes[0] = Convert.ToByte(cccc - 65248);

                string newChar = System.Text.Encoding.Unicode.GetString(bytes);
                return newChar;
            }
            else if (cccc == 12290)
            {
                return ".";
            }
            else
            {
                return newPressChar.ToString();
            }
        }

        private void cbCH2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < (char)127)
            {
                e.Handled = false;
                return;
            }

            string newChar = ChangeHalfCode(e.KeyChar);
            e.KeyChar = newChar[0];

            e.Handled = false;
        }

        private void cbCH1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < (char)127)
            {
                e.Handled = false;
                return;
            }

            string newChar = ChangeHalfCode(e.KeyChar);
            e.KeyChar = newChar[0];

            e.Handled = false;
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "查询":
                    this.QueryYBData();
                    //this.ClearControl();
                    break;
                case "增加":
                    if (!ValidateCarCardData() && txtCZH.Text.Trim().Length > 0)
                    {
                        MessageBox.Show("卡号" + "'" + txtCZH.Text.Trim() + "'" + "还未分配，请联系管理员或相关单位查明原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                    if (check() == false)
                    {
                        break;
                    }
                    if (this.txtCZH.Text.Trim() != "")
                    {
                        if (MoreYBDelete() == false)
                            break;
                    }
                    if (this.cbCH1.Text.Trim() != "" && this.cbCH2.Text.Trim() != "")
                    {
                        if (MoreNonCardYBDelete() == false)
                            break;
                    }
                    this.SavePredictInfo();
                    this.QueryYBData();
                    //this.ClearControl();
                    break;

                case "修改":
                    if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null || ultraGrid1.ActiveRow.Selected == false)
                    {
                        MessageBox.Show("请选择一条预报信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (ControlProve() == false)
                    {
                        break;
                    }
                    if (DialogResult.Yes == MessageBox.Show("您确认要修改该条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        UpdateOldData();
                        UpdateData();
                        this.QueryYBData();
                        //this.ClearControl();
                    }
                    break;
                case "删除":
                    if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null || ultraGrid1.ActiveRow.Selected == false)
                    {
                        MessageBox.Show("请选择一条预报信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (DialogResult.Yes == MessageBox.Show("您确认要删除该条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        this.DeleteOldData();
                        this.ClearControl();
                    }
                    break;
                case "导入":
                    this.ClearControl();
                    if (this.txtYBWJ.Text == "")
                    {
                        MessageBox.Show("请选择要导入的预报信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    this.ReadAndSaveExcelInfo();
                    this.QueryYBData();
                    this.txtYBWJ.Text = "";
                    break;

            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down && m_List.Visible == true && m_List.Items.Count != 0 &&
                (cbWLMC.Focused == true || cbSHDW.Focused == true || cbFHDW.Focused == true || cbCYDW.Focused == true || cbProvider.Focused == true))
            {
                m_List.SetSelected(0, true);
                m_List.Focus();
                return true;
            }
            if (keyData == Keys.Up &&
                (cbWLMC.Focused == true || cbSHDW.Focused == true || cbFHDW.Focused == true || cbCYDW.Focused == true || cbProvider.Focused == true))
            {
                Control c = GetNextControl(this.ActiveControl, false);
                bool ok = SelectNextControl(this.ActiveControl, false, true, true, true);
                if (ok && c != null)
                {
                    if (c is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)c).SelectAll();
                    }
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void m_List_Click(object sender, EventArgs e)   //双击选中智能输入列表中的内容
        {
            if (m_List.Text.Trim().Length == 0)
            {
                return;
            }
            switch (m_ListType)
            {
                case "Material":
                    this.cbWLMC.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbWLMC.Focus();
                    m_List.Visible = false;
                    break;
                case "Reveiver":
                    this.cbSHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbSHDW.Focus();
                    m_List.Visible = false;
                    break;
                case "Sender":
                    this.cbFHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbFHDW.Focus();
                    m_List.Visible = false;
                    break;
                case "Provider":
                    this.cbProvider.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbProvider.Focus();
                    m_List.Visible = false;
                    break;
                case "Trans":
                    this.cbCYDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbCYDW.Focus();
                    m_List.Visible = false;
                    break;
                default:
                    m_List.Visible = false;
                    break;
            }
        }

        void m_List_Leave(object sender, EventArgs e)
        {

        }


        private void m_List_KeyPress(object sender, KeyPressEventArgs e)
        {
            string text = "";
            switch (m_ListType)
            {
                case "Material":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbWLMC.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbWLMC.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbWLMC.Focus();
                        text = cbWLMC.Text + e.KeyChar;
                        cbWLMC.Text = text;
                        cbWLMC.SelectionStart = cbWLMC.Text.Length;
                    }
                    break;
                case "Reveiver":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbSHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbSHDW.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbSHDW.Focus();
                        text = cbSHDW.Text + e.KeyChar;
                        cbSHDW.Text = text;
                        cbSHDW.SelectionStart = cbSHDW.Text.Length;
                    }
                    break;
                case "Sender":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbFHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbFHDW.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbFHDW.Focus();
                        text = cbFHDW.Text + e.KeyChar;
                        cbFHDW.Text = text;
                        cbFHDW.SelectionStart = cbFHDW.Text.Length;
                    }
                    break;
                case "Provider":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbProvider.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbProvider.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbProvider.Focus();
                        text = cbProvider.Text + e.KeyChar;
                        cbProvider.Text = text;
                        cbProvider.SelectionStart = cbProvider.Text.Length;
                    }
                    break;
                case "Trans":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbCYDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbCYDW.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbCYDW.Focus();
                        text = cbCYDW.Text + e.KeyChar;
                        cbCYDW.Text = text;
                        cbCYDW.SelectionStart = cbCYDW.Text.Length;
                    }
                    break;
                default:
                    break;
            }
        }


        private void ChangeString(object sender)
        {
            if (sender is System.Windows.Forms.TextBox)
            {
                System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
                for (int i = 0; i < tb.Text.Length; i++)
                {
                    int isChange = 0;
                    char newChar = FullCodeToHalfCode(tb.Text[i], ref isChange);
                    if (isChange == 1)
                    {
                        tb.Text = tb.Text.Replace(tb.Text[i], newChar);
                        tb.SelectionStart = i + 1;
                    }
                }

            }
            else if (sender is ComboBox)
            {
                ComboBox cb = (ComboBox)sender;

                for (int i = 0; i < cb.Text.Length; i++)
                {
                    int isChange = 0;
                    char newChar = FullCodeToHalfCode(cb.Text[i], ref isChange);
                    if (isChange == 1)
                    {
                        cb.Text = cb.Text.Replace(cb.Text[i], newChar);
                        cb.SelectionStart = i + 1;
                    }
                }
            }
        }

        /*全角字符从的unicode编码从65281~65374   
         半角字符从的unicode编码从33~126   
         差值65248
         空格比较特殊,全角为       12288,半角为       32 
       */
        private char FullCodeToHalfCode(char c, ref int isChange)
        {
            //得到c的编码
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(c.ToString());

            int H = Convert.ToInt32(bytes[1]);
            int L = Convert.ToInt32(bytes[0]);

            //得到unicode编码
            int value = H * 256 + L;

            //是全角
            if (value >= 65281 && value <= 65374)
            {
                int halfvalue = value - 65248;//65248是全半角间的差值。
                byte halfL = Convert.ToByte(halfvalue);

                bytes[0] = halfL;
                bytes[1] = 0;
                isChange = 1;
            }
            else if (value == 12288)
            {
                int halfvalue = 32;
                byte halfL = Convert.ToByte(halfvalue);

                bytes[0] = halfL;
                bytes[1] = 0;
                isChange = 1;
            }
            else
            {
                isChange = 0;
                return c;
            }

            //将bytes转换成字符
            string ret = System.Text.Encoding.Unicode.GetString(bytes);

            return Convert.ToChar(ret);
        }

        private void cbWLMC_TextChanged(object sender, EventArgs e)
        {
            if (this.cbWLMC.Text.Trim().Length == 0 || this.cbWLMC.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (cbBF.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbWLMC.Text = "";
                return;
            }

            if (m_List == null || cbWLMC.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbWLMC.Text.Length; i++)
            {
                if (Char.IsLower(cbWLMC.Text[i]) == false && Char.IsUpper(cbWLMC.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Material";
            m_List.Location = new System.Drawing.Point(344, 68);

            string text = this.cbWLMC.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempMaterial.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_MaterialName"].ToString());
            }
            m_List.Visible = true;
        }

        private void cbWLMC_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbWLMC.Text = "";
                    }
                }

            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void cbFHDW_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbFHDW.SelectedValue == null || this.cbFHDW.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbFHDW.Text = "";
                    }
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void cbSHDW_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbSHDW.SelectedValue == null || this.cbSHDW.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbSHDW.Text = "";
                    }
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void cbCYDW_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbCYDW.SelectedValue == null || this.cbCYDW.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbCYDW.Text = "";
                    }
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void moreBtn_Click(object sender, EventArgs e)
        {
            if (cbBF.Text.Trim() == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);

            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(), this.ob);
            //frm.ob = this.ob;
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this.cbWLMC.SelectedValue.ToString());
        }

        //发货单位下拉框拼音助记码选择
        private void cbFHDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbFHDW.Text.Trim().Length == 0 || this.cbFHDW.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (cbBF.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFHDW.Text = "";
                return;
            }

            if (m_List == null || cbFHDW.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbFHDW.Text.Length; i++)
            {
                if (Char.IsLower(cbFHDW.Text[i]) == false && Char.IsUpper(cbFHDW.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Sender";
            m_List.Location = new System.Drawing.Point(631, 56);

            string text = this.cbFHDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempSender.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_SUPPLIERName"].ToString());
            }
            m_List.Visible = true;
        }

        private void cbSHDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbSHDW.Text.Trim().Length == 0 || this.cbSHDW.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (cbBF.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSHDW.Text = "";
                return;
            }

            if (m_List == null || cbSHDW.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbSHDW.Text.Length; i++)
            {
                if (Char.IsLower(cbSHDW.Text[i]) == false && Char.IsUpper(cbSHDW.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Reveiver";
            m_List.Location = new System.Drawing.Point(344, 97);

            string text = this.cbSHDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempReveiver.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_Memo"].ToString());
            }
            m_List.Visible = true;
        }

        private void cbCYDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbCYDW.Text.Trim().Length == 0 || this.cbCYDW.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (cbBF.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCYDW.Text = "";
                return;
            }

            if (m_List == null || cbCYDW.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbCYDW.Text.Length; i++)
            {
                if (Char.IsLower(cbCYDW.Text[i]) == false && Char.IsUpper(cbCYDW.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Trans";
            m_List.Location = new System.Drawing.Point(618, 97);

            string text = this.cbCYDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempTrans.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_TRANSNAME"].ToString());
            }
            m_List.Visible = true;
        }

        //全角转化成半角
        private void txtCZH_Leave(object sender, EventArgs e)
        {
            string strCardNo = this.txtCZH.Text;
            string strSQL = "select fs_bindcarno from bt_cardmanage where fs_sequenceno ='" + strCardNo + "'";
            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.car.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSQL };
            System.Data.DataTable dt = new System.Data.DataTable();
            ccp.SourceDataTable = dt;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                string cc = dt.Rows[0]["fs_bindcarno"].ToString();
                if (cc != "")
                {
                    string ch1 = cc.Substring(0, 2);
                    this.cbCH1.Text = ch1;
                    string ch2 = cc.Substring(2);
                    this.cbCH2.Text = ch2;
                }
            }
            ChangeString(sender);
        }

        private void cbCH1_Leave(object sender, EventArgs e)
        {
            ChangeString(sender);
        }

        private void cbCH2_Leave(object sender, EventArgs e)
        {
            ChangeString(sender);
        }

        private void cbProvider_TextChanged(object sender, EventArgs e)
        {
            if (this.cbProvider.Text.Trim().Length == 0 || this.cbProvider.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (cbBF.Text.Trim().Length == 0)
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbProvider.Text = "";
                return;
            }

            if (m_List == null || cbProvider.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < this.cbProvider.Text.Length; i++)
            {
                if (Char.IsLower(this.cbProvider.Text[i]) == false && Char.IsUpper(this.cbProvider.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Provider";
            m_List.Location = new System.Drawing.Point(this.cbProvider.Location.X, this.cbProvider.Location.Y + 20);
            m_List.Width = cbProvider.Width;

            string text = this.cbProvider.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempProvider.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_PROVIDERNAME"].ToString());
            }
            m_List.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cbBF.Text.Trim() == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(), this.ob);
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void cbProvider_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbProvider.SelectedValue == null || this.cbProvider.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbProvider.Text = "";
                    }
                }

            }
            catch (System.Exception exp)
            {

            }
        }

        /// <summary>
        /// 新增预报信息
        /// </summary>

        public bool SFZNumber(string str_number)
        {
            // isIDCard1=/^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$/;
            //isIDCard2=/^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{4}$/;
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"(^([1-9]\d{14}|[1-9]\d{16}[\dx|X])$)");

        }

        public bool zlNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_number, @"(^(\d*.\d{2})$)");
        }

        private void ultraGrid1_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null)
            {
                return;
            }
            ultraGrid1.ActiveRow.Selected = true;

            this.txtYBH.Text = ultraGrid1.ActiveRow.Cells["FS_PLANCODE"].Text.Trim();
            this.txtCZH.Text = ultraGrid1.ActiveRow.Cells["FS_CARDNUMBER"].Text.Trim();
            string CH = ultraGrid1.ActiveRow.Cells["FS_CARNO"].Text.Trim();
            if (CH != "")
            {
                string status = ultraGrid1.ActiveRow.Cells["FS_STATUS"].Text.Trim();
                if (CH.Length < 7)
                {
                    string CH1 = CH.Substring(0, 1);
                    this.cbCH1.Text = CH1;


                    string CH2 = CH.Substring(1);
                    this.cbCH2.Text = CH2;
                }
                else
                {
                    string CH1 = CH.Substring(0, 2);
                    this.cbCH1.Text = CH1;


                    string CH2 = CH.Substring(2);
                    this.cbCH2.Text = CH2;
                }
            }
            this.cbBF.SelectedValue = ultraGrid1.ActiveRow.Cells["FS_POUNDTYPE"].Text.Trim();
            //this.cbBF.Text = ultraGrid1.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            this.txtHTH.Text = ultraGrid1.ActiveRow.Cells["FS_CONTRACTNO"].Text.Trim();
            dtHTH.Clear();
            if (dtHTH.Columns.Count > 0)
            {
                DataRow newDr = dtHTH.NewRow();
                newDr[0] = ultraGrid1.ActiveRow.Cells["FS_CONTRACTITEM"].Text.Trim().ToString();
                dtHTH.Rows.Add(newDr);
            }

            this.cbWLMC.SelectedValue = ultraGrid1.ActiveRow.Cells["FS_MATERIAL"].Text.Trim();

            this.txtHTXMH.Text = ultraGrid1.ActiveRow.Cells["FS_CONTRACTITEM"].Text.Trim();
            this.cbWLMC.Text = ultraGrid1.ActiveRow.Cells["FS_MATERIALNAME"].Text.Trim();
            this.txtZS.Text = ultraGrid1.ActiveRow.Cells["FN_BILLETCOUNT"].Text.Trim();
            this.cbFHDW.Text = ultraGrid1.ActiveRow.Cells["FS_SENDER"].Text.Trim();
            this.cbSHDW.Text = ultraGrid1.ActiveRow.Cells["FS_RECEIVERFACTORY"].Text.Trim();
            this.cbCYDW.Text = ultraGrid1.ActiveRow.Cells["FS_TRANSNO"].Text.Trim();
            this.cbFHDWKCD.Text = ultraGrid1.ActiveRow.Cells["FS_SENDERSTORE"].Text.Trim();
            this.cbReceiverStore.Text = ultraGrid1.ActiveRow.Cells["FS_RECEIVERSTORE"].Text.Trim();
            this.cbLX.Text = ultraGrid1.ActiveRow.Cells["FS_WEIGHTTYPE"].Text.Trim();
            this.txtLH.Text = ultraGrid1.ActiveRow.Cells["FS_STOVENO"].Text.Trim();
            this.dateDQSJ.Text = ultraGrid1.ActiveRow.Cells["FS_OVERTIME"].Text.Trim();
            this.txtYBZZ.Text = ultraGrid1.ActiveRow.Cells["FN_SENDGROSSWEIGHT"].Text.Trim();
            this.txtYBPZ.Text = ultraGrid1.ActiveRow.Cells["FN_SENDTAREWEIGHT"].Text.Trim();
            this.txtYBJZ.Text = ultraGrid1.ActiveRow.Cells["FN_SENDNETWEIGHT"].Text.Trim();
            this.txt_jhcs.Text = ultraGrid1.ActiveRow.Cells["FN_TIMES"].Text.Trim();
            this.txtYJLCS.Text = ultraGrid1.ActiveRow.Cells["FN_WEIGHTTIMES"].Text.Trim();
            this.cbBC.Text = ultraGrid1.ActiveRow.Cells["FS_SHIFT"].Text.Trim();
            this.cbYBLXR.Text = ultraGrid1.ActiveRow.Cells["FS_PLANUSER"].Text.Trim();
            this.cbYBLXDH.Text = ultraGrid1.ActiveRow.Cells["FS_PLANTEL"].Text.Trim();
            this.cbYXJ.Text = ultraGrid1.ActiveRow.Cells["FS_LEVEL"].Text.Trim();

            this.txtJSYXM.Text = ultraGrid1.ActiveRow.Cells["FS_DRIVERNAME"].Text.Trim();
            this.txtJSYSFZ.Text = ultraGrid1.ActiveRow.Cells["FS_DRIVERIDCARD"].Text.Trim();
            this.txtBZ.Text = ultraGrid1.ActiveRow.Cells["FS_DRIVERREMARK"].Text.Trim();

            this.cbProvider.Text = ultraGrid1.ActiveRow.Cells["FS_PROVIDER"].Text.Trim();


            if (cbYXJ.Text == "是")
            {
                this.checkBox3.Checked = true;
            }
            else
            {
                checkBox3.Checked = false;
            }

            this.cbYXJ.Text = ultraGrid1.ActiveRow.Cells["FS_IFSAMPLING"].Text.Trim();
            if (cbYXJ.Text == "是")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            //this.cbYXJ.Text = ultraGrid1.ActiveRow.Cells["FS_IFACCEPT"].Text.Trim();
            //if (cbYXJ.Text == "1")
            //{
            //    checkBox2.Checked = true;
            //}
            //else
            //{
            //    checkBox2.Checked = false;
            //}

            index = ultraGrid1.ActiveRow.Index; //删除索引

            //cbCH1.Text = "";

        }

        private void cb_JQPLAN_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_JQPLAN.Checked == true)
            {
                this.comboBox1.Enabled = true;
                //this.txt_jhcs.ReadOnly = false;
            }
            else
            {
                this.comboBox1.Enabled = false;
                //this.txt_jhcs.ReadOnly = true;
            }
        }

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

        public string[] SaveWeightData(Hashtable param)
        {
            CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_PredictInfo.SavePredictInfo(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}", param);

            string result = "";
            string message = "";
            string weightNo = "";
            if (ccp.ReturnObject != null && !string.IsNullOrEmpty(ccp.ReturnObject.ToString()))
            {
                object[] results = (object[])ccp.ReturnObject;
                for (int i = 0; i < results.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            result = results[i] != null ? results[i].ToString() : "";
                            break;
                        case 1:
                            weightNo = results[i] != null ? results[i].ToString() : "";
                            break;
                        case 2:
                            message = results[i] != null ? results[i].ToString() : "";
                            break;
                    }
                }
            }
            return new string[] { result, weightNo, message };
        }

        /// <summary>
        /// 获取部门ID
        /// </summary>
        /// <returns></returns>
        private string getDepartID()
        {
            string str_Depart = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
            string sql = "select DEPARTID from CORE_APP_DEPARTMENT where DEPARTNAME = '" + str_Depart + "' ";
            System.Data.DataTable dt_DepartID = new System.Data.DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.CarCard";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { sql };
            //DataTable dt = new DataTable();
            //dt = dataTable1.Clone();
            ccp.SourceDataTable = dt_DepartID;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt_DepartID.Rows.Count != 0)
            { str_DepartID = dt_DepartID.Rows[0][0].ToString(); }

            return str_DepartID;
        }
    }
}
