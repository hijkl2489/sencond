using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.CarSip.Client.Meas;
using YGJZJL.CarSip.Client.App;
namespace YGJZJL.CarSip.Client.Test
{
    public partial class FrmIcCard : Form
    {
        HgIcCard card;

        public FrmIcCard()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(card.Status == DeviceStatus.CLOSE) card.Open();
        }
#region <业务处理函数>

        delegate void CardChangeDelegate(CardData data);
        private void OnCardChanged(object sender, CardEventArgs e)
        {
            Invoke( new CardChangeDelegate(HandleCardData) , new object[] { e.Value } );
        }
        void HandleCardData(CardData data)
        {
            tbValue.Text = data.CarNo;
            tbID.Text = data.ID;
        }
#endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            card = new HgIcCard();            
            card.PortName = "COM14";
            //card.CardChanged += new CardChangedEventHandler(OnCardChanged);
            card.Open();
            card.InitCard();
            tbID.Text = card.ID;            
        }
        
        private void btWrite_Click(object sender, EventArgs e)
        {
            //nudSector.Value = 10;
            //nudBlock.Value = 0;
            //tbValue.Text = "宾洪斌";
            //card.CardThread.Suspend();

            card.WriteData((int)nudSector.Value, (int)nudBlock.Value,tbValue.Text);
            card.Beep(10);
            //card.CardThread.Resume();
            //tbID.Text = card.ID;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            card.Close();
        }

        private void btRead_Click(object sender, EventArgs e)
        {
          
            tbValue.Text = card.ReadData((int)nudSector.Value, (int)nudBlock.Value);
            
            //System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            //stopwatch.Start();
            //CardData data = card.ReadCard();
            //stopwatch.Stop();
            //tbValue.Text = stopwatch.Elapsed.Seconds.ToString();
           
        }       
    }
}
