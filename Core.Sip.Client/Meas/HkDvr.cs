using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.IO;

namespace Core.Sip.Client.Meas
{
   public class HkDvr :   IDevice
    {
       #region <定义辅助类和方法>
       const int G722_WAVE_FRAME_SIZE = 1280;
       const int G722_VOICE_FRAME_SIZE = 80;
       const int G711_WAVE_FRAME_SIZE = 320;
       const int G711_VOICE_FRAME_SIZE = 160;
      
       public struct WavInfo
       {
           public string groupid;
           public string rifftype;
           public long filesize;
           public string chunkid;
           public long chunksize;

           public short wformattag;      //记录着此声音的格式代号，例如WAVE_FORMAT_PCM，WAVE_F0RAM_ADPCM等等。
           public ushort wchannels;      //记录声音的频道数。
           public ulong dwsamplespersec; //记录每秒取样数。
           public ulong dwavgbytespersec;//记录每秒的数据量。
           public ushort wblockalign;    //记录区块的对齐单位。
           public ushort wbitspersample; //记录每个取样所需的位元数。

           public string datachunkid;
           public long datasize;
       }

       public  bool GetWavInfo(string strpath)
       {
           WavInfo wavInfo = new WavInfo();
           FileInfo fi = new FileInfo(strpath);
           FileStream fs = fi.OpenRead();
           if (fs.Length >= 44)
           {
               byte[] bInfo = new byte[44];
               fs.Read(bInfo, 0, 44);
               wavInfo.groupid = Encoding.Default.GetString(bInfo, 0, 4);
               wavInfo.filesize = BitConverter.ToInt32(bInfo, 4);
               wavInfo.rifftype = Encoding.Default.GetString(bInfo, 8, 4);
               wavInfo.chunkid = Encoding.Default.GetString(bInfo, 12, 4);
               if (wavInfo.groupid == "RIFF" && wavInfo.rifftype == "WAVE" && wavInfo.chunkid == "fmt ")
               {                 
                   wavInfo.chunksize = BitConverter.ToInt32(bInfo, 16);
                   wavInfo.wformattag = BitConverter.ToInt16(bInfo, 20);
                   wavInfo.wchannels = BitConverter.ToUInt16(bInfo, 22);
                   wavInfo.dwsamplespersec = BitConverter.ToUInt32(bInfo, 24);
                   wavInfo.dwavgbytespersec = BitConverter.ToUInt32(bInfo, 28);
                   wavInfo.wblockalign = BitConverter.ToUInt16(bInfo, 32);
                   wavInfo.wbitspersample = BitConverter.ToUInt16(bInfo, 34);
                   wavInfo.datachunkid = Encoding.Default.GetString(bInfo, 36, 4);
                   wavInfo.datasize = BitConverter.ToInt32(bInfo, 40);
                   // 读取音频数据
                   int readBytes = 44, bufBytes = 0, buffSize = 0; 
                   int dataSize = (int)wavInfo.datasize;
                   byte[] fileBuf = new byte[G722_WAVE_FRAME_SIZE];
                   _wav_buffer = new sbyte[G722_WAVE_FRAME_SIZE];
                   _dec_buffer = new sbyte[G722_VOICE_FRAME_SIZE];
                   //fs.Read(_buffer, 44, wavInfo.datasize);
                   while (readBytes < dataSize)
                   {
                       bufBytes = dataSize - readBytes;
                       if (bufBytes > G722_WAVE_FRAME_SIZE) bufBytes = G722_WAVE_FRAME_SIZE;
                       buffSize = fs.Read(fileBuf, 0, bufBytes);
                       for (int i = 0; i < bufBytes; i++)
                       {
                           if (fileBuf[i] > 127)
                           {
                               _wav_buffer[i] = (sbyte)(fileBuf[i] - 256);
                           }
                           else
                           {
                               _wav_buffer[i] = (sbyte)fileBuf[i];
                           }                          
                       }
                       HCNetSDK.NET_DVR_EncodeG722Frame(_dec_handle, _wav_buffer, _dec_buffer);
                       readBytes += buffSize;
                   }

                   return true;
               }
           }
           return false;
       }
       #endregion

       #region <成员变量>
        //登录标识
        private int _user_id  = -1;
        //预览标识
        private int[] _real_handles = null;
        private int _max_channel_num = 36;
        //对讲句柄
        int _talk_handle = -1;
        //语音转发句柄
        int voiceHandle = -1;
        // 
        bool bSendingVoiceData = false;
       // 设备信息
       private NET_DVR_DEVICEINFO_V30 deviceInfo = new NET_DVR_DEVICEINFO_V30();
       // 客户信息
       private NET_DVR_CLIENTINFO lpClientInfo = new NET_DVR_CLIENTINFO();
       // 登录信息
       private string _ip = "";
       private ushort _port = 0;
       private string _user_name = "";
       private string _password = "";
       // 音频信息
       private sbyte[] _wav_buffer = null; // Wave 文件缓存
       private sbyte[] _dec_buffer = null; // G722编码缓存
       IntPtr _dec_handle = new IntPtr(-1);           // 编码句柄
       #endregion
       #region<构造函数>
       public HkDvr()
       {
           //clientInfo = new NET_DVR_DEVICEINFO_V30();
           // 初始化通道
           _real_handles = new int[_max_channel_num];
           for (int i = 0; i < _real_handles.Length; i++)
           {
               _real_handles[i] = -1;
           }
       }
       #endregion
       #region <设备接口>
       public bool Open()
       {
           int ret = -1;
           if (!SDK_Init()) return false;
           ret = SDK_Login(_ip, _port, _user_name, _password);
           if (ret < 0) return false;
           _dec_handle = HCNetSDK.NET_DVR_InitG722Encoder();
           return true;
       }
       public bool Close()
       {
           HCNetSDK.NET_DVR_ReleaseG722Encoder(_dec_handle);
           if (!SDK_Logout()) return false;
           return SDK_Cleanup();
       }
       #endregion
       #region<SDK 函数>
       public bool Init(string configParams)
       {
           string[] strParams = configParams.Split(new char[] { ',' });
           _ip = strParams[0];
           _port = Convert.ToUInt16(strParams[1]);
           _user_name = strParams[2];
           _password = strParams[3];
           return true;
       }
       // 初始化设备SDK
        public bool SDK_Init()
        {       
            return  HCNetSDK.NET_DVR_Init();           
        }
        

