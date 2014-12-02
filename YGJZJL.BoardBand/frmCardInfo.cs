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

namespace YGJZJL.BoardBand
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
        }

        private void GetFlowCardInfo()
        {
            string strWhere = "";

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strWhere += Convert.ToString("   and A.FS_BATCH_OPTDATE between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi') ").Trim() + " ";
            }

            string ConditionName = "";

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && string.IsNullOrEmpty(tbBatchNoTo.Text.Trim()))
            {
                ConditionName = rbtn_BatchNo.Checked ? "a.fs_zc_batchno" : "a.fs_gp_stoveno";
                strWhere += Convert.ToString("   and " + ConditionName + " like '%" + tbQueryBatchNo.Text.Trim() + "%' ").Trim() + " ";
            }
            else if (!string.IsNullOrEmpty(tbBatchNoTo.Text.Trim()) && string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
            {
                ConditionName = rbtn_BatchNo.Checked ? "a.fs_zc_batchno" : "a.fs_gp_stoveno";
                strWhere += Convert.ToString("   and " + ConditionName + " like '%" + tbBatchNoTo.Text.Trim() + "%' ").Trim() + " ";
            }

            //if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && !string.IsNullOrEmpty(tbBatchNoTo.Text.Trim()))
            //{
            //    ConditionName = rbtn_BatchNo.Checked ? "a.fs_zc_batchno" : "a.fs_gp_stoveno";
            //    strWhere = " and t.fs_batch_optdate >= (select max(x.fs_batch_optdate) from it_fp_techcard x where " + ConditionName + " = '" + tbQueryBatchNo.Text.Trim() + "') ";
            //    strWhere += " and t.fs_batch_optdate <= (select max(x.fs_batch_optdate) from it_fp_techcard x where " + ConditionName + " = '" + tbBatchNoTo.Text.Trim() + "') ";
            //}

            string strSql = "";
            strSql += Convert.ToString("select a.FS_CARDNO,").Trim() + " ";
            strSql += Convert.ToString("       a.FS_GP_STOVENO,").Trim() + " ";
            strSql += Convert.ToString("       a.FS_GP_SPE,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_C,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_SI,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_MN,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_S,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_P,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_AS,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_TI,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_SB,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_GP_ALS,").Trim() + " ";
            strSql += Convert.ToString("       a.FN_JJ_WEIGHT,").Trim() + " ";
            strSql += Convert.ToString("       a.FS_GP_JUDGER,").Trim() + " ";
            strSql += Convert.ToString("       to_char(a.fn_gp_LEN,'99.000') as fn_gp_LEN ,").Trim() + " ";
            strSql += Convert.ToString("       e.FS_PERSON FS_BILLETPERSON,").Trim()+"";
            strSql += Convert.ToString("       to_char(e.FD_WEIGHTTIME,'yyyy-MM-dd hh24:mi:ss') FD_BILLETWEIGHTTIME,").Trim() + " ";
            strSql += Convert.ToString("       decode(e.FS_SHIFT,'0','常白','1','早','2','中','3','晚',e.FS_SHIFT) FS_BILLETSHIFT,").Trim()+" ";
            strSql += Convert.ToString("       decode(e.FS_TERM,'0','常白','1','甲','2','乙','3','丙',e.FS_TERM) FS_BILLETTERM,").Trim()+" ";

            strSql += Convert.ToString("       d.FS_PERSON FS_ZKDPERSON,").Trim() + "";
            strSql += Convert.ToString("       decode(d.FS_SHIFT,'0','常白','1','早','2','中','3','晚',d.FS_SHIFT) FS_ZKDSHIFT,").Trim() + " ";
            strSql += Convert.ToString("       decode(d.FS_TERM,'0','常白','1','甲','2','乙','3','丙',d.FS_TERM) FS_ZKDTERM,").Trim() + " ";

            strSql += Convert.ToString("       to_char(a.FD_GP_JUDGEDATE,'yyyy-MM-dd hh24:mi:ss') FD_GP_JUDGEDATE,").Trim() + " ";
            strSql += Convert.ToString("       a.FS_ZC_BATCHNO,").Trim() + " ";
            strSql += Convert.ToString("       to_char(e.FD_WEIGHTTIME,'yyyy-MM-dd hh24:mi:ss') FD_ZC_ENTERDATETIME,").Trim() + " ";
            strSql += Convert.ToString("       to_char(a.FS_BATCH_OPTDATE,'yyyy-MM-dd hh24:mi:ss') FS_BATCH_OPTDATE,").Trim() + " ";
            strSql += Convert.ToString("       a.FS_BATCH_OPTOR,").Trim() + " ";
            strSql += Convert.ToString("       decode(b.FN_ISRETURNBILLET,'1','返炉','0',decode(a.fs_transtype,'1','热送','2','冷送',a.fs_transtype)) fs_transtype,").Trim() + " ";
            strSql += Convert.ToString("       decode(b.FN_ISRETURNBILLET,'0','否','1','是',b.FN_ISRETURNBILLET) FN_ISRETURNBILLET,").Trim() + " ";
            strSql += Convert.ToString("       decode(c.fs_billetstatus,'0','成卷','1','精轧废','2','卷取废','3','中间坯',c.fs_billetstatus) fs_billetstatus,").Trim() + " ";
            strSql += Convert.ToString("       d.FN_KHJZ AS FN_WEIGHT,").Trim() + " ";
            strSql += Convert.ToString("       to_char(d.FD_DATETIME,'yyyy-MM-dd hh24:mi:ss') FD_DATETIME,").Trim() + " ";
            strSql += Convert.ToString("       d.FS_REEL,").Trim() + " ";
            strSql += Convert.ToString("       a.FS_ADVISESPEC,").Trim() + " ";
            strSql += Convert.ToString("       d.FN_BANDNO,d.FS_SPEC,d.FS_STEELTYPE ").Trim() + " ";
            strSql += Convert.ToString("  from it_fp_techcard             a,").Trim() + " ";
            strSql += Convert.ToString("       dt_bp_plan                 b,").Trim() + " ";
            strSql += Convert.ToString("       dt_zkd_plan                c,").Trim() + " ";
            strSql += Convert.ToString("       dt_zkd_productweightdetail d,dt_boardweightmain e ").Trim() + " ";
            strSql += Convert.ToString("  where a.FS_CARDNO like 'BP10%' ").Trim() + " ";
            strSql += Convert.ToString("  and a.fs_gp_stoveno=b.fs_stoveno(+) ").Trim() + " ";
            strSql += Convert.ToString("  and a.fs_gp_stoveno=c.FS_STOVENO(+) ").Trim() + " ";
            strSql += Convert.ToString("  and a.fs_gp_stoveno=d.fs_stoveno(+) ").Trim();
            strSql += Convert.ToString("  and a.fs_gp_stoveno=e.fs_stoveno(+) ").Trim();

            strSql += strWhere;
            strSql += " order by a.fs_zc_batchno asc, e.FS_ORDER asc";
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

            //if (_STOCK.Equals("SH000098") || _STOCK.Equals("SH000120"))
            //{
            //    ultraGrid1.DisplayLayout.Bands[0].Columns["FN_ZZ_QUALIFIED_TRATE"].Hidden = true;
            //}

            MarkupRows();
        }

        private void MarkupRows()
        {
            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToString(Convert.ToString(ultraGrid1.Rows[i].Cells["FN_ISRETURNBILLET"].Value)) == "是")
                    {
                        ultraGrid1.Rows[i].Appearance.ForeColor = Color.Red;
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
       
    }
}