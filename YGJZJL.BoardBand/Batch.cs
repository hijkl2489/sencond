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

namespace YGJZJL.BoardBand
{
    public partial class Batch : FrmBase
    {
        private enum QueryOpportunity
        {
            UI,
            AfterOpt
        }

        private string _STOCK = "";
        private ProduceLine _PRODUCELINE = ProduceLine.GX;
        private CodeType_XC _CODETYPE = CodeType_XC.DEFAULT;

        public Batch()
        {
            InitializeComponent();
        }

        private void Batch_Load(object sender, EventArgs e)
        {
            CommonMethod.RefreshAndAutoSize(ultraGrid1);
            CommonMethod.RefreshAndAutoSize(ultraGrid2);

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

                if (!string.IsNullOrEmpty(strKey))
                {
                    if (strKey.Substring(0, 3).Equals("ZKD"))
                    {
                        this._STOCK = "SH000100";
                        this._PRODUCELINE = ProduceLine.ZKD;
                        CodeType.Visible = false;
                    }
                    else if (strKey.Substring(0, 2).Equals("BC"))
                    {
                        this._STOCK = "SH000098";
                        this._PRODUCELINE = ProduceLine.BC;
                        CodeType.Visible = false;
                    }
                    else if (strKey.Substring(0, 2).Equals("XC"))
                    {
                        this._STOCK = "SH000120";                                                  //型材未知
                        this._PRODUCELINE = ProduceLine.XC;
                        CodeType.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            this.GetBatchInfo(QueryOpportunity.UI);
            this.GetStockInfo(QueryOpportunity.AfterOpt);
        }

        private bool GetLastBatchInfo(out string BatchNo, out string OrderNo, out string Spec, out string Length)
        {
            BatchNo = "";
            OrderNo = "";
            Spec = "";
            Length = "";

            string CodeStart = "";

            switch (_PRODUCELINE)
            {
                case ProduceLine.BC:
                    CodeStart = "N";
                    break;
                case ProduceLine.XC:
                    CodeStart = "X";
                    break;
                case ProduceLine.GX:
                    CodeStart = "G";
                    break;
                case ProduceLine.ZKD:
                    CodeStart = "Z";
                    break;
                default:
                    CodeStart = "N";
                    break;
            }

            if (_PRODUCELINE == ProduceLine.XC)
            {
                if (_CODETYPE == CodeType_XC.BC)
                    CodeStart = "P";
            }

            string strSql = "";
            strSql += Convert.ToString("select fs_zc_batchno, fs_zc_orderno, fn_zz_spec, fn_length").Trim() + " ";
            strSql += Convert.ToString("  from (select row_number() over(order by fs_gp_stoveno) xh,").Trim() + " ";
            strSql += Convert.ToString("               fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("               fs_zc_orderno,").Trim() + " ";
            strSql += Convert.ToString("               fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("               fn_length").Trim() + " ";
            strSql += Convert.ToString("          from it_fp_techcard").Trim() + " ";
            strSql += Convert.ToString("         where fs_gp_flow = '" + _STOCK + "' and fs_zc_batchno =").Trim() + " ";
            strSql += Convert.ToString("               (select max(Fs_Zc_Batchno)").Trim() + " ";
            strSql += Convert.ToString("                  from it_fp_techcard t").Trim() + " ";
            strSql += Convert.ToString("                 where fs_gp_flow = '" + _STOCK + "'").Trim() + " ";
            strSql += Convert.ToString("                   and fs_batched = '1'").Trim() + " ";
            strSql += Convert.ToString("                   and fs_zc_batchno is not null").Trim() + " ";
            strSql += Convert.ToString("                   and fs_zc_batchno like '" + CodeStart + "%'))").Trim() + " ";
            strSql += Convert.ToString(" where xh = 1").Trim();

            string err = "";
            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 1)
            {
                BatchNo = Convert.ToString(ds.Tables[0].Rows[0]["FS_ZC_BATCHNO"]);
                OrderNo = Convert.ToString(ds.Tables[0].Rows[0]["FS_ZC_ORDERNO"]);
                Spec = Convert.ToString(ds.Tables[0].Rows[0]["FN_ZZ_SPEC"]);
                Length = Convert.ToString(ds.Tables[0].Rows[0]["FN_LENGTH"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetBatchInfo(QueryOpportunity Opp)
        {
            //PL/SQL SPECIAL COPY
            string strSql = "";
            strSql += Convert.ToString("select distinct t.fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fn_zz_spec) over(partition by t.fs_zc_batchno) fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fn_length) over(partition by t.fs_zc_batchno) fn_length,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fs_gp_steeltype) over(partition by t.fs_zc_batchno) fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fs_zc_orderno) over(partition by t.fs_zc_batchno) fs_zc_orderno,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fs_gp_spe) over(partition by t.fs_zc_batchno) fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fn_gp_len) over(partition by t.fs_zc_batchno) fn_gp_len,").Trim() + " ";
            strSql += Convert.ToString("                sum(nvl(t.fn_billet_count, 0)) over(partition by t.fs_zc_batchno) fn_billet_count,").Trim() + " ";
            strSql += Convert.ToString("                sum(nvl(t.fn_billet_weight, 0)) over(partition by t.fs_zc_batchno) fn_billet_weight,").Trim() + " ";
            strSql += Convert.ToString("                sum(nvl(t.fn_billet_weight_ll, 0)) over(partition by t.fs_zc_batchno) fn_billet_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fs_batch_optor) over(partition by t.fs_zc_batchno) fs_batch_optor,").Trim() + " ";
            strSql += Convert.ToString("                max(t.fs_batch_optdate) over(partition by t.fs_zc_batchno) fs_batch_optdate").Trim() + " ";
            strSql += Convert.ToString("  from (select t.fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("               t.fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("               round(t.fn_length, 3) fn_length,").Trim() + " ";
            strSql += Convert.ToString("               t.fn_gpys_number fn_billet_count,").Trim() + " ";
            strSql += Convert.ToString("               t.fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("               t.fs_zc_orderno,").Trim() + " ";
            strSql += Convert.ToString("               t.fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("               round(t.fn_gp_len, 3) fn_gp_len,").Trim() + " ";
            strSql += Convert.ToString("               t.fs_batch_optor,").Trim() + " ";
            strSql += Convert.ToString("               to_char(t.fs_batch_optdate, 'yyyy-MM-dd HH24:mi:ss') fs_batch_optdate,").Trim() + " ";
            strSql += Convert.ToString("               round(nvl(a.FN_WEIGHT,0), 3) fn_billet_weight,").Trim() + " ";
            strSql += Convert.ToString("               round(nvl(fn_gpys_number, 0) * round(0.21 * nvl(t.fn_gp_len, 0), 3), 3) fn_billet_weight_ll").Trim() + " ";
            strSql += Convert.ToString("          from it_fp_techcard t,dt_boardweightmain a ").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batched = '1'").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_gp_stoveno=a.fs_stoveno(+)").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_zc_batchno is not null").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_gp_flow = '" + this._STOCK + "'").Trim() + " ";
            strSql += Convert.ToString("           and nvl(t.fs_isvalid, '0') = '0'").Trim() + " ";

            if (Opp == QueryOpportunity.UI)
            {
                if (cbxDateTime.Checked)
                {
                    string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                    string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                    strSql += Convert.ToString("   and t.fs_batch_optdate between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
                }

                if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
                {
                    strSql += Convert.ToString("   and t.fs_zc_batchno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
                }

                if (!string.IsNullOrEmpty(tbQueryStoveNo.Text.Trim()))
                {
                    strSql += Convert.ToString("   and exists (select 1 from (select x.fs_zc_batchno from it_fp_techcard x where x.fs_batched = '1' and x.fs_gp_stoveno like '%" + tbQueryStoveNo.Text.Trim() + "%') x where x.fs_zc_batchno = t.fs_zc_batchno)").Trim() + " ";
                }
            }
            else
            {
                strSql += Convert.ToString("   and t.fs_batch_optdate > (sysdate - 1)").Trim() + " ";
            }

            strSql += Convert.ToString(" ) t order by FS_ZC_BATCHNO desc").Trim();

            string err = "";
            DataTable tbMain = null, tbDetail = null;

            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tbMain = ds.Tables[0];
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("查询出错！\n" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                dataTable3.Rows.Clear();
                dataTable2.Rows.Clear();
                return;
            }

            strSql = "";
            strSql += Convert.ToString("select t.fs_gp_stoveno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_gp_len, 3) fn_gp_len,").Trim() + " ";
            strSql += Convert.ToString("       '1' fn_billet_count,").Trim() + " ";
            strSql += Convert.ToString("       round(nvl(a.FN_WEIGHT,0), 3) fn_billet_weight,").Trim() + " ";
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
            strSql += Convert.ToString("       t.fs_gp_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_judger,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gp_judgedate, 'yyyy-mm-dd hh24:mi:ss') fd_gp_judgedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_cardno").Trim() + " ";
            strSql += Convert.ToString("  from it_fp_techcard t,dt_boardweightmain a ").Trim() + " ";
            strSql += Convert.ToString(" where t.fs_batched = '1'").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_gp_stoveno=a.fs_stoveno(+)").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_zc_batchno is not null").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_gp_flow = '" + this._STOCK + "'").Trim() + " ";
            strSql += Convert.ToString("   and nvl(t.fs_isvalid, '0') = '0'").Trim() + " ";

            if (Opp == QueryOpportunity.UI)
            {
                if (cbxDateTime.Checked)
                {
                    string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                    string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                    strSql += Convert.ToString("   and t.fs_batch_optdate between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
                }

                if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
                {
                    strSql += Convert.ToString("   and t.fs_zc_batchno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
                }

                if (!string.IsNullOrEmpty(tbQueryStoveNo.Text.Trim()))
                {
                    strSql += Convert.ToString("   and exists (select 1 from (select x.fs_zc_batchno from it_fp_techcard x where x.fs_batched = '1' and x.fs_gp_stoveno like '%" + tbQueryStoveNo.Text.Trim() + "%') x where x.fs_zc_batchno = t.fs_zc_batchno)").Trim() + " ";
                }
            }
            else
            {
                strSql += Convert.ToString("   and t.fs_batch_optdate > (sysdate - 1)").Trim() + " ";
            }

            err = "";

            ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tbDetail = ds.Tables[0];

                ArrayList alistCnst1 = new ArrayList();
                if (dataTable3.Constraints.Count > 0)
                {
                    foreach (Constraint cnst in dataTable3.Constraints)
                    {
                        alistCnst1.Add(cnst);
                    }

                    dataTable3.Constraints.Clear();
                }

                CommonMethod.CopyDataToDatatable(ref tbDetail, ref dataTable3, true);
                CommonMethod.CopyDataToDatatable(ref tbMain, ref dataTable2, true);

                for (int i = 0; i < alistCnst1.Count; i++)
                {
                    dataTable3.Constraints.Add((Constraint)alistCnst1[i]);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("查询出错！\n" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            CommonMethod.RefreshAndAutoSize(ultraGrid1);

            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }
        }

        private void GetStockInfo(QueryOpportunity Opp)
        {
            //PL/SQL Special Copy
            string strSql = "";
            strSql += Convert.ToString("select 'false' checked,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_stoveno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_gp_len, 3) fn_gp_len,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_billet_count,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_billet_weight, 3) fn_billet_weight,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_billet_count * round(0.21 * t.fn_gp_len, 3), 3) fn_billet_weight_ll,").Trim() + " ";
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
            strSql += Convert.ToString("       t.fs_gp_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_judger,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gp_judgedate, 'yyyy-MM-dd hh24:mi:ss') fd_gp_judgedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_cardno,").Trim() + " ";
            strSql += Convert.ToString("       '1' as fn_gpys_number,").Trim() + " ";
            strSql += Convert.ToString("       nvl(t.fs_unqualified, '0') fs_unqualified,").Trim() + " ";
            strSql += Convert.ToString("       round(nvl(x.FN_WEIGHT,0), 3) fn_gpys_weight,").Trim() + " ";
            strSql += Convert.ToString("        round(nvl(t.fn_gpys_number, 0) * round(0.21 * t.fn_gp_len, 3), 3) fn_gpys_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("       decode(t.fs_checked, '1', '√', '') fs_checked").Trim() + " ";
            strSql += Convert.ToString("  from it_fp_techcard t,dt_boardweightmain x ").Trim() + " ";
            strSql += Convert.ToString(" where nvl(t.fs_batched, '0') = '0' and t.fs_gp_stoveno=x.fs_stoveno(+) ").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_zc_batchno is null").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_gp_flow = 'SH000100'").Trim() + " ";
            //strSql += Convert.ToString("   and nvl(t.fs_isvalid, '0') = '0'").Trim() + " ";
            //strSql += Convert.ToString("   and t.fs_checked = '1'").Trim() + " ";
            //strSql += Convert.ToString("   and nvl(t.fn_gpys_number, 0) > 0").Trim() + " ";

            if (Opp == QueryOpportunity.UI)
            {
                if (cbxDateTime.Checked)
                {
                    string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                    string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                    strSql += Convert.ToString("   and t.fd_gp_judgedate between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
                }

                if (!string.IsNullOrEmpty(tbQueryButtressNo.Text.Trim()))
                {
                    strSql += Convert.ToString("   and t.fs_djh like '%" + tbQueryButtressNo.Text.Trim() + "%'").Trim() + " ";
                }

                if (!string.IsNullOrEmpty(tbQueryStoveNo.Text.Trim()))
                {
                    strSql += Convert.ToString("   and t.fs_gp_stoveno like '%" + tbQueryStoveNo.Text.Trim() + "%'").Trim() + " ";
                }
            }

            strSql += Convert.ToString(" order by t.fs_gp_stoveno").Trim();

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

            this.MarkupRows();

            CommonMethod.RefreshAndAutoSize(ultraGrid2);

            this.BatchCalc();
        }

        private bool DataValidation_Save(out ArrayList alistCardNo, out ArrayList alistCount, out  ArrayList alistWeight)
        {
            alistCardNo = new ArrayList();
            alistCount = new ArrayList();
            alistWeight = new ArrayList();

            ultraGrid2.UpdateData();

            try
            {
                UltraGridRow row = null;
                string strCardNo = "", strCount = "", strWeight = "";

                for (int i = 0; i < this.ultraGrid2.Rows.Count; i++)
                {
                    row = this.ultraGrid2.Rows[i];

                    if (Convert.ToBoolean(row.Cells["CHECKED"].Value))
                    {
                        strCardNo = Convert.ToString(row.Cells["FS_CARDNO"].Value).Trim();
                        strCount = Convert.ToString(row.Cells["FN_BILLET_COUNT"].Value).Trim();
                        strWeight = Convert.ToString(row.Cells["FN_BILLET_WEIGHT"].Value).Trim();

                        if (string.IsNullOrEmpty(strCount))
                        {
                            MessageBox.Show("请输入组批条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid2, i, "FN_BILLET_COUNT", true);
                            return false;
                        }

                        int iCount = 0;
                        bool bOK = false;

                        bOK = int.TryParse(strCount, out iCount);

                        if (!bOK)
                        {
                            MessageBox.Show("组批条数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid2, i, "FN_BILLET_COUNT", true);
                            return false;
                        }

                        if (iCount <= 0)
                        {
                            MessageBox.Show("组批条数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid2, i, "FN_BILLET_COUNT", true);
                            return false;
                        }

                        //if (string.IsNullOrEmpty(strWeight))
                        //{
                        //    MessageBox.Show("请输入组批重量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        //    CommonMethod.SetUltraGridActiveCell(ref ultraGrid2, i, "FN_BILLET_WEIGHT", true);
                        //    return false;
                        //}

                        decimal dWeight = 0.0M;
                        bOK = decimal.TryParse(strWeight, out dWeight);

                        if (!bOK)
                        {
                            MessageBox.Show("组批重量必须是数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid2, i, "FN_BILLET_WEIGHT", true);
                            return false;
                        }

                        if (dWeight < 0)
                        {
                            MessageBox.Show("组批重量必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid2, i, "FN_BILLET_WEIGHT", true);
                            return false;
                        }

                        alistCardNo.Add(strCardNo);
                        alistCount.Add(strCount);
                        alistWeight.Add(strWeight);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据验证失败！\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }

        private bool DataValidation_Spec(out string strBatchNo, out string strOrderNo, out string strSpec, out string strLength)
        {
            bool Success = false;

            strBatchNo = "";
            strOrderNo = "";
            strSpec = "";
            strLength = "";

            if (GetLastBatchInfo(out strBatchNo, out strOrderNo, out strSpec, out strLength))
            {
                Success = true;
            }

            frmSetSpec frm = new frmSetSpec(_PRODUCELINE, _CODETYPE, Success, strBatchNo, strOrderNo, strSpec, strLength);
            frm.Location = CommonMethod.SetChildWindowLocation(frm.Size);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                strBatchNo = frm.BATCHNO;
                strOrderNo = frm.ORDERNO;
                strSpec = frm.SPEC;
                strLength = frm.LEN;
                return true;
            }

            return false;
        }

        private bool Validation_UnQualified(ArrayList alistCardNo)
        {
            if (alistCardNo == null || alistCardNo.Count == 0)
            {
                return true;
            }

            string str = "";

            for (int i = 0; i < alistCardNo.Count; i++)
            {
                str += (string.IsNullOrEmpty(str) ? "" : " union ") + "select '" + alistCardNo[i].ToString() + "' fs_cardno from dual";
            }

            string err = "";
            string strSql = "select t.fs_gp_stoveno from it_fp_techcard t where nvl(t.fs_unqualified, '0') = '1' and exists (select 1 from (" + str + ") t1 where t1.fs_cardno = t.fs_cardno) ";

            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable table = ds.Tables[0];

                str = "";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    str += (string.IsNullOrEmpty(str) ? "" : "、") + Convert.ToString(table.Rows[i]["FS_GP_STOVENO"]);
                }

                if (!string.IsNullOrEmpty(str))
                {
                    if (MessageBox.Show("炉号 " + str + " 质量不合格，确认要组批吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    return true;        //出错忽略质量情况
                }

                return true;
            }
        }

        private void Save()
        {
            try
            {
                if (ultraGrid2.Rows.Count <= 0)
                {
                    return;
                }

                ArrayList alistCardNo = new ArrayList();
                ArrayList alistCount = new ArrayList();
                ArrayList alistWeight = new ArrayList();

                bool bOK = DataValidation_Save(out alistCardNo, out alistCount, out alistWeight);

                if (!bOK)
                    return;

                if (alistCardNo.Count == 0)
                {
                    MessageBox.Show("请选择需要组批的炉号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string strBatchNo = "", strOrderNo = "", strSpec = "", strLength = "";
                bOK = DataValidation_Spec(out strBatchNo, out strOrderNo, out strSpec, out strLength);

                if (!bOK)
                    return;

                if (!Validation_UnQualified(alistCardNo))
                    return;

                object[] sArgs;
                string strProcedure = "", strErr = "";
                ArrayList obj = null;

                Hashtable htblValue = new Hashtable();

                if (_PRODUCELINE == ProduceLine.XC)
                {
                    string V9 = (_CODETYPE == CodeType_XC.BC ? "1" : "0");
                    sArgs = new object[] { alistCardNo, alistCount, alistWeight, _STOCK, this.UserInfo.GetUserName(), strSpec, strLength, strOrderNo, strBatchNo, V9 };

                    strProcedure = "KG_MCMS_FLOWCARD.BatchManual_XC";
                }
                else
                {
                    sArgs = new object[] { alistCardNo, alistCount, alistWeight, _STOCK, this.UserInfo.GetUserName(), strSpec, strLength, strOrderNo, strBatchNo };

                    strProcedure = "KG_MCMS_FLOWCARD.BatchManual";
                }

                obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                if (int.Parse(obj[2].ToString()) > 0)
                {
                    this.GetBatchInfo(QueryOpportunity.AfterOpt);
                    this.GetStockInfo(QueryOpportunity.AfterOpt);
                    MessageBox.Show("组批操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("组批操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }

        private void Cancel()
        {
            try
            {
                if (ultraGrid1.Rows.Count <= 0)
                {
                    return;
                }

                if (ultraGrid1.ActiveRow == null)
                {
                    MessageBox.Show("请选择需要撤销组批的轧制编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                string strBatchNo = "";

                if (ultraGrid1.ActiveRow.ParentRow == null)
                {
                    strBatchNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_ZC_BATCHNO"].Value);
                }
                else
                {
                    strBatchNo = Convert.ToString(ultraGrid1.ActiveRow.ParentRow.Cells["FS_ZC_BATCHNO"].Value);
                }

                if (MessageBox.Show("确定要撤销轧制编号【" + strBatchNo + "】吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                string[] sArgs;
                string strProcedure = "", strErr = "";
                ArrayList obj = null;

                sArgs = new string[] { strBatchNo, this.UserInfo.GetUserName() };
                strProcedure = "KG_MCMS_FLOWCARD.Batch_Cancel";
                obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                if (int.Parse(obj[2].ToString()) > 0)
                {
                    this.GetBatchInfo(QueryOpportunity.AfterOpt);
                    this.GetStockInfo(QueryOpportunity.AfterOpt);
                    MessageBox.Show("轧制批次【" + strBatchNo + "】撤销成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); 
                }
                else
                {
                    MessageBox.Show("轧制批次【" + strBatchNo + "】撤销失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    {
                        if (rbtn_BatchInfo.Checked)
                        {
                            this.GetBatchInfo(QueryOpportunity.UI);
                        }
                        else
                        {
                            this.GetStockInfo(QueryOpportunity.UI);
                        }
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
            }

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

        private void cbx_Filter_CheckedChanged(object sender, EventArgs e)
        {
            CommonMethod.SetUltraGridRowFilter(ref ultraGrid1, cbx_Filter.Checked);
            CommonMethod.SetUltraGridRowFilter(ref ultraGrid2, cbx_Filter.Checked);
        }

        private void cbxDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = dateTimePicker2.Enabled = cbxDateTime.Checked;
        }

        private void rbtn_BatchInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_BatchInfo.Checked)
            {
                tbQueryButtressNo.Clear();
            }

            if (!rbtn_BatchInfo.Checked)
            {
                tbQueryBatchNo.Clear();
                cbxDateTime.Checked = false;
            }

            tbQueryButtressNo.Enabled = !rbtn_BatchInfo.Checked;
            tbQueryBatchNo.Enabled = rbtn_BatchInfo.Checked;

            ultraToolbarsManager1.Tools["组批时间从"].SharedProps.Caption = (rbtn_BatchInfo.Checked ? "组批时间从" : "判定时间从");            
        }

        private void llb_ExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ultraGrid1.Rows.ExpandAll(true);
        }

        private void llb_CloseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ultraGrid1.Rows.CollapseAll(true);
        }

        private void ultraGrid2_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key.Equals("CHECKED") || e.Cell.Column.Key.Equals("FN_BILLET_COUNT") || e.Cell.Column.Key.Equals("FN_BILLET_WEIGHT"))
                {
                    this.ultraGrid2.UpdateData();
                }
            }
            catch { }
        }

        private void ultraGrid2_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key.Equals("CHECKED"))
                {
                    if (Convert.ToBoolean(e.Cell.Value))
                    {
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_COUNT"].Value = this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_GPYS_NUMBER"].Value;
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value = this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value;
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT_LL"].Value = this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value;

                        string strWeightSZ = Convert.ToString(this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value).Trim();

                        if (string.IsNullOrEmpty(strWeightSZ) || strWeightSZ == "0")
                        {
                            this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value = this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT_LL"].Value;
                        }

                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid2, e.Cell.Row.Index, "FN_BILLET_COUNT", true);
                    }
                    else
                    {
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_COUNT"].Value = "";
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value = "";
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT_LL"].Value = "";
                    }
                    this.BatchCalc();
                }
                else if (e.Cell.Column.Key.Equals("FN_BILLET_WEIGHT"))
                {
                    string strValue = Convert.ToString(e.Cell.Value).Trim();

                    if (!Convert.ToBoolean(ultraGrid2.Rows[e.Cell.Row.Index].Cells["CHECKED"].Value) && !string.IsNullOrEmpty(strValue))
                    {
                        e.Cell.Value = "";
                        return;
                    }
                    this.BatchCalc();
                }
                else if (e.Cell.Column.Key.Equals("FN_BILLET_COUNT"))
                {
                    string strCount = Convert.ToString(e.Cell.Value).Trim();

                    if (!Convert.ToBoolean(ultraGrid2.Rows[e.Cell.Row.Index].Cells["CHECKED"].Value) && !string.IsNullOrEmpty(strCount))
                    {
                        e.Cell.Value = "";
                        return;
                    }

                    string strCountYS = Convert.ToString(this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_GPYS_NUMBER"].Value).Trim();

                    if (string.IsNullOrEmpty(strCount))
                    {
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value = "";
                        this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT_LL"].Value = "";
                        this.BatchCalc();
                        return;
                    }

                    int iCount = 0;
                    int iCountYS = Convert.ToInt16(strCountYS);

                    if (!int.TryParse(strCount, out iCount))
                    {
                        MessageBox.Show("组批数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountYS.ToString();
                        return;
                    }

                    if (iCount <= 0)
                    {
                        MessageBox.Show("组批数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountYS.ToString();
                        return;
                    }

                    if (iCount > iCountYS)
                    {
                        MessageBox.Show("组批数不能大于验收数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountYS.ToString();
                        return;
                    }

                    try
                    {
                        string strWeightYS = Convert.ToString(this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value).Trim();

                        if (!string.IsNullOrEmpty(strWeightYS))
                        {
                            decimal dWeightYS = Convert.ToDecimal(strWeightYS);

                            decimal dWeight = Math.Round(iCount * dWeightYS / iCountYS, 3);

                            this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value = dWeight.ToString();
                        }                        
                    }
                    catch { }

                    try
                    {
                        string strWeightYSLL = Convert.ToString(this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value).Trim();

                        if (!string.IsNullOrEmpty(strWeightYSLL))
                        {
                            decimal dWeightYSLL = Convert.ToDecimal(strWeightYSLL);

                            decimal dWeightLL = Math.Round(iCount * dWeightYSLL / iCountYS, 3);

                            this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT_LL"].Value = dWeightLL.ToString();

                            string strWeightSZ = Convert.ToString(this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value).Trim();

                            if (string.IsNullOrEmpty(strWeightSZ) || strWeightSZ == "0")
                            {
                                this.ultraGrid2.Rows[e.Cell.Row.Index].Cells["FN_BILLET_WEIGHT"].Value = dWeightLL.ToString();
                            }
                        }                        
                    }
                    catch { }

                    this.BatchCalc();
                }
            }
            catch { }
        }

        private void BatchCalc()
        {
            try
            {
                bool bChecked = false;

                string strCount = "", strWeight = "", strWeightLL = "";

                int iCount = 0, iCountAcc = 0;

                decimal dWeight = 0.0M, dWeightAcc = 0.0M, dWeightLL = 0.0M, dWeightLLAcc = 0.0M;

                for (int i = 0; i < ultraGrid2.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(ultraGrid2.Rows[i].Cells["CHECKED"].Value))
                    {
                        if (!bChecked)
                            bChecked = true;

                        strCount = Convert.ToString(ultraGrid2.Rows[i].Cells["FN_BILLET_COUNT"].Value).Trim();
                        strWeight = Convert.ToString(ultraGrid2.Rows[i].Cells["FN_BILLET_WEIGHT"].Value).Trim();
                        strWeightLL = Convert.ToString(ultraGrid2.Rows[i].Cells["FN_BILLET_WEIGHT_LL"].Value).Trim();

                        if (!string.IsNullOrEmpty(strCount))
                        {
                            iCount = Convert.ToInt16(strCount);
                        }
                        else
                        {
                            iCount = 0;
                        }

                        if (!string.IsNullOrEmpty(strWeight))
                        {
                            dWeight = Convert.ToDecimal(strWeight);
                        }
                        else
                        {
                            dWeight = 0.0M;
                        }

                        if (!string.IsNullOrEmpty(strWeightLL))
                        {
                            dWeightLL = Convert.ToDecimal(strWeightLL);
                        }
                        else
                        {
                            dWeightLL = 0.0M;
                        }

                        iCountAcc += iCount;
                        dWeightAcc += dWeight;
                        dWeightLLAcc += dWeightLL;
                    }
                }

                if (bChecked)
                {
                    tbStatics.Text = "条数：" + iCountAcc.ToString() + "条　　　重量：" + dWeightAcc.ToString() + "吨　　　理重：" + dWeightLLAcc.ToString() + "吨";
                    if (!tbStatics.Visible)
                    {
                        tbStatics.Visible = true;
                    }
                }
                else
                {
                    if (tbStatics.Visible)
                    {
                        tbStatics.Clear();
                        tbStatics.Visible = false;
                    }
                }
            }
            catch { }
        }

        private void rbtn_CodeXC_CheckedChanged(object sender, EventArgs e)
        {
            _CODETYPE = rbtn_CodeXC.Checked ? CodeType_XC.DEFAULT : CodeType_XC.BC;
        }

        private void MarkupRows()
        {
            for (int i = 0; i < ultraGrid2.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToString(ultraGrid2.Rows[i].Cells["FS_UNQUALIFIED"].Value).Trim().Equals("1"))
                    {
                        ultraGrid2.Rows[i].Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        ultraGrid2.Rows[i].Appearance.ResetForeColor();
                    }
                }
                catch { }
            }
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UltraGridRow ugr in ultraGrid2.Rows)
            {

                if (this.cbSelectAll.Checked)
                {
                    ugr.Cells["CHECKED"].Value = "True";
                }
                else
                {
                    ugr.Cells["CHECKED"].Value = "False";
                }
            }
        }

    }
}