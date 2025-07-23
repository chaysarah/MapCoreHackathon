namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmOverlayManagerList
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
            this.lstOverlayManager = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(205, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstOverlayManager
            // 
            this.lstOverlayManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstOverlayManager.FormattingEnabled = true;
            this.lstOverlayManager.Location = new System.Drawing.Point(0, 0);
            this.lstOverlayManager.Name = "lstOverlayManager";
            this.lstOverlayManager.Size = new System.Drawing.Size(292, 225);
            this.lstOverlayManager.TabIndex = 1;
            // 
            // frmOverlayManagerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(292, 259);
            this.Controls.Add(this.lstOverlayManager);
            this.Controls.Add(this.btnOK);
            this.Name = "frmOverlayManagerList";
            this.Text = "frmOverlayManagerList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstOverlayManager;
    }
}