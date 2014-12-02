using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

namespace Core.Sip.Client.Printer
{
    public class ExcelPrinter : PrinterBase
    {
        private string xmlResourcePath = string.Empty;
        public string XmlResourcePath
        {
            get { return xmlResourcePath; }
            set { xmlResourcePath = value.Contains(":") ? value : AppDomain.CurrentDomain.BaseDirectory + value; }
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

        public ExcelPrinter()
        {
            
        }

        public ExcelPrinter(string xmlResourcePath)
        {
            if (!xmlResourcePath.Contains(":"))
            {
                xmlResourcePath = AppDomain.CurrentDomain.BaseDirectory + xmlResourcePath;
            }

            this.xmlResourcePath = xmlResourcePath;
        }

        public ExcelPrinter(string xmlResourcePath, ArrayList paramList)
        {
            if (!xmlResourcePath.Contains(":"))
            {
                xmlResourcePath = AppDomain.CurrentDomain.BaseDirectory + xmlResourcePath;
            }
            this.xmlResourcePath = xmlResourcePath;
            this.paramList = paramList;
        }

        public void printExcel()
        {
            try
            {
                string excelPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp.xls";
                if (createTmpFile(this.xmlResourcePath, excelPath))
                {
                    System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                    string printerName = printDoc.PrinterSettings.PrinterName;
                    //设置自定义纸张
                    if (base._PageSetup.IsCustom)
                    {
                        printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("原料标签",base._PageSetup.Width,base._PageSetup.Length);
                    }
                    this.printExcel(excelPath, printerName);
                }                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void printExcel(string xmlResourceStr)
        {
            try
            {
                string excelPath = AppDomain.CurrentDomain.BaseDirectory + "\\tmp.xls";
                try
                {
                    StreamWriter writer = new StreamWriter(excelPath, false, Encoding.UTF8);
                    writer.Write(xmlResourceStr);
                    writer.Close();

                    System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                    string printerName = printDoc.PrinterSettings.PrinterName;
                    //设置自定义纸张
                    if (base._PageSetup.IsCustom)
                    {
                        printDoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("原料标签", base._PageSetup.Width, base._PageSetup.Length);
                    }
                    this.printExcel(excelPath, printerName);
                }
                catch (Exception e)
                {
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool createTmpFile(string inPath, string outPath)
        {
            bool flag = true;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                string bufferStr = string.Empty;
                int index = 0;
                int count = paramList.Count;

                if (string.IsNullOrEmpty(inPath))
                {
                    return false;
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(inPath);
                foreach (XmlNode n in doc.GetElementsByTagName("Data"))
                {
                    if (n.InnerText.Contains(DATE_REPLACE_STR))
                    {
                        bufferStr = index < count ? paramList[index++].ToString() : "";
                        n.InnerText = n.InnerText.Replace(DATE_REPLACE_STR,bufferStr);
                    }
                }

                //bufferStr = doc.OuterXml;
                //reader = new StreamReader(inPath, Encoding.UTF8);
                writer = new StreamWriter(outPath, false, Encoding.UTF8);
                writer.Write(doc.OuterXml);

                //while (!string.IsNullOrEmpty(bufferStr = reader.ReadLine()))
                //{
                //    while (bufferStr.Contains(">" + DATE_REPLACE_STR + "<"))
                //    {
                //        bufferStr = bufferStr.Replace(DATE_REPLACE_STR, "");
                //    }
                //    writer.WriteLine(bufferStr);
                //}
            }
            catch (Exception e)
            {
                flag = false;
                throw e;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
            return flag;
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
