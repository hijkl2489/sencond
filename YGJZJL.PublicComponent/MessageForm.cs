using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YGJZJL.PublicComponent
{
    public partial class MessageForm : Form
    {
        int ShowSeconds = 1;
        int show = 0;

        public void SetMessage(string msg)
        {
            show = 0;
            label1.Text = msg;
            Update();
            this.Show();
            this.Update();
        }

        public MessageForm()
        {
            InitializeComponent();

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                show++;
                if (show >= ShowSeconds)
                {
                    this.Hide();
                }
                this.Update();
            }
            else
            {
                show = 0;
            }
        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}