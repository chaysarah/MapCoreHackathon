namespace MCTester.General_Forms
{
    partial class frmRasterorDtmLayerFiles
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
            this.lbxFileNames = new System.Windows.Forms.ListBox();
            this.lblFullPath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbxFileNames
            // 
            this.lbxFileNames.FormattingEnabled = true;
            this.lbxFileNames.Location = new System.Drawing.Point(1, 48);
            this.lbxFileNames.Name = "lbxFileNames";
            this.lbxFileNames.Size = new System.Drawing.Size(283, 212);
            this.lbxFileNames.TabIndex = 0;
            // 
            // lblFullPath
            // 
            this.lblFullPath.AutoSize = true;
            this.lblFullPath.Location = new System.Drawing.Point(12, 9);
            this.lblFullPath.Name = "lblFullPath";
            this.lblFullPath.Size = new System.Drawing.Size(32, 13);
            this.lblFullPath.TabIndex = 1;
            this.lblFullPath.Text = "Path:";
            // 
            // frmRasterorDtmLayerFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lblFullPath);
            this.Controls.Add(this.lbxFileNames);
            this.Name = "frmRasterorDtmLayerFiles";
            this.Text = "frmRasterorDtmLayerFiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxFileNames;
        private System.Windows.Forms.Label lblFullPath;
    }
}