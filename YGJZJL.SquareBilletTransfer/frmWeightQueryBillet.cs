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

namespace YGJZJL.SquareBilletTransfer
{
    public partial class frmWeightQueryBillet : FrmBase
    {
        public frmWeightQueryBillet()
        {
            InitializeComponent();
        }

        private void frmWeightQueryBillet_Load(object sender, EventArgs e)
        {
            Constant.RefreshAndAutoSize(ultraGrid1);

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
                pictureBox3.Hide();
                pictureBox3.SendToBack();
            }
            catch { }
        }

        private void GetWeightInfo()
        {
            //PL/SQL SPECIAL COPY
            string strWhere = "";

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strWhere += Convert.ToString("   and t.fd_starttime between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
            {
                strWhere += Convert.ToString("   and t.fs_stoveno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
            }

            string strSql = "";
            strSql += Convert.ToString("select *").Trim() + " ";
            strSql += Convert.ToString("  from (select case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  0").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  1").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  2").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  3").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  4").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  5").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  6").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  7").Trim() + " ";
            strSql += Convert.ToString("               end XH,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  '牌号【' || fs_steeltype || '】小计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  '规格【' || fs_spec || '】小计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  '长度【' || fn_length || '】小计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  '去向【' || fs_store || '】小计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  '输送方式【' || fs_transtype || '】小计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 and grouping(fn_length) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 1 and grouping(fs_transtype) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  '总计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_steeltype) = 0 and grouping(fs_spec) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fn_length) = 0 and grouping(fs_store) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fs_stoveno)").Trim() + " ";
            strSql += Convert.ToString("               end fs_stoveno,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  fs_steeltype").Trim() + " ";
            strSql += Convert.ToString("               end fs_steeltype,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  fs_spec").Trim() + " ";
            strSql += Convert.ToString("               end fs_spec,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  fn_length").Trim() + " ";
            strSql += Convert.ToString("               end fn_length,").Trim() + " ";
            strSql += Convert.ToString("               sum(nvl(fn_count, 0)) fn_count,").Trim() + " ";
            strSql += Convert.ToString("               sum(nvl(fn_billetcount, 0)) fn_billetcount,").Trim() + " ";
            strSql += Convert.ToString("               round(sum(nvl(fn_totalweight, 0)), 3) fn_totalweight,").Trim() + " ";
            strSql += Convert.ToString("               round(sum(nvl(fn_totalweight_ll, 0)), 3) fn_totalweight_ll,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fd_starttime)").Trim() + " ";
            strSql += Convert.ToString("               end fd_starttime,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fd_endtime)").Trim() + " ";
            strSql += Convert.ToString("               end fd_endtime,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  fs_store").Trim() + " ";
            strSql += Convert.ToString("               end fs_store,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  fs_transtype").Trim() + " ";
            strSql += Convert.ToString("               end fs_transtype,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fs_completeflag)").Trim() + " ";
            strSql += Convert.ToString("               end fs_completeflag,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(chemout)").Trim() + " ";
            strSql += Convert.ToString("               end chemout,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_stoveno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 and grouping(fn_length) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_store) = 0 and grouping(fs_transtype) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fs_unqualified)").Trim() + " ";
            strSql += Convert.ToString("               end fs_unqualified").Trim() + " ";
            strSql += Convert.ToString("          from (select t.fs_stoveno,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_steeltype,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_spec,").Trim() + " ";
            strSql += Convert.ToString("                       t.fn_length,").Trim() + " ";
            strSql += Convert.ToString("                       1 fn_count,").Trim() + " ";
            strSql += Convert.ToString("                       t2.fn_billetcount,").Trim() + " ";
            strSql += Convert.ToString("                       t2.fn_totalweight,").Trim() + " ";
            strSql += Convert.ToString("                       round(round(decode(substr(t.fs_stoveno, 4, 1),").Trim() + " ";
            strSql += Convert.ToString("                                    '1',").Trim() + " ";
            strSql += Convert.ToString("                                    0.174,").Trim() + " ";
            strSql += Convert.ToString("                                    '2',").Trim() + " ";
            strSql += Convert.ToString("                                    0.174,").Trim() + " ";
            strSql += Convert.ToString("                                    '3',").Trim() + " ";
            strSql += Convert.ToString("                                    0.176,").Trim() + " ";
            strSql += Convert.ToString("                                    0.174) * t.fn_length, 3) * t2.fn_billetcount, 3)").Trim() + " ";
            strSql += Convert.ToString("                               fn_totalweight_ll,").Trim() + " ";
            strSql += Convert.ToString("                       to_char(t.fd_starttime, 'yyyy-MM-dd hh24:mi:ss') fd_starttime,").Trim() + " ";
            strSql += Convert.ToString("                       to_char(t.fd_endtime, 'yyyy-MM-dd hh24:mi:ss') fd_endtime,").Trim() + " ";
            strSql += Convert.ToString("                       decode(nvl(t.fs_store, t1.fs_gp_flow),").Trim() + " ";
            strSql += Convert.ToString("                              'SH000100',").Trim() + " ";
            strSql += Convert.ToString("                              '高线',").Trim() + " ";
            strSql += Convert.ToString("                              'SH000098',").Trim() + " ";
            strSql += Convert.ToString("                              '棒材') fs_store,").Trim() + " ";
            strSql += Convert.ToString("                       decode(nvl(t.fs_transtype, t1.fs_transtype),").Trim() + " ";
            strSql += Convert.ToString("                              '1',").Trim() + " ";
            strSql += Convert.ToString("                              '1#辊道',").Trim() + " ";
            strSql += Convert.ToString("                              '2',").Trim() + " ";
            strSql += Convert.ToString("                              '2#辊道',").Trim() + " ";
            strSql += Convert.ToString("                              '3',").Trim() + " ";
            strSql += Convert.ToString("                              '汽车') fs_transtype,").Trim() + " ";
            strSql += Convert.ToString("                       decode(t.fs_completeflag, '1', '√', '') fs_completeflag,").Trim() + " ";
            strSql += Convert.ToString("                       case").Trim() + " ";
            strSql += Convert.ToString("                         when nvl(t1.fn_gp_c, 0) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                          '无'").Trim() + " ";
            strSql += Convert.ToString("                       end chemout,").Trim() + " ";
            strSql += Convert.ToString("                       decode(nvl(t1.fs_unqualified,'0'),'1','不合格','合格') fs_unqualified").Trim() + " ";
            strSql += Convert.ToString("                  from dt_steelweightmain t,").Trim() + " ";
            strSql += Convert.ToString("                       it_fp_techcard t1,").Trim() + " ";
            strSql += Convert.ToString("                       (select t1.fs_stoveno,").Trim() + " ";
            strSql += Convert.ToString("                               count(1) fn_billetcount,").Trim() + " ";
            strSql += Convert.ToString("                               sum(nvl(t1.fn_netweight, 0)) fn_totalweight").Trim() + " ";
            strSql += Convert.ToString("                          from dt_steelweightdetailroll t1").Trim() + " ";
            strSql += Convert.ToString("                         where exists").Trim() + " ";
            strSql += Convert.ToString("                         (select 1").Trim() + " ";
            strSql += Convert.ToString("                                  from dt_steelweightmain t").Trim() + " ";
            strSql += Convert.ToString("                                 where t.fs_stoveno = t1.fs_stoveno " + strWhere + ")").Trim() + " ";
            strSql += Convert.ToString("                         group by t1.fs_stoveno) t2").Trim() + " ";
            strSql += Convert.ToString("                 where t.fs_stoveno = t1.fs_gp_stoveno(+)").Trim() + " ";
            strSql += Convert.ToString("                   and t.fs_stoveno = t2.fs_stoveno(+) " + strWhere + ")").Trim() + " ";
            strSql += Convert.ToString("         group by cube(fs_stoveno,").Trim() + " ";
            strSql += Convert.ToString("                       fs_steeltype,").Trim() + " ";
            strSql += Convert.ToString("                       fs_spec,").Trim() + " ";
            strSql += Convert.ToString("                       fn_length,").Trim() + " ";
            strSql += Convert.ToString("                       fs_store,").Trim() + " ";
            strSql += Convert.ToString("                       fs_transtype))").Trim() + " ";
            strSql += Convert.ToString(" where xh <> 7").Trim() + " ";
            strSql += Convert.ToString(" order by xh, fs_stoveno").Trim();

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
                return;
            }

            strSql = "";
            strSql += Convert.ToString("select t.fs_weightno,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_billetindex,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_netweight,").Trim() + " ";
            strSql += Convert.ToString("       round(nvl(decode(substr(t.fs_stoveno, 4, 1),").Trim() + " ";
            strSql += Convert.ToString("                        '1',").Trim() + " ";
            strSql += Convert.ToString("                        0.174,").Trim() + " ";
            strSql += Convert.ToString("                        '2',").Trim() + " ";
            strSql += Convert.ToString("                        0.174,").Trim() + " ";
            strSql += Convert.ToString("                        '3',").Trim() + " ";
            strSql += Convert.ToString("                        0.176,").Trim() + " ";
            strSql += Convert.ToString("                        0.174),").Trim() + " ";
            strSql += Convert.ToString("                 0.174) * t1.fn_length,").Trim() + " ";
            strSql += Convert.ToString("             3) fn_netweight_ll,").Trim() + " ";
            strSql += Convert.ToString("       t2.fs_pointname fs_weightpoint,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_person,t.FS_HISWEIGHT,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_weighttime, 'yyyy-MM-dd HH24:mi:ss') fd_weighttime,").Trim() + " ";
            strSql += Convert.ToString("       decode(t.fs_thweitsingle, '1', '理重', '实重') fs_thweitsingle,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_stoveno").Trim() + " ";
            strSql += Convert.ToString("  from dt_steelweightdetailroll t, dt_steelweightmain t1, bt_point t2").Trim() + " ";
            strSql += Convert.ToString(" where t.fs_stoveno = t1.fs_stoveno(+)").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_weightpoint = t2.fs_pointcode(+)").Trim() + " ";
            strSql += Convert.ToString("   and exists").Trim() + " ";
            strSql += Convert.ToString(" (select 1").Trim() + " ";
            strSql += Convert.ToString("          from dt_steelweightmain t1").Trim() + " ";
            strSql += Convert.ToString("         where t1.fs_stoveno = t.fs_stoveno").Trim() + " ";

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strSql += Convert.ToString("   and t1.fd_starttime between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
            {
                strSql += Convert.ToString("   and t1.fs_stoveno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
            }

            strSql += Convert.ToString(" ) order by t.fs_stoveno, t.fn_billetindex").Trim();

            err = "";

            ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

            this.MarkupRows();

            Constant.RefreshAndAutoSize(ultraGrid1);

            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }
        }

        private string _LASTNO = "";

        private bool GetPictures(string strNo)
        {
            string err = "";
            string strSql = "select t.fb_image1, t.fb_image2 from dt_steelweightimage t where t.fs_weightno = '" + strNo + "'";

            DataSet ds = SelectReturnDS("ygjzjl.bar.DBHelp", "getSqlInfo", new object[] { strSql }, out err);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable table = ds.Tables[0];

                try
                {
                    byte[] Image1 = (byte[])table.Rows[0]["FB_IMAGE1"];
                    byte[] Image2 = (byte[])table.Rows[0]["FB_IMAGE2"];

                    BaseInfo GetImage = new BaseInfo();
                    GetImage.BitmapToImage(Image1, pictureBox1, pictureBox1.Width, pictureBox1.Height);
                    GetImage.BitmapToImage(Image2, pictureBox2, pictureBox2.Width, pictureBox2.Height);

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("图片显示出错！\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(err))
                {
                    MessageBox.Show("查询出错！\n" + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                try
                {
                    Image image1 = pictureBox1.Image;
                    Image image2 = pictureBox2.Image;

                    if (image1 != null)
                    {
                        image1.Dispose();
                    }

                    if (image2 != null)
                    {
                        image2.Dispose();
                    }
                }
                catch { }
                finally
                {
                    pictureBox1.Image = null;
                    pictureBox2.Image = null;
                }
                MessageBox.Show("没有找到计量图片数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
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
                case "导出":
                    {
                        CommonMethod.ExportDataWithSaveDialog2(ref this.ultraGrid1, "热送数据");
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
        }

        private void cbxDateTime_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = dateTimePicker2.Enabled = cbxDateTime.Checked;
        }

        private void llb_ExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ultraGrid1.Rows.ExpandAll(true);
        }

        private void llb_CloseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ultraGrid1.Rows.CollapseAll(true);
        }

        private void MarkupRows()
        {
            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_UNQUALIFIED"].Value).Trim().Equals("不合格"))
                    {
                        ultraGrid1.Rows[i].Appearance.ForeColor = Color.Red;
                    }
                    else
                    {
                        ultraGrid1.Rows[i].Appearance.ResetForeColor();
                    }
                }
                catch { }

                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["CHEMOUT"].Value).Trim().Length > 0)
                    {
                        ultraGrid1.Rows[i].Cells["CHEMOUT"].Appearance.BackColor = Color.Red;
                        ultraGrid1.Rows[i].Cells["CHEMOUT"].Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        ultraGrid1.Rows[i].Cells["CHEMOUT"].Appearance.ResetBackColor();
                        ultraGrid1.Rows[i].Cells["CHEMOUT"].Appearance.ResetForeColor();
                    }
                }
                catch { }
            }
        }

        private void ultraGrid1_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                if (e.Row.ParentRow != null)
                {
                    string strNo = Convert.ToString(e.Row.Cells["FS_WEIGHTNO"].Value);

                    if (!strNo.Equals(_LASTNO))
                    {
                        if (this.GetPictures(strNo))
                        {
                            if (!this.ultraExpandableGroupBox1.Expanded)
                            {
                                this.ultraExpandableGroupBox1.Expanded = true;
                            }

                            _LASTNO = strNo;
                        }
                    }
                }
            }
            catch { }
        }

        private void picSmall_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is PictureBox)
                {
                    Image image = ((PictureBox)sender).Image;

                    if (image == null)
                        return;

                    pictureBox3.Image = image;

                    if (!pictureBox3.Visible)
                    {
                        pictureBox3.BringToFront();
                        pictureBox3.Show();
                    }
                }
            }
            catch { }
        }

        private void picLarge_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Visible)
            {
                pictureBox3.SendToBack();
                pictureBox3.Hide();
            }
        }
    }
}