       protected bool SDK_Cleanup()
        {
            bool ret = false;
            ret = HCNetSDK.NET_DVR_Cleanup();
            return ret;
        }

        // 设置网络
       public bool SetConnectTime(uint waitTime, uint tryTimes)
        {
            bool ret = false;
            ret = HCNetSDK.NET_DVR_SetConnectTime(waitTime, tryTimes);
            return ret;
        }

       public bool SetReconnect(uint interval, bool isReconnect)
        {
            bool ret = false;
            return ret;
        }

        // 登录DVR系统
       public int SDK_Login(string ip, ushort port, string username, string password)
        {
            _user_id  = HCNetSDK.NET_DVR_Login_V30(ip, port, username, password, out deviceInfo);
            return _user_id ;
        }

        public bool SDK_Logout()
        {
            bool ret = false;
            ret = HCNetSDK.NET_DVR_Logout(_user_id);
            return ret;
        }

       //
       public int RealPlay(int lChannel, IntPtr hPlayWnd)
       {
           return SDK_RealPlay(lChannel, 0, hPlayWnd);
       }

        //视频控制
       public int SDK_RealPlay(int lChannel, int lLinkMode,IntPtr hPlayWnd)
        {
            lpClientInfo = new NET_DVR_CLIENTINFO();
            lpClientInfo.lChannel = lChannel;
            lpClientInfo.lLinkMode = lLinkMode;
            lpClientInfo.hPlayWnd = hPlayWnd;
            _real_handles[lChannel - 1] = HCNetSDK.NET_DVR_RealPlay_V30(_user_id, ref lpClientInfo, null, 1, false);
            return _real_handles[lChannel - 1];
        }

       // 关闭所有通道
       public void StopAllRealPlay() 
       {
           for (int i = 0; i < _real_handles.Length; i++)
           {
               if (_real_handles[i] > -1)
               {
                   SDK_StopRealPlay(_real_handles[i]);
               }
           }
       }

       public bool SDK_StopRealPlay(int lRealHandle)
       {
           bool ret = false;
           ret = HCNetSDK.NET_DVR_StopRealPlay(lRealHandle);
           // add 
           HCNetSDK.NET_DVR_SetAudioMode(1);
           return ret;
       }

       // 默认取第一个通道
       public bool OpenSound()
       {
           bool ret = false;
           ret = HCNetSDK.NET_DVR_OpenSound(_real_handles[0]);
           ret = HCNetSDK.NET_DVR_Volume(_real_handles[0], 65535);// 设置最大声音
           return ret;
       }
        // 采集声音
       public bool SDK_OpenSound(int lRealHandle)
       {
           bool ret = false;
           ret = HCNetSDK.NET_DVR_OpenSound(lRealHandle);
           HCNetSDK.NET_DVR_Volume(lRealHandle, 65535);// 设置最大声音
           return ret;
       }

       // 关闭声音采集
       public bool SDK_CloseSound()
       {
           bool ret = false;
           HCNetSDK.NET_DVR_CloseSound();
           return ret;
       }

       public bool SDK_SetVolume(ushort vol)
       {
           bool ret = false;   //???? 
           ret = HCNetSDK.NET_DVR_Volume(_user_id , vol);
           return ret;
       }
       // 
       public void OnVoiceData(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser)
       {

       }
       // 启动语音对讲
       public bool SDK_StartTalk()
       {
           IntPtr pUser = new IntPtr();
           int ret = HCNetSDK.NET_DVR_StartVoiceCom_V30(_user_id, 1, false, OnVoiceData, pUser);
           if (ret < 0)
           {
               SDK_StopTalk();
               return false;
           }
           else 
           {
               _talk_handle = ret;
               return true;
           }   
           return false;
       }

       public bool SDK_StopTalk()
        {
            bool ret = false;
            if (_talk_handle > -1) ret = HCNetSDK.NET_DVR_StopVoiceCom(_talk_handle);
            _talk_handle = -1;
            return ret;            
        }

        public bool SendVoiceData(string fileName)
        {
            if (_talk_handle < 0) SDK_StopTalk();
            if (!File.Exists(fileName)) return false;
            if(!GetWavInfo(fileName)) return false;
            
            return true;
        }

        // 抓图
       public bool CapturePicture(uint channelId, string fileName)
        {
            bool ret = false;
            return ret;
        }

        //设置设备时间
       public bool ConfigTime(DateTime dateTime)
        {
            bool ret = false;
            return ret;
        }
       #endregion
    }
}
