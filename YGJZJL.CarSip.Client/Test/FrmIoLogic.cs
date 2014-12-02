using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using YGJZJL.CarSip.Client.Meas;

namespace YGJZJL.CarSip.Client.Test
{
    public partial class FrmIoLogic : Form
    {

        CoreIoLogik _iologic = null;

        public FrmIoLogic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
         
        }

        private void label3_Click(object sender, EventArgs e)
        {
        
        }

        private void label6_Click(object sender, EventArgs e)
        {
        
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

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

        private void btnWirteDO_Click(object sender, EventArgs e)
        {
            if (cbDO1.Text != "") _iologic.DO[0] = Convert.ToBoolean(Convert.ToInt32(cbDO1.Text.Trim()));
            if (cbDO2.Text != "") _iologic.DO[1] = Convert.ToBoolean(Convert.ToInt32(cbDO2.Text.Trim()));
            if (cbDO3.Text != "") _iologic.DO[2] =  Convert.ToBoolean(Convert.ToInt32(cbDO3.Text.Trim()));
            if (cbDO4.Text != "") _iologic.DO[3] = Convert.ToBoolean(Convert.ToInt32(cbDO4.Text.Trim()));
            if (cbDO5.Text != "") _iologic.DO[4] = Convert.ToBoolean(Convert.ToInt32(cbDO5.Text.Trim()));
            if (cbDO6.Text != "") _iologic.DO[5] = Convert.ToBoolean(Convert.ToInt32(cbDO6.Text.Trim()));
            _iologic.WriteDO();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbDO3.Text != "")
            {
                _iologic.DO[2] = Convert.ToBoolean(Convert.ToInt32(cbDO3.Text.Trim()));
                _iologic.WriteDO();
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDO5.Text != "")
            {
                _iologic.DO[4] = Convert.ToBoolean(Convert.ToInt32(cbDO5.Text.Trim()));
                _iologic.WriteDO();
            }

        }

        private void btnReadDO_Click(object sender, EventArgs e)
        {
            _iologic.ReadDO();
            lbDO1.Text = _iologic.DO[0].ToString();
            lbDO2.Text = _iologic.DO[1].ToString();
            lbDO3.Text = _iologic.DO[2].ToString();
            lbDO4.Text = _iologic.DO[3].ToString();
            lbDO5.Text = _iologic.DO[4].ToString();
            lbDO6.Text = _iologic.DO[5].ToString();
        }

        private void btnReadDI_Click(object sender, EventArgs e)
        {
            _iologic.ReadDI();
            lbDI1.Text = _iologic.DI[0].ToString();
            lbDI2.Text = _iologic.DI[1].ToString();
            lbDI3.Text = _iologic.DI[2].ToString();
            lbDI4.Text = _iologic.DI[3].ToString();
            lbDI5.Text = _iologic.DI[4].ToString();
            lbDI6.Text = _iologic.DI[5].ToString();
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
     
        }

        private void reBoot()
        {
            _iologic.Close();
            _iologic = new CoreIoLogik();
            _iologic.Init("10.246.3.31,502");
            _iologic.Open();
        
        }

        private void FrmIoLogic_Load(object sender, EventArgs e)
        {
            _iologic = new CoreIoLogik();
            _iologic.Init("10.25.3.243,502");
            _iologic.Open();
        }

        private void cbDO1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDO1.Text != "")
            {
                _iologic.DO[0] = Convert.ToBoolean(Convert.ToInt32(cbDO1.Text.Trim()));
                _iologic.WriteDO();
            }
        }
    }
}
