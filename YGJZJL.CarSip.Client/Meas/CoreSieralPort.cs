using System;
//using System.IO.Ports;
using System.Text;

namespace YGJZJL.CarSip.Client.Meas
{
   
    public class CoreSieralPort : CoreDevice, IDevice
    {
        #region <成员变量>
        private System.IO.Ports.SerialPort _sp = new System.IO.Ports.SerialPort();                
        #endregion

        #region <属性>
        public System.IO.Ports.SerialPort SerialPort
        {
            get { return _sp; }
        }
        
        #endregion

        #region <构造函数>
        public CoreSieralPort()
        {
            if(_sp.Equals(null) ) _sp = new System.IO.Ports.SerialPort();
            Status =  DeviceStatus.CLOSE;
        }
        #endregion

        #region <公共的方法>        
        protected System.IO.Ports.Parity ConvertParity(string str)
        {
            switch (Char.ToUpper(str[0]))
            {
                case 'E':
                    return System.IO.Ports.Parity.Even;
                case 'O':
                    return System.IO.Ports.Parity.Odd;
                case 'M':
                    return System.IO.Ports.Parity.Mark;
                case 'S':
                    return System.IO.Ports.Parity.Space;
                default:
                    return System.IO.Ports.Parity.None;
            }
            return System.IO.Ports.Parity.None;
        }

        protected System.IO.Ports.StopBits ConvertStopBit(string str)
        {
            if (str == "1") return System.IO.Ports.StopBits.One;
            if (str == "1.5") return System.IO.Ports.StopBits.OnePointFive;
            if (str == "2") return System.IO.Ports.StopBits.Two;
            return System.IO.Ports.StopBits.None;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">
        /// 串口参数，必须是以下格式：串口名,波特率,校验位,数据位,停止位
        /// 如：COM9,9600,NONE,8,1或COM2,115200,EVEN,7,1.5
        /// </param>
        public virtual void Init(string config)
        {
            string[] strtmp = config.Split(new char[] { ',' });
            _sp.PortName = strtmp[0];
            _sp.BaudRate = Convert.ToInt32(strtmp[1]);
            _sp.Parity = ConvertParity(strtmp[2]);
            _sp.DataBits = Convert.ToInt32(strtmp[3]);
            _sp.StopBits = ConvertStopBit(strtmp[4]);            
        }

        public virtual bool Open() 
        {
            try
            {
                if (_sp.IsOpen)
                {
                    this.Close();
                }
                _sp.Open();
                Status = DeviceStatus.OPEN;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public virtual bool Close()
        {
            if (_sp.IsOpen)
            {
                _sp.DiscardInBuffer();
                _sp.DiscardOutBuffer();
                System.Threading.Thread.Sleep(100);
                _sp.Close();
            }
            Status = DeviceStatus.CLOSE;
            return true;
        }
        #endregion
    }


}
