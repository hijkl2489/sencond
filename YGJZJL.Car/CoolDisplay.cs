using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Drawing;

namespace YGJZJL.Car
{
    /// <summary>
    /// 液晶显示屏类，用于操作液晶屏，采用串口通讯
    /// </summary>
    class CoolDisplay
    {

        #region private member variable

        private SerialPort m_SerialPort;
        private string m_szDeviceType;//设备类型，不同设备的不同处理方法通过次变量进行处理
        private string m_szDeviceName;//设备名称，比如汽车衡1#，主要用于写日志
        private string m_szStateInfo;//设备状态(打开或者关闭)
        private string m_szState;    //设备状态（空闲、使用）
        private bool m_bOpened;


        //串口参数
        private string m_szPortName;
        private int m_nBaudRate;
        private Parity m_Parity;
        private int m_nDataBits;
        private StopBits m_StopBits;

        #endregion

        #region constructor

        public CoolDisplay()
        {
            m_SerialPort = null;
            m_szDeviceType = "";
            m_szDeviceName = "";
            m_szStateInfo = "close";
            m_szState = "close";
            m_bOpened = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="szSerialParams">串口参数，必须是以下格式：串口名,波特率,校验位,数据位,停止位
        ///                                         如：COM9,9600,N,8,1或COM2,115200,E,7,1.5</param>
        public CoolDisplay(string szSerialParams)
        {
            string[] strtmp = szSerialParams.Split(new char[] { ',' });
            PortName = strtmp[0];
            BaudRate = Convert.ToInt32(strtmp[1]);
            Parity = strtmp[2];
            DataBits = Convert.ToInt32(strtmp[3]);
            StopBits = strtmp[4];
        }

        #endregion

        #region methods

        /// <summary>
        /// 打开通讯口
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                if (m_SerialPort == null || m_SerialPort.IsOpen == false)
                {
                    m_SerialPort = new SerialPort(m_szPortName, m_nBaudRate, m_Parity, m_nDataBits, m_StopBits);

                    m_SerialPort.Open();
                    m_szStateInfo = "open";
                    m_szState = "idle";
                    m_bOpened = true;
                    m_SerialPort.ErrorReceived += new SerialErrorReceivedEventHandler(SerialPortErrorHandler);
                    return true;
                }
                else
                {
                    return true;                    
                }
            }
            catch (System.Exception exp)
            {
                WriteLog("打开 " + PortName + " 失败");
                m_szStateInfo = "close";
                m_szState = "close";
                m_bOpened = false;
                return false;
            }
        }

        /// <summary>
        /// 关闭通讯口
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                if (m_SerialPort.IsOpen)
                {
                    m_SerialPort.Close();                    
                }

