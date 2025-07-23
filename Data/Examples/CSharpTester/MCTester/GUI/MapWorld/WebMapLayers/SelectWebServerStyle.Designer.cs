namespace MCTester.MapWorld.WebMapLayers
{
    partial class SelectWebServerStyle
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
            this.ctrlCheckedListBox1 = new MCTester.Controls.CtrlCheckedListBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(182, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ctrlCheckedListBox1
            // 
            this.ctrlCheckedListBox1.Location = new System.Drawing.Point(0, 1);
            this.ctrlCheckedListBox1.Name = "ctrlCheckedListBox1";
            this.ctrlCheckedListBox1.Size = new System.Drawing.Size(274, 113);
            this.ctrlCheckedListBox1.TabIndex = 2;
            // 
            // SelectWebServerStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(281, 150);
            this.Controls.Add(this.ctrlCheckedListBox1);
            this.Controls.Add(this.btnOK);
            this.Name = "SelectWebServerStyle";
            this.Text = "Select Web Server Style";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private Controls.CtrlCheckedListBox ctrlCheckedListBox1;
    }
}