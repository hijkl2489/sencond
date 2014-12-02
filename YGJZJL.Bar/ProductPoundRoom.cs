using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using SDK_Com;

namespace YGJZJL.Bar
{
    /// <summary>
    /// 计量点类，用于操作计量点
    /// </summary>
    public class ProductPoundRoom
    {
        #region private member variable

        //数据表JL_POINTINFO字段映射
        private string m_POINTID;//计量点编码
        private string m_POINTNAME;//计量点名称
        private string m_POINTTYPE;//计量点称重类型

        private string m_VIEDOIP;//硬盘录像机IP
        private string m_VIEDOPORT;//硬盘录像机端口
        private string m_VIEDOUSER;//硬盘录像机用户名
        private string m_VIEDOPWD;//硬盘录像机密码

        private string m_MOXAIP;//MOXA卡IP

        private string m_METERTYPE;//仪表类型
        private string m_METERPARA;//仪表参数
        private string m_MOXAPORT;//计量仪表MOXA卡端口        

        private string m_RTUIP;//RTUIP
        private string m_RTUPORT;//RTU端口

        private string m_PRINTERIP;//打印服务器IP
        private string m_PRINTERNAME;//打印机名称
        private string m_PRINTTYPECODE;//打印机类型代码
        private int m_USEDPAPER;//已用纸张量
        private int m_TOTALPAPAR;//打印纸总数量
        private int m_USEDINK;//已用纸张量
        private int m_TOTALINK;//打印纸总数量

        private string m_STATUS;//计量点状态 IDLE-空闲 USE-正在计量
        private string m_ACCEPTTERMINAL;//接管的终端IP

        private string m_LEDPORT;//电子屏MOXA卡端口
        private string m_LEDPARA;//电子屏MOXA卡参数
        private string m_LEDTYPE;//电子屏类型

        private string m_READERPORT;//读卡器MOXA卡端口
        private string m_READERPARA;//读卡器MOXA卡参数
        private string m_READERTYPE;//读卡器类型

        private string m_DISPLAYPORT;//液晶屏MOXA卡端口
        private string m_DISPLAYPARA;//液晶屏MOXA卡参数
        private string m_DISPLAYTYPE;//液晶屏类型

        private decimal m_ZEROVALUE;//复位值

        //使用何种设备标志
        private bool m_bUseMeter;//采集仪表数据
        private bool m_bUseLED;//使用LED
        private bool m_bUseReader;//使用读卡器
        private bool m_bUseDisplay;//使用液晶屏
        private bool m_bUseRtu;//使用Rtu

        //MOXA
        private CoolSerial m_CoolSerialForMeter;//仪表
        private CoolDisplay m_CoolDisplay;//液晶屏
        //private CoolSerial m_CoolLed;//LED
        private CoolLed m_CoolLed;//LED

        //RTU
        private CoolRtu m_CoolRtu;//Rtu data collect
        private CoolRtu m_CoolRtuForCommand;//Rtu command send

        //线程
        private System.Threading.Thread m_hThread;//线程
        private bool m_bRunning;//线程运行开关

        //数据
        private string m_szMeterData;//仪表采集数据
        private decimal m_MeterValue;//仪表重量数据
        private decimal m_MeterPreData;//仪表前一次重量
        private int m_nMeterStabTimes;//仪表稳定次数

        private string m_szReaderGUID;//读卡器全球唯一号
        private string m_szReaderCardNo;//读卡器卡号
        private byte[] m_szRtuData;//Rtu data

        //派位
        private bool m_bDistributed;//是否派位

        //sign
        private bool m_bSigned;//接管

        //本地声音提示
        private SoundPlayer m_SoundPlayer;//播放声音

        //硬盘录像机
        private SDK_Com.HKDVR m_VideoRecord;//硬盘录像机类
        private int m_VideoHandle;//硬盘录像机句柄，SDK_Login获取后赋值
        private int m_Channel1;//通道1句柄
        private int m_Channel2;//通道2句柄
        private int m_Channel3;//通道3句柄
        private int m_Channel4;//通道4句柄
        private int m_Channel5;//通道5句柄
        private int m_Channel6;//通道6句柄
        private bool m_bTalk;//是否正在对讲
        private int m_TalkID;//对讲句柄
        private int m_AudioNum; //可使用音频数

