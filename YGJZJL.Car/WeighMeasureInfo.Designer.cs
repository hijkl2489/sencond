namespace YGJZJL.Car
{
    partial class WeighMeasureInfo
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
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("绑定一次计量表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PLANCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CARDNUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CARNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CONTRACTNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CONTRACTITEM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIAL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIALNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LX");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_FHDW");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDERSTORE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SHDW");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRANSNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CYDW");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVERSTORE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POUNDTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POUND");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_SENDGROSSWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_SENDTAREWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_SENDNETWEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_WEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_WEIGHTER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_WEIGHTTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SHIFT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TERM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_LOADINSTORETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_LOADOUTSTORETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_UNLOADFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_UNLOADSTOREPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LOADFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LOADSTOREPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SAMPLEPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_FIRSTLABELID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_UNLOADINSTORETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_UNLOADOUTSTORETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_YCSFYC");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_YKL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_STOVENO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_COUNT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_SAMPLETIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SAMPLEPLACE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SAMPLEFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_UNLOADPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_UNLOADTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_UNLOADPLACE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CHECKPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_CHECKTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CHECKPLACE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_CHECKFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_IFSAMPLING");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_IFACCEPT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DRIVERNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DRIVERIDCARD");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_REWEIGHTFLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FD_REWEIGHTTIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_REWEIGHTPLACE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_REWEIGHTPERSON");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DFJZ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_BILLNUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_YKBL");
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement1 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect1 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("语音表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICENAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_INSTRTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MEMO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICEFILE");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("语音表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICENAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_INSTRTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MEMO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VOICEFILE");
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool8 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YYDJ");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool3 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("剩余纸张数");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool17 = new Infragistics.Win.UltraWinToolbars.ButtonTool("HZ");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool19 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool20 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YCJLTX");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool21 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BDDY");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool1 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("ControlContainerTool1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool22 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btCorrention");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool23 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YYDJ");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool6 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("剩余纸张数");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool24 = new Infragistics.Win.UltraWinToolbars.ButtonTool("HZ");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool26 = new Infragistics.Win.UltraWinToolbars.ButtonTool("QHSP");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool27 = new Infragistics.Win.UltraWinToolbars.ButtonTool("YCJLTX");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool28 = new Infragistics.Win.UltraWinToolbars.ButtonTool("BDDY");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool29 = new Infragistics.Win.UltraWinToolbars.ButtonTool("btCorrention");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool30 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.UltraWinToolbars.ControlContainerTool controlContainerTool2 = new Infragistics.Win.UltraWinToolbars.ControlContainerTool("ControlContainerTool1");
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("计量点基础表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("XZ");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SIGN");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_METERTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_METERPARA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MOXAIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MOXAPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn82 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOUSER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn83 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_VIEDOPWD");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn84 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTDEPART");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RTUIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RTUPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PRINTERIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PRINTERNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PRINTTYPECODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_USEDPRINTPAPER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn92 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_USEDPRINTINK");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn93 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LEDIP");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn94 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LEDPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_VALUE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_ALLOWOTHERTARE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DISPLAYPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn98 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DISPLAYPARA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn99 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_READERPORT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn100 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_READERPARA");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn101 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_READERTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn102 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_DISPLAYTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn103 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_LEDTYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn104 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FF_CLEARVALUE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn105 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTSTATE");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane1 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("32d14a8b-0181-4ead-8b7c-2bf841e5b2f4"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane1 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("77136960-0f57-42d8-b935-2ea6dbebbbfe"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("32d14a8b-0181-4ead-8b7c-2bf841e5b2f4"), -1);
            Infragistics.Win.UltraWinDock.DockAreaPane dockAreaPane2 = new Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.DockedRight, new System.Guid("7e44afaa-31e1-457e-913f-0ae6ac575567"));
            Infragistics.Win.UltraWinDock.DockableControlPane dockableControlPane2 = new Infragistics.Win.UltraWinDock.DockableControlPane(new System.Guid("413b1f55-3871-4022-81b9-b73a24cd10e7"), new System.Guid("00000000-0000-0000-0000-000000000000"), -1, new System.Guid("7e44afaa-31e1-457e-913f-0ae6ac575567"), -1);
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement2 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect2 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeighMeasureInfo));
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.panelYCSP = new System.Windows.Forms.Panel();
            this.panel22 = new System.Windows.Forms.Panel();
            this.VideoChannel5 = new System.Windows.Forms.PictureBox();
            this.panel25 = new System.Windows.Forms.Panel();
            this.VideoChannel6 = new System.Windows.Forms.PictureBox();
            this.panel21 = new System.Windows.Forms.Panel();
            this.VideoChannel4 = new System.Windows.Forms.PictureBox();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraGrid3 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
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
            this.dataColumn171 = new System.Data.DataColumn();
            this.dataColumn216 = new System.Data.DataColumn();
            this.dataTable2 = new System.Data.DataTable();
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
            this.dataColumn15 = new System.Data.DataColumn();
            this.dataColumn16 = new System.Data.DataColumn();
            this.dataColumn17 = new System.Data.DataColumn();
            this.dataColumn18 = new System.Data.DataColumn();
            this.dataColumn19 = new System.Data.DataColumn();
            this.dataColumn20 = new System.Data.DataColumn();
            this.dataColumn51 = new System.Data.DataColumn();
            this.dataColumn71 = new System.Data.DataColumn();
            this.dataColumn94 = new System.Data.DataColumn();
            this.dataColumn95 = new System.Data.DataColumn();
            this.dataColumn96 = new System.Data.DataColumn();
            this.dataColumn97 = new System.Data.DataColumn();
            this.dataColumn98 = new System.Data.DataColumn();
            this.dataColumn99 = new System.Data.DataColumn();
            this.dataColumn100 = new System.Data.DataColumn();
            this.dataColumn102 = new System.Data.DataColumn();
            this.dataColumn101 = new System.Data.DataColumn();
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
            this.dataColumn176 = new System.Data.DataColumn();
            this.dataColumn177 = new System.Data.DataColumn();
            this.dataColumn178 = new System.Data.DataColumn();
            this.dataColumn179 = new System.Data.DataColumn();
            this.dataColumn180 = new System.Data.DataColumn();
            this.dataColumn181 = new System.Data.DataColumn();
            this.dataColumn182 = new System.Data.DataColumn();
            this.dataColumn183 = new System.Data.DataColumn();
            this.dataColumn184 = new System.Data.DataColumn();
            this.dataColumn185 = new System.Data.DataColumn();
            this.dataColumn186 = new System.Data.DataColumn();
            this.dataColumn187 = new System.Data.DataColumn();
            this.dataColumn188 = new System.Data.DataColumn();
            this.dataColumn189 = new System.Data.DataColumn();
            this.dataColumn204 = new System.Data.DataColumn();
            this.dataColumn205 = new System.Data.DataColumn();
            this.dataColumn206 = new System.Data.DataColumn();
            this.dataColumn207 = new System.Data.DataColumn();
            this.dataColumn208 = new System.Data.DataColumn();
            this.dataTable3 = new System.Data.DataTable();
            this.dataColumn21 = new System.Data.DataColumn();
            this.dataColumn22 = new System.Data.DataColumn();
            this.dataColumn92 = new System.Data.DataColumn();
            this.dataColumn93 = new System.Data.DataColumn();
            this.dataTable4 = new System.Data.DataTable();
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
            this.dataColumn53 = new System.Data.DataColumn();
            this.dataColumn172 = new System.Data.DataColumn();
            this.dataColumn173 = new System.Data.DataColumn();
            this.dataColumn174 = new System.Data.DataColumn();
            this.dataColumn175 = new System.Data.DataColumn();
            this.dataColumn217 = new System.Data.DataColumn();
            this.dataColumn218 = new System.Data.DataColumn();
            this.dataTable5 = new System.Data.DataTable();
            this.dataColumn36 = new System.Data.DataColumn();
            this.dataColumn37 = new System.Data.DataColumn();
            this.dataColumn38 = new System.Data.DataColumn();
            this.dataColumn39 = new System.Data.DataColumn();
            this.dataColumn40 = new System.Data.DataColumn();
            this.dataColumn41 = new System.Data.DataColumn();
            this.dataColumn42 = new System.Data.DataColumn();
            this.dataColumn43 = new System.Data.DataColumn();
            this.dataColumn44 = new System.Data.DataColumn();
            this.dataColumn45 = new System.Data.DataColumn();
            this.dataColumn46 = new System.Data.DataColumn();
            this.dataColumn47 = new System.Data.DataColumn();
            this.dataColumn48 = new System.Data.DataColumn();
            this.dataColumn49 = new System.Data.DataColumn();
            this.dataColumn50 = new System.Data.DataColumn();
            this.dataTable6 = new System.Data.DataTable();
            this.dataColumn54 = new System.Data.DataColumn();
            this.dataColumn55 = new System.Data.DataColumn();
            this.dataColumn56 = new System.Data.DataColumn();
            this.dataColumn57 = new System.Data.DataColumn();
            this.dataColumn58 = new System.Data.DataColumn();
            this.dataColumn59 = new System.Data.DataColumn();
            this.dataColumn60 = new System.Data.DataColumn();
            this.dataColumn61 = new System.Data.DataColumn();
            this.dataColumn62 = new System.Data.DataColumn();
            this.dataTable7 = new System.Data.DataTable();
            this.dataColumn116 = new System.Data.DataColumn();
            this.dataColumn117 = new System.Data.DataColumn();
            this.dataColumn118 = new System.Data.DataColumn();
            this.dataColumn119 = new System.Data.DataColumn();
            this.dataTable8 = new System.Data.DataTable();
            this.dataColumn120 = new System.Data.DataColumn();
            this.dataColumn121 = new System.Data.DataColumn();
            this.dataColumn122 = new System.Data.DataColumn();
            this.dataTable9 = new System.Data.DataTable();
            this.dataColumn123 = new System.Data.DataColumn();
            this.dataColumn124 = new System.Data.DataColumn();
            this.dataColumn125 = new System.Data.DataColumn();
            this.dataColumn126 = new System.Data.DataColumn();
            this.dataTable10 = new System.Data.DataTable();
            this.dataColumn127 = new System.Data.DataColumn();
            this.dataColumn128 = new System.Data.DataColumn();
            this.dataTable11 = new System.Data.DataTable();
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
            this.dataColumn156 = new System.Data.DataColumn();
            this.dataColumn157 = new System.Data.DataColumn();
            this.dataColumn158 = new System.Data.DataColumn();
            this.dataColumn159 = new System.Data.DataColumn();
            this.dataColumn160 = new System.Data.DataColumn();
            this.dataColumn161 = new System.Data.DataColumn();
            this.dataColumn162 = new System.Data.DataColumn();
            this.dataColumn163 = new System.Data.DataColumn();
            this.dataColumn164 = new System.Data.DataColumn();
            this.dataColumn165 = new System.Data.DataColumn();
            this.dataColumn166 = new System.Data.DataColumn();
            this.dataColumn167 = new System.Data.DataColumn();
            this.dataColumn168 = new System.Data.DataColumn();
            this.dataColumn169 = new System.Data.DataColumn();
            this.dataColumn170 = new System.Data.DataColumn();
            this.dataColumn190 = new System.Data.DataColumn();
            this.dataColumn191 = new System.Data.DataColumn();
            this.dataColumn192 = new System.Data.DataColumn();
            this.dataColumn193 = new System.Data.DataColumn();
            this.dataColumn194 = new System.Data.DataColumn();
            this.dataColumn195 = new System.Data.DataColumn();
            this.dataColumn196 = new System.Data.DataColumn();
            this.dataColumn197 = new System.Data.DataColumn();
            this.dataColumn198 = new System.Data.DataColumn();
            this.dataColumn199 = new System.Data.DataColumn();
            this.dataColumn200 = new System.Data.DataColumn();
            this.dataColumn201 = new System.Data.DataColumn();
            this.dataColumn202 = new System.Data.DataColumn();
            this.dataColumn203 = new System.Data.DataColumn();
            this.dataColumn209 = new System.Data.DataColumn();
            this.dataColumn210 = new System.Data.DataColumn();
            this.dataColumn211 = new System.Data.DataColumn();
            this.dataColumn212 = new System.Data.DataColumn();
            this.dataColumn213 = new System.Data.DataColumn();
            this.dataColumn214 = new System.Data.DataColumn();
            this.dataColumn215 = new System.Data.DataColumn();
            this.ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ultraChart1 = new Infragistics.Win.UltraWinChart.UltraChart();
            this.picHT = new System.Windows.Forms.PictureBox();
            this.panelYYBF = new System.Windows.Forms.Panel();
            this.panel23 = new System.Windows.Forms.Panel();
            this.ultraGrid5 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.ultraGrid4 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panelSPKZ = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chb_AutoInfrared = new System.Windows.Forms.CheckBox();
            this.txtZZ = new System.Windows.Forms.TextBox();
            this.panel1_Fill_Panel = new System.Windows.Forms.Panel();
            this._panel1_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._panel1_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.txtTDL = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.VideoChannel8 = new System.Windows.Forms.PictureBox();
            this.VideoChannel7 = new System.Windows.Forms.PictureBox();
            this.VideoChannel3 = new System.Windows.Forms.PictureBox();
            this.VideoChannel2 = new System.Windows.Forms.PictureBox();
            this.VideoChannel1 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtXSZL = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StatusBack = new YGJZJL.Car.CoolInfraredRay();
            this.StatusFront = new YGJZJL.Car.CoolInfraredRay();
            this.StatusLight = new YGJZJL.Car.CoolIndicator();
            this.StatusRedGreen = new YGJZJL.Car.CoolIndicator();
            this.btnHDHW = new System.Windows.Forms.Button();
            this.btnQDHW = new System.Windows.Forms.Button();
            this.btnZMDKG = new System.Windows.Forms.Button();
            this.btnHL = new System.Windows.Forms.Button();
            this.btnQL = new System.Windows.Forms.Button();
            this.lbWD = new System.Windows.Forms.Label();
            this.lbYS = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.ultraGrid2 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnBC = new System.Windows.Forms.Button();
            this.btnGPBC = new System.Windows.Forms.Button();
            this.btnSD = new System.Windows.Forms.Button();
            this.btnDS = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.cbProvider = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tbBZ = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.tbCharge = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbReceiverPlace = new System.Windows.Forms.TextBox();
            this.tbSenderPlace = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtPJBH = new System.Windows.Forms.TextBox();
            this.txtHTXMH = new System.Windows.Forms.ComboBox();
            this.txtDFJZ = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.btnFHDW = new System.Windows.Forms.Button();
            this.btnSHDW = new System.Windows.Forms.Button();
            this.btnCYDW = new System.Windows.Forms.Button();
            this.btnWLMC = new System.Windows.Forms.Button();
            this.txtYKL = new System.Windows.Forms.TextBox();
            this.txtZL = new System.Windows.Forms.TextBox();
            this.txtPZ = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJZ = new System.Windows.Forms.TextBox();
            this.txtMZ = new System.Windows.Forms.TextBox();
            this.txtZS3 = new System.Windows.Forms.TextBox();
            this.txtZS2 = new System.Windows.Forms.TextBox();
            this.txtZS = new System.Windows.Forms.TextBox();
            this.chbSFYC = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBC = new System.Windows.Forms.TextBox();
            this.cbJLLX = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chbQXPZ = new System.Windows.Forms.CheckBox();
            this.cbCH = new System.Windows.Forms.ComboBox();
            this.cbCH1 = new System.Windows.Forms.ComboBox();
            this.cbWLMC = new System.Windows.Forms.ComboBox();
            this.cbSHDW = new System.Windows.Forms.ComboBox();
            this.cbCYDW = new System.Windows.Forms.ComboBox();
            this.txtJLY = new System.Windows.Forms.TextBox();
            this.cbFHDW = new System.Windows.Forms.ComboBox();
            this.cbLX = new System.Windows.Forms.ComboBox();
            this.txtJLD = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLH = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHTH = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCZH = new System.Windows.Forms.TextBox();
            this.txtCarNo = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbLS = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtLH3 = new System.Windows.Forms.TextBox();
            this.txtLH2 = new System.Windows.Forms.TextBox();
            this.ultraDockManager1 = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._WeighMeasureInfoUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._WeighMeasureInfoUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._WeighMeasureInfoUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._WeighMeasureInfoUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._WeighMeasureInfoAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.dockableWindow2 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.dockableWindow1 = new Infragistics.Win.UltraWinDock.DockableWindow();
            this.panel38 = new System.Windows.Forms.Panel();
            this.pictureBox38 = new System.Windows.Forms.PictureBox();
            this.panel37 = new System.Windows.Forms.Panel();
            this.pictureBox37 = new System.Windows.Forms.PictureBox();
            this.panel36 = new System.Windows.Forms.Panel();
            this.pictureBox36 = new System.Windows.Forms.PictureBox();
            this.panel33 = new System.Windows.Forms.Panel();
            this.pictureBox33 = new System.Windows.Forms.PictureBox();
            this.panel32 = new System.Windows.Forms.Panel();
            this.pictureBox32 = new System.Windows.Forms.PictureBox();
            this.panel31 = new System.Windows.Forms.Panel();
            this.pictureBox31 = new System.Windows.Forms.PictureBox();
            this.panel18 = new System.Windows.Forms.Panel();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.panel19 = new System.Windows.Forms.Panel();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.picFDTP = new System.Windows.Forms.PictureBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panelYCJL = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.ultraChart2 = new Infragistics.Win.UltraWinChart.UltraChart();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.panel15 = new System.Windows.Forms.Panel();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.panel16 = new System.Windows.Forms.Panel();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.panel17 = new System.Windows.Forms.Panel();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.windowDockingArea1 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.windowDockingArea2 = new Infragistics.Win.UltraWinDock.WindowDockingArea();
            this.ultraTabPageControl3.SuspendLayout();
            this.panelYCSP.SuspendLayout();
            this.panel22.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel5)).BeginInit();
            this.panel25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel6)).BeginInit();
            this.panel21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel4)).BeginInit();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable11)).BeginInit();
            this.ultraTabPageControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHT)).BeginInit();
            this.panelYYBF.SuspendLayout();
            this.panel23.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid4)).BeginInit();
            this.panelSPKZ.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).BeginInit();
            this.ultraTabControl1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).BeginInit();
            this._WeighMeasureInfoAutoHideControl.SuspendLayout();
            this.dockableWindow2.SuspendLayout();
            this.dockableWindow1.SuspendLayout();
            this.panel38.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox38)).BeginInit();
            this.panel37.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).BeginInit();
            this.panel36.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox36)).BeginInit();
            this.panel33.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox33)).BeginInit();
            this.panel32.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox32)).BeginInit();
            this.panel31.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox31)).BeginInit();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            this.panel19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).BeginInit();
            this.panelYCJL.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            this.panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.panelYCSP);
            this.coreBind.SetDatabasecommand(this.ultraTabPageControl3, null);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(1, 20);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(741, 153);
            this.coreBind.SetVerification(this.ultraTabPageControl3, null);
            // 
            // panelYCSP
            // 
            this.panelYCSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panelYCSP.Controls.Add(this.panel22);
            this.panelYCSP.Controls.Add(this.panel25);
            this.panelYCSP.Controls.Add(this.panel21);
            this.coreBind.SetDatabasecommand(this.panelYCSP, null);
            this.panelYCSP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelYCSP.Location = new System.Drawing.Point(0, 0);
            this.panelYCSP.Name = "panelYCSP";
            this.panelYCSP.Size = new System.Drawing.Size(741, 153);
            this.panelYCSP.TabIndex = 17;
            this.coreBind.SetVerification(this.panelYCSP, null);
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.VideoChannel5);
            this.coreBind.SetDatabasecommand(this.panel22, null);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel22.Location = new System.Drawing.Point(240, 0);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(262, 153);
            this.panel22.TabIndex = 3;
            this.coreBind.SetVerification(this.panel22, null);
            // 
            // VideoChannel5
            // 
            this.VideoChannel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel5, null);
            this.VideoChannel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel5.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel5.Name = "VideoChannel5";
            this.VideoChannel5.Size = new System.Drawing.Size(262, 153);
            this.VideoChannel5.TabIndex = 3;
            this.VideoChannel5.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel5, null);
            this.VideoChannel5.Click += new System.EventHandler(this.VideoChannel5_Click);
            // 
            // panel25
            // 
            this.panel25.Controls.Add(this.VideoChannel6);
            this.coreBind.SetDatabasecommand(this.panel25, null);
            this.panel25.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel25.Location = new System.Drawing.Point(502, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(239, 153);
            this.panel25.TabIndex = 1;
            this.coreBind.SetVerification(this.panel25, null);
            // 
            // VideoChannel6
            // 
            this.VideoChannel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel6, null);
            this.VideoChannel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel6.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel6.Name = "VideoChannel6";
            this.VideoChannel6.Size = new System.Drawing.Size(239, 153);
            this.VideoChannel6.TabIndex = 3;
            this.VideoChannel6.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel6, null);
            this.VideoChannel6.Click += new System.EventHandler(this.VideoChannel6_Click);
            // 
            // panel21
            // 
            this.panel21.Controls.Add(this.VideoChannel4);
            this.coreBind.SetDatabasecommand(this.panel21, null);
            this.panel21.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel21.Location = new System.Drawing.Point(0, 0);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(240, 153);
            this.panel21.TabIndex = 0;
            this.coreBind.SetVerification(this.panel21, null);
            // 
            // VideoChannel4
            // 
            this.VideoChannel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel4, null);
            this.VideoChannel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VideoChannel4.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel4.Name = "VideoChannel4";
            this.VideoChannel4.Size = new System.Drawing.Size(240, 153);
            this.VideoChannel4.TabIndex = 2;
            this.VideoChannel4.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel4, null);
            this.VideoChannel4.Click += new System.EventHandler(this.VideoChannel4_Click);
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ultraGrid3);
            this.coreBind.SetDatabasecommand(this.ultraTabPageControl1, null);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(741, 153);
            this.coreBind.SetVerification(this.ultraTabPageControl1, null);
            // 
            // ultraGrid3
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid3, null);
            this.ultraGrid3.DataMember = "绑定一次计量表";
            this.ultraGrid3.DataSource = this.dataSet1;
            appearance31.BackColor = System.Drawing.Color.White;
            appearance31.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid3.DisplayLayout.Appearance = appearance31;
            ultraGridColumn1.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn4.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn5.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn5.Header.VisiblePosition = 58;
            ultraGridColumn6.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn6.Header.VisiblePosition = 59;
            ultraGridColumn7.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn8.Header.VisiblePosition = 5;
            ultraGridColumn9.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn9.Header.VisiblePosition = 7;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn10.Header.VisiblePosition = 8;
            ultraGridColumn11.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn11.Header.VisiblePosition = 9;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn12.Header.VisiblePosition = 10;
            ultraGridColumn13.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn13.Header.VisiblePosition = 11;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn14.Header.VisiblePosition = 12;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn15.Header.VisiblePosition = 13;
            ultraGridColumn16.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn16.Header.VisiblePosition = 14;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn17.Header.VisiblePosition = 15;
            ultraGridColumn18.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn18.Header.VisiblePosition = 16;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn19.Header.VisiblePosition = 17;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn20.Header.VisiblePosition = 18;
            ultraGridColumn21.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn21.Header.VisiblePosition = 19;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn22.Header.VisiblePosition = 20;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn23.Header.VisiblePosition = 21;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn24.Header.VisiblePosition = 6;
            ultraGridColumn25.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn25.Header.VisiblePosition = 22;
            ultraGridColumn26.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn26.Header.VisiblePosition = 23;
            ultraGridColumn27.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn27.Header.VisiblePosition = 24;
            ultraGridColumn28.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn28.Header.VisiblePosition = 29;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn29.Header.VisiblePosition = 30;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn30.Header.VisiblePosition = 31;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn31.Header.VisiblePosition = 32;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn32.Header.VisiblePosition = 33;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn33.Header.VisiblePosition = 34;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn34.Header.VisiblePosition = 35;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn35.Header.VisiblePosition = 36;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn36.Header.VisiblePosition = 37;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn37.Header.VisiblePosition = 38;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn38.Header.VisiblePosition = 39;
            ultraGridColumn38.Hidden = true;
            ultraGridColumn39.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn39.Header.VisiblePosition = 28;
            ultraGridColumn40.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn40.Header.VisiblePosition = 27;
            ultraGridColumn41.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn41.Header.VisiblePosition = 25;
            ultraGridColumn42.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn42.Header.VisiblePosition = 26;
            ultraGridColumn43.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn43.Header.VisiblePosition = 40;
            ultraGridColumn44.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn44.Header.VisiblePosition = 41;
            ultraGridColumn45.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn45.Header.VisiblePosition = 42;
            ultraGridColumn46.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn46.Header.VisiblePosition = 43;
            ultraGridColumn47.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn47.Header.VisiblePosition = 44;
            ultraGridColumn48.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn48.Header.VisiblePosition = 45;
            ultraGridColumn49.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn49.Header.VisiblePosition = 46;
            ultraGridColumn50.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn50.Header.VisiblePosition = 47;
            ultraGridColumn51.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn51.Header.VisiblePosition = 48;
            ultraGridColumn52.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn52.Header.VisiblePosition = 49;
            ultraGridColumn53.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn53.Header.VisiblePosition = 50;
            ultraGridColumn54.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn54.Header.VisiblePosition = 51;
            ultraGridColumn55.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn55.Header.VisiblePosition = 52;
            ultraGridColumn56.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn56.Header.VisiblePosition = 53;
            ultraGridColumn57.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn57.Header.VisiblePosition = 54;
            ultraGridColumn58.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn58.Header.VisiblePosition = 55;
            ultraGridColumn58.Hidden = true;
            ultraGridColumn59.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn59.Header.VisiblePosition = 56;
            ultraGridColumn59.Hidden = true;
            ultraGridColumn60.FilterOperatorDropDownItems = ((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems)((Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Equals | Infragistics.Win.UltraWinGrid.FilterOperatorDropDownItems.Contains)));
            ultraGridColumn60.Header.VisiblePosition = 57;
            ultraGridColumn60.Hidden = true;
            ultraGridColumn61.Header.VisiblePosition = 60;
            ultraGridColumn62.Header.VisiblePosition = 61;
            ultraGridColumn63.Header.VisiblePosition = 62;
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
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61,
            ultraGridColumn62,
            ultraGridColumn63});
            this.ultraGrid3.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid3.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid3.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.ultraGrid3.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGrid3.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance32.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid3.DisplayLayout.Override.CardAreaAppearance = appearance32;
            this.ultraGrid3.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ultraGrid3.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
            this.ultraGrid3.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            appearance33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance33.BackColor2 = System.Drawing.Color.White;
            appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance33.FontData.SizeInPoints = 11F;
            appearance33.FontData.UnderlineAsString = "False";
            appearance33.ForeColor = System.Drawing.Color.Black;
            appearance33.TextHAlignAsString = "Center";
            appearance33.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid3.DisplayLayout.Override.HeaderAppearance = appearance33;
            this.ultraGrid3.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            appearance34.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid3.DisplayLayout.Override.RowAppearance = appearance34;
            appearance35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance35.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorAppearance = appearance35;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid3.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance36.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance36.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid3.DisplayLayout.Override.SelectedRowAppearance = appearance36;
            this.ultraGrid3.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid3.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.ultraGrid3.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.ultraGrid3.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid3.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid3.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGrid3.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGrid3.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ultraGrid3.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.ultraGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid3.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid3.Name = "ultraGrid3";
            this.ultraGrid3.Size = new System.Drawing.Size(741, 153);
            this.ultraGrid3.TabIndex = 3;
            this.coreBind.SetVerification(this.ultraGrid3, null);
            this.ultraGrid3.Click += new System.EventHandler(this.ultraGrid3_Click);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1,
            this.dataTable2,
            this.dataTable3,
            this.dataTable4,
            this.dataTable5,
            this.dataTable6,
            this.dataTable7,
            this.dataTable8,
            this.dataTable9,
            this.dataTable10,
            this.dataTable11});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
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
            this.dataColumn171,
            this.dataColumn216});
            this.dataTable1.TableName = "计量点基础表";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "计量点编码";
            this.dataColumn1.ColumnName = "FS_POINTCODE";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "计量点";
            this.dataColumn2.ColumnName = "FS_POINTNAME";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "接管";
            this.dataColumn3.ColumnName = "XZ";
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
            // dataColumn171
            // 
            this.dataColumn171.ColumnName = "FF_CLEARVALUE";
            // 
            // dataColumn216
            // 
            this.dataColumn216.Caption = "计量点状态";
            this.dataColumn216.ColumnName = "FS_POINTSTATE";
            // 
            // dataTable2
            // 
            this.dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
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
            this.dataColumn14,
            this.dataColumn15,
            this.dataColumn16,
            this.dataColumn17,
            this.dataColumn18,
            this.dataColumn19,
            this.dataColumn20,
            this.dataColumn51,
            this.dataColumn71,
            this.dataColumn94,
            this.dataColumn95,
            this.dataColumn96,
            this.dataColumn97,
            this.dataColumn98,
            this.dataColumn99,
            this.dataColumn100,
            this.dataColumn102,
            this.dataColumn101,
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
            this.dataColumn176,
            this.dataColumn177,
            this.dataColumn178,
            this.dataColumn179,
            this.dataColumn180,
            this.dataColumn181,
            this.dataColumn182,
            this.dataColumn183,
            this.dataColumn184,
            this.dataColumn185,
            this.dataColumn186,
            this.dataColumn187,
            this.dataColumn188,
            this.dataColumn189,
            this.dataColumn204,
            this.dataColumn205,
            this.dataColumn206,
            this.dataColumn207,
            this.dataColumn208});
            this.dataTable2.TableName = "一次计量表";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "作业编号";
            this.dataColumn4.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "预报号";
            this.dataColumn5.ColumnName = "FS_PLANCODE";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "合同号";
            this.dataColumn6.ColumnName = "FS_CONTRACTNO";
            // 
            // dataColumn7
            // 
            this.dataColumn7.Caption = "合同项目编号";
            this.dataColumn7.ColumnName = "FS_CONTRACTITEM";
            // 
            // dataColumn8
            // 
            this.dataColumn8.Caption = "炉号";
            this.dataColumn8.ColumnName = "FS_STOVENO";
            // 
            // dataColumn9
            // 
            this.dataColumn9.Caption = "支数";
            this.dataColumn9.ColumnName = "FN_COUNT";
            // 
            // dataColumn10
            // 
            this.dataColumn10.Caption = "卡号";
            this.dataColumn10.ColumnName = "FS_CARDNUMBER";
            // 
            // dataColumn11
            // 
            this.dataColumn11.Caption = "车号";
            this.dataColumn11.ColumnName = "FS_CARNO";
            // 
            // dataColumn12
            // 
            this.dataColumn12.Caption = "重量";
            this.dataColumn12.ColumnName = "FN_WEIGHT";
            // 
            // dataColumn13
            // 
            this.dataColumn13.Caption = "计量点";
            this.dataColumn13.ColumnName = "FS_POUND";
            // 
            // dataColumn14
            // 
            this.dataColumn14.Caption = "计量员";
            this.dataColumn14.ColumnName = "FS_WEIGHTER";
            // 
            // dataColumn15
            // 
            this.dataColumn15.Caption = "计量时间";
            this.dataColumn15.ColumnName = "FD_WEIGHTTIME";
            // 
            // dataColumn16
            // 
            this.dataColumn16.Caption = "班次";
            this.dataColumn16.ColumnName = "FS_SHIFT";
            // 
            // dataColumn17
            // 
            this.dataColumn17.Caption = "流向";
            this.dataColumn17.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn18
            // 
            this.dataColumn18.Caption = "发货单位";
            this.dataColumn18.ColumnName = "FS_SENDER";
            // 
            // dataColumn19
            // 
            this.dataColumn19.Caption = "收货单位";
            this.dataColumn19.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn20
            // 
            this.dataColumn20.Caption = "承运单位";
            this.dataColumn20.ColumnName = "FS_TRANSNO";
            // 
            // dataColumn51
            // 
            this.dataColumn51.Caption = "物料名称";
            this.dataColumn51.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn71
            // 
            this.dataColumn71.Caption = "是否异常";
            this.dataColumn71.ColumnName = "FS_YCSFYC";
            // 
            // dataColumn94
            // 
            this.dataColumn94.Caption = "发货库存点代码";
            this.dataColumn94.ColumnName = "FS_SENDERSTORE";
            // 
            // dataColumn95
            // 
            this.dataColumn95.Caption = "收货库存点代码";
            this.dataColumn95.ColumnName = "FS_RECEIVERSTORE";
            // 
            // dataColumn96
            // 
            this.dataColumn96.Caption = "磅房编号";
            this.dataColumn96.ColumnName = "FS_POUNDTYPE";
            // 
            // dataColumn97
            // 
            this.dataColumn97.Caption = "预报总重";
            this.dataColumn97.ColumnName = "FN_SENDGROSSWEIGHT";
            // 
            // dataColumn98
            // 
            this.dataColumn98.Caption = "预报皮重";
            this.dataColumn98.ColumnName = "FN_SENDTAREWEIGHT";
            // 
            // dataColumn99
            // 
            this.dataColumn99.Caption = "预报净量";
            this.dataColumn99.ColumnName = "FN_SENDNETWEIGHT";
            // 
            // dataColumn100
            // 
            this.dataColumn100.Caption = "班别";
            this.dataColumn100.ColumnName = "FS_TERM";
            // 
            // dataColumn102
            // 
            this.dataColumn102.Caption = "发货单位";
            this.dataColumn102.ColumnName = "FS_FHDW";
            // 
            // dataColumn101
            // 
            this.dataColumn101.Caption = "收货单位";
            this.dataColumn101.ColumnName = "FS_SHDW";
            // 
            // dataColumn103
            // 
            this.dataColumn103.Caption = "承运单位";
            this.dataColumn103.ColumnName = "FS_CYDW";
            // 
            // dataColumn104
            // 
            this.dataColumn104.Caption = "流向";
            this.dataColumn104.ColumnName = "FS_LX";
            // 
            // dataColumn105
            // 
            this.dataColumn105.Caption = "装车入库时间";
            this.dataColumn105.ColumnName = "FD_LOADINSTORETIME";
            // 
            // dataColumn106
            // 
            this.dataColumn106.Caption = "装车出库时间";
            this.dataColumn106.ColumnName = "FD_LOADOUTSTORETIME";
            // 
            // dataColumn107
            // 
            this.dataColumn107.ColumnName = "FS_UNLOADFLAG";
            // 
            // dataColumn108
            // 
            this.dataColumn108.ColumnName = "FS_UNLOADSTOREPERSON";
            // 
            // dataColumn109
            // 
            this.dataColumn109.ColumnName = "FS_LOADFLAG";
            // 
            // dataColumn110
            // 
            this.dataColumn110.ColumnName = "FS_LOADSTOREPERSON";
            // 
            // dataColumn111
            // 
            this.dataColumn111.ColumnName = "FS_SAMPLEPERSON";
            // 
            // dataColumn112
            // 
            this.dataColumn112.ColumnName = "FS_FIRSTLABELID";
            // 
            // dataColumn113
            // 
            this.dataColumn113.ColumnName = "FD_UNLOADINSTORETIME";
            // 
            // dataColumn114
            // 
            this.dataColumn114.ColumnName = "FD_UNLOADOUTSTORETIME";
            // 
            // dataColumn115
            // 
            this.dataColumn115.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn176
            // 
            this.dataColumn176.ColumnName = "FS_IFSAMPLING";
            // 
            // dataColumn177
            // 
            this.dataColumn177.ColumnName = "FS_IFACCEPT";
            // 
            // dataColumn178
            // 
            this.dataColumn178.ColumnName = "FS_DRIVERNAME";
            // 
            // dataColumn179
            // 
            this.dataColumn179.ColumnName = "FS_DRIVERIDCARD";
            // 
            // dataColumn180
            // 
            this.dataColumn180.ColumnName = "FD_SAMPLETIME";
            // 
            // dataColumn181
            // 
            this.dataColumn181.ColumnName = "FS_SAMPLEPLACE";
            // 
            // dataColumn182
            // 
            this.dataColumn182.ColumnName = "FS_SAMPLEFLAG";
            // 
            // dataColumn183
            // 
            this.dataColumn183.ColumnName = "FS_UNLOADPERSON";
            // 
            // dataColumn184
            // 
            this.dataColumn184.ColumnName = "FD_UNLOADTIME";
            // 
            // dataColumn185
            // 
            this.dataColumn185.ColumnName = "FS_UNLOADPLACE";
            // 
            // dataColumn186
            // 
            this.dataColumn186.ColumnName = "FS_CHECKPERSON";
            // 
            // dataColumn187
            // 
            this.dataColumn187.ColumnName = "FD_CHECKTIME";
            // 
            // dataColumn188
            // 
            this.dataColumn188.ColumnName = "FS_CHECKPLACE";
            // 
            // dataColumn189
            // 
            this.dataColumn189.ColumnName = "FS_CHECKFLAG";
            // 
            // dataColumn204
            // 
            this.dataColumn204.Caption = "应扣量";
            this.dataColumn204.ColumnName = "FS_YKL";
            // 
            // dataColumn205
            // 
            this.dataColumn205.Caption = "复磅标志";
            this.dataColumn205.ColumnName = "FS_REWEIGHTFLAG";
            // 
            // dataColumn206
            // 
            this.dataColumn206.Caption = "复磅确认时间";
            this.dataColumn206.ColumnName = "FD_REWEIGHTTIME";
            // 
            // dataColumn207
            // 
            this.dataColumn207.Caption = "复磅确认地点";
            this.dataColumn207.ColumnName = "FS_REWEIGHTPLACE";
            // 
            // dataColumn208
            // 
            this.dataColumn208.Caption = "复磅确认员";
            this.dataColumn208.ColumnName = "FS_REWEIGHTPERSON";
            // 
            // dataTable3
            // 
            this.dataTable3.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn21,
            this.dataColumn22,
            this.dataColumn92,
            this.dataColumn93});
            this.dataTable3.TableName = "语音表";
            // 
            // dataColumn21
            // 
            this.dataColumn21.Caption = "声音名称";
            this.dataColumn21.ColumnName = "FS_VOICENAME";
            // 
            // dataColumn22
            // 
            this.dataColumn22.Caption = "计量类型汽车衡";
            this.dataColumn22.ColumnName = "FS_INSTRTYPE";
            // 
            // dataColumn92
            // 
            this.dataColumn92.Caption = "描述";
            this.dataColumn92.ColumnName = "FS_MEMO";
            // 
            // dataColumn93
            // 
            this.dataColumn93.Caption = "声音文件内容";
            this.dataColumn93.ColumnName = "FS_VOICEFILE";
            this.dataColumn93.DataType = typeof(byte[]);
            // 
            // dataTable4
            // 
            this.dataTable4.Columns.AddRange(new System.Data.DataColumn[] {
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
            this.dataColumn53,
            this.dataColumn172,
            this.dataColumn173,
            this.dataColumn174,
            this.dataColumn175,
            this.dataColumn217,
            this.dataColumn218});
            this.dataTable4.TableName = "预报表";
            // 
            // dataColumn23
            // 
            this.dataColumn23.ColumnName = "FS_PLANCODE";
            // 
            // dataColumn24
            // 
            this.dataColumn24.ColumnName = "FS_CARDNUMBER";
            // 
            // dataColumn25
            // 
            this.dataColumn25.ColumnName = "FS_CARNO";
            // 
            // dataColumn26
            // 
            this.dataColumn26.ColumnName = "FS_CONTRACTNO";
            // 
            // dataColumn27
            // 
            this.dataColumn27.ColumnName = "FS_CONTRACTITEM";
            // 
            // dataColumn28
            // 
            this.dataColumn28.ColumnName = "FS_SENDER";
            // 
            // dataColumn29
            // 
            this.dataColumn29.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn30
            // 
            this.dataColumn30.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn31
            // 
            this.dataColumn31.ColumnName = "FS_RECEIVERFACTORY";
            // 
            // dataColumn32
            // 
            this.dataColumn32.ColumnName = "FS_TRANSNO";
            // 
            // dataColumn33
            // 
            this.dataColumn33.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn34
            // 
            this.dataColumn34.ColumnName = "FS_STOVENO";
            // 
            // dataColumn35
            // 
            this.dataColumn35.ColumnName = "FN_BILLETCOUNT";
            // 
            // dataColumn53
            // 
            this.dataColumn53.Caption = "期限皮重标志";
            this.dataColumn53.ColumnName = "FS_LEVEL";
            // 
            // dataColumn172
            // 
            this.dataColumn172.ColumnName = "FS_IFSAMPLING";
            // 
            // dataColumn173
            // 
            this.dataColumn173.ColumnName = "FS_IFACCEPT";
            // 
            // dataColumn174
            // 
            this.dataColumn174.ColumnName = "FS_DRIVERNAME";
            // 
            // dataColumn175
            // 
            this.dataColumn175.ColumnName = "FS_DRIVERIDCARD";
            // 
            // dataColumn217
            // 
            this.dataColumn217.Caption = "供应商";
            this.dataColumn217.ColumnName = "FS_PROVIDER";
            // 
            // dataColumn218
            // 
            this.dataColumn218.ColumnName = "FS_DRIVERREMARK";
            // 
            // dataTable5
            // 
            this.dataTable5.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn36,
            this.dataColumn37,
            this.dataColumn38,
            this.dataColumn39,
            this.dataColumn40,
            this.dataColumn41,
            this.dataColumn42,
            this.dataColumn43,
            this.dataColumn44,
            this.dataColumn45,
            this.dataColumn46,
            this.dataColumn47,
            this.dataColumn48,
            this.dataColumn49,
            this.dataColumn50});
            this.dataTable5.TableName = "图片表";
            // 
            // dataColumn36
            // 
            this.dataColumn36.Caption = "作业编号";
            this.dataColumn36.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn37
            // 
            this.dataColumn37.ColumnName = "FB_IMAGE1";
            // 
            // dataColumn38
            // 
            this.dataColumn38.ColumnName = "FB_IMAGE2";
            // 
            // dataColumn39
            // 
            this.dataColumn39.ColumnName = "FB_IMAGE3";
            // 
            // dataColumn40
            // 
            this.dataColumn40.ColumnName = "FB_IMAGE4";
            // 
            // dataColumn41
            // 
            this.dataColumn41.ColumnName = "FB_IMAGE5";
            // 
            // dataColumn42
            // 
            this.dataColumn42.ColumnName = "FB_IMAGE6";
            // 
            // dataColumn43
            // 
            this.dataColumn43.ColumnName = "FB_IMAGE7";
            // 
            // dataColumn44
            // 
            this.dataColumn44.ColumnName = "FB_IMAGE8";
            // 
            // dataColumn45
            // 
            this.dataColumn45.ColumnName = "FB_IMAGE9";
            // 
            // dataColumn46
            // 
            this.dataColumn46.ColumnName = "FB_IMAGE10";
            // 
            // dataColumn47
            // 
            this.dataColumn47.ColumnName = "FB_IMAGE11";
            // 
            // dataColumn48
            // 
            this.dataColumn48.ColumnName = "FB_IMAGE12";
            // 
            // dataColumn49
            // 
            this.dataColumn49.ColumnName = "FB_IMAGEYCJL";
            // 
            // dataColumn50
            // 
            this.dataColumn50.ColumnName = "FB_IMAGEECJL";
            // 
            // dataTable6
            // 
            this.dataTable6.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn54,
            this.dataColumn55,
            this.dataColumn56,
            this.dataColumn57,
            this.dataColumn58,
            this.dataColumn59,
            this.dataColumn60,
            this.dataColumn61,
            this.dataColumn62});
            this.dataTable6.TableName = "曲线图";
            // 
            // dataColumn54
            // 
            this.dataColumn54.ColumnName = "ZL1";
            this.dataColumn54.DataType = typeof(decimal);
            // 
            // dataColumn55
            // 
            this.dataColumn55.ColumnName = "ZL2";
            this.dataColumn55.DataType = typeof(decimal);
            // 
            // dataColumn56
            // 
            this.dataColumn56.ColumnName = "ZL3";
            this.dataColumn56.DataType = typeof(decimal);
            // 
            // dataColumn57
            // 
            this.dataColumn57.ColumnName = "ZL4";
            this.dataColumn57.DataType = typeof(decimal);
            // 
            // dataColumn58
            // 
            this.dataColumn58.ColumnName = "ZL5";
            this.dataColumn58.DataType = typeof(decimal);
            // 
            // dataColumn59
            // 
            this.dataColumn59.ColumnName = "ZL6";
            this.dataColumn59.DataType = typeof(decimal);
            // 
            // dataColumn60
            // 
            this.dataColumn60.ColumnName = "ZL7";
            this.dataColumn60.DataType = typeof(decimal);
            // 
            // dataColumn61
            // 
            this.dataColumn61.ColumnName = "ZL8";
            this.dataColumn61.DataType = typeof(decimal);
            // 
            // dataColumn62
            // 
            this.dataColumn62.ColumnName = "ZL9";
            this.dataColumn62.DataType = typeof(decimal);
            // 
            // dataTable7
            // 
            this.dataTable7.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn116,
            this.dataColumn117,
            this.dataColumn118,
            this.dataColumn119});
            this.dataTable7.TableName = "物料表";
            // 
            // dataColumn116
            // 
            this.dataColumn116.ColumnName = "FS_POINTNO";
            // 
            // dataColumn117
            // 
            this.dataColumn117.ColumnName = "FS_MATERIALNO";
            // 
            // dataColumn118
            // 
            this.dataColumn118.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn119
            // 
            this.dataColumn119.ColumnName = "FN_TIMES";
            // 
            // dataTable8
            // 
            this.dataTable8.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn120,
            this.dataColumn121,
            this.dataColumn122});
            this.dataTable8.TableName = "发货单位表";
            // 
            // dataColumn120
            // 
            this.dataColumn120.ColumnName = "FS_POINTNO";
            // 
            // dataColumn121
            // 
            this.dataColumn121.ColumnName = "FS_SUPPLIER";
            // 
            // dataColumn122
            // 
            this.dataColumn122.ColumnName = "FS_SUPPLIERNAME";
            // 
            // dataTable9
            // 
            this.dataTable9.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn123,
            this.dataColumn124,
            this.dataColumn125,
            this.dataColumn126});
            this.dataTable9.TableName = "收货单位表";
            // 
            // dataColumn123
            // 
            this.dataColumn123.ColumnName = "FS_POINTNO";
            // 
            // dataColumn124
            // 
            this.dataColumn124.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn125
            // 
            this.dataColumn125.ColumnName = "FS_MEMO";
            // 
            // dataColumn126
            // 
            this.dataColumn126.ColumnName = "FN_TIMES";
            // 
            // dataTable10
            // 
            this.dataTable10.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn127,
            this.dataColumn128});
            this.dataTable10.TableName = "承运单位表";
            // 
            // dataColumn127
            // 
            this.dataColumn127.ColumnName = "FS_TRANSNO";
            // 
            // dataColumn128
            // 
            this.dataColumn128.ColumnName = "FS_TRANSNAME";
            // 
            // dataTable11
            // 
            this.dataTable11.Columns.AddRange(new System.Data.DataColumn[] {
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
            this.dataColumn141,
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
            this.dataColumn155,
            this.dataColumn156,
            this.dataColumn157,
            this.dataColumn158,
            this.dataColumn159,
            this.dataColumn160,
            this.dataColumn161,
            this.dataColumn162,
            this.dataColumn163,
            this.dataColumn164,
            this.dataColumn165,
            this.dataColumn166,
            this.dataColumn167,
            this.dataColumn168,
            this.dataColumn169,
            this.dataColumn170,
            this.dataColumn190,
            this.dataColumn191,
            this.dataColumn192,
            this.dataColumn193,
            this.dataColumn194,
            this.dataColumn195,
            this.dataColumn196,
            this.dataColumn197,
            this.dataColumn198,
            this.dataColumn199,
            this.dataColumn200,
            this.dataColumn201,
            this.dataColumn202,
            this.dataColumn203,
            this.dataColumn209,
            this.dataColumn210,
            this.dataColumn211,
            this.dataColumn212,
            this.dataColumn213,
            this.dataColumn214,
            this.dataColumn215});
            this.dataTable11.TableName = "绑定一次计量表";
            // 
            // dataColumn129
            // 
            this.dataColumn129.Caption = "作业编号";
            this.dataColumn129.ColumnName = "FS_WEIGHTNO";
            // 
            // dataColumn130
            // 
            this.dataColumn130.Caption = "预报号";
            this.dataColumn130.ColumnName = "FS_PLANCODE";
            // 
            // dataColumn131
            // 
            this.dataColumn131.Caption = "卡号";
            this.dataColumn131.ColumnName = "FS_CARDNUMBER";
            // 
            // dataColumn132
            // 
            this.dataColumn132.Caption = "车号";
            this.dataColumn132.ColumnName = "FS_CARNO";
            // 
            // dataColumn133
            // 
            this.dataColumn133.Caption = "合同号";
            this.dataColumn133.ColumnName = "FS_CONTRACTNO";
            // 
            // dataColumn134
            // 
            this.dataColumn134.Caption = "合同项目编号";
            this.dataColumn134.ColumnName = "FS_CONTRACTITEM";
            // 
            // dataColumn135
            // 
            this.dataColumn135.Caption = "物资代码";
            this.dataColumn135.ColumnName = "FS_MATERIAL";
            // 
            // dataColumn136
            // 
            this.dataColumn136.Caption = "物料名称";
            this.dataColumn136.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn137
            // 
            this.dataColumn137.Caption = "流向代码";
            this.dataColumn137.ColumnName = "FS_WEIGHTTYPE";
            // 
            // dataColumn138
            // 
            this.dataColumn138.Caption = "流向";
            this.dataColumn138.ColumnName = "FS_LX";
            // 
            // dataColumn139
            // 
            this.dataColumn139.Caption = "发货单位代码";
            this.dataColumn139.ColumnName = "FS_SENDER";
            // 
            // dataColumn140
            // 
            this.dataColumn140.Caption = "发货单位";
            this.dataColumn140.ColumnName = "FS_FHDW";
            // 
            // dataColumn141
            // 
            this.dataColumn141.Caption = "发货库存点代码";
            this.dataColumn141.ColumnName = "FS_SENDERSTORE";
            // 
            // dataColumn142
            // 
            this.dataColumn142.Caption = "收货工厂代码";
            this.dataColumn142.ColumnName = "FS_RECEIVER";
            // 
            // dataColumn143
            // 
            this.dataColumn143.Caption = "收货单位";
            this.dataColumn143.ColumnName = "FS_SHDW";
            // 
            // dataColumn144
            // 
            this.dataColumn144.Caption = "承运方代码";
            this.dataColumn144.ColumnName = "FS_TRANSNO";
            // 
            // dataColumn145
            // 
            this.dataColumn145.Caption = "承运单位";
            this.dataColumn145.ColumnName = "FS_CYDW";
            // 
            // dataColumn146
            // 
            this.dataColumn146.Caption = "收货库存点代码";
            this.dataColumn146.ColumnName = "FS_RECEIVERSTORE";
            // 
            // dataColumn147
            // 
            this.dataColumn147.Caption = "磅房编号";
            this.dataColumn147.ColumnName = "FS_POUNDTYPE";
            // 
            // dataColumn148
            // 
            this.dataColumn148.Caption = "计量点";
            this.dataColumn148.ColumnName = "FS_POUND";
            // 
            // dataColumn149
            // 
            this.dataColumn149.Caption = "预报总重";
            this.dataColumn149.ColumnName = "FN_SENDGROSSWEIGHT";
            // 
            // dataColumn150
            // 
            this.dataColumn150.Caption = "预报皮重";
            this.dataColumn150.ColumnName = "FN_SENDTAREWEIGHT";
            // 
            // dataColumn151
            // 
            this.dataColumn151.Caption = "预报净量";
            this.dataColumn151.ColumnName = "FN_SENDNETWEIGHT";
            // 
            // dataColumn152
            // 
            this.dataColumn152.Caption = "重量";
            this.dataColumn152.ColumnName = "FN_WEIGHT";
            // 
            // dataColumn153
            // 
            this.dataColumn153.Caption = "计量员";
            this.dataColumn153.ColumnName = "FS_WEIGHTER";
            // 
            // dataColumn154
            // 
            this.dataColumn154.Caption = "计量时间";
            this.dataColumn154.ColumnName = "FD_WEIGHTTIME";
            // 
            // dataColumn155
            // 
            this.dataColumn155.Caption = "班次";
            this.dataColumn155.ColumnName = "FS_SHIFT";
            // 
            // dataColumn156
            // 
            this.dataColumn156.Caption = "班别";
            this.dataColumn156.ColumnName = "FS_TERM";
            // 
            // dataColumn157
            // 
            this.dataColumn157.ColumnName = "FD_LOADINSTORETIME";
            // 
            // dataColumn158
            // 
            this.dataColumn158.ColumnName = "FD_LOADOUTSTORETIME";
            // 
            // dataColumn159
            // 
            this.dataColumn159.ColumnName = "FS_UNLOADFLAG";
            // 
            // dataColumn160
            // 
            this.dataColumn160.ColumnName = "FS_UNLOADSTOREPERSON";
            // 
            // dataColumn161
            // 
            this.dataColumn161.ColumnName = "FS_LOADFLAG";
            // 
            // dataColumn162
            // 
            this.dataColumn162.ColumnName = "FS_LOADSTOREPERSON";
            // 
            // dataColumn163
            // 
            this.dataColumn163.ColumnName = "FS_SAMPLEPERSON";
            // 
            // dataColumn164
            // 
            this.dataColumn164.ColumnName = "FS_FIRSTLABELID";
            // 
            // dataColumn165
            // 
            this.dataColumn165.ColumnName = "FD_UNLOADINSTORETIME";
            // 
            // dataColumn166
            // 
            this.dataColumn166.ColumnName = "FD_UNLOADOUTSTORETIME";
            // 
            // dataColumn167
            // 
            this.dataColumn167.Caption = "是否异常";
            this.dataColumn167.ColumnName = "FS_YCSFYC";
            // 
            // dataColumn168
            // 
            this.dataColumn168.Caption = "应扣量";
            this.dataColumn168.ColumnName = "FS_YKL";
            // 
            // dataColumn169
            // 
            this.dataColumn169.Caption = "炉号";
            this.dataColumn169.ColumnName = "FS_STOVENO";
            // 
            // dataColumn170
            // 
            this.dataColumn170.Caption = "支数";
            this.dataColumn170.ColumnName = "FN_COUNT";
            // 
            // dataColumn190
            // 
            this.dataColumn190.Caption = "取样时间";
            this.dataColumn190.ColumnName = "FD_SAMPLETIME";
            // 
            // dataColumn191
            // 
            this.dataColumn191.Caption = "取样点";
            this.dataColumn191.ColumnName = "FS_SAMPLEPLACE";
            // 
            // dataColumn192
            // 
            this.dataColumn192.Caption = "取样确认";
            this.dataColumn192.ColumnName = "FS_SAMPLEFLAG";
            // 
            // dataColumn193
            // 
            this.dataColumn193.Caption = "卸车员";
            this.dataColumn193.ColumnName = "FS_UNLOADPERSON";
            // 
            // dataColumn194
            // 
            this.dataColumn194.Caption = "卸车时间";
            this.dataColumn194.ColumnName = "FD_UNLOADTIME";
            // 
            // dataColumn195
            // 
            this.dataColumn195.Caption = "卸车点";
            this.dataColumn195.ColumnName = "FS_UNLOADPLACE";
            // 
            // dataColumn196
            // 
            this.dataColumn196.Caption = "验收员";
            this.dataColumn196.ColumnName = "FS_CHECKPERSON";
            // 
            // dataColumn197
            // 
            this.dataColumn197.Caption = "验收时间";
            this.dataColumn197.ColumnName = "FD_CHECKTIME";
            // 
            // dataColumn198
            // 
            this.dataColumn198.Caption = "验收点";
            this.dataColumn198.ColumnName = "FS_CHECKPLACE";
            // 
            // dataColumn199
            // 
            this.dataColumn199.Caption = "验收确认";
            this.dataColumn199.ColumnName = "FS_CHECKFLAG";
            // 
            // dataColumn200
            // 
            this.dataColumn200.Caption = "是否取样需要";
            this.dataColumn200.ColumnName = "FS_IFSAMPLING";
            // 
            // dataColumn201
            // 
            this.dataColumn201.Caption = "是否需要验收 ";
            this.dataColumn201.ColumnName = "FS_IFACCEPT";
            // 
            // dataColumn202
            // 
            this.dataColumn202.Caption = "驾驶员姓名";
            this.dataColumn202.ColumnName = "FS_DRIVERNAME";
            // 
            // dataColumn203
            // 
            this.dataColumn203.Caption = "驾驶员身份证";
            this.dataColumn203.ColumnName = "FS_DRIVERIDCARD";
            // 
            // dataColumn209
            // 
            this.dataColumn209.Caption = "复磅标记";
            this.dataColumn209.ColumnName = "FS_REWEIGHTFLAG";
            // 
            // dataColumn210
            // 
            this.dataColumn210.Caption = "复磅确认时间";
            this.dataColumn210.ColumnName = "FD_REWEIGHTTIME";
            // 
            // dataColumn211
            // 
            this.dataColumn211.Caption = "复磅确认地点";
            this.dataColumn211.ColumnName = "FS_REWEIGHTPLACE";
            // 
            // dataColumn212
            // 
            this.dataColumn212.Caption = "复磅确认员";
            this.dataColumn212.ColumnName = "FS_REWEIGHTPERSON";
            // 
            // dataColumn213
            // 
            this.dataColumn213.Caption = "对方净重";
            this.dataColumn213.ColumnName = "FS_DFJZ";
            // 
            // dataColumn214
            // 
            this.dataColumn214.Caption = "单据编号";
            this.dataColumn214.ColumnName = "FS_BILLNUMBER";
            // 
            // dataColumn215
            // 
            this.dataColumn215.Caption = "应扣比例";
            this.dataColumn215.ColumnName = "FS_YKBL";
            // 
            // ultraTabPageControl2
            // 
            this.ultraTabPageControl2.Controls.Add(this.ultraChart1);
            this.ultraTabPageControl2.Controls.Add(this.picHT);
            this.coreBind.SetDatabasecommand(this.ultraTabPageControl2, null);
            this.ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabPageControl2.Name = "ultraTabPageControl2";
            this.ultraTabPageControl2.Size = new System.Drawing.Size(741, 153);
            this.coreBind.SetVerification(this.ultraTabPageControl2, null);
            // 
            //			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
            //			'ChartType' must be persisted ahead of any Axes change made in design time.
            //		
            this.ultraChart1.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.LineChart;
            // 
            // ultraChart1
            // 
            this.ultraChart1.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement1.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ultraChart1.Axis.PE = paintElement1;
            this.ultraChart1.Axis.X.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X.Labels.Visible = false;
            this.ultraChart1.Axis.X.LineThickness = 1;
            this.ultraChart1.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X.MajorGridLines.Visible = false;
            this.ultraChart1.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.X.TickmarkIntervalType = Infragistics.UltraChart.Shared.Styles.AxisIntervalType.Hours;
            this.ultraChart1.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.X.Visible = false;
            this.ultraChart1.Axis.X2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X2.Labels.Visible = false;
            this.ultraChart1.Axis.X2.LineThickness = 1;
            this.ultraChart1.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.X2.TickmarkIntervalType = Infragistics.UltraChart.Shared.Styles.AxisIntervalType.Hours;
            this.ultraChart1.Axis.X2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.X2.Visible = false;
            this.ultraChart1.Axis.Y.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart1.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y.LineThickness = 1;
            this.ultraChart1.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Y.TickmarkInterval = 50;
            this.ultraChart1.Axis.Y.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Y.Visible = true;
            this.ultraChart1.Axis.Y2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart1.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y2.Labels.Visible = false;
            this.ultraChart1.Axis.Y2.LineThickness = 1;
            this.ultraChart1.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Y2.TickmarkInterval = 50;
            this.ultraChart1.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Y2.Visible = false;
            this.ultraChart1.Axis.Z.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z.Labels.Visible = false;
            this.ultraChart1.Axis.Z.LineThickness = 1;
            this.ultraChart1.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Z.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Z.Visible = false;
            this.ultraChart1.Axis.Z2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z2.Labels.Visible = false;
            this.ultraChart1.Axis.Z2.LineThickness = 1;
            this.ultraChart1.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Z2.Visible = false;
            this.ultraChart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ultraChart1.ColorModel.AlphaLevel = ((byte)(150));
            this.ultraChart1.ColorModel.ColorBegin = System.Drawing.Color.Pink;
            this.ultraChart1.ColorModel.ColorEnd = System.Drawing.Color.DarkRed;
            this.ultraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            this.coreBind.SetDatabasecommand(this.ultraChart1, null);
            this.ultraChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraChart1.Effects.Effects.Add(gradientEffect1);
            this.ultraChart1.Location = new System.Drawing.Point(0, 0);
            this.ultraChart1.Name = "ultraChart1";
            this.ultraChart1.Size = new System.Drawing.Size(741, 153);
            this.ultraChart1.TabIndex = 3;
            this.ultraChart1.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            this.coreBind.SetVerification(this.ultraChart1, null);
            this.ultraChart1.ChartDataClicked += new Infragistics.UltraChart.Shared.Events.ChartDataClickedEventHandler(this.ultraChart1_ChartDataClicked);
            // 
            // picHT
            // 
            this.coreBind.SetDatabasecommand(this.picHT, null);
            this.picHT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picHT.Location = new System.Drawing.Point(0, 0);
            this.picHT.Name = "picHT";
            this.picHT.Size = new System.Drawing.Size(741, 153);
            this.picHT.TabIndex = 1;
            this.picHT.TabStop = false;
            this.coreBind.SetVerification(this.picHT, null);
            // 
            // panelYYBF
            // 
            this.panelYYBF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panelYYBF.Controls.Add(this.panel23);
            this.panelYYBF.Controls.Add(this.btn5);
            this.panelYYBF.Controls.Add(this.btn6);
            this.panelYYBF.Controls.Add(this.btn4);
            this.panelYYBF.Controls.Add(this.btn3);
            this.panelYYBF.Controls.Add(this.btn2);
            this.panelYYBF.Controls.Add(this.btn1);
            this.panelYYBF.Controls.Add(this.ultraGrid4);
            this.coreBind.SetDatabasecommand(this.panelYYBF, null);
            this.panelYYBF.Location = new System.Drawing.Point(0, 28);
            this.panelYYBF.Name = "panelYYBF";
            this.panelYYBF.Size = new System.Drawing.Size(136, 579);
            this.panelYYBF.TabIndex = 0;
            this.panelYYBF.Tag = "";
            this.coreBind.SetVerification(this.panelYYBF, null);
            // 
            // panel23
            // 
            this.panel23.Controls.Add(this.ultraGrid5);
            this.coreBind.SetDatabasecommand(this.panel23, null);
            this.panel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel23.Location = new System.Drawing.Point(0, 0);
            this.panel23.Name = "panel23";
            this.panel23.Size = new System.Drawing.Size(136, 579);
            this.panel23.TabIndex = 33;
            this.coreBind.SetVerification(this.panel23, null);
            // 
            // ultraGrid5
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid5, null);
            this.ultraGrid5.DataMember = "语音表";
            this.ultraGrid5.DataSource = this.dataSet1;
            appearance13.BackColor = System.Drawing.Color.White;
            appearance13.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid5.DisplayLayout.Appearance = appearance13;
            ultraGridColumn64.Header.VisiblePosition = 0;
            ultraGridColumn65.Header.VisiblePosition = 1;
            ultraGridColumn65.Hidden = true;
            ultraGridColumn66.Header.VisiblePosition = 2;
            ultraGridColumn66.Hidden = true;
            ultraGridColumn67.Header.VisiblePosition = 3;
            ultraGridColumn67.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66,
            ultraGridColumn67});
            this.ultraGrid5.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.ultraGrid5.DisplayLayout.InterBandSpacing = 10;
            appearance14.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid5.DisplayLayout.Override.CardAreaAppearance = appearance14;
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance15.BackColor2 = System.Drawing.Color.White;
            appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance15.FontData.SizeInPoints = 11F;
            appearance15.FontData.UnderlineAsString = "False";
            appearance15.ForeColor = System.Drawing.Color.Black;
            appearance15.TextHAlignAsString = "Center";
            appearance15.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid5.DisplayLayout.Override.HeaderAppearance = appearance15;
            appearance16.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid5.DisplayLayout.Override.RowAppearance = appearance16;
            appearance17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance17.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorAppearance = appearance17;
            this.ultraGrid5.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid5.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance18.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance18.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid5.DisplayLayout.Override.SelectedRowAppearance = appearance18;
            this.ultraGrid5.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid5.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid5.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid5.Name = "ultraGrid5";
            this.ultraGrid5.Size = new System.Drawing.Size(136, 579);
            this.ultraGrid5.TabIndex = 3;
            this.coreBind.SetVerification(this.ultraGrid5, null);
            this.ultraGrid5.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGrid5_ClickCell);
            // 
            // btn5
            // 
            this.btn5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btn5, null);
            this.btn5.Location = new System.Drawing.Point(22, 166);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(94, 28);
            this.btn5.TabIndex = 13;
            this.btn5.Text = "称重完成";
            this.btn5.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btn5, null);
            // 
            // btn6
            // 
            this.btn6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btn6, null);
            this.btn6.Location = new System.Drawing.Point(22, 200);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(94, 39);
            this.btn6.TabIndex = 12;
            this.btn6.Text = "此车无预报请离开秤台";
            this.btn6.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btn6, null);
            // 
            // btn4
            // 
            this.btn4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btn4, null);
            this.btn4.Location = new System.Drawing.Point(7, 132);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(120, 28);
            this.btn4.TabIndex = 11;
            this.btn4.Text = "单据放倒请放正";
            this.btn4.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btn4, null);
            // 
            // btn3
            // 
            this.btn3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btn3, null);
            this.btn3.Location = new System.Drawing.Point(19, 87);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(99, 39);
            this.btn3.TabIndex = 10;
            this.btn3.Text = " 超出停车线   请后退到停车线";
            this.btn3.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btn3, null);
            // 
            // btn2
            // 
            this.btn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btn2, null);
            this.btn2.Location = new System.Drawing.Point(7, 54);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(122, 28);
            this.btn2.TabIndex = 9;
            this.btn2.Text = "请放第一次临时磅单";
            this.btn2.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btn2, null);
            // 
            // btn1
            // 
            this.btn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btn1, null);
            this.btn1.Location = new System.Drawing.Point(22, 20);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(94, 28);
            this.btn1.TabIndex = 8;
            this.btn1.Text = "请报车号";
            this.btn1.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btn1, null);
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // ultraGrid4
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid4, null);
            this.ultraGrid4.DataMember = "语音表";
            this.ultraGrid4.DataSource = this.dataSet1;
            appearance7.BackColor = System.Drawing.Color.White;
            appearance7.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid4.DisplayLayout.Appearance = appearance7;
            ultraGridColumn68.Header.VisiblePosition = 0;
            ultraGridColumn69.Header.VisiblePosition = 1;
            ultraGridColumn70.Header.VisiblePosition = 2;
            ultraGridColumn71.Header.VisiblePosition = 3;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn68,
            ultraGridColumn69,
            ultraGridColumn70,
            ultraGridColumn71});
            this.ultraGrid4.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.ultraGrid4.DisplayLayout.InterBandSpacing = 10;
            appearance8.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid4.DisplayLayout.Override.CardAreaAppearance = appearance8;
            appearance9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance9.BackColor2 = System.Drawing.Color.White;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance9.FontData.SizeInPoints = 11F;
            appearance9.FontData.UnderlineAsString = "False";
            appearance9.ForeColor = System.Drawing.Color.Black;
            appearance9.TextHAlignAsString = "Center";
            appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid4.DisplayLayout.Override.HeaderAppearance = appearance9;
            appearance10.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid4.DisplayLayout.Override.RowAppearance = appearance10;
            appearance11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance11.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid4.DisplayLayout.Override.RowSelectorAppearance = appearance11;
            this.ultraGrid4.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid4.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance12.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance12.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid4.DisplayLayout.Override.SelectedRowAppearance = appearance12;
            this.ultraGrid4.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid4.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid4.Location = new System.Drawing.Point(0, 469);
            this.ultraGrid4.Name = "ultraGrid4";
            this.ultraGrid4.Size = new System.Drawing.Size(109, 157);
            this.ultraGrid4.TabIndex = 3;
            this.coreBind.SetVerification(this.ultraGrid4, null);
            this.ultraGrid4.Visible = false;
            // 
            // panelSPKZ
            // 
            this.panelSPKZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panelSPKZ.Controls.Add(this.button5);
            this.panelSPKZ.Controls.Add(this.button6);
            this.panelSPKZ.Controls.Add(this.button7);
            this.panelSPKZ.Controls.Add(this.button8);
            this.panelSPKZ.Controls.Add(this.button4);
            this.panelSPKZ.Controls.Add(this.button3);
            this.panelSPKZ.Controls.Add(this.button2);
            this.panelSPKZ.Controls.Add(this.button1);
            this.panelSPKZ.Controls.Add(this.button15);
            this.panelSPKZ.Controls.Add(this.button14);
            this.panelSPKZ.Controls.Add(this.button13);
            this.panelSPKZ.Controls.Add(this.button12);
            this.panelSPKZ.Controls.Add(this.button11);
            this.panelSPKZ.Controls.Add(this.button10);
            this.coreBind.SetDatabasecommand(this.panelSPKZ, null);
            this.panelSPKZ.Location = new System.Drawing.Point(0, 28);
            this.panelSPKZ.Name = "panelSPKZ";
            this.panelSPKZ.Size = new System.Drawing.Size(131, 579);
            this.panelSPKZ.TabIndex = 1;
            this.coreBind.SetVerification(this.panelSPKZ, null);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button5, null);
            this.button5.Location = new System.Drawing.Point(68, 285);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(55, 28);
            this.button5.TabIndex = 20;
            this.button5.Tag = "7";
            this.button5.Text = "调焦-";
            this.button5.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button5, null);
            this.button5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button6, null);
            this.button6.Location = new System.Drawing.Point(7, 285);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(55, 28);
            this.button6.TabIndex = 19;
            this.button6.Tag = "6";
            this.button6.Text = "调焦+";
            this.button6.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button6, null);
            this.button6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button7, null);
            this.button7.Location = new System.Drawing.Point(68, 245);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(55, 28);
            this.button7.TabIndex = 18;
            this.button7.Tag = "5";
            this.button7.Text = "变倍-";
            this.button7.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button7, null);
            this.button7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button8, null);
            this.button8.Location = new System.Drawing.Point(7, 245);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(55, 28);
            this.button8.TabIndex = 17;
            this.button8.Tag = "4";
            this.button8.Text = "变倍+";
            this.button8.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button8, null);
            this.button8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button4, null);
            this.button4.Location = new System.Drawing.Point(68, 196);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(55, 28);
            this.button4.TabIndex = 16;
            this.button4.Tag = "35";
            this.button4.Text = "右下";
            this.button4.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button4, null);
            this.button4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button3, null);
            this.button3.Location = new System.Drawing.Point(7, 196);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(55, 28);
            this.button3.TabIndex = 15;
            this.button3.Tag = "34";
            this.button3.Text = "左下";
            this.button3.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button3, null);
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button2, null);
            this.button2.Location = new System.Drawing.Point(68, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 28);
            this.button2.TabIndex = 14;
            this.button2.Tag = "33";
            this.button2.Text = "右上";
            this.button2.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button2, null);
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button1, null);
            this.button1.Location = new System.Drawing.Point(7, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 28);
            this.button1.TabIndex = 13;
            this.button1.Tag = "32";
            this.button1.Text = "左上";
            this.button1.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button1, null);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button15
            // 
            this.button15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button15, null);
            this.button15.Location = new System.Drawing.Point(68, 326);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(55, 28);
            this.button15.TabIndex = 12;
            this.button15.Tag = "9";
            this.button15.Text = "光圈-";
            this.button15.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button15, null);
            this.button15.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button15.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button14
            // 
            this.button14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button14, null);
            this.button14.Location = new System.Drawing.Point(7, 326);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(55, 28);
            this.button14.TabIndex = 11;
            this.button14.Tag = "8";
            this.button14.Text = "光圈+";
            this.button14.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button14, null);
            this.button14.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button14.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button13, null);
            this.button13.Location = new System.Drawing.Point(69, 67);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(55, 28);
            this.button13.TabIndex = 10;
            this.button13.Tag = "3";
            this.button13.Text = "右";
            this.button13.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button13, null);
            this.button13.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button13.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button12, null);
            this.button12.Location = new System.Drawing.Point(7, 66);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(55, 28);
            this.button12.TabIndex = 9;
            this.button12.Tag = "2";
            this.button12.Text = "左";
            this.button12.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button12, null);
            this.button12.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button12.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button11, null);
            this.button11.Location = new System.Drawing.Point(37, 105);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(55, 28);
            this.button11.TabIndex = 8;
            this.button11.Tag = "1";
            this.button11.Text = "下";
            this.button11.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button11, null);
            this.button11.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button11.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button10, null);
            this.button10.Location = new System.Drawing.Point(37, 28);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(55, 28);
            this.button10.TabIndex = 7;
            this.button10.Tag = "0";
            this.button10.Text = "上";
            this.button10.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button10, null);
            this.button10.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Down);
            this.button10.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlButton_Up);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chb_AutoInfrared);
            this.panel1.Controls.Add(this.txtZZ);
            this.panel1.Controls.Add(this.panel1_Fill_Panel);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Left);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Right);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Top);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Bottom);
            this.coreBind.SetDatabasecommand(this.panel1, null);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 27);
            this.panel1.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1, null);
            // 
            // chb_AutoInfrared
            // 
            this.chb_AutoInfrared.AutoSize = true;
            this.chb_AutoInfrared.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.coreBind.SetDatabasecommand(this.chb_AutoInfrared, null);
            this.chb_AutoInfrared.Location = new System.Drawing.Point(555, 5);
            this.chb_AutoInfrared.Name = "chb_AutoInfrared";
            this.chb_AutoInfrared.Size = new System.Drawing.Size(96, 16);
            this.chb_AutoInfrared.TabIndex = 604;
            this.chb_AutoInfrared.Text = "自动红外检测";
            this.chb_AutoInfrared.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.chb_AutoInfrared, null);
            // 
            // txtZZ
            // 
            this.coreBind.SetDatabasecommand(this.txtZZ, null);
            this.txtZZ.Location = new System.Drawing.Point(142, 3);
            this.txtZZ.Name = "txtZZ";
            this.txtZZ.ReadOnly = true;
            this.txtZZ.Size = new System.Drawing.Size(51, 21);
            this.txtZZ.TabIndex = 0;
            this.coreBind.SetVerification(this.txtZZ, null);
            // 
            // panel1_Fill_Panel
            // 
            this.panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.coreBind.SetDatabasecommand(this.panel1_Fill_Panel, null);
            this.panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1_Fill_Panel.Location = new System.Drawing.Point(0, 26);
            this.panel1_Fill_Panel.Name = "panel1_Fill_Panel";
            this.panel1_Fill_Panel.Size = new System.Drawing.Size(1007, 1);
            this.panel1_Fill_Panel.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1_Fill_Panel, null);
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
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this.panel1;
            this.ultraToolbarsManager1.LockToolbars = true;
            this.ultraToolbarsManager1.ShowFullMenusDelay = 500;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2003;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            ultraToolbar1.FloatingLocation = new System.Drawing.Point(392, 382);
            ultraToolbar1.FloatingSize = new System.Drawing.Size(486, 44);
            controlContainerTool3.ControlName = "txtZZ";
            controlContainerTool3.InstanceProps.IsFirstInGroup = true;
            controlContainerTool3.InstanceProps.Width = 122;
            buttonTool19.InstanceProps.IsFirstInGroup = true;
            buttonTool20.InstanceProps.IsFirstInGroup = true;
            buttonTool21.InstanceProps.IsFirstInGroup = true;
            controlContainerTool1.ControlName = "chb_AutoInfrared";
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool8,
            controlContainerTool3,
            buttonTool17,
            buttonTool19,
            buttonTool20,
            buttonTool21,
            controlContainerTool1,
            buttonTool22});
            ultraToolbar1.Text = "UltraToolbar1";
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            buttonTool23.SharedPropsInternal.Caption = "打开对讲";
            buttonTool23.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            controlContainerTool6.ControlName = "txtZZ";
            controlContainerTool6.SharedPropsInternal.Caption = "剩余纸张数";
            controlContainerTool6.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool6.SharedPropsInternal.Width = 122;
            buttonTool24.SharedPropsInternal.Caption = "换纸";
            buttonTool24.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool26.SharedPropsInternal.Caption = "切换视频";
            buttonTool26.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool27.SharedPropsInternal.Caption = "查看/关闭一次计量图像";
            buttonTool27.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool28.SharedPropsInternal.Caption = "磅单打印";
            buttonTool28.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool29.SharedPropsInternal.Caption = "校秤";
            buttonTool29.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.TextOnlyAlways;
            buttonTool30.SharedPropsInternal.Caption = "钢坯查询";
            buttonTool30.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            controlContainerTool2.ControlName = "chb_AutoInfrared";
            controlContainerTool2.SharedPropsInternal.Caption = "ControlContainerTool1";
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool23,
            controlContainerTool6,
            buttonTool24,
            buttonTool26,
            buttonTool27,
            buttonTool28,
            buttonTool29,
            buttonTool30,
            controlContainerTool2});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
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
            // txtTDL
            // 
            this.coreBind.SetDatabasecommand(this.txtTDL, null);
            this.txtTDL.Location = new System.Drawing.Point(38, 33);
            this.txtTDL.Name = "txtTDL";
            this.txtTDL.ReadOnly = true;
            this.txtTDL.Size = new System.Drawing.Size(51, 21);
            this.txtTDL.TabIndex = 5;
            this.coreBind.SetVerification(this.txtTDL, null);
            this.txtTDL.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel2.Controls.Add(this.panel3);
            this.coreBind.SetDatabasecommand(this.panel2, null);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(264, 580);
            this.panel2.TabIndex = 1;
            this.coreBind.SetVerification(this.panel2, null);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.VideoChannel8);
            this.panel3.Controls.Add(this.VideoChannel7);
            this.panel3.Controls.Add(this.VideoChannel3);
            this.panel3.Controls.Add(this.VideoChannel2);
            this.panel3.Controls.Add(this.VideoChannel1);
            this.coreBind.SetDatabasecommand(this.panel3, null);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(264, 580);
            this.panel3.TabIndex = 0;
            this.coreBind.SetVerification(this.panel3, null);
            // 
            // VideoChannel8
            // 
            this.VideoChannel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel8, null);
            this.VideoChannel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.VideoChannel8.Location = new System.Drawing.Point(0, 772);
            this.VideoChannel8.Name = "VideoChannel8";
            this.VideoChannel8.Size = new System.Drawing.Size(245, 193);
            this.VideoChannel8.TabIndex = 4;
            this.VideoChannel8.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel8, null);
            this.VideoChannel8.Click += new System.EventHandler(this.VideoChannel8_Click);
            // 
            // VideoChannel7
            // 
            this.VideoChannel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel7, null);
            this.VideoChannel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.VideoChannel7.Location = new System.Drawing.Point(0, 579);
            this.VideoChannel7.Name = "VideoChannel7";
            this.VideoChannel7.Size = new System.Drawing.Size(245, 193);
            this.VideoChannel7.TabIndex = 3;
            this.VideoChannel7.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel7, null);
            this.VideoChannel7.Click += new System.EventHandler(this.VideoChannel7_Click);
            // 
            // VideoChannel3
            // 
            this.VideoChannel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel3, null);
            this.VideoChannel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.VideoChannel3.Location = new System.Drawing.Point(0, 386);
            this.VideoChannel3.Name = "VideoChannel3";
            this.VideoChannel3.Size = new System.Drawing.Size(245, 193);
            this.VideoChannel3.TabIndex = 2;
            this.VideoChannel3.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel3, null);
            this.VideoChannel3.Click += new System.EventHandler(this.VideoChannel3_Click);
            // 
            // VideoChannel2
            // 
            this.VideoChannel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel2, null);
            this.VideoChannel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.VideoChannel2.Location = new System.Drawing.Point(0, 193);
            this.VideoChannel2.Name = "VideoChannel2";
            this.VideoChannel2.Size = new System.Drawing.Size(245, 193);
            this.VideoChannel2.TabIndex = 1;
            this.VideoChannel2.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel2, null);
            this.VideoChannel2.Click += new System.EventHandler(this.VideoChannel2_Click);
            // 
            // VideoChannel1
            // 
            this.VideoChannel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coreBind.SetDatabasecommand(this.VideoChannel1, null);
            this.VideoChannel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.VideoChannel1.Location = new System.Drawing.Point(0, 0);
            this.VideoChannel1.Name = "VideoChannel1";
            this.VideoChannel1.Size = new System.Drawing.Size(245, 193);
            this.VideoChannel1.TabIndex = 0;
            this.VideoChannel1.TabStop = false;
            this.coreBind.SetVerification(this.VideoChannel1, null);
            this.VideoChannel1.Click += new System.EventHandler(this.VideoChannel1_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.ultraTabControl1);
            this.coreBind.SetDatabasecommand(this.panel6, null);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(264, 427);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(743, 180);
            this.panel6.TabIndex = 2;
            this.coreBind.SetVerification(this.panel6, null);
            // 
            // ultraTabControl1
            // 
            this.ultraTabControl1.Controls.Add(this.ultraTabSharedControlsPage1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl1);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl2);
            this.ultraTabControl1.Controls.Add(this.ultraTabPageControl3);
            this.coreBind.SetDatabasecommand(this.ultraTabControl1, null);
            this.ultraTabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraTabControl1.Location = new System.Drawing.Point(0, 6);
            this.ultraTabControl1.Name = "ultraTabControl1";
            this.ultraTabControl1.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.ultraTabControl1.Size = new System.Drawing.Size(743, 174);
            this.ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Flat;
            this.ultraTabControl1.TabIndex = 0;
            ultraTab2.TabPage = this.ultraTabPageControl3;
            ultraTab2.Text = "计量点图像";
            ultraTab1.TabPage = this.ultraTabPageControl1;
            ultraTab1.Text = "计量称重信息";
            ultraTab3.TabPage = this.ultraTabPageControl2;
            ultraTab3.Text = "重量曲线图";
            this.ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab2,
            ultraTab1,
            ultraTab3});
            this.coreBind.SetVerification(this.ultraTabControl1, null);
            // 
            // ultraTabSharedControlsPage1
            // 
            this.coreBind.SetDatabasecommand(this.ultraTabSharedControlsPage1, null);
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(741, 153);
            this.coreBind.SetVerification(this.ultraTabSharedControlsPage1, null);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel7.Controls.Add(this.txtTDL);
            this.panel7.Controls.Add(this.button9);
            this.panel7.Controls.Add(this.textBox1);
            this.panel7.Controls.Add(this.txtXSZL);
            this.panel7.Controls.Add(this.groupBox1);
            this.panel7.Controls.Add(this.btnQL);
            this.panel7.Controls.Add(this.lbWD);
            this.panel7.Controls.Add(this.lbYS);
            this.panel7.Controls.Add(this.label2);
            this.coreBind.SetDatabasecommand(this.panel7, null);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(264, 27);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(743, 97);
            this.panel7.TabIndex = 3;
            this.coreBind.SetVerification(this.panel7, null);
            // 
            // button9
            // 
            this.coreBind.SetDatabasecommand(this.button9, null);
            this.button9.Location = new System.Drawing.Point(306, 82);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 572;
            this.button9.Text = "Test";
            this.button9.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button9, null);
            this.button9.Visible = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // textBox1
            // 
            this.coreBind.SetDatabasecommand(this.textBox1, null);
            this.textBox1.Location = new System.Drawing.Point(265, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(43, 21);
            this.textBox1.TabIndex = 571;
            this.coreBind.SetVerification(this.textBox1, null);
            this.textBox1.Visible = false;
            // 
            // txtXSZL
            // 
            this.txtXSZL.BackColor = System.Drawing.SystemColors.WindowText;
            this.coreBind.SetDatabasecommand(this.txtXSZL, null);
            this.txtXSZL.Font = new System.Drawing.Font("宋体", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtXSZL.ForeColor = System.Drawing.Color.Lime;
            this.txtXSZL.Location = new System.Drawing.Point(14, 18);
            this.txtXSZL.MaxLength = 8;
            this.txtXSZL.Multiline = true;
            this.txtXSZL.Name = "txtXSZL";
            this.txtXSZL.Size = new System.Drawing.Size(238, 68);
            this.txtXSZL.TabIndex = 570;
            this.txtXSZL.Text = "0.00";
            this.txtXSZL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.coreBind.SetVerification(this.txtXSZL, null);
            this.txtXSZL.TextChanged += new System.EventHandler(this.txtXSZL_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StatusBack);
            this.groupBox1.Controls.Add(this.StatusFront);
            this.groupBox1.Controls.Add(this.StatusLight);
            this.groupBox1.Controls.Add(this.StatusRedGreen);
            this.groupBox1.Controls.Add(this.btnHDHW);
            this.groupBox1.Controls.Add(this.btnQDHW);
            this.groupBox1.Controls.Add(this.btnZMDKG);
            this.groupBox1.Controls.Add(this.btnHL);
            this.coreBind.SetDatabasecommand(this.groupBox1, null);
            this.groupBox1.Location = new System.Drawing.Point(413, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 89);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制";
            this.coreBind.SetVerification(this.groupBox1, null);
            // 
            // StatusBack
            // 
            this.StatusBack.Connected = true;
            this.coreBind.SetDatabasecommand(this.StatusBack, null);
            this.StatusBack.LineWidth = 4;
            this.StatusBack.Location = new System.Drawing.Point(144, 53);
            this.StatusBack.Name = "StatusBack";
            this.StatusBack.Size = new System.Drawing.Size(32, 32);
            this.StatusBack.TabIndex = 17;
            this.StatusBack.Text = "coolInfraredRay2";
            this.coreBind.SetVerification(this.StatusBack, null);
            // 
            // StatusFront
            // 
            this.StatusFront.Connected = true;
            this.coreBind.SetDatabasecommand(this.StatusFront, null);
            this.StatusFront.LineWidth = 4;
            this.StatusFront.Location = new System.Drawing.Point(144, 14);
            this.StatusFront.Name = "StatusFront";
            this.StatusFront.Size = new System.Drawing.Size(32, 32);
            this.StatusFront.TabIndex = 16;
            this.StatusFront.Text = "coolInfraredRay1";
            this.coreBind.SetVerification(this.StatusFront, null);
            // 
            // StatusLight
            // 
            this.coreBind.SetDatabasecommand(this.StatusLight, null);
            this.StatusLight.Gradient = true;
            this.StatusLight.Location = new System.Drawing.Point(24, 53);
            this.StatusLight.Name = "StatusLight";
            this.StatusLight.Size = new System.Drawing.Size(32, 32);
            this.StatusLight.TabIndex = 13;
            this.StatusLight.Text = "coolIndicator2";
            this.coreBind.SetVerification(this.StatusLight, null);
            // 
            // StatusRedGreen
            // 
            this.coreBind.SetDatabasecommand(this.StatusRedGreen, null);
            this.StatusRedGreen.Gradient = true;
            this.StatusRedGreen.Location = new System.Drawing.Point(24, 13);
            this.StatusRedGreen.Name = "StatusRedGreen";
            this.StatusRedGreen.Size = new System.Drawing.Size(32, 32);
            this.StatusRedGreen.TabIndex = 12;
            this.StatusRedGreen.Text = "coolIndicator1";
            this.coreBind.SetVerification(this.StatusRedGreen, null);
            // 
            // btnHDHW
            // 
            this.btnHDHW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnHDHW, null);
            this.btnHDHW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHDHW.Location = new System.Drawing.Point(204, 51);
            this.btnHDHW.Name = "btnHDHW";
            this.btnHDHW.Size = new System.Drawing.Size(73, 34);
            this.btnHDHW.TabIndex = 11;
            this.btnHDHW.Text = "后端红外";
            this.btnHDHW.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnHDHW, null);
            // 
            // btnQDHW
            // 
            this.btnQDHW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnQDHW, null);
            this.btnQDHW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQDHW.Location = new System.Drawing.Point(204, 13);
            this.btnQDHW.Name = "btnQDHW";
            this.btnQDHW.Size = new System.Drawing.Size(73, 34);
            this.btnQDHW.TabIndex = 10;
            this.btnQDHW.Text = "前端红外";
            this.btnQDHW.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnQDHW, null);
            // 
            // btnZMDKG
            // 
            this.btnZMDKG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnZMDKG, null);
            this.btnZMDKG.Location = new System.Drawing.Point(81, 55);
            this.btnZMDKG.Name = "btnZMDKG";
            this.btnZMDKG.Size = new System.Drawing.Size(43, 28);
            this.btnZMDKG.TabIndex = 7;
            this.btnZMDKG.Text = "开/关";
            this.btnZMDKG.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnZMDKG, null);
            this.btnZMDKG.Click += new System.EventHandler(this.btnZMDKG_Click);
            // 
            // btnHL
            // 
            this.btnHL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnHL, null);
            this.btnHL.Location = new System.Drawing.Point(81, 18);
            this.btnHL.Name = "btnHL";
            this.btnHL.Size = new System.Drawing.Size(43, 28);
            this.btnHL.TabIndex = 6;
            this.btnHL.Text = "红/绿";
            this.btnHL.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnHL, null);
            this.btnHL.Click += new System.EventHandler(this.btnHL_Click);
            // 
            // btnQL
            // 
            this.btnQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnQL, null);
            this.btnQL.Location = new System.Drawing.Point(294, 56);
            this.btnQL.Name = "btnQL";
            this.btnQL.Size = new System.Drawing.Size(105, 28);
            this.btnQL.TabIndex = 4;
            this.btnQL.Text = "仪表清零";
            this.btnQL.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnQL, null);
            this.btnQL.Click += new System.EventHandler(this.btnQL_Click);
            // 
            // lbWD
            // 
            this.lbWD.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.lbWD, null);
            this.lbWD.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWD.Location = new System.Drawing.Point(315, 26);
            this.lbWD.Name = "lbWD";
            this.lbWD.Size = new System.Drawing.Size(76, 21);
            this.lbWD.TabIndex = 3;
            this.lbWD.Text = "不稳定";
            this.coreBind.SetVerification(this.lbWD, null);
            this.lbWD.TextChanged += new System.EventHandler(this.lbWD_TextChanged);
            // 
            // lbYS
            // 
            this.coreBind.SetDatabasecommand(this.lbYS, null);
            this.lbYS.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbYS.ForeColor = System.Drawing.Color.Red;
            this.lbYS.Location = new System.Drawing.Point(292, 23);
            this.lbYS.Name = "lbYS";
            this.lbYS.Size = new System.Drawing.Size(28, 28);
            this.lbYS.TabIndex = 2;
            this.lbYS.Text = "●";
            this.lbYS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.lbYS, null);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.label2, null);
            this.label2.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(247, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 56);
            this.label2.TabIndex = 1;
            this.label2.Text = "t";
            this.coreBind.SetVerification(this.label2, null);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.ultraGroupBox2);
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.ultraGroupBox1);
            this.coreBind.SetDatabasecommand(this.panel8, null);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(264, 124);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(743, 303);
            this.panel8.TabIndex = 4;
            this.coreBind.SetVerification(this.panel8, null);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularInset;
            this.ultraGroupBox2.Controls.Add(this.panel11);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox2, null);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox2.Location = new System.Drawing.Point(479, 0);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(264, 252);
            this.ultraGroupBox2.TabIndex = 2;
            this.ultraGroupBox2.Text = "计量点接管信息";
            this.coreBind.SetVerification(this.ultraGroupBox2, null);
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.ultraGrid2);
            this.panel11.Controls.Add(this.ultraGrid1);
            this.coreBind.SetDatabasecommand(this.panel11, null);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(3, 20);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(257, 229);
            this.panel11.TabIndex = 0;
            this.coreBind.SetVerification(this.panel11, null);
            // 
            // ultraGrid2
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid2, null);
            this.ultraGrid2.DataMember = "计量点基础表";
            this.ultraGrid2.DataSource = this.dataSet1;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid2.DisplayLayout.Appearance = appearance1;
            ultraGridColumn72.Header.VisiblePosition = 1;
            ultraGridColumn72.Hidden = true;
            ultraGridColumn73.Header.VisiblePosition = 2;
            ultraGridColumn73.Width = 118;
            ultraGridColumn74.Header.VisiblePosition = 0;
            ultraGridColumn74.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn74.Width = 45;
            ultraGridColumn75.Header.VisiblePosition = 3;
            ultraGridColumn75.Hidden = true;
            ultraGridColumn76.Header.VisiblePosition = 4;
            ultraGridColumn76.Hidden = true;
            ultraGridColumn77.Header.VisiblePosition = 5;
            ultraGridColumn77.Hidden = true;
            ultraGridColumn78.Header.VisiblePosition = 6;
            ultraGridColumn78.Hidden = true;
            ultraGridColumn79.Header.VisiblePosition = 7;
            ultraGridColumn79.Hidden = true;
            ultraGridColumn80.Header.VisiblePosition = 8;
            ultraGridColumn80.Hidden = true;
            ultraGridColumn81.Header.VisiblePosition = 9;
            ultraGridColumn81.Hidden = true;
            ultraGridColumn82.Header.VisiblePosition = 10;
            ultraGridColumn82.Hidden = true;
            ultraGridColumn83.Header.VisiblePosition = 11;
            ultraGridColumn83.Hidden = true;
            ultraGridColumn84.Header.VisiblePosition = 12;
            ultraGridColumn84.Hidden = true;
            ultraGridColumn85.Header.VisiblePosition = 13;
            ultraGridColumn85.Hidden = true;
            ultraGridColumn86.Header.VisiblePosition = 14;
            ultraGridColumn86.Hidden = true;
            ultraGridColumn87.Header.VisiblePosition = 15;
            ultraGridColumn87.Hidden = true;
            ultraGridColumn88.Header.VisiblePosition = 16;
            ultraGridColumn88.Hidden = true;
            ultraGridColumn89.Header.VisiblePosition = 17;
            ultraGridColumn89.Hidden = true;
            ultraGridColumn90.Header.VisiblePosition = 18;
            ultraGridColumn90.Hidden = true;
            ultraGridColumn91.Header.VisiblePosition = 19;
            ultraGridColumn91.Hidden = true;
            ultraGridColumn92.Header.VisiblePosition = 20;
            ultraGridColumn92.Hidden = true;
            ultraGridColumn93.Header.VisiblePosition = 21;
            ultraGridColumn93.Hidden = true;
            ultraGridColumn94.Header.VisiblePosition = 22;
            ultraGridColumn94.Hidden = true;
            ultraGridColumn95.Header.VisiblePosition = 23;
            ultraGridColumn95.Hidden = true;
            ultraGridColumn96.Header.VisiblePosition = 24;
            ultraGridColumn96.Hidden = true;
            ultraGridColumn97.Header.VisiblePosition = 25;
            ultraGridColumn97.Hidden = true;
            ultraGridColumn98.Header.VisiblePosition = 26;
            ultraGridColumn98.Hidden = true;
            ultraGridColumn99.Header.VisiblePosition = 27;
            ultraGridColumn99.Hidden = true;
            ultraGridColumn100.Header.VisiblePosition = 28;
            ultraGridColumn100.Hidden = true;
            ultraGridColumn101.Header.VisiblePosition = 29;
            ultraGridColumn101.Hidden = true;
            ultraGridColumn102.Header.VisiblePosition = 30;
            ultraGridColumn102.Hidden = true;
            ultraGridColumn103.Header.VisiblePosition = 31;
            ultraGridColumn103.Hidden = true;
            ultraGridColumn104.Header.VisiblePosition = 32;
            ultraGridColumn104.Hidden = true;
            ultraGridColumn105.Header.VisiblePosition = 33;
            ultraGridColumn105.Hidden = true;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74,
            ultraGridColumn75,
            ultraGridColumn76,
            ultraGridColumn77,
            ultraGridColumn78,
            ultraGridColumn79,
            ultraGridColumn80,
            ultraGridColumn81,
            ultraGridColumn82,
            ultraGridColumn83,
            ultraGridColumn84,
            ultraGridColumn85,
            ultraGridColumn86,
            ultraGridColumn87,
            ultraGridColumn88,
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91,
            ultraGridColumn92,
            ultraGridColumn93,
            ultraGridColumn94,
            ultraGridColumn95,
            ultraGridColumn96,
            ultraGridColumn97,
            ultraGridColumn98,
            ultraGridColumn99,
            ultraGridColumn100,
            ultraGridColumn101,
            ultraGridColumn102,
            ultraGridColumn103,
            ultraGridColumn104,
            ultraGridColumn105});
            ultraGridBand4.RowLayoutStyle = Infragistics.Win.UltraWinGrid.RowLayoutStyle.GroupLayout;
            this.ultraGrid2.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.ultraGrid2.DisplayLayout.InterBandSpacing = 10;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid2.DisplayLayout.Override.CardAreaAppearance = appearance2;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance3.BackColor2 = System.Drawing.Color.White;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance3.FontData.SizeInPoints = 11F;
            appearance3.FontData.UnderlineAsString = "False";
            appearance3.ForeColor = System.Drawing.Color.Black;
            appearance3.TextHAlignAsString = "Center";
            appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid2.DisplayLayout.Override.HeaderAppearance = appearance3;
            appearance4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.Override.RowAppearance = appearance4;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorAppearance = appearance5;
            this.ultraGrid2.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid2.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance6.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance6.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid2.DisplayLayout.Override.SelectedRowAppearance = appearance6;
            this.ultraGrid2.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid2.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid2.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid2.Name = "ultraGrid2";
            this.ultraGrid2.Size = new System.Drawing.Size(253, 225);
            this.ultraGrid2.TabIndex = 1;
            this.coreBind.SetVerification(this.ultraGrid2, null);
            this.ultraGrid2.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ultraGrid2_ClickCell);
            this.ultraGrid2.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ultraGrid2_CellChange);
            this.ultraGrid2.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.ultraGrid2_AfterSelectChange);
            // 
            // ultraGrid1
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid1, null);
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(253, 225);
            this.ultraGrid1.TabIndex = 0;
            this.coreBind.SetVerification(this.ultraGrid1, null);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel10.Controls.Add(this.btnBC);
            this.panel10.Controls.Add(this.btnGPBC);
            this.panel10.Controls.Add(this.btnSD);
            this.panel10.Controls.Add(this.btnDS);
            this.panel10.Controls.Add(this.button16);
            this.panel10.Controls.Add(this.button17);
            this.coreBind.SetDatabasecommand(this.panel10, null);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(479, 252);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(264, 51);
            this.panel10.TabIndex = 1;
            this.coreBind.SetVerification(this.panel10, null);
            // 
            // btnBC
            // 
            this.btnBC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnBC, null);
            this.btnBC.Location = new System.Drawing.Point(116, 10);
            this.btnBC.Name = "btnBC";
            this.btnBC.Size = new System.Drawing.Size(62, 32);
            this.btnBC.TabIndex = 25;
            this.btnBC.Text = "保存";
            this.btnBC.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnBC, null);
            this.btnBC.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // btnGPBC
            // 
            this.btnGPBC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnGPBC, null);
            this.btnGPBC.Location = new System.Drawing.Point(116, 10);
            this.btnGPBC.Name = "btnGPBC";
            this.btnGPBC.Size = new System.Drawing.Size(62, 32);
            this.btnGPBC.TabIndex = 27;
            this.btnGPBC.Text = "钢坯保存";
            this.btnGPBC.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnGPBC, null);
            this.btnGPBC.Click += new System.EventHandler(this.btnGPBC_Click);
            // 
            // btnSD
            // 
            this.btnSD.BackColor = System.Drawing.Color.Violet;
            this.coreBind.SetDatabasecommand(this.btnSD, null);
            this.btnSD.Location = new System.Drawing.Point(24, 10);
            this.btnSD.Name = "btnSD";
            this.btnSD.Size = new System.Drawing.Size(66, 32);
            this.btnSD.TabIndex = 26;
            this.btnSD.Text = "打开设备";
            this.btnSD.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnSD, null);
            this.btnSD.Click += new System.EventHandler(this.btnSD_Click);
            // 
            // btnDS
            // 
            this.btnDS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnDS, null);
            this.btnDS.Location = new System.Drawing.Point(69, 10);
            this.btnDS.Name = "btnDS";
            this.btnDS.Size = new System.Drawing.Size(52, 32);
            this.btnDS.TabIndex = 23;
            this.btnDS.Text = "读数";
            this.btnDS.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnDS, null);
            this.btnDS.Visible = false;
            this.btnDS.Click += new System.EventHandler(this.btnDS_Click);
            // 
            // button16
            // 
            this.coreBind.SetDatabasecommand(this.button16, null);
            this.button16.Location = new System.Drawing.Point(2, 4);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(26, 39);
            this.button16.TabIndex = 707;
            this.button16.Text = "16";
            this.button16.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button16, null);
            this.button16.Visible = false;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button17
            // 
            this.coreBind.SetDatabasecommand(this.button17, null);
            this.button17.Location = new System.Drawing.Point(175, 4);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(23, 38);
            this.button17.TabIndex = 708;
            this.button17.Text = "17";
            this.button17.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button17, null);
            this.button17.Visible = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularInset;
            this.ultraGroupBox1.Controls.Add(this.panel9);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox1, null);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(479, 303);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "预报和称重信息";
            this.coreBind.SetVerification(this.ultraGroupBox1, null);
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2003;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Controls.Add(this.cbProvider);
            this.panel9.Controls.Add(this.label17);
            this.panel9.Controls.Add(this.label19);
            this.panel9.Controls.Add(this.tbBZ);
            this.panel9.Controls.Add(this.label29);
            this.panel9.Controls.Add(this.button18);
            this.panel9.Controls.Add(this.label28);
            this.panel9.Controls.Add(this.label27);
            this.panel9.Controls.Add(this.tbCharge);
            this.panel9.Controls.Add(this.label25);
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.tbReceiverPlace);
            this.panel9.Controls.Add(this.tbSenderPlace);
            this.panel9.Controls.Add(this.label24);
            this.panel9.Controls.Add(this.txtPJBH);
            this.panel9.Controls.Add(this.txtHTXMH);
            this.panel9.Controls.Add(this.txtDFJZ);
            this.panel9.Controls.Add(this.label22);
            this.panel9.Controls.Add(this.btnFHDW);
            this.panel9.Controls.Add(this.btnSHDW);
            this.panel9.Controls.Add(this.btnCYDW);
            this.panel9.Controls.Add(this.btnWLMC);
            this.panel9.Controls.Add(this.txtYKL);
            this.panel9.Controls.Add(this.txtZL);
            this.panel9.Controls.Add(this.txtPZ);
            this.panel9.Controls.Add(this.label18);
            this.panel9.Controls.Add(this.label4);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.txtJZ);
            this.panel9.Controls.Add(this.txtMZ);
            this.panel9.Controls.Add(this.txtZS3);
            this.panel9.Controls.Add(this.txtZS2);
            this.panel9.Controls.Add(this.txtZS);
            this.panel9.Controls.Add(this.chbSFYC);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Controls.Add(this.txtBC);
            this.panel9.Controls.Add(this.cbJLLX);
            this.panel9.Controls.Add(this.label12);
            this.panel9.Controls.Add(this.chbQXPZ);
            this.panel9.Controls.Add(this.cbCH);
            this.panel9.Controls.Add(this.cbCH1);
            this.panel9.Controls.Add(this.cbWLMC);
            this.panel9.Controls.Add(this.cbSHDW);
            this.panel9.Controls.Add(this.cbCYDW);
            this.panel9.Controls.Add(this.txtJLY);
            this.panel9.Controls.Add(this.cbFHDW);
            this.panel9.Controls.Add(this.cbLX);
            this.panel9.Controls.Add(this.txtJLD);
            this.panel9.Controls.Add(this.label26);
            this.panel9.Controls.Add(this.label21);
            this.panel9.Controls.Add(this.label16);
            this.panel9.Controls.Add(this.label14);
            this.panel9.Controls.Add(this.label15);
            this.panel9.Controls.Add(this.label11);
            this.panel9.Controls.Add(this.label13);
            this.panel9.Controls.Add(this.label10);
            this.panel9.Controls.Add(this.txtLH);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Controls.Add(this.txtHTH);
            this.panel9.Controls.Add(this.label6);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Controls.Add(this.txtCZH);
            this.panel9.Controls.Add(this.txtCarNo);
            this.panel9.Controls.Add(this.label20);
            this.panel9.Controls.Add(this.cbLS);
            this.panel9.Controls.Add(this.label23);
            this.panel9.Controls.Add(this.txtLH3);
            this.panel9.Controls.Add(this.txtLH2);
            this.coreBind.SetDatabasecommand(this.panel9, null);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 20);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(472, 280);
            this.panel9.TabIndex = 0;
            this.coreBind.SetVerification(this.panel9, null);
            // 
            // cbProvider
            // 
            this.cbProvider.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.cbProvider, null);
            this.cbProvider.DataSource = this.dataSet1;
            this.cbProvider.DisplayMember = "发货单位表.FS_SUPPLIERNAME";
            this.cbProvider.FormattingEnabled = true;
            this.cbProvider.Location = new System.Drawing.Point(60, 165);
            this.cbProvider.Name = "cbProvider";
            this.cbProvider.Size = new System.Drawing.Size(132, 20);
            this.cbProvider.TabIndex = 712;
            this.cbProvider.ValueMember = "发货单位表.FS_SUPPLIER";
            this.coreBind.SetVerification(this.cbProvider, null);
            this.cbProvider.SelectedIndexChanged += new System.EventHandler(this.cbProvider_SelectedIndexChanged);
            this.cbProvider.Leave += new System.EventHandler(this.cbProvider_Leave);
            this.cbProvider.TextChanged += new System.EventHandler(this.cbProvider_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label17, null);
            this.label17.Location = new System.Drawing.Point(250, 261);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 718;
            this.label17.Text = "计量员";
            this.coreBind.SetVerification(this.label17, null);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label19, null);
            this.label19.Location = new System.Drawing.Point(130, 261);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(41, 12);
            this.label19.TabIndex = 717;
            this.label19.Text = "计量点";
            this.coreBind.SetVerification(this.label19, null);
            // 
            // tbBZ
            // 
            this.tbBZ.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.tbBZ, null);
            this.tbBZ.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbBZ.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbBZ.Location = new System.Drawing.Point(291, 164);
            this.tbBZ.MaxLength = 20;
            this.tbBZ.Multiline = true;
            this.tbBZ.Name = "tbBZ";
            this.tbBZ.Size = new System.Drawing.Size(166, 21);
            this.tbBZ.TabIndex = 715;
            this.coreBind.SetVerification(this.tbBZ, null);
            // 
            // label29
            // 
            this.coreBind.SetDatabasecommand(this.label29, null);
            this.label29.Location = new System.Drawing.Point(232, 163);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(59, 24);
            this.label29.TabIndex = 716;
            this.label29.Text = "备注";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label29, null);
            // 
            // button18
            // 
            this.button18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.button18, null);
            this.button18.Location = new System.Drawing.Point(192, 163);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(25, 21);
            this.button18.TabIndex = 714;
            this.button18.Tag = "Provider";
            this.button18.Text = "..";
            this.button18.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.button18, null);
            this.button18.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // label28
            // 
            this.coreBind.SetDatabasecommand(this.label28, null);
            this.label28.Location = new System.Drawing.Point(2, 163);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(58, 24);
            this.label28.TabIndex = 713;
            this.label28.Text = "供应单位";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label28, null);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label27, null);
            this.label27.Location = new System.Drawing.Point(141, 193);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 711;
            this.label27.Text = "费用(元)";
            this.coreBind.SetVerification(this.label27, null);
            // 
            // tbCharge
            // 
            this.coreBind.SetDatabasecommand(this.tbCharge, null);
            this.tbCharge.Enabled = false;
            this.tbCharge.Location = new System.Drawing.Point(194, 187);
            this.tbCharge.Name = "tbCharge";
            this.tbCharge.Size = new System.Drawing.Size(55, 21);
            this.tbCharge.TabIndex = 710;
            this.tbCharge.Text = "0";
            this.coreBind.SetVerification(this.tbCharge, null);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label25, null);
            this.label25.Location = new System.Drawing.Point(232, 146);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.TabIndex = 706;
            this.label25.Text = "卸货地点";
            this.coreBind.SetVerification(this.label25, null);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label7, null);
            this.label7.Location = new System.Drawing.Point(224, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 705;
            this.label7.Text = "合同项目号";
            this.coreBind.SetVerification(this.label7, null);
            // 
            // tbReceiverPlace
            // 
            this.tbReceiverPlace.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.tbReceiverPlace, null);
            this.tbReceiverPlace.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbReceiverPlace.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbReceiverPlace.Location = new System.Drawing.Point(291, 141);
            this.tbReceiverPlace.MaxLength = 20;
            this.tbReceiverPlace.Multiline = true;
            this.tbReceiverPlace.Name = "tbReceiverPlace";
            this.tbReceiverPlace.Size = new System.Drawing.Size(166, 21);
            this.tbReceiverPlace.TabIndex = 703;
            this.coreBind.SetVerification(this.tbReceiverPlace, null);
            // 
            // tbSenderPlace
            // 
            this.tbSenderPlace.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.tbSenderPlace, null);
            this.tbSenderPlace.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSenderPlace.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbSenderPlace.Location = new System.Drawing.Point(60, 141);
            this.tbSenderPlace.MaxLength = 20;
            this.tbSenderPlace.Multiline = true;
            this.tbSenderPlace.Name = "tbSenderPlace";
            this.tbSenderPlace.Size = new System.Drawing.Size(157, 21);
            this.tbSenderPlace.TabIndex = 701;
            this.coreBind.SetVerification(this.tbSenderPlace, null);
            // 
            // label24
            // 
            this.coreBind.SetDatabasecommand(this.label24, null);
            this.label24.Location = new System.Drawing.Point(2, 141);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(59, 24);
            this.label24.TabIndex = 702;
            this.label24.Text = "发货地点";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label24, null);
            // 
            // txtPJBH
            // 
            this.txtPJBH.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtPJBH, null);
            this.txtPJBH.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPJBH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPJBH.Location = new System.Drawing.Point(60, 232);
            this.txtPJBH.MaxLength = 20;
            this.txtPJBH.Multiline = true;
            this.txtPJBH.Name = "txtPJBH";
            this.txtPJBH.Size = new System.Drawing.Size(132, 21);
            this.txtPJBH.TabIndex = 694;
            this.coreBind.SetVerification(this.txtPJBH, null);
            // 
            // txtHTXMH
            // 
            this.coreBind.SetDatabasecommand(this.txtHTXMH, null);
            this.txtHTXMH.FormattingEnabled = true;
            this.txtHTXMH.Location = new System.Drawing.Point(291, 51);
            this.txtHTXMH.Name = "txtHTXMH";
            this.txtHTXMH.Size = new System.Drawing.Size(69, 20);
            this.txtHTXMH.TabIndex = 700;
            this.coreBind.SetVerification(this.txtHTXMH, null);
            // 
            // txtDFJZ
            // 
            this.txtDFJZ.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtDFJZ, null);
            this.txtDFJZ.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDFJZ.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDFJZ.Location = new System.Drawing.Point(60, 256);
            this.txtDFJZ.MaxLength = 10;
            this.txtDFJZ.Multiline = true;
            this.txtDFJZ.Name = "txtDFJZ";
            this.txtDFJZ.Size = new System.Drawing.Size(69, 21);
            this.txtDFJZ.TabIndex = 696;
            this.coreBind.SetVerification(this.txtDFJZ, null);
            // 
            // label22
            // 
            this.coreBind.SetDatabasecommand(this.label22, null);
            this.label22.Location = new System.Drawing.Point(1, 255);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 24);
            this.label22.TabIndex = 697;
            this.label22.Text = "对方净重";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label22, null);
            // 
            // btnFHDW
            // 
            this.btnFHDW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnFHDW, null);
            this.btnFHDW.Location = new System.Drawing.Point(192, 119);
            this.btnFHDW.Name = "btnFHDW";
            this.btnFHDW.Size = new System.Drawing.Size(25, 21);
            this.btnFHDW.TabIndex = 693;
            this.btnFHDW.Tag = "Sender";
            this.btnFHDW.Text = "..";
            this.btnFHDW.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnFHDW, null);
            this.btnFHDW.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // btnSHDW
            // 
            this.btnSHDW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnSHDW, null);
            this.btnSHDW.Location = new System.Drawing.Point(436, 119);
            this.btnSHDW.Name = "btnSHDW";
            this.btnSHDW.Size = new System.Drawing.Size(25, 21);
            this.btnSHDW.TabIndex = 692;
            this.btnSHDW.Tag = "Receiver";
            this.btnSHDW.Text = "..";
            this.btnSHDW.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnSHDW, null);
            this.btnSHDW.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // btnCYDW
            // 
            this.btnCYDW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnCYDW, null);
            this.btnCYDW.Location = new System.Drawing.Point(436, 97);
            this.btnCYDW.Name = "btnCYDW";
            this.btnCYDW.Size = new System.Drawing.Size(25, 21);
            this.btnCYDW.TabIndex = 691;
            this.btnCYDW.Tag = "Trans";
            this.btnCYDW.Text = "..";
            this.btnCYDW.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnCYDW, null);
            this.btnCYDW.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // btnWLMC
            // 
            this.btnWLMC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnWLMC, null);
            this.btnWLMC.Location = new System.Drawing.Point(436, 74);
            this.btnWLMC.Name = "btnWLMC";
            this.btnWLMC.Size = new System.Drawing.Size(25, 21);
            this.btnWLMC.TabIndex = 690;
            this.btnWLMC.Tag = "Material";
            this.btnWLMC.Text = "..";
            this.btnWLMC.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnWLMC, null);
            this.btnWLMC.Click += new System.EventHandler(this.moreBtn_Click);
            // 
            // txtYKL
            // 
            this.coreBind.SetDatabasecommand(this.txtYKL, null);
            this.txtYKL.Location = new System.Drawing.Point(345, 187);
            this.txtYKL.Name = "txtYKL";
            this.txtYKL.ReadOnly = true;
            this.txtYKL.Size = new System.Drawing.Size(66, 21);
            this.txtYKL.TabIndex = 612;
            this.coreBind.SetVerification(this.txtYKL, null);
            // 
            // txtZL
            // 
            this.txtZL.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtZL, null);
            this.txtZL.Location = new System.Drawing.Point(242, 232);
            this.txtZL.Name = "txtZL";
            this.txtZL.ReadOnly = true;
            this.txtZL.Size = new System.Drawing.Size(80, 21);
            this.txtZL.TabIndex = 611;
            this.coreBind.SetVerification(this.txtZL, null);
            // 
            // txtPZ
            // 
            this.txtPZ.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtPZ, null);
            this.txtPZ.Location = new System.Drawing.Point(194, 209);
            this.txtPZ.Name = "txtPZ";
            this.txtPZ.ReadOnly = true;
            this.txtPZ.Size = new System.Drawing.Size(80, 21);
            this.txtPZ.TabIndex = 606;
            this.coreBind.SetVerification(this.txtPZ, null);
            // 
            // label18
            // 
            this.coreBind.SetDatabasecommand(this.label18, null);
            this.label18.Location = new System.Drawing.Point(284, 209);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 24);
            this.label18.TabIndex = 610;
            this.label18.Text = "净量(t)";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label18, null);
            // 
            // label4
            // 
            this.coreBind.SetDatabasecommand(this.label4, null);
            this.label4.Location = new System.Drawing.Point(140, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 24);
            this.label4.TabIndex = 609;
            this.label4.Text = "皮重(t)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label4, null);
            // 
            // label3
            // 
            this.coreBind.SetDatabasecommand(this.label3, null);
            this.label3.Location = new System.Drawing.Point(2, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 24);
            this.label3.TabIndex = 608;
            this.label3.Text = "毛重(t)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label3, null);
            // 
            // txtJZ
            // 
            this.txtJZ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this.txtJZ, null);
            this.txtJZ.Location = new System.Drawing.Point(345, 209);
            this.txtJZ.Name = "txtJZ";
            this.txtJZ.ReadOnly = true;
            this.txtJZ.Size = new System.Drawing.Size(112, 21);
            this.txtJZ.TabIndex = 607;
            this.coreBind.SetVerification(this.txtJZ, null);
            // 
            // txtMZ
            // 
            this.txtMZ.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtMZ, null);
            this.txtMZ.Location = new System.Drawing.Point(60, 209);
            this.txtMZ.Name = "txtMZ";
            this.txtMZ.ReadOnly = true;
            this.txtMZ.Size = new System.Drawing.Size(78, 21);
            this.txtMZ.TabIndex = 605;
            this.coreBind.SetVerification(this.txtMZ, null);
            // 
            // txtZS3
            // 
            this.coreBind.SetDatabasecommand(this.txtZS3, null);
            this.txtZS3.Location = new System.Drawing.Point(166, 74);
            this.txtZS3.Name = "txtZS3";
            this.txtZS3.Size = new System.Drawing.Size(51, 21);
            this.txtZS3.TabIndex = 604;
            this.coreBind.SetVerification(this.txtZS3, null);
            // 
            // txtZS2
            // 
            this.coreBind.SetDatabasecommand(this.txtZS2, null);
            this.txtZS2.Location = new System.Drawing.Point(113, 74);
            this.txtZS2.Name = "txtZS2";
            this.txtZS2.Size = new System.Drawing.Size(51, 21);
            this.txtZS2.TabIndex = 603;
            this.coreBind.SetVerification(this.txtZS2, null);
            // 
            // txtZS
            // 
            this.coreBind.SetDatabasecommand(this.txtZS, null);
            this.txtZS.Location = new System.Drawing.Point(60, 74);
            this.txtZS.Name = "txtZS";
            this.txtZS.Size = new System.Drawing.Size(51, 21);
            this.txtZS.TabIndex = 602;
            this.coreBind.SetVerification(this.txtZS, null);
            // 
            // chbSFYC
            // 
            this.coreBind.SetDatabasecommand(this.chbSFYC, null);
            this.chbSFYC.ForeColor = System.Drawing.Color.Red;
            this.chbSFYC.Location = new System.Drawing.Point(415, 186);
            this.chbSFYC.Name = "chbSFYC";
            this.chbSFYC.Size = new System.Drawing.Size(49, 28);
            this.chbSFYC.TabIndex = 26;
            this.chbSFYC.Text = "异常";
            this.chbSFYC.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.chbSFYC, null);
            // 
            // label1
            // 
            this.coreBind.SetDatabasecommand(this.label1, null);
            this.label1.Location = new System.Drawing.Point(277, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 24);
            this.label1.TabIndex = 600;
            this.label1.Text = "应扣量(Kg)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label1, null);
            // 
            // txtBC
            // 
            this.txtBC.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtBC, null);
            this.txtBC.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBC.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtBC.Location = new System.Drawing.Point(405, 256);
            this.txtBC.MaxLength = 8;
            this.txtBC.Multiline = true;
            this.txtBC.Name = "txtBC";
            this.txtBC.ReadOnly = true;
            this.txtBC.Size = new System.Drawing.Size(52, 21);
            this.txtBC.TabIndex = 19;
            this.coreBind.SetVerification(this.txtBC, null);
            // 
            // cbJLLX
            // 
            this.coreBind.SetDatabasecommand(this.cbJLLX, null);
            this.cbJLLX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJLLX.FormattingEnabled = true;
            this.cbJLLX.Items.AddRange(new object[] {
            "",
            "外协",
            "复磅",
            "退货过磅"});
            this.cbJLLX.Location = new System.Drawing.Point(60, 187);
            this.cbJLLX.Name = "cbJLLX";
            this.cbJLLX.Size = new System.Drawing.Size(78, 20);
            this.cbJLLX.TabIndex = 20;
            this.coreBind.SetVerification(this.cbJLLX, null);
            this.cbJLLX.SelectedIndexChanged += new System.EventHandler(this.cbJLLX_SelectedIndexChanged);
            // 
            // label12
            // 
            this.coreBind.SetDatabasecommand(this.label12, null);
            this.label12.Location = new System.Drawing.Point(2, 187);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 24);
            this.label12.TabIndex = 598;
            this.label12.Text = "计量类型";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label12, null);
            // 
            // chbQXPZ
            // 
            this.chbQXPZ.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.chbQXPZ, null);
            this.chbQXPZ.Location = new System.Drawing.Point(366, 51);
            this.chbQXPZ.Name = "chbQXPZ";
            this.chbQXPZ.Size = new System.Drawing.Size(96, 16);
            this.chbQXPZ.TabIndex = 22;
            this.chbQXPZ.Text = "保存期限皮重";
            this.chbQXPZ.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.chbQXPZ, null);
            // 
            // cbCH
            // 
            this.coreBind.SetDatabasecommand(this.cbCH, null);
            this.cbCH.FormattingEnabled = true;
            this.cbCH.Location = new System.Drawing.Point(291, 2);
            this.cbCH.Name = "cbCH";
            this.cbCH.Size = new System.Drawing.Size(47, 20);
            this.cbCH.TabIndex = 2;
            this.coreBind.SetVerification(this.cbCH, null);
            this.cbCH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCH_KeyPress);
            // 
            // cbCH1
            // 
            this.coreBind.SetDatabasecommand(this.cbCH1, null);
            this.cbCH1.FormattingEnabled = true;
            this.cbCH1.Location = new System.Drawing.Point(337, 2);
            this.cbCH1.Name = "cbCH1";
            this.cbCH1.Size = new System.Drawing.Size(124, 20);
            this.cbCH1.TabIndex = 3;
            this.coreBind.SetVerification(this.cbCH1, null);
            this.cbCH1.Leave += new System.EventHandler(this.cbCH1_Leave);
            this.cbCH1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCH1_KeyPress);
            // 
            // cbWLMC
            // 
            this.cbWLMC.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.cbWLMC, null);
            this.cbWLMC.DataSource = this.dataSet1;
            this.cbWLMC.DisplayMember = "物料表.FS_MATERIALNAME";
            this.cbWLMC.FormattingEnabled = true;
            this.cbWLMC.Location = new System.Drawing.Point(291, 74);
            this.cbWLMC.Name = "cbWLMC";
            this.cbWLMC.Size = new System.Drawing.Size(145, 20);
            this.cbWLMC.TabIndex = 12;
            this.cbWLMC.ValueMember = "物料表.FS_MATERIALNO";
            this.coreBind.SetVerification(this.cbWLMC, null);
            this.cbWLMC.Leave += new System.EventHandler(this.cbWLMC_Leave);
            this.cbWLMC.TextChanged += new System.EventHandler(this.cbWLMC_TextChanged);
            // 
            // cbSHDW
            // 
            this.cbSHDW.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.cbSHDW, null);
            this.cbSHDW.DataSource = this.dataSet1;
            this.cbSHDW.DisplayMember = "收货单位表.FS_MEMO";
            this.cbSHDW.FormattingEnabled = true;
            this.cbSHDW.Location = new System.Drawing.Point(291, 119);
            this.cbSHDW.Name = "cbSHDW";
            this.cbSHDW.Size = new System.Drawing.Size(145, 20);
            this.cbSHDW.TabIndex = 16;
            this.cbSHDW.ValueMember = "收货单位表.FS_RECEIVER";
            this.coreBind.SetVerification(this.cbSHDW, null);
            this.cbSHDW.Leave += new System.EventHandler(this.cbSHDW_Leave);
            this.cbSHDW.TextChanged += new System.EventHandler(this.cbSHDW_TextChanged);
            // 
            // cbCYDW
            // 
            this.cbCYDW.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.cbCYDW, null);
            this.cbCYDW.DataSource = this.dataSet1;
            this.cbCYDW.DisplayMember = "承运单位表.FS_TRANSNAME";
            this.cbCYDW.FormattingEnabled = true;
            this.cbCYDW.Location = new System.Drawing.Point(291, 97);
            this.cbCYDW.Name = "cbCYDW";
            this.cbCYDW.Size = new System.Drawing.Size(145, 20);
            this.cbCYDW.TabIndex = 14;
            this.cbCYDW.ValueMember = "承运单位表.FS_TRANSNO";
            this.coreBind.SetVerification(this.cbCYDW, null);
            this.cbCYDW.Leave += new System.EventHandler(this.cbCYDW_Leave);
            this.cbCYDW.TextChanged += new System.EventHandler(this.cbCYDW_TextChanged);
            // 
            // txtJLY
            // 
            this.txtJLY.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtJLY, null);
            this.txtJLY.Font = new System.Drawing.Font("宋体", 11F);
            this.txtJLY.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJLY.Location = new System.Drawing.Point(291, 256);
            this.txtJLY.MaxLength = 8;
            this.txtJLY.Multiline = true;
            this.txtJLY.Name = "txtJLY";
            this.txtJLY.ReadOnly = true;
            this.txtJLY.Size = new System.Drawing.Size(74, 21);
            this.txtJLY.TabIndex = 18;
            this.coreBind.SetVerification(this.txtJLY, null);
            // 
            // cbFHDW
            // 
            this.cbFHDW.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.cbFHDW, null);
            this.cbFHDW.DataSource = this.dataSet1;
            this.cbFHDW.DisplayMember = "发货单位表.FS_SUPPLIERNAME";
            this.cbFHDW.FormattingEnabled = true;
            this.cbFHDW.Location = new System.Drawing.Point(60, 119);
            this.cbFHDW.Name = "cbFHDW";
            this.cbFHDW.Size = new System.Drawing.Size(132, 20);
            this.cbFHDW.TabIndex = 15;
            this.cbFHDW.ValueMember = "发货单位表.FS_SUPPLIER";
            this.coreBind.SetVerification(this.cbFHDW, null);
            this.cbFHDW.Leave += new System.EventHandler(this.cbFHDW_Leave);
            this.cbFHDW.TextChanged += new System.EventHandler(this.cbFHDW_TextChanged);
            // 
            // cbLX
            // 
            this.cbLX.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLX.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.cbLX, null);
            this.cbLX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLX.FormattingEnabled = true;
            this.cbLX.Location = new System.Drawing.Point(60, 97);
            this.cbLX.Name = "cbLX";
            this.cbLX.Size = new System.Drawing.Size(157, 20);
            this.cbLX.TabIndex = 13;
            this.coreBind.SetVerification(this.cbLX, null);
            // 
            // txtJLD
            // 
            this.txtJLD.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtJLD, null);
            this.txtJLD.Font = new System.Drawing.Font("宋体", 11F);
            this.txtJLD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtJLD.Location = new System.Drawing.Point(174, 256);
            this.txtJLD.MaxLength = 8;
            this.txtJLD.Multiline = true;
            this.txtJLD.Name = "txtJLD";
            this.txtJLD.ReadOnly = true;
            this.txtJLD.Size = new System.Drawing.Size(73, 21);
            this.txtJLD.TabIndex = 17;
            this.coreBind.SetVerification(this.txtJLD, null);
            // 
            // label26
            // 
            this.coreBind.SetDatabasecommand(this.label26, null);
            this.label26.Location = new System.Drawing.Point(366, 255);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(42, 24);
            this.label26.TabIndex = 589;
            this.label26.Text = "班次";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label26, null);
            // 
            // label21
            // 
            this.coreBind.SetDatabasecommand(this.label21, null);
            this.label21.Location = new System.Drawing.Point(192, 232);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 24);
            this.label21.TabIndex = 587;
            this.label21.Text = "重量(t)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label21, null);
            // 
            // label16
            // 
            this.coreBind.SetDatabasecommand(this.label16, null);
            this.label16.Location = new System.Drawing.Point(223, 97);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 24);
            this.label16.TabIndex = 581;
            this.label16.Text = "承运单位";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label16, null);
            // 
            // label14
            // 
            this.coreBind.SetDatabasecommand(this.label14, null);
            this.label14.Location = new System.Drawing.Point(223, 119);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(71, 24);
            this.label14.TabIndex = 580;
            this.label14.Text = "收货单位";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label14, null);
            // 
            // label15
            // 
            this.coreBind.SetDatabasecommand(this.label15, null);
            this.label15.Location = new System.Drawing.Point(2, 119);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 24);
            this.label15.TabIndex = 579;
            this.label15.Text = "发货单位";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label15, null);
            // 
            // label11
            // 
            this.coreBind.SetDatabasecommand(this.label11, null);
            this.label11.Location = new System.Drawing.Point(4, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 24);
            this.label11.TabIndex = 578;
            this.label11.Text = "流向";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label11, null);
            // 
            // label13
            // 
            this.coreBind.SetDatabasecommand(this.label13, null);
            this.label13.Location = new System.Drawing.Point(225, 74);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 24);
            this.label13.TabIndex = 577;
            this.label13.Text = "物料名称";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label13, null);
            // 
            // label10
            // 
            this.coreBind.SetDatabasecommand(this.label10, null);
            this.label10.Location = new System.Drawing.Point(2, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 24);
            this.label10.TabIndex = 574;
            this.label10.Text = "炉号";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label10, null);
            // 
            // txtLH
            // 
            this.txtLH.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtLH, null);
            this.txtLH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtLH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLH.Location = new System.Drawing.Point(60, 28);
            this.txtLH.MaxLength = 10;
            this.txtLH.Multiline = true;
            this.txtLH.Name = "txtLH";
            this.txtLH.Size = new System.Drawing.Size(132, 21);
            this.txtLH.TabIndex = 6;
            this.coreBind.SetVerification(this.txtLH, null);
            this.txtLH.Leave += new System.EventHandler(this.txtLH_Leave);
            this.txtLH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLH_KeyPress);
            // 
            // label8
            // 
            this.coreBind.SetDatabasecommand(this.label8, null);
            this.label8.Location = new System.Drawing.Point(2, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 24);
            this.label8.TabIndex = 570;
            this.label8.Text = "合同号";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label8, null);
            // 
            // txtHTH
            // 
            this.txtHTH.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtHTH, null);
            this.txtHTH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtHTH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtHTH.Location = new System.Drawing.Point(60, 51);
            this.txtHTH.MaxLength = 12;
            this.txtHTH.Multiline = true;
            this.txtHTH.Name = "txtHTH";
            this.txtHTH.Size = new System.Drawing.Size(157, 21);
            this.txtHTH.TabIndex = 4;
            this.coreBind.SetVerification(this.txtHTH, null);
            this.txtHTH.Leave += new System.EventHandler(this.txtHTH_Leave);
            // 
            // label6
            // 
            this.coreBind.SetDatabasecommand(this.label6, null);
            this.label6.Location = new System.Drawing.Point(223, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 24);
            this.label6.TabIndex = 568;
            this.label6.Text = "车号";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label6, null);
            // 
            // label5
            // 
            this.coreBind.SetDatabasecommand(this.label5, null);
            this.label5.Location = new System.Drawing.Point(2, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 567;
            this.label5.Text = "卡号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label5, null);
            // 
            // label9
            // 
            this.coreBind.SetDatabasecommand(this.label9, null);
            this.label9.Location = new System.Drawing.Point(1, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 24);
            this.label9.TabIndex = 576;
            this.label9.Text = "支数";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label9, null);
            // 
            // txtCZH
            // 
            this.txtCZH.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtCZH, null);
            this.txtCZH.Font = new System.Drawing.Font("宋体", 11F);
            this.txtCZH.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCZH.Location = new System.Drawing.Point(60, 2);
            this.txtCZH.MaxLength = 5;
            this.txtCZH.Name = "txtCZH";
            this.txtCZH.Size = new System.Drawing.Size(157, 24);
            this.txtCZH.TabIndex = 1;
            this.coreBind.SetVerification(this.txtCZH, null);
            this.txtCZH.Leave += new System.EventHandler(this.txtCZH_Leave);
            // 
            // txtCarNo
            // 
            this.txtCarNo.BackColor = System.Drawing.Color.Bisque;
            this.coreBind.SetDatabasecommand(this.txtCarNo, null);
            this.txtCarNo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtCarNo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCarNo.Location = new System.Drawing.Point(60, 2);
            this.txtCarNo.MaxLength = 8;
            this.txtCarNo.Name = "txtCarNo";
            this.txtCarNo.Size = new System.Drawing.Size(157, 24);
            this.txtCarNo.TabIndex = 601;
            this.coreBind.SetVerification(this.txtCarNo, null);
            this.txtCarNo.Visible = false;
            this.txtCarNo.TextChanged += new System.EventHandler(this.txtCarNo_TextChanged);
            // 
            // label20
            // 
            this.coreBind.SetDatabasecommand(this.label20, null);
            this.label20.Location = new System.Drawing.Point(1, 232);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 24);
            this.label20.TabIndex = 695;
            this.label20.Text = "票据编号";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label20, null);
            // 
            // cbLS
            // 
            this.coreBind.SetDatabasecommand(this.cbLS, null);
            this.cbLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLS.FormattingEnabled = true;
            this.cbLS.Items.AddRange(new object[] {
            "",
            "还不是最后一炉",
            "已是最后一炉"});
            this.cbLS.Location = new System.Drawing.Point(366, 232);
            this.cbLS.Name = "cbLS";
            this.cbLS.Size = new System.Drawing.Size(91, 20);
            this.cbLS.TabIndex = 699;
            this.coreBind.SetVerification(this.cbLS, null);
            // 
            // label23
            // 
            this.coreBind.SetDatabasecommand(this.label23, null);
            this.label23.Location = new System.Drawing.Point(325, 227);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(40, 32);
            this.label23.TabIndex = 698;
            this.label23.Text = "一车多炉";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.coreBind.SetVerification(this.label23, null);
            // 
            // txtLH3
            // 
            this.txtLH3.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtLH3, null);
            this.txtLH3.Font = new System.Drawing.Font("宋体", 11F);
            this.txtLH3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLH3.Location = new System.Drawing.Point(328, 28);
            this.txtLH3.MaxLength = 10;
            this.txtLH3.Multiline = true;
            this.txtLH3.Name = "txtLH3";
            this.txtLH3.Size = new System.Drawing.Size(132, 21);
            this.txtLH3.TabIndex = 8;
            this.coreBind.SetVerification(this.txtLH3, null);
            this.txtLH3.Leave += new System.EventHandler(this.txtLH3_Leave);
            // 
            // txtLH2
            // 
            this.txtLH2.BackColor = System.Drawing.SystemColors.Window;
            this.coreBind.SetDatabasecommand(this.txtLH2, null);
            this.txtLH2.Font = new System.Drawing.Font("宋体", 11F);
            this.txtLH2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLH2.Location = new System.Drawing.Point(194, 28);
            this.txtLH2.MaxLength = 10;
            this.txtLH2.Multiline = true;
            this.txtLH2.Name = "txtLH2";
            this.txtLH2.Size = new System.Drawing.Size(132, 21);
            this.txtLH2.TabIndex = 7;
            this.coreBind.SetVerification(this.txtLH2, null);
            this.txtLH2.Leave += new System.EventHandler(this.txtLH2_Leave);
            // 
            // ultraDockManager1
            // 
            this.ultraDockManager1.CompressUnpinnedTabs = false;
            dockAreaPane1.DockedBefore = new System.Guid("7e44afaa-31e1-457e-913f-0ae6ac575567");
            dockableControlPane1.Control = this.panelYYBF;
            dockableControlPane1.FlyoutSize = new System.Drawing.Size(136, -1);
            dockableControlPane1.OriginalControlBounds = new System.Drawing.Rectangle(142, -23, 200, 100);
            dockableControlPane1.Pinned = false;
            dockableControlPane1.Size = new System.Drawing.Size(100, 100);
            dockableControlPane1.Text = "语音播报";
            dockAreaPane1.Panes.AddRange(new Infragistics.Win.UltraWinDock.DockablePaneBase[] {
            dockableControlPane1});
            dockAreaPane1.Size = new System.Drawing.Size(95, 666);
            dockableControlPane2.Control = this.panelSPKZ;
            dockableControlPane2.FlyoutSize = new System.Drawing.Size(131, -1);
            dockableControlPane2.OriginalControlBounds = new System.Drawing.Rectangle(189, 132, 200, 100);
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
            this.ultraDockManager1.BeforeDockChange += new Infragistics.Win.UltraWinDock.BeforeDockChangeEventHandler(this.ultraDockManager1_BeforeDockChange);
            // 
            // _WeighMeasureInfoUnpinnedTabAreaLeft
            // 
            this.coreBind.SetDatabasecommand(this._WeighMeasureInfoUnpinnedTabAreaLeft, null);
            this._WeighMeasureInfoUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._WeighMeasureInfoUnpinnedTabAreaLeft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._WeighMeasureInfoUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 0);
            this._WeighMeasureInfoUnpinnedTabAreaLeft.Name = "_WeighMeasureInfoUnpinnedTabAreaLeft";
            this._WeighMeasureInfoUnpinnedTabAreaLeft.Owner = this.ultraDockManager1;
            this._WeighMeasureInfoUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 607);
            this._WeighMeasureInfoUnpinnedTabAreaLeft.TabIndex = 5;
            this.coreBind.SetVerification(this._WeighMeasureInfoUnpinnedTabAreaLeft, null);
            // 
            // _WeighMeasureInfoUnpinnedTabAreaRight
            // 
            this.coreBind.SetDatabasecommand(this._WeighMeasureInfoUnpinnedTabAreaRight, null);
            this._WeighMeasureInfoUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._WeighMeasureInfoUnpinnedTabAreaRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._WeighMeasureInfoUnpinnedTabAreaRight.Location = new System.Drawing.Point(1007, 0);
            this._WeighMeasureInfoUnpinnedTabAreaRight.Name = "_WeighMeasureInfoUnpinnedTabAreaRight";
            this._WeighMeasureInfoUnpinnedTabAreaRight.Owner = this.ultraDockManager1;
            this._WeighMeasureInfoUnpinnedTabAreaRight.Size = new System.Drawing.Size(21, 607);
            this._WeighMeasureInfoUnpinnedTabAreaRight.TabIndex = 6;
            this.coreBind.SetVerification(this._WeighMeasureInfoUnpinnedTabAreaRight, null);
            // 
            // _WeighMeasureInfoUnpinnedTabAreaTop
            // 
            this.coreBind.SetDatabasecommand(this._WeighMeasureInfoUnpinnedTabAreaTop, null);
            this._WeighMeasureInfoUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._WeighMeasureInfoUnpinnedTabAreaTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._WeighMeasureInfoUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 0);
            this._WeighMeasureInfoUnpinnedTabAreaTop.Name = "_WeighMeasureInfoUnpinnedTabAreaTop";
            this._WeighMeasureInfoUnpinnedTabAreaTop.Owner = this.ultraDockManager1;
            this._WeighMeasureInfoUnpinnedTabAreaTop.Size = new System.Drawing.Size(1007, 0);
            this._WeighMeasureInfoUnpinnedTabAreaTop.TabIndex = 7;
            this.coreBind.SetVerification(this._WeighMeasureInfoUnpinnedTabAreaTop, null);
            // 
            // _WeighMeasureInfoUnpinnedTabAreaBottom
            // 
            this.coreBind.SetDatabasecommand(this._WeighMeasureInfoUnpinnedTabAreaBottom, null);
            this._WeighMeasureInfoUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._WeighMeasureInfoUnpinnedTabAreaBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._WeighMeasureInfoUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 607);
            this._WeighMeasureInfoUnpinnedTabAreaBottom.Name = "_WeighMeasureInfoUnpinnedTabAreaBottom";
            this._WeighMeasureInfoUnpinnedTabAreaBottom.Owner = this.ultraDockManager1;
            this._WeighMeasureInfoUnpinnedTabAreaBottom.Size = new System.Drawing.Size(1007, 0);
            this._WeighMeasureInfoUnpinnedTabAreaBottom.TabIndex = 8;
            this.coreBind.SetVerification(this._WeighMeasureInfoUnpinnedTabAreaBottom, null);
            // 
            // _WeighMeasureInfoAutoHideControl
            // 
            this._WeighMeasureInfoAutoHideControl.Controls.Add(this.dockableWindow2);
            this._WeighMeasureInfoAutoHideControl.Controls.Add(this.dockableWindow1);
            this.coreBind.SetDatabasecommand(this._WeighMeasureInfoAutoHideControl, null);
            this._WeighMeasureInfoAutoHideControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._WeighMeasureInfoAutoHideControl.Location = new System.Drawing.Point(991, 0);
            this._WeighMeasureInfoAutoHideControl.Name = "_WeighMeasureInfoAutoHideControl";
            this._WeighMeasureInfoAutoHideControl.Owner = this.ultraDockManager1;
            this._WeighMeasureInfoAutoHideControl.Size = new System.Drawing.Size(16, 607);
            this._WeighMeasureInfoAutoHideControl.TabIndex = 9;
            this.coreBind.SetVerification(this._WeighMeasureInfoAutoHideControl, null);
            // 
            // dockableWindow2
            // 
            this.dockableWindow2.Controls.Add(this.panelYYBF);
            this.coreBind.SetDatabasecommand(this.dockableWindow2, null);
            this.dockableWindow2.Location = new System.Drawing.Point(-10000, 0);
            this.dockableWindow2.Name = "dockableWindow2";
            this.dockableWindow2.Owner = this.ultraDockManager1;
            this.dockableWindow2.Size = new System.Drawing.Size(136, 607);
            this.dockableWindow2.TabIndex = 606;
            this.coreBind.SetVerification(this.dockableWindow2, null);
            // 
            // dockableWindow1
            // 
            this.dockableWindow1.Controls.Add(this.panelSPKZ);
            this.coreBind.SetDatabasecommand(this.dockableWindow1, null);
            this.dockableWindow1.Location = new System.Drawing.Point(-10000, 0);
            this.dockableWindow1.Name = "dockableWindow1";
            this.dockableWindow1.Owner = this.ultraDockManager1;
            this.dockableWindow1.Size = new System.Drawing.Size(131, 607);
            this.dockableWindow1.TabIndex = 607;
            this.coreBind.SetVerification(this.dockableWindow1, null);
            // 
            // panel38
            // 
            this.panel38.Controls.Add(this.pictureBox38);
            this.coreBind.SetDatabasecommand(this.panel38, null);
            this.panel38.Location = new System.Drawing.Point(325, 426);
            this.panel38.Name = "panel38";
            this.panel38.Size = new System.Drawing.Size(316, 213);
            this.panel38.TabIndex = 6;
            this.coreBind.SetVerification(this.panel38, null);
            // 
            // pictureBox38
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox38, null);
            this.pictureBox38.Location = new System.Drawing.Point(0, 0);
            this.pictureBox38.Name = "pictureBox38";
            this.pictureBox38.Size = new System.Drawing.Size(100, 50);
            this.pictureBox38.TabIndex = 0;
            this.pictureBox38.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox38, null);
            // 
            // panel37
            // 
            this.panel37.Controls.Add(this.pictureBox37);
            this.coreBind.SetDatabasecommand(this.panel37, null);
            this.panel37.Location = new System.Drawing.Point(3, 426);
            this.panel37.Name = "panel37";
            this.panel37.Size = new System.Drawing.Size(316, 213);
            this.panel37.TabIndex = 5;
            this.coreBind.SetVerification(this.panel37, null);
            // 
            // pictureBox37
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox37, null);
            this.pictureBox37.Location = new System.Drawing.Point(0, 0);
            this.pictureBox37.Name = "pictureBox37";
            this.pictureBox37.Size = new System.Drawing.Size(100, 50);
            this.pictureBox37.TabIndex = 0;
            this.pictureBox37.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox37, null);
            // 
            // panel36
            // 
            this.panel36.Controls.Add(this.pictureBox36);
            this.coreBind.SetDatabasecommand(this.panel36, null);
            this.panel36.Location = new System.Drawing.Point(325, 214);
            this.panel36.Name = "panel36";
            this.panel36.Size = new System.Drawing.Size(316, 213);
            this.panel36.TabIndex = 4;
            this.coreBind.SetVerification(this.panel36, null);
            // 
            // pictureBox36
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox36, null);
            this.pictureBox36.Location = new System.Drawing.Point(0, 0);
            this.pictureBox36.Name = "pictureBox36";
            this.pictureBox36.Size = new System.Drawing.Size(100, 50);
            this.pictureBox36.TabIndex = 0;
            this.pictureBox36.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox36, null);
            // 
            // panel33
            // 
            this.panel33.Controls.Add(this.pictureBox33);
            this.coreBind.SetDatabasecommand(this.panel33, null);
            this.panel33.Location = new System.Drawing.Point(3, 213);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(316, 213);
            this.panel33.TabIndex = 3;
            this.coreBind.SetVerification(this.panel33, null);
            // 
            // pictureBox33
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox33, null);
            this.pictureBox33.Location = new System.Drawing.Point(0, 0);
            this.pictureBox33.Name = "pictureBox33";
            this.pictureBox33.Size = new System.Drawing.Size(100, 50);
            this.pictureBox33.TabIndex = 0;
            this.pictureBox33.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox33, null);
            // 
            // panel32
            // 
            this.panel32.Controls.Add(this.pictureBox32);
            this.coreBind.SetDatabasecommand(this.panel32, null);
            this.panel32.Location = new System.Drawing.Point(325, 1);
            this.panel32.Name = "panel32";
            this.panel32.Size = new System.Drawing.Size(316, 207);
            this.panel32.TabIndex = 2;
            this.coreBind.SetVerification(this.panel32, null);
            // 
            // pictureBox32
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox32, null);
            this.pictureBox32.Location = new System.Drawing.Point(0, 0);
            this.pictureBox32.Name = "pictureBox32";
            this.pictureBox32.Size = new System.Drawing.Size(100, 50);
            this.pictureBox32.TabIndex = 0;
            this.pictureBox32.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox32, null);
            // 
            // panel31
            // 
            this.panel31.Controls.Add(this.pictureBox31);
            this.coreBind.SetDatabasecommand(this.panel31, null);
            this.panel31.Location = new System.Drawing.Point(3, 1);
            this.panel31.Name = "panel31";
            this.panel31.Size = new System.Drawing.Size(316, 207);
            this.panel31.TabIndex = 1;
            this.coreBind.SetVerification(this.panel31, null);
            // 
            // pictureBox31
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox31, null);
            this.pictureBox31.Location = new System.Drawing.Point(0, 0);
            this.pictureBox31.Name = "pictureBox31";
            this.pictureBox31.Size = new System.Drawing.Size(100, 50);
            this.pictureBox31.TabIndex = 0;
            this.pictureBox31.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox31, null);
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.pictureBox16);
            this.coreBind.SetDatabasecommand(this.panel18, null);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel18.Location = new System.Drawing.Point(322, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(328, 215);
            this.panel18.TabIndex = 1;
            this.coreBind.SetVerification(this.panel18, null);
            // 
            // pictureBox16
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox16, null);
            this.pictureBox16.Location = new System.Drawing.Point(0, 0);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(100, 50);
            this.pictureBox16.TabIndex = 0;
            this.pictureBox16.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox16, null);
            // 
            // panel19
            // 
            this.panel19.Controls.Add(this.pictureBox17);
            this.coreBind.SetDatabasecommand(this.panel19, null);
            this.panel19.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel19.Location = new System.Drawing.Point(0, 0);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(322, 215);
            this.panel19.TabIndex = 0;
            this.coreBind.SetVerification(this.panel19, null);
            // 
            // pictureBox17
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox17, null);
            this.pictureBox17.Location = new System.Drawing.Point(0, 0);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(100, 50);
            this.pictureBox17.TabIndex = 0;
            this.pictureBox17.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox17, null);
            // 
            // picFDTP
            // 
            this.coreBind.SetDatabasecommand(this.picFDTP, null);
            this.picFDTP.Location = new System.Drawing.Point(327, 118);
            this.picFDTP.Name = "picFDTP";
            this.picFDTP.Size = new System.Drawing.Size(10, 10);
            this.picFDTP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFDTP.TabIndex = 605;
            this.picFDTP.TabStop = false;
            this.coreBind.SetVerification(this.picFDTP, null);
            this.picFDTP.DoubleClick += new System.EventHandler(this.picFDTP_DoubleClick);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // panelYCJL
            // 
            this.panelYCJL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panelYCJL.Controls.Add(this.panel12);
            this.panelYCJL.Controls.Add(this.panel13);
            this.panelYCJL.Controls.Add(this.panel14);
            this.panelYCJL.Controls.Add(this.panel15);
            this.panelYCJL.Controls.Add(this.panel16);
            this.panelYCJL.Controls.Add(this.panel17);
            this.coreBind.SetDatabasecommand(this.panelYCJL, null);
            this.panelYCJL.Location = new System.Drawing.Point(262, 27);
            this.panelYCJL.Name = "panelYCJL";
            this.panelYCJL.Size = new System.Drawing.Size(10, 626);
            this.panelYCJL.TabIndex = 17;
            this.coreBind.SetVerification(this.panelYCJL, null);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.ultraChart2);
            this.panel12.Controls.Add(this.pictureBox10);
            this.coreBind.SetDatabasecommand(this.panel12, null);
            this.panel12.Location = new System.Drawing.Point(325, 420);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(312, 209);
            this.panel12.TabIndex = 6;
            this.coreBind.SetVerification(this.panel12, null);
            // 
            //			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
            //			'ChartType' must be persisted ahead of any Axes change made in design time.
            //		
            this.ultraChart2.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.LineChart;
            // 
            // ultraChart2
            // 
            this.ultraChart2.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement2.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement2.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ultraChart2.Axis.PE = paintElement2;
            this.ultraChart2.Axis.X.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart2.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart2.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart2.Axis.X.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart2.Axis.X.Labels.SeriesLabels.FormatString = "";
            this.ultraChart2.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart2.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.X.Labels.Visible = false;
            this.ultraChart2.Axis.X.LineThickness = 1;
            this.ultraChart2.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart2.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.X.MajorGridLines.Visible = true;
            this.ultraChart2.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart2.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.X.MinorGridLines.Visible = false;
            this.ultraChart2.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart2.Axis.X.Visible = false;
            this.ultraChart2.Axis.X2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart2.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart2.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart2.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart2.Axis.X2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart2.Axis.X2.Labels.SeriesLabels.FormatString = "";
            this.ultraChart2.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart2.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart2.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.X2.Labels.Visible = false;
            this.ultraChart2.Axis.X2.LineThickness = 1;
            this.ultraChart2.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart2.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.X2.MajorGridLines.Visible = true;
            this.ultraChart2.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart2.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.X2.MinorGridLines.Visible = false;
            this.ultraChart2.Axis.X2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart2.Axis.X2.Visible = false;
            this.ultraChart2.Axis.Y.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart2.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart2.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart2.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Y.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart2.Axis.Y.Labels.SeriesLabels.FormatString = "";
            this.ultraChart2.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart2.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Y.LineThickness = 1;
            this.ultraChart2.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart2.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Y.MajorGridLines.Visible = true;
            this.ultraChart2.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart2.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Y.MinorGridLines.Visible = false;
            this.ultraChart2.Axis.Y.TickmarkInterval = 50;
            this.ultraChart2.Axis.Y.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart2.Axis.Y.Visible = true;
            this.ultraChart2.Axis.Y2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart2.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart2.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Y2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart2.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            this.ultraChart2.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Y2.Labels.Visible = false;
            this.ultraChart2.Axis.Y2.LineThickness = 1;
            this.ultraChart2.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart2.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Y2.MajorGridLines.Visible = true;
            this.ultraChart2.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart2.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Y2.MinorGridLines.Visible = false;
            this.ultraChart2.Axis.Y2.TickmarkInterval = 50;
            this.ultraChart2.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart2.Axis.Y2.Visible = false;
            this.ultraChart2.Axis.Z.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart2.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.Z.Labels.ItemFormatString = "";
            this.ultraChart2.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Z.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart2.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Z.Labels.Visible = false;
            this.ultraChart2.Axis.Z.LineThickness = 1;
            this.ultraChart2.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart2.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Z.MajorGridLines.Visible = true;
            this.ultraChart2.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart2.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Z.MinorGridLines.Visible = false;
            this.ultraChart2.Axis.Z.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart2.Axis.Z.Visible = false;
            this.ultraChart2.Axis.Z2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart2.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.Z2.Labels.ItemFormatString = "";
            this.ultraChart2.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Z2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart2.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart2.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart2.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart2.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart2.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart2.Axis.Z2.Labels.Visible = false;
            this.ultraChart2.Axis.Z2.LineThickness = 1;
            this.ultraChart2.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart2.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Z2.MajorGridLines.Visible = true;
            this.ultraChart2.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart2.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart2.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart2.Axis.Z2.MinorGridLines.Visible = false;
            this.ultraChart2.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart2.Axis.Z2.Visible = false;
            this.ultraChart2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ultraChart2.ColorModel.AlphaLevel = ((byte)(150));
            this.ultraChart2.ColorModel.ColorBegin = System.Drawing.Color.Pink;
            this.ultraChart2.ColorModel.ColorEnd = System.Drawing.Color.DarkRed;
            this.ultraChart2.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            this.coreBind.SetDatabasecommand(this.ultraChart2, null);
            this.ultraChart2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraChart2.Effects.Effects.Add(gradientEffect2);
            this.ultraChart2.Location = new System.Drawing.Point(0, 0);
            this.ultraChart2.Name = "ultraChart2";
            this.ultraChart2.Size = new System.Drawing.Size(312, 209);
            this.ultraChart2.TabIndex = 2;
            this.ultraChart2.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.ultraChart2.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            this.coreBind.SetVerification(this.ultraChart2, null);
            // 
            // pictureBox10
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox10, null);
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(0, 0);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(312, 18);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox10.TabIndex = 0;
            this.pictureBox10.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox10, null);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.pictureBox11);
            this.coreBind.SetDatabasecommand(this.panel13, null);
            this.panel13.Location = new System.Drawing.Point(3, 420);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(312, 209);
            this.panel13.TabIndex = 5;
            this.coreBind.SetVerification(this.panel13, null);
            // 
            // pictureBox11
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox11, null);
            this.pictureBox11.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(0, 0);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(312, 205);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox11.TabIndex = 0;
            this.pictureBox11.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox11, null);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.pictureBox12);
            this.coreBind.SetDatabasecommand(this.panel14, null);
            this.panel14.Location = new System.Drawing.Point(325, 210);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(312, 209);
            this.panel14.TabIndex = 4;
            this.coreBind.SetVerification(this.panel14, null);
            // 
            // pictureBox12
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox12, null);
            this.pictureBox12.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(0, 0);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(312, 205);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox12.TabIndex = 0;
            this.pictureBox12.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox12, null);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.pictureBox13);
            this.coreBind.SetDatabasecommand(this.panel15, null);
            this.panel15.Location = new System.Drawing.Point(3, 210);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(312, 209);
            this.panel15.TabIndex = 3;
            this.coreBind.SetVerification(this.panel15, null);
            // 
            // pictureBox13
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox13, null);
            this.pictureBox13.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox13.Image")));
            this.pictureBox13.Location = new System.Drawing.Point(0, 0);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(312, 205);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox13.TabIndex = 0;
            this.pictureBox13.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox13, null);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.pictureBox14);
            this.coreBind.SetDatabasecommand(this.panel16, null);
            this.panel16.Location = new System.Drawing.Point(325, 1);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(312, 209);
            this.panel16.TabIndex = 2;
            this.coreBind.SetVerification(this.panel16, null);
            // 
            // pictureBox14
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox14, null);
            this.pictureBox14.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.Location = new System.Drawing.Point(0, 0);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(312, 205);
            this.pictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox14.TabIndex = 0;
            this.pictureBox14.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox14, null);
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.pictureBox15);
            this.coreBind.SetDatabasecommand(this.panel17, null);
            this.panel17.Location = new System.Drawing.Point(3, 1);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(312, 209);
            this.panel17.TabIndex = 1;
            this.coreBind.SetVerification(this.panel17, null);
            // 
            // pictureBox15
            // 
            this.coreBind.SetDatabasecommand(this.pictureBox15, null);
            this.pictureBox15.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(0, 0);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(312, 209);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox15.TabIndex = 0;
            this.pictureBox15.TabStop = false;
            this.coreBind.SetVerification(this.pictureBox15, null);
            this.pictureBox15.DoubleClick += new System.EventHandler(this.pictureBox15_DoubleClick);
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
            this.windowDockingArea1.TabIndex = 10;
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
            this.windowDockingArea2.TabIndex = 11;
            this.coreBind.SetVerification(this.windowDockingArea2, null);
            // 
            // WeighMeasureInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 607);
            this.Controls.Add(this._WeighMeasureInfoAutoHideControl);
            this.Controls.Add(this.picFDTP);
            this.Controls.Add(this.panelYCJL);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.windowDockingArea1);
            this.Controls.Add(this.windowDockingArea2);
            this.Controls.Add(this._WeighMeasureInfoUnpinnedTabAreaTop);
            this.Controls.Add(this._WeighMeasureInfoUnpinnedTabAreaBottom);
            this.Controls.Add(this._WeighMeasureInfoUnpinnedTabAreaLeft);
            this.Controls.Add(this._WeighMeasureInfoUnpinnedTabAreaRight);
            this.coreBind.SetDatabasecommand(this, null);
            this.KeyPreview = true;
            this.Name = "WeighMeasureInfo";
            this.Tag = "Weigh";
            this.Text = "汽车称重计量信息";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.WeighMeasureInfo_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WeighMeasureInfo_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WeighMeasureInfo_FormClosing);
            this.ultraTabPageControl3.ResumeLayout(false);
            this.panelYCSP.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel5)).EndInit();
            this.panel25.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel6)).EndInit();
            this.panel21.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel4)).EndInit();
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable11)).EndInit();
            this.ultraTabPageControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHT)).EndInit();
            this.panelYYBF.ResumeLayout(false);
            this.panel23.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid4)).EndInit();
            this.panelSPKZ.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoChannel1)).EndInit();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabControl1)).EndInit();
            this.ultraTabControl1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDockManager1)).EndInit();
            this._WeighMeasureInfoAutoHideControl.ResumeLayout(false);
            this.dockableWindow2.ResumeLayout(false);
            this.dockableWindow1.ResumeLayout(false);
            this.panel38.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox38)).EndInit();
            this.panel37.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).EndInit();
            this.panel36.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox36)).EndInit();
            this.panel33.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox33)).EndInit();
            this.panel32.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox32)).EndInit();
            this.panel31.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox31)).EndInit();
            this.panel18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            this.panel19.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFDTP)).EndInit();
            this.panelYCJL.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            this.panel15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.panel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            this.panel17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox txtZZ;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private System.Windows.Forms.Panel panel1_Fill_Panel;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbYS;
        private System.Windows.Forms.Label lbWD;
        private System.Windows.Forms.Button btnQL;
        private System.Windows.Forms.GroupBox groupBox1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel10;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private System.Windows.Forms.Button btnZMDKG;
        private System.Windows.Forms.Button btnHL;
        private System.Windows.Forms.Button btnHDHW;
        private System.Windows.Forms.Button btnQDHW;
        private System.Windows.Forms.Panel panelYYBF;
        private System.Windows.Forms.Panel panelSPKZ;
        private Infragistics.Win.UltraWinDock.UltraDockManager ultraDockManager1;
        private Infragistics.Win.UltraWinDock.AutoHideControl _WeighMeasureInfoAutoHideControl;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow2;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _WeighMeasureInfoUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _WeighMeasureInfoUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _WeighMeasureInfoUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1;
        private Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea2;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _WeighMeasureInfoUnpinnedTabAreaRight;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl ultraTabControl1;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private System.Windows.Forms.Button btnBC;
        private System.Windows.Forms.Button btnDS;
        private System.Data.DataTable dataTable2;
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
        private System.Data.DataColumn dataColumn15;
        private System.Data.DataColumn dataColumn16;
        private System.Data.DataColumn dataColumn17;
        private System.Data.DataColumn dataColumn18;
        private System.Data.DataColumn dataColumn19;
        private System.Data.DataColumn dataColumn20;
        private System.Windows.Forms.CheckBox chbQXPZ;
        private System.Windows.Forms.ComboBox cbCH;
        private System.Windows.Forms.TextBox txtJLY;
        private System.Windows.Forms.ComboBox cbLX;
        private System.Windows.Forms.TextBox txtJLD;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtBC;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLH;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHTH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCZH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel38;
        private System.Windows.Forms.PictureBox pictureBox38;
        private System.Windows.Forms.Panel panel37;
        private System.Windows.Forms.PictureBox pictureBox37;
        private System.Windows.Forms.Panel panel36;
        private System.Windows.Forms.PictureBox pictureBox36;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.PictureBox pictureBox33;
        private System.Windows.Forms.Panel panel32;
        private System.Windows.Forms.PictureBox pictureBox32;
        private System.Windows.Forms.Panel panel31;
        private System.Windows.Forms.PictureBox pictureBox31;
        private System.Windows.Forms.TextBox txtTDL;
        private System.Data.DataTable dataTable3;
        private System.Data.DataColumn dataColumn21;
        private System.Data.DataColumn dataColumn22;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid4;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid3;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl2;
        private System.Windows.Forms.ComboBox cbJLLX;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtXSZL;
        private System.Data.DataTable dataTable4;
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
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid2;
        private System.Data.DataTable dataTable5;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Data.DataColumn dataColumn36;
        private System.Data.DataColumn dataColumn37;
        private System.Data.DataColumn dataColumn38;
        private System.Data.DataColumn dataColumn39;
        private System.Data.DataColumn dataColumn40;
        private System.Data.DataColumn dataColumn41;
        private System.Data.DataColumn dataColumn42;
        private System.Data.DataColumn dataColumn43;
        private System.Data.DataColumn dataColumn44;
        private System.Data.DataColumn dataColumn45;
        private System.Data.DataColumn dataColumn46;
        private System.Data.DataColumn dataColumn47;
        private System.Data.DataColumn dataColumn48;
        private System.Data.DataColumn dataColumn49;
        private System.Data.DataColumn dataColumn50;
        private System.Windows.Forms.ComboBox cbCH1;
        private System.Data.DataColumn dataColumn51;
        private System.Data.DataColumn dataColumn52;
        private System.Windows.Forms.TextBox txtLH3;
        private System.Windows.Forms.TextBox txtLH2;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private System.Windows.Forms.Panel panelYCSP;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.PictureBox picFDTP;
        private System.Windows.Forms.Panel panel22;
        private System.Data.DataColumn dataColumn53;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Data.DataTable dataTable6;
        private System.Data.DataColumn dataColumn54;
        private System.Data.DataColumn dataColumn55;
        private System.Data.DataColumn dataColumn56;
        private System.Data.DataColumn dataColumn57;
        private System.Data.DataColumn dataColumn58;
        private System.Data.DataColumn dataColumn59;
        private System.Data.DataColumn dataColumn60;
        private System.Data.DataColumn dataColumn61;
        private System.Data.DataColumn dataColumn62;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn6;
        private Infragistics.Win.UltraWinChart.UltraChart ultraChart1;
        private System.Windows.Forms.PictureBox picHT;
        private System.Data.DataColumn dataColumn63;
        private System.Data.DataColumn dataColumn64;
        private System.Data.DataColumn dataColumn65;
        private System.Data.DataColumn dataColumn66;
        private System.Data.DataColumn dataColumn67;
        private System.Data.DataColumn dataColumn68;
        private System.Data.DataColumn dataColumn69;
        private System.Data.DataColumn dataColumn70;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox chbSFYC;
        private System.Windows.Forms.Button btnSD;
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
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Panel panel23;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid5;
        private CoolInfraredRay StatusBack;
        private CoolInfraredRay StatusFront;
        private CoolIndicator StatusLight;
        private CoolIndicator StatusRedGreen;
        private System.Windows.Forms.TextBox txtCarNo;
        private System.Data.DataColumn dataColumn92;
        private System.Data.DataColumn dataColumn93;
        private System.Data.DataColumn dataColumn94;
        private System.Data.DataColumn dataColumn95;
        private System.Data.DataColumn dataColumn96;
        private System.Data.DataColumn dataColumn97;
        private System.Data.DataColumn dataColumn98;
        private System.Data.DataColumn dataColumn99;
        private System.Data.DataColumn dataColumn100;
        private System.Data.DataColumn dataColumn102;
        private System.Data.DataColumn dataColumn101;
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
        private System.Data.DataTable dataTable7;
        private System.Data.DataColumn dataColumn116;
        private System.Data.DataColumn dataColumn117;
        private System.Data.DataColumn dataColumn118;
        private System.Data.DataColumn dataColumn119;
        private System.Data.DataTable dataTable8;
        private System.Data.DataColumn dataColumn120;
        private System.Data.DataColumn dataColumn121;
        private System.Data.DataColumn dataColumn122;
        private System.Data.DataTable dataTable9;
        private System.Data.DataColumn dataColumn123;
        private System.Data.DataColumn dataColumn124;
        private System.Data.DataColumn dataColumn125;
        private System.Data.DataColumn dataColumn126;
        private System.Data.DataTable dataTable10;
        private System.Data.DataColumn dataColumn127;
        private System.Data.DataColumn dataColumn128;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Data.DataTable dataTable11;
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
        private System.Data.DataColumn dataColumn156;
        private System.Data.DataColumn dataColumn157;
        private System.Data.DataColumn dataColumn158;
        private System.Data.DataColumn dataColumn159;
        private System.Data.DataColumn dataColumn160;
        private System.Data.DataColumn dataColumn161;
        private System.Data.DataColumn dataColumn162;
        private System.Data.DataColumn dataColumn163;
        private System.Data.DataColumn dataColumn164;
        private System.Data.DataColumn dataColumn165;
        private System.Data.DataColumn dataColumn166;
        private System.Data.DataColumn dataColumn167;
        private System.Data.DataColumn dataColumn168;
        private System.Data.DataColumn dataColumn169;
        private System.Data.DataColumn dataColumn170;
        private System.Data.DataColumn dataColumn171;
        private System.Data.DataColumn dataColumn172;
        private System.Data.DataColumn dataColumn173;
        private System.Data.DataColumn dataColumn174;
        private System.Data.DataColumn dataColumn175;
        private System.Data.DataColumn dataColumn176;
        private System.Data.DataColumn dataColumn177;
        private System.Data.DataColumn dataColumn178;
        private System.Data.DataColumn dataColumn179;
        private System.Data.DataColumn dataColumn180;
        private System.Data.DataColumn dataColumn181;
        private System.Data.DataColumn dataColumn182;
        private System.Data.DataColumn dataColumn183;
        private System.Data.DataColumn dataColumn184;
        private System.Data.DataColumn dataColumn185;
        private System.Data.DataColumn dataColumn186;
        private System.Data.DataColumn dataColumn187;
        private System.Data.DataColumn dataColumn188;
        private System.Data.DataColumn dataColumn189;
        private System.Data.DataColumn dataColumn190;
        private System.Data.DataColumn dataColumn191;
        private System.Data.DataColumn dataColumn192;
        private System.Data.DataColumn dataColumn193;
        private System.Data.DataColumn dataColumn194;
        private System.Data.DataColumn dataColumn195;
        private System.Data.DataColumn dataColumn196;
        private System.Data.DataColumn dataColumn197;
        private System.Data.DataColumn dataColumn198;
        private System.Data.DataColumn dataColumn199;
        private System.Data.DataColumn dataColumn200;
        private System.Data.DataColumn dataColumn201;
        private System.Data.DataColumn dataColumn202;
        private System.Data.DataColumn dataColumn203;
        private System.Windows.Forms.TextBox txtZS;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtJZ;
        private System.Windows.Forms.TextBox txtPZ;
        private System.Windows.Forms.TextBox txtMZ;
        private System.Windows.Forms.TextBox txtZS3;
        private System.Windows.Forms.TextBox txtZS2;
        private System.Windows.Forms.TextBox txtYKL;
        private System.Windows.Forms.TextBox txtZL;
        private System.Data.DataColumn dataColumn204;
        private System.Data.DataColumn dataColumn205;
        private System.Data.DataColumn dataColumn206;
        private System.Data.DataColumn dataColumn207;
        private System.Data.DataColumn dataColumn208;
        private System.Data.DataColumn dataColumn209;
        private System.Data.DataColumn dataColumn210;
        private System.Data.DataColumn dataColumn211;
        private System.Data.DataColumn dataColumn212;
        private System.Windows.Forms.Button btnWLMC;
        private System.Windows.Forms.Button btnFHDW;
        private System.Windows.Forms.Button btnSHDW;
        private System.Windows.Forms.Button btnCYDW;
        public System.Windows.Forms.ComboBox cbSHDW;
        public System.Windows.Forms.ComboBox cbCYDW;
        public System.Windows.Forms.ComboBox cbFHDW;
        public System.Windows.Forms.ComboBox cbWLMC;
        private System.Windows.Forms.TextBox txtPJBH;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDFJZ;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnGPBC;
        private System.Windows.Forms.ComboBox cbLS;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox txtHTXMH;
        private System.Data.DataColumn dataColumn213;
        private System.Data.DataColumn dataColumn214;
        private System.Data.DataColumn dataColumn215;
        private System.Windows.Forms.TextBox tbReceiverPlace;
        private System.Windows.Forms.TextBox tbSenderPlace;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.TextBox tbCharge;
        private System.Windows.Forms.Label label27;
        private System.Data.DataColumn dataColumn216;
        private System.Windows.Forms.Button button18;
        public System.Windows.Forms.ComboBox cbProvider;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox tbBZ;
        private System.Windows.Forms.Label label29;
        private System.Data.DataColumn dataColumn217;
        private System.Data.DataColumn dataColumn218;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox chb_AutoInfrared;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.PictureBox VideoChannel5;
        private System.Windows.Forms.PictureBox VideoChannel6;
        private System.Windows.Forms.PictureBox VideoChannel4;
        private System.Windows.Forms.Panel panelYCJL;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.Panel panel12;
        private Infragistics.Win.UltraWinChart.UltraChart ultraChart2;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox VideoChannel1;
        private System.Windows.Forms.PictureBox VideoChannel8;
        private System.Windows.Forms.PictureBox VideoChannel7;
        private System.Windows.Forms.PictureBox VideoChannel3;
        private System.Windows.Forms.PictureBox VideoChannel2;
        private Infragistics.Win.UltraWinDock.DockableWindow dockableWindow1;
    }
}
