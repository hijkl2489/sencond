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
using Infragistics.Win.UltraWinGrid.ExcelExport;

namespace YGJZJL.Bar
{
    public partial class BilletFlowCard : FrmBase
    {
        private string _STOCK = "";
        private ProduceLine _PRODUCELINE = ProduceLine.GX;
        private Post _POST = Post.Receive;

        public BilletFlowCard()
        {
            InitializeComponent();
        }

        private void BilletFlowCard_Load(object sender, EventArgs e)
        {
            CommonMethod.RefreshAndAutoSize(ultraGrid1);

            try
            {
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                string strKey = this.CustomInfo.ToUpper();

                if (!string.IsNullOrEmpty(strKey) && strKey.Length == 3)
                {
                    if (strKey.Substring(0, 2).Equals("GX"))
                    {
                        this._STOCK = "SH000100";
                        this._PRODUCELINE = ProduceLine.GX;
                    }
                    else if (strKey.Substring(0, 2).Equals("BC"))
                    {
                        this._STOCK = "SH000098";
                        this._PRODUCELINE = ProduceLine.BC;
                    }
                    else if (strKey.Substring(0, 2).Equals("XC"))
                    {
                        this._STOCK = "SH000120";                                                  //型材未知
                        this._PRODUCELINE = ProduceLine.XC;
                    }

                    if (strKey.Substring(2, 1).Equals("1"))
                    {
                        this._POST = Post.Receive;
                        ucBilletFlowCard1.SetPost_BilletReceive();
                        SetToolButtonCaption("Save", "验收登记");
                        SetToolButtonCaption("Cancel", "撤销验收");
                        SetToolButtonCaption("开始", "分析时间");
                        SetToolButtonCaption("轧制编号", "　　货架编号");
                        SetToolButtonVisible("其他库1", false);
                        SetToolButtonVisible("其他库2", false);
                        SetToolButtonVisible("其他库3", false);
                        SetToolButtonVisible("出炉结束", false);
                        SetToolButtonVisible("取消结束", false);
                        rbtnA.Text = "未验收";
                        rbtnB.Text = "已验收";
                        cbxDateTime.Checked = false;
                        lbl_Mark.Text = "此颜色表示已验收登记";
                    }
                    else if (strKey.Substring(2, 1).Equals("2"))
                    {
                        this._POST = Post.Charge;
                        ucBilletFlowCard1.SetPost_Charge();
                        SetToolButtonCaption("Save", "入炉登记");
                        SetToolButtonCaption("Cancel", "撤销入炉");
                        SetToolButtonCaption("开始", "组批时间");
                        SetToolButtonCaption("轧制编号", "　　轧制编号");
                        SetToolButtonVisible("其他库1", false);
                        SetToolButtonVisible("其他库2", false);
                        SetToolButtonVisible("其他库3", false);
                        SetToolButtonVisible("出炉结束", false);
                        SetToolButtonVisible("取消结束", false);
                        rbtnA.Text = "未入炉";
                        rbtnB.Text = "已入炉";
                        cbxDateTime.Checked = true;
                        lbl_Mark.Text = "此颜色表示已入炉登记";
                    }
                    else if (strKey.Substring(2, 1).Equals("3"))
                    {
                        this._POST = Post.DisCharge;
                        ultraExpandableGroupBox1.Visible = false;
                        SetToolButtonCaption("Save", "出炉开始");
                        SetToolButtonCaption("Cancel", "撤销开始");
                        SetToolButtonCaption("开始", "组批时间");
                        SetToolButtonCaption("轧制编号", "　　轧制编号");
                        SetToolButtonVisible("其他库1", false);
                        SetToolButtonVisible("其他库2", false);
                        SetToolButtonVisible("其他库3", false);
                        SetToolButtonVisible("出炉结束", true);
                        SetToolButtonVisible("取消结束", true);
                        rbtnA.Text = "未出炉";
                        rbtnB.Text = "已出炉";
                        rbtnC.Checked = true;
                        cbxDateTime.Checked = true;
                        lbl_Mark.Text = "此颜色表示已出炉登记";
                        lbl_InFurnance.Text = "此颜色表示正在出炉";
                        lbl_InFurnance.Visible = true;
                    }
                    else if (strKey.Substring(2, 1).Equals("4"))
                    {
                        this._POST = Post.Roll;
                        ucBilletFlowCard1.SetPost_Rolling(_PRODUCELINE);
                        SetToolButtonCaption("Save", "保存");
                        SetToolButtonCaption("Cancel", "撤销");
                        SetToolButtonCaption("开始", "组批时间");
                        SetToolButtonCaption("轧制编号", "　　轧制编号");
                        SetToolButtonVisible("其他库1", false);
                        SetToolButtonVisible("其他库2", false);
                        SetToolButtonVisible("其他库3", false);
                        SetToolButtonVisible("出炉结束", false);
                        SetToolButtonVisible("取消结束", false);
                        rbtnA.Text = "未挑废";
                        rbtnB.Text = "已挑废";
                        cbxDateTime.Checked = true;
                        lbl_Mark.Text = "此颜色表示已挑废登记";
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void SetToolButtonCaption(string strKey, string strCaption)
        {
            try
            {
                this.ultraToolbarsManager1.Tools[strKey].SharedProps.Caption = strCaption;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void SetToolButtonVisible(string strKey, bool Visible)
        {
            try
            {
                this.ultraToolbarsManager1.Tools[strKey].SharedProps.Visible = Visible;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private string GetOther1()
        {
            switch (_PRODUCELINE)
            {
                case ProduceLine.BC:
                    return "SH000100";
                case ProduceLine.XC:
                    return "SH000100";
                case ProduceLine.GX:
                    return "SH000098";
                default:
                    return "?";
            }

        }

        private string GetOther2()
        {
            switch (_PRODUCELINE)
            {
                case ProduceLine.BC:
                    return "SH000120";
                case ProduceLine.XC:
                    return "SH000098";
                case ProduceLine.GX:
                    return "SH000120";
                default:
                    return "?";
            }
        }

        private string GetOther3()
        {
            return "SH000166";
        }

        private string GetStoreName(string StoreCode)
        {
            switch (StoreCode)
            {
                case "SH000100":
                    return "高线库";
                case "SH000098":
                    return "棒材库";
                case "SH000120":
                    return "型材库";
                case "SH000166":
                    return "炼钢库";
                default:
                    return "";
            }
        }

        private void GetFlowCardInfo(string strCardNo)
        {
            string strSql = "";
            strSql += Convert.ToString("select 'false' checked,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_cardno,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_smeltdate, 'yyyy-mm-dd hh24:mi:ss') fd_smeltdate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_stoveno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_c,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_si,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_mn,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_s,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_p,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_ni,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_cr,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_cu,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_v,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_mo,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_ceq,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_as,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_ti,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_sb,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_als,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_totalcount,").Trim() + " ";
            strSql += Convert.ToString("       round(round(t.fn_gp_len *0.21, 3) * decode(nvl(t.fn_gp_checkcount, 0), 0, nvl(t.fn_gp_totalcount, 0), nvl(t.fn_gp_checkcount, 0)), 3)  fn_ll_weight,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fn_gp_checkcount, 0), 0, nvl(t.fn_gp_totalcount, 0), nvl(t.fn_gp_checkcount, 0)) fn_gp_checkcount,").Trim() + " ";
            strSql += Convert.ToString("       round(decode(nvl(t.fn_gp_totalcount, 0), 0, '', (decode(nvl(t.fn_gp_checkcount, 0), 0, nvl(t.fn_gp_totalcount, 0), nvl(t.fn_gp_checkcount, 0))*nvl(t.fn_jj_weight, 0)/nvl(t.fn_gp_totalcount,0))), 3) fn_jj_weight,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_judger,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gp_judgedate, 'yyyy-mm-dd hh24:mi:ss') fd_gp_judgedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gpys_number,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_djh,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gpys_receivedate, 'yyyy-mm-dd hh24:mi:ss') fd_gpys_receivedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gpys_receiver,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zc_enternumber,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_zc_enterdatetime, 'yyyy-mm-dd hh24:mi:ss') fd_zc_enterdatetime,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zc_operator,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zc_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_length,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_zz_date, 'yyyy-mm-dd hh24:mi:ss') fd_zz_date,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zz_operator,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zz_num,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zz_wastnum,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zz_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_flow,").Trim() + " ";
            strSql += Convert.ToString("       nvl(t.fs_discharge_begined, '0') fs_discharge_begined,").Trim() + " ";
            strSql += Convert.ToString("       nvl(t.fs_discharge_end, '0') fs_discharge_end,").Trim() + " ";
            strSql += Convert.ToString("       decode(t.fs_freezed, '1', '冻结', '自由') fs_freezed,").Trim() + " ";
            strSql += Convert.ToString("       decode(t.fs_checked, '1', '√', '') fs_checked,").Trim() + " ";
            strSql += "decode(t.FS_UNQUALIFIED,'0','√','1','×','√') FS_UNQUALIFIED ";
            strSql += Convert.ToString("  from it_fp_techcard t").Trim() + " ";
            strSql += Convert.ToString(" where nvl(t.fs_isvalid, '0') = '0'").Trim() + " ";

            if (_POST == Post.Receive)
            {
                string str = "";

                if (cbx_Other1.Checked)
                {
                    str += " t.fs_gp_flow = '" + GetOther1() + "' ";
                }

                if (cbx_Other2.Checked)
                {
                    str += (string.IsNullOrEmpty(str) ? "" : " or ") + " t.fs_gp_flow = '" + GetOther2() + "' ";
                }

                if (cbx_Other3.Checked)
                {
                    str += (string.IsNullOrEmpty(str) ? "" : " or ") + " t.fs_gp_flow = '" + GetOther3() + "' ";
                }

                if (string.IsNullOrEmpty(str))
                {
                    str += " t.fs_gp_flow = '" + _STOCK + "' ";
                }

                str = "(" + str + ")";

                strSql += " and " + str + " ";
                //strSql += Convert.ToString("   and case when t.fs_transtype = '1' or t.fs_transtype = '2' then nvl(t.fs_gp_completeflag, '0') when t.fs_transtype = '3' then '1' else '0' end = '1'").Trim() + " ";
                //strSql += Convert.ToString("   and case when t.fs_transtype = '1' or t.fs_transtype = '2' then nvl(t.fn_jj_weight, 0) when t.fs_transtype = '3' then 1 else 0 end > 0").Trim() + " ";
                strSql += Convert.ToString("   and nvl(t.fs_batched, '0') = '0' and t.fs_zc_batchno is null").Trim() + " ";

                if (rbtnA.Checked)
                {
                    strSql += Convert.ToString("   and (nvl(t.Fs_Checked, 0) = '0' or nvl(t.Fn_Gpys_Number, 0) <= 0)").Trim() + " ";
                }
                else if (rbtnB.Checked)
                {
                    strSql += Convert.ToString("   and nvl(t.Fs_Checked, 0) = '1' and nvl(t.Fn_Gpys_Number, 0) > 0").Trim() + " ";
                }
            }
            else if (_POST == Post.Charge)
            {
                strSql += Convert.ToString("   and t.fs_gp_flow = '" + _STOCK + "'").Trim() + " ";
                strSql += Convert.ToString("   and nvl(t.fs_batched, '0') = '1'").Trim() + " ";
                strSql += Convert.ToString("   and t.fs_zc_batchno is not null").Trim() + " ";
                strSql += Convert.ToString("   and nvl(t.fn_zz_num, 0) = 0").Trim() + " ";

                if (rbtnA.Checked)
                {
                    strSql += Convert.ToString("   and nvl(t.Fn_Zc_Enternumber, 0) <= 0").Trim() + " ";
                }
                else if (rbtnB.Checked)
                {
                    strSql += Convert.ToString("   and nvl(t.Fn_Zc_Enternumber, 0) > 0").Trim() + " ";
                }
            }
            else if (_POST == Post.DisCharge)
            {
                strSql += Convert.ToString("   and t.fs_gp_flow = '" + _STOCK + "'").Trim() + " ";
                strSql += Convert.ToString("   and nvl(t.fs_batched, '0') = '1'").Trim() + " ";
                strSql += Convert.ToString("   and t.fs_zc_batchno is not null").Trim() + " ";
                strSql += Convert.ToString("   and nvl(t.fn_zc_enternumber, 0) > 0").Trim() + " ";
                strSql += Convert.ToString("   and t.fs_zc_operator is not null").Trim() + " ";
                strSql += Convert.ToString("   and not exists (select 1 from dt_productplan t1 where t1.fs_batchno = t.fs_zc_batchno and nvl(t1.fs_completeflag, '0') = '1')").Trim() + " ";

                if (rbtnA.Checked)
                {
                    strSql += Convert.ToString("   and nvl(t.fs_discharge_begined, '0') = '0'").Trim() + " ";
                }
                else if (rbtnB.Checked)
                {
                    strSql += Convert.ToString("   and nvl(t.fs_discharge_end, '0') = '1'").Trim() + " ";
                }
            }
            else if (_POST == Post.Roll)
            {
                strSql += Convert.ToString("   and t.fs_gp_flow = '" + _STOCK + "'").Trim() + " ";
                strSql += Convert.ToString("   and nvl(t.fs_batched, '0') = '1'").Trim() + " ";
                strSql += Convert.ToString("   and t.fs_zc_batchno is not null").Trim() + " ";
                strSql += Convert.ToString("   and nvl(t.fn_zc_enternumber, 0) > 0").Trim() + " ";
                strSql += Convert.ToString("   and t.fs_zc_operator is not null").Trim() + " ";

                if (rbtnA.Checked)
                {
                    strSql += Convert.ToString("   and nvl(t.Fn_Zz_Num, 0) <= 0").Trim() + " ";
                }
                else if (rbtnB.Checked)
                {
                    strSql += Convert.ToString("   and nvl(t.Fn_Zz_Num, 0) > 0").Trim() + " ";
                }
            }

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                if (_POST == Post.Receive)
                {
                    strSql += Convert.ToString("   and t.fd_gp_judgedate between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
                }
                else
                {
                    strSql += Convert.ToString("   and t.fs_batch_optdate between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
                }
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
            {
                if (_POST == Post.Receive)
                {
                    strSql += Convert.ToString("   and t.fs_djh like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
                }
                else
                {
                    strSql += Convert.ToString("   and t.fs_zc_batchno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
                }
            }

            if (!string.IsNullOrEmpty(tbQueryStoveNo.Text.Trim()))
            {
                strSql += Convert.ToString("   and t.fs_gp_stoveno like '%" + tbQueryStoveNo.Text.Trim().ToUpper() + "%'").Trim() + " ";
            }

            if (_POST == Post.Receive)
            {
                strSql += Convert.ToString("   order by t.fs_gp_stoveno desc").Trim() + " ";
            }
            else
            {
                strSql += Convert.ToString("   order by t.fs_zc_batchno desc, t.fs_gp_stoveno").Trim() + " ";
            }

            string err = "";
            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable table = ds.Tables[0];
                CommonMethod.CopyDataToDatatable(ref table, ref dataTable1, true);
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("查询出错！\n" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataTable1.Rows.Clear();
            }

            CommonMethod.RefreshAndAutoSize(ultraGrid1);

            MarkupRows();

            if (dataTable1.Rows.Count == 0)
            {
                ucBilletFlowCard1.ResetData();
            }
            else
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }

            //if (string.IsNullOrEmpty(strCardNo))
                //return;

            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_CARDNO"].Value).Equals(strCardNo))
                    {
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FS_CARDNO", false);
                    }

                    if (ultraGrid1.Rows[i].Cells["FS_UNQUALIFIED"].Value.ToString() == "×")
                    {
                        ultraGrid1.Rows[i].Appearance.ForeColor = Color.Red;
                    }
                }
                catch { }
            }
        }

        private void Save()
        {
            try
            {
                if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null)
                {
                    return;
                }

                string strCardNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Value);
                string strStoveNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Value);

                string[] sArgs;
                string strProcedure = "", strErr = "";
                ArrayList obj = null;

                Hashtable htblValue = new Hashtable();

                if (_POST == Post.Receive)
                {
                    if (!ucBilletFlowCard1.DataValidation_BilletReceive(out htblValue))
                    {
                        return;
                    }

                    string StoreCode = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_GP_FLOW"].Value);
                    if (!string.IsNullOrEmpty(StoreCode) && !StoreCode.Equals(_STOCK))
                    {
                        string StoreName = GetStoreName(StoreCode);
                        string StoreCurr = GetStoreName(_STOCK);

                        if (MessageBox.Show("此炉号数据信息在【" + StoreName + "】，确认要在【" + StoreCurr + "】进行验收吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    sArgs = new string[] { strCardNo, htblValue["V2"].ToString(), htblValue["V3"].ToString(), this.UserInfo.GetUserName(), this._STOCK };
                    strProcedure = "KG_MCMS_FLOWCARD.BilletAcceptToNewStore";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }
                else if (_POST == Post.Charge)
                {
                    if (!ucBilletFlowCard1.DataValidation_Charge(out htblValue))
                    {
                        return;
                    }
                    sArgs = new string[] { strCardNo, htblValue["V2"].ToString(), htblValue["V3"].ToString(), this.UserInfo.GetUserName(), htblValue["V5"].ToString() };
                    strProcedure = "KG_MCMS_FLOWCARD.Charge";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }
                else if (_POST == Post.DisCharge)
                {
                    sArgs = new string[] { strCardNo, this.UserInfo.GetUserName() };
                    strProcedure = "KG_MCMS_FLOWCARD.DisCharge_Begin";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }
                else if (_POST == Post.Roll)
                {
                    if (!ucBilletFlowCard1.DataValidation_Rolling(_PRODUCELINE, out htblValue))
                    {
                        return;
                    }
                    sArgs = new string[] { strCardNo, htblValue["V2"].ToString(), htblValue["V3"].ToString(), 
                                            htblValue["V4"].ToString(), htblValue["V5"].ToString(), htblValue["V6"].ToString(), 
                                            this.UserInfo.GetUserName() , htblValue["V8"].ToString()};
                    strProcedure = "KG_MCMS_FLOWCARD.DisCharge";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }

               /*if (int.Parse(obj[2].ToString()) > 0)
                {
                    //加入验收后获取最终成分功能，调用甘俊的代码
                    if (_POST == Post.Receive)
                    {
                        try
                        {
                            CoreClientParam ccp = new CoreClientParam();
                            ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
                            ccp.MethodName = "updateJudgeElements";
                            ccp.ServerParams = new object[] { new string[] { strStoveNo } };
                            CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                        }
                        catch { }
                    }

                }
                else
                {
                    MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/


                this.GetFlowCardInfo(strCardNo);
                MessageBox.Show("验收成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch { }
        }

        private void Save1()
        {
            if (_POST != Post.DisCharge)
            {
                return;
            }

            if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null)
            {
                return;
            }

            string strCardNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Value);

            string[] sArgs;
            string strProcedure = "", strErr = "";
            ArrayList obj = null;

            sArgs = new string[] { strCardNo, this.UserInfo.GetUserName() };
            strProcedure = "KG_MCMS_FLOWCARD.DisCharge_End";
            obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

            if (int.Parse(obj[2].ToString()) > 0)
            {
                this.GetFlowCardInfo(strCardNo);
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancel()
        {
            try
            {
                if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null)
                {
                    return;
                }

                string strCardNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Value);

                string[] sArgs;
                string strProcedure = "", strErr = "";
                ArrayList obj = null;

                if (_POST == Post.Receive)
                {
                    sArgs = new string[] { strCardNo, this.UserInfo.GetUserName() };
                    strProcedure = "KG_MCMS_FLOWCARD.BilletAccept_Cancel";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }
                else if (_POST == Post.Charge)
                {
                    sArgs = new string[] { strCardNo, this.UserInfo.GetUserName() };
                    strProcedure = "KG_MCMS_FLOWCARD.Charge_Cancel";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }
                else if (_POST == Post.DisCharge)
                {
                    sArgs = new string[] { strCardNo, this.UserInfo.GetUserName() };
                    strProcedure = "KG_MCMS_FLOWCARD.DisCharge_Begin_Cancel";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }
                else if (_POST == Post.Roll)
                {
                    sArgs = new string[] { strCardNo, this.UserInfo.GetUserName() };
                    strProcedure = "KG_MCMS_FLOWCARD.DisCharge_Cancel";
                    obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                }

                if (int.Parse(obj[2].ToString()) > 0)
                {
                    this.GetFlowCardInfo(strCardNo);
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }

        private void Cancel1()
        {
            if (_POST != Post.DisCharge)
            {
                return;
            }

            if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null)
            {
                return;
            }

            string strCardNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Value);

            string[] sArgs;
            string strProcedure = "", strErr = "";
            ArrayList obj = null;

            sArgs = new string[] { strCardNo, this.UserInfo.GetUserName() };
            strProcedure = "KG_MCMS_FLOWCARD.DisCharge_End_Cancel";
            obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

            if (int.Parse(obj[2].ToString()) > 0)
            {
                this.GetFlowCardInfo(strCardNo);
                MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    {
                        this.GetFlowCardInfo("");
                        break;
                    }
                case "Save":
                    {
                        this.Save();
                        break;
                    }
                case "Cancel":
                    {
                        this.Cancel();
                        break;
                    }
                case "出炉结束":
                    {
                        this.Save1();
                        break;
                    }
                case "取消结束":
                    {
                        this.Cancel1();
                        break;
                    }
                case "导出":
                    {
                        CommonMethod.ExportDataWithSaveDialog2(ref this.ultraGrid1, this.Text);
                        break;
                    }
            }

        }

        private void cbxDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = dateTimePicker2.Enabled = cbxDateTime.Checked;
        }

        #region 访问服务端

        private DataSet CreateDataSet(DataTable dt)
        {
            DataSet ds = new DataSet();

            if (dt != null)
            {
                ds.Tables.Add(dt);
            }

            return ds;
        }

        private ArrayList ProcReturnDS(string ServerName, string MethodName, object[] obj, out string err)
        {
            try
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = ServerName;//服务端类名
                ccp.MethodName = MethodName;//上面类中的指定方法      
                ccp.ServerParams = obj;
                ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);//执行                
                err = "";
                return (ArrayList)ccp.ReturnObject;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }

        private DataSet SelectReturnDS(string ServerName, string MethodName, object[] obj, out string err)
        {
            try
            {
                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = ServerName;//服务端类名
                ccp.MethodName = MethodName;//上面类中的指定方法    
                ccp.ServerParams = obj;
                ccp = this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);//执行   
                DataSet ds = CreateDataSet(ccp.SourceDataTable);
                err = ccp.ReturnInfo;
                return ds;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return null;
            }
        }

        #endregion

        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                if (ultraGrid1.ActiveRow != null)
                {
                    string strCardNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_CARDNO"].Value);

                    DataRow[] Rows = dataTable1.Select("FS_CARDNO = '" + strCardNo + "'");

                    if (Rows.Length > 0)
                    {
                        ultraExpandableGroupBox1.Text = "数据编辑区域【" + strCardNo + "】";
                        DataRow row = Rows[0];
                        ucBilletFlowCard1.SetData(ref row);
                    }
                }
            }
            catch { }
        }

        private void cbx_Filter_CheckedChanged(object sender, EventArgs e)
        {
            CommonMethod.SetUltraGridRowFilter(ref ultraGrid1, cbx_Filter.Checked);
        }

        private void MarkupRows()
        {
            if (_POST == Post.Receive)
            {
                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_CHECKED"].Value).Equals("√"))
                        {
                            ultraGrid1.Rows[i].Appearance.ForeColor = Color.Blue;
                        }
                        else
                        {
                            ultraGrid1.Rows[i].Appearance.ResetForeColor();
                        }
                    }
                    catch { }
                }
            }
            else if (_POST == Post.Charge)
            {
                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_ZC_BATCHNO"].Value).Length > 0 &&
                          Convert.ToInt32(Convert.ToString(ultraGrid1.Rows[i].Cells["FN_ZC_ENTERNUMBER"].Value)) > 0)
                        {
                            ultraGrid1.Rows[i].Appearance.ForeColor = Color.Blue;
                        }
                        else
                        {
                            ultraGrid1.Rows[i].Appearance.ResetForeColor();
                        }
                    }
                    catch { }
                }
            }
            else if (_POST == Post.DisCharge)
            {
                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToString(Convert.ToString(ultraGrid1.Rows[i].Cells["FS_DISCHARGE_BEGINED"].Value)) == "1" &&
                          Convert.ToString(Convert.ToString(ultraGrid1.Rows[i].Cells["FS_DISCHARGE_END"].Value)) == "0")
                        {
                            ultraGrid1.Rows[i].Appearance.ForeColor = Color.Red;
                        }
                        else if (Convert.ToString(Convert.ToString(ultraGrid1.Rows[i].Cells["FS_DISCHARGE_BEGINED"].Value)) == "1" &&
                          Convert.ToString(Convert.ToString(ultraGrid1.Rows[i].Cells["FS_DISCHARGE_END"].Value)) == "1")
                        {
                            ultraGrid1.Rows[i].Appearance.ForeColor = Color.Blue;
                        }
                        else
                        {
                            ultraGrid1.Rows[i].Appearance.ResetForeColor();
                        }
                    }
                    catch { }
                }
            }
            else if (_POST == Post.Roll)
            {
                for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_ZC_BATCHNO"].Value).Length > 0 &&
                          Convert.ToInt32(Convert.ToString(ultraGrid1.Rows[i].Cells["FN_ZZ_NUM"].Value)) > 0)
                        {
                            ultraGrid1.Rows[i].Appearance.ForeColor = Color.Blue;
                        }
                        else
                        {
                            ultraGrid1.Rows[i].Appearance.ResetForeColor();
                        }
                    }
                    catch { }
                }
            }
        }

        private void cbx_Other1_CheckedChanged(object sender, EventArgs e)
        {
            cbx_Other1.BackColor = cbx_Other1.Checked ? Color.Red : Color.FromArgb(192, 255, 192);
        }

        private void cbx_Other2_CheckedChanged(object sender, EventArgs e)
        {
            cbx_Other2.BackColor = cbx_Other2.Checked ? Color.Red : Color.FromArgb(192, 255, 192);
        }

        private void cbx_Other3_CheckedChanged(object sender, EventArgs e)
        {
            cbx_Other3.BackColor = cbx_Other3.Checked ? Color.Red : Color.FromArgb(192, 255, 192);
        }
    }
}