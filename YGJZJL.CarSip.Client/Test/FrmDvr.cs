using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.CarSip.Client.Meas;
using YGJZJL.CarSip.Client.App;
using System.Threading;
namespace YGJZJL.CarSip.Client.Test
{
    public partial class FrmDvr : Form
    {
        private CoreApp _measApp;
        private HkDvr _dvr;

        public FrmDvr()
        {
            InitializeComponent();
            _measApp = new CoreApp();
            _dvr = new HkDvr();
        }

       
        #region <称重处理方法>
        delegate void setText(string str);
        public void OnWeightChanged(object sender, WeightEventArgs e)
        {
            Invoke(new setText(HandleWeightChange), new object[] { e.Value.ToString() });
        }
        private void HandleWeightChange(string weight)
        {
            txtWtChange.Text = weight;  
        }
        private void HandleWeightCompelte(string weight)
        {
            txtWtComplete.Text = weight;
        }
        public void OnWeightCompleted(object sender, WeightEventArgs e)
        {
            this.Invoke(new setText(HandleWeightCompelte), new object[] { e.Value.ToString() });
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            //sdk = new  SDK_Com.HKDVR();
            //BT_POINT param = new BT_POINT();
            //_measApp.Params = new BT_POINT();
            //_measApp.Init();
            //_measApp.Weight.WeightChanged += new YGJZJL.CarSip.Client.Meas.WeightChangedEventHandler(OnWeightChanged);
            //_measApp.Weight.WeightCompleted += new YGJZJL.CarSip.Client.Meas.WeightCompletedEventHandler(OnWeightCompleted);
            //_measApp.VideoChannel[0] = _measApp.Dvr.SDK_RealPlay(1, 0, (int)Video1.Handle);
            //_measApp.VideoChannel[1] = _measApp.Dvr.SDK_RealPlay(2, 0, (int)Video2.Handle);
            //_measApp.VideoChannel[2] = _measApp.Dvr.SDK_RealPlay(3, 0, (int)Video3.Handle);
            //_measApp.Run();
            //_dvr.Init("10.25.3.241,8000,admin,12345");
            _dvr.Init("10.25.168.252,8000,admin,12345");
            
            _dvr.Open();
            _dvr.RealPlay(1, Video1.Handle);
            _dvr.RealPlay(2, Video2.Handle);
            _dvr.RealPlay(3, Video3.Handle);
        } 

        private void button4_Click(object sender, EventArgs e)
        {
            ////关闭音频
            ////参数：对应视频句柄，音频通常对应第一路视频
            //sdk.SDK_CloseSound(lRealHandle);

            //if (lRealHandle > 0)
            //{
            //    //关闭视频
            //    //参数：视频句柄
            //    sdk.SDK_StopRealPlay(lRealHandle);
            //}
            ////硬盘录象机登出
            //sdk.SDK_Logout();
            ////SDK资源释放
            //sdk.SDK_Cleanup();

            //m_lLoginID = -1;
            //m_lTalkHandle = -1;
            //lRealHandle = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool ret = _dvr.CapturePicture(1, "d:\\123.jpg");
            string errMsg = "";
            if (!ret) errMsg = _dvr.GetErrorMessage(); 
            SetDvrRetValue(errMsg);
        }

        private void SetDvrRetValue(string ret)
        {
            txtDvrRet.Text = ret.ToString();
        }

        private void btCardWrite_Click(object sender, EventArgs e)
        {
            _measApp.Card.WriteData( (int)nudSector.Value, (int)nudBlock.Value,txtCardValue.Text);
            tbID.Text = _measApp.Card.ID;
        }

        private void btCardRead_Click(object sender, EventArgs e)
        {
            txtCardValue.Text = _measApp.Card.ReadData((int)nudSector.Value, (int)nudBlock.Value);
            tbID.Text = _measApp.Card.ID;
        }

        private void btTalk_Click(object sender, EventArgs e)
        {
            //开始对讲
            //返回：对讲句柄           
            SetDvrRetValue(_dvr.VoiceHandle.ToString());
            if (_dvr.VoiceHandle<0)
            {
                _dvr.SDK_StartTalk();
                btTalk.Text = "停止语音";
            }
            else    //关闭对讲
            {
                _dvr.SDK_StopTalk();               
                btTalk.Text = "开始语音";
            }
        }

        private void btDvrTimeSyn_Click(object sender, EventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            if (!_dvr.ConfigTime(dtNow))
            {
                SetDvrRetValue(_dvr.GetErrorMessage());
            }
        }

        private void btSendVoice_Click(object sender, EventArgs e)
        {
            //自定义语音发送
            //参数：文件路径，按采样标准（采样频率为16000，16位采样，单通道）获取的PCM音频数据
            //int ret = _measApp.Dvr.SDK_SendData("D:\\计量完成1.wav");            
            //SetDvrRetValue(ret);
            //if (ret < 0) _measApp.Dvr.SDK_StopTalk();
            bool ret = _dvr.SendVoiceData("D:\\ProductComplete.wav");
        }

        private void FrmDvr_FormClosing(object sender, FormClosingEventArgs e)
        {         
            _measApp.Finit();
            _dvr.Close();
        }

       

        private void btLedPowerOff_Click(object sender, EventArgs e)
        {
            _measApp.Led.SetPower((int)LedScreen.PowerStatus.OFF);
        }

        private void btLedPowerOn_Click(object sender, EventArgs e)
        {
            _measApp.Led.SetPower((int)LedScreen.PowerStatus.ON);
        }

        private void btLedWrite_Click(object sender, EventArgs e)
        {

            _measApp.Led.SendText(txtLedData.Text
                , "宋体"
                , 12
                ,(int)LedScreen.Method.IMMEDIATE
                , (int)LedScreen.Speed.LEVEL1
                ,(int)LedScreen.Transparent.OFF);
        }

        private void btPlayBack_Click(object sender, EventArgs e)
        {
            tpStart.Value = DateTime.Now.AddHours(-5);
            _dvr.StopRealPlay(2);
            bool ret = _dvr.PlayBackByTime(3, tpStart.Value, tpStop.Value, Video2.Handle);
            if (!ret) SetDvrRetValue(_dvr.GetErrorMessage());
            _dvr.FindFile(3, tpStart.Value, tpStop.Value);            
            foreach (HkDvr.FindData data in _dvr.VideoFiles)
            {
                listBox1.Items.Add(data.FileName);
            }
        }

     
       
    }
}