        //保存标志，只允许保存一次
        private bool m_bSaved;
        //RTU状态
        private bool m_PreState;
        private bool m_CurState;

        #endregion


        #region constructor

        public ProductPoundRoom()
        {
            m_POINTID = "";//计量点编码
            m_POINTNAME = "";//计量点名称
            m_POINTTYPE = "";//计量点称重类型

            m_VIEDOIP = "";//硬盘录像机IP
            m_VIEDOPORT = "";//硬盘录像机端口
            m_VIEDOUSER = "";//硬盘录像机用户名
            m_VIEDOPWD = "";//硬盘录像机密码

            m_MOXAIP = "";//MOXA卡IP

            m_METERTYPE = "";//仪表类型
            m_METERPARA = "";//仪表参数
            m_MOXAPORT = "";//计量仪表MOXA卡端口
            m_MeterPreData = 0;//仪表前一次重量
            m_nMeterStabTimes = 0;//仪表稳定次数

            m_RTUIP = "";//RTUIP
            m_RTUPORT = "";//RTU端口

            m_PRINTERIP = "";//打印服务器IP
            m_PRINTERNAME = "";//打印机名称
            m_PRINTTYPECODE = "";//打印机类型代码
            m_USEDPAPER = 0;//已用纸张量
            m_TOTALPAPAR = 0;//打印纸总数量
            m_STATUS = "";//计量点状态
            m_ACCEPTTERMINAL = "";//接管的终端IP

            m_LEDPORT = "";//电子屏MOXA卡端口
            m_LEDPARA = "";//电子屏MOXA卡参数
            m_LEDTYPE = "";//电子屏类型

            m_READERPORT = "";//读卡器MOXA卡端口
            m_READERPARA = "";//读卡器MOXA卡参数
            m_READERTYPE = "";//读卡器类型

            m_DISPLAYPORT = "";//液晶屏MOXA卡端口
            m_DISPLAYPARA = "";//液晶屏MOXA卡参数
            m_DISPLAYTYPE = "";//液晶屏类型

            //使用何种设备标志
            m_bUseMeter = false;//采集仪表数据
            m_bUseLED = false;//使用LED
            m_bUseReader = false;//使用读卡器
            m_bUseDisplay = false;//使用液晶屏
            m_bUseRtu = false;//使用rtu

            //MOXA
            m_CoolSerialForMeter = null;//仪表
            m_CoolLed = null;//LED
            m_CoolDisplay = null;//液晶屏

            //rtu
            m_CoolRtu = null;//rtu data collect
            m_CoolRtuForCommand = null;//rtu command send

            //线程
            m_hThread = null;//线程
            m_bRunning = false;//线程运行开关

            //数据
            m_szMeterData = "";//仪表采集数据
            m_szReaderGUID = "";//读卡器全球唯一号
            m_szReaderCardNo = "";//读卡器卡号
            m_szRtuData = null;//rtu data

            //接管
            m_bSigned = false;

            //播放声音
            m_SoundPlayer = null;

            //硬盘录像机
            m_VideoRecord = null;
            m_VideoHandle = 0;
            m_Channel1 = 0;//通道1句柄
            m_Channel2 = 0;//通道2句柄
            m_Channel3 = 0;//通道3句柄
            m_Channel4 = 0;//通道4句柄
            m_Channel5 = 0;//通道5句柄
            m_Channel6 = 0;//通道6句柄
            m_bTalk = false;//是否正在对讲
            m_TalkID = 0;//对讲句柄
            m_AudioNum = 1; //可使用音频数

            m_bSaved = false;
            m_PreState = false;
            m_CurState = false;
        }

        #endregion


        #region methods

