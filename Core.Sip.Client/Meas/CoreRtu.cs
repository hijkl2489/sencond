using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace Core.Sip.Client.Meas
{
    public enum RtuCommand
    {
        READ_VALUE = 0X01,
        CLOSE_SWITCH ,
        OPEN_SWITCH
    }

    public enum SwitchStatus
    {
        OPEN = 0,
        CLOSE = 1
    }
    public class CoreRtu
    {
        #region <成员变量>
        string _ipAddr;     // IP地址 
        ushort _port;       // 设备端口 1100
        Socket _socket;     // 套接字
        byte _dest = 1;     // 设备地址， 通常指Rtu的某一模块
        byte _DOAddrLow = 0x50;
        byte[] _DOAddrHight = null; 
        bool[] _DOValue = null;//DO状态值
        #endregion 

        #region <属性>
        public string IP
        {
            get { return _ipAddr; }
            set { _ipAddr = value; }
        }

        public ushort Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public bool[] DO
        {
            get { return _DOValue; }
        }

        #endregion

        public CoreRtu()
        {
            _ipAddr = "10.25.3.11";
            _port =1100;
            // 设备地址
            _dest = 0x01;
            // DO 地址地位
            _DOAddrLow = 0x50;

            // DO 的高位地址
            _DOAddrHight = new byte[6] { 0xDD, 0xDE, 0xDF, 0xE0, 0xE1, 0xE2 };

            // DO 值初始化
            _DOValue = new bool[8];
        }
        #region <公共方法>
        
        public void init(string config)
        {            
            string[] strtmp = config.Split(new char[] { ':' });
            IP = strtmp[0];
            Port = Convert.ToUInt16(strtmp[1]);

            // 设备地址
            _dest = 0x01;
            // DO 地址地位
            _DOAddrLow = 0x50;

             // DO 的高位地址
            _DOAddrHight = new byte[6]{0xDD,0xDE,0xDF,0xE0, 0xE1, 0xE2}; 

            // DO 值初始化
            _DOValue = new bool[8];
           
        }

        protected void Request(RtuCommand cmd, byte varAddr)
        {
            byte[] obuffer = new byte[255];
            byte[] d = new byte[1];
            int len = 0;
            BitArray bits = null;
            Connect();
            SendCommand(cmd, varAddr);
            len = _socket.Receive(obuffer);
            _socket.Close();
            if (cmd == RtuCommand.READ_VALUE)
            {
                d[0] = obuffer[10];
                bits = new BitArray(d);
                for (int i = 0; i < bits.Length; i++)
                {
                    _DOValue[i] = bits.Get(i);
                }
            }
            else
            {
                d[0] = obuffer[0];
                bits = new BitArray(d);
            }
            
        }

        protected void SendCommand(RtuCommand cmd, byte varAddr)
        {
            byte[] cmdBuf = new byte[12];
            //cmdBuf.SetValue(0x00, 0);
            for (int i = 0; i < 5; i++) cmdBuf[i] = 0; 
     
            switch ((RtuCommand)cmd)
            {
                case RtuCommand.READ_VALUE:
                     cmdBuf[5] = 6;
                     cmdBuf[6] = _dest;
                     cmdBuf[7] = 1;
                     cmdBuf[8] = _DOAddrLow;
                     cmdBuf[9] = 0x70;
                     cmdBuf[10] = 0;
                     cmdBuf[11] = 16;
                     break;
                case RtuCommand.OPEN_SWITCH:
                     cmdBuf[5] = 6;
                     cmdBuf[6] = _dest;
                     cmdBuf[7] = 5;
                     cmdBuf[8] = _DOAddrLow;
                     cmdBuf[9] = Convert.ToByte(varAddr - 1);
                     cmdBuf[10] = 0;
                     cmdBuf[11] = 0;
                     break;
                case RtuCommand.CLOSE_SWITCH:
                     cmdBuf[5] = 6;
                     cmdBuf[6] = _dest;
                     cmdBuf[7] = 5;
                     cmdBuf[8] = _DOAddrLow;
                     cmdBuf[9] = Convert.ToByte(varAddr - 1);
                     cmdBuf[10] = 0xFF;
                     cmdBuf[11] = 0;
                     break;
            }
            int len = 5 + cmdBuf[5] + 1;
            _socket.Send(cmdBuf, len, SocketFlags.None);
        }

       
        protected void Connect()
        {
            //设置接受超时时间为2秒和地址重用
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 20000);
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            IPEndPoint epRemote = new IPEndPoint(IPAddress.Parse(IP), Port);
            try
            {
                // 建立连接
                _socket.Connect(epRemote);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void OpenSwitch(int index)
        {
            Request(RtuCommand.OPEN_SWITCH,_DOAddrHight[index]);
        }
        public void CloseSwith(int index)
        {
            Request(RtuCommand.CLOSE_SWITCH, _DOAddrHight[index]);
        }

        public bool[] ReadDO()
        {
            Request(RtuCommand.READ_VALUE, (byte)0x00);
            return _DOValue;
        }
        #endregion
 /*
        public byte[] RTU_BIND(IPAddress destip, int port, byte dest, string cmdtype ,int addr)
        {
            Socket socket;
            byte[] finalmsg;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 20000);
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                //参数1指定本机IP地址（此处指所有可用的IP地址），参数2指定接收用的端口
                //IPEndPoint myHost = new IPEndPoint(IPAddress.Any, 1595);
                //将本机IP地址和端口与套接字绑定，为接收做准备
                //socket.Bind(myHost);
                //定义远程终端IP地址和端口（实际使用时应为远程主机IP地址），为发
                //送数据做准备
                IPEndPoint remote = new IPEndPoint(destip, port);
                //建立与远程主机的连接
                socket.Connect(remote);
                finalmsg = new byte[20];//最大返回字节
            }
            catch (Exception er)
            {
                throw new Exception(er.Message);
            }
            //向远程终端发送信息
            try
            {
                byte[] byteout = new byte[255];
                int length;
                switch (cmdtype)
                {
                    case "ReadVal":
                        byteout = this.ReadVal(dest);
                        break;
                    case "OpenSwitch":
                        byteout = this.OpenSwitch(dest,addr);
                        break;
                    case "CloseSwitch":
                        byteout = this.CloseSwitch(dest,addr);
                        break;                   
                    default:
                        break;
                }
                //length=byteout[1]+2;
                //byteout[5]为长度位
                length = 5 + byteout[5] + 1;
                int iLen = socket.Send(byteout, length, SocketFlags.None);
            }
            catch (Exception err)
            {
                //控制台输出异常信息，可删除该指令，在主程序中通过try-catch获得该异常
                Console.WriteLine(err.ToString());
                //抛出一个异常信息
                throw new Exception(err.Message);
            }
            //以下代码是等待接受方的确认信息，使发送方知道清零命令执行效果
            //从本地绑定的IP地址和端口接收远程终端的数据，返回接收的字节或字节数数length = socket.Receive(bytes);
            byte[] byin = new byte[255];
            //byte[] byin = socket.Receive(bytes);
            try { socket.Receive(byin); }
            catch (Exception err) { throw new Exception(err.Message); }
            //如果只想知道各离散设备的状态，也调用DRTU_RVMSG处理，返回一个字节含所有设备状态信息，使用的时候再对该字节解析，返回想要的一位信息
            socket.ReceiveTimeout = 2000;
            try
            {
                switch (cmdtype)
                {
                    case "ReadVal":
                        finalmsg = this.RecVal(byin);
                        break;
                    default:
                        break;
                }
                ////关闭套接字
                socket.Close();
                return finalmsg;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }


        public byte[] RecVal(byte[] rec)
        {
            byte[] suceed = new byte[1];//
            //suceed[0] = rec[9];
            suceed[0] = rec[10];
            //suceed[1] = rec[9];
            return suceed;
        }

        //开关打开控制，寄存器开始地址是2
        public byte[] OpenSwitch(byte dest, int addr)
        {
            //报文格式:0-4：传输ID 5：长度 6：设备ID 7：功能码 8-9：寄存器地址 10-11：寄存器地址偏移
            byte[] byout = new byte[12];
            byout[0] = 0;
            byout[1] = 0;
            byout[2] = 0;
            byout[3] = 0;
            byout[4] = 0;
            byout[5] = 6;
            byout[6] = dest;
            byout[7] = 5;
            byout[8] = 0x50;
            byout[9] = Convert.ToByte(addr-1);
            byout[10] = 0;
            byout[11] = 0;
                      
            return byout;
        }

        public byte[] CloseSwitch(byte dest, int addr)
        {
            //报文格式:0-4：传输ID 5：长度 6：设备ID 7：功能码 8-9：寄存器地址 10-11：寄存器地址偏移
            byte[] byout = new byte[12];

            byout[0] = 0;
            byout[1] = 0;
            byout[2] = 0;
            byout[3] = 0;
            byout[4] = 0;
            byout[5] = 6;
            byout[6] = dest;
            byout[7] = 5;
            byout[8] = 0x50;
            byout[9] = Convert.ToByte(addr-1);
            byout[10] = 0xFF;
            byout[11] = 0;

            return byout;
        }

        public byte[] ReadVal(byte dest)
        {
            //报文格式:0-4：传输ID 5：长度 6：设备ID 7：功能码 8-9：寄存器地址 10-11：寄存器地址偏移
            byte[] byout = new byte[12];

            byout[0] = 0;
            byout[1] = 0;
            byout[2] = 0;
            byout[3] = 0;
            byout[4] = 0;
            byout[5] = 6;
            byout[6] = dest;
            byout[7] = 1;
            byout[8] = 0x50;
            byout[9] = 0x70;
            byout[10] = 0;
            byout[11] = 16;

            return byout;
        }
 */ 

    }
}
