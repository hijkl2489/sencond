using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace YGJZJL.StaticTrack
{
    public class SerialCommlibMulti
    {
        //WeighMeasureInfo aa = new WeighMeasureInfo();
        public string PortNum;
        public int BaudRate;
        public byte ByteSize;
        public string Parity;
        public byte StopBits;
        public int ReadTimeout;

        public int hComm = -1;//comm port win32 file handle
        public int hComm1 = -1;//comm port win32 file handle
        public int hComm2 = -1;//comm port win32 file handle

        public bool commThreadAlive1 = false;
        public bool commThreadAlive2 = false;

        //数据传送方式
        public bool bDataSend = false;

        private double weightPhyNum1 = 0;
        private double weightPhyNum2 = 0;
        private double commcount = 0;

        private int commCount1 = 0;
        private int commCount2 = 0;

        public int Tran;//10,50,100,500,1000

        public bool commThreadAlive = true;

        //win32 api constants
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

        public string WeightType;                  //仪表类型。
        private double inResult = 0;                    //返回结果。
        private double CFCTlResult = 0;
        private double CFCMVResult = 0;
        public string ErrorMsg;                    //返回错误信息。
        public double CommCount;                  //通信状态
        private Thread commThread;                 //串口监控线程
        private string comm3190ReadString = "02 41 42 30 33 03"; //3190仪表读数据命令
        private string comm3190ZeroString = "02 41 48 30 39 03"; //3190仪表置零命令
        private string comm8142ReadString = "50";                //8142仪表读数据命令
        private string comm8142ZeroString = "5A";                //8142仪表置零命令
        private string commCFC100ReadString = "05 32 0D"; //3190仪表读数据命令
        private byte[] WriteBufferBytes;                         //写串口数组
        private string commMethod = "read";                        //仪表操作方法，默认读数据
        public event NumReceivedEvent NumReceived;               //数据接收事件

        #region 串口动态链接库的声明
        [StructLayout(LayoutKind.Sequential)]
        private struct DCB
        {
            //taken from c struct in platform sdk 
            public int DCBlength;           // sizeof(DCB) 
            public int BaudRate;            // 指定当前波特率 current baud rate
            // these are the c struct bit fields, bit twiddle flag to set
            public int fBinary;             // 指定是否允许二进制模式,在windows95中必须主TRUE binary mode, no EOF check 
            public int fParity;             // 指定是否允许奇偶校验 enable parity checking 
            public int fOutxCtsFlow;        // 指定CTS是否用于检测发送控制，当为TRUE是CTS为OFF，发送将被挂起。 CTS output flow control 
            public int fOutxDsrFlow;        // 指定CTS是否用于检测发送控制 DSR output flow control 
            public int fDtrControl;         // DTR_CONTROL_DISABLE值将DTR置为OFF, DTR_CONTROL_ENABLE值将DTR置为ON, DTR_CONTROL_HANDSHAKE允许DTR"握手" DTR flow control type 
            public int fDsrSensitivity;     // 当该值为TRUE时DSR为OFF时接收的字节被忽略 DSR sensitivity 
            public int fTXContinueOnXoff;   // 指定当接收缓冲区已满,并且驱动程序已经发送出XoffChar字符时发送是否停止。TRUE时，在接收缓冲区接收到缓冲区已满的字节XoffLim且驱动程序已经发送出XoffChar字符中止接收字节之后，发送继续进行。　FALSE时，在接收缓冲区接收到代表缓冲区已空的字节XonChar且驱动程序已经发送出恢复发送的XonChar之后，发送继续进行。XOFF continues Tx 
            public int fOutX;               // TRUE时，接收到XoffChar之后便停止发送接收到XonChar之后将重新开始 XON/XOFF out flow control 
            public int fInX;                // TRUE时，接收缓冲区接收到代表缓冲区满的XoffLim之后，XoffChar发送出去接收缓冲区接收到代表缓冲区空的XonLim之后，XonChar发送出去 XON/XOFF in flow control 
            public int fErrorChar;          // 该值为TRUE且fParity为TRUE时，用ErrorChar 成员指定的字符代替奇偶校验错误的接收字符 enable error replacement 
            public int fNull;               // eTRUE时，接收时去掉空（0值）字节 enable null stripping 
            public int fRtsControl;         // RTS flow control 
            /*RTS_CONTROL_DISABLE时,RTS置为OFF  RTS_CONTROL_ENABLE时, RTS置为ON RTS_CONTROL_HANDSHAKE时,当接收缓冲区小于半满时RTS为ON  当接收缓冲区超过四分之三满时RTS为OFF
             RTS_CONTROL_TOGGLE时,当接收缓冲区仍有剩余字节时RTS为ON ,否则缺省为OFF*/

            public int fAbortOnError;       // TRUE时,有错误发生时中止读和写操作 abort on error 
            public int fDummy2;             // 未使用 reserved 

            public uint flags;
            public ushort wReserved;        // 未使用,必须为0 not currently used 
            public ushort XonLim;           // 指定在XON字符发送这前接收缓冲区中可允许的最小字节数 transmit XON threshold 
            public ushort XoffLim;          // 指定在XOFF字符发送这前接收缓冲区中可允许的最小字节数 transmit XOFF threshold 
            public byte ByteSize;           // 指定端口当前使用的数据位	number of bits/byte, 4-8 
            public byte Parity;             // 指定端口当前使用的奇偶校验方法,可能为:EVENPARITY,MARKPARITY,NOPARITY,ODDPARITY  0-4=no,odd,even,mark,space 
            public byte StopBits;           // 指定端口当前使用的停止位数,可能为:ONESTOPBIT,ONE5STOPBITS,TWOSTOPBITS  0,1,2 = 1, 1.5, 2 
            public char XonChar;            // 指定用于发送和接收字符XON的值 Tx and Rx XON character 
            public char XoffChar;           // 指定用于发送和接收字符XOFF值 Tx and Rx XOFF character 
            public char ErrorChar;          // 本字符用来代替接收到的奇偶校验发生错误时的值 error replacement character 
            public char EofChar;            // 当没有使用二进制模式时,本字符可用来指示数据的结束 end of input character 
            public char EvtChar;            // 当接收到此字符时,会产生一个事件 received event character 
            public ushort wReserved1;       // 未使用 reserved; do not use 
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct COMSTAT
        {

            public int cbInQue;
            public int cbOutQue;
        }

        [DllImport("kernel32.dll")]
        private static extern int CreateFile(
          string lpFileName,                        // 要打开的串口名称
          uint dwDesiredAccess,                     // 指定串口的访问方式，一般设置为可读可写方式
          uint dwShareMode,                         // 指定串口的共享模式，串口不能共享，所以设置为0
          int lpSecurityAttributes,                 // 设置串口的安全属性，WIN9X下不支持，应设为NULL
          uint dwCreationDisposition,               // 对于串口通信，创建方式只能为OPEN_EXISTING
          uint dwFlagsAndAttributes,                // 指定串口属性与标志，设置为FILE_FLAG_OVERLAPPED(重叠I/O操作)，指定串口以异步方式通信
          int hTemplateFile                         // 对于串口通信必须设置为NULL
        );
        [DllImport("kernel32.dll")]
        private static extern bool GetCommState(
          int hFile,                                // 通信设备句柄
          ref DCB lpDCB                             // 设备控制块DCB
        );
        [DllImport("kernel32.dll")]
        private static extern bool BuildCommDCB(
          string lpDef,                             // 设备控制字符串
          ref DCB lpDCB                             // 设备控制块
        );
        [DllImport("kernel32.dll")]
        private static extern bool SetCommState(
          int hFile,                                // 通信设备句柄
          ref DCB lpDCB                             // 设备控制块
        );
        [DllImport("kernel32.dll")]
        private static extern bool GetCommTimeouts(
          int hFile,                                // 通信设备句柄 handle to comm device
          ref COMMTIMEOUTS lpCommTimeouts           // 超时时间 time-out values
        );
        [DllImport("kernel32.dll")]
        private static extern bool SetCommTimeouts(
          int hFile,                                // 通信设备句柄 handle to comm device
          ref COMMTIMEOUTS lpCommTimeouts           // 超时时间 time-out values
        );
        [DllImport("kernel32.dll")]
        private static extern bool ReadFile(
          int hFile,                               // 通信设备句柄 handle to file
          byte[] lpBuffer,                         // 数据缓冲区 data buffer
          int nNumberOfBytesToRead,                // 多少字节等待读取 number of bytes to read
          ref int lpNumberOfBytesRead,             // 读取多少字节 number of bytes read
          ref OVERLAPPED lpOverlapped              // 溢出缓冲区 overlapped buffer
        );
        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(
          int hFile,                               // 通信设备句柄 handle to file
          byte[] lpBuffer,                         // 数据缓冲区 data buffer
          int nNumberOfBytesToWrite,               // 多少字节等待写入 number of bytes to write
          ref int lpNumberOfBytesWritten,          // 已经写入多少字节 number of bytes written
          ref OVERLAPPED lpOverlapped              // 溢出缓冲区 overlapped buffer
        );
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(
          int hObject                               // handle to object
        );
        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();  // 获取错误编码

        [DllImport("kernel32.dll")]
        private static extern bool PurgeComm(
            int hFile,                              // 通信设备句柄 handle to comm device
            int dwFlags);                           // 需要完成的操作
        #endregion

        #region 串口初始化方法（打开、关闭）
        /// <summary>
        /// 串口初始化，打开串口。
        /// </summary>
        public bool InitPort()
        {
            DCB dcbCommPort = new DCB();
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();
            //// 如果串口没有打开，就打开 IF THE PORT CANNOT BE OPENED, BAIL OUT.
            //if (hComm != INVALID_HANDLE_VALUE)
            //{
            //    CloseHandle(hComm);
            //}
            // 打开串口 OPEN THE COMM PORT.
            hComm = CreateFile(PortNum, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
            if (hComm != INVALID_HANDLE_VALUE)
            {
                char[] szBaud = new char[50];
                dcbCommPort.flags = 0;
                // 设置通信超时时间 SET THE COMM TIMEOUTS.
                GetCommTimeouts(hComm, ref ctoCommPort);
                ctoCommPort.ReadIntervalTimeout = 300;
                ctoCommPort.ReadTotalTimeoutConstant = 10;
                ctoCommPort.ReadTotalTimeoutMultiplier = 10;
                ctoCommPort.WriteTotalTimeoutMultiplier = 10;
                ctoCommPort.WriteTotalTimeoutConstant = 10;
                SetCommTimeouts(hComm, ref ctoCommPort);
                if (dcbCommPort.fParity != 1)
                    dcbCommPort.fParity = 1;
                // 设置串口 SET BAUD RATE, PARITY, WORD SIZE, AND STOP BITS.
                string szbaud = "baud=" + BaudRate + " parity=" + Parity + " data=" + ByteSize + " stop=" + StopBits;
                dcbCommPort.DCBlength = Marshal.SizeOf(dcbCommPort);
                if (GetCommState(hComm, ref dcbCommPort))
                {
                    if (BuildCommDCB(szbaud, ref dcbCommPort))
                    {
                        if (SetCommState(hComm, ref dcbCommPort))
                        {
                            ; // normal operation... continue
                        }
                        else
                        {
                            uint ErrorNum = GetLastError();
                            ErrorMsg = "串口参数设置失败SetCommState！" + ErrorNum;
                        }
                    }
                    else
                    {
                        uint ErrorNum = GetLastError();
                        ErrorMsg = "串口参数设置失败BuildCommDCB！" + ErrorNum;
                    }
                }
                else
                {
                    uint ErrorNum = GetLastError();
                    ErrorMsg = "串口参数获取失败GetCommState！" + ErrorNum;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public bool Close(int Commh)
        {
            if (Commh != INVALID_HANDLE_VALUE)
            {
                CloseHandle(Commh);
                Commh = INVALID_HANDLE_VALUE;
            }
            return true;

        }
        #endregion

        #region 串口读写及监控线程
        /// <summary>
        /// 监控读写串口线程
        /// </summary>
        public void CommThreadPro()
        {
            //if (hComm != -1)
            //{
            //    PurgeComm(hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
            //}
            //do
            //{
            //    if (hComm != -1)
            //    {
            //        //ControlComm(WeightType);
            //        //if (commThread.IsAlive)
            //        Thread.Sleep(150);
            //    }

            //} while (commThreadAlive);

        }

        #region 串口读写XK2001
        /// <summary>
        /// 串口读写3190-2
        /// </summary>
        public void ControlCommXK2001()
        {
            bool bGet = false;
            byte[] recb;
            double doubTmp = 0;

            do
            {
                if (hComm2 != -1)
                {
                    string disTmp = "";
                    try
                    {
                        //读取串口数据。
                        recb = Read(20, hComm2);
                        if (recb.Length > 0)
                        {
                            int j = 0;
                            for (int i = 0; i < 19; i++)
                            {
                                if ((recb[i] == 61) && (!bGet))
                                {
                                    bGet = true;
                                    continue;
                                }

                                if ((recb[i] == 61) && (bGet))
                                {
                                    bGet = false;
                                    break;
                                }

                                if (bGet)
                                {
                                    if ((recb[i] - 48) < 0)
                                    {
                                        disTmp += 0;
                                    }
                                    else
                                    {
                                        disTmp += recb[i] - 48;
                                    }
                                    j++;
                                    if (j == 8) break;
                                }
                            }

                            char[] mychars = disTmp.ToCharArray();
                            Array.Reverse(mychars);
                            disTmp = mychars[2].ToString() + mychars[3].ToString() + mychars[4].ToString() + "." + mychars[5].ToString() + mychars[6].ToString() + mychars[7].ToString();
                            doubTmp = Convert.ToDouble(disTmp);

                            if (mychars[2] == 45)
                                doubTmp = doubTmp * -1;
                            else
                            {
                                doubTmp = doubTmp * 1;
                            }

                            try
                            {
                                weightPhyNum2 = doubTmp;
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg = "读取数据格式错误！" + ex.Message;
                            }
                        }
                    }
                    catch
                    {
                        ErrorMsg = "发送失败!";
                    }

                    if (weightPhyNum2 != 0)
                    {
                        commCount2 += 1;
                        if (commCount2 >= 1000000)
                        {
                            commCount2 = 0;
                        }
                    }
                    if (NumReceived != null)
                    {
                        NumberReceivedEventArgs e = new NumberReceivedEventArgs(weightPhyNum1, weightPhyNum2, commCount1, commCount2);
                        NumReceived(null, e);

                    }

                    //PurgeComm(hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                    PurgeComm(hComm2, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                }

            } while (commThreadAlive2);
        }

        public void ControlCommXK2001a()
        {
            bool bGet = false;
            byte[] recb;
            double doubTmp = 0;

            do
            {
                if (hComm1 != -1)
                {
                    string disTmp = "";
                    try
                    {
                        //读取串口数据。
                        recb = Read(20, hComm1);
                        if (recb.Length > 0)
                        {
                            int j = 0;
                            for (int i = 0; i < 19; i++)
                            {
                                if ((recb[i] == 61) && (!bGet))
                                {
                                    bGet = true;
                                    continue;
                                }

                                if ((recb[i] == 61) && (bGet))
                                {
                                    bGet = false;
                                    break;
                                }

                                if (bGet)
                                {
                                    if ((recb[i] - 48) < 0)
                                    {
                                        disTmp += 0;
                                    }
                                    else
                                    {
                                        disTmp += recb[i] - 48;
                                    }
                                    j++;
                                    if (j == 8) break;
                                }
                            }

                            char[] mychars = disTmp.ToCharArray();
                            Array.Reverse(mychars);
                            disTmp = mychars[2].ToString() + mychars[3].ToString() + mychars[4].ToString() + "." + mychars[5].ToString() + mychars[6].ToString() + mychars[7].ToString();
                            doubTmp = Convert.ToDouble(disTmp);

                            if (mychars[2] == 45)
                                doubTmp = doubTmp * -1;
                            else
                            {
                                doubTmp = doubTmp * 1;
                            }

                            try
                            {
                                weightPhyNum1 = doubTmp;
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg = "读取数据格式错误！" + ex.Message;
                            }
                        }
                    }
                    catch
                    {
                        ErrorMsg = "发送失败!";
                    }

                    if (weightPhyNum1 != 0)
                    {
                        commCount1 += 1;
                        if (commCount1 >= 1000000)
                        {
                            commCount1 = 0;
                        }
                    }
                    if (NumReceived != null)
                    {
                        NumberReceivedEventArgs e = new NumberReceivedEventArgs(weightPhyNum1, weightPhyNum2, commCount1, commCount2);
                        NumReceived(null, e);

                    }

                    //PurgeComm(hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                    PurgeComm(hComm1, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                }

            } while (commThreadAlive1);
        }

        public void ControlCommXK2001b()
        {
            bool bGet = false;
            byte[] recb;
            double doubTmp = 0;

            do
            {
                if (hComm1 != -1)
                {
                    string disTmp = "";
                    try
                    {
                        //读取串口数据。
                        recb = Read(27, hComm1);
                        if (recb.Length > 0)
                        {
                            int j = 0;
                            for (int i = 0; i < 26; i++)
                            {
                                if ((recb[i] == 2) && (!bGet))
                                {
                                    bGet = true;
                                    continue;
                                }

                                if ((recb[i] == 2) && (bGet))
                                {
                                    bGet = false;
                                    break;
                                }

                                if (bGet)
                                {
                                    if ((recb[i] - 48) < 0)
                                    {
                                        disTmp += 0;
                                    }
                                    else
                                    {
                                        disTmp += recb[i] - 48;
                                    }
                                    j++;
                                    if (j == 12) break;
                                }
                            }

                            char[] mychars = disTmp.ToCharArray();
                            //Array.Reverse(mychars);
                            disTmp = mychars[1].ToString() + mychars[2].ToString() + mychars[3].ToString() + "." + mychars[4].ToString() + mychars[5].ToString() + mychars[6].ToString();
                            doubTmp = Convert.ToDouble(disTmp);

                            if (mychars[0] == 45)
                                doubTmp = doubTmp * -1;
                            else
                            {
                                doubTmp = doubTmp * 1;
                            }

                            try
                            {
                                weightPhyNum1 = doubTmp;
                            }
                            catch (Exception ex)
                            {
                                ErrorMsg = "读取数据格式错误！" + ex.Message;
                            }
                        }
                    }
                    catch
                    {
                        ErrorMsg = "发送失败!";
                    }

                    if (weightPhyNum1 != 0)
                    {
                        commCount1 += 1;
                        if (commCount1 >= 1000000)
                        {
                            commCount1 = 0;
                        }
                    }
                    if (NumReceived != null)
                    {
                        NumberReceivedEventArgs e = new NumberReceivedEventArgs(weightPhyNum1, weightPhyNum2, commCount1, commCount2);
                        NumReceived(null, e);

                    }

                    //PurgeComm(hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                    PurgeComm(hComm1, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                }

            } while (commThreadAlive1);
        }
        #endregion

        #region 串口读写3190
        /// <summary>
        /// 串口读写3190-2
        /// </summary>
        /// 命令行方式
        public void ControlComm3190()
        {
            byte[] recb;
            string disresult = "";
            //switch (weightType)
            //{
            //    case "3190": //仪表类型为“3190”，读写入参数格式。
            do
            {
                if (hComm2 != -1)
                {
                    //if (commThread.IsAlive)
                    //Thread.Sleep(150);

                    //连续方式
                    if (!bDataSend)
                    {
                        if (commMethod == "setzero")
                        {
                            WriteBufferBytes = StringToByte(comm3190ZeroString);
                            Write(WriteBufferBytes, hComm2);
                            commMethod = "read";

                        }
                        else
                        {
                            WriteBufferBytes = StringToByte(comm3190ReadString);
                        }
                        //发送读数据命令。
                        Write(WriteBufferBytes, hComm2);
                    }
                    try
                    {
                        //读取串口数据。
                        recb = Read(80, hComm2);
                        if (recb.Length > 0)
                        {
                            disresult = dis(recb, recb.Length, 4, 6, 3, 10, true);
                            if (disresult != "")
                                try
                                {
                                    weightPhyNum2 = Convert.ToDouble(disresult);
                                }
                                catch (Exception ex)
                                {
                                    ErrorMsg = "读取数据格式错误！" + ex.Message;
                                }
                        }
                    }
                    catch
                    {
                        ErrorMsg = "发送失败!";
                    }

                    if (weightPhyNum2 != 0)
                    {
                        commCount2 += 1;
                        if (commCount2 >= 1000000)
                        {
                            commCount2 = 0;
                        }
                    }
                    if (NumReceived != null)
                    {
                        NumberReceivedEventArgs e = new NumberReceivedEventArgs(weightPhyNum1, weightPhyNum2, commCount1, commCount2);
                        NumReceived(null, e);

                    }

                    //PurgeComm(hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                    PurgeComm(hComm2, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                }

            } while (commThreadAlive2);
        }
        #endregion

        #region 串口读写8142

        /// <summary>
        /// 串口读写8142-7
        /// </summary>
        /// 命令行方式
        public void ControlComm814201()
        {
            byte[] recb;
            string disresult = "";
            do
            {
                try
                {
                    if (hComm1 != -1)
                    {
                        //if (commThread.IsAlive)
                        Thread.Sleep(150);

                        if (commMethod == "setzero")
                        {
                            WriteBufferBytes = StringToByte(comm8142ZeroString);
                            Write(WriteBufferBytes, hComm1);
                            commMethod = "read";
                        }
                        else
                        {
                            WriteBufferBytes = StringToByte(comm8142ReadString);
                        }
                        //发送读数据命令。
                        Write(WriteBufferBytes, hComm1);
                        //读取串口数据。
                        recb = Read(200, hComm1);
                        if (recb.Length > 0)
                        {
                            disresult = dis(recb, recb.Length, 67, 6, 0, 0, false);
                            if (disresult != "")
                                try
                                {
                                    weightPhyNum1 = Convert.ToDouble(disresult);
                                }
                                catch (Exception ex)
                                {
                                    ErrorMsg = "读取数据格式错误！" + ex.Message;
                                }
                        }
                        else
                        {
                            ErrorMsg = "读取失败!";
                        }
                    }
                }
                catch
                {
                    ErrorMsg = "读取失败!";
                }

                if (weightPhyNum1 != 0)
                {
                    commCount1 += 1;
                    if (commCount1 >= 1000000)
                    {
                        commCount1 = 0;
                    }
                }
                if (NumReceived != null)
                {
                    NumberReceivedEventArgs e = new NumberReceivedEventArgs(weightPhyNum1, weightPhyNum2, commCount1, commCount2);
                    NumReceived(null, e);

                }
                PurgeComm(hComm1, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
            } while (commThreadAlive1);
        }

        public void ControlComm814201(bool bLx)
        {
            byte[] recb;
            string disresult = "";
            do
            {
                try
                {
                    if (hComm1 != -1)
                    {
                        //if (commThread.IsAlive)
                        Thread.Sleep(50);

                        //读取串口数据。
                        recb = Read(200, hComm1);
                        if (recb.Length > 0)
                        {
                            disresult = dis(recb, recb.Length, 67, 6, 0, 0, false);
                            if (disresult != "")
                                try
                                {
                                    weightPhyNum1 = Convert.ToDouble(disresult);
                                }
                                catch (Exception ex)
                                {
                                    ErrorMsg = "读取数据格式错误！" + ex.Message;
                                }
                        }
                        else
                        {
                            ErrorMsg = "读取失败!";
                        }
                    }
                }
                catch
                {
                    ErrorMsg = "读取失败!";
                }

                if (weightPhyNum1 != 0)
                {
                    commCount1 += 1;
                    if (commCount1 >= 1000000)
                    {
                        commCount1 = 0;
                    }
                }
                if (NumReceived != null)
                {
                    NumberReceivedEventArgs e = new NumberReceivedEventArgs(weightPhyNum1, weightPhyNum2, commCount1, commCount2);
                    NumReceived(null, e);

                }
                PurgeComm(hComm1, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
            } while (commThreadAlive1);
        }
        #endregion

        #region 串口读写CFC-100
        /// <summary>
        /// 串口读写CFC-100
        /// </summary>
        public void ControlCommCFC()
        {
            byte[] recb;
            do
            {
                if (hComm1 != -1)
                {
                    if (commMethod == "setzero")
                    {
                        WriteBufferBytes = StringToByte(commCFC100ReadString);
                        Write(WriteBufferBytes, hComm1);
                        commMethod = "read";
                    }
                    else
                    {
                        WriteBufferBytes = StringToByte(commCFC100ReadString);
                    }
                    //发送读数据命令。
                    Write(WriteBufferBytes, hComm1);
                    //读取串口数据。
                    recb = Read(200, hComm1);
                    if (recb.Length > 0)
                    {
                        DisCFCWeightData(recb, ref weightPhyNum1, ref CFCMVResult);
                    }
                    else
                    {
                        ErrorMsg = "读取失败!";
                    }


                    if (inResult != 0 || weightPhyNum1 != 0)
                    {
                        commCount1 += 1;
                        if (commCount1 >= 1000000)
                        {
                            commCount1 = 0;
                        }
                    }
                    if (NumReceived != null)
                    {
                        NumberReceivedEventArgs e = new NumberReceivedEventArgs(weightPhyNum1, weightPhyNum2, commCount1, commCount2);
                        NumReceived(null, e);

                    }
                    //PurgeComm(hComm, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                    PurgeComm(hComm2, PURGE_RXCLEAR | PURGE_TXCLEAR | PURGE_RXABORT | PURGE_TXABORT);
                    //if (commThread.IsAlive)
                    Thread.Sleep(150);
                }

            } while (commThreadAlive1);
        }
        #endregion

        #region
        /// <summary>
        /// 写读串口
        /// </summary>
        private byte[] Read(int NumBytes, int Commh)
        {
            byte[] BufBytes;
            byte[] OutBytes;
            BufBytes = new byte[NumBytes];
            OutBytes = new byte[1];
            if (Commh != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                ReadFile(Commh, BufBytes, NumBytes, ref BytesRead, ref ovlCommPort);
                OutBytes = new byte[BytesRead];
                Array.Copy(BufBytes, OutBytes, BytesRead);
            }
            else
            {
                ErrorMsg = "串口未打开！";
            }
            return OutBytes;
        }
        /// <summary>
        /// 写串口
        /// </summary>
        private int Write(byte[] WriteBytes, int Commh)
        {
            int BytesWritten = 0;
            if (Commh != INVALID_HANDLE_VALUE)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                WriteFile(Commh, WriteBytes, WriteBytes.Length, ref BytesWritten, ref ovlCommPort);
            }
            else
            {
                ErrorMsg = "串口未打开！";
            }
            return BytesWritten;
        }
        #endregion

        #region 外部调用操作串口方法
        /// <summary>
        /// 发送置零命令到串口
        /// </summary>
        public bool SetZeroCmd()
        {
            if (commMethod == "read")
            {
                commMethod = "setzero";
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 字节转换方法
        /// <summary>
        /// 把字节型转换成十六进制字符串
        /// </summary>
        private string ByteToString(byte[] InBytes)
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
        private byte[] StringToByte(string InString)
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
        /// 去掉发送数组中的空格
        /// </summary>

        private string delspace(string putin)
        {
            string putout = "";
            for (int i = 0; i < putin.Length; i++)
            {
                if (putin[i] != ' ')
                    putout += putin[i];
            }
            return putout;
        }
        /// <summary>
        /// CFC-100仪表累计重量、瞬时流量字节转换成双整形数字
        /// </summary>
        private void DisCFCWeightData(byte[] recdw, ref double CFCTLDis, ref double CFCMVDis)
        {
            string disCFCWeightData = System.Text.Encoding.ASCII.GetString(recdw);
            int foundTLs = disCFCWeightData.IndexOf("TL");
            if (foundTLs > 1)
            {
                string STRCFCTLDis = disCFCWeightData.Substring(foundTLs + 2, 10);
                try
                {
                    CFCTLDis = Convert.ToDouble(STRCFCTLDis);
                }
                catch (Exception ex)
                {
                    ErrorMsg = "读取数据格式错误！" + ex.Message;
                }
            }
            int foundMVs = disCFCWeightData.IndexOf("MV");
            if (foundMVs > 1)
            {
                string STRCFCMVDis = disCFCWeightData.Substring(foundMVs + 2, 5);
                try
                {
                    CFCMVDis = Convert.ToDouble(STRCFCMVDis);
                }
                catch (Exception ex)
                {
                    ErrorMsg = "读取数据格式错误！" + ex.Message;
                }

            }

        }

        /// <summary>
        /// 字节转换为数字显示。
        /// <param name="recdw">读取的字节包</param>
        /// <param name="Length">包长度</param>
        /// <param name="NumStart">重量数据开始位置</param>
        /// <param name="Numsum">重量数据总位数</param>
        /// <param name="NegChar">正负号位置</param>
        /// <param name="NegChar">小数点位置</param>
        /// <param name="PointSTC">是否有小数点，TRUE:有</param>
        /// </summary>
        private string dis(byte[] recdw, int Length, int NumStart, int Numsum, int NegChar, int PointNum, bool PointSTC)
        {
            string dis = "";
            if (recdw.Length == Length && recdw[0] == 2)
            {
                for (int i = NumStart; i < NumStart + Numsum; i++)
                {
                    if ((recdw[i] - 48) < 0)
                    {
                        dis += 0;
                    }
                    else
                    {
                        dis += recdw[i] - 48;
                    }
                }
                double disint = Convert.ToDouble(dis);
                switch (NegChar)
                {
                    case 0:
                        break;
                    case 3:
                        if (recdw[3] == 45)
                            disint = disint * -1;
                        else
                        {
                            disint = disint * 1;
                        }

                        break;
                }

                if (PointSTC)
                {
                    try
                    {
                        double xdi = System.Math.Pow(10, (recdw[PointNum] - 48));
                        disint = disint / xdi;
                        dis = disint.ToString("#0.000");
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = "小数点位读取错误：" + ex.Message;
                    }
                }

            }
            else
            {
                ErrorMsg = "数据包格式错误！";
            }

            return dis;
        }
        #endregion
    }

    #region 仪表数据传出代理及事件类
    public delegate void NumReceivedEvent(object sender, NumberReceivedEventArgs e);
    /// <summary>
    /// 数据接收事件参数,包含数据
    /// </summary>
    public class NumberReceivedEventArgs : EventArgs
    {
        private double _weightPhyNum1;
        private double _weightPhyNum2;
        private int _commcount1;
        private int _commcount2;

        public double WeightPhyNum1
        {
            get
            {
                return _weightPhyNum1;
            }
        }
        public double WeightPhyNum2
        {
            get
            {
                return _weightPhyNum2;
            }
        }

        public int CommCount1
        {
            get
            {
                return _commcount1;
            }
        }
        public int CommCount2
        {
            get
            {
                return _commcount2;
            }
        }
        //public double CfcTLResult
        //{
        //    get
        //    {
        //        return _cFCTlResult;
        //    }
        //}
        //public double CfcMVResult
        //{
        //    get
        //    {
        //        return _cFCMVResult;
        //    }
        //}
        /// <summary>
        /// 重量数据接收事件参数的构造函数
        /// </summary>
        /// <param name="cardPhyNum">重量数据</param>
        public NumberReceivedEventArgs(double weightPhyNum1, double weightPhyNum2, int commCount1, int commCount2)
        {
            this._weightPhyNum1 = weightPhyNum1;
            this._weightPhyNum2 = weightPhyNum2;
            this._commcount1 = commCount1;
            this._commcount2 = commCount2;
        }

    }
    #endregion

    #endregion

}

