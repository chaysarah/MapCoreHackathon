namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmSTilingSchemeParams
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
            this.ctrlTilingSchemeParams1 = new MCTester.Controls.CtrlTilingSchemeParams();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlTilingSchemeParams1
            // 
            this.ctrlTilingSchemeParams1.Location = new System.Drawing.Point(2, 7);
            this.ctrlTilingSchemeParams1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlTilingSchemeParams1.Name = "ctrlTilingSchemeParams1";
            this.ctrlTilingSchemeParams1.Size = new System.Drawing.Size(462, 140);
            this.ctrlTilingSchemeParams1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(202, 152);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(71, 29);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmSTilingSchemeParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(474, 194);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ctrlTilingSchemeParams1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmSTilingSchemeParams";
            this.Text = "Tiling Scheme Params";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtrlTilingSchemeParams ctrlTilingSchemeParams1;
        private System.Windows.Forms.Button btnOK;
    }
}