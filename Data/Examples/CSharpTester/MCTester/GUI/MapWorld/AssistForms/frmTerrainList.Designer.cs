namespace MCTester.MapWorld.MapUserControls
{
    partial class frmTerrainList
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
            this.lstTerrains = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstTerrains
            // 
            this.lstTerrains.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstTerrains.FormattingEnabled = true;
            this.lstTerrains.Location = new System.Drawing.Point(0, 0);
            this.lstTerrains.Name = "lstTerrains";
            this.lstTerrains.Size = new System.Drawing.Size(275, 251);
            this.lstTerrains.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(207, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 24);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmTerrainList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(275, 284);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstTerrains);
            this.Name = "frmTerrainList";
            this.Text = "frmTerrainList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstTerrains;
        private System.Windows.Forms.Button btnOK;
    }
}