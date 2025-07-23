namespace MCTester.MapWorld.MapUserControls
{
    partial class frmGridRegionLine
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
            this.lstGridRegionLine = new System.Windows.Forms.ListBox();
            this.btnGridRegionLineOK = new System.Windows.Forms.Button();
            this.chxSetAllGridLines = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstGridRegionLine
            // 
            this.lstGridRegionLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstGridRegionLine.FormattingEnabled = true;
            this.lstGridRegionLine.Location = new System.Drawing.Point(0, 0);
            this.lstGridRegionLine.Name = "lstGridRegionLine";
            this.lstGridRegionLine.Size = new System.Drawing.Size(292, 238);
            this.lstGridRegionLine.TabIndex = 1;
            // 
            // btnGridRegionLineOK
            // 
            this.btnGridRegionLineOK.Location = new System.Drawing.Point(217, 241);
            this.btnGridRegionLineOK.Name = "btnGridRegionLineOK";
            this.btnGridRegionLineOK.Size = new System.Drawing.Size(75, 23);
            this.btnGridRegionLineOK.TabIndex = 2;
            this.btnGridRegionLineOK.Text = "OK";
            this.btnGridRegionLineOK.UseVisualStyleBackColor = true;
            this.btnGridRegionLineOK.Click += new System.EventHandler(this.btnGridRegionLineOK_Click);
            // 
            // chxSetAllGridLines
            // 
            this.chxSetAllGridLines.AutoSize = true;
            this.chxSetAllGridLines.Location = new System.Drawing.Point(3, 244);
            this.chxSetAllGridLines.Name = "chxSetAllGridLines";
            this.chxSetAllGridLines.Size = new System.Drawing.Size(120, 17);
            this.chxSetAllGridLines.TabIndex = 3;
            this.chxSetAllGridLines.Text = "Set All Grid Regions";
            this.chxSetAllGridLines.UseVisualStyleBackColor = true;
            // 
            // frmGridRegionLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.chxSetAllGridLines);
            this.Controls.Add(this.btnGridRegionLineOK);
            this.Controls.Add(this.lstGridRegionLine);
            this.Name = "frmGridRegionLine";
            this.Text = "frmGridRegionLine";
            this.Load += new System.EventHandler(this.frmGridRegionLine_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstGridRegionLine;
        private System.Windows.Forms.Button btnGridRegionLineOK;
        private System.Windows.Forms.CheckBox chxSetAllGridLines;
    }
}