using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Core.Sip.Client.Printer
{
    public class XLSPrinter : PrinterBase
    {
        private string xlsPath = string.Empty;
        public string XlsPath
        {
            get { return xlsPath; }
            set { xlsPath = value.Contains(":") ? value : AppDomain.CurrentDomain.BaseDirectory + value; }
        }
        private ArrayList paramList = new ArrayList();
        public ArrayList ParamList
        {
            get { return paramList; }
            set { paramList = value; }
        }
        private PictureInfo picInfo = new PictureInfo();
        public PictureInfo PicInfo
        {
            get { return picInfo; }
            set { picInfo = value; }
        }

        /// <summary>
        /// 打印方向设置 default0纵向  1横向
        /// </summary>
        public int Orientation
        {
            get { return base.PageSetup.Orientation; }
            set { base.PageSetup.Orientation = value; }
        }

        /// <summary>
        ///纸张设置
        /// </summary>
        public  new PageSetup PageSetup
        {
            get { return base.PageSetup; }
            set { base.PageSetup = value; }
        }

        public XLSPrinter()
        {
            
        }

        public XLSPrinter(string xlsPath)
        {
            if (!xlsPath.Contains(":"))
            {
                xlsPath = AppDomain.CurrentDomain.BaseDirectory + xlsPath;
            }

            this.xlsPath = xlsPath;
        }

        public XLSPrinter(string xlsPath, ArrayList paramList)
        {
            if (!xlsPath.Contains(":"))
            {
                xlsPath = AppDomain.CurrentDomain.BaseDirectory + xlsPath;
            }
            this.xlsPath = xlsPath;
            this.paramList = paramList;
        }

        public void printExcel()
        {
            try
            {
                System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                string printerName = printDoc.PrinterSettings.PrinterName;
                this.printExcel(xlsPath, printerName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected override void paintPicByExcel(Microsoft.Office.Interop.Excel._Worksheet ws)
        {
            if (!string.IsNullOrEmpty(PicInfo.PicPath))
            {
                Microsoft.Office.Interop.Excel.Pictures pics = (Microsoft.Office.Interop.Excel.Pictures)ws.Pictures(missing);
                Microsoft.Office.Interop.Excel.Picture pic = pics.Insert(picInfo.PicPath, missing);
                pic.Left = picInfo.PicLeft;
                pic.Top = picInfo.PicTop;
                pic.Width = picInfo.PicWidth;
                pic.Height = picInfo.PicHeight;
            }
        }

        protected override void paintPicByEt(ET._Worksheet ws)
        {
            if (!string.IsNullOrEmpty(PicInfo.PicPath))
            {
                ET.Pictures pics = (ET.Pictures)ws.Pictures(missing);
                pics.Insert(picInfo.PicPath, missing);
            }
        }
    }
}
