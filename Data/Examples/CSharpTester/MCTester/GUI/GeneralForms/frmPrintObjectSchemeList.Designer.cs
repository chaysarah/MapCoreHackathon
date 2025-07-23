namespace MCTester.General_Forms
{
    partial class frmPrintObjectSchemeList
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
            this.lstObjectScheme = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(146, 231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstObjectScheme
            // 
            this.lstObjectScheme.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstObjectScheme.FormattingEnabled = true;
            this.lstObjectScheme.Location = new System.Drawing.Point(0, 0);
            this.lstObjectScheme.Name = "lstObjectScheme";
            this.lstObjectScheme.Size = new System.Drawing.Size(221, 225);
            this.lstObjectScheme.TabIndex = 1;
            this.lstObjectScheme.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstObjectScheme_MouseDown);
            // 
            // frmPrintObjectSchemeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(221, 261);
            this.Controls.Add(this.lstObjectScheme);
            this.Controls.Add(this.btnOK);
            this.Name = "frmPrintObjectSchemeList";
            this.Text = "frmPrintObjectSchemeList";
            this.Load += new System.EventHandler(this.frmPrintObjectSchemeList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstObjectScheme;
    }
}