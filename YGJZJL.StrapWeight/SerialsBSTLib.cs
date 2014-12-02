using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SerialsComLib;
using YGJZJL.PublicComponent;

namespace SerialsBSTLib
{
    public class SerialsBSTLib
    {
         #region 私有字段
        /// <summary>
        /// 通讯管理对象
        /// </summary>
        private SerialsComLib.SerialsComLib _devComm = null;
        /// <summary>
        /// 通讯串口句柄值
        /// </summary>
        private int _CommHandle = 0;

        /// <summary>
        /// 消息最大长度
        /// </summary>
        private readonly int MaxInfoLen = 256;

        /// <summary>
        /// 是否正在读取数据
        /// </summary>
        private bool _isReading = false;

        /// <summary>
        /// 监听端口的线程
        /// </summary>
        private Thread _threadRead = null;

        
        /// <summary>
        /// 存储解析出来的一条消息
        /// </summary>
        private List<Byte> _data = new List<Byte>();

        private string _portName = "";
        #endregion

        #region 构造函数
        /// <summary>
        /// BST-100仪表的构造函数
        /// </summary>
        /// <param name="portName">BST-100仪表连接的串口名称</param>
        /// <param name="baudRate">波特率</param>
        public SerialsBSTLib(string portNum, int baudRate, byte byteSize, string parity, byte stopBits)
        {
            this._portName = portNum;
            _devComm = new SerialsComLib.SerialsComLib(portNum, baudRate, byteSize, parity, stopBits);
        }

        /// <summary>
        /// BST-100仪表的构造函数
        /// </summary>
        /// <param name="devComm">通讯对象</param>
        public SerialsBSTLib(SerialsComLib.SerialsComLib devComm)
        {
            _devComm = devComm;
        }
        #endregion

        #region 使用的通讯端口名称
        /// <summary>
        /// 使用的通讯端口名称
        /// </summary>
        public string PortName
        {
            get { return this._portName; }
        }
        #endregion

        #region BST-100仪表接收事件
        public event BstNumReceivedEvent BstNumReceived;
        #endregion

        #region 公共方法
        /// <summary>
        /// 启动BST-100皮带读数
        /// </summary>
        public bool Start()
        {
            if (_isReading)
            {
                return true;
            }
            // 如果通讯对象没有连接,则创建连接,如果连接失败,则返回失败
            if (!_devComm.IsConnected)
                if (!this._devComm.Connect(ref _CommHandle))
                    return false;
            _isReading = true;
            _threadRead = new Thread(new ThreadStart(ReadPort));
            _threadRead.Name = PortName;
            _threadRead.Start();
            return true;
        }

