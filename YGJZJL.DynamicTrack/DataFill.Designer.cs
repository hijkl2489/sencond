namespace YGJZJL.DynamicTrack
{
    partial class DataFill
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
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("计量数据", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STOVESEATNO", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STOVENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_GROSSWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_TAREWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDERSTROENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVESTORE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_NETWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVEFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_RECEIVETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_AUDITOR");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_AUDITTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_UPLOADFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_TOCENTERTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_ACCOUNTDATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_TESTIFYDATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROSSTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_TARETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_YKL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CHK");
            Infragistics.Win.UltraWinGrid.UltraGridGroup ultraGridGroup1 = new Infragistics.Win.UltraWinGrid.UltraGridGroup("重量", 2583985);
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridGroup ultraGridGroup2 = new Infragistics.Win.UltraWinGrid.UltraGridGroup("验收", 2583986);
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings1 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Count, null, "FS_POTNO", 3, true, "计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FS_STOVENO", 2, true);
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings2 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "FN_NETWEIGHT", 8, true, "计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FN_NETWEIGHT", 8, true);
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool1 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("开始日期");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool2 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("结束日期");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool10 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("炉座号");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool8 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("收货单位");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ToExcel");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool3 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("全选");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("find");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("数据回填");
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Edit");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("UpSap");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ToExcel");
            Infragistics.Win.UltraWinToolbars.PopupControlContainerTool popupControlContainerTool2 = new Infragistics.Win.UltraWinToolbars.PopupControlContainerTool("PopupControlContainerTool1");
            Infragistics.Win.UltraWinToolbars.PopupGalleryTool popupGalleryTool2 = new Infragistics.Win.UltraWinToolbars.PopupGalleryTool("PopupGalleryTool1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("find");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("数据回填");
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool4 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("开始日期");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool5 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("结束日期");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool6 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("已验收");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool7 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("全选");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool11 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("炉座号");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool9 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("收货单位");
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cbSH = new System.Windows.Forms.ComboBox();
            this.cbFH = new System.Windows.Forms.ComboBox();
            this.cbEdit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLH = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uGridData = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dataColumn25 = new System.Data.DataColumn();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataTable4 = new System.Data.DataTable();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataTable5 = new System.Data.DataTable();
            this.dataColumn28 = new System.Data.DataColumn();
            this.dataColumn29 = new System.Data.DataColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbQuerySH = new System.Windows.Forms.ComboBox();
            this.cbquery = new System.Windows.Forms.ComboBox();
            this.chBall = new System.Windows.Forms.CheckBox();
            this.panel3_Fill_Panel = new System.Windows.Forms.Panel();
            this.qDteEnd = new System.Windows.Forms.DateTimePicker();
            this._panel3_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.uToolBar = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._panel3_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel3_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel3_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.qDteBegin = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uGridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uToolBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.coreBind.SetDatabasecommand(this.panel1, null);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 654);
            this.panel1.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1, null);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ultraGroupBox1);
            this.coreBind.SetDatabasecommand(this.panel4, null);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 504);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(992, 150);
            this.panel4.TabIndex = 2;
            this.coreBind.SetVerification(this.panel4, null);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.cbSH);
            this.ultraGroupBox1.Controls.Add(this.cbFH);
            this.ultraGroupBox1.Controls.Add(this.cbEdit);
            this.ultraGroupBox1.Controls.Add(this.label6);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.txtLH);
            this.ultraGroupBox1.Controls.Add(this.label5);
            this.ultraGroupBox1.Controls.Add(this.label4);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox1, null);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(992, 150);
            this.ultraGroupBox1.TabIndex = 1;
            this.ultraGroupBox1.Text = "数据编辑区";
            this.coreBind.SetVerification(this.ultraGroupBox1, null);
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // cbSH
            // 
            this.coreBind.SetDatabasecommand(this.cbSH, null);
            this.cbSH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSH.FormattingEnabled = true;
            this.cbSH.Items.AddRange(new object[] {
            "--请选择--",
            "铸铁厂",
            "炼钢厂"});
            this.cbSH.Location = new System.Drawing.Point(844, 29);
            this.cbSH.Name = "cbSH";
            this.cbSH.Size = new System.Drawing.Size(121, 20);
            this.cbSH.TabIndex = 652;
            this.coreBind.SetVerification(this.cbSH, null);
            this.cbSH.SelectedIndexChanged += new System.EventHandler(this.cbSH_SelectedIndexChanged);
            // 
            // cbFH
            // 
            this.coreBind.SetDatabasecommand(this.cbFH, null);
            this.cbFH.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFH.FormattingEnabled = true;
            this.cbFH.Items.AddRange(new object[] {
            "--请选择--",
            "1#高炉",
            "2#高炉",
            "3#高炉"});
            this.cbFH.Location = new System.Drawing.Point(594, 29);
            this.cbFH.Name = "cbFH";
            this.cbFH.Size = new System.Drawing.Size(121, 20);
            this.cbFH.TabIndex = 651;
            this.coreBind.SetVerification(this.cbFH, null);
            this.cbFH.SelectedIndexChanged += new System.EventHandler(this.cbFH_SelectedIndexChanged);
            // 
            // cbEdit
            // 
            this.coreBind.SetDatabasecommand(this.cbEdit, null);
            this.cbEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEdit.FormattingEnabled = true;
            this.cbEdit.Location = new System.Drawing.Point(87, 32);
            this.cbEdit.Name = "cbEdit";
            this.cbEdit.Size = new System.Drawing.Size(121, 20);
            this.cbEdit.TabIndex = 650;
            this.coreBind.SetVerification(this.cbEdit, null);
            this.cbEdit.SelectedIndexChanged += new System.EventHandler(this.cbEdit_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label6, null);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(762, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 647;
            this.label6.Text = "收货单位";
            this.coreBind.SetVerification(this.label6, null);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label2, null);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(506, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 646;
            this.label2.Text = "发货单位";
            this.coreBind.SetVerification(this.label2, null);
            // 
            // txtLH
            // 
            this.coreBind.SetDatabasecommand(this.txtLH, null);
            this.txtLH.Location = new System.Drawing.Point(342, 29);
            this.txtLH.Name = "txtLH";
            this.txtLH.Size = new System.Drawing.Size(113, 21);
            this.txtLH.TabIndex = 645;
            this.coreBind.SetVerification(this.txtLH, null);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label5, null);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(262, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 643;
            this.label5.Text = "炉号";
            this.coreBind.SetVerification(this.label5, null);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label4, null);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(6, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 642;
            this.label4.Text = "炉座号";
            this.coreBind.SetVerification(this.label4, null);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.uGridData);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.qDteBegin);
            this.coreBind.SetDatabasecommand(this.panel2, null);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 504);
            this.panel2.TabIndex = 1;
            this.coreBind.SetVerification(this.panel2, null);
            // 
            // uGridData
            // 
            this.coreBind.SetDatabasecommand(this.uGridData, null);
            this.uGridData.DataMember = "计量数据";
            this.uGridData.DataSource = this.dataSet1;
            appearance29.BackColor = System.Drawing.Color.White;
            appearance29.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.uGridData.DisplayLayout.Appearance = appearance29;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.RowLayoutColumnInfo.OriginX = 2;
            ultraGridColumn1.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(97, 0);
            ultraGridColumn1.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn1.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn1.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn1.Width = 114;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.RowLayoutColumnInfo.OriginX = 14;
            ultraGridColumn2.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(76, 0);
            ultraGridColumn2.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn2.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn2.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.RowLayoutColumnInfo.OriginX = 16;
            ultraGridColumn3.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn3.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(108, 0);
            ultraGridColumn3.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn3.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn3.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.RowLayoutColumnInfo.OriginX = 18;
            ultraGridColumn4.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(61, 0);
            ultraGridColumn4.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn4.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn4.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.RowLayoutColumnInfo.OriginX = 0;
            ultraGridColumn5.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn5.RowLayoutColumnInfo.ParentGroupIndex = 0;
            ultraGridColumn5.RowLayoutColumnInfo.ParentGroupKey = "重量";
            ultraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(73, 0);
            ultraGridColumn5.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 27);
            ultraGridColumn5.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn5.RowLayoutColumnInfo.SpanY = 3;
            ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn6.RowLayoutColumnInfo.OriginX = 2;
            ultraGridColumn6.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn6.RowLayoutColumnInfo.ParentGroupIndex = 0;
            ultraGridColumn6.RowLayoutColumnInfo.ParentGroupKey = "重量";
            ultraGridColumn6.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(76, 0);
            ultraGridColumn6.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 27);
            ultraGridColumn6.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn6.RowLayoutColumnInfo.SpanY = 3;
            ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.RowLayoutColumnInfo.OriginX = 20;
            ultraGridColumn7.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(85, 0);
            ultraGridColumn7.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn7.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn7.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn8.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.RowLayoutColumnInfo.OriginX = 22;
            ultraGridColumn8.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(99, 0);
            ultraGridColumn8.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn8.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn8.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.RowLayoutColumnInfo.OriginX = 4;
            ultraGridColumn9.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn9.RowLayoutColumnInfo.ParentGroupIndex = 0;
            ultraGridColumn9.RowLayoutColumnInfo.ParentGroupKey = "重量";
            ultraGridColumn9.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(78, 0);
            ultraGridColumn9.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 27);
            ultraGridColumn9.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn9.RowLayoutColumnInfo.SpanY = 3;
            ultraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn10.RowLayoutColumnInfo.OriginX = 0;
            ultraGridColumn10.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn10.RowLayoutColumnInfo.ParentGroupIndex = 1;
            ultraGridColumn10.RowLayoutColumnInfo.ParentGroupKey = "验收";
            ultraGridColumn10.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(85, 0);
            ultraGridColumn10.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 27);
            ultraGridColumn10.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn10.RowLayoutColumnInfo.SpanY = 3;
            ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.RowLayoutColumnInfo.OriginX = 2;
            ultraGridColumn11.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn11.RowLayoutColumnInfo.ParentGroupIndex = 1;
            ultraGridColumn11.RowLayoutColumnInfo.ParentGroupKey = "验收";
            ultraGridColumn11.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(85, 0);
            ultraGridColumn11.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 27);
            ultraGridColumn11.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn11.RowLayoutColumnInfo.SpanY = 3;
            ultraGridColumn12.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn12.Header.VisiblePosition = 12;
            ultraGridColumn12.RowLayoutColumnInfo.OriginX = 4;
            ultraGridColumn12.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn12.RowLayoutColumnInfo.ParentGroupIndex = 1;
            ultraGridColumn12.RowLayoutColumnInfo.ParentGroupKey = "验收";
            ultraGridColumn12.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 27);
            ultraGridColumn12.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn12.RowLayoutColumnInfo.SpanY = 3;
            ultraGridColumn13.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn13.Header.VisiblePosition = 13;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn13.RowLayoutColumnInfo.OriginX = 22;
            ultraGridColumn13.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn13.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn13.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn14.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn14.Header.VisiblePosition = 14;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn14.RowLayoutColumnInfo.OriginX = 24;
            ultraGridColumn14.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn14.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn14.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn15.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn15.Header.VisiblePosition = 15;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn15.RowLayoutColumnInfo.OriginX = 26;
            ultraGridColumn15.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn15.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn15.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn16.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn16.Header.VisiblePosition = 16;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn16.RowLayoutColumnInfo.OriginX = 28;
            ultraGridColumn16.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn16.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn16.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn17.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn17.Header.VisiblePosition = 17;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn17.RowLayoutColumnInfo.OriginX = 30;
            ultraGridColumn17.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn17.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn17.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn18.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn18.Header.VisiblePosition = 18;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn18.RowLayoutColumnInfo.OriginX = 32;
            ultraGridColumn18.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn18.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn18.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn19.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn19.Header.VisiblePosition = 19;
            ultraGridColumn19.RowLayoutColumnInfo.OriginX = 4;
            ultraGridColumn19.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn19.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn19.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn19.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn20.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn20.Header.VisiblePosition = 20;
            ultraGridColumn20.RowLayoutColumnInfo.OriginX = 6;
            ultraGridColumn20.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn20.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn20.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn20.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn21.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn21.Header.VisiblePosition = 21;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn21.RowLayoutColumnInfo.OriginX = 6;
            ultraGridColumn21.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn21.RowLayoutColumnInfo.ParentGroupIndex = 0;
            ultraGridColumn21.RowLayoutColumnInfo.ParentGroupKey = "重量";
            ultraGridColumn21.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(98, 0);
            ultraGridColumn21.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 27);
            ultraGridColumn21.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn21.RowLayoutColumnInfo.SpanY = 3;
            ultraGridColumn22.Header.VisiblePosition = 5;
            ultraGridColumn22.RowLayoutColumnInfo.OriginX = 0;
            ultraGridColumn22.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn22.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(18, 0);
            ultraGridColumn22.RowLayoutColumnInfo.PreferredLabelSize = new System.Drawing.Size(0, 50);
            ultraGridColumn22.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn22.RowLayoutColumnInfo.SpanY = 4;
            ultraGridColumn22.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
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
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22});
            appearance1.TextHAlignAsString = "Center";
            ultraGridGroup1.CellAppearance = appearance1;
            ultraGridGroup1.Key = "重量";
            ultraGridGroup1.RowLayoutGroupInfo.LabelSpan = 1;
            ultraGridGroup1.RowLayoutGroupInfo.OriginX = 8;
            ultraGridGroup1.RowLayoutGroupInfo.OriginY = 0;
            ultraGridGroup1.RowLayoutGroupInfo.SpanX = 6;
            ultraGridGroup1.RowLayoutGroupInfo.SpanY = 4;
            appearance2.TextHAlignAsString = "Center";
            ultraGridGroup2.CellAppearance = appearance2;
            ultraGridGroup2.Key = "验收";
            ultraGridGroup2.RowLayoutGroupInfo.LabelSpan = 1;
            ultraGridGroup2.RowLayoutGroupInfo.OriginX = 24;
            ultraGridGroup2.RowLayoutGroupInfo.OriginY = 0;
            ultraGridGroup2.RowLayoutGroupInfo.SpanX = 6;
            ultraGridGroup2.RowLayoutGroupInfo.SpanY = 4;
            ultraGridBand1.Groups.AddRange(new Infragistics.Win.UltraWinGrid.UltraGridGroup[] {
            ultraGridGroup1,
            ultraGridGroup2});
            ultraGridBand1.RowLayoutStyle = Infragistics.Win.UltraWinGrid.RowLayoutStyle.GroupLayout;
            summarySettings1.DisplayFormat = "合计:{0}罐";
            summarySettings1.GroupBySummaryValueAppearance = appearance3;
            summarySettings2.DisplayFormat = "合计:{0}千克";
            summarySettings2.GroupBySummaryValueAppearance = appearance4;
            ultraGridBand1.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings1,
            summarySettings2});
            this.uGridData.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.uGridData.DisplayLayout.GroupByBox.Prompt = "拖动列标题到此，按该列进行分组。";
            this.uGridData.DisplayLayout.InterBandSpacing = 10;
            appearance34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            this.uGridData.DisplayLayout.Override.ActiveRowAppearance = appearance34;
            appearance35.BackColor = System.Drawing.Color.Transparent;
            this.uGridData.DisplayLayout.Override.CardAreaAppearance = appearance35;
            this.uGridData.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance36.BackColor2 = System.Drawing.Color.White;
            appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance36.FontData.SizeInPoints = 11F;
            appearance36.ForeColor = System.Drawing.Color.Black;
            appearance36.TextHAlignAsString = "Center";
            appearance36.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.uGridData.DisplayLayout.Override.HeaderAppearance = appearance36;
            appearance37.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.uGridData.DisplayLayout.Override.RowAppearance = appearance37;
            appearance38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance38.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.uGridData.DisplayLayout.Override.RowSelectorAppearance = appearance38;
            this.uGridData.DisplayLayout.Override.RowSelectorWidth = 12;
            this.uGridData.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance39.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance39.ForeColor = System.Drawing.Color.Black;
            this.uGridData.DisplayLayout.Override.SelectedRowAppearance = appearance39;
            this.uGridData.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.uGridData.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.uGridData.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.uGridData.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.uGridData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uGridData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uGridData.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.uGridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uGridData.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uGridData.Location = new System.Drawing.Point(0, 26);
            this.uGridData.Name = "uGridData";
            this.uGridData.Size = new System.Drawing.Size(992, 478);
            this.uGridData.TabIndex = 17;
            this.coreBind.SetVerification(this.uGridData, null);
            this.uGridData.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.uGridData_CellChange);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3,
            this.dataTable4,
            this.dataTable5});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn2,
            this.dataColumn9,
            this.dataColumn1,
            this.dataColumn3,
            this.dataColumn10,
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24,
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn4});
            this.dataTable1.TableName = "计量数据";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "序号";
            this.dataColumn2.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn9
            // 
            this.dataColumn9.Caption = "炉座号";
            this.dataColumn9.ColumnName = "FS_STOVESEATNO";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "炉号";
            this.dataColumn1.ColumnName = "FS_STOVENO";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "罐号";
            this.dataColumn3.ColumnName = "FS_POTNO";
            // 
            // dataColumn10
            // 
            this.dataColumn10.Caption = "毛重";
            this.dataColumn10.ColumnName = "FN_GROSSWEIGHT";
            // 
            // dataColumn12
            // 
            this.dataColumn12.Caption = "皮重";
            this.dataColumn12.ColumnName = "FN_TAREWEIGHT";
            // 
            // dataColumn13
            // 
            this.dataColumn13.Caption = "发货单位";
            this.dataColumn13.ColumnName = "FS_SENDERSTROENO";
            // 
            // dataColumn14
            // 
            this.dataColumn14.Caption = "收货单位";
            this.dataColumn14.ColumnName = "FS_RECEIVESTORE";
            // 
            // dataColumn18
            // 
            this.dataColumn18.Caption = "净重";
            this.dataColumn18.ColumnName = "FN_NETWEIGHT";
            // 
            // dataColumn19
            // 
            this.dataColumn19.Caption = "状态";
            this.dataColumn19.ColumnName = "FS_RECEIVEFLAG";
            // 
            // dataColumn20
            // 
            this.dataColumn20.Caption = "验收人";
            this.dataColumn20.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn21
            // 
            this.dataColumn21.Caption = "验收时间";
            this.dataColumn21.ColumnName = "FD_RECEIVETIME";
            // 
            // dataColumn22
            // 
            this.dataColumn22.Caption = "审核员";
            this.dataColumn22.ColumnName = "FS_AUDITOR";
            // 
            // dataColumn23
            // 
            this.dataColumn23.Caption = "审核时间";
            this.dataColumn23.ColumnName = "FD_AUDITTIME";
            // 
            // dataColumn24
            // 
            this.dataColumn24.Caption = "上传标志";
            this.dataColumn24.ColumnName = "FS_UPLOADFLAG";
            // 
            // dataColumn25
            // 
            this.dataColumn25.Caption = "上传时间";
            this.dataColumn25.ColumnName = "FD_TOCENTERTIME";
            // 
            // dataColumn26
            // 
            this.dataColumn26.Caption = "统计日期";
            this.dataColumn26.ColumnName = "FS_ACCOUNTDATE";
            // 
            // dataColumn27
            // 
            this.dataColumn27.Caption = "上传日期";
            this.dataColumn27.ColumnName = "FD_TESTIFYDATE";
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
            this.dataColumn32.Caption = "应扣量";
            this.dataColumn32.ColumnName = "FN_YKL";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "选择";
            this.dataColumn4.ColumnName = "CHK";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn6});
            this.dataTable2.TableName = "炉座信息（查询）";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "炉座编号";
            this.dataColumn5.ColumnName = "FS_STOVESEATNO";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "炉座名称";
            this.dataColumn6.ColumnName = "FS_STOVESEATNAME";
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn11});
            this.dataTable3.TableName = "发货单位";
            // 
            // dataColumn7
            // 
            this.dataColumn7.Caption = "发货单位代码";
            this.dataColumn7.ColumnName = "FS_GY";
            // 
            // dataColumn8
            // 
            this.dataColumn8.Caption = "发货单位名称";
            this.dataColumn8.ColumnName = "FS_SUPPLIERNAME";
            // 
            // dataColumn11
            // 
            this.dataColumn11.Caption = "来源";
            this.dataColumn11.ColumnName = "FS_FROM";
            // 
            // dataTable4
            // 
            this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17});
            this.dataTable4.TableName = "收货单位";
            // 
            // dataColumn15
            // 
            this.dataColumn15.Caption = "收货单位代码";
            this.dataColumn15.ColumnName = "FS_SH";
            // 
            // dataColumn16
            // 
            this.dataColumn16.Caption = "收货单位名称";
            this.dataColumn16.ColumnName = "FS_MEMO";
            // 
            // dataColumn17
            // 
            this.dataColumn17.Caption = "来源";
            this.dataColumn17.ColumnName = "FS_FROM";
            // 
            // dataTable5
            // 
            this.dataTable5.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn28,
            this.dataColumn29});
            this.dataTable5.TableName = "炉座信息（编辑）";
            // 
            // dataColumn28
            // 
            this.dataColumn28.Caption = "炉座号";
            this.dataColumn28.ColumnName = "FS_STOVESEATNO";
            // 
            // dataColumn29
            // 
            this.dataColumn29.Caption = "炉座名称";
            this.dataColumn29.ColumnName = "FS_STOVESEATNAME";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbQuerySH);
            this.panel3.Controls.Add(this.cbquery);
            this.panel3.Controls.Add(this.chBall);
            this.panel3.Controls.Add(this.panel3_Fill_Panel);
            this.panel3.Controls.Add(this.qDteEnd);
            this.panel3.Controls.Add(this._panel3_Toolbars_Dock_Area_Left);
            this.panel3.Controls.Add(this._panel3_Toolbars_Dock_Area_Right);
            this.panel3.Controls.Add(this._panel3_Toolbars_Dock_Area_Top);
            this.panel3.Controls.Add(this._panel3_Toolbars_Dock_Area_Bottom);
            this.coreBind.SetDatabasecommand(this.panel3, null);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 26);
            this.panel3.TabIndex = 2;
            this.coreBind.SetVerification(this.panel3, null);
            // 
            // cbQuerySH
            // 
            this.coreBind.SetDatabasecommand(this.cbQuerySH, null);
            this.cbQuerySH.FormattingEnabled = true;
            this.cbQuerySH.Items.AddRange(new object[] {
            "--全部--",
            "铸铁厂",
            "炼钢厂"});
            this.cbQuerySH.Location = new System.Drawing.Point(513, 3);
            this.cbQuerySH.Name = "cbQuerySH";
            this.cbQuerySH.Size = new System.Drawing.Size(71, 20);
            this.cbQuerySH.TabIndex = 17;
            this.coreBind.SetVerification(this.cbQuerySH, null);
            this.cbQuerySH.SelectedIndexChanged += new System.EventHandler(this.cbQuerySH_SelectedIndexChanged);
            // 
            // cbquery
            // 
            this.coreBind.SetDatabasecommand(this.cbquery, null);
            this.cbquery.FormattingEnabled = true;
            this.cbquery.Location = new System.Drawing.Point(382, 3);
            this.cbquery.Name = "cbquery";
            this.cbquery.Size = new System.Drawing.Size(72, 20);
            this.cbquery.TabIndex = 12;
            this.coreBind.SetVerification(this.cbquery, null);
            this.cbquery.SelectedIndexChanged += new System.EventHandler(this.cbquery_SelectedIndexChanged);
            // 
            // chBall
            // 
            this.chBall.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.chBall, null);
            this.chBall.Location = new System.Drawing.Point(585, 6);
            this.chBall.Name = "chBall";
            this.chBall.Size = new System.Drawing.Size(48, 16);
            this.chBall.TabIndex = 7;
            this.chBall.Text = "全选";
            this.chBall.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.chBall, null);
            this.chBall.CheckedChanged += new System.EventHandler(this.chBall_CheckedChanged);
            // 
            // panel3_Fill_Panel
            // 
            this.panel3_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.panel3_Fill_Panel, null);
            this.panel3_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3_Fill_Panel.Location = new System.Drawing.Point(0, 28);
            this.panel3_Fill_Panel.Name = "panel3_Fill_Panel";
            this.panel3_Fill_Panel.Size = new System.Drawing.Size(992, 0);
            this.panel3_Fill_Panel.TabIndex = 0;
            this.coreBind.SetVerification(this.panel3_Fill_Panel, null);
            // 
            // qDteEnd
            // 
            this.qDteEnd.CustomFormat = "";
            this.coreBind.SetDatabasecommand(this.qDteEnd, null);
            this.qDteEnd.Location = new System.Drawing.Point(226, 3);
            this.qDteEnd.Name = "qDteEnd";
            this.qDteEnd.Size = new System.Drawing.Size(107, 21);
            this.qDteEnd.TabIndex = 2;
            this.coreBind.SetVerification(this.qDteEnd, null);
            // 
            // _panel3_Toolbars_Dock_Area_Left
            // 
            this._panel3_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel3_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel3_Toolbars_Dock_Area_Left, null);
            this._panel3_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._panel3_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel3_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 28);
            this._panel3_Toolbars_Dock_Area_Left.Name = "_panel3_Toolbars_Dock_Area_Left";
            this._panel3_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 0);
            this._panel3_Toolbars_Dock_Area_Left.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel3_Toolbars_Dock_Area_Left, null);
            // 
            // uToolBar
            // 
            this.uToolBar.DesignerFlags = 1;
            this.uToolBar.DockWithinContainer = this.panel3;
            this.uToolBar.LockToolbars = true;
            this.uToolBar.ShowFullMenusDelay = 500;
            this.uToolBar.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            controlContainerTool1.ControlName = "qDteBegin";
            controlContainerTool1.InstanceProps.Width = 166;
            controlContainerTool2.ControlName = "qDteEnd";
            controlContainerTool2.InstanceProps.Width = 166;
            controlContainerTool10.ControlName = "cbquery";
            controlContainerTool10.InstanceProps.Width = 119;
            controlContainerTool8.ControlName = "cbQuerySH";
            controlContainerTool8.InstanceProps.Width = 130;
            buttonTool6.InstanceProps.IsFirstInGroup = true;
            controlContainerTool3.ControlName = "chBall";
            buttonTool7.InstanceProps.IsFirstInGroup = true;
            appearance19.ForeColor = System.Drawing.Color.White;
            buttonTool4.InstanceProps.AppearancesLarge.PressedAppearance = appearance19;
            buttonTool4.InstanceProps.IsFirstInGroup = true;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            controlContainerTool1,
            controlContainerTool2,
            controlContainerTool10,
            controlContainerTool8,
            buttonTool6,
            controlContainerTool3,
            buttonTool7,
            buttonTool4});
            ultraToolbar1.Text = "UltraToolbar1";
            this.uToolBar.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            buttonTool1.SharedPropsInternal.Caption = "确认修改";
            buttonTool1.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool2.SharedPropsInternal.Caption = "上传SAP";
            buttonTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool3.SharedPropsInternal.Caption = "导出";
            buttonTool3.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool3.SharedPropsInternal.Visible = false;
            popupControlContainerTool2.SharedPropsInternal.Caption = "PopupControlContainerTool1";
            popupControlContainerTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            popupGalleryTool2.SharedPropsInternal.Caption = "PopupGalleryTool1";
            buttonTool8.SharedPropsInternal.Caption = "查询";
            buttonTool8.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            appearance20.ForeColor = System.Drawing.Color.White;
            buttonTool5.SharedPropsInternal.AppearancesLarge.Appearance = appearance20;
            buttonTool5.SharedPropsInternal.Caption = "数据回填";
            buttonTool5.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool4.ControlName = "qDteBegin";
            controlContainerTool4.SharedPropsInternal.Caption = "开始日期";
            controlContainerTool4.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool4.SharedPropsInternal.Width = 166;
            controlContainerTool5.ControlName = "qDteEnd";
            controlContainerTool5.SharedPropsInternal.Caption = "结束日期";
            controlContainerTool5.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool5.SharedPropsInternal.Width = 166;
            controlContainerTool6.SharedPropsInternal.Caption = "已验收";
            controlContainerTool7.ControlName = "chBall";
            controlContainerTool7.SharedPropsInternal.Caption = "全选";
            controlContainerTool11.ControlName = "cbquery";
            controlContainerTool11.SharedPropsInternal.Caption = "炉座号";
            controlContainerTool11.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool11.SharedPropsInternal.Width = 119;
            controlContainerTool9.ControlName = "cbQuerySH";
            controlContainerTool9.SharedPropsInternal.AllowMultipleInstances = true;
            controlContainerTool9.SharedPropsInternal.Caption = "收货单位";
            controlContainerTool9.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool9.SharedPropsInternal.Width = 130;
            this.uToolBar.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1,
            buttonTool2,
            buttonTool3,
            popupControlContainerTool2,
            popupGalleryTool2,
            buttonTool8,
            buttonTool5,
            controlContainerTool4,
            controlContainerTool5,
            controlContainerTool6,
            controlContainerTool7,
            controlContainerTool11,
            controlContainerTool9});
            this.uToolBar.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.uToolBar_ToolClick);
            // 
            // _panel3_Toolbars_Dock_Area_Right
            // 
            this._panel3_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel3_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel3_Toolbars_Dock_Area_Right, null);
            this._panel3_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._panel3_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel3_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(992, 28);
            this._panel3_Toolbars_Dock_Area_Right.Name = "_panel3_Toolbars_Dock_Area_Right";
            this._panel3_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 0);
            this._panel3_Toolbars_Dock_Area_Right.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel3_Toolbars_Dock_Area_Right, null);
            // 
            // _panel3_Toolbars_Dock_Area_Top
            // 
            this._panel3_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel3_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel3_Toolbars_Dock_Area_Top, null);
            this._panel3_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._panel3_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel3_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._panel3_Toolbars_Dock_Area_Top.Name = "_panel3_Toolbars_Dock_Area_Top";
            this._panel3_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(992, 28);
            this._panel3_Toolbars_Dock_Area_Top.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel3_Toolbars_Dock_Area_Top, null);
            // 
            // _panel3_Toolbars_Dock_Area_Bottom
            // 
            this._panel3_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel3_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel3_Toolbars_Dock_Area_Bottom, null);
            this._panel3_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._panel3_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel3_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 26);
            this._panel3_Toolbars_Dock_Area_Bottom.Name = "_panel3_Toolbars_Dock_Area_Bottom";
            this._panel3_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(992, 0);
            this._panel3_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.uToolBar;
            this.coreBind.SetVerification(this._panel3_Toolbars_Dock_Area_Bottom, null);
            // 
            // qDteBegin
            // 
            this.qDteBegin.CustomFormat = "";
            this.coreBind.SetDatabasecommand(this.qDteBegin, null);
            this.qDteBegin.Location = new System.Drawing.Point(71, 5);
            this.qDteBegin.Name = "qDteBegin";
            this.qDteBegin.Size = new System.Drawing.Size(107, 21);
            this.qDteBegin.TabIndex = 1;
            this.coreBind.SetVerification(this.qDteBegin, null);
            // 
            // DataFill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 654);
            this.Controls.Add(this.panel1);
            this.coreBind.SetDatabasecommand(this, null);
            this.Name = "DataFill";
            this.Text = "数据回填";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.DataFill_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uGridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uToolBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel3_Fill_Panel;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel3_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager uToolBar;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel3_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel3_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel3_Toolbars_Dock_Area_Bottom;
        private System.Windows.Forms.Panel panel4;
        private Infragistics.Win.UltraWinGrid.UltraGrid uGridData;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker qDteEnd;
        private System.Windows.Forms.DateTimePicker qDteBegin;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn12;
        private System.Data.DataColumn dataColumn13;
        private System.Data.DataColumn dataColumn14;
        private System.Data.DataColumn dataColumn18;
        private System.Data.DataColumn dataColumn19;
        private System.Data.DataColumn dataColumn20;
        private System.Data.DataColumn dataColumn21;
        private System.Data.DataColumn dataColumn22;
        private System.Data.DataColumn dataColumn23;
        private System.Data.DataColumn dataColumn24;
        private System.Data.DataColumn dataColumn25;
        private System.Data.DataColumn dataColumn26;
        private System.Data.DataColumn dataColumn27;
        private System.Data.DataColumn dataColumn30;
        private System.Data.DataColumn dataColumn31;
        private System.Data.DataColumn dataColumn32;
        private System.Data.DataColumn dataColumn4;
        private System.Windows.Forms.CheckBox chBall;
        private System.Windows.Forms.ComboBox cbquery;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Windows.Forms.ComboBox cbEdit;
        private System.Windows.Forms.ComboBox cbSH;
        private System.Windows.Forms.ComboBox cbFH;
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataTable dataTable4;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataTable dataTable5;
        private System.Data.DataColumn dataColumn28;
        private System.Data.DataColumn dataColumn29;
        private System.Windows.Forms.ComboBox cbQuerySH;
    }
}