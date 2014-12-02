using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Sip.Client.Printer
{
    public class PrinterBase
    {
        protected object missing = System.Reflection.Missing.Value;
        protected const string DATE_REPLACE_STR = "?";
        protected PageSetup _PageSetup = new PageSetup();//纸张设置

        internal PageSetup PageSetup
        {
            get { return _PageSetup; }
            set { _PageSetup = value; }
        }

        protected bool printExcel(string excelPath, string printerName)
        {
            bool flag = true;
            try
            {
                if (CheckExcelInstalled.isExcelInstalled())
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = excel.Application.Workbooks.Add(excelPath);
                    //excel.Visible = true;
                    Microsoft.Office.Interop.Excel._Worksheet ws = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Worksheets["Sheet1"];
                    try
                    {
                        paintPicByExcel(ws);
                        //打印方向
                        if (_PageSetup.Orientation == 1)
                        {
                            ws.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                        }

                        //边距
                        if (_PageSetup.TopMargin != 0)
                        ws.PageSetup.TopMargin = _PageSetup.TopMargin;
                        if (_PageSetup.BottomMargin != 0)
                        ws.PageSetup.BottomMargin = _PageSetup.BottomMargin;
                        if (_PageSetup.LeftMargin != 0)
                        ws.PageSetup.LeftMargin = _PageSetup.LeftMargin;
                        if (_PageSetup.RightMargin != 0)
                        ws.PageSetup.RightMargin = _PageSetup.RightMargin;
                        ws.PrintOut(1, 2, 1, false, printerName, false, false, missing);
                        workbook.Saved = true;
                    }
                    catch (Exception e)
                    {
                        flag = false;
                    }
                    finally
                    {
                        workbook.Close(missing, missing, missing);
                        excel.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                    }

                }
                else
                {
                    ET.Application et = new ET.Application();
                    ET._Workbook ewb = et.Workbooks.Add(excelPath);
                    //et.Visible = true;
                    ET._Worksheet ews = (ET._Worksheet)ewb.Worksheets["Sheet1"];
                    try
                    {
                        paintPicByEt(ews);
                        if (_PageSetup.Orientation == 1)
                        {
                            ews.PageSetup.Orientation = ET.XlPageOrientation.xlLandscape;
                        }

                        //边距
                        if (_PageSetup.TopMargin != 0)
                            ews.PageSetup.TopMargin = _PageSetup.TopMargin;
                        if (_PageSetup.BottomMargin != 0)
                            ews.PageSetup.BottomMargin = _PageSetup.BottomMargin;
                        if (_PageSetup.LeftMargin != 0)
                            ews.PageSetup.LeftMargin = _PageSetup.LeftMargin;
                        if (_PageSetup.RightMargin != 0)
                            ews.PageSetup.RightMargin = _PageSetup.RightMargin;
                        ews.PrintOut(1, 1, 1, false, printerName, false, false, missing, false, 1, 1, 0, 0, false, ET.ETPaperTray.etPrinterDefaultBin, false, ET.ETPaperOrder.etPrinterRepeat);
                        ewb.Saved = true;
                    }
                    catch (Exception e)
                    {
                        flag = false;
                    }
                    finally
                    {
                        ewb.Close(missing, missing, missing);
                        et.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ews);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(ewb);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(et);
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                flag = false;
            }

            return flag;
        }

        protected virtual void paintPicByExcel(Microsoft.Office.Interop.Excel._Worksheet ws)
        {

        }

        protected virtual void paintPicByEt(ET._Worksheet ws)
        {

        }
    }
}
