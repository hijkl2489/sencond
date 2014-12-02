using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using Core.Sip.Client.App;

using YGJZJL.PublicComponent;
using Infragistics.Win.UltraWinGrid;

namespace YGJZJL.Pipe
{
    public partial class frmWeightQueryPipe : FrmBase
    {
        CorePrinter _printer = new CorePrinter();
        string _PointID = string.Empty;

        public frmWeightQueryPipe()
        {
            InitializeComponent();
        }

        private void frmWeightQueryPipe_Load(object sender, EventArgs e)
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
                this.ultraGrid1.DisplayLayout.Bands[1].Columns["FS_SHIFT"].ValueList = CommonMethod.GetValuelistBanci();
                this.ultraGrid1.DisplayLayout.Bands[1].Columns["FS_TERM"].ValueList = CommonMethod.GetValuelistBanzu();
            }
            catch { }

            try
            {
                string strKey = this.CustomInfo.ToUpper();

                if (!string.IsNullOrEmpty(strKey))
                {
                    if (strKey.Substring(0, 1).Equals("0"))                         //查询
                    {
                        this.ultraToolbarsManager1.Toolbars[1].Visible = false;
                        this.SetToolButtonVisible("判定不合格", false);
                        this.SetToolButtonVisible("撤销不合格", false);
                    }
                    else if (strKey.Substring(0, 1).Equals("1"))                    //补打
                    {
                        this.ultraToolbarsManager1.Toolbars[1].Visible = true;
                        this.SetToolButtonVisible("判定不合格", false);
                        this.SetToolButtonVisible("撤销不合格", false);
                    }
                    else if (strKey.Substring(0, 1).Equals("2"))                    //判废
                    {
                        this.ultraToolbarsManager1.Toolbars[1].Visible = false;
                        this.SetToolButtonVisible("判定不合格", true);
                        this.SetToolButtonVisible("撤销不合格", true);
                    }

                    _PointID = strKey.Substring(1, 3);
                }
            }
            catch { }

            try
            {
                pictureBox3.Hide();
                pictureBox3.SendToBack();
                foreach (string printerName in PrinterSettings.InstalledPrinters)
                {
                    cbPrint.Items.Add(printerName);
                }
            }
            catch { }
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

