using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using YGJZJL.PublicComponent;
using Infragistics.Win.UltraWinGrid;
using Excel;

namespace YGJZJL.Sap
{
    public partial class BcSapCx : FrmBase
    {
        #region 变量定义

        //使用公用对象
        BaseInfo objBi = new BaseInfo();

        //查询条件语句
        string strWhere = "";

        //本地缓存订单号信息
        System.Data.DataTable dtOrder = new System.Data.DataTable();

        //操作序号
        string strOptNo = "";


        //引入公用类
        SapClass sapClass = new SapClass();

        #endregion

        public BcSapCx()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造查询条件
        /// </summary>
        private bool getStrWhere()
        {
            if (qDteBegin.Value > qDteEnd.Value)
            {
                MessageBox.Show("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }

            strWhere = " AND (t.FD_STARTTIME BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
                + qDteEnd.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss'))";


            if (qTxtScdd.Text.Trim() != "")
            {
                if ((qTxtScdd.Text.Trim().Length != 12) && (qTxtScdd.Text.Trim().Length < 12))
                {
                    strWhere += " AND FS_PRODUCTNO LIKE '%" + qTxtScdd.Text + "%'";
                }
                if ((qTxtScdd.Text.Trim().Length == 12))
                {
                    strWhere += " AND FS_PRODUCTNO = '" + qTxtScdd.Text + "'";
                }
            }

            strWhere += " AND t.FS_UPLOADFLAG = '1'";

            if(qCmbLzh.Text.Trim()!="")
                strWhere += " AND t.FS_BATCHNO = '"+qCmbLzh.Text+"'";

            strWhere += " AND t.FS_SAPSTORE = '1311'";       
            return true;
        }

        /// <summary>
        /// 显示方坯信息
        /// </summary>
        private void showGridInfo()
        {
            string strTmpTable, strTmpField, strTmpOrder;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "";
            strTmpField = "";
            strTmpOrder = "";

            strTmpTable = "DT_PRODUCTWEIGHTMAIN t";
            strTmpField = "TO_CHAR(t.FD_ENDTIME,'yyyy-MM-dd HH24:mi:ss') AS GBSJ,t.FS_BATCHNO,t.FS_PRODUCTNO,"
                + "t.FS_ITEMNO,t.FS_SAPSTORE,t.FS_AUDITOR,t.FD_AUDITTIME,t.FS_ISMATCH,t.FS_UPLOADFLAG,"
                + "t.FS_ACCOUNTDATE,t.FS_STEELTYPE,t.FS_SPEC,t.FS_HEADER,";
            strTmpField += " (select sum( r.FN_WEIGHT) from DT_PRODUCTWEIGHTDETAIL r where r.FS_BATCHNO=t.FS_BATCHNO and r.FS_UPLOADFLAG='1') as  FN_TOTALWEIGHT,";
            strTmpField += "(select sum( r.FN_THEORYWEIGHT) from DT_PRODUCTWEIGHTDETAIL r where r.FS_BATCHNO=t.FS_BATCHNO and r.FS_UPLOADFLAG='1')  as FN_THEORYTOTALWEIGHT";
           
            strTmpOrder = " ORDER BY FD_ENDTIME";

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strWhere, strTmpOrder }; //

            dataSet1.Tables[0].Clear();
            ccpData.SourceDataTable = dataSet1.Tables[0];
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            catch (Exception)
            {
                MessageBox.Show( "信息查询异常！");
            }
        }

        /// <summary>
        /// 棒材信息冲销
        /// </summary>
        private void unLoadData()
        {
            if ((uGridData.Selected.Rows.Count == 0) || (strOptNo == ""))
            {
                MessageBox.Show("请先双击要修改的数据行，只能单行操作，不支持批量修改！");
                return;
            }

            string strTmpNo = "";

            if (DialogResult.Yes == MessageBox.Show("该操作将导致" + strOptNo
                + "上传标志被复位，可能导致SAP数据重复，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                strTmpNo = "FS_BATCHNO='" + strOptNo + "'";

                string strTmpTable, strTmpField;

                strTmpTable = "DT_PRODUCTWEIGHTMAIN";
                strTmpField = "FS_UPLOADFLAG='0',FS_ISMATCH='0'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.base.QueryData";
                ccp.MethodName = "check_UpdateInfo";

                ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpNo };

                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);


                CoreClientParam ccp2 = new CoreClientParam();
                strTmpTable = "DT_PRODUCTWEIGHTDETAIL";
                strTmpField = "FS_UPLOADFLAG='0',FS_ISMATCH='0'";

                ccp2.ServerName = "ygjzjl.base.QueryData";
                ccp2.MethodName = "check_UpdateInfo";

                ccp2.ServerParams = new object[] { strTmpTable, strTmpField, strTmpNo };

                this.ExecuteNonQuery(ccp2, CoreInvokeType.Internal);

                showGridInfo();
                lblScr.Text = "";
                lblOpt.Text = "当前操作：";
            }
        }

        
        private void UpSap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Control c = GetNextControl(this.ActiveControl, true);
                bool ok = SelectNextControl(this.ActiveControl, true, true, true, true);
                if (ok && c != null)
                {
                    if (c is System.Windows.Forms.TextBox)
                    {
                        ((System.Windows.Forms.TextBox)c).SelectAll();
                    }
                    if (c.TabIndex == 12) return;
                }
            }
        }

        private void UpSap_Load(object sender, EventArgs e)
        {
            lblScr.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            ugpData.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            strWhere = " AND (FD_STARTTIME BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
                + qDteEnd.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss'))";

            strWhere += " AND FS_UPLOADFLAG = '1'";

          /*  if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1002") || 
                (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1001"))
            {
                strWhere += " AND FS_BATCHNO LIKE ('2%')";
            }

            if (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC2001")
            {
                strWhere += " AND FS_BATCHNO LIKE ('1%')";
            }*/

            strWhere += " AND t.FS_SAPSTORE = '1311'";

            showGridInfo();
        }

        
        private void uToolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "find":
                    {
                        if (!getStrWhere())
                            break;
                        showGridInfo();
                        break;
                    }
                case "Reload":
                    {
                        unLoadData();
                        break;
                    }
                case "ToExcel":
                    {
                        Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, uGridData);
                        if(Constant.WaitingForm!=null)
                        Constant.WaitingForm.Close();
                        break;
                    }
            }
        }

        private void showMxsj(string sLh)
        {
            string strTmpTable, strTmpField, strTmpWhere, strTmpOrder;
            CoreClientParam ccpOrderInfo = new CoreClientParam();
            ccpOrderInfo.ServerName = "ygjzjl.base.QueryData";
            ccpOrderInfo.MethodName = "queryData";

            strTmpTable = "DT_PRODUCTWEIGHTDETAIL";
            strTmpField = "FS_BATCHNO,FN_BANDNO,FS_TYPE,FN_LENGTH,FN_WEIGHT,FN_BANDBILLETCOUNT,FD_DATETIME,FN_THEORYWEIGHT";
            strTmpWhere = " AND FS_BATCHNO = '" + sLh + "' and FS_UPLOADFLAG='1'";
            strTmpOrder = " ORDER BY FS_BATCHNO,FN_BANDNO";
            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            dataSet1.Tables[2].Clear();
            ccpOrderInfo.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, strTmpOrder };
            ccpOrderInfo.SourceDataTable = dataSet1.Tables[2];
            this.ExecuteQueryToDataTable(ccpOrderInfo, CoreInvokeType.Internal);
            //string strTmp = "明细数据：";
            //if (dtTmpData.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtTmpData.Rows.Count - 1; i++)
            //    {
            //        strTmp += " | " + dtTmpData.Rows[i]["FS_BATCHNO"].ToString();
            //        strTmp += "  " + dtTmpData.Rows[i]["FN_BANDNO"].ToString();
            //        strTmp += "  " + dtTmpData.Rows[i]["FS_TYPE"].ToString();
            //        strTmp += "  " + dtTmpData.Rows[i]["FN_WEIGHT"].ToString();
            //    }
            //    rtxtHint1.Text += strTmp + "\n";
            //}
        }

        private void showSapLog(string sLh)
        {
            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpOrderInfo = new CoreClientParam();
            ccpOrderInfo.ServerName = "ygjzjl.base.QueryData";
            ccpOrderInfo.MethodName = "queryData";

            strTmpTable = "IT_SAPLOG";
            strTmpField = "FS_DESCRIBE,FS_ORDERNO,FD_UPLOADTIME,FN_UPLOADNUM,FS_USER";
            strTmpWhere = " AND FS_BATCHNO = '" + sLh + "'";
            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpOrderInfo.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" };
            ccpOrderInfo.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpOrderInfo, CoreInvokeType.Internal);
            string strTmp = "";
            strTmp = dtTmpData.Rows[0]["FS_USER"].ToString() + "在" + dtTmpData.Rows[0]["FD_UPLOADTIME"].ToString();
            strTmp += "上传" + dtTmpData.Rows[0]["FS_DESCRIBE"].ToString() + "，";
            strTmp += "重量为：" + dtTmpData.Rows[0]["FN_UPLOADNUM"].ToString() + "。";

            lblScr.Text = "上传日志：" + strTmp;
        }

        private void uGridData_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            uGridData.ActiveRow.Selected = true;
            if (uGridData.ActiveRow.Index == -1) return;
            lblScr.Text = "";
            lblOpt.Text = "当前操作：";
            strOptNo = uGridData.ActiveRow.Cells["FS_BATCHNO"].Value.ToString();
            lblOpt.Text += strOptNo;
            showSapLog(strOptNo);
            showMxsj(strOptNo);
        }
    }
}