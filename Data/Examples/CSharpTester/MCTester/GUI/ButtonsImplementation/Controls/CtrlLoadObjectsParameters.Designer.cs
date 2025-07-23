namespace MCTester.Controls
{
    partial class CtrlLoadObjectsParameters
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.chxIsShowStorageFormat = new System.Windows.Forms.CheckBox();
            this.chxIsShowVersion = new System.Windows.Forms.CheckBox();
            this.cmbStorageFormatAfterLoad = new System.Windows.Forms.ComboBox();
            this.ntxVersion2 = new MCTester.Controls.NumericTextBox();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.chxIsShowStorageFormat);
            this.groupBox9.Controls.Add(this.chxIsShowVersion);
            this.groupBox9.Controls.Add(this.cmbStorageFormatAfterLoad);
            this.groupBox9.Controls.Add(this.ntxVersion2);
            this.groupBox9.Location = new System.Drawing.Point(3, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(286, 69);
            this.groupBox9.TabIndex = 59;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Load Parameters (From Last File or Memory Buffer)";
            // 
            // chxIsShowStorageFormat
            // 
            this.chxIsShowStorageFormat.AutoSize = true;
            this.chxIsShowStorageFormat.Location = new System.Drawing.Point(6, 45);
            this.chxIsShowStorageFormat.Name = "chxIsShowStorageFormat";
            this.chxIsShowStorageFormat.Size = new System.Drawing.Size(58, 17);
            this.chxIsShowStorageFormat.TabIndex = 56;
            this.chxIsShowStorageFormat.Text = "Format";
            this.chxIsShowStorageFormat.UseVisualStyleBackColor = true;
            this.chxIsShowStorageFormat.CheckedChanged += new System.EventHandler(this.chxIsShowStorageFormat_CheckedChanged);
            // 
            // chxIsShowVersion
            // 
            this.chxIsShowVersion.AutoSize = true;
            this.chxIsShowVersion.Location = new System.Drawing.Point(6, 20);
            this.chxIsShowVersion.Name = "chxIsShowVersion";
            this.chxIsShowVersion.Size = new System.Drawing.Size(61, 17);
            this.chxIsShowVersion.TabIndex = 55;
            this.chxIsShowVersion.Text = "Version";
            this.chxIsShowVersion.UseVisualStyleBackColor = true;
            this.chxIsShowVersion.CheckedChanged += new System.EventHandler(this.chxIsShowVersion_CheckedChanged);
            // 
            // cmbStorageFormatAfterLoad
            // 
            this.cmbStorageFormatAfterLoad.Enabled = false;
            this.cmbStorageFormatAfterLoad.FormattingEnabled = true;
            this.cmbStorageFormatAfterLoad.Location = new System.Drawing.Point(79, 43);
            this.cmbStorageFormatAfterLoad.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStorageFormatAfterLoad.Name = "cmbStorageFormatAfterLoad";
            this.cmbStorageFormatAfterLoad.Size = new System.Drawing.Size(169, 21);
            this.cmbStorageFormatAfterLoad.TabIndex = 60;
            // 
            // ntxVersion2
            // 
            this.ntxVersion2.Enabled = false;
            this.ntxVersion2.Location = new System.Drawing.Point(78, 18);
            this.ntxVersion2.Name = "ntxVersion2";
            this.ntxVersion2.Size = new System.Drawing.Size(170, 20);
            this.ntxVersion2.TabIndex = 58;
            this.ntxVersion2.Visible = false;
            // 
            // CtrlLoadObjectsParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox9);
            this.Name = "CtrlLoadObjectsParameters";
            this.Size = new System.Drawing.Size(305, 75);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox chxIsShowStorageFormat;
        private System.Windows.Forms.CheckBox chxIsShowVersion;
        private System.Windows.Forms.ComboBox cmbStorageFormatAfterLoad;
        private NumericTextBox ntxVersion2;
    }
}
