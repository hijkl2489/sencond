using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YGJZJL.PublicComponent;
using CoreFS.CA06;
using System.IO;
using System.Diagnostics;
using Infragistics.Win.UltraWinGrid;


namespace YGJZJL.Car
{
    public partial class MeasureDataQuery : FrmBase
    {
        private string StrDateFieldSel="";
        GetBaseInfo Getbaseinfo=new GetBaseInfo();
        private string FS_WEIGHTNO;
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        private YGJZJL.CarSip.Client.App.CorePrinter _printer = new YGJZJL.CarSip.Client.App.CorePrinter();
        private YGJZJL.Car.CarWeightDataManage _carWeightDataManage = null;
       
        private string Username = CoreFS.SA06.CoreUserInfo.UserInfo.GetUserName();

      
        private int SelectIndexGird = -1;

        LimitQueryTime limitQueryTime = new LimitQueryTime();//为判断时间区间设定的变量
        DateTime beginTime;
        DateTime endTime;
        bool decisionResult;
        
       
        #region 构造函数
        public MeasureDataQuery()
        {
            InitializeComponent();            
            InitultraChartDatasource(this.ultraChart12);
            InitultraChartDatasource(this.ultraChart11);
        }
        #endregion

        #region 提示窗口
        private void DateCheck()
        {
            try
            {
                this.dateBegin.Value = DateTime.Today;
                this.dateEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void MeasureDataQuery_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            m_BaseInfo.ob = this.ob;
            m_BaseInfo.GetBFData(this.ultraComboEditor1, "QC");

            if (Constant.WaitingForm == null)
            {
                Constant.WaitingForm = new WaitingForm();
            }

            Constant.WaitingForm.ShowToUser = true;
            Constant.WaitingForm.Show();
            Constant.WaitingForm.Update();

            try
            {
                if (Constant.RunPath == "")
                {
                    Constant.RunPath = System.Environment.CurrentDirectory;
                }
            }
            catch (System.Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }

            try
            {
                this.dateBegin.Value = DateTime.Today;
                this.dateEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            //Constant.SetViewStyle(this);
            cbDateSel_ItemAdd();
            this.cbDateField.SelectedIndex = 1;
            StrDateFieldSel = cbDateField.SelectedValue.ToString();
            //ControlerInit();
            //QueryAndBindJLGrid();
            //BindingCboxDatasource();
            _carWeightDataManage = new CarWeightDataManage(this.ob);
            this.Cursor = Cursors.Default;
            Constant.WaitingForm.ShowToUser = false;
            Constant.WaitingForm.Close();
        }
        #endregion

        #region 附工具栏事件
        /// <summary>
        /// 工具栏设置
        /// </summary>
        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "Query":
                    {// ButtonTool
                        if (!Decision())//判断时间区间//杨滔添加
                        {
                            DateCheck();
                            return;
                        }
                        Query();

                        //QueryWeigh();            // Place code here
                        break;
                    }

                case "Export":
                    {
                        try
                        {
                            SaveFileDialog saveFileDialogExcel = new SaveFileDialog();
                            saveFileDialogExcel.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx|所有文件(*.*)|*.*";
                            saveFileDialogExcel.Title = "导出Excel文件";
                            saveFileDialogExcel.InitialDirectory = System.Environment.CurrentDirectory;
                            saveFileDialogExcel.ShowDialog();
                            if (saveFileDialogExcel.FileName != "")
                            {
                                this.ultraGridExcelExporter1.Export(this.ultraGrid1, saveFileDialogExcel.FileName);
                                MessageBox.Show("导出Excel成功！", "提示！");
                            }
                            //System.Diagnostics.Process.Start(saveFileDialogExcel.FileName);
                        }
                        catch (Exception ex)
                        { MessageBox.Show(ex.Message); }
                        break;
                    }
                case "RePrint":
                    {
                        RePrint();
                        break;
                    }
                default:
                    break;
              
            }
        }
        #endregion

        #region 调用服务端方法
        /// <summary>
        /// 查询汽车计量信息
        /// </summary>

