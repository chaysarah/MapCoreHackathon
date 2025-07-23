namespace MCTester.Controls
{
    partial class LocalCacheParams
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
            this.gbLocalCacheParams = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbLocalCacheSubFolder = new System.Windows.Forms.TextBox();
            this.gbLocalCacheParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLocalCacheParams
            // 
            this.gbLocalCacheParams.Controls.Add(this.tbLocalCacheSubFolder);
            this.gbLocalCacheParams.Controls.Add(this.label1);
            this.gbLocalCacheParams.Location = new System.Drawing.Point(3, 3);
            this.gbLocalCacheParams.Name = "gbLocalCacheParams";
            this.gbLocalCacheParams.Size = new System.Drawing.Size(470, 60);
            this.gbLocalCacheParams.TabIndex = 16;
            this.gbLocalCacheParams.TabStop = false;
            this.gbLocalCacheParams.Text = "Local Cache Params";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local Cache Sub Folder:";
            // 
            // tbLocalCacheSubFolder
            // 
            this.tbLocalCacheSubFolder.Location = new System.Drawing.Point(136, 25);
            this.tbLocalCacheSubFolder.Name = "tbLocalCacheSubFolder";
            this.tbLocalCacheSubFolder.Size = new System.Drawing.Size(328, 20);
            this.tbLocalCacheSubFolder.TabIndex = 2;
            // 
            // LocalCacheParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbLocalCacheParams);
            this.Name = "LocalCacheParams";
            this.Size = new System.Drawing.Size(482, 70);
            this.gbLocalCacheParams.ResumeLayout(false);
            this.gbLocalCacheParams.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLocalCacheParams;
        private System.Windows.Forms.TextBox tbLocalCacheSubFolder;
        private System.Windows.Forms.Label label1;
    }
}
