namespace MCTester.Controls
{
    partial class CtrlPropertyEnum
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
            this.lblRegEnum = new System.Windows.Forms.Label();
            this.cmbRegEnum = new System.Windows.Forms.ComboBox();
            this.lblSelEnum = new System.Windows.Forms.Label();
            this.cmbSelEnum = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lblRegEnum);
            this.tpRegular.Controls.Add(this.cmbRegEnum);
            this.tpRegular.Controls.SetChildIndex(this.cmbRegEnum, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegEnum, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.cmbSelEnum);
            this.tpSelection.Controls.Add(this.lblSelEnum);
            this.tpSelection.Controls.SetChildIndex(this.lblSelEnum, 0);
            this.tpSelection.Controls.SetChildIndex(this.cmbSelEnum, 0);
            // 
            // lblRegEnum
            // 
            this.lblRegEnum.AutoSize = true;
            this.lblRegEnum.Location = new System.Drawing.Point(85, 54);
            this.lblRegEnum.Name = "lblRegEnum";
            this.lblRegEnum.Size = new System.Drawing.Size(66, 13);
            this.lblRegEnum.TabIndex = 18;
            this.lblRegEnum.Text = "Enum Lable:";
            // 
            // cmbRegEnum
            // 
            this.cmbRegEnum.FormattingEnabled = true;
            this.cmbRegEnum.Location = new System.Drawing.Point(154, 51);
            this.cmbRegEnum.Name = "cmbRegEnum";
            this.cmbRegEnum.Size = new System.Drawing.Size(217, 21);
            this.cmbRegEnum.TabIndex = 19;
            // 
            // lblSelEnum
            // 
            this.lblSelEnum.AutoSize = true;
            this.lblSelEnum.Location = new System.Drawing.Point(85, 54);
            this.lblSelEnum.Name = "lblSelEnum";
            this.lblSelEnum.Size = new System.Drawing.Size(66, 13);
            this.lblSelEnum.TabIndex = 27;
            this.lblSelEnum.Text = "Enum Lable:";
            // 
            // cmbSelEnum
            // 
            this.cmbSelEnum.FormattingEnabled = true;
            this.cmbSelEnum.Location = new System.Drawing.Point(154, 51);
            this.cmbSelEnum.Name = "cmbSelEnum";
            this.cmbSelEnum.Size = new System.Drawing.Size(217, 21);
            this.cmbSelEnum.TabIndex = 28;
            // 
            // CtrlPropertyEnum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyEnum";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cmbRegEnum;
        private System.Windows.Forms.Label lblRegEnum;
        public System.Windows.Forms.ComboBox cmbSelEnum;
        private System.Windows.Forms.Label lblSelEnum;





    }
}
