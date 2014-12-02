namespace YGJZJL.Car
{
    partial class MeterControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtWeight = new LxControl.LxLedControl();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).BeginInit();
            this.SuspendLayout();
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.Color.Transparent;
            this.txtWeight.BackColor_1 = System.Drawing.SystemColors.ControlText;
            this.txtWeight.BackColor_2 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtWeight.BevelRate = 0.5F;
            this.txtWeight.CornerRadius = 6;
            this.txtWeight.FadedColor = System.Drawing.SystemColors.ControlText;
            this.txtWeight.ForeColor = System.Drawing.Color.Green;
            this.txtWeight.HighlightOpaque = ((byte)(50));
            this.txtWeight.Location = new System.Drawing.Point(0, 0);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(258, 71);
            this.txtWeight.TabIndex = 679;
            this.txtWeight.Text = "0.000";
            this.txtWeight.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.txtWeight.TotalCharCount = 7;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnClear.Location = new System.Drawing.Point(282, 43);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(83, 28);
            this.btnClear.TabIndex = 684;
            this.btnClear.Text = "仪表清零";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.Transparent;
            this.txtStatus.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStatus.Location = new System.Drawing.Point(304, 9);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(76, 21);
            this.txtStatus.TabIndex = 683;
            this.txtStatus.Text = "未连接";
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(275, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(28, 28);
            this.lblStatus.TabIndex = 682;
            this.lblStatus.Text = "●";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MeterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtWeight);
            this.Name = "MeterControl";
            this.Size = new System.Drawing.Size(383, 72);
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private LxControl.LxLedControl txtWeight;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}
