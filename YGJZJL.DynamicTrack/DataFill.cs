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
using Infragistics.Win.UltraWinGrid;
using System.Text.RegularExpressions;

namespace YGJZJL.DynamicTrack
{
    public partial class DataFill : FrmBase
    {
        /// <summary>
        /// 查询条件中的炉座号
        /// </summary>
        string stoveno;

        /// <summary>
        /// 查询条件中的收货单位
        /// </summary>
        string QuerySH;

        /// <summary>
        /// 编辑区的炉座号
        /// </summary>
        string strLZH;

        /// <summary>
        /// 编辑区的发货单位
        /// </summary>
        string strFH;

        /// <summary>
        /// 编辑区的收货单位
        /// </summary>
        string strSH;

        /// <summary>
        /// 定义是否选中标志
        /// </summary>
        bool flag;

        public DataFill()
        {
            InitializeComponent();
        }

        private void uToolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "find":
                    {
                        if (!getStrWhere())
                            break;
                        showGridInfo();
                        break;
                    }
                case "数据回填":
                    {
                        DFUpdate();
                        break;
                    }
            }
        }

       
       /// <summary>
        /// 数据回填更新操作
       /// </summary>
 
        private void DFUpdate()
        {

            string strLH = txtLH.Text.Trim();
            if (strLZH == "-1" || strLH.Equals("") || strFH == "--请选择--" || strSH == "--请选择--")
            {

                MessageBox.Show("在数据回填之前，请填写炉座号、炉号和收发单位信息！");
                return;
            }
            Regex reg = new Regex(@"\s{2,}");
            strLH = reg.Replace(strLH, "");
            
            if (strLH.Length > 20)
            {
                MessageBox.Show("炉号长度不能大于20个字符！");
                txtLH.Focus();
                return;
            }

            foreach (UltraGridRow ugr in uGridData.Rows)
            {
                flag = (ugr.Cells["CHK"].Value.ToString() == "True");
                if (flag)
                    continue;
                else
                    break;
            }
          
            if (flag)
            {
                if (MessageBox.Show("确定要回填全部选项？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    chBall.Checked = false;
                    return;
                }
            }
            foreach (UltraGridRow ugr in uGridData.Rows)
            {
                if (Convert.ToBoolean(ugr.Cells["CHK"].Value))
                {
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "com.dbComm.DBComm";
                    ccp.MethodName = "save";
                    ArrayList list = new ArrayList();
                    list.Add(strLZH);
                    list.Add(strLH);
                    list.Add(strFH);
                    list.Add(strSH);
                    list.Add(ugr.Cells["FS_WEIGHTNO"].Value.ToString());
                    ccp.ServerParams = new object[] { "DATAFILL_01.UPDATE", list };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);                  
                }
            }
            chBall.Checked = false;
             cbEdit.SelectedIndex = 0;
             txtLH.Text = "";
             cbFH.SelectedItem = "--请选择--";
             cbSH.SelectedItem = "--请选择--";
             showGridInfo();          
        }

        private bool getStrWhere()
        {
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                MessageBox.Show("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }

            return true;
        }
      
        /// <summary>
        /// 根据条件查询需要回填的数据
        /// </summary>
        private void showGridInfo()
        {
            TimeSpan ts = new TimeSpan(qDteBegin.Value.Hour, qDteBegin.Value.Minute, qDteBegin.Value.Second);
            qDteBegin.Value = qDteBegin.Value.Add(-ts);//默认当天的0时0分0秒为开始时间
            string begintime = qDteBegin.Value.ToString("yyyyMMddHHmmss");
            string endtime = qDteEnd.Value.ToString("yyyyMMddHHmmss");
            dataSet1.Tables["计量数据"].Clear();
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "com.dbComm.DBComm";
            ccpData.MethodName = "query";
            ArrayList list = new ArrayList();
            if (stoveno != "-1"&&QuerySH=="--全部--")
            {

                list.Add(begintime);
                list.Add(endtime);
                list.Add(begintime);
                list.Add(endtime);
                list.Add(stoveno);
                ccpData.ServerParams = new object[] { "DATAFILL_01_01.SELECT", list };
                ccpData.SourceDataTable = dataSet1.Tables["计量数据"];
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            else if (stoveno == "-1" && QuerySH != "--全部--")
            {
                list.Add(begintime);
                list.Add(endtime);
                list.Add(begintime);
                list.Add(endtime);
                list.Add(QuerySH);
                ccpData.ServerParams = new object[] { "DATAFILL_01_02.SELECT", list };
                ccpData.SourceDataTable = dataSet1.Tables["计量数据"];
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            else if (stoveno != "-1" && QuerySH != "--全部--")
            {
                list.Add(begintime);
                list.Add(endtime);
                list.Add(begintime);
                list.Add(endtime);
                list.Add(stoveno);
                list.Add(QuerySH);
                ccpData.ServerParams = new object[] { "DATAFILL_01_03.SELECT", list };
                ccpData.SourceDataTable = dataSet1.Tables["计量数据"];
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            else
            {
                list.Add(begintime);
                list.Add(endtime);
                list.Add(begintime);
                list.Add(endtime);
                ccpData.ServerParams = new object[] { "DATAFILL_01.SELECT", list };
                ccpData.SourceDataTable = dataSet1.Tables["计量数据"];
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            cbquery.SelectedIndex = 0;
            cbQuerySH.SelectedItem = "--全部--";
            foreach (DataRow dr in dataSet1.Tables["计量数据"].Rows)
            {
                if (dr["FS_RECEIVEFLAG"].ToString() == "0")
                {
                    dr["FS_RECEIVEFLAG"] = "未验收";
                }
                if (dr["FS_RECEIVEFLAG"].ToString() == "1")
                {
                    dr["FS_RECEIVEFLAG"] = "已验收";
                }
            }

        }

       
        private void chBall_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UltraGridRow ugr in uGridData.Rows)
            {

                if (chBall.Checked)
                {
                    ugr.Cells["CHK"].Value = "True";
                }
                else
                {
                    ugr.Cells["CHK"].Value = "False";
                }
            }
        }

        private void uGridData_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "CHK")
            {
                if (!Convert.ToBoolean(e.Cell.Value))
                {
                    e.Cell.Value = "True";

                }
                else
                {
                    e.Cell.Value = "FALSE";
                    
                }
              
            }
        }
        /// <summary>
        /// 下拉框绑定查询到值
        /// </summary>
        private void QueryStoveSeat()
        {
            dataSet1.Tables["炉座信息（查询）"].Clear();
            CoreClientParam ccpData1 = new CoreClientParam();
            ccpData1.ServerName = "com.dbComm.DBComm";
            ccpData1.MethodName = "query";
            ccpData1.ServerParams = new object[] { "DATAFILL_02.SELECT", new ArrayList() };
            ccpData1.SourceDataTable = dataSet1.Tables["炉座信息（查询）"];
            this.ExecuteQueryToDataTable(ccpData1, CoreInvokeType.Internal);

            dataSet1.Tables["炉座信息（编辑）"].Clear();
            CoreClientParam ccpData2 = new CoreClientParam();
            ccpData2.ServerName = "com.dbComm.DBComm";
            ccpData2.MethodName = "query";
            ccpData2.ServerParams = new object[] { "DATAFILL_02.SELECT", new ArrayList() };
            ccpData2.SourceDataTable = dataSet1.Tables["炉座信息（编辑）"];
            this.ExecuteQueryToDataTable(ccpData2, CoreInvokeType.Internal);




            DataRow row1;
            row1 = dataSet1.Tables["炉座信息（查询）"].NewRow();
            row1["FS_STOVESEATNO"] = "-1";
            row1["FS_STOVESEATNAME"] = "--全部-- ";
            dataSet1.Tables["炉座信息（查询）"].Rows.InsertAt(row1, 0);


            DataRow row2;
            row2 = dataSet1.Tables["炉座信息（编辑）"].NewRow();
            row2["FS_STOVESEATNO"] = "-1";
            row2["FS_STOVESEATNAME"] = "--全部-- ";
            dataSet1.Tables["炉座信息（编辑）"].Rows.InsertAt(row2, 0);



            this.cbquery.DisplayMember = "FS_STOVESEATNAME";
            this.cbquery.ValueMember = "FS_STOVESEATNO";
            cbquery.DataSource = dataSet1.Tables["炉座信息（查询）"];
            

            this.cbEdit.DisplayMember = "FS_STOVESEATNAME";
            this.cbEdit.ValueMember = "FS_STOVESEATNO";
            cbEdit.DataSource = dataSet1.Tables["炉座信息（编辑）"];
        } 

        private void DataFill_Load(object sender, EventArgs e)
        {
            QueryStoveSeat();
            cbQuerySH.SelectedItem = "--全部--";
            cbFH.SelectedItem = "--请选择--";
            cbSH.SelectedItem = "--请选择--";
        }
      
        private void cbquery_SelectedIndexChanged(object sender, EventArgs e)
        {
             stoveno = cbquery.SelectedValue.ToString();
        }

        private void cbEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
             strLZH = cbEdit.SelectedValue.ToString();
        }

        private void cbFH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFH.SelectedItem.ToString() == "1#高炉")
            { strFH = "1"; }
            if (cbFH.SelectedItem.ToString() == "2#高炉")
            { strFH = "2"; }
            if (cbFH.SelectedItem.ToString() == "3#高炉")
            { strFH = "3"; }
            
        }

        private void cbSH_SelectedIndexChanged(object sender, EventArgs e)
        {
            strSH = cbSH.SelectedItem.ToString();
        }

        private void cbQuerySH_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuerySH = cbQuerySH.SelectedItem.ToString();
        }

     
        

    }
}
