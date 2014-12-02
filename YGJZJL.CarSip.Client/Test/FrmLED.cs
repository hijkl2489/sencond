using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using YGJZJL.CarSip.Client.Meas;




namespace YGJZJL.CarSip.Client.Test
{
    public partial class FrmLED : Form
    {

        LedScreen _led;
        public FrmLED()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            _led = new LedScreen();
            _led.Init("10.25.168.230,0,6666,9990");
            _led.Open();
            _led.SendText("C#客户端文本", "宋体", 12, 1, 1, 0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _led.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _led.SetPower(1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            _led.SetPower(0);
        }

        private void FrmLED_Load(object sender, EventArgs e)
        {
            _led = new LedScreen();
            _led.Open();
        }
    }
        
}
