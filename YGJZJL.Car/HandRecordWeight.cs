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
using Excel;

namespace YGJZJL.Car
{
    public partial class HandRecordWeight : FrmBase
    {
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        BaseInfo m_BaseInfo_1 = new BaseInfo();

        string m_SelectPointID = "";
        string m_WeightNo = "";
        Excel.Application app;
        _Workbook workbook;



        LimitQueryTime limitQueryTime = new LimitQueryTime();//为判断时间区间设定的变量
        DateTime beginTime;
        DateTime endTime;
        bool decisionResult;
        string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        string strDepartMent = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();

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

        //下载流向  ,add by luobin 
        //private void DownLoadFlow()
        //{
        //    string strSql = "select FS_TYPECODE, FS_TYPENAME From BT_WEIGHTTYPE order by FS_TYPECODE";


        //    CoreClientParam ccp = new CoreClientParam();
        //    ccp.ServerName = "ygjzjl.car.TransPlanInfo";
        //    ccp.MethodName = "ExcuteQuery";
        //    ccp.ServerParams = new object[] { strSql };
        //    ccp.SourceDataTable = this.m_FlowTable;

        //    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        //}

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

        public HandRecordWeight()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    if (!Decision())//判断时间区间//杨滔添加
                    {
                        return;
                    }
                    {
                        Query();
                        break;
                    }
                case "Update":
                    {
                        if (this.ultraGrid3.ActiveRow == null || this.ultraGrid3.ActiveRow.Selected == false || this.ultraGrid3.Rows.Count <= 0)
                        {
                            MessageBox.Show("请选择一条信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        if (DialogResult.Yes == MessageBox.Show("您确认要修改该条记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            Update();
                            Query();
                        }
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
                case "Print":
                    {
                        Print();
                        Query();
                        break;
                    }
                case "Print1":
                    {
                        Print1();
                        Query();
                        break;
                    }
                case "ChooseExcel":
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = @"C:\桌面\";  //指定打开文件默认路径
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
                        break;
                    }
                case "BatchUpload":
                    {

                        //this.ClearData();
                        if (this.txtYBWJ.Text == "")
                        {
                            MessageBox.Show("请选择要导入的预报信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        this.ReadAndSaveExcelInfo();
                        Query();
                        this.txtYBWJ.Text = "";
                        break;
                    }
                default:
                    break;
            }
        }

        private void ReadAndSaveExcelInfo()
        {
            int num = 0;

            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
            Excel.Range range = worksheet.get_Range("A1", "P65535");//A2、O65535表示到Excel从第A列的第2行到第O列的第65535-1行  A2、P65535表示到Excel从第A列的第2行到第P列的第65535-1行 
            System.Array values = (System.Array)range.Formula;
            num = values.GetLength(0);

            if (values.GetValue(1, 1).ToString().Trim() != "车号" && values.GetValue(1, 2).ToString().Trim() != "物料名称" && values.GetValue(1, 3).ToString().Trim() != "发货单位")
            {
                MessageBox.Show("预报选择错误，请重新选择Excel文件: '汽车衡预报模板'！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = 2; i <= num; i++)
            {
                try
                {
                    if (values.GetValue(i, 1).ToString().Trim() == "" && values.GetValue(i, 2).ToString().Trim() == "")
                    {
                        break;
                    }
                    else
                    {
                        string strWeightNo = Guid.NewGuid().ToString();//计量编号
                        string strCarNo = values.GetValue(i, 1).ToString().Trim();//车号

                        string strMaterialName = values.GetValue(i, 2).ToString().Trim();//物料名称
                        string strMaterialCode = string.Empty;

                        DataRow[] drs = null;

                        drs = this.m_MaterialTable.Select("FS_MATERIALNAME ='" + strMaterialName + "'");

                        if (drs.Length > 0)
                        {
                            strMaterialCode = drs[0]["FS_WL"].ToString();
                        }
                        else
                        {
                            strMaterialCode = strMaterialName;
                        }

                        string strSender = values.GetValue(i, 3).ToString().Trim();//发货单位
                        string strSenderCode = string.Empty;
                        drs = null;
                        drs = this.m_ProviderTable.Select("FS_SUPPLIERNAME ='" + strSender + "'");
                        if (drs.Length > 0)
                        {
                            strSenderCode = drs[0]["FS_GY"].ToString();
                        }
                        else
                        {
                            strSenderCode = strSender;
                        }


                        string strReceiver = values.GetValue(i, 4).ToString().Trim();//发货单位
                        string strReceiverCode = string.Empty;
                        drs = null;
                        drs = this.m_ReveiverTable.Select("FS_MEMO ='" + strReceiver + "'");
                        if (drs.Length > 0)
                        {
                            strReceiverCode = drs[0]["FS_SH"].ToString();
                        }
                        else
                        {
                            strReceiverCode = strReceiver;
                        }

                        string strTrans = values.GetValue(i, 5).ToString().Trim();//收货单位
                        string strTransCode = string.Empty;
                        drs = null;
                        drs = this.m_TransTable.Select("FS_TRANSNAME ='" + strTrans + "'");
                        if (drs.Length > 0)
                        {
                            strTransCode = drs[0]["FS_CY"].ToString();
                        }
                        else
                        {
                            strTransCode = strTrans;
                        }

                        string strFlow = values.GetValue(i, 6).ToString().Trim();//流向
                        string strFlowCode = string.Empty;
                        drs = null;
                        drs = this.m_FlowTable.Select("FS_TYPENAME ='" + strFlow + "'");
                        if (drs.Length > 0)
                        {
                            strFlowCode = drs[0]["FS_TYPECODE"].ToString();
                        }
                        else
                        {
                            strFlowCode = strFlow;
                        }

                        string strGrossPoint = values.GetValue(i, 7).ToString().Trim();//毛重计量点名称
                        string strGrossPerson = values.GetValue(i, 8).ToString().Trim();//毛重计量员
                        string strGrossTime = values.GetValue(i, 9).ToString().Trim();//毛重计量时间
                        string strGrossWeight = values.GetValue(i, 10).ToString().Trim();//毛重

                        string strTarePoint = values.GetValue(i, 11).ToString().Trim();//皮重计量点名称
                        string strTarePerson = values.GetValue(i, 12).ToString().Trim();//皮重计量员
                        string strTareTime = values.GetValue(i, 13).ToString().Trim();//皮重计量时间
                        string strTareWeight = values.GetValue(i, 14).ToString().Trim();//皮重

                        string strYKL = values.GetValue(i, 15).ToString().Trim();//应扣量

                        double inGrossWeight = Math.Round(Convert.ToDouble(strGrossWeight.Trim()), 3);
                        double inTareWeight = Math.Round(Convert.ToDouble(strTareWeight.Trim()), 3);
                        double inNetWeight = Math.Round((inGrossWeight - inTareWeight), 3);

                        double inKHJZ = 0;
                        double inYKL = 0;

                        if (strYKL != "" && strYKL != "0")
                        {
                            inYKL = Convert.ToDouble(strYKL);
                            inKHJZ = Math.Round((inNetWeight - inYKL), 3);
                        }
                        else
                        {
                            inKHJZ = inNetWeight;
                        }


                        double _sendgrossweight = 0;
                        //if (values.GetValue(i, 16).ToString().Trim() != string.Empty)
                        //{
                        //    _sendgrossweight=Math.Round(Convert.ToDouble(values.GetValue(i, 16).ToString().Trim()), 3);
                        //}

                        double _sendtareweight = 0;
                        //if (values.GetValue(i, 17).ToString().Trim() != string.Empty)
                        //{
                        //    _sendtareweight = Math.Round(Convert.ToDouble(values.GetValue(i, 17).ToString().Trim()), 3);
                        //}

                        double _sendnetweight = 0;
                        //if (values.GetValue(i, 18).ToString().Trim() != string.Empty)
                        //{
                        //    _sendnetweight = Math.Round(Convert.ToDouble(values.GetValue(i, 18).ToString().Trim()), 3);
                        //}

                        string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " Excel导入";

                        string strBZ = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup();
                        string strGrossShift = "";
                        if (string.Compare(strGrossTime.Substring(11, 8), "00:00:00") >= 0 && string.Compare(strGrossTime.Substring(11, 8), "08:00:00") < 0)
                            strGrossShift = "3";
                        if (string.Compare(strGrossTime.Substring(11, 8), "08:00:00") >= 0 && string.Compare(strGrossTime.Substring(11, 8), "16:00:00") < 0)
                            strGrossShift = "1";
                        if (string.Compare(strGrossTime.Substring(11, 8), "16:00:00") >= 0 && string.Compare(strGrossTime.Substring(11, 8), "24:00:00") < 0)
                            strGrossShift = "2";

                        string strTareShift = "";
                        if (string.Compare(strTareTime.Substring(11, 8), "00:00:00") >= 0 && string.Compare(strTareTime.Substring(11, 8), "08:00:00") < 0)
                            strTareShift = "3";
                        if (string.Compare(strTareTime.Substring(11, 8), "08:00:00") >= 0 && string.Compare(strTareTime.Substring(11, 8), "16:00:00") < 0)
                            strTareShift = "1";
                        if (string.Compare(strTareTime.Substring(11, 8), "16:00:00") >= 0 && string.Compare(strTareTime.Substring(11, 8), "24:00:00") < 0)
                            strTareShift = "2";

                        string strDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        string strIP = m_BaseInfo_1.getIPAddress();
                        string strMAC = m_BaseInfo_1.getMACAddress();
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        string sql = string.Empty;
                        sql = "insert into dt_carweight_weight (FS_WEIGHTNO,FS_CARNO,Fs_Material,FS_MATERIALNAME,";//5
                        sql += "FS_SENDER,Fs_Receiver,Fs_Transno,Fs_Weighttype,Fs_GrossPoint,FS_TarePoint,";//7
                        sql += "FN_GrossWeight,FN_TareWeight,FN_NetWeight,FS_GrossPerson,FS_TarePerson,Fd_GrossDatetime,FD_TareDateTime,fs_Memo,FS_GROSSSHIFT,FS_TARESHIFT,FS_KHJZ,FS_BZ,FS_YKL,FN_SENDGROSSWEIGHT,FN_SENDTAREWEIGHT,FN_SENDNETWEIGHT)";
                        sql += " values ('" + strWeightNo + "','" + strCarNo + "','" + strMaterialCode + "','" + strMaterialName + "',";
                        sql += " '" + strSenderCode + "','" + strReceiverCode + "','" + strTransCode + "','" + strFlowCode + "','" + strGrossPoint + "','" + strTarePoint + "',";
                        sql += inGrossWeight + "," + inTareWeight + "," + inNetWeight + ",'" + strGrossPerson + "','" + strTarePerson + "',TO_DATE('" + strGrossTime + "','yyyy-MM-dd HH24:mi:ss'),TO_DATE('" + strTareTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strMemo + "','" + strGrossShift + "','" + strTareShift + "'," + inKHJZ + ",'" + strBZ + "'," + inYKL + "," + _sendgrossweight + "," + _sendtareweight + "," + _sendnetweight + ")";

                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                        ccp.MethodName = "ExcuteNonQuery";
                        ccp.ServerParams = new object[] { sql };
                        ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        if (ccp.ReturnCode == 0)
                        {
                            m_BaseInfo_1.ob = this.ob;
                            m_BaseInfo_1.insertLog(strDate, "增加", p_UPDATER, strIP, strMAC, "", strWeightNo, "", "", strCarNo, "", "DT_CARWEIGHT_WEIGHT", "汽车秤/历史数据补录/Excel数据导入");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void Print()
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请在下列表格中选择要打印的数据");
                return;
            }

            if (cbBF.SelectedValue == null || cbBF.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择打印的磅房");
                return;
            }

            //PrintPreviewDialog dialog = new PrintPreviewDialog();
            //dialog.Document = this.printDocument1;
            //dialog.PrintPreviewControl.AutoZoom = false;
            //dialog.PrintPreviewControl.Zoom = 0.75;
            //dialog.ShowDialog();


            printDocument1.PrinterSettings.PrinterName = this.cbBF.SelectedValue.ToString().Trim();//EPSON BA-T500 Full cut
            System.Drawing.Printing.Margins margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            printDocument1.DefaultPageSettings.Margins = margins;
            printDocument1.Print();

        }


        private void Print1()
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请在下列表格中选择要打印的数据");
                return;
            }

            //if (cbBF.SelectedValue == null || cbBF.SelectedValue.ToString().Trim() == "")
            //{
            //    MessageBox.Show("请选择打印的磅房");
            //    return;
            //}

            //PrintPreviewDialog dialog = new PrintPreviewDialog();
            //dialog.Document = this.printDocument1;
            //dialog.PrintPreviewControl.AutoZoom = false;
            //dialog.PrintPreviewControl.Zoom = 0.75;
            //dialog.ShowDialog();


            printDocument1.PrinterSettings.PrinterName = "JLDT";//EPSON BA-T500 Full cut
            System.Drawing.Printing.Margins margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            printDocument1.DefaultPageSettings.Margins = margins;
            printDocument1.Print();

        }

        //下载流向信息  ,add by luobin 
        private void DownLoadFlow()
        {
            System.Data.DataTable m_FlowTable = new System.Data.DataTable();//流向数据表
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

            cbFlow.DataSource = m_FlowTable;
            cbFlow.DisplayMember = "FS_TYPENAME";
            cbFlow.ValueMember = "FS_TYPECODE";

        }

        private bool Decision()//判断所选时间区间是否大于60天，如果大于则返回false//杨滔添加
        {
            beginTime = BeginTime.Value.Date;
            endTime = EndTime.Value.Date;
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

        private void Query()
        {
            string strBeginTime = BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss')";
            string strEndTIme = EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            string sql = "select A.FS_WEIGHTNO,A.FS_CARDNUMBER,A.FS_CARNO,B.FS_MATERIALNAME,C.FS_SUPPLIERNAME,d.fs_memo as FS_Receiver,";
            sql += "A.Fs_Senderstore,A.FS_RECEIVERSTORE,E.FS_TRANSNAME,F.FS_TYPENAME,G.FS_POINTNAME as FS_GrossPointName,H.FS_PointName As FS_TarePointName,A.FN_GrossWeight,A.FN_TareWeight,A.FN_NetWEIGHT,A.FS_YKL,A.FS_YKBL,A.FS_KHJZ,";
            sql += " A.FS_GrossPerson,A.FS_TarePerson,A.FS_FULLLABELID,A.FS_CONTRACTNO,A.FN_COUNT,A.FS_STOVENO,";
            sql += "to_char(A.Fd_GrossDateTime, 'yyyy-MM-dd HH24:mi:ss') as FD_GrossDateTime,to_char(A.Fd_TareDateTime, 'yyyy-MM-dd HH24:mi:ss') as FD_TareDateTime,A.fs_Memo,A.FS_GrossPoint,A.FS_TarePoint,A.FS_BZ,I.FS_ProviderName,J.FS_ADVISESPEC,J.FS_ZZJY ,";
            sql += " A.FS_REWEIGHTFLAG,to_char(A.FD_REWEIGHTTIME, 'yyyy-MM-dd HH24:mi:ss') as FD_REWEIGHTTIME,A.FS_REWEIGHTPLACE,A.FS_REWEIGHTPERSON,";
            sql += " A.FS_UNLOADPERSON,to_char(A.FD_UNLOADTIME, 'yyyy-MM-dd HH24:mi:ss') AS FD_UNLOADTIME ,A.FS_UNLOADPLACE,A.FS_UNLOADFLAG ";
            sql += " from dt_carweight_weight A,It_Material B,It_Supplier C,It_Store D,BT_TRANS E,Bt_Weighttype F,Bt_Point G,Bt_Point H,It_Provider I,IT_FP_TECHCARD J";
            sql += " where A.Fs_Material = b.fs_wl(+) and A.FS_SENDER = C.FS_GY(+) and A.Fs_Receiver = d.fs_sh(+)";
            sql += " and A.Fs_Transno = E.FS_CY(+) and A.Fs_Weighttype = F.FS_TYPECODE(+) and A.FS_GrossPoint = G.FS_POINTCODE(+) and A.FS_TarePoint = H.FS_PointCode(+) and A.FS_Provider = I.FS_SP(+) and A.FS_STOVENO = J.FS_GP_STOVENO(+)";
            sql += " and A.FD_TareDateTime >= to_date('" + BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
            sql += " and A.FD_TareDateTime <= to_date('" + EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";

            this.dataTable1.Rows.Clear();
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
            UltraGridRow ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null || ugr.Index < 0)
            {
                MessageBox.Show("请先选择要修改的行！");
                return;
            }

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
            double inGrossWeight = Math.Round(Convert.ToDouble(this.tbGrossWeight.Text.Trim()), 3);
            double inTareWeight = Math.Round(Convert.ToDouble(this.tbTareWeight.Text.Trim()), 3);
            double inNetWeight = Math.Round((inGrossWeight - inTareWeight), 3);


            //扣渣
            string strYKL = tbYKL.Text.Trim();
            string strYKBL = tbYKBL.Text.Trim();
            double inKHJZ = 0;
            double inYKL = 0;
            double inYKBL = 0;

            if (strYKL != "" && strYKL != "0")
            {
                inYKL = Convert.ToDouble(strYKL);
                inKHJZ = Math.Round((inNetWeight - inYKL), 3);
            }
            else if (strYKBL != "")
            {
                inYKBL = Convert.ToDouble(strYKBL);
                inKHJZ = Math.Round((inNetWeight - inNetWeight * inYKBL), 3);

            }
            else
            {
                inKHJZ = inNetWeight;
            }

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

            string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " 手工补录";
            string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string strIP = m_BaseInfo_1.getIPAddress();
            string strMAC = m_BaseInfo_1.getMACAddress();
            string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            string m_Memo = "";

            string strContractNo = this.tbContractNo.Text.Trim();
            string strStoveNo = this.tbStoveNo.Text.Trim();

            int inCount = 0;
            if (tbCount.Text.Trim() != "")
                inCount = Convert.ToInt32(tbCount.Text.Trim());
            string str_oldStoveno = ugr.Cells["FS_StoveNo"].Text.Trim();//修改前的炉号
            string strSecondTime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            CoreClientParam ccp = new CoreClientParam();
            string sql = "";

            if (str_oldStoveno != "" || strStoveNo != "")
            {//当是钢坯数据时
                int i_ZZCheckFlag = IsZZCompleted(strStoveNo);
                if (i_ZZCheckFlag == 1)
                {//判断输入或者带出的炉号
                    MessageBox.Show("炉号" + strStoveNo + "钢坯已轧制完成，不能修改，请联系管理员！");
                    return;
                }
                if (i_ZZCheckFlag == 0)
                {//判断输入或者带出的炉号
                    MessageBox.Show("炉号" + strStoveNo + "的钢坯卡片不存在，不能修改，请联系管理员！");
                    return;
                }

                if (!strStoveNo.Equals(str_oldStoveno))
                {//此处有一个漏洞，当修改前炉号也是一炉多车时，将需要再修改老炉号对应的一条数据，才会合乎逻辑要求，否则将会出现找不到坯、无重量等情况
                    if (DialogResult.No == MessageBox.Show("您修改了炉号，继续操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        return;
                    }
                    i_ZZCheckFlag = IsZZCompleted(str_oldStoveno);
                    if (i_ZZCheckFlag == 1)
                    {//判断修改前的炉号
                        MessageBox.Show("炉号" + str_oldStoveno + "钢坯已轧制完成，不能修改，请联系管理员！");
                        return;
                    }
                    string updateoldsql = "";
                    m_Memo = "";
                    if (strReceiver == "SH000099")
                    {
                        updateoldsql = "update IT_FP_TECHCARD set  FN_JJ_WEIGHT ='0',FS_JJ_OPERATOR = '',FD_GPYS_RECEIVEDATE = NULL,"
                        + " FS_GPYS_RECEIVER = '',FN_GPYS_NUMBER = '0',FS_CHECKED= '0',FN_GPYS_WEIGHT='0',FS_GP_Flow='',FN_GP_TOTALCOUNT='0',"
                        + " FS_GP_SOURCE = '',FS_GP_COMPLETEFLAG = '0',FS_TransType = '' where FS_GP_STOVENO = '" + str_oldStoveno + "'";
                        m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=0,FN_GPYS_NUMBER=0"
                            + "，FN_JJ_WEIGHT(钢坯重量)=0,FN_GPYS_WEIGHT=0"
                            + "，FS_GP_Flow=，FS_GP_COMPLETEFLAG=0,FS_CHECKED=0";
                    }
                    else
                    {
                        updateoldsql = "update IT_FP_TECHCARD set  FN_JJ_WEIGHT ='0',FS_JJ_OPERATOR = '',FS_GP_Flow='',FN_GP_TOTALCOUNT='0',"
                        + " FS_GP_SOURCE = '',FS_GP_COMPLETEFLAG = '0',FS_TransType = '' where FS_GP_STOVENO = '" + str_oldStoveno + "'";
                        m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=0，FN_JJ_WEIGHT(钢坯重量)=0，FS_GP_Flow=，FS_GP_COMPLETEFLAG=0";
                    }
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { updateoldsql };
                    ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ccp.ReturnCode == 0)
                    {
                        m_BaseInfo_1.ob = this.ob;
                        m_BaseInfo_1.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, str_oldStoveno, str_oldStoveno, "", "", "", "IT_FP_TECHCARD", "汽车秤/历史数据补录/修改按钮-原炉号置空操作");
                    }
                }
            }

            sql = "update dt_carweight_weight SET FS_CARDNUMBER = '" + strCardNo + "',";
            sql += " FS_CARNO = '" + strCarNo + "',";
            sql += " Fs_Material = '" + strMaterial + "',";
            sql += " FS_SENDER  = '" + strSender + "',";
            sql += " Fs_Receiver  = '" + strReceiver + "',";
            sql += " Fs_Senderstore  = '" + strSenderPlace + "',";
            sql += " FS_RECEIVERSTORE  = '" + strReceiverPlace + "',";
            sql += " Fs_Transno  = '" + strTrans + "',";
            sql += " Fs_Weighttype  = '" + strFlow + "',";
            //sql += " Fs_Poundtype  = '" + m_SelectPointID + "',";
            sql += " FN_GrossWEIGHT  = " + inGrossWeight + ",";
            sql += " FN_TareWEIGHT  = " + inTareWeight + ",";
            sql += " FN_NetWEIGHT  = " + inNetWeight + ",";
            //sql += " FS_KHJZ  = " + inNetWeight + ",";
            sql += " FS_CONTRACTNO  = '" + strContractNo + "',";
            sql += " FS_STOVENO  = '" + strStoveNo + "',";
            sql += " FS_Provider  = '" + strProvider + "',";
            sql += " FS_BZ  = '" + strBZ + "',";
            sql += " FN_COUNT  = " + inCount + ",";
            sql += " FS_YKL  = " + inYKL + ",";
            sql += " FS_YKBL  = " + inYKBL + ",";
            sql += " FS_KHJZ  = " + inKHJZ + ",";
            sql += " fs_Memo  = '" + strMemo + "'";
            sql += " where FS_WEIGHTNO = '" + m_WeightNo + "'";


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
            string strGrossWEIGHTLog = this.ultraGrid3.ActiveRow.Cells["FN_GrossWeight"].Text;
            string strTareWEIGHTLog = this.ultraGrid3.ActiveRow.Cells["FN_TareWeight"].Text;
            string strMemoLog = this.ultraGrid3.ActiveRow.Cells["fs_Memo"].Text;


            string strLog = "卡号：" + strCardNOLog + ">" + strCardNo + ",车号：" + strCarNoLog + ">" + strCarNo + ",物料："//卡号、车号
                + strMaterialLog + ">" + this.cbWLMC.Text + ",发货单位：" + strSENDERLog + ">" + this.cbFHDW.Text + ",收货单位："//物料、发货单位
                + strReceiverLog + ">" + this.cbSHDW.Text + ",装货点：" + strSenderstoreLog + ">" + this.tbSenderPlace.Text + ",卸货点："//收货单位、装货地点
                + strRECEIVERSTORELog + ">" + tbReceiverPlace.Text + ",承运单位：" + strTransnoLog + ">" + this.cbCYDW.Text + ",流向："//卸货地点、承运单位
                + strWeighttypeLog + ">" + this.cbFlow.Text + ",毛重：" + strGrossWEIGHTLog + ">" + inGrossWeight +",皮重："+strTareWEIGHTLog+">"+inTareWeight+ ",备注：" + strMemoLog + ">" + strMemo;//流向、重量

            m_BaseInfo_1.ob = this.ob;
            this.m_BaseInfo_1.WriteOperationLog("DT_CARWEIGHT_WEIGHT", strDepartMent, strUserName, "修改", strLog, "历史计量数据补录", "汽车衡二次数据表", "汽车衡");//调用写操作日志方法



            if (ccp.ReturnCode == 0)
            {
                m_Memo = "";
                string strSql = " select FS_STOVENO,FN_COUNT,FS_CARNO,FS_MATERIAL,FN_NETWEIGHT from DT_CARWEIGHT_WEIGHT where FS_WEIGHTNO = '" + m_WeightNo + "'";
                System.Data.DataTable dts = new System.Data.DataTable();
                dts = m_BaseInfo_1.QueryData(strSql);

                if (strStoveNo != dts.Rows[0]["FS_STOVENO"].ToString().Trim())
                    m_Memo = m_Memo + " FS_STOVENO(冶炼炉号) = " + dts.Rows[0]["FS_STOVENO"].ToString().Trim() + " -> " + strStoveNo + "";

                if (strMaterial != dts.Rows[0]["FS_MATERIAL"].ToString().Trim())
                    m_Memo = m_Memo + " FS_MATERIAL(物料代码) = " + dts.Rows[0]["FS_MATERIAL"].ToString().Trim() + " -> " + strMaterial + "";

                if (strCarNo != dts.Rows[0]["FS_CARNO"].ToString().Trim())
                    m_Memo = m_Memo + " FS_CARNO(车牌号) = " + dts.Rows[0]["FS_CARNO"].ToString().Trim() + " -> " + strCarNo + "";

                if (inNetWeight.ToString() != dts.Rows[0]["FN_NETWEIGHT"].ToString().Trim())
                    m_Memo = m_Memo + " FN_NETWEIGHT(净重) = " + dts.Rows[0]["FN_NETWEIGHT"].ToString().Trim() + " -> " + inNetWeight.ToString() + "";

                if (m_Memo != "")
                    m_BaseInfo_1.ob = this.ob;
                m_BaseInfo_1.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, m_WeightNo, strStoveNo, "", "", "", "DT_CARWEIGHT_WEIGHT", "汽车秤/历史数据补录/修改按钮");

            }
            UpdateFPCardByNewStoveNo(strStoveNo, strReceiver, inNetWeight, inCount, strSecondTime, strSender);//更新新炉号对应的方坯信息
            if (!strStoveNo.Equals(str_oldStoveno))
            {
                UpdateFPCardByOldStoveNo(str_oldStoveno, strSecondTime);//更新旧炉号对应的方坯信息
            }
        }

        private void Add()
        {
            if (IsValid() == false)
                return;

            string strCarNo = this.tbCarNo.Text.Trim();
            string strCardNo = this.tbCardNo.Text.Trim();
            string strSenderPlace = this.tbSenderPlace.Text.Trim();
            string strReceiverPlace = this.tbReceiverPlace.Text.Trim();
            double inGrossWeight = Math.Round(Convert.ToDouble(this.tbGrossWeight.Text.Trim()), 3);
            double inTareWeight = Math.Round(Convert.ToDouble(this.tbTareWeight.Text.Trim()), 3);
            double inNetWeight = Math.Round((inGrossWeight - inTareWeight), 3);

            //扣渣
            string strYKL = tbYKL.Text.Trim();
            string strYKBL = tbYKBL.Text.Trim();
            double inKHJZ = 0;
            double inYKL = 0;
            double inYKBL = 0;

            if (strYKL != "" && strYKL != "0")
            {
                inYKL = Convert.ToDouble(strYKL);
                inKHJZ = Math.Round((inNetWeight - inYKL), 3);
            }
            else if (strYKBL != "")
            {
                inYKBL = Convert.ToDouble(strYKBL);
                inKHJZ = Math.Round((inNetWeight - inNetWeight * inYKBL), 3);

            }
            else
            {
                inKHJZ = inNetWeight;
            }

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


            string strFlow = this.cbFlow.SelectedValue.ToString().Trim();

            string strProvider = "";
            if (this.cbProvider.SelectedValue != null)
                strProvider = this.cbProvider.SelectedValue.ToString();
            else
            {
                if (this.cbProvider.Text.Trim() != "")
                    strProvider = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbProvider.Text.Trim(), "SaveSPDWData");
            }

            string strBZ = this.tbBZ.Text.Trim();

            string strMemo = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + " 手工补录";


            m_WeightNo = Guid.NewGuid().ToString();
            string strGrossPerson = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            string strTarePerson = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            string strGrossTime = dtGrossTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strTareTime = dtTareTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            string strSecondTime = "";
            if (dtTareTime.Value >= dtGrossTime.Value)
                strSecondTime = dtTareTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            else
                strSecondTime = dtGrossTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            string strGrossShift = "";
            if (string.Compare(strGrossTime.Substring(11, 8), "00:00:00") >= 0 && string.Compare(strGrossTime.Substring(11, 8), "08:00:00") < 0)
                strGrossShift = "夜";
            if (string.Compare(strGrossTime.Substring(11, 8), "08:00:00") >= 0 && string.Compare(strGrossTime.Substring(11, 8), "16:00:00") < 0)
                strGrossShift = "早";
            if (string.Compare(strGrossTime.Substring(11, 8), "16:00:00") >= 0 && string.Compare(strGrossTime.Substring(11, 8), "24:00:00") < 0)
                strGrossShift = "中";

            string strTareShift = "";
            if (string.Compare(strTareTime.Substring(11, 8), "00:00:00") >= 0 && string.Compare(strTareTime.Substring(11, 8), "08:00:00") < 0)
                strTareShift = "夜";
            if (string.Compare(strTareTime.Substring(11, 8), "08:00:00") >= 0 && string.Compare(strTareTime.Substring(11, 8), "16:00:00") < 0)
                strTareShift = "早";
            if (string.Compare(strTareTime.Substring(11, 8), "16:00:00") >= 0 && string.Compare(strTareTime.Substring(11, 8), "24:00:00") < 0)
                strTareShift = "中";


            string strBarCode = strTareTime.Substring(0, 4);
            strBarCode += strTareTime.Substring(5, 2);
            strBarCode += strTareTime.Substring(8, 2);
            strBarCode += strTareTime.Substring(11, 2);
            strBarCode += strTareTime.Substring(14, 2);
            strBarCode += strTareTime.Substring(17, 2);
            strBarCode += m_SelectPointID;

            string strContractNo = this.tbContractNo.Text.Trim();
            string strStoveNo = this.tbStoveNo.Text.Trim();
            if (strStoveNo != "")
            {
                int i_ZZCheckFlag = IsZZCompleted(strStoveNo);
                if (i_ZZCheckFlag == 1)
                {//判断输入或者带出的炉号
                    MessageBox.Show("炉号" + strStoveNo + "钢坯已轧制完成，不能新增，请联系管理员！");
                    return;
                }
                if (i_ZZCheckFlag == 0)
                {//判断输入或者带出的炉号
                    MessageBox.Show("炉号" + strStoveNo + "的钢坯卡片不存在，不能新增，请联系管理员！");
                    return;
                }
            }

            CoreClientParam ccp = new CoreClientParam();
            string sql = "";
            if (strStoveNo != "")
            {
                //汽车衡历史计量表中是否存在该炉号
                System.Data.DataTable dt1 = new System.Data.DataTable();
                sql = "select count(1) from dt_carweight_weight where FS_STOVENO = '" + strStoveNo + "'";
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = dt1;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (Convert.ToInt32(dt1.Rows[0][0].ToString()) > 0)
                {
                    if (DialogResult.No == MessageBox.Show("该炉号已存在计量记录，如果不是一炉多车情况，请选择‘否’返回，继续操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
            }

            string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string strIP = m_BaseInfo_1.getIPAddress();
            string strMAC = m_BaseInfo_1.getMACAddress();
            string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            int inCount = 0;
            if (tbCount.Text.Trim() != "")
                inCount = Convert.ToInt32(tbCount.Text.Trim());

            //CoreClientParam ccp = new CoreClientParam();
            sql = "select FS_POINTSTATE from bt_point where FS_POINTCODE = '" + m_SelectPointID + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            System.Data.DataTable dtState = new System.Data.DataTable();
            ccp.SourceDataTable = dtState;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            string strState = dtState.Rows[0][0].ToString().Trim();

            sql = "insert into dt_carweight_weight (FS_WEIGHTNO,FS_CARDNUMBER,FS_CARNO,Fs_Material,";
            sql += "FS_SENDER,Fs_Receiver,Fs_Transno,Fs_Weighttype,Fs_GrossPoint,FS_TarePoint,Fs_Senderstore,";
            sql += "FS_RECEIVERSTORE,FN_GrossWeight,FN_TareWeight,FN_NetWeight,FS_GrossPerson,FS_TarePerson,Fd_GrossDatetime,FD_TareDateTime,fs_Memo,FS_FULLLABELID,FD_ECJLSJ,FS_GROSSSHIFT,FS_TARESHIFT,FS_DATASTATE,FS_CONTRACTNO,FS_STOVENO,FN_COUNT,FS_JZDATE,FS_KHJZ,FS_Provider,FS_BZ,FS_YKL,FS_YKBL)";
            sql += " values ('" + m_WeightNo + "','" + strCardNo + "','" + strCarNo + "','" + strMaterial + "',";
            sql += " '" + strSender + "','" + strReceiver + "','" + strTrans + "','" + strFlow + "','" + m_SelectPointID + "','" + m_SelectPointID + "','" + strSenderPlace + "',";
            sql += " '" + strReceiverPlace + "'," + inGrossWeight + "," + inTareWeight + "," + inNetWeight + ",'" + strGrossPerson + "','" + strTarePerson + "',TO_DATE('" + strGrossTime + "','yyyy-MM-dd HH24:mi:ss'),TO_DATE('" + strTareTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strMemo + "','" + strBarCode + "',TO_DATE('" + strSecondTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strGrossShift + "','" + strTareShift + "','" + strState + "','" + strContractNo + "','" + strStoveNo + "'," + inCount + ",TO_DATE('" + strSecondTime + "','yyyy-MM-dd HH24:mi:ss')," + inKHJZ + ",'" + strProvider + "','" + strBZ + "'," + inYKL + "," + inYKBL + ")";

            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (strStoveNo != "")
                {
                    UpdateFPCardByNewStoveNo(strStoveNo, strReceiver, inNetWeight, inCount, strSecondTime, strSender);
                }
                m_BaseInfo_1.ob = this.ob;
                string strLog = "卡号=" + strCardNo + ",车号=" + strCarNo + ",物料="//卡号、车号
              + cbWLMC.Text + ",发货单位=" + cbFHDW.Text + ",收货单位="//物料、发货单位
              + cbSHDW.Text + ",装货点=" + tbSenderPlace.Text + ",卸货点="//收货单位、装货地点
              + tbReceiverPlace.Text + ",承运单位=" + cbCYDW.Text + ",流向="//卸货地点、承运单位
              + cbFlow.Text + ",毛重=" + this.tbGrossWeight.Text + ",皮重="+tbTareWeight.Text+",备注=" + tbBZ.Text;//流向、重量


                this.m_BaseInfo_1.WriteOperationLog("DT_CARWEIGHT_WEIGHT", strDepartMent, strUserName, "增加", strLog, "历史计量数据补录", "汽车衡二次数据表", "汽车衡");//调用写操作日志方法



                //m_BaseInfo_1.insertLog(strDate, "增加", p_UPDATER, strIP, strMAC, "", m_WeightNo, strStoveNo, "", "", "", "DT_CARWEIGHT_WEIGHT", "汽车秤/历史数据补录/新增按钮");
            }

        }

        private void Delete()
        {
            if (m_WeightNo == "")
            {
                MessageBox.Show("请选择需要删除的数据");
                return;
            }

            UltraGridRow ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null || ugr.Index < 0)
            {
                MessageBox.Show("请先选择要删除的行！");
                return;
            }

            CoreClientParam ccp = new CoreClientParam();
            string sql = "";
            string strStoveNo = ugr.Cells["FS_StoveNo"].Text.Trim();
            if (strStoveNo != "")
            {
                int i_ZZCheckFlag = IsZZCompleted(strStoveNo);
                if (i_ZZCheckFlag == 1)
                {//判断输入或者带出的炉号
                    MessageBox.Show("炉号" + strStoveNo + "钢坯已轧制完成，不能删除，请联系管理员！");
                    return;
                }
            }

            string strSecondTime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string strIP = m_BaseInfo_1.getIPAddress();
            string strMAC = m_BaseInfo_1.getMACAddress();
            string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            sql = "delete dt_carweight_weight where FS_WEIGHTNO = '" + m_WeightNo + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (strStoveNo != "")
                {

                    string strReceiver = "";
                    if (this.cbSHDW.SelectedValue == null || this.cbSHDW.SelectedValue.ToString().Trim() == "")
                        strReceiver = this.m_BaseInfo.InsertBaseData(m_SelectPointID, this.cbSHDW.Text.Trim(), "SaveSHDWData");
                    else
                        strReceiver = this.cbSHDW.SelectedValue.ToString().Trim();

                    //修改工艺流动卡信息
                    string updateoldsql = "";
                    string m_Memo = "";
                    if (strReceiver == "SH000099")
                    {
                        updateoldsql = "update IT_FP_TECHCARD set  FN_JJ_WEIGHT ='0',FS_JJ_OPERATOR = '',FD_GPYS_RECEIVEDATE = NULL,"
                        + " FS_GPYS_RECEIVER = '',FN_GPYS_NUMBER = '0',FS_CHECKED= '0',FN_GPYS_WEIGHT='0',FS_GP_Flow='',FN_GP_TOTALCOUNT='0',"
                        + " FS_GP_SOURCE = '',FS_GP_COMPLETEFLAG = '0',FS_TransType = '' where FS_GP_STOVENO = '" + strStoveNo + "'";
                        m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=0,FN_GPYS_NUMBER=0"
                            + "，FN_JJ_WEIGHT(钢坯重量)=0,FN_GPYS_WEIGHT=0"
                            + "，FS_GP_Flow=，FS_GP_COMPLETEFLAG=0,FS_CHECKED=0";
                    }
                    else
                    {
                        updateoldsql = "update IT_FP_TECHCARD set  FN_JJ_WEIGHT ='0',FS_JJ_OPERATOR = '',FS_GP_Flow='',FN_GP_TOTALCOUNT='0',"
                        + " FS_GP_SOURCE = '',FS_GP_COMPLETEFLAG = '0',FS_TransType = '' where FS_GP_STOVENO = '" + strStoveNo + "'";
                        m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=0，FN_JJ_WEIGHT(钢坯重量)=0，FS_GP_Flow=，FS_GP_COMPLETEFLAG=0";
                    }
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { updateoldsql };
                    ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ccp.ReturnCode == 0)
                    {
                        m_BaseInfo_1.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, strStoveNo, strStoveNo, "", "", "", "IT_FP_TECHCARD", "汽车秤/历史数据补录/删除按钮-置空操作");
                    }
                    UpdateFPCardByOldStoveNo(strStoveNo, strSecondTime);

                }
                string strCardNOLog = this.ultraGrid3.ActiveRow.Cells["FS_CARDNUMBER"].Text;
                string strCarNoLog = this.ultraGrid3.ActiveRow.Cells["FS_CARNO"].Text;
                string strMaterialLog = this.ultraGrid3.ActiveRow.Cells["FS_MATERIALNAME"].Text;
                string strSENDERLog = this.ultraGrid3.ActiveRow.Cells["FS_SUPPLIERNAME"].Text;
                string strReceiverLog = this.ultraGrid3.ActiveRow.Cells["FS_Receiver"].Text;
                string strSenderstoreLog = this.ultraGrid3.ActiveRow.Cells["FS_SENDERSTORE"].Text;
                string strRECEIVERSTORELog = this.ultraGrid3.ActiveRow.Cells["FS_RECEIVERSTORE"].Text;
                string strTransnoLog = this.ultraGrid3.ActiveRow.Cells["FS_TRANSNAME"].Text;
                string strWeighttypeLog = this.ultraGrid3.ActiveRow.Cells["FS_TYPENAME"].Text;
                string strGrossWEIGHTLog = this.ultraGrid3.ActiveRow.Cells["FN_GrossWeight"].Text;
                string strTareWEIGHTLog = this.ultraGrid3.ActiveRow.Cells["FN_TareWeight"].Text;
                string strMemoLog = this.ultraGrid3.ActiveRow.Cells["fs_Memo"].Text;

                m_BaseInfo_1.ob = this.ob;
                string strLog = "卡号=" + strCardNOLog + ",车号=" + strCarNoLog + ",物料="//卡号、车号
              + strMaterialLog + ",发货单位=" + strSENDERLog + ",收货单位="//物料、发货单位
              + strReceiverLog + ",装货点=" + strSenderstoreLog + ",卸货点="//收货单位、装货地点
              + strRECEIVERSTORELog + ",承运单位=" + strTransnoLog + ",流向="//卸货地点、承运单位
              + strWeighttypeLog + ",毛重=" + strGrossWEIGHTLog + ",皮重=" + strTareWEIGHTLog + ",备注=" + tbBZ.Text;//流向、重量


                this.m_BaseInfo_1.WriteOperationLog("DT_CARWEIGHT_WEIGHT", strDepartMent, strUserName, "删除", strLog, "历史计量数据补录", "汽车衡二次数据表", "汽车衡");//调用写操作日志方法


            }

