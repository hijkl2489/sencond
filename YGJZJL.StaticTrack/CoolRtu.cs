using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace YGJZJL.StaticTrack
{
    /// <summary>
    /// Rtu类，Rtu采用Modbus TCP通讯协议进行通信
    /// </summary>
    class CoolRtu
    {
        #region private member variable

        private Socket m_hSocket;

        private string m_RtuIP;//rtu ip地址
        private int m_RtuPort;//rtu 通讯端口

        private string m_szDeviceName;//设备名称（计量点名称）
        private string m_szStateInfo;//设备状态

        private bool m_bOpened;
        private bool m_bRunning;

        //Rtu数据
        private byte[] m_szData;

        //线程
        private System.Threading.Thread m_hThread;

        #endregion

        #region constructor

        public CoolRtu()
        {
            m_hSocket = null;
            m_RtuIP = "";
            m_RtuPort = 0;

            m_bRunning = false;
            m_szDeviceName = "";
            m_bOpened = false;
            m_szStateInfo = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strIP">IP地址</param>
        /// <param name="nPort">通讯端口</param>
        public CoolRtu(string strIP, int nPort)
        {
            m_RtuIP = strIP;
            m_RtuPort = nPort;
        }

        #endregion

        #region methods

        /// <summary>
        /// 和服务器建立通讯连接
        /// </summary>
        public bool Connect2Server()
        {
            if (m_hSocket != null)
            {
                return true;
            }

            try
            {
                m_hSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                //设置接受超时时间为3秒
                m_hSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
                m_hSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);

                IPEndPoint remote = new IPEndPoint(IPAddress.Parse(m_RtuIP), m_RtuPort);

                //建立与远程主机的连接
                m_hSocket.Connect(remote);
                m_bOpened = true;
                m_szStateInfo = "open";
                
                return true;
            }
            catch (Exception error)
            {
                WriteLog(m_szDeviceName + ")和Rtu建立连接时发生错误：" + error.Message);
                m_bOpened = false;
                m_hSocket = null;
                m_szStateInfo = "close";
                
                return false;
            }
        }

        public void Close()
        {
            //关闭连接
            try
            {
                if (m_hSocket.Connected == true)
                {
                    m_hSocket.Close();
                    m_bOpened = false;
                    m_hSocket = null;
                    m_szStateInfo = "close";
                    WriteLog(m_szDeviceName + ")Rtu正常关闭");
                }
            }
            catch (System.Exception error)
            {
                WriteLog(m_szDeviceName + ")关闭Rtu时发生错误：" + error.Message);
            }
        }

        /// <summary>
        /// 向Rtu发送命令
        /// </summary>
        /// <param name="device">设备地址，通常指Rtu的某一模块</param>
        /// <param name="Command">命令,1-读寄存器数据，5-设置线圈（开关）状态</param>
        /// <param name="AddrH">地址偏移高8位</param>
        /// <param name="AddrL">地址偏移低8位</param>
        /// <param name="DevH">需要的数据高8位</param>
        /// <param name="DevL">需要的个数低8位</param>
        /// <returns></returns>
        public byte[] RtuCommand(byte device, byte Command, byte AddrH, byte AddrL, byte DevH, byte DevL)
        {
            if (m_bOpened == false)
            {
                return null;
            }


            byte[] byteRet = new byte[20];
            byte[] byteout = new byte[12];
            int length = 0;

            byteout[0] = 0;
            byteout[1] = 0;
            byteout[2] = 0;
            byteout[3] = 0;
            byteout[4] = 0;
            byteout[5] = 6;
            byteout[6] = device;//设备地址，通常指Rtu的某一模块
            byteout[7] = Command;
            byteout[8] = AddrH;
            byteout[9] = AddrL;
            byteout[10] = DevH;//当Command为5时，0xFF表示线圈断开，0x00表示线圈闭合
            byteout[11] = DevL;

            length = 5 + byteout[5] + 1;

            try
            {
                int iLen = m_hSocket.Send(byteout, length, SocketFlags.None);
                System.Threading.Thread.Sleep(50);

                m_hSocket.Receive(byteRet);

                return byteRet;
            }
            catch (Exception error)
            {
                WriteLog(m_szDeviceName + ")Rtu通讯错误：" + error.Message);
                m_bOpened = false;
                m_hSocket.Close();
                m_hSocket = null;
                m_szStateInfo = "未连接";
            }
            return null;
        }

        /// <summary>
        /// 启动通讯线程，对于发送命令的实例对象，不应调用此方法
        /// </summary>
        /// <returns></returns>
        public bool StartUse()
        {
            if (m_bOpened == false)
            {
                return false;
            }

            m_bRunning = true;
            m_hThread = new System.Threading.Thread(new System.Threading.ThreadStart(DeviceThread));
            m_hThread.Start();

            return true;
        }

        /// <summary>
        /// 数据采集线程
        /// </summary>
        private void DeviceThread()
        {
            try
            {
                while (m_bRunning)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(100);

                        m_szData = RtuCommand(1, 1, 0x50, 0x70, 0, 16);
                    }
                    catch (System.Exception error)
                    {
                        WriteLog(m_szDeviceName + ")Rtu错误：" + error.Message);
                        m_bOpened = false;
                        m_hSocket.Close();
                        m_hSocket = null;
                        m_szStateInfo = "未连接";
                    }
                }

                m_bOpened = false;

                //关闭连接
                try
                {
                    m_hSocket.Close();

                    m_szStateInfo = "close";
                    WriteLog(m_szDeviceName + ")Rtu正常关闭");
                }
                catch (System.Exception error)
                {
                    WriteLog(m_szDeviceName + ")关闭Rtu时发生错误：" + error.Message);
                }
            }
            catch (System.Exception exp)
            {
                WriteLog(m_szDeviceName + ")错误：" + exp.Message);
            }
        }

        /// <summary>
        /// 获取Rtu采集到的数据，数据来自数据采集线程
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            return m_szData;
        }

        /// <summary>
        /// 停止使用，线程终止
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

            m_bOpened = false;
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

                System.IO.TextWriter tw = new System.IO.StreamWriter(m_szRunPath + "\\log\\" + DeviceName + "-" + strDate + "_socket.log", true);

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

        #region Properties

        /// <summary>
        /// Rtu IP地址
        /// </summary>
        public string RtuIP
        {
            get
            {
                return m_RtuIP;
            }
            set
            {
                m_RtuIP = value;
            }
        }

        /// <summary>
        /// Rtu通讯端口
        /// </summary>
        public int RtuPort
        {
            get
            {
                return m_RtuPort;
            }
            set
            {
                m_RtuPort = value;
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
        #endregion


    }
}
