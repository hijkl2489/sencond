using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YGJZJL.PublicComponent
{
    public partial class WaitingForm : Form
    {
        bool m_bRun = true;
        private System.Threading.Thread m_hThread;//Ïß³Ì
        private bool m_ShowToUser = false;

        public WaitingForm()
        {
            InitializeComponent();
        }

        public bool ShowToUser
        {
            get
            {
                return m_ShowToUser;
            }
            set
            {
                m_ShowToUser = value;
            }
        }

        private void WaitingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constant.WaitingForm = null;
        }

        private void WaitingForm_Load(object sender, EventArgs e)
        {
            Visible = true;
            m_ShowToUser = true;
 
            m_hThread = new System.Threading.Thread(new System.Threading.ThreadStart(GetVisibleOfThis));
            m_hThread.Start();            
        }

        private void GetVisibleOfThis()
        {
            while (m_bRun)
            {
                try
                {
                    System.Threading.Thread.Sleep(100);

                    if (m_ShowToUser == false)
                    {
                        Hide();
                    }

                    else//(m_ShowToUser == true)
                    {
                        Show();
                        Update();
                    }
                }
                catch
                {
                }                
            }
        }

        private void WaitingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_ShowToUser = false;
            Hide();
        }
    }
}