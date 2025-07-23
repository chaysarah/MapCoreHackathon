namespace MCTester.General_Forms
{
    partial class frmStandaloneItemsList
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
            this.btnStandaloneItemsOK = new System.Windows.Forms.Button();
            this.lstStandaloneItems = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnStandaloneItemsOK
            // 
            this.btnStandaloneItemsOK.Location = new System.Drawing.Point(162, 244);
            this.btnStandaloneItemsOK.Name = "btnStandaloneItemsOK";
            this.btnStandaloneItemsOK.Size = new System.Drawing.Size(75, 23);
            this.btnStandaloneItemsOK.TabIndex = 4;
            this.btnStandaloneItemsOK.Text = "OK";
            this.btnStandaloneItemsOK.UseVisualStyleBackColor = true;
            this.btnStandaloneItemsOK.Click += new System.EventHandler(this.btnStandaloneItemsOK_Click);
            // 
            // lstStandaloneItems
            // 
            this.lstStandaloneItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstStandaloneItems.FormattingEnabled = true;
            this.lstStandaloneItems.Location = new System.Drawing.Point(0, 0);
            this.lstStandaloneItems.Name = "lstStandaloneItems";
            this.lstStandaloneItems.Size = new System.Drawing.Size(241, 238);
            this.lstStandaloneItems.TabIndex = 3;
            // 
            // frmStandaloneItemsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(241, 272);
            this.Controls.Add(this.btnStandaloneItemsOK);
            this.Controls.Add(this.lstStandaloneItems);
            this.Name = "frmStandaloneItemsList";
            this.Text = "frmStandaloneItemsList";
            this.Load += new System.EventHandler(this.frmStandaloneItemsList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStandaloneItemsOK;
        private System.Windows.Forms.ListBox lstStandaloneItems;
    }
}