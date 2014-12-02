using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using YGJZJL.PublicComponent;
using Infragistics.Win.UltraWinGrid;


namespace YGJZJL.StaticTrack
{
    public partial class HanderFirstWeightData : FrmBase
    {
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
        public System.Data.DataTable tempFlow = new System.Data.DataTable();

        private System.Data.DataTable m_LoadPointTable = new System.Data.DataTable();//装货点
        public System.Data.DataTable tempLoadPoint = new System.Data.DataTable();

        private System.Data.DataTable m_UnLoadPointTable = new System.Data.DataTable();//卸货点
        public System.Data.DataTable tempUnLoadPoint = new System.Data.DataTable();

        private System.Windows.Forms.ListBox m_List = new System.Windows.Forms.ListBox(); //下拉列表框
        private string m_ListType = "";  //下拉列表框类型 

        string strWeightPoint = "";
        string strWeightNo = "";
        GetBaseInfo getbaseinfo=null;
        MoreBaseInfo frm = null;
        int rowno = -1;
        string strOp = "";
        string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        string strDepartMent = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
        string strShift = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder()).ToString();//班次
        string strGroup =  Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup()); //班组
        

        public HanderFirstWeightData()
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
            dc = new DataColumn("FS_TYPENAME".ToUpper()); m_FlowTable.Columns.Add(dc);
            dc = new DataColumn("FS_FLOW".ToUpper()); m_FlowTable.Columns.Add(dc);
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_FlowTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_FlowTable.Columns.Add(dc);

            // 装货点 FS_TYPECODE, FS_TYPENAME
            dc = new DataColumn("FS_LOADPOINTNAME".ToUpper()); this.m_LoadPointTable.Columns.Add(dc);
            dc = new DataColumn("FS_LOAD".ToUpper()); m_LoadPointTable.Columns.Add(dc);
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_LoadPointTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_LoadPointTable.Columns.Add(dc);


            // 卸货点 FS_TYPECODE, FS_TYPENAME
            dc = new DataColumn("FS_UNLOADPOINTNAME".ToUpper()); this.m_UnLoadPointTable.Columns.Add(dc);
            dc = new DataColumn("FS_UNLOAD".ToUpper()); m_UnLoadPointTable.Columns.Add(dc);
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_UnLoadPointTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_UnLoadPointTable.Columns.Add(dc);
        }

        //下载磅房对应物料基础信息   

        private void DownLoadMaterial()
        {
            try
            {
                string strSql = "select A.FS_PointNo, A.FS_MATERIALNO, b.fs_materialname, b.FS_HELPCODE, a.fn_times ";
                strSql += " from Bt_Pointmaterial A, It_Material B, Bt_Point C ";
                strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.Fs_Materialno = B.Fs_Wl and C.Fs_Pointtype = 'JG' ";
                strSql += "  order by a.fn_times desc ";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.base.QueryData";//kiscicplat
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { strSql };
                ccp.SourceDataTable = this.m_MaterialTable;

                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (Exception ex)
            {
                Console.Write("发生异常:" + ex.Message + "\n");
                Console.Read();
            }
        }
        //下载磅房对应收货单位信息
        private void DownLoadReceiver()
        {
            string strSql = "select A.FS_PointNo, A.FS_Receiver, b.fs_memo, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointReceiver A, It_Store B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_Receiver = B.Fs_SH and C.Fs_Pointtype = 'JG' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ReveiverTable;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对应发货单位信息  ,add by zzy
        private void DownLoadSender()
        {
            string strSql = "select A.FS_PointNo, A.FS_SUPPLIER, b.FS_SUPPLIERNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_Pointsupplier A, IT_SUPPLIER B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_SUPPLIER = B.Fs_GY  and C.Fs_Pointtype = 'JG' ";
            strSql += "  order by a.fn_times desc ";



            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SenderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }



        //下载磅房对供应单位信息  ,add by zzy 
        private void DownLoadProvider()
        {
            string strSql = "select A.FS_PointNo, A.FS_PROVIDER, b.FS_PROVIDERNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from BT_POINTPROVIDER A, IT_PROVIDER B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_PROVIDER = B.FS_SP and C.Fs_Pointtype = 'JG' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ProviderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }


        //下载磅房对承运单位信息  ,add by zzy 
        private void DownLoadTrans()
        {
            string strSql = "select A.FS_PointNo, A.FS_TransNo, b.FS_TRANSNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointTrans A, BT_Trans B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_TransNo = B.Fs_CY and C.Fs_Pointtype = 'JG' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_TransTable;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }



        //下载磅房对应车号信息  ,add by zzy 
        private void DownLoadCarNo()
        {
            string strSql = "select FS_POINTNO, FS_CARNO, FN_TIMES From BT_POINTCARNO order by FN_TIMES desc ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_CarNoTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


        }

        //下载流向信息  ,add by zzy 
        private void DownLoadFlow()
        {
            string strSql = "select FS_POINTNO, FS_FLOW ,FN_TIMES,a.fs_typename  From bt_pointflow m left join bt_weighttype a on m.fs_flow = a.fs_typecode order by FN_TIMES ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_FlowTable;
            m_FlowTable.Clear();
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }
        //下载磅房对应装货点基础信息   

        //private void DownLoadLoadPoint()
        //{
        //    try
        //    {
        //        string strSql = "select A.FS_PointNo, A.FS_LOAD, b.FS_LOADPOINTNAME, b.FS_HELPCODE, a.fn_times ";
        //        strSql += " from BT_POINTLOAD A, It_LOADPOINT B, Bt_Point C ";
        //        strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_LOAD = B.Fs_ZH and C.Fs_Pointtype = 'QC' ";
        //        strSql += "  order by a.fn_times desc ";

        //        CoreClientParam ccp = new CoreClientParam();
        //        ccp.ServerName = "ygjzjl.base.QueryData";//kiscicplat
        //        ccp.MethodName = "queryByClientSql";
        //        ccp.ServerParams = new object[] { strSql };
        //        ccp.SourceDataTable = this.m_LoadPointTable;

        //        this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write("发生异常:" + ex.Message + "\n");
        //        Console.Read();
        //    }
        //}
        //下载磅房对应卸货点基础信息   

        //private void DownLoadUnLoadPoint()
        //{
        //    try
        //    {
        //        string strSql = "select A.FS_PointNo, A.FS_UNLOAD, b.FS_UNLOADPOINTNAME, b.FS_HELPCODE, a.fn_times ";
        //        strSql += " from BT_POINTUNLOAD A, It_UNLOADPOINT B, Bt_Point C ";
        //        strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_UNLOAD = B.Fs_XH and C.Fs_Pointtype = 'QC' ";
        //        strSql += "  order by a.fn_times desc ";

        //        CoreClientParam ccp = new CoreClientParam();
        //        ccp.ServerName = "ygjzjl.base.QueryData";//kiscicplat
        //        ccp.MethodName = "queryByClientSql";
        //        ccp.ServerParams = new object[] { strSql };
        //        ccp.SourceDataTable = this.m_UnLoadPointTable;

        //        this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write("发生异常:" + ex.Message + "\n");
        //        Console.Read();
        //    }
        //}


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
            this.cbMaterial.DataSource = tempMaterial;
            cbMaterial.DisplayMember = "fs_materialname";
            cbMaterial.ValueMember = "FS_MATERIALNO";
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
            this.cbReceiver.DataSource = this.tempReveiver;
            this.cbReceiver.DisplayMember = "FS_MEMO";
            this.cbReceiver.ValueMember = "FS_Receiver";
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
            this.cbSender.DataSource = this.tempSender;
            this.cbSender.DisplayMember = "FS_SUPPLIERNAME";
            this.cbSender.ValueMember = "FS_SUPPLIER";
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
            this.cbTrans.DataSource = this.tempTrans;
            cbTrans.DisplayMember = "FS_TRANSNAME";
            cbTrans.ValueMember = "FS_TRANSNO";

        }

    

        //按磅房筛选流向

        private void BandPointFlow(string PointID)
        {
            DataRow[] drs = null;

            this.tempFlow = this.m_FlowTable.Clone();

            drs = this.m_FlowTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempFlow.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempFlow.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempFlow.NewRow();
            this.tempFlow.Rows.InsertAt(drz, 0);
            this.cbFlow.DataSource = this.tempFlow;
            cbFlow.DisplayMember = "fs_typename";
            cbFlow.ValueMember = "fs_flow";
            //cbCH1.ValueMember = "FS_TRANSNO";

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

        private void BaseOperation()
        {
            //物料基础操作
            if (this.cbMaterial.Text.Trim().Length > 0)
            {
                if (this.cbMaterial.SelectedValue!= null)
                    getbaseinfo.SetNonBaseData(strWeightPoint, cbMaterial.SelectedValue.ToString().Trim(), "SetWLData");
                   
                
            }

            //发货方基础操作
            if (this.cbSender.Text.Trim().Length > 0)
            {
                if (this.cbSender.SelectedValue != null)

                    getbaseinfo.SetNonBaseData(strWeightPoint, this.cbSender.SelectedValue.ToString().Trim(), "SetFHDWData");
                
            }

            //收货方基础操作
            if (this.cbReceiver.Text.Trim().Length > 0)
            {
                if (this.cbReceiver.SelectedValue != null)

                    getbaseinfo.SetNonBaseData(strWeightPoint, this.cbReceiver.SelectedValue.ToString().Trim(), "SetSHDWData");
                
            }

            //承运方基础操作
            if (this.cbTrans.Text.Trim().Length > 0)
            {
                if (this.cbTrans.SelectedValue != null)

                    getbaseinfo.SetNonBaseData(strWeightPoint, this.cbTrans.SelectedValue.ToString().Trim(), "SetCYDWData");

            }

            ////流向基础操作
            //if (this.cbFlow.Text.Trim().Length > 0)
            //{
            //    if (this.cbFlow.SelectedValue != null)
            //        getbaseinfo.SetNonBaseData(strWeightPoint, this.cbFlow.Text.Trim(), "SetLXData");
            //}

           
        }

      

        private void GetPoint()//绑定磅房下拉框
        {


            string strTmpTable, strTmpField, strTmpOrder, strTmpWhere;

            CoreClientParam ccp = new CoreClientParam();
            strTmpTable = "BT_POINT";
            strTmpField = "FS_POINTCODE,FS_POINTNAME";
            strTmpWhere = " AND FS_POINTTYPE = 'JG'";
            strTmpOrder = "";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryData";
            ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, strTmpOrder };

            DataTable dtPoint = new DataTable();
            ccp.SourceDataTable = dtPoint;

            try
            {
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
             
            }
            catch (Exception)
            {
            }
          
            if (dtPoint.Rows.Count > 0)
            {
                DataRow dr = dtPoint.NewRow();
                dtPoint.Rows.InsertAt(dr, 0);

                this.cbWeightPoint.DataSource = dtPoint;
                cbWeightPoint.DisplayMember = "FS_POINTNAME";
                cbWeightPoint.ValueMember = "FS_POINTCODE";

                cbWeightPoint.AutoCompleteMode =System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                cbWeightPoint.AutoCompleteSource =System.Windows.Forms.AutoCompleteSource.ListItems;
            }
            else
            {
                cbWeightPoint.DataSource = dtPoint;
            }
        }

   

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        if (this.dtpBegin.Value > this.dtpEnd.Value)
                        {
                            MessageBox.Show("开始日期必须小于结束日期!");
                            return;
                        }
                        queryFirstDataByDate();
                    }
                    break;
                case "Update":
                    {
                        if (!CheckInPut())
                        {
                            return;
                        }
                        

                        UpdateFirstData();
                        queryFirstDataByDate();
                    }
                    break;
                case "Add":
                    {
                        if (!CheckInPut())
                        {
                            return;
                        }
                        

                        AddFirstData();
                        queryFirstDataByDate();
                        
                    }
                    break;
                case "Delete":
                    {
                        DeleteFirstData();
                        queryFirstDataByDate();
                    }
                    break;
            }
        }

        private bool CheckInPut()//输入检查
        {
            if (this.cbWeightPoint.SelectedValue== null || this.cbWeightPoint.Text == "")
            {
                MessageBox.Show("请先选择磅房信息!");
                cbWeightPoint.Focus();
                return false;
            }
           
            if (txtTRAINNO.Text.Trim() == "")
            {
                MessageBox.Show("请先输入火车车号!");
                txtTRAINNO.Focus();
                return false;
            }
            if (txtTRAINNO.Text.Trim().Length > 20)
            {
                MessageBox.Show("车号不能大于20位，请重新进行输入！");
                txtTRAINNO.Focus();
                return false;
            }
            if (this.cbFlow.SelectedValue == null || this.cbFlow.Text == "")
            {
                MessageBox.Show("请先选择流向信息!");
                cbFlow.Focus();
                return false;
            }
            if (this.cbMaterial.SelectedValue == null || this.cbMaterial.Text == "")
            {
                MessageBox.Show("请先选择物料信息!");
                cbMaterial.Focus();
                return false;
            }
            if (this.cbTrans.SelectedValue == null || this.cbTrans.Text == "")
            {
                MessageBox.Show("请先选择承运单位信息!");
                cbTrans.Focus();
                return false;
            }
            if (this.cbSender.SelectedValue == null || this.cbSender.Text == "")
            {
                MessageBox.Show("请先选择发货单位信息!");
                cbSender.Focus();
                return false;
            }
            if (this.cbReceiver.SelectedValue == null || this.cbReceiver.Text == "")
            {
                MessageBox.Show("请先选择收货单位信息!");
                cbReceiver.Focus();
                return false;
            }

            if (txtGrossWeight.Text == "")
            {
                MessageBox.Show("请输入毛重重量!");
                txtGrossWeight.Focus();
                return false;
            }
           
            if (txtGrossWeight.Text.Length > 10)
            {
                MessageBox.Show("毛重不能超过10位数");
                txtGrossWeight.Focus();
                return false;
            }
               
            try
            {
                Convert.ToDouble(txtGrossWeight.Text);
                
                
            }
            catch
            {
                MessageBox.Show("请输入正确的毛重、皮重信息!");
                txtGrossWeight.Focus();
                return false;
            }
            
         
            return true;
               
        }

             

       
        private void ultraGrid1_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            
            rowno = ultraGrid1.ActiveRow.Index;
            if (rowno < 0)
            {
                return;
            }

            strOp = ultraGrid1.Rows[rowno].Cells["FS_WEIGHTNO"].Text.ToString();
            cbWeightPoint.Text=ultraGrid1.Rows[rowno].Cells["FS_POINTNAME"].Text.ToString();
            cbFlow.Text=ultraGrid1.Rows[rowno].Cells["FS_TYPENAME"].Text.ToString();
            cbMaterial.Text = ultraGrid1.Rows[rowno].Cells["FS_MATERIALNAME"].Text.ToString();
            cbTrans.Text = ultraGrid1.Rows[rowno].Cells["FS_TRANS"].Text.ToString();
            cbSender.Text = ultraGrid1.Rows[rowno].Cells["FS_SENDER"].Text.ToString();
            cbReceiver.Text = ultraGrid1.Rows[rowno].Cells["FS_RECEIVER"].Text.ToString();
            txtTRAINNO.Text = ultraGrid1.Rows[rowno].Cells["FS_TRAINNO"].Text.ToString();
            txtGrossWeight.Text = ultraGrid1.Rows[rowno].Cells["FN_WEIGHT"].Text.ToString();
            
        }

        private void cbWeightPoint_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cbWeightPoint.Text == "System.Data.DataRowView" || cbWeightPoint.Text == "" || cbWeightPoint.SelectedValue == null)
            {
                return;
            }
            strWeightPoint = cbWeightPoint.SelectedValue.ToString();

            this.BandPointMaterial(strWeightPoint); //绑定磅房物料
            this.BandPointReceiver(strWeightPoint); //绑定磅房收货单位
            this.BandPointSender(strWeightPoint); //绑定磅房发货单位
            this.BandPointTrans(strWeightPoint); //绑定磅房承运单位
            GetLXData();//绑定流向信息
            //getbaseinfo.GetLXData(cbFlow, strWeightPoint);
            //getbaseinfo.GetWLData(cbMaterial, strWeightPoint);
            //getbaseinfo.GetCYDWData(cbTrans, strWeightPoint);
            //getbaseinfo.GetFHDWData(cbSender, strWeightPoint);
            //getbaseinfo.GetSHDWData(cbReceiver, strWeightPoint);
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
                    this.cbMaterial.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbMaterial.Focus();
                    m_List.Visible = false;
                    break;
                case "Reveiver":
                    this.cbReceiver.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbReceiver.Focus();
                    m_List.Visible = false;
                    break;
                case "Sender":
                    this.cbSender.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbSender.Focus();
                    m_List.Visible = false;
                    break;
                
                case "Trans":
                    this.cbTrans.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbTrans.Focus();
                    m_List.Visible = false;
                    break;
              
                default:
                    m_List.Visible = false;
                    break;
            }
        }

        private void m_List_KeyPress(object sender, KeyPressEventArgs e)
        {
            string text = "";
            switch (m_ListType)
            {
                case "Material":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        this.cbMaterial.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbMaterial.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        this.cbMaterial.Focus();
                        text = cbMaterial.Text + e.KeyChar;
                        cbMaterial.Text = text;
                        cbMaterial.SelectionStart = cbMaterial.Text.Length;
                    }
                    break;
                case "Reveiver":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        this.cbReceiver.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbReceiver.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbReceiver.Focus();
                        text = cbReceiver.Text + e.KeyChar;
                        cbReceiver.Text = text;
                        cbReceiver.SelectionStart = cbReceiver.Text.Length;
                    }
                    break;
                case "Sender":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        this.cbSender.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbSender.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbSender.Focus();
                        text = cbSender.Text + e.KeyChar;
                        cbSender.Text = text;
                        cbSender.SelectionStart = cbSender.Text.Length;
                    }
                    break;
                
                case "Trans":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        this.cbTrans.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbTrans.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbTrans.Focus();
                        text = cbTrans.Text + e.KeyChar;
                        cbTrans.Text = text;
                        cbTrans.SelectionStart = cbTrans.Text.Length;
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 从数据库获取流向数据(DHS项目实施后要做相应修改,不能固定成K02)
        /// </summary>
        private void GetLXData()//客户端加参数，调用kiscicplat.common.queryByClient
        {
           
            //string strBFID = "K02";
            string sql = " select DISTINCT FS_TYPECODE,FS_TYPENAME,FS_HELPCODE FROM BT_WEIGHTTYPE A  ";
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "kiscicplat.dhs.carweight.CarWeightPrediction";
            //ccp.MethodName = "QueryLXData";
            //ccp.ServerParams = new object[] { strBFID };
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtLX = new System.Data.DataTable();
            ccp.SourceDataTable = dtLX;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtLX.Rows.Count > 0)
            {
                DataRow dr = dtLX.NewRow();
                dtLX.Rows.InsertAt(dr, 0);

                this.cbFlow.DataSource = dtLX;
                cbFlow.DisplayMember = "FS_TYPENAME";
                cbFlow.ValueMember = "FS_TYPECODE";

                cbFlow.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbFlow.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbFlow.DataSource = dtLX;
            }
        }
        void m_List_Leave(object sender, EventArgs e)
        {

        }

        private void btMaterial_Click(object sender, EventArgs e)
        {
            
            if (this.cbWeightPoint.SelectedValue == null|| this.cbWeightPoint.SelectedValue.ToString()=="")
            {
                MessageBox.Show("请先选择磅房信息!");
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            frm = new MoreBaseInfo(this, Btn.Tag.ToString(),this.ob);
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btTrans_Click(object sender, EventArgs e)
        {
            if (this.cbWeightPoint.SelectedValue == null || this.cbWeightPoint.SelectedValue.ToString() == "")
            {
                MessageBox.Show("请先选择磅房信息!");
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(),this.ob);
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btSender_Click(object sender, EventArgs e)
        {
            if (this.cbWeightPoint.SelectedValue == null || this.cbWeightPoint.SelectedValue.ToString() == "")
            {
                MessageBox.Show("请先选择磅房信息!");
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(),this.ob);
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btReceiver_Click(object sender, EventArgs e)
        {
            if (this.cbWeightPoint.SelectedValue == null || this.cbWeightPoint.SelectedValue.ToString() == "")
            {
                MessageBox.Show("请先选择磅房信息!");
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(),this.ob);
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void cbMaterial_Leave(object sender, EventArgs e)
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
                if (this.cbMaterial.SelectedValue == null || this.cbMaterial.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbMaterial.Text = "";
                    }
                }
                //if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                //{
                //    if (m_List.Visible == false)
                //    {
                //        MessageBox.Show("请选择物料");
                //        this.cbWLMC.Text = "";
                //        this.cbWLMC.Focus();
                //    }
                //}
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbTrans_Leave(object sender, EventArgs e)
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
                if (this.cbTrans.SelectedValue == null || this.cbTrans.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbTrans.Text = "";
                    }
                }
                //if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                //{
                //    if (m_List.Visible == false)
                //    {
                //        MessageBox.Show("请选择物料");
                //        this.cbWLMC.Text = "";
                //        this.cbWLMC.Focus();
                //    }
                //}
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbSender_Leave(object sender, EventArgs e)
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
                if (this.cbSender.SelectedValue == null || this.cbSender.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbSender.Text = "";
                    }
                }
                //if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                //{
                //    if (m_List.Visible == false)
                //    {
                //        MessageBox.Show("请选择物料");
                //        this.cbWLMC.Text = "";
                //        this.cbWLMC.Focus();
                //    }
                //}
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbReceiver_Leave(object sender, EventArgs e)
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
                if (this.cbReceiver.SelectedValue == null || this.cbReceiver.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbReceiver.Text = "";
                    }
                }
                //if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                //{
                //    if (m_List.Visible == false)
                //    {
                //        MessageBox.Show("请选择物料");
                //        this.cbWLMC.Text = "";
                //        this.cbWLMC.Focus();
                //    }
                //}
            }
            catch (System.Exception exp)
            {

            }
        }


        private void ChangeString(ComboBox combox)
        {

            System.Windows.Forms.ComboBox cb = combox;
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
            combox.Text = cb.Text;


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

        private void cbMaterial_TextChanged(object sender, EventArgs e)
        {
            if (this.cbMaterial.Text.Trim().Length == 0 || this.cbMaterial.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            //if (cbBF.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbWLMC.Text = "";
            //    return;
            //}

            if (m_List == null || this.cbMaterial.Focused == false)
            {
                return;
            }

            ChangeString(cbMaterial);//全角转换为半角，导致死循环

            for (int i = 0; i < cbMaterial.Text.Length; i++)
            {
                if (Char.IsLower(cbMaterial.Text[i]) == false && Char.IsUpper(cbMaterial.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Material";
            m_List.Location = new System.Drawing.Point(123, 133);

            string text = this.cbMaterial.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            if (tempMaterial.Rows.Count > 0)
            {
                matchRows = this.tempMaterial.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_TIMES desc");

                m_List.Items.Clear();
                foreach (DataRow dr in matchRows)
                {
                    m_List.Items.Add(dr["FS_MaterialName"].ToString());
                }
                m_List.Visible = true;
            }
        }

        private void cbTrans_TextChanged(object sender, EventArgs e)
        {
            if (this.cbTrans.Text.Trim().Length == 0 || this.cbTrans.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            //if (cbBF.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbWLMC.Text = "";
            //    return;
            //}

            if (m_List == null || this.cbTrans.Focused == false)
            {
                return;
            }

            ChangeString(cbTrans);//全角转换为半角，导致死循环

            for (int i = 0; i < cbTrans.Text.Length; i++)
            {
                if (Char.IsLower(cbTrans.Text[i]) == false && Char.IsUpper(cbTrans.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Trans";
            m_List.Location = new System.Drawing.Point(440, 133);

            string text = this.cbTrans.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            if (this.tempTrans.Rows.Count > 0)
            {
                matchRows = this.tempTrans.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_TIMES desc");

                m_List.Items.Clear();
                foreach (DataRow dr in matchRows)
                {
                    m_List.Items.Add(dr["FS_TRANSNAME"].ToString());
                }
                m_List.Visible = true;
            }
        }

        private void cbSender_TextChanged(object sender, EventArgs e)
        {
            if (this.cbSender.Text.Trim().Length == 0 || this.cbSender.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            //if (cbBF.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbWLMC.Text = "";
            //    return;
            //}

            if (m_List == null || this.cbSender.Focused == false)
            {
                return;
            }

            ChangeString(cbSender);//全角转换为半角，导致死循环

            for (int i = 0; i < cbSender.Text.Length; i++)
            {
                if (Char.IsLower(cbSender.Text[i]) == false && Char.IsUpper(cbSender.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Sender";
            m_List.Location = new System.Drawing.Point(123, 179);

            string text = this.cbSender.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            if (this.tempSender.Rows.Count > 0)
            {
                matchRows = this.tempSender.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_TIMES desc");

                m_List.Items.Clear();
                foreach (DataRow dr in matchRows)
                {
                    m_List.Items.Add(dr["FS_MEMO"].ToString());
                }
                m_List.Visible = true;
            }
       
        }

        private void cbReceiver_TextChanged(object sender, EventArgs e)
        {
            if (this.cbReceiver.Text.Trim().Length == 0 || this.cbReceiver.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            //if (cbBF.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    cbSHDW.Text = "";
            //    return;
            //}

            if (m_List == null || cbReceiver.Focused == false)
            {
                return;
            }

            ChangeString(cbReceiver);//全角转换为半角，导致死循环

            for (int i = 0; i < cbReceiver.Text.Length; i++)
            {
                if (Char.IsLower(cbReceiver.Text[i]) == false && Char.IsUpper(cbReceiver.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Reveiver";
            m_List.Location = new System.Drawing.Point(440, 176);

            string text = this.cbReceiver.Text;
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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dtpTareTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ultraGroupBox2_Click(object sender, EventArgs e)
        {

        }


        //根据日期查询一次计量数据信息
        private void queryFirstDataByDate()
        {
            string strBeginDateTime = this.dtpBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string strEndDateTime = this.dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59");
            //string sql = " and FS_WEIGHTPOINT='" + PointID + "' and FS_POTNO = '" + txtGh.Text.Trim() + "'";
            string sql = "select t.fs_weightno,t.fs_material, t.fs_weighttype, t.fs_senderstoreno, t.fs_receiverstoreno,t.fs_shift,t.fs_group,t.fs_memo,to_char(t.fd_updatetime,'yyyy-MM-dd hh24:mi:ss') as fd_updatetime,"
                         + "  t.fs_trainno, to_char(t.fn_weight,'999.000') as fn_weight, t.fs_weightperson,to_char(t.fd_weighttime,'yyyy-MM-dd hh24:mi:ss') as fd_weighttime, t.fs_weightpoint,  t.fs_deleteflag,  t.fs_deleteuser,"
                         + " to_char(t.fd_deletedate,'yyyy-MM-dd hh24:mi:ss') as fd_deletedate, t.fs_transno, ma.fs_materialname as fs_materialname,  fh.fs_suppliername as fs_sender,  sh.fs_memo as fs_receiver,"
                         + " lx.fs_typename as fs_typename, jld.fs_pointname as fs_pointname,  cy.fs_transname as fs_trans from dt_statictrackfirstweight t ,"
                         + " it_material  ma,  it_supplier fh,  it_store sh,  bt_weighttype lx,  bt_point jld,  bt_trans cy "
                         + " where t.fs_weightpoint='K12'  AND t.fs_material = ma.fs_wl(+)  and t.fs_senderstoreno = fh.fs_gy(+)  and t.fs_receiverstoreno = sh.fs_sh(+) "
                         + " and t.fs_weighttype = lx.fs_typecode(+)   and t.fs_weightpoint = jld.fs_pointcode(+)  and t.fs_transno = cy.fs_cy(+) "
                         + " and t.fd_weighttime between to_date('" + strBeginDateTime + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + strEndDateTime + "','yyyy-MM-dd hh24:mi:ss') ";
            if (tbTrainNo.Text != "")
            {
                sql += " and t.fs_trainno='"+tbTrainNo.Text.Trim()+"'";
            }
            sql += " and   t.fs_deleteflag!='1' order by fd_weighttime desc";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { sql };
            this.dataSet2.Tables["静态轨道衡一次计量数据"].Clear();
            ccp.SourceDataTable = this.dataSet2.Tables["静态轨道衡一次计量数据"];


            try
            {
                 this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (Exception ex1)
            {
                //MessageBox.Show("发生异常：" + ex1.Message + "，请重新执行保存操作！");

            }
        }

       //新增一次数据
        private void AddFirstData()
        {
            string strTmpTable, strTmpField, strTmpValue;
            strOp = Guid.NewGuid().ToString();
   

           
            string strTrainNo = txtTRAINNO.Text.Trim();
            string strFlow = cbFlow.SelectedValue != null ? cbFlow.SelectedValue.ToString() : "";
            string strMaterial = cbMaterial.SelectedValue != null ? cbMaterial.SelectedValue.ToString() : "";
            string strSender = cbSender.SelectedValue != null ? cbSender.SelectedValue.ToString() : "";
            string strReceiver = cbReceiver.SelectedValue != null ? cbReceiver.SelectedValue.ToString() : "";
            string strTrans = cbTrans.SelectedValue != null ? cbTrans.SelectedValue.ToString() : "";
            string strGrossWeight = txtGrossWeight.Text;
            string strGrossWeightTime = dtpGrossTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
           strWeightPoint = cbWeightPoint.SelectedValue.ToString();

           string sql = "select * from DT_STATICTRACKFIRSTWEIGHT t where t.fs_trainno='"+strTrainNo+"'";
           CoreClientParam ccp = new CoreClientParam();
           ccp.ServerName = "ygjzjl.base.QueryData";
           ccp.MethodName = "queryByClientSql";
           ccp.ServerParams = new object[] { sql };
           DataTable dt = new DataTable();
           ccp.SourceDataTable = dt;
           this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
           if (dt.Rows.Count > 0)
           {
               MessageBox.Show("系统中已经存在该车号的一次记录，请核对车号后再操作!");
               return;
           }


            
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "insertDataInfo";

            strTmpTable = "DT_STATICTRACKFIRSTWEIGHT";
            strTmpField = " FS_WEIGHTNO,FS_TRAINNO,FS_MATERIAL,FS_WEIGHTTYPE,FS_SENDERSTORENO,FS_RECEIVERSTORENO,FS_TRANSNO,"
                        + " FN_WEIGHT,FS_WEIGHTPERSON,FD_WEIGHTTIME,FS_WEIGHTPOINT,FS_SHIFT,FS_GROUP,FS_MEMO ";
            strTmpValue = "'" + strOp + "','" + strTrainNo + "','" + strMaterial + "','" + strFlow + "','" + strSender + "','" + strReceiver + "','" + strTrans
                        + "','" + strGrossWeight + "','" + strUserName + "',to_date('" + strGrossWeightTime + "','yyyy-MM-dd hh24:mi:ss'),'" + strWeightPoint + "','" + strShift + "','" + strGroup + "','"+strUserName+" 新增'";


            ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };




            string strLog = "车号=" + strTrainNo  + ",物料="//卡号、车号
                + cbMaterial.Text + ",发货单位=" + cbSender.Text +  ",收货单位="//物料、发货单位
                + cbReceiver.Text + ",承运单位=" + cbTrans.Text+ ",流向="//卸货地点、承运单位
                + cbFlow.Text +",重量=" + txtGrossWeight.Text + ",";//流向、重量


            try
            {
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            }
            catch (Exception)
            {
            }
            if (ccp.ReturnCode == 0)
            {

                MessageBox.Show("数据保存成功！");
                BaseOperation();
                this.objBi.WriteOperationLog("DT_STATICTRACKFIRSTWEIGHT", strDepartMent, strUserName, "增加", strLog, "一次数据补录", "静态轨道衡一次数据表", "静态轨道衡");//调用写操作日志方法

                return ;
            }
            else
            {
                MessageBox.Show("数据保存失败！");
                return ;
            }

        }



        //修改一次数据
        private void UpdateFirstData()
        {

            if (rowno < 0)
            {
                MessageBox.Show("请先双击选择要修改的数据!");
                return;
            }
            string strGrossWeightTime = this.dtpGrossTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
           
            string strTrainNo = txtTRAINNO.Text.Trim();
            string strFlow = cbFlow.SelectedValue != null ? cbFlow.SelectedValue.ToString() : "";
            string strMaterial = cbMaterial.SelectedValue != null ? cbMaterial.SelectedValue.ToString() : "";
            string strSender = cbSender.SelectedValue != null ? cbSender.SelectedValue.ToString() : "";
            string strReceiver = cbReceiver.SelectedValue != null ? cbReceiver.SelectedValue.ToString() : "";
            string strTrans = cbTrans.SelectedValue != null ? cbTrans.SelectedValue.ToString() : "";
            string strGrossWeight = this.txtGrossWeight.Text;
         
         
            strWeightPoint = cbWeightPoint.SelectedValue.ToString();
            string strUpdateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //PicWeight = p_FN_GROSSWEIGHT;
            //string p_FD_ACCOUNTDATE = p_FD_GROSSTIME.Substring(0,10);


            string strTmpTable, strTmpField, strTmpWhere;
            strTmpTable = "DT_STATICTRACKFIRSTWEIGHT";
            strTmpField = " FS_TRAINNO='" + strTrainNo + "',FS_MATERIAL='" + strMaterial + "',FS_WEIGHTTYPE='" + strFlow + "',"
                       + " FS_SENDERSTORENO='" + strSender + "',FS_RECEIVERSTORENO='" + strReceiver + "',"
                       + " FS_TRANSNO='" + strTrans + "',FD_UPDATETIME=to_date('" + strUpdateTime + "','yyyy-MM-dd hh24:mi:ss'),"
                       + " FN_WEIGHT='" + strGrossWeight  + "',FS_MEMO='" + strUserName + " 修改'";
            strTmpWhere = " FS_WEIGHTNO='" + strOp + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "check_UpdateInfo";
            ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };


            string strCarNoLog = this.ultraGrid1.Rows[rowno].Cells["FS_TRAINNO"].Text;
            string strMaterialLog = this.ultraGrid1.Rows[rowno].Cells["FS_MATERIALNAME"].Text;
            string strSENDERLog = this.ultraGrid1.Rows[rowno].Cells["fs_sender"].Text;
            string strReceiverLog = this.ultraGrid1.Rows[rowno].Cells["fs_receiver"].Text;
     
            string strTransnoLog = this.ultraGrid1.Rows[rowno].Cells["fs_trans"].Text;
            string strWeighttypeLog = this.ultraGrid1.Rows[rowno].Cells["fs_typename"].Text;
            string strWEIGHTLog = this.ultraGrid1.Rows[rowno].Cells["FN_WEIGHT"].Text;
            


            string strLog = "车号：" + strCarNoLog + ">" + strTrainNo + ",物料："//卡号、车号
                + strMaterialLog + ">" + cbMaterial.Text + ",发货单位：" + strSENDERLog + ">" + cbSender.Text + ",收货单位："//物料、发货单位
                + strReceiverLog + ">" + cbReceiver.Text +  ",承运单位：" + strTransnoLog + ">" + cbTrans.Text + ",流向："//卸货地点、承运单位
                + strWeighttypeLog + ">" + cbFlow.Text + ",重量：" + strWEIGHTLog + ">" + strGrossWeight + ",";//流向、重量


            
            try
            {
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message + "数据保存失败！");
                return ;
            }

            if (ccp.ReturnCode == 0)
            {

                MessageBox.Show("数据保存成功！");
                BaseOperation();
                this.objBi.WriteOperationLog("DT_STATICTRACKFIRSTWEIGHT", strDepartMent, strUserName, "修改", strLog, "一次数据补录", "静态轨道衡一次数据表", "静态轨道衡");//调用写操作日志方法

                return ;
            }
            else
            {
                MessageBox.Show("数据保存失败！");
                return ;
            }
            txtGrossWeight.Text = "";
            



            rowno = -1;
        }

        private void DeleteFirstData()
        {
           
            string strTmpTable, strTmpWhere;

            if (rowno < 0)
            {
                MessageBox.Show("请先双击选择要修改的数据!");
                return;
            }



            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "DeleteData";

            strTmpTable = "DT_STATICTRACKFIRSTWEIGHT";

            strTmpWhere = " FS_WEIGHTNO='" + strOp + "'";

            ccp.ServerParams = new object[] { strTmpTable, strTmpWhere };


            string strLog = "车号=" + this.ultraGrid1.Rows[rowno].Cells["FS_TRAINNO"].Text + ",物料="//卡号、车号
                + cbMaterial.Text + ",发货单位=" + cbSender.Text + ",收货单位="//物料、发货单位
                + cbReceiver.Text + ",承运单位=" + cbTrans.Text + ",流向="//卸货地点、承运单位
                + cbFlow.Text + ",重量=" + txtGrossWeight.Text + ",";//流向、重量

            try
            {
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                this.objBi.WriteOperationLog("DT_STATICTRACKFIRSTWEIGHT", strDepartMent, strUserName, "删除", strLog, "一次数据补录", "静态轨道衡一次数据表", "静态轨道衡");//调用写操作日志方法

            }
            catch (Exception)
            {
            }
            rowno = -1;
        }

        private void HanderHistoryWeightData_Load(object sender, EventArgs e)
        {
            objBi = new BaseInfo(this.ob);
            getbaseinfo = new GetBaseInfo(this.ob);
            GetPoint();//绑定磅房下拉框

            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadTrans();  //下载磅房对应承运单位信息到内存表
            this.DownLoadCarNo(); //下载磅房对应车号信息到内存表
            this.DownLoadFlow();//下载磅房对应流向信息到内存表
            //this.DownLoadLoadPoint();//下载磅房对应装货点信息到内存表
            //this.DownLoadUnLoadPoint();//下载磅房对应卸货点信息到内存表
        }


        private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }
    }
}
