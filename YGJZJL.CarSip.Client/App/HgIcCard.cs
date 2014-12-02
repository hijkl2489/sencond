using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using YGJZJL.CarSip.Client.Meas;
using System.Diagnostics;
namespace YGJZJL.CarSip.Client.App
{
    #region <定义读卡事件>
    /// <summary>
    /// 卡变化事件
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">卡参数</param>
    public delegate void CardChangedEventHandler(object sender, CardEventArgs e);
    #endregion
    #region <定义卡参数>
    public class CardEventArgs : EventArgs
    {
        private CardData _value;
        public CardData Value
        {
            get { return _value; }
            set { this._value = value; }
        }
        public CardEventArgs()
        {
            _value = new CardData();
        }
        public CardEventArgs(CardData value)
        {
            this._value = value;
        }

    }
    #endregion

    public class HgIcCard : IcCard
    {

        #region <事件>
        public event CardChangedEventHandler CardChanged = null;
        #endregion
        // add 
        uint _pre_snr = 0;    // 前一张卡的序列号
        Thread _thread = null; // 线程
        Mutex _mutex = null;
        int _count = 0;
        int _max_count = 5;
        // add 
        Stopwatch _stopwatch = null;
        int _card_again_time_limits = 0;

        private bool _isSaved = false;

        public bool IsSaved
        {
            get { return _isSaved; }
            set { _isSaved = value; }
        }
        private bool _readOnly = true;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

        public Thread CardThread
        {
            get { return _thread; }

        }
        public Mutex CardMutex
        {
            get { return _mutex; }
        }
        public HgIcCard()
        {
            _pre_snr = 0;
            _thread = null;
            CardChanged = null;
            _count = 0;
            _max_count = 5;
            _stopwatch = new Stopwatch();
            _card_again_time_limits = 6;
        }

        public bool Open()
        {
            if (Status == DeviceStatus.CLOSE || Status == DeviceStatus.INIT)
            {
                base.Open();
            }
            if (CardChanged != null)
            {
                _thread = new Thread(new ThreadStart(FreshCardData));
                _thread.Start();
                _mutex = new Mutex();
            }
            return true;
        }
        public bool Close()
        {
            //int ret = -1;
            //if (_thread != null)
            //{
            //    if (_thread.ThreadState == System.Threading.ThreadState.Suspended)
            //        _thread.Resume();
            //    _thread.Abort();
            //}
            //return base.Close();

            int ret = -1;
            if (_thread != null)
            {
                if (_thread.ThreadState == System.Threading.ThreadState.Suspended) _thread.Resume();
                _thread.Abort();
            }
            return base.Close();
            
        }
        // 事件处理函数
        public virtual void OnCardChange(CardData data)
        {
            if (CardChanged == null) return;
            CardEventArgs arg = new CardEventArgs(data);
            CardChanged(this, arg);
        }
        // 线程刷新卡
        private void FreshCardData()
        {
            int ret = -1;
            while (true)
            {
                Thread.Sleep(200);
                InitCard();
                if (Snr == 0)
                {
                    if (_pre_snr != 0 && _stopwatch.Elapsed.TotalSeconds > _card_again_time_limits) _pre_snr = Snr;
                    continue;
                }
                if (_pre_snr == Snr) continue;  //&& _stopwatch.Elapsed.TotalSeconds < _card_again_time_limits
                if (IsSaved) continue;
                _pre_snr = Snr;
                _stopwatch.Stop();
                _stopwatch.Start();
                if (Snr != 0)
                {                    
                    CardData data = ReadBaseData();
                    if (data != null && !string.IsNullOrEmpty(data.CardNo))
                    {                       
                        OnCardChange(data);
                        Beep(10);
                        if (!_readOnly)
                        {
                            _thread.Suspend();
                        }
                    }
                }
            }
        }

