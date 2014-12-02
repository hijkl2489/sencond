using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGJZJL.CarSip.Client.Meas;
using SDK_Com;
namespace YGJZJL.CarSip.Client.App
{
    public class CoreApp : IApplication
    {
        #region <成员变量>        
        // 数据采集仪表
        private HgRtu _rtu = null;         // 仪表
        private HgIcCard _card = null;         // 读卡器
        private HgLcd _lcd = null;       // 液晶屏
        private LedScreen _led = null;       // LED显示屏
        private CoreIoLogik _iologic = null; // MOXA IoLogic
        private CarWeight _weight = null;
        private CorePrinter _printer = null;
        private bool _Saved=false;//是否已保存
        //线程
        private System.Threading.Thread _thread; //线程
        //是否上称提示
        private bool _isFlash = false;

        public bool IsFlash
        {
            get { return _isFlash; }
            set { _isFlash = value; }
        }

        //上称提示线程
        private System.Threading.Thread _flashThread;

        public System.Threading.Thread FlashThread
        {
            get { return _flashThread; }
            set { _flashThread = value; }
        }

        // 视频
        private HkDvr _dvr;
  
        // 配置参数
        BT_POINT _params;
        #endregion

        #region <属性>
        // 基础信息
        public string Name { get; set; }
        public string Code { get; set; }
        public string Depart { get; set; }
        public bool IsSaved
        {
            get { return _Saved; }
            set 
            { 
                _Saved = value;
                if(_card != null)
                    _card.IsSaved = value;
            }
        }
        public BT_POINT Params
        {
            get { return _params; }
            set { _params = value; }
        }
        public HgRtu Rtu
        {
            get { return _rtu; }
            set { _rtu = value; }
        }
        public HgIcCard Card
        {
            get { return _card; }
            set {  _card = value; }
        }
        public HgLcd Lcd
        {
            get { return _lcd; }
            set { _lcd = value; }
        }
        public LedScreen Led
        {
            get { return _led; }
            set { _led = value; }
        }
        public CoreIoLogik IoLogik
        {
            get { return _iologic; }
            set { _iologic = value; }
        }
        public HkDvr Dvr
        {
            get { return _dvr; }
            set { _dvr = value; }
        }
        public CarWeight Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public CorePrinter Printer
        {
            get { return _printer; }
            set { _printer = value; }
        }
        #endregion

        #region <公共方法>
        public int Init(int m_iSelectedPound)
        {
            IsSaved = false;
            int ret = -1;
            string [] strParams = new string [5];
            //_params = new BT_POINT();             
            
            _thread = null;//new System.Threading.Thread();
            _flashThread = null;

            if (_params.FS_RTUIP != "")
            {
                _rtu = new HgRtu();
                _rtu.IP = _params.FS_RTUIP;
                if (_params.FS_RTUPORT != "") _rtu.Port = Convert.ToUInt16(_params.FS_RTUPORT);
            }
            if (_params.FS_READERPARA != "")
            {
                _card = new HgIcCard();
                _card.Init(_params.FS_READERPARA,m_iSelectedPound); 
            }
            if (_params.FS_LEDIP != "")
            {
                _led = new LedScreen();
                _led.Init(_params.FS_LEDIP);
            }

            if (_params.FS_DISPLAYPARA != "")
            {
                _lcd = new HgLcd();
                _lcd.Init(_params.FS_DISPLAYPARA);
            }
            if (_params.FS_MOXAIP != "")
            {
                _iologic = new CoreIoLogik();
                _iologic.IP = _params.FS_MOXAIP;
                if (_params.FS_MOXAPORT != "") _iologic.Port = Convert.ToInt32(_params.FS_MOXAPORT);
            }
            if (_params.FS_VIEDOIP != "")
            {
                _dvr = new HkDvr();
                _dvr.Init(_params.FS_VIEDOIP+","+_params.FS_VIEDOPORT+"," +_params.FS_VIEDOUSER+"," +_params.FS_VIEDOPWD);                
            }

            if (_params.FS_METERPARA != "")
            {
                _weight = new CarWeight();
                _weight.DeviceName = _params.FS_METERTYPE;
                _weight.Init(_params.FS_METERPARA);
            }
            if (_params.FS_PRINTERIP != "")
            {
                _printer = new CorePrinter();
                _printer.Init(_params.FS_PRINTERNAME);
            }
       
            return ret; 
        }

        public void Run()
        {
          
                if (_dvr != null)
                {
                    try
                    {
                        _dvr.Open();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }


                if (_led != null)
                {
                    try
                    {
                        _led.Open();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                if (_iologic != null)
                {
                    try
                    {
                        _iologic.Open();
                    }
                    catch (Exception ex)
                    {
                        
                        throw ex;
                    }
                 
                }
                if (_rtu != null)
                {
                    try
                    {
                        _rtu.Open();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }

            if (_card != null && !_card.Open())
            {
                _card = null;
            }
            if (_lcd != null && !_lcd.Open()) _lcd = null;
            if (_weight != null && !_weight.Open()) _weight = null;
        }

        public int Finit()
        {
            int ret = -1;
            if (_dvr != null)
            {
                try
                {
                    _dvr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            if (_lcd != null && _lcd.SerialPort.IsOpen)
            {
                try
                {
                    _lcd.Close();
                }
                catch (Exception ex)
                {
                    
                    throw ex ;
                }
            }

            if (_led != null)
            {
                try
                {
                    _led.Close();
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
               
            }
            if (_iologic != null)
            {
                try
                {
                    _iologic.Close(); 
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
             
            }
            if (_card != null)
            {
                try
                {
                    _card.Close(); 
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
            if (_weight != null)
            {
                try
                {
                    _weight.Close(); 
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
                }
            if (_rtu != null)
            {
                try
                {
                    _rtu.Close(); 
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
              }
            return ret;
        }
        #endregion
    }
}
