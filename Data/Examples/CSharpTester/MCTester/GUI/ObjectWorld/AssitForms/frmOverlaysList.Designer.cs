namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmOverlaysList
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
            this.lstOverlay = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstOverlay
            // 
            this.lstOverlay.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstOverlay.FormattingEnabled = true;
            this.lstOverlay.Location = new System.Drawing.Point(0, 0);
            this.lstOverlay.Name = "lstOverlay";
            this.lstOverlay.Size = new System.Drawing.Size(272, 238);
            this.lstOverlay.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(197, 244);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmOverlaysList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(272, 273);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstOverlay);
            this.Name = "frmOverlaysList";
            this.Text = "frmSchemeList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstOverlay;
        private System.Windows.Forms.Button btnOK;
    }
}