        public bool WriteCard(CardData data)
        {
            _readOnly = false;

            bool ret = false;
            switch (base._readerType)
            {
                case ReaderType.COM:
                    try
                    {
                        if (!string.IsNullOrEmpty(data.UnloadFlag))
                        {
                            while (!WriteData(11, 0, data.UnloadFlag) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }
                        if (!string.IsNullOrEmpty(data.UnloadKZ))
                        {
                            while (!WriteData(11, 1, data.UnloadKZ) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }

                        //                Thread.Sleep(50);
                        if (!string.IsNullOrEmpty(data.CardNo))
                        {
                            while (!WriteData(0, 1, data.CardNo) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }
                        //                Thread.Sleep(50);
                        if (!string.IsNullOrEmpty(data.CarNo))
                        {
                            while (!WriteData(0, 2, data.CarNo) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }
                        //                Thread.Sleep(50);
                        if (!string.IsNullOrEmpty(data.FirLocNo))
                        {
                            while (!WriteData(1, 0, data.FirLocNo) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }
                        //                Thread.Sleep(50);
                        if (!string.IsNullOrEmpty(data.FirWeight))
                        {
                            while (!WriteData(1, 1, data.FirWeight) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }
                        //                Thread.Sleep(50);
                        if (!string.IsNullOrEmpty(data.CardType))
                        {
                            while (!WriteData(1, 2, data.CardType) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }
                        //                Thread.Sleep(50);
                        if (!string.IsNullOrEmpty(data.MateriaName))
                        {
                            while (!WriteData(2, data.MateriaName) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }
                        //                Thread.Sleep(50);
                        if (!string.IsNullOrEmpty(data.SecWeight))
                        {

                            while (!WriteData(6, 1, data.SecWeight) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }

                        if (!string.IsNullOrEmpty(data.Sender))
                        {

                            while (!WriteData(3, data.Sender) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }

                        if (!string.IsNullOrEmpty(data.Receiver))
                        {

                            while (!WriteData(4, data.Receiver) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }

                        if (!string.IsNullOrEmpty(data.PlanLoc))
                        {

                            while (!WriteData(5, data.PlanLoc) && _count < _max_count)
                            {
                                InitCard();
                                _count++;
                            }
                        }

                        //if (!string.IsNullOrEmpty(data.Sender)) WriteData(3, data.Sender);
                        //if (!string.IsNullOrEmpty(data.Receiver)) WriteData(4, data.Receiver);
                        //if (!string.IsNullOrEmpty(data.PlanLoc)) WriteData(5, data.PlanLoc);
                        //if (!string.IsNullOrEmpty(data.SecLocNo)) WriteData(6, 0, data.SecLocNo);
                        //if (!string.IsNullOrEmpty(data.SecWeight)) WriteData(6, 1, data.SecWeight);       
                        //if (!string.IsNullOrEmpty(data.WgtOpCd)) WriteData(7, data.WgtOpCd);
                        //if (!string.IsNullOrEmpty(data.UnloadName)) WriteData(8, data.UnloadName);
                        //if (!string.IsNullOrEmpty(data.UnloadDepart)) WriteData(9, data.UnloadDepart);
                        //if (!string.IsNullOrEmpty(data.UnloadTime)) WriteData(10, data.UnloadTime);
                        //if (!string.IsNullOrEmpty(data.UnloadFlag)) WriteData(11, 0, data.UnloadFlag);
                        //if (!string.IsNullOrEmpty(data.UnloadKZ)) WriteData(11, 1, data.UnloadKZ);
                        //if (!string.IsNullOrEmpty(data.Reserve6)) WriteData(11, 2, data.Reserve6);
                        //if (!string.IsNullOrEmpty(data.PassNo)) WriteData(12, data.PassNo);
                        //if (!string.IsNullOrEmpty(data.EnterGate)) WriteData(13, 0, data.EnterGate);
                        //if (!string.IsNullOrEmpty(data.EnterChecker)) WriteData(13, 1, data.EnterChecker);
                        //if (!string.IsNullOrEmpty(data.EnterTime)) WriteData(13, 2, data.EnterTime);
                        //if (!string.IsNullOrEmpty(data.LeaveGate)) WriteData(14, 0, data.LeaveGate);
                        //if (!string.IsNullOrEmpty(data.LeaveChecker)) WriteData(14, 1, data.LeaveChecker);
                        //if (!string.IsNullOrEmpty(data.LeaveTime)) WriteData(14, 2, data.LeaveTime);
                        //if (!string.IsNullOrEmpty(data.Reserve7)) WriteData(15, 0, data.Reserve7);
                        //if (!string.IsNullOrEmpty(data.Reserve8)) WriteData(15, 1, data.Reserve8);
                        //if (!string.IsNullOrEmpty(data.Reserve9)) WriteData(15, 2, data.Reserve9);
                        Beep(10);
                        //ClearCard();
                        _count = 0;
                        _thread.Resume();
                        ret = true;
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                    }
                    break;
                case ReaderType.NET:
                    try
                    {
                        bool[] rets = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true };
                        if (!string.IsNullOrEmpty(data.UnloadFlag))
                            rets[0] = WriteData(11, 0, data.UnloadFlag);
                        if (!string.IsNullOrEmpty(data.UnloadKZ))
                            rets[1] = WriteData(11, 1, data.UnloadKZ);
                        if (!string.IsNullOrEmpty(data.CardNo))
                            rets[2] = WriteData(0, 1, data.CardNo);
                        if (!string.IsNullOrEmpty(data.CarNo))
                            rets[3] = WriteData(0, 2, data.CarNo);
                        if (!string.IsNullOrEmpty(data.FirLocNo))
                            rets[4] = WriteData(1, 0, data.FirLocNo);
                        if (!string.IsNullOrEmpty(data.FirWeight))
                            rets[5] = WriteData(1, 1, data.FirWeight);
                        if (!string.IsNullOrEmpty(data.CardType))
                            rets[6] = WriteData(1, 2, data.CardType);
                        if (!string.IsNullOrEmpty(data.MateriaName))
                            rets[7] = WriteData(2, data.MateriaName);
                        if (!string.IsNullOrEmpty(data.SecWeight))
                            rets[8] = WriteData(6, 1, data.SecWeight);
                        if (!string.IsNullOrEmpty(data.Sender))
                            rets[9] = WriteData(3, data.Sender);
                        if (!string.IsNullOrEmpty(data.Receiver))
                            rets[10] = WriteData(4, data.Receiver);
                        if (!string.IsNullOrEmpty(data.PlanLoc))
                            rets[11] = WriteData(5, data.PlanLoc);
                        Beep(10);
                        _thread.Resume();
                        if (rets[0] && rets[1] && rets[2] && rets[3] &&  rets[5] && rets[6] && rets[7] && rets[8] && rets[9] && rets[10] && rets[11])
                            ret = true;
                    }
                    catch (Exception ex)
                    {
                        ret = false;
                    }
                    break;
            }
            return ret;
        }

        public CardData ReadBaseData()
        {
            CardData data = new CardData();
            InitCard();
            data.ID = ID;
            data.CardNo = ReadData(0, 1).Replace("\0","");
            data.CarNo = ReadData(0, 2).Replace("\0", "");
            data.CardType = ReadData(1, 2).Replace("\0", "");
            data.UnloadFlag = ReadData(11, 0).Replace("\0", "");//卸货标志
            if (!string.IsNullOrEmpty(data.UnloadFlag))
            {
                data.UnloadKZ = ReadData(11, 1).Replace("\0", "");//扣渣
                data.UnloadName = ReadData(8).Replace("\0", "");//卸货人
                data.UnloadDepart = ReadData(9).Replace("\0", "");//卸货点
                data.UnloadTime = ReadData(10).Replace("\0", "");//卸货时间
                data.WgtOpCd = ReadData(7).Replace("\0", "");//计量编号
            }
            return data;
        }

        public CardData ReadCard()
        {
            CardData data = new CardData();
            InitCard();            
            data.ID = ID;
            data.CardNo = ReadData(0, 1).Replace("\0", "");
            data.CarNo = ReadSectorData(0, 2).Replace("\0", "");

            data.FirLocNo = ReadData(1, 0).Replace("\0", "");
            data.FirWeight = ReadSectorData(1, 1).Replace("\0", "");
            data.CardType = ReadSectorData(1, 2).Replace("\0", "");
            data.MateriaName = ReadData(2).Replace("\0", "");
            data.Sender = ReadData(3).Replace("\0", "");
            data.Receiver = ReadData(4).Replace("\0", "");
            data.PlanLoc = ReadData(5).Replace("\0", "");
            data.SecLocNo = ReadData(6, 0).Replace("\0", "");
            data.SecWeight = ReadSectorData(6, 1).Replace("\0", "");
            data.WgtOpCd = ReadData(7).Replace("\0", "");
            data.UnloadName = ReadData(8).Replace("\0", "");
            data.UnloadDepart = ReadData(9).Replace("\0", "");
            data.UnloadTime = ReadData(10).Replace("\0", "");
            data.UnloadFlag = ReadData(11, 0).Replace("\0", "");
            data.UnloadKZ = ReadSectorData(11, 1).Replace("\0", "");
            data.Reserve6 = ReadSectorData(11, 2).Replace("\0", "");
            data.PassNo = ReadData(12).Replace("\0", "");
            data.EnterGate = ReadData(13, 0).Replace("\0", "");
            data.EnterChecker = ReadSectorData(13, 1).Replace("\0", "");
            data.EnterTime = ReadSectorData(13, 2).Replace("\0", "");
            data.LeaveGate = ReadData(14, 0).Replace("\0", "");
            data.LeaveChecker = ReadSectorData(14, 1).Replace("\0", "");
            data.LeaveTime = ReadSectorData(14, 2).Replace("\0", "");
            data.Reserve7 = ReadData(15, 0).Replace("\0", "");
            data.Reserve8 = ReadSectorData(15, 1).Replace("\0", "");
            data.Reserve9 = ReadSectorData(15, 2).Replace("\0", "");            
            Beep(10);
            return data;
        }

        public void ReRead()
        {

               _pre_snr = 0;
            if (_thread != null && _thread.ThreadState == System.Threading.ThreadState.Suspended)
            {
                _thread.Resume();
            }
            if (this._readerType == ReaderType.NET)
            {
                this.Close();
                Thread.Sleep(200);
                this.Open();
            }
            //_pre_snr = 0;
            
            //_pre_snr = 0;
            //if (_thread != null && _thread.ThreadState == System.Threading.ThreadState.Suspended)
            //{
            //    _thread.Resume();
            //}
            //switch (_readerType)
            //{
            //    case ReaderType.NET:
            //        this.Close();
            //        Thread.Sleep(200);
            //        this.Open();
            //        break;
            //    case ReaderType.COM:
            //        data=ReadCard();
            //        if(data != null && !string.IsNullOrEmpty(data.CardNo))
            //        {
            //            OnCardChange(data);
            //            Beep(10);
            //        }
            //        break;
            //}

            //if (_thread != null && _thread.ThreadState == System.Threading.ThreadState.Suspended)
            //{
            //    _thread.Resume();
            //}
        }
    }
}
