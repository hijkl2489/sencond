namespace YGJZJL.SquareBilletTransfer
{
    partial class BoardBilletWeight
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

            StopPoundRoomThread();

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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("语音表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICENAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICEFILE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_INSTRTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MEMO");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YYDJ");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool3 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloseLED");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("OpenLED");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Refresh");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool1 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("日期");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool2 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("炉号");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardBilletWeight));
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YYDJ");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool4 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloseLED");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("OpenLED");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Refresh");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool11 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btCorrention");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("计量点基础表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("XZ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SIGN");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_METERTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_METERPARA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MOXAIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MOXAPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOUSER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOPWD");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTDEPART");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RTUIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RTUPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PRINTERIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PRINTERNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PRINTTYPECODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_USEDPRINTPAPER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_USEDPRINTINK");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LEDIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LEDPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_VALUE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_ALLOWOTHERTARE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DISPLAYPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DISPLAYPARA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_READERPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_READERPARA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_READERTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DISPLAYTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LEDTYPE");
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("预报表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STOVENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STEELTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SPEC");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_LENGTH");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_COUNT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_ORDERNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_ITEMNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIAL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIALNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_FAHUO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SHOUHUO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_ISRETURNBILLET");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings1 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Count, null, "FS_STOVENO", 0, true, "预报表", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FS_STOVENO", 0, true);
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings2 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Sum, null, "FN_COUNT", 4, true, "预报表", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FN_COUNT", 4, true);
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("辊道计量从表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STOVENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_BILLETCOUNT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_WEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_WEIGHTTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SHIFT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TERM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PERSON");
            Infragistics.Win.UltraWinGrid.SummarySettings summarySettings3 = new Infragistics.Win.UltraWinGrid.SummarySettings("", Infragistics.Win.UltraWinGrid.SummaryType.Count, null, "FS_STOVENO", 0, true, "辊道计量从表", 0, Infragistics.Win.UltraWinGrid.SummaryPosition.UseSummaryPositionColumn, "FS_STOVENO", 0, true);
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("934dd081-e572-46a2-8767-91ba960b93ec"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("8ae72129-44fd-4872-8713-823aa1219e19"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("934dd081-e572-46a2-8767-91ba960b93ec"), -1);
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane2 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("be8b4e0c-9178-4069-8244-a71509396517"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane2 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("4784f5a0-fd7f-4ee8-9732-26923939e403"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("be8b4e0c-9178-4069-8244-a71509396517"), -1);
            this.panelYYBF = new System.Windows.Forms.Panel();
            this.ultraGrid4 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn13 = new System.Data.DataColumn();
            this.dataColumn43 = new System.Data.DataColumn();
            this.dataColumn44 = new System.Data.DataColumn();
            this.dataColumn45 = new System.Data.DataColumn();
            this.dataColumn46 = new System.Data.DataColumn();
            this.dataColumn47 = new System.Data.DataColumn();
            this.dataColumn48 = new System.Data.DataColumn();
            this.dataColumn49 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn12 = new System.Data.DataColumn();
            this.dataColumn14 = new System.Data.DataColumn();
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
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
            this.dataColumn28 = new System.Data.DataColumn();
            this.dataColumn29 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.dataColumn33 = new System.Data.DataColumn();
            this.dataColumn34 = new System.Data.DataColumn();
            this.dataColumn35 = new System.Data.DataColumn();
            this.dataColumn36 = new System.Data.DataColumn();
            this.dataColumn37 = new System.Data.DataColumn();
            this.dataColumn38 = new System.Data.DataColumn();
            this.dataColumn39 = new System.Data.DataColumn();
            this.dataColumn40 = new System.Data.DataColumn();
            this.dataColumn41 = new System.Data.DataColumn();
            this.dataColumn42 = new System.Data.DataColumn();
            this.dataTable4 = new System.Data.DataTable();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn72 = new System.Data.DataColumn();
            this.dataColumn73 = new System.Data.DataColumn();
            this.dataTable5 = new System.Data.DataTable();
            this.dataColumn10 = new System.Data.DataColumn();
            this.dataColumn11 = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
            this.dataColumn50 = new System.Data.DataColumn();
            this.dataColumn51 = new System.Data.DataColumn();
            this.dataColumn52 = new System.Data.DataColumn();
            this.dataColumn53 = new System.Data.DataColumn();
            this.dataColumn54 = new System.Data.DataColumn();
            this.dataColumn55 = new System.Data.DataColumn();
            this.dataColumn56 = new System.Data.DataColumn();
            this.dataColumn57 = new System.Data.DataColumn();
            this.dataColumn58 = new System.Data.DataColumn();
            this.dataColumn59 = new System.Data.DataColumn();
            this.dataColumn60 = new System.Data.DataColumn();
            this.dataColumn61 = new System.Data.DataColumn();
            this.dataColumn62 = new System.Data.DataColumn();
            this.dataColumn68 = new System.Data.DataColumn();
            this.dataTable6 = new System.Data.DataTable();
            this.dataColumn63 = new System.Data.DataColumn();
            this.dataColumn64 = new System.Data.DataColumn();
            this.dataColumn65 = new System.Data.DataColumn();
            this.dataColumn66 = new System.Data.DataColumn();
            this.dataColumn67 = new System.Data.DataColumn();
            this.dataColumn69 = new System.Data.DataColumn();
            this.dataColumn70 = new System.Data.DataColumn();
            this.panelSPKZ = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.handWeight = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btLLBC = new System.Windows.Forms.Button();
            this.panel1_Fill_Panel = new System.Windows.Forms.Panel();
            this.btnBC = new System.Windows.Forms.Button();
            this._panel1_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.picFDTP = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.VideoChannel1 = new System.Windows.Forms.PictureBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.VideoChannel2 = new System.Windows.Forms.PictureBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.VideoChannel3 = new System.Windows.Forms.PictureBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtZL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.tbx_llastTotalWeight = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbx_llastBilletCount = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbx_llastStoveNo = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbx_lastTotalWeight = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbx_lastBilletCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_lastStoveNo = new System.Windows.Forms.TextBox();
            this.btXF = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDS = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.txtBB = new System.Windows.Forms.TextBox();
            this.txtBC = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWLMC = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDDH = new System.Windows.Forms.TextBox();
            this.cbGZ = new System.Windows.Forms.ComboBox();
            this.cbGG = new System.Windows.Forms.ComboBox();
            this.cbSHDW = new System.Windows.Forms.ComboBox();
            this.txtJLY = new System.Windows.Forms.TextBox();
            this.txtCD = new System.Windows.Forms.TextBox();
            this.txtDDXMH = new System.Windows.Forms.TextBox();
            this.cbFHDW = new System.Windows.Forms.ComboBox();
            this.cbLiuX = new System.Windows.Forms.ComboBox();
            this.txtJLD = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.txtLH = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGrid5 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnQL = new System.Windows.Forms.Button();
            this.txtXSZL = new LxControl.LxLedControl();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTextEditor5 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTextEditor3 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraOptionSet1 = new Infragistics.Win.UltraWinEditors.UltraOptionSet();
            this.btChanShift = new System.Windows.Forms.Button();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.lbWD = new System.Windows.Forms.Label();
            this.lbYS = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BilletInfo_Fill_Panel = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.ultraDockManager1 = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._FrmBaseUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._FrmBaseUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._FrmBaseUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._FrmBaseUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._FrmBaseAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.dockableWindow1 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.windowDockingArea2 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.dockableWindow2 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.panelYYBF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.handWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).BeginInit();
            this.panel13.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).BeginInit();
            this.panel14.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel3)).BeginInit();
            this.panel7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).BeginInit();
            this.ultraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).BeginInit();
            this.ultraGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtXSZL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).BeginInit();
            this.BilletInfo_Fill_Panel.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).BeginInit();
            this._FrmBaseAutoHideControl.SuspendLayout();
            this.dockableWindow1.SuspendLayout();
            this.windowDockingArea2.SuspendLayout();
            this.dockableWindow2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelYYBF
            // 
            this.panelYYBF.Controls.Add(this.ultraGrid4);
            this.coreBind.SetDatabasecommand(this.panelYYBF, null);
            this.panelYYBF.Location = new System.Drawing.Point(0, 28);
            this.panelYYBF.Name = "panelYYBF";
            this.panelYYBF.Size = new System.Drawing.Size(108, 556);
            this.panelYYBF.TabIndex = 56;
            this.coreBind.SetVerification(this.panelYYBF, null);
            // 
            // ultraGrid4
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid4, null);
            this.ultraGrid4.DataMember = "语音表";
            this.ultraGrid4.DataSource = this.dataSet1;
            appearance13.BackColor = System.Drawing.Color.White;
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid4.DisplayLayout.Appearance = appearance13;
            ultraGridColumn1.AutoSizeEdit = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.AutoSizeEdit = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4});
            this.ultraGrid4.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid4.DisplayLayout.InterBandSpacing = 10;
            appearance14.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid4.DisplayLayout.Override.CardAreaAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance15.BackColor2 = System.Drawing.Color.White;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance15.FontData.SizeInPoints = 11F;
            appearance15.FontData.UnderlineAsString = "False";
            appearance15.ForeColor = System.Drawing.Color.Black;
            appearance15.TextHAlignAsString = "Center";
            appearance15.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid4.DisplayLayout.Override.HeaderAppearance = appearance15;
            appearance16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid4.DisplayLayout.Override.RowAppearance = appearance16;
            appearance17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance17.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid4.DisplayLayout.Override.RowSelectorAppearance = appearance17;
            this.ultraGrid4.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid4.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance18.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance18.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid4.DisplayLayout.Override.SelectedRowAppearance = appearance18;
            this.ultraGrid4.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid4.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid4.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid4.Name = "ultraGrid4";
            this.ultraGrid4.Size = new System.Drawing.Size(108, 556);
            this.ultraGrid4.TabIndex = 64;
            this.ultraGrid4.TabStop = false;
            this.coreBind.SetVerification(this.ultraGrid4, null);
            this.ultraGrid4.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGrid4_ClickCell_1);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable4,
            this.dataTable5,
            this.dataTable3,
            this.dataTable6});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn16,
            this.dataColumn13,
            this.dataColumn43,
            this.dataColumn44,
            this.dataColumn45,
            this.dataColumn46,
            this.dataColumn47,
            this.dataColumn48,
            this.dataColumn49});
            this.dataTable1.TableName = "辊道计量主表";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "炉号";
            this.dataColumn1.ColumnName = "FS_STOVENO";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "支数";
            this.dataColumn2.ColumnName = "FN_BILLETCOUNT";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "总重量";
            this.dataColumn3.ColumnName = "FN_TOTALWEIGHT";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "计量员";
            this.dataColumn4.ColumnName = "FS_PERSON";
            // 
            // dataColumn16
            // 
            this.dataColumn16.Caption = "班次";
            this.dataColumn16.ColumnName = "FS_SHIFT";
            // 
            // dataColumn13
            // 
            this.dataColumn13.Caption = "是否完炉";
            this.dataColumn13.ColumnName = "FS_COMPLETEFLAG";
            // 
            // dataColumn43
            // 
            this.dataColumn43.Caption = "计量开始时间";
            this.dataColumn43.ColumnName = "FD_STARTTIME";
            // 
            // dataColumn44
            // 
            this.dataColumn44.Caption = "计量完炉时间";
            this.dataColumn44.ColumnName = "FD_ENDTIME";
            // 
            // dataColumn45
            // 
            this.dataColumn45.Caption = "计量点";
            this.dataColumn45.ColumnName = "FS_POINTNAME";
            // 
            // dataColumn46
            // 
            this.dataColumn46.Caption = "流向";
            this.dataColumn46.ColumnName = "FS_FLOW";
            // 
            // dataColumn47
            // 
            this.dataColumn47.Caption = "物料名称";
            this.dataColumn47.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn48
            // 
            this.dataColumn48.Caption = "发货单位";
            this.dataColumn48.ColumnName = "FS_SENDER";
            // 
            // dataColumn49
            // 
            this.dataColumn49.Caption = "收货单位";
            this.dataColumn49.ColumnName = "FS_RECEIVER";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn12,
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn17,
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
            this.dataColumn28,
            this.dataColumn29,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32,
            this.dataColumn33,
            this.dataColumn34,
            this.dataColumn35,
            this.dataColumn36,
            this.dataColumn37,
            this.dataColumn38,
            this.dataColumn39,
            this.dataColumn40,
            this.dataColumn41,
            this.dataColumn42});
            this.dataTable2.TableName = "计量点基础表";
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
            // dataColumn12
            // 
            this.dataColumn12.Caption = "标志";
            this.dataColumn12.ColumnName = "FS_SIGN";
            // 
            // dataColumn14
            // 
            this.dataColumn14.Caption = "仪表类型";
            this.dataColumn14.ColumnName = "FS_METERTYPE";
            // 
            // dataColumn15
            // 
            this.dataColumn15.Caption = "仪表参数";
            this.dataColumn15.ColumnName = "FS_METERPARA";
            // 
            // dataColumn17
            // 
            this.dataColumn17.Caption = "IP";
            this.dataColumn17.ColumnName = "FS_MOXAIP";
            // 
            // dataColumn18
            // 
            this.dataColumn18.Caption = "端口";
            this.dataColumn18.ColumnName = "FS_MOXAPORT";
            // 
            // dataColumn19
            // 
            this.dataColumn19.Caption = "VIEDOIP";
            this.dataColumn19.ColumnName = "FS_VIEDOIP";
            // 
            // dataColumn20
            // 
            this.dataColumn20.Caption = "录像机端口";
            this.dataColumn20.ColumnName = "FS_VIEDOPORT";
            // 
            // dataColumn21
            // 
            this.dataColumn21.Caption = "用户名";
            this.dataColumn21.ColumnName = "FS_VIEDOUSER";
            // 
            // dataColumn22
            // 
            this.dataColumn22.Caption = "密码";
            this.dataColumn22.ColumnName = "FS_VIEDOPWD";
            // 
            // dataColumn23
            // 
            this.dataColumn23.Caption = "POINTDEPART";
            this.dataColumn23.ColumnName = "FS_POINTDEPART";
            // 
            // dataColumn24
            // 
            this.dataColumn24.Caption = "POINTTYPE";
            this.dataColumn24.ColumnName = "FS_POINTTYPE";
            // 
            // dataColumn25
            // 
            this.dataColumn25.Caption = "RTUIP";
            this.dataColumn25.ColumnName = "FS_RTUIP";
            // 
            // dataColumn26
            // 
            this.dataColumn26.Caption = "RTUPORT";
            this.dataColumn26.ColumnName = "FS_RTUPORT";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "FS_PRINTERIP";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "FS_PRINTERNAME";
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "FS_PRINTTYPECODE";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "FN_USEDPRINTPAPER";
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "FN_USEDPRINTINK";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "FS_LEDIP";
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "FS_LEDPORT";
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "FN_VALUE";
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "FS_ALLOWOTHERTARE";
            // 
            // dataColumn36
            // 
            this.dataColumn36.ColumnName = "FS_DISPLAYPORT";
            // 
            // dataColumn37
            // 
            this.dataColumn37.ColumnName = "FS_DISPLAYPARA";
            // 
            // dataColumn38
            // 
            this.dataColumn38.ColumnName = "FS_READERPORT";
            // 
            // dataColumn39
            // 
            this.dataColumn39.ColumnName = "FS_READERPARA";
            // 
            // dataColumn40
            // 
            this.dataColumn40.ColumnName = "FS_READERTYPE";
            // 
            // dataColumn41
            // 
            this.dataColumn41.ColumnName = "FS_DISPLAYTYPE";
            // 
            // dataColumn42
            // 
            this.dataColumn42.ColumnName = "FS_LEDTYPE";
            // 
            // dataTable4
            // 
            this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn5,
            this.dataColumn9,
            this.dataColumn72,
            this.dataColumn73});
            this.dataTable4.TableName = "语音表";
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
            this.dataColumn9.DataType = typeof(byte[]);
            // 
            // dataColumn72
            // 
            this.dataColumn72.ColumnName = "FS_INSTRTYPE";
            // 
            // dataColumn73
            // 
            this.dataColumn73.ColumnName = "FS_MEMO";
            // 
            // dataTable5
            // 
            this.dataTable5.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn10,
            this.dataColumn11});
            this.dataTable5.TableName = "当班统计表";
            // 
            // dataColumn10
            // 
            this.dataColumn10.Caption = "当班累计支数";
            this.dataColumn10.ColumnName = "TOTALCOUNTZS";
            // 
            // dataColumn11
            // 
            this.dataColumn11.Caption = "当班累计重量";
            this.dataColumn11.ColumnName = "TOTALCOUNTZL";
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn50,
            this.dataColumn51,
            this.dataColumn52,
            this.dataColumn53,
            this.dataColumn54,
            this.dataColumn55,
            this.dataColumn56,
            this.dataColumn57,
            this.dataColumn58,
            this.dataColumn59,
            this.dataColumn60,
            this.dataColumn61,
            this.dataColumn62,
            this.dataColumn68});
            this.dataTable3.TableName = "预报表";
            // 
            // dataColumn50
            // 
            this.dataColumn50.Caption = "炉号";
            this.dataColumn50.ColumnName = "FS_STOVENO";
            // 
            // dataColumn51
            // 
            this.dataColumn51.Caption = "钢种";
            this.dataColumn51.ColumnName = "FS_STEELTYPE";
            // 
            // dataColumn52
            // 
            this.dataColumn52.Caption = "规格";
            this.dataColumn52.ColumnName = "FS_SPEC";
            // 
            // dataColumn53
            // 
            this.dataColumn53.Caption = "长度";
            this.dataColumn53.ColumnName = "FN_LENGTH";
            // 
            // dataColumn54
            // 
            this.dataColumn54.Caption = "预报块数";
            this.dataColumn54.ColumnName = "FN_COUNT";
            // 
            // dataColumn55
            // 
            this.dataColumn55.Caption = "订单号";
            this.dataColumn55.ColumnName = "FS_ORDERNO";
            // 
            // dataColumn56
            // 
            this.dataColumn56.Caption = "项目号";
            this.dataColumn56.ColumnName = "FS_ITEMNO";
            // 
            // dataColumn57
            // 
            this.dataColumn57.Caption = "发货单位代码";
            this.dataColumn57.ColumnName = "FS_SENDER";
            // 
            // dataColumn58
            // 
            this.dataColumn58.Caption = "收货单位代码";
            this.dataColumn58.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn59
            // 
            this.dataColumn59.Caption = "物料代码";
            this.dataColumn59.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn60
            // 
            this.dataColumn60.Caption = "物料名称";
            this.dataColumn60.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn61
            // 
            this.dataColumn61.Caption = "发货单位";
            this.dataColumn61.ColumnName = "FS_FAHUO";
            // 
            // dataColumn62
            // 
            this.dataColumn62.Caption = "收货单位";
            this.dataColumn62.ColumnName = "FS_SHOUHUO";
            // 
            // dataColumn68
            // 
            this.dataColumn68.Caption = "返回坯";
            this.dataColumn68.ColumnName = "FN_ISRETURNBILLET";
            // 
            // dataTable6
            // 
            this.dataTable6.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn63,
            this.dataColumn64,
            this.dataColumn65,
            this.dataColumn66,
            this.dataColumn67,
            this.dataColumn69,
            this.dataColumn70});
            this.dataTable6.TableName = "辊道计量从表";
            // 
            // dataColumn63
            // 
            this.dataColumn63.Caption = "炉号";
            this.dataColumn63.ColumnName = "FS_STOVENO";
            // 
            // dataColumn64
            // 
            this.dataColumn64.Caption = "支数";
            this.dataColumn64.ColumnName = "FN_BILLETCOUNT";
            // 
            // dataColumn65
            // 
            this.dataColumn65.Caption = "重量";
            this.dataColumn65.ColumnName = "FN_WEIGHT";
            // 
            // dataColumn66
            // 
            this.dataColumn66.Caption = "计量时间";
            this.dataColumn66.ColumnName = "FD_WEIGHTTIME";
            // 
            // dataColumn67
            // 
            this.dataColumn67.Caption = "班次";
            this.dataColumn67.ColumnName = "FS_SHIFT";
            // 
            // dataColumn69
            // 
            this.dataColumn69.Caption = "班别";
            this.dataColumn69.ColumnName = "FS_TERM";
            // 
            // dataColumn70
            // 
            this.dataColumn70.Caption = "计量员";
            this.dataColumn70.ColumnName = "FS_PERSON";
            // 
            // panelSPKZ
            // 
            this.coreBind.SetDatabasecommand(this.panelSPKZ, null);
            this.panelSPKZ.Location = new System.Drawing.Point(0, 0);
            this.panelSPKZ.Name = "panelSPKZ";
            this.panelSPKZ.Size = new System.Drawing.Size(200, 100);
            this.panelSPKZ.TabIndex = 0;
            this.coreBind.SetVerification(this.panelSPKZ, null);
            // 
            // button5
            // 
            this.coreBind.SetDatabasecommand(this.button5, null);
            this.button5.Location = new System.Drawing.Point(0, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            this.coreBind.SetVerification(this.button5, null);
            // 
            // button6
            // 
            this.coreBind.SetDatabasecommand(this.button6, null);
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 0;
            this.coreBind.SetVerification(this.button6, null);
            // 
            // button7
            // 
            this.coreBind.SetDatabasecommand(this.button7, null);
            this.button7.Location = new System.Drawing.Point(0, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.coreBind.SetVerification(this.button7, null);
            // 
            // button8
            // 
            this.coreBind.SetDatabasecommand(this.button8, null);
            this.button8.Location = new System.Drawing.Point(0, 0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 0;
            this.coreBind.SetVerification(this.button8, null);
            // 
            // button4
            // 
            this.coreBind.SetDatabasecommand(this.button4, null);
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.coreBind.SetVerification(this.button4, null);
            // 
            // button3
            // 
            this.coreBind.SetDatabasecommand(this.button3, null);
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.coreBind.SetVerification(this.button3, null);
            // 
            // button10
            // 
            this.coreBind.SetDatabasecommand(this.button10, null);
            this.button10.Location = new System.Drawing.Point(0, 0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 0;
            this.coreBind.SetVerification(this.button10, null);
            // 
            // button11
            // 
            this.coreBind.SetDatabasecommand(this.button11, null);
            this.button11.Location = new System.Drawing.Point(0, 0);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 0;
            this.coreBind.SetVerification(this.button11, null);
            // 
            // button15
            // 
            this.coreBind.SetDatabasecommand(this.button15, null);
            this.button15.Location = new System.Drawing.Point(0, 0);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(75, 23);
            this.button15.TabIndex = 0;
            this.coreBind.SetVerification(this.button15, null);
            // 
            // button14
            // 
            this.coreBind.SetDatabasecommand(this.button14, null);
            this.button14.Location = new System.Drawing.Point(0, 0);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(75, 23);
            this.button14.TabIndex = 0;
            this.coreBind.SetVerification(this.button14, null);
            // 
            // button13
            // 
            this.coreBind.SetDatabasecommand(this.button13, null);
            this.button13.Location = new System.Drawing.Point(0, 0);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 0;
            this.coreBind.SetVerification(this.button13, null);
            // 
            // button12
            // 
            this.coreBind.SetDatabasecommand(this.button12, null);
            this.button12.Location = new System.Drawing.Point(0, 0);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 0;
            this.coreBind.SetVerification(this.button12, null);
            // 
            // button16
            // 
            this.coreBind.SetDatabasecommand(this.button16, null);
            this.button16.Location = new System.Drawing.Point(0, 0);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(75, 23);
            this.button16.TabIndex = 0;
            this.coreBind.SetVerification(this.button16, null);
            // 
            // button17
            // 
            this.coreBind.SetDatabasecommand(this.button17, null);
            this.button17.Location = new System.Drawing.Point(0, 0);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(75, 23);
            this.button17.TabIndex = 0;
            this.coreBind.SetVerification(this.button17, null);
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this.panel1;
            this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            buttonTool2.InstanceProps.IsFirstInGroup = true;
            controlContainerTool3.InstanceProps.IsFirstInGroup = true;
            controlContainerTool3.InstanceProps.Width = 78;
            buttonTool4.InstanceProps.IsFirstInGroup = true;
            buttonTool6.InstanceProps.IsFirstInGroup = true;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool2,
            controlContainerTool3,
            buttonTool4,
            buttonTool6,
            buttonTool8});
            ultraToolbar1.Text = "UltraToolbar1";
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            controlContainerTool1.SharedPropsInternal.Caption = "日期";
            controlContainerTool1.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool2.SharedPropsInternal.Caption = "炉号";
            controlContainerTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            appearance35.Image = ((object)(resources.GetObject("appearance35.Image")));
            buttonTool1.SharedPropsInternal.AppearancesSmall.Appearance = appearance35;
            buttonTool1.SharedPropsInternal.Caption = "查询";
            buttonTool1.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool3.SharedPropsInternal.Caption = " 语音对讲 ";
            buttonTool3.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            controlContainerTool4.SharedPropsInternal.Caption = " 1 ";
            controlContainerTool4.SharedPropsInternal.Enabled = false;
            controlContainerTool4.SharedPropsInternal.Visible = false;
            controlContainerTool4.SharedPropsInternal.Width = 78;
            buttonTool5.SharedPropsInternal.Caption = " 关闭LED显示";
            buttonTool5.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool7.SharedPropsInternal.Caption = "打开LED显示 ";
            buttonTool7.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool9.SharedPropsInternal.Caption = "预报刷新";
            buttonTool9.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool11.SharedPropsInternal.Caption = "校秤";
            buttonTool11.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            controlContainerTool1,
            controlContainerTool2,
            buttonTool1,
            buttonTool3,
            controlContainerTool4,
            buttonTool5,
            buttonTool7,
            buttonTool9,
            buttonTool11});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraLabel5);
            this.panel1.Controls.Add(this.handWeight);
            this.panel1.Controls.Add(this.btLLBC);
            this.panel1.Controls.Add(this.panel1_Fill_Panel);
            this.panel1.Controls.Add(this.btnBC);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Left);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Right);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Top);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Bottom);
            this.coreBind.SetDatabasecommand(this.panel1, null);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 27);
            this.panel1.TabIndex = 43;
            this.coreBind.SetVerification(this.panel1, null);
            // 
            // ultraLabel5
            // 
            appearance4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance4.TextHAlignAsString = "Center";
            appearance4.TextVAlignAsString = "Middle";
            this.ultraLabel5.Appearance = appearance4;
            this.coreBind.SetDatabasecommand(this.ultraLabel5, null);
            this.ultraLabel5.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraLabel5.Location = new System.Drawing.Point(840, 5);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(21, 21);
            this.ultraLabel5.TabIndex = 616;
            this.ultraLabel5.Text = "吨";
            this.coreBind.SetVerification(this.ultraLabel5, null);
            // 
            // handWeight
            // 
            appearance25.BorderColor = System.Drawing.Color.Black;
            this.handWeight.Appearance = appearance25;
            this.handWeight.AutoSize = false;
            this.coreBind.SetDatabasecommand(this.handWeight, null);
            this.handWeight.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.OfficeXP;
            this.handWeight.Font = new System.Drawing.Font("宋体", 14F);
            this.handWeight.Location = new System.Drawing.Point(746, 0);
            this.handWeight.MaxLength = 7;
            this.handWeight.Name = "handWeight";
            this.handWeight.Size = new System.Drawing.Size(93, 28);
            this.handWeight.TabIndex = 750;
            this.coreBind.SetVerification(this.handWeight, null);
            // 
            // btLLBC
            // 
            this.btLLBC.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.coreBind.SetDatabasecommand(this.btLLBC, null);
            this.btLLBC.Location = new System.Drawing.Point(630, -3);
            this.btLLBC.Name = "btLLBC";
            this.btLLBC.Size = new System.Drawing.Size(92, 28);
            this.btLLBC.TabIndex = 747;
            this.btLLBC.TabStop = false;
            this.btLLBC.Text = "理重保存";
            this.btLLBC.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btLLBC, null);
            this.btLLBC.Visible = false;
            this.btLLBC.Click += new System.EventHandler(this.btLLBC_Click_1);
            // 
            // panel1_Fill_Panel
            // 
            this.panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.panel1_Fill_Panel, null);
            this.panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1_Fill_Panel.Location = new System.Drawing.Point(0, 26);
            this.panel1_Fill_Panel.Name = "panel1_Fill_Panel";
            this.panel1_Fill_Panel.Size = new System.Drawing.Size(1007, 1);
            this.panel1_Fill_Panel.TabIndex = 44;
            this.coreBind.SetVerification(this.panel1_Fill_Panel, null);
            // 
            // btnBC
            // 
            this.btnBC.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.btnBC, null);
            this.btnBC.Location = new System.Drawing.Point(886, 3);
            this.btnBC.Name = "btnBC";
            this.btnBC.Size = new System.Drawing.Size(66, 22);
            this.btnBC.TabIndex = 746;
            this.btnBC.TabStop = false;
            this.btnBC.Text = "手动保存";
            this.btnBC.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnBC, null);
            this.btnBC.Click += new System.EventHandler(this.btnBC_Click_1);
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
            this._panel1_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 1);
            this._panel1_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Left, null);
            // 
            // _panel1_Toolbars_Dock_Area_Right
            // 
            this._panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Right, null);
            this._panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1007, 26);
            this._panel1_Toolbars_Dock_Area_Right.Name = "_panel1_Toolbars_Dock_Area_Right";
            this._panel1_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 1);
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
            this._panel1_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1007, 26);
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
            this._panel1_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1007, 0);
            this._panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Bottom, null);
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.chkAutoSave, null);
            this.chkAutoSave.Enabled = false;
            this.chkAutoSave.Location = new System.Drawing.Point(553, 64);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(78, 16);
            this.chkAutoSave.TabIndex = 49;
            this.chkAutoSave.Text = "自动保存 ";
            this.chkAutoSave.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.chkAutoSave, null);
            this.chkAutoSave.CheckedChanged += new System.EventHandler(this.chkAutoSave_CheckedChanged);
            // 
            // picFDTP
            // 
            this.coreBind.SetDatabasecommand(this.picFDTP, null);
            this.picFDTP.Location = new System.Drawing.Point(167, 138);
            this.picFDTP.Name = "picFDTP";
            this.picFDTP.Size = new System.Drawing.Size(10, 10);
            this.picFDTP.TabIndex = 608;
            this.picFDTP.TabStop = false;
            this.coreBind.SetVerification(this.picFDTP, null);
            this.picFDTP.Visible = false;
            this.picFDTP.DoubleClick += new System.EventHandler(this.picFDTP_DoubleClick);
            this.picFDTP.MouseLeave += new System.EventHandler(this.picFDTP_MouseLeave);
            this.picFDTP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseMove);
            this.picFDTP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseDown);
            this.picFDTP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseUp);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.coreBind.SetDatabasecommand(this.panel2, null);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 557);
            this.panel2.TabIndex = 47;
            this.coreBind.SetVerification(this.panel2, null);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel12, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel13, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel14, 0, 2);
            this.coreBind.SetDatabasecommand(this.tableLayoutPanel1, null);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(343, 555);
            this.tableLayoutPanel1.TabIndex = 51;
            this.coreBind.SetVerification(this.tableLayoutPanel1, null);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.panel3);
            this.coreBind.SetDatabasecommand(this.panel12, null);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(3, 3);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(337, 178);
            this.panel12.TabIndex = 0;
            this.coreBind.SetVerification(this.panel12, null);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.VideoChannel1);
            this.coreBind.SetDatabasecommand(this.panel3, null);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(337, 178);
            this.panel3.TabIndex = 48;
            this.coreBind.SetVerification(this.panel3, null);
            // 
            // VideoChannel1
            // 
            this.VideoChannel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel1, null);
            this.VideoChannel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel1.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel1.Name = "VideoChannel1";
            this.VideoChannel1.Size = new System.Drawing.Size(337, 178);
            this.VideoChannel1.TabIndex = 1;
            this.VideoChannel1.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel1, null);
            this.VideoChannel1.DoubleClick += new System.EventHandler(this.VideoChannel1_DoubleClick);
            this.VideoChannel1.Click += new System.EventHandler(this.VideoChannel1_Click);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.panel4);
            this.coreBind.SetDatabasecommand(this.panel13, null);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 187);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(337, 179);
            this.panel13.TabIndex = 1;
            this.coreBind.SetVerification(this.panel13, null);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.VideoChannel2);
            this.coreBind.SetDatabasecommand(this.panel4, null);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(337, 179);
            this.panel4.TabIndex = 49;
            this.coreBind.SetVerification(this.panel4, null);
            // 
            // VideoChannel2
            // 
            this.VideoChannel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel2, null);
            this.VideoChannel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel2.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel2.Name = "VideoChannel2";
            this.VideoChannel2.Size = new System.Drawing.Size(337, 179);
            this.VideoChannel2.TabIndex = 0;
            this.VideoChannel2.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel2, null);
            this.VideoChannel2.DoubleClick += new System.EventHandler(this.VideoChannel2_DoubleClick);
            this.VideoChannel2.Click += new System.EventHandler(this.VideoChannel2_Click);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel5);
            this.coreBind.SetDatabasecommand(this.panel14, null);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(3, 372);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(337, 180);
            this.panel14.TabIndex = 2;
            this.coreBind.SetVerification(this.panel14, null);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.VideoChannel3);
            this.panel5.Controls.Add(this.panel7);
            this.coreBind.SetDatabasecommand(this.panel5, null);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(337, 180);
            this.panel5.TabIndex = 50;
            this.coreBind.SetVerification(this.panel5, null);
            // 
            // VideoChannel3
            // 
            this.VideoChannel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel3, null);
            this.VideoChannel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel3.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel3.Name = "VideoChannel3";
            this.VideoChannel3.Size = new System.Drawing.Size(337, 180);
            this.VideoChannel3.TabIndex = 0;
            this.VideoChannel3.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel3, null);
            this.VideoChannel3.DoubleClick += new System.EventHandler(this.VideoChannel3_DoubleClick);
            this.VideoChannel3.Click += new System.EventHandler(this.VideoChannel3_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.groupBox1);
            this.panel7.Controls.Add(this.ultraGroupBox2);
            this.coreBind.SetDatabasecommand(this.panel7, null);
            this.panel7.Location = new System.Drawing.Point(331, 162);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(741, 53);
            this.panel7.TabIndex = 52;
            this.coreBind.SetVerification(this.panel7, null);
            this.panel7.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox1.Controls.Add(this.txtZL);
            this.groupBox1.Controls.Add(this.label6);
            this.coreBind.SetDatabasecommand(this.groupBox1, null);
            this.groupBox1.Location = new System.Drawing.Point(248, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 37);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据编辑区";
            this.coreBind.SetVerification(this.groupBox1, null);
            // 
            // txtZL
            // 
            this.txtZL.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtZL, null);
            this.txtZL.Font = new System.Drawing.Font("宋体", 11F);
            this.txtZL.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtZL.Location = new System.Drawing.Point(104, 18);
            this.txtZL.MaxLength = 8;
            this.txtZL.Name = "txtZL";
            this.txtZL.ReadOnly = true;
            this.txtZL.Size = new System.Drawing.Size(78, 24);
            this.txtZL.TabIndex = 744;
            this.txtZL.TabStop = false;
            this.coreBind.SetVerification(this.txtZL, null);
            // 
            // label6
            // 
            this.coreBind.SetDatabasecommand(this.label6, null);
            this.label6.Location = new System.Drawing.Point(33, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 24);
            this.label6.TabIndex = 743;
            this.label6.Text = "重量(t)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label6, null);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularInset;
            this.ultraGroupBox2.Controls.Add(this.panel11);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox2, null);
            this.ultraGroupBox2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 137);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(741, 20);
            this.ultraGroupBox2.TabIndex = 61;
            this.ultraGroupBox2.Text = "预报和称重信息";
            this.coreBind.SetVerification(this.ultraGroupBox2, null);
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            this.ultraGroupBox2.Visible = false;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.label18);
            this.panel11.Controls.Add(this.tbx_llastTotalWeight);
            this.panel11.Controls.Add(this.label19);
            this.panel11.Controls.Add(this.tbx_llastBilletCount);
            this.panel11.Controls.Add(this.label20);
            this.panel11.Controls.Add(this.tbx_llastStoveNo);
            this.panel11.Controls.Add(this.label17);
            this.panel11.Controls.Add(this.tbx_lastTotalWeight);
            this.panel11.Controls.Add(this.label16);
            this.panel11.Controls.Add(this.tbx_lastBilletCount);
            this.panel11.Controls.Add(this.label3);
            this.panel11.Controls.Add(this.tbx_lastStoveNo);
            this.panel11.Controls.Add(this.btXF);
            this.panel11.Controls.Add(this.button2);
            this.panel11.Controls.Add(this.btnDS);
            this.panel11.Controls.Add(this.button1);
            this.panel11.Controls.Add(this.button9);
            this.panel11.Controls.Add(this.txtBB);
            this.panel11.Controls.Add(this.txtBC);
            this.panel11.Controls.Add(this.label1);
            this.panel11.Controls.Add(this.cbWLMC);
            this.panel11.Controls.Add(this.label12);
            this.panel11.Controls.Add(this.label4);
            this.panel11.Controls.Add(this.txtDDH);
            this.panel11.Controls.Add(this.cbGZ);
            this.panel11.Controls.Add(this.cbGG);
            this.panel11.Controls.Add(this.cbSHDW);
            this.panel11.Controls.Add(this.txtJLY);
            this.panel11.Controls.Add(this.txtCD);
            this.panel11.Controls.Add(this.txtDDXMH);
            this.panel11.Controls.Add(this.cbFHDW);
            this.panel11.Controls.Add(this.cbLiuX);
            this.panel11.Controls.Add(this.txtJLD);
            this.panel11.Controls.Add(this.label5);
            this.panel11.Controls.Add(this.label7);
            this.panel11.Controls.Add(this.label8);
            this.panel11.Controls.Add(this.label14);
            this.panel11.Controls.Add(this.label15);
            this.panel11.Controls.Add(this.label11);
            this.panel11.Controls.Add(this.label13);
            this.panel11.Controls.Add(this.label10);
            this.panel11.Controls.Add(this.label9);
            this.panel11.Controls.Add(this.label23);
            this.panel11.Controls.Add(this.txtLH);
            this.panel11.Controls.Add(this.label24);
            this.panel11.Controls.Add(this.ultraGroupBox3);
            this.coreBind.SetDatabasecommand(this.panel11, null);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 24);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(734, 0);
            this.panel11.TabIndex = 46;
            this.coreBind.SetVerification(this.panel11, null);
            // 
            // label18
            // 
            this.coreBind.SetDatabasecommand(this.label18, null);
            this.label18.Location = new System.Drawing.Point(303, 193);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 24);
            this.label18.TabIndex = 736;
            this.label18.Text = "重量";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label18, null);
            // 
            // tbx_llastTotalWeight
            // 
            this.tbx_llastTotalWeight.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.tbx_llastTotalWeight, null);
            this.tbx_llastTotalWeight.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_llastTotalWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_llastTotalWeight.Location = new System.Drawing.Point(377, 193);
            this.tbx_llastTotalWeight.MaxLength = 9;
            this.tbx_llastTotalWeight.Name = "tbx_llastTotalWeight";
            this.tbx_llastTotalWeight.ReadOnly = true;
            this.tbx_llastTotalWeight.Size = new System.Drawing.Size(68, 24);
            this.tbx_llastTotalWeight.TabIndex = 735;
            this.coreBind.SetVerification(this.tbx_llastTotalWeight, null);
            // 
            // label19
            // 
            this.coreBind.SetDatabasecommand(this.label19, null);
            this.label19.Location = new System.Drawing.Point(169, 193);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(76, 24);
            this.label19.TabIndex = 734;
            this.label19.Text = "条数";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label19, null);
            // 
            // tbx_llastBilletCount
            // 
            this.tbx_llastBilletCount.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.tbx_llastBilletCount, null);
            this.tbx_llastBilletCount.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_llastBilletCount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_llastBilletCount.Location = new System.Drawing.Point(247, 193);
            this.tbx_llastBilletCount.MaxLength = 9;
            this.tbx_llastBilletCount.Name = "tbx_llastBilletCount";
            this.tbx_llastBilletCount.ReadOnly = true;
            this.tbx_llastBilletCount.Size = new System.Drawing.Size(50, 24);
            this.tbx_llastBilletCount.TabIndex = 733;
            this.coreBind.SetVerification(this.tbx_llastBilletCount, null);
            // 
            // label20
            // 
            this.coreBind.SetDatabasecommand(this.label20, null);
            this.label20.Location = new System.Drawing.Point(1, 193);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 24);
            this.label20.TabIndex = 732;
            this.label20.Text = "NO1：炉号";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label20, null);
            // 
            // tbx_llastStoveNo
            // 
            this.tbx_llastStoveNo.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.tbx_llastStoveNo, null);
            this.tbx_llastStoveNo.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_llastStoveNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_llastStoveNo.Location = new System.Drawing.Point(69, 193);
            this.tbx_llastStoveNo.MaxLength = 9;
            this.tbx_llastStoveNo.Name = "tbx_llastStoveNo";
            this.tbx_llastStoveNo.ReadOnly = true;
            this.tbx_llastStoveNo.Size = new System.Drawing.Size(94, 24);
            this.tbx_llastStoveNo.TabIndex = 731;
            this.coreBind.SetVerification(this.tbx_llastStoveNo, null);
            // 
            // label17
            // 
            this.coreBind.SetDatabasecommand(this.label17, null);
            this.label17.Location = new System.Drawing.Point(311, 222);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 24);
            this.label17.TabIndex = 730;
            this.label17.Text = "重量";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label17, null);
            // 
            // tbx_lastTotalWeight
            // 
            this.tbx_lastTotalWeight.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.tbx_lastTotalWeight, null);
            this.tbx_lastTotalWeight.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_lastTotalWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_lastTotalWeight.Location = new System.Drawing.Point(377, 222);
            this.tbx_lastTotalWeight.MaxLength = 9;
            this.tbx_lastTotalWeight.Name = "tbx_lastTotalWeight";
            this.tbx_lastTotalWeight.ReadOnly = true;
            this.tbx_lastTotalWeight.Size = new System.Drawing.Size(68, 24);
            this.tbx_lastTotalWeight.TabIndex = 729;
            this.coreBind.SetVerification(this.tbx_lastTotalWeight, null);
            // 
            // label16
            // 
            this.coreBind.SetDatabasecommand(this.label16, null);
            this.label16.Location = new System.Drawing.Point(179, 222);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 24);
            this.label16.TabIndex = 728;
            this.label16.Text = "条数";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label16, null);
            // 
            // tbx_lastBilletCount
            // 
            this.tbx_lastBilletCount.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.tbx_lastBilletCount, null);
            this.tbx_lastBilletCount.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_lastBilletCount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_lastBilletCount.Location = new System.Drawing.Point(247, 222);
            this.tbx_lastBilletCount.MaxLength = 9;
            this.tbx_lastBilletCount.Name = "tbx_lastBilletCount";
            this.tbx_lastBilletCount.ReadOnly = true;
            this.tbx_lastBilletCount.Size = new System.Drawing.Size(50, 24);
            this.tbx_lastBilletCount.TabIndex = 727;
            this.coreBind.SetVerification(this.tbx_lastBilletCount, null);
            // 
            // label3
            // 
            this.coreBind.SetDatabasecommand(this.label3, null);
            this.label3.Location = new System.Drawing.Point(3, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 24);
            this.label3.TabIndex = 726;
            this.label3.Text = "NO2：炉号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label3, null);
            // 
            // tbx_lastStoveNo
            // 
            this.tbx_lastStoveNo.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.tbx_lastStoveNo, null);
            this.tbx_lastStoveNo.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_lastStoveNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_lastStoveNo.Location = new System.Drawing.Point(69, 222);
            this.tbx_lastStoveNo.MaxLength = 9;
            this.tbx_lastStoveNo.Name = "tbx_lastStoveNo";
            this.tbx_lastStoveNo.ReadOnly = true;
            this.tbx_lastStoveNo.Size = new System.Drawing.Size(94, 24);
            this.tbx_lastStoveNo.TabIndex = 725;
            this.coreBind.SetVerification(this.tbx_lastStoveNo, null);
            // 
            // btXF
            // 
            this.btXF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btXF, null);
            this.btXF.Location = new System.Drawing.Point(435, 257);
            this.btXF.Name = "btXF";
            this.btXF.Size = new System.Drawing.Size(30, 19);
            this.btXF.TabIndex = 723;
            this.btXF.TabStop = false;
            this.btXF.Text = "修 改";
            this.btXF.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btXF, null);
            this.btXF.Visible = false;
            this.btXF.Click += new System.EventHandler(this.btXF_Click);
            // 
            // button2
            // 
            this.coreBind.SetDatabasecommand(this.button2, null);
            this.button2.Location = new System.Drawing.Point(507, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 20);
            this.button2.TabIndex = 722;
            this.button2.Tag = "Receiver";
            this.button2.Text = "...";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button2, null);
            this.button2.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // btnDS
            // 
            this.btnDS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnDS, null);
            this.btnDS.Location = new System.Drawing.Point(380, 257);
            this.btnDS.Name = "btnDS";
            this.btnDS.Size = new System.Drawing.Size(49, 21);
            this.btnDS.TabIndex = 15;
            this.btnDS.TabStop = false;
            this.btnDS.Text = "读数";
            this.btnDS.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnDS, null);
            this.btnDS.Visible = false;
            this.btnDS.Click += new System.EventHandler(this.btnDS_Click);
            // 
            // button1
            // 
            this.coreBind.SetDatabasecommand(this.button1, null);
            this.button1.Location = new System.Drawing.Point(221, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 20);
            this.button1.TabIndex = 721;
            this.button1.Tag = "Sender";
            this.button1.Text = "...";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button1, null);
            this.button1.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // button9
            // 
            this.coreBind.SetDatabasecommand(this.button9, null);
            this.button9.Location = new System.Drawing.Point(221, 32);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(23, 20);
            this.button9.TabIndex = 720;
            this.button9.Tag = "Material";
            this.button9.Text = "...";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button9.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button9, null);
            this.button9.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // txtBB
            // 
            this.txtBB.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtBB, null);
            this.txtBB.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBB.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBB.Location = new System.Drawing.Point(171, 160);
            this.txtBB.MaxLength = 8;
            this.txtBB.Name = "txtBB";
            this.txtBB.ReadOnly = true;
            this.txtBB.Size = new System.Drawing.Size(50, 24);
            this.txtBB.TabIndex = 13;
            this.txtBB.TabStop = false;
            this.coreBind.SetVerification(this.txtBB, null);
            // 
            // txtBC
            // 
            this.txtBC.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtBC, null);
            this.txtBC.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBC.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBC.Location = new System.Drawing.Point(80, 160);
            this.txtBC.MaxLength = 8;
            this.txtBC.Name = "txtBC";
            this.txtBC.ReadOnly = true;
            this.txtBC.Size = new System.Drawing.Size(50, 24);
            this.txtBC.TabIndex = 12;
            this.txtBC.TabStop = false;
            this.coreBind.SetVerification(this.txtBC, null);
            // 
            // label1
            // 
            this.coreBind.SetDatabasecommand(this.label1, null);
            this.label1.Location = new System.Drawing.Point(130, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 24);
            this.label1.TabIndex = 24;
            this.label1.Text = "班别";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label1, null);
            // 
            // cbWLMC
            // 
            this.coreBind.SetDatabasecommand(this.cbWLMC, null);
            this.cbWLMC.FormattingEnabled = true;
            this.cbWLMC.Location = new System.Drawing.Point(80, 28);
            this.cbWLMC.Name = "cbWLMC";
            this.cbWLMC.Size = new System.Drawing.Size(141, 25);
            this.cbWLMC.TabIndex = 2;
            this.coreBind.SetVerification(this.cbWLMC, null);
            this.cbWLMC.SelectedIndexChanged += new System.EventHandler(this.cbWLMC_SelectedIndexChanged);
            // 
            // label12
            // 
            this.coreBind.SetDatabasecommand(this.label12, null);
            this.label12.Location = new System.Drawing.Point(9, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 24);
            this.label12.TabIndex = 27;
            this.label12.Text = "物料名称";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label12, null);
            // 
            // label4
            // 
            this.coreBind.SetDatabasecommand(this.label4, null);
            this.label4.Location = new System.Drawing.Point(275, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 24);
            this.label4.TabIndex = 36;
            this.label4.Text = "订单号";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label4, null);
            // 
            // txtDDH
            // 
            this.txtDDH.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtDDH, null);
            this.txtDDH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtDDH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDDH.Location = new System.Drawing.Point(360, 3);
            this.txtDDH.MaxLength = 12;
            this.txtDDH.Name = "txtDDH";
            this.txtDDH.Size = new System.Drawing.Size(141, 24);
            this.txtDDH.TabIndex = 1;
            this.coreBind.SetVerification(this.txtDDH, null);
            this.txtDDH.Leave += new System.EventHandler(this.txtDDH_Leave);
            // 
            // cbGZ
            // 
            this.coreBind.SetDatabasecommand(this.cbGZ, null);
            this.cbGZ.FormattingEnabled = true;
            this.cbGZ.Location = new System.Drawing.Point(80, 55);
            this.cbGZ.Name = "cbGZ";
            this.cbGZ.Size = new System.Drawing.Size(141, 25);
            this.cbGZ.TabIndex = 4;
            this.coreBind.SetVerification(this.cbGZ, null);
            // 
            // cbGG
            // 
            this.coreBind.SetDatabasecommand(this.cbGG, null);
            this.cbGG.FormattingEnabled = true;
            this.cbGG.Location = new System.Drawing.Point(360, 53);
            this.cbGG.Name = "cbGG";
            this.cbGG.Size = new System.Drawing.Size(141, 25);
            this.cbGG.TabIndex = 5;
            this.coreBind.SetVerification(this.cbGG, null);
            // 
            // cbSHDW
            // 
            this.coreBind.SetDatabasecommand(this.cbSHDW, null);
            this.cbSHDW.FormattingEnabled = true;
            this.cbSHDW.Location = new System.Drawing.Point(360, 105);
            this.cbSHDW.Name = "cbSHDW";
            this.cbSHDW.Size = new System.Drawing.Size(141, 25);
            this.cbSHDW.TabIndex = 9;
            this.cbSHDW.Tag = "Receiver";
            this.coreBind.SetVerification(this.cbSHDW, null);
            this.cbSHDW.SelectedIndexChanged += new System.EventHandler(this.cbSHDW_SelectedIndexChanged);
            // 
            // txtJLY
            // 
            this.txtJLY.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtJLY, null);
            this.txtJLY.Font = new System.Drawing.Font("宋体", 11F);
            this.txtJLY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJLY.Location = new System.Drawing.Point(360, 132);
            this.txtJLY.MaxLength = 8;
            this.txtJLY.Name = "txtJLY";
            this.txtJLY.ReadOnly = true;
            this.txtJLY.Size = new System.Drawing.Size(141, 24);
            this.txtJLY.TabIndex = 11;
            this.txtJLY.TabStop = false;
            this.coreBind.SetVerification(this.txtJLY, null);
            // 
            // txtCD
            // 
            this.txtCD.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtCD, null);
            this.txtCD.Font = new System.Drawing.Font("宋体", 11F);
            this.txtCD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCD.Location = new System.Drawing.Point(360, 79);
            this.txtCD.MaxLength = 8;
            this.txtCD.Name = "txtCD";
            this.txtCD.Size = new System.Drawing.Size(141, 24);
            this.txtCD.TabIndex = 7;
            this.coreBind.SetVerification(this.txtCD, null);
            // 
            // txtDDXMH
            // 
            this.txtDDXMH.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtDDXMH, null);
            this.txtDDXMH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtDDXMH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDDXMH.Location = new System.Drawing.Point(360, 28);
            this.txtDDXMH.MaxLength = 4;
            this.txtDDXMH.Name = "txtDDXMH";
            this.txtDDXMH.Size = new System.Drawing.Size(141, 24);
            this.txtDDXMH.TabIndex = 3;
            this.coreBind.SetVerification(this.txtDDXMH, null);
            // 
            // cbFHDW
            // 
            this.coreBind.SetDatabasecommand(this.cbFHDW, null);
            this.cbFHDW.FormattingEnabled = true;
            this.cbFHDW.Location = new System.Drawing.Point(80, 109);
            this.cbFHDW.Name = "cbFHDW";
            this.cbFHDW.Size = new System.Drawing.Size(141, 25);
            this.cbFHDW.TabIndex = 8;
            this.coreBind.SetVerification(this.cbFHDW, null);
            this.cbFHDW.SelectedIndexChanged += new System.EventHandler(this.cbFHDW_SelectedIndexChanged);
            // 
            // cbLiuX
            // 
            this.cbLiuX.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLiuX.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.cbLiuX, null);
            this.cbLiuX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLiuX.FormattingEnabled = true;
            this.cbLiuX.Location = new System.Drawing.Point(80, 82);
            this.cbLiuX.Name = "cbLiuX";
            this.cbLiuX.Size = new System.Drawing.Size(141, 25);
            this.cbLiuX.TabIndex = 6;
            this.coreBind.SetVerification(this.cbLiuX, null);
            // 
            // txtJLD
            // 
            this.txtJLD.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtJLD, null);
            this.txtJLD.Font = new System.Drawing.Font("宋体", 11F);
            this.txtJLD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJLD.Location = new System.Drawing.Point(80, 135);
            this.txtJLD.MaxLength = 8;
            this.txtJLD.Name = "txtJLD";
            this.txtJLD.ReadOnly = true;
            this.txtJLD.Size = new System.Drawing.Size(141, 24);
            this.txtJLD.TabIndex = 10;
            this.txtJLD.TabStop = false;
            this.coreBind.SetVerification(this.txtJLD, null);
            // 
            // label5
            // 
            this.coreBind.SetDatabasecommand(this.label5, null);
            this.label5.Location = new System.Drawing.Point(14, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 24);
            this.label5.TabIndex = 37;
            this.label5.Text = "班次";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label5, null);
            // 
            // label7
            // 
            this.coreBind.SetDatabasecommand(this.label7, null);
            this.label7.Location = new System.Drawing.Point(279, 131);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 24);
            this.label7.TabIndex = 39;
            this.label7.Text = "计量员";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label7, null);
            // 
            // label8
            // 
            this.coreBind.SetDatabasecommand(this.label8, null);
            this.label8.Location = new System.Drawing.Point(11, 136);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 24);
            this.label8.TabIndex = 40;
            this.label8.Text = "计量点";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label8, null);
            // 
            // label14
            // 
            this.coreBind.SetDatabasecommand(this.label14, null);
            this.label14.Location = new System.Drawing.Point(268, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(91, 24);
            this.label14.TabIndex = 29;
            this.label14.Text = "收货单位";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label14, null);
            // 
            // label15
            // 
            this.coreBind.SetDatabasecommand(this.label15, null);
            this.label15.Location = new System.Drawing.Point(0, 112);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 24);
            this.label15.TabIndex = 30;
            this.label15.Text = "发货单位";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label15, null);
            // 
            // label11
            // 
            this.coreBind.SetDatabasecommand(this.label11, null);
            this.label11.Location = new System.Drawing.Point(11, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 24);
            this.label11.TabIndex = 26;
            this.label11.Text = "流向";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label11, null);
            // 
            // label13
            // 
            this.coreBind.SetDatabasecommand(this.label13, null);
            this.label13.Location = new System.Drawing.Point(277, 55);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 24);
            this.label13.TabIndex = 28;
            this.label13.Text = "规格";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label13, null);
            // 
            // label10
            // 
            this.coreBind.SetDatabasecommand(this.label10, null);
            this.label10.Location = new System.Drawing.Point(9, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 24);
            this.label10.TabIndex = 25;
            this.label10.Text = "钢种";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label10, null);
            // 
            // label9
            // 
            this.coreBind.SetDatabasecommand(this.label9, null);
            this.label9.Location = new System.Drawing.Point(275, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 24);
            this.label9.TabIndex = 41;
            this.label9.Text = "订单项目号";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label9, null);
            // 
            // label23
            // 
            this.coreBind.SetDatabasecommand(this.label23, null);
            this.label23.Location = new System.Drawing.Point(9, 5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 24);
            this.label23.TabIndex = 33;
            this.label23.Text = "炉号";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label23, null);
            // 
            // txtLH
            // 
            this.txtLH.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtLH, null);
            this.txtLH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtLH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLH.Location = new System.Drawing.Point(80, 3);
            this.txtLH.MaxLength = 9;
            this.txtLH.Name = "txtLH";
            this.txtLH.Size = new System.Drawing.Size(141, 24);
            this.txtLH.TabIndex = 0;
            this.coreBind.SetVerification(this.txtLH, null);
            this.txtLH.Leave += new System.EventHandler(this.txtLH_Leave);
            this.txtLH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLH_KeyPress);
            // 
            // label24
            // 
            this.coreBind.SetDatabasecommand(this.label24, null);
            this.label24.Location = new System.Drawing.Point(277, 82);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(69, 24);
            this.label24.TabIndex = 34;
            this.label24.Text = "长度";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label24, null);
            // 
            // ultraGroupBox3
            // 
            this.ultraGroupBox3.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularInset;
            this.ultraGroupBox3.Controls.Add(this.ultraGrid2);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox3, null);
            this.ultraGroupBox3.Location = new System.Drawing.Point(534, 156);
            this.ultraGroupBox3.Name = "ultraGroupBox3";
            this.ultraGroupBox3.Size = new System.Drawing.Size(182, 22);
            this.ultraGroupBox3.TabIndex = 60;
            this.ultraGroupBox3.Text = "计量点信息";
            this.coreBind.SetVerification(this.ultraGroupBox3, null);
            this.ultraGroupBox3.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            this.ultraGroupBox3.Visible = false;
            // 
            // ultraGrid2
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid2, null);
            this.ultraGrid2.DataMember = "计量点基础表";
            this.ultraGrid2.DataSource = this.dataSet1;
            appearance2.BackColor = System.Drawing.Color.White;
            appearance2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid2.DisplayLayout.Appearance = appearance2;
            ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 118;
            ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn7.Header.VisiblePosition = 0;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn7.Width = 45;
            ultraGridColumn8.Header.VisiblePosition = 3;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn9.Header.VisiblePosition = 4;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn10.Header.VisiblePosition = 5;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn11.Header.VisiblePosition = 6;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn12.Header.VisiblePosition = 7;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn13.Header.VisiblePosition = 8;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn14.Header.VisiblePosition = 9;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn15.Header.VisiblePosition = 10;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn16.Header.VisiblePosition = 11;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn17.Header.VisiblePosition = 12;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn18.Header.VisiblePosition = 13;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn19.Header.VisiblePosition = 14;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn20.Header.VisiblePosition = 15;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn21.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn21.Header.VisiblePosition = 16;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn22.Header.VisiblePosition = 17;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn23.Header.VisiblePosition = 18;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn24.Header.VisiblePosition = 19;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn25.Header.VisiblePosition = 20;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn26.Header.VisiblePosition = 21;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn27.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn27.Header.VisiblePosition = 22;
            ultraGridColumn27.Hidden = true;
            ultraGridColumn28.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn28.Header.VisiblePosition = 23;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn29.Header.VisiblePosition = 24;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn30.Header.VisiblePosition = 25;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn31.Header.VisiblePosition = 26;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn32.Header.VisiblePosition = 27;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn33.Header.VisiblePosition = 28;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn34.Header.VisiblePosition = 29;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn35.Header.VisiblePosition = 30;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn36.Header.VisiblePosition = 31;
            ultraGridColumn36.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
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
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36});
            this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraGrid2.DisplayLayout.InterBandSpacing = 10;
            appearance8.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid2.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance9.BackColor2 = System.Drawing.Color.White;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance9.FontData.SizeInPoints = 11F;
            appearance9.FontData.UnderlineAsString = "False";
            appearance9.ForeColor = System.Drawing.Color.Black;
            appearance9.TextHAlignAsString = "Center";
            appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid2.DisplayLayout.Override.HeaderAppearance = appearance9;
            appearance10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.Override.RowAppearance = appearance10;
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance11.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorAppearance = appearance11;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid2.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance12.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance12.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid2.DisplayLayout.Override.SelectedRowAppearance = appearance12;
            this.ultraGrid2.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid2.Location = new System.Drawing.Point(3, 26);
            this.ultraGrid2.Name = "ultraGrid2";
            this.ultraGrid2.Size = new System.Drawing.Size(175, 0);
            this.ultraGrid2.TabIndex = 62;
            this.ultraGrid2.TabStop = false;
            this.coreBind.SetVerification(this.ultraGrid2, null);
            this.ultraGrid2.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.ultraGrid2_AfterSelectChange);
            // 
            // ultraGroupBox4
            // 
            this.ultraGroupBox4.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularInset;
            this.ultraGroupBox4.Controls.Add(this.ultraGrid5);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox4, null);
            this.ultraGroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGroupBox4.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox4.Name = "ultraGroupBox4";
            this.ultraGroupBox4.Size = new System.Drawing.Size(428, 458);
            this.ultraGroupBox4.TabIndex = 63;
            this.ultraGroupBox4.Text = "未完炉预报信息";
            this.coreBind.SetVerification(this.ultraGroupBox4, null);
            this.ultraGroupBox4.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // ultraGrid5
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid5, null);
            this.ultraGrid5.DataMember = "预报表";
            this.ultraGrid5.DataSource = this.dataSet1;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid5.DisplayLayout.Appearance = appearance1;
            ultraGridColumn37.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn37.Header.VisiblePosition = 0;
            ultraGridColumn37.Width = 105;
            ultraGridColumn38.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn38.Header.VisiblePosition = 3;
            ultraGridColumn38.Width = 83;
            ultraGridColumn39.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn39.Header.VisiblePosition = 4;
            ultraGridColumn39.Width = 78;
            ultraGridColumn40.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn40.Header.VisiblePosition = 5;
            ultraGridColumn40.Width = 63;
            ultraGridColumn41.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn41.Header.VisiblePosition = 1;
            ultraGridColumn41.Width = 73;
            ultraGridColumn42.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn42.Header.VisiblePosition = 6;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn43.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn43.Header.VisiblePosition = 7;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn44.Header.VisiblePosition = 12;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn45.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn45.Header.VisiblePosition = 11;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn46.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn46.Header.VisiblePosition = 10;
            ultraGridColumn46.Hidden = true;
            ultraGridColumn47.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn47.Header.VisiblePosition = 2;
            ultraGridColumn47.Hidden = true;
            ultraGridColumn48.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn48.Header.VisiblePosition = 8;
            ultraGridColumn48.Hidden = true;
            ultraGridColumn49.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn49.Header.VisiblePosition = 9;
            ultraGridColumn49.Hidden = true;
            ultraGridColumn50.Header.VisiblePosition = 13;
            ultraGridColumn50.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(72, 0);
            ultraGridBand3.Columns.AddRange(new object[] {
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
            ultraGridColumn49,
            ultraGridColumn50});
            ultraGridBand3.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            ultraGridBand3.RowLayoutStyle = Infragistics.Win.UltraWinGrid.RowLayoutStyle.GroupLayout;
            summarySettings1.DisplayFormat = "合计{0}炉";
            summarySettings1.GroupBySummaryValueAppearance = appearance24;
            summarySettings2.DisplayFormat = "{0}块";
            summarySettings2.GroupBySummaryValueAppearance = appearance28;
            ultraGridBand3.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings1,
            summarySettings2});
            this.ultraGrid5.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.ultraGrid5.DisplayLayout.InterBandSpacing = 10;
            appearance7.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid5.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance19.BackColor2 = System.Drawing.Color.White;
            appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance19.FontData.SizeInPoints = 11F;
            appearance19.ForeColor = System.Drawing.Color.Black;
            appearance19.TextHAlignAsString = "Center";
            appearance19.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid5.DisplayLayout.Override.HeaderAppearance = appearance19;
            appearance20.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid5.DisplayLayout.Override.RowAppearance = appearance20;
            appearance22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance22.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorAppearance = appearance22;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid5.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance27.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance27.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid5.DisplayLayout.Override.SelectedRowAppearance = appearance27;
            this.ultraGrid5.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid5.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid5.Location = new System.Drawing.Point(3, 25);
            this.ultraGrid5.Name = "ultraGrid5";
            this.ultraGrid5.Size = new System.Drawing.Size(421, 430);
            this.ultraGrid5.TabIndex = 61;
            this.ultraGrid5.TabStop = false;
            this.coreBind.SetVerification(this.ultraGrid5, null);
            this.ultraGrid5.Click += new System.EventHandler(this.ultraGrid5_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularInset;
            this.ultraGroupBox1.Controls.Add(this.ultraGrid1);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox1, null);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(234, 458);
            this.ultraGroupBox1.TabIndex = 64;
            this.ultraGroupBox1.Text = "计量称重信息";
            this.coreBind.SetVerification(this.ultraGroupBox1, null);
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // ultraGrid1
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid1, null);
            this.ultraGrid1.DataMember = "辊道计量从表";
            this.ultraGrid1.DataSource = this.dataSet1;
            appearance33.BackColor = System.Drawing.Color.White;
            appearance33.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid1.DisplayLayout.Appearance = appearance33;
            ultraGridColumn51.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn51.Header.VisiblePosition = 0;
            ultraGridColumn51.RowLayoutColumnInfo.OriginX = 0;
            ultraGridColumn51.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn51.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(98, 0);
            ultraGridColumn51.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn51.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn51.Width = 94;
            ultraGridColumn52.Header.VisiblePosition = 1;
            ultraGridColumn52.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(90, 0);
            ultraGridColumn53.Header.VisiblePosition = 2;
            ultraGridColumn53.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(74, 0);
            ultraGridColumn54.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn54.Header.VisiblePosition = 4;
            ultraGridColumn54.RowLayoutColumnInfo.OriginX = 12;
            ultraGridColumn54.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn54.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn54.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn54.Width = 135;
            ultraGridColumn55.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn55.Header.VisiblePosition = 3;
            ultraGridColumn55.RowLayoutColumnInfo.OriginX = 6;
            ultraGridColumn55.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn55.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn55.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn55.Width = 50;
            ultraGridColumn56.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn56.Header.VisiblePosition = 6;
            ultraGridColumn56.RowLayoutColumnInfo.OriginX = 8;
            ultraGridColumn56.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn56.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(53, 0);
            ultraGridColumn56.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn56.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn57.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn57.Header.VisiblePosition = 5;
            ultraGridColumn57.RowLayoutColumnInfo.OriginX = 10;
            ultraGridColumn57.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn57.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn57.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn57.Width = 60;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57});
            ultraGridBand4.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            ultraGridBand4.RowLayoutStyle = Infragistics.Win.UltraWinGrid.RowLayoutStyle.GroupLayout;
            summarySettings3.DisplayFormat = "合计{0}条";
            summarySettings3.GroupBySummaryValueAppearance = appearance29;
            ultraGridBand4.Summaries.AddRange(new Infragistics.Win.UltraWinGrid.SummarySettings[] {
            summarySettings3});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.ultraGrid1.DisplayLayout.InterBandSpacing = 10;
            appearance43.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance43;
            appearance44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance44.BackColor2 = System.Drawing.Color.White;
            appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance44.FontData.SizeInPoints = 11F;
            appearance44.ForeColor = System.Drawing.Color.Black;
            appearance44.TextHAlignAsString = "Center";
            appearance44.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance44;
            appearance45.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance45;
            appearance46.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance46.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorAppearance = appearance46;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid1.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance47.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance47.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid1.DisplayLayout.Override.SelectedRowAppearance = appearance47;
            this.ultraGrid1.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid1.Location = new System.Drawing.Point(3, 25);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(227, 430);
            this.ultraGrid1.TabIndex = 61;
            this.ultraGrid1.TabStop = false;
            this.coreBind.SetVerification(this.ultraGrid1, null);
            this.ultraGrid1.DoubleClick += new System.EventHandler(this.ultraGrid1_DoubleClick);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel6.Controls.Add(this.btnQL);
            this.panel6.Controls.Add(this.txtXSZL);
            this.panel6.Controls.Add(this.chkAutoSave);
            this.panel6.Controls.Add(this.ultraLabel1);
            this.panel6.Controls.Add(this.ultraLabel3);
            this.panel6.Controls.Add(this.ultraTextEditor5);
            this.panel6.Controls.Add(this.ultraLabel7);
            this.panel6.Controls.Add(this.ultraTextEditor3);
            this.panel6.Controls.Add(this.ultraLabel2);
            this.panel6.Controls.Add(this.ultraOptionSet1);
            this.panel6.Controls.Add(this.btChanShift);
            this.panel6.Controls.Add(this.ultraLabel4);
            this.panel6.Controls.Add(this.lbWD);
            this.panel6.Controls.Add(this.lbYS);
            this.panel6.Controls.Add(this.label2);
            this.coreBind.SetDatabasecommand(this.panel6, null);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(345, 27);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(662, 99);
            this.panel6.TabIndex = 51;
            this.coreBind.SetVerification(this.panel6, null);
            // 
            // btnQL
            // 
            this.btnQL.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.coreBind.SetDatabasecommand(this.btnQL, null);
            this.btnQL.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQL.Location = new System.Drawing.Point(431, 53);
            this.btnQL.Name = "btnQL";
            this.btnQL.Size = new System.Drawing.Size(93, 28);
            this.btnQL.TabIndex = 753;
            this.btnQL.Text = "清零";
            this.btnQL.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnQL, null);
            this.btnQL.Click += new System.EventHandler(this.btnQL_Click_1);
            // 
            // txtXSZL
            // 
            this.txtXSZL.BackColor = System.Drawing.Color.Transparent;
            this.txtXSZL.BackColor_1 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtXSZL.BackColor_2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtXSZL.BevelRate = 0.5F;
            this.txtXSZL.CornerRadius = 6;
            this.coreBind.SetDatabasecommand(this.txtXSZL, null);
            this.txtXSZL.FadedColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtXSZL.ForeColor = System.Drawing.Color.Green;
            this.txtXSZL.HighlightOpaque = ((byte)(50));
            this.txtXSZL.Location = new System.Drawing.Point(73, 15);
            this.txtXSZL.Name = "txtXSZL";
            this.txtXSZL.Size = new System.Drawing.Size(258, 71);
            this.txtXSZL.TabIndex = 752;
            this.txtXSZL.Text = "0.000";
            this.txtXSZL.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.txtXSZL.TotalCharCount = 7;
            this.coreBind.SetVerification(this.txtXSZL, null);
            // 
            // ultraLabel1
            // 
            appearance26.BackColor = System.Drawing.Color.LightBlue;
            appearance26.TextHAlignAsString = "Center";
            appearance26.TextVAlignAsString = "Middle";
            this.ultraLabel1.Appearance = appearance26;
            this.coreBind.SetDatabasecommand(this.ultraLabel1, null);
            this.ultraLabel1.Font = new System.Drawing.Font("宋体", 14F);
            this.ultraLabel1.Location = new System.Drawing.Point(907, 15);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(28, 28);
            this.ultraLabel1.TabIndex = 751;
            this.ultraLabel1.Text = "块";
            this.coreBind.SetVerification(this.ultraLabel1, null);
            // 
            // ultraLabel3
            // 
            appearance23.BackColor = System.Drawing.Color.LightBlue;
            appearance23.TextHAlignAsString = "Center";
            appearance23.TextVAlignAsString = "Middle";
            this.ultraLabel3.Appearance = appearance23;
            this.coreBind.SetDatabasecommand(this.ultraLabel3, null);
            this.ultraLabel3.Font = new System.Drawing.Font("宋体", 14F);
            this.ultraLabel3.Location = new System.Drawing.Point(906, 53);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(28, 28);
            this.ultraLabel3.TabIndex = 750;
            this.ultraLabel3.Text = "吨";
            this.coreBind.SetVerification(this.ultraLabel3, null);
            // 
            // ultraTextEditor5
            // 
            appearance5.BorderColor = System.Drawing.Color.Black;
            this.ultraTextEditor5.Appearance = appearance5;
            this.ultraTextEditor5.AutoSize = false;
            this.coreBind.SetDatabasecommand(this.ultraTextEditor5, null);
            this.ultraTextEditor5.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.OfficeXP;
            this.ultraTextEditor5.Enabled = false;
            this.ultraTextEditor5.Font = new System.Drawing.Font("宋体", 14F);
            this.ultraTextEditor5.Location = new System.Drawing.Point(813, 15);
            this.ultraTextEditor5.MaxLength = 7;
            this.ultraTextEditor5.Name = "ultraTextEditor5";
            this.ultraTextEditor5.Size = new System.Drawing.Size(93, 28);
            this.ultraTextEditor5.TabIndex = 749;
            this.coreBind.SetVerification(this.ultraTextEditor5, null);
            // 
            // ultraLabel7
            // 
            appearance39.BackColor = System.Drawing.Color.LightBlue;
            appearance39.TextHAlignAsString = "Center";
            appearance39.TextVAlignAsString = "Middle";
            this.ultraLabel7.Appearance = appearance39;
            this.coreBind.SetDatabasecommand(this.ultraLabel7, null);
            this.ultraLabel7.Font = new System.Drawing.Font("宋体", 14F);
            this.ultraLabel7.Location = new System.Drawing.Point(729, 15);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(85, 28);
            this.ultraLabel7.TabIndex = 748;
            this.ultraLabel7.Text = "累计块数";
            this.coreBind.SetVerification(this.ultraLabel7, null);
            // 
            // ultraTextEditor3
            // 
            appearance21.BorderColor = System.Drawing.Color.Black;
            this.ultraTextEditor3.Appearance = appearance21;
            this.ultraTextEditor3.AutoSize = false;
            this.coreBind.SetDatabasecommand(this.ultraTextEditor3, null);
            this.ultraTextEditor3.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.OfficeXP;
            this.ultraTextEditor3.Enabled = false;
            this.ultraTextEditor3.Font = new System.Drawing.Font("宋体", 14F);
            this.ultraTextEditor3.Location = new System.Drawing.Point(813, 53);
            this.ultraTextEditor3.MaxLength = 10;
            this.ultraTextEditor3.Name = "ultraTextEditor3";
            this.ultraTextEditor3.Size = new System.Drawing.Size(93, 28);
            this.ultraTextEditor3.TabIndex = 747;
            this.coreBind.SetVerification(this.ultraTextEditor3, null);
            // 
            // ultraLabel2
            // 
            appearance40.BackColor = System.Drawing.Color.LightBlue;
            appearance40.TextHAlignAsString = "Center";
            appearance40.TextVAlignAsString = "Middle";
            this.ultraLabel2.Appearance = appearance40;
            this.coreBind.SetDatabasecommand(this.ultraLabel2, null);
            this.ultraLabel2.Font = new System.Drawing.Font("宋体", 14F);
            this.ultraLabel2.Location = new System.Drawing.Point(729, 53);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(85, 28);
            this.ultraLabel2.TabIndex = 746;
            this.ultraLabel2.Text = "累计重量";
            this.coreBind.SetVerification(this.ultraLabel2, null);
            // 
            // ultraOptionSet1
            // 
            appearance3.FontData.SizeInPoints = 14F;
            this.ultraOptionSet1.Appearance = appearance3;
            this.ultraOptionSet1.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.coreBind.SetDatabasecommand(this.ultraOptionSet1, null);
            valueListItem1.DataValue = "1";
            valueListItem1.DisplayText = "中宽带";
            this.ultraOptionSet1.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1});
            this.ultraOptionSet1.Location = new System.Drawing.Point(646, 17);
            this.ultraOptionSet1.Name = "ultraOptionSet1";
            this.ultraOptionSet1.Size = new System.Drawing.Size(81, 23);
            this.ultraOptionSet1.TabIndex = 95;
            this.coreBind.SetVerification(this.ultraOptionSet1, null);
            this.ultraOptionSet1.ValueChanged += new System.EventHandler(this.ultraOptionSet1_ValueChanged);
            // 
            // btChanShift
            // 
            this.btChanShift.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.coreBind.SetDatabasecommand(this.btChanShift, null);
            this.btChanShift.Location = new System.Drawing.Point(647, 52);
            this.btChanShift.Name = "btChanShift";
            this.btChanShift.Size = new System.Drawing.Size(62, 28);
            this.btChanShift.TabIndex = 745;
            this.btChanShift.TabStop = false;
            this.btChanShift.Text = "换 班";
            this.btChanShift.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btChanShift, null);
            this.btChanShift.Click += new System.EventHandler(this.btChanShift_Click_1);
            // 
            // ultraLabel4
            // 
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance6.TextHAlignAsString = "Center";
            appearance6.TextVAlignAsString = "Middle";
            this.ultraLabel4.Appearance = appearance6;
            this.coreBind.SetDatabasecommand(this.ultraLabel4, null);
            this.ultraLabel4.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraLabel4.Location = new System.Drawing.Point(565, 19);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(67, 21);
            this.ultraLabel4.TabIndex = 96;
            this.ultraLabel4.Text = "计量点";
            this.coreBind.SetVerification(this.ultraLabel4, null);
            // 
            // lbWD
            // 
            this.lbWD.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.lbWD, null);
            this.lbWD.Font = new System.Drawing.Font("宋体", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWD.Location = new System.Drawing.Point(465, 17);
            this.lbWD.Name = "lbWD";
            this.lbWD.Size = new System.Drawing.Size(58, 23);
            this.lbWD.TabIndex = 93;
            this.lbWD.Text = "稳定";
            this.coreBind.SetVerification(this.lbWD, null);
            // 
            // lbYS
            // 
            this.coreBind.SetDatabasecommand(this.lbYS, null);
            this.lbYS.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbYS.ForeColor = System.Drawing.Color.Lime;
            this.lbYS.Location = new System.Drawing.Point(424, 14);
            this.lbYS.Name = "lbYS";
            this.lbYS.Size = new System.Drawing.Size(30, 28);
            this.lbYS.TabIndex = 94;
            this.lbYS.Text = "●";
            this.coreBind.SetVerification(this.lbYS, null);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.coreBind.SetDatabasecommand(this.label2, null);
            this.label2.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(337, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 56);
            this.label2.TabIndex = 32;
            this.label2.Text = "吨";
            this.coreBind.SetVerification(this.label2, null);
            // 
            // BilletInfo_Fill_Panel
            // 
            this.BilletInfo_Fill_Panel.Controls.Add(this.panel8);
            this.BilletInfo_Fill_Panel.Controls.Add(this.panel6);
            this.BilletInfo_Fill_Panel.Controls.Add(this.panel2);
            this.BilletInfo_Fill_Panel.Controls.Add(this.panel1);
            this.BilletInfo_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.BilletInfo_Fill_Panel, null);
            this.BilletInfo_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BilletInfo_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.BilletInfo_Fill_Panel.Name = "BilletInfo_Fill_Panel";
            this.BilletInfo_Fill_Panel.Size = new System.Drawing.Size(1007, 584);
            this.BilletInfo_Fill_Panel.TabIndex = 0;
            this.coreBind.SetVerification(this.BilletInfo_Fill_Panel, null);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel9);
            this.coreBind.SetDatabasecommand(this.panel8, null);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(345, 126);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(662, 458);
            this.panel8.TabIndex = 53;
            this.coreBind.SetVerification(this.panel8, null);
            // 
            // panel10
            // 
            this.panel10.AutoScroll = true;
            this.panel10.Controls.Add(this.ultraGroupBox1);
            this.coreBind.SetDatabasecommand(this.panel10, null);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(428, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(234, 458);
            this.panel10.TabIndex = 1;
            this.coreBind.SetVerification(this.panel10, null);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.ultraGroupBox4);
            this.coreBind.SetDatabasecommand(this.panel9, null);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(428, 458);
            this.panel9.TabIndex = 0;
            this.coreBind.SetVerification(this.panel9, null);
            // 
            // ultraDockManager1
            // 
            this.ultraDockManager1.CompressUnpinnedTabs = false;
            dockAreaPane1.DockedBefore = new System.Guid("be8b4e0c-9178-4069-8244-a71509396517");
            dockableControlPane1.Control = this.panelYYBF;
            dockableControlPane1.FlyoutSize = new System.Drawing.Size(108, -1);
            dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(5, 114, 200, 100);
            dockableControlPane1.Pinned = false;
            dockableControlPane1.Size = new System.Drawing.Size(100, 100);
            dockableControlPane1.Text = "语音播报";
            dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane1});
            dockAreaPane1.Size = new System.Drawing.Size(95, 612);
            dockableControlPane2.Closed = true;
            dockableControlPane2.Control = this.panelSPKZ;
            dockableControlPane2.FlyoutSize = new System.Drawing.Size(139, -1);
            dockableControlPane2.OriginalControlBounds = new System.Drawing.Rectangle(267, 63, 200, 100);
            dockableControlPane2.Size = new System.Drawing.Size(100, 100);
            dockableControlPane2.Text = "视频控制";
            dockAreaPane2.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane2});
            dockAreaPane2.Size = new System.Drawing.Size(95, 612);
            this.ultraDockManager1.DockAreas.AddRange(new Infragistics.Win.UltraWinDock.DockAreaPane[] {
            dockAreaPane1,
            dockAreaPane2});
            this.ultraDockManager1.HostControl = this;
            this.ultraDockManager1.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.Office2003;
            // 
            // _FrmBaseUnpinnedTabAreaLeft
            // 
            this.coreBind.SetDatabasecommand(this._FrmBaseUnpinnedTabAreaLeft, null);
            this._FrmBaseUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._FrmBaseUnpinnedTabAreaLeft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._FrmBaseUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 0);
            this._FrmBaseUnpinnedTabAreaLeft.Name = "_FrmBaseUnpinnedTabAreaLeft";
            this._FrmBaseUnpinnedTabAreaLeft.Owner = this.ultraDockManager1;
            this._FrmBaseUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 584);
            this._FrmBaseUnpinnedTabAreaLeft.TabIndex = 609;
            this.coreBind.SetVerification(this._FrmBaseUnpinnedTabAreaLeft, null);
            // 
            // _FrmBaseUnpinnedTabAreaRight
            // 
            this.coreBind.SetDatabasecommand(this._FrmBaseUnpinnedTabAreaRight, null);
            this._FrmBaseUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._FrmBaseUnpinnedTabAreaRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._FrmBaseUnpinnedTabAreaRight.Location = new System.Drawing.Point(1007, 0);
            this._FrmBaseUnpinnedTabAreaRight.Name = "_FrmBaseUnpinnedTabAreaRight";
            this._FrmBaseUnpinnedTabAreaRight.Owner = this.ultraDockManager1;
            this._FrmBaseUnpinnedTabAreaRight.Size = new System.Drawing.Size(21, 584);
            this._FrmBaseUnpinnedTabAreaRight.TabIndex = 610;
            this.coreBind.SetVerification(this._FrmBaseUnpinnedTabAreaRight, null);
            // 
            // _FrmBaseUnpinnedTabAreaTop
            // 
            this.coreBind.SetDatabasecommand(this._FrmBaseUnpinnedTabAreaTop, null);
            this._FrmBaseUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._FrmBaseUnpinnedTabAreaTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._FrmBaseUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 0);
            this._FrmBaseUnpinnedTabAreaTop.Name = "_FrmBaseUnpinnedTabAreaTop";
            this._FrmBaseUnpinnedTabAreaTop.Owner = this.ultraDockManager1;
            this._FrmBaseUnpinnedTabAreaTop.Size = new System.Drawing.Size(1007, 0);
            this._FrmBaseUnpinnedTabAreaTop.TabIndex = 611;
            this.coreBind.SetVerification(this._FrmBaseUnpinnedTabAreaTop, null);
            // 
            // _FrmBaseUnpinnedTabAreaBottom
            // 
            this.coreBind.SetDatabasecommand(this._FrmBaseUnpinnedTabAreaBottom, null);
            this._FrmBaseUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._FrmBaseUnpinnedTabAreaBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._FrmBaseUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 584);
            this._FrmBaseUnpinnedTabAreaBottom.Name = "_FrmBaseUnpinnedTabAreaBottom";
            this._FrmBaseUnpinnedTabAreaBottom.Owner = this.ultraDockManager1;
            this._FrmBaseUnpinnedTabAreaBottom.Size = new System.Drawing.Size(1007, 0);
            this._FrmBaseUnpinnedTabAreaBottom.TabIndex = 612;
            this.coreBind.SetVerification(this._FrmBaseUnpinnedTabAreaBottom, null);
            // 
            // _FrmBaseAutoHideControl
            // 
            this._FrmBaseAutoHideControl.Controls.Add(this.dockableWindow1);
            this.coreBind.SetDatabasecommand(this._FrmBaseAutoHideControl, null);
            this._FrmBaseAutoHideControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._FrmBaseAutoHideControl.Location = new System.Drawing.Point(994, 0);
            this._FrmBaseAutoHideControl.Name = "_FrmBaseAutoHideControl";
            this._FrmBaseAutoHideControl.Owner = this.ultraDockManager1;
            this._FrmBaseAutoHideControl.Size = new System.Drawing.Size(13, 584);
            this._FrmBaseAutoHideControl.TabIndex = 613;
            this.coreBind.SetVerification(this._FrmBaseAutoHideControl, null);
            // 
            // dockableWindow1
            // 
            this.dockableWindow1.Controls.Add(this.panelYYBF);
            this.coreBind.SetDatabasecommand(this.dockableWindow1, null);
            this.dockableWindow1.Location = new System.Drawing.Point(5, 0);
            this.dockableWindow1.Name = "dockableWindow1";
            this.dockableWindow1.Owner = this.ultraDockManager1;
            this.dockableWindow1.Size = new System.Drawing.Size(108, 584);
            this.dockableWindow1.TabIndex = 616;
            this.coreBind.SetVerification(this.dockableWindow1, null);
            // 
            // windowDockingArea1
            // 
            this.coreBind.SetDatabasecommand(this.windowDockingArea1, null);
            this.windowDockingArea1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowDockingArea1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowDockingArea1.Location = new System.Drawing.Point(0, 0);
            this.windowDockingArea1.Name = "windowDockingArea1";
            this.windowDockingArea1.Owner = this.ultraDockManager1;
            this.windowDockingArea1.Size = new System.Drawing.Size(100, 612);
            this.windowDockingArea1.TabIndex = 614;
            this.coreBind.SetVerification(this.windowDockingArea1, null);
            // 
            // windowDockingArea2
            // 
            this.windowDockingArea2.Controls.Add(this.dockableWindow2);
            this.coreBind.SetDatabasecommand(this.windowDockingArea2, null);
            this.windowDockingArea2.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowDockingArea2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowDockingArea2.Location = new System.Drawing.Point(0, 0);
            this.windowDockingArea2.Name = "windowDockingArea2";
            this.windowDockingArea2.Owner = this.ultraDockManager1;
            this.windowDockingArea2.Size = new System.Drawing.Size(100, 612);
            this.windowDockingArea2.TabIndex = 615;
            this.coreBind.SetVerification(this.windowDockingArea2, null);
            // 
            // dockableWindow2
            // 
            this.dockableWindow2.Controls.Add(this.panelSPKZ);
            this.coreBind.SetDatabasecommand(this.dockableWindow2, null);
            this.dockableWindow2.Location = new System.Drawing.Point(-10000, 0);
            this.dockableWindow2.Name = "dockableWindow2";
            this.dockableWindow2.Owner = this.ultraDockManager1;
            this.dockableWindow2.Size = new System.Drawing.Size(139, 612);
            this.dockableWindow2.TabIndex = 617;
            this.coreBind.SetVerification(this.dockableWindow2, null);
            // 
            // BoardBilletWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 584);
            this.Controls.Add(this._FrmBaseAutoHideControl);
            this.Controls.Add(this.picFDTP);
            this.Controls.Add(this.BilletInfo_Fill_Panel);
            this.Controls.Add(this.windowDockingArea1);
            this.Controls.Add(this.windowDockingArea2);
            this.Controls.Add(this._FrmBaseUnpinnedTabAreaTop);
            this.Controls.Add(this._FrmBaseUnpinnedTabAreaBottom);
            this.Controls.Add(this._FrmBaseUnpinnedTabAreaLeft);
            this.Controls.Add(this._FrmBaseUnpinnedTabAreaRight);
            this.coreBind.SetDatabasecommand(this, null);
            this.KeyPreview = true;
            this.Name = "BoardBilletWeight";
            this.Text = "BilletInfo";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.BoardBilletWeight_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BilletInfo_KeyPress);
            this.panelYYBF.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.handWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).EndInit();
            this.panel14.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel3)).EndInit();
            this.panel7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).EndInit();
            this.ultraGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).EndInit();
            this.ultraGroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtXSZL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditor3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraOptionSet1)).EndInit();
            this.BilletInfo_Fill_Panel.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).EndInit();
            this._FrmBaseAutoHideControl.ResumeLayout(false);
            this.dockableWindow1.ResumeLayout(false);
            this.windowDockingArea2.ResumeLayout(false);
            this.dockableWindow2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private System.Windows.Forms.Panel panelSPKZ;
        private System.Windows.Forms.Panel panelYYBF;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid4;
        private System.Data.DataSet dataSet1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn12;
        private System.Data.DataColumn dataColumn14;
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn17;
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
        private System.Data.DataColumn dataColumn28;
        private System.Data.DataColumn dataColumn29;
        private System.Data.DataColumn dataColumn30;
        private System.Data.DataColumn dataColumn31;
        private System.Data.DataColumn dataColumn32;
        private System.Data.DataColumn dataColumn33;
        private System.Data.DataColumn dataColumn34;
        private System.Data.DataColumn dataColumn35;
        private System.Data.DataColumn dataColumn36;
        private System.Data.DataColumn dataColumn37;
        private System.Data.DataColumn dataColumn38;
        private System.Data.DataColumn dataColumn39;
        private System.Data.DataColumn dataColumn40;
        private System.Data.DataColumn dataColumn41;
        private System.Data.DataColumn dataColumn42;
        private System.Data.DataTable dataTable4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataTable dataTable5;
        private System.Data.DataColumn dataColumn10;
        private System.Data.DataColumn dataColumn11;
        private System.Data.DataColumn dataColumn13;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        public System.Data.DataTable dataTable1;
        private System.Windows.Forms.PictureBox picFDTP;
        private System.Data.DataColumn dataColumn43;
        private System.Data.DataColumn dataColumn44;
        private System.Data.DataColumn dataColumn45;
        private System.Data.DataColumn dataColumn46;
        private System.Data.DataColumn dataColumn47;
        private System.Data.DataColumn dataColumn48;
        private System.Data.DataColumn dataColumn49;
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn dataColumn50;
        private System.Data.DataColumn dataColumn51;
        private System.Data.DataColumn dataColumn52;
        private System.Data.DataColumn dataColumn53;
        private System.Data.DataColumn dataColumn54;
        private System.Data.DataColumn dataColumn55;
        private System.Data.DataColumn dataColumn56;
        private System.Data.DataColumn dataColumn57;
        private System.Data.DataColumn dataColumn58;
        private System.Data.DataColumn dataColumn59;
        private System.Data.DataColumn dataColumn60;
        private System.Data.DataColumn dataColumn61;
        private System.Data.DataColumn dataColumn62;
        private System.Data.DataTable dataTable6;
        private System.Data.DataColumn dataColumn63;
        private System.Data.DataColumn dataColumn64;
        private System.Data.DataColumn dataColumn65;
        private System.Data.DataColumn dataColumn66;
        private System.Data.DataColumn dataColumn67;
        private System.Data.DataColumn dataColumn69;
        private System.Data.DataColumn dataColumn70;
        private System.Data.DataColumn dataColumn72;
        private System.Data.DataColumn dataColumn73;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.Panel panel1_Fill_Panel;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;
        private System.Windows.Forms.Panel BilletInfo_Fill_Panel;
        private System.Windows.Forms.Panel panel7;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbx_llastTotalWeight;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbx_llastBilletCount;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbx_llastStoveNo;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbx_lastTotalWeight;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbx_lastBilletCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_lastStoveNo;
        private System.Windows.Forms.Button btXF;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDS;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox txtBB;
        private System.Windows.Forms.TextBox txtBC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbWLMC;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDDH;
        private System.Windows.Forms.ComboBox cbGZ;
        private System.Windows.Forms.ComboBox cbGG;
        private System.Windows.Forms.ComboBox cbSHDW;
        private System.Windows.Forms.TextBox txtJLY;
        private System.Windows.Forms.TextBox txtCD;
        private System.Windows.Forms.TextBox txtDDXMH;
        private System.Windows.Forms.ComboBox cbFHDW;
        private System.Windows.Forms.ComboBox cbLiuX;
        private System.Windows.Forms.TextBox txtJLD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtLH;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel6;
        private Infragistics.Win.UltraWinEditors.UltraOptionSet ultraOptionSet1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private System.Windows.Forms.Label lbWD;
        private System.Windows.Forms.Label lbYS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Windows.Forms.PictureBox VideoChannel2;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox4;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid5;
        private System.Windows.Forms.PictureBox VideoChannel3;
        private System.Windows.Forms.Panel panel8;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btLLBC;
        private System.Windows.Forms.Button btnBC;
        private System.Windows.Forms.Button btChanShift;
        private System.Windows.Forms.TextBox txtZL;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private Infragistics.Win.UltraWinDock.UltraDockManager ultraDockManager1;
        private Infragistics.Win.UltraWinDock.AutoHideControl _FrmBaseAutoHideControl;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow1;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _FrmBaseUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _FrmBaseUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _FrmBaseUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea2;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow2;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _FrmBaseUnpinnedTabAreaRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor ultraTextEditor5;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor ultraTextEditor3;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private System.Windows.Forms.PictureBox VideoChannel1;
        private LxControl.LxLedControl txtXSZL;
        private System.Windows.Forms.Button btnQL;
        private System.Data.DataColumn dataColumn68;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor handWeight;
    }
}