            sql = "delete dt_images where FS_WEIGHTNO = '" + m_WeightNo + "'";
            //CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }


        /// <summary>
        /// 更新新炉号对应的信息，要加上历史表中原重量和支数和,20110301彭海波增加
        /// </summary>
        private void UpdateFPCardByNewStoveNo(string strStoveNo, string strReceiver, Double inNetWeight, int inCount, string strSecondTime, string strSender)
        {
            if (strStoveNo != "")
            {
                double f_OldWeight = 0;
                int i_OldCount = 0;
                string selectoldsql = "select sum(Fn_Netweight) as f_OldWeight,sum(FN_COUNT) AS i_OldCount  from dt_carweight_weight  where  fs_stoveno='" + strStoveNo + "'";
                CoreClientParam selectccp = new CoreClientParam();
                selectccp.ServerName = "ygjzjl.car.StorageInfo";
                selectccp.MethodName = "queryByClientSql";
                selectccp.ServerParams = new object[] { selectoldsql };
                System.Data.DataTable dt_temp = new System.Data.DataTable();
                selectccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {//一炉多车情况
                    string str_OldWeight = dt_temp.Rows[0]["f_OldWeight"].ToString().Trim();
                    string str_OldCount = dt_temp.Rows[0]["i_OldCount"].ToString().Trim();
                    if (str_OldWeight != null && str_OldWeight != "")
                    {
                        f_OldWeight = double.Parse(str_OldWeight);
                    }
                    if (str_OldCount != null && str_OldCount != "")
                    {
                        i_OldCount = int.Parse(str_OldCount);
                    }
                }
                string sql = "";
                string m_Memo = "";
                if (strReceiver == "SH000099")
                {
                    sql = " update IT_FP_TECHCARD set  FN_JJ_WEIGHT =  " + (f_OldWeight) + ",FS_JJ_OPERATOR = '" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "',";
                    sql += " FD_GPYS_RECEIVEDATE = TO_DATE('" + strSecondTime + "','yyyy-MM-dd HH24:mi:ss'),FN_GP_TOTALCOUNT='" + (i_OldCount) + "',";
                    sql += " FS_GPYS_RECEIVER = '" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "',FN_GPYS_NUMBER = " + (i_OldCount) + ",";
                    sql += " FS_CHECKED= '1',FN_GPYS_WEIGHT = " + (f_OldWeight) + ",FS_GP_Flow = '" + strReceiver + "',FS_GP_SOURCE = '" + strSender + "',FS_GP_COMPLETEFLAG = '1',FS_TransType = '2' ";
                    sql += " where FS_GP_STOVENO = '" + strStoveNo + "'";
                    m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=" + (i_OldCount) + ",FN_GPYS_NUMBER=" + (i_OldCount)
                            + "，FN_JJ_WEIGHT(钢坯重量)=" + (f_OldWeight) + ",FN_GPYS_WEIGHT=" + (f_OldWeight)
                            + "，FS_GP_Flow=" + strReceiver + "，FS_GP_COMPLETEFLAG=1,FS_CHECKED=1";
                }
                else
                {
                    sql = " update IT_FP_TECHCARD set  FN_JJ_WEIGHT =  " + (f_OldWeight) + ",FS_JJ_OPERATOR = '" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "',";
                    sql += " FS_GP_Flow = '" + strReceiver + "',FS_GP_SOURCE = '" + strSender + "',FS_GP_COMPLETEFLAG = '1',FS_TransType = '2',FN_GP_TOTALCOUNT='" + (i_OldCount) + "'";
                    sql += " where FS_GP_STOVENO = '" + strStoveNo + "'";
                    m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=" + (i_OldCount) + "，FN_JJ_WEIGHT(钢坯重量)=" + (f_OldWeight)
                            + "，FS_GP_Flow=" + strReceiver + "，FS_GP_COMPLETEFLAG=1";
                }

                selectccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                selectccp.MethodName = "ExcuteNonQuery";
                selectccp.ServerParams = new object[] { sql };
                this.ExecuteNonQuery(selectccp, CoreInvokeType.Internal);
                if (selectccp.ReturnCode == 0)
                {
                    string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                    string strIP = m_BaseInfo_1.getIPAddress();
                    string strMAC = m_BaseInfo_1.getMACAddress();
                    string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    m_BaseInfo_1.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, strStoveNo, strStoveNo, "", "", "", "IT_FP_TECHCARD", "汽车秤/历史数据补录/新炉号赋值操作");

                }
            }

        }


