using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YGJZJL.CarSip.Client.App;

namespace YGJZJL.CarSip.Client.Meas
{
    public class IcCard : CoreDevice, IDevice
    {
        private UdpClient[] SendClient = new UdpClient[20];
        private IPEndPoint ep = null;
        private IPEndPoint SendPoint;
        private UdpClient udpClient;
        private byte[] buffer;
        private int datalen;
        private int m_iSelectedPound;

        #region <类声明>
        enum Mode
        {
            IDLE = 0,
            ALL = 1,
            HL = 2
        }
        /// <summary>
        /// 读卡器类型
        /// </summary>
        protected enum ReaderType
        {
            COM = 0, NET = 1
        }
        #endregion

        #region <成员变量>
        protected ReaderType _readerType = ReaderType.COM;//读卡器类型
        Int16 _port = 0;       //串口编号
        int _baud = 115200;    //波特率
        int _icdev = -1;      //设备句柄
        uint _snr = 0;        //卡序列号
        string _ip = "";      //读卡器IP


        string _guid;         //全球唯一序列号
        string[] _data;       //数据
        static byte[] _key = new byte[6] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };          // 密码
        // byte[] _key2;       // MF卡密码
        byte[] _buff32;        // 缓冲区
        byte[] _buff16;

        #endregion

        #region <属性>
        public Int16 Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public string PortName
        {
            get { return "COM" + (_port + 1).ToString(); }
            set
            {
                _port = Int16.Parse(value.Substring(3));
                _port -= 1;
            }
        }
        public int BaudRate
        {
            get { return _baud; }
            set { _baud = value; }
        }
        public uint Snr
        {
            get { return _snr; }
            set { _snr = value; }
        }
        public string ID
        {
            get { return _guid; }
            set { _guid = value; }
        }

        #endregion

        #region <构造函数>
        public IcCard()
        {
            _key = new byte[6] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            _buff16 = new byte[16];
            _icdev = -1;
            //  SendClient.Close(); SendClient = null;
            //CoreApp core = new CoreApp();

            //if (SendClient == null)
            //{
            //    SendClient = new UdpClient(1000);
            //}
        }
        #endregion

        #region <公共方法>
        public bool Init(string configParam, int m_iSelectedPound)
        {
            if (configParam.ToUpper().StartsWith("COM"))
            {
                string[] strParam = configParam.Split(new char[] { ',' });
                PortName = strParam[0];
                if (strParam.LongLength > 1) BaudRate = Convert.ToInt32(strParam[1]);
                _readerType = ReaderType.COM;
            }
            else
            {
                _ip = configParam;
                _readerType = ReaderType.NET;
                this.m_iSelectedPound = m_iSelectedPound;
            }
            return true;
        }

