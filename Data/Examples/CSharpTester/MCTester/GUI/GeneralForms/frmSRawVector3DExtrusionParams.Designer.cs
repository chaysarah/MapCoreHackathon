namespace MCTester.General_Forms
{
    partial class frmSRawVector3DExtrusionParams
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
            this.ctrlRawVector3DExtrusionParams1 = new MCTester.Controls.CtrlRawVector3DExtrusionParams();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlRawVector3DExtrusionParams1
            // 
            this.ctrlRawVector3DExtrusionParams1.Location = new System.Drawing.Point(6, 2);
            this.ctrlRawVector3DExtrusionParams1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlRawVector3DExtrusionParams1.Name = "ctrlRawVector3DExtrusionParams1";
            this.ctrlRawVector3DExtrusionParams1.Size = new System.Drawing.Size(736, 586);
            this.ctrlRawVector3DExtrusionParams1.TabIndex = 27;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(336, 601);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(92, 22);
            this.btnOK.TabIndex = 33;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmSRawVector3DExtrusionParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(763, 625);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ctrlRawVector3DExtrusionParams1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSRawVector3DExtrusionParams";
            this.Text = "Raw Vector 3D Extrusion Params";
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.CtrlRawVector3DExtrusionParams ctrlRawVector3DExtrusionParams1;
        private System.Windows.Forms.Button btnOK;
    }
}