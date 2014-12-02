using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Excel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinDataSource;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using System.Collections;
using CoreFS.CA06;

namespace YGJZJL.SquareBilletTransfer
{
    public enum ProduceLine
    {
        BC,
        XC,
        GX
    }

    public enum Post
    {
        Receive,
        Charge,
        DisCharge,
        Roll
    }

    public partial class ucBilletFlowCard : UserControl
    {
        public ucBilletFlowCard()
        {
            InitializeComponent();

            try
            {
                this.ultraGrid1.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGrid1_ClickCellButton);
                ultraGrid1.DisplayLayout.Bands[0].Columns["FD_ZC_ENTERDATETIME"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
                ultraGrid1.DisplayLayout.Bands[0].Columns["FD_ZC_ENTERDATETIME"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnMouseEnter;
                ultraGrid1.DisplayLayout.Bands[0].Columns["FD_ZZ_DATE"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
                ultraGrid1.DisplayLayout.Bands[0].Columns["FD_ZZ_DATE"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnMouseEnter;
            }
            catch { }

            try
            {
                ultraGrid1.DisplayLayout.Bands[0].Columns["FN_LENGTH"].ValueList = CommonMethod.GetValuelistLength();
                CommonMethod.InitUserControl(ref ultraDataSource1, ref ultraGrid1);
                CommonMethod.SetUltraGridCellReadOnly(ref ultraGrid1, true);
            }
            catch { }
        }

        public void SetData(ref DataRow row)
        {
            try
            {
                if (row == null) return;

                if (this.ultraDataSource1.Rows.Count == 0)
                    CommonMethod.InitUserControl(ref ultraDataSource1, ref ultraGrid1);

                CommonMethod.ResetUltraDataSource(ref ultraDataSource1, ref ultraGrid1);

                for (int i = 0; i < ultraDataSource1.Band.Columns.Count; i++)
                {
                    try
                    {
                        if (row.Table.Columns.Contains(ultraDataSource1.Band.Columns[i].Key))
                        {
                            ultraDataSource1.Rows[0][i] = row[ultraDataSource1.Band.Columns[i].Key];
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        public void ResetData()
        {
            CommonMethod.InitUserControl(ref ultraDataSource1, ref ultraGrid1);
        }

        public void SetPost_BilletReceive()
        {
            try
            {
                ArrayList alistEditable = new ArrayList();

                alistEditable.Add("FN_GPYS_NUMBER");
                alistEditable.Add("FS_DJH");

                CommonMethod.SetUltraGridCellEditable(ref ultraGrid1, alistEditable);

                ultraGrid1.ActiveCell = ultraGrid1.Rows[0].Cells["FN_GPYS_NUMBER"];
                ultraGrid1.PerformAction(UltraGridAction.EnterEditMode);
            }
            catch { }
        }

        public bool DataValidation_BilletReceive(out Hashtable htblValue)
        {
            htblValue = new Hashtable();

            try
            {
                if (ultraGrid1.Rows.Count == 0)
                {
                    return false;
                }

                ultraGrid1.UpdateData();

                string strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_GPYS_NUMBER"].Value).Trim();

                if (string.IsNullOrEmpty(strValue))
                {
                    MessageBox.Show("请输入验收条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_GPYS_NUMBER", true);
                    return false;
                }

                int iCount = 0;
                bool bOK = false;

                bOK = int.TryParse(strValue, out iCount);

                if (!bOK)
                {
                    MessageBox.Show("验收条数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_GPYS_NUMBER", true);
                    return false;
                }

                if (iCount < 0)
                {
                    MessageBox.Show("验收条数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_GPYS_NUMBER", true);
                    return false;
                }

                htblValue.Add("V2", strValue);           //验收条数
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FS_DJH"].Value).Trim();
                htblValue.Add("V3", strValue);           //货架号

                return true;
            }
            catch { }

            return false;
        }

        public void SetPost_Charge()
        {
            try
            {
                ArrayList alistEditable = new ArrayList();

                //alistEditable.Add("FS_ZC_BATCHNO");
                alistEditable.Add("FN_ZC_ENTERNUMBER");
                alistEditable.Add("FD_ZC_ENTERDATETIME");
                alistEditable.Add("FS_ZC_MEMO");

                CommonMethod.SetUltraGridCellEditable(ref ultraGrid1, alistEditable);

                ultraGrid1.ActiveCell = ultraGrid1.Rows[0].Cells["FN_ZC_ENTERNUMBER"];
                ultraGrid1.PerformAction(UltraGridAction.EnterEditMode);
            }
            catch { }
        }

        public bool DataValidation_Charge(out Hashtable htblValue)
        {
            htblValue = new Hashtable();

            try
            {
                if (ultraGrid1.Rows.Count == 0)
                {
                    return false;
                }

                ultraGrid1.UpdateData();

                string strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_ZC_ENTERNUMBER"].Value).Trim();

                if (string.IsNullOrEmpty(strValue))
                {
                    MessageBox.Show("请输入入炉条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZC_ENTERNUMBER", true);
                    return false;
                }

                int iCount = 0;
                bool bOK = false;

                bOK = int.TryParse(strValue, out iCount);

                if (!bOK)
                {
                    MessageBox.Show("入炉条数必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZC_ENTERNUMBER", true);
                    return false;
                }

                if (iCount <= 0)
                {
                    MessageBox.Show("入炉条数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZC_ENTERNUMBER", true);
                    return false;
                }

                htblValue.Add("V2", strValue);           //入炉条数
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FD_ZC_ENTERDATETIME"].Text).Trim();
                htblValue.Add("V3", strValue);           //入炉时间
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FS_ZC_MEMO"].Text).Trim();
                htblValue.Add("V5", strValue);           //备注

                return true;
            }
            catch { }

            return false;
        }

        public void SetPost_Rolling(ProduceLine ProLine)
        {
            try
            {
                ArrayList alistEditable = new ArrayList();

                alistEditable.Add("FN_ZZ_SPEC");
                if (ProLine != ProduceLine.GX)
                    alistEditable.Add("FN_LENGTH");
                alistEditable.Add("FD_ZZ_DATE");
                alistEditable.Add("FN_ZZ_NUM");
                alistEditable.Add("FN_ZZ_WASTNUM");
                alistEditable.Add("FS_ZZ_MEMO");

                CommonMethod.SetUltraGridCellEditable(ref ultraGrid1, alistEditable);

                ultraGrid1.ActiveCell = ultraGrid1.Rows[0].Cells["FN_ZZ_SPEC"];
                ultraGrid1.PerformAction(UltraGridAction.EnterEditMode);
            }
            catch { }
        }

        public bool DataValidation_Rolling(ProduceLine ProLine, out Hashtable htblValue)
        {
            htblValue = new Hashtable();

            try
            {
                if (ultraGrid1.Rows.Count == 0)
                {
                    return false;
                }

                ultraGrid1.UpdateData();

                string strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_ZZ_SPEC"].Value).Trim();

                if (string.IsNullOrEmpty(strValue))
                {
                    MessageBox.Show("请输入轧制规格！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZZ_SPEC", true);
                    return false;
                }

                int iCount = 0;
                decimal dCount = 0.0M;
                bool bOK = false;

                if (ProLine != ProduceLine.GX)
                {
                    strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_LENGTH"].Value).Trim();

                    if (string.IsNullOrEmpty(strValue))
                    {
                        MessageBox.Show("请输入定尺长度！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_LENGTH", true);
                        return false;
                    }

                    bOK = int.TryParse(strValue, out iCount);

                    if (!bOK)
                    {
                        MessageBox.Show("定尺长度必须是整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_LENGTH", true);
                        return false;
                    }

                    if (iCount < 0)
                    {
                        MessageBox.Show("定尺长度必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_LENGTH", true);
                        return false;
                    }
                }

                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_ZZ_NUM"].Value).Trim();

                if (string.IsNullOrEmpty(strValue))
                {
                    MessageBox.Show("请输入成材条数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZZ_NUM", true);
                    return false;
                }

                bOK = decimal.TryParse(strValue, out dCount);

                if (!bOK)
                {
                    MessageBox.Show("成材条数必须是数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZZ_NUM", true);
                    return false;
                }

                if (dCount < 0)
                {
                    MessageBox.Show("成材条数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZZ_NUM", true);
                    return false;
                }

                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_ZZ_WASTNUM"].Value).Trim();

                if (!string.IsNullOrEmpty(strValue))
                {
                    bOK = decimal.TryParse(strValue, out dCount);

                    if (!bOK)
                    {
                        MessageBox.Show("轧废条数必须是数值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZZ_WASTNUM", true);
                        return false;
                    }

                    if (dCount < 0)
                    {
                        MessageBox.Show("轧废条数必须大于零！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        CommonMethod.SetUltraGridActiveCell(ref ultraGrid1, 0, "FN_ZZ_WASTNUM", true);
                        return false;
                    }
                }

                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_ZZ_SPEC"].Text).Trim();
                htblValue.Add("V2", strValue);           //轧制规格
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_LENGTH"].Text).Trim();
                htblValue.Add("V3", strValue);           //定尺长度
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FD_ZZ_DATE"].Text).Trim();
                htblValue.Add("V4", strValue);           //轧制时间
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_ZZ_NUM"].Text).Trim();
                htblValue.Add("V5", strValue);           //成材支数
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FN_ZZ_WASTNUM"].Text).Trim();
                htblValue.Add("V6", strValue);           //轧废支数
                strValue = Convert.ToString(ultraGrid1.Rows[0].Cells["FS_ZZ_MEMO"].Text).Trim();
                htblValue.Add("V8", strValue);           //轧废支数

                return true;
            }
            catch { }

            return false;
        }

        private void ultraGrid1_ClickCellButton(object sender, CellEventArgs e)
        {
            try
            {
                if ((e.Cell.Column.Key.Equals("FD_ZC_ENTERDATETIME") || e.Cell.Column.Key.Equals("FD_ZZ_DATE")) && e.Cell.Column.CellActivation == Activation.AllowEdit)
                {
                    e.Cell.Value = DateTime.Now;
                }
            }
            catch { }
        }

        protected override bool ShowFocusCues
        {
            get
            {
                return false;
            }
        }
    }

    public class CommonMethod
    {
        public static void InitUserControl(ref UltraDataSource ultDataSource, ref UltraGrid ultraGrid)
        {
            try
            {
                int iColumnsCount = ultDataSource.Band.Columns.Count;
                object[] obj = new object[iColumnsCount];

                ultDataSource.Rows.Clear();

                for (int i = 0; i < ultDataSource.Band.Columns.Count; i++)
                {
                    try
                    {
                        if (ultDataSource.Band.Columns[i].DataType == typeof(Bitmap) ||
                            ultDataSource.Band.Columns[i].DataType == typeof(Image))
                            obj[i] = null;
                        else if (ultDataSource.Band.Columns[i].DataType == typeof(DateTime))
                            obj[i] = DBNull.Value;
                        else if (ultDataSource.Band.Columns[i].DataType == typeof(decimal) ||
                                ultDataSource.Band.Columns[i].DataType == typeof(double) ||
                                ultDataSource.Band.Columns[i].DataType == typeof(Single))
                            obj[i] = 0;
                        else
                            obj[i] = "";
                    }
                    catch { }

                    try
                    {
                        ultraGrid.DisplayLayout.Bands[0].Columns[i].Header.Appearance.FontData.Bold = DefaultableBoolean.False;
                    }
                    catch { }
                }

                ultDataSource.Rows.Add(obj);

                ultraGrid.UpdateData();
            }
            catch { }
        }

        public static void SetUltraGridCellReadOnly(ref UltraGrid ultraGrid, bool bReadOnly)
        {
            try
            {
                for (int i = 0; i < ultraGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    try
                    {
                        ultraGrid.DisplayLayout.Bands[0].Columns[i].CellActivation = (bReadOnly ? Activation.ActivateOnly : Activation.AllowEdit);
                        ultraGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.BackColor = (bReadOnly ? Color.Bisque : Color.White);
                    }
                    catch { }
                }
            }
            catch { }
        }

        public static void SetUltraGridCellReadOnly(ref UltraGrid ultraGrid, ArrayList alistColumns)
        {
            try
            {
                bool bReadOnly = false;

                for (int i = 0; i < ultraGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    try
                    {
                        if (alistColumns == null || !alistColumns.Contains(ultraGrid.DisplayLayout.Bands[0].Columns[i].Key))
                        {
                            bReadOnly = false;
                        }
                        else
                        {
                            bReadOnly = true;
                        }

                        ultraGrid.DisplayLayout.Bands[0].Columns[i].CellActivation = (bReadOnly ? Activation.ActivateOnly : Activation.AllowEdit);
                        ultraGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.BackColor = (bReadOnly ? Color.Bisque : Color.White);
                    }
                    catch { }
                }
            }
            catch { }
        }

        public static void SetUltraGridCellEditable(ref UltraGrid ultraGrid, ArrayList alistColumns)
        {
            try
            {
                bool bEditable = false;

                for (int i = 0; i < ultraGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    try
                    {
                        if (alistColumns == null || !alistColumns.Contains(ultraGrid.DisplayLayout.Bands[0].Columns[i].Key))
                        {
                            bEditable = false;
                        }
                        else
                        {
                            bEditable = true;
                        }

                        ultraGrid.DisplayLayout.Bands[0].Columns[i].CellActivation = (!bEditable ? Activation.ActivateOnly : Activation.AllowEdit);
                        ultraGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.BackColor = (!bEditable ? Color.Bisque : Color.White);
                    }
                    catch { }
                }
            }
            catch { }
        }

        public static void ResetUltraDataSource(ref UltraDataSource ultDataSource, ref UltraGrid ultraGrid)
        {
            try
            {
                if (ultDataSource.Rows.Count == 0) return;

                for (int i = 0; i < ultDataSource.Band.Columns.Count; i++)
                {
                    try
                    {
                        if (ultDataSource.Band.Columns[i].DataType == typeof(Bitmap) ||
                            ultDataSource.Band.Columns[i].DataType == typeof(Image))
                            ultDataSource.Rows[0][i] = null;
                        else if (ultDataSource.Band.Columns[i].DataType == typeof(DateTime))
                            ultDataSource.Rows[0][i] = DBNull.Value;
                        else if (ultDataSource.Band.Columns[i].DataType == typeof(decimal) ||
                            ultDataSource.Band.Columns[i].DataType == typeof(double) ||
                            ultDataSource.Band.Columns[i].DataType == typeof(Single))
                            ultDataSource.Rows[0][i] = 0;
                        else
                            ultDataSource.Rows[0][i] = "";
                    }
                    catch { }
                }

                ultraGrid.UpdateData();
            }
            catch { }
        }

        public static bool UltraGridContainsColumn(ref UltraGrid grid, string strColumn)
        {
            if (grid == null || grid.DisplayLayout.Bands[0].Columns.Count <= 0)
                return false;

            for (int i = 0; i < grid.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                if (grid.DisplayLayout.Bands[0].Columns[i].Key.Equals(strColumn))
                    return true;
            }

            return false;
        }

        public static bool UltraGridContainsColumn(ref UltraGrid grid, int BandIndex, string strColumn)
        {
            if (grid == null || grid.DisplayLayout.Bands[BandIndex].Columns.Count <= 0)
                return false;

            if (BandIndex <= grid.DisplayLayout.Bands.Count - 1)
            {
                for (int i = 0; i < grid.DisplayLayout.Bands[BandIndex].Columns.Count; i++)
                {
                    if (grid.DisplayLayout.Bands[BandIndex].Columns[i].Key.Equals(strColumn))
                        return true;
                }
            }

            return false;
        }

        public static void SetUltraGridActiveCell(ref UltraGrid ultraGrid, int RowIndexNo, string ColumnName, bool EnterEditMode)
        {
            try
            {
                if (ultraGrid != null && RowIndexNo >= 0 && ultraGrid.Rows.Count >= RowIndexNo + 1)
                {
                    ultraGrid.ActiveCell = ultraGrid.Rows[RowIndexNo].Cells[ColumnName];

                    if (EnterEditMode)
                        ultraGrid.PerformAction(UltraGridAction.EnterEditMode);
                }
            }
            catch { }
        }

        public static bool RecordLocation(ref UltraGrid ultraGrid, string strColumn, object value, out int iRowIndex)
        {
            iRowIndex = -1;

            try
            {
                if (ultraGrid != null && ultraGrid.Rows.Count > 0)
                {
                    for (int i = 0; i < ultraGrid.Rows.Count; i++)
                    {
                        if (ultraGrid.Rows[i].Cells[strColumn].Value == value)
                        {
                            iRowIndex = ultraGrid.Rows[i].Index;
                            return true;
                        }
                    }
                }
            }
            catch { }

            return false;
        }

        public static ValueList GetValuelistLength()
        {
            ValueList vlist = new ValueList();
            try
            {
                vlist.ValueListItems.Add(9, "9");
                vlist.ValueListItems.Add(11, "11");
                vlist.ValueListItems.Add(12, "12");
            }
            catch { }

            return vlist;
        }

        public static ValueList GetValuelistBanci()
        {
            ValueList vlist = new ValueList();
            try
            {
                vlist.ValueListItems.Add("0", "常白班");
                vlist.ValueListItems.Add("1", "早");
                vlist.ValueListItems.Add("2", "中");
                vlist.ValueListItems.Add("3", "晚");
            }
            catch { }

            return vlist;
        }

        public static ValueList GetValuelistBanzu()
        {
            ValueList vlist = new ValueList();
            try
            {
                vlist.ValueListItems.Add("0", "常白班");
                vlist.ValueListItems.Add("1", "甲");
                vlist.ValueListItems.Add("2", "乙");
                vlist.ValueListItems.Add("3", "丙");
                vlist.ValueListItems.Add("4", "丁");
            }
            catch { }

            return vlist;
        }

        public static void CopyDataToDatatable(ref DataTable src, ref DataTable dest, bool ClearExists)
        {
            if (src == null || dest == null)
            {
                return;
            }

            if (ClearExists)
            {
                dest.Rows.Clear();
            }

            DataRow CurRow, NewRow;

            for (int i = 0; i < src.Rows.Count; i++)
            {
                CurRow = src.Rows[i];
                NewRow = dest.NewRow();

                for (int j = 0; j < src.Columns.Count; j++)
                {
                    try
                    {
                        if (dest.Columns.Contains(src.Columns[j].ColumnName))
                        {
                            NewRow[src.Columns[j].ColumnName] = CurRow[j];
                        }
                    }
                    catch { }
                }

                dest.Rows.Add(NewRow);
            }

            dest.AcceptChanges();
        }

        public static void SetUltraGridRowFilter(ref UltraGrid ultraGrid, bool bAllowFilter)
        {
            try
            {
                if (bAllowFilter)
                {
                    if (ultraGrid.DisplayLayout.Override.FilterUIType != FilterUIType.FilterRow)
                        ultraGrid.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;

                    for (int i = 0; i < ultraGrid.DisplayLayout.Bands.Count; i++)
                    {
                        for (int j = 0; j < ultraGrid.DisplayLayout.Bands[i].Columns.Count; j++)
                        {
                            try
                            {
                                if (!ultraGrid.DisplayLayout.Bands[i].Columns[j].Hidden &&
                                    ultraGrid.DisplayLayout.Bands[i].Columns[j].RowLayoutColumnInfo.LabelPosition != LabelPosition.LabelOnly)
                                    ultraGrid.DisplayLayout.Bands[i].Columns[j].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
                            }
                            catch { }
                        }
                    }
                }
                else
                {
                    if (ultraGrid.DisplayLayout.Override.FilterUIType != FilterUIType.HeaderIcons)
                        ultraGrid.DisplayLayout.Override.FilterUIType = FilterUIType.HeaderIcons;

                    for (int i = 0; i < ultraGrid.DisplayLayout.Bands.Count; i++)
                    {
                        try
                        {
                            ultraGrid.DisplayLayout.Bands[i].ColumnFilters.ClearAllFilters();
                        }
                        catch { }

                        for (int j = 0; j < ultraGrid.DisplayLayout.Bands[i].Columns.Count; j++)
                        {
                            try
                            {
                                ultraGrid.DisplayLayout.Bands[i].Columns[j].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
        }

        public static void RefreshAndAutoSize(Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid)
        {
            try
            {
                ultraGrid.DataBind();

                foreach (UltraGridBand band in ultraGrid.DisplayLayout.Bands)
                {
                    foreach (UltraGridColumn column in ultraGrid.DisplayLayout.Bands[band.Key].Columns)
                    {
                        column.PerformAutoResize(PerformAutoSizeType.AllRowsInBand);
                    }
                }

                ultraGrid.Refresh();
            }
            catch { }
        }

        public static Point SetChildWindowLocation(Size ChildWindowSize)
        {
            int width = Cursor.Position.X + ChildWindowSize.Width - Screen.PrimaryScreen.Bounds.Width;
            int height = Cursor.Position.Y + ChildWindowSize.Height - Screen.PrimaryScreen.Bounds.Height + 30;

            width = (width > 0) ? (Cursor.Position.X - width) : Cursor.Position.X;                      // X坐标
            height = (height > 0) ? (Cursor.Position.Y - height + 10) : (Cursor.Position.Y + 10);       // Y坐标

            return new Point(width, height);
        }

        public static void ExportDataWithSaveDialog(ref UltraGrid ultraGrid1, string strFileName)
        {
            try
            {
                if (ultraGrid1.Rows.Count == 0) return;

                if (strFileName.Length == 0)
                    strFileName = "未命名";

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "保存";
                dlg.OverwritePrompt = true;
                dlg.Filter = "Excel文件(*.xls)|*.xls";
                dlg.AddExtension = true;
                dlg.FileName = strFileName;

                string strCaption = strFileName;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    strFileName = dlg.FileName;

                    Workbook wb = new Workbook();
                    Worksheet ws2 = wb.Worksheets.Add("Sheet1");
                    WorksheetMergedCellsRegion region = null;

                    ws2.Rows[0].Cells[0].Value = strCaption;

                    ws2.Rows[0].Cells[0].CellFormat.Alignment = Infragistics.Excel.HorizontalCellAlignment.Center;
                    ws2.Rows[0].Cells[0].CellFormat.VerticalAlignment = Infragistics.Excel.VerticalCellAlignment.Center;
                    ws2.Rows[0].Cells[0].CellFormat.WrapText = Infragistics.Excel.ExcelDefaultableBoolean.True;
                    ws2.Rows[0].Cells[0].CellFormat.Font.Name = "宋体";
                    ws2.Rows[0].Cells[0].CellFormat.Font.Height = 350;
                    ws2.Rows[0].Cells[0].CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                    ws2.Rows[0].Height = 880;

                    int iMaxY = 0;
                    int iMaxHeight = 0;
                    int iMaxWidth = 0;
                    for (int i = 0; i < ultraGrid1.DisplayLayout.Bands[0].Columns.Count; i++)
                    {
                        if (!ultraGrid1.DisplayLayout.Bands[0].Columns[i].Hidden && ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.LabelPosition != LabelPosition.None)
                        {
                            try
                            {
                                ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX].Value = ultraGrid1.DisplayLayout.Bands[0].Columns[i].Header.Caption;
                                ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX].CellFormat.Alignment = Infragistics.Excel.HorizontalCellAlignment.Center;
                                ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX].CellFormat.VerticalAlignment = Infragistics.Excel.VerticalCellAlignment.Center;
                                ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX].CellFormat.WrapText = Infragistics.Excel.ExcelDefaultableBoolean.True;
                                ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX].CellFormat.Font.Bold = Infragistics.Excel.ExcelDefaultableBoolean.True;
                                ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX].CellFormat.WrapText = Infragistics.Excel.ExcelDefaultableBoolean.True;
                                ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX].CellFormat.Font.Name = "宋体";

                                region = ws2.MergedCellsRegions.Add(1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY,
                                      ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX,
                                      1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY +
                                      ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.SpanY - 1,
                                      ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX +
                                      ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.SpanX - 1);

                                if (ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Height != 500)
                                    ws2.Rows[1 + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY].Height = 500;

                                if (ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.SpanY > iMaxHeight)
                                {
                                    iMaxHeight = ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.SpanY;
                                }

                                if (ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY > iMaxY)
                                {
                                    iMaxY = ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginY;
                                    iMaxHeight = iMaxY + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.SpanY;
                                }

                                if (ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.SpanX + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX > iMaxWidth)
                                {
                                    iMaxWidth = ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.SpanX + ultraGrid1.DisplayLayout.Bands[0].Columns[i].RowLayoutColumnInfo.OriginX;
                                }
                            }
                            catch (Exception ex) { string str = ex.Message; }
                        }
                    }

                    region = ws2.MergedCellsRegions.Add(0, 0, 0, iMaxWidth - 1);

                    decimal dValue = 0.0M;
                    bool bDecimal = false;
                    for (int i = 0; i < ultraGrid1.Rows.Count; i++)
                    {
                        for (int j = 0; j < ultraGrid1.DisplayLayout.Bands[0].Columns.Count; j++)
                        {
                            try
                            {
                                if (!ultraGrid1.DisplayLayout.Bands[0].Columns[j].Hidden && ultraGrid1.DisplayLayout.Bands[0].Columns[j].RowLayoutColumnInfo.LabelPosition != LabelPosition.LabelOnly)
                                {
                                    if (decimal.TryParse(Convert.ToString(ultraGrid1.Rows[i].Cells[j].Text), out dValue))
                                    {
                                        if (dValue == 0 || dValue < 1 || !Convert.ToString(ultraGrid1.Rows[i].Cells[j].Text).StartsWith("0"))
                                        {
                                            bDecimal = true;
                                        }
                                        else
                                        {
                                            bDecimal = false;
                                        }
                                    }
                                    else
                                    {
                                        bDecimal = false;
                                    }

                                    if (bDecimal)
                                    {
                                        ws2.Rows[1 + iMaxHeight + i].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[j].RowLayoutColumnInfo.OriginX].Value = dValue;
                                    }
                                    else
                                    {
                                        ws2.Rows[1 + iMaxHeight + i].Cells[ultraGrid1.DisplayLayout.Bands[0].Columns[j].RowLayoutColumnInfo.OriginX].Value = ultraGrid1.Rows[i].Cells[j].Text;
                                    }
                                }
                            }
                            catch { }

                        }
                    }

                    BIFF8Writer.WriteWorkbookToFile(wb, strFileName);

                    if (File.Exists(strFileName) &&
                        MessageBox.Show("数据导出成功！\r\n需要打开所导出文件吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ProcessStartInfo p = new ProcessStartInfo(strFileName);
                        p.WorkingDirectory = Path.GetDirectoryName(strFileName);
                        Process.Start(p);
                    }
                }
            }
            catch { }

        }

        private static UltraGridExcelExporter ultraGridExcelExporter1 = new UltraGridExcelExporter();

        public static void ExportDataWithSaveDialog2(ref UltraGrid myGrid1, string strFileName)
        {
            try
            {
                if (myGrid1.Rows.Count == 0) return;

                if (strFileName.Length == 0)
                    strFileName = "未命名";

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "保存";
                dlg.OverwritePrompt = true;
                dlg.Filter = "Excel文件(*.xls)|*.xls";
                dlg.AddExtension = true;
                dlg.FileName = strFileName;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    strFileName = dlg.FileName;
                    ultraGridExcelExporter1.Export(myGrid1, strFileName);

                    if (MessageBox.Show("数据导出成功！\r\n需要打开所导出文件吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ProcessStartInfo p = new ProcessStartInfo(strFileName);
                        p.WorkingDirectory = Path.GetDirectoryName(strFileName);
                        Process.Start(p);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
