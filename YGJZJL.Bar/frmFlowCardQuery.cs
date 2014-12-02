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

namespace YGJZJL.Bar
{
    public partial class frmFlowCardQuery : FrmBase
    {
        private string _STOCK = "";

        public frmFlowCardQuery()
        {
            InitializeComponent();
        }

        private void frmFlowCardQuery_Load(object sender, EventArgs e)
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
                    else if (strKey.Substring(0, 2).Equals("LG"))
                    {
                        this._STOCK = "SH000166";
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
                this.ultraToolbarsManager1.Toolbars[0].Tools[strKey].SharedProps.Caption = strCaption;
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

                strWhere += Convert.ToString("   and t.fd_plantime between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryStoveNo.Text.Trim()))
            {
                strWhere += Convert.ToString("   and t.fs_stoveno like '%" + tbQueryStoveNo.Text.Trim() + "%'").Trim() + " ";
            }

            string strSql = "";
            strSql += Convert.ToString("select t.fs_techcardno fs_cardno,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t1.fd_smeltdate, 'yyyy-MM-dd hh24:mi') fd_smeltdate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_stoveno fs_gp_stoveno,").Trim() + " ";
            strSql += Convert.ToString("       t1.FS_GP_STEELTYPE fs_gp_steeltype,").Trim() + " ";
            strSql += Convert.ToString("       t1.FS_GP_SPE fs_gp_spe,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_length, 3) fn_length,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t1.fs_gp_flow, t.fs_receiver),'SH000098','棒材厂','SH000100','高线厂','SH000120','型材厂','SH000166','炼钢落地',t1.fs_gp_flow) fs_gp_flow,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_c,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_si,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_mn,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_s,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_p,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_ni,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_cr,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_cu,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_v,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_mo,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_ceq,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_as,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_ti,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_sb,").Trim() + " ";
            strSql += Convert.ToString("       t1.fn_gp_als,").Trim() + " ";
            strSql += Convert.ToString("       nvl(t2.fn_gp_totalcount, t1.FN_GP_TOTALCOUNT) fn_gp_totalcount,").Trim() + " ";
            strSql += Convert.ToString("       case when t2.fn_gp_totalcount is not null then round(round(t.fn_length *").Trim() + " ";
            strSql += Convert.ToString("                                  0.21,3) * t2.fn_gp_totalcount,").Trim() + " ";
            strSql += Convert.ToString("             3) else round(round(0.21*t.fn_length,3)*t.fn_count,3) end fn_ll_weight,").Trim() + " ";
            strSql += Convert.ToString("       t2.fn_gp_totalcount fn_gp_checkcount,").Trim() + " ";
            strSql += Convert.ToString("       nvl(round(t2.fn_jj_weight, 3), round(round(0.21*t.fn_length,3)*t.fn_count,3)) fn_jj_weight,").Trim() + " ";
            strSql += Convert.ToString("       t1.fs_gp_memo,").Trim() + " ";
            strSql += Convert.ToString("       t1.fs_gp_judger,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t1.fd_gp_judgedate, 'yyyy-MM-dd hh24:mi:ss') fd_gp_judgedate").Trim() + " ";
            strSql += Convert.ToString("  from dt_fp_plan t,").Trim() + " ";
            strSql += Convert.ToString("       it_fp_techcard t1,").Trim() + " ";
            strSql += Convert.ToString("       (select x.fs_stoveno,").Trim() + " ";
            strSql += Convert.ToString("               count(1) fn_gp_totalcount,").Trim() + " ";
            strSql += Convert.ToString("               sum(nvl(x.fn_netweight, 0)) fn_jj_weight").Trim() + " ";
            strSql += Convert.ToString("          from dt_steelweightdetailroll x").Trim() + " ";
            strSql += Convert.ToString("         where exists").Trim() + " ";
            strSql += Convert.ToString("         (select 1").Trim() + " ";
            strSql += Convert.ToString("                  from dt_fp_plan t").Trim() + " ";
            strSql += Convert.ToString("                 where t.fs_stoveno = x.fs_stoveno " + strWhere + ")").Trim() + " ";
            strSql += Convert.ToString("         group by x.fs_stoveno) t2").Trim() + " ";

            if (string.IsNullOrEmpty(_STOCK))
            {
                strSql += Convert.ToString(" where 1 = 1").Trim() + " ";
            }
            else
            {
                strSql += Convert.ToString(" where nvl(t1.fs_gp_flow, t.fs_receiver) = '" + _STOCK + "'").Trim() + " ";
            }
            
            strSql += Convert.ToString("   and t.fs_stoveno = t1.fs_gp_stoveno(+)").Trim() + " ";
            //strSql += Convert.ToString("   and t.fs_techcardno = t1.fs_cardno(+)").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_stoveno = t2.fs_stoveno(+) " + strWhere).Trim() + " ";
            strSql += Convert.ToString(" order by substr(t.fs_stoveno, 4, 1), t.fs_stoveno desc").Trim();

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
                case "Export":
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