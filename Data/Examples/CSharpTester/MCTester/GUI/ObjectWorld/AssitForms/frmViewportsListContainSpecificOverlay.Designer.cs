namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmViewportsListContainSpecificOverlay
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
            this.lstViewports = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxMinScale = new MCTester.Controls.NumericTextBox();
            this.ntxMaxScale = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Viewports:";
            // 
            // lstViewports
            // 
            this.lstViewports.FormattingEnabled = true;
            this.lstViewports.ItemHeight = 16;
            this.lstViewports.Location = new System.Drawing.Point(8, 31);
            this.lstViewports.Margin = new System.Windows.Forms.Padding(4);
            this.lstViewports.Name = "lstViewports";
            this.lstViewports.Size = new System.Drawing.Size(340, 244);
            this.lstViewports.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(249, 372);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(4, 299);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Min Scale:";
            // 
            // ntxMinScale
            // 
            this.ntxMinScale.Enabled = false;
            this.ntxMinScale.Location = new System.Drawing.Point(92, 295);
            this.ntxMinScale.Margin = new System.Windows.Forms.Padding(4);
            this.ntxMinScale.Name = "ntxMinScale";
            this.ntxMinScale.Size = new System.Drawing.Size(132, 22);
            this.ntxMinScale.TabIndex = 7;
            this.ntxMinScale.Text = "0";
            // 
            // ntxMaxScale
            // 
            this.ntxMaxScale.Enabled = false;
            this.ntxMaxScale.Location = new System.Drawing.Point(92, 327);
            this.ntxMaxScale.Margin = new System.Windows.Forms.Padding(4);
            this.ntxMaxScale.Name = "ntxMaxScale";
            this.ntxMaxScale.Size = new System.Drawing.Size(132, 22);
            this.ntxMaxScale.TabIndex = 9;
            this.ntxMaxScale.Text = "MAX";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(4, 331);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Max Scale:";
            // 
            // frmViewportsListContainSpecificOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(355, 405);
            this.Controls.Add(this.ntxMaxScale);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ntxMinScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstViewports);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmViewportsListContainSpecificOverlay";
            this.Text = "frmViewportsListContainSpecificOverlay";
            this.Load += new System.EventHandler(this.frmViewportsListContainSpecificOverlay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstViewports;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxMinScale;
        private MCTester.Controls.NumericTextBox ntxMaxScale;
        private System.Windows.Forms.Label label3;
    }
}