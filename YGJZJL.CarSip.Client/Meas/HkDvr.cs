using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace YGJZJL.CarSip.Client.Meas
{
   #region <定义辅助类和方法>
   public delegate void VoiceDataEventHandler(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);
   //public delegate void MessageEventHandler(int lCommand, ref HCNetSDK.NET_DVR_ALARMER pAlarmer, string pAlarmInfo, uint dwBufLen, IntPtr pUser);
   #endregion 

   public class HkDvr : CoreDevice, IDevice, IDvr
    {
       #region <定义辅助类和方法>     
       // 将DateTime 转 DVR时间
       private HCNetSDK.NET_DVR_TIME DateTimeToDvrTime(DateTime dt)
       {
           HCNetSDK.NET_DVR_TIME dvrTime = new HCNetSDK.NET_DVR_TIME();
           dvrTime.dwYear = (uint)dt.Year;
           dvrTime.dwMonth = (uint)dt.Month;
           dvrTime.dwDay = (uint)dt.Day;
           dvrTime.dwHour = (uint)dt.Hour;
           dvrTime.dwMinute = (uint)dt.Minute;
           dvrTime.dwSecond = (uint)dt.Second;
           return dvrTime;
       }
       // 将DVR时间转换为DateTime时间
       private DateTime DvrTimeToDataTime(HCNetSDK.NET_DVR_TIME dvrTime)
       {
           DateTime dt = new DateTime
               ( (int)dvrTime.dwYear
               , (int)dvrTime.dwMonth
               , (int)dvrTime.dwDay
               , (int)dvrTime.dwHour
               , (int)dvrTime.dwMinute
               , (int)dvrTime.dwSecond);                
           return dt;
       }
       // 查找回放文件结构
       public class FindData
       {
           public string FileName { get; set; }      // 文件名
           public DateTime StartTime { get; set; }   // 文件的开始时间
           public DateTime StopTime { get; set; }    // 文件的结束时间
           public uint FileSize { get; set; }        // 文件的大小
           public bool Locked { get; set; }          // 文件是否被锁      
       }
       // 常量定义
       const int G722_WAVE_FRAME_SIZE = 1280;
       const int G722_VOICE_FRAME_SIZE = 80;
       const int G711_WAVE_FRAME_SIZE = 320;
       const int G711_VOICE_FRAME_SIZE = 160;
       const int SAMPLES_PER_SECOND_G711_MU = 8000;
       const int SAMPLES_PER_SECOND_G722_MU = 16000; 
       const int VIOCE_CHANNEL = 1;
       const int BITS_PER_SAMPLE = 16;
       static HCNetSDK.VoiceDataCallBack _call_back = new HCNetSDK.VoiceDataCallBack(OnVoiceData);
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

       public void SendWavFile(object strpath)
       {
           WavInfo wavInfo = new WavInfo();
           FileInfo fi = new FileInfo((string)strpath);
           FileStream fs = fi.OpenRead();
           bool ret = false;
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
                   int readBytes = 0, bufBytes = 0, buffSize = 0; 
                   int dataSize = (int)wavInfo.datasize;
                   byte[] fileBuf = new byte[G722_WAVE_FRAME_SIZE];
                   byte[] _wav_buffer = new byte[G722_WAVE_FRAME_SIZE];
                   byte[] _dec_buffer = new byte[G722_VOICE_FRAME_SIZE];
                   //fs.Read(_buffer, 44, wavInfo.datasize);
                   while (readBytes < dataSize)
                   {
                       bufBytes = dataSize - readBytes;
                       if (bufBytes > G722_WAVE_FRAME_SIZE) bufBytes = G722_WAVE_FRAME_SIZE;
                       buffSize = fs.Read(fileBuf, 0, bufBytes);
                       if (buffSize == 0) break;      
                       ret = HCNetSDK.NET_DVR_EncodeG722Frame(_dec_handle, fileBuf, _dec_buffer);
                       ret = HCNetSDK.NET_DVR_VoiceComSendData(_voice_handle, _dec_buffer, G722_VOICE_FRAME_SIZE);
                       readBytes += buffSize;                      
                       Thread.Sleep(40);
                   }
                   StopTalk();
                   //return true;
               }
           }
          // return false;
           
       }
       #endregion
       #region <事件>
       public event HCNetSDK.MSGCallBack MessageReceived;  //MessageEventHandler
       #endregion 
       #region <成员变量>
        //登录标识
        private int _user_id  = -1;
        //预览标识
        private int[] _real_handles = null;
        private int _max_channel_num = 36;
       
        //语音句柄， 1#通道
        int _voice_handle = -1;
        // 
        bool _sending_data = false;
       // 设备信息
        private NET_DVR_DEVICEINFO_V30 deviceInfo = new NET_DVR_DEVICEINFO_V30();
       // 客户信息
        private NET_DVR_CLIENTINFO lpClientInfo = new NET_DVR_CLIENTINFO();
       // 登录信息
       private string _ip = "";
       private ushort _port = 0;
       private string _user_name = "";
       private string _password = "";
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
       public int[] RealChannels
       {
           get { return _real_handles; }
           set { _real_handles = value; }
       }
       public int UserId
       {
           get { return _user_id; }
           set { _user_id = value; }
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
       public HkDvr()
       {
           // 初始化通道
           _real_handles = new int[_max_channel_num];
           for (int i = 0; i < _real_handles.Length; i++)
           {
               _real_handles[i] = -1;
           }
           _find_files = new ArrayList();
           //_call_back = new HCNetSDK.VoiceDataCallBack(OnVoiceData);
           // 初始化线程
           //_thread = new Thread(new ParameterizedThreadStart(SendWavFile));
           //_find_thread = new Thread(new ThreadStart());
       }
       #endregion
       #region<回调函数>
       
       // 语音回调函数
       public static void OnVoiceData(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser)
       {

       }
       #endregion
       #region <设备接口>
       public bool Init(string configParams)
       {
           string[] strParams = configParams.Split(new char[] { ',' });
           _ip = strParams[0];
           _port = Convert.ToUInt16(strParams[1]);
           _user_name = strParams[2];
           _password = strParams[3];
           return true;
       }
       public bool Open()
       {
           try
           {
               int ret = -1;
               if (!HCNetSDK.NET_DVR_Init()) return false;
               _user_id = HCNetSDK.NET_DVR_Login_V30(_ip, _port, _user_name, _password, out deviceInfo);
               if (_user_id < 0) return false;
               _dec_handle = HCNetSDK.NET_DVR_InitG722Encoder();
               // 增加消息处理时间
               if (MessageReceived != null)
               {
                   IntPtr pUser = new IntPtr();
                   bool bRet = HCNetSDK.NET_DVR_SetDVRMessageCallBack_V30(MessageReceived, pUser);
                   _alarm_handle = HCNetSDK.NET_DVR_SetupAlarmChan_V30(_user_id);
               }
               return true;
           }
           catch (Exception ex)
           {
               WriteLog("硬盘录像机打开失败:"+ex.Message);
               return false;
           }
          
       }
       public bool Close()
       {
           try
           {
               //if (_call_back != null)
               //{
               //    _call_back = null;
               //}
               // 关闭音频
               CloseSound();
               // 视频
               StopAllRealPlay();
               // 释放音频解码
               HCNetSDK.NET_DVR_ReleaseG722Encoder(_dec_handle);
               // 关闭警告
               if (_alarm_handle > 0) HCNetSDK.NET_DVR_CloseAlarmChan_V30(_alarm_handle);
               if (!HCNetSDK.NET_DVR_Logout(_user_id)) return false;
               if (!HCNetSDK.NET_DVR_Cleanup()) return false;
               return true;
           }
           catch (Exception ex)
           {
               
             WriteLog("硬盘录像机关闭失败:"+ex.Message);
               return false;
           }
           
       }
       #endregion
       #region<SDK 函数>       
       // 初始化设备SDK
        public int SDK_Init()
        {       
            bool ret =   HCNetSDK.NET_DVR_Init();
            if (!ret) return -1;
            return 0;
        }
        
       // 释放SDK
       protected int SDK_Cleanup()
        {
            bool ret = HCNetSDK.NET_DVR_Cleanup();
            if (!ret) return -1;
            return 0;
        }    

        // 登录DVR系统
       public int SDK_Login(string ip, ushort port, string username, string password)
        {
            _user_id  = HCNetSDK.NET_DVR_Login_V30(ip, port, username, password, out deviceInfo);
            return _user_id ;
        }
        
        // 登出DVR系统
        public int SDK_Logout()
        {
            bool ret = false;
            ret = HCNetSDK.NET_DVR_Logout(_user_id);
            if (!ret) return -1;
            return 0;
        }       

        //视频控制
       public int SDK_RealPlay(int lChannel, int lLinkMode,IntPtr hPlayWnd)
        {
            lpClientInfo = new NET_DVR_CLIENTINFO();
            lpClientInfo.lChannel = lChannel;
            lpClientInfo.lLinkMode = lLinkMode;
            lpClientInfo.hPlayWnd = hPlayWnd;
            _real_handles[lChannel - 1] = HCNetSDK.NET_DVR_RealPlay_V30(_user_id, ref lpClientInfo, null, 1, false);
            HCNetSDK.NET_DVR_SetAudioMode(1);
            return _real_handles[lChannel - 1];
        }

       // 关闭视频
       public int SDK_StopRealPlay(int lRealHandle)
       {
           bool ret = false;
           ret = HCNetSDK.NET_DVR_StopRealPlay(lRealHandle);      
           if (!ret) return -1;
           return 0;
       }

       // 采集声音
       public int SDK_OpenSound(int lRealHandle)
       {
           try
           {
               bool ret = false;
               ret = HCNetSDK.NET_DVR_OpenSound(lRealHandle);
               HCNetSDK.NET_DVR_Volume(lRealHandle, 65535);// 设置最大声音
               if (!ret) return -1;
               return 0;
           }
           catch (Exception ex)
           {
               
             WriteLog("采集声音失败:"+ex.Message);
               return -1;
           }
         
       }

       // 关闭声音采集
       public int SDK_CloseSound(int lRealHandle)
       {
           try
           {
               bool ret = false;
               HCNetSDK.NET_DVR_CloseSound();
               if (!ret) return -1;
               return 0;
           }
           catch (Exception ex) 
           {
                 WriteLog("关闭声音采集失败:"+ex.Message);
               return -1;
           }
        
       }

       // 设置音量
       public int SDK_SetVolume(ushort vol)
       {
           bool ret = false;   //???? 
           ret = HCNetSDK.NET_DVR_Volume(_user_id, vol);
           if (!ret) return -1;
           return 0;
       }

       // 启动语音对讲
       public int SDK_StartTalk()
       {
           try
           {
               IntPtr pUser = new IntPtr(88);
               if (_voice_handle > -1) SDK_StopTalk();
               //if (_call_back == null) _call_back = new HCNetSDK.VoiceDataCallBack(OnVoiceData);
               _voice_handle = HCNetSDK.NET_DVR_StartVoiceCom_V30(_user_id, 1, false, _call_back, pUser);
               return _voice_handle;
           }
           catch (Exception ex)
           {

               WriteLog("启动语音对讲失败:" + ex.Message);
               return -1;
           }
          
       }

       // 关闭语音对讲
       public int SDK_StopTalk()
       {
           try
           {
               bool ret = false;
               if (_voice_handle > -1) ret = HCNetSDK.NET_DVR_StopVoiceCom(_voice_handle);
               _voice_handle = -1;
               if (!ret) return -1;
               return 0;
           }
           catch (Exception ex)
           {
             
               WriteLog("关闭语音对讲失败:" + ex.Message);
               return -1;
           }
         
       }

       public int SDK_SendData(string fileName)
       {
           bool ret = SendVoiceData(fileName);
           if (!ret) return -1;          
           return 0;
       }
       public int SDK_CapturePicture(int lChannel, string fileName)
       {
           HCNetSDK.NET_DVR_JPEGPARA jpegPara = new HCNetSDK.NET_DVR_JPEGPARA();
           jpegPara.wPicQuality = 0;
           jpegPara.wPicSize = 0;
           bool ret = HCNetSDK.NET_DVR_CaptureJPEGPicture(_user_id, lChannel, ref jpegPara, fileName);
           if (!ret) return -1;
           return 0; 
       }
       public int SDK_ConfigTime(int year, int month, int day, int hour, int minute, int second)
       {
           HCNetSDK.NET_DVR_TIME dvrTime = new HCNetSDK.NET_DVR_TIME();
           dvrTime.dwYear = (uint)year;
           dvrTime.dwMonth = (uint)month;
           dvrTime.dwDay = (uint)day;
           dvrTime.dwHour = (uint)hour;
           dvrTime.dwMinute = (uint)minute;
           dvrTime.dwSecond = (uint)second;
           byte[] buf = HCNetSDK.StructToBytes(dvrTime);         
           bool ret = HCNetSDK.NET_DVR_SetDVRConfig(_user_id, HCNetSDK.NET_DVR_SET_TIMECFG, 0, buf, (uint)buf.Length);
           return 0;
       }
       #endregion

       //-----------------------------新API-------------------------------
       #region<DVR 函数>   
       // 登录DVR系统
       public bool Login()
       {
           _user_id = HCNetSDK.NET_DVR_Login_V30(_ip, _port, _user_name, _password, out deviceInfo);
           if (_user_id < 0) return false ;
           return true;
       }

       // 登出DVR系统
       public bool Logout()
       {
           return HCNetSDK.NET_DVR_Logout(_user_id);           
       }
       
       // 播放视频
       public bool RealPlay(int channel, IntPtr hPlayWnd)
       {
           lpClientInfo = new NET_DVR_CLIENTINFO();
           lpClientInfo.lChannel = channel;
           lpClientInfo.lLinkMode = 0;
           lpClientInfo.hPlayWnd = hPlayWnd;
           _real_handles[channel - 1] = HCNetSDK.NET_DVR_RealPlay_V30(_user_id, ref lpClientInfo, null, 1, false);
           HCNetSDK.NET_DVR_SetAudioMode(1);
           if (_real_handles[channel - 1] < 0) return false;
           return true;
       }

       // 关闭对应通道的视频
       public bool StopRealPlay(int channel)
       {
           try
           {
               return HCNetSDK.NET_DVR_StopRealPlay(RealChannels[channel - 1]);
           }
           catch (Exception ex)
           {
               WriteLog("StopRealPlay(" + (channel-1).ToString ()+")" + ex.Message);
               return false;
           }
           
       }

       // 关闭所有通道
       protected void StopAllRealPlay()
       {
           for (int i = 0; i < _real_handles.Length; i++)
           {
               if (_real_handles[i] > -1)
               {
                   StopRealPlay(i+1);
               }
           }
       }      

       // 默认取第一个通道
       public bool OpenSound()
       {
           return OpenSound(0);
       }

       /// <summary>
       /// 打开声音
       /// </summary>
       /// <param name="channel">通道编号0-7</param>
       /// <returns></returns>
       public bool OpenSound(int channel)
       {
           try
           {
               bool ret = false;
               if (!HCNetSDK.NET_DVR_OpenSound(_real_handles[channel])) return false;
                bool bs = HCNetSDK.NET_DVR_Volume(_real_handles[channel], 65535);// 设置最大声音
                return bs;
           }
           catch (Exception ex)
           {
               WriteLog("打开声音" + ex.Message);
               return false;
           }
        
       }

       // 关闭声音采集
       public bool CloseSound()
       {
           try
           {
               return HCNetSDK.NET_DVR_CloseSound();
           }
           catch (Exception ex)
           {
               WriteLog("关闭声音采集" + ex.Message);
               return false;
           }
          
       }

       // 启动语音对讲
       public bool StartTalk()
       {
           try
           {
               IntPtr pUser = new IntPtr(88);
               if (_voice_handle > -1) SDK_StopTalk();
               //if (_call_back == null) _call_back  = new HCNetSDK.VoiceDataCallBack(OnVoiceData);
               _voice_handle = HCNetSDK.NET_DVR_StartVoiceCom_V30(_user_id, 1, false, _call_back, pUser);
               if (_voice_handle < 0) return false;
               return true;
           }
           catch (Exception ex)
           {
               WriteLog("启动语音对讲失败:" + ex.Message);
               return false;
           }
        
       }

       // 关闭语音对讲
       public bool StopTalk()
       {
           try
           {
               bool ret = false;
               if (_voice_handle > -1) return HCNetSDK.NET_DVR_StopVoiceCom(_voice_handle);
               _voice_handle = -1;
               return false;
           }
           catch (Exception ex)
           {
               WriteLog("关闭语音对讲失败:" + ex.Message);
               return false;
           }
         
       }

       // 设置音量
       public bool SetVolume(ushort vol)
       {
           try
           {
               return HCNetSDK.NET_DVR_Volume(_user_id, vol);          
           }
           catch (Exception ex)
           {
               WriteLog("设置音量失败:" + ex.Message);
               return false;
           }   
          
       }

       //转发音频数据
       public bool SendVoiceData(string fileName)
       {
           try
           {
               IntPtr pUser = new IntPtr(88);
               if (_voice_handle > 0) StopTalk();
               //if (_call_back == null) _call_back = new HCNetSDK.VoiceDataCallBack(OnVoiceData);
               _voice_handle = HCNetSDK.NET_DVR_StartVoiceCom_MR_V30(_user_id, VIOCE_CHANNEL, _call_back, pUser);
               if (!File.Exists(fileName)) return false;
               _wav_thread = new Thread(new ParameterizedThreadStart(SendWavFile));
               _wav_thread.Start(fileName);
               return true;
           }
           catch (Exception ex)
           {
               
              WriteLog("转发音频数据失败:" + ex.Message);
               return false;
           }
        
       }

       // 抓图
       public bool CapturePicture(uint channel, string fileName)
       {
           HCNetSDK.NET_DVR_JPEGPARA jpegPara = new HCNetSDK.NET_DVR_JPEGPARA();
           jpegPara.wPicQuality = 0;
           jpegPara.wPicSize = 0;
           return HCNetSDK.NET_DVR_CaptureJPEGPicture(_user_id, (int)channel, ref jpegPara, fileName);
       }

       //设置设备时间
       public bool ConfigTime(DateTime dt)
       {
           HCNetSDK.NET_DVR_TIME dvrTime = DateTimeToDvrTime(dt);          
           byte[] buf = HCNetSDK.StructToBytes(dvrTime);
           return HCNetSDK.NET_DVR_SetDVRConfig(_user_id, HCNetSDK.NET_DVR_SET_TIMECFG, 0, buf, (uint)buf.Length);           
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
           return HCNetSDK.NET_DVR_SetReconnect(interval, isReconnect);
       }

       // 云台控制
       public bool PTZControl(int channel, PtzCommand command, PtzStop stop, PtzSpeed speed)
       {
           return HCNetSDK.NET_DVR_PTZControlWithSpeed(_real_handles[channel - 1]
               , (uint)command
               , (uint)stop
               , (uint)speed); 
          
       }

       //2012-04-05  add by [bhb] reason : 增加回放功能
       // 查找录像文件  
       public bool FindFile(int channel, DateTime startTime, DateTime stopTime)
       {
           HCNetSDK.NET_DVR_FILECOND cond = new HCNetSDK.NET_DVR_FILECOND();
           cond.dwFileType = 0xFF;
           cond.lChannel = channel;
           cond.dwIsLocked = 0xFF;
           cond.dwUseCardNo = 0;
           cond.sCardNumber = "";
           cond.struStartTime = DateTimeToDvrTime(startTime);
           cond.struStopTime = DateTimeToDvrTime(stopTime);
           int findHandle = HCNetSDK.NET_DVR_FindFile_V30(_user_id, ref cond);
           if (findHandle < 0) return false;// msg = GetErrorMessage();
           _find_files.Clear();
           FindDvrFiles(findHandle);
           bool ret = HCNetSDK.NET_DVR_FindClose_V30(findHandle);
           return ret;
       }

       // 获取文件信息
       public void FindDvrFiles(int findHandle)
       {
           HCNetSDK.NET_DVR_FINDDATA_V30 fileData = new HCNetSDK.NET_DVR_FINDDATA_V30();
           
           fileData.struStartTime = new HCNetSDK.NET_DVR_TIME();
           fileData.struStopTime = new HCNetSDK.NET_DVR_TIME();
           fileData.byLocked = 0xFF;
           fileData.byRes = new byte[3];
           fileData.sFileName = "";//Encoding.ASCII.GetString(new byte[100]);
           fileData.dwFileSize = 0;
           fileData.sCardNum = ""; //Encoding.ASCII.GetString(new byte[32]);
           //fileData.
           int ret = -1;
           while (true)
           {
               ret = HCNetSDK.NET_DVR_FindNextFile_V30(findHandle, ref fileData);
               switch (ret)
               {
                   case HCNetSDK.NET_DVR_FILE_SUCCESS:
                       FindData data = new FindData();
                       data.StartTime = DvrTimeToDataTime(fileData.struStartTime);
                       data.StopTime = DvrTimeToDataTime(fileData.struStopTime);
                       data.FileName = fileData.sFileName;
                       data.FileSize = fileData.dwFileSize;
                       data.Locked   =  Convert.ToBoolean(fileData.byLocked);
                       _find_files.Add(data);
                       continue; 
                   case HCNetSDK.NET_DVR_ISFINDING:
                       Thread.Sleep(1000);
                       continue;
                   case HCNetSDK.NET_DVR_FILE_NOFIND:
                   case HCNetSDK.NET_DVR_NOMOREFILE:
                       break;
                   case HCNetSDK.NET_DVR_FILE_EXCEPTION:
                       break;
                   default:
                       break;
               }
               break;
           }
           FindClose();           
       }
       // 结束查找文件
       private bool FindClose()
       {
           bool ret = HCNetSDK.NET_DVR_FindClose_V30(_find_handle);
           _find_handle = -1;
           return ret;
       }

       /// <summary>
       /// 按时间回放
       /// </summary>
       /// <param name="channel">通道号</param>
       /// <param name="startTime">开始时间</param>
       /// <param name="stopTime">停止时间</param>
       /// <param name="hWnd">窗口句柄</param>
       /// <returns></returns>
       public bool PlayBackByTime(int channel, DateTime startTime, DateTime stopTime, IntPtr hWnd)
       {
           HCNetSDK.NET_DVR_TIME dvrStart = DateTimeToDvrTime(startTime);
           HCNetSDK.NET_DVR_TIME dvrStop = DateTimeToDvrTime(stopTime);
           _play_handle = HCNetSDK.NET_DVR_PlayBackByTime(_user_id, channel, ref dvrStart, ref dvrStop, hWnd);
           if(_play_handle == -1) return false;
           uint outValue = 0;
           return PlayBackControl(PlayControlCode.START, 0, out outValue);           
       }
       /// <summary>
       /// 按文件名回放录像文件
       /// </summary>
       /// <param name="fileName"> 回放的文件名，长度不能超过100字节</param>
       /// <param name="hWnd">窗口句柄</param>
       /// <returns></returns>
       public bool PlayBackByName(string fileName, IntPtr hWnd)
       {
           _play_handle = HCNetSDK.NET_DVR_PlayBackByName(_user_id, fileName, hWnd);
           if (_play_handle < 0) return false;
           return true;
       }

       // 停止回放
       public bool StopPlayBack()
       {
           return HCNetSDK.NET_DVR_StopPlayBack(_play_handle);
       }

       /// <summary>
       /// 控制录像回放的状态
       /// </summary>
       /// <param name="controlCode">播放控制</param>
       /// <param name="inValue">输入参数</param>
       /// <param name="outValue">输出参数</param>
       /// <returns></returns>
       /// 输入/输出参数与控制命令列表
       /// -----------------------------------------------------------------------------------------------------
       /// 状态命令         命令说明                                   inValue          outValue
       /// -----------------------------------------------------------------------------------------------------
       /// START            开始播放                                   整型的偏移量     无 
       /// SET_POS          改变回放的进度                             进度值（0-100）  无 
       /// GET_POS          获取回放的进度                             无               一个4字节整型的进度值（0-100） 
       /// GET_TIME         获取当前已播放的时间（按文件回放有效）     无               一个4字节整型值 
       /// GET_FRAME        获取当前已播放的帧数（按文件回放有效）     无               一个4字节整型值 
       /// GET_TOTAL_FRAMES 获取当前播放文件总的帧数（按文件回放有效） 无               一个4字节整型值 
       /// GET_TOTAL_TIME 获取当前播放文件总的时间（按文件回放有效）   无               一个4字节整型值 
       public bool PlayBackControl(PlayControlCode controlCode, uint inValue, out uint outValue)
       {
           bool ret = HCNetSDK.NET_DVR_PlayBackControl(_play_handle, (uint)controlCode, inValue, out outValue);
           return true;
       }
       //------------------------------增加摄像头调焦功能----------------------------
       public string GetErrorMessage()
       {
           _error_code = HCNetSDK.NET_DVR_GetLastError();
           switch (_error_code)
           {
               case 0: _error_message = "没有错误";
                   break;
               case 1:
                   _error_message = "用户名密码错误,注册时输入的用户名或者密码错误";
                   break;
               case 2:
                   _error_message = "权限不足";
                   break;
               case 3:
                   _error_message = "SDK未初始化";
                   break;
               case 4:
                   _error_message = "通道号错误";
                   break;
               case 5: 
                   _error_message = "连接到设备的用户个数超过最大"; 
                   break;
               case 6: _error_message = "版本不匹配"; break;
               case 7: _error_message = "连接设备失败"; break;
               case 8: _error_message = "向设备发送失败"; break;
               case 9: _error_message = "从设备接收数据失败"; break;
               case 10: _error_message = "从设备接收数据超时"; break;
               case 11: _error_message = "传送的数据有误"; break;
               case 12: _error_message = "调用次序错误"; break;
               case 13: _error_message = "无此权限"; break;
               case 14: _error_message = "设备命令执行超时"; break;
               case 15: _error_message = "串口号错误"; break;
               case 16: _error_message = "报警端口错误"; break;
               case 17: _error_message = "参数错误"; break;
               case 18: _error_message = "设备通道处于错误状态"; break;
               case 19: _error_message = "设备无硬盘"; break;
               case 20: _error_message = "硬盘号错误"; break;
               case 21: _error_message = "设备硬盘满"; break;
               case 22: _error_message = "设备硬盘出错"; break;
               case 23: _error_message = "设备不支持"; break;
               case 24: _error_message = "设备忙"; break;
               case 25: _error_message = "设备修改不成功"; break;
               case 26: _error_message = "密码输入格式不正确"; break;
               case 27: _error_message = "硬盘正在格式化，不能启动操作"; break;
               case 28: _error_message = "设备资源不足"; break;
               case 29: _error_message = "设备操作失败"; break;
               case 30: _error_message = "语音对讲、语音广播操作中采集本地音频或打开音频输出失败"; break;
               case 31: _error_message = "设备语音对讲被占用"; break;
               case 32: _error_message = "时间输入不正确"; break;
               case 33: _error_message = "回放时设备没有指定的文件"; break;
               case 34: _error_message = "创建文件出错"; break;
               case 35: _error_message = "打开文件出错"; break;
               case 36: _error_message = "上次的操作还没有完成"; break;
               case 37: _error_message = "获取当前播放的时间出错"; break;
               case 38: _error_message = "播放出错"; break;
               case 39: _error_message = "文件格式不正确"; break;
               case 40: _error_message = "路径错误"; break;
               case 41: _error_message = "SDK资源分配错误"; break;
               case 42: _error_message = "声卡模式错误,当前打开声音播放模式与实际设置的模式不符出错"; break;
               case 43: _error_message = "缓冲区太小"; break;
               case 44: _error_message = "创建SOCKET出错"; break;
               case 45: _error_message = "设置SOCKET出错"; break;
               case 46: _error_message = "个数达到最大"; break;
               case 47: _error_message = "用户不存在"; break;
               case 48: _error_message = "写FLASH出错,设备升级时写FLASH失败"; break;
               case 49: _error_message = "设备升级失败,网络或升级文件语言不匹配等原因升级失败"; break;
               case 50: _error_message = "解码卡已经初始化过"; break;
               case 51: _error_message = "调用播放库中某个函数失败"; break;
               case 52: _error_message = "登录设备的用户数达到最大"; break;
               case 53: _error_message = "获得本地PC的IP地址或物理地址失败"; break;
               case 54: _error_message = "设备该通道没有启动编码"; break;
               case 55: _error_message = "IP地址不匹配"; break;
               case 56: _error_message = "MAC地址不匹配"; break;
               case 57: _error_message = "升级文件语言不匹配"; break;
               case 58: _error_message = "播放器路数达到最大"; break;
               case 59: _error_message = "备份设备中没有足够空间进行备份"; break;
               case 60: _error_message = "没有找到指定的备份设备"; break;
               case 61: _error_message = "图像素位数不符，限24色"; break;
               case 62: _error_message = "图片高*宽超限，限128*256"; break;
               case 63: _error_message = "图片大小超限，限100K"; break;
               case 64: _error_message = "载入当前目录下Player Sdk出错"; break;
               case 65: _error_message = "找不到Player Sdk中某个函数入口"; break;
               case 66: _error_message = "载入当前目录下DSsdk出错"; break;
               case 67: _error_message = "找不到DsSdk中某个函数入口"; break;
               case 68: _error_message = "调用硬解码库DsSdk中某个函数失败"; break;
               case 69: _error_message = "声卡被独占"; break;
               case 70: _error_message = "加入多播组失败"; break;
               case 71: _error_message = "建立日志文件目录失败"; break;
               case 72: _error_message = "绑定套接字失败"; break;
               case 73: _error_message = "socket连接中断，此错误通常是由于连接中断或目的地不可达"; break;
               case 74: _error_message = "注销时用户ID正在进行某操作"; break;
               case 75: _error_message = "监听失败"; break;
               case 76: _error_message = "程序异常"; break;
               case 77: _error_message = "写文件失败,本地录像、远程下载录像、下载图片等操作时写文件失败"; break;
               case 78: _error_message = "禁止格式化只读硬盘"; break;
               case 79: _error_message = "远程用户配置结构中存在相同的用户名"; break;
               case 80: _error_message = "导入参数时设备型号不匹配"; break;
               case 81: _error_message = "导入参数时语言不匹配"; break;
               case 82: _error_message = "导入参数时软件版本不匹配"; break;
               case 83: _error_message = "预览时外接IP通道不在线"; break;
               case 84: _error_message = "加载标准协议通讯库StreamTransClient失败"; break;
               case 85: _error_message = "加载转封装库失败"; break;
               case 86: _error_message = "超出最大的IP接入通道数"; break;
               case 87: _error_message = "添加录像标签或者其他操作超出最多支持的个数"; break;
               case 88: _error_message = "图像增强仪，参数模式错误（用于硬件设置时，客户端进行软件设置时错误值）"; break;
               case 89: _error_message = "码分器不在线"; break;
               case 90: _error_message = "设备正在备份"; break;
               case 91: _error_message = "通道不支持该操作"; break;
               case 92: _error_message = "高度线位置太集中或长度线不够倾斜"; break;
               case 93: _error_message = "取消标定冲突，如果设置了规则及全局的实际大小尺寸过滤"; break;
               case 94: _error_message = "标定点超出范围"; break;
               case 95: _error_message = "尺寸过滤器不符合要求"; break;
               case 200: _error_message = "名称已存在"; break;
               case 201: _error_message = "阵列达到上限"; break;
               case 202: _error_message = "虚拟磁盘达到上限"; break;
               case 203: _error_message = "虚拟磁盘槽位已满"; break;
               case 204: _error_message = "重建阵列所需物理磁盘状态错误"; break;
               case 205: _error_message = "重建阵列所需物理磁盘为指定热备"; break;
               case 206: _error_message = "重建阵列所需物理磁盘非空闲"; break;
               case 207: _error_message = "不能从当前的阵列类型迁移到新的阵列类型"; break;
               case 208: _error_message = "迁移操作已暂停"; break;
               case 209: _error_message = "正在执行的迁移操作已取消"; break;
               case 210: _error_message = "阵列上存在虚拟磁盘，无法删除阵列"; break;
               case 211: _error_message = "对象物理磁盘为虚拟磁盘组成部分且工作正常"; break;
               case 212: _error_message = "指定的物理磁盘被分配为虚拟磁盘"; break;
               case 213: _error_message = "物理磁盘数量与指定的RAID等级不匹配"; break;
               case 214: _error_message = "阵列正常，无法重建"; break;
               case 215: _error_message = "存在正在执行的后台任务"; break;
               case 216: _error_message = "无法用ATAPI盘创建虚拟磁盘"; break;
               case 217: _error_message = "阵列无需迁移"; break;
               case 218: _error_message = "物理磁盘不属于同意类型"; break;
               case 219: _error_message = "无虚拟磁盘，无法进行此项操作"; break;
               case 220: _error_message = "磁盘空间过小，无法被指定为热备盘"; break;
               case 221: _error_message = "磁盘已被分配为某阵列热备盘"; break;
               case 222: _error_message = "阵列缺少盘"; break;
               case 300: _error_message = "配置ID不合理"; break;
               case 301: _error_message = "多边形不符合要求"; break;
               case 302: _error_message = "规则参数不合理"; break;
               case 303: _error_message = "配置信息冲突"; break;
               case 304: _error_message = "当前没有标定信息"; break;
               case 305: _error_message = "摄像机参数不合理"; break;
               case 306: _error_message = "长度不够倾斜，不利于标定"; break;
               case 307: _error_message = "标定出错，以为所有点共线或者位置太集中"; break;
               case 308: _error_message = "摄像机标定参数值计算失败"; break;
               case 309: _error_message = "输入的样本标定线超出了样本外接矩形框"; break;
               case 310: _error_message = "没有设置进入区域"; break;
               case 311: _error_message = "交通事件规则中没有包括车道"; break;
               case 312: _error_message = "当前没有设置车道"; break;
               case 313: _error_message = "事件规则中包含2种不同方向"; break;
               case 314: _error_message = "车道和数据规则冲突"; break;
               case 315: _error_message = "不支持的事件类型"; break;
               case 316: _error_message = "车道没有方向 "; break;
               case 317: _error_message = "尺寸过滤框不合理 "; break;
               case 407: _error_message = "获取RTSP端口错误"; break;
               case 411: _error_message = "RTSP DECRIBE发送超时"; break;
               case 412: _error_message = "RTSP DECRIBE发送失败"; break;
               case 413: _error_message = "RTSP DECRIBE接收超时"; break;
               case 414: _error_message = "RTSP DECRIBE接收数据错误"; break;
               case 415: _error_message = "RTSP DECRIBE接收失败"; break;
               case 416: _error_message = "RTSP DECRIBE服务器返回401,501等错误"; break;
               case 421: _error_message = "RTSP SETUP发送超时"; break;
               case 422: _error_message = "RTSP SETUP发送错误"; break;
               case 423: _error_message = "RTSP SETUP接收超时"; break;
               case 424: _error_message = "RTSP SETUP接收数据错误"; break;
               case 425: _error_message = "RTSP SETUP接收失败"; break;
               case 426: _error_message = "设备超过最大连接数"; break;
               case 431: _error_message = "RTSP PLAY发送超时"; break;
               case 432: _error_message = "RTSP PLAY发送错误"; break;
               case 433: _error_message = "RTSP PLAT接收超时"; break;
               case 434: _error_message = "RTSP PLAY接收数据错误"; break;
               case 435: _error_message = "RTSP PLAY接收失败"; break;
               case 436: _error_message = "RTSP PLAY设备返回错误状态"; break;
               case 441: _error_message = "RTSP TEARDOWN发送超时"; break;
               case 442: _error_message = "RTSP TEARDOWN发送错误"; break;
               case 443: _error_message = "RTSP TEARDOWN接收超时"; break;
               case 444: _error_message = "RTSP TEARDOWN接收数据错误"; break;
               case 445: _error_message = "RTSP TEARDOWN接收失败"; break;
               case 446: _error_message = "RTSP TEARDOWN设备返回错误状态"; break;
               case 500: _error_message = "没有错误"; break;
               case 501: _error_message = "输入参数非法"; break;
               case 502: _error_message = "调用顺序不对"; break;
               case 503: _error_message = "多媒体时钟设置失败"; break;
               case 504: _error_message = "视频解码失败"; break;
               case 505: _error_message = "音频解码失败"; break;
               case 506: _error_message = "分配内存失败"; break;
               case 507: _error_message = "文件操作失败"; break;
               case 508: _error_message = "创建线程事件等失败"; break;
               case 509: _error_message = "创建directDraw失败"; break;
               case 510: _error_message = "创建后端缓存失败"; break;
               case 511: _error_message = "缓冲区满，输入流失败"; break;
               case 512: _error_message = "创建音频设备失败"; break;
               case 513: _error_message = "设置音量失败"; break;
               case 514: _error_message = "只能在播放文件时才能使用此接口"; break;
               case 515: _error_message = "只能在播放流时才能使用此接口"; break;
               case 516: _error_message = "系统不支持，解码器只能工作在Pentium 3以上"; break;
               case 517: _error_message = "没有文件头"; break;
               case 518: _error_message = "解码器和编码器版本不对应"; break;
               case 519: _error_message = "初始化解码器失败"; break;
               case 520: _error_message = "文件太短或码流无法识别"; break;
               case 521: _error_message = "初始化多媒体时钟失败"; break;
               case 522: _error_message = "位拷贝失败"; break;
               case 523: _error_message = "显示overlay失败"; break;
               case 524: _error_message = "打开混合流文件失败"; break;
               case 525: _error_message = "打开视频流文件失败"; break;
               case 526: _error_message = "JPEG压缩错误"; break;
                   break;
           }
           return _error_message;
       }
       #endregion
    }
}
