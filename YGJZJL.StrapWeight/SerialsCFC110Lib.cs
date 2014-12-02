using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SerialsComLib;
using System.Windows.Forms;
using YGJZJL.PublicComponent;

namespace YGJZJL.StrapWeight
{
    class SerialsCFC110Lib
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
        //private readonly int MaxInfoLen = 256;

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
        /// CFC100仪表的构造函数
        /// </summary>
        /// <param name="portName">CFC100仪表连接的串口名称</param>
        /// <param name="baudRate">波特率</param>
        public SerialsCFC110Lib(string portNum, int baudRate, byte byteSize, string parity, byte stopBits)
        {
            this._portName = portNum;
            _devComm = new SerialsComLib.SerialsComLib(portNum, baudRate, byteSize, parity, stopBits);
        }

        /// <summary>
        /// CFC100仪表的构造函数
        /// </summary>
        /// <param name="devComm">通讯对象</param>
        public SerialsCFC110Lib(SerialsComLib.SerialsComLib devComm)
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

        #region CFC110仪表接收事件
        public event Cfc110NumReceivedEvent CfcNumReceived;
        #endregion

        #region 公共方法
        /// <summary>
        /// 启动CFC110皮带读数
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
        /// 停止CFC110皮带读数
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
        /// 发送消息
        /// </summary>
        private void SendMsg(string msgType)
        {
            byte[] msgTL = new byte[17] { 0x2A, 0x05, 0x30, 0x31, 0x30, 0x31, 0x30, 0x31, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x45, 0x0E };
                                      
            byte[] msgMV = new byte[17] { 0x2A, 0x05, 0x30, 0x31, 0x30, 0x33, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x44, 0x0E };
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
            double Cfc110TLNum = 0;
            double Cfc110MVNum = 0;
            
           
            while (_isReading)
            {
                try
                {
                    this._devComm.ClearComBuffer(_CommHandle);
                    // 读累计量
                    SendMsg("TL");
                 //   byte[] d = new byte[10];
                   
                    byte[] d = this._devComm.Read(_CommHandle, 19);
                    string result = "";
                    int resultTL=0;
                   // Byte[] TLNum = new Byte[9];
                    if (d.Length == 19)
                    {
                        ////检查LRC校验码
                        //byte[] LrcResult = new byte[] { 0 };
                        //LrcResult = this._devComm.LrcCheck(d);
                        //if (d[d.Length - 4] == LrcResult[0] && d[d.Length - 3] == LrcResult[1])
                        //{
                            for (int i = 0; i < 8; i++)
                            {
                                if (d[i + 8].ToString().Length == 1)
                                    result += "0" + Convert.ToString(d[i + 8], 10);
                                else
                                    result += Convert.ToString(d[i + 8], 16);
                            }

                            //解析成BST-100仪表数据
                            // string Cfc110ResultStr = "";
                            try
                            {
                                string strTemp = "";
                                string strTemp1 = "";
                                for (int i = 0; i < 8; i++)
                                {
                                    strTemp1 = (Convert.ToInt32(result.Substring(i * 2, 2)) - 30).ToString();
                                    switch (strTemp1)//2014.02.21 吴锐家增
                                    {
                                        case "11":
                                            strTemp1 = "A";
                                            break;
                                        case "12":
                                            strTemp1 = "B";
                                            break;
                                        case "13":
                                            strTemp1 = "C";
                                            break;
                                        case "14":
                                            strTemp1 = "D";
                                            break;
                                        case "15":
                                            strTemp1 = "E";
                                            break;
                                        case "16":
                                            strTemp1 = "F";
                                            break;
                                    }


                                    strTemp = strTemp + strTemp1;
                                }
                                resultTL = Convert.ToInt32(strTemp, 16);
                                Cfc110TLNum =Convert.ToDouble( resultTL)/1000;
                            }
                            catch (Exception ex)
                            {
                                WriteLog("读累计量" + ex.Message);
                            }
                        }
                        

                    //}
                    this._devComm.ClearComBuffer(_CommHandle);
                    // 读瞬时量
                    SendMsg("PV");
                    string reulstPV="";
                    int ResultPV = 0;
                  
                    byte[] PV = this._devComm.Read(_CommHandle, 19);
                    if (PV.Length == 19)
                    {
                     
                            for (int i = 0; i < 8; i++)
                            {
                                if (PV[i + 8].ToString().Length == 1)
                                    reulstPV += "0" + Convert.ToString(PV[i + 8], 16);
                                else
                                    reulstPV += Convert.ToString(PV[i + 8], 16);
                            }

                            //解析成CFC110仪表数据
                            // string Cfc110ResultStr = "";
                            try
                            {
                                string strTemp = "";
                                string strTemp1 = "";
                                for (int i = 0; i < 8; i++)
                                {
                                    strTemp1 = (Convert.ToInt32(reulstPV.Substring(i * 2, 2)) - 30).ToString();
                                    switch (strTemp1)//2014.02.21 吴锐家增
                                    {
                                        case "11":
                                            strTemp1 = "A";
                                            break;
                                        case "12":
                                            strTemp1 = "B";
                                            break;
                                        case "13":
                                            strTemp1 = "C";
                                            break;
                                        case "14":
                                            strTemp1 = "D";
                                            break;
                                        case "15":
                                            strTemp1 = "E";
                                            break;
                                        case "16":
                                            strTemp1 = "F";
                                            break;
                                    }


                                    strTemp = strTemp + strTemp1;
                                }
                                ResultPV = Convert.ToInt32(strTemp, 16);
                                Cfc110MVNum =Convert.ToDouble( ResultPV)/1000 ;
                            }
                            catch (Exception ex)
                            {
                                WriteLog("读累计量" + ex.Message);
                            }

                        
                        //string Cfc110ResultStr = "";
                        //try
                        //{
                        //    Cfc110ResultStr = System.Text.Encoding.ASCII.GetString(PV);
                        //    string MvNum = (Convert.ToInt32(Cfc110ResultStr.Substring(7, 8), 16)).ToString().Remove(0, 2);
                        //    Cfc110MVNum = (Convert.ToDouble(MvNum)) / 100000.0;
                        //}
                        //catch (Exception ex)
                        //{
                        //    WriteLog("读瞬时量出错" + ex.Message);
                        //}

                    }
                    // 触发事件
                    if (CfcNumReceived != null)
                    {
                        Cfc110NumReceivedEventArgs e = new Cfc110NumReceivedEventArgs(Cfc110TLNum.ToString("0.00"), Cfc110MVNum.ToString("0.00"));
                        CfcNumReceived(null, e);
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
            System.IO.TextWriter tw = new System.IO.StreamWriter(Constant.RunPath + "\\log\\CFC-100_" + strDate + ".log", true);

            tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            tw.WriteLine(str);
            tw.WriteLine("\r\n");
            tw.Close();
        }
    }
    /// <summary>
    /// CFC100仪表接收事件模型的委托
    /// </summary>
    public delegate void Cfc110NumReceivedEvent(object sender, Cfc110NumReceivedEventArgs e);
    /// <summary>
    /// CFC100仪表接收事件参数,包含了累积量、瞬时量
    /// </summary>
    public class Cfc110NumReceivedEventArgs : EventArgs
    {
        #region 私有字段
        private string _cfcTLNum;
        private string _cfcMVNum;
        #endregion

        #region 属性
        /// <summary>
        /// 获取累计量
        /// </summary>
        public string CfcTLNum
        {
            get
            {
                return _cfcTLNum;
            }
        }
        /// <summary>
        /// 获取顺时量
        /// </summary>
        public string CfcMVNum
        {
            get
            {
                return _cfcMVNum;
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// CFC100仪表接收事件参数的构造函数
        /// </summary>
        /// <param name="bstTLNum">累积量</param>
        public Cfc110NumReceivedEventArgs(string cfcTLNum, string cfcMVNum)
        {
            this._cfcTLNum = cfcTLNum;
            this._cfcMVNum = cfcMVNum;
        }
        #endregion


    }
    
}
