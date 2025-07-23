namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmSchemeList
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
            this.lstScheme = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.chxKeepRelevantProperties = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstScheme
            // 
            this.lstScheme.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstScheme.FormattingEnabled = true;
            this.lstScheme.Location = new System.Drawing.Point(0, 0);
            this.lstScheme.Name = "lstScheme";
            this.lstScheme.Size = new System.Drawing.Size(292, 225);
            this.lstScheme.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(217, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chxKeepRelevantProperties
            // 
            this.chxKeepRelevantProperties.AutoSize = true;
            this.chxKeepRelevantProperties.Location = new System.Drawing.Point(12, 235);
            this.chxKeepRelevantProperties.Name = "chxKeepRelevantProperties";
            this.chxKeepRelevantProperties.Size = new System.Drawing.Size(147, 17);
            this.chxKeepRelevantProperties.TabIndex = 2;
            this.chxKeepRelevantProperties.Text = "Keep Relevant Properties";
            this.chxKeepRelevantProperties.UseVisualStyleBackColor = true;
            // 
            // frmSchemeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(292, 258);
            this.Controls.Add(this.chxKeepRelevantProperties);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstScheme);
            this.Name = "frmSchemeList";
            this.Text = "frmSchemeList";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstScheme;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chxKeepRelevantProperties;
    }
}