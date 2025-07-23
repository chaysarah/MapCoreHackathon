namespace MCTester.MapWorld.MapUserControls
{
    partial class frmGridRegionText
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
            this.btnGridRegionTextOK = new System.Windows.Forms.Button();
            this.lstGridRegionText = new System.Windows.Forms.ListBox();
            this.chxSetAllGridLines = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnGridRegionTextOK
            // 
            this.btnGridRegionTextOK.Location = new System.Drawing.Point(217, 242);
            this.btnGridRegionTextOK.Name = "btnGridRegionTextOK";
            this.btnGridRegionTextOK.Size = new System.Drawing.Size(75, 23);
            this.btnGridRegionTextOK.TabIndex = 4;
            this.btnGridRegionTextOK.Text = "OK";
            this.btnGridRegionTextOK.UseVisualStyleBackColor = true;
            this.btnGridRegionTextOK.Click += new System.EventHandler(this.btnGridRegionTextOK_Click);
            // 
            // lstGridRegionText
            // 
            this.lstGridRegionText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstGridRegionText.FormattingEnabled = true;
            this.lstGridRegionText.Location = new System.Drawing.Point(0, 0);
            this.lstGridRegionText.Name = "lstGridRegionText";
            this.lstGridRegionText.Size = new System.Drawing.Size(292, 238);
            this.lstGridRegionText.TabIndex = 3;
            // 
            // chxSetAllGridLines
            // 
            this.chxSetAllGridLines.AutoSize = true;
            this.chxSetAllGridLines.Location = new System.Drawing.Point(2, 244);
            this.chxSetAllGridLines.Name = "chxSetAllGridLines";
            this.chxSetAllGridLines.Size = new System.Drawing.Size(120, 17);
            this.chxSetAllGridLines.TabIndex = 5;
            this.chxSetAllGridLines.Text = "Set All Grid Regions";
            this.chxSetAllGridLines.UseVisualStyleBackColor = true;
            // 
            // frmGridRegionText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.chxSetAllGridLines);
            this.Controls.Add(this.btnGridRegionTextOK);
            this.Controls.Add(this.lstGridRegionText);
            this.Name = "frmGridRegionText";
            this.Text = "frmGridRegionText";
            this.Load += new System.EventHandler(this.frmGridRegionText_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGridRegionTextOK;
        private System.Windows.Forms.ListBox lstGridRegionText;
        private System.Windows.Forms.CheckBox chxSetAllGridLines;
    }
}