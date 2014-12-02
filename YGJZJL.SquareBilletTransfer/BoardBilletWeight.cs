using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using YGJZJL.PublicComponent;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using SDK_Com;
using System.Threading;
using Infragistics.Win.UltraWinGrid;
using Core.Sip.Client.App;
using Core.Sip.Client.Meas;

namespace YGJZJL.SquareBilletTransfer
{
    public partial class BoardBilletWeight : FrmBase
    {
        #region 定义变量
        //校秤：
        public delegate void CorrentionPicture();//校秤抓图委托
        private CorrentionPicture m_MainThreadCorrentionPicture;//建立校秤委托变量
        BaseInfo baseinfo = new BaseInfo();
        private string correntionWeight = "";
        private string correntionWeightNo = "";
        private string pointId = "";

        private DataTable dt;
        BaseInfo b = new BaseInfo();
        BaseInfo m_BaseInfo;
        SDK_Com.HKDVR sdk;
        int loghandle;
        private string strRunPath;             //系统路径
        private string strLastNumber = "";     //最后一个序列号
        private string filename1;              //图片1文件名称
        private string filename2;              //图片2文件名称
        private string strZYBH = "";                   //作业编号
        private string strWLBH = "";             //物料代码
        private string strWLMC = "";             //物料名称
        private string strFHDW = "";             //发货单位代码
        private string strSHDW = "";             //收货单位代码
        private string strLiuX = "";             //流向代码
        private int countZS = 0;                 //统计表中累计支数计数
        private double countZL = 0;              //统计表中累计重量计数    
        private string pointcode = "K19";        //计量点代码,默认为辊道秤
        private string pointname = "";           //计量点名称
        public string cpflg;                     //完炉标志
        private int totalZS = 0;                 //预报单炉总支数
        private string valueWL = "";
        private string valueFH = "";
        private string valueSH = "";
        private string strLLJZ = "0";         //理论计重标志

        private BT_POINT[] m_Points;//计量点数组
        private CoreApp _measApp;//计量点
        private bool _isTalk = false;//是否正在对讲
        private int _talkId = -1;
        private bool _saved = false;

        //ADD BY TOM
        private int m_nPointCount;//计量点个数
        private bool m_bRunningForPoundRoom;//计量点线程运行开关
        private bool m_bPoundRoomThreadClosed;//计量点线程关闭开关
        private System.Threading.Thread m_hThreadForPoundRoom;//计量点线程句柄

        private int m_iSelectedPound;//选择的计量点索引，用于计量点切换时RecordClose等方法使用

        public delegate void CapPicture();//抓图委托
        private CapPicture m_MainThreadCapPicture;//建立委托变量
        private bool m_bGraspImageSign = false;//抓图开关

        public delegate void ClearWeight();//清空重量委托
        private ClearWeight m_MainThreadClearWeight;//建立委托变量

        int k;  //硬盘录像机视频调节参数，是具体调节哪个视频
        int BigChannel = 0;  //放大图片句柄
        int m_CurSelBigChannel = -1;//当前放大的是哪一个通道

        public delegate void AlarmVoice();//播放音频
        private AlarmVoice m_MainThreadAlarmVoice;//建立委托变量
        public string m_AlarmVoicePath = ""; //声音文件路径
        private string p_shiftdate = "";

        //ADD
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
        YGJZJL.PublicComponent.MessageForm messForm = new MessageForm();

        private string _rollerNo = "";
        private bool _mousedown = false;
        private int _moveLeft = 0;
        private int _moveTop = 0;

        public delegate void QueryYBThreadDelegate();//绑定委托
        private QueryYBThreadDelegate m_QueryYBThreadDelegate;//建立委托变量
        private bool m_flag;
        private Thread m_thread;
        #endregion

