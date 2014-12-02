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
    public partial class ZkdBpSapCx : FrmBase
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
        SapClass sapClass = null;

        #endregion

        public ZkdBpSapCx()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造查询条件
        /// </summary>
        private bool getStrWhere()
        {
            //MessageBox.Show((qDteBegin.Value - qDteEnd.Value).ToString());
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                MessageBox.Show("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }

            strWhere = " AND (FD_WEIGHTTIME BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
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

            if (txtLh.Text.Trim() != "")
            {
                strWhere += " AND FS_STOVENO LIKE '" + txtLh.Text.Trim() + "%'";
            }

            strWhere += " AND FS_UPLOADFLAG = '1'";

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

            strTmpTable = "dt_boardweightmain t";
            strTmpField = "TO_CHAR(t.FD_WEIGHTTIME,'yyyy-MM-dd HH24:mi:ss') AS GBSJ,t.FS_STOVENO,t.FS_PRODUCTNO,"
                + "t.FS_ITEMNO,t.FS_SAPSTORE,t.FS_UPLOADFLAG,t.FS_ACCOUNTDATE,t.FS_HEADER,"
               +"t.FS_STEELTYPE,t.FS_SPEC,t.FS_AUDITOR,t.FN_WEIGHT";

            //strTmpField += " (select count( r.fs_weightno) from dt_steelweightdetailroll r where r.fs_stoveno=t.fs_stoveno) as  FN_BILLETCOUNT,";
            //strTmpField += "(select sum( r.fn_netweight) from dt_steelweightdetailroll r where r.fs_stoveno=t.fs_stoveno)  as FN_TOTALWEIGHT";


            strTmpOrder = " ORDER BY t.FD_WEIGHTTIME";

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strWhere, strTmpOrder }; //

            dataSet1.Tables[0].Clear();
            ccpData.SourceDataTable = dataSet1.Tables[0];
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            catch (Exception ex1)
            {
                MessageBox.Show( "信息查询异常！");
            }
        }

        /// <summary>
        /// 钢坯信息冲销
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
                strTmpNo = "FS_STOVENO='" + strOptNo + "'";

                string strTmpTable, strTmpField;
                strTmpTable = "dt_boardweightmain";
                strTmpField = "FS_UPLOADFLAG='0',FS_ISMATCH='0'";

                sapClass.uptDataFlag(strTmpTable, strTmpField, strTmpNo);
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
            sapClass = new SapClass(this.ob);
            lblScr.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            getStrWhere();

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
                        if (Constant.WaitingForm != null)
                        Constant.WaitingForm.Close();
                        break;
                    }
            }
        }

        private void uGridData_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            uGridData.ActiveRow.Selected = true;
            if (uGridData.ActiveRow.Index == -1) return;
            lblScr.Text = "";
            lblOpt.Text = "当前操作：";
            strOptNo = uGridData.ActiveRow.Cells["FS_STOVENO"].Value.ToString();
            lblOpt.Text += strOptNo;
            lblScr.Text = "上传日志：" + sapClass.showSapLog(strOptNo);
        }
    }
}