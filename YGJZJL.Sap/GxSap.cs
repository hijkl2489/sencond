/*程序更新维护
 * 1、20110119按蒋云要求，用户更改订单后，把数据库上传垛帐标志复位；
 * 2、20110328按MVC架构设计，上传界面与视拓框架拆开；
 */ 
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
    public partial class GxSap : FrmBase
    {
        #region 变量定义

        //EXCEL应用
        Excel.Application app;

        //使用公用对象
        BaseInfo objBi = new BaseInfo();

        //查询条件语句
        string strWhere = "";

        //本地缓存订单号信息
        System.Data.DataTable dtOrder = new System.Data.DataTable();

        //操作序号
        string strOptNo = "";

        _Workbook workbook;

        //可以修改的数据信息
        string p_FS_PRODUCTNO = "";
        string p_FS_MATERIAL = "";

        //引入公用类
       // SapClass sapClass = new SapClass();
        SapClass sapClass = null;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public GxSap()
        {
            InitializeComponent();
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
        /// 根据订单号从SAP下载订单信息
        /// </summary>
        private void downOrderInfo(string sDdh)
        {
            lstHint2.Items.Clear();
            dtOrder.Clear();
            dtOrder = sapClass.downOrderInfo(sDdh);

            if (dtOrder.Rows.Count == 0)
            {
                lstHint2.Items.Add("生产订单信息下载失败！");
                return;
            }
            
            lstHint2.Items.Add("生产订单信息下载成功！");
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
                    MessageBox.Show("工厂、生产订单、库存地都不能为空！");
                    return;
                }
            }

            string strTmpTable, strTmpField, strTmpWhere;

            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    //if (Convert.ToDecimal(uGridData.Rows[ugr.Index].Cells["FN_JJ_WEIGHT"].Value) < Convert.ToDecimal(uGridData.Rows[ugr.Index].Cells["FN_WEIGHT"].Value))
                    //{
                    //    if (DialogResult.No == MessageBox.Show(strOptNo + "已经材大于坯，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    //    {
                    //        continue;
                    //    }
                    //}
                    /*
                    p_FS_PRODUCTNO = (txtScdd.Text != "") ? txtScdd.Text : uGridData.Rows[ugr.Index].Cells["FS_PRODUCTNO"].Value.ToString();
                    p_FS_ITEMNO = (txtHxmh.Text != "") ? txtHxmh.Text : "0001";
                    p_FS_MATERIALNAME = "";
                    p_FS_RECEIVEFACTORY = (txtGc.Text != "") ? txtGc.Text : uGridData.Rows[ugr.Index].Cells["FS_PLANT"].Value.ToString();
                    p_FS_RECEIVESTORE = txtKcd.Text;
                    p_FS_ACCOUNTDATE = dteJzrq.Value.ToString("yyyy-MM-dd");
                     */

                    if (iFlag == 0)
                    {
                        if (txtWlbh.Text.Trim() == "") sapClass.getMaterial(txtScdd.Text);
                    }
                    
                    strTmpTable = "DT_GX_STORAGEWEIGHTMAIN";
                    strTmpWhere = "FS_BATCHNO ='" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'";

                    switch (iFlag)
                    {
                        case 0:
                            strTmpField = "FS_PRODUCTNO='" + txtScdd.Text + "',FS_SAPSTORE='" + txtKcd.Text + "',"
                                + "FS_ACCOUNTDATE='" + dteJzrq.Value.ToString("yyyy-MM-dd") + "',FS_PLANT='" + txtGc.Text + "',"
                                + "FS_MATERIALNO='" + txtWlbh.Text + "',FS_MATERIALNAME='"
                                + txtWlmc.Text + "',FS_HEADER='" + cmbHead.Text + "'";
                            try
                            {
                                sapClass.uptData(strTmpTable, strTmpField, strTmpWhere);
                            }
                            catch (Exception)
                            { }
                            break;
                        case 1:
                            break;
                    }
 
                    strTmpTable = "DT_GX_STORAGEWEIGHTDETAIL";
                    strTmpField = "";
                    switch (iFlag)
                    {
                        case 0:
                            strTmpField = "FS_PRODUCTNO='" + txtScdd.Text + "',FS_SAPSTORE='" + txtKcd.Text + "',"
                                + "FS_ACCOUNTDATE='" + dteJzrq.Value.ToString("yyyy-MM-dd") + "',FS_PLANT='" + txtGc.Text + "',"
                                + "FS_HEADER='" + cmbHead.Text + "',FS_ISMATCH='1',FS_UPFLAG='0'";
                            break;
                        case 1:
                            strTmpField = "FS_ISMATCH=0";
                            break;
                    }
                    strTmpWhere = "FS_BATCHNO ='" + uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString() + "'"
                        + " AND FN_BANDNO=" + uGridData.Rows[ugr.Index].Cells["FN_BANDNO"].Value.ToString();

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
            string[] strTmpArray = { "", "", "", "", "", "" };
            strTmpArray = sapClass.getOrdInfo(sDdh);

            if (strTmpArray[1] == "")
            {
                lstHint2.Items.Add("生产订单信息获取失败，从SAP下载！");
                sapClass.downOrderInfo(sDdh);
                strTmpArray = sapClass.getOrdInfo(sDdh);
            }

            if (strTmpArray[1] == "")
            {
                lstHint2.Items.Add("生产订单信息获取失败！");
            }

            txtScdd.Text = sDdh;
            txtHxmh.Text = strTmpArray[0];
            txtWlbh.Text = strTmpArray[1];
            txtGc.Text = strTmpArray[2];
            txtWlmc.Text = sapClass.getMaterialName(strTmpArray[1]);
        }

        /// <summary>
        /// 根据订单号查询物料信息
        /// </summary>
        private bool getStrWhere()
        {
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                lstHint1.Items.Add("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }


            strWhere = " AND (FD_DATETIME BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
                + qDteEnd.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-MM-dd hh24:mi:ss'))";

            sapClass.strCardWhere = " AND (t2.FD_ENDTIME BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") 
                + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('" + qDteEnd.Value.ToString("yyyy-MM-dd 23:59:59")
                + "','yyyy-MM-dd hh24:mi:ss')) AND t2.FS_UPLOADFLAG = '0' AND t2.FS_COMPLETEFLAG = '1'";

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

            if (qTxtLh.Text.Trim() != "")
            {
                strWhere += " AND FS_BATCHNO = '" + qTxtLh.Text + "'";
            }

            strWhere += " AND FS_UPLOADFLAG = '0'";

            strWhere += " AND FS_COMPLETEFLAG = '1'";

          //  txtKcd.Text = "1351";//玉钢线材库

            return true;
        }

        /// <summary>
        /// 从EXCEL导入数据
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

                    strTmpTable = "DT_GX_STORAGEWEIGHTMAIN";
                    strTmpField = "FS_ACCOUNTDATE,FS_PRODUCTNO,FS_ITEMNO,FS_BATCHNO,FN_TOTALWEIGHT,FS_PLANT,"
                        + "FS_SAPSTORE,FS_FLOW,FS_HEADER";
                    strTmpValue = "'" + val1 + "','" + val2 + "','" + val3 + "','" + val6 + "','" + val7 + "','" + val8
                        + "','" + val10 + "','" + val11 + "','" + val16 + "'";

                    sapClass.insData(strTmpTable, strTmpField, strTmpValue);
                }
            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        private void showGridInfo()
        {
            string strTmpTable, strTmpField, strTmpOrder;
            //System.Data.DataTable dtTmp;
            CoreClientParam ccpData = new CoreClientParam();
            ccpData.ServerName = "ygjzjl.base.QueryData";
            ccpData.MethodName = "queryData";

            strTmpTable = "VIEW_GX_DATA1";
            strTmpField = "TO_CHAR(FD_DATETIME,'yyyy-MM-dd HH24:mi:ss') AS GBSJ,FS_BATCHNO,FN_BANDNO,FS_PRODUCTNO,"
                + "FS_PLANT,FS_SAPSTORE,FS_UPLOADFLAG,FN_WEIGHT,FS_ACCOUNTDATE,FS_STEELTYPE,FS_SPEC,FS_ISMATCH,"
                + "FS_HEADER,FS_GP_STOVENO,FN_JJ_WEIGHT";
            strTmpOrder = " ORDER BY FS_BATCHNO,FN_BANDNO";

            
            //dtTmp = sapClass.selData(strTmpTable, strTmpField, strWhere, strTmpOrder);
            //dataSet1.Tables[0]=dtTmp;

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
                lstHint1.Items.Add(ex1.Message + "信息查询异常！");
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

           // txtKcd.Text = "1310";//玉钢线材库

            getStrWhere();

            showGridInfo();
        }

        private void qTxtScdd_Leave(object sender, EventArgs e)
        {
            if (qTxtScdd.Text.Trim() == "") return;
            if (!sapClass.chkScdd(qTxtScdd.Text))
            {
                qTxtScdd.Focus();
            }
        }

        private void txtScdd_Leave(object sender, EventArgs e)
        {
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
                MessageBox.Show("炉号不能大于10位，请重新进行输入！");
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
            lstHint1.Items.Clear();
            lstHint2.Items.Clear();
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
                        sapClass.chkCardNo("DT_GX_STORAGEWEIGHTMAIN");
                        showGridInfo();
                        break;
                    }
                case "Edit":
                    {
                        doUpdate(0);
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
            txtScdd.Text = uGridData.ActiveRow.Cells["FS_PRODUCTNO"].Text;
            txtHxmh.Text = "0001";
            txtGc.Text = uGridData.ActiveRow.Cells["FS_PLANT"].Text;
            txtKcd.Text = uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text!="" ? uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text : "";
            txtZl.Text = uGridData.ActiveRow.Cells["FN_WEIGHT"].Text;
            cmbHead.Text = uGridData.ActiveRow.Cells["FS_HEADER"].Text;
        }

        private void upLoadData()
        {
            if (uGridData.Rows.Count == 0)
            {
                lstHint2.Items.Add("错误：没有需要上传的数据！");
                return;
            }

            string strTmpMater, strTmpOrder, strTmpBatch1, strTmpBatch2, strTmpValue, strTmpJzrq, strTmpPrebatch;
            strTmpMater = "";
            string strTmpNo = "";
            decimal dTmpZl = 0;

            string[] strTmpHeader = new string[] { "", "", "" };
            ArrayList listSubItem = new ArrayList();
            ArrayList listBatch = new ArrayList();
            strTmpOrder = "";
            dTmpZl = 0;
            System.Data.DataRow[] tmpRow;
            //edit
            tmpRow = dataSet1.Tables[0].Select("FS_ISMATCH='1' AND FS_UPLOADFLAG='0'", "FS_BATCHNO,FN_BANDNO");

            for (int i = 0; i < tmpRow.Length; i++)
            {
                //strTmpOrder = tmpRow[i]["FS_PRODUCTNO"].ToString();
                //strTmpMater = sapClass.getMaterial(strTmpOrder);
                //strTmpNo = (tmpRow[i]["FS_BATCHNO"].ToString());
                ////strTmpNo = (tmpRow[i]["FN_BANDNO"].ToString().Length == 1) 
                ////    ? tmpRow[i]["FS_BATCHNO"].ToString() + "0" + tmpRow[i]["FN_BANDNO"].ToString()
                ////    : tmpRow[i]["FS_BATCHNO"].ToString() + tmpRow[i]["FN_BANDNO"].ToString();
                //sapClass.batchValueUp(strTmpMater, strTmpNo, "8000");
                //break;

                listBatch.Add(tmpRow[i]["FN_BANDNO"].ToString());
                strTmpBatch1 = tmpRow[i]["FS_BATCHNO"].ToString().Substring(0, 8);
                strTmpBatch2 = (i < (tmpRow.Length - 1)) ?
                    tmpRow[i + 1]["FS_BATCHNO"].ToString().Substring(0, 8) :
                    tmpRow[i]["FS_BATCHNO"].ToString().Substring(0, 8);              

                dTmpZl += Convert.ToDecimal(tmpRow[i]["FN_WEIGHT"]);
                if ((strTmpBatch1 != strTmpBatch2) || (i == (tmpRow.Length - 1)))
                {
                    strTmpJzrq = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy-MM-dd");
                    strTmpHeader[0] = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy.MM.dd");
                    strTmpHeader[1] = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy.MM.dd");
                    strTmpHeader[2] = tmpRow[i]["FS_HEADER"].ToString();

                    strTmpOrder = tmpRow[i]["FS_PRODUCTNO"].ToString();
                    strTmpMater = sapClass.getMaterial(strTmpOrder);
                    strTmpNo = tmpRow[i]["FS_BATCHNO"].ToString();
                    strTmpPrebatch = tmpRow[i]["FS_GP_STOVENO"].ToString();

                    listSubItem.Clear();
                    listSubItem.Add(strTmpMater);//物料编号
                    listSubItem.Add(tmpRow[i]["FS_PLANT"].ToString());//工厂
                    listSubItem.Add(tmpRow[i]["FS_SAPSTORE"].ToString());//库存地点
                    listSubItem.Add(strTmpNo);//批次
                    listSubItem.Add("101");//移动类型101
                    listSubItem.Add("");//库存类型
                    listSubItem.Add("");//特殊库存标识
                    listSubItem.Add(dTmpZl.ToString());//发货数量
                    listSubItem.Add("TON");//收货时的计量单位
                    listSubItem.Add("0001");//项目文本
                    listSubItem.Add(strTmpOrder);//生产订单编号
                    listSubItem.Add("F");//移动标识'F'
                    listSubItem.Add("");//销售订单号
                    listSubItem.Add("");//销售订单行项目

                    string sTmp = sapClass.BAPI_GOODSMVT_CREATE("02", strTmpHeader, listSubItem);

                    if (sTmp != "")
                    {
                        strTmpValue = "FS_UPLOADFLAG='1',FS_SPRUEFLOS='" + sapClass.downSprueflosInfo(sTmp) + "'";
                        sapClass.strArrayUpload[2] = sTmp + "-" + tmpRow[i]["FS_BATCHNO"].ToString();
                        sapClass.strArrayUpload[6] = dTmpZl.ToString();
                        sTmp = "炉号为" + strTmpBatch1 + "-凭证号" + sTmp + "-上传重量" + dTmpZl.ToString();
                    //    sapClass.insChemCopy(strTmpMater, tmpRow[i]["FS_PLANT"].ToString(),
                      //      strTmpNo, tmpRow[i]["FS_GP_STOVENO"].ToString());
                        sapClass.uptDataFlag("DT_GX_STORAGEWEIGHTMAIN", "FS_UPLOADFLAG='1',FS_MATERIALNO='" + strTmpMater + "'", "FS_BATCHNO='" + strTmpNo
                            + "' AND FS_ACCOUNTDATE='" + strTmpJzrq + "'");
                        //sapClass.uptDataFlag("DT_GX_STORAGEWEIGHTDETAIL", "FS_UPLOADFLAG='1'", "FS_BATCHNO='" + strTmpNo
                            //+ "' AND FS_ACCOUNTDATE='" + strTmpJzrq + "'");
                        for (int j = 0; j < listBatch.Count; j++)
                        {
                            sapClass.uptDataFlag("DT_GX_STORAGEWEIGHTDETAIL", strTmpValue, "FS_BATCHNO='" + strTmpNo
                                + "' AND FN_BANDNO=" + listBatch[j]);
                        }
                        listBatch.Clear();
                        sapClass.uptSapLog(strTmpNo, sapClass.strArrayUpload);
          //              sapClass.insMssqlData(1, "chemCopy", "PSTING_DATE,BATCH,PLANT,preBatch,MATERIAL",
            //                "'" + strTmpJzrq + "','" + strTmpBatch1 + "','"
              //              + "1100','" + strTmpPrebatch + "','" + strTmpMater + "'");
                        sTmp += "，数据上传成功！";

                        //垛帐接口
                        DepotManage dm = new DepotManage();
                        bool flag = dm.UpdateMaterial(strTmpNo, strTmpMater);
                       
                        
                        //dTmpZl = 0;//清0
                        
                        lstHint2.Items.Add(sTmp);
                    }
                    else
                    {
                        lstHint2.Items.Add(sapClass.strSapError);
                    }
                    dTmpZl = 0;
                }
                
            }
            showGridInfo();
        }
    }
}