        public BoardBilletWeight()
        {
            m_iSelectedPound = -1;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        #region 窗体按钮事件
        //"读数"按钮
        private void btnDS_Click(object sender, EventArgs e)
        {
            Read();
        }

        //"保存"按钮
        private void btnBC_Click_1(object sender, EventArgs e)
        {
            //txtXSZL.Text = "2";

            //Read   
            txtZL.Text = handWeight.Text.Trim();
            double tmpWeight = 0;
            if (double.TryParse(handWeight.Text.Trim(), out tmpWeight))
            {
                //if (tmpWeight - 2.05 > 0.2)
                //{
                //    if (DialogResult.Yes == MessageBox.Show("重量不正常,是否理论计重？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                //    {
                //        strLLJZ = "1";
                //        Save();
                //        QueryCount();
                //        strLLJZ = "0";
                //    }
                //    else
                //    {
                //        Save();
                //        QueryCount();
                //    }
                //}
                //else
                //{
                    Save();
                    QueryCount();
                //}
            }
            else
            {
                MessageBox.Show("重量格式不正确！");
            }
        }

        private void Read()
        {
          
            //读取重量数据
            txtZL.Text = txtXSZL.Text.Trim();
            ////抓图            
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        private void Save()
        {
            try
            {
                if (this.chkAutoSave.Checked == false)
                {
                    txtZL.Text = handWeight.Text.Trim();
                }
                else
                {
                    txtZL.Text = this.txtXSZL.Text.Trim();
                }
                if (m_iSelectedPound == -1)
                {
                    MessageBox.Show("请先选择计量点！");
                    this.txtZL.Text = "";
                    return;
                }
                int i = m_iSelectedPound;

                if (Check1())
                    return;

                if(!IsReturnBillet(this.txtLH.Text.Trim()))
                {
                //保存控件数据至计量主表和计量明细表
                     SaveJLCZData();
                }
                else
                {
                    setRecordComplete(this.txtLH.Text.Trim());
                    messForm.SetMessage("该块为返回坯！重量已保存！");
                    Thread.Sleep(500);
                    this.QueryAndBindYBData();
                }

                //按纽颜色变化
                if (btnBC.BackColor == Color.Bisque)
                    btnBC.BackColor = Color.LightSeaGreen;
                else
                    btnBC.BackColor = Color.Bisque;


                txtZL.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 自动语音播放
        /// </summary>
        public void AutoAlarmVoice()
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

                FileInfo fi = new FileInfo(m_AlarmVoicePath);
                int waveTimeLen = Convert.ToInt32((fi.Length - 54) / 16 + 500);
                _measApp.Dvr.SDK_SendData(m_AlarmVoicePath);
                Thread.Sleep(waveTimeLen);
            }
        }

        //"清零"按钮
        private void btnQL_Click(object sender, EventArgs e)
        {

        }
      
        #endregion

        
        #region 窗体事件及控件初始化

        //清空控件
        private void ClearControler()
        {
            this.txtXSZL.Text = "0000";
            this.txtLH.Text = "";
            this.txtDDH.Text = "";
            this.cbWLMC.Text = "";
            this.txtDDXMH.Text = "";
            this.cbGZ.Text = "";
            this.cbGG.Text = "";
            this.cbLiuX.Text = "";
            this.txtCD.Text = "";
            this.cbFHDW.Text = "";
            this.cbSHDW.Text = "";
            this.lbWD.Text = "稳定";
            this.lbYS.ForeColor = Color.Green;
        }

        private void ClearCompleteControler()
        {
           
            this.txtDDH.Text = "";
            this.cbWLMC.Text = "";
            this.txtDDXMH.Text = "";
            this.cbGZ.Text = "";
            this.cbGG.Text = "";
            this.cbLiuX.Text = "";
            this.txtCD.Text = "";
            this.cbFHDW.Text = "";
            this.cbSHDW.Text = "";
        }
        //炉号文本框时间：光标移开下载预报信息
        private void txtLH_Leave(object sender, EventArgs e)
        {   
            CompletationQuery();
            if (cpflg == "1")
            {
                cpflg = "";
                ClearControler();
                this.txtLH.Focus();
                this.btnDS.Enabled = false;
                this.btnBC.Enabled = true;
                MessageBox.Show("该炉号已经完炉，请输入新炉号！");
                return;
            }
            else
            {
                QueryAndBindYBData();
                this.btnDS.Enabled = true;
            }
        }

       //窗体初始化
        private void BoardBilletWeight_Load(object sender, EventArgs e)
        {
            //if (CustomInfo.Equals("GD1"))
            //{
            //    ultraOptionSet1.Items.RemoveAt(1);
            //    ultraOptionSet1.Items[0].DisplayText = "棒材";
            //}
            //else if (CustomInfo.Equals("GD2"))
            //{
            //    ultraOptionSet1.Items.RemoveAt(0);
            //}

            baseinfo.ob = this.ob;
            b.ob = this.ob;
            
            m_BaseInfo = new BaseInfo();
            m_BaseInfo.ob = this.ob;
            this.chkAutoSave.Enabled = true;
            if (Constant.RunPath == "")
            {
                Constant.RunPath = System.Environment.CurrentDirectory;
            }
            ClearControler();//清空控件
            this.txtJLD.Text = "";
            this.txtJLY.Text = "";
            this.txtBC.Text = "";
            this.txtBB.Text = "";
            this.txtZL.Text = "";
            pointcode = "K19";

            //初始化计量点
            WeighPoint wp = new WeighPoint(this.ob);
            m_Points = wp.GetPoints("BP") ;
            if (m_Points != null)
            {
                m_nPointCount = m_Points.Length;
            }
            dt = new DataTable();
            txtJLY.Text = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            this.txtBC.Text = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            this.txtBB.Text = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
            p_shiftdate = System.DateTime.Today.Date.ToShortDateString();
            strRunPath = System.Environment.CurrentDirectory;//当前自定义路径
            GetYYBBData();

            QueryAndBindJLDData();//绑定计量点GRID
            m_List.KeyPress += new KeyPressEventHandler(m_List_KeyPress);
            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadSteelType();  //下载磅房对应承运单位信息到内存表
            this.DownLoadSepc(); //下载磅房对应车号信息到内存表
            this.DownLoadFlow();  //下载流向信息


            this.btnDS.Enabled = false;
            this.btnBC.Enabled = true;
            this.txtLH.Focus();
        }

        #endregion

        #region
        // 从本地磁盘获取图片文件
        private byte[] GetImageFile(string SequenceNo, int index)
        {
            byte[] FileContent;

            if (System.IO.File.Exists(strRunPath + "\\fppicture\\" + SequenceNo + index.ToString() + ".bmp") == true)
            {
                Bitmap img = new Bitmap(strRunPath + "\\fppicture\\" + SequenceNo + index.ToString() + ".bmp");
                System.Drawing.Image.GetThumbnailImageAbort callb = null;
                System.Drawing.Image newimage = img.GetThumbnailImage(img.Width / 2, img.Height / 2, callb, new System.IntPtr());
                //缩略图   
                newimage.Save(strRunPath + "\\fppicture\\" + SequenceNo + index.ToString() + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                newimage.Dispose();
                FileContent = System.IO.File.ReadAllBytes(strRunPath + "\\fppicture\\" + SequenceNo + index.ToString() + ".JPG");

                return FileContent;
            }
            else
            {
                string error;
                error = "本地不存在图片" + SequenceNo + index.ToString() + ".bmp" + "！";
                MessageBox.Show(error,"错误",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            FileContent = System.IO.File.ReadAllBytes(strRunPath + "\\fppicture\\" + SequenceNo + index.ToString() + ".JPG"); 
            return FileContent;
        }

        #endregion

        #region 下拉框绑定数据方法集
        //获取物料名称数据
        private void GetWLMCData() 
        {
            ArrayList param = new ArrayList();
            param.Add(pointcode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_07.SELECT", param };
            DataTable dtWLMC = new DataTable();
            ccp.SourceDataTable = dtWLMC;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dtWLMC.Rows.Count > 0)
            {
                DataRow dr = dtWLMC.NewRow();
                dtWLMC.Rows.InsertAt(dr, 0);
                cbWLMC.DataSource = dtWLMC;

                cbWLMC.DisplayMember = "FS_MATERIALNAME";
                cbWLMC.ValueMember = "FS_MATERIALNO";
            }


            
        }
        //获取钢种数据
        private void GetGZData()
        {
            ArrayList param = new ArrayList();
            param.Add(pointcode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_08.SELECT", param };
            DataTable dtGZ = new DataTable();
            ccp.SourceDataTable = dtGZ;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
           
            if (dtGZ.Rows.Count > 0)
            {
                DataRow dr = dtGZ.NewRow();
                dtGZ.Rows.InsertAt(dr, 0);
                cbGZ.DataSource = dtGZ;
                cbGZ.DisplayMember = "FS_STEELTYPE";
                cbGZ.ValueMember = "FS_STEELTYPE";
            }
            
        }
        //获取流向数据
        private void GetLiuXData()
        {
            ArrayList param = new ArrayList();
            param.Add(pointcode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_09.SELECT", param };
            DataTable dtLiuX = new DataTable();
            ccp.SourceDataTable = dtLiuX;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);           
            if (dtLiuX.Rows.Count > 0)
            {
                DataRow dr = dtLiuX.NewRow();
                dtLiuX.Rows.InsertAt(dr, 0);
                cbLiuX.DataSource = dtLiuX;
                cbLiuX.DisplayMember = "FS_TYPENAME";
                cbLiuX.ValueMember = "FS_FLOW";
            }
            
        }
        //获取规格数据
        private void GetGGData()
        {
            ArrayList param = new ArrayList();
            param.Add(pointcode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_10.SELECT", param };
            DataTable dtGG = new DataTable();
            ccp.SourceDataTable = dtGG;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            
            if (dtGG.Rows.Count > 0)
            {
                DataRow dr = dtGG.NewRow();
                dtGG.Rows.InsertAt(dr, 0);
                cbGG.DataSource = dtGG;
                cbGG.DisplayMember = "FS_SPEC";
                cbGG.ValueMember = "FS_SPEC";
            }
           
        }
        //获取发货单位数据
        private void GetFHDWData()
        {
            ArrayList param = new ArrayList();
            param.Add(pointcode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_11.SELECT", param };
            DataTable dtFHDW = new DataTable();
            ccp.SourceDataTable = dtFHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            
            if (dtFHDW.Rows.Count > 0)
            {
                DataRow dr = dtFHDW.NewRow();
                dtFHDW.Rows.InsertAt(dr, 0);
                cbFHDW.DataSource = dtFHDW;
                cbFHDW.DisplayMember = "FS_MEMO";
                cbFHDW.ValueMember = "FS_SENDER";
            }
            
        }
        //获取收货单位数据
        private void GetSHDWData()
        {
            ArrayList param = new ArrayList();
            param.Add(pointcode);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_12.SELECT", param };
            DataTable dtSHDW = new DataTable();
            ccp.SourceDataTable = dtSHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            
            if (dtSHDW.Rows.Count > 0)
            {
                DataRow dr = dtSHDW.NewRow();
                dtSHDW.Rows.InsertAt(dr, 0);
                cbSHDW.DataSource = dtSHDW;
                cbSHDW.DisplayMember = "FS_MEMO";
                cbSHDW.ValueMember = "FS_RECEIVER";
            }
           
        }
        //获取语音播报信息
        private void GetYYBBData()
        {
            string strName = "";
            string strType = "GD";
            dataTable4.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "QueryVoiceTableData";
            ccp.ServerParams = new object[] { strName, strType };
            ccp.SourceDataTable = dataTable4;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid4);

            if (System.IO.Directory.Exists(Constant.RunPath + "\\gdsound\\") == false)
            {
                System.IO.Directory.CreateDirectory(Constant.RunPath + "\\gdsound\\");
            }

            for (int i = 0; i < dataTable4.Rows.Count; i++)
            {
                if (System.IO.File.Exists(Constant.RunPath + "\\gdsound\\" + dataTable4.Rows[i]["FS_VOICENAME"].ToString().Trim()) == false)
                {
                    System.IO.File.WriteAllBytes(Constant.RunPath + "\\gdsound\\" + dataTable4.Rows[i]["FS_VOICENAME"].ToString().Trim(), (byte[])dataTable4.Rows[i]["FS_VOICEFILE"]);
                }
            }
        }

        
        #endregion

        #region 绑定UtralGrid
        //绑定Grid1 计量称重信息表
        private void QueryAndBindJLCZData()
        {
            string strSQL = "select * from(SELECT T.FS_STOVENO,'1' FN_BILLETCOUNT,T.FN_WEIGHT,TO_CHAR(T.FD_WEIGHTTIME, 'YYYY-MM-DD HH24:MI:SS') FD_WEIGHTTIME,";
            strSQL += "T.FS_SHIFT,T.FS_TERM,T.FS_PERSON FROM DT_BOARDWEIGHTMAIN T ORDER BY FD_WEIGHTTIME DESC ) WHERE ROWNUM<=30";
            this.dataSet1.Tables["辊道计量从表"].Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            ccp.SourceDataTable = dataSet1.Tables["辊道计量从表"];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid1);
        }

                #endregion


        //绑定Grid2 计量点信息表
        private void QueryAndBindJLDData()
        {
            dataTable2.Clear();
            string strSQL = @"SELECT 'FALSE' AS XZ,
		       T.FS_POINTCODE,
		       T.FS_POINTNAME,
		       T.FS_POINTDEPART,
		       T.FS_POINTTYPE,
		       T.FS_VIEDOIP,
		       T.FS_VIEDOPORT,
		       T.FS_VIEDOUSER,
		       T.FS_VIEDOPWD,
		       T.FS_METERTYPE,
		       T.FS_METERPARA,
		       T.FS_MOXAIP,
		       T.FS_MOXAPORT,
		       T.FS_RTUIP,
		       T.FS_RTUPORT,
		       T.FS_PRINTERIP,
		       T.FS_PRINTERNAME,
		       T.FS_PRINTTYPECODE,
		       T.FN_USEDPRINTPAPER,
		       T.FN_USEDPRINTINK,
		       T.FS_LEDIP,
		       T.FS_LEDPORT,
		       T.FN_VALUE,
		       T.FS_ALLOWOTHERTARE,
		       T.FS_SIGN,
		       T.FS_DISPLAYPORT,
		       T.FS_DISPLAYPARA,
		       T.FS_READERPORT,
		       T.FS_READERPARA,
		       T.FS_READERTYPE,
		       T.FS_DISPLAYTYPE
		  FROM BT_POINT T
		 WHERE T.FS_POINTTYPE = 'BP'
		 ORDER BY T.FS_POINTCODE";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            ccp.SourceDataTable = dataTable2;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid2);
            ultraGrid2.Rows[0].Activated = true;
        }

        //判断是不是为返回坯
        private bool IsReturnBillet(string strStove)
        {
            bool boolResult = false;
            string strSQL = "select * from dt_boardweightmain where FS_STOVENO='" + strStove + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            DataTable dt = new DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if(dt.Rows.Count>0)
            {
                boolResult=true;
            }
            return boolResult;
        }

        //标记完炉
        private void setRecordComplete(string strStove)
        {
            string strSQL = "update dt_bp_plan set FS_COMPLETEFLAG='1' where FS_STOVENO='" + strStove + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSQL };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            strSQL = "update it_fp_techcard t set  t.fs_gpys_receiver    = 'system',FS_GP_COMPLETEFLAG='1',";
            strSQL += "t.fd_gpys_receivedate = sysdate,t.fs_checked = '1' where fs_gp_stoveno='" + strStove + "'";
           ccp.ServerParams = new object[] { strSQL };
           this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

           strSQL = "update dt_boardweightmain t set  t.FS_ORDER =(select a.fs_order from dt_bp_plan a where a.fs_stoveno=t.fs_stoveno) ";
           strSQL += " where t.fs_stoveno='" + strStove + "'";
           
           ccp.ServerParams = new object[] { strSQL };
           this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        #region 保存数据方法
        //保存控件数据到计量主表和从表
        private void SaveJLCZData()
        {
            try
            {
                //从表数据
                
                strZYBH = Guid.NewGuid().ToString().Trim(); //作业编号
                if (txtDDH.Text != "" && txtDDXMH.Text == "")
                {
                    this.txtDDXMH.Text = "0001";

                }

                string strJLY = UserInfo.GetUserName();//计量员
                string strBC = UserInfo.GetUserOrder();  //班次
                string strBB = UserInfo.GetUserGroup();//班别
                //主表参数 
                string strDDH = this.txtDDH.Text.Trim(); //订单号      
                string strDDXMH = this.txtDDXMH.Text.Trim();//订单项目号


                if (this.cbWLMC.Text.Trim() != "")
                    strWLMC = this.cbWLMC.Text.Trim(); //物料名称 
                else
                    strWLMC = "板坯";

                if (cbWLMC.SelectedIndex > 0)
                {
                    strWLBH = cbWLMC.SelectedValue.ToString().Trim();//物料代码
                }
                else
                {
                    if (valueWL != "")
                        strWLBH = valueWL;
                    else
                        strWLBH = "WL000371";//二作方坯 Q235A 150*150*(3000-3150)
                }


                if (cbFHDW.SelectedIndex > 0)
                {
                    strFHDW = cbFHDW.SelectedValue.ToString().Trim();//发货单位代码
                }
                else
                {
                    if (valueFH != "")
                        strFHDW = valueFH;
                    else
                        strFHDW = "FH000126";//炼钢厂一作业区

                }

                if (cbSHDW.SelectedIndex > 0)
                {
                    strSHDW = cbSHDW.SelectedValue.ToString().Trim();//收货单位代码
                }
                //else
                //{
                //    if (valueFH != "")
                //        strSHDW = valueSH;
                //    else
                //        strSHDW = "SH000098";//棒线厂一作业区
                //}
               
                //if (cbSHDW.Text.Trim() == "棒线厂一作业区")
                //{
                //    strSHDW = "SH000098";
                //}
                //else if (cbSHDW.Text.Trim() == "棒线厂二作业区")
                //{
                //    strSHDW = "SH000099";
                //}
                //else
                //{
                //    strSHDW = "SH000098";
                //}

                if (cbLiuX.SelectedIndex > 0)
                {
                    strLiuX = cbLiuX.SelectedValue.ToString().Trim();//流向代码
                }
                else
                {
                    strLiuX = "003";//生产订单收货
                }

                string strGZ = this.cbGZ.Text.Trim();    //钢种
                string strGG = this.cbGG.Text.Trim();    //规格
                double strCD = Convert.ToDouble(this.txtCD.Text.Trim());//定尺长度


                //计量点代码 使用全局变量pointcode

                //主从共用参数
                string strLH = this.txtLH.Text.Trim(); //炉号
                double ZL;
                try
                {
                    //ZL = Convert.ToDouble(this.txtZL.Text.Trim());
                    ZL = Convert.ToDouble(string.Format("{0:F3}", Single.Parse(this.txtZL.Text.Trim())));
                }//重量
                catch (Exception ce)
                {
                    ZL = 0;
                }

                Hashtable ht = new Hashtable();
                ht.Add("i1", strZYBH);
                ht.Add("i2", strLH);
                ht.Add("i3", strDDH);
                ht.Add("i4", strWLBH);
                ht.Add("i5", strWLMC);
                ht.Add("i6", strGZ);
                ht.Add("i7", strGG);
                ht.Add("i8", strLiuX);
                ht.Add("i9", strFHDW);
                ht.Add("i10", strSHDW);
                ht.Add("i11", pointcode);
                ht.Add("i12", strJLY);
                ht.Add("i13", ZL);
                ht.Add("i14", strBC);
                ht.Add("i15", strBB);
                ht.Add("i16", strCD);

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "executeProcedureBySql2";
                string strSql = "{call KG_MCMS_BoardBilletInfo.SaveJLData(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)}";
                ccp.ServerParams = new object[] { strSql, ht  };
                ccp.IfShowErrMsg = false;
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                if (ccp.ReturnCode == -1)
                {
                    MessageBox.Show("重量保存失败");
                    return;
                }
                
                string result = "";
                string completeLH = "";

                m_AlarmVoicePath = Constant.RunPath + "\\sound\\ProductComplete.wav";

                    result = ccp.ReturnObject.ToString();
                    countZL += ZL;//保存成功则累计重量
                    countZS += 1;//保存成功累计支数
                    messForm.SetMessage("保存成功！");
                    
                QueryAndBindJLCZData();               
                //保存抓图信息
                //SavePictureData();
                QueryAndBindYBData();//UUUUUUUUUUUUUUUUUUUUUUU--7月18日---晚注销 后移动到此处

                _saved = true;
                ////LED屏显示
                //if (m_iSelectedPound >= 0 && m_iSelectedPound < m_nPointCount && _measApp.Led != null)
                //{
                //    string str = "";
                //    string strLedText = "";
                //    //if (dataTable6.Rows.Count > 0 && dataTable6.Rows[0]["FS_THWEITSINGLE"].ToString() == "理论")
                //    //    str = "理论计重";
                //    if (dataTable1.Rows.Count > 0 && dataTable3.Rows.Count > 0)
                //    {

                //        DataTable dt = new DataTable();
                //        string sql = " select to_char(FN_NetWeight,'FM999990.000') from dt_boardweightmain where fs_stoveno = '" + strLH + "' ";
                //        //sql += " and fn_billetindex = (select max(fn_billetindex) from dt_steelweightdetailroll where fs_stoveno = '" + strLH + "')";

                //        //CoreClientParam ccp = new CoreClientParam();
                //        ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                //        ccp.MethodName = "ExcuteQuery";
                //        ccp.ServerParams = new object[] { sql };
                //        ccp.SourceDataTable = dt;

                //        this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                //        string strLastWeight = dt.Rows[0][0].ToString();

                //        _measApp.Led.SendText("炉号：" + dataTable1.Rows[0]["FS_STOVENO"].ToString() + "，" + dataTable1.Rows[0]["FN_BILLETCOUNT"].ToString() + "/" + dataTable3.Rows[0]["FN_COUNT"].ToString() + "支\n\n单块重量：" + strLastWeight + " 吨\n\n" + "累计重量：" + dataTable1.Rows[0]["FN_TOTALWEIGHT"].ToString() + " 吨" + " " + str, "宋体", 9, 1, 4, 0);

                //        //WriteLog(dataTable1.Rows[0]["FS_STOVENO"].ToString() + "-1");
                //    }
                //    else
                //    {
                //        if (completeLH != "")
                //        {
                //            dataTable1.Clear();
                //            CoreClientParam cop = new CoreClientParam();
                //            cop.ServerName = "core.mcms.billetTransfer.BilletInfo";
                //            cop.MethodName = "QueryJLCZData";
                //            cop.ServerParams = new object[] { completeLH };
                //            cop.SourceDataTable = dataTable1;
                //            this.ExecuteQueryToDataTable(cop, CoreInvokeType.Internal);
                //            strLedText = "炉号：" + dataTable1.Rows[0]["FS_STOVENO"].ToString() + "，" + dataTable1.Rows[0]["FN_BILLETCOUNT"].ToString() + "/" + dataTable1.Rows[0]["FN_BILLETCOUNT"].ToString() + "支\n\n单支重量：" + this.txtZL.Text.Trim() + "吨\n\n" + "累计重量：" + dataTable1.Rows[0]["FN_TOTALWEIGHT"].ToString() + " 吨" + " " + str;
                //            WriteLog(strLedText);

                //            _measApp.Led.SendText("炉号：" + dataTable1.Rows[0]["FS_STOVENO"].ToString() + "，" + dataTable1.Rows[0]["FN_BILLETCOUNT"].ToString() + "/" + dataTable1.Rows[0]["FN_BILLETCOUNT"].ToString() + "支\n\n单支重量：" + this.txtZL.Text.Trim() + "吨\n\n" + "累计重量：" + dataTable1.Rows[0]["FN_TOTALWEIGHT"].ToString() + " 吨" + " " + str, "宋体", 9, 1, 4, 0);

                //            WriteLog(dataTable1.Rows[0]["FS_STOVENO"].ToString() + "-2");
                //        }
                //        else
                //        {
                //            strLedText = "当前暂无计量信息！";
                //            WriteLog(strLedText);
                //            _measApp.Led.SendText("当前暂无计量信息！", "宋体", 9, 1, 4, 0);

                //            WriteLog(completeLH + "无数据-3");
                //        }
                //    }
                //    System.Threading.Thread.Sleep(300);

                //}
                //液晶屏显示
               // WriteLog("1");
                if (m_iSelectedPound >= 0 && m_iSelectedPound < m_nPointCount && _measApp.Lcd != null && dataTable1.Rows.Count > 0)
                //m_iSelectedPound >= 0 && m_iSelectedPound < m_nPointCount && m_PoundRoomArray[m_iSelectedPound].UseLED
                {
                    WriteLog("2");
                    _measApp.Lcd.ClearScreen();//记得每次处理一组事务前先清屏
                    _measApp.Lcd.DrawPicture(1);
                    //WriteLog("炉号:" + dataTable1.Rows[0]["FS_STOVENO"].ToString().Trim());
                    _measApp.Lcd.WriteText(480, 240, Color.Yellow, "炉号:" + txtLH.Text.Trim());
                    //WriteLog("钢种:" + cbGZ.Text.Trim());
                    _measApp.Lcd.WriteText(480, 285, Color.Yellow, "钢种:" + cbGZ.Text.Trim());
                    WriteLog("规格:" + cbGG.Text.Trim());
                    _measApp.Lcd.WriteText(480, 334, Color.Yellow, "规格:" + cbGG.Text.Trim());

                    _measApp.Lcd.WriteText(480, 388, Color.Yellow, "长度:" + txtCD.Text.Trim());
                    //_measApp.Lcd.WriteText(480, 438, Color.Yellow, "当前支数:" + dataTable1.Rows[0]["FN_BILLETCOUNT"].ToString().Trim());
                    _measApp.Lcd.WriteText(480, 438, Color.Yellow, "重量:" + this.txtZL.Text.ToString());

                    //WriteLog("总支数:" + dataTable1.Rows[0]["FN_BILLETCOUNT"].ToString().Trim());
                    //_measApp.Lcd.WriteText(480, 534, Color.Yellow, "总支数:" + totalZS.ToString());
                    //WriteLog("总重量:" + dataTable1.Rows[0]["FN_TOTALWEIGHT"].ToString().Trim());
                    //_measApp.Lcd.WriteText(480, 581, Color.Yellow, "总重量:" + dataTable1.Rows[0]["FN_TOTALWEIGHT"].ToString().Trim());
                   // WriteLog("Over");
                }

                #region 自动播放语音

                    if (this.chkAutoSave.CheckState == CheckState.Unchecked) //手动保存模式下语音播放
                    {
                        AutoAlarmVoice();
                    }
                    else
                    {
                        m_MainThreadAlarmVoice = new AlarmVoice(AutoAlarmVoice);
                        Invoke(m_MainThreadAlarmVoice); //用委托播放声音  
                    }

                #endregion

                this.btnBC.Enabled = true;
                this.btnDS.Enabled = true;
                this.txtZL.Text = "";
                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 写日志信息到日志文件
        /// </summary>
        /// <param name="str"></param>
        private void WriteLog(string str)
        {
            if (System.IO.Directory.Exists(Constant.RunPath + "\\log") == false)
            {
                System.IO.Directory.CreateDirectory(Constant.RunPath + "\\log");
            }

            string strDate = System.DateTime.Now.ToString("yyyyMMdd");
            System.IO.TextWriter tw = new System.IO.StreamWriter(Constant.RunPath + "\\log\\EGGD_" + strDate + ".log", true);

            tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            tw.WriteLine(str);
            tw.WriteLine("\r\n");
            tw.Close();
        }

        //保存图片数据
        private void SavePictureData()
        {
            
            string strZL = "";
            if (dataTable3 != null && dataTable3.Rows != null && dataTable3.Rows.Count > 0 && dataTable6.Rows.Count>0)
            {
                strZL = dataTable6.Rows[0]["FS_STOVENO"] + ":\n" + "1\\1 块\n" + dataTable6.Rows[0]["FN_WEIGHT"].ToString() + " 吨";
                ultraGroupBox1.Refresh();
            }
            b.GraspAndSaveGPImage(VideoChannel1, VideoChannel3, strZYBH, strZL);
        }

        #endregion

        //查询预报数据并绑定到控件
        private void QueryAndBindYBData()
        {   
            //按计量点查询
            if (string.IsNullOrEmpty(_rollerNo))
            {
                MessageBox.Show("请选择计量点！");
                return;
            }

            string strSQL="SELECT A.FS_ORDER,A.FS_STOVENO,A.FS_STEELTYPE,A.FS_SPEC,round(A.FN_LENGTH,2) AS FN_LENGTH, " +
                "A.FS_ITEMNO,A. FS_MATERIAL,A.FS_SENDER,A.FS_RECEIVER, " +
                "decode(A.FN_ISRETURNBILLET,'0','否','1','是','否') FN_ISRETURNBILLET,A.FN_COUNT "+
                "FROM DT_BP_PLAN A,BT_POINT E,IT_FP_TECHCARD F WHERE A.FS_COMPLETEFLAG <> '1' and " +
				"A.FS_POINTID = E.FS_POINTCODE(+)  and A.FS_TECHCARDNO = F.FS_CARDNO order by a.fs_order asc";
            //查询数据
            string strLH = "";//uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu719修改
            //DataTable dtYB = new DataTable();
            dataTable3.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            ccp.SourceDataTable = dataTable3;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid5);
            valueWL = "";
            valueFH = "";
            valueSH = "";
            //绑定数据到控件
            if (dataTable3.Rows.Count > 0)
            {
                this.txtLH.Text = dataTable3.Rows[0]["FS_STOVENO"].ToString();                   //炉号
                this.txtDDH.Text = dataTable3.Rows[0]["FS_ORDERNO"].ToString();                  //订单号   
                this.txtCD.Text = dataTable3.Rows[0]["FN_LENGTH"].ToString();                    //长度
                this.cbGZ.Text = dataTable3.Rows[0]["FS_STEELTYPE"].ToString();                  //钢种
                this.cbGG.Text = dataTable3.Rows[0]["FS_SPEC"].ToString();                       //规格

                if (dataTable3.Rows[0]["FS_SENDER"].ToString() != "")
                    valueFH = dataTable3.Rows[0]["FS_SENDER"].ToString();                       //发货单位编号

                this.cbFHDW.Text = dataTable3.Rows[0]["FS_FAHUO"].ToString();                   //发货单位名称

                if (dataTable3.Rows[0]["FS_RECEIVER"].ToString() != "")
                    valueSH = dataTable3.Rows[0]["FS_RECEIVER"].ToString();                    //收货单位编号

                this.cbSHDW.Text = dataTable3.Rows[0]["FS_SHOUHUO"].ToString();                 //收货单位名称
                //cbWLMC.DataSource = dtYB;
                //this.cbWLMC.DisplayMember = "FS_MATERIALNAME";
                //this.cbWLMC.ValueMember = "FS_WL"; 
                if (dataTable3.Rows[0]["FS_MATERIAL"].ToString() != "")
                    valueWL = dataTable3.Rows[0]["FS_MATERIAL"].ToString();                       //物料编号 

                this.cbWLMC.Text = dataTable3.Rows[0]["FS_MATERIALNAME"].ToString();             //物料名称
                this.txtDDXMH.Text = dataTable3.Rows[0]["FS_ITEMNO"].ToString();                 //订单项目号
                totalZS = Convert.ToInt32(dataTable3.Rows[0]["FN_COUNT"].ToString());            //预报钢坯总支数
                ultraGrid5.Rows[0].Appearance.BackColor = Color.Pink;
                for (int i = 0; i < ultraGrid5.Rows.Count; i++)
                {
                    if (ultraGrid5.Rows[i].Cells["FN_ISRETURNBILLET"].Text.Trim() == "是")
                    {
                        ultraGrid5.Rows[i].Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                ClearControler();
            }
        }
        
         //设置计量点选中标志
        private void SetPointSign()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "core.mcms.billetTransfer.BilletInfo";
            ccp.MethodName = "SetPointSign";
            ccp.ServerParams = new object[] { pointcode };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        #region 文本框和下拉框事件集
        
        //下拉框选项改变事件
        private void cbWLMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueWL = cbWLMC.SelectedValue.ToString().Trim();
        }

        //private void cbGZ_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CursorMoving();

        //}
       
        //private void cbLiuX_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CursorMoving();
        //}


        private void cbFHDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueFH = cbFHDW.SelectedValue.ToString().Trim();
        }

        private void cbSHDW_SelectedIndexChanged(object sender, EventArgs e)
        {
            valueSH = cbSHDW.SelectedValue.ToString().Trim();
        }

        //private void cbBC_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CursorMoving();
        //}
        
        //private void cbGG_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CursorMoving();
        //}
        #endregion

      
        //完炉查询
        private void CompletationQuery()
        {
            string strLH = this.txtLH.Text.ToString();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "core.mcms.billetTransfer.BilletInfo";
            ccp.MethodName = "QueryCompleteflag";
            ccp.ServerParams = new object[] { strLH };
            DataTable dtWLFlag = new DataTable();
            ccp.SourceDataTable = dtWLFlag;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dtWLFlag.Rows.Count > 0)
            {
                cpflg = dtWLFlag.Rows[0]["FS_COMPLETEFLAG"].ToString();
            }
        }

        private void BilletInfo_KeyPress(object sender, KeyPressEventArgs e)
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

        //add 5月 6日
        //打开选择更多下拉选项子窗体
        private void moreBtn_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(),this.ob);
            frm.Owner = this;
            //frm.ShowDialog();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRow dr;
                if (Btn.Tag.ToString() == "Material")
                {
                    //插入物料内存主表
                    dr = m_MaterialTable.NewRow();
                    dr["fs_materialname"] = frm.strReturnName;
                    dr["FS_MATERIALNO"] = frm.strReturnCode;
                    dr["FS_PointNo"] = pointcode;
                    dr["FS_HELPCODE"] = frm.strReturnHelpCode;
                    m_MaterialTable.Rows.Add(dr);
                    //插入物料名称下拉框绑定数据表
                    dr = tempMaterial.NewRow();
                    dr["fs_materialname"] = frm.strReturnName;
                    dr["FS_MATERIALNO"] = frm.strReturnCode;
                    dr["FS_HELPCODE"] = frm.strReturnHelpCode;
                    tempMaterial.Rows.Add(dr);

                    cbWLMC.Text = frm.strReturnName;
                }
                else if (Btn.Tag.ToString() == "Sender")
                {
                    //插入发货单位内存主表
                    dr = m_SenderTable.NewRow();
                    dr["FS_MEMO"] = frm.strReturnName;
                    dr["FS_SENDER"] = frm.strReturnCode;
                    dr["FS_PointNo"] = pointcode;
                    dr["FS_HELPCODE"] = frm.strReturnHelpCode;
                    m_SenderTable.Rows.Add(dr);
                    //插入发货单位下拉框绑定数据表
                    dr = tempSender.NewRow();
                    dr["FS_MEMO"] = frm.strReturnName;
                    dr["FS_SENDER"] = frm.strReturnCode;
                    dr["FS_HELPCODE"] = frm.strReturnHelpCode;
                    tempSender.Rows.Add(dr);

                    cbFHDW.Text = frm.strReturnName;
                }
                else if (Btn.Tag.ToString() == "Receiver")
                {
                    //插入收货单位内存主表
                    dr = m_RecevierTable.NewRow();
                    dr["FS_MEMO"] = frm.strReturnName;
                    dr["FS_Receiver"] = frm.strReturnCode;
                    dr["FS_PointNo"] = pointcode;
                    dr["FS_HELPCODE"] = frm.strReturnHelpCode;
                    m_RecevierTable.Rows.Add(dr);
                    //插入收货单位下拉框绑定数据表
                    dr = tempReveiver.NewRow();
                    dr["FS_MEMO"] = frm.strReturnName;
                    dr["FS_Receiver"] = frm.strReturnCode;
                    dr["FS_HELPCODE"] = frm.strReturnHelpCode;
                    tempReveiver.Rows.Add(dr);

                    cbSHDW.Text = frm.strReturnName;
                }
            }
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

            //磅房对应收货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("FS_Receiver".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("fs_memo".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_RecevierTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_RecevierTable.Columns.Add(dc);

            //磅房对应发货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_SENDER".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_MEMO".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_SenderTable.Columns.Add(dc);

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
            m_MaterialTable.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_13.SELECT",new ArrayList() };
            ccp.SourceDataTable = this.m_MaterialTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对收货单位信息  ,add by luobin 
        private void DownLoadReceiver()
        {
            m_RecevierTable.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_14.SELECT", new ArrayList() };
            ccp.SourceDataTable = this.m_RecevierTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对发货单位信息  ,add by luobin 
        private void DownLoadSender()
        {
            m_SenderTable.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_15.SELECT", new ArrayList() };
            ccp.SourceDataTable = this.m_SenderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }


        //下载磅房对钢种信息  ,add by luobin 
        private void DownLoadSteelType()
        {
            m_SteelTypeTable.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_16.SELECT", new ArrayList() };
            ccp.SourceDataTable = this.m_SteelTypeTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对应规格信息  ,add by luobin 
        private void DownLoadSepc()
        {
            m_SpecTable.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_17.SELECT", new ArrayList() };
            ccp.SourceDataTable = this.m_SpecTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

        }

        //下载流向信息  ,add by luobin 
        private void DownLoadFlow()
        {
            m_FlowTable.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_18.SELECT", new ArrayList() };
            ccp.SourceDataTable = this.m_FlowTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            DataRow drz = this.m_FlowTable.NewRow();
            m_FlowTable.Rows.InsertAt(drz, 0);
            cbLiuX.DataSource = this.m_FlowTable;
            cbLiuX.DisplayMember = "FS_TYPENAME";
            cbLiuX.ValueMember = "FS_TYPECODE";

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
            //cbSpec.ValueMember = "FS_SPEC";

        }

        private void InitConfig()
        {
            //物料下拉框
            this.panel11.Controls.Add(m_List);
            m_List.Size = new Size(141, 80);
            m_List.Visible = false;
            m_List.ScrollAlwaysVisible = true;
            m_List.BringToFront();
            m_List.Click += new EventHandler(m_List_Click);
            m_List.KeyPress += new KeyPressEventHandler(m_List_KeyPress);
            m_List.Leave += new EventHandler(m_List_Leave);

            cbWLMC.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbWLMC.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbGZ.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbGZ.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbGG.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbGG.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbFHDW.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbFHDW.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbSHDW.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbSHDW.AutoCompleteSource = AutoCompleteSource.ListItems;

            cbLiuX.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbLiuX.AutoCompleteSource = AutoCompleteSource.ListItems;

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
                case "Receiver":
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
                    this.cbLiuX.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbLiuX.Focus();
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
                case "Receiver":
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

        #endregion

        #region 新增
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
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbMaterial_TextChanged(object sender, EventArgs e)
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
                    valueWL = ""; //ADD
                    return;
                }
            }

            m_ListType = "Material";
            m_List.Location = new System.Drawing.Point(69, 77);

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

        private void cbSender_TextChanged(object sender, EventArgs e)
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
                    valueFH = ""; //ADD
                    return;
                }
            }

            m_ListType = "Sender";
            m_List.Location = new System.Drawing.Point(69, 173);

            string text = this.cbFHDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempSender.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_Memo"].ToString());
            }
            m_List.Visible = true;
        }

        private void cbReceiver_TextChanged(object sender, EventArgs e)
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
                    valueSH = ""; //ADD
                    return;
                }
            }