        /// <summary>
        /// 启动所有需要的设备通讯线程
        /// </summary>
        public void StartUse()
        {
            try
            {
                int i = 0;//打开20次，有时仪表一次会打不开
                if (UseMeter)
                {
                    i = 0;
                    m_CoolSerialForMeter = new CoolSerial(METERPARA);
                    m_CoolSerialForMeter.DeviceType = METERTYPE;//仪表类型，决定如何处理事务
                    m_CoolSerialForMeter.DeviceName = POINTNAME;

                    while (m_CoolSerialForMeter.StateInfo != "open" && i < 20)
                    {
                        m_CoolSerialForMeter.Open();
                        if (m_CoolSerialForMeter.StateInfo == "open")
                            m_CoolSerialForMeter.StartUse();
                        i++;
                    }

                }

                if (UseLED)
                {
                    m_CoolLed = new CoolLed(LEDPARA);
                    m_CoolLed.DeviceType = LEDTYPE;//LED类型，决定如何处理事务
                    m_CoolLed.DeviceName = POINTNAME;
                    if (m_CoolLed.Open())
                    {
                        //add code here
                    }
                }

                if (UseDisplay)
                {
                    m_CoolDisplay = new CoolDisplay(DISPLAYPARA);
                    m_CoolDisplay.DeviceType = DISPLAYTYPE;//液晶屏类型，决定如何处理事务
                    m_CoolDisplay.DeviceName = POINTNAME;
                    m_CoolDisplay.Open();
                }

                if (UseRtu)
                {
                    i = 0;
                    m_CoolRtu = new CoolRtu(RTUIP, Int32.Parse(RTUPORT));
                    m_CoolRtu.DeviceName = POINTNAME;
                    //if (m_CoolRtu.Connect2Server())                       
                    //{
                    //    m_CoolRtu.StartUse();
                    //}
                    while (m_CoolRtu.StateInfo != "open" && i < 10)
                    {
                        m_CoolRtu.Connect2Server();
                        if (m_CoolRtu.StateInfo == "open")
                            m_CoolRtu.StartUse();
                        i++;
                    }

                    i = 0;
                    m_CoolRtuForCommand = new CoolRtu(RTUIP, Int32.Parse(RTUPORT));
                    m_CoolRtuForCommand.DeviceName = POINTNAME;
                    while (m_CoolRtuForCommand.StateInfo != "open" && i < 10)
                    {
                        m_CoolRtuForCommand.Connect2Server();                        
                        i++;
                    }
                    //if (m_CoolRtuForCommand.Connect2Server())
                    //{
                    //}

                }

                m_bRunning = true;
                m_hThread = new System.Threading.Thread(new System.Threading.ThreadStart(DataCollectThread));
                m_hThread.Start();
            }
            catch (Exception e)
            {
                WriteLog("StartUse" + e.ToString());
            }
        }

        /// <summary>
        /// 数据采集线程
        /// </summary>
        private void DataCollectThread()
        {
            try
            {
                while (m_bRunning)
                {
                    if (UseMeter)
                    {
                        MeterData = m_CoolSerialForMeter.StringData;
                        MeterValue = m_CoolSerialForMeter.DecimalData;
                    }

                    if (UseRtu)
                    {
                        m_szRtuData = m_CoolRtu.GetData();
                    }

                    System.Threading.Thread.Sleep(200);//请根据需要调整线程运行周期  上次是100ms
                }
            }
            catch (System.Exception exp)
            {
                WriteLog(POINTNAME + ")错误：" + exp.Message);
            }
        }

        /// <summary>
        /// 停止所有的设备通讯线程
        /// </summary>
        public void StopUse()
        {  
            m_bRunning = false;

            if (UseMeter && m_CoolSerialForMeter != null)
            {
                m_CoolSerialForMeter.StopUse();
            }

            if (UseLED && m_CoolLed != null)
            {
                m_CoolLed.Close();
            }

            if (UseDisplay && m_CoolDisplay != null)
            {
                m_CoolDisplay.Close();
            }

            if (UseRtu)
            {
                if (m_CoolRtu != null)
                {
                    m_CoolRtu.StopUse();
                }
                if (m_CoolRtuForCommand != null)
                {
                    m_CoolRtuForCommand.Close();
                }
            }
            
            System.Threading.Thread.Sleep(200);
        }

