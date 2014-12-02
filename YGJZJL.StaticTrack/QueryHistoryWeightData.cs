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
    public partial class QueryHistoryWeightData : FrmBase
    {
       

        
        

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
       

        string strWeightPoint = "K12";
      
      
        MoreBaseInfo frm = null;
        int rowno = -1;
        string strOp = "";
        string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        string strDepartMent = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
        string strShift = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder()).ToString();//班次
        string strGroup = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup()).ToString(); //班组
        public QueryHistoryWeightData()
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
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_SUPPLIER = B.Fs_GY and C.Fs_Pointtype = 'JG' ";
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
                        queryHistoryDataByDate();
                    }
                    break;
                case "ToExcel":
                    {
                        try
                        {
                            Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, ultraGrid1);
                            Constant.WaitingForm.Close();
                        }
                        catch (Exception e1)
                        { 
                        }
                    }
                    break;
                default:
                    break;
               
                   
            }
        }

        

        //根据日期查询历史计量数据信息
        private void queryHistoryDataByDate()
        {
            string strBeginDateTime = this.dtpBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string strEndDateTime = this.dtpEnd.Value.ToString("yyyy-MM-dd 23:59:59");
            //string sql = " and FS_WEIGHTPOINT='" + PointID + "' and FS_POTNO = '" + txtGh.Text.Trim() + "'";
            string sql = "select t.fs_weightno,t.fs_material, t.fs_weighttype, t.fs_senderstoreno, t.fs_receiverstoreno,"
                         + "  t.fs_trainno, t.fs_grossperson,to_char(t.fd_grosstime,'yyyy-MM-dd hh24:mi:ss') as fd_grosstime,  "
                         + " t.fs_tareperson,to_char(t.fd_taretime,'yyyy-MM-dd hh24:mi:ss') as fd_taretime,t.fs_grossshift,t.fs_grossgroup,"
                         + " t.fs_tareshift,t.fs_taregroup,to_char(t.fn_grossweight,'999.000') as fn_grossweight,to_char(t.fn_tareweight,'999.000') as fn_tareweight,to_char(t.fn_netweight,'999.000') as fn_netweight ,t.fs_memo,to_char(FD_UPDATETIME,'yyyy-MM-dd hh24:Mi:ss') as fd_updatetime,"
                         + " t.fs_trans, ma.fs_materialname as fs_materialname,  fh.fs_suppliername as fs_sender,  sh.fs_memo as fs_receiver,"
                         + " lx.fs_typename as fs_typename, a.fs_pointname as fs_grosspoint, b.fs_pointname as fs_tarepoint, cy.fs_transname as fs_trans from DT_STATICTRACKWEIGHT_WEIGHT t ,"
                         + " it_material  ma,  it_supplier fh,  it_store sh,  bt_weighttype lx,  bt_point a,bt_point b,  bt_trans cy "
                         + " where t.FS_GROSSPOINT='"+strWeightPoint+"' and t.fs_material = ma.fs_wl(+)  and t.fs_senderstoreno = fh.fs_gy(+)  and t.fs_receiverstoreno = sh.fs_sh(+) "
                         + " and t.fs_weighttype = lx.fs_typecode(+)   and t.fs_grosspoint = a.fs_pointcode(+)  and t.fs_tarepoint=b.fs_pointcode(+)  and t.fs_trans = cy.fs_cy(+) "
                         + " and t.fd_grosstime between to_date('" + strBeginDateTime + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + strEndDateTime + "','yyyy-MM-dd hh24:mi:ss') ";
            if (tbTrainNo.Text != "")
            {
                sql += " and t.fs_trainno='"+tbTrainNo.Text+"' ";
            }
            sql += " and   t.fs_deleteflag!='1' order by fd_grosstime desc";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { sql };
           dataSet2.Tables["静态轨道衡二次计量数据"].Clear();
            ccp.SourceDataTable = this.dataSet2.Tables["静态轨道衡二次计量数据"];


            try
            {
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (Exception ex1)
            {
                //MessageBox.Show("发生异常：" + ex1.Message + "，请重新执行保存操作！");

            }
        }

     
    }
}
