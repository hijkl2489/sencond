/*
 * 机柜类，
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YGJZJL.CarSip.Client.Meas
{
    public class CoreCabinet 
    {
        #region <成员变量>
        
        // 数据采集仪表
        private CoreRtu _rtu = null;         // 仪表
        private IcCard _card = null;     // 读卡器
        private LCDScreen _lcd = null;   // 液晶屏
        private LedScreen _led = null;   // LED显示屏
        private CoreIoLogik _iologic = null; // MOXA IoLogic
        private CoreWeight _weight = null;
        private LablePrinter _printer = null;
        //线程
        private System.Threading.Thread _thread; //线程
        // 视频
        private HkDvr _dvr;
        private int[] _video_channel;
        // 配置参数
        BTDevice[] _params;
        #endregion

        #region <属性>
        // 基础信息
        public string Name { get; set; }
        public string Code { get; set; }
        public string Depart { get; set; }
        public BTDevice[] Params
        {
            get { return _params; }
            set { _params = value; }
        }
        public CoreRtu Rtu
        {
            get { return _rtu; }
        }
        public IcCard Card
        {
            get { return _card; }
        }
        public LCDScreen Lcd
        {
            get { return _lcd; }
        }
        public LedScreen Led
        {
            get { return _led; }
        }
        public CoreIoLogik IoLogik
        {
            get { return _iologic; }
        }
        public HkDvr Dvr
        {
            get { return _dvr; }
        }
        public CoreWeight Weight
        {
            get { return _weight; }
        }
        public int[] VideoChannel
        {
            get { return _video_channel; }
        }
        public LablePrinter Printer
        {
            get { return _printer; }
        }
        #endregion
        #region <公共方法>
        public int Init()
        {
            int ret = -1;
            string [] strParams = new string [20];
            //_params = new BT_POINT();
            foreach (BTDevice devParam in _params)
            {
                switch (devParam.FS_TYPE)
                {
                    case "WGT":
                        _weight = new CoreWeight();
                        _weight.Init(devParam.FS_PARAM);
                        break;
                    case "LCD":
                        _lcd = new LCDScreen();
                        _lcd.Init(devParam.FS_PARAM);
                        break;
                    case "LED":
                        _led = new LedScreen();
                        _led.Init(devParam.FS_PARAM);
                        break;
                    case "ZPL":
                        _printer = new LablePrinter();
                        _printer.Init(devParam.FS_PARAM);
                        break;
                    case "DVR":
                        _dvr = new HkDvr();                                          
                        break;
                    case "RTU":
                        _rtu = new CoreRtu();
                        _rtu.init(devParam.FS_PARAM);
                        break;
                    case "IOLOGIK":
                        _iologic = new CoreIoLogik();
                        _iologic.Init(devParam.FS_PARAM);
                        break;
                    case"CARD":
                        _card = new IcCard();
                        _card.Init(devParam.FS_PARAM,1);
                        break;
                }
            }
            
            _thread = null;//new System.Threading.Thread();              
            return 0; 
        }

        public void Run()
        {
            if (_lcd != null) _lcd.Open();
            if (_led != null) _led.Open();
            if (_iologic != null) _iologic.Open();
            if (_card != null) _card.Open();
            if (_weight != null) _weight.Open(); 
        }

        public int Finit()
        {
            int ret = -1;
            if (_dvr != null) _dvr.Close();                 
            if (_lcd != null) _lcd.Close();
            if (_led != null) _led.Close();
            if (_iologic != null) _iologic.Close();
            if (_card != null) _card.Close();
            if (_weight != null) _weight.Close();
            return 0;
        }
        #endregion
    }
}
