namespace YGJZJL.BoardBand
{
    partial class ChangeShift
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
            Infragistics.Win.ValueListItem valueListItem6 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem7 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem8 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem9 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem1 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem2 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem3 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem4 = new Infragistics.Win.ValueListItem();
            Infragistics.Win.ValueListItem valueListItem5 = new Infragistics.Win.ValueListItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cbShift = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.cbTerm = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.tbName = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            ((System.ComponentModel.ISupportInitialize)(this.cbShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTerm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbName)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.coreBind.SetDatabasecommand(this.button1, null);
            this.button1.Location = new System.Drawing.Point(43, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "确 定";
            this.button1.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button1, null);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.coreBind.SetDatabasecommand(this.button2, null);
            this.button2.Location = new System.Drawing.Point(158, 126);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "取 消";
            this.button2.UseVisualStyleBackColor = true;
            this.coreBind.SetVerification(this.button2, null);
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.label2, null);
            this.label2.Location = new System.Drawing.Point(45, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "班 次";
            this.coreBind.SetVerification(this.label2, null);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.label3, null);
            this.label3.Location = new System.Drawing.Point(45, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "班 别";
            this.coreBind.SetVerification(this.label3, null);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.label1, null);
            this.label1.Location = new System.Drawing.Point(45, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "姓 名";
            this.coreBind.SetVerification(this.label1, null);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.coreBind.SetDatabasecommand(this.dateTimePicker1, null);
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(111, 95);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dateTimePicker1.Size = new System.Drawing.Size(122, 21);
            this.dateTimePicker1.TabIndex = 10;
            this.coreBind.SetVerification(this.dateTimePicker1, null);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.coreBind.SetDatabasecommand(this.label4, null);
            this.label4.Location = new System.Drawing.Point(45, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "日 期";
            this.coreBind.SetVerification(this.label4, null);
            // 
            // cbShift
            // 
            this.coreBind.SetDatabasecommand(this.cbShift, null);
            this.cbShift.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            valueListItem6.DataValue = "0";
            valueListItem6.DisplayText = "常白班";
            valueListItem7.DataValue = "1";
            valueListItem7.DisplayText = "早班";
            valueListItem8.DataValue = "2";
            valueListItem8.DisplayText = "中班";
            valueListItem9.DataValue = "3";
            valueListItem9.DisplayText = "晚班";
            this.cbShift.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem6,
            valueListItem7,
            valueListItem8,
            valueListItem9});
            this.cbShift.Location = new System.Drawing.Point(111, 40);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(121, 21);
            this.cbShift.TabIndex = 12;
            this.coreBind.SetVerification(this.cbShift, null);
            // 
            // cbTerm
            // 
            this.coreBind.SetDatabasecommand(this.cbTerm, null);
            this.cbTerm.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            valueListItem1.DataValue = "0";
            valueListItem1.DisplayText = "常白班";
            valueListItem2.DataValue = "1";
            valueListItem2.DisplayText = "甲班";
            valueListItem3.DataValue = "2";
            valueListItem3.DisplayText = "乙班";
            valueListItem4.DataValue = "3";
            valueListItem4.DisplayText = "丙班";
            valueListItem5.DataValue = "4";
            valueListItem5.DisplayText = "丁班";
            this.cbTerm.Items.AddRange(new Infragistics.Win.ValueListItem[] {
            valueListItem1,
            valueListItem2,
            valueListItem3,
            valueListItem4,
            valueListItem5});
            this.cbTerm.Location = new System.Drawing.Point(111, 67);
            this.cbTerm.Name = "cbTerm";
            this.cbTerm.Size = new System.Drawing.Size(121, 21);
            this.cbTerm.TabIndex = 13;
            this.coreBind.SetVerification(this.cbTerm, null);
            // 
            // tbName
            // 
            this.coreBind.SetDatabasecommand(this.tbName, null);
            this.tbName.Location = new System.Drawing.Point(111, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(121, 21);
            this.tbName.TabIndex = 14;
            this.coreBind.SetVerification(this.tbName, null);
            // 
            // ChangeShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 167);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.cbTerm);
            this.Controls.Add(this.cbShift);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.coreBind.SetDatabasecommand(this, null);
            this.Name = "ChangeShift";
            this.Text = "换班";
            this.coreBind.SetVerification(this, null);
            this.Load += new System.EventHandler(this.ChangeShift_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbTerm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cbShift;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cbTerm;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor tbName;
    }
}