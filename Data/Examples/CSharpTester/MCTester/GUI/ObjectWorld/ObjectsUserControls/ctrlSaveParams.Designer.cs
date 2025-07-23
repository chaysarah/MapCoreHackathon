namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ctrlSaveParams
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
            this.label11 = new System.Windows.Forms.Label();
            this.cbSavingVersionCompatibility = new System.Windows.Forms.ComboBox();
            this.chxSaveLoadPropertiesCSV = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(163, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Saving Version Compatibility:";
            // 
            // cbSavingVersionCompatibility
            // 
            this.cbSavingVersionCompatibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSavingVersionCompatibility.FormattingEnabled = true;
            this.cbSavingVersionCompatibility.Location = new System.Drawing.Point(311, 3);
            this.cbSavingVersionCompatibility.Name = "cbSavingVersionCompatibility";
            this.cbSavingVersionCompatibility.Size = new System.Drawing.Size(186, 21);
            this.cbSavingVersionCompatibility.TabIndex = 33;
            this.cbSavingVersionCompatibility.SelectedIndexChanged += new System.EventHandler(this.cbSavingVersionCompatibility_SelectedIndexChanged);
            // 
            // chxSaveLoadPropertiesCSV
            // 
            this.chxSaveLoadPropertiesCSV.AutoSize = true;
            this.chxSaveLoadPropertiesCSV.Location = new System.Drawing.Point(3, 6);
            this.chxSaveLoadPropertiesCSV.Name = "chxSaveLoadPropertiesCSV";
            this.chxSaveLoadPropertiesCSV.Size = new System.Drawing.Size(154, 17);
            this.chxSaveLoadPropertiesCSV.TabIndex = 32;
            this.chxSaveLoadPropertiesCSV.Text = "Save\\Load Properties CSV";
            this.chxSaveLoadPropertiesCSV.UseVisualStyleBackColor = true;
            this.chxSaveLoadPropertiesCSV.CheckedChanged += new System.EventHandler(this.chxSaveLoadPropertiesCSV_CheckedChanged);
            // 
            // ctrlSaveParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbSavingVersionCompatibility);
            this.Controls.Add(this.chxSaveLoadPropertiesCSV);
            this.Name = "ctrlSaveParams";
            this.Size = new System.Drawing.Size(500, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbSavingVersionCompatibility;
        private System.Windows.Forms.CheckBox chxSaveLoadPropertiesCSV;
    }
}
