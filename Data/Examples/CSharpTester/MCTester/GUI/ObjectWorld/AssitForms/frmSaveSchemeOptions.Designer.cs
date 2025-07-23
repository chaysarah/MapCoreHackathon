namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmSaveSchemeOptions
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
            this.btnOK = new System.Windows.Forms.Button();
            this.chxSavePropertiesNames = new System.Windows.Forms.CheckBox();
            this.chxSaveToCSV = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(92, 59);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chxSavePropertiesNames
            // 
            this.chxSavePropertiesNames.AutoSize = true;
            this.chxSavePropertiesNames.Location = new System.Drawing.Point(12, 12);
            this.chxSavePropertiesNames.Name = "chxSavePropertiesNames";
            this.chxSavePropertiesNames.Size = new System.Drawing.Size(137, 17);
            this.chxSavePropertiesNames.TabIndex = 1;
            this.chxSavePropertiesNames.Text = "Save Properties Names";
            this.chxSavePropertiesNames.UseVisualStyleBackColor = true;
            // 
            // chxSaveToCSV
            // 
            this.chxSaveToCSV.AutoSize = true;
            this.chxSaveToCSV.Location = new System.Drawing.Point(12, 35);
            this.chxSaveToCSV.Name = "chxSaveToCSV";
            this.chxSaveToCSV.Size = new System.Drawing.Size(141, 17);
            this.chxSaveToCSV.TabIndex = 2;
            this.chxSaveToCSV.Text = "Save Properties To CSV";
            this.chxSaveToCSV.UseVisualStyleBackColor = true;
            // 
            // frmSaveSchemeOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 87);
            this.Controls.Add(this.chxSaveToCSV);
            this.Controls.Add(this.chxSavePropertiesNames);
            this.Controls.Add(this.btnOK);
            this.Name = "frmSaveSchemeOptions";
            this.Text = "frmSaveSchemeOptions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chxSavePropertiesNames;
        private System.Windows.Forms.CheckBox chxSaveToCSV;
    }
}