using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Core.Sip.Client.Meas;




namespace Core.Sip.Client.Test
{
    public partial class FrmLED : Form
    {

        LedScreen m_CoolLed;
        public FrmLED()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            m_CoolLed = new LedScreen();
            m_CoolLed.Init("10.25.168.230,0,6666,9990");
            m_CoolLed.Open();
            m_CoolLed.SendText("C#客户端文本", "宋体", 12, 1, 1, 0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_CoolLed.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_CoolLed.SetPower(1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_CoolLed.SetPower(0);
        }

        private void FrmLED_Load(object sender, EventArgs e)
        {
            m_CoolLed = new LedScreen();
            m_CoolLed.Open();
        }
    }
        
}
