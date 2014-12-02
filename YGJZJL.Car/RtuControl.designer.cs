namespace YGJZJL.Car
{
    partial class RtuControl
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRing = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.coreInfraredRay2 = new YGJZJL.Car.CoreInfraredRay();
            this.coreInfraredRay1 = new YGJZJL.Car.CoreInfraredRay();
            this.coreIndicator2 = new YGJZJL.Car.CoreIndicator(this.components);
            this.coreIndicator1 = new YGJZJL.Car.CoreIndicator(this.components);
            this.btnHDHW = new System.Windows.Forms.Button();
            this.btnQDHW = new System.Windows.Forms.Button();
            this.btnZMDKG = new System.Windows.Forms.Button();
            this.btnHL = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btnRestart);
            this.groupBox1.Controls.Add(this.btnRing);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.coreInfraredRay2);
            this.groupBox1.Controls.Add(this.coreInfraredRay1);
            this.groupBox1.Controls.Add(this.coreIndicator2);
            this.groupBox1.Controls.Add(this.coreIndicator1);
            this.groupBox1.Controls.Add(this.btnHDHW);
            this.groupBox1.Controls.Add(this.btnQDHW);
            this.groupBox1.Controls.Add(this.btnZMDKG);
            this.groupBox1.Controls.Add(this.btnHL);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 96);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制";
            // 
            // btnRing
            // 
            this.btnRing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRing.Location = new System.Drawing.Point(335, 20);
            this.btnRing.Name = "btnRing";
            this.btnRing.Size = new System.Drawing.Size(43, 28);
            this.btnRing.TabIndex = 17;
            this.btnRing.Text = "电铃";
            this.btnRing.UseVisualStyleBackColor = false;
            this.btnRing.MouseLeave += new System.EventHandler(this.btnRing_MouseLeave);
            this.btnRing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRing_MouseDown);
            this.btnRing.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRing_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Image = global::YGJZJL.Car.Resource1.ring;
            this.pictureBox1.Location = new System.Drawing.Point(292, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // coreInfraredRay2
            // 
            this.coreInfraredRay2.Connected = true;
            this.coreInfraredRay2.LineWidth = 4;
            this.coreInfraredRay2.Location = new System.Drawing.Point(138, 53);
            this.coreInfraredRay2.Name = "coreInfraredRay2";
            this.coreInfraredRay2.Size = new System.Drawing.Size(32, 32);
            this.coreInfraredRay2.TabIndex = 15;
            this.coreInfraredRay2.Text = "coreInfraredRay2";
            // 
            // coreInfraredRay1
            // 
            this.coreInfraredRay1.Connected = true;
            this.coreInfraredRay1.LineWidth = 4;
            this.coreInfraredRay1.Location = new System.Drawing.Point(138, 17);
            this.coreInfraredRay1.Name = "coreInfraredRay1";
            this.coreInfraredRay1.Size = new System.Drawing.Size(32, 32);
            this.coreInfraredRay1.TabIndex = 14;
            this.coreInfraredRay1.Text = "coreInfraredRay1";
            // 
            // coreIndicator2
            // 
            this.coreIndicator2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.coreIndicator2.Gradient = true;
            this.coreIndicator2.Location = new System.Drawing.Point(13, 51);
            this.coreIndicator2.Name = "coreIndicator2";
            this.coreIndicator2.Size = new System.Drawing.Size(32, 32);
            this.coreIndicator2.TabIndex = 13;
            this.coreIndicator2.Text = "coreIndicator2";
            // 
            // coreIndicator1
            // 
            this.coreIndicator1.ForeColor = System.Drawing.Color.Green;
            this.coreIndicator1.Gradient = true;
            this.coreIndicator1.Location = new System.Drawing.Point(13, 16);
            this.coreIndicator1.Name = "coreIndicator1";
            this.coreIndicator1.Size = new System.Drawing.Size(32, 32);
            this.coreIndicator1.TabIndex = 12;
            this.coreIndicator1.Text = "coreIndicator1";
            // 
            // btnHDHW
            // 
            this.btnHDHW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnHDHW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHDHW.Location = new System.Drawing.Point(183, 54);
            this.btnHDHW.Name = "btnHDHW";
            this.btnHDHW.Size = new System.Drawing.Size(72, 32);
            this.btnHDHW.TabIndex = 11;
            this.btnHDHW.Text = "后端红外";
            this.btnHDHW.UseVisualStyleBackColor = false;
            // 
            // btnQDHW
            // 
            this.btnQDHW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnQDHW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQDHW.Location = new System.Drawing.Point(183, 17);
            this.btnQDHW.Name = "btnQDHW";
            this.btnQDHW.Size = new System.Drawing.Size(72, 32);
            this.btnQDHW.TabIndex = 10;
            this.btnQDHW.Text = "前端红外";
            this.btnQDHW.UseVisualStyleBackColor = false;
            // 
            // btnZMDKG
            // 
            this.btnZMDKG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnZMDKG.Location = new System.Drawing.Point(57, 55);
            this.btnZMDKG.Name = "btnZMDKG";
            this.btnZMDKG.Size = new System.Drawing.Size(43, 28);
            this.btnZMDKG.TabIndex = 7;
            this.btnZMDKG.Text = "开/关";
            this.btnZMDKG.UseVisualStyleBackColor = false;
            this.btnZMDKG.Click += new System.EventHandler(this.btnZMDKG_Click);
            // 
            // btnHL
            // 
            this.btnHL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnHL.Location = new System.Drawing.Point(56, 18);
            this.btnHL.Name = "btnHL";
            this.btnHL.Size = new System.Drawing.Size(43, 28);
            this.btnHL.TabIndex = 6;
            this.btnHL.Text = "红/绿";
            this.btnHL.UseVisualStyleBackColor = false;
            this.btnHL.Click += new System.EventHandler(this.btnHL_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRestart.Location = new System.Drawing.Point(292, 55);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(86, 28);
            this.btnRestart.TabIndex = 18;
            this.btnRestart.Text = "设备重启";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // RtuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Name = "RtuControl";
            this.Size = new System.Drawing.Size(389, 96);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnHDHW;
        private System.Windows.Forms.Button btnQDHW;
        private System.Windows.Forms.Button btnZMDKG;
        private System.Windows.Forms.Button btnHL;
        private CoreIndicator coreIndicator1;
        private CoreIndicator coreIndicator2;
        private CoreInfraredRay coreInfraredRay1;
        private CoreInfraredRay coreInfraredRay2;
        private System.Windows.Forms.Button btnRing;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRestart;
    }
}
