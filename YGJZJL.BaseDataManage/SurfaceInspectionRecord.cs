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

namespace YGJZJL.BaseDataManage
{
    public partial class SurfaceInspectionRecord : FrmBase
    {
        public SurfaceInspectionRecord()
        {
            InitializeComponent();
        }
        private string strCurrentBatchNo = "";
        private ValueList _VLProductSurfaceDefects;
        private ValueList _VLSizePrecision;
        private ValueList _VLTestResults;

        private ValueList _VLSteelType;
        private ValueList _VLSpec;
        private string strUserName = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
        private string strUserShift = Table_CA02_UserGroup.Static_T_CA02_UserGroup.GetUserGroupName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserGroup());
        private string strUserTerm = Table_CA02_UserOrder.Static_T_CA02_UserOrder.GetUserOrderName(CoreFS.SA06.CoreUserInfo.UserInfo.GetUserOrder());
        private string[] strOldInfo = new string[23];
        private string[] strNewInfo = new string[23];
        private string[] strHeadInfo = new string[23];
        UltraGridRow currentRow = null;


        BaseInfo m_BaseInfo = new BaseInfo();
        string strIP = "";
        string strMAC = "";

        private void SurfaceInspectionRecord_Load(object sender, EventArgs e)
        {
            this.dtp_Begin.Value = DateTime.Today;
            this.dtp_End.Value = DateTime.Today.AddDays(1).AddSeconds(-1);

            this._VLSizePrecision = new ValueList();
            BindToSizePrecision();
            this.ultraGrid1.DisplayLayout.Bands[0].Columns["FS_SIZEPRECISION"].ValueList = this._VLSizePrecision;


            this._VLProductSurfaceDefects = new ValueList();
            BindToProductSurfaceDefects();
            this.ultraGrid1.DisplayLayout.Bands[0].Columns["FS_WASTEPRODUCTTYPE"].ValueList = this._VLProductSurfaceDefects;

            this._VLTestResults = new ValueList();
            BindToTestResults();
            this.ultraGrid1.DisplayLayout.Bands[0].Columns["FS_TESTRESULTS"].ValueList = this._VLTestResults;

            this._VLSteelType = new ValueList();
            this._VLSpec = new ValueList();

            strIP = m_BaseInfo.getIPAddress();
            strMAC = m_BaseInfo.getMACAddress();
            saveHeadInfo();

            this.txtBeginBatch.Enabled = false;
            this.txtEndBatch.Enabled = false;
            this.cbBatchNo.Checked = false;
        }
        private void saveHeadInfo()
        {
            strHeadInfo[0] = "横肋外径1";
            strHeadInfo[1] = "横肋外径2";
            strHeadInfo[2] = "纵肋外径1";
            strHeadInfo[3] = "纵肋外径2";
            strHeadInfo[4] = "内径1";
            strHeadInfo[5] = "内径2";
            strHeadInfo[6] = "肋间距1";
            strHeadInfo[7] = "肋间距2";
            strHeadInfo[8] = "长度1";
            strHeadInfo[9] = "长度2";
            strHeadInfo[10] = "直径1";
            strHeadInfo[11] = "直径2";
            strHeadInfo[12] = "尺寸精度";
            strHeadInfo[13] = "每米弯曲度";
            strHeadInfo[14] = "合格量";
            strHeadInfo[15] = "废品分类";
            strHeadInfo[16] = "废品量";
            strHeadInfo[17] = "备注";
            strHeadInfo[18] = "直径3";
            strHeadInfo[19] = "直径4";
            strHeadInfo[20] = "检验结果";
            strHeadInfo[21] = "改判牌号";
            strHeadInfo[22] = "改判规格";

        }
        private void saveOldInfo(UltraGridRow row)
        {
            strOldInfo[0] = row.Cells["FN_TRANSVERRSEDIAMETER1"].Value.ToString();
            strOldInfo[1] = row.Cells["FN_TRANSVERRSEDIAMETER2"].Value.ToString();
            strOldInfo[2] = row.Cells["FN_LONGITUDINALDIAMETER1"].Value.ToString();
            strOldInfo[3] = row.Cells["FN_LONGITUDINALDIAMETER2"].Value.ToString();
            strOldInfo[4] = row.Cells["FN_INSIDEDIAMETER1"].Value.ToString();
            strOldInfo[5] = row.Cells["FN_INSIDEDIAMETER2"].Value.ToString();
            strOldInfo[6] = row.Cells["FN_RIBSPACING1"].Value.ToString();
            strOldInfo[7] = row.Cells["FN_RIBSPACING2"].Value.ToString();
            strOldInfo[8] = row.Cells["FN_LENGTH1"].Value.ToString();
            strOldInfo[9] = row.Cells["FN_LENGTH2"].Value.ToString();
            strOldInfo[10] = row.Cells["FN_DIAMETER1"].Value.ToString();
            strOldInfo[11] = row.Cells["FN_DIAMETER2"].Value.ToString();
            strOldInfo[12] = row.Cells["FS_SIZEPRECISION"].Value.ToString();
            strOldInfo[13] = row.Cells["FS_EACHMETERBEND"].Value.ToString();
            strOldInfo[14] = row.Cells["FN_QUALIFIEDRATE"].Value.ToString();
            strOldInfo[15] = row.Cells["FS_WASTEPRODUCTTYPE"].Value.ToString();
            strOldInfo[16] = row.Cells["FN_WASTEPRODUCTRATE"].Value.ToString();
            strOldInfo[17] = row.Cells["FS_MEMO"].Value.ToString();
            strOldInfo[18] = row.Cells["FN_DIAMETER3"].Value.ToString();
            strOldInfo[19] = row.Cells["FN_DIAMETER4"].Value.ToString();
            strOldInfo[20] = row.Cells["FS_TESTRESULTS"].Value.ToString();
            strOldInfo[21] = row.Cells["FS_COMMUTEDSTEELTYPE"].Value.ToString();
            strOldInfo[22] = row.Cells["FS_COMMUTEDSPEC"].Value.ToString();
        }
        private void saveNewInfo(UltraGridRow row)
        {
            //strNewInfo[0] = this.ugcpFN_TRANSVERRSEDIAMETER1.Text;
            //strNewInfo[1] = this.ugcpFN_TRANSVERRSEDIAMETER2.Text;
            //strNewInfo[2] = this.ugcpFN_LONGITUDINALDIAMETER1.Text;
            //strNewInfo[3] = this.ugcpFN_LONGITUDINALDIAMETER2.Text;
            //strNewInfo[4] = this.ugcpFN_INSIDEDIAMETER1.Text;
            //strNewInfo[5] = this.ugcpFN_INSIDEDIAMETER2.Text;
            //strNewInfo[6] = this.ugcpFN_RIBSPACING1.Text;
            //strNewInfo[7] = this.ugcpFN_RIBSPACING2.Text;
            //strNewInfo[8] = this.ugcpFN_LENGTH1.Text;
            //strNewInfo[9] = this.ugcpFN_LENGTH2.Text;
            //strNewInfo[10] = this.ugcpFN_DIAMETER1.Text;
            //strNewInfo[11] = this.ugcpFN_DIAMETER2.Text;
            //strNewInfo[12] = this.ugcpFS_SIZEPRECISION.Text;
            //strNewInfo[13] = this.ugcpFS_EACHMETERBEND.Text;
            //strNewInfo[14] = this.ugcpFN_QUALIFIEDRATE.Text;
            //strNewInfo[15] = this.ugcpFS_WASTEPRODUCTTYPE.Text;
            //strNewInfo[16] = this.ugcpFN_WASTEPRODUCTRATE.Text;
            //strNewInfo[17] = this.ugcpFS_MEMO.Text;

            strNewInfo[0] = row.Cells["FN_TRANSVERRSEDIAMETER1"].Value.ToString();
            strNewInfo[1] = row.Cells["FN_TRANSVERRSEDIAMETER2"].Value.ToString();
            strNewInfo[2] = row.Cells["FN_LONGITUDINALDIAMETER1"].Value.ToString();
            strNewInfo[3] = row.Cells["FN_LONGITUDINALDIAMETER2"].Value.ToString();
            strNewInfo[4] = row.Cells["FN_INSIDEDIAMETER1"].Value.ToString();
            strNewInfo[5] = row.Cells["FN_INSIDEDIAMETER2"].Value.ToString();
            strNewInfo[6] = row.Cells["FN_RIBSPACING1"].Value.ToString();
            strNewInfo[7] = row.Cells["FN_RIBSPACING2"].Value.ToString();
            strNewInfo[8] = row.Cells["FN_LENGTH1"].Value.ToString();
            strNewInfo[9] = row.Cells["FN_LENGTH2"].Value.ToString();
            strNewInfo[10] = row.Cells["FN_DIAMETER1"].Value.ToString();
            strNewInfo[11] = row.Cells["FN_DIAMETER2"].Value.ToString();
            strNewInfo[12] = row.Cells["FS_SIZEPRECISION"].Value.ToString();
            strNewInfo[13] = row.Cells["FS_EACHMETERBEND"].Value.ToString();
            strNewInfo[14] = row.Cells["FN_QUALIFIEDRATE"].Value.ToString();
            strNewInfo[15] = row.Cells["FS_WASTEPRODUCTTYPE"].Value.ToString();
            strNewInfo[16] = row.Cells["FN_WASTEPRODUCTRATE"].Value.ToString();
            strNewInfo[17] = row.Cells["FS_MEMO"].Value.ToString();
            strNewInfo[18] = row.Cells["FN_DIAMETER3"].Value.ToString();
            strNewInfo[19] = row.Cells["FN_DIAMETER4"].Value.ToString();
            strNewInfo[20] = row.Cells["FS_TESTRESULTS"].Value.ToString();
            strNewInfo[21] = row.Cells["FS_COMMUTEDSTEELTYPE"].Value.ToString();
            strNewInfo[22] = row.Cells["FS_COMMUTEDSPEC"].Value.ToString();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "bt_Query":
                    doQuery();

                    break;
                case "btnExport":
                    doExport();
                    break;
                default:
                    break;
            }
        }
        private void doQuery()
        {
            if (!ParseTime(this.dtp_Begin.Value, this.dtp_End.Value) && !this.cbBatchNo.Checked)
            {
                return;
            }
            this.dataSet1.Tables[1].Clear();
            this.dataSet1.Tables[0].Clear();
            if (this.cb_ProductionLine.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请选择产线类型！");
                return;
            }
            string strSql = "";
            switch (cb_ProductionLine.Text)
            {
                case "线材":
                    strSql = @"select t.FS_BATCHNO as FS_BATCHNO,
                                   t.FS_SPEC as FS_SPEC,
                                   t.FN_BANDCOUNT as FN_BANDCOUNT,
                                   t.FN_TOTALWEIGHT as FN_TOTALWEIGHT,
                                   to_char(t.FD_ENDTIME,'yyyy-MM-dd hh24:mi:ss') as FD_ENDTIME,
                                   t.FS_STEELTYPE,
                                   s.FN_TRANSVERRSEDIAMETER1 as FN_TRANSVERRSEDIAMETER1,
                                   s.FN_TRANSVERRSEDIAMETER2 as FN_TRANSVERRSEDIAMETER2,
                                   s.FN_LONGITUDINALDIAMETER1 as FN_LONGITUDINALDIAMETER1,
                                   s.FN_LONGITUDINALDIAMETER2 as FN_LONGITUDINALDIAMETER2,
                                   s.FN_INSIDEDIAMETER1 as FN_INSIDEDIAMETER1,
                                   s.FN_INSIDEDIAMETER2 as FN_INSIDEDIAMETER2,
                                   s.FN_RIBSPACING1 as FN_RIBSPACING1,
                                   s.FN_RIBSPACING2 as FN_RIBSPACING2,
                                   s.FN_LENGTH1 as FN_LENGTH1,
                                   s.FN_LENGTH2 as FN_LENGTH2,
                                   s.FN_DIAMETER1 as FN_DIAMETER1,
                                   s.FN_DIAMETER2 as FN_DIAMETER2,
                                   s.FS_SIZEPRECISION as FS_SIZEPRECISION,
                                   s.FS_EACHMETERBEND as FS_EACHMETERBEND,
                                   s.FN_QUALIFIEDRATE as FN_QUALIFIEDRATE,
                                   s.FS_WASTEPRODUCTTYPE as FS_WASTEPRODUCTTYPE,
                                   s.FN_WASTEPRODUCTRATE as FN_WASTEPRODUCTRATE,
                                   s.FS_MEMO as FS_MEMO,
                                    s.FS_CREATEPERSON as FS_CREATEPERSON,
                                    s.FS_CREATESHIFT as FS_CREATESHIFT,
                                    s.FS_CREATETERM as FS_CREATETERM,
                                    to_char(s.FS_CREATEDATETIME,'yyyy-MM-dd hh24:mi:ss') as FS_CREATEDATETIME,
                                   to_char(round((s.FN_LONGITUDINALDIAMETER1-s.FN_INSIDEDIAMETER1)/2,1),'990.9') as FN_LONGITUDINALRIBHEIGHT1,
                                   to_char(round((s.FN_LONGITUDINALDIAMETER2-s.FN_INSIDEDIAMETER2)/2,1),'990.9') as FN_LONGITUDINALRIBHEIGHT2,
                                   to_char(round((s.FN_TRANSVERRSEDIAMETER1-s.FN_INSIDEDIAMETER1)/2,1),'990.9') as FN_TRANSVERSERIBHEIGHT1,
                                   to_char(round((s.FN_TRANSVERRSEDIAMETER2-s.FN_INSIDEDIAMETER2)/2,1),'990.9') as FN_TRANSVERSERIBHEIGHT2,
                                   abs(case when upper(t.fs_steeltype) = 'HPB300' then to_char(round((s.FN_DIAMETER1-s.FN_DIAMETER2),1),'990.9') else to_char(round((s.FN_DIAMETER1-s.FN_DIAMETER2),2),'990.99') end) as FN_NONROUNDNESS,
                                        FN_DIAMETER3,
                                        FN_DIAMETER4,
                                        FS_TESTRESULTS,
                                   abs(case
                                         when upper(t.fs_steeltype) = 'HPB300' then
                                          to_char(round((s.FN_DIAMETER3 - s.FN_DIAMETER4), 1),'990.9')
                                         else
                                          to_char(round((s.FN_DIAMETER3 - s.FN_DIAMETER4), 2),'990.99')
                                       end) as FN_NONROUNDNESS2,
                                        c.FN_GPYS_WEIGHT as FN_GPYS_WEIGHT,
                                     s.FS_COMMUTEDSTEELTYPE as FS_COMMUTEDSTEELTYPE,
                                     s.FS_COMMUTEDSPEC as FS_COMMUTEDSPEC
                              from DT_GX_STORAGEWEIGHTMAIN t
                              left join  DT_SURFACEINSPECTIONRECORDS s on t.fs_batchno = s.fs_batchno
                              left join it_fp_techcard c on c.fs_zc_batchno = t.fs_batchno
                               where 1=1 ";

                    if (this.cbBatchNo.Checked)
                    {
                        strSql += " and t.FS_BATCHNO >= '" + this.txtBeginBatch.Text + "' and t.FS_BATCHNO <= '" + this.txtEndBatch.Text + "'  order by t.FS_BATCHNO desc";
                    }
                    else
                    {
                        strSql += " and t.fd_endtime between to_date('" + this.dtp_Begin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + this.dtp_End.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')  order by t.FS_BATCHNO desc";
                    }
                    break;
                case "棒材":
                    strSql = @"select t.FS_BATCHNO as FS_BATCHNO,
                                   t.FS_SPEC as FS_SPEC,
                                    t.FN_BANDCOUNT as FN_BANDCOUNT,
                                   t.FN_TOTALWEIGHT as FN_TOTALWEIGHT,
                                    t.FN_THEORYTOTALWEIGHT as FN_THEORYTOTALWEIGHT,
                                   t.FS_STEELTYPE,
                                   to_char(t.FD_ENDTIME, 'yyyy-MM-dd hh24:mi:ss') as FD_ENDTIME,
                                   
                                   s.FN_TRANSVERRSEDIAMETER1 as FN_TRANSVERRSEDIAMETER1,
                                   s.FN_TRANSVERRSEDIAMETER2 as FN_TRANSVERRSEDIAMETER2,
                                   s.FN_LONGITUDINALDIAMETER1 as FN_LONGITUDINALDIAMETER1,
                                   s.FN_LONGITUDINALDIAMETER2 as FN_LONGITUDINALDIAMETER2,
                                   s.FN_INSIDEDIAMETER1 as FN_INSIDEDIAMETER1,
                                   s.FN_INSIDEDIAMETER2 as FN_INSIDEDIAMETER2,
                                   s.FN_RIBSPACING1 as FN_RIBSPACING1,
                                   s.FN_RIBSPACING2 as FN_RIBSPACING2,
                                   s.FN_LENGTH1 as FN_LENGTH1,
                                   s.FN_LENGTH2 as FN_LENGTH2,
                                   s.FN_DIAMETER1 as FN_DIAMETER1,
                                   s.FN_DIAMETER2 as FN_DIAMETER2,
                                   s.FS_SIZEPRECISION as FS_SIZEPRECISION,
                                   s.FS_EACHMETERBEND as FS_EACHMETERBEND,
                                   s.FN_QUALIFIEDRATE as FN_QUALIFIEDRATE,
                                   s.FS_WASTEPRODUCTTYPE as FS_WASTEPRODUCTTYPE,
                                   s.FN_WASTEPRODUCTRATE as FN_WASTEPRODUCTRATE,
                                   s.FS_MEMO as FS_MEMO,
                                    s.FS_CREATEPERSON as FS_CREATEPERSON,
                                    s.FS_CREATESHIFT as FS_CREATESHIFT,
                                    s.FS_CREATETERM as FS_CREATETERM,
                                    to_char(s.FS_CREATEDATETIME,'yyyy-MM-dd hh24:mi:ss') as FS_CREATEDATETIME,
                                   to_char(round((s.FN_LONGITUDINALDIAMETER1 - s.FN_INSIDEDIAMETER1) / 2, 1),'990.9') as FN_LONGITUDINALRIBHEIGHT1,
                                   to_char(round((s.FN_LONGITUDINALDIAMETER2 - s.FN_INSIDEDIAMETER2) / 2, 1),'990.9') as FN_LONGITUDINALRIBHEIGHT2,
                                   to_char(round((s.FN_TRANSVERRSEDIAMETER1 - s.FN_INSIDEDIAMETER1) / 2, 1),'990.9') as FN_TRANSVERSERIBHEIGHT1,
                                   to_char(round((s.FN_TRANSVERRSEDIAMETER2 - s.FN_INSIDEDIAMETER2) / 2, 1),'990.9') as FN_TRANSVERSERIBHEIGHT2,
                                   abs(case
                                         when upper(t.fs_steeltype) = 'HPB300' then
                                          to_char(round((s.FN_DIAMETER1 - s.FN_DIAMETER2), 1),'990.9')
                                         else
                                          to_char(round((s.FN_DIAMETER1 - s.FN_DIAMETER2), 2),'990.99')
                                       end) as FN_NONROUNDNESS,
                                        FN_DIAMETER3,
                                        FN_DIAMETER4,
                                        FS_TESTRESULTS,
                                   abs(case
                                         when upper(t.fs_steeltype) = 'HPB300' then
                                          to_char(round((s.FN_DIAMETER3 - s.FN_DIAMETER4), 1),'990.9')
                                         else
                                          to_char(round((s.FN_DIAMETER3 - s.FN_DIAMETER4), 2),'990.99')
                                       end) as FN_NONROUNDNESS2,
                                     c.FN_GPYS_WEIGHT as FN_GPYS_WEIGHT,
                                     s.FS_COMMUTEDSTEELTYPE as FS_COMMUTEDSTEELTYPE,
                                     s.FS_COMMUTEDSPEC as FS_COMMUTEDSPEC
                              from DT_PRODUCTWEIGHTMAIN t
                              left join DT_SURFACEINSPECTIONRECORDS s on t.fs_batchno = s.fs_batchno
                              left join it_fp_techcard c on c.fs_zc_batchno = t.fs_batchno     
                              where t.fs_batchno like 'N%' ";
                    if (this.cbBatchNo.Checked)
                    {
                        strSql += " and t.FS_BATCHNO >= '" + this.txtBeginBatch.Text + "' and t.FS_BATCHNO <= '" + this.txtEndBatch.Text + "' order by t.FS_BATCHNO desc";
                    }
                    else
                    {
                        strSql += " and t.fd_endtime between to_date('" + this.dtp_Begin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + this.dtp_End.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')  order by t.FS_BATCHNO desc";
                    }
                    break;
                case "650":
                    strSql = @"select t.FS_BATCHNO as FS_BATCHNO,
                                   t.FS_SPEC as FS_SPEC,
                                t.FN_BANDCOUNT as FN_BANDCOUNT,
                                   t.FN_TOTALWEIGHT as FN_TOTALWEIGHT,
                                    t.FN_THEORYTOTALWEIGHT as FN_THEORYTOTALWEIGHT,
                                   t.FS_STEELTYPE,
                                   to_char(t.FD_ENDTIME, 'yyyy-MM-dd hh24:mi:ss') as FD_ENDTIME,
                                   
                                   s.FN_TRANSVERRSEDIAMETER1 as FN_TRANSVERRSEDIAMETER1,
                                   s.FN_TRANSVERRSEDIAMETER2 as FN_TRANSVERRSEDIAMETER2,
                                   s.FN_LONGITUDINALDIAMETER1 as FN_LONGITUDINALDIAMETER1,
                                   s.FN_LONGITUDINALDIAMETER2 as FN_LONGITUDINALDIAMETER2,
                                   s.FN_INSIDEDIAMETER1 as FN_INSIDEDIAMETER1,
                                   s.FN_INSIDEDIAMETER2 as FN_INSIDEDIAMETER2,
                                   s.FN_RIBSPACING1 as FN_RIBSPACING1,
                                   s.FN_RIBSPACING2 as FN_RIBSPACING2,
                                   s.FN_LENGTH1 as FN_LENGTH1,
                                   s.FN_LENGTH2 as FN_LENGTH2,
                                   s.FN_DIAMETER1 as FN_DIAMETER1,
                                   s.FN_DIAMETER2 as FN_DIAMETER2,
                                   s.FS_SIZEPRECISION as FS_SIZEPRECISION,
                                   s.FS_EACHMETERBEND as FS_EACHMETERBEND,
                                   s.FN_QUALIFIEDRATE as FN_QUALIFIEDRATE,
                                   s.FS_WASTEPRODUCTTYPE as FS_WASTEPRODUCTTYPE,
                                   s.FN_WASTEPRODUCTRATE as FN_WASTEPRODUCTRATE,
                                   s.FS_MEMO as FS_MEMO,
                                    s.FS_CREATEPERSON as FS_CREATEPERSON,
                                    s.FS_CREATESHIFT as FS_CREATESHIFT,
                                    s.FS_CREATETERM as FS_CREATETERM,
                                    to_char(s.FS_CREATEDATETIME,'yyyy-MM-dd hh24:mi:ss') as FS_CREATEDATETIME,
                                   to_char(round((s.FN_LONGITUDINALDIAMETER1 - s.FN_INSIDEDIAMETER1) / 2, 1),'990.9') as FN_LONGITUDINALRIBHEIGHT1,
                                   to_char(round((s.FN_LONGITUDINALDIAMETER2 - s.FN_INSIDEDIAMETER2) / 2, 1),'990.9') as FN_LONGITUDINALRIBHEIGHT2,
                                   to_char(round((s.FN_TRANSVERRSEDIAMETER1 - s.FN_INSIDEDIAMETER1) / 2, 1),'990.9') as FN_TRANSVERSERIBHEIGHT1,
                                   to_char(round((s.FN_TRANSVERRSEDIAMETER2 - s.FN_INSIDEDIAMETER2) / 2, 1),'990.9') as FN_TRANSVERSERIBHEIGHT2,
                                   abs(case
                                         when upper(t.fs_steeltype) = 'HPB300' then
                                          to_char(round((s.FN_DIAMETER1 - s.FN_DIAMETER2), 1),'990.9')
                                         else
                                          to_char(round((s.FN_DIAMETER1 - s.FN_DIAMETER2), 2),'990.99')
                                       end) as FN_NONROUNDNESS,
                                        FN_DIAMETER3,
                                        FN_DIAMETER4,
                                        FS_TESTRESULTS,
                                   abs(case
                                         when upper(t.fs_steeltype) = 'HPB300' then
                                          to_char(round((s.FN_DIAMETER3 - s.FN_DIAMETER4), 1),'990.9')
                                         else
                                          to_char(round((s.FN_DIAMETER3 - s.FN_DIAMETER4), 2),'990.99')
                                       end) as FN_NONROUNDNESS2,
                                     c.FN_GPYS_WEIGHT as FN_GPYS_WEIGHT,
                                     s.FS_COMMUTEDSTEELTYPE as FS_COMMUTEDSTEELTYPE,
                                     s.FS_COMMUTEDSPEC as FS_COMMUTEDSPEC
                              from DT_PRODUCTWEIGHTMAIN t
                              left join DT_SURFACEINSPECTIONRECORDS s on t.fs_batchno = s.fs_batchno
                              left join it_fp_techcard c on c.fs_zc_batchno = t.fs_batchno
                             where t.fs_batchno like '1%' ";
                    if (this.cbBatchNo.Checked)
                    {
                        strSql += " and t.FS_BATCHNO >= '" + this.txtBeginBatch.Text + "' and t.FS_BATCHNO <= '" + this.txtEndBatch.Text + "' order by t.FS_BATCHNO desc";
                    }
                    else
                    {
                        strSql += " and t.fd_endtime between to_date('" + this.dtp_Begin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + this.dtp_End.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')  order by t.FS_BATCHNO desc";
                    }

                    break;
                default:
                    break;


            }
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";




            ccp.ServerParams = new object[] { strSql };
            dataSet1.Tables[0].Clear();
            ccp.SourceDataTable = dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            if (ccp.ReturnCode == 0)
            {
                if (this.dataSet1.Tables[0].Rows.Count > 0)
                {
                    doQueryDetail();
                    showRowsColor();
                    LoadSTEELTYPE();
                    LoadSPEC();
                }
            }
        }

        private void doQueryDetail()
        {
            string strTempWhere = "(";
            foreach (DataRow row in this.dataSet1.Tables[0].Rows)
            {
                strTempWhere += "'" + row["FS_BATCHNO"].ToString() + "',";
            }
            strTempWhere = strTempWhere.TrimEnd(',');
            strTempWhere += ")";
            if (this.cb_ProductionLine.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请选择产线类型！");
                return;
            }
            string strSql = "";
            switch (cb_ProductionLine.Text)
            {
                case "线材":
                    strSql = @"select FS_BATCHNO,
                               FN_BANDNO,
                               FN_HOOKNO,
                               FN_BANDBILLETCOUNT,
                               
                               FN_WEIGHT,
                               FS_PERSON,
                               FS_POINT,
                               to_char(t.FD_DATETIME, 'yyyy-MM-dd hh24:mi:ss') as FD_DATETIME,
                               FS_SHIFT,
                               FS_TERM,
                               FS_LABELID,
                               FN_DECWEIGHT,
                               FS_FLAG,                               
                               FS_ISPACKWIRE     
                          from dt_gx_storageweightdetail t
                               where t.FS_BATCHNO in " + strTempWhere + " order by FN_BANDNO asc";

                    //if (this.cbBatchNo.Checked)
                    //{
                    //    strSql += " and t.FS_BATCHNO >= '" + this.txtBeginBatch.Text + "' and t.FS_BATCHNO <= '" + this.txtEndBatch.Text + "'";
                    //}
                    //else
                    //{
                    //    strSql += " and t.FD_DATETIME between to_date('" + this.dtp_Begin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + this.dtp_End.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')";
                    //}
                    break;
                case "棒材":
                    strSql = @"Select FS_BATCHNO,
                               FN_BANDNO,
                               FN_HOOKNO,
                               FN_BANDBILLETCOUNT,                               
                               FS_TYPE,
                               FN_LENGTH,                               
                               FN_WEIGHT,
                               FS_PERSON,
                               FS_POINT,
                               to_char(t.FD_DATETIME, 'yyyy-MM-dd hh24:mi:ss') as FD_DATETIME,
                               FS_SHIFT,
                               FS_TERM,
                               FS_LABELID,
                               FN_DECWEIGHT,
                               FS_FLAG,                               
                               FN_THEORYWEIGHT,
                               round(((FN_WEIGHT-FN_THEORYWEIGHT)/FN_THEORYWEIGHT)*100,2) || '%' as FN_WEIGHTDEVIATION
                          from dt_productweightdetail t
                             where t.FS_BATCHNO in " + strTempWhere + " order by FN_BANDNO asc";
                    //if (this.cbBatchNo.Checked)
                    //{
                    //    strSql += " and t.FS_BATCHNO >= '" + this.txtBeginBatch.Text + "' and t.FS_BATCHNO <= '" + this.txtEndBatch.Text + "'";
                    //}
                    //else
                    //{
                    //    strSql += " and t.FD_DATETIME between to_date('" + this.dtp_Begin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + this.dtp_End.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')";
                    //}
                    break;
                case "650":
                    strSql = @"Select FS_BATCHNO,
                               FN_BANDNO,
                               FN_HOOKNO,
                               FN_BANDBILLETCOUNT,                               
                               FS_TYPE,
                               FN_LENGTH,                               
                               FN_WEIGHT,
                               FS_PERSON,
                               FS_POINT,
                               to_char(t.FD_DATETIME, 'yyyy-MM-dd hh24:mi:ss') as FD_DATETIME,
                               FS_SHIFT,
                               FS_TERM,
                               FS_LABELID,
                               FN_DECWEIGHT,
                               FS_FLAG,                               
                               FN_THEORYWEIGHT,
                               round(((FN_WEIGHT-FN_THEORYWEIGHT)/FN_THEORYWEIGHT)*100,2) || '%' as FN_WEIGHTDEVIATION
                          from dt_productweightdetail t
                             where t.FS_BATCHNO in " + strTempWhere + " order by FN_BANDNO asc";
                    //if (this.cbBatchNo.Checked)
                    //{
                    //    strSql += " and t.FS_BATCHNO >= '" + this.txtBeginBatch.Text + "' and t.FS_BATCHNO <= '" + this.txtEndBatch.Text + "'";
                    //}
                    //else
                    //{
                    //    strSql += " and t.FD_DATETIME between to_date('" + this.dtp_Begin.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-MM-dd hh24:mi:ss') and to_date('" + this.dtp_End.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss')";
                    //}

                    break;
                default:
                    break;


            }
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";




            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";









            ccp.ServerParams = new object[] { strSql };
            dataSet1.Tables[1].Clear();
            ccp.SourceDataTable = dataSet1.Tables[1];
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            //Constant.RefreshAndAutoSize(ultraGrid1);
        }
        private void doExport()
        {
            ExportDataWithSaveDialog2(ref this.ultraGrid1, "棒线材表面检验记录");
        }






        private int checkIsExist()
        {
            DataTable dt = new DataTable();
            string strSql = "select  t.fs_batchno from dt_surfaceinspectionrecords t where t.fs_batchno = '" + this.strCurrentBatchNo + "'";
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";





            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";
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
        private bool doCheck()
        {
            if (this.ugcpFN_TRANSVERRSEDIAMETER1.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_TRANSVERRSEDIAMETER1.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_TRANSVERRSEDIAMETER1.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_TRANSVERRSEDIAMETER2.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_TRANSVERRSEDIAMETER2.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_TRANSVERRSEDIAMETER2.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_LONGITUDINALDIAMETER1.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_LONGITUDINALDIAMETER1.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_LONGITUDINALDIAMETER1.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_LONGITUDINALDIAMETER2.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_LONGITUDINALDIAMETER2.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_LONGITUDINALDIAMETER2.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_INSIDEDIAMETER1.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_INSIDEDIAMETER1.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_INSIDEDIAMETER1.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_INSIDEDIAMETER2.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_INSIDEDIAMETER2.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_INSIDEDIAMETER2.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_RIBSPACING1.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_RIBSPACING1.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_RIBSPACING1.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_RIBSPACING2.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_RIBSPACING2.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_RIBSPACING2.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }

            if (this.ugcpFN_LENGTH1.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_LENGTH1.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_LENGTH1.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_LENGTH2.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_LENGTH2.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_LENGTH2.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }

            if (this.ugcpFN_DIAMETER1.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_DIAMETER1.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_DIAMETER1.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_DIAMETER2.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_DIAMETER2.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_DIAMETER2.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_DIAMETER3.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_DIAMETER3.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_DIAMETER3.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFN_DIAMETER4.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_DIAMETER4.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_DIAMETER4.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            //if (this.ugcpFN_QUALIFIEDRATE.Text != string.Empty)
            //{
            //    try
            //    {
            //        Convert.ToDouble(this.ugcpFN_QUALIFIEDRATE.Text);
            //    }
            //    catch (Exception ex)
            //    {
            //        this.ugcpFN_QUALIFIEDRATE.Focus();
            //        MessageBox.Show("请输入数值类型的数据！");
            //        return true;
            //    }
            //}
            if (this.ugcpFN_WASTEPRODUCTRATE.Text != string.Empty)
            {
                try
                {
                    Convert.ToDouble(this.ugcpFN_WASTEPRODUCTRATE.Text);
                }
                catch (Exception ex)
                {
                    this.ugcpFN_WASTEPRODUCTRATE.Focus();
                    MessageBox.Show("请输入数值类型的数据！");
                    return true;
                }
            }
            if (this.ugcpFS_TESTRESULTS.Text == string.Empty)
            {
                this.ugcpFS_TESTRESULTS.Focus();
                MessageBox.Show("请选择检验结果！");
                return true;
            }
            return false;
        }
        /// <summary>
        /// 废品分类
        /// </summary>
        public void BindToProductSurfaceDefects()
        {
            DataTable dt = new DataTable();
            string strSql = "select t.FS_ID,FS_NAME from bt_productsurfacedefects t";
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";


            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";


            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dt;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            //DataView dataView = dtTemp.DefaultView;
            //DataTable dataTableDistinct = dataView.ToTable(true, "FS_MATERIAL", "FS_MATERIALNAME");//注：其中ToTable（）的第一个参数为是否DISTINCT
            this._VLProductSurfaceDefects.ValueListItems.Clear();
            foreach (DataRow row in dt.Rows)
            {
                this._VLProductSurfaceDefects.ValueListItems.Add(row["FS_ID"].ToString(), row["FS_NAME"].ToString());
            }
        }

        /// <summary>
        /// 尺寸精度
        /// </summary>
        public void BindToSizePrecision()
        {

            this._VLSizePrecision.ValueListItems.Clear();
            this._VLSizePrecision.ValueListItems.Add("C", "C");
            this._VLSizePrecision.ValueListItems.Add("B", "B");
            this._VLSizePrecision.ValueListItems.Add("A", "A");
        }


        public void BindToTestResults()
        {

            this._VLTestResults.ValueListItems.Clear();
            this._VLTestResults.ValueListItems.Add("合格", "合格");
            this._VLTestResults.ValueListItems.Add("不合格", "不合格");
            this._VLTestResults.ValueListItems.Add("待处理", "待处理");
        }

        //private void ultraGrid1_BeforeRowEditTemplateClosed(object sender, BeforeRowEditTemplateClosedEventArgs e)
        //{
        //    saveNewInfo(e.Template.Row);
        //}

        private void cbBatchNo_CheckStateChanged(object sender, EventArgs e)
        {
            if (cbBatchNo.Checked)
            {
                this.txtBeginBatch.Enabled = true;
                this.txtEndBatch.Enabled = true;
            }
            else
            {
                this.txtBeginBatch.Enabled = false;
                this.txtEndBatch.Enabled = false;
            }
        }

        private void showRowsColor()
        {
            foreach (UltraGridRow row in this.ultraGrid1.Rows)
            {
                if (row.HasChild())
                {
                    row.Cells["FN_LENGTH"].Value = row.ChildBands[0].Rows[0].Cells["FN_LENGTH"].Value;
                }

                if (row.Cells["FS_TESTRESULTS"].Value.ToString() == "不合格")
                {
                    row.Appearance.BackColor = System.Drawing.Color.Red;
                }
                else if (row.Cells["FS_TESTRESULTS"].Value.ToString() == "待处理")
                {
                    row.Appearance.BackColor = System.Drawing.Color.DarkOrange;
                }
            }
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

        private void UpdateTechCard()
        {
            string sql = "UPDATE IT_FP_TECHCARD SET ";
            sql += " FN_JZ_NUM = case when Length('" + currentRow.Cells["FN_BANDCOUNT"].Value.ToString() + "') > 0 then '" + currentRow.Cells["FN_BANDCOUNT"].Value.ToString() + "' else '0' end";
            sql += " ,FN_JZ_WEIGHT = case when Length('" + currentRow.Cells["FN_TOTALWEIGHT"].Value.ToString() + "') > 0 then '" + currentRow.Cells["FN_TOTALWEIGHT"].Value.ToString() + "' else '0' end";
            sql += " ,FN_JZ_RETURNWEIGHT = 0";
            sql += " ,FN_JZ_WASTWEIGHT = case when Length('" + this.ugcpFN_WASTEPRODUCTRATE.Text + "') > 0 then '" + this.ugcpFN_WASTEPRODUCTRATE.Text + "' else '0' end";
            sql += " ,FN_JZ_CHECKWEIGHT = case when Length('" + currentRow.Cells["FN_TOTALWEIGHT"].Value.ToString() + "') > 0 then '" + currentRow.Cells["FN_TOTALWEIGHT"].Value.ToString() + "' else '0' end";
            sql += " ,FS_JZ_CHECKER = '" + strUserName + "'";
            sql += " WHERE FS_ZC_BATCHNO = '" + currentRow.Cells["FS_BATCHNO"].Value.ToString() + "'";

            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteNonQuery";



            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";







            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }

        private void InsertTechcardMaintail()
        {
            string sql = "INSERT INTO IT_FP_TECHCARDMAINTAIL(NO,BATCHNO,REASONNAME,COUNT)";
            sql += " VALUES ('" + Guid.NewGuid().ToString() + "', '" + currentRow.Cells["FS_BATCHNO"].Value.ToString() + "', '"
                    + this.ugcpFS_WASTEPRODUCTTYPE.Text + "',case when Length('" + this.ugcpFN_WASTEPRODUCTRATE.Text + "') > 0 then '" + this.ugcpFN_WASTEPRODUCTRATE.Text + "' else '0' end)";
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteNonQuery";




            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";







            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

        }
        private void UpdateTechcardMaintail()
        {
            string sql = "UPDATE IT_FP_TECHCARDMAINTAIL SET ";
            sql += " REASONNAME = '" + this.ugcpFS_WASTEPRODUCTTYPE.Text + "'";
            sql += " ,COUNT = case when Length('" + this.ugcpFN_WASTEPRODUCTRATE.Text + "') > 0 then '" + this.ugcpFN_WASTEPRODUCTRATE.Text + "' else '0' end";
            sql += " WHERE BATCHNO = '" + currentRow.Cells["FS_BATCHNO"].Value.ToString() + "'";
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteNonQuery";


            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";


            ccp.ServerParams = new object[] { sql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
        }


        public bool ParseTime(DateTime beginTime, DateTime endTime)
        {
            //string strPlusTime = (endTime - beginTime).ToString().Substring(0, 2);
            double strTime = endTime.Subtract(beginTime).TotalDays;
            //int strTime = Convert.ToInt32(strPlusTime);
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
        public void BandSteelTypeAndSpec(UltraGridRow row)
        {
            if (row.Cells["FS_COMMUTEDSTEELTYPE"].Value.ToString() == string.Empty)
            {
                row.Cells["FS_COMMUTEDSTEELTYPE"].Value = row.Cells["FS_STEELTYPE"].Value;
            }
            if (row.Cells["FS_COMMUTEDSPEC"].Value.ToString() == string.Empty)
            {
                row.Cells["FS_COMMUTEDSPEC"].Value = row.Cells["FS_SPEC"].Value;
            }
        }

        private void LoadSTEELTYPE()
        {
            string strSql = "";
            switch (cb_ProductionLine.Text)
            {
                case "线材":
                    strSql = @"select distinct t.fs_steeltype as FS_STEELTYPE  from dt_gx_storageweightmain t";
                    break;
                case "棒材":
                    strSql = @"select distinct t.fs_steeltype as FS_STEELTYPE  from dt_productweightmain t";
                    break;
                case "650":
                    strSql = @"select distinct t.fs_steeltype as FS_STEELTYPE  from dt_productweightmain t";
                    break;
                default:
                    return;
            }

            DataTable dtTemp = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";


            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";







            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dtTemp;
            ccp = base.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            this._VLSteelType.ValueListItems.Clear();
            foreach (DataRow row in dtTemp.Rows)
            {
                this._VLSteelType.ValueListItems.Add(row["FS_STEELTYPE"].ToString(), row["FS_STEELTYPE"].ToString());
            }

            this.ultraGrid1.DisplayLayout.Bands[0].Columns["FS_COMMUTEDSTEELTYPE"].ValueList = this._VLSteelType;
        }
        private void LoadSPEC()
        {
            string strSql = "";
            switch (cb_ProductionLine.Text)
            {
                case "线材":
                    strSql = @"select distinct t.fs_spec as FS_SPEC  from dt_gx_storageweightmain t";
                    break;
                case "棒材":
                    strSql = @"select distinct t.fs_spec as FS_SPEC  from dt_productweightmain t";
                    break;
                case "650":
                    strSql = @"select distinct t.fs_spec as FS_SPEC  from dt_productweightmain t";
                    break;
                default:
                    return;
            }

            DataTable dtTemp = new DataTable();
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";



            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";







            ccp.ServerParams = new object[] { strSql };
            ccp.SourceDataTable = dtTemp;
            ccp = base.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            this._VLSpec.ValueListItems.Clear();
            foreach (DataRow row in dtTemp.Rows)
            {
                this._VLSpec.ValueListItems.Add(row["FS_SPEC"].ToString(), row["FS_SPEC"].ToString());
            }

            this.ultraGrid1.DisplayLayout.Bands[0].Columns["FS_COMMUTEDSPEC"].ValueList = this._VLSpec;
        }

        private void btnTemplateOk_Click(object sender, EventArgs e)
        {
            // This code was automatically generated by the RowEditTemplate Wizard
            // 
            // Close the template and save any pending changes

            if (doCheck())
            {
                return;
            }
            int intTemp = checkIsExist();
            string strDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strSql = "";
            switch (intTemp)
            {
                case 0:
                    strSql = @"insert into DT_SURFACEINSPECTIONRECORDS 
                                  (FS_BATCHNO,
                                   FN_TRANSVERRSEDIAMETER1,
                                   FN_TRANSVERRSEDIAMETER2,
                                   FN_LONGITUDINALDIAMETER1,
                                   FN_LONGITUDINALDIAMETER2,
                                   FN_INSIDEDIAMETER1,
                                   FN_INSIDEDIAMETER2,
                                   FN_RIBSPACING1,
                                   FN_RIBSPACING2,
                                   FN_LENGTH1,
                                   FN_LENGTH2,
                                   FN_DIAMETER1,
                                   FN_DIAMETER2,
                                   FS_SIZEPRECISION,
                                   FS_EACHMETERBEND,

                                   FS_WASTEPRODUCTTYPE,
                                   FN_WASTEPRODUCTRATE,
                                   FS_MEMO,FS_CREATEPERSON,FS_CREATESHIFT,FS_CREATETERM,FS_CREATEDATETIME,FN_DIAMETER3,
                                        FN_DIAMETER4,
                                        FS_TESTRESULTS,FS_COMMUTEDSTEELTYPE,FS_COMMUTEDSPEC) values
                                  ('" + strCurrentBatchNo + "',"
                                     + "case when Length('" + this.ugcpFN_TRANSVERRSEDIAMETER1.Text + "') > 0 then '" + this.ugcpFN_TRANSVERRSEDIAMETER1.Text + "' else '0' end,"
                                   + "case when Length('" + this.ugcpFN_TRANSVERRSEDIAMETER2.Text + "') > 0 then '" + this.ugcpFN_TRANSVERRSEDIAMETER2.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_LONGITUDINALDIAMETER1.Text + "') > 0 then '" + this.ugcpFN_LONGITUDINALDIAMETER1.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_LONGITUDINALDIAMETER2.Text + "') > 0 then '" + this.ugcpFN_LONGITUDINALDIAMETER2.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_INSIDEDIAMETER1.Text + "') > 0 then '" + this.ugcpFN_INSIDEDIAMETER1.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_INSIDEDIAMETER2.Text + "') > 0 then '" + this.ugcpFN_INSIDEDIAMETER2.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_RIBSPACING1.Text + "') > 0  then '" + this.ugcpFN_RIBSPACING1.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_RIBSPACING2.Text + "') > 0 then '" + this.ugcpFN_RIBSPACING2.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_LENGTH1.Text + "') > 0 then '" + this.ugcpFN_LENGTH1.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_LENGTH2.Text + "') > 0 then '" + this.ugcpFN_LENGTH2.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_DIAMETER1.Text + "') > 0 then '" + this.ugcpFN_DIAMETER1.Text + "' else '0' end,"
                                          + "case when Length('" + this.ugcpFN_DIAMETER2.Text + "') > 0 then '" + this.ugcpFN_DIAMETER2.Text + "' else '0' end,"
                                          + "'" + this.ugcpFS_SIZEPRECISION.Text + "',"
                                          + "'" + this.ugcpFS_EACHMETERBEND.Text + "',"

                                          + "'" + this.ugcpFS_WASTEPRODUCTTYPE.Text + "',"
                                          + "case when Length('" + this.ugcpFN_WASTEPRODUCTRATE.Text + "') > 0 then '" + this.ugcpFN_WASTEPRODUCTRATE.Text + "' else '0' end,"
                                          + "'" + this.ugcpFS_MEMO.Text + "','" + strUserName + "','" + strUserShift + "','" + strUserTerm + "',sysdate,'" + this.ugcpFN_DIAMETER3.Text + "','" + this.ugcpFN_DIAMETER4.Text + "','" + this.ugcpFS_TESTRESULTS.Text + "','" + this.ugcpFS_COMMUTEDSTEELTYPE.Text + "','" + this.ugcpFS_COMMUTEDSPEC.Text + "')";
                    break;
                case 1:
                    strSql = @"update DT_SURFACEINSPECTIONRECORDS set FN_TRANSVERRSEDIAMETER1=case when Length('" + this.ugcpFN_TRANSVERRSEDIAMETER1.Text + "') > 0 then '" + this.ugcpFN_TRANSVERRSEDIAMETER1.Text + "' else '0' end ,"
                        + "FN_TRANSVERRSEDIAMETER2 = case when Length('" + this.ugcpFN_TRANSVERRSEDIAMETER2.Text + "') > 0 then '" + this.ugcpFN_TRANSVERRSEDIAMETER2.Text + "' else '0' end,"
                         + "FN_LONGITUDINALDIAMETER1 = case when Length('" + this.ugcpFN_LONGITUDINALDIAMETER1.Text + "') > 0 then '" + this.ugcpFN_LONGITUDINALDIAMETER1.Text + "' else '0' end,"
                      + "FN_LONGITUDINALDIAMETER2 = case when Length('" + this.ugcpFN_LONGITUDINALDIAMETER2.Text + "') > 0 then '" + this.ugcpFN_LONGITUDINALDIAMETER2.Text + "' else '0' end,"
                      + "FN_INSIDEDIAMETER1 = case when Length('" + this.ugcpFN_INSIDEDIAMETER1.Text + "') > 0 then '" + this.ugcpFN_INSIDEDIAMETER1.Text + "' else '0' end,"
                      + "FN_INSIDEDIAMETER2 = case when Length('" + this.ugcpFN_INSIDEDIAMETER2.Text + "') > 0 then '" + this.ugcpFN_INSIDEDIAMETER2.Text + "' else '0' end,"
                      + "FN_RIBSPACING1 = case when Length('" + this.ugcpFN_RIBSPACING1.Text + "') > 0 then '" + this.ugcpFN_RIBSPACING1.Text + "' else '0' end,"
                      + "FN_RIBSPACING2 = case when Length('" + this.ugcpFN_RIBSPACING2.Text + "') > 0 then '" + this.ugcpFN_RIBSPACING2.Text + "' else '0' end,"
                      + "FN_LENGTH1 = case when Length('" + this.ugcpFN_LENGTH1.Text + "') > 0 then '" + this.ugcpFN_LENGTH1.Text + "' else '0' end,"
                      + "FN_LENGTH2 = case when Length('" + this.ugcpFN_LENGTH2.Text + "') > 0 then '" + this.ugcpFN_LENGTH2.Text + "' else '0' end,"
                      + "FN_DIAMETER1 = case when Length('" + this.ugcpFN_DIAMETER1.Text + "') > 0 then '" + this.ugcpFN_DIAMETER1.Text + "' else '0' end,"
                      + "FN_DIAMETER2 = case when Length('" + this.ugcpFN_DIAMETER2.Text + "') > 0 then '" + this.ugcpFN_DIAMETER2.Text + "' else '0' end,"
                      + "FS_SIZEPRECISION = '" + this.ugcpFS_SIZEPRECISION.Text + "',"
                      + "FS_EACHMETERBEND = '" + this.ugcpFS_EACHMETERBEND.Text + "',"

                      + "FS_WASTEPRODUCTTYPE = '" + this.ugcpFS_WASTEPRODUCTTYPE.Text + "',"
                      + "FN_WASTEPRODUCTRATE = case when Length('" + this.ugcpFN_WASTEPRODUCTRATE.Text + "') > 0 then '" + this.ugcpFN_WASTEPRODUCTRATE.Text + "' else '0' end,"
                      + "FN_DIAMETER3= case when Length('" + this.ugcpFN_DIAMETER3.Text + "') > 0 then '" + this.ugcpFN_DIAMETER3.Text + "' else '0' end,"
                      + "FN_DIAMETER4= case when Length('" + this.ugcpFN_DIAMETER4.Text + "') > 0 then '" + this.ugcpFN_DIAMETER4.Text + "' else '0' end,"
                       + "FS_TESTRESULTS='" + this.ugcpFS_TESTRESULTS.Text + "',"
                       + "FS_COMMUTEDSTEELTYPE='" + this.ugcpFS_COMMUTEDSTEELTYPE.Text + "',"
                       + "FS_COMMUTEDSPEC='" + this.ugcpFS_COMMUTEDSPEC.Text + "',"
                      + "FS_MEMO='" + this.ugcpFS_MEMO.Text + "' WHERE FS_BATCHNO = '" + this.strCurrentBatchNo + "'";
                    break;
                case -1:
                    MessageBox.Show("数据操作失败！");
                    break;
                default:
                    break;
            }
            CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteNonQuery";




            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteQuery";

            ccp.ServerName = "ygjzjl.base.QueryData";
            ccp.MethodName = "queryByClientSql";









            ccp.ServerParams = new object[] { strSql };
            ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                MessageBox.Show("数据操作成功！");
                UpdateTechCard();
                this.ultraGridRowEditTemplate1.Close(true);

                string m_Memo = "";
                if (intTemp == 1)
                {
                    for (int i = 0; i < strHeadInfo.Length; i++)
                    {
                        m_Memo += strHeadInfo[i] + ":" + strOldInfo[i] + "->" + strNewInfo[i] + ",";
                    }
                    //m_BaseInfo.insertLog(strDate, "修改", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/棒线产品外形表面质量检验记录");
                           this.insertLogs(strDate, "修改", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/棒线产品外形表面质量检验记录");


                    UpdateTechcardMaintail();
                }
                if (intTemp == 0)
                {
                    for (int i = 0; i < strHeadInfo.Length; i++)
                    {
                        m_Memo += strHeadInfo[i] + ":" + strNewInfo[i] + ",";
                    }
                    //m_BaseInfo.insertLog(strDate, "新增", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/棒线产品外形表面质量检验记录");
                    this.insertLogs(strDate, "新增", strUserName, strIP, strMAC, m_Memo, "", "", strCurrentBatchNo, "", "", "DT_SURFACEINSPECTIONRECORDS", "产品外表质量检验/棒线产品外形表面质量检验记录");

                    InsertTechcardMaintail();
                }

                doQuery();
            }
            else
            {
                MessageBox.Show("数据操作失败！");
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
        private void btnTemplateCancel_Click(object sender, EventArgs e)
        {
            this.ultraGridRowEditTemplate1.Close(false);
        }

        private void ultraGrid1_BeforeRowEditTemplateClosed(object sender, BeforeRowEditTemplateClosedEventArgs e)
        {
            saveNewInfo(e.Template.Row);
        }

        private void ultraGrid1_AfterRowEditTemplateDisplayed(object sender, AfterRowEditTemplateDisplayedEventArgs e)
        {
            currentRow = e.Template.Row;
            strCurrentBatchNo = e.Template.Row.Cells["FS_BATCHNO"].Value.ToString();
            saveOldInfo(e.Template.Row);
            BandSteelTypeAndSpec(e.Template.Row);
        }
    }
}
