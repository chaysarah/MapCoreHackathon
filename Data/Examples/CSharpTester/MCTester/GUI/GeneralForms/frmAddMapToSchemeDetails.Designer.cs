namespace MCTester.General_Forms
{
    partial class frmAddMapToSchemeDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSchemeArea = new System.Windows.Forms.TextBox();
            this.txtOverlayManagerName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSchemeMapType = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSchemeComments = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scheme Area:";
            // 
            // txtSchemeArea
            // 
            this.txtSchemeArea.Location = new System.Drawing.Point(421, 26);
            this.txtSchemeArea.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtSchemeArea.Name = "txtSchemeArea";
            this.txtSchemeArea.Size = new System.Drawing.Size(260, 38);
            this.txtSchemeArea.TabIndex = 1;
            // 
            // txtOverlayManagerName
            // 
            this.txtOverlayManagerName.Location = new System.Drawing.Point(421, 212);
            this.txtOverlayManagerName.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtOverlayManagerName.Name = "txtOverlayManagerName";
            this.txtOverlayManagerName.Size = new System.Drawing.Size(260, 38);
            this.txtOverlayManagerName.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(80, 219);
            this.label10.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(322, 32);
            this.label10.TabIndex = 18;
            this.label10.Text = "Overlay Manager Name:";
            // 
            // txtSchemeMapType
            // 
            this.txtSchemeMapType.Location = new System.Drawing.Point(421, 86);
            this.txtSchemeMapType.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtSchemeMapType.Name = "txtSchemeMapType";
            this.txtSchemeMapType.Size = new System.Drawing.Size(260, 38);
            this.txtSchemeMapType.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(80, 93);
            this.label11.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(259, 32);
            this.label11.TabIndex = 20;
            this.label11.Text = "Scheme Map Type:";
            // 
            // txtSchemeComments
            // 
            this.txtSchemeComments.Location = new System.Drawing.Point(421, 150);
            this.txtSchemeComments.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.txtSchemeComments.Name = "txtSchemeComments";
            this.txtSchemeComments.Size = new System.Drawing.Size(260, 38);
            this.txtSchemeComments.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(80, 157);
            this.label12.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(270, 32);
            this.label12.TabIndex = 22;
            this.label12.Text = "Scheme Comments:";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(285, 308);
            this.btnOK.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(200, 55);
            this.btnOK.TabIndex = 24;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmAddMapToSchemeDetails
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(787, 398);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtSchemeComments);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtSchemeMapType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtOverlayManagerName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtSchemeArea);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "frmAddMapToSchemeDetails";
            this.Text = "Add Map To Scheme Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSchemeArea;
        private System.Windows.Forms.TextBox txtOverlayManagerName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSchemeMapType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSchemeComments;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnOK;
    }
}