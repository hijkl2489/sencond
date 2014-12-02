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

namespace Core.KgMcms.TrackWeigh
{
    public partial class UpSap : FrmBase
    {
        #region 变量定义

        //使用公用对象
        BaseInfo objBi = new BaseInfo();

        //查询条件语句
        string strWhere = "";

        //物料凭证号
        string strMaterDocu = "";

        //冲销包含的炉号
        string strLh;

        //本地缓存订单号信息
        DataTable dtOrder = new DataTable();

        //基础信息操作
        GetBaseInfo BaseInfo;

        //操作序号
        string strOptNo = "";

        //可以修改的数据信息
        string p_FS_PRODUCTNO = "";
        string p_FS_ITEMNO = "";
        string p_FS_MATERIAL = "";
        string p_FS_MATERIALNAME = "";
        string p_FS_RECEIVEFACTORY = "";
        string p_FS_RECEIVESTORE = "";
        string p_FS_POTNO = "";
        string p_FS_STOVENO = "";
        string p_FS_STOVESEATNO = "";
        string p_FS_ACCOUNTDATE = "";

        #endregion

        public UpSap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 检查数据信息
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        private bool chkDataInfo(int iIndex)
        {
            if (uGridData.Rows[iIndex].Cells["FS_POTNO"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行罐号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_STOVENO"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行炉号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_STOVESEATNO"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行炉座号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_PRODUCTNO"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行生产订单号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_MATERIAL"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行物料信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_RECEIVEFACTORY"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行工厂信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_RECEIVESTORE"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行收货库存地信息不能为空！");
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

        /// <summary>
        /// 检查用户是否选择多个高炉信息
        /// </summary>
        private bool chkStoveNo()
        {
            string strTmp1 = "";
            string strTmp2 = "";

            if (uGridData.Rows.Count == 1) return true;

            foreach (UltraGridRow ugr in uGridData.Rows)
            {
                if (strTmp1 == "") strTmp1 = uGridData.Rows[ugr.Index].Cells["FS_STOVESEATNO"].Value.ToString();
                strTmp2 = uGridData.Rows[ugr.Index].Cells["FS_STOVESEATNO"].Value.ToString();
                if (strTmp1 != strTmp2)
                {
                    strTmp1 = "r";
                    break;                
                }
            }

            if (strTmp1 == "r")
            {
                lstHint2.Items.Add("错误：你的选择包括多个炉座号！");
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// 根据订单号从SAP下载订单信息
        /// </summary>
        private void downOrderInfo(string sDdh)
        {
            lstHint2.Items.Clear();
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
            //else
            //{
            //    DataRow[] drs = dtOrder.Select("FS_MATERIALNAME='" + dtOrder.Rows[0][3].ToString() + "'");
            //    if (drs[0].IsNull(0))
            //    {
            //        CoreClientParam ccpWl = new CoreClientParam();
            //        ccpWl.ServerName = "Core.KgMcms.BaseDataManage.QueryPointInfo";
            //        ccpWl.MethodName = "SaveWLData";
            //        ccpWl.ServerParams = new object[] { "K13", txtWlmc.Text, "SGLR" };

            //        this.ExecuteNonQuery(ccpWl, CoreInvokeType.Internal);
            //        txtWlmc.Text = ccpWl.ReturnObject.ToString();
            //    }
            //}

            //txtGc.Text = "8000";
            //txtHxmh.Text = dtOrder.Rows[0][0].ToString();
            //txtWlbh.Text = "PTS000";
            lstHint2.Items.Add("生产订单信息下载成功！");
        }

        /// <summary>
        /// 编辑数据信息
        /// </summary>
        private void doUpdate()
        {
            if ((ckSC.Checked) || (ckQR.Checked))
            {
                MessageBox.Show("查询模式为【已上传】或【已确认】，不允许进行数据修改操作！");
                return;
            }

            if ((txtLh.Text == "") || (txtScdd.Text == "") || (cmbLzh.Text == "") || (txtKcd.Text == ""))
            {
                MessageBox.Show("炉座号、炉号、生产订单与库存地都不能为空！");
                return;
            }

            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    strOptNo = uGridData.Rows[ugr.Index].Cells["FS_WEIGHTNO"].Value.ToString();

                    p_FS_POTNO = uGridData.Rows[ugr.Index].Cells["FS_POTNO"].Value.ToString();
                    p_FS_STOVENO = (txtLh.Text != "") ? txtLh.Text : uGridData.Rows[ugr.Index].Cells["FS_STOVENO"].Value.ToString();
                    p_FS_PRODUCTNO = (txtScdd.Text != "") ? txtScdd.Text : uGridData.Rows[ugr.Index].Cells["FS_PRODUCTNO"].Value.ToString();
                    p_FS_ITEMNO = (txtHxmh.Text != "") ? txtHxmh.Text : "0001";
                    p_FS_MATERIAL = uGridData.Rows[ugr.Index].Cells["FS_MATERIAL"].Value.ToString();
                    p_FS_MATERIALNAME = uGridData.Rows[ugr.Index].Cells["FS_MATERIALNAME"].Value.ToString();
                    p_FS_RECEIVEFACTORY = (txtGc.Text != "") ? txtGc.Text : uGridData.Rows[ugr.Index].Cells["FS_RECEIVEFACTORY"].Value.ToString();
                    p_FS_RECEIVESTORE = (txtKcd.Text != "") ? txtKcd.Text : uGridData.Rows[ugr.Index].Cells["FS_RECEIVESTORE"].Value.ToString();
                    p_FS_STOVESEATNO = (cmbLzh.Text != "") ? cmbLzh.Text : uGridData.Rows[ugr.Index].Cells["STOVESEATNO"].Value.ToString();
                    p_FS_ACCOUNTDATE = dteJzrq.Value.ToString("yyyy-MM-dd");

                    uptData();
                }
            }
            else
            {
                MessageBox.Show("请先选择要修改的数据行！");
            }
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
        /// 根据订单号查询物料信息
        /// </summary>
        private bool getStrWhere()
        {
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                txtHint.Text = "开始日期不能大于结束日期，请进行检查！";
                qDteBegin.Focus();
                return false;
            }

            if (qDteBegin.Value == qDteEnd.Value)
                strWhere = " AND FS_ACCOUNTDATE='" + qDteBegin.Value.ToString("yyyy-MM-dd") + "'";
            else
                strWhere = " AND (FS_ACCOUNTDATE BETWEEN '" + qDteBegin.Value.ToString("yyyy-MM-dd") + "' AND　'"
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

            if (qCmbLzh.Text != "")
            {
                strWhere += " AND FS_STOVESEATNO = '" + qCmbLzh.Text + "'";
            }

            //strWhere += " AND (FS_AUDITOR != '' AND FS_AUDITOR IS NOT NULL)";

            //if (ckYS.Checked)
            //    strWhere += " AND FS_AUDITOR != '' AND FS_AUDITOR IS NOT NULL";

            if (ckBJWC.Checked)
                strWhere += " AND FS_STOVENO != '' AND FS_STOVESEATNO != ''";

            if (ckQR.Checked)
                strWhere += " AND FS_RECEIVEFLAG = '1'";
            else
                strWhere += " AND FS_RECEIVEFLAG = '0'";

            if (ckSC.Checked)
                strWhere += " AND FS_UPLOADFLAG = '1'";
            else
                strWhere += " AND FS_UPLOADFLAG = '0'";

            if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt02") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt04") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt05") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt06") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt07") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt12"))
            {
                strWhere += " AND FS_RECEIVE IN ('SH000166','SH000161','SH000124','SH000187')";
            }

            if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt03") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt08") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt09") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt10") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt11") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt13") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt17"))
            {
                strWhere += " AND FS_RECEIVE = 'SH000167'";
            }

            strWhere += " AND FS_WEIGHTTYPE = '003'";
            return true;
        }

        /// <summary>
        /// 执行MBST冲销
        /// </summary>
        private bool optMbst()
        {
            bool bTmp;
            string strTmpRfc = "BAPI_GOODSMVT_CANCEL";
            string strTmpPzn, strTmpJzrq, strTmpCzyh;
            strTmpJzrq = uGridData.ActiveRow.Cells["FS_ACCOUNTDATE"].Value.ToString();
            strTmpPzn = strTmpJzrq.Substring(0, 4);
            strTmpCzyh = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.Sap.UploadSapRfc";
            ccp.MethodName = "cancelMoveDocu";

            ccp.ServerParams = new object[] { strTmpRfc, strMaterDocu, strTmpPzn, strTmpJzrq, strTmpCzyh };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            string sTmp;
            if (ccp.ReturnCode == 0)
            {
                sTmp = ccp.ReturnObject.ToString();
                sTmp += "->数据SAP系统冲销成功！";
                bTmp = true;
            }
            else
            {
                sTmp = ccp.ReturnInfo;
                bTmp = false;
            }

            lstHint2.Items.Add(sTmp);
            return bTmp;
        }

        /// <summary>
        /// 根据订单号查询信息
        /// </summary>
        private void queryOrderInfo(string sDdh)
        {
            CoreClientParam ccpOrder = new CoreClientParam();
            ccpOrder.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccpOrder.MethodName = "query_DdInfo";
            ccpOrder.ServerParams = new object[] { "FS_PRODUCTNO = " + sDdh };

            dtOrder.Clear();
            ccpOrder.SourceDataTable = dtOrder;
            this.ExecuteQueryToDataTable(ccpOrder, CoreInvokeType.Internal);
            if (dtOrder.Rows.Count > 0)
            {
                txtScdd.Text = sDdh;
                txtWlmc.Text = dtOrder.Rows[0][1].ToString();
                txtGc.Text = "8000";
                txtHxmh.Text = "0001";
                txtWlbh.Text = "PTS000";
            }
        }

        /// <summary>
        /// 铁水信息
        /// </summary>
        private void showGridInfo()
        {
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccpData.MethodName = "query_TSUpload";
            ccpData.ServerParams = new object[] { strWhere }; //

            //dataSet1.Tables[0].Clear();
            //DataTable dtData = new DataTable();
            //ccpData.SourceDataTable = dtData;
            dataSet1.Tables[0].Clear();
            ccpData.SourceDataTable = dataSet1.Tables[0];
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
                //Constant.RefreshAndAutoSize(uGridData);
            }
            catch(Exception)
            {
                lstHint2.Items.Add("铁水信息查询异常！");
            }
            //uGridData.DataSource = dataSet1;
            //MessageBox.Show(dataSet1.Tables[0].Rows.Count.ToString());
        }

        /// <summary>
        /// 显示铁水汇总信息
        /// </summary>
        private void showListInfo()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccp.MethodName = "query_TSUploadGroup";
            ccp.ServerParams = new object[] { strWhere };

            DataTable dtGroup = new DataTable();
            string strTmp = "";

            dtGroup.Clear();
            ccp.SourceDataTable = dtGroup;

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if(dtGroup.Rows.Count == 0) return;

            txtHint.Clear();

            for (int i = 1; i < dtGroup.Rows.Count; i++ )
            {
                strTmp = dtGroup.Rows[i][2].ToString() + "高炉" + "共有" + dtGroup.Rows[i][3].ToString() + "罐，重量"
                    + dtGroup.Rows[i][1].ToString();
                txtHint.Text += strTmp;
            }
        }

        /// <summary>
        /// 铁水信息冲销
        /// </summary>
        private void unLoadData()
        {
            if (uGridData.Selected.Rows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的数据行！");
                return;
            }

            if (!ckSC.Checked)
            {
                MessageBox.Show("只有已经上传的数据允许执行冲销操作！");
                return;
            }

            if(strMaterDocu == "")
            {
                MessageBox.Show("请先双击要冲销的数据行！");
                return;
            }

            if((uGridData.Selected.Rows[0].Cells["FS_UPLOADFLAG"].Value.ToString() == "0") || (uGridData.Selected.Rows[0].Cells["FS_UPLOADFLAG"].Value.ToString() == ""))
            {
                MessageBox.Show("你可能没有执行查询操作！");
                return;
            }

            string strTmpNo = "";

            if (DialogResult.Yes == MessageBox.Show("该操作将导致此次上传被冲销，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                if (!optMbst())
                {
                    return;
                }

                strTmpNo = "FS_STOVENO IN (" + strLh + ")";

                string strTmpTable, strTmpField;

                strTmpTable = "DT_IRONWEIGHT";
                strTmpField = "FS_UPLOADFLAG='0',FS_RECEIVEFLAG='0'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
                ccp.MethodName = "check_UpdateInfo";
                ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpNo };
                try
                {
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
                catch (Exception)
                {
                    return;
                }

                strTmpTable = "IT_SAPLOG";
                ccp.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
                ccp.MethodName = "DeleteData";
                ccp.ServerParams = new object[] { strTmpTable, "FS_DESCRIBE LIKE '" + strMaterDocu + "%' AND FS_MATERIALNO='PTS000'" };
                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                
                showGridInfo();
            }
        }

        /// <summary>
        /// 铁水信息编辑
        /// </summary>
        private void uptData()
        {
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccp.MethodName = "Update_UploadTS";
            ccp.ServerParams = new object[] { 
                strOptNo,
                p_FS_PRODUCTNO,
                p_FS_ITEMNO,
                p_FS_MATERIAL,
                p_FS_MATERIALNAME,
                p_FS_RECEIVEFACTORY,
                p_FS_RECEIVESTORE,
                p_FS_POTNO,
                p_FS_STOVENO,
                p_FS_STOVESEATNO,
                p_FS_ACCOUNTDATE
            };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode != 0)
            {
                MessageBox.Show("罐号为" + p_FS_POTNO + "的数据保存失败！");
            }
            else
            {
                lstHint2.Items.Add("罐号为" + p_FS_POTNO + "的数据修改成功！");
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
            ugpData.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            strWhere = " AND FS_ACCOUNTDATE='" + qDteBegin.Value.ToString("yyyy-MM-dd") + "'";
            strWhere += " AND FS_RECEIVEFLAG = '0'";
            strWhere += " AND FS_UPLOADFLAG = '0'";
            //strWhere += " AND (FS_AUDITOR = '' OR FS_AUDITOR IS NULL)";

            if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt02") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt04") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt05") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt06") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt07") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt12"))
            {
                strWhere += " AND FS_GROSSPOINT = 'K14'";
            }

            if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt03") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt08") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt09") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt10") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt11") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt13") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() == "lt17"))
            {
                strWhere += " AND FS_GROSSPOINT = 'K13'";
            }
            showGridInfo();
        }

        private void qTxtScdd_Leave(object sender, EventArgs e)
        {
            if (qTxtScdd.Text.Trim().Length == 0)
                return;

            if (qTxtScdd.Text.Trim().Length != 12)
            {
                MessageBox.Show("生产订单号信息必须等于12位，请重新进行输入！");
                qTxtScdd.Focus();
                return;
            }

            if (qTxtScdd.Text.Trim().Substring(0, 3) != "000")
            {
                MessageBox.Show("生产订单号信息开头3位必须为0，请重新进行输入！");
                qTxtScdd.Focus();
                return;
            }
        }

        private void txtScdd_Leave(object sender, EventArgs e)
        {
            if (txtScdd.Text.Trim().Length == 0)
                return;

            if (txtScdd.Text.Trim().Length != 12)
            {
                MessageBox.Show("生产订单号信息必须等于12位，请重新进行输入！");
                txtScdd.Focus();
                return;
            }

            if (txtScdd.Text.Trim().Substring(0, 3) != "000")
            {
                MessageBox.Show("生产订单号信息开头3位必须为0，请重新进行输入！");
                txtScdd.Focus();
                return;
            }

            getOrderInfo(txtScdd.Text);
        }

        private void txtGh_Leave(object sender, EventArgs e)
        {
            if (txtGh.Text.Trim() == "") return;
            if (txtGh.Text.Trim().Length > 20)
            {
                MessageBox.Show("罐号不能大于20位，请重新进行输入！");
                txtGh.Focus();
            }
        }

        private void txtLh_Leave(object sender, EventArgs e)
        {
            if (txtLh.Text.Trim() == "") return;
            if (txtLh.Text.Trim().Length > 20)
            {
                MessageBox.Show("炉号不能大于20位，请重新进行输入！");
                txtLh.Focus();
            }
        }

        private void txtGc_Leave(object sender, EventArgs e)
        {
            if (txtGc.Text.Trim() == "") return;
            if (txtGc.Text.Trim().Length != 4)
            {
                MessageBox.Show("工厂数据等于4位（8000），请重新进行输入！");
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
            switch (e.Tool.Key.ToString())
            {
                case "find":
                {
                    if (!getStrWhere())
                        break;
                    showListInfo();
                    showGridInfo();
                    break;
                }
                case "Edit":
                {
                    //if (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() != "lt01") return;
                    doUpdate();
                    showListInfo();
                    showGridInfo();
                    break;
                }
                case "UpSap":
                {
                    //if (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() != "lt01") return;
                    upLoadData();
                    break;
                }
                case "Reload":
                {
                    //if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() != "lt12") 
                    //    && (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() != "lt17")) return;
                    unLoadData();
                    break;
                }
                case "ToExcel":
                {
                    Constant.ExportGrid2Excel(this, this.ultraGridExcelExporter1, uGridData);
                    Constant.WaitingForm.Close();
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
            catch(Exception)
            {
            }

            strOptNo = uGridData.ActiveRow.Cells["FS_WEIGHTNO"].Text;
            if ((uGridData.ActiveRow.Cells["FS_RECEIVEFLAG"].Text == "1") && (uGridData.ActiveRow.Cells["FS_UPLOADFLAG"].Text == "1"))
            {
                showSapLog(uGridData.ActiveRow.Cells["FS_STOVENO"].Text);
                return;
            }

            
            txtGh.Text = uGridData.ActiveRow.Cells["FS_POTNO"].Text;
            cmbLzh.Text = uGridData.ActiveRow.Cells["FS_STOVESEATNO"].Text;
            txtLh.Text = uGridData.ActiveRow.Cells["FS_STOVENO"].Text;
            txtScdd.Text = uGridData.ActiveRow.Cells["FS_PRODUCTNO"].Text;
            txtWlbh.Text = uGridData.ActiveRow.Cells["FS_MATERIAL"].Text;
            txtWlmc.Text = uGridData.ActiveRow.Cells["FS_MATERIALNAME"].Text;
            txtGc.Text = "8000";
            txtKcd.Text = uGridData.ActiveRow.Cells["FS_RECEIVESTORE"].Text;
            txtZl.Text = uGridData.ActiveRow.Cells["FN_NETWEIGHT"].Text;
        }

        private string getSelectNo()
        {
            string strTmp = "FS_WEIGHTNO IN (";
            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    if (!chkDataInfo(ugr.Index)) continue;
                    if (uGridData.Rows[ugr.Index].Cells["FS_UPLOADFLAG"].Value.ToString() == "1") continue;
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

        private void showSapLog(string sLh)
        {
            txtHint.Clear();
            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpOrderInfo = new CoreClientParam();
            ccpOrderInfo.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccpOrderInfo.MethodName = "queryData";

            strTmpTable = "IT_SAPLOG";
            strTmpField = "FS_DESCRIBE";
            strTmpWhere = " AND FS_DESCRIBE LIKE '%" + sLh + ",%' AND FS_MATERIALNO='PTS000'";
            DataTable dtTmpData = new DataTable();

            ccpOrderInfo.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" };
            ccpOrderInfo.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpOrderInfo, CoreInvokeType.Internal);
            string strTmp = "";
            strTmp += dtTmpData.Rows[0][0].ToString();
            txtHint.Text = "上传日志：" + strTmp;
            
            string[] strTmpLog = strTmp.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
            strMaterDocu = strTmpLog[0];
            string[] strTmpArrayLh = strTmpLog[1].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            strLh = "";
            string strTmp1 = "";

            for (int i = 0; i < strTmpArrayLh.Length; i++)
            {
                if (strTmp1 != strTmpArrayLh[i])
                {
                    if (strLh == "")
                        strLh = "'" + strTmpArrayLh[i] + "'";
                    else
                        strLh += ",'" + strTmpArrayLh[i] + "'";
                }
                strTmp1 = strTmpArrayLh[i];
            }
        }

        private void UpSap_KeyDown(object sender, KeyEventArgs e)
        {            
            if ((e.KeyCode != Keys.F2) && (e.KeyCode != Keys.F3)) return;
            if (!chkSelectData()) return;

            lstHint2.Items.Clear();
            string strTmpTable, strTmpField, strTmpWhere;
            strTmpTable = "DT_IRONWEIGHT";
            strTmpField = "";

            if (e.KeyCode == Keys.F2)
            {
                if ((ckSC.Checked) || (ckQR.Checked))
                {
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("您将确认选中的数据信息，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    strTmpField = "FS_RECEIVEFLAG='1'";
                }
                else
                    return;
            }

            if (e.KeyCode == Keys.F3)
            {
                if (ckSC.Checked)
                {
                    MessageBox.Show("查询模式为【已上传】，不允许进行该操作！");
                    return;
                }

                strTmpField = "FS_RECEIVEFLAG='0'";
            }

            strTmpWhere = getSelectNo();
            if (strTmpWhere == "") return;

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccp.MethodName = "check_UpdateInfo";

            ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            if (ccp.ReturnCode == 0)
            {
                showGridInfo();
            }
        }

        private void upLoadData()
        {
            if (uGridData.Rows.Count == 0)
            {
                lstHint2.Items.Add("错误：没有需要上传的数据！");
                return;
            }

            if (ckSC.Checked)
            {
                MessageBox.Show("查询模式为【已上传】，不允许进行上传操作！");
                return;
            }

            if (!ckQR.Checked)
            {
                MessageBox.Show("只有查询模式为【已确认】，才允许进行上传操作！");
                return;
            }

            if (uGridData.Rows[0].Cells["FS_RECEIVEFLAG"].Value.ToString() == "0")
            {
                MessageBox.Show("你可能没有执行查询操作！");
                return;
            }

            if(!chkStoveNo()) return;

            string strTmpLh = "";

            byte iFlag = 0;
            string strTmpRfc, strTmpCode, strTmpOrder;
            string strTmpNo = "";
            decimal dTmpZl = 0;
            
            string[] strTmpHeader = new string[]{ "","",""};
            string[] strTmpUpload = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
            ArrayList listItem = new ArrayList();
            ArrayList listSubItem = new ArrayList();
            CoreClientParam ccp = new CoreClientParam();

            strTmpRfc = "BAPI_GOODSMVT_CREATE";
            strTmpCode = "02";
            strTmpOrder = "";

            for (int i = 0; i < uGridData.Rows.Count; i++ )
            {
                if ((uGridData.Rows[i].Cells["FS_RECEIVEFLAG"].Value.ToString() == "1") && (uGridData.Rows[i].Cells["FS_UPLOADFLAG"].Value.ToString() == "0"))
                {
                    if (uGridData.Rows[i].Cells["FS_AUDITOR"].Value.ToString() == "")
                    {
                        lstHint2.Items.Add("第" + i.ToString() + "行数据炼钢未审核！操作终止。");
                        return;
                    }

                    if (iFlag == 0)
                    {
                        strTmpHeader[0] = Convert.ToDateTime(uGridData.Rows[i].Cells["FS_ACCOUNTDATE"].Value).ToString("yyyy.MM.dd");
                        strTmpHeader[1] = Convert.ToDateTime(uGridData.Rows[i].Cells["FS_ACCOUNTDATE"].Value).ToString("yyyy.MM.dd");
                        strTmpHeader[2] = "";

                        listSubItem.Add("PTS000");//物料编号
                        //listSubItem.Add("BHRB400180011");
                        listSubItem.Add(uGridData.Rows[i].Cells["FS_RECEIVEFACTORY"].Value.ToString());//工厂
                        listSubItem.Add(uGridData.Rows[i].Cells["FS_RECEIVESTORE"].Value.ToString());//库存地点
                        //listSubItem.Add("1004000101");//批次
                        listSubItem.Add("");//批次
                        listSubItem.Add("101");//移动类型101
                        listSubItem.Add("");//库存类型
                        listSubItem.Add("");//特殊库存标识

                        strTmpUpload[0] = "PTS000";
                        strTmpUpload[1] = "101";
                        strTmpUpload[3] = uGridData.Rows[i].Cells["FS_PRODUCTNO"].Value.ToString();
                        strTmpUpload[4] = uGridData.Rows[i].Cells["FS_STOVESEATNO"].Value.ToString();
                        strTmpUpload[5] = Convert.ToDateTime(objBi.GetServerTime()).ToString("yyyy-MM-dd HH:mm:ss");
                        strTmpUpload[7] = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                        strTmpUpload[8] = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID();
                        strTmpUpload[9] = uGridData.Rows[i].Cells["FS_ITEMNO"].Value.ToString();
                        strTmpUpload[10] = strTmpUpload[5];

                        strTmpOrder = uGridData.Rows[i].Cells["FS_PRODUCTNO"].Value.ToString();
                        iFlag = 1;
                    }

                    dTmpZl += Convert.ToDecimal(uGridData.Rows[i].Cells["FN_NETWEIGHT"].Value);
                    if (strTmpNo == "")
                        strTmpNo = "'" + uGridData.Rows[i].Cells["FS_WEIGHTNO"].Value.ToString() + "'";
                    else
                        strTmpNo += ",'" + uGridData.Rows[i].Cells["FS_WEIGHTNO"].Value.ToString() + "'";

                    if (strTmpLh == "")
                        strTmpLh = uGridData.Rows[i].Cells["FS_STOVENO"].Value.ToString();
                    else
                        strTmpLh += "," + uGridData.Rows[i].Cells["FS_STOVENO"].Value.ToString();
                }
            }

            listSubItem.Add(dTmpZl.ToString());//发货数量
            listSubItem.Add("TON");//收货时的计量单位
            listSubItem.Add("0001");//项目文本
            listSubItem.Add(strTmpOrder);//生产订单编号
            listSubItem.Add("F");//移动标识'F'
            listSubItem.Add("");//销售订单号
            listSubItem.Add("");//销售订单行项目
            listItem.Add(listSubItem);

            ccp.ServerName = "Core.KgMcms.Sap.UploadSapRfc";
            ccp.MethodName = "up_Product";

            ccp.ServerParams = new object[] { strTmpRfc, strTmpHeader, strTmpCode, listItem };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            string sTmp;
            if (ccp.ReturnCode == 0)
            {
                sTmp = ccp.ReturnObject.ToString();
                strTmpUpload[2] = sTmp + "-" + strTmpLh;
                strTmpUpload[6] = dTmpZl.ToString();
                
                lstHint2.Items.Add(sTmp);
                uptSapLog(strTmpNo, strTmpUpload);
                showGridInfo();
                sTmp += "，数据上传成功！";
            }
            else
            {
                sTmp = ccp.ReturnInfo;
            }

            lstHint2.Items.Add(sTmp);
        }

        private void uptSapLog(string sTmpNo, string[] sArray)
        {
            string strTmpTable, strTmpField, strTmpWhere, strTmpValue;
            string strTmpDate, strTmpTime;

            strTmpDate = DateTime.Now.ToString("yyyy-MM-dd");
            strTmpTime = DateTime.Now.ToString("HH:mm:ss");
            strTmpTable = "DT_IRONWEIGHT";
            strTmpField = "FS_UPLOADFLAG='1',FD_TOCENTERTIME='" + strTmpDate + "',FD_TESTIFYDATE='";
            strTmpField += strTmpTime + "'";
            strTmpWhere = "FS_WEIGHTNO IN (" + sTmpNo + ")";

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccp.MethodName = "check_UpdateInfo";

            ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

            strTmpTable = "IT_SAPLOG";
            strTmpField = "FS_MATERIALNO,FS_MOVETYPE,FS_DESCRIBE,FS_ORDERNO,FS_BATCHNO,FD_UPLOADTIME,"
                + "FN_UPLOADNUM,FS_USER,FS_USERNO,FS_ITEMNO,FD_STARTTIME,FD_ENDTIME";

            strTmpValue = "";
            for(int i = 0; i < sArray.Length - 1; i++)
            {
                if(i == 0)
                    strTmpValue = "'" + sArray[i] + "'";
                else
                    strTmpValue += ",'" + sArray[i] + "'";
            }

            strTmpValue += ",'" + Convert.ToDateTime(objBi.GetServerTime()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            CoreClientParam ccpLog = new CoreClientParam();
            ccpLog.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            ccpLog.MethodName = "insertDataInfo";

            ccpLog.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };

            this.ExecuteNonQuery(ccpLog, CoreInvokeType.Internal);
        }
    }
}
