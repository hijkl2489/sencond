namespace Core.Sip.Client.Test
{
    partial class FrmIcCard
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labSector = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.btWrite = new System.Windows.Forms.Button();
            this.btRead = new System.Windows.Forms.Button();
            this.nudSector = new System.Windows.Forms.NumericUpDown();
            this.labBlock = new System.Windows.Forms.Label();
            this.nudBlock = new System.Windows.Forms.NumericUpDown();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudSector)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlock)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(372, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "序列号";
            // 
            // labSector
            // 
            this.labSector.AutoSize = true;
            this.labSector.Location = new System.Drawing.Point(109, 85);
            this.labSector.Name = "labSector";
            this.labSector.Size = new System.Drawing.Size(41, 12);
            this.labSector.TabIndex = 3;
            this.labSector.Text = "扇区号";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(154, 54);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(141, 21);
            this.tbID.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(465, 54);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btWrite
            // 
            this.btWrite.Location = new System.Drawing.Point(372, 88);
            this.btWrite.Name = "btWrite";
            this.btWrite.Size = new System.Drawing.Size(75, 23);
            this.btWrite.TabIndex = 7;
            this.btWrite.Text = "写卡";
            this.btWrite.UseVisualStyleBackColor = true;
            this.btWrite.Click += new System.EventHandler(this.btWrite_Click);
            // 
            // btRead
            // 
            this.btRead.Location = new System.Drawing.Point(465, 88);
            this.btRead.Name = "btRead";
            this.btRead.Size = new System.Drawing.Size(75, 23);
            this.btRead.TabIndex = 8;
            this.btRead.Text = "读卡";
            this.btRead.UseVisualStyleBackColor = true;
            this.btRead.Click += new System.EventHandler(this.btRead_Click);
            // 
            // nudSector
            // 
            this.nudSector.Location = new System.Drawing.Point(154, 81);
            this.nudSector.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudSector.Name = "nudSector";
            this.nudSector.Size = new System.Drawing.Size(48, 21);
            this.nudSector.TabIndex = 9;
            // 
            // labBlock
            // 
            this.labBlock.AutoSize = true;
            this.labBlock.Location = new System.Drawing.Point(211, 85);
            this.labBlock.Name = "labBlock";
            this.labBlock.Size = new System.Drawing.Size(29, 12);
            this.labBlock.TabIndex = 10;
            this.labBlock.Text = "块号";
            // 
            // nudBlock
            // 
            this.nudBlock.Location = new System.Drawing.Point(246, 81);
            this.nudBlock.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudBlock.Name = "nudBlock";
            this.nudBlock.Size = new System.Drawing.Size(48, 21);
            this.nudBlock.TabIndex = 11;
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(154, 111);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(140, 21);
            this.tbValue.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "块值";
            // 
            // FrmIcCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 336);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.nudBlock);
            this.Controls.Add(this.labBlock);
            this.Controls.Add(this.nudSector);
            this.Controls.Add(this.btRead);
            this.Controls.Add(this.btWrite);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.labSector);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "FrmIcCard";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSector)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labSector;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btWrite;
        private System.Windows.Forms.Button btRead;
        private System.Windows.Forms.NumericUpDown nudSector;
        private System.Windows.Forms.Label labBlock;
        private System.Windows.Forms.NumericUpDown nudBlock;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label2;
    }
}