                m_szStateInfo = "close";
                m_szState = "close";
                m_bOpened = false;
                return true;
            }
            catch (System.Exception exp)
            {
                return false;
            }
        }

        /// <summary>
        /// 转换字符串为int数组
        /// </summary>
        /// <param name="s">要转换的字符串</param>
        /// <returns></returns>
        private int[] StringToHexString(string s)
        {
            Encoding CnEnconding = Encoding.GetEncoding("GB2312");

            byte[] b = CnEnconding.GetBytes(s);
            int[] rel = new int[b.Length];
            string result = string.Empty;
            int i = 0;
            for (i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i], 16);
                rel[i] = Convert.ToInt32(b[i]);
            }

            return rel;
        }

        /// <summary>
        /// 24位真彩色转16位颜色
        /// </summary>
        /// <param name="rgb">真彩色参数</param>
        /// <returns></returns>
        private byte[] Get565FromARGB(Color rgb)
        {
            ushort ret = 0;

            ushort r = rgb.R;
            ushort g = rgb.G;
            ushort b = rgb.B;

            ret = (ushort)(((r >> 3) << 11) | ((g >> 2) << 5) | (b >> 3));

            byte[] retbyte = new byte[] { (byte)(ret >> 8), (byte)ret };
            return retbyte;
        }

        /// <summary>
        /// 清屏，在每次执行一组事务前应调用此方法
        /// </summary>
        /// <returns></returns>
        public bool ClearScreen()
        {
            if (m_bOpened == false)
            {
                return false;
            }

            byte[] strCommand = new byte[] { 0xAA, 0x52, 0xCC, 0x33, 0xC3, 0x3C };
            m_SerialPort.Write(strCommand, 0, strCommand.Length);

            return true;
        }

        /*
         * 设置当前调色板(0x40)
         * TX: AA 40 <FC> <BC> CC 33 C3 3C
         * Rx: 无
         * <FC> 前景色调色板, 2 字节 (16 bit, 65K color,565模式), 复位默认值是 0xFFFF (白色)。
         * <BC> 背景色调色板, 2 字节(16 bit, 65K color,565模式),复位默认值是 0x001F (蓝色)。
         */
        /// <summary>
        /// 设置前景色和背景色，对于98命令，本函数设定的前景色不起作用，客户端调用98命令时如果为了显示一致，最好使用本方法设置的前景色作为字体颜色
        /// </summary>
        /// <param name="colorFore">前景色</param>
        /// <param name="colorBack">背景色</param>
        /// <returns></returns>
        public bool SetDisplayColor(Color colorFore, Color colorBack)
        {
            if (m_bOpened == false)
            {
                return false;
            }

            byte[] foreColor = Get565FromARGB(colorFore);
            byte[] backColor = Get565FromARGB(colorBack);

            byte[] strCommand = new byte[10];
            strCommand[0] = 0xAA;
            strCommand[1] = 0x40;

            strCommand[2] = foreColor[0];
            strCommand[3] = foreColor[1];
            strCommand[4] = backColor[0];
            strCommand[5] = backColor[1];

            strCommand[6] = 0xCC;
            strCommand[7] = 0x33;
            strCommand[8] = 0xC3;
            strCommand[9] = 0x3C;

            m_SerialPort.Write(strCommand, 0, 10);

            return true;
        }

        /// <summary>
        /// 在指定坐标以指定字库和指定格式显示字符串
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="LibID">字库选择，取值范围0x00-0x3B</param>
        /// <param name="Mode">选择文本显示模式以及编码方式</param>
        /// <param name="FontSize">字体大小，目前准备使用的中文字库只支持1-7，且只有7能够正确显示</param>
        /// <param name="ForeColor">字体颜色，一般情况应该和SetDisplayColor方法调用的前景色一致</param>
        /// <param name="BackColor">字体背景色，当Mode为0x8*时本颜色不起作用</param>
        /// <param name="strData">要显示的字符串</param>
        /// <returns></returns>
        public bool WriteText(ushort x, ushort y, byte LibID, byte Mode, byte FontSize, Color ForeColor, Color BackColor, string strData)
        {
            if (m_bOpened == false)
            {
                return false;
            }

            int j = 0;
            int[] reVal;

            byte[] fore = Get565FromARGB(ForeColor);
            byte[] back = Get565FromARGB(BackColor);

            reVal = StringToHexString(strData);
            byte[] strCommand = new byte[255];
            strCommand[0] = 0xAA;
            strCommand[1] = 0x98;
            strCommand[2] = (byte)(x >> 8);
            strCommand[3] = (byte)x;
            strCommand[4] = (byte)(y >> 8);
            strCommand[5] = (byte)y;            
            strCommand[6] = LibID;//字库选择，取值范围0x00-0x3B
            strCommand[7] = Mode;//选择文本显示模式以及编码方式
            strCommand[8] = FontSize;//字体大小，目前准备使用的中文字库只支持1-7
            strCommand[9] = fore[0];
            strCommand[10] = fore[1];
            strCommand[11] = back[0];
            strCommand[12] = back[1];
            for (j = 0; j < reVal.Length; j++)
            {
                strCommand[j + 13] = (byte)(reVal[j]);
            }
            strCommand[reVal.Length + 13] = 0xCC;
            strCommand[reVal.Length + 14] = 0x33;
            strCommand[reVal.Length + 15] = 0xC3;
            strCommand[reVal.Length + 16] = 0x3C;

            m_SerialPort.Write(strCommand, 0, 17 + reVal.Length);

            return true;
        }

        /// <summary>
        /// 在指定坐标以透明背景显示字符串
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="ForeColor">字体颜色</param>
        /// <param name="strData">要显示的字符串</param>
        /// <returns></returns>
        public bool WriteText(ushort x, ushort y, Color ForeColor, string strData)
        {
            if (m_bOpened == false)
            {
                return false;
            }

            int j = 0;
            int[] reVal;

            byte[] fore = Get565FromARGB(ForeColor);


            reVal = StringToHexString(strData);
            byte[] strCommand = new byte[255];
            strCommand[0] = 0xAA;
            strCommand[1] = 0x98;
            strCommand[2] = (byte)(x >> 8);
            strCommand[3] = (byte)x;
            strCommand[4] = (byte)(y >> 8);
            strCommand[5] = (byte)y;
            strCommand[6] = 0x23;//字库选择，取值范围0x00-0x3B
            strCommand[7] = 0x81;//选择文本显示模式以及编码方式
            strCommand[8] = 0x03;//字体大小，目前准备使用的中文字库只支持1-7
            strCommand[9] = fore[0];
            strCommand[10] = fore[1];
            strCommand[11] = 0x00;
            strCommand[12] = 0x1F;
            for (j = 0; j < reVal.Length; j++)
            {
                strCommand[j + 13] = (byte)(reVal[j]);
            }
            strCommand[reVal.Length + 13] = 0xCC;
            strCommand[reVal.Length + 14] = 0x33;
            strCommand[reVal.Length + 15] = 0xC3;
            strCommand[reVal.Length + 16] = 0x3C;

            m_SerialPort.Write(strCommand, 0, 17 + reVal.Length);

            return true;
        }

        /// <summary>
        /// 在指定坐标以标准字库和系统预设格式显示字符串
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="Font">字体：       0x53: 显示 8*8 点阵 ASCII 字符串；
        ///                                 0x54: 显示 16*16 点阵的扩展码汉字字符串（ASCII 字符以半角 8*16 点阵显示）
        ///                                 0x55: 显示 32*32 点阵的内码汉字字符串（ASCII 字符以半角 16*32 点阵显示）
        ///                                 0x6E: 显示 12*12 点阵的扩展码汉字字符串（ASCII 字符以半角 6*12 点阵显示）
        ///                                 0x6F: 显示 24*24 点阵的内码汉字字符串（ASCII 字符以半角 12*24 点阵显示）</param>
        /// <param name="strData">要显示的字符串</param>
        /// <returns></returns>
        public bool WriteText(ushort x, ushort y, byte Font, string strData)
        {
            if (m_bOpened == false)
            {
                return false;
            }

            if (Font != 0x53 && Font != 0x54 && Font != 0x55 && Font != 0x6E && Font != 0x6F)
            {
                return false;
            }

            int i = 0;
            int j = 0;
            int[] reVal;

            reVal = StringToHexString(strData);
            byte[] strCommand = new byte[255];
            strCommand[i++] = 0xAA;
            strCommand[i++] = Font;
            strCommand[i++] = (byte)(x >> 8);
            strCommand[i++] = (byte)x;
            strCommand[i++] = (byte)(y >> 8);
            strCommand[i++] = (byte)y;
            
            for (j = 0; j < reVal.Length; j++)
            {
                strCommand[i++] = (byte)(reVal[j]);
            }
            strCommand[i++] = 0xCC;
            strCommand[i++] = 0x33;
            strCommand[i++] = 0xC3;
            strCommand[i++] = 0x3C;

            m_SerialPort.Write(strCommand, 0, i);

            return true;
        }

        /// <summary>
        /// 全屏显示图片
        /// </summary>
        /// <param name="PictureID">库中的图片ID</param>
        /// <returns></returns>
        public bool DrawPicture(int PictureID)
        {
            if (m_bOpened == false)
            {
                return false;
            }

            byte[] pid = new byte[] { (byte)(PictureID >> 24), (byte)((PictureID << 8) >> 24), (byte)((PictureID << 16) >> 24), (byte)PictureID };

            byte[] strCommand = null;
            if (pid[0] > 0)
            {
                strCommand = new byte[] { 0xAA, 0x70, pid[0], pid[1], pid[2], pid[3], 0xCC, 0x33, 0xC3, 0x3C };
            }
            else if (pid[1] > 0)
            {
                strCommand = new byte[] { 0xAA, 0x70, pid[1], pid[2], pid[3], 0xCC, 0x33, 0xC3, 0x3C };
            }
            else if (pid[2] > 0)
            {
                strCommand = new byte[] { 0xAA, 0x70, pid[2], pid[3], 0xCC, 0x33, 0xC3, 0x3C };
            }
            else
            {
                strCommand = new byte[] { 0xAA, 0x70, pid[3], 0xCC, 0x33, 0xC3, 0x3C };
            }

            m_SerialPort.Write(strCommand, 0, strCommand.Length);

            return true;
        }

        /// <summary>
        /// 显示剪贴图
        /// </summary>
        /// <param name="PictureID">图片在库中的ID</param>
        /// <param name="left">前贴坐标-左</param>
        /// <param name="top">前贴坐标-上</param>
        /// <param name="right">前贴坐标-右</param>
        /// <param name="bottom">前贴坐标-下</param>
        /// <param name="x">贴图坐标x</param>
        /// <param name="y">贴图坐标y</param>
        /// <returns></returns>
        public bool DrawClipPicture(int PictureID, ushort left, ushort top, ushort right, ushort bottom, ushort x, ushort y)
        {
            if (m_bOpened == false)
            {
                return false;
            }

            int i = 0;

            byte[] strCommand = new byte[255];

            strCommand[i++] = 0xAA;
            strCommand[i++] = 0x71;

            byte[] pid = new byte[] { (byte)(PictureID >> 24), (byte)((PictureID << 8) >> 24), (byte)((PictureID << 16) >> 24), (byte)PictureID };

            if (pid[0] > 0)
            {
                strCommand[i++] = pid[0];
                strCommand[i++] = pid[1];
                strCommand[i++] = pid[2];
                strCommand[i++] = pid[3];
            }
            else if (pid[1] > 0)
            {
                strCommand[i++] = pid[1];
                strCommand[i++] = pid[2];
                strCommand[i++] = pid[3];
            }
            else if (pid[2] > 0)
            {
                strCommand[i++] = pid[2];
                strCommand[i++] = pid[3];
            }
            else
            {
                strCommand[i++] = pid[3];
            }


            strCommand[i++] = (byte)(left >> 8);
            strCommand[i++] = (byte)left;
            strCommand[i++] = (byte)(top >> 8);
            strCommand[i++] = (byte)top;

            strCommand[i++] = (byte)(right >> 8);
            strCommand[i++] = (byte)right;
            strCommand[i++] = (byte)(bottom >> 8);
            strCommand[i++] = (byte)bottom;

            strCommand[i++] = (byte)(x >> 8);
            strCommand[i++] = (byte)x;
            strCommand[i++] = (byte)(y >> 8);
            strCommand[i++] = (byte)y;

            strCommand[i++] = 0xCC;
            strCommand[i++] = 0x33;
            strCommand[i++] = 0xC3;
            strCommand[i++] = 0x3C;

            m_SerialPort.Write(strCommand, 0, i);

            return true;
        }

        private void SerialPortErrorHandler(object o, SerialErrorReceivedEventArgs e)
        {
            m_szStateInfo = e.EventType.ToString();
            WriteLog("SerialPortErrorHandler:" + e.EventType.ToString());
        }

        private void WriteLog(string str)
        {
            try
            {
                string m_szRunPath = System.Environment.CurrentDirectory.ToString();
                //string m_szRunPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                if (!(System.IO.Directory.Exists(m_szRunPath + "\\log")))
                {
                    System.IO.Directory.CreateDirectory(m_szRunPath + "\\log");
                }

                string strDate = System.DateTime.Now.ToString("yyyyMMddhhmm");

                System.IO.TextWriter tw = new System.IO.StreamWriter(m_szRunPath + "\\log\\" + DeviceName + "_" + PortName + "-" + strDate + "_Display.log", true);

                tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                tw.WriteLine(str);
                tw.WriteLine("\r\n");

                tw.Close();
            }
            catch (Exception e)
            {
                ;
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
        /// 状态信息
        /// </summary>
        public string State
        {
            get
            {
                return m_szState;
            }
            set
            {
                m_szState = value;
            }
        }
        /// <summary>
        /// 设备端口名称
        /// </summary>
        public string PortName
        {
            get
            {
                return m_szPortName;
            }
            set
            {
                m_szPortName = value;
            }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            get
            {
                return m_nBaudRate;
            }
            set
            {
                m_nBaudRate = value;
            }
        }

        /// <summary>
        /// 校验位
        /// </summary>
        public string Parity
        {
            get
            {
                if (m_Parity == System.IO.Ports.Parity.None)
                {
                    return "NONE";
                }
                else if (m_Parity == System.IO.Ports.Parity.Odd)
                {
                    return "ODD";
                }
                else if (m_Parity == System.IO.Ports.Parity.Even)
                {
                    return "EVEN";
                }
                else if (m_Parity == System.IO.Ports.Parity.Mark)
                {
                    return "MARK";
                }
                else if (m_Parity == System.IO.Ports.Parity.Space)
                {
                    return "SPACE";
                }
                return "unknown";
            }
            set
            {
                if (value.ToUpper() == "NONE")
                {
                    m_Parity = System.IO.Ports.Parity.None;
                }
                else if (value.ToUpper() == "ODD")
                {
                    m_Parity = System.IO.Ports.Parity.Odd;
                }
                else if (value.ToUpper() == "EVEN")
                {
                    m_Parity = System.IO.Ports.Parity.Even;
                }
                else if (value.ToUpper() == "MARK")
                {
                    m_Parity = System.IO.Ports.Parity.Mark;
                }
                else if (value.ToUpper() == "SPACE")
                {
                    m_Parity = System.IO.Ports.Parity.Space;
                }
            }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public int DataBits
        {
            get
            {
                return m_nDataBits;
            }
            set
            {
                m_nDataBits = value;
            }
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBits
        {
            get
            {
                if (m_StopBits == System.IO.Ports.StopBits.None)
                {
                    return "0";
                }
                else if (m_StopBits == System.IO.Ports.StopBits.One)
                {
                    return "1";
                }
                else if (m_StopBits == System.IO.Ports.StopBits.Two)
                {
                    return "2";
                }
                else if (m_StopBits == System.IO.Ports.StopBits.OnePointFive)
                {
                    return "1.5";
                }

                return "unknown";
            }
            set
            {
                if (value.ToUpper() == "0")
                {
                    m_StopBits = System.IO.Ports.StopBits.None;
                }
                else if (value.ToUpper() == "1")
                {
                    m_StopBits = System.IO.Ports.StopBits.One;
                }
                else if (value.ToUpper() == "2")
                {
                    m_StopBits = System.IO.Ports.StopBits.Two;
                }
                else if (value.ToUpper() == "1.5")
                {
                    m_StopBits = System.IO.Ports.StopBits.OnePointFive;
                }
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
        #endregion
    }
}