        /// <summary>
        /// 停止BST-100皮带读数
        /// </summary>
        public bool Stop()
        {
            try
            {
                if (!_isReading) return true;

                _isReading = false;
                Thread.Sleep(150);// 等待100毫秒让上述线程自动运行终止
                _devComm.Close(_CommHandle);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 发送消息
        /// <summary>
        /// 发送消息线程
        /// </summary>
        private void SendMsg(string msgType)
        {
            byte[] msgTL = new byte[17] { 0x3A, 0x30, 0x31, 0x30, 0x33, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x46, 0x41, 0x0D, 0x0A };
            byte[] msgMV = new byte[17] { 0x3A, 0x30, 0x31, 0x30, 0x33, 0x30, 0x30, 0x30, 0x34, 0x30, 0x30, 0x30, 0x32, 0x46, 0x36, 0x0D, 0x0A };
            if (msgType == "TL")
            {
                this._devComm.Send(_CommHandle, msgTL);
            }
            else
            {
                this._devComm.Send(_CommHandle, msgMV);
            }
        }
        #endregion

        #region 端口监听的线程
        /// <summary>
        /// 端口监听,解析数据并触发相应事件
        /// </summary>
        public void ReadPort()
        {
            double  Bst100TLNum = 0;
            double  Bst100MVNum = 0;
            while (_isReading)
            {
                try
                {
                    // 读累计量
                    SendMsg("TL");
                    byte[] d = this._devComm.Read(_CommHandle, 19);
                    if (d.Length == 19)
                    {
                        Console.WriteLine("接收:" + System.Text.Encoding.ASCII.GetString(d));
                        //检查LRC校验码
                        byte[] LrcResult = new byte[] { 0 };
                        LrcResult = this._devComm.LrcCheck(d);
                        if (d[d.Length - 4] == LrcResult[0] && d[d.Length - 3] == LrcResult[1])
                        {
                            //解析成BST-100仪表数据
                            string Bst100ResultStr = "";
                            try
                            {
                                Bst100ResultStr = System.Text.Encoding.ASCII.GetString(d);
                                Bst100TLNum = (Convert.ToInt32(Bst100ResultStr.Substring(7, 8), 16)) / 1000.0;
                            }
                            catch (Exception ex)
                            {
                                WriteLog("读累计量" + ex.Message);
                            }
                        }

                    }

                    // 读瞬时量
                    SendMsg("PV");
                    byte[] PV = this._devComm.Read(_CommHandle, 19);
                    if (PV.Length == 19)
                    {
                        Console.WriteLine("接收:" + System.Text.Encoding.ASCII.GetString(PV));
                        //检查LRC校验码
                        byte[] LrcResult = new byte[] { 0 };
                        LrcResult = this._devComm.LrcCheck(PV);
                        if (PV[PV.Length - 4] == LrcResult[0] && PV[PV.Length - 3] == LrcResult[1])
                        {
                            //解析成BST-100仪表数据
                            string Bst100ResultStr = "";
                            try
                            {
                                Bst100ResultStr = System.Text.Encoding.ASCII.GetString(PV);
                                string MvNum = (Convert.ToInt32(Bst100ResultStr.Substring(7, 8), 16)).ToString().Remove(0, 2);
                                Bst100MVNum = (Convert.ToDouble(MvNum)) / 100000.0;
                            }
                            catch (Exception ex)
                            {
                                WriteLog("读瞬时量出错"+ex.Message);
                            }
                        }

                    }
                    // 触发事件
                    if (BstNumReceived != null)
                    {
                        BstNumReceivedEventArgs e = new BstNumReceivedEventArgs(Bst100TLNum.ToString("0.000"), Bst100MVNum.ToString("0.000"));
                        BstNumReceived(null, e);
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("端口监听的线程出错" + ex.Message);
                }
                
            }
        }
        #endregion
        private void WriteLog(string str)
        {
            if (System.IO.Directory.Exists(Constant.RunPath + "\\log") == false)
            {
                System.IO.Directory.CreateDirectory(Constant.RunPath + "\\log");
            }

            string strDate = System.DateTime.Now.ToString("yyyyMMdd");
            System.IO.TextWriter tw = new System.IO.StreamWriter(Constant.RunPath + "\\log\\BST-100_" + strDate + ".log", true);

            tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            tw.WriteLine(str);
            tw.WriteLine("\r\n");
            tw.Close();
        }
    }

    /// <summary>
    /// BST-100仪表接收事件模型的委托
    /// </summary>
    public delegate void BstNumReceivedEvent(object sender, BstNumReceivedEventArgs e);
    /// <summary>
    /// BST-100仪表接收事件参数,包含了累积量、瞬时量
    /// </summary>
    public class BstNumReceivedEventArgs : EventArgs
    {
        #region 私有字段
        private string _bstTLNum;
        private string _bstMVNum;
        #endregion

        #region 属性
        /// <summary>
        /// 获取累计量
        /// </summary>
        public string BstTLNum
        {
            get
            {
                return _bstTLNum;
            }
        }
        /// <summary>
        /// 获取顺时量
        /// </summary>
        public string BstMVNum
        {
            get
            {
                return _bstMVNum;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// BST-100仪表接收事件参数的构造函数
        /// </summary>
        /// <param name="bstTLNum">累积量</param>
        public BstNumReceivedEventArgs(string bstTLNum, string bstMVNum)
        {
            this._bstTLNum = bstTLNum;
            this._bstMVNum = bstMVNum;
        }
        #endregion
    }
}
