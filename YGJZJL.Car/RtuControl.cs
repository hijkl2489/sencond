using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YGJZJL.Car
{
    /// <summary>
    /// 红绿灯控制代理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void TrafficEventHandler(object sender, TrafficEventArgs e);
    /// <summary>
    /// 照明灯控制
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LightEventHandler(object sender, LightEventArgs e);
    /// <summary>
    /// 电铃控制
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RingEventHandler(object sender, RingEventArgs e);
    /// <summary>
    /// 设备重启控制
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RestartHandler(object sender, EventArgs e);


    #region 控制状态枚举
    /// <summary>
    /// 红绿灯状态
    /// </summary>
    public enum TrafficLightStatus
    {
        /// <summary>
        /// 绿灯
        /// </summary>
        Green,
        /// <summary>
        /// 红灯
        /// </summary>
        Red
    }

    /// <summary>
    /// 照明灯状态
    /// </summary>
    public enum LightStatus
    {
        /// <summary>
        /// 打开
        /// </summary>
        Open,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed
    }

    /// <summary>
    /// 电铃状态
    /// </summary>
    public enum RingStatus
    {
        /// <summary>
        /// 响铃
        /// </summary>
        Ring,
        /// <summary>
        /// 停止
        /// </summary>
        Stop
    }

    /// <summary>
    /// 红外连接状态
    /// </summary>
    public enum InfraredRadioStatus
    {
        /// <summary>
        /// 连接
        /// </summary>
        Connected,
        /// <summary>
        /// 断开
        /// </summary>
        Disconnected
    }
    #endregion

    public partial class RtuControl : UserControl
    {
        #region <事件>
        public event TrafficEventHandler TrafficStatusChanged;
        public event LightEventHandler LightStatusChanged;
        public event RingEventHandler RingStatusChanged;
        public event RestartHandler DeviceRestart;
        #endregion

        #region 控制属性
        private TrafficLightStatus _trafficLight = TrafficLightStatus.Green;

        /// <summary>
        /// 设置/获取红绿灯状态
        /// </summary>
        public TrafficLightStatus TrafficLight
        {
            get { return _trafficLight; }
            set 
            { 
                _trafficLight = value;
                switch (value)
                {
                    case TrafficLightStatus.Green:
                        coreIndicator1.ForeColor = Color.Green;
                        break;
                    case TrafficLightStatus.Red:
                        coreIndicator1.ForeColor = Color.Red;
                        break;
                }
            }
        }
        private LightStatus _light = LightStatus.Closed;

        /// <summary>
        /// 设置/获取照明灯状态
        /// </summary>
        public LightStatus Light
        {
            get { return _light; }
            set 
            { 
                _light = value;
                switch (value)
                {
                    case LightStatus.Open:
                        coreIndicator2.ForeColor = Color.LightGray;
                        break;
                    case LightStatus.Closed:
                        coreIndicator2.ForeColor = Color.Black;
                        break;
                }
            }
        }

        private InfraredRadioStatus _frontInfraredRadio = InfraredRadioStatus.Connected;

        /// <summary>
        /// 设置/获取前端红外状态
        /// </summary>
        public InfraredRadioStatus FrontInfraredRadio
        {
            get { return _frontInfraredRadio; }
            set 
            {
                _frontInfraredRadio = value;
                switch (value)
                {
                    case InfraredRadioStatus.Connected:
                        coreInfraredRay1.Connected = true;
                        coreInfraredRay1.ForeColor = Color.DarkBlue;
                        break;
                    case InfraredRadioStatus.Disconnected:
                        coreInfraredRay1.Connected = false;
                        coreInfraredRay1.ForeColor = Color.Red;
                        break;
                }
            }
        }

        private InfraredRadioStatus _backInfreredRadio = InfraredRadioStatus.Connected;

        /// <summary>
        /// 设置/获取后端红外状态
        /// </summary>
        public InfraredRadioStatus BackInfreredRadio
        {
            get { return _backInfreredRadio; }
            set 
            {
                _backInfreredRadio = value;
                switch (value)
                {
                    case InfraredRadioStatus.Connected:
                        coreInfraredRay2.Connected = true;
                        coreInfraredRay2.ForeColor = Color.DarkBlue;
                        break;
                    case InfraredRadioStatus.Disconnected:
                        coreInfraredRay2.Connected = false;
                        coreInfraredRay2.ForeColor = Color.Red;
                        break;
                }
            }
        }

        private RingStatus _ring = RingStatus.Stop;

        /// <summary>
        /// 设置/获取电铃状态
        /// </summary>
        public RingStatus Ring
        {
            get { return _ring; }
            set 
            { 
                _ring = value; 
            }
        }

        /// <summary>
        /// 设备重启按钮是否显示
        /// </summary>
        /// 
        public bool ShowRestartControl
        {
            get { return btnRestart.Visible; }
            set { btnRestart.Visible = value; }
        }
        #endregion

        public RtuControl()
        {
            InitializeComponent();
        }

        private void btnHL_Click(object sender, EventArgs e)
        {
            switch (this.TrafficLight)
            {
                case TrafficLightStatus.Green:
                    this.TrafficLight = TrafficLightStatus.Red;
                    break;
                case TrafficLightStatus.Red:
                    this.TrafficLight = TrafficLightStatus.Green;
                    break;
            }
            OnTrafficLightStatusChange(this.TrafficLight);
        }

        private void btnZMDKG_Click(object sender, EventArgs e)
        {
            switch (this.Light)
            {
                case LightStatus.Closed:
                    this.Light = LightStatus.Open;
                    break;
                case LightStatus.Open:
                    this.Light = LightStatus.Closed;
                    break;
            }
            OnLightStatusChange(this.Light);
        }

        private void btnRing_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Ring == RingStatus.Stop)
            {
                OnRingStatusChange(RingStatus.Ring);
                this.Ring = RingStatus.Ring;
            }
        }

        private void btnRing_MouseLeave(object sender, EventArgs e)
        {
            if (this.Ring == RingStatus.Ring)
            {
                OnRingStatusChange(RingStatus.Stop);
                this.Ring = RingStatus.Stop;
            }
        }

        private void btnRing_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.Ring == RingStatus.Ring)
            {
                OnRingStatusChange(RingStatus.Stop);
                this.Ring = RingStatus.Stop;
            }
        }

        public virtual void OnTrafficLightStatusChange(TrafficLightStatus status)
        {
            if (TrafficStatusChanged == null) return;
            TrafficEventArgs arg = new TrafficEventArgs(status);
            TrafficStatusChanged(this, arg);
        }

        public virtual void OnLightStatusChange(LightStatus status)
        {
            if (LightStatusChanged == null) return;
            LightEventArgs arg = new LightEventArgs(status);
            LightStatusChanged(this, arg);
        }

        public virtual void OnRingStatusChange(RingStatus status)
        {
            if (RingStatusChanged == null) return;
            RingEventArgs arg = new RingEventArgs(status);
            RingStatusChanged(this, arg);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (DeviceRestart == null) return;
            DeviceRestart(sender, e);
        }
    }

    #region <定义控制事件参数>
    /// <summary>
    /// 红绿灯控制
    /// </summary>
    public class TrafficEventArgs : EventArgs
    {
        private TrafficLightStatus _value = TrafficLightStatus.Green;

        public TrafficLightStatus Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public TrafficEventArgs()
        {
            _value = TrafficLightStatus.Green;
        }
        public TrafficEventArgs(TrafficLightStatus value)
        {
            this._value = value;
        }
    }

    /// <summary>
    /// 照明灯控制
    /// </summary>
    public class LightEventArgs : EventArgs
    {
        private LightStatus _value = LightStatus.Closed;

        public LightStatus Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public LightEventArgs()
        {
            _value = LightStatus.Closed;
        }
        public LightEventArgs(LightStatus value)
        {
            this._value = value;
        }
    }

    /// <summary>
    /// 电铃控制
    /// </summary>
    public class RingEventArgs : EventArgs
    {
        private RingStatus _value = RingStatus.Stop;

        public RingStatus Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public RingEventArgs()
        {
            _value = RingStatus.Stop;
        }
        public RingEventArgs(RingStatus value)
        {
            this._value = value;
        }
    }
    #endregion
}
