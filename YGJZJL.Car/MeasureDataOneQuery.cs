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
    public partial class MeasureDataOneQuery : FrmBase
    {
        string strZYBH = "";
        GetBaseInfo m_BaseInfo = new GetBaseInfo();
        private YGJZJL.CarSip.Client.App.CorePrinter _printer = new YGJZJL.CarSip.Client.App.CorePrinter();
        private YGJZJL.Car.CarWeightDataManage _carWeightDataManage = null;

        public MeasureDataOneQuery()
        {
            InitializeComponent();
            //InitultraChartDatasource(this.ultraChart12);
            InitultraChartDatasource(this.ultraChart11);
        }

        private void MeasureDataOneQuery_Load(object sender, EventArgs e)
        {
            m_BaseInfo.ob = this.ob;
            m_BaseInfo.GetBFData(this.ultraComboEditor1, "QC");
            DataGridInit();
            Query();
            _carWeightDataManage = new CarWeightDataManage(this.ob);
            //QueryYCBData();
            try
            {
                this.BeginTime.Value = DateTime.Today;
                this.EndTime.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 网格显示设置
        /// </summary>
        private void DataGridInit()
        {
            //行编辑器显示序号
            ultraGrid3.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.VisibleIndex;
            ultraGrid3.DisplayLayout.Override.RowSelectorWidth = 25;
            ultraGrid3.DisplayLayout.Override.RowSelectorAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            for (int i = 0; i <= ultraGrid3.DisplayLayout.Bands[0].Columns.Count - 1; i++)
            {
                ultraGrid3.DisplayLayout.Bands[0].Columns[i].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                ultraGrid3.DisplayLayout.Bands[0].Columns[i].FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            }
        }
        /// <summary>
        /// 查询一次计量绑定数据
        /// </summary>
        private void QueryYCBData()
        {
            string strWhere = " and FD_WEIGHTTIME >= to_date('" + BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
            strWhere += " and FS_FALG <> '1'";
            strWhere += " and FD_WEIGHTTIME <= to_date('" + EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            if (this.txtCarNo.Text.Trim().Length > 0)
            {
                strWhere += " and fs_carno like '%" + txtCarNo.Text.Trim().Replace("'", "''") + "%'"; 
            }
           
            dataTable1.Rows.Clear();
            dataTable11.Rows.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.car.WeighMeasureInfo";
            ccp.MethodName = "QueryYCJLData";
            ccp.ServerParams = new object[] { strWhere };
            ccp.SourceDataTable = dataTable11;
  
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            
            Constant.RefreshAndAutoSize(ultraGrid3);

            foreach (UltraGridRow ugr in ultraGrid3.Rows)
            {
                if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
                {
                    ugr.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void Query()
        {
            string strBeginTime = BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss')";
            string strEndTIme = EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";
            string strCarNo = this.txtCarNo.Text.Trim();
            string strWhere = " and FD_WEIGHTTIME >= to_date('" + BeginTime.Value.ToString("yyyy-MM-dd 00:00:00") + "','yyyy-mm-dd hh24:mi:ss') ";
            strWhere += " and FS_FALG <> '1'";
            strWhere += " and FD_WEIGHTTIME <= to_date('" + EndTime.Value.ToString("yyyy-MM-dd 23:59:59") + "','yyyy-mm-dd hh24:mi:ss')";

            String sql = "select A.FS_WEIGHTNO,A.FS_PLANCODE,A.FS_CONTRACTNO,A.FS_CONTRACTITEM,A.FS_STOVENO,Provider_BHTOMC(A.FS_Provider)FS_Provider,A.FS_BZ,"
                + "A.FN_COUNT,A.FS_CARDNUMBER,A.FS_CARNO,A.FS_MATERIAL,A.FS_MATERIALNAME,A.FS_Sender,FHDW_BHTOMC(A.FS_Sender)FS_FHDW,"
                + "A.FS_SENDERSTORE,A.FS_TRANSNO,CYDW_BHTOMC(A.FS_TRANSNO)FS_CYDW,A.FS_RECEIVER,SHDW_BHTOMC(A.FS_RECEIVER)FS_SHDW,"
                + "A.FS_RECEIVERSTORE,A.FS_WEIGHTTYPE,LX_BHTOMC(A.FS_WEIGHTTYPE)FS_LX,A.FS_POUNDTYPE,A.FN_SENDGROSSWEIGHT,"
                + "A.FN_SENDTAREWEIGHT,A.FN_SENDNETWEIGHT,A.FN_WEIGHT,A.FS_POUND,A.FS_WEIGHTER,"
                + "to_char(A.FD_WEIGHTTIME,'yyyy-MM-dd HH24:mi:ss')as FD_WEIGHTTIME,A.FS_SHIFT,A.FS_TERM,FS_FIRSTLABELID,";
            sql += "to_char(FD_UNLOADINSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_UNLOADINSTORETIME,"
                    + "to_char(FD_UNLOADOUTSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_UNLOADOUTSTORETIME,"
                    + "FS_UNLOADFLAG,FS_UNLOADSTOREPERSON,"
                    + "to_char(FD_LOADINSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_LOADINSTORETIME,"
                    + "to_char(FD_LOADOUTSTORETIME,'yyyy-MM-dd HH24:mi:ss')as FD_LOADOUTSTORETIME,"
                    + "FS_LOADFLAG,FS_LOADSTOREPERSON,FS_SAMPLEPERSON,FS_YCSFYC,"
                    + "to_char(FD_SAMPLETIME,'yyyy-MM-dd HH24:mi:ss')as FD_SAMPLETIME,FS_SAMPLEPLACE,"
                    + "FS_SAMPLEFLAG,FS_UNLOADPERSON,to_char(FD_UNLOADTIME,'yyyy-MM-dd HH24:mi:ss')as FD_UNLOADTIME,"
                    + "FS_UNLOADPLACE,FS_CHECKPERSON,to_char(FD_CHECKTIME,'yyyy-MM-dd HH24:mi:ss')as FD_CHECKTIME,"
                    + "FS_CHECKPLACE,FS_CHECKFLAG,FS_IFSAMPLING,FS_IFACCEPT,FS_DRIVERNAME,FS_DRIVERIDCARD,FS_YKL,"
                    + "FS_REWEIGHTFLAG,to_char(FD_REWEIGHTTIME,'yyyy-MM-dd HH24:mi:ss')as FD_REWEIGHTTIME,"
                    + "FS_REWEIGHTPLACE,FS_REWEIGHTPERSON,FS_BILLNUMBER,FS_DFJZ,FS_YKBL,FS_MEMO,"
                    + " A.FS_REWEIGHTFLAG,to_char(A.FD_REWEIGHTTIME, 'yyyy-MM-dd HH24:mi:ss') as FD_REWEIGHTTIME,A.FS_REWEIGHTPLACE,A.FS_REWEIGHTPERSON"
                    + " from DT_FIRSTCARWEIGHT A where 1=1 "
                    + strWhere;

            if (!string.IsNullOrEmpty(txtCarNo.Text.Trim()))
            {
                sql += " and  A.FS_CARNO LIKE '%" + txtCarNo.Text.Trim() + "%'";
            }
            if (!txtCarNo.Text.Trim().Contains("."))
            {
                sql += " and A.FS_CARNO not like '%.%'";
            }
            sql+=" order by FD_WEIGHTTIME DESC";

            this.dataSet1.Tables[1].Rows.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "ygjzjl.basedatamanage.OtherBaseInfo";
            ccp.MethodName = "ExcuteQuery";
            ccp.ServerParams = new object[] { sql };
            ccp.SourceDataTable = dataSet1.Tables[1];

            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);

            //Constant.RefreshAndAutoSize(ultraGrid3);

            //foreach (UltraGridRow ugr in ultraGrid3.Rows)
            //{
            //    if (ugr.Cells["FS_REWEIGHTFLAG"].Text.ToString() == "2")
            //    {
            //        ugr.Appearance.ForeColor = Color.Red;
            //    }
            //}
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key.ToString())
            {
                case "Query":
                    {
                        if (BeginTime.Value > EndTime.Value)
                        {
                            MessageBox.Show("开始时间大于结束时间!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        Query();
                        //QueryYCBData();
                        break;
                    }
                case "Update":
                    {
                        //if (ultraGrid3.ActiveRow == null || ultraGrid3.ActiveRow.Selected == false)
                        //{
                        //    MessageBox.Show("请双击一条需要修改的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    return;
                        //}
                        UpdateOldData();
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

        private void RePrint()
        {
            if (ultraGrid3.ActiveRow != null && ultraGrid3.ActiveRow.Index >= 0)
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
                        Infragistics.Win.UltraWinGrid.UltraGridRow uRow = ultraGrid3.ActiveRow;
                        YGJZJL.CarSip.Client.App.HgLable hgLable = _carWeightDataManage.GetPrintData(uRow.Cells["FS_WEIGHTNO"].Value.ToString());
                        if (hgLable != null)
                        {
                            hgLable.WeightPoint = uRow.Cells["FS_POUND"].Value.ToString();
                            //hgLable.BarCode = uRow.Cells["FS_WEIGHTNO"].Value.ToString();
                            hgLable.MeasTech = uRow.Cells["Fs_Weighter"].Value.ToString();
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

        private void UpdateOldData()
        {
            //string strSql = "UPDATE DT_FIRSTCARWEIGHT SET FS_CARNO = '" + cbCarNo.Text.Trim() + "' WHERE FS_WEIGHTNO = '" + strZYBH + "'";
            //CoreClientParam ccp = new CoreClientParam();
            //ccp.ServerName = "Core.KgMcms.BaseDataManage.OtherBaseInfo";
            //ccp.MethodName = "ExcuteNonQuery";
            //ccp.ServerParams = new object[] { strSql };
            //ccp = this.ExecuteNonQuery(ccp, CoreInvokeType.Internal);
            //QueryYCBData();
        }

        private void ultraGrid3_DoubleClick(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (ultraGrid3.Rows.Count > 0 && e.Row.Index != -1)
            {
                strZYBH = this.ultraGrid3.Rows[e.Row.Index].Cells["FS_WEIGHTNO"].Value.ToString();
                //SelectIndexGird = e.Row.Index;
                if (strZYBH != "")
                {
                    GetWeightImage(strZYBH);
                }
                else
                {
                    MessageBox.Show("计量数据不正确！");
                }

            }
            if (this.dataTable1.Rows.Count > 0)
            {
                for (int i = 0; i < 9; i++)
                {

                    if (this.dataTable1.Rows[0][i] != DBNull.Value && i != 8)
                    {
                        byte[] PicStreamByte = (byte[])this.dataTable1.Rows[0][i];
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
                            case 6:
                                this.ulpic6.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 7:
                                this.ulpic7.Image = BytesToBitmap(PicStreamByte);
                                break;
                            case 8:
                                this.ulpic8.Image = BytesToBitmap(PicStreamByte);
                                break;
                        

                        }
                    }
                }
                DataTable ultrachart11 = CureToTable(this.dataTable1.Rows[0][8].ToString().Trim());
                
                if (ultrachart11.Rows.Count > 0)
                {
                    if (ultrachart11.Columns.Count != 0)
                        this.ultraChart11.DataSource = ultrachart11;
                }
                else
                {
                    InitultraChartDatasource(this.ultraChart11);
                }
               

            }
            else
            {
                this.ulpic1.Image = null;
                this.ulpic2.Image = null;
                this.ulpic3.Image = null;
                this.ulpic4.Image = null;
                this.ulpic5.Image = null;
             
                this.ulpic7.Image = null;
                this.ulpic8.Image = null;
                this.ulpic6.Image = null;
              
                InitultraChartDatasource(this.ultraChart11);
            }
            this.Cursor = Cursors.Default;
        }


        /// <summary>
        /// 双击计量数据表格行查询出相关计量图片
        /// </summary>
        private void GetWeightImage(string WeightNo)
        {
            this.dataTable1.Rows.Clear();
            String Sql = "SELECT FB_IMAGE1,FB_IMAGE2,FB_IMAGE3,FB_IMAGE4,FB_IMAGE5,FB_IMAGE6,";
            Sql += "FB_IMAGE7,FB_IMAGE8,FS_CURVEIMAGEONE FROM DT_IMAGES";
            Sql += " WHERE FS_WEIGHTNO ='" + WeightNo + "'";
            CoreClientParam cpp = new CoreClientParam();
            cpp.ServerName = "ygjzjl.car.CarCard";
            cpp.MethodName = "queryByClientSql";
            cpp.ServerParams = new object[] { Sql };
            cpp.SourceDataTable = this.dataTable1;
            
            this.ExecuteQueryToDataTable(cpp, CoreInvokeType.Internal);

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
            string de = "";
            while (CureStr.Length > 0)
            {
                DataColumn dc = new DataColumn("ZL" + i, typeof(double));
                cure.Columns.Add(dc);
                int strinof = CureStr.IndexOf(",", 0);
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
                    CureStr = CureStr.Remove(0, CureStr.IndexOf(",", 0) + 1);
                }
                catch (Exception ex)
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

            if (Bytes.Length == 0 || Bytes.Length == 1)
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

    }
}
