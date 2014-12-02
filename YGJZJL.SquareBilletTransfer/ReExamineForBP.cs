using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using YGJZJL.PublicComponent;
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.SquareBilletTransfer
{
    public partial class ReExamineForBP : FrmBase
    {

        public ReExamineForBP()
        {
            InitializeComponent();
        }

        private void ReExamineForBP_Load(object sender, EventArgs e)
        {
            RefreshAndAutoSize(ultraGrid1);
            try
            {
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void GetWeightInfo()
        {
            string strWhere = "";

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strWhere += Convert.ToString("   and A.FD_GP_JUDGEDATE between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryStoveNo.Text.Trim()))
            {
                strWhere += Convert.ToString("   and A.FS_GP_STOVENO like '%" + tbQueryStoveNo.Text.Trim() + "%'").Trim() + " ";
            }

            if(cbQuanified.Checked&&!cbUnquanified.Checked)
            {
                strWhere+=" and A.FS_UNQUALIFIED='0'";
            }

            if(!cbQuanified.Checked&&cbUnquanified.Checked)
            {
                strWhere+=" and A.FS_UNQUALIFIED='1'";
            }

            //PL/SQL SPECIAL COPY
            string strSql = "SELECT A.FS_CARDNO,A.FS_GP_STOVENO,A.FS_GP_STEELTYPE,A.FS_GP_SPE,A.FN_GP_LEN AS FN_LENGTH,A.FS_ADVISESPEC,A.FN_GP_C,A.FN_GP_SI,A.FN_GP_MN,A.FN_GP_S,A.FN_GP_P,A.FN_GP_NI,A.FN_GP_CR,A.FN_GP_CU,A.FN_GP_V,A.FN_GP_AS,A.FN_GP_TI,A.FN_GP_SB,A.FN_GP_ALS,A.FS_GP_JUDGER,";
            strSql += "to_char(A.FD_GP_JUDGEDATE,'yyyy-MM-dd hh24:mi:ss') AS FD_GP_JUDGEDATE,A.FN_GP_MO,A.FN_GP_NB,A.FN_GP_CEQ,A.FN_GP_TOTALCOUNT,decode(A.FS_UNQUALIFIED,'0','√','1','×','√') FS_UNQUALIFIED1,A.FS_UNQUALIFIED FS_UNQUALIFIED2 ,decode(A.FS_GP_CHANGETYPE,'0','否','1','是','否') FS_GP_CHANGETYPE,FS_GP_STEELTYPEOLD "
            +"FROM IT_FP_TECHCARD A WHERE FS_CARDNO like 'BP10%' ";
            strSql += strWhere + " order by a.FS_CARDNO desc";

            dataTable1.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dataTable1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];

                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    if (ultraGrid1.Rows[i].Cells["FS_UNQUALIFIED2"].Value.ToString() == "1")
                    {
                        ultraGrid1.Rows[i].Appearance.ForeColor = Color.Red;
                    }
                }
            }
            this.ckAllselect.Checked = false;
            RefreshAndAutoSize(ultraGrid1);
        }


        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    {
                        this.GetWeightInfo();
                        break;
                    }
                case "GetElements":
                    {
                        GetQSElements();
                        break;
                    }
                case "Modify":
                    {
                        updateElements();
                        break;
                    }
                case "改判":
                    {
                        updateSteelType();
                        break;
                    }
            }

        }

        private void GetQSElements()
        {
            if (tbQueryStoveNo.Text.Trim() ==string.Empty)
            {
                MessageBox.Show("请输入要获取化学成分的炉号");
                return;
            }
            else
            {
                  Cursor oldCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                if (Constant.WaitingForm == null)
                {
                    Constant.WaitingForm = new WaitingForm();
                }
                Constant.WaitingForm.ShowToUser = true;
                Constant.WaitingForm.Show();
                Constant.WaitingForm.Update();
                try
                {
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
                    ccp.MethodName = "GetQSElements";
                    ccp.ServerParams = new object[] { this.tbQueryStoveNo.Text.Trim() };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    ArrayList list = (ArrayList)ccp.ReturnObject;

                    if (list == null)
                    {
                        MessageBox.Show("未能获取到该炉号的化学成分！");
                        return;
                    }
                    else
                    {
                        if (list.Count > 0)
                        {
                            tbC.Text = (list[0].ToString() == string.Empty) ? "0" : list[0].ToString();
                            tbSi.Text = (list[1].ToString() == string.Empty) ? "0" : list[1].ToString();
                            tbMn.Text = (list[2].ToString() == string.Empty) ? "0" : list[2].ToString();
                            tbS.Text = (list[3].ToString() == string.Empty) ? "0" : list[3].ToString();
                            tbP.Text = (list[4].ToString() == string.Empty) ? "0" : list[4].ToString();
                        }
                    }
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                    MessageBox.Show("QS系统信息获取失败！");
                }

                this.Cursor = Cursors.Default;
                Constant.WaitingForm.ShowToUser = false;
                Constant.WaitingForm.Close();
            }
        }

        private void updateElements()
        {
            if (!checkControls())
            {
                return;
            }



            bool flag = false;
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                flag = (ugr.Cells["FS_CHECK"].Value.ToString() == "True");
                if (!flag)
                    continue;
                else
                    break;
            }

            if (flag)
            {
                if (MessageBox.Show("确定修改所选项预报？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                   this.ckAllselect.Checked = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择要修改预报的记录！");
                return;
            }
           
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                if (ugr.Cells["FS_CHECK"].Value.ToString() == "True")
                {
                    string p_FN_GP_C = tbC.Text.Trim();
                    string p_FN_GP_SI = tbSi.Text.Trim();
                    string p_FN_GP_MN = tbMn.Text.Trim();
                    string p_FN_GP_S = tbS.Text.Trim();
                    string p_FN_GP_P = tbP.Text.Trim();
                    string p_FN_GP_AS = tbAs.Text.Trim();
                    string p_FN_GP_TI = tbTi.Text.Trim();
                    string p_FN_GP_SB = tbSb.Text.Trim();
                    string p_FN_GP_ALS = tbAls.Text.Trim();
                    string p_FS_UNQUALIFIED = "0";
                    string p_FS_CARDNO = txtCardNo.Text.Trim().Replace("'", "''");
                    string p_FS_GP_STEELTYPE = this.txtSteelType.Text.Trim().Replace("'", "''");
                    string p_FS_GP_SPE = this.txtSpec.Text.Trim().Replace("'", "''");
                    string p_FN_GP_LEN = this.txtLength.Text.Trim().Replace("'", "''");
                    string p_FS_ADVISESPEC = this.tbAdviseSpec.Text.Trim().Replace("'", "''");
                    if (this.cbEditQuanified.Checked)
                    {
                        p_FS_UNQUALIFIED = "0";
                    }
                    else
                    {
                        p_FS_UNQUALIFIED = "1";
                    }

                    string strSql = "update IT_FP_TECHCARD set FN_GP_C= '" + p_FN_GP_C + "',FN_GP_SI='" + p_FN_GP_SI + "',FN_GP_MN='" + p_FN_GP_MN + "',FN_GP_S='" + p_FN_GP_S + "',FN_GP_P='" + p_FN_GP_P + "',FN_GP_AS='" + p_FN_GP_AS + "',FN_GP_TI='" + p_FN_GP_TI + "',FN_GP_SB='" + p_FN_GP_SB + "',FN_GP_ALS='" + p_FN_GP_ALS + "',";
                    strSql += "FS_UNQUALIFIED='" + p_FS_UNQUALIFIED + "',FS_GP_STEELTYPE='" + p_FS_GP_STEELTYPE + "',";
                    strSql += "FS_GP_SPE='" + p_FS_GP_SPE + "',FN_GP_LEN='" + p_FN_GP_LEN + "',FS_ADVISESPEC='" + p_FS_ADVISESPEC + "' where FS_CARDNO='" +ugr.Cells["FS_CARDNO"].Text.ToString()+"'";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { strSql };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
            }

            GetWeightInfo();

            //string p_FN_GP_C = tbC.Text.Trim();
            //string p_FN_GP_SI = tbSi.Text.Trim();
            //string p_FN_GP_MN = tbMn.Text.Trim();
            //string p_FN_GP_S = tbS.Text.Trim();
            //string p_FN_GP_P = tbP.Text.Trim();
            //string p_FS_UNQUALIFIED = "0";
            //string p_FS_CARDNO = txtCardNo.Text.Trim().Replace("'", "''");
            //string p_FS_GP_STEELTYPE = this.txtSteelType.Text.Trim().Replace("'", "''");
            //string p_FS_GP_SPE = this.txtSpec.Text.Trim().Replace("'", "''");
            //string p_FN_GP_LEN = this.txtLength.Text.Trim().Replace("'", "''");
            //string p_FS_ADVISESPEC = this.tbAdviseSpec.Text.Trim().Replace("'", "''");
            //if (this.cbEditQuanified.Checked)
            //{
            //    p_FS_UNQUALIFIED = "0";
            //}
            //else
            //{
            //    p_FS_UNQUALIFIED = "1";
            //}
            
            //string strSql="update IT_FP_TECHCARD set FN_GP_C= '"+p_FN_GP_C+"',FN_GP_SI='"+p_FN_GP_SI+"',FN_GP_MN='"+p_FN_GP_MN+"',FN_GP_S='"+p_FN_GP_S+"',FN_GP_P='"+p_FN_GP_P+"',";
            //strSql+="FS_UNQUALIFIED='" + p_FS_UNQUALIFIED + "',FS_GP_STEELTYPE='" + p_FS_GP_STEELTYPE + "',";
            //strSql += "FS_GP_SPE='" + p_FS_GP_SPE + "',FN_GP_LEN='" + p_FN_GP_LEN + "',FS_ADVISESPEC='"+p_FS_ADVISESPEC+"' where FS_CARDNO='" + p_FS_CARDNO + "'";

            //CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteNonQuery";
            //ccp.ServerParams = new object[] { strSql };
            //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            //if (ccp.ReturnCode == 0)
            //{
            //    MessageBox.Show("数据修改成功！");
            //    GetWeightInfo();
            //}
        }


        private void updateSteelType()
        {

            if (this.txtSteelTypeOld.Text == "")
            {
                MessageBox.Show("改判时原钢种不能为空！");
                return;

            }


            bool flag = false;
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                flag = (ugr.Cells["FS_CHECK"].Value.ToString() == "True");
                if (!flag)
                    continue;
                else
                    break;
            }

            if (flag)
            {
                if (MessageBox.Show("确定对所选预报进行改判操作？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    this.ckAllselect.Checked = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择要修改预报的记录！");
                return;
            }

            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                if (ugr.Cells["FS_CHECK"].Value.ToString() == "True")
                {
               
                    string p_FS_GP_STEELTYPEOLD = this.txtSteelTypeOld.Text.Trim().Replace("'", "''");
                   
                  

                    string strSql = "update IT_FP_TECHCARD set  FS_GP_STEELTYPEOLD='" + p_FS_GP_STEELTYPEOLD + "' ,FS_GP_CHANGETYPE='1' where FS_CARDNO='" + ugr.Cells["FS_CARDNO"].Text.ToString() + "'";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { strSql };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
            }

            GetWeightInfo();

            //string p_FN_GP_C = tbC.Text.Trim();
            //string p_FN_GP_SI = tbSi.Text.Trim();
            //string p_FN_GP_MN = tbMn.Text.Trim();
            //string p_FN_GP_S = tbS.Text.Trim();
            //string p_FN_GP_P = tbP.Text.Trim();
            //string p_FS_UNQUALIFIED = "0";
            //string p_FS_CARDNO = txtCardNo.Text.Trim().Replace("'", "''");
            //string p_FS_GP_STEELTYPE = this.txtSteelType.Text.Trim().Replace("'", "''");
            //string p_FS_GP_SPE = this.txtSpec.Text.Trim().Replace("'", "''");
            //string p_FN_GP_LEN = this.txtLength.Text.Trim().Replace("'", "''");
            //string p_FS_ADVISESPEC = this.tbAdviseSpec.Text.Trim().Replace("'", "''");
            //if (this.cbEditQuanified.Checked)
            //{
            //    p_FS_UNQUALIFIED = "0";
            //}
            //else
            //{
            //    p_FS_UNQUALIFIED = "1";
            //}

            //string strSql="update IT_FP_TECHCARD set FN_GP_C= '"+p_FN_GP_C+"',FN_GP_SI='"+p_FN_GP_SI+"',FN_GP_MN='"+p_FN_GP_MN+"',FN_GP_S='"+p_FN_GP_S+"',FN_GP_P='"+p_FN_GP_P+"',";
            //strSql+="FS_UNQUALIFIED='" + p_FS_UNQUALIFIED + "',FS_GP_STEELTYPE='" + p_FS_GP_STEELTYPE + "',";
            //strSql += "FS_GP_SPE='" + p_FS_GP_SPE + "',FN_GP_LEN='" + p_FN_GP_LEN + "',FS_ADVISESPEC='"+p_FS_ADVISESPEC+"' where FS_CARDNO='" + p_FS_CARDNO + "'";

            //CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteNonQuery";
            //ccp.ServerParams = new object[] { strSql };
            //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            //if (ccp.ReturnCode == 0)
            //{
            //    MessageBox.Show("数据修改成功！");
            //    GetWeightInfo();
            //}
        }

        private bool checkControls()
        {
            if (this.txtCardNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("工艺流程卡号不能为空!");
                return false;
            }

            if (this.txtStoveNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("工艺流程卡号不能为空!");
                return false;
            }


            if (this.txtSteelType.Text.Trim() == string.Empty)
            {
                MessageBox.Show("钢种不能为空!");
                return false;
            }

            if (this.txtSpec.Text.Trim() == string.Empty)
            {
                MessageBox.Show("规格不能为空!");
                return false;
            }

            if (this.txtLength.Text.Trim() == string.Empty)
            {
                MessageBox.Show("长度不能为空!");
                return false;
            }

            if(!isCorrectFormat(txtLength.Text.Trim()))
            {
                 MessageBox.Show("长度只能为数字!");
                return false;
            }

            if (tbC.Text.Trim() == string.Empty || tbSi.Text.Trim() ==string.Empty || tbMn.Text.Trim() == string.Empty || tbS.Text.Trim() == string.Empty || tbP.Text.Trim() == string.Empty)
            {
                MessageBox.Show("化学成分不能为空!");
                return false;
            }

            //if (!isCorrectFormat(tbC.Text.Trim()) || !isCorrectFormat(tbSi.Text.Trim()) || !isCorrectFormat(tbMn.Text.Trim()) || !isCorrectFormat(tbS.Text.Trim()) || !isCorrectFormat(tbP.Text.Trim()) || !isCorrectFormat(tbNi.Text.Trim()))
            //{
            //     MessageBox.Show("化学成分格式不对!");
            //    return false;
            //}

            //if (!isCorrectFormat(tbCr.Text.Trim()) || !isCorrectFormat(tbCu.Text.Trim()) || !isCorrectFormat(tbV.Text.Trim()) || !isCorrectFormat(tbMo.Text.Trim()) || !isCorrectFormat(tbNb.Text.Trim()) || !isCorrectFormat(tbCeq.Text.Trim()) || !isCorrectFormat(tbAs.Text.Trim()))
            //{
            //    MessageBox.Show("化学成分格式不对!");
            //    return false;
            //}

            return true;
        }


        private bool isCorrectFormat(string str)
        {
            try
            {
                Convert.ToDecimal(str);
            }
            catch
            {
                return false;
            }

            return true;
        }


        private void cbxDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = dateTimePicker2.Enabled = cbxDateTime.Checked;
        }

        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow ActiveRow = ultraGrid1.ActiveRow;

                this.txtCardNo.Text = ActiveRow.Cells["FS_CARDNO"].Value.ToString();
                this.txtStoveNo.Text = ActiveRow.Cells["FS_GP_STOVENO"].Value.ToString();
                this.txtSteelType.Text = ActiveRow.Cells["FS_GP_STEELTYPE"].Value.ToString();
                this.txtSpec.Text = ActiveRow.Cells["FS_GP_SPE"].Value.ToString();
                this.txtLength.Text = ActiveRow.Cells["FN_LENGTH"].Value.ToString();

                string isQuanified = ActiveRow.Cells["FS_UNQUALIFIED2"].Value.ToString();

                if (isQuanified == "1")
                {
                    this.cbEditQuanified.Checked = false;
                }
                else
                {
                    this.cbEditQuanified.Checked = true;
                }

               tbAdviseSpec.Text = ActiveRow.Cells["FS_ADVISESPEC"].Value.ToString();
               tbC.Text = (ActiveRow.Cells["FN_GP_C"].Value.ToString() == string.Empty) ? "0" : ActiveRow.Cells["FN_GP_C"].Value.ToString();
               tbSi.Text = (ActiveRow.Cells["FN_GP_SI"].Value.ToString() == string.Empty) ? "0" : ActiveRow.Cells["FN_GP_SI"].Value.ToString();
               tbMn.Text = (ActiveRow.Cells["FN_GP_MN"].Value.ToString() == string.Empty) ? "0" : ActiveRow.Cells["FN_GP_MN"].Value.ToString();
               tbS.Text = (ActiveRow.Cells["FN_GP_S"].Value.ToString() == string.Empty) ? "0" : ActiveRow.Cells["FN_GP_S"].Value.ToString();
               tbP.Text = (ActiveRow.Cells["FN_GP_P"].Value.ToString() == string.Empty) ? "0" : ActiveRow.Cells["FN_GP_P"].Value.ToString();
            }
            catch { }
        }

        public static void RefreshAndAutoSize(Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid)
        {
            try
            {
                ultraGrid.DataBind();

                foreach (UltraGridBand band in ultraGrid.DisplayLayout.Bands)
                {
                    foreach (UltraGridColumn column in ultraGrid.DisplayLayout.Bands[band.Key].Columns)
                    {
                        column.PerformAutoResize(PerformAutoSizeType.AllRowsInBand);
                    }
                }

                ultraGrid.Refresh();
            }
            catch { }
        }

        private void ultraGrid1_ClickCell(object sender, ClickCellEventArgs e)
        {
            if (e.Cell.Column.Key == "checked")
            {
                if (!Convert.ToBoolean(e.Cell.Value))
                {
                    e.Cell.Value = "True";
                }
                else
                {
                    e.Cell.Value = "FALSE";

                }

                if (ultraGrid1.Rows.Count > 0)
                {
                    ultraGrid1.Rows[e.Cell.Row.Index].Cells[2].Activated = true;
                }
            }
        }


        private void update()
        {

            string p_FN_GP_C = tbC.Text.Trim();
            string p_FN_GP_SI = tbSi.Text.Trim();
            string p_FN_GP_MN = tbMn.Text.Trim();
            string p_FN_GP_S = tbS.Text.Trim();
            string p_FN_GP_P = tbP.Text.Trim();
            string p_FS_UNQUALIFIED = "0";
            string p_FS_CARDNO = txtCardNo.Text.Trim().Replace("'", "''");
            string p_FS_GP_STEELTYPE = this.txtSteelType.Text.Trim().Replace("'", "''");
            string p_FS_GP_SPE = this.txtSpec.Text.Trim().Replace("'", "''");
            string p_FN_GP_LEN = this.txtLength.Text.Trim().Replace("'", "''");
            string p_FS_ADVISESPEC = this.tbAdviseSpec.Text.Trim().Replace("'", "''");
            if (this.cbEditQuanified.Checked)
            {
                p_FS_UNQUALIFIED = "0";
            }
            else
            {
                p_FS_UNQUALIFIED = "1";
            }

            string strSql = "update IT_FP_TECHCARD set FN_GP_C= '" + p_FN_GP_C + "',FN_GP_SI='" + p_FN_GP_SI + "',FN_GP_MN='" + p_FN_GP_MN + "',FN_GP_S='" + p_FN_GP_S + "',FN_GP_P='" + p_FN_GP_P + "',";
            strSql += "FS_UNQUALIFIED='" + p_FS_UNQUALIFIED + "',FS_GP_STEELTYPE='" + p_FS_GP_STEELTYPE + "',";
            strSql += "FS_GP_SPE='" + p_FS_GP_SPE + "',FN_GP_LEN='" + p_FN_GP_LEN + "',FS_ADVISESPEC='" + p_FS_ADVISESPEC + "' where FS_CARDNO='" + p_FS_CARDNO + "'";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSql };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            //if (ccp.ReturnCode == 0)
            //{
                
            //    GetWeightInfo();
            //}
        }

        private void ckAllselect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {

                if (this.ckAllselect.Checked)
                {
                    ugr.Cells["FS_CHECK"].Value = "True";
                }
                else
                {
                    ugr.Cells["FS_CHECK"].Value = "False";
                }
            }
        }
    }
}