using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using YGJZJL.CarSip.Client.Meas;
namespace YGJZJL.CarSip.Client.App
{
    #region <定义读卡事件>
    /// <summary>
    /// 卡变化事件
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">卡参数</param>
    public delegate void CardChangedEventHandlerRegist(object sender, CardEventArgsRegist e);
    #endregion
    #region <定义卡参数>
    public class CardEventArgsRegist : EventArgs
    {
        private CardData _value;
        public CardData Value
        {
            get { return _value; }
            set { this._value = value; }
        }
        public CardEventArgsRegist()
        {
            _value = new CardData();
        }
        public CardEventArgsRegist(CardData value)
        {
            this._value = value;
        }

    }
    #endregion

    public class RegistIcCard : IcCard
    {

        #region <事件>
        public event CardChangedEventHandlerRegist CardChanged = null;
        #endregion
        // add 
        uint _pre_snr = 0;    // 前一张卡的序列号
        Thread _thread = null; // 线程
        Mutex _mutex = null;

        public Thread CardThread
        {
            get { return _thread; }

        }
        public Mutex CardMutex
        {
            get { return _mutex; }
        }
        public RegistIcCard()
        {
            _pre_snr = 0;
            _thread = null;
            CardChanged = null;
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
            int ret = -1;
            if (_thread != null) _thread.Abort();
            return base.Close();
            
        }
        // 事件处理函数
        public virtual void OnCardChange(CardData data)
        {
            if (CardChanged == null) return;
            CardEventArgsRegist arg = new CardEventArgsRegist(data);
            CardChanged(this, arg);
        }
        // 线程刷新卡
        private void FreshCardData()
        {
            int ret = -1;
            while (true)
            {
                Thread.Sleep(2000);
                //_mutex.WaitOne();
                CardData data = ReadBaseData();             
                if (_pre_snr == Snr) continue;
                _pre_snr = Snr;
                if (Snr != 0)
                {
                    Beep(10);
                    OnCardChange(data);
                }
            }
        }

