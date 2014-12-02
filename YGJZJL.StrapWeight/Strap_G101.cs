using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;
using SerialCommlib;
using System.Threading;
using SDK_Com;
//using SDK_CLib;
using System.Media;
using System.Runtime.InteropServices;
using Core.Sip.Client.App;
using Core.Sip.Client.Meas;

namespace YGJZJL.StrapWeight
{
    public partial class Strap_G101 : FrmBase
    {
        #region 参数定义
        private uint hRealHandle = 0;
        private uint hDeviceHandle = 0;
        private const string BR = "\r\n";
      //  SDK_Com.HKDVR sdk;
        int loghandle;
        int relhandle;
        string strRunPath = "";
        GetBaseInfo a;
        BaseInfo b;
        public string PointID = "";//计量点编码
        public string valueWL = "";
        public string valueFH = "";
        public string valueSH = "";
        public string FS_WL = "";//物料编码
        public string FS_FH = "";//发货方编码
        public string FS_SH = "";//收货方编码
        public string WeightNo = "";//作业编号
        public string Flow = "";//流向
        public string StrapNo = "";//皮带号
        public string ContractNo="";//合同号
        public string ItemNo = "";//项目号
        public string Shift = "";//班次
        public string Term = "";//班别
        public string Person = "";//计量员
        public string Stime = "";//开始时间
        public string Etime = "";//结束时间
        public double Gweight = 0;//总重
        public double Nweight = 0;//净重
        public double Tweight = 0;//扣重
        public double Sweight = 0;//始重
        public int HopperNum;//斗数
        public string Systime = "";//服务器时间
        public string flag="";
        public string ifclosed = "";
        public int factor = 100; //扣重系数
        public string userid;//权限ID
        System.Data.DataTable dtHTH = new System.Data.DataTable();
        //仪表读数参数
        public string iPort = "COM4"; //1,2,3,4
        public int iRate = 2400; //1200,2400,4800,9600
        public byte bSize = 8; //8 bits
        public string bParity = "E"; // 0-4=no,odd,even,mark,space 
        public byte bStopBits = 1; // 0,1,2 = 1, 1.5, 2 
        public int iTimeout = 10;
        public string bWeightType = "";
        //IP摄像头参数
        public string vip = "";
        public short vport = 0;
        public string vpassword = "";
        public string vuser = "";
        private SerialsCFC110Lib serialsCFC110Lib;
        public SerialCommlib.SerialCommlib mycom1 = new SerialCommlib.SerialCommlib();
        public SoundPlayer m_SoundPlayer;//播放声音
        public double m_dPreData = 0;//保存上次实时重量
        public string m_State = "0";  //状态 0：第一次进入程序 1：保存时占用 2：空闲

        Core.Sip.Client.App.CoreApp app = null;



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
        #endregion





        #region 窗体非按钮事件集
        public Strap_G101()
        {
            InitializeComponent();
            strRunPath = System.Environment.CurrentDirectory;
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
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.Fs_Materialno = B.Fs_Wl and C.Fs_Pointtype = 'PD' ";
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
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_Receiver = B.Fs_SH and C.Fs_Pointtype = 'PD' ";
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
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_SUPPLIER = B.Fs_GY and C.Fs_Pointtype = 'PD' ";
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
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_PROVIDER = B.FS_SP and C.Fs_Pointtype = 'PD' ";
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
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_TransNo = B.Fs_CY and C.Fs_Pointtype = 'PD' ";
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
        //窗体载入事件
        private void Strap_Load(object sender, EventArgs e)
        {
            if (Constant.RunPath == "")
            {
                Constant.RunPath = System.Environment.CurrentDirectory;
            }
          //  app = new Core.Sip.Client.App.CoreApp();
          //  app.Params = new Core.Sip.Client.App.BT_POINT();
          //  app.Params.FS_VIEDOIP = "10.32.18.100,4520,admin,admin";


          //  Core.Sip.Client.Meas.SsIpCamera camer = new Core.Sip.Client.Meas.SsIpCamera();

          //bool ttt=  camer.Init("10.32.18.100,4520,admin,admin");
          //ttt = camer.Login();
          //ttt = camer.Open();
            //string[] strParams = new string[5];
            //strParams = app.Params.FS_VIEDOIP.Split(new char[] { ',' });
          //  app.Cameras[0].Init("10.32.18.100,4520,admin,admin");
          //  app.Cameras[0].Login();
          //bool tt=  app.Cameras[0].RealPlay(pictureBox1.Handle);
            //VideoChannel1
                



            m_State = "0"; //第一次进入程序
            a = new GetBaseInfo();
            b = new BaseInfo();
            PointID = "K35";
            QueryPointBaseInfo();

            DownLoadFlow();
            //InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadTrans();  //下载磅房对应承运单位信息到内存表

            string strPointID = PointID;
            this.BandPointMaterial(strPointID);
            this.BandPointMaterial(strPointID); //绑定磅房物料
            this.BandPointReceiver(strPointID); //绑定磅房收货单位
            this.BandPointSender(strPointID); //绑定磅房发货单位
            //this.tbJLD.Text = "G101皮带秤";
            this.cbPDH.Text = "0";
            this.tbKZ.Text = "0.0";
            this.tbJLY.Text = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
           
            this.tbBC.Text = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            this.tbBB.Text = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
            //QueryAndBindComboxes();
            BindFirstData();
            OpenYB();
            GetIPVedio();
            //GetVedio();
        }
        //回车事件
        private void Strap_KeyPress(object sender, KeyPressEventArgs e)
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
        //合同号光标离开
        private void tbHTH_Leave(object sender, EventArgs e)
        {
            if (tbHTH.Text.Trim().Length == 10 && !isNumber(tbHTH.Text.Trim()))
            {
                DownSapData();
            }
            else
            {
                if (ifclosed != "true" && tbHTH.Text.Trim().Length != 0)
                {
                    MessageBox.Show("请正确输入10位合同号！");
                    this.tbHTH.Focus();
                    return;
                }
            }
        }
        //项目号文本框输入
        private void cbXMH_TextChanged(object sender, EventArgs e)
        {
            int i = cbXMH.Text.IndexOf(cbXMH.Text);
            if (flag != "")
            {
                cbWLMC.Text = dtHTH.Rows[i]["FS_MATERIALNAME"].ToString();
            }
        }
        //物料下拉框选择事件
        private void cbWLMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueWL = "";
        }
        private void cbFHDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueFH = "";
        }

