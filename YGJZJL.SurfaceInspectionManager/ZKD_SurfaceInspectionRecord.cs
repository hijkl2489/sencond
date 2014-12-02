using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Infragistics.Win.UltraWinGrid;
using System.Data.SqlClient;
using System.Threading;
using System.Runtime.InteropServices;
using Infragistics.Win;
using System.Diagnostics;
using System.IO;
using YGJZJL.PublicComponent;

namespace YGJZJL.SurfaceInspectionManager
{
    public partial class ZKD_SurfaceInspectionRecord : FrmBase
    {
        private string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        private string strUserShift = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
        private string strUserTerm = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());

        private string strCurrentBatchNo = "";
        private string[] strOldInfo = new string[13];
        private string[] strNewInfo = new string[13];
        private string[] strHeadInfo = new string[13];
        UltraGridRow currentRow = null;

        private ValueList _VLProductSurfaceDefects;//缺陷
        private ValueList _VLTestResults;//判定结果
        private ValueList _VLToProcess;//转序工序

        BaseInfo m_BaseInfo = new BaseInfo();
        string strIP = "";
        string strMAC = "";

        public ZKD_SurfaceInspectionRecord()
        {
            InitializeComponent();
        }

        private void ZKD_SurfaceInspectionRecord_Load(object sender, EventArgs e)
        {
            this.dtp_Begin.Value = DateTime.Today;
            this.dtp_End.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

            initTextBoxEnable();

            this._VLProductSurfaceDefects = new ValueList();
            BindToProductSurfaceDefects();
            this.ulgZKD.DisplayLayout.Bands[1].Columns["FS_DEFECTSTYPE"].ValueList = this._VLProductSurfaceDefects;

            this._VLTestResults = new ValueList();
            BindToTestResults();
            this.ulgZKD.DisplayLayout.Bands[1].Columns["FS_TESTRESULT"].ValueList = this._VLTestResults;

            this._VLToProcess = new ValueList();
            BindToPorcess();
            this.ulgZKD.DisplayLayout.Bands[1].Columns["FS_ZXGX"].ValueList = this._VLToProcess;

            strIP = m_BaseInfo.getIPAddress();
            strMAC = m_BaseInfo.getMACAddress();
            saveHeadInfo();
        }

        private void saveHeadInfo()
        {
            strHeadInfo[0] = "抽测宽度1";
            strHeadInfo[1] = "抽测宽度2";
            strHeadInfo[2] = "抽测厚度1-操作侧";
            strHeadInfo[3] = "抽测厚度1-传动侧";
            strHeadInfo[4] = "抽测厚度1-中部";
            strHeadInfo[5] = "抽测厚度2-操作侧";
            strHeadInfo[6] = "抽测厚度2-传动侧";
            strHeadInfo[7] = "抽测厚度2-中部";
            strHeadInfo[8] = "缺陷类型";
            strHeadInfo[9] = "塔形";
            strHeadInfo[10] = "判定结果";
            strHeadInfo[11] = "转序工序";
            strHeadInfo[12] = "备注";
        }

        private void saveOldInfo(UltraGridRow row)
        {
            strOldInfo[0] = row.Cells["FN_CCKD1"].Value.ToString();
            strOldInfo[1] = row.Cells["FN_CCKD2"].Value.ToString();
            strOldInfo[2] = row.Cells["FN_CCHD1CZC"].Value.ToString();
            strOldInfo[3] = row.Cells["FN_CCHD1CDC"].Value.ToString();
            strOldInfo[4] = row.Cells["FN_CCHD1ZB"].Value.ToString();
            strOldInfo[5] = row.Cells["FN_CCHD2CZC"].Value.ToString();
            strOldInfo[6] = row.Cells["FN_CCHD2CDC"].Value.ToString();
            strOldInfo[7] = row.Cells["FN_CCHD2ZB"].Value.ToString();
            strOldInfo[8] = row.Cells["FS_DEFECTSTYPE"].Value.ToString();
            strOldInfo[9] = row.Cells["FN_TX"].Value.ToString();
            strOldInfo[10] = row.Cells["FS_TESTRESULT"].Value.ToString();
            strOldInfo[11] = row.Cells["FS_ZXGX"].Value.ToString();
            strOldInfo[12] = row.Cells["FS_MEMO"].Value.ToString();
        }

        private void saveNewInfo(UltraGridRow row)
        {
            strOldInfo[0] = row.Cells["FN_CCKD1"].Value.ToString();
            strOldInfo[1] = row.Cells["FN_CCKD2"].Value.ToString();
            strOldInfo[2] = row.Cells["FN_CCHD1CZC"].Value.ToString();
            strOldInfo[3] = row.Cells["FN_CCHD1CDC"].Value.ToString();
            strOldInfo[4] = row.Cells["FN_CCHD1ZB"].Value.ToString();
            strOldInfo[5] = row.Cells["FN_CCHD2CZC"].Value.ToString();
            strOldInfo[6] = row.Cells["FN_CCHD2CDC"].Value.ToString();
            strOldInfo[7] = row.Cells["FN_CCHD2ZB"].Value.ToString();
            strOldInfo[8] = row.Cells["FS_DEFECTSTYPE"].Value.ToString();
            strOldInfo[9] = row.Cells["FN_TX"].Value.ToString();
            strOldInfo[10] = row.Cells["FS_TESTRESULT"].Value.ToString();
            strOldInfo[11] = row.Cells["FS_ZXGX"].Value.ToString();
            strOldInfo[12] = row.Cells["FS_MEMO"].Value.ToString();
        }

        //工具条
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case"Query":
                    doQuerySurfaceInspectionInfo();
                    break;
                case"ToExcel":
                    doExport();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 导出EXCEL
        /// </summary>
        private void doExport()
        {
            ExportDataWithSaveDialog2(ref this.ulgZKD, "中宽带表面检验记录");
        }

        public void ExportDataWithSaveDialog2(ref UltraGrid myGrid1, string strFileName)
        {
            try
            {
                string strRunPath = System.Environment.CurrentDirectory;
                if (myGrid1.Rows.Count == 0) return;

                if (strFileName.Length == 0)
                    strFileName = "未命名";

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "保存";
                dlg.OverwritePrompt = true;
                dlg.Filter = "Excel文件(*.xls)|*.xls";
                dlg.AddExtension = true;
                dlg.FileName = strFileName;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    strFileName = dlg.FileName;
                    this.ultraGridExcelExporter1.Export(myGrid1, strFileName);
                    System.Environment.CurrentDirectory = strRunPath;


                    if (MessageBox.Show("数据导出成功！\r\n需要打开所导出文件吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ProcessStartInfo p = new ProcessStartInfo(strFileName);
                        p.WorkingDirectory = Path.GetDirectoryName(strFileName);
                        Process.Start(p);
                        // System.Environment.CurrentDirectory = p.WorkingDirectory;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 查询中宽带表面检验信息,主表
        /// </summary>
        public void doQuerySurfaceInspectionInfo()
        {
            if (!ParseTime(this.dtp_Begin.Value, this.dtp_End.Value) && !this.cbBatchNo.Checked)
            {
                return;
            }
            this.dataSet1.Tables[1].Clear();
            this.dataSet1.Tables[0].Clear();
            string strSql = @"select t.fs_batchno,
                       count(1)as fs_reel,
                       t.fs_spec,
                       t.fs_steeltype,
                       sum(t.fn_weight) as fn_weight,
                       to_char(min(t.fd_datetime), 'yyyy-MM-dd hh24:mi:ss') as fd_datetime,
                       a.fn_cckd1,
                       a.fn_cckd2,
                       a.fn_cchd1czc,
                       a.fn_cchd1cdc,
                       a.fn_cchd1zb,
                       a.fn_cchd2czc,
                       a.fn_cchd2cdc,
                       a.fn_cchd2zb
                  from dt_zkd_productweightdetail t
                  left join DT_ZKD_QM_BATCHNOINFO a on t.fs_batchno = a.fs_batchno
                  where 1=1";
            if (this.cbBatchNo.Checked)
            {
                strSql += " and t.FS_BATCHNO >= '" + this.tbBeginBatch.Text + "' and t.FS_BATCHNO <= '" + this.tbEndBatch.Text + "'";
            }
            else
            {
                strSql += " and t.fd_datetime between to_date('" + this.dtp_Begin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + this.dtp_End.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')  and a.fn_cckd1 is null or a.fn_cckd1 > 0";
            }

            strSql += @"  group by t.fs_batchno,
                      t.fs_spec,
                      t.fs_steeltype,
                      a.fn_cckd1,
                      a.fn_cckd2,
                      a.fn_cchd1czc,
                      a.fn_cchd1cdc,
                      a.fn_cchd1zb,
                      a.fn_cchd2czc,
                      a.fn_cchd2cdc,
                      a.fn_cchd2zb                     
             order by t.fs_batchno desc";

            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";

            ccp.ServerParams = new object[] { strSql };
            dataSet1.Tables[0].Clear();
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            Constant.RefreshAndAutoSize(ulgZKD);

            if (this.dataSet1.Tables[0].Rows.Count > 0)
            {
                doQueryDetail();
                //showRowsColor();
                //LoadSTEELTYPE();
                //LoadSPEC();
            }

        }

        /// <summary>
        /// 查询中宽带表面检验明细表信息
        /// </summary>
        private void doQueryDetail()
        {
            string strTempWhere = "(";
            foreach (DataRow row in this.dataSet1.Tables[0].Rows)
            {
                strTempWhere += "'" + row["FS_BATCHNO"].ToString() + "',";
            }
            strTempWhere = strTempWhere.TrimEnd(',');
            strTempWhere += ")";

            string strSql = @"select t.fs_batchno,
                           t.fn_bandno,
                           t.fn_weight,
                           to_char(t.fd_datetime, 'yyyy-MM-dd hh24:mi:ss') as fd_datetime,
                           t.fs_productno,
                           t.fn_ykl,
                           t.fn_khjz,
                           t.fs_reel,
                           t.fs_stoveno,
                           t.fs_person,
                           t.fs_spec,
                           t.fs_steeltype,
                           a.fn_cckd1,
                           a.fn_cckd2,
                           a.fn_cchd1czc,
                           a.fn_cchd1cdc,
                           a.fn_cchd1zb,
                           a.fn_cchd2czc,
                           a.fn_cchd2cdc,
                           a.fn_cchd2zb,
                           a.fs_defectstype,
                           a.fn_tx,
                           a.fs_testresult,
                           a.fs_zxgx,
                           a.fs_memo,
                           a.fs_createperson,
                           a.fs_createshift,
                           a.fs_createterm,
                           to_char(a.fd_createdatetime, 'yyyy-MM-dd hh24:mi:ss') as fd_createdatetime
                      from dt_zkd_productweightdetail t
                      left join DT_ZKD_QM_BATCHNOINFO a on t.fs_batchno = a.fs_batchno
                                                       and t.fn_bandno = a.fn_bandno
                  where t.fs_batchno in " + strTempWhere + " order by FN_BANDNO asc";

            CoreClientParam ccp1 = new CoreClientParam();

            ccp1.ServerName = "ygjzjl.base.QueryData";
            ccp1.MethodName = "queryByClientSql";

            ccp1.ServerParams = new object[] { strSql };
            dataSet1.Tables[1].Clear();
            ccp1.SourceDataTable = dtDetail;
            this.ExecuteQueryToDataTable(ccp1, CoreInvokeType.Internal);
            showRowsColor();
            //Constant.RefreshAndAutoSize(ulgZKD);
        }

        //是否按批次查询条件
        private void cbBatchNo_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbBatchNo.Checked)
            {
                this.tbBeginBatch.Enabled = true;
                this.tbEndBatch.Enabled = true;
            }
            else
            {
                this.tbBeginBatch.Enabled = false;
                this.tbEndBatch.Enabled = false;
            }
        }
        //初始化批次号TextBox控件
        private void initTextBoxEnable()
        {
            this.tbBeginBatch.Enabled = false;
            this.tbEndBatch.Enabled = false;
        }

        /// <summary>
        /// 判定查询区间天数
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public bool ParseTime(DateTime beginTime,DateTime endTime)
        {
            double strTime = endTime.Subtract(beginTime).TotalDays;
            if (strTime > 5)
            {
                MessageBox.Show("所选时间区间大于 5 天，数据量过大，请从新选择时间区间！");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnTemplateOk_Click(object sender, EventArgs e)
        {
            // This code was automatically generated by the RowEditTemplate Wizard
            // 
            // Close the template and save any pending changes.

            if (doCheck())
            {
                return;
            }
            if (this.ugcpFS_BATCHNO.Text == string.Empty)
            {
                return;
            }
            int intTemp = checkIsExist();
            string strDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            switch (intTemp)
            {
                case 0:
                    strSql = @"insert into dt_zkd_qm_batchnoinfo
                      (fs_batchno,
                       fn_bandno,
                       fn_cckd1,
                       fn_cckd2,
                       fn_cchd1czc,
                       fn_cchd1cdc,
                       fn_cchd1zb,
                       fn_cchd2czc,
                       fn_cchd2cdc,
                       fn_cchd2zb,
                       fs_defectstype,
                       fn_tx,
                       fs_testresult,
                       fs_zxgx,
                       fs_memo,
                       fs_createperson,
                       fs_createshift,
                       fs_createterm,
                       fd_createdatetime) values
                      ('" + ugcpFS_BATCHNO.Text + "',"
                        + "'"+this.ugcpFN_BANDNO.Text+"',"
                        + "case when Length('" + this.ugcpFN_CCKD1.Text + "') > 0 then '" + this.ugcpFN_CCKD1.Text + "'else '0' end,"
                        + "case when Length('" + this.ugcpFN_CCKD2.Text + "') > 0 then '" + this.ugcpFN_CCKD2.Text + "' else '0' end,"
                        + "case when Length('" + this.ugcpFN_CCHD1CZC.Text + "') > 0 then '" + this.ugcpFN_CCHD1CZC.Text + "' else '0' end,"
                        + "case when Length('" + this.ugcpFN_CCHD1CDC.Text + "') > 0 then '" + this.ugcpFN_CCHD1CDC.Text + "' else '0' end,"
                        + "case when Length('" + this.ugcpFN_CCHD1ZB.Text + "') > 0 then '" + this.ugcpFN_CCHD1ZB.Text + "' else '0' end,"
                        + "case when Length('" + this.ugcpFN_CCHD2CZC.Text + "') > 0 then '" + this.ugcpFN_CCHD2CZC.Text + "' else '0' end,"
                        + "case when Length('" + this.ugcpFN_CCHD2CDC.Text + "') > 0 then '" + this.ugcpFN_CCHD2CDC.Text + "' else '0' end,"
                        + "case when Length('" + this.ugcpFN_CCHD2ZB.Text + "') > 0 then '" + this.ugcpFN_CCHD2ZB.Text + "' else '0' end,"
                        + "'" + this.ugcpFS_DEFECTSTYPE.Text + "',"
                        + "case when Length('" + this.ugcpFN_TX.Text + "') > 0 then '" + this.ugcpFN_TX.Text + "' else '0' end,"
                        + "'" + this.ugcpFS_TESTRESULT.Text + "',"
                        + "'" + this.ugcpFS_ZXGX.Text + "',"
                        + "'" + this.ugcpFS_MEMO.Text + "',"
                        + "'" + this.strUserName + "','" + this.strUserShift + "','" + this.strUserTerm + "',sysdate)";
                    break;
                case 1:
                    strSql = @"update dt_zkd_qm_batchnoinfo t set t.fn_cckd1 = case when Length('"+this.ugcpFN_CCKD1.Text+"') > 0 then '"+this.ugcpFN_CCKD1.Text+"' else '0' end,"
                        + "t.fn_cckd2 = case when Length('"+this.ugcpFN_CCKD2.Text+"') > 0 then '"+this.ugcpFN_CCKD2.Text+"' else '0' end,"
                        + "t.fn_cchd1czc = case when Length('"+this.ugcpFN_CCHD1CZC.Text+"') > 0 then '"+this.ugcpFN_CCHD1CZC.Text+"' else '0' end,"
                        + "t.fn_cchd1cdc = case when Length('"+this.ugcpFN_CCHD1CDC.Text+"') > 0 then '"+this.ugcpFN_CCHD1CDC.Text+"' else '0' end,"
                        + "t.fn_cchd1zb = case when Length('"+this.ugcpFN_CCHD1ZB.Text+"') > 0 then '"+this.ugcpFN_CCHD1ZB.Text+"' else '0' end,"
                        + "t.fn_cchd2czc = case when Length('"+this.ugcpFN_CCHD2CZC.Text+"') > 0 then '"+this.ugcpFN_CCHD2CZC.Text+"' else '0' end,"
                        + "t.fn_cchd2cdc = case when Length('"+this.ugcpFN_CCHD2CDC.Text+"') > 0 then '"+this.ugcpFN_CCHD2CDC.Text+"' else '0' end,"
                        + "t.fn_cchd2zb = case when Length('"+this.ugcpFN_CCHD2ZB.Text+"') > 0 then '"+this.ugcpFN_CCHD2ZB.Text+"' else '0' end,"
                        + "t.fs_defectstype = '"+this.ugcpFS_DEFECTSTYPE.Text.ToString()+"',"
                        + "t.fn_tx = case when Length('"+this.ugcpFN_TX.Text+"') > 0 then '"+this.ugcpFN_TX.Text+"' else '0' end,"
                        + "t.fs_testresult = '"+this.ugcpFS_TESTRESULT.Text.ToString()+"',"
                        + "t.fs_zxgx = '"+this.ugcpFS_ZXGX.Text.ToString()+"',"
                        + "t.fs_memo = '"+this.ugcpFS_MEMO.Text.ToString()+"',"
                        + "t.fs_createperson = '" + this.strUserName + "',t.fs_createshift = '" + this.strUserShift + "',t.fs_createterm = '" + this.strUserTerm + "',t.fd_createdatetime = sysdate where t.fs_batchno = '" + this.ugcpFS_BATCHNO.Text + "' and t.fn_bandno = '"+this.ugcpFN_BANDNO.Text+"'";

                    break;
                case -1:
                    MessageBox.Show("数据操作失败！");
                    break;
                default:
                    break;
            }
            

            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";
            ccp.ServerParams = new object[] { strSql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("数据操作成功！");
                //UpdateTechCard();
                this.ultraGridRowEditTemplate1.Close(true);

                string m_Memo = "";
                if (intTemp == 1)
                {
                    for (int i = 0; i < strHeadInfo.Length; i++)
                    {
                        m_Memo += strHeadInfo[i] + ":" + strOldInfo[i] + "->" + strNewInfo[i] + ",";
                    }
                    //m_BaseInfo.insertLog(strDate, "修改", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/棒线产品外形表面质量检验记录");
                    this.insertLogs(strDate, "修改", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/中宽带产品外形表面质量检验记录");

                }
                if (intTemp == 0)
                {
                    for (int i = 0; i < strHeadInfo.Length; i++)
                    {
                        m_Memo += strHeadInfo[i] + ":" + strNewInfo[i] + ",";
                    }
                    //m_BaseInfo.insertLog(strDate, "新增", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/棒线产品外形表面质量检验记录");
                    this.insertLogs(strDate, "新增", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/中宽带产品外形表面质量检验记录");

                }

                doQuerySurfaceInspectionInfo();
            }
            else
            {
                MessageBox.Show("数据操作失败！");
            }

        }

        private void btnTemplateCancel_Click(object sender, EventArgs e)
        {
            // This code was automatically generated by the RowEditTemplate Wizard
            // 
            // Close the template and discard any pending changes.
            this.ultraGridRowEditTemplate1.Close(false);

        }

        /// <summary>
        /// 缺陷类型
        /// </summary>
        public void BindToProductSurfaceDefects()
        {
            this._VLProductSurfaceDefects.ValueListItems.Clear();
            this._VLProductSurfaceDefects.ValueListItems.Add("破边", "破边");
            this._VLProductSurfaceDefects.ValueListItems.Add("塔形", "塔形");
            this._VLProductSurfaceDefects.ValueListItems.Add("卷起不规范", "卷起不规范");
            this._VLProductSurfaceDefects.ValueListItems.Add("松卷", "松卷");
            this._VLProductSurfaceDefects.ValueListItems.Add("厚度不均", "厚度不均");
            this._VLProductSurfaceDefects.ValueListItems.Add("中间浪", "中间浪");
        }
        /// <summary>
        /// 判定结果
        /// </summary>
        public void BindToTestResults()
        {
            this._VLTestResults.ValueListItems.Clear();
            this._VLTestResults.ValueListItems.Add("合格", "合格");
            this._VLTestResults.ValueListItems.Add("利用品", "利用品");
            this._VLTestResults.ValueListItems.Add("废品", "废品");
        }
        /// <summary>
        /// 转序工序
        /// </summary>
        public void BindToPorcess()
        {
            this._VLToProcess.ValueListItems.Clear();
            this._VLToProcess.ValueListItems.Add("有精整", "有精整");
            this._VLToProcess.ValueListItems.Add("待判", "待判");
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <returns></returns>
        private bool doCheck()
        {
            if (this.ugcpFN_CCKD1.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCKD1.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCKD1.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_CCKD2.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCKD2.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCKD2.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_TX.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_TX.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_TX.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_CCHD1CZC.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCHD1CZC.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCHD1CZC.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_CCHD1CDC.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCHD1CDC.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCHD1CDC.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_CCHD1ZB.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCHD1ZB.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCHD1ZB.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_CCHD2CZC.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCHD2CZC.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCHD2CZC.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_CCHD2CDC.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCHD2CDC.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCHD2CDC.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_CCHD2ZB.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_CCHD2ZB.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_CCHD2ZB.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFS_TESTRESULT.Text == string.Empty)
            {
                this.ugcpFS_TESTRESULT.Focus();
                MessageBox.Show("请输入判定结果！");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 查询轧制批号是否在表面质量检验表
        /// </summary>
        /// <returns></returns>
        private int checkIsExist()
        {
            DataTable dt = new DataTable();
            string strSql = "select  t.fs_batchno from dt_zkd_qm_batchnoinfo t where t.fs_batchno = '" + this.ugcpFS_BATCHNO.Text + "' and t.fn_bandno = '"+this.ugcpFN_BANDNO.Text+"'";
            CoreClientParam ccp = new CoreClientParam();

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";

            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 插入数据库操作日志
        /// </summary>
        /// <param name="strDateTime">操作时间</param>
        /// <param name="strOperationType">操作类型</param>
        /// <param name="strOperater">操作人</param>
        /// <param name="strOperationIP">IP地址</param>
        /// <param name="strOperationMAC">MAC地址</param>
        /// <param name="strOperationMemo">操作内容</param>
        /// <param name="strKeyWord">关键字</param>
        /// <param name="strStoveNo">冶炼炉号</param>
        /// <param name="strBatchNo">轧制编号</param>
        /// <param name="strCarNo">车号</param>
        /// <param name="strBandNo">吊(支)号</param>
        /// <param name="strTableName">表名</param>
        /// <param name="strModuleName">模块名</param>
        private void insertLogs(string strDateTime, string strOperationType, string strOperater, string strOperationIP, string strOperationMAC, string strOperationMemo, string strKeyWord, string strStoveNo, string strBatchNo, string strCarNo, string strBandNo, string strTableName, string strModuleName)
        {
            string sql = " insert into dt_techCardOperation (FD_DATATIME,FS_OPERATIONTYPE,Fs_Operater,Fs_Operationip,Fs_Operationmac,FS_OPERATIONMEMO,FS_KeyWord,FS_StoveNo,FS_BatchNo,FS_CARNO,FS_BANDNO,FS_TABLENAME,FS_MODULENAME)  values (to_date('" + strDateTime + "','yyyy-MM-dd HH24:mi:ss'),'" + strOperationType + "','" + strOperater + "','" + strOperationIP + "','" + strOperationMAC + "','" + strOperationMemo + "','" + strKeyWord + "','" + strStoveNo + "','" + strBatchNo + "','" + strCarNo + "','" + strBandNo + "','" + strTableName + "','" + strModuleName + "')";
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteNonQuery";
            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }
        private void ulgZKD_BeforeRowEditTemplateClosed(object sender, BeforeRowEditTemplateClosedEventArgs e)
        {
            saveNewInfo(e.Template.Row);
        }

        private void ulgZKD_AfterRowEditTemplateDisplayed(object sender, AfterRowEditTemplateDisplayedEventArgs e)
        {
            currentRow = e.Template.Row;
            strCurrentBatchNo = e.Template.Row.Cells["FS_BATCHNO"].Value.ToString();
            saveOldInfo(e.Template.Row);
        }

        private void showRowsColor()
        {
            foreach (UltraGridRow row in this.ulgZKD.Rows)
            {
                //if (row.HasChild())
                //{
                //    row.Cells["FN_LENGTH"].Value = row.ChildBands[0].Rows[0].Cells["FN_LENGTH"].Value;
                //}
                for (int i = 0; i < row.ChildBands[0].Rows.Count; i++)
                {
                    if (row.ChildBands[0].Rows[i].Cells["FS_TESTRESULT"].Value.ToString() == "废品")
                    {
                        row.ChildBands[0].Rows[i].Appearance.BackColor = System.Drawing.Color.Red;
                    }
                    else if (row.ChildBands[0].Rows[i].Cells["FS_TESTRESULT"].Value.ToString() == "利用品")
                    {
                        row.ChildBands[0].Rows[i].Appearance.BackColor = System.Drawing.Color.DarkOrange;
                    }
                }
            }
        }
    }
}
