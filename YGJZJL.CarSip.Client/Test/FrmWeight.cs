using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.CarSip.Client.Meas;
namespace YGJZJL.CarSip.Client.Test
{
    public partial class FrmWeight : Form
    {
        CoreWeight _weight = null;
        public FrmWeight()
        {
            InitializeComponent();
            _weight = new CoreWeight();            
        }
        delegate void setText(string str);
        public void OnWeightChanged(object sender, WeightEventArgs e)
        {
            this.Invoke(new setText(setWtChange), new object[] { e.Value.ToString() });                     
        }
        private void setWtChange(string str)
        {
            this.tbWtChange.Text = str;
        }
        private void setWtCompelte(string str)
        {
            this.tbWtComplete.Text = str;
        }
        public void OnWeightCompleted(object sender, WeightEventArgs e)
        {
            this.Invoke(new setText(setWtCompelte), new object[] { e.Value.ToString() });
        }

        private void FrmWeight_Load(object sender, EventArgs e)
        {
            /*
             * 串口参数(串口名，波特率，校验位，数据位，停止位)，
             * 称重协议(开始位，结束位，报文长度，重量数据开始位置，重量数据开始位置，重量数据长度,数据是否逆转)，
             * 限定值(最小重量，最大重量，稳定值计数器最大值) 
             */
            _weight.Init("COM2,4800,N,8,1,02,0D,17,6,9,0");
            _weight.DeviceName = "";
            //_weight.SerialPort.NewLine = new byte[] { 0x0D };
            _weight.WeightChanged += new WeightChangedEventHandler(OnWeightChanged);
            _weight.WeightCompleted += new WeightCompletedEventHandler(OnWeightCompleted);
            _weight.Open();
        }

        private void FrmWeight_FormClosed(object sender, FormClosedEventArgs e)
        {
            _weight.Close();
        }
    }
}
