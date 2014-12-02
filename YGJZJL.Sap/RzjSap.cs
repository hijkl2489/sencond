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
    public partial class RzjSap : FrmBase
    {
        #region 变量定义

        //EXCEL应用
        Excel.Application app;

        //使用公用对象
      //  BaseInfo objBi = new BaseInfo();

        //查询条件语句
        string strWhere = "";

        _Workbook workbook;

        //可以修改的数据信息
        string p_FS_PRODUCTNO = "";
        string p_FS_ITEMNO = "";
        string p_FS_MATERIALNAME = "";
        string p_FS_RECEIVEFACTORY = "";
        string p_FS_RECEIVESTORE = "";
        string p_FS_BATCHNO = "";
        string p_FS_ACCOUNTDATE = "";

        //引入公用类
        SapClass sapClass = null;
      //  SapClass sapClass = new SapClass();

        //本地缓存订单号信息
        System.Data.DataTable dtOrder = new System.Data.DataTable();

        //操作序号
        string strOptNo = "";
      
        public RzjSap()
        {
            InitializeComponent();
           // sapClass.ob = (OpeBase)obb;
        }


        #endregion
       
         

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

        /// <summary>
        /// 根据订单号从SAP下载订单信息
        /// </summary>
        private void downOrderInfo(string sDdh)
        {
            lstHint2.Items.Clear();
            if (dtOrder.Rows.Count > 0) dtOrder.Clear();
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
                    strOptNo = uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString();
                    if (uGridData.Selected.Rows.Count == 1)
                    {
                        p_FS_BATCHNO = (txtLh.Text != "") ? txtLh.Text : uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString();
                    }
                    else
                    {
                        p_FS_BATCHNO = uGridData.Rows[ugr.Index].Cells["FS_BATCHNO"].Value.ToString();
                    }

                    p_FS_PRODUCTNO = (txtScdd.Text != "") ? txtScdd.Text : uGridData.Rows[ugr.Index].Cells["FS_PRODUCTNO"].Value.ToString();
                    p_FS_ITEMNO = (txtHxmh.Text != "") ? txtHxmh.Text : "0001";
                    p_FS_MATERIALNAME = sapClass.getMaterialName(sapClass.getMaterial(p_FS_PRODUCTNO));
                    p_FS_RECEIVEFACTORY = (txtGc.Text != "") ? txtGc.Text : uGridData.Rows[ugr.Index].Cells["FS_PLANT"].Value.ToString();
                    p_FS_RECEIVESTORE = (txtKcd.Text != "") ? txtKcd.Text : uGridData.Rows[ugr.Index].Cells["FS_SAPSTORE"].Value.ToString();
                    p_FS_ACCOUNTDATE = dteJzrq.Value.ToString("yyyy-MM-dd");

                    strTmpTable = "DT_PLATECONFIRMWEIGHTMAIN";
                    strTmpField = "";
                    switch(iFlag)
                    {
                        case 0:
                            strTmpField = "FS_PRODUCTNO='" + txtScdd.Text + "',FS_ITEMNO='0001',FS_SAPSTORE='" + txtKcd.Text + "',"
                                + "FS_ACCOUNTDATE='" + dteJzrq.Value.ToString("yyyy-MM-dd") + "',FS_PLANT='1100',FS_HEADER='"
                                + cmbHead.Text + "',FS_ISMATCH=1,FS_AUDITOR='" + CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName()
                                + "'";
                            break;
                        case 1:
                            strTmpField = "FS_ISMATCH=0" ;
                            break;
                    }
                    strTmpWhere = "FS_BATCHNO='" + strOptNo + "'";

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
        /// 获取用户选中的行
        /// </summary>
        private string getSelectNo()
        {
            string strTmp = "FS_WEIGHTNO IN (";
            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
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

        /// <summary>
        /// 查询信息
        /// </summary>
        private bool getStrWhere()
        {
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                lstHint1.Items.Add("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }


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

            strWhere += " AND FS_UPLOADFLAG = '0'";

            if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1001") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1002"))
            {
                strWhere += " AND FS_DRDW = '棒线厂一作'";
                txtKcd.Text = "8005";
            }
            if (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC2001")
            {
                strWhere += " AND FS_DRDW = '棒线厂二作'";
            }
            

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

            string strTmpFile = openFileDialog.FileName.ToLower();

            if (strTmpFile == "")
            {
                MessageBox.Show("请选择导入的Excel文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
            string[] strTmpArray = strTmpFile.Split('\\');
            strTmpFile = strTmpArray[strTmpArray.Length - 1];

            if (strTmpFile != "生产发料.xls")
            {
                MessageBox.Show("请选择生产发料.xls文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ReadAndSaveExcelInfo();
        }

        /// <summary>
        /// 根据订单号查询信息
        /// </summary>
        private void queryOrderInfo(string sDdh)
        {
            CoreClientParam ccpOrder = new CoreClientParam();
            ccpOrder.ServerName = "ygjzjl.base.QueryData";
            ccpOrder.MethodName = "query_DdInfo";
            ccpOrder.ServerParams = new object[] { "FS_PRODUCTNO ='" + sDdh + "' " };

            dtOrder.Clear();
            ccpOrder.SourceDataTable = dtOrder;
            this.ExecuteQueryToDataTable(ccpOrder, CoreInvokeType.Internal);
            if (dtOrder.Rows.Count > 0)
            {
                txtScdd.Text = sDdh;
                txtWlmc.Text = dtOrder.Rows[0][1].ToString();
                txtGc.Text = dtOrder.Rows[0][2].ToString();
                txtHxmh.Text = dtOrder.Rows[0][3].ToString();
                txtWlbh.Text = dtOrder.Rows[0][4].ToString();
            }
        }

        /// <summary>
        /// 读取并保存Excel中的信息到数据库中
        /// </summary>
        private void ReadAndSaveExcelInfo()
        {
            //Workbook wBook = excel.Workbooks.Add(true);
            Sheets sheets = workbook.Worksheets;
            _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
            //Worksheet wSheet = (Excel._Worksheet)wBook.ActiveSheet;
            //Worksheet worksheet = (Excel.Worksheet)wBook.ActiveSheet;
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;

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

            int num = 0;

            Excel.Range range = worksheet.get_Range("A2", "P1000");//A2、O1000表示到Excel从第A列的第2行到第O列的第1000-1行  A2、P65535表示到Excel从第A列的第2行到第P列的第65535-1行 
            System.Array values = (System.Array)range.Formula;
            num = values.GetLength(0);

            for (int i = 1; i <= num; i++)
            {
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
                    val11 = values.GetValue(i, 11).ToString().Trim(); //抬头文本

                    string strLRR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    string strLRD = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();

                    strTmpTable = "DT_SAP261";
                    strTmpField = "FS_WEIGHTNO,FS_ACCOUNTDATE,FS_PRODUCTNO,FS_ITEMNO,FS_MATERIAL,FS_MATERIALNAME,FS_STOVENO,"
                        + "FN_NETWEIGHT,FS_PLANT,FS_SAPSTORE,FS_HEADER,FS_WEIGHTTYPE,FS_DRDW,FS_AUDITOR";
                    strTmpValue = "'" + Guid.NewGuid().ToString() + "','" + val1 + "','" + val2 + "','" + val3 + "','" 
                        + val4 + "','" + val5 + "','" + val6 + "'," + val7 + ",'" + val9 + "','" + val10
                        + "','" + val11 + "'," + "'261',";

                  /*  if ((CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1001") || (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC1002"))
                    {
                        strTmpValue += "'棒线厂一作',";
                    }
                    if (CoreFS.SA06.CoreUserInfo.UserInfo.GetRole() == "BXC2001")
                    {
                        strTmpValue += "'棒线厂二作',";
                    }*/
                    strTmpValue += "'" + strLRD + "','" + strLRR + "'";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.base.QueryData";
                    ccp.MethodName = "insertDataInfo";
                    ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };
                    try
                    {
                        this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                    }
                    catch(Exception ex1)
                    {
                        lstHint1.Items.Add(ex1.Message + "第" + i.ToString() + "行信息导入成功！");
                    }
                }
            }

            app.Quit();
            app = null;

            lstHint1.Items.Add("信息导入成功！");

            System.Diagnostics.Process[] myProcesses;         
            myProcesses = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process myProcess in myProcesses)
            {
                if(myProcess.ProcessName == "EXCEL")
                    myProcess.Kill();
            }
        }

        /// <summary>
        /// 显示板坯信息
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
            ugpData.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
            lblScr.Text += CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
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
                case "Find":
                    {
                        if (!getStrWhere())
                            break;
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
                case "InExcel":
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

            txtLh.Text = uGridData.ActiveRow.Cells["FS_STOVENO"].Text;
            txtScdd.Text = uGridData.ActiveRow.Cells["FS_PRODUCTNO"].Text;
            txtHxmh.Text = "0001";          
            txtGc.Text = "1100";
            txtKcd.Text = uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text != "" ? uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text : "8014";
            txtZl.Text = uGridData.ActiveRow.Cells["FN_NETWEIGHT"].Text;
            cmbHead.Text = uGridData.ActiveRow.Cells["FS_HEADER"].Text;
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

                    //sapClass.execute261(strTmpPrebatch);
                    
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

        private void ckExcel_CheckedChanged(object sender, EventArgs e)
        {
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
            CoreClientParam ccpDel = new CoreClientParam();
            ccpDel.ServerName = "ygjzjl.base.QueryData";
            ccpDel.MethodName = "DeleteData";

            ccpDel.ServerParams = new object[] { strTmpTable, strTmpWhere };

            this.ExecuteNonQuery(ccpDel, CoreInvokeType.Internal);
            if (ccpDel.ReturnCode == 0)
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