namespace YGJZJL.CarSip.Client.Test
{
    partial class FrmDvr
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
            this.Video1 = new System.Windows.Forms.PictureBox();
            this.btTalk = new System.Windows.Forms.Button();
            this.btSendVoice = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.txtDvrRet = new System.Windows.Forms.TextBox();
            this.btDvrTimeSyn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWtComplete = new System.Windows.Forms.TextBox();
            this.txtWtChange = new System.Windows.Forms.TextBox();
            this.Video2 = new System.Windows.Forms.PictureBox();
            this.Video3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCardValue = new System.Windows.Forms.TextBox();
            this.nudBlock = new System.Windows.Forms.NumericUpDown();
            this.labBlock = new System.Windows.Forms.Label();
            this.nudSector = new System.Windows.Forms.NumericUpDown();
            this.btCardRead = new System.Windows.Forms.Button();
            this.btCardWrite = new System.Windows.Forms.Button();
            this.tbID = new System.Windows.Forms.TextBox();
            this.labSector = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grpIcCard = new System.Windows.Forms.GroupBox();
            this.grpWeight = new System.Windows.Forms.GroupBox();
            this.btLedPowerOff = new System.Windows.Forms.Button();
            this.btLedPowerOn = new System.Windows.Forms.Button();
            this.btLedWrite = new System.Windows.Forms.Button();
            this.txtLedData = new System.Windows.Forms.TextBox();
            this.tpStart = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tpStop = new System.Windows.Forms.DateTimePicker();
            this.btPlayBack = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.Video1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSector)).BeginInit();
            this.grpIcCard.SuspendLayout();
            this.grpWeight.SuspendLayout();
            this.SuspendLayout();
            // 
            // Video1
            // 
            this.Video1.Location = new System.Drawing.Point(1, 139);
            this.Video1.Name = "Video1";
            this.Video1.Size = new System.Drawing.Size(273, 185);
            this.Video1.TabIndex = 0;
            this.Video1.TabStop = false;
            // 
            // btTalk
            // 
            this.btTalk.Location = new System.Drawing.Point(4, 7);
            this.btTalk.Name = "btTalk";
            this.btTalk.Size = new System.Drawing.Size(68, 32);
            this.btTalk.TabIndex = 2;
            this.btTalk.Text = "开始语音";
            this.btTalk.UseVisualStyleBackColor = true;
            this.btTalk.Click += new System.EventHandler(this.btTalk_Click);
            // 
            // btSendVoice
            // 
            this.btSendVoice.Location = new System.Drawing.Point(78, 7);
            this.btSendVoice.Name = "btSendVoice";
            this.btSendVoice.Size = new System.Drawing.Size(57, 32);
            this.btSendVoice.TabIndex = 3;
            this.btSendVoice.Text = "传音频";
            this.btSendVoice.UseVisualStyleBackColor = true;
            this.btSendVoice.Click += new System.EventHandler(this.btSendVoice_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(143, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(53, 32);
            this.button5.TabIndex = 5;
            this.button5.Text = "截图";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txtDvrRet
            // 
            this.txtDvrRet.Location = new System.Drawing.Point(68, 56);
            this.txtDvrRet.Name = "txtDvrRet";
            this.txtDvrRet.Size = new System.Drawing.Size(195, 21);
            this.txtDvrRet.TabIndex = 6;
            // 
            // btDvrTimeSyn
            // 
            this.btDvrTimeSyn.Location = new System.Drawing.Point(204, 7);
            this.btDvrTimeSyn.Name = "btDvrTimeSyn";
            this.btDvrTimeSyn.Size = new System.Drawing.Size(68, 32);
            this.btDvrTimeSyn.TabIndex = 7;
            this.btDvrTimeSyn.Text = "时间同步";
            this.btDvrTimeSyn.UseVisualStyleBackColor = true;
            this.btDvrTimeSyn.Click += new System.EventHandler(this.btDvrTimeSyn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "返回值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "最终重量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "实时重量";
            // 
            // txtWtComplete
            // 
            this.txtWtComplete.Location = new System.Drawing.Point(71, 47);
            this.txtWtComplete.Name = "txtWtComplete";
            this.txtWtComplete.Size = new System.Drawing.Size(93, 21);
            this.txtWtComplete.TabIndex = 10;
            // 
            // txtWtChange
            // 
            this.txtWtChange.Location = new System.Drawing.Point(71, 20);
            this.txtWtChange.Name = "txtWtChange";
            this.txtWtChange.Size = new System.Drawing.Size(93, 21);
            this.txtWtChange.TabIndex = 9;
            // 
            // Video2
            // 
            this.Video2.Location = new System.Drawing.Point(1, 326);
            this.Video2.Name = "Video2";
            this.Video2.Size = new System.Drawing.Size(273, 196);
            this.Video2.TabIndex = 13;
            this.Video2.TabStop = false;
            // 
            // Video3
            // 
            this.Video3.Location = new System.Drawing.Point(1, 524);
            this.Video3.Name = "Video3";
            this.Video3.Size = new System.Drawing.Size(273, 196);
            this.Video3.TabIndex = 14;
            this.Video3.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "块值";
            // 
            // txtCardValue
            // 
            this.txtCardValue.Location = new System.Drawing.Point(66, 77);
            this.txtCardValue.Name = "txtCardValue";
            this.txtCardValue.Size = new System.Drawing.Size(140, 21);
            this.txtCardValue.TabIndex = 25;
            // 
            // nudBlock
            // 
            this.nudBlock.Location = new System.Drawing.Point(158, 47);
            this.nudBlock.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudBlock.Name = "nudBlock";
            this.nudBlock.Size = new System.Drawing.Size(48, 21);
            this.nudBlock.TabIndex = 24;
            // 
            // labBlock
            // 
            this.labBlock.AutoSize = true;
            this.labBlock.Location = new System.Drawing.Point(123, 51);
            this.labBlock.Name = "labBlock";
            this.labBlock.Size = new System.Drawing.Size(29, 12);
            this.labBlock.TabIndex = 23;
            this.labBlock.Text = "块号";
            // 
            // nudSector
            // 
            this.nudSector.Location = new System.Drawing.Point(66, 47);
            this.nudSector.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudSector.Name = "nudSector";
            this.nudSector.Size = new System.Drawing.Size(48, 21);
            this.nudSector.TabIndex = 22;
            // 
            // btCardRead
            // 
            this.btCardRead.Location = new System.Drawing.Point(132, 104);
            this.btCardRead.Name = "btCardRead";
            this.btCardRead.Size = new System.Drawing.Size(75, 23);
            this.btCardRead.TabIndex = 21;
            this.btCardRead.Text = "读卡";
            this.btCardRead.UseVisualStyleBackColor = true;
            this.btCardRead.Click += new System.EventHandler(this.btCardRead_Click);
            // 
            // btCardWrite
            // 
            this.btCardWrite.Location = new System.Drawing.Point(39, 104);
            this.btCardWrite.Name = "btCardWrite";
            this.btCardWrite.Size = new System.Drawing.Size(75, 23);
            this.btCardWrite.TabIndex = 20;
            this.btCardWrite.Text = "写卡";
            this.btCardWrite.UseVisualStyleBackColor = true;
            this.btCardWrite.Click += new System.EventHandler(this.btCardWrite_Click);
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(66, 20);
            this.tbID.Name = "tbID";
            this.tbID.ReadOnly = true;
            this.tbID.Size = new System.Drawing.Size(141, 21);
            this.tbID.TabIndex = 18;
            // 
            // labSector
            // 
            this.labSector.AutoSize = true;
            this.labSector.Location = new System.Drawing.Point(21, 51);
            this.labSector.Name = "labSector";
            this.labSector.Size = new System.Drawing.Size(41, 12);
            this.labSector.TabIndex = 17;
            this.labSector.Text = "扇区号";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "序列号";
            // 
            // grpIcCard
            // 
            this.grpIcCard.Controls.Add(this.label4);
            this.grpIcCard.Controls.Add(this.label5);
            this.grpIcCard.Controls.Add(this.txtCardValue);
            this.grpIcCard.Controls.Add(this.labSector);
            this.grpIcCard.Controls.Add(this.nudBlock);
            this.grpIcCard.Controls.Add(this.tbID);
            this.grpIcCard.Controls.Add(this.labBlock);
            this.grpIcCard.Controls.Add(this.btCardWrite);
            this.grpIcCard.Controls.Add(this.nudSector);
            this.grpIcCard.Controls.Add(this.btCardRead);
            this.grpIcCard.Location = new System.Drawing.Point(668, 127);
            this.grpIcCard.Name = "grpIcCard";
            this.grpIcCard.Size = new System.Drawing.Size(228, 141);
            this.grpIcCard.TabIndex = 27;
            this.grpIcCard.TabStop = false;
            this.grpIcCard.Text = "IC卡操作";
            // 
            // grpWeight
            // 
            this.grpWeight.Controls.Add(this.txtWtChange);
            this.grpWeight.Controls.Add(this.txtWtComplete);
            this.grpWeight.Controls.Add(this.label3);
            this.grpWeight.Controls.Add(this.label2);
            this.grpWeight.Location = new System.Drawing.Point(668, 22);
            this.grpWeight.Name = "grpWeight";
            this.grpWeight.Size = new System.Drawing.Size(228, 89);
            this.grpWeight.TabIndex = 28;
            this.grpWeight.TabStop = false;
            this.grpWeight.Text = "称重仪表";
            // 
            // btLedPowerOff
            // 
            this.btLedPowerOff.Location = new System.Drawing.Point(767, 346);
            this.btLedPowerOff.Name = "btLedPowerOff";
            this.btLedPowerOff.Size = new System.Drawing.Size(69, 23);
            this.btLedPowerOff.TabIndex = 31;
            this.btLedPowerOff.Text = "关电源";
            this.btLedPowerOff.UseVisualStyleBackColor = true;
            this.btLedPowerOff.Click += new System.EventHandler(this.btLedPowerOff_Click);
            // 
            // btLedPowerOn
            // 
            this.btLedPowerOn.Location = new System.Drawing.Point(682, 346);
            this.btLedPowerOn.Name = "btLedPowerOn";
            this.btLedPowerOn.Size = new System.Drawing.Size(61, 23);
            this.btLedPowerOn.TabIndex = 30;
            this.btLedPowerOn.Text = "开电源";
            this.btLedPowerOn.UseVisualStyleBackColor = true;
            this.btLedPowerOn.Click += new System.EventHandler(this.btLedPowerOn_Click);
            // 
            // btLedWrite
            // 
            this.btLedWrite.Location = new System.Drawing.Point(682, 317);
            this.btLedWrite.Name = "btLedWrite";
            this.btLedWrite.Size = new System.Drawing.Size(61, 23);
            this.btLedWrite.TabIndex = 29;
            this.btLedWrite.Text = "写数据";
            this.btLedWrite.UseVisualStyleBackColor = true;
            this.btLedWrite.Click += new System.EventHandler(this.btLedWrite_Click);
            // 
            // txtLedData
            // 
            this.txtLedData.Location = new System.Drawing.Point(749, 317);
            this.txtLedData.Name = "txtLedData";
            this.txtLedData.Size = new System.Drawing.Size(89, 21);
            this.txtLedData.TabIndex = 32;
            // 
            // tpStart
            // 
            this.tpStart.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.tpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tpStart.Location = new System.Drawing.Point(68, 80);
            this.tpStart.Name = "tpStart";
            this.tpStart.Size = new System.Drawing.Size(195, 21);
            this.tpStart.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "开始时间";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "结束时间";
            // 
            // tpStop
            // 
            this.tpStop.CustomFormat = "yyyy年MM月dd日 HH:mm:ss";
            this.tpStop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tpStop.Location = new System.Drawing.Point(68, 105);
            this.tpStop.Name = "tpStop";
            this.tpStop.Size = new System.Drawing.Size(195, 21);
            this.tpStop.TabIndex = 36;
            // 
            // btPlayBack
            // 
            this.btPlayBack.Location = new System.Drawing.Point(280, 7);
            this.btPlayBack.Name = "btPlayBack";
            this.btPlayBack.Size = new System.Drawing.Size(50, 32);
            this.btPlayBack.TabIndex = 37;
            this.btPlayBack.Text = "回放";
            this.btPlayBack.UseVisualStyleBackColor = true;
            this.btPlayBack.Click += new System.EventHandler(this.btPlayBack_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(369, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 88);
            this.listBox1.TabIndex = 38;
            // 
            // FrmDvr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 741);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.btPlayBack);
            this.Controls.Add(this.tpStop);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tpStart);
            this.Controls.Add(this.txtLedData);
            this.Controls.Add(this.btLedPowerOff);
            this.Controls.Add(this.btLedPowerOn);
            this.Controls.Add(this.btLedWrite);
            this.Controls.Add(this.grpWeight);
            this.Controls.Add(this.grpIcCard);
            this.Controls.Add(this.Video3);
            this.Controls.Add(this.Video2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btDvrTimeSyn);
            this.Controls.Add(this.txtDvrRet);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btSendVoice);
            this.Controls.Add(this.btTalk);
            this.Controls.Add(this.Video1);
            this.Name = "FrmDvr";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDvr_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Video1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Video3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSector)).EndInit();
            this.grpIcCard.ResumeLayout(false);
            this.grpIcCard.PerformLayout();
            this.grpWeight.ResumeLayout(false);
            this.grpWeight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Video1;
        private System.Windows.Forms.Button btTalk;
        private System.Windows.Forms.Button btSendVoice;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtDvrRet;
        private System.Windows.Forms.Button btDvrTimeSyn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWtComplete;
        private System.Windows.Forms.TextBox txtWtChange;
        private System.Windows.Forms.PictureBox Video2;
        private System.Windows.Forms.PictureBox Video3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCardValue;
        private System.Windows.Forms.NumericUpDown nudBlock;
        private System.Windows.Forms.Label labBlock;
        private System.Windows.Forms.NumericUpDown nudSector;
        private System.Windows.Forms.Button btCardRead;
        private System.Windows.Forms.Button btCardWrite;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label labSector;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpIcCard;
        private System.Windows.Forms.GroupBox grpWeight;
        private System.Windows.Forms.Button btLedPowerOff;
        private System.Windows.Forms.Button btLedPowerOn;
        private System.Windows.Forms.Button btLedWrite;
        private System.Windows.Forms.TextBox txtLedData;
        private System.Windows.Forms.DateTimePicker tpStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker tpStop;
        private System.Windows.Forms.Button btPlayBack;
        private System.Windows.Forms.ListBox listBox1;
    }
}