        /// <summary>
        /// 更新旧炉号对应的信息，要加上历史表中原重量和支数和,目的是防止交叉修改及一炉多车会出现的问题,20110301彭海波增加
        /// </summary>
        private void UpdateFPCardByOldStoveNo(string strOldStoveNo, string strOldSecondTime)
        {
            if (strOldStoveNo != "")
            {
                double f_OldWeight = 0;
                int i_OldCount = 0;
                string strOldReceiver = "", strOldSender = "";
                string selectoldsql = "select Fn_Netweight,FN_COUNT,fs_receiver,fs_sender  from dt_carweight_weight  where  fs_stoveno='" + strOldStoveNo + "'";
                CoreClientParam selectccp = new CoreClientParam();
                selectccp.ServerName = "ygjzjl.car.StorageInfo";
                selectccp.MethodName = "queryByClientSql";
                selectccp.ServerParams = new object[] { selectoldsql };
                System.Data.DataTable dt_temp = new System.Data.DataTable();
                selectccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(selectccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {//一炉多车情况
                    for (int i = 0; i < dt_temp.Rows.Count; i++)
                    {
                        string str_OldWeight = dt_temp.Rows[i]["Fn_Netweight"].ToString().Trim();
                        string str_OldCount = dt_temp.Rows[i]["FN_COUNT"].ToString().Trim();
                        if (str_OldWeight != null && str_OldWeight != "")
                        {
                            f_OldWeight += double.Parse(str_OldWeight);
                        }
                        i_OldCount += int.Parse(str_OldCount);
                    }
                    strOldReceiver = dt_temp.Rows[0]["fs_receiver"].ToString().Trim();
                    strOldSender = dt_temp.Rows[0]["fs_sender"].ToString().Trim();
                    string sql = "";
                    //修改工艺流动卡信息
                    string m_Memo = "";
                    if (strOldReceiver == "SH000099")
                    {
                        sql = " update IT_FP_TECHCARD set  FN_JJ_WEIGHT =  " + f_OldWeight + ",FS_JJ_OPERATOR = '" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "',";
                        sql += " FD_GPYS_RECEIVEDATE = TO_DATE('" + strOldSecondTime + "','yyyy-MM-dd HH24:mi:ss'),FN_GP_TOTALCOUNT='" + i_OldCount + "',";
                        sql += " FS_GPYS_RECEIVER = '" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "',FN_GPYS_NUMBER = " + i_OldCount + ",";
                        sql += " FS_CHECKED= '1',FN_GPYS_WEIGHT = " + f_OldWeight + ",FS_GP_Flow = '" + strOldReceiver + "',FS_GP_SOURCE = '" + strOldSender + "',FS_GP_COMPLETEFLAG = '1',FS_TransType = '2' ";
                        sql += " where FS_GP_STOVENO = '" + strOldStoveNo + "'";
                        m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=" + i_OldCount + ",FN_GPYS_NUMBER=" + i_OldCount
                            + "，FN_JJ_WEIGHT(钢坯重量)=" + f_OldWeight + ",FN_GPYS_WEIGHT=" + f_OldWeight
                            + "，FS_GP_Flow=" + strOldReceiver + "，FS_GP_COMPLETEFLAG=1,FS_CHECKED=1";

                    }
                    else
                    {
                        sql = " update IT_FP_TECHCARD set  FN_JJ_WEIGHT =  " + f_OldWeight + ",FS_JJ_OPERATOR = '" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "',";
                        sql += " FS_GP_Flow = '" + strOldReceiver + "',FS_GP_SOURCE = '" + strOldSender + "',FS_GP_COMPLETEFLAG = '1',FS_TransType = '2',FN_GP_TOTALCOUNT='" + i_OldCount + "'";
                        sql += " where FS_GP_STOVENO = '" + strOldStoveNo + "'";
                        m_Memo = "FN_GP_TOTALCOUNT(钢坯条数)=" + i_OldCount + "，FN_JJ_WEIGHT(钢坯重量)=" + f_OldWeight
                            + "，FS_GP_Flow=" + strOldReceiver + "，FS_GP_COMPLETEFLAG=1";
                    }

                    selectccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    selectccp.MethodName = "ExcuteNonQuery";
                    selectccp.ServerParams = new object[] { sql };
                    this.ExecuteNonQuery(selectccp, CoreInvokeType.Internal);
                    if (selectccp.ReturnCode == 0)
                    {
                        string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        string strIP = m_BaseInfo_1.getIPAddress();
                        string strMAC = m_BaseInfo_1.getMACAddress();
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        m_BaseInfo_1.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, strOldStoveNo, strOldStoveNo, "", "", "", "IT_FP_TECHCARD", "汽车秤/历史数据补录/原炉号重查历史表后的赋值操作");

                    }
                }

            }

        }

