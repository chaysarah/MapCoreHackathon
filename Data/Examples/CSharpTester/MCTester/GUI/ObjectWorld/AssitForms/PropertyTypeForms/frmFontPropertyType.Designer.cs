namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmFontPropertyType
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
            this.label2 = new System.Windows.Forms.Label();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.chkIsUnicode = new System.Windows.Forms.CheckBox();
            this.ctrlFontButtons1 = new MCTester.Controls.CtrlFontButtons();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Font:";
            // 
            // chkIsUnicode
            // 
            this.chkIsUnicode.AutoSize = true;
            this.chkIsUnicode.Location = new System.Drawing.Point(175, 43);
            this.chkIsUnicode.Name = "chkIsUnicode";
            this.chkIsUnicode.Size = new System.Drawing.Size(77, 17);
            this.chkIsUnicode.TabIndex = 5;
            this.chkIsUnicode.Text = "Is Unicode";
            this.chkIsUnicode.UseVisualStyleBackColor = true;
            this.chkIsUnicode.Visible = false;
            // 
            // ctrlFontButtons1
            // 
            this.ctrlFontButtons1.Location = new System.Drawing.Point(81, 13);
            this.ctrlFontButtons1.Name = "ctrlFontButtons1";
            this.ctrlFontButtons1.Size = new System.Drawing.Size(268, 21);
            this.ctrlFontButtons1.TabIndex = 6;
            // 
            // frmFontPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.ctrlFontButtons1);
            this.Controls.Add(this.chkIsUnicode);
            this.Controls.Add(this.label2);
            this.Name = "frmFontPropertyType";
            this.Text = "frmFontPropertyType";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.chkIsUnicode, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.ctrlFontButtons1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.CheckBox chkIsUnicode;
        private Controls.CtrlFontButtons ctrlFontButtons1;
    }
}