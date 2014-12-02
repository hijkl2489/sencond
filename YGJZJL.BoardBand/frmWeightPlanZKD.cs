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
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.BoardBand
{
    public partial class frmWeightPlanZKD : FrmBase
    {
        public frmWeightPlanZKD()
        {
            InitializeComponent();
        }
        
        private void frmWeightPlanZKD_Load(object sender, EventArgs e)
        {
            CommonMethod.RefreshAndAutoSize(ultraGrid1);

            try
            {
                dateTimePicker1.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
                BindPoint();
                BindBilletStatus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void GetFlowCardInfo(string strCardNo)
        {

            string strWhere = string.Empty;
            if (this.cbxDateTime.Checked)
            {
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("查询结束时间不能小于开始时间！");
                    return;
                }

                string startTime = dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00");
                string endTime = dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59");
                strWhere += " and a.fs_batch_optdate between  to_date('" + startTime + "','yyyy-MM-dd HH24:mi:ss') and to_date('" + endTime + "','yyyy-MM-dd HH24:mi:ss') ";
            }

            if (this.tbQueryBatchNo.Text != string.Empty)
            {
                strWhere += " and a.fs_zc_batchno like '%" + tbQueryBatchNo.Text.Trim() + "%' ";
            }

            if (tbQueryStoveNo.Text != string.Empty)
            {
                strWhere += " and a.FS_GP_STOVENO like '%" + tbQueryStoveNo.Text.Trim() + "%' ";
            }

            string strSql = "";
            strSql += Convert.ToString("select a.FS_CARDNO,'false' checked,").Trim() + " ";
            strSql += Convert.ToString("a.FS_GP_STOVENO,").Trim() + " ";
            strSql += Convert.ToString("a.FS_GP_STEELTYPE,").Trim() + " ";
            strSql += Convert.ToString("a.FS_GP_SPE,").Trim() + " ";
            strSql += Convert.ToString("nvl(e.fn_weight,0) FN_JJ_WEIGHT,").Trim() + " ";
            strSql += Convert.ToString("a.FS_GP_MEMO,").Trim() + " ";
            strSql += Convert.ToString("a.FS_GP_JUDGER,").Trim() + " ";
            strSql += Convert.ToString("to_char(e.FD_WEIGHTTIME, 'yyyy-MM-dd HH24:mi:ss') FD_ZC_ENTERDATETIME,").Trim() + " ";
            strSql += Convert.ToString("to_char(a.FD_GP_JUDGEDATE, 'yyyy-MM-dd HH24:mi:ss') FD_GP_JUDGEDATE,").Trim() + " ";
            strSql += Convert.ToString("a.FS_ZC_BATCHNO,").Trim() + " ";
            strSql += Convert.ToString("b.FS_REEL,").Trim() + " ";
            strSql += Convert.ToString("b.FS_STEELTYPE,").Trim() + " ";
            strSql += Convert.ToString("b.FS_SPEC,").Trim() + " ";
            strSql += Convert.ToString("b.FS_PRODUCTNO,").Trim() + " ";
            strSql += Convert.ToString("decode(b.fs_billetstatus,'0','成卷','1','精轧废','2','卷取废','3','中间坯',b.fs_billetstatus) fs_billetstatus,").Trim() + " ";
            strSql += Convert.ToString("b.FS_ORDER,").Trim() + " ";
            strSql += Convert.ToString("decode(b.FS_COMPLETEFLAG,'1','√','') FS_COMPLETEFLAG,").Trim() + " ";
            strSql += Convert.ToString("c.FN_WEIGHT,").Trim() + " ";
            strSql += Convert.ToString("decode(b.fs_point,'K23','卷取秤1#','K24','卷取秤2#','K25','地上衡',b.fs_point) fs_point,").Trim() + " ";
            strSql += Convert.ToString("to_char(b.FD_PLANTIME, 'yyyy-MM-dd HH24:mi:ss') FD_PLANTIME,").Trim() + " ";
            strSql += Convert.ToString("b.FS_PERSON,").Trim() + " ";
            strSql += Convert.ToString("a.FS_ADVISESPEC,").Trim() + " ";
            strSql += Convert.ToString("       decode(d.FN_ISRETURNBILLET,'1','返炉','0',decode(a.fs_transtype,'1','热送','2','冷送',a.fs_transtype)) fs_transtype").Trim() + " ";
            strSql += Convert.ToString("from it_fp_techcard a,dt_zkd_plan b,dt_zkd_productweightdetail c,dt_bp_plan d,dt_boardweightmain e ").Trim() + " ";
            strSql += Convert.ToString("where a.FS_CARDNO like 'BP10%'").Trim() + " ";
            strSql += Convert.ToString(" and a.fs_batched='1' and a.fs_zc_batchno is not null").Trim() + " ";
            strSql += Convert.ToString(" and a.fs_gp_stoveno=b.fs_stoveno(+)").Trim() + " ";
            strSql += Convert.ToString(" and a.fs_gp_stoveno=c.FS_STOVENO(+)").Trim() + " ";
            strSql += Convert.ToString(" and a.fs_gp_stoveno=d.fs_stoveno(+)").Trim() + " ";
            strSql += Convert.ToString(" and a.fs_gp_stoveno=e.fs_stoveno(+)").Trim() + " ";
            //strSql += Convert.ToString(" and (d.FN_ISRETURNBILLET='0' or (d.FN_ISRETURNBILLET='1' and d.fs_completeflag='1'))").Trim() + " ";
            strSql += strWhere;
            strSql += Convert.ToString(" order by a.fs_zc_batchno asc,e.fs_order asc").Trim() + " ";


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
                //ucBilletFlowCard1.ResetData();
            }
            else
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }

            //if (string.IsNullOrEmpty(strCardNo))
            //return;
        }

        private void MarkupRows()
        {
            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToString(Convert.ToString(ultraGrid1.Rows[i].Cells["fs_transtype"].Value)) == "返炉")
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
                        this.GetFlowCardInfo("");
                        break;
                    }
                case "置为返回坯":
                    {
                        if (ultraGrid1.ActiveRow != null)
                        {
                            DialogResult result = MessageBox.Show(this, "是否将炉号：" + ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Text.ToString() + "置为返回坯？", "提示", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                setStoveAsReturnBillet(ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Text.ToString());
                                
                                this.GetFlowCardInfo("");
                            }
                        }
                        else
                        {
                            MessageBox.Show("请先选择要置为返回坯的炉号！");
                        }

                        break;
                    }
                case "Export":
                    {
                        CommonMethod.ExportDataWithSaveDialog2(ref this.ultraGrid1, this.Text);
                        break;
                    }
            }
        }

        private void setStoveAsReturnBillet(string strStoveNo)
        {
            string strSQL = "select * from dt_zkd_plan where fs_stoveno='" + strStoveNo + "' and fs_completeflag = '1'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            DataTable dt1 = new DataTable();
            ccp.SourceDataTable = dt1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt1.Rows.Count > 0)
            {
                MessageBox.Show("炉号为：" + strStoveNo + "的卷已计量完成,不能置为返回坯");
                return;
            }

            strSQL = "delete dt_bp_plan where FS_STOVENO='" + strStoveNo + "'";
            
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { strSQL };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            strSQL = "update it_fp_techcard set FS_BATCHED='0',FS_BATCH_OPTOR=null,FS_BATCH_OPTDATE=null,fs_zc_batchno=null,";
            strSQL += "fs_batched_heatno=null,FS_GP_COMPLETEFLAG='0',";
            strSQL += "FS_SOURCE_HEATNO=null,FS_SYS_BATCHNO=null where FS_GP_STOVENO='" + strStoveNo + "'";

            ccp.ServerParams = new object[] { strSQL };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            strSQL = "update it_fp_techcard set fs_batched_heatno=replace(replace(fs_batched_heatno,'" + strStoveNo + ",',''),'," + strStoveNo + "','')";
            strSQL += " where fs_batched_heatno like '%" + strStoveNo + "%'";

            ccp.ServerParams = new object[] { strSQL };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);



            strSQL = "select fs_reel from dt_zkd_plan where fs_stoveno='" + strStoveNo + "'";
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            DataTable dt = new DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (dt.Rows.Count > 0)
            {
                string strReelTemp = dt.Rows[0]["fs_reel"].ToString();

                if (strReelTemp != string.Empty&&strReelTemp.Contains("-"))
                {
                    strSQL = "   update DT_ZKD_PLAN set FS_REEL = substr(FS_REEL, 1, 5) ||LPAD(NVL(TO_NUMBER(SUBSTR(FS_REEL, 6, 4)), 0) - 1,4,'0')";
                    strSQL += " where fs_reel like substr('" + strReelTemp + "', 1, 4) || '%' and NVL(TO_NUMBER(SUBSTR(FS_REEL, 6, 4)), 0) >=TO_NUMBER(SUBSTR('" + strReelTemp + "', 6, 4)) and fs_completeflag <> '1'";

                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteNonQuery";
                    ccp.ServerParams = new object[] { strSQL };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }

                strSQL = "delete dt_zkd_plan where fs_stoveno='" + strStoveNo + "'";

                ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                ccp.MethodName = "ExcuteNonQuery";
                ccp.ServerParams = new object[] { strSQL };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            }

            MessageBox.Show("炉号："+strStoveNo+" 置为返回坯成功!");
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
                    string strCardNo = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Value);

                    //DataRow[] Rows = dataTable1.Select("FS_GP_STOVENO = '" + strCardNo + "'");
                    this.txtBatchNo.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_ZC_BATCHNO"].Value);
                    this.txtStoveNo.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_GP_STOVENO"].Value);
                    if (Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Value) != string.Empty)
                    {
                        this.txtSteelType.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_STEELTYPE"].Value);
                    }
                    else
                    {
                        this.txtSteelType.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_GP_STEELTYPE"].Value);
                    }

                    this.txtSpec.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_SPEC"].Value);

                    this.cbPointID.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["fs_point"].Value);
                    if (Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_REEL"].Value) != string.Empty)
                    {
                        this.txtReel.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_REEL"].Value);
                    }
                    else
                    {
                        this.txtReel.Text = GetNextReel();
                    }
                    this.cbBilletStatus.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_BILLETSTATUS"].Value);
                    this.txtProductNo.Text = Convert.ToString(ultraGrid1.ActiveRow.Cells["FS_PRODUCTNO"].Value);
                }
            }
            catch { }
        }

        private string GetNextReel()
        {
            string strResult = string.Empty;
            string strSQL = "select substr(to_char(sysdate,'yyyymm'),3,4)||'-'||LPAD(NVL(TO_NUMBER(SUBSTR(MAX(FS_REEL),6,4)),0)+1,4,'0') as fs_reel from dt_zkd_plan ";
            strSQL += " where FS_REEL like substr(to_char(sysdate,'yyyymm'),3,4)||'%'";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { strSQL };
            DataTable dt = new DataTable();
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dt.Rows.Count > 0)
            {
                strResult = dt.Rows[0]["fs_reel"].ToString();
            }
            return strResult;
        }

        private void cbx_Filter_CheckedChanged(object sender, EventArgs e)
        {
            CommonMethod.SetUltraGridRowFilter(ref ultraGrid1, cbx_Filter.Checked);
        }

        private void BindPoint()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            dt.Rows.Add("K23", "卷取秤1#");
            dt.Rows.Add("K24", "卷取秤2#");
            dt.Rows.Add("K25", "地上衡");
            cbPointID.DataSource = dt;
            cbPointID.ValueMember = "Value";
            cbPointID.DisplayMember = "Text";
        }

        private void BindBilletStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            dt.Rows.Add("", "");
            dt.Rows.Add("0", "成卷");
            dt.Rows.Add("1", "精轧废");
            dt.Rows.Add("2", "卷取废");
            dt.Rows.Add("3", "中间坯");
            this.cbBilletStatus.DataSource = dt;
            cbBilletStatus.ValueMember = "Value";
            cbBilletStatus.DisplayMember = "Text";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ultraGrid1.ActiveRow == null)
            {
                MessageBox.Show("请先选择做预报记录！");
            }
            else
            {
                string strResult = Check();

                if (strResult != string.Empty)
                {
                    MessageBox.Show(strResult);
                    return;
                }

                Hashtable param = new Hashtable();
                param.Add("I1", txtBatchNo.Text);
                param.Add("I2", txtStoveNo.Text);
                param.Add("I3", txtSteelType.Text);
                param.Add("I4", txtSpec.Text);
                param.Add("I5", cbPointID.SelectedValue.ToString());
                param.Add("I6", txtReel.Text);
                param.Add("I7", cbBilletStatus.SelectedValue.ToString());
                param.Add("I8", txtProductNo.Text);
                param.Add("I9", this.UserInfo.GetUserName().ToString());
                param.Add("I10", this.UserInfo.GetUserOrder().ToString());
                param.Add("I11", this.UserInfo.GetUserGroup().ToString());
                CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_ZKDINFO.ADD_ZKD_PLAN(?,?,?,?,?,?,?,?,?,?,?)}", param);
                this.GetFlowCardInfo("");
            }
        }

        /// <summary>
        /// 执行存储过程2
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected CoreClientParam excuteProcedure2(string sql, Hashtable param)
        {
            if (param == null)
            {
                param = new Hashtable();
            }
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "executeProcedureBySql2";
            ccp.ServerParams = new object[] { sql, param };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            return ccp;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                flag = (ugr.Cells["checked"].Value.ToString() == "True");
                if (!flag)
                    continue;
                else
                    break;
            }

            if (flag)
            {
                if (MessageBox.Show("确定新增所选项预报？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    cb_selectAll.Checked = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择要删除预报的记录！");
                return;
            }

            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                if (ugr.Cells["checked"].Value.ToString() == "True")
                {
                    string strStoveNo = ugr.Cells["FS_GP_STOVENO"].Value.ToString();

                    string strSQL = "select * from dt_zkd_plan where fs_stoveno='" + strStoveNo + "' and fs_completeflag<>'1'";
                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                    ccp.MethodName = "ExcuteQuery";
                    ccp.ServerParams = new object[] { strSQL };
                    DataTable dt = new DataTable();
                    ccp.SourceDataTable = dt;
                    this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
                    if (dt.Rows.Count > 0)
                    {
                        strSQL = "delete dt_zkd_plan where fs_stoveno='" + strStoveNo + "'";
                        ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
                        ccp.MethodName = "ExcuteNonQuery";
                        ccp.ServerParams = new object[] { strSQL };
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    }
                    else
                    {
                        MessageBox.Show("炉号：" + strStoveNo + " 的预报不存在或已计量完成！");
                    }
                }
            }

            MessageBox.Show("预报删除成功！");
            this.GetFlowCardInfo("");
        }

        private string Check()
        {
            if (txtBatchNo.Text == string.Empty)
            {
                return "轧制编号不能为空!";
            }

            if (this.txtStoveNo.Text == string.Empty)
            {
                return "炉号不能为空!";
            }

            if (txtSteelType.Text == string.Empty)
            {
                return "牌号不能为空!";
            }

            if (txtSpec.Text == string.Empty)
            {
                return "规格不能为空!";
            }

            if (cbBilletStatus.SelectedValue.ToString() == string.Empty)
            {
                return "请选择成卷状态!";
            }

            if (cbBilletStatus.SelectedValue.ToString() == "0")
            {
                if (cbPointID.SelectedValue.ToString() == string.Empty)
                {
                    return "请选择预报计量点!";
                }

                //if (txtReel.Text.ToString() == string.Empty)
                //{
                //    return "出炉卷号不能为空!";
                //}
            }


            if (txtProductNo.Text == string.Empty)
            {
                return "订单号不能为空!";
            }

            return string.Empty;
        }

        private string Check2()
        {
            if (txtBatchNo.Text == string.Empty)
            {
                return "轧制编号不能为空!";
            }

            if (this.txtStoveNo.Text == string.Empty)
            {
                return "炉号不能为空!";
            }

            if (txtSteelType.Text == string.Empty)
            {
                return "牌号不能为空!";
            }

            if (txtSpec.Text == string.Empty)
            {
                return "规格不能为空!";
            }

            if (cbBilletStatus.SelectedValue.ToString() == string.Empty)
            {
                return "请选择成卷状态!";
            }

            if (cbBilletStatus.SelectedValue.ToString() == "0")
            {
                if (cbPointID.SelectedValue.ToString() == string.Empty)
                {
                    return "请选择预报计量点!";
                }

                if (txtReel.Text.ToString() == string.Empty)
                {
                    return "出炉卷号不能为空!";
                }
            }

            if (txtProductNo.Text == string.Empty)
            {
                return "订单号不能为空!";
            }

            return string.Empty;
        }

        private void cb_selectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {

                if (this.cb_selectAll.Checked)
                {
                    ugr.Cells["checked"].Value = "True";
                }
                else
                {
                    ugr.Cells["checked"].Value = "False";
                }
            }
        }

        private void bt_AddProdict_Click(object sender, EventArgs e)
        {
            string strResult = Check();

            if (strResult != string.Empty)
            {
                MessageBox.Show(strResult);
                return;
            }
            bool flag = false;
            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                flag = (ugr.Cells["checked"].Value.ToString() == "True");
                if (!flag)
                    continue;
                else
                    break;
            }

            if (flag)
            {
                if (MessageBox.Show("确定新增所选项预报？", "玉钢集中计系统", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    cb_selectAll.Checked = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选择要新增预报的记录！");
                return;
            }
            string strBilletStatus = cbBilletStatus.SelectedValue.ToString();

            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                if (ugr.Cells["checked"].Value.ToString() == "True")
                {
                    string strReel = string.Empty;
                    string strPointID = string.Empty;
                    if(strBilletStatus=="0")
                    {
                        strReel = GetNextReel();
                        strPointID=cbPointID.SelectedValue.ToString();
                    }

                    Hashtable param = new Hashtable();
                    param.Add("I1", ugr.Cells["FS_ZC_BATCHNO"].Value.ToString());
                    param.Add("I2", ugr.Cells["FS_GP_STOVENO"].Value.ToString());
                    param.Add("I3", this.txtSteelType.Text);
                    param.Add("I4", this.txtSpec.Text);
                    param.Add("I5", strPointID);
                    param.Add("I6", strReel);
                    param.Add("I7", cbBilletStatus.SelectedValue.ToString());
                    param.Add("I8", txtProductNo.Text);
                    param.Add("I9", this.UserInfo.GetUserName().ToString());
                    param.Add("I10", this.UserInfo.GetUserOrder().ToString());
                    param.Add("I11", this.UserInfo.GetUserGroup().ToString());
                    CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_ZKDINFO.ADD_ZKD_PLAN(?,?,?,?,?,?,?,?,?,?,?)}", param);
                }
            }

            this.GetFlowCardInfo("");

            this.cb_selectAll.Checked = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (ultraGrid1.ActiveRow == null)
            {
                MessageBox.Show("请先选择需修改的预报记录！");
            }
            else
            {
                string strResult = Check2();

                if (strResult != string.Empty)
                {
                    MessageBox.Show(strResult);
                    return;
                }

                string strReel = string.Empty;
                string strPointID = string.Empty;
                if (cbBilletStatus.SelectedIndex == 1)
                {
                    strReel = this.txtReel.Text;
                    strPointID = cbPointID.SelectedValue.ToString();
                }
                else
                {
                    strReel = "";
                    strPointID = "";
                }

                Hashtable param = new Hashtable();
                param.Add("I1", txtBatchNo.Text);
                param.Add("I2", txtStoveNo.Text);
                param.Add("I3", txtSteelType.Text);
                param.Add("I4", txtSpec.Text);
                param.Add("I5", strPointID);
                param.Add("I6", strReel);
                param.Add("I7", cbBilletStatus.SelectedValue.ToString());
                param.Add("I8", txtProductNo.Text);
                param.Add("I9", this.UserInfo.GetUserName().ToString());
                param.Add("I10", this.UserInfo.GetUserOrder().ToString());
                param.Add("I11", this.UserInfo.GetUserGroup().ToString());
                CoreClientParam ccp = this.excuteProcedure2("{call KG_MCMS_ZKDINFO.Modify_ZKD_PLAN(?,?,?,?,?,?,?,?,?,?,?)}", param);
                this.GetFlowCardInfo("");
            }
        }

        private void cbBilletStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBilletStatus.SelectedIndex != 1)
            {
                this.txtReel.Text = string.Empty;
                this.cbPointID.Text = string.Empty;
            }
        }

        private void ultraGrid1_CellChange(object sender, CellEventArgs e)
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
            }
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
    }
}