        private void cbSHDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueSH = "";
        }


        #endregion

        #region 窗体按钮事件集
        private void StrapStart()
        {
            if (m_State == "2") //如果空闲
            {
                m_State = "1";  //占用
                this.tbJZ.Text = "";
                this.tbZZ.Text = "";
                this.tbQSZL.Text = this.tbLJLL.Text.Trim();
                GetSysTime();
                this.tbQSSJ.Text = Systime;
                Sweight = Convert.ToDouble(this.tbQSZL.Text.Trim());
                Stime = this.tbQSSJ.Text;
                //sdk.SDK_CapturePicture(relhandle, strRunPath + "\\Pic\\" + PointID + "_1" + ".bmp");//开始计量抓图(球机)
                //axSTWViewer1.SaveJpegFile(strRunPath + "\\Pic\\" + PointID + "_1" + ".jpg");
                SaveFirstData();
                this.btnKS.Enabled = false;
                this.btnReStart.Enabled = true;
                this.btnWC.Enabled = true;
                m_State = "2";  //释放，变为空闲
            }
        }

        private void StrapComplete()
        {
            if (m_State == "2") //如果空闲
            {
                m_State = "1";  //占用
                Gweight = Convert.ToDouble(this.tbLJLL.Text.Trim()) - Sweight;
                //if (Gweight > 0)
                //{
                WeightNo = Guid.NewGuid().ToString().Trim();
                this.tbZZ.Text = string.Format("{0:F3}", Gweight);
                //if (!check())
                //{
                //    this.tbZZ.Text = "";
                //    return;
                //}
                GetSysTime();
                Etime = Systime;
                //sdk.SDK_CapturePicture(relhandle, strRunPath + "\\Pic\\" + PointID + "_2" + ".bmp");//完成计量抓图（球机）
                //axSTWViewer1.SaveJpegFile(strRunPath + "\\Pic\\" + PointID + "_2" + ".jpg");
                PicUpLoad();
                SaveJLCZData();
                QueryAndBindJLCZData();
                //QueryAndBindComboxes();
                this.btnWC.Enabled = false;
                this.btnReStart.Enabled = false;
                this.btnKS.Enabled = true;
                //}
                //else
                //{
                //    MessageBox.Show("数据异常，起始重量大于完成重量，请检查仪表");
                //    return;
                //}
                m_State = "2";  //释放，变为空闲
            }
        }

        //开始按钮
        private void btnKS_Click(object sender, EventArgs e)
        {
            this.StrapStart();
            //this.tbJZ.Text = "";
            //this.tbZZ.Text = "";
            //this.tbQSZL.Text = this.tbLJLL.Text.Trim();
            //GetSysTime();
            //this.tbQSSJ.Text = Systime;
            //Sweight = Convert.ToDouble(this.tbQSZL.Text.Trim());
            //Stime = this.tbQSSJ.Text;
            ////sdk.SDK_CapturePicture(relhandle, strRunPath + "\\Pic\\" + PointID + "_1" + ".bmp");//开始计量抓图(球机)
           //axSTWViewer1.SaveJpegFile(strRunPath + "\\Pic\\" + PointID + "_1" + ".jpg");
            //SaveFirstData();
            //this.btnKS.Enabled = false;
            //this.btnReStart.Enabled = true;
            //this.btnWC.Enabled = true;
        }
        //完成按钮
        private void btnWC_Click(object sender, EventArgs e)
        {
            this.StrapComplete();
            //Gweight = Convert.ToDouble(this.tbLJLL.Text.Trim()) - Sweight;
            //if (Gweight > 0)
            //{
            //    WeightNo = Guid.NewGuid().ToString().Trim();
            //    this.tbZZ.Text = string.Format("{0:F3}", Gweight);
            //    if (!check())
            //    {
            //        this.tbZZ.Text = "";
            //        return;
            //    }
            //    GetSysTime();
            //    Etime = Systime;
            //    //sdk.SDK_CapturePicture(relhandle, strRunPath + "\\Pic\\" + PointID + "_2" + ".bmp");//完成计量抓图（球机）
            //axSTWViewer1.SaveJpegFile(strRunPath + "\\Pic\\" + PointID + "_2" + ".jpg");
            //    PicUpLoad();
            //    SaveJLCZData();
            //    QueryAndBindJLCZData();
            //    QueryAndBindComboxes();
            //    this.btnWC.Enabled = false;
            //    this.btnReStart.Enabled = false;
            //    this.btnKS.Enabled = true;
            //}
            //else
            //{ 
            //    MessageBox.Show("数据异常，起始重量大于完成重量，请检查仪表");
            //    return;
            //}
        }
        //保存扣重系数
        private void btBC_Click(object sender, EventArgs e)
        {
            if (isInt(tbXS.Text.Trim()))
            {
                MessageBox.Show("请输入1-100区间任意整数");
                tbXS.Focus();
                return;
            }
            factor = Convert.ToInt32(this.tbXS.Text.Trim());
            if (factor > 100 || factor <= 0)
            {
                MessageBox.Show("请输入1-100区间任意整数");
                tbXS.Focus();
                return;
            }
            Person = this.tbJLY.Text.Trim();
            CoreClientParam ccp = new CoreClientParam();
            ccp.IfShowErrMsg = true;
            ccp.ServerName = "Core.KgMcms.StrapWeight.Strap";
            ccp.MethodName = "Save_FactorData";
            ccp.ServerParams = new object[] { PointID, Person, factor };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode != -1)
            {
                MessageBox.Show("保存成功");
            }
        }

        #endregion

        #region 下拉框数据绑定
        private void QueryAndBindComboxes()
        {
            a.GetWLData(this.cbWLMC, PointID);
            a.GetLXData(this.cbFlow, PointID);
            a.GetFHDWData(this.cbFHDW, PointID);
            a.GetSHDWData(this.cbSHDW, PointID);
        }
        #endregion

        #region GRID数据绑定
        private void QueryAndBindJLCZData()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.strapweight.StrapWeight";
            ccp.MethodName = "QueryJLCZData_K35";
            ccp.SourceDataTable = dataTable1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid1);
        }
        #endregion

        #region 计量数据保存同步基础信息（下拉框信息）操作
        private void BaseOperation()
        {
            //物料基础操作
            if (this.cbWLMC.Text.Trim().Length > 0)
            {
                if (this.cbWLMC.SelectedValue == null)
                    FS_WL = a.InsertBaseData(PointID, this.cbWLMC.Text.Trim(), "SaveWLData");
                else
                {
                    if (valueWL == "")
                    {
                        FS_WL = this.cbWLMC.SelectedValue.ToString();
                        a.SetNonBaseData(PointID, cbWLMC.SelectedValue.ToString().Trim(), "SetWLData");
                    }
                    else
                    {
                        FS_WL = valueWL;
                        a.SetNonBaseData(PointID, valueWL, "SetWLData");
                    }
                }
            }
            //发货方基础操作
            if (this.cbFHDW.Text.Trim().Length > 0)
            {
                if (this.cbFHDW.SelectedValue == null)
                    FS_FH = a.InsertBaseData(PointID, this.cbFHDW.Text.Trim(), "SaveFHDWData");
                else
                {
                    if (valueFH == "")
                    {
                        FS_FH = this.cbFHDW.SelectedValue.ToString();
                        a.SetNonBaseData(PointID, this.cbFHDW.SelectedValue.ToString().Trim(), "SetFHDWData");
                    }
                    else
                    {
                        FS_FH = valueFH;
                        a.SetNonBaseData(PointID, valueFH, "SetFHDWData");
                    }
                }
            }

            //收货方基础操作
            if (this.cbSHDW.Text.Trim().Length > 0)
            {
                if (this.cbSHDW.SelectedValue == null)
                    FS_SH = a.InsertBaseData(PointID, this.cbSHDW.Text.Trim(), "SaveSHDWData");
                else
                {
                    if (valueSH == "")
                    {
                        FS_SH = this.cbSHDW.SelectedValue.ToString();
                        a.SetNonBaseData(PointID, this.cbSHDW.SelectedValue.ToString().Trim(), "SetSHDWData");
                    }
                    else
                    {
                        FS_SH = valueSH;
                        a.SetNonBaseData(PointID, valueSH, "SetSHDWData");
                    }
                }
            }

        }
        #endregion

        #region 保存计量数据
        private void SaveJLCZData()
        {
            //if (!check())
            //    return;
            //BaseOperation();

            //WeightNo = Guid.NewGuid().ToString().Trim();
            ContractNo = this.tbHTH.Text.Trim();
            if (this.tbDS.Text.Trim() != "")
                HopperNum = Convert.ToInt32(this.tbDS.Text.Trim());
            ItemNo = this.cbXMH.Text.Trim();
            Flow = this.cbFlow.SelectedValue.ToString();
            Person = this.tbJLY.Text.Trim();
            Shift = this.tbBC.Text.Trim();
            Term = this.tbBB.Text.Trim();
            StrapNo = this.cbPDH.Text.Trim();
            Tweight = Convert.ToDouble(this.tbKZ.Text.Trim());
            Gweight = Convert.ToDouble(this.tbZZ.Text.Trim());
            this.tbJZ.Text = string.Format("{0:F3}", Gweight - Tweight * 0.001);
            Nweight = Convert.ToDouble(this.tbJZ.Text.Trim());
            Double EndWeight = Convert.ToDouble(this.tbLJLL.Text.Trim()); // ADD by luobin

            //add by luobin
            //自动保存中，如果用户忘记选择，就用下列默认值
            if (FS_WL == "")
                FS_WL = "WL000781";
            if (FS_FH == "")
                FS_FH = "GY000037";
            if (FS_SH == "")
                FS_SH = "SH000191";
            if (Flow == "")
                Flow = "001";

            CoreClientParam ccp = new CoreClientParam();
            ccp.IfShowErrMsg = true;
            ccp.ServerName = "ygjzjl.strapweight.StrapWeight";
            ccp.MethodName = "Save_StrapWeight";
            ccp.ServerParams = new object[] { WeightNo, ContractNo, ItemNo, HopperNum, FS_WL, FS_FH, FS_SH, Flow, Gweight, Tweight, Nweight, Etime, Person, Shift, Term, StrapNo, PointID, EndWeight };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //if (ccp.ReturnCode != -1)
            //    MessageBox.Show("计量数据保存成功");

            this.btnWC.Enabled = false;
            this.btnReStart.Enabled = false;
            this.btnKS.Enabled = true;

        }
        #endregion

        #region 文本框输入合法性检测
        private bool check()
        {
            if (cbPDH.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择皮带号！");
                cbPDH.Focus();
                return false;
            }

            if (cbFlow.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择流向！");
                cbFlow.Focus();
                return false;
            }

            if (tbHTH.Text.Trim() != "" && tbHTH.Text.Trim().Length != 10)
            {
                MessageBox.Show("请输入10位合同号！");
                this.tbHTH.Focus();
                return false;
            }

            //if (cbXMH.Text.Trim().Length <= 0)
            //{
            //    MessageBox.Show("请选择或输入项目号！");
            //    this.cbXMH.Focus();
            //    return false;
            //}

            if (cbWLMC.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择或输入物料名！");
                cbWLMC.Focus();
                return false;
            }

            if (cbFHDW.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择或输入发货单位！");
                cbFHDW.Focus();
                return false;
            }
            if (cbSHDW.Text.Trim().Length <= 0)
            {
                MessageBox.Show("请选择或输入收货单位！");
                cbSHDW.Focus();
                return false;
            }
            if (isNumber(tbKZ.Text.Trim()))
            {
                MessageBox.Show("请输入正确扣重重量！");
                tbKZ.Focus();
                return false;
            }
            if (tbDS.Text.Trim() != "" && isInt(tbDS.Text.Trim()))
            {
                MessageBox.Show("请输入正确的斗数");
                tbDS.Focus();
                return false;
            }
            return true;

        }

        private bool isNumber(string str)
        {
            bool isVaild = false;
            try
            {
                Convert.ToSingle(str.Trim());
            }
            catch
            {
                isVaild = true;
                return isVaild;
            }
            return isVaild;
        }

        private bool isInt(string str)
        {
            bool isVaild = false;
            try
            {
                Convert.ToInt32(str.Trim());
            }
            catch
            {
                isVaild = true;
                return isVaild;
            }
            return isVaild;
        }
        #endregion

        #region 控件数据清空
        private void Clear()
        {
            this.cbFHDW.Text = "";
            this.cbFlow.Text = "";
            //this.cbPDH.Text = "";
            this.cbSHDW.Text = "";
            this.cbWLMC.Text = "";
            this.cbXMH.Text = "";
            //this.tbBB.Text = "";
            //this.tbBC.Text = "";
            this.tbDS.Text = "";
            this.tbHTH.Text = "";
            //this.tbJLD.Text = "";
            //this.tbJLY.Text = "";
            this.tbJZ.Text = "";
            this.tbKZ.Text = "";
            this.tbQSSJ.Text = "";
            this.tbQSZL.Text = "";
            this.tbZZ.Text = "";
        }
        #endregion

        #region 从SAP下载合同号、合同项目号、物料信息
        private void DownSapData()
        {
            flag = "";
            dtHTH.Rows.Clear();
            //string rfc = "ZJL_PO_DOWN";/ p方法名
            string hth = tbHTH.Text.ToString();

            CoreClientParam ccpHTH = new CoreClientParam();
            ccpHTH.ServerName = "Core.KgMcms.CarWeigh.SapOperation";
            ccpHTH.MethodName = "queryContractNo";
            ccpHTH.ServerParams = new object[] { hth };
            ccpHTH.IfShowErrMsg = false;
            ccpHTH.SourceDataTable = dtHTH;
            this.ExecuteQueryToDataTable(ccpHTH, CoreInvokeType.Internal);
            if (ccpHTH.ReturnCode == -1)
                MessageBox.Show("\rSAP无项目号与该合同号匹配\r\r   请手工录入项目号！");
            if (dtHTH.Rows.Count > 0)
            {
                cbXMH.DataSource = dtHTH;
                cbXMH.DisplayMember = "FS_ITEMNO";

                cbXMH.Text = dtHTH.Rows[0]["FS_ITEMNO"].ToString();
                cbWLMC.Text = dtHTH.Rows[0]["FS_MATERIALNAME"].ToString();
                valueWL = dtHTH.Rows[0]["FS_WL"].ToString();
                flag = "1";
            }
        }
        #endregion

        #region 仪表读数
        private void NumResult(object sender, Cfc110NumReceivedEventArgs e)
        {
            //double RealTimeData = e.CfcMVNum;
            //if (RealTimeData > 10)//有重量
            //{
            //    if (m_dPreData <= 10 && m_State == "2" && btnKS.Enabled == true)//启动
            //    {
            //        // axDbgNet1.DbgOUT("111111111\n");
            //        //皮带重量由小于5到大于5时启动自动保存一次计量
            //        this.StrapStart();
            //        //if (System.IO.File.Exists(PublicComponent.Constant.RunPath + "\\ding.wav"))
            //        //    PlaySound(PublicComponent.Constant.RunPath + "\\ding.wav");//播放启动声音
            //    }
            //}
            //else
            //{
            //    // axDbgNet1.DbgOUT("22222222222\n");
            //    if (m_dPreData > 10 && m_State == "2" && btnWC.Enabled == true)//启动
            //    {
            //        //皮带重量由大于5到小于5时启动自动保存二次计量
            //        this.StrapComplete();
            //        //if (System.IO.File.Exists(PublicComponent.Constant.RunPath + "\\stop.wav"))
            //        //    PlaySound(PublicComponent.Constant.RunPath + "\\stop.wav");//播放停止声音
            //    }
            //}

            //m_dPreData = RealTimeData;
            //this.tbLJLL.Text = e.CfcTLNum;
            //this.tbSSLL.Text = e.CfcMVNum;
            //if (m_State == "0")
            //    m_State = "2";
        }
        private void serialsCFCLib_NumReceived(object sender, Cfc110NumReceivedEventArgs e)
        {
            this.Invoke(new EventHandler(delegate
            {
                double RealTimeData = Double.Parse(e.CfcMVNum);
                if (RealTimeData > 10)//有重量
                {
                    if (m_dPreData <= 10 && m_State == "2" && btnKS.Enabled == true)//启动
                    {
                        // axDbgNet1.DbgOUT("111111111\n");
                        //皮带重量由小于5到大于5时启动自动保存一次计量
                        this.StrapStart();
                        //if (System.IO.File.Exists(PublicComponent.Constant.RunPath + "\\ding.wav"))
                        //    PlaySound(PublicComponent.Constant.RunPath + "\\ding.wav");//播放启动声音
                    }
                }
                else
                {
                    // axDbgNet1.DbgOUT("22222222222\n");
                    if (m_dPreData > 10 && m_State == "2" && btnWC.Enabled == true)//启动
                    {
                        //皮带重量由大于5到小于5时启动自动保存二次计量
                        this.StrapComplete();
                        //if (System.IO.File.Exists(PublicComponent.Constant.RunPath + "\\stop.wav"))
                        //    PlaySound(PublicComponent.Constant.RunPath + "\\stop.wav");//播放停止声音
                    }
                }

                m_dPreData = RealTimeData;
                this.tbLJLL.Text = e.CfcTLNum;
                this.tbSSLL.Text = e.CfcMVNum;
                if (m_State == "0")
                    m_State = "2";

            }));

        }

        //private void mycom1_NumReceived(object sender, CfcNumReceivedEventArgs e)
        //{

        //   // CfcNumReceivedEventArgs fc = new CfcNumReceivedEventArgs(NumResult);
        //    try
        //    {
        //        this.Invoke(new EventHandler(delegate{
        //             this.tbLJLL.Text = e.CfcTLNum.ToString();
        //    this.tbSSLL.Text = e.CfcMVNum.ToString();
        //    if (m_State == "0")
        //        m_State = "2";
        //        }));

        //    }
        //    catch
        //    {
        //        ;
        //    }
        //}
        private void OpenYB()
        {
            //if (mycom1. == -1)
            //{
            //    mycom1.PortNum = "\\\\.\\" + iPort;        //1,2,3,4
            //    mycom1.BaudRate = iRate;       //1200,2400,4800,9600
            //    mycom1.ByteSize = bSize;       //8 bits
            //    mycom1.Parity = bParity;       // 0-4=no,odd,even,mark,space 
            //    mycom1.StopBits = bStopBits;   // 0,1,2 = 1, 1.5, 2 
            //    mycom1.WeightType = bWeightType;
            //    mycom1.CfcNumReceived += new CfcNumReceivedEvent(mycom1_NumReceived);
            //    mycom1.StartMonitoring();
            //}
            //else
            //{
            //    MessageBox.Show("仪表未开启！");
            //}
            serialsCFC110Lib = new SerialsCFC110Lib("\\\\.\\" + iPort, iRate, bSize, bParity, bStopBits);
            serialsCFC110Lib.CfcNumReceived += new Cfc110NumReceivedEvent(serialsCFCLib_NumReceived);
            serialsCFC110Lib.Start();
        }
        private void Strap_F201_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (mycom1.commThreadAlive)
            //{
            //    mycom1.StopMonitoring();
            //    mycom1.Close();
            //}
            ifclosed = "true";
        }

        #endregion

        #region 获取服务器当前时间
        private void GetSysTime()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.strapweight.StrapWeight";
            ccp.MethodName = "GetTime";
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            Systime = ccp.ReturnObject.ToString();
        }
        #endregion

        //#region 图像处理
        // //连接硬盘录像机获取视频
        //private void GetVedio()
        //{
        //    sdk = new SDK318();
        //    sdk.SDK_Init();
        //    int val = 0;
        //    sdk.SDK_Login("10.6.18.109", 37777, "admin", "admin", ref val);
        //    loghandle = val;
        //    int pichandle = (int)pictureBox1.Handle;
        //    relhandle = sdk.SDK_RealPlay(loghandle, 0, pichandle, 2);

        //}
        ////本地已保存图片格式转换并存入数据库

        //#endregion

        #region 本地图片保存进数据库
        private void PicUpLoad()
        {
            //System.Drawing.Image imgSrc1 = System.Drawing.Image.FromFile(strRunPath + "\\Pic\\" + PointID + "_1" + ".bmp");
            //System.Drawing.Image imgSrc2 = System.Drawing.Image.FromFile(strRunPath + "\\Pic\\" + PointID + "_2" + ".bmp");
            //imgSrc1.Save(strRunPath + "\\Pic\\" + PointID + "_1" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //imgSrc2.Save(strRunPath + "\\Pic\\" + PointID + "_2" + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            b.GraspAndSavePDImage(strRunPath + "\\Pic\\" + PointID + "_1" + ".jpg", strRunPath + "\\Pic\\" + PointID + "_2" + ".jpg", WeightNo);
        }

        #endregion

        #region IP摄像机图像获取
        private void GetIPVedio()
        {

            //axSTWViewer1.SetSurfaceSize(400, 400);
            //axSTWViewer1.Connect(vip, vport, vuser, vpassword);
            //axSTWViewer1.Channel = 1;

            //this.axSTWViewer1.SetSurfaceSize(400, 400);

            SSNetSDK.XNS_DEV_Init();

            LPXNS_DEV_DEVICEINFO lp = new LPXNS_DEV_DEVICEINFO();

            string content = "123";

            IntPtr intptr = Marshal.AllocHGlobal(Marshal.SizeOf(lp));

            Marshal.StructureToPtr(lp, intptr, true);


            hDeviceHandle = SSNetSDK.XNS_DEV_Login(vip, vport, vuser, vpassword, "SNZ-5200", ref content, intptr, true, 80);
            //MessageBox.Show("注册成功7！"+ hDeviceHandle.ToString());
            //this.label1.Text += "设备句柄=" + hDeviceHandle + BR;
            //hRealHandle = SSNetSDK.XNS_DEV_StartRealPlay(hDeviceHandle, 3, this.axSTWViewer1.Handle);
            //MessageBox.Show("注册成功8！" + hRealHandle.ToString());

            //  SSNetSDK.XNS_DEV_Cleanup();
           // LPXNS_DEV_WORKSTATE state = new LPXNS_DEV_WORKSTATE();


        }
        #endregion

        #region 一次计量数据查询
        private void QueryAndBindFirstData()
        {
            DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StrapWeight.Strap";
            ccp.MethodName = "QueryFirstData_K31";
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }
        #endregion

        #region 查询并绑定扣重系数到文本框
        private void QueryFactorData()
        {
            DataTable dtFactor = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StrapWeight.Strap";
            ccp.MethodName = "QueryFactorData_K31";
            ccp.SourceDataTable = dtFactor;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dtFactor.Rows.Count > 0)
            {
                this.tbXS.Text = dtFactor.Rows[0]["FN_FACTOR"].ToString().Trim();
                factor = Convert.ToInt32(this.tbXS.Text.Trim());
                userid = dtFactor.Rows[0]["FS_USER"].ToString().Trim();
            }
            else
            {
                factor = 100;
            }

        }
        #endregion

        #region 保存一次计量数据


        private void SaveFirstData()
        {
            //if (tbKZ.Text.Trim() != "" && isNumber(tbKZ.Text.Trim()))
            //{
            //    MessageBox.Show("请输入正确扣重重量！");
            //    tbKZ.Focus();
            //    return;
            //}
            //if (tbDS.Text.Trim() != "" && isInt(tbDS.Text.Trim()))
            //{
            //    MessageBox.Show("请输入正确的斗数");
            //    tbDS.Focus();
            //    return;
            //}
            //if (tbHTH.Text.Trim() != "" && (tbHTH.Text.Trim().Length != 10 || isNumber(tbHTH.Text.Trim())))
            //{
            //    MessageBox.Show("请输入10位合同号！");
            //    this.tbHTH.Focus();
            //    return;
            //}
            //BaseOperation();
            ContractNo = this.tbHTH.Text.Trim();
            if (this.tbDS.Text.Trim() != "")
                HopperNum = Convert.ToInt32(this.tbDS.Text.Trim());
            ItemNo = this.cbXMH.Text.Trim();
            Flow = this.cbFlow.SelectedValue.ToString();
            StrapNo = this.cbPDH.Text.Trim();
            if (this.tbKZ.Text.Trim() != "")
                Tweight = Convert.ToDouble(this.tbKZ.Text.Trim());
            CoreClientParam ccp = new CoreClientParam();
            ccp.IfShowErrMsg = true;
            ccp.ServerName = "ygjzjl.strapweight.StrapWeight";
            ccp.MethodName = "Save_FistData";
            ccp.ServerParams = new object[] { PointID, Stime, Sweight, ContractNo, HopperNum, ItemNo, StrapNo, Tweight, FS_WL, FS_FH, FS_SH, Flow };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        #endregion

        #region 绑定一次计量数据
        private void BindFirstData()
        {

            String strSql = "select round(A.FN_STRATWEIHT,3) as FN_STRATWEIHT,to_char(A.FD_STARTTIME,'yyyy-MM-dd HH24:mi:ss') as FD_STARTTIME, " +
         "A.FS_CONTRACTNO,A.FS_ITEMNO,A.FN_HOPPERNUM,B.FS_MATERIALNAME,C.FS_SUPPLIERNAME AS FS_SENDERNAME,D.FS_MEMO AS FS_RECEIVERNAME, " +
         "E.FS_TYPENAME AS FS_FLOW,round(A.FN_TAREWEIGHT,3) as FN_TAREWEIGHT,round(A.FN_NETWEIGHT,3) as FN_NETWEIGHT, " +
         "to_char(A.FD_STARTTIME,'yyyy-MM-dd HH24:mi:ss') as FD_STARTTIME,A.FS_STRAPNO,A.FS_MATERIALNO,A.FS_SENDER,A.FS_RECEIVER " +
         "from DT_STRAPWEIGHT A,IT_MATERIAL B,IT_SUPPLIER C,IT_STORE D,BT_WEIGHTTYPE E " +
         "where A. FS_MATERIALNO = B.FS_WL(+) and A.FS_SENDER = C.FS_GY(+) and A.FS_RECEIVER = D.FS_SH(+) and A.FS_FLOW = E.FS_TYPECODE(+) and A.FS_COMPLETEFLAG='0' and A.FS_POINTID='K35'";
            DataTable dt = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            //ccp.ServerName = "ygjzjl.strapweight.StrapWeight";
            //ccp.MethodName = "QueryFirstData_K35";
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                this.tbQSSJ.Text = dt.Rows[0]["FD_STARTTIME"].ToString();
                this.tbQSZL.Text = string.Format("{0:F3}", dt.Rows[0]["FN_STRATWEIHT"].ToString().Trim());
                this.tbHTH.Text = dt.Rows[0]["FS_CONTRACTNO"].ToString();
                this.tbKZ.Text = string.Format("{0:F3}", dt.Rows[0]["FN_TAREWEIGHT"].ToString().Trim());
                this.tbDS.Text = dt.Rows[0]["FN_HOPPERNUM"].ToString().Trim();
                this.cbPDH.Text = dt.Rows[0]["FS_STRAPNO"].ToString().Trim();
                this.cbWLMC.Text = dt.Rows[0]["FS_MATERIALNAME"].ToString().Trim();
                this.cbFHDW.Text = dt.Rows[0]["FS_SENDERNAME"].ToString().Trim();
                this.cbSHDW.Text = dt.Rows[0]["FS_RECEIVERNAME"].ToString().Trim();
                this.cbXMH.Text = dt.Rows[0]["FS_ITEMNO"].ToString().Trim();
                this.cbFlow.Text = dt.Rows[0]["FS_FLOW"].ToString().Trim();
                if (dt.Rows[0]["FS_MATERIALNO"].ToString().Trim() != "")
                    valueWL = dt.Rows[0]["FS_MATERIALNO"].ToString().Trim();

                if (dt.Rows[0]["FS_SENDER"].ToString().Trim() != "")
                    valueFH = dt.Rows[0]["FS_SENDER"].ToString().Trim();

                if (dt.Rows[0]["FS_RECEIVER"].ToString().Trim() != "")
                    valueSH = dt.Rows[0]["FS_RECEIVER"].ToString().Trim();
                this.btnKS.Enabled = false;
                this.btnReStart.Enabled = true;
                this.btnWC.Enabled = true;
                Sweight = Convert.ToDouble(this.tbQSZL.Text.Trim());
            }
            else
            {
                this.btnWC.Enabled = false;
                this.btnReStart.Enabled = false;
                this.btnKS.Enabled = true;
            };

        }
        #endregion

        #region 查询计量点基础信息
        private void QueryPointBaseInfo()
        {
            DataTable datatable1 = new DataTable();

            try
            {
                string pointCode = "";
               

                string strWhere = "SELECT 'False' AS XZ,T.FS_POINTCODE,T.FS_POINTNAME,T.FS_POINTDEPART,T.FS_POINTTYPE,T.FS_VIEDOIP,T.FS_VIEDOPORT,T.FS_VIEDOUSER,T.FS_VIEDOPWD,";
                strWhere += " T.FS_METERTYPE,T.FS_METERPARA,T.FS_MOXAIP,T.FS_MOXAPORT,T.FS_RTUIP,T.FS_RTUPORT,T.FS_PRINTERIP,T.FS_PRINTERNAME,T.FS_PRINTTYPECODE,T.FN_USEDPRINTPAPER,";
                strWhere += " T.FN_USEDPRINTINK,T.FS_LEDIP,T.FS_LEDPORT,T.FN_VALUE,T.FS_ALLOWOTHERTARE,T.FS_SIGN,T.FS_DISPLAYPORT,T.FS_DISPLAYPARA,";
                strWhere += " T.FS_READERPORT,T.FS_READERPARA,T.FS_READERTYPE,T.FS_DISPLAYTYPE,T.FF_CLEARVALUE FROM BT_POINT T ";

                strWhere += " WHERE T.FS_POINTTYPE = 'PD' ";
                strWhere += " and T.FS_POINTCODE='" + PointID + "' ORDER BY T.FS_POINTCODE";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.bar.StoreageWeight_BC";
                ccp.MethodName = "QueryTableData";
                ccp.ServerParams = new object[] { strWhere };
                ccp.SourceDataTable = datatable1;

                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }


            if (datatable1.Rows.Count > 0)
            {
                bWeightType = datatable1.Rows[0]["FS_METERTYPE"].ToString();
                string[] pa = datatable1.Rows[0]["FS_METERPARA"].ToString().Split(',');
                iPort = pa[0]; //1,2,3,4
                iRate = Convert.ToInt32(pa[1]); //1200,2400,4800,9600   
                bParity = pa[2]; // 0-4=no,odd,even,mark,space 
                bSize = Convert.ToByte(pa[3]); //8 bits
                bStopBits = Convert.ToByte(pa[4]); // 0,1,2 = 1, 1.5,
                this.tbJLD.Text = datatable1.Rows[0]["FS_POINTNAME"].ToString();
                vip = datatable1.Rows[0]["FS_VIEDOIP"].ToString();
                vport = Convert.ToInt16(datatable1.Rows[0]["FS_VIEDOPORT"].ToString());
                vuser = datatable1.Rows[0]["FS_VIEDOUSER"].ToString();
                vpassword = datatable1.Rows[0]["FS_VIEDOPWD"].ToString();
            }
        }

        #endregion
        

        private void PlaySound(string location)
        {

            if (m_SoundPlayer == null)
            {
                m_SoundPlayer = new SoundPlayer();
            }

            m_SoundPlayer.SoundLocation = location;
            m_SoundPlayer.Load();
            m_SoundPlayer.Play();
        }

        private void btnReStart_Click(object sender, EventArgs e)
        {
            this.tbJZ.Text = "";
            this.tbZZ.Text = "";
            this.tbQSZL.Text = this.tbLJLL.Text.Trim();
            GetSysTime();
            this.tbQSSJ.Text = Systime;
            Sweight = Convert.ToDouble(this.tbQSZL.Text.Trim());
            Stime = this.tbQSSJ.Text;
            //sdk.SDK_CapturePicture(relhandle, strRunPath + "\\Pic\\" + PointID + "_1" + ".bmp");//开始计量抓图(球机)
            //axSTWViewer1.SaveJpegFile(strRunPath + "\\Pic\\" + PointID + "_1" + ".jpg");
            SaveFirstData();
            this.btnKS.Enabled = false;
            this.btnReStart.Enabled = true;
            this.btnWC.Enabled = true;
        }

        private void btnWLMC_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);

            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(), this.ob);
            //frm.ob = this.ob;
            frm.Owner = this;
            frm.ShowDialog();
        }

        ////按磅房筛选物料信息
        //private void BandPointMaterial(string PointID)
        //{
        //    DataRow[] drs = null;

        //    tempMaterial = m_MaterialTable.Clone();

        //    drs = this.m_MaterialTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

        //    tempMaterial.Clear();
        //    foreach (DataRow dr in drs)
        //    {
        //        tempMaterial.Rows.Add(dr.ItemArray);
        //    }

        //    DataRow drz = tempMaterial.NewRow();
        //    tempMaterial.Rows.InsertAt(drz, 0);
        //    cbWLMC.DataSource = tempMaterial;
        //    cbWLMC.DisplayMember = "fs_materialname";
        //    cbWLMC.ValueMember = "FS_MATERIALNO";
        //}

        ////下载磅房对应物料基础信息  ,add by luobin 
        //private void DownLoadMaterial()
        //{
        //    string strSql = "select A.FS_PointNo, A.FS_MATERIALNO, b.fs_materialname, b.FS_HELPCODE, a.fn_times ";
        //    strSql += " from Bt_Pointmaterial A, It_Material B, Bt_Point C ";
        //    strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.Fs_Materialno = B.Fs_Wl and C.Fs_Pointtype = 'QC' ";
        //    strSql += "  order by a.fn_times desc ";


        //    CoreClientParam ccp = new CoreClientParam();
        //    ccp.ServerName = "Core.KgMcms.CarWeigh.TransPlanInfo";
        //    ccp.MethodName = "ExcuteQuery";
        //    ccp.ServerParams = new object[] { strSql };
        //    ccp.SourceDataTable = this.m_MaterialTable;

        //    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        //}

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

    }
}
