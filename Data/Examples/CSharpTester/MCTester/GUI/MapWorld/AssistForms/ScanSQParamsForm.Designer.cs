namespace MCTester.MapWorld
{
    partial class ScanSQParamsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanSQParamsForm));
            this.ctrlQueryParamsScan = new MCTester.Controls.CtrlQueryParams();
            this.btnScanSQParamsOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlQueryParamsScan
            // 
            this.ctrlQueryParamsScan.Location = new System.Drawing.Point(-1, 1);
            this.ctrlQueryParamsScan.Name = "ctrlQueryParamsScan";
            this.ctrlQueryParamsScan.Size = new System.Drawing.Size(436, 530);
            this.ctrlQueryParamsScan.TabIndex = 0;
            // 
            // btnScanSQParamsOK
            // 
            this.btnScanSQParamsOK.Location = new System.Drawing.Point(351, 537);
            this.btnScanSQParamsOK.Name = "btnScanSQParamsOK";
            this.btnScanSQParamsOK.Size = new System.Drawing.Size(75, 23);
            this.btnScanSQParamsOK.TabIndex = 1;
            this.btnScanSQParamsOK.Text = "OK";
            this.btnScanSQParamsOK.UseVisualStyleBackColor = true;
            this.btnScanSQParamsOK.Click += new System.EventHandler(this.btnScanSQParamsOK_Click);
            // 
            // ScanSQParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(438, 567);
            this.Controls.Add(this.btnScanSQParamsOK);
            this.Controls.Add(this.ctrlQueryParamsScan);
            this.Name = "ScanSQParamsForm";
            this.Text = "ScanSQParamsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MCTester.Controls.CtrlQueryParams ctrlQueryParamsScan;
        private System.Windows.Forms.Button btnScanSQParamsOK;
    }
}