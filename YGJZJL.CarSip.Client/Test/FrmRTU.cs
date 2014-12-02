using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Collections;
using YGJZJL.CarSip.Client.App;
namespace YGJZJL.CarSip.Client.Test
{
    public partial class FrmRtu : Form
    {

        public System.Threading.Thread m_DataCollectThread;
        public byte portnum = 0;
        public string ip = "10.25.15.247";
        public int port = 1100;//1100; //
        public bool state1 = false;
        public bool state2 = false;
        public bool state3 = false;
        public bool state4 = false;
        public bool state5 = false;
        public bool state6 = false;
        public bool state7 = false;
        public bool state8 = false;
        public bool m_bRunning = false;
        public HgRtu _rtu;


        public FrmRtu()
        {
            InitializeComponent();
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[0])
              _rtu.OpenSwitch(0); 
            else
              _rtu.CloseSwitch(0); 

            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                       
                _rtu.OpenGreen();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _rtu = new HgRtu();
            //_rtu.init(ip + ":" + port.ToString());
            _rtu.IP = ip;
            _rtu.Port = (ushort)port;
            _rtu.DOChanged +=new RtuChangedEventHandler(OnDOChanged);
            _rtu.Open();
            if (_rtu.DO[1]) btRedGrean.Text = "开绿灯";
            else btRedGrean.Text = "开红灯";
        }
        private delegate void setText(int index, bool status);
        private void OnDOChanged(object sender, RtuEventArgs e)
        {
            Invoke(new setText(ChangeText), new object[] { e.Value.index, e.Value.status }); 
        }
        private void ChangeText(int index, bool status)
        {
            txtChange.Text = index.ToString() + " = " + status.ToString();
        }

        private void Refresh()
        {
            System.Threading.Thread.Sleep(200);

            byte[] dis = new byte[1];

            _rtu.ReadDO();
            //MessageBox.Show(Convert.ToInt32(dis).ToString());
            if (_rtu.DO[0])
                button1.Text = "关闭";
            else
                button1.Text = "打开";

            if (_rtu.DO[1])
                button2.Text = "关闭";
            else
                button2.Text = "打开";

            if (_rtu.DO[2])
                button3.Text = "关闭";
            else
                button3.Text = "打开";

            if (_rtu.DO[3])
                button4.Text = "关闭";
            else
                button4.Text = "打开";

            if (_rtu.DO[4])
                button5.Text = "关闭";
            else
                button5.Text = "打开";

            if (_rtu.DO[5])
                button6.Text = "关闭";
            else
                button6.Text = "打开";

            if (_rtu.DO[6])
                button7.Text = "关闭";
            else
                button7.Text = "打开";

            if (_rtu.DO[7])
                button8.Text = "关闭";
            else
                button8.Text = "打开";
            label8.Text = _rtu.DO[0].ToString();
            label9.Text = _rtu.DO[1].ToString();
            label10.Text = _rtu.DO[2].ToString();
            label11.Text = _rtu.DO[3].ToString();
            label12.Text = _rtu.DO[4].ToString();
            label13.Text = _rtu.DO[5].ToString();
            label14.Text = _rtu.DO[6].ToString();
            label16.Text = _rtu.DO[7].ToString();
        }

        public void GetVal(byte[] dmsg)
        {
            byte[] d = new byte[1];
            d[0] = dmsg[0];
            //d[1] = dmsg[1];
            BitArray all = new BitArray(d);
            state1 = all.Get(0);//照明灯状态
            state2 = all.Get(1);//红绿灯状态
            state3 = all.Get(3);//前道闸状态
            state4 = all.Get(4);//后道闸状态
            state5 = all.Get(5);//电源状态
            state6 = all.Get(6);//外红外状态
            state7 = all.Get(7);//内红外状态
            state8 = all.Get(8);//ATM灯状态
            if (state1)
                button1.Text = "关闭";
            else
                button1.Text = "打开";

            if (state2)
                button2.Text = "关闭";
            else
                button2.Text = "打开";
            
            if (state3)
                button3.Text = "关闭";
            else
                button3.Text = "打开";

            if (state4)
                button4.Text = "关闭";
            else
                button4.Text = "打开";

            if (state5)
                button5.Text = "关闭";
            else
                button5.Text = "打开";

            if (state6)
                button6.Text = "关闭";
            else
                button6.Text = "打开";

            if (state7)
                button7.Text = "关闭";
            else
                button7.Text = "打开";

            if (state8)
                button8.Text = "关闭";
            else
                button8.Text = "打开";

            label8.Text = state1.ToString();
            label9.Text = state2.ToString();
            label10.Text = state3.ToString();
            label11.Text = state4.ToString();
            label12.Text = state5.ToString();
            label13.Text = state6.ToString();
            label14.Text = state7.ToString();
            label16.Text = state8.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[2])
                _rtu.OpenSwitch(2);
            else
                _rtu.CloseSwitch(2);
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[3])
                _rtu.OpenSwitch(3);
            else
                _rtu.CloseSwitch(3);
            Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[4])
                _rtu.OpenSwitch(4);
            else
                _rtu.CloseSwitch(4);
            Refresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[5])
                _rtu.OpenSwitch(5);
            else
                _rtu.CloseSwitch(5);
            Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[0])
                _rtu.OpenSwitch(0);
            else
                _rtu.CloseSwitch(0);
            Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[1])
                _rtu.OpenSwitch(1);
            else
                _rtu.CloseSwitch(1);
            Refresh();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (_rtu.DO[1])
            {
                _rtu.OpenGreen();
                this.btRedGrean.Text = "开红灯";
            }
            else
            {
                _rtu.OpenRed();
                this.btRedGrean.Text = "开绿灯";
            }
            Refresh();
        }

      

        private void button9_Click(object sender, EventArgs e)
        {
            Refresh();
        }


     
    }
}