        private bool Decision()//判断所选时间区间是否大于60天，如果大于则返回false//杨滔添加
        {
            beginTime = dateBegin.Value.Date;
            endTime = dateEnd.Value.Date;
            decisionResult = limitQueryTime.ParseTime(beginTime, endTime);
            if (!decisionResult)
            {
                return false;
            }
            else
            {
                return true;
            }
        
        }

   
        private void QueryWeigh()
        {
            string strBeginDate = dateBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string strEndDate = dateEnd.Value.ToString("yyyy-MM-dd 23:59:59");
            string strHth = string.Empty;
            string strGYDW = string.Empty;
            string strSHDW = string.Empty;
            string strCarNo = this.txtCarNo.Text.Trim().Replace("'", "''");

            string strDateField = StrDateFieldSel;
            
            if (dateBegin.Value > dateEnd.Value)
            {
                dateBegin.Value = dateEnd.Value;
                MessageBox.Show("开始时间不能大于结束时间！");
                return;
            }
            
                QueryWeighInformation(strBeginDate, strEndDate, strHth, strDateField, strGYDW, strSHDW,strCarNo);

           
        }
        /// <summary>
        /// 查询汽车计量信息服务端方法
        /// </summary>
        private void QueryWeighInformation(string strBeginDate, string strEndDate, string strHth, string strDateField, string strGYDW, string strSHDW, string strCarNo)
        {
            CoreClientParam cpp = new CoreClientParam();
            this.dataTable1.Rows.Clear();
            cpp.ServerName = "ygjzjl.car.MeasureDataQuery";
            cpp.MethodName = "QueryWeightList";
            cpp.ServerParams = new object[] { strBeginDate, strEndDate, strHth, strDateField, strGYDW, strSHDW, strCarNo };
            cpp.SourceDataTable = this.dataSet1.Tables[0];
            this.ExecuteQueryToDataTable(cpp, CoreInvokeType.Internal);

            try
            {
               
                foreach (UltraGridRow ugr in ultraGrid1.Rows)
                {
                    if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                    {
                        ugr.Appearance.ForeColor = Color.Red;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       
       private void  Query()
        {
            string strBeginTime = dateBegin.Value.ToString("yyyy-MM-dd 00:00:00");
            string strEndTIme = dateEnd.Value.ToString("yyyy-MM-dd 23:59:59");
            
              String Sql = "SELECT t.FS_CARDNUMBER, t.FS_CARNO,t.FS_MATERIALNAME,t.FS_STOVENO,t.FS_SENDERNAME,t.FS_TRANSNAME,t.FS_RECEIVERNAME,t.FN_SENDGROSSWEIGHT,t.FN_SENDTAREWEIGHT,t.FS_SENDERSTORE,t.FS_RECEIVERSTORE,t.FS_WEIGHTNO,t.FS_CHECKFLAG,";
            Sql += "t.FN_SENDNETWEIGHT,LX_BHTOMC(FS_WEIGHTTYPE)FS_WEIGHTTYPE,t.FN_GROSSWEIGHT,t.FS_GROSSPOINT,t.FS_GROSSPERSON,t.FS_GROSSSHIFT,t.FN_TAREWEIGHT,t.FS_CONTRACTNO,t.FS_REWEIGHTPERSON,t.FS_REWEIGHTPLACE,t.FS_REWEIGHTFLAG,";
            Sql += "t.FS_TAREPOINT,t.FS_TAREPERSON,t.FS_TARESHIFT,t.FS_SAMPLEPLACE,t.FS_UNLOADPERSON,t.FN_NETWEIGHT,t.FS_SAMPLEPERSON,t.FS_MATERIAL,t.FS_GY,t.FS_CY,t.FS_MEMO,t.FS_SAMPLEFLAG,t.FS_UNLOADFLAG,";
            Sql += "t.FS_SH,t.FS_TYPECODE,t.FS_WXPAYFLAG,t.FS_UNLOADPLACE,t.FS_CHECKPERSON,t.FS_CHECKPLACE,t.FS_YKL,t.FS_YKBL,t.FS_KHJZ,t.FS_DATASTATE,t.FS_BZ,t.FS_PROVIDER,t.FS_PROVIDERNAME,";
            Sql += "t.FN_FHJZ,t.FN_SJTH,t.FN_CETH,t.FN_YFDJ,t.FN_YFJE,t.FN_KSDJ,t.FN_KTH,t.FN_SFJE,";
            Sql += "to_char(t.FD_FHRQ,'yyyy-MM-dd hh24:mi:ss')as FD_FHRQ,";
            Sql += "to_char(t.FD_REWEIGHTTIME,'yyyy-MM-dd hh24:mi:ss')as FD_REWEIGHTTIME,";
            Sql += "to_char(t.FD_CHECKTIME,'yyyy-MM-dd hh24:mi:ss')as FD_CHECKTIME,";
            Sql += "to_char(t.FD_UNLOADTIME,'yyyy-MM-dd hh24:mi:ss')as FD_UNLOADTIME,";
            Sql += "to_char(t.FD_SAMPLETIME,'yyyy-MM-dd hh24:mi:ss')as FD_SAMPLETIME,";
            Sql += "to_char(t.FD_GROSSDATETIME,'yyyy-MM-dd hh24:mi:ss')as FD_GROSSDATETIME,";
            Sql += "to_char(t.FD_TAREDATETIME,'yyyy-MM-dd hh24:mi:ss')as FD_TAREDATETIME,";
            Sql += "to_char(t.FD_TOCENTERTIME,'yyyy-MM-dd hh24:mi:ss')as FD_TOCENTERTIME,";
            Sql += "to_char(t.FD_ACCOUNTDATE,'yyyy-MM-dd hh24:mi:ss')as FD_ACCOUNTDATE,";
            Sql += "to_char(t.FS_JZDATE,'yyyy-MM-dd hh24:mi:ss')as FS_JZDATE,";
            Sql += "to_char(FS_CREATEJSRQ,'yyyy-MM-dd hh24:mi:ss')as FS_CREATEJSRQ,";
            Sql += "to_char(t.FD_TESTIFYDATE,'yyyy-MM-dd hh24:mi:ss')as FD_TESTIFYDATE, ";
            Sql += "  t.FN_SENDGROSSWEIGHT,t.FN_SENDTAREWEIGHT,t.FN_SENDNETWEIGHT,t.FS_SETTLEMENTNAME ";
            Sql += " FROM DT_CARWEIGHTVIEW t ";
            Sql += "WHERE " + StrDateFieldSel + " >=TO_DATE('" + strBeginTime + "','yyyy-mm-dd hh24:mi:ss')";
            Sql += "AND " + StrDateFieldSel + "<=TO_DATE('" + strEndTIme + "','yyyy-mm-dd hh24:mi:ss') ";

            if (!string.IsNullOrEmpty(txtCarNo.Text.Trim()))
            {
                Sql += " and t.FS_CARNO LIKE '%" + txtCarNo.Text.Trim() + "%'";
            }
            if (!txtCarNo.Text.Trim().Contains("."))
            {
                Sql += " and t.FS_CARNO not like '%.%'";
            }

            this.dataTable1.Rows.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { Sql };
            ccp.SourceDataTable = dataSet1.Tables[0];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

          

            Constant.RefreshAndAutoSize(ultraGrid1);

            foreach (UltraGridRow ugr in ultraGrid1.Rows)
            {
                if (ugr == null || ugr.Cells == null || ugr.Cells.Count == 0)
                {
                    continue;
                }
                if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                {
                    ugr.Appearance.ForeColor = Color.Red;
                }

                string _GROSSWEIGHT = ugr.Cells["FN_GROSSWEIGHT"].Text.ToString();//毛重
                string _SENDGROSSWEIGHT = ugr.Cells["FN_SENDGROSSWEIGHT"].Text.ToString();//对方毛重
                double in_GROSSWEIGHT = 0;
                double.TryParse(_GROSSWEIGHT, out in_GROSSWEIGHT);
                double in_SENDGROSSWEIGHT = 0;
                double.TryParse(_SENDGROSSWEIGHT, out in_SENDGROSSWEIGHT);
                if (in_SENDGROSSWEIGHT != 0 && Math.Abs(in_GROSSWEIGHT - in_SENDGROSSWEIGHT) >= 0.3)
                {
                    ugr.Cells["FN_SENDGROSSWEIGHT"].Appearance.ForeColor = Color.Red;
                }

                string _TAREWEIGHT = ugr.Cells["FN_TAREWEIGHT"].Text.ToString();//皮重
                string _SENDTAREWEIGHT = ugr.Cells["FN_SENDTAREWEIGHT"].Text.ToString();//对方皮重
                double in_TAREWEIGHT = 0;
                double.TryParse(_TAREWEIGHT, out in_TAREWEIGHT);
                double in_SENDTAREWEIGHT = 0;
                double.TryParse(_SENDTAREWEIGHT, out in_SENDTAREWEIGHT);
                if (in_SENDTAREWEIGHT != 0 && Math.Abs(in_TAREWEIGHT - in_SENDTAREWEIGHT) >= 0.3)
                {
                    ugr.Cells["FN_SENDTAREWEIGHT"].Appearance.ForeColor = Color.Red;
                }

                string _NETWEIGHT = ugr.Cells["FN_NETWEIGHT"].Text.ToString();//净重
                string _SENDNETWEIGHT = ugr.Cells["FN_SENDNETWEIGHT"].Text.ToString();//对方净重
                double in_NETWEIGHT = 0;
                double.TryParse(_NETWEIGHT, out in_NETWEIGHT);
                double in_SENDNETWEIGHT = 0;
                double.TryParse(_SENDNETWEIGHT, out in_SENDNETWEIGHT);
                if (in_SENDNETWEIGHT != 0 && Math.Abs(in_NETWEIGHT - in_SENDNETWEIGHT) >= 0.3)
                {
                    ugr.Cells["FN_SENDNETWEIGHT"].Appearance.ForeColor = Color.Red;
                }
            }

        }
        /// <summary>
        /// 根据登陆的用户获取相关的发货单位、供应单位及收货单位信息。
        /// </summary>
        private void QueryPointToCb(string CbNo, string CbName,string CbUserNo ,string CbTable,ComboBox cb)
        {
            CoreClientParam cpp = new CoreClientParam();
            DataTable tabale = new DataTable();
            cpp.ServerName = "ygjzjl.car.MeasureDataQuery";
            cpp.MethodName = "QueryUserToCmboxInform";
            cpp.ServerParams = new object[] { CbNo, CbName, CbUserNo, CbTable, Username };
            cpp.SourceDataTable = tabale;
            this.ExecuteQueryToDataTable(cpp, CoreInvokeType.Internal);
            if (tabale.Rows.Count>0)
            {
                cb.DataSource = tabale;
                cb.DisplayMember = CbName;
                cb.ValueMember = CbNo;
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }
        #endregion

        /// <summary>
        /// 初始化曲线图表数据源
        /// </summary>
        private void InitultraChartDatasource(Infragistics.Win.UltraWinChart.UltraChart chart)
        {
            DataTable temp = new DataTable();
            DataRow curerow = temp.NewRow();
            temp.Rows.Add(curerow);
            DataColumn dc = new DataColumn("ZL", typeof(double));
            temp.Columns.Add(dc);
            temp.AcceptChanges();            
            chart.DataSource = temp;
            chart.DataBind();
        }
        /// <summary>
        /// 绑定用户对应的下拉列表框数据
        /// </summary>
        private void BindingCboxDatasource()
        {
            QueryPointToCb("FS_GY", "FS_SUPPLIERNAME", "FS_USERROLES", "IT_SUPPLIER", this.cbGYDWUSER);
            QueryPointToCb("FS_SH", "FS_MEMO", "FS_USERROLES", "IT_STORE", this.cbSHDWUSER);
        }


        #region 日期字段初始化
        /// <summary>
        /// 查询时间字段加载。
        /// </summary>
        private void cbDateSel_ItemAdd()
        {
            DataTable cbTable = new DataTable();
            DataColumn colum1 = new DataColumn();
            DataColumn colum2 = new DataColumn();
            colum1.ColumnName = "DataText";
            colum2.ColumnName = "DataValue";
            cbTable.Columns.AddRange(new DataColumn[] { colum1, colum2 });
            foreach (DataColumn colum in this.dataSet1.Tables[0].Columns)
            {
                if (colum.Caption.Contains("计量时间"))
                //if (colum.Caption.Contains("计量时间") || colum.Caption.Contains("日期"))
                {
                    DataRow row = cbTable.NewRow();
                    row["DataText"] = colum.Caption;
                    row["DataValue"] = colum.ColumnName;
                    cbTable.Rows.Add(row);
                    cbTable.AcceptChanges();
                }
            }
            if (cbTable.Rows.Count > 0)
            {
                this.cbDateField.DataSource = cbTable;
                this.cbDateField.DisplayMember = "DataText";
                this.cbDateField.ValueMember = "DataValue";
            }
        }
        /// <summary>
        /// 查询时间字段设置。
        /// </summary>
        private void cbDateField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDateField.SelectedValue != null)
                StrDateFieldSel = this.cbDateField.SelectedValue.ToString();

        }
        #endregion

        
        /// <summary>
        /// 数据表格选择行触发事件
        /// </summary>
        private void ultraGrid1_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (ultraGrid1.Rows.Count > 0 && e.Row.Index != -1)
            {
                FS_WEIGHTNO = this.ultraGrid1.Rows[e.Row.Index].Cells["FS_WEIGHTNO"].Value.ToString();
                SelectIndexGird = e.Row.Index;
                if (FS_WEIGHTNO != "")
                {
                    GetWeightImage(FS_WEIGHTNO);
                }
                else
                {
                    MessageBox.Show("计量数据不正确！");
                }
                
            }
            if (this.dataSet1.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i <= 10; i++)
                {

                    if (this.dataSet1.Tables[1].Rows[0][i] != DBNull.Value&&i!=5)
                    {
                        byte[] PicStreamByte = (byte[])this.dataSet1.Tables[1].Rows[0][i];
                        switch (i + 1)
                        {
                            case 1:
                                this.ulpic1.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 2:
                                this.ulpic2.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 3:
                                this.ulpic3.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 4:
                                this.ulpic4.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 5:
                                this.ulpic5.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 7:
                                this.ulpic7.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 8:
                                this.ulpic8.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 9:
                                this.ulpic9.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 10:
                                this.ulpic10.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 11:
                                this.ulpic11.Image = BytesToBitmap(PicStreamByte);
                                break;


                        }
                    }
                }
                DataTable ultrachart11=CureToTable(this.dataSet1.Tables[1].Rows[0][5].ToString().Trim());
                DataTable ultrachart12=CureToTable(this.dataSet1.Tables[1].Rows[0][11].ToString().Trim());
                if (ultrachart11.Rows.Count > 0)
                {
                    if (ultrachart11.Columns.Count != 0)
                    this.ultraChart11.DataSource = ultrachart11;
                }
                else
                {
                    InitultraChartDatasource(this.ultraChart11);
                }
                if (ultrachart12.Rows.Count > 0)
                {
                    if (ultrachart12.Columns.Count!=0)
                    this.ultraChart12.DataSource = ultrachart12;
                }
                else
                {
                    InitultraChartDatasource(this.ultraChart12);
                }
                
            }
            else
            {                
                this.ulpic1.Image = null;
                this.ulpic2.Image = null;
                this.ulpic3.Image = null;
                this.ulpic4.Image = null;
                this.ulpic5.Image = null;
                this.ulpic11.Image = null;
                this.ulpic7.Image = null;
                this.ulpic8.Image = null;
                this.ulpic9.Image = null;
                this.ulpic10.Image = null;
                InitultraChartDatasource(this.ultraChart12);
                InitultraChartDatasource(this.ultraChart11);
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 曲线字符串转数据表
        /// </summary>
        private DataTable CureToTable(string CureStr)
        {
            DataTable cure = new DataTable();
            DataRow curerow = cure.NewRow();
            cure.Rows.Add(curerow);
            cure.AcceptChanges();
            int i = 0;
            string de="";
            while(CureStr.Length>0)
            {
                DataColumn dc = new DataColumn("ZL" + i, typeof(double));
                cure.Columns.Add(dc);
                int strinof=CureStr.IndexOf(",",0);
                if (strinof != -1)
                {
                    de = CureStr.Substring(0, strinof);
                }
                else
                {
                    de = CureStr;
                    cure.Rows[0][i] = Convert.ToDouble(de);
                    CureStr = CureStr.Remove(0, CureStr.Length);
                }
                try
                {
                    cure.Rows[0][i] = Convert.ToDouble(de);
                    CureStr = CureStr.Remove(0, CureStr.IndexOf(",", 0)+1);
                }
                catch(Exception ex)
                {

                }
                cure.AcceptChanges();
                i = i + 1;
                
            }
            return cure;
        }
        /// <summary>
        /// 二进制转换成图片
        /// </summary>
        public static Bitmap BytesToBitmap(byte[] Bytes)
        {

            if (Bytes.Length == 0||Bytes.Length==1)
            {
                return null;
            }
            try
            {
                MemoryStream ImageMem = new MemoryStream(Bytes, true);
                ImageMem.Write(Bytes, 0, Bytes.Length);
                Bitmap _Image = new Bitmap(ImageMem);
                return _Image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            
        }
        
        /// <summary>
        /// 通过计量点名称得到计量点编码
        /// </summary>
        private string GetPointNo(string PointName)
        {
            string pointNo = "";
            CoreClientParam cpp = new CoreClientParam();
            cpp.ServerName = "ygjzjl.car.MeasureDataQuery";
            cpp.MethodName = "QueryWeightPointNo";
            cpp.ServerParams = new object[] {PointName};
            cpp.SourceDataTable = this.dataSet1.Tables[2];
            this.ExecuteQueryToDataTable(cpp, CoreInvokeType.Internal);
            if (this.dataSet1.Tables[2].Rows.Count>0)
            {
               pointNo =this.dataSet1.Tables[2].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
            return pointNo;
        }
         
        /// <summary>
        /// 双击计量数据表格行查询出相关计量图片
        /// </summary>
        private void GetWeightImage(string WeightNo)
        {
            CoreClientParam cpp = new CoreClientParam();
            cpp.ServerName = "ygjzjl.car.MeasureDataQuery";
            cpp.MethodName = "QueryWeightImage";
            cpp.ServerParams = new object[] { WeightNo };
            cpp.SourceDataTable = this.dataSet1.Tables[1];
            this.dataSet1.Tables[1].Clear();
            this.ExecuteQueryToDataTable(cpp, CoreInvokeType.Internal);
            
        }

        #region 输入框条件判断
      
        private void dateBegin_ValueChanged(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void cbx_Filter_CheckedChanged(object sender, EventArgs e)
        {
            //CommonMethod.SetUltraGridRowFilter(ref ultraGrid1, cbx_Filter.Checked);
        }

        private void RePrint()
        {
            if (ultraGrid1.ActiveRow != null && ultraGrid1.ActiveRow.Index >= 0)
            {
                if (ultraComboEditor1.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择打印的计量点！");
                    return;
                }
                else
                {
                    try
                    {
                        string pointCode = ultraComboEditor1.SelectedValue.ToString();
                        Infragistics.Win.UltraWinGrid.UltraGridRow uRow = ultraGrid1.ActiveRow;
                        YGJZJL.CarSip.Client.App.HgLable hgLable = _carWeightDataManage.GetPrintData(uRow.Cells["FS_WEIGHTNO"].Value.ToString());
                        if (hgLable != null)
                        {
                            string grossTime = uRow.Cells["FD_GROSSDATETIME"].Value.ToString();
                            string tareTime = uRow.Cells["FD_TAREDATETIME"].Value.ToString();

                            hgLable.WeightPoint = (string.Compare(grossTime, tareTime) > 0) ? uRow.Cells["FS_GROSSPOINT"].Value.ToString() : uRow.Cells["FS_TAREPOINT"].Value.ToString();
                            //hgLable.BarCode = uRow.Cells["FS_WEIGHTNO"].Value.ToString();
                            hgLable.MeasTech = (string.Compare(grossTime, tareTime) > 0) ? uRow.Cells["FS_GROSSPERSON"].Value.ToString() : uRow.Cells["FS_TAREPERSON"].Value.ToString();
                            hgLable.Type = YGJZJL.CarSip.Client.App.LableType.CAR;
                            _printer.Data = hgLable;
                            _printer.PrinterName = pointCode;
                            _printer.Print();

                            //纸张-1
                            _carWeightDataManage.ReducePrinterPaper(pointCode);
                            //int count = 0;
                            //int.TryParse(txtZZ.Text.Trim(), out count);
                            //txtZZ.Text = (--count).ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("未配置{0}的打印机", ultraComboEditor1.Text));
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择需要打印的计量记录！");
            }
        }
    }
}
