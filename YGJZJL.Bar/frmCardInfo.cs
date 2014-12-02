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
    public partial class frmCardInfo : FrmBase
    {
        private string _STOCK = "";
        private UltraGridExcelExporter ultraGridExcelExporter1 = new UltraGridExcelExporter();

        public frmCardInfo()
        {
            InitializeComponent();
        }

        private void frmCardInfo_Load(object sender, EventArgs e)
        {
            //CommonMethod.RefreshAndAutoSize(ultraGrid1);

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

                if (!string.IsNullOrEmpty(strKey) && strKey.Length == 2)
                {
                    if (strKey.Substring(0, 2).Equals("GX"))
                    {
                        this._STOCK = "SH000100";
                    }
                    else if (strKey.Substring(0, 2).Equals("BC"))
                    {
                        this._STOCK = "SH000098";
                    }
                    else if (strKey.Substring(0, 2).Equals("XC"))
                    {
                        this._STOCK = "SH000120";
                    }
                }
            }
            catch { }
        }

        private void GetFlowCardInfo()
        {
            string strWhere = "";

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strWhere += Convert.ToString("   and t.fs_batch_optdate between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            string ConditionName = "";

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && string.IsNullOrEmpty(tbBatchNoTo.Text.Trim()))
            {
                ConditionName = rbtn_BatchNo.Checked ? "t.fs_zc_batchno" : "t.fs_gp_stoveno";
                strWhere += Convert.ToString("   and " + ConditionName + " like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
            }
            else if (!string.IsNullOrEmpty(tbBatchNoTo.Text.Trim()) && string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
            {
                ConditionName = rbtn_BatchNo.Checked ? "t.fs_zc_batchno" : "t.fs_gp_stoveno";
                strWhere += Convert.ToString("   and " + ConditionName + " like '%" + tbBatchNoTo.Text.Trim() + "%'").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && !string.IsNullOrEmpty(tbBatchNoTo.Text.Trim()))
            {
                ConditionName = rbtn_BatchNo.Checked ? "x.fs_zc_batchno" : "x.fs_gp_stoveno";
                strWhere = " and t.fs_batch_optdate >= (select max(x.fs_batch_optdate) from it_fp_techcard x where " + ConditionName + " = '" + tbQueryBatchNo.Text.Trim() + "') ";
                strWhere += " and t.fs_batch_optdate <= (select max(x.fs_batch_optdate) from it_fp_techcard x where " + ConditionName + " = '" + tbBatchNoTo.Text.Trim() + "') ";
            }

            string strSql = "";
            strSql += Convert.ToString("select 'false' checked,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_cardno,").Trim() + " ";
            strSql += Convert.ToString("       t.fd_smeltdate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_stoveno,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_c,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_si,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_mn,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_s,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_p,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_ni,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gp_nb,").Trim() + " ";
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
            strSql += Convert.ToString("       round(round(t.fn_gp_len * 0.21, 3) * decode(nvl(t.fn_gp_checkcount, 0), 0, nvl(t.fn_gp_totalcount, 0), nvl(t.fn_gp_checkcount, 0)), 3)  fn_ll_weight,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fn_gp_checkcount, 0), 0, nvl(t.fn_gp_totalcount, 0), nvl(t.fn_gp_checkcount, 0)) fn_gp_checkcount,").Trim() + " ";
            strSql += Convert.ToString("       round(decode(nvl(t.fn_gp_totalcount, 0), 0, '', (decode(nvl(t.fn_gp_checkcount, 0), 0, nvl(t.fn_gp_totalcount, 0), nvl(t.fn_gp_checkcount, 0))*nvl(t.fn_jj_weight, 0)/nvl(t.fn_gp_totalcount,0))), 3) fn_jj_weight,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_judger,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gp_judgedate, 'yyyy-MM-dd hh24:mi:ss') fd_gp_judgedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gpys_number,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_djh,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gpys_receivedate, 'yyyy-MM-dd hh24:mi:ss') fd_gpys_receivedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gpys_receiver,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zc_batchno,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zc_enternumber,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_zc_enterdatetime, 'yyyy-MM-dd hh24:mi:ss') fd_zc_enterdatetime,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zc_operator,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zc_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zz_spec,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_length,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_zz_date, 'yyyy-MM-dd hh24:mi:ss') fd_zz_date,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zz_operator,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zz_num,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_zz_wastnum,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_zz_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_flow,").Trim() + " ";
            strSql += Convert.ToString("       nvl(t.fs_discharge_begined, '0') fs_discharge_begined,").Trim() + " ";
            strSql += Convert.ToString("       nvl(t.fs_discharge_end, '0') fs_discharge_end,").Trim() + " ";
            strSql += Convert.ToString("       decode(t.fs_freezed, '1', '冻结', '自由') fs_freezed,").Trim() + " ";
            strSql += Convert.ToString("       decode(t.fs_checked, '1', '√', '') fs_checked").Trim() + " ";

            if (_STOCK.Equals("SH000100"))
            {
                strSql += Convert.ToString("       ,(select sum(d.fn_weight) from dt_gx_storageweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0') FN_ZZ_QUALIFIED_WEIGHT").Trim() + " ";
                strSql += Convert.ToString("       ,(select sum(d.fn_weight) from dt_gx_storageweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '1') FN_ZZ_UNQUALIFIED_WEIGHT").Trim() + " ";
                strSql += Convert.ToString(",round((select sum(d.fn_weight) from dt_gx_storageweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0')").Trim() + " ";
                strSql += Convert.ToString(" /(select sum(round(decode(nvl(fn_gp_totalcount, 0),0,'',(decode(nvl(fn_gp_checkcount, 0),0,nvl(fn_gp_totalcount, 0),nvl(fn_gp_checkcount, 0))*nvl(fn_jj_weight, 0) / nvl(fn_gp_totalcount, 0))),3)) from it_fp_techcard where fs_zc_batchno = t.fs_zc_batchno),4)*100 FN_ZZ_QUALIFIED_RATE").Trim() + " ";
                strSql += Convert.ToString(" ,round((select sum(d.fn_weight) from dt_gx_storageweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0')").Trim() + " ";
                strSql += Convert.ToString(" /(select sum(round(round(fn_gp_len * 0.21, 3) * decode(nvl(fn_gp_checkcount, 0),0,nvl(fn_gp_totalcount, 0),nvl(fn_gp_checkcount, 0)),3)) from it_fp_techcard where fs_zc_batchno = t.fs_zc_batchno),4)*100 FN_ZZ_QUALIFIED_TRATE").Trim() + " ";
            }
            else if (_STOCK.Equals("SH000098"))
            {
                strSql += Convert.ToString("       ,(select sum(d.fn_weight) from dt_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0') FN_ZZ_QUALIFIED_WEIGHT").Trim() + " ";
                strSql += Convert.ToString("       ,(select sum(d.fn_weight) from dt_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '1') FN_ZZ_UNQUALIFIED_WEIGHT").Trim() + " ";
                strSql += Convert.ToString(",round((select sum(d.FN_THEORYWEIGHT) from dt_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0')").Trim() + " ";
                strSql += Convert.ToString(" /(select sum(fn_jj_weight) from it_fp_techcard where fs_zc_batchno = t.fs_zc_batchno),4)*100 FN_ZZ_QUALIFIED_RATE").Trim() + " ";
                strSql += Convert.ToString(" ,round((select sum(d.FN_THEORYWEIGHT) from dt_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0')").Trim() + " ";
                strSql += Convert.ToString(" /(select sum(round(round(fn_gp_len * 0.21, 3) * decode(nvl(fn_gp_checkcount, 0),0,nvl(fn_gp_totalcount, 0),nvl(fn_gp_checkcount, 0)),3)) from it_fp_techcard where fs_zc_batchno = t.fs_zc_batchno),4)*100 FN_ZZ_QUALIFIED_TRATE").Trim() + " ";
            }
            else if (_STOCK.Equals("SH000120"))
            {
                strSql += Convert.ToString("       ,(select sum(d.fn_weight) from dt_xc_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0') FN_ZZ_QUALIFIED_WEIGHT").Trim() + " ";
                strSql += Convert.ToString("       ,(select sum(d.fn_weight) from dt_xc_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '1') FN_ZZ_UNQUALIFIED_WEIGHT").Trim() + ", ";
                strSql += Convert.ToString("round((select sum(d.FN_THEORYWEIGHT) from dt_xc_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0')").Trim() + " ";
                strSql += Convert.ToString(" /(select sum(fn_jj_weight) from it_fp_techcard where fs_zc_batchno = t.fs_zc_batchno),4)*100 FN_ZZ_QUALIFIED_RATE").Trim() + " ";
                strSql += Convert.ToString(" ,round((select sum(d.FN_THEORYWEIGHT) from dt_xc_productweightdetail d where d.fs_batchno = t.fs_zc_batchno and nvl(d.fs_unqualified,'0') = '0')").Trim() + " ";
                strSql += Convert.ToString(" /(select sum(round(round(fn_gp_len * 0.21, 3) * decode(nvl(fn_gp_checkcount, 0),0,nvl(fn_gp_totalcount, 0),nvl(fn_gp_checkcount, 0)),3)) from it_fp_techcard where fs_zc_batchno = t.fs_zc_batchno),4)*100 FN_ZZ_QUALIFIED_TRATE").Trim() + " ";
            }

            strSql += Convert.ToString("  from it_fp_techcard t").Trim() + " ";
            strSql += Convert.ToString(" where t.fs_gp_flow = '" + _STOCK + "' and nvl(t.fs_isvalid, '0') = '0' and t.fs_batched = '1' and t.fs_zc_batchno is not null").Trim() + " ";
            strSql += Convert.ToString("   and case when t.fs_transtype = '1' or t.fs_transtype = '2' then nvl(t.fs_gp_completeflag, '0') when t.fs_transtype = '3' then '1' else '0' end = '1'").Trim() + " ";
            strSql += Convert.ToString("   and case when t.fs_transtype = '1' or t.fs_transtype = '2' then nvl(t.fn_jj_weight, 0) when t.fs_transtype = '3' then 1 else 0 end > 0").Trim() + " ";
            strSql += " " + strWhere + " ";
            strSql += Convert.ToString("   order by t.fs_zc_batchno desc, t.fs_gp_stoveno").Trim() + " ";

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

            //CommonMethod.RefreshAndAutoSize(ultraGrid1);

            if (_STOCK.Equals("SH000098") || _STOCK.Equals("SH000120"))
            {
                ultraGrid1.DisplayLayout.Bands[0].Columns["FN_ZZ_QUALIFIED_TRATE"].Hidden = true;
            }

            MarkupRows();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    {
                        this.GetFlowCardInfo();
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

        private void cbx_Filter_CheckedChanged(object sender, EventArgs e)
        {
            CommonMethod.SetUltraGridRowFilter(ref ultraGrid1, cbx_Filter.Checked);
        }

        private void MarkupRows()
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

        public class CustomMergedCellEvaluatorWithBatchNo : Infragistics.Win.UltraWinGrid.IMergedCellEvaluator
        {
            public CustomMergedCellEvaluatorWithBatchNo() { }
            // 合并相同的单元格
            public bool ShouldCellsBeMerged(Infragistics.Win.UltraWinGrid.UltraGridRow row1, Infragistics.Win.UltraWinGrid.UltraGridRow row2, Infragistics.Win.UltraWinGrid.UltraGridColumn column)
            {
                string process1 = row1.GetCellValue("FS_ZC_BATCHNO") != null ? row1.GetCellValue("FS_ZC_BATCHNO").ToString() : "";
                string process2 = row2.GetCellValue("FS_ZC_BATCHNO") != null ? row2.GetCellValue("FS_ZC_BATCHNO").ToString() : "";
                return process1 == process2 && !process1.Equals("轧制编号");
            }
        }

        private void ultraGrid1_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            e.Layout.Bands[0].Columns["FN_ZZ_QUALIFIED_WEIGHT"].MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Always;
            e.Layout.Bands[0].Columns["FN_ZZ_QUALIFIED_WEIGHT"].MergedCellEvaluator = new CustomMergedCellEvaluatorWithBatchNo();
            e.Layout.Bands[0].Columns["FN_ZZ_UNQUALIFIED_WEIGHT"].MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Always;
            e.Layout.Bands[0].Columns["FN_ZZ_UNQUALIFIED_WEIGHT"].MergedCellEvaluator = new CustomMergedCellEvaluatorWithBatchNo();
            e.Layout.Bands[0].Columns["FN_ZZ_QUALIFIED_RATE"].MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Always;
            e.Layout.Bands[0].Columns["FN_ZZ_QUALIFIED_RATE"].MergedCellEvaluator = new CustomMergedCellEvaluatorWithBatchNo();
            e.Layout.Bands[0].Columns["FN_ZZ_QUALIFIED_TRATE"].MergedCellStyle = Infragistics.Win.UltraWinGrid.MergedCellStyle.Always;
            e.Layout.Bands[0].Columns["FN_ZZ_QUALIFIED_TRATE"].MergedCellEvaluator = new CustomMergedCellEvaluatorWithBatchNo();
        }
    }
}