using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Sip.Client.Meas
{
    public class IcCard : CoreDevice, IDevice
    {
        #region <类声明>
        enum Mode
        {
            LOAD = 0,
            CARD= 1
        }
        #endregion

        #region <成员变量>
        Int16 _port = 0;       //串口编号
        int _baud = 115200;    //波特率
        int _icdev = -1;      //设备句柄
        uint _snr = 0;        //卡序列号
        string _guid;         //全球唯一序列号
        string[] _data;       //数据
        byte[] _key;          // 密码
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
            get { return "COM"+(_port+1).ToString(); }
            set { 
                _port = Int16.Parse(value.Substring(3)) ;
                _port -= 1;
            }
        }
        public int BaudRate
        {
            get { return _baud; }
            set { _baud = value; }
        }
        //public string CardNo
        //{
        //    get 
        //    {
        //        ReadData();
        //        return _cardNo; 
        //    }
        //    set { _cardNo = value; }
        //}
        public string ID
        {
            get { return _guid; }
            set { _guid = value; }
        }
        #endregion

        #region <构造函数>
        public IcCard()
        {

        }
        #endregion

        #region <公共方法>
       public bool Init(string configParam)
        {
            string[] strParam = configParam.Split(new char[] { ',' });
            PortName = strParam[0];
            BaudRate = Convert.ToInt32(strParam[1]);            
            return true;
        }
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            int ret = -1;
            string hexKey = "ffffffffffff";
            byte[] bytesKey = new byte[17];
           // _key2 = new byte[6] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
            _key = new byte[7];
            _buff32 = new byte[32];
            _icdev = MwRfSDK.rf_init(_port, _baud);
            ret = _icdev;
            if (ret < 0) return false;
            bytesKey = Encoding.ASCII.GetBytes(hexKey);
            MwRfSDK.a_hex(bytesKey, _key, 12);
             

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
            ret = MwRfSDK.rf_exit(_icdev);
            _icdev = 0;
            if (ret < 0) return false;
            Status = DeviceStatus.CLOSE;
            return true;
        }

        public bool StopCard()
        {
            int ret = MwRfSDK.rf_halt(_icdev);
            if (ret != 0) return false;
            return true;  
        }

        public void Beep(int msec)
        {
            MwRfSDK.rf_beep(_icdev, msec);
        }

        protected string InitCard(int sector)
        {
            int ret = -1;
            ret = MwRfSDK.rf_reset(_icdev, 3);
            ret = MwRfSDK.rf_card(_icdev, (byte)Mode.CARD, ref _snr);
            _guid = _snr.ToString("X");
            ret = MwRfSDK.rf_load_key(_icdev, (byte)Mode.LOAD, sector, _key);
            ret = MwRfSDK.rf_authentication(_icdev, (byte)Mode.LOAD, sector);
            return _guid;           
        }
        
        public string ReadData(int sector, int block)
        {
            int ret = -1;
            InitCard(sector);
            ret = MwRfSDK.rf_read_hex(_icdev, 4 * sector + block, _buff32);
            return HexToStr(System.Text.Encoding.ASCII.GetString(_buff32));
        }

        public bool WriteData(string strData, int sector, int block)
        {
            int ret = -1;
            if (strData.Length > 16) return false;    
            string strTemp = StrToHex(strData).PadLeft(32, '0');
            _buff32 = Encoding.ASCII.GetBytes(strTemp);
            InitCard(sector);
            ret = MwRfSDK.rf_write_hex(_icdev, sector * 4 + block, _buff32);
            if (ret != 0) return false;
            return true;        
        }        
        #endregion
    }
}
