using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;

namespace YGJZJL.BoardBand
{
    public partial class ChangeShift : FrmBase
    {
        public string strreturnname = "";
        public string strreturnshife = "";
        public string strreturnterm = "";
        public string strreturndate = "";

        public string _args = "";

        public ChangeShift(OpeBase op,string args)
        {
            InitializeComponent();
            this.ob = op;
            _args = args;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.tbName.Text.Trim() == "")
            {
                MessageBox.Show("请输入姓名！");
                return;
            }
            if (this.cbShift.Text.Trim() == "")
            {
                MessageBox.Show("请选择班次！");
                return;
            }
            if (this.cbTerm.Text.Trim() == "")
            {
                MessageBox.Show("请选择班别！");
                return;
            }
            strreturnname = this.tbName.Text.Trim();
            strreturnshife = this.cbShift.Value.ToString();
            strreturnterm = this.cbTerm.Value.ToString();
            strreturndate = this.dateTimePicker1.Text.Trim().ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ChangeShift_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ArrayList list = new ArrayList();
            list.Add(_args);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "BILLETINFO_22.SELECT", list};
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tbName.Items.Add(dt.Rows[i]["USERID"].ToString(),dt.Rows[i]["USERNAME"].ToString());
                }
            }
        }
    }
}
