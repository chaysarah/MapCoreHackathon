namespace MCTester.General_Forms
{
    partial class frmChangedCameraModel
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
            this.ctrlBrowseTextureFilesDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.chxStart = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctrlBrowseTextureFilesDirectory
            // 
            this.ctrlBrowseTextureFilesDirectory.AutoSize = true;
            this.ctrlBrowseTextureFilesDirectory.FileName = "";
            this.ctrlBrowseTextureFilesDirectory.Filter = "";
            this.ctrlBrowseTextureFilesDirectory.IsFolderDialog = true;
            this.ctrlBrowseTextureFilesDirectory.IsFullPath = true;
            this.ctrlBrowseTextureFilesDirectory.IsSaveFile = false;
            this.ctrlBrowseTextureFilesDirectory.LabelCaption = "Files Directory:";
            this.ctrlBrowseTextureFilesDirectory.Location = new System.Drawing.Point(94, 2);
            this.ctrlBrowseTextureFilesDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseTextureFilesDirectory.MultiFilesSelect = false;
            this.ctrlBrowseTextureFilesDirectory.Name = "ctrlBrowseTextureFilesDirectory";
            this.ctrlBrowseTextureFilesDirectory.Prefix = "";
            this.ctrlBrowseTextureFilesDirectory.Size = new System.Drawing.Size(358, 24);
            this.ctrlBrowseTextureFilesDirectory.TabIndex = 18;
            // 
            // chxStart
            // 
            this.chxStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chxStart.AutoSize = true;
            this.chxStart.Location = new System.Drawing.Point(462, 3);
            this.chxStart.Name = "chxStart";
            this.chxStart.Size = new System.Drawing.Size(53, 23);
            this.chxStart.TabIndex = 104;
            this.chxStart.Text = "START";
            this.chxStart.UseVisualStyleBackColor = true;
            this.chxStart.CheckedChanged += new System.EventHandler(this.chxStart_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "Files Directory:";
            // 
            // frmChangedCameraModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(518, 28);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chxStart);
            this.Controls.Add(this.ctrlBrowseTextureFilesDirectory);
            this.Name = "frmChangedCameraModel";
            this.Text = "Changed Camera Model";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmChangedCameraModel_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.CtrlBrowseControl ctrlBrowseTextureFilesDirectory;
        private System.Windows.Forms.CheckBox chxStart;
        private System.Windows.Forms.Label label1;
    }
}