        #region <公共方法>
        public bool Init(string configParam)
        {
            if (configParam.ToUpper().StartsWith("COM"))
            {
                string[] strParam = configParam.Split(new char[] { ',' });
                PortName = strParam[0];
                if (strParam.LongLength > 1) BaudRate = Convert.ToInt32(strParam[1]);
                _readerType = ReaderType.COM;
            }
            else
            {
                _ip = configParam;
                _readerType = ReaderType.NET;
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            byte[] status = new byte[30];
            byte[] ver = new byte[30];
            int ret = -1;
            //string hexKey = "ffffffffffff";
            //byte[] bytesKey = new byte[17];            
            switch (_readerType)
            {
                case ReaderType.COM:
                    _icdev = MwRfSDK.rf_init(_port, _baud);
                    ret = _icdev;
                    if (ret < 0) return false;
                    // 加载Key
                    for (int sector = 0; sector < 16; sector++)
                    {
                        ret = MwRfSDK.rf_load_key(_icdev, (byte)Mode.IDLE, sector, _key);
                        if (ret != 0) return false;
                    }
                    break;
                case ReaderType.NET:
                    //MwRfSDKNet.rf_exit(_icdev);

                    //_icdev = MwRfSDKNet.rf_init(Encoding.ASCII.GetBytes(_ip), 13);
                    //ret = _icdev;
                    //Int16 st = MwRfSDKNet.rf_get_status(_icdev, status);
                    //if (_icdev >= 0 && st == 0)
                    //{
                    //    st = MwRfSDKNet.rf_lib_ver(ver);
                    //    // 加载Key
                    //    for (int sector = 0; sector < 16; sector++)
                    //    {
                    //        ret = MwRfSDKNet.rf_load_key(_icdev, (byte)Mode.IDLE, sector, _key);
                    //        if (ret != 0) return false;
                    //    }
                    //}
                    //else
                    //    return false;
                    opennetreadcard();
                    break;
            }
            Status = DeviceStatus.OPEN;
            return true;
        }
        /// <summary>
        /// 关闭通讯口
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            int ret = -1;
            switch (_readerType)
            {
                case ReaderType.COM:
                    ret = MwRfSDK.rf_exit(_icdev);
                    _icdev = 0;
                    if (ret < 0) return false;
                    break;
                case ReaderType.NET:
                    //ret = MwRfSDKNet.rf_exit(_icdev);
                    //_icdev = 0;
                    //if (ret < 0) return false;
                    //if (SendClient != null) { SendClient[this.m_iSelectedPound].Close(); SendClient = null; }

                    if (SendClient != null)
                    {
                        CloseCard();
                        SendClient[this.m_iSelectedPound].Close();
                        SendClient[this.m_iSelectedPound] = null;
                    }
                    //if (SendClient != null) { SendClient[this.m_iSelectedPound].Close(); }



                    break;
            }
            Status = DeviceStatus.CLOSE;

            return true;
        }

        public bool StopCard()
        {
            int ret = -1;
            switch (_readerType)
            {
                case ReaderType.COM:
                    ret = MwRfSDK.rf_halt(_icdev);
                    if (ret != 0) return false;
                    break;
                case ReaderType.NET:
                    //ret = MwRfSDKNet.rf_halt(_icdev);
                    //if (ret != 0) return false;
                    break;
            }
            return true;
        }

        public void Beep(int msec)
        {
            switch (_readerType)
            {
                case ReaderType.COM:
                    MwRfSDK.rf_beep(_icdev, msec);
                    break;
                case ReaderType.NET:
                    //MwRfSDKNet.rf_beep(_icdev, msec);
                    Beep();
                    break;
            }
        }

        public bool SelectCard()
        {
            int ret = -1;
            byte size = 0x00;
            switch (_readerType)
            {
                case ReaderType.COM:
                    ret = MwRfSDK.rf_select(_icdev, _snr, out size);
                    if (ret != 0) return false;
                    break;
                case ReaderType.NET:
                    //ret = MwRfSDKNet.rf_select(_icdev, _snr, out size);
                    //if (ret != 0) return false;
                    break;
            }
            return true;
        }

