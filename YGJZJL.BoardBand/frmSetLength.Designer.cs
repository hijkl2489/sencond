namespace YGJZJL.BoardBand
{
    partial class frmSetSpec
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinEditors.EditorButton editorButton1 = new Infragistics.Win.UltraWinEditors.EditorButton("LAST");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinEditors.EditorButton editorButton2 = new Infragistics.Win.UltraWinEditors.EditorButton("LAST");
            Infragistics.Win.UltraWinEditors.EditorButton editorButton3 = new Infragistics.Win.UltraWinEditors.EditorButton("INIT");
            Infragistics.Win.UltraWinEditors.EditorButton editorButton4 = new Infragistics.Win.UltraWinEditors.EditorButton("LAST");
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cbx_BatchNo = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Edt_Length = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label1 = new System.Windows.Forms.Label();
            this.Edt_Spec = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label3 = new System.Windows.Forms.Label();
            this.Edt_BatchNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.Edt_OrderNo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Spec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_BatchNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_OrderNo)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.Parallel3D;
            this.ultraGroupBox1.Controls.Add(this.cbx_BatchNo);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.Edt_Length);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.Edt_Spec);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.Edt_BatchNo);
            this.ultraGroupBox1.Controls.Add(this.Edt_OrderNo);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(438, 85);
            this.ultraGroupBox1.TabIndex = 0;
            // 
            // cbx_BatchNo
            // 
            this.cbx_BatchNo.AutoSize = true;
            this.cbx_BatchNo.Location = new System.Drawing.Point(13, 21);
            this.cbx_BatchNo.Name = "cbx_BatchNo";
            this.cbx_BatchNo.Size = new System.Drawing.Size(97, 16);
            this.cbx_BatchNo.TabIndex = 0;
            this.cbx_BatchNo.Text = "指定轧制编号";
            this.cbx_BatchNo.UseVisualStyleBackColor = true;
            this.cbx_BatchNo.CheckedChanged += new System.EventHandler(this.cbx_BatchNo_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(41, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "生产订单号";
            this.label2.Visible = false;
            // 
            // Edt_Length
            // 
            appearance1.TextVAlignAsString = "Middle";
            this.Edt_Length.Appearance = appearance1;
            editorButton1.Key = "LAST";
            this.Edt_Length.ButtonsRight.Add(editorButton1);
            this.Edt_Length.Location = new System.Drawing.Point(332, 49);
            this.Edt_Length.MaxLength = 8;
            this.Edt_Length.Name = "Edt_Length";
            this.Edt_Length.Size = new System.Drawing.Size(80, 21);
            this.Edt_Length.TabIndex = 4;
            this.Edt_Length.Text = "11";
            this.Edt_Length.Visible = false;
            this.Edt_Length.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(this.Edt_Length_EditorButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(276, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "定尺长度";
            this.label1.Visible = false;
            // 
            // Edt_Spec
            // 
            appearance2.TextVAlignAsString = "Middle";
            this.Edt_Spec.Appearance = appearance2;
            editorButton2.Key = "LAST";
            this.Edt_Spec.ButtonsRight.Add(editorButton2);
            this.Edt_Spec.Location = new System.Drawing.Point(332, 19);
            this.Edt_Spec.MaxLength = 10;
            this.Edt_Spec.Name = "Edt_Spec";
            this.Edt_Spec.Size = new System.Drawing.Size(80, 21);
            this.Edt_Spec.TabIndex = 3;
            this.Edt_Spec.Text = "11";
            this.Edt_Spec.Visible = false;
            this.Edt_Spec.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(this.Edt_Spec_EditorButtonClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(276, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "轧制规格";
            this.label3.Visible = false;
            // 
            // Edt_BatchNo
            // 
            editorButton3.Key = "INIT";
            this.Edt_BatchNo.ButtonsRight.Add(editorButton3);
            this.Edt_BatchNo.Enabled = false;
            this.Edt_BatchNo.Location = new System.Drawing.Point(112, 19);
            this.Edt_BatchNo.MaxLength = 8;
            this.Edt_BatchNo.Name = "Edt_BatchNo";
            this.Edt_BatchNo.Size = new System.Drawing.Size(136, 21);
            this.Edt_BatchNo.TabIndex = 1;
            this.Edt_BatchNo.Text = "自动";
            this.Edt_BatchNo.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(this.Edt_BatchNo_EditorButtonClick);
            // 
            // Edt_OrderNo
            // 
            editorButton4.Key = "LAST";
            this.Edt_OrderNo.ButtonsRight.Add(editorButton4);
            this.Edt_OrderNo.Location = new System.Drawing.Point(112, 49);
            this.Edt_OrderNo.MaxLength = 12;
            this.Edt_OrderNo.Name = "Edt_OrderNo";
            this.Edt_OrderNo.Size = new System.Drawing.Size(136, 21);
            this.Edt_OrderNo.TabIndex = 2;
            this.Edt_OrderNo.Text = "11";
            this.Edt_OrderNo.Visible = false;
            this.Edt_OrderNo.EditorButtonClick += new Infragistics.Win.UltraWinEditors.EditorButtonEventHandler(this.Edt_OrderNo_EditorButtonClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(337, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(251, 100);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确认(&O)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmSetSpec
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(438, 135);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ultraGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetSpec";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "轧制信息";
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_Spec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_BatchNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Edt_OrderNo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Edt_Spec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Edt_Length;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbx_BatchNo;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Edt_BatchNo;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor Edt_OrderNo;
    }
}