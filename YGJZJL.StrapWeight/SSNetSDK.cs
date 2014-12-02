using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace YGJZJL.StrapWeight
{
    public delegate void CallBackMessage(uint dwType, uint nDeviceHandle, int nChannel, int nError, int nParam1, int nParam2);
    public delegate void CallBackDetect([MarshalAs(UnmanagedType.BStr)]string szVendorName, string szModelName, string szDeviceName, string szIP, [MarshalAs(UnmanagedType.BStr)]string szSubnetMask, [MarshalAs(UnmanagedType.BStr)]string szGateway, [MarshalAs(UnmanagedType.BStr)]string szMac, int nPort, int nHttpPort, bool bSupportIpInstall);
    public delegate void CallBackStream(IntPtr pContext, uint nDeviceHandle, int lChannel, int nMediaType, IntPtr pData, int nInputType, int nFrameType, int nFrameTime, int nFrameSize, uint nTimeStamp, int lFrameWidth, int lFrameHeight, int lFrameRate);
    [UnmanagedFunctionPointerAttribute(CallingConvention.StdCall)]
    public delegate void CallBackCapture(IntPtr pContext, ref byte pData, int nDataSize);

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public  struct LPXNS_DEV_DEVICEINFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public byte[] sVersion;// = new char[128];
        public byte byAlarmInPortNum;
        public byte byAlarmOutPortNum;
        public byte byDvr;
        public byte byNetCam;
        public byte byChanNum;
        public byte byEncoderNum;
        public byte byIPChanNum;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public byte[] byRes1;// = new char[24];
    }
    public struct LPXNS_DEV_WORKSTATE
    {
        public uint dwDeviceStatus; 	//设备的状态,0-未连接,1-已连接,-1-没有初始化
        [MarshalAs(UnmanagedType.LPArray)]
        public XNS_DEV_CHANNELSTATE[] struChanStatus;//通道的状态
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] byAlarmOutStatus;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] byAlarmInStatus;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] byRes;
    }
    public struct XNS_DEV_CHANNELSTATE
    {
        public byte bySignalStatus;	//连接的信号状态,0-信号丢失,1-正常
        public byte byRecordStatus;	//远端通道是否在录像,0-不录像,1-录像
        public byte byLocalRecordStatus; //本地通道是否在录像,0-不录像,1-录像
        public byte byListenStatus;	//通道监听状态,0-没有打开,1-打开
        public byte byTalkStatus;		//通道对讲状态,0-没有打开,1-打开
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] byRes;
    }
    public class SSNetSDK
    {
        /// <summary>
        /// 获得SDK支持的设备型号数量
        /// </summary>
        /// <returns>设备型号数量</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern uint XNS_DEV_GetModelCount();
        /// <summary>
        /// 释放查询过程分配内存空间
        /// </summary>
        /// <returns>恒为TRUE</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_ReleaseModel();
        /// <summary>
        /// 设备自动发现，发现结果通过CallBackDetect回调函数返回
        /// </summary>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_AutoDetect();
        /// <summary>
        /// 返回最后操作的错误码
        /// </summary>
        /// <returns>错误码</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern uint XNS_DEV_GetLastError();
        /// <summary>
        /// 初始化SDK，调用其他SDK函数的前提
        /// </summary>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_Init();
        /// <summary>
        /// 释放SDK资源，在结束之前最后调用
        /// </summary>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_Cleanup();
        /// <summary>
        /// 设置消息回调，搜索回调
        /// </summary>
        /// <param name="cbMessage"></param>
        /// <param name="cbDetect"></param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_SetMessCallBack([MarshalAs(UnmanagedType.FunctionPtr)]CallBackMessage cbMessage, [MarshalAs(UnmanagedType.FunctionPtr)]CallBackDetect cbDetect);
        /// <summary>
        /// 用户注册设备
        /// </summary>
        /// <param name="sDEVIP">设备IP地址</param>
        /// <param name="wDEVPort">设备端口号</param>
        /// <param name="sUserName">登录的用户名</param>
        /// <param name="sPassword">用户密码</param>
        /// <param name="sModelName">设备型号名称</param>
        /// <param name="pContext">用户数据</param>
        /// <param name="lpDeviceInfo">设备信息,可以为NULL</param>
        /// <param name="bBlock">是否采用阻塞连接模式</param>
        /// <param name="wHttpPort">http端口</param>
        /// <returns>0表示失败 其他值表示返回的设备句柄值</returns>        
        [DllImport("SSNetSDK.DLL")]
        public static extern uint XNS_DEV_Login(string sDEVIP, short wDEVPort, string sUserName, string sPassword, string sModelName, ref string pContext, IntPtr lpDeviceInfo, bool bBlock, ushort wHttpPort);


        /// <summary>
        /// 用户注销
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_Logout(uint ulDeviceHandle);
        /// <summary>
        /// 获得用户数据，此数据从XNS_DEV_Login（）传入
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <returns>用户数据</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern object XNS_DEV_GetContextFromDeviceHandle(uint ulDeviceHandle);
        /// <summary>
        /// 获取设备的通道等信息
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <param name="lpDeviceInfo"></param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_GetDevInfo(uint ulDeviceHandle, LPXNS_DEV_DEVICEINFO lpDeviceInfo);
        /// <summary>
        /// 获取设备的当前状态
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <param name="lpDeviceState"></param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_GetDevState(uint ulDeviceHandle, LPXNS_DEV_WORKSTATE lpDeviceState);
        /// <summary>
        /// 实时预览
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <param name="lChannel">通道号,从1开始</param>
        /// <param name="hPlayWnd"></param>
        /// <returns>0表示失败 其他值表示返回的预览句柄</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern uint XNS_DEV_StartRealPlay(uint ulDeviceHandle, int lChannel, IntPtr hPlayWnd);
        /// <summary>
        /// 停止预览
        /// </summary>
        /// <param name="ulRealHandle">预览句柄</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_StopRealPlay(uint ulRealHandle);
        /// <summary>
        /// 注册回调函数，捕获实时码流数据
        /// </summary>
        /// <param name="ulRealHandle">预览句柄</param>
        /// <param name="cbRealDataCallBack">标准码流回调函数,当不为空时为启动回调，为空时为停止回调</param>
        /// <param name="pUser">用户数据</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_SetRealDataCallBack(uint ulRealHandle, [MarshalAs(UnmanagedType.FunctionPtr)]CallBackStream cbRealDataCallBack, [MarshalAs(UnmanagedType.IUnknown)]object pUser);
        /// <summary>
        /// 启动流播放
        /// </summary>
        /// <param name="hPlayWnd">播放窗口句柄</param>
        /// <returns>0表示失败 其他值表示返回的流句柄</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern uint XNS_DEV_StartStreamPlay(IntPtr hPlayWnd);
        /// <summary>
        /// 停止流播放
        /// </summary>
        /// <param name="ulStreamHandle">流句柄</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_StopStreamPlay(uint ulStreamHandle);
        /// <summary>
        /// 传入流数据
        /// </summary>
        /// <param name="ulStreamHandle">流句柄</param>
        /// <param name="lMediaData">流数据</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_InputData(uint ulStreamHandle, int lMediaData);
        /// <summary>
        /// 开启声音，支持全部与视频相关的功能模块
        /// </summary>
        /// <param name="ulRealHandle">预览句柄</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_OpenSound(uint ulRealHandle);
        /// <summary>
        /// 关闭声音
        /// </summary>
        /// <param name="ulRealHandle">预览句柄</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_CloseSound(uint ulRealHandle);
        /// <summary>
        /// 调节播放音量
        /// </summary>
        /// <param name="ulRealHandle">预览句柄</param>
        /// <param name="wVolume">音量，取值范围[0,100]</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_SetVolume(uint ulRealHandle, int wVolume);
        /// <summary>
        /// 开始音频对讲
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <param name="lChannel">对讲通道</param>
        /// <param name="blSendData">是否使用XNS_DEV_SendAudioData发送音频数据</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_StartTalk(uint ulDeviceHandle, int lChannel, bool blSendData);
        /// <summary>
        /// 停止音频对讲
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <param name="lChannel">对讲通道</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_StopTalk(uint ulDeviceHandle, int lChannel);
        /// <summary>
        /// 发送音频数据
        /// </summary>
        /// <param name="ulDeviceHandle">设备句柄</param>
        /// <param name="lData">数据指针，byte*类型</param>
        /// <param name="lDataSize">数据长度</param>
        /// <returns></returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_SendAudioData(uint ulDeviceHandle, int lData, int lDataSize);
        /// <summary>
        /// 单帧数据捕获并保存成图片
        /// </summary>
        /// <param name="lRealHandle">预览句柄</param>
        /// <param name="szFile">文件名称</param>
        /// <param name="nFormat">图片格式</param>
        /// <returns>TRUE表示成功 FALSE表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_SaveSnapshot(uint lRealHandle, string szFile, int nFormat);
        /// <summary>
        /// 在不预览视频的情况下，进行前端抓拍 
        /// </summary>
        /// <param name="ulDeviceHandle"> 设备句柄 </param>
        /// <param name="lChannel">抓拍通道</param>
        /// <param name="sPicFileName"> 抓拍图片保存路径，可以为 NULL</param>
        /// <param name="cbCaptureCallback"> 数据回调函数指针</param>
        /// <param name="pContext">用户自定义数据，此数据将通过回调函数返回 </param>
        /// <returns>true 表示成功 false 表示失败</returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_RemoteCaptureJPEGPic(uint ulDeviceHandle, int lChannel, string sPicFileName, CallBackCapture cbCaptureCallback, IntPtr pContext);
        /// <summary>
        /// 云台控制 
        /// </summary>
        /// <param name="ulRealHandle"> 预览句柄 </param>
        /// <param name="dwPTZCommand">云台控制命令</param>
        /// <param name="dwStop">云台开始动作或停止动作：0－开始；1－停止 </param>
        /// <param name="lSpeed">云台控制的速度（1~100）</param>
        /// <returns></returns>
        [DllImport("SSNetSDK.DLL")]
        public static extern bool XNS_DEV_PTZControlWithSpeed(uint ulRealHandle, uint dwPTZCommand, uint dwStop, int lSpeed);

    }
}
