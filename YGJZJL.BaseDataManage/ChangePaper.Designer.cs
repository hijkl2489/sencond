namespace YGJZJL.BaseDataManage
{
    partial class ChangePaper
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("计量点基础信息表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FN_USEDPRINTPAPER");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("UltraToolbar1");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangePaper");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool3 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Query");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool4 = new Infragistics.Win.UltraWinToolbars.ButtonTool("ChangePaper");
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.tbBf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraGrid1 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.ultraToolbarsManager1 = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._panel1_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._panel1_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraGroupBox1);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Left);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Right);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Top);
            this.panel1.Controls.Add(this._panel1_Toolbars_Dock_Area_Bottom);
            this.coreBind.SetDatabasecommand(this.panel1, null);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 204);
            this.panel1.TabIndex = 0;
            this.coreBind.SetVerification(this.panel1, null);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.tbBf);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.coreBind.SetDatabasecommand(this.ultraGroupBox1, null);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 26);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(992, 178);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "编辑数据";
            this.coreBind.SetVerification(this.ultraGroupBox1, null);
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // tbBf
            // 
            this.coreBind.SetDatabasecommand(this.tbBf, null);
            this.tbBf.Location = new System.Drawing.Point(81, 54);
            this.tbBf.Name = "tbBf";
            this.tbBf.Size = new System.Drawing.Size(197, 21);
            this.tbBf.TabIndex = 1;
            this.coreBind.SetVerification(this.tbBf, null);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.coreBind.SetDatabasecommand(this.label1, null);
            this.label1.Location = new System.Drawing.Point(12, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "计量点";
            this.coreBind.SetVerification(this.label1, null);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ultraGrid1);
            this.coreBind.SetDatabasecommand(this.panel2, null);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 204);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 462);
            this.panel2.TabIndex = 1;
            this.coreBind.SetVerification(this.panel2, null);
            // 
            // ultraGrid1
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid1, null);
            this.ultraGrid1.DataMember = "计量点基础信息表";
            this.ultraGrid1.DataSource = this.dataSet1;
            appearance1.BackColor = System.Drawing.Color.White;
            appearance1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid1.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3});
            this.ultraGrid1.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid1.DisplayLayout.InterBandSpacing = 10;
            this.ultraGrid1.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid1.DisplayLayout.Override.CardAreaAppearance = appearance2;
            this.ultraGrid1.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.ultraGrid1.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance3.ForeColor = System.Drawing.Color.White;
            appearance3.TextHAlignAsString = "Left";
            appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid1.DisplayLayout.Override.HeaderAppearance = appearance3;
            appearance4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.Override.RowAppearance = appearance4;
            appearance6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance6.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorAppearance = appearance6;
            this.ultraGrid1.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid1.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(169)))), ((int)(((byte)(226)))));
            appearance5.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(235)))), ((int)(((byte)(254)))));
            appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance5.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid1.DisplayLayout.Override.SelectedRowAppearance = appearance5;
            this.ultraGrid1.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid1.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid1.Name = "ultraGrid1";
            this.ultraGrid1.Size = new System.Drawing.Size(992, 462);
            this.ultraGrid1.TabIndex = 0;
            this.ultraGrid1.Text = "计量点基础数据";
            this.coreBind.SetVerification(this.ultraGrid1, null);
            this.ultraGrid1.DoubleClick += new System.EventHandler(this.ultraGrid1_DoubleClick);
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
            this.dataColumn3});
            this.dataTable1.TableName = "计量点基础信息表";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "计量点代码";
            this.dataColumn1.ColumnName = "FS_POINTCODE";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "计量点名称";
            this.dataColumn2.ColumnName = "FS_POINTNAME";
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "已用纸张数";
            this.dataColumn3.ColumnName = "FN_USEDPRINTPAPER";
            // 
            // ultraToolbarsManager1
            // 
            this.ultraToolbarsManager1.DesignerFlags = 1;
            this.ultraToolbarsManager1.DockWithinContainer = this.panel1;
            this.ultraToolbarsManager1.MenuAnimationStyle = Infragistics.Win.UltraWinToolbars.MenuAnimationStyle.Random;
            this.ultraToolbarsManager1.Style = Infragistics.Win.UltraWinToolbars.ToolbarStyle.Office2007;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            buttonTool2.InstanceProps.IsFirstInGroup = true;
            ultraToolbar1.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1,
            buttonTool2});
            ultraToolbar1.Text = "UltraToolbar1";
            this.ultraToolbarsManager1.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            buttonTool3.SharedPropsInternal.Caption = "查询";
            buttonTool3.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            buttonTool4.SharedPropsInternal.Caption = "换纸";
            buttonTool4.SharedPropsInternal.DisplayStyle = Infragistics.Win.UltraWinToolbars.ToolDisplayStyle.ImageAndText;
            this.ultraToolbarsManager1.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool3,
            buttonTool4});
            this.ultraToolbarsManager1.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ultraToolbarsManager1_ToolClick);
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
            this._panel1_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 178);
            this._panel1_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Left, null);
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
            this._panel1_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 178);
            this._panel1_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager1;
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
            this._panel1_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Top, null);
            // 
            // _panel1_Toolbars_Dock_Area_Bottom
            // 
            this._panel1_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._panel1_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.coreBind.SetDatabasecommand(this._panel1_Toolbars_Dock_Area_Bottom, null);
            this._panel1_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._panel1_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._panel1_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 204);
            this._panel1_Toolbars_Dock_Area_Bottom.Name = "_panel1_Toolbars_Dock_Area_Bottom";
            this._panel1_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(992, 0);
            this._panel1_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager1;
            this.coreBind.SetVerification(this._panel1_Toolbars_Dock_Area_Bottom, null);
            // 
            // ChangePaper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 666);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.coreBind.SetDatabasecommand(this, null);
            this.Name = "ChangePaper";
            this.Text = "Form1";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.ChangePaper_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager1;
        private System.Windows.Forms.TextBox tbBf;
        private System.Windows.Forms.Label label1;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _panel1_Toolbars_Dock_Area_Bottom;
    }
}