using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CoreFS.CA06;
using PrintSolution;

namespace YGJZJL.Pipe
{
    public partial class StorageQuery : FrmBase
    {
        public StorageQuery()
        {
            InitializeComponent();
        }

        private void ultraToolbarsManager1_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            switch (e.Tool.Key)
            {
                case "查询":
                    Query();
                    break;
                case "导出":
                    if (this.saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                    {
                        Export(saveFileDialog1.FileName,true);
                    }
                    break;
                case "打印":
                    if (cbPrint.SelectedIndex < 0)
                    {
                        MessageBox.Show("请选择打印机！");
                        return;
                    }
                    string path = System.Environment.CurrentDirectory + "\\temp\\tmp.xls";
                    //Export(path, false);
                    if (Export(path, false))
                        Print(path);
                    break;
            }
        }

        private void Query()
        {
            string rkdh = tbStorage.Text.Trim();

            ArrayList param = new ArrayList();
            param.Add(rkdh);
            string sql = @"SELECT D.FS_RKDH,M.FS_BATCHNO,M.FS_MATERIALNAME,M.FS_STEELTYPE,M.FS_SPEC FS_SIZE
                            , FS_STANDARD, decode(D.FS_TERM,'0','常白班','1','甲','2','乙','3','丙','4','丁') as FS_SHIFT
                            ,TO_CHAR(D.FD_DATETIME,'YYYY')||'年'||TO_CHAR(D.FD_DATETIME,'MM')||'月'||TO_CHAR(D.FD_DATETIME,'DD')||'日' FS_DATE 
                            FROM DT_PIPEWEIGHTMAIN M
                            ,(SELECT A.* FROM (SELECT T.* FROM DT_PIPEWEIGHTDETAIL T WHERE T.FS_RKDH = ? ORDER BY T.FD_DATETIME) A WHERE ROWNUM = 1) D
                            WHERE M.FS_BATCHNO = D.FS_BATCHNO";
            dataTable2.Clear();
            CoreClientParam ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "queryBySql";
            ccp.ServerParams = new object[] { sql, param };
            ccp.SourceDataTable = dataTable2;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);
            if (dataTable2.Rows.Count > 0)
            {
                DataRow dr = dataTable2.Rows[0];
                lblMaterialName.Text = dr["FS_MATERIALNAME"].ToString();
                lblShift.Text = dr["FS_SHIFT"].ToString();
                lblSteelType.Text = dr["FS_STEELTYPE"].ToString();
                textBox1.Text = dr["FS_SIZE"].ToString();
                lblStandard.Text = dr["FS_STANDARD"].ToString();
                lblRKDH.Text = dr["FS_RKDH"].ToString();
                lblDate.Text = dr["FS_DATE"].ToString();
            }

