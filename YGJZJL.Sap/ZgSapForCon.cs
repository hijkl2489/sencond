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
using YGJZJL.DepotMngr;

namespace YGJZJL.Sap
{
    public partial class ZgSapForCon : BaseInfo
    {
        #region 变量定义

        //EXCEL应用
        Excel.Application app;

        //使用公用对象
        BaseInfo objBi = new BaseInfo();

        //查询条件语句
        string strWhere = "";

        //物料凭证号
        string strMaterDocu = "";

        //冲销包含的炉号
        string strLh;

        //本地缓存订单号信息
        System.Data.DataTable dtOrder = new System.Data.DataTable();

        //操作序号
        string strOptNo = "";

        string strDepartMent = "";

        _Workbook workbook;

        //可以修改的数据信息
        string p_FS_PRODUCTNO = "";
        string p_FS_ITEMNO = "";
        string p_FS_MATERIAL = "";
        string p_FS_MATERIALNAME = "";
        string p_FS_RECEIVEFACTORY = "";
        string p_FS_RECEIVESTORE = "";
        string p_FS_STOVENO = "";
        string p_FS_ACCOUNTDATE = "";
        string p_FS_PRIBATCHNO = "";
        string strKey = "";

        //引入公用类
        SapClass sapClass = null;
        //  SapClass sapClass = new SapClass();

        #endregion

        public ZgSapForCon()
        {
            InitializeComponent();
           // objBi.ob = (OpeBase)obb;
        }
     /*   public BcSap(object obb)
        {
            objBi.ob = (OpeBase)obb;
            this.ob = (OpeBase)obb;
        }*/
        /// <summary>
        /// 检查用户信息是否正确
        /// </summary>
        private bool chkDataInfo(int iIndex)
        {
            if (uGridData.Rows[iIndex].Cells["FS_BATCHNO"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行轧制号信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_PRODUCTNO"].Value.ToString() == "")
            {
                lstHint2.Items.Add("第" + iIndex.ToString() + "行生产订单号信息不能为空！");
                return false;
            }

            //if (uGridData.Rows[iIndex].Cells["FS_MATERIAL"].Value.ToString() == "")
            //{
            //    lstHint2.Items.Add("第" + iIndex.ToString() + "行物料信息不能为空！");
            //    return false;
            //}


            if (uGridData.Rows[iIndex].Cells["FS_SAPSTORE"].Value.ToString() == "")
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
            string strTmp = "";
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

            foreach (UltraGridRow ugr in uGridData.Selected.Rows)
            {
                if (chkDataInfo(ugr.Index)) strTmp += "1";
            }

            if (strTmp == "") return false;

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
            try
            { this.ExecuteQueryToDataTable(ccpProductNo, CoreInvokeType.Internal); }
            catch (Exception e)
            { }
         

            if (dtOrder.Rows.Count == 0)
            {
                lstHint2.Items.Add("生产订单信息下载失败！");
                return;
            }
            lstHint2.Items.Add("生产订单信息下载成功！");
        }

        /// <summary>
        /// 根据凭证号从SAP下载检验批信息
        /// </summary>
        private void downSprueflosInfo(string sPzh, string sBatch)
        {
            string strTmpTable, strTmpField, strTmpWhere;

            CoreClientParam ccpSp = new CoreClientParam();
            ccpSp.ServerName = "ygjzjl.sap.DownloadSapRfc";
            ccpSp.MethodName = "down_SPRUEFLOS";
            ccpSp.ServerParams = new object[] { "ZJL_DOWNPRUEFLOS", sPzh };
            dtOrder.Clear();
            ccpSp.SourceDataTable = dtOrder;
            this.ExecuteQueryToDataTable(ccpSp, CoreInvokeType.Internal);

            if (ccpSp.ReturnCode == 0)
            {
                strTmpTable = "DT_PIPEWEIGHTMAIN";
                strTmpField = "FS_SPRUEFLOS='"+ccpSp.ReturnObject.ToString()+"'";
                strTmpWhere = "FS_BATCHNO='"+sBatch+"'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.base.QueryData";
                ccp.MethodName = "check_UpdateInfo";

                ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                if (ccp.ReturnCode != 0)
                {
                    lstHint2.Items.Add("检验批下载失败！");
                }
                else
                {
                    lstHint2.Items.Add("检验批下载成功！");
                }
            }
        }

        /// <summary>
        /// 编辑数据信息
        /// </summary>
        private void doUpdate(int iFlag)
        {
            if (iFlag == 0)
            {
                if ((txtScdd.Text == "") || (txtGc.Text == "") || (txtKcd.Text == ""))
                {
                    MessageBox.Show("工厂、生产订单与库存地都不能为空！");
                    return;
                }
                //if (this.txtScdd.Text.Length != 12)
                //{
                //    MessageBox.Show("请输入12位订单!");
                //    this.txtScdd.Focus();
                //    return;
                //}
            }

            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    strOptNo = uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString();
                    if (uGridData.Selected.Rows.Count == 1)
                    {
                        p_FS_STOVENO = (txtLh.Text != "") ? txtLh.Text : uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString();
                    }
                    else
                    {
                        p_FS_STOVENO = uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString();
                    }

                    p_FS_PRODUCTNO = (txtScdd.Text != "") ? txtScdd.Text : uGridData.Rows[ugr.Index].Cells["FS_PRODUCTNO"].Value.ToString();
                    p_FS_ITEMNO = txtHxmh.Text ;
                    p_FS_MATERIAL =txtWlbh.Text.Trim();
                    p_FS_MATERIALNAME = txtWlmc.Text;
                    p_FS_RECEIVEFACTORY = txtGc.Text;
                    p_FS_RECEIVESTORE = (txtKcd.Text != "") ? txtKcd.Text : "";
                    p_FS_ACCOUNTDATE = dteJzrq.Value.ToString("yyyy-MM-dd");
                    //p_FS_PRIBATCHNO = tb_OldBatchNo.Text;

                    string strTmpTable, strTmpField, strTmpWhere;

                    String shr = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                    strTmpTable = "DT_PIPEWEIGHTMAIN";
                    strTmpField = "";
                    if (iFlag == 0)
                    {
                        strTmpField = "FS_PRODUCTNO='" + txtScdd.Text + "',FS_ITEMNO='"+p_FS_ITEMNO+"',FS_SAPSTORE='" + txtKcd.Text + "',FS_PLANT='" + txtGc.Text + "',"
                            + "FS_ACCOUNTDATE='" + dteJzrq.Value.ToString("yyyy-MM-dd") + "',FS_HEADER='" + cmbHead.Text + "'";
                        strTmpField += ",FS_ISMATCH='1',FS_MATERIALNO='" + p_FS_MATERIAL + "',FS_MATERIALNAME='"+p_FS_MATERIALNAME+"',FS_PRIBATCHNO='"+p_FS_PRIBATCHNO+"' ";

                        if (ckTheory.Checked)
                            strTmpField += ",FS_LLJZ='1'";
                        else
                            strTmpField += ",FS_LLJZ='0'";
                        //by lgx 2012.4.4
                        //  strTmpField += ",FS_AUDITOR='" + shr + "'";
                    }
                    if (iFlag == 1)
                    {
                        strTmpField = "FS_ISMATCH='0'";
                    }

                    strTmpWhere = "FS_BATCHNO ='" + strOptNo + "'";

                    sapClass.uptData(strTmpTable, strTmpField, strTmpWhere);

                   /* CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.base.QueryData";
                    ccp.MethodName = "check_UpdateInfo";

                    ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);*/


                    strTmpField = "";
                    strTmpWhere = "";
                    strTmpTable = "";
                    strTmpTable = "DT_PIPEWEIGHTDETAIL";
                   
                    if (iFlag == 0)
                    {
                        strTmpField += "FS_ISMATCH='1'";
                    }
                    if (iFlag == 1)
                    {
                        strTmpField = "FS_ISMATCH='0'";
                    }
                    strTmpWhere = "FS_BATCHNO ='" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'"
                               + " AND FN_BANDNO=" + uGridData.Rows[ugr.Index].Cells["FN_BANDNO"].Value.ToString();
                    //strTmpWhere = "FS_BATCHNO ='" + strOptNo + "'";

                  /*  CoreClientParam ccp2 = new CoreClientParam();
                    ccp2.ServerName = "ygjzjl.base.QueryData";
                    ccp2.MethodName = "check_UpdateInfo";

                    ccp2.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

                    this.ExecuteNonQuery(ccp2, CoreInvokeType.Internal);*/

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
            if (qDteBegin.Value > qDteEnd.Value)
            {
                lstHint1.Items.Add("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }


            strWhere = " AND (FD_DATETIME BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
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

            if (qTxtZzh.Text.Trim() != "")
            {
                strWhere += " AND FS_BATCHNO = '" + qTxtZzh.Text + "'";
            }

            strWhere += " AND FS_UPLOADFLAG = '0' ";
            switch (strKey)
            {
                case "ZF":
                    strWhere += " AND  (FS_BATCHNO like '%TZ%' OR FS_BATCHNO LIKE '%TX%' ) ";
                    break;
                case "LH":
                    strWhere += "  AND  (FS_BATCHNO like '%TL%' OR FS_BATCHNO LIKE '%TG%' ) ";
                    break;
                default:

                    break;

            }

            //strWhere+="AND FS_POINT IN ('K26','K27')";
            //strWhere += " AND FS_COMPLETEFLAG = '1'";

            //strWhere += " AND FS_SAPSTORE = '1311'";//玉钢棒材库存地

       /*     if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1002") || 
                (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1001"))
            {
                strWhere += " AND FS_BATCHNO LIKE ('2%')";
                ckTheory.Enabled = false;
                txtKcd.Text = "1450";
            }

            if (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC2001")
            {
                strWhere += " AND FS_BATCHNO LIKE ('1%')";
                ckTheory.Enabled = true;
            }*/
            return true;
        }

        /// <summary>
        /// 执行EXCEL导入
        /// </summary>
        private void openExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"E:\计量系统\安装3.0\";  //指定打开文件默认路径
            openFileDialog.Filter = "Excel文件|*.xls";     //指定打开默认选择文件类型名
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                app = new Excel.Application();
                app.Visible = false;
                app.UserControl = true;
                Workbooks workbooks = app.Workbooks;
                object MissingValue = Type.Missing;
                workbook = workbooks.Add(openFileDialog.FileName);
            }
            if (openFileDialog.FileName == "")
            {
                MessageBox.Show("请选择导入的Excel文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReadAndSaveExcelInfo();
        }

        /// <summary>
        /// 执行MBST冲销
        /// </summary>
        private void optMbst()
        {
            string strTmpRfc = "BAPI_GOODSMVT_CANCEL";
            string strTmpPzn, strTmpJzrq, strTmpCzyh;
            strTmpJzrq = uGridData.ActiveRow.Cells["FS_ACCOUNTDATE"].Value.ToString();
            strTmpPzn = strTmpJzrq.Substring(0, 4);
            strTmpCzyh = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.sap.UploadSapRfc";
            ccp.MethodName = "cancelMoveDocu";

            ccp.ServerParams = new object[] { strTmpRfc, strMaterDocu, strTmpPzn, strTmpJzrq, strTmpCzyh };
            this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            string sTmp;
            if (ccp.ReturnCode == 0)
            {
                sTmp = ccp.ReturnObject.ToString();
                sTmp += "->数据SAP系统冲销成功！";
            }
            else
            {
                sTmp = ccp.ReturnInfo;
            }

            lstHint2.Items.Add(sTmp);
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

        /// <summary>
        /// 读取并保存Excel中的信息到数据库中
        /// </summary>
        private void ReadAndSaveExcelInfo()
        {
            string strTmpTable, strTmpField, strTmpValue;

            string val1 = "";
            string val2 = "";
            string val3 = "";
            string val4 = "";
            string val5 = "";
            string val6 = "";
            string val7 = "";
            string val8 = "";
            string val9 = "";

            string val10 = "";
            string val11 = "";
            string val12 = "";
            string val13 = "";
            string val14 = "";
            string val15 = "";
            string val16 = "";

            int num = 0;

            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
            Excel.Range range = worksheet.get_Range("A2", "P65535");//A2、O65535表示到Excel从第A列的第2行到第O列的第65535-1行  A2、P65535表示到Excel从第A列的第2行到第P列的第65535-1行 
            System.Array values = (System.Array)range.Formula;
            num = values.GetLength(0);

            //(OWC11.XlBorderWeight.xlThin);   //边框细线 
            //((Excel.Range)worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[3, 3])).Borders.Weight = 25; //单元格高度
            //((Excel.Range)worksheet.Cells[2, 2]).ColumnWidth = 20;  //单元格列宽度 
            //((Range)worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[3, 3])).MergeCells(true);  //合并单元格
            //((Range)worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[3, 3])).Font.Bold(true);   //字体粗体

            //if (values.GetValue(1, 1).ToString().Trim() != "车证号" && values.GetValue(1, 2).ToString().Trim() != "车号" && values.GetValue(1, 3).ToString().Trim() != "合同号")
            //{
            //    MessageBox.Show("预报选择错误，请重新选择Excel文件: '汽车衡预报模板'！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            for (int i = 2; i <= num; i++)
            {
                //if (values.GetValue(i, 1).ToString().Trim() == "" && values.GetValue(i, 2).ToString().Trim() == "" && values.GetValue(i, 5).ToString().Trim() == "")
                //{
                //    num = i;
                //}
                if (values.GetValue(i, 1).ToString().Trim() == "" || values.GetValue(i, 2).ToString().Trim() == "")
                {
                    break;
                }
                else
                {
                    val1 = values.GetValue(i, 1).ToString().Trim();  //记帐日期
                    val2 = values.GetValue(i, 2).ToString().Trim();  //订单号
                    val3 = values.GetValue(i, 3).ToString().Trim();  //订单行项目号
                    val4 = values.GetValue(i, 4).ToString().Trim();  //物料编码
                    val5 = values.GetValue(i, 5).ToString().Trim();  //物料名称
                    val6 = values.GetValue(i, 6).ToString().Trim();  //批号
                    val7 = values.GetValue(i, 7).ToString().Trim();  //收货数量
                    val8 = values.GetValue(i, 8).ToString().Trim();  //计量单位
                    val9 = values.GetValue(i, 9).ToString().Trim(); //工厂

                    val10 = values.GetValue(i, 10).ToString().Trim(); //库存地点
                    val11 = values.GetValue(i, 11).ToString().Trim(); //移动类型
                    val12 = values.GetValue(i, 12).ToString().Trim(); //上工序批次
                    val13 = values.GetValue(i, 13).ToString().Trim(); //特殊库存标志
                    val14 = values.GetValue(i, 14).ToString().Trim(); //销售订单号
                    val15 = values.GetValue(i, 15).ToString().Trim(); //销售订单行项目号
                    val16 = values.GetValue(i, 16).ToString().Trim(); //抬头文本

                    string strLRR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    string strLRD = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();

                    strTmpTable = "DT_PIPEWEIGHTMAIN";
                    strTmpField = "FS_ACCOUNTDATE,FS_PRODUCTNO,FS_ITEMNO,FS_STOVENO,FN_TOTALWEIGHT,FS_PLANT,"
                        + "FS_SAPSTORE,FS_WEIGHTTYPE,FS_HEADER";
                    strTmpValue = "'" + val1 + "','" + val2 + "'" + val3 + "','" + val6 + "'" + val7 + "','" + val8
                        + "'" + val10 + "','" + "'" + val11 + "','" + "'" + val16 + "'";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.base.QueryData";
                    ccp.MethodName = "insertDataInfo";
                    ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
            }
        }

        /// <summary>
        /// 显示信息
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

            strTmpTable = "view_zg_data1";
            strTmpField = "TO_CHAR(FD_DATETIME,'yyyy-MM-dd HH24:mi:ss') AS GBSJ,FS_BATCHNO,FN_BANDNO,FS_PRODUCTNO,FS_MATERIALNAME,FS_MATERIALNO,"
               + "FS_PLANT,FS_SAPSTORE,FS_UPLOADFLAG,FN_WEIGHT,FN_THEORYWEIGHT,FS_ACCOUNTDATE,FS_STEELTYPE,FS_SPEC,FS_ISMATCH,"
               + "FS_HEADER,FS_ITEMNO ";
            strTmpOrder = " ORDER BY FS_BATCHNO,FN_BANDNO";

           /* strTmpField = "TO_CHAR(FD_ENDTIME,'yyyy-MM-dd HH24:mi:ss') AS GBSJ,FS_BATCHNO,FS_PRODUCTNO,"
                + "FS_ITEMNO,FS_SAPSTORE,FS_AUDITOR,FD_AUDITTIME,FS_ISMATCH,FS_UPLOADFLAG,FN_TOTALWEIGHT,"
                + "FS_ACCOUNTDATE,FS_STEELTYPE,FN_BANDCOUNT,FS_SPEC,FN_THEORYTOTALWEIGHT,FS_HEADER,FS_GP_STOVENO";
            strTmpOrder = " ORDER BY FD_ENDTIME";*/

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strWhere, strTmpOrder }; //

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
            catch (Exception ex1)
            {
                lstHint2.Items.Add(ex1.Message + "信息查询异常！");
            }
            //uGridData.DataSource = dataSet1;
            //MessageBox.Show(dataSet1.Tables[0].Rows.Count.ToString());
        }

        /// <summary>
        /// 信息冲销
        /// </summary>
        private void unLoadData()
        {
            if (uGridData.Selected.Rows.Count == 0)
            {
                MessageBox.Show("请先选择要修改的数据行！");
                return;
            }

            if (strMaterDocu == "")
            {
                MessageBox.Show("请先双击要修改的数据行！");
                return;
            }

            if ((uGridData.Selected.Rows[0].Cells["FS_UPLOADFLAG"].Value.ToString() == "0") || (uGridData.Selected.Rows[0].Cells["FS_UPLOADFLAG"].Value.ToString() == ""))
            {
                MessageBox.Show("你可能没有执行查询操作！");
                return;
            }

            string strTmpNo = "";

            if (DialogResult.Yes == MessageBox.Show("该操作将导致此次上传被冲销，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                optMbst();
                strTmpNo = "FS_BATCHNO='" + strOptNo + "'";

                string strTmpTable, strTmpField;

                strTmpTable = "dt_pipeweightmain";
                strTmpField = "FS_UPLOADFLAG='0',FS_ISMATCH='0'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.base.QueryData";
                ccp.MethodName = "check_UpdateInfo";

                ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpNo };

                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                showGridInfo();
            }
        }

        /// <summary>
        /// 信息编辑
        /// </summary>
        /*    private void uptData(int iFlag)
            {
                string strTmpTable, strTmpField, strTmpWhere;

              String shr=  CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

                strTmpTable = "dt_pipeweightmain";
                strTmpField = "";
                if (iFlag == 0)
                {
                    strTmpField = "FS_PRODUCTNO='" + txtScdd.Text + "',FS_ITEMNO='0001',FS_SAPSTORE='" + txtKcd.Text + "',FS_PLANT='" + txtGc.Text + "',"
                        + "FS_ACCOUNTDATE='" + dteJzrq.Value.ToString("yyyy-MM-dd") + "',FS_HEADER='" + cmbHead.Text + "'";
                    strTmpField += ",FS_ISMATCH='1',FS_MATERIALNO='" + p_FS_MATERIAL + "'";

                    if (ckTheory.Checked)
                        strTmpField += ",FS_LLJZ='1'";
                    else
                        strTmpField += ",FS_LLJZ='0'";
                    //by lgx 2012.4.4
                  //  strTmpField += ",FS_AUDITOR='" + shr + "'";
                }
                if (iFlag == 1)
                {
                    strTmpField = "FS_ISMATCH='0'";
                }

                strTmpWhere = "FS_BATCHNO ='" + strOptNo + "'";

                CoreClientParam ccp = new CoreClientParam();
                ccp.ServerName = "ygjzjl.base.QueryData";
                ccp.MethodName = "check_UpdateInfo";

                ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

                this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);



                strTmpTable = "dt_pipeweightdetail";
                strTmpField = "";
                if (iFlag == 0)
                {    
                    strTmpField += "FS_ISMATCH='1'";
                }
                if (iFlag == 1)
                {
                    strTmpField = "FS_ISMATCH='0'";
                }
                strTmpWhere = "FS_BATCHNO ='" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'"
                           + " AND FN_BANDNO=" + uGridData.Rows[ugr.Index].Cells["FN_BANDNO"].Value.ToString();
                //strTmpWhere = "FS_BATCHNO ='" + strOptNo + "'";

                CoreClientParam ccp2 = new CoreClientParam();
                ccp2.ServerName = "ygjzjl.base.QueryData";
                ccp2.MethodName = "check_UpdateInfo";

                ccp2.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

                this.ExecuteNonQuery(ccp2, CoreInvokeType.Internal);

                if (ccp2.ReturnCode != 0)
                {
                    MessageBox.Show("数据修改失败！");
                }
                else
                {
                    lstHint2.Items.Add("数据修改成功！");
                }
            }*/

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
            strDepartMent = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
            try
            {
                strKey = this.CustomInfo.ToUpper();

            }
            catch (Exception E1)
            {
            }
            strWhere = " AND (FD_DATETIME BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
                + qDteEnd.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss'))";
            //strWhere += " AND FS_POINTID = 'K15'";

            strWhere += " AND FS_UPLOADFLAG = '0'";
            switch (strKey)
            {
                case "ZF":
                    strWhere += " AND  (FS_BATCHNO like '%TZ%' OR FS_BATCHNO LIKE '%TX%' ) ";
                    break;
                case "LH":
                    strWhere += "  AND  (FS_BATCHNO like '%TL%' OR FS_BATCHNO LIKE '%TG%' ) ";
                    break;
                default:
                   
                    break;

            }


            //strWhere += " AND FS_SAPSTORE = '1311'";//玉钢型材库存地
            //strWhere += " AND (FS_AUDITOR = '' OR FS_AUDITOR IS NULL)";
         /*   if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1002") || 
                (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1001"))
            {
                strWhere += " AND FS_BATCHNO LIKE ('2%')";
                ckTheory.Enabled = false;
                txtKcd.Text = "1450";
            }

            if (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC2001")
            {
                strWhere += " AND FS_BATCHNO LIKE ('1%')";
                ckTheory.Enabled = true;
            }*/

            showGridInfo();
        }

        private void qTxtScdd_Leave(object sender, EventArgs e)
        {
            if (qTxtScdd.Text.Trim() == "") return;
            if (!sapClass.chkScdd(txtScdd.Text))
            {
                txtScdd.Focus();
                return;
            }
        }

        private void txtScdd_Leave(object sender, EventArgs e)
        {
            if (txtScdd.Text.Trim() == "") return;
            //if (!sapClass.chkScdd(txtScdd.Text))
            //{
            //    txtScdd.Focus();
            //    return;
            //}

            //getOrderInfo(txtScdd.Text);
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
            switch (e.Tool.Key.ToString())
            {
                case "Cancel":
                    {
                        doUpdate(1);
                        showGridInfo();
                        break;
                    }
                case "find":
                    {
                        if (!getStrWhere())
                            break;
                        showGridInfo();
                        break;
                    }
                case "Edit":
                    {
                        //if (CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID() != "lt01") return;
                        doUpdate(0);
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
                case "inExcel":
                    {
                        openExcel();
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
            txtLh.Text = uGridData.ActiveRow.Cells["FS_BATCHNO"].Text + "-" + uGridData.ActiveRow.Cells["FN_BANDNO"].Text;
           // txtLh.Text = uGridData.ActiveRow.Cells["FS_BATCHNO"].Text;
            txtScdd.Text = uGridData.ActiveRow.Cells["FS_PRODUCTNO"].Text;
            txtHxmh.Text = uGridData.ActiveRow.Cells["FS_ITEMNO"].Text;          
            txtGc.Text = "1100";
            txtKcd.Text = uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text!=""?uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text:""; 
           // txtKcd.Text = "1450";//玉钢棒材库
          /*  if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1002") || 
                (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1001"))
            {
                txtKcd.Text = "1450";//玉钢棒材库
            }

            if (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC2001")
            {
                txtKcd.Text = "1450";
            }*/
            
            txtZl.Text = uGridData.ActiveRow.Cells["FN_WEIGHT"].Text;
            txtZl1.Text = uGridData.ActiveRow.Cells["FN_THEORYWEIGHT"].Text;
            cmbHead.Text = uGridData.ActiveRow.Cells["FS_HEADER"].Text;
            //tb_OldBatchNo.Text = uGridData.ActiveRow.Cells["FS_PRIBATCHNO"].Text;
        }

        private string getMaterial(string sDdh)
        {
            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "IT_PRODUCTDETAIL";
            strTmpField = "FS_MATERIAL";
            strTmpWhere = " AND FS_PRODUCTNO='" + sDdh + "'";

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" }; //
            dtOrder.Clear();
            ccpData.SourceDataTable = dtOrder;
            this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            if (dtOrder.Rows.Count > 0)
            {
                return dtOrder.Rows[0]["FS_MATERIAL"].ToString();
            }
            else
            {
                return "";
            }
        }

        private string getSelectNo()
        {
            string strTmp = "FS_BATCHNO IN (";
            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    if (!chkDataInfo(ugr.Index)) continue;
                    if (strTmp == "FS_BATCHNO IN (")
                    {
                        strTmp += "'" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'";
                    }
                    else
                    {
                        strTmp += ",'" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'";
                    }
                }
                if (strTmp == "FS_BATCHNO IN (")
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

        private string getSelectNo(int iOpt)
        {
            string strTmp = "FS_BATCHNO IN (";

            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    if (strTmp == "FS_BATCHNO IN (")
                    {
                        strTmp += "'" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'";
                    }
                    else
                    {
                        strTmp += ",'" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'";
                    }
                }
                if (strTmp == "FS_BATCHNO IN (")
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
            lstHint1.Items.Clear();
            string strTmpTable, strTmpField, strTmpWhere;
            CoreClientParam ccpOrderInfo = new CoreClientParam();
            ccpOrderInfo.ServerName = "ygjzjl.base.QueryData";
            ccpOrderInfo.MethodName = "queryData";

            strTmpTable = "IT_SAPLOG";
            strTmpField = "FS_DESCRIBE";
            strTmpWhere = " AND FS_DESCRIBE = '" + sLh + "'";
            System.Data.DataTable dtTmpData = new System.Data.DataTable();

            ccpOrderInfo.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, "" };
            ccpOrderInfo.SourceDataTable = dtTmpData;
            this.ExecuteQueryToDataTable(ccpOrderInfo, CoreInvokeType.Internal);
            string strTmp = "";
            strLh = dtTmpData.Rows[0][0].ToString();
            strTmp += strLh;
            lstHint1.Items.Add("上传日志：" + strTmp);
        }

        private void UpSap_KeyDown(object sender, KeyEventArgs e)
        {
            //if ((e.KeyCode != Keys.F2) && (e.KeyCode != Keys.F3)) return;
            //if (!chkSelectData()) return;

            //lstHint2.Items.Clear();
            //string strTmpTable, strTmpField;
            //string strTmpWhere = "";
            //strTmpTable = "dt_pipeweightmain";
            //strTmpField = "";

            //if (e.KeyCode == Keys.F2)
            //{
            //    if ((ckSC.Checked) || (ckQR1.Checked))
            //    {
            //        return;
            //    }

            //    if (DialogResult.Yes == MessageBox.Show("您将确认选中的数据信息，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            //    {
            //        strTmpField = "FS_ISMATCH='1',FS_AUDITOR='" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName() + "'";
            //    }
            //    else
            //        return;
            //    strTmpWhere = getSelectNo(0);
            //}

            //if (e.KeyCode == Keys.F3)
            //{
            //    if (ckSC.Checked)
            //    {
            //        MessageBox.Show("查询模式为【已上传】，不允许进行该操作！");
            //        return;
            //    }
            //    strTmpField = "FS_ISMATCH='0',FS_AUDITOR=''";
            //    strTmpWhere = getSelectNo(0);
            //}

            //if (strTmpWhere == "") return;

            //CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.StaticTrackWeight.StaticWeight";
            //ccp.MethodName = "check_UpdateInfo";

            //ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere };

            //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //if (ccp.ReturnCode == 0)
            //{
            //    showGridInfo();
            //}
        }

        private void upLoadData()
        {
          //  downSprueflosInfo("5000452515", "20071481");
           
            if (uGridData.Rows.Count == 0)
            {
                lstHint2.Items.Add("错误：没有需要上传的数据！");
                return;
            }

            //if (!ckQR1.Checked)
            //{
            //    MessageBox.Show("只有查询模式为【已确认】，才允许进行上传操作！");
            //    return;
            //}

            //if (uGridData.Rows[0].Cells["FS_ISMATCH"].Value.ToString() == "0")
            //{
            //    MessageBox.Show("你可能没有执行数据修改操作！");
            //    return;
            //}

            string strTmpMater, strTmpOrder, strTmpBatch1, strTmpBatch2, strTmpValue, strTmpJzrq, strTmpPrebatch,strPriBatchNo;
            strTmpMater = "";
            string strTmpNo = "";
            decimal dTmpZl = 0;
            string strXmh = "";
            System.Data.DataRow[] tmpRow;
            ArrayList listBatch = new ArrayList();
            string[] strTmpHeader = new string[] { "", "", "" };
            string[] strTmpUpload = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
           
            CoreClientParam ccp = new CoreClientParam();

          String  strTmpRfc = "BAPI_GOODSMVT_CREATE";
          String  strTmpCode = "01";
            strTmpOrder = "";

            tmpRow = dataSet1.Tables[0].Select("FS_ISMATCH='1' AND FS_UPLOADFLAG='0'", "FS_BATCHNO,FN_BANDNO");
            for (int i = 0; i < tmpRow.Length; i++)
            {
                ArrayList listItem = new ArrayList();
                ArrayList listSubItem = new ArrayList();
                //ArrayList listItem2 = new ArrayList();//上传批次特性时使用
                //ArrayList listSubItem2 = new ArrayList();
              //  if (tmpRow[i].Cells["FS_ISMATCH"].Value.ToString() == "0") continue;
               // if (!chkDataInfo(i)) continue;
                listBatch.Add(tmpRow[i]["FN_BANDNO"].ToString());
                strTmpBatch1 = tmpRow[i]["FS_BATCHNO"].ToString().Substring(0, 8);
                strTmpBatch2 = (i < (tmpRow.Length - 1)) ?
                    tmpRow[i + 1]["FS_BATCHNO"].ToString().Substring(0, 8) :
                    tmpRow[i]["FS_BATCHNO"].ToString().Substring(0, 8);
                if (ckTheory.Checked)
                    dTmpZl += Convert.ToDecimal(tmpRow[i]["FN_THEORYWEIGHT"]);
                else
                    dTmpZl += Convert.ToDecimal(tmpRow[i]["FN_WEIGHT"]);

                if ((strTmpBatch1 != strTmpBatch2) || (i == (tmpRow.Length - 1)))
                {
                    strTmpJzrq = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy-MM-dd");
                    strTmpHeader[0] = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy.MM.dd");
                    strTmpHeader[1] = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy.MM.dd");
                    strTmpHeader[2] = tmpRow[i]["FS_HEADER"].ToString();
                    
                    strTmpOrder = tmpRow[i]["FS_PRODUCTNO"].ToString();
                    strTmpMater = txtWlbh.Text.Trim();
                    strTmpNo = tmpRow[i]["FS_BATCHNO"].ToString();
                    strTmpPrebatch = tmpRow[i]["FS_GP_STOVENO"].ToString();
                    strXmh = tmpRow[i]["FS_ITEMNO"].ToString();
                    //strPriBatchNo=tmpRow[i]["FS_PRIBATCHNO"].ToString();

                    listSubItem.Clear();
                    listItem.Clear();
                    listSubItem.Add(strTmpMater);//物料编号
                    listSubItem.Add("1100");//工厂
                    //listSubItem.Add(tmpRow[i]["FS_PLANT"].ToString());//工厂

                    listSubItem.Add(tmpRow[i]["FS_SAPSTORE"].ToString());//库存地点
                    listSubItem.Add(strTmpNo);//批次
                    listSubItem.Add("101");//移动类型101
                    listSubItem.Add("");//库存类型
                    listSubItem.Add("");//特殊库存标识
                    listSubItem.Add(dTmpZl.ToString());//发货数量
                    listSubItem.Add("TON");//收货时的计量单位
                    listSubItem.Add(strXmh);//项目文本
                    listSubItem.Add(strTmpOrder);//采购订单编号
                    listSubItem.Add("B");//移动标识'B'
                    listSubItem.Add("");//销售订单号
                    listSubItem.Add("");//销售订单行项目
                    listItem.Add(listSubItem);

                    strTmpUpload[0] = strTmpMater;
                    strTmpUpload[1] = "101";
                    strTmpUpload[3] = tmpRow[i]["FS_PRODUCTNO"].ToString();
                    strTmpUpload[4] = strTmpNo;
                    strTmpUpload[5] = Convert.ToDateTime(this.GetServerTime()).ToString("yyyy-MM-dd HH:mm:ss");
                    strTmpUpload[7] = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    strTmpUpload[8] = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserID();
                    strTmpUpload[9] = tmpRow[i]["FS_ITEMNO"].ToString();
                    strTmpUpload[10] = strTmpUpload[5];

                    ccp.ServerName = "ygjzjl.sap.UploadSapRfc";
                    ccp.MethodName = "up_ContractForZG";

                    ccp.ServerParams = new object[] { strTmpRfc, strTmpHeader, strTmpCode, listItem };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++杨滔增上传上工序批次号功能

                    //listItem2.Add(strPriBatchNo);
                    //ccp.MethodName = "upBatchValue";

                    //ccp.ServerParams = new object[] { strTmpMater, strTmpNo, "8500", listItem2 };
                    //this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);

                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                    string sTmp;

                    if (ccp.ReturnCode == 0)
                    {
                        sTmp = ccp.ReturnObject.ToString();

                        strTmpValue = "FS_UPLOADFLAG='1',FS_SPRUEFLOS='" + sapClass.downSprueflosInfo(sTmp) + "'";
                       

                       // downSprueflosInfo(sTmp, strTmpNo);
                        
                        upTheoryWt(strTmpNo, strTmpHeader[0], strTmpOrder, strTmpMater, Convert.ToDateTime(tmpRow[i]["GBSJ"]).ToString("yyyy.MM.dd"),
                            tmpRow[i]["FS_SPEC"].ToString(), tmpRow[i]["FS_HEADER"].ToString());
                        //sapClass.insChemCopy(strTmpMater, tmpRow[i]["FS_PLANT"].ToString(),
                        //    strTmpNo, tmpRow[i]["FS_GP_STOVENO"].ToString());

                        strTmpUpload[2] = sTmp + "-" + tmpRow[i]["FS_BATCHNO"].ToString();
                        strTmpUpload[6] = dTmpZl.ToString();
                        sTmp = "炉号为" + strTmpNo + "-凭证号" + sTmp + "-上传重量" + dTmpZl.ToString();

                        sapClass.uptDataFlag("dt_pipeweightmain", "FS_UPLOADFLAG='1',FS_MATERIALNO='" + strTmpMater + "'", "FS_BATCHNO='" + strTmpNo
                         + "' AND FS_ACCOUNTDATE='" + strTmpJzrq + "'");

                        for (int j = 0; j < listBatch.Count; j++)
                        {
                            sapClass.uptDataFlag("dt_pipeweightdetail", strTmpValue, "FS_BATCHNO='" + strTmpNo
                                + "' AND FN_BANDNO=" + listBatch[j]);
                        }
                        listBatch.Clear();
                      //  sapClass.uptSapLog(strTmpNo, sapClass.strArrayUpload);
                        uptSapLog(strTmpNo, strTmpUpload);
                      //  sapClass.insMssqlData(1, "chemCopy", "PSTING_DATE,BATCH,PLANT,preBatch,MATERIAL",
                      //      "'" + strTmpJzrq + "','" + strTmpNo + "','1100','" + strTmpPrebatch + "','" + strTmpMater 
                      //      + "'");
                        sTmp += "，数据上传成功！";
                        //垛帐接口
                        //垛帐接口
                        DepotManage dm = new DepotManage();
                        bool flag = dm.UpdateMaterial(strTmpNo, strTmpMater);
                        
                       
                    }
                    else
                    {
                        sTmp = ccp.ReturnInfo;
                    }
                    //upTheoryWt("20080556", "2010.08.08", "000200017617", "BHRB3E0320011", "2010.08.08","HRB335E", "丙早");
                    dTmpZl = 0;//清0
                    lstHint2.Items.Add(sTmp);
                }
            }

            showGridInfo();
        }

        private void upTheoryWt(string sPch, string sJzrq, string sDdh, string sWlh,  string sGbrq, string sGg, string sScbc)
        {
            string strTmpTable, strTmpField, strTmpWhere, strTmpOrder;
            string strTmpFlag;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "dt_pipeweightdetail";
            strTmpField = "TO_CHAR(FD_DATETIME,'yyyy.MM.dd') AS GBSJ,FN_THEORYWEIGHT,FN_WEIGHT,"
                + "FS_TYPE,FS_SHIFT,FN_LENGTH,FN_BANDBILLETCOUNT,FN_BANDNO,FS_BATCHNO";
            strTmpWhere = " AND FS_BATCHNO='" + sPch + "' and FS_ISMATCH='1' ";
            strTmpOrder = " ORDER BY FS_BATCHNO,FN_BANDNO";

            ccpData.ServerParams = new object[] { strTmpTable, strTmpField, strTmpWhere, strTmpOrder }; //

            System.Data.DataTable dtTmpData = new System.Data.DataTable();
            ccpData.SourceDataTable = dtTmpData;
            try
            {
                this.ExecuteQueryToDataTable(ccpData, CoreInvokeType.Internal);
            }
            catch (Exception)
            {
            }

            if (dtTmpData.Rows.Count == 0) return;
            for (int i = 0; i < dtTmpData.Rows.Count; i++)
            {
                if (dtTmpData.Rows[i]["FN_BANDNO"].ToString().Length == 1)
                {
                    sPch = dtTmpData.Rows[i]["FS_BATCHNO"].ToString() + "0" + dtTmpData.Rows[i]["FN_BANDNO"].ToString();
                }
                else
                {
                    sPch = dtTmpData.Rows[i]["FS_BATCHNO"].ToString() + dtTmpData.Rows[i]["FN_BANDNO"].ToString();
                }

                if (Convert.ToDecimal(dtTmpData.Rows[i]["FN_THEORYWEIGHT"]) == Convert.ToDecimal(dtTmpData.Rows[i]["FN_WEIGHT"]))
                {
                    strTmpFlag = "0";
                }
                else
                {
                    strTmpFlag = "1";
                }

                CoreClientParam ccpSp = new CoreClientParam();
                ccpSp.ServerName = "ygjzjl.sap.UploadSapRfc";
                ccpSp.MethodName = "up_BcsjToSap";
                String updept = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();
                ccpSp.ServerParams = new object[] { "1", sPch, sJzrq, "1100", sDdh, sWlh, strTmpFlag,
                    dtTmpData.Rows[i]["FN_THEORYWEIGHT"].ToString(),dtTmpData.Rows[i]["FN_WEIGHT"].ToString(),
                    dtTmpData.Rows[i]["FS_TYPE"].ToString(),dtTmpData.Rows[i]["GBSJ"].ToString(),
                    updept,sGg,dtTmpData.Rows[i]["FS_SHIFT"].ToString(), sScbc,
                    dtTmpData.Rows[i]["FN_LENGTH"].ToString(),dtTmpData.Rows[i]["FN_BANDBILLETCOUNT"].ToString()
                };
                try
                {
                    this.ExecuteNonQuery(ccpSp, CoreInvokeType.Internal);
                }
                catch (Exception)
                {
                }
            }          
        }

        private void uptSapLog(string sTmpNo, string[] sArray)
        {
            string strTmpTable, strTmpField, strTmpWhere, strTmpValue;
            string strTmpDate, strTmpTime;

            strTmpDate = DateTime.Now.ToString("yyyy-MM-dd");
            strTmpTime = DateTime.Now.ToString("HH:mm:ss");
            

            strTmpTable = "IT_SAPLOG";
            strTmpField = "FS_MATERIALNO,FS_MOVETYPE,FS_DESCRIBE,FS_ORDERNO,FS_BATCHNO,FD_UPLOADTIME,"
                + "FN_UPLOADNUM,FS_USER,FS_USERNO,FS_ITEMNO,FD_STARTTIME,FD_ENDTIME";

            strTmpValue = "";
            for (int i = 0; i < sArray.Length - 1; i++)
            {
                if (i == 0)
                    strTmpValue = "'" + sArray[i] + "'";
                else
                    strTmpValue += ",'" + sArray[i] + "'";
            }

            strTmpValue += ",'" + Convert.ToDateTime(this.GetServerTime()).ToString("yyyy-MM-dd HH:mm:ss") + "'";
            CoreClientParam ccpLog = new CoreClientParam();
            ccpLog.ServerName = "ygjzjl.base.QueryData";
            ccpLog.MethodName = "insertDataInfo";

            ccpLog.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };

            this.ExecuteNonQuery(ccpLog, CoreInvokeType.Internal);
        }

        private void ckTheory_CheckedChanged(object sender, EventArgs e)
        {
            if (ckTheory.Checked==false)
            {
                if (DialogResult.No == MessageBox.Show("您将按实际重量上传数据，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    ckTheory.Checked = true;
                }
            }
        }
    }
}