using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
namespace Core.Sip.Client.Meas
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }        
        public Coordinate()
        {
            X= 0;
            Y= 0;
        }
    }
    public class BarcodeCoord : Coordinate
    {
        public int height{get;set;}
    }

    public class ZplPrinter
    {
#region <API>
        /// </summary>
        /// <param name="ChineseText">待转变中文内容</param>
        /// <param name="FontName">字体名称</param>
        /// <param name="DataName">返回的图片字符重命</param>
        /// <param name="Orient">旋转角度0,90,180,270</param>
        /// <param name="Height">字体高度</param>
        /// <param name="Width">字体宽度，通常是0</param>
        /// <param name="IsBold">1 变粗,0 正常</param>
        /// <param name="IsItalic">1 斜体,0 正常</param>
        /// <param name="ReturnPicData">返回的图片字符</param>
        /// <returns></returns>

        [DllImport("fnthex32.dll")]
        public static extern int GETFONTHEX(
         string ChineseText,    //待转变中文内容
         string FontName,       //字体名称
   //      string DataName,      // 返回的图片字符重命
         int Orient,           // 旋转角度0,90,180,270
         int Height,           // 字体高度
         int Width,   // 字体宽度，通常是0
         byte IsBold, //1 变粗,0 正常
         byte IsItalic, //1 斜体,0 正常
         System.Text.StringBuilder ReturnPicData); //返回的图片字符
#endregion
        #region <成员变量>
        /******************* 标签位置 ***********************/
        int _dots_mm = 12;       // 8/MM = 200 dpi, 12/MM = 300 dpi and 24/MM = 600 dpi     
        string _end_flag = "\r\n"; // 行指令结束符    
    
       /******************* 字体 ***********************/
       private char _font_type;           // 字体类型 A-Z，0-9（打印机的任何字体，包括下载字体，EPROM中储存的，当然这些字体必须用^CW来定义为A-Z，0-9）
       protected char _font_orientation;    // N = 正常 （Normal);R = 顺时针旋转90度（Roated);I = 顺时针旋转180度（Inverted);B = 顺时针旋转270度 (Bottom)
       protected int _font_height;          // 字体高，单位 dot
       protected int _font_width;           // 字体宽, 单位 dot

       /******************* 条码 ***********************/
       protected int _barcode_height = 0;  // 条码高度

        /******************* 网络 ***********************/  
        TcpClient tcpClient;              // TCP 类
        string _ip = "10.10.76.210";      // IP 地址
        Int32 _port = 9100;               // 端口
        bool m_alive = false;             // 会话状态
        NetworkStream m_stream = null;    // 数据流
        IPAddress addr = null;            // 端点

        /******************* 可变参数 ***********************/
        string strCmd = ""; // 指令
        int xPos = 0;       // X坐标
        int yPos = 0;       // Y坐标

        byte[] emptyCmd = Encoding.Default.GetBytes("^XA^XZ");
        #endregion
        #region <属性>
        // 数据
        public string[] Data { get; set; }
        // 坐标
        public Coordinate[] Coord { get; set; }        

        /// 打印机IP地址
        public string IP
        {
            get{
                return _ip;
            }
            set
            {
                _ip = value;
               // connect();
            }
        }
        /// <summary>
        /// 设置和获取dpi属性
        ///  8/MM = 200 dpi, 12/MM = 300 dpi and 24/MM = 600 dpi
        /// </summary>
        public int DPI
        {
            get
            {
                int value;
                switch (_dots_mm)
                {
                    case 8:
                        value = 200;
                        break;
                    case 12:
                        value = 300;
                        break;
                    case 24:
                        value = 600;
                        break;
                    default:
                        value = 300;
                        break;
                }
                return value;
            }
            set
            {
                switch (value)
                {
                    case 200:
                        _dots_mm = 8;
                        break;
                    case 300:
                        _dots_mm = 12;
                        break;
                    case 600:
                        _dots_mm = 24;
                        break;
                    default:
                        _dots_mm = 12;
                        break;
                }

            }
        }

        public char FontDirect
        {
            get { return _font_orientation; }
            set {_font_orientation = value;}

        }
          /// <summary>
        /// 打印机端口
        /// </summary>
        public Int32 Port 
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }
        public char  Font 
        {
            get{ return _font_type;}
            set{_font_type = value;}
        }
        public int FontHeight
        {
            get { return _font_height;}
            set { _font_height = value; }
        }
        public int FontWidth
        {
            get { return _font_width;  }
            set { _font_width = value; }
        }
        public int BarcodeHeight
        {
            get
            {
                return _barcode_height;
            }
            set 
            {
                _barcode_height = value;
            }
        }
        
        #endregion

        #region <构造函数>     
        public ZplPrinter()
        {
            initNet();
            initCommand();
        }  
        #endregion

        #region <初始化>
        public virtual bool Init(string configParam)
        {
            _ip = configParam;
            initNet();
            initCommand();
            return true;
        }

        /// <summary>
        ///  初始化网络设置
        /// </summary>
        protected void initNet()
        {
            tcpClient = new TcpClient();
            _port = 9100;
            m_alive = false;
            addr = IPAddress.Parse(_ip);
            // thread = new Thread(sendHeart);
        }      

        /// <summary>
        /// 变量初始化
        /// </summary>
        public void initCommand()
        {
            _dots_mm = 12;       // 8/MM = 200 dpi, 12/MM = 300 dpi and 24/MM = 600 dpi      
            _end_flag = "\r\n";
            //
            strCmd = "";    // 指令
            xPos = 0;       // X坐标
            yPos = 0;       // Y坐标

            //
            _barcode_height = 150;  //
            // 缺省字体设置
            _font_type = 'F';
            _font_height = 66;
            _font_width = 26;
            _font_orientation = 'N';
        }
        #endregion
        /// <summary>
        /// _dots_mm 属性
        /// </summary>
        public int getDotMM()
        {     
                return _dots_mm;           
        }
        
        /// <summary>
        /// 设置缺省字体
        /// </summary>
        /// <param name="font">字体类型A-Z 0-9 </param>
        /// <param name="height">高度； 单位mm</param>
        /// <param name="width">宽度； 单位mm</param>
        public void setDefaultFont(char font, int height, int width, char orientation)
        {
            _font_type = font;
            _font_height = height;
            _font_width = width;
            _font_orientation = orientation; 
            strCmd += "^CF" + _font_type + "," + _font_height + "," + _font_width + _end_flag;  //'^CFA,50,24';
        }


        /// <summary>
        /// 与打印机建立Socket 连接
        /// </summary>
        /// <returns>连接状态</returns>
        public void  connect()
        {            
            //try
            //{
            //    if (m_stream != null)
            //        m_stream.Close();
            //    if (tcpClient != null)
            //        tcpClient.Close();
            //    tcpClient = new TcpClient();
            //    addr = IPAddress.Parse(_ip);
            //    tcpClient.Connect(addr, _port);
            //    m_stream = tcpClient.GetStream();
            //    //m_stream.ReadTimeout = 1000;
            //    //m_stream.WriteTimeout = 1000;
            //    m_alive = true;
            //}
            //catch (Exception ex)
            //{
            //    string error = ex.Message;
            //    m_alive = false;
            //    //throw new Exception("连接打印机失败！"+ex.Message);
            //}
              
        }

        // 发送指令给打印机
        public void sendMessage(byte[] data)
        {
            if (!alive()) throw new Exception("打印失败，请检查打印机连接！",null);
                //connect();   
            try
            {
                if (alive())
                {
                    m_stream.Write(data, 0, data.Length);
                    //m_alive = true;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //m_alive = false;
                throw new Exception("数据发送失败"+ ex.Message);     //showErrorMessage("数据发送失败！" + ex.Message);                
            }           
        }

        // 判断连接状态
        protected bool alive()
        {
            return m_alive;
        }

        /// <summary>
        /// 打印开始指令
        /// </summary>
        protected string getBeginCmd()
        {
            string strCmd = "";
            strCmd += "^XA" + _end_flag;
            return strCmd;
        }

        /// <summary>
        /// 打印结束指令
        /// </summary>
        protected string getEndCmd()
        {
            string strCmd = "";
            strCmd += "^XZ"+_end_flag;
            return strCmd;          
        }

        protected string getLableHomeCmd(int x, int y)
        {
            string strCmd = "";
            strCmd += "^LH" +x.ToString() + ","  + y.ToString() + _end_flag;
            return strCmd;
        }
        /// <summary>
        /// 重新尝试一次打印
        /// </summary>
        protected bool printAgain()
        {
            sendMessage(Encoding.Default.GetBytes(strCmd));
            return alive();
        }

        /// <summary>
        /// 设置打印数据的位置
        /// </summary>
        /// <param name="xPos">横坐标位置</param>
        /// <param name="yPos">纵坐标位置</param>
        protected string getPositionCommand(int xPos, int yPos)
        {
            return "^FO" + xPos.ToString() + "," + yPos.ToString() + _end_flag;
        }

        /// <summary>
        /// 设置数据项
        /// </summary>
        /// <param name="data">数据内容</param>
        protected string getFieldDataCommand(string data)
        {
             return "^FD" + data + "^FS" + _end_flag;
        }

        /// <summary>
        /// 在指定坐标填充数据
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="data">填充的数据</param>
        /// <returns></returns>
        protected string fillDataCmd(int x, int y, string data)
        {
            return  getPositionCommand(x, y) + getFieldDataCommand(data);
        }
        protected int getStringDots(string data)
        {
            int len = 0;
           // int level = data.Length/5;
            len = Convert.ToInt32((data.Length + (double)data.Length / 3) * _font_width);
        
            return len;
           
        }
    

        /// <summary>
        /// 获取条码指令
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="orientation"> 方向: N = 正常 （Normal);R = 顺时针旋转90度（Roated);I = 顺时针旋转180度（Inverted);B = 顺时针旋转270度 (Bottom)</param>
        /// <param name="data">条码数据项</param>
        protected string getBarcodeCmd(int x, int y, string orientation, string data)
        {
            string strCmd = "";
            strCmd += getPositionCommand(x, y);
            strCmd += "^BC"+ orientation+ "," + _barcode_height.ToString() + ",Y,N,N" + _end_flag; // code 128 
            strCmd += getFieldDataCommand(data);
            return strCmd;
        }

        /// <summary>
        /// 添加条码指令
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="orientation"> 方向: N = 正常 （Normal);R = 顺时针旋转90度（Roated);I = 顺时针旋转180度（Inverted);B = 顺时针旋转270度 (Bottom)</param>
        /// <param name="data">条码数据项</param>
        protected string setBarcodeCmd(int x, int y, string orientation, string data)
        {
            strCmd += getPositionCommand(xPos, yPos);
            strCmd += "^BC" + orientation + "," + _barcode_height.ToString() + ",Y,N,N" + _end_flag; // code 128 
            strCmd += getFieldDataCommand(data);
            return strCmd;
        }

        /// <summary>
        /// 获取打印图片指令
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="mx">横坐标</param>
        /// <param name="my">纵坐标</param>
        /// <returns>打印指令字符串</returns>
        protected string getImageCommand(string fileName, int mx, int my)
        {
            string strCmd = "";
            strCmd = "^XGR:" + fileName + "," + mx + "," + my + "^FS";
            return strCmd;
        }

        /// <summary>
        /// 设置打印图片指令
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="mx">横坐标</param>
        /// <param name="my">纵坐标</param>
        /// <returns>打印指令字符串</returns>
        protected void setImageCommand(string fileName, int mx, int my)
        {
            strCmd += "^XGR:" + fileName + "," + mx + "," + my + "^FS";
        }
        /// <summary>
        /// 设置字体指令
        /// </summary>
        /// <returns>打印指令字符串</returns>
        protected string getDefaultFontCmd()
        {
            string strCmd = "";
            strCmd += "^CF" + _font_type  + "," + _font_height + "," + _font_width + _end_flag;  //'^CFA,50,24';
            return strCmd;
        }
        /// <summary>
        /// 获取可缩放/点阵字体指令
        /// </summary>
        /// <returns></returns>
        protected string getFontCmd()
        {            
            string cmd = "^A" + _font_type + _font_orientation + "," + _font_height + "," + _font_width + _end_flag;  //'^AFR,50,24';
            return cmd;
        }
        /// <summary>
        /// 设置字体浓度
        /// </summary>
        /// <param name="darkness"></param>
        protected void setDarkness(int darkness)
        {
            strCmd += "^CM" +darkness.ToString()+_end_flag;
        }
        //Use Unicode UTF16 Big Endian
        public string getUtf16BECmd()
        {
            return "^CI29" + _end_flag;
        }
        protected void sendCommand(string strCmd)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int pakLen = strCmd.Length;
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            socket.Connect(IPAddress.Parse(_ip), _port);
            pakLen = socket.Send(Encoding.Default.GetBytes(strCmd), SocketFlags.None);
            socket.Close();
            //sendMessage(Encoding.Default.GetBytes(strCmd));
        }
    }
}
