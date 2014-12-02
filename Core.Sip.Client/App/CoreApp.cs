using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Sip.Client.Meas;
using SDK_Com;
namespace Core.Sip.Client.App
{
    public class CoreApp : IApplication
    {
        #region <成员变量>
        
        // 数据采集仪表
        private CoreRtu _rtu = null;         // 仪表
        private IcCard _card = null;     // 读卡器
        private LCDScreen _lcd = null;   // 液晶屏
        private LedScreen _led = null;   // LED显示屏
        private CoreIoLogik _iologic = null; // MOXA IoLogic
        private CoreWeight _weight = null;
        private CorePrinter _printer = null;
        //线程
        private System.Threading.Thread _thread; //线程
        //++++++++++++++++++++++++++++++++++++++++++++++++++++
        private SsIpCamera[] _cameras = null;
        //++++++++++++++++++++++++++++++++++++++++++++++++++++
        // private  Cameras 
        // 视频
        private SDK_Com.HKDVR _dvr;
        private int[] _video_channel;
        // 配置参数
        BT_POINT _params;
        #endregion

        #region <属性>
        // 基础信息
        public string Name { get; set; }
        public string Code { get; set; }
        public string Depart { get; set; }
        public BT_POINT Params
        {
            get { return _params; }
            set { _params = value; }
        }
        public CoreRtu Rtu
        {
            get { return _rtu; }
            set { _rtu = value; }
        }
        public IcCard Card
        {
            get { return _card; }
            set {  _card = value; }
        }
        public LCDScreen Lcd
        {
            get { return _lcd; }
            set { _lcd = value; }
        }
        public LedScreen Led
        {
            get { return _led; }
            set { _led = value; }
        }
        public CoreIoLogik IoLogik
        {
            get { return _iologic; }
            set { _iologic = value; }
        }
        public SDK_Com.HKDVR Dvr
        {
            get { return _dvr; }
            set { _dvr = value; }
        }
        public CoreWeight Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public int[] VideoChannel
        {
            get { return _video_channel; }
            set {  _video_channel = value; }
        }
        public CorePrinter Printer
        {
            get { return _printer; }
            set { _printer = value; }
        }

        //++++++++++++++++++++++++++++++++++++++++++++++++++++
        public SsIpCamera[] Cameras
        {
            get { return _cameras; }
            set { _cameras = value; }
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++
        #endregion
        #region <公共方法>
        public int Init()
        {
            int ret = -1;
            string [] strParams = new string [5];
            //_params = new BT_POINT();             
            
            _thread = null;//new System.Threading.Thread();
           

            if (_params.FS_RTUIP != "")
            {
                _rtu = new CoreRtu();
                _rtu.IP = _params.FS_RTUIP;
                if (_params.FS_RTUPORT != "") _rtu.Port = Convert.ToUInt16(_params.FS_RTUPORT);
            }
            if (_params.FS_READERPARA != "")
            {
                _card = new IcCard();
                strParams = _params.FS_READERPARA.Split(new char[] { ',' });
                _card.Port = Convert.ToInt16(strParams[0]);
                _card.BaudRate = Convert.ToInt32(strParams[1]);
            }
            if (_params.FS_LEDIP != "")
            {
                _led = new LedScreen();
                _led.Init(_params.FS_LEDIP);
                
                //_led.RemotePort = Convert.ToInt32(_params.FS_LEDPORT);
            }

            if (_params.FS_DISPLAYPARA != "")
            {
                _lcd = new LCDScreen();
                _lcd.init(_params.FS_DISPLAYPARA);
            }
            if (_params.FS_MOXAIP != "")
            {
                _iologic = new CoreIoLogik();
                _iologic.IP = _params.FS_MOXAIP;
                if (_params.FS_MOXAPORT != "") _iologic.Port = Convert.ToInt32(_params.FS_MOXAPORT);
            }
            if (_params.FS_VIEDOIP != "")
            {
                //**************************************************************
                strParams = _params.FS_VIEDOIP.Split(new char[] { ',' });
                if (strParams.Length < 2)
                {
                    _dvr = new SDK_Com.HKDVR();
                    _dvr.SDK_Init();
                    _dvr.SDK_Login(_params.FS_VIEDOIP, Convert.ToInt32(_params.FS_VIEDOPORT), _params.FS_VIEDOUSER, _params.FS_VIEDOPWD);
                }
                else
                {
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    Cameras = new SsIpCamera[strParams.Length];
                    for (int i = 0; i < Cameras.Length; i++)
                    {
                        Cameras[i] = new SsIpCamera();
                        Cameras[i].Init(strParams[i]);
                    }
                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                }
            }

            if (_params.FS_METERPARA != "")
            {
                _weight = new CoreWeight();
                _weight.DeviceName = _params.FS_METERTYPE;
                _weight.init(_params.FS_METERPARA);
            }
            if (_params.FS_PRINTERIP != "")
            {
                _printer = new CorePrinter();
                _printer.Init(_params.FS_PRINTERNAME);
            }
            // 初始化视频通道
            _video_channel = new int[8];
            for (int i = 0; i < _video_channel.Length; i++) _video_channel[i] = -1;
            ret = 0;
            return ret; 
        }

        public void Run()
        {
            //if (_dvr != null) _dvr.SDK_Init();
            if (_lcd != null) _lcd.Open();
            if (_led != null) _led.Open();
            if (_iologic != null) _iologic.Open();
            if (_card != null) _card.Open();
            if (_weight != null) _weight.Open();
            if (Cameras!=null&&Cameras.Length > 0)
            {
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                bool rs = SSNetSDK.XNS_DEV_Init();
                for (int i = 0; i < Cameras.Length; i++)
                {
                    Cameras[i].Login();
                }
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
        }

        public int Finit()
        {
            int ret = -1;
            if (_dvr != null)
            {
                for (int i = 0; i < _video_channel.Length; i++)
                {
                    if (_video_channel[i] > -1)
                    {
                        _dvr.SDK_StopRealPlay(_video_channel[i]);
                    }
                }
                _dvr.SDK_Logout();
                _dvr.SDK_Cleanup();
            }            
            if (_lcd != null) _lcd.Close();
            if (_led != null) _led.Close();
            if (_iologic != null) _iologic.Close();
            if (_card != null) _card.Close();
            if (_weight != null) _weight.Close();
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            if (Cameras != null&&Cameras.Length > 0)
            {
                for (int i = 0; i < Cameras.Length; i++)
                {
                    if (Cameras[i] == null) continue;
                    Cameras[i].StopRealPlay();
                    Cameras[i].Logout();
                }
                SSNetSDK.XNS_DEV_Cleanup();
                //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            }
            return ret;
        }
        #endregion
    }
}
