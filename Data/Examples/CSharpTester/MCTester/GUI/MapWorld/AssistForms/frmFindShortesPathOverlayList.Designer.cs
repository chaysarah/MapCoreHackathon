namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmFindShortesPathOverlayList
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
            this.lstOverlays = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(117, 248);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstOverlays
            // 
            this.lstOverlays.FormattingEnabled = true;
            this.lstOverlays.Location = new System.Drawing.Point(2, 2);
            this.lstOverlays.Name = "lstOverlays";
            this.lstOverlays.Size = new System.Drawing.Size(190, 238);
            this.lstOverlays.TabIndex = 1;
            // 
            // frmFindShortesPathOverlayList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(197, 274);
            this.Controls.Add(this.lstOverlays);
            this.Controls.Add(this.btnOK);
            this.Name = "frmFindShortesPathOverlayList";
            this.Text = "frmFinedShortesPathOMList";
            this.Load += new System.EventHandler(this.frmFindShortesPathOverlayList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstOverlays;
    }
}