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

namespace YGJZJL.DynamicTrack
{
    public partial class IronCheck : FrmBase
    {
        /// <summary>
        /// 获得用户
        /// </summary>
        String username = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName().Trim();

        /// <summary>
        /// 定义查询条件中的炉座号
        /// </summary>
        string stoveseatno;

        /// <summary>
        /// 是否选中标志
        /// </summary>
        bool flag;

        public IronCheck()
        {
            InitializeComponent();
           
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "查询":
                    {
                        ShowGridInfo();
                        break;
                    }
                case "验收确认":
                    {
                        YSUpdate();
                        break;
                    }
                case "取消验收":
                    {
                        YSCancel();
                        break;
                    }
            }
        }

        /// <summary>
        /// 查询验收数据
        /// </summary>
        private void ShowGridInfo()
        {
            TimeSpan ts = new TimeSpan(dtpbegin.Value.Hour, dtpbegin.Value.Minute, dtpbegin.Value.Second);
            dtpbegin.Value = dtpbegin.Value.Add(-ts);//默认当天的0时0分0秒为开始时间
            string beginTime = dtpbegin.Value.ToString("yyyyMMdd");
            string endTime = dtpend.Value.ToString("yyyyMMdd");
            string chboxYS;
            if (chbYS.Checked)//根据复选框判断查询是否已验收的数据
            {
                chboxYS = "1";
            }
            else
            {
                chboxYS = "0";
            }
            if (stoveseatno != "-1")
            {
                ArrayList list = new ArrayList();
                list.Add(beginTime);
                list.Add(endTime);
                //list.Add(chboxYS);
                list.Add(stoveseatno);

                dataSet1.Tables["验收信息"].Clear();
                CoreClientParam ccpData = new CoreClientParam();
                ccpData.ServerName = "com.dbComm.DBComm";
                ccpData.MethodName = "query";
                ccpData.ServerParams = new object[] { "IRONCHECK_01.SELECT", list };
                ccpData.SourceDataTable = dataSet1.Tables["验收信息"];
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);

                dataSet1.Tables["计量数据"].Clear();
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "query";
                ccp.ServerParams = new object[] { "IRONCHECK_11.SELECT", list };
                ccp.SourceDataTable = dataSet1.Tables["计量数据"];
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            else
            {
                dataSet1.Tables["计量数据"].Clear();
                ArrayList list = new ArrayList();
                list.Add(beginTime);
                list.Add(endTime);
                //list.Add(chboxYS);

                dataSet1.Tables["验收信息"].Clear();
                CoreClientParam ccpData = new CoreClientParam();
                ccpData.ServerName = "com.dbComm.DBComm";
                ccpData.MethodName = "query";
                ccpData.ServerParams = new object[] { "IRONCHECK_02.SELECT", list };
                ccpData.SourceDataTable = dataSet1.Tables["验收信息"];
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);

                ultraGrid1.UpdateData();
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow ugr in ultraGrid1.Rows)
                {
                    if (ugr.Cells["FS_RECEIVEFLAG"].Value.ToString().Equals("√"))
                    {
                        ugr.Appearance.ForeColor = Color.Blue;
                    }
                    else
                    {
                        ugr.Appearance.ForeColor = Color.Black;
                    }
                }

                dataSet1.Tables["计量数据"].Clear();
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "query";
                ccp.ServerParams = new object[] { "IRONCHECK_12.SELECT", list };
                ccp.SourceDataTable = dataSet1.Tables["计量数据"];
                this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            }
            cbstoveseat.SelectedIndex = 0;
        }


        /// <summary>
        /// 验收确认的更新操作
        /// </summary>       
        private void YSUpdate()
        {
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                flag = (ugr.Cells["CHK"].Value.ToString() == "True");
                if (flag)
                    continue;
                else
                    break;
            }

            if (flag)
            {
                if (MessageBox.Show("确定要验收全部选项？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    chball.Checked = false;
                    return;


                }
            }
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
               
                if (ugr.Cells["CHK"].Value.ToString()== "True")
                {
                    if (ugr.Cells["FS_STOVESEATNO"].Value.ToString() == "" || ugr.Cells["FS_STOVENO"].Value.ToString() == "" || ugr.Cells["FS_SENDERSTROENO"].Value.ToString() == "" || ugr.Cells["FS_RECEIVESTORE"].Value.ToString() == "")
                    {
                        MessageBox.Show("请回填相关数据，再进行验收确认!");
                        return;
                    }

                    if (ugr.Cells["FS_RECEIVEFLAG"].Value.ToString().Equals("√"))
                    {
                        MessageBox.Show("炉号:"+ugr.Cells["FS_STOVESEATNO"].Value.ToString() +"已验收");
                        return;
                    }

                    ArrayList list = new ArrayList();
                    list.Add(username);
                    list.Add(ugr.Cells["FS_STOVENO"].Value.ToString());
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "com.dbComm.DBComm";
                    ccp.MethodName = "save";
                    ccp.ServerParams = new object[] { "IRONCHECK_02.UPDATE", list };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
            }
            chball.Checked = false;
            ShowGridInfo();
        }

        /// <summary>
        /// 验收的取消
        /// </summary>
        private void YSCancel()
        {
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                flag = (ugr.Cells["CHK"].Value.ToString() == "True");
                if (flag)
                    continue;
                else
                    break;
            }

            if (flag)
            {
                if (MessageBox.Show("确定要取消全部验收选项？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    chball.Checked = false;
                    return;
                }
            }
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                if (ugr.Cells["CHK"].Value.ToString() == "True")
                {
                    ArrayList list = new ArrayList();
                    list.Add(username);
                    list.Add(ugr.Cells["FS_STOVENO"].Value.ToString());
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "com.dbComm.DBComm";
                    ccp.MethodName = "save";
                    ccp.ServerParams = new object[] { "IRONCHECK_03.UPDATE", list };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }

            }
            ShowGridInfo();

        }
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chball_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {

                if (chball.Checked)
                {
                    ugr.Cells["CHK"].Value = "True";
                }
                else
                {
                    ugr.Cells["CHK"].Value = "False";
                }
            }
        }
        /// <summary>
        /// 单选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ultraGrid1_CellChange(object sender, CellEventArgs e)
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
        private void QueryLZH()
        {
            dataSet1.Tables["炉座信息"].Clear();
            CoreClientParam ccpData1 = new CoreClientParam();
            ccpData1.ServerName = "com.dbComm.DBComm";
            ccpData1.MethodName = "query";
            ccpData1.ServerParams = new object[] { "DATAFILL_02.SELECT", new ArrayList() };
            ccpData1.SourceDataTable = dataSet1.Tables["炉座信息"];
            this.ExecuteQueryToDataTable(ccpData1, CoreInvokeType.Internal);

            DataRow row;
            row = dataSet1.Tables["炉座信息"].NewRow();
            row["FS_STOVESEATNO"] = "-1";
            row["FS_STOVESEATNAME"] = "--全部-- ";
            dataSet1.Tables["炉座信息"].Rows.InsertAt(row, 0);


            this.cbstoveseat.DisplayMember = "FS_STOVESEATNAME";
            this.cbstoveseat.ValueMember = "FS_STOVESEATNO";
            cbstoveseat.DataSource = dataSet1.Tables["炉座信息"];



        }

        private void IronCheck_Load(object sender, EventArgs e)
        {
            QueryLZH();
        }

        private void cbstoveseat_SelectedIndexChanged(object sender, EventArgs e)
        {
             stoveseatno = cbstoveseat.SelectedValue.ToString();
        }


    }
}