            //查询表格数据
            sql = @"SELECT ROWNUM SEQ,A.* FROM (SELECT T.FS_BATCHNO,TO_CHAR(T.FN_BANDNO,'00') FN_BANDNO,T.FN_LENGTH
                            ,decode(T.FN_THEORYWEIGHT,0,'',to_char(T.FN_THEORYWEIGHT,'FM990.099')) FN_THEORYWEIGHT,T.FN_WEIGHT,'合格' FS_JUDGE,'' FS_YARD,T.FS_REMARK
                            ,T.FS_RKDH,TO_CHAR(T.FD_DATETIME,'YYYY-MM-DD HH24:MI:SS') FS_DATE
                             FROM DT_PIPEWEIGHTDETAIL T
                            WHERE T.FS_RKDH = ?
                            ORDER BY T.FS_BATCHNO,T.FN_BANDNO) A";
            dataTable1.Clear();
            ccp = new CoreClientParam();
            ccp.ServerName = "com.dbComm.DBComm";
            ccp.MethodName = "queryBySql";
            ccp.ServerParams = new object[] { sql, param};
            ccp.SourceDataTable = dataTable1;
            this.ExecuteQueryToDataTable(ccp, CoreInvokeType.Internal);


        }

        private bool Export(string filepath,bool isOpen)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel._Workbook objBook;
                ultraGridExcelExporter1.Export(ultraGrid1, filepath);

                Microsoft.Office.Interop.Excel.Workbooks objBooks;//接口 workbooks
                Microsoft.Office.Interop.Excel.Sheets objSheets;// 接口 sheets
                Microsoft.Office.Interop.Excel.Worksheet objSheet;//接口 worksheet
                Microsoft.Office.Interop.Excel.Range range = null;
                excel = new Microsoft.Office.Interop.Excel.Application();

                objBooks = excel.Workbooks;
                //Object miss = System.Reflection.Missing.Value;
                objBook = objBooks.Add(filepath);
                objSheets = objBook.Sheets;
                objSheet = (Microsoft.Office.Interop.Excel.Worksheet)objSheets[1];
                excel.Visible = false; //让后台执行设置为不可见，为true的话会看到打开一个Excel，然后数据在往里写
                //自动换行，
                objSheet.Cells.WrapText = true;

                Microsoft.Office.Interop.Excel.Range tmpRange = (Microsoft.Office.Interop.Excel.Range)objSheet.Rows[1, System.Reflection.Missing.Value];
                tmpRange.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, System.Reflection.Missing.Value);
                objSheet.get_Range("A1", "I1").Merge(objSheet.get_Range("A1", "I1").MergeCells);
                objSheet.get_Range("A1", "I1").VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                objSheet.get_Range("A1", "I1").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                objSheet.get_Range("A1", "I1").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).Font.Size = 18;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).RowHeight = 30;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).Font.Bold = true;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).Interior.Color = ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[3, 1]).Interior.Color;
                
                excel.Cells[1, 1] = "云南昆钢制管有限公司产成品入库单";

                Microsoft.Office.Interop.Excel.Range tmpRange2 = (Microsoft.Office.Interop.Excel.Range)objSheet.Rows[2, System.Reflection.Missing.Value];
                tmpRange2.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, System.Reflection.Missing.Value);
                objSheet.get_Range("A2", "I2").Merge(objSheet.get_Range("A2", "I2").MergeCells);
                objSheet.get_Range("A2", "I2").VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                objSheet.get_Range("A2", "I2").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                objSheet.get_Range("A2", "I2").Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[2, 1]).Font.Size = 9;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[2, 1]).RowHeight = 15;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[2, 1]).Font.Bold = false;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[2, 1]).Interior.Color = ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).Interior.Color;
                
                excel.Cells[2, 1] = "生产班次:  " + lblShift.Text+"                                                                                                                                             "+tbType.Text;

                Microsoft.Office.Interop.Excel.Range tmpRange3 = (Microsoft.Office.Interop.Excel.Range)objSheet.Rows[3, System.Reflection.Missing.Value];
                tmpRange3.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, System.Reflection.Missing.Value);
                objSheet.get_Range("A3", "I3").Merge(objSheet.get_Range("A3", "I3").MergeCells);
                objSheet.get_Range("A3", "I3").VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                objSheet.get_Range("A3", "I3").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[3, 1]).Font.Size = 9;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[3, 1]).RowHeight = 15;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[3, 1]).Font.Bold = false;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[3, 1]).Interior.Color = ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).Interior.Color;

                excel.Cells[3, 1] = "产品名称:" + lblMaterialName.Text + "     牌号:" + lblSteelType.Text 
                                   + "     规格(mm):" + textBox1.Text + "     执行标准:" + lblStandard.Text 
                                   + "     入库单号:" + lblRKDH.Text;

                int sumIndex = objSheet.UsedRange.Cells.Rows.Count+1;
                Microsoft.Office.Interop.Excel.Range tmpRangeSum = (Microsoft.Office.Interop.Excel.Range)objSheet.Rows[sumIndex, System.Reflection.Missing.Value];
                tmpRangeSum.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown, System.Reflection.Missing.Value);
                objSheet.get_Range("A" + sumIndex.ToString(), "D" + sumIndex.ToString()).Merge(objSheet.get_Range("A" + sumIndex.ToString(), "D" + sumIndex.ToString()).MergeCells);
                objSheet.get_Range("G" + sumIndex.ToString(), "I" + sumIndex.ToString()).Merge(objSheet.get_Range("G" + sumIndex.ToString(), "I" + sumIndex.ToString()).MergeCells);
                objSheet.get_Range("A" + sumIndex.ToString(), "D" + sumIndex.ToString()).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                objSheet.get_Range("A" + sumIndex.ToString(), "D" + sumIndex.ToString()).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                objSheet.get_Range("A" + sumIndex.ToString(), "D" + sumIndex.ToString()).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                objSheet.get_Range("E" + sumIndex.ToString(), "E" + sumIndex.ToString()).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                objSheet.get_Range("F" + sumIndex.ToString(), "F" + sumIndex.ToString()).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                objSheet.get_Range("G" + sumIndex.ToString(), "I" + sumIndex.ToString()).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 1]).Font.Size = 9;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 1]).RowHeight = 15;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 1]).Font.Bold = false;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 1]).Interior.Color = ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).Interior.Color;

                excel.Cells[sumIndex, 1] = "合   计";
                double sumWeight = 0;
                double sumTheoryWeight = 0;
                foreach(DataRow dr in dataTable1.Rows)
                {
                    if (dr["FN_WEIGHT"].ToString()!="")
                    {
                        sumWeight += double.Parse(dr["FN_WEIGHT"].ToString());
                    }
                    
                    if (dr["FN_THEORYWEIGHT"].ToString() != "")
                    {
                        sumTheoryWeight += double.Parse(dr["FN_THEORYWEIGHT"].ToString());
                    }
                }
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 6]).Font.Size = 9;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 6]).RowHeight = 15;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 6]).Font.Bold = false;
                ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[sumIndex, 6]).Interior.Color = ((Microsoft.Office.Interop.Excel.Range)objSheet.UsedRange.Cells[1, 1]).Interior.Color;
                excel.Cells[sumIndex, 5] = sumTheoryWeight.ToString();
                excel.Cells[sumIndex, 6] = sumWeight.ToString();
               

                //页眉页脚
                objSheet.PageSetup.RightHeader = "&P/&N页 ";
                
                objSheet.PageSetup.LeftFooter = "入库员:                                 "
                    + "质检员:                                 "
                    + "验收员:                                 "
                    + "日期: " + lblDate.Text;
                //objSheet.PageSetup.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperB4;
                objBook.SaveCopyAs(filepath);
                //设置禁止弹出保存和覆盖的询问提示框   
                excel.DisplayAlerts = false;
                excel.AlertBeforeOverwriting = false;

                //确保Excel进程关闭   
                objBooks.Close();
                excel.Workbooks.Close();
                excel.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(objBooks);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                GC.Collect();
                System.GC.WaitForPendingFinalizers();
                //MessageBox.Show("数据导出完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (System.IO.File.Exists(filepath) && isOpen)
                    System.Diagnostics.Process.Start(filepath); //保存成功后打开此文件

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private void Print(string filePath)
        {
            XLSPrinter xlsPrinter = new XLSPrinter(filePath);
            
            xlsPrinter.PageSetup.LeftMargin = 25;
            xlsPrinter.PageSetup.TopMargin = 5;
            //xlsPrinter.PageSetup.RightMargin = 40;
            //xlsPrinter.PageSetup.BottomMargin = 100;
            xlsPrinter.PrinterName = cbPrint.SelectedItem.ToString();
            xlsPrinter.printExcel();
        }

        private void StorageQuery_Load(object sender, EventArgs e)
        {
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                cbPrint.Items.Add(printerName);
            }
        }
    }
}
