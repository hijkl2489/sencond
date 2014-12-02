using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using YGJZJL.PublicComponent;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;
using Infragistics.Win.UltraWinGrid;
using System.Threading;
using System.Media;
using System.Net;
using System.Collections;
using Infragistics.Win;
using System.IO.Ports;
using YGJZJL.CarSip.Client.Meas;


namespace YGJZJL.Car
{
    public partial class WeighMeasureInfo : FrmBase
    {

        //2012.3.30:16:00 加IC卡
        IcCard card;
        #region 参数定义
        //校秤：
        public delegate void CorrentionPicture();//校秤抓图委托
        private CorrentionPicture m_MainThreadCorrentionPicture;//建立校秤委托变量
        string correntionWeightNo = "";//保存校秤操作编号
        BaseInfo baseinfo = new BaseInfo();
        string correntionWeight = "";//校秤，仪表显示重量

        private Thread SaveBaseInfoThread;//保存基础信息线程（基础信息表+1保存）
        private Thread SoundPlayThread;   //声音播放线程

        private string m_ImageWeight = "";

        private bool s_run = true;
        //private string strError = "";
        //string sJLDID = "";//服务端返回值，计量点ID
        string sWLID = "";//服务端返回值，物料ID
        string sFHDWID = "";//服务端返回值，发货单位ID
        string sSHDWID = "";//服务端返回值，收货单位ID
        string sCYDWID = "";//服务端返回值，承运单位ID

        string WLMC = ""; //线程定义物料名称
        string FHDWMC = ""; //线程定义发货单位名称
        string SHDWMC = ""; //线程定义收货单位名称
        string CYDWMC = ""; //线程定义承运单位名称
        string strPoint = "";//计量点初始化为空

        private Thread ColorThread;   //计量点颜色变化提示线程
        int indexColor;

        //打开图片定义参数
        private static int intPictureBoxWidth;
        private static int intPictureBoxHeight;
        //private byte[] m_imagebytes = null;
        private byte[] imagebytes1 = null;
        private byte[] imagebytes2 = null;
        private byte[] imagebytes3 = null;
        private byte[] imagebytes4 = null;
        private byte[] imagebytes5 = null;
        private byte[] imagebytes6 = null;
        //private string m_strFileName = "";

        private string stRunPath;

        private string strJLDID = "";              //当前选择的计量点ID
        private string strNumber = "";         //保存本地图片编号
        private string fileName1;                           //图片1保存名称
        private string fileName2;                           //图片2保存名称
        private string fileName3;                           //图片3保存名称
        private string fileName4;                           //图片4保存名称
        private string fileName5;                           //图片5保存名称
        private string fileName6;                           //图片6保存名称
        private string fileName7;                           //动态曲线图片7保存名称
        private string fileName8;
        private string fileName9;
        private string fileName10;

        string strYB = "";    //预报表是否为空
        string strYCJL = "";  //一次计量表是否为空
        string strZYBH = "";  //作业编号
        //期限皮重表
        string strQXPZ = "";  //期限皮重
        string qxJLD;
        string qxJLY;
        string qxJLSJ;
        string qxBC;
        string qxCZBH;        //操作编号
        string ImageJZ;       //添加到图片上净重

        //二次计量表
        string e_WEIGHTNO = ""; //作业编号
        string e_CONTRACTNO = ""; //合同号
        string e_CONTRACTITEM = ""; //合同项目编号
        string e_STOVENO = ""; //炉号
        string e_COUNT = ""; //支数
        string e_CARDNUMBER = ""; //车证号
        string e_CARNO = ""; //车号
        string e_MATERIAL = ""; //物资代码
        string e_MATERIALNAME = ""; //物料名称
        string e_SENDER = ""; //发货方代码
        string e_TRANSNO = ""; //承运方代码
        string e_RECEIVER = ""; //收货方代码


        string e_SENDGROSSWEIGHT = ""; //预报总重
        string e_SENDTAREWEIGHT = ""; //预报皮重
        string e_SENDNETWEIGHT = ""; //预报净量
        string e_WEIGHTTYPE = ""; //流向
        string e_GROSSWEIGHT = ""; //毛重重量
        string e_GROSSPOINT = ""; //毛重计量点
        string e_GROSSPERSON = ""; //毛重计量员
        string e_GROSSDATETIME = ""; //毛重计量时间
        string e_GROSSSHIFT = ""; //毛重计量班次
        string e_TAREWEIGHT = ""; //皮重重量
        string e_TAREPOINT = ""; //皮重计量点
        string e_TAREPERSON = ""; //皮重计量员
        string e_TAREDATETIME = ""; //皮重计量时间
        string e_TARESHIFT = ""; //皮重计量班次
        string e_FIRSTLABELID = ""; //一次磅单条码
        string e_FULLLABELID = ""; //完整磅单条码
        string e_NETWEIGHT = ""; //净重
        string e_SAMPLEPERSON = ""; //取样员
        string e_YKL = ""; //应扣量
        string e_SAMPLETIME = ""; //取样时间
        string e_SAMPLEPLACE = ""; //取样点
        string e_SAMPLEFLAG = ""; //取样确认
        string e_DRIVERNAME = ""; //驾驶员姓名
        string e_DRIVERIDCARD = ""; //驾驶员身份证
        string e_SENDERSTORE = ""; //发货地点
        string e_RECEIVERSTORE = ""; //卸货地点
        string e_IFSAMPLING = ""; //是否需要取样确认
        string e_IFACCEPT = ""; //是否需要验收确认
        string e_IFUNLOAD = ""; //是否需要卸货确认
        string e_REWEIGHTFLAG = ""; //复磅标记
        string e_REWEIGHTTIME = ""; //复磅确认时间
        string e_REWEIGHTPLACE = ""; //复磅确认地点
        string e_REWEIGHTPERSON = ""; //复磅确认员
        string e_UNLOADFLAG = ""; //卸车确认(0:退货过磅,1:收货确认,2:退货不过磅)

        string strECJL = "";

        //一次计量表
        string stHTH;
        string stHTXMH;
        string stLH;
        string stZS; //支数
        string stCZH;
        string stCH;
        string stWLID;
        string stWLMC;
        string stFHFDM;
        string stCYDW;
        string stSHFDM;
        string stSHKCD;

        //add by luobin 
        string strGPMaterial = "";
        string strAdviseSpec = ""; //建议轧制规格
        string strZZJY = "";//轧制建议
        string stSendStore = "";    //发货地点
        string stReceiverSotre = ""; //收货地点
        string stYBZZ;
        string stYBPZ;
        string stYBJZ;
        string stLX;
        string strYCZL = "";    //一次计量重量
        string strYCJLD;        //一次计量点
        string strYCJLY;        //一次计量员
        string strYCJLSJ;        //一次计量时间
        string strYCJLBC;        //一次计量班次
        string strYCBDTM;        //一次计量磅单条码
        string strXCRKSJ;      //卸车入库时间
        string strXCCKSJ;      //卸车出库时间
        string strXCQR;        //卸车确认
        string strXCKGY;       //卸车库管员
        string strZCRKSJ;      //装车入库时间
        string strZCCKSJ;      //装车出库时间      
        string strZCQR;        //装车确认
        string strZCKGY;       //装车库管员
        string strQYY;         //取样人
        string strBFBH;        //磅房编号
        string strYCSFYC;      //一次计量是否异常



        string s_SAMPLETIME;//取样时间
        string s_SAMPLEPLACE;//取样地点
        string s_SAMPLEFLAG; //取样确认
        string s_UNLOADPERSON; //卸车员
        string s_UNLOADTIME;//卸车时间
        string s_UNLOADPLACE;//卸车点
        string s_CHECKPERSON;//验收员
        string s_CHECKTIME;//验收时间
        string s_CHECKPLACE;//验收点
        string s_CHECKFLAG;//验收确认
        string s_IFSAMPLING;//是否取样需要确认
        string s_IFACCEPT;//是否需要验收确认 
        string s_DRIVERNAME;//驾驶员姓名
        string s_DRIVERIDCARD;//驾驶员身份证
        string s_SENDERSTORE;//发货地点
        string s_REWEIGHTFLAG;//复磅标志
        string s_REWEIGHTTIME;//复磅确认时间
        string s_REWEIGHTPLACE;//复磅确认地点
        string s_REWEIGHTPERSON;//复磅确认员
        string s_BILLNUMBER;//单据编号
        //string s_ECJLSJ;//二次计量时间
        string s_DFJZ;//钢坯对方净重
        string s_CZ;//钢坯计量净重与对方净重差值

        string s_YKBL;//应扣比例

        string stYBH; //预报号

        /// <summary>
        /// 预报表信息
        /// </summary>
        struct YBInfo
        {
            public string strYBH;     //预报号
            public string strCZH;     //车证号
            public string strCH;      //车号
            public string strHTH;     //合同号
            public string strHTXMH;   //合同项目号
            public string strLH;      //炉号
            public string strZS;      //支数
            public string strWLID;    //物资代码
            public string strWLMC;    //物料名称
            public string strFHFDM;   //发货方代码
            public string strFHKCD;   //发货库存点代码
            public string strSHFDM;   //收货方代码
            public string strSHKCD;   //收货库存点代码
            public string strLX;      //流向
            public string strCYDW;    //承运方代码
            public string strHQLX;    //衡器类型
            public string strYBZZ;    //预报总重
            public string strYBPZ;    //预报皮重
            public string strYBJZ;    //预报净重
            public string strBF;      //磅房编号
            public string strQXPZBZ;  //是否需要卸货确认
        }

        string y_IFSAMPLING;//是否需要取样确认
        string y_IFACCEPT;//是否需要验收确认 
        string y_DRIVERNAME;//驾驶员姓名
        string y_DRIVERIDCARD;//驾驶员身份证

        string sFHDW;
        string sSHDW;
        string sCYDW;
        string sWLMC;
        string sLX;

        private YBInfo[] ybCount = new YBInfo[19]; //预报信息
        private int selectRow = 0;           //选择的行数
        private string lspzCH;          //车号对应历史皮重

        string strDS = "";   //读数完成后，txtDS.Text的值就不再变

        private string UserSign = "1"; //用户锁定选择状态(打开界面默认不能选择计量点)
        string strUID = "";
        string strUMM = "";

        /// <summary>
        /// 定义矩形框
        /// </summary>
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //钢坯参数定义
        private string sDDH = "";      //订单号（合同号）
        private string sCountZS = "";  //总支数
        private string jlWLID = "";     //计量物料ID
        private string jlWLMC = "";    //计量物料名称
        string GZ = "";               //钢种
        string GG = "";               //规格
        string CD = "";               //长度
        string GPJZ = "";             //钢坯净重

        private DataTable dtQX = new DataTable();  //曲线图表定义
        int j = 0; //曲线图表列定义
        int ksht = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制

        private DataTable dtQX1 = new DataTable();  //曲线图表定义
        private DataTable dtQX2 = new DataTable();  //曲线图表定义
        private DataTable dtQX3 = new DataTable();  //曲线图表定义
        private DataTable dtQX4 = new DataTable();  //曲线图表定义
        private DataTable dtQX5 = new DataTable();  //曲线图表定义
        private DataTable dtQX6 = new DataTable();  //曲线图表定义
        private DataTable dtQX7 = new DataTable();  //曲线图表定义
        private DataTable dtQX8 = new DataTable();  //曲线图表定义
        private DataTable dtQX9 = new DataTable();  //曲线图表定义
        private DataTable dtQX10 = new DataTable();  //曲线图表定义
        private DataTable dtQX11 = new DataTable();  //曲线图表定义
        private DataTable dtQX12 = new DataTable();  //曲线图表定义
        private DataTable dtQX13 = new DataTable();  //曲线图表定义
        private DataTable dtQX14 = new DataTable();  //曲线图表定义

        int j1 = 0; //曲线图表列定义
        int j2 = 0; //曲线图表列定义
        int j3 = 0; //曲线图表列定义
        int j4 = 0; //曲线图表列定义
        int j5 = 0; //曲线图表列定义
        int j6 = 0; //曲线图表列定义
        int j7 = 0; //曲线图表列定义
        int j8 = 0; //曲线图表列定义
        int j9 = 0; //曲线图表列定义
        int j10 = 0; //曲线图表列定义
        int j11 = 0; //曲线图表列定义
        int j12 = 0; //曲线图表列定义
        int j13 = 0; //曲线图表列定义
        int j14 = 0; //曲线图表列定义

        int ksht1 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht2 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht3 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht4 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht5 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht6 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht7 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht8 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht9 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht10 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht11 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht12 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht13 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制
        int ksht14 = 0; //曲线是否开始绘制，重量保存后，车下称前是不用绘制，下车开始上称时再开始绘制


        #endregion

        #region 仪表采集参数定义

        public string iPort = "COM2"; //1,2,3,4
        public int iRate = 4800; //1200,2400,4800,9600
        public byte bSize = 8; //8 bits
        public string bParity = "N"; // 0-4=no,odd,even,mark,space 
        public byte bStopBits = 1; // 0,1,2 = 1, 1.5, 2 
        public int iTimeout = 10;
        private string YBLX = "";

        public byte[] recb;
        public SerialCommlib mycom1 = new SerialCommlib();
        string cjzl = "";
        //private string StartMonitoring = "0"; //线程是否启动
        string IPDZ = "";
        string jzzl1 = "";
        string jzzl2 = "";
        //string jzzl3 = "";


        //public bool commThreadAlive = false;

        //磅房对应线程定义
        //private Thread commThreadName;              //串口监控线程
        private Thread commThread1;                 //串口监控线程 K01 西北门100T重车磅1
        private Thread commThread2;                 //串口监控线程 K02 西北门100t空车磅
        private Thread commThread3;                 //串口监控线程 K03 四烧150t汽车衡
        private Thread commThread4;                 //串口监控线程 K04 焦化50t汽车衡
        private Thread commThread5;                 //串口监控线程 K05 二钢80t汽车磅
        private Thread commThread6;                 //串口监控线程 K06 二钢100t汽车衡
        private Thread commThread7;                 //串口监控线程 K07 大营门100t汽车衡
        private Thread commThread8;                 //串口监控线程 K08 三钢100吨汽车衡
        private Thread commThread9;                 //串口监控线程 K09 三钢80吨汽车衡
        private Thread commThread10;                //串口监控线程 K10 安海50吨汽车衡

        //bool commThreadAlive1 = false;
        //bool commThreadAlive2 = false;
        //bool commThreadAlive3 = false;
        //bool commThreadAlive4 = false;
        //bool commThreadAlive5 = false;
        //bool commThreadAlive6 = false;
        //bool commThreadAlive7 = false;
        //bool commThreadAlive8 = false;
        //bool commThreadAlive9 = false;
        //bool commThreadAlive10 = false;

        //磅房对应仪表重量定义
        private string bfzl1 = "";                  //K01
        private string bfzl2 = "";                  //K02
        private string bfzl3 = "";                  //K03
        private string bfzl4 = "";                  //K04
        private string bfzl5 = "";                  //K05
        private string bfzl6 = "";                  //K06
        private string bfzl7 = "";                  //K07
        private string bfzl8 = "";                  //K08
        private string bfzl9 = "";                  //K09
        private string bfzl10 = "";                 //K010

        public int hComm1;                       //线程1连接句柄
        public int hComm2;                       //线程2连接句柄
        public int hComm3;                       //线程3连接句柄
        public int hComm4;                       //线程4连接句柄
        public int hComm5;                       //线程5连接句柄
        public int hComm6;                       //线程6连接句柄
        public int hComm7;                       //线程7连接句柄
        public int hComm8;                       //线程8连接句柄
        public int hComm9;                       //线程9连接句柄
        public int hComm10;                      //线程10连接句柄

        int index1;
        int index2;
        int index3;
        int index4;
        int index5;
        int index6;
        int index7;
        int index8;
        int index9;
        int index10;

        string sYBZL1 = "";      //仪表1采集重量
        string sYBZL2 = "";      //仪表2采集重量
        string sYBZL3 = "";      //仪表3采集重量
        string sYBZL4 = "";      //仪表4采集重量
        string sYBZL5 = "";      //仪表5采集重量
        string sYBZL6 = "";      //仪表6采集重量
        string sYBZL7 = "";      //仪表7采集重量
        string sYBZL8 = "";      //仪表8采集重量
        string sYBZL9 = "";      //仪表9采集重量
        string sYBZL10 = "";     //仪表10采集重量

        int y1 = 0;             //定义采集重量之差次数
        int y2 = 0;
        int y3 = 0;
        int y4 = 0;
        int y5 = 0;
        int y6 = 0;
        int y7 = 0;
        int y8 = 0;
        int y9 = 0;
        int y10 = 0;


        #endregion

        #region 仪表采集定义新

        //private System.IO.Ports.SerialPort m_SerialPort = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort1 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort2 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort3 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort4 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort5 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort6 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort7 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort8 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort9 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort10 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort11 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort12 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort13 = null;//串口
        private System.IO.Ports.SerialPort m_SerialPort14 = null;//串口

        SerialPort[] m_SerialPort;

        string strMeter = "3190";      //仪表类型
        string strPort = "COM8";    //串口号(端口号)
        string strCom = "4800,n,8,1";    //仪表参数

        int y = 0;
        float ybzl1 = 0;
        UltraGridCell cell;

        bool Comstate = false;
        bool Comstate1 = false;
        bool Comstate2 = false;
        bool Comstate3 = false;
        bool Comstate4 = false;
        bool Comstate5 = false;
        bool Comstate6 = false;
        bool Comstate7 = false;
        bool Comstate8 = false;
        bool Comstate9 = false;
        bool Comstate10 = false;

        private System.Threading.Thread myThread1;
        private System.Threading.Thread myThread2;
        private System.Threading.Thread myThread3;
        private System.Threading.Thread myThread4;
        private System.Threading.Thread myThread5;
        private System.Threading.Thread myThread6;
        private System.Threading.Thread myThread7;
        private System.Threading.Thread myThread8;
        private System.Threading.Thread myThread9;
        private System.Threading.Thread myThread10;

        float strYBZL1;    //仪表重量1
        float strYBZL2;    //仪表重量2
        float strYBZL3;    //仪表重量3
        float strYBZL4;    //仪表重量4
        float strYBZL5;    //仪表重量5
        float strYBZL6;    //仪表重量6
        float strYBZL7;    //仪表重量7
        float strYBZL8;    //仪表重量8
        float strYBZL9;    //仪表重量9
        float strYBZL10;   //仪表重量10

        int zt1 = 0;   //是否读到仪表重量数据
        int zt2 = 0;   //仪表是否读到数
        int zt3 = 0;   //仪表是否读到数
        int zt4 = 0;   //仪表是否读到数
        int zt5 = 0;   //仪表是否读到数
        int zt6 = 0;   //仪表是否读到数
        int zt7 = 0;   //仪表是否读到数
        int zt8 = 0;   //仪表是否读到数
        int zt9 = 0;   //仪表是否读到数
        int zt10 = 0;   //仪表是否读到数

        int wdcs1 = 0; //稳定次数
        int wdcs2 = 0; //稳定次数
        int wdcs3 = 0; //稳定次数
        int wdcs4 = 0; //稳定次数
        int wdcs5 = 0; //稳定次数
        int wdcs6 = 0; //稳定次数
        int wdcs7 = 0; //稳定次数
        int wdcs8 = 0; //稳定次数
        int wdcs9 = 0; //稳定次数
        int wdcs10 = 0; //稳定次数

        //private bool myThreadAlive1 = false;
        //private bool myThreadAlive2 = false;

        string yb = "";
        int index = 0;

        #endregion

        #region Rtu参数定义

        public System.Threading.Thread m_DataCollectThread;
        public byte portnum = 0;
        public string ip = "10.6.18.212";
        public int port = 1100;
        public bool state1 = false;
        public bool state2 = false;
        public bool state3 = false;
        public bool state4 = false;
        public bool state5 = false;
        public bool state6 = false;
        public bool state7 = false;
        public bool m_bRunning = false;

        #endregion

        #region 硬盘录像机参数定义
        //private SDK_Com.HKDVR sdk = new SDK_Com.HKDVR();
        //private int m_lLoginID = -1;

        //private int lRealHandle = -1;
        //private int lRealHandle1 = -1;

        //private int m_lTalkHandle = -1;

        //private bool m_bSendingData = false;

        int loghandle;
        int relhandle;
        int talhandle;
        int soundhandle;
        int talkID;
        int istalk;
        string yplxjIP = "";
        int lxjDK;
        string lxjUser = "";
        string lxjMM = "";

        SDK_Com.HKDVR sdk;
        //SDK318Class sdk;
        //SDK318 sdk = new SDK318();//硬盘录像机实例化
        string sdkXCSY = "";   //现场声音
        string sdkYYDJ = "";   //语音对讲

        //string sflj = "";      //是否已连接硬盘录像机

        int relhandle2;
        int relhandle3;
        int relhandle4;
        int relhandle5;
        int relhandle6;

        #endregion

        #region new

        private DataTable dtQXT0 = new DataTable();  //曲线图表定义
        private DataTable dtQXT1 = new DataTable();  //曲线图表定义
        private DataTable dtQXT2 = new DataTable();  //曲线图表定义
        private DataTable dtQXT3 = new DataTable();  //曲线图表定义
        private DataTable dtQXT4 = new DataTable();  //曲线图表定义
        private DataTable dtQXT5 = new DataTable();  //曲线图表定义
        private DataTable dtQXT6 = new DataTable();  //曲线图表定义
        private DataTable dtQXT7 = new DataTable();  //曲线图表定义
        private DataTable dtQXT8 = new DataTable();  //曲线图表定义
        private DataTable dtQXT9 = new DataTable();  //曲线图表定义

        private DataTable[] dtQXT; //汽车衡曲线图数组
        //private int m_PoundCount;  //计量点个数，用于构建计量点曲线图列表,用下面add by tom定义的private int m_nPointCount;//计量点个数来构建,实际用的是实数10。

        private int curveColumns0 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns1 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns2 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns3 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns4 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns5 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns6 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns7 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns8 = 0; //计算曲线图列，总共到第几列了
        private int curveColumns9 = 0; //计算曲线图列，总共到第几列了

        private int[] curveColumns;  //计算曲线图列，总共到第几列了

        int strBackZero0 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero1 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero2 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero3 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero4 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero5 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero6 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero7 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero8 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图
        int strBackZero9 = 0;  //重量是否已返回到0，要求重量返回到0后，再开始重新绘图

        int[] strBackZero; //重量是否已返回到0数组，要求重量返回到0后，再开始重新绘图

        int BackZeroSign0 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign1 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign2 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign3 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign4 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign5 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign6 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign7 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign8 = 0; //判断是否要修改strBackZero与 BackZero值
        int BackZeroSign9 = 0; //判断是否要修改strBackZero与 BackZero值
        int[] BackZeroSign; //判断是否要修改strBackZero 与 BackZero值

        int BackZero0 = 0;  //重量是否已返回到0,返回后，下次保存按扭才生效，1时不能保存
        int BackZero1 = 0;
        int BackZero2 = 0;
        int BackZero3 = 0;  //重量是否已返回到0,返回后，下次保存按扭才生效，1时不能保存
        int BackZero4 = 0;
        int BackZero5 = 0;  //重量是否已返回到0,返回后，下次保存按扭才生效，1时不能保存
        int BackZero6 = 0;  //重量是否已返回到0,返回后，下次保存按扭才生效，1时不能保存
        int BackZero7 = 0;  //重量是否已返回到0,返回后，下次保存按扭才生效，1时不能保存
        int BackZero8 = 0;  //重量是否已返回到0,返回后，下次保存按扭才生效，1时不能保存
        int BackZero9 = 0;  //重量是否已返回到0,返回后，下次保存按扭才生效，1时不能保存
        int[] BackZero;  //重量是否已返回到0数组,返回后，下次保存按扭才生效，1时不能保存

        private BaseData[] m_BaseInfoArray; //汽车衡计量点对应的基础表数组  //计量点个数用tom定义的private int m_nPointCount

        int k;  //硬盘录像机视频调节参数，是具体调节哪个视频
        int BigChannel = 0;  //放大图片句柄
        int m_CurSelBigChannel = -1;//当前放大的是哪一个通道

        private Thread GraspImageThread;  //抓图线程
        private int m_GraspImageSign = 0; //抓图线程标志，0为未启动，1为启动

        private Thread SaveImageThread;   //保存图片线程
        private int m_SaveImageSign = 0; //保存图片线程标志，0为未启动，1为启动

        //构建一个结构，打印用
        struct printInfo
        {
            public string printCZH; //车证卡号
            public string printCH; //车号
            public string printHTH; //合同号
            public string printWLMC; //物料名称
            public string printFHDW; //发货单位
            public string printSHDW; //收货单位
            public string printCYDW; //承运单位
            public string printJLLX; //计量类型
            public string printJLD;  //计量点
            public string printJLY;  //计量员
            public string printPZ;  //打印皮重
            public string printMZ;  //打印毛重
            public string printJZ;  //打印净重
            public string printKHJZ; //扣后净重
            public string printYKL; //应扣量
            public string printYKBL; //应扣比例

            public string pringJLCS; //一次计量还是二次计量
            public string printCS;//钢坯一车多炉打印标志，'1'为第一炉，'2'为第二炉，'3'为第三炉。 '0'为所有

            public string printLH; //炉号
            public string printZS; //支数
            public string printGZ; //钢种
            public string printGG; //规格

            public string printLH1; //炉号
            public string printZS1; //支数
            public string printLH2; //炉号
            public string printZS2; //支数
            public string printLH3; //炉号
            public string printZS3; //支数
            public string printAdviseSpec; //建议轧制规格
            public string printZZJY; //轧制建议
        }
        private printInfo print = new printInfo();
        string strCode = ""; //磅单条码号

        private string ifStart = "0"; //为0时默认启动txtCZH的回车查询事件，为1时关闭txtCZH的回车查询事件,为钢坯秤,1为钢坯保存,0为汽车其它保存

        public delegate void BindUltraGridDelegate();//绑定委托
        private BindUltraGridDelegate m_BindUltraGridDelegate;//建立委托变量


        private System.Data.DataTable m_MaterialTable = new System.Data.DataTable();//磅房对应物料内存表;将所有磅房对应的物料下载到该表
        public System.Data.DataTable tempMaterial = new System.Data.DataTable();  //物料临时表；用于磅房筛选和助记码筛选

        private System.Data.DataTable m_ReveiverTable = new System.Data.DataTable();//收货方
        public System.Data.DataTable tempReveiver = new System.Data.DataTable();

        private System.Data.DataTable m_SenderTable = new System.Data.DataTable();//发货方
        public System.Data.DataTable tempSender = new System.Data.DataTable();

        private System.Data.DataTable m_ProviderTable = new System.Data.DataTable();//供应商
        public System.Data.DataTable tempProvider = new System.Data.DataTable();

        private System.Data.DataTable m_TransTable = new System.Data.DataTable();//承运方
        public System.Data.DataTable tempTrans = new System.Data.DataTable();

        private System.Data.DataTable m_CarNoTable = new System.Data.DataTable();//车号
        public System.Data.DataTable tempCarNo = new System.Data.DataTable();

        private System.Data.DataTable m_FlowTable = new System.Data.DataTable();//流向表


        private System.Windows.Forms.ListBox m_List = new System.Windows.Forms.ListBox(); //下拉列表框
        private string m_ListType = "";  //下拉列表框类型 

        decimal s_toZore0 = 0;//软清零
        decimal s_toZore1 = 0;//软清零
        decimal s_toZore2 = 0;//软清零
        decimal s_toZore3 = 0;//软清零
        decimal s_toZore4 = 0;//软清零
        decimal s_toZore5 = 0;//软清零
        decimal s_toZore6 = 0;//软清零
        decimal s_toZore7 = 0;//软清零
        decimal s_toZore8 = 0;//软清零
        decimal s_toZore9 = 0;//软清零

        decimal[] s_toZore;//软清零数组

        string strBCSJ = "0";//在保存时不需要计算净重

        string s_Guid = "";//钢坯一车多炉保存时，存储过程返回的Guid，用于保存图片
        string ImageGPZL = "";//钢坯一车多炉保存时打印到图片上的重量

        //钢坯按车号自动查询一次
        int querycs = 0;

        private bool b_daozha0 = true;
        private bool b_daozha1 = true;
        private bool b_daozha2 = true;
        private bool b_daozha3 = true;
        private bool b_daozha4 = true;
        private bool b_daozha5 = true;
        private bool b_daozha6 = true;
        private bool b_daozha7 = true;
        private bool b_daozha8 = true;
        private bool b_daozha9 = true;

        private bool[] b_daozha; //判断是否打道闸 

        private int ifControlDaozha0 = 0;
        private int ifControlDaozha1 = 0;
        private int ifControlDaozha2 = 0;
        private int ifControlDaozha3 = 0;
        private int ifControlDaozha4 = 0;
        private int ifControlDaozha5 = 0;
        private int ifControlDaozha6 = 0;
        private int ifControlDaozha7 = 0;
        private int ifControlDaozha8 = 0;
        private int ifControlDaozha9 = 0;

        private int[] ifControlDaozha; //是否手动控制道闸，0为自动，1为手动
        #endregion

        #region add by tom
        //add by tom
        private PoundRoom[] m_PoundRoomArray;//汽车衡计量点数组
        private int m_nPointCount;//计量点个数
        private bool m_bRunningForPoundRoom;//计量点线程运行开关
        private bool m_bPoundRoomThreadClosed;//计量点线程关闭开关
        private System.Threading.Thread m_hThreadForPoundRoom;//计量点线程句柄

        private int m_iSelectedPound;//选择的计量点索引，用于计量点切换时RecordClose等方法使用

        public delegate void CapPicture();//抓图委托
        private CapPicture m_MainThreadCapPicture;//建立委托变量


        public int m_AlarmCount = 1;    // 停车未到停车线，语音提示一次，该值为0，车下磅后该值恢复为1。

        public delegate void AlarmVoice();//播放音频
        private AlarmVoice m_MainThreadAlarmVoice;//建立委托变量
        public string m_AlarmVoicePath = ""; //声音文件路径

        #endregion

        SerialPort s_SerialPort = null;//屏幕显示数据串口句柄

        BaseInfo getImage = new BaseInfo();//实例化图片公共方法

        public WeighMeasureInfo()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            m_iSelectedPound = -1;
            m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
            m_bRunningForPoundRoom = false;//计量点线程运行开关
        }

        #region add by tom methods

        //add 截图
        private void MainThreadCapPicture()
        {
            m_GraspImageSign = 1; //如果为1，则下次不能再开启线程
            int i = m_iSelectedPound;

            strNumber = m_PoundRoomArray[i].POINTID;
            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";
            fileName3 = strNumber + "3.bmp";
            fileName4 = strNumber + "4.bmp";
            fileName5 = strNumber + "5.bmp";
            fileName6 = strNumber + "6.bmp";
            fileName7 = strNumber + "7.bmp";
            fileName8 = strNumber + "8.bmp";
            fileName9 = strNumber + "9.bmp";
            fileName10 = strNumber + "10.bmp";

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].POINTID != ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//非当前正在操作的计量点
            {
                return;
            }

            //抓动态曲线图
            try
            {
                Bitmap img = getscreenfromhandle((int)picHT.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName7, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //抓第一张图
            try
            {
                //m_PoundRoomArray[i].VideoRecord.SDK_CapturePicture(m_PoundRoomArray[i].Channel1, stRunPath + "\\qcpicture\\" + fileName1);
                Bitmap img = getscreenfromhandle((int)this.VideoChannel1.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName1, ImageFormat.Bmp);
                Thread.Sleep(200);

            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
               // sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel2, stRunPath + "\\qcpicture\\" + fileName2);
                Bitmap img = getscreenfromhandle((int)this.VideoChannel2.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName2, ImageFormat.Bmp);
                Thread.Sleep(200);

            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第三张图
            try
            {
                //sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel3, stRunPath + "\\qcpicture\\" + fileName3);
                Bitmap img = getscreenfromhandle((int)VideoChannel3.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName3, ImageFormat.Bmp);
                Thread.Sleep(200);

            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //抓第四张图
            try
            {
                //sdk.SDK_CapturePicture(relhandle4, stRunPath + "\\qcpicture\\" + fileName4);
                //sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel4, stRunPath + "\\qcpicture\\" + fileName4);
                Bitmap img = getscreenfromhandle((int)VideoChannel4.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName4, ImageFormat.Bmp);
                Thread.Sleep(200);


            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第五张图
            try
            {
                
                //sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel5, stRunPath + "\\qcpicture\\" + fileName5);
                Bitmap img = getscreenfromhandle((int)VideoChannel5.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName5, ImageFormat.Bmp);
                Thread.Sleep(200);

            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第六张图
            try
            {
                //sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel6, stRunPath + "\\qcpicture\\" + fileName5);
                Bitmap img = getscreenfromhandle((int)VideoChannel6.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName6, ImageFormat.Bmp);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //抓第七张图
            try
            {
                //sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel7, stRunPath + "\\qcpicture\\" + fileName5);
                Bitmap img = getscreenfromhandle((int)VideoChannel7.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName7, ImageFormat.Bmp);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //抓第八张图
            try
            {
                //sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel8, stRunPath + "\\qcpicture\\" + fileName5);
                Bitmap img = getscreenfromhandle((int)VideoChannel8.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName8, ImageFormat.Bmp);
                Thread.Sleep(200);
            }
            catch (System.Exception error)
            {
                MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            m_GraspImageSign = 0;

            if (cbJLLX.Text.Trim() != "复磅")
            {

                if (strYCJL != "" || strQXPZ != "")
                {
                    UpdateTPData(strZYBH);
                    if (ifStart == "1" && cbLS.Text.Trim() == "还不是最后一炉")
                    {
                        m_SaveImageSign = 0;
                        AddTPData(strZYBH + "1");
                    }
                }

                else
                    AddTPData(strZYBH);


            }
            if (cbJLLX.Text.Trim() == "复磅")
            {
                DeleteTPData(strZYBH);
                AddTPData(strZYBH);
            }

        }

        //停止计量点线程
        public void StopPoundRoomThread()
        {
            m_bRunningForPoundRoom = false;//停止计量点线程

            //最多等待5秒，让计量点线程自动退出
            for (int nCount = 0; nCount < 50; nCount++)
            {
                if (m_bPoundRoomThreadClosed == true)
                    break;
                System.Threading.Thread.Sleep(100);
            }

            if (m_PoundRoomArray != null && m_PoundRoomArray.Length > 0)
            {
                for (int j = 0; j < m_PoundRoomArray.Length; j++)
                {
                    if (m_PoundRoomArray[j] != null && m_PoundRoomArray[j].Signed)
                    {
                        m_PoundRoomArray[j].StopUse();
                        RecordClose(j);
                        m_PoundRoomArray[j].Signed = false;
                    }
                }
            }
        }

        /// <summary>
        /// 根据数据库查询JL_POINTINFO结果初始化计量点
        /// </summary>
        /// <param name="dt"></param>
        private void InitPound(DataTable dt)
        {
            StopPoundRoomThread();//停止计量点处理线程

            //构建计量点列表，所用信息从查询计量点表 JL_POINTINFO 获取
            m_nPointCount = dt.Rows.Count;
            m_PoundRoomArray = new PoundRoom[m_nPointCount];
            int i = 0;
            for (i = 0; i < m_nPointCount; i++)
            {
                m_PoundRoomArray[i] = new PoundRoom();
                m_PoundRoomArray[i].POINTID = dt.Rows[i]["FS_POINTCODE"].ToString().Trim();
                m_PoundRoomArray[i].POINTNAME = dt.Rows[i]["FS_POINTNAME"].ToString().Trim();
                m_PoundRoomArray[i].POINTTYPE = dt.Rows[i]["FS_POINTTYPE"].ToString().Trim();

                m_PoundRoomArray[i].VIEDOIP = dt.Rows[i]["FS_VIEDOIP"].ToString().Trim();
                m_PoundRoomArray[i].VIEDOPORT = dt.Rows[i]["FS_VIEDOPORT"].ToString().Trim();
                m_PoundRoomArray[i].VIEDOUSER = dt.Rows[i]["FS_VIEDOUSER"].ToString().Trim();
                m_PoundRoomArray[i].VIEDOPWD = dt.Rows[i]["FS_VIEDOPWD"].ToString().Trim();

                m_PoundRoomArray[i].METERTYPE = dt.Rows[i]["FS_METERTYPE"].ToString().Trim();
                m_PoundRoomArray[i].METERPARA = dt.Rows[i]["FS_METERPARA"].ToString().Trim();
                m_PoundRoomArray[i].MOXAIP = dt.Rows[i]["FS_MOXAIP"].ToString().Trim();
                m_PoundRoomArray[i].MOXAPORT = dt.Rows[i]["FS_MOXAPORT"].ToString().Trim();

                m_PoundRoomArray[i].RTUIP = dt.Rows[i]["FS_RTUIP"].ToString().Trim();
                m_PoundRoomArray[i].RTUPORT = dt.Rows[i]["FS_RTUPORT"].ToString().Trim();

                m_PoundRoomArray[i].PRINTERIP = dt.Rows[i]["FS_PRINTERIP"].ToString().Trim();
                m_PoundRoomArray[i].PRINTERNAME = dt.Rows[i]["FS_PRINTERNAME"].ToString().Trim();
                m_PoundRoomArray[i].PRINTTYPECODE = dt.Rows[i]["FS_PRINTTYPECODE"].ToString().Trim();
                m_PoundRoomArray[i].USEDPAPER = Convert.ToInt32(dt.Rows[i]["FN_USEDPRINTPAPER"].ToString().Length > 0 ? dt.Rows[i]["FN_USEDPRINTPAPER"].ToString().Trim() : "0");
                //m_PoundRoomArray[i].TOTALPAPAR = Convert.ToInt32(dt.Rows[i]["TOTALPAPAR"].ToString().Length > 0 ? dt.Rows[i]["TOTALPAPAR"].ToString() : "0");
                //m_PoundRoomArray[i].STATUS = dt.Rows[i]["STATUS"].ToString();
                //m_PoundRoomArray[i].ACCEPTTERMINAL = dt.Rows[i]["ACCEPTTERMINAL"].ToString();

                m_PoundRoomArray[i].LEDPORT = dt.Rows[i]["FS_LEDPORT"].ToString().Trim();
                m_PoundRoomArray[i].LEDPARA = dt.Rows[i]["FS_LEDIP"].ToString().Trim();
                m_PoundRoomArray[i].LEDTYPE = dt.Rows[i]["FS_LEDTYPE"].ToString().Trim();

                m_PoundRoomArray[i].READERPORT = dt.Rows[i]["FS_READERPORT"].ToString().Trim();
                m_PoundRoomArray[i].READERPARA = dt.Rows[i]["FS_READERPARA"].ToString().Trim();
                m_PoundRoomArray[i].READERTYPE = dt.Rows[i]["FS_READERTYPE"].ToString().Trim();

                m_PoundRoomArray[i].DISPLAYPORT = dt.Rows[i]["FS_DISPLAYPORT"].ToString().Trim();
                m_PoundRoomArray[i].DISPLAYPARA = dt.Rows[i]["FS_DISPLAYPARA"].ToString().Trim();
                m_PoundRoomArray[i].DISPLAYTYPE = dt.Rows[i]["FS_DISPLAYTYPE"].ToString().Trim();

                m_PoundRoomArray[i].ZEROVALUE = Convert.ToDecimal(dt.Rows[i]["FN_VALUE"].ToString().Length > 0 ? dt.Rows[i]["FN_VALUE"].ToString().Trim() : "0");

                m_PoundRoomArray[i].CLEARVALUE = Convert.ToDecimal(dt.Rows[i]["FF_CLEARVALUE"].ToString().Length > 0 ? dt.Rows[i]["FF_CLEARVALUE"].ToString().Trim() : "0.00");

                //判断是否使用仪表
                m_PoundRoomArray[i].UseMeter = m_PoundRoomArray[i].METERPARA.Length > 0 ? true : false;
                //判断是否使用读卡器
                m_PoundRoomArray[i].UseReader = m_PoundRoomArray[i].READERPARA.Length > 0 ? true : false;
                //判断是否使用LED
                m_PoundRoomArray[i].UseLED = m_PoundRoomArray[i].LEDPARA.Length > 0 ? true : false;
                //判断是否使用液晶屏
                m_PoundRoomArray[i].UseDisplay = m_PoundRoomArray[i].DISPLAYPARA.Length > 0 ? true : false;

                //判断是否使用Rtu
                m_PoundRoomArray[i].UseRtu = m_PoundRoomArray[i].RTUIP.Length > 0 ? true : false;

                m_PoundRoomArray[i].Signed = dt.Rows[i]["XZ"].ToString().ToUpper().Trim() == "TRUE" ? true : false;

                //计量点状态
                m_PoundRoomArray[i].POINTSTATE = dt.Rows[i]["FS_POINTSTATE"].ToString().Trim();

            }
        }

        private void BeginPoundRoomThread()
        {
            m_bRunningForPoundRoom = true;
            m_bPoundRoomThreadClosed = false;
            m_hThreadForPoundRoom = new System.Threading.Thread(new System.Threading.ThreadStart(PoundRoomThread));
            m_hThreadForPoundRoom.Start();
        }

        /// <summary>
        /// 非当前操作计量点来车提示线程
        /// </summary>
        /// <param name="sequence"></param>
        private void FlashGridRow(object sequence)
        {
            int i = (int)sequence;

            while (ultraGrid2.ActiveRow.Index != i && m_bRunningForPoundRoom)
            {
                ultraGrid2.Rows[i].Appearance.BackColor = Color.Red;
                Thread.Sleep(300);
                ultraGrid2.Rows[i].Appearance.BackColor = Color.White;
                Thread.Sleep(300);
            }
            ultraGrid2.Rows[i].Appearance.BackColor = Color.White;
        }

        /// <summary>
        /// 仪表重量大于复位值处理
        /// </summary>
        /// <param name="iPoundRoom">计量点索引，从0开始</param>
        private void HandleWeightPound(int iPoundRoom, decimal decData)
        {
            int i = iPoundRoom;

            if (Math.Abs(m_PoundRoomArray[i].MeterPreData - decData) < (Decimal)m_PoundRoomArray[i].CLEARVALUE)//稳定，考虑增加稳定计数参考值到数据库，不要写死0.1
            {
                m_PoundRoomArray[i].MeterStabTimes += 1;
            }
            else
            {
                m_PoundRoomArray[i].MeterStabTimes = 0;
            }
            m_PoundRoomArray[i].MeterPreData = decData;

            //if (ifControlDaozha[i] == 0 && chb_Autocontrol.Checked == true)
            //{
            //    if (b_daozha[i] == false && m_PoundRoomArray[i].MeterStabTimes > 6)
            //    {
            //        if (StatusCome.Down == false)
            //        {
            //            //上磅道闸落下
            //            m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE1 - 1), (byte)0xFF, (byte)0);
            //            ifControlDaozha[i] = 1;
            //        }
            //    }
            //}

            if (m_PoundRoomArray[i].STATUS != "USE")//first time
            {
                m_PoundRoomArray[i].STATUS = "USE";

                //非当前操作计量点来车Flash提示，只执行一次
                if (i != ultraGrid2.ActiveRow.Index)
                {
                    m_PoundRoomArray[i].STATUS = "USE";
                    Thread parameterThread = new Thread(new ParameterizedThreadStart(FlashGridRow));
                    parameterThread.Name = "Thread A:";
                    parameterThread.Start(i);
                }

                //播放声音
                m_PoundRoomArray[i].PlaySound(YGJZJL.PublicComponent.Constant.RunPath + "\\CarArrive.wav");

                //RTU控制
                if (m_PoundRoomArray[i].UseRtu)
                {
                    //红灯亮
                    m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xDE - 1), (byte)0xFF, (byte)0);
                    //if (chb_Autocontrol.Checked == true)
                    //{
                        //if () //m_PoundRoomArray[i].POINTID == "K01" || m_PoundRoomArray[i].POINTID == "K02"
                        //{
                        //下磅道闸落下
                        m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0xFF, (byte)0);
                        //System.Threading.Thread.Sleep(3000);
                        //if (b_daozha[i] == false && m_PoundRoomArray[i].MeterStabTimes > 6)
                        //{
                        //    //上磅道闸落下
                        //    m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE1 - 1), (byte)0xFF, (byte)0);
                        //}

                        //}
                    //}
                }
                //液晶屏显示
                if (m_PoundRoomArray[i].UseDisplay)
                {
                    //m_PoundRoomArray[i].Display.ClearScreen();//记得每次处理一组事务前先清屏
                    ////m_PoundRoomArray[i].Display.WriteText(0x00, 0x00, 0x24, 0x81, 0x07, Color.Blue, Color.Black, "欢迎使用昆钢集中计量系统，请刷车证卡，请等待......");
                    //m_PoundRoomArray[i].Display.WriteText(0x00, 0x00, 0x55, "操作提示:请刷车证卡");
                    //m_PoundRoomArray[i].Display.WriteText(0x00, 0x20, 0x55, "车证卡号：未知 车号：未知");
                    //m_PoundRoomArray[i].Display.WriteText(0x00, 0x40, 0x55, "发货单位：未知");
                    //m_PoundRoomArray[i].Display.WriteText(0x00, 0x60, 0x55, "收货单位：未知");
                    //m_PoundRoomArray[i].Display.WriteText(0x00, 0x80, 0x55, "承运单位：未知");
                    //m_PoundRoomArray[i].Display.WriteText(0x00, 0xA0, 0x55, "重量：未知");

                    //m_PoundRoomArray[i].Display.DrawClipPicture(1, 0, 0, 1024, 568, 0, 200);
                }
            }
        }

        /// <summary>
        /// 仪表重量小于复位值处理
        /// </summary>
        /// <param name="iPoundRoom"></param>
        /// <param name="decData"></param>
        private void HandleZeroPound(int iPoundRoom, decimal decData)
        {
            int i = iPoundRoom;
            m_AlarmCount = 1;//允许播放停车未到位置的语音 

            if (m_PoundRoomArray[i].STATUS == "USE")//下秤事件
            {
                //RTU控制
                if (m_PoundRoomArray[i].UseRtu)
                {
                    //绿灯亮，0x00
                    m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xDE - 1), (byte)0x00, (byte)0);

                    //if (chb_Autocontrol.Checked == true)
                    //{
                        //if () //m_PoundRoomArray[i].POINTID == "K01" || m_PoundRoomArray[i].POINTID == "K02"  
                        //{
                        //上磅道闸打开
                        m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE1 - 1), (byte)0x00, (byte)0);
                        //System.Threading.Thread.Sleep(3000);
                        //下磅道闸关闭
                        //m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0xFF, (byte)0);
                        //}
                    //}
                }
                //液晶屏显示缺省欢迎画面
                if (m_PoundRoomArray[i].UseDisplay)
                {
                    int sleepTime = 20;
                    if (m_PoundRoomArray[i].POINTID == "K10")
                        sleepTime = 100;

                    m_PoundRoomArray[i].Display.ClearScreen();//记得每次处理一组事务前先清屏
                    Thread.Sleep(sleepTime);
                    m_PoundRoomArray[i].Display.DrawPicture(1);
                }

                if (m_PoundRoomArray[i].POINTID == ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//当前正在操作的计量点，刷新界面
                {
                    //清空控件数据
                    ClearData();
                    ClearCarNoData();
                    ClearImageAndWeight();
                    ClearControlData();
                    ClearQXPZData();
                }
            }

            m_PoundRoomArray[i].ClearCardNoAndGuid();
            m_PoundRoomArray[i].CardNo = "";
            m_PoundRoomArray[i].ReaderGUID = "";
            m_PoundRoomArray[i].STATUS = "IDLE";
            m_PoundRoomArray[i].MeterStabTimes = 0;
            //ClearData();
            //ClearCarNoData();//没来车时就输不了卡号，放上面USE条件里面清空，在为空磅时也能输入所有信息
        }

        /// <summary>
        /// 动态绘图
        /// </summary>
        private void DrawImageData(int iPoundRoom, decimal decData)
        {
            //chenyx添加
            //构建计量点曲线图列表,在加载窗体是就定义了，因为curveColumns必须要在前面定义，为了统一管理，就在前面一起定义了
            //dtQXT = new DataTable[10] { dtQXT0, dtQXT1, dtQXT2, dtQXT3, dtQXT4, dtQXT5, dtQXT6, dtQXT7, dtQXT8, dtQXT9 };
            //curveColumns = new int[10] { curveColumns0, curveColumns1, curveColumns2, curveColumns3, curveColumns4, curveColumns5, curveColumns6, curveColumns7, curveColumns8, curveColumns9 };

            int i = iPoundRoom;
            //动态绘制曲线图
            if (m_PoundRoomArray[i].MeterStabTimes < 6 && strBackZero[i] == 0)
            {
                if (dtQXT[i].Rows.Count == 0)
                {
                    DataColumn dc = new DataColumn("ZL1", typeof(decimal));
                    dtQXT[i].Columns.Add(dc);

                    DataRow dr = dtQXT[i].NewRow();
                    dr[0] = decData;
                    dtQXT[i].Rows.Add(dr);
                    dtQXT[i].AcceptChanges();

                    curveColumns[i] = 1;
                }
                else
                {
                    curveColumns[i] = curveColumns[i] + 1;
                    DataColumn dc = new DataColumn("ZL" + curveColumns[i], typeof(decimal));
                    dtQXT[i].Columns.Add(dc);

                    for (int c = 0; c <= 4000; c++)
                    {
                        if (dtQXT[i].Rows[0][c].ToString() == "")
                        {
                            dtQXT[i].Rows[0][c] = decData;
                            dtQXT[i].AcceptChanges();

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 处理仪表采集数据
        /// </summary>
        /// <param name="iPoundRoom">计量点索引，从0开始</param>
        private void HandleMeterData(int iPoundRoom)
        {
            int i = iPoundRoom;
            //int curveColumns = 0;  //计算曲线图列，总共到第几列了

            string strMeterData = m_PoundRoomArray[i].MeterData;
            Decimal decData = m_PoundRoomArray[i].MeterValue;
            if (strMeterData != null && strMeterData.Length > 0)
            {

                if (decData > m_PoundRoomArray[i].ZEROVALUE)//大于复位值
                {
                    HandleWeightPound(i, decData);
                    DrawImageData(i, decData);
                    if (BackZeroSign[i] == 1)
                    {
                        BackZero[i] = 0; //重新开启保存按扭，0允许保存
                    }
                }
                else//空磅
                {
                    HandleZeroPound(i, decData);

                    //清空曲线图表
                    if (dtQXT[i].Rows.Count > 0)
                    {
                        dtQXT[i].Rows.Clear();
                        dtQXT[i].Columns.Clear();
                    }
                    //ultraChart1.DataSource = dataTable6;
                    strBackZero[i] = 0; //重量是否已返回到0数组，要求重量返回到0后，再开始重新绘图
                    BackZeroSign[i] = 0; //上称多次稳定，曲线图可以变化画

                    ifControlDaozha[i] = 0;

                    //BackZero[i] = 0; //重新开启保存按扭，这里不行，要是车开始在磅台上，保存就不能点
                }

                if (m_PoundRoomArray[i].POINTID == ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//当前正在操作的计量点，刷新界面
                {
                    //txtXSZL.Text = decData.ToString();
                    if (m_PoundRoomArray[i].MeterStabTimes > 6)
                    {
                        lbWD.Text = "稳定";
                        lbYS.ForeColor = Color.Green;
                        strBackZero[i] = 1; //1表示重量稳定
                        txtZL.Text = (decData - s_toZore[m_iSelectedPound]).ToString();
                        //btnBC.Enabled = true;

                        if (BackZeroSign[i] == 0)
                        {
                            if (BackZero[i] == 0)
                            {
                                btnBC.Enabled = true;
                                btnGPBC.Enabled = true;
                            }
                        }
                        if (strBCSJ == "0") //保存按钮没启动，不再计算毛皮净
                        {
                            CountWeight();
                        }
                        //BackZero[i] = 1; //重新关闭保存按扭
                    }
                    else
                    {
                        if (m_PoundRoomArray[i].STATUS == "IDLE")
                        {
                            lbWD.Text = "空磅";
                            lbYS.ForeColor = Color.Red;
                            txtZL.Text = "";
                            btnBC.Enabled = false;
                            btnGPBC.Enabled = false;
                            //清空曲线图表
                            dtQXT[i].Rows.Clear();
                            dtQXT[i].Columns.Clear();
                            strBackZero[i] = 0;  //重量是否已返回到0数组，要求重量返回到0后，再开始重新绘图
                            //BackZero = 0; //重新开启保存按扭
                        }
                        else
                        {
                            lbWD.Text = "不稳定";
                            lbYS.ForeColor = Color.Red;
                            if (BackZeroSign[i] != 1) //如果没保存，BackZeroSign[i]标志为0，保存后，则为1
                            {
                                strBackZero[i] = 0;
                            }
                            txtZL.Text = "";
                            btnBC.Enabled = false;
                            btnGPBC.Enabled = false;
                            //BackZero = 1;
                        }
                    }
                    if (m_PoundRoomArray[i].MeterStabTimes < 6)
                    {
                        BackZero[i] = 0;
                    }

                    txtXSZL.Text = (decData - s_toZore[m_iSelectedPound]).ToString();

                    //动态绘制曲线图
                    if (dtQXT[i].Rows.Count > 0)
                    {
                        ultraChart1.DataSource = dtQXT[i];
                        ultraChart1.DataBind();
                    }
                    else
                    {
                        ultraChart1.DataSource = dataTable6;
                        ultraChart1.DataBind();
                    }

                }
            }
        }

        /// <summary>
        /// 处理读卡器采集数据
        /// </summary>
        /// <param name="iPoundRoom">计量点索引，从0开始</param>
        private void HandleReaderData(int iPoundRoom)
        {
            int i = iPoundRoom;

            if (m_PoundRoomArray[i].CardNo != null && m_PoundRoomArray[i].CardNo != "")
            {

                if (m_PoundRoomArray[i].Display.State == "idle")
                {
                    int sleepTime = 20;
                    if (m_PoundRoomArray[i].POINTID == "K10")
                        sleepTime = 100;

                    m_PoundRoomArray[i].Display.ClearScreen();//记得每次处理一组事务前先清屏
                    Thread.Sleep(sleepTime);
                    m_PoundRoomArray[i].Display.DrawPicture(15); //等待计量图片
                    Thread.Sleep(sleepTime);
                    m_PoundRoomArray[i].Display.WriteText(500, 205, Color.Yellow, m_PoundRoomArray[i].CardNo);
                    m_PoundRoomArray[i].Display.State = "used";
                }
                if (m_PoundRoomArray[i].POINTID == ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//当前正在操作的计量点，刷新界面
                {
                    if (m_PoundRoomArray[i].CardNo != null)
                    {
                        if (m_PoundRoomArray[i].CardNo != "")
                        {
                            txtCZH.Text = m_PoundRoomArray[i].CardNo;
                            if (txtCarNo.Text.Trim().Length <= 0) //防止在保存过程中数据被清空
                            {
                                txtCarNo.Text = m_PoundRoomArray[i].ReaderGUID;//reset while complete
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 处理Rtu采集数据
        /// </summary>
        /// <param name="iPoundRoom">计量点索引，从0开始</param>
        private void HandleRtuData(int iPoundRoom)
        {
            int i = iPoundRoom;

            byte[] rtuData = m_PoundRoomArray[i].RtuData;
            if (rtuData == null || rtuData.Length < 10)
            {
                return;
            }

            byte[] d = new byte[1];
            d[0] = rtuData[10];

            BitArray all = new BitArray(d);

            b_daozha[i] = all.Get(7);

            if (m_PoundRoomArray[i].POINTID == ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//当前正在操作的计量点，刷新界面
            {
                //byte[] d = new byte[1];
                //d[0] = rtuData[10];

                //BitArray all = new BitArray(d);
                if (all.Get(0) == true)//照明等状态
                {
                    StatusLight.ForeColor = Color.DarkOrange;
                }
                else
                {
                    StatusLight.ForeColor = Color.Black;
                }
                if (all.Get(1) == true)//红绿灯状态
                {
                    StatusRedGreen.ForeColor = Color.Red;
                }
                else
                {
                    StatusRedGreen.ForeColor = Color.Lime;
                }
                if (all.Get(2) == true)//ATM灯状态
                {
                    //pictureBox4.Image = red;
                }
                else
                {
                    //pictureBox4.Image = green;
                }
                //if (all.Get(3) == true)//下磅道闸状态，0xE0
                //{
                //    StatusLeave.Down = true;
                //    StatusLeave.ForeColor = Color.Red;
                //}
                //else
                //{
                //    StatusLeave.Down = false;
                //    StatusLeave.ForeColor = Color.Lime;
                //}
                //if (all.Get(4) == true)//上磅道闸状态，0xE1
                //{
                //    StatusCome.Down = true;
                //    StatusCome.ForeColor = Color.Red;
                //}
                //else
                //{
                //    StatusCome.Down = false;
                //    StatusCome.ForeColor = Color.Lime;
                //}
                if (all.Get(5) == true)//电源状态
                {

                }
                else
                {

                }

                if (all.Get(6) == true)//前红外状态
                {
                    StatusFront.Connected = true;
                    StatusFront.ForeColor = Color.DarkBlue;
                }
                else
                {
                    StatusFront.Connected = false;
                    StatusFront.ForeColor = Color.Red;
                }
                //b_daozha = all.Get(7); //判断是否打道闸false打后面道闸，
                if (all.Get(7) == true)//后红外状态
                {
                    StatusBack.Connected = true;
                    StatusBack.ForeColor = Color.DarkBlue;
                }
                else
                {
                    StatusBack.Connected = false;
                    StatusBack.ForeColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 处理语音对讲按钮事件
        /// </summary>
        private void HandleTalk(int iPoundRoom)
        {
            if (iPoundRoom < 0)
            {
                return;
            }

            int i = iPoundRoom;

            if (ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption == "关闭对讲")
            {
                if (m_PoundRoomArray[i].TalkID > 0)//正在对讲，关闭
                {

                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";

                    m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(1,0,(int)picFDTP.Handle);
                    m_PoundRoomArray[i].VideoRecord.SDK_StopTalk();//SDK_StopTalk(m_PoundRoomArray[i].TalkID);
                    m_PoundRoomArray[i].TalkID = 0;
                    m_PoundRoomArray[i].Talk = false;

                    //ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                }
            }
            else
            {
                if (m_PoundRoomArray[i].VideoRecord != null && m_PoundRoomArray[i].AUDIONUM > 0)
                {
                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";

                    m_PoundRoomArray[i].TalkID = m_PoundRoomArray[i].VideoRecord.SDK_StartTalk();//SDK_StartTalk(m_PoundRoomArray[i].VideoHandle);
                    m_PoundRoomArray[i].VideoRecord.SDK_SetVolume(65500);
                    m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(1, 0, (int)picFDTP.Handle);
                    m_PoundRoomArray[i].Talk = true;

                    //ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                }
            }
        }

        /// <summary>
        /// 磅房数据处理线程
        /// </summary>
        private void PoundRoomThread()
        {
            //InitializeAllocation();

            for (int i = 0; i < m_nPointCount; i++)
            {
                if (m_PoundRoomArray[i].Signed)
                {
                    m_PoundRoomArray[i].StartUse();
                }
                System.Threading.Thread.Sleep(500);
            }

            string strMeterData = "";
            string strReaderData = "";
            decimal decData = 0;

            while (m_bRunningForPoundRoom)
            {
                for (int i = 0; i < m_nPointCount; i++)
                {
                    //使线程结束条件触发时能及时退出
                    if (m_bRunningForPoundRoom == false)
                    {
                        m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
                        return;
                    }

                    if (m_PoundRoomArray[i].Signed)
                    {
                        //使线程结束条件触发时能及时退出
                        if (m_bRunningForPoundRoom == false)
                        {
                            m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
                            return;
                        }

                        //处理仪表数据
                        if (m_PoundRoomArray[i].UseMeter)
                        {
                            HandleMeterData(i);//仪表数据处理
                        }

                        //使线程结束条件触发时能及时退出
                        if (m_bRunningForPoundRoom == false)
                        {
                            m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
                            return;
                        }

                        //处理读卡器数据
                        if (m_PoundRoomArray[i].UseReader)//reader data handle
                        {
                            HandleReaderData(i);
                        }

                        //使线程结束条件触发时能及时退出
                        if (m_bRunningForPoundRoom == false)
                        {
                            m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
                            return;
                        }

                        //处理Rtu数据
                        if (m_PoundRoomArray[i].UseRtu)//rtu data handle
                        {
                            HandleRtuData(i);
                        }
                        //分析收到的仪表采集数据strMeterData
                        //如果重量大于一定的值（这个值应该维护到计量点表里去），则表示来车，
                        //第一次则置红灯亮---PoundRoom来完成，避免线程阻塞

                        //分析收到的读卡器数据strReaderData，获取卡号
                    }

                }

                System.Threading.Thread.Sleep(100);
            }

            m_bPoundRoomThreadClosed = true;//计量点线程关闭开关
        }

        /// <summary>
        /// 打开计量点的IC卡
        /// </summary>
        /// <param name="iPoundRoom">计量点索引</param>

        private void ICCardOpen(int iPoundRoom)
        {            
 
        }

        /// <summary>
        /// 关闭计量点的IC卡
        /// </summary>
        /// <param name="iPoundRoom">计量点索引</param>

        private void ICCardClose(int iPoundRoom)
        { 
        
        }

        /// <summary>
        /// 打开计量点的硬盘录像机
        /// </summary>
        /// <param name="iPoundRoom">计量点索引</param>
        private void RecordOpen(int iPoundRoom)
        {
            int i = iPoundRoom;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].POINTID != ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//非当前正在操作的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].VideoRecord == null)
            {
                m_PoundRoomArray[i].VideoRecord = new SDK_Com.HKDVR();
                m_PoundRoomArray[i].VideoRecord.SDK_Init();
            }

            int VideoHandle = 0;
            m_PoundRoomArray[i].VideoRecord.SDK_Login(m_PoundRoomArray[i].VIEDOIP,
                                                    Convert.ToInt32(m_PoundRoomArray[i].VIEDOPORT),
                                                    m_PoundRoomArray[i].VIEDOUSER,
                                                    m_PoundRoomArray[i].VIEDOPWD);
            if (VideoHandle < 0)
            {
                MessageBox.Show("登录硬盘录像机失败，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ////add by luobin
            //uint dwYear = (uint)getImage.SynTime.wYear;
            //uint dwMonth = (uint)getImage.SynTime.wMonth;
            //uint dwDay = (uint)getImage.SynTime.wDay;
            //uint dwHour = (uint)getImage.SynTime.wHour;
            //uint dwMinute = (uint)getImage.SynTime.wMinute;
            //uint dwSecond = (uint)getImage.SynTime.wSecond;
            //int reVal = 0;
            ////设置硬盘录象机时间
            //m_PoundRoomArray[i].VideoRecord.SDK_SetupDeviceTime(VideoHandle, dwYear, dwMonth, dwDay, dwHour, dwMinute, dwSecond, ref reVal);


            m_PoundRoomArray[i].VideoHandle = VideoHandle;
            m_PoundRoomArray[i].Channel1 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(1, 0, (int)VideoChannel1.Handle);//注意第1个通道为车牌，在第4个图片显示 VideoChannel4 
            //m_PoundRoomArray[i].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[i].VideoHandle, 0, m_PoundRoomArray[i].POINTNAME + "- 车牌");

            m_PoundRoomArray[i].Channel2 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(2, 0, (int)VideoChannel2.Handle);
            //m_PoundRoomArray[i].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[i].VideoHandle, 1, m_PoundRoomArray[i].POINTNAME + "- 车头");

            m_PoundRoomArray[i].Channel3 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(3, 0, (int)VideoChannel3.Handle);
            //m_PoundRoomArray[i].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[i].VideoHandle, 2, m_PoundRoomArray[i].POINTNAME + "- 车尾");

            m_PoundRoomArray[i].Channel4 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(4, 0, (int)VideoChannel4.Handle);//注意第4个通道为车牌，在第1个图片显示 VideoChannel1
            //m_PoundRoomArray[i].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[i].VideoHandle, 3, m_PoundRoomArray[i].POINTNAME + "- 车身");

            m_PoundRoomArray[i].Channel5 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(5, 0, (int)VideoChannel5.Handle);//票据
            //m_PoundRoomArray[i].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[i].VideoHandle, 4, m_PoundRoomArray[i].POINTNAME + "- 票据");

            m_PoundRoomArray[i].Channel6 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(6, 0, (int)VideoChannel6.Handle);//液晶屏
            m_PoundRoomArray[i].Channel7 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(7, 0, (int)VideoChannel7.Handle);//磅房
            m_PoundRoomArray[i].Channel8 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(8, 0, (int)VideoChannel8.Handle);//司机
            if (m_PoundRoomArray[i].Channel1 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_OpenSound(m_PoundRoomArray[i].Channel1);
                m_PoundRoomArray[i].VideoRecord.SDK_SetVolume(65500);
            }
        }

        /// <summary>
        /// 关闭计量点的硬盘录像机
        /// </summary>
        /// <param name="iPoundRoom">计量点索引</param>
        private void RecordClose(int iPoundRoom)
        {
            int i = iPoundRoom;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].VideoRecord == null)
            {
                return;
            }

            //关闭语音对讲
            if (ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption == "关闭对讲")
            {
                if (m_PoundRoomArray[i].TalkID > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].VideoHandle);
                    m_PoundRoomArray[i].VideoRecord.SDK_StopTalk();
                    m_PoundRoomArray[i].TalkID = 0;
                    m_PoundRoomArray[i].Talk = false;

                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                }
            }

            m_PoundRoomArray[i].VideoRecord.SDK_CloseSound(m_PoundRoomArray[i].VideoHandle);

            //关闭第1通道御览
            if (m_PoundRoomArray[i].Channel1 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel1);
                m_PoundRoomArray[i].Channel1 = 0;
                VideoChannel1.Refresh();
            }

            //关闭第2通道御览
            if (m_PoundRoomArray[i].Channel2 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel2);
                m_PoundRoomArray[i].Channel2 = 0;
                VideoChannel2.Refresh();
            }

            //关闭第3通道御览
            if (m_PoundRoomArray[i].Channel3 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel3);
                m_PoundRoomArray[i].Channel3 = 0;
                VideoChannel3.Refresh();
            }

            //关闭第4通道御览
            if (m_PoundRoomArray[i].Channel4 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel4);
                m_PoundRoomArray[i].Channel4 = 0;
                VideoChannel4.Refresh();
            }

            //关闭第5通道御览
            if (m_PoundRoomArray[i].Channel5 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel5);
                m_PoundRoomArray[i].Channel5 = 0;
                VideoChannel5.Refresh();
            }

            //关闭第6通道御览
            if (m_PoundRoomArray[i].Channel6 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel6);
                m_PoundRoomArray[i].Channel6 = 0;
                VideoChannel6.Refresh();
            }
            //关闭第7通道御览
            if (m_PoundRoomArray[i].Channel7 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel7);
                m_PoundRoomArray[i].Channel7 = 0;
                VideoChannel7.Refresh();
            }

            //关闭第8通道御览
            if (m_PoundRoomArray[i].Channel8 > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel8);
                m_PoundRoomArray[i].Channel8 = 0;
                VideoChannel8.Refresh();
            }

            //m_PoundRoomArray[i].VideoRecord.SDK_Logout(m_PoundRoomArray[i].VideoHandle);
            m_PoundRoomArray[i].VideoRecord.SDK_Logout();
            m_PoundRoomArray[i].VideoRecord.SDK_Cleanup();
            m_PoundRoomArray[i].VideoRecord = null;

        }

        #endregion

        #region 重要的方法

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(int hwnd, ref RECT rc);

        [DllImportAttribute("gdi32.dll")]
        public static extern IntPtr CreateDC(
                string lpszDriver,   //   驱动名称   
                string lpszDevice,   //   设备名称   
                string lpszOutput,   //   无用，可以设定位"NULL"   
                IntPtr lpInitData   //   任意的打印机数据   
                );

        [DllImport("Gdi32.dll")]
        public static extern bool BitBlt(
                IntPtr hdcDest,   //   目标设备的句柄   
                int nXDest,   //   目标对象的左上角的X坐标   
                int nYDest,   //   目标对象的左上角的X坐标   
                int nWidth,   //   目标对象的矩形的宽度   
                int nHeight,   //   目标对象的矩形的长度   
                IntPtr hdcSrc,   //   源设备的句柄   
                int nXSrc,   //   源对象的左上角的X坐标   
                int nYSrc,   //   源对象的左上角的X坐标   
                System.Int32 dwRop   //   光栅的操作值   
                );

        #endregion

        #region 初始化窗体和下拉框
        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeighMeasureInfo_Load(object sender, EventArgs e)
        {

            card = new IcCard();
            card.PortName = "COM1";


            getImage.ob = this.ob;
            getImage.SynServerTime();//同步服务器时间
            //InphaseServerTime cc = new InphaseServerTime();
            //cc.Check_Before_Login();
            //m_SerialPort1 = new SerialPort();
            //m_SerialPort2 = new SerialPort();
            //m_SerialPort7 = new SerialPort();
            //m_SerialPort = new SerialPort[10] { m_SerialPort1, m_SerialPort2, m_SerialPort3, m_SerialPort4, m_SerialPort5, m_SerialPort6, m_SerialPort7, m_SerialPort8, m_SerialPort9, m_SerialPort10 };
            m_MainThreadCapPicture= new CapPicture (MainThreadCapPicture);
            //构建计量点曲线图列表与动态列
            dtQXT = new DataTable[10] { dtQXT0, dtQXT1, dtQXT2, dtQXT3, dtQXT4, dtQXT5, dtQXT6, dtQXT7, dtQXT8, dtQXT9 };
            curveColumns = new int[10] { curveColumns0, curveColumns1, curveColumns2, curveColumns3, curveColumns4, curveColumns5, curveColumns6, curveColumns7, curveColumns8, curveColumns9 };
            strBackZero = new int[10] { strBackZero0, strBackZero1, strBackZero2, strBackZero3, strBackZero4, strBackZero5, strBackZero6, strBackZero7, strBackZero8, strBackZero9 };
            BackZeroSign = new int[10] { BackZeroSign0, BackZeroSign1, BackZeroSign2, BackZeroSign3, BackZeroSign4, BackZeroSign5, BackZeroSign6, BackZeroSign7, BackZeroSign8, BackZeroSign9 };
            BackZero = new int[10] { BackZero0, BackZero1, BackZero2, BackZero3, BackZero4, BackZero5, BackZero6, BackZero7, BackZero8, BackZero9 };
            s_toZore = new decimal[10] { s_toZore0, s_toZore1, s_toZore2, s_toZore3, s_toZore4, s_toZore5, s_toZore6, s_toZore7, s_toZore8, s_toZore9 };

            b_daozha = new bool[10] { b_daozha0, b_daozha1, b_daozha2, b_daozha3, b_daozha4, b_daozha5, b_daozha6, b_daozha7, b_daozha8, b_daozha9 };

            ifControlDaozha = new int[10] { ifControlDaozha0, ifControlDaozha1, ifControlDaozha2, ifControlDaozha3, ifControlDaozha4, ifControlDaozha5, ifControlDaozha6, ifControlDaozha7, ifControlDaozha8, ifControlDaozha9 };

            stRunPath = System.Environment.CurrentDirectory;//当前界面自己定义路径

            Constant.RunPath = System.Environment.CurrentDirectory;//Core.KgMcms.PublicComponent带的定义路径

            //Constant.SetViewStyle(this);

            ControlerInit();
            //GetLXData();
            DataGridInit();
            QueryJLDData();
           // QueryYYBBData(); 该方法为语音播报方法  Load调用该方法时ultraToolbarsManager冲突 ultraToolbarsManager 不显示
            panelYCJL.Width = 652;
            panelYCJL.Visible = false;//一次计量图片
            //panelYCSP.Height = 209;
            picFDTP.Visible = false;//双击放大图片
            panel22.Visible = true;//查询出一次计量车号图片
            txtJLY.Text = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            txtBC.Text = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
            //chbQXPZ.Enabled = false;

            sdk = new SDK_Com.HKDVR();//硬盘录像机实例化
            sdk.SDK_Init();//必须要初始化
            //ConnectYPLXJ();

            DataRow dro = dataTable6.NewRow();
            dro[0] = 0;
            dataTable6.Rows.Add(dro);
            ultraChart1.DataSource = dataTable6;

            lbYS.ForeColor = Color.Red;


            //GetLXData();
            //for (int i = 1; i <= 6; i++)
            //{
            //    Button btnItemName = (Button)panelYYBF.Controls.Find("btn" + Convert.ToString(i), true)[0];
            //    btnItemName.Enabled = false;
            //}
            //for (int i = 1; i <= 15; i++)
            //{
            //    if (i != 9)
            //    {
            //        Button btnItemName = (Button)panelSPKZ.Controls.Find("button" + Convert.ToString(i), true)[0];
            //        btnItemName.Enabled = false;
            //    }
            //}

            s_SerialPort = new SerialPort("COM2", 115200, Parity.None, 8, StopBits.One); //屏幕显示数据实例化

            m_SerialPort1 = new SerialPort();
            m_SerialPort2 = new SerialPort();
            m_SerialPort3 = new SerialPort();
            m_SerialPort4 = new SerialPort();
            m_SerialPort5 = new SerialPort();
            m_SerialPort6 = new SerialPort();
            m_SerialPort7 = new SerialPort();
            m_SerialPort8 = new SerialPort();
            m_SerialPort9 = new SerialPort();
            m_SerialPort10 = new SerialPort();
            m_SerialPort11 = new SerialPort();
            m_SerialPort12 = new SerialPort();
            m_SerialPort13 = new SerialPort();
            m_SerialPort14 = new SerialPort();

            QueryYCBData();
            ClearYBData();
            ClearYCBData();

            InitConfig();
            this.BuildMyTable();//构建内存表格式
            this.DownLoadMaterial(); //下载磅房对应物料信息到内存表
            this.DownLoadReceiver();  //下载磅房对应收货单位信息到内存表
            this.DownLoadSender();  //下载磅房对应发货单位信息到内存表
            this.DownLoadTrans();  //下载磅房对应承运单位信息到内存表
            this.DownLoadCarNo(); //下载磅房对应车号信息到内存表
            //this.DownLoadFlow();  //下载流向信息
            this.DownLoadProvider();  //下载磅房对应供应单位信息内存表

            printInfoClear();
        }

        /// <summary>
        /// 下拉框初始化
        /// </summary>
        private void ControlerInit()
        {
            GetCHData();
            //GetCH2Data();
            //GetWLData();
            GetLXData();
            //GetFHDWData();
            //GetSHDWData();
            //GetCYDWData();
        }
        #endregion

        #region  网格显示设置
        /// <summary>
        /// 网格显示设置
        /// </summary>
        private void DataGridInit()
        {
            //行编辑器显示序号
            ultraGrid2.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
            ultraGrid2.DisplayLayout.Override.RowSelectorWidth = 25;
            ultraGrid2.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            for (int i = 0; i <= ultraGrid2.DisplayLayout.Bands[0].Columns.Count - 1; i++)
            {
                ultraGrid2.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ultraGrid2.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Equals;
            }
            ultraGrid2.DisplayLayout.Bands[0].Columns["XZ"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;

            //行编辑器显示序号
            ultraGrid3.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
            ultraGrid3.DisplayLayout.Override.RowSelectorWidth = 28;
            ultraGrid3.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            for (int i = 0; i <= ultraGrid3.DisplayLayout.Bands[0].Columns.Count - 1; i++)
            {
                //ultraGrid3.DisplayLayout.Bands[0].Columns[i].AutoCompleteMode = false;
                ultraGrid3.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ultraGrid3.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            }

            //行编辑器显示序号
            ultraGrid5.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
            ultraGrid5.DisplayLayout.Override.RowSelectorWidth = 20;
            ultraGrid5.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            for (int i = 0; i <= ultraGrid4.DisplayLayout.Bands[0].Columns.Count - 1; i++)
            {
                ultraGrid5.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                //ultraGrid5.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Equals;
            }

            if (ultraGrid2.Rows.Count > 0)
            {
                ultraGrid2.ActiveRow = null;
            }
            Constant.RefreshAndAutoSize(ultraGrid2);

            if (ultraGrid3.Rows.Count > 0)
            {
                ultraGrid3.ActiveRow = null;
            }
            Constant.RefreshAndAutoSize(ultraGrid3);


            if (ultraGrid5.Rows.Count > 0)
            {
                ultraGrid5.ActiveRow = null;
            }
            Constant.RefreshAndAutoSize(ultraGrid5);
        }
        #endregion

        #region 下拉框绑定
        /// <summary>
        /// 从数据库获取车号数据
        /// </summary>
        private void GetCHData()
        {
            DataTable dtCH = new DataTable();
            try
            {
                string strSql = "select DISTINCT A.FS_CARNO,A.FN_TIMES FROM BT_POINTCARNO A WHERE A.FS_POINTNO = '" + strJLDID + "' ORDER BY A.FN_TIMES DESC";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "QueryCHData";
                ccp.ServerParams = new object[] { strSql };
                ccp.SourceDataTable = dtCH;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            catch (System.Exception error)
            {
                MessageBox.Show("从数据库获取车号数据  " + error.Message);
            }

            try
            {
                if (dtCH.Rows.Count > 0)
                {
                    DataRow dr = dtCH.NewRow();

                    dr[0] = "";
                    dtCH.Rows.InsertAt(dr, 0);

                    //删除相同的记录
                    DataView dtCarNo = dtCH.DefaultView;
                    dtCH = dtCarNo.ToTable(true, "FS_CARNO");

                    cbCH.DataSource = dtCH;
                    cbCH.DisplayMember = "FS_CARNO";

                    cbCH.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                    cbCH.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (System.Exception error1)
            {
                MessageBox.Show("从数据库获取车号数据后车号绑定  " + error1.Message);
            }

        }
        /// <summary>
        /// 从数据库获取车号2数据
        /// </summary>
        private void GetCH2Data()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryCHData";

            System.Data.DataTable dtCH2 = new System.Data.DataTable();

            ccp.SourceDataTable = dtCH2;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtCH2.Rows.Count > 0)
            {
                for (int i = 0; i <= dtCH2.Rows.Count - 1; i++)
                {
                    string CH = dtCH2.Rows[i]["FS_CARNO"].ToString().Trim();
                    string CH2 = CH.Substring(1);
                    dtCH2.Rows[i]["FS_CARNO"] = CH2;
                    dtCH2.AcceptChanges();
                }
                DataRow dr = dtCH2.NewRow();
                dtCH2.Rows.InsertAt(dr, 0);

                //删除相同的记录
                DataView dtCarNo = dtCH2.DefaultView;
                dtCH2 = dtCarNo.ToTable(true, "FS_CARNO");

                cbCH1.DataSource = dtCH2;
                cbCH1.DisplayMember = "FS_CARNO";

            }
        }
        /// <summary>
        /// 从数据库获取物料数据
        /// </summary>
        private void GetWLData()
        {
            string strBFID = strJLDID;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryWLData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtWL = new System.Data.DataTable();

            ccp.SourceDataTable = dtWL;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtWL.Rows.Count > 0)
            {
                DataRow dr = dtWL.NewRow();
                dtWL.Rows.InsertAt(dr, 0);

                cbWLMC.DataSource = dtWL;
                cbWLMC.DisplayMember = "FS_MATERIALNAME";
                cbWLMC.ValueMember = "FS_MATERIALNO";

                cbWLMC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                cbWLMC.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbWLMC.DataSource = dtWL;
            }
        }
        /// <summary>
        /// 从数据库获取流向数据
        /// </summary>
        private void GetLXData()
        {
            //string strBFID = strJLDID;
            string strBFID = "K02";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryLXData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtLX = new System.Data.DataTable();

            ccp.SourceDataTable = dtLX;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtLX.Rows.Count > 0)
            {
                DataRow dr = dtLX.NewRow();
                dtLX.Rows.InsertAt(dr, 0);

                cbLX.DataSource = dtLX;
                cbLX.DisplayMember = "FS_TYPENAME";
                cbLX.ValueMember = "FS_FLOW";

                //cbLX.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                //cbLX.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbLX.DataSource = dtLX;
            }
        }
        /// <summary>
        /// 从数据库获取发货单位数据
        /// </summary>
        private void GetFHDWData()
        {
            string strBFID = strJLDID;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryFHDWData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtFHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtFHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtFHDW.Rows.Count > 0)
            {
                DataRow dr = dtFHDW.NewRow();
                dtFHDW.Rows.InsertAt(dr, 0);

                cbFHDW.DataSource = dtFHDW;
                cbFHDW.DisplayMember = "FS_SUPPLIERNAME";
                cbFHDW.ValueMember = "FS_SUPPLIER";

                cbFHDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                cbFHDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbFHDW.DataSource = dtFHDW;
            }
        }
        /// <summary>
        /// 从数据库获取收货单位数据
        /// </summary>
        private void GetSHDWData()
        {
            string strBFID = strJLDID;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QuerySHDWData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtSHDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtSHDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtSHDW.Rows.Count > 0)
            {
                DataRow dr = dtSHDW.NewRow();
                dtSHDW.Rows.InsertAt(dr, 0);

                cbSHDW.DataSource = dtSHDW;
                cbSHDW.DisplayMember = "FS_MEMO";
                cbSHDW.ValueMember = "FS_RECEIVER";

                cbSHDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                cbSHDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbSHDW.DataSource = dtSHDW;
            }
        }
        /// <summary>
        /// 从数据库获取承运单位数据
        /// </summary>
        private void GetCYDWData()
        {
            string strBFID = strJLDID;
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.PredictInfo";
            ccp.MethodName = "QueryCYDWData";
            ccp.ServerParams = new object[] { strBFID };

            System.Data.DataTable dtCYDW = new System.Data.DataTable();

            ccp.SourceDataTable = dtCYDW;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtCYDW.Rows.Count > 0)
            {
                DataRow dr = dtCYDW.NewRow();
                dtCYDW.Rows.InsertAt(dr, 0);

                cbCYDW.DataSource = dtCYDW;
                cbCYDW.DisplayMember = "FS_TRANSNAME";
                cbCYDW.ValueMember = "FS_TRANSNO";

                cbCYDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                cbCYDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            else
            {
                cbCYDW.DataSource = dtCYDW;
            }
        }

        private void GetBaseData()
        {
            m_BaseInfoArray = new BaseData[m_nPointCount];

            int i = 0;
            for (i = 0; i < m_nPointCount; i++)
            {
                //物料表
                CoreClientParam ccpWL = new CoreClientParam();
                ccpWL.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccpWL.MethodName = "QueryWLData";
                ccpWL.ServerParams = new object[] { m_PoundRoomArray[i].POINTID };

                System.Data.DataTable dtWLB = new System.Data.DataTable();

                ccpWL.SourceDataTable = dtWLB;
                this.ExecuteQueryToDataTable(ccpWL, CoreInvokeType.Internal);
                //物料下拉列表绑定赋个空值
                if (dtWLB.Rows.Count > 0)
                {
                    DataRow dr = dtWLB.NewRow();
                    dtWLB.Rows.InsertAt(dr, 0);
                }

                //发货单位表
                CoreClientParam ccpFHDW = new CoreClientParam();
                ccpFHDW.ServerName = "ygjzjl.car.PredictInfo";
                ccpFHDW.MethodName = "QueryFHDWData";
                ccpFHDW.ServerParams = new object[] { m_PoundRoomArray[i].POINTID };

                System.Data.DataTable dtFHDWB = new System.Data.DataTable();

                ccpFHDW.SourceDataTable = dtFHDWB;
                this.ExecuteQueryToDataTable(ccpFHDW, CoreInvokeType.Internal);
                //发货单位下拉列表绑定赋个空值
                if (dtFHDWB.Rows.Count > 0)
                {
                    DataRow dr = dtFHDWB.NewRow();
                    dtFHDWB.Rows.InsertAt(dr, 0);
                }

                //收货单位表
                CoreClientParam ccpSHDW = new CoreClientParam();
                ccpSHDW.ServerName = "ygjzjl.car.PredictInfo";
                ccpSHDW.MethodName = "QuerySHDWData";
                ccpSHDW.ServerParams = new object[] { m_PoundRoomArray[i].POINTID };

                System.Data.DataTable dtSHDWB = new System.Data.DataTable();

                ccpSHDW.SourceDataTable = dtSHDWB;
                this.ExecuteQueryToDataTable(ccpSHDW, CoreInvokeType.Internal);
                //收货单位下拉列表绑定赋个空值
                if (dtSHDWB.Rows.Count > 0)
                {
                    DataRow dr = dtSHDWB.NewRow();
                    dtSHDWB.Rows.InsertAt(dr, 0);
                }

                //承运单位表
                CoreClientParam ccpCYDW = new CoreClientParam();
                ccpCYDW.ServerName = "ygjzjl.car.PredictInfo";
                ccpCYDW.MethodName = "QueryCYDWData";
                ccpCYDW.ServerParams = new object[] { m_PoundRoomArray[i].POINTID };

                System.Data.DataTable dtCYDWB = new System.Data.DataTable();

                ccpCYDW.SourceDataTable = dtCYDWB;
                this.ExecuteQueryToDataTable(ccpCYDW, CoreInvokeType.Internal);
                //承运单位下拉列表绑定赋个空值
                if (dtCYDWB.Rows.Count > 0)
                {
                    DataRow dr = dtCYDWB.NewRow();
                    dtCYDWB.Rows.InsertAt(dr, 0);
                }

                m_BaseInfoArray[i] = new BaseData();
                m_BaseInfoArray[i].dtWL = dtWLB.Copy();
                m_BaseInfoArray[i].dtFHDW = dtFHDWB.Copy();
                m_BaseInfoArray[i].dtSHDW = dtSHDWB.Copy();
                m_BaseInfoArray[i].dtCYDW = dtCYDWB.Copy();

            }
        }

        private void BandBaseData()
        {
            //物料下拉列表绑定
            if (m_BaseInfoArray[m_iSelectedPound].dtWL.Rows.Count > 0)
            {
                dataTable7.Rows.Clear();
                //dataTable7 = m_BaseInfoArray[m_iSelectedPound].dtWL.Copy();
                //DataRow drNew = dataTable7.NewRow();

                dataTable7.Merge(m_BaseInfoArray[m_iSelectedPound].dtWL);  //防止每次绑定下拉框时，下拉框跳动（先没有，后出现），在界面上绑定好数据源
                //DataRow newDr;
                //newDr = dataTable7.NewRow();
                //newDr["FS_MATERIALNO"] = "";
                //newDr["FS_MATERIALNAME"] = "";
                //dataTable7.Rows.Add(newDr);
                //foreach (DataRow dr in m_BaseInfoArray[m_iSelectedPound].dtWL.Rows)
                //{
                //    newDr = dataTable7.NewRow();
                //    newDr["FS_MATERIALNO"] = dr["FS_MATERIALNO"].ToString();
                //    newDr["FS_MATERIALNAME"] = dr["FS_MATERIALNAME"].ToString();
                //    dataTable7.Rows.Add(newDr);

                //}
                //dataTable7.AcceptChanges();
                ////cbWLMC.DataSource = m_BaseInfoArray[m_iSelectedPound].dtWL;
                //cbWLMC.DisplayMember = "FS_MATERIALNAME";
                //cbWLMC.ValueMember = "FS_MATERIALNO";

                //cbWLMC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                //cbWLMC.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            //else
            //{
            //    cbWLMC.DataSource = m_BaseInfoArray[m_iSelectedPound].dtWL;
            //}
            //发货单位下拉列表绑定
            if (m_BaseInfoArray[m_iSelectedPound].dtFHDW.Rows.Count > 0)
            {
                dataTable8.Rows.Clear();
                dataTable8.Merge(m_BaseInfoArray[m_iSelectedPound].dtFHDW);

                //cbFHDW.DataSource = m_BaseInfoArray[m_iSelectedPound].dtFHDW;
                //cbFHDW.DisplayMember = "FS_SUPPLIERNAME";
                //cbFHDW.ValueMember = "FS_SUPPLIER";

                //cbFHDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                //cbFHDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            //else
            //{
            //    cbFHDW.DataSource = m_BaseInfoArray[m_iSelectedPound].dtFHDW;
            //}
            //收货单位下拉列表绑定
            if (m_BaseInfoArray[m_iSelectedPound].dtSHDW.Rows.Count > 0)
            {
                dataTable9.Rows.Clear();
                dataTable9.Merge(m_BaseInfoArray[m_iSelectedPound].dtSHDW);

                //cbSHDW.DataSource = m_BaseInfoArray[m_iSelectedPound].dtSHDW;
                //cbSHDW.DisplayMember = "FS_MEMO";
                //cbSHDW.ValueMember = "FS_RECEIVER";

                //cbSHDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                //cbSHDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            //else
            //{
            //    cbSHDW.DataSource = m_BaseInfoArray[m_iSelectedPound].dtSHDW;
            //}
            //承运单位下拉列表绑定
            if (m_BaseInfoArray[m_iSelectedPound].dtCYDW.Rows.Count > 0)
            {
                dataTable10.Rows.Clear();
                dataTable10.Merge(m_BaseInfoArray[m_iSelectedPound].dtCYDW);

                //cbCYDW.DataSource = m_BaseInfoArray[m_iSelectedPound].dtCYDW;
                //cbCYDW.DisplayMember = "FS_TRANSNAME";
                //cbCYDW.ValueMember = "FS_TRANSNO";

                //cbCYDW.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                //cbCYDW.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            //else
            //{
            //    cbCYDW.DataSource = m_BaseInfoArray[m_iSelectedPound].dtCYDW;
            //}
        }
        #endregion

        #region Toolbar事件
        /// <summary>
        /// Toolbar按钮重写事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ToolbarKey"></param>
        public override void ToolBar_Click(object sender, string ToolbarKey)
        {
            base.ToolBar_Click(sender, ToolbarKey);

            switch (ToolbarKey)
            {
                case "Query":
                    this.QueryYBData();
                    break;
                case "Add":
                    this.QueryYCJLData("");
                    break;
                case "Delete":
                    //this.DeleteOldData();
                    break;
                case "Update":
                    //UpdateOldData();
                    break;
            }
        }
        /// <summary>
        /// Toolbar按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "YYDJ":
                    {
                        this.Cursor = Cursors.WaitCursor;
                        HandleTalk(m_iSelectedPound);
                        Thread.Sleep(300);
                        this.Cursor = Cursors.Default;
                        break;
                    }
                case "HZ":
                    {
                        if (DialogResult.Yes == MessageBox.Show("确定要换纸吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            ClearHZ();
                        }
                        break;
                    }
                case "HM":
                    {
                        if (DialogResult.Yes == MessageBox.Show("确定要换墨吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                        {
                            ClearHM();
                        }
                        break;
                    }
                case "QHSP":
                    {
                        VideoSwitch();
                        break;
                    }
                case "YCJLTX":
                    {
                        QueryAndCloseYCPic();
                        break;
                    }
                case "BDDY":
                    {
                        //PrintBDData();
                        if (print.printCS == "0")
                        {
                            Print();
                        }
                        if (print.printCS == "1")
                        {
                            print.printLH = print.printLH1;
                            print.printZS = print.printZS1;
                            Print();
                        }
                        if (print.printCS == "2")
                        {
                            print.printLH = print.printLH2;
                            print.printZS = print.printZS2;
                            Print();
                        }
                        if (print.printCS == "3")
                        {
                            print.printLH = print.printLH3;
                            print.printZS = print.printZS3;
                            Print();
                        }
                        break;
                    }
                case "Query":
                    {
                        if (txtLH.Text.Trim() == "")
                        {
                            MessageBox.Show("请输入炉号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtLH.Focus();
                            break;
                        }
                        ifStart = "1";
                        QueryGPData();
                        cbJLLX.Text = "";
                        cbJLLX.Enabled = false;
                        break;
                    }
                case "btCorrention":
                    {
                        if (strPoint == "")
                        {
                            MessageBox.Show("请双击选择计量点接管信息，接管计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        string term = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
                        correntionWeight = txtXSZL.Text.Trim();
                        correntionWeightNo = Guid.NewGuid().ToString();
                        if (!baseinfo.correntionInformation(correntionWeightNo, strPoint, this.txtJLY.Text.Trim(), this.txtBC.Text.Trim(), term, correntionWeight))
                        {
                            return;
                        }
                        CorrentionSaveImage();
                        MessageBox.Show("校秤完成！！！");
                        break;
                    }
            }
        }
        #endregion

        #region 刷卡查询
        /// <summary>
        /// 自动查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCarNo_TextChanged(object sender, EventArgs e)
        {
            if (txtCarNo.Text.Trim().Length == 0 || txtCZH.Text.Trim().Length == 0)
            {
                return;
            }
            ClearYBData();
            ClearYCBData();

            //if (ifStart == "1")
            //{
            //    //钢坯计量，没预报
            //    return;
            //}
            ifStart = "0"; //如果刷卡则恢复为汽车其它物料计量，取消钢坯秤计量
            if (txtCZH.Text.Trim() == "")
            {
                return;
            }
            if (strJLDID == "")
            {
                MessageBox.Show("请先选择计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCZH.Text.Trim() != "")
            {
                if (!ValidateCarCardData())
                {
                    MessageBox.Show("卡号" + "'" + txtCZH.Text.Trim() + "'" + "还未分配，请联系管理员或相关单位查明原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //ClearData();
            cbJLLX.Enabled = true;
            ClearECJLBData();
            QueryECJLData();
            if (e_REWEIGHTFLAG == "1")
            {
                QueryYCPic();
                QueryPZData();
                DisPlayShow();  //刷显示屏
                return;
            }

            QueryYCJLData("");
            if (strYCJL == "") //如果一次计量标志strYCJL为空，则再查询预报表，为1，说明有一次计量数据
            {
                QueryYBData();
            }
            else
            {
                ybCount[selectRow].strQXPZBZ = "0";
            }

            QueryQXPZData();

            btnDS.Enabled = true;
            panelYCSP.Visible = true;

            if (strYCJL != "" || strQXPZ != "")
            {
                print.pringJLCS = "1";
                QueryYCPic();
            }
            else
                print.pringJLCS = "";

            if (ybCount[selectRow].strQXPZBZ == "1") //目前预报这个标志都是0，这个标志现在是是否需要卸车确认，可以不需要了。
            {
                chbQXPZ.Checked = true;
            }
            else
            {
                //chbQXPZ.Enabled = false;
                chbQXPZ.Checked = false;
            }
            QueryPZData();
            strDS = "0";
            btnGPBC.Visible = false;
            btnBC.Visible = true;

            DisPlayShow();  //刷显示屏
        }
        /// <summary>
        /// 手动查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCZH_Leave(object sender, EventArgs e)
        {
           
            if (txtCZH.Text.Trim().Length == 0)
            {
                return;
            }
            ClearYBData();
            ClearYCBData();
            ClearQXPZData();
            if (ifStart == "1")
            {
                //钢坯计量，没预报
                return;
            }
          

            if (strJLDID == "")
            {
                MessageBox.Show("请先选择计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCZH.Text.Trim() != "")
            {
                if (!ValidateCarCardData())
                {
                    MessageBox.Show("卡号" + "'" + txtCZH.Text.Trim() + "'" + "还未分配，请联系管理员或相关单位查明原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            ClearData();
            cbJLLX.Enabled = true;
            ClearECJLBData();
            QueryECJLData();
            if (e_REWEIGHTFLAG == "1")
            {
                QueryYCPic();
                QueryPZData();
                DisPlayShow();  //刷显示屏
                return;
            }

            QueryYCJLData("");
            if (strYCJL == "")
            {
                QueryYBData();
            }
            else
            {
                ybCount[selectRow].strQXPZBZ = "0";
            }

            //add by luobin
            QueryQXPZData();

            btnDS.Enabled = true;
            panelYCSP.Visible = true;
            if (strYCJL != "" || strQXPZ != "")
            {
                print.pringJLCS = "1";
                QueryYCPic();
            }
            else
                print.pringJLCS = "";

            if (ybCount[selectRow].strQXPZBZ == "1")
            {
                chbQXPZ.Checked = true;
            }
            else
            {
                chbQXPZ.Checked = false;
            }
            QueryPZData();
            strDS = "0";
            btnGPBC.Visible = false;
            btnBC.Visible = true;

            DisPlayShow();  
            btnBC.Enabled = true;
            //if (this.cbLX.Text.ToString() == "进厂")
            //{ 
            //    if (!QueryPD(this.txtCZH.Text.Trim()))
            //    {
            //        btnBC.Enabled = false;
            //        return;
            //    }
            //} 
        }

        private void cbCH1_Leave(object sender, EventArgs e)
        {
            //if (ifStart == "1")
            //{
            //    //钢坯计量，没预报
            //    return;
            //}
            //if (strYCJL == "" && strYB == "1")
            //{
            //    return;
            //}
            ClearYBData();
            ClearYCBData();
            ClearQXPZData();
            if (cbCH1.Text.Trim() == "")
            {
                return;
            }

            if (strJLDID == "")
            {
                MessageBox.Show("请先选择计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbCH.Text.Trim() == "")
            {
                MessageBox.Show("请先选择省简称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cbJLLX.Enabled = true;
            ClearECJLBData();
            QueryECJLData();
            if (e_REWEIGHTFLAG == "1")
            {
                QueryYCPic();
                QueryPZData();
                return;
            }

            QueryYCJLData("");
            if (strYCJL == "")
            {
                QueryYBData();
                QueryQXPZData();
            }
            else
            {
                ybCount[selectRow].strQXPZBZ = "0";
            }



            btnDS.Enabled = true;
            panelYCSP.Visible = true;
            if (strYCJL != "" || strQXPZ != "")
            {
                print.pringJLCS = "1";
                QueryYCPic();
            }
            else
                print.pringJLCS = "";

            if (ybCount[selectRow].strQXPZBZ == "1")
            {
                chbQXPZ.Checked = true;
            }
            else
            {
                chbQXPZ.Checked = false;
            }
            QueryPZData();
            strDS = "0";

            //钢坯保存按钮与保存按钮显示哪个
            if (txtLH.Text.Trim() != "")
            {
                btnGPBC.Visible = true;
                btnBC.Visible = false;
            }
            else
            {
                btnGPBC.Visible = false;
                btnBC.Visible = true;
            }

            DisPlayShow();  //刷显示屏
        }

        /// <summary>
        /// 切换计量点查询，不用，这样会影响切换（如果在保存时马上就切换）
        /// </summary>
        private void SwitchPoundQuery()
        {
            if (txtCZH.Text.Trim().Length == 0)
            {
                return;
            }

            if (strJLDID == "")
            {
                MessageBox.Show("请先选择计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtCZH.Text.Trim() != "")
            {
                if (!ValidateCarCardData())
                {
                    MessageBox.Show("卡号" + "'" + txtCZH.Text.Trim() + "'" + "还未分配，请联系管理员或相关单位查明原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            QueryYCJLData("");
            if (strYCJL == "")
            {
                QueryYBData();
            }
            else
            {
                ybCount[selectRow].strQXPZBZ = "0";
            }
            btnDS.Enabled = true;
            panelYCSP.Visible = true;
            if (strYCJL != "")
            {
                QueryYCPic();
            }
            if (ybCount[selectRow].strQXPZBZ == "1")
            {
                chbQXPZ.Checked = true;
            }
            else
            {
                //chbQXPZ.Enabled = false;
                chbQXPZ.Checked = false;
            }
            QueryPZData();
            strDS = "0";

            DisPlayShow();  //刷显示屏
        }

        /// <summary>
        /// 车证卡验证
        /// </summary>
        private bool ValidateCarCardData()
        {
            string sql = "SELECT A.FS_CARDLEVEL,A.FS_ISVALID FROM BT_CARDMANAGE A WHERE A.FS_SEQUENCENO = '" + txtCZH.Text.Trim() + "'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "ValidateCarCardData";
            ccp.ServerParams = new object[] { sql };
            DataTable dtCarNo = new DataTable();
            ccp.SourceDataTable = dtCarNo;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            string strBZ = "";
            if (dtCarNo.Rows.Count > 0)
            {
                strBZ = dtCarNo.Rows[0]["FS_ISVALID"].ToString();
            }
            if (strBZ == "2" || strBZ == "4")
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 历史皮重比较报警
        /// </summary>
        private void QueryPZData()
        {
            string xszl = txtXSZL.Text.Trim();
            string lszl = "";
            //查询历史皮重
            string sql = "select a.fn_average from dt_tareweighthistory a where a.fs_carno = '" + cbCH.Text.Trim() + cbCH1.Text.Trim() + "'";
            CoreClientParam ccpPZ = new CoreClientParam();
            ccpPZ.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpPZ.MethodName = "QueryPZData";
            ccpPZ.ServerParams = new object[] { sql };
            DataTable dtLSPZ = new DataTable();
            ccpPZ.SourceDataTable = dtLSPZ;

            this.ExecuteQueryToDataTable(ccpPZ, CoreInvokeType.Internal);
            if (dtLSPZ.Rows.Count > 0)
            {
                lszl = dtLSPZ.Rows[0][0].ToString();
            }

            string bjfwMIN = "";
            string bjfwMAX = "";
            //查询报警允许最小、最大范围值
            string sqlBJ = "select a.FN_WEIGHTDEVMIN,a.FN_WEIGHTDEVMAX from DT_ALERTCONDITION a";
            CoreClientParam ccpBJ = new CoreClientParam();
            ccpBJ.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpBJ.MethodName = "QueryBJData";
            ccpBJ.ServerParams = new object[] { sqlBJ };
            DataTable dtBJ = new DataTable();
            ccpBJ.SourceDataTable = dtBJ;

            this.ExecuteQueryToDataTable(ccpBJ, CoreInvokeType.Internal);

            if (dtBJ.Rows.Count > 0)
            {
                bjfwMIN = dtBJ.Rows[0][1].ToString();
                bjfwMAX = dtBJ.Rows[0][0].ToString();
            }
            //显示皮重与历史皮重之差
            if (lszl == "")
            {
                lszl = "0";
            }
            if (bjfwMIN == "")
            {
                bjfwMIN = "0";
            }
            if (bjfwMAX == "")
            {
                bjfwMAX = "0";
            }
            //皮重与历史皮重之差的绝对值大于0.5（最小偏差）并且小于1（最大偏差）时，就报警，皮重与历史皮重之差的绝对值小于0.5时，皮重正常；、
            //皮重与历史皮重之差的绝对值大于1时，即认为是过毛重，或车重不正常。所以这个区间要设置好！
            if (xszl == "")
            {
                xszl = "0";
            }
            decimal dZL = Convert.ToDecimal(xszl) - Convert.ToDecimal(lszl);
            if (dZL >= 0)
            {
                if (Convert.ToDecimal(xszl) - Convert.ToDecimal(lszl) > Convert.ToDecimal(bjfwMIN) && Convert.ToDecimal(xszl) - Convert.ToDecimal(lszl) < Convert.ToDecimal(bjfwMAX))
                {
                    MessageBox.Show("该车皮重已超过历史皮重允许误差！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            if (dZL < 0)
            {
                if (Convert.ToDecimal(lszl) - Convert.ToDecimal(xszl) > Convert.ToDecimal(bjfwMIN) && Convert.ToDecimal(lszl) - Convert.ToDecimal(xszl) < Convert.ToDecimal(bjfwMAX))
                {
                    MessageBox.Show("该车皮重已超过历史皮重允许误差！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        /// <summary>
        /// 查询预报信息
        /// </summary>
        private void QueryYBData()
        {
            string strWhere = "";
            string sWhere = "";

            strWhere = " and B.FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "'";
            sWhere = " and FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "'";
            if (txtCZH.Text.Trim() == "")
            {
                string strCarNo = cbCH.Text.Trim() + cbCH1.Text.Trim();
                strWhere = " and B.FS_CARNO = '" + strCarNo + "'";
                sWhere = " and FS_CARNO = '" + strCarNo + "'";
            }

            //DataTable dtYB = new DataTable();//预报表

            CoreClientParam ccp = new CoreClientParam();
            this.dataTable4.Rows.Clear();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryYBData";
            ccp.ServerParams = new object[] { strWhere, sWhere };
            ccp.SourceDataTable = dataSet1.Tables[3];//dataTable4
            this.dataTable4.Rows.Clear();
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            strYB = "";
            if (dataTable4.Rows.Count > 0)
            {
                ClearData();
                //cbCH1.Text = dataTable4.Rows[0]["FS_CARNO"].ToString();

                string CH = dataTable4.Rows[0]["FS_CARNO"].ToString();
                string CH1 = CH.Substring(0, 2);
                this.cbCH.Text = CH1;
                string CH2 = CH.Substring(2);
                this.cbCH1.Text = CH2;

                txtCZH.Text = dataTable4.Rows[0]["FS_CARDNUMBER"].ToString();
                txtHTH.Text = dataTable4.Rows[0]["FS_CONTRACTNO"].ToString();
                txtHTXMH.Text = dataTable4.Rows[0]["FS_CONTRACTITEM"].ToString();
                txtZS.Text = dataTable4.Rows[0]["FN_BILLETCOUNT"].ToString();
                txtLH.Text = dataTable4.Rows[0]["FS_STOVENO"].ToString();
                cbWLMC.Text = dataTable4.Rows[0]["FS_MATERIALNAME"].ToString();
                cbLX.Text = dataTable4.Rows[0]["FS_LX"].ToString();
                cbCYDW.Text = dataTable4.Rows[0]["FS_CYDW"].ToString();
                cbFHDW.Text = dataTable4.Rows[0]["FS_FHDW"].ToString();
                cbSHDW.Text = dataTable4.Rows[0]["FS_SHDW"].ToString();

                //add by luobin
                tbSenderPlace.Text = dataTable4.Rows[0]["FS_SENDERSTORE"].ToString();
                tbReceiverPlace.Text = dataTable4.Rows[0]["FS_RECEIVERSTORE"].ToString();
                cbProvider.Text = dataTable4.Rows[0]["FS_PROVIDER"].ToString();
                tbBZ.Text = dataTable4.Rows[0]["FS_DRIVERREMARK"].ToString();



                strYB = "1";

                ybCount[selectRow].strYBH = dataTable4.Rows[0]["FS_PLANCODE"].ToString().Trim();
                ybCount[selectRow].strHTH = txtHTH.Text.Trim();
                ybCount[selectRow].strHTXMH = txtHTXMH.Text.Trim();
                ybCount[selectRow].strLH = txtLH.Text.Trim();
                ybCount[selectRow].strZS = txtZS.Text.Trim();
                ybCount[selectRow].strCZH = txtCZH.Text.Trim();
                ybCount[selectRow].strCH = dataTable4.Rows[0]["FS_CARNO"].ToString().Trim();
                ybCount[selectRow].strWLID = dataTable4.Rows[0]["FS_MATERIAL"].ToString().Trim();
                ybCount[selectRow].strWLMC = cbWLMC.Text.Trim();
                ybCount[selectRow].strFHFDM = dataTable4.Rows[0]["FS_SENDER"].ToString().Trim();
                ybCount[selectRow].strFHKCD = dataTable4.Rows[0]["FS_SENDERSTORE"].ToString().Trim();
                ybCount[selectRow].strSHFDM = dataTable4.Rows[0]["FS_RECEIVERFACTORY"].ToString().Trim();
                //ybCount[selectRow].strSHKCD = dataTable4.Rows[0]["FS_LEVEL"].ToString().Trim();
                ybCount[selectRow].strSHKCD = dataTable4.Rows[0]["FS_RECEIVERSTORE"].ToString().Trim();
                ybCount[selectRow].strLX = dataTable4.Rows[0]["FS_WEIGHTTYPE"].ToString().Trim();
                ybCount[selectRow].strCYDW = dataTable4.Rows[0]["FS_TRANSNO"].ToString().Trim();
                ybCount[selectRow].strBF = dataTable4.Rows[0]["FS_POUNDTYPE"].ToString().Trim();
                ybCount[selectRow].strYBZZ = dataTable4.Rows[0]["FN_SENDGROSSWEIGHT"].ToString().Trim();
                ybCount[selectRow].strYBPZ = dataTable4.Rows[0]["FN_SENDTAREWEIGHT"].ToString().Trim();
                ybCount[selectRow].strYBJZ = dataTable4.Rows[0]["FN_SENDNETWEIGHT"].ToString().Trim();
                ybCount[selectRow].strHQLX = dataTable4.Rows[0]["FS_POUNDTYPE"].ToString().Trim();
                ybCount[selectRow].strQXPZBZ = dataTable4.Rows[0]["FS_LEVEL"].ToString().Trim();

                lspzCH = dataTable4.Rows[0]["FS_CARNO"].ToString();

                sWLMC = dataTable4.Rows[0]["FS_MATERIALNAME"].ToString();
                sLX = dataTable4.Rows[0]["FS_LX"].ToString();
                sFHDW = dataTable4.Rows[0]["FS_FHDW"].ToString();
                sSHDW = dataTable4.Rows[0]["FS_SHDW"].ToString();
                sCYDW = dataTable4.Rows[0]["FS_CYDW"].ToString();

                y_IFSAMPLING = dataTable4.Rows[0]["FS_IFSAMPLING"].ToString().Trim();
                y_IFACCEPT = dataTable4.Rows[0]["FS_IFACCEPT"].ToString().Trim();
                y_DRIVERNAME = dataTable4.Rows[0]["FS_DRIVERNAME"].ToString().Trim();
                y_DRIVERIDCARD = dataTable4.Rows[0]["FS_DRIVERIDCARD"].ToString().Trim();
            }
        }
        /// <summary>
        /// 查询一次计量表信息
        /// </summary>
        private void QueryYCJLData(string gpCH)
        {
            string strDataState = m_PoundRoomArray[m_iSelectedPound].POINTSTATE;

            string strWhere = " and FS_DATASTATE = '" + strDataState + "' and ( 1=2 ";

            if (txtCZH.Text.Trim() != "")
                strWhere += " or A.FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "'";

            if (cbCH1.Text.Trim() != "" && cbCH.Text.Trim() != "")
                strWhere += " or A.FS_CARNO = '" + cbCH.Text.Trim() + "" + cbCH1.Text.Trim() + "'";

            if (gpCH != "")
                strWhere += " or A.FS_STOVENO = '" + gpCH + "'";

            strWhere += ")";

            //strWhere = " and (A.FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "'";
            //if (txtCZH.Text.Trim() == "")
            //{
            //    strWhere = " and A.FS_CARNO = '" + cbCH.Text.Trim() + "" + cbCH1.Text.Trim() + "'";
            //}
            //if (gpCH != "")//根据钢坯炉号去查钢坯一次计量记录
            //{
            //    strWhere = " and A.FS_STOVENO = '" + gpCH + "'";
            //}



            DataTable dtYCJL = new DataTable();//一次计量表

            CoreClientParam ccp = new CoreClientParam();
            this.dataTable2.Rows.Clear();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryYCJLData";
            ccp.ServerParams = new object[] { strWhere };
            ccp.SourceDataTable = dtYCJL;
            
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            //dataTable2 = dtYCJL.Copy();
            //Constant.RefreshAndAutoSize(ultraGrid3);

            strYCJL = "";
            if (dtYCJL.Rows.Count > 0)
            {
                ClearData();
                //cbCH1.Text = dtYCJL.Rows[0]["FS_CARNO"].ToString();
                string CH = dtYCJL.Rows[0]["FS_CARNO"].ToString();
                string CH1 = CH.Substring(0, 2);
                this.cbCH.Text = CH1;
                string CH2 = CH.Substring(2);
                this.cbCH1.Text = CH2;

                txtCZH.Text = dtYCJL.Rows[0]["FS_CARDNUMBER"].ToString();
                txtHTH.Text = dtYCJL.Rows[0]["FS_CONTRACTNO"].ToString();
                //txtHTXMH.Text = "0001";//原先代码
                txtHTXMH.Text = "";
                if (dtYCJL.Rows[0]["FN_COUNT"].ToString().Trim() != "")
                {
                    txtZS.Text = dtYCJL.Rows[0]["FN_COUNT"].ToString();
                }
                txtLH.Text = dtYCJL.Rows[0]["FS_STOVENO"].ToString();
                cbWLMC.Text = dtYCJL.Rows[0]["FS_MATERIALNAME"].ToString();
                cbLX.Text = dtYCJL.Rows[0]["FS_LX"].ToString();
                cbCYDW.Text = dtYCJL.Rows[0]["FS_CYDW"].ToString();
                cbFHDW.Text = dtYCJL.Rows[0]["FS_FHDW"].ToString();
                //add by luobin
                tbSenderPlace.Text = dtYCJL.Rows[0]["FS_SENDERSTORE"].ToString();
                tbReceiverPlace.Text = dtYCJL.Rows[0]["FS_RECEIVERSTORE"].ToString();
                cbProvider.Text = dtYCJL.Rows[0]["FS_PROVIDER"].ToString();
                tbBZ.Text = dtYCJL.Rows[0]["FS_BZ"].ToString();

                cbSHDW.Text = dtYCJL.Rows[0]["FS_SHDW"].ToString();
                txtYKL.Text = dtYCJL.Rows[0]["FS_YKL"].ToString();
                txtPJBH.Text = dtYCJL.Rows[0]["FS_BILLNUMBER"].ToString();

                s_YKBL = dtYCJL.Rows[0]["FS_YKBL"].ToString();
                if (s_YKBL == "0")
                    s_YKBL = "";

                strYCJL = "1";
                strZYBH = dtYCJL.Rows[0]["FS_WEIGHTNO"].ToString();

                stYBH = dtYCJL.Rows[0]["FS_PLANCODE"].ToString();

                if (dtYCJL.Rows[0]["FS_CONTRACTNO"].ToString().Trim() != "")
                    stHTH = dtYCJL.Rows[0]["FS_CONTRACTNO"].ToString();

                stHTXMH = "0001";
                stLH = dtYCJL.Rows[0]["FS_STOVENO"].ToString();
                if (dtYCJL.Rows[0]["FN_COUNT"].ToString().Trim() != "")
                {
                    stZS = dtYCJL.Rows[0]["FN_COUNT"].ToString();
                }
                stCZH = dtYCJL.Rows[0]["FS_CARDNUMBER"].ToString();
                stCH = dtYCJL.Rows[0]["FS_CARNO"].ToString();
                stWLID = dtYCJL.Rows[0]["FS_MATERIAL"].ToString();
                stWLMC = dtYCJL.Rows[0]["FS_MATERIALNAME"].ToString();
                stFHFDM = dtYCJL.Rows[0]["FS_SENDER"].ToString();
                stSHFDM = dtYCJL.Rows[0]["FS_RECEIVER"].ToString();
                stSHKCD = ""; //是否需要卸货确认
                stSendStore = dtYCJL.Rows[0]["FS_SENDERSTORE"].ToString();
                stReceiverSotre = dtYCJL.Rows[0]["FS_RECEIVERSTORE"].ToString();

                stCYDW = dtYCJL.Rows[0]["FS_TRANSNO"].ToString();
                stYBZZ = dtYCJL.Rows[0]["FN_SENDGROSSWEIGHT"].ToString();
                stLX = dtYCJL.Rows[0]["FS_WEIGHTTYPE"].ToString();
                stYBPZ = dtYCJL.Rows[0]["FN_SENDTAREWEIGHT"].ToString();
                stYBJZ = dtYCJL.Rows[0]["FN_SENDNETWEIGHT"].ToString();

                strYCZL = dtYCJL.Rows[0]["FN_WEIGHT"].ToString();
                strYCJLD = dtYCJL.Rows[0]["FS_POUND"].ToString();
                strYCJLY = dtYCJL.Rows[0]["FS_WEIGHTER"].ToString();
                strYCJLSJ = dtYCJL.Rows[0]["FD_WEIGHTTIME"].ToString();
                strYCJLBC = dtYCJL.Rows[0]["FS_SHIFT"].ToString();

                strYCBDTM = dtYCJL.Rows[0]["FS_FIRSTLABELID"].ToString();

                strXCRKSJ = dtYCJL.Rows[0]["FD_UNLOADINSTORETIME"].ToString();
                strXCCKSJ = dtYCJL.Rows[0]["FD_UNLOADOUTSTORETIME"].ToString();
                strXCQR = dtYCJL.Rows[0]["FS_UNLOADFLAG"].ToString();
                strXCKGY = dtYCJL.Rows[0]["FS_UNLOADSTOREPERSON"].ToString();
                strZCRKSJ = dtYCJL.Rows[0]["FD_LOADINSTORETIME"].ToString();
                strZCCKSJ = dtYCJL.Rows[0]["FD_LOADOUTSTORETIME"].ToString();
                strZCQR = dtYCJL.Rows[0]["FS_LOADFLAG"].ToString();
                strZCKGY = dtYCJL.Rows[0]["FS_LOADSTOREPERSON"].ToString();
                strQYY = dtYCJL.Rows[0]["FS_SAMPLEPERSON"].ToString();
                strYCSFYC = dtYCJL.Rows[0]["FS_YCSFYC"].ToString();

                strBFBH = dtYCJL.Rows[0]["FS_POUNDTYPE"].ToString();

                lspzCH = dtYCJL.Rows[0]["FS_CARNO"].ToString();

                sWLMC = dtYCJL.Rows[0]["FS_MATERIALNAME"].ToString();
                sLX = dtYCJL.Rows[0]["FS_LX"].ToString();
                sFHDW = dtYCJL.Rows[0]["FS_FHDW"].ToString();
                sSHDW = dtYCJL.Rows[0]["FS_SHDW"].ToString();
                sCYDW = dtYCJL.Rows[0]["FS_CYDW"].ToString();

                s_SAMPLETIME = dtYCJL.Rows[0]["FD_SAMPLETIME"].ToString();
                s_SAMPLEPLACE = dtYCJL.Rows[0]["FS_SAMPLEPLACE"].ToString();
                s_SAMPLEFLAG = dtYCJL.Rows[0]["FS_SAMPLEFLAG"].ToString();
                s_UNLOADPERSON = dtYCJL.Rows[0]["FS_UNLOADPERSON"].ToString();
                s_UNLOADTIME = dtYCJL.Rows[0]["FD_UNLOADTIME"].ToString();
                s_UNLOADPLACE = dtYCJL.Rows[0]["FS_UNLOADPLACE"].ToString();
                s_CHECKPERSON = dtYCJL.Rows[0]["FS_CHECKPERSON"].ToString();
                s_CHECKTIME = dtYCJL.Rows[0]["FD_CHECKTIME"].ToString();
                s_CHECKPLACE = dtYCJL.Rows[0]["FS_CHECKPLACE"].ToString();
                s_CHECKFLAG = dtYCJL.Rows[0]["FS_CHECKFLAG"].ToString();
                s_IFSAMPLING = dtYCJL.Rows[0]["FS_IFSAMPLING"].ToString();
                s_IFACCEPT = dtYCJL.Rows[0]["FS_IFACCEPT"].ToString();
                s_DRIVERNAME = dtYCJL.Rows[0]["FS_DRIVERNAME"].ToString();
                s_DRIVERIDCARD = dtYCJL.Rows[0]["FS_DRIVERIDCARD"].ToString();
                s_SENDERSTORE = dtYCJL.Rows[0]["FS_SENDERSTORE"].ToString();
                s_REWEIGHTFLAG = dtYCJL.Rows[0]["FS_REWEIGHTFLAG"].ToString();
                s_REWEIGHTTIME = dtYCJL.Rows[0]["FD_REWEIGHTTIME"].ToString();
                s_REWEIGHTPLACE = dtYCJL.Rows[0]["FS_REWEIGHTPLACE"].ToString();
                s_REWEIGHTPERSON = dtYCJL.Rows[0]["FS_REWEIGHTPERSON"].ToString();
                s_BILLNUMBER = dtYCJL.Rows[0]["FS_BILLNUMBER"].ToString();
                s_DFJZ = dtYCJL.Rows[0]["FS_DFJZ"].ToString();

                //add by luobin
                txtDFJZ.Text = dtYCJL.Rows[0]["FS_DFJZ"].ToString();

                if (s_REWEIGHTFLAG == "1")
                {
                    cbJLLX.Text = "复磅";
                    cbJLLX.Enabled = false;
                }

                if (strXCQR == "0")
                {
                    cbJLLX.Text = "退货过磅";
                    cbJLLX.Enabled = false;
                }

                if (stLH != "")
                {
                    ifStart = "1";
                }

                if (s_REWEIGHTFLAG != "1")
                {
                    if (Convert.ToDecimal(strYCZL) >= Convert.ToDecimal(txtXSZL.Text.Trim()))
                    {
                        txtMZ.Text = strYCZL;
                        txtPZ.Text = txtXSZL.Text.Trim();
                        txtJZ.Text = (Convert.ToDecimal(strYCZL) - Convert.ToDecimal(txtXSZL.Text.Trim())).ToString().Trim();
                    }
                    if (Convert.ToDecimal(strYCZL) < Convert.ToDecimal(txtXSZL.Text.Trim()))
                    {
                        txtPZ.Text = strYCZL;
                        txtMZ.Text = txtXSZL.Text.Trim();
                        txtJZ.Text = (Convert.ToDecimal(txtXSZL.Text.Trim()) - Convert.ToDecimal(strYCZL)).ToString().Trim();
                    }
                }
            }
            //钢坯按车号自动查询一次
            if (stLH != "" && querycs == 0)
            {
                ClearGPData();
                querycs = 1;
                ifStart = "1";
                QueryGPData();
                cbJLLX.Text = "";
                cbJLLX.Enabled = false;
                btnGPBC.Visible = true;
                btnBC.Visible = false;
                QueryGPDataOne();
            }
            //获取服务端查询出来的单个值
            //string a = ((ccp.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_PRINTTYPECODE"].ToString();
            //float b = Convert.ToSingle((((ccp.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FN_PAPERNUM"] as System.Collections.Hashtable)["value"]);
        }

        /// <summary>
        /// 查询一次计量绑定数据
        /// </summary>
        private void QueryYCBData()
        {
            string strWhere = "";

            CoreClientParam ccp = new CoreClientParam();
            this.dataTable11.Rows.Clear();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryYCJLData";
            ccp.ServerParams = new object[] { strWhere };
            //DataTable dtYCJLB = new DataTable();
            ccp.SourceDataTable = dataTable11;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            //if (dtYCJLB.Rows.Count > 0)
            //{
            //    ultraGrid3.DataSource = dtYCJLB;
            //}
            Constant.RefreshAndAutoSize(ultraGrid3);

            foreach (UltraGridRow ugr in ultraGrid3.Rows)
            {
                if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                {
                    ugr.Appearance.ForeColor = Color.Red;
                }
            }

        }

        /// <summary>
        /// 查询二次计量数据
        /// </summary>
        private void QueryECJLData()
        {
            string strDataState = m_PoundRoomArray[m_iSelectedPound].POINTSTATE;

            string strWhere = " and A.FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "' and A.FS_DATASTATE = '" + strDataState + "'";
            string sWhere = " and B.FS_CARDNUMBER = '" + txtCZH.Text.Trim() + "' and A.FS_DATASTATE = '" + strDataState + "'";

            if (cbCH1.Text.Trim() != "" && txtCZH.Text.Trim() == "")
            {
                strWhere = " and A.FS_CARNO = '" + cbCH.Text.Trim() + cbCH1.Text.Trim() + "'";
                sWhere = " and B.FS_CARNO = '" + cbCH.Text.Trim() + cbCH1.Text.Trim() + "'";
            }
            string strSql = "SELECT A.FS_WEIGHTNO,A.FS_CONTRACTNO,A.FS_CONTRACTITEM,A.FS_STOVENO,A.FN_COUNT,A.FS_CARDNUMBER,A.FS_CARNO,A.FS_MATERIAL,WL_BHTOMC(A.FS_MATERIAL)AS FS_MATERIALNAME,";
            strSql += "A.FS_SENDER,FHDW_BHTOMC(A.FS_SENDER)FHDW,Provider_BHTOMC(A.FS_Provider)FS_Provider,A.FS_BZ,A.FS_TRANSNO,CYDW_BHTOMC(A.FS_TRANSNO)CYDW,A.FS_RECEIVER,SHDW_BHTOMC(A.FS_RECEIVER)SHDW,A.FN_SENDGROSSWEIGHT,A.FN_SENDTAREWEIGHT,";
            strSql += "A.FN_SENDNETWEIGHT,A.FS_WEIGHTTYPE,LX_BHTOMC(A.FS_WEIGHTTYPE)LX,A.FN_GROSSWEIGHT,A.FS_GROSSPOINT,A.FS_GROSSPERSON,A.FD_GROSSDATETIME,A.FS_GROSSSHIFT,A.FN_TAREWEIGHT,A.FS_TAREPOINT,";
            strSql += "A.FS_TAREPERSON,A.FD_TAREDATETIME,A.FS_TARESHIFT,A.FS_FIRSTLABELID,A.FS_FULLLABELID,A.FN_NETWEIGHT,A.FS_SAMPLEPERSON,A.FS_YKL,A.FD_SAMPLETIME,A.FS_SAMPLEPLACE,A.FS_SAMPLEFLAG,";
            strSql += "A.FS_DRIVERNAME,A.FS_DRIVERIDCARD,A.FS_SENDERSTORE,A.FS_RECEIVERSTORE,A.FS_IFSAMPLING,A.FS_IFACCEPT,A.FS_IFUNLOAD,A.FS_REWEIGHTFLAG,A.FD_REWEIGHTTIME,A.FS_REWEIGHTPLACE,A.FS_REWEIGHTPERSON,A.FS_BILLNUMBER, ";
            strSql += "A.FS_UNLOADFLAG FROM DT_CARWEIGHT_WEIGHT A WHERE A.FD_GROSSDATETIME = (SELECT MAX(B.FD_GROSSDATETIME) FROM DT_CARWEIGHT_WEIGHT B WHERE 1=1" + sWhere + ")";
            strSql += strWhere;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryData";
            DataTable dtECJL = new DataTable();
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dtECJL;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dtECJL.Rows.Count > 0)
            {
                e_REWEIGHTFLAG = dtECJL.Rows[0]["FS_REWEIGHTFLAG"].ToString().Trim();

                //if (e_REWEIGHTFLAG != "1")
                //{
                //    pictureBox18.Image = BitmapToImage(new byte[1]);
                //    pictureBox18.Refresh();
                //    panel20.Visible = false;
                //    panel22.BringToFront();
                //    return;
                //}

                e_WEIGHTNO = dtECJL.Rows[0]["FS_WEIGHTNO"].ToString().Trim();
                e_CONTRACTNO = dtECJL.Rows[0]["FS_CONTRACTNO"].ToString().Trim();
                e_CONTRACTITEM = dtECJL.Rows[0]["FS_CONTRACTITEM"].ToString().Trim();
                e_STOVENO = dtECJL.Rows[0]["FS_STOVENO"].ToString().Trim();
                e_COUNT = dtECJL.Rows[0]["FN_COUNT"].ToString().Trim();
                e_CARDNUMBER = dtECJL.Rows[0]["FS_CARDNUMBER"].ToString().Trim();
                e_CARNO = dtECJL.Rows[0]["FS_CARNO"].ToString().Trim();
                e_MATERIAL = dtECJL.Rows[0]["FS_MATERIAL"].ToString().Trim();
                e_MATERIALNAME = dtECJL.Rows[0]["FS_MATERIALNAME"].ToString().Trim();
                e_SENDER = dtECJL.Rows[0]["FS_SENDER"].ToString().Trim();
                e_TRANSNO = dtECJL.Rows[0]["FS_TRANSNO"].ToString().Trim();
                e_RECEIVER = dtECJL.Rows[0]["FS_RECEIVER"].ToString().Trim();
                //add by luobin 
                e_SENDERSTORE = dtECJL.Rows[0]["FS_SENDERSTORE"].ToString();
                e_RECEIVERSTORE = dtECJL.Rows[0]["FS_RECEIVERSTORE"].ToString();
                e_SENDGROSSWEIGHT = dtECJL.Rows[0]["FN_SENDGROSSWEIGHT"].ToString().Trim();
                e_SENDTAREWEIGHT = dtECJL.Rows[0]["FN_SENDTAREWEIGHT"].ToString().Trim();
                e_SENDNETWEIGHT = dtECJL.Rows[0]["FN_SENDNETWEIGHT"].ToString().Trim();
                e_WEIGHTTYPE = dtECJL.Rows[0]["FS_WEIGHTTYPE"].ToString().Trim();
                e_GROSSWEIGHT = dtECJL.Rows[0]["FN_GROSSWEIGHT"].ToString().Trim();
                e_GROSSPOINT = dtECJL.Rows[0]["FS_GROSSPOINT"].ToString().Trim();
                e_GROSSPERSON = dtECJL.Rows[0]["FS_GROSSPERSON"].ToString().Trim();
                e_GROSSDATETIME = dtECJL.Rows[0]["FD_GROSSDATETIME"].ToString().Trim();
                e_GROSSSHIFT = dtECJL.Rows[0]["FS_GROSSSHIFT"].ToString().Trim();
                e_TAREWEIGHT = dtECJL.Rows[0]["FN_TAREWEIGHT"].ToString().Trim();
                e_TAREPOINT = dtECJL.Rows[0]["FS_TAREPOINT"].ToString().Trim();
                e_TAREPERSON = dtECJL.Rows[0]["FS_TAREPERSON"].ToString().Trim();
                e_TAREDATETIME = dtECJL.Rows[0]["FD_TAREDATETIME"].ToString().Trim();
                e_TARESHIFT = dtECJL.Rows[0]["FS_TARESHIFT"].ToString().Trim();
                e_FIRSTLABELID = dtECJL.Rows[0]["FS_FIRSTLABELID"].ToString().Trim();
                e_FULLLABELID = dtECJL.Rows[0]["FS_FULLLABELID"].ToString().Trim();
                e_NETWEIGHT = dtECJL.Rows[0]["FN_NETWEIGHT"].ToString().Trim();
                e_SAMPLEPERSON = dtECJL.Rows[0]["FS_SAMPLEPERSON"].ToString().Trim();
                e_YKL = dtECJL.Rows[0]["FS_YKL"].ToString().Trim();
                e_SAMPLETIME = dtECJL.Rows[0]["FD_SAMPLETIME"].ToString().Trim();
                e_SAMPLEPLACE = dtECJL.Rows[0]["FS_SAMPLEPLACE"].ToString().Trim();
                e_SAMPLEFLAG = dtECJL.Rows[0]["FS_SAMPLEFLAG"].ToString().Trim();
                e_DRIVERNAME = dtECJL.Rows[0]["FS_DRIVERNAME"].ToString().Trim();
                e_DRIVERIDCARD = dtECJL.Rows[0]["FS_DRIVERIDCARD"].ToString().Trim();
                e_SENDERSTORE = dtECJL.Rows[0]["FS_SENDERSTORE"].ToString().Trim();
                e_IFSAMPLING = dtECJL.Rows[0]["FS_IFSAMPLING"].ToString().Trim();
                e_IFACCEPT = dtECJL.Rows[0]["FS_IFACCEPT"].ToString().Trim();
                e_IFUNLOAD = dtECJL.Rows[0]["FS_IFUNLOAD"].ToString().Trim();
                e_REWEIGHTTIME = dtECJL.Rows[0]["FD_REWEIGHTTIME"].ToString().Trim();
                e_REWEIGHTPLACE = dtECJL.Rows[0]["FS_REWEIGHTPLACE"].ToString().Trim();
                e_REWEIGHTPERSON = dtECJL.Rows[0]["FS_REWEIGHTPERSON"].ToString().Trim();

                e_UNLOADFLAG = dtECJL.Rows[0]["FS_UNLOADFLAG"].ToString().Trim();

                if (e_REWEIGHTFLAG == "1")
                {
                    strECJL = "1";

                    txtCZH.Text = e_CARDNUMBER;
                    cbCH.Text = e_CARNO.Substring(0, 2);
                    cbCH1.Text = e_CARNO.Substring(2);
                    cbWLMC.Text = e_MATERIALNAME;
                    cbLX.Text = dtECJL.Rows[0]["LX"].ToString().Trim();
                    cbCYDW.Text = dtECJL.Rows[0]["CYDW"].ToString().Trim();
                    cbFHDW.Text = dtECJL.Rows[0]["FHDW"].ToString().Trim();
                    cbSHDW.Text = dtECJL.Rows[0]["SHDW"].ToString().Trim();

                    //add by luobin
                    tbSenderPlace.Text = dtECJL.Rows[0]["FS_SENDERSTORE"].ToString();
                    tbReceiverPlace.Text = dtECJL.Rows[0]["FS_RECEIVERSTORE"].ToString();
                    cbProvider.Text = dtECJL.Rows[0]["FS_PROVIDER"].ToString();
                    tbBZ.Text = dtECJL.Rows[0]["FS_BZ"].ToString();


                    txtPJBH.Text = dtECJL.Rows[0]["FS_BILLNUMBER"].ToString().Trim();
                    cbJLLX.Text = "复磅";
                    cbJLLX.Enabled = false;

                    strZYBH = e_WEIGHTNO;
                }

                if (e_UNLOADFLAG == "0")
                {
                    cbJLLX.Text = "退货过磅";
                    cbJLLX.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        private void ClearData()
        {
            cbCH.Text = "";
            cbCH1.Text = "";
            txtHTH.Text = "";
            txtHTXMH.Text = "";
            //txtZS.Text = "";
            txtLH.Text = "";
            cbWLMC.Text = "";
            cbLX.Text = "";
            cbCYDW.Text = "";
            cbFHDW.Text = "";
            cbSHDW.Text = "";
            cbJLLX.Text = "";

            //add by luobin
            tbSenderPlace.Text = "";
            tbReceiverPlace.Text = "";

            strAdviseSpec = "";
            strZZJY = "";
            strGPMaterial = "";

        }
        /// <summary>
        /// 清空车证号与重量数据
        /// </summary>
        private void ClearCarNoData()
        {
            cbCH1.Text = "";
            txtZL.Text = "";
            txtPZ.Text = "";
            txtMZ.Text = "";
            txtJZ.Text = "";
            txtCZH.Text = "";
            txtCarNo.Text = "";
            strYB = "";
            strYCJL = "";
        }
        /// <summary>
        /// 验证
        /// </summary>
        private bool ControlProve()
        {
            if (txtJLD.Text == "")
            {
                MessageBox.Show("请选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJLD.Focus();
                return false;
            }
            if (cbCH.Text == "")
            {
                MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCH.Focus();
                return false;
            }
            if (cbCH1.Text == "")
            {
                MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCH1.Focus();
                return false;
            }
            if (cbWLMC.Text == "")
            {
                MessageBox.Show("请选择物料信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbWLMC.Focus();
                return false;
            }
            if (cbLX.Text == "")
            {
                MessageBox.Show("请选择流向信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLX.Focus();
                return false;
            }
            if (cbFHDW.Text == "")
            {
                MessageBox.Show("请选择发货单位信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFHDW.Focus();
                return false;
            }
            if (cbSHDW.Text == "")
            {
                MessageBox.Show("请选择收货单位信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSHDW.Focus();
                return false;
            }
            if (cbCYDW.Text == "")
            {
                MessageBox.Show("请选择承运单位信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCYDW.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region 读数按钮
        /// <summary>
        /// 读数按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDS_Click(object sender, EventArgs e)
        {
            //Invoke(m_MainThreadCapPicture);

            if (strPoint == "")
            {
                MessageBox.Show("请选择计量点！");
                return;
            }
            if (txtCZH.Text == "")
            {
                MessageBox.Show("请先刷卡或者录入车证卡号！");
                return;
            }
            if (strYCJL == "" && strYB == "")
            {
                MessageBox.Show("还没预报或没有一次计量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (txtXSZL.Text != "")
            {
                float value = Convert.ToSingle(txtXSZL.Text);
                txtZL.Text = string.Format("{0:F3}", value);
            }

            btnBC.Enabled = true;
            btnGPBC.Enabled = true;
            btnDS.Enabled = false;

            strDS = "1";

            //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
            //Invoke(m_MainThreadCapPicture); //用委托抓图

            //if (strYCJL == "1")
            //{
            //    QueryYCJLData();
            //}
            //ultraTabPageControl1.Enabled = false;
            //ultraTabPageControl3.Enabled = false;
            //ultraTabPageControl2.Tab.Selected = true;

            //曲线图表刷新
            if (m_nPointCount > 0)
            {
                for (int i = 0; i < m_nPointCount; i++)
                {
                    if (m_PoundRoomArray[i].POINTNAME.Trim() == ultraGrid2.ActiveRow.Cells["FS_POINTNAME"].Value.ToString().Trim())
                    {
                        dtQX.Rows.Clear();
                        dtQX.Columns.Clear();
                        ultraChart1.DataSource = dataTable6;
                    }
                }
            }

            switch (strPoint)
            {
                case "K01":
                    ksht1 = 1;
                    break;
                case "K02":
                    ksht2 = 1;
                    break;
                case "K03":
                    ksht3 = 1;
                    break;
                case "K04":
                    ksht4 = 1;
                    break;
                case "K05":
                    ksht5 = 1;
                    break;
                case "K06":
                    ksht6 = 1;
                    break;
                case "K07":
                    ksht7 = 1;
                    break;
                case "K08":
                    ksht8 = 1;
                    break;
                case "K09":
                    ksht9 = 1;
                    break;
                case "K010":
                    ksht10 = 1;
                    break;
                case "K011":
                    ksht11 = 1;
                    break;
                case "K012":
                    ksht12 = 1;
                    break;
                case "K013":
                    ksht13 = 1;
                    break;
                case "K014":
                    ksht14 = 1;
                    break;


                default:
                    break;
            }
        }

        private void GraspImage()
        {

            //DateTime now = DateTime.Now;
            //strNumber = strJLDID + "-" + now.ToString("yyyyMMddHHmmss"); 

            //string qc = "qcpicture";
            //getImage.GraspImage(strNumber, qc, pictureBox1, pictureBox2, strTPID);
            //getImage.GraspImage(strNumber, qc, pictureBox1, pictureBox2, pictureBox3);

            strNumber = strPoint;
            fileName1 = strNumber + "1.bmp";
            fileName2 = strNumber + "2.bmp";
            fileName3 = strNumber + "3.bmp";
            fileName4 = strNumber + "4.bmp";
            fileName5 = strNumber + "5.bmp";
            fileName6 = strNumber + "6.bmp";
            fileName7 = strNumber + "7.bmp";

            int i;
            for (i = 0; i < 10; i++)
            {
                if (m_PoundRoomArray[i].POINTID == strPoint)
                {
                    i = i;
                    break;
                }
            }

            //抓动态曲线图
            try
            {
                //sdk.SDK_CapturePicture(relhandle, stRunPath + "\\qcpicture\\" + fileName7);
                Bitmap img = getscreenfromhandle((int)picHT.Handle);
                img.Save(stRunPath + "\\qcpicture\\" + fileName7, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //抓第一张图
            try
            {
                m_PoundRoomArray[i].VideoRecord.SDK_CapturePicture(m_PoundRoomArray[i].Channel1, stRunPath + "\\qcpicture\\" + fileName1);
                Thread.Sleep(200);
                //Bitmap img = getscreenfromhandle((int)VideoChannel1.Handle);
                //img.Save(stRunPath + "\\qcpicture\\" + fileName1, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第二张图
            try
            {
                sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel2, stRunPath + "\\qcpicture\\" + fileName2);
                Thread.Sleep(200);
                //Bitmap img = getscreenfromhandle((int)VideoChannel2.Handle);
                //img.Save(stRunPath + "\\qcpicture\\" + fileName2, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第三张图
            try
            {
                sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel3, stRunPath + "\\qcpicture\\" + fileName3);
                Thread.Sleep(200);
                //Bitmap img = getscreenfromhandle((int)VideoChannel3.Handle);
                //img.Save(stRunPath + "\\qcpicture\\" + fileName3, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //panelYCSP.BringToFront();
            //panelYCSP.SendToBack();
            //抓第四张图
            try
            {
                //sdk.SDK_CapturePicture(relhandle4, stRunPath + "\\qcpicture\\" + fileName4);
                sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel4, stRunPath + "\\qcpicture\\" + fileName4);
                Thread.Sleep(200);

                //Bitmap img = getscreenfromhandle((int)pictureBox19.Handle);
                //img.Save(stRunPath + "\\qcpicture\\" + fileName4, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //抓第五张图
            try
            {
                //sdk.SDK_CapturePicture(relhandle5, stRunPath + "\\qcpicture\\" + fileName5);
                sdk.SDK_CapturePicture(m_PoundRoomArray[i].Channel5, stRunPath + "\\qcpicture\\" + fileName5);
                Thread.Sleep(200);
                //Bitmap img = getscreenfromhandle((int)pictureBox18.Handle);
                //img.Save(stRunPath + "\\qcpicture\\" + fileName5, ImageFormat.Bmp);
            }
            catch (System.Exception error)
            {
                //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ////抓第六张图
            //try
            //{
            //    sdk.SDK_CapturePicture(relhandle6, stRunPath + "\\qcpicture\\" + fileName6);
            //    //Bitmap img = getscreenfromhandle((int)pictureBox6.Handle);
            //    //img.Save(stRunPath + "\\qcpicture\\" + fileName6, ImageFormat.Bmp);
            //}
            //catch (System.Exception error)
            //{
            //    //MessageBox.Show(error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
           
        }
        #endregion

        #region 获取图片方法
        /// <summary>
        /// 抓取句柄所指窗口
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        private Bitmap getscreenfromhandle(int hwnd)
        {
            RECT rc = new RECT();
            GetWindowRect(hwnd, ref  rc);
            return getscreen(rc.left, rc.top, rc.right - rc.left, rc.bottom - rc.top);

        }

        private Bitmap getscreen(int left, int top, int width, int height)  //获得屏幕指定区域 
        {
            IntPtr dc1 = CreateDC("DISPLAY", null, null, (IntPtr)null);
            Graphics newGraphics = Graphics.FromHdc(dc1);
            Bitmap img = new Bitmap(width, height, newGraphics);
            Graphics thisGraphics = Graphics.FromImage(img);
            IntPtr dc2 = thisGraphics.GetHdc();
            IntPtr dc3 = newGraphics.GetHdc();
            BitBlt(dc2, 0, 0, width, height, dc3, left, top, 13369376);
            thisGraphics.ReleaseHdc(dc2);
            newGraphics.ReleaseHdc(dc3);
            return img;
        }
        #endregion

        #region 保存图片信息
        /// <summary>
        /// 保存一次计量图片数据带返回值
        /// </summary>
        /// <param name="PictureName"></param>
        private void AddTPData(string strWeightNo)
        {
            while (m_SaveImageSign == 0)
            {
                if (m_GraspImageSign == 1) //如果抓图还未完成，继续进行抓图
                {
                    continue;
                }
                string strTPID = "";



                strTPID = strWeightNo;

                byte[] TP1 = GetImageFile(strNumber, 1);
                byte[] TP2 = GetImageFile(strNumber, 2);
                byte[] TP3 = GetImageFile(strNumber, 3);
                byte[] TP4 = GetImageFile(strNumber, 4);
                byte[] TP5 = GetImageFile(strNumber, 5);

                byte[] TP6 = new byte[1];
                byte[] TP7 = new byte[1];
                string strCurveImage = SaveCurveImage();

                string strJLCS = "";
                if (strYCJL == "1" && cbJLLX.Text.Trim() != "复磅")
                {
                    strJLCS = "2";
                }
                else
                {
                    strJLCS = "1";
                }

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "SaveTPData";
                ccp.ServerParams = new object[] { strTPID, TP1, TP2, TP3, TP4, TP5, TP6, TP7, strJLCS, strCurveImage };

                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                m_SaveImageSign = 1;
            }


        }

        private byte[] GetImageFile(string SequenceNo, int index)
        {
            byte[] FileContent;

            if (System.IO.File.Exists(stRunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".bmp") == true)
            {
                Bitmap img = new Bitmap(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".bmp");
                //System.Drawing.Image.GetThumbnailImageAbort callb = null;
                //System.Drawing.Image newimage = img.GetThumbnailImage(img.Width / 2, img.Height / 2, callb, new System.IntPtr());缩放图片，要引用IntPtr方法（重要方法），也要定义callb
                //System.Drawing.Image newimage = img.GetThumbnailImage(img.Width, img.Height, callb, new System.IntPtr());
                System.Drawing.Image newimage = System.Drawing.Image.FromFile(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".bmp");

                if (index == 1)
                {
                    //if (s_Guid != "" && ImageGPZL != "")
                    //{
                    //    ImageJZ = ImageGPZL;
                    //}
                    //else
                    //{
                    // ImageJZ = Convert.ToDecimal(txtXSZL.Text.Trim()).ToString(); //添加到图片重量
                    //}
                    Graphics g = Graphics.FromImage(newimage);
                    g.DrawImage(newimage, 0, 0, newimage.Width, newimage.Height);
                    Font f = new Font("宋体", 28);
                    Brush b = new SolidBrush(Color.Red);
                    if (m_ImageWeight == "")
                        m_ImageWeight = (Convert.ToDecimal(txtXSZL.Text.Trim()) - -s_toZore[m_iSelectedPound]).ToString(); ;
                    string addText = m_ImageWeight;
                    //string addText = Convert.ToDecimal(txtXSZL.Text.Trim()).ToString();
                    g.DrawString(addText, f, b, 100, 20);
                }

                //转换成JPG   
                newimage.Save(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                newimage.Dispose();
                FileContent = System.IO.File.ReadAllBytes(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG");

                return FileContent;
            }

            if (System.IO.File.Exists(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG") == true)
            {
                FileContent = System.IO.File.ReadAllBytes(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG");

                return FileContent;
            }

            FileContent = new byte[1];
            //FileContent = System.IO.File.ReadAllBytes(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG");
            return FileContent;
        }

        /// <summary>
        /// 保存二次计量图片数据
        /// </summary>
        /// <param name="PictureName"></param>
        private void UpdateTPData(string strWeightNo)
        {
            while (m_SaveImageSign == 0)
            {
                if (m_GraspImageSign == 1)
                {
                    continue;
                }
                string s = DateTime.Now.ToString("yyyyMMddHHmmssffffff");//获取6位毫秒微妙DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss.ff")
                string aa = DateTime.Now.Ticks.ToString();//时间排序
                string bb = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString();//获取毫秒
                string strTPID = strWeightNo;
                //dr["TPID"] = TPID;

                byte[] TP1 = GetImageFile(strNumber, 1);
                byte[] TP2 = GetImageFile(strNumber, 2);
                byte[] TP3 = GetImageFile(strNumber, 3);
                byte[] TP4 = GetImageFile(strNumber, 4);
                byte[] TP5 = GetImageFile(strNumber, 5);
                //byte[] TP6 = GetImageFile(strNumber, 6);
                byte[] TP6 = new byte[1];
                //byte[] TP7 = GetImageFile(strNumber, 7);
                byte[] TP7 = new byte[1];

                string strCurveImage = SaveCurveImage();

                string strJLCS = "";
                if (strYCJL == "1")
                {
                    strJLCS = "2";
                }
                else
                {
                    strJLCS = "1";
                }

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "UpdateTPData";
                ccp.ServerParams = new object[] { strTPID, TP1, TP2, TP3, TP4, TP5, TP6, TP7, strJLCS, strCurveImage };
                //ccp.IfShowErrMsg = false;

                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                //string a = ccp.ReturnObject.ToString(); 
                btnBC.Enabled = false;
                btnGPBC.Enabled = false;

                m_SaveImageSign = 1;
            }
        }

        /// <summary>
        /// 删除一次计量图片数据
        /// </summary>
        /// <param name="PictureName"></param>
        private void DeleteTPData(string strWeightNo)
        {
            string strTPID = strWeightNo;


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "DeleteTPData";
            ccp.ServerParams = new object[] { strTPID };
            //ccp.IfShowErrMsg = false;

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        #endregion

        #region 主体数据保存
        /// <summary>
        /// 保存一次计量数据
        /// </summary>
        private bool AddYCJLData()
        {
            //if (strZYBH == "") //如果图片未保存成功，这里自己产生Guid
            //{
            //    strZYBH = Guid.NewGuid().ToString().Trim();
            //}
            //string strSJZL = txtXSZL.Text.Trim();
            strZYBH = Guid.NewGuid().ToString().Trim();
            string strSJZL = "";
            //if (ImageJZ != null)
            //{
            //    strSJZL = ImageJZ;
            //}
            //else
            //{                

            if (txtZL.Text.Trim() == "")
                strSJZL = (Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
            else
                strSJZL = (Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();

            m_ImageWeight = strSJZL;
            //}
            string strJLD = txtJLD.Text.Trim();
            string strJLY = txtJLY.Text.Trim();
            string strBC = txtBC.Text.Trim();

            ybCount[selectRow].strCZH = txtCZH.Text.Trim();

            //车号
            string strCH1 = this.cbCH.Text.Trim();
            string strCH2 = this.cbCH1.Text.Trim();
            ybCount[selectRow].strCH = strCH1 + strCH2;

            string strYKL = txtYKL.Text.Trim();
            string strZL = "";
            if (strYKL != "")
            {
                decimal YKL = Convert.ToDecimal(strYKL) / 1000;
                decimal JZ = Convert.ToDecimal(strSJZL) - YKL;
                strZL = JZ.ToString();
            }
            else
            {
                strZL = strSJZL;
            }


            if (cbWLMC.SelectedValue == null || cbWLMC.SelectedValue.ToString() == "")
            {

            }
            else
            {
                ybCount[selectRow].strWLID = cbWLMC.SelectedValue.ToString().Trim();
                ybCount[selectRow].strWLMC = cbWLMC.Text.Trim();
            }


            if (cbFHDW.SelectedValue == null || cbFHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strFHFDM = cbFHDW.SelectedValue.ToString().Trim();
            }


            if (cbSHDW.SelectedValue == null || cbSHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strSHFDM = cbSHDW.SelectedValue.ToString().Trim();
            }


            if (cbCYDW.SelectedValue == null || cbCYDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strCYDW = cbCYDW.SelectedValue.ToString().Trim();
            }


            if (cbLX.SelectedValue != null)
            {
                if (cbLX.Text != sLX)
                {
                    ybCount[selectRow].strLX = cbLX.SelectedValue.ToString().Trim();
                }
            }
            //add by luobin 

            //if (ybCount[selectRow].strSHKCD == "") ;
            ybCount[selectRow].strSHKCD = tbReceiverPlace.Text.Trim();

            //if (ybCount[selectRow].strFHKCD == "")
            ybCount[selectRow].strFHKCD = tbSenderPlace.Text.Trim();

            string ycsfyc = "";
            //3.8号新加
            if (chbSFYC.Checked == true)
            {
                ycsfyc = "1";//异常保存标志
            }

            string strProvider = "";
            if (cbProvider.SelectedValue != null)
                strProvider = cbProvider.SelectedValue.ToString();
            string strBZ = tbBZ.Text.Trim();
            string strStoveNo = txtLH.Text.Trim();
            string strCount = txtZS.Text.Trim();
            string strOrderNo = txtHTH.Text.Trim();
            string strItemNo = txtHTXMH.Text.Trim();


            print.printJZ = strZL; //打印重量

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveYCJLData";
            ccp.ServerParams = new object[] { strZYBH, ybCount[selectRow].strYBH, strOrderNo, strItemNo, strStoveNo, strCount,
                 ybCount[selectRow].strCZH, ybCount[selectRow].strCH, ybCount[selectRow].strWLID, ybCount[selectRow].strWLMC, ybCount[selectRow].strFHFDM, ybCount[selectRow].strFHKCD,
                 ybCount[selectRow].strSHFDM, ybCount[selectRow].strSHKCD, ybCount[selectRow].strLX,ybCount[selectRow].strCYDW, strJLDID, ybCount[selectRow].strYBZZ, ybCount[selectRow].strYBPZ, 
                 ybCount[selectRow].strYBJZ, strZL, strJLD, strJLY, strBC, ycsfyc, strYKL, y_IFSAMPLING, y_IFACCEPT, y_DRIVERNAME, y_DRIVERIDCARD, txtPJBH.Text.Trim(), txtDFJZ.Text.Trim(), strCode,strProvider,strBZ };

            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //add by luobin
            WriteLog("一次计量保存 计量号:" + strZYBH + "  车号: " + ybCount[selectRow].strCH + "  返回代码 ：" + ccp.ReturnCode.ToString() + "     返回信息 ：" + ccp.ReturnInfo);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }

            //string errInfo = "";
            //if (ccp.ReturnCode == -1)
            //{
            //    errInfo = ccp.ReturnInfo;
            //}
            //if (errInfo != "")
            //{
            //    if (errInfo.IndexOf("ORA-01401") >= 0)
            //    {
            //        MessageBox.Show("值输入过大，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cbCH1.Focus();
            //        DeleteTPData();
            //    }
            //}

            btnBC.Enabled = false;
            btnGPBC.Enabled = false;
            return true;

        }

        /// <summary>
        /// 复磅修改一次计量数据
        /// </summary>
        private bool UpdateYCJLData()
        {

            string strSJZL = "";
            //if (ImageJZ != null)
            //{
            //    strSJZL = ImageJZ;
            //}
            //else
            //{

            if (txtZL.Text.Trim() == "")
                strSJZL = (Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
            else
                strSJZL = (Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();

            m_ImageWeight = strSJZL;
            //}
            string strJLD = txtJLD.Text.Trim();
            string strJLY = txtJLY.Text.Trim();
            string strBC = txtBC.Text.Trim();

            //车号
            string strCH1 = this.cbCH.Text.Trim();
            string strCH2 = this.cbCH1.Text.Trim();
            ybCount[selectRow].strCH = strCH1 + strCH2;

            string strYKL = txtYKL.Text.Trim();
            string strZL = "";
            if (strYKL != "")
            {
                decimal YKL = Convert.ToDecimal(strYKL) / 1000;
                decimal JZ = Convert.ToDecimal(strSJZL) - YKL;
                strZL = JZ.ToString();
            }
            else
            {
                strZL = strSJZL;
            }



            if (cbWLMC.SelectedValue == null || cbWLMC.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stWLID = cbWLMC.SelectedValue.ToString().Trim();
                stWLMC = cbWLMC.Text.Trim();
            }

            if (cbFHDW.SelectedValue == null || cbFHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                stFHFDM = cbFHDW.SelectedValue.ToString().Trim();
            }

            if (cbSHDW.SelectedValue == null || cbSHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                stSHFDM = cbSHDW.SelectedValue.ToString().Trim();
            }

            if (cbCYDW.SelectedValue == null || cbCYDW.SelectedValue.ToString() == "")
            { }
            else
            {
                stCYDW = cbCYDW.SelectedValue.ToString().Trim();
            }

            //if (cbLX.SelectedValue == null)
            //{
            //    MessageBox.Show("流向不能输入，请选择流向！");
            //    cbLX.Text = "";
            //    cbLX.Focus();
            //    btnBC.Enabled = true;
            //    return;
            //}
            //else
            //{
            //    stLX = cbLX.SelectedValue.ToString().Trim();
            //}

            if (cbLX.SelectedValue != null)
            {
                if (cbLX.Text != sLX)
                {
                    stLX = cbLX.SelectedValue.ToString().Trim();
                }
            }

            //if (cbWLMC.SelectedValue == null || cbFHDW.SelectedValue == null || cbSHDW.SelectedValue == null || cbCYDW.SelectedValue == null)
            //{
            //    GetBaseData();
            //    BandBaseData();
            //}

            string ycsfyc = "";
            //3.8号新加
            if (chbSFYC.Checked == true)
            {
                ycsfyc = "1";
            }

            string strProvider = "";
            if (cbProvider.SelectedValue != null)
                strProvider = cbProvider.SelectedValue.ToString();
            string strBZ = tbBZ.Text.Trim();

            print.printJZ = strZL;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdateYCJLData";
            ccp.ServerParams = new object[] { strZYBH, stYBH, txtHTH.Text.Trim()/*stHTH*/, stHTXMH, stLH, stZS, stCZH, stCH, stWLID, stWLMC, stFHFDM, s_SENDERSTORE,
                 stSHFDM, stSHKCD, stLX, stCYDW, strJLDID, stYBZZ, stYBPZ, stYBJZ, strZL, strJLD, strJLY, strBC, ycsfyc, strYKL, s_SAMPLETIME, 
                 s_SAMPLEPLACE, s_SAMPLEFLAG, s_UNLOADPERSON, s_UNLOADTIME, s_UNLOADPLACE, s_CHECKPERSON, s_CHECKTIME, s_CHECKPLACE, s_CHECKFLAG,
                 s_DRIVERNAME, s_DRIVERIDCARD, s_IFSAMPLING, s_IFACCEPT, s_REWEIGHTTIME, s_REWEIGHTPLACE, s_REWEIGHTPERSON, txtPJBH.Text, strCode,strProvider,strBZ };

            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }
            //string errInfo = "";
            //if (ccp.ReturnCode == -1)
            //{
            //    errInfo = ccp.ReturnInfo;
            //    if (ccp.ReturnInfo == null)
            //    {
            //        errInfo = "";
            //    }
            //}
            //if (errInfo != "")
            //{
            //    if (errInfo.IndexOf("ORA-01401") >= 0)
            //    {
            //        MessageBox.Show("值输入过大，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cbCH1.Focus();
            //    }
            //}

            btnBC.Enabled = false;
            btnGPBC.Enabled = false;
            return true;
            //UltValuesShow();
        }
        /// <summary>
        /// 保存二次计量数据同时删除一次计量表数据
        /// </summary>
        private bool AddECJLData()
        {
            //string strSJZL = txtZL.Text.Trim();//二次计量重量

            string s_DFJZ = this.txtDFJZ.Text.Trim();
            string strSJZL = "";
            //if (ImageJZ != null)
            //{
            //    strSJZL = ImageJZ;
            //}
            //else
            //{

            if (txtZL.Text.Trim() == "")
                strSJZL = (Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
            else
                strSJZL = (Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
            //}
            m_ImageWeight = strSJZL;

            string strJLD = strJLDID;
            string strJLY = txtJLY.Text.Trim();
            string strBC = txtBC.Text.Trim();

            //车号
            string strCH1 = this.cbCH.Text.Trim();
            string strCH2 = this.cbCH1.Text.Trim();
            stCH = strCH1 + strCH2;

            string strYKL = txtYKL.Text.Trim();
            string strECZL = "";
            strECZL = strSJZL;
            //if (strYKL != "")
            //{
            //    decimal YKL = Convert.ToDecimal(strYKL) / 1000;
            //    decimal dJZ = Convert.ToDecimal(strSJZL) - YKL;
            //    strECZL = dJZ.ToString();
            //}
            //else if (s_YKBL != "")
            //{
            //    strECZL = strSJZL;
            //}
            //else
            //{
            //    strECZL = strSJZL;
            //}

            string strMZ = "";//毛重
            string strPZ = "";//皮重
            string strJZ = "";//净重
            string strMZJLD = "";//毛重计量点
            string strMZJLY = "";//毛重计量员
            string strMZJLSJ = "";//毛重计量时间
            string strMZJLBC = "";//毛重计量班次
            string strPZJLD = "";//皮重计量点
            string strPZJLY = "";//皮重计量员
            string strPZJLSJ = "";//皮重计量时间
            string strPZJLBC = "";//皮重计量班次
            string strKHZL = ""; //扣后重量

            string strWZBDTM = "";//完整磅单条码
            strWZBDTM = strCode;
            if (strECZL == "")
            {
                strECZL = "0";
            }
            if (strYCZL == "")
            {
                strYCZL = "0";
            }
            if (Convert.ToDecimal(strECZL) <= Convert.ToDecimal(strYCZL))
            {
                strMZ = strYCZL;
                strPZ = strECZL;
                Decimal JZ = Math.Round(Convert.ToDecimal(strMZ) - Convert.ToDecimal(strPZ), 3);
                strJZ = JZ.ToString();

                //扣渣
                if (strYKL != "" && strYKL != "0")
                {
                    decimal YKL = Convert.ToDecimal(strYKL);
                    strKHZL = (Convert.ToDecimal(JZ) - YKL).ToString();
                }
                else if (s_YKBL != "")
                {
                    decimal KHJZ = Math.Round(Convert.ToDecimal(JZ) - Convert.ToDecimal(JZ) * Convert.ToDecimal(s_YKBL), 3);
                    strKHZL = KHJZ.ToString();
                }
                else
                {
                    strKHZL = JZ.ToString();
                }
                print.printKHJZ = strKHZL;

                //strMZJLD = strYCJLD.ToString().Trim();
                strMZJLD = strBFBH.ToString().Trim();
                strMZJLY = strYCJLY.ToString().Trim();
                strMZJLSJ = strYCJLSJ.ToString();//毛重计量时间
                strMZJLBC = strYCJLBC.ToString().Trim();

                //strPZJLD = strJLD;
                strPZJLD = strJLDID;
                strPZJLY = strJLY;
                strPZJLBC = strBC;
                strPZJLSJ = "";//皮重计量时间

                //钢坯对方净重
                if (s_DFJZ != "")
                {
                    s_CZ = (JZ - Convert.ToDecimal(s_DFJZ)).ToString();//钢坯对方净重差值
                }
                else
                {
                    s_CZ = "";
                }

                //钢坯计算，，，
                if (txtZS2.Text == "")
                {
                    txtZS2.Text = "0";
                }
                if (txtZS3.Text == "")
                {
                    txtZS3.Text = "0";
                }
                if (txtZS.Text.Trim() != "")
                {
                    int iZS = Convert.ToInt32(txtZS.Text.Trim()) + Convert.ToInt32(txtZS2.Text.Trim()) + Convert.ToInt32(txtZS3.Text.Trim());
                    //GPJZ = strJZ; //钢坯净重
                    decimal sGPJZ = JZ / iZS;
                    decimal GPZL = sGPJZ * Convert.ToInt32(stZS); //钢坯净重
                    GPJZ = GPZL.ToString();
                    //ImageJZ = GPJZ;
                }
                //ImageJZ = GPJZ;

            }
            if (Convert.ToDecimal(strECZL) > Convert.ToDecimal(strYCZL))
            {
                strMZ = strECZL;
                strPZ = strYCZL;
                Decimal JZ = Convert.ToDecimal(strMZ) - Convert.ToDecimal(strPZ);
                strJZ = JZ.ToString();

                //扣渣
                if (strYKL != "" && strYKL != "0")
                {
                    decimal YKL = Convert.ToDecimal(strYKL);
                    strKHZL = (Convert.ToDecimal(JZ) - YKL).ToString();
                }
                else if (s_YKBL != "")
                {
                    decimal KHJZ = Math.Round(Convert.ToDecimal(JZ) - Convert.ToDecimal(JZ) * Convert.ToDecimal(s_YKBL), 3);
                    strKHZL = KHJZ.ToString();
                }
                else
                {
                    strKHZL = JZ.ToString();
                }

                //strMZJLD = strJLD;
                strMZJLD = strJLDID;
                strMZJLY = strJLY;
                strMZJLBC = strBC;

                //strPZJLD = strYCJLD;
                strPZJLD = strBFBH;
                strPZJLY = strYCJLY;
                strPZJLSJ = strYCJLSJ.ToString();//皮重计量时间
                strPZJLBC = strYCJLBC;

                if (s_DFJZ != "")
                {
                    s_CZ = (JZ - Convert.ToDecimal(s_DFJZ)).ToString();
                }
                else
                {
                    s_CZ = "";
                }

                if (txtZS2.Text == "")
                {
                    txtZS2.Text = "0";
                }
                if (txtZS3.Text == "")
                {
                    txtZS3.Text = "0";
                }

                if (txtZS.Text.Trim() != "")
                {
                    int iZS = Convert.ToInt32(txtZS.Text.Trim()) + Convert.ToInt32(txtZS2.Text.Trim()) + Convert.ToInt32(txtZS3.Text.Trim());
                    //GPJZ = strJZ; //钢坯净重
                    decimal sGPJZ = JZ / iZS;
                    decimal GPZL = sGPJZ * Convert.ToInt32(stZS); //钢坯净重
                    GPJZ = GPZL.ToString();
                    //ImageJZ = GPJZ;
                }
                //ImageJZ = GPJZ;
            }

            //if (cbWLMC.SelectedValue == null)
            //{
            //    SaveWLData();
            //    stWLID = sWLID;
            //    stWLMC = cbWLMC.Text.Trim();
            //    //GetWLData();
            //}
            //else
            //{
            //    if (cbWLMC.Text != sWLMC)
            //    {
            //        stWLID = cbWLMC.SelectedValue.ToString().Trim();
            //        stWLMC = cbWLMC.Text.Trim();
            //    }
            //}
            if (cbWLMC.SelectedValue == null || cbWLMC.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stWLID = cbWLMC.SelectedValue.ToString().Trim();
                stWLMC = cbWLMC.Text.Trim();
            }

            //if (cbFHDW.SelectedValue == null)
            //{
            //    SaveFHDWData();
            //    stFHFDM = sFHDWID;
            //    //GetFHDWData();
            //}
            //else
            //{
            //    if (cbFHDW.Text != sFHDW)
            //    {
            //        stFHFDM = cbFHDW.SelectedValue.ToString().Trim();
            //    }
            //}

            if (cbFHDW.SelectedValue == null || cbFHDW.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stFHFDM = cbFHDW.SelectedValue.ToString().Trim();
            }

            //if (cbSHDW.SelectedValue == null)
            //{
            //    SaveSHDWData();
            //    stSHFDM = sSHDWID;
            //    //GetSHDWData();
            //}
            //else
            //{
            //    if (cbSHDW.Text != sSHDW)
            //    {
            //        stSHFDM = cbSHDW.SelectedValue.ToString().Trim();
            //    }
            //}

            if (cbSHDW.SelectedValue == null || cbSHDW.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stSHFDM = cbSHDW.SelectedValue.ToString().Trim();
            }

            //if (cbCYDW.SelectedValue == null)
            //{
            //    SaveCYDWData();
            //    stCYDW = sCYDWID;
            //    //GetCYDWData();
            //}
            //else
            //{
            //    if (cbCYDW.Text != sCYDW)
            //    {
            //        stCYDW = cbCYDW.SelectedValue.ToString().Trim();
            //    }
            //}

            if (cbCYDW.SelectedValue == null || cbCYDW.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stCYDW = cbCYDW.SelectedValue.ToString().Trim();
            }

            //if (cbLX.SelectedValue == null)
            //{
            //    MessageBox.Show("流向不能输入，请选择流向！");
            //    cbLX.Text = "";
            //    cbLX.Focus();
            //    btnBC.Enabled = true;
            //    return;
            //}
            //else
            //{
            //    stLX = cbLX.SelectedValue.ToString().Trim();
            //}

            if (cbLX.SelectedValue != null)
            {
                if (cbLX.Text != sLX)
                {
                    stLX = cbLX.SelectedValue.ToString().Trim();
                }
            }

            //if (cbWLMC.SelectedValue == null || cbFHDW.SelectedValue == null || cbSHDW.SelectedValue == null || cbCYDW.SelectedValue == null)
            //{
            //    GetBaseData();
            //    BandBaseData();
            //}

            string strSFYC = "";
            //3.8号新加
            if (chbSFYC.Checked == true)
            {
                strSFYC = "1";
            }
            //else
            //{
            //    if (strYCSFYC == "1")
            //    {
            //        strSFYC = "1";
            //    }
            //}

            //打印重量赋值
            print.printMZ = string.Format("{0:F3}", Convert.ToDecimal(strMZ));
            print.printPZ = string.Format("{0:F3}", Convert.ToDecimal(strPZ));
            print.printJZ = string.Format("{0:F3}", Convert.ToDecimal(strJZ));

            stSendStore = this.tbSenderPlace.Text.Trim();
            stReceiverSotre = this.tbReceiverPlace.Text.Trim();
            string strProvider = "";
            if (cbProvider.SelectedValue != null)
                strProvider = cbProvider.SelectedValue.ToString();
            string strBZ = tbBZ.Text.Trim();


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveECJLData";
            ccp.ServerParams = new object[] { strZYBH, txtHTH.Text.Trim()/*stHTH*/, stHTXMH, stLH, stZS, stCZH, stCH, stWLID, stWLMC, stFHFDM, 
                stCYDW, stSHFDM, stLX, stYBZZ, stYBPZ, stYBJZ, strMZ, strMZJLD, strMZJLY, strMZJLSJ, 
                strMZJLBC, strPZ, strPZJLD, strPZJLY, strPZJLSJ, strPZJLBC, strYCBDTM, strXCRKSJ, strXCCKSJ, strXCQR,
                strXCKGY, strZCRKSJ, strZCCKSJ, strZCQR, strZCKGY, strJZ, strQYY, strWZBDTM, strYCSFYC, strSFYC,
                strYKL, strJLD, s_SAMPLETIME, s_SAMPLEPLACE, s_SAMPLEFLAG,s_UNLOADPERSON, s_UNLOADTIME, s_UNLOADPLACE, s_CHECKPERSON, s_CHECKTIME,
                s_CHECKPLACE, s_CHECKFLAG, s_DRIVERNAME, s_DRIVERIDCARD, stSendStore,s_IFSAMPLING, stSHKCD, s_IFACCEPT, s_REWEIGHTFLAG, s_REWEIGHTTIME,
                s_REWEIGHTPLACE, s_REWEIGHTPERSON, txtPJBH.Text.Trim(), s_DFJZ, s_CZ, s_YKBL, strKHZL,stReceiverSotre,strProvider,strBZ };

            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            WriteLog("二次计量保存 计量号:" + strZYBH + "  车号: " + cbCH.Text.Trim() + cbCH1.Text.Trim() + "  返回代码 ：" + ccp.ReturnCode.ToString() + "     返回信息 ：" + ccp.ReturnInfo);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }



            if (txtZS2.Text == "0")
            {
                txtZS2.Text = "";
            }
            if (txtZS3.Text == "0")
            {
                txtZS3.Text = "";
            }

            //判断车号是否输入过长
            //string errInfo = "";
            //if (ccp.ReturnCode == -1)
            //{
            //    errInfo = ccp.ReturnInfo;
            //}
            //if (errInfo != "")
            //{
            //    if (errInfo.IndexOf("ORA-01401") >= 0)
            //    {
            //        MessageBox.Show("值输入过大，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cbCH1.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //}


            //保存历史皮重
            CoreClientParam ccpPZ = new CoreClientParam();
            ccpPZ.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpPZ.MethodName = "SavePZData";
            ccpPZ.ServerParams = new object[] { stCH, strPZ };

            this.ExecuteNonQuery(ccpPZ, CoreInvokeType.Internal);

            //MessageBox.Show("二次计量保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            btnBC.Enabled = false;
            btnGPBC.Enabled = false;
            return true;
            //UltValuesShow();
            //QueryYCBData();
        }
        /// <summary>
        /// 保存外协数据
        /// </summary>
        private bool AddWXData()
        {
            string sZYBH = "";
            strZYBH = Guid.NewGuid().ToString();
            sZYBH = strZYBH;

            //if (strZYBH != "")
            //{
            //    sZYBH = strZYBH;
            //}
            //else
            //{ 
            //    sZYBH = Guid.NewGuid().ToString(); 
            //}
            string sCH = cbCH.Text.Trim() + cbCH1.Text.Trim();
            string sZL = "";
            if (txtZL.Text.Trim() != "")
                sZL = (Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
            else
                sZL = (Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();

            m_ImageWeight = sZL;
            string cardNo = this.txtCZH.Text.Trim();
            string sJLD = txtJLD.Text.Trim();
            string sJLY = txtJLY.Text.Trim();
            decimal WXZL = Convert.ToDecimal(sZL) - s_toZore[m_iSelectedPound];
            string sWXZL = WXZL.ToString();
            double inCharge = 0;
            if (this.tbCharge.Text.Trim() != "")
                inCharge = Convert.ToDouble(this.tbCharge.Text.Trim());

            print.printJZ = sWXZL;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveWXData";
            ccp.ServerParams = new object[] { sZYBH, sCH, sWXZL, sJLD, sJLY, inCharge, strCode, cardNo };

            ccp.IfShowErrMsg = false;

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }
            btnBC.Enabled = false;
            return true;

        }
        /// <summary>
        /// 保存期限皮重数据
        /// </summary>
        private bool SaveQXPZData()
        {
            //string qGuid = Guid.NewGuid().ToString();
            if (strZYBH == "")
                strZYBH = Guid.NewGuid().ToString();

            string qCH = cbCH.Text.Trim() + cbCH1.Text.Trim();
            string cardNumber = this.txtCZH.Text.Trim();
            string strJLD = strJLDID;
            string strJLY = txtJLY.Text.Trim();
            string qZL = "";

            if (txtZL.Text.Trim() == "")
                qZL = txtXSZL.Text.Trim();
            else
                qZL = txtZL.Text.Trim();

            //m_ImageWeight = qZL;

            string dataBegin = DateTime.Today.ToString("yyyy-MM-dd 00:00:00");
            string dataEnd = DateTime.Today.ToString("yyyy-MM-dd 23:59:59");
            string qxBC = txtBC.Text.Trim();

            decimal QXPZ = Convert.ToDecimal(qZL) - s_toZore[m_iSelectedPound];
            string sQXPZ = QXPZ.ToString();

            m_ImageWeight = sQXPZ;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveQXPZData";
            ccp.ServerParams = new object[] { strZYBH, qCH, strJLD, strJLY, sQXPZ, dataBegin, dataEnd, qxBC,cardNumber };

            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }
            btnBC.Enabled = false;
            return true;
            //UltValuesShow();
        }
        /// <summary>
        /// 绑定数据显示
        /// </summary>
        private void UltValuesShow()
        {
            dataTable2.Clear();
            DataRow newDr = dataTable2.NewRow();
            dataTable2.Rows.InsertAt(newDr, 0);
            dataTable2.Rows[0]["FS_CARDNUMBER"] = txtCZH.Text.Trim();
            string cCH = cbCH.Text.Trim() + cbCH1.Text.Trim();
            dataTable2.Rows[0]["FS_CARNO"] = cCH;
            dataTable2.Rows[0]["FS_CONTRACTNO"] = txtHTH.Text.Trim();
            dataTable2.Rows[0]["FS_CONTRACTITEM"] = txtHTXMH.Text.Trim();
            dataTable2.Rows[0]["FS_STOVENO"] = txtLH.Text.Trim();
            dataTable2.Rows[0]["FN_COUNT"] = txtZS.Text.Trim();
            dataTable2.Rows[0]["FS_MATERIALNAME"] = cbWLMC.Text.Trim();
            dataTable2.Rows[0]["FS_SENDER"] = cbFHDW.Text.Trim();
            dataTable2.Rows[0]["FS_RECEIVER"] = cbSHDW.Text.Trim();
            dataTable2.Rows[0]["FS_TRANSNO"] = cbCYDW.Text.Trim();
            dataTable2.Rows[0]["FS_WEIGHTTYPE"] = cbLX.Text.Trim();
            dataTable2.Rows[0]["FN_WEIGHT"] = txtZL.Text.Trim();
            dataTable2.Rows[0]["FS_POUND"] = txtJLD.Text.Trim();
            dataTable2.Rows[0]["FS_WEIGHTER"] = txtJLY.Text.Trim();
            dataTable2.Rows[0]["FD_WEIGHTTIME"] = DateTime.Now.ToString();
            dataTable2.Rows[0]["FS_SHIFT"] = txtBC.Text.Trim();
            dataTable2.AcceptChanges();
            ultraGrid3.Refresh();
            Constant.RefreshAndAutoSize(ultraGrid3);
        }
        /// <summary>
        /// 保存按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBC_Click(object sender, EventArgs e)
        {
            try
            {
                //是否检测红外
                //if (chb_AutoInfrared.Checked == true)
                //{
                //    //前后端红外都被挡，给予保存提示
                //    if (StatusBack.Connected == false && StatusFront.Connected == false)
                //    {
                //        if (DialogResult.No == MessageBox.Show("前、后端红外都被挡，请确认停车到位，是否继续保存", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                //            return;
                //    }
                //}
                
                if (strPoint == "")
                {
                    MessageBox.Show("请选择计量点！");
                    return;
                }

                if (cbJLLX.Enabled == true && cbJLLX.Text.Trim() == "复磅")
                {
                    //MessageBox.Show("“复磅”不能选择，请重新选择计量类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //return;
                    //UpdateYCJLData();//复磅修改一次计量数据
                    string cardno = this.txtCZH.Text.Trim();
                    string strSQL = "select * from dt_firstcarweight where fs_cardnumber ='" + cardno + "'";
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.car.CarCard";
                    ccp.MethodName = "queryByClientSql";
                    ccp.ServerParams = new object[] { strSQL };
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ccp.SourceDataTable = dt;
                    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                    if (dt.Rows.Count > 0)
                    {
                        if (UpdateYCJLData() == false)
                        {
                            return;
                        }

                    }
                    else
                    {
                        string strSQl1 = "select * from dt_carweight_weight  where  fd_ecjlsj =(select max(fd_ecjlsj) from dt_carweight_weight where fs_cardnumber='" + cardno + "') and fs_cardnumber='" + cardno + "' ";
                        CoreClientParam ccp1 = new CoreClientParam();
                        ccp1.ServerName = "ygjzjl.car.CarCard";
                        ccp1.MethodName = "queryByClientSql";
                        ccp1.ServerParams = new object[] { strSQl1 };
                        System.Data.DataTable dt1 = new System.Data.DataTable();
                        ccp1.SourceDataTable = dt1;
                        this.ExecuteQueryToDataTable(ccp1, CoreInvokeType.Internal);
                        if (dt1.Rows.Count > 0)
                        {
                            if (UpdateECJLBData() == false)
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                   
                }
               
                if (cbJLLX.Text.Trim() != "外协")
                {
                    if (ControlProve() == false)
                    {
                        return;
                    }
                }
                else
                {
                    if (txtJLD.Text == "")
                    {
                        MessageBox.Show("请选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtJLD.Focus();
                        return;
                    }
                    if (cbCH.Text == "")
                    {
                        MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbCH.Focus();
                        return;
                    }
                    if (cbCH1.Text == "")
                    {
                        MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbCH1.Focus();
                        return;
                    }
                }

                //判断是否取样、卸货与验收
                if (strYCJL == "1" && cbJLLX.Text == "")
                {

                    //卸货
                    if (stSHKCD == "1")//是否需要卸货
                     {
                    if (strXCQR != "1")//是否已经卸货
                    {
                        if (DialogResult.No == MessageBox.Show("卡号：" + txtCZH.Text.Trim() + "车号：" + cbCH.Text.Trim() + cbCH1.Text.Trim() + "未卸货！,是否继续保存", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                            //MessageBox.Show("该车还未卸货，是否允许过磅！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                            return;
                    }

                    if (Convert.ToDateTime(strYCJLSJ).ToString("yyyy-MM-dd").Equals(System.DateTime.Now.ToString("yyyy-MM-dd")) == false)
                    {
                        if (DialogResult.No == MessageBox.Show("卡号：" + txtCZH.Text.Trim() + "车号：" + cbCH.Text.Trim() + cbCH1.Text.Trim() + "计量日期不为同一天！,是否继续保存", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                            return;
                    }

                    if (Convert.ToSingle(this.txtJZ.Text.Trim()) < 1)
                    {
                        if (DialogResult.No == MessageBox.Show("卡号：" + txtCZH.Text.Trim() + "车号：" + cbCH.Text.Trim() + cbCH1.Text.Trim() + "净重小于1吨！,是否继续保存", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                            return;
                    }

                    }

                }

                if (ifStart == "1")
                {
                    if (txtLH.Text.Trim() != "" && sDDH == "")
                    {
                        MessageBox.Show("请先查询炉号对应的合同号！");
                        return;
                    }
                    if (txtLH.Text.Trim() != "" && sDDH != "" && txtZS.Text.Trim() == "")
                    {
                        MessageBox.Show("请录入炉号对应的支数！");
                        txtZS.Focus();
                        return;
                    }
                    if (txtLH.Text.Trim() == "" && txtLH2.Text.Trim() != "" || txtLH3.Text.Trim() != "")
                    {
                        MessageBox.Show("请把炉号录入相应的位置！");
                        txtLH.Focus();
                        return;
                    }
                }

                if (strPoint == "")
                {
                    MessageBox.Show("请双击选择计量点接管信息，接管计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                

                this.Cursor = Cursors.WaitCursor;
                strBCSJ = "1"; //在保存时不需要计算净重，防止保存时系统自动退出               

                printInfoClear();//打印参数初始化
                strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                //print.printCZH = txtCZH.Text.Trim();
                print.printCH = cbCH.Text.Trim() + cbCH1.Text.Trim();
                print.printHTH = txtHTH.Text.Trim();
                print.printWLMC = cbWLMC.Text.Trim();
                print.printFHDW = cbFHDW.Text.Trim();
                print.printSHDW = cbSHDW.Text.Trim();
                print.printCYDW = cbCYDW.Text.Trim();
                print.printJLLX = cbJLLX.Text.Trim();
                print.printJLD = txtJLD.Text.Trim();
                print.printJLY = txtJLY.Text.Trim();
                //add by luobin
                print.printYKL = txtYKL.Text.Trim();
                print.printYKBL = s_YKBL;
                print.printAdviseSpec = this.strAdviseSpec;
                print.printZZJY = this.strZZJY;

                //print.pringJLCS = strYCJL;
                WriteLog("1");
                if (e_REWEIGHTFLAG == "1")//二次计量复磅标志（期限皮重时用）
                {
                    if (UpdateECJLBData() == false)
                    {
                        strBCSJ = "0";
                        this.Cursor = Cursors.Default;
                        return;
                    }

                }
                WriteLog("2");
                if (e_REWEIGHTFLAG != "1")
                {
                    //if (strYCJL == "")
                    //{
                    //    if (chbQXPZ.Checked == false)
                    //    {
                    //        QueryQXPZData();
                    //    }
                    //}

                    if (ifStart == "0")
                    {
                        if (strYCJL == "" && cbJLLX.Text == "")
                        {
                            if (strQXPZ == "")
                            {
                                WriteLog("3");
                                if (AddYCJLData() == false)//保存一次计量数据
                                {
                                    strBCSJ = "0";
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                                WriteLog("3.1");

                            }
                            else
                            {
                                WriteLog("4");
                                if (AddECJLBData() == false)//二次计量数据
                                {
                                    strBCSJ = "0";
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                                WriteLog("4.1");
                            }
                        }
                        else
                        {
                            if (strYCJL == "1" && cbJLLX.Text == "")
                            {
                                //if (txtLH.Text == "" && txtLH2.Text == "" && txtLH3.Text == "")
                                // {
                                WriteLog("5");
                                if (AddECJLData() == false)
                                {
                                    strBCSJ = "0";
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                                WriteLog("5.1");
                                // }
                            }
                            WriteLog("6");
                            if (cbJLLX.Text == "外协")
                            {
                                if (AddWXData() == false)
                                {
                                    strBCSJ = "0";
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                            WriteLog("6.1");
                            if (cbJLLX.Text.Trim() == "复磅")
                            {
                                if (strYCJL == "1")//判断一次计量是否是空
                                {
                                    if (UpdateYCJLData() == false)
                                    {
                                        strBCSJ = "0";
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                                if (strECJL == "1")
                                {
                                    if (UpdateECJLBData() == false)
                                    {
                                        strBCSJ = "0";
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                        }
                        //是否保存期限皮重
                        if (chbQXPZ.Checked == true)
                        {
                            //AddTPData();
                            if (SaveQXPZData() == false)
                            {
                                strBCSJ = "0";
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                }


                if (e_REWEIGHTFLAG == "1")
                {
                    DisPlayShowForFirst();
                }
                if (e_REWEIGHTFLAG != "1")
                {
                    //if (strYCJL == "" && strYB == "1" || chbQXPZ.Checked == true)
                    if (strYCJL == "")
                    {
                        WriteLog("8");
                        DisPlayShowForFirst();//一次计量完成后，液晶显示计量信息
                        WriteLog("8.1");
                    }
                    if (strYCJL == "1" && cbJLLX.Text == "")
                    {
                        WriteLog("9");
                        DisPlayShowForSecond();//二次计量完成后液晶显示计量信息
                        WriteLog("9.1");
                    }
                }

                //抓图线程
                m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                Invoke(m_MainThreadCapPicture); //用委托抓图

                //打印磅单
                //Print();


                //查询一次计量表
                WriteLog("10");
                m_BindUltraGridDelegate = new BindUltraGridDelegate(QueryYCBData);
                //Invoke(m_BindUltraGridDelegate);
                BeginInvoke(m_BindUltraGridDelegate);
                WriteLog("10.1");
                //panelYCSP.Visible = false;
                //曲线图表刷新
                dtQX.Rows.Clear();
                dtQX.Columns.Clear();
                ultraChart1.DataSource = dataTable6;
                ksht = 1;//开始画图标志，

                ClearControlData();//清空控件数据
                ClearControl();//清空控件内容
                ClearQXPZData();
                if (strYB == "1")
                {
                    ClearYBData();
                }

                WriteLog("11");

                //曲线图表刷新
                if (m_nPointCount > 0)
                {
                    for (int i = 0; i < m_nPointCount; i++)
                    {
                        if (m_PoundRoomArray[i].POINTNAME.Trim() == ultraGrid2.ActiveRow.Cells["FS_POINTNAME"].Value.ToString().Trim())
                        {
                            dtQX.Rows.Clear();
                            dtQX.Columns.Clear();
                            ultraChart1.DataSource = dataTable6;
                            BackZeroSign[i] = 1; //BackZeroSign = 1，意思就是车子在上称过程中如出现重量多次稳定，可以继续绘图；如是下称，则不准再画。
                        }
                    }
                }
                WriteLog("12");
                ifStart = "0";//保存后重新恢复启动
                cbJLLX.Enabled = true;
                //strZYBH = ""; //清除Guid

                //if (chb_Autocontrol.Checked == true)
                //{
                    //if (m_PoundRoomArray[m_iSelectedPound].POINTID == "K01" || m_PoundRoomArray[m_iSelectedPound].POINTID == "K02")
                    m_PoundRoomArray[m_iSelectedPound].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0x00, (byte)0);
                //}



                m_PoundRoomArray[m_iSelectedPound].ClearCardNoAndGuid();
                m_PoundRoomArray[m_iSelectedPound].CardNo = "";
                m_PoundRoomArray[m_iSelectedPound].ReaderGUID = "";
                txtZL.Text = "";


                WriteLog("13");
                if (strYCJL == "1")
                {
                    ClearYCBData();//清空一次计量数据
                }
                if (e_REWEIGHTFLAG == "1")
                {
                    ClearECJLBData();//清空两次计量数据
                }
                WriteLog("14");
                //pictureBox18.Image = BitmapToImage(new byte[1]); //车牌号一次计量图片
                //pictureBox18.Refresh();
                //panel20.Visible = false; //车牌号一次计量图片panel
                //panel22.BringToFront(); //磅单视频

                strBCSJ = "0";
                WriteLog("15");

                #region 自动播放语音

                m_AlarmVoicePath = Constant.RunPath + "\\sound\\称重完成.wav";

                WriteLog("准备语音播放...");
                if (System.IO.File.Exists(m_AlarmVoicePath))
                {

                    if (m_PoundRoomArray[m_iSelectedPound].Talk == true && m_PoundRoomArray[m_iSelectedPound].TalkID > 0)
                    {
                        m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_RealPlay(1,0,(int)picFDTP.Handle);
                        m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_StopTalk();
                        m_PoundRoomArray[m_iSelectedPound].TalkID = 0;
                        m_PoundRoomArray[m_iSelectedPound].Talk = false;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                    }

                    FileInfo fi = new FileInfo(m_AlarmVoicePath);
                    int waveTimeLen = Convert.ToInt32((fi.Length - 54) / 16 + 500);

                    if (m_PoundRoomArray[m_iSelectedPound].AUDIONUM > 0)
                    {

                        m_PoundRoomArray[m_iSelectedPound].AUDIONUM = 0;
                        WriteLog("开始语音播放,预计时间   " + waveTimeLen.ToString());
                        int reVal = m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SendData(m_AlarmVoicePath);

                        Thread.Sleep(waveTimeLen);
                        WriteLog("语音播放完成，播放字节  " + reVal.ToString());
                        if (reVal <= 0 && m_PoundRoomArray[m_iSelectedPound].ISFIRSTSEND == false)
                            reBootVideo();
                        m_PoundRoomArray[m_iSelectedPound].ISFIRSTSEND = false;
                        m_PoundRoomArray[m_iSelectedPound].AUDIONUM = 1;
                    }


                }
                #endregion
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                WriteLog("保存失败: " + ex.ToString());
            }

            
            //this.sendWC();调用计量完成的声音
            //this.pictureJP();调用截图
            //this.stopYP();
        }

        /// <summary>
        /// 线程语音播放
        /// </summary>
        public void AutoAlarmVoice()
        {
            FileInfo fi = new FileInfo(m_AlarmVoicePath);
            int waveTimeLen = Convert.ToInt32((fi.Length - 54) / 16 + 500);

            if (m_PoundRoomArray[m_iSelectedPound].AUDIONUM > 0)
            {
                m_PoundRoomArray[m_iSelectedPound].AUDIONUM = 0;

                int reVal = m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SendData(m_AlarmVoicePath);
                //Thread.Sleep(waveTimeLen);
                if (reVal <= 0 && m_PoundRoomArray[m_iSelectedPound].ISFIRSTSEND == false)
                    reBootVideo();
                m_PoundRoomArray[m_iSelectedPound].ISFIRSTSEND = false;
                m_PoundRoomArray[m_iSelectedPound].AUDIONUM = 1;
            }
        }

        /// <summary>
        /// 保存车号简称 比如:云A
        /// </summary>
        private void AddCarNO()
        {
            string s_CarNo = "";
            if (cbCH.Text.Trim() != "")
            {
                s_CarNo = cbCH.Text.Trim();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveCHData";
            ccp.ServerParams = new object[] { strJLDID, s_CarNo };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        /// <summary>
        /// 查询计量点信息
        /// </summary>
        private void QueryJLDData()
        {
            string strWhere = "SELECT 'FALSE' AS XZ,T.FS_POINTCODE,T.FS_POINTNAME,T.FS_POINTDEPART,T.FS_POINTTYPE,T.FS_VIEDOIP,T.FS_VIEDOPORT,T.FS_VIEDOUSER,T.FS_VIEDOPWD,";
            strWhere += " T.FS_METERTYPE,T.FS_METERPARA,T.FS_MOXAIP,T.FS_MOXAPORT,T.FS_RTUIP,T.FS_RTUPORT,T.FS_PRINTERIP,T.FS_PRINTERNAME,T.FS_PRINTTYPECODE,T.FN_USEDPRINTPAPER,";
            strWhere += " T.FN_USEDPRINTINK,T.FS_LEDIP,T.FS_LEDPORT,T.FN_VALUE,T.FS_ALLOWOTHERTARE,T.FS_SIGN,T.FS_DISPLAYPORT,T.FS_DISPLAYPARA,";
            strWhere += " T.FS_READERPORT,T.FS_READERPARA,T.FS_READERTYPE,T.FS_DISPLAYTYPE,T.FF_CLEARVALUE,T.FS_POINTSTATE FROM BT_POINT T ";
            strWhere += " WHERE T.FS_POINTTYPE = 'QC' ORDER BY T.FS_POINTCODE";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryJLDData";
            ccp.ServerParams = new object[] { strWhere };
            ccp.SourceDataTable = dataTable1;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid2);

            //InitPound(dataSet1.Tables[0]);
        }
        /// <summary>
        /// 查询语音播报信息并下载到本地
        /// </summary>
        private void QueryYYBBData()
        {
            //string strWhere = "select t.fs_voicename,t.fs_voicefile,t.FS_INSTRTYPE from bt_voice t where t.FS_INSTRTYPE = 'QC'";

            string strName = "";
            string strType = "QC";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.BaseTableManger";
            ccp.MethodName = "QueryVoiceTableData";
            ccp.ServerParams = new object[] { strName, strType };
            ccp.SourceDataTable = dataSet1.Tables["语音表"];
            dataTable3.Rows.Clear();
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ultraGrid5);

            for (int i = 0; i < dataTable3.Rows.Count; i++)
            {
                if (System.IO.File.Exists(stRunPath + "\\sound\\" + dataTable3.Rows[i]["FS_VOICENAME"].ToString().Trim()) == false)
                {
                    System.IO.File.WriteAllBytes(stRunPath + "\\sound\\" + dataTable3.Rows[i]["FS_VOICENAME"].ToString().Trim(), (byte[])dataTable3.Rows[i]["FS_VOICEFILE"]);
                }
            }
        }

        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultraGrid3_Click(object sender, EventArgs e)
        {
            UltraGridRow ugr;
            ugr = this.ultraGrid3.ActiveRow;
            if (ugr == null)
            {
                return;
            }
            ultraGrid3.ActiveRow.Selected = true;
        }

        /// <summary>
        /// 查询期限皮重表信息
        /// </summary>
        private void QueryQXPZData()
        {
            if (cbCH.Text == null)
            {
                MessageBox.Show("请勾选“保存期限皮重”");
                chbQXPZ.Focus();
                return;
            }
            string qxCH = "";
            if (cbCH.Text == null)
            {
                MessageBox.Show("请输入车号！");
                cbCH.Focus();
                return;
            }
            else
            {
                qxCH = cbCH.Text + cbCH1.Text;
            }
            DataTable dtQXPZ = new DataTable();


            string strDataState = m_PoundRoomArray[m_iSelectedPound].POINTSTATE;
            string strCurDateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = "select FN_TAREWEIGHT,FS_POINT,FS_PERSON,TO_CHAR(FD_DATETIME,'yyyy-MM-dd HH24:mi:ss')as FD_DATETIME,FS_SHIFT,FS_WEIGHTNO from DT_TERMTARE ";
            sql += " where FD_STARTTIME <= TO_DATE('" + strCurDateTime + "','yyyy-MM-dd HH24:mi:ss') and  FD_EndTime > TO_DATE('" + strCurDateTime + "','yyyy-MM-dd HH24:mi:ss')";
            sql += " and  FS_CARNO = '" + qxCH + "' and FS_DATASTATE = '" + strDataState + "'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dtQXPZ;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


            if (dtQXPZ.Rows.Count > 0)
            {
                //if (DialogResult.Yes == MessageBox.Show("该车有期限皮重,是否使用?", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                if (m_PoundRoomArray[m_iSelectedPound].POINTID == dtQXPZ.Rows[0]["FS_POINT"].ToString())
                {
                    strQXPZ = dtQXPZ.Rows[0]["FN_TAREWEIGHT"].ToString();
                    qxJLD = dtQXPZ.Rows[0]["FS_POINT"].ToString();
                    qxJLY = dtQXPZ.Rows[0]["FS_PERSON"].ToString();
                    qxJLSJ = dtQXPZ.Rows[0]["FD_DATETIME"].ToString();
                    qxBC = dtQXPZ.Rows[0]["FS_SHIFT"].ToString();
                    qxCZBH = dtQXPZ.Rows[0]["FS_WEIGHTNO"].ToString();
                    if (strYCJL == "")
                        strZYBH = dtQXPZ.Rows[0]["FS_WEIGHTNO"].ToString();

                    txtMZ.Text = txtXSZL.Text.Trim();
                    txtPZ.Text = strQXPZ;
                    txtJZ.Text = (Convert.ToDecimal(txtXSZL.Text.Trim()) - Convert.ToDecimal(strQXPZ)).ToString();
                }

            }
        }
        private void ClearQXPZData()
        {
            strQXPZ = "";
            qxJLD = "";
            qxJLY = "";
            qxJLSJ = "";
            qxBC = "";
            qxCZBH = "";
            txtMZ.Text = "";
            txtPZ.Text = "";
            txtJZ.Text = "";
        }
        /// <summary>
        /// 直接保存二次计量表数据
        /// </summary>
        private bool AddECJLBData()
        {
            string strECZL = "";//二次计量重量
            //if (ImageJZ != null)
            //{
            //    strECZL = ImageJZ;
            //}
            //else
            //{
            if (txtZL.Text.Trim() == "")
                strECZL = (Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
            else
                strECZL = (Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();

            m_ImageWeight = strECZL;
            //}
            string strJLD = strJLDID;
            string strJLY = txtJLY.Text.Trim();
            string strBC = txtBC.Text.Trim();

            string strMZ = "";//毛重
            string strPZ = "";//皮重
            string strJZ = "";//净重
            string strMZJLD = "";//毛重计量点
            string strMZJLY = "";//毛重计量员
            string strMZJLSJ = "";//毛重计量时间
            string strMZJLBC = "";//毛重计量班次
            string strPZJLD = "";//皮重计量点
            string strPZJLY = "";//皮重计量员
            string strPZJLSJ = "";//皮重计量时间
            string strPZJLBC = "";//皮重计量班次

            //add by luobin
            string strSenderPlace = ""; //发货地点
            string strReceiverPlace = ""; //卸货地点


            //string strWZBDTM = "";//完整磅单条码

            if (strECZL == "")
            {
                strECZL = "0";
            }
            if (Convert.ToDecimal(strECZL) > Convert.ToDecimal(strQXPZ))
            {
                strMZ = strECZL;
                strPZ = strQXPZ;
                Decimal JZ = Math.Round(Convert.ToDecimal(strMZ) - Convert.ToDecimal(strPZ), 3);
                strJZ = JZ.ToString();
                //strMZJLD = strJLD.ToString().Trim();
                strMZJLD = strJLDID;
                strMZJLY = strJLY.ToString().Trim();
                strMZJLSJ = "";
                strMZJLBC = strBC.ToString().Trim();

                strPZJLD = qxJLD;
                strPZJLY = qxJLY;
                strPZJLSJ = qxJLSJ;//皮重计量时间
                strPZJLBC = qxBC;

                GPJZ = strJZ; //钢坯净重
            }
            else
            {
                MessageBox.Show("毛重比皮重轻，请检查！");
                return false;
            }

            if (cbWLMC.SelectedValue == null || cbWLMC.SelectedValue.ToString() == "")
            {
            }
            else
            {
                ybCount[selectRow].strWLID = cbWLMC.SelectedValue.ToString().Trim();
                ybCount[selectRow].strWLMC = cbWLMC.Text.Trim();
            }

            if (cbFHDW.SelectedValue == null || cbFHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strFHFDM = cbFHDW.SelectedValue.ToString().Trim();
            }

            if (cbSHDW.SelectedValue == null || cbSHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strSHFDM = cbSHDW.SelectedValue.ToString().Trim();
            }

            if (cbCYDW.SelectedValue == null || cbCYDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strCYDW = cbCYDW.SelectedValue.ToString().Trim();
            }

            if (cbLX.SelectedValue != null)
            {
                if (cbLX.Text != sLX)
                {
                    ybCount[selectRow].strLX = cbLX.SelectedValue.ToString().Trim();
                }
            }

            //add by luobin
            print.printMZ = string.Format("{0:F3}", Convert.ToDecimal(strMZ));
            print.printPZ = string.Format("{0:F3}", Convert.ToDecimal(strPZ));
            print.printJZ = string.Format("{0:F3}", Convert.ToDecimal(strJZ));

            strSenderPlace = this.tbSenderPlace.Text.Trim();
            strReceiverPlace = this.tbReceiverPlace.Text.Trim();
            string strProvider = "";
            if (cbProvider.SelectedValue != null)
                strProvider = cbProvider.SelectedValue.ToString();
            string strBZ = tbBZ.Text.Trim();
            //strMZJLSJ = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            strZYBH = Guid.NewGuid().ToString().Trim();


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveECJLBData";
            ccp.ServerParams = new object[] { strZYBH, ybCount[selectRow].strHTH, ybCount[selectRow].strHTXMH, ybCount[selectRow].strLH, ybCount[selectRow].strZS,
                ybCount[selectRow].strCZH, cbCH.Text.Trim() + cbCH1.Text.Trim(), ybCount[selectRow].strWLID, ybCount[selectRow].strWLMC, ybCount[selectRow].strFHFDM,
                ybCount[selectRow].strCYDW, ybCount[selectRow].strSHFDM, ybCount[selectRow].strLX, ybCount[selectRow].strYBZZ, ybCount[selectRow].strYBPZ, 
                ybCount[selectRow].strYBJZ, strMZ, strMZJLD, strMZJLY, strMZJLSJ, strMZJLBC, strPZ, strPZJLD, strPZJLY, strPZJLSJ, strPZJLBC, strJZ, qxCZBH, 
                y_IFSAMPLING, ybCount[selectRow].strQXPZBZ, y_IFACCEPT, txtPJBH.Text,strSenderPlace,strReceiverPlace,strCode,strProvider,strBZ};
            ccp.IfShowErrMsg = false;

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            WriteLog("二次计量保存 计量号:" + strZYBH + "  车号: " + cbCH.Text.Trim() + cbCH1.Text.Trim() + "  返回代码 ：" + ccp.ReturnCode.ToString() + "     返回信息 ：" + ccp.ReturnInfo);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }


            btnBC.Enabled = false;
            return true;
            //UltValuesShow();
        }

        /// <summary>
        /// 复磅修改二次计量表
        /// </summary>
        private bool UpdateECJLBData()
        {
            e_GROSSWEIGHT = "";//毛重重量
            e_TAREWEIGHT = "";//皮重重量
            //if (ImageJZ != null)
            //{
            //    e_GROSSWEIGHT = ImageJZ;
            //}
            //else
            //{
            string strSQL = "select fd_grossdatetime ,fn_grossweight, fd_taredatetime ,fn_tareweight ,fd_ecjlsj from dt_carweight_weight ";
            strSQL += " where fs_cardnumber = '" + this.txtCZH.Text.Trim() + "' ";
            strSQL += " order by fd_ecjlsj desc";
            CoreClientParam ccp1 = new CoreClientParam();
            ccp1.ServerName = "ygjzjl.car.CarCard";
            ccp1.MethodName = "queryByClientSql";
            ccp1.ServerParams = new object[] { strSQL };
            System.Data.DataTable dt = new System.Data.DataTable();
            ccp1.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp1, CoreInvokeType.Internal);
            string grossdatetime = "";
            string taredatetime = "";
            string ecjlsj = "";
            string strJZ = "";
            string strYKL = "";
            string strKHZL = "";

            if (dt.Rows.Count > 0)
            {
                grossdatetime = dt.Rows[0]["fd_grossdatetime"].ToString();
                taredatetime = dt.Rows[0]["fd_taredatetime"].ToString();
                ecjlsj = dt.Rows[0]["fd_ecjlsj"].ToString();
                if (grossdatetime == ecjlsj)
                {
                    if (txtZL.Text.Trim() == "")
                        e_GROSSWEIGHT = (Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
                    else
                        e_GROSSWEIGHT = (Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();

                    m_ImageWeight = e_GROSSWEIGHT;
                    e_TAREWEIGHT = dt.Rows[0]["fn_tareweight"].ToString();
                }
                if (taredatetime == ecjlsj)
                {
                    if (txtZL.Text.Trim() == "")
                        e_TAREWEIGHT = (Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();
                    else
                        e_TAREWEIGHT = (Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]).ToString();

                    m_ImageWeight = e_TAREWEIGHT;
                    e_GROSSWEIGHT = dt.Rows[0]["fn_grossweight"].ToString();

                }
                Decimal JZ = Math.Round(Convert.ToDecimal(e_GROSSWEIGHT) - Convert.ToDecimal(e_TAREWEIGHT), 3);
                strJZ = JZ.ToString();

                //扣渣
                if (strYKL != "" && strYKL != "0")
                {
                    decimal YKL = Convert.ToDecimal(strYKL);
                    strKHZL = (Convert.ToDecimal(JZ) - YKL).ToString();
                }
                else if (s_YKBL != "")
                {
                    decimal KHJZ = Math.Round(Convert.ToDecimal(JZ) - Convert.ToDecimal(JZ) * Convert.ToDecimal(s_YKBL), 3);
                    strKHZL = KHJZ.ToString();
                }
                else
                {
                    strKHZL = JZ.ToString();
                }
            }


            string strProvider = "";
            if (cbProvider.SelectedValue != null)
                strProvider = cbProvider.SelectedValue.ToString();
            string strBZ = tbBZ.Text.Trim();
            //}
            e_NETWEIGHT = Math.Abs(Convert.ToDecimal(e_GROSSWEIGHT) - Convert.ToDecimal(e_TAREWEIGHT)).ToString();//净重
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdateECJLBData";
            ccp.ServerParams = new object[] { e_WEIGHTNO, e_CONTRACTNO, e_CONTRACTITEM, e_STOVENO, e_COUNT, e_CARDNUMBER, e_CARNO, e_MATERIAL, e_MATERIALNAME, 
                e_SENDER, e_TRANSNO, e_RECEIVER, e_SENDGROSSWEIGHT, e_SENDTAREWEIGHT, e_SENDNETWEIGHT, e_WEIGHTTYPE, e_GROSSWEIGHT, e_GROSSPOINT, e_GROSSPERSON, 
                e_GROSSDATETIME, e_GROSSSHIFT, e_TAREWEIGHT, e_TAREPOINT, e_TAREPERSON, e_TAREDATETIME, e_TARESHIFT, e_FIRSTLABELID, e_FULLLABELID, e_NETWEIGHT, 
                e_SAMPLEPERSON, e_YKL, e_SAMPLETIME, e_SAMPLEPLACE, e_SAMPLEFLAG, e_DRIVERNAME, e_DRIVERIDCARD, e_SENDERSTORE, e_IFSAMPLING, e_IFACCEPT, 
                e_IFUNLOAD, e_REWEIGHTFLAG, e_REWEIGHTTIME, e_REWEIGHTPLACE, e_REWEIGHTPERSON, txtPJBH.Text,strProvider,strBZ ,strKHZL};

            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 回车键控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeighMeasureInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Control c = GetNextControl(this.ActiveControl, true);
                bool ok = SelectNextControl(this.ActiveControl, true, true, true, true);
                if (ok && c != null)
                {
                    if (c is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)c).SelectAll();
                    }
                }
            }
        }
        /// <summary>
        /// 控件不能为空判断
        /// </summary>
        private bool NotEmpty()
        {
            if (cbCH.Text == "")
            {
                MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCH.Focus();
                return false;
            }
            if (cbCH1.Text == "")
            {
                MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCH1.Focus();
                return false;
            }
            if (cbWLMC.Text == "")
            {
                MessageBox.Show("物料不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbWLMC.Focus();
                return false;
            }
            if (cbLX.Text == "")
            {
                MessageBox.Show("流向不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLX.Focus();
                return false;
            }
            if (cbCYDW.Text == "")
            {
                MessageBox.Show("承运单位不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCYDW.Focus();
                return false;
            }
            if (cbFHDW.Text == "")
            {
                MessageBox.Show("发货单位不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFHDW.Focus();
                return false;
            }
            if (cbSHDW.Text == "")
            {
                MessageBox.Show("收货单位不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSHDW.Focus();
                return false;
            }

            return true;
        }
        /// <summary>
        /// 打印参数初始化
        /// </summary>
        private void printInfoClear()
        {
            print.printCZH = ""; //车证卡号
            print.printCH = ""; //车号
            print.printHTH = ""; //合同号
            print.printWLMC = ""; //物料名称
            print.printFHDW = ""; //发货单位
            print.printSHDW = ""; //收货单位
            print.printCYDW = ""; //承运单位
            print.printPZ = "";  //打印皮重
            print.printMZ = "";  //打印毛重
            print.printJZ = "";  //打印净重
            print.printJLLX = "";  //计量类型
            print.printJLD = "";  //计量点
            print.printJLY = "";  //计量员

            //print.pringJLCS = ""; //计量次数
            print.printCS = "0"; //计量标志

            print.printLH = ""; //炉号
            print.printZS = ""; //支数

            print.printLH1 = ""; //炉号1
            print.printZS1 = ""; //支数1
            print.printLH2 = ""; //炉号2
            print.printZS2 = ""; //支数2
            print.printLH3 = ""; //炉号3
            print.printZS3 = ""; //支数3
            print.printAdviseSpec = ""; //建议轧制规格
            print.printZZJY = "";
            print.printYKBL = "";
            print.printYKL = "";

            strCode = "";
        }
        /// <summary>
        /// 将全角转换为半角
        /// </summary>
        /// <param name="newPressChar"></param>
        /// <returns></returns>
        private string ChangeHalfCode(Char newPressChar)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(newPressChar.ToString());
            int aaa = Convert.ToInt32(bytes[0]);
            int bbb = Convert.ToInt32(bytes[1]);

            int cccc = bbb * 256 + aaa;
            if (cccc >= 65281 && cccc <= 65374)
            {
                bytes[1] = 0;
                bytes[0] = Convert.ToByte(cccc - 65248);

                string newChar = System.Text.Encoding.Unicode.GetString(bytes);
                return newChar;
            }
            else if (cccc == 12290)
            {
                return ".";
            }
            else
            {
                return newPressChar.ToString();
            }
        }
        #endregion

        #region 插入新的基础数据
        /// <summary>
        /// 往表中插入新的物料信息
        /// </summary>
        private void SaveWLData()
        {
            string inPointID = "";
            inPointID = m_PoundRoomArray[m_iSelectedPound].POINTID;
            //if (strYB != "")
            //{
            //    inPointID = ybCount[selectRow].strBF;
            //}
            //if (strYCJL != "")
            //{ 
            //    inPointID = strBFBH; 
            //}
            //if (inPointID == "")
            //{
            //    inPointID = txtJLD.Text.Trim();
            //}
            string inWLMC = this.cbWLMC.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveWLData";
            ccp.ServerParams = new object[] { inPointID, inWLMC, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sWLID = ccp.ReturnObject.ToString();
        }
        /// <summary>
        /// 往表中插入新的发货单位信息
        /// </summary>
        private void SaveFHDWData()
        {
            string inPointID = "";
            //if (strYB != "")
            //{
            inPointID = m_PoundRoomArray[m_iSelectedPound].POINTID;
            //}
            //if (strYCJL != "")
            //{
            //    inPointID = strBFBH;
            //}
            string inFHDW = this.cbFHDW.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveFHDWData";
            ccp.ServerParams = new object[] { inPointID, inFHDW, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sFHDWID = ccp.ReturnObject.ToString();
        }
        /// <summary>
        /// 往表中插入新的收货单位信息
        /// </summary>
        private void SaveSHDWData()
        {
            string inPointID = "";
            //if (strYB != "")
            //{
            inPointID = m_PoundRoomArray[m_iSelectedPound].POINTID;
            //}
            //if (strYCJL != "")
            //{
            //    inPointID = strBFBH;
            //}
            string inSHDW = this.cbSHDW.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveSHDWData";
            ccp.ServerParams = new object[] { inPointID, inSHDW, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sSHDWID = ccp.ReturnObject.ToString();
        }
        /// <summary>
        /// 往表中插入新的承运单位信息
        /// </summary>
        private void SaveCYDWData()
        {
            string inPointID = "";
            //if (strYB != "")
            //{
            inPointID = m_PoundRoomArray[m_iSelectedPound].POINTID;
            //}
            //if (strYCJL != "")
            //{
            //    inPointID = strBFBH;
            //}
            string inCYDW = this.cbCYDW.Text.Trim();
            string inFrom = "SGLR";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveCYDWData";
            ccp.ServerParams = new object[] { inPointID, inCYDW, inFrom };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            sCYDWID = ccp.ReturnObject.ToString();
        }
        #endregion

        #region Toolbar事件控制方法
        /// <summary>
        /// 磅单打印
        /// </summary>
        private void PrintBDData()
        {
            int ZZ = 0;
            int TDL = 0;
            string sZZ = "";
            string sTDL = "";

            ZZ = Convert.ToInt32(txtZZ.Text.Trim()) - 1;
            //TDL = Convert.ToInt32(txtTDL.Text.Trim()) - 1;

            sZZ = ZZ.ToString();
            sTDL = TDL.ToString();

            txtZZ.Text = sZZ;
            txtTDL.Text = sTDL;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdateZZTDLData";
            ccp.ServerParams = new object[] { strJLDID, sZZ, sTDL };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 纸张碳带量数据查询
        /// </summary>
        private void QueryZZTDLData()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryZZTDLData";
            ccp.ServerParams = new object[] { strJLDID };
            DataTable dtZZ = new DataTable();
            ccp.SourceDataTable = dtZZ;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            //获取服务端查询出来的单个值
            //string aZZ = (((ccp.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FN_USEDPRINTPAPER"]as System.Collections.Hashtable)["value"].ToString();
            //string aTDL = (((ccp.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FN_USEDPRINTINK"]as System.Collections.Hashtable)["value"].ToString();
            //float b = Convert.ToSingle((((ccp.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FN_USEDPRINTINK"] as System.Collections.Hashtable)["value"]);
            if (dtZZ.Rows.Count > 0)
            {
                string aZZ = dtZZ.Rows[0][0].ToString();
                txtZZ.Text = aZZ;
                //txtTDL.Text = aTDL;
            }
        }
        /// <summary>
        /// 换纸
        /// </summary>
        private void ClearHZ()
        {
            //string cHZ = "800";
            //string cHM = txtTDL.Text.Trim();
            //CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            //ccp.MethodName = "UpdateZZTDLData";
            //ccp.ServerParams = new object[] { strJLDID, cHZ, cHM };

            //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //txtZZ.Text = "800";

            string strSql = "SELECT A.FS_PRINTTYPECODE,A.FS_PRINTTYPEDESCRIBE,A.FN_PAPERNUM FROM BT_PRINTTYPE A WHERE A.FS_PRINTTYPECODE = '" + m_PoundRoomArray[m_iSelectedPound].PRINTTYPECODE + "'";
            CoreClientParam ccpHZ = new CoreClientParam();
            ccpHZ.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpHZ.MethodName = "QueryZZData";
            ccpHZ.ServerParams = new object[] { strSql };
            DataTable dtZZ = new DataTable();
            ccpHZ.SourceDataTable = dtZZ;

            this.ExecuteQueryToDataTable(ccpHZ, CoreInvokeType.Internal);
            if (dtZZ.Rows.Count > 0)
            {
                string aZZ = dtZZ.Rows[0][0].ToString().Trim();
                txtZZ.Text = aZZ;
            }
        }
        /// <summary>
        /// 换墨
        /// </summary>
        private void ClearHM()
        {
            string cHZ = txtZZ.Text.Trim();
            string cHM = "0";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdateZZTDLData";
            ccp.ServerParams = new object[] { strJLDID, cHZ, cHM };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            txtTDL.Text = "0";
        }
        /// <summary>
        /// 查看或关闭一次计量图片
        /// </summary>
        private void QueryAndCloseYCPic()
        {
            if (txtCZH.Text.Trim().Length <= 0 && this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption == "关闭一次计量图像")
            {
                panelYCJL.Visible = false;
                this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption = "查看一次计量图像";
                imagebytes1 = null;
                imagebytes2 = null;
                imagebytes3 = null;
                imagebytes4 = null;
                imagebytes5 = null;
                imagebytes6 = null;
                return;
            }
            if (strYCJL == "" && e_REWEIGHTFLAG != "1")
            {
                MessageBox.Show("还没有一次计量图像！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption == "关闭一次计量图像")
            {
                panelYCJL.Visible = false;
                this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption = "查看一次计量图像";
                imagebytes1 = null;
                imagebytes2 = null;
                imagebytes3 = null;
                imagebytes4 = null;
                imagebytes5 = null;
                imagebytes6 = null;
                return;
            }

            DataTable dtImage = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryYCTXData";
            //Object[] args = new Object[] { strZYBH };
            ccp.ServerParams = new Object[] { strZYBH };
            ccp.SourceDataTable = dtImage;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            //1
            if (dtImage.Rows.Count > 0)
            {
                imagebytes1 = (byte[])dtImage.Rows[0]["FB_IMAGE1"];

                intPictureBoxWidth = pictureBox15.Width;
                intPictureBoxHeight = pictureBox15.Height;
                //m_imagebytes = imagebytes1;
                pictureBox15.Image = BitmapToImage(imagebytes1);
                //测试用
                //MemoryStream stream = new MemoryStream(m_imagebytes, true); // 创建一个内存流，支持写入，用于存放图片二进制数据 
                //stream.Write(m_imagebytes, 0, m_imagebytes.Length);
                //Bitmap FinalImage = new Bitmap(stream);
                //pictureBox15.Image = FinalImage;
                //2
                imagebytes2 = (byte[])dtImage.Rows[0]["FB_IMAGE2"];

                intPictureBoxWidth = pictureBox14.Width;
                intPictureBoxHeight = pictureBox14.Height;
                pictureBox14.Image = BitmapToImage(imagebytes2);
                //3
                imagebytes3 = (byte[])dtImage.Rows[0]["FB_IMAGE3"];

                intPictureBoxWidth = pictureBox13.Width;
                intPictureBoxHeight = pictureBox13.Height;
                pictureBox13.Image = BitmapToImage(imagebytes3);
                //4
                imagebytes4 = (byte[])dtImage.Rows[0]["FB_IMAGE4"];

                intPictureBoxWidth = pictureBox12.Width;
                intPictureBoxHeight = pictureBox12.Height;
                pictureBox12.Image = BitmapToImage(imagebytes4);
                //5
                imagebytes5 = (byte[])dtImage.Rows[0]["FB_IMAGE5"];

                intPictureBoxWidth = pictureBox11.Width;
                intPictureBoxHeight = pictureBox11.Height;
                pictureBox11.Image = BitmapToImage(imagebytes5);
                //6
                imagebytes6 = (byte[])dtImage.Rows[0]["FB_IMAGE6"];

                intPictureBoxWidth = pictureBox10.Width;
                intPictureBoxHeight = pictureBox10.Height;
                pictureBox10.Image = BitmapToImage(imagebytes6);

                string[] curveImage = dtImage.Rows[0]["FS_CURVEIMAGEONE"].ToString().Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries); //一次计量动态图片数据
                DataTable dtCurveImage = new DataTable();

                for (int i = 0; i < curveImage.Length; i++)
                {
                    DataColumn dc = new DataColumn("ZL" + i, typeof(decimal));
                    dtCurveImage.Columns.Add(dc);

                    if (i == 0)
                    {
                        DataRow dr = dtCurveImage.NewRow();
                        dr[0] = curveImage[i].ToString();
                        dtCurveImage.Rows.Add(dr);
                    }
                    else
                    {
                        dtCurveImage.Rows[0][i] = curveImage[i].ToString();
                    }
                }
                ultraChart2.DataSource = dtCurveImage;

                if (this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption == "查看/关闭一次计量图像")
                {
                    panelYCJL.Visible = true;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption = "关闭一次计量图像";
                    return;
                }
                if (this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption == "查看一次计量图像")
                {
                    panelYCJL.Visible = true;
                    this.ultraToolbarsManager1.Toolbars[0].Tools["YCJLTX"].SharedProps.Caption = "关闭一次计量图像";
                    return;
                }
            }
        }
        /// <summary>
        /// 系统自动查询一次计量车牌号图片
        /// </summary>
        private void QueryYCPic()
        {
            DataTable dtImage = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryYCTXData";

            ccp.ServerParams = new Object[] { strZYBH };
            ccp.SourceDataTable = dtImage;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dtImage.Rows.Count > 0)
            {
                imagebytes1 = (byte[])dtImage.Rows[0]["FB_IMAGE1"];

                intPictureBoxWidth = VideoChannel4.Width;
                intPictureBoxHeight = VideoChannel4.Height;

                //panel20.Visible = true;
                //panel20.BringToFront();

                //pictureBox18.Image = BitmapToImage(imagebytes1);
                imagebytes1 = null;
            }
        }
        private void pictureBox15_DoubleClick(object sender, EventArgs e)
        {
            //方法一：
            //picFDTP.Visible = true;
            //picFDTP.Width = pictureBox15.Width * 2;
            //picFDTP.Height = pictureBox15.Height * 2;

            //intPictureBoxWidth = picFDTP.Width;
            //intPictureBoxHeight = picFDTP.Height;

            //picFDTP.Image = BitmapToImage(imagebytes1);

            //方法二
            picFDTP.Visible = true;
            picFDTP.Width = pictureBox15.Width * 2;
            picFDTP.Height = pictureBox15.Height * 2;

            int Width = picFDTP.Width;
            int Height = picFDTP.Height;

            getImage.BitmapToImage(imagebytes1, picFDTP, Width, Height);

            ////方法三
            //picFDTP.Visible = true;
            //picFDTP.Width = pictureBox15.Width * 2;
            //picFDTP.Height = pictureBox15.Height * 2;

            //int Width = picFDTP.Width;
            //int Height = picFDTP.Height;
            //BaseInfo getImage = new BaseInfo();
            //string strID = "d1fec9d8-72ce-4833-b6bd-294bba15ef72";
            //getImage.QueryQCImage(strID);
            //DataTable dtTP = getImage.dtImage;

            //byte[] imagebytes12 = (byte[])dtTP.Rows[0]["FB_IMAGE1"];
            //getImage.BitmapToImage(imagebytes12, picFDTP, Width, Height);
        }

        #endregion

        #region Bitmap转换成Image
        //Bitmap转换成Image,等比例缩放功能
        public bool ThumbnailCallback()
        {
            return false;
        }
        private Image BitmapToImage(byte[] imagebytes)
        {
            MemoryStream stream = new MemoryStream(imagebytes, true); // 创建一个内存流，支持写入，用于存放图片二进制数据 
            Bitmap FinalImage;

            if (imagebytes[0] == 0)
            {
                FinalImage = null;
            }
            else
            {
                stream.Write(imagebytes, 0, imagebytes.Length);
                FinalImage = new Bitmap(stream);
            }
            if (FinalImage == null)
            {
                return null;
            }
            Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            int intOriginalWidth = FinalImage.Width;
            int intOriginalHeight = FinalImage.Height;
            int intNewWidth = FinalImage.Width;
            int intNewHeight = FinalImage.Height;

            if (intOriginalWidth <= (int)intPictureBoxWidth && intOriginalHeight <= (int)intPictureBoxHeight)
            {
                //宽和高都不大于intPictureBoxWidth和intPictureBoxHeight
                if (intPictureBoxWidth / intOriginalWidth > intPictureBoxHeight / intOriginalHeight)
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                    //宽大于PictureBox控件的宽，再按比例缩放
                    if (intNewWidth > intPictureBoxWidth)
                    {
                        intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intNewHeight) * intPictureBoxWidth / Convert.ToDecimal(intNewWidth), 0));
                        intNewHeight = intPictureBoxWidth;
                    }
                }
                else
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));

                    //高大于PictureBox控件的高，再按比例缩放
                    if (intNewHeight > intPictureBoxHeight)
                    {
                        //intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intNewWidth) * intPictureBoxHeight / Convert.ToDecimal(intNewHeight), 0));
                        intNewWidth = intPictureBoxWidth;
                        intNewHeight = intPictureBoxHeight;
                    }
                }

            }
            else if (intOriginalWidth > (int)intPictureBoxWidth && Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0) <= (int)intPictureBoxHeight)
            {
                //宽大于intPictureBoxWidth且高等比例缩放后不大于intPictureBoxHeight
                intNewWidth = (int)intPictureBoxWidth;
                intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
            }
            else if (intOriginalHeight > (int)intPictureBoxHeight && Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0) <= (int)intPictureBoxWidth)
            {
                //高大于intPictureBoxHeight且宽等比例缩放后不大于intPictureBoxWidth
                intNewHeight = (int)intPictureBoxHeight;
                intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
            }
            else
            {
                //否则以缩放比例大的缩放高和宽
                if (intPictureBoxWidth / intOriginalWidth > intPictureBoxHeight / intOriginalHeight)
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxHeight / Convert.ToDecimal(intOriginalHeight), 0));
                }
                else
                {
                    intNewHeight = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalHeight) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
                    intNewWidth = Convert.ToInt32(Math.Round(Convert.ToDecimal(intOriginalWidth) * intPictureBoxWidth / Convert.ToDecimal(intOriginalWidth), 0));
                }
            }
            Image myImage = FinalImage.GetThumbnailImage(intNewWidth, intNewHeight, myCallback, IntPtr.Zero);
            return myImage;
        }
        #endregion

        #region 线程控制方法
        /// <summary>
        /// 线程控制添加基础信息次数+1
        /// </summary>
        private void AddCSData()
        {
            //物料
            string WLID = "";
            //if (cbWLMC.SelectedValue == null)
            //{
            //WLID = sWLID;
            //}
            //else
            //{
            //WLID = cbWLMC.SelectedValue.ToString().Trim();
            //}
            //int WLCS = 1;
            if (WLMC == "1")
            {
                WLID = sWLID;
            }
            else
            {
                WLID = ybCount[selectRow].strWLID;
            }
            CoreClientParam ccpWL = new CoreClientParam();
            ccpWL.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpWL.MethodName = "AddWLCSData";
            ccpWL.ServerParams = new object[] { strJLDID, WLID };

            this.ExecuteNonQuery(ccpWL, CoreInvokeType.Internal);

            //发货单位
            string FHDWID = "";
            if (FHDWMC == "1")
            {
                FHDWID = sFHDWID;
            }
            else
            {
                FHDWID = ybCount[selectRow].strFHFDM;
            }
            CoreClientParam ccpFHDW = new CoreClientParam();
            ccpFHDW.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpFHDW.MethodName = "AddFHDWCSData";
            ccpFHDW.ServerParams = new object[] { strJLDID, FHDWID };

            this.ExecuteNonQuery(ccpFHDW, CoreInvokeType.Internal);
            //收货单位
            string SHDWID = "";
            if (SHDWMC == "1")
            {
                SHDWID = sSHDWID;
            }
            else
            {
                SHDWID = ybCount[selectRow].strSHFDM;
            }
            CoreClientParam ccpSHDW = new CoreClientParam();
            ccpSHDW.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpSHDW.MethodName = "AddSHDWCSData";
            ccpSHDW.ServerParams = new object[] { strJLDID, SHDWID };

            this.ExecuteNonQuery(ccpSHDW, CoreInvokeType.Internal);
            //承运单位
            string CYDWID = "";
            if (CYDWMC == "1")
            {
                CYDWID = sCYDWID;
            }
            else
            {
                CYDWID = ybCount[selectRow].strCYDW;
            }
            CoreClientParam ccpCYDW = new CoreClientParam();
            ccpCYDW.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpCYDW.MethodName = "AddCYDWCSData";
            ccpCYDW.ServerParams = new object[] { strJLDID, CYDWID };

            this.ExecuteNonQuery(ccpCYDW, CoreInvokeType.Internal);
        }
        /// <summary>
        /// 关闭窗体时，关闭线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeighMeasureInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        /// <summary>
        /// 声音报警线程
        /// </summary>
        private void SoundPlayMethod()
        {
            if (!File.Exists(Constant.RunPath + "\\请报车号.wav"))
            {
                return;
            }
            FileStream fs = new FileStream(Constant.RunPath + "\\请报车号.wav", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            SoundPlayer sp = new SoundPlayer(fs);

            while (s_run)
            {
                Thread.Sleep(1 * 1000);

                try
                {
                    sp.Play();
                    //Thread.Sleep(1 * 500);
                    //sp.Play();
                    //Thread.Sleep(500);
                    //sp.Play();
                    //Thread.Sleep(500);
                    //sp.Play();
                    //Thread.Sleep(500);
                    //sp.Play();
                    //Thread.Sleep(500);
                    //sp.Play();
                    break;
                }
                catch (System.Exception ex)
                {

                }
                finally { }
            }
        }

        private void ultraGrid5_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Key.ToUpper() != "FS_VOICENAME" || e.Cell.Value.ToString().Length == 0)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            if (m_iSelectedPound < 0)
            {
                MessageBox.Show("请先选择一个计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }
            if (ultraGrid5.Rows.Count <= 0)
            {
                MessageBox.Show("还没有声音文件，请添加声音文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
                return;
            }

            int i = m_iSelectedPound;

            //if (m_PoundRoomArray[i].VideoRecord == null)
            //{
            //    MessageBox.Show("硬盘录像机未连接，不能发送语音！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (m_PoundRoomArray[i].VideoRecord == null || m_PoundRoomArray[i].VideoHandle <= 0)
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (m_PoundRoomArray[i].Talk == true && m_PoundRoomArray[i].TalkID > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_iSelectedPound);
                m_PoundRoomArray[i].VideoRecord.SDK_StopTalk();
                m_PoundRoomArray[i].TalkID = 0;
                m_PoundRoomArray[i].Talk = false;

                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }

            FileInfo fi = new FileInfo(Constant.RunPath + "\\sound\\" + ultraGrid5.ActiveRow.Cells["FS_VOICENAME"].Value.ToString().Trim());
            int waveTimeLen = Convert.ToInt32((fi.Length - 54) / 16 + 500);

            if (m_PoundRoomArray[i].AUDIONUM > 0)
            {
                m_PoundRoomArray[i].AUDIONUM = 0;
                int reVal = m_PoundRoomArray[i].VideoRecord.SDK_SendData(Constant.RunPath + "\\sound\\" + ultraGrid5.ActiveRow.Cells["FS_VOICENAME"].Value.ToString().Trim());
                Thread.Sleep(waveTimeLen);
                if (reVal <= 0 && m_PoundRoomArray[m_iSelectedPound].ISFIRSTSEND == false)
                    reBootVideo();
                m_PoundRoomArray[m_iSelectedPound].ISFIRSTSEND = false;
                m_PoundRoomArray[i].AUDIONUM = 1;
            }

            this.Cursor = Cursors.Default;
            txtYKL.Focus();
          
        }

        /// <summary>
        /// 写日志信息到日志文件
        /// </summary>
        /// <param name="str"></param>
        private void WriteLog(string str)
        {
            if (System.IO.Directory.Exists(Constant.RunPath + "\\log") == false)
            {
                System.IO.Directory.CreateDirectory(Constant.RunPath + "\\log");
            }

            string strDate = System.DateTime.Now.ToString("yyyyMMdd");
            System.IO.TextWriter tw = new System.IO.StreamWriter(Constant.RunPath + "\\log\\QCWXHDataCollect_" + strDate + ".log", true);

            tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            tw.WriteLine(str);
            tw.WriteLine("\r\n");
            tw.Close();
        }
        #endregion

        #region 钢坯转移查询
        private void QueryGPData()
        {
            string strLH = txtLH.Text.Trim();
            if (strLH.Length == 9) //方坯
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccp.MethodName = "QueryGPFPData";
                ccp.ServerParams = new object[] { strLH };
                DataTable dtDDH = new DataTable();
                ccp.SourceDataTable = dtDDH;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dtDDH.Rows.Count > 0)
                {
                    sDDH = dtDDH.Rows[0]["FS_GP_ORDERNO"].ToString().Trim();
                    sCountZS = dtDDH.Rows[0]["FN_GP_TOTALCOUNT"].ToString().Trim();
                }
                else
                {
                    MessageBox.Show("没有找到对应的合同号，请检查炉号是否正确！");
                    sDDH = txtHTH.Text.Trim();
                    //return;
                }
            }
          

            sDDH = txtHTH.Text;
            //txtHTH.Text = sDDH;
            //SAP物料ID
            CoreClientParam ccpWLID = new CoreClientParam();
            ccpWLID.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpWLID.MethodName = "QueryWLIDData";
            ccpWLID.ServerParams = new object[] { sDDH };
            DataTable dtSAPWL = new DataTable();
            ccpWLID.SourceDataTable = dtSAPWL;
            this.ExecuteQueryToDataTable(ccpWLID, CoreInvokeType.Internal);
            string sapWLID = "";
            if (dtSAPWL.Rows.Count > 0)
            {
                sapWLID = dtSAPWL.Rows[0]["FS_MATERIAL"].ToString().Trim();
                txtHTXMH.Text = dtSAPWL.Rows[0]["FS_ITEMNO"].ToString().Trim();
            }
            //string sapWLID = (((ccpWLID.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_MATERIAL"] as System.Collections.Hashtable)["value"].ToString();
            //txtHTXMH.Text = (((ccpWLID.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_ITEMNO"] as System.Collections.Hashtable)["value"].ToString();
            ////通过SAP物料ID查询计量的物料ID（如果找不到就要去SAP下载）
            //CoreClientParam ccpWL = new CoreClientParam();
            //ccpWL.ServerName = "ygjzjl.car.WeighMeasureInfo";
            //ccpWL.MethodName = "QueryWLData";
            //ccpWL.ServerParams = new object[] { sapWLID };
            //DataTable dtjlWLID = new DataTable();
            //ccpWL.SourceDataTable = dtjlWLID;

            //this.ExecuteQueryToDataTable(ccpWL, CoreInvokeType.Internal);

            //if (dtjlWLID.Rows.Count <= 0)
            //{
            //    //从SAP下载物料
            //    string rfcfunc = "ZJL_MATERIALEXTRA_DOWN";//sap方法名
            //    CoreClientParam ccpSAPXZ = new CoreClientParam();
            //    ccpSAPXZ.ServerName = "Core.KgMcms.Sap.DownloadSapRfc";
            //    ccpSAPXZ.MethodName = "down_IT_MATERIAL";
            //    ccpSAPXZ.ServerParams = new object[] { rfcfunc, sapWLID };
            //    this.ExecuteNonQuery(ccpSAPXZ, CoreInvokeType.Internal);
            //    //下载完后，重新查询，找计量物料ID
            //    CoreClientParam ccpWL1 = new CoreClientParam();
            //    ccpWL1.ServerName = "ygjzjl.car.WeighMeasureInfo";
            //    ccpWL1.MethodName = "QueryWLIDData";
            //    ccpWL1.ServerParams = new object[] { sapWLID };
            //    DataTable dtjlWLID1 = new DataTable();
            //    ccpWL1.SourceDataTable = dtjlWLID1;
            //    this.ExecuteQueryToDataTable(ccpWL1, CoreInvokeType.Internal);
            //    if (dtjlWLID1.Rows.Count > 0)
            //    {
            //        jlWLID = dtjlWLID1.Rows[0]["FS_WL"].ToString().Trim();
            //        jlWLMC = dtjlWLID1.Rows[0]["FS_MATERIALNAME"].ToString().Trim();
            //        //jlWLID = (((ccpWL1.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_WL"] as System.Collections.Hashtable)["value"].ToString();
            //        //jlWLMC = (((ccpWL1.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_MATERIALNAME"] as System.Collections.Hashtable)["value"].ToString();
            //        cbWLMC.Text = jlWLMC;
            //    }
            //}
            //else
            //{
            //    jlWLID = dtjlWLID.Rows[0]["FS_WL"].ToString().Trim();
            //    jlWLMC = dtjlWLID.Rows[0]["FS_MATERIALNAME"].ToString().Trim();
            //    //jlWLID = (((ccpWL.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_WL"] as System.Collections.Hashtable)["value"].ToString();
            //    //jlWLMC = (((ccpWL.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_MATERIALNAME"] as System.Collections.Hashtable)["value"].ToString();
            //    cbWLMC.Text = jlWLMC;
            //}
            //通过计量物料ID查询钢种、规格、长度  4.11与陈师讨论后是：通过SAP物料ID查询钢种、规格、长度
            if (strLH.Length == 9) //方坯
            {
                CoreClientParam ccpGZ = new CoreClientParam();
                ccpGZ.ServerName = "ygjzjl.car.WeighMeasureInfo";
                ccpGZ.MethodName = "QueryGZData";
                ccpGZ.ServerParams = new object[] { sapWLID };
                DataTable dtGZ = new DataTable();
                ccpGZ.SourceDataTable = dtGZ;

                this.ExecuteQueryToDataTable(ccpGZ, CoreInvokeType.Internal);
                if (dtGZ.Rows.Count > 0)
                {
                    GZ = dtGZ.Rows[0]["FS_STEELTYPE"].ToString().Trim();
                    GG = dtGZ.Rows[0]["FS_SPEC"].ToString().Trim();
                    CD = dtGZ.Rows[0]["FN_LENGTH"].ToString().Trim();
                }
            }

            //GZ = (((ccpGZ.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_STEELTYPE"] as System.Collections.Hashtable)["value"].ToString();
            //GG = (((ccpGZ.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FS_SPEC"] as System.Collections.Hashtable)["value"].ToString();
            //CD = (((ccpGZ.ReturnObject as System.Collections.ArrayList)[0] as System.Collections.Hashtable)["FN_LENGHT"] as System.Collections.Hashtable)["value"].ToString();

        }

        /// <summary>
        /// 输入合同号离开下载物料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHTH_Leave(object sender, EventArgs e)
        {
            //if (txtHTH.Text.Trim() == "" || txtHTH.Text.Trim() == sDDH.Trim() || txtHTH.Text.Trim().Length != 12)
            if (txtHTH.Text.Trim() == "")
            {
                return;
            }
            if (strJLDID == "")
            {
                MessageBox.Show("请先选择计量点！");
                return;
            }
            //等待窗体
            Cursor oldCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (Constant.WaitingForm == null)
            {
                Constant.WaitingForm = new WaitingForm();
            }
            Constant.WaitingForm.ShowToUser = true;
            Constant.WaitingForm.Show();
            Constant.WaitingForm.Update();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.SapOperation";
            ccp.MethodName = "queryProductNo";
            ccp.ServerParams = new object[] { txtHTH.Text.Trim() };
            DataTable dt = new DataTable();
            ccp.SourceDataTable = dt;
            ccp.IfShowErrMsg = false;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    this.txtHTXMH.DataSource = dt;
                    this.txtHTXMH.DisplayMember = "FS_ITEMNO";
                    this.txtHTXMH.ValueMember = "FS_ITEMNO";
                    this.txtHTXMH.Text = dt.Rows[0]["FS_ITEMNO"].ToString();

                    DataRow dr;
                    //插入物料内存主表
                    dr = m_MaterialTable.NewRow();
                    dr["fs_materialname"] = dt.Rows[0]["FS_MATERIALNAME"].ToString();
                    dr["FS_MATERIALNO"] = dt.Rows[0]["FS_WL"].ToString();
                    dr["FS_PointNo"] = strJLDID;
                    dr["FS_HELPCODE"] = "";
                    m_MaterialTable.Rows.Add(dr);
                    //插入物料名称下拉框绑定数据表
                    dr = tempMaterial.NewRow();
                    dr["fs_materialname"] = dt.Rows[0]["FS_MATERIALNAME"].ToString();
                    dr["FS_MATERIALNO"] = dt.Rows[0]["FS_WL"].ToString();
                    dr["FS_HELPCODE"] = "";
                    tempMaterial.Rows.Add(dr);

                    this.cbWLMC.Text = dt.Rows[0]["FS_MATERIALNAME"].ToString();

                    if (dt.Rows[0]["FS_STEELTYPE"].ToString() != "")
                        GZ = dt.Rows[0]["FS_STEELTYPE"].ToString();
                    if (dt.Rows[0]["FS_SPEC"].ToString() != "")
                        GG = dt.Rows[0]["FS_SPEC"].ToString();
                    if (dt.Rows[0]["FN_LENGTH"].ToString() != "")
                        CD = dt.Rows[0]["FN_LENGTH"].ToString();
                }
                else
                {

                    this.txtHTXMH.Text = "";
                    this.cbWLMC.Text = "";
                    GZ = "";
                    GG = "";
                    CD = "";
                }
            }
            else
                MessageBox.Show("订单下载失败！");

            this.Cursor = Cursors.Default;
            Constant.WaitingForm.ShowToUser = false;
            Constant.WaitingForm.Close();
        }

        #endregion

        #region 仪表重量采集

        /// <summary>
        /// 仪表连接   缺东西
        /// </summary>
        private void connectYB(string strJLDID)
        {

            mycom1.PortNum = iPort;//1,2,3,4
            mycom1.BaudRate = iRate;//1200,2400,4800,9600
            mycom1.ByteSize = bSize; //8 bits
            mycom1.Parity = bParity; // 0-4=no,odd,even,mark,space 
            mycom1.StopBits = bStopBits; // 0,1,2 = 1, 1.5, 2 
            //mycom1.WeightType = "8142";
            mycom1.WeightType = YBLX;


            if (strJLDID == "K01")
            {
                mycom1.NumReceived += new NumReceivedEvent(mycom1_NumReceived);
                //StartMonitoring(commThread1);
                if (mycom1.InitPort())
                {
                    mycom1.hComm1 = mycom1.hComm;

                    commThread1 = new Thread(new ThreadStart(mycom1.ControlComm31901));
                    commThread1.Start();
                    bool aa = commThread1.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive1 = mycom1.commThreadAlive;
                    //mycom1.CommCount = 0;
                }
            }

            if (strJLDID == "K02")
            {
                //StartMonitoring(commThread2);
                if (mycom1.InitPort())
                {
                    mycom1.hComm2 = mycom1.hComm;

                    commThread2 = new Thread(new ThreadStart(mycom1.ControlComm31902));
                    commThread2.Start();
                    bool aa = commThread2.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive2 = mycom1.commThreadAlive;
                    //mycom1.CommCount = 0;

                    mycom1.NumReceived += new NumReceivedEvent(mycom2_NumReceived);
                }
            }

            if (strJLDID == "K03")
            {
                mycom1.NumReceived += new NumReceivedEvent(mycom3_NumReceived);
                //StartMonitoring(commThread3);
                if (mycom1.InitPort())
                {
                    commThread3 = new Thread(new ThreadStart(mycom1.ControlComm31903));
                    commThread3.Start();
                    bool aa = commThread3.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive3 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm3 = mycom1.hComm;
                }
            }

            if (strJLDID == "K04")
            {
                mycom1.NumReceived += new NumReceivedEvent(mycom4_NumReceived);
                //StartMonitoring(commThread4);
                if (mycom1.InitPort())
                {
                    commThread4 = new Thread(new ThreadStart(mycom1.ControlComm31904));
                    commThread4.Start();
                    bool aa = commThread4.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive4 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm4 = mycom1.hComm;
                }
            }

            if (strJLDID == "K05")
            {
                //StartMonitoring(commThread5);
                if (mycom1.InitPort())
                {
                    commThread5 = new Thread(new ThreadStart(mycom1.ControlComm31905));
                    commThread5.Start();
                    bool aa = commThread5.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive5 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm5 = mycom1.hComm;

                    mycom1.NumReceived += new NumReceivedEvent(mycom5_NumReceived);
                }
            }

            if (strJLDID == "K06")
            {
                //StartMonitoring(commThread6);
                if (mycom1.InitPort())
                {
                    commThread6 = new Thread(new ThreadStart(mycom1.ControlCommCFC));
                    commThread6.Start();
                    bool aa = commThread6.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive6 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm6 = mycom1.hComm;

                    mycom1.NumReceived += new NumReceivedEvent(mycom6_NumReceived);
                }
            }

            if (strJLDID == "K07")
            {
                //StartMonitoring(commThread7);
                if (mycom1.InitPort())
                {
                    commThread7 = new Thread(new ThreadStart(mycom1.ControlComm81427));
                    commThread7.Start();
                    bool aa = commThread7.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive7 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm7 = mycom1.hComm;

                    mycom1.NumReceived += new NumReceivedEvent(mycom7_NumReceived);
                }
            }

            if (strJLDID == "K08")
            {
                mycom1.NumReceived += new NumReceivedEvent(mycom8_NumReceived);
                //StartMonitoring(commThread8);
                if (mycom1.InitPort())
                {
                    commThread8 = new Thread(new ThreadStart(mycom1.ControlComm31908));
                    commThread8.Start();
                    bool aa = commThread8.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive8 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm8 = mycom1.hComm;
                }
            }

            if (strJLDID == "K09")
            {
                mycom1.NumReceived += new NumReceivedEvent(mycom9_NumReceived);
                //StartMonitoring(commThread9);
                if (mycom1.InitPort())
                {
                    commThread9 = new Thread(new ThreadStart(mycom1.ControlComm31909));
                    commThread9.Start();
                    bool aa = commThread9.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive9 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm9 = mycom1.hComm;
                }
            }

            if (strJLDID == "K10")
            {
                mycom1.NumReceived += new NumReceivedEvent(mycom10_NumReceived);
                //StartMonitoring(commThread10);
                if (mycom1.InitPort())
                {
                    commThread10 = new Thread(new ThreadStart(mycom1.ControlComm319010));
                    commThread10.Start();
                    bool aa = commThread10.IsAlive;
                    mycom1.commThreadAlive = true;
                    mycom1.commThreadAlive10 = mycom1.commThreadAlive;
                    mycom1.CommCount = 0;
                    mycom1.hComm10 = mycom1.hComm;
                }
            }

            //StartMonitoring = "1";
            //if (mycom1.hComm == -1)
            //{
            //    StartMonitoring = "0";
            //}

            if (mycom1.hComm == -1)
            {
                //RichTextBox1.AppendText("\r\n串口开启失败……");
                MessageBox.Show("串口开启失败……");
                YBLX = "0";
            }
        }

        /// <summary>
        /// 清零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQL_Click(object sender, EventArgs e)
        {
            //if (mycom1.hComm != -1)
            //{
            //    mycom1.SetZeroCmd();
            //}
            int i = m_iSelectedPound;
            if (m_PoundRoomArray[i].POINTID == ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//当前正在操作的计量点
            {
                if (lbWD.Text == "未连仪表")
                {
                    MessageBox.Show("还未与仪表建立连接，请先接管计量点（打钩），或请管理员查明原因！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (Math.Abs(Convert.ToSingle(txtXSZL.Text.Trim())) > 1)
                {
                    MessageBox.Show("仪表数据太大，不能清零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (s_toZore[i] != 0)
                {
                    s_toZore[i] = Convert.ToDecimal(txtXSZL.Text.Trim()) + s_toZore[i];
                }
                else
                {
                    s_toZore[i] = Convert.ToDecimal(txtXSZL.Text.Trim());
                }
                txtXSZL.Text = "0.000";
                return;
            }
            if (strJLDID == "K01")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[1].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K02")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[2].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K03")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[3].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K04")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[4].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K05")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[5].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K06")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[6].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K07")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[7].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K08")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[8].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K09")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[9].Write(strCommand, 0, 8);
            }
            if (strJLDID == "K10")
            {
                char[] strCommand = new char[] { (char)2, (char)65, (char)72, (char)48, (char)57, (char)3, (char)0, (char)0 };
                m_SerialPort[10].Write(strCommand, 0, 8);
            }
        }

        //private delegate void setstring(NumberReceivedEventArgs e);

        #region 缺4个秤的NumResult 和 mycom1_NumReceived 方法
        private void NumResult(object sender, NumberReceivedEventArgs e)
        {
            txtXSZL.Text = e.WeightPhyNum1.ToString("#0.00");

            //this.textBox1.Text = mycom1.CommCount.ToString();
            string xszl = txtXSZL.Text;
            string[] ss = xszl.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries); //以小数点分割采集重量数据 "A|B|:|C:D"变ss[0]: A|B，ss[1]: C:D //把点改为“|:|”分割
            string[] zz = xszl.Split(".".ToCharArray()); //以小数点分割采集重量数据 "A|B|:|C:D"变zz[0]: A，zz[1]: B，zz[2]: ，zz[3]: ，zz[4]: C，zz[5]: D //用"|:|"分割
            string intzl = ss[0];
            int lenth = intzl.Length;
            cjzl = intzl.Substring(lenth - 1);
        }
        private void NumResult1(object sender, NumberReceivedEventArgs e)
        {
            bfzl1 = e.WeightPhyNum1.ToString("#0.00");
            if (strJLDID == "K01")
            {
                txtXSZL.Text = bfzl1;
            }
            if (e.WeightPhyNum1 > 0.1)
            {
                ultraGrid2.Rows[index1].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum1 < 0.1)
            {
                ultraGrid2.Rows[index1].Cells[1].Appearance.BackColor = Color.Red;
            }
        }
        private void NumResult2(object sender, NumberReceivedEventArgs e)
        {
            bfzl2 = e.WeightPhyNum2.ToString("#0.00");
            if (strJLDID == "K02")
            {
                txtXSZL.Text = bfzl2;
            }
            if (e.WeightPhyNum2 > 0.1)
            {
                ultraGrid2.Rows[index2].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum2 < 0.1)
            {
                ultraGrid2.Rows[index2].Cells[1].Appearance.BackColor = Color.White;
            }

            //判断6次重量之差都小于20Kg，点击计量点后，系统自动读数
            //int t = e.CommCount2;
            //y2 = t;
            //double ybcjzl1 = Convert.ToDouble(bfzl2);
            //double ybcjzl2 = 0;
            //Thread.Sleep(300);
            //if (e.CommCount2 != t)
            //{
            //    ybcjzl2 = e.WeightPhyNum2;
            //    if (ybcjzl2 - ybcjzl1 < 5.1)
            //    {
            //        y2 = y2 + 1;
            //    }
            //}
            //if (y2 == 6)
            //{
            //    sYBZL2 = e.WeightPhyNum2.ToString("#0.00");
            //}
            double ybcjzl1 = Convert.ToDouble(bfzl2);
            double ybcjzl2 = 0;
            Thread.Sleep(300);
            if (e.WeightPhyNum2 != ybcjzl1)
            {
                ybcjzl2 = e.WeightPhyNum2;
                if (ybcjzl2 - ybcjzl1 < 0.1)
                {
                    y2 = y2 + 1;
                }
            }
            if (y2 == 6)
            {
                sYBZL2 = e.WeightPhyNum2.ToString("#0.00");
                y2 = 0;
            }
        }
        private void NumResult3(object sender, NumberReceivedEventArgs e)
        {
            bfzl3 = e.WeightPhyNum3.ToString("#0.00");
            if (strJLDID == "K03")
            {
                txtXSZL.Text = bfzl3;
            }
            if (e.WeightPhyNum3 > 0.1)
            {
                ultraGrid2.Rows[index3].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum3 < 0.1)
            {
                ultraGrid2.Rows[index3].Cells[1].Appearance.BackColor = Color.Red;
            }
        }
        private void NumResult4(object sender, NumberReceivedEventArgs e)
        {
            bfzl4 = e.WeightPhyNum4.ToString("#0.00");
            if (strJLDID == "K04")
            {
                txtXSZL.Text = bfzl4;
            }
            if (e.WeightPhyNum4 > 0.1)
            {
                ultraGrid2.Rows[index4].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum4 < 0.1)
            {
                ultraGrid2.Rows[index4].Cells[1].Appearance.BackColor = Color.White;
            }

            //double t = e.CommCount;
            int y = 0;
            double ybcjzl1 = e.WeightPhyNum4;
            double ybcjzl2 = 0;
            //Thread.Sleep(300);
            if (e.WeightPhyNum4 != ybcjzl1)
            {
                ybcjzl2 = e.WeightPhyNum4;
                if (ybcjzl2 - ybcjzl1 < 0.1)
                {
                    y4 = y4 + 1;
                }
            }
            if (y4 == 6)
            {
                sYBZL4 = e.WeightPhyNum4.ToString("#0.00");
            }
        }
        private void NumResult5(object sender, NumberReceivedEventArgs e)
        {
            bfzl5 = e.WeightPhyNum5.ToString("#0.00");
            if (strJLDID == "K05")
            {
                txtXSZL.Text = bfzl5;
            }
            if (e.WeightPhyNum5 > 0.1)
            {
                ultraGrid2.Rows[index5].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum5 < 0.1)
            {
                ultraGrid2.Rows[index5].Cells[1].Appearance.BackColor = Color.Red;
            }
        }
        private void NumResult6(object sender, NumberReceivedEventArgs e)
        {
            bfzl6 = e.WeightPhyNum6.ToString("#0.00");
            if (strJLDID == "K06")
            {
                txtXSZL.Text = bfzl6;
            }
            if (e.WeightPhyNum6 > 0.1)
            {
                ultraGrid2.Rows[index6].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum6 < 0.1)
            {
                ultraGrid2.Rows[index6].Cells[1].Appearance.BackColor = Color.White;
            }

            double ybcjzl1 = Convert.ToDouble(bfzl6);
            double ybcjzl2 = 0;
            Thread.Sleep(300);
            if (e.WeightPhyNum6 != ybcjzl1)
            {
                ybcjzl2 = e.WeightPhyNum6;
                if (ybcjzl2 - ybcjzl1 < 0.1)
                {
                    y6 = y6 + 1;
                }
            }
            if (y6 == 6)
            {
                sYBZL6 = e.WeightPhyNum6.ToString("#0.00");
                y6 = 0;
            }
        }
        private void NumResult7(object sender, NumberReceivedEventArgs e)
        {
            bfzl7 = e.WeightPhyNum7.ToString("#0.00");
            if (strJLDID == "K07")
            {
                txtXSZL.Text = (e.WeightPhyNum7 / 1000).ToString("#0.000");
            }
            if (e.WeightPhyNum7 > 0.1)
            {
                ultraGrid2.Rows[index7].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum7 < 0.1)
            {
                ultraGrid2.Rows[index6].Cells[1].Appearance.BackColor = Color.White;
            }
        }
        private void NumResult8(object sender, NumberReceivedEventArgs e)
        {
            bfzl8 = e.WeightPhyNum8.ToString("#0.00");
            if (strJLDID == "K08")
            {
                txtXSZL.Text = bfzl8;
            }
            if (e.WeightPhyNum8 > 0.1)
            {
                ultraGrid2.Rows[index8].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum8 < 0.1)
            {
                ultraGrid2.Rows[index8].Cells[1].Appearance.BackColor = Color.Red;
            }
        }
        private void NumResult9(object sender, NumberReceivedEventArgs e)
        {
            bfzl9 = e.WeightPhyNum9.ToString("#0.00");
            if (strJLDID == "K09")
            {
                txtXSZL.Text = bfzl9;
            }
            if (e.WeightPhyNum9 > 0.1)
            {
                ultraGrid2.Rows[index9].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum9 < 0.1)
            {
                ultraGrid2.Rows[index9].Cells[1].Appearance.BackColor = Color.Red;
            }
        }
        private void NumResult10(object sender, NumberReceivedEventArgs e)
        {
            bfzl10 = e.WeightPhyNum10.ToString("#0.00");
            if (strJLDID == "K10")
            {
                txtXSZL.Text = bfzl10;
            }
            if (e.WeightPhyNum10 > 0.1)
            {
                ultraGrid2.Rows[index10].Cells[1].Appearance.BackColor = Color.Red;
            }
            if (e.WeightPhyNum10 < 0.1)
            {
                ultraGrid2.Rows[index10].Cells[1].Appearance.BackColor = Color.Red;
            }
        }

        private void mycom1_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult1);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom2_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult2);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom3_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult3);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom4_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult4);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom5_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult5);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom6_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult6);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom7_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult7);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom8_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult8);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom9_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult9);
            this.Invoke(fc, new object[] { null, e });
        }
        private void mycom10_NumReceived(object sender, NumberReceivedEventArgs e)
        {
            NumReceivedEvent fc = new NumReceivedEvent(NumResult10);
            this.Invoke(fc, new object[] { null, e });
        }
        #endregion

        /// <summary>
        /// 开始读数据监控
        /// </summary>
        public bool StartMonitoring(Thread commThread)
        {
            if (mycom1.InitPort())
            {
                commThread = new Thread(new ThreadStart(mycom1.CommThreadPro));
                commThread.Start();
                bool aa = commThread.IsAlive;
                mycom1.commThreadAlive = true;
                mycom1.CommCount = 0;
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 停止读数据监控
        /// </summary>
        public bool StopMonitoring(Thread commThread, bool commThreadAlive)
        {
            commThread.Abort();
            if (!commThreadAlive) return true;
            mycom1.commThreadAlive = false;

            if (mycom1.Close(mycom1.hComm1))
            {
                mycom1.CommCount = 0;
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 硬盘录像机
        /// <summary>
        /// 硬盘录像机连接
        /// </summary>
        private void ConnectYPLXJ() //有关视频的注释
        {
            int val = 0;
            sdk.SDK_Login(yplxjIP, lxjDK, lxjUser, lxjMM);//ip、端口、用户名、密码
            loghandle = val;

            //视频1
            int pichandle = (int)VideoChannel1.Handle;
            relhandle = sdk.SDK_RealPlay(1, 0, pichandle);
            //视频2
            int pichandle2 = (int)VideoChannel2.Handle;
            relhandle2 = sdk.SDK_RealPlay(2, 0, pichandle2);
            //视频3
            int pichandle3 = (int)VideoChannel3.Handle;
            relhandle3 = sdk.SDK_RealPlay(3, 0, pichandle3);
            //视频4
            int pichandle4 = (int)VideoChannel4.Handle;
            relhandle4 = sdk.SDK_RealPlay(4, 0, pichandle4);
            //视频5（照车牌号）
            int pichandle5 = (int)VideoChannel5.Handle;
            relhandle5 = sdk.SDK_RealPlay(5, 0, pichandle5);
            //视频6（照磅房）一般是隐藏的
            int pichandle6 = (int)VideoChannel6.Handle;
            relhandle6 = sdk.SDK_RealPlay(6, 0, pichandle6);

            sdk.SDK_OpenSound(relhandle); //采集（接收）现场声音
            sdk.SDK_SetVolume(65500); //音量大小控制
            sdkXCSY = "1";

        }
        /// <summary>
        /// 打开语音对讲
        /// </summary>
        private void OpenSoundTalk()
        {
            talkID = sdk.SDK_StartTalk(); //打开对讲

            //talhandle = sdk.SDK_StartTalk(loghandle); //打开对讲
            sdk.SDK_SetVolume(65500); //声音大小控制 0XFF00十进制，相当于声音80
            sdk.SDK_RealPlay(1,0,(int)picFDTP.Handle); //开始录音并播放
            istalk = 1;
            sdkYYDJ = "1";
        }
        /// <summary>
        /// 关闭语音对讲
        /// </summary>
        private void CloseSoundTalk()
        {
            sdk.SDK_StopRealPlay(m_iSelectedPound);
            sdk.SDK_StopTalk();
            istalk = 0;
            sdkYYDJ = "0";
        }
        
        #region 6个按钮  在语音播报界面上 底层
        private void btn1_Click(object sender, EventArgs e)
        {
            if (m_iSelectedPound < 0)
            {
                MessageBox.Show("请先选择一个计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int i = m_iSelectedPound;

            if (m_PoundRoomArray[i].VideoRecord == null)
            {
                MessageBox.Show("硬盘录像机未连接，不能发送语音！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (m_PoundRoomArray[i].Talk == true && m_PoundRoomArray[i].TalkID > 0)
            {
                m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(1,0,(int)picFDTP.Handle);
                m_PoundRoomArray[i].VideoRecord.SDK_StopTalk();//SDK_StopTalk(m_PoundRoomArray[i].TalkID);
                m_PoundRoomArray[i].Talk = false;
                m_PoundRoomArray[i].TalkID = 0;
            }

            m_PoundRoomArray[i].VideoRecord.SDK_SendData(Constant.RunPath + "\\sound\\请报车号.wav");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            sdk.SDK_StopRealPlay(m_iSelectedPound);
            sdk.SDK_StopTalk();
            sdk.SDK_SendData(Constant.RunPath + "\\sound\\请放第一次临时磅单.wav");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            sdk.SDK_StopRealPlay(m_iSelectedPound);
            sdk.SDK_StopTalk();
            sdk.SDK_SendData(Constant.RunPath + "\\sound\\超出停车线 请后退到停车线.wav");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            sdk.SDK_StopRealPlay(m_iSelectedPound);
            sdk.SDK_StopTalk();
            sdk.SDK_SendData(Constant.RunPath + "\\sound\\单据放倒请放正.wav");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            sdk.SDK_StopRealPlay(m_iSelectedPound);
            sdk.SDK_StopTalk();
            sdk.SDK_SendData(Constant.RunPath + "\\sound\\称重完成.wav");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            sdk.SDK_StopRealPlay(m_iSelectedPound);
            sdk.SDK_StopTalk();
            sdk.SDK_SendData(Constant.RunPath + "\\sound\\此车无预报请离开秤台.wav");
        }
        #endregion

        /// <summary>
        /// 在图片上添加重量 没有用到的 空方法
        /// </summary>
        /// <param name="SequenceNo"></param>
        /// <param name="index"></param>
        private void ImageAddWeight()
        {
            //sdk.SDK_CapturePicture(relhandle, "F:\\123.bmp");

            //System.Drawing.Image imgSrc = System.Drawing.Image.FromFile("F:\\123.bmp");
            //Graphics g = Graphics.FromImage(imgSrc);
            //g.DrawImage(imgSrc, 0, 0, imgSrc.Width, imgSrc.Height);
            //Font f = new Font("宋体", 20);
            //Brush b = new SolidBrush(Color.Red);
            //string addText = ImageJZ;
            //g.DrawString(addText, f, b, 100, 20);
            //imgSrc.Save("F:\\234.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //在图片上添加重量
            //System.Drawing.Image imgSrc = System.Drawing.Image.FromFile("F:\\123.bmp");
            //Graphics g = Graphics.FromImage(imgSrc);
            //g.DrawImage(imgSrc, 0, 0, imgSrc.Width, imgSrc.Height);
            //Font f = new Font("宋体", 20);
            //Brush b = new SolidBrush(Color.Red);
            //string addText = ImageJZ;
            //g.DrawString(addText, f, b, 100, 20);

            //byte[] FileContent;

            //if (System.IO.File.Exists(stRunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".bmp") == true)
            //{
            //    Bitmap img = new Bitmap(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".bmp");
            //    System.Drawing.Image.GetThumbnailImageAbort callb = null;
            //    System.Drawing.Image newimage = img.GetThumbnailImage(img.Width / 2, img.Height / 2, callb, new System.IntPtr());
            //    System.Drawing.Image newimage = img.GetThumbnailImage(img.Width, img.Height, callb, new System.IntPtr());

            //    在图片上添加重量
            //    Graphics g = Graphics.FromImage(imgSrc);
            //    g.DrawImage(imgSrc, 0, 0, imgSrc.Width, imgSrc.Height);
            //    Font f = new Font("宋体", 20);
            //    Brush b = new SolidBrush(Color.Red);
            //    string addText = ImageJZ;
            //    g.DrawString(addText, f, b, 100, 20);

            //    转换成JPG   
            //    newimage.Save(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG", System.Drawing.Imaging.ImageFormat.Jpeg);
            //    img.Dispose();
            //    newimage.Dispose();
            //    FileContent = System.IO.File.ReadAllBytes(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG");

            //    return FileContent;
            //}

            //if (System.IO.File.Exists(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG") == true)
            //{
            //    FileContent = System.IO.File.ReadAllBytes(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG");

            //    return FileContent;
            //}

            //FileContent = System.IO.File.ReadAllBytes(Constant.RunPath + "\\qcpicture\\" + SequenceNo + index.ToString() + ".JPG");
            //return FileContent;
        }
        #endregion

        #region 曲线图绘制

        private void DrawImage()
        {
            if (dtQX.Rows.Count == 0)
            {
                DataColumn dc = new DataColumn("ZL1", typeof(decimal));
                dtQX.Columns.Add(dc);

                DataRow dr = dtQX.NewRow();
                dr[0] = Convert.ToDecimal(txtXSZL.Text.Trim());
                dtQX.Rows.Add(dr);
                dtQX.AcceptChanges();

                ultraChart1.DataSource = dtQX;
                ultraChart1.DataBind();
                j = 1;
                return;
            }
            else
            {
                j = j + 1;
                DataColumn dc = new DataColumn("ZL" + j, typeof(decimal));
                dtQX.Columns.Add(dc);

                for (int i = 0; i <= 88; i++)
                {
                    if (dtQX.Rows[0][i].ToString() == "")
                    {
                        dtQX.Rows[0][i] = Convert.ToDecimal(txtXSZL.Text.Trim());
                        dtQX.AcceptChanges();

                        ultraChart1.DataSource = dtQX;
                        //ultraChart1.DataMember = "曲线图";
                        ultraChart1.DataBind();
                        return;
                    }
                }
            }

            //测试成功
            //if (dataTable6.Rows.Count == 0)
            //{
            //    DataRow dr = dataTable6.NewRow();
            //    dr[0] = Convert.ToDecimal(txtXSZL.Text.Trim());
            //    dataTable6.Rows.Add(dr);
            //    dataTable6.AcceptChanges();

            //    ultraChart1.DataSource = dataSet1;
            //    ultraChart1.DataMember = "曲线图";
            //    ultraChart1.DataBind();
            //    return;
            //}
            //else
            //{
            //    for (int i = 0; i <= 5; i++)
            //    {
            //        if (dataTable6.Rows[0][i].ToString() == "")
            //        {
            //            dataTable6.Rows[0][i] = Convert.ToDecimal(txtXSZL.Text.Trim());
            //            dataTable6.AcceptChanges();

            //            ultraChart1.DataSource = dataSet1;
            //            ultraChart1.DataMember = "曲线图";
            //            ultraChart1.DataBind();
            //            return;
            //        }
            //    }
            //}

            //测试,要画第二条线，再定义一行DataRow就OK了。以此类推
            //DataRow dr = dataTable6.NewRow();
            //dr[0] = 2;
            //dr[1] = 4;
            //dr[2] = 6;
            //dataTable6.Rows.Add(dr);
            //dataTable6.AcceptChanges();


            //ultraChart1.DataSource = dataSet1;
            ////ultraChart1.Data.DataMember = "ZL1";
            //ultraChart1.DataMember = "曲线图";
            ////ultraChart1.Series.DataBind();
            //ultraChart1.DataBind();
        }

        private void DrawImage1()
        {
            if (dtQX1.Rows.Count == 0)
            {
                DataColumn dc = new DataColumn("ZL1", typeof(decimal));
                dtQX1.Columns.Add(dc);

                DataRow dr = dtQX1.NewRow();
                dr[0] = Convert.ToDecimal(txtXSZL.Text.Trim());
                dtQX.Rows.Add(dr);
                dtQX.AcceptChanges();

                j1 = 1;
                return;
            }
            else
            {
                j1 = j1 + 1;
                DataColumn dc = new DataColumn("ZL" + j, typeof(decimal));
                dtQX1.Columns.Add(dc);

                for (int i = 0; i <= 4000; i++)
                {
                    if (dtQX1.Rows[0][i].ToString() == "")
                    {
                        dtQX1.Rows[0][i] = Convert.ToDecimal(txtXSZL.Text.Trim());
                        dtQX1.AcceptChanges();

                        return;
                    }
                }
            }
        }

        //没有用到的 空方法
        private void txtXSZL_TextChanged(object sender, EventArgs e)
        {
            //DrawImage();
            //if (strPoint == (string)c || strJLDID == (string)c)

            //switch (strPoint)
            //{
            //    case "K01":
            //        if (dtQX1.Rows.Count > 0)
            //        {
            //            //动态绘制曲线图
            //            ultraChart1.DataSource = dataTable6;
            //            ultraChart1.DataSource = dtQX1;
            //            ultraChart1.DataBind();
            //            break;
            //        }
            //        break;
            //    case "K02":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX2;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K03":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX3;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K04":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX4;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K05":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX5;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K06":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX6;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K07":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX7;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K08":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX8;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K09":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX9;
            //        ultraChart1.DataBind();
            //        break;
            //    case "K10":
            //        //动态绘制曲线图
            //        ultraChart1.DataSource = dataTable6;
            //        ultraChart1.DataSource = dtQX10;
            //        ultraChart1.DataBind();
            //        break;

            //    default:
            //        break;
            //}

        }
        #endregion

        #region Rtu按钮控制程序

        //空方法
        private void NewRefresh()
        {

        }

        public void GetVal(byte[] dmsg)
        {
            byte[] d = new byte[1];
            d[0] = dmsg[0];
            //d[1] = dmsg[1];
            BitArray all = new BitArray(d);
            state1 = all.Get(0);//照明等状态
            state2 = all.Get(1);//红绿灯状态
            state3 = all.Get(3);//前道闸状态
            state4 = all.Get(4);//后道闸状态
            state5 = all.Get(5);//电源状态
            state6 = all.Get(6);//外红外状态
            state7 = all.Get(7);//内红外状态

            //if (state1)
            //    btnZMDKG.Text = "关闭";
            //else
            //    btnZMDKG.Text = "打开";

            //if (state2)
            //    btnHL.Text = "关闭";
            //else
            //    btnHL.Text = "打开";

            //if (state3)
            //    btnSBDZ.Text = "关闭";
            //else
            //    btnSBDZ.Text = "打开";

            //if (state4)
            //    btnXBDZ.Text = "关闭";
            //else
            //    btnXBDZ.Text = "打开";

            ////if (state5)
            ////    btnXBDZ.Text = "关闭";
            ////else
            ////    btnXBDZ.Text = "打开";

            //if (state6)
            //    btnQDHW.Text = "关闭";
            //else
            //    btnQDHW.Text = "打开";

            //if (state7)
            //    btnHDHW.Text = "关闭";
            //else
            //    btnHDHW.Text = "打开";

            //btnZMDKG.Text = state1.ToString();
            //btnHL.Text = state2.ToString();
            //btnSBDZ.Text = state3.ToString();
            //btnXBDZ.Text = state4.ToString();
            ////btnDYZT.Text = state5.ToString();
            //btnQDHW.Text = state6.ToString();
            //btnHDHW.Text = state7.ToString();
        }

        /// <summary>
        /// 照明灯开关,0x00关闭, 0xFF打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnZMDKG_Click(object sender, EventArgs e)
        {
            int i = ultraGrid2.ActiveRow.Index;
            if (i < 0)
            {
                MessageBox.Show("请选择一个计量点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (StatusRedGreen.ForeColor == Color.Black)
            {
                MessageBox.Show("还未建立连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (m_PoundRoomArray != null && m_PoundRoomArray.Length >= i && m_PoundRoomArray[i] != null)
            {
                if (m_PoundRoomArray[i].Signed && m_PoundRoomArray[i].UseRtu)
                {
                    if (StatusLight.ForeColor == Color.DarkOrange)
                    {
                        m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xDD - 1), (byte)0x00, (byte)0);//0x00关闭
                    }
                    else
                    {
                        m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xDD - 1), (byte)0xFF, (byte)0);//
                    }
                }
            }
        }

        /// <summary>
        /// 红绿灯切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHL_Click(object sender, EventArgs e)
        {
            int i = ultraGrid2.ActiveRow.Index;
            if (i < 0)
            {
                MessageBox.Show("请选择一个计量点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (StatusRedGreen.ForeColor == Color.Black)
            {
                MessageBox.Show("还未建立连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (m_PoundRoomArray != null && m_PoundRoomArray.Length >= i && m_PoundRoomArray[i] != null)
            {
                if (m_PoundRoomArray[i].Signed && m_PoundRoomArray[i].UseRtu)
                {
                    if (StatusRedGreen.ForeColor == Color.Red)
                    {
                        m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xDE - 1), (byte)0x00, (byte)0);
                    }
                    else
                    {
                        m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xDE - 1), (byte)0xFF, (byte)0);
                    }
                }
            }
        }

        #region 道闸方法 红钢没有

        /// <summary>
        /// 上磅道闸控制，StatusCome，0xE1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSBDZ_Click(object sender, EventArgs e)
        {
            int i = ultraGrid2.ActiveRow.Index;
            if (i < 0)
            {
                MessageBox.Show("请选择一个计量点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (StatusCome.ForeColor == Color.Black)
            //{
            //    MessageBox.Show("还未建立连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (m_PoundRoomArray != null && m_PoundRoomArray.Length >= i && m_PoundRoomArray[i] != null)
            //{
            //    if (m_PoundRoomArray[i].Signed && m_PoundRoomArray[i].UseRtu)
            //    {
            //        if (StatusCome.Down == true)//上磅道闸落下
            //        {
            //            m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE1 - 1), (byte)0x00, (byte)0);
            //        }
            //        else
            //        {
            //            m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE1 - 1), (byte)0xFF, (byte)0);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 下磅道闸控制，StatusLeave，0xE0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXBDZ_Click(object sender, EventArgs e)
        {
            int i = ultraGrid2.ActiveRow.Index;
            if (i < 0)
            {
                MessageBox.Show("请选择一个计量点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (StatusLeave.ForeColor == Color.Black)
            //{
            //    MessageBox.Show("还未建立连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (m_PoundRoomArray != null && m_PoundRoomArray.Length >= i && m_PoundRoomArray[i] != null)
            //{
            //    if (m_PoundRoomArray[i].Signed && m_PoundRoomArray[i].UseRtu)
            //    {
            //        if (StatusLeave.Down == true)//下磅道闸落下
            //        {
            //            m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0x00, (byte)0);
            //        }
            //        else
            //        {
            //            m_PoundRoomArray[i].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0xFF, (byte)0);
            //        }
            //    }
            //}
        }

        #endregion

        #region 注释掉的3button个方法

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    if (state5)
        //        RC.RTU_BIND(IPAddress.Parse(ip), port, portnum, "OpenSwitch", 0x7E);
        //    else
        //        RC.RTU_BIND(IPAddress.Parse(ip), port, portnum, "CloseSwitch", 0x7E);

        //    Refresh();
        //}

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    if (state6)
        //        RC.RTU_BIND(IPAddress.Parse(ip), port, portnum, "OpenSwitch", 0x7F);
        //    else
        //        RC.RTU_BIND(IPAddress.Parse(ip), port, portnum, "CloseSwitch", 0x7F);

        //    Refresh();
        //}

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    if (state7)
        //        RC.RTU_BIND(IPAddress.Parse(ip), port, portnum, "OpenSwitch", 0x80);
        //    else
        //        RC.RTU_BIND(IPAddress.Parse(ip), port, portnum, "CloseSwitch", 0x80);

        //    Refresh();
        //}
        #endregion

        #endregion

        #region 用户验证和清空数据

        /// <summary>
        /// 锁定选择框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSD_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (btnSD.Text == "关闭设备")
            {
                if (DialogResult.No == MessageBox.Show("是否关闭设备！", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                UserSign = "0";

                //    //&&要修改&&

                StopPoundRoomThread();

                //    //&&要修改&&

                ClearControl();
                btnSD.BackColor = Color.SkyBlue;
                strJLDID = "";
                txtJLD.Text = "";              
                btnSD.Text = "打开设备";
                this.Cursor = Cursors.Default;
            }
            else
            {
                UserSign = "1";
                int temp =0;
                DataTable dtTemp= dataSet1.Tables[0].Copy();
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    if (dtTemp.Rows[i]["XZ"].ToString().ToLower() == "true")
                    {
                        temp++;
                    }
                }
                if (temp == 0)
                {
                    MessageBox.Show("请先选择计量点！");
                    return;
                }
                btnSD.BackColor = Color.Violet;
                strUID = "";
                strUMM = "";
                btnSD.Text = "关闭设备";

                DataTable tmpTable = dataSet1.Tables[0].Copy();
                dataSet1.Tables[0].Clear();
                DataRow[] drs = tmpTable.Select("", "XZ DESC");
                for (int i = 0; i < drs.Length; i++)
                {
                    dataSet1.Tables[0].Rows.Add(drs[i].ItemArray);
                }

                ultraGrid2.ActiveRow = ultraGrid2.Rows[0];
                ultraGrid2.Rows[0].Selected = true;

                //ultraGrid2.Rows[0].Cells["XZ"].Value = true;
                InitPound(dataSet1.Tables[0]);
                GetBaseData();
                BeginPoundRoomThread();
                m_iSelectedPound = 0;
                RecordOpen(m_iSelectedPound);
                BandBaseData();
                strJLDID = m_PoundRoomArray[m_iSelectedPound].POINTID;
                txtJLD.Text = m_PoundRoomArray[m_iSelectedPound].POINTNAME;
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 清空控件数据
        /// </summary>
        private void ClearControlData()
        {
            this.txtCZH.Text = "";
            cbCH.Text = "";
            cbCH1.Text = "";
            txtHTH.Text = "";
            txtHTXMH.Text = "";
            chbQXPZ.Checked = false;
            txtLH.Text = "";
            txtLH2.Text = "";
            txtLH3.Text = "";
            txtZS.Text = "";
            txtZS2.Text = "";
            txtZS3.Text = "";
            txtZL.Text = "";
            txtYKL.Text = "";
            //txtXSZL.Text = "";
            cbFHDW.Text = "";
            cbSHDW.Text = "";
            cbCYDW.Text = "";
            cbWLMC.Text = "";
            cbLX.Text = "";
            cbJLLX.Text = "";
            txtPJBH.Text = "";

            ////刷新打印重量
            //print.printMZ = "";
            //print.printPZ = "";
            //print.printJZ = "";

            txtMZ.Text = "";
            txtPZ.Text = "";
            txtJZ.Text = "";

            strYB = "";
            strYCJL = "";
            s_DFJZ = "";

            GZ = "";
            GG = "";
            CD = "";
            s_Guid = "";
            ImageGPZL = "";

            tbReceiverPlace.Text = "";
            tbSenderPlace.Text = "";
            cbProvider.Text = "";
            tbBZ.Text = "";
            txtDFJZ.Text = "";
            strAdviseSpec = "";
            strZZJY = "";
            strGPMaterial = "";
        }

        /// <summary>
        /// 清空控件内容
        /// </summary>
        private void ClearControl()
        {
            //this.txtCZH.Text = "";
            //cbCH.Text = "";
            //cbCH1.Text = "";
            //txtHTH.Text = "";
            //txtHTXMH.Text = "";
            //chbQXPZ.Checked = false;
            //txtLH.Text = "";
            //txtLH2.Text = "";
            //txtLH3.Text = "";
            //txtZS.Text = "";
            //txtZS2.Text = "";
            //txtZS3.Text = "";
            //txtZL.Text = "";
            //txtYKL.Text = "";
            ////txtXSZL.Text = "";
            //cbFHDW.Text = "";
            //cbSHDW.Text = "";
            //cbCYDW.Text = "";
            //cbWLMC.Text = "";
            //cbLX.Text = "";
            //cbJLLX.Text = "";
            //txtPJBH.Text = "";

            //仪表置零，禁止读数
            txtXSZL.Text = "0.00";
            lbYS.ForeColor = Color.Red;
            lbWD.Text = "未连仪表";
            btnDS.Enabled = false;

            //红绿灯、红外线、道闸、照明等置未连接状态，禁止控制按钮
            StatusRedGreen.ForeColor = Color.Black;
            StatusLight.ForeColor = Color.Black;
            //StatusCome.ForeColor = Color.Black;
            //StatusLeave.ForeColor = Color.Black;
            StatusFront.ForeColor = Color.Black;
            StatusBack.ForeColor = Color.Black;

            //禁止保存
            btnBC.Enabled = false;
            m_SaveImageSign = 0;
            m_GraspImageSign = 0;
            //BackZero = 1; //防止重复保存

            ////刷新打印重量
            //print.printMZ = "";
            //print.printPZ = "";
            //print.printJZ = "";

            //txtMZ.Text = "";
            //txtPZ.Text = "";
            //txtJZ.Text = "";
        }

        /// <summary>
        /// 清空预报数据
        /// </summary>
        private void ClearYBData()
        {
            ybCount[selectRow].strBF = "";
            ybCount[selectRow].strCH = "";
            ybCount[selectRow].strCYDW = "";
            ybCount[selectRow].strCZH = "";
            ybCount[selectRow].strFHFDM = "";
            ybCount[selectRow].strFHKCD = "";
            ybCount[selectRow].strHQLX = "";
            ybCount[selectRow].strHTH = "";
            ybCount[selectRow].strHTXMH = "";
            ybCount[selectRow].strLH = "";
            ybCount[selectRow].strLX = "";
            ybCount[selectRow].strQXPZBZ = "";
            ybCount[selectRow].strSHFDM = "";
            ybCount[selectRow].strSHKCD = "";
            ybCount[selectRow].strWLID = "";
            ybCount[selectRow].strWLMC = "";
            ybCount[selectRow].strYBH = "";
            ybCount[selectRow].strYBJZ = "";
            ybCount[selectRow].strYBPZ = "";
            ybCount[selectRow].strYBZZ = "";
            ybCount[selectRow].strZS = "";
            y_IFSAMPLING = "";//是否取样需要
            y_IFACCEPT = "";//是否需要验收 
            y_DRIVERNAME = "";//驾驶员姓名
            y_DRIVERIDCARD = "";//驾驶员身份证

            sFHDW = "";
            sSHDW = "";
            sCYDW = "";
            sWLMC = "";
            sLX = "";
        }

        /// <summary>
        /// 清空一次计量数据
        /// </summary>
   
        private void ClearYCBData()
        {
            stHTH = "";
            stHTXMH = "";
            stLH = "";
            stZS = "";
            stCZH = "";
            stCH = "";
            stWLID = "";
            stWLMC = "";
            stFHFDM = "";
            stCYDW = "";
            stSHFDM = "";
            stSHKCD = ""; //是否需要卸货确认
            stYBZZ = "";
            stYBPZ = "";
            stYBJZ = "";
            stLX = "";
            strYCZL = "";    //一次计量重量
            strYCJLD = "";        //一次计量点
            strYCJLY = "";        //一次计量员
            strYCJLSJ = "";        //一次计量时间
            strYCJLBC = "";        //一次计量班次
            strYCBDTM = "";        //一次计量磅单条码
            strXCRKSJ = "";      //卸车入库时间
            strXCCKSJ = "";      //卸车出库时间
            strXCQR = "";        //卸车确认
            strXCKGY = "";       //卸车库管员
            strZCRKSJ = "";      //装车入库时间
            strZCCKSJ = "";      //装车出库时间      
            strZCQR = "";        //装车确认
            strZCKGY = "";       //装车库管员
            strQYY = "";         //取样人
            strBFBH = "";        //磅房编号
            strYCSFYC = "";      //一次计量是否异常

            s_SAMPLETIME = "";//取样时间
            s_SAMPLEPLACE = "";//取样地点
            s_SAMPLEFLAG = ""; //取样确认
            s_UNLOADPERSON = ""; //卸车员
            s_UNLOADTIME = "";//卸车时间
            s_UNLOADPLACE = "";//卸车点
            s_CHECKPERSON = "";//验收员
            s_CHECKTIME = "";//验收时间
            s_CHECKPLACE = "";//验收点
            s_CHECKFLAG = "";//验收确认
            s_IFSAMPLING = "";//是否取样需要确认
            s_IFACCEPT = "";//是否需要验收确认 
            s_DRIVERNAME = "";//驾驶员姓名
            s_DRIVERIDCARD = "";//驾驶员身份证
            s_SENDERSTORE = "";//发货地点
            s_REWEIGHTFLAG = "";//复磅标志
            s_REWEIGHTTIME = "";//复磅确认时间
            s_REWEIGHTPLACE = "";//复磅确认地点
            s_REWEIGHTPERSON = "";//复磅确认员
            s_BILLNUMBER = "";//单据编号

            stYBH = ""; //预报号
        }
        /// <summary>
        /// 清空二次计量参数
        /// </summary>
        private void ClearECJLBData()
        {
            e_WEIGHTNO = ""; //作业编号
            e_CONTRACTNO = ""; //合同号
            e_CONTRACTITEM = ""; //合同项目编号
            e_STOVENO = ""; //炉号
            e_COUNT = ""; //支数
            e_CARDNUMBER = ""; //车证号
            e_CARNO = ""; //车号
            e_MATERIAL = ""; //物资代码
            e_MATERIALNAME = ""; //物料名称
            e_SENDER = ""; //发货方代码
            e_TRANSNO = ""; //承运方代码
            e_RECEIVER = ""; //收货方代码
            e_SENDGROSSWEIGHT = ""; //预报总重
            e_SENDTAREWEIGHT = ""; //预报皮重
            e_SENDNETWEIGHT = ""; //预报净量
            e_WEIGHTTYPE = ""; //流向
            e_GROSSWEIGHT = ""; //毛重重量
            e_GROSSPOINT = ""; //毛重计量点
            e_GROSSPERSON = ""; //毛重计量员
            e_GROSSDATETIME = ""; //毛重计量时间
            e_GROSSSHIFT = ""; //毛重计量班次
            e_TAREWEIGHT = ""; //皮重重量
            e_TAREPOINT = ""; //皮重计量点
            e_TAREPERSON = ""; //皮重计量员
            e_TAREDATETIME = ""; //皮重计量时间
            e_TARESHIFT = ""; //皮重计量班次
            e_FIRSTLABELID = ""; //一次磅单条码
            e_FULLLABELID = ""; //完整磅单条码
            e_NETWEIGHT = ""; //净重
            e_SAMPLEPERSON = ""; //取样员
            e_YKL = ""; //应扣量
            e_SAMPLETIME = ""; //取样时间
            e_SAMPLEPLACE = ""; //取样点
            e_SAMPLEFLAG = ""; //取样确认
            e_DRIVERNAME = ""; //驾驶员姓名
            e_DRIVERIDCARD = ""; //驾驶员身份证
            e_SENDERSTORE = ""; //发货地点
            e_IFSAMPLING = ""; //是否需要取样确认
            e_IFACCEPT = ""; //是否需要验收确认
            e_IFUNLOAD = ""; //是否需要卸货确认
            e_REWEIGHTFLAG = ""; //复磅标记
            e_REWEIGHTTIME = ""; //复磅确认时间
            e_REWEIGHTPLACE = ""; //复磅确认地点
            e_REWEIGHTPERSON = ""; //复磅确认员

            strECJL = "";
        }

        #endregion

        #region 仪表采集新

        /// <summary>
        /// 仪表连接
        /// </summary>
         
        //仪表采集 缺4个秤
        private void ConnectMeter(string strJLDID)
        {
            if (OpenComm(strJLDID))
            {
                if (strJLDID == "K01")
                {
                    myThread1 = new Thread(new ParameterizedThreadStart(ReadCommDataThread31901));
                    myThread1.Start("K01");
                    return;
                }
                if (strJLDID == "K02")
                {
                    myThread2 = new Thread(new ParameterizedThreadStart(ReadCommDataThread31902));
                    myThread2.Start("K02");
                    //myThreadAlive2 = true;
                    return;
                }
                if (strJLDID == "K03")
                {
                    myThread3 = new Thread(new ParameterizedThreadStart(ReadCommDataThread31903));
                    myThread3.Start("K03");
                    return;
                }
                if (strJLDID == "K04")
                {
                    myThread4 = new Thread(new ParameterizedThreadStart(ReadCommDataThread31904));
                    myThread4.Start("K04");
                    return;
                }
                if (strJLDID == "K05")
                {
                    myThread5 = new Thread(new ParameterizedThreadStart(ReadCommDataThread31905));
                    myThread5.Start("K05");
                    return;
                }
                if (strJLDID == "K06")
                {
                    myThread6 = new Thread(new ParameterizedThreadStart(ReadCommDataThread81426));
                    myThread6.Start("K06");
                    return;
                }
                if (strJLDID == "K07")
                {
                    myThread7 = new Thread(new ParameterizedThreadStart(ReadCommDataThread81427));
                    myThread7.Start("K07");
                    return;
                }
                if (strJLDID == "K08")
                {
                    myThread8 = new Thread(new ParameterizedThreadStart(ReadCommDataThread31908));
                    myThread8.Start("K08");
                    return;
                }
                if (strJLDID == "K09")
                {
                    myThread9 = new Thread(new ParameterizedThreadStart(ReadCommDataThread31909));
                    myThread9.Start("K09");
                    return;
                }
                if (strJLDID == "K10")
                {
                    myThread10 = new Thread(new ParameterizedThreadStart(ReadCommDataThread319010));
                    myThread10.Start("K10");
                    return;
                }
            }
        }

        #region 线程调用方法(仪表重量采集)
        /// <summary>
        /// 打开串口，串口如没打开，则不启动线程
        /// </summary>
        /// <param name="c"></param>
        
        //缺4个串口
        private bool OpenComm(string c)
        {
            string port = "";
            string ccom = "";
            //string yb = "";
            //int index = 0;

            switch (c)
            {
                case "K01":
                    port = strPort;
                    ccom = strCom;
                    index = index1 + 1;
                    break;
                case "K02":
                    port = strPort;
                    ccom = strCom;
                    //yb = YBLX;
                    index = index2 + 1;
                    //Comstate2 = true;
                    //Comstate = Comstate2;
                    //indexColor = index2;
                    break;
                case "K03":
                    port = strPort;
                    ccom = strCom;
                    index = index3 + 1;
                    break;
                case "K04":
                    port = strPort;
                    ccom = strCom;
                    index = index4 + 1;
                    break;
                case "K05":
                    port = strPort;
                    ccom = strCom;
                    index = index5 + 1;
                    break;
                case "K06":
                    port = strPort;
                    ccom = strCom;
                    index = index6 + 1;
                    break;
                case "K07":
                    port = strPort;
                    ccom = strCom;
                    index = index7 + 1;
                    break;
                case "K08":
                    port = strPort;
                    ccom = strCom;
                    index = index8 + 1;
                    break;
                case "K09":
                    port = strPort;
                    ccom = strCom;
                    index = index9 + 1;
                    break;
                case "K10":
                    port = strPort;
                    ccom = strCom;
                    index = index10 + 1;
                    break;

                default:
                    break;
            }

            bool a = OpenPort(port, ccom, index);

            if (!a)
            {
                MessageBox.Show("打开串口失败！");
                cell.Value = false;
                return false;
            }
            else
            {
                switch (c)
                {
                    case "K01":
                        Comstate1 = true;
                        indexColor = index1;
                        break;
                    case "K02":
                        Comstate2 = true;
                        //Comstate = Comstate2;
                        indexColor = index2;
                        break;
                    case "K03":
                        Comstate3 = true;
                        indexColor = index3;
                        break;
                    case "K04":
                        Comstate4 = true;
                        indexColor = index4;
                        break;
                    case "K05":
                        Comstate5 = true;
                        indexColor = index5;
                        break;
                    case "K06":
                        Comstate6 = true;
                        indexColor = index6;
                        break;
                    case "K07":
                        Comstate7 = true;
                        indexColor = index7;
                        break;
                    case "K08":
                        Comstate8 = true;
                        indexColor = index8;
                        break;
                    case "K09":
                        Comstate9 = true;
                        indexColor = index9;
                        break;
                    case "K10":
                        Comstate10 = true;
                        indexColor = index10;
                        break;

                    default:
                        break;
                }
            }
            return true;
        }

        #region 多线程调用一个方法（现在没用次方法）
        //缺4个
        private void ReadCommDataThread3190(object c)
        {
            switch ((string)c)
            {
                case "K01":
                    yb = YBLX;
                    index = index1 + 1;
                    Comstate1 = true;
                    Comstate = Comstate1;
                    indexColor = index1;
                    break;
                case "K02":
                    yb = YBLX;
                    index = index2 + 1;
                    Comstate2 = true;
                    Comstate = Comstate2;
                    indexColor = index2;
                    break;
                case "K03":
                    yb = YBLX;
                    index = index3 + 1;
                    Comstate3 = true;
                    Comstate = Comstate3;
                    indexColor = index3;
                    break;
                case "K04":
                    yb = YBLX;
                    index = index4 + 1;
                    Comstate4 = true;
                    Comstate = Comstate4;
                    indexColor = index4;
                    break;
                case "K05":
                    yb = YBLX;
                    index = index5 + 1;
                    Comstate5 = true;
                    Comstate = Comstate5;
                    indexColor = index5;
                    break;
                case "K06":
                    yb = YBLX;
                    index = index6 + 1;
                    Comstate6 = true;
                    Comstate = Comstate6;
                    indexColor = index6;
                    break;
                case "K07":
                    yb = YBLX;
                    index = index7 + 1;
                    Comstate7 = true;
                    Comstate = Comstate7;
                    indexColor = index7;
                    break;
                case "K08":
                    yb = YBLX;
                    index = index8 + 1;
                    Comstate8 = true;
                    Comstate = Comstate8;
                    indexColor = index8;
                    break;
                case "K09":
                    yb = YBLX;
                    index = index9 + 1;
                    Comstate9 = true;
                    Comstate = Comstate9;
                    indexColor = index9;
                    break;
                case "K10":
                    yb = YBLX;
                    index = index10 + 1;
                    Comstate10 = true;
                    Comstate = Comstate10;
                    indexColor = index10;
                    break;

                default:
                    break;
            }

            float b = 0;
            while (Comstate)
            {
                b = B3190(index);

                float ybzl2 = 0;
                switch ((string)c)
                {
                    case "K01":
                        ybzl2 = b;
                        break;
                    case "K02":
                        ybzl2 = b;
                        break;
                    case "K03":
                        ybzl2 = b;
                        break;
                    case "K04":
                        ybzl2 = b;
                        break;
                    case "K05":
                        ybzl2 = b;
                        break;
                    case "K06":
                        ybzl2 = b;
                        break;
                    case "K07":
                        ybzl2 = b;
                        break;
                    case "K08":
                        ybzl2 = b;
                        break;
                    case "K09":
                        ybzl2 = b;
                        break;
                    case "K10":
                        ybzl2 = b;
                        break;

                    default:
                        break;
                }

                if (ybzl1 > 0.5)
                {
                    //ColorThread = new Thread(new ThreadStart(ColorMethod));
                    //ColorThread.Start();

                    ultraGrid2.Rows[index - 1].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.Red;
                    //ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor2 = Color.Red;
                    //ultraGrid2.Rows[index - 1].Cells[1].Appearance.BorderColor2 = Color.Lime;
                    ultraGrid2.Rows[index - 1].Cells[1].Appearance.BorderColor = Color.Lime;
                    //ultraGrid2.Rows[index - 1].Cells[1].Appearance.ForeColor = Color.Black;
                }
                if (ybzl1 < 0.5)
                {
                    ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    if (ybzl2 >= ybzl1)
                    {
                        if (ybzl2 - ybzl1 <= 0.1)
                        {
                            y = y + 1;
                        }
                        if (ybzl2 - ybzl1 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (ybzl1 - ybzl2 < 0.1)
                            y = y + 1;
                        if (ybzl1 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (y == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        y = 0;
                    }

                    ybzl1 = b;
                }
            }
        }
        //缺4个
        private void ReadCommDataThread8142(object c)
        {
            float b = 0;
            while (Comstate)
            {
                b = B8142(index);

                float ybzl2 = 0;
                switch ((string)c)
                {
                    case "K01":
                        ybzl2 = b;
                        break;
                    case "K02":
                        ybzl2 = b;
                        break;
                    case "K03":
                        ybzl2 = b;
                        break;
                    case "K04":
                        ybzl2 = b;
                        break;
                    case "K05":
                        ybzl2 = b;
                        break;
                    case "K06":
                        ybzl2 = b;
                        break;
                    case "K07":
                        ybzl2 = b;
                        break;
                    case "K08":
                        ybzl2 = b;
                        break;
                    case "K09":
                        ybzl2 = b;
                        break;
                    case "K10":
                        ybzl2 = b;
                        break;

                    default:
                        break;
                }

                if (ybzl1 > 0.5)
                {
                    //ColorThread = new Thread(new ThreadStart(ColorMethod));
                    //ColorThread.Start();

                    ultraGrid2.Rows[index - 1].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.Red;
                    //ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor2 = Color.Red;
                    //ultraGrid2.Rows[index - 1].Cells[1].Appearance.BorderColor2 = Color.Lime;
                    ultraGrid2.Rows[index - 1].Cells[1].Appearance.BorderColor = Color.Lime;
                    //ultraGrid2.Rows[index - 1].Cells[1].Appearance.ForeColor = Color.Black;
                }
                if (ybzl1 < 0.5)
                {
                    ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    if (ybzl2 >= ybzl1)
                    {
                        if (ybzl2 - ybzl1 <= 0.1)
                        {
                            y = y + 1;
                        }
                        if (ybzl2 - ybzl1 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (ybzl1 - ybzl2 < 0.1)
                            y = y + 1;
                        if (ybzl1 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (y == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        y = 0;
                    }

                    ybzl1 = b;
                }
            }
        }
        #endregion
        //注视掉的方法
        //private void ReadCommDataThread(object c)
        //{
        //    //string port = "";
        //    //string ccom = "";
        //    //string yb = "";
        //    //int index = 0;

        //    //switch ((string)c)
        //    //{
        //    //    case "K01":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index1 + 1;
        //    //        Comstate1 = true;
        //    //        Comstate = Comstate1;
        //    //        indexColor = index1;
        //    //        break;
        //    //    case "K02":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index2 + 1;
        //    //        Comstate2 = true;
        //    //        Comstate = Comstate2;
        //    //        indexColor = index2;
        //    //        break;
        //    //    case "K03":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index3 + 1;
        //    //        Comstate3 = true;
        //    //        Comstate = Comstate3;
        //    //        indexColor = index3;
        //    //        break;
        //    //    case "K04":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index4 + 1;
        //    //        Comstate4 = true;
        //    //        Comstate = Comstate4;
        //    //        indexColor = index4;
        //    //        break;
        //    //    case "K05":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index5 + 1;
        //    //        Comstate5 = true;
        //    //        Comstate = Comstate5;
        //    //        indexColor = index5;
        //    //        break;
        //    //    case "K06":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index6 + 1;
        //    //        Comstate6 = true;
        //    //        Comstate = Comstate6;
        //    //        indexColor = index6;
        //    //        break;
        //    //    case "K07":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index7 + 1;
        //    //        Comstate7 = true;
        //    //        Comstate = Comstate7;
        //    //        indexColor = index7;
        //    //        break;
        //    //    case "K08":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index8 + 1;
        //    //        Comstate8 = true;
        //    //        Comstate = Comstate8;
        //    //        indexColor = index8;
        //    //        break;
        //    //    case "K09":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index9 + 1;
        //    //        Comstate9 = true;
        //    //        Comstate = Comstate9;
        //    //        indexColor = index9;
        //    //        break;
        //    //    case "K10":
        //    //        port = strPort;
        //    //        ccom = strCom;
        //    //        yb = YBLX;
        //    //        index = index10 + 1;
        //    //        Comstate10 = true;
        //    //        Comstate = Comstate10;
        //    //        indexColor = index10;
        //    //        break;

        //    //    default:
        //    //        break;
        //    //}

        //    //bool a = OpenPort(port, ccom, index);
        //    //float b = 0;

        //    //if (!a)
        //    //{
        //    //    MessageBox.Show("打开串口失败！");

        //    //    cell.Value = false;
        //    //}
        //    //else
        //    //{
        //    //Comstate = true;
        //    while (Comstate)
        //    {

        //        if (yb == "3190")
        //        {
        //            b = B3190(index);
        //        }
        //        else if (yb == "8142")
        //        {
        //            b = B8142(index);
        //        }

        //        //switch ((string)c)
        //        //{
        //        //    case "K01":
        //        //        strYBZL1 = b;
        //        //        break;
        //        //    case "K02":
        //        //        strYBZL2 = b;
        //        //        break;
        //        //    case "K03":
        //        //        strYBZL3 = b;
        //        //        break;
        //        //    case "K04":
        //        //        strYBZL4 = b;
        //        //        break;
        //        //    case "K05":
        //        //        strYBZL5 = b;
        //        //        break;
        //        //    case "K06":
        //        //        strYBZL6 = b;
        //        //        break;
        //        //    case "K07":
        //        //        strYBZL7 = b;
        //        //        break;
        //        //    case "K08":
        //        //        strYBZL8 = b;
        //        //        break;
        //        //    case "K09":
        //        //        strYBZL9 = b;
        //        //        break;
        //        //    case "K10":
        //        //        strYBZL10 = b;
        //        //        break;

        //        //    default:
        //        //        break;
        //        //}

        //        //if (strJLDID == (string)c)
        //        //{
        //        //    //txtXSZL.Text = b.ToString();
        //        //    float ybzl1 = b;
        //        //    float aa = B3190(index);
        //        //    float ybzl2 = 0;
        //        //    switch ((string)c)
        //        //    {
        //        //        case "K01":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K02":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K03":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K04":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K05":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K06":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K07":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K08":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K09":
        //        //            ybzl2 = aa;
        //        //            break;
        //        //        case "K10":
        //        //            ybzl2 = aa;
        //        //            break;

        //        //        default:
        //        //            break;
        //        //    }

        //        //    if (ybzl1 > 0.1)
        //        //    {
        //        //        ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.Red;
        //        //    }
        //        //    if (ybzl1 < 0.1)
        //        //    {
        //        //        ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.White;
        //        //    }

        //        //    if (ybzl2 >= ybzl1)
        //        //    {
        //        //        if (ybzl2 - ybzl1 < 0.2)
        //        //        {
        //        //            y = y + 1;
        //        //        }
        //        //        if (ybzl2 - ybzl1 > 0.2)
        //        //        {
        //        //            lbYS.ForeColor = Color.Red;
        //        //            lbWD.Text = "不稳定";
        //        //        }
        //        //    }
        //        //    else
        //        //    {
        //        //        if (ybzl1 - ybzl2 < 0.2)
        //        //            y = y + 1;
        //        //        if (ybzl1 - ybzl2 > 0.2)
        //        //        {
        //        //            lbYS.ForeColor = Color.Red;
        //        //            lbWD.Text = "不稳定";
        //        //        }
        //        //    }

        //        //    txtXSZL.Text = b.ToString("0.000");

        //        //    if (y == 6)
        //        //    {
        //        //        lbYS.ForeColor = Color.Lime;
        //        //        lbWD.Text = "稳定";
        //        //        y = 0;
        //        //    }
        //        //}

        //        //if (strJLDID == (string)c)
        //        //{
        //        //txtXSZL.Text = b.ToString();
        //        float ybzl2 = 0;
        //        switch ((string)c)
        //        {
        //            case "K01":
        //                ybzl2 = b;
        //                break;
        //            case "K02":
        //                ybzl2 = b;
        //                break;
        //            case "K03":
        //                ybzl2 = b;
        //                break;
        //            case "K04":
        //                ybzl2 = b;
        //                break;
        //            case "K05":
        //                ybzl2 = b;
        //                break;
        //            case "K06":
        //                ybzl2 = b;
        //                break;
        //            case "K07":
        //                ybzl2 = b;
        //                break;
        //            case "K08":
        //                ybzl2 = b;
        //                break;
        //            case "K09":
        //                ybzl2 = b;
        //                break;
        //            case "K10":
        //                ybzl2 = b;
        //                break;

        //            default:
        //                break;
        //        }

        //        if (ybzl1 > 0.5)
        //        {
        //            //ColorThread = new Thread(new ThreadStart(ColorMethod));
        //            //ColorThread.Start();

        //            ultraGrid2.Rows[index - 1].Cells["XZ"].Appearance.BackColor = Color.Red;

        //            ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.Red;
        //            //ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor2 = Color.Red;
        //            //ultraGrid2.Rows[index - 1].Cells[1].Appearance.BorderColor2 = Color.Lime;
        //            ultraGrid2.Rows[index - 1].Cells[1].Appearance.BorderColor = Color.Lime;
        //            //ultraGrid2.Rows[index - 1].Cells[1].Appearance.ForeColor = Color.Black;
        //        }
        //        if (ybzl1 < 0.5)
        //        {
        //            ultraGrid2.Rows[index - 1].Cells[1].Appearance.BackColor = Color.White;
        //        }

        //        if (strPoint == (string)c || strJLDID == (string)c)
        //        {
        //            txtXSZL.Text = b.ToString("0.00");

        //            if (ybzl2 >= ybzl1)
        //            {
        //                if (ybzl2 - ybzl1 <= 0.1)
        //                {
        //                    y = y + 1;
        //                }
        //                if (ybzl2 - ybzl1 > 0.1)
        //                {
        //                    lbYS.ForeColor = Color.Red;
        //                    lbWD.Text = "不稳定";
        //                    txtZL.Text = "";
        //                    btnDS.Enabled = true;
        //                    strDS = "0";
        //                }
        //            }
        //            else
        //            {
        //                if (ybzl1 - ybzl2 < 0.1)
        //                    y = y + 1;
        //                if (ybzl1 - ybzl2 > 0.1)
        //                {
        //                    lbYS.ForeColor = Color.Red;
        //                    lbWD.Text = "不稳定";
        //                    txtZL.Text = "";
        //                    btnDS.Enabled = true;
        //                    strDS = "0";
        //                }
        //            }

        //            if (y == 6)
        //            {
        //                lbYS.ForeColor = Color.Lime;
        //                lbWD.Text = "稳定";
        //                if (strDS == "0")
        //                {
        //                    txtZL.Text = txtXSZL.Text;
        //                }
        //                y = 0;
        //            }

        //            ybzl1 = b;
        //        }

        //        //}
        //    }

        //    //}
        //}
        #endregion

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="strPort"></param>
        /// <param name="strCom"></param>
        /// <param name="i"></param>
        /// <returns></returns>
         
        //缺4个
        private bool OpenPort(string strPort, string strCom, int i)
        {
            bool reVal = false;

            string[] str = strCom.Split(',');
            //设置奇偶校验
            Parity parity = Parity.None;
            //设置停止位
            StopBits stopBits = StopBits.None;

            if (strCom != "")
            {
                if (str[2].ToUpper() == "O")
                {
                    parity = Parity.Odd;
                }
                if (str[2].ToUpper() == "E")
                {
                    parity = Parity.Even;
                }

                if (str[4] == "1")
                {
                    stopBits = StopBits.One;
                }
                if (str[4] == "1.5")
                {
                    stopBits = StopBits.OnePointFive;
                }
                if (str[4] == "2")
                {
                    stopBits = StopBits.Two;
                }

                switch (i)
                {
                    case 1:
                        m_SerialPort1 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 2:
                        m_SerialPort2 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 3:
                        m_SerialPort3 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 4:
                        m_SerialPort4 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 5:
                        m_SerialPort5 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 6:
                        m_SerialPort6 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 7:
                        m_SerialPort7 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 8:
                        m_SerialPort8 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 9:
                        m_SerialPort9 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;
                    case 10:
                        m_SerialPort10 = new SerialPort(strPort, Int32.Parse(str[1]), parity, Int32.Parse(str[3]), stopBits);
                        break;

                    default:
                        break;
                }

                try
                {
                    switch (i)
                    {
                        case 1:
                            m_SerialPort1.Open();
                            reVal = m_SerialPort1.IsOpen;
                            break;
                        case 2:
                            m_SerialPort2.Open();
                            reVal = m_SerialPort2.IsOpen;
                            break;
                        case 3:
                            m_SerialPort3.Open();
                            reVal = m_SerialPort3.IsOpen;
                            break;
                        case 4:
                            m_SerialPort4.Open();
                            reVal = m_SerialPort4.IsOpen;
                            break;
                        case 5:
                            m_SerialPort5.Open();
                            reVal = m_SerialPort5.IsOpen;
                            break;
                        case 6:
                            m_SerialPort6.Open();
                            reVal = m_SerialPort6.IsOpen;
                            break;
                        case 7:
                            m_SerialPort7.Open();
                            reVal = m_SerialPort7.IsOpen;
                            break;
                        case 8:
                            m_SerialPort8.Open();
                            reVal = m_SerialPort8.IsOpen;
                            break;
                        case 9:
                            m_SerialPort9.Open();
                            reVal = m_SerialPort9.IsOpen;
                            break;
                        case 10:
                            m_SerialPort10.Open();
                            reVal = m_SerialPort10.IsOpen;
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception err)
                {
                    reVal = false;
                }
            }
            return reVal;
        }

        #region 多线程调用一个读数方法（现在没用次方法）
        private float B3190(int i)
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort[i].Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");

            if (m_SerialPort[i].BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort[i].ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }

        private float B8142(int i)
        {
            float reVal = 0;

            char[] strCommand = new char[] { (char)80 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort[i].Write(strCommand, 0, 1);
            System.Threading.Thread.Sleep(700);

            //richTextBox1.AppendText("8142 " + m_SerialPort[i].BytesToRead.ToString() + "\n");

            if (m_SerialPort[i].BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort[i].ReadExisting();
                char[] d = strtmp.ToCharArray();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(65, 8);
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / 1));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        #endregion

        /// <summary>
        /// 线程控制颜色提示方法
        /// </summary>
        private void ColorMethod()
        {
            //int k = Convert.ToInt32(index);
            ultraGrid2.Rows[indexColor].Cells[1].Appearance.BackColor = Color.Red;
            Thread.Sleep(1000);
            ultraGrid2.Rows[indexColor].Cells[1].Appearance.BackColor = Color.Lime;
            Thread.Sleep(1000);
        }

        #region 仪表线程调用方法(稳定值判断) 缺4个

        private void ReadCommDataThread31901(object c)
        {
            float b = 0;
            while (Comstate1)
            {
                b = B31901();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt1 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt1 = 1;
                }

                if (strYBZL1 > 0.0)
                {
                    ultraGrid2.Rows[index1].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index1].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index1].Cells[1].Appearance.BorderColor = Color.Lime;

                    Thread.Sleep(200);
                    ultraGrid2.Rows[index1].Cells[1].Appearance.BackColor = Color.Lime;
                }
                if (strYBZL1 < 0.0)
                {
                    ultraGrid2.Rows[index1].Cells[1].Appearance.BackColor = Color.White;
                }

                //判断重量稳定
                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL1)
                    {
                        if (ybzl2 - strYBZL1 <= 0.1)
                        {
                            wdcs1 = wdcs1 + 1;
                        }
                        if (ybzl2 - strYBZL1 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL1 - ybzl2 < 0.1)
                        {
                            wdcs1 = wdcs1 + 1;
                        }
                        if (strYBZL1 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs1 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs1 = 0;
                    }

                    strYBZL1 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)

                    ////动态绘制曲线图
                    //ultraChart1.DataSource = dtQX1;
                    //ultraChart1.DataBind();
                }


                //动态绘制曲线图
                if (dtQX1.Rows.Count == 0)
                {
                    DataColumn dc = new DataColumn("ZL1", typeof(decimal));
                    dtQX1.Columns.Add(dc);

                    DataRow dr = dtQX1.NewRow();
                    dr[0] = Convert.ToDecimal(txtXSZL.Text.Trim());
                    dtQX.Rows.Add(dr);
                    dtQX.AcceptChanges();

                    j1 = 1;
                    return;
                }
                else
                {
                    j1 = j1 + 1;
                    DataColumn dc = new DataColumn("ZL" + j1, typeof(decimal));
                    dtQX1.Columns.Add(dc);

                    for (int i = 0; i <= 4000; i++)
                    {
                        if (dtQX1.Rows[0][i].ToString() == "")
                        {
                            dtQX1.Rows[0][i] = Convert.ToDecimal(txtXSZL.Text.Trim());
                            dtQX1.AcceptChanges();

                            return;
                        }
                    }
                }
            }
        }
        private void ReadCommDataThread31902(object c)
        {
            float b = 0;
            while (Comstate2)
            {
                b = B31902();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt2 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt2 = 1;
                }

                if (strYBZL2 > 0.5)
                {
                    ultraGrid2.Rows[index2].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index2].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index2].Cells[1].Appearance.BorderColor = Color.Lime;

                    Thread.Sleep(200);
                    ultraGrid2.Rows[index2].Cells[1].Appearance.BackColor = Color.Lime;

                    //if (ksht2 == -1)
                    //{
                    //    ksht2 = 0;  //开始绘图重新开启
                    //}
                }
                if (strYBZL2 < 0.5)
                {
                    ultraGrid2.Rows[index2].Cells[1].Appearance.BackColor = Color.White;
                    //ksht2 = -1;  //开始绘图准备开启(车已下称台，等待下车上称台时重新开启绘图)
                }

                if (ybzl2 > 0.5)
                {
                    if (ksht2 == -1)
                    {
                        ksht2 = 0;  //开始绘图重新开启
                    }
                }
                if (ybzl2 < 0.5)
                {
                    ksht2 = -1;  //开始绘图准备开启(车已下称台，等待下车上称台时重新开启绘图)
                }

                //float htzl = 0;
                if (ksht2 == 0) //开始绘图
                {
                    if (ybzl2 != strYBZL2)
                    {
                        //dtQX2.Rows.Clear();
                        //dtQX2.Columns.Clear();
                        //动态绘制曲线图
                        if (dtQX2.Rows.Count == 0)
                        {
                            DataColumn dc = new DataColumn("ZL1", typeof(decimal));
                            dtQX2.Columns.Add(dc);

                            DataRow dr = dtQX2.NewRow();
                            //dr[0] = Convert.ToDecimal(txtXSZL.Text.Trim());
                            dr[0] = b;
                            dtQX2.Rows.Add(dr);
                            dtQX2.AcceptChanges();

                            j2 = 1;

                            //break;
                            //return;
                        }
                        else
                        {
                            j2 = j2 + 1;
                            DataColumn dc = new DataColumn("ZL" + j2, typeof(decimal));
                            dtQX2.Columns.Add(dc);

                            for (int i = 0; i <= 4000; i++)
                            {
                                if (dtQX2.Rows[0][i].ToString() == "")
                                {
                                    dtQX2.Rows[0][i] = Convert.ToDecimal(txtXSZL.Text.Trim());
                                    dtQX2.AcceptChanges();

                                    break;
                                    //return;
                                }
                            }
                        }
                    }
                    //htzl = b; //绘图重量与前次重量比较，不相等时才往绘图表中填充数据
                }

                //根据计量点判断重量是否稳定，并是否显示到界面显示重量文本框中
                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL2)
                    {
                        if (ybzl2 - strYBZL2 <= 0.1)
                        {
                            wdcs2 = wdcs2 + 1;
                        }
                        if (ybzl2 - strYBZL2 > 0.1)
                        {
                            float aa = Math.Abs(strYBZL2 - ybzl2);//绝对值
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL2 - ybzl2 < 0.1)
                        {
                            wdcs2 = wdcs2 + 1;
                        }
                        if (strYBZL2 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs2 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs2 = 0;
                    }

                    strYBZL2 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小) //绘图重量与前次重量比较，不相等时才往绘图表中填充数据

                    //动态绘制曲线图
                    ultraChart1.DataSource = dtQX2;
                    ultraChart1.DataBind();
                }
            }
        }
        private void ReadCommDataThread31903(object c)
        {
            float b = 0;
            while (Comstate3)
            {
                b = B31903();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt3 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt3 = 1;
                }

                if (strYBZL3 > 0.5)
                {
                    ultraGrid2.Rows[index3].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index3].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index3].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL3 < 0.5)
                {
                    ultraGrid2.Rows[index3].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL3)
                    {
                        if (ybzl2 - strYBZL3 <= 0.1)
                        {
                            wdcs3 = wdcs3 + 1;
                        }
                        if (ybzl2 - strYBZL3 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL3 - ybzl2 < 0.1)
                        {
                            wdcs3 = wdcs3 + 1;
                        }
                        if (strYBZL3 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs3 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs3 = 0;
                    }

                    strYBZL3 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread31904(object c)
        {
            float b = 0;
            while (Comstate4)
            {
                b = B31904();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt4 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt4 = 1;
                }

                if (strYBZL4 > 0.5)
                {
                    ultraGrid2.Rows[index4].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index4].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index4].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL4 < 0.5)
                {
                    ultraGrid2.Rows[index4].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL4)
                    {
                        if (ybzl2 - strYBZL4 <= 0.1)
                        {
                            wdcs4 = wdcs4 + 1;
                        }
                        if (ybzl2 - strYBZL4 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL4 - ybzl2 < 0.1)
                        {
                            wdcs4 = wdcs4 + 1;
                        }
                        if (strYBZL4 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs4 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs4 = 0;
                    }

                    strYBZL4 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread31905(object c)
        {
            float b = 0;
            while (Comstate5)
            {
                b = B31905();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt5 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt5 = 1;
                }

                if (strYBZL5 > 0.5)
                {
                    ultraGrid2.Rows[index5].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index5].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index5].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL5 < 0.5)
                {
                    ultraGrid2.Rows[index5].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL5)
                    {
                        if (ybzl2 - strYBZL5 <= 0.1)
                        {
                            wdcs5 = wdcs5 + 1;
                        }
                        if (ybzl2 - strYBZL5 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL5 - ybzl2 < 0.1)
                        {
                            wdcs5 = wdcs5 + 1;
                        }
                        if (strYBZL5 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs5 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs5 = 0;
                    }

                    strYBZL5 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread31906(object c)
        {
            float b = 0;
            while (Comstate6)
            {
                b = B31906();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt6 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt6 = 1;
                }

                if (strYBZL6 > 0.5)
                {
                    ultraGrid2.Rows[index6].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index6].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index6].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL6 < 0.5)
                {
                    ultraGrid2.Rows[index6].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL6)
                    {
                        if (ybzl2 - strYBZL6 <= 0.1)
                        {
                            wdcs6 = wdcs6 + 1;
                        }
                        if (ybzl2 - strYBZL6 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL6 - ybzl2 < 0.1)
                        {
                            wdcs6 = wdcs6 + 1;
                        }
                        if (strYBZL6 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs6 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs6 = 0;
                    }

                    strYBZL6 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread31907(object c)
        {
            float b = 0;
            while (Comstate7)
            {
                b = B31907();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt7 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt7 = 1;
                }

                if (strYBZL7 > 0.5)
                {
                    ultraGrid2.Rows[index7].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index7].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index7].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL7 < 0.5)
                {
                    ultraGrid2.Rows[index7].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL7)
                    {
                        if (ybzl2 - strYBZL7 <= 0.1)
                        {
                            wdcs7 = wdcs7 + 1;
                        }
                        if (ybzl2 - strYBZL7 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL7 - ybzl2 < 0.1)
                        {
                            wdcs7 = wdcs7 + 1;
                        }
                        if (strYBZL7 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs7 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs7 = 0;
                    }

                    strYBZL7 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread31908(object c)
        {
            float b = 0;
            while (Comstate8)
            {
                b = B31908();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt8 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt8 = 1;
                }

                if (strYBZL8 > 0.5)
                {
                    ultraGrid2.Rows[index8].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index8].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index8].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL8 < 0.5)
                {
                    ultraGrid2.Rows[index8].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL8)
                    {
                        if (ybzl2 - strYBZL8 <= 0.1)
                        {
                            wdcs8 = wdcs8 + 1;
                        }
                        if (ybzl2 - strYBZL8 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL8 - ybzl2 < 0.1)
                        {
                            wdcs8 = wdcs8 + 1;
                        }
                        if (strYBZL8 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs8 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs8 = 0;
                    }

                    strYBZL8 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread31909(object c)
        {
            float b = 0;
            while (Comstate9)
            {
                b = B31909();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt9 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt9 = 1;
                }

                if (strYBZL9 > 0.5)
                {
                    ultraGrid2.Rows[index9].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index9].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index9].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL9 < 0.5)
                {
                    ultraGrid2.Rows[index9].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL9)
                    {
                        if (ybzl2 - strYBZL9 <= 0.1)
                        {
                            wdcs9 = wdcs9 + 1;
                        }
                        if (ybzl2 - strYBZL9 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL9 - ybzl2 < 0.1)
                        {
                            wdcs9 = wdcs9 + 1;
                        }
                        if (strYBZL9 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs9 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs9 = 0;
                    }

                    strYBZL9 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread319010(object c)
        {
            float b = 0;
            while (Comstate10)
            {
                b = B319010();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt10 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt10 = 1;
                }

                if (strYBZL10 > 0.5)
                {
                    ultraGrid2.Rows[index10].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index10].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index10].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL10 < 0.5)
                {
                    ultraGrid2.Rows[index10].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    //int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL10)
                    {
                        if (ybzl2 - strYBZL10 <= 0.1)
                        {
                            wdcs10 = wdcs10 + 1;
                        }
                        if (ybzl2 - strYBZL10 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL10 - ybzl2 < 0.1)
                        {
                            wdcs10 = wdcs10 + 1;
                        }
                        if (strYBZL10 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs10 == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs10 = 0;
                    }

                    strYBZL10 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        #endregion

        #region 仪表读数3190(1-10) 缺4个
        /// <summary>
        /// 写入与读数
        /// </summary>
        /// <returns></returns>
        private float B31901()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort1.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort1.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort1.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B31902()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort2.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort2.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort2.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }

            return reVal;
        }
        private float B31903()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort3.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort3.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort3.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B31904()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort4.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort4.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort4.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B31905()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort5.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort5.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort5.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B31906()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort6.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort6.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort6.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B31907()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort7.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort7.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort7.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B31908()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort8.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort8.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort8.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B31909()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort9.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort9.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort9.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B319010()
        {
            float reVal = 0;
            char[] strCommand = new char[] { (char)2, (char)65, (char)66, (char)48, (char)51, (char)3, (char)0, (char)0 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort10.Write(strCommand, 0, 8);
            System.Threading.Thread.Sleep(200);
            //richTextBox1.AppendText("3190 " + m_SerialPort[i].BytesToRead.ToString() + "\n");
            //WriteLog("3190 " + m_SerialPort.BytesToRead.ToString());
            if (m_SerialPort10.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort10.ReadExisting();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(4, 6);
                    int min = Convert.ToInt32(strtmp.Substring(10, 1));
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / System.Math.Pow(10, min)));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        #endregion 

        #region 仪表线程调用方法8142测试
        private void ReadCommDataThread81426(object c)
        {
            float b = 0;
            while (Comstate6)
            {
                b = B81426();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt6 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt6 = 1;
                }

                if (strYBZL6 > 0.5)
                {
                    ultraGrid2.Rows[index6].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index6].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index6].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL6 < 0.5)
                {
                    ultraGrid2.Rows[index6].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL6)
                    {
                        if (ybzl2 - strYBZL6 <= 0.1)
                        {
                            wdcs = wdcs + 1;
                        }
                        if (ybzl2 - strYBZL6 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL6 - ybzl2 < 0.1)
                        {
                            wdcs = wdcs + 1;
                        }
                        if (strYBZL6 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs = 0;
                    }

                    strYBZL6 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        private void ReadCommDataThread81427(object c)
        {
            float b = 0;
            while (Comstate7)
            {
                b = B81427();

                float ybzl2 = 0;

                ybzl2 = b;

                if (ybzl2.ToString() == "0" && zt7 == 0)
                {
                    MessageBox.Show("没采集到仪表重量,请让管理员检查仪表是否打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    zt7 = 1;
                }

                if (strYBZL7 > 0.5)
                {
                    ultraGrid2.Rows[index7].Cells["XZ"].Appearance.BackColor = Color.Red;

                    ultraGrid2.Rows[index7].Cells[1].Appearance.BackColor = Color.Red;
                    ultraGrid2.Rows[index7].Cells[1].Appearance.BorderColor = Color.Lime;
                }
                if (strYBZL7 < 0.5)
                {
                    ultraGrid2.Rows[index7].Cells[1].Appearance.BackColor = Color.White;
                }

                if (strPoint == (string)c || strJLDID == (string)c)
                {
                    txtXSZL.Text = b.ToString("0.00");

                    int wdcs = 0; //稳定次数
                    if (ybzl2 >= strYBZL7)
                    {
                        if (ybzl2 - strYBZL7 <= 0.1)
                        {
                            wdcs = wdcs + 1;
                        }
                        if (ybzl2 - strYBZL7 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }
                    else
                    {
                        if (strYBZL7 - ybzl2 < 0.1)
                        {
                            wdcs = wdcs + 1;
                        }
                        if (strYBZL7 - ybzl2 > 0.1)
                        {
                            lbYS.ForeColor = Color.Red;
                            lbWD.Text = "不稳定";
                            txtZL.Text = "";
                            btnDS.Enabled = true;
                            strDS = "0";
                        }
                    }

                    if (wdcs == 6)
                    {
                        lbYS.ForeColor = Color.Lime;
                        lbWD.Text = "稳定";
                        if (strDS == "0")
                        {
                            txtZL.Text = txtXSZL.Text;
                        }
                        wdcs = 0;
                    }

                    strYBZL7 = b;  //给仪表定义的参数重量赋值(赋采集过来的仪表重量，用于下次和采集上了的仪表重量判断大小)
                }
            }
        }
        #endregion

        #region 仪表读数8142测试
        private float B81426()
        {
            float reVal = 0;

            char[] strCommand = new char[] { (char)80 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort6.Write(strCommand, 0, 1);
            System.Threading.Thread.Sleep(700);

            //richTextBox1.AppendText("8142 " + m_SerialPort[i].BytesToRead.ToString() + "\n");

            if (m_SerialPort6.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort6.ReadExisting();
                char[] d = strtmp.ToCharArray();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(65, 8);
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / 1));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        private float B81427()
        {
            float reVal = 0;

            char[] strCommand = new char[] { (char)80 };
            StringBuilder strRecv = new StringBuilder();

            m_SerialPort7.Write(strCommand, 0, 1);
            System.Threading.Thread.Sleep(700);

            //richTextBox1.AppendText("8142 " + m_SerialPort[i].BytesToRead.ToString() + "\n");

            if (m_SerialPort7.BytesToRead > 0)//串口缓冲有数据可读
            {
                string strtmp = m_SerialPort7.ReadExisting();
                char[] d = strtmp.ToCharArray();
                strRecv = strRecv.Append(strtmp);
                try
                {
                    string result = strtmp.Substring(65, 8);
                    reVal = (float)Convert.ToDouble(string.Format("{0:F3}", Single.Parse(result) / 1));
                }
                catch (Exception err)
                {

                }
            }
            return reVal;
        }
        #endregion

        #endregion

        #region 视频切换与云台控制
        private void VideoSwitch()
        {
            int i = m_iSelectedPound;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].VideoRecord == null)
            {
                return;
            }
            if (VideoChannel1.Visible == true)
            {
                VideoChannel1.Visible = false;
                VideoChannel5.Visible = true;
                VideoChannel5.BringToFront();
                //打开第6通道
                m_PoundRoomArray[i].Channel6 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_PoundRoomArray[i].VideoHandle, 5, (int)VideoChannel5.Handle);
                //m_PoundRoomArray[i].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[i].VideoHandle, 5, m_PoundRoomArray[i].POINTNAME + "- 磅房");
            }
            else
            {
                VideoChannel1.Visible = true;
                VideoChannel5.Visible = false;
                VideoChannel1.BringToFront();

                //关闭第6通道御览
                if (m_PoundRoomArray[i].Channel6 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel6);
                    m_PoundRoomArray[i].Channel6 = 0;
                    VideoChannel5.Refresh();
                }
            }
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            //pictureBox1.Visible = false;
            //pictureBox21.Visible = true;
            //pictureBox21.BringToFront();
            if (button1.Enabled != true && button2.Enabled != true)
            {
                for (int i = 1; i <= 15; i++)
                {
                    if (i != 9)
                    {
                        Button btnItemName = (Button)panelSPKZ.Controls.Find("button" + Convert.ToString(i), true)[0];
                        btnItemName.Enabled = true;
                    }
                }
            }
            k = 0;
        }

        private void pictureBox21_DoubleClick(object sender, EventArgs e)
        {
            //pictureBox1.Visible = true;
            //pictureBox21.Visible = false;
            //pictureBox1.BringToFront();
            if (button1.Enabled != true && button2.Enabled != true)
            {
                for (int i = 1; i <= 15; i++)
                {
                    if (i != 9)
                    {
                        Button btnItemName = (Button)panelSPKZ.Controls.Find("button" + Convert.ToString(i), true)[0];
                        btnItemName.Enabled = true;
                    }
                }
            }
        }

       

        #region 云台调节（视频远近、焦距调节）
        private void ControlButton_Down(object sender, MouseEventArgs e)
        {
            
        }

        private void ControlButton_Up(object sender, MouseEventArgs e)
        {
           
        }

        #endregion

        #endregion

        #region 给屏显发送信息
        private void ScreenShowData()
        {
            if (!s_SerialPort.IsOpen)
            {
                s_SerialPort.Open();
            }
            int j = 0;
            int[] reVal;

            reVal = StringToHexString("毛重：52.2t  皮重：12.9t  净重：39.3t");
            byte[] strCommand = new byte[100];
            strCommand[0] = 0xAA;
            strCommand[1] = 0x98;
            //坐标地址，十六进制
            strCommand[2] = 0x00;
            strCommand[3] = 0x80;//80这个是显示段落的第一行空几格
            strCommand[4] = 0x00;
            strCommand[5] = 0x30;//30这个是段落前面空几行才开始显示
            //字体，寄存器地址
            strCommand[6] = 0x24;

            strCommand[7] = 0xC1;
            strCommand[8] = 0x07;
            strCommand[9] = 0xFF;
            strCommand[10] = 0xFF;
            strCommand[11] = 0x00;
            strCommand[12] = 0x1F;
            //自己定义的值，显示在屏幕上的值，字符串
            for (j = 0; j < reVal.Length; j++)
            {
                strCommand[j + 13] = (byte)(reVal[j]);
            }
            strCommand[reVal.Length + 13] = 0xCC;
            strCommand[reVal.Length + 14] = 0x33;
            strCommand[reVal.Length + 15] = 0xC3;
            strCommand[reVal.Length + 16] = 0x3C;
            //byte[] aaa= new byte[]{0xAA, 0x98, 0x00, 0x80, 0x00, 0x30, 0x00, 0xC0, 0x04, 0xF8, 0x00, 0x00, 0x1F, 0x48, 0x6F, 0x77, 0x20, 0x61, 0x72, 0x65, 0x20, 0x79, 0x6F, 0x75, 0x20, 0x3F, 0xCC, 0x33, 0xC3, 0x3C};
            //s_SerialPort.Write(aaa, 0, aaa.le);
            s_SerialPort.Write(strCommand, 0, 17 + reVal.Length);
        }
        /// <summary>
        /// 发送的字符串转会成int数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private int[] StringToHexString(string s)
        {
            Encoding CnEnconding = Encoding.GetEncoding("GB2312");

            byte[] b = CnEnconding.GetBytes(s);
            int[] rel = new int[b.Length];
            string result = string.Empty;
            int i = 0;
            for (i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i], 16);
                // rel[i] = byte.Parse("0x"+Convert.ToString(b[i], 16));
                //rel[i] = result.ToCharArray();
                rel[i] = Convert.ToInt32(b[i]);
            }

            //rel = result.ToCharArray(); 

            //rel[0] = byte.Parse(result.Substring(0, 2));
            return rel;
        }
        /// <summary>
        /// 清屏
        /// </summary>
        private void ClearScreenData()
        {
            byte[] strCommand = new byte[] { 170, 82, 204, 51, 195, 60 };
            s_SerialPort.Write(strCommand, 0, strCommand.Length);
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox19_DoubleClick(object sender, EventArgs e)
        {
            ScreenShowData();
        }
        private void pictureBox3_DoubleClick(object sender, EventArgs e)
        {
            ClearScreenData();
        }
        #endregion

        #region 计量点ultraGrid2各个事件
        private void button9_Click(object sender, EventArgs e)
        {
            Print();
            return;
            if (m_iSelectedPound < 0)
            {
                MessageBox.Show("请先选择一个计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int i = m_iSelectedPound;       
        }

        private void ultraGrid2_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key.ToUpper() == "XZ")
            {
                if (btnSD.Text=="关闭设备")
                {
                    if (e.Cell.Value.ToString() == "True")
                    {
                        e.Cell.Value = true;
                    }
                    else
                    {
                        e.Cell.Value = false;
                    }

                    ultraGrid2.ActiveRow.Selected = true;

                    MessageBox.Show("请先关闭设备！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }
        }

        private void ultraGrid2_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (picFDTP.Visible == true)
            {
                CloseBigPicture();
                //MessageBox.Show("请关闭放大的视频！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
            if (m_iSelectedPound == ultraGrid2.ActiveRow.Index)
            {
                return;
            }
            if (btnSD.Text == "打开设备")
            {
                return;
            }
            //&&要修改&&
            this.Cursor = Cursors.WaitCursor;
            //关闭前一个选择的计量点语音视频
            RecordClose(m_iSelectedPound);

            ClearControlData();
            ClearControl();
            ClearGPData();
            ClearQXPZData();

            int iSelectIndex = ultraGrid2.ActiveRow.Index;
            m_iSelectedPound = iSelectIndex;
            txtCarNo.Text = "";
            //打开当前选择的计量点语音视频
            RecordOpen(iSelectIndex);
            ICCardOpen(iSelectIndex);

            ////新加的
            //if (UserSign == "0")
            //{
            strJLDID = ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim();
            strPoint = ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim();
            txtJLD.Text = ultraGrid2.ActiveRow.Cells["FS_POINTNAME"].Text.Trim();
            //}

            //GetCHData();
            this.BandPointMaterial(strJLDID); //绑定磅房物料
            this.BandPointReceiver(strJLDID); //绑定磅房收货单位
            this.BandPointSender(strJLDID); //绑定磅房发货单位
            this.BandPointTrans(strJLDID); //绑定磅房承运单位
            this.BandPointCarNo(strJLDID);//绑定磅房车号
            this.BandPointProvider(strJLDID);//绑定磅房供应单位

            if (m_nPointCount > 0)
            {
                //BandBaseData();
                txtZZ.Text = m_PoundRoomArray[m_iSelectedPound].USEDPAPER.ToString();
            }
            //txtCZH.Focus();
            ClearImageAndWeight();
            ifStart = "0";

            //SwitchPoundQuery();

            this.Cursor = Cursors.Default;

            //&&要修改&&
        }

        private void ultraGrid2_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Key == "FS_POINTNAME")
            {
                ultraGrid2.ActiveRow.Selected = true;
            }
        }
        #endregion

        #region 双击放大
        private void VideoChannel1_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(3);
        }

        private void VideoChannel2_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(1);
        }

        private void VideoChannel3_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(2);
        }

        private void VideoChannel4_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(0);
        }

        private void VideoChannel5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //panel20.Visible = true;
                //panel20.BringToFront();
                return;
            }

            CloseBigPicture();
            OpenBigPicture(4);
        }

        private void picFDTP_DoubleClick(object sender, EventArgs e)
        {
            CloseBigPicture();
        }

        private void VideoChannel2_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(2);
        }

        private void VideoChannel3_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(3);
        }

        private void VideoChannel1_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(1);
        }   

        private void VideoChannel4_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(4);
        }

        private void VideoChannel5_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(5);
        }

        private void VideoChannel6_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(6);
        }

        private void VideoChannel7_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(7);
        }

        private void VideoChannel8_Click(object sender, EventArgs e)
        {
            CloseBigPicture();
            OpenBigPicture(8);
        }

        private void OpenBigPicture(int iChannel)
        {
            int i = m_iSelectedPound;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].VideoRecord == null)
            {
                return;
            }

            if (iChannel == 0)
            {
                bool bTalkNow = false;
                //关闭语音对讲
                if (m_PoundRoomArray[i].Talk == true && m_PoundRoomArray[i].TalkID > 0)
                {
                    bTalkNow = true;
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].TalkID);
                    m_PoundRoomArray[i].VideoRecord.SDK_StopTalk();
                    m_PoundRoomArray[i].TalkID = 0;
                    m_PoundRoomArray[i].Talk = false;

                    ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                }

                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel1 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_CloseSound(m_iSelectedPound);

                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel1);
                    m_PoundRoomArray[i].Channel1 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_PoundRoomArray[i].VideoHandle, 0, (int)picFDTP.Handle);//注意第1个通道为车牌，在第4个图片显示 

                    if (bTalkNow)//如果放大前正在对讲，则再次打开
                    {
                        m_PoundRoomArray[i].TalkID = m_PoundRoomArray[i].VideoRecord.SDK_StartTalk();
                        m_PoundRoomArray[i].VideoRecord.SDK_SetVolume(65500);
                        m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_PoundRoomArray[i].VideoHandle, 0, (int)picFDTP.Handle);
                        m_PoundRoomArray[i].Talk = true;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                    }

                    m_PoundRoomArray[i].VideoRecord.SDK_OpenSound(BigChannel);
                    m_PoundRoomArray[i].VideoRecord.SDK_SetVolume(65500);

                }
            }
            else if (iChannel == 1)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel2 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel2);
                    m_PoundRoomArray[i].Channel2 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(iChannel,0, (int)picFDTP.Handle);
                }
            }
            else if (iChannel == 2)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel3 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel3);
                    m_PoundRoomArray[i].Channel3 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(iChannel, 0, (int)picFDTP.Handle);
                }
            }
            else if (iChannel == 3)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel4 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel4);
                    m_PoundRoomArray[i].Channel4 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(iChannel, 0, (int)picFDTP.Handle);
                }
            }
            else if (iChannel == 4)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel5 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel5);
                    m_PoundRoomArray[i].Channel5 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(iChannel, 0, (int)picFDTP.Handle);
                }
            }
            else if (iChannel == 5)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel6 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel6);
                    m_PoundRoomArray[i].Channel6 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(iChannel, 0, (int)picFDTP.Handle);
                }
            }

            else if (iChannel == 6)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel7 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel7);
                    m_PoundRoomArray[i].Channel7 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(iChannel, 0, (int)picFDTP.Handle);
                }
            }

            else if (iChannel == 7)
            {
                //关闭小图片监视,打开大图片监视
                if (m_PoundRoomArray[i].Channel8 > 0)
                {
                    m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[i].Channel8);
                    m_PoundRoomArray[i].Channel8 = 0;
                    BigChannel = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(iChannel, 0, (int)picFDTP.Handle);
                }
            }

            m_CurSelBigChannel = BigChannel > 0 ? iChannel : -1;

            if (BigChannel > 0)
            {
                picFDTP.Width = VideoChannel1.Width * 2;
                picFDTP.Height = VideoChannel1.Height * 2;
                picFDTP.Visible = true;
            }
            
        }

        /// <summary>
        /// 关闭大图监视，还原小图监视 有关视频的注释
        /// </summary>
        private void CloseBigPicture() 
        {
            int i = m_iSelectedPound;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].VideoRecord == null)
            {
                return;
            }

            if (BigChannel > 0 && m_CurSelBigChannel >= 0)
            {
                picFDTP.Visible = false;
                m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(BigChannel);
                BigChannel = 0;

                if (m_CurSelBigChannel == 1)
                {
                    bool bTalkNow = false;
                    //关闭语音对讲
                    if (m_PoundRoomArray[i].Talk == true && m_PoundRoomArray[i].TalkID > 0)
                    {
                        bTalkNow = true;
                        m_PoundRoomArray[i].VideoRecord.SDK_StopRealPlay(m_iSelectedPound);
                        m_PoundRoomArray[i].VideoRecord.SDK_StopTalk();
                        m_PoundRoomArray[i].TalkID = 0;
                        m_PoundRoomArray[i].Talk = false;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
                    }

                    m_PoundRoomArray[i].VideoRecord.SDK_CloseSound(m_iSelectedPound);

                    m_PoundRoomArray[i].Channel1 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_PoundRoomArray[i].VideoHandle, m_CurSelBigChannel, (int)VideoChannel1.Handle);

                    m_PoundRoomArray[i].VideoRecord.SDK_OpenSound(m_PoundRoomArray[i].Channel1);
                    m_PoundRoomArray[i].VideoRecord.SDK_SetVolume(65500);

                    if (bTalkNow)//如果放大前正在对讲，则再次打开
                    {
                        m_PoundRoomArray[i].TalkID = m_PoundRoomArray[i].VideoRecord.SDK_StartTalk();
                        m_PoundRoomArray[i].VideoRecord.SDK_SetVolume(65500);
                        m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_PoundRoomArray[i].VideoHandle, 0, (int)picFDTP.Handle);
                        m_PoundRoomArray[i].Talk = true;

                        ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "关闭对讲";
                    }
                }
                else if (m_CurSelBigChannel == 2)
                {
                    m_PoundRoomArray[i].Channel2 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_CurSelBigChannel, 0, (int)VideoChannel2.Handle);
                }
                else if (m_CurSelBigChannel == 3)
                {
                    m_PoundRoomArray[i].Channel3 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_CurSelBigChannel, 0, (int)VideoChannel3.Handle);
                }
                else if (m_CurSelBigChannel == 4)
                {
                    m_PoundRoomArray[i].Channel4 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_CurSelBigChannel, 0, (int)VideoChannel4.Handle);
                }
                else if (m_CurSelBigChannel == 5)
                {
                    m_PoundRoomArray[i].Channel5 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_CurSelBigChannel, 0, (int)VideoChannel5.Handle);
                }
                else if (m_CurSelBigChannel == 6)
                {
                    m_PoundRoomArray[i].Channel6 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_CurSelBigChannel, 0, (int)VideoChannel6.Handle);
                }
                else if (m_CurSelBigChannel == 7)
                {
                    m_PoundRoomArray[i].Channel7 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_CurSelBigChannel, 0, (int)VideoChannel7.Handle);
                }
                else if (m_CurSelBigChannel == 8)
                {
                    m_PoundRoomArray[i].Channel8 = m_PoundRoomArray[i].VideoRecord.SDK_RealPlay(m_CurSelBigChannel, 0, (int)VideoChannel8.Handle);
                }

                m_CurSelBigChannel = -1;
                picFDTP.Refresh();
            }
            else
            {
                picFDTP.Visible = false;
                picFDTP.Refresh();
            }
        }

        #endregion

        #region 解析动态曲线图片重量数据
        private string SaveCurveImage()
        {
            string strCurveImageData = "";
            int i = m_iSelectedPound;
            for (int c = 0; c < dtQXT[i].Columns.Count; c++)
            {
                if (dtQXT[i].Rows[0][c].ToString().Trim().Length > 0)
                {
                    if (c == 0)
                    {
                        strCurveImageData += "" + dtQXT[i].Rows[0][c].ToString().Trim() + "";
                    }
                    else
                    {
                        strCurveImageData += "," + dtQXT[i].Rows[0][c].ToString().Trim() + "";
                    }
                }
                else
                {
                    strCurveImageData += "" + dtQXT[i].Rows[0][c].ToString().Trim() + "";
                }
            }
            return strCurveImageData;
        }
        #endregion

        #region 磅单打印绘图
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int oX = 20, oY = 40;  //偏移量
            int xStep = 232;
            int yStep = 33;

            Font headFont = new Font("Arial", 14, FontStyle.Bold);
            Font drawFont = new Font("Arial", 9);
            Pen blackPen = new Pen(Color.Black, 2);
            StringFormat drawFormat1 = new StringFormat();
            drawFormat1.Alignment = StringAlignment.Center;
            drawFormat1.LineAlignment = StringAlignment.Center;

            StringFormat drawFormat2 = new StringFormat();
            drawFormat2.Alignment = StringAlignment.Near;
            drawFormat2.LineAlignment = StringAlignment.Center;

            StringFormat drawFormat3 = new StringFormat();
            drawFormat3.Alignment = StringAlignment.Far;
            drawFormat3.LineAlignment = StringAlignment.Center;

            Rectangle headRec = new Rectangle(oX, oY, 286, yStep);
            Rectangle rec = new Rectangle(oX, oY, xStep, yStep);

            //Pen pen = new Pen(Color.Black, 10);

            headRec.X = oX / 2;
            headRec.Y = oY / 8;
            e.Graphics.DrawString("昆钢公司物资计量单", headFont, Brushes.Black, headRec, drawFormat1);

            //获取服务器时间
            string strServerTime = getImage.GetServerTime();
            string[] serverTime = strServerTime.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            DateTime date = Convert.ToDateTime(serverTime[0]);
            string time = serverTime[1];

            //DateTime dtCode = Convert.ToDateTime(strServerTime);
            //string strCode = dtCode.ToString("yyyyMMddHHmmss") + strJLDID;

            if (print.printJLLX == "外协")
            {
                //车号
                rec.Y = oY;
                rec.Width = xStep; //设置控件宽度
                e.Graphics.DrawString("车号: " + print.printCH, drawFont, Brushes.Black, rec, drawFormat2);

                //日期
                rec.Y = oY + 1 * yStep;
                e.Graphics.DrawString("日期: " + date.ToShortDateString(), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 1 * yStep;
                e.Graphics.DrawString("时间: " + time, drawFont, Brushes.Black, rec, drawFormat3);

                //重量
                rec.Y = oY;
                e.Graphics.DrawString("重量: " + print.printJZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //计量点
                rec.Y = oY + 2 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + print.printJLD, drawFont, Brushes.Black, rec, drawFormat2);

                //计量员 改为编号
                rec.Y = oY + 3 * yStep;
                e.Graphics.DrawString("编号: " + strCode, drawFont, Brushes.Black, rec, drawFormat2);

                //备注
                rec.Y = oY + 4 * yStep;
                yStep = 36;
                e.Graphics.DrawString("备注:" + print.printJLLX + "    收费金额:" + this.tbCharge.Text.Trim() + " 元", drawFont, Brushes.Black, rec, drawFormat2);

                //打印条码
                Code128 c128 = new Code128();
                //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                c128.printCode(strCode, 320, 80, 2, e);

                //注意
                rec.Y = oY + 6 * yStep;
                //yStep = 66;前面备注那已经赋值了
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);
                return;
            }

            //合同号
            rec.Y = oY;
            rec.Width = 300; //设置控件宽度
            //rec.Width = xStep;
            e.Graphics.DrawString("合同(订单)号: " + print.printHTH, drawFont, Brushes.Black, rec, drawFormat2);

            //string strMonth = DateTime.Now.Month.ToString();
            //string strYear = DateTime.Now.Year.ToString();
            //string strXTBH = strYear + strMonth;


            //发货单位
            rec.Y = oY + 1 * yStep;
            //rec.Width = 300;
            e.Graphics.DrawString("发货单位: " + print.printFHDW, drawFont, Brushes.Black, rec, drawFormat2);

            //收货单位
            rec.Y = oY + 2 * yStep;
            e.Graphics.DrawString("收货单位: " + print.printSHDW, drawFont, Brushes.Black, rec, drawFormat2);

            //物资名称
            //string s_printWLMC1 = "";
            //string s_printWLMC2 = "";
            //if (print.printWLMC.Length > 12)
            //{
            //    s_printWLMC1 = print.printWLMC.Substring(0, 12);
            //    s_printWLMC2 = print.printWLMC.Substring(13);

            //    rec.Y = oY + 3 * yStep;
            //    e.Graphics.DrawString("物资名称: " + print.printWLMC, drawFont, Brushes.Black, rec, drawFormat2);
            //}
            rec.Width = 239; //物料名称太长了换行
            rec.Y = oY + 3 * yStep;
            e.Graphics.DrawString("物资名称: " + print.printWLMC, drawFont, Brushes.Black, rec, drawFormat2);
            rec.Width = 300; //物料名称太长了换行后还原

            //承运单位
            rec.Y = oY + 4 * yStep;
            e.Graphics.DrawString("承运单位: " + print.printCYDW, drawFont, Brushes.Black, rec, drawFormat2);

            if (print.printLH.Length > 0)
            {
                //车号
                rec.Y = oY + 7 * yStep;
                rec.Width = xStep; //设置控件宽度
                e.Graphics.DrawString("车号: " + print.printCH, drawFont, Brushes.Black, rec, drawFormat2);
            }
            else
            {
                //车号
                rec.Y = oY + 5 * yStep;
                rec.Width = xStep; //设置控件宽度
                e.Graphics.DrawString("车号: " + print.printCH, drawFont, Brushes.Black, rec, drawFormat2);
            }

            ////承运单位
            //rec.Y = oY + 5 * yStep;
            //e.Graphics.DrawString("承运单位:" + comboBox5.Text, drawFont, Brushes.Black, rec, drawFormat2);

            //钢坯打印
            if (print.printLH.Length > 0 && print.pringJLCS == "1")
            {
                //炉号
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("炉号: " + print.printLH, drawFont, Brushes.Black, rec, drawFormat2);

                //轧制建议
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("轧制建议: " + print.printZZJY, drawFont, Brushes.Black, rec, drawFormat3);

                //支数
                //rec.Y = oY + 5 * yStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("支(块)数: " + print.printZS, drawFont, Brushes.Black, rec, drawFormat2);

                //建议轧制规格
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("建议轧制规格: " + print.printAdviseSpec, drawFont, Brushes.Black, rec, drawFormat3);

                ////钢种
                //rec.Y = oY + 6 * yStep;
                //e.Graphics.DrawString("钢种: " + print.printGZ, drawFont, Brushes.Black, rec, drawFormat2);

                ////规格
                //rec.Y = oY + 6 * yStep;
                //e.Graphics.DrawString("规格: " + print.printGG, drawFont, Brushes.Black, rec, drawFormat3);


                //日期
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("日期: " + date.ToShortDateString(), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("时间: " + time, drawFont, Brushes.Black, rec, drawFormat2);

                //毛重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 7 * yStep;
                e.Graphics.DrawString("毛重: " + print.printMZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //皮重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("皮重: " + print.printPZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //净重
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("净重: " + print.printJZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //计量点
                rec.Y = oY + 10 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + print.printJLD, drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 11 * yStep;
                e.Graphics.DrawString("编号: " + strCode, drawFont, Brushes.Black, rec, drawFormat2);

                if (print.printJLLX == "")
                {
                    //备注
                    rec.Y = oY + 12 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);
                }
                if (print.printJLLX != "")
                {
                    //备注
                    rec.Y = oY + 12 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:" + print.printJLLX, drawFont, Brushes.Black, rec, drawFormat2);
                }

                //打印条码
                Code128 c128 = new Code128();
                //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                c128.printCode(strCode, 320, 80, 3, e);

                //注意
                rec.Y = oY + 14 * yStep;
                //yStep = 66;前面备注那已经赋值了
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);

                if (print.printLH2.Length > 0 && print.printCS == "1" || print.printLH2.Length > 0 && print.printCS == "0")
                {
                    print.printCS = "2";
                }
                if (print.printLH3.Length > 0 && print.printCS == "2")
                {
                    print.printCS = "3";
                }
                if (print.printLH1.Length > 0 && print.printCS == "3")
                {
                    print.printCS = "1";
                }
                return;
            }
            else if (print.printLH.Length > 0 && print.pringJLCS == "")
            {
                //炉号
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("炉号: " + print.printLH, drawFont, Brushes.Black, rec, drawFormat2);

                //轧制建议
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("轧制建议: " + ultraGrid3.ActiveRow.Cells["FS_ZZJY"].Text.Trim(), drawFont, Brushes.Black, rec, drawFormat3);

                //支数
                //rec.Y = oY + 5 * yStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("支(块)数: " + print.printZS, drawFont, Brushes.Black, rec, drawFormat2);


                //建议轧制规格
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("建议轧制规格: " + print.printAdviseSpec, drawFont, Brushes.Black, rec, drawFormat3);

                ////钢种
                //rec.Y = oY + 6 * yStep;
                //e.Graphics.DrawString("钢种: " + print.printGZ, drawFont, Brushes.Black, rec, drawFormat2);

                ////规格
                //rec.Y = oY + 6 * yStep;
                //e.Graphics.DrawString("规格: " + print.printGG, drawFont, Brushes.Black, rec, drawFormat3);

                //日期
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("日期: " + date.ToShortDateString(), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("时间: " + time, drawFont, Brushes.Black, rec, drawFormat3);

                //重量
                rec.Y = oY + 7 * yStep;
                e.Graphics.DrawString("重量: " + print.printJZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //计量点
                rec.Y = oY + 9 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + print.printJLD, drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 10 * yStep;
                e.Graphics.DrawString("编号: " + strCode, drawFont, Brushes.Black, rec, drawFormat2);

                ////备注
                //rec.Y = oY + 9 * yStep;
                //yStep = 36;
                //e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);
                if (print.printJLLX == "")
                {
                    //备注
                    rec.Y = oY + 11 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);
                }
                if (print.printJLLX != "")
                {
                    //备注
                    rec.Y = oY + 11 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:" + print.printJLLX, drawFont, Brushes.Black, rec, drawFormat2);
                }

                //打印条码
                Code128 c128 = new Code128();
                //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                c128.printCode(strCode, 320, 80, 4, e);

                //注意
                rec.Y = oY + 13 * yStep;
                //e.Graphics.DrawString("注意：本凭证请妥善保管遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);

                if (print.printLH2.Length > 0 && print.printCS == "1" || print.printLH2.Length > 0 && print.printCS == "0")
                {
                    print.printCS = "2";
                }
                if (print.printLH3.Length > 0 && print.printCS == "2")
                {
                    print.printCS = "3";
                }
                if (print.printLH1.Length > 0 && print.printCS == "3")
                {
                    print.printCS = "1";
                }
                return;
            }

            if (print.printLH.Length == 0 && print.pringJLCS == "1")
            {
                //日期
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("日期: " + date.ToShortDateString(), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 7 * yStep;
                e.Graphics.DrawString("时间: " + time, drawFont, Brushes.Black, rec, drawFormat2);

                //毛重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("毛重: " + print.printMZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //皮重
                //rec.X = oX + 2 * xStep;
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("皮重: " + print.printPZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                if (print.printYKL == "" && print.printYKBL == "")
                {
                    //净重
                    rec.Y = oY + 7 * yStep;
                    e.Graphics.DrawString("净重: " + print.printJZ + " t", drawFont, Brushes.Black, rec, drawFormat3);
                }
                else
                {
                    if (print.printYKL != "")
                    {
                        //扣渣
                        rec.Y = oY + 7 * yStep;
                        e.Graphics.DrawString("扣渣量: " + print.printYKL + " t", drawFont, Brushes.Black, rec, drawFormat3);
                    }
                    else
                    {
                        //扣渣
                        rec.Y = oY + 7 * yStep;
                        e.Graphics.DrawString("扣渣比例: " + print.printYKBL, drawFont, Brushes.Black, rec, drawFormat3);
                    }


                    rec.Y = oY + 8 * yStep;
                    e.Graphics.DrawString("净重(扣后): " + print.printKHJZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                }
                //计量点
                rec.Y = oY + 8 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + print.printJLD, drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 9 * yStep;
                e.Graphics.DrawString("编号: " + strCode, drawFont, Brushes.Black, rec, drawFormat2);

                if (print.printJLLX == "")
                {
                    //备注
                    rec.Y = oY + 10 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);
                }
                if (print.printJLLX != "")
                {
                    //备注
                    rec.Y = oY + 10 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:" + print.printJLLX, drawFont, Brushes.Black, rec, drawFormat2);
                }

                //打印条码
                Code128 c128 = new Code128();
                //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                c128.printCode(strCode, 320, 80, 0, e);

                //注意
                rec.Y = oY + 12 * yStep;
                //yStep = 66;前面备注那已经赋值了
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);
            }
            else
            {
                //日期
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("日期: " + date.ToShortDateString(), drawFont, Brushes.Black, rec, drawFormat2);
                //时间
                rec.Y = oY + 6 * yStep;
                e.Graphics.DrawString("时间: " + time, drawFont, Brushes.Black, rec, drawFormat3);

                //重量
                rec.Y = oY + 5 * yStep;
                e.Graphics.DrawString("重量: " + print.printJZ + " t", drawFont, Brushes.Black, rec, drawFormat3);

                //计量点
                rec.Y = oY + 7 * yStep;
                rec.Width = 300;
                e.Graphics.DrawString("计量点: " + print.printJLD, drawFont, Brushes.Black, rec, drawFormat2);

                //计量员
                rec.Y = oY + 8 * yStep;
                e.Graphics.DrawString("编号: " + strCode, drawFont, Brushes.Black, rec, drawFormat2);

                ////备注
                //rec.Y = oY + 9 * yStep;
                //yStep = 36;
                //e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);
                if (print.printJLLX == "")
                {
                    //备注
                    rec.Y = oY + 9 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:", drawFont, Brushes.Black, rec, drawFormat2);
                }
                if (print.printJLLX != "")
                {
                    //备注
                    rec.Y = oY + 9 * yStep;
                    yStep = 36;
                    e.Graphics.DrawString("备注:" + print.printJLLX, drawFont, Brushes.Black, rec, drawFormat2);
                }

                //打印条码
                Code128 c128 = new Code128();
                //strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                c128.printCode(strCode, 320, 80, 1, e);

                //注意
                rec.Y = oY + 11 * yStep;
                //yStep = 66;前面备注那已经赋值了
                e.Graphics.DrawString("注意：本凭证请妥善保管,避免高温、潮湿、阳光直射,遇有问题请致电（8610918）", drawFont, Brushes.Black, rec, drawFormat2);
            }

            ////条码打印编号
            //rec.Y = oY;
            //e.Graphics.DrawString("编号: " + strCode, drawFont, Brushes.Black, rec, drawFormat3);

            //e.Graphics.DrawRectangle(pen, rec);
            e.HasMorePages = false;
        }

        /// <summary>
        /// 设置打印机页面的相关属性,此处没有初始化，代码可以去掉
        /// </summary>
        private void PrinterInit()  //打印机初始化
        {
            printDocument1.PrinterSettings.PrinterName = m_PoundRoomArray[m_iSelectedPound].PRINTERNAME;  //用打印机名字打印
        }

        private void Print()
        {
            //打印
            printDocument1.PrinterSettings.PrinterName = m_PoundRoomArray[m_iSelectedPound].PRINTERNAME;//EPSON BA-T500 Full cut
            System.Drawing.Printing.Margins margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            printDocument1.DefaultPageSettings.Margins = margins;
            printDocument1.Print();

            //预览
            //PrintPreviewDialog dialog = new PrintPreviewDialog();
            //dialog.Document = this.printDocument1;
            //dialog.PrintPreviewControl.AutoZoom = false;
            //dialog.PrintPreviewControl.Zoom = 0.75;
            //dialog.ShowDialog();

        }
        #endregion

        #region 磅单打印调用Excel

        private void PrintPoundData()
        {
            //1类型的固定报表测试
            string fileName = "汽车衡磅单打印";
            string filePath = "汽车衡磅单打印\\汽车衡磅单打印";

            XMLReport test = new XMLReport(fileName);

            System.Collections.ArrayList headlist = new System.Collections.ArrayList();

            System.Collections.ArrayList nodevaluelist = test.GetNodeValueListByXMLPath(@"Config/Head/Source/SqlLang");
            string selectsql = nodevaluelist[0].ToString() + " where FS_WEIGHTNO='" + strZYBH + "'";//此只为测试
            DataTable testdatatable = new DataTable();

            CoreClientParam ccpquery = new CoreClientParam();
            ccpquery.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpquery.MethodName = "QueryDYData";
            ccpquery.ServerParams = new object[] { selectsql };
            ccpquery.SourceDataTable = testdatatable;
            this.ExecuteQueryToDataTable(ccpquery, CoreInvokeType.Internal);

            if (testdatatable.Rows.Count < 1)
            {
                MessageBox.Show("没有数据！");
                return;
            }

            test.CreateFixXMLReportFile(testdatatable);
            test.PrintReportXMLFile();
        }
        #endregion

        #region 刷卡刷屏方法
        /// <summary>
        /// 刷车证卡后液晶屏显示计量信息
        /// </summary>
        private void DisPlayShow()
        {
            int i = m_iSelectedPound;

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Display == null || m_PoundRoomArray[i].Display.StateInfo.Trim() != "open")//未接管的计量点
            {
                return;
            }

            int sleepTime = 20;
            if (m_PoundRoomArray[m_iSelectedPound].POINTID == "K10")
                sleepTime = 100;

            m_PoundRoomArray[m_iSelectedPound].Display.ClearScreen();//记得每次处理一组事务前先清屏
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.DrawPicture(21);
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 205, Color.Yellow, txtCZH.Text.Trim());
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 253, Color.Yellow, cbCH.Text.Trim() + cbCH1.Text.Trim());
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 303, Color.Yellow, cbFHDW.Text.Trim());
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 355, Color.Yellow, cbSHDW.Text.Trim());
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 405, Color.Yellow, cbCYDW.Text.Trim());
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 455, Color.Yellow, cbWLMC.Text.Trim());
        }

        /// <summary>
        /// 一次计量完成后液晶屏显示计量信息
        /// </summary>
        private void DisPlayShowForFirst()
        {
            int sleepTime = 20;
            //if (m_PoundRoomArray[m_iSelectedPound].POINTID == "K10")
            //  sleepTime = 100;

            m_PoundRoomArray[m_iSelectedPound].Display.ClearScreen();//记得每次处理一组事务前先清屏
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.DrawPicture(26);
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 205, Color.Yellow, txtCZH.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 253, Color.Yellow, cbCH.Text.Trim() + cbCH1.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 303, Color.Yellow, cbFHDW.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 355, Color.Yellow, cbSHDW.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 405, Color.Yellow, cbCYDW.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 455, Color.Yellow, cbWLMC.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 455, Color.Yellow, cbWLMC.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 500, Color.Yellow, txtXSZL.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].ClearReader();
        }

        /// <summary>
        /// 二次计量完成后液晶屏显示计量信息
        /// </summary>
        private void DisPlayShowForSecond()
        {
            int sleepTime = 20;
            //if (m_PoundRoomArray[m_iSelectedPound].POINTID == "K10")
            //    sleepTime = 100;

            m_PoundRoomArray[m_iSelectedPound].Display.ClearScreen();//记得每次处理一组事务前先清屏
            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.DrawPicture(26);

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 205, Color.Yellow, txtCZH.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 253, Color.Yellow, cbCH.Text.Trim() + cbCH1.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 303, Color.Yellow, cbFHDW.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 355, Color.Yellow, cbSHDW.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 405, Color.Yellow, cbCYDW.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 455, Color.Yellow, cbWLMC.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 500, Color.Yellow, txtXSZL.Text.Trim());

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].Display.WriteText(500, 550, Color.Yellow, print.printJZ);

            Thread.Sleep(sleepTime);
            m_PoundRoomArray[m_iSelectedPound].ClearReader();

        }
        #endregion
        //注释 空方法
        private void lbWD_TextChanged(object sender, EventArgs e)
        {
           
        }

        #region 计算毛重、皮重、净重
        /// <summary>
        /// 重量改变，如果有一次计量数据，就要同时计算毛重、皮重、净重
        /// </summary>
        private void CountWeight()
        {
            if (s_REWEIGHTFLAG == "1")
                return;

            if (strYCJL == "1")
            {
                if (txtXSZL.Text.Trim() == "")
                {
                    txtXSZL.Text = "0";
                }
                if (strYCZL == "")
                {
                    strYCZL = "0";
                }
                if (txtXSZL.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(strYCZL) >= Convert.ToDecimal(txtXSZL.Text.Trim()))
                    {
                        txtMZ.Text = strYCZL;
                        txtPZ.Text = txtXSZL.Text.Trim();
                        txtJZ.Text = (Convert.ToDecimal(strYCZL) - Convert.ToDecimal(txtXSZL.Text.Trim())).ToString().Trim();
                    }
                }
                if (txtXSZL.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(strYCZL) < Convert.ToDecimal(txtXSZL.Text.Trim()))
                    {
                        txtMZ.Text = txtXSZL.Text.Trim();
                        txtPZ.Text = strYCZL;
                        txtJZ.Text = (Convert.ToDecimal(txtXSZL.Text.Trim()) - Convert.ToDecimal(strYCZL)).ToString().Trim();
                    }
                }
            }
            else
                if (strQXPZ != "")
                {
                    txtMZ.Text = txtXSZL.Text.Trim();
                    txtPZ.Text = strQXPZ;
                    txtJZ.Text = (Convert.ToDecimal(txtXSZL.Text.Trim()) - Convert.ToDecimal(strQXPZ)).ToString();
                }

        }
        /// <summary>
        /// 清空图片与计算的重量
        /// </summary>
        private void ClearImageAndWeight()
        {
            txtMZ.Text = "";
            txtPZ.Text = "";
            txtJZ.Text = "";

            //pictureBox18.Image = BitmapToImage(new byte[1]);
            //pictureBox18.Refresh();
            //panel20.Visible = false;
            //panel22.BringToFront();
        }
        #endregion

        #region 钢坯保存
        /// <summary>
        /// 保存钢坯一次计量
        /// </summary>
        private bool SaveGPDataOne()
        {
            if (strZYBH == "")
            {
                strZYBH = Guid.NewGuid().ToString().Trim();
            }
            //string strSJZL = txtXSZL.Text.Trim();
            string strSJZL = "";
            //if (ImageJZ != null)
            //{
            //    strSJZL = ImageJZ;
            //}
            //else
            //{
            strSJZL = txtZL.Text.Trim();
            if (strSJZL == "")
                strSJZL = txtXSZL.Text.Trim();

            //}
            string strJLD = txtJLD.Text.Trim();
            string strJLY = txtJLY.Text.Trim();
            string strBC = txtBC.Text.Trim();

            ybCount[selectRow].strCZH = txtCZH.Text.Trim();

            //车号
            string strCH1 = this.cbCH.Text.Trim();
            string strCH2 = this.cbCH1.Text.Trim();
            ybCount[selectRow].strCH = strCH1 + strCH2;

            string strYKL = txtYKL.Text.Trim();
            string strZL = "";
            if (strYKL != "")
            {
                decimal YKL = Convert.ToDecimal(strYKL) / 1000;
                decimal JZ = Convert.ToDecimal(strSJZL) - YKL;
                strZL = JZ.ToString();
            }
            else
            {
                strZL = strSJZL;
            }

            if (cbWLMC.SelectedValue == null || cbWLMC.SelectedValue.ToString() == "")
            {
                //MessageBox.Show("请选择物料信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //cbWLMC.Focus();
                //return;
            }
            else
            {
                ybCount[selectRow].strWLID = cbWLMC.SelectedValue.ToString().Trim();
                ybCount[selectRow].strWLMC = cbWLMC.Text.Trim();
            }

            if (cbFHDW.SelectedValue == null || cbFHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strFHFDM = cbFHDW.SelectedValue.ToString().Trim();
            }

            if (cbSHDW.SelectedValue == null || cbSHDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strSHFDM = cbSHDW.SelectedValue.ToString().Trim();
            }

            if (cbCYDW.SelectedValue == null || cbCYDW.SelectedValue.ToString() == "")
            { }
            else
            {
                ybCount[selectRow].strCYDW = cbCYDW.SelectedValue.ToString().Trim();
            }

            if (cbLX.SelectedValue != null)
            {
                if (cbLX.Text != sLX)
                {
                    ybCount[selectRow].strLX = cbLX.SelectedValue.ToString().Trim();
                }
            }

            string ycsfyc = "";
            //3.8号新加
            if (chbSFYC.Checked == true)
            {
                ycsfyc = "1";
            }
            string strProvider = "";
            if (cbProvider.SelectedValue != null)
                strProvider = cbProvider.SelectedValue.ToString();
            print.printJZ = strZL;
            string strBZ = tbBZ.Text.Trim();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveYCJLData";
            ccp.ServerParams = new object[] { strZYBH, ybCount[selectRow].strYBH, txtHTH.Text.Trim(), txtHTXMH.Text.Trim(), txtLH.Text.Trim(),
                txtZS.Text.Trim(), ybCount[selectRow].strCZH, ybCount[selectRow].strCH, ybCount[selectRow].strWLID, ybCount[selectRow].strWLMC, 
                ybCount[selectRow].strFHFDM, ybCount[selectRow].strFHKCD,ybCount[selectRow].strSHFDM, ybCount[selectRow].strSHKCD, ybCount[selectRow].strLX,
                ybCount[selectRow].strCYDW, strJLDID, ybCount[selectRow].strYBZZ, ybCount[selectRow].strYBPZ,ybCount[selectRow].strYBJZ, 
                strZL,strJLD, strJLY, strBC, ycsfyc, 
                strYKL,y_IFSAMPLING, y_IFACCEPT, y_DRIVERNAME, y_DRIVERIDCARD, 
                txtPJBH.Text.Trim(),txtDFJZ.Text.Trim(), strCode,strProvider,strBZ};

            ccp.IfShowErrMsg = false;

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode != 0)
            {
              //  MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                MessageBox.Show("数据操作失败！" + "已存在该条数据");
                return false;
            }

            //string errInfo = "";
            //if (ccp.ReturnCode == -1)
            //{
            //    errInfo = ccp.ReturnInfo;
            //}
            //if (errInfo != "")
            //{
            //    if (errInfo.IndexOf("ORA-01401") >= 0)
            //    {
            //        MessageBox.Show("值输入过大，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cbCH1.Focus();
            //        DeleteTPData();
            //    }
            //}

            btnGPBC.Enabled = false;

            return true;
        }
        /// <summary>
        /// 保存钢坯二次计量
        /// </summary>
        private bool SaveGPDataTwo()
        {
            string eczl = "";
            string ecjld = "";
            string ecjly = "";
            string ecbc = "";
            string ecjldid = "";

            string strgzlx = ""; //判端是方坯还是板坯

            string strSJZL = "";
            //if (ImageJZ != null)
            //{
            //    strSJZL = ImageJZ;
            //}
            //else
            //{
            strSJZL = txtZL.Text.Trim();
            if (strSJZL == "")
                strSJZL = txtXSZL.Text.Trim();
            //}

            string strJLD = strJLDID;
            string strJLY = txtJLY.Text.Trim();
            string strBC = txtBC.Text.Trim();

            //车号
            string strCH1 = this.cbCH.Text.Trim();
            string strCH2 = this.cbCH1.Text.Trim();
            stCH = strCH1 + strCH2;

            string strYKL = txtYKL.Text.Trim();
            string strECZL = "";
            if (strYKL != "")
            {
                decimal YKL = Convert.ToDecimal(strYKL) / 1000;
                decimal dJZ = Convert.ToDecimal(strSJZL) - YKL;
                strECZL = dJZ.ToString();
            }
            else
            {
                strECZL = strSJZL;
            }

            string strMZ = "";//毛重
            string strPZ = "";//皮重
            string strJZ = "";//净重
            string strMZJLD = "";//毛重计量点
            string strMZJLY = "";//毛重计量员
            string strMZJLSJ = "";//毛重计量时间
            string strMZJLBC = "";//毛重计量班次
            string strPZJLD = "";//皮重计量点
            string strPZJLY = "";//皮重计量员
            string strPZJLSJ = "";//皮重计量时间
            string strPZJLBC = "";//皮重计量班次

            string strWZBDTM = "";//完整磅单条码
            strWZBDTM = strCode;
            if (strECZL == "")
            {
                strECZL = "0";
            }
            if (strYCZL == "")
            {
                strYCZL = "0";
            }
            if (Convert.ToDecimal(strECZL) <= Convert.ToDecimal(strYCZL))
            {
                strMZ = strYCZL;
                strPZ = strECZL;
                Decimal JZ = Math.Round(Convert.ToDecimal(strMZ) - Convert.ToDecimal(strPZ), 3);
                strJZ = JZ.ToString();
                strMZJLD = strBFBH.ToString().Trim();
                strMZJLY = strYCJLY.ToString().Trim();
                strMZJLSJ = strYCJLSJ.ToString();//毛重计量时间
                strMZJLBC = strYCJLBC.ToString().Trim();

                strPZJLD = strJLDID;
                strPZJLY = strJLY;
                strPZJLBC = strBC;
                strPZJLSJ = "";//皮重计量时间

                if (s_DFJZ != "")
                {
                    s_CZ = (JZ - Convert.ToDecimal(s_DFJZ)).ToString();
                }
                else
                {
                    s_CZ = "";
                }

                if (txtZS2.Text == "")
                {
                    txtZS2.Text = "0";
                }
                if (txtZS3.Text == "")
                {
                    txtZS3.Text = "0";
                }

                if (strJZ != "")
                {
                    GPJZ = strJZ;
                }
                else
                {
                    GPJZ = "0";
                }
            }
            if (Convert.ToDecimal(strECZL) > Convert.ToDecimal(strYCZL))
            {
                strMZ = strECZL;
                strPZ = strYCZL;
                Decimal JZ = Convert.ToDecimal(strMZ) - Convert.ToDecimal(strPZ);
                strJZ = JZ.ToString();

                strMZJLD = strJLDID;
                strMZJLY = strJLY;
                strMZJLBC = strBC;

                strPZJLD = strBFBH;
                strPZJLY = strYCJLY;
                strPZJLSJ = strYCJLSJ.ToString();//皮重计量时间
                strPZJLBC = strYCJLBC;

                if (s_DFJZ != "")
                {
                    s_CZ = (JZ - Convert.ToDecimal(s_DFJZ)).ToString();
                }
                else
                {
                    s_CZ = "";
                }

                if (txtZS2.Text == "")
                {
                    txtZS2.Text = "0";
                }
                if (txtZS3.Text == "")
                {
                    txtZS3.Text = "0";
                }

                if (strJZ != "")
                {
                    GPJZ = strJZ;
                }
                else
                {
                    GPJZ = "0";
                }
            }

            //多炉处理
            eczl = strECZL;
            ecjld = txtJLD.Text.Trim();
            ecjly = txtJLY.Text.Trim();
            ecbc = txtBC.Text.Trim();
            ecjldid = strJLDID;

            if (cbWLMC.SelectedValue == null || cbWLMC.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stWLID = cbWLMC.SelectedValue.ToString().Trim();
                stWLMC = cbWLMC.Text.Trim();
            }

            if (cbFHDW.SelectedValue == null || cbFHDW.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stFHFDM = cbFHDW.SelectedValue.ToString().Trim();
            }

            if (cbSHDW.SelectedValue == null || cbSHDW.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stSHFDM = cbSHDW.SelectedValue.ToString().Trim();
            }

            if (cbCYDW.SelectedValue == null || cbCYDW.SelectedValue.ToString() == "")
            {
            }
            else
            {
                stCYDW = cbCYDW.SelectedValue.ToString().Trim();
            }

            if (cbLX.SelectedValue != null)
            {
                if (cbLX.Text != sLX)
                {
                    stLX = cbLX.SelectedValue.ToString().Trim();
                }
            }

            string strSFYC = "";
            //3.8号新加
            if (chbSFYC.Checked == true)
            {
                strSFYC = "1";
            }

            if (txtLH.Text.Trim().Length == 9) //根据炉号区分是板坯还是方坯，10位是板坯，9位是方坯   这里没用到
            {
                strgzlx = "0";
            }
            else
            {
                strgzlx = "1";
            }

            //打印重量赋值
            print.printMZ = string.Format("{0:F3}", Convert.ToDecimal(strMZ));
            print.printPZ = string.Format("{0:F3}", Convert.ToDecimal(strPZ));
            print.printJZ = string.Format("{0:F3}", Convert.ToDecimal(strJZ));
    
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveGPECJLData";
            ccp.ServerParams = new object[] { strZYBH, txtHTH.Text.Trim()/*stHTH*/, stHTXMH, txtLH.Text.Trim(), txtZS.Text.Trim(),
                stCZH, stCH, stWLID, stWLMC, stFHFDM, 
                stCYDW, stSHFDM, stLX, stYBZZ, stYBPZ,
                stYBJZ, strMZ, strMZJLD, strMZJLY, strMZJLSJ, 
                strMZJLBC, strPZ, strPZJLD, strPZJLY, strPZJLSJ, 
                strPZJLBC, strYCBDTM, strXCRKSJ, strXCCKSJ, strXCQR, 
                strXCKGY, strZCRKSJ, strZCCKSJ, strZCQR, strZCKGY, 
                strJZ, strQYY, strWZBDTM, strYCSFYC, strSFYC, 
                strYKL, strJLD, s_SAMPLETIME, s_SAMPLEPLACE, s_SAMPLEFLAG,
                s_UNLOADPERSON, s_UNLOADTIME, s_UNLOADPLACE, s_CHECKPERSON, s_CHECKTIME, 
                s_CHECKPLACE, s_CHECKFLAG, s_DRIVERNAME, s_DRIVERIDCARD, s_SENDERSTORE,
                s_IFSAMPLING, stSHKCD, s_IFACCEPT, s_REWEIGHTFLAG, s_REWEIGHTTIME, 
                s_REWEIGHTPLACE, s_REWEIGHTPERSON, txtPJBH.Text.Trim(), s_DFJZ, s_CZ, 
                cbLS.Text.Trim(), strgzlx, eczl, ecjld, ecjly, 
                ecbc, ecjldid };

            ccp.IfShowErrMsg = false;

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }

            //if (ccp.ReturnObject != null)
            //{
            m_SaveImageSign = 0;
            m_GraspImageSign = 0;

            if (ccp.ReturnObject != null)
                s_Guid = ccp.ReturnObject.ToString();//接收返回值
            else
                s_Guid = "";

            ImageGPZL = eczl;
            //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
            //Invoke(m_MainThreadCapPicture); //用委托抓图
            //}

            if (txtZS2.Text == "0")
            {
                txtZS2.Text = "";
            }
            if (txtZS3.Text == "0")
            {
                txtZS3.Text = "";
            }

            //判断车号是否输入过长
            //string errInfo = "";
            //if (ccp.ReturnCode == -1)
            //{
            //    errInfo = ccp.ReturnInfo;
            //}
            //if (errInfo != "")
            //{
            //    if (errInfo.IndexOf("ORA-01401") >= 0)
            //    {
            //        MessageBox.Show("值输入过大，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        cbCH1.Focus();
            //    }
            //    else
            //    {
            //        MessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //}

            //if (errInfo == "")
            //{
            //保存历史皮重
            CoreClientParam ccpPZ = new CoreClientParam();
            ccpPZ.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpPZ.MethodName = "SavePZData";
            ccpPZ.ServerParams = new object[] { stCH, strPZ };

            this.ExecuteNonQuery(ccpPZ, CoreInvokeType.Internal);
            // }

            btnGPBC.Enabled = true;

            return true;
        }

        /// <summary>
        /// 保存钢坯方坯计量主表数据
        /// </summary>
        private void SaveGPFPData()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveGPFPData";
            string strStoveNo = txtLH.Text.Trim();
            string strWeight = "";
            if (txtZL.Text.Trim() != "")
                strWeight = txtZL.Text.Trim();
            else
                strWeight = txtXSZL.Text.Trim();

            string strCount = txtZS.Text.Trim();
            string strZS = txtZS.Text.Trim();

            ccp.ServerParams = new object[] { strStoveNo, txtHTH.Text.Trim()/*stHTH*/, stHTXMH, stFHFDM, stSHFDM, stWLID, stWLMC, GZ, GG, CD, stLX, strJLDID, strWeight, strZS, strCount, 
                txtJLY.Text.Trim(), txtBC.Text.Trim() };
            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }

        /// <summary>
        /// 查询钢坯一次计量数据
        /// </summary>
        private void QueryGPDataOne()
        {
            if (ifStart == "0" || txtLH.Text.Trim() == "")
            {
                return;
            }

            ClearYBData();
            ClearYCBData();
            ClearECJLBData();

            string strGPLH = txtLH.Text.Trim();
            QueryYCJLData(strGPLH);
            panelYCSP.Visible = true;
            if (strYCJL != "")
            {
                QueryYCPic();
                ybCount[selectRow].strQXPZBZ = "0";
            }
            if (ybCount[selectRow].strQXPZBZ == "1")
            {
                chbQXPZ.Checked = true;
            }
            else
            {
                chbQXPZ.Checked = false;
            }
            QueryPZData();

            DisPlayShow();  //刷显示屏
        }

        /// <summary>
        /// 炉号控件离开事件,查询合同号等信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLH_Leave(object sender, EventArgs e)
        {


            if (strJLDID == "")
            {
                MessageBox.Show("请先选择计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string strStoveNo = txtLH.Text.Trim();

            if (strStoveNo != "")
                if (QueryTechCard(strStoveNo, txtHTH, txtZS) == false)
                    return;

            if (txtLH.Text.Trim() == "")
            {
                ifStart = "0";
                btnGPBC.Visible = false;
                btnBC.Visible = true;
                return;
            }

            //ClearGPData();
            //ClearImageAndWeight();
            ifStart = "1";
            QueryGPData();
            //cbJLLX.Text = "";
            //cbJLLX.Enabled = false;
            QueryGPDataOne();
            btnGPBC.Visible = true;
            btnBC.Visible = false;
            btnGPBC.Enabled = true;
        }

        /// <summary>
        /// 清空钢坯控件数据
        /// </summary>
        private void ClearGPData()
        {
            this.txtCZH.Text = "";
            //cbCH.Text = "";
            //cbCH1.Text = "";
            txtHTH.Text = "";
            txtHTXMH.Text = "";
            chbQXPZ.Checked = false;
            txtLH2.Text = "";
            txtLH3.Text = "";
            txtZS2.Text = "";
            txtZS3.Text = "";
            txtZL.Text = "";
            txtYKL.Text = "";
            //txtXSZL.Text = "";
            cbFHDW.Text = "";
            cbSHDW.Text = "";
            cbCYDW.Text = "";
            cbWLMC.Text = "";
            cbLX.Text = "";
            cbJLLX.Text = "";
            txtPJBH.Text = "";
            cbLS.Text = "";

            ////刷新打印重量
            //print.printMZ = "";
            //print.printPZ = "";
            //print.printJZ = "";

            txtMZ.Text = "";
            txtPZ.Text = "";
            txtJZ.Text = "";

            strYB = "";
            strYCJL = "";
            s_DFJZ = "";

            GZ = "";
            GG = "";
            CD = "";

            s_Guid = "";
            ImageGPZL = "";
            querycs = 0;

            m_SaveImageSign = 0;
            m_GraspImageSign = 0;
        }

        /// <summary>
        /// 钢坯保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGPBC_Click(object sender, EventArgs e)
        {
            //是否检测红外
            //if (chb_AutoInfrared.Checked == true)
            //{
            //    //前后端红外都被挡，给予保存提示
            //    if (StatusBack.Connected == false && StatusFront.Connected == false)
            //    {
            //        if (DialogResult.No == MessageBox.Show("前、后端红外都被挡，请确认停车到位，是否继续保存", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            //            return;
            //    }
            //}

            if (strPoint == "")
            {
                MessageBox.Show("请先选择计量点！");
                return;
            }

            if (cbJLLX.Enabled == true && cbJLLX.Text.Trim() == "复磅")
            {
                MessageBox.Show("“复磅”不能选择，请重新选择计量类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtZS.Text.Trim() == "" && txtLH.Text.Trim().Length>0)//20110301彭海波修改
            {
                MessageBox.Show("请录入支数！");
                return;
            }

            if (ControlProve() == false)
            {
                return;
            }

            if (txtLH.Text.Trim().Length > 0 && strYCJL == "1" && txtZS.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入支数或块数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZS.Focus();
                return;
            }
            if (txtLH2.Text.Trim().Length > 0 && strYCJL == "1" && txtZS2.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入支数或块数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZS2.Focus();
                return;
            }
            if (txtLH3.Text.Trim().Length > 0 && strYCJL == "1" && txtZS3.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入支数或块数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZS3.Focus();
                return;
            }


            this.Cursor = Cursors.WaitCursor;
            strBCSJ = "1";

            //2012.3.28.16：50 打印机 参数
            printInfoClear();
            strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
            //print.printCZH = txtCZH.Text.Trim();
            print.printCH = cbCH.Text.Trim() + cbCH1.Text.Trim();
            print.printHTH = txtHTH.Text.Trim();
            print.printWLMC = cbWLMC.Text.Trim();
            print.printFHDW = cbFHDW.Text.Trim();
            print.printSHDW = cbSHDW.Text.Trim();
            print.printCYDW = cbCYDW.Text.Trim();
            print.printJLLX = cbJLLX.Text.Trim();
            print.printJLD = txtJLD.Text.Trim();
            print.printJLY = txtJLY.Text.Trim();
            //add by luobin
            print.printYKL = txtYKL.Text.Trim();
            print.printYKBL = s_YKBL;
            //print.pringJLCS = strYCJL;

            print.printLH = txtLH.Text.Trim();
            print.printZS = txtZS.Text.Trim();
            print.printGZ = GZ;
            print.printGG = GG;

            print.printLH1 = txtLH.Text.Trim();
            print.printZS1 = txtZS.Text.Trim();
            print.printLH2 = txtLH2.Text.Trim();
            print.printZS2 = txtZS2.Text.Trim();
            print.printLH3 = txtLH3.Text.Trim();
            print.printZS3 = txtZS3.Text.Trim();
            print.printAdviseSpec = this.strAdviseSpec;
            print.printZZJY = this.strZZJY;



            if (strYCJL != "" || strQXPZ != "" )
            {
                if (txtLH.Text.Trim().Length != 0 && txtLH2.Text.Trim().Length != 0)
                {
                    //if (SaveGPData() == false)
                    if (SaveBPHistory() == false)
                    {
                        this.Cursor = Cursors.Default;
                        strBCSJ = "0";
                        return;
                    }
                }
                else
                {
                    //if (SaveGPTwoData() == false)
                    if (SaveFPHistory() == false)
                    {

                        this.Cursor = Cursors.Default;
                        strBCSJ = "0";
                        return;
                    }
                }              
            }
            else
            {

                if (cbLS.Text.Trim() == "还不是最后一炉")
                {
                    if (SaveGPTwoData() == false)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                }
                if (cbLS.Text.Trim() == "")
                {
                    if (AddYCJLData() == false)
                    {
                        strBCSJ = "0";
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }

            }

            if (cbCH.SelectedIndex < 0)
            {
                this.DownLoadCarNo();
            }

            if (strYCJL == "")
            {
                DisPlayShowForFirst();
            }
            if (strYCJL == "1")
            {
                DisPlayShowForSecond();
            }


            //有错误
            m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
            Invoke(m_MainThreadCapPicture); //用委托抓图
            //打印磅单
            //Print();

            //查询一次计量表
            m_BindUltraGridDelegate = new BindUltraGridDelegate(QueryYCBData);
            BeginInvoke(m_BindUltraGridDelegate);

            dtQX.Rows.Clear();
            dtQX.Columns.Clear();
            ultraChart1.DataSource = dataTable6;
            ksht = 1;

            ClearControlData();
            ClearControl();
            ClearQXPZData();
            if (strYB == "1")
            {
                ClearYBData();
            }
            

            //曲线图表刷新
            if (m_nPointCount > 0)
            {
                for (int i = 0; i < m_nPointCount; i++)
                {
                    if (m_PoundRoomArray[i].POINTNAME.Trim() == ultraGrid2.ActiveRow.Cells["FS_POINTNAME"].Value.ToString().Trim())
                    {
                        dtQX.Rows.Clear();
                        dtQX.Columns.Clear();
                        ultraChart1.DataSource = dataTable6;
                        BackZeroSign[i] = 1; //BackZeroSign = 1，意思就是车子在上称过程中如出现重量多次稳定，可以继续绘图；如是下称，则不准再画。
                    }
                }
            }

            ifStart = "0";//保存后重新恢复启动
            cbJLLX.Enabled = true;
            //strZYBH = ""; //清除Guid

            //if (chb_Autocontrol.Checked == true)
            //{
            //    if (m_PoundRoomArray[m_iSelectedPound].POINTID == "K01" || m_PoundRoomArray[m_iSelectedPound].POINTID == "K02")
            //        m_PoundRoomArray[m_iSelectedPound].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0x00, (byte)0);
            //}

            m_PoundRoomArray[m_iSelectedPound].ClearCardNoAndGuid();
            m_PoundRoomArray[m_iSelectedPound].CardNo = "";
            m_PoundRoomArray[m_iSelectedPound].ReaderGUID = "";
            txtZL.Text = "";

            if (strYCJL == "1")
            {
                ClearYCBData();
            }

          //  btnGPBC.Enabled = false; 2012年3月21号
            strBCSJ = "0";

            this.Cursor = Cursors.Default;
            #region 自动播放语音

            m_AlarmVoicePath = Constant.RunPath + "\\sound\\称重完成.wav";

            WriteLog("准备语音播放...");
            
            #endregion


           
        }

        /// <summary>
        /// 保存板坯数据，一车多炉取平均值 add by luobin
        /// </summary>
        /// <returns>保存是否成功</returns>
      
        //板坯注释 红钢没有
        private bool SaveBPHistory()
        {
        //    string strContractNo = txtHTH.Text.Trim();
        //    string strItemNo = txtHTXMH.Text.Trim();
        //    string strStoveNo1 = txtLH.Text.Trim();
        //    string strStoveNo2 = txtLH2.Text.Trim();
        //    string strStoveNo3 = txtLH3.Text.Trim();

        //    string strCount1 = txtZS.Text.Trim();
        //    if (strCount1.Trim() == "")
        //        strCount1 = "0";
        //    string strCount2 = txtZS2.Text.Trim();
        //    if (strCount2.Trim() == "")
        //        strCount2 = "0";
        //    string strCount3 = txtZS3.Text.Trim();
        //    if (strCount3.Trim() == "")
        //        strCount3 = "0";

        //    string strCardNo = txtCZH.Text.Trim();
        //    string strCarNo = cbCH.Text.Trim() + cbCH1.Text.Trim();
        //    string strMaterial = cbWLMC.SelectedValue.ToString().Trim();
        //    string strMaterialName = cbWLMC.Text.Trim();
        //    string strSender = cbFHDW.SelectedValue.ToString().Trim();
        //    string strTrans = cbCYDW.SelectedValue.ToString().Trim();
        //    string strReceiver = cbSHDW.SelectedValue.ToString().Trim();
        //    string strFlow = cbLX.SelectedValue.ToString().Trim();
        //    string strSecondCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
        //    string strSecondTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


        //    string strGrossWeight = "";
        //    string strTareWeight = "";
        //    string strNetWeight = "";

        //    string strGrossPoint = "";
        //    string strGrossWeighter = "";
        //    string strGrossTime = "";
        //    string strGrossShift = "";
        //    string strTarePoint = "";
        //    string strTareWeighter = "";
        //    string strTareTime = "";
        //    string strTareShift = "";

        //    string strDriverName = "";
        //    string strDriverCard = "";
        //    string strWeightNo = "";
        //    string strFirstCode = "";
        //    string strTermWeightNo = "";
        //    float flCurWeight = 0;

        //    if (txtZL.Text.Trim() == "")
        //        flCurWeight = Convert.ToSingle(Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]);
        //    else
        //        flCurWeight = Convert.ToSingle(Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]);

        //    m_ImageWeight = Math.Round(flCurWeight, 3).ToString();

        //    if (strYCJL != "")
        //    {
        //        strWeightNo = strZYBH;
        //        strFirstCode = strYCBDTM;
        //        strDriverName = s_DRIVERNAME;
        //        strDriverCard = s_DRIVERIDCARD;

        //        bool CurIsTare = false; //当次是否是皮重
        //        if (flCurWeight <= Convert.ToSingle(strYCZL))
        //            CurIsTare = true;
        //        if (CurIsTare)
        //        {
        //            strGrossWeight = strYCZL;
        //            strTareWeight = flCurWeight.ToString();
        //            strNetWeight = (Convert.ToSingle(strYCZL) - flCurWeight).ToString();

        //            strGrossPoint = strBFBH;
        //            strGrossWeighter = strYCJLY;
        //            strGrossTime = strYCJLSJ;
        //            strGrossShift = strYCJLBC;

        //            strTarePoint = strJLDID;
        //            strTareWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        //            strTareTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //            strTareShift = txtBC.Text.Trim();
        //        }
        //        else
        //        {
        //            strTareWeight = strYCZL;
        //            strGrossWeight = flCurWeight.ToString();
        //            strNetWeight = (flCurWeight - Convert.ToSingle(strYCZL)).ToString();

        //            strTarePoint = strBFBH;
        //            strTareWeighter = strYCJLY;
        //            strTareTime = strYCJLSJ;
        //            strTareShift = strYCJLBC;

        //            strGrossPoint = strJLDID;
        //            strGrossWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        //            strGrossTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //            strGrossShift = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder();

        //        }

        //    }
        //    else
        //    {
        //        if (strQXPZ != "")
        //        {
        //            strWeightNo = Guid.NewGuid().ToString();
        //            strTareWeight = strQXPZ;
        //            strGrossWeight = flCurWeight.ToString();
        //            strNetWeight = (flCurWeight - Convert.ToSingle(strQXPZ)).ToString();

        //            strTarePoint = qxJLD;
        //            strTareWeighter = qxJLY;
        //            strTareTime = qxJLSJ;
        //            strTareShift = qxBC;
        //            strTermWeightNo = qxCZBH;

        //            strGrossPoint = strJLDID;
        //            strGrossWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        //            strGrossTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //            strGrossShift = txtBC.Text.Trim();

        //        }
        //    }

        //    //string strNetWeight = Math.Round((Convert.ToSingle(strGrossWeight) - Convert.ToSingle(strTareWeight)), 3).ToString();

        //    print.printLH = txtLH.Text.Trim() + "  " + txtLH2.Text.Trim();
        //    print.printZS = (Convert.ToInt32(txtZS.Text.Trim()) + Convert.ToInt32(txtZS.Text.Trim())).ToString();
        //    //print.printMZ = strGrossWeight;
        //    //print.printPZ = strTareWeight;
        //    //print.printJZ = strNetWeight;
        //    print.printMZ = string.Format("{0:F3}", Convert.ToDecimal(strGrossWeight));
        //    print.printPZ = string.Format("{0:F3}", Convert.ToDecimal(strTareWeight));
        //    print.printJZ = string.Format("{0:F3}", Convert.ToDecimal(strNetWeight));

        //    CoreClientParam ccp = new CoreClientParam();
        //    ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
        //    ccp.MethodName = "SaveBPHistory";
        //    ccp.ServerParams = new object[] {strWeightNo,strContractNo,strItemNo,strStoveNo1,strStoveNo2,             
        //                                     strStoveNo3,strCount1,strCount2, strCount3,strCardNo,
        //                                     strCarNo,strMaterial,strMaterialName,strSender,strTrans,
        //                                     strReceiver,strFlow,strGrossWeight,strGrossPoint,strGrossWeighter, 
        //                                     strGrossTime,strGrossShift,strTareWeight,strTarePoint,strTareWeighter,
        //                                     strTareTime,strTareShift,strFirstCode,strSecondCode,strDriverName,
        //                                     strDriverCard,strSecondTime,strTermWeightNo };


        //    ccp.IfShowErrMsg = false;
        //    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        //    WriteLog("板坯二次计量保存 计量号:" + strZYBH + "  车号: " + ybCount[selectRow].strCH + "  返回代码 ：" + ccp.ReturnCode.ToString() + "     返回信息 ：" + ccp.ReturnInfo);
        //    if (ccp.ReturnCode != 0)
        //    {
        //        MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
        //        return false;
        //    }
            return true;
        }

        /// <summary>
        /// 保存钢坯数据，一车多炉算平均值(板坯用)
        /// </summary>
        private bool SaveGPData()
        {
            #region 钢坯保存方法一算平均数

            if (ifStart == "1" && strYCJL == "" || txtLH.Text.Trim() != "" && strYCJL == "")
            {
                if (strQXPZ == "")
                {
                    //钢坯一次计量数据保存
                    if (txtLH.Text != "")
                    {
                        ybCount[selectRow].strHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        ybCount[selectRow].strHTXMH = txtHTXMH.Text.Trim();
                        ybCount[selectRow].strWLID = jlWLID;
                        ybCount[selectRow].strWLMC = jlWLMC;
                        ybCount[selectRow].strLH = txtLH.Text.Trim();
                        ybCount[selectRow].strZS = txtZS.Text.Trim();
                        stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        stHTXMH = txtHTXMH.Text.Trim();
                        stWLID = jlWLID;
                        stWLMC = jlWLMC;
                        stLH = txtLH.Text.Trim();
                        stZS = txtZS.Text.Trim();

                        if (AddYCJLData() == false)
                        {
                            return false;
                        }
                    }
                    if (txtLH2.Text != "")
                    {
                        ybCount[selectRow].strHTH = txtHTH.Text.Trim();// sDDH;//合同号（订单号）
                        ybCount[selectRow].strHTXMH = txtHTXMH.Text.Trim();
                        ybCount[selectRow].strWLID = jlWLID;
                        ybCount[selectRow].strWLMC = jlWLMC;
                        ybCount[selectRow].strLH = txtLH2.Text.Trim();
                        ybCount[selectRow].strZS = txtZS2.Text.Trim();
                        stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        stHTXMH = txtHTXMH.Text.Trim();
                        stWLID = jlWLID;
                        stWLMC = jlWLMC;
                        stLH = txtLH2.Text.Trim();
                        stZS = txtZS2.Text.Trim();
                        //AddTPData();
                        //SaveImageThread = new Thread(new ThreadStart(AddTPData));
                        //SaveImageThread.Start();
                        strZYBH = "";
                        //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                        //Invoke(m_MainThreadCapPicture); //用委托二次抓图

                        if (AddYCJLData() == false)
                        {
                            return false;
                        }
                    }
                    if (txtLH3.Text != "")
                    {
                        ybCount[selectRow].strHTH = txtHTH.Text.Trim();// sDDH;//合同号（订单号）
                        ybCount[selectRow].strHTXMH = txtHTXMH.Text.Trim();
                        ybCount[selectRow].strWLID = jlWLID;
                        ybCount[selectRow].strWLMC = jlWLMC;
                        ybCount[selectRow].strLH = txtLH3.Text.Trim();
                        ybCount[selectRow].strZS = txtZS3.Text.Trim();
                        stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        stHTXMH = txtHTXMH.Text.Trim();
                        stWLID = jlWLID;
                        stWLMC = jlWLMC;
                        stLH = txtLH3.Text.Trim();
                        stZS = txtZS3.Text.Trim();

                        strZYBH = "";
                        //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                        //Invoke(m_MainThreadCapPicture); //用委托三次抓图

                        if (AddYCJLData() == false)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //钢坯二次计量数据保存（有期限皮重）
                    if (txtLH.Text != "")
                    {
                        ybCount[selectRow].strHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        ybCount[selectRow].strHTXMH = txtHTXMH.Text.Trim();
                        ybCount[selectRow].strWLID = jlWLID;
                        ybCount[selectRow].strWLMC = jlWLMC;
                        ybCount[selectRow].strLH = txtLH.Text.Trim();
                        ybCount[selectRow].strZS = txtZS.Text.Trim();
                        stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        stHTXMH = txtHTXMH.Text.Trim();
                        stWLID = jlWLID;
                        stWLMC = jlWLMC;
                        stLH = txtLH.Text.Trim();
                        stZS = txtZS.Text.Trim();
                        //AddTPData();
                        //SaveImageThread = new Thread(new ThreadStart(AddTPData));
                        //SaveImageThread.Start();
                        if (AddECJLBData() == false)
                            return false;
                            SaveGPFPData();//保存方坯
                    }
                    if (txtLH2.Text != "")
                    {
                        ybCount[selectRow].strHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        ybCount[selectRow].strHTXMH = txtHTXMH.Text.Trim();
                        ybCount[selectRow].strWLID = jlWLID;
                        ybCount[selectRow].strWLMC = jlWLMC;
                        ybCount[selectRow].strLH = txtLH2.Text.Trim();
                        ybCount[selectRow].strZS = txtZS2.Text.Trim();
                        stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        stHTXMH = txtHTXMH.Text.Trim();
                        stWLID = jlWLID;
                        stWLMC = jlWLMC;
                        stLH = txtLH2.Text.Trim();
                        stZS = txtZS2.Text.Trim();
                        //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                        //Invoke(m_MainThreadCapPicture); //用委托二次抓图

                        if (AddECJLBData() == false)
                            SaveGPFPData();

                    }
                    if (txtLH3.Text != "")
                    {
                        ybCount[selectRow].strHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        ybCount[selectRow].strHTXMH = txtHTXMH.Text.Trim();
                        ybCount[selectRow].strWLID = jlWLID;
                        ybCount[selectRow].strWLMC = jlWLMC;
                        ybCount[selectRow].strLH = txtLH3.Text.Trim();
                        ybCount[selectRow].strZS = txtZS3.Text.Trim();
                        stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                        stHTXMH = txtHTXMH.Text.Trim();
                        stWLID = jlWLID;
                        stWLMC = jlWLMC;
                        stLH = txtLH3.Text.Trim();
                        stZS = txtZS3.Text.Trim();
                        //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                        //Invoke(m_MainThreadCapPicture); //用委托三次抓图
                        if (AddECJLBData() == false)
                            return false;
                            SaveGPFPData();

                    }
                }
            }
            if (ifStart == "1" && strYCJL == "1" || txtLH.Text.Trim() != "" && strYCJL == "1")
            {
                cbJLLX.Text = "";
                //钢坯二次计量数据保存
                if (txtLH.Text != "")
                {
                    stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                    stHTXMH = txtHTXMH.Text.Trim();
                    stWLID = jlWLID;
                    stWLMC = jlWLMC;
                    stLH = txtLH.Text.Trim();
                    stZS = txtZS.Text.Trim();
                    string gpCH = txtLH.Text.Trim();
                    QueryYCJLData(gpCH);
                    //UpdateTPData();
                    if (AddECJLData() == false)
                        return false;
                        SaveGPFPData();

                }
                if (txtLH2.Text != "")
                {
                    stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                    stHTXMH = txtHTXMH.Text.Trim();
                    stWLID = jlWLID;
                    stWLMC = jlWLMC;
                    stLH = txtLH2.Text.Trim();
                    stZS = txtZS2.Text.Trim();
                    string gpCH = txtLH2.Text.Trim();
                    QueryYCJLData(gpCH);
                    //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                    //Invoke(m_MainThreadCapPicture); //用委托二次抓图
                    //UpdateTPData();

                    if (AddECJLData() == false)
                        return false;
                        SaveGPFPData();

                }
                if (txtLH3.Text != "")
                {
                    stHTH = txtHTH.Text.Trim();//sDDH;//合同号（订单号）
                    stHTXMH = txtHTXMH.Text.Trim();
                    stWLID = jlWLID;
                    stWLMC = jlWLMC;
                    stLH = txtLH3.Text.Trim();
                    stZS = txtZS3.Text.Trim();
                    string gpCH = txtLH3.Text.Trim();
                    QueryYCJLData(gpCH);
                    //m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                    //Invoke(m_MainThreadCapPicture); //用委托三次抓图
                    ////UpdateTPData();
                    if (AddECJLData() == false)
                        return false;
                        SaveGPFPData();
 
                }
            }

            return true;
            #endregion
        }

        /// <summary>
        /// 检查是否已轧材，没找到卡片返回0，已轧制返回1，未轧制返回2,20110301彭海波增加
        /// </summary>
        private int IsZZCompleted(string str_stoveno)
        {
            int int_isqyg = -1;
            try
            {
                string strSql = "SELECT FN_JZ_WEIGHT,FS_ZC_BATCHNO from IT_FP_TECHCARD WHERE FS_GP_STOVENO='" + str_stoveno.Trim() + "'";
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.car.StorageInfo";
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { strSql };
                DataTable dt_temp = new DataTable();
                ccp.SourceDataTable = dt_temp;
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                if (dt_temp.Rows.Count > 0)
                {
                    string str_FN_JZ_WEIGHT = dt_temp.Rows[0]["FN_JZ_WEIGHT"].ToString().Trim();
                    string str_FS_ZC_BATCHNO = dt_temp.Rows[0]["FS_ZC_BATCHNO"].ToString().Trim();
                    if (str_FN_JZ_WEIGHT.Equals("0") || str_FN_JZ_WEIGHT.Equals("") || str_FS_ZC_BATCHNO.Equals(""))
                    {
                        int_isqyg = 2;
                    }
                    else
                    {//当轧材重量和轧制号都有值时，才确认开始轧制或已经轧完
                        int_isqyg = 1;
                    }
                }
                else
                {
                    int_isqyg = 0;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                return int_isqyg;
            }
            return int_isqyg;
        }

        /// <summary>
        /// 保存方坯数据，前车重车做为后车的皮 add by luobin
        /// </summary>
        /// <returns>保存是否成功</returns>
        private bool SaveFPHistory()
        {

            string strDFJZ = txtDFJZ.Text.Trim();//对方净重


            string strContractNo = txtHTH.Text.Trim();
            string strItemNo = txtHTXMH.Text.Trim();
            string strStoveNo = txtLH.Text.Trim();
            //20110302彭海波增加，开始
            int i_ZZCheckFlag = IsZZCompleted(strStoveNo);
            if (i_ZZCheckFlag == 0)
            {
                MessageBox.Show("没有找到炉号" + strStoveNo+"对应的卡片预报，不能当做钢坯计量，请以其他物资计量或与预报单位联系！");
                return false;
            }
            else if (i_ZZCheckFlag==1)
            {
                MessageBox.Show("炉号" + strStoveNo + "钢坯已轧制完成，请检查炉号！");
                return false;
            }
            //结束
            string strCount = txtZS.Text.Trim();
            if (strCount.Trim() == "")
                strCount = "0";


            string strCardNo = txtCZH.Text.Trim();
            string strCarNo = cbCH.Text.Trim() + cbCH1.Text.Trim();
            //string strMaterial = cbWLMC.SelectedValue.ToString().Trim();

            //if (strMaterial == "")
            //strMaterial = strGPMaterial;
            string strMaterial = strGPMaterial;
            string strMaterialName = cbWLMC.Text.Trim();
            string strSender = cbFHDW.SelectedValue.ToString().Trim();
            string strTrans = cbCYDW.SelectedValue.ToString().Trim();
            string strReceiver = cbSHDW.SelectedValue.ToString().Trim();
            string strFlow = cbLX.SelectedValue.ToString().Trim();
            string strSecondCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
            string strSecondTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            WriteLog("方坯数据保存: 炉号： =" + txtLH.Text.Trim() + "车号：= " + cbCH.Text.Trim() + cbCH1.Text.Trim() + "    strMaterial:=" + strMaterial + "   strGPMaterial :=" + strGPMaterial + "   strMaterialName := " + strMaterialName);

            string strGrossWeight = "";
            string strTareWeight = "";
            string strNetWeight = "";

            string strGrossPoint = "";
            string strGrossWeighter = "";
            string strGrossTime = "";
            string strGrossShift = "";
            string strTarePoint = "";
            string strTareWeighter = "";
            string strTareTime = "";
            string strTareShift = "";

            string strDriverName = "";
            string strDriverCard = "";
            string strWeightNo = "";
            string strFirstCode = "";
            string strTermWeightNo = "";
            float flCurWeight = 0;

            if (txtZL.Text.Trim() == "")
                flCurWeight = Convert.ToSingle(Convert.ToDecimal(txtXSZL.Text.Trim()) - s_toZore[m_iSelectedPound]);
            else
                flCurWeight = Convert.ToSingle(Convert.ToDecimal(txtZL.Text.Trim()) - s_toZore[m_iSelectedPound]);


            m_ImageWeight = Math.Round(flCurWeight, 3).ToString();

            string strFlag = cbLS.Text.Trim();

            if (strYCJL != "")
            {
                strWeightNo = strZYBH;
                strFirstCode = strYCBDTM;
                strDriverName = s_DRIVERNAME;
                strDriverCard = s_DRIVERIDCARD;

                bool CurIsTare = false; //当次是否是皮重
                if (flCurWeight <= Convert.ToSingle(strYCZL))
                    CurIsTare = true;
                if (CurIsTare)
                {
                    strGrossWeight = strYCZL;
                    strTareWeight = flCurWeight.ToString();
                    strNetWeight = (Convert.ToSingle(strYCZL) - flCurWeight).ToString();

                    strGrossPoint = strBFBH;
                    strGrossWeighter = strYCJLY;
                    strGrossTime = strYCJLSJ;
                    strGrossShift = strYCJLBC;

                    strTarePoint = strJLDID;
                    strTareWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    strTareTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    strTareShift = txtBC.Text.Trim();
                }
                else
                {
                    strTareWeight = strYCZL;
                    strGrossWeight = flCurWeight.ToString();
                    strNetWeight = (flCurWeight - Convert.ToSingle(strYCZL)).ToString();

                    strTarePoint = strBFBH;
                    strTareWeighter = strYCJLY;
                    strTareTime = strYCJLSJ;
                    strTareShift = strYCJLBC;

                    strGrossPoint = strJLDID;
                    strGrossWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    strGrossTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    strGrossShift = txtBC.Text.Trim();

                }

            }
            else
            {
                if (strQXPZ != "")
                {
                    strZYBH = Guid.NewGuid().ToString();
                    strWeightNo = strZYBH;
                    strTareWeight = strQXPZ;
                    strGrossWeight = flCurWeight.ToString();
                    strNetWeight = (flCurWeight - Convert.ToSingle(strQXPZ)).ToString();

                    strTarePoint = qxJLD;
                    strTareWeighter = qxJLY;
                    strTareTime = qxJLSJ;
                    strTareShift = qxBC;
                    strTermWeightNo = qxCZBH;

                    strGrossPoint = strJLDID;
                    strGrossWeighter = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    strGrossTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    strGrossShift = txtBC.Text.Trim();

                }
            }

            //string strNetWeight = Math.Round((Convert.ToSingle(strGrossWeight) - Convert.ToSingle(strTareWeight)),3).ToString();

            print.printLH = txtLH.Text.Trim();
            print.printMZ = string.Format("{0:F3}", Convert.ToDecimal(strGrossWeight));
            print.printPZ = string.Format("{0:F3}", Convert.ToDecimal(strTareWeight));
            print.printJZ = string.Format("{0:F3}", Convert.ToDecimal(strNetWeight));

            WriteLog("方坯二次计量开始保存 计量号:" + strZYBH + "  车号: " + strCarNo + "  炉号： " + txtLH.Text.Trim());
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "SaveFPHistory";
            ccp.ServerParams = new object[] {strWeightNo,strContractNo,strItemNo,strStoveNo,strCount,
                                             strCardNo,strCarNo,strMaterial,strMaterialName,strSender,
                                             strTrans,strReceiver,strFlow,strGrossWeight,strGrossPoint,
                                             strGrossWeighter,strGrossTime,strGrossShift,strTareWeight,strTarePoint,
                                             strTareWeighter,strTareTime,strTareShift,strFirstCode,strSecondCode,
                                             strDriverName,strDriverCard,strSecondTime,strTermWeightNo,strFlag,strDFJZ };


            ccp.IfShowErrMsg = false;
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            WriteLog("方坯二次计量保存 计量号:" + strZYBH + "  车号: " + strCarNo + "  炉号： " + txtLH.Text.Trim() + "  返回代码 ：" + ccp.ReturnCode.ToString() + "     返回信息 ：" + ccp.ReturnInfo);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("数据操作失败！" + ccp.ReturnInfo);
                return false;
            }
            else
            {
                string strDate = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                string strIP = baseinfo.getIPAddress();
                string strMAC = baseinfo.getMACAddress();
                string p_UPDATER = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                string m_Memo ="FN_GP_TOTALCOUNT(钢坯条数)在原来过磅记录基础上增加 " + strCount 
                    + "，FN_JJ_WEIGHT(钢坯重量)在原来基础上增加(" + strGrossWeight + "-" + strTareWeight
                    + ")，FS_GP_Flow=" + strFlow + "，FS_GP_COMPLETEFLAG=1；如果流向为SH000099，则程序还进行了验收";
                baseinfo.ob = this.ob;
                baseinfo.insertLog(strDate, "修改", p_UPDATER, strIP, strMAC, m_Memo, strStoveNo, strStoveNo, "", "", "", "IT_FP_TECHCARD", "汽车秤/过磅计量/钢坯保存按钮");

            }
            return true;
        }

        /// <summary>
        /// 保存钢坯数据，一车多炉方法二，用上一车的毛重做为下一车的皮重(方坯用)
        /// </summary>
        private bool SaveGPTwoData()
        {
            if (ifStart == "1" && strYCJL == "" || txtLH.Text.Trim() != "" && strYCJL == "")
            {
                //钢坯一次计量数据保存
                if (SaveGPDataOne() == false)
                    return false;
            }
            if (ifStart == "1" && strYCJL == "1" || txtLH.Text.Trim() != "" && strYCJL == "1")
            {
                cbJLLX.Text = "";
                //钢坯二次计量数据保存
                if (SaveGPDataTwo() == false)
                    return false;
                    SaveGPFPData();

            }
            return true;
        }
        #endregion

        private void cbCH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < (char)127)
            {
                e.Handled = false;
                return;
            }

            string newChar = ChangeHalfCode(e.KeyChar);
            e.KeyChar = newChar[0];

            e.Handled = false;
        }

        private void cbCH1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < (char)127)
            {
                e.Handled = false;
                return;
            }

            string newChar = ChangeHalfCode(e.KeyChar);
            e.KeyChar = newChar[0];

            e.Handled = false;
        }

        #region 5.1新修改
        private void moreBtn_Click(object sender, EventArgs e)
        {
            if (m_PoundRoomArray == null || strJLDID == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            System.Windows.Forms.Button Btn = (System.Windows.Forms.Button)(sender);
            MoreBaseInfo frm = new MoreBaseInfo(this, Btn.Tag.ToString(),this.ob);
            frm.Owner = this;
            frm.ShowDialog();
        }


        //构建内存表格式
        private void BuildMyTable()
        {
            DataColumn dc;
            //磅房对应物料
            dc = new DataColumn("FS_PointNo".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("FS_MATERIALNO".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("fs_materialname".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_MaterialTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_MaterialTable.Columns.Add(dc);

            //磅房对应收货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("FS_Receiver".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("fs_memo".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_ReveiverTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_ReveiverTable.Columns.Add(dc);

            //磅房对应发货单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_SUPPLIER".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_SUPPLIERName".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_SenderTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_SenderTable.Columns.Add(dc);

            //磅房对应承运单位表
            dc = new DataColumn("FS_PointNo".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_TransNo".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_TRANSNAME".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("FS_HELPCODE".ToUpper()); m_TransTable.Columns.Add(dc);
            dc = new DataColumn("fn_times".ToUpper()); m_TransTable.Columns.Add(dc);

            //磅房对应车号表  FS_POINTNO, FS_CARNO, FN_TIMES
            dc = new DataColumn("FS_POINTNO".ToUpper()); m_CarNoTable.Columns.Add(dc);
            dc = new DataColumn("FS_CARNO".ToUpper()); m_CarNoTable.Columns.Add(dc);
            dc = new DataColumn("FN_TIMES".ToUpper()); m_CarNoTable.Columns.Add(dc);

            //流向表 FS_TYPECODE, FS_TYPENAME
            dc = new DataColumn("FS_TYPECODE".ToUpper()); m_FlowTable.Columns.Add(dc);
            dc = new DataColumn("FS_TYPENAME".ToUpper()); m_FlowTable.Columns.Add(dc);
        }

        //下载磅房对应物料基础信息  ,add by luobin 
        private void DownLoadMaterial()
        {
            string strSql = "select A.FS_PointNo, A.FS_MATERIALNO, b.fs_materialname, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_Pointmaterial A, It_Material B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.Fs_Materialno = B.Fs_Wl and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_MaterialTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对收货单位信息  ,add by luobin 
        private void DownLoadReceiver()
        {
            string strSql = "select A.FS_PointNo, A.FS_Receiver, b.fs_memo, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointReceiver A, It_Store B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_Receiver = B.Fs_SH and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ReveiverTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对发货单位信息  ,add by luobin 
        private void DownLoadSender()
        {
            string strSql = "select A.FS_PointNo, A.FS_SUPPLIER, b.FS_SUPPLIERNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_Pointsupplier A, IT_SUPPLIER B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_SUPPLIER = B.Fs_GY and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_SenderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }


        //下载磅房对供应单位信息  ,add by luobin 
        private void DownLoadProvider()
        {
            string strSql = "select A.FS_PointNo, A.FS_PROVIDER, b.FS_PROVIDERNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from BT_POINTPROVIDER A, IT_PROVIDER B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_PROVIDER = B.FS_SP and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_ProviderTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对承运单位信息  ,add by luobin 
        private void DownLoadTrans()
        {
            string strSql = "select A.FS_PointNo, A.FS_TransNo, b.FS_TRANSNAME, b.FS_HELPCODE, a.fn_times ";
            strSql += " from Bt_PointTrans A, BT_Trans B, Bt_Point C ";
            strSql += " where A.Fs_pointno = C.Fs_Pointcode and A.FS_TransNo = B.Fs_CY and C.Fs_Pointtype = 'QC' ";
            strSql += "  order by a.fn_times desc ";


            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_TransTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
        }

        //下载磅房对应车号信息  ,add by luobin 
        private void DownLoadCarNo()
        {
            string strSql = "select FS_POINTNO, FS_CARNO, FN_TIMES From BT_POINTCARNO order by FN_TIMES desc ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_CarNoTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


        }

        //下载流向信息  ,add by luobin 
        private void DownLoadFlow()
        {
            string strSql = "select FS_TYPECODE, FS_TYPENAME From BT_WEIGHTTYPE order by FS_TYPECODE ";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.TransPlanInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = this.m_FlowTable;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            cbLX.DataSource = this.m_FlowTable;
            cbLX.DisplayMember = "FS_TYPENAME";
            cbLX.ValueMember = "FS_TYPECODE";

        }

        //按磅房筛选物料信息
        private void BandPointMaterial(string PointID)
        {
            DataRow[] drs = null;

            tempMaterial = m_MaterialTable.Clone();

            drs = this.m_MaterialTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            tempMaterial.Clear();
            foreach (DataRow dr in drs)
            {
                tempMaterial.Rows.Add(dr.ItemArray);
            }

            DataRow drz = tempMaterial.NewRow();
            tempMaterial.Rows.InsertAt(drz, 0);
            cbWLMC.DataSource = tempMaterial;
            cbWLMC.DisplayMember = "fs_materialname";
            cbWLMC.ValueMember = "FS_MATERIALNO";
        }

        //按磅房筛选收货单位
        private void BandPointReceiver(string PointID)
        {
            DataRow[] drs = null;

            this.tempReveiver = this.m_ReveiverTable.Clone();

            drs = this.m_ReveiverTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempReveiver.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempReveiver.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempReveiver.NewRow();
            this.tempReveiver.Rows.InsertAt(drz, 0);
            this.cbSHDW.DataSource = this.tempReveiver;
            this.cbSHDW.DisplayMember = "FS_MEMO";
            this.cbSHDW.ValueMember = "FS_Receiver";
        }

        //按磅房筛选发货单位
        private void BandPointSender(string PointID)
        {
            DataRow[] drs = null;

            this.tempSender = this.m_SenderTable.Clone();

            drs = this.m_SenderTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempSender.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempSender.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempSender.NewRow();
            this.tempSender.Rows.InsertAt(drz, 0);
            this.cbFHDW.DataSource = this.tempSender;
            this.cbFHDW.DisplayMember = "FS_SUPPLIERName";
            this.cbFHDW.ValueMember = "FS_SUPPLIER";
        }

        //按磅房筛选供应单位
        private void BandPointProvider(string PointID)
        {
            DataRow[] drs = null;

            this.tempProvider = this.m_ProviderTable.Clone();

            drs = this.m_ProviderTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempProvider.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempProvider.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempProvider.NewRow();
            this.tempProvider.Rows.InsertAt(drz, 0);
            this.cbProvider.DataSource = this.tempProvider;
            this.cbProvider.DisplayMember = "FS_PROVIDERNAME";
            this.cbProvider.ValueMember = "FS_PROVIDER";
        }

        //按磅房筛选承运单位
        private void BandPointTrans(string PointID)
        {
            DataRow[] drs = null;

            this.tempTrans = this.m_TransTable.Clone();

            drs = this.m_TransTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempTrans.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempTrans.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempTrans.NewRow();
            this.tempTrans.Rows.InsertAt(drz, 0);
            this.cbCYDW.DataSource = this.tempTrans;
            cbCYDW.DisplayMember = "FS_TRANSNAME";
            cbCYDW.ValueMember = "FS_TRANSNO";

        }

        //按磅房筛选车号
        private void BandPointCarNo(string PointID)
        {
            DataRow[] drs = null;

            this.tempCarNo = this.m_CarNoTable.Clone();

            drs = this.m_CarNoTable.Select("FS_PointNo ='" + PointID + "'", "FN_TIMES desc");

            this.tempCarNo.Clear();
            foreach (DataRow dr in drs)
            {
                this.tempCarNo.Rows.Add(dr.ItemArray);
            }

            DataRow drz = this.tempCarNo.NewRow();
            this.tempCarNo.Rows.InsertAt(drz, 0);
            this.cbCH.DataSource = this.tempCarNo;
            cbCH.DisplayMember = "FS_CARNO";
            //cbCH1.ValueMember = "FS_TRANSNO";

        }

        /// <summary>
        /// 构建一个公用的下拉列表框
        /// </summary>
        private void InitConfig()
        {
            this.panel9.Controls.Add(m_List);
            m_List.Size = new Size(145, 80);
            m_List.Visible = false;
            m_List.ScrollAlwaysVisible = true;
            m_List.BringToFront();
            m_List.Click += new EventHandler(m_List_Click);
            m_List.KeyPress += new KeyPressEventHandler(m_List_KeyPress);
            m_List.Leave += new EventHandler(m_List_Leave);
        }

        /// <summary>
        /// 键盘按键重写事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Down && m_List.Visible == true && m_List.Items.Count != 0 &&
                (cbWLMC.Focused == true || cbSHDW.Focused == true || cbFHDW.Focused == true || cbCYDW.Focused == true || cbProvider.Focused == true))
            {
                m_List.SetSelected(0, true);
                m_List.Focus();
                return true;
            }
            if (keyData == Keys.Up &&
                (cbWLMC.Focused == true || cbSHDW.Focused == true || cbFHDW.Focused == true || cbCYDW.Focused == true || cbProvider.Focused == true))
            {
                Control c = GetNextControl(this.ActiveControl, false);
                bool ok = SelectNextControl(this.ActiveControl, false, true, true, true);
                if (ok && c != null)
                {
                    if (c is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)c).SelectAll();
                    }
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void m_List_Click(object sender, EventArgs e)   //双击选中智能输入列表中的内容
        {
            if (m_List.Text.Trim().Length == 0)
            {
                return;
            }
            switch (m_ListType)
            {
                case "Material":
                    this.cbWLMC.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbWLMC.Focus();
                    m_List.Visible = false;
                    break;
                case "Reveiver":
                    this.cbSHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbSHDW.Focus();
                    m_List.Visible = false;
                    break;
                case "Sender":
                    this.cbFHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbFHDW.Focus();
                    m_List.Visible = false;
                    break;
                case "Provider":
                    this.cbProvider.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbProvider.Focus();
                    m_List.Visible = false;
                    break;
                case "Trans":
                    this.cbCYDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                    this.cbCYDW.Focus();
                    m_List.Visible = false;
                    break;
                default:
                    m_List.Visible = false;
                    break;
            }
        }

        void m_List_Leave(object sender, EventArgs e)
        {
            m_List.Hide();
        }

        private void m_List_KeyPress(object sender, KeyPressEventArgs e)
        {
            string text = "";
            switch (m_ListType)
            {
                case "Material":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbWLMC.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbWLMC.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbWLMC.Focus();
                        text = cbWLMC.Text + e.KeyChar;
                        cbWLMC.Text = text;
                        cbWLMC.SelectionStart = cbWLMC.Text.Length;
                    }
                    break;
                case "Reveiver":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbSHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbSHDW.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbSHDW.Focus();
                        text = cbSHDW.Text + e.KeyChar;
                        cbSHDW.Text = text;
                        cbSHDW.SelectionStart = cbSHDW.Text.Length;
                    }
                    break;
                case "Sender":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbFHDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbFHDW.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbFHDW.Focus();
                        text = cbFHDW.Text + e.KeyChar;
                        cbFHDW.Text = text;
                        cbFHDW.SelectionStart = cbWLMC.Text.Length;
                    }
                    break;
                case "Provider":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbProvider.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbProvider.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbProvider.Focus();
                        text = cbProvider.Text + e.KeyChar;
                        cbProvider.Text = text;
                        cbProvider.SelectionStart = cbProvider.Text.Length;
                    }
                    break;
                case "Trans":
                    if (e.KeyChar == 13 && m_List.SelectedIndex >= 0)
                    {
                        cbCYDW.Text = m_List.Items[m_List.SelectedIndex].ToString();
                        cbCYDW.Focus();
                        m_List.Visible = false;
                    }

                    else if ((e.KeyChar >= 65 && e.KeyChar <= 90) || (e.KeyChar >= 97 && e.KeyChar <= 122))
                    {
                        m_List.Items.Clear();

                        cbCYDW.Focus();
                        text = cbCYDW.Text + e.KeyChar;
                        cbCYDW.Text = text;
                        cbCYDW.SelectionStart = cbCYDW.Text.Length;
                    }
                    break;
                default:
                    break;
            }
        }

        private void ChangeString(object sender)
        {
            if (sender is System.Windows.Forms.TextBox)
            {
                System.Windows.Forms.TextBox tb = (System.Windows.Forms.TextBox)sender;
                for (int i = 0; i < tb.Text.Length; i++)
                {
                    int isChange = 0;
                    char newChar = FullCodeToHalfCode(tb.Text[i], ref isChange);
                    if (isChange == 1)
                    {
                        tb.Text = tb.Text.Replace(tb.Text[i], newChar);
                        tb.SelectionStart = i + 1;
                    }
                }

            }
            else if (sender is ComboBox)
            {
                ComboBox cb = (ComboBox)sender;

                for (int i = 0; i < cb.Text.Length; i++)
                {
                    int isChange = 0;
                    char newChar = FullCodeToHalfCode(cb.Text[i], ref isChange);
                    if (isChange == 1)
                    {
                        cb.Text = cb.Text.Replace(cb.Text[i], newChar);
                        cb.SelectionStart = i + 1;
                    }
                }
            }
        }

        /*全角字符从的unicode编码从65281~65374   
         半角字符从的unicode编码从33~126   
         差值65248
         空格比较特殊,全角为       12288,半角为       32 
       */
        private char FullCodeToHalfCode(char c, ref int isChange)
        {
            //得到c的编码
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(c.ToString());

            int H = Convert.ToInt32(bytes[1]);
            int L = Convert.ToInt32(bytes[0]);

            //得到unicode编码
            int value = H * 256 + L;

            //是全角
            if (value >= 65281 && value <= 65374)
            {
                int halfvalue = value - 65248;//65248是全半角间的差值。
                byte halfL = Convert.ToByte(halfvalue);

                bytes[0] = halfL;
                bytes[1] = 0;
                isChange = 1;
            }
            else if (value == 12288)
            {
                int halfvalue = 32;
                byte halfL = Convert.ToByte(halfvalue);

                bytes[0] = halfL;
                bytes[1] = 0;
                isChange = 1;
            }
            else
            {
                isChange = 0;
                return c;
            }

            //将bytes转换成字符
            string ret = System.Text.Encoding.Unicode.GetString(bytes);

            return Convert.ToChar(ret);
        }

        private void cbWLMC_TextChanged(object sender, EventArgs e)
        {
            if (this.cbWLMC.Text.Trim().Length == 0 || this.cbWLMC.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (m_PoundRoomArray == null || strJLDID == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbWLMC.Text = "";
                return;
            }

            if (m_List == null || cbWLMC.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbWLMC.Text.Length; i++)
            {
                if (Char.IsLower(cbWLMC.Text[i]) == false && Char.IsUpper(cbWLMC.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Material";
            //m_List.Location = new System.Drawing.Point(291, 118);
            m_List.Location = new System.Drawing.Point(cbWLMC.Location.X, cbWLMC.Location.Y + 20);
            m_List.Width = cbWLMC.Width;

            string text = this.cbWLMC.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempMaterial.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_MaterialName"].ToString());
            }
            m_List.Visible = true;
        }

        private void cbWLMC_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbWLMC.Text = "";
                    }
                }
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbFHDW_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbFHDW.SelectedValue == null || this.cbFHDW.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbFHDW.Text = "";
                    }
                }
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbSHDW_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbSHDW.SelectedValue == null || this.cbSHDW.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbSHDW.Text = "";
                    }
                }
            }
            catch (System.Exception exp)
            {

            }
        }

        private void cbCYDW_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbCYDW.SelectedValue == null || this.cbCYDW.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbCYDW.Text = "";
                    }
                }
            }
            catch (System.Exception exp)
            {

            }
        }

        //发货单位下拉框拼音助记码选择
        private void cbFHDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbFHDW.Text.Trim().Length == 0 || this.cbFHDW.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (m_PoundRoomArray == null || strJLDID == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFHDW.Text = "";
                return;
            }

            if (m_List == null || cbFHDW.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbFHDW.Text.Length; i++)
            {
                if (Char.IsLower(cbFHDW.Text[i]) == false && Char.IsUpper(cbFHDW.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Sender";
            //m_List.Location = new System.Drawing.Point(60, 175);
            m_List.Location = new System.Drawing.Point(cbFHDW.Location.X, cbFHDW.Location.Y + 20);
            m_List.Width = cbFHDW.Width;
            string text = this.cbFHDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempSender.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_SUPPLIERName"].ToString());
            }
            m_List.Visible = true;

            //DataTable dtSender = new DataTable();
            //dtSender = tempSender.Clone();
            //for (int i = 0; i < matchRows.Length; i++)
            //{
            //    dtSender.Rows.Add(matchRows[i].ItemArray);
            //}
            //DataRow drz = dtSender.NewRow();
            //dtSender.Rows.InsertAt(drz, 0);

            //this.cbFHDW.DataSource = dtSender;
            //this.cbFHDW.DisplayMember = "FS_SUPPLIERName";
            //this.cbFHDW.ValueMember = "FS_SUPPLIER";
            ////cbFHDW.SelectedText = text;
        }

        private void cbSHDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbSHDW.Text.Trim().Length == 0 || this.cbSHDW.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (m_PoundRoomArray == null || strJLDID == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbSHDW.Text = "";
                return;
            }

            if (m_List == null || cbSHDW.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbSHDW.Text.Length; i++)
            {
                if (Char.IsLower(cbSHDW.Text[i]) == false && Char.IsUpper(cbSHDW.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Reveiver";
            //m_List.Location = new System.Drawing.Point(291, 173);
            m_List.Location = new System.Drawing.Point(cbSHDW.Location.X, cbSHDW.Location.Y + 20);
            m_List.Width = cbSHDW.Width;


            string text = this.cbSHDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempReveiver.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_Memo"].ToString());
            }
            m_List.Visible = true;
        }

        private void cbCYDW_TextChanged(object sender, EventArgs e)
        {
            if (this.cbCYDW.Text.Trim().Length == 0 || this.cbCYDW.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (m_PoundRoomArray == null || strJLDID == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCYDW.Text = "";
                return;
            }

            if (m_List == null || cbCYDW.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < cbCYDW.Text.Length; i++)
            {
                if (Char.IsLower(cbCYDW.Text[i]) == false && Char.IsUpper(cbCYDW.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Trans";
            //m_List.Location = new System.Drawing.Point(291, 146);
            m_List.Location = new System.Drawing.Point(cbCYDW.Location.X, cbCYDW.Location.Y + 20);
            m_List.Width = cbCYDW.Width;

            string text = this.cbCYDW.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempTrans.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_TRANSNAME"].ToString());
            }
            m_List.Visible = true;
        }

        //全角转化成半角
        //private void txtCZH_Leave(object sender, EventArgs e)
        //{
        //    ChangeString(sender);
        //}

        //private void cbCH1_Leave(object sender, EventArgs e)
        //{
        //    ChangeString(sender);
        //}

        private void cbCH2_Leave(object sender, EventArgs e)
        {
            ChangeString(sender);
        }
        #endregion
        //有关视频的注释
        private void reBootVideo()   
        {
            if (m_PoundRoomArray[m_iSelectedPound].Talk == true && m_PoundRoomArray[m_iSelectedPound].TalkID > 0)
            {
                m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_StopRealPlay(m_iSelectedPound);
                m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_StopTalk();
                m_PoundRoomArray[m_iSelectedPound].TalkID = 0;
                m_PoundRoomArray[m_iSelectedPound].Talk = false;

                ultraToolbarsManager1.Toolbars[0].Tools["YYDJ"].SharedProps.Caption = "打开对讲";
            }

            m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_CloseSound(m_iSelectedPound);

            //Video.SDK_CloseSound();
            m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_StopRealPlay(m_PoundRoomArray[m_iSelectedPound].Channel1);
            m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_Logout();
            m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_Cleanup();

            System.Threading.Thread.Sleep(200);
            m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_Init();

            int VideoHandle = 0;
            m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_Login(m_PoundRoomArray[m_iSelectedPound].VIEDOIP,
                                                    Convert.ToInt32(m_PoundRoomArray[m_iSelectedPound].VIEDOPORT),
                                                    m_PoundRoomArray[m_iSelectedPound].VIEDOUSER,
                                                    m_PoundRoomArray[m_iSelectedPound].VIEDOPWD);



            m_PoundRoomArray[m_iSelectedPound].VideoHandle = VideoHandle;
            m_PoundRoomArray[m_iSelectedPound].Channel1 = m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_RealPlay(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 0, (int)VideoChannel4.Handle);//注意第1个通道为车牌，在第4个图片显示 VideoChannel4 
            //m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 0, m_PoundRoomArray[i].POINTNAME + "- 车牌");

            m_PoundRoomArray[m_iSelectedPound].Channel2 = m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_RealPlay(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 1, (int)VideoChannel2.Handle);
            //m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 1, m_PoundRoomArray[m_iSelectedPound].POINTNAME + "- 车头");

            m_PoundRoomArray[m_iSelectedPound].Channel3 = m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_RealPlay(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 2, (int)VideoChannel3.Handle);
            //m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 2, m_PoundRoomArray[m_iSelectedPound].POINTNAME + "- 车尾");

            m_PoundRoomArray[m_iSelectedPound].Channel4 = m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_RealPlay(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 3, (int)VideoChannel1.Handle);//注意第4个通道为车牌，在第1个图片显示 VideoChannel1
            //m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 3, m_PoundRoomArray[m_iSelectedPound].POINTNAME + "- 车身");

            m_PoundRoomArray[m_iSelectedPound].Channel5 = m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_RealPlay(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 4, (int)VideoChannel4.Handle);
            //m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SETCHANNELNAME(m_PoundRoomArray[m_iSelectedPound].VideoHandle, 4, m_PoundRoomArray[m_iSelectedPound].POINTNAME + "- 票据");


            if (m_PoundRoomArray[m_iSelectedPound].Channel1 > 0)
            {
                m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_OpenSound(m_PoundRoomArray[m_iSelectedPound].Channel1);
                m_PoundRoomArray[m_iSelectedPound].VideoRecord.SDK_SetVolume(65500);
                m_PoundRoomArray[m_iSelectedPound].ISFIRSTSEND = true;
                System.Threading.Thread.Sleep(200);
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                if (strPoint == "")
                {
                    MessageBox.Show("请选择计量点！");
                    return;
                }

                if (cbJLLX.Enabled == true && cbJLLX.Text.Trim() == "复磅")
                {
                    MessageBox.Show("“复磅”不能选择，请重新选择计量类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (cbJLLX.Text.Trim() != "外协")
                {
                    if (ControlProve() == false)
                    {
                        return;
                    }
                }
                else
                {
                    if (txtJLD.Text == "")
                    {
                        MessageBox.Show("请选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtJLD.Focus();
                        return;
                    }
                    if (cbCH.Text == "")
                    {
                        MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbCH.Focus();
                        return;
                    }
                    if (cbCH1.Text == "")
                    {
                        MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbCH1.Focus();
                        return;
                    }
                }

                //判断是否取样、卸货与验收
                if (strYCJL == "1" && cbJLLX.Text == "")
                {

                    //卸货
                    //if (stSHKCD == "1")
                    // {
                    if (strXCQR != "1")
                    {
                        if (DialogResult.No == MessageBox.Show("卡号：" + txtCZH.Text.Trim() + "车号：" + cbCH.Text.Trim() + cbCH1.Text.Trim() + "未卸货！,是否继续保存", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            //MessageBox.Show("该车还未卸货，是否允许过磅！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                            return;
                    }
                    //}

                }

                if (ifStart == "1")
                {
                    if (txtLH.Text.Trim() != "" && sDDH == "")
                    {
                        MessageBox.Show("请先查询炉号对应的合同号！");
                        return;
                    }
                    if (txtLH.Text.Trim() != "" && sDDH != "" && txtZS.Text.Trim() == "")
                    {
                        MessageBox.Show("请录入炉号对应的支数！");
                        txtZS.Focus();
                        return;
                    }
                    if (txtLH.Text.Trim() == "" && txtLH2.Text.Trim() != "" || txtLH3.Text.Trim() != "")
                    {
                        MessageBox.Show("请把炉号录入相应的位置！");
                        txtLH.Focus();
                        return;
                    }
                }

                if (strPoint == "")
                {
                    MessageBox.Show("请双击选择计量点接管信息，接管计量点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                strBCSJ = "1"; //在保存时不需要计算净重，防止保存时系统自动退出               

                printInfoClear();
                strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
                //print.printCZH = txtCZH.Text.Trim();
                print.printCH = cbCH.Text.Trim() + cbCH1.Text.Trim();
                print.printHTH = txtHTH.Text.Trim();
                print.printWLMC = cbWLMC.Text.Trim();
                print.printFHDW = cbFHDW.Text.Trim();
                print.printSHDW = cbSHDW.Text.Trim();
                print.printCYDW = cbCYDW.Text.Trim();
                print.printJLLX = cbJLLX.Text.Trim();
                print.printJLD = txtJLD.Text.Trim();
                print.printJLY = txtJLY.Text.Trim();
                //add by luobin
                print.printYKL = txtYKL.Text.Trim();
                print.printYKBL = s_YKBL;
                print.printAdviseSpec = this.strAdviseSpec;
                print.printZZJY = this.strZZJY;
                //print.pringJLCS = strYCJL;
                WriteLog("1");
                if (e_REWEIGHTFLAG == "1")//二次计量复磅标志（期限皮重时用）
                {
                    if (UpdateECJLBData() == false)
                    {
                        strBCSJ = "0";
                        this.Cursor = Cursors.Default;
                        return;
                    }

                }
                WriteLog("2");
                if (e_REWEIGHTFLAG != "1")
                {
                    //if (strYCJL == "")
                    //{
                    //    if (chbQXPZ.Checked == false)
                    //    {
                    //        QueryQXPZData();
                    //    }
                    //}

                    if (ifStart == "0")
                    {
                        if (strYCJL == "" && strYB == "1" || strYCJL == "" && strYB == "" && cbJLLX.Text == "")
                        {
                            if (strQXPZ == "")
                            {
                                WriteLog("3");
                                if (AddYCJLData() == false)
                                {
                                    strBCSJ = "0";
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                                WriteLog("3.1");

                            }
                            else
                            {
                                WriteLog("4");
                                if (AddECJLBData() == false)
                                {
                                    strBCSJ = "0";
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                                WriteLog("4.1");
                            }
                        }
                        else
                        {
                            if (strYCJL == "1" && cbJLLX.Text == "")
                            {
                                if (txtLH.Text == "" && txtLH2.Text == "" && txtLH3.Text == "")
                                {
                                    WriteLog("5");
                                    if (AddECJLData() == false)
                                    {
                                        strBCSJ = "0";
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                    WriteLog("5.1");
                                }
                            }
                            WriteLog("6");
                            if (cbJLLX.Text == "外协")
                            {
                                if (AddWXData() == false)
                                {
                                    strBCSJ = "0";
                                    this.Cursor = Cursors.Default;
                                    return;
                                }
                            }
                            WriteLog("6.1");
                            if (cbJLLX.Text.Trim() == "复磅")
                            {
                                if (strYCJL == "1")
                                {
                                    if (UpdateYCJLData() == false)
                                    {
                                        strBCSJ = "0";
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                                if (strECJL == "1")
                                {
                                    if (UpdateECJLBData() == false)
                                    {
                                        strBCSJ = "0";
                                        this.Cursor = Cursors.Default;
                                        return;
                                    }
                                }
                            }
                        }
                        //是否保存期限皮重
                        if (chbQXPZ.Checked == true)
                        {
                            //AddTPData();
                            if (SaveQXPZData() == false)
                            {
                                strBCSJ = "0";
                                this.Cursor = Cursors.Default;
                                return;
                            }
                        }
                    }
                }


                if (e_REWEIGHTFLAG == "1")
                {
                    DisPlayShowForFirst();
                }
                if (e_REWEIGHTFLAG != "1")
                {
                    //if (strYCJL == "" && strYB == "1" || chbQXPZ.Checked == true)
                    if (strYCJL == "")
                    {
                        WriteLog("8");
                        DisPlayShowForFirst();
                        WriteLog("8.1");
                    }
                    if (strYCJL == "1" && cbJLLX.Text == "")
                    {
                        WriteLog("9");
                        DisPlayShowForSecond();
                        WriteLog("9.1");
                    }
                }

                //抓图线程
                m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
                Invoke(m_MainThreadCapPicture); //用委托抓图

                //打印磅单
                //Print();


                //查询一次计量表
                WriteLog("10");
                m_BindUltraGridDelegate = new BindUltraGridDelegate(QueryYCBData);
                //Invoke(m_BindUltraGridDelegate);
                BeginInvoke(m_BindUltraGridDelegate);
                WriteLog("10.1");
                //panelYCSP.Visible = false;
                //曲线图表刷新
                dtQX.Rows.Clear();
                dtQX.Columns.Clear();
                ultraChart1.DataSource = dataTable6;
                ksht = 1;//开始画图标志，

                ClearControlData();
                ClearControl();
                ClearQXPZData();
                if (strYB == "1")
                {
                    ClearYBData();
                }

                WriteLog("11");

                //曲线图表刷新
                if (m_nPointCount > 0)
                {
                    for (int i = 0; i < m_nPointCount; i++)
                    {
                        if (m_PoundRoomArray[i].POINTNAME.Trim() == ultraGrid2.ActiveRow.Cells["FS_POINTNAME"].Value.ToString().Trim())
                        {
                            dtQX.Rows.Clear();
                            dtQX.Columns.Clear();
                            ultraChart1.DataSource = dataTable6;
                            BackZeroSign[i] = 1; //BackZeroSign = 1，意思就是车子在上称过程中如出现重量多次稳定，可以继续绘图；如是下称，则不准再画。
                        }
                    }
                }
                WriteLog("12");
                ifStart = "0";//保存后重新恢复启动
                cbJLLX.Enabled = true;
                //strZYBH = ""; //清除Guid

                //if (chb_Autocontrol.Checked == true)
                //{
                //    //if (m_PoundRoomArray[m_iSelectedPound].POINTID == "K01" || m_PoundRoomArray[m_iSelectedPound].POINTID == "K02")
                //    m_PoundRoomArray[m_iSelectedPound].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0x00, (byte)0);
                //}



                m_PoundRoomArray[m_iSelectedPound].ClearCardNoAndGuid();
                m_PoundRoomArray[m_iSelectedPound].CardNo = "";
                m_PoundRoomArray[m_iSelectedPound].ReaderGUID = "";
                txtZL.Text = "";


                WriteLog("13");
                if (strYCJL == "1")
                {
                    ClearYCBData();
                }
                if (e_REWEIGHTFLAG == "1")
                {
                    ClearECJLBData();
                }
                WriteLog("14");
                //pictureBox18.Image = BitmapToImage(new byte[1]); //车牌号一次计量图片
                //pictureBox18.Refresh();
                //panel20.Visible = false; //车牌号一次计量图片panel
                //panel22.BringToFront(); //磅单视频

                strBCSJ = "0";
                WriteLog("15");

                #region 自动播放语音

                m_AlarmVoicePath = Constant.RunPath + "\\sound\\称重完成.wav";

                WriteLog("准备语音播放...");
                
                #endregion
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                WriteLog("保存失败: " + ex.ToString());
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (strPoint == "")
            {
                MessageBox.Show("请先选择计量点！");
                return;
            }

            if (cbJLLX.Enabled == true && cbJLLX.Text.Trim() == "复磅")
            {
                MessageBox.Show("“复磅”不能选择，请重新选择计量类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtJLD.Text == "")
            {
                MessageBox.Show("请选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtJLD.Focus();
                return;
            }
            if (cbCH.Text == "")
            {
                MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCH.Focus();
                return;
            }
            if (cbCH1.Text == "")
            {
                MessageBox.Show("车号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCH1.Focus();
                return;
            }

            //sDDH = txtHTH.Text.Trim();
            //if (txtLH.Text != "" && sDDH == "")
            //{
            //    MessageBox.Show("该炉号没有对应的合同号！");
            //    //return;
            //}
            if (txtZS.Text == "" && txtLH.Text.Trim().Length == 9)
            {
                MessageBox.Show("请录入支数！");
                return;
            }

            //if (txtLH.Text == "" && txtLH2.Text != "" || txtLH3.Text != "")
            //{
            //    MessageBox.Show("请把炉号录入第一个输入框！");
            //    txtLH.Focus();
            //    return;
            //}

            if (cbJLLX.Enabled == true && cbJLLX.Text.Trim() == "复磅")
            {
                MessageBox.Show("“复磅”不能选择，请重新选择计量类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ControlProve() == false)
            {
                return;
            }

            if (txtLH.Text.Trim().Length > 0 && strYCJL == "1" && txtZS.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入支数或块数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZS.Focus();
                return;
            }
            if (txtLH2.Text.Trim().Length > 0 && strYCJL == "1" && txtZS2.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入支数或块数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZS2.Focus();
                return;
            }
            if (txtLH3.Text.Trim().Length > 0 && strYCJL == "1" && txtZS3.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入支数或块数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtZS3.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            strBCSJ = "1";


            printInfoClear();
            strCode = DateTime.Now.ToString("yyyyMMddHHmmss") + strJLDID;
            //print.printCZH = txtCZH.Text.Trim();
            print.printCH = cbCH.Text.Trim() + cbCH1.Text.Trim();
            print.printHTH = txtHTH.Text.Trim();
            print.printWLMC = cbWLMC.Text.Trim();
            print.printFHDW = cbFHDW.Text.Trim();
            print.printSHDW = cbSHDW.Text.Trim();
            print.printCYDW = cbCYDW.Text.Trim();
            print.printJLLX = cbJLLX.Text.Trim();
            print.printJLD = txtJLD.Text.Trim();
            print.printJLY = txtJLY.Text.Trim();
            //add by luobin
            print.printYKL = txtYKL.Text.Trim();
            print.printYKBL = s_YKBL;
            //print.pringJLCS = strYCJL;

            print.printLH = txtLH.Text.Trim();
            print.printZS = txtZS.Text.Trim();
            print.printGZ = GZ;
            print.printGG = GG;

            print.printLH1 = txtLH.Text.Trim();
            print.printZS1 = txtZS.Text.Trim();
            print.printLH2 = txtLH2.Text.Trim();
            print.printZS2 = txtZS2.Text.Trim();
            print.printLH3 = txtLH3.Text.Trim();
            print.printZS3 = txtZS3.Text.Trim();
            print.printAdviseSpec = this.strAdviseSpec;
            print.printZZJY = this.strZZJY;

            //if (strYCJL == "")
            //{
            //    if (chbQXPZ.Checked == false)
            //    {
            //        QueryQXPZData();
            //    }
            //}

            if (strYCJL != "" || strQXPZ != "")
            {
                if (txtLH.Text.Trim().Length != 0 )
                {
                    //if (SaveGPData() == false)
                    if (SaveFPHistory() == false)
                    {
                        this.Cursor = Cursors.Default;
                        strBCSJ = "0";
                        return;
                    }
                }
            }
            else
            {
                if (AddYCJLData() == false)
                {
                    strBCSJ = "0";
                    this.Cursor = Cursors.Default;
                    return;
                }
            }

            if (cbCH.SelectedIndex < 0)
            {
                this.DownLoadCarNo();
            }

            if (strYCJL == "")
            {
                DisPlayShowForFirst();
            }
            if (strYCJL == "1")
            {
                DisPlayShowForSecond();
            }



            m_MainThreadCapPicture = new CapPicture(MainThreadCapPicture);
            Invoke(m_MainThreadCapPicture); //用委托抓图
            //打印磅单
            //Print();

            //查询一次计量表
            m_BindUltraGridDelegate = new BindUltraGridDelegate(QueryYCBData);
            BeginInvoke(m_BindUltraGridDelegate);

            dtQX.Rows.Clear();
            dtQX.Columns.Clear();
            ultraChart1.DataSource = dataTable6;
            ksht = 1;

            ClearControlData();
            ClearControl();
            ClearQXPZData();
            if (strYB == "1")
            {
                ClearYBData();
            }

            //曲线图表刷新
            if (m_nPointCount > 0)
            {
                for (int i = 0; i < m_nPointCount; i++)
                {
                    if (m_PoundRoomArray[i].POINTNAME.Trim() == ultraGrid2.ActiveRow.Cells["FS_POINTNAME"].Value.ToString().Trim())
                    {
                        dtQX.Rows.Clear();
                        dtQX.Columns.Clear();
                        ultraChart1.DataSource = dataTable6;
                        BackZeroSign[i] = 1; //BackZeroSign = 1，意思就是车子在上称过程中如出现重量多次稳定，可以继续绘图；如是下称，则不准再画。
                    }
                }
            }

            ifStart = "0";//保存后重新恢复启动
            cbJLLX.Enabled = true;
            //strZYBH = ""; //清除Guid

            //if (chb_Autocontrol.Checked == true)
            //{
            //    if (m_PoundRoomArray[m_iSelectedPound].POINTID == "K01" || m_PoundRoomArray[m_iSelectedPound].POINTID == "K02")
            //        m_PoundRoomArray[m_iSelectedPound].SendRtuCommand((byte)1, (byte)5, (byte)0x50, (byte)(0xE0 - 1), (byte)0x00, (byte)0);
            //}

            m_PoundRoomArray[m_iSelectedPound].ClearCardNoAndGuid();
            m_PoundRoomArray[m_iSelectedPound].CardNo = "";
            m_PoundRoomArray[m_iSelectedPound].ReaderGUID = "";
            txtZL.Text = "";

            if (strYCJL == "1")
            {
                ClearYCBData();
            }

            //pictureBox18.Image = BitmapToImage(new byte[1]);
            //pictureBox18.Refresh();
            //panel20.Visible = false;
            //panel22.BringToFront();

            btnGPBC.Enabled = false;
            strBCSJ = "0";


            #region 自动播放语音

            m_AlarmVoicePath = Constant.RunPath + "\\sound\\称重完成.wav";

            WriteLog("准备语音播放...");

            #endregion


            this.Cursor = Cursors.Default;
        }
        //外协
        private void cbJLLX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbJLLX.Text.Trim() == "外协")
                tbCharge.Enabled = true;
            else
                tbCharge.Enabled = false;
          
        }

        private void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbProvider_Leave(object sender, EventArgs e)
        {
            try
            {
                if (m_List == null)
                {
                    return;
                }

                if (m_List.Focused == false)
                {
                    m_List.Visible = false;
                }
                if (this.cbProvider.SelectedValue == null || this.cbProvider.SelectedValue.ToString().Trim() == "")
                {
                    if (m_List.Visible == false)
                    {
                        cbProvider.Text = "";
                    }
                }
                //if (this.cbWLMC.SelectedValue == null || this.cbWLMC.SelectedValue.ToString().Trim() == "")
                //{
                //    if (m_List.Visible == false)
                //    {
                //        MessageBox.Show("请选择物料");
                //        this.cbWLMC.Text = "";
                //        this.cbWLMC.Focus();
                //    }
                //}
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void cbProvider_TextChanged(object sender, EventArgs e)
        {
            if (this.cbProvider.Text.Trim().Length == 0 || this.cbProvider.Text.Trim() == "System.Data.DataRowView")
            {
                m_List.Hide();
                return;
            }

            if (m_PoundRoomArray == null || strJLDID == "")
            {
                MessageBox.Show("请先选择磅房信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbProvider.Text = "";
                return;
            }

            if (m_List == null || cbProvider.Focused == false)
            {
                return;
            }

            ChangeString(sender);

            for (int i = 0; i < this.cbProvider.Text.Length; i++)
            {
                if (Char.IsLower(this.cbProvider.Text[i]) == false && Char.IsUpper(this.cbProvider.Text[i]) == false)  //是否纯字母
                {
                    m_List.Visible = false;
                    return;
                }
            }

            m_ListType = "Provider";
            m_List.Location = new System.Drawing.Point(this.cbProvider.Location.X, this.cbProvider.Location.Y + 20);
            m_List.Width = cbProvider.Width;

            string text = this.cbProvider.Text;
            text = text.ToUpper();

            DataRow[] matchRows = null;

            matchRows = this.tempProvider.Select("FS_HELPCODE LIKE '%" + text + "%'", "FN_Times desc");

            m_List.Items.Clear();
            foreach (DataRow dr in matchRows)
            {
                m_List.Items.Add(dr["FS_PROVIDERNAME"].ToString());
            }
            m_List.Visible = true;
        }

        private bool QueryTechCard(string strStoveNo, TextBox tbOrderNo, TextBox tbCount)
        {

             CoreClientParam ccp = new CoreClientParam();
            string sql = "";


            DataTable dt1 = new DataTable();
            sql = "select count(1) from dt_carweight_weight where FS_STOVENO = '" + strStoveNo + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dt1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (Convert.ToInt32(dt1.Rows[0][0].ToString()) > 0)
            {
                MessageBox.Show("该炉号已经计量，不允许重复计量！");
                return false;
            }

            //电子工艺流动卡中是否存在该炉号，若不存在，不允许保存
            DataTable dt2 = new DataTable();
            sql = "select count(1) from it_fp_techcard where FS_GP_STOVENO = '" + strStoveNo + "' and fs_transtype ='3'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dt2;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (Convert.ToInt32(dt2.Rows[0][0].ToString()) <= 0)
            {
                MessageBox.Show("工艺流动卡中不存在该炉号，不允许计量！");
                return false;
            }


            DataTable dt = new DataTable();
            sql = "select A.FN_GP_TOTALCOUNT, A.FS_GP_ORDERNO,C.FS_WL,C.FS_MATERIALNAME,A.FS_ADVISESPEC,A.FS_ZZJY from IT_FP_TECHCARD A, IT_PRODUCTDETAIL B,It_Material C ";
            sql += " where A.FS_GP_STOVENO = '" + strStoveNo + "'   and A.Fs_Gp_Orderno = B.FS_PRODUCTNO(+)   and B.FS_MATERIAL = C.Fs_Sapcode(+) ";
            sql += " order by FD_GP_JUDGEDATE desc";


            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dt;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    tbOrderNo.Text = dt.Rows[0]["FS_GP_ORDERNO"].ToString();
                    tbCount.Text = dt.Rows[0]["FN_GP_TOTALCOUNT"].ToString();
                    cbWLMC.Text = dt.Rows[0]["FS_MATERIALNAME"].ToString();
                    strAdviseSpec = dt.Rows[0]["FS_ADVISESPEC"].ToString(); //建议轧制规格
                    strZZJY = dt.Rows[0]["FS_ZZJY"].ToString();//轧制建议
                    strGPMaterial = dt.Rows[0]["FS_WL"].ToString();
                    WriteLog("   炉号 ：= " + strStoveNo + "   合同号 ：=" + dt.Rows[0]["FS_GP_ORDERNO"].ToString() + "   获取物料代码  ：=" + dt.Rows[0]["FS_WL"].ToString());
                }
            }
            return true;
        }

        private void txtLH_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtLH2_Leave(object sender, EventArgs e)
        {
            string strStoveNo = txtLH2.Text.Trim();

            if (strStoveNo != "")
                if (QueryTechCard(strStoveNo, txtHTH, txtZS2) == false)
                    return;
        }

        private void txtLH3_Leave(object sender, EventArgs e)
        {
            string strStoveNo = txtLH3.Text.Trim();

            if (strStoveNo != "")
                if (QueryTechCard(strStoveNo, txtHTH, txtZS3) == false)
                    return;
        }

        /// <summary>
        /// 校秤抓图并保存
        /// </summary>
        private void CorrentionSaveImage()
        {
            m_MainThreadCorrentionPicture = new CorrentionPicture(MainThreadCorrentionPicture);
            Invoke(m_MainThreadCorrentionPicture); //用委托抓图
        }

        /// <summary>
        /// 校秤截图处理
        /// </summary>
        private void MainThreadCorrentionPicture()
        {
            //m_GraspImageSign = 1; //如果为1，则下次不能再开启线程
            int i = m_iSelectedPound;
            strNumber = m_PoundRoomArray[i].POINTID;
            string poundPicPath = stRunPath + "\\JZPicture\\";
            strNumber = m_PoundRoomArray[i].POINTID;
            string poundPicFilePath = poundPicPath + strNumber + "corrention1.bmp";

            if (i < 0 || m_PoundRoomArray == null || m_PoundRoomArray[i] == null)
            {
                return;
            }

            if (m_PoundRoomArray[i].VIEDOIP == null || m_PoundRoomArray[i].VIEDOIP.Trim().Length == 0)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].Signed != true)//未接管的计量点
            {
                return;
            }

            if (m_PoundRoomArray[i].POINTID != ultraGrid2.ActiveRow.Cells["FS_POINTCODE"].Text.Trim())//非当前正在操作的计量点
            {
                return;
            }

            //抓第二张图
            try
            {

                m_PoundRoomArray[i].VideoRecord.SDK_CapturePicture(m_PoundRoomArray[i].Channel4, poundPicFilePath);
                System.Threading.Thread.Sleep(200);

            }
            catch (System.Exception error)
            {
                MessageBox.Show("抓校秤图：" + error.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                baseinfo.GraspAndSaveCorrentionImage(strNumber + "corrention1.bmp", correntionWeightNo, correntionWeight);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("baseinfo.GraspAndSaveCorrentImage截图出错:" + ex1.Message);
            }
            m_GraspImageSign = 0;
            //try
            //{
            //    OrderCap();
            //    PoundCap();
            //}
            //catch (Exception ex1)
            //{
            //    MessageBox.Show("界面图片显示出错:" + ex1.Message);
            // }
        }

        /// <summary>
        /// 判断车辆是否有入场登记信息
        /// </summary>

        private bool QueryPD(string str)
        {
            string sql = "";
            str = this.txtCZH.Text.Trim();
            try
            {
                sql = "SELECT * FROM DT_ENTERFACRECORD WHERE FS_CARDNUMBER = '"+str+"' and fn_enterfacflag=1";
                CoreClientParam ccp = new CoreClientParam();
                DataTable dt = new DataTable();
                ccp.ServerName = "ygjzjl.car.CarCard";
                ccp.MethodName = "queryByClientSql";
                ccp.ServerParams = new object[] { sql };
                ccp.SourceDataTable = dt;

                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("车辆没有入厂登记,请进行入场登记，重新计量!");
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private void ultraChart1_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
        {

        }

        private void ultraDockManager1_BeforeDockChange(object sender, Infragistics.Win.UltraWinDock.BeforeDockChangeEventArgs e)
        {

        }

    }
}