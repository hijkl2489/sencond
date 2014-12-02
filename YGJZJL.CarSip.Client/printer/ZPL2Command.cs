using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace YGJZJL.CarSip.Client.Printer
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class ZPL2Command
    {
        /******************* 标签位置 ***********************/
        int dotsMm = 12;       // 8/MM = 200 dpi, 12/MM = 300 dpi and 24/MM = 600 dpi     
        string endFlag = "\r\n"; // 行指令结束符    
    
       /******************* 字体 ***********************/
       protected char fontType;           // 字体类型 A-Z，0-9（打印机的任何字体，包括下载字体，EPROM中储存的，当然这些字体必须用^CW来定义为A-Z，0-9）
       protected char fontOrientation;    // N = 正常 （Normal);R = 顺时针旋转90度（Roated);I = 顺时针旋转180度（Inverted);B = 顺时针旋转270度 (Bottom)
       protected int fontHeight;          // 字体高，单位 dot
       protected int fontWidth;           // 字体宽, 单位 dot

       /******************* 条码 ***********************/
       protected int barcodeHeight = 0;  // 条码高度

        /******************* 网络 ***********************/  
        TcpClient tcpClient;              // TCP 类
        string ipAddr = "10.10.76.210";   // IP 地址
        Int32 port = 12000;               // 端口
        bool m_alive = false;             // 会话状态
        NetworkStream m_stream = null;    // 数据流
        IPAddress addr = null;            // 端点

        /******************* 可变参数 ***********************/
        string strCmd = ""; // 指令
        int xPos = 0;       // X坐标
        int yPos = 0;       // Y坐标

        byte[] emptyCmd = Encoding.Default.GetBytes("^XA^XZ");

        #region <属性>
        // 数据
        public string[] Data { get; set; }
        // 坐标
        public Coordinate[] Coord { get; set; }        

        /// 打印机IP地址
        public string Addr
        {
            get{
                return ipAddr;
            }
            set
            {
                ipAddr = value;
                connect();
                
               // if (thread != null) thread.Start();
            }
        }
        /// <summary>
        /// 设置和获取dpi属性
        ///  8/MM = 200 dpi, 12/MM = 300 dpi and 24/MM = 600 dpi
        /// </summary>
        public int dpi
        {
            get
            {
                int value;
                switch (dotsMm)
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
                        dotsMm = 8;
                        break;
                    case 300:
                        dotsMm = 12;
                        break;
                    case 600:
                        dotsMm = 24;
                        break;
                    default:
                        dotsMm = 12;
                        break;
                }

            }
        }
        public char FontDirect
        {
            get { return fontOrientation; }
            set {fontOrientation = value;}

        }
        #endregion

        #region <构造函数>     
        public ZPL2Command()
        {
            initNet();
            initCommand();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="addr">IP 地址</param>
        public ZPL2Command(string addr)
        {
            ipAddr = addr;
            initNet();
            initCommand();
        }
        #endregion

        /// <summary>
        ///  初始化网络设置
        /// </summary>
        protected void initNet()
        {
            tcpClient = new TcpClient();
            port = 9100;
            m_alive = false;
            addr = IPAddress.Parse(ipAddr);
            // thread = new Thread(sendHeart);
        }      

        /// <summary>
        /// 变量初始化
        /// </summary>
        public void initCommand()
        {
            dotsMm = 12;       // 8/MM = 200 dpi, 12/MM = 300 dpi and 24/MM = 600 dpi      
            endFlag = "\r\n";
            //
            strCmd = "";    // 指令
            xPos = 0;       // X坐标
            yPos = 0;       // Y坐标

            //
            barcodeHeight = 150;  //
            // 缺省字体设置
            fontType = 'F';
            fontHeight = 66;
            fontWidth = 26;
            fontOrientation = 'B';
        }
        /// <summary>
        /// dotsMm 属性
        /// </summary>
        public int getDotMM()
        {     
                return dotsMm;           
        }
        /// <summary>
        /// 打印机端口
        /// </summary>
        public Int32 Port 
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }
        public char  Font 
        {
            get
            {
                return fontType;
            }
            set
            {
                fontType = value;
            }
        }

        public int BarcodeHeight
        {
            get
            {
                return barcodeHeight;
            }
            set 
            {
                barcodeHeight = value;
            }
        }
        public string IP
        {
            get { return ipAddr; }
            set { ipAddr = value;}
        }
        /// <summary>
        /// 设置缺省字体
        /// </summary>
        /// <param name="font">字体类型A-Z 0-9 </param>
        /// <param name="height">高度； 单位mm</param>
        /// <param name="width">宽度； 单位mm</param>
        public void setDefaultFont(char font, int height, int width, char orientation)
        {
            fontType = font;
            fontHeight = height;
            fontWidth = width;
            fontOrientation = orientation; 
            strCmd += "^CF" + fontType + "," + fontHeight + "," + fontWidth + endFlag;  //'^CFA,50,24';
        }


        /// <summary>
        /// 与打印机建立Socket 连接
        /// </summary>
        /// <returns>连接状态</returns>
        public void  connect()
        {            
            try
            {
                if (m_stream != null)
                    m_stream.Close();
                if (tcpClient != null)
                    tcpClient.Close();
                tcpClient = new TcpClient();
                addr = IPAddress.Parse(ipAddr);
                tcpClient.Connect(addr, port);
                m_stream = tcpClient.GetStream();
                //m_stream.ReadTimeout = 1000;
                //m_stream.WriteTimeout = 1000;
                m_alive = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                m_alive = false;
                //throw new Exception("连接打印机失败！"+ex.Message);
            }
              
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
            strCmd += "^XA" + endFlag;
            return strCmd;
        }

        /// <summary>
        /// 打印结束指令
        /// </summary>
        protected string getEndCmd()
        {
            string strCmd = "";
            strCmd += "^XZ"+endFlag;
            return strCmd;          
        }

        protected string getLableHomeCmd(int x, int y)
        {
            string strCmd = "";
            strCmd += "^LH" +x.ToString() + ","  + y.ToString() + endFlag;
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
            return "^FO" + xPos.ToString() + "," + yPos.ToString() + endFlag;
        }

        /// <summary>
        /// 设置数据项
        /// </summary>
        /// <param name="data">数据内容</param>
        protected string getFieldDataCommand(string data)
        {
             return "^FD" + data + "^FS" + endFlag;
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
            len = Convert.ToInt32((data.Length + (double)data.Length / 3) * fontWidth);
        
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
            strCmd += "^BC"+ orientation+ "," + barcodeHeight.ToString() + ",Y,N,N" + endFlag; // code 128 
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
            strCmd += "^BC" + orientation + "," + barcodeHeight.ToString() + ",Y,N,N" + endFlag; // code 128 
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
            strCmd += "^CF" + fontType  + "," + fontHeight + "," + fontWidth + endFlag;  //'^CFA,50,24';
            return strCmd;
        }
        /// <summary>
        /// 获取可缩放/点阵字体指令
        /// </summary>
        /// <returns></returns>
        protected string getFontCmd()
        {            
            string cmd = "^A" + fontType + fontOrientation + "," + fontHeight + "," + fontWidth + endFlag;  //'^AFR,50,24';
            return cmd;
        }
        /// <summary>
        /// 设置字体浓度
        /// </summary>
        /// <param name="darkness"></param>
        protected void setDarkness(int darkness)
        {
            strCmd += "^CM" +darkness.ToString()+endFlag;
        }

        protected void sendCommand(string strCmd)
        {
            sendMessage(Encoding.Default.GetBytes(strCmd));
        }

      

    }
}
