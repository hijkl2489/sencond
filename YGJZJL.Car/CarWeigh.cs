using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CoreFS.CA06;
using YGJZJL.CarSip.Client.App;
using YGJZJL.CarSip.Client.Meas;
using YGJZJL.PublicComponent;
using Infragistics.UltraChart.Resources.Appearance;
using System.IO.Ports;

namespace YGJZJL.Car
{
    public partial class CarWeigh : FrmBase
    {
        #region 全局变量
        private CarWeightDataManage _carWeightDatamanage = null;
        private CoreApp _measApp = null;
        private int m_iSelectedPound = -1;//选择的计量点索引，用于计量点切换时RecordClose等方法使用
        private string _curPath = System.Environment.CurrentDirectory;
        private bool _isTalk = false;
        private int _talkId = -1;
        private int _bigChannelId = -1;
        private int _preBigChannel = -1;

        private DataTable _dtFirstWeighData = new DataTable();

        private string _preWeightNo = "";//上次计量编号

        private CoreApp[] _measApps = null;
        private CarNumericSeries[] _series = null;//动态曲线数据数组

        private BaseInfo _baseInfo = new BaseInfo();
        private MessageForm messForm = new MessageForm();
        private bool _isReWriteCard = false;//是否进行补写卡

        #endregion

        public CarWeigh()
        {
            InitializeComponent();
        }

        private void CarWeigh_Load(object sender, EventArgs e)
        {
            if (!UserInfo.GetUserID().Equals("admin"))
            {
                button2.Visible = false;
            }
            if (!UserInfo.GetRole().Equals("admin") && !UserInfo.GetRole().Equals("bzz") && !CustomInfo.Equals("device"))
            {
                rtuControl1.ShowRestartControl = false;
            }
            Cursor = Cursors.WaitCursor;
            InitParams();
            Cursor = Cursors.Default;
        }

        #region 初始化参数
        /// <summary>
        /// 初始化参数
        /// </summary>
        private void InitParams()
        {
            _carWeightDatamanage = new CarWeightDataManage(this.ob);
            InitWeightPoints();
            InitDevice();
            InitWeightEditorData();
            InitChart();
            InitVoice();
            //同步系统时间
            _baseInfo.ob = this.ob;
            _baseInfo.SynServerTime();
        }

        /// <summary>
        /// 初始化计量点
        /// </summary>
        private void InitWeightPoints()
        {
            //查询计量点信息
            _carWeightDatamanage.GetWeightPoints(this.dataTable1);
            WeighPoint wp = new WeighPoint(this.ob);
            //按计量点数量生成CoreApp对象数组
            if (_measApps == null)
            {
                _measApps = new CoreApp[ultraGrid2.Rows.Count];
            }
            int count = 0;
            for (int i = 0; i < ultraGrid2.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow uRow = ultraGrid2.Rows[i];
                string sign = uRow.Cells["FN_POINTFLAG"].Value.ToString().Trim();
                string signIp = uRow.Cells["FS_IP"].Value.ToString().Trim();
                if (sign.Equals("1"))
                {
                    if (signIp.Equals(_carWeightDatamanage.IP4))
                    {
                        uRow.Appearance.BackColor = Color.Green;
                        //uRow.Cells["XZ"].Value = true;
                        count++;

                        //初始化设备
                        string pointCode = uRow.Cells["FS_POINTCODE"].Value.ToString().Trim();
                        _measApps[i] = new CoreApp();
                        _measApps[i].Params = wp.GetPoint(pointCode);
                        _measApps[i].Init(i);
                        //初始化读卡器
                        if (_measApps[i].Card != null)
                        {
                            _measApps[i].Card.CardChanged += new CardChangedEventHandler(OnCardChanged1);
                        }
                        // 初始化称重仪
                        if (_measApps[i].Weight != null)
                        {
                            _measApps[i].Weight.WeightChanged += new YGJZJL.CarSip.Client.Meas.WeightChangedEventHandler(OnWeightChanged1);
                            _measApps[i].Weight.WeightCompleted += new YGJZJL.CarSip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted1);
                        }
                        //初始化RTU
                        if (_measApps[i].Rtu != null)
                        {
                            _measApps[i].Rtu.DOChanged += new RtuChangedEventHandler(Rtu_DOChanged1);
                        }
                        _measApps[i].Run();
                    }
                    else
                    {
                        uRow.Appearance.BackColor = Color.Yellow;
                        uRow.Cells["XZ"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    }
                }
                else
                {
                    uRow.Appearance.BackColor = Color.White;
                }
            }
            btnJG.Text = count == 0 ? "接 管" : "取消接管";
            if (btnJG.Text.Equals("取消接管"))
            {
                MessageBox.Show("接管成功！");
            }
            //设置一次计量图片显示大小
            panel17.Width = videoChannel5.Width;
            panel17.Height = videoChannel5.Height;
            panel18.Width = videoChannel6.Width;
            panel18.Height = videoChannel6.Height;
            panel19.Width = videoChannel7.Width;
            panel19.Height = videoChannel7.Height;
            panel20.Width = videoChannel8.Width;
            panel20.Height = videoChannel8.Height;
        }

        /// <summary>
        /// 初始化云台及RTU状态事件
        /// </summary>
        private void InitDevice()
        {
            RegistPTZ(videoChannel1, 1);//云台控制注册
            RegistPTZ(videoChannel2, 2);//云台控制注册
            RegistPTZ(videoChannel3, 3);//云台控制注册
            RegistPTZ(videoChannel4, 4);//云台控制注册
            rtuControl1.LightStatusChanged += new LightEventHandler(rtuControl1_LightStatusChanged);
            rtuControl1.RingStatusChanged += new RingEventHandler(rtuControl1_RingStatusChanged);
            rtuControl1.TrafficStatusChanged += new TrafficEventHandler(rtuControl1_TrafficStatusChanged);
            rtuControl1.DeviceRestart += new RestartHandler(rtuControl1_DeviceRestart);
        }

        /// <summary>
        /// 初始化动态曲线
        /// </summary>
        private void InitChart()
        {
            _series = new CarNumericSeries[dataTable1.Rows.Count];//根据计量点数据初始化动态曲线图数据数量
            NumericDataPoint np = null;
            for (int i = 0; i < _series.Length; i++)
            {
                _series[i] = new CarNumericSeries();
                _series[i].Points.Clear();
                np = new NumericDataPoint();
                np.Value = 0;
                _series[i].Points.Add(np);//将每个计量点曲线第一点初始化为0
            }
        }

        /// <summary>
        /// WeightEditorData控件初始化
        /// </summary>
        private void InitWeightEditorData()
        {
            weighEditorControl1.weighEditorEvent += new EventHandler(weighEditorControl1_weighEditorEvent);
            weighEditorControl1.weightEditorTextChahgeEvent += new EventHandler(weighEditorControl1_weightEditorTextChahgeEvent);
            _carWeightDatamanage.GetMaterial(weighEditorControl1.DT_Material);
            _carWeightDatamanage.GetReceiver(weighEditorControl1.DT_Receiver);
            _carWeightDatamanage.GetSender(weighEditorControl1.DT_Sender);
            _carWeightDatamanage.GetTrans(weighEditorControl1.DT_Trans);
            _carWeightDatamanage.GetCarNo(weighEditorControl1.DT_Car);
            _carWeightDatamanage.GetFlow(weighEditorControl1.DT_Flow);
            weighEditorControl1.Clear();
        }

        private void InitVoice()
        {
            _carWeightDatamanage.GetVoiceData(dataTable3);
            Constant.RefreshAndAutoSize(ultraGrid5);

            for (int i = 0; i < dataTable3.Rows.Count; i++)
            {
                if (System.IO.File.Exists(_curPath + "\\qcsound\\" + dataTable3.Rows[i]["FS_VOICENAME"].ToString().Trim()) == false)
                {
                    System.IO.File.WriteAllBytes(_curPath + "\\qcsound\\" + dataTable3.Rows[i]["FS_VOICENAME"].ToString().Trim(), (byte[])dataTable3.Rows[i]["FS_VOICEFILE"]);
                }
            }
        }
        #endregion

        #region 响应rtuControl事件
        /// <summary>
        /// 照明灯开关控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtuControl1_LightStatusChanged(object sender, LightEventArgs e)
        {
            if (_measApp != null && _measApp.Rtu != null)
            {
                switch (e.Value)
                {
                    case LightStatus.Closed:
                        _measApp.Rtu.CloseLight();
                        break;
                    case LightStatus.Open:
                        _measApp.Rtu.OpenLight();
                        break;
                }
            }
        }

        /// <summary>
        /// 红绿灯控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtuControl1_TrafficStatusChanged(object sender, TrafficEventArgs e)
        {
            if (_measApp != null && _measApp.Rtu != null)
            {
                switch (e.Value)
                {
                    case TrafficLightStatus.Red:
                        _measApp.Rtu.OpenRed();
                        break;
                    case TrafficLightStatus.Green:
                        _measApp.Rtu.OpenGreen();
                        break;
                }
            }
        }

        /// <summary>
        /// 电铃开关控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtuControl1_RingStatusChanged(object sender, RingEventArgs e)
        {
            if (_measApp != null && _measApp.Rtu != null)
            {
                switch (e.Value)
                {
                    case RingStatus.Ring:
                        _measApp.Rtu.OpenRing();
                        break;
                    case RingStatus.Stop:
                        _measApp.Rtu.CloseRing();
                        break;
                }
            }
        }
        
        /// <summary>
        /// 设备重启处理 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtuControl1_DeviceRestart(object sender, EventArgs e)
        {
            if (_measApp != null && _measApp.Rtu != null)
            {
                _measApp.Rtu.RestartDevice();
            }
        }
        #endregion

        #region 响应weightEditorcontol事件
        /// <summary>
        /// 处理及绑定预报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void weighEditorControl1_weighEditorEvent(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Control control = (Control)sender;
            switch (control.Name)
            {
                case "txtCZH":
                    if (string.IsNullOrEmpty(weighEditorControl1.CarNo))
                    {
                        Log.Debug("CardNo leave." + weighEditorControl1.CardNo);
                        _carWeightDatamanage.GetTransPlan(weighEditorControl1.DT_TransPlan, weighEditorControl1.CardNo, "", weighEditorControl1.IFYBFirst);
                        weighEditorControl1.BandTransPlan();
                    }
                    break;
                case "cbCH1":
                    //if (string.IsNullOrEmpty(weighEditorControl1.CardNo))
                    //{
                        Log.Debug("weightEditorControl CarNo Leave," + weighEditorControl1.CardNo + "," + weighEditorControl1.CarNo);
                        _carWeightDatamanage.GetTransPlan(weighEditorControl1.DT_TransPlan, "", weighEditorControl1.CarNo, weighEditorControl1.IFYBFirst);
                        weighEditorControl1.BandTransPlan();
                    //}
                        weighEditorControl1.WeighStatus = _carWeightDatamanage.GetWeightStatus(weighEditorControl1.CarNo, (!string.IsNullOrEmpty(weighEditorControl1.DischargeFlag)) && weighEditorControl1.DischargeFlag.Equals("1"));
                    SetFirstPicVisible(true);
                    break;
                case "textBox2":
                    _carWeightDatamanage.GetQuickPlan(weighEditorControl1.DT_TransPlan, weighEditorControl1.QuickPlan);
                    weighEditorControl1.BandTransPlan();
                    this.button1.Focus();
                    break;
                case "btnRefresh":
                    weighEditorControl1.Clear();
                    weighEditorControl1.CarNo = "";
                    weighEditorControl1.CardNo = "";
                    if (_measApp != null)
                    {
                        if (_measApp.Weight != null)
                        {
                            _measApp.Weight._receiveFlag = true;
                        }
                        if (_measApp.Card != null)
                        {
                            if (_measApp.Weight != null)
                            {
                                _measApp.Weight._receiveFlag = true;
                            }
                            if (_measApp.Card != null)
                            {
                                _measApp.Card.ReRead();
                            }
                        }
                    }
                    break;
            }
        }

        private void SetFirstPicVisible(bool visible)
        {
            if (visible)
            {
                Bitmap[] bm = _carWeightDatamanage.GetFirstImage(weighEditorControl1.CarNo);
                if (bm != null)
                {
                    pictureBox1.Image = bm[0];
                    if (pictureBox1.Image != null)
                    {
                        panel17.Visible = visible;
                    }
                    pictureBox2.Image = bm[1];
                    if (pictureBox2.Image != null)
                    {
                        panel18.Visible = visible;
                    }
                    pictureBox3.Image = bm[2];
                    if (pictureBox3.Image != null)
                    {
                        panel19.Visible = visible;
                    }
                    pictureBox4.Image = bm[3];
                    if (pictureBox4.Image != null)
                    {
                        panel20.Visible = visible;
                    }
                }
            }
            else
            {
                panel17.Visible = visible;
                panel18.Visible = visible;
                panel19.Visible = visible;
                panel20.Visible = visible;
            }
        }

        /// <summary>
        /// 筛选、显示一次计量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void weighEditorControl1_weightEditorTextChahgeEvent(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Control control = (Control)sender;
            DataRow[] drs = null;
            bool flag = false;
            switch (control.Name)
            {
                case "txtCZH":
                    flag = true;
                    break;
                case "cbCH1":
                    flag = true;
                    break;
                case "cbCH":
                    flag = true;
                    break;
            }

            if (flag)
            {
                string condition = "";
                if (!string.IsNullOrEmpty(weighEditorControl1.CardNo))
                {
                    condition += "FS_CARDNUMBER like '%" + weighEditorControl1.CardNo + "%'";
                }
                if (!string.IsNullOrEmpty(weighEditorControl1.CarNo))
                {
                    if (!string.IsNullOrEmpty(condition))
                    {
                        condition += " AND";
                    }
                    condition += " FS_CARNO like '%" + weighEditorControl1.CarNo + "%'";
                }
                drs = dataTable11.Select(condition);
                _dtFirstWeighData.Clear();
                foreach (DataRow dr in drs)
                {
                    _dtFirstWeighData.Rows.Add(dr.ItemArray);
                }
                ultraGrid3.DataSource = _dtFirstWeighData;
            }
        }
        #endregion

        #region 切换计量点
        private void ultraGrid2_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            if (e.Row.Index < 0)
            {
                MessageBox.Show("请选择计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (e.Row.Index >= 0 && e.Row.Cells["FN_POINTFLAG"].Value.ToString().Trim().Equals("1") && e.Row.Cells["FS_IP"].Value.ToString().Trim().Equals(_carWeightDatamanage.IP4))
            {
                string pointCode = e.Row.Cells["FS_POINTCODE"].Value.ToString().Trim();
                string pointName = e.Row.Cells["FS_POINTNAME"].Value.ToString().Trim();
                if (!pointCode.Equals(weighEditorControl1.PointCode))
                {
                    ChangePoint(pointCode);
                    //设置计量点信息
                    weighEditorControl1.SetPointInfo(pointCode, pointName, UserInfo.GetUserName(), _carWeightDatamanage.GetOrderGroupName(CarWeightDataManage.OperationInfo.order, UserInfo.GetUserOrder()), _carWeightDatamanage.GetOrderGroupName(CarWeightDataManage.OperationInfo.group, UserInfo.GetUserGroup()));

                    this.ultraChart1.Series.Clear();
                    this.ultraChart1.Series.Add(_series[e.Row.Index]);//绑定曲线图数据
                }
            }
        }


        public void ChangePoint(string pointCode)
        {
            try
            {
                //如果有上一个计量点没有被关闭，那么就关闭上一个计量点所有的设备              
                if (_measApp != null)
                {
                    if (_measApp.Dvr != null)
                    {
                        //切换计量点前如果正在语音对讲先关闭对讲
                        if (_isTalk)
                        {
                            _measApp.Dvr.StopTalk();
                            _isTalk = false;
                            ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                        }
                        _measApp.Dvr.CloseSound();//关闭声音
                        for (int i = 1; i <= 8; i++)
                        {
                            _measApp.Dvr.StopRealPlay(i);
                        }
                        bigVideoChannel.Visible = false;
                        _bigChannelId = -1;
                    }
                    //停止上个计量点数据接收，只接收重量进行提示
                    if (_measApp.Card != null)
                    {
                        _measApp.Card.CardChanged -= new CardChangedEventHandler(OnCardChanged);
                        _measApp.Card.ReadOnly = true;//设置为不写卡
                    }
                    if (_measApp.Weight != null)
                    {
                        _measApp.Weight.WeightChanged -= new YGJZJL.CarSip.Client.Meas.WeightChangedEventHandler(OnWeightChanged);
                        _measApp.Weight.WeightCompleted -= new YGJZJL.CarSip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted);
                    }
                    if (_measApp.Rtu != null)
                    {
                        _measApp.Rtu.DOChanged -= new RtuChangedEventHandler(Rtu_DOChanged);
                    }

                    //_measApp.Finit();
                    _measApp = null;
                }

                if (this.ultraGrid2.ActiveRow == null || this.ultraGrid2.ActiveRow.Index < 0)
                {
                    return;
                }
                //WeighPoint wp = new WeighPoint(this.ob);

                //初始化计量点对象
                int iSelectIndex = ultraGrid2.ActiveRow.Index;
                if (iSelectIndex == m_iSelectedPound)
                {
                    return;
                }

                messForm.SetMessage("正在打开设备，请稍候！");

                m_iSelectedPound = iSelectIndex;
                _measApp = _measApps[m_iSelectedPound];//new CoreApp();
                //_measApp.Params = wp.GetPoint(pointCode);

                //初始化打印机
                _measApp.Params.FS_PRINTERNAME = pointCode;// new System.Drawing.Printing.PrintDocument().PrinterSettings.PrinterName;
                txtZZ.Text = _measApp.Params.FN_USEDPRINTPAPER;
                //_measApp.Init();
                //初始化读卡器
                if (_measApp.Card != null)
                {
                    try
                    {
                        _measApp.Card.CardChanged += new CardChangedEventHandler(OnCardChanged);
                        _measApp.Card.ReadOnly = false;//卡可写
                        _measApp.Card.ClearCard();
                        if (_measApp.Card.CardThread.ThreadState == ThreadState.Suspended)//注释测试读卡
                        {
                            _measApp.Card.CardThread.Resume();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("读卡器初始化失败，请重新启动！"+"/n"+ex.Message );
                    }
                }
                else
                {
                    MessageBox.Show("读卡器初始化失败，请重新启动！");
                }

                // 初始化称重仪
                if (_measApp.Weight != null)
                {
                    try
                    {
                        _measApp.Weight.WeightChanged += new YGJZJL.CarSip.Client.Meas.WeightChangedEventHandler(OnWeightChanged);
                        _measApp.Weight.WeightCompleted += new YGJZJL.CarSip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("仪表初始化失败，请重新启动！"+"/n" + ex.Message);
                    }
                 }
                else
                {
                    MessageBox.Show("仪表初始化失败，请重新启动！");
                    meterControl1.Status = MeterStatus.UnConnect;//仪表未连接上
                }

                if (_measApp.Rtu != null)
                {
                    try
                    {
                        //初始化红绿灯
                        _measApp.Rtu.DOChanged += new RtuChangedEventHandler(Rtu_DOChanged);
                        _measApp.Rtu.ReRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("RTU初始化失败，请重新启动！"+"\n"+ex.Message );
                    }
                }
                else
                {
                    MessageBox.Show("RTU初始化失败，请重新启动！");
                }

                //_measApp.Run();
                // 初始化视频
                for (int i = 1; i <= 8; i++)
                {
                    _measApp.Dvr.RealPlay(i, ((PictureBox)Controls.Find("videoChannel" + i.ToString(), true)[0]).Handle);
                }
                Thread.Sleep(100);
                //打开第7通道声音
                _measApp.Dvr.OpenSound(6);
                //停止正在计量计量点闪烁
                if (_measApp.IsFlash)
                {
                    WeighPrompt(_measApp, false);
                }
                //播放语音
                bool bl = _measApp.Dvr.SendVoiceData(Constant.RunPath + "\\sound\\正在计量.wav");
                //StrBandPoint = "1";//标识已经接管计量点
                //初始化动态曲线图
                InitChart();
                //加载一次计量数据
                _carWeightDatamanage.GetFirstWeighData(this.dataTable11, "", "");
                _dtFirstWeighData = dataTable11.Copy();
                this.ultraGrid3.DataSource = _dtFirstWeighData;
                #region 调整ultraGrid格式
                if (ultraGrid3.DisplayLayout.Bands.Count > 0)
                {
                    Infragistics.Win.UltraWinGrid.ColumnsCollection cols = ultraGrid3.DisplayLayout.Bands[0].Columns;
                    if (cols.Exists("FS_MATERIAL"))
                    {
                        cols["FS_MATERIAL"].Hidden = true;
                    }
                    if (cols.Exists("FS_SENDER"))
                    {
                        cols["FS_SENDER"].Hidden = true;
                    }
                    if (cols.Exists("FS_RECEIVER"))
                    {
                        cols["FS_RECEIVER"].Hidden = true;
                    }
                    if (cols.Exists("FS_WEIGHTTYPE"))
                    {
                        cols["FS_WEIGHTTYPE"].Hidden = true;
                    }
                    if (cols.Exists("FS_TRANSNO"))
                    {
                        cols["FS_TRANSNO"].Hidden = true;
                    }
                    if (cols.Exists("FS_POUNDTYPE"))
                    {
                        cols["FS_POUNDTYPE"].Hidden = true;
                    }
                    if (cols.Exists("FD_LOADINSTORETIME"))
                    {
                        cols["FD_LOADINSTORETIME"].Hidden = true;
                    }
                    if (cols.Exists("FD_LOADOUTSTORETIME"))
                    {
                        cols["FD_LOADOUTSTORETIME"].Hidden = true;
                    }
                    if (cols.Exists("FS_UNLOADFLAG"))
                    {
                        cols["FS_UNLOADFLAG"].Hidden = true;
                    }
                    if (cols.Exists("FS_UNLOADSTOREPERSON"))
                    {
                        cols["FS_UNLOADSTOREPERSON"].Hidden = true;
                    }
                    if (cols.Exists("FS_LOADFLAG"))
                    {
                        cols["FS_LOADFLAG"].Hidden = true;
                    }
                    if (cols.Exists("FS_LOADSTOREPERSON"))
                    {
                        cols["FS_LOADSTOREPERSON"].Hidden = true;
                    }
                    if (cols.Exists("FS_SAMPLEPERSON"))
                    {
                        cols["FS_SAMPLEPERSON"].Hidden = true;
                    }
                    if (cols.Exists("FS_FIRSTLABELID"))
                    {
                        cols["FS_FIRSTLABELID"].Hidden = true;
                    }
                    if (cols.Exists("FD_UNLOADINSTORETIME"))
                    {
                        cols["FD_UNLOADINSTORETIME"].Hidden = true;
                    }
                    if (cols.Exists("FD_UNLOADOUTSTORETIME"))
                    {
                        cols["FD_UNLOADOUTSTORETIME"].Hidden = true;
                    }
                    if (cols.Exists("FS_BZ"))
                    {
                        cols["FS_BZ"].Hidden = true;
                    }
                    if (cols.Exists("FS_PROVIDER"))
                    {
                        cols["FS_PROVIDER"].Hidden = true;
                    }
                    if (cols.Exists("FS_MEMO"))
                    {
                        cols["FS_MEMO"].Hidden = true;
                    }
                    if (cols.Exists("FS_MEMO"))
                    {
                        cols["FS_MEMO"].Hidden = true;
                    }
                    if (cols.Exists("FS_RECEIVERFACTORY"))
                    {
                        cols["FS_RECEIVERFACTORY"].Hidden = true;
                    }

                }
                Constant.RefreshAndAutoSize(ultraGrid3);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("设备连接异常,可能您已连接,请联系管理员!" + ex.Message);
                Log.Error(ex.Message);
            }
        }


        /// <summary>
        /// 非当前操作计量点来车提示线程
        /// </summary>
        /// <param name="sequence"></param>
        //private void FlashGridRow(object sequence)
        //{
        //    int i = (int)sequence;

        //    while (ultraGrid2.ActiveRow.Index != i)
        //    {
        //        ultraGrid2.Rows[i].Appearance.BackColor = Color.Red;
        //        Thread.Sleep(300);
        //        ultraGrid2.Rows[i].Appearance.BackColor = Color.Yellow;
        //        Thread.Sleep(300);
        //    }
        //    ultraGrid2.Rows[i].Appearance.BackColor = Color.Yellow;
        //}
        #endregion

        #region 语音选择播报
        private void ultraGrid5_ClickCell(object sender, Infragistics.Win.UltraWinGrid.ClickCellEventArgs e)
        {
            if (e.Cell.Column.Key.ToUpper() != "FS_VOICENAME" || e.Cell.Value.ToString().Length == 0)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            if (weighEditorControl1.PointCode == null || weighEditorControl1.PointCode.Trim().Equals(""))
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

            if (_measApp == null || _measApp.Dvr == null)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            bool talked = false;
            if (_isTalk)
            {
                talked = true;
                _measApp.Dvr.StopTalk();
                _isTalk = false;
                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }
            _measApp.Dvr.SendVoiceData(_curPath + "\\qcsound\\" + ultraGrid5.ActiveRow.Cells["FS_VOICENAME"].Value.ToString().Trim());//此语音待更改

            this.Cursor = Cursors.Default;
            weighEditorControl1.Focus();
        }
        #endregion

        #region 响应设备接口事件
        #region <读卡处理方法>
        //处理读卡事件
        public void OnCardChanged(object sender, CardEventArgs e)
        {
            Invoke(new GetCardText(HandleCardChange), new object[] { e.Value });
        }
        delegate void GetCardText(CardData data);
        private void HandleCardChange(CardData data)
        {
            weighEditorControl1.CardNo = data.CardNo.Trim();
            //weighEditorControl1.CarNo = data.CarNo.Trim();
            weighEditorControl1.DischargeFlag = data.UnloadFlag.Trim();
            if (!string.IsNullOrEmpty(weighEditorControl1.DischargeFlag))
            {
                weighEditorControl1.CardWeightNo = data.WgtOpCd.Trim();
                weighEditorControl1.Discharger = data.UnloadName.Trim();
                weighEditorControl1.DischargeTime = data.UnloadTime.Trim();
                weighEditorControl1.DischargeDepart = data.UnloadDepart.Trim();
                if (weighEditorControl1.DischargeFlag.Equals("1"))
                {
                    //历史记录不显示扣渣量
                    if (!weighEditorControl1.CardWeightNo.EndsWith("H"))
                    {
                        double kz = 0;
                        double.TryParse(data.UnloadKZ.Trim(), out kz);
                        weighEditorControl1.Deduction = kz;
                    }
                    else
                    {
                        weighEditorControl1.CardDedution = data.UnloadKZ.Trim();
                    }
                }
            }

            weighEditorControl1.CardWeightNo = data.WgtOpCd.Trim();
            _carWeightDatamanage.GetTransPlan(weighEditorControl1.DT_TransPlan, weighEditorControl1.CardNo, "", weighEditorControl1.IFYBFirst);
            weighEditorControl1.BandTransPlan();
            //DataRow dr = weighEditorControl1.DT_TransPlan.Rows.Count > 0 ? weighEditorControl1.DT_TransPlan.Rows[0] : null;
            //if (dr != null && !string.IsNullOrEmpty(dr["FS_CARNO"].ToString()) && string.IsNullOrEmpty(weighEditorControl1.CarNo))
            //{
            //    weighEditorControl1.CarNo = dr["FS_CARNO"].ToString();
            //}

            //显示车辆过磅信息
            weighEditorControl1.WeighStatus = _carWeightDatamanage.GetWeightStatus(weighEditorControl1.CarNo, (!string.IsNullOrEmpty(weighEditorControl1.DischargeFlag)) && weighEditorControl1.DischargeFlag.Equals("1"));
            SetFirstPicVisible(true);

            Log.Debug("CardInfo = cardNo:" + weighEditorControl1.CardNo + ",carNo:" + weighEditorControl1.CarNo + ",cardWeightNo:" + weighEditorControl1.CardWeightNo + ",dischargeFlag:" + weighEditorControl1.DischargeFlag + ",discharger:" + weighEditorControl1.Discharger + ",dischargeTime:" + weighEditorControl1.DischargeTime);
        }

        //未使用计量点处理
        public void OnCardChanged1(object sender, CardEventArgs e)
        {

        }
        #endregion

        #region 红外对射处理方法
        delegate void GetHLDdText(int i, bool status);
        public void Rtu_DOChanged(object sender, RtuEventArgs e)
        {
            try
            {
                Invoke(new GetHLDdText(HandleHLDChange), new object[] { e.Value.index, e.Value.status });
            }
            catch (Exception ex)
            {

            }
        }
        private void HandleHLDChange(int index, bool status)
        {
            
            switch (index)
            {
                case 0:
                    rtuControl1.Light = status ? LightStatus.Open : LightStatus.Closed;
                    break;
                case 1:
                    rtuControl1.TrafficLight = status ? TrafficLightStatus.Red : TrafficLightStatus.Green;
                    break;
                case 6:
                case 7:
                    rtuControl1.FrontInfraredRadio = status ? InfraredRadioStatus.Connected : InfraredRadioStatus.Disconnected;
                    break;
                default:
                    break;
            }
        }

        //未使用计量点处理
        public void Rtu_DOChanged1(object sender, RtuEventArgs e)
        {
        }
        #endregion

        #region <称重处理方法>
        //处理重量事件
        delegate void HandleWeightEvent(double weight);
        /// <summary>
        /// 处理不稳定重量值
        /// </summary>
        /// <param name="weight"></param>
        public void OnWeightChanged(object sender, WeightEventArgs e)
        {
            try
            {
                Invoke(new HandleWeightEvent(HandleWeightChange), new object[] { e.Value });
            }
            catch (Exception ex)
            {
            }
        }
        private double _preWeight = -1; // 前一次称重值 
        private void HandleWeightChange(double weight)
        {
            bool flag = false;
            if (weight <= _measApp.Weight.MinWeight)
            {
                meterControl1.Status = MeterStatus.Null;
                _measApp.IsSaved = false;//复位后保存按钮能再次保存，彭海波增加
                _measApp.Rtu.OpenGreen();  //打开绿灯
                rtuControl1.TrafficLight = TrafficLightStatus.Green;

                _series[m_iSelectedPound].Points.Clear();
                NumericDataPoint np = new NumericDataPoint();
                np.Value = 0;
                _series[m_iSelectedPound].Points.Add(np);

                //LCD切回欢迎界面
                if (_measApp.Lcd != null)
                {
                    if (_measApp.Lcd.LcdStatus != LCD_PICTURE.WELCOME)
                    {
                        _measApp.Lcd.ClearScreen();
                        _measApp.Lcd.DrawPicture((int)LCD_PICTURE.WELCOME);
                        _measApp.Lcd.LcdStatus = LCD_PICTURE.WELCOME;
                    }
                }
            }
            else
            {
                meterControl1.Status = MeterStatus.UnStable;

                _measApp.Rtu.OpenRed();  //打开红灯
                rtuControl1.TrafficLight = TrafficLightStatus.Red;
                flag = true;
            }

            //switch (weighEditorControl1.PointCode)
            //{
            //    //case "K004":
            //    //    weight /= 100;
            //    //    break;
            //    //case "K003":
            //    //case "K006":
            //    //    weight /= 1000;
            //    //    break;
            //    //case "K005":
            //    //    weight /= 1000;
            //    //    break;
            //    default:
            //        weight /= 1000;
            //        break;
            //}

            if (flag)//weight > 0 && weight >= _preWeight
            {
                NumericDataPoint np = new NumericDataPoint();
                np.Value = weight;
                _series[m_iSelectedPound].Points.Add(np);
            }

            meterControl1.Weight = weight;

            button1.Enabled = false;
            //button1.Enabled = true;

            _preWeight = weight;
        }
        //-------------------
        /// <summary>
        /// 处理稳定重量值
        /// </summary>
        /// <param name="weight"></param>
        public void OnWeightCompleted(object sender, WeightEventArgs e)
        {
            try
            {
                this.Invoke(new HandleWeightEvent(HandleWeightCompelte), new object[] { e.Value });
            }
            catch (Exception ex)
            {
            }
        }
        private void HandleWeightCompelte(double weight)
        {
            //switch (weighEditorControl1.PointCode)
            //{
            //    //case "K004":
            //    //    weight /= 100;
            //    //    break;
            //    //case "K003":
            //    //case "K006":
            //    //    weight /= 1000;
            //    //    break;
            //    //case "K005":
            //    //    weight /= 1000;
            //    //    break;
            //    default:
            //        weight /= 1000;
            //        break;
            //}

            NumericDataPoint np = new NumericDataPoint();
            np.Value = weight;
            _series[m_iSelectedPound].Points.Add(np);

            meterControl1.Status = MeterStatus.Stable;
            meterControl1.Weight = weight;
            if (!_measApp.IsSaved)
            {//复位后保存按钮能再次保存
                _isReWriteCard = false;
                button1.Enabled = true;
            }
            button1.Refresh();
        }

        //处理重量事件
        delegate void HandleWeightEvent2(object sender,double weight);
        //未使用计量点处理
        public void OnWeightChanged1(object sender, WeightEventArgs e)
        {
            try
            {
                if (this.IsHandleCreated)
                Invoke(new HandleWeightEvent2(HandleWeightChange2), new object[] { sender, e.Value });
            }
            catch (Exception ex)
            {
                Log.Error("CarWeigh.OnWeightChanged1() : " + ex.Message);
            }
        }
        private void HandleWeightChange2(object sender, double weight)
        {
            CoreWeight coreWeight = (CoreWeight)sender;
            if (_measApp != null && _measApp.Params.FS_POINTCODE.Equals(coreWeight.DeviceName) && !_measApp.IsFlash)
            {
                return;//正在计量的称不提示
            }
            //Log.Debug("CarWegih.HandleWeightChange2() : pointCode:" + coreWeight.DeviceName + ",weight:" + weight);
            foreach (CoreApp app in _measApps)
            {
                if (app != null && app.Params.FS_POINTCODE.Equals(coreWeight.DeviceName))
                {
                    if (weight <= app.Weight.MinWeight)
                    {
                        app.Rtu.OpenGreen();
                        app.Lcd.ClearScreen();
                        app.Lcd.DrawPicture((int)LCD_PICTURE.WELCOME);
                        app.Lcd.LcdStatus = LCD_PICTURE.WELCOME;
                        app.IsSaved = false;//复位后不再提示
                        if (app.IsFlash)
                        {
                            WeighPrompt(app, false);
                            Log.Debug("CarWegih.WeighPrompt("+app.Params.FS_POINTCODE+",false)");
                        }
                    }
                    else
                    {
                        app.Rtu.OpenRed();
                        if (!app.IsFlash&&!app.IsSaved)
                        {
                            WeighPrompt(app, true);
                            Log.Debug("CarWegih.WeighPrompt(" + app.Params.FS_POINTCODE + ",true)");
                        }
                    }
                    break;
                }
            }
        }
        public void OnWeightCompleted1(object sender, WeightEventArgs e)
        {
            try
            {
                if (this.IsHandleCreated)
                    Invoke(new HandleWeightEvent2(HandleWeightChange2), new object[] { sender, e.Value });
            }
            catch (Exception ex)
            {
            }
        }
        private void HandleWeightCompelte2(object sender, double weight)
        {
            
        }
        #endregion

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        #region 计量点上称提示
        private void WeighPrompt(CoreApp app, bool isFlash)
        {
            if (isFlash)
            {
                app.FlashThread = new Thread(new ParameterizedThreadStart(flashPointDelegate));
                app.FlashThread.Start(app.Params.FS_POINTCODE);
            }
            else
            {
                if (app.FlashThread != null)
                {
                    app.FlashThread.Abort();
                    app.FlashThread = null;
                    Infragistics.Win.UltraWinGrid.UltraGridRow uRow = null;
                    for (int i = 0; i < ultraGrid2.Rows.Count; i++)
                    {
                        uRow = ultraGrid2.Rows[i];
                        if (uRow.Cells["FS_POINTCODE"].Value.ToString().Equals(app.Params.FS_POINTCODE))
                        {
                            uRow.Appearance.BackColor = Color.Green;
                            break;
                        }
                    }
                }
            }
            app.IsFlash = isFlash;
        }

        //闪烁计量点委托
        delegate void FlashPointEvent(string pointCode);
        private void flashPointDelegate(object pointCode)
        {
            while (true)
            {
                try
                {
                    Invoke(new FlashPointEvent(flashPoint), new object[] { pointCode });
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void flashPoint(string pointCode)
        {
            if (weighEditorControl1.PointCode.Equals(pointCode))
            {
                return;
            }
            Infragistics.Win.UltraWinGrid.UltraGridRow uRow = null;
            for (int i = 0; i < ultraGrid2.Rows.Count; i++)
            {
                uRow = ultraGrid2.Rows[i];
                if (uRow.Cells["FS_POINTCODE"].Value.ToString().Equals(pointCode))
                {
                    uRow.Appearance.BackColor = uRow.Appearance.BackColor == Color.Red ? Color.White : Color.Red;
                    break;
                }
            }
        }
        #endregion 
        #endregion

        #region 云台控制
        private bool _mousedown = false;//判断鼠标按下状态
        private int _moveLeft = 0;//X轴移动长度
        private int _moveTop = 0;//Y轴移动长度
        private PtzCommand _direction = PtzCommand.PAN_LEFT;//移动方向
        private bool _upflag = false;//云台可控后鼠标松开标识
        private bool _wheelFlag = false;//是否捕捉滚轮事件
        private int _wheelChannel = 0;//鼠标滚动通道
        private System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        private Control _ptzControl = null;//当前控制的通道
        private int _curChannelId = -1;//当前视频通道，按钮控制云台用

        /// <summary>
        /// 事件类型
        /// </summary>
        private enum EventType
        {
            Down, Up, Leave, Move, Click, Enter, Wheel
        }

        /// <summary>
        /// 云台控制启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void StartPTZ(object sender, int x, int y)
        {
            if (!_mousedown)
            {
                _moveLeft = x;
                _moveTop = y;
            }
            _upflag = false;
            if (!_upflag)
            {
                _ptzControl = (Control)sender;
                _timer.Interval = 2000;
                _timer.Tick += new EventHandler(_timer_Tick);
                _timer.Start();
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (!_mousedown && !_upflag)
            {
                _mousedown = true;
                _timer.Stop();
                _ptzControl.Cursor = Cursors.NoMove2D;
            }
        }

        /// <summary>
        /// 处理云台移动方向
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="channelId"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void MovePTZ(object sender, int channelId, int x, int y)
        {
            if (_mousedown && _measApp != null && _measApp.Dvr != null)
            {
                //tbCharge.Text = Convert.ToString(Math.Atan2(y - _moveTop, x - _moveLeft) * 180 / Math.PI);
                double angle = Math.Atan2(y - _moveTop, x - _moveLeft) * 180 / Math.PI;
                if (angle < 22.5 && angle >= -22.5)
                {
                    _direction = PtzCommand.PAN_RIGHT;
                    ((Control)sender).Cursor = Cursors.PanEast;
                }
                else if (angle >= 22.5 && angle < 67.5)
                {
                    _direction = PtzCommand.DOWN_RIGHT;
                    ((Control)sender).Cursor = Cursors.PanSE;
                }
                else if (angle >= 67.5 && angle < 112.5)
                {
                    _direction = PtzCommand.TILT_DOWN;
                    ((Control)sender).Cursor = Cursors.PanSouth;
                }
                else if (angle >= 112.5 && angle < 157.5)
                {
                    _direction = PtzCommand.DOWN_LEFT;
                    ((Control)sender).Cursor = Cursors.PanSW;
                }
                else if (angle >= 157.5 || angle < -157.5)
                {
                    _direction = PtzCommand.PAN_LEFT;
                    ((Control)sender).Cursor = Cursors.PanWest;
                }
                else if (angle >= -157.5 && angle < -112.5)
                {
                    _direction = PtzCommand.UP_LEFT;
                    ((Control)sender).Cursor = Cursors.PanNW;
                }
                else if (angle >= -112.5 && angle < -67.5)
                {
                    _direction = PtzCommand.TILT_UP;
                    ((Control)sender).Cursor = Cursors.PanNorth;
                }
                else if (angle >= -67.5 && angle < -22.5)
                {
                    _direction = PtzCommand.UP_RIGHT;
                    ((Control)sender).Cursor = Cursors.PanNE;
                }
            }
        }

        /// <summary>
        /// 转动一次云台
        /// </summary>
        /// <param name="channelId">通道号</param>
        private void TurnDirection(int channelId)
        {
            if (_mousedown && _measApp != null && _measApp.Dvr != null)
            {
                _measApp.Dvr.PTZControl(channelId, _direction, PtzStop.START, PtzSpeed.LEVEL4);
                _measApp.Dvr.PTZControl(channelId, _direction, PtzStop.STOP, PtzSpeed.LEVEL4);
            }
        }

        /// <summary>
        /// 转动一次云台，按钮控制，彭海波增加
        /// </summary>
        /// <param name="channelId">通道号</param>
        /// <param name="command">转动方向</param>
        private void TurnDirectionByButton(int channelId,PtzCommand command)
        {
            if (_measApp != null && _measApp.Dvr != null)
            {
                _measApp.Dvr.PTZControl(channelId, command, PtzStop.START, PtzSpeed.LEVEL4);
                _measApp.Dvr.PTZControl(channelId, command, PtzStop.STOP, PtzSpeed.LEVEL4);
            }
        }

        /// <summary>
        /// 停止云台转动
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="channelId">通道号</param>
        private void StopPTZ(object sender, int channelId)
        {
            if (_measApp != null && _measApp.Dvr != null)
            {
                _measApp.Dvr.PTZControl(channelId, _direction, PtzStop.STOP, PtzSpeed.LEVEL1);
            }
            _mousedown = false;
            _timer.Stop();
            ((Control)sender).Cursor = Cursors.Default;
        }

        /// <summary>
        /// 调整焦距
        /// </summary>
        /// <param name="zoomtype">调整焦距类型 1焦距变小 2焦距变大</param>
        /// <param name="wheelLength">调焦大小</param>
        /// <param name="channelId">调焦通道</param>
        private void WheelPTZ(int zoomtype, int wheelLength, int channelId)
        {
            if (_mousedown && _measApp != null && _measApp.Dvr != null)
            {
                if (zoomtype == 1)
                {
                    _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_OUT, PtzStop.START, PtzSpeed.LEVEL1);
                    Thread.Sleep(wheelLength);
                    _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_OUT, PtzStop.STOP, PtzSpeed.LEVEL1);
                }
                else if (zoomtype == 2)
                {
                    _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_IN, PtzStop.START, PtzSpeed.LEVEL1);
                    Thread.Sleep(wheelLength);
                    _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_IN, PtzStop.STOP, PtzSpeed.LEVEL1);
                }
            }
        }


        /// <summary>
        /// 调整焦距，按钮控制用，彭海波增加
        /// </summary>
        /// <param name="zoomtype">调整焦距类型 1焦距变小 2焦距变大</param>
        /// <param name="wheelLength">调焦大小</param>
        /// <param name="channelId">调焦通道</param>
        private void WheelPTZByButton(int zoomtype, int wheelLength, int channelId)
        {
            if (zoomtype == 1)
            {
                _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_OUT, PtzStop.START, PtzSpeed.LEVEL1);
                Thread.Sleep(wheelLength);
                _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_OUT, PtzStop.STOP, PtzSpeed.LEVEL1);
            }
            else if (zoomtype == 2)
            {
                _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_IN, PtzStop.START, PtzSpeed.LEVEL1);
                Thread.Sleep(wheelLength);
                _measApp.Dvr.PTZControl(channelId, PtzCommand.ZOOM_IN, PtzStop.STOP, PtzSpeed.LEVEL1);
            } 
        }

        /// <summary>
        /// 云台控制
        /// </summary>
        /// <param name="sender">事件源对象</param>
        /// <param name="e">事件数据</param>
        /// <param name="et">事件类型</param>
        /// <param name="channelId">通道号</param>
        private void PTZControl(object sender, EventArgs e, EventType et, int channelId)
        {
            switch (et)
            {
                case EventType.Down:
                    ((Control)sender).Focus();
                    MouseEventArgs de = (MouseEventArgs)e;
                    StartPTZ(sender, de.X, de.Y);
                    if (!_upflag && _mousedown && _measApp != null && _measApp.Dvr != null)
                    {
                        _measApp.Dvr.PTZControl(channelId, _direction, PtzStop.START, PtzSpeed.LEVEL1);
                    }
                    break;
                case EventType.Leave:
                    _wheelFlag = false;
                    _wheelChannel = 0;
                    StopPTZ(sender, channelId);
                    break;
                case EventType.Move:
                    MouseEventArgs me = (MouseEventArgs)e;
                    MovePTZ(sender, channelId, me.X, me.Y);
                    break;
                case EventType.Up:
                    if (_upflag && _measApp != null && _measApp.Dvr != null)
                    {
                        _measApp.Dvr.PTZControl(channelId, _direction, PtzStop.STOP, PtzSpeed.LEVEL1);
                    }
                    _upflag = true;
                    break;
                case EventType.Click:
                    TurnDirection(channelId);
                    break;
                case EventType.Enter:
                    _wheelFlag = true;
                    _wheelChannel = channelId;
                    break;
                case EventType.Wheel:
                    MouseEventArgs we = (MouseEventArgs)e;
                    int zoomType = 0;//1焦距变小 2焦距变大
                    if (we.Delta > 0)
                    {
                        zoomType = 1;
                    }
                    else if (we.Delta < 0)
                    {
                        zoomType = 2;
                    }

                    int wheelLength = (int)(Math.Round((double)(Math.Abs(we.Delta / 3))));
                    WheelPTZ(zoomType, wheelLength, channelId);
                    HandledMouseEventArgs h = e as HandledMouseEventArgs;
                    if (h != null)
                    {
                        h.Handled = true;
                    }
                    break;
            }
        }

        /// <summary>
        /// 云台控制注册
        /// </summary>
        /// <param name="control"></param>
        /// <param name="channelId"></param>
        private void RegistPTZ(Control control, int channelId)
        {
            control.Text = channelId.ToString();
            control.MouseDown += new MouseEventHandler(control_MouseDown);
            control.MouseLeave += new EventHandler(control_MouseLeave);
            control.MouseMove += new MouseEventHandler(control_MouseMove);
            control.MouseUp += new MouseEventHandler(control_MouseUp);
            control.MouseClick += new MouseEventHandler(control_Click);
            control.MouseWheel += new MouseEventHandler(control_MouseWheel);
        }

        private void control_MouseDown(object sender, MouseEventArgs e)
        {
            PTZControl(sender, e, EventType.Down, int.Parse(((Control)sender).Text));
            _curChannelId = int.Parse(((Control)sender).Text);
        }

        private void control_MouseLeave(object sender, EventArgs e)
        {
            PTZControl(sender, e, EventType.Leave, int.Parse(((Control)sender).Text));
        }

        private void control_MouseMove(object sender, MouseEventArgs e)
        {
            PTZControl(sender, e, EventType.Move, int.Parse(((Control)sender).Text));
        }

        private void control_MouseUp(object sender, MouseEventArgs e)
        {
            PTZControl(sender, e, EventType.Up, int.Parse(((Control)sender).Text));
        }

        private void control_Click(object sender, EventArgs e)
        {
            PTZControl(sender, e, EventType.Click, int.Parse(((Control)sender).Text));
            _curChannelId=int.Parse(((Control)sender).Text);
        }

        private void control_MouseWheel(object sender, MouseEventArgs e)
        {
            PTZControl(sender, e, EventType.Wheel, int.Parse(((Control)sender).Text));
        }


        private void buttonUP_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.TILT_UP);
        }

        private void buttonLEFT_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.PAN_LEFT);
        }

        private void buttonRIGHT_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.PAN_RIGHT);
        }

        private void buttonDOWN_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.TILT_DOWN);
        }

        private void buttonLEFTUP_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.UP_LEFT);
        }

        private void buttonRIGHTUP_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.UP_RIGHT);
        }

        private void buttonLEFTDOWN_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.DOWN_LEFT);
        }

        private void buttonRIGHTDOWN_Click(object sender, EventArgs e)
        {
            TurnDirectionByButton(_curChannelId, PtzCommand.DOWN_RIGHT);
        }

        private void buttonZOOMOUT_Click(object sender, EventArgs e)
        {
            WheelPTZByButton(2, 40, _curChannelId);
        }

        private void buttonZOOMIN_Click(object sender, EventArgs e)
        {
            WheelPTZByButton(1, 40, _curChannelId);
        }

        private void buttonLENGTH_Click(object sender, EventArgs e)
        {

        }

        private void buttonSHORT_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #region 窗口关闭
        private void CarWeigh_FormClosing(object sender, FormClosingEventArgs e)
        { 
            //如果有上一个计量点没有被关闭，那么就关闭上一个计量点所有的设备              
            if (_measApp != null)
            {
                _measApp.Rtu.OpenRed();
                if (_measApp.Dvr != null)
                {
                    if (_isTalk)
                    {
                        _measApp.Dvr.StopTalk();
                        _isTalk = false;
                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                    }
                    _measApp.Dvr.CloseSound();//关闭声音
                    for (int i = 1; i <= 8; i++)
                    {
                        _measApp.Dvr.StopRealPlay(i);
                    }
                }
                _measApp = null;
            }

            for (int i = 0; i < _measApps.Length; i++)
            {
                if (_measApps[i] != null)
                {
                    _measApps[i].Finit();
                    _measApps[i] = null;
                }
            }
        }
        #endregion

        #region 接管/取消计量点
        private void btnJG_Click(object sender, EventArgs e)
        {
            //先判断是否选择已经被别人已接管的计量点
            if (btnJG.Text == "接 管")
            {
                //接管计量点,把计量点标记改为已接管
                BandPoint();
                //判断称上是否来车，如果有车计量点闪烁               
                //FlashGridRow(sult);
                InitBandPoints();
            }
            else//关闭计量点
            {
                ClosePoint();
            }
            weighEditorControl1.Clear();
        }

        /// <summary>
        /// 接管计量点
        /// </summary>
        private void BandPoint()
        {
            ArrayList param = new ArrayList();
            ultraGrid2.UpdateData();
            string sign = "";
            bool isChecked = false;
            string signIp = "";
            string pointCode = "";

            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid2.Rows)
            {
                sign = uRow.Cells["FN_POINTFLAG"].Text.Trim();
                isChecked = Convert.ToBoolean(uRow.Cells["XZ"].Value);
                signIp = uRow.Cells["FS_IP"].Text.Trim();
                pointCode = uRow.Cells["FS_POINTCODE"].Text.Trim();
                if (sign.Equals("1") && isChecked && signIp.Equals(_carWeightDatamanage.IP4))
                {
                    MessageBox.Show("已存在已被接管的计量点,请刷新计量点并重新选择!");
                    return;
                }

                if (isChecked && sign.Equals("0"))
                {
                    param.Add(pointCode);
                }
            }

            if (param.Count > 0)
            {
                _carWeightDatamanage.BandPoints(param);
                btnJG.Text = "取消接管";
                
            }
            else
            {
                MessageBox.Show("请选择要接管的计量点!");
            }

            RefreashPoint();
        }

        /// <summary>
        /// 初始化接管计量点
        /// </summary>
        private void InitBandPoints()
        {
            WeighPoint wp = new WeighPoint(this.ob);
            int count = 0;
            for (int i = 0; i < ultraGrid2.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow uRow = ultraGrid2.Rows[i];
                string sign = uRow.Cells["FN_POINTFLAG"].Value.ToString().Trim();
                string signIp = uRow.Cells["FS_IP"].Value.ToString().Trim();
                if (sign.Equals("1"))
                {
                    if (signIp.Equals(_carWeightDatamanage.IP4))
                    {
                        uRow.Appearance.BackColor = Color.Green;
                        //uRow.Cells["XZ"].Value = true;
                        count++;

                        //初始化设备
                        bool flag = true;
                        string pointCode = uRow.Cells["FS_POINTCODE"].Value.ToString().Trim();
                        foreach (CoreApp measApp in _measApps)
                        {
                            if (measApp != null && measApp.Params.FS_POINTCODE.Equals(pointCode))
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            _measApps[i] = new CoreApp();
                            _measApps[i].Params = wp.GetPoint(pointCode);
                            _measApps[i].Init(i);
                            //初始化读卡器
                            if (_measApps[i].Card != null)
                            {
                                _measApps[i].Card.CardChanged += new CardChangedEventHandler(OnCardChanged1);
                            }
                            // 初始化称重仪
                            if (_measApps[i].Weight != null)
                            {
                                _measApps[i].Weight.WeightChanged += new YGJZJL.CarSip.Client.Meas.WeightChangedEventHandler(OnWeightChanged1);
                                _measApps[i].Weight.WeightCompleted += new YGJZJL.CarSip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted1);
                            }
                            //初始化RTU
                            if (_measApps[i].Rtu != null)
                            {
                                _measApps[i].Rtu.DOChanged += new RtuChangedEventHandler(Rtu_DOChanged1);
                            }

                            _measApps[i].Run();

                        }
                    }
                    else
                    {
                        uRow.Appearance.BackColor = Color.Yellow;
                        uRow.Cells["XZ"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    }
                }
                else
                {
                    uRow.Appearance.BackColor = Color.White;
                }
            }
            btnJG.Text = count == 0 ? "接 管" : "取消接管";
            if (btnJG.Text.Equals("取消接管"))
            {
                MessageBox.Show("接管成功！");
            }
        }

        /// <summary>
        /// 取消接管
        /// </summary>
        private void ClosePoint()
        {
            if (DialogResult.Yes != MessageBox.Show("是否确定取消接管的计量点？", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            ArrayList param = new ArrayList();
            ultraGrid2.UpdateData();
            string sign = "";
            bool isChecked = false;
            string signIp = "";
            string pointCode = "";
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid2.Rows)
            {
                sign = uRow.Cells["FN_POINTFLAG"].Text.Trim();
                isChecked = Convert.ToBoolean(uRow.Cells["XZ"].Value);
                signIp = uRow.Cells["FS_IP"].Text.Trim();
                pointCode = uRow.Cells["FS_POINTCODE"].Text.Trim();
                if (sign.Equals("1") && isChecked && signIp.Equals(_carWeightDatamanage.IP4))
                {
                    param.Add(pointCode);

                    //关闭当前计量点视频
                    if (_measApp != null && _measApp.Params.FS_POINTCODE.Equals(pointCode))
                    {
                        _measApp.Rtu.OpenRed();
                        if (_measApp.Dvr != null)
                        {
                            if (_isTalk)
                            {
                                _measApp.Dvr.StopTalk();
                                _isTalk = false;
                                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                            }
                            _measApp.Dvr.CloseSound();//关闭声音
                            for (int i = 1; i <= 8; i++)
                            {
                                _measApp.Dvr.StopRealPlay(i);
                            }
                        }
                        _measApp = null;
                        m_iSelectedPound = -1;
                        weighEditorControl1.PointCode = "";
                        weighEditorControl1.PointName = "";
                        meterControl1.Status = MeterStatus.UnConnect;
                        meterControl1.Weight = 0;
                        videoChannel1.Refresh();
                        videoChannel2.Refresh();
                        videoChannel3.Refresh();
                        videoChannel4.Refresh();
                        videoChannel5.Refresh();
                        videoChannel6.Refresh();
                        videoChannel7.Refresh();
                        videoChannel8.Refresh();
                    }

                    //释放计量点连接
                    for (int i = 0; i < _measApps.Length; i++)
                    {
                        if (_measApps[i] != null && _measApps[i].Params.FS_POINTCODE.Equals(pointCode))
                        {
                            if (_measApps[i].Rtu != null)
                            {
                                _measApps[i].Rtu.OpenRed();
                            }
                            if (_measApps[i].Weight != null)
                            {
                                _measApps[i].Weight.WeightChanged -= new YGJZJL.CarSip.Client.Meas.WeightChangedEventHandler(OnWeightChanged1);
                                _measApps[i].Weight.WeightCompleted -= new YGJZJL.CarSip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted1);
                            }
                            if (_measApps[i].IsFlash)
                            {
                                WeighPrompt(_measApps[i], false);
                                Log.Debug("CarWegih.WeighPrompt(" + _measApps[i].Params.FS_POINTCODE + ",false)");
                            }
                            _measApps[i].Finit();
                            _measApps[i] = null;
                            break;
                        }
                    }
                }
            }

            if (param.Count > 0)
            {
                _carWeightDatamanage.ClosePoints(param);
                btnJG.Text = "接 管";
            }

            RefreashPoint();
        }

        /// <summary>
        /// 刷新计量点
        /// </summary>
        private void RefreashPoint()
        {
            _carWeightDatamanage.GetWeightPoints(this.dataTable1);
            WeighPoint wp = new WeighPoint(this.ob);
            int count = 0;
            for (int i = 0; i < ultraGrid2.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow uRow = ultraGrid2.Rows[i];
                string sign = uRow.Cells["FN_POINTFLAG"].Value.ToString().Trim();
                string signIp = uRow.Cells["FS_IP"].Value.ToString().Trim();
                if (sign.Equals("1"))
                {
                    if (signIp.Equals(_carWeightDatamanage.IP4))
                    {
                        uRow.Appearance.BackColor = Color.Green;
                        //uRow.Cells["XZ"].Value = true;
                        count++;
                    }
                    else
                    {
                        uRow.Appearance.BackColor = Color.Yellow;
                        uRow.Cells["XZ"].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    }
                }
                else
                {
                    uRow.Appearance.BackColor = Color.White;
                }
            }
            btnJG.Text = count == 0 ? "接 管" : "取消接管";
        }

        #endregion

        #region 保存重量

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                Save();
            }
        }
        public void Save()
        {
            try
            {
                if (weighEditorControl1.CheckInput())
                {
                    this.Cursor = Cursors.WaitCursor;

                    //meterControl1.Weight = 15;
                    double weight = meterControl1.Weight;

                    if (_measApp != null && _measApp.Weight != null)
                    {
                        _measApp.Weight._receiveFlag = false;
                    }

                    string message = "";
                    bool isAffirm = true;

                    bool isNeedDischarge = weighEditorControl1.IsNeedDischarge;
                    if (weighEditorControl1.Flow.Equals("001"))
                    {
                        isNeedDischarge = true;
                    }

                    if (!_carWeightDatamanage.CheckWeighHistory(weighEditorControl1.CarNo, weight, weighEditorControl1.Flow, isNeedDischarge, weighEditorControl1.DischargeFlag,weighEditorControl1.AlowTareWeight, out message,out isAffirm))
                    {
                        if (!isAffirm)
                        {
                            MessageBox.Show(message, "提示");
                            Cursor = Cursors.Default;
                            if (_measApp != null && _measApp.Weight != null)
                            {
                                _measApp.Weight._receiveFlag = true;
                            }
                            return;
                        }
                        if (MessageBox.Show(message, "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            Cursor = Cursors.Default;
                            if (_measApp != null && _measApp.Weight != null)
                            {
                                _measApp.Weight._receiveFlag = true;
                            }
                            return;
                        }
                    }

                    button1.Enabled = false;
                    messForm.SetMessage("正在计量，请稍候！");
                    //期限皮重更新上次扣渣量
                    if (weighEditorControl1.CardWeightNo.EndsWith("H") && weighEditorControl1.DischargeFlag.Equals("1"))
                    {
                        Hashtable ht = new Hashtable();
                        ht.Add("I1", weighEditorControl1.DischargeFlag);
                        ht.Add("I2", weighEditorControl1.Discharger);
                        ht.Add("I3", weighEditorControl1.DischargeTime);
                        ht.Add("I4", weighEditorControl1.CardDedution);
                        ht.Add("I5", weighEditorControl1.CardWeightNo);
                        ht.Add("I6", "");//预留
                        ht.Add("I7", "");//预留
                        ht.Add("O8", "");
                        ht.Add("O9", "");
                        string[] res = _carWeightDatamanage.SaveCardDischarge(ht);
                    }

                Hashtable param = new Hashtable();
                param.Add("I1", weighEditorControl1.CardNo);
                param.Add("I2", weighEditorControl1.CarNo);
                param.Add("I3", weighEditorControl1.StoveNo1);
                param.Add("I4", weighEditorControl1.StoveNo2);
                param.Add("I5", weighEditorControl1.StoveNo3);
                param.Add("I6", weighEditorControl1.OrderNo);
                param.Add("I7", weighEditorControl1.OrderSeq);
                param.Add("I8", weighEditorControl1.IsTermTare ? "1" : "0");
                param.Add("I9", weighEditorControl1.StoveCount1);
                param.Add("I10", weighEditorControl1.StoveCount2);
                param.Add("I11", weighEditorControl1.StoveCount3);
                param.Add("I12", weighEditorControl1.Material);
                param.Add("I13", weighEditorControl1.MaterialName);
                param.Add("I14", weighEditorControl1.Flow);
                param.Add("I15", weighEditorControl1.TransCompany);
                param.Add("I16", weighEditorControl1.SendCompany);
                param.Add("I17", weighEditorControl1.ReceiveCompany);
                param.Add("I18", weighEditorControl1.SendPlace);
                param.Add("I19", weighEditorControl1.DischargePlace);
                param.Add("I20", weighEditorControl1.WeightType);
                param.Add("I21", weighEditorControl1.Cost);
                param.Add("I22", weighEditorControl1.Deduction);
                param.Add("I23", weighEditorControl1.BillNo);
                param.Add("I24", weighEditorControl1.IsMultiMaterials ? "1" : "0");
                param.Add("I25", weighEditorControl1.SenderWeight);
                param.Add("I26", UserInfo.GetUserID());
                param.Add("I27", UserInfo.GetUserOrder());
                param.Add("I28", UserInfo.GetUserGroup());
                param.Add("I29", weighEditorControl1.PointCode);
                param.Add("I30", weighEditorControl1.PointName);
                param.Add("I31", weighEditorControl1.Remark);
                param.Add("I32", weight.ToString());
                param.Add("I33", weighEditorControl1.TransPlanNo);
                param.Add("I34", weighEditorControl1.DischargeFlag);
                param.Add("I35", !string.IsNullOrEmpty(weighEditorControl1.DischargeFlag) ? weighEditorControl1.Discharger : "");
                if (!string.IsNullOrEmpty(weighEditorControl1.DischargeFlag))
                {
                    try
                    {
                        DateTime.ParseExact(weighEditorControl1.DischargeTime, "yyyyMMddHHmmss", null).ToString("yyyyMMddHHmmss");
                        param.Add("I36", !string.IsNullOrEmpty(weighEditorControl1.DischargeFlag) ? weighEditorControl1.DischargeTime : "");

                    }
                    catch(Exception ee)
                    {
                        param.Add("I36", !string.IsNullOrEmpty(weighEditorControl1.DischargeFlag) ? System.DateTime.Now.ToString("yyyyMMddHHmmss") : "");
                        string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        string strIP = _baseInfo.getIPAddress();
                        string strMAC = _baseInfo.getMACAddress();
                        //string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                        _baseInfo.insertLog(strDate, "汽车", p_UPDATER, strIP, strMAC, "", weighEditorControl1.CarNo, weighEditorControl1.TareWeight.ToString(), weighEditorControl1.DischargeTime, "", "", "DT_CARWEIGHT_WEIGHT", "汽车衡过磅/手持时间错误");

                    }
                }

                    if(string.IsNullOrEmpty(weighEditorControl1.DischargeFlag))
                    {
                        param.Add("I36","");
                    }
                ////param.Add("I36", !string.IsNullOrEmpty(weighEditorControl1.DischargeFlag) ? weighEditorControl1.DischargeTime : "");
                param.Add("I37", !string.IsNullOrEmpty(weighEditorControl1.DischargeFlag) ? weighEditorControl1.DischargeDepart : "");
                param.Add("I38", (int)weighEditorControl1.TermTareType);
                param.Add("I39", weighEditorControl1.SenderGrosssWeight);
                param.Add("I40", weighEditorControl1.SenderTareWeight);
                param.Add("O41", "");
                param.Add("O42", "");
                param.Add("O43", "");

                string[] result = _carWeightDatamanage.SaveWeightData(param);

                    //string debugInfo = "";
                    //for (int i = 1; i != 40;i++ )
                    //{
                    //    debugInfo += "I" + i + "=" + param["I" + i].ToString() + ",";
                    //}
                    //Log.Debug("SaveWeightData: point:"+weighEditorControl1.PointName+ ",params:" + debugInfo);

                    if (string.IsNullOrEmpty(result[0]) || result[0].Trim().Equals("0"))
                    {
                        _preWeightNo = result[1];
                        //MessageBox.Show("计量完成！");
                        //语音电铃截图LCD显示
                        if (_measApp != null)
                        {
                            if (_measApp.Rtu != null)
                            {
                                _measApp.Rtu.OpenRing();
                                Log.Debug("carweight.save() : RTU End!");
                            }
                            if (_measApp.Card != null && !string.IsNullOrEmpty(weighEditorControl1.CardNo))
                            {
                                _cardData = new CardData();
                                _cardData.CarNo = weighEditorControl1.CarNo;
                                _cardData.MateriaName = weighEditorControl1.MaterialName;
                                _cardData.Receiver = weighEditorControl1.ReceiveCompanyName;
                                _cardData.Sender = weighEditorControl1.SendCompanyName;
                                _cardData.PlanLoc = weighEditorControl1.DischargePlace;
                                _cardData.UnloadFlag = " ";
                                _cardData.UnloadKZ = " ";
                                _cardData.WgtOpCd = result[1];
                                if (!string.IsNullOrEmpty(result[1]) && result[1].EndsWith("H"))
                                {
                                    //_cardData.SecLocNo = weighEditorControl1.PointName;
                                    //_cardData.SecWeight = weight.ToString()+"t";
                                    _cardData.FirLocNo = " ";
                                    _cardData.FirWeight = " ";
                                    _cardData.CarNo = " ";
                                    _cardData.MateriaName = " ";
                                    _cardData.Receiver = " ";
                                    _cardData.Sender = " ";
                                    _cardData.PlanLoc = " ";
                                }
                                else
                                {
                                    _cardData.FirLocNo = weighEditorControl1.PointName;
                                    _cardData.FirWeight = weight.ToString() + "t";

                                    _cardData.SecLocNo = " ";
                                    _cardData.SecWeight = " ";
                                }
                                //Thread th = new Thread(ThreadWrite);
                                //th.Start();
                                try
                                {
                                    ThreadWrite();
                                }
                                catch (Exception e)
                                { 

                                }

                                Log.Debug("carweight.save() : IcCard End!");
                            }
                            if (_measApp.Lcd != null)
                            {
                                ShowLcd();
                                Log.Debug("carweight.save() : Lcd End!");
                            }

                            if (_measApp.Dvr != null)
                            {
                                bool talked = false;
                                if (_isTalk)
                                {
                                    talked = true;
                                    _measApp.Dvr.StopTalk();
                                    _isTalk = false;
                                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                                }
                                if (_isReWriteCard)
                                {
                                    _measApp.Dvr.SendVoiceData(_curPath + "\\sound\\刷卡失败 请重新刷卡.wav");
                                    weighEditorControl1.lblStatus.ForeColor = Color.Red;
                                    weighEditorControl1.WeighStatus = "写卡失败，请让司机插卡，然后点击界面上的“补写”按钮！";
                                }

                                else
                                  
                                _measApp.Dvr.SendVoiceData(_curPath + "\\qcsound\\计量完成.wav");
                                Log.Debug("carweight.save() : DVR Send Voice End!");
                                //if (talked)
                                //{
                                //    Thread.Sleep(200);
                                //    _isTalk = _measApp.Dvr.StartTalk();
                                //    if (_isTalk)
                                //    {
                                //        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                                //    }
                                //}
                                Capture(weighEditorControl1.PointCode, result[1], weight);
                                Log.Debug("carweight.save() : DVR Capture End!");
                            }
                        }

                        //采购进厂或二次计量自动打印
                        if (ultraCheckEditor1.Checked && (weighEditorControl1.Flow.Equals("001") || _preWeightNo.EndsWith("H")))
                        {
                            Log.Debug("carweight.save() : Printer Begin!");
                            Print();
                            Log.Debug("carweight.save() : Printer End!");
                        }
                        string strStatus = weighEditorControl1.WeighStatus;
                    
                        weighEditorControl1.Clear();
                        if (strStatus.Contains("补写"))
                        {
                            weighEditorControl1.WeighStatus = strStatus;
                        }
                        weighEditorControl1.CardNo = "";
                        weighEditorControl1.CarNo = "";
                        //_series[m_iSelectedPound].Points.Clear();
                        //NumericDataPoint np = new NumericDataPoint();
                        //np.Value = 0;
                        //_series[m_iSelectedPound].Points.Add(np);

                    //加载一次计量数据
                    _carWeightDatamanage.GetFirstWeighData(this.dataTable11, "", "");
                    _dtFirstWeighData = dataTable11.Copy();
                    ultraGrid3.DataSource = _dtFirstWeighData;

                    SetFirstPicVisible(false);
                    _measApp.IsSaved = true;//保存标识
                    button1.Enabled = false;
					messForm.Visible = false;
                }
                    if (!string.IsNullOrEmpty(result[2]))
                    {
                        MessageBox.Show(_isReWriteCard ? (result[2] + "   请补写卡！") : result[2]);


                    }

                   
                    weighEditorControl1.Focus();
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Log.Error("carweigh.save() : " + ex.Message);
            }
            catch
            {
                Log.Error("carweigh.save() Error!");
            }
            finally
            {
                if (_measApp != null && _measApp.Weight != null)
                {
                    _measApp.Weight._receiveFlag = true; ;
                }
            }
        }

        /// <summary>
        /// 显示LCD数据
        /// </summary>
        private void ShowLcd()
        {
            _measApp.Lcd.ClearScreen();
            _measApp.Lcd.DrawPicture((int)LCD_PICTURE.TEXT);

            HgLable hgLable = _carWeightDatamanage.GetPrintData(_preWeightNo);
            if (hgLable != null)
            {
                hgLable.WeightPoint = weighEditorControl1.PointName;
                hgLable.MeasTech = weighEditorControl1.Weigher;
                hgLable.Type = LableType.CAR;

                ArrayList data = null;
                if (!string.IsNullOrEmpty(hgLable.Charge))
                {
                    data = new ArrayList(6);
                    data.Add("车  号: " + hgLable.CarNo);
                    data.Add("时  间: " + hgLable.Date.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
                    data.Add("重  量: " + hgLable.Weight);
                    data.Add("计量点: " + hgLable.WeightPoint);
                    data.Add("备  注: 外协     收费金额:" + hgLable.Charge);
                    _measApp.Lcd.Data = data.ToArray();
                    _measApp.Lcd.DisplayData();
                    return;
                }
                data = new ArrayList();

                data.Add("发货单位: " + hgLable.SupplierName);
                data.Add("收货单位: " + hgLable.Receiver);
                data.Add("物资名称: " + hgLable.MaterialName);
                data.Add("车号: " + hgLable.CarNo);

                //钢坯二次计量打印
                if (!string.IsNullOrEmpty(hgLable.StoveNo) && hgLable.WeightNum == "1")
                {
                    data.Add("时间: " + hgLable.Date.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
                    data.Add("毛重: " + hgLable.GrossWeight + " t");
                    data.Add("皮重: " + hgLable.TareWeight + " t");
                    data.Add("净重: " + hgLable.NetWeight + " t");
                    _measApp.Lcd.Data = data.ToArray();
                    _measApp.Lcd.DisplayData();
                    return;
                }
                //钢坯一次计量打印
                else if (!string.IsNullOrEmpty(hgLable.StoveNo) && hgLable.WeightNum == "")
                {
                    data.Add("时间: " + hgLable.Date.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
                    data.Add("重量: " + hgLable.Weight + " t");
                    _measApp.Lcd.Data = data.ToArray();
                    _measApp.Lcd.DisplayData();
                    return;
                }
                // 二次计量打印
                if (string.IsNullOrEmpty(hgLable.StoveNo) && hgLable.WeightNum == "1")
                {
                    data.Add("时间: " + hgLable.Date.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
                    data.Add("毛重: " + hgLable.GrossWeight + " t");
                    data.Add("皮重: " + hgLable.TareWeight + " t");
                    if (string.IsNullOrEmpty(hgLable.Rate) && string.IsNullOrEmpty(hgLable.DeductWeight))
                    {
                        data.Add("净重: " + hgLable.NetWeight + " t");
                    }
                    else
                    {
                        data.Add("净重(扣后): " + hgLable.DeductAfterWeight + " t" + (!string.IsNullOrEmpty(hgLable.DeductWeight) ? "   扣渣量: " + hgLable.DeductWeight + " t" + "   扣渣比例: " + hgLable.Rate : ""));
                    }
                }
                // 一次计量打印
                else
                {
                    data.Add("时间: " + hgLable.Date.ToString("yyyy年MM月dd日 HH时mm分ss秒"));
                    data.Add("重量: " + hgLable.Weight + " t");
                }
                _measApp.Lcd.Data = data.ToArray();
                _measApp.Lcd.DisplayData();
                _measApp.Lcd.LcdStatus = LCD_PICTURE.TEXT;
            }
        }

        private new void Capture(string pointCode, string weightNo, double weight)
        {
            string imgPath = _curPath + "\\pic\\" + pointCode + "{0}.jpg";
            for (uint i = 1; i <= 8; i++)
            {
                _measApp.Dvr.CapturePicture(i, string.Format(imgPath, i));
            }
            string newPic = WriteTextOnPic(string.Format(imgPath, 1), weight.ToString());

            ArrayList pics = new ArrayList();
            pics.Add(ConvertPic(newPic));
            for (uint i = 2; i <= 8; i++)
            {
                pics.Add(ConvertPic(string.Format(imgPath, i)));
            }
            CarNumericSeries carNumericSeries = (CarNumericSeries)(ultraChart1.Series[0]);
            _carWeightDatamanage.SavePicData(weightNo, pics, carNumericSeries.PointsString, "0");
        }

        private string WriteTextOnPic(string imgPath, string text)
        {
            if (System.IO.File.Exists(imgPath))
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
                Graphics g = Graphics.FromImage(image);
                Font f = new Font("宋体", 28);
                Brush b = new SolidBrush(Color.Red);
                g.DrawString(text, f, b, 100, 20);
                //转换成JPG   
                image.Save(imgPath + ".jpg");
                image.Dispose();
                return imgPath + ".jpg";
            }

            return imgPath;
        }

        private byte[] ConvertPic(string imgPath)
        {
            byte[] pic = new byte[1];

            if (System.IO.File.Exists(imgPath))
            {
                pic = System.IO.File.ReadAllBytes(imgPath);
            }

            return pic;
        }

        public delegate void CardWriter();//写卡委托
        private CardWriter _writeCardDelegate;//建立委托变量
        private CardData _cardData = null;//写卡数据
        private void ThreadWrite()
        {
            _writeCardDelegate = new CardWriter(WriteCard);
            Invoke(_writeCardDelegate);
        }

        private void WriteCard()
        {
            //_measApp.Card.CardThread.Suspend();
            try
            {
                _isReWriteCard = !_measApp.Card.WriteCard(_cardData);
            }
            catch (Exception ex)
            {
                _isReWriteCard = true;
            }
      
            //_measApp.Card.CardThread.Resume();
        }
        //private void WriteCard()
        //{
        //    //_measApp.Card.CardThread.Suspend();
        //    _measApp.Card.WriteCard(_cardData);
        //    //_measApp.Card.CardThread.Resume();
        //}

        #endregion

        #region test
        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(((CarNumericSeries)ultraChart1.Series[0]).PointsString);
            //ShowLcd();

            //CardData cd = _measApp.Card.ReadCard();
            //MessageBox.Show(cd.ToString());
            //if (_measApp.Card.CardThread.ThreadState == ThreadState.Suspended)
            //{
            //    _measApp.Card.CardThread.Resume();
            //}

            //CardData data = new CardData();
            //data.CardNo = "00301";
            //HandleCardChange(data);

            Save();
        }
        #endregion

        #region 刷新计量点
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreashPoint();
            //_measApp.Dvr.SendVoiceData("E:\\Harpor\\昆钢电子信息\\红玉钢集中计量\\客户端程序\\玉钢\\Output\\qcsound\\计量完成.wav");//测试语音用
        }
        #endregion

        #region toolbar click
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "YYDJ"://打开对讲
                    HandleTalk();
                    break;
                case "ButtonTool2"://钢坯查询
                    break;
                case "YCJLTX"://一次计量图片查看
                    panel17.Visible = !panel17.Visible;
                    panel18.Visible = !panel18.Visible;
                    panel19.Visible = !panel19.Visible;
                    panel20.Visible = !panel20.Visible;
                    break;
                case "BDDY"://磅单打印
                    Print();
                    break;
                case "HZ"://换纸
                    if (_measApp != null && _measApp.Params != null)
                    {
                        _carWeightDatamanage.ReloadPrinterPaper(weighEditorControl1.PointCode);
                        txtZZ.Text = _carWeightDatamanage.GetPrinterPaperCount(weighEditorControl1.PointCode).ToString();
                    }
                    break;
                case "Query"://预报查询
                    this.Cursor = Cursors.WaitCursor;

                    QuickPlanInfo planInfo = new QuickPlanInfo(this.ob);
                    if (DialogResult.OK == planInfo.ShowDialog())
                    {
                        weighEditorControl1.SendCompanyName = planInfo.s_SENDER;
                        weighEditorControl1.MaterialName = planInfo.s_MATERIALNAME;
                        weighEditorControl1.Flow = planInfo.s_POINTFLOW;
                        weighEditorControl1.ReceiveCompanyName = planInfo.s_RECEIVERFACTORY;
                        weighEditorControl1.TransCompanyName = planInfo.s_TRANSNO;
                        weighEditorControl1.SendPlace = planInfo.s_SENDERSTORE;
                        weighEditorControl1.DischargePlace = planInfo.s_RECEIVERSTORE;
                        planInfo.ParameData();
                    }

                    this.Cursor = Cursors.Default;
                    break;
                case "ButtonTool10"://期限皮重管理
                    break;
                case "btCorrention":
                    this.CarWeightdetail();
                    break;
                default:
                    break;
            }
        }

        private void Print()
        {
            if (_measApp.Printer != null)
            {
                HgLable hgLable = _carWeightDatamanage.GetPrintData(_preWeightNo);
                if (hgLable != null)
                {
                    hgLable.WeightPoint = weighEditorControl1.PointName;
                    //hgLable.BarCode = _preWeightNo;
                    hgLable.MeasTech = weighEditorControl1.Weigher;
                    hgLable.Type = LableType.CAR;
                    _measApp.Printer.Data = hgLable;
                    _measApp.Printer.PrinterName = weighEditorControl1.PointCode;
                    int printCount = 1;
                    int.TryParse(numericUpDown1.Value.ToString(),out printCount);
                    try
                    {
                        for (int i = 0; i < printCount; i++)
                        {
                            _measApp.Printer.Print();

                            //纸张-1
                            _carWeightDatamanage.ReducePrinterPaper(weighEditorControl1.PointCode);
                            int count = 0;
                            int.TryParse(txtZZ.Text.Trim(), out count);
                            txtZZ.Text = (--count).ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("打印失败，请检查是否安装打印机{0}。",weighEditorControl1.PointCode));
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Print2();
        }

        /// <summary>
        /// 红钢现场过磅用榜单打印，玉钢不用
        /// </summary>
        private void Print2()
        {
            try
            {
                //只打印完整过磅记录
                if (_preWeightNo.EndsWith("H"))
                {
                    HgLable hgLable = _carWeightDatamanage.GetPrintData(_preWeightNo);
                    if (hgLable != null)
                    {
                        hgLable.WeightPoint = weighEditorControl1.PointName;
                        hgLable.BarCode = _preWeightNo;
                        hgLable.MeasTech = weighEditorControl1.Weigher;
                        hgLable.Type = LableType.CAR;
                        ArrayList param = new ArrayList();
                        switch (weighEditorControl1.PointCode)
                        {
                            case "K001":
                                param.Add("一号");
                                break;
                            case "K002":
                                param.Add("二号");
                                break;
                            case "K003":
                                param.Add("三号");
                                break;
                            case "K004":
                                param.Add("四号");
                                break;
                            case "K011":
                                param.Add("十一号");
                                break;
                            case "K012":
                                param.Add("十二号");
                                break;
                            case "K013":
                                param.Add("十三号");
                                break;
                            case "K014":
                                param.Add("十四号");
                                break;

                            default:
                                return;
                        }
                        param.Add(_preWeightNo);
                        param.Add(hgLable.MaterialName);
                        param.Add("t");
                        param.Add(hgLable.CarComment);
                        param.Add(hgLable.GrossWeight);
                        param.Add(hgLable.SupplierName);
                        param.Add(hgLable.TareWeight);
                        param.Add(hgLable.TransName);
                        param.Add("");
                        param.Add(hgLable.Receiver);
                        param.Add(hgLable.DeductWeight);
                        param.Add(hgLable.Flow);
                        param.Add(hgLable.NetWeight);
                        param.Add(hgLable.CarNo);
                        param.Add(hgLable.MeasTech);
                        param.Add(hgLable.GrossWeightTime);
                        param.Add(hgLable.TareWeightTime);

                        YGJZJL.CarSip.Client.Printer.ExcelPrinter ePrinter = new YGJZJL.CarSip.Client.Printer.ExcelPrinter("weight.xml", param);
                        ePrinter.PageSetup.IsCustom = true;
                        ePrinter.PageSetup.TopMargin = 0.5;
                        ePrinter.PageSetup.BottomMargin = 1;
                        ePrinter.PageSetup.LeftMargin = 1;
                        ePrinter.PageSetup.RightMargin = 1;
                        ePrinter.printExcel();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败,请关闭程序后重新操作！" + ex.Message);
                return;
            }
        }

        /// <summary>
        /// 处理语音对讲按钮事件
        /// </summary>
        private void HandleTalk()
        {
            try
            {
                if (_measApp == null || _measApp.Dvr == null)
                {
                    return;
                }
                if (_isTalk)//正在对讲，关闭
                {
                    _isTalk = !_measApp.Dvr.StopTalk();
                    if (!_isTalk)
                    {
                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                    }
                }
                else
                {
                    _isTalk = _measApp.Dvr.StartTalk();
                    if (_isTalk)
                    {
                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("语音播放" + ex.Message);
            }
        }

        /// <summary>
        /// 校秤
        /// </summary>
        private void CarWeightdetail()
        {
            string weightNo = "";
            double weight = meterControl1.Weight;
            string compatedate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string correntrer = UserInfo.GetUserID();
            string shift = UserInfo.GetUserOrder();
            string group = UserInfo.GetUserGroup();
            string remark = weighEditorControl1.Remark;
            string pointcode = weighEditorControl1.PointCode;
            string carno = weighEditorControl1.CarNo;

            string correntionId = _carWeightDatamanage.Corrention(pointcode, carno, weight, correntrer, shift, group, remark);
            Capture(pointcode, correntionId, weight);
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 6)
            {
                numericUpDown1.Value = 0;
            }
            else if (numericUpDown1.Value < 0)
            {
                numericUpDown1.Value = 6;
            }
        }
        #endregion

        #region 视频放大还原
        private void bigVideoChannel_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
        }

        private void videoChannel1_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(1);
        }

        private void videoChannel2_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(2);
        }

        private void videoChannel3_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(3);
        }

        private void videoChannel4_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(4);
        }

        private void videoChannel5_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(5);
        }

        private void videoChannel6_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(6);
        }

        private void videoChannel7_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(7);
        }

        private void videoChannel8_DoubleClick(object sender, EventArgs e)
        {
            OpenBigPicture(8);
        }

        private void OpenBigPicture(int channelId)
        {
            if (channelId < 1 || channelId == _bigChannelId || _measApp == null || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            bool isTalkNow = false;
            if (_isTalk && (channelId == 1 || _bigChannelId == 1))
            {
                isTalkNow = true;
                _measApp.Dvr.StopTalk();
                _isTalk = false;
                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }
            //关闭第7通道声音
            if (channelId == 7 || _bigChannelId == 7)
            {
                Thread.Sleep(100);
                _measApp.Dvr.CloseSound();
            }
            _measApp.Dvr.StopRealPlay(channelId);
            Thread.Sleep(100);
            if (_bigChannelId >= 0)
            {
                _measApp.Dvr.StopRealPlay(_bigChannelId);
                _measApp.Dvr.RealPlay(_bigChannelId, ((PictureBox)Controls.Find("videoChannel" + _bigChannelId.ToString(), true)[0]).Handle);
            }

            _measApp.Dvr.RealPlay(channelId, bigVideoChannel.Handle);
            Thread.Sleep(100);
            //打开第7通道声音
            if (channelId == 7 || _bigChannelId == 7)
            {
                _measApp.Dvr.OpenSound(6);
                Thread.Sleep(100);
            }
            if (isTalkNow)
            {
                _isTalk = _measApp.Dvr.StartTalk();
                if (_isTalk)
                {
                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                }
            }
            bigVideoChannel.Width = videoChannel1.Width * 2;
            bigVideoChannel.Height = videoChannel1.Height * 2;
            bigVideoChannel.Visible = true;
            bigVideoChannel.BringToFront();

            _bigChannelId = channelId;
        }

        /// <summary>
        /// 关闭大图监视，还原小图监视
        /// </summary>
        private void CloseBigPicture()
        {
            if (_measApp == null || _bigChannelId < 0 || _measApp.Dvr == null || string.IsNullOrEmpty(_measApp.Params.FS_VIEDOIP))
            {
                return;
            }

            bool isTalkNow = false;
            if (_bigChannelId == 1 && _isTalk)
            {
                isTalkNow = true; ;
                _measApp.Dvr.StopTalk();
                _isTalk = false;
                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }
            Thread.Sleep(100);
            //关闭第7通道声音
            if (_bigChannelId == 7)
            {
                _measApp.Dvr.CloseSound();
            }
            Thread.Sleep(100);
            _measApp.Dvr.StopRealPlay(_bigChannelId);
            Thread.Sleep(100);
            _measApp.Dvr.RealPlay(_bigChannelId, ((PictureBox)Controls.Find("videoChannel" + _bigChannelId.ToString(), true)[0]).Handle);
            //打开第7通道声音
            if (_bigChannelId == 7)
            {
              _measApp.Dvr.OpenSound(6);
            }
            if (isTalkNow)
            {
                _isTalk = _measApp.Dvr.StartTalk();
                if (_isTalk)
                {
                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                }
            }

            bigVideoChannel.Visible = false;
            _bigChannelId = -1;
        }

        #region 拖动视频窗口(与云台控制共用全局变量)
        private void bigVideoChannel_MouseDown(object sender, MouseEventArgs e)
        {
            _mousedown = true;
            _moveLeft = e.X;
            _moveTop = e.Y;
        }

        private void bigVideoChannel_MouseLeave(object sender, EventArgs e)
        {
            _mousedown = false;
        }

        private void bigVideoChannel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mousedown)
            {
                bigVideoChannel.Left += e.X - _moveLeft;
                bigVideoChannel.Top += e.Y - _moveTop;
                _moveLeft = e.X;
                _moveTop = e.Y;
            }
        }

        private void bigVideoChannel_MouseUp(object sender, MouseEventArgs e)
        {
            _mousedown = false;
        }
        #endregion
        #endregion

        #region 一次计量记录选择
        private void ultraGrid3_DoubleClick(object sender, EventArgs e)
        {
            if (ultraGrid3.ActiveRow != null && ultraGrid3.ActiveRow.Index > -1)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow uRow = ultraGrid3.ActiveRow;
                weighEditorControl1.DT_TransPlan.Clear();
                DataRow dr = weighEditorControl1.DT_TransPlan.NewRow();
                dr["FS_PLANCODE"] = uRow.Cells["FS_PLANCODE"].Value.ToString();
                dr["FS_CARDNUMBER"] = uRow.Cells["FS_CARDNUMBER"].Value.ToString();
                dr["FS_CARNO"] = uRow.Cells["FS_CARNO"].Value.ToString();
                dr["FS_CONTRACTNO"] = uRow.Cells["FS_CONTRACTNO"].Value.ToString();
                dr["FS_CONTRACTITEM"] = uRow.Cells["FS_CONTRACTITEM"].Value.ToString();
                dr["FS_SENDER"] = uRow.Cells["FS_SENDER"].Value.ToString();
                dr["FS_FHDW"] = uRow.Cells["FS_FHDW"].Value.ToString();
                dr["FS_PROVIDER"] = uRow.Cells["FS_PROVIDER"].Value.ToString();
                dr["FS_SENDERSTORE"] = uRow.Cells["FS_SENDERSTORE"].Value.ToString();
                dr["FS_MATERIAL"] = uRow.Cells["FS_MATERIAL"].Value.ToString();
                dr["FS_MATERIALNAME"] = uRow.Cells["FS_MATERIALNAME"].Value.ToString();
                dr["FS_RECEIVERFACTORY"] = uRow.Cells["FS_RECEIVERFACTORY"].Value.ToString();
                dr["FS_SHDW"] = uRow.Cells["FS_SHDW"].Value.ToString();
                dr["FS_TRANSNO"] = uRow.Cells["FS_TRANSNO"].Value.ToString();
                dr["FS_CYDW"] = uRow.Cells["FS_CYDW"].Value.ToString();
                dr["FS_WEIGHTTYPE"] = uRow.Cells["FS_WEIGHTTYPE"].Value.ToString();
                dr["FS_LX"] = uRow.Cells["FS_LX"].Value.ToString();
                dr["FS_POUNDTYPE"] = uRow.Cells["FS_POUNDTYPE"].Value.ToString();
                dr["FN_SENDGROSSWEIGHT"] = uRow.Cells["FN_SENDGROSSWEIGHT"].Value.ToString();
                dr["FN_SENDTAREWEIGHT"] = uRow.Cells["FN_SENDTAREWEIGHT"].Value.ToString();
                dr["FN_SENDNETWEIGHT"] = uRow.Cells["FN_SENDNETWEIGHT"].Value.ToString();
                dr["FS_STOVENO"] = uRow.Cells["FS_STOVENO"].Value.ToString();
                dr["FN_BILLETCOUNT"] = uRow.Cells["FN_COUNT"].Value.ToString();
                dr["FS_POINT"] = uRow.Cells["FS_POUNDTYPE"].Value.ToString();
                dr["FS_DATETIME"] = uRow.Cells["FD_WEIGHTTIME"].Value.ToString();
                dr["FS_IFSAMPLING"] = uRow.Cells["FS_IFSAMPLING"].Value.ToString();
                dr["FS_IFACCEPT"] = uRow.Cells["FS_IFACCEPT"].Value.ToString();
                dr["FS_DRIVERNAME"] = uRow.Cells["FS_DRIVERNAME"].Value.ToString();
                dr["FS_DRIVERIDCARD"] = uRow.Cells["FS_DRIVERIDCARD"].Value.ToString();
                weighEditorControl1.DT_TransPlan.Rows.Add(dr);
                weighEditorControl1.BandTransPlan();
            }
        }
        #endregion

        #region 播放语音
        private bool _isSendVoice = true;
        private void ultraGrid1_DoubleClick(object sender, EventArgs e)
        {
            if (_isSendVoice && ultraGrid5.ActiveRow != null && ultraGrid5.ActiveRow.Index >= 0)
            {

                ultraGrid5.Cursor = Cursors.WaitCursor;
                if (_measApp == null)
                {
                    MessageBox.Show("请先选择一个计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ultraGrid5.Cursor = Cursors.Hand;
                    return;
                }
                if (ultraGrid5.Rows.Count <= 0)
                {
                    MessageBox.Show("没有声音文件，请添加声音文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ultraGrid5.Cursor = Cursors.Hand;
                    return;
                }

                if (_measApp.Dvr != null)
                {
                    if (_isTalk)
                    {
                        _isTalk = false;
                        _talkId = -1;
                        _measApp.Dvr.StopTalk();
                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                    }

                    FileInfo fi = new FileInfo(_curPath + "\\sound\\" + ultraGrid5.ActiveRow.Cells["FS_VOICENAME"].Value.ToString().Trim());
                    int waveTimeLen = Convert.ToInt32((fi.Length - 54) / 16 + 500);
                    _measApp.Dvr.SendVoiceData(_curPath + "\\sound\\" + ultraGrid5.ActiveRow.Cells["FS_VOICENAME"].Value.ToString().Trim());
                    _isSendVoice = false;
                    Thread thread = new Thread(allowSend);
                    thread.Start(waveTimeLen);
                }

                ultraGrid5.Cursor = Cursors.Hand;
                weighEditorControl1.Focus();
            }
        }

        private void allowSend(object millisencondsTimeout)
        {
            Thread.Sleep((int)millisencondsTimeout);
            _isSendVoice = true;
        }
        #endregion

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string sign = "";
            bool isChecked = false;
            string signIp = "";
            string pointCode = "";
            Infragistics.Win.UltraWinGrid.UltraGridRow uRow = null;
            switch (e.ClickedItem.Text)
            {
                case "接管":
                    //接管计量点,把计量点标记改为已接管
                    BandPoint();
                    //判断称上是否来车，如果有车计量点闪烁               
                    //FlashGridRow(sult);
                    InitBandPoints();
                    break;
                case "取消":
                    ClosePoint();
                    break;
                case "全选(已接管)":
                    for (int i = 0; i < ultraGrid2.Rows.Count; i++)
                    {
                        uRow = ultraGrid2.Rows[i];
                        sign = uRow.Cells["FN_POINTFLAG"].Text.Trim();
                        signIp = uRow.Cells["FS_IP"].Text.Trim();
                        pointCode = uRow.Cells["FS_POINTCODE"].Text.Trim();
                        if (sign.Equals("1") && signIp.Equals(_carWeightDatamanage.IP4))
                        {
                            uRow.Cells["XZ"].Value = true;
                        }
                        else
                        {
                            uRow.Cells["XZ"].Value = false;
                        }
                    }
                    break;
                case "全选(未接管)":
                    for (int i = 0; i < ultraGrid2.Rows.Count; i++)
                    {
                        uRow = ultraGrid2.Rows[i];
                        sign = uRow.Cells["FN_POINTFLAG"].Text.Trim();
                        signIp = uRow.Cells["FS_IP"].Text.Trim();
                        pointCode = uRow.Cells["FS_POINTCODE"].Text.Trim();
                        if (sign.Equals("0"))
                        {
                            uRow.Cells["XZ"].Value = true;
                        }
                        else
                        {
                            uRow.Cells["XZ"].Value = false;
                        }
                    }
                    break;
                case "取消选择":
                    for (int i = 0; i < ultraGrid2.Rows.Count; i++)
                    {
                        uRow = ultraGrid2.Rows[i];
                        uRow.Cells["XZ"].Value = false;
                    }
                    break;

            }
            ultraGrid2.UpdateData();
            weighEditorControl1.Clear();
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            ultraGrid2.UpdateData();
            int count1 = 0, count2 = 0;
            string sign = "";
            bool isChecked = false;
            string signIp = "";
            string pointCode = "";
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow uRow in ultraGrid2.Rows)
            {
                sign = uRow.Cells["FN_POINTFLAG"].Text.Trim();
                isChecked = Convert.ToBoolean(uRow.Cells["XZ"].Value);
                signIp = uRow.Cells["FS_IP"].Text.Trim();
                pointCode = uRow.Cells["FS_POINTCODE"].Text.Trim();
                if (isChecked)
                {
                    count1 += sign.Equals("1") ? 1 : 0;
                    count2 += sign.Equals("0") ? 1 : 0;
                }
                contextMenuStrip1.Items[0].Enabled = count1 == 0;
                contextMenuStrip1.Items[1].Enabled = count2 == 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_measApp != null && _measApp.Card != null)
            {
                DataTable dt = new DataTable();
                _carWeightDatamanage.GetCardData(weighEditorControl1.CardNo, weighEditorControl1.CarNo, dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    _cardData = new CardData();
                    _cardData.CarNo = dr["FS_CARNO"].ToString(); ;
                    _cardData.MateriaName = dr["FS_MATERIALNAME"].ToString();
                    _cardData.Receiver = dr["FS_SHDW"].ToString();
                    _cardData.Sender = dr["FS_FHDW"].ToString();
                    _cardData.PlanLoc = dr["FS_RECEIVERFACTORY"].ToString();
                    _cardData.UnloadFlag = " ";
                    _cardData.UnloadKZ = " ";
                    _cardData.WgtOpCd = dr["FS_WEIGHTNO"].ToString();
                    _cardData.FirLocNo = dr["FS_POUND"].ToString();
                    _cardData.FirWeight = dr["FN_WEIGHT"].ToString() + "t";
                    _cardData.SecLocNo = " ";
                    _cardData.SecWeight = " ";
                    if (_measApp.Card.WriteCard(_cardData))
                    {
                        MessageBox.Show("补写成功！");
                        weighEditorControl1.lblStatus.ForeColor = Color.Black;
                        weighEditorControl1.WeighStatus = "";
                    }
                    else
                    {
                        MessageBox.Show("补写失败！");
                    }
                }
                else
                {
                    _measApp.Card.ClearCard();
                }
            }
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                GC.Collect();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
