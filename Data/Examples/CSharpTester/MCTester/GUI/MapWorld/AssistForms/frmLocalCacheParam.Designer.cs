namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmLocalCacheParam
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
            this.ctrlLocalCacheParams = new MCTester.Controls.LocalCacheParams();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlLocalCacheParams
            // 
            this.ctrlLocalCacheParams.Location = new System.Drawing.Point(4, 11);
            this.ctrlLocalCacheParams.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlLocalCacheParams.Name = "ctrlLocalCacheParams";
            this.ctrlLocalCacheParams.Size = new System.Drawing.Size(631, 83);
            this.ctrlLocalCacheParams.SubFolderPath = "";
            this.ctrlLocalCacheParams.TabIndex = 33;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(560, 102);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 34;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmLocalCacheParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(653, 141);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ctrlLocalCacheParams);
            this.Name = "frmLocalCacheParam";
            this.Text = "Local Cache Params";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.LocalCacheParams ctrlLocalCacheParams;
        private System.Windows.Forms.Button btnOK;
    }
}