        /// <summary>
        /// 检查是否已轧材，没找到卡片返回0，已轧制返回1，未轧制返回2,20110301彭海波增加
        /// </summary>
        private int IsZZCompleted(string str_stoveno)
        {
            int int_isqyg = -1;
            try
            {
                string strSql = "SELECT FN_JZ_WEIGHT,FS_ZC_BATCHNO from IT_FP_TECHCARD WHERE FS_GP_STOVENO='" + str_stoveno.Trim() + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.StorageInfo";
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { strSql };
                System.Data.DataTable dt_temp = new System.Data.DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    string str_FN_JZ_WEIGHT = dt_temp.Rows[0]["FN_JZ_WEIGHT"].ToString().Trim();
                    string str_FS_ZC_BATCHNO = dt_temp.Rows[0]["FS_ZC_BATCHNO"].ToString().Trim();
                    if (str_FN_JZ_WEIGHT.Equals("0") || str_FN_JZ_WEIGHT.Equals("") || str_FS_ZC_BATCHNO.Equals(""))
                    {
                        int_isqyg = 2;
                    }
                    else
                    {//当轧材重量和轧制号都有值时，才确认开始轧制或已经轧完
                        int_isqyg = 1;
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





            if (this.cbWLMC.Text == "" || cbWLMC.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择物料！");
                this.cbWLMC.Focus();
                return false;
            }

            if (this.cbFHDW.Text == "" || cbFHDW.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择发货单位！");
                this.cbFHDW.Focus();
                return false;
            }

            if (this.cbSHDW.Text == "" || cbSHDW.SelectedValue.ToString().Trim() == "")
            {
                MessageBox.Show("请选择收货单位！");
                this.cbSHDW.Focus();
                return false;
            }

            if (this.cbCYDW.Text == "" || this.cbCYDW.SelectedValue.ToString().Trim() == "")
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
            m_BaseInfo.GetBFData(this.cbBF, "QC");
            m_BaseInfo_1 = new BaseInfo(this.ob);
            //DownLoadFlow();
            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadTrans();  //下载磅房对应承运单位信息到内存表
            this.DownLoadFlow();
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
            //this.cbBF.Text = ultraGrid3.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            m_SelectPointID = ultraGrid3.ActiveRow.Cells["FS_TarePoint"].Text.Trim();
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

            this.tbGrossWeight.Text = ultraGrid3.ActiveRow.Cells["FN_GrossWEIGHT"].Text.Trim();
            this.tbTareWeight.Text = ultraGrid3.ActiveRow.Cells["FN_TareWEIGHT"].Text.Trim();

            this.tbContractNo.Text = ultraGrid3.ActiveRow.Cells["FS_CONTRACTNO"].Text.Trim();
            this.tbStoveNo.Text = ultraGrid3.ActiveRow.Cells["FS_STOVENO"].Text.Trim();
            this.tbCount.Text = ultraGrid3.ActiveRow.Cells["FN_COUNT"].Text.Trim();
            //this.txtCZH.Text = ultraGrid3.ActiveRow.Cells["Fd_Weighttime"].Text.Trim();

            this.tbBZ.Text = ultraGrid3.ActiveRow.Cells["FS_BZ"].Text.Trim();
            this.cbProvider.Text = ultraGrid3.ActiveRow.Cells["FS_PROVIDERNAME"].Text.Trim();

            this.tbYKL.Text = ultraGrid3.ActiveRow.Cells["FS_YKL"].Text.Trim();
            this.tbYKBL.Text = ultraGrid3.ActiveRow.Cells["FS_YKBL"].Text.Trim();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {


            int oX = 20, oY = 40;  //偏移量
            int xStep = 232;
            int yStep = 33;

            System.Drawing.Font headFont = new System.Drawing.Font("Arial", 14, FontStyle.Bold);
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 9);
            Pen blackPen = new Pen(Color.Black, 2);
            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;

            StringFormat drawFormat2 = new StringFormat();
            drawFormat2.Alignment = StringAlignment.Near;
            drawFormat2.LineAlignment = StringAlignment.Center;

            StringFormat drawFormat3 = new StringFormat();
            drawFormat3.Alignment = StringAlignment.Far;
            drawFormat3.LineAlignment = StringAlignment.Center;

            System.Drawing.Rectangle headRec = new System.Drawing.Rectangle(oX, oY, 286, yStep);
            System.Drawing.Rectangle rec = new System.Drawing.Rectangle(oX, oY, xStep, yStep);

            //Pen pen = new Pen(Color.Black, 10);

            headRec.X = oX / 2;
            headRec.Y = oY / 8;
            e.Graphics.DrawString("玉溪联合企业物资计量单", headFont, Brushes.Black, headRec, drawFormat1);

            //合同号
            rec.Y = oY;
            rec.Width = 300; //设置控件宽度
            //rec.Width = xStep;
            e.Graphics.DrawString("合同号: " + ultraGrid3.ActiveRow.Cells["FS_CONTRACTNO"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);



            //发货单位
            rec.Y = oY + 1 * yStep;
            //rec.Width = 300;
            e.Graphics.DrawString("发货单位: " + ultraGrid3.ActiveRow.Cells["FS_SUPPLIERNAME"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);

            //收货单位
            rec.Y = oY + 2 * yStep;
            e.Graphics.DrawString("收货单位: " + ultraGrid3.ActiveRow.Cells["FS_Receiver"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);

            //物资名称           
            rec.Width = 239; //物料名称太长了换行
            rec.Y = oY + 3 * yStep;
            e.Graphics.DrawString("物资名称: " + ultraGrid3.ActiveRow.Cells["FS_MATERIALNAME"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);
            rec.Width = 300; //物料名称太长了换行后还原

            //承运单位
            rec.Y = oY + 4 * yStep;
            e.Graphics.DrawString("承运单位: " + ultraGrid3.ActiveRow.Cells["FS_TRANSNAME"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);


            if (tbStoveNo.Text.Trim() != "")
            {
                //车号
                rec.Y = oY + 7 * yStep;
                rec.Width = xStep; //设置控件宽度
                e.Graphics.DrawString("车号: " + ultraGrid3.ActiveRow.Cells["FS_CarNo"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);
            }
            else
            {
                //车号
                rec.Y = oY + 5 * yStep;
                rec.Width = xStep; //设置控件宽度
                e.Graphics.DrawString("车号: " + ultraGrid3.ActiveRow.Cells["FS_CarNo"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);
            }

            if (tbStoveNo.Text.Trim() != "")
            {
                //炉号
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("炉号: " + ultraGrid3.ActiveRow.Cells["FS_StoveNo"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);

                //轧制建议
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("轧制建议: " + ultraGrid3.ActiveRow.Cells["FS_ZZJY"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat3);

                //支数
                //rec.Y = oY + 5 * yStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("支(块)数: " + ultraGrid3.ActiveRow.Cells["FN_Count"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);

                //建议轧制规格
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("建议轧制规格: " + ultraGrid3.ActiveRow.Cells["FS_ADVISESPEC"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat3);

                //日期
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("日期: " + ultraGrid3.ActiveRow.Cells["FD_TAREDateTime"].Text.Trim().Substring(0, 10), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("时间: " + ultraGrid3.ActiveRow.Cells["FD_TAREDateTime"].Text.Trim().Substring(11, 8), drawFont, Brushes.Black, rec, drawFormat2);

                //毛重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 7 * yStep;
                e.Graphics.DrawString("毛重: " + ultraGrid3.ActiveRow.Cells["FN_GrossWEIGHT"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //皮重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("皮重: " + ultraGrid3.ActiveRow.Cells["FN_TareWEIGHT"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //净重
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("净重: " + ultraGrid3.ActiveRow.Cells["FN_NetWEIGHT"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //计量点
                rec.Y = oY + 10 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + ultraGrid3.ActiveRow.Cells["FS_TAREPOINTNAME"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 11 * yStep;
                e.Graphics.DrawString("编号: " + ultraGrid3.ActiveRow.Cells["FS_FULLLABELID"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);


                //备注
                rec.Y = oY + 12 * yStep;
                yStep = 36;
                e.Graphics.DrawString("备注: " + ultraGrid3.ActiveRow.Cells["FS_BZ"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);



                //打印条码
                Code128 c128 = new Code128();
                //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                c128.printCode(ultraGrid3.ActiveRow.Cells["FS_FULLLABELID"].Text.Trim(), 320, 80, 3, e);

                //注意
                rec.Y = oY + 14 * yStep;
                //yStep = 66;前面备注那已经赋值了
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
                e.HasMorePages = false;
            }
            else
            {

                //日期
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("日期: " + ultraGrid3.ActiveRow.Cells["FD_TAREDateTime"].Text.Trim().Substring(0, 10), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 7 * yStep;
                e.Graphics.DrawString("时间: " + ultraGrid3.ActiveRow.Cells["FD_TAREDateTime"].Text.Trim().Substring(11, 8), drawFont, Brushes.Black, rec, drawFormat2);

                //毛重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("毛重: " + ultraGrid3.ActiveRow.Cells["FN_GrossWEIGHT"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //皮重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("皮重: " + ultraGrid3.ActiveRow.Cells["FN_TareWEIGHT"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);


                bool isYKL = false;
                if (tbYKL.Text.Trim() != "" && tbYKL.Text.Trim() != "0")
                    isYKL = true;

                bool isYKBL = false;
                if (tbYKBL.Text.Trim() != "" && tbYKBL.Text.Trim() != "0")
                    isYKBL = true;


                if ((!isYKL) && (!isYKBL))
                {
                    //净重
                    rec.Y = oY + 7 * yStep;
                    e.Graphics.DrawString("净重: " + ultraGrid3.ActiveRow.Cells["FN_NetWEIGHT"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);
                }
                else
                {
                    if (isYKL)
                    {
                        //扣渣
                        rec.Y = oY + 7 * yStep;
                        e.Graphics.DrawString("扣渣量: " + ultraGrid3.ActiveRow.Cells["FS_YKL"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);
                    }
                    else
                    {
                        //扣渣
                        rec.Y = oY + 7 * yStep;
                        e.Graphics.DrawString("扣渣比例: " + ultraGrid3.ActiveRow.Cells["FS_YKBL"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat3);
                    }


                    rec.Y = oY + 8 * yStep;
                    e.Graphics.DrawString("净重(扣后): " + ultraGrid3.ActiveRow.Cells["FS_KHJZ"].Text.Trim() + " t", drawFont, Brushes.Black, rec, drawFormat3);

                }




                //计量点
                rec.Y = oY + 8 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + ultraGrid3.ActiveRow.Cells["FS_TAREPOINTNAME"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("编号: " + ultraGrid3.ActiveRow.Cells["FS_FULLLABELID"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat2);


                //备注
                rec.Y = oY + 10 * yStep;
                yStep = 36;
                e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);

                //打印条码
                Code128 c128 = new Code128();
                //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                c128.printCode(ultraGrid3.ActiveRow.Cells["FS_FULLLABELID"].Text.Trim(), 320, 80, 0, e);

                //注意
                rec.Y = oY + 12 * yStep;

                //e.Graphics.DrawString("注意：本凭证请妥善保管遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（2992161）", drawFont, Brushes.Black, rec, drawFormat2);
                e.HasMorePages = false;
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





        //发货单位下拉框拼音助记码选择
        private void cbFHDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbFHDW.Text.Trim().Length == 0 || this.cbFHDW.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
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
