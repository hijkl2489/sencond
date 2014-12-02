using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.Meas
{
    public enum DeviceStatus
    {
        INIT = 0,
        CLOSE,
        OPEN,
        IDLE,
    }
    public class CoreDevice
    {
        #region <成员变量>
        private string _deviceName;
        private string _deviceType;
        private DeviceStatus _status = new DeviceStatus();
        #endregion

        #region <属性>
        public virtual string DeviceName
        {
           get{ return _deviceName; }
           set{ _deviceName = value; }
        }
        public virtual string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        }
        public DeviceStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        #endregion
        #region <通用的方法>
        protected string StrToHex(string str)
        {
            string strTemp = "";
            if (str == "")
                return "";
            byte[] bTemp = System.Text.Encoding.Default.GetBytes(str);

            for (int i = 0; i < bTemp.Length; i++)
            {
                strTemp += bTemp[i].ToString("x");
            }
            return strTemp;
        }

        // 2012-03-25  modify by [BHB]  对于"00"的数据认为是自动填充的内容
        protected string HexToStr(string s)
        {
            string xx = "";
            string strAsc = "";
            string hex  = "";
            int value = 0;
            for (int i = 0; i < s.Length / 2; i++)
            {
                hex = s.Substring(2 * i, 2);
                value = Convert.ToInt32(hex, 16);
                if (0 == value) continue;
                strAsc += (char)value;
            }
            return strAsc;         
        }

        protected void WriteLog(string str)
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

                System.IO.TextWriter tw = new System.IO.StreamWriter(m_szRunPath + "\\log\\" + DeviceName +  "-" + strDate + "_Display.log", true);

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
    }
}
