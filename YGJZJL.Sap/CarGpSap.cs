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
    public partial class CarGpSap : FrmBase
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

        _Workbook workbook;

        //可以修改的数据信息
        string p_FS_PRODUCTNO = "";
        string p_FS_ITEMNO = "";
        string p_FS_MATERIALNAME = "";
        string p_FS_RECEIVEFACTORY = "";
        string p_FS_RECEIVESTORE = "";
        string p_FS_STOVENO = "";
        string p_FS_ACCOUNTDATE = "";

        //引入公用类
        SapClass sapClass = null;

        #endregion

        public CarGpSap()
        {
            InitializeComponent();
        }

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

            if (uGridData.Rows[iIndex].Cells["FS_SAPSTORE"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行库存地信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_ACCOUNTDATE"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行库存地信息不能为空！");
                return false;
            }

            if (uGridData.Rows[iIndex].Cells["FS_PLANT"].Value.ToString() == "")
            {
                //lstHint2.Items.Add("第" + iIndex.ToString() + "行库存地信息不能为空！");
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
        private void doUpdate(int flag)
        {
            if (flag==0 && (txtScdd.Text == "") || (txtGc.Text == "") || (txtKcd.Text == ""))
            {
                MessageBox.Show("工厂、炉号、生产订单与库存地都不能为空！");
                return;
            }

            string strTmpTable, strTmpField="", strTmpWhere;

            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    strOptNo = uGridData.Rows[ugr.Index].Cells["FS_STOVENO"].Value.ToString();
                    if (uGridData.Selected.Rows.Count == 1)
                    {
                        p_FS_STOVENO = (txtLh.Text != "") ? txtLh.Text : uGridData.Rows[ugr.Index].Cells["FS_STOVENO"].Value.ToString();
                    }
                    else
                    {
                        p_FS_STOVENO = uGridData.Rows[ugr.Index].Cells["FS_STOVENO"].Value.ToString();
                    }

                    p_FS_PRODUCTNO = (txtScdd.Text != "") ? txtScdd.Text : uGridData.Rows[ugr.Index].Cells["FS_PRODUCTNO"].Value.ToString();
                    p_FS_ITEMNO = (txtHxmh.Text != "") ? txtHxmh.Text : "0001";
                    p_FS_MATERIALNAME = "";
                    p_FS_RECEIVEFACTORY = (txtGc.Text != "") ? txtGc.Text : uGridData.Rows[ugr.Index].Cells["FS_PLANT"].Value.ToString();
                    p_FS_RECEIVESTORE = (txtKcd.Text != "") ? txtKcd.Text : uGridData.Rows[ugr.Index].Cells["FS_SAPSTORE"].Value.ToString();
                    p_FS_ACCOUNTDATE = dteJzrq.Value.ToString("yyyy-MM-dd");
                     strTmpTable = "IT_FP_TECHCARD";
                      strTmpWhere = "FS_GP_STOVENO ='" + strOptNo + "'";

                     if(flag==0)
                    strTmpField = "FS_PRODUCTNO='" + txtScdd.Text + "',FS_ITEMNO='0001',FS_SAPSTORE='" + txtKcd.Text + "',"
                        + "FS_ACCOUNTDATE='" + dteJzrq.Value.ToString("yyyy-MM-dd") + "',FS_PLANT='1100',FS_ISMATCH='1'";
                     else if(flag==1)
                             strTmpField = "FS_ISMATCH='0'";

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
            if (qDteBegin.Value > qDteEnd.Value.AddSeconds(10))
            {
                lstHint1.Items.Add("开始日期不能大于结束日期，请进行检查！");
                qDteBegin.Focus();
                return false;
            }


            strWhere = " AND (FD_GP_JUDGEDATE BETWEEN to_date('" + qDteBegin.Value.ToString("yyyy-MM-dd  00:00:00") + "','yyyy-MM-dd hh24:mi:ss') AND　to_date('"
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

            if (qTxtLh.Text.Trim() != "")
            {
                strWhere += " AND FS_GP_STOVENO = '" + qTxtLh.Text + "'";
            }

           // strWhere += " AND FS_POINT = 'K17'"; 

            strWhere += " AND FS_UPLOADFLAG = '0'";
            strWhere += " and FS_GP_STOVENO not like '%YG%'";

            strWhere += " and FS_TRANSTYPE='3'";//汽车倒运

           // txtKcd.Text = "1352";//玉钢钢坯库

            return true;
        }

        /// <summary>
        /// 从EXCEL导入数据
        /// </summary>
        private void openExcel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
          //  openFileDialog.InitialDirectory = @"E:\计量系统\安装3.0\";  //指定打开文件默认路径
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
        /// 根据订单号查询信息
        /// </summary>
        private void queryOrderInfo(string sDdh)
        {
            CoreClientParam ccpOrder = new CoreClientParam();
            ccpOrder.ServerName = "ygjzjl.base.QueryData";
            ccpOrder.MethodName = "query_DdInfo";
            ccpOrder.ServerParams = new object[] { "FS_PRODUCTNO ='"+sDdh+"' "  };

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

            for (int i = 1; i <= num; i++)
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
                   // val4 = values.GetValue(i, 4).ToString().Trim();  //物料编码
                   // val5 = values.GetValue(i, 5).ToString().Trim();  //物料名称
                    val6 = values.GetValue(i, 4).ToString().Trim();  //批号
                    val7 = values.GetValue(i, 5).ToString().Trim();  //收货数量
                    val8 = values.GetValue(i, 6).ToString().Trim();  //计量单位
                    val9 = values.GetValue(i, 7).ToString().Trim(); //工厂

                    val10 = values.GetValue(i, 8).ToString().Trim(); //库存地点
                   // val11 = values.GetValue(i, 11).ToString().Trim(); //移动类型
                    val12 = values.GetValue(i, 9).ToString().Trim(); //上工序批次
                   // val13 = values.GetValue(i, 13).ToString().Trim(); //特殊库存标志
                    //val14 = values.GetValue(i, 14).ToString().Trim(); //销售订单号
                    //val15 = values.GetValue(i, 15).ToString().Trim(); //销售订单行项目号
                    val16 = values.GetValue(i, 10).ToString().Trim(); //抬头文本

                    string strLRR = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();
                    string strLRD = CoreFS.SA06.CoreUserInfo.UserInfo.GetDepartment();

                    strTmpTable = "dt_carweight_weight";
                    strTmpField = "fs_weightno,FS_ACCOUNTDATE,FS_PRODUCTNO,FS_ITEMNO,FS_STOVENO,FN_NETWEIGHT,FS_PLANT,FS_SAPSTORE,FD_GP_JUDGEDATE";
                    strTmpValue = "'" + Guid.NewGuid().ToString() + "','" + val1 + "','" + val2 + "','" + val3 + "','" + val6 + "','" + val7 + "','" + val9+ "','" 
                        + val10 + "',sysdate";

                    CoreClientParam ccp = new CoreClientParam();
                    ccp.ServerName = "ygjzjl.base.QueryData";
                    ccp.MethodName = "insertDataInfo";
                    ccp.ServerParams = new object[] { strTmpTable, strTmpField, strTmpValue };
                    this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
                }
            }
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

            strTmpTable = "IT_FP_TECHCARD";
            strTmpField = "TO_CHAR(FD_GP_JUDGEDATE,'yyyy-MM-dd HH24:mi:ss') AS GBSJ,FS_GP_STOVENO as FS_STOVENO,FS_PRODUCTNO,"
                + "FS_ITEMNO,FS_PLANT,FS_SAPSTORE,FS_UPLOADFLAG,FN_JJ_WEIGHT as FN_NETWEIGHT,FS_ACCOUNTDATE, FN_GP_TOTALCOUNT as FN_BILLETCOUNT,FS_ISMATCH";
            strTmpOrder = " ORDER BY FD_GP_JUDGEDATE";
            

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
                lstHint2.Items.Add("信息查询异常！");
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

           // txtKcd.Text = "1352";//1352玉钢钢坯库

            getStrWhere();

            showGridInfo();
        }

        private void qTxtScdd_Leave(object sender, EventArgs e)
        {
            if (!sapClass.chkScdd(qTxtScdd.Text))
            {
                qTxtScdd.Focus();
            }
        }

        private void txtScdd_Leave(object sender, EventArgs e)
        {
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
                case "find":
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
                case "Canle":
                    {
                        doUpdate(1);
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
            catch (Exception e1)
            {
            }

            strOptNo = uGridData.ActiveRow.Cells["FS_STOVENO"].Text;
            txtLh.Text = uGridData.ActiveRow.Cells["FS_STOVENO"].Text;
            txtScdd.Text = uGridData.ActiveRow.Cells["FS_PRODUCTNO"].Text;
            txtHxmh.Text = uGridData.ActiveRow.Cells["FS_ITEMNO"].Text;          
            txtGc.Text = "1100";
            txtKcd.Text = uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text!="" ? uGridData.ActiveRow.Cells["FS_SAPSTORE"].Text : "";
            txtZl.Text = uGridData.ActiveRow.Cells["FN_NETWEIGHT"].Text;
           // txtKcd.Text = "1352";
        }

        private string getSelectNo()
        {
            string strTmp = "FS_STOVENO IN (";
            if (uGridData.Selected.Rows.Count > 0)
            {
                foreach (UltraGridRow ugr in uGridData.Selected.Rows)
                {
                    if (!chkDataInfo(ugr.Index)) continue;
                    if (strTmp == "FS_STOVENO IN (")
                    {
                        strTmp += "'" + uGridData.Rows[ugr.Index].Cells["FS_STOVENO"].Value.ToString() + "'";
                    }
                    else
                    {
                        strTmp += ",'" + uGridData.Rows[ugr.Index].Cells["FS_STOVENO"].Value.ToString() + "'";
                    }
                }
                if (strTmp == "FS_STOVENO IN (")
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

            string strTmpMater, strTmpOrder, strTmpBatch1, strTmpBatch2;
            strTmpMater = "";
            string strTmpNo = "";
            decimal dTmpZl = 0;
            System.Data.DataRow[] tmpRow;

            string[] strTmpHeader = new string[] { "", "", "" };
           
            strTmpOrder = "";
            tmpRow = dataSet1.Tables[0].Select("FS_ISMATCH='1' AND FS_UPLOADFLAG='0'", "FS_STOVENO");

            for (int i = 0; i < tmpRow.Length; i++)
            {
                ArrayList listSubItem = new ArrayList();
                ArrayList listItem = new ArrayList();
                //  if (tmpRow[i].Cells["FS_ISMATCH"].Value.ToString() == "0") continue;
                // if (!chkDataInfo(i)) continue;
              //  listBatch.Add(tmpRow[i]["FN_BANDNO"].ToString());
                strTmpBatch1 = tmpRow[i]["FS_STOVENO"].ToString().Substring(0, 9);
                strTmpBatch2 = (i < (tmpRow.Length - 1)) ?
                    tmpRow[i + 1]["FS_STOVENO"].ToString().Substring(0, 9) :
                    tmpRow[i]["FS_STOVENO"].ToString().Substring(0, 9);
                dTmpZl += Convert.ToDecimal(tmpRow[i]["FN_NETWEIGHT"]);
               // if (!chkDataInfo(i)) continue;
                if ((strTmpBatch1 != strTmpBatch2) || (i == (tmpRow.Length - 1)))
                {
                    strTmpHeader[0] = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy.MM.dd");
                    strTmpHeader[1] = Convert.ToDateTime(tmpRow[i]["FS_ACCOUNTDATE"]).ToString("yyyy.MM.dd");
                    strTmpHeader[2] = "";

                    //dTmpZl = Convert.ToDecimal(tmpRow[i]["FN_NETWEIGHT"]);
                    strTmpOrder = tmpRow[i]["FS_PRODUCTNO"].ToString();
                    strTmpMater = sapClass.getMaterial(strTmpOrder);
                  //  strTmpNo = tmpRow[i]["FS_STOVENO"].ToString();
                    strTmpNo = strTmpBatch1;

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


                    //string sTmp = sapClass.BAPI_GOODSMVT_CREATE(strTmpHeader, listSubItem);
                    string sTmp = sapClass.BAPI_GOODSMVT_CREATE("02",strTmpHeader, listSubItem);

                    if (sTmp != "")
                    {
                        sapClass.strArrayUpload[2] = sTmp + "-" + strTmpBatch1;
                        sapClass.strArrayUpload[6] = dTmpZl.ToString();
                        sTmp = "炉号为" + strTmpNo + "-凭证号" + sTmp + "-上传重量" + dTmpZl.ToString(); ;
                      //  sapClass.ins261Data(strTmpMater, dTmpZl.ToString(), strTmpNo, tmpRow[i]["FS_SAPSTORE"].ToString());
                        sapClass.uptDataFlag("IT_FP_TECHCARD", "FS_UPLOADFLAG='1'", "FS_GP_STOVENO like'%" + strTmpNo + "%'");
                        sapClass.uptSapLog(strTmpNo, sapClass.strArrayUpload);
                        sTmp += "，数据上传成功！";
                        lstHint2.Items.Add(sTmp);
                    }
                    else
                    {
                        lstHint2.Items.Add(sapClass.strSapError);
                       
                    }
                    dTmpZl = 0;//清0
                   // lstHint2.Items.Add(sTmp);
                }
            }

            showGridInfo();
        }
    }
}