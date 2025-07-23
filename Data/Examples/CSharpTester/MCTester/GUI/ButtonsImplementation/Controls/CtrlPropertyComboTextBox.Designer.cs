namespace MCTester.Controls
{
    partial class CtrlPropertyComboTextBox
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
            this.ntxRegValue = new MCTester.Controls.NumericTextBox();
            this.lblRegValue = new System.Windows.Forms.Label();
            this.ntxSelValue = new MCTester.Controls.NumericTextBox();
            this.lblSelValue = new System.Windows.Forms.Label();
            this.cmbRegEnum = new System.Windows.Forms.ComboBox();
            this.cmbSelEnum = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lblRegValue);
            this.tpRegular.Controls.Add(this.cmbRegEnum);
            this.tpRegular.Controls.Add(this.ntxRegValue);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegValue, 0);
            this.tpRegular.Controls.SetChildIndex(this.cmbRegEnum, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegValue, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.cmbSelEnum);
            this.tpSelection.Controls.Add(this.ntxSelValue);
            this.tpSelection.Controls.Add(this.lblSelValue);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelValue, 0);
            this.tpSelection.Controls.SetChildIndex(this.ntxSelValue, 0);
            this.tpSelection.Controls.SetChildIndex(this.cmbSelEnum, 0);
            // 
            // ntxRegValue
            // 
            this.ntxRegValue.Location = new System.Drawing.Point(297, 53);
            this.ntxRegValue.Name = "ntxRegValue";
            this.ntxRegValue.Size = new System.Drawing.Size(83, 20);
            this.ntxRegValue.TabIndex = 18;
            this.ntxRegValue.TextChanged += new System.EventHandler(this.ntxRegValue_TextChanged);
            // 
            // lblRegValue
            // 
            this.lblRegValue.AutoSize = true;
            this.lblRegValue.Location = new System.Drawing.Point(85, 56);
            this.lblRegValue.Name = "lblRegValue";
            this.lblRegValue.Size = new System.Drawing.Size(36, 13);
            this.lblRegValue.TabIndex = 19;
            this.lblRegValue.Text = "Lable:";
            // 
            // ntxSelValue
            // 
            this.ntxSelValue.Location = new System.Drawing.Point(297, 53);
            this.ntxSelValue.Name = "ntxSelValue";
            this.ntxSelValue.Size = new System.Drawing.Size(83, 20);
            this.ntxSelValue.TabIndex = 27;
            this.ntxSelValue.TextChanged += new System.EventHandler(this.ntxSelValue_TextChanged);
            // 
            // lblSelValue
            // 
            this.lblSelValue.AutoSize = true;
            this.lblSelValue.Location = new System.Drawing.Point(85, 56);
            this.lblSelValue.Name = "lblSelValue";
            this.lblSelValue.Size = new System.Drawing.Size(36, 13);
            this.lblSelValue.TabIndex = 28;
            this.lblSelValue.Text = "Lable:";
            // 
            // cmbRegEnum
            // 
            this.cmbRegEnum.FormattingEnabled = true;
            this.cmbRegEnum.Location = new System.Drawing.Point(127, 52);
            this.cmbRegEnum.Name = "cmbRegEnum";
            this.cmbRegEnum.Size = new System.Drawing.Size(164, 21);
            this.cmbRegEnum.TabIndex = 25;
            this.cmbRegEnum.SelectedIndexChanged += new System.EventHandler(this.cmbRegEnum_SelectedIndexChanged);
            // 
            // cmbSelEnum
            // 
            this.cmbSelEnum.FormattingEnabled = true;
            this.cmbSelEnum.Location = new System.Drawing.Point(127, 52);
            this.cmbSelEnum.Name = "cmbSelEnum";
            this.cmbSelEnum.Size = new System.Drawing.Size(164, 21);
            this.cmbSelEnum.TabIndex = 29;
            this.cmbSelEnum.SelectedIndexChanged += new System.EventHandler(this.cmbSelEnum_SelectedIndexChanged);
            // 
            // CtrlPropertyComboTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyComboTextBox";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NumericTextBox ntxRegValue;
        private System.Windows.Forms.Label lblRegValue;
        private NumericTextBox ntxSelValue;
        private System.Windows.Forms.Label lblSelValue;
        private System.Windows.Forms.ComboBox cmbRegEnum;
        private System.Windows.Forms.ComboBox cmbSelEnum;
    }
}
