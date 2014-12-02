namespace YGJZJL.StaticTrack
{
    partial class TrackWeight
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
                StopPoundRoomThread();//停止计量点处理线程
                RecordClose(0);
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
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("静态轨道衡二次计量数据", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIAL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDERSTORENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVERSTORENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRAINNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_GROSSWEIGHT", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
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
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings1 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Count, null, "FS_MATERIALNAME", 24, true, "静态轨道衡二次计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, null, -1, false);
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings2 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "FN_NETWEIGHT", 14, true, "静态轨道衡二次计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FN_NETWEIGHT", 14, true);
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("静态轨道衡一次计量数据", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIAL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDERSTORENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVERSTORENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRAINNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_WEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_WEIGHTTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTPOINT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DELETEFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DELETEUSER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_DELETEDATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIALNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TYPENAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRANS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRANSNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SHIFT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_GROUP");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings3 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Count, null, "FS_MATERIALNAME", 13, true, "静态轨道衡一次计量数据", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, null, -1, false);
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("语音表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICENAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICEFILE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_INSTRTYPE");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool4 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("日期");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool5 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("至");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("find");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Aedio");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btCorrention");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool1 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("日期");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool2 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("炉号");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackWeight));
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Aedio");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("find");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btCorrention");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool6 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("至");
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("05deab1a-dfec-4181-9cb3-ad4fe0377535"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("89870e2b-ce2c-4fc0-9610-41bf1733973c"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("05deab1a-dfec-4181-9cb3-ad4fe0377535"), -1);
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane2 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("777aa848-96d9-4a9c-8e57-ab46776d741c"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane2 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("167a762b-28a1-4b3a-b58a-b7c31ec2d826"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("777aa848-96d9-4a9c-8e57-ab46776d741c"), -1);
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
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
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelYYBF = new System.Windows.Forms.Panel();
            this.uDridSound = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dataColumn25 = new System.Data.DataColumn();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataColumn34 = new System.Data.DataColumn();
            this.dataColumn35 = new System.Data.DataColumn();
            this.dataColumn36 = new System.Data.DataColumn();
            this.dataColumn37 = new System.Data.DataColumn();
            this.dataColumn38 = new System.Data.DataColumn();
            this.dataColumn142 = new System.Data.DataColumn();
            this.dataColumn143 = new System.Data.DataColumn();
            this.dataColumn144 = new System.Data.DataColumn();
            this.dataColumn145 = new System.Data.DataColumn();
            this.dataColumn146 = new System.Data.DataColumn();
            this.dataColumn147 = new System.Data.DataColumn();
            this.dataColumn148 = new System.Data.DataColumn();
            this.dataColumn149 = new System.Data.DataColumn();
            this.dataColumn150 = new System.Data.DataColumn();
            this.dataColumn151 = new System.Data.DataColumn();
            this.dataColumn152 = new System.Data.DataColumn();
            this.dataColumn153 = new System.Data.DataColumn();
            this.dataColumn154 = new System.Data.DataColumn();
            this.dataColumn155 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn28 = new System.Data.DataColumn();
            this.dataColumn29 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.dataColumn33 = new System.Data.DataColumn();
            this.dataColumn40 = new System.Data.DataColumn();
            this.dataColumn41 = new System.Data.DataColumn();
            this.dataColumn42 = new System.Data.DataColumn();
            this.dataColumn55 = new System.Data.DataColumn();
            this.dataColumn56 = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn39 = new System.Data.DataColumn();
            this.dataTable5 = new System.Data.DataTable();
            this.dataColumn44 = new System.Data.DataColumn();
            this.dataColumn45 = new System.Data.DataColumn();
            this.dataColumn46 = new System.Data.DataColumn();
            this.dataColumn47 = new System.Data.DataColumn();
            this.dataColumn48 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn43 = new System.Data.DataColumn();
            this.dataColumn49 = new System.Data.DataColumn();
            this.dataColumn50 = new System.Data.DataColumn();
            this.dataColumn51 = new System.Data.DataColumn();
            this.dataColumn52 = new System.Data.DataColumn();
            this.dataColumn53 = new System.Data.DataColumn();
            this.dataColumn54 = new System.Data.DataColumn();
            this.panelSPKZ = new System.Windows.Forms.Panel();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.ck_Tare = new System.Windows.Forms.CheckBox();
            this.btnTrainTare = new System.Windows.Forms.Button();
            this.btnWc = new System.Windows.Forms.Button();
            this.btnSglr = new System.Windows.Forms.Button();
            this.btnBC = new System.Windows.Forms.Button();
            this.btnDS = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btReceiver = new System.Windows.Forms.Button();
            this.btSender = new System.Windows.Forms.Button();
            this.btTrans = new System.Windows.Forms.Button();
            this.btMaterial = new System.Windows.Forms.Button();
            this.cbTrans = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNetWeight = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTareWeight = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbMaterial = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbReceiver = new System.Windows.Forms.ComboBox();
            this.txtJly = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.cbSender = new System.Windows.Forms.ComboBox();
            this.cbFlow = new System.Windows.Forms.ComboBox();
            this.txtJld = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCh = new System.Windows.Forms.Label();
            this.txtTRAINNO = new System.Windows.Forms.TextBox();
            this.picFDTP = new System.Windows.Forms.PictureBox();
            this.btnQL = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtMeterWeight = new LxControl.LxLedControl();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMaterShow = new System.Windows.Forms.Label();
            this.lblMater = new System.Windows.Forms.Label();
            this.BilletInfo_GD_Fill_Panel = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.VideoChannel3 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.VideoChannel2 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.VideoChannel1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.panel1_Fill_Panel = new System.Windows.Forms.Panel();
            this.dateRQ = new System.Windows.Forms.DateTimePicker();
            this._panel1_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._panel1_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraDockManager1 = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._MoltenInfo_OneUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MoltenInfo_OneUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MoltenInfo_OneUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MoltenInfo_OneUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MoltenInfo_OneAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.dockableWindow1 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.dockableWindow2 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.windowDockingArea2 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.dsQuery = new System.Data.DataSet();
            this.dataTable4 = new System.Data.DataTable();
            this.dataColumn120 = new System.Data.DataColumn();
            this.dataColumn121 = new System.Data.DataColumn();
            this.dataColumn122 = new System.Data.DataColumn();
            this.dataColumn123 = new System.Data.DataColumn();
            this.dataColumn124 = new System.Data.DataColumn();
            this.dataColumn125 = new System.Data.DataColumn();
            this.dataColumn126 = new System.Data.DataColumn();
            this.dataColumn127 = new System.Data.DataColumn();
            this.dataColumn128 = new System.Data.DataColumn();
            this.dataColumn129 = new System.Data.DataColumn();
            this.dataColumn130 = new System.Data.DataColumn();
            this.dataColumn131 = new System.Data.DataColumn();
            this.dataColumn132 = new System.Data.DataColumn();
            this.dataColumn133 = new System.Data.DataColumn();
            this.dataColumn134 = new System.Data.DataColumn();
            this.dataColumn135 = new System.Data.DataColumn();
            this.dataColumn136 = new System.Data.DataColumn();
            this.dataColumn137 = new System.Data.DataColumn();
            this.dataColumn138 = new System.Data.DataColumn();
            this.dataColumn139 = new System.Data.DataColumn();
            this.dataColumn140 = new System.Data.DataColumn();
            this.dataColumn141 = new System.Data.DataColumn();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable8)).BeginInit();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
            this.panelYYBF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uDridSound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).BeginInit();
            this.panelSPKZ.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeterWeight)).BeginInit();
            this.BilletInfo_GD_Fill_Panel.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel3)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).BeginInit();
            this._MoltenInfo_OneAutoHideControl.SuspendLayout();
            this.dockableWindow1.SuspendLayout();
            this.dockableWindow2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsQuery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.ultraGrid1);
            this.coreBind.SetDatabasecommand(this.ultraTabPageControl2, null);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(1, 22);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(735, 130);
            this.coreBind.SetVerification(this.ultraTabPageControl2, null);
            // 
            // ultraGrid1
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid1, null);
            this.ultraGrid1.DataMember = "静态轨道衡二次计量数据";
            this.ultraGrid1.DataSource = this.dataSet2;
            appearance34.BackColor = System.Drawing.Color.White;
            appearance34.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid1.DisplayLayout.Appearance = appearance34;
            ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn1.Header.VisiblePosition = 18;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn2.Header.VisiblePosition = 19;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 0;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 13;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 14;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.VisiblePosition = 1;
            ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.MinWidth = 100;
            ultraGridColumn8.Header.VisiblePosition = 16;
            ultraGridColumn9.Header.VisiblePosition = 17;
            ultraGridColumn10.Header.VisiblePosition = 15;
            ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn11.Header.VisiblePosition = 8;
            ultraGridColumn11.MinWidth = 100;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn14.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn14.Header.VisiblePosition = 10;
            ultraGridColumn15.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn15.Header.VisiblePosition = 9;
            ultraGridColumn15.MinWidth = 100;
            ultraGridColumn16.Header.VisiblePosition = 21;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.VisiblePosition = 22;
            ultraGridColumn18.Header.VisiblePosition = 23;
            ultraGridColumn19.Header.VisiblePosition = 24;
            ultraGridColumn20.Header.VisiblePosition = 20;
            ultraGridColumn21.Header.VisiblePosition = 25;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.Header.VisiblePosition = 5;
            ultraGridColumn23.Header.VisiblePosition = 26;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 6;
            ultraGridColumn25.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn25.Header.VisiblePosition = 2;
            ultraGridColumn25.MinWidth = 100;
            ultraGridColumn26.Header.VisiblePosition = 3;
            ultraGridColumn27.Header.VisiblePosition = 4;
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
            ultraGridColumn27});
            summarySettings1.DisplayFormat = "累计共{0}车";
            summarySettings1.GroupBySummaryValueAppearance = appearance5;
            summarySettings2.DisplayFormat = "累计共{0}吨";
            summarySettings2.GroupBySummaryValueAppearance = appearance6;
            ultraGridBand1.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings1,
            summarySettings2});
            ultraGridBand1.SummaryFooterCaption = "";
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance37.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance37;
            this.ultraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ultraGrid1.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            this.ultraGrid1.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            this.ultraGrid1.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.ultraGrid1.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance38.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance38.ForeColor = System.Drawing.Color.White;
            appearance38.TextHAlignAsString = "Center";
            appearance38.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance38;
            this.ultraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance39.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance39;
            appearance40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance40.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorAppearance = appearance40;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid1.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(169)))), ((int)(((byte)(226)))));
            appearance41.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance41.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid1.DisplayLayout.Override.SelectedRowAppearance = appearance41;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid1.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.ultraGrid1.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
            this.ultraGrid1.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid1.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid1.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ultraGrid1.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(735, 130);
            this.ultraGrid1.TabIndex = 1;
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
            this.dataColumn71.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn72
            // 
            this.dataColumn72.Caption = "物料代码";
            this.dataColumn72.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn73
            // 
            this.dataColumn73.Caption = "流向代码";
            this.dataColumn73.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn74
            // 
            this.dataColumn74.Caption = "发货单位代码";
            this.dataColumn74.ColumnName = "FS_SENDERSTORENO";
            // 
            // dataColumn75
            // 
            this.dataColumn75.Caption = "收货单位代码";
            this.dataColumn75.ColumnName = "FS_RECEIVERSTORENO";
            // 
            // dataColumn76
            // 
            this.dataColumn76.Caption = "车号";
            this.dataColumn76.ColumnName = "FS_TRAINNO";
            // 
            // dataColumn77
            // 
            this.dataColumn77.Caption = "重量";
            this.dataColumn77.ColumnName = "FN_WEIGHT";
            // 
            // dataColumn78
            // 
            this.dataColumn78.Caption = "计量员";
            this.dataColumn78.ColumnName = "FS_WEIGHTPERSON";
            // 
            // dataColumn79
            // 
            this.dataColumn79.Caption = "计量时间";
            this.dataColumn79.ColumnName = "FD_WEIGHTTIME";
            // 
            // dataColumn80
            // 
            this.dataColumn80.Caption = "计量点";
            this.dataColumn80.ColumnName = "FS_WEIGHTPOINT";
            // 
            // dataColumn82
            // 
            this.dataColumn82.Caption = "删除标志";
            this.dataColumn82.ColumnName = "FS_DELETEFLAG";
            // 
            // dataColumn83
            // 
            this.dataColumn83.Caption = "删除确认人";
            this.dataColumn83.ColumnName = "FS_DELETEUSER";
            // 
            // dataColumn84
            // 
            this.dataColumn84.Caption = "删除日期";
            this.dataColumn84.ColumnName = "FD_DELETEDATE";
            // 
            // dataColumn85
            // 
            this.dataColumn85.Caption = "物料名称";
            this.dataColumn85.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn86
            // 
            this.dataColumn86.Caption = "发货单位";
            this.dataColumn86.ColumnName = "FS_SENDER";
            // 
            // dataColumn87
            // 
            this.dataColumn87.Caption = "收货单位";
            this.dataColumn87.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn88
            // 
            this.dataColumn88.Caption = "流向";
            this.dataColumn88.ColumnName = "FS_TYPENAME";
            // 
            // dataColumn89
            // 
            this.dataColumn89.Caption = "计量点";
            this.dataColumn89.ColumnName = "FS_POINTNAME";
            // 
            // dataColumn90
            // 
            this.dataColumn90.Caption = "承运单位";
            this.dataColumn90.ColumnName = "FS_TRANS";
            // 
            // dataColumn91
            // 
            this.dataColumn91.Caption = "承运单位代码";
            this.dataColumn91.ColumnName = "FS_TRANSNO";
            // 
            // dataColumn81
            // 
            this.dataColumn81.Caption = "班次";
            this.dataColumn81.ColumnName = "FS_SHIFT";
            // 
            // dataColumn92
            // 
            this.dataColumn92.Caption = "班组";
            this.dataColumn92.ColumnName = "FS_GROUP";
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
            this.dataColumn119});
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
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ultraGrid2);
            this.coreBind.SetDatabasecommand(this.ultraTabPageControl1, null);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(880, 130);
            this.coreBind.SetVerification(this.ultraTabPageControl1, null);
            // 
            // ultraGrid2
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid2, null);
            this.ultraGrid2.DataMember = "静态轨道衡一次计量数据";
            this.ultraGrid2.DataSource = this.dataSet2;
            appearance60.BackColor = System.Drawing.Color.White;
            appearance60.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid2.DisplayLayout.Appearance = appearance60;
            ultraGridColumn28.Header.VisiblePosition = 0;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.VisiblePosition = 1;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.Header.VisiblePosition = 2;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 3;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 4;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 5;
            ultraGridColumn34.Header.VisiblePosition = 10;
            ultraGridColumn35.Header.VisiblePosition = 14;
            ultraGridColumn36.Header.VisiblePosition = 12;
            ultraGridColumn37.Header.VisiblePosition = 15;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.VisiblePosition = 11;
            ultraGridColumn39.Header.VisiblePosition = 16;
            ultraGridColumn40.Header.VisiblePosition = 17;
            ultraGridColumn41.Header.VisiblePosition = 6;
            ultraGridColumn42.Header.VisiblePosition = 7;
            ultraGridColumn43.Header.VisiblePosition = 8;
            ultraGridColumn43.Width = 102;
            ultraGridColumn44.Header.VisiblePosition = 13;
            ultraGridColumn45.Header.VisiblePosition = 18;
            ultraGridColumn46.Header.VisiblePosition = 9;
            ultraGridColumn47.Header.VisiblePosition = 21;
            ultraGridColumn47.Hidden = true;
            ultraGridColumn48.Header.VisiblePosition = 19;
            ultraGridColumn49.Header.VisiblePosition = 20;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49});
            summarySettings3.DisplayFormat = "累计共{0}车";
            summarySettings3.GroupBySummaryValueAppearance = appearance7;
            ultraGridBand2.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings3});
            ultraGridBand2.SummaryFooterCaption = "";
            this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraGrid2.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid2.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid2.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid2.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance62.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid2.DisplayLayout.Override.CardAreaAppearance = appearance62;
            this.ultraGrid2.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ultraGrid2.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            this.ultraGrid2.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            this.ultraGrid2.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.ultraGrid2.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance63.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance63.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance63.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance63.ForeColor = System.Drawing.Color.White;
            appearance63.TextHAlignAsString = "Center";
            appearance63.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid2.DisplayLayout.Override.HeaderAppearance = appearance63;
            this.ultraGrid2.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance64.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.Override.RowAppearance = appearance64;
            appearance65.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance65.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorAppearance = appearance65;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid2.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance66.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(169)))), ((int)(((byte)(226)))));
            appearance66.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance66.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid2.DisplayLayout.Override.SelectedRowAppearance = appearance66;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.ultraGrid2.DisplayLayout.Override.SummaryDisplayArea = Infragistics.Win.UltraWinGrid.SummaryDisplayAreas.BottomFixed;
            this.ultraGrid2.DisplayLayout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid2.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid2.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid2.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid2.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ultraGrid2.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.ultraGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid2.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid2.Name = "ultraGrid2";
            this.ultraGrid2.Size = new System.Drawing.Size(880, 130);
            this.ultraGrid2.TabIndex = 2;
            this.coreBind.SetVerification(this.ultraGrid2, null);
            // 
            // panelYYBF
            // 
            this.panelYYBF.Controls.Add(this.uDridSound);
            this.coreBind.SetDatabasecommand(this.panelYYBF, null);
            this.panelYYBF.Location = new System.Drawing.Point(0, 28);
            this.panelYYBF.Name = "panelYYBF";
            this.panelYYBF.Size = new System.Drawing.Size(95, 706);
            this.panelYYBF.TabIndex = 57;
            this.coreBind.SetVerification(this.panelYYBF, null);
            // 
            // uDridSound
            // 
            this.coreBind.SetDatabasecommand(this.uDridSound, null);
            this.uDridSound.DataMember = "语音表";
            this.uDridSound.DataSource = this.dataSet1;
            appearance13.BackColor = System.Drawing.Color.White;
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.uDridSound.DisplayLayout.Appearance = appearance13;
            ultraGridColumn50.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn50.Header.VisiblePosition = 0;
            ultraGridColumn51.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn51.Header.VisiblePosition = 1;
            ultraGridColumn52.Header.VisiblePosition = 2;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52});
            this.uDridSound.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.uDridSound.DisplayLayout.InterBandSpacing = 10;
            appearance14.BackColor = System.Drawing.Color.Transparent;
            this.uDridSound.DisplayLayout.Override.CardAreaAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance15.BackColor2 = System.Drawing.Color.White;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance15.FontData.SizeInPoints = 11F;
            appearance15.FontData.UnderlineAsString = "False";
            appearance15.ForeColor = System.Drawing.Color.Black;
            appearance15.TextHAlignAsString = "Center";
            appearance15.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.uDridSound.DisplayLayout.Override.HeaderAppearance = appearance15;
            appearance16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.uDridSound.DisplayLayout.Override.RowAppearance = appearance16;
            appearance17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance17.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.uDridSound.DisplayLayout.Override.RowSelectorAppearance = appearance17;
            this.uDridSound.DisplayLayout.Override.RowSelectorWidth = 12;
            this.uDridSound.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance18.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance18.ForeColor = System.Drawing.Color.Black;
            this.uDridSound.DisplayLayout.Override.SelectedRowAppearance = appearance18;
            this.uDridSound.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.uDridSound.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.uDridSound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uDridSound.Location = new System.Drawing.Point(0, 0);
            this.uDridSound.Name = "uDridSound";
            this.uDridSound.Size = new System.Drawing.Size(95, 706);
            this.uDridSound.TabIndex = 4;
            this.coreBind.SetVerification(this.uDridSound, null);
            this.uDridSound.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.uDridSound_ClickCell);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3,
            this.dataTable5});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn11,
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn24,
            this.dataColumn25,
            this.dataColumn26,
            this.dataColumn34,
            this.dataColumn35,
            this.dataColumn36,
            this.dataColumn37,
            this.dataColumn38,
            this.dataColumn142,
            this.dataColumn143,
            this.dataColumn144,
            this.dataColumn145,
            this.dataColumn146,
            this.dataColumn147,
            this.dataColumn148,
            this.dataColumn149,
            this.dataColumn150,
            this.dataColumn151,
            this.dataColumn152,
            this.dataColumn153,
            this.dataColumn154,
            this.dataColumn155});
            this.dataTable1.TableName = "计量点基础表";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "计量点编码";
            this.dataColumn6.ColumnName = "FS_POINTCODE";
            // 
            // dataColumn7
            // 
            this.dataColumn7.Caption = "计量点";
            this.dataColumn7.ColumnName = "FS_POINTNAME";
            // 
            // dataColumn8
            // 
            this.dataColumn8.Caption = "接管";
            this.dataColumn8.ColumnName = "XZ";
            // 
            // dataColumn11
            // 
            this.dataColumn11.Caption = "工厂";
            this.dataColumn11.ColumnName = "FS_POINTDEPART";
            // 
            // dataColumn12
            // 
            this.dataColumn12.Caption = "称重类型";
            this.dataColumn12.ColumnName = "FS_POINTTYPE";
            // 
            // dataColumn13
            // 
            this.dataColumn13.Caption = "硬盘录像机IP";
            this.dataColumn13.ColumnName = "FS_VIEDOIP";
            // 
            // dataColumn14
            // 
            this.dataColumn14.Caption = "硬盘录像机端口";
            this.dataColumn14.ColumnName = "FS_VIEDOPORT";
            // 
            // dataColumn15
            // 
            this.dataColumn15.Caption = "硬盘录像机用户名";
            this.dataColumn15.ColumnName = "FS_VIEDOUSER";
            // 
            // dataColumn16
            // 
            this.dataColumn16.Caption = "硬盘录像机密码";
            this.dataColumn16.ColumnName = "FS_VIEDOPWD";
            // 
            // dataColumn17
            // 
            this.dataColumn17.Caption = "仪表类型";
            this.dataColumn17.ColumnName = "FS_METERTYPE";
            // 
            // dataColumn18
            // 
            this.dataColumn18.Caption = "仪表参数";
            this.dataColumn18.ColumnName = "FS_METERPARA";
            // 
            // dataColumn19
            // 
            this.dataColumn19.Caption = "MOXA卡IP";
            this.dataColumn19.ColumnName = "FS_MOXAIP";
            // 
            // dataColumn20
            // 
            this.dataColumn20.Caption = "MOXA卡端口";
            this.dataColumn20.ColumnName = "FS_MOXAPORT";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "FS_RTUIP";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "FS_RTUPORT";
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "FS_PRINTERIP";
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "FS_PRINTERNAME";
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "FS_LEDPORT";
            // 
            // dataColumn36
            // 
            this.dataColumn36.ColumnName = "FS_LEDIP";
            // 
            // dataColumn37
            // 
            this.dataColumn37.Caption = "复位值";
            this.dataColumn37.ColumnName = "FN_VALUE";
            // 
            // dataColumn38
            // 
            this.dataColumn38.Caption = "清零值（差值）";
            this.dataColumn38.ColumnName = "FF_CLEARVALUE";
            // 
            // dataColumn142
            // 
            this.dataColumn142.ColumnName = "FS_PRINTTYPECODE";
            // 
            // dataColumn143
            // 
            this.dataColumn143.ColumnName = "FN_USEDPRINTPAPER";
            // 
            // dataColumn144
            // 
            this.dataColumn144.ColumnName = "FN_USEDPRINTINK";
            // 
            // dataColumn145
            // 
            this.dataColumn145.ColumnName = "FS_ALLOWOTHERTARE";
            // 
            // dataColumn146
            // 
            this.dataColumn146.ColumnName = "FS_SIGN";
            // 
            // dataColumn147
            // 
            this.dataColumn147.ColumnName = "FS_DISPLAYPORT";
            // 
            // dataColumn148
            // 
            this.dataColumn148.ColumnName = "FS_DISPLAYPARA";
            // 
            // dataColumn149
            // 
            this.dataColumn149.ColumnName = "FS_READERPORT";
            // 
            // dataColumn150
            // 
            this.dataColumn150.ColumnName = "FS_READERPARA";
            // 
            // dataColumn151
            // 
            this.dataColumn151.ColumnName = "FS_READERTYPE";
            // 
            // dataColumn152
            // 
            this.dataColumn152.ColumnName = "FS_DISPLAYTYPE";
            // 
            // dataColumn153
            // 
            this.dataColumn153.ColumnName = "TOTALPAPAR";
            // 
            // dataColumn154
            // 
            this.dataColumn154.ColumnName = "TOTALINK";
            // 
            // dataColumn155
            // 
            this.dataColumn155.ColumnName = "FS_LEDTYPE";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn21,
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn10,
            this.dataColumn28,
            this.dataColumn29,
            this.dataColumn30,
            this.dataColumn32,
            this.dataColumn33,
            this.dataColumn40,
            this.dataColumn41,
            this.dataColumn42,
            this.dataColumn55,
            this.dataColumn56});
            this.dataTable2.TableName = "信息采集表";
            // 
            // dataColumn21
            // 
            this.dataColumn21.Caption = "车皮号";
            this.dataColumn21.ColumnName = "FS_POTNO";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "生产订单号";
            this.dataColumn1.ColumnName = "FS_PRODUCTNO";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "物料名称";
            this.dataColumn2.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "毛重";
            this.dataColumn3.ColumnName = "FN_GROSSWEIGHT";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "皮重";
            this.dataColumn4.ColumnName = "FN_TAREWEIGHT";
            // 
            // dataColumn10
            // 
            this.dataColumn10.Caption = "净重";
            this.dataColumn10.ColumnName = "FN_NETWEIGHT";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "FS_RECEIVESTORE";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "FS_SENDERSTROENO";
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "FS_ITEMNO";
            // 
            // dataColumn40
            // 
            this.dataColumn40.Caption = "重车时间";
            this.dataColumn40.ColumnName = "FS_GROSSTIME";
            // 
            // dataColumn41
            // 
            this.dataColumn41.Caption = "空车时间";
            this.dataColumn41.ColumnName = "FD_TARETIME";
            // 
            // dataColumn42
            // 
            this.dataColumn42.Caption = "应扣量";
            this.dataColumn42.ColumnName = "FS_YKL";
            // 
            // dataColumn55
            // 
            this.dataColumn55.Caption = "发货单位";
            this.dataColumn55.ColumnName = "FS_SENDER";
            // 
            // dataColumn56
            // 
            this.dataColumn56.Caption = "收货单位";
            this.dataColumn56.ColumnName = "FS_RECEIVER";
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn9,
            this.dataColumn39});
            this.dataTable3.TableName = "语音表";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "声音名称";
            this.dataColumn5.ColumnName = "FS_VOICENAME";
            // 
            // dataColumn9
            // 
            this.dataColumn9.Caption = "声音文件";
            this.dataColumn9.ColumnName = "FS_VOICEFILE";
            // 
            // dataColumn39
            // 
            this.dataColumn39.ColumnName = "FS_INSTRTYPE";
            // 
            // dataTable5
            // 
            this.dataTable5.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn44,
            this.dataColumn45,
            this.dataColumn46,
            this.dataColumn47,
            this.dataColumn48,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn27,
            this.dataColumn31,
            this.dataColumn43,
            this.dataColumn49,
            this.dataColumn50,
            this.dataColumn51,
            this.dataColumn52,
            this.dataColumn53,
            this.dataColumn54});
            this.dataTable5.TableName = "第一次计量信息";
            // 
            // dataColumn44
            // 
            this.dataColumn44.Caption = "车皮号";
            this.dataColumn44.ColumnName = "FS_POTNO";
            // 
            // dataColumn45
            // 
            this.dataColumn45.Caption = "物料名称";
            this.dataColumn45.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn46
            // 
            this.dataColumn46.Caption = "计量员";
            this.dataColumn46.ColumnName = "FS_WEIGHTPERSON";
            // 
            // dataColumn47
            // 
            this.dataColumn47.Caption = "计量时间";
            this.dataColumn47.ColumnName = "FD_WEIGHTTIME";
            // 
            // dataColumn48
            // 
            this.dataColumn48.Caption = "重量";
            this.dataColumn48.ColumnName = "FN_WEIGHT";
            // 
            // dataColumn22
            // 
            this.dataColumn22.Caption = "操作编号";
            this.dataColumn22.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn23
            // 
            this.dataColumn23.Caption = "订单号";
            this.dataColumn23.ColumnName = "FS_PRODUCTNO";
            // 
            // dataColumn27
            // 
            this.dataColumn27.Caption = "项目号";
            this.dataColumn27.ColumnName = "FS_ITEMNO";
            // 
            // dataColumn31
            // 
            this.dataColumn31.Caption = "物料代码";
            this.dataColumn31.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn43
            // 
            this.dataColumn43.Caption = "流向";
            this.dataColumn43.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn49
            // 
            this.dataColumn49.Caption = "发货单位代码";
            this.dataColumn49.ColumnName = "FS_SENDERSTROENO";
            // 
            // dataColumn50
            // 
            this.dataColumn50.Caption = "收货单位代码";
            this.dataColumn50.ColumnName = "FS_RECEIVESTORE";
            // 
            // dataColumn51
            // 
            this.dataColumn51.Caption = "炉号";
            this.dataColumn51.ColumnName = "FS_STOVENO";
            // 
            // dataColumn52
            // 
            this.dataColumn52.Caption = "炉座号";
            this.dataColumn52.ColumnName = "FS_STOVESEATNO";
            // 
            // dataColumn53
            // 
            this.dataColumn53.Caption = "发货单位";
            this.dataColumn53.ColumnName = "FS_SENDER";
            // 
            // dataColumn54
            // 
            this.dataColumn54.Caption = "收货单位";
            this.dataColumn54.ColumnName = "FS_RECIEVER";
            // 
            // panelSPKZ
            // 
            this.panelSPKZ.Controls.Add(this.button15);
            this.panelSPKZ.Controls.Add(this.button14);
            this.panelSPKZ.Controls.Add(this.button13);
            this.panelSPKZ.Controls.Add(this.button12);
            this.panelSPKZ.Controls.Add(this.button11);
            this.panelSPKZ.Controls.Add(this.button10);
            this.coreBind.SetDatabasecommand(this.panelSPKZ, null);
            this.panelSPKZ.Location = new System.Drawing.Point(0, 28);
            this.panelSPKZ.Name = "panelSPKZ";
            this.panelSPKZ.Size = new System.Drawing.Size(126, 706);
            this.panelSPKZ.TabIndex = 58;
            this.coreBind.SetVerification(this.panelSPKZ, null);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button15, null);
            this.button15.Location = new System.Drawing.Point(35, 231);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(55, 28);
            this.button15.TabIndex = 18;
            this.button15.Tag = "4";
            this.button15.Text = "近";
            this.button15.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button15, null);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button14, null);
            this.button14.Location = new System.Drawing.Point(35, 183);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(55, 28);
            this.button14.TabIndex = 17;
            this.button14.Tag = "5";
            this.button14.Text = "远";
            this.button14.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button14, null);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button13, null);
            this.button13.Location = new System.Drawing.Point(67, 65);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(55, 28);
            this.button13.TabIndex = 16;
            this.button13.Tag = "3";
            this.button13.Text = "右";
            this.button13.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button13, null);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button12, null);
            this.button12.Location = new System.Drawing.Point(5, 64);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(55, 28);
            this.button12.TabIndex = 15;
            this.button12.Tag = "2";
            this.button12.Text = "左";
            this.button12.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button12, null);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button11, null);
            this.button11.Location = new System.Drawing.Point(35, 103);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(55, 28);
            this.button11.TabIndex = 14;
            this.button11.Tag = "1";
            this.button11.Text = "下";
            this.button11.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button11, null);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button10, null);
            this.button10.Location = new System.Drawing.Point(35, 26);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(55, 28);
            this.button10.TabIndex = 13;
            this.button10.Tag = "0";
            this.button10.Text = "上";
            this.button10.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button10, null);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Controls.Add(this.panel10);
            this.coreBind.SetDatabasecommand(this.panel8, null);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel8.Location = new System.Drawing.Point(483, 132);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(741, 289);
            this.panel8.TabIndex = 0;
            this.coreBind.SetVerification(this.panel8, null);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel11.Controls.Add(this.ck_Tare);
            this.panel11.Controls.Add(this.btnTrainTare);
            this.panel11.Controls.Add(this.btnWc);
            this.panel11.Controls.Add(this.btnSglr);
            this.panel11.Controls.Add(this.btnBC);
            this.panel11.Controls.Add(this.btnDS);
            this.coreBind.SetDatabasecommand(this.panel11, null);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(544, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(197, 289);
            this.panel11.TabIndex = 5;
            this.coreBind.SetVerification(this.panel11, null);
            // 
            // ck_Tare
            // 
            this.ck_Tare.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.ck_Tare, null);
            this.ck_Tare.Location = new System.Drawing.Point(22, 100);
            this.ck_Tare.Name = "ck_Tare";
            this.ck_Tare.Size = new System.Drawing.Size(96, 16);
            this.ck_Tare.TabIndex = 31;
            this.ck_Tare.Text = "保存期限皮重";
            this.ck_Tare.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.ck_Tare, null);
            this.ck_Tare.Visible = false;
            // 
            // btnTrainTare
            // 
            this.btnTrainTare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnTrainTare, null);
            this.btnTrainTare.Location = new System.Drawing.Point(108, 138);
            this.btnTrainTare.Name = "btnTrainTare";
            this.btnTrainTare.Size = new System.Drawing.Size(63, 32);
            this.btnTrainTare.TabIndex = 30;
            this.btnTrainTare.Text = "保存火车皮重量";
            this.btnTrainTare.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnTrainTare, null);
            this.btnTrainTare.Visible = false;
            this.btnTrainTare.Click += new System.EventHandler(this.btnTrainTare_Click);
            // 
            // btnWc
            // 
            this.btnWc.BackColor = System.Drawing.Color.Violet;
            this.coreBind.SetDatabasecommand(this.btnWc, null);
            this.btnWc.Location = new System.Drawing.Point(22, 138);
            this.btnWc.Name = "btnWc";
            this.btnWc.Size = new System.Drawing.Size(63, 32);
            this.btnWc.TabIndex = 29;
            this.btnWc.Text = "计量完成";
            this.btnWc.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnWc, null);
            this.btnWc.Visible = false;
            this.btnWc.Click += new System.EventHandler(this.btnWc_Click);
            // 
            // btnSglr
            // 
            this.btnSglr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnSglr, null);
            this.btnSglr.Location = new System.Drawing.Point(22, 198);
            this.btnSglr.Name = "btnSglr";
            this.btnSglr.Size = new System.Drawing.Size(63, 32);
            this.btnSglr.TabIndex = 28;
            this.btnSglr.Text = "手工录入";
            this.btnSglr.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnSglr, null);
            this.btnSglr.Click += new System.EventHandler(this.btnSglr_Click);
            // 
            // btnBC
            // 
            this.btnBC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnBC, null);
            this.btnBC.Location = new System.Drawing.Point(108, 198);
            this.btnBC.Name = "btnBC";
            this.btnBC.Size = new System.Drawing.Size(63, 32);
            this.btnBC.TabIndex = 3;
            this.btnBC.Text = "保存";
            this.btnBC.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnBC, null);
            this.btnBC.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // btnDS
            // 
            this.btnDS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnDS, null);
            this.btnDS.Location = new System.Drawing.Point(9, 22);
            this.btnDS.Name = "btnDS";
            this.btnDS.Size = new System.Drawing.Size(52, 32);
            this.btnDS.TabIndex = 2;
            this.btnDS.Text = "读数";
            this.btnDS.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnDS, null);
            this.btnDS.Visible = false;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.ultraGroupBox2);
            this.coreBind.SetDatabasecommand(this.panel10, null);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(544, 289);
            this.panel10.TabIndex = 1;
            this.coreBind.SetVerification(this.panel10, null);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.HeaderDoubleSolid;
            this.ultraGroupBox2.Controls.Add(this.panel9);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox2, null);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(544, 289);
            this.ultraGroupBox2.TabIndex = 4;
            this.ultraGroupBox2.Text = "称重信息";
            this.coreBind.SetVerification(this.ultraGroupBox2, null);
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Controls.Add(this.btReceiver);
            this.panel9.Controls.Add(this.btSender);
            this.panel9.Controls.Add(this.btTrans);
            this.panel9.Controls.Add(this.btMaterial);
            this.panel9.Controls.Add(this.cbTrans);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Controls.Add(this.txtNetWeight);
            this.panel9.Controls.Add(this.label12);
            this.panel9.Controls.Add(this.txtTareWeight);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Controls.Add(this.cbMaterial);
            this.panel9.Controls.Add(this.label4);
            this.panel9.Controls.Add(this.cbReceiver);
            this.panel9.Controls.Add(this.txtJly);
            this.panel9.Controls.Add(this.txtWeight);
            this.panel9.Controls.Add(this.cbSender);
            this.panel9.Controls.Add(this.cbFlow);
            this.panel9.Controls.Add(this.txtJld);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Controls.Add(this.txtBc);
            this.panel9.Controls.Add(this.label6);
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Controls.Add(this.label14);
            this.panel9.Controls.Add(this.label15);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Controls.Add(this.lblCh);
            this.panel9.Controls.Add(this.txtTRAINNO);
            this.coreBind.SetDatabasecommand(this.panel9, null);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(1, 20);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(542, 268);
            this.panel9.TabIndex = 1;
            this.coreBind.SetVerification(this.panel9, null);
            // 
            // btReceiver
            // 
            this.btReceiver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btReceiver, null);
            this.btReceiver.Location = new System.Drawing.Point(438, 119);
            this.btReceiver.Name = "btReceiver";
            this.btReceiver.Size = new System.Drawing.Size(30, 21);
            this.btReceiver.TabIndex = 698;
            this.btReceiver.Tag = "Receiver";
            this.btReceiver.Text = "..";
            this.btReceiver.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btReceiver, null);
            this.btReceiver.Click += new System.EventHandler(this.btReceiver_Click);
            // 
            // btSender
            // 
            this.btSender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btSender, null);
            this.btSender.Location = new System.Drawing.Point(212, 116);
            this.btSender.Name = "btSender";
            this.btSender.Size = new System.Drawing.Size(30, 21);
            this.btSender.TabIndex = 697;
            this.btSender.Tag = "Sender";
            this.btSender.Text = "..";
            this.btSender.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btSender, null);
            this.btSender.Click += new System.EventHandler(this.btSender_Click);
            // 
            // btTrans
            // 
            this.btTrans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btTrans, null);
            this.btTrans.Location = new System.Drawing.Point(438, 70);
            this.btTrans.Name = "btTrans";
            this.btTrans.Size = new System.Drawing.Size(30, 21);
            this.btTrans.TabIndex = 696;
            this.btTrans.Tag = "Transport";
            this.btTrans.Text = "..";
            this.btTrans.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btTrans, null);
            this.btTrans.Click += new System.EventHandler(this.btTrans_Click);
            // 
            // btMaterial
            // 
            this.btMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btMaterial, null);
            this.btMaterial.Location = new System.Drawing.Point(212, 69);
            this.btMaterial.Name = "btMaterial";
            this.btMaterial.Size = new System.Drawing.Size(30, 21);
            this.btMaterial.TabIndex = 695;
            this.btMaterial.Tag = "Material";
            this.btMaterial.Text = "..";
            this.btMaterial.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btMaterial, null);
            this.btMaterial.Click += new System.EventHandler(this.btMaterial_Click);
            // 
            // cbTrans
            // 
            this.coreBind.SetDatabasecommand(this.cbTrans, null);
            this.cbTrans.FormattingEnabled = true;
            this.cbTrans.Location = new System.Drawing.Point(316, 70);
            this.cbTrans.Name = "cbTrans";
            this.cbTrans.Size = new System.Drawing.Size(119, 20);
            this.cbTrans.TabIndex = 615;
            this.cbTrans.Tag = "Transport";
            this.coreBind.SetVerification(this.cbTrans, null);
            this.cbTrans.Leave += new System.EventHandler(this.cbTrans_Leave);
            this.cbTrans.TextChanged += new System.EventHandler(this.cbTrans_TextChanged);
            // 
            // label1
            // 
            this.coreBind.SetDatabasecommand(this.label1, null);
            this.label1.Location = new System.Drawing.Point(250, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 24);
            this.label1.TabIndex = 616;
            this.label1.Text = "承运单位";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label1, null);
            // 
            // txtNetWeight
            // 
            this.txtNetWeight.AcceptsTab = true;
            this.txtNetWeight.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtNetWeight, null);
            this.txtNetWeight.Font = new System.Drawing.Font("宋体", 11F);
            this.txtNetWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNetWeight.Location = new System.Drawing.Point(395, 167);
            this.txtNetWeight.MaxLength = 8;
            this.txtNetWeight.Name = "txtNetWeight";
            this.txtNetWeight.ReadOnly = true;
            this.txtNetWeight.Size = new System.Drawing.Size(73, 24);
            this.txtNetWeight.TabIndex = 613;
            this.coreBind.SetVerification(this.txtNetWeight, null);
            // 
            // label12
            // 
            this.coreBind.SetDatabasecommand(this.label12, null);
            this.label12.Location = new System.Drawing.Point(14, 167);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 24);
            this.label12.TabIndex = 614;
            this.label12.Text = "总重(t)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label12, null);
            // 
            // txtTareWeight
            // 
            this.txtTareWeight.AcceptsTab = true;
            this.txtTareWeight.BackColor = System.Drawing.Color.White;
            this.coreBind.SetDatabasecommand(this.txtTareWeight, null);
            this.txtTareWeight.Font = new System.Drawing.Font("宋体", 11F);
            this.txtTareWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTareWeight.Location = new System.Drawing.Point(249, 167);
            this.txtTareWeight.MaxLength = 8;
            this.txtTareWeight.Name = "txtTareWeight";
            this.txtTareWeight.Size = new System.Drawing.Size(65, 24);
            this.txtTareWeight.TabIndex = 611;
            this.coreBind.SetVerification(this.txtTareWeight, null);
            this.txtTareWeight.Leave += new System.EventHandler(this.txtTareWeight_Leave);
            // 
            // label9
            // 
            this.coreBind.SetDatabasecommand(this.label9, null);
            this.label9.Location = new System.Drawing.Point(162, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 24);
            this.label9.TabIndex = 612;
            this.label9.Text = "车皮重量(t)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label9, null);
            // 
            // cbMaterial
            // 
            this.coreBind.SetDatabasecommand(this.cbMaterial, null);
            this.cbMaterial.FormattingEnabled = true;
            this.cbMaterial.Location = new System.Drawing.Point(69, 70);
            this.cbMaterial.Name = "cbMaterial";
            this.cbMaterial.Size = new System.Drawing.Size(137, 20);
            this.cbMaterial.TabIndex = 104;
            this.cbMaterial.Tag = "Material";
            this.coreBind.SetVerification(this.cbMaterial, null);
            this.cbMaterial.Leave += new System.EventHandler(this.cbMaterial_Leave);
            this.cbMaterial.TextChanged += new System.EventHandler(this.cbMaterial_TextChanged);
            // 
            // label4
            // 
            this.coreBind.SetDatabasecommand(this.label4, null);
            this.label4.Location = new System.Drawing.Point(11, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 24);
            this.label4.TabIndex = 606;
            this.label4.Text = "物料名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label4, null);
            // 
            // cbReceiver
            // 
            this.cbReceiver.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.coreBind.SetDatabasecommand(this.cbReceiver, null);
            this.cbReceiver.FormattingEnabled = true;
            this.cbReceiver.Location = new System.Drawing.Point(318, 120);
            this.cbReceiver.Name = "cbReceiver";
            this.cbReceiver.Size = new System.Drawing.Size(117, 20);
            this.cbReceiver.TabIndex = 108;
            this.cbReceiver.Tag = "Receiver";
            this.coreBind.SetVerification(this.cbReceiver, null);
            this.cbReceiver.Leave += new System.EventHandler(this.cbReceiver_Leave);
            this.cbReceiver.TextChanged += new System.EventHandler(this.cbReceiver_TextChanged);
            // 
            // txtJly
            // 
            this.txtJly.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtJly, null);
            this.txtJly.Font = new System.Drawing.Font("宋体", 11F);
            this.txtJly.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJly.Location = new System.Drawing.Point(277, 221);
            this.txtJly.MaxLength = 8;
            this.txtJly.Name = "txtJly";
            this.txtJly.ReadOnly = true;
            this.txtJly.Size = new System.Drawing.Size(93, 24);
            this.txtJly.TabIndex = 110;
            this.coreBind.SetVerification(this.txtJly, null);
            // 
            // txtWeight
            // 
            this.txtWeight.AcceptsTab = true;
            this.txtWeight.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtWeight, null);
            this.txtWeight.Font = new System.Drawing.Font("宋体", 11F);
            this.txtWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtWeight.Location = new System.Drawing.Point(69, 167);
            this.txtWeight.MaxLength = 8;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(65, 24);
            this.txtWeight.TabIndex = 112;
            this.coreBind.SetVerification(this.txtWeight, null);
            this.txtWeight.Leave += new System.EventHandler(this.txtWeight_Leave);
            // 
            // cbSender
            // 
            this.coreBind.SetDatabasecommand(this.cbSender, null);
            this.cbSender.FormattingEnabled = true;
            this.cbSender.Location = new System.Drawing.Point(69, 118);
            this.cbSender.Name = "cbSender";
            this.cbSender.Size = new System.Drawing.Size(137, 20);
            this.cbSender.TabIndex = 107;
            this.cbSender.Tag = "Sender";
            this.coreBind.SetVerification(this.cbSender, null);
            this.cbSender.Leave += new System.EventHandler(this.cbSender_Leave);
            this.cbSender.TextChanged += new System.EventHandler(this.cbSender_TextChanged);
            // 
            // cbFlow
            // 
            this.coreBind.SetDatabasecommand(this.cbFlow, null);
            this.cbFlow.FormattingEnabled = true;
            this.cbFlow.Location = new System.Drawing.Point(316, 20);
            this.cbFlow.Name = "cbFlow";
            this.cbFlow.Size = new System.Drawing.Size(119, 20);
            this.cbFlow.TabIndex = 102;
            this.coreBind.SetVerification(this.cbFlow, null);
            // 
            // txtJld
            // 
            this.txtJld.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtJld, null);
            this.txtJld.Font = new System.Drawing.Font("宋体", 11F);
            this.txtJld.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJld.Location = new System.Drawing.Point(69, 221);
            this.txtJld.MaxLength = 8;
            this.txtJld.Name = "txtJld";
            this.txtJld.ReadOnly = true;
            this.txtJld.Size = new System.Drawing.Size(147, 24);
            this.txtJld.TabIndex = 109;
            this.coreBind.SetVerification(this.txtJld, null);
            // 
            // label5
            // 
            this.coreBind.SetDatabasecommand(this.label5, null);
            this.label5.Location = new System.Drawing.Point(376, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 24);
            this.label5.TabIndex = 599;
            this.label5.Text = "班次";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label5, null);
            // 
            // txtBc
            // 
            this.txtBc.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtBc, null);
            this.txtBc.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBc.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBc.Location = new System.Drawing.Point(412, 221);
            this.txtBc.MaxLength = 8;
            this.txtBc.Name = "txtBc";
            this.txtBc.ReadOnly = true;
            this.txtBc.Size = new System.Drawing.Size(53, 24);
            this.txtBc.TabIndex = 111;
            this.coreBind.SetVerification(this.txtBc, null);
            // 
            // label6
            // 
            this.coreBind.SetDatabasecommand(this.label6, null);
            this.label6.Location = new System.Drawing.Point(340, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 24);
            this.label6.TabIndex = 597;
            this.label6.Text = "净重(t)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label6, null);
            // 
            // label7
            // 
            this.coreBind.SetDatabasecommand(this.label7, null);
            this.label7.Location = new System.Drawing.Point(229, 221);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 24);
            this.label7.TabIndex = 595;
            this.label7.Text = "计量员";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label7, null);
            // 
            // label8
            // 
            this.coreBind.SetDatabasecommand(this.label8, null);
            this.label8.Location = new System.Drawing.Point(11, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 24);
            this.label8.TabIndex = 593;
            this.label8.Text = "计量点";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label8, null);
            // 
            // label14
            // 
            this.coreBind.SetDatabasecommand(this.label14, null);
            this.label14.Location = new System.Drawing.Point(241, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 24);
            this.label14.TabIndex = 591;
            this.label14.Text = "收货单位";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label14, null);
            // 
            // label15
            // 
            this.coreBind.SetDatabasecommand(this.label15, null);
            this.label15.Location = new System.Drawing.Point(9, 116);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 24);
            this.label15.TabIndex = 590;
            this.label15.Text = "发货单位";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label15, null);
            // 
            // label11
            // 
            this.coreBind.SetDatabasecommand(this.label11, null);
            this.label11.Location = new System.Drawing.Point(268, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 24);
            this.label11.TabIndex = 589;
            this.label11.Text = "流向";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label11, null);
            // 
            // lblCh
            // 
            this.coreBind.SetDatabasecommand(this.lblCh, null);
            this.lblCh.Location = new System.Drawing.Point(11, 17);
            this.lblCh.Name = "lblCh";
            this.lblCh.Size = new System.Drawing.Size(58, 24);
            this.lblCh.TabIndex = 578;
            this.lblCh.Text = "车号";
            this.lblCh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.lblCh, null);
            // 
            // txtTRAINNO
            // 
            this.txtTRAINNO.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtTRAINNO, null);
            this.txtTRAINNO.Font = new System.Drawing.Font("宋体", 11F);
            this.txtTRAINNO.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTRAINNO.Location = new System.Drawing.Point(69, 17);
            this.txtTRAINNO.MaxLength = 8;
            this.txtTRAINNO.Name = "txtTRAINNO";
            this.txtTRAINNO.Size = new System.Drawing.Size(137, 24);
            this.txtTRAINNO.TabIndex = 101;
            this.coreBind.SetVerification(this.txtTRAINNO, null);
            this.txtTRAINNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTRAINNO_KeyPress);
            // 
            // picFDTP
            // 
            this.coreBind.SetDatabasecommand(this.picFDTP, null);
            this.picFDTP.Location = new System.Drawing.Point(70, 85);
            this.picFDTP.Name = "picFDTP";
            this.picFDTP.Size = new System.Drawing.Size(20, 13);
            this.picFDTP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFDTP.TabIndex = 2;
            this.picFDTP.TabStop = false;
            this.coreBind.SetVerification(this.picFDTP, null);
            this.picFDTP.Visible = false;
            this.picFDTP.DoubleClick += new System.EventHandler(this.picFDTP_DoubleClick);
            this.picFDTP.MouseLeave += new System.EventHandler(this.picFDTP_MouseLeave);
            this.picFDTP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseMove);
            this.picFDTP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseDown);
            this.picFDTP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseUp);
            // 
            // btnQL
            // 
            this.coreBind.SetDatabasecommand(this.btnQL, null);
            this.btnQL.Location = new System.Drawing.Point(251, 103);
            this.btnQL.Name = "btnQL";
            this.btnQL.Size = new System.Drawing.Size(99, 28);
            this.btnQL.TabIndex = 56;
            this.btnQL.Text = "清零";
            this.btnQL.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.btnQL, null);
            this.btnQL.Click += new System.EventHandler(this.btnQL_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.btnQL);
            this.panel6.Controls.Add(this.ultraGroupBox1);
            this.coreBind.SetDatabasecommand(this.panel6, null);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(483, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(741, 105);
            this.panel6.TabIndex = 55;
            this.coreBind.SetVerification(this.panel6, null);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.picFDTP);
            this.ultraGroupBox1.Controls.Add(this.txtMeterWeight);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.lblMaterShow);
            this.ultraGroupBox1.Controls.Add(this.lblMater);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox1, null);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(737, 101);
            this.ultraGroupBox1.TabIndex = 2;
            this.ultraGroupBox1.Text = "重量信息";
            this.coreBind.SetVerification(this.ultraGroupBox1, null);
            // 
            // txtMeterWeight
            // 
            this.txtMeterWeight.BackColor = System.Drawing.Color.Transparent;
            this.txtMeterWeight.BackColor_1 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtMeterWeight.BackColor_2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtMeterWeight.BevelRate = 0.5F;
            this.coreBind.SetDatabasecommand(this.txtMeterWeight, null);
            this.txtMeterWeight.FadedColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtMeterWeight.ForeColor = System.Drawing.Color.Green;
            this.txtMeterWeight.HighlightOpaque = ((byte)(50));
            this.txtMeterWeight.Location = new System.Drawing.Point(57, 17);
            this.txtMeterWeight.Name = "txtMeterWeight";
            this.txtMeterWeight.Size = new System.Drawing.Size(274, 67);
            this.txtMeterWeight.TabIndex = 673;
            this.txtMeterWeight.Text = "0.000";
            this.txtMeterWeight.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.txtMeterWeight.TotalCharCount = 8;
            this.coreBind.SetVerification(this.txtMeterWeight, null);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label2, null);
            this.label2.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(369, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 56);
            this.label2.TabIndex = 53;
            this.label2.Text = "吨";
            this.coreBind.SetVerification(this.label2, null);
            // 
            // lblMaterShow
            // 
            this.lblMaterShow.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.lblMaterShow, null);
            this.lblMaterShow.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMaterShow.Location = new System.Drawing.Point(486, 46);
            this.lblMaterShow.Name = "lblMaterShow";
            this.lblMaterShow.Size = new System.Drawing.Size(120, 21);
            this.lblMaterShow.TabIndex = 55;
            this.lblMaterShow.Text = "未连接仪表";
            this.coreBind.SetVerification(this.lblMaterShow, null);
            // 
            // lblMater
            // 
            this.coreBind.SetDatabasecommand(this.lblMater, null);
            this.lblMater.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMater.ForeColor = System.Drawing.Color.Crimson;
            this.lblMater.Location = new System.Drawing.Point(458, 44);
            this.lblMater.Name = "lblMater";
            this.lblMater.Size = new System.Drawing.Size(30, 28);
            this.lblMater.TabIndex = 54;
            this.lblMater.Text = "●";
            this.coreBind.SetVerification(this.lblMater, null);
            // 
            // BilletInfo_GD_Fill_Panel
            // 
            this.BilletInfo_GD_Fill_Panel.Controls.Add(this.panel7);
            this.BilletInfo_GD_Fill_Panel.Controls.Add(this.panel8);
            this.BilletInfo_GD_Fill_Panel.Controls.Add(this.panel6);
            this.BilletInfo_GD_Fill_Panel.Controls.Add(this.panel2);
            this.BilletInfo_GD_Fill_Panel.Controls.Add(this.panel1);
            this.BilletInfo_GD_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.BilletInfo_GD_Fill_Panel, null);
            this.BilletInfo_GD_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BilletInfo_GD_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.BilletInfo_GD_Fill_Panel.Name = "BilletInfo_GD_Fill_Panel";
            this.BilletInfo_GD_Fill_Panel.Size = new System.Drawing.Size(1224, 578);
            this.BilletInfo_GD_Fill_Panel.TabIndex = 1;
            this.coreBind.SetVerification(this.BilletInfo_GD_Fill_Panel, null);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.ultraTabControl1);
            this.coreBind.SetDatabasecommand(this.panel7, null);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(483, 421);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(741, 157);
            this.panel7.TabIndex = 57;
            this.coreBind.SetVerification(this.panel7, null);
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.coreBind.SetDatabasecommand(this.ultraTabControl1, null);
            this.ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(737, 153);
            this.ultraTabControl1.TabIndex = 0;
            ultraTab2.TabPage = this.ultraTabPageControl2;
            ultraTab2.Text = "计量信息";
            ultraTab1.TabPage = this.ultraTabPageControl1;
            ultraTab1.Text = "一次计量信息";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab2,
            ultraTab1});
            this.coreBind.SetVerification(this.ultraTabControl1, null);
            this.ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007;
            // 
            // ultraTabSharedControlsPage1
            // 
            this.coreBind.SetDatabasecommand(this.ultraTabSharedControlsPage1, null);
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(735, 130);
            this.coreBind.SetVerification(this.ultraTabSharedControlsPage1, null);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pnlBottom);
            this.coreBind.SetDatabasecommand(this.panel2, null);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(483, 551);
            this.panel2.TabIndex = 2;
            this.coreBind.SetVerification(this.panel2, null);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.panel5);
            this.pnlBottom.Controls.Add(this.panel3);
            this.pnlBottom.Controls.Add(this.panel4);
            this.coreBind.SetDatabasecommand(this.pnlBottom, null);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(479, 705);
            this.pnlBottom.TabIndex = 3;
            this.coreBind.SetVerification(this.pnlBottom, null);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.VideoChannel3);
            this.coreBind.SetDatabasecommand(this.panel5, null);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 470);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(479, 235);
            this.panel5.TabIndex = 6;
            this.coreBind.SetVerification(this.panel5, null);
            // 
            // VideoChannel3
            // 
            this.VideoChannel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel3, null);
            this.VideoChannel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel3.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel3.Name = "VideoChannel3";
            this.VideoChannel3.Size = new System.Drawing.Size(479, 235);
            this.VideoChannel3.TabIndex = 1;
            this.VideoChannel3.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel3, null);
            this.VideoChannel3.DoubleClick += new System.EventHandler(this.VideoChannel3_DoubleClick);
            this.VideoChannel3.Click += new System.EventHandler(this.picBf_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.VideoChannel2);
            this.coreBind.SetDatabasecommand(this.panel3, null);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 235);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(479, 235);
            this.panel3.TabIndex = 5;
            this.coreBind.SetVerification(this.panel3, null);
            // 
            // VideoChannel2
            // 
            this.VideoChannel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoChannel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.VideoChannel2, null);
            this.VideoChannel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel2.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel2.Name = "VideoChannel2";
            this.VideoChannel2.Size = new System.Drawing.Size(479, 235);
            this.VideoChannel2.TabIndex = 1;
            this.VideoChannel2.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel2, null);
            this.VideoChannel2.DoubleClick += new System.EventHandler(this.VideoChannel2_DoubleClick);
            this.VideoChannel2.Click += new System.EventHandler(this.pic12_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.VideoChannel1);
            this.coreBind.SetDatabasecommand(this.panel4, null);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel4.Size = new System.Drawing.Size(479, 235);
            this.panel4.TabIndex = 4;
            this.coreBind.SetVerification(this.panel4, null);
            // 
            // VideoChannel1
            // 
            this.VideoChannel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel1, null);
            this.VideoChannel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel1.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel1.Name = "VideoChannel1";
            this.VideoChannel1.Size = new System.Drawing.Size(479, 235);
            this.VideoChannel1.TabIndex = 1;
            this.VideoChannel1.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel1, null);
            this.VideoChannel1.DoubleClick += new System.EventHandler(this.pic11_DoubleClick);
            this.VideoChannel1.Click += new System.EventHandler(this.pic11_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpEnd);
            this.panel1.Controls.Add(this.panel1_Fill_Panel);
            this.panel1.Controls.Add(this.dateRQ);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Left);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Right);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Top);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Bottom);
            this.coreBind.SetDatabasecommand(this.panel1, null);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1224, 27);
            this.panel1.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1, null);
            // 
            // dtpEnd
            // 
            this.coreBind.SetDatabasecommand(this.dtpEnd, null);
            this.dtpEnd.Location = new System.Drawing.Point(226, 2);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(116, 21);
            this.dtpEnd.TabIndex = 674;
            this.coreBind.SetVerification(this.dtpEnd, null);
            // 
            // panel1_Fill_Panel
            // 
            this.panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.panel1_Fill_Panel, null);
            this.panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1_Fill_Panel.Location = new System.Drawing.Point(0, 28);
            this.panel1_Fill_Panel.Name = "panel1_Fill_Panel";
            this.panel1_Fill_Panel.Size = new System.Drawing.Size(1224, 0);
            this.panel1_Fill_Panel.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1_Fill_Panel, null);
            // 
            // dateRQ
            // 
            this.coreBind.SetDatabasecommand(this.dateRQ, null);
            this.dateRQ.Location = new System.Drawing.Point(94, 3);
            this.dateRQ.Name = "dateRQ";
            this.dateRQ.Size = new System.Drawing.Size(110, 21);
            this.dateRQ.TabIndex = 57;
            this.coreBind.SetVerification(this.dateRQ, null);
            // 
            // _panel1_Toolbars_Dock_Area_Left
            // 
            this._panel1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Left, null);
            this._panel1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._panel1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 28);
            this._panel1_Toolbars_Dock_Area_Left.Name = "_panel1_Toolbars_Dock_Area_Left";
            this._panel1_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 0);
            this._panel1_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Left, null);
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this.panel1;
            this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            controlContainerTool4.ControlName = "dateRQ";
            controlContainerTool4.InstanceProps.IsFirstInGroup = true;
            controlContainerTool5.ControlName = "dtpEnd";
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            controlContainerTool4,
            controlContainerTool5,
            buttonTool2,
            buttonTool3,
            buttonTool6});
            ultraToolbar1.Text = "UltraToolbar1";
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            controlContainerTool1.ControlName = "dateRQ";
            controlContainerTool1.SharedPropsInternal.Caption = "毛重计量日期";
            controlContainerTool1.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool2.SharedPropsInternal.Caption = "罐号";
            controlContainerTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            appearance1.Image = ((object)(resources.GetObject("appearance1.Image")));
            buttonTool1.SharedPropsInternal.AppearancesSmall.Appearance = appearance1;
            buttonTool1.SharedPropsInternal.Caption = "查询";
            buttonTool1.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool4.SharedPropsInternal.Caption = "打开对讲";
            buttonTool4.SharedPropsInternal.CustomizerCaption = "音频";
            buttonTool4.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool5.SharedPropsInternal.Caption = "查询";
            buttonTool5.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool7.SharedPropsInternal.Caption = "校秤";
            buttonTool7.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            controlContainerTool6.ControlName = "dtpEnd";
            controlContainerTool6.SharedPropsInternal.Caption = "至";
            controlContainerTool6.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            controlContainerTool1,
            controlContainerTool2,
            buttonTool1,
            buttonTool4,
            buttonTool5,
            buttonTool7,
            controlContainerTool6});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
            // 
            // _panel1_Toolbars_Dock_Area_Right
            // 
            this._panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Right, null);
            this._panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1224, 28);
            this._panel1_Toolbars_Dock_Area_Right.Name = "_panel1_Toolbars_Dock_Area_Right";
            this._panel1_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 0);
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
            this._panel1_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1224, 28);
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
            this._panel1_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 27);
            this._panel1_Toolbars_Dock_Area_Bottom.Name = "_panel1_Toolbars_Dock_Area_Bottom";
            this._panel1_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1224, 0);
            this._panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Bottom, null);
            // 
            // ultraDockManager1
            // 
            this.ultraDockManager1.CompressUnpinnedTabs = false;
            dockAreaPane1.DockedBefore = new System.Guid("777aa848-96d9-4a9c-8e57-ab46776d741c");
            dockableControlPane1.Control = this.panelYYBF;
            dockableControlPane1.FlyoutSize = new System.Drawing.Size(95, -1);
            dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(3, 73, 200, 100);
            dockableControlPane1.Pinned = false;
            dockableControlPane1.Size = new System.Drawing.Size(100, 100);
            dockableControlPane1.Text = "语音播报";
            dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane1});
            dockAreaPane1.Size = new System.Drawing.Size(95, 666);
            dockableControlPane2.Control = this.panelSPKZ;
            dockableControlPane2.FlyoutSize = new System.Drawing.Size(126, -1);
            dockableControlPane2.OriginalControlBounds = new System.Drawing.Rectangle(265, 22, 200, 100);
            dockableControlPane2.Pinned = false;
            dockableControlPane2.Size = new System.Drawing.Size(100, 100);
            dockableControlPane2.Text = "视频控制";
            dockAreaPane2.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane2});
            dockAreaPane2.Size = new System.Drawing.Size(95, 666);
            this.ultraDockManager1.DockAreas.AddRange(new Infragistics.Win.UltraWinDock.DockAreaPane[] {
            dockAreaPane1,
            dockAreaPane2});
            this.ultraDockManager1.HostControl = this;
            this.ultraDockManager1.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.Office2003;
            // 
            // _MoltenInfo_OneUnpinnedTabAreaLeft
            // 
            this.coreBind.SetDatabasecommand(this._MoltenInfo_OneUnpinnedTabAreaLeft, null);
            this._MoltenInfo_OneUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._MoltenInfo_OneUnpinnedTabAreaLeft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MoltenInfo_OneUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 0);
            this._MoltenInfo_OneUnpinnedTabAreaLeft.Name = "_MoltenInfo_OneUnpinnedTabAreaLeft";
            this._MoltenInfo_OneUnpinnedTabAreaLeft.Owner = this.ultraDockManager1;
            this._MoltenInfo_OneUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 578);
            this._MoltenInfo_OneUnpinnedTabAreaLeft.TabIndex = 2;
            this.coreBind.SetVerification(this._MoltenInfo_OneUnpinnedTabAreaLeft, null);
            // 
            // _MoltenInfo_OneUnpinnedTabAreaRight
            // 
            this.coreBind.SetDatabasecommand(this._MoltenInfo_OneUnpinnedTabAreaRight, null);
            this._MoltenInfo_OneUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._MoltenInfo_OneUnpinnedTabAreaRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MoltenInfo_OneUnpinnedTabAreaRight.Location = new System.Drawing.Point(1224, 0);
            this._MoltenInfo_OneUnpinnedTabAreaRight.Name = "_MoltenInfo_OneUnpinnedTabAreaRight";
            this._MoltenInfo_OneUnpinnedTabAreaRight.Owner = this.ultraDockManager1;
            this._MoltenInfo_OneUnpinnedTabAreaRight.Size = new System.Drawing.Size(21, 578);
            this._MoltenInfo_OneUnpinnedTabAreaRight.TabIndex = 3;
            this.coreBind.SetVerification(this._MoltenInfo_OneUnpinnedTabAreaRight, null);
            // 
            // _MoltenInfo_OneUnpinnedTabAreaTop
            // 
            this.coreBind.SetDatabasecommand(this._MoltenInfo_OneUnpinnedTabAreaTop, null);
            this._MoltenInfo_OneUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._MoltenInfo_OneUnpinnedTabAreaTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MoltenInfo_OneUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 0);
            this._MoltenInfo_OneUnpinnedTabAreaTop.Name = "_MoltenInfo_OneUnpinnedTabAreaTop";
            this._MoltenInfo_OneUnpinnedTabAreaTop.Owner = this.ultraDockManager1;
            this._MoltenInfo_OneUnpinnedTabAreaTop.Size = new System.Drawing.Size(1224, 0);
            this._MoltenInfo_OneUnpinnedTabAreaTop.TabIndex = 4;
            this.coreBind.SetVerification(this._MoltenInfo_OneUnpinnedTabAreaTop, null);
            // 
            // _MoltenInfo_OneUnpinnedTabAreaBottom
            // 
            this.coreBind.SetDatabasecommand(this._MoltenInfo_OneUnpinnedTabAreaBottom, null);
            this._MoltenInfo_OneUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._MoltenInfo_OneUnpinnedTabAreaBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MoltenInfo_OneUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 578);
            this._MoltenInfo_OneUnpinnedTabAreaBottom.Name = "_MoltenInfo_OneUnpinnedTabAreaBottom";
            this._MoltenInfo_OneUnpinnedTabAreaBottom.Owner = this.ultraDockManager1;
            this._MoltenInfo_OneUnpinnedTabAreaBottom.Size = new System.Drawing.Size(1224, 0);
            this._MoltenInfo_OneUnpinnedTabAreaBottom.TabIndex = 5;
            this.coreBind.SetVerification(this._MoltenInfo_OneUnpinnedTabAreaBottom, null);
            // 
            // _MoltenInfo_OneAutoHideControl
            // 
            this._MoltenInfo_OneAutoHideControl.Controls.Add(this.dockableWindow1);
            this._MoltenInfo_OneAutoHideControl.Controls.Add(this.dockableWindow2);
            this.coreBind.SetDatabasecommand(this._MoltenInfo_OneAutoHideControl, null);
            this._MoltenInfo_OneAutoHideControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MoltenInfo_OneAutoHideControl.Location = new System.Drawing.Point(985, 0);
            this._MoltenInfo_OneAutoHideControl.Name = "_MoltenInfo_OneAutoHideControl";
            this._MoltenInfo_OneAutoHideControl.Owner = this.ultraDockManager1;
            this._MoltenInfo_OneAutoHideControl.Size = new System.Drawing.Size(10, 734);
            this._MoltenInfo_OneAutoHideControl.TabIndex = 6;
            this.coreBind.SetVerification(this._MoltenInfo_OneAutoHideControl, null);
            // 
            // dockableWindow1
            // 
            this.dockableWindow1.Controls.Add(this.panelYYBF);
            this.coreBind.SetDatabasecommand(this.dockableWindow1, null);
            this.dockableWindow1.Location = new System.Drawing.Point(-10000, 0);
            this.dockableWindow1.Name = "dockableWindow1";
            this.dockableWindow1.Owner = this.ultraDockManager1;
            this.dockableWindow1.Size = new System.Drawing.Size(95, 734);
            this.dockableWindow1.TabIndex = 9;
            this.coreBind.SetVerification(this.dockableWindow1, null);
            // 
            // dockableWindow2
            // 
            this.dockableWindow2.Controls.Add(this.panelSPKZ);
            this.coreBind.SetDatabasecommand(this.dockableWindow2, null);
            this.dockableWindow2.Location = new System.Drawing.Point(5, 0);
            this.dockableWindow2.Name = "dockableWindow2";
            this.dockableWindow2.Owner = this.ultraDockManager1;
            this.dockableWindow2.Size = new System.Drawing.Size(126, 734);
            this.dockableWindow2.TabIndex = 10;
            this.coreBind.SetVerification(this.dockableWindow2, null);
            // 
            // windowDockingArea1
            // 
            this.coreBind.SetDatabasecommand(this.windowDockingArea1, null);
            this.windowDockingArea1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowDockingArea1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowDockingArea1.Location = new System.Drawing.Point(871, 0);
            this.windowDockingArea1.Name = "windowDockingArea1";
            this.windowDockingArea1.Owner = this.ultraDockManager1;
            this.windowDockingArea1.Size = new System.Drawing.Size(100, 666);
            this.windowDockingArea1.TabIndex = 7;
            this.coreBind.SetVerification(this.windowDockingArea1, null);
            // 
            // windowDockingArea2
            // 
            this.coreBind.SetDatabasecommand(this.windowDockingArea2, null);
            this.windowDockingArea2.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowDockingArea2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowDockingArea2.Location = new System.Drawing.Point(871, 0);
            this.windowDockingArea2.Name = "windowDockingArea2";
            this.windowDockingArea2.Owner = this.ultraDockManager1;
            this.windowDockingArea2.Size = new System.Drawing.Size(100, 666);
            this.windowDockingArea2.TabIndex = 8;
            this.coreBind.SetVerification(this.windowDockingArea2, null);
            // 
            // dsQuery
            // 
            this.dsQuery.DataSetName = "NewDataSet";
            this.dsQuery.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable4});
            // 
            // dataTable4
            // 
            this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn120,
            this.dataColumn121,
            this.dataColumn122,
            this.dataColumn123,
            this.dataColumn124,
            this.dataColumn125,
            this.dataColumn126,
            this.dataColumn127,
            this.dataColumn128,
            this.dataColumn129,
            this.dataColumn130,
            this.dataColumn131,
            this.dataColumn132,
            this.dataColumn133,
            this.dataColumn134,
            this.dataColumn135,
            this.dataColumn136,
            this.dataColumn137,
            this.dataColumn138,
            this.dataColumn139,
            this.dataColumn140,
            this.dataColumn141});
            this.dataTable4.TableName = "一次计量临时表";
            // 
            // dataColumn120
            // 
            this.dataColumn120.Caption = "操作编号";
            this.dataColumn120.ColumnName = "fs_weightno";
            // 
            // dataColumn121
            // 
            this.dataColumn121.Caption = "物料代码";
            this.dataColumn121.ColumnName = "fs_material";
            // 
            // dataColumn122
            // 
            this.dataColumn122.Caption = "流向代码";
            this.dataColumn122.ColumnName = "fs_weighttype";
            // 
            // dataColumn123
            // 
            this.dataColumn123.Caption = "发货单位代码";
            this.dataColumn123.ColumnName = "fs_senderstroeno";
            // 
            // dataColumn124
            // 
            this.dataColumn124.Caption = "收货单位代码";
            this.dataColumn124.ColumnName = "fs_receivestoreno";
            // 
            // dataColumn125
            // 
            this.dataColumn125.Caption = "班次";
            this.dataColumn125.ColumnName = "fs_shift";
            // 
            // dataColumn126
            // 
            this.dataColumn126.Caption = "班组";
            this.dataColumn126.ColumnName = "fs_group";
            // 
            // dataColumn127
            // 
            this.dataColumn127.Caption = "车号";
            this.dataColumn127.ColumnName = "fs_trainno";
            // 
            // dataColumn128
            // 
            this.dataColumn128.Caption = "重量";
            this.dataColumn128.ColumnName = "fn_weight";
            // 
            // dataColumn129
            // 
            this.dataColumn129.Caption = "计量员";
            this.dataColumn129.ColumnName = "fs_weightperson";
            // 
            // dataColumn130
            // 
            this.dataColumn130.Caption = "计量时间";
            this.dataColumn130.ColumnName = "fd_weighttime";
            // 
            // dataColumn131
            // 
            this.dataColumn131.Caption = "计量点";
            this.dataColumn131.ColumnName = "fs_weightpoint";
            // 
            // dataColumn132
            // 
            this.dataColumn132.Caption = "删除标志";
            this.dataColumn132.ColumnName = "fs_deleteflag";
            // 
            // dataColumn133
            // 
            this.dataColumn133.Caption = "删除人";
            this.dataColumn133.ColumnName = "fs_deleteuser";
            // 
            // dataColumn134
            // 
            this.dataColumn134.Caption = "删除日期";
            this.dataColumn134.ColumnName = "fd_deletedate";
            // 
            // dataColumn135
            // 
            this.dataColumn135.Caption = "承运单位代码";
            this.dataColumn135.ColumnName = "fs_transno";
            // 
            // dataColumn136
            // 
            this.dataColumn136.Caption = "物料名称";
            this.dataColumn136.ColumnName = "fs_materialname";
            // 
            // dataColumn137
            // 
            this.dataColumn137.Caption = "发货单位";
            this.dataColumn137.ColumnName = "fs_sender";
            // 
            // dataColumn138
            // 
            this.dataColumn138.Caption = "收货单位";
            this.dataColumn138.ColumnName = "fs_receiver";
            // 
            // dataColumn139
            // 
            this.dataColumn139.Caption = "流向";
            this.dataColumn139.ColumnName = "fs_typename";
            // 
            // dataColumn140
            // 
            this.dataColumn140.Caption = "计量点";
            this.dataColumn140.ColumnName = "fs_pointname";
            // 
            // dataColumn141
            // 
            this.dataColumn141.Caption = "承运单位";
            this.dataColumn141.ColumnName = "fs_trans";
            // 
            // TrackWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 578);
            this.Controls.Add(this._MoltenInfo_OneAutoHideControl);
            this.Controls.Add(this.BilletInfo_GD_Fill_Panel);
            this.Controls.Add(this.windowDockingArea1);
            this.Controls.Add(this.windowDockingArea2);
            this.Controls.Add(this._MoltenInfo_OneUnpinnedTabAreaTop);
            this.Controls.Add(this._MoltenInfo_OneUnpinnedTabAreaBottom);
            this.Controls.Add(this._MoltenInfo_OneUnpinnedTabAreaLeft);
            this.Controls.Add(this._MoltenInfo_OneUnpinnedTabAreaRight);
            this.coreBind.SetDatabasecommand(this, null);
            this.Name = "TrackWeight";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Tag = "TrackWeight";
            this.Text = "静态轨道衡";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.MoltenInfo_One_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MoltenInfo_One_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MoltenInfo_One_FormClosing);
            this.ultraTabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable8)).EndInit();
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
            this.panelYYBF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uDridSound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).EndInit();
            this.panelSPKZ.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeterWeight)).EndInit();
            this.BilletInfo_GD_Fill_Panel.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel3)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).EndInit();
            this._MoltenInfo_OneAutoHideControl.ResumeLayout(false);
            this.dockableWindow1.ResumeLayout(false);
            this.dockableWindow2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsQuery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn21;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnQL;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblMaterShow;
        private System.Windows.Forms.Label lblMater;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel BilletInfo_GD_Fill_Panel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel1_Fill_Panel;
        private System.Windows.Forms.DateTimePicker dateRQ;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;
        private System.Windows.Forms.Panel panelSPKZ;
        private System.Windows.Forms.Panel panelYYBF;
        private Infragistics.Win.UltraWinDock.UltraDockManager ultraDockManager1;
        private Infragistics.Win.UltraWinDock.AutoHideControl _MoltenInfo_OneAutoHideControl;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow1;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow2;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MoltenInfo_OneUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MoltenInfo_OneUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MoltenInfo_OneUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea2;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MoltenInfo_OneUnpinnedTabAreaRight;
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn9;
        private Infragistics.Win.UltraWinGrid.UltraGrid uDridSound;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Data.DataSet dsQuery;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataTable dataTable4;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn12;
        private System.Data.DataColumn dataColumn13;
        private System.Data.DataColumn dataColumn14;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
        private System.Data.DataColumn dataColumn19;
        private System.Data.DataColumn dataColumn20;
        private System.Data.DataColumn dataColumn24;
        private System.Data.DataColumn dataColumn25;
        private System.Data.DataColumn dataColumn26;
        private System.Data.DataColumn dataColumn28;
        private System.Data.DataColumn dataColumn29;
        private System.Data.DataColumn dataColumn30;
        private System.Data.DataColumn dataColumn32;
        private System.Data.DataColumn dataColumn33;
        private System.Data.DataColumn dataColumn34;
        private System.Data.DataColumn dataColumn35;
        private System.Data.DataColumn dataColumn36;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnBC;
        private System.Windows.Forms.Button btnDS;
        private System.Data.DataColumn dataColumn37;
        private System.Data.DataColumn dataColumn38;
        private System.Data.DataColumn dataColumn39;
        private System.Windows.Forms.Panel panel10;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJly;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtJld;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCh;
        private System.Windows.Forms.TextBox txtTRAINNO;
        private System.Windows.Forms.Panel panel7;
        private System.Data.DataColumn dataColumn40;
        private System.Data.DataColumn dataColumn41;
        private System.Data.DataColumn dataColumn42;
        private System.Windows.Forms.Button btnSglr;
        private System.Windows.Forms.Button btnWc;
        private System.Windows.Forms.TextBox txtTareWeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNetWeight;
        private System.Windows.Forms.Label label12;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Data.DataTable dataTable5;
        private System.Data.DataColumn dataColumn44;
        private System.Data.DataColumn dataColumn45;
        private System.Data.DataColumn dataColumn46;
        private System.Data.DataColumn dataColumn47;
        private System.Data.DataColumn dataColumn48;
        private System.Windows.Forms.Button btnTrainTare;
        private System.Windows.Forms.CheckBox ck_Tare;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
        private System.Data.DataColumn dataColumn22;
        private System.Data.DataColumn dataColumn23;
        private System.Data.DataColumn dataColumn27;
        private System.Data.DataColumn dataColumn31;
        private System.Data.DataColumn dataColumn43;
        private System.Data.DataColumn dataColumn49;
        private System.Data.DataColumn dataColumn50;
        private System.Data.DataColumn dataColumn51;
        private System.Data.DataColumn dataColumn52;
        private System.Data.DataColumn dataColumn53;
        private System.Data.DataColumn dataColumn54;
        private System.Data.DataColumn dataColumn55;
        private System.Data.DataColumn dataColumn56;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
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
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbFlow;
        public System.Windows.Forms.ComboBox cbSender;
        public System.Windows.Forms.ComboBox cbTrans;
        public System.Windows.Forms.ComboBox cbMaterial;
        public System.Windows.Forms.ComboBox cbReceiver;
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
        private System.Data.DataTable dataTable8;
        private System.Data.DataColumn dataColumn81;
        private System.Data.DataColumn dataColumn92;
        private System.Windows.Forms.Button btReceiver;
        private System.Windows.Forms.Button btSender;
        private System.Windows.Forms.Button btTrans;
        private System.Windows.Forms.Button btMaterial;
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
        private System.Data.DataColumn dataColumn120;
        private System.Data.DataColumn dataColumn121;
        private System.Data.DataColumn dataColumn122;
        private System.Data.DataColumn dataColumn123;
        private System.Data.DataColumn dataColumn124;
        private System.Data.DataColumn dataColumn125;
        private System.Data.DataColumn dataColumn126;
        private System.Data.DataColumn dataColumn127;
        private System.Data.DataColumn dataColumn128;
        private System.Data.DataColumn dataColumn129;
        private System.Data.DataColumn dataColumn130;
        private System.Data.DataColumn dataColumn131;
        private System.Data.DataColumn dataColumn132;
        private System.Data.DataColumn dataColumn133;
        private System.Data.DataColumn dataColumn134;
        private System.Data.DataColumn dataColumn135;
        private System.Data.DataColumn dataColumn136;
        private System.Data.DataColumn dataColumn137;
        private System.Data.DataColumn dataColumn138;
        private System.Data.DataColumn dataColumn139;
        private System.Data.DataColumn dataColumn140;
        private System.Data.DataColumn dataColumn141;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox VideoChannel2;
        private System.Windows.Forms.PictureBox VideoChannel1;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.PictureBox VideoChannel3;
        private System.Windows.Forms.Panel panel4;
        private LxControl.LxLedControl txtMeterWeight;
        private System.Data.DataColumn dataColumn142;
        private System.Data.DataColumn dataColumn143;
        private System.Data.DataColumn dataColumn144;
        private System.Data.DataColumn dataColumn145;
        private System.Data.DataColumn dataColumn146;
        private System.Data.DataColumn dataColumn147;
        private System.Data.DataColumn dataColumn148;
        private System.Data.DataColumn dataColumn149;
        private System.Data.DataColumn dataColumn150;
        private System.Data.DataColumn dataColumn151;
        private System.Data.DataColumn dataColumn152;
        private System.Data.DataColumn dataColumn153;
        private System.Data.DataColumn dataColumn154;
        private System.Data.DataColumn dataColumn155;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox picFDTP;
    }
}