using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace YGJZJL.StrapWeight
{
    /// <summary>
    /// 串口操作类，目前只用于汽车衡计量仪表
    /// </summary>
    class CoolSerial
    {
        #region private member variable

        private SerialPort m_SerialPort;
        private string m_szDeviceType;//设备类型，不同设备的不同处理方法通过次变量进行处理
        private string m_szDeviceName;//设备名称，比如汽车衡1#，主要用于写日志
        private string m_szStateInfo;//设备状态
        private bool m_bOpened;
        private bool m_bRunning;

        //串口参数
        private string m_szPortName;
        private int m_nBaudRate;
        private Parity m_Parity;
        private int m_nDataBits;
        private StopBits m_StopBits;

        //串口数据
        private decimal m_dData;
        private string m_szData;

        //线程
        private System.Threading.Thread m_hThread;

        #endregion

        #region constructor

        public CoolSerial()
        {
            m_SerialPort = null;
            m_szDeviceType = "";
            m_szDeviceName = "";
            m_szStateInfo = "close";
            m_bOpened = false;
            m_bRunning = false;

            m_szPortName = "";
            m_nBaudRate = 0;
            m_Parity = System.IO.Ports.Parity.None;
            m_nDataBits = 0;
            m_StopBits = System.IO.Ports.StopBits.One;

            m_dData = 0;
            m_szData = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="szSerialParams">串口参数，必须是以下格式：串口名,波特率,校验位,数据位,停止位
        ///                                         如：COM9,9600,N,8,1或COM2,115200,E,7,1.5</param>
        public CoolSerial(string szSerialParams)
        {
            string[] strtmp = szSerialParams.Split(new char[] { ','});
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
                m_bOpened = false;
                return false;
            }

            return false;
        }

        /// <summary>
        /// 启动通讯线程
        /// </summary>
        public void StartUse()
        {
            if (m_bOpened == false)
            {
                return;
            }

            m_bRunning = true;
            m_hThread = new System.Threading.Thread(new System.Threading.ThreadStart(DeviceThread));
            m_hThread.Start();
        }

        /// <summary>
        /// 读数据线程
        /// </summary>
        private void DeviceThread()
        {
            try
            {
                StringBuilder strRecv = new StringBuilder();
                string strtmp = "";
                string strData = "";
                char[] strCommand3190 = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };

                while (m_bRunning)
                {
                    try
                    {
                        if (m_bOpened == false)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }

                        System.Threading.Thread.Sleep(200);

                       switch (m_szDeviceType)
                        {
                            case "XK3190":
                                this.Parse_XK3190();
                                break;
                            case "XK3190B":
                                this.Parse_XK3190B();
                                break;
                            case "XK3190A9":
                                this.Parse_XK3190A9();
                                break;
                            case "8142":
                                this.Parse_8142();
                                break;
                            case "XK2001":
                                this.Parse_XK2001();
                                break;
                            default:
                                break;

                        }
                        //设备个性需求处理---------------------------------
                    }
                    catch (System.Exception error)
                    {
                        WriteLog(m_szDeviceName + ")串口错误：" + error.Message);
                    }
                }

                m_bOpened = false;

                try
                {
                    m_SerialPort.Close();
                   
                    m_szStateInfo = "close";
                    WriteLog(m_szDeviceName + ")串口正常关闭");
                }
                catch (System.Exception error)
                {
                    WriteLog(m_szDeviceName + ")关闭串口时发生错误：" + error.Message);
                }
            }
            catch (System.Exception exp)
            {
                WriteLog(m_szDeviceName + ")错误：" + exp.Message);
            }
        }


        /// <summary>
        /// XK2001数据解析
        /// </summary>
        private void Parse_XK2001()
        {
            StringBuilder strRecv = new StringBuilder();
            string strtmp = "";
            string strData = "";
            if (m_SerialPort.BytesToRead > 0)
            {
                System.Threading.Thread.Sleep(100);
                strtmp = m_SerialPort.ReadExisting();

                strRecv.Append(strtmp);

                //=和=之间是数据
                int index = strRecv.ToString().LastIndexOf('=');

                if (index >= 7)
                {
                    strData = strRecv.ToString().Substring(index - 7, 7);
                    strData.Trim();

                    m_szData = strData;
                    m_dData = getWeight_XK2001(strData);
                    strRecv.Remove(0, index + 1);
                }
            }
        }

        /// <summary>
        /// XK3190数据解析
        /// </summary>
        private void Parse_XK3190()
        {
            StringBuilder strRecv = new StringBuilder();
            string strtmp = "";
            string strData = "";
            if (m_SerialPort.BytesToRead > 0)
            {
                System.Threading.Thread.Sleep(100);
                strtmp = m_SerialPort.ReadExisting();

                strRecv.Append(strtmp);

                //=和=之间是数据
                //strRecv.Replace("-", "0");
                int index = strRecv.ToString().LastIndexOf('=');

                if (strRecv.Length > 8)//index >= 8
                {
                    strData = strRecv.ToString().Substring(index+1).Trim();
                    //strData.Trim();
                    byte[] buff = Encoding.ASCII.GetBytes(strData);
                    Array.Reverse(buff);
                    m_szData = strData;
                    m_dData = Decimal.Parse(Encoding.ASCII.GetString(buff))/1000;
                    //m_dData = getWeight_XK3190(strData);
                    strRecv.Remove(0, index + 1);
                }
            }
        }

        /// <summary>
        /// XK3190A9数据解析
        /// </summary>
        private void Parse_XK3190A9()
        {
            StringBuilder strRecv = new StringBuilder();
            string strtmp = "";
            string strData = "";
            if (m_SerialPort.BytesToRead > 0)
            {
                strtmp = m_SerialPort.ReadExisting();
                strRecv.Append(strtmp);

                //0x02和0x03之间是数据
                int index = strRecv.ToString().LastIndexOf((char)0x03);

                if (index >= 11)
                {
                    strData = strRecv.ToString().Substring(index - 10, 8);
                    strData.Trim();

                    m_szData = strData;
                    m_dData = getWeight_XK3190A9(strData);

                    strRecv.Remove(0, index + 1);
                }
            }
        }

        /// <summary>
        /// XK3190B数据解析
        /// </summary>
        private void Parse_XK3190B()
        {
            StringBuilder strRecv = new StringBuilder();
            string strtmp = "";
            string strData = "";
            if (m_SerialPort.BytesToRead > 0)
            {
                System.Threading.Thread.Sleep(500);
                strtmp = m_SerialPort.ReadExisting();

                strRecv.Append(strtmp);

                //=和=之间是数据
                int index = strRecv.ToString().LastIndexOf('=');

                if (index >= 8)
                {
                    strData = strRecv.ToString().Substring(index - 8, 8);
                    strData.Trim();

                    m_szData = strData;
                    m_dData = getWeight_XK3190B(strData);
                    strRecv.Remove(0, index + 1);
                }
            }
        }

        /// <summary>
        /// 8142数据解析
        /// </summary>
        private void Parse_8142()
        {
            StringBuilder strRecv = new StringBuilder();
            string strtmp = "";
            string strData = "";
            if (m_SerialPort.BytesToRead > 0)
            {
                strtmp = m_SerialPort.ReadExisting();
                strRecv.Append(strtmp);

                //0x02和0x03之间是数据
                int index = strRecv.ToString().LastIndexOf((char)0x0D);

                if (index >= 16)
                {
                    strData = strRecv.ToString().Substring(index - 16, 17);
                    strData.Trim();

                    m_szData = strData;
                    m_dData = getWeight_8142(strData);

                    strRecv.Remove(0, index + 1);
                }
            }
        }

        //获取仪表重量值 XK2001
        private Decimal getWeight_XK2001(string strMeterData)
        {
            Decimal decData = 0;
            double tmpdata = 0;
            int j = 0;

            string result = "";

            char[] tmp = strMeterData.ToCharArray();
            for (j = tmp.Length - 2; j >= 0; j--)
            {
                result = result + tmp[j].ToString();
            }
            tmpdata = Convert.ToDouble(result.Trim());
            tmpdata = tmpdata / 1000;
            decData = Convert.ToDecimal(string.Format("{0:F3}", Convert.ToDecimal(tmpdata)));

            return decData;
        }

        //获取仪表重量值 XK3190
        private Decimal getWeight_XK3190(string strMeterData)
        {
            Decimal decData = 0;

            int j = 0;

            string result = "";

            char[] tmp = strMeterData.ToCharArray();
            for (j = tmp.Length - 1; j >= 3; j--)
            {
                result = result + tmp[j].ToString();
            }
            result += ".";
            for (j = 2; j >= 0; j--)
            {
                result = result + tmp[j].ToString();
            }
            decData = Convert.ToDecimal(string.Format("{0:F3}", Convert.ToDecimal(result)));
            return decData;
        }

        //获取仪表重量值 XK3190B
        private Decimal getWeight_XK3190B(string strMeterData)
        {
            Decimal decData = 0;

            int j = 0;

            string result = "";

            char[] tmp = strMeterData.ToCharArray();
            for (j = tmp.Length - 1; j >= 3; j--)
            {
                result = result + tmp[j].ToString();
            }
            result += ".";
            for (j = 2; j >= 0; j--)
            {
                result = result + tmp[j].ToString();
            }
            decData = Convert.ToDecimal(string.Format("{0:F3}", Convert.ToDecimal(result)));
            return decData;
        }

        //获取仪表重量值 XK3190A9
        private Decimal getWeight_XK3190A9(string strMeterData)
        {
            //连续发送模式下的数据提取
            double tmpdata = 0;
            Decimal decData = 0;

            if (strMeterData.Length == 8)
            {
                tmpdata = Convert.ToDouble(strMeterData.Substring(0, 7));
                tmpdata = tmpdata / 1000;//kg转换为t
                tmpdata = tmpdata / Math.Pow(10, Convert.ToDouble(strMeterData.Substring(strMeterData.Length - 1, 1)));
                decData = Convert.ToDecimal(string.Format("{0:F3}", Convert.ToDecimal(tmpdata)));

            }
            return decData;
        }

        //获取仪表重量值 8142
        private Decimal getWeight_8142(string strMeterData)
        {
            Decimal decData = 0;
            double tmpdata = 0;
            if (strMeterData.Length == 17)
            {
                tmpdata = Convert.ToDouble(strMeterData.Substring(4, 6));
                tmpdata = tmpdata / 1000;//kg转换为t
                //tmpdata = tmpdata / Math.Pow(10, Convert.ToDouble(strMeterData.Substring(strMeterData.Length - 1, 1)));
                decData = Convert.ToDecimal(string.Format("{0:F3}", Convert.ToDecimal(tmpdata)));
            }
            return decData;
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

            m_szStateInfo = "close";
            m_bOpened = false;
        }

        private void SerialPortErrorHandler(object o, SerialErrorReceivedEventArgs e)
        {
            m_szStateInfo = e.EventType.ToString();
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

                System.IO.TextWriter tw = new System.IO.StreamWriter(m_szRunPath + "\\log\\" + DeviceName + "_" + PortName + "-" + strDate + "_Serial.log", true);

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
        /// 校验位
        /// </summary>
        public string Parity
        {
            get
            {
                if (m_Parity == System.IO.Ports.Parity.None)
                {
                    return "N";
                }
                else if (m_Parity == System.IO.Ports.Parity.Odd)
                {
                    return "O";
                }
                else if (m_Parity == System.IO.Ports.Parity.Even)
                {
                    return "E";
                }
                else if (m_Parity == System.IO.Ports.Parity.Mark)
                {
                    return "M";
                }
                else if (m_Parity == System.IO.Ports.Parity.Space)
                {
                    return "S";
                }
                return "unknown";
            }
            set
            {
                if (value.ToUpper() == "N")
                {
                    m_Parity = System.IO.Ports.Parity.None;
                }
                else if (value.ToUpper() == "O")
                {
                    m_Parity = System.IO.Ports.Parity.Odd;
                } 
                else if (value.ToUpper() == "E")
                {
                    m_Parity = System.IO.Ports.Parity.Even;
                }
                else if (value.ToUpper() == "M")
                {
                    m_Parity = System.IO.Ports.Parity.Mark;
                }
                else if (value.ToUpper() == "S")
                {
                    m_Parity = System.IO.Ports.Parity.Space;
                }
            }
        }

        /// <summary>
        /// 数据位
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
        /// 通讯原始报文
        /// </summary>
        public string StringData
        {
            get
            {
                return m_szData;
            }
        }

        /// <summary>
        /// 仪表读数
        /// </summary>
        public decimal DecimalData
        {
            get
            {
                return m_dData;
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
