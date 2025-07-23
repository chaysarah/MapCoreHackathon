namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmRawVectorParams
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
            this.button1 = new System.Windows.Forms.Button();
            this.rawVectorParams1 = new MCTester.Controls.CtrlRawVectorParams();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(310, 565);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 22);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rawVectorParams1
            // 
            this.rawVectorParams1.Location = new System.Drawing.Point(0, 2);
            this.rawVectorParams1.Margin = new System.Windows.Forms.Padding(2);
            this.rawVectorParams1.Name = "rawVectorParams1";
            this.rawVectorParams1.Size = new System.Drawing.Size(681, 559);
            this.rawVectorParams1.TabIndex = 2;
            this.rawVectorParams1.TargetGridCoordinateSystem = null;
            // 
            // frmRawVectorParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(686, 597);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rawVectorParams1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmRawVectorParams";
            this.Text = "Raw Vector Params";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtrlRawVectorParams rawVectorParams1;
        private System.Windows.Forms.Button button1;
    }
}