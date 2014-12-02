namespace YGJZJL.CarSip.Client.Test
{
    partial class FrmWeight
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
            this.tbWtChange = new System.Windows.Forms.TextBox();
            this.tbWtComplete = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbWtChange
            // 
            this.tbWtChange.Location = new System.Drawing.Point(207, 49);
            this.tbWtChange.Name = "tbWtChange";
            this.tbWtChange.Size = new System.Drawing.Size(93, 21);
            this.tbWtChange.TabIndex = 0;
            // 
            // tbWtComplete
            // 
            this.tbWtComplete.Location = new System.Drawing.Point(207, 106);
            this.tbWtComplete.Name = "tbWtComplete";
            this.tbWtComplete.Size = new System.Drawing.Size(93, 21);
            this.tbWtComplete.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "实时重量";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(148, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "最终重量";
            // 
            // FrmWeight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 389);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbWtComplete);
            this.Controls.Add(this.tbWtChange);
            this.Name = "FrmWeight";
            this.Text = "FrmWeight";
            this.Load += new System.EventHandler(this.FrmWeight_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmWeight_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbWtChange;
        private System.Windows.Forms.TextBox tbWtComplete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}