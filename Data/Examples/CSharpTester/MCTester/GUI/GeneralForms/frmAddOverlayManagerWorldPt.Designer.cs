namespace MCTester.General_Forms
{
    partial class frmAddOverlayManagerWorldPt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddOverlayManagerWorldPt));
            this.ctrl3DOMWorldPt = new MCTester.Controls.Ctrl3DVector();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrl3DOMWorldPt
            // 
            this.ctrl3DOMWorldPt.Location = new System.Drawing.Point(2, 2);
            this.ctrl3DOMWorldPt.Name = "ctrl3DOMWorldPt";
            this.ctrl3DOMWorldPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DOMWorldPt.TabIndex = 0;
            this.ctrl3DOMWorldPt.X = 0;
            this.ctrl3DOMWorldPt.Y = 0;
            this.ctrl3DOMWorldPt.Z = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(276, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(40, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmAddOverlayManagerWorldPt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(320, 32);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ctrl3DOMWorldPt);
            this.Name = "frmAddOverlayManagerWorldPt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Add World Point";
            this.Load += new System.EventHandler(this.frmAddOverlayManagerWorldPt_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MCTester.Controls.Ctrl3DVector ctrl3DOMWorldPt;
        private System.Windows.Forms.Button btnOK;
    }
}