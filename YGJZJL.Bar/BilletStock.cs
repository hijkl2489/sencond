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

namespace YGJZJL.Bar
{
    public partial class BilletStock : FrmBase
    {
        private string _STOCK = "";

        public BilletStock()
        {
            InitializeComponent();
        }

        private void BilletStock_Load(object sender, EventArgs e)
        {
            CommonMethod.RefreshAndAutoSize(ultraGrid1);

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
                        this._STOCK = "SH000120";                                                  //型材未知
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void GetStockInfo(string strCardNo)
        {
            //PL/SQL Special Copy
            string strSql = "";
            strSql += Convert.ToString("select 'false' checked,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_cardno,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_smeltdate, 'yyyy-MM-dd hh24:mi:ss') fd_smeltdate,").Trim() + " ";
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
            strSql += Convert.ToString("       round(t.fn_jj_weight, 3) fn_jj_weight,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_gp_totalcount * round(decode(substr(t.fs_gp_stoveno, 4, 1), '1', 0.174, '2', 0.174, '3', 0.176, 0.174) * t.fn_gp_len, 3), 3) fn_jj_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_memo,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gp_judger,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gp_judgedate, 'yyyy-MM-dd hh24:mi:ss') fd_gp_judgedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_gpys_number,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_djh,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_gpys_receivedate, 'yyyy-MM-dd hh24:mi:ss') fd_gpys_receivedate,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_gpys_receiver,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fs_checked, '0'), '0', '', '√') fs_checked,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_gpys_weight, 3) fn_gpys_weight,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_gpys_number * round(decode(substr(t.fs_gp_stoveno, 4, 1), '1', 0.174, '2', 0.174, '3', 0.176, 0.174) * t.fn_gp_len, 3), 3) fn_gpys_weight_ll,").Trim() + " ";
            strSql += Convert.ToString("       round(t.fn_gp_len, 2) fn_gp_len,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_advisespec,").Trim() + " ";
            strSql += "decode(t.FS_UNQUALIFIED,'0','√','1','×','√') FS_UNQUALIFIED ";
            strSql += Convert.ToString("  from it_fp_techcard t").Trim() + " ";
            strSql += Convert.ToString(" where 1 = 1").Trim() + " ";
            strSql += Convert.ToString("   and case when t.fs_transtype = '1' or t.fs_transtype = '2' then nvl(t.fs_gp_completeflag, '0') when t.fs_transtype = '3' then '1' else '0' end = '1'").Trim() + " ";
            strSql += Convert.ToString("   and case when t.fs_transtype = '1' or t.fs_transtype = '2' then nvl(t.fn_jj_weight, 0) when t.fs_transtype = '3' then 1 else 0 end > 0").Trim() + " ";
            strSql += Convert.ToString("   and nvl(t.fs_batched, '0') = '0' and t.fs_zc_batchno is null").Trim() + " ";
            strSql += Convert.ToString("   and t.fs_gp_flow = '" + this._STOCK + "'").Trim() + " ";
            strSql += Convert.ToString("   and nvl(t.fs_isvalid, '0') = '0'").Trim() + " ";

            if (!string.IsNullOrEmpty(tbQueryButtressNo.Text.Trim()))
            {
                strSql += Convert.ToString("   and t.fs_djh like '%" + tbQueryButtressNo.Text.Trim() + "%'").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryStoveNo.Text.Trim()))
            {
                strSql += Convert.ToString("   and t.fs_gp_stoveno like '%" + tbQueryStoveNo.Text.Trim() + "%'").Trim() + " ";
            }

            strSql += Convert.ToString(" order by t.fs_gp_stoveno desc").Trim();

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

            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }

            //if (string.IsNullOrEmpty(strCardNo))
               // return;

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

        private bool DataValidation_Save(out ArrayList alistCardNo, out ArrayList alistCount, out  ArrayList alistWeight, out ArrayList alistPosition,out ArrayList alistStoveNo)
        {
            alistStoveNo = new ArrayList();
            alistCardNo = new ArrayList();
            alistCount = new ArrayList();
            alistWeight = new ArrayList();
            alistPosition = new ArrayList();

            ultraGrid1.UpdateData();

            try
            {
                UltraGridRow row = null;
                string strCardNo = "", strCount = "", strWeight = "", strPosition = "", strStoveNo = "";

                for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
                {
                    row = this.ultraGrid1.Rows[i];

                    if (Convert.ToBoolean(row.Cells["CHECKED"].Value))
                    {
                        strCardNo = Convert.ToString(row.Cells["FS_CARDNO"].Value).Trim();
                        strCount = Convert.ToString(row.Cells["FN_GPYS_NUMBER"].Value).Trim();
                        strWeight = Convert.ToString(row.Cells["FN_GPYS_WEIGHT"].Value).Trim();
                        strPosition = Convert.ToString(row.Cells["FS_DJH"].Value).Trim();
                        strStoveNo = Convert.ToString(row.Cells["FS_GP_STOVENO"].Value).Trim();

                        if (string.IsNullOrEmpty(strCount))
                        {
                            MessageBox.Show("请输入验收条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_NUMBER", true);
                            return false;
                        }

                        int iCount = 0;
                        bool bOK = false;

                        bOK = int.TryParse(strCount, out iCount);

                        if (!bOK)
                        {
                            MessageBox.Show("验收条数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_NUMBER", true);
                            return false;
                        }

                        if (iCount <= 0)
                        {
                            MessageBox.Show("验收条数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_NUMBER", true);
                            return false;
                        }

                        if (string.IsNullOrEmpty(strWeight))
                        {
                            MessageBox.Show("请输入验收重量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_WEIGHT", true);
                            return false;
                        }

                        decimal dWeight = 0.0M;
                        bOK = decimal.TryParse(strWeight, out dWeight);

                        if (!bOK)
                        {
                            MessageBox.Show("验收重量必须是数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_WEIGHT", true);
                            return false;
                        }

                        if (dWeight <= 0)
                        {
                            MessageBox.Show("验收重量必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FN_GPYS_WEIGHT", true);
                            return false;
                        }

                        if (string.IsNullOrEmpty(strPosition))
                        {
                            MessageBox.Show("请输入货架号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FS_DJH", true);
                            return false;
                        }

                        alistCardNo.Add(strCardNo);
                        alistCount.Add(strCount);
                        alistWeight.Add(strWeight);
                        alistPosition.Add(strPosition);
                        alistStoveNo.Add(strStoveNo);
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

        private bool DataValidation_Cancel(out ArrayList alistCardNo)
        {
            alistCardNo = new ArrayList();

            ultraGrid1.UpdateData();

            try
            {
                for (int i = 0; i < this.ultraGrid1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(this.ultraGrid1.Rows[i].Cells["CHECKED"].Value))
                    {
                        alistCardNo.Add(Convert.ToString(this.ultraGrid1.Rows[i].Cells["FS_CARDNO"].Value).Trim());
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

        private string[] GetStoveArray(ArrayList alist)
        {
            if (alist == null || alist.Count == 0)
            {
                return new string[] { };
            }

            int iCount = alist.Count;
            string[] Results = new string[iCount];

            for (int i = 0; i < alist.Count; i++)
            {
                Results[i] = alist[i].ToString();
            }

            return Results;
        }

        private void Save()
        {
            try
            {
                if (ultraGrid1.Rows.Count <= 0)
                {
                    return;
                }
                ArrayList alistCardNo = new ArrayList();
                ArrayList alistCount = new ArrayList();
                ArrayList alistWeight = new ArrayList();
                ArrayList alistPosition = new ArrayList();
                ArrayList alistStoveNo = new ArrayList();

                bool bOK = DataValidation_Save(out alistCardNo, out alistCount, out alistWeight, out alistPosition, out alistStoveNo);

                if (!bOK)
                    return;

                if (alistCardNo.Count == 0)
                {
                    MessageBox.Show("请选择需要验收的炉号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                object[] sArgs;
                string strProcedure = "", strErr = "";
                ArrayList obj = null;

                Hashtable htblValue = new Hashtable();

                sArgs = new object[] { alistCardNo, alistCount, alistWeight, alistPosition, this.UserInfo.GetUserName() };

                strProcedure = "KG_MCMS_FLOWCARD.StockBilletReceive";
                obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                /*if (int.Parse(obj[2].ToString()) > 0)
                {
                    //加入验收后获取最终成分功能，调用甘俊的代码
                    try
                    {
                        string[] Stoves = GetStoveArray(alistStoveNo);
                        CoreClientParam ccp = new CoreClientParam();
                        ccp.ServerName = "core.mcms.billetTransfer.WeightPlan";
                        ccp.MethodName = "updateJudgeElements";
                        ccp.ServerParams = new object[] { Stoves };
                        CoreClientParam ret = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    }
                    catch { }

                    this.GetStockInfo(alistCardNo[0].ToString());
                    MessageBox.Show("数据保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }*/

                this.GetStockInfo(alistCardNo[0].ToString());
                MessageBox.Show("验收成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

                ArrayList alistCardNo = new ArrayList();

                bool bOK = DataValidation_Cancel(out alistCardNo);

                if (!bOK)
                    return;

                if (alistCardNo.Count == 0)
                {
                    MessageBox.Show("请选择需要撤销验收的炉号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                object[] sArgs;
                string strProcedure = "", strErr = "";
                ArrayList obj = null;

                sArgs = new object[] { alistCardNo, this.UserInfo.GetUserName() };
                strProcedure = "KG_MCMS_FLOWCARD.StockBilletReceive_Cancel";
                obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                if (int.Parse(obj[2].ToString()) > 0)
                {
                    this.GetStockInfo(alistCardNo[0].ToString());
                    MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        this.GetStockInfo("");
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
        }

        private void ultraGrid1_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key.Equals("CHECKED") || e.Cell.Column.Key.Equals("FN_GPYS_NUMBER") || 
                    e.Cell.Column.Key.Equals("FN_GPYS_WEIGHT") || e.Cell.Column.Key.Equals("FS_DJH"))
                {
                    this.ultraGrid1.UpdateData();
                }
            }
            catch { }
        }

        private void ultraGrid1_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column.Key.Equals("CHECKED"))
                {
                    if (Convert.ToBoolean(e.Cell.Value))
                    {
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_NUMBER"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GP_TOTALCOUNT"].Value;
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT"].Value;
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT_LL"].Value;

                        string strWeight = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value).Trim();

                        if (string.IsNullOrEmpty(strWeight) || strWeight == "0")
                        {
                            this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value;
                        }

                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, e.Cell.Row.Index, "FN_GPYS_NUMBER", true);
                    }
                    else
                    {
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_NUMBER"].Value = "";
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = ""; ;
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = "";
                    }
                }
                else if (e.Cell.Column.Key.Equals("FS_DJH") || e.Cell.Column.Key.Equals("FN_GPYS_WEIGHT"))
                {
                    string strValue = Convert.ToString(e.Cell.Value).Trim();

                    if (!Convert.ToBoolean(ultraGrid1.Rows[e.Cell.Row.Index].Cells["CHECKED"].Value) && !string.IsNullOrEmpty(strValue))
                    {
                        e.Cell.Value = "";
                        return;
                    }
                }
                else if (e.Cell.Column.Key.Equals("FN_GPYS_NUMBER"))
                {
                    string strCount = Convert.ToString(e.Cell.Value).Trim();

                    if (!Convert.ToBoolean(ultraGrid1.Rows[e.Cell.Row.Index].Cells["CHECKED"].Value) && !string.IsNullOrEmpty(strCount))
                    {
                        e.Cell.Value = "";
                        return;
                    }

                    string strCountLG = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GP_TOTALCOUNT"].Value).Trim();

                    if (string.IsNullOrEmpty(strCount))
                    {
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = "";
                        this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = "";
                        return;
                    }

                    int iCount = 0;
                    int iCountLG = Convert.ToInt16(strCountLG);

                    if (!int.TryParse(strCount, out iCount))
                    {
                        MessageBox.Show("验收数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountLG.ToString();
                        return;
                    }

                    if (iCount <= 0)
                    {
                        MessageBox.Show("验收数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountLG.ToString();
                        return;
                    }

                    if (iCount > iCountLG)
                    {
                        MessageBox.Show("验收数不能大于总条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        e.Cell.Value = iCountLG.ToString();
                        return;
                    }

                    try
                    {
                        string strWeightLG = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT"].Value).Trim();

                        if (!string.IsNullOrEmpty(strWeightLG))
                        {
                            decimal dWeightYS = Convert.ToDecimal(strWeightLG);

                            decimal dWeight = Math.Round(iCount * dWeightYS / iCountLG, 3);

                            this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = dWeight.ToString();
                        }
                    }
                    catch { }

                    try
                    {
                        string strWeightLGLL = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_JJ_WEIGHT_LL"].Value).Trim();

                        if (!string.IsNullOrEmpty(strWeightLGLL))
                        {
                            decimal dWeightYS = Convert.ToDecimal(strWeightLGLL);

                            decimal dWeightLL = Math.Round(iCount * dWeightYS / iCountLG, 3);

                            this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT_LL"].Value = dWeightLL.ToString();

                            string strWeightSZ = Convert.ToString(this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value).Trim();

                            if (string.IsNullOrEmpty(strWeightSZ) || strWeightSZ == "0")
                            {
                                this.ultraGrid1.Rows[e.Cell.Row.Index].Cells["FN_GPYS_WEIGHT"].Value = dWeightLL.ToString();
                            }
                        }
                    }
                    catch { }

                    

                    
                }
            }
            catch { }
        }

    }
}