namespace MCTester.MapWorld.MapUserControls
{
    partial class frmSaveLoad
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
            this.btnOpenSaveFileName = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtBaseDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenSaveBaseDir = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkSaveUserData = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnOpenSaveFileName
            // 
            this.btnOpenSaveFileName.Location = new System.Drawing.Point(369, 13);
            this.btnOpenSaveFileName.Name = "btnOpenSaveFileName";
            this.btnOpenSaveFileName.Size = new System.Drawing.Size(28, 23);
            this.btnOpenSaveFileName.TabIndex = 0;
            this.btnOpenSaveFileName.Text = "...";
            this.btnOpenSaveFileName.UseVisualStyleBackColor = true;
            this.btnOpenSaveFileName.Click += new System.EventHandler(this.btnOpenSaveFileName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Name:";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(97, 16);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(266, 20);
            this.txtFileName.TabIndex = 2;
            // 
            // txtBaseDirectory
            // 
            this.txtBaseDirectory.Location = new System.Drawing.Point(97, 42);
            this.txtBaseDirectory.Name = "txtBaseDirectory";
            this.txtBaseDirectory.Size = new System.Drawing.Size(266, 20);
            this.txtBaseDirectory.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Base Directory:";
            // 
            // btnOpenSaveBaseDir
            // 
            this.btnOpenSaveBaseDir.Location = new System.Drawing.Point(369, 39);
            this.btnOpenSaveBaseDir.Name = "btnOpenSaveBaseDir";
            this.btnOpenSaveBaseDir.Size = new System.Drawing.Size(28, 23);
            this.btnOpenSaveBaseDir.TabIndex = 3;
            this.btnOpenSaveBaseDir.Text = "...";
            this.btnOpenSaveBaseDir.UseVisualStyleBackColor = true;
            this.btnOpenSaveBaseDir.Click += new System.EventHandler(this.btnOpenSaveBaseDir_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(322, 104);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkSaveUserData
            // 
            this.chkSaveUserData.AutoSize = true;
            this.chkSaveUserData.Location = new System.Drawing.Point(15, 68);
            this.chkSaveUserData.Name = "chkSaveUserData";
            this.chkSaveUserData.Size = new System.Drawing.Size(102, 17);
            this.chkSaveUserData.TabIndex = 9;
            this.chkSaveUserData.Text = "Save User Data";
            this.chkSaveUserData.UseVisualStyleBackColor = true;
            // 
            // frmSaveLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(402, 133);
            this.Controls.Add(this.chkSaveUserData);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBaseDirectory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenSaveBaseDir);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenSaveFileName);
            this.Name = "frmSaveLoad";
            this.Text = "frmSaveLoadTerrain";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenSaveFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtBaseDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenSaveBaseDir;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkSaveUserData;
    }
}