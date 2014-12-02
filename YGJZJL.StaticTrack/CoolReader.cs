using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YGJZJL.StaticTrack
{
    /// <summary>
    /// 读卡器类，用于操作读卡器，读写数据
    /// </summary>
    class CoolReader
    {
        #region api list

        [DllImport("mwrf32.dll", EntryPoint = "rf_init", SetLastError = true,      CharSet = CharSet.Auto, ExactSpelling = false,
            CallingConvention = CallingConvention.StdCall)]
        //说明：初始化通讯接口
        public static extern int rf_init(Int16 port, int baud);

        [DllImport("mwrf32.dll", EntryPoint = "rf_anticoll", SetLastError = true,CharSet = CharSet.Auto, ExactSpelling = false,     CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_anticoll(int icdev, int bcnt, out uint snr);

        [DllImport("mwrf32.dll", EntryPoint = "rf_reset", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_reset(int icdev, int msec);


        [DllImport("mwrf32.dll", EntryPoint = "rf_request", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_request(int icdev, int mode, out UInt16 tagtype);

        [DllImport("mwrf32.dll", EntryPoint = "rf_beep", SetLastError = true,
     CharSet = CharSet.Auto, ExactSpelling = false,
     CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_beep(int icdev, int msec);

        [DllImport("mwrf32.dll", EntryPoint = "a_hex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, [MarshalAs(UnmanagedType.LPArray)]byte[] hex, int len);

        [DllImport("mwrf32.dll", EntryPoint = "rf_load_key", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_load_key(int icdev, int mode, int secnr, [MarshalAs(UnmanagedType.LPArray)]byte[] keybuff);

        [DllImport("mwrf32.dll", EntryPoint = "hex_a", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 hex_a([MarshalAs(UnmanagedType.LPArray)]byte[] hex, [MarshalAs(UnmanagedType.LPArray)]byte[] asc, int len);


        [DllImport("mwrf32.dll", EntryPoint = "rf_authentication", SetLastError = true,                 CharSet = CharSet.Auto, ExactSpelling = false,                 CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_authentication(int icdev, int mode, int secnr);

        [DllImport("mwrf32.dll", EntryPoint = "rf_read", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_read(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        [DllImport("mwrf32.dll", EntryPoint = "rf_select", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,
     CallingConvention = CallingConvention.StdCall)]
        //说明：     返回设备当前状态
        public static extern Int16 rf_select(int icdev, uint snr, out byte size);

        [DllImport("mwrf32.dll", EntryPoint = "rf_write", SetLastError = true,
     CharSet = CharSet.Auto, ExactSpelling = false,
     CallingConvention = CallingConvention.StdCall)]

        //说明：     返回设备当前状态
        public static extern Int16 rf_write(int icdev, int blocknr, [MarshalAs(UnmanagedType.LPArray)]byte[] databuff);

        [DllImport("mwrf32.dll", EntryPoint = "rf_exit", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //说明：    关闭通讯口
        public static extern Int16 rf_exit(int icdev);


        #endregion

        #region private member variable

        private string m_szGUID;//全球唯一序列号
        private string m_szCardNo;//助记卡号，如00001
        private int m_nDevice;//设备句柄

        private string m_szDeviceType;//设备类型，不同设备的不同处理方法通过次变量进行处理
        private string m_szDeviceName;//设备名称，比如汽车衡1#，主要用于写日志
        private string m_szStateInfo;//设备状态
        private bool m_bOpened;
        private bool m_bRunning;

        //串口参数
        private string m_szPortName;
        private int m_nBaudRate;//波特率
        private short m_nCommPort;//串口号

        private System.Threading.Thread m_hThread = null;
        

        #endregion

        #region constructor

        public CoolReader()
        {
            m_szGUID = "";//全球唯一序列号
            m_szCardNo = "";//助记卡号，如00001
            m_nDevice = 0;//设备句柄

            m_szDeviceType = "";//设备类型，不同设备的不同处理方法通过次变量进行处理
            m_szDeviceName = "";//设备名称，比如汽车衡1#，主要用于写日志
            m_szStateInfo = "";//设备状态
            m_bOpened = false;
            m_bRunning = false;

            //串口参数
            m_szPortName = "";
            m_nBaudRate = 0;//波特率
            m_nCommPort = 0;//串口号

            m_hThread = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nPort">串口号，如：9</param>
        /// <param name="nBaudRate">波特率，如：9600</param>
        public CoolReader(short nPort, int nBaudRate)
        {
            m_szPortName = "COM" + nPort.ToString();
            m_nBaudRate = nBaudRate;//波特率
            m_nCommPort = nPort;//串口号
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strParams">串口参数，必须是以下格式：串口号,波特率。如：9,9600或2,115200</param>
        public CoolReader(string strParams)
        {
            string[] strtmp = strParams.Split(new char[] { ',' });
            if (strtmp.Length < 2)
            {
                return;
            }

            m_nCommPort = Convert.ToInt16(strtmp[0]);//串口号

            m_szPortName = "COM" + m_nCommPort.ToString();
            m_nBaudRate = Convert.ToInt32(strtmp[1]);//波特率
        }

        #endregion 

        #region methods

        /// <summary>
        /// 打开通讯口，调用StartUse方法前应调用此方法
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                if (m_bOpened == false)
                {
                    m_nDevice = rf_init(m_nCommPort, m_nBaudRate);

                    if (m_nDevice > 0)
                    {
                        m_szStateInfo = "open";
                        m_bOpened = true;
                        return true;
                    }
                    else
                    {
                        WriteLog("打开 " + m_szPortName + " 失败。");
                        m_szStateInfo = "close";
                        m_bOpened = false;
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception exp)
            {
                WriteLog(exp.Message);
                m_szStateInfo = "close";
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
            if (m_nDevice > 0)
            {
                rf_exit(m_nDevice);
                m_nDevice = 0;
                m_szStateInfo = "close";
                m_bOpened = false;
                return true;
            }

            return true;
        }

        /// <summary>
        /// 启动通讯线程
        /// </summary>
        public void StartUse()
        {
            if (m_bOpened == false)
            {
                return ;
            }

            m_bRunning = true;
            m_hThread = new System.Threading.Thread(new System.Threading.ThreadStart(DeviceThread));
            m_hThread.Start();
        }

        /// <summary>
        /// 读数据线程，读到的GUID和数据分别保存在属性GUID和CardNo
        /// </summary>
        private void DeviceThread()
        {
            try
            {
                UInt16 tagtype = 0;
                byte size = 0;
                uint snr = 0;
                int st = 0;
                int sec = 1;
                int i = 0;
                string skey = "ffffffffffff";
                byte[] key1 = new byte[17];
                byte[] key2 = new byte[7];
                byte[] data = new byte[16];
                byte[] buff = new byte[32];
                string reVal = "";

                for (i = 0; i < 16; i++)
                    data[i] = 0;
                for (i = 0; i < 32; i++)
                    buff[i] = 0;

                while (m_bRunning)
                {
                    if (m_bOpened == false)
                    {
                        System.Threading.Thread.Sleep(300);
                        continue;
                    }

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);

                    rf_reset(m_nDevice, 3);//1

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);

                    st = rf_request(m_nDevice, 1, out tagtype);//2
                    if (st != 0)
                        continue;

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);

                    st = rf_anticoll(m_nDevice, 0, out snr);//3
                    if (st != 0)
                        continue;

                    string snrstr = "";
                    snrstr = snr.ToString("X");
                    m_szGUID = snrstr;

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);

                    st = rf_select(m_nDevice, snr, out size);//4
                    if (st != 0)
                        continue;

                    key1 = Encoding.ASCII.GetBytes(skey);
                    a_hex(key1, key2, 12);

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);

                    st = rf_load_key(m_nDevice, 0, sec, key2);//5
                    if (st != 0)
                        continue;

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);

                    st = rf_authentication(m_nDevice, 0, sec);//6
                    if (st != 0)
                        continue;

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);

                    st = rf_read(m_nDevice, sec * 4 + 1, data);//7
                    if (st != 0)
                        continue;

                    hex_a(data, buff, 16);
                    reVal = System.Text.Encoding.ASCII.GetString(buff);
                    m_szCardNo = reVal.Substring(27, 5);

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);//8

                    st = rf_beep(m_nDevice, 10);

                    if (m_bRunning == false)
                        break;
                    System.Threading.Thread.Sleep(50);
                }

                m_bOpened = false;
            }
            catch (System.Exception exp)
            {
                WriteLog(m_szDeviceName + ")错误：" + exp.Message);
            }
        }

        /// <summary>
        /// 读数据，此方法适用于不启用线程时读卡用，读到的数据保存在CardNo属性中
        /// </summary>
        /// <returns></returns>
        public bool ReadData()
        {
            if (m_bOpened == false)
            {
                return false;
            }

            try
            {
                UInt16 tagtype = 0;
                byte size = 0;
                uint snr = 0;
                int st = 0;
                int sec = 1;
                int i = 0;
                string skey = "ffffffffffff";
                byte[] key1 = new byte[17];
                byte[] key2 = new byte[7];
                byte[] data = new byte[16];
                byte[] buff = new byte[32];
                string reVal = "";

                for (i = 0; i < 16; i++)
                    data[i] = 0;
                for (i = 0; i < 32; i++)
                    buff[i] = 0;

                rf_reset(m_nDevice, 3);
                st = rf_request(m_nDevice, 1, out tagtype);
                if (st != 0)
                    return false;

                st = rf_anticoll(m_nDevice, 0, out snr);
                if (st != 0)
                    return false;

                string snrstr = "";
                snrstr = snr.ToString("X");
                m_szGUID = snrstr;

                st = rf_select(m_nDevice, snr, out size);
                if (st != 0)
                    return false;

                key1 = Encoding.ASCII.GetBytes(skey);
                a_hex(key1, key2, 12);
                st = rf_load_key(m_nDevice, 0, sec, key2);
                if (st != 0)
                    return false;

                st = rf_authentication(m_nDevice, 0, sec);
                if (st != 0)
                    return false;

                st = rf_read(m_nDevice, sec * 4 + 1, data);
                if (st != 0)
                    return false;

                hex_a(data, buff, 16);
                reVal = System.Text.Encoding.ASCII.GetString(buff);
                m_szCardNo = reVal.Substring(27, 5);

                st = rf_beep(m_nDevice, 10);
                
                return true;

            }
            catch (System.Exception exp)
            {
                WriteLog(m_szDeviceName + ")错误：" + exp.Message);
                return false;
            }
        }

        /// <summary>
        /// 在0扇区1块写数据，这个方法不具备指定区块写数据的功能，如有更多必要，可完善改进
        /// </summary>
        /// <param name="strData">要写入的数据，不能大于32字节长度</param>
        /// <returns></returns>
        public bool WriteData(string strData)
        {
            if (m_bOpened == false)
            {
                return false;
            }

            if (strData.Length > 32)
            {
                return false;
            }

            int strDataLength = strData.Trim().Length;//add by harpor
            for (int i = 0; i < 32 - strDataLength; i++)
            {
                strData = "0" + strData;
            }

            UInt16 tagtype = 0;
            byte size = 0;
            uint snr = 0;
            int st = 0;
            int sec = 1;

            string skey = "ffffffffffff";
            byte[] key1 = new byte[17];
            byte[] key2 = new byte[7];
            byte[] data = new byte[16];
            byte[] buff = new byte[32];

            byte[] databuff = new byte[16];
            byte[] buff1 = new byte[32];

            rf_reset(m_nDevice, 3);
            st = rf_request(m_nDevice, 1, out tagtype);
            if (st != 0)
                return false;

            st = rf_anticoll(m_nDevice, 0, out snr);
            if (st != 0)
                return false;

            string snrstr = "";
            snrstr = snr.ToString("X");

            st = rf_select(m_nDevice, snr, out size);
            if (st != 0)
                return false;

            key1 = Encoding.ASCII.GetBytes(skey);
            a_hex(key1, key2, 12);
            st = rf_load_key(m_nDevice, 0, sec, key2);
            if (st != 0)
                return false;

            st = rf_authentication(m_nDevice, 0, sec);
            if (st != 0)
                return false;

            buff1 = Encoding.ASCII.GetBytes(strData);
            a_hex(buff1, databuff, 32);

            st = rf_write(m_nDevice, sec * 4 + 1, databuff);
            if (st == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 停止使用，通讯线程终止
        /// </summary>
        public void StopUse()
        {
            m_bRunning = false;

            for (int i = 0; i < 50; i++)
            {
                if (m_bOpened == false)
                {
                    break;
                }

                System.Threading.Thread.Sleep(100);
            }

            Close();
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

                System.IO.TextWriter tw = new System.IO.StreamWriter(m_szRunPath + "\\log\\" + DeviceName + "_" + PortName + "-" + strDate + "_Reader.log", true);

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
        /// 串口号
        /// </summary>
        public short CommPort
        {
            get
            {
                return m_nCommPort;
            }
            set
            {
                m_nCommPort = value;
            }
        }

        /// <summary>
        /// 全球唯一序列号
        /// </summary>
        public string GUID
        {
            get
            {
                return m_szGUID;
            }
            set
            {
                m_szGUID = value;
            }
        }

        /// <summary>
        /// 助记卡号，如00001
        /// </summary>
        public string CardNo
        {
            get
            {
                return m_szCardNo;
            }
            set
            {
                m_szCardNo = value;
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
