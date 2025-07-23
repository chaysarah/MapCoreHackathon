namespace MCTester.MapWorld.MapUserControls
{
    partial class frmGlobalMapViewportList
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
            this.lstViewports = new System.Windows.Forms.ListBox();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(207, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 24);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstViewports
            // 
            this.lstViewports.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstViewports.FormattingEnabled = true;
            this.lstViewports.Location = new System.Drawing.Point(0, 0);
            this.lstViewports.Name = "lstViewports";
            this.lstViewports.Size = new System.Drawing.Size(275, 251);
            this.lstViewports.TabIndex = 5;
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(0, 257);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(94, 24);
            this.btnClearSelection.TabIndex = 7;
            this.btnClearSelection.Text = "Clear Selection";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Visible = false;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // frmGlobalMapViewportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(275, 284);
            this.Controls.Add(this.btnClearSelection);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstViewports);
            this.Name = "frmGlobalMapViewportList";
            this.Text = "frmGlobalMapViewportList";
            this.Load += new System.EventHandler(this.frmGlobalMapViewportList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstViewports;
        private System.Windows.Forms.Button btnClearSelection;
    }
}