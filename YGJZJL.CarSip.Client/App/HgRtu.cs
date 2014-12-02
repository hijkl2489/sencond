using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using YGJZJL.CarSip.Client.Meas;

namespace YGJZJL.CarSip.Client.App
{
    #region <定义称重代理>
    /// <summary>
    /// RTU变化代理
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="e">RTU事件</param>
    public delegate void RtuChangedEventHandler(object sender, RtuEventArgs e);   
    #endregion

    #region <定义RTU参数>
    /// <summary>
    /// 汽车事件定义
    /// </summary>
    public class RtuEventArgs : EventArgs
    {
        public class ValueType
        {
            public int  index {get;set;}
            public bool status { get; set; }
        }
        private ValueType _value;
        public ValueType Value
        {
            get { return _value; }
            set { this._value = value; }
        }
        public RtuEventArgs()
        {
            _value = new ValueType();
        }
        public RtuEventArgs(int i, bool b)
        {
            _value = new ValueType();
            _value.index = i;
            _value.status = b;
        }        
    }
    #endregion
    public class HgRtu : CoreRtu
    {
        private Thread _thread = null;
        private bool[] _pre_do = null;
        private Thread _ringThread = null;
        private Thread _deviceRestartThread = null;
        #region <事件>
        public event RtuChangedEventHandler DOChanged = null; 
        #endregion
        #region <设备基本方法>
        public bool Open()
        {
            if (DOChanged != null)
            {
                _thread = new Thread(new ThreadStart(Fresh));
                _thread.Start();
            }
            _pre_do = new bool[DO.Length];
            ReadDO();
            for (int i = 0; i < DO.Length; i++) _pre_do[i] = DO[i];

            return true;
        }
        public bool Close()
        {                 
            if (_thread != null) _thread.Abort();
            return true;
        }
        #endregion

       #region <照明灯控制>
        public void OpenLight()
        {
            if (!DO[0]) CloseSwitch(0);
        }
        public void CloseLight()
        {
            if(DO[0])   OpenSwitch(0); 
        }
        #endregion

        #region <红绿灯控制>
        public void OpenGreen()
        {
            if (DO[1]) OpenSwitch(1);
                        
        }
        public void OpenRed()
        {
            if (!DO[1]) CloseSwitch(1);
        }
        #endregion

        #region <仪表清零>
        public void ClearZero()
        {
            OpenSwitch(2);
        }
        #endregion
    
        // 适用于19#和20#汽车衡
        #region <铃声控制>
        public void OpenRing()
        {
            _ringThread = new Thread(new ThreadStart(Ring));
            _ringThread.Start();
        }

        private void Ring()
        {
            if (!DO[3])
            {
                CloseSwitch(3);
                Thread.Sleep(3000);
                OpenSwitch(3);
            }
        }
        public void CloseRing()
        {
            if (DO[3]) OpenSwitch(3);
        }
        #endregion  
 
        #region <远程设备重启>
        public void RestartDevice()
        {
            _deviceRestartThread = new Thread(new ThreadStart(Restart));
            _deviceRestartThread.Start();
        }

        private void Restart()
        {
            if (!DO[4])
            {
                CloseSwitch(4);
                Thread.Sleep(3000);
                OpenSwitch(4);
            }
        }
        #endregion

        // 事件处理函数
        public virtual void OnRtuChange(int i, bool b)
        {
            if (DOChanged == null) return;
            RtuEventArgs arg = new RtuEventArgs(i, b);
            DOChanged(this, arg);
        }

        void Fresh()
        {
            while (true)
            {
                ReadDO();
                for (int i = 0; i < DO.Length; i++)
                {
                    if (_pre_do[i] != DO[i])
                    {                        
                        OnRtuChange(i,DO[i]);
                        _pre_do[i] = DO[i];
                    }
                }     
            }
            Thread.Sleep(1000);
        }

        public void ReRead()
        {
            _pre_do = new bool[DO.Length];
        }
    }
}
