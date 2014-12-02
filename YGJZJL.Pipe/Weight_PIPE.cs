using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using SDK_Com;
//using SerialCommlib;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using Core.Sip.Client.App;
using Core.Sip.Client.Meas;
using System.Drawing.Printing;
using YGJZJL.Pipe.std;

namespace YGJZJL.Pipe
{
    /**
     * 思路：
     * 1、
     * 2、
     * 3、
     */
    public partial class Weight_PIPE : FrmBase
    {
        #region private member variable
        
        //校秤：
        public delegate void CorrentionPicture();//校秤抓图委托
        private CorrentionPicture m_MainThreadCorrentionPicture;//建立校秤委托变量
        BaseInfo baseinfo = new BaseInfo();
        private string correntionWeight = "";
        private string correntionWeightNo = "";
        private string pointId = "";

        private BT_POINT[] m_Points;//计量点数组
        private CoreApp _measApp;//计量点
        private bool _isTalk = false;//是否正在对讲
        private int _talkId = -1;
        private bool _saved = false;

        private string p_shiftdate = "";
        private string _printType = "";//打牌类型
        private string _theoryWeight = "";//理重

        private int m_nPointCount;//计量点个数
        private bool m_bRunningForPoundRoom;//计量点线程运行开关
        private bool m_bPoundRoomThreadClosed;//计量点线程关闭开关
        private System.Threading.Thread m_hThreadForPoundRoom;//计量点线程句柄

        private int m_iSelectedPound = -1;//选择的计量点索引，用于计量点切换时RecordClose等方法使用

        public delegate void CapPicture();//抓图委托
        private CapPicture m_MainThreadCapPicture;//建立委托变量
        //private bool m_bGraspImageSign = false;//抓图开关
        BaseInfo b = new BaseInfo();
        int k;  //硬盘录像机视频调节参数，是具体调节哪个视频
        int BigChannel = 0;  //放大图片句柄
        int m_CurSelBigChannel = -1;//当前放大的是哪一个通道

        public delegate void CloseLight();//清空重量委托
        private CloseLight m_MainThreadCloseLight;//建立委托变量

        private string stRunPath = "";
        YGJZJL.PublicComponent.GetBaseInfo m_GetBaseInfo;
        private string m_szCurUser = "";//当前用户（计量员）
        private string m_szCurBC = "";//当前班次
        private string m_szCurBZ = "";//当前班组
        
        private string PointID = "";    //计量点代码
        private string WeightNo = "";  //计量产生的GUID

        public delegate void AlarmVoice();//播放音频
        private AlarmVoice m_MainThreadAlarmVoice;//建立委托变量

        //查询重量信息变量定义
        public delegate void QueryAndBindWeightDataDelegate();
        private QueryAndBindWeightDataDelegate m_QueryAndBindWeightDataDelegate = null;
        private bool m_bQueryWeightDataOver = true;//查询重量信息回调完成标识
        private string m_szCurBatchNo = ""; //当前计量批次号
        private DataTable m_WeightDataTable = new DataTable();//计量信息内存表，存储计量信息

        //预报信息
        //private DataTable m_PlanDataTable = new DataTable();//预报信息内存表，存储预报信息
        
        private DataTable m_MaterialTable = new DataTable();//物料数据表
        public DataTable tempMaterial = new DataTable();  //物料临时表；用于磅房筛选和助记码筛选
        private DataTable m_SteelTypeTable = new DataTable();//钢种数据表
        private DataTable tempSteelType = new DataTable();
        private DataTable m_SpecTable = new DataTable();//规格数据表
        private DataTable tempSpec = new DataTable();
        private DataTable m_SenderTable = new DataTable();//发货单位数据表
        public DataTable tempSender = new DataTable();
        private DataTable m_RecevierTable = new DataTable();//收货单位数据表
        public DataTable tempReveiver = new DataTable();
        private DataTable m_FlowTable = new DataTable();//流向数据表
        public DataTable tempFlow = new DataTable();

        private System.Windows.Forms.ListBox m_List = new System.Windows.Forms.ListBox(); //下拉列表框
        private string m_ListType = "";  //下拉列表框类型 

        public string m_AlarmVoicePath = ""; //声音文件路径

        private string m_DefaultFlow = "生产订单收货";
        private string m_DefaultSender = "";
        private string m_DefaultReceiver = "销售公司";
        private string m_DefaultStandard = "";
        private string m_DefaultMeterial = "";
        private string m_DefaultPM = string.Empty;

        private string _strBatchNo = ""; //本次计量的批次号 用于图片数据叠加
        private string _strBandNo = "";//本地计量的吊号 用于图片数据叠加
        
        bool m_Flag = true;

        System.Threading.Thread m_hDataUpThread;
        bool m_hRunning = false;

        string strStoreCode = "SH000098"; //作业区代码，用于预报获取分类

        private DataTable dt_TheoryWeight; //棒材理论重量和理论单捆支数
        private bool bCompleteStove = false;

        private WeightStdManage _weightStdManage = null;
        private ProdutionWeightStd _produtionWeightStd = null;

        MessageForm messageForm = new MessageForm();
        private string m_preFlag = string.Empty;

        string strShift = "";
        string strTerm = "";
        string strShiftDate = "";
        string strOperator = "";
       // private DepotManage _depotManage = null;//垛帐系统数据上传

        #endregion

        public Weight_PIPE()
        {
            m_iSelectedPound = -1;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //初始化计量点对象
            _measApp = new CoreApp();
        }

        #region 基础数据处理

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

            tempMaterial = m_MaterialTable.Clone();

            //磅房对应收货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("FS_Receiver".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("fs_memo".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_RecevierTable.Columns.Add(dc);

            tempReveiver = m_RecevierTable.Clone();

            //磅房对应发货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_SENDER".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_MEMO".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_SenderTable.Columns.Add(dc);

            tempSender = m_SenderTable.Clone();

            //磅房对应钢种表
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_SteelTypeTable.Columns.Add(dc);
            dc = new DataColumn("FS_STEELTYPE".ToUpper()); m_SteelTypeTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_SteelTypeTable.Columns.Add(dc);

            //磅房对应规格表
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_SpecTable.Columns.Add(dc);
            dc = new DataColumn("FS_SPEC".ToUpper()); m_SpecTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_SpecTable.Columns.Add(dc);

            //流向表 FS_TYPECODE, FS_TYPENAME
            dc = new DataColumn("FS_TYPECODE".ToUpper()); m_FlowTable.Columns.Add(dc);
            dc = new DataColumn("FS_TYPENAME".ToUpper()); m_FlowTable.Columns.Add(dc);
        }

        //下载磅房对应物料基础信息  ,add by luobin 
        private void DownLoadMaterial()
        {
            string strSql = "select A.FS_PointNo, A.FS_MATERIALNO, b.fs_materialname, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_Pointmaterial A, It_Material B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.Fs_Materialno = B.Fs_Wl and C.Fs_Pointtype = 'GC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_MaterialTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对收货单位信息  ,add by luobin 
        private void DownLoadReceiver()
        {
            string strSql = "select A.FS_PointNo, A.FS_Receiver, b.fs_memo, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointReceiver A, It_Store B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_Receiver = B.Fs_SH and C.Fs_Pointtype = 'GC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_RecevierTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对发货单位信息  ,add by luobin 
        private void DownLoadSender()
        {
            string strSql = "select A.FS_PointNo, A.FS_SENDER, B.FS_MEMO, B.FS_HELPCODE, A.fn_times ";
            strSql += " from Bt_PointSender A, IT_MRP B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_SENDER = B.FS_FH and C.Fs_Pointtype = 'GC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SenderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }


        //下载磅房对钢种信息  ,add by luobin 
        private void DownLoadSteelType()
        {
            string strSql = "SELECT A.FS_POINTNO,A.FS_STEELTYPE,A.FN_TIMES FROM BT_POINTSTEELTYPE A,BT_POINT B WHERE A.FS_POINTNO = B.FS_POINTCODE AND B.FS_POINTTYPE = 'GC' ORDER BY FN_TIMES DESC";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SteelTypeTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对应规格信息  ,add by luobin 
        private void DownLoadSepc()
        {
            string strSql = "SELECT A.FS_POINTNO,A.FS_SPEC,A.FN_TIMES FROM BT_POINTSPEC A,BT_POINT B WHERE A.FS_POINTNO = B.FS_POINTCODE AND B.FS_POINTTYPE = 'GC' ORDER BY FN_TIMES DESC";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
            ccp.MethodName = "QueryTableData";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SpecTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }

        //下载流向信息  ,add by luobin 
        private void DownLoadFlow()
        {
            string strSql = "select FS_TYPECODE, FS_TYPENAME From BT_WEIGHTTYPE order by FS_TYPECODE ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
            ccp.MethodName = "QueryTableData";
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

            this.tempReveiver = this.m_RecevierTable.Clone();

            drs = this.m_RecevierTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

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
            this.cbFHDW.DisplayMember = "FS_MEMO";
            this.cbFHDW.ValueMember = "FS_SENDER";
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
            this.cbGZ.DataSource = this.tempSteelType;
            cbGZ.DisplayMember = "FS_STEELTYPE";
            cbGZ.ValueMember = "FS_STEELTYPE";

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
            this.cbGG.DataSource = this.tempSpec;
            cbGG.DisplayMember = "FS_SPEC";
            //cbGG.ValueMember = "FS_SPEC";

        }

        private void InitConfig()
        {
            //物料下拉框
            groupBox4.Controls.Add(m_List);
            m_List.Size = new Size(175, 80);
            m_List.Visible = false;
            m_List.ScrollAlwaysVisible = true;
            m_List.BringToFront();
            m_List.Click += new EventHandler(m_List_Click);
            m_List.KeyPress += new KeyPressEventHandler(m_List_KeyPress);
            m_List.Leave += new EventHandler(m_List_Leave);

            cbWLMC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cbWLMC.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbGZ.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cbGZ.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbGG.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cbGG.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbFHDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cbFHDW.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbSHDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cbSHDW.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbLX.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            cbLX.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void m_List_Click(object sender, EventArgs e)   //双击选中智能输入列表中的内容
        {
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
                case "SteelType":
                    this.cbGZ.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbGZ.Focus();
                    m_List.Visible = false;
                    break;
                case "Spec":
                    this.cbGG.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbGG.Focus();
                    m_List.Visible = false;
                    break;
                case "Flow":
                    this.cbLX.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbLX.Focus();
                    m_List.Visible = false;
                    break;
                default:
                    m_List.Visible = false;
                    break;
            }
        }

        void m_List_Leave(object sender, EventArgs e)
        {
            m_List.Hide();
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
                case "SteelType":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbGZ.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbGZ.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbGZ.Focus();
                        text = cbGZ.Text + e.KeyChar;
                        cbGZ.Text = text;
                        cbGZ.SelectionStart = cbGZ.Text.Length;
                    }
                    break;
                case "Spec":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbGG.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbGG.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbGG.Focus();
                        text = cbGG.Text + e.KeyChar;
                        cbGG.Text = text;
                        cbGG.SelectionStart = cbGG.Text.Length;
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 全角半角输入转换
        /// </summary>
        /// <param name="sender"></param>
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

        
        /// <summary>
        ///  全角字符从的unicode编码从65281~65374
        ///  半角字符从的unicode编码从33~126
        ///  差值65248
        ///  空格比较特殊,全角为       12288,半角为       32 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="isChange"></param>
        /// <returns></returns>
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
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbWLMC_TextChanged(object sender, EventArgs e)
        {
            if (this.cbWLMC.Text.Trim().Length == 0 || this.cbWLMC.Text.Trim() == "System.Data.DataRowView")
                return;


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
            m_List.Location = new System.Drawing.Point(72, 96);

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

        private void cbFHDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbFHDW.Text.Trim().Length == 0 || this.cbFHDW.Text.Trim() == "System.Data.DataRowView")
                return;


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
            m_List.Location = new System.Drawing.Point(72, 126);

            string text = this.cbFHDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempSender.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_MEMO"].ToString());
            }
            m_List.Visible = true;
        }

        private void cbSHDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbSHDW.Text.Trim().Length == 0 || this.cbSHDW.Text.Trim() == "System.Data.DataRowView")
                return;


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
            m_List.Location = new System.Drawing.Point(353, 126);

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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down && m_List.Visible == true && m_List.Items.Count != 0 &&
                (cbWLMC.Focused == true || cbGZ.Focused == true || cbGG.Focused == true || cbFHDW.Focused == true || cbSHDW.Focused == true))
            {
                m_List.SetSelected(0, true);
                m_List.Focus();
                return true;
            }
            if (keyData == Keys.Up &&
                (cbWLMC.Focused == true || cbGZ.Focused == true || cbGG.Focused == true || cbFHDW.Focused == true || cbSHDW.Focused == true))
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

        #endregion

        #region methods

