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
    public partial class GxSapFl : FrmBase
    {
        #region 变量定义

        //使用公用对象
        BaseInfo objBi = new BaseInfo();

        //查询条件语句
        string strWhere = "";

        //本地缓存表信息
        System.Data.DataTable dtOrder = new System.Data.DataTable();

        //操作序号
        string strOptNo = "";

        //引入公用类
        SapClass sapClass = null;

        #endregion

        public GxSapFl()
        {
            InitializeComponent();
        }

        #region 数据检查

        /// <summary>
        /// 检查用户信息是否正确
        /// </summary>
        private bool chkDataInfo(int iIndex)
        {
            if (uGridData.Rows[iIndex].Cells["FS_STOVENO"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行炉号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_PRODUCTNO"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行生产订单号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_PLANT"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行炉号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_SAPSTORE"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行生产订单号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_ACCOUNTDATE"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行生产订单号信息不能为空！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检查用户是否选择信息
        /// </summary>
        private bool chkSelectData()
        {
            if (uGridData.Rows.Count == 0)
            {
                MessageBox.Show("不存在可以操作的信息！");
                return false;
            }

            if (uGridData.Selected.Rows.Count == 0)
            {
                MessageBox.Show("请先选择需要操作的信息！");
                return false;
            }

            return true;
        }

        #endregion

        #region 订单号处理

        /// <summary>
        /// 根据订单号从SAP下载订单信息
        /// </summary>
        private void downOrderInfo(string sDdh)
        {
            lstHint2.Items.Clear();
           // string plantid="1100";
            CoreClientParam ccpProductNo = new CoreClientParam();
            ccpProductNo.ServerName = "ygjzjl.base.SapOperation";
            ccpProductNo.MethodName = "queryProductNo";
            ccpProductNo.ServerParams = new object[] { sDdh };
            dtOrder.Clear();
            ccpProductNo.SourceDataTable = dtOrder;
            this.ExecuteQueryToDataTable(ccpProductNo, CoreInvokeType.Internal);

            if (dtOrder.Rows.Count == 0)
            {
                lstHint2.Items.Add("生产订单信息下载失败！");
                return;
            }
            lstHint2.Items.Add("生产订单信息下载成功！");
        }

        /// <summary>
        /// 根据订单号查询物料信息
        /// </summary>
        private void getOrderInfo(string sDdh)
        {
            queryOrderInfo(sDdh);

            if (dtOrder.Rows.Count > 0) return;

            if (dtOrder.Rows.Count == 0)
            {
                lstHint2.Items.Add("生产订单信息获取失败，从SAP下载！");
                downOrderInfo(sDdh);
            }

            if (dtOrder.Rows.Count == 0)
            {
                lstHint2.Items.Add("生产订单信息获取失败！");
            }
            else
            {
                queryOrderInfo(sDdh);
            }
        }

        /// <summary>
        /// 根据订单号查询信息
        /// </summary>
        private void queryOrderInfo(string sDdh)
        {
            CoreClientParam ccpOrder = new CoreClientParam();
            ccpOrder.ServerName = "ygjzjl.base.QueryData";
            ccpOrder.MethodName = "query_DdInfo";
            ccpOrder.ServerParams = new object[] { "FS_PRODUCTNO ='"+sDdh+"' " };

            dtOrder.Clear();
            ccpOrder.SourceDataTable = dtOrder;
            this.ExecuteQueryToDataTable(ccpOrder, CoreInvokeType.Internal);
            if (dtOrder.Rows.Count > 0)
            {
                txtScdd.Text = sDdh;
                txtWlmc.Text = dtOrder.Rows[0]["FS_MATERIALNAME"].ToString();
                txtGc.Text = dtOrder.Rows[0]["FS_FACTORY"].ToString();
                txtHxmh.Text = dtOrder.Rows[0]["FS_ITEMNO"].ToString();
                txtWlbh.Text = dtOrder.Rows[0]["FS_MATERIAL"].ToString();
            }
        }


        #endregion

        /// <summary>
        /// 编辑数据信息
        /// </summary>
        private void doUpdate()
        {
            if ((txtScdd.Text == "") || (txtGc.Text == "") || (txtKcd.Text == ""))
            {
                MessageBox.Show("工厂、生产订单与库存地都不能为空！");
                return;
            }

            string strTmpTable, strTmpField, strTmpWhere;

            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    strOptNo = uGridData.Rows[ugr.Index].Cells["FS_STOVENO"].Value.ToString();

                    strTmpTable = "DT_SAP261";
                    strTmpField = "";
                    strTmpField = "FS_PRODUCTNO='" + txtScdd.Text + "',FS_ITEMNO='0001',FS_SAPSTORE='" + txtKcd.Text + "',"
                        + "FS_ACCOUNTDATE='" + dteJzrq.Value.ToString("yyyy-MM-dd") + "',FS_PLANT='"+ txtGc.Text +"',FS_HEADER='"
                        + cmbHead.Text + "',FS_AUDITOR='" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "'";
                    strTmpWhere = "FS_STOVENO='" + strOptNo + "'";

                    if (sapClass.uptData(strTmpTable, strTmpField, strTmpWhere))
                    {
                        lstHint2.Items.Add("数据修改成功！");
                    }
                    else
                    {
                        MessageBox.Show("数据修改失败！");
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选择要修改的数据行！");
            }
        }

        

        /// <summary>
        /// 构造查询
        /// </summary>
        private bool getStrWhere()
        {
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                lstHint1.Items.Add("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }

            strWhere = " AND (FS_ACCOUNTDATE BETWEEN '" + qDteBegin.Value.ToString("yyyy-MM-dd")  + "' AND　'"
                + qDteEnd.Value.ToString("yyyy-MM-dd") + "')";


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

            if (qTxtZzh.Text.Trim() != "")
            {
                strWhere += " AND FS_STOVENO LIKE '" + qTxtZzh.Text + "%'";
            }

            strWhere += " AND FS_UPLOADFLAG = '0'";

         //   strWhere += " AND FS_DRDW='棒线厂四作'";

            strWhere += " AND FS_DRDW='" + CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment() + "'";
            strWhere += " AND FS_SAPSTORE = '1310'";//玉钢线材库
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

            strTmpTable = "DT_SAP261";
            strTmpField = "FS_WEIGHTNO,FN_NETWEIGHT,FS_STOVENO,FS_UPLOADFLAG,FS_AUDITOR,FS_HEADER,FS_PRODUCTNO,"
                + "FS_PLANT,FS_SAPSTORE,FS_ACCOUNTDATE,FS_MATERIAL,FS_MATERIALNAME";
            strTmpOrder = " ORDER BY FS_STOVENO";


            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strWhere, strTmpOrder }; //
            dataSet1.Tables[0].Clear();
            ccpData.SourceDataTable = dataSet1.Tables[0];
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
                //Constant.RefreshAndAutoSize(uGridData);
            }
            catch (Exception ex1)
            {
                lstHint2.Items.Add(ex1.Message + "信息查询异常！");
            }
        }

        private void UpSap_KeyPress(object sender, KeyPressEventArgs e)
        {
            sapClass = new SapClass(this.ob);
            
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

       
        private void qTxtScdd_Leave(object sender, EventArgs e)
        {
           // sapClass = new SapClass(this.ob);
            if (qTxtScdd.Text.Trim() == "") return;
            if (!sapClass.chkScdd(txtScdd.Text))
            {
                txtScdd.Focus();
                return;
            }
        }

        private void txtScdd_Leave(object sender, EventArgs e)
        {
           // sapClass = new SapClass(this.ob);
            if (txtScdd.Text.Trim() == "") return;
            if (!sapClass.chkScdd(txtScdd.Text))
            {
                txtScdd.Focus();
                return;
            }

            getOrderInfo(txtScdd.Text);
        }


        private void txtLh_Leave(object sender, EventArgs e)
        {
            if (txtLh.Text.Trim() == "") return;
            if (txtLh.Text.Trim().Length > 10)
            {
                MessageBox.Show("批次号不能大于10位，请重新进行输入！");
                txtLh.Focus();
            }
        }

        private void txtGc_Leave(object sender, EventArgs e)
        {
            if (txtGc.Text.Trim() == "") return;
            if (txtGc.Text.Trim().Length != 4)
            {
                MessageBox.Show("工厂数据等于4位（1100），请重新进行输入！");
                txtGc.Focus();
            }
        }

        private void txtKcd_Leave(object sender, EventArgs e)
        {
            if (txtKcd.Text.Trim() == "") return;
            if (txtKcd.Text.Trim().Length != 4)
            {
                MessageBox.Show("库存地数据等于4位，请重新进行输入！");
                txtKcd.Focus();
            }
        }

        private void uToolBar_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            sapClass = new SapClass(this.ob);
            lstHint1.Items.Clear();
            lstHint2.Items.Clear();
            
            switch (e.Tool.Key.ToString())
            {
                //case "Cancel":
                //    {
                //        doUpdate(1);
                //        showGridInfo();
                //        break;
                //    }
                case "Find":
                    {
                        if (!getStrWhere())
                            break;
                        showGridInfo();
                        break;
                    }
                case "Edit":
                    {
                        doUpdate();
                        showGridInfo();
                        break;
                    }
                case "UpSap":
                    {
                        upLoadData();
                        break;
                    }
                case "ToExcel":
                    {
                        Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, uGridData);
                        if(Constant.WaitingForm!=null)
                        Constant.WaitingForm.Close();
                        break;
                    }
                case "InExcel":
                    {
                        ArrayList lstTmp = new ArrayList();
                       // lstTmp = sapClass.ExcelToDatabase(2, "棒线厂四作");
                        lstTmp = sapClass.ExcelToDatabase(2, CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment());
                        foreach (string strTmpValue in lstTmp)
                        {
                            lstHint2.Items.Add(strTmpValue);
                        }
                        showGridInfo();
                        break;
                    }
            }
        }

        private void uGridData_DoubleClickCell(object sender, Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs e)
        {
            uGridData.ActiveRow.Selected = true;
            if (uGridData.ActiveRow.Index == -1) return;
            try
            {
                dteJzrq.Value = Convert.ToDateTime(uGridData.ActiveRow.Cells["FS_ACCOUNTDATE"].Text);
            }
            catch (Exception)
            {
            }

            txtScdd.Text = uGridData.ActiveRow.Cells["FS_PRODUCTNO"].Text;
            txtHxmh.Text = "0001";          
            txtGc.Text = "1100";//1100是玉钢工厂
            txtKcd.Text = uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text != "" ? uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text : "8303";
            txtZl.Text = uGridData.ActiveRow.Cells["FN_NETWEIGHT"].Text;
            cmbHead.Text = uGridData.ActiveRow.Cells["FS_HEADER"].Text;
        }

        private string getSelectNo()
        {
            string strTmp = "FS_WEIGHTNO IN (";
            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    if (!chkDataInfo(ugr.Index)) continue;
                    if (strTmp == "FS_WEIGHTNO IN (")
                    {
                        strTmp += "'" + uGridData.Rows[ugr.Index].Cells["FS_WEIGHTNO"].Value.ToString() + "'";
                    }
                    else
                    {
                        strTmp += ",'" + uGridData.Rows[ugr.Index].Cells["FS_WEIGHTNO"].Value.ToString() + "'";
                    }
                }
                if (strTmp == "FS_WEIGHTNO IN (")
                    strTmp = "";
                else
                    strTmp += ")";

                return strTmp;
            }
            else
            {
                return "";
            }
        }
  
        private void upLoadData()
        {
            if (uGridData.Rows.Count == 0)
            {
                lstHint2.Items.Add("错误：没有需要上传的数据！");
                return;
            }

            string strTmpMater, strTmpOrder, strTmpValue;
            strTmpMater = "";
            string strTmpNo = "";
            decimal dTmpZl = 0;

            string[] strTmpHeader = new string[] { "", "", "" };
            ArrayList listSubItem = new ArrayList();
            strTmpOrder = "";
            dTmpZl = 0;

            for (int i = 0; i < uGridData.Rows.Count; i++)
            {
                if (!chkDataInfo(i)) continue;
                if (uGridData.Rows[i].Cells["FS_UPLOADFLAG"].Value.ToString() == "0")
                {
                    strTmpHeader[0] = Convert.ToDateTime(uGridData.Rows[i].Cells["FS_ACCOUNTDATE"].Value).ToString("yyyy.MM.dd");
                    strTmpHeader[1] = Convert.ToDateTime(uGridData.Rows[i].Cells["FS_ACCOUNTDATE"].Value).ToString("yyyy.MM.dd");
                    strTmpHeader[2] = uGridData.Rows[i].Cells["FS_HEADER"].Value.ToString();

                    strTmpOrder = uGridData.Rows[i].Cells["FS_PRODUCTNO"].Value.ToString();
                    strTmpMater = uGridData.Rows[i].Cells["FS_MATERIAL"].Value.ToString();
                    strTmpNo = uGridData.Rows[i].Cells["FS_STOVENO"].Value.ToString();
                    dTmpZl = Convert.ToDecimal(uGridData.Rows[i].Cells["FN_NETWEIGHT"].Value);

                    listSubItem.Clear();
                    listSubItem.Add(strTmpMater);//物料编号
                    listSubItem.Add(uGridData.Rows[i].Cells["FS_PLANT"].Value.ToString());//工厂
                    listSubItem.Add(uGridData.Rows[i].Cells["FS_SAPSTORE"].Value.ToString());//库存地点
                    listSubItem.Add(strTmpNo);//批次
                    listSubItem.Add("261");//移动类型101
                    listSubItem.Add(dTmpZl.ToString());//发货数量
                    listSubItem.Add("TON");//收货时的计量单位
                    listSubItem.Add(strTmpOrder);//生产订单编号
                    listSubItem.Add("0001");//生产订单行项目号


                    string sTmp = sapClass.BAPI_GOODSMVT_CREATE("03", strTmpHeader, listSubItem);

                    if (sTmp != "")
                    {
                        strTmpValue = "FS_UPLOADFLAG='1',FD_TOCENTERTIME=sysdate";
                        sapClass.strArrayUpload[2] = sTmp + "-" + uGridData.Rows[i].Cells["FS_STOVENO"].Value.ToString();
                        sapClass.strArrayUpload[6] = dTmpZl.ToString();
                        sTmp = "炉号" + uGridData.Rows[i].Cells["FS_STOVENO"].Value.ToString() + "-凭证号" + sTmp
                            + "-上传重量" + dTmpZl.ToString();
                        sapClass.uptDataFlag("DT_SAP261", strTmpValue, "FS_STOVENO='" + strTmpNo
                            + "' AND FS_PRODUCTNO='" + strTmpOrder + "'");
                        sapClass.uptSapLog(strTmpNo, sapClass.strArrayUpload);
                        sTmp += "，发料成功！";
                        lstHint2.Items.Add(sTmp);
                    }
                    else
                    {
                        lstHint2.Items.Add(sapClass.strSapError);
                    }
                }
            }

            showGridInfo();
        }

        private void PySapFl_Load(object sender, EventArgs e)
        {
            objBi.ob = this.ob;
            ugpData.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            lblScr.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

           // txtKcd.Text = "1310";//玉钢线材库

            getStrWhere();

            showGridInfo();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (!chkSelectData()) return;

            string strTmpTable, strTmpWhere;
            lstHint1.Items.Clear();

            strTmpTable = "DT_SAP261";
            strTmpWhere = getSelectNo();
            
            if(sapClass.delData(strTmpTable, strTmpWhere))
            {
                lstHint1.Items.Add("信息删除成功！");
            }
            else
            {
                lstHint1.Items.Add("信息删除失败！");
            }
            showGridInfo();
        }
    }
}