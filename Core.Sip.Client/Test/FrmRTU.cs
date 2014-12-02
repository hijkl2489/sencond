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
using Core.Sip.Client.Meas;
namespace Core.Sip.Client.Test
{
    public partial class FrmRtu : Form
    {

        public System.Threading.Thread m_DataCollectThread;
        public byte portnum = 0;
        public string ip = "10.25.168.101";
        public int port = 1100;
        public bool state1 = false;
        public bool state2 = false;
        public bool state3 = false;
        public bool state4 = false;
        public bool state5 = false;
        public bool state6 = false;
        public bool state7 = false;
        public bool state8 = false;
        public bool m_bRunning = false;
        public CoreRtu RC;


        public FrmRtu()
        {
            InitializeComponent();
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RC.DO[0])
              RC.OpenSwitch(0); 
            else
              RC.CloseSwith(0); 

            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RC.DO[1])
                RC.OpenSwitch(1);
            else
                RC.CloseSwith(1);
            Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RC = new CoreRtu();
            RC.init(ip + ":" + port.ToString());
            //RC.IP = ip;
            //RC.Port = (ushort)port;
            portnum = Convert.ToByte("1");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Refresh();
        }

        private void Refresh()
        {
            System.Threading.Thread.Sleep(200);

            byte[] dis = new byte[1];

            RC.ReadDO();
            //MessageBox.Show(Convert.ToInt32(dis).ToString());
            if (RC.DO[0])
                button1.Text = "关闭";
            else
                button1.Text = "打开";

            if (RC.DO[1])
                button2.Text = "关闭";
            else
                button2.Text = "打开";

            if (RC.DO[2])
                button3.Text = "关闭";
            else
                button3.Text = "打开";

            if (RC.DO[3])
                button4.Text = "关闭";
            else
                button4.Text = "打开";

            if (RC.DO[4])
                button5.Text = "关闭";
            else
                button5.Text = "打开";

            if (RC.DO[5])
                button6.Text = "关闭";
            else
                button6.Text = "打开";

            if (RC.DO[6])
                button7.Text = "关闭";
            else
                button7.Text = "打开";

            if (RC.DO[7])
                button8.Text = "关闭";
            else
                button8.Text = "打开";
            label8.Text = RC.DO[0].ToString();
            label9.Text = RC.DO[1].ToString();
            label10.Text = RC.DO[2].ToString();
            label11.Text = RC.DO[3].ToString();
            label12.Text = RC.DO[4].ToString();
            label13.Text = RC.DO[5].ToString();
            label14.Text = RC.DO[6].ToString();
            label16.Text = RC.DO[7].ToString();
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

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (RC.DO[2])
                RC.OpenSwitch(2);
            else
                RC.CloseSwith(2);
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RC.DO[3])
                RC.OpenSwitch(3);
            else
                RC.CloseSwith(3);
            Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (RC.DO[4])
                RC.OpenSwitch(4);
            else
                RC.CloseSwith(4);
            Refresh();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (RC.DO[5])
                RC.OpenSwitch(5);
            else
                RC.CloseSwith(5);
            Refresh();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (RC.DO[0])
                RC.OpenSwitch(0);
            else
                RC.CloseSwith(0);
            Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (RC.DO[1])
                RC.OpenSwitch(1);
            else
                RC.CloseSwith(1);
            Refresh();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            byte[] d = new byte[1];
            d[0] = 5;
            //d[1] = dmsg[1];
            BitArray all = new BitArray(d);
            bool b1 = all.Get(0);//照明等状态
            bool b2 = all.Get(1);//红绿灯状态
            bool b3 = all.Get(2);//前道闸状态
            bool b4 = all.Get(3);//后道闸状态
            bool b5 = all.Get(4);//电源状态
            bool b6 = all.Get(5);//电源状态

            BitArray cc = new BitArray(8);
            cc.Set(0, b1);
            cc.Set(1, b2);
            cc.Set(2, b3);
            cc.Set(3, b4);
            cc.Set(4, b5);
            cc.Set(5, b6);

            int a = BitArray2Int(cc);
            UInt32 b = Convert.ToUInt32(a);


        }

        public int BitArray2Int(BitArray ba)
        {
            Int32 ret = 0;
            for (Int32 i = 0; i < ba.Length; i++)
            {
                if (ba.Get(i))
                {
                    ret |= (1 << i);
                }
            }
            return ret;
        }


     
    }
}
