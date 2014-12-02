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

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinEditors;

namespace YGJZJL.Bar
{
    public partial class frmWeightPlanBC : FrmBase
    {
        private const string CNST_BC_STATION_A = "K17";         //棒材A区
        private const string CNST_BC_STATION_B = "K18";         //棒材B区

        public frmWeightPlanBC()
        {
            InitializeComponent();
        }

        private void frmWeightPlanBC_Load(object sender, EventArgs e)
        {
            CommonMethod.RefreshAndAutoSize(ultraGrid1);

            try
            {
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch { }

            try
            {
                this.ultraGrid1.DisplayLayout.Bands[1].Columns["FS_POINTID"].ValueList = GetValuelistWeightPoint();
                this.ultraGrid1.DisplayLayout.Bands[1].Columns["FS_PRINTWEIGHTTYPE"].ValueList = GetValuelistWeightType();
            }
            catch { }
            this.Reset();
        }

        private void GetPlanInfo(string strBatchNo)
        {
            string strWhere = "";

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strWhere += Convert.ToString("   and t.fs_batch_optdate between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
            {
                strWhere += Convert.ToString("   and t.fs_zc_batchno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
            }
            
            //预报状态
            if (cbEdt_Status.SelectedIndex > 0)
            {
                if (cbEdt_Status.Value.Equals("1"))
                {
                    strWhere += Convert.ToString("   and exists (select 1 from dt_productplan t1 where t1.fs_batchno = t.fs_zc_batchno and t1.fs_pointid is not null and t1.fn_bandcount > 0)").Trim() + " ";
                }
                else if (cbEdt_Status.Value.Equals("0"))
                {
                    strWhere += Convert.ToString("   and not exists (select 1 from dt_productplan t1 where t1.fs_batchno = t.fs_zc_batchno and t1.fs_pointid is not null and t1.fn_bandcount > 0)").Trim() + " ";
                }
            }
            //完炉状态
            if (cbEdt_Finish.SelectedIndex > 0)
            {
                if (cbEdt_Finish.Value.Equals("1"))
                {
                    strWhere += Convert.ToString("   and exists (select 1 from dt_productplan t1 where t1.fs_batchno = t.fs_zc_batchno and t1.fs_pointid is not null and t1.fs_completeflag = '1')").Trim() + " ";
                    strWhere += Convert.ToString("   and not exists (select 1 from dt_productplan t1 where t1.fs_batchno = t.fs_zc_batchno and t1.fs_pointid is not null and t1.fs_completeflag = '0')").Trim() + " ";
                }
                else if (cbEdt_Finish.Value.Equals("0"))
                {
                    strWhere += Convert.ToString("   and exists (select 1 from dt_productplan t1 where t1.fs_batchno = t.fs_zc_batchno and t1.fs_pointid is not null and t1.fs_completeflag = '0')").Trim() + " ";
                    strWhere += Convert.ToString("   and not exists (select 1 from dt_productplan t1 where t1.fs_batchno = t.fs_zc_batchno and t1.fs_pointid is not null and t1.fs_completeflag = '1')").Trim() + " ";
                }
            }

            //PL/SQL SPECIAL COPY
            string strSql = "";
            strSql += Convert.ToString("select fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("       fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("       fn_length,").Trim() + " ";
            strSql += Convert.ToString("       fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("       fs_zc_orderno,").Trim() + " ";

            strSql += Convert.ToString("       (select t.fn_bandcount").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_pointid = '" + CNST_BC_STATION_A + "') fn_point_01_plan,").Trim() + " ";

            strSql += Convert.ToString("       (select decode(nvl(t.fs_fclflag, '0'), '1', '非尺', '')").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_pointid = '" + CNST_BC_STATION_A + "') fn_point_01_fc,").Trim() + " ";

            strSql += Convert.ToString("       (select count(1)").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_point = '" + CNST_BC_STATION_A + "') fn_point_01_num,").Trim() + " ";

            strSql += Convert.ToString("       (select round(sum(nvl(t.fn_weight, 0)), 3)").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_point = '" + CNST_BC_STATION_A + "') fn_point_01_wgt,").Trim() + " ";

            strSql += Convert.ToString("       (select round(sum(nvl(t.fn_theoryweight, 0)), 3)").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_point = '" + CNST_BC_STATION_A + "') fn_point_01_wgt_ll,").Trim() + " ";

            strSql += Convert.ToString("       (select decode(t.fs_completeflag, '1', '√', '')").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_pointid = '" + CNST_BC_STATION_A + "') fn_point_01_done,").Trim() + " ";

            strSql += Convert.ToString("       (select t.fn_bandcount").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_pointid = '" + CNST_BC_STATION_B + "') fn_point_02_plan,").Trim() + " ";

            strSql += Convert.ToString("       (select decode(nvl(t.fs_fclflag, '0'), '1', '非尺', '')").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_pointid = '" + CNST_BC_STATION_B + "') fn_point_02_fc,").Trim() + " ";

            strSql += Convert.ToString("       (select count(1)").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_point = '" + CNST_BC_STATION_B + "') fn_point_02_num,").Trim() + " ";

            strSql += Convert.ToString("       (select sum(nvl(t.fn_weight, 0))").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_point = '" + CNST_BC_STATION_B + "') fn_point_02_wgt,").Trim() + " ";

            strSql += Convert.ToString("       (select sum(nvl(t.fn_theoryweight, 0))").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_point = '" + CNST_BC_STATION_B + "') fn_point_02_wgt_ll,").Trim() + " ";

            strSql += Convert.ToString("       (select decode(t.fs_completeflag, '1', '√', '')").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t.fs_pointid = '" + CNST_BC_STATION_B + "') fn_point_02_done,").Trim() + " ";

            strSql += Convert.ToString("       case when decode(fn_zz_weight,0,decode(fn_zc_enterweight,0,fn_billet_weight,fn_zc_enterweight),fn_zz_weight) > 0 then ").Trim() + " ";
            strSql += Convert.ToString("       round(100*nvl(rate_actual,0)/decode(fn_zz_weight,0,decode(fn_zc_enterweight,0,fn_billet_weight,fn_zc_enterweight),fn_zz_weight), 2) ").Trim() + " ";
            strSql += Convert.ToString("       end RATE_ACTUAL,").Trim() + " ";     //RATE_ACTUAL

            strSql += Convert.ToString("       case when decode(fn_zz_weight,0,decode(fn_zc_enterweight,0,fn_billet_weight,fn_zc_enterweight),fn_zz_weight) > 0 then ").Trim() + " ";
            strSql += Convert.ToString("       round(100*nvl(rate_theory,0)/decode(fn_zz_weight,0,decode(fn_zc_enterweight,0,fn_billet_weight,fn_zc_enterweight),fn_zz_weight), 2) ").Trim() + " ";
            strSql += Convert.ToString("       end RATE_THEORY,").Trim() + " ";     //RATE_THEORY

            strSql += Convert.ToString("       (select max(t.fs_planperson)").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno) fs_planperson,").Trim() + " ";

            strSql += Convert.ToString("       (select to_char(min(t.fd_plantime), 'yyyy-MM-dd hh24:mi:ss')").Trim() + " ";
            strSql += Convert.ToString("          from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString("         where t.fs_batchno = fs_zc_batchno) fd_plantime,").Trim() + " ";

            strSql += Convert.ToString("       fn_billet_count,").Trim() + " ";
            strSql += Convert.ToString("       fn_billet_weight,").Trim() + " ";
            strSql += Convert.ToString("       fn_billet_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("       fn_zc_enternumber,").Trim() + " ";
            strSql += Convert.ToString("       fn_zc_enterweight,").Trim() + " ";
            strSql += Convert.ToString("       fn_zc_enterweight_ll,").Trim() + " ";
            strSql += Convert.ToString("       case").Trim() + " ";
            strSql += Convert.ToString("         when nvl(fn_zc_enternumber, 0) > 0 and fs_discharge_end_max = '1' and fs_discharge_end_min = '1' then").Trim() + " ";
            strSql += Convert.ToString("          '√'").Trim() + " ";
            strSql += Convert.ToString("       end discharged,").Trim() + " ";
            strSql += Convert.ToString("       fn_zz_num,").Trim() + " ";
            strSql += Convert.ToString("       fn_zz_weight,").Trim() + " ";
            strSql += Convert.ToString("       fn_zz_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("       fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("       fn_gp_len,").Trim() + " ";
            strSql += Convert.ToString("       fs_batch_optor,").Trim() + " ";
            strSql += Convert.ToString("       fs_batch_optdate").Trim() + " ";
            strSql += Convert.ToString("  from (select distinct t.fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fn_zz_spec) over(partition by t.fs_zc_batchno) fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fn_length) over(partition by t.fs_zc_batchno) fn_length,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fs_gp_steeltype) over(partition by t.fs_zc_batchno) fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fs_zc_orderno) over(partition by t.fs_zc_batchno) fs_zc_orderno,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fs_gp_spe) over(partition by t.fs_zc_batchno) fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fn_gp_len) over(partition by t.fs_zc_batchno) fn_gp_len,").Trim() + " ";

            strSql += Convert.ToString("                        (select sum(nvl(t1.fn_weight,0)) from dt_productweightdetail t1 where t1.fs_batchno = t.fs_zc_batchno) rate_actual,").Trim() + " ";
            strSql += Convert.ToString("                        (select sum(nvl(t1.fn_theoryweight,0)) from dt_productweightdetail t1 where t1.fs_batchno = t.fs_zc_batchno) rate_theory,").Trim() + " ";

            strSql += Convert.ToString("                        sum(nvl(t.fn_billet_count, 0)) over(partition by t.fs_zc_batchno) fn_billet_count,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_billet_weight, 0)) over(partition by t.fs_zc_batchno) fn_billet_weight,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_billet_weight_ll, 0)) over(partition by t.fs_zc_batchno) fn_billet_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_zc_enternumber, 0)) over(partition by t.fs_zc_batchno) fn_zc_enternumber,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_zc_enterweight, 0)) over(partition by t.fs_zc_batchno) fn_zc_enterweight,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_zc_enterweight_ll, 0)) over(partition by t.fs_zc_batchno) fn_zc_enterweight_ll,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_zz_num, 0)) over(partition by t.fs_zc_batchno) fn_zz_num,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_zz_weight, 0)) over(partition by t.fs_zc_batchno) fn_zz_weight,").Trim() + " ";
            strSql += Convert.ToString("                        sum(nvl(t.fn_zz_weight_ll, 0)) over(partition by t.fs_zc_batchno) fn_zz_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("                        max(nvl(t.fs_discharge_end, '0')) over(partition by t.fs_zc_batchno) fs_discharge_end_max,").Trim() + " ";
            strSql += Convert.ToString("                        min(nvl(t.fs_discharge_end, '0')) over(partition by t.fs_zc_batchno) fs_discharge_end_min,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fs_batch_optor) over(partition by t.fs_zc_batchno) fs_batch_optor,").Trim() + " ";
            strSql += Convert.ToString("                        max(t.fs_batch_optdate) over(partition by t.fs_zc_batchno) fs_batch_optdate").Trim() + " ";
            strSql += Convert.ToString("          from (select t.fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("                       t.fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("                       round(t.fn_length, 3) fn_length,").Trim() + " ";
            strSql += Convert.ToString("                       t.fn_gpys_number fn_billet_count,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_zc_orderno,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("                       round(t.fn_gp_len, 3) fn_gp_len,").Trim() + " ";
            strSql += Convert.ToString("                       round(t.fn_gpys_weight, 3) fn_billet_weight,").Trim() + " ";
            strSql += Convert.ToString("                       round(nvl(fn_gpys_number, 0) *").Trim() + " ";
            strSql += Convert.ToString("                             round(").Trim() + " ";
            strSql += Convert.ToString("                                    0.21 * nvl(t.fn_gp_len, 0), 3),").Trim() + " ";
            strSql += Convert.ToString("                             3) fn_billet_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("                       t.fn_zc_enternumber,").Trim() + " ";
            strSql += Convert.ToString("                       round(t.fn_zc_enternumber * t.fn_gpys_weight /").Trim() + " ";
            strSql += Convert.ToString("                             t.fn_gpys_number,").Trim() + " ";
            strSql += Convert.ToString("                             3) fn_zc_enterweight,").Trim() + " ";
            strSql += Convert.ToString("                       round(t.fn_zc_enternumber *").Trim() + " ";
            strSql += Convert.ToString("                             round(").Trim() + " ";
            strSql += Convert.ToString("                                    0.21 * nvl(t.fn_gp_len, 0), 3),").Trim() + " ";
            strSql += Convert.ToString("                             3) fn_zc_enterweight_ll,").Trim() + " ";
            strSql += Convert.ToString("                       t.fn_zz_num,").Trim() + " ";
            strSql += Convert.ToString("                       nvl(t.fs_discharge_end, '0') fs_discharge_end,").Trim() + " ";
            strSql += Convert.ToString("                       round(t.fn_zz_num * t.fn_gpys_weight /").Trim() + " ";
            strSql += Convert.ToString("                             t.fn_gpys_number,").Trim() + " ";
            strSql += Convert.ToString("                             3) fn_zz_weight,").Trim() + " ";
            strSql += Convert.ToString("                       round(t.fn_zz_num *").Trim() + " ";
            strSql += Convert.ToString("                             round(").Trim() + " ";
            strSql += Convert.ToString("                                    0.21 * nvl(t.fn_gp_len, 0), 3),").Trim() + " ";
            strSql += Convert.ToString("                             3) fn_zz_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_batch_optor,").Trim() + " ";
            strSql += Convert.ToString("                       to_char(t.fs_batch_optdate, 'yyyy-MM-dd HH24:mi:ss') fs_batch_optdate").Trim() + " ";
            strSql += Convert.ToString("                  from it_fp_techcard t").Trim() + " ";
            strSql += Convert.ToString("                 where t.fs_batched = '1'").Trim() + " ";
            strSql += Convert.ToString("                   and t.fs_zc_batchno is not null").Trim() + " ";
            strSql += Convert.ToString("                   and t.fs_gp_flow = 'SH000098'").Trim() + " ";
            strSql += Convert.ToString("                   and nvl(t.fs_isvalid, '0') = '0'").Trim() + " ";
            strSql += Convert.ToString(" " + strWhere + " ) t) order by FS_ZC_BATCHNO desc").Trim();

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

                dataTable2.Rows.Clear();
                dataTable1.Rows.Clear();
                this.Reset();
                return;
            }

            strSql = "";
            strSql += Convert.ToString("select t.fs_batchno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_steeltype,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_spec,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_length,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_productno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_pointid,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_bandcount fn_point_plan,").Trim() + " ";
            strSql += Convert.ToString("       (select count(1)").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t1").Trim() + " ";
            strSql += Convert.ToString("         where t1.fs_batchno = t.fs_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t1.fs_point = t.fs_pointid) fn_point_num,").Trim() + " ";
            strSql += Convert.ToString("       (select sum(nvl(t1.fn_weight, 0))").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t1").Trim() + " ";
            strSql += Convert.ToString("         where t1.fs_batchno = t.fs_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t1.fs_point = t.fs_pointid) fn_point_wgt,").Trim() + " ";
            strSql += Convert.ToString("       (select sum(nvl(t1.fn_theoryweight, 0))").Trim() + " ";
            strSql += Convert.ToString("          from dt_productweightdetail t1").Trim() + " ";
            strSql += Convert.ToString("         where t1.fs_batchno = t.fs_batchno").Trim() + " ";
            strSql += Convert.ToString("           and t1.fs_point = t.fs_pointid) fn_point_wgt_ll,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_standno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_printweighttype,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_singlenum,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_singleweight,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_printtype,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fs_fclflag, '0'), '1', '非尺', '') fs_fclflag,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fs_twinstype, '0'), '1', '√', '') fs_twinstype,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fs_addresscheck, '0'), '1', '√', '') fs_addresscheck,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fs_standardcheck, '0'), '1', '√', '') fs_standardcheck,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fs_steeltypecheck, '0'), '1', '√', '') fs_steeltypecheck").Trim() + " ";
            strSql += Convert.ToString("  from dt_productplan t").Trim() + " ";
            strSql += Convert.ToString(" where t.fs_gp_flow = 'SH000098'").Trim() + " ";
            strSql += Convert.ToString("   and exists (select 1").Trim() + " ";
            strSql += Convert.ToString("          from (select distinct t.fs_zc_batchno").Trim() + " ";
            strSql += Convert.ToString("                  from It_Fp_Techcard t").Trim() + " ";
            strSql += Convert.ToString("                 where t.fs_batched = '1'").Trim() + " ";
            strSql += Convert.ToString("                   and t.fs_zc_batchno is not null").Trim() + " ";
            strSql += Convert.ToString("                   and t.fs_gp_flow = 'SH000098'").Trim() + " ";
            strSql += Convert.ToString("                   and nvl(t.fs_isvalid, '0') = '0'").Trim() + " ";
            strSql += Convert.ToString("     " + strWhere + " ) x where x.fs_zc_batchno = t.fs_batchno)").Trim() + " ";
            strSql += Convert.ToString(" order by t.fs_batchno, t.fs_pointid").Trim();

            err = "";

            ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && string.IsNullOrEmpty(err))
            {
                tbDetail = ds.Tables[0];

                ArrayList alistCnst1 = new ArrayList();
                if (dataTable2.Constraints.Count > 0)
                {
                    foreach (Constraint cnst in dataTable2.Constraints)
                    {
                        alistCnst1.Add(cnst);
                    }

                    dataTable2.Constraints.Clear();
                }

                CommonMethod.CopyDataToDatatable(ref tbDetail, ref dataTable2, true);
                CommonMethod.CopyDataToDatatable(ref tbMain, ref dataTable1, true);

                for (int i = 0; i < alistCnst1.Count; i++)
                {
                    dataTable2.Constraints.Add((Constraint)alistCnst1[i]);
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

            if (dataTable1.Rows.Count == 0)
            {
                this.Reset();
                this.CalcCountPlan();
                this.CalcCountPlanRate(cbx_Point1, Edt_Num1, cbx_FC1, lbl_Rate_Plan1);
                this.CalcCountPlanRate(cbx_Point2, Edt_Num2, cbx_FC2, lbl_Rate_Plan2);
            }
            else
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }

            this.MarkupRows();

            if (string.IsNullOrEmpty(strBatchNo))
                return;

            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_ZC_BATCHNO"].Value).Equals(strBatchNo))
                    {
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FS_ZC_BATCHNO", false);
                        ultraGrid1.Rows[i].Expanded = true;
                        break;
                    }
                }
                catch { }
            }
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    {
                        this.GetPlanInfo("");
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

        private void SetEditAreaFromRowInfo(UltraGridRow Row)
        {
            try
            {
                if (Row == null)
                {
                    return;
                }

                bool bPoint1 = false, bPoint2 = false;
                string strBatchNo = "", strSteel = "", strSpec = "", strLength = "", strOrderNo = "", strStandardNo = "";
                string strSingleNum = "", strWeightLL = "", strNum1 = "", strFcFlag1 = "", strNum2 = "", strFcFlag2 = "";
                string strWeightType = "", strCardType = "", strDouble = "", strPrintAddr = "", strPrintStdNo = "", strPrintSteel = "";

                strBatchNo = Convert.ToString(Row.Cells["FS_BATCHNO"].Value).Trim();
                strSteel = Convert.ToString(Row.Cells["FS_STEELTYPE"].Value).Trim();
                strSpec = Convert.ToString(Row.Cells["FS_SPEC"].Value).Trim();
                strLength = Convert.ToString(Row.Cells["FN_LENGTH"].Value).Trim();
                strOrderNo = Convert.ToString(Row.Cells["FS_PRODUCTNO"].Value).Trim();
                strStandardNo = Convert.ToString(Row.Cells["FS_STANDNO"].Value).Trim();

                strSingleNum = Convert.ToString(Row.Cells["FN_SINGLENUM"].Value).Trim();
                strWeightLL = Convert.ToString(Row.Cells["FN_SINGLEWEIGHT"].Value).Trim();

                strWeightType = Convert.ToString(Row.Cells["FS_PRINTWEIGHTTYPE"].Value).Trim();
                strCardType = Convert.ToString(Row.Cells["FS_PRINTTYPE"].Value).Trim();
                strDouble = Convert.ToString(Row.Cells["FS_TWINSTYPE"].Value).Trim();
                strPrintAddr = Convert.ToString(Row.Cells["FS_ADDRESSCHECK"].Value).Trim();
                strPrintStdNo = Convert.ToString(Row.Cells["FS_STANDARDCHECK"].Value).Trim();
                strPrintSteel = Convert.ToString(Row.Cells["FS_STEELTYPECHECK"].Value).Trim();

                if (Convert.ToString(Row.Cells["FS_POINTID"].Value).Equals(CNST_BC_STATION_A))
                {
                    strNum1 = Convert.ToString(Row.Cells["FN_POINT_PLAN"].Value).Trim();
                    strFcFlag1 = Convert.ToString(Row.Cells["FS_FCLFLAG"].Value).Trim();
                    bPoint1 = true;
                }
                else if (Convert.ToString(Row.Cells["FS_POINTID"].Value).Equals(CNST_BC_STATION_B))
                {
                    strNum2 = Convert.ToString(Row.Cells["FN_POINT_PLAN"].Value).Trim();
                    strFcFlag2 = Convert.ToString(Row.Cells["FS_FCLFLAG"].Value).Trim();
                    bPoint2 = true;
                }

                Edt_BatchNo.Text = strBatchNo;
                cbEdt_Steel.Text = strSteel;
                cbEdt_Spec.Text = strSpec;
                cbEdt_Length.Text = strLength;
                Edt_OrderNo.Text = strOrderNo;
                cbEdt_StandardNo.Text = strStandardNo;
                cbEdt_CardType.Text = strCardType;

                try
                {
                    if (!string.IsNullOrEmpty(strSingleNum))
                        Edt_SingleNum.Value = Convert.ToDecimal(strSingleNum);
                    else
                        Edt_SingleNum.Value = null;
                }
                catch { }

                try
                {
                    if (!string.IsNullOrEmpty(strWeightLL))
                        Edt_WgtLL.Value = Convert.ToDecimal(strWeightLL);
                    else
                        Edt_WgtLL.Value = null;
                }
                catch { }

                string strValue = "";

                try
                {
                    strValue = (strFcFlag1.Equals("非尺") ? "1" : "");
                    this.SetFC1(strValue);
                }
                catch { }

                try
                {
                    if (!string.IsNullOrEmpty(strNum1))
                        Edt_Num1.Value = Convert.ToDecimal(strNum1);
                    else
                        Edt_Num1.Value = null;
                }
                catch { }
                finally
                {
                    cbx_Point1.Checked = bPoint1;
                }

                try
                {
                    strValue = (strFcFlag2.Equals("非尺") ? "1" : "");
                    this.SetFC2(strValue);
                }
                catch { }

                try
                {
                    if (!string.IsNullOrEmpty(strNum2))
                        Edt_Num2.Value = Convert.ToDecimal(strNum2);
                    else
                        Edt_Num2.Value = null;
                }
                catch { }
                finally
                {
                    cbx_Point2.Checked = bPoint2;
                }

                this.SetPrintWeightType(strWeightType);

                this.TrueValue(strDouble, out strValue);
                this.SetDoubleCopies(strValue);

                this.TrueValue(strPrintAddr, out strValue);
                this.SetPrintAddr(strValue);

                this.TrueValue(strPrintStdNo, out strValue);
                this.SetPrintStdNo(strValue);

                this.TrueValue(strPrintSteel, out strValue);
                this.SetPrintSteel(strValue);
            }
            catch { }
        }

        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow ActiveRow = ultraGrid1.ActiveRow; 
                string strBatchNo = "", strSteel = "", strSpec = "", strLength = "", strOrderNo = "";

                if (ActiveRow.ParentRow == null)    //选择的是主记录
                {
                    if (ActiveRow.ChildBands.Count > 0 && ActiveRow.ChildBands[0].Rows.Count > 0)       //有子行
                    {
                        ActiveRow = ActiveRow.ChildBands[0].Rows[0];
                        this.SetEditAreaFromRowInfo(ActiveRow);
                    }
                    else                            //还没有预报
                    {
                        this.Reset();

                        strBatchNo = Convert.ToString(ActiveRow.Cells["FS_ZC_BATCHNO"].Value).Trim();
                        strSteel = Convert.ToString(ActiveRow.Cells["FS_GP_STEELTYPE"].Value).Trim();
                        strSpec = Convert.ToString(ActiveRow.Cells["FN_ZZ_SPEC"].Value).Trim();
                        strLength = Convert.ToString(ActiveRow.Cells["FN_LENGTH"].Value).Trim();
                        strOrderNo = Convert.ToString(ActiveRow.Cells["FS_ZC_ORDERNO"].Value).Trim();

                        Edt_BatchNo.Text = strBatchNo;
                        cbEdt_Steel.Text = strSteel;
                        cbEdt_Spec.Text = strSpec;
                        cbEdt_Length.Text = strLength;
                        Edt_OrderNo.Text = strOrderNo;
                    }
                }
                else                                //选择的是预报子记录
                {
                    this.SetEditAreaFromRowInfo(ActiveRow);
                }
            }
            catch { }

            this.CalcCountPlan();
            this.CalcCountPlanRate(cbx_Point1, Edt_Num1, cbx_FC1, lbl_Rate_Plan1);
            this.CalcCountPlanRate(cbx_Point2, Edt_Num2, cbx_FC2, lbl_Rate_Plan2);
        }

        private void cbx_Filter_CheckedChanged(object sender, EventArgs e)
        {
            CommonMethod.SetUltraGridRowFilter(ref ultraGrid1, cbx_Filter.Checked);
        }

        private void Reset()
        {
            try
            {
                cbEdt_Steel.SelectedIndex = -1;
                cbEdt_Steel.Clear();

                cbEdt_Spec.SelectedIndex = -1;
                cbEdt_Spec.Clear();

                cbEdt_Length.SelectedIndex = -1;
                cbEdt_Length.Clear();

                Edt_BatchNo.Clear();
                Edt_OrderNo.Clear();

                cbEdt_StandardNo.SelectedIndex = -1;
                cbEdt_StandardNo.Clear();

                cbEdt_CardType.Value = "常规打牌";

                Edt_SingleNum.Value = null;
                Edt_WgtLL.Value = null;

                Edt_Num1.Value = null;
                Edt_Num2.Value = null;

                cbx_FC1.Checked = false;
                cbx_FC2.Checked = false;

                cbx_Point1.Checked = false;
                cbx_Point2.Checked = false;

                this.SetPrintWeightType("");
                this.SetDoubleCopies("1");
                this.SetPrintAddr("1");
                this.SetPrintStdNo("");
                this.SetPrintSteel("1");
            }
            catch { }
        }

        private void SetPrintWeightType(string Value)
        {
            switch (Value)
            {
                case "0":
                    cbx_WgtT.Checked = true;
                    cbx_WgtR.Checked = false;
                    break;
                case "1":
                    cbx_WgtT.Checked = false;
                    cbx_WgtR.Checked = true;
                    break;
                case "2":
                    cbx_WgtT.Checked = true;
                    cbx_WgtR.Checked = true;
                    break;
                default:
                    cbx_WgtT.Checked = true;
                    cbx_WgtR.Checked = false;
                    break;
            }
        }

        private void SetFC1(string Value)
        {
            cbx_FC1.Checked = (Value.Equals("1"));
        }

        private void SetFC2(string Value)
        {
            cbx_FC2.Checked = (Value.Equals("1"));
        }

        private void SetDoubleCopies(string Value)
        {
            cbx_Double.Checked = (Value.Equals("1"));
        }

        private void SetPrintAddr(string Value)
        {
            cbx_Addr.Checked = (Value.Equals("1"));
        }

        private void SetPrintStdNo(string Value)
        {
            cbx_StandardNo.Checked = (Value.Equals("1"));
        }

        private void SetPrintSteel(string Value)
        {
            cbx_Steel.Checked = (Value.Equals("1"));
        }

        private void cbx_Point1_CheckedChanged(object sender, EventArgs e)
        {
            Edt_Num1.Enabled = cbx_Point1.Checked;
            cbx_FC1.Enabled = cbx_Point1.Checked;
            btnDone1.Enabled = cbx_Point1.Checked;
            btnSave1.Text = cbx_Point1.Checked ? "保存预报" : "删除预报";

            if (cbx_Point1.Checked)
            {
                try
                {
                    if (!cbx_Point2.Checked && !string.IsNullOrEmpty(lbl_Count.Text))
                    {
                        Edt_Num1.Value = Convert.ToDecimal(lbl_Count.Text);
                    }
                }
                catch { }

                Edt_Num1.SelectAll();
                Edt_Num1.Focus();
            }

            this.CalcCountPlanRate(cbx_Point1, Edt_Num1, cbx_FC1, lbl_Rate_Plan1);
        }

        private void cbx_Point2_CheckedChanged(object sender, EventArgs e)
        {
            Edt_Num2.Enabled = cbx_Point2.Checked;
            cbx_FC2.Enabled = cbx_Point2.Checked;
            btnDone2.Enabled = cbx_Point2.Checked;
            btnSave2.Text = cbx_Point2.Checked ? "保存预报" : "删除预报";

            if (cbx_Point2.Checked)
            {
                try
                {
                    if (!cbx_Point1.Checked && !string.IsNullOrEmpty(lbl_Count.Text))
                    {
                        Edt_Num2.Value = Convert.ToDecimal(lbl_Count.Text);
                    }
                }
                catch { }

                Edt_Num2.SelectAll();
                Edt_Num2.Focus();
            }

            this.CalcCountPlanRate(cbx_Point2, Edt_Num2, cbx_FC2, lbl_Rate_Plan2);
        }

        private ValueList  GetValuelistWeightType()
        {
            ValueList vlist = new ValueList();
            try
            {
                vlist.ValueListItems.Add("0", "理重");
                vlist.ValueListItems.Add("1", "实重");
                vlist.ValueListItems.Add("2", "实重＋理重");
                vlist.ValueListItems.Add("", "");
            }
            catch { }

            return vlist;
        }

        private ValueList GetValuelistWeightPoint()
        {
            ValueList vlist = new ValueList();
            try
            {
                vlist.ValueListItems.Add(CNST_BC_STATION_A, "A区");
                vlist.ValueListItems.Add(CNST_BC_STATION_B, "B区");
                vlist.ValueListItems.Add("", "");
            }
            catch { }

            return vlist;
        }

        private bool TrueValue(string Value,out string ConvertedValue)
        {
            ConvertedValue = "";

            if (Value.Equals("√"))
            {
                ConvertedValue = "1";
                return true;
            }

            return false;
        }

        private void llb_ExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.ultraGrid1.Rows.ExpandAll(true);
        }

        private void llb_CloseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.ultraGrid1.Rows.CollapseAll(true);
        }

        private void CalcCountPlan()
        {
            try
            {
                UltraGridRow row = ultraGrid1.ActiveRow;

                if (ultraGrid1.Rows.Count <= 0 || row == null)
                {
                    lbl_Count.Text = "";
                    lbl_Rate_Pre.Text = "";
                    return;
                }

                if (Edt_WgtLL.Value == null || string.IsNullOrEmpty(Edt_WgtLL.Value.ToString().Trim()))
                {
                    lbl_Count.Text = "";
                    lbl_Rate_Pre.Text = "";
                    return;
                }

                if (row.ParentRow != null)
                    row = row.ParentRow;

                string strTotal = Convert.ToString(row.Cells["FN_ZZ_WEIGHT"].Value).Trim();

                if (string.IsNullOrEmpty(strTotal) || strTotal.Equals("0"))
                {
                    strTotal = Convert.ToString(row.Cells["FN_ZC_ENTERWEIGHT"].Value).Trim();
                }

                if (string.IsNullOrEmpty(strTotal) || strTotal.Equals("0"))
                {
                    strTotal = Convert.ToString(row.Cells["FN_BILLET_WEIGHT"].Value).Trim();
                }

                if (string.IsNullOrEmpty(strTotal) || strTotal.Equals("0"))
                {
                    lbl_Count.Text = "";
                    lbl_Rate_Pre.Text = "";
                    return;
                }

                decimal dUnit = 0.0M;
                decimal dTotal = 0.0M;

                bool bOK = decimal.TryParse(strTotal, out dTotal);

                if (!bOK)
                {
                    lbl_Count.Text = "";
                    lbl_Rate_Pre.Text = "";
                    return;
                }

                bOK = decimal.TryParse(Edt_WgtLL.Value.ToString().Trim(), out dUnit);

                if (!bOK || dUnit <= 0)
                {
                    lbl_Count.Text = "";
                    lbl_Rate_Pre.Text = "";
                    return;
                }

                lbl_Count.Text = Convert.ToString(Convert.ToInt16(dTotal / dUnit) + 1);
                lbl_Rate_Pre.Text = Convert.ToString(Math.Round(100 * (Convert.ToInt16(dTotal / dUnit) + 1) * dUnit / dTotal, 2)) + "%";
            }
            catch
            {
                lbl_Count.Text = "";
                lbl_Rate_Pre.Text = "";
            }
        }

        private void CalcCountPlanRate(UltraCheckEditor cbx_Checked, UltraNumericEditor edt_Num, UltraCheckEditor cbx_Fcflag, Label lbl_Result)
        {
            try
            {
                UltraGridRow row = ultraGrid1.ActiveRow;

                if (ultraGrid1.Rows.Count <= 0 || row == null)
                {
                    lbl_Result.Text = "";
                    return;
                }

                if (!cbx_Checked.Checked || cbx_Fcflag.Checked)
                {
                    lbl_Result.Text = "";
                    return;
                }

                if (edt_Num.Value == null || string.IsNullOrEmpty(edt_Num.Value.ToString().Trim()))
                {
                    lbl_Result.Text = "";
                    return;
                }

                if (Edt_WgtLL.Value == null || string.IsNullOrEmpty(Edt_WgtLL.Value.ToString().Trim()))
                {
                    lbl_Result.Text = "";
                    return;
                }

                if (row.ParentRow != null)
                    row = row.ParentRow;

                string strTotal = Convert.ToString(row.Cells["FN_ZZ_WEIGHT"].Value).Trim();

                if (string.IsNullOrEmpty(strTotal) || strTotal.Equals("0"))
                {
                    strTotal = Convert.ToString(row.Cells["FN_ZC_ENTERWEIGHT"].Value).Trim();
                }

                if (string.IsNullOrEmpty(strTotal) || strTotal.Equals("0"))
                {
                    strTotal = Convert.ToString(row.Cells["FN_BILLET_WEIGHT"].Value).Trim();
                }

                if (string.IsNullOrEmpty(strTotal) || strTotal.Equals("0"))
                {
                    lbl_Result.Text = "";
                    return;
                }

                decimal dUnit = 0.0M;
                decimal dCount = 0.0M;
                decimal dTotal = 0.0M;

                bool bOK = decimal.TryParse(strTotal, out dTotal);

                if (!bOK)
                {
                    lbl_Result.Text = "";
                    return;
                }

                bOK = decimal.TryParse(edt_Num.Value.ToString().Trim(), out dCount);

                if (!bOK)
                {
                    lbl_Result.Text = "";
                    return;
                }

                bOK = decimal.TryParse(Edt_WgtLL.Value.ToString().Trim(), out dUnit);

                if (!bOK || dUnit <= 0)
                {
                    lbl_Result.Text = "";
                    return;
                }

                decimal dRate = Math.Round(100 * dCount * dUnit / dTotal, 2);

                if (dRate < 95.0M)
                {
                    lbl_Result.ForeColor = Color.Red;
                }
                else if (dRate > 107.0M)
                {
                    lbl_Result.ForeColor = Color.Blue;
                }
                else
                {
                    lbl_Result.ForeColor = Color.Green;
                }

                lbl_Result.Text = Convert.ToString(dRate) + "%";
            }
            catch
            {
                lbl_Result.Text = "";
            }
        }

        private void MarkupRows()
        {
            decimal dRate = 0.0M;
            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                try
                {
                    string strRate = Convert.ToString(ultraGrid1.Rows[i].Cells["RATE_THEORY"].Value).Trim();

                    if (string.IsNullOrEmpty(strRate))
                    {
                        ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.ResetForeColor();
                        ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.FontData.ResetBold();
                    }
                    else
                    {
                        dRate = Convert.ToDecimal(strRate);

                        if (dRate < 95)
                        {
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.ForeColor = Color.Red;
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.FontData.Bold = DefaultableBoolean.True;
                        }
                        else if (dRate > 107)
                        {
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.ForeColor = Color.Blue;
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.FontData.Bold = DefaultableBoolean.True;
                        }
                        else if (dRate > 102)
                        {
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.ForeColor = Color.DarkViolet;
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.FontData.Bold = DefaultableBoolean.False;
                        }
                        else
                        {
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.ForeColor = Color.Green;
                            ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.FontData.Bold = DefaultableBoolean.False;
                        }
                    }
                }
                catch
                {
                    ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.ResetForeColor();
                    ultraGrid1.Rows[i].Cells["RATE_THEORY"].Appearance.FontData.ResetBold();
                }

                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["FN_POINT_01_PLAN"].Value).Trim().Length > 0)
                    {
                        ultraGrid1.Rows[i].Cells["FN_POINT_01_PLAN"].Appearance.BackColor = Color.Lime;
                    }
                    else
                    {
                        ultraGrid1.Rows[i].Cells["FN_POINT_01_PLAN"].Appearance.ResetBackColor();
                    }
                }
                catch { }

                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["FN_POINT_02_PLAN"].Value).Trim().Length > 0)
                    {
                        ultraGrid1.Rows[i].Cells["FN_POINT_02_PLAN"].Appearance.BackColor = Color.Lime;
                    }
                    else
                    {
                        ultraGrid1.Rows[i].Cells["FN_POINT_02_PLAN"].Appearance.ResetBackColor();
                    }
                }
                catch { }

                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_FCLFLAG"].Value).Trim().Equals("非尺"))
                    {
                        ultraGrid1.Rows[i].Cells["FS_FCLFLAG"].Appearance.BackColor = Color.Red;
                        ultraGrid1.Rows[i].Cells["FS_FCLFLAG"].Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        ultraGrid1.Rows[i].Cells["FS_FCLFLAG"].Appearance.ResetBackColor();
                        ultraGrid1.Rows[i].Cells["FS_FCLFLAG"].Appearance.ResetForeColor();
                    }
                }
                catch { }
            }
        }

        private void Edt_WgtLL_ValueChanged(object sender, EventArgs e)
        {
            this.CalcCountPlan();
            this.CalcCountPlanRate(cbx_Point1, Edt_Num1, cbx_FC1, lbl_Rate_Plan1);
            this.CalcCountPlanRate(cbx_Point2, Edt_Num2, cbx_FC2, lbl_Rate_Plan2);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null)
                {
                    return;
                }

                string V0 = "";

                if (ultraGrid1.ActiveRow.ParentRow != null)
                {
                    V0 = Convert.ToString(ultraGrid1.ActiveRow.ParentRow.Cells["FS_ZC_BATCHNO"].Value);
                }
                else
                {
                    V0 = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_ZC_BATCHNO"].Value);
                }

                Button button = (Button)sender;

                if (!button.Equals(btnDone1) && !button.Equals(btnDone2))
                {
                    return;
                }

                string V1 = button.Equals(btnDone1) ? CNST_BC_STATION_A : CNST_BC_STATION_B;
                string V2 = this.UserInfo.GetUserName();

                if (MessageBox.Show("确认轧制编号【" + V0 + "】在 " + (V1.Equals(CNST_BC_STATION_A) ? "A区" : "B区") + " 计量结束吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                string strProcedure = "KG_MCMS_FLOWCARD.WeightPlanBC_Done", strErr = "";
                object[] sArgs = new object[] { V0, V1, V2 };

                ArrayList obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                if (int.Parse(obj[2].ToString()) > 0)
                {
                    this.GetPlanInfo(V0);
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            catch { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;

                if (!button.Equals(btnSave1) && !button.Equals(btnSave2))
                {
                    return;
                }

                UltraCheckEditor cbxPoint = (button.Equals(btnSave1) ? cbx_Point1 : cbx_Point2);
                UltraNumericEditor EdtNum = (button.Equals(btnSave1) ? Edt_Num1 : Edt_Num2);
                UltraCheckEditor cbxFC = (button.Equals(btnSave1) ? cbx_FC1 : cbx_FC2);

                if (ultraGrid1.Rows.Count <= 0 || ultraGrid1.ActiveRow == null)
                {
                    return;
                }

                string V0 = "";

                if (ultraGrid1.ActiveRow.ParentRow != null)
                {
                    V0 = Convert.ToString(ultraGrid1.ActiveRow.ParentRow.Cells["FS_ZC_BATCHNO"].Value);
                }
                else
                {
                    V0 = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_ZC_BATCHNO"].Value);
                }

                string V1 = button.Equals(btnSave1) ? CNST_BC_STATION_A : CNST_BC_STATION_B;
                string V2 = cbxPoint.Checked ? "1" : "2";
                string V3 = EdtNum.Value == null ? "" : EdtNum.Value.ToString();
                string V4 = cbxFC.Checked ? "1" : "0";
                string V5 = cbEdt_Steel.Text.Trim();
                string V6 = cbEdt_Spec.Text.Trim();
                string V7 = cbEdt_Length.Text.Trim();
                string V8 = Edt_OrderNo.Text.Trim();
                string V9 = cbEdt_StandardNo.Text.Trim();
                string V10 = Edt_SingleNum.Value == null ? "" : Edt_SingleNum.Value.ToString();
                string V11 = (cbx_WgtR.Checked && !cbx_WgtT.Checked) ? "1" : ((!cbx_WgtR.Checked && cbx_WgtT.Checked) ? "0" : ((cbx_WgtR.Checked && cbx_WgtT.Checked) ? "2" : ""));
                string V12 = cbEdt_CardType.Text.Trim();
                string V13 = cbx_Double.Checked ? "1" : "0";
                string V14 = cbx_Addr.Checked ? "1" : "0";
                string V15 = cbx_StandardNo.Checked ? "1" : "0";
                string V16 = cbx_Steel.Checked ? "1" : "0";
                string V17 = this.UserInfo.GetUserName();
                string V18 = Edt_WgtLL.Value == null ? "" : Edt_WgtLL.Value.ToString();

                bool Warned = false;
                int iNum = 0;

                if (V2 == "2")
                {
                    if (MessageBox.Show("确认要删除轧制编号【" + V0 + "】在" + (V1.Equals(CNST_BC_STATION_A) ? "A区" : "B区") + "的预报信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    Warned = true;
                }

                if (V2 == "1")
                {
                    if (string.IsNullOrEmpty(V5))
                    {
                        MessageBox.Show("请输入牌号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        cbEdt_Steel.SelectAll();
                        cbEdt_Steel.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(V6))
                    {
                        MessageBox.Show("请输入规格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        cbEdt_Spec.SelectAll();
                        cbEdt_Spec.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(V7) && V4 == "0")
                    {
                        MessageBox.Show("请输入定尺长度！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        cbEdt_Length.SelectAll();
                        cbEdt_Length.Focus();
                        return;
                    }

                    decimal dLength = 0.0M;
                    if (!string.IsNullOrEmpty(V7))
                    {
                        if (!decimal.TryParse(V7, out dLength))
                        {
                            MessageBox.Show("定尺不正确，请输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            cbEdt_Length.SelectAll();
                            cbEdt_Length.Focus();
                            return;
                        }

                        if (dLength <= 0)
                        {
                            MessageBox.Show("定尺必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            cbEdt_Length.SelectAll();
                            cbEdt_Length.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(V8))
                    {
                        MessageBox.Show("请输入生产订单号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Edt_OrderNo.SelectAll();
                        Edt_OrderNo.Focus();
                        return;
                    }

                    if (V4 == "0")
                    {
                        if (string.IsNullOrEmpty(V10))
                        {
                            MessageBox.Show("请输入单捆支数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Edt_SingleNum.SelectAll();
                            Edt_SingleNum.Focus();
                            return;
                        }

                        if (!int.TryParse(V10, out iNum))
                        {
                            MessageBox.Show("单捆支数不正确，请输入整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Edt_SingleNum.SelectAll();
                            Edt_SingleNum.Focus();
                            return;
                        }

                        if (iNum <= 0)
                        {
                            MessageBox.Show("单捆支数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Edt_SingleNum.SelectAll();
                            Edt_SingleNum.Focus();
                            return;
                        }

                        if (string.IsNullOrEmpty(V18))
                        {
                            MessageBox.Show("请输入单捆理重！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Edt_WgtLL.SelectAll();
                            Edt_WgtLL.Focus();
                            return;
                        }

                        decimal dWgt = 0.0M;

                        if (!decimal.TryParse(V18, out dWgt))
                        {
                            MessageBox.Show("单捆理重不正确，请输入整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Edt_WgtLL.SelectAll();
                            Edt_WgtLL.Focus();
                            return;
                        }

                        if (dWgt <= 0)
                        {
                            MessageBox.Show("单捆理重必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            Edt_WgtLL.SelectAll();
                            Edt_WgtLL.Focus();
                            return;
                        }
                    }

                    if (V4 == "1" && V11 != "1")
                    {
                        MessageBox.Show("非尺预报只能选择实重打牌！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        SetPrintWeightType("1");
                        return;
                    }

                    if (cbx_StandardNo.Checked && string.IsNullOrEmpty(cbEdt_StandardNo.Text.Trim()))
                    {
                        MessageBox.Show("请输入标准号预报数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        cbEdt_StandardNo.SelectAll();
                        cbEdt_StandardNo.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(V3))
                    {
                        MessageBox.Show("请输入" + (V1.Equals(CNST_BC_STATION_A) ? "A区" : "B区") + "预报数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        EdtNum.SelectAll();
                        EdtNum.Focus();
                        return;
                    }

                    if (!int.TryParse(V3, out iNum))
                    {
                        MessageBox.Show((V1.Equals(CNST_BC_STATION_A) ? "A区" : "B区") + "预报数不正确，请输入整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        EdtNum.SelectAll();
                        EdtNum.Focus();
                        return;
                    }

                    if (iNum <= 0)
                    {
                        MessageBox.Show((V1.Equals(CNST_BC_STATION_A) ? "A区" : "B区") + "预报数为零，如果确认不要在" + (V1.Equals(CNST_BC_STATION_A) ? "A区" : "B区") + "计量，请取消掉数据前面的 √ ！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        EdtNum.SelectAll();
                        EdtNum.Focus();
                        return;
                    }
                }

                if (!Warned && MessageBox.Show("确认保存预报信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                string strProcedure = "KG_MCMS_FLOWCARD.SaveWeightPlanBC", strErr = "";
                object[] sArgs = new object[] { V0, V1, V2, V3, V4, V5, V6, V7, V8, V9, V10, V11, V12, V13, V14, V15, V16, V17, V18 };

                ArrayList obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                if (int.Parse(obj[2].ToString()) > 0)
                {
                    this.GetPlanInfo(V0);
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }    
            }
            catch { }
        }

        private void Edt_Num1_ValueChanged(object sender, EventArgs e)
        {
            this.CalcCountPlanRate(cbx_Point1, Edt_Num1, cbx_FC1, lbl_Rate_Plan1);
        }

        private void Edt_Num2_ValueChanged(object sender, EventArgs e)
        {
            this.CalcCountPlanRate(cbx_Point2, Edt_Num2, cbx_FC2, lbl_Rate_Plan2);
        }

        private void cbx_FC1_CheckedChanged(object sender, EventArgs e)
        {
            this.CalcCountPlanRate(cbx_Point1, Edt_Num1, cbx_FC1, lbl_Rate_Plan1);
        }

        private void cbx_FC2_CheckedChanged(object sender, EventArgs e)
        {
            this.CalcCountPlanRate(cbx_Point2, Edt_Num2, cbx_FC2, lbl_Rate_Plan2);
        }

    }
}