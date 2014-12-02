using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace YGJZJL.CarSip.Client.Meas
{
    public class LedScreen : CoreDevice, IDevice
    {

        #region <类型定义>
        public enum Transparent
        {
            OFF = 0,
            ON = 1           
        }
        //2.左滚显示;3.连续上滚;4.中间向上下展开;5.中间向两边展开;6.中间向四周展开;7.向左移入;8.向右移入;9.从左向右展开;10.从右向左展开;11.右上角移入;12.右下角移入;13.左上角移入;14.左下角移入;15.从上向下移入;16.从下向上移入;17.闪  烁;</param>
        public enum Method
        {
            IMMEDIATE = 1,
            TO_LEFT = 2,
            TO_TOP  = 3,
            MIDDLE_TOP_BOTTOM = 4,
            MIDDLE_LEFT_RIGHT = 5,
            MIDDLE_SURROUD = 6,
            LEFT_IN = 7,
            RIGHT_IN = 8,
            EXPANSION_LEFT_RIGHT = 9,
            EXPANSION_RIGHT_LEFT = 10,
            RIGHT_TOP_IN = 11,
            RIGHT_BOTTOM_IN = 12,
            LEFT_TOP_IN = 13,
            LEFT_BOTTOM_IN = 14,
            TOP_BOTTOM_IN = 15,
            BOTTOM_TOP_IN = 16,
            TWINKLE = 17
        }
        public enum Speed
        {
            LEVEL1 = 1,
            LEVEL2,
            LEVEL3,
            LEVEL4,
            LEVEL5,
            LEVEL6,
            LEVEL7
        }
        public enum PowerStatus
        {
            OFF = 0,
            ON  = 1
        }
        #endregion
        #region <成员变量>
        private int _dev = -1;                  // 设备
        private string _ip = "192.168.0.99";   // IP 地址
        private byte _address = 0x00;              // 硬件地址
        private uint _remote_port = 6666;    // 设备端口
        private uint _local_port = 9990;     // 本地端口
        #endregion

        #region <属性>
        public string IP
        {
            get{return _ip;}
            set {_ip = value;}
        }
        public int LocalPort
        {
            get{return (int)_local_port;}
            set {_local_port = Convert.ToUInt16(value);}
        }
        public int RemotePort
        {
            get{return (int)_remote_port;}
            set { _remote_port = Convert.ToUInt16(value); }
        }
        public int CardAddress
        {
            get { return (int)_address; }
            set { _address = Convert.ToByte(value); }

        }
        #endregion
    

        #region constructor

        public LedScreen()
        {
           _dev = -1;              // 设备
           _ip = "192.168.0.99";   // IP 地址
           _address = 0;           // 硬件地址
           _remote_port = 6666;    // 设备端口
           _local_port = 9990;     // 本地端口
        }
      
        public void Init(string config)
        {
            string[] strtmp = config.Split(new char[] { ',' });
            _ip = strtmp[0];
            _address = Convert.ToByte(strtmp[1]);
            _remote_port = Convert.ToUInt32(strtmp[2]);
            _local_port = Convert.ToUInt32(strtmp[3]);
        }

        #endregion

        #region methods

        /// <summary>
        /// 建立和Led的连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            LedSdk.PDeviceParam param = new LedSdk.PDeviceParam();
            param.FlowCon = 0;
            param.devType = LedSdk.DEVICE_TYPE_UDP;
            param.rmtPort = _remote_port;
            param.locPort = _local_port;
            // 建立连接
            _dev = LedSdk.LED_Open(ref param, 0, 0, 0);
            if (_dev == -1)
            {
                Status =  DeviceStatus.CLOSE;
                return false;
            }
            else
            {
                Status = DeviceStatus.INIT;
                return true;
            }
        }

        /// <summary>
        /// 发送文本数据到LED
        /// </summary>
        /// <param name="strText">要发送的字符串，用\n分行以显示多行</param>
        /// <param name="strFontName">字体名称，如宋体</param>
        /// <param name="nFontSize">字体大小，显示4行数据建议字体大小为10，显示5行数据建议字体大小为9，其余大小不太适合本案</param>
        /// <param name="nMethod">显示方式，建议为1。1.立即显示;2.左滚显示;3.连续上滚;4.中间向上下展开;5.中间向两边展开;6.中间向四周展开;7.向左移入;8.向右移入;9.从左向右展开;10.从右向左展开;11.右上角移入;12.右下角移入;13.左上角移入;14.左下角移入;15.从上向下移入;16.从下向上移入;17.闪  烁;</param>
        /// <param name="nSpeed">显示速度（1-8），越大越快，建议为1</param>
        /// <param name="nTransparent">是否透明。0=不透明 1=透明</param>
        public void SendText(string text, string fontName, int fontSize, int method, int speed, int transparent)
        {
            int ret = LedSdk.MakeRoot(LedSdk.ROOT_PLAY, LedSdk.SCREEN_COLOR);//创建一个发送序列,以前的将被清除
            if (ret == -1) return;
            ret = LedSdk.AddLeaf(86400000);                                  //增加一个页面
            if (ret == -1) return;
            LedSdk.Rectangle rect = new LedSdk.Rectangle();
            Font font = new Font(fontName, fontSize);
            int height = font.Height + 3;
            string[] strArray = text.Split(new char[] { '\n' });
            for (int i = 0; i < strArray.Length; i++)
            {
                ret = LedSdk.SetRect(ref rect, 0, height * i, 256, height * (i + 1));
                if (ret == -1) return;
                ret = LedSdk.AddText(strArray[i],ref rect, method, speed, transparent, fontName, fontSize, 255);
                if (ret == -1) return;
            }
            LedSdk.LED_SendToScreen(_dev, _address, _ip, _remote_port); 
        }
        // 简化版
        public void SendText(string text, string fontName, int fontSize)
        {
            SendText(text, fontName, fontSize, 1, 1, 0);
        }
        /// <summary>
        /// 打开或关闭Led电源
        /// </summary>
        /// <param name="nPowerOn">电源状态。0=关闭，1=打开</param>
        public void SetPower(int Power)
        {
            LedSdk.LED_SetPower(_dev,_address, _ip, _remote_port,(uint)Power);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public bool Close()
        {
            if (_dev >= 0)
            {
                Status = DeviceStatus.CLOSE;
                LedSdk.LED_Close(_dev);
                return true;
            }
            return false;
        }
        #endregion       
    }
}
