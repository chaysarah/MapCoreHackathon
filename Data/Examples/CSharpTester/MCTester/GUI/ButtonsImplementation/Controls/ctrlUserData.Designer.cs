namespace MCTester.Controls
{
    partial class ctrlUserData
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbUserData = new System.Windows.Forms.GroupBox();
            this.txtUserData = new System.Windows.Forms.TextBox();
            this.gbUserData.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUserData
            // 
            this.gbUserData.Controls.Add(this.txtUserData);
            this.gbUserData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUserData.Location = new System.Drawing.Point(0, 0);
            this.gbUserData.Name = "gbUserData";
            this.gbUserData.Size = new System.Drawing.Size(288, 67);
            this.gbUserData.TabIndex = 0;
            this.gbUserData.TabStop = false;
            this.gbUserData.Text = "User Data";
            // 
            // txtUserData
            // 
            this.txtUserData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserData.Location = new System.Drawing.Point(3, 16);
            this.txtUserData.Multiline = true;
            this.txtUserData.Name = "txtUserData";
            this.txtUserData.Size = new System.Drawing.Size(282, 48);
            this.txtUserData.TabIndex = 8;
            // 
            // ctrlUserData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbUserData);
            this.Name = "ctrlUserData";
            this.Size = new System.Drawing.Size(288, 67);
            this.gbUserData.ResumeLayout(false);
            this.gbUserData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUserData;
        private System.Windows.Forms.TextBox txtUserData;
    }
}
