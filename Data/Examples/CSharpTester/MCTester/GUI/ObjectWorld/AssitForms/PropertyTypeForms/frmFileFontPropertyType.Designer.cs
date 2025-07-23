namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmFileFontPropertyType
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
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.cbIsMemoryBuffer = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ctrlBrowseFileFont = new MCTester.Controls.CtrlBrowseControl();
            this.ntbFontHeight = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ntxPropertyID
            // 
            this.ntxPropertyID.Location = new System.Drawing.Point(86, 46);
            this.ntxPropertyID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // cbIsMemoryBuffer
            // 
            this.cbIsMemoryBuffer.AutoSize = true;
            this.cbIsMemoryBuffer.Checked = true;
            this.cbIsMemoryBuffer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsMemoryBuffer.Location = new System.Drawing.Point(182, 46);
            this.cbIsMemoryBuffer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbIsMemoryBuffer.Name = "cbIsMemoryBuffer";
            this.cbIsMemoryBuffer.Size = new System.Drawing.Size(105, 17);
            this.cbIsMemoryBuffer.TabIndex = 66;
            this.cbIsMemoryBuffer.Text = "Is Memory Buffer";
            this.cbIsMemoryBuffer.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 64;
            this.label9.Text = "Font Height:";
            // 
            // ctrlBrowseFileFont
            // 
            this.ctrlBrowseFileFont.AutoSize = true;
            this.ctrlBrowseFileFont.FileName = "";
            this.ctrlBrowseFileFont.Filter = "";
            this.ctrlBrowseFileFont.IsFolderDialog = false;
            this.ctrlBrowseFileFont.IsFullPath = true;
            this.ctrlBrowseFileFont.IsSaveFile = false;
            this.ctrlBrowseFileFont.LabelCaption = "File Name:      ";
            this.ctrlBrowseFileFont.Location = new System.Drawing.Point(81, 1);
            this.ctrlBrowseFileFont.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseFileFont.MultiFilesSelect = false;
            this.ctrlBrowseFileFont.Name = "ctrlBrowseFileFont";
            this.ctrlBrowseFileFont.Prefix = "";
            this.ctrlBrowseFileFont.Size = new System.Drawing.Size(324, 24);
            this.ctrlBrowseFileFont.TabIndex = 63;
            // 
            // ntbFontHeight
            // 
            this.ntbFontHeight.Location = new System.Drawing.Point(86, 25);
            this.ntbFontHeight.Name = "ntbFontHeight";
            this.ntbFontHeight.Size = new System.Drawing.Size(56, 20);
            this.ntbFontHeight.TabIndex = 65;
            this.ntbFontHeight.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "File Name:";
            // 
            // frmFileFontPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 69);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbIsMemoryBuffer);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ctrlBrowseFileFont);
            this.Controls.Add(this.ntbFontHeight);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmFileFontPropertyType";
            this.Text = "frmFontPropertyType";
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.ntbFontHeight, 0);
            this.Controls.SetChildIndex(this.ctrlBrowseFileFont, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.cbIsMemoryBuffer, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.CheckBox cbIsMemoryBuffer;
        private System.Windows.Forms.Label label9;
        private Controls.CtrlBrowseControl ctrlBrowseFileFont;
        private Controls.NumericTextBox ntbFontHeight;
        private System.Windows.Forms.Label label2;
    }
}