        /// <summary>
        /// 计量业务到达时在本机播放声音提示计量员进行操作
        /// </summary>
        /// <param name="location"></param>
        public void PlaySound(string location)
        {
            if (m_SoundPlayer == null)
            {
                m_SoundPlayer = new SoundPlayer();
            }

            m_SoundPlayer.SoundLocation = location;
            m_SoundPlayer.Load();
            m_SoundPlayer.Play();
        }


        /// <summary>
        /// 向Rtu发送命令
        /// </summary>
        /// <param name="device">设备地址，通常指Rtu的某一模块</param>
        /// <param name="Command">命令,1-读寄存器数据，5-设置线圈（开关）状态</param>
        /// <param name="AddrH">地址偏移高8位</param>
        /// <param name="AddrL">地址偏移低8位</param>
        /// <param name="DevH">需要的数据高8位</param>
        /// <param name="DevL">需要的个数低8位</param>
        /// <returns></returns>
        public bool SendRtuCommand(byte device, byte Command, byte AddrH, byte AddrL, byte DevH, byte DevL)
        {
            byte[] retByte = m_CoolRtuForCommand.RtuCommand(device, Command, AddrH, AddrL, DevH, DevL);
            if (retByte == null || retByte.Length == 0)
            {
                return false;
            }

            return retByte[0] == Command ? true : false;
        }

        private void WriteLog(string str)
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

                System.IO.TextWriter tw = new System.IO.StreamWriter(m_szRunPath + "\\log\\" + POINTID + "-" + strDate + "_pound.log", true);

                tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                tw.WriteLine(str);
                tw.WriteLine("\r\n");

                tw.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 计量点ID
        /// </summary>
        public string POINTID
        {
            get
            {
                return m_POINTID;
            }
            set
            {
                m_POINTID = value;
            }
        }


        /// <summary>
        /// 计量点名称
        /// </summary>
        public string POINTNAME
        {
            get
            {
                return m_POINTNAME;
            }
            set
            {
                m_POINTNAME = value;
            }
        }

        /// <summary>
        /// 计量点类型
        /// </summary>
        public string POINTTYPE
        {
            get
            {
                return m_POINTTYPE;
            }
            set
            {
                m_POINTTYPE = value;
            }
        }

        /// <summary>
        /// 硬盘录像机IP
        /// </summary>
        public string VIEDOIP
        {
            get
            {
                return m_VIEDOIP;
            }
            set
            {
                m_VIEDOIP = value;
            }
        }

        /// <summary>
        /// 硬盘录像机端口
        /// </summary>
        public string VIEDOPORT
        {
            get
            {
                return m_VIEDOPORT;
            }
            set
            {
                m_VIEDOPORT = value;
            }
        }

        /// <summary>
        /// 硬盘录像机用户名
        /// </summary>
        public string VIEDOUSER
        {
            get
            {
                return m_VIEDOUSER;
            }
            set
            {
                m_VIEDOUSER = value;
            }
        }

        /// <summary>
        /// 硬盘录像机访问密码
        /// </summary>
        public string VIEDOPWD
        {
            get
            {
                return m_VIEDOPWD;
            }
            set
            {
                m_VIEDOPWD = value;
            }
        }

        /// <summary>
        /// 计量仪表类型
        /// </summary>
        public string METERTYPE
        {
            get
            {
                return m_METERTYPE;
            }
            set
            {
                m_METERTYPE = value;
            }
        }

        /// <summary>
        /// 计量仪表串口通讯参数，必须是以下格式：串口名,波特率,校验位,数据位,停止位，如：COM9,9600,N,8,1或COM2,115200,E,7,1.5
        /// </summary>
        public string METERPARA
        {
            get
            {
                return m_METERPARA;
            }
            set
            {
                m_METERPARA = value;
            }
        }

        /// <summary>
        /// 计量仪表前一次数据读数值
        /// </summary>
         public decimal MeterPreData
        {
            get
            {
                return m_MeterPreData;
            }
            set
            {
                m_MeterPreData = value;
            }
        }

