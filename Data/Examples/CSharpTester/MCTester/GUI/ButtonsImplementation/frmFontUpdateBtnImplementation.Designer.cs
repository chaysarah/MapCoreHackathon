namespace MCTester.ButtonsImplementation
{
    partial class frmFontUpdateBtnImplementation
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
            this.lstExistingFont = new System.Windows.Forms.ListBox();
            this.btnDeleteFont = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.NewFontDialog = new System.Windows.Forms.FontDialog();
            this.chxIsUnicode = new System.Windows.Forms.CheckBox();
            this.chxIsEmbedded = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstExistingFont
            // 
            this.lstExistingFont.FormattingEnabled = true;
            this.lstExistingFont.Location = new System.Drawing.Point(12, 35);
            this.lstExistingFont.Name = "lstExistingFont";
            this.lstExistingFont.Size = new System.Drawing.Size(198, 134);
            this.lstExistingFont.TabIndex = 3;
            this.lstExistingFont.SelectedIndexChanged += new System.EventHandler(this.lstExistingFont_SelectedIndexChanged);
            this.lstExistingFont.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstExistingFont_MouseMove);
            // 
            // btnDeleteFont
            // 
            this.btnDeleteFont.Location = new System.Drawing.Point(36, 175);
            this.btnDeleteFont.Name = "btnDeleteFont";
            this.btnDeleteFont.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteFont.TabIndex = 9;
            this.btnDeleteFont.Text = "Delete";
            this.btnDeleteFont.UseVisualStyleBackColor = true;
            this.btnDeleteFont.Click += new System.EventHandler(this.btnDeleteFont_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(117, 175);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(93, 23);
            this.btnUpdate.TabIndex = 34;
            this.btnUpdate.Text = "Update Existing";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // chxIsUnicode
            // 
            this.chxIsUnicode.AutoSize = true;
            this.chxIsUnicode.Location = new System.Drawing.Point(12, 12);
            this.chxIsUnicode.Name = "chxIsUnicode";
            this.chxIsUnicode.Size = new System.Drawing.Size(77, 17);
            this.chxIsUnicode.TabIndex = 35;
            this.chxIsUnicode.Text = "Is Unicode";
            this.chxIsUnicode.UseVisualStyleBackColor = true;
            // 
            // chxIsEmbedded
            // 
            this.chxIsEmbedded.AutoSize = true;
            this.chxIsEmbedded.Location = new System.Drawing.Point(117, 12);
            this.chxIsEmbedded.Name = "chxIsEmbedded";
            this.chxIsEmbedded.Size = new System.Drawing.Size(88, 17);
            this.chxIsEmbedded.TabIndex = 40;
            this.chxIsEmbedded.Text = "Is Embedded";
            this.chxIsEmbedded.UseVisualStyleBackColor = true;
            // 
            // frmFontUpdateBtnImplementation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(217, 205);
            this.Controls.Add(this.chxIsEmbedded);
            this.Controls.Add(this.chxIsUnicode);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDeleteFont);
            this.Controls.Add(this.lstExistingFont);
            this.Name = "frmFontUpdateBtnImplementation";
            this.Text = "frmFontUpdateBtnImplementation";
            this.Load += new System.EventHandler(this.frmFontUpdateBtnImplementation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstExistingFont;
        private System.Windows.Forms.Button btnDeleteFont;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.FontDialog NewFontDialog;
        private System.Windows.Forms.CheckBox chxIsUnicode;
        private System.Windows.Forms.CheckBox chxIsEmbedded;
    }
}