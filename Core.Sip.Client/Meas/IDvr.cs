using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Sip.Client.Meas
{
    public enum PtzCommand
    {
        LIGHT_PWRON = 2, //接通灯光电源 
        WIPER_PWRON = 3, //接通雨刷开关 
        FAN_PWRON = 4,   //接通风扇开关 
        HEATER_PWRON = 5, //接通加热器开关 
        AUX_PWRON1 = 6, //接通辅助设备开关 
        AUX_PWRON2 = 7, //接通辅助设备开关 
        ZOOM_IN = 11,   //焦距变大(倍率变大) 
        ZOOM_OUT = 12,  //焦距变小(倍率变小) 
        FOCUS_NEAR = 13, //焦点前调 
        FOCUS_FAR = 14, //焦点后调 
        IRIS_OPEN = 15, //光圈扩大 
        IRIS_CLOSE = 16, //光圈缩小 
        TILT_UP = 21, //云台上仰 
        TILT_DOWN = 22, //云台下俯 
        PAN_LEFT = 23, //云台左转 
        PAN_RIGHT = 24, //云台右转 
        UP_LEFT = 25, //云台上仰和左转 
        UP_RIGHT = 26, //云台上仰和右转 
        DOWN_LEFT = 27, //云台下俯和左转 
        DOWN_RIGHT = 28, //云台下俯和右转 
        PAN_AUTO = 29    //云台左右自动扫描 
    }
    public enum PlayControlCode
    {
        START = 1,   //	开始播放
        PAUSE = 3,   //	暂停播放
        RESTART = 4, //	恢复播放
        FAST = 5,   //	快放
        SLOW = 6,   //	慢放
        NORMAL = 7, //	正常速度播放（在暂停后调用将恢复暂停前的速度播放）
        FRAME = 8, //	单帧放（恢复正常回放使用NORMAL命令）
        START_AUDIO = 9, //	打开声音
        STOP_AUDIO = 10, //	关闭声音
        AUDIO_VOLUME = 11, //	调节音量，取值范围[0,0xffff]
        SET_POS = 12,   //	改变文件回放的进度
        GET_POS = 13,  //	获取文件回放的进度
        GET_TIME = 14, //	获取当前已经播放的时间(按文件回放的时候有效)
        GET_FRAME = 15, //	获取当前已经播放的帧数(按文件回放的时候有效)
        GET_TOTAL_FRAMES = 16, //	获取当前播放文件总的帧数(按文件回放的时候有效)
        GET_TOTAL_TIME = 17,  //	获取当前播放文件总的时间(按文件回放的时候有效)
        THROW_BFRAME = 20   //	丢B帧
    }
    public enum PtzSpeed
    {
        LEVEL1=  1,
        LEVEL2 = 2,
        LEVEL3 = 3,
        LEVEL4 = 4,
        LEVEL5 = 5,
        LEVEL6 = 6,
        LEVEL7 = 7,

    }

    public enum PtzStop
    {
        START = 0,
        STOP = 1
    }
    // DVR系统接口类
    interface IDvr 
    {
        
        // 登录DVR系统
        bool Login();
        bool Logout();

        //视频控制
        bool RealPlay(int channel, IntPtr hPlayWnd);
        bool StopRealPlay(int channel);

        // 采集声音
        bool OpenSound();
        bool CloseSound();    
        /// <summary>
        /// 功能：调节播放音量
        /// </summary>
        /// <param name="vol">音量</param>
        /// <returns>返回值：  true 表示成功; false 表示失败</returns>
        bool SetVolume(ushort vol);
        
        // 语言控制
        bool StartTalk();
        bool StopTalk();

        // 转发语音数据
        bool SendVoiceData(string fileName);

        // 抓图
        bool CapturePicture(uint channel, string fileName);

        //设置设备时间
        bool ConfigTime(DateTime dateTime);

        // 设置网络
        bool SetConnectTime(uint waitTime, uint tryTimes);
        bool SetReconnect(uint interval, bool isReconnect);

        // 云台控制
        bool PTZControl(int channel, uint command, uint stop, int speed);
        
        //-----------------需添加的接口-----------------
        // 视频回放
    }
}