        public bool WriteCard(CardData data)
        {
            bool ret = false;
            try
            {
                InitCard();
                if (!string.IsNullOrEmpty(data.ID)) WriteData(0, 0, data.ID);
                if (!string.IsNullOrEmpty(data.CardNo)) WriteData(0, 1, data.CardNo);
                if (!string.IsNullOrEmpty(data.CarNo)) WriteData(0, 2, data.CarNo);
                if (!string.IsNullOrEmpty(data.FirLocNo)) WriteData(1, 0, data.FirLocNo);
                if (!string.IsNullOrEmpty(data.FirWeight)) WriteData(1, 1, data.FirWeight);
                if (!string.IsNullOrEmpty(data.CardType)) WriteData(1, 2, data.CardType);
                if (!string.IsNullOrEmpty(data.MateriaName)) WriteData(2, data.MateriaName);
                if (!string.IsNullOrEmpty(data.Sender)) WriteData(3, data.Sender);
                if (!string.IsNullOrEmpty(data.Receiver)) WriteData(4, data.Receiver);
                if (!string.IsNullOrEmpty(data.PlanLoc)) WriteData(5, data.PlanLoc);
                if (!string.IsNullOrEmpty(data.SecLocNo)) WriteData(6, 0, data.SecLocNo);
                if (!string.IsNullOrEmpty(data.SecWeight)) WriteData(6, 1, data.SecWeight);       
                if (!string.IsNullOrEmpty(data.WgtOpCd)) WriteData(7, data.WgtOpCd);
                if (!string.IsNullOrEmpty(data.UnloadName)) WriteData(8, data.UnloadName);
                if (!string.IsNullOrEmpty(data.UnloadDepart)) WriteData(9, data.UnloadDepart);
                if (!string.IsNullOrEmpty(data.UnloadTime)) WriteData(10, data.UnloadTime);
                if (!string.IsNullOrEmpty(data.UnloadFlag)) WriteData(11, 0, data.UnloadFlag);
                if (!string.IsNullOrEmpty(data.UnloadKZ)) WriteData(11, 1, data.UnloadKZ);
                if (!string.IsNullOrEmpty(data.Reserve6)) WriteData(11, 2, data.Reserve6);
                if (!string.IsNullOrEmpty(data.PassNo)) WriteData(12, data.PassNo);
                if (!string.IsNullOrEmpty(data.EnterGate)) WriteData(13, 0, data.EnterGate);
                if (!string.IsNullOrEmpty(data.EnterChecker)) WriteData(13, 1, data.EnterChecker);
                if (!string.IsNullOrEmpty(data.EnterTime)) WriteData(13, 2, data.EnterTime);
                if (!string.IsNullOrEmpty(data.LeaveGate)) WriteData(14, 0, data.LeaveGate);
                if (!string.IsNullOrEmpty(data.LeaveChecker)) WriteData(14, 1, data.LeaveChecker);
                if (!string.IsNullOrEmpty(data.LeaveTime)) WriteData(14, 2, data.LeaveTime);
                if (!string.IsNullOrEmpty(data.Reserve7)) WriteData(15, 0, data.Reserve7);
                if (!string.IsNullOrEmpty(data.Reserve8)) WriteData(15, 1, data.Reserve8);
                if (!string.IsNullOrEmpty(data.Reserve9)) WriteData(15, 2, data.Reserve9);
                Beep(10);
				ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }

        public CardData ReadBaseData()
        {
            CardData data = new CardData();
            InitCard();
            data.ID = ID;
            data.CardNo = ReadData(0, 1).Replace("\0","");
            //data.CarNo = ReadData(0, 2).Replace("\0", "");
            data.CardType = ReadData(1, 2).Replace("\0", "");
            //data.UnloadFlag = ReadData(11, 0).Replace("\0", "");//卸货标志
            //if (!string.IsNullOrEmpty(data.UnloadFlag))
            //{
            //    data.UnloadKZ = ReadData(11, 1).Replace("\0", "");//扣渣
            //    data.UnloadName = ReadData(8).Replace("\0", "");//卸货人
            //    data.UnloadDepart = ReadData(9).Replace("\0", "");//卸货点
            //    data.UnloadTime = ReadData(10).Replace("\0", "");//卸货时间
            //}
            return data;
        }

        public CardData ReadCard()
        {
            CardData data = new CardData();
            InitCard();            
            data.ID = ID;
            data.CardNo = ReadData(0, 1);
            data.CarNo = ReadSectorData(0, 2);

            data.FirLocNo = ReadData(1, 0);
            data.FirWeight = ReadSectorData(1, 1);
            data.CardType = ReadSectorData(1, 2);
            data.MateriaName = ReadData(2);
            data.Sender = ReadData(3);
            data.Receiver = ReadData(4);
            data.PlanLoc = ReadData(5);
            data.SecLocNo = ReadData(6, 0);
            data.SecWeight = ReadSectorData(6, 1);          
            data.WgtOpCd = ReadData(7);
            data.UnloadName = ReadData(8);
            data.UnloadDepart = ReadData(9);
            data.UnloadTime = ReadData(10);
            data.UnloadFlag = ReadData(11, 0);
            data.UnloadKZ = ReadSectorData(11, 1);
            data.Reserve6 = ReadSectorData(11, 2);
            data.PassNo = ReadData(12);
            data.EnterGate = ReadData(13, 0);
            data.EnterChecker = ReadSectorData(13, 1);
            data.EnterTime = ReadSectorData(13, 2);
            data.LeaveGate = ReadData(14, 0);
            data.LeaveChecker = ReadSectorData(14, 1);
            data.LeaveTime = ReadSectorData(14, 2);
            data.Reserve7 = ReadData(15, 0);
            data.Reserve8 = ReadSectorData(15, 1);
            data.Reserve9 = ReadSectorData(15, 2);            
            Beep(10);
            return data;
        }
    }
}
