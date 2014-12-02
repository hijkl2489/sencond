using System;
using System.Runtime.InteropServices;
using System.Threading;


namespace SerialsComLib
{
    public class SerialsComLib
    {
        /// <summary>
        /// SerialsBSTLib通讯管理类
        /// </summary>
        #region win32 API常量
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        private const uint INFINITE = 0xFFFFFFFF;
        private const int OPEN_EXISTING = 3;
        private const int INVALID_HANDLE_VALUE = -1;

        private const int PURGE_TXABORT = 0x1;     // 终止所有正在进行的字符输出操作'Kill the pending/current writes to the comm port.
        private const int PURGE_RXABORT = 0x2;     // 终止所有正在进行的字符输入操作'Kill the pending/current reads to the comm port.
        private const int PURGE_TXCLEAR = 0x4;     // 命令指导设备驱动程序清除输出缓冲区'Kill the transmit queue if there.
        private const int PURGE_RXCLEAR = 0x8;     // 命令用于设备驱动程序清除输入缓冲区'Kill the typeahead buffer if there.
        #endregion

        #region 结构体
        /// <summary>
        /// 设备控制块,作为API封装函数的输入输出参数所必须的一个数据结构体
        /// </summary>
        [StructLayout(LayoutKind.Sequential)] // 开辟连续的内存空间
        public struct DCB
        {
            /// <summary>
            /// 结构体的字节数
            /// </summary>
            public int DCBlength;
            /// <summary>
            /// 当前波特率
            /// </summary>
            public int BaudRate;
            /// <summary>
            /// 指定是否允许二进制模式,在windows95中必须主TRUE binary mode, no EOF check 
            /// </summary>
            public int fBinary;
            /// <summary>
            /// 指定是否允许奇偶校验
            /// </summary>
            public int fParity;
            /// <summary>
            /// 指定CTS是否用于检测发送控制，当为TRUE是CTS为OFF，发送将被挂起。
            /// </summary>
            public int fOutxCtsFlow;
            /// <summary>
            /// 指定DRS是否用于检测发送控制
            /// </summary>
            public int fOutxDsrFlow;
            /// <summary>
            /// DTR_CONTROL_DISABLE值将DTR置为OFF, DTR_CONTROL_ENABLE值将DTR置为ON, DTR_CONTROL_HANDSHAKE允许DTR"握手"
            /// </summary>
            public int fDtrControl;
            /// <summary>
            /// 当该值为TRUE时DSR为OFF时接收的字节被忽略
            /// </summary>
            public int fDsrSensitivity;
            /// <summary>
            /// 指定当接收缓冲区已满,并且驱动程序已经发送出XoffChar字符时发送是否停止。
            /// <remark>
            /// TRUE时，在接收缓冲区接收到缓冲区已满的字节XoffLim且驱动程序已经发送出XoffChar字符中止接收字节之后，发送继续进行。　
            /// FALSE时，在接收缓冲区接收到代表缓冲区已空的字节XonChar且驱动程序已经发送出恢复发送的XonChar之后，发送继续进行。
            /// </remark>
            /// </summary>
            public int fTXContinueOnXoff;
            /// <summary>
            /// TRUE时，接收到XoffChar之后便停止发送接收到XonChar之后将重新开始
            /// </summary>
            public int fOutX;
            /// <summary>
            /// TRUE时，接收缓冲区接收到代表缓冲区满的XoffLim之后，XoffChar发送出去接收缓冲区接收到代表缓冲区空的XonLim之后，XonChar发送出去
            /// </summary>
            public int fInX;
            /// <summary>
            /// 该值为TRUE且fParity为TRUE时，用ErrorChar 成员指定的字符代替奇偶校验错误的接收字符
            /// </summary>
            public int fErrorChar;
            /// <summary>
            /// 为TRUE时，接收时去掉空（0值）字节
            /// </summary>
            public int fNull;
            /// <summary>
            /// RTS流量控制
            /// <remark>
            /// RTS_CONTROL_DISABLE时,RTS置为OFF,RTS_CONTROL_ENABLE时, RTS置为ON 
            /// RTS_CONTROL_HANDSHAKE时, 当接收缓冲区小于半满时RTS为ON ,当接收缓冲区超过四分之三满时RTS为OFF 
            /// RTS_CONTROL_TOGGLE时, 当接收缓冲区仍有剩余字节时RTS为ON ,否则缺省为OFF
            /// </remark>
            /// </summary>
            public int fRtsControl;
            /// <summary>
            /// TRUE时,有错误发生时中止读和写操作
            /// </summary>
            public int fAbortOnError;
            /// <summary>
            /// 未使用
            /// </summary>
            public int fDummy2;
            /// <summary>
            /// 标志
            /// </summary>
            public uint flags;
            /// <summary>
            /// 未使用,必须为0
            /// </summary>
            public ushort wReserved;
            /// <summary>
            /// 指定在XON字符发送这前接收缓冲区中可允许的最小字节数
            /// </summary>
            public ushort XonLim;
            /// <summary>
            /// 指定在XOFF字符发送这前接收缓冲区中可允许的最小字节数
            /// </summary>
            public ushort XoffLim;
            /// <summary>
            /// 指定端口当前使用的数据位
            /// </summary>
            public byte ByteSize;
            /// <summary>
            /// 指定端口当前使用的奇偶校验方法,0-4分别表示(无),(奇校验),(偶校验),(标记),(空格)
            /// </summary>
            public byte Parity;
            /// <summary>
            /// 指定端口当前使用的停止位数, 0-2分别表示 1, 1.5, 2 
            /// </summary>
            public byte StopBits;
            /// <summary>
            /// 指定用于发送和接收字符XON的值
            /// </summary>
            public char XonChar;
            /// <summary>
            /// 指定用于发送和接收字符XOFF值
            /// </summary>
            public char XoffChar;
            /// <summary>
            /// 本字符用来代替接收到的奇偶校验发生错误时的值
            /// </summary>
            public char ErrorChar;
            /// <summary>
            /// 当没有使用二进制模式时,本字符可用来指示数据的结束
            /// </summary>
            public char EofChar;
            /// <summary>
            /// 当接收到此字符时,会产生一个事件
            /// </summary>
            public char EvtChar;
            /// <summary>
            /// 未使用
            /// </summary>
            public ushort wReserved1;
        }


        /// <summary>
        /// 串口超时参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)] // 开辟连续的内存空间
        private struct COMMTIMEOUTS
        {
            /// <summary>
            /// 读取间隔时间超时
            /// </summary>
            public int ReadIntervalTimeout;
            /// <summary>
            /// 全部读取超时时间倍数
            /// </summary>
            public int ReadTotalTimeoutMultiplier;
            /// <summary>
            /// 全部读取超时常量
            /// </summary>
            public int ReadTotalTimeoutConstant;
            /// <summary>
            /// 全部写入超时时间倍数
            /// </summary>
            public int WriteTotalTimeoutMultiplier;
            /// <summary>
            /// 全部写入超时常量
            /// </summary>
            public int WriteTotalTimeoutConstant;
        }


        /// <summary>
        /// 数据重叠参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)] // 开辟连续的内存空间   
        private struct OVERLAPPED
        {
            /// <summary>
            /// 内部数据位
            /// </summary>
            public int Internal;
            /// <summary>
            /// 内部数据高位
            /// </summary>
            public int InternalHigh;
            /// <summary>
            /// 偏移位
            /// </summary>
            public int Offset;
            /// <summary>
            /// 偏移高位
            /// </summary>
            public int OffsetHigh;
            /// <summary>
            /// 事件句柄
            /// </summary>
            public int hEvent;
        }
        #endregion

        #region 私有字段
        private bool _isOpened = false;				// 通讯端口是否打开
        private int _sendDataTimeout = 1000;		// 发送数据超时时限
        //private bool _isReseivedResponse = false;	// 是否接收到数据回复
        private int _reSendNum = 2;					// 发送数据的重试次数
        //private bool _isSended = false;				// 是否已经发送数据
        private object _o = new object();			// 用于充当多线程访问时的被锁对象

        // 以下为实例化串口需要的变量
        private string _portNum = "COM1";			// 串口名称
        private int _baudRate = 9600;				// 波特率
        private byte _byteSize = 8;					// 端口参数
        private string _parity = "O";					// 0-4=no,odd,even,mark,space 
        private byte _stopBits = 1;					// 0,1,2 = 1, 1.5, 2 
        private int _readTimeout = 10;				// 端口参数
        //private int hComm = -1;						// win32函数句柄

        #endregion

        #region 公共属性
        /// <summary>
        /// 连接对象的名称
        /// </summary>
        public string CommName
        {
            get
            {
                return this._portNum;
            }
        }
        /// <summary>
        /// 连接对象的端口号或者是波特率
        /// </summary>
        public int CommPort
        {
            get
            {
                return this._baudRate;
            }
        }
        /// <summary>
        /// 获取和设置串口名称,默认为COM1
        /// </summary>
        public string PortNum
        {
            set { this._portNum = value; }
            get { return this._portNum; }
        }

        /// <summary>
        /// 获取和设置波特率,默认为9600
        /// </summary>
        public int BaudRate
        {
            set { this._baudRate = value; }
            get { return this._baudRate; }
        }

        /// <summary>
        /// 获取和设置数据位,默认为8
        /// </summary>
        public byte ByteSize
        {
            set { this._byteSize = value; }
            get { return this._byteSize; }
        }

        /// <summary>
        /// 获取和设置奇偶校验位,默认为0
        /// <remark>
        /// 0-4分别表示(无),(奇校验),(偶校验),(标记),(空格)
        /// </remark>
        /// </summary>
        public string Parity
        {
            set { this._parity = value; }
            get { return this._parity; }
        }

        /// <summary>
        /// 获取和设置停止位,默认为0
        /// <remark>
        /// 0-2分别表示1, 1.5, 2
        /// </remark>
        /// </summary>
        public byte StopBits // 0,1,2 = 1, 1.5, 2 
        {
            set { this._stopBits = value; }
            get { return this._stopBits; }
        }

        /// <summary>
        /// 获取和设置超时时限,默认为10
        /// </summary>
        public int ReadTimeout
        {
            set { this._readTimeout = value; }
            get { return this._readTimeout; }
        }

        /// <summary>
        /// 当前串口是否打开
        /// </summary>
        public bool IsConnected
        {
            get { return this._isOpened; }
        }

        /// <summary>
        /// 获取和设置发送数据的超时时限,默认为500毫秒
        /// </summary>
        public int SendDataTimeOut
        {
            get { return _sendDataTimeout; }
            set { _sendDataTimeout = value; }
        }
        /// <summary>
        /// 发送数据的重试次数
        /// </summary>
        public int ReSendNum
        {
            get { return _reSendNum; }
            set { _reSendNum = value; }
        }
        #endregion

        #region 封装的API函数
        /// <summary>
        /// 创建一个串口
        /// </summary>
        /// <param name="lpFileName">要打开的串口名称 </param>
        /// <param name="dwDesiredAccess">指定串口的访问方式，一般设置为可读可写方式 </param>
        /// <param name="dwShareMode">指定串口的共享模式，串口不能共享，所以设置为0 </param>
        /// <param name="lpSecurityAttributes">设置串口的安全属性，WIN9X下不支持，应设为NULL </param>
        /// <param name="dwCreationDisposition">对于串口通信，创建方式只能为OPEN_EXISTING </param>
        /// <param name="dwFlagsAndAttributes">指定串口属性与标志，设置为FILE_FLAG_OVERLAPPED(重叠I/O操作)，指定串口以异步方式通信 </param>
        /// <param name="hTemplateFile">对于串口通信必须设置为NULL </param>
        /// <returns>句柄</returns>
        [DllImport("kernel32.dll")]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes,
            int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

        /// <summary>
        /// 获取串口状态
        /// </summary>
        /// <param name="hFile">通信设备句柄 </param>
        /// <param name="lpDCB">设备控制块DCB </param>
        /// <returns>获取是否成功</returns>
        [DllImport("kernel32.dll")]
        private static extern bool GetCommState(int hFile, ref DCB lpDCB);

        /// <summary>
        /// 新建一个设备控制块
        /// </summary>
        /// <param name="lpDef">设备控制字符串 </param>
        /// <param name="lpDCB">设备控制块DCB</param>
        /// <returns>是否成功</returns>
        [DllImport("kernel32.dll")]
        private static extern bool BuildCommDCB(string lpDef, ref DCB lpDCB);

        /// <summary>
        /// 设置串口状态
        /// </summary>
        /// <param name="hFile">通信设备句柄 </param>
        /// <param name="lpDCB">设备控制块 </param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetCommState(int hFile, ref DCB lpDCB);

        /// <summary>
        /// 获取串口超时参数
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpCommTimeouts">超时时间</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool GetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        /// <summary>
        /// 设置串口超时参数
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpCommTimeouts">超时时间</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool SetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpBuffer">数据缓冲</param>
        /// <param name="nNumberOfBytesToRead">多少字节等待读取</param>
        /// <param name="lpNumberOfBytesRead">读取多少字节</param>
        /// <param name="lpOverlapped">溢出缓冲区</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool ReadFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="hFile">通信设备句柄</param>
        /// <param name="lpBuffer">数据缓冲区</param>
        /// <param name="nNumberOfBytesToWrite">多少字节等待写入</param>
        /// <param name="lpNumberOfBytesWritten">已经写入多少字节</param>
        /// <param name="lpOverlapped">溢出缓冲区</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);

        /// <summary>
        /// 关闭对象句柄
        /// </summary>
        /// <param name="hObject">对象句柄</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(int hObject);

        /// <summary>
        /// 获取上一个容错信息
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();
        /// <summary>
        /// 清空串口缓冲区
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern bool PurgeComm(
            int hFile,                              // 通信设备句柄 handle to comm device
            int dwFlags);                           // 需要完成的操作
       #endregion

        #region 构造函数
        /// <summary>
        /// Rs485设备通讯管理类
        /// </summary>
        /// <param name="portNum">串口名称</param>
        /// <param name="baudRate">波特率</param>
        public SerialsComLib(string portNum, int baudRate, byte byteSize, string parity, byte stopBits)
        {
            this._portNum = portNum;
            this._baudRate = baudRate;
            this._byteSize = byteSize;
            this._parity = parity;
            this._stopBits = stopBits;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 打开串口
        /// </summary>
        public bool Connect(ref int commHandle)
        {
            if (this._isOpened) return true;

            // 创建一个设备控制块和一个超时参数
            DCB dcbCommPort = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();

            // 打开串口
            commHandle = CreateFile(PortNum, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);

            // 如果串口没有打开，则返回false
            if (commHandle == INVALID_HANDLE_VALUE)
            {
                this._isOpened = false;
                return false;
            }

            char[] szBaud = new char[50];
            dcbCommPort.flags = 0;
            // 设置通信超时时间 SET THE COMM TIMEOUTS.
            GetCommTimeouts(commHandle, ref ctoCommPort);
            ctoCommPort.ReadIntervalTimeout = 300;
            ctoCommPort.ReadTotalTimeoutConstant = 10;
            ctoCommPort.ReadTotalTimeoutMultiplier = 10;
            ctoCommPort.WriteTotalTimeoutMultiplier = 10;
            ctoCommPort.WriteTotalTimeoutConstant = 10;
            SetCommTimeouts(commHandle, ref ctoCommPort);
            if (dcbCommPort.fParity != 1)
                dcbCommPort.fParity = 1;
            // 设置串口 SET BAUD RATE, PARITY, WORD SIZE, AND STOP BITS.
            string szbaud = "baud=" + BaudRate + " parity=" + Parity + " data=" + ByteSize + " stop=" + StopBits;
            dcbCommPort.DCBlength = Marshal.SizeOf(dcbCommPort);
            if (GetCommState(commHandle, ref dcbCommPort))
            {
                if (BuildCommDCB(szbaud, ref dcbCommPort))
                {
                    if (SetCommState(commHandle, ref dcbCommPort))
                    {
                        ; // normal operation... continue
                    }
                    else
                    {
                        throw (new ApplicationException("串口参数设置失败SetCommState！"));
                    }
                }
                else
                {
                    throw (new ApplicationException("串口参数设置失败BuildCommDCB！"));
                }
            }
            else
            {
                throw (new ApplicationException("串口参数设置失败GetCommState！"));
            }
            this._isOpened = true;
            return true;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close(int commHandle)
        {
            if (commHandle != INVALID_HANDLE_VALUE)
            {
                CloseHandle(commHandle);
                this._isOpened = false;
                commHandle = INVALID_HANDLE_VALUE;
            }

        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="numBytes">要读取的字节数</param>
        /// <returns>字节数组</returns>
        public byte[] Read(int commHandle,int numBytes)
        {
            // 创建中间变量存储数据
            byte[] BufBytes;
            byte[] OutBytes = null;
            BufBytes = new byte[numBytes];

            // 读取数据并返回
            if (commHandle != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                ReadFile(commHandle, BufBytes, numBytes, ref BytesRead, ref ovlCommPort);
                OutBytes = new byte[BytesRead];
                Array.Copy(BufBytes, OutBytes, BytesRead);
            }
            else
            {
                throw (new ApplicationException("串口未打开！"));
            }
            return OutBytes;
        }
        /// <summary>
        /// 清空串口缓冲区
        /// </summary>
        /// <param name="date">句柄</param>
        public void ClearComBuffer(int commHandle)
        {
            if (commHandle != INVALID_HANDLE_VALUE)
            {
                PurgeComm(commHandle, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
            }
        }
        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="date">数据</param>
        public int Send(int commHandle,byte[] date)
        {
            if (commHandle != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesWritten = 0;
                WriteFile(commHandle, date, date.Length, ref BytesWritten, ref ovlCommPort);
                Console.WriteLine("SerialsBSTLib发送:" + BitConverter.ToString(date));
                return BytesWritten;
            }
            else
            {
                throw (new ApplicationException("无法打开串口,可能该串口不存在或者已被其他应用程序使用！"));
            }

        }
        /// <summary>
        /// 把字节型转换成十六进制字符串
        /// </summary>
        public string ByteToString(byte[] InBytes)
        {
            string StringOut = "";
            foreach (byte InByte in InBytes)
            {
                StringOut = StringOut + String.Format("{0:X2} ", InByte);
            }
            return StringOut;
        }
        /// <summary>
        /// 把十六进制字符串转换成字节型
        /// </summary>
        public byte[] StringToByte(string InString)
        {
            int Length;
            byte[] ByteOut;
            string Instr = delspace(InString);
            if ((InString.Length - 1) / 2 < 1)
                Length = 1;
            else
            {
                Length = (InString.Length - 1) / 2;
            }
            ByteOut = new byte[Length];
            int j = 0;
            for (int i = 0; i < Instr.Length; i = i + 2, j++)
            {

                ByteOut[j] = Convert.ToByte(Instr.Substring(i, 2), 16);

            }
            return ByteOut;
        }
        /// <summary>
        /// LRC校验--16进制转换为ASCII码
        /// </summary>
        public byte HextoAscII(byte Hex)
        {
            int hex = 0;
            byte hextoAsc = 0;
            if (Hex >= 0 && Hex <= 9)
            {
                hex = Hex + 0x30;
            }
            else
            {
                hex = Hex + 0x41 - 10;
            }
            hextoAsc = (byte)hex;
            return hextoAsc;
        }
        /// <summary>
        /// LRC校验--ASCII码转换为16进制
        /// </summary>
        public byte AscIItoHex(byte AscII)
        {
            int hex = 0;
            byte hextoAsc = 0;
            if (AscII >= 0x30 && AscII <= 0x39)
            {
                hex = AscII - 0x30;
            }
            else
            {
                hex = AscII - 0x41 + 10;
            }
            hextoAsc = (byte)hex;
            return hextoAsc;
        }
        /// <summary>
        /// LRC校验
        /// </summary>
        public byte[] LrcCheck(byte[] LrcCk)
        {
            int lrcAdd = 0;
            int lrcResult = 0;
            byte[] LrcReturn = new byte[2] { 0, 0 };
            for (int i = 1; i <15; i+=2)
            {
                lrcAdd += (AscIItoHex(LrcCk[i]) * 16 + AscIItoHex(LrcCk[i + 1]));
            }
            lrcResult = lrcAdd % 256;
            lrcResult = (~lrcResult)+1;
            string dee = Convert.ToString(lrcResult, 16);
            LrcReturn[0] = HextoAscII(Convert.ToByte(dee.Substring(6, 1), 16));
            LrcReturn[1] = HextoAscII(Convert.ToByte(dee.Substring(7, 1), 16));
            return LrcReturn;
        }
        /// <summary>
        /// CRC校验
        /// </summary>
        public uint gen_crc(byte[] frame)
        {
            int frame_len = frame.Length;

            byte c;
            uint treat, bcrc;
            uint wcrc = 0;

            for (int i = 7; i < 15; i++)
            {
                c = frame[i];

                for (int j = 0; j < 8; j++)
                {
                    treat = c & 0x80U;
                    c <<= 1;
                    bcrc = (wcrc >> 8) & 0x80;
                    wcrc <<= 1;
                    if (treat != bcrc)
                    {
                        wcrc ^= 0x1021;
                    }
                }
            }

            return wcrc;
        } 
        /// <summary>
        /// 异或校验
        /// </summary>
        public byte[] XonCheck(byte[] XonCk,int Start,int End)
        {
            int XonAdd = 0;
            byte[] LrcReturn = new byte[2] { 0, 0 };
            for (int i = Start; i <=End; i++)
            {
                XonAdd = XonAdd ^ (Convert.ToInt32(XonCk[i]));
            }
            string dee = Convert.ToString(XonAdd, 16);
            LrcReturn[0] = HextoAscII(Convert.ToByte(dee.Substring(dee.Length-2, 1), 16));
            LrcReturn[1] = HextoAscII(Convert.ToByte(dee.Substring(dee.Length-1, 1), 16));
            return LrcReturn;
        }
        /// <summary>
        /// 去掉发送数组中的空格
        /// </summary>
        public string delspace(string putin)
        {
            string putout = "";
            for (int i = 0; i < putin.Length; i++)
            {
                if (putin[i] != ' ')
                    putout += putin[i];
            }
            return putout;
        }

        #endregion

    }
    
}
