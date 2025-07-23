namespace MCTester.Controls
{
    partial class CtrlFontButtons
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
            this.btnDeleteFont = new System.Windows.Forms.Button();
            this.btnUpdateFont = new System.Windows.Forms.Button();
            this.btnCreateFont = new System.Windows.Forms.Button();
            this.btnRecreateFont = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDeleteFont
            // 
            this.btnDeleteFont.Location = new System.Drawing.Point(204, -1);
            this.btnDeleteFont.Name = "btnDeleteFont";
            this.btnDeleteFont.Size = new System.Drawing.Size(59, 23);
            this.btnDeleteFont.TabIndex = 74;
            this.btnDeleteFont.Text = "Delete";
            this.btnDeleteFont.UseVisualStyleBackColor = true;
            this.btnDeleteFont.Click += new System.EventHandler(this.btnDeleteFont_Click);
            // 
            // btnUpdateInReplaceFont
            // 
            this.btnUpdateFont.Location = new System.Drawing.Point(133, -1);
            this.btnUpdateFont.Name = "btnUpdateInReplaceFont";
            this.btnUpdateFont.Size = new System.Drawing.Size(69, 23);
            this.btnUpdateFont.TabIndex = 73;
            this.btnUpdateFont.Text = "Update";
            this.btnUpdateFont.UseVisualStyleBackColor = true;
            this.btnUpdateFont.Click += new System.EventHandler(this.btnUpdateFont_Click);
            // 
            // btnCreateFont
            // 
            this.btnCreateFont.Location = new System.Drawing.Point(-1, -1);
            this.btnCreateFont.Name = "btnCreateFont";
            this.btnCreateFont.Size = new System.Drawing.Size(56, 23);
            this.btnCreateFont.TabIndex = 72;
            this.btnCreateFont.Text = "Create";
            this.btnCreateFont.UseVisualStyleBackColor = true;
            this.btnCreateFont.Click += new System.EventHandler(this.btnCreateFont_Click);
            // 
            // btnUpdateFontNew
            // 
            this.btnRecreateFont.Location = new System.Drawing.Point(57, -1);
            this.btnRecreateFont.Name = "btnRecreateFont";
            this.btnRecreateFont.Size = new System.Drawing.Size(74, 23);
            this.btnRecreateFont.TabIndex = 75;
            this.btnRecreateFont.Text = "Re-create";
            this.btnRecreateFont.UseVisualStyleBackColor = true;
            this.btnRecreateFont.Click += new System.EventHandler(this.btnRecreateFont_Click);
            // 
            // CtrlFontButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRecreateFont);
            this.Controls.Add(this.btnDeleteFont);
            this.Controls.Add(this.btnUpdateFont);
            this.Controls.Add(this.btnCreateFont);
            this.Name = "CtrlFontButtons";
            this.Size = new System.Drawing.Size(268, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDeleteFont;
        private System.Windows.Forms.Button btnUpdateFont;
        private System.Windows.Forms.Button btnCreateFont;
        private System.Windows.Forms.Button btnRecreateFont;
    }
}
