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
    /// 仪表清零代理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void MeterClearEventHandler(object sender, EventArgs e);

    /// <summary>
    /// 仪表状态
    /// </summary>
    public enum MeterStatus 
    {
        /// <summary>
        /// Stable  稳定
        /// </summary>
        Stable,
        /// <summary>
        /// 不稳定
        /// </summary>
        UnStable,
        /// <summary>
        /// 空磅
        /// </summary>
        Null,
        /// <summary>
        /// 未连接
        /// </summary>
        UnConnect
    }

    public partial class MeterControl : UserControl
    {
        public event MeterClearEventHandler MeterClear;

        private MeterStatus _status = MeterStatus.UnConnect;
        //private MeterStatus _status = MeterStatus.Null;

        /// <summary>
        /// 设置/获取仪表状态
        /// </summary>
        public MeterStatus Status
        {
            get { return _status; }
            set 
            { 
                _status = value;
                switch (value)
                {
                    case MeterStatus.Null:
                        this.lblStatus.ForeColor = Color.Green;
                        this.txtStatus.Text = "空磅";
                        break;
                    case MeterStatus.UnStable:
                        this.lblStatus.ForeColor = Color.Red;
                        this.txtStatus.Text = "不稳定";
                        break;
                    case MeterStatus.Stable:
                        this.lblStatus.ForeColor = Color.Green;
                        this.txtStatus.Text = "稳定";
                        break;
                    case MeterStatus.UnConnect:
                        this.lblStatus.ForeColor = Color.Red;
                        this.txtStatus.Text = "未连接";
                        break;
                }
            }
        }

        private double _weight = 0;

        /// <summary>
        /// 设置/获取仪表重量
        /// </summary>
        public double Weight
        {
            get { return _weight; }
            set 
            { 
                _weight = value;
                this.txtWeight.Text = value.ToString("0.000");
            }
        }

        public MeterControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 仪表清零
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Weight = 0;
            this.Status = MeterStatus.Null;
            OnMeterClear();
        }

        public virtual void OnMeterClear()
        {
            if (MeterClear == null) return;
            EventArgs arg = new EventArgs();
            MeterClear(this, arg);
        }
    }
}
