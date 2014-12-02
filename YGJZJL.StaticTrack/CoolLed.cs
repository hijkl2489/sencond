using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace YGJZJL.StaticTrack
{
    /// <summary>
    /// LED屏操作类，材秤用
    /// </summary>
    public class CoolLed
    {
        #region api list

        //通讯方式常量
        public const int DEVICE_TYPE_COM = 0;
        public const int DEVICE_TYPE_UDP = 1;
        public const int DEVICE_TYPE_MODEM = 2;

        //播放类型常量
        public const int ROOT_PLAY = 17;
        public const int ROOT_DOWNLOAD = 18;

        //电源状态常量
        public const int LED_POWER_OFF = 0;
        public const int LED_POWER_ON = 1;

        public const int SCREEN_COLOR = 2;

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

        public struct Rectangle
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
        public static extern int LED_Open(ref PDeviceParam param, int Notify, int Window, int Message);

        [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
        public static extern void LED_Close(int dev);

        [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
        public static extern void LED_SetPower(int dev, byte CardAddress, [MarshalAs(UnmanagedType.LPStr)]  String Host, uint port, uint Power);

        [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
        public static extern void LED_SendToScreen(int dev, byte CardAddress, [MarshalAs(UnmanagedType.LPStr)] string Host, uint port);

        [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
        public static extern int MakeRoot(int RootType, int ScreenType);

        [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
        public static extern int AddLeaf(uint DisplayTime);

        [DllImport("LEDSender.dll", CharSet = CharSet.Auto)]
        public static extern int AddText([MarshalAs(UnmanagedType.LPStr)] string str, ref Rectangle rect, int method, int speed, int transparent, [MarshalAs(UnmanagedType.LPStr)] string fontname, int fontsize, int fontcolor);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetRect(ref Rectangle ARect, int left, int top, int right, int bottom);

        #endregion

        #region private member variable

        private int m_hDeviceHandle;
        private string m_szServerIP;
        private int m_nAddress;
        private int m_nServerPort;
        private int m_nLocalPort;

        private string m_szDeviceType;//设备类型，不同设备的不同处理方法通过次变量进行处理
        private string m_szDeviceName;//设备名称，比如汽车衡1#，主要用于写日志
        private string m_szStateInfo;//设备状态
        private bool m_bOpened;

        #endregion

        #region constructor

        public CoolLed()
        {
            m_hDeviceHandle = -1;
            m_szServerIP = "";
            m_nAddress = -1;
            m_nServerPort = -1;
            m_nLocalPort = -1;

            m_szDeviceType = "";
            m_szDeviceName = "";
            m_szStateInfo = "close";
            m_bOpened = false;
        }

        public CoolLed(string strParam)
        {
            string[] strtmp = strParam.Split(new char[] { ',' });
            m_szServerIP = strtmp[0];
            m_nAddress = Convert.ToInt32(strtmp[1]);
            m_nServerPort = Convert.ToUInt16(strtmp[2]);
            m_nLocalPort = Convert.ToInt32(strtmp[3]);
        }

        #endregion

        #region methods

        /// <summary>
        /// 建立和Led的连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            PDeviceParam param = new PDeviceParam();
            param.FlowCon = 0;
            param.devType = 1;
            param.rmtPort = Convert.ToUInt16(m_nServerPort);
            param.locPort = Convert.ToUInt16(m_nLocalPort);

            m_hDeviceHandle = LED_Open(ref param, 0, 0, 0);
            if (m_hDeviceHandle == -1)
            {
                m_bOpened = false;
                m_szStateInfo = "close";
                return false;
            }
            else
            {
                m_bOpened = true;
                m_szStateInfo = "close";
                return true;
            }
        }

        /// <summary>
        /// 发送数据到Led
        /// </summary>
        /// <param name="strText">要发送的字符串，用\n分行以显示多行</param>
        /// <param name="strFontName">字体名称，如宋体</param>
        /// <param name="nFontSize">字体大小，显示4行数据建议字体大小为10，显示5行数据建议字体大小为9，其余大小不太适合本案</param>
        /// <param name="nMethod">显示方式，建议为1。1.立即显示;2.左滚显示;3.连续上滚;4.中间向上下展开;5.中间向两边展开;6.中间向四周展开;7.向左移入;8.向右移入;9.从左向右展开;10.从右向左展开;11.右上角移入;12.右下角移入;13.左上角移入;14.左下角移入;15.从上向下移入;16.从下向上移入;17.闪  烁;</param>
        /// <param name="nSpeed">显示速度（1-8），越大越快，建议为1</param>
        /// <param name="nTransparent">是否透明。0=不透明 1=透明</param>
        public void SendText(string strText, string strFontName, int nFontSize, int nMethod, int nSpeed, int nTransparent)
        {
            MakeRoot(ROOT_PLAY, SCREEN_COLOR);//创建一个发送序列,以前的将被清除
            AddLeaf(100000000);//增加一个页面
            Rectangle rect = new Rectangle();
            int step = nFontSize + 3;
            int i = 0;
            string[] strToSend = strText.Split(new char[] { '\n' });

            for (i = 0; i < strToSend.Length; i++)
            {
                SetRect(ref rect, 0, step * i, 256, step * (i + 1));
                AddText(strToSend[i],
                        ref rect,
                        nMethod,
                        nSpeed,
                        nTransparent,
                        strFontName,
                        nFontSize,
                        255
                        );
            }


            LED_SendToScreen(m_hDeviceHandle,
                            (byte)m_nAddress,
                            m_szServerIP,
                            Convert.ToUInt16(m_nServerPort)
                            );

        }

        /// <summary>
        /// 打开或关闭Led电源
        /// </summary>
        /// <param name="nPowerOn">电源状态。0=关闭，1=打开</param>
        public void SetPower(int nPowerOn)
        {
            LED_SetPower(m_hDeviceHandle,
                        (byte)m_nAddress,
                        m_szServerIP,
                        Convert.ToUInt16(m_nServerPort),
                        Convert.ToUInt16(nPowerOn)
                        );
        }

        /// <summary>
        /// 关闭连接，停止使用
        /// </summary>
        public void Close()
        {
            if (m_hDeviceHandle >= 0)
            {
                m_bOpened = false;
                m_szStateInfo = "close";
                LED_Close(m_hDeviceHandle);
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType
        {
            get
            {
                return m_szDeviceType;
            }
            set
            {
                m_szDeviceType = value;
            }
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName
        {
            get
            {
                return m_szDeviceName;
            }
            set
            {
                m_szDeviceName = value;
            }
        }

        /// <summary>
        /// 状态信息
        /// </summary>
        public string StateInfo
        {
            get
            {
                return m_szStateInfo;
            }
            set
            {
                m_szStateInfo = value;
            }
        }

        /// <summary>
        /// 是否打开，只读
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return m_bOpened;
            }
        }

        /// <summary>
        /// Led IP地址
        /// </summary>
        public string ServerIP
        {
            get
            {
                return m_szServerIP;
            }
            set
            {
                m_szServerIP = value;
            }
        }

        /// <summary>
        /// Led 地址
        /// </summary>
        public int Address
        {
            get
            {
                return m_nAddress;
            }
            set
            {
                m_nAddress = value;
            }
        }

        /// <summary>
        /// Led 通讯端口
        /// </summary>
        public int ServerPort
        {
            get
            {
                return m_nServerPort;
            }
            set
            {
                m_nServerPort = value;
            }
        }

        /// <summary>
        /// 本地通讯端口
        /// </summary>
        public int LocalPort
        {
            get
            {
                return m_nLocalPort;
            }
            set
            {
                m_nLocalPort = value;
            }
        }

        #endregion
    }
}
