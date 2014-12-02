namespace YGJZJL.DynamicTrack
{
    partial class DataManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("find");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ToExcel");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ToExcel");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("find");
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("计量数据", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STOVESEATNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STOVENO", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_NETWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROSSTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_TARETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_GROSSWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_TAREWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROSSPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TAREPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_RECEIVETIME");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings1 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "FN_NETWEIGHT", 4, true, "计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FN_NETWEIGHT", 4, true);
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings2 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Count, null, "FS_POTNO", 1, true, "计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FS_POTNO", 1, true);
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1_Fill_Panel = new System.Windows.Forms.Panel();
            this._panel1_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.uToolBar = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._panel1_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.dataColumn33 = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ugbEdit = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtGh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.qDteEnd = new System.Windows.Forms.DateTimePicker();
            this.qDteBegin = new System.Windows.Forms.DateTimePicker();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uToolBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbEdit)).BeginInit();
            this.ugbEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel1_Fill_Panel);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Left);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Right);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Top);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Bottom);
            this.coreBind.SetDatabasecommand(this.panel1, null);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 26);
            this.panel1.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1, null);
            // 
            // panel1_Fill_Panel
            // 
            this.panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.panel1_Fill_Panel, null);
            this.panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1_Fill_Panel.Location = new System.Drawing.Point(0, 26);
            this.panel1_Fill_Panel.Name = "panel1_Fill_Panel";
            this.panel1_Fill_Panel.Size = new System.Drawing.Size(992, 0);
            this.panel1_Fill_Panel.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1_Fill_Panel, null);
            // 
            // _panel1_Toolbars_Dock_Area_Left
            // 
            this._panel1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Left, null);
            this._panel1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._panel1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 26);
            this._panel1_Toolbars_Dock_Area_Left.Name = "_panel1_Toolbars_Dock_Area_Left";
            this._panel1_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 0);
            this._panel1_Toolbars_Dock_Area_Left.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Left, null);
            // 
            // uToolBar
            // 
            this.uToolBar.DesignerFlags = 1;
            this.uToolBar.DockWithinContainer = this.panel1;
            this.uToolBar.LockToolbars = true;
            this.uToolBar.ShowFullMenusDelay = 500;
            this.uToolBar.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            buttonTool6.InstanceProps.IsFirstInGroup = true;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool7,
            buttonTool6});
            ultraToolbar1.Text = "UltraToolbar1";
            this.uToolBar.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            buttonTool3.SharedPropsInternal.Caption = "导出";
            buttonTool3.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool8.SharedPropsInternal.Caption = "查询";
            buttonTool8.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            this.uToolBar.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool3,
            buttonTool8});
            this.uToolBar.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.uToolBar_ToolClick);
            // 
            // _panel1_Toolbars_Dock_Area_Right
            // 
            this._panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Right, null);
            this._panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(992, 26);
            this._panel1_Toolbars_Dock_Area_Right.Name = "_panel1_Toolbars_Dock_Area_Right";
            this._panel1_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 0);
            this._panel1_Toolbars_Dock_Area_Right.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Right, null);
            // 
            // _panel1_Toolbars_Dock_Area_Top
            // 
            this._panel1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Top, null);
            this._panel1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._panel1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._panel1_Toolbars_Dock_Area_Top.Name = "_panel1_Toolbars_Dock_Area_Top";
            this._panel1_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(992, 26);
            this._panel1_Toolbars_Dock_Area_Top.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Top, null);
            // 
            // _panel1_Toolbars_Dock_Area_Bottom
            // 
            this._panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Bottom, null);
            this._panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 26);
            this._panel1_Toolbars_Dock_Area_Bottom.Name = "_panel1_Toolbars_Dock_Area_Bottom";
            this._panel1_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(992, 0);
            this._panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Bottom, null);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn2,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn33,
            this.dataColumn1,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5});
            this.dataTable1.TableName = "计量数据";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "序号";
            this.dataColumn2.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn15
            // 
            this.dataColumn15.Caption = "罐号";
            this.dataColumn15.ColumnName = "FS_POTNO";
            // 
            // dataColumn16
            // 
            this.dataColumn16.Caption = "炉座号";
            this.dataColumn16.ColumnName = "FS_STOVESEATNO";
            // 
            // dataColumn17
            // 
            this.dataColumn17.Caption = "炉号";
            this.dataColumn17.ColumnName = "FS_STOVENO";
            // 
            // dataColumn18
            // 
            this.dataColumn18.Caption = "净重";
            this.dataColumn18.ColumnName = "FN_NETWEIGHT";
            // 
            // dataColumn30
            // 
            this.dataColumn30.Caption = "重罐时间";
            this.dataColumn30.ColumnName = "FS_GROSSTIME";
            // 
            // dataColumn31
            // 
            this.dataColumn31.Caption = "空罐时间";
            this.dataColumn31.ColumnName = "FD_TARETIME";
            // 
            // dataColumn32
            // 
            this.dataColumn32.Caption = "毛重";
            this.dataColumn32.ColumnName = "FN_GROSSWEIGHT";
            // 
            // dataColumn33
            // 
            this.dataColumn33.Caption = "皮重";
            this.dataColumn33.ColumnName = "FN_TAREWEIGHT";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "毛重计量员";
            this.dataColumn1.ColumnName = "FS_GROSSPERSON";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "皮重计量员";
            this.dataColumn3.ColumnName = "FS_TAREPERSON";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "收货库管员";
            this.dataColumn4.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "收库时间";
            this.dataColumn5.ColumnName = "FD_RECEIVETIME";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.coreBind.SetDatabasecommand(this.panel2, null);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 628);
            this.panel2.TabIndex = 4;
            this.coreBind.SetVerification(this.panel2, null);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ultraGrid1);
            this.coreBind.SetDatabasecommand(this.panel4, null);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 104);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(992, 524);
            this.panel4.TabIndex = 1;
            this.coreBind.SetVerification(this.panel4, null);
            // 
            // ultraGrid1
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid1, null);
            this.ultraGrid1.DataMember = "计量数据";
            this.ultraGrid1.DataSource = this.dataSet1;
            appearance27.BackColor = System.Drawing.Color.White;
            appearance27.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid1.DisplayLayout.Appearance = appearance27;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.RowLayoutColumnInfo.OriginX = 0;
            ultraGridColumn1.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(110, 0);
            ultraGridColumn1.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn1.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn1.Width = 36;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.RowLayoutColumnInfo.OriginX = 6;
            ultraGridColumn2.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(75, 0);
            ultraGridColumn2.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn2.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.RowLayoutColumnInfo.OriginX = 2;
            ultraGridColumn3.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn3.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(72, 0);
            ultraGridColumn3.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn3.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.RowLayoutColumnInfo.OriginX = 4;
            ultraGridColumn4.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(98, 0);
            ultraGridColumn4.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn4.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.RowLayoutColumnInfo.OriginX = 12;
            ultraGridColumn5.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(112, 0);
            ultraGridColumn5.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn5.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.RowLayoutColumnInfo.OriginX = 14;
            ultraGridColumn6.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn6.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(104, 0);
            ultraGridColumn6.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn6.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.RowLayoutColumnInfo.OriginX = 16;
            ultraGridColumn7.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(106, 0);
            ultraGridColumn7.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn7.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.RowLayoutColumnInfo.OriginX = 8;
            ultraGridColumn8.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(76, 0);
            ultraGridColumn8.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn8.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.RowLayoutColumnInfo.OriginX = 10;
            ultraGridColumn9.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn9.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(79, 0);
            ultraGridColumn9.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn9.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.RowLayoutColumnInfo.OriginX = 18;
            ultraGridColumn10.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn10.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(84, 0);
            ultraGridColumn10.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn10.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.RowLayoutColumnInfo.OriginX = 20;
            ultraGridColumn11.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn11.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(85, 0);
            ultraGridColumn11.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn11.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.RowLayoutColumnInfo.OriginX = 22;
            ultraGridColumn12.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn12.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(85, 0);
            ultraGridColumn12.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn12.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn13.RowLayoutColumnInfo.OriginX = 24;
            ultraGridColumn13.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn13.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn13.RowLayoutColumnInfo.SpanY = 2;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13});
            ultraGridBand1.RowLayoutStyle = Infragistics.Win.UltraWinGrid.RowLayoutStyle.GroupLayout;
            summarySettings1.DisplayFormat = "合计:{0}千克";
            summarySettings1.GroupBySummaryValueAppearance = appearance12;
            summarySettings2.DisplayFormat = "合计:{0}罐";
            summarySettings2.GroupBySummaryValueAppearance = appearance13;
            ultraGridBand1.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings1,
            summarySettings2});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.GroupByBox.Prompt = "拖动列标题到此，按该列进行分组。";
            this.ultraGrid1.DisplayLayout.InterBandSpacing = 10;
            appearance30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            this.ultraGrid1.DisplayLayout.Override.ActiveRowAppearance = appearance30;
            appearance31.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance31;
            this.ultraGrid1.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance32.BackColor2 = System.Drawing.Color.White;
            appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance32.FontData.SizeInPoints = 11F;
            appearance32.ForeColor = System.Drawing.Color.Black;
            appearance32.TextHAlignAsString = "Center";
            appearance32.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance32;
            appearance33.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance33;
            appearance34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance34.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorAppearance = appearance34;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid1.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance35.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance35.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid1.DisplayLayout.Override.SelectedRowAppearance = appearance35;
            this.ultraGrid1.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGrid1.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(992, 524);
            this.ultraGrid1.TabIndex = 0;
            this.coreBind.SetVerification(this.ultraGrid1, null);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ugbEdit);
            this.coreBind.SetDatabasecommand(this.panel3, null);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 104);
            this.panel3.TabIndex = 0;
            this.coreBind.SetVerification(this.panel3, null);
            // 
            // ugbEdit
            // 
            this.ugbEdit.Controls.Add(this.txtGh);
            this.ugbEdit.Controls.Add(this.label4);
            this.ugbEdit.Controls.Add(this.txtLh);
            this.ugbEdit.Controls.Add(this.label3);
            this.ugbEdit.Controls.Add(this.label1);
            this.ugbEdit.Controls.Add(this.qDteEnd);
            this.ugbEdit.Controls.Add(this.qDteBegin);
            this.coreBind.SetDatabasecommand(this.ugbEdit, null);
            this.ugbEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugbEdit.Location = new System.Drawing.Point(0, 0);
            this.ugbEdit.Name = "ugbEdit";
            this.ugbEdit.Size = new System.Drawing.Size(992, 104);
            this.ugbEdit.TabIndex = 0;
            this.ugbEdit.Text = "查询条件区";
            this.coreBind.SetVerification(this.ugbEdit, null);
            this.ugbEdit.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // txtGh
            // 
            this.coreBind.SetDatabasecommand(this.txtGh, null);
            this.txtGh.Location = new System.Drawing.Point(274, 75);
            this.txtGh.Name = "txtGh";
            this.txtGh.Size = new System.Drawing.Size(100, 21);
            this.txtGh.TabIndex = 8;
            this.coreBind.SetVerification(this.txtGh, null);
            this.txtGh.Leave += new System.EventHandler(this.txtGh_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label4, null);
            this.label4.Location = new System.Drawing.Point(211, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "罐号";
            this.coreBind.SetVerification(this.label4, null);
            // 
            // txtLh
            // 
            this.coreBind.SetDatabasecommand(this.txtLh, null);
            this.txtLh.Location = new System.Drawing.Point(92, 75);
            this.txtLh.Name = "txtLh";
            this.txtLh.Size = new System.Drawing.Size(100, 21);
            this.txtLh.TabIndex = 6;
            this.coreBind.SetVerification(this.txtLh, null);
            this.txtLh.Leave += new System.EventHandler(this.txtLh_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label3, null);
            this.label3.Location = new System.Drawing.Point(12, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "炉号";
            this.coreBind.SetVerification(this.label3, null);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label1, null);
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "计量时间";
            this.coreBind.SetVerification(this.label1, null);
            // 
            // qDteEnd
            // 
            this.coreBind.SetDatabasecommand(this.qDteEnd, null);
            this.qDteEnd.Location = new System.Drawing.Point(274, 34);
            this.qDteEnd.Name = "qDteEnd";
            this.qDteEnd.Size = new System.Drawing.Size(149, 21);
            this.qDteEnd.TabIndex = 1;
            this.qDteEnd.Value = new System.DateTime(2012, 4, 27, 0, 0, 0, 0);
            this.coreBind.SetVerification(this.qDteEnd, null);
            // 
            // qDteBegin
            // 
            this.coreBind.SetDatabasecommand(this.qDteBegin, null);
            this.qDteBegin.Location = new System.Drawing.Point(90, 34);
            this.qDteBegin.Name = "qDteBegin";
            this.qDteBegin.Size = new System.Drawing.Size(150, 21);
            this.qDteBegin.TabIndex = 0;
            this.qDteBegin.Value = new System.DateTime(2012, 4, 27, 0, 0, 0, 0);
            this.coreBind.SetVerification(this.qDteBegin, null);
            // 
            // DataManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 654);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.coreBind.SetDatabasecommand(this, null);
            this.KeyPreview = true;
            this.Name = "DataManage";
            this.Text = "数据管理";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.DataManage_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uToolBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugbEdit)).EndInit();
            this.ugbEdit.ResumeLayout(false);
            this.ugbEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel1_Fill_Panel;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager uToolBar;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Windows.Forms.Panel panel2;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
        private System.Data.DataColumn dataColumn30;
        private System.Data.DataColumn dataColumn31;
        private System.Data.DataColumn dataColumn32;
        private System.Data.DataColumn dataColumn33;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.Misc.UltraGroupBox ugbEdit;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Windows.Forms.DateTimePicker qDteEnd;
        private System.Windows.Forms.DateTimePicker qDteBegin;
        private System.Windows.Forms.TextBox txtLh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGh;
        private System.Windows.Forms.Label label4;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
    }
}