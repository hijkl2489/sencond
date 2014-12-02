using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.Sip.Client.Meas;


namespace Core.Sip.Client.Test
{
    public partial class FrmIcCard : Form
    {
        IcCard card;

        public FrmIcCard()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            card.Open();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            card = new IcCard();            
            card.PortName = "COM1";
            
        }
        
        private void btWrite_Click(object sender, EventArgs e)
        {
            card.WriteData(tbValue.Text, (int)nudSector.Value, (int)nudBlock.Value);
            tbID.Text = card.ID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            card.Close();
        }

        private void btRead_Click(object sender, EventArgs e)
        {
            tbValue.Text = card.ReadData((int)nudSector.Value, (int)nudBlock.Value);
            tbID.Text = card.ID;
        }       
    }
}
