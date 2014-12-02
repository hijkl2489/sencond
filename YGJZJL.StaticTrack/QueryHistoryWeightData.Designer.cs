namespace YGJZJL.StaticTrack
{
    partial class QueryHistoryWeightData
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("静态轨道衡二次计量数据", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIAL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDERSTORENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVERSTORENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRAINNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_GROSSWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROSSPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROSSPOINT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_GROSSTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_TAREWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TAREPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TAREPOINT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_TARETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_NETWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_YKL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROSSSHIFT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROSSGROUP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TARESHIFT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TAREGROUP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MEMO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TYPENAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRANS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIALNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_UPDATETIME");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings1 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "FN_NETWEIGHT", 14, true, "静态轨道衡二次计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FN_NETWEIGHT", 14, true);
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings2 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Count, null, "FS_WEIGHTNO", 0, true, "静态轨道衡二次计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FS_TRAINNO", 5, true);
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool5 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("ControlContainerTool1");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool6 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("至：");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool1 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("TrainForQuery");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Update");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Add");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ToExcel");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryHistoryWeightData));
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Update");
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Add");
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Delete");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool2 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("TrainForQuery");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool4 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("ControlContainerTool1");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool7 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("至：");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ToExcel");
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpBegin = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.tbTrainNo = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet2 = new System.Data.DataSet();
            this.dataTable6 = new System.Data.DataTable();
            this.dataColumn57 = new System.Data.DataColumn();
            this.dataColumn58 = new System.Data.DataColumn();
            this.dataColumn59 = new System.Data.DataColumn();
            this.dataColumn60 = new System.Data.DataColumn();
            this.dataColumn61 = new System.Data.DataColumn();
            this.dataColumn62 = new System.Data.DataColumn();
            this.dataColumn63 = new System.Data.DataColumn();
            this.dataColumn64 = new System.Data.DataColumn();
            this.dataColumn65 = new System.Data.DataColumn();
            this.dataColumn66 = new System.Data.DataColumn();
            this.dataColumn67 = new System.Data.DataColumn();
            this.dataColumn68 = new System.Data.DataColumn();
            this.dataColumn69 = new System.Data.DataColumn();
            this.dataColumn70 = new System.Data.DataColumn();
            this.dataTable7 = new System.Data.DataTable();
            this.dataColumn71 = new System.Data.DataColumn();
            this.dataColumn72 = new System.Data.DataColumn();
            this.dataColumn73 = new System.Data.DataColumn();
            this.dataColumn74 = new System.Data.DataColumn();
            this.dataColumn75 = new System.Data.DataColumn();
            this.dataColumn76 = new System.Data.DataColumn();
            this.dataColumn77 = new System.Data.DataColumn();
            this.dataColumn78 = new System.Data.DataColumn();
            this.dataColumn79 = new System.Data.DataColumn();
            this.dataColumn80 = new System.Data.DataColumn();
            this.dataColumn82 = new System.Data.DataColumn();
            this.dataColumn83 = new System.Data.DataColumn();
            this.dataColumn84 = new System.Data.DataColumn();
            this.dataColumn85 = new System.Data.DataColumn();
            this.dataColumn86 = new System.Data.DataColumn();
            this.dataColumn87 = new System.Data.DataColumn();
            this.dataColumn88 = new System.Data.DataColumn();
            this.dataColumn89 = new System.Data.DataColumn();
            this.dataColumn90 = new System.Data.DataColumn();
            this.dataColumn91 = new System.Data.DataColumn();
            this.dataColumn81 = new System.Data.DataColumn();
            this.dataColumn92 = new System.Data.DataColumn();
            this.dataTable8 = new System.Data.DataTable();
            this.dataColumn93 = new System.Data.DataColumn();
            this.dataColumn94 = new System.Data.DataColumn();
            this.dataColumn95 = new System.Data.DataColumn();
            this.dataColumn96 = new System.Data.DataColumn();
            this.dataColumn97 = new System.Data.DataColumn();
            this.dataColumn98 = new System.Data.DataColumn();
            this.dataColumn99 = new System.Data.DataColumn();
            this.dataColumn100 = new System.Data.DataColumn();
            this.dataColumn101 = new System.Data.DataColumn();
            this.dataColumn102 = new System.Data.DataColumn();
            this.dataColumn103 = new System.Data.DataColumn();
            this.dataColumn104 = new System.Data.DataColumn();
            this.dataColumn105 = new System.Data.DataColumn();
            this.dataColumn106 = new System.Data.DataColumn();
            this.dataColumn107 = new System.Data.DataColumn();
            this.dataColumn108 = new System.Data.DataColumn();
            this.dataColumn109 = new System.Data.DataColumn();
            this.dataColumn110 = new System.Data.DataColumn();
            this.dataColumn111 = new System.Data.DataColumn();
            this.dataColumn112 = new System.Data.DataColumn();
            this.dataColumn113 = new System.Data.DataColumn();
            this.dataColumn114 = new System.Data.DataColumn();
            this.dataColumn115 = new System.Data.DataColumn();
            this.dataColumn116 = new System.Data.DataColumn();
            this.dataColumn117 = new System.Data.DataColumn();
            this.dataColumn118 = new System.Data.DataColumn();
            this.dataColumn119 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this._panel1_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._panel1_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpBegin);
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.tbTrainNo);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Left);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Right);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Top);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Bottom);
            this.coreBind.SetDatabasecommand(this.panel1, null);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 666);
            this.panel1.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1, null);
            // 
            // dtpBegin
            // 
            this.coreBind.SetDatabasecommand(this.dtpBegin, null);
            this.dtpBegin.Location = new System.Drawing.Point(46, 2);
            this.dtpBegin.Name = "dtpBegin";
            this.dtpBegin.Size = new System.Drawing.Size(107, 21);
            this.dtpBegin.TabIndex = 705;
            this.coreBind.SetVerification(this.dtpBegin, null);
            // 
            // dtpEnd
            // 
            this.coreBind.SetDatabasecommand(this.dtpEnd, null);
            this.dtpEnd.Location = new System.Drawing.Point(192, 2);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(107, 21);
            this.dtpEnd.TabIndex = 697;
            this.coreBind.SetVerification(this.dtpEnd, null);
            // 
            // tbTrainNo
            // 
            this.coreBind.SetDatabasecommand(this.tbTrainNo, null);
            this.tbTrainNo.Location = new System.Drawing.Point(369, 2);
            this.tbTrainNo.Name = "tbTrainNo";
            this.tbTrainNo.Size = new System.Drawing.Size(81, 21);
            this.tbTrainNo.TabIndex = 0;
            this.coreBind.SetVerification(this.tbTrainNo, null);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ultraGroupBox2);
            this.coreBind.SetDatabasecommand(this.panel3, null);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 640);
            this.panel3.TabIndex = 1;
            this.coreBind.SetVerification(this.panel3, null);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.ultraGrid1);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox2, null);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(992, 640);
            this.ultraGroupBox2.TabIndex = 1;
            this.ultraGroupBox2.Text = "历史数据";
            this.coreBind.SetVerification(this.ultraGroupBox2, null);
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // ultraGrid1
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid1, null);
            this.ultraGrid1.DataMember = "静态轨道衡二次计量数据";
            this.ultraGrid1.DataSource = this.dataSet2;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid1.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 12;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 13;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 94;
            ultraGridColumn4.Header.VisiblePosition = 6;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 9;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn6.Header.VisiblePosition = 1;
            ultraGridColumn6.Width = 85;
            ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridColumn8.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn8.Header.VisiblePosition = 14;
            ultraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn9.Header.VisiblePosition = 15;
            ultraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn10.Header.VisiblePosition = 16;
            ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn11.Header.VisiblePosition = 4;
            ultraGridColumn12.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn12.Header.VisiblePosition = 17;
            ultraGridColumn13.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn13.Header.VisiblePosition = 18;
            ultraGridColumn14.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn14.Header.VisiblePosition = 19;
            ultraGridColumn15.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn15.Header.VisiblePosition = 5;
            ultraGridColumn16.Header.VisiblePosition = 20;
            ultraGridColumn17.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn17.Header.VisiblePosition = 21;
            ultraGridColumn18.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn18.Header.VisiblePosition = 22;
            ultraGridColumn19.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn19.Header.VisiblePosition = 23;
            ultraGridColumn20.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn20.Header.VisiblePosition = 24;
            ultraGridColumn21.Header.VisiblePosition = 25;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn22.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            ultraGridColumn22.Header.VisiblePosition = 11;
            ultraGridColumn23.Header.VisiblePosition = 27;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 8;
            ultraGridColumn25.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn25.Header.VisiblePosition = 2;
            ultraGridColumn26.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn26.Header.VisiblePosition = 7;
            ultraGridColumn27.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn27.Header.VisiblePosition = 10;
            ultraGridColumn28.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn28.Header.VisiblePosition = 26;
            ultraGridColumn28.Hidden = true;
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
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28});
            summarySettings1.DisplayFormat = "总净重：{0}吨";
            summarySettings1.GroupBySummaryValueAppearance = appearance9;
            summarySettings2.DisplayFormat = "累计{0}车";
            summarySettings2.GroupBySummaryValueAppearance = appearance10;
            ultraGridBand1.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings1,
            summarySettings2});
            ultraGridBand1.SummaryFooterCaption = "累计：";
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid1.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance4;
            this.ultraGrid1.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.ultraGrid1.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance5.ForeColor = System.Drawing.Color.White;
            appearance5.TextHAlignAsString = "Left";
            appearance5.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance5;
            appearance6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance6;
            appearance7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance7.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorAppearance = appearance7;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid1.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(169)))), ((int)(((byte)(226)))));
            appearance8.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance8.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid1.DisplayLayout.Override.SelectedRowAppearance = appearance8;
            this.ultraGrid1.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid1.Location = new System.Drawing.Point(3, 18);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(986, 619);
            this.ultraGrid1.TabIndex = 0;
            this.coreBind.SetVerification(this.ultraGrid1, null);
            // 
            // dataSet2
            // 
            this.dataSet2.DataSetName = "NewDataSet";
            this.dataSet2.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable6,
            this.dataTable7,
            this.dataTable8});
            // 
            // dataTable6
            // 
            this.dataTable6.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn57,
            this.dataColumn58,
            this.dataColumn59,
            this.dataColumn60,
            this.dataColumn61,
            this.dataColumn62,
            this.dataColumn63,
            this.dataColumn64,
            this.dataColumn65,
            this.dataColumn66,
            this.dataColumn67,
            this.dataColumn68,
            this.dataColumn69,
            this.dataColumn70});
            this.dataTable6.TableName = "静态轨道衡预报表";
            // 
            // dataColumn57
            // 
            this.dataColumn57.Caption = "操作编号";
            this.dataColumn57.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn58
            // 
            this.dataColumn58.Caption = "物料名称代码";
            this.dataColumn58.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn59
            // 
            this.dataColumn59.Caption = "流向";
            this.dataColumn59.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn60
            // 
            this.dataColumn60.Caption = "发货单位代码";
            this.dataColumn60.ColumnName = "FS_SENDERSTROENO";
            // 
            // dataColumn61
            // 
            this.dataColumn61.Caption = "收货单位代码";
            this.dataColumn61.ColumnName = "FS_RECEIVESTORENO";
            // 
            // dataColumn62
            // 
            this.dataColumn62.Caption = "车号";
            this.dataColumn62.ColumnName = "FS_TRAINNO";
            // 
            // dataColumn63
            // 
            this.dataColumn63.Caption = "计量点";
            this.dataColumn63.ColumnName = "FS_WEIGHTPOINT";
            // 
            // dataColumn64
            // 
            this.dataColumn64.Caption = "录入部门";
            this.dataColumn64.ColumnName = "FS_DEPARTMENT";
            // 
            // dataColumn65
            // 
            this.dataColumn65.Caption = "录入员";
            this.dataColumn65.ColumnName = "FS_USER";
            // 
            // dataColumn66
            // 
            this.dataColumn66.Caption = "录入时间";
            this.dataColumn66.ColumnName = "FD_TIMES";
            // 
            // dataColumn67
            // 
            this.dataColumn67.Caption = "物料名称";
            this.dataColumn67.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn68
            // 
            this.dataColumn68.Caption = "发货单位";
            this.dataColumn68.ColumnName = "FS_SENDER";
            // 
            // dataColumn69
            // 
            this.dataColumn69.Caption = "收货单位";
            this.dataColumn69.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn70
            // 
            this.dataColumn70.Caption = "承运单位";
            this.dataColumn70.ColumnName = "FS_TRANS";
            // 
            // dataTable7
            // 
            this.dataTable7.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn71,
            this.dataColumn72,
            this.dataColumn73,
            this.dataColumn74,
            this.dataColumn75,
            this.dataColumn76,
            this.dataColumn77,
            this.dataColumn78,
            this.dataColumn79,
            this.dataColumn80,
            this.dataColumn82,
            this.dataColumn83,
            this.dataColumn84,
            this.dataColumn85,
            this.dataColumn86,
            this.dataColumn87,
            this.dataColumn88,
            this.dataColumn89,
            this.dataColumn90,
            this.dataColumn91,
            this.dataColumn81,
            this.dataColumn92});
            this.dataTable7.TableName = "静态轨道衡一次计量数据";
            // 
            // dataColumn71
            // 
            this.dataColumn71.Caption = "操作编号";
            this.dataColumn71.ColumnName = "fs_weightno";
            // 
            // dataColumn72
            // 
            this.dataColumn72.Caption = "物料代码";
            this.dataColumn72.ColumnName = "fs_material";
            // 
            // dataColumn73
            // 
            this.dataColumn73.Caption = "流向代码";
            this.dataColumn73.ColumnName = "fs_weighttype";
            // 
            // dataColumn74
            // 
            this.dataColumn74.Caption = "发货单位代码";
            this.dataColumn74.ColumnName = "fs_senderstoreno";
            // 
            // dataColumn75
            // 
            this.dataColumn75.Caption = "收货单位代码";
            this.dataColumn75.ColumnName = "fs_receiverstoreno";
            // 
            // dataColumn76
            // 
            this.dataColumn76.Caption = "车号";
            this.dataColumn76.ColumnName = "fs_trainno";
            // 
            // dataColumn77
            // 
            this.dataColumn77.Caption = "重量";
            this.dataColumn77.ColumnName = "fn_weight";
            // 
            // dataColumn78
            // 
            this.dataColumn78.Caption = "计量员";
            this.dataColumn78.ColumnName = "fs_weightperson";
            // 
            // dataColumn79
            // 
            this.dataColumn79.Caption = "计量时间";
            this.dataColumn79.ColumnName = "fd_weighttime";
            // 
            // dataColumn80
            // 
            this.dataColumn80.Caption = "计量点";
            this.dataColumn80.ColumnName = "fs_weightpoint";
            // 
            // dataColumn82
            // 
            this.dataColumn82.Caption = "删除标志";
            this.dataColumn82.ColumnName = "fs_delete";
            // 
            // dataColumn83
            // 
            this.dataColumn83.Caption = "删除确认人";
            this.dataColumn83.ColumnName = "fs_deleteuser";
            // 
            // dataColumn84
            // 
            this.dataColumn84.Caption = "删除日期";
            this.dataColumn84.ColumnName = "fd_deletedate";
            // 
            // dataColumn85
            // 
            this.dataColumn85.Caption = "物料名称";
            this.dataColumn85.ColumnName = "fs_materialname";
            // 
            // dataColumn86
            // 
            this.dataColumn86.Caption = "发货单位";
            this.dataColumn86.ColumnName = "fs_sender";
            // 
            // dataColumn87
            // 
            this.dataColumn87.Caption = "收货单位";
            this.dataColumn87.ColumnName = "fs_receiver";
            // 
            // dataColumn88
            // 
            this.dataColumn88.Caption = "流向";
            this.dataColumn88.ColumnName = "fs_typename";
            // 
            // dataColumn89
            // 
            this.dataColumn89.Caption = "计量点";
            this.dataColumn89.ColumnName = "fs_pointname";
            // 
            // dataColumn90
            // 
            this.dataColumn90.Caption = "承运单位";
            this.dataColumn90.ColumnName = "fs_trans";
            // 
            // dataColumn91
            // 
            this.dataColumn91.Caption = "承运单位代码";
            this.dataColumn91.ColumnName = "fs_transno";
            // 
            // dataColumn81
            // 
            this.dataColumn81.Caption = "班次";
            this.dataColumn81.ColumnName = "fs_shift";
            // 
            // dataColumn92
            // 
            this.dataColumn92.Caption = "班组";
            this.dataColumn92.ColumnName = "fs_group";
            // 
            // dataTable8
            // 
            this.dataTable8.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn93,
            this.dataColumn94,
            this.dataColumn95,
            this.dataColumn96,
            this.dataColumn97,
            this.dataColumn98,
            this.dataColumn99,
            this.dataColumn100,
            this.dataColumn101,
            this.dataColumn102,
            this.dataColumn103,
            this.dataColumn104,
            this.dataColumn105,
            this.dataColumn106,
            this.dataColumn107,
            this.dataColumn108,
            this.dataColumn109,
            this.dataColumn110,
            this.dataColumn111,
            this.dataColumn112,
            this.dataColumn113,
            this.dataColumn114,
            this.dataColumn115,
            this.dataColumn116,
            this.dataColumn117,
            this.dataColumn118,
            this.dataColumn119,
            this.dataColumn15});
            this.dataTable8.TableName = "静态轨道衡二次计量数据";
            // 
            // dataColumn93
            // 
            this.dataColumn93.Caption = "操作编号";
            this.dataColumn93.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn94
            // 
            this.dataColumn94.Caption = "物料代码";
            this.dataColumn94.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn95
            // 
            this.dataColumn95.Caption = "流向代码";
            this.dataColumn95.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn96
            // 
            this.dataColumn96.Caption = "发货单位代码";
            this.dataColumn96.ColumnName = "FS_SENDERSTORENO";
            // 
            // dataColumn97
            // 
            this.dataColumn97.Caption = "收货单位代码";
            this.dataColumn97.ColumnName = "FS_RECEIVERSTORENO";
            // 
            // dataColumn98
            // 
            this.dataColumn98.Caption = "车号";
            this.dataColumn98.ColumnName = "FS_TRAINNO";
            // 
            // dataColumn99
            // 
            this.dataColumn99.Caption = "毛重";
            this.dataColumn99.ColumnName = "FN_GROSSWEIGHT";
            // 
            // dataColumn100
            // 
            this.dataColumn100.Caption = "毛重计量员";
            this.dataColumn100.ColumnName = "FS_GROSSPERSON";
            // 
            // dataColumn101
            // 
            this.dataColumn101.Caption = "毛重计量点";
            this.dataColumn101.ColumnName = "FS_GROSSPOINT";
            // 
            // dataColumn102
            // 
            this.dataColumn102.Caption = "毛重计量时间";
            this.dataColumn102.ColumnName = "FD_GROSSTIME";
            // 
            // dataColumn103
            // 
            this.dataColumn103.Caption = "皮重";
            this.dataColumn103.ColumnName = "FN_TAREWEIGHT";
            // 
            // dataColumn104
            // 
            this.dataColumn104.Caption = "皮重计量员";
            this.dataColumn104.ColumnName = "FS_TAREPERSON";
            // 
            // dataColumn105
            // 
            this.dataColumn105.Caption = "皮重计量点";
            this.dataColumn105.ColumnName = "FS_TAREPOINT";
            // 
            // dataColumn106
            // 
            this.dataColumn106.Caption = "皮重计量时间";
            this.dataColumn106.ColumnName = "FD_TARETIME";
            // 
            // dataColumn107
            // 
            this.dataColumn107.Caption = "净重";
            this.dataColumn107.ColumnName = "FN_NETWEIGHT";
            // 
            // dataColumn108
            // 
            this.dataColumn108.Caption = "应扣量";
            this.dataColumn108.ColumnName = "FN_YKL";
            // 
            // dataColumn109
            // 
            this.dataColumn109.Caption = "毛重计量班次";
            this.dataColumn109.ColumnName = "FS_GROSSSHIFT";
            // 
            // dataColumn110
            // 
            this.dataColumn110.Caption = "毛重计量班组";
            this.dataColumn110.ColumnName = "FS_GROSSGROUP";
            // 
            // dataColumn111
            // 
            this.dataColumn111.Caption = "皮重计量班次";
            this.dataColumn111.ColumnName = "FS_TARESHIFT";
            // 
            // dataColumn112
            // 
            this.dataColumn112.Caption = "皮重计量班组";
            this.dataColumn112.ColumnName = "FS_TAREGROUP";
            // 
            // dataColumn113
            // 
            this.dataColumn113.Caption = "备注";
            this.dataColumn113.ColumnName = "FS_MEMO";
            // 
            // dataColumn114
            // 
            this.dataColumn114.Caption = "流向";
            this.dataColumn114.ColumnName = "FS_TYPENAME";
            // 
            // dataColumn115
            // 
            this.dataColumn115.Caption = "计量点名称";
            this.dataColumn115.ColumnName = "FS_POINTNAME";
            // 
            // dataColumn116
            // 
            this.dataColumn116.Caption = "承运单位";
            this.dataColumn116.ColumnName = "FS_TRANS";
            // 
            // dataColumn117
            // 
            this.dataColumn117.Caption = "物料名称";
            this.dataColumn117.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn118
            // 
            this.dataColumn118.Caption = "发货单位";
            this.dataColumn118.ColumnName = "FS_SENDER";
            // 
            // dataColumn119
            // 
            this.dataColumn119.Caption = "收货单位";
            this.dataColumn119.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn15
            // 
            this.dataColumn15.Caption = "修改时间";
            this.dataColumn15.ColumnName = "FD_UPDATETIME";
            // 
            // _panel1_Toolbars_Dock_Area_Left
            // 
            this._panel1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Left, null);
            this._panel1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._panel1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 26);
            this._panel1_Toolbars_Dock_Area_Left.Name = "_panel1_Toolbars_Dock_Area_Left";
            this._panel1_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 640);
            this._panel1_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Left, null);
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this.panel1;
            this.ultraToolbarsManager1.MenuAnimationStyle = Infragistics.Win.UltraWinToolbars.MenuAnimationStyle.Random;
            this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            ultraToolbar1.FloatingLocation = new System.Drawing.Point(41, 193);
            ultraToolbar1.FloatingSize = new System.Drawing.Size(350, 60);
            controlContainerTool5.ControlName = "dtpBegin";
            controlContainerTool6.ControlName = "dtpEnd";
            controlContainerTool6.InstanceProps.IsFirstInGroup = true;
            controlContainerTool1.ControlName = "tbTrainNo";
            controlContainerTool1.InstanceProps.Width = 152;
            buttonTool9.InstanceProps.IsFirstInGroup = true;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            controlContainerTool5,
            controlContainerTool6,
            controlContainerTool1,
            buttonTool1,
            buttonTool3,
            buttonTool5,
            buttonTool6,
            buttonTool9});
            ultraToolbar1.Text = "UltraToolbar1";
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            appearance11.Image = ((object)(resources.GetObject("appearance11.Image")));
            buttonTool2.SharedPropsInternal.AppearancesSmall.Appearance = appearance11;
            buttonTool2.SharedPropsInternal.Caption = "查询";
            buttonTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            appearance12.Image = ((object)(resources.GetObject("appearance12.Image")));
            buttonTool4.SharedPropsInternal.AppearancesSmall.Appearance = appearance12;
            buttonTool4.SharedPropsInternal.Caption = "修改";
            buttonTool4.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool4.SharedPropsInternal.Enabled = false;
            buttonTool4.SharedPropsInternal.Visible = false;
            appearance13.Image = ((object)(resources.GetObject("appearance13.Image")));
            buttonTool7.SharedPropsInternal.AppearancesSmall.Appearance = appearance13;
            buttonTool7.SharedPropsInternal.Caption = "新增";
            buttonTool7.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool7.SharedPropsInternal.Enabled = false;
            buttonTool7.SharedPropsInternal.Visible = false;
            appearance14.Image = ((object)(resources.GetObject("appearance14.Image")));
            buttonTool8.SharedPropsInternal.AppearancesSmall.Appearance = appearance14;
            buttonTool8.SharedPropsInternal.Caption = "删除";
            buttonTool8.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool8.SharedPropsInternal.Enabled = false;
            buttonTool8.SharedPropsInternal.Visible = false;
            controlContainerTool2.ControlName = "tbTrainNo";
            controlContainerTool2.SharedPropsInternal.Caption = "按车号查询";
            controlContainerTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool2.SharedPropsInternal.Width = 152;
            controlContainerTool4.ControlName = "dtpBegin";
            controlContainerTool4.SharedPropsInternal.Caption = "从：";
            controlContainerTool4.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool7.ControlName = "dtpEnd";
            controlContainerTool7.SharedPropsInternal.Caption = "至：";
            controlContainerTool7.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            appearance22.Image = ((object)(resources.GetObject("appearance22.Image")));
            buttonTool10.SharedPropsInternal.AppearancesSmall.Appearance = appearance22;
            buttonTool10.SharedPropsInternal.Caption = "导出到Excel";
            buttonTool10.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool2,
            buttonTool4,
            buttonTool7,
            buttonTool8,
            controlContainerTool2,
            controlContainerTool4,
            controlContainerTool7,
            buttonTool10});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
            // 
            // _panel1_Toolbars_Dock_Area_Right
            // 
            this._panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Right, null);
            this._panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(992, 26);
            this._panel1_Toolbars_Dock_Area_Right.Name = "_panel1_Toolbars_Dock_Area_Right";
            this._panel1_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 640);
            this._panel1_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Right, null);
            // 
            // _panel1_Toolbars_Dock_Area_Top
            // 
            this._panel1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Top, null);
            this._panel1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._panel1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._panel1_Toolbars_Dock_Area_Top.Name = "_panel1_Toolbars_Dock_Area_Top";
            this._panel1_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(992, 26);
            this._panel1_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Top, null);
            // 
            // _panel1_Toolbars_Dock_Area_Bottom
            // 
            this._panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Bottom, null);
            this._panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 666);
            this._panel1_Toolbars_Dock_Area_Bottom.Name = "_panel1_Toolbars_Dock_Area_Bottom";
            this._panel1_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(992, 0);
            this._panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
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
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14});
            this.dataTable1.TableName = "静态轨道衡预报表";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "操作编号";
            this.dataColumn1.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "物料名称代码";
            this.dataColumn2.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "流向";
            this.dataColumn3.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "发货单位代码";
            this.dataColumn4.ColumnName = "FS_SENDERSTROENO";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "收货单位代码";
            this.dataColumn5.ColumnName = "FS_RECEIVESTORENO";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "车号";
            this.dataColumn6.ColumnName = "FS_TRAINNO";
            // 
            // dataColumn7
            // 
            this.dataColumn7.Caption = "计量点";
            this.dataColumn7.ColumnName = "FS_WEIGHTPOINT";
            // 
            // dataColumn8
            // 
            this.dataColumn8.Caption = "录入部门";
            this.dataColumn8.ColumnName = "FS_DEPARTMENT";
            // 
            // dataColumn9
            // 
            this.dataColumn9.Caption = "录入员";
            this.dataColumn9.ColumnName = "FS_USER";
            // 
            // dataColumn10
            // 
            this.dataColumn10.Caption = "录入时间";
            this.dataColumn10.ColumnName = "FD_TIMES";
            // 
            // dataColumn11
            // 
            this.dataColumn11.Caption = "物料名称";
            this.dataColumn11.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn12
            // 
            this.dataColumn12.Caption = "发货单位";
            this.dataColumn12.ColumnName = "FS_SENDER";
            // 
            // dataColumn13
            // 
            this.dataColumn13.Caption = "收货单位";
            this.dataColumn13.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn14
            // 
            this.dataColumn14.Caption = "承运单位";
            this.dataColumn14.ColumnName = "FS_TRANS";
            // 
            // QueryHistoryWeightData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 666);
            this.Controls.Add(this.panel1);
            this.coreBind.SetDatabasecommand(this, null);
            this.Name = "QueryHistoryWeightData";
            this.Tag = "HanderHistoryWeightData";
            this.Text = "静轨历史数据查询";
            this.coreBind.SetVerification(this, null);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private System.Windows.Forms.TextBox tbTrainNo;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn12;
        private System.Data.DataColumn dataColumn13;
        private System.Data.DataColumn dataColumn14;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Data.DataSet dataSet2;
        private System.Data.DataTable dataTable6;
        private System.Data.DataColumn dataColumn57;
        private System.Data.DataColumn dataColumn58;
        private System.Data.DataColumn dataColumn59;
        private System.Data.DataColumn dataColumn60;
        private System.Data.DataColumn dataColumn61;
        private System.Data.DataColumn dataColumn62;
        private System.Data.DataColumn dataColumn63;
        private System.Data.DataColumn dataColumn64;
        private System.Data.DataColumn dataColumn65;
        private System.Data.DataColumn dataColumn66;
        private System.Data.DataColumn dataColumn67;
        private System.Data.DataColumn dataColumn68;
        private System.Data.DataColumn dataColumn69;
        private System.Data.DataColumn dataColumn70;
        private System.Data.DataTable dataTable7;
        private System.Data.DataColumn dataColumn71;
        private System.Data.DataColumn dataColumn72;
        private System.Data.DataColumn dataColumn73;
        private System.Data.DataColumn dataColumn74;
        private System.Data.DataColumn dataColumn75;
        private System.Data.DataColumn dataColumn76;
        private System.Data.DataColumn dataColumn77;
        private System.Data.DataColumn dataColumn78;
        private System.Data.DataColumn dataColumn79;
        private System.Data.DataColumn dataColumn80;
        private System.Data.DataColumn dataColumn82;
        private System.Data.DataColumn dataColumn83;
        private System.Data.DataColumn dataColumn84;
        private System.Data.DataColumn dataColumn85;
        private System.Data.DataColumn dataColumn86;
        private System.Data.DataColumn dataColumn87;
        private System.Data.DataColumn dataColumn88;
        private System.Data.DataColumn dataColumn89;
        private System.Data.DataColumn dataColumn90;
        private System.Data.DataColumn dataColumn91;
        private System.Data.DataColumn dataColumn81;
        private System.Data.DataColumn dataColumn92;
        private System.Data.DataTable dataTable8;
        private System.Data.DataColumn dataColumn93;
        private System.Data.DataColumn dataColumn94;
        private System.Data.DataColumn dataColumn95;
        private System.Data.DataColumn dataColumn96;
        private System.Data.DataColumn dataColumn97;
        private System.Data.DataColumn dataColumn98;
        private System.Data.DataColumn dataColumn99;
        private System.Data.DataColumn dataColumn100;
        private System.Data.DataColumn dataColumn101;
        private System.Data.DataColumn dataColumn102;
        private System.Data.DataColumn dataColumn103;
        private System.Data.DataColumn dataColumn104;
        private System.Data.DataColumn dataColumn105;
        private System.Data.DataColumn dataColumn106;
        private System.Data.DataColumn dataColumn107;
        private System.Data.DataColumn dataColumn108;
        private System.Data.DataColumn dataColumn109;
        private System.Data.DataColumn dataColumn110;
        private System.Data.DataColumn dataColumn111;
        private System.Data.DataColumn dataColumn112;
        private System.Data.DataColumn dataColumn113;
        private System.Data.DataColumn dataColumn114;
        private System.Data.DataColumn dataColumn115;
        private System.Data.DataColumn dataColumn116;
        private System.Data.DataColumn dataColumn117;
        private System.Data.DataColumn dataColumn118;
        private System.Data.DataColumn dataColumn119;
        private System.Windows.Forms.DateTimePicker dtpBegin;
        private System.Data.DataColumn dataColumn15;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
    }
}