        /// <summary>
        /// 明细上传垛帐系统 alter by tt 2010-08-31 判断是否已经上传的条件改为批次号,获取未传记录的条件增加作业区分类
        /// </summary>
        private void DoDataUp()
        {
            int reVal = 0;
            System.Threading.Thread.Sleep(2000);
            while (m_hRunning)
            {
                try
                {
                    string strSql = "select b.fs_batchno || lpad(b.fn_bandno, 2, '0') as batchno,b.fd_datetime,e.fs_material as fs_sapcode,b.fn_weight,'8000' as p_plant,b.fs_labelid,b.fn_theoryweight,b.fs_weightno from dt_productweightmain a,dt_productweightdetail b, IT_PRODUCTDETAIL e where a.fs_batchno = b.fs_batchno and a.FS_PRODUCTNO = e.FS_PRODUCTNO(+) and (b.fs_upflag = '0' or b.fs_upflag is null) and rownum < 10 and b.fs_batchno like '2%'";
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteQuery";
                    ccp.ServerParams = new object[] { strSql };
                    DataTable dt_temp = new DataTable();
                    ccp.SourceDataTable = dt_temp;

                    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                    if (dt_temp.Rows.Count > 0)
                    {
                        try
                        {
                            System.Data.SqlClient.SqlConnection m_SqlConnection = new System.Data.SqlClient.SqlConnection("Data Source=10.1.1.11;Initial Catalog=L3_DepotMngr;User ID=sa;Password =prodepote");
                            m_SqlConnection.Open();
                            for (int i = 0; i < dt_temp.Rows.Count; i++)
                            {
                                //如果存在，先删除***********************************
                                strSql = "select * from ZTJ_THEORYTOTRUE where id = '" + dt_temp.Rows[i]["fs_weightno"].ToString() + "'";
                                System.Data.SqlClient.SqlDataAdapter sda = new SqlDataAdapter(strSql,m_SqlConnection);
                                DataSet ds = new DataSet();
                                sda.Fill(ds);
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    strSql = "select QS_FLAG from ZTJ_THEORYTOTRUE where id = '" + dt_temp.Rows[i]["fs_weightno"].ToString() + "'";
                                    System.Data.SqlClient.SqlDataAdapter sdb = new SqlDataAdapter(strSql, m_SqlConnection);
                                    DataSet dsb = new DataSet();
                                    sdb.Fill(dsb);
                                    WriteLog("判定标志:" + dsb.Tables[0].Rows[0][0].ToString().Trim());
                                    if (dsb.Tables[0].Rows[0][0].ToString().Trim() == "Y")
                                        strSql = "update ZTJ_THEORYTOTRUE set batch = '" + dt_temp.Rows[i]["batchno"].ToString() + "',PSTING_DATE = '" + dt_temp.Rows[i]["fd_datetime"].ToString() + "',WEIGHT = '" + dt_temp.Rows[i]["fn_weight"].ToString() + "',THEORYWEIGHT = '" + dt_temp.Rows[i]["fn_theoryweight"].ToString() + "' where id = '" + dt_temp.Rows[i]["fs_weightno"].ToString() + "'";
                                    else
                                        strSql = "update ZTJ_THEORYTOTRUE set batch = '" + dt_temp.Rows[i]["batchno"].ToString() + "',PSTING_DATE = '" + dt_temp.Rows[i]["fd_datetime"].ToString() + "',MATERIAL='" + dt_temp.Rows[i]["fs_sapcode"].ToString() + "',WEIGHT = '" + dt_temp.Rows[i]["fn_weight"].ToString() + "',THEORYWEIGHT = '" + dt_temp.Rows[i]["fn_theoryweight"].ToString() + "' where id = '" + dt_temp.Rows[i]["fs_weightno"].ToString() + "'";
                                    
                                    WriteLog("执行语句:" + strSql);
                                    //strSql = "update ZTJ_THEORYTOTRUE set id = '" + dt_temp.Rows[i]["fs_weightno"].ToString() + "',PSTING_DATE = '" + dt_temp.Rows[i]["fd_datetime"].ToString() + "',WEIGHT = '" + dt_temp.Rows[i]["fn_weight"].ToString() + "',THEORYWEIGHT = '" + dt_temp.Rows[i]["fn_theoryweight"].ToString() + "' where batch = '" + dt_temp.Rows[i]["batchno"].ToString() + "'";
                                    System.Data.SqlClient.SqlCommand scc = new SqlCommand(strSql, m_SqlConnection);
                                    reVal = scc.ExecuteNonQuery();
                                    WriteLog("执行结果:" + reVal.ToString().Trim());
                                    if (reVal > 0)
                                    {
                                        strSql = "update dt_productweightdetail set fs_upflag = '1' where fs_weightno = '" + dt_temp.Rows[i]["fs_weightno"].ToString() + "'";
                                        ccp = new CoreClientParam();
                                        ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                                        ccp.MethodName = "ExcuteNonQuery";
                                        ccp.ServerParams = new object[] { strSql };
                                        ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                                    }
                                }
                                else
                                {

                                    string strInsert = "insert into ZTJ_THEORYTOTRUE(BATCH,PSTING_DATE,MATERIAL,WEIGHT,P_Plant,BARCODE,THEORYWEIGHT,ID) VALUES('";
                                    strInsert += dt_temp.Rows[i]["batchno"].ToString() + "','";
                                    strInsert += dt_temp.Rows[i]["fd_datetime"].ToString() + "','";
                                    strInsert += dt_temp.Rows[i]["fs_sapcode"].ToString() + "','";
                                    strInsert += dt_temp.Rows[i]["fn_weight"].ToString() + "','";
                                    strInsert += dt_temp.Rows[i]["p_plant"].ToString() + "','";
                                    strInsert += dt_temp.Rows[i]["fs_labelid"].ToString() + "','";
                                    strInsert += dt_temp.Rows[i]["fn_theoryweight"].ToString() + "','";
                                    strInsert += dt_temp.Rows[i]["fs_weightno"].ToString() + "')";
                                    WriteLog("执行语句:" + strInsert);
                                    System.Data.SqlClient.SqlCommand scd = new SqlCommand(strInsert, m_SqlConnection);
                                    reVal = scd.ExecuteNonQuery();
                                    WriteLog("执行结果:" + reVal.ToString().Trim());
                                    if (reVal > 0)
                                    {

                                        strSql = "update dt_productweightdetail set fs_upflag = '1' where fs_weightno = '" + dt_temp.Rows[i]["fs_weightno"].ToString() + "'";
                                        ccp = new CoreClientParam();
                                        ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                                        ccp.MethodName = "ExcuteNonQuery";
                                        ccp.ServerParams = new object[] { strSql };
                                        ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                                    }
                                }
                            }
                            m_SqlConnection.Close();
                        }
                        catch (Exception exp)
                        {
                            WriteLog(exp.Message + "-1");
                        }
                    }
                }
                catch (Exception exp)
                {
                    WriteLog(exp.Message + "-2");
                }
                System.Threading.Thread.Sleep(2000);
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

        private void BeginPoundRoomThread()
        {
            try
            {
                m_bRunningForPoundRoom = true;
                m_bPoundRoomThreadClosed = false;
                m_hThreadForPoundRoom = new System.Threading.Thread(new System.Threading.ThreadStart(PoundRoomThread));
                m_hThreadForPoundRoom.Start();
            }
            catch (Exception exp)
            {
                WriteLog(exp.Message);
            }
        }

        /// <summary>
        /// 磅房数据处理线程
        /// </summary>
        private void PoundRoomThread()
        {
            while (m_bRunningForPoundRoom)
            {
                for (int i = 0; i < m_nPointCount; i++)
                {
                    //使线程结束条件触发时能及时退出
                    if (m_bRunningForPoundRoom == false)
                    {
                        m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
                        return;
                    }

                    //使线程结束条件触发时能及时退出
                    if (m_bRunningForPoundRoom == false)
                    {
                        m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
                        return;
                    }

                    //使线程结束条件触发时能及时退出
                    if (m_bRunningForPoundRoom == false)
                    {
                        m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
                        return;
                    }
                    //分析收到的仪表采集数据strMeterData
                    //如果重量大于一定的值（这个值应该维护到计量点表里去），则表示来车，
                    //第一次则置红灯亮---PoundRoom来完成，避免线程阻塞
                }

                System.Threading.Thread.Sleep(200);
            }

            m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
        }


        /// <summary>
        /// 打开计量点的硬盘录像机
        /// </summary>
        /// <param name="iPoundRoom">计量点索引</param>
        private void RecordOpen(int iPoundRoom)
        {
            int i = iPoundRoom;

            //if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            //{
            //    return;
            //}

            //    if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //    {
            //        return;
            //    }

            if (_measApp == null)
            {
                _measApp = new CoreApp();
            }
            _measApp.Params = m_Points[iPoundRoom];
            _measApp.Params.FS_PRINTERNAME = new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName;
            _measApp.Init();
            _measApp.Weight.WeightChanged += new Core.Sip.Client.Meas.WeightChangedEventHandler(OnWeightChanged);
            _measApp.Weight.WeightCompleted += new Core.Sip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted); 
            //_measApp.VideoChannel[2] = _measApp.Dvr.SDK_RealPlay(3, 0, (int)VideoChannel3.Handle);
            //_measApp.VideoChannel[3] = _measApp.Dvr.SDK_RealPlay(4, 0, (int)VideoChannel4.Handle);
            try { _measApp.Run(); }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //bool ret = _measApp.Cameras[0].RealPlay(VideoChannel1.Handle); 
            //ret = _measApp.Cameras[1].RealPlay(VideoChannel2.Handle);
        }

        /// <summary>
        /// 停止计量点线程
        /// </summary>
        public void StopPoundRoomThread()
        {
            m_bRunningForPoundRoom = false;//停止计量点线程

            //最多等待5秒，让计量点线程自动退出
            for (int nCount = 0; nCount < 50; nCount++)
            {
                if (m_bPoundRoomThreadClosed == true)
                    break;
                System.Threading.Thread.Sleep(100);
            }
        }

        #region <称重处理方法>
        delegate void setText(string str);
        public void OnWeightChanged(object sender, WeightEventArgs e)
        {
            Invoke(new setText(HandleWeightChange), new object[] { e.Value.ToString() });
        }
        private void HandleWeightChange(string weight)
        {
            double d_weight = 0;
            double.TryParse(weight, out d_weight);
                
            if (d_weight <= 0)
            {
                lbWD.Text = "空磅";
                txtZL.Text = "";

                if (_measApp.IoLogik != null)
                {
                    bool[] dostatus = _measApp.IoLogik.ReadDO();
                    if (dostatus[0])
                    {
                        _measApp.IoLogik.DO[0] = false;
                        _measApp.IoLogik.WriteDO();
                    }
                }

                //btnBC.Enabled = false;
            }
            else
            {
                if (Math.Abs(double.Parse(txtXSZL.Text)*1000 - d_weight) > _measApp.Weight.Precision)
                {
                    lbWD.Text = "不稳定";
                    lbYS.ForeColor = Color.Red;
                    txtZL.Text = "";
                }
                //btnBC.Enabled = false;
            }

            txtXSZL.Text = (d_weight / 1000).ToString();
        }
        private void HandleWeightCompelte(string weight)
        {
            _saved = false;
            // txtXSZL.Text = weight;
            HandleMeterData(weight);

        }
        public void OnWeightCompleted(object sender, WeightEventArgs e)
        {
            //MessageBox.Show("1");
            this.Invoke(new setText(HandleWeightCompelte), new object[] { e.Value.ToString() });
        }

        /// <summary>
        /// 处理仪表采集数据
        /// </summary>
        /// <param name="iPoundRoom">计量点索引，从0开始</param>
        private void HandleMeterData(string weight)
        {
            //MessageBox.Show("weight:" + weight);
            if (!string.IsNullOrEmpty(weight))
            {
                //if (decData > m_PoundRoomArray[i].ZEROVALUE)//大于复位值
                //{
                //    HandleWeightPound(i, decData);
                //}
                //else//空磅
                //{
                //    HandleZeroPound(i, decData);
                //}

                if (_measApp != null)//当前正在操作的计量点，刷新界面
                {
                    #region 控制灯
                    if (_measApp.IoLogik != null && !_measApp.IoLogik.ReadDO()[0])
                    {
                        _measApp.IoLogik.DO[0] = true;
                        _measApp.IoLogik.WriteDO();
                    }
                    #endregion

                    double d_weight = 0;
                    double.TryParse(weight, out d_weight);

                    txtXSZL.Text = (d_weight / 1000).ToString();
                    txtZL.Text = (d_weight / 1000).ToString();
                    tbx_hWeight.Text = (d_weight / 1000).ToString();

                    try
                    {
                        if (txtQTYKL.Text != string.Empty)
                        {
                            txtJZ.Text = ((d_weight - double.Parse(txtQTYKL.Text)) / 1000).ToString();
                        }
                        else
                        {
                            txtJZ.Text = txtZL.Text;
                        }
                    }
                    catch
                    {

                    }
                    //MessageBox.Show("稳定:" + weight);
                    lbWD.Text = "稳定";
                    lbYS.ForeColor = Color.Green;
                    lbWD.Refresh();
                    lbYS.Refresh();

                    if (chkAutoSave.Checked)//自动保存
                    {
                        SaveWeightData();

                        //btnBC.Enabled = false;
                    }
                    else
                    {
                        btnBC.Enabled = true;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 开关LED电源
        /// </summary>
        private void HandleLEDPower(int iPoundRoom)
        {
            if (iPoundRoom < 0 || iPoundRoom >= m_nPointCount)
            {
                return;
            }

            int i = iPoundRoom;

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP) || _measApp.Led == null)
            {
                return;
            }

            //if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //{
            //    return;
            //}

            if (ultraToolbarsManager1.Toolbars[0].Tools["CloseLED"].SharedProps.Caption == "关闭LED显示")
            {
                _measApp.Led.SetPower(0);
                ultraToolbarsManager1.Toolbars[0].Tools["CloseLED"].SharedProps.Caption = "打开LED显示";
            }
            else if (ultraToolbarsManager1.Toolbars[0].Tools["CloseLED"].SharedProps.Caption == "打开LED显示")
            {
                _measApp.Led.SetPower(1);
                ultraToolbarsManager1.Toolbars[0].Tools["CloseLED"].SharedProps.Caption = "关闭LED显示";
            }
        }

        /// <summary>
        /// 处理语音对讲按钮事件
        /// </summary>
        private void HandleTalk(int iPoundRoom)
        {
            if (iPoundRoom < 0 || iPoundRoom >= m_nPointCount)
            {
                return;
            }


            int i = iPoundRoom;

            if (_isTalk && _talkId >= 0)//正在对讲，关闭
            {
                //_measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
                _measApp.Dvr.SDK_StopTalk();
                _isTalk = false;
                _talkId = -1;

                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }
            else
            {
                if (_measApp.Dvr != null)
                {
                    _talkId = _measApp.Dvr.SDK_StartTalk();
                    _measApp.Dvr.SDK_SetVolume(65500);
                    _measApp.Dvr.SDK_RealPlay(0, 0, (int)picFDTP.Handle);
                    _isTalk = true;

                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                }
            }
        }

        private void OpenBigPicture(int iChannel)
        {
            int i = m_iSelectedPound;
            bool isOpen = false;

            if (i < 0 || _measApp == null || _measApp.Cameras == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            //关闭小图片监视,打开大图片监视
          
            picFDTP.Width = VideoChannel1.Width * 2;
            picFDTP.Height = VideoChannel1.Height * 2;
            picFDTP.Visible = true;
            picFDTP.BringToFront();
            
            isOpen = _measApp.Cameras[iChannel].StopRealPlay();
            _measApp.Cameras[iChannel].Logout();
            _measApp.Cameras[iChannel].Login();
            isOpen = _measApp.Cameras[iChannel].RealPlay(picFDTP.Handle);
            BigChannel = iChannel;
            m_CurSelBigChannel = iChannel;
        }

        /// <summary>
        /// 关闭大图监视，还原小图监视
        /// </summary>
        private void CloseBigPicture()
        {
            int i = m_iSelectedPound;

            if (i < 0 || _measApp == null || _measApp.Cameras == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            if (m_CurSelBigChannel >= 0)
            {
                picFDTP.Visible = false;
                _measApp.Cameras[BigChannel].StopRealPlay();
                _measApp.Cameras[BigChannel].Logout();
                _measApp.Cameras[BigChannel].Login();
                if (BigChannel == 0)
                {
                    _measApp.Cameras[BigChannel].RealPlay(VideoChannel1.Handle);
                }
                else
                {
                    _measApp.Cameras[BigChannel].RealPlay(VideoChannel2.Handle);
                }
                m_CurSelBigChannel = -1;
                //picFDTP.Refresh();
            }
            else
            {
                picFDTP.Visible = false;
                //picFDTP.Refresh();
            }
        }

        /// <summary>
        /// 查询计量点基础数据信息
        /// </summary>
        /// <param name="iPoundRoom"></param>
        private void QueryPoundBaseInfo(int iPoundRoom)
        {
            int i = iPoundRoom;

            //if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            //{
            //    return;
            //}
            string strPointID = _measApp.Params.FS_POINTCODE;
            txtJLD.Text = _measApp.Params.FS_POINTNAME;
            this.txtZZBH.Text = getNextBatchNo(m_preFlag);
            this.txtBandNo.Text = getNextBandNo(txtZZBH.Text);

            //根据选择的计量点绑定物料、钢种、规格、发货单位、收货单位
            //BandPointMaterial(PointID);
            //BandPointReceiver(PointID); 
            //BandPointSender(PointID);
            //BandPointSteelType(PointID);
            //BandPointSpec(PointID);

            //add by luobin 取默认物料
            this.cbLX.Text = this.m_DefaultFlow;
            this.cbFHDW.Text = this.m_DefaultSender;
            this.cbSHDW.Text = this.m_DefaultReceiver;
            this.cbx_Standard.Text = this.m_DefaultStandard;
            this.cbWLMC.Text = this.m_DefaultMeterial;
        }

        /// <summary>
        /// 获取语音播报信息，这里把入库的语音统一管理，如果要把高线和其它入库点的语音分开，注意修改文件夹名称
        /// </summary>
        private void GetYYBBData()
        {
            string strName = "";
            string strType = "RK";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "QueryVoiceTableData";
            ccp.ServerParams = new object[] { strName, strType };
            ccp.SourceDataTable = dataTable2;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid5);

            if (System.IO.Directory.Exists(Constant.RunPath + "\\rksound\\") == false)
            {
                System.IO.Directory.CreateDirectory(Constant.RunPath + "\\rksound\\");
            }

            for (int i = 0; i < dataTable2.Rows.Count; i++)
            {
                if (System.IO.File.Exists(Constant.RunPath + "\\rksound\\" + dataTable2.Rows[i]["FS_VOICENAME"].ToString().Trim()) == false)
                {
                    System.IO.File.WriteAllBytes(Constant.RunPath + "\\rksound\\" + dataTable2.Rows[i]["FS_VOICENAME"].ToString().Trim(), (byte[])dataTable2.Rows[i]["FS_VOICEFILE"]);
                }
            }
        }

        /// <summary>
        /// 控件事件及属性设置
        /// </summary>
        private void ControlInit()
        {
            //隐藏放大图片控件
            this.picFDTP.Visible = false;

            //添加控件事件
            //this.VideoChannel1.Click += new EventHandler(VideoChannel1_Click);
            this.VideoChannel1.DoubleClick += new EventHandler(VideoChannel1_DoubleClick);
            //this.VideoChannel2.Click += new EventHandler(VideoChannel2_Click);
            this.VideoChannel2.DoubleClick += new EventHandler(VideoChannel2_DoubleClick);
           
            //this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            //this.ultraGrid2.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(ultraGrid2_AfterSelectChange);
            this.ultraGrid2.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(ultraGrid2_DoubleClickRow);
            
            this.ultraGrid5.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(ultraGrid5_ClickCell);
            this.picFDTP.DoubleClick += new EventHandler(picFDTP_DoubleClick);
            //this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(ultraToolbarsManager1_ToolClick);

            //自动保存CheckBox事件
            this.chkAutoSave.Click += new EventHandler(chkAutoSave_Click);

            //计量点Grid
            this.ultraGrid2.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid2.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid2.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn ugc in ultraGrid2.DisplayLayout.Bands[0].Columns)
            {
                if (ugc.Header.Caption.Trim() != "接管")
                {
                    ugc.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
                else
                {
                    ugc.CellActivation = Activation.AllowEdit;
                    ugc.AutoSizeEdit = DefaultableBoolean.True;
                }
            }

            //本批数据
            this.ultraGrid3.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid3.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid3.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid3.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid3.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid3.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn ugc in ultraGrid3.DisplayLayout.Bands[0].Columns)
            {
                ugc.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            } 

            //语音播报Grid
            this.ultraGrid5.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid5.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid5.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid5.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid5.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid5.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn ugc in ultraGrid5.DisplayLayout.Bands[0].Columns)
            {
                ugc.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            }           
        }

        /// <summary>
        /// 清空控件
        /// </summary>
        private void ClearControl()
        {
            this.txtXSZL.Text = "0.000";
            this.txtZZBH.Text = "";//轧制批号
            //this.txtDDH.Text = "";//订单号
            //this.txtDDXMH.Text = "";//订单项目号
            this.txtGH.Text = "";//钩号
            //this.cbWLMC.Text = "";//物料名称
            this.cbGZ.Text = "";//钢种
            this.cbGG.Text = "";//规格
            this.cbLX.Text = this.m_DefaultFlow;//流向
            this.cbFHDW.Text = this.m_DefaultSender;//发货单位
            this.cbSHDW.Text = this.m_DefaultReceiver;//收货单位
            this.lbWD.Text = "未连接到仪表";
            this.txtZL.Text = "0.000";
            this.lbYS.ForeColor = Color.Red;
            this.textMaxWeight.Text = "";
            this.textMinWeight.Text = "";
        }

        /// <summary>
        /// 查询计量点信息
        /// </summary>
        private void QueryJLDData()
        {
            try
            {
                string pointCode = "";
               

                string strWhere = "SELECT 'False' AS XZ,T.FS_POINTCODE,T.FS_POINTNAME,T.FS_POINTDEPART,T.FS_POINTTYPE,T.FS_VIEDOIP,T.FS_VIEDOPORT,T.FS_VIEDOUSER,T.FS_VIEDOPWD,";
                strWhere += " T.FS_METERTYPE,T.FS_METERPARA,T.FS_MOXAIP,T.FS_MOXAPORT,T.FS_RTUIP,T.FS_RTUPORT,T.FS_PRINTERIP,T.FS_PRINTERNAME,T.FS_PRINTTYPECODE,T.FN_USEDPRINTPAPER,";
                strWhere += " T.FN_USEDPRINTINK,T.FS_LEDIP,T.FS_LEDPORT,T.FN_VALUE,T.FS_ALLOWOTHERTARE,T.FS_SIGN,T.FS_DISPLAYPORT,T.FS_DISPLAYPARA,";
                strWhere += " T.FS_READERPORT,T.FS_READERPARA,T.FS_READERTYPE,T.FS_DISPLAYTYPE,T.FF_CLEARVALUE,A.FN_PAPERNUM TOTALPAPAR,A.FN_INKNUM TOTALINK FROM BT_POINT T ";
                strWhere += " LEFT OUTER JOIN BT_PRINTTYPE A ON T.FS_PRINTTYPECODE = A.FS_PRINTTYPECODE ";
                strWhere += " WHERE T.FS_POINTTYPE = 'ZG' ";
                strWhere += " and T.FS_POINTCODE='" + CustomInfo + "' ORDER BY T.FS_POINTCODE";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { strWhere };
                ccp.SourceDataTable = dataTable1;

                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                Constant.RefreshAndAutoSize(ultraGrid2);

                if (dataTable1 != null && dataTable1.Rows != null && dataTable1.Rows.Count > 0)
                {
                    ultraGrid2.Rows[0].Activated = false;

                    //InitPound(dataTable1);
                    //BeginPoundRoomThread();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }    

        /// <summary>
        /// 创建计量数据表格式
        /// </summary>
        private void BuildWeightDataTable()
        {
            DataColumn dc = new DataColumn("FS_BATCHNO"); dc.Caption = "批次号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_PRODUCTNO"); dc.Caption = "订单号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_ITEMNO"); dc.Caption = "订单项目号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_MATERIALNO"); dc.Caption = "物料代码"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_MATERIALNAME"); dc.Caption = "物料名称"; m_WeightDataTable.Columns.Add(dc);

            dc = new DataColumn("FS_MRP"); dc.Caption = "车间代码"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_STEELTYPE"); dc.Caption = "钢号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_SPEC"); dc.Caption = "规格"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_STORE"); dc.Caption = "库存点代码"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_BANDCOUNT"); dc.Caption = "累计吊数"; m_WeightDataTable.Columns.Add(dc);

            dc = new DataColumn("FN_BILLETCOUNT"); dc.Caption = "累计支数"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_TOTALWEIGHT"); dc.Caption = "累计重量"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FD_STARTTIME"); dc.Caption = "计量开始时间"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FD_ENDTIME"); dc.Caption = "计量结束时间"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_COMPLETEFLAG"); dc.Caption = "完炉标志"; m_WeightDataTable.Columns.Add(dc);

            dc = new DataColumn("FS_FLOW"); dc.Caption = "流向"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_TECHCARDNO"); dc.Caption = "工艺流动卡号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_WEIGHTNO"); dc.Caption = "作业编号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_BANDNO"); dc.Caption = "吊号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_HOOKNO"); dc.Caption = "钩号"; m_WeightDataTable.Columns.Add(dc);

            dc = new DataColumn("FN_BANDBILLETCOUNT"); dc.Caption = "支数"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_TYPE"); dc.Caption = "类型"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_LENGTH"); dc.Caption = "长度"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_WEIGHT"); dc.Caption = "重量"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_PERSON"); dc.Caption = "计量员"; m_WeightDataTable.Columns.Add(dc);

            dc = new DataColumn("FS_POINT"); dc.Caption = "计量点"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FD_DATETIME"); dc.Caption = "计量时间"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_SHIFT"); dc.Caption = "班次"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_TERM"); dc.Caption = "班别"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_LABELID"); dc.Caption = "条码号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_DECWEIGHT"); dc.Caption = "扣重"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_FLAG"); dc.Caption = "标识"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_JJ_WEIGHT"); dc.Caption = "坯重"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FS_GP_STOVENO"); dc.Caption = "炉号"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_GP_TOTALCOUNT"); dc.Caption = "条数"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("FN_THEORYWEIGHT"); dc.Caption = "理论重量"; m_WeightDataTable.Columns.Add(dc);

            dc = new DataColumn("fn_theorytotalweight"); dc.Caption = "理论总重"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("rDiff"); dc.Caption = "当前负偏差"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("tDiff"); dc.Caption = "累计负偏差"; m_WeightDataTable.Columns.Add(dc);
            dc = new DataColumn("rate"); dc.Caption = "成材率"; m_WeightDataTable.Columns.Add(dc);


            ultraGrid3.DataSource = m_WeightDataTable;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_BATCHNO"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_THEORYWEIGHT"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_BATCHNO"].Width = 100;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_PRODUCTNO"].Hidden = true;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_ITEMNO"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_MATERIALNO"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_MATERIALNAME"].Hidden = true;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_MRP"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_STEELTYPE"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_SPEC"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_STORE"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_BANDCOUNT"].Hidden = true;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_BILLETCOUNT"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_TOTALWEIGHT"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FD_STARTTIME"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FD_ENDTIME"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_COMPLETEFLAG"].Hidden = true;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_FLOW"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_TECHCARDNO"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_WEIGHTNO"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_BANDNO"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_BANDNO"].Width = 50;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_HOOKNO"].Hidden = true;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_WEIGHT"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_WEIGHT"].Width = 50;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_BANDBILLETCOUNT"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_TYPE"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_TYPE"].Width = 60;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_LENGTH"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_LENGTH"].Width = 50;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_SHIFT"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_SHIFT"].Width = 50;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FD_DATETIME"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FD_DATETIME"].Width = 135;
  
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_PERSON"].Hidden = false;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_PERSON"].Width = 60;

            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_POINT"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_TERM"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_LABELID"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_DECWEIGHT"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_FLAG"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_JJ_WEIGHT"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FS_GP_STOVENO"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["FN_GP_TOTALCOUNT"].Hidden = true;

            ultraGrid3.DisplayLayout.Bands[0].Columns["fn_theorytotalweight"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["rDiff"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["tDiff"].Hidden = true;
            ultraGrid3.DisplayLayout.Bands[0].Columns["rate"].Hidden = true;

            //Constant.RefreshAndAutoSize(ultraGrid3);
        }

        /// <summary>
        /// 按批次号查询计量明细数据--用于LED和液晶屏以及界面显示
        /// </summary>
        /// <param name="strBatchNo"></param>
        private void QueryWeightDataByBatchNo()
        {
            try
            {
                string strWhere = "SELECT F.FN_BANDCOUNT,A.FS_BATCHNO, A.FS_PRODUCTNO, '' as FS_ITEMNO, A.FS_MATERIALNO, A.FS_MATERIALNAME, C.FS_MEMO FS_MRP, "
                                + " A.FS_STEELTYPE, A.FS_SPEC, D.FS_MEMO FS_STORE,A.FN_BILLETCOUNT, ROUND(A.FN_TOTALWEIGHT,3) AS FN_TOTALWEIGHT, TO_CHAR(A.FD_STARTTIME, 'YYYY-MM-DD HH24:MI:SS') AS FD_STARTTIME, "
                                + " TO_CHAR(A.FD_ENDTIME, 'YYYY-MM-DD HH24:MI:SS') AS FD_ENDTIME, A.FS_COMPLETEFLAG,A.FS_FLOW, A.FS_TECHCARDNO,B.FS_WEIGHTNO, B.FN_BANDNO, "
                                + " B.FN_HOOKNO, B.FN_BANDBILLETCOUNT,B.FS_TYPE,B.FN_LENGTH, to_char(ROUND(B.FN_WEIGHT,3),'FM999999.000') as FN_WEIGHT, B.FS_PERSON, B.FS_POINT, TO_CHAR(B.FD_DATETIME, 'YYYY-MM-DD HH24:MI:SS') AS FD_DATETIME, "
                                + " B.FS_SHIFT, B.FS_TERM, B.FS_LABELID, ROUND(B.FN_DECWEIGHT,3) FN_DECWEIGHT, B.FS_FLAG,ROUND(F.Fn_Billetweight,3) as FN_JJ_WEIGHT,E.FS_GP_STOVENO,E.FN_GP_TOTALCOUNT,B.Fn_Theoryweight AS Fn_Theoryweight,a.fn_theorytotalweight"
                                + ",decode(nvl(B.Fn_Theoryweight,0),0,0,round((B.Fn_Theoryweight-B.FN_WEIGHT)/B.Fn_Theoryweight,4)*100) as rDiff "
                                +",decode(nvl(a.fn_theorytotalweight,0),0,0,round((a.fn_theorytotalweight-A.FN_TOTALWEIGHT)/a.fn_theorytotalweight,4)*100) as tDiff "
                                +",decode(nvl(F.Fn_Billetweight,0),0,0,round(A.FN_TOTALWEIGHT/F.Fn_Billetweight,4)*100) as rate  "
                                + " FROM dt_pipeweightmain A "
                                + " LEFT OUTER JOIN dt_pipeweightdetail B ON A.FS_BATCHNO = B.FS_BATCHNO "
                                + " LEFT OUTER JOIN IT_MRP C ON C.FS_FH = A.FS_MRP "
                                + " LEFT OUTER JOIN DT_PRODUCTPLAN F ON F.FS_BATCHNO = A.FS_BATCHNO AND F.FS_POINTID = '"+CustomInfo+"'"
                                + " LEFT OUTER JOIN IT_STORE D ON A.FS_STORE = D.FS_SH "
                                + " LEFT OUTER JOIN IT_FP_TECHCARD E ON A.FS_TECHCARDNO = E.FS_CARDNO "
                                + " WHERE A.FS_BATCHNO = '" + m_szCurBatchNo + "'" + " "
                                + " ORDER BY A.FS_BATCHNO, B.FN_BANDNO desc";
                m_WeightDataTable.Clear();
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { strWhere };
       
                ccp.SourceDataTable = m_WeightDataTable;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                ultraGrid3.UpdateData();
                Constant.RefreshAndAutoSize(ultraGrid3);
                
                if(m_WeightDataTable != null && m_WeightDataTable.Rows != null && m_WeightDataTable.Rows.Count > 0)
                {
                    ultraGroupBox1.Text = "轧制编号：" + m_WeightDataTable.Rows[0]["FS_BATCHNO"].ToString() + "，共 " + m_WeightDataTable.Rows[0]["FN_BANDNO"].ToString() + "吊，合计 "
                         + m_WeightDataTable.Rows[0]["FN_TOTALWEIGHT"].ToString() + " 吨";

                    ultraGroupBox1.Refresh();
                }
               
                m_bQueryWeightDataOver = true;
            }
            catch (System.Exception exp)
            {
                //MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 按批次号查询计量明细数据
        /// </summary>
        /// <param name="strBatchNo"></param>
        private void QueryWeightDataByBatchNoNoLed(string strBatchNo)
        {
            try
            {
                string strWhere = "SELECT F.FN_BANDCOUNT,A.FS_BATCHNO, A.FS_PRODUCTNO, A.FS_ITEMNO, A.FS_MATERIALNO, A.FS_MATERIALNAME, C.FS_MEMO FS_MRP, "
                                + " A.FS_STEELTYPE, A.FS_SPEC, D.FS_MEMO FS_STORE,A.FN_BILLETCOUNT, to_char(ROUND(A.FN_TOTALWEIGHT,3),'FM9999.000') AS FN_TOTALWEIGHT, TO_CHAR(A.FD_STARTTIME, 'YYYY-MM-DD HH24:MI:SS') AS FD_STARTTIME, "
                                + " TO_CHAR(A.FD_ENDTIME, 'YYYY-MM-DD HH24:MI:SS') AS FD_ENDTIME, A.FS_COMPLETEFLAG,A.FS_FLOW, A.FS_TECHCARDNO,B.FS_WEIGHTNO, B.FN_BANDNO, "
                                + " B.FN_HOOKNO, B.FN_BANDBILLETCOUNT,B.FS_TYPE,B.FN_LENGTH, ROUND(B.FN_WEIGHT,3) FN_WEIGHT, B.FS_PERSON, B.FS_POINT, TO_CHAR(B.FD_DATETIME, 'YYYY-MM-DD HH24:MI:SS') AS FD_DATETIME, "
                                + " B.FS_SHIFT, B.FS_TERM, B.FS_LABELID, ROUND(B.FN_DECWEIGHT,3) FN_DECWEIGHT, B.FS_FLAG,to_char(ROUND(F.Fn_Billetweight,3),'FM9999.000') as FN_JJ_WEIGHT,E.FS_GP_STOVENO,E.FN_GP_TOTALCOUNT,to_char(B.Fn_Theoryweight,'FM9999.000') AS Fn_Theoryweight "
                                + " FROM DT_PRODUCTWEIGHTMAIN A "
                                + " LEFT OUTER JOIN DT_PRODUCTWEIGHTDETAIL B ON A.FS_BATCHNO = B.FS_BATCHNO "
                                + " LEFT OUTER JOIN IT_MRP C ON C.FS_FH = A.FS_MRP "
                                + " LEFT OUTER JOIN DT_PRODUCTPLAN F ON F.FS_BATCHNO = A.FS_BATCHNO "
                                + " LEFT OUTER JOIN IT_STORE D ON A.FS_STORE = D.FS_SH "
                                + " LEFT OUTER JOIN IT_FP_TECHCARD E ON A.FS_TECHCARDNO = E.FS_CARDNO "
                                + " WHERE A.FS_BATCHNO = '" + m_szCurBatchNo + "'" + " "
                                + " ORDER BY A.FS_BATCHNO, B.FN_BANDNO desc";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { strWhere };

                ccp.SourceDataTable = m_WeightDataTable;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                //Constant.RefreshAndAutoSize(ultraGrid3);

                if (m_WeightDataTable != null && m_WeightDataTable.Rows != null && m_WeightDataTable.Rows.Count > 0)
                {
                    ultraGroupBox1.Text = "轧制编号：" + m_WeightDataTable.Rows[0]["FS_BATCHNO"].ToString() + "，共 " + m_WeightDataTable.Rows[0]["FN_BANDNO"].ToString() + "吊，合计 "
                         + m_WeightDataTable.Rows[0]["FN_TOTALWEIGHT"].ToString() + " 吨";

                    ultraGroupBox1.Refresh();
                }

                m_bQueryWeightDataOver = true;
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 加载棒材理论基础表
        /// </summary>
        private void GetProductTheoryBaseInfo()
        {
            try
            {
                string strSql = "select FS_SPEC,FN_LENGTH,FN_WEIGHT,FN_BILLETCOUNT from BT_BCTHEORYWEIGHTINFO";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { strSql };
                dt_TheoryWeight = new DataTable();
                ccp.SourceDataTable = this.dt_TheoryWeight;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 按批次号查询计量明细数据
        /// </summary>
        /// <param name="strBatchNo"></param>
        private DataRow QueryTheoryWeightByBatchNo(string strWeightNo)
        {
            try
            {
                string strWhere = "select TO_CHAR(FN_THEORYWEIGHT,'FM999999.000') AS FN_THEORYWEIGHT,FN_BANDNO from DT_PRODUCTWEIGHTDETAIL where FS_WEIGHTNO = '" + strWeightNo + "'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { strWhere };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                if (dt_temp != null && dt_temp.Rows.Count > 0)
                {
                    return dt_temp.Rows[0];
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
                return null;
            }
        }

        /// <summary>
        /// 从表是否上传
        /// </summary>
        /// <param name="strBatchNo"></param>
        /// <param name="strBandNo"></param>
        /// <returns></returns>
        private bool IsUpLoadForDetail(string strBatchNo, string strBandNo)
        {
            bool reVal = false;
            DataTable dt = new DataTable();
            try
            {
                string sql = "select FS_UPLOADFLAG from DT_PRODUCTWEIGHTDETAIL where FS_BATCHNO = '" + strBatchNo + "' and FN_BANDNO = '" + strBandNo + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = dt;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                //if (dt.Rows[0]["FS_UPLOADFLAG"] == "1")
                if (dt.Rows[0]["FS_UPLOADFLAG"].ToString() == "1")
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
        /// 保存计量信息
        /// </summary>
        private void SaveWeightData()
        {
            //输入内容验证
            if (CheckInput() == false)
            {
                return;
            }
            int i = m_iSelectedPound;


            //复磅
            if (this.cbx_fb.CheckState == CheckState.Checked)
            {
                if (this.tb_zzbh_fb.Text.Trim() == "" || this.tb_bandno_fb.Text.Trim() == "")
                {
                    MessageBox.Show("复磅信息不全！");
                    return;
                }

                if ((IsUpLoadForDetail(tb_zzbh_fb.Text.Trim(), this.tb_bandno_fb.Text.Trim())) == true)
                {
                    MessageBox.Show("该数据已经上传，不允许复磅！");
                    return;
                }

                //DoReWeightForBC(this.tb_zzbh_fb.Text.Trim(), this.tb_bandno_fb.Text.Trim(), this.txtXSZL.Text.Trim());
                return;
            }

            //手工录入
            if (this.cbx_Hand.CheckState == CheckState.Checked || CustomInfo.Equals("K33") || CustomInfo.Equals("K36"))
            {
                if (CheckIsNumber(this.tbx_hWeight.Text.Trim()))
                {
                    SaveWeightDataByHand();
                }
                else
                {
                    MessageBox.Show("手工录入的重量不是有效的数字！");
                }
                return;
            }

            if (_saved)//是否已经保存
            {
                MessageBox.Show("数据已经保存过了，不允许再次保存！", "提示", MessageBoxButtons.OK);
                return;
            }
            if (txtXSZL.Text == "0.000")
            {
                MessageBox.Show("仪表数据为零，请联系管理员！");
                return;
            }
            try
            {
                //DataRow dr = QueryPlanDataByBatchNo(txtZZBH.Text.Trim());//从预报内存表中查询某一批次的相关信息
                //if (dr == null)
                //{
                //    MessageBox.Show("没有该批次的预报信息！", "提示", MessageBoxButtons.OK);
                //}
                string strMaterialCode = "";

                if (cbWLMC.SelectedIndex > 0)
                {
                    strMaterialCode = cbWLMC.SelectedValue.ToString();
                }

                float p_FN_LENGTH = 0;
                string p_FS_BATCHNO = txtZZBH.Text.Trim();//批次号
                m_szCurBatchNo = p_FS_BATCHNO;

                string p_FS_PRODUCTNO = txtDDH.Text.Trim();//订单号
                string p_FS_ITEMNO = "";//定单项目号
                string p_FS_MATERIALNO = strMaterialCode;//物料代码
                string p_FS_MATERIALNAME = cbWLMC.Text.Trim();//物料名称
                string P_FS_BANDNO = this.txtBandNo.Text;

                string p_FS_MRP = "";
                if (cbFHDW.SelectedIndex >= 0)
                {
                    p_FS_MRP = cbFHDW.SelectedValue.ToString();//发货单位（车间名称）
                }
                string p_FS_STORE = "";
                if (cbSHDW.SelectedIndex >= 0)
                {
                    p_FS_STORE = cbSHDW.SelectedValue.ToString();//收货单位（库存点）
                }
                string p_FS_STEELTYPE = cbGZ.Text.Trim();//钢种
                string p_FS_SPEC = cbGG.Text.Trim();//规格
                WeightNo = Guid.NewGuid().ToString();//计量作业编号
                string p_FS_WEIGHTNO = WeightNo;
                int p_FN_BANDBILLETCOUNT = 0;//支数
                if (txtZS.Text.Trim().Length > 0)
                {
                    p_FN_BANDBILLETCOUNT = Convert.ToInt32(txtZS.Text.Trim());
                }
                string p_FS_TYPE = cbLXIN.Text.Trim();//类型
                if (!float.TryParse(cbDCCD.Text.Trim(), out p_FN_LENGTH))
                {
                    p_FN_LENGTH = 0;
                }
                double p_FN_WEIGHT = 0;
                double.TryParse(this.txtZL.Text.Trim(), out p_FN_WEIGHT);
                //double p_FN_WEIGHT = Convert.ToDouble(string.Format("{0:F3}", Single.Parse(this.txtZL.Text.Trim())));//重量
                string p_FS_PERSON = UserInfo.GetUserName();//计量员
                string p_FS_POINT = _measApp.Params.FS_POINTCODE;//计量点编号
                string p_FS_SHIFT = UserInfo.GetUserOrder();//班次
                string p_FS_TERM = UserInfo.GetUserGroup();//班别
                //string p_FS_LABELID = "00000000";//条码号
                string strBarCode = GetBarCode(p_FS_BATCHNO, this.txtBandNo.Text);
                string p_FS_LABELID = strBarCode;
                string p_FS_FLOW = "";//流向
                if (cbLX.SelectedIndex > 0)
                {
                    p_FS_FLOW = cbLX.SelectedValue.ToString();
                }
                float p_FN_DECWEIGHT = 0;
                if (txtQTYKL.Text.Trim().Length > 0)
                {
                    p_FN_DECWEIGHT = Convert.ToSingle(string.Format("{0:F3}", Single.Parse(txtQTYKL.Text.Trim())));//应扣重量
                }
                Hashtable ht = new Hashtable();
                ht.Add("I1", p_FS_BATCHNO);
                ht.Add("I2", p_FS_PRODUCTNO);
                ht.Add("I3", "");
                ht.Add("I4", p_FS_MATERIALNO);
                ht.Add("I5", p_FS_MRP);
                ht.Add("I6", p_FS_STORE);
                ht.Add("I7", p_FS_STEELTYPE);
                ht.Add("I8", p_FS_SPEC);
                ht.Add("I9", p_FS_WEIGHTNO);
                ht.Add("I10", p_FN_BANDBILLETCOUNT);
                ht.Add("I11", p_FS_TYPE);
                ht.Add("I12", p_FN_LENGTH);
                ht.Add("I13", p_FN_WEIGHT);
                ht.Add("I14", p_FS_PERSON);
                ht.Add("I15", p_FS_POINT);
                ht.Add("I16", p_FS_SHIFT);
                ht.Add("I17", p_FS_TERM);
                ht.Add("I18", strBarCode);
                ht.Add("I19", p_FS_MATERIALNAME);
                ht.Add("I20", p_FS_FLOW);
                ht.Add("I21", p_FN_DECWEIGHT);
                ht.Add("I22", this.txtBandNo.Text);
                ht.Add("I23",this.tbRKDH.Text.Trim());
                ht.Add("I24", this.cbx_Standard.Text);
                ht.Add("I25", string.IsNullOrEmpty(txtJZ.Text.Trim()) ? "0" : txtJZ.Text.Trim());//理重
                ht.Add("I26",cbxRemark.Text.Trim());
                CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_PIPE.INSERT_DT_PIPEWEIGHT(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}", ht);

                string reVal = "";
                if (ccp.ReturnCode != 0)
                {
                    MessageBox.Show("数据保存失败！");
                    return;
                }
                else
                {
                    reVal = "";
                    if (ccp.ReturnObject != null)
                    {
                        reVal = ccp.ReturnObject.ToString();
                    }
                    ////返回为空表示没有下一预报
                    //if (reVal == "" || reVal != this.m_szCurBatchNo)
                    //{
                    //    messageForm.SetMessage("批次" + txtZZBH.Text.Trim() + "  已完炉!");
                    //    bCompleteStove = true;
                    //    ClearControl();
                    //    QueryWeightDataByBatchNo();
                    //    m_WeightDataTable.Rows.Clear();
                    //    //重新绑定新预报到界面
                    //    QueryLastStoveNo();
                    //}
                    //else
                    //{
                        messageForm.SetMessage("保存成功！");

                        tbx_lastbatch.Text = p_FS_BATCHNO;
                        tbx_lastbandcount.Text = this.txtBandNo.Text;
                        tbx_lasttotalweight.Text = p_FN_WEIGHT.ToString();

                        QueryWeightDataByBatchNo();
                        this.txtBandNo.Text = getNextBandNo(txtZZBH.Text);
                        bCompleteStove = false;
                    //}

                }
                //保存成功后保存和完炉按钮不可操作，直到下一吊上称后
                //btnBC.Enabled = false;

                _saved = true;

                //存储图片
                this._strBatchNo = p_FS_BATCHNO;
                this._strBandNo = GetMaxBandNoOfBatchNo(p_FS_BATCHNO);
                GraspAndSaveImage();

               
                #region 条码打印
                DataRow dRow = QueryTheoryWeightByBatchNo(p_FS_WEIGHTNO);
                if (cbx_print.Checked)
                {
                    if (_measApp.Printer != null)
                    {
                        _measApp.Printer.Data = new HgLable();
                        _measApp.Printer.Data.PM = this.m_DefaultPM;
                        _measApp.Printer.Data.BatchNo = p_FS_BATCHNO; //批次号
                        
                        _measApp.Printer.Data.Spec = p_FS_SPEC;//规格
                        _measApp.Printer.Data.Weight = p_FN_WEIGHT.ToString();  //重量
                        if (CustomInfo.Equals("K33") || CustomInfo.Equals("K36"))//720机组打印理重
                        {
                            _measApp.Printer.Data.Weight = txtJZ.Text.Trim();  //重量
                        }
                        _measApp.Printer.Data.Date = DateTime.Now;
                        string strTerm = UserInfo.GetUserGroup();
                        if (CustomInfo.Equals("K33") || CustomInfo.Equals("K36"))
                        {
                            strTerm = UserInfo.GetUserGroup().Equals("1") ? "甲" : UserInfo.GetUserGroup().Equals("2") ? "乙" : UserInfo.GetUserGroup().Equals("3") ? "丙" : UserInfo.GetUserGroup().Equals("4") ? "丁" : "常白";
                        }
                       
                        _measApp.Printer.Data.Term = strTerm; //班别

                        _measApp.Printer.Data.PrintAddress = true; //地址
                        _measApp.Printer.Data.Standard = this.cbx_Standard.Text; //标准
                        _measApp.Printer.Data.SteelType = p_FS_STEELTYPE; //牌号
                        _measApp.Printer.Data.Length = p_FN_LENGTH.ToString();
                        _measApp.Printer.Data.Count = p_FN_BANDBILLETCOUNT.ToString(); //支数
                        if (p_FS_TYPE == "非定尺" || p_FS_TYPE == "非定尺(3-6m)")
                        {
                            _measApp.Printer.Data.Count = "非";
                        }
                        switch (CustomInfo)
                        {
                            case "K26":
                            case "K27":
                                _measApp.Printer.Data.Type = LableType.PIPE;
                                break;
                            case "K34":
                            case "K32":
                            case "K37":
                                _measApp.Printer.Data.Type = LableType.PIPE;
                                break;
                            case "K33":
                            case "K36":
                                _measApp.Printer.Data.Type = LableType.PIPE2;
                                break;
                        }

                        string theoryWeight = "0";
                        //根据界面支数和规格 查询单捆重量

                        if (dRow != null)
                        {
                            //_measApp.Printer.Data.BandNo = dRow["FN_BANDNO"].ToString();
                            theoryWeight = dRow["FN_THEORYWEIGHT"].ToString();
                        }
                        else
                        {
                            //_measApp.Printer.Data.BandNo = "1";
                        }
                        _measApp.Printer.Data.BandNo = P_FS_BANDNO.Length == 1 ? ("0" + P_FS_BANDNO) : (P_FS_BANDNO);

                        _measApp.Printer.Data.BarCode = strBarCode; //条码号

                        //_measApp.Printer.Copies = 2;
                        //_measApp.Printer.Print();
                        //_measApp.Printer.Print();
                    }
                }

                ////上传垛帐系统
                //DepotObject depotObj = new DepotObject();
                //depotObj.BatchNo = p_FS_BATCHNO;
                //depotObj.BatchIndex = _strBandNo;
                //depotObj.Weight = p_FN_WEIGHT.ToString();
                //depotObj.TheoryWeight = dRow["FN_THEORYWEIGHT"].ToString();
                //depotObj.Barcode = GetBarCode(p_FS_LABELID, _strBandNo);
                //_depotManage.UploadDepot(depotObj);
                #endregion


            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 手工录入
        /// </summary>
        private void SaveWeightDataByHand()
        {
            try
            {
                //DataRow dr = QueryPlanDataByBatchNo(txtZZBH.Text.Trim());//从预报内存表中查询某一批次的相关信息
                //if (dr == null)
                //{
                //    MessageBox.Show("没有该批次的预报信息！", "提示", MessageBoxButtons.OK);
                //}
                string strMaterialCode = "";

                if (cbWLMC.SelectedIndex > 0)
                {
                    strMaterialCode = cbWLMC.SelectedValue.ToString();
                }

                float p_FN_LENGTH = 0;
                string p_FS_BATCHNO = txtZZBH.Text.Trim();//批次号
                m_szCurBatchNo = p_FS_BATCHNO;

                string p_FS_PRODUCTNO = txtDDH.Text.Trim();//订单号
                string p_FS_ITEMNO = "";//定单项目号
                string p_FS_MATERIALNO = strMaterialCode;//物料代码
                string p_FS_MATERIALNAME = cbWLMC.Text.Trim();//物料名称
                string p_FS_BANDNO = this.txtBandNo.Text;

                string p_FS_MRP = "";
                if (cbFHDW.SelectedIndex >= 0)
                {
                    p_FS_MRP = cbFHDW.SelectedValue.ToString();//发货单位（车间名称）
                }
                string p_FS_STORE = "";
                if (cbSHDW.SelectedIndex >= 0)
                {
                    p_FS_STORE = cbSHDW.SelectedValue.ToString();//收货单位（库存点）
                }
                string p_FS_STEELTYPE = cbGZ.Text.Trim();//钢种
                string p_FS_SPEC = cbGG.Text.Trim();//规格
                WeightNo = Guid.NewGuid().ToString();//计量作业编号
                string p_FS_WEIGHTNO = WeightNo;
                int p_FN_BANDBILLETCOUNT = 0;//支数
                if (txtZS.Text.Trim().Length > 0)
                {
                    p_FN_BANDBILLETCOUNT = Convert.ToInt32(txtZS.Text.Trim());
                }
                string p_FS_TYPE = cbLXIN.Text.Trim();//类型

                if (!float.TryParse(cbDCCD.Text.Trim(), out p_FN_LENGTH))
                {
                    p_FN_LENGTH = 0;
                }

                //if (p_FS_TYPE == "非定尺" || p_FS_TYPE == "非定尺(3-6m)")
                //{
                //    p_FN_LENGTH = 0;
                //    p_FN_BANDBILLETCOUNT = 0;
                //}

                double p_FN_WEIGHT = Convert.ToDouble(string.Format("{0:F3}", Single.Parse(this.tbx_hWeight.Text.Trim())));//重量
                string p_FS_PERSON = m_szCurUser;//计量员
                string p_FS_POINT = _measApp.Params.FS_POINTCODE;//计量点编号
                string p_FS_SHIFT = m_szCurBC;//班次
                string p_FS_TERM = m_szCurBZ;
                //string p_FS_LABELID = "00000000";//条码号
                string strBarCode = GetBarCode(p_FS_BATCHNO, this.txtBandNo.Text);
                string p_FS_LABELID = strBarCode;
                string p_FS_FLOW = "";//流向
                if (cbLX.SelectedIndex > 0)
                {
                    p_FS_FLOW = cbLX.SelectedValue.ToString();
                }
                float p_FN_DECWEIGHT = 0;
                if (txtQTYKL.Text.Trim().Length > 0)
                {
                    p_FN_DECWEIGHT = Convert.ToSingle(string.Format("{0:F3}", Single.Parse(txtQTYKL.Text.Trim())));//应扣重量
                }

                Hashtable ht = new Hashtable();
                ht.Add("I1", p_FS_BATCHNO);
                ht.Add("I2", p_FS_PRODUCTNO);
                ht.Add("I3", "");
                ht.Add("I4", p_FS_MATERIALNO);
                ht.Add("I5", p_FS_MRP);
                ht.Add("I6", p_FS_STORE);
                ht.Add("I7", p_FS_STEELTYPE);
                ht.Add("I8", p_FS_SPEC);
                ht.Add("I9", p_FS_WEIGHTNO);
                ht.Add("I10", p_FN_BANDBILLETCOUNT);
                ht.Add("I11", p_FS_TYPE);
                ht.Add("I12", p_FN_LENGTH);
                ht.Add("I13", p_FN_WEIGHT);
                ht.Add("I14", p_FS_PERSON);
                ht.Add("I15", p_FS_POINT);
                ht.Add("I16", p_FS_SHIFT);
                ht.Add("I17", p_FS_TERM);
                ht.Add("I18", strBarCode);
                ht.Add("I19", p_FS_MATERIALNAME);
                ht.Add("I20", p_FS_FLOW);
                ht.Add("I21", p_FN_DECWEIGHT);
                ht.Add("I22", this.txtBandNo.Text);
                ht.Add("I23",this.tbRKDH.Text.Trim());
                ht.Add("I24", this.cbx_Standard.Text);
                ht.Add("I25", string.IsNullOrEmpty(txtJZ.Text.Trim()) ? "0" : txtJZ.Text.Trim());
                ht.Add("I26", cbxRemark.Text.Trim());

                CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_PIPE.INSERT_DT_PIPEWEIGHT(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}", ht);
                string reVal = "";
                if (ccp.ReturnCode != 0)
                {
                    MessageBox.Show("数据保存失败！");
                    return;
                }
                else
                {
                    reVal = "";
                    if (ccp.ReturnObject != null)
                    {
                        reVal = ccp.ReturnObject.ToString();
                    }
                    //返回为空表示没有下一预报
                    //if (reVal == "" || reVal != this.m_szCurBatchNo)
                    //{
                    //    messageForm.SetMessage("批次" + txtZZBH.Text.Trim() + "  已完炉!");
                    //    this.bCompleteStove = true;
                    //    ClearControl();
                    //    QueryWeightDataByBatchNo();
                    //    //自动跳到下一个预报，而不是第一条
                    //    m_WeightDataTable.Rows.Clear();                      
                    //    QueryLastStoveNo();
                    //}
                    //else
                    //{
                        messageForm.SetMessage("保存成功！");

                        tbx_lastbatch.Text = p_FS_BATCHNO;
                        tbx_lastbandcount.Text = p_FS_BANDNO;
                        tbx_lasttotalweight.Text = p_FN_WEIGHT.ToString();

                        QueryWeightDataByBatchNo();
                        this.txtBandNo.Text = getNextBandNo(txtZZBH.Text);
                        bCompleteStove = false;
                    //}
                }           

                //存储图片
                this._strBatchNo = p_FS_BATCHNO;
                this._strBandNo = GetMaxBandNoOfBatchNo(_strBatchNo);

                #region 条码打印

                DataRow dRow = QueryTheoryWeightByBatchNo(p_FS_WEIGHTNO);
                if (cbx_print.Checked)
                {
                    if (_measApp.Printer != null)
                    {

                        _measApp.Printer.Data = new HgLable();
                        //_measApp.Printer.Data.PM = this.cbx_PM.Text;
                        _measApp.Printer.Data.PM = m_DefaultPM;
                        _measApp.Printer.Data.BatchNo = p_FS_BATCHNO; //批次号
                        //_measApp.Printer.PrinterName = cbPrint.Text;
                        _measApp.Printer.Data.Spec = p_FS_SPEC;//规格
                        _measApp.Printer.Data.Weight = p_FN_WEIGHT.ToString();  //重量
                        if (CustomInfo.Equals("K33") || CustomInfo.Equals("K36"))//720机组打印理重
                        {
                            _measApp.Printer.Data.Weight = txtJZ.Text.Trim();
                        }
                        _measApp.Printer.Data.Date = DateTime.Now;
                        string strTerm = UserInfo.GetUserGroup();
                        if (CustomInfo.Equals("K33") || CustomInfo.Equals("K36"))
                        {
                            strTerm = UserInfo.GetUserGroup().Equals("1") ? "甲" : UserInfo.GetUserGroup().Equals("2") ? "乙" : UserInfo.GetUserGroup().Equals("3") ? "丙" : UserInfo.GetUserGroup().Equals("4") ? "丁" : "常白";
                        }
                        _measApp.Printer.Data.Term = strTerm; //班别

                        _measApp.Printer.Data.PrintAddress = true; //地址
                        _measApp.Printer.Data.Standard = this.cbx_Standard.Text; //标准
                        _measApp.Printer.Data.SteelType = p_FS_STEELTYPE; //牌号
                        _measApp.Printer.Data.Length = p_FN_LENGTH.ToString();
                        _measApp.Printer.Data.Count = p_FN_BANDBILLETCOUNT.ToString(); //支数 
                        if (p_FS_TYPE == "非定尺" || p_FS_TYPE == "非定尺(3-6m)")
                        {
                            _measApp.Printer.Data.Count = "非";
                        }
                        switch(CustomInfo)
                        {
                            case "K26":
                            case "K27":
                                _measApp.Printer.Data.Type = LableType.PIPE;
                                break;
                            case "K34":
                            case "K32":
                            case "K37":
                                _measApp.Printer.Data.Type = LableType.PIPE;
                                break;
                            case "K33":
                            case "K36":
                                _measApp.Printer.Data.Type = LableType.PIPE2;
                                break;
                        }

                        string theoryWeight = "0";
                        //根据界面支数和规格 查询单捆重量

                        if (dRow != null)
                        {
                            //_measApp.Printer.Data.BandNo = dRow["FN_BANDNO"].ToString();
                            theoryWeight = dRow["FN_THEORYWEIGHT"].ToString();
                        }
                        else
                        {
                            //_measApp.Printer.Data.BandNo = "1";
                        }
                        _measApp.Printer.Data.BandNo = p_FS_BANDNO.Trim().Length == 1 ? ("0" + p_FS_BANDNO.Trim()) : (p_FS_BANDNO.Trim());

                        _measApp.Printer.Data.BarCode = strBarCode; //条码号

                        //_measApp.Printer.Copies = 1;
                        //_measApp.Printer.Print();
                        //_measApp.Printer.Print();
                    }
                }
                #endregion

                //上传垛帐系统
                //DepotObject depotObj = new DepotObject();
                //depotObj.BatchNo = p_FS_BATCHNO;
                //depotObj.BatchIndex = _strBandNo;
                //depotObj.Weight = p_FN_WEIGHT.ToString();
                //depotObj.TheoryWeight = dRow["FN_THEORYWEIGHT"].ToString();
                //depotObj.Barcode = GetBarCode(p_FS_LABELID, _strBandNo);
                //_depotManage.UploadDepot(depotObj);
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

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

        private bool CheckExist(string strBatchNo,string strBandNo)
        {
            try
            {
                string strSql = "SELECT * from dt_productweightdetail WHERE FS_BATCHNO='" + strBatchNo + "' and fn_bandno = '" + strBandNo + "'";
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

        private string GetMaxBandNoOfBatchNo(string strBatchNo)
        {
            try
            {
                string strSql = "SELECT max(FN_BANDNO) AS FN_BANDNO from dt_pipeweightdetail WHERE FS_BATCHNO='" + strBatchNo + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteQuery";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    return dt_temp.Rows[0]["FN_BANDNO"].ToString();
                }
                else
                {
                    return "1";
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return "";
            }
        }

        /// <summary>
        /// 棒材复磅
        /// </summary>
        /// <param name="strZZBH"></param>
        /// <param name="strBandNo"></param>
        private void DoReWeightForBC(string strZZBH,string strBandNo,string strWeight)
        {
            try
            {
                //***************************************
                //这里应先做是否存在strZZBH+strBandNo记录的检查

                if (!CheckExist(strZZBH, strBandNo))
                {
                    MessageBox.Show("您要复磅的数据不存在，请检查！");
                    return;
                }

                //***************************************
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "DoReWeightForBC";
                ccp.ServerParams = new object[] {strZZBH,
			                                    strBandNo,
                                                strWeight,
                                                Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName()).ToString()};
                CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ret.ReturnCode != 0)
                {
                    MessageBox.Show("数据保存失败！");
                    return;
                }
                else
                {
                    DialogResult dResult = MessageBox.Show(this,"数据保存成功！是否打印标牌？","提示",MessageBoxButtons.YesNo);
                    if (dResult == DialogResult.Yes)
                    {
                        QueryBCWeightDetailData(strZZBH, strBandNo);
                    }

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void QueryBCWeightDetailData(string strBatchNo,string strBandNo)
        {
            try
            {
                string strSql = "select decode(c.FS_PRINTWEIGHTTYPE,1,'True',0,'False') as FS_PRINTWEIGHTTYPE,decode(c.FS_ADDRESSCHECK,1,'True',0,'False') as FS_ADDRESSCHECK,decode(c.FS_STANDARDCHECK,1,'True',0,'False') as FS_STANDARDCHECK,decode(c.FS_STEELTYPECHECK,1,'True',0,'False') as FS_STEELTYPECHECK,a.FD_DATETIME,c.FS_PRINTTYPE,b.FS_SPEC,b.FS_STEELTYPE,a.FS_BATCHNO,a.FN_BANDNO,a.FN_BANDBILLETCOUNT,a.FS_TYPE,a.FN_LENGTH,to_char(Round(a.FN_WEIGHT,3),'FM999999.000') as FN_WEIGHT,TO_CHAR(Round(a.FN_THEORYWEIGHT,3),'FM999999.000') as FN_THEORYWEIGHT,a.FS_SHIFT,a.FS_TERM,a.FS_LABELID from dt_productweightdetail a,dt_productweightmain b,dt_productplan c where a.fs_batchno = b.fs_batchno and b.fs_batchno = c.fs_batchno and";

                strSql += " a.fs_batchno = '" + strBatchNo + "'";
                strSql += " and a.fn_bandno = '" + strBandNo+ "'";
                strSql += " order by a.fn_bandno,length(a.fn_bandno)";

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
                    if (dt_temp.Rows[0]["FS_ADDRESSCHECK"].ToString() == "" || dt_temp.Rows[0]["FS_STANDARDCHECK"].ToString() == "" || dt_temp.Rows[0]["FS_STEELTYPECHECK"].ToString() == "" || dt_temp.Rows[0]["FS_PRINTWEIGHTTYPE"].ToString() == "")
                    {
                        MessageBox.Show("打印信息配置不能为空，请检查！");
                        return;
                    }

                    if (_measApp.Printer != null && cbPrint.SelectedIndex >= 0)
                    {
                        _measApp.Printer.Data = new HgLable();
                        _measApp.Printer.Data.BatchNo = dt_temp.Rows[0]["FS_BATCHNO"].ToString();
                        _measApp.Printer.Data.BandNo = dt_temp.Rows[0]["FN_BANDNO"].ToString();
                        _measApp.Printer.Data.Type = LableType.PIPE;
                        _measApp.Printer.Data.Spec = dt_temp.Rows[0]["FS_SPEC"].ToString();
                        _measApp.Printer.Data.Weight = (Convert.ToInt32(dt_temp.Rows[0]["FN_WEIGHT"].ToString().Trim()) * 1000).ToString();  //重量
                        _measApp.Printer.Copies = 2;
                        _measApp.Printer.Data.Date = DateTime.Now;
                        string strTerm = string.Empty;
                        switch (dt_temp.Rows[0]["FS_SHIFT"].ToString())
                        {
                            case "1": strTerm = "A"; break;
                            case "2": strTerm = "B"; break;
                            case "3": strTerm = "C"; break;
                            case "4": strTerm = "D"; break;
                            default: strTerm = "A"; break;
                        }

                        _measApp.Printer.Data.Term = strTerm; //班别
                        _measApp.Printer.Data.BarCode = dt_temp.Rows[0]["FS_LABELID"].ToString();
                        _measApp.Printer.Data.PrintAddress = true; ;
                        _measApp.Printer.Data.Standard = "";
                        _measApp.Printer.Data.SteelType = dt_temp.Rows[0]["FS_STEELTYPE"].ToString();
                        _measApp.Printer.Data.Length = dt_temp.Rows[0]["FN_LENGTH"].ToString();
                        _measApp.Printer.Data.Count = dt_temp.Rows[0]["FN_BANDBILLETCOUNT"].ToString();
                        _measApp.Printer.Print();
                    }
                }
                else
                { }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        #endregion

        /// <summary>
        /// 打印-PrintForTwins
        /// </summary>
        public void PrintForTwins()
        {
        }

        /// <summary>
        /// 线程语音播放
        /// </summary>
        public void AutoAlarmVoice()
        {
            try
            {
                if (System.IO.File.Exists(m_AlarmVoicePath))
                {
                    if (_isTalk && _talkId > 0)
                    {
                        _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
                        _measApp.Dvr.SDK_StopTalk();
                        _talkId = 0;
                        _isTalk = false;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "语音对讲";
                    }
                }

                FileInfo fi = new FileInfo(m_AlarmVoicePath);
                int waveTimeLen = Convert.ToInt32((fi.Length - 54) / 16 + 500);
                _measApp.Dvr.SDK_SendData(m_AlarmVoicePath);
                Thread.Sleep(waveTimeLen);
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.Message);
            }
        }

        #region control event handler

        private void btnBC_Click(object sender, EventArgs e)
        {
            //button1_Click(null,null); return;
            SaveWeightData();
            this.cbx_fb.CheckState = CheckState.Unchecked;
            this.tb_bandno_fb.Text = "";
            this.tb_zzbh_fb.Text = "";
            this.tb_zzbh_fb.Enabled = false;
            this.tb_bandno_fb.Enabled = false;
            //this.txtJZ.Text = "";
        }

        /// <summary>
        /// 抓图并保存
        /// </summary>
        private void GraspAndSaveImage()
        {
            m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
            Invoke(m_MainThreadCapPicture); //用委托抓图
        }

        private bool CheckInput()
        {
            if (txtZZBH.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入轧制编号！");
                txtZZBH.Focus();
                return false;
            }
            //if (cbWLMC.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请输入物料名称！");
            //    cbWLMC.Focus();
            //    return false;
            //}
            if (cbGZ.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入钢种！");
                cbGZ.Focus();
                return false;
            }
            if (cbGG.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入规格！");
                cbGG.Focus();
                return false;
            }
            if(txtXSZL.Text.Trim().Length==0)
            {
                MessageBox.Show("仪表重量为零，请查看仪表是否连接上！");
                txtXSZL.Focus();
                return false;
            }

            return true;
        }

        private void Weight_BC_Load(object sender, EventArgs e)
        {
            //隐藏视频
            panel2.Width = 0;
            //隐藏计量点
            groupBox7.Width = 0;
            b.ob = this.ob;
            switch(CustomInfo)
            {
                case "K26":
                case "K27":
                case "K32":
                    strStoreCode = "SH000101";
                    m_preFlag = "TX";
                    m_DefaultSender = "制管直焊作业区";
                    m_DefaultReceiver = "小焊管成品库";
                    m_DefaultStandard = "GB/T13793-2008";
                    m_DefaultMeterial = "焊接钢管";
                    m_DefaultPM = "焊接钢管";
                    break;
                case "K33":
                case "K36":
                    strStoreCode = "SH000102";
                    m_preFlag = "TL";
                    m_DefaultSender = "制管螺焊作业区";
                    m_DefaultReceiver = "大焊管成品库";
                    m_DefaultStandard = "GB/T3091-2008";
                    m_DefaultMeterial = "螺管";
                    m_DefaultPM = "螺管";
                    break;
                case "K34":
                
                case "K37":
                    strStoreCode = "SH000102";
                    m_preFlag = "TZ";
                    m_DefaultSender = "制管直焊作业区";
                    m_DefaultReceiver = "大焊管成品库";
                    m_DefaultStandard = "GB/T13793-2008";
                    m_DefaultMeterial = "焊接钢管";
                    m_DefaultPM = "焊接钢管";
                    break;
            }

            bandSteelType(CustomInfo);
            bandPingMing(CustomInfo);
            bandGG(CustomInfo);
            bandLength(CustomInfo);
            bandPingMing(CustomInfo);
            bandStandard(CustomInfo);
           
            stRunPath = System.Environment.CurrentDirectory;

            if (Constant.RunPath == "")
            {

                Constant.RunPath = System.Environment.CurrentDirectory;
            }

            //查询计量点信息，并启动所有查得的计量点的后台采集工作，和汽车衡不一样的是不需要管理员分配，直接接管
            QueryJLDData();

            //初始化计量点
            WeighPoint wp = new WeighPoint(this.ob);
            string pointCode = "";
            pointCode = CustomInfo;
            m_Points = new BT_POINT[]{wp.GetPoint(pointCode)};
            if (m_Points != null)
            {
                m_nPointCount = m_Points.Length;
            }

            m_GetBaseInfo = new YGJZJL.PublicComponent.GetBaseInfo();
            m_GetBaseInfo.ob = this.ob;
            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadSteelType();  //下载磅房对应承运单位信息到内存表
            this.DownLoadSepc(); //下载磅房对应车号信息到内存表
            this.DownLoadFlow();  //下载流向信息

            //获得登录用户信息并显示到控件
            m_szCurUser = UserInfo.GetUserName();// CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName().ToString().Trim();
            m_szCurBC = UserInfo.GetUserOrder(); //Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            m_szCurBZ = UserInfo.GetUserGroup();// Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());

            txtJLY.Text = m_szCurUser;
            txtBC.Text = m_szCurBC;

            //控件初始化
            ClearControl();

            //控件事件和属性设置
            ControlInit();

            //获取语音信息
            GetYYBBData();

            //创建计量数据表的结构
            BuildWeightDataTable();

            m_hDataUpThread = new Thread(new System.Threading.ThreadStart(DoDataUp));
            m_hDataUpThread.Start();
            m_hRunning = true;


            //复磅
            this.tb_zzbh_fb.Enabled = false;
            this.tb_bandno_fb.Enabled = false;

            //手工录入
            this.cbx_Hand.CheckState = CheckState.Unchecked;
            this.tbx_hWeight.Enabled = false;

            this.txtBC.Text = GetOrderGroupName(OperationInfo.order, UserInfo.GetUserOrder());
            this.txtBZ.Text = GetOrderGroupName(OperationInfo.group, UserInfo.GetUserGroup());

            //初始化标准管理对象
            _weightStdManage = new WeightStdManage(this.ob);
            //_depotManage = new DepotManage();

            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                cbPrint.Items.Add(printerName);
            }


            //打开计量点
            ultraGrid2.Rows[0].Cells["XZ"].Value = true;            
            ultraGrid2.Rows[0].Activated = true;
            ultraGrid2.UpdateData();
            ultraGrid2_DoubleClickRow(ultraGrid2,new DoubleClickRowEventArgs(ultraGrid2.Rows[0],RowArea.Cell));

            if (!CustomInfo.Equals("K33") && !CustomInfo.Equals("K36"))
            {
                label36.Visible = false;
                label37.Visible = false;
                txtThick.Visible = false;
                cbxRemark.Visible = false;
            }
        }

        private void ultraGrid2_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (m_iSelectedPound == ultraGrid2.ActiveRow.Index)
                {
                    
                    return;
                }
                ultraGrid2.UpdateData();

                if (ultraGrid2.ActiveRow.Cells["XZ"].Text.ToUpper() == "FALSE")
                {
                    m_iSelectedPound = ultraGrid2.ActiveRow.Index;
                    MessageBox.Show("请选择已接管的计量点！");
                    return;
                }
                if (_measApp == null)
                {
                    MessageBox.Show("请先打开设备");
                    return;
                }
                //this.Cursor = Cursors.WaitCursor;
                //清空界面控件绑定
                ClearControl(); 
                //关闭前一个选择的计量点语音视频
                //RecordClose(m_iSelectedPound);

                int iSelectIndex = ultraGrid2.ActiveRow.Index; 
                m_iSelectedPound = iSelectIndex;
                //打开当前选择的计量点语音视频
                RecordOpen(iSelectIndex); 
                //查询计量点基础数据并绑定到控件
                QueryPoundBaseInfo(iSelectIndex); 

                if (_measApp.Params.FS_POINTCODE == "K17") //80万吨棒材1#材秤为A区
                {
                    this.strStoreCode = "SH000098";
                }
                else //80万吨棒材2#材秤为B区
                {
                    this.strStoreCode = "SH000099";
                }

                //查询预报数据
                QueryWeightDataByBatchNo();
                QueryLastStoveNo();
                //GetProductTheoryBaseInfo();

                //获取剩余纸张数和碳带数显示在界面
                //txtUsedPrintPaper.Text = _measApp.Params.FN_USEDPRINTPAPER;
                //txtUsedPrintTink.Text = _measApp.Params.FN_USEDPRINTINK;
                //this.Cursor = Cursors.Default;

            }
            catch (Exception exp)
            {
                WriteLog(exp.Message);
            }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "YYDJ":
                    HandleTalk(m_iSelectedPound);

                    break;
                case "CloseLED":
                    HandleLEDPower(m_iSelectedPound);
                    break;
                case "QueryPlan":
                    //清空界面控件数据绑定
                    //ClearControl();                    

                    //查询棒材预报信息
                    {
                        //QueryPlanData();
                    }
                    break;
                case "ChangePage":
                    {
                        ChangePaperEvent();
                        break;
                    }
                case "ChangeTink":
                    {
                        ChangeTinkEvent();
                        break;
                    }
                case "PrintBill":
                    { 
                        //此处为实现打印功能代码
                        PrintPoundBillEvent();
                        break;
                    }
                case "btCorrention":
                    {
                        pointId = _measApp.Params.FS_POINTCODE;
                        correntionWeight = txtXSZL.Text.Trim();//重量,字符串
                        correntionWeightNo = Guid.NewGuid().ToString();
                        baseinfo.ob = this.ob;
                        if (!baseinfo.correntionInformation(correntionWeightNo, pointId, txtJLY.Text.Trim(), txtBC.Text.Trim(), m_szCurBZ, correntionWeight))
                        {
                            return;
                        }
                        CorrentionSaveImage();
                        MessageBox.Show("校秤完成！！！");
                        break;
                    }
            }
        }
        //换纸-
        private void ChangePaperEvent()
        {
            if (m_iSelectedPound < 0)
            {
                MessageBox.Show("请先接管计量点");
                return;
            }
            _measApp.Params.FN_USEDPRINTPAPER = "200";
            txtUsedPrintPaper.Text = "200";
            
            //更新BT_POINT表中剩余纸张数
            m_GetBaseInfo.ob = this.ob;
            m_GetBaseInfo.SetZZData(PointID);
            
        }
        //换碳带
        private void ChangeTinkEvent()
        {
            if (m_iSelectedPound < 0)
            {
                MessageBox.Show("请先接管计量点");
                return;
            }
            //此处为实现换碳带功能
            _measApp.Params.FN_USEDPRINTINK = "200";
            txtUsedPrintTink.Text = "200";
            
            //更新BT_POINT表中剩余碳带数
            m_GetBaseInfo.SetTDData(PointID);
            
        }
        //打印-----未实现
        private void PrintPoundBillEvent()
        {
            //if (txtUsedPrintPaper.Text.Trim() == "" || Convert.ToInt32(txtUsedPrintPaper.Text.Trim()) < 1)
            //{
            //    MessageBox.Show("请换打印纸！");
            //    return;
            //}
            //if (txtUsedPrintTink.Text.Trim() == "" || Convert.ToInt32(txtUsedPrintTink.Text.Trim()) < 1)
            //{
            //    MessageBox.Show("请换碳带！");
            //    return;
            //}
            //此处为打印处理过程

            //打印一次剩余纸张数和碳带数减1
            _measApp.Params.FN_USEDPRINTPAPER = (int.Parse(_measApp.Params.FN_USEDPRINTPAPER) - 1).ToString();
            _measApp.Params.FN_USEDPRINTINK = (int.Parse(_measApp.Params.FN_USEDPRINTINK) - 1).ToString();
            txtUsedPrintPaper.Text = _measApp.Params.FN_USEDPRINTPAPER;
            txtUsedPrintTink.Text = _measApp.Params.FN_USEDPRINTINK;
            
            //更新BT_POINT表中剩余纸张数和碳带数
            m_GetBaseInfo.SubZZData(PointID);
            m_GetBaseInfo.SubTDData(PointID);
        }

        private void VideoChannel1_Click(object sender, EventArgs e)
        {
            //((Panel)(VideoChannel1.Parent)).BorderStyle = BorderStyle.FixedSingle;
            //k = 0;

            //((Panel)(VideoChannel2.Parent)).BorderStyle = BorderStyle.None;
        }

        private void VideoChannel2_Click(object sender, EventArgs e)
        {
            ((Panel)(VideoChannel2.Parent)).BorderStyle = BorderStyle.FixedSingle;
            k = 1;

            ((Panel)(VideoChannel1.Parent)).BorderStyle = BorderStyle.None;
        }

        private void VideoChannel1_DoubleClick(object sender, EventArgs e)
        {
            //Video1.Visible = true;
            //bool ret = false;
            //ret = _measApp.Cameras[0].StopRealPlay();
            //_measApp.Cameras[0].Logout();
            //_measApp.Cameras[0].Login();
            //ret = _measApp.Cameras[0].RealPlay(Video1.Handle);
            //for (int i = 0; i < 10 && !ret; i++)
            //{
            //    ret = _measApp.Cameras[0].RealPlay(Video1.Handle);
            //}

            //m_CurSelBigChannel = 0;
            if (_measApp.Cameras[0] == null) return;

            CloseBigPicture();
            OpenBigPicture(0);
        }

        private void VideoChannel2_DoubleClick(object sender, EventArgs e)
        {
            if (_measApp.Cameras[1] == null) return;
            CloseBigPicture();
            OpenBigPicture(1);
        }

        private void picFDTP_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
        }

        private void ultraGrid5_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (e.Cell.Column.Key.ToUpper() != "FS_VOICENAME" || e.Cell.Value.ToString().Length == 0)
            {
                return;
            }

            //this.Cursor = Cursors.WaitCursor;
            if (m_iSelectedPound < 0)
            {
                MessageBox.Show("请先选择一个计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }
            if (ultraGrid5.Rows.Count <= 0)
            {
                MessageBox.Show("还没有声音文件，请添加声音文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            int i = m_iSelectedPound;

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                this.Cursor = Cursors.Default;
                return;
            }

            //if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //{
            //    this.Cursor = Cursors.Default;
            //    return;
            //}

            if (_isTalk && _talkId > 0)
            {
                _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
                _measApp.Dvr.SDK_StopTalk();
                _talkId = 0;
                _isTalk = false;

                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }

            System.IO.FileInfo fi = new System.IO.FileInfo(Constant.RunPath + "\\rksound\\" + ultraGrid5.ActiveRow.Cells["FS_VOICENAME"].Value.ToString().Trim());
            int waveTimeLen = Convert.ToInt32((fi.Length - 54) / 16 + 500);

            _measApp.Dvr.SDK_SendData(Constant.RunPath + "\\rksound\\" + ultraGrid5.ActiveRow.Cells["FS_VOICENAME"].Value.ToString().Trim());
            System.Threading.Thread.Sleep(waveTimeLen);
            //this.Cursor = Cursors.Default;
        }

        private void chkAutoSave_Click(object sender, EventArgs e)
        {
            if (chkAutoSave.Checked)
            {
                btnBC.Enabled = false;
            }
            else
            {
                btnBC.Enabled = true;
            }
        }

        private void btnQL_Click(object sender, EventArgs e)
        {
            //HandleMeterData("2012");
            //this.txtXSZL.Text = "0";
            ////打印测试
            //if (_measApp.Printer.Data == null)
            //{
            //    _measApp.Printer.Data = new HgLable();
            //}
            //_measApp.Printer.PrinterName = new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName;
            //_measApp.Printer.Data = new HgLable();
            ////m_CoolLed.SendText("能看到不？", "宋体", 9, 1, 4, 0);
            //_measApp.Printer.Data.BatchNo = "P2041328";
            //_measApp.Printer.Data.BandNo = "13";
            //_measApp.Printer.Data.Date = DateTime.Now;


            ////_measApp.Printer.Data.Standard = "GB 1499.2-2007";
            //_measApp.Printer.Data.SteelType = "HRB335E";
            //_measApp.Printer.Data.Spec = "12";
            //_measApp.Printer.Data.Length = "12";

            //_measApp.Printer.Data.Weight = "2374";
            //_measApp.Printer.Data.Count = "223";

            ////_printer.Data.PrintStandard = "0";
            ////_printer.Data.PrintSteelType = "1";

            //_measApp.Printer.Data.Term = "1";
            //_measApp.Printer.Data.BarCode = GetBarCode(_measApp.Printer.Data.BatchNo,"13");
            //_measApp.Printer.Data.PrintAddress = true;
            //_measApp.Printer.Data.Type = LableType.BIG;
            //_measApp.Printer.Print();
            //_measApp.Printer.Print();
            //_measApp.Led.SendText("湖南视拓科技发展有限公司", "宋体", 12, 1, 4, 0);
            //_measApp.Dvr.SDK_SendData("D:\\计量完成1.wav");

            //iologic
            //_measApp.IoLogik.WriteDO(1);
            txtXSZL.Text = "0.000";
        }

        /// <summary>
        /// 完炉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWL_Click(object sender, EventArgs e)
        {
            //保存计量结果
            //SaveWeightData();
            //标记完炉
            string reVal = completeProduct(this.txtZZBH.Text.Trim());
            //if (reVal == "")
            //{
                ClearControl();
              //  return;
            //}
        }

        /// <summary>
        /// 人工完炉
        /// </summary>
        /// <returns></returns>
        private string completeProduct( string strBacthNo)
        { 
            string reVal = "";
            try
            {
                string p_FS_BATCHNO = strBacthNo;

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "completeBatchNo";
                ccp.ServerParams = new object[] { p_FS_BATCHNO };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnObject != null)
                {
                    reVal = ccp.ReturnObject.ToString();
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            return reVal;
        }

        /// <summary>
        /// 根据定单号查询物料、钢种、规格、定尺长度信息并绑定到窗体
        /// </summary>
        /// <param name="orderNo"></param>
        private void GetOrderInfo(string orderNo)
        {
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

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.SapOperation";
            ccp.MethodName = "queryProductNo";
            ccp.ServerParams = new object[] { orderNo };
            DataTable dt_temp = new DataTable();

            ccp.SourceDataTable = dt_temp;
            ccp.IfShowErrMsg = false;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (dt_temp.Rows.Count > 0)
                {
                    if (dt_temp.Rows[0]["FS_STEELTYPE"].ToString() != "")
                        this.cbGZ.Text = dt_temp.Rows[0]["FS_STEELTYPE"].ToString();
                    if (dt_temp.Rows[0]["FS_SPEC"].ToString() != "")
                        this.cbGG.Text = dt_temp.Rows[0]["FS_SPEC"].ToString();
                    if (dt_temp.Rows[0]["FN_LENGTH"].ToString() != "")
                        this.cbDCCD.Text = dt_temp.Rows[0]["FN_LENGTH"].ToString();
                    //if (dt_temp.Rows[0]["FS_ITEMNO"].ToString() != "")
                    //    this.txtPM.Text = dt_temp.Rows[0]["FS_ITEMNO"].ToString(); //订单项目号
                    if (dt_temp.Rows[0]["FS_MATERIALNAME"].ToString() != "")
                        this.cbWLMC.Text = dt_temp.Rows[0]["FS_MATERIALNAME"].ToString(); //物料名称
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("订单下载失败！");
            }

            this.Cursor = Cursors.Default;
            Constant.WaitingForm.ShowToUser = false;
            Constant.WaitingForm.Close();
        }

        #endregion        

        private void Weight_BC_KeyPress(object sender, KeyPressEventArgs e)
        {
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
        }
        //打开选定设备采集数据
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                btnOpen.Focus();
                ultraGrid2.UpdateData();
                if (dataTable1 != null && dataTable1.Rows != null && dataTable1.Rows.Count > 0)
                {
                    ultraGrid2.Rows[0].Activated = false;
                    m_iSelectedPound = -1;
                    //InitPound(dataTable1);
                    BeginPoundRoomThread();
                    MessageBox.Show("打开设备完成,请选择需要操作的计量点！", "提示", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show("没有选择计量点！", "提示", MessageBoxButtons.OK);
                }
            }
            catch (Exception exp)
            {
                WriteLog(exp.Message);
            }
        }

        private void MainThreadCapPicture()
        {
            //m_GraspImageSign = 1; //如果为1，则下次不能再开启线程
            int i = m_iSelectedPound;

            string strNumber = _measApp.Params.FS_POINTCODE;
            string fileName1 = strNumber + "1.bmp";
            string fileName2 = strNumber + "2.bmp";

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            //if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //{
            //    return;
            //}

            //抓第一张图
            try
            {
                _measApp.Cameras[0].CapturePicture(0, stRunPath + "\\pic\\" + fileName1);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                _measApp.Cameras[1].CapturePicture(0, stRunPath + "\\pic\\" + fileName2);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string strZL = "";
            if (_strBandNo != "")
            {
                strZL = _strBatchNo + "-" + this._strBandNo + " " + this.txtZL.Text.Trim() + " 吨";

            }
            else
            {
                strZL = this.txtZL.Text.Trim() + " 吨";

            }
            
            b.GraspAndSaveRKImage(fileName1, fileName2, WeightNo, strZL);
        }

        private void Weight_BC_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_hRunning = false;
            if (_measApp != null)
            {
                _measApp.Finit();
            }
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtDDH_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == 13)
            {
                if (this.txtDDH.Text.Trim() != "")
                {
                    GetOrderInfo(this.txtDDH.Text.Trim());
                }
            }
        }

        private void cbx_fb_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.cbx_fb.CheckState == CheckState.Checked)
            {
                this.tb_bandno_fb.Text = "";
                this.tb_zzbh_fb.Text = "";
                this.tb_zzbh_fb.Enabled = true;
                this.tb_bandno_fb.Enabled = true;
            }
            else
            {
                this.tb_bandno_fb.Text = "";
                this.tb_zzbh_fb.Text = "";
                this.tb_zzbh_fb.Enabled = false;
                this.tb_bandno_fb.Enabled = false;
            }

        }

        /// <summary>
        /// 查询最近一次完炉的累计数据
        /// </summary>
        private void QueryLastStoveNo()
        {
            DataTable dt = new DataTable();
            dt.Clear();

            string sql = "select FS_BATCHNO,FN_BANDCOUNT,TO_CHAR(FN_TOTALWEIGHT,'FM999999.000') AS FN_TOTALWEIGHT,TO_CHAR(ROUND(FN_THEORYTOTALWEIGHT,3),'FM999999.000') AS FN_THEORYTOTALWEIGHT FROM DT_PIPEWEIGHTMAIN WHERE FS_BATCHNO IN (SELECT MAX(FS_BATCHNO) FROM DT_PIPEWEIGHTMAIN WHERE FS_COMPLETEFLAG = '1' and FS_BATCHNO like 'N%')";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                this.tbx_lastbandcount.Text = dt.Rows[0]["FN_BANDCOUNT"].ToString();
                this.tbx_lastbatch.Text = dt.Rows[0]["FS_BATCHNO"].ToString();
                this.tbx_lasttotalweight.Text = dt.Rows[0]["FN_TOTALWEIGHT"].ToString();
                this.tbx_theoryweight.Text = dt.Rows[0]["FN_THEORYTOTALWEIGHT"].ToString();
            }
        }

        private void cbx_Hand_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbx_Hand_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbx_Hand.CheckState == CheckState.Checked)
            {
                this.tbx_hWeight.Enabled = true;
                this.btnBC.Enabled = true;
                this.chkAutoSave.Checked = false;
            }
            else
            {
                this.tbx_hWeight.Enabled = false;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string args = "weight";
            ChangeShift winfrm = new ChangeShift(this.ob, args);
            winfrm.Owner = this;
            winfrm.StartPosition = FormStartPosition.CenterParent;

            if (winfrm.ShowDialog() == DialogResult.OK)
            {
                UserInfo.SetUserName(winfrm.strreturnname);
                UserInfo.SetUserOrder(winfrm.strreturnshife);
                UserInfo.SetUserGroup(winfrm.strreturnterm);
                p_shiftdate = winfrm.strreturndate;

                this.txtBC.Text = GetOrderGroupName(OperationInfo.order, UserInfo.GetUserOrder());
                this.txtBZ.Text = GetOrderGroupName(OperationInfo.group, UserInfo.GetUserGroup());
                this.txtJLY.Text =UserInfo.GetUserName();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            this.m_szCurUser = UserInfo.GetUserName();
            this.strShiftDate = p_shiftdate;
            this.m_szCurBC = UserInfo.GetUserOrder();
            this.m_szCurBZ = UserInfo.GetUserGroup();

            this.txtBC.Text = m_szCurBC;
           // this.txtBB.Text = strTerm;
            this.txtJLY.Text = m_szCurUser.Trim();
        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 校秤抓图并保存
        /// </summary>
        private void CorrentionSaveImage()
        {
            m_MainThreadCorrentionPicture = new CorrentionPicture(MainThreadCorrentionPicture);
            Invoke(m_MainThreadCorrentionPicture); //用委托抓图
        }


        /// <summary>
        /// 校秤截图处理
        /// </summary>
        private void MainThreadCorrentionPicture()
        {
            //m_GraspImageSign = 1; //如果为1，则下次不能再开启线程
            int i = m_iSelectedPound;

            string strNumber = _measApp.Params.FS_POINTCODE;
            string fileName1 = strNumber + "corrention1.bmp";

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            //if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //{
            //    return;
            //}

            //抓第一张图
            try
            {
                _measApp.Dvr.SDK_CapturePicture(_measApp.VideoChannel[0], stRunPath + "\\JZPicture\\" + fileName1);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            //strZYBH = Guid.NewGuid().ToString().Trim();
            string strZL = this.txtZL.Text.Trim() + " 吨";
            
            b.GraspAndSaveCorrentionImage(fileName1, correntionWeightNo, correntionWeight);
        }

        #region 获取条码
        /// <summary>
        /// 条码编码数据 402 + 轧编号 + 吊号
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="batchIndex"></param>
        /// <returns></returns>
        private string GetBarCode(string batchNo, string batchIndex)
        {
            if (string.IsNullOrEmpty(batchNo) || string.IsNullOrEmpty(batchIndex))
            {
                return null;
            }
            batchNo = batchNo.Trim().Insert(1,System.DateTime.Now.Year.ToString().Substring(2,1));
            batchIndex = batchIndex.Trim();
            if (batchIndex.Length == 1)
            {
                batchIndex = "0" + batchIndex;
            }
            else if (batchIndex.Length == 0)
            {
                batchIndex = "01";
            }
            return "402" + batchNo.Trim() + batchIndex;
        }
        #endregion

        #region 班次班组
        private enum OperationInfo : byte
        {
            order,group
        }

        private string GetOrderGroupName(OperationInfo opInfo, string value)
        {
            string str = "";

            if (opInfo == OperationInfo.order)
            {
                switch (value)
                {
                    case "0":
                        str = "常白";
                        break;
                    case "1":
                        str = "早";
                        break;
                    case "2":
                        str = "中";
                        break;
                    case "3":
                        str = "晚";
                        break;
                }
            }
            else if (opInfo == OperationInfo.group)
            {
                switch (value)
                {
                    case "0":
                        str = "常白";
                        break;
                    case "1":
                        str = "甲";
                        break;
                    case "2":
                        str = "乙";
                        break;
                    case "3":
                        str = "丙";
                        break;
                    case "4":
                        str = "丁";
                        break;
                }
            }

            return str;
        }
        #endregion

        #region 拖动视频窗口
        private bool _mousedown = false;
        private int _moveLeft = 0;
        private int _moveTop = 0;
        private void picFDTP_MouseDown(object sender, MouseEventArgs e)
        {
            _mousedown = true;
            _moveLeft = e.X;
            _moveTop = e.Y;
        }

        private void picFDTP_MouseLeave(object sender, EventArgs e)
        {
            _mousedown = false;
        }

        private void picFDTP_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mousedown)
            {
                picFDTP.Left += e.X - _moveLeft;
                picFDTP.Top += e.Y - _moveTop;
                _moveLeft = e.X;
                _moveTop = e.Y;
            }
        }

        private void picFDTP_MouseUp(object sender, MouseEventArgs e)
        {
            _mousedown = false;
        }
        #endregion

        private void chkAutoSave_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkAutoSave.CheckState == CheckState.Checked)
            {
                this.cbx_Hand.Checked = false;
            }
        }

        private string getNextBatchNo(string strFlag)
        {
            string strResult = string.Empty;
            string strSQL = "select " + "'" + strFlag + "'" + "||substr(TO_CHAR(SYSDATE,'YYYYMM'),4,3)||LPAD(NVL(TO_NUMBER(SUBSTR(MAX(fs_batchno),6,3)),0)+1,3,'0') as fs_batchno from dt_pipeweightmain ";
            strSQL += "where fs_batchno like " + "'" + strFlag + "'" + "||substr(TO_CHAR(SYSDATE,'YYYYMM'),4,3)||'%'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            DataTable dt = new DataTable();
            ccp.ServerParams = new object[] { strSQL };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                strResult = dt.Rows[0]["fs_batchno"].ToString();
            }

            return strResult;
        }

        private string getNextBandNo(string strBatchNo)
        {
            string strResult = string.Empty;
            string strSQL = "select LPAD(NVL(TO_NUMBER(MAX(fn_bandno)),0)+1,2,'0') as fs_bandno from dt_pipeweightdetail where fs_batchno='" + strBatchNo + "'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            DataTable dt = new DataTable();
            ccp.ServerParams = new object[] { strSQL };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                strResult = dt.Rows[0]["fs_bandno"].ToString();
            }

            return strResult;
        }

        private void btnChangStove_Click(object sender, EventArgs e)
        {
            this.txtZZBH.Text = getNextBatchNo(m_preFlag);
            this.txtBandNo.Text = getNextBandNo(txtZZBH.Text);
            QueryLastStoveNo();
        }

        private void bandSteelType(string strPointID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            switch (strPointID)
            {
                case "K26":
                case "K27":
                case "K34":
                case "K32":
                case "K33":
                case "K36":
                case "K37":
                    dt.Rows.Add("0", "Q195");
                    dt.Rows.Add("1", "Q235");
                    dt.Rows.Add("2", "Q235A");
                    dt.Rows.Add("3", "Q235B");
                    break;
                    break;
            }
            cbGZ.DataSource = dt;
            cbGZ.ValueMember = "Value";
            cbGZ.DisplayMember = "Text";
        }

        private void bandGG(string strPointID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            switch (strPointID)
            {
                case "K26":
                case "K27":
                    {
                        dt.Rows.Add("0", "Φ22×2.2×客户定尺");
                        dt.Rows.Add("1", "Φ33×3.25×6000");
                        dt.Rows.Add("2", "Φ48×2.0×6000");
                        dt.Rows.Add("3", "Φ48×2.25×6000");
                        dt.Rows.Add("4", "Φ48×2.5×6000");
                        dt.Rows.Add("5", "Φ48×2.75×6000");
                        dt.Rows.Add("6", "Φ48×3.0×6000");
                        dt.Rows.Add("7", "Φ48×3.25×6000");
                        dt.Rows.Add("8", "Φ48×3.5×6000");
                    } break;
                case "K34":
                case "K32":
                case "K33":
                case "K36":
                case "K37":
                    dt.Rows.Add("0", "Φ114×2.5×6000");
                    dt.Rows.Add("1", "Φ140×3.75×6000");
                    dt.Rows.Add("2", "Φ140×3.75×客户定尺");
                    dt.Rows.Add("3", "Φ15×2.0×6000");
                    dt.Rows.Add("4", "Φ20×2.0×6000");
                    dt.Rows.Add("5", "Φ219×3.0×6000");
                    dt.Rows.Add("6", "Φ26.9×2.25×6000");
                    dt.Rows.Add("7", "Φ33×3.25×6000");
                    dt.Rows.Add("8", "Φ48×2.75×6000");
                    dt.Rows.Add("9", "Φ76×2.0×6000");
                    dt.Rows.Add("10", "Φ48×2.75×6000");
                    dt.Rows.Add("11", "Φ89×2.5×6000");
                    break;

            }
            cbGG.DataSource = dt;
            cbGG.ValueMember = "Value";
            cbGG.DisplayMember = "Text";
        }

        private void bandPingMing(string strPointID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            switch (strPointID)
            {
                case "K26":
                case "K27":
                    {
                        dt.Rows.Add("0", "焊接钢管 Q235B Φ48×3.0×6000");
                        dt.Rows.Add("1", "焊接钢管 Q235B Φ48×3.25×6000");
                        dt.Rows.Add("2", "焊接钢管 Q235B Φ48×2.75×6000");
                        dt.Rows.Add("3", "焊接钢管 Q235B Φ48×3.5×6000");
                        dt.Rows.Add("4", "焊接钢管 Q235B Φ48×2.5×6000");
                        dt.Rows.Add("5", "焊接钢管 Q235B Φ60×2.5×6000");
                        dt.Rows.Add("6", "焊接钢管 Q235B Φ60×2.75×6000");
                        dt.Rows.Add("7", "焊接钢管 Q235B Φ60×3.0×6000");
                        dt.Rows.Add("8", "焊接钢管 Q235B Φ60×3.25×6000");
                        dt.Rows.Add("9", "焊接钢管 Q235B Φ60×3.5×6000");
                        dt.Rows.Add("10", "焊接钢管 Q235B Φ48×3.0×客户定尺");
                        dt.Rows.Add("11", "焊接钢管 Q235B Φ48×3.25×客户定尺");
                        dt.Rows.Add("12", "焊接钢管 Q235B Φ33×2.5×6000");
                        dt.Rows.Add("13", "焊接钢管 Q235B Φ26.9×2.5×6000");
                        dt.Rows.Add("14", "焊接钢管 Q235B Φ33×2.75×6000");
                        dt.Rows.Add("15", "焊接钢管 Q235B Φ22×2.2×客户定尺");
                        dt.Rows.Add("16", "焊接钢管 Q235B Φ60×3.5×客户定尺");
                        dt.Rows.Add("17", "焊接钢管 Q235B Φ33×3.0×6000");
                        dt.Rows.Add("18", "焊接钢管 Q235B Φ33×3.25×6000");
                        dt.Rows.Add("19", "焊接钢管 Q235B Φ48×2.0×6000");
                        dt.Rows.Add("20", "焊接钢管 Q235B Φ48×2.25×6000");
                        dt.Rows.Add("21", "焊接钢管 Q235B Φ33×2.0×6000");
                        dt.Rows.Add("22", "焊接钢管 Q235B Φ33×2.25×6000");
                        dt.Rows.Add("23", "焊接钢管 Q235B Φ48×2.75×客户定尺");
                        dt.Rows.Add("24", "焊接钢管 Q235B Φ22×2.5×客户定尺");
                        dt.Rows.Add("25", "焊接钢管 Q235B Φ22×2.5×6000");
                        dt.Rows.Add("26", "焊接钢管 Q235B Φ22×2.75×6000");
                        dt.Rows.Add("27", "焊接钢管 Q235B Φ22×3.0×6000");
                        dt.Rows.Add("28", "一作业区搞震带肋钢筋 HRB400E Φ12×9000");
                        dt.Rows.Add("29", "煤气管 Q235B Φ325×8×12000");
                        dt.Rows.Add("30", "焊接钢管 Q235B Φ21×2.2×6000");
                        dt.Rows.Add("31", "焊接钢管 Q235B Φ21×2.2×客户定尺");
                        dt.Rows.Add("32", "焊接钢管 Q235B Φ21×2.5×6000");
                        dt.Rows.Add("33", "焊接钢管 Q235B Φ76×2.0×6000");
                        dt.Rows.Add("34", "焊接钢管 Q235B Φ76×2.5×6000");
                        dt.Rows.Add("35", "焊接钢管 Q235B Φ76×3.0×6000");
                        dt.Rows.Add("36", "焊接钢管 Q235B Φ76×3.25×6000");
                        dt.Rows.Add("37", "焊接钢管 Q235B Φ76×3.25×客户定尺");
                        dt.Rows.Add("38", "焊接钢管 Q235B Φ76×3.5×6000");
                        dt.Rows.Add("39", "焊接钢管 Q235B Φ76×3.75×6000");
                        dt.Rows.Add("40", "焊接钢管 Q235B Φ76×4.0×6000");
                        dt.Rows.Add("41", "焊接钢管 Q235B Φ76×4.5×6000");
                        dt.Rows.Add("42", "焊接钢管 Q235B Φ76×4.75×6000");
                        dt.Rows.Add("43", "焊接钢管 Q235B Φ76×5.0×6000");
                        dt.Rows.Add("44", "焊接钢管 Q235B Φ89×5.5×6000");
                        dt.Rows.Add("45", "焊接钢管 Q235B Φ114×4.25×6000");
                        dt.Rows.Add("46", "焊接钢管 Q235B Φ165×3.0×6000");
                        dt.Rows.Add("47", "彩涂卷 TDC51D+Z 0.40≤H＜0.45");
                        dt.Rows.Add("48", "切边钢带 Q235B 3.25*146");
                        dt.Rows.Add("49", "切边钢带 Q235B 3.5*146");
                    } break;
                case "K33":
                case "K36":
                    {
                        dt.Rows.Add("0", "螺旋焊管 Q235B Φ820×10×6000");
                        dt.Rows.Add("1", "螺旋焊管 Q235B Φ820×12×6000");
                        dt.Rows.Add("2", "螺旋焊管 Q235B Φ820×13×6000");
                        dt.Rows.Add("3", "螺旋焊管 Q235B Φ820×11.5×6000");
                        break;
                    }
                case "K34":
                case "K32":
                
                case "K37":
                    dt.Rows.Add("0", "焊接钢管 Q235B Φ114×3.0×6000");
                    dt.Rows.Add("1", "焊接钢管 Q235B Φ114×3.25×6000");
                    dt.Rows.Add("2", "焊接钢管 Q235B Φ114×2.75×6000");
                    dt.Rows.Add("3", "焊接钢管 Q235B Φ114×3.5×6000");
                    dt.Rows.Add("4", "焊接钢管 Q235B Φ114×2.5×6000");
                    dt.Rows.Add("5", "焊接钢管 Q235B Φ219×2.5×6000");
                    dt.Rows.Add("6", "焊接钢管 Q235B Φ219×2.75×6000");
                    dt.Rows.Add("7", "焊接钢管 Q235B Φ219×3.0×6000");
                    dt.Rows.Add("8", "焊接钢管 Q235B Φ219×3.25×6000");
                    dt.Rows.Add("9", "焊接钢管 Q235B Φ60×3.5×6000");
                    dt.Rows.Add("10", "焊接钢管 Q235B Φ48×3.0×客户定尺");
                    dt.Rows.Add("11", "焊接钢管 Q235B Φ48×3.25×客户定尺");
                    dt.Rows.Add("12", "焊接钢管 Q235B Φ33×2.5×6000");
                    dt.Rows.Add("13", "焊接钢管 Q235B Φ26.9×2.5×6000");
                    dt.Rows.Add("14", "焊接钢管 Q235B Φ33×2.75×6000");
                    dt.Rows.Add("15", "焊接钢管 Q235B Φ22×2.2×客户定尺");
                    dt.Rows.Add("16", "焊接钢管 Q235B Φ60×3.5×客户定尺");
                    dt.Rows.Add("17", "焊接钢管 Q235B Φ33×3.0×6000");
                    dt.Rows.Add("18", "焊接钢管 Q235B Φ33×3.25×6000");
                    dt.Rows.Add("19", "焊接钢管 Q235B Φ48×2.0×6000");
                    dt.Rows.Add("20", "焊接钢管 Q235B Φ48×2.25×6000");
                    dt.Rows.Add("21", "焊接钢管 Q235B Φ33×2.0×6000");
                    dt.Rows.Add("22", "焊接钢管 Q235B Φ33×2.25×6000");
                    dt.Rows.Add("23", "焊接钢管 Q235B Φ48×2.75×客户定尺");
                    dt.Rows.Add("24", "焊接钢管 Q235B Φ22×2.5×客户定尺");
                    dt.Rows.Add("25", "焊接钢管 Q235B Φ22×2.5×6000");
                    dt.Rows.Add("26", "焊接钢管 Q235B Φ22×2.75×6000");
                    dt.Rows.Add("27", "焊接钢管 Q235B Φ22×3.0×6000");
                    dt.Rows.Add("28", "一作业区搞震带肋钢筋 HRB400E Φ12×9000");
                    dt.Rows.Add("29", "煤气管 Q235B Φ325×8×12000");
                    dt.Rows.Add("30", "焊接钢管 Q235B Φ21×2.2×6000");
                    dt.Rows.Add("31", "焊接钢管 Q235B Φ21×2.2×客户定尺");
                    dt.Rows.Add("32", "焊接钢管 Q235B Φ21×2.5×6000");
                    dt.Rows.Add("33", "焊接钢管 Q235B Φ76×2.0×6000");
                    dt.Rows.Add("34", "焊接钢管 Q235B Φ76×2.5×6000");
                    dt.Rows.Add("35", "焊接钢管 Q235B Φ76×3.0×6000");
                    dt.Rows.Add("36", "焊接钢管 Q235B Φ76×3.25×6000");
                    dt.Rows.Add("37", "焊接钢管 Q235B Φ76×3.25×客户定尺");
                    dt.Rows.Add("38", "焊接钢管 Q235B Φ76×3.5×6000");
                    dt.Rows.Add("39", "焊接钢管 Q235B Φ76×3.75×6000");
                    dt.Rows.Add("40", "焊接钢管 Q235B Φ76×4.0×6000");
                    dt.Rows.Add("41", "焊接钢管 Q235B Φ76×4.5×6000");
                    dt.Rows.Add("42", "焊接钢管 Q235B Φ76×4.75×6000");
                    dt.Rows.Add("43", "焊接钢管 Q235B Φ76×5.0×6000");
                    dt.Rows.Add("44", "焊接钢管 Q235B Φ89×5.5×6000");
                    dt.Rows.Add("45", "焊接钢管 Q235B Φ114×4.25×6000");
                    dt.Rows.Add("46", "焊接钢管 Q235B Φ165×3.0×6000");
                    dt.Rows.Add("47", "彩涂卷 TDC51D+Z 0.40≤H＜0.45");
                    dt.Rows.Add("48", "切边钢带 Q235B 3.25*146");
                    dt.Rows.Add("49", "切边钢带 Q235B 3.5*146");
                    break;
            }
            this.cbx_PM.DataSource = dt;
            cbx_PM.ValueMember = "Value";
            cbx_PM.DisplayMember = "Text";
        }

        private void bandStandard(string strPointID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            switch (strPointID)
            {
                case "K26":
                case "K27":
                    {
                        dt.Rows.Add("0", "GB/T13793-2008");
                    } break;
                case "K34":
                case "K32":
                case "K37":
                    dt.Rows.Add("0", "GB/T13793-2008");
                    dt.Rows.Add("1", "GB/T3091-2008");
                    dt.Rows.Add("2", "GB/T6725-2008");
                    break;
                case "K33":
                case "K36":
                    dt.Rows.Add("0", "GB/T3091-2008");
                    dt.Rows.Add("1", "SY/T5037-2000");
                    dt.Rows.Add("2", "GB/T9711.1-1997");
                    break;
            }
            this.cbx_Standard.DataSource = dt;
            cbx_Standard.ValueMember = "Value";
            cbx_Standard.DisplayMember = "Text";
        }

        private void bandLength(string strPointID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            switch (strPointID)
            {
                case "K26":
                case "K27":
                    {
                        dt.Rows.Add("0", "6000");
                        dt.Rows.Add("1", "12000");
                    } break;
                case "K34":
                case "K32":
                case "K37":
                case "K33":
                case "K36":
                    dt.Rows.Add("0", "6000");
                    dt.Rows.Add("1", "7000");
                    dt.Rows.Add("2", "7500");
                    dt.Rows.Add("3", "9000");
                    dt.Rows.Add("4", "12000");
                    break;
            }
            cbDCCD.DataSource = dt;
            cbDCCD.ValueMember = "Value";
            cbDCCD.DisplayMember = "Text";
        }

        /// <summary>
        /// 执行存储过程2
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
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

        private void txtQTYKL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int kc = (int)e.KeyChar;
                if (kc != 8)
                {
                    if (kc < 47 || kc > 58)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void txtQTYKL_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtZL.Text != string.Empty)
            {
                try
                {
                    if (txtQTYKL.Text != string.Empty)
                    {
                        txtJZ.Text = Convert.ToString(((double)Math.Round((double.Parse(this.txtZL.Text.ToString().Trim()) * 1000)) - double.Parse(txtQTYKL.Text)) / 1000);
                    }
                    else
                    {
                        txtJZ.Text = txtZL.Text;
                    }
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_measApp == null || _measApp.Printer == null) return;
            if (cbPrint.SelectedIndex < 0)
            {
                MessageBox.Show("请选择打印机！");
                return;
            }

            _measApp.Printer.PrinterName = cbPrint.Text;
            switch (CustomInfo)
            { 
                case "K36":    _measApp.Printer.Print();
                    break;
                default :
                    _measApp.Printer.Print();
                    _measApp.Printer.Print();
                    break;
            }
            
        }

        private void tbRKDH_TextChanged(object sender, EventArgs e)
        {
            tbRKDH.Text = tbRKDH.Text.ToUpper();
            tbRKDH.SelectionStart = tbRKDH.Text.Length;
        }

        private void txtZS_Leave(object sender, EventArgs e)
        {
            Calc720Weight();
        }

        private void cbDCCD_Leave(object sender, EventArgs e)
        {
            Calc720Weight();
        }

        /// <summary>
        /// 计算720机组理论重量
        /// </summary>
        private void Calc720Weight()
        {
            if (!CustomInfo.Equals("K33") && !CustomInfo.Equals("K36")) return;
            double weight = 0,theoryWeight = 0;
            string[] sizeArray = cbGG.Text.Trim().Split(new char[]{'×'});
            string lenghtStr = cbDCCD.Text.Trim();
            string countStr = txtZS.Text.Trim();
            double diameter = 0, thick = 0, lenght = 0, count = 0,thick1 = 0;
            if (sizeArray.Length > 2 && double.TryParse(sizeArray[0].Substring(1), out diameter)
                && double.TryParse(sizeArray[1], out thick) && double.TryParse(lenghtStr, out lenght)
                && double.TryParse(countStr, out count))
            {
                theoryWeight = Math.Round((diameter - thick) * thick * lenght * 0.0246615 / Math.Pow(1000, 2), 3);
                
                if(double.TryParse(txtThick.Text.Trim(),out thick1))
                {
                    weight = Math.Round((diameter - thick1) * thick1 * lenght * 0.0246615 / Math.Pow(1000, 2), 3);
                }
            }
            txtJZ.Text = theoryWeight.ToString();
            tbx_hWeight.Text = weight.ToString();
        }

        private void txtThick_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtThick_Leave(object sender, EventArgs e)
        {
            Calc720Weight();
        }
    }
}