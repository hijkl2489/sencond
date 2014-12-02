using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;

namespace YGJZJL.PublicComponent
{
    public partial class UserProve : FrmBase
    {
        //public _UserProve User = new _UserProve();

        public UserProve()
        {
            InitializeComponent();
        }

        //public struct _UserProve
        //{
        //    public string strUID;
        //    public string strUMM;
        //}

        private string _strUID = "";
        /// <summary>
        /// 获取用户名
        /// </summary>
        public string stUID
        {
            get
            {
                return _strUID;
            }
            //set
            //{
            //    _strUID = value;
            //}
        }

        private string _strUMM = "";
        /// <summary>
        /// 获取用户密码
        /// </summary>
        public string stUMM
        {
            get
            {
                return _strUMM;
            }
        }

        private void btnSD_Click(object sender, EventArgs e)
        {
            if (txtYHM.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名，或者关闭！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _strUID = txtYHM.Text.Trim();

            if (txtMM.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名，或者关闭！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _strUMM = txtMM.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 回车键控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserProve_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Control c = GetNextControl(this.ActiveControl, true);
                bool ok = SelectNextControl(this.ActiveControl, true, true, true, true);
                if (ok && c != null)
                {
                    if (c is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)c).SelectAll();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox4.Focus();
                return;
            }
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入旧密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }
            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("请输入新密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }
            if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("请确认新密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox3.Focus();
                return;
            }
            if (textBox2.Text.Trim() != textBox3.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致，请重新输入新密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Focus();
                return;
            }

            string strUserMM = textBox1.Text.Trim();
            string strSql = "select a.USERWD from BT_USER a where a.USERNAME = '" + textBox4.Text.Trim() + "'";

            CoreClientParam ccpCX = new CoreClientParam();
            ccpCX.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccpCX.MethodName = "QueryUserData";
            ccpCX.ServerParams = new object[] { strSql };
            DataTable dtUser = new DataTable();
            ccpCX.SourceDataTable = dtUser;

            this.ExecuteQueryToDataTable(ccpCX, CoreInvokeType.Internal);
            if (dtUser.Rows.Count > 0)
            {
                string strUserWD = dtUser.Rows[0][0].ToString();
                if (!strUserWD.Equals(strUserMM))
                {
                    MessageBox.Show("旧密码不正确，请重新输入旧密码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();
                    return;
                }
            }

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "UpdateMMData";
            ccp.ServerParams = new object[] { textBox4.Text.Trim(), textBox2.Text.Trim() };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            txtYHM.Text = textBox4.Text.Trim();
            MessageBox.Show("密码修改成功，请用新密码登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            panel1.Visible = false;
        }

        private void btnXGMM_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            textBox4.Text = txtYHM.Text.Trim();
        }

    }
}
