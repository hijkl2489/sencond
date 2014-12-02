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
using System.Diagnostics;
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.Car
{
    public partial class HandRecordFirstWeight : FrmBase
    {
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        string m_SelectPointID = "";
        string m_WeightNo = "";
        BaseInfo objBi = null;
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
        string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        string strDepartMent = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();

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

       

        private void InitConfig()
        {
            //物料下拉框
            this.ultraGroupBox1.Controls.Add(m_List);
            m_List.Size = new Size(157, 80);
            m_List.Visible = false;
            m_List.ScrollAlwaysVisible = true;
            m_List.BringToFront();
            m_List.Click += new EventHandler(m_List_Click);
            m_List.KeyPress += new KeyPressEventHandler(m_List_KeyPress);
            m_List.Leave += new EventHandler(m_List_Leave);


        }

        public HandRecordFirstWeight()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        Query();                        
                        break;
                    }
                case "Update":
                    {
                        Update();
                        Query();
                        break;
                    }
                case "Add":
                        {
                        Add();
                            Query();
                            break;
                        }

                case "Delete":
                        {
                            if (DialogResult.Yes == MessageBox.Show("是否确认要删除当前数据?", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                Delete();
                                Query();
                            }
                            break;
                        }
                default :
                    break;
            }
        }

        //下载流向信息  ,add by luobin 
        private void DownLoadFlow()
        {
             DataTable m_FlowTable = new DataTable();//流向数据表
             DataColumn dc;
             dc = new DataColumn("FS_TYPECODE".ToUpper()); m_FlowTable.Columns.Add(dc);
             dc = new DataColumn("FS_TYPENAME".ToUpper()); m_FlowTable.Columns.Add(dc);

            string sql = "select FS_TYPECODE, FS_TYPENAME From BT_WEIGHTTYPE order by FS_TYPECODE ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = m_FlowTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            DataRow dr = m_FlowTable.NewRow();
            dr["FS_TYPECODE"] = "";
            dr["FS_TYPENAME"] = "";
            m_FlowTable.Rows.InsertAt(dr, 0);

            cbFlow.DataSource = m_FlowTable;
            cbFlow.DisplayMember = "FS_TYPENAME";
            cbFlow.ValueMember = "FS_TYPECODE";

        }

        private void Query()
        {
            string strBeginTime =  BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss')";
            string strEndTIme = EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            string sql = "select A.FS_WEIGHTNO,A.FS_CARDNUMBER,A.FS_CARNO,A.FS_STOVENO,A.FN_COUNT,B.FS_MATERIALNAME,C.FS_SUPPLIERNAME,d.fs_memo as FS_Receiver,";
            sql += "A.Fs_Senderstore,A.FS_RECEIVERSTORE,E.FS_TRANSNAME,F.FS_TYPENAME,G.FS_POINTNAME,A.FN_WEIGHT,";
                   sql += " A.Fs_Weighter,to_char(A.Fd_Weighttime, 'yyyy-MM-dd HH24:mi:ss') as FD_Weighttime,A.Fs_Weighter,";
                   sql += "to_char(A.Fd_Weighttime, 'yyyy-MM-dd HH24:mi:ss') as FD_Weighttime,A.fs_Memo,A.Fs_Poundtype,A.FS_BZ ,";
                   sql += " A.FS_REWEIGHTFLAG,to_char(A.FD_REWEIGHTTIME, 'yyyy-MM-dd HH24:mi:ss') as FD_REWEIGHTTIME,A.FS_REWEIGHTPLACE,A.FS_REWEIGHTPERSON,";
                   sql += "  A.FS_UNLOADPERSON ,to_char(A.FD_UNLOADTIME, 'yyyy-MM-dd HH24:mi:ss') as FD_UNLOADTIME , A.FS_UNLOADPLACE ,A.FS_UNLOADFLAG ";
                   sql += " from dt_firstcarweight A,It_Material B,It_Supplier C,It_Store D,BT_TRANS E,Bt_Weighttype F,Bt_Point G,It_Provider H";
                   sql += " where A.Fs_Material = b.fs_wl(+) and A.FS_SENDER = C.FS_GY(+) and A.Fs_Receiver = d.fs_sh(+)";
                   sql += " and A.Fs_Transno = E.FS_CY(+) and A.Fs_Weighttype = F.FS_TYPECODE(+) and A.Fs_Poundtype = G.FS_POINTCODE(+) and A.FS_Provider = H.FS_SP(+)";
                   sql += " and A.FD_Weighttime >= to_date('" + BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
                   sql += " and FD_WEIGHTTIME <= to_date('" + EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
                   sql += " and A.FS_FALG <>1 ";
                   dataTable1.Rows.Clear();
                   CoreClientParam ccp = new CoreClientParam();
                   ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                   ccp.MethodName = "ExcuteQuery";
                   ccp.ServerParams = new object[] { sql };
                   ccp.SourceDataTable = dataSet1.Tables[0];

                   this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                   Constant.RefreshAndAutoSize(ultraGrid3);

                   foreach (UltraGridRow ugr in ultraGrid3.Rows)
                   {
                       if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                       {
                           ugr.Appearance.ForeColor = Color.Red;
                       }
                   }
        }

        private void Update()
        {
            if (IsValid() == false)
                return;

            if (m_WeightNo == "")
            {
                MessageBox.Show("请选择需要修改的数据");
                return;
            }

            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strSenderPlace = this.tbSenderPlace.Text.Trim();
            string strReceiverPlace = this.tbReceiverPlace.Text.Trim();
            double inWeight = Math.Round(Convert.ToDouble(this.tbWeight.Text.Trim()),3);
            
            string strMaterial = "";
            if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                strMaterial = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbWLMC.Text.Trim(), "SaveWLData");
            else
                strMaterial = this.cbWLMC.SelectedValue.ToString().Trim();

            string strSender = "";
            if (this.cbFHDW.SelectedValue == null || this.cbFHDW.SelectedValue.ToString().Trim() == "")
                strSender = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbFHDW.Text.Trim(), "SaveGYDWData");
            else
                strSender = this.cbFHDW.SelectedValue.ToString().Trim();

            string strReceiver = "";
            if (this.cbSHDW.SelectedValue == null || this.cbSHDW.SelectedValue.ToString().Trim() == "")
                strReceiver = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbSHDW.Text.Trim(), "SaveSHDWData");
            else
                strReceiver = this.cbSHDW.SelectedValue.ToString().Trim();

            string strTrans = "";
            if (this.cbCYDW.SelectedValue == null || this.cbCYDW.SelectedValue.ToString().Trim() == "")
                strTrans = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbCYDW.Text.Trim(), "SaveCYDWData");
            else
                strTrans = this.cbCYDW.SelectedValue.ToString().Trim();

            string strProvider = "";
            if (this.cbProvider.SelectedValue != null)
                strProvider = this.cbProvider.SelectedValue.ToString();
            else
            {
                if (this.cbProvider.Text.Trim() != "")
                    strProvider = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbProvider.Text.Trim(), "SaveSPDWData");
            }

            string strBZ = this.tbBZ.Text.Trim();

            string strFlow = this.cbFlow.SelectedValue.ToString().Trim();
            string strMaterialName = this.cbWLMC.Text.Trim();
            string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " 手工补录";


            string sql = "update dt_firstcarweight SET FS_CARDNUMBER = '" + strCardNo + "',";
                   sql += " FS_CARNO = '" + strCarNo + "',";
                   sql += " Fs_Material = '" + strMaterial + "',";
                   sql += " Fs_MaterialName = '" + strMaterialName + "',";
                   sql += " FS_SENDER  = '" + strSender + "',";
                   sql += " Fs_Receiver  = '" + strReceiver + "',";
                   sql += " Fs_Senderstore  = '" + strSenderPlace + "',";
                   sql += " FS_RECEIVERSTORE  = '" + strReceiverPlace + "',";
                   sql += " Fs_Transno  = '" + strTrans + "',";
                   sql += " Fs_Weighttype  = '" + strFlow + "',";
                   sql += " Fs_Poundtype  = '" + m_SelectPointID + "',";
                   sql += " FN_WEIGHT  = " + inWeight + ",";
                   sql += " FS_Provider  = '" + strProvider + "',";
                   sql += " FS_BZ  = '" + strBZ + "',";
                   sql += " fs_Memo  = '" + strMemo + "'";
                   sql += " where FS_WEIGHTNO = '" + m_WeightNo + "'";
                   
                   
                    
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            string strCardNOLog = this.ultraGrid3.ActiveRow.Cells["FS_CARDNUMBER"].Text;
            string strCarNoLog = this.ultraGrid3.ActiveRow.Cells["FS_CARNO"].Text;
            string strMaterialLog = this.ultraGrid3.ActiveRow.Cells["FS_MATERIALNAME"].Text;
            string strSENDERLog = this.ultraGrid3.ActiveRow.Cells["FS_SUPPLIERNAME"].Text;
            string strReceiverLog = this.ultraGrid3.ActiveRow.Cells["FS_Receiver"].Text;
            string strSenderstoreLog = this.ultraGrid3.ActiveRow.Cells["FS_SENDERSTORE"].Text;
            string strRECEIVERSTORELog = this.ultraGrid3.ActiveRow.Cells["FS_RECEIVERSTORE"].Text;
            string strTransnoLog = this.ultraGrid3.ActiveRow.Cells["FS_TRANSNAME"].Text;
            string strWeighttypeLog = this.ultraGrid3.ActiveRow.Cells["FS_TYPENAME"].Text;
            string strWEIGHTLog = this.ultraGrid3.ActiveRow.Cells["FN_WEIGHT"].Text;
            string strMemoLog = this.ultraGrid3.ActiveRow.Cells["fs_Memo"].Text;


            string strLog = "卡号：" + strCardNOLog + ">" + strCardNo + ",车号：" + strCarNoLog + ">" + strCarNo + ",物料："//卡号、车号
                + strMaterialLog + ">" +this.cbWLMC.Text + ",发货单位：" + strSENDERLog + ">" + this.cbFHDW.Text + ",收货单位："//物料、发货单位
                + strReceiverLog + ">" + this.cbSHDW.Text + ",装货点：" + strSenderstoreLog + ">" +this.tbSenderPlace.Text + ",卸货点："//收货单位、装货地点
                + strRECEIVERSTORELog + ">" + tbReceiverPlace.Text + ",承运单位：" + strTransnoLog + ">" + this.cbCYDW.Text + ",流向："//卸货地点、承运单位
                + strWeighttypeLog + ">" + this.cbFlow.Text + ",重量：" + strWEIGHTLog + ">" + inWeight + ",备注：" + strMemoLog + ">" + strMemo;//流向、重量


            this.objBi.WriteOperationLog("DT_FIRSTCARWEIGHT", strDepartMent, strUserName, "修改", strLog, "一次数据补录", "汽车衡一次数据表","汽车衡");//调用写操作日志方法

        }

        private void Add()
        {
            if (IsValid() == false)
                return;
            


            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strSenderPlace = this.tbSenderPlace.Text.Trim();
            string strReceiverPlace = this.tbReceiverPlace.Text.Trim();
            double inWeight = 0;
            try
            {
                 inWeight = Math.Round(Convert.ToDouble(this.tbWeight.Text.Trim()), 3);

            }
            catch(Exception e)
            {
                MessageBox.Show("请输入数字格式的重量!");
            }

            string strMaterialName = this.cbWLMC.Text.Trim();
            string strPound = this.cbBF.Text.Trim();
            string strShift = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder();

            string strMaterial = "";
            if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                strMaterial = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbWLMC.Text.Trim(), "SaveWLData");
            else
                strMaterial = this.cbWLMC.SelectedValue.ToString().Trim();


            string strSender = "";
            if (this.cbFHDW.SelectedValue == null || this.cbFHDW.SelectedValue.ToString().Trim() == "")
                strSender = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbFHDW.Text.Trim(), "SaveGYDWData");
            else
                strSender = this.cbFHDW.SelectedValue.ToString().Trim();

            string strReceiver = "";
            if (this.cbSHDW.SelectedValue == null || this.cbSHDW.SelectedValue.ToString().Trim() == "")
                strReceiver = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbSHDW.Text.Trim(), "SaveSHDWData");
            else
                strReceiver = this.cbSHDW.SelectedValue.ToString().Trim();

            string strTrans = "";
            if (this.cbCYDW.SelectedValue == null || this.cbCYDW.SelectedValue.ToString().Trim() == "")
                strTrans = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbCYDW.Text.Trim(), "SaveCYDWData");
            else
                strTrans = this.cbCYDW.SelectedValue.ToString().Trim();

            string strProvider = "";
            if (this.cbProvider.SelectedValue != null)
                strProvider = this.cbProvider.SelectedValue.ToString();
            else
            { 
               if(this.cbProvider.Text.Trim() != "")
                   strProvider = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbProvider.Text.Trim(), "SaveSPDWData");
            }   

            string strBZ = this.tbBZ.Text.Trim();
           
            //string strFlow = "";
            //if (this.cbFlow.SelectedValue == null || this.cbFlow.SelectedValue.ToString().Trim() == "")
            //    strFlow = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbFlow.Text.Trim(), "SaveLXData");
            //else
            string strFlow = this.cbFlow.SelectedValue.ToString().Trim();

            string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " 手工补录";
            CoreClientParam ccp = new CoreClientParam();
            string sql = "select FS_POINTSTATE from bt_point where FS_POINTCODE = '" + m_SelectPointID + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtState = new System.Data.DataTable();
            ccp.SourceDataTable = dtState;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            string strState = dtState.Rows[0][0].ToString().Trim();


            sql = "select count(1) from dt_firstcarweight where FS_CARNO = '" + strCarNo + "' and FS_DATASTATE = '"+strState+"'";          
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dt = new System.Data.DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (Convert.ToInt32(dt.Rows[0][0].ToString().Trim()) > 0)
            {
                MessageBox.Show("该车号在一次计量表中已经存在记录，不允许添加!");
                return;
            }

            m_WeightNo = Guid.NewGuid().ToString();
            string strWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            string strTime = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");


            sql = "insert into dt_firstcarweight (FS_WEIGHTNO,FS_CARDNUMBER,FS_CARNO,Fs_Material,FS_MaterialName,";
            sql += "FS_SENDER,Fs_Receiver,Fs_Transno,Fs_Weighttype,Fs_Poundtype,FS_Pound,Fs_Senderstore,";
            sql += "FS_RECEIVERSTORE,FN_WEIGHT,Fs_Weighter,Fd_Weighttime,fs_Memo,FS_Shift,FS_DATASTATE,FS_Provider,FS_BZ)";
            sql += " values ('" + m_WeightNo + "','" + strCardNo + "','" + strCarNo + "','" + strMaterial + "','" + strMaterialName + "',";
            sql += " '" + strSender + "','" + strReceiver + "','" + strTrans + "','" + strFlow + "','" + m_SelectPointID + "','" + strPound + "','" + strSenderPlace + "',";
            sql += " '" + strReceiverPlace + "'," + inWeight + ",'" + strWeighter + "',TO_DATE('" + strTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strMemo + "','" + strShift + "','"+strState+"','"+strProvider+"','"+strBZ+"')";

             //CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);


            string strLog = "卡号=" + strCardNo + ",车号=" + strCarNo + ",物料="//卡号、车号
                + cbWLMC.Text + ",发货单位=" + cbFHDW.Text + ",收货单位="//物料、发货单位
                + cbSHDW.Text + ",装货点=" + tbSenderPlace.Text + ",卸货点="//收货单位、装货地点
                + tbReceiverPlace.Text + ",承运单位=" + cbCYDW.Text + ",流向="//卸货地点、承运单位
                + cbFlow.Text + ",重量=" + tbWeight.Text + ",备注=" + tbBZ.Text;//流向、重量


            this.objBi.WriteOperationLog("DT_FIRSTCARWEIGHT", strDepartMent, strUserName, "增加", strLog, "一次数据补录", "汽车衡一次数据表", "汽车衡");//调用写操作日志方法


        }

        private void Delete()
        {
            if (m_WeightNo == "")
            {
                MessageBox.Show("请选择需要删除的数据");
                return;
            }

            string sql = "delete dt_firstcarweight where FS_WEIGHTNO = '" + m_WeightNo + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            sql = "delete dt_images where FS_WEIGHTNO = '" + m_WeightNo + "'";
            //CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            string strCardNOLog = this.ultraGrid3.ActiveRow.Cells["FS_CARDNUMBER"].Text;
            string strCarNoLog = this.ultraGrid3.ActiveRow.Cells["FS_CARNO"].Text;
            string strMaterialLog = this.ultraGrid3.ActiveRow.Cells["FS_MATERIALNAME"].Text;
            string strSENDERLog = this.ultraGrid3.ActiveRow.Cells["FS_SUPPLIERNAME"].Text;
            string strReceiverLog = this.ultraGrid3.ActiveRow.Cells["FS_Receiver"].Text;
            string strSenderstoreLog = this.ultraGrid3.ActiveRow.Cells["FS_SENDERSTORE"].Text;
            string strRECEIVERSTORELog = this.ultraGrid3.ActiveRow.Cells["FS_RECEIVERSTORE"].Text;
            string strTransnoLog = this.ultraGrid3.ActiveRow.Cells["FS_TRANSNAME"].Text;
            string strWeighttypeLog = this.ultraGrid3.ActiveRow.Cells["FS_TYPENAME"].Text;
            string strWEIGHTLog = this.ultraGrid3.ActiveRow.Cells["FN_WEIGHT"].Text;
            string strMemoLog = this.ultraGrid3.ActiveRow.Cells["fs_Memo"].Text;




            string strLog = "卡号=" + strCardNOLog + ",车号=" + strCarNoLog + ",物料="//卡号、车号
                + strMaterialLog + ",发货单位=" + strSENDERLog + ",收货单位="//物料、发货单位
                + strReceiverLog + ",装货点=" + strSenderstoreLog + ",卸货点="//收货单位、装货地点
                + strRECEIVERSTORELog + ",承运单位=" + strTransnoLog + ",流向="//卸货地点、承运单位
                + strWeighttypeLog + ",重量=" + strWEIGHTLog + ",备注=" + strMemoLog;//流向、重量


            this.objBi.WriteOperationLog("DT_FIRSTCARWEIGHT", strDepartMent, strUserName, "删除", strLog, "一次数据补录", "汽车衡一次数据表","汽车衡");//调用写操作日志方法

        }

        private bool IsValid()
        {
            if (this.cbBF.SelectedValue == null || this.cbBF.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择计量磅房");
                this.cbBF.Focus();
                return false;
            }

            if (this.tbCarNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入车号！");
                this.tbCarNo.Focus();
                return false;
            }

       
            if (this.cbWLMC.Text == "")
            {
                MessageBox.Show("请选择物料！");
                this.cbWLMC.Focus();
                return false;
            }

            if (this.cbFHDW.Text == "")
            {
                MessageBox.Show("请选择发货单位！");
                this.cbFHDW.Focus();
                return false;
            }

            if (this.cbSHDW.Text == "")
            {
                MessageBox.Show("请选择收货单位！");
                this.cbSHDW.Focus();
                return false;
            }

            if (this.cbCYDW.Text == "")
            {
                MessageBox.Show("请选择承运单位！");
                this.cbCYDW.Focus();
                return false;
            }

            if (this.cbFlow.SelectedValue == null || this.cbFlow.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择流向!");
                return false;
            }

            return true;
        }

        private void HandRecordFirstWeight_Load(object sender, EventArgs e)
        {
            m_BaseInfo.ob = this.ob;
            objBi = new BaseInfo(this.ob);
            m_BaseInfo.GetBFData(this.cbBF, "QC");
            DownLoadFlow();
            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadTrans();  //下载磅房对应承运单位信息到内存表
          
            try
            {
                this.BeginTime.Value = DateTime.Today;
                this.EndTime.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //Query();
        }

    

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
           
                string strPointID = cbBF.SelectedValue.ToString().Trim();
                m_SelectPointID = strPointID;
                this.BandPointMaterial(strPointID); //绑定磅房物料
                this.BandPointReceiver(strPointID); //绑定磅房收货单位
                this.BandPointSender(strPointID); //绑定磅房发货单位
                this.BandPointTrans(strPointID); //绑定磅房承运单位
                //this.BandPointCarNo(strPointID);//绑定磅房车号

                this.BandPointProvider(strPointID); //绑定磅房发货单位
           
      
        }

        private void ultraGrid3_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null)            
                return;
            this.cbBF.Text = ultraGrid3.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            m_SelectPointID = ultraGrid3.ActiveRow.Cells["FS_Poundtype"].Text.Trim(); ;
            m_WeightNo = ultraGrid3.ActiveRow.Cells["FS_WEIGHTNO"].Text.Trim();

            this.tbCardNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARDNUMBER"].Text.Trim();
            this.tbCarNo.Text = ultraGrid3.ActiveRow.Cells["FS_CARNO"].Text.Trim();
            this.cbWLMC.Text = ultraGrid3.ActiveRow.Cells["FS_MATERIALNAME"].Text.Trim();
            this.cbFHDW.Text = ultraGrid3.ActiveRow.Cells["FS_SUPPLIERNAME"].Text.Trim();
            this.cbSHDW.Text = ultraGrid3.ActiveRow.Cells["FS_Receiver"].Text.Trim();
            this.tbSenderPlace.Text = ultraGrid3.ActiveRow.Cells["Fs_Senderstore"].Text.Trim();
            this.tbReceiverPlace.Text = ultraGrid3.ActiveRow.Cells["FS_RECEIVERSTORE"].Text.Trim();
            this.cbCYDW.Text = ultraGrid3.ActiveRow.Cells["FS_TRANSNAME"].Text.Trim();
            this.cbFlow.Text = ultraGrid3.ActiveRow.Cells["FS_TYPENAME"].Text.Trim();
            
            this.tbWeight.Text = ultraGrid3.ActiveRow.Cells["FN_WEIGHT"].Text.Trim();
            this.tbBZ.Text = ultraGrid3.ActiveRow.Cells["FS_BZ"].Text.Trim();
            //this.cbProvider.Text = ultraGrid3.ActiveRow.Cells["FS_PROVIDERNAME"].Text.Trim();
            //this.txtCZH.Text = ultraGrid3.ActiveRow.Cells["Fd_Weighttime"].Text.Trim();
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
            m_List.Location = new System.Drawing.Point(81, 76);

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
            m_List.Location = new System.Drawing.Point(81, 112);

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
            m_List.Location = new System.Drawing.Point(322, 115);

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
            m_List.Location = new System.Drawing.Point(322, 78);

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



    }
}
