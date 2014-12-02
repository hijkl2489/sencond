using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Core.Sip.Client.Meas
{
    public class ExcelPrinter 
    {
        #region <导入API>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("User32.dll")]
        public static extern bool ShowWindow(IntPtr HWND, int MSG);
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string ClassN, string WindN);
        [DllImport("User32.dll")]
        public static extern IntPtr GetTopWindow(IntPtr Phwnd);
        [DllImport("User32.dll")]
        public static extern IntPtr CloseWindow(IntPtr HWND);
        #endregion

        //打印数据要包含打印类型(外贸\内销..)及对应的数据
        #region <成员变量>
        public DataSet _printData;
        public string _printType;   //外贸\内销
        public string _printCopies; //打印份数


        private string _excelName = "";
        private string _excelWindowsName = "";
        private string _excelPath = "";
        private Hashtable _iniTable = null;
        private Hashtable _iniCell = null;
        private object missing = System.Reflection.Missing.Value; // 在引用excel时，有些参数为空，就用它替换
        private Microsoft.Office.Interop.Excel.Application excel = null;// 引用excel组件
        private Microsoft.Office.Interop.Excel.Workbook workbook = null; // 工作薄
        private Microsoft.Office.Interop.Excel.Worksheet worksheet = null; // 工作表
        Microsoft.Office.Interop.Excel.Worksheet sh2 = null;
        Microsoft.Office.Interop.Excel.Worksheet sh3 = null;
        ArrayList pids = null;
        #endregion

        #region <构造函数>
        public ExcelPrinter()
        {
            pids = new ArrayList();
            _iniTable = new Hashtable();
            _iniCell = new Hashtable();
            //在创建进程前将所有excel进程记录下来，打印结束后将增加的excel进程kill
            foreach (Process process in System.Diagnostics.Process.GetProcesses())
            {
                if (process.ProcessName.ToUpper().Equals("EXCEL"))
                {
                    pids.Add(process.Id);
                }
            }

            excel=new Microsoft.Office.Interop.Excel.ApplicationClass();
        }
        #endregion

        #region <公共方法>
        //写INI文件
        public void IniWriteValue(string Section, string Key, string Value)
        {
            string Path = Application.StartupPath + "\\Core.ZGGJMes.Client.Printer.ini";
            WritePrivateProfileString(Section, Key, Value, Path);
            string a = "Φ";
        }

        //读取INI文件指定
        public string IniReadValue(string Section, string Key)
        {
            try
            {
                string Path = Application.StartupPath + "\\Core.ZGGJMes.Client.Printer.ini";
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp, 255, Path);
                return temp.ToString();
            }
            catch { }
            return null;
        }

        public ArrayList DoPrint()
        {
            //根据配置文件读取有关excel模版的信息
            _excelName = IniReadValue("模版信息", _printType);
            _excelWindowsName = "Microsoft Excel - " + _excelName+"  [只读]";
            _excelPath = Application.StartupPath + "\\" + _excelName;

            //当条形码打印成功后增加到arraylist里返回到调用方设置已打印标志
            ArrayList _arPrinted = new ArrayList();
            try
            {
                if (_printData == null || _printData.Tables.Count == 0 || _printData.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("无打印数据!", "提示");
                    CloseExcel();
                    return _arPrinted;
                }
                //检验是否取到模版信息，否则退出
                if (_excelName == "" || _excelWindowsName == "" || _excelPath == "")
                {
                    MessageBox.Show("模版信息维护错误！", "提示");
                    CloseExcel();
                    return _arPrinted;
                }
                //判断excel模版是否已经被打开，打开excel模版
                IntPtr a = FindWindow(null, _excelWindowsName);
                if (a.ToString() != "0")
                {
                    MessageBox.Show("Excel模版已经被打开,可能是正在打印或人为打开。\n请确认没有执行打印程序,然后手动关闭该EXCEL文档。", "提示");
                    CloseExcel();
                    return _arPrinted;
                }

                excel.Visible = false;
                excel.DisplayAlerts = false;
                excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlNormal;
                excel.Top = 8000;
                excel.WorkbookActivate+=new Microsoft.Office.Interop.Excel.AppEvents_WorkbookActivateEventHandler(excel_WorkbookActivate);
                string fileName = _excelPath;
                workbook = excel.Workbooks.Open(fileName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing,missing, missing);
                workbook.ReadOnlyRecommended = true;
                Int32 sheetNum1 = 0;
                try
                {
                    sheetNum1 = Convert.ToInt32(IniReadValue(_printType, "FIELDNUM"));
                }
                catch
                {
                    MessageBox.Show("配置文件-" + _printType + "-FIELDNUM，维护错误");
                    CloseExcel(); 
                    return _arPrinted;
                }

                Hashtable hs = new Hashtable();
                Hashtable hs2 = new Hashtable();
                for (Int32 s = 1; s < sheetNum1 + 1; s++)
                {
                    string FieldValue = IniReadValue(_printType, "F" + s.ToString());
                    hs.Add("F" + s.ToString(), FieldValue);
                    string FieldCell = IniReadValue(_printType, "C" + s.ToString());
                    hs2.Add("C" + s.ToString(), FieldCell);
                }
                _iniTable.Add(_printType, hs);
                _iniCell.Add(_printType, hs2);


                //针对每一行数据生成数据表
                Int32 num = _printData.Tables[0].Rows.Count;

                //for (int i = 1; i <= workbook.Worksheets.Count; i++)
                //{
                //    string aaa = ((Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[i]).Name;
                //    sh2 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(aaa);
                //    _printType = aaa;
                //}

                sh2 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(_printType);
                for (int i = 1; i < num+1; i++)
                {
                    if (i == 1)
                    {
                        sh3 = sh2;
                    }
                    else
                    {
                        sh3 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(_printType + " (" + Convert.ToString(i) + ")");
                    }
                    sh2.Copy(Type.Missing, sh3);
 
                }
                string Barcode = "";
 
                for (int i = 0; i < num; i++)
                {
                    //根据配置文件设置excel中的数据
                    DataRow r = _printData.Tables[0].Rows[i];
                    Barcode = r["条码号"].ToString();
                    string sheet = "";
 
                    sheet = _printType + " (" + Convert.ToString(i + 2) + ")";
 
                    try
                    {
                        GetWorkSheet(sheet);
                    }
                    catch { MessageBox.Show("找不到打印模版表" + sheet, "提示"); return _arPrinted; }

                    Hashtable ht = (Hashtable)_iniTable[_printType];
                    Hashtable ht2 = (Hashtable)_iniCell[_printType];
                    foreach (DictionaryEntry de in ht)
                    {
                        try
                        {
                            SetCellRangeValue(ht2["C" + de.Key.ToString().Substring(1)].ToString(), r[de.Value.ToString()].ToString());
                        }
                        catch { }
                    }
                    SetCellRangeValue("A10", r["条码号"].ToString());
                    _arPrinted.Add(r["条码号"].ToString());
                }


                //调用VBA宏生成条形码
                object robj = new object();
                try
                {
                    RunExcelMacro(_excelPath, "getTime3", new Object[] { "" }, out robj, true);
                }
                catch
                {
                    MessageBox.Show("执行宏失败！", "提示");
                    CloseExcel();
                    return _arPrinted;
                }
                Thread.Sleep(1000);
                //打印输出
                try
                {
                    for (int i = 0; i < Convert.ToInt32(_printCopies); i++)
                    {
                        workbook.Worksheets.PrintOut(2, num+1, 1, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }
                }
                catch
                {
                    MessageBox.Show("连接打印机失败，请检查操作系统中默认打印机是否运行正常！", "提示"); 
                    _arPrinted.Clear();
                    CloseExcel();
                    return _arPrinted;
                }

                CloseExcel();
                return _arPrinted;
            }
            catch
            {
                CloseExcel();
                return _arPrinted;
            }
        }

        void excel_WorkbookActivate(Microsoft.Office.Interop.Excel.Workbook Wb)
        {
            excel.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMinimized;
        }
        /// <summary>
        /// 得到工作表
        /// </summary>
        /// <param name="worksheetName"></param>
        /// <returns></returns>
        public void GetWorkSheet(string worksheetName)
        {
            worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(worksheetName);
        }
        /// <summary>
        /// 销毁Excel内存
        /// </summary>
        public void CloseExcel()
        {
            workbook.Close(false, null, null);
            //退出Excel，并且释放调用的COM资源
            excel.Quit();

            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sh2);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sh3);
            }
            catch { }
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            }
            catch { }
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            }
            catch { }

            worksheet = null;
            sh2 = null;
            sh3 = null;
            workbook = null;
            excel = null;

            GC.Collect();

            KillExcelProcess();
        }
        void KillExcelProcess()
        {
            foreach (Process process in System.Diagnostics.Process.GetProcesses())
            {
                if (process.ProcessName.ToUpper().Equals("EXCEL"))
                {
                    if (!pids.Contains(process.Id))
                    {
                        process.Kill();
                    }
                }
            }
        }
        /// <summary>
        /// 取得工作表名的集合
        /// </summary>
        /// <returns></returns>
        public string[] GetSheetNames()
        {
            string[] names = new string[workbook.Worksheets.Count];
            for (int i = 0; i < names.Length; i++)
            {
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.get_Item(i + 1);
                names[i] = worksheet.Name;
            }
            return names;
        }

        /// <summary>
        /// 根据行号和列号取值
        /// </summary>
        /// <param name="position">如:B6</param>
        /// <returns></returns>
        public object GetCellValue(string position)
        {
            object obj = null;
            try
            {
                obj = worksheet.get_Range(position, position).Value2;
                obj = worksheet.get_Range("A1", Type.Missing).Value2;

            }
            catch (Exception exp)
            {
                //throw exp;
            }
            return obj;
        }

        /// <summary>
        /// 取出范围内的单元格值
        /// </summary>
        /// <param name="fromPosition"></param>
        /// <param name="toPosition"></param>
        /// <returns></returns>
        public object[] GetCellRangeValue(string fromPosition, string toPosition)
        {
            string[] ranges = GetRange(fromPosition, toPosition);
            object[] obj = new object[ranges.Length];
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i] = GetCellValue(ranges[i]);
            }
            return obj;
        }

        /// <summary>
        /// 根据excel中的两点，取出这两点范围的位置。如"A1,A2"，以逗号分隔
        /// </summary>
        /// <param name="fromPosition"></param>
        /// <param name="toPosition"></param>
        /// <returns></returns>
        private string[] GetRange(string fromPosition, string toPosition)
        {
            int[] _formPosition = GetPosition(fromPosition);
            int[] _toPosition = GetPosition(toPosition);
            if (_formPosition[0] > _toPosition[0])
            {
                int temp = _toPosition[0];
                _toPosition[0] = _formPosition[0];
                _formPosition[0] = temp;
            }
            if (_formPosition[1] > _toPosition[1])
            {
                int temp = _toPosition[1];
                _toPosition[1] = _formPosition[1];
                _formPosition[1] = temp;
            }

            string result = "";
            for (int i = _formPosition[0]; i <= _toPosition[0]; i++)
            {
                for (int j = _formPosition[1]; j <= _toPosition[1]; j++)
                {
                    result += ReplaceNumber(i) + j.ToString() + ",";
                }
            }
            if (result.EndsWith(","))
                result = result.Substring(0, result.Length - 1);
            return result.Split(',');
        }

        /// <summary>
        /// 根据行数和列数（列数为字母，并且行号和列号放在一起）取出对应的数字数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int[] GetPosition(string str)
        {
            char[] chars = str.ToUpper().ToCharArray();
            int[] position = new int[2];
            string column = "";
            string row = "";
            for (int i = 0; i < chars.Length; i++)
            {
                if ('A' <= chars[i] && chars[i] <= 'Z')
                    column += chars[i].ToString();
                if ('0' <= chars[i] && chars[i] <= '9')
                    row += chars[i].ToString();
            }
            position[0] = ReplaceString(column);
            position[1] = int.Parse(row);

            return position;
        }

        /// <summary>
        /// 把在excel中的字母列转换成相应的数字
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private string ReplaceNumber(int index)
        {
            int i = index;
            int j = 0;
            if (index > 26)
            {
                i = index % 26;
                j = index / 26;
            }
            if (j == 0)
                return ((char)((int)'A' + i - 1)).ToString();
            else
                return ((char)((int)'A' + j - 1)).ToString() + ((char)((int)'A' + i - 1)).ToString();
        }

        /// <summary>
        /// 与ReplaceNumber相反
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int ReplaceString(string str)
        {
            str = str.ToUpper();
            char[] chars = str.ToCharArray();
            int index = 0;
            if (chars.Length == 1)
                index = (int)chars[0] - (int)'A' + 1;
            else if (chars.Length == 2)
            {
                index = (int)chars[1] - (int)'A' + 1;
                index += ((int)chars[0] - (int)'A' + 1) * 26;
            }
            return index;
        }
        /// <summary>
        /// 设置单元格数据
        /// </summary>
        /// <param name="cellPosition"></param>
        /// <param name="value"></param>
        public void SetCellRangeValue(object cellPosition, string value)
        {
            string[] ranges = GetRange(cellPosition.ToString(), cellPosition.ToString());
            try
            {
                worksheet.get_Range(ranges[0], ranges[0]).Value2 = value;
            }
            catch (Exception exp)
            {
                throw exp;
            }


        }

        public void RunExcelMacro( string excelFilePath, string macroName, object[] parameters, out object rtnValue,bool isShowExcel)
        {
            try
            {
                #region 检查入参

                // 检查文件是否存在
                if (!File.Exists(excelFilePath))
                {
                    throw new System.Exception(excelFilePath + " 文件不存在");
                }

                // 检查是否输入宏名称
                if (string.IsNullOrEmpty(macroName))
                {
                    throw new System.Exception("请输入宏的名称");
                }

                #endregion

                #region 调用宏处理

                // 准备打开Excel文件时的缺省参数对象
                object oMissing = System.Reflection.Missing.Value;

                // 根据参数组是否为空，准备参数组对象
                object[] paraObjects;

                if (parameters == null)
                {
                    paraObjects = new object[] { macroName };
                }
                else
                {
                    // 宏参数组长度
                    int paraLength = parameters.Length;

                    paraObjects = new object[paraLength + 1];

                    paraObjects[0] = macroName;
                    for (int i = 0; i < paraLength; i++)
                    {
                        paraObjects[i + 1] = parameters[i];
                    }
                }

                // 创建Excel对象示例
                Microsoft.Office.Interop.Excel.ApplicationClass oExcel = (Microsoft.Office.Interop.Excel.ApplicationClass)excel;
                // 判断是否要求执行时Excel可见
                if (isShowExcel)
                {
                    // 使创建的对象可见
                    oExcel.Visible = true;
                }

                // 创建Workbooks对象
               // Microsoft.Office.Interop.Excel.Workbooks oBooks = (Microsoft.Office.Interop.Excel.Workbooks)workbook;

                // 执行Excel中的宏
                rtnValue = this.RunMacro(oExcel, paraObjects);
　
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行宏
        /// </summary>
        /// <param name="oApp">Excel对象</param>
        /// <param name="oRunArgs">参数（第一个参数为指定宏名称，后面为指定宏的参数值）</param>
        /// <returns>宏返回值</returns>
        private object RunMacro(object oApp, object[] oRunArgs)
        {
            try
            {
                // 声明一个返回对象
                object objRtn;

                // 反射方式执行宏
                objRtn = oApp.GetType().InvokeMember(
                                                        "Run",
                                                        System.Reflection.BindingFlags.Default |
                                                        System.Reflection.BindingFlags.InvokeMethod,
                                                        null,
                                                        oApp,
                                                        oRunArgs
                                                     );

                // 返回值
                return objRtn;

            }
            catch (Exception ex)
            {
                // 如果有底层异常，抛出底层异常
                if (ex.InnerException.Message.ToString().Length > 0)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
        }
      #endregion
    }
      

}
