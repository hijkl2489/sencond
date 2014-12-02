using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Diagnostics;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace YGJZJL.PublicComponent
{
    public class Constant
    {
        private static string m_RunPath = "";//设置程序运行(启动)路径
        public static WaitingForm WaitingForm = null;
        
        public Constant()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            WaitingForm = new WaitingForm();
            if (m_RunPath.Trim().Length == 0)
            {
                m_RunPath = System.Environment.CurrentDirectory;
            }
        }

        public static string RunPath
        {//取得运行路径
            get
            {
                return m_RunPath;
            }
            set
            {
                m_RunPath = value;
            }
        }

        #region UltraGrid设置

        public static void RefreshAndAutoSize(Infragistics.Win.UltraWinGrid.UltraGrid grid)
        {
            //grid.DataBind();

            grid.ActiveRow = null;

            foreach (Infragistics.Win.UltraWinGrid.UltraGridColumn ugc in grid.DisplayLayout.Bands[0].Columns)
            {
                ugc.PerformAutoResize(PerformAutoSizeType.AllRowsInBand);
            }

            grid.Refresh();
        }

        public static void ExportGrid2Excel(System.Windows.Forms.Form Form,
            Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ExcelExporter,
            Infragistics.Win.UltraWinGrid.UltraGrid Grid)
        {
            Cursor oldCursor = Form.Cursor;

            Form.Cursor = Cursors.WaitCursor;

            if (Constant.WaitingForm == null)
            {
                Constant.WaitingForm = new WaitingForm();
            }

            Constant.WaitingForm.ShowToUser = true;
            Constant.WaitingForm.Show();
            Constant.WaitingForm.Update();

            try
            {
                if (!System.IO.Directory.Exists(System.Environment.CurrentDirectory + "\\temp"))
                {
                    System.IO.Directory.CreateDirectory(System.Environment.CurrentDirectory + "\\temp");
                }

                string StrfileName = string.Format(System.Environment.CurrentDirectory + "\\temp\\" + Form.Text + ".xls");
                ExcelExporter.Export(Grid, StrfileName);

                ProcessStartInfo p = new ProcessStartInfo(StrfileName);
                p.WorkingDirectory = Path.GetDirectoryName(StrfileName);
                Process.Start(p);

                Form.Cursor = oldCursor;
                Constant.WaitingForm.ShowToUser = false;
                Constant.WaitingForm.Close();
            }
            catch (Exception ex)
            {
                Form.Cursor = oldCursor;
                Constant.WaitingForm.ShowToUser = false;
                Constant.WaitingForm.Close();
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        public static void ConvertTOspell(TextBox tb1, TextBox tb2)//将tb1中的拼音助记码显示在tb2中
        {
            string[] chArray = new string[tb1.Text.Length];
            string[] spArray = new string[tb1.Text.Length];
            tb2.Clear();
            for (int i = 0; i < tb1.Text.Length; i++)
            {
                chArray[i] = tb1.Text.ToString().Substring(i, 1);
                try
                {
                    spArray[i] = ConvertChinestToSpell(chArray[i]);
                }
                catch
                {
                    spArray[i] = chArray[i];
                }

            }

            for (int i = 0; i < spArray.Length; ++i)
            {

                tb2.Text += spArray[i];
            }
        }


        #region 处理拼音助记码
        /// <summary>
        /// 获取一串汉字的拼音声母
        /// </summary>
        /// <param name="chinese">Unicode格式的汉字字符串</param>
        /// <returns>拼音声母字符串</returns>
        /// <example>
        /// “杭城风萧萧”转换为“HCFXX”
        /// </example>
        public static String ConvertChinestToSpell(String chinese)
        {
            char[] buffer = new char[chinese.Length];
            for (int i = 0; i < chinese.Length; i++)
            {
                buffer[i] = ConvertChinestToSpell(chinese[i]);
            }
            return new String(buffer);
        }

        /// <summary>
        /// 获取一个汉字的拼音声母
        /// </summary>
        /// <param name="chinese">Unicode格式的一个汉字</param>
        /// <returns>汉字的声母</returns>
        public static char ConvertChinestToSpell(Char chinese)
        {
            Encoding gb2312 = Encoding.GetEncoding("GB2312");
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte[].
            byte[] unicodeBytes = unicode.GetBytes(new Char[] { chinese });
            // Perform the conversion from one encoding to the other.
            byte[] asciiBytes = Encoding.Convert(unicode, gb2312, unicodeBytes);

            // 计算该汉字的GB-2312编码
            int n = (int)asciiBytes[0] << 8;
            n += (int)asciiBytes[1];

            // 根据汉字区域码获取拼音声母
            if (In(0xB0A1, 0xB0C4, n)) return 'A';
            if (In(0XB0C5, 0XB2C0, n)) return 'B';
            if (In(0xB2C1, 0xB4ED, n)) return 'C';
            if (In(0xB4EE, 0xB6E9, n)) return 'D';
            if (In(0xB6EA, 0xB7A1, n)) return 'E';
            if (In(0xB7A2, 0xB8c0, n)) return 'F';
            if (In(0xB8C1, 0xB9FD, n)) return 'G';
            if (In(0xB9FE, 0xBBF6, n)) return 'H';
            if (In(0xBBF7, 0xBFA5, n)) return 'J';
            if (In(0xBFA6, 0xC0AB, n)) return 'K';
            if (In(0xC0AC, 0xC2E7, n)) return 'L';
            if (In(0xC2E8, 0xC4C2, n)) return 'M';
            if (In(0xC4C3, 0xC5B5, n)) return 'N';
            if (In(0xC5B6, 0xC5BD, n)) return 'O';
            if (In(0xC5BE, 0xC6D9, n)) return 'P';
            if (In(0xC6DA, 0xC8BA, n)) return 'Q';
            if (In(0xC8BB, 0xC8F5, n)) return 'R';
            if (In(0xC8F6, 0xCBF0, n)) return 'S';
            if (In(0xCBFA, 0xCDD9, n)) return 'T';
            if (In(0xCDDA, 0xCEF3, n)) return 'W';
            if (In(0xCEF4, 0xD188, n)) return 'X';
            if (In(0xD1B9, 0xD4D0, n)) return 'Y';
            if (In(0xD4D1, 0xD7F9, n)) return 'Z';
            return '\0';
        }

        private static bool In(int Lp, int Hp, int Value)
        {
            return ((Value <= Hp) && (Value >= Lp));
        }

        #endregion
    }
}
