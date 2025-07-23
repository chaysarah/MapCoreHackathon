namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmMapHeightLines
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
            this.lstMapsHeightLines = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstMapsHeightLines
            // 
            this.lstMapsHeightLines.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstMapsHeightLines.FormattingEnabled = true;
            this.lstMapsHeightLines.Location = new System.Drawing.Point(0, 0);
            this.lstMapsHeightLines.Name = "lstMapsHeightLines";
            this.lstMapsHeightLines.Size = new System.Drawing.Size(257, 264);
            this.lstMapsHeightLines.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(182, 270);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmMapHeightLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(257, 299);
            this.Controls.Add(this.lstMapsHeightLines);
            this.Controls.Add(this.btnOK);
            this.Name = "frmMapHeightLines";
            this.Text = "frmMapHeightLines";
            this.Load += new System.EventHandler(this.frmMapHeightLines_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstMapsHeightLines;
        private System.Windows.Forms.Button btnOK;
    }
}