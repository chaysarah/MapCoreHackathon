namespace MCTester.Controls
{
    partial class CtrlPropertyFont
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
            this.chxSelUnicodeText = new System.Windows.Forms.CheckBox();
            this.lblSelLength = new System.Windows.Forms.Label();
            this.numUDSelTextLength = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSelTextLength)).BeginInit();
            this.SuspendLayout();
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lblSelLength);
            this.tpSelection.Controls.Add(this.numUDSelTextLength);
            this.tpSelection.Controls.Add(this.chxSelUnicodeText);
            this.tpSelection.Controls.SetChildIndex(this.chxSelUnicodeText, 0);
            this.tpSelection.Controls.SetChildIndex(this.numUDSelTextLength, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelLength, 0);
            // 
            // chxSelUnicodeText
            // 
            this.chxSelUnicodeText.AutoSize = true;
            this.chxSelUnicodeText.Location = new System.Drawing.Point(291, 30);
            this.chxSelUnicodeText.Name = "chxSelUnicodeText";
            this.chxSelUnicodeText.Size = new System.Drawing.Size(77, 17);
            this.chxSelUnicodeText.TabIndex = 29;
            this.chxSelUnicodeText.Text = "Is Unicode";
            this.chxSelUnicodeText.UseVisualStyleBackColor = true;
            // 
            // lblSelLength
            // 
            this.lblSelLength.AutoSize = true;
            this.lblSelLength.Location = new System.Drawing.Point(242, 56);
            this.lblSelLength.Name = "lblSelLength";
            this.lblSelLength.Size = new System.Drawing.Size(43, 13);
            this.lblSelLength.TabIndex = 31;
            this.lblSelLength.Text = "Length:";
            // 
            // numUDSelTextLength
            // 
            this.numUDSelTextLength.Location = new System.Drawing.Point(291, 54);
            this.numUDSelTextLength.Name = "numUDSelTextLength";
            this.numUDSelTextLength.Size = new System.Drawing.Size(68, 20);
            this.numUDSelTextLength.TabIndex = 30;
            // 
            // CtrlPropertyFont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyFont";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSelTextLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chxSelUnicodeText;
        private System.Windows.Forms.Label lblSelLength;
        private System.Windows.Forms.NumericUpDown numUDSelTextLength;
    }
}
