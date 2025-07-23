namespace MCTester.GUI.PC.GUI.Forms
{
    partial class ExistingTextureListForm
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
            this.lstTextures = new System.Windows.Forms.ListBox();
            this.btnDeleteTexture = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(535, 270);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstTextures
            // 
            this.lstTextures.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstTextures.FormattingEnabled = true;
            this.lstTextures.Location = new System.Drawing.Point(0, 0);
            this.lstTextures.Name = "lstTextures";
            this.lstTextures.Size = new System.Drawing.Size(610, 264);
            this.lstTextures.TabIndex = 1;
            // 
            // btnDeleteTexture
            // 
            this.btnDeleteTexture.Location = new System.Drawing.Point(0, 270);
            this.btnDeleteTexture.Name = "btnDeleteTexture";
            this.btnDeleteTexture.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTexture.TabIndex = 2;
            this.btnDeleteTexture.Text = "Delete";
            this.btnDeleteTexture.UseVisualStyleBackColor = true;
            this.btnDeleteTexture.Click += new System.EventHandler(this.btnDeleteTexture_Click);
            // 
            // ExistingTextureListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 296);
            this.Controls.Add(this.btnDeleteTexture);
            this.Controls.Add(this.lstTextures);
            this.Controls.Add(this.btnOK);
            this.Name = "ExistingTextureListForm";
            this.Text = "ExistingTextureListForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ExistingTextureListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstTextures;
        private System.Windows.Forms.Button btnDeleteTexture;
    }
}