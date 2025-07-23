namespace MCTester.MapWorld.MapUserControls
{
    partial class frmSectionMapPolygonItem
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
            this.btnSectionMapPolygonItemOK = new System.Windows.Forms.Button();
            this.lstSectionMapPolygonItem = new System.Windows.Forms.ListBox();
            this.btnClearViewportSelection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSectionMapPolygonItemOK
            // 
            this.btnSectionMapPolygonItemOK.Location = new System.Drawing.Point(217, 242);
            this.btnSectionMapPolygonItemOK.Name = "btnSectionMapPolygonItemOK";
            this.btnSectionMapPolygonItemOK.Size = new System.Drawing.Size(75, 23);
            this.btnSectionMapPolygonItemOK.TabIndex = 0;
            this.btnSectionMapPolygonItemOK.Text = "OK";
            this.btnSectionMapPolygonItemOK.UseVisualStyleBackColor = true;
            this.btnSectionMapPolygonItemOK.Click += new System.EventHandler(this.btnSectionMapPolygonItemOK_Click);
            // 
            // lstSectionMapPolygonItem
            // 
            this.lstSectionMapPolygonItem.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstSectionMapPolygonItem.FormattingEnabled = true;
            this.lstSectionMapPolygonItem.Location = new System.Drawing.Point(0, 0);
            this.lstSectionMapPolygonItem.Name = "lstSectionMapPolygonItem";
            this.lstSectionMapPolygonItem.Size = new System.Drawing.Size(292, 238);
            this.lstSectionMapPolygonItem.TabIndex = 1;
            // 
            // btnClearViewportSelection
            // 
            this.btnClearViewportSelection.Location = new System.Drawing.Point(0, 242);
            this.btnClearViewportSelection.Name = "btnClearViewportSelection";
            this.btnClearViewportSelection.Size = new System.Drawing.Size(42, 23);
            this.btnClearViewportSelection.TabIndex = 44;
            this.btnClearViewportSelection.Text = "Clear";
            this.btnClearViewportSelection.UseVisualStyleBackColor = true;
            this.btnClearViewportSelection.Click += new System.EventHandler(this.btnClearViewportSelection_Click);
            // 
            // frmSectionMapPolygonItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.btnClearViewportSelection);
            this.Controls.Add(this.lstSectionMapPolygonItem);
            this.Controls.Add(this.btnSectionMapPolygonItemOK);
            this.Name = "frmSectionMapPolygonItem";
            this.Text = "frmSectionMapPolygonItem";
            this.Load += new System.EventHandler(this.frmSectionMapPolygonItem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSectionMapPolygonItemOK;
        private System.Windows.Forms.ListBox lstSectionMapPolygonItem;
        private System.Windows.Forms.Button btnClearViewportSelection;
    }
}