        public string InitCard()
        {
            _snr = 0;
            _guid = "";

            int ret = -1;
            //string strTemp = "";
            switch (_readerType)
            {
                case ReaderType.COM:
                    ret = MwRfSDK.rf_reset(_icdev, 3);
                    if (ret != 0)
                    {
                        _snr = 0;
                        _guid = "";
                        return "";
                    }
                    ret = MwRfSDK.rf_card(_icdev, (byte)Mode.IDLE, ref _snr);
                    if (ret != 0)
                    {
                        _snr = 0;
                        _guid = "";
                        return "";
                    }
                    _guid = _snr.ToString("X");
                    break;
                case ReaderType.NET:
                    //ret = MwRfSDKNet.rf_reset(_icdev, 3);
                    //if (ret != 0)
                    //{
                    //    _snr = 0;
                    //    _guid = "";
                    //    return "";
                    //}
                    //ret = MwRfSDKNet.rf_card(_icdev, (byte)Mode.IDLE, ref _snr);
                    //if (ret != 0)
                    //{
                    //    _snr = 0;
                    //    _guid = "";
                    //    return "";
                    //}
                    byte[] tempbuff = null;
                    int datalen = 0;


                    try
                    {

                        cardOfTesting();
                        Thread.Sleep(100);
                        ep = null;
                        tempbuff = SendClient[this.m_iSelectedPound].Receive(ref ep);
                        Thread.Sleep(100);
                        //Thread.Sleep(50);
                        //cardOfTesting();

                        //Thread.Sleep(50);
                        //  tempbuff = this.ReciveMessage();

                        Thread.Sleep(50);
                        this.CloseCard();
                        ep = null;
                        byte[] buff = SendClient[this.m_iSelectedPound].Receive(ref ep);

                        Thread.Sleep(50);
                        // CloseCard();



                    }
                    catch (Exception ex)
                    {
                        //  CloseCard();

                    }
                    if (tempbuff != null)
                        datalen = tempbuff.Length;
                    if (datalen > 0)
                    {
                        if ((tempbuff[0] == 0xbb) & (tempbuff[1] == 0xff))
                        {
                            switch (tempbuff[2])
                            {
                                case 0x70:
                                    ret = 0;
                                    for (int j = 3; j < datalen; j++)
                                    {
                                        tempbuff[j - 3] = tempbuff[j];
                                    }
                                    //_guid = Encoding.Default.GetString(tempbuff);
                                    //_snr = Convert.ToUInt32(_guid);

                                    for (int i = 0; i < tempbuff.Length; i++)
                                        _guid += funBtoHex(tempbuff[i]);
                                    for (int i = 0; i < tempbuff.Length; i++)
                                        _snr += tempbuff[i];

                                    //_snr = Convert.ToUInt32(_guid);
                                    break;
                                default:
                                    ret = 1;
                                    if (ret != 0)
                                    {
                                        _snr = 0;
                                        _guid = "";
                                        return "";
                                    }
                                    break;
                            }
                        }
                    }

                    break;
            }
            return _guid;
        }
        public void opennetreadcard()
        {
            //byte[] tempbuff = null;
            //int datalen = 0;
            //string MyPcIp = "10.32.2.235";//计算机的IP
            SendClient[this.m_iSelectedPound] = new UdpClient(1000 + this.m_iSelectedPound);
            int MACPort = Convert.ToInt32("1234");//读卡器的端口
            string MACIp = _ip;//读卡器的IP

            try
            {

                //udpClient = new UdpClient(Convert.ToInt32("1234"));//计算机端口

                SendPoint = new IPEndPoint(IPAddress.Parse(MACIp), MACPort);

            }
            catch (Exception ex)
            {


            }
        }
        public bool Auth(int sector)
        {
            int ret = -1;
            switch (_readerType)
            {
                case ReaderType.COM:
                    ret = MwRfSDK.rf_authentication(_icdev, (byte)Mode.IDLE, sector);
                    if (ret != 0)
                    {
                        return false;
                    }
                    break;
                case ReaderType.NET:
                    //ret = MwRfSDKNet.rf_authentication(_icdev, (byte)Mode.IDLE, sector);
                    //if (ret != 0)
                    //{
                    //    return false;
                    //}
                    break;
            }
            return true;
        }

        /// <summary>
        /// 读IC卡数据，此方法适用于不启用线程时读卡用，
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="block">块号</param>
        /// <returns>读到的内容</returns>
        public string ReadData(int sector, int block)
        {
            int ret = -1;
            //InitCard(sector);
            if (!Auth(sector)) return "";
            return ReadSectorData(sector, block);
        }

        /// <summary>
        /// 连续读取该扇区的数据
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="block">块号</param>
        /// <returns>读到的内容</returns>
        public string ReadSectorData(int sector, int block)
        {
            byte[] resultBuff = null;
            byte[] buff = new byte[16];
            int ret = -1;
            switch (_readerType)
            {
                case ReaderType.COM:
                    ret = MwRfSDK.rf_read(_icdev, 4 * sector + block, buff);
                    if (ret != 0) return "";
                    break;
                case ReaderType.NET:
                    //ret = MwRfSDKNet.rf_read(_icdev, 4 * sector + block, buff);
                    resultBuff = Read(4 * sector + block);
                    datalen = resultBuff.Length;
                    if (datalen > 0)
                    {
                        if ((resultBuff[0] == 0xbb) & (resultBuff[1] == 0xff))
                        {
                            switch (resultBuff[2])
                            {
                                case 0x10:
                                    ret = 0;
                                    for (int j = 3; j < datalen; j++)
                                    {
                                        buff[j - 3] = resultBuff[j];
                                    }
                                    break;
                                default:
                                    ret = 1;
                                    break;
                            }
                        }
                    }
                    if (ret != 0) return "";
                    break;
            }
            string data = Encoding.Default.GetString(buff);
            return data;
        }

        /// <summary>
        /// 读IC卡数据，此方法适用于不启用线程时读卡用，
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <returns>读到的内容</returns>
        public string ReadData(int sector)
        {
            byte[] resultBuff = null;
            string data = "";
            int ret = -1;
            byte[] buff = new byte[16];
            //InitCard(sector);
            if (!Auth(sector)) return "";
            switch (_readerType)
            {
                case ReaderType.COM:
                    for (int block = 0; block < 3; block++)
                    {
                        for (int j = 0; j < buff.Length; j++) buff[j] = 0x00;
                        ret = MwRfSDK.rf_read(_icdev, 4 * sector + block, buff);
                        data += Encoding.Default.GetString(buff);
                        if (buff[15] == 0x00) break;
                    }
                    break;
                case ReaderType.NET:
                    for (int block = 0; block < 3; block++)
                    {
                        for (int j = 0; j < buff.Length; j++) buff[j] = 0x00;
                        //ret = MwRfSDKNet.rf_read(_icdev, 4 * sector + block, buff);
                        resultBuff = Read(4 * sector + block);
                        datalen = resultBuff.Length;
                        if (datalen > 0)
                        {
                            if ((resultBuff[0] == 0xbb) & (resultBuff[1] == 0xff))
                            {
                                switch (resultBuff[2])
                                {
                                    case 0x10:
                                        ret = 0;
                                        for (int j = 3; j < datalen; j++)
                                        {
                                            buff[j - 3] = resultBuff[j];
                                        }
                                        break;
                                    default:
                                        ret = 1;
                                        break;
                                }
                            }
                        }
                        data += Encoding.Default.GetString(buff);
                        if (buff[15] == 0x00) break;
                    }
                    break;
            }
            return data;
        }
        /// <summary>
        /// 在指定块区写数据
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="block">块号</param>
        /// <param name="strData">写入的内容</param>
        /// <returns></returns>
        public bool WriteData(int sector, int block, string strData)
        {
            //InitCard();
            bool ret = false;
            //_mutex.WaitOne();
            // ret = Auth(sector);
            //   if (ret)
            //_mutex.ReleaseMutex();
            return WriteSectorData(sector, block, strData); ;
        }
        /// <summary>
        /// 在指定块区连续写数据
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="block">块号</param>
        /// <param name="strData">写入的内容</param>
        /// <returns></returns>
        public bool WriteSectorData(int sector, int block, string strData)
        {
            int ret = -1;
            byte[] resultBuff = null;
            //_mutex.WaitOne();
            byte[] buff = Encoding.Default.GetBytes(strData);
            if (buff.Length > 16) { return false; }
            for (int i = 0; i < _buff16.Length; i++)
            {
                if (i < buff.Length) _buff16[i] = buff[i];
                else _buff16[i] = 0x00;
            }

            switch (_readerType)
            {
                case ReaderType.COM:
                    ret = MwRfSDK.rf_write(_icdev, sector * 4 + block, _buff16);
                    break;
                case ReaderType.NET:
                    //ret = MwRfSDKNet.rf_write(_icdev, sector * 4 + block, _buff16);
                    resultBuff = Write(4 * sector + block, _buff16);
                    datalen = resultBuff.Length;
                    if (datalen > 0)
                    {
                        if ((resultBuff[0] == 0xbb) & (resultBuff[1] == 0xff))
                        {
                            switch (resultBuff[2])
                            {
                                case 0xAF:
                                    ret = 0;
                                    break;
                                default:
                                    ret = 1;
                                    break;
                            }
                        }
                    }
                    break;
            }
            //_mutex.ReleaseMutex();
            if (ret != 0) return false;
            return true;
        }
        /// <summary>
        /// 连续写整个扇区
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="strData">写入的内容</param>
        /// <returns></returns>
        public bool WriteData(int sector, string data)
        {
            int datalen;
            byte[] resultBuff = null;
            byte[] bytesData = Encoding.Default.GetBytes(data);
            byte[] buff = new byte[16];
            int i = 0, ret = -1;
            bool completed = false;
            //InitCard();
            if (!Auth(sector)) return false;
            switch (_readerType)
            {
                case ReaderType.COM:
                    for (int block = 0; block < 3; block++)
                    {
                        for (int j = 0; j < buff.Length; j++)
                        {
                            i = block * 16 + j;
                            if (i < bytesData.Length)
                            {
                                buff[j] = bytesData[i];
                            }
                            else
                            {
                                buff[j] = 0x00;
                                completed = true;
                            }
                        }
                        ret = MwRfSDK.rf_write(_icdev, 4 * sector + block, buff);
                        if (ret != 0) return false;
                        if (completed) break;
                    }
                    break;
                case ReaderType.NET:
                    for (int block = 0; block < 3; block++)
                    {
                        for (int j = 0; j < buff.Length; j++)
                        {
                            i = block * 16 + j;
                            if (i < bytesData.Length)
                            {
                                buff[j] = bytesData[i];
                            }
                            else
                            {
                                buff[j] = 0x00;
                                completed = true;
                            }
                        }

                        //ret = MwRfSDKNet.rf_write(_icdev, 4 * sector + block, buff);
                        resultBuff = Write(4 * sector + block, buff);
                        datalen = resultBuff.Length;
                        if (datalen > 0)
                        {
                            if ((resultBuff[0] == 0xbb) & (resultBuff[1] == 0xff))
                            {
                                switch (resultBuff[2])
                                {
                                    case 0xAF:
                                        ret = 0;
                                        break;
                                    default:
                                        ret = 1;
                                        break;
                                }
                            }
                        }
                        if (ret != 0) return false;
                        if (completed) break;
                    }
                    break;
            }

            return true;
        }
        //清除卡中的数据
        public void ClearCard()
        {
            _snr = 0;
            StopCard();
        }

        #endregion

        public byte[] Read(int blocknr)
        {
            byte[] tempBuff = null;
            int datalen;
            String strGetMessage = "";
            byte[] buffer1 = new Byte[20];
            byte[] buffer2 = null;

            try
            {
                cardOfTesting();
                Thread.Sleep(100);
                ep = null;
                byte[] buffer = SendClient[this.m_iSelectedPound].Receive(ref ep);
                Thread.Sleep(100);
                datalen = buffer.Length;
                if (datalen > 0)
                {
                    strGetMessage = "";
                    //for (int i = 0; i < datalen; i++)
                    //    strGetMessage = strGetMessage + funBtoHex(buffer[i]);

                    if ((buffer[0] == 0xbb) & (buffer[1] == 0xff))
                    {

                        strGetMessage = "接收" + ep.ToString() + "刷卡:" + strGetMessage;
                        // this.Invoke(new MethodInvoker(DisplayMessage));
                        //读卡
                        this.funMReadOne1(blocknr);
                        Thread.Sleep(100);
                        ep = null;
                        buffer1 = SendClient[this.m_iSelectedPound].Receive(ref ep);
                        Thread.Sleep(100);
                        datalen = 0;
                        datalen = buffer1.Length;
                        if (datalen > 0)
                        {
                            strGetMessage = "";


                            if ((buffer1[0] == 0xbb) & (buffer1[1] == 0xff))
                            {
                                if (buffer1[2] == 0x10)
                                {
                                    strGetMessage = "读成功，接收" + ep.ToString() + ":" + strGetMessage;


                                    // this.Beep();
                                    // buffer2 = SendClient.Receive(ref ep);
                                    Thread.Sleep(100);
                                    CloseCard();


                                    ep = null;
                                    byte[] buffer3 = SendClient[this.m_iSelectedPound].Receive(ref ep);
                                    Thread.Sleep(100);
                                    datalen = 0;
                                    datalen = buffer3.Length;
                                    if (datalen > 0)
                                    {

                                        if ((buffer3[0] == 0xbb) & (buffer3[1] == 0xff))
                                        {
                                            if (buffer3[2] == 0x40)
                                            {
                                                strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                                                //this.Invoke(new MethodInvoker(DisplayMessage));
                                            }
                                            //else
                                            //{
                                            //    Thread.Sleep(200);
                                            //    CloseCard();
                                            //    Thread.Sleep(200);
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                                        //   this.Invoke(new MethodInvoker(DisplayMessage));
                                    }

                                }
                            }
                            //else if (buffer[2] == 0xA1 || buffer[2] == 0xA0)
                            //{
                            //    Thread.Sleep(200);
                            //    CloseCard();
                            //    Thread.Sleep(200);
                            //    ep = null;

                            //    buffer = SendClient.Receive(ref ep);
                            //    Thread.Sleep(200);
                            //}
                        }

                    }
                    else
                    {
                        strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                        // this.Invoke(new MethodInvoker(DisplayMessage));
                    }

                    //}
                }
                else
                {
                    strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                    // this.Invoke(new MethodInvoker(DisplayMessage));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return buffer1;
        }
        public byte[] Write(int blocknr, byte[] datebuff)
        {
            byte[] tempbuff = null;
            int datalen;
            String strGetMessage = "";
            byte[] buffer = null;

            try
            {
                cardOfTesting();
                Thread.Sleep(100);
                ep = null;

                buffer = SendClient[this.m_iSelectedPound].Receive(ref ep);
                datalen = buffer.Length;
                if (datalen > 0)
                {

                    if ((buffer[0] == 0xbb) & (buffer[1] == 0xff))
                    {

                        strGetMessage = "接收" + ep.ToString() + "刷卡:" + strGetMessage;
                        //this.Invoke(new MethodInvoker(DisplayMessage));
                        this.funMWriteOne1(blocknr, datebuff);
                        Thread.Sleep(100);
                        ep = null;
                        buffer = SendClient[this.m_iSelectedPound].Receive(ref ep);
                        datalen = 0;
                        datalen = buffer.Length;
                        if (datalen > 0)
                        {
                            if ((buffer[0] == 0xbb) & (buffer[1] == 0xff))
                            {
                                if (buffer[2] == 0xAF)
                                {
                                    strGetMessage = "写入成功，接收" + ep.ToString() + ":" + strGetMessage;
                                    //this.Invoke(new MethodInvoker(DisplayMessage));
                                    //this.Beep();
                                    Thread.Sleep(100);
                                    // ep = null;
                                    //byte[] buffer1 = SendClient.Receive(ref ep);
                                    CloseCard();
                                    Thread.Sleep(100);
                                    ep = null;
                                    byte[] buffer2 = SendClient[this.m_iSelectedPound].Receive(ref ep);
                                    datalen = 0;
                                    datalen = buffer2.Length;
                                    if (datalen > 0)
                                    {

                                        if ((buffer2[0] == 0xbb) & (buffer2[1] == 0xff))
                                        {
                                            if (buffer2[2] == 0x40)
                                            {
                                                strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                                                //    this.Invoke(new MethodInvoker(DisplayMessage));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                                        // this.Invoke(new MethodInvoker(DisplayMessage));
                                    }

                                }
                            }

                        }
                        else
                        {
                            strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                            // this.Invoke(new MethodInvoker(DisplayMessage));
                        }

                    }
                }
                else
                {
                    strGetMessage = "接收" + ep.ToString() + ":" + strGetMessage;
                    //this.Invoke(new MethodInvoker(DisplayMessage));
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return buffer;
        }
        /// <summary>
        /// 关卡
        /// </summary>
        private void CloseCard()
        {
            try
            {
                byte[] data = new byte[3];
                data[0] = 0xaa;
                data[1] = 0xff;
                data[2] = 0x40;
                SendClient[this.m_iSelectedPound].Send(data, 3, SendPoint);

                //int i;
                //string strRead = "发送关卡" + SendPoint + ":";
                //for (i = 0; i < 3; i++)
                //{
                //    strRead = strRead + funBtoHex(data[i]);
                //}
                //strMessage = strRead;
                //this.Invoke(new MethodInvoker(DisplayReceiveMessage));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
        }
        /// <summary>
        /// 接受返回在消息
        /// </summary>
        public byte[] ReciveMessage()
        {
            // int datalen;
            ep = null;


            buffer = udpClient.Receive(ref ep);




            //datalen = buffer.Length;

            //if (datalen > 0)
            //{
            //    //strGetMessage = "";
            //    //for (int i = 0; i < datalen; i++)
            //    //    strGetMessage = strGetMessage + funBtoHex(buffer[i]);
            //    //strGetMessage = "接收" + ep.ToString() + "刷卡:" + strGetMessage;
            //    //this.Invoke(new MethodInvoker(DisplayMessage));
            //    return ep.ToString();
            //}
            return buffer;
        }
        /// <summary>
        ///蜂鸣
        /// </summary>
        public void Beep()
        {
            byte[] tempbuff = null;
            int datalen;


            try
            {



                //cardOfTesting();
                //Thread.Sleep(500);

                //this.ReciveMessage();

                byte[] data = new byte[11];
                data[0] = 0xaa;
                data[1] = 0xff;
                data[2] = 2;
                data[3] = Convert.ToByte("3");
                data[4] = Convert.ToByte("3");
                data[5] = Convert.ToByte("3");
                data[6] = Convert.ToByte("3");
                data[7] = Convert.ToByte("3");
                data[8] = Convert.ToByte("3");
                data[9] = Convert.ToByte("3");
                data[10] = Convert.ToByte("3");

                SendClient[this.m_iSelectedPound].Send(data, 11, SendPoint);
                ep = null;
                byte[] buffer = SendClient[this.m_iSelectedPound].Receive(ref ep);
                //int i;
                //string strRead = "发送" + SendPoint + ":";
                //for (i = 0; i < 11; i++)
                //{
                //    strRead = strRead + funBtoHex(data[i]);
                //}
                //strMessage = strRead;
                //this.Invoke(new MethodInvoker(DisplayReceiveMessage));

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
        }
        /// <summary>
        /// 检测卡
        /// </summary>
        public void cardOfTesting()
        {
            try
            {
                byte[] data = new byte[4];
                data[0] = 0xaa;
                data[1] = 0xff;
                data[2] = 0x70;
                //if (radioButton3.Checked == true)
                data[3] = 0x52;
                // else
                //   data[3] = 0x26;
                SendClient[this.m_iSelectedPound].Send(data, 4, SendPoint);

                //int i;
                //string strRead = "发送检测卡" + SendPoint + ":";
                //for (i = 0; i < 4; i++)
                //{
                //    strRead = strRead + funBtoHex(data[i]);
                //}
                //strMessage = strRead;
                //this.Invoke(new MethodInvoker(DisplayReceiveMessage));
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
        }
        private void funMReadOne1(int blocknr)
        {
            byte passAB;
            string strPass;
            byte b_Block;
            int nowPosit = 0;
            string strMid;
            byte[] b_Pass = new byte[] { 0, 0, 0, 0, 0, 0 };

            b_Block = Convert.ToByte(blocknr.ToString(), 10);//块号
            //if (radioPassOne1.Checked)
            passAB = 0x60;
            //else
            //    passAB = 0x61;
            strPass = "FFFFFFFFFFFF";//密码

            nowPosit = 0;
            for (int i = 0; i < 12; i += 2)
            {
                strMid = strPass.Substring(i, 2);
                b_Pass[nowPosit] = Convert.ToByte(strMid, 16);
                nowPosit = nowPosit + 1;
            }
            funReadOne1(b_Block, passAB, ref b_Pass, true);

        }
        private void funReadOne1(byte b_Block, byte passAB, ref byte[] b_Pass, bool isShow)
        {
            byte[] outData = new byte[11];//发送数据
            int i;

            outData[0] = 0xAA;
            outData[1] = 0XFF;
            outData[2] = 0X10;
            outData[3] = b_Block;
            outData[4] = passAB;
            for (i = 0; i < 6; i++)
            {
                outData[5 + i] = b_Pass[i];

            }
            try
            {
                // priType = 0;
                SendClient[this.m_iSelectedPound].Send(outData, 11, SendPoint);
                //string strRead = "发送读卡" + SendPoint + ":";
                //if (isShow)
                //{
                //    for (i = 0; i < 11; i++)
                //    {
                //        strRead = strRead + funBtoHex(outData[i]);
                //    }
                //    strMessage = strRead;
                //    this.Invoke(new MethodInvoker(DisplayReceiveMessage));
                //}
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }

        }
        public void funWriteOne1(byte b_Block, byte passAB, ref byte[] b_Pass, ref byte[] b_WData, bool isShow)
        {//写一块读卡方式1
            byte[] outData = new byte[27];//发送数据
            int i;

            outData[0] = 0xAA;
            outData[1] = 0XFF;
            outData[2] = 0X20;
            outData[3] = b_Block;
            outData[4] = passAB;
            for (i = 0; i < 6; i++)
            {
                outData[5 + i] = b_Pass[i];

            }
            for (i = 0; i < 16; i++)
            {
                outData[11 + i] = b_WData[i];

            }
            try
            {
                SendClient[this.m_iSelectedPound].Send(outData, 27, SendPoint);
                //if (isShow)
                //{
                //    string strRead = "发送写卡" + SendPoint + ":";
                //    for (i = 0; i < 27; i++)
                //    {
                //        strRead = strRead + funBtoHex(outData[i]);
                //    }
                //    strMessage = strRead;
                //    this.Invoke(new MethodInvoker(DisplayReceiveMessage));
                //}
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
        }
        private void funMWriteOne1(int blocknr, byte[] datebuff)
        {
            int i;
            byte passAB;
            string strPass;
            string strWData;
            byte b_Block;
            int nowPosit = 0;
            string strMid;
            byte[] b_Pass = new byte[] { 0, 0, 0, 0, 0, 0 };
            byte[] b_WData = new byte[16];

            b_Block = Convert.ToByte(blocknr.ToString(), 10);
            //if (radioPassOne1.Checked)
            passAB = 0x60;
            //else
            //    passAB = 0x61;
            strPass = "FFFFFFFFFFFF";

            nowPosit = 0;
            for (i = 0; i < 12; i += 2)
            {
                strMid = strPass.Substring(i, 2);
                b_Pass[nowPosit] = Convert.ToByte(strMid, 16);
                nowPosit = nowPosit + 1;
            }
            //strWData = "";//写入内容
            //nowPosit = 0;
            //for (i = 0; i < 32; i += 2)
            //{
            //    strMid = strWData.Substring(i, 2);
            //    b_WData[nowPosit] = Convert.ToByte(strMid, 16);
            //    nowPosit = nowPosit + 1;
            //}

            //funWriteOne1(b_Block, passAB, ref b_Pass, ref b_WData, true);
            funWriteOne1(b_Block, passAB, ref b_Pass, ref datebuff, true);
        }
        private string funBtoHex(byte num)
        {
            string strhex;
            strhex = num.ToString("X");
            if (strhex.Length == 1)
                strhex = " 0" + strhex;
            else
                strhex = " " + strhex;
            return strhex;

        }
    }
}