        private void GetWeightInfo(string strBatchNo)
        {
            string strWhere = "";
            if (_PointID != "")
            {
                strWhere += " and t1.fs_point='" + _PointID + "'";
            }

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strWhere += Convert.ToString("   and t.fd_starttime between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && string.IsNullOrEmpty(txtQueryBatchNo2.Text.Trim()))
            {
                strWhere += Convert.ToString("   and t.fs_batchno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
            }

            if (string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && !string.IsNullOrEmpty(txtQueryBatchNo2.Text.Trim()))
            {
                strWhere += Convert.ToString("   and t.fs_batchno like '%" + txtQueryBatchNo2.Text.Trim() + "%'").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && !string.IsNullOrEmpty(txtQueryBatchNo2.Text.Trim()))
            {
                if (string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
                {
                    strWhere += " and t1.fs_batchno>='" + tbQueryBatchNo.Text.Trim() + "'";
                }
                else
                {
                    strWhere += " and t1.fs_batchno||trim(to_char(fn_bandno,'00'))>='" + tbQueryBatchNo.Text.Trim() + "'||trim(to_char('"+tbQueryBandNo.Text+"','00'))";
                }

                if (string.IsNullOrEmpty(this.tbQueryBandNo2.Text.Trim()))
                {
                    strWhere += " and t1.fs_batchno<='" + txtQueryBatchNo2.Text.Trim() + "'";
                }
                else
                {
                    strWhere += " and t1.fs_batchno||trim(to_char(fn_bandno,'00'))<='" + txtQueryBatchNo2.Text.Trim() + "'||trim(to_char('" + tbQueryBandNo2.Text + "','00'))";
                }
            }

            //PL/SQL SPECIAL COPY

            string strSql = "";
            strSql += Convert.ToString("select a.*,Round(a.fn_theoryweight/a.RealWeight,4)*100 RealRate").Trim() + " ";
            strSql += Convert.ToString("  from (select case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  0").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  1").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 1 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  2").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  3").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  4").Trim() + " ";
            strSql += Convert.ToString("               end XH,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  '规格【' || fs_spec || '】小计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 1 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  '牌号【' || fs_steeltype || '】小计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 1 and grouping(fs_steeltype) = 1 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 1 then").Trim() + " ";
            strSql += Convert.ToString("                  '总计：'").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_steeltype) = 0 and grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fs_batchno)").Trim() + " ";
            strSql += Convert.ToString("               end fs_batchno,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  fs_steeltype").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  null").Trim() + " ";
            strSql += Convert.ToString("               end fs_steeltype,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  fs_spec").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  null").Trim() + " ";
            strSql += Convert.ToString("               end fs_spec,").Trim() + " ";
            strSql += Convert.ToString("               sum(nvl(fn_bandcount, 0)) fn_bandcount,").Trim() + " ";
            strSql += Convert.ToString("               round(sum(nvl(fn_totalweight, 0)), 3) fn_totalweight,").Trim() + " ";
            strSql += Convert.ToString("               round(sum(nvl(fn_theoryweight, 0)), 3) fn_theoryweight,sum(Realweight) Realweight,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fs_productno)").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  null").Trim() + " ";
            strSql += Convert.ToString("               end fs_productno,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fd_starttime)").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  null").Trim() + " ";
            strSql += Convert.ToString("               end fd_starttime,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fd_endtime)").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  null").Trim() + " ";
            strSql += Convert.ToString("               end fd_endtime,").Trim() + " ";
            strSql += Convert.ToString("               case").Trim() + " ";
            strSql += Convert.ToString("                 when grouping(fs_batchno) = 0 and grouping(fs_steeltype) = 0 and").Trim() + " ";
            strSql += Convert.ToString("                      grouping(fs_spec) = 0 then").Trim() + " ";
            strSql += Convert.ToString("                  max(fs_completeflag)").Trim() + " ";
            strSql += Convert.ToString("                 else").Trim() + " ";
            strSql += Convert.ToString("                  null").Trim() + " ";
            strSql += Convert.ToString("               end fs_completeflag ").Trim() + " ";
            strSql += Convert.ToString("          from (select t.fs_batchno,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_productno,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_steeltype,").Trim() + " ";
            strSql += Convert.ToString("                       t.fs_spec,").Trim() + " ";
            strSql += Convert.ToString("                       t1.fn_bandcount,").Trim() + " ";
            strSql += Convert.ToString("                       t1.fn_totalweight,").Trim() + " ";
            strSql += Convert.ToString("                       t1.fn_theoryweight,").Trim() + " ";
            strSql += Convert.ToString("                       to_char(t.fd_starttime, 'yyyy-MM-dd hh24:mi:ss') fd_starttime,").Trim() + " ";
            strSql += Convert.ToString("                       to_char(t.fd_endtime, 'yyyy-MM-dd hh24:mi:ss') fd_endtime,").Trim() + " ";
            strSql += Convert.ToString("                       decode(t.fs_completeflag, '1', '√', '') fs_completeflag").Trim() + " ";
            strSql += ",(select  sum(it.fn_jj_weight) from it_fp_techcard it where it.fs_zc_batchno=t.fs_batchno) RealWeight  ";
            strSql += Convert.ToString("                  from dt_pipeweightmain t,").Trim() + " ";
            strSql += Convert.ToString("                       (select t1.fs_batchno,").Trim() + " ";
            strSql += Convert.ToString("                               count(1) fn_bandcount,").Trim() + " ";
            strSql += Convert.ToString("                               round(sum(nvl(t1.fn_weight, 0)),3) fn_totalweight,").Trim() + " ";
            strSql += Convert.ToString("                               round(sum(nvl(t1.fn_theoryweight, 0)),3) fn_theoryweight").Trim() + " ";
            strSql += Convert.ToString("                          from dt_pipeweightdetail t1").Trim() + " ";
            strSql += Convert.ToString("                         where exists").Trim() + " ";
            strSql += Convert.ToString("                         (select 1").Trim() + " ";
            strSql += Convert.ToString("                                  from dt_pipeweightmain t").Trim() + " ";
            strSql += Convert.ToString("                                 where t.fs_batchno = t1.fs_batchno " + strWhere + ")").Trim() + " ";
            strSql += Convert.ToString("                         group by t1.fs_batchno) t1").Trim() + " ";
            strSql += Convert.ToString("                 where t.fs_batchno = t1.fs_batchno)").Trim() + " ";
            strSql += Convert.ToString("         group by cube(fs_batchno, fs_steeltype, fs_spec)) a").Trim() + " ";
            strSql += Convert.ToString(" where xh <> 4").Trim() + " ";
            strSql += Convert.ToString(" order by xh, fs_batchno").Trim();

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

            strWhere = "";
            string strWhere1 = string.Empty;

            if (cbxDateTime.Checked)
            {
                string strDateTimeFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm");
                string strDateTimeTo = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm");

                strWhere1 += Convert.ToString("   and x.fd_starttime between to_date('" + strDateTimeFrom + "', 'yyyy-MM-dd HH24:mi') and to_date('" + strDateTimeTo + "', 'yyyy-MM-dd HH24:mi')").Trim() + " ";
            }

            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && string.IsNullOrEmpty(txtQueryBatchNo2.Text.Trim()))
            {
                strWhere1 += Convert.ToString("   and x.fs_batchno like '%" + tbQueryBatchNo.Text.Trim() + "%'").Trim() + " ";
            }

            if (string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && !string.IsNullOrEmpty(txtQueryBatchNo2.Text.Trim()))
            {
                strWhere1 += Convert.ToString("   and x.fs_batchno like '%" + txtQueryBatchNo2.Text.Trim() + "%'").Trim() + " ";
            }
            if (_PointID != "")
            {
                strWhere += " and t.fs_point='" + _PointID + "'";
            }
            if (!string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()) && !string.IsNullOrEmpty(txtQueryBatchNo2.Text.Trim()))
            {
                if (string.IsNullOrEmpty(tbQueryBatchNo.Text.Trim()))
                {
                    strWhere += " and t.fs_batchno>='" + tbQueryBatchNo.Text.Trim() + "'";
                }
                else
                {
                    strWhere += " and t.fs_batchno||trim(to_char(fn_bandno,'00'))>='" + tbQueryBatchNo.Text.Trim() + "'||trim(to_char('" + tbQueryBandNo.Text + "','00'))";
                }

                if (string.IsNullOrEmpty(this.tbQueryBandNo2.Text.Trim()))
                {
                    strWhere += " and t.fs_batchno<='" + txtQueryBatchNo2.Text.Trim() + "'";
                }
                else
                {
                    strWhere += " and t.fs_batchno||trim(to_char(fn_bandno,'00'))<='" + txtQueryBatchNo2.Text.Trim() + "'||trim(to_char('" + tbQueryBandNo2.Text + "','00'))";
                }
            }

            strSql = "";
            strSql += Convert.ToString("select t.fs_batchno,t.fn_bandno,'1' FS_PRINTWEIGHTTYPE,").Trim() + " ";
            strSql += Convert.ToString("       m.fs_materialname,t.fn_weight,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_theoryweight,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_type,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_length,").Trim() + " ";
            strSql += Convert.ToString("       t.fn_bandbilletcount,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_person,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_standard FS_STANDNO,").Trim() + " ";
            strSql += Convert.ToString("       p.fs_pointname fs_point,").Trim() + " ";
            strSql += Convert.ToString("       to_char(t.fd_datetime, 'yyyy-MM-dd HH24:mi:ss') fd_datetime,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_shift,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_term,").Trim() + " ";
            strSql += Convert.ToString("       decode(t.fs_upflag, '1', '√', '') fs_upflag,").Trim() + " ";
            strSql += Convert.ToString("       t.fs_weightno,t.FS_JUDGER,to_char(t.FS_JUDGETIME, 'yyyy-MM-dd HH24:mi:ss') FS_JUDGETIME,").Trim() + " ";
            strSql += Convert.ToString("       decode(nvl(t.fs_unqualified, '0'), '1', '不合格', '合格') fs_unqualified,").Trim() + " ";
            strSql += Convert.ToString("       '' FN_DISWEIGHT").Trim() + " ";
            strSql += Convert.ToString("  from dt_pipeweightdetail t,dt_pipeweightmain m,bt_point p").Trim() + " ";
            strSql += Convert.ToString(" where t.fs_batchno = m.fs_batchno and t.fs_point = p.fs_pointcode and exists (select 1").Trim() + " ";
            strSql += Convert.ToString("          from dt_pipeweightmain x").Trim() + " ";
            strSql += Convert.ToString("         where x.fs_batchno = t.fs_batchno " + strWhere1 + ")").Trim() + " ";
            strSql += strWhere;
            strSql += Convert.ToString(" order by t.fs_batchno, t.fn_bandno").Trim();

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

            CommonMethod.RefreshAndAutoSize(ultraGrid1);

            if (ultraGrid1.Rows.Count > 0)
            {
                ultraGrid1.ActiveRow = ultraGrid1.Rows[0];
            }

            if (string.IsNullOrEmpty(strBatchNo))
                return;

            for (int i = 0; i < ultraGrid1.Rows.Count; i++)
            {
                try
                {
                    if (Convert.ToString(ultraGrid1.Rows[i].Cells["FS_BATCHNO"].Value).Equals(strBatchNo))
                    {
                        ultraGrid1.Rows[i].ExpandAll();
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, i, "FS_BATCHNO", false);
                        break;
                    }
                }
                catch { }
            }

        }

        private string _LASTNO = "";

        private bool GetPictures(string strNo)
        {
            return false;
            string err = "";
            string strSql = "select t.fb_image1, t.fb_image2 from dt_storageweightimage t where t.fs_weightno = '" + strNo + "'";

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
                        this.GetWeightInfo("");
                        break;
                    }
                case "导出":
                    {
                        CommonMethod.ExportDataWithSaveDialog2(ref this.ultraGrid1, "棒材计量数据");
                        break;
                    }
                case "判定不合格":
                    {
                        this.Save();
                        break;
                    }
                case "撤销不合格":
                    {
                        this.Cancel();
                        break;
                    }
                case "补打":
                    {
                        this.Reprint();
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

        private void ultraGrid1_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow ActiveRow = ultraGrid1.ActiveRow;

                if (ActiveRow.ParentRow == null)
                {
                    return;
                }

                string PrintAddr = Convert.ToString(ActiveRow.Cells["FS_ADDRESSCHECK"].Value).Trim();
                string PrintStdNo = Convert.ToString(ActiveRow.Cells["FS_STANDARDCHECK"].Value).Trim();
                string PrintSteel = Convert.ToString(ActiveRow.Cells["FS_STEELTYPECHECK"].Value).Trim();
                string WeightMode = Convert.ToString(ActiveRow.Cells["FS_PRINTWEIGHTTYPE"].Value).Trim();

                cbx_Addr.Checked = PrintAddr.Equals("1");
                cbx_Standard.Checked = PrintStdNo.Equals("1");
                cbx_Steel.Checked = PrintSteel.Equals("1");

                if (WeightMode.Equals("0"))
                {
                    cbx_Theory.Checked = true;
                    cbx_Actual.Checked = false;
                }
                else if (WeightMode.Equals("1"))
                {
                    cbx_Theory.Checked = false;
                    cbx_Actual.Checked = true;
                }
                else if (WeightMode.Equals("2"))
                {
                    cbx_Theory.Checked = true;
                    cbx_Actual.Checked = true;
                }
                else
                {
                    cbx_Theory.Checked = true;
                    cbx_Actual.Checked = false;
                }

                if (_PointID.Equals("K33") || _PointID.Equals("K36"))
                {
                    cbx_Theory.Checked = true;
                    cbx_Actual.Checked = false;
                }

                cbx_Standard.Checked = true;
                cbx_Addr.Checked = true;
            }
            catch { }
        }

        private void SetPrintData(UltraGridRow ActiveRow, int iCopies)
        {
            if (ActiveRow == null)
                return;

            if (iCopies <= 0)
                iCopies = 2;

            string strBatchNo = Convert.ToString(ActiveRow.Cells["FS_BATCHNO"].Value).Trim();
            string strBandNo = Convert.ToString(ActiveRow.Cells["FN_BANDNO"].Value).Trim();
            string strStandardNo = Convert.ToString(ActiveRow.Cells["FS_STANDNO"].Value).Trim();
            string strSteelType = Convert.ToString(ActiveRow.ParentRow.Cells["FS_STEELTYPE"].Value);
            string strSpec = Convert.ToString(ActiveRow.ParentRow.Cells["FS_SPEC"].Value);
            string strType = Convert.ToString(ActiveRow.Cells["FS_TYPE"].Value).Trim();
            string strLength = Convert.ToString(ActiveRow.Cells["FN_LENGTH"].Value).Trim();
            string strActual = Convert.ToString(ActiveRow.Cells["FN_WEIGHT"].Value).Trim();
            string strTheory = Convert.ToString(ActiveRow.Cells["FN_THEORYWEIGHT"].Value).Trim();
            string strCount = Convert.ToString(ActiveRow.Cells["FN_BANDBILLETCOUNT"].Value).Trim();
            string strTerm = Convert.ToString(ActiveRow.Cells["FS_TERM"].Value).Trim();
            if (_PointID.Equals("K33") || _PointID.Equals("K36"))
            {
                strTerm = strTerm.Equals("1") ? "甲" : strTerm.Equals("2") ? "乙" : strTerm.Equals("3") ? "丙" :strTerm.Equals("4") ? "丁" : "常白";
            }
            string strWeighTime = Convert.ToString(ActiveRow.Cells["FD_DATETIME"].Value).Trim();
            string materialName = Convert.ToString(ActiveRow.Cells["FS_MATERIALNAME"].Value).Trim();
            //switch (UserInfo.GetUserGroup())
            //{
            //    case "1": strTerm = "A"; break;
            //    case "2": strTerm = "B"; break;
            //    case "3": strTerm = "C"; break;
            //    case "4": strTerm = "D"; break;
            //    default: strTerm = "A"; break;
            //}
            _printer.PrinterName = cbPrint.Text;
            _printer.Data.PM = materialName;
            switch (_PointID)
            {
                case "K26":
                case "K27":
                case "K34":
                case "K32":
                case "K37":
                    _printer.Data.Type = LableType.PIPE;
                    break;
                case "K33":
                case "K36":
                    _printer.Data.Type = LableType.PIPE2;
                    break;
            }
            _printer.Data.BatchNo = strBatchNo;
            _printer.Data.BandNo = strBandNo;
            _printer.Data.Standard = strStandardNo;
            _printer.Data.SteelType = strSteelType;
            _printer.Data.Spec = strSpec;
            _printer.Data.Length = strLength;
            _printer.Data.Term = strTerm;
            _printer.Data.PrintAddress = cbx_Addr.Checked;

            try
            {
                _printer.Data.BarCode = GetBarCode(strBatchNo, strBandNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("条码生成失败！\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _printer.Data.Date = Convert.ToDateTime(strWeighTime);
            }
            catch
            {
                _printer.Data.Date = DateTime.Now;
            }

            try
            {
                _printer.Data.Count = strCount;
            }
            catch { }

            try
            {
                strActual = Convert.ToDecimal(strActual).ToString();
            }
            catch { }

            try
            {
                strTheory = Convert.ToDecimal(strTheory).ToString();
            }
            catch { }

            if (strType != "定尺")
            {
                _printer.Data.Count = "非";
                _printer.Data.Weight = strActual;

                for (int i = 0; i < iCopies; i++)
                {
                    _printer.Print();
                }

                return;
            }

            if (cbx_Actual.Checked)
            {
                _printer.Data.Weight = strActual;

                for (int i = 0; i < iCopies; i++)
                {
                    _printer.Print();
                }
            }

            if (cbx_Theory.Checked)
            {
                _printer.Data.Weight = strTheory;

                for (int i = 0; i < iCopies; i++)
                {
                    _printer.Print();
                }
            }
        }

        private void Reprint()
        {
            try
            {
                if (ultraGrid1.Rows.Count == 0)
                {
                    return;
                }
                if (cbPrint.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择打印机！");
                    return;
                }

                UltraGridRow ActiveRow = ultraGrid1.ActiveRow;

                if (ActiveRow.ParentRow == null && ActiveRow.ChildBands.Count > 0 && ActiveRow.ChildBands[0].Rows.Count == 0)
                {
                    MessageBox.Show("请选择轧制编号补打整炉或者展开轧制编号选择下方需要补打的吊号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (!cbx_Actual.Checked && !cbx_Theory.Checked)
                {
                    MessageBox.Show("请选择打牌重量类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                if (string.IsNullOrEmpty(Edt_Copies.Value.ToString()))
                {
                    MessageBox.Show("请选择打印份数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                int iCopies = 2;
                try
                {
                    iCopies = Convert.ToInt16(Edt_Copies.Value);
                }
                catch
                {
                    iCopies = 2;
                }

                _printer.Data = new HgLable();

                if (ActiveRow.ParentRow != null)
                {
                    this.SetPrintData(ActiveRow, iCopies);
                }
                else
                {
                    for (int i = 0; i < ActiveRow.ChildBands[0].Rows.Count; i++)
                    {
                        this.SetPrintData(ActiveRow.ChildBands[0].Rows[i], iCopies);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetBarCode(string batchNo, string batchIndex)
        {
            if (string.IsNullOrEmpty(batchNo) || string.IsNullOrEmpty(batchIndex))
            {
                return null;
            }

            batchNo = batchNo.Trim().Insert(1, "1");
            batchIndex = batchIndex.Trim();

            if (batchIndex.Length == 1)
            {
                batchIndex = "0" + batchIndex;
            }
            else if (batchIndex.Length == 0)
            {
                batchIndex = "01";
            }

            return "402" + batchNo.Trim() + batchIndex;
        }

        private void Save()
        {
            try
            {
                if (ultraGrid1.Rows.Count == 0)
                {
                    return;
                }

                UltraGridRow ActiveRow = ultraGrid1.ActiveRow;

                if (ActiveRow.ParentRow == null)
                {
                    if (ActiveRow.ChildBands == null)
                    {
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("确认要将撤销此"+ActiveRow.ChildBands[0].Rows.Count+"吊钢不合格状态吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        string strBatchNo = Convert.ToString(ActiveRow.Cells["FS_BATCHNO"].Value).Trim();

                        for (int i = 0; i < ActiveRow.ChildBands[0].Rows.Count; i++)
                        {
                            string strNo = Convert.ToString(ActiveRow.ChildBands[0].Rows[i].Cells["FS_WEIGHTNO"].Value).Trim();
                            
                            object[] sArgs = new object[] { strNo, this.UserInfo.GetUserName() };
                            string strErr = "";
                            string strProcedure = "KG_MCMS_FLOWCARD.SaveUnQualified_BC";
                            ArrayList obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                        }

                        this.GetWeightInfo(strBatchNo);
                        MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    if (MessageBox.Show("确认要将此吊钢置为不合格吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    string strNo = Convert.ToString(ActiveRow.Cells["FS_WEIGHTNO"].Value).Trim();
                    string strBatchNo = Convert.ToString(ActiveRow.Cells["FS_BATCHNO"].Value).Trim();

                    object[] sArgs = new object[] { strNo, this.UserInfo.GetUserName() };
                    string strErr = "";
                    string strProcedure = "KG_MCMS_FLOWCARD.SaveUnQualified_BC";
                    ArrayList obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                    if (int.Parse(obj[2].ToString()) > 0)
                    {
                        this.GetWeightInfo(strBatchNo);
                        MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Cancel()
        {
            try
            {
                if (ultraGrid1.Rows.Count == 0)
                {
                    return;
                }

                UltraGridRow ActiveRow = ultraGrid1.ActiveRow;

                if (ActiveRow.ParentRow == null)
                {
                    if (ActiveRow.ChildBands == null)
                    {
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("确认要将撤销此" + ActiveRow.ChildBands[0].Rows.Count + "吊钢不合格状态吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        string strBatchNo = Convert.ToString(ActiveRow.Cells["FS_BATCHNO"].Value).Trim();
                        for (int i = 0; i < ActiveRow.ChildBands[0].Rows.Count; i++)
                        {
                            string strNo = Convert.ToString(ActiveRow.ChildBands[0].Rows[i].Cells["FS_WEIGHTNO"].Value).Trim();
                            object[] sArgs = new object[] { strNo, this.UserInfo.GetUserName() };
                            string strErr = "";
                            string strProcedure = "KG_MCMS_FLOWCARD.CancelUnQualified_BC";
                            ArrayList obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);
                        }

                        this.GetWeightInfo(strBatchNo);
                        MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    if (MessageBox.Show("确认要将撤销此吊钢不合格状态吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;

                    string strNo = Convert.ToString(ActiveRow.Cells["FS_WEIGHTNO"].Value).Trim();
                    string strBatchNo = Convert.ToString(ActiveRow.Cells["FS_BATCHNO"].Value).Trim();

                    object[] sArgs = new object[] { strNo, this.UserInfo.GetUserName() };
                    string strErr = "";
                    string strProcedure = "KG_MCMS_FLOWCARD.CancelUnQualified_BC";
                    ArrayList obj = ProcReturnDS("ygjzjl.bar.DBHelp", "doProcedure", new object[] { strProcedure, sArgs }, out strErr);

                    if (int.Parse(obj[2].ToString()) > 0)
                    {
                        this.GetWeightInfo(strBatchNo);
                        MessageBox.Show("操作成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("操作失败！\n" + obj[3].ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "出错", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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