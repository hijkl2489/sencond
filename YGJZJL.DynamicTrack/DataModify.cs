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
using YGJZJL.PublicComponent;
using Infragistics.Win.UltraWinGrid;


namespace YGJZJL.DynamicTrack
{
    public partial class DataModify : FrmBase
    {
        /// <summary>
        /// 临时生产单号
        /// </summary>
        private string strWeightNoTemp = string.Empty;

        /// <summary>
        /// grid索引初始化
        /// </summary>
        private int grid2Index = -1;

        /// <summary>
        /// grid选中行标识
        /// </summary>
        bool rowSelected = false;
        string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        string strDepartMent = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
        BaseInfo objBi = null;
        public DataModify()
        {
            InitializeComponent();
        }

        private void DataQuery_Load(object sender, EventArgs e)
        {
           objBi = new BaseInfo(this.ob);
        }

        private void uToolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    QueryData();
                    break;
                case "Modify":
                    UpdateData();
                    ckYield.Checked = false;
                    break;
                case "AddRecord":
                    AddRecord();
                    break;
                case "DeleteRecord":
                    DeleteRecord();
                    break;
            }
        }

        private void QueryData()
        {
            string strBeginDate = this.dtTimeBegin.Value.ToString("yyyyMMdd000000");
            string strEndDate = this.dtTimeEnd.Value.ToString("yyyyMMdd235959");

            if (dtTimeBegin.Value > dtTimeEnd.Value)
            {
                dtTimeBegin.Value = dtTimeEnd.Value;
                MessageBox.Show("计量开始时间不能大于结束时间！");
                return;
            }

            string strStoveNo = txtStoveNo.Text.Trim().Replace("'", "''");
            ArrayList list = new ArrayList();
            dataSet1.Tables[0].Clear();
            list.Add(strBeginDate);
            list.Add(strEndDate);

            if (!cbStoveSeat1.Checked && !cbStoveSeat2.Checked && !cbStoveSeat3.Checked)
            {
                list.Add("1");
                list.Add("2");
                list.Add("3");
            }
            else
            {
                string strStoveSeat1 = string.Empty;
                string strStoveSeat2 = string.Empty;
                string strStoveSeat3 = string.Empty;

                if (cbStoveSeat1.Checked)
                {
                    strStoveSeat1 = "1";
                }

                if (cbStoveSeat2.Checked)
                {
                    strStoveSeat2 = "2";
                }

                if (cbStoveSeat3.Checked)
                {
                    strStoveSeat3 = "3";
                }

                list.Add(strStoveSeat1);
                list.Add(strStoveSeat2);
                list.Add(strStoveSeat3);
            }

            list.Add(strStoveNo);
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "query";
            ccp.ServerParams = new object[] { "DATAMODIFY_01.SELECT", list };
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            rowSelected = false;
            grid2Index = -1;
            setTextBoxEmpty();
        }

        private void AddRecord()
        {
            if (!checkItems())
            {
                return;
            }

            string strStoveSeatNo = (ultraOptionSet1.CheckedIndex + 1).ToString();
            string strStoveNo = txtStoveNo1.Text.Trim().Replace("'", "''");
            string strPotNo = txtPotNo.Text.Trim().Replace("'", "''");
            string strGrossWeight = txtGrossWeight.Text.Trim().Replace("'", "''");
            string strTearWeight = txtTearWeight.Text.Trim().Replace("'", "''");
            string strNetWeight = txtNetWeight.Text.Trim().Replace("'", "''");
            string strReceiveFac = (ultraOptionSet2.CheckedIndex + 1).ToString();
            //string strAddRecorder = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            string strAddRecorder = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "手动新增";
            string strShift = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder()).ToString();//班次

            ArrayList list = new ArrayList();
            list.Add(strStoveSeatNo);
            list.Add(strStoveNo);
            list.Add(strPotNo);
            list.Add(strGrossWeight);
            list.Add(strTearWeight);
            list.Add(strNetWeight);
            list.Add(strReceiveFac);
            list.Add(strAddRecorder);
            list.Add(strAddRecorder);
            list.Add(strShift);

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "save";
            ccp.ServerParams = new object[] { "DATAMODIFY_01.ADD", list };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("新增记录成功！");
                string strLog = "罐号=" + strPotNo + ",炉座号="//卡号、车号
            + strStoveSeatNo + ",炉号=" + strStoveNo + ",毛重="//物料、发货单位
            + strGrossWeight + ",皮重=" + strTearWeight + ",净重="//卸货地点、承运单位
            + strNetWeight + ",流向=" + strReceiveFac + ",";
                this.objBi.WriteOperationLog("DT_IRONWEIGHT", strDepartMent, strUserName, "增加", strLog, "数据修改", "动态轨道衡二次数据表", "动态轨道衡");//调用写操作日志方法

            }


            QueryData();
        }

        private void DeleteRecord()
        {
            if (!rowSelected)
            {
                MessageBox.Show("请先选择要删除的记录");
                return;
            }

            if (strWeightNoTemp == string.Empty)
            {
                return;
            }

            string strStoveSeatNo = (ultraOptionSet1.CheckedIndex + 1).ToString();
            string strStoveNo = txtStoveNo1.Text.Trim().Replace("'", "''");
            string strPotNo = txtPotNo.Text.Trim().Replace("'", "''");
            string strGrossWeight = txtGrossWeight.Text.Trim().Replace("'", "''");
            string strTearWeight = txtTearWeight.Text.Trim().Replace("'", "''");
            string strNetWeight = txtNetWeight.Text.Trim().Replace("'", "''");
            string strReceiveFac = (ultraOptionSet2.CheckedIndex + 1).ToString();

            ArrayList list = new ArrayList();
            list.Add(strWeightNoTemp);
         
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "save";
            ccp.ServerParams = new object[] { "DATAMODIFY_01.DELETE", list };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("记录删除成功！");
                string strLog = "罐号=" + strPotNo + ",炉座号="//卡号、车号
          + strStoveSeatNo + ",炉号=" + strStoveNo + ",毛重="//物料、发货单位
          + strGrossWeight + ",皮重=" + strTearWeight + ",净重="//卸货地点、承运单位
          + strNetWeight + ",流向=" + strReceiveFac + ",";
                this.objBi.WriteOperationLog("DT_IRONWEIGHT", strDepartMent, strUserName, "删除", strLog, "数据修改", "动态轨道衡二次数据表", "动态轨道衡");//调用写操作日志方法


            }

            QueryData();
        }

        private void ultraGrid1_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "XZ")
            {
                if (!Convert.ToBoolean(e.Cell.Value))
                {
                    if (rowSelected||(grid2Index==e.Cell.Row.Index))
                    {
                        ultraGrid1.Rows[e.Cell.Row.Index].Cells["XZ"].Value = "FALSE";
                        MessageBox.Show("已有一行数据被选中，请先取消");
                    }
                    else
                    {
                        rowSelected = true;
                        grid2Index = e.Cell.Row.Index;
                        strWeightNoTemp = e.Cell.Row.Cells["FS_WEIGHTNO"].Text.ToString();
                        if (e.Cell.Row.Cells["FS_STOVESEATNO"].Text.ToString() != string.Empty)
                        {
                            this.ultraOptionSet1.CheckedIndex = int.Parse(e.Cell.Row.Cells["FS_STOVESEATNO"].Text.ToString()) - 1;
                        }

                        this.txtStoveNo1.Text = e.Cell.Row.Cells["FS_STOVENO"].Text.ToString();
                        this.txtPotNo.Text = e.Cell.Row.Cells["FS_POTNO"].Text.ToString();
                        this.txtGrossWeight.Text = e.Cell.Row.Cells["FN_GROSSWEIGHT"].Text.ToString();
                        this.txtNetWeight.Text = e.Cell.Row.Cells["FN_NETWEIGHT"].Text.ToString();
                        this.txtTearWeight.Text = e.Cell.Row.Cells["FN_TAREWEIGHT"].Text.ToString();
                        this.dtpGrossTime.Value = Convert.ToDateTime(e.Cell.Row.Cells["FS_GROSSTIME"].Text.ToString());
                        this.cbShift.Text=e.Cell.Row.Cells["FN_GROSSORDER"].Text.ToString();
                        if (e.Cell.Row.Cells["FS_RECEIVEFACTORY1"].Text.ToString() == "炼钢")
                        {
                            this.ultraOptionSet2.CheckedIndex = 0;
                        }
                        else
                        {
                            this.ultraOptionSet2.CheckedIndex = 1;
                        }
                    }
                }
                else
                {
                    rowSelected = false;
                    grid2Index = -1;
                    setTextBoxEmpty();
                }
                ckYield.Checked = false;
                ultraGrid1.UpdateData();
            }
        }

        /// <summary>
        /// 清空编辑区
        /// </summary>
        private void setTextBoxEmpty()
        {
            strWeightNoTemp = string.Empty;
            this.ultraOptionSet1.CheckedIndex = -1;
            this.ultraOptionSet2.CheckedIndex = -1;
            this.txtStoveNo1.Text = string.Empty;
            this.txtPotNo.Text = string.Empty;
            this.txtGrossWeight.Text = string.Empty;
            this.txtNetWeight.Text = string.Empty;
            this.txtTearWeight.Text = string.Empty;
        }

        /// <summary>
        /// 验证输入重量格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGrossWeight_Leave(object sender, EventArgs e)
        {
            if (txtGrossWeight.Text != string.Empty)
            {
                try
                {
                    decimal.Parse(txtGrossWeight.Text);
                }
                catch
                {
                    MessageBox.Show("毛重只能为数字!");
                    txtGrossWeight.Focus();
                    return;
                }

                if (txtTearWeight.Text != string.Empty)
                {
                    try
                    {
                        decimal.Parse(txtTearWeight.Text);

                    }
                    catch
                    {
                        MessageBox.Show("皮重只能为数字!");
                        txtTearWeight.Focus();
                        return;
                    }

                    if (decimal.Parse(txtGrossWeight.Text) < decimal.Parse(txtTearWeight.Text))
                    {
                        MessageBox.Show("毛重不能比皮重小");
                        txtGrossWeight.Text = string.Empty;
                        txtGrossWeight.Focus();
                        return;
                    }
                    else
                    {
                        this.txtNetWeight.Text = (decimal.Parse(txtGrossWeight.Text) - decimal.Parse(txtTearWeight.Text)).ToString();
                    }
                }
            }


        }

        /// <summary>
        /// 验证输入重量格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTearWeight_Leave(object sender, EventArgs e)
        {
            if (txtTearWeight.Text != string.Empty)
            {
                try
                {
                    decimal.Parse(txtTearWeight.Text);

                }
                catch
                {
                    MessageBox.Show("皮重只能为数字!");
                    txtTearWeight.Focus();
                    return;
                }

                if (txtGrossWeight.Text != string.Empty)
                {
                    try
                    {
                        decimal.Parse(txtGrossWeight.Text);

                    }
                    catch
                    {
                        MessageBox.Show("毛重只能为数字!");
                        txtGrossWeight.Focus();
                        return;
                    }

                    if (decimal.Parse(txtGrossWeight.Text) < decimal.Parse(txtTearWeight.Text))
                    {
                        MessageBox.Show("皮重不能比毛重大");
                        txtTearWeight.Text = string.Empty;
                        txtTearWeight.Focus();
                        return;
                    }
                    else
                    {
                        this.txtNetWeight.Text = (decimal.Parse(txtGrossWeight.Text) - decimal.Parse(txtTearWeight.Text)).ToString();
                    }
                }
            }
        }

        private void UpdateData()
        {
            if (rowSelected == false)
            {
                MessageBox.Show("请先选择要修改的行");
                return;
            }
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                if (ugr.Cells["XZ"].Value.ToString() == "True" && ugr.Cells["FS_RECEIVEFLAG"].Value.ToString() == "1")
                {
                    MessageBox.Show("不能修改已验收的计量数据", "玉钢集中计量");
                    return;
                }
            }
            if (!checkItems())
            {
                return;
            }

            string strWeightNoLocal = strWeightNoTemp;
            string strStoveSeatNo = (ultraOptionSet1.CheckedIndex + 1).ToString();
            string strStoveNo = txtStoveNo1.Text.Trim().Replace("'", "''");
            string strPotNo = txtPotNo.Text.Trim().Replace("'", "''");
            string strGrossWeight = txtGrossWeight.Text.Trim().Replace("'", "''");
            string strTearWeight = txtTearWeight.Text.Trim().Replace("'", "''");
            string strNetWeight = txtNetWeight.Text.Trim().Replace("'", "''");
            string strReceiveFac = (ultraOptionSet2.CheckedIndex + 1).ToString();

            string strGrossWeightTime = dtpGrossTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string strShift = cbShift.Text;
            ArrayList list = new ArrayList();

            if (ckYield.Checked == false)
            {
                list.Add(strStoveSeatNo);
                list.Add(strStoveNo);
                list.Add(strPotNo);
                list.Add(strGrossWeight);
                list.Add(strTearWeight);
                list.Add(strNetWeight);
                list.Add(strReceiveFac);
                list.Add(strWeightNoTemp);

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "save";
                ccp.ServerParams = new object[] { "DATAMODIFY_01.MODIFY", list };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
            else
            {
              
                list.Add(strGrossWeightTime);
                list.Add(strShift);
                list.Add(strWeightNoTemp);
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "com.dbComm.DBComm";
                ccp.MethodName = "save";
                ccp.ServerParams = new object[] { "DATAMODIFY_02.MODIFY", list };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            }
            string strPotNoLog = this.ultraGrid1.ActiveRow.Cells["FS_POTNO"].Text;
            string strStoveSeatNoLog = this.ultraGrid1.ActiveRow.Cells["FS_STOVESEATNO"].Text;
            string strStoveNoLog = this.ultraGrid1.ActiveRow.Cells["FS_STOVENO"].Text;
            string strGrossWeightLog = this.ultraGrid1.ActiveRow.Cells["FN_GROSSWEIGHT"].Text;
            string strTearWeightLog = this.ultraGrid1.ActiveRow.Cells["FN_TAREWEIGHT"].Text;
            string strNetWeightLog = this.ultraGrid1.ActiveRow.Cells["FN_NETWEIGHT"].Text;
            string strReceiveFacLog = this.ultraGrid1.ActiveRow.Cells["FS_RECEIVEFACTORY1"].Text;


            string strLog = "罐号：" + strPotNoLog + ">" + strPotNo + ",炉座号："//卡号、车号
                + strStoveSeatNoLog + ">" + strStoveSeatNo + ",炉号：" + strStoveNoLog + ">" + strStoveNo + ",毛重："//物料、发货单位
                + strGrossWeightLog + ">" + strGrossWeight + ",皮中：" + strTearWeightLog + ">" + strTearWeight + ",净重："//卸货地点、承运单位
                + strNetWeightLog + ">" + strNetWeight + ",流向：" + strReceiveFacLog + ">" + strReceiveFac ;//流向、重量
            this.objBi.WriteOperationLog("DT_IRONWEIGHT", strDepartMent, strUserName, "修改", strLog, "数据修改", "动态轨道衡二次数据表", "动态轨道衡");//调用写操作日志方法

            QueryData();

            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                if (strWeightNoLocal == ultraGrid1.Rows[i].Cells["FS_WEIGHTNO"].Text)
                {
                    ultraGrid1.Rows[i].Selected = true;
                    break;
                }
            }
        }

        /// <summary>
        /// 非空验证
        /// </summary>
        /// <returns></returns>
        private bool checkItems()
        {
            if (ultraOptionSet1.CheckedIndex == -1)
            {
                MessageBox.Show("请先选择炉座号");
                return false;
            }

            if (this.txtStoveNo1.Text == string.Empty)
            {
                MessageBox.Show("炉号不能为空");
                return false;
            }

            if (this.txtPotNo.Text == string.Empty)
            {
                MessageBox.Show("罐号不能为空");
                return false;
            }

            if (this.txtGrossWeight.Text == string.Empty)
            {
                MessageBox.Show("皮重不能为空");
                return false;
            }

            try
            {
                decimal.Parse(txtGrossWeight.Text);

            }
            catch
            {
                MessageBox.Show("毛重只能为数字!");
                return false;
            }

            if (this.txtTearWeight.Text == string.Empty)
            {
                MessageBox.Show("皮重不能为空");
                return false;
            }

            try
            {
                decimal.Parse(txtTearWeight.Text);

            }
            catch
            {
                MessageBox.Show("皮重只能为数字!");
                return false;
            }

            if (decimal.Parse(txtGrossWeight.Text) < decimal.Parse(txtTearWeight.Text))
            {
                MessageBox.Show("皮重不能比毛重大");
                return false;
            }
            else
            {
                this.txtNetWeight.Text = (decimal.Parse(txtGrossWeight.Text) - decimal.Parse(txtTearWeight.Text)).ToString();
            }


            if (this.txtNetWeight.Text == string.Empty)
            {
                MessageBox.Show("净重不能为空");
                return false;
            }

            if (ultraOptionSet2.CheckedIndex == -1)
            {
                MessageBox.Show("请先选择流向");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataModify_KeyPress(object sender, KeyPressEventArgs e)
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


        private void ultraOptionSet1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtStoveNo1.Focus();
            }
        }

        private void txtStoveNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtPotNo.Focus();
            }
        }

        private void txtPotNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtGrossWeight.Focus();
            }
        }

        private void txtGrossWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtTearWeight.Focus();
            }
        }

        private void txtTearWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtNetWeight.Focus();
            }
        }

        private void txtNetWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.ultraOptionSet2.Focus();
            }
        }

        private void cbStoveSeat2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckYield_CheckedChanged(object sender, EventArgs e)
        {
            if (ckYield.Checked == true)
            {
                this.dtpGrossTime.Enabled = true;
                this.cbShift.Enabled = true;
            }
            else
            {
                this.dtpGrossTime.Enabled = false;
                this.cbShift.Enabled = false;
            }
        }

        //private void txtTearWeight_Enter(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.txtNetWeight.Text = (decimal.Parse(txtGrossWeight.Text) - decimal.Parse(txtTearWeight.Text)).ToString();
        //    }
        //    catch (Exception e3)
        //    {
        //        MessageBox.Show("请输入数字格式的重量!");
        //        return;
        //    }
        //}

        private void txtTearWeight_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.txtNetWeight.Text = (decimal.Parse(txtGrossWeight.Text) - decimal.Parse(txtTearWeight.Text)).ToString();
            }
            catch (Exception e3)
            {
                //MessageBox.Show("请输入数字格式的重量!");
                //return;
            }
        }

       
    }
}
