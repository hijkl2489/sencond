namespace YGJZJL.Car
{
    partial class QuickPlanInfo
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
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("运输计划表", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_PLANCODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_MATERIALNAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDERSTORE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_SENDER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVERFACTORY");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_RECEIVERSTORE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTFLOW");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_TRANSNO");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FS_POINTID");
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ultraGrid3 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.dataColumn6 = new System.Data.DataColumn();
            this.dataColumn7 = new System.Data.DataColumn();
            this.dataColumn8 = new System.Data.DataColumn();
            this.dataColumn9 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnQX = new System.Windows.Forms.Button();
            this.btnQD = new System.Windows.Forms.Button();
            this.dataColumn3 = new System.Data.DataColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ultraGrid3);
            this.panel2.Controls.Add(this.panel3);
            this.coreBind.SetDatabasecommand(this.panel2, null);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 466);
            this.panel2.TabIndex = 1;
            this.coreBind.SetVerification(this.panel2, null);
            // 
            // ultraGrid3
            // 
            this.coreBind.SetDatabasecommand(this.ultraGrid3, null);
            this.ultraGrid3.DataMember = "运输计划表";
            this.ultraGrid3.DataSource = this.dataSet1;
            appearance19.BackColor = System.Drawing.Color.White;
            appearance19.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal;
            this.ultraGrid3.DisplayLayout.Appearance = appearance19;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.RowLayoutColumnInfo.OriginX = 0;
            ultraGridColumn1.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn1.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn1.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn1.Width = 70;
            ultraGridColumn2.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.RowLayoutColumnInfo.OriginX = 4;
            ultraGridColumn2.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn2.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn2.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn3.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.RowLayoutColumnInfo.OriginX = 6;
            ultraGridColumn3.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn3.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn3.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn4.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.RowLayoutColumnInfo.OriginX = 8;
            ultraGridColumn4.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn4.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn4.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn5.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.RowLayoutColumnInfo.OriginX = 10;
            ultraGridColumn5.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn5.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn5.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn6.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.RowLayoutColumnInfo.OriginX = 12;
            ultraGridColumn6.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn6.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn6.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn7.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.RowLayoutColumnInfo.OriginX = 14;
            ultraGridColumn7.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn7.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn7.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn8.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.RowLayoutColumnInfo.OriginX = 16;
            ultraGridColumn8.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn8.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn8.RowLayoutColumnInfo.SpanY = 2;
            ultraGridColumn9.CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.RowLayoutColumnInfo.OriginX = 2;
            ultraGridColumn9.RowLayoutColumnInfo.OriginY = 0;
            ultraGridColumn9.RowLayoutColumnInfo.PreferredCellSize = new System.Drawing.Size(70, 0);
            ultraGridColumn9.RowLayoutColumnInfo.SpanX = 2;
            ultraGridColumn9.RowLayoutColumnInfo.SpanY = 2;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9});
            ultraGridBand1.RowLayoutStyle = Infragistics.Win.UltraWinGrid.RowLayoutStyle.GroupLayout;
            this.ultraGrid3.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.ultraGrid3.DisplayLayout.InterBandSpacing = 10;
            appearance20.BackColor = System.Drawing.Color.Transparent;
            this.ultraGrid3.DisplayLayout.Override.CardAreaAppearance = appearance20;
            appearance21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            appearance21.BackColor2 = System.Drawing.Color.White;
            appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.None;
            appearance21.FontData.SizeInPoints = 11F;
            appearance21.FontData.UnderlineAsString = "False";
            appearance21.ForeColor = System.Drawing.Color.Black;
            appearance21.TextHAlignAsString = "Center";
            appearance21.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent;
            this.ultraGrid3.DisplayLayout.Override.HeaderAppearance = appearance21;
            appearance22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid3.DisplayLayout.Override.RowAppearance = appearance22;
            appearance23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(149)))), ((int)(((byte)(255)))));
            appearance23.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorAppearance = appearance23;
            this.ultraGrid3.DisplayLayout.Override.RowSelectorWidth = 12;
            this.ultraGrid3.DisplayLayout.Override.RowSpacingBefore = 2;
            appearance24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            appearance24.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(149)))), ((int)(((byte)(21)))));
            appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance24.ForeColor = System.Drawing.Color.Black;
            this.ultraGrid3.DisplayLayout.Override.SelectedRowAppearance = appearance24;
            this.ultraGrid3.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.True;
            this.ultraGrid3.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(68)))), ((int)(((byte)(208)))));
            this.ultraGrid3.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid;
            this.ultraGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGrid3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ultraGrid3.Location = new System.Drawing.Point(0, 0);
            this.ultraGrid3.Name = "ultraGrid3";
            this.ultraGrid3.Size = new System.Drawing.Size(592, 376);
            this.ultraGrid3.TabIndex = 4;
            this.coreBind.SetVerification(this.ultraGrid3, null);
            this.ultraGrid3.Click += new System.EventHandler(this.ultraGrid3_Click);
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
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8,
            this.dataColumn9,
            this.dataColumn2,
            this.dataColumn3});
            this.dataTable1.TableName = "运输计划表";
            // 
            // dataColumn1
            // 
            this.dataColumn1.Caption = "预报编号";
            this.dataColumn1.ColumnName = "FS_PLANCODE";
            // 
            // dataColumn4
            // 
            this.dataColumn4.Caption = "物料名称";
            this.dataColumn4.ColumnName = "FS_MATERIALNAME";
            // 
            // dataColumn5
            // 
            this.dataColumn5.Caption = "发货地点";
            this.dataColumn5.ColumnName = "FS_SENDERSTORE";
            // 
            // dataColumn6
            // 
            this.dataColumn6.Caption = "发货单位";
            this.dataColumn6.ColumnName = "FS_SENDER";
            // 
            // dataColumn7
            // 
            this.dataColumn7.Caption = "收货单位";
            this.dataColumn7.ColumnName = "FS_RECEIVERFACTORY";
            // 
            // dataColumn8
            // 
            this.dataColumn8.Caption = "卸货地点";
            this.dataColumn8.ColumnName = "FS_RECEIVERSTORE";
            // 
            // dataColumn9
            // 
            this.dataColumn9.Caption = "流向";
            this.dataColumn9.ColumnName = "FS_POINTFLOW";
            // 
            // dataColumn2
            // 
            this.dataColumn2.Caption = "承运单位";
            this.dataColumn2.ColumnName = "FS_TRANSNO";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(208)))), ((int)(((byte)(250)))));
            this.panel3.Controls.Add(this.btnQX);
            this.panel3.Controls.Add(this.btnQD);
            this.coreBind.SetDatabasecommand(this.panel3, null);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 376);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(592, 90);
            this.panel3.TabIndex = 0;
            this.coreBind.SetVerification(this.panel3, null);
            // 
            // btnQX
            // 
            this.btnQX.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnQX, null);
            this.btnQX.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQX.Location = new System.Drawing.Point(355, 29);
            this.btnQX.Name = "btnQX";
            this.btnQX.Size = new System.Drawing.Size(90, 33);
            this.btnQX.TabIndex = 6;
            this.btnQX.Text = "取 消";
            this.btnQX.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnQX, null);
            this.btnQX.Click += new System.EventHandler(this.btnQX_Click);
            // 
            // btnQD
            // 
            this.btnQD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.coreBind.SetDatabasecommand(this.btnQD, null);
            this.btnQD.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQD.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQD.Location = new System.Drawing.Point(158, 29);
            this.btnQD.Name = "btnQD";
            this.btnQD.Size = new System.Drawing.Size(90, 33);
            this.btnQD.TabIndex = 5;
            this.btnQD.Text = "确  定";
            this.btnQD.UseVisualStyleBackColor = false;
            this.coreBind.SetVerification(this.btnQD, null);
            this.btnQD.Click += new System.EventHandler(this.btnQD_Click);
            // 
            // dataColumn3
            // 
            this.dataColumn3.Caption = "预报编号";
            this.dataColumn3.ColumnName = "FS_POINTID";
            // 
            // QuickPlanInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.panel2);
            this.coreBind.SetDatabasecommand(this, null);
            this.Name = "QuickPlanInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "长期预报";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.GuideCardNoInfo_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGrid3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGrid3;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.Button btnQD;
        private System.Windows.Forms.Button btnQX;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataColumn dataColumn8;
        private System.Data.DataColumn dataColumn9;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
    }
}