        /// <summary>
        /// 计量仪表稳定次数
        /// </summary>
        public int MeterStabTimes
        {
            get
            {
                return m_nMeterStabTimes;
            }
            set
            {
                m_nMeterStabTimes = value;
            }
        }       

        /// <summary>
        /// MOXA网关IP地址，管理用，对程序无影响
        /// </summary>
        public string MOXAIP
        {
            get
            {
                return m_MOXAIP;
            }
            set
            {
                m_MOXAIP = value;
            }
        }

        /// <summary>
        /// 计量仪表使用MOXA网关端口
        /// </summary>
        public string MOXAPORT
        {
            get
            {
                return m_MOXAPORT;
            }
            set
            {
                m_MOXAPORT = value;
            }
        }

        /// <summary>
        /// Rtu IP地址
        /// </summary>
        public string RTUIP
        {
            get
            {
                return m_RTUIP;
            }
            set
            {
                m_RTUIP = value;
            }
        }

        /// <summary>
        /// Rtu通讯端口
        /// </summary>
        public string RTUPORT
        {
            get
            {
                return m_RTUPORT;
            }
            set
            {
                m_RTUPORT = value;
            }
        }

        /// <summary>
        /// 票据打印机IP地址，管理用，对程序无影响
        /// </summary>
        public string PRINTERIP
        {
            get
            {
                return m_PRINTERIP;
            }
            set
            {
                m_PRINTERIP = value;
            }
        }

        /// <summary>
        /// 票据打印机名称，打印时根据名称调用对应的打印机
        /// </summary>
        public string PRINTERNAME
        {
            get
            {
                return m_PRINTERNAME;
            }
            set
            {
                m_PRINTERNAME = value;
            }
        }

        /// <summary>
        /// 票据打印机类型代码，打印机换纸时获取总的纸张数
        /// </summary>
        public string PRINTTYPECODE
        {
            get
            {
                return m_PRINTTYPECODE;
            }
            set
            {
                m_PRINTTYPECODE = value;
            }
        }

        /// <summary>
        /// 票据打印机剩余纸张数
        /// </summary>
        public int USEDPAPER
        {
            get
            {
                return m_USEDPAPER;
            }
            set
            {
                m_USEDPAPER = value;
            }
        }

        /// <summary>
        /// 票据打印机剩余碳带数
        /// </summary>
        public int USEDINK
        {
            get
            {
                return m_USEDINK;
            }
            set
            {
                m_USEDINK = value;
            }
        }

        /// <summary>
        /// 票据打印机总纸数
        /// </summary>
        public int TOTALPAPAR
        {
            get
            {
                return m_TOTALPAPAR;
            }
            set
            {
                m_TOTALPAPAR = value;
            }
        }

        /// <summary>
        /// 票据打印机总碳带数
        /// </summary>
        public int TOTALINK
        {
            get
            {
                return m_TOTALINK;
            }
            set
            {
                m_TOTALINK = value;
            }
        }

        /// <summary>
        /// 计量点状态，USE-正在计量，IDLE-空闲
        /// </summary>
        public string STATUS
        {
            get
            {
                return m_STATUS;
            }
            set
            {
                m_STATUS = value;
            }
        }

        /// <summary>
        /// 暂未使用此属性
        /// </summary>
        public string ACCEPTTERMINAL
        {
            get
            {
                return m_ACCEPTTERMINAL;
            }
            set
            {
                m_ACCEPTTERMINAL = value;
            }
        }

        /// <summary>
        /// LED使用MOXA网关端口，管理用，对程序无影响
        /// </summary>
        public string LEDPORT
        {
            get
            {
                return m_LEDPORT;
            }
            set
            {
                m_LEDPORT = value;
            }
        }

        /// <summary>
        /// LED通讯参数，用于入库计量点
        /// </summary>
        public string LEDPARA
        {
            get
            {
                return m_LEDPARA;
            }
            set
            {
                m_LEDPARA = value;
            }
        }
        
        /// <summary>
        /// LED类型
        /// </summary>
        public string LEDTYPE
        {
            get
            {
                return m_LEDTYPE;
            }
            set
            {
                m_LEDTYPE = value;
            }
        }

        /// <summary>
        /// 读卡器MOXA卡端口，管理用，对程序无影响
        /// </summary>
        public string READERPORT
        {
            get
            {
                return m_READERPORT;
            }
            set
            {
                m_READERPORT = value;
            }
        }

        /// <summary>
        /// 读卡器串口通讯参数，必须是以下格式：串口号,波特率。如：9,9600或2,115200
        /// </summary>
        public string READERPARA
        {
            get
            {
                return m_READERPARA;
            }
            set
            {
                m_READERPARA = value;
            }
        }

        /// <summary>
        /// 读卡器类型，对于明华RF-35，数据库中应配制成RF35
        /// </summary>
        public string READERTYPE
        {
            get
            {
                return m_READERTYPE;
            }
            set
            {
                m_READERTYPE = value;
            }
        }

        /// <summary>
        /// 液晶屏MOXA端口，管理用，对程序无影响
        /// </summary>
        public string DISPLAYPORT
        {
            get
            {
                return m_DISPLAYPORT;
            }
            set
            {
                m_DISPLAYPORT = value;
            }
        }

        /// <summary>
        /// 液晶屏串口通讯参数，必须是以下格式：串口名,波特率,校验位,数据位,停止位，如：COM9,9600,N,8,1或COM2,115200,E,7,1.5
        /// </summary>
        public string DISPLAYPARA
        {
            get
            {
                return m_DISPLAYPARA;
            }
            set
            {
                m_DISPLAYPARA = value;
            }
        }

        /// <summary>
        /// 液晶屏类型，昆钢项目应为EBN15
        /// </summary>
        public string DISPLAYTYPE
        {
            get
            {
                return m_DISPLAYTYPE;
            }
            set
            {
                m_DISPLAYTYPE = value;
            }
        }

        /// <summary>
        /// 零点值，作为上称判断
        /// </summary>
        public decimal ZEROVALUE
        {
            get
            {
                return m_ZEROVALUE;
            }
            set
            {
                m_ZEROVALUE = value;
            }
        }

        /// <summary>
        /// 是否采集计量仪表
        /// </summary>
        public bool UseMeter
        {
            get
            {
                return m_bUseMeter;
            }
            set
            {
                m_bUseMeter = value;
            }
        }

        /// <summary>
        /// 是否有读卡器
        /// </summary>
        public bool UseReader
        {
            get
            {
                return m_bUseReader;
            }
            set
            {
                m_bUseReader = value;
            }
        }

        /// <summary>
        /// 是否有液晶屏
        /// </summary>
        public bool UseDisplay
        {
            get
            {
                return m_bUseDisplay;
            }
            set
            {
                m_bUseDisplay = value;
            }
        }

        /// <summary>
        /// 是否有LED
        /// </summary>
        public bool UseLED
        {
            get
            {
                return m_bUseLED;
            }
            set
            {
                m_bUseLED = value;
            }
        }

        /// <summary>
        /// 是否有Rtu
        /// </summary>
        public bool UseRtu
        {
            get
            {
                return m_bUseRtu;
            }
            set
            {
                m_bUseRtu = value;
            }
        }

        /// <summary>
        /// 计量仪表原始通讯报文
        /// </summary>
        public string MeterData
        {
            get
            {
                return m_szMeterData;
            }
            set
            {
                m_szMeterData = value;
            }
        }

        /// <summary>
        /// 计量仪表重量
        /// </summary>
        public decimal MeterValue
        {
            get
            {
                return m_MeterValue;
            }
            set
            {
                m_MeterValue = value;
            }
        }


        /// <summary>
        /// 读卡器GUID
        /// </summary>
        public string ReaderGUID
        {
            get
            {
                return m_szReaderGUID;
            }
            set
            {
                m_szReaderGUID = value;
            }
        }

        /// <summary>
        /// 车证卡助记号
        /// </summary>
        public string CardNo
        {
            get
            {
                return m_szReaderCardNo;
            }
            set
            {
                m_szReaderCardNo = value;
            }
        }

        /// <summary>
        /// Rtu通讯原始报文
        /// </summary>
        public byte[] RtuData
        {
            get
            {
                return m_szRtuData;
            }
            set
            {
                m_szRtuData = value;
            }
        }

        /// <summary>
        /// 暂未使用
        /// </summary>
        public bool Distributed
        {
            get
            {
                return m_bDistributed;
            }
            set
            {
                m_bDistributed = value;
            }
        }

        /// <summary>
        /// 是否分配
        /// </summary>
        public bool Signed
        {
            get
            {
                return m_bSigned;
            }
            set
            {
                m_bSigned = value;
            }
        }

        /// <summary>
        /// 读卡器句柄，父级对象用来发送命令
        /// </summary>
        public CoolDisplay Display
        {
            get
            {
                return m_CoolDisplay;
            } 
        }

        /// <summary>
        /// LED句柄，父级对象用来发送命令
        /// </summary>
        public CoolLed SendLED
        {
            get
            {
                return m_CoolLed;
            }
        }

        /// <summary>
        /// 硬盘录像机
        /// </summary>
        public SDK_Com.HKDVR VideoRecord
        {
            get
            {
                return m_VideoRecord;
            }
            set
            {
                m_VideoRecord = value; 
            }
        }

        /// <summary>
        /// 硬盘录像机句柄，SDK_Login获取后赋值
        /// </summary>
        public int VideoHandle
        {
            get
            {
                return m_VideoHandle;
            }
            set
            {
                m_VideoHandle = value;
            }
        }

        /// <summary>
        /// 通道1句柄
        /// </summary>
        public int Channel1
        {
            get
            {
                return m_Channel1;
            }
            set
            {
                m_Channel1 = value;
            }
        }

        /// <summary>
        /// 通道2句柄
        /// </summary>
        public int Channel2
        {
            get
            {
                return m_Channel2;
            }
            set
            {
                m_Channel2 = value;
            }
        }

        /// <summary>
        /// 通道3句柄
        /// </summary>
        public int Channel3
        {
            get
            {
                return m_Channel3;
            }
            set
            {
                m_Channel3 = value;
            }
        }

        /// <summary>
        /// 通道4句柄
        /// </summary>
        public int Channel4
        {
            get
            {
                return m_Channel4;
            }
            set
            {
                m_Channel4 = value;
            }
        }

        /// <summary>
        /// 通道5句柄
        /// </summary>
        public int Channel5
        {
            get
            {
                return m_Channel5;
            }
            set
            {
                m_Channel5 = value;
            }
        }

        /// <summary>
        /// 通道6句柄
        /// </summary>
        public int Channel6
        {
            get
            {
                return m_Channel6;
            }
            set
            {
                m_Channel6 = value;
            }
        }

        /// <summary>
        /// 是否正在对讲
        /// </summary>
        public bool Talk
        {
            get
            {
                return m_bTalk;
            }
            set
            {
                m_bTalk = value;
            }
        }

        /// <summary>
        /// 对讲句柄
        /// </summary>
        public int TalkID
        {
            get
            {
                return m_TalkID;
            }
            set
            {
                m_TalkID = value;
            }
        }

        /// <summary>
        /// 可使用的音频数量，
        /// </summary>
        public int AUDIONUM
        {
            get
            {
                return m_AudioNum;
            }
            set
            {
                m_AudioNum = value;
            }
        }

        /// <summary>
        /// 保存标志
        /// </summary>
        public bool Saved
        {
            get
            {
                return m_bSaved;
            }
            set
            {
                m_bSaved = value;
            }
        }

        /// <summary>
        /// RTU大厅信号灯前次状态
        /// </summary>
        public bool PreState
        {
            get
            {
                return m_PreState;
            }
            set
            {
                m_PreState = value;
            }
        }

        /// <summary>
        /// RTU大厅信号灯前状态
        /// </summary>
        public bool CurState
        {
            get
            {
                return m_CurState;
            }
            set
            {
                m_CurState = value;
            }
        }

        #endregion
    }
}
