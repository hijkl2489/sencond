using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using YGJZJL.CarSip.Client.App;

namespace YGJZJL.CarSip.Client.Test
{
    public partial class FrmLCD : Form
    {
        private HgLcd _lcd = null;

        public FrmLCD()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _lcd.Data = new string[5];
            _lcd.Data[0] = "坯料重量：1789 公斤";
            _lcd.Data[1] = "物质类型： 钢坯";
            _lcd.Data[2] = "卸货地点：型材原料库";
            _lcd.Data[3] = "卸货人： ";
            _lcd.Data[4] = "卸货时间： 2012年4月17日";
            _lcd.DisplayData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _lcd = new HgLcd();
            _lcd.Init("COM13");
            _lcd.Open();            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(textBox1.Text.Trim());

            //调用寄存器地址1的图片。
            bool b = _lcd.DrawPicture(a);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _lcd.ClearScreen();
        }  

       
    }
}
