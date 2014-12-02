using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using YGJZJL.PublicComponent;
using System.Globalization;

namespace YGJZJL.StrapWeight
{
    public partial class QueryAndUpdateForAdmin : FrmBase
    {

        string strWhere = "";
        string strWeightno = "";
        int rowno = -1;
        public QueryAndUpdateForAdmin()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        Query();
                    }
                    break;
                case "Update":
                    {
                        Update();
                        Query();
                    }
                    break;
            }
        }

        private void Query()
        {


            string strQuery = " select a.fs_weightno,a.fs_person,a.fs_shift, a.fs_term, to_char( a.fd_starttime,'yyyy-MM-dd hh24:mi:ss') as fd_starttime,"
            +" to_char(a.fd_endtime,'yyyy-MM-dd hh24:mi:ss') as fd_endtime ,to_char(fn_grossweight,'9999999.999') as fn_grossweight,"
            +"to_char(fn_tareweight,'99999999.999') as fn_tareweight,to_char(fn_netweight,'99999999.999') as fn_netweight,"
            +"to_char(fn_stratweiht,'99999999.999') as fn_stratweiht,to_char(fn_endweight,'99999999.999') as fn_endweight,"
            + "a.fs_pointname from dt_strapdata_forupdateview a";
            strQuery+= GetStrWhere();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strQuery };
            ccp.SourceDataTable = dataSet1.Tables[0];
            dataSet1.Tables[0].Clear();
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
              
        }

        private void QueryAndUpdateForAdmin_Load(object sender, EventArgs e)
        {
            Query();
        }

        private string  GetStrWhere()
        {
            strWhere = " where (FD_STARTTIME BETWEEN to_date('" + this.dateTimePicker1.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
              + dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss'))";

           
            return strWhere;
        }


        private void Update()
        {
            if (isvalid())
            {
                if (DialogResult.Yes == MessageBox.Show("是否确认要修改当前数据?", "修改提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    double do_startWeight = Convert.ToDouble(this.tb_startWeight.Text.Trim());
                    string str_startWeight = Math.Round(do_startWeight, 3).ToString("#0.000");

                
                    string str_startTime = dateTimePicker3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                  

                    string str_shift = cb_shift.Text;
                    string str_term = cb_term.Text;


                    string str_Update = "update DT_STRAPWEIGHT t set FN_STRATWEIHT='" + str_startWeight + "',";
                    str_Update += " FD_STARTTIME=to_date('" + str_startTime + "','yyyy-MM-dd hh24:mi:ss')";
                    if (this.tb_endWeight.Text != "")
                    {
                        double do_endWeight = Convert.ToDouble(this.tb_endWeight.Text.Trim());
                        string str_endWeight = Math.Round(do_endWeight, 3).ToString("#0.000");
                        string str_endTime = dateTimePicker4.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        double do_netWeight = Convert.ToDouble(this.tb_endWeight.Text.Trim()) - Convert.ToDouble(this.tb_startWeight.Text.Trim());
                        string str_netWeight = Math.Round(do_netWeight,3) .ToString("#0.000");

                        str_Update += " ,FN_ENDWEIGHT='" + str_endWeight + "',"+"FD_ENDTIME=to_date('" + str_endTime + "','yyyy-MM-dd hh24:mi:ss'),";
                        str_Update += " FD_PRODUCTTIME=to_date('" + str_endTime + "','yyyy-MM-dd hh24:mi:ss'),";
                        str_Update += " FS_SHIFT='" + str_shift + "',";
                        str_Update += " FS_TERM='" + str_term + "',";
                        str_Update += " FN_GROSSWEIGHT='" + str_netWeight + "',";
                        str_Update += " FN_NETWEIGHT='" + str_netWeight + "'";
                    }
                    str_Update += " where FS_WEIGHTNO='" + strWeightno + "'";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { str_Update };
                   
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    if (ccp.ReturnCode == 0)
                    {
                        MessageBox.Show("修改成功！");
                       
                    }

                }


            }
        }

        private bool isvalid()
        {
            if (rowno < 0)
            {
                MessageBox.Show("请选择需要修改的记录！");
                return false;
            }
            try
            {
                Convert.ToDouble(this.tb_startWeight.Text.Trim());
            }
            catch (Exception e1)
            {
                MessageBox.Show("请输入正确的开始重量！");
                this.tb_startWeight.Focus();
                return false;
            }
            if (ultraGrid1.Rows[rowno].Cells["FD_ENDTIME"].Text != "")
            {
                try
                {
                    Convert.ToDouble(this.tb_endWeight.Text.Trim());
                }
                catch (Exception e1)
                {
                    MessageBox.Show("请输入正确的结束重量！");
                    this.tb_endWeight.Focus();
                    return false;
                }

                if (Convert.ToDouble(this.tb_startWeight.Text.Trim()) > Convert.ToDouble(this.tb_endWeight.Text.Trim()))
                {
                    MessageBox.Show("开始重量必须小鱼结束重量！");
                    this.tb_startWeight.Focus();
                    return false;
                }

                if (dateTimePicker3.Value >= dateTimePicker4.Value)
                {
                    MessageBox.Show("开始时间必须小于结束时间！");
                    this.dateTimePicker3.Focus();
                    return false;
                }
                if (this.cb_shift.Text == "")
                {
                    MessageBox.Show("请选择正确的班次！");
                    this.cb_shift.Focus();
                    return false;
                }
                if (this.cb_term.Text == "")
                {
                    MessageBox.Show("请选择正确的班组！");
                    this.cb_term.Focus();
                    return false;
                }
            }

            return true;
        }

        private void ultraGrid1_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            rowno = ultraGrid1.ActiveRow.Index;
            if (rowno >= 0)
            {
                IFormatProvider culture = new CultureInfo("zh-CN", true);
                try
                {
                    this.tb_startWeight.Text = this.ultraGrid1.Rows[rowno].Cells["FN_STRATWEIHT"].Text;
                    this.tb_endWeight.Text = this.ultraGrid1.Rows[rowno].Cells["FN_ENDWEIGHT"].Text;
                    //DateTime dtstart=new DateTime(this.ultraGrid1.Rows[rowno].Cells["FD_STARTTIME"].Text);
                    //DateTime dtend = new DateTime(this.ultraGrid1.Rows[rowno].Cells["FD_ENDTIME"].Text);
                    this.dateTimePicker3.Value = DateTime.ParseExact(this.ultraGrid1.Rows[rowno].Cells["FD_STARTTIME"].Text, "yyyy-MM-dd HH:mm:ss", culture);
                    if (this.ultraGrid1.Rows[rowno].Cells["FD_ENDTIME"].Text != "")
                    {
                        this.dateTimePicker4.Value = DateTime.ParseExact(this.ultraGrid1.Rows[rowno].Cells["FD_ENDTIME"].Text, "yyyy-MM-dd HH:mm:ss", culture);
                    }
                    this.cb_shift.Text = this.ultraGrid1.Rows[rowno].Cells["FS_SHIFT"].Text;
                    this.cb_term.Text = this.ultraGrid1.Rows[rowno].Cells["FS_TERM"].Text;
                    strWeightno = this.ultraGrid1.Rows[rowno].Cells["FS_WEIGHTNO"].Text;
                }
                catch (Exception e4)
                { 

                }
            }
        }

        private void ultraGrid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }
    }
}
