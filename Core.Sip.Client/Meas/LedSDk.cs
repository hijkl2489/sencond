using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Core.Sip.Client.Meas
{
   public class LedSdk
    {
        //通讯方式常量
        public const int DEVICE_TYPE_COM = 0;
        public const int DEVICE_TYPE_UDP = 1;
        public const int DEVICE_TYPE_MODEM = 2;

        //串行通讯速度常量
        public const int SBR_9600 = 0;
        public const int SBR_14400 = 1;
        public const int SBR_19200 = 2;
        public const int SBR_38400 = 3;
        public const int SBR_57600 = 4;
        public const int SBR_115200 = 5;

       //播放类型常量
        public const int ROOT_PLAY = 17;
        public const int ROOT_DOWNLOAD = 18;

       //显示屏类型常量
        public const int SCREEN_UNICOLOR = 1;
        public const int SCREEN_COLOR = 2;
        public const int SCREEN_FULLCOLOR = 3;
        public const int SCREEN_GRAY = 4;

        //响应消息常量
        public const int LM_RX_COMPLETE = 1;
        public const int LM_TX_COMPLETE = 2;
        public const int LM_RESPOND = 3;
        public const int LM_TIMEOUT = 4;
        public const int LM_NOTIFY = 5;
        public const int LM_PARAM = 6;
        public const int LM_TX_PROGRESS = 7;
        public const int LM_RX_PROGRESS = 8;

        //电源状态常量
        public const int LED_POWER_OFF = 0;
        public const int LED_POWER_ON = 1;

        //时间格式常量
        public const int DF_YMD = 1;                 //年月日  "2004年12月31日"
        public const int DF_HN = 2;                  //时分    "19:20"
        public const int DF_HNS = 3;                //时分秒  "19:20:30"
        public const int DF_Y = 4;                   //年      "2004"
        public const int DF_M = 5;                   //月      "12" "01" 注意：始终显示两位数字
        public const int DF_D = 6;                   //日
        public const int DF_H = 7;                   //时
        public const int DF_N = 8;                   //分
        public const int DF_S = 9;                   //秒
        public const int DF_W = 10;                  //星期    "星期三"

        //正计时、倒计时format参数
        public const int CF_DAY = 0;                 //天数
        public const int CF_HOUR = 1;                //小时数
        public const int CF_HMS = 2;                 //时分秒
        public const int CF_HM = 3;                  //时分
        public const int CF_MS = 4;                  //分秒
        public const int CF_S = 5;                   //秒

        public const int FONT_SET_16 = 0;            //16点阵字符
        public const int FONT_SET_24 = 1;            //24点阵字符

        public const int PKC_QUERY = 4;
        public const int PKC_ADJUST_TIME = 6;
        public const int PKC_GET_POWER = 9;
        public const int PKC_SET_POWER = 10;
        public const int PKC_GET_BRIGHT = 11;
       public const int PKC_SET_BRIGHT = 12;

        public const int GWL_WNDPROC = -4;
        public const int WM_USER = 1024;
        public const int WM_LED_NOTIFY = WM_USER + 1;

       public delegate IntPtr AddressOf(IntPtr hWnd, int Message, int wParam, int lParam);
        
       public struct PDeviceParam
        {
            public int devType;
            public int speed;
            public uint locPort;
            public uint rmtPort;
            public int[] reserved;
            public int ComPort;
            public int FlowCon;
        }
       public struct TNotifyMessage
       {
          public long Message;
          public long Command;
          public long Result;
          public long Status;
          public long Address;
          public long Size;
          public long gBuffer;
          public PDeviceParam param;
          public byte[] Host;
          public long Port;
        }

        public struct Rectangle
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

       public struct SYSTEMTIME
       {
           public int wYear;
           public int wMonth;
           public int wDayOfWeek;
           public int wDay;
           public int wHour;
           public int wMinute;
           public int wSecond;
           public int wMilliseconds;
       }

       public struct TTimeStamp
       {
           public long date;
           public long time;
       }
    
       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int LED_Open(ref PDeviceParam param, int Notify, int Window, int Message);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern void LED_Close(int dev);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern long LED_Query(ref IntPtr dev,ref long addr,ref long host,ref long port);
       
       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern void LED_SetPower(int dev, byte CardAddress, [MarshalAs(UnmanagedType.LPStr)]  String Host, uint port, uint Power);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern void LED_SendToScreen(int dev, byte CardAddress, [MarshalAs(UnmanagedType.LPStr)] string Host, uint port);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern long LED_GetNotifyMessage(TNotifyMessage Notify);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int MakeRoot(int RootType, int ScreenType);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddLeaf(uint DisplayTime);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddWindow(int dc, int width, int height,ref Rectangle rect, int method, int speed, int transparent);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern void LED_GetPower(int dev, byte CardAddress, byte ScrNo, [MarshalAs(UnmanagedType.LPStr)] string Host, uint port);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern void LED_SetBrightness(int dev, byte CardAddress, byte ScrNo, [MarshalAs(UnmanagedType.LPStr)] string Host, uint port, byte Brightness);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern void LED_SetIPAddress(int dev, byte CardAddress, byte ScrNo, [MarshalAs(UnmanagedType.LPStr)]  string Host, uint port, [MarshalAs(UnmanagedType.LPStr)] string NewIP);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int LED_DownLoadFontSet(int dev, byte CardAddress, byte ScrNo, [MarshalAs(UnmanagedType.LPStr)]  string Host, uint port, [MarshalAs(UnmanagedType.LPStr)] string filename);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddDateTime(ref Rectangle rect, int transparent, [MarshalAs(UnmanagedType.LPStr)]  string fontname, int fontsize, int fontcolor, int format);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddString([MarshalAs(UnmanagedType.LPStr)] string str,ref Rectangle rect, int method, int speed, int transparent, int fontset, int fontcolor);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddText([MarshalAs(UnmanagedType.LPStr)] string str, ref Rectangle rect, int method, int speed, int transparent, [MarshalAs(UnmanagedType.LPStr)] string fontname, int fontsize, int fontcolor);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddMovie([MarshalAs(UnmanagedType.LPStr)] string filename,ref Rectangle rect, int stretch);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddCountUp(ref Rectangle rect, int transparent, [MarshalAs(UnmanagedType.LPStr)] string fontname, int fontsize, int fontcolor, int format, SYSTEMTIME starttime);

       [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
       public static extern int AddCountDown(ref Rectangle rect, int transparent, [MarshalAs(UnmanagedType.LPStr)] string fontname, int fontsize, int fontcolor, int format, SYSTEMTIME endtime);

       [DllImport("user32.dll", CharSet = CharSet.Auto)]
       public static extern int SetRect(ref Rectangle ARect, int left, int top, int right, int bottom);

       [DllImport("user32.dll", CharSet = CharSet.Auto)]
       public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, int wParam, int lParam);

       [DllImport("user32.dll")]
       public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, AddressOf dwValue);

       [DllImport("user32.dll", CharSet = CharSet.Auto)]
       public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


       [DllImport("user32.dll")]
       public static extern long PostMessage(ref long hWnd, ref long Message, ref long wParam, ref long lParam);

    }
}
