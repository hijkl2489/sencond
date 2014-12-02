namespace YGJZJL.Pipe
{
    partial class Weight_PIPE
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("语音表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICENAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_INSTRTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MEMO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICEFILE");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("e801a732-ee6c-4e51-a24d-f0583ba849af"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("b86aa706-8cb5-428a-808d-cbd7078e1944"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("e801a732-ee6c-4e51-a24d-f0583ba849af"), -1);
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YYDJ");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool2 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("Container1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloseLED");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool6 = new Infragistics.Win.UltraWinToolbars.ButtonTool("QueryPlan");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool3 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("剩余纸张数");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool13 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangePage");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool5 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("剩余碳带数");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool15 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangeTink");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool7 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btCorrention");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YYDJ");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool1 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("Container1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("CloseLED");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool5 = new Infragistics.Win.UltraWinToolbars.ButtonTool("QueryPlan");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangePage");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool10 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangeTink");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool4 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("剩余纸张数");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool6 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("剩余碳带数");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool9 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btCorrention");
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
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
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FF_CLEARVALUE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TOTALPAPAR");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TOTALINK");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            this.VoiceC = new System.Windows.Forms.Panel();
            this.ultraGrid5 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn26 = new System.Data.DataColumn();
            this.dataColumn27 = new System.Data.DataColumn();
            this.dataColumn28 = new System.Data.DataColumn();
            this.dataColumn52 = new System.Data.DataColumn();
            this.dataColumn63 = new System.Data.DataColumn();
            this.dataColumn64 = new System.Data.DataColumn();
            this.dataColumn65 = new System.Data.DataColumn();
            this.dataColumn66 = new System.Data.DataColumn();
            this.dataColumn67 = new System.Data.DataColumn();
            this.dataColumn68 = new System.Data.DataColumn();
            this.dataColumn69 = new System.Data.DataColumn();
            this.dataColumn70 = new System.Data.DataColumn();
            this.dataColumn72 = new System.Data.DataColumn();
            this.dataColumn73 = new System.Data.DataColumn();
            this.dataColumn74 = new System.Data.DataColumn();
            this.dataColumn75 = new System.Data.DataColumn();
            this.dataColumn76 = new System.Data.DataColumn();
            this.dataColumn77 = new System.Data.DataColumn();
            this.dataColumn78 = new System.Data.DataColumn();
            this.dataColumn79 = new System.Data.DataColumn();
            this.dataColumn80 = new System.Data.DataColumn();
            this.dataColumn81 = new System.Data.DataColumn();
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
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
            this.dataColumn29 = new System.Data.DataColumn();
            this.dataColumn30 = new System.Data.DataColumn();
            this.dataColumn31 = new System.Data.DataColumn();
            this.dataColumn32 = new System.Data.DataColumn();
            this.ultraDockManager1 = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._Finishing_HotRolledCoilInfoAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.dockableWindow2 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtUsedPrintTink = new System.Windows.Forms.TextBox();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.panel1_Fill_Panel = new System.Windows.Forms.Panel();
            this.txtUsedPrintPaper = new System.Windows.Forms.TextBox();
            this._panel1_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._panel1_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.VideoChannel1 = new System.Windows.Forms.PictureBox();
            this.panel15 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.VideoChannel2 = new System.Windows.Forms.PictureBox();
            this.picFDTP = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtXSZL = new LxControl.LxLedControl();
            this.lxLedControl1 = new LxControl.LxLedControl();
            this.btnWeightException = new System.Windows.Forms.Button();
            this.btnWeightComplete = new System.Windows.Forms.Button();
            this.StatusLight = new YGJZJL.Pipe.CoolIndicator();
            this.button16 = new System.Windows.Forms.Button();
            this.cbx_print = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnQL = new System.Windows.Forms.Button();
            this.lbWD = new System.Windows.Forms.Label();
            this.lbYS = new System.Windows.Forms.Label();
            this.txtYKL = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGH = new System.Windows.Forms.TextBox();
            this.button18 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDH = new System.Windows.Forms.TextBox();
            this.ds_plan = new System.Data.DataSet();
            this.dataTable4 = new System.Data.DataTable();
            this.dataColumn10 = new System.Data.DataColumn();
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
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn23 = new System.Data.DataColumn();
            this.dataColumn24 = new System.Data.DataColumn();
            this.dataColumn25 = new System.Data.DataColumn();
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
            this.dataColumn43 = new System.Data.DataColumn();
            this.dataSet2 = new System.Data.DataSet();
            this.dataTable3 = new System.Data.DataTable();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtThick = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.cbxRemark = new System.Windows.Forms.ComboBox();
            this.label36 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cbPrint = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tbRKDH = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtBandNo = new System.Windows.Forms.TextBox();
            this.cbx_PM = new System.Windows.Forms.ComboBox();
            this.txtJZ = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.txtQTYKL = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textMinWeight = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textMaxWeight = new System.Windows.Forms.TextBox();
            this.txtBZ = new System.Windows.Forms.TextBox();
            this.txtBC = new System.Windows.Forms.TextBox();
            this.tbx_theoryweight = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.tbx_lasttotalweight = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tbx_lastbandcount = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tbx_lastbatch = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cbx_Standard = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.tbx_hWeight = new System.Windows.Forms.TextBox();
            this.cbx_Hand = new System.Windows.Forms.CheckBox();
            this.tb_bandno_fb = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tb_zzbh_fb = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.btnChangStove = new System.Windows.Forms.Button();
            this.cbx_fb = new System.Windows.Forms.CheckBox();
            this.btnBC = new System.Windows.Forms.Button();
            this.btnWL = new System.Windows.Forms.Button();
            this.cbDCCD = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtZS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbLXIN = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDDH = new System.Windows.Forms.TextBox();
            this.cbGG = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cbGZ = new System.Windows.Forms.ComboBox();
            this.cbFHDW = new System.Windows.Forms.ComboBox();
            this.cbLX = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbWLMC = new System.Windows.Forms.ComboBox();
            this.cbSHDW = new System.Windows.Forms.ComboBox();
            this.txtJLY = new System.Windows.Forms.TextBox();
            this.txtZL = new System.Windows.Forms.TextBox();
            this.txtJLD = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtZZBH = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.printDocument3 = new System.Drawing.Printing.PrintDocument();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGrid3 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.coolIndicator1 = new YGJZJL.Pipe.CoolIndicator();
            this.VoiceC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).BeginInit();
            this.dockableWindow2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel14.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).BeginInit();
            this.panel15.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtXSZL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_plan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
            this.panel9.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).BeginInit();
            this.panel4.SuspendLayout();
            this.windowDockingArea1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VoiceC
            // 
            this.VoiceC.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.VoiceC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.VoiceC.Controls.Add(this.ultraGrid5);
            this.VoiceC.Dock = System.Windows.Forms.DockStyle.Right;
            this.VoiceC.Location = new System.Drawing.Point(0, 28);
            this.VoiceC.Name = "VoiceC";
            this.VoiceC.Size = new System.Drawing.Size(150, 627);
            this.VoiceC.TabIndex = 6;
            // 
            // ultraGrid5
            // 
            this.ultraGrid5.DataMember = "语音表";
            this.ultraGrid5.DataSource = this.dataSet1;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid5.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
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
            this.ultraGrid5.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid5.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid5.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGrid5.DisplayLayout.MaxRowScrollRegions = 1;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid5.DisplayLayout.Override.CardAreaAppearance = appearance2;
            this.ultraGrid5.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance3.BackColor = System.Drawing.Color.Silver;
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance3.ForeColor = System.Drawing.Color.Black;
            appearance3.TextHAlignAsString = "Center";
            appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid5.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.ultraGrid5.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.ultraGrid5.DisplayLayout.Override.RowAppearance = appearance4;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorAppearance = appearance5;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid5.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            appearance6.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance6.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid5.DisplayLayout.Override.SelectedRowAppearance = appearance6;
            this.ultraGrid5.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(167)))), ((int)(((byte)(191)))));
            this.ultraGrid5.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid5.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid5.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid5.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid5.Name = "ultraGrid5";
            this.ultraGrid5.Size = new System.Drawing.Size(146, 623);
            this.ultraGrid5.TabIndex = 0;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn26,
            this.dataColumn27,
            this.dataColumn28,
            this.dataColumn52,
            this.dataColumn63,
            this.dataColumn64,
            this.dataColumn65,
            this.dataColumn66,
            this.dataColumn67,
            this.dataColumn68,
            this.dataColumn69,
            this.dataColumn70,
            this.dataColumn72,
            this.dataColumn73,
            this.dataColumn74,
            this.dataColumn75,
            this.dataColumn76,
            this.dataColumn77,
            this.dataColumn78,
            this.dataColumn79,
            this.dataColumn80,
            this.dataColumn81,
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
            this.dataColumn1,
            this.dataColumn8,
            this.dataColumn9});
            this.dataTable1.TableName = "计量点基础表";
            // 
            // dataColumn26
            // 
            this.dataColumn26.Caption = "计量点编码";
            this.dataColumn26.ColumnName = "FS_POINTCODE";
            // 
            // dataColumn27
            // 
            this.dataColumn27.Caption = "计量点";
            this.dataColumn27.ColumnName = "FS_POINTNAME";
            // 
            // dataColumn28
            // 
            this.dataColumn28.Caption = "接管";
            this.dataColumn28.ColumnName = "XZ";
            // 
            // dataColumn52
            // 
            this.dataColumn52.Caption = "标志";
            this.dataColumn52.ColumnName = "FS_SIGN";
            // 
            // dataColumn63
            // 
            this.dataColumn63.Caption = "仪表类型";
            this.dataColumn63.ColumnName = "FS_METERTYPE";
            // 
            // dataColumn64
            // 
            this.dataColumn64.Caption = "仪表参数";
            this.dataColumn64.ColumnName = "FS_METERPARA";
            // 
            // dataColumn65
            // 
            this.dataColumn65.Caption = "IP";
            this.dataColumn65.ColumnName = "FS_MOXAIP";
            // 
            // dataColumn66
            // 
            this.dataColumn66.Caption = "端口";
            this.dataColumn66.ColumnName = "FS_MOXAPORT";
            // 
            // dataColumn67
            // 
            this.dataColumn67.Caption = "VIEDOIP";
            this.dataColumn67.ColumnName = "FS_VIEDOIP";
            // 
            // dataColumn68
            // 
            this.dataColumn68.Caption = "录像机端口";
            this.dataColumn68.ColumnName = "FS_VIEDOPORT";
            // 
            // dataColumn69
            // 
            this.dataColumn69.Caption = "用户名";
            this.dataColumn69.ColumnName = "FS_VIEDOUSER";
            // 
            // dataColumn70
            // 
            this.dataColumn70.Caption = "密码";
            this.dataColumn70.ColumnName = "FS_VIEDOPWD";
            // 
            // dataColumn72
            // 
            this.dataColumn72.Caption = "FS_POINTDEPART";
            this.dataColumn72.ColumnName = "FS_POINTDEPART";
            // 
            // dataColumn73
            // 
            this.dataColumn73.Caption = "FS_POINTTYPE";
            this.dataColumn73.ColumnName = "FS_POINTTYPE";
            // 
            // dataColumn74
            // 
            this.dataColumn74.Caption = "FS_RTUIP";
            this.dataColumn74.ColumnName = "FS_RTUIP";
            // 
            // dataColumn75
            // 
            this.dataColumn75.Caption = "FS_RTUPORT";
            this.dataColumn75.ColumnName = "FS_RTUPORT";
            // 
            // dataColumn76
            // 
            this.dataColumn76.Caption = "FS_PRINTERIP";
            this.dataColumn76.ColumnName = "FS_PRINTERIP";
            // 
            // dataColumn77
            // 
            this.dataColumn77.Caption = "FS_PRINTERNAME";
            this.dataColumn77.ColumnName = "FS_PRINTERNAME";
            // 
            // dataColumn78
            // 
            this.dataColumn78.Caption = "FS_PRINTTYPECODE";
            this.dataColumn78.ColumnName = "FS_PRINTTYPECODE";
            // 
            // dataColumn79
            // 
            this.dataColumn79.Caption = "FN_USEDPRINTPAPER";
            this.dataColumn79.ColumnName = "FN_USEDPRINTPAPER";
            // 
            // dataColumn80
            // 
            this.dataColumn80.Caption = "FN_USEDPRINTINK";
            this.dataColumn80.ColumnName = "FN_USEDPRINTINK";
            // 
            // dataColumn81
            // 
            this.dataColumn81.Caption = "FS_LEDIP";
            this.dataColumn81.ColumnName = "FS_LEDIP";
            // 
            // dataColumn82
            // 
            this.dataColumn82.Caption = "FS_LEDPORT";
            this.dataColumn82.ColumnName = "FS_LEDPORT";
            // 
            // dataColumn83
            // 
            this.dataColumn83.Caption = "FN_VALUE";
            this.dataColumn83.ColumnName = "FN_VALUE";
            // 
            // dataColumn84
            // 
            this.dataColumn84.Caption = "FS_ALLOWOTHERTARE";
            this.dataColumn84.ColumnName = "FS_ALLOWOTHERTARE";
            // 
            // dataColumn85
            // 
            this.dataColumn85.Caption = "FS_DISPLAYPORT";
            this.dataColumn85.ColumnName = "FS_DISPLAYPORT";
            // 
            // dataColumn86
            // 
            this.dataColumn86.Caption = "FS_DISPLAYPARA";
            this.dataColumn86.ColumnName = "FS_DISPLAYPARA";
            // 
            // dataColumn87
            // 
            this.dataColumn87.Caption = "FS_READERPORT";
            this.dataColumn87.ColumnName = "FS_READERPORT";
            // 
            // dataColumn88
            // 
            this.dataColumn88.Caption = "FS_READERPARA";
            this.dataColumn88.ColumnName = "FS_READERPARA";
            // 
            // dataColumn89
            // 
            this.dataColumn89.Caption = "FS_READERTYPE";
            this.dataColumn89.ColumnName = "FS_READERTYPE";
            // 
            // dataColumn90
            // 
            this.dataColumn90.Caption = "FS_DISPLAYTYPE";
            this.dataColumn90.ColumnName = "FS_DISPLAYTYPE";
            // 
            // dataColumn91
            // 
            this.dataColumn91.Caption = "FS_LEDTYPE";
            this.dataColumn91.ColumnName = "FS_LEDTYPE";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "FF_CLEARVALUE";
            // 
            // dataColumn8
            // 
            this.dataColumn8.ColumnName = "TOTALPAPAR";
            // 
            // dataColumn9
            // 
            this.dataColumn9.ColumnName = "TOTALINK";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn29,
            this.dataColumn30,
            this.dataColumn31,
            this.dataColumn32});
            this.dataTable2.TableName = "语音表";
            // 
            // dataColumn29
            // 
            this.dataColumn29.Caption = "声音名称";
            this.dataColumn29.ColumnName = "FS_VOICENAME";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "FS_INSTRTYPE";
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "FS_MEMO";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "FS_VOICEFILE";
            this.dataColumn32.DataType = typeof(byte[]);
            // 
            // ultraDockManager1
            // 
            this.ultraDockManager1.AnimationSpeed = Infragistics.Win.UltraWinDock.AnimationSpeed.StandardSpeedPlus2;
            this.ultraDockManager1.AutoHideDelay = 300;
            this.ultraDockManager1.CaptionStyle = Infragistics.Win.UltraWinDock.CaptionStyle.Office2003;
            dockableControlPane1.Closed = true;
            dockableControlPane1.Control = this.VoiceC;
            dockableControlPane1.FlyoutSize = new System.Drawing.Size(150, -1);
            dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(698, 0, 156, 666);
            dockableControlPane1.Size = new System.Drawing.Size(100, 100);
            dockableControlPane1.Text = "语音播报";
            dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane1});
            dockAreaPane1.Size = new System.Drawing.Size(95, 666);
            this.ultraDockManager1.DockAreas.AddRange(new Infragistics.Win.UltraWinDock.DockAreaPane[] {
            dockAreaPane1});
            this.ultraDockManager1.HostControl = this;
            this.ultraDockManager1.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.Office2003;
            // 
            // _Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft
            // 
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 0);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft.Name = "_Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft";
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft.Owner = this.ultraDockManager1;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 655);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft.TabIndex = 8;
            // 
            // _Finishing_HotRolledCoilInfoUnpinnedTabAreaRight
            // 
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight.Location = new System.Drawing.Point(1028, 0);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight.Name = "_Finishing_HotRolledCoilInfoUnpinnedTabAreaRight";
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight.Owner = this.ultraDockManager1;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 655);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight.TabIndex = 9;
            // 
            // _Finishing_HotRolledCoilInfoUnpinnedTabAreaTop
            // 
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 0);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop.Name = "_Finishing_HotRolledCoilInfoUnpinnedTabAreaTop";
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop.Owner = this.ultraDockManager1;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop.Size = new System.Drawing.Size(1028, 0);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop.TabIndex = 10;
            // 
            // _Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom
            // 
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 655);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom.Name = "_Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom";
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom.Owner = this.ultraDockManager1;
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom.Size = new System.Drawing.Size(1028, 0);
            this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom.TabIndex = 11;
            // 
            // _Finishing_HotRolledCoilInfoAutoHideControl
            // 
            this._Finishing_HotRolledCoilInfoAutoHideControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._Finishing_HotRolledCoilInfoAutoHideControl.Location = new System.Drawing.Point(852, 0);
            this._Finishing_HotRolledCoilInfoAutoHideControl.Name = "_Finishing_HotRolledCoilInfoAutoHideControl";
            this._Finishing_HotRolledCoilInfoAutoHideControl.Owner = this.ultraDockManager1;
            this._Finishing_HotRolledCoilInfoAutoHideControl.Size = new System.Drawing.Size(155, 655);
            this._Finishing_HotRolledCoilInfoAutoHideControl.TabIndex = 12;
            // 
            // dockableWindow2
            // 
            this.dockableWindow2.Controls.Add(this.VoiceC);
            this.dockableWindow2.Location = new System.Drawing.Point(5, 0);
            this.dockableWindow2.Name = "dockableWindow2";
            this.dockableWindow2.Owner = this.ultraDockManager1;
            this.dockableWindow2.Size = new System.Drawing.Size(150, 655);
            this.dockableWindow2.TabIndex = 608;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtUsedPrintTink);
            this.panel1.Controls.Add(this.chkAutoSave);
            this.panel1.Controls.Add(this.panel1_Fill_Panel);
            this.panel1.Controls.Add(this.txtUsedPrintPaper);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Left);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Right);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Top);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Bottom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 26);
            this.panel1.TabIndex = 15;
            // 
            // txtUsedPrintTink
            // 
            this.txtUsedPrintTink.Location = new System.Drawing.Point(581, 2);
            this.txtUsedPrintTink.Name = "txtUsedPrintTink";
            this.txtUsedPrintTink.ReadOnly = true;
            this.txtUsedPrintTink.Size = new System.Drawing.Size(39, 21);
            this.txtUsedPrintTink.TabIndex = 577;
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.Location = new System.Drawing.Point(12, 4);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(72, 16);
            this.chkAutoSave.TabIndex = 574;
            this.chkAutoSave.Text = "自动保存";
            this.chkAutoSave.UseVisualStyleBackColor = true;
            this.chkAutoSave.CheckStateChanged += new System.EventHandler(this.chkAutoSave_CheckStateChanged);
            // 
            // panel1_Fill_Panel
            // 
            this.panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1_Fill_Panel.Location = new System.Drawing.Point(0, 28);
            this.panel1_Fill_Panel.Name = "panel1_Fill_Panel";
            this.panel1_Fill_Panel.Size = new System.Drawing.Size(1028, 0);
            this.panel1_Fill_Panel.TabIndex = 0;
            // 
            // txtUsedPrintPaper
            // 
            this.txtUsedPrintPaper.Location = new System.Drawing.Point(422, 3);
            this.txtUsedPrintPaper.Name = "txtUsedPrintPaper";
            this.txtUsedPrintPaper.ReadOnly = true;
            this.txtUsedPrintPaper.Size = new System.Drawing.Size(38, 21);
            this.txtUsedPrintPaper.TabIndex = 576;
            // 
            // _panel1_Toolbars_Dock_Area_Left
            // 
            this._panel1_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this._panel1_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._panel1_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 28);
            this._panel1_Toolbars_Dock_Area_Left.Name = "_panel1_Toolbars_Dock_Area_Left";
            this._panel1_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 0);
            this._panel1_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this.panel1;
            this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            controlContainerTool2.ControlName = "chkAutoSave";
            controlContainerTool2.InstanceProps.IsFirstInGroup = true;
            buttonTool4.InstanceProps.IsFirstInGroup = true;
            buttonTool6.InstanceProps.IsFirstInGroup = true;
            controlContainerTool3.ControlName = "txtUsedPrintPaper";
            controlContainerTool3.InstanceProps.IsFirstInGroup = true;
            controlContainerTool3.InstanceProps.Width = 109;
            buttonTool13.InstanceProps.IsFirstInGroup = true;
            controlContainerTool5.ControlName = "txtUsedPrintTink";
            controlContainerTool5.InstanceProps.IsFirstInGroup = true;
            controlContainerTool5.InstanceProps.Width = 110;
            buttonTool15.InstanceProps.IsFirstInGroup = true;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1,
            controlContainerTool2,
            buttonTool4,
            buttonTool6,
            controlContainerTool3,
            buttonTool13,
            controlContainerTool5,
            buttonTool15,
            buttonTool7});
            ultraToolbar1.Text = "UltraToolbar1";
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            buttonTool2.SharedPropsInternal.Caption = "打开对讲";
            buttonTool2.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool2.SharedPropsInternal.Visible = false;
            controlContainerTool1.ControlName = "chkAutoSave";
            controlContainerTool1.SharedPropsInternal.Caption = "1";
            buttonTool3.SharedPropsInternal.Caption = "关闭LED显示";
            buttonTool3.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool3.SharedPropsInternal.Visible = false;
            buttonTool5.SharedPropsInternal.Caption = "查询预报";
            buttonTool5.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool5.SharedPropsInternal.Visible = false;
            buttonTool8.SharedPropsInternal.Caption = "换纸";
            buttonTool8.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool8.SharedPropsInternal.Visible = false;
            buttonTool10.SharedPropsInternal.Caption = "换碳带";
            buttonTool10.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool10.SharedPropsInternal.Visible = false;
            controlContainerTool4.ControlName = "txtUsedPrintPaper";
            controlContainerTool4.SharedPropsInternal.AllowMultipleInstances = true;
            controlContainerTool4.SharedPropsInternal.Caption = "剩余纸张数";
            controlContainerTool4.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            controlContainerTool4.SharedPropsInternal.Visible = false;
            controlContainerTool4.SharedPropsInternal.Width = 109;
            controlContainerTool6.ControlName = "txtUsedPrintTink";
            controlContainerTool6.SharedPropsInternal.AllowMultipleInstances = true;
            controlContainerTool6.SharedPropsInternal.Caption = "剩余碳带数";
            controlContainerTool6.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            controlContainerTool6.SharedPropsInternal.Visible = false;
            controlContainerTool6.SharedPropsInternal.Width = 110;
            buttonTool9.SharedPropsInternal.Caption = "校秤";
            buttonTool9.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool2,
            controlContainerTool1,
            buttonTool3,
            buttonTool5,
            buttonTool8,
            buttonTool10,
            controlContainerTool4,
            controlContainerTool6,
            buttonTool9});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
            // 
            // _panel1_Toolbars_Dock_Area_Right
            // 
            this._panel1_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this._panel1_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._panel1_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(1028, 28);
            this._panel1_Toolbars_Dock_Area_Right.Name = "_panel1_Toolbars_Dock_Area_Right";
            this._panel1_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 0);
            this._panel1_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _panel1_Toolbars_Dock_Area_Top
            // 
            this._panel1_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this._panel1_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._panel1_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._panel1_Toolbars_Dock_Area_Top.Name = "_panel1_Toolbars_Dock_Area_Top";
            this._panel1_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(1028, 28);
            this._panel1_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // _panel1_Toolbars_Dock_Area_Bottom
            // 
            this._panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this._panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 26);
            this._panel1_Toolbars_Dock_Area_Bottom.Name = "_panel1_Toolbars_Dock_Area_Bottom";
            this._panel1_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(1028, 0);
            this._panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(84, 629);
            this.panel2.TabIndex = 19;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel14, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel15, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(84, 629);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.groupBox3);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(3, 3);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(78, 308);
            this.panel14.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel11);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(78, 308);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.VideoChannel1);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 17);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(72, 288);
            this.panel11.TabIndex = 1;
            // 
            // VideoChannel1
            // 
            this.VideoChannel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel1.ErrorImage = null;
            this.VideoChannel1.InitialImage = null;
            this.VideoChannel1.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel1.Name = "VideoChannel1";
            this.VideoChannel1.Size = new System.Drawing.Size(72, 288);
            this.VideoChannel1.TabIndex = 0;
            this.VideoChannel1.TabStop = false;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.groupBox2);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel15.Location = new System.Drawing.Point(3, 317);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(78, 309);
            this.panel15.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel10);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(78, 309);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.VideoChannel2);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(3, 17);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(72, 289);
            this.panel10.TabIndex = 0;
            // 
            // VideoChannel2
            // 
            this.VideoChannel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel2.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel2.Name = "VideoChannel2";
            this.VideoChannel2.Size = new System.Drawing.Size(72, 289);
            this.VideoChannel2.TabIndex = 0;
            this.VideoChannel2.TabStop = false;
            // 
            // picFDTP
            // 
            this.picFDTP.Location = new System.Drawing.Point(180, 122);
            this.picFDTP.Name = "picFDTP";
            this.picFDTP.Size = new System.Drawing.Size(10, 10);
            this.picFDTP.TabIndex = 607;
            this.picFDTP.TabStop = false;
            this.picFDTP.MouseLeave += new System.EventHandler(this.picFDTP_MouseLeave);
            this.picFDTP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseMove);
            this.picFDTP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseDown);
            this.picFDTP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picFDTP_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel3.Controls.Add(this.checkBox2);
            this.panel3.Controls.Add(this.checkBox1);
            this.panel3.Controls.Add(this.txtXSZL);
            this.panel3.Controls.Add(this.lxLedControl1);
            this.panel3.Controls.Add(this.btnWeightException);
            this.panel3.Controls.Add(this.btnWeightComplete);
            this.panel3.Controls.Add(this.StatusLight);
            this.panel3.Controls.Add(this.button16);
            this.panel3.Controls.Add(this.cbx_print);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.btnQL);
            this.panel3.Controls.Add(this.lbWD);
            this.panel3.Controls.Add(this.lbYS);
            this.panel3.Controls.Add(this.txtYKL);
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.txtGH);
            this.panel3.Controls.Add(this.button18);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtDH);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(84, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(944, 110);
            this.panel3.TabIndex = 20;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(530, 89);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 679;
            this.checkBox2.Text = "实重";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(480, 89);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 678;
            this.checkBox1.Text = "理重";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // txtXSZL
            // 
            this.txtXSZL.BackColor = System.Drawing.Color.Transparent;
            this.txtXSZL.BackColor_1 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtXSZL.BackColor_2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtXSZL.BevelRate = 0.5F;
            this.txtXSZL.CornerRadius = 6;
            this.txtXSZL.FadedColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtXSZL.ForeColor = System.Drawing.Color.Green;
            this.txtXSZL.HighlightOpaque = ((byte)(50));
            this.txtXSZL.Location = new System.Drawing.Point(46, 27);
            this.txtXSZL.Name = "txtXSZL";
            this.txtXSZL.Size = new System.Drawing.Size(258, 71);
            this.txtXSZL.TabIndex = 677;
            this.txtXSZL.Text = "0.000";
            this.txtXSZL.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.txtXSZL.TotalCharCount = 7;
            // 
            // lxLedControl1
            // 
            this.lxLedControl1.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl1.BackColor_1 = System.Drawing.Color.Black;
            this.lxLedControl1.BackColor_2 = System.Drawing.Color.DimGray;
            this.lxLedControl1.BevelRate = 0.5F;
            this.lxLedControl1.FadedColor = System.Drawing.Color.DimGray;
            this.lxLedControl1.ForeColor = System.Drawing.Color.LightGreen;
            this.lxLedControl1.HighlightOpaque = ((byte)(50));
            this.lxLedControl1.Location = new System.Drawing.Point(152, 45);
            this.lxLedControl1.Name = "lxLedControl1";
            this.lxLedControl1.Size = new System.Drawing.Size(8, 8);
            this.lxLedControl1.TabIndex = 676;
            this.lxLedControl1.Text = "LXLEDCONTROL1";
            // 
            // btnWeightException
            // 
            this.btnWeightException.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnWeightException.Location = new System.Drawing.Point(618, 63);
            this.btnWeightException.Name = "btnWeightException";
            this.btnWeightException.Size = new System.Drawing.Size(64, 28);
            this.btnWeightException.TabIndex = 674;
            this.btnWeightException.Text = "计量异常";
            this.btnWeightException.UseVisualStyleBackColor = false;
            // 
            // btnWeightComplete
            // 
            this.btnWeightComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnWeightComplete.Location = new System.Drawing.Point(618, 29);
            this.btnWeightComplete.Name = "btnWeightComplete";
            this.btnWeightComplete.Size = new System.Drawing.Size(64, 28);
            this.btnWeightComplete.TabIndex = 673;
            this.btnWeightComplete.Text = "计量完成";
            this.btnWeightComplete.UseVisualStyleBackColor = false;
            // 
            // StatusLight
            // 
            this.StatusLight.Gradient = true;
            this.StatusLight.Location = new System.Drawing.Point(584, 42);
            this.StatusLight.Name = "StatusLight";
            this.StatusLight.Size = new System.Drawing.Size(32, 32);
            this.StatusLight.TabIndex = 672;
            this.StatusLight.Text = "coolIndicator2";
            // 
            // button16
            // 
            this.button16.BackColor = System.Drawing.Color.Blue;
            this.button16.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button16.Location = new System.Drawing.Point(319, 71);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(36, 36);
            this.button16.TabIndex = 670;
            this.button16.Text = "保存";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Visible = false;
            // 
            // cbx_print
            // 
            this.cbx_print.AutoSize = true;
            this.cbx_print.Checked = true;
            this.cbx_print.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_print.Location = new System.Drawing.Point(404, 90);
            this.cbx_print.Name = "cbx_print";
            this.cbx_print.Size = new System.Drawing.Size(72, 16);
            this.cbx_print.TabIndex = 670;
            this.cbx_print.Text = "打印标牌";
            this.cbx_print.UseVisualStyleBackColor = true;
            this.cbx_print.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(418, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(43, 21);
            this.textBox1.TabIndex = 572;
            this.textBox1.Visible = false;
            // 
            // btnQL
            // 
            this.btnQL.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnQL.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQL.Location = new System.Drawing.Point(396, 57);
            this.btnQL.Name = "btnQL";
            this.btnQL.Size = new System.Drawing.Size(93, 28);
            this.btnQL.TabIndex = 5;
            this.btnQL.Text = "清零";
            this.btnQL.UseVisualStyleBackColor = false;
            this.btnQL.Click += new System.EventHandler(this.btnQL_Click);
            // 
            // lbWD
            // 
            this.lbWD.AutoSize = true;
            this.lbWD.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWD.Location = new System.Drawing.Point(432, 29);
            this.lbWD.Name = "lbWD";
            this.lbWD.Size = new System.Drawing.Size(60, 24);
            this.lbWD.TabIndex = 4;
            this.lbWD.Text = "稳定";
            // 
            // lbYS
            // 
            this.lbYS.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbYS.ForeColor = System.Drawing.Color.Red;
            this.lbYS.Location = new System.Drawing.Point(393, 25);
            this.lbYS.Name = "lbYS";
            this.lbYS.Size = new System.Drawing.Size(37, 33);
            this.lbYS.TabIndex = 3;
            this.lbYS.Text = "●";
            this.lbYS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtYKL
            // 
            this.txtYKL.BackColor = System.Drawing.SystemColors.Window;
            this.txtYKL.Font = new System.Drawing.Font("宋体", 11F);
            this.txtYKL.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtYKL.Location = new System.Drawing.Point(205, 19);
            this.txtYKL.MaxLength = 8;
            this.txtYKL.Name = "txtYKL";
            this.txtYKL.Size = new System.Drawing.Size(10, 24);
            this.txtYKL.TabIndex = 19;
            this.txtYKL.Visible = false;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(379, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 16);
            this.label14.TabIndex = 661;
            this.label14.Text = "应扣量(Kg)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label14.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(308, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 56);
            this.label2.TabIndex = 2;
            this.label2.Text = "吨";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(154, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 24);
            this.label7.TabIndex = 632;
            this.label7.Text = "钩号";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Visible = false;
            // 
            // txtGH
            // 
            this.txtGH.BackColor = System.Drawing.SystemColors.Window;
            this.txtGH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtGH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGH.Location = new System.Drawing.Point(235, 0);
            this.txtGH.MaxLength = 8;
            this.txtGH.Name = "txtGH";
            this.txtGH.ReadOnly = true;
            this.txtGH.Size = new System.Drawing.Size(53, 24);
            this.txtGH.TabIndex = 4;
            this.txtGH.Visible = false;
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.Blue;
            this.button18.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button18.Location = new System.Drawing.Point(711, 63);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(65, 28);
            this.button18.TabIndex = 682;
            this.button18.Text = "换班";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(290, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 24);
            this.label3.TabIndex = 653;
            this.label3.Text = "吊号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // txtDH
            // 
            this.txtDH.BackColor = System.Drawing.SystemColors.Window;
            this.txtDH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtDH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDH.Location = new System.Drawing.Point(357, 0);
            this.txtDH.MaxLength = 8;
            this.txtDH.Name = "txtDH";
            this.txtDH.ReadOnly = true;
            this.txtDH.Size = new System.Drawing.Size(53, 24);
            this.txtDH.TabIndex = 5;
            this.txtDH.Visible = false;
            // 
            // ds_plan
            // 
            this.ds_plan.DataSetName = "NewDataSet";
            this.ds_plan.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable4});
            // 
            // dataTable4
            // 
            this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn10,
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
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn23,
            this.dataColumn24,
            this.dataColumn25,
            this.dataColumn33,
            this.dataColumn34,
            this.dataColumn35,
            this.dataColumn36,
            this.dataColumn37,
            this.dataColumn38,
            this.dataColumn39,
            this.dataColumn40,
            this.dataColumn41,
            this.dataColumn42,
            this.dataColumn43});
            this.dataTable4.TableName = "Table1";
            // 
            // dataColumn10
            // 
            this.dataColumn10.Caption = "轧制编号";
            this.dataColumn10.ColumnName = "FS_BATCHNO";
            // 
            // dataColumn11
            // 
            this.dataColumn11.ColumnName = "FS_PRODUCTNO";
            // 
            // dataColumn12
            // 
            this.dataColumn12.ColumnName = "FS_ItemNo";
            // 
            // dataColumn13
            // 
            this.dataColumn13.ColumnName = "FS_TechCardNo";
            // 
            // dataColumn14
            // 
            this.dataColumn14.Caption = "坯重";
            this.dataColumn14.ColumnName = "FN_BILLETWEIGHT";
            // 
            // dataColumn15
            // 
            this.dataColumn15.Caption = "炉号";
            this.dataColumn15.ColumnName = "FS_STOVENO";
            // 
            // dataColumn16
            // 
            this.dataColumn16.ColumnName = "FN_GP_TOTALCOUNT";
            // 
            // dataColumn17
            // 
            this.dataColumn17.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn18
            // 
            this.dataColumn18.ColumnName = "SENDER";
            // 
            // dataColumn19
            // 
            this.dataColumn19.Caption = "钢号";
            this.dataColumn19.ColumnName = "FS_STEELTYPE";
            // 
            // dataColumn20
            // 
            this.dataColumn20.Caption = "规格";
            this.dataColumn20.ColumnName = "FS_SPEC";
            // 
            // dataColumn21
            // 
            this.dataColumn21.ColumnName = "RECEIVER";
            // 
            // dataColumn22
            // 
            this.dataColumn22.Caption = "长度";
            this.dataColumn22.ColumnName = "FN_LENGTH";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "FS_FLOW";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "FS_PLANPERSON";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "FD_TIME";
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "FN_JJ_WEIGHT";
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "FN_SINGLENUM";
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "FN_SINGLEWEIGHT";
            // 
            // dataColumn36
            // 
            this.dataColumn36.Caption = "预报吊数";
            this.dataColumn36.ColumnName = "FN_BANDCOUNT";
            // 
            // dataColumn37
            // 
            this.dataColumn37.ColumnName = "FS_PRINTTYPE";
            // 
            // dataColumn38
            // 
            this.dataColumn38.ColumnName = "fs_standardcheck";
            // 
            // dataColumn39
            // 
            this.dataColumn39.ColumnName = "FS_STEELTYPECHECK";
            // 
            // dataColumn40
            // 
            this.dataColumn40.ColumnName = "FS_ADDRESSCHECK";
            // 
            // dataColumn41
            // 
            this.dataColumn41.ColumnName = "fs_printweighttype";
            // 
            // dataColumn42
            // 
            this.dataColumn42.ColumnName = "FS_TWINSTYPE";
            // 
            // dataColumn43
            // 
            this.dataColumn43.ColumnName = "FS_FCLFLAG";
            // 
            // dataSet2
            // 
            this.dataSet2.DataSetName = "NewDataSet";
            this.dataSet2.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable3});
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7});
            this.dataTable3.TableName = "钩号匹配信息";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "钩号";
            this.dataColumn2.ColumnName = "FN_HOOKNO";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "轧制批号";
            this.dataColumn3.ColumnName = "FS_BATCHNO";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "吊号";
            this.dataColumn4.ColumnName = "FN_BANDNO";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "钩状态";
            this.dataColumn5.ColumnName = "FS_LOSTFLAG";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "是否复磅";
            this.dataColumn6.ColumnName = "FS_REDOFLAG";
            // 
            // dataColumn7
            // 
            this.dataColumn7.Caption = "序号";
            this.dataColumn7.ColumnName = "FN_NUMBER";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(84, 136);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(944, 334);
            this.panel6.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBox7);
            this.panel5.Controls.Add(this.groupBox4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(944, 334);
            this.panel5.TabIndex = 23;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.groupBox7.Controls.Add(this.panel13);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox7.Location = new System.Drawing.Point(908, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(36, 334);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "计量点接管信息";
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel13.Controls.Add(this.ultraGrid2);
            this.panel13.Controls.Add(this.panel9);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 17);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(30, 314);
            this.panel13.TabIndex = 0;
            // 
            // ultraGrid2
            // 
            this.ultraGrid2.DataMember = "计量点基础表";
            this.ultraGrid2.DataSource = this.dataSet1;
            appearance13.BackColor = System.Drawing.Color.White;
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid2.DisplayLayout.Appearance = appearance13;
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 124;
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 153;
            ultraGridColumn7.AutoSizeEdit = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn7.Header.VisiblePosition = 0;
            ultraGridColumn7.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn7.Width = 45;
            ultraGridColumn8.Header.VisiblePosition = 3;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 4;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 5;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 6;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 7;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 8;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.VisiblePosition = 9;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Header.VisiblePosition = 10;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 11;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.VisiblePosition = 12;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.VisiblePosition = 13;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.VisiblePosition = 14;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.Header.VisiblePosition = 15;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn21.Header.VisiblePosition = 16;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.Header.VisiblePosition = 17;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.Header.VisiblePosition = 18;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 19;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.VisiblePosition = 20;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Header.VisiblePosition = 21;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn27.Header.VisiblePosition = 22;
            ultraGridColumn27.Hidden = true;
            ultraGridColumn28.Header.VisiblePosition = 23;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.VisiblePosition = 24;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.Header.VisiblePosition = 25;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 26;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 27;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 28;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.Header.VisiblePosition = 29;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.Header.VisiblePosition = 30;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.Header.VisiblePosition = 31;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 32;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.VisiblePosition = 33;
            ultraGridColumn38.Hidden = true;
            ultraGridColumn39.Header.VisiblePosition = 34;
            ultraGridColumn39.Hidden = true;
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
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39});
            this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraGrid2.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid2.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid2.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid2.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance14.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid2.DisplayLayout.Override.CardAreaAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance15.BackColor2 = System.Drawing.Color.White;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance15.FontData.SizeInPoints = 11F;
            appearance15.FontData.UnderlineAsString = "False";
            appearance15.ForeColor = System.Drawing.Color.Black;
            appearance15.TextHAlignAsString = "Center";
            appearance15.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid2.DisplayLayout.Override.HeaderAppearance = appearance15;
            appearance16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.Override.RowAppearance = appearance16;
            appearance17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance17.BackColor2 = System.Drawing.Color.White;
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorAppearance = appearance17;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid2.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance18.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance18.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid2.DisplayLayout.Override.SelectedRowAppearance = appearance18;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid2.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ultraGrid2.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid2.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid2.Name = "ultraGrid2";
            this.ultraGrid2.Size = new System.Drawing.Size(26, 270);
            this.ultraGrid2.TabIndex = 2;
            this.ultraGrid2.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.ultraGrid2_DoubleClickRow);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel9.Controls.Add(this.btnOpen);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 270);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(26, 40);
            this.panel9.TabIndex = 2;
            // 
            // btnOpen
            // 
            this.btnOpen.BackColor = System.Drawing.Color.Blue;
            this.btnOpen.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnOpen.Location = new System.Drawing.Point(6, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(72, 32);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "打开设备";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel7);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(944, 334);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "称重信息";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.txtThick);
            this.panel7.Controls.Add(this.label37);
            this.panel7.Controls.Add(this.cbxRemark);
            this.panel7.Controls.Add(this.label36);
            this.panel7.Controls.Add(this.button1);
            this.panel7.Controls.Add(this.cbPrint);
            this.panel7.Controls.Add(this.label35);
            this.panel7.Controls.Add(this.tbRKDH);
            this.panel7.Controls.Add(this.label34);
            this.panel7.Controls.Add(this.label33);
            this.panel7.Controls.Add(this.txtBandNo);
            this.panel7.Controls.Add(this.cbx_PM);
            this.panel7.Controls.Add(this.txtJZ);
            this.panel7.Controls.Add(this.label32);
            this.panel7.Controls.Add(this.txtQTYKL);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Controls.Add(this.label31);
            this.panel7.Controls.Add(this.textMinWeight);
            this.panel7.Controls.Add(this.label30);
            this.panel7.Controls.Add(this.textMaxWeight);
            this.panel7.Controls.Add(this.txtBZ);
            this.panel7.Controls.Add(this.txtBC);
            this.panel7.Controls.Add(this.tbx_theoryweight);
            this.panel7.Controls.Add(this.label28);
            this.panel7.Controls.Add(this.tbx_lasttotalweight);
            this.panel7.Controls.Add(this.label27);
            this.panel7.Controls.Add(this.tbx_lastbandcount);
            this.panel7.Controls.Add(this.label26);
            this.panel7.Controls.Add(this.tbx_lastbatch);
            this.panel7.Controls.Add(this.label25);
            this.panel7.Controls.Add(this.cbx_Standard);
            this.panel7.Controls.Add(this.label23);
            this.panel7.Controls.Add(this.label15);
            this.panel7.Controls.Add(this.panel12);
            this.panel7.Controls.Add(this.cbDCCD);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.txtZS);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.cbLXIN);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.txtDDH);
            this.panel7.Controls.Add(this.cbGG);
            this.panel7.Controls.Add(this.label18);
            this.panel7.Controls.Add(this.cbGZ);
            this.panel7.Controls.Add(this.cbFHDW);
            this.panel7.Controls.Add(this.cbLX);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.cbWLMC);
            this.panel7.Controls.Add(this.cbSHDW);
            this.panel7.Controls.Add(this.txtJLY);
            this.panel7.Controls.Add(this.txtZL);
            this.panel7.Controls.Add(this.txtJLD);
            this.panel7.Controls.Add(this.label21);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.label19);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.label13);
            this.panel7.Controls.Add(this.label10);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.txtZZBH);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 17);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(938, 314);
            this.panel7.TabIndex = 0;
            // 
            // txtThick
            // 
            this.txtThick.BackColor = System.Drawing.SystemColors.Window;
            this.txtThick.Font = new System.Drawing.Font("宋体", 14F);
            this.txtThick.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtThick.Location = new System.Drawing.Point(885, 110);
            this.txtThick.MaxLength = 8;
            this.txtThick.Name = "txtThick";
            this.txtThick.Size = new System.Drawing.Size(80, 29);
            this.txtThick.TabIndex = 700;
            this.txtThick.TextChanged += new System.EventHandler(this.txtThick_TextChanged);
            this.txtThick.Leave += new System.EventHandler(this.txtThick_Leave);
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label37.Location = new System.Drawing.Point(817, 110);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(80, 32);
            this.label37.TabIndex = 701;
            this.label37.Text = "厚度";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxRemark
            // 
            this.cbxRemark.Font = new System.Drawing.Font("宋体", 14F);
            this.cbxRemark.FormattingEnabled = true;
            this.cbxRemark.Items.AddRange(new object[] {
            "接头",
            "补焊"});
            this.cbxRemark.Location = new System.Drawing.Point(792, 145);
            this.cbxRemark.Name = "cbxRemark";
            this.cbxRemark.Size = new System.Drawing.Size(100, 27);
            this.cbxRemark.TabIndex = 698;
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label36.Location = new System.Drawing.Point(737, 143);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(65, 32);
            this.label36.TabIndex = 699;
            this.label36.Text = "备注";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Blue;
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button1.Location = new System.Drawing.Point(623, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 32);
            this.button1.TabIndex = 691;
            this.button1.Text = "打印";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbPrint
            // 
            this.cbPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrint.Font = new System.Drawing.Font("宋体", 14F);
            this.cbPrint.FormattingEnabled = true;
            this.cbPrint.Location = new System.Drawing.Point(623, 3);
            this.cbPrint.Name = "cbPrint";
            this.cbPrint.Size = new System.Drawing.Size(350, 27);
            this.cbPrint.TabIndex = 697;
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label35.Location = new System.Drawing.Point(543, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(80, 32);
            this.label35.TabIndex = 696;
            this.label35.Text = "打印类型";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbRKDH
            // 
            this.tbRKDH.BackColor = System.Drawing.SystemColors.Window;
            this.tbRKDH.Font = new System.Drawing.Font("宋体", 14F);
            this.tbRKDH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbRKDH.Location = new System.Drawing.Point(89, 3);
            this.tbRKDH.MaxLength = 12;
            this.tbRKDH.Name = "tbRKDH";
            this.tbRKDH.Size = new System.Drawing.Size(175, 29);
            this.tbRKDH.TabIndex = 694;
            this.tbRKDH.TextChanged += new System.EventHandler(this.tbRKDH_TextChanged);
            // 
            // label34
            // 
            this.label34.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label34.Location = new System.Drawing.Point(6, 3);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(80, 32);
            this.label34.TabIndex = 695;
            this.label34.Text = "入库单号";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label33.Location = new System.Drawing.Point(181, 43);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(48, 24);
            this.label33.TabIndex = 693;
            this.label33.Text = "吊号";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBandNo
            // 
            this.txtBandNo.BackColor = System.Drawing.SystemColors.Window;
            this.txtBandNo.Font = new System.Drawing.Font("宋体", 14F);
            this.txtBandNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBandNo.Location = new System.Drawing.Point(232, 41);
            this.txtBandNo.MaxLength = 8;
            this.txtBandNo.Name = "txtBandNo";
            this.txtBandNo.Size = new System.Drawing.Size(34, 29);
            this.txtBandNo.TabIndex = 692;
            // 
            // cbx_PM
            // 
            this.cbx_PM.Font = new System.Drawing.Font("宋体", 14F);
            this.cbx_PM.FormattingEnabled = true;
            this.cbx_PM.Location = new System.Drawing.Point(625, 41);
            this.cbx_PM.Name = "cbx_PM";
            this.cbx_PM.Size = new System.Drawing.Size(350, 27);
            this.cbx_PM.TabIndex = 691;
            // 
            // txtJZ
            // 
            this.txtJZ.BackColor = System.Drawing.Color.Bisque;
            this.txtJZ.Enabled = false;
            this.txtJZ.Font = new System.Drawing.Font("宋体", 14F);
            this.txtJZ.ForeColor = System.Drawing.Color.Red;
            this.txtJZ.Location = new System.Drawing.Point(806, 184);
            this.txtJZ.MaxLength = 8;
            this.txtJZ.Name = "txtJZ";
            this.txtJZ.ReadOnly = true;
            this.txtJZ.Size = new System.Drawing.Size(75, 29);
            this.txtJZ.TabIndex = 689;
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label32.Location = new System.Drawing.Point(723, 183);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(80, 32);
            this.label32.TabIndex = 688;
            this.label32.Text = "净重(t)";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtQTYKL
            // 
            this.txtQTYKL.BackColor = System.Drawing.SystemColors.Window;
            this.txtQTYKL.Font = new System.Drawing.Font("宋体", 14F);
            this.txtQTYKL.ForeColor = System.Drawing.Color.Red;
            this.txtQTYKL.Location = new System.Drawing.Point(646, 146);
            this.txtQTYKL.MaxLength = 8;
            this.txtQTYKL.Name = "txtQTYKL";
            this.txtQTYKL.Size = new System.Drawing.Size(80, 29);
            this.txtQTYKL.TabIndex = 687;
            this.txtQTYKL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQTYKL_KeyUp);
            this.txtQTYKL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQTYKL_KeyPress);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(540, 146);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(101, 32);
            this.label24.TabIndex = 686;
            this.label24.Text = "应扣量(kg)";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(155, 300);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(71, 24);
            this.label31.TabIndex = 685;
            this.label31.Text = "重量下限：";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textMinWeight
            // 
            this.textMinWeight.BackColor = System.Drawing.SystemColors.Window;
            this.textMinWeight.Enabled = false;
            this.textMinWeight.Font = new System.Drawing.Font("宋体", 11F);
            this.textMinWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textMinWeight.Location = new System.Drawing.Point(228, 300);
            this.textMinWeight.MaxLength = 8;
            this.textMinWeight.Name = "textMinWeight";
            this.textMinWeight.ReadOnly = true;
            this.textMinWeight.Size = new System.Drawing.Size(59, 24);
            this.textMinWeight.TabIndex = 684;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(15, 302);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 24);
            this.label30.TabIndex = 681;
            this.label30.Text = "重量上限：";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textMaxWeight
            // 
            this.textMaxWeight.BackColor = System.Drawing.SystemColors.Window;
            this.textMaxWeight.Enabled = false;
            this.textMaxWeight.Font = new System.Drawing.Font("宋体", 11F);
            this.textMaxWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textMaxWeight.Location = new System.Drawing.Point(88, 300);
            this.textMaxWeight.MaxLength = 8;
            this.textMaxWeight.Name = "textMaxWeight";
            this.textMaxWeight.ReadOnly = true;
            this.textMaxWeight.Size = new System.Drawing.Size(56, 24);
            this.textMaxWeight.TabIndex = 680;
            // 
            // txtBZ
            // 
            this.txtBZ.BackColor = System.Drawing.Color.Bisque;
            this.txtBZ.Enabled = false;
            this.txtBZ.Font = new System.Drawing.Font("宋体", 14F);
            this.txtBZ.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBZ.Location = new System.Drawing.Point(477, 183);
            this.txtBZ.MaxLength = 8;
            this.txtBZ.Name = "txtBZ";
            this.txtBZ.ReadOnly = true;
            this.txtBZ.Size = new System.Drawing.Size(45, 29);
            this.txtBZ.TabIndex = 683;
            // 
            // txtBC
            // 
            this.txtBC.BackColor = System.Drawing.Color.Bisque;
            this.txtBC.Enabled = false;
            this.txtBC.Font = new System.Drawing.Font("宋体", 14F);
            this.txtBC.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBC.Location = new System.Drawing.Point(426, 183);
            this.txtBC.MaxLength = 8;
            this.txtBC.Name = "txtBC";
            this.txtBC.ReadOnly = true;
            this.txtBC.Size = new System.Drawing.Size(45, 29);
            this.txtBC.TabIndex = 17;
            // 
            // tbx_theoryweight
            // 
            this.tbx_theoryweight.BackColor = System.Drawing.SystemColors.Window;
            this.tbx_theoryweight.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_theoryweight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_theoryweight.Location = new System.Drawing.Point(479, 270);
            this.tbx_theoryweight.MaxLength = 12;
            this.tbx_theoryweight.Name = "tbx_theoryweight";
            this.tbx_theoryweight.Size = new System.Drawing.Size(74, 24);
            this.tbx_theoryweight.TabIndex = 678;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(435, 270);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(45, 24);
            this.label28.TabIndex = 679;
            this.label28.Text = "理重：";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_lasttotalweight
            // 
            this.tbx_lasttotalweight.BackColor = System.Drawing.SystemColors.Window;
            this.tbx_lasttotalweight.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_lasttotalweight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_lasttotalweight.Location = new System.Drawing.Point(348, 270);
            this.tbx_lasttotalweight.MaxLength = 12;
            this.tbx_lasttotalweight.Name = "tbx_lasttotalweight";
            this.tbx_lasttotalweight.Size = new System.Drawing.Size(74, 24);
            this.tbx_lasttotalweight.TabIndex = 676;
            // 
            // label27
            // 
            this.label27.Location = new System.Drawing.Point(303, 270);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(45, 24);
            this.label27.TabIndex = 677;
            this.label27.Text = "实重：";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_lastbandcount
            // 
            this.tbx_lastbandcount.BackColor = System.Drawing.SystemColors.Window;
            this.tbx_lastbandcount.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_lastbandcount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_lastbandcount.Location = new System.Drawing.Point(227, 270);
            this.tbx_lastbandcount.MaxLength = 12;
            this.tbx_lastbandcount.Name = "tbx_lastbandcount";
            this.tbx_lastbandcount.Size = new System.Drawing.Size(60, 24);
            this.tbx_lastbandcount.TabIndex = 674;
            // 
            // label26
            // 
            this.label26.Location = new System.Drawing.Point(183, 270);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(45, 24);
            this.label26.TabIndex = 675;
            this.label26.Text = "吊数：";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbx_lastbatch
            // 
            this.tbx_lastbatch.BackColor = System.Drawing.SystemColors.Window;
            this.tbx_lastbatch.Font = new System.Drawing.Font("宋体", 11F);
            this.tbx_lastbatch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_lastbatch.Location = new System.Drawing.Point(88, 270);
            this.tbx_lastbatch.MaxLength = 12;
            this.tbx_lastbatch.Name = "tbx_lastbatch";
            this.tbx_lastbatch.Size = new System.Drawing.Size(95, 24);
            this.tbx_lastbatch.TabIndex = 672;
            // 
            // label25
            // 
            this.label25.Location = new System.Drawing.Point(0, 270);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(90, 24);
            this.label25.TabIndex = 673;
            this.label25.Text = "上一批次号：";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbx_Standard
            // 
            this.cbx_Standard.Font = new System.Drawing.Font("宋体", 14F);
            this.cbx_Standard.FormattingEnabled = true;
            this.cbx_Standard.Location = new System.Drawing.Point(352, 41);
            this.cbx_Standard.Name = "cbx_Standard";
            this.cbx_Standard.Size = new System.Drawing.Size(176, 27);
            this.cbx_Standard.TabIndex = 669;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label23.Location = new System.Drawing.Point(360, 181);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(80, 32);
            this.label23.TabIndex = 666;
            this.label23.Text = "班次";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(272, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 32);
            this.label15.TabIndex = 665;
            this.label15.Text = "标准";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.label29);
            this.panel12.Controls.Add(this.tbx_hWeight);
            this.panel12.Controls.Add(this.cbx_Hand);
            this.panel12.Controls.Add(this.tb_bandno_fb);
            this.panel12.Controls.Add(this.label22);
            this.panel12.Controls.Add(this.tb_zzbh_fb);
            this.panel12.Controls.Add(this.label20);
            this.panel12.Controls.Add(this.btnChangStove);
            this.panel12.Controls.Add(this.cbx_fb);
            this.panel12.Controls.Add(this.btnBC);
            this.panel12.Controls.Add(this.btnWL);
            this.panel12.Location = new System.Drawing.Point(0, 223);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(828, 47);
            this.panel12.TabIndex = 662;
            this.panel12.Paint += new System.Windows.Forms.PaintEventHandler(this.panel12_Paint);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label29.Location = new System.Drawing.Point(549, 13);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(25, 16);
            this.label29.TabIndex = 681;
            this.label29.Text = "吨";
            // 
            // tbx_hWeight
            // 
            this.tbx_hWeight.BackColor = System.Drawing.SystemColors.Window;
            this.tbx_hWeight.Enabled = false;
            this.tbx_hWeight.Font = new System.Drawing.Font("宋体", 14F);
            this.tbx_hWeight.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbx_hWeight.Location = new System.Drawing.Point(475, 7);
            this.tbx_hWeight.MaxLength = 8;
            this.tbx_hWeight.Name = "tbx_hWeight";
            this.tbx_hWeight.Size = new System.Drawing.Size(72, 29);
            this.tbx_hWeight.TabIndex = 680;
            // 
            // cbx_Hand
            // 
            this.cbx_Hand.AutoSize = true;
            this.cbx_Hand.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cbx_Hand.Location = new System.Drawing.Point(399, 13);
            this.cbx_Hand.Name = "cbx_Hand";
            this.cbx_Hand.Size = new System.Drawing.Size(61, 20);
            this.cbx_Hand.TabIndex = 670;
            this.cbx_Hand.Text = "手工";
            this.cbx_Hand.UseVisualStyleBackColor = true;
            this.cbx_Hand.CheckStateChanged += new System.EventHandler(this.cbx_Hand_CheckStateChanged);
            this.cbx_Hand.CheckedChanged += new System.EventHandler(this.cbx_Hand_CheckedChanged);
            // 
            // tb_bandno_fb
            // 
            this.tb_bandno_fb.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_bandno_fb.Location = new System.Drawing.Point(315, 9);
            this.tb_bandno_fb.Name = "tb_bandno_fb";
            this.tb_bandno_fb.Size = new System.Drawing.Size(48, 26);
            this.tb_bandno_fb.TabIndex = 669;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(273, 12);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(42, 16);
            this.label22.TabIndex = 668;
            this.label22.Text = "吊号";
            // 
            // tb_zzbh_fb
            // 
            this.tb_zzbh_fb.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_zzbh_fb.Location = new System.Drawing.Point(144, 9);
            this.tb_zzbh_fb.Name = "tb_zzbh_fb";
            this.tb_zzbh_fb.Size = new System.Drawing.Size(111, 26);
            this.tb_zzbh_fb.TabIndex = 667;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(71, 14);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 16);
            this.label20.TabIndex = 666;
            this.label20.Text = "轧制编号";
            // 
            // btnChangStove
            // 
            this.btnChangStove.BackColor = System.Drawing.Color.Blue;
            this.btnChangStove.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnChangStove.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnChangStove.Location = new System.Drawing.Point(738, 5);
            this.btnChangStove.Name = "btnChangStove";
            this.btnChangStove.Size = new System.Drawing.Size(75, 32);
            this.btnChangStove.TabIndex = 690;
            this.btnChangStove.Text = "换炉";
            this.btnChangStove.UseVisualStyleBackColor = false;
            this.btnChangStove.Click += new System.EventHandler(this.btnChangStove_Click);
            // 
            // cbx_fb
            // 
            this.cbx_fb.AutoSize = true;
            this.cbx_fb.Enabled = false;
            this.cbx_fb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cbx_fb.Location = new System.Drawing.Point(8, 11);
            this.cbx_fb.Name = "cbx_fb";
            this.cbx_fb.Size = new System.Drawing.Size(61, 20);
            this.cbx_fb.TabIndex = 665;
            this.cbx_fb.Text = "复磅";
            this.cbx_fb.UseVisualStyleBackColor = true;
            this.cbx_fb.CheckStateChanged += new System.EventHandler(this.cbx_fb_CheckStateChanged);
            // 
            // btnBC
            // 
            this.btnBC.BackColor = System.Drawing.Color.Blue;
            this.btnBC.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.btnBC.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnBC.Location = new System.Drawing.Point(623, 4);
            this.btnBC.Name = "btnBC";
            this.btnBC.Size = new System.Drawing.Size(75, 32);
            this.btnBC.TabIndex = 1;
            this.btnBC.Text = "保存";
            this.btnBC.UseVisualStyleBackColor = false;
            this.btnBC.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // btnWL
            // 
            this.btnWL.BackColor = System.Drawing.Color.Blue;
            this.btnWL.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnWL.Location = new System.Drawing.Point(623, 5);
            this.btnWL.Name = "btnWL";
            this.btnWL.Size = new System.Drawing.Size(75, 32);
            this.btnWL.TabIndex = 2;
            this.btnWL.Text = "完炉";
            this.btnWL.UseVisualStyleBackColor = false;
            this.btnWL.Visible = false;
            this.btnWL.Click += new System.EventHandler(this.btnWL_Click);
            // 
            // cbDCCD
            // 
            this.cbDCCD.Font = new System.Drawing.Font("宋体", 14F);
            this.cbDCCD.FormattingEnabled = true;
            this.cbDCCD.Location = new System.Drawing.Point(440, 148);
            this.cbDCCD.Name = "cbDCCD";
            this.cbDCCD.Size = new System.Drawing.Size(93, 27);
            this.cbDCCD.TabIndex = 14;
            this.cbDCCD.Leave += new System.EventHandler(this.cbDCCD_Leave);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(363, 148);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 32);
            this.label12.TabIndex = 658;
            this.label12.Text = "定尺长度";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtZS
            // 
            this.txtZS.BackColor = System.Drawing.SystemColors.Window;
            this.txtZS.Font = new System.Drawing.Font("宋体", 14F);
            this.txtZS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtZS.Location = new System.Drawing.Point(268, 149);
            this.txtZS.MaxLength = 8;
            this.txtZS.Name = "txtZS";
            this.txtZS.Size = new System.Drawing.Size(80, 29);
            this.txtZS.TabIndex = 13;
            this.txtZS.Leave += new System.EventHandler(this.txtZS_Leave);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(200, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 32);
            this.label4.TabIndex = 657;
            this.label4.Text = "支数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbLXIN
            // 
            this.cbLXIN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLXIN.Font = new System.Drawing.Font("宋体", 14F);
            this.cbLXIN.FormattingEnabled = true;
            this.cbLXIN.Items.AddRange(new object[] {
            "定尺",
            "非定尺",
            "客户定尺"});
            this.cbLXIN.Location = new System.Drawing.Point(88, 150);
            this.cbLXIN.Name = "cbLXIN";
            this.cbLXIN.Size = new System.Drawing.Size(112, 27);
            this.cbLXIN.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 32);
            this.label1.TabIndex = 654;
            this.label1.Text = "类型";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDDH
            // 
            this.txtDDH.BackColor = System.Drawing.SystemColors.Window;
            this.txtDDH.Font = new System.Drawing.Font("宋体", 14F);
            this.txtDDH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDDH.Location = new System.Drawing.Point(351, 0);
            this.txtDDH.MaxLength = 12;
            this.txtDDH.Name = "txtDDH";
            this.txtDDH.Size = new System.Drawing.Size(175, 29);
            this.txtDDH.TabIndex = 1;
            this.txtDDH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDDH_KeyPress);
            // 
            // cbGG
            // 
            this.cbGG.Font = new System.Drawing.Font("宋体", 14F);
            this.cbGG.FormattingEnabled = true;
            this.cbGG.Location = new System.Drawing.Point(353, 113);
            this.cbGG.Name = "cbGG";
            this.cbGG.Size = new System.Drawing.Size(175, 27);
            this.cbGG.TabIndex = 11;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(272, 111);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 32);
            this.label18.TabIndex = 646;
            this.label18.Text = "规格";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbGZ
            // 
            this.cbGZ.Font = new System.Drawing.Font("宋体", 14F);
            this.cbGZ.FormattingEnabled = true;
            this.cbGZ.Location = new System.Drawing.Point(89, 113);
            this.cbGZ.Name = "cbGZ";
            this.cbGZ.Size = new System.Drawing.Size(175, 27);
            this.cbGZ.TabIndex = 10;
            // 
            // cbFHDW
            // 
            this.cbFHDW.Font = new System.Drawing.Font("宋体", 14F);
            this.cbFHDW.FormattingEnabled = true;
            this.cbFHDW.Location = new System.Drawing.Point(627, 77);
            this.cbFHDW.Name = "cbFHDW";
            this.cbFHDW.Size = new System.Drawing.Size(175, 27);
            this.cbFHDW.TabIndex = 8;
            this.cbFHDW.Leave += new System.EventHandler(this.cbWLMC_Leave);
            this.cbFHDW.TextChanged += new System.EventHandler(this.cbFHDW_TextChanged);
            // 
            // cbLX
            // 
            this.cbLX.Font = new System.Drawing.Font("宋体", 14F);
            this.cbLX.FormattingEnabled = true;
            this.cbLX.Location = new System.Drawing.Point(628, 111);
            this.cbLX.Name = "cbLX";
            this.cbLX.Size = new System.Drawing.Size(175, 27);
            this.cbLX.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(6, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 32);
            this.label9.TabIndex = 634;
            this.label9.Text = "钢种";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(544, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 32);
            this.label6.TabIndex = 630;
            this.label6.Text = "发货单位";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(276, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 32);
            this.label5.TabIndex = 628;
            this.label5.Text = "订单号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbWLMC
            // 
            this.cbWLMC.Font = new System.Drawing.Font("宋体", 14F);
            this.cbWLMC.FormattingEnabled = true;
            this.cbWLMC.ItemHeight = 19;
            this.cbWLMC.Location = new System.Drawing.Point(90, 78);
            this.cbWLMC.Name = "cbWLMC";
            this.cbWLMC.Size = new System.Drawing.Size(175, 27);
            this.cbWLMC.TabIndex = 6;
            this.cbWLMC.Leave += new System.EventHandler(this.cbWLMC_Leave);
            this.cbWLMC.TextChanged += new System.EventHandler(this.cbWLMC_TextChanged);
            // 
            // cbSHDW
            // 
            this.cbSHDW.Font = new System.Drawing.Font("宋体", 14F);
            this.cbSHDW.FormattingEnabled = true;
            this.cbSHDW.Location = new System.Drawing.Point(354, 76);
            this.cbSHDW.Name = "cbSHDW";
            this.cbSHDW.Size = new System.Drawing.Size(175, 27);
            this.cbSHDW.TabIndex = 9;
            this.cbSHDW.Leave += new System.EventHandler(this.cbWLMC_Leave);
            this.cbSHDW.TextChanged += new System.EventHandler(this.cbSHDW_TextChanged);
            // 
            // txtJLY
            // 
            this.txtJLY.BackColor = System.Drawing.Color.Bisque;
            this.txtJLY.Font = new System.Drawing.Font("宋体", 14F);
            this.txtJLY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJLY.Location = new System.Drawing.Point(276, 184);
            this.txtJLY.MaxLength = 8;
            this.txtJLY.Name = "txtJLY";
            this.txtJLY.ReadOnly = true;
            this.txtJLY.Size = new System.Drawing.Size(80, 29);
            this.txtJLY.TabIndex = 16;
            // 
            // txtZL
            // 
            this.txtZL.BackColor = System.Drawing.Color.Bisque;
            this.txtZL.Enabled = false;
            this.txtZL.Font = new System.Drawing.Font("宋体", 14F);
            this.txtZL.ForeColor = System.Drawing.Color.Red;
            this.txtZL.Location = new System.Drawing.Point(628, 186);
            this.txtZL.MaxLength = 8;
            this.txtZL.Name = "txtZL";
            this.txtZL.ReadOnly = true;
            this.txtZL.Size = new System.Drawing.Size(80, 29);
            this.txtZL.TabIndex = 18;
            // 
            // txtJLD
            // 
            this.txtJLD.BackColor = System.Drawing.Color.Bisque;
            this.txtJLD.Font = new System.Drawing.Font("宋体", 14F);
            this.txtJLD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJLD.Location = new System.Drawing.Point(87, 185);
            this.txtJLD.MaxLength = 8;
            this.txtJLD.Name = "txtJLD";
            this.txtJLD.ReadOnly = true;
            this.txtJLD.Size = new System.Drawing.Size(112, 29);
            this.txtJLD.TabIndex = 15;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(544, 183);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 32);
            this.label21.TabIndex = 619;
            this.label21.Text = "总重(t)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(204, 183);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 32);
            this.label17.TabIndex = 617;
            this.label17.Text = "计量员";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(6, 182);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 32);
            this.label19.TabIndex = 615;
            this.label19.Text = "计量点";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(273, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 32);
            this.label16.TabIndex = 613;
            this.label16.Text = "收货单位";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(6, 76);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 32);
            this.label13.TabIndex = 609;
            this.label13.Text = "物料名称";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(543, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 32);
            this.label10.TabIndex = 606;
            this.label10.Text = "流向";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(6, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 32);
            this.label8.TabIndex = 602;
            this.label8.Text = "轧制编号";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtZZBH
            // 
            this.txtZZBH.BackColor = System.Drawing.SystemColors.Window;
            this.txtZZBH.Font = new System.Drawing.Font("宋体", 14F);
            this.txtZZBH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtZZBH.Location = new System.Drawing.Point(89, 41);
            this.txtZZBH.MaxLength = 8;
            this.txtZZBH.Name = "txtZZBH";
            this.txtZZBH.Size = new System.Drawing.Size(90, 29);
            this.txtZZBH.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(545, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 32);
            this.label11.TabIndex = 649;
            this.label11.Text = "品名";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.ultraGrid3);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(944, 185);
            this.ultraGroupBox1.TabIndex = 2;
            this.ultraGroupBox1.Text = "本批数据";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // ultraGrid3
            // 
            appearance35.BackColor = System.Drawing.Color.White;
            appearance35.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid3.DisplayLayout.Appearance = appearance35;
            this.ultraGrid3.DisplayLayout.InterBandSpacing = 10;
            appearance36.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid3.DisplayLayout.Override.CardAreaAppearance = appearance36;
            appearance37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance37.BackColor2 = System.Drawing.Color.White;
            appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance37.FontData.SizeInPoints = 11F;
            appearance37.FontData.UnderlineAsString = "False";
            appearance37.ForeColor = System.Drawing.Color.Black;
            appearance37.TextHAlignAsString = "Center";
            appearance37.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid3.DisplayLayout.Override.HeaderAppearance = appearance37;
            appearance38.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid3.DisplayLayout.Override.RowAppearance = appearance38;
            appearance39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance39.BackColor2 = System.Drawing.Color.White;
            appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorAppearance = appearance39;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid3.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance40.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance40.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid3.DisplayLayout.Override.SelectedRowAppearance = appearance40;
            this.ultraGrid3.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid3.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid3.Font = new System.Drawing.Font("宋体", 15F);
            this.ultraGrid3.Location = new System.Drawing.Point(3, 18);
            this.ultraGrid3.Name = "ultraGrid3";
            this.ultraGrid3.Size = new System.Drawing.Size(938, 164);
            this.ultraGrid3.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.ultraGroupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(84, 470);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(944, 185);
            this.panel4.TabIndex = 21;
            // 
            // windowDockingArea1
            // 
            this.windowDockingArea1.Controls.Add(this.dockableWindow2);
            this.windowDockingArea1.Dock = System.Windows.Forms.DockStyle.Right;
            this.windowDockingArea1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.windowDockingArea1.Location = new System.Drawing.Point(871, 0);
            this.windowDockingArea1.Name = "windowDockingArea1";
            this.windowDockingArea1.Owner = this.ultraDockManager1;
            this.windowDockingArea1.Size = new System.Drawing.Size(100, 666);
            this.windowDockingArea1.TabIndex = 13;
            // 
            // coolIndicator1
            // 
            this.coolIndicator1.Gradient = true;
            this.coolIndicator1.Location = new System.Drawing.Point(0, 0);
            this.coolIndicator1.Name = "coolIndicator1";
            this.coolIndicator1.Size = new System.Drawing.Size(0, 0);
            this.coolIndicator1.TabIndex = 0;
            this.coolIndicator1.Text = "coolIndicator1";
            // 
            // Weight_PIPE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 655);
            this.Controls.Add(this._Finishing_HotRolledCoilInfoAutoHideControl);
            this.Controls.Add(this.picFDTP);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowDockingArea1);
            this.Controls.Add(this._Finishing_HotRolledCoilInfoUnpinnedTabAreaTop);
            this.Controls.Add(this._Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom);
            this.Controls.Add(this._Finishing_HotRolledCoilInfoUnpinnedTabAreaRight);
            this.Controls.Add(this._Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft);
            this.KeyPreview = true;
            this.Name = "Weight_PIPE";
            this.Text = "制管材秤计量";
            this.Load += new System.EventHandler(this.Weight_BC_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Weight_BC_FormClosed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Weight_BC_KeyPress);
            this.VoiceC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).EndInit();
            this.dockableWindow2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).EndInit();
            this.panel15.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtXSZL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ds_plan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
            this.panel9.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).EndInit();
            this.panel4.ResumeLayout(false);
            this.windowDockingArea1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel VoiceC;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid5;
        private Infragistics.Win.UltraWinDock.UltraDockManager ultraDockManager1;
        private Infragistics.Win.UltraWinDock.AutoHideControl _Finishing_HotRolledCoilInfoAutoHideControl;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _Finishing_HotRolledCoilInfoUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _Finishing_HotRolledCoilInfoUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _Finishing_HotRolledCoilInfoUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _Finishing_HotRolledCoilInfoUnpinnedTabAreaRight;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow2;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox VideoChannel2;
        private System.Windows.Forms.PictureBox VideoChannel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbYS;
        private System.Windows.Forms.Label lbWD;
        private System.Windows.Forms.Button btnQL;
        private System.Windows.Forms.Panel panel1_Fill_Panel;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtGH;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Panel panel13;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn26;
        private System.Data.DataColumn dataColumn27;
        private System.Data.DataColumn dataColumn28;
        private System.Data.DataColumn dataColumn52;
        private System.Data.DataColumn dataColumn63;
        private System.Data.DataColumn dataColumn64;
        private System.Data.DataColumn dataColumn65;
        private System.Data.DataColumn dataColumn66;
        private System.Data.DataColumn dataColumn67;
        private System.Data.DataColumn dataColumn68;
        private System.Data.DataColumn dataColumn69;
        private System.Data.DataColumn dataColumn70;
        private System.Data.DataColumn dataColumn72;
        private System.Data.DataColumn dataColumn73;
        private System.Data.DataColumn dataColumn74;
        private System.Data.DataColumn dataColumn75;
        private System.Data.DataColumn dataColumn76;
        private System.Data.DataColumn dataColumn77;
        private System.Data.DataColumn dataColumn78;
        private System.Data.DataColumn dataColumn79;
        private System.Data.DataColumn dataColumn80;
        private System.Data.DataColumn dataColumn81;
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
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumn29;
        private System.Data.DataColumn dataColumn30;
        private System.Data.DataColumn dataColumn31;
        private System.Data.DataColumn dataColumn32;
        private System.Windows.Forms.PictureBox picFDTP;
        private System.Data.DataColumn dataColumn1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Data.DataSet dataSet2;
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Windows.Forms.TextBox txtDH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtYKL;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtUsedPrintTink;
        private System.Windows.Forms.TextBox txtUsedPrintPaper;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Windows.Forms.Button btnOpen;
        private YGJZJL.Pipe.CoolIndicator coolIndicator1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbx_print;
        private System.Data.DataSet ds_plan;
        private System.Data.DataTable dataTable4;
        private System.Data.DataColumn dataColumn10;
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
        private System.Data.DataColumn dataColumn21;
        private System.Data.DataColumn dataColumn22;
        private System.Data.DataColumn dataColumn23;
        private System.Data.DataColumn dataColumn24;
        private System.Data.DataColumn dataColumn25;
        private System.Data.DataColumn dataColumn33;
        private System.Data.DataColumn dataColumn34;
        private System.Data.DataColumn dataColumn35;
        private System.Data.DataColumn dataColumn36;
        private System.Data.DataColumn dataColumn37;
        private System.Data.DataColumn dataColumn38;
        private System.Data.DataColumn dataColumn39;
        private System.Data.DataColumn dataColumn40;
        private System.Data.DataColumn dataColumn41;
        private System.Windows.Forms.Button button16;
        private System.Data.DataColumn dataColumn42;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Drawing.Printing.PrintDocument printDocument3;
        private YGJZJL.Pipe.CoolIndicator StatusLight;
        private System.Windows.Forms.Button btnWeightException;
        private System.Windows.Forms.Button btnWeightComplete;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.TextBox tbx_theoryweight;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbx_lasttotalweight;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbx_lastbandcount;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tbx_lastbatch;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbx_Standard;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox tbx_hWeight;
        private System.Windows.Forms.CheckBox cbx_Hand;
        private System.Windows.Forms.TextBox tb_bandno_fb;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tb_zzbh_fb;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox cbx_fb;
        private System.Windows.Forms.Button btnBC;
        private System.Windows.Forms.Button btnWL;
        private System.Windows.Forms.ComboBox cbDCCD;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtZS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbLXIN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDDH;
        private System.Windows.Forms.ComboBox cbGG;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbGZ;
        public System.Windows.Forms.ComboBox cbFHDW;
        private System.Windows.Forms.ComboBox cbLX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbWLMC;
        public System.Windows.Forms.ComboBox cbSHDW;
        private System.Windows.Forms.TextBox txtJLY;
        private System.Windows.Forms.TextBox txtZL;
        private System.Windows.Forms.TextBox txtJLD;
        private System.Windows.Forms.TextBox txtBC;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtZZBH;
        private System.Windows.Forms.Label label11;
        private LxControl.LxLedControl txtXSZL;
        private LxControl.LxLedControl lxLedControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Data.DataColumn dataColumn43;
        private System.Windows.Forms.TextBox txtBZ;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textMinWeight;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox textMaxWeight;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtQTYKL;
        private System.Windows.Forms.Button btnChangStove;
        private System.Windows.Forms.TextBox txtJZ;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Panel panel4;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid3;
        private System.Windows.Forms.ComboBox cbx_PM;
        private System.Windows.Forms.TextBox txtBandNo;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox tbRKDH;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox cbPrint;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox cbxRemark;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtThick;
        private System.Windows.Forms.Label label37;
    }
}