namespace MCTester.Controls
{
    partial class CtrlPropertySelectFile
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
            this.ctrlRegBrowse = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlSelBrowse = new MCTester.Controls.CtrlBrowseControl();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegBrowse);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegBrowse, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.ctrlSelBrowse);
            this.tpSelection.Controls.SetChildIndex(this.ctrlSelBrowse, 0);
            // 
            // ctrlRegBrowse
            // 
            this.ctrlRegBrowse.AutoSize = true;
            this.ctrlRegBrowse.FileName = "";
            this.ctrlRegBrowse.Filter = "";
            this.ctrlRegBrowse.IsFolderDialog = false;
            this.ctrlRegBrowse.IsSaveFile = false;
            this.ctrlRegBrowse.LabelCaption = "File Name:";
            this.ctrlRegBrowse.Location = new System.Drawing.Point(80, 52);
            this.ctrlRegBrowse.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlRegBrowse.Name = "ctrlRegBrowse";
            this.ctrlRegBrowse.Size = new System.Drawing.Size(300, 24);
            this.ctrlRegBrowse.TabIndex = 25;
            // 
            // ctrlSelBrowse
            // 
            this.ctrlSelBrowse.AutoSize = true;
            this.ctrlSelBrowse.FileName = "";
            this.ctrlSelBrowse.Filter = "";
            this.ctrlSelBrowse.IsFolderDialog = false;
            this.ctrlSelBrowse.IsSaveFile = false;
            this.ctrlSelBrowse.LabelCaption = "File Name:";
            this.ctrlSelBrowse.Location = new System.Drawing.Point(80, 52);
            this.ctrlSelBrowse.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlSelBrowse.Name = "ctrlSelBrowse";
            this.ctrlSelBrowse.Size = new System.Drawing.Size(300, 24);
            this.ctrlSelBrowse.TabIndex = 27;
            // 
            // CtrlPropertySelectFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertySelectFile";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public CtrlBrowseControl ctrlRegBrowse;
        public CtrlBrowseControl ctrlSelBrowse;
    }
}
