using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
namespace Core.Sip.Client.Meas
{
    public class SsIpCamera : CoreDevice, IDevice, IDvr
    {
        #region <事件>
        public event CallBackCapture OnCapture=null;
        #endregion
        #region <成员变量>
        //登录标识
        private uint _dev_handle  = 0;
        //预览标识
        private uint _real_handle = 0;
        private int _max_channel_num = 8;
       
        //语音句柄， 1#通道
        int _voice_handle = -1;
        // 
        bool _sending_data = false;
       // 设备信息
        private NET_DVR_DEVICEINFO_V30 deviceInfo = new NET_DVR_DEVICEINFO_V30();
       // 客户信息
        private NET_DVR_CLIENTINFO lpClientInfo = new NET_DVR_CLIENTINFO();
       // 登录信息
       private string _ip = "10.35.10.210";
       private ushort _port = 0;
       private string _user_name = "admin";
       private string _password = "4321";
       private string _model_name = "SNZ-5200";
       // 解码器
       IntPtr _dec_handle = new IntPtr(-1);           // 编码句柄
       // 线程
       Thread _wav_thread = null;
       Thread _find_thread = null;
       // 错误信息
       uint _error_code = 0;
       string _error_message = "";
       // 回放句柄
       int _play_handle = -1;
       int _find_handle = -1;
       // 视频文件处理
       ArrayList _find_files = null;
       // 警告处理
       int _alarm_handle = -1;
       #endregion
       #region<属性>
       // 
       public uint RealChannel
       {
           get { return _real_handle; }
           set { _real_handle = value; }
       }
       public uint DevId
       {
           get { return _dev_handle; }
           set { _dev_handle = value; }
       }
       public int VoiceHandle
       {
           get { return _voice_handle; }
           set { _voice_handle = value; }
       }
       public ArrayList VideoFiles
       {
           get { return _find_files; }
       }
       public string ErrorMessage
       {
           get { return _error_message; }
       }
       #endregion
       #region<构造函数>
       public SsIpCamera()
       {
           // 初始化通道
           _real_handle = 0;//new int[_max_channel_num];
           _ip = "10.35.10.210";
           _port = 4520;
           _user_name = "admin";
           _password = "4321";
           _model_name = "SNZ-5200";      
       }
       #endregion
       #region <设备接口>
        // 需要
       public bool Init(string configParams)
       {
           string[] strParams = configParams.Split(new char[] { ',' });
           if (strParams.Length < 1) return false;           
           if (strParams.Length > 0) _ip = strParams[0];
           if (strParams.Length > 1) _port = Convert.ToUInt16(strParams[1]);
           if (strParams.Length > 2) _user_name = strParams[2];
           if (strParams.Length > 3) _password = strParams[3];
           return true;
       }
       public bool Open()
       {
           int ret = -1;
           if (!SSNetSDK.XNS_DEV_Init()) return false;
           if (!Login()) return false;           
           return true;
       }
       public bool Close()
       {

           //CloseSound();
           // 视频
           StopRealPlay();      
           // 关闭警告
           //if (_alarm_handle > 0) HCNetSDK.NET_DVR_CloseAlarmChan_V30(_alarm_handle);
           Logout();
           if (!SSNetSDK.XNS_DEV_Cleanup()) return false;
           return true;
       }
       #endregion
        // 登录DVR系统
        public bool Login()
       {
           LPXNS_DEV_DEVICEINFO lp = new LPXNS_DEV_DEVICEINFO();
           string content = "123";
           IntPtr intptr = Marshal.AllocHGlobal(Marshal.SizeOf(lp));
           Marshal.StructureToPtr(lp, intptr, true);
           _dev_handle = SSNetSDK.XNS_DEV_Login
              (_ip, _port, _user_name, _password, _model_name
              , ref content, intptr, true, 10006);
           if (_dev_handle == 0) return false;
           return true;
       }
        public bool Logout()
        {
            if (_dev_handle>0) return SSNetSDK.XNS_DEV_Logout(_dev_handle);
            return false;
        }

        //视频控制
        public bool RealPlay(int channel, IntPtr hPlayWnd)
        {
            _real_handle = SSNetSDK.XNS_DEV_StartRealPlay(_dev_handle,channel, hPlayWnd);
            if (_real_handle == 0) return false;
            return true;
        }
        public bool RealPlay(IntPtr hPlayWnd)
        {
            return RealPlay(2, hPlayWnd);
        }
        public bool StopRealPlay(int channel)
        {
            if (_real_handle > 0) return SSNetSDK.XNS_DEV_StopRealPlay(_real_handle);
            return false;
        }
        public bool StopRealPlay()
        {
            if (_real_handle>0)  return SSNetSDK.XNS_DEV_StopRealPlay(_real_handle);
            return false;
        }
        // 采集声音
        public bool OpenSound()
        {
            return SSNetSDK.XNS_DEV_OpenSound(_real_handle);
        }
        public bool CloseSound()
        {
            return SSNetSDK.XNS_DEV_CloseSound(_real_handle);
        }

        public bool SetVolume(ushort vol)
        {
            return SSNetSDK.XNS_DEV_SetVolume(_real_handle, vol);
        }

        // 语言控制
        public bool StartTalk()
        {
            return SSNetSDK.XNS_DEV_StartTalk(_dev_handle, 1, false);
        }
        public bool StopTalk()
        {
            return SSNetSDK.XNS_DEV_StopTalk(_dev_handle, 1);
        }

        // 转发语音数据
        public bool SendVoiceData(string fileName)
        {
            bool ret = false;
            if (!StopTalk()) return false;
            if (!SSNetSDK.XNS_DEV_StartTalk(_dev_handle, 1, true)) return false;
            //SSNetSDK.XNS_DEV_SendAudioData(_dev_handle,)
            return true;
        }

        // 抓图
        public bool CapturePicture(uint channel, string fileName)
        {
            IntPtr ptr = new IntPtr(0);
            return SSNetSDK.XNS_DEV_SaveSnapshot(_real_handle,  fileName, 2);
        }

        //设置设备时间
        public bool ConfigTime(DateTime dateTime)
        {
            return false;
        }

        // 设置网络
        public bool SetConnectTime(uint waitTime, uint tryTimes)
        {
            return false;
        }
        public bool SetReconnect(uint interval, bool isReconnect)
        {
            return false;
        }

        // 云台控制
        public bool PTZControl(int channel, uint command, uint stop, int speed)
        {
            return SSNetSDK.XNS_DEV_PTZControlWithSpeed(_real_handle, command, stop, speed);
        }
    }
}