            m_ListType = "Receiver";
            m_List.Location = new System.Drawing.Point(303, 173);

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

        private void BeginPoundRoomThread()
        {
            m_bRunningForPoundRoom = true;
            m_bPoundRoomThreadClosed = false;
            m_hThreadForPoundRoom = new System.Threading.Thread(new System.Threading.ThreadStart(PoundRoomThread));
            m_hThreadForPoundRoom.Start();
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
                }

                System.Threading.Thread.Sleep(100);
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

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            //    if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //    {
            //        return;
            //    }

            if (_measApp == null)
            {
                _measApp = new CoreApp();
                _measApp.Params = m_Points[iPoundRoom];
                _measApp.Params.FS_PRINTERNAME = new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName;
                _measApp.Init();
                //_measApp.Weight.WeightChanged += new Core.Sip.Client.Meas.WeightChangedEventHandler(OnWeightChanged);
                //_measApp.Weight.WeightCompleted += new Core.Sip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted);
                _measApp.VideoChannel[0] = _measApp.Dvr.SDK_RealPlay(1, 0, (int)VideoChannel1.Handle);
                _measApp.VideoChannel[1] = _measApp.Dvr.SDK_RealPlay(2, 0, (int)VideoChannel2.Handle);
                _measApp.VideoChannel[2] = _measApp.Dvr.SDK_RealPlay(3, 0, (int)VideoChannel3.Handle);
                _measApp.Run();
            }
        }

        /// <summary>
        /// 关闭计量点的硬盘录像机
        /// </summary>
        /// <param name="iPoundRoom">计量点索引</param>
        private void RecordClose(int iPoundRoom)
        {
            int i = iPoundRoom;

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }
            //if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //{
            //    return;
            //}

            //关闭语音对讲
            if (_isTalk && _talkId > 0)
            {
                _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
                _measApp.Dvr.SDK_StopTalk();
                _isTalk = false;
                _talkId = -1;

                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }

            _measApp.Dvr.SDK_CloseSound(_measApp.VideoChannel[0]);

            //关闭第1通道御览
            if (_measApp.VideoChannel[0] > 0)
            {
                _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
                _measApp.VideoChannel[0] = 0;
                VideoChannel3.Refresh();
            }

            //关闭第2通道御览
            if (_measApp.VideoChannel[1] > 0)
            {
                _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[1]);
                _measApp.VideoChannel[1] = 0;
                VideoChannel1.Refresh();
            }

            //关闭第3通道御览
            if (_measApp.VideoChannel[2] > 0)
            {
                _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[2]);
                _measApp.VideoChannel[2] = 0;
                VideoChannel2.Refresh();
            }
        }

        public void StopPoundRoomThread()
        {
            m_bRunningForPoundRoom = false;//停止计量点线程

            //最多等待5秒，让计量点线程自动退出
            for (int nCount = 0; nCount < 20; nCount++)
            {
                if (m_bPoundRoomThreadClosed == true)
                    break;
                System.Threading.Thread.Sleep(100);
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

            if (_isTalk && _talkId > 0)//正在对讲，关闭
            {
                _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
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

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            //if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //{
            //    return;
            //}

            if (iChannel == 0)
            {
                bool bTalkNow = false;
                //关闭语音对讲
                if (_isTalk && _talkId > -1)
                {
                    bTalkNow = true;
                    _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
                    _measApp.Dvr.SDK_StopTalk();
                    _isTalk = false;
                    _talkId = -1;

                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                }

                //关闭小图片监视,打开大图片监视
                if (_measApp.VideoChannel[0] >= 0)
                {
                    _measApp.Dvr.SDK_CloseSound(_measApp.VideoChannel[0]);

                    _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[0]);
                    _measApp.VideoChannel[0] = 0;
                    BigChannel = _measApp.Dvr.SDK_RealPlay(1, 0, (int)picFDTP.Handle);

                    if (bTalkNow)//如果放大前正在对讲，则再次打开
                    {
                        _talkId = _measApp.Dvr.SDK_StartTalk();
                        _measApp.Dvr.SDK_SetVolume(65500);
                        _measApp.Dvr.SDK_RealPlay(1, 0, (int)picFDTP.Handle);
                        _isTalk = true;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                    }

                    _measApp.Dvr.SDK_OpenSound(BigChannel);
                    _measApp.Dvr.SDK_SetVolume(65500);

                }
            }
            else if (iChannel == 1)
            {
                //关闭小图片监视,打开大图片监视
                if (_measApp.VideoChannel[1] > 0)
                {
                    _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[1]);
                    _measApp.VideoChannel[1] = 0;
                    BigChannel = _measApp.Dvr.SDK_RealPlay(2, 0, (int)picFDTP.Handle);
                }

            }
            else if (iChannel == 2)
            {
                //关闭小图片监视,打开大图片监视
                if (_measApp.VideoChannel[2] > 0)
                {
                    _measApp.Dvr.SDK_StopRealPlay(_measApp.VideoChannel[2]);
                    _measApp.VideoChannel[2] = 0;
                    BigChannel = _measApp.Dvr.SDK_RealPlay(3, 0, (int)picFDTP.Handle);
                }
            }

            m_CurSelBigChannel = BigChannel > 0 ? iChannel : -1;

            if (BigChannel > 0)
            {
                picFDTP.Width = VideoChannel3.Width * 2;
                picFDTP.Height = VideoChannel3.Height * 2;
                picFDTP.Visible = true;
                picFDTP.BringToFront();
            }
        }

        /// <summary>
        /// 关闭大图监视，还原小图监视
        /// </summary>
        private void CloseBigPicture()
        {
            int i = m_iSelectedPound;

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            //if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            //{
            //    return;
            //}

            if (BigChannel > 0 && m_CurSelBigChannel >= 0)
            {
                picFDTP.Visible = false;
                _measApp.Dvr.SDK_StopRealPlay(BigChannel);
                BigChannel = 0;

                if (m_CurSelBigChannel == 0)
                {
                    bool bTalkNow = false;
                    //关闭语音对讲
                    if (_isTalk && _talkId > 0)
                    {
                        bTalkNow = true;

                        _measApp.Dvr.SDK_StopTalk();
                        _isTalk = false;
                        _talkId = -1;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                    }

                    _measApp.VideoChannel[0] = _measApp.Dvr.SDK_RealPlay(1, 0, (int)VideoChannel1.Handle);
                    _measApp.Dvr.SDK_OpenSound(_measApp.VideoChannel[0]);
                    _measApp.Dvr.SDK_SetVolume(65500);


                    if (bTalkNow)//如果放大前正在对讲，则再次打开
                    {
                        _talkId = _measApp.Dvr.SDK_StartTalk();
                        _measApp.Dvr.SDK_SetVolume(65500);

                        _isTalk = true;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                    }
                }
                else if (m_CurSelBigChannel == 1)
                {
                    _measApp.VideoChannel[1] = _measApp.Dvr.SDK_RealPlay(2, 0, (int)VideoChannel2.Handle);
                }
                else if (m_CurSelBigChannel == 2)
                {
                    _measApp.VideoChannel[2] = _measApp.Dvr.SDK_RealPlay(3, 0, (int)VideoChannel3.Handle);
                }

                m_CurSelBigChannel = -1;
                picFDTP.Refresh();
            }
            else
            {
                picFDTP.Visible = false;
                picFDTP.Refresh();
            }
        }

        private void ultraGrid2_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            if (m_iSelectedPound == ultraGrid2.ActiveRow.Index)
            {
                return;
            }
           
            this.Cursor = Cursors.WaitCursor;
            //关闭前一个选择的计量点语音视频
            RecordClose(m_iSelectedPound);

            ClearControler();

            int iSelectIndex = ultraGrid2.ActiveRow.Index;
            m_iSelectedPound = iSelectIndex;

            //打开当前选择的计量点语音视频
            RecordOpen(iSelectIndex);

            //初始化接口程序
            if (_measApp != null)
            {
                _measApp.Finit();
            }
            if (_measApp == null)
            {
                _measApp = new CoreApp();
            }
            _measApp.Params = m_Points[m_iSelectedPound];
            _measApp.Params.FS_PRINTERNAME = new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName;
            _measApp.Init();
            _measApp.Weight.WeightChanged += new Core.Sip.Client.Meas.WeightChangedEventHandler(OnWeightChanged);
            _measApp.Weight.WeightCompleted += new Core.Sip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted);
            //_measApp.Weight.WeightChanged += new Core.Sip.Client.Meas.WeightChangedEventHandler(OnWeightChanged);
            //_measApp.Weight.WeightCompleted += new Core.Sip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted);
            _measApp.VideoChannel[0] = _measApp.Dvr.SDK_RealPlay(1, 0, (int)VideoChannel1.Handle);
            _measApp.VideoChannel[1] = _measApp.Dvr.SDK_RealPlay(2, 0, (int)VideoChannel2.Handle);
            _measApp.VideoChannel[2] = _measApp.Dvr.SDK_RealPlay(3, 0, (int)VideoChannel3.Handle);
            _measApp.Run();

            this.txtJLD.Text = _measApp.Params.FS_POINTNAME;
            pointcode = _measApp.Params.FS_POINTCODE;

            //辊道编号
            _rollerNo = "1";
            //ControlerInt();//绑定下拉框   
            BandPointMaterial(pointcode);
            BandPointReceiver(pointcode);
            BandPointSender(pointcode);
            BandPointSteelType(pointcode);
            BandPointSpec(pointcode);
            QueryAndBindYBData();
            //this.txtLH1.Focus();
            
            this.Cursor = Cursors.Default;
            //启动线程
            BeginPoundRoomThread();

            m_flag = true;
            m_thread = new Thread(new ThreadStart(QueryTread));
            m_thread.Start();

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

            txtXSZL.Text = (d_weight / 1000).ToString();
            if (d_weight <= _measApp.Weight.MinWeight)
            {
                lbWD.Text = "空磅";
                txtZL.Text = "";

                //if (_measApp.IoLogik != null)
                //{
                //    bool[] dostatus = _measApp.IoLogik.ReadDO();
                //    if (dostatus[0])
                //    {
                //        _measApp.IoLogik.DO[0] = false;
                //        _measApp.IoLogik.WriteDO();
                //    }
                //}

                //btnBC.Enabled = false;
            }
            else
            {
                lbWD.Text = "不稳定";
                lbYS.ForeColor = Color.Red;
                txtZL.Text = "";
                btnBC.Enabled = true;
            }
        }

        private void HandleWeightCompelte(string weight)
        {
            _saved = false;
            // txtXSZL.Text = weight;
            HandleMeterData(weight);

        }
        public void OnWeightCompleted(object sender, WeightEventArgs e)
        {
            this.Invoke(new setText(HandleWeightCompelte), new object[] { e.Value.ToString() });
        }

        /// <summary>
        /// 处理仪表采集数据
        /// </summary>
        /// <param name="iPoundRoom">计量点索引，从0开始</param>
        private void HandleMeterData(string weight)
        {
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
                    double d_weight = 0;
                    double.TryParse(weight, out d_weight);

                    txtXSZL.Text = (d_weight / 1000).ToString();
                    lbWD.Text = "稳定";
                    lbYS.ForeColor = Color.Green;
                    lbWD.Refresh();
                    lbYS.Refresh();

                    txtZL.Text = (d_weight / 1000).ToString();
                    if (chkAutoSave.Checked)
                    {
                        this.Save();
                        QueryCount();
                    }
                    //if (chkAutoSave.Checked)//自动保存
                    //{
                    //    //SaveWeightData();

                    //    btnBC.Enabled = false;
                    //}
                    //else
                    //{
                    //    btnBC.Enabled = true;
                    //}
                }
            }
        }
        #endregion

        public void ClearWeightFun()
        {
            Thread.Sleep(3000);
            m_MainThreadClearWeight = new ClearWeight(ClearXSZL);
            Invoke(m_MainThreadClearWeight); //用委托播放声音  
        }

        private void ClearXSZL()
        {
            txtXSZL.Text = "0000";
        }

        private void QueryCount()
        {
            string strSQL = @"SELECT COUNT(FS_STOVENO) STEELCOUNT, SUM(FN_WEIGHT) SUMWEIGHT FROM dt_boardweightmain";
            strSQL += " WHERE FD_WEIGHTTIME > SYSDATE-0.5 AND FS_SHIFT = '" + UserInfo.GetUserOrder() + "'";
            strSQL += " AND FS_TERM = '" + UserInfo.GetUserGroup() + "' AND FS_PERSON = '" + UserInfo.GetUserName() + "'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            DataTable dt = new DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ultraTextEditor5.Text = dr["STEELCOUNT"].ToString();
                ultraTextEditor3.Text = dr["SUMWEIGHT"].ToString();
            }
            else
            {
                ultraTextEditor5.Text = "0";
                ultraTextEditor3.Text = "0";
            }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "YYDJ":
                    {
                        HandleTalk(m_iSelectedPound);

                        break;
                    }
                case "YCJLTX":
                    {
                        //QueryAndCloseYCPic();
                        break;
                    }
                case "CloseLED":
                    {
                        CloseLEDPower(m_iSelectedPound);
                        break;
                    }
                case "OpenLED":
                    {
                        OpenLEDPower(m_iSelectedPound);
                        break;
                    }
                case "Refresh":
                    {
                        //string nowstoveno = this.txtLH.Text.Trim();
                        this.txtLH.Text = ""; 
                        QueryAndBindYBData();
                        break;
                    }
                case "btCorrention":
                    {
                        if (m_iSelectedPound == -1)
                        {
                            MessageBox.Show("请先选择计量点！");
                            //this.txtZL.Text = "";
                            return;
                        }

                    
                        string operater = UserInfo.GetUserName(); //操作员
                        string shift = UserInfo.GetUserOrder();//班次
                        string term = UserInfo.GetUserGroup();//班别
                        correntionWeight = txtXSZL.Text.Trim();//重量,字符串
                        correntionWeightNo = Guid.NewGuid().ToString();
                        if(!baseinfo.correntionInformation(correntionWeightNo, pointcode, operater, shift, term, correntionWeight))
                        {
                            return;
                        }
                        CorrentionSaveImage();
                        MessageBox.Show("校秤完成！！！");
                        break;
                    }
            }
        }

        private void VideoChannel1_Click(object sender, EventArgs e)
        {
            panel3.BorderStyle = BorderStyle.Fixed3D;
            k = 0;

            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel5.BorderStyle = BorderStyle.FixedSingle;
        }

        private void VideoChannel2_Click(object sender, EventArgs e)
        {
            panel4.BorderStyle = BorderStyle.Fixed3D;
            k = 1;

            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel3.BorderStyle = BorderStyle.FixedSingle;
        }

        private void VideoChannel3_Click(object sender, EventArgs e)
        {
            panel5.BorderStyle = BorderStyle.Fixed3D;
            k = 2;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel4.BorderStyle = BorderStyle.FixedSingle;
        }

        private void VideoChannel1_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(0);
        }

        private void VideoChannel2_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(1);
        }

        private void VideoChannel3_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(2);
        }

        private void picFDTP_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
        }

        private void txtLH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                CompletationQuery();
                if (cpflg == "1")
                {
                    cpflg = "";
                    ClearControler();
                    this.txtLH.Focus();
                    this.btnDS.Enabled = false;
                    this.btnBC.Enabled = true;
                    MessageBox.Show("该炉号已经完炉，请输入新炉号！");
                    return;
                }
                else
                {
                    QueryAndBindYBData();
                    this.btnDS.Enabled = true;
                }
            }
        }

        private void ultraGrid5_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid5.ActiveRow;
            if (ugr == null) return;
            this.txtLH.Text = ugr.Cells["FS_STOVENO"].Text.Trim();                   //炉号
            this.txtDDH.Text = ugr.Cells["FS_ORDERNO"].Text.Trim();                  //订单号   
            this.txtCD.Text = ugr.Cells["FN_LENGTH"].Text.Trim();                    //长度
            this.cbGZ.Text = ugr.Cells["FS_STEELTYPE"].Text.Trim();                  //钢种
            this.cbGG.Text = ugr.Cells["FS_SPEC"].Text.Trim();                       //规格

            if (ugr.Cells["FS_SENDER"].Text.Trim() != "")
                valueFH = ugr.Cells["FS_SENDER"].Text.Trim();                       //发货单位编号

            this.cbFHDW.Text = ugr.Cells["FS_FAHUO"].Text.Trim();                   //发货单位名称

            if (ugr.Cells["FS_RECEIVER"].Text.Trim() != "")
                valueSH = ugr.Cells["FS_RECEIVER"].Text.Trim();                    //收货单位编号

            this.cbSHDW.Text = ugr.Cells["FS_SHOUHUO"].Text.Trim();                 //收货单位名称
            //cbWLMC.DataSource = dtYB;
            //this.cbWLMC.DisplayMember = "FS_MATERIALNAME";
            //this.cbWLMC.ValueMember = "FS_WL"; 
            if (ugr.Cells["FS_MATERIAL"].Text.Trim() != "")
                valueWL = ugr.Cells["FS_MATERIAL"].Text.Trim();                       //物料编号 

            this.cbWLMC.Text = ugr.Cells["FS_MATERIALNAME"].Text.Trim();             //物料名称
            this.txtDDXMH.Text = ugr.Cells["FS_ITEMNO"].Text.Trim();                 //订单项目号
            totalZS = Convert.ToInt32(ugr.Cells["FN_COUNT"].Text.Trim());
            QueryAndBindJLCZData();
        }

        private void ultraGrid4_ClickCell_1(object sender, ClickCellEventArgs e)
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
            txtDDH.Focus();
     
        }

        private void OpenLEDPower(int iPoundRoom)
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
            if (ultraToolbarsManager1.Toolbars[0].Tools["OpenLED"].SharedProps.Caption.Trim() == "打开LED显示")
            {
                _measApp.Led.SetPower(1);
                ultraToolbarsManager1.Toolbars[0].Tools["OpenLED"].SharedProps.Caption = "已打开LED显示";
                ultraToolbarsManager1.Toolbars[0].Tools["CloseLED"].SharedProps.Caption = "关闭LED显示";

            }
        }

        private void CloseLEDPower(int iPoundRoom)
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

            if (ultraToolbarsManager1.Toolbars[0].Tools["CloseLED"].SharedProps.Caption.Trim() == "关闭LED显示")
            {
                _measApp.Led.SetPower(0);
                ultraToolbarsManager1.Toolbars[0].Tools["CloseLED"].SharedProps.Caption = "已关闭LED显示";
                ultraToolbarsManager1.Toolbars[0].Tools["OpenLED"].SharedProps.Caption = "打开LED显示";
            }
           
        }

        private bool Check1()
        {
            bool reVal = false;

            if (txtJLD.Text == "")
            {
                MessageBox.Show("请选择计量点");
                reVal = true;
            }
            if (this.ultraGrid5.Rows.Count == 0)
            {
                //MessageBox.Show("当前没有预报信息");
                //this.ultraGrid5.Focus();
                reVal = true;
            }
            return reVal;
        }

        private bool Check()
        {
            bool reVal = false;
           
            if (txtJLD.Text == "")
            {
                MessageBox.Show("请选择计量点");
                reVal = true;
            }
            if (this.txtLH.Text.Length != 9)
            {
                MessageBox.Show("请输入正确的炉号");
                this.txtLH.Focus();
                reVal = true;
            }
            if (txtLH.Text == "")
            {
                MessageBox.Show("请输入炉号");
                this.txtLH.Focus();
                reVal = true;
            }
            if (this.txtCD.Text == "" || !IsNumber(this.txtCD.Text.Trim()))
            {
                MessageBox.Show("请输入正确的长度");
                this.txtCD.Focus();
                reVal = true;
            }
            return reVal;
        }

        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            //保存按钮是否可用
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

        private void btLLBC_Click_1(object sender, EventArgs e)
        {
            this.chkAutoSave.Checked = false;
            strLLJZ = "1";
            Save();
            QueryCount();
            strLLJZ = "0";
        }

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
            dt.Clear();
            ccp.SourceDataTable = this.dt;
            ccp.IfShowErrMsg = false;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (dt.Rows.Count > 0)
                {
                   
                    this.txtDDXMH.Text = dt.Rows[0]["FS_ITEMNO"].ToString();

                    DataRow dr;
                    //插入物料内存主表
                    dr = m_MaterialTable.NewRow();
                    dr["fs_materialname"] = dt.Rows[0]["FS_MATERIALNAME"].ToString();
                    dr["FS_MATERIALNO"] = dt.Rows[0]["FS_WL"].ToString();
                    dr["FS_PointNo"] = pointcode;
                    dr["FS_HELPCODE"] = "";
                    m_MaterialTable.Rows.Add(dr);
                    //插入物料名称下拉框绑定数据表
                    dr = tempMaterial.NewRow();
                    dr["fs_materialname"] = dt.Rows[0]["FS_MATERIALNAME"].ToString();
                    dr["FS_MATERIALNO"] = dt.Rows[0]["FS_WL"].ToString();
                    dr["FS_HELPCODE"] = "";
                    tempMaterial.Rows.Add(dr);

                    this.cbWLMC.Text = dt.Rows[0]["FS_MATERIALNAME"].ToString();
                    valueWL = dt.Rows[0]["FS_WL"].ToString();
                    if (dt.Rows[0]["FS_STEELTYPE"].ToString() != "")
                        this.cbGZ.Text = dt.Rows[0]["FS_STEELTYPE"].ToString();
                    if (dt.Rows[0]["FS_SPEC"].ToString() != "")
                        this.cbGG.Text = dt.Rows[0]["FS_SPEC"].ToString();
                    if (dt.Rows[0]["FN_LENGTH"].ToString() != "")
                        this.txtCD.Text = dt.Rows[0]["FN_LENGTH"].ToString();
                }
               
            }
            else
                MessageBox.Show("订单下载失败！");

            this.Cursor = Cursors.Default;
            Constant.WaitingForm.ShowToUser = false;
            Constant.WaitingForm.Close();
        }

        /// <summary>
        /// 查询上炉和上上炉的信息
        /// </summary>
        private void QueryLastStoveData()
        {
            try
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "query";
                ccp.ServerParams = new object[] { "BILLETINFO_06.SELECT",new ArrayList() };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
               

                if (ccp.ReturnCode != 0)
                {
                    MessageBox.Show(ccp.ReturnInfo);
                }
                else
                {
                    if (dt_temp.Rows.Count > 1)
                    {
                        this.tbx_lastStoveNo.Text = dt_temp.Rows[0]["FS_STOVENO"].ToString();
                        this.tbx_lastBilletCount.Text = dt_temp.Rows[0]["FN_BILLETCOUNT"].ToString();
                        this.tbx_lastTotalWeight.Text = dt_temp.Rows[0]["FN_TOTALWEIGHT"].ToString();
                        this.tbx_llastStoveNo.Text = dt_temp.Rows[1]["FS_STOVENO"].ToString();
                        this.tbx_llastBilletCount.Text = dt_temp.Rows[1]["FN_BILLETCOUNT"].ToString();
                        this.tbx_llastTotalWeight.Text = dt_temp.Rows[1]["FN_TOTALWEIGHT"].ToString();
                    }
                    else if (dt_temp.Rows.Count == 1)
                    {
                        this.tbx_lastStoveNo.Text = dt_temp.Rows[0]["FS_STOVENO"].ToString();
                        this.tbx_lastBilletCount.Text = dt_temp.Rows[0]["FN_BILLETCOUNT"].ToString();
                        this.tbx_lastTotalWeight.Text = dt_temp.Rows[0]["FN_TOTALWEIGHT"].ToString();
                        this.tbx_llastStoveNo.Text = "";
                        this.tbx_llastBilletCount.Text = "";
                        this.tbx_llastTotalWeight.Text = "";
                    }
                    else
                    {
                        this.tbx_lastStoveNo.Text = "";
                        this.tbx_lastBilletCount.Text = "";
                        this.tbx_lastTotalWeight.Text = "";
                        this.tbx_llastStoveNo.Text = "";
                        this.tbx_llastBilletCount.Text = "";
                        this.tbx_llastTotalWeight.Text = "";
                    }
                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        /// <summary>
        /// 定单号输入验证
        /// </summary>
        private void DownOrderInfo()
        {
            if (txtDDH.Text.Trim() != "" && txtDDH.Text.Trim().Length != 12)
            {
                MessageBox.Show("请输入正确的订单号！");
                txtDDH.Focus();
                return;
            }
            else
            {
                if (txtDDH.Text.Trim() != "")
                    GetOrderInfo(txtDDH.Text.Trim());
            }
        }

        private void txtDDH_Leave(object sender, EventArgs e)
        {
            if (m_iSelectedPound < 0 )
            {
                MessageBox.Show("请先选择计量点！");
                return;
            }
            DownOrderInfo();
        }

        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {
            this.btXF.Visible = true;
        }

        private void btXF_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr = this.ultraGrid1.ActiveRow;
            if (ugr == null)
            {
                MessageBox.Show("请选择要维护的记录！");
                return;
            }
            string p_FS_WEIGHTNO = ugr.Cells["FS_WEIGHTNO"].Text.Trim().ToString();
            double p_FN_NETWEIGHT = Convert.ToDouble(ugr.Cells["FS_WEIGHTNO"].Text.Trim().ToString());
            
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "core.mcms.billetTransfer.BilletInfo";
            ccp.MethodName = "RebuildData";
            ccp.ServerParams = new object[] { p_FS_WEIGHTNO, p_FN_NETWEIGHT,_rollerNo };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            QueryAndBindJLCZData();
            this.btXF.Visible = false;
            
        }

        private void btChanShift_Click_1(object sender, EventArgs e)
        {
            string args = "";
            if (pointcode.Equals("K15"))
            {
                args = "bcgdjly";
            }
            else if (pointcode.Equals("K151"))
            {
                args = "xcgdjly";
            }
            ChangeShift winfrm = new ChangeShift(this.ob,args);
            winfrm.Owner = this;
            winfrm.StartPosition = FormStartPosition.CenterParent;

            if (winfrm.ShowDialog() == DialogResult.OK)
            {
                UserInfo.SetUserName(winfrm.strreturnname);
                UserInfo.SetUserOrder(winfrm.strreturnshife);
                UserInfo.SetUserGroup(winfrm.strreturnterm);
                p_shiftdate = winfrm.strreturndate;
            }
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

            //------------------------------------------------
            string stRunPath = System.Environment.CurrentDirectory;
            string poundPicPath = stRunPath + "\\JZPicture\\";
            string poundPicFilePath = poundPicPath + strNumber + "corrention1.bmp";

            if (i < 0 || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP) || _measApp.Led == null)
            {
                return;
            }

            try
            {
                _measApp.Dvr.SDK_CapturePicture(_measApp.VideoChannel[0], poundPicFilePath);
                System.Threading.Thread.Sleep(200);

            }
            catch (System.Exception error)
            {
                MessageBox.Show("抓校秤图：" + error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                baseinfo.GraspAndSaveCorrentionImage(strNumber + "corrention1.bmp", correntionWeightNo, correntionWeight);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("baseinfo.GraspAndSaveCorrentImage截图出错:" + ex1.Message);
            }
        }

        private void ultraOptionSet1_ValueChanged(object sender, EventArgs e)
        {
            if (ultraOptionSet1.CheckedIndex == 0 && ultraGrid2.Rows.Count > 0)
            {
                ultraGrid2.Rows[0].Activate();
                ultraGrid2_AfterSelectChange(ultraGrid2, new AfterSelectChangeEventArgs(Type.GetType("")));
            }
            if (ultraOptionSet1.CheckedIndex == 1 && ultraGrid2.Rows.Count > 1)
            {
                ultraGrid2.Rows[1].Activate();
                ultraGrid2_AfterSelectChange(ultraGrid2, new AfterSelectChangeEventArgs(Type.GetType("")));
            }
        }

        private void picFDTP_MouseDown(object sender, MouseEventArgs e)
        {
            _mousedown = true;
            _moveLeft = e.X;
            _moveTop = e.Y;
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

        private void picFDTP_MouseLeave(object sender, EventArgs e)
        {
            _mousedown = false;
        }

        private void picFDTP_MouseUp(object sender, MouseEventArgs e)
        {
            _mousedown = false;
        }

        private void btnQL_Click_1(object sender, EventArgs e)
        {
            this.txtXSZL.Text = "0";
        }

        private void QueryTread()
        {
            while (m_flag)
            {
                m_QueryYBThreadDelegate = new QueryYBThreadDelegate(QueryAndBindYBData);
                Invoke(m_QueryYBThreadDelegate);

                System.Threading.Thread.Sleep(20000);
            }

        }
    }
}
