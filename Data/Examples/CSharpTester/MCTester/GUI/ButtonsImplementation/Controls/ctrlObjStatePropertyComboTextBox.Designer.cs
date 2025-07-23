namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyComboTextBox
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
            this.cmbRegEnum = new System.Windows.Forms.ComboBox();
            this.ntxRegValue = new MCTester.Controls.NumericTextBox();
            this.cmbSelEnum = new System.Windows.Forms.ComboBox();
            this.ntxSelValue = new MCTester.Controls.NumericTextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.cmbRegEnum);
            this.tpRegular.Controls.Add(this.ntxRegValue);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegValue, 0);
            this.tpRegular.Controls.SetChildIndex(this.cmbRegEnum, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ntxSelValue);
            this.tpObjectState.Controls.Add(this.cmbSelEnum);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.cmbSelEnum, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelValue, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // cmbRegEnum
            // 
            this.cmbRegEnum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegEnum.FormattingEnabled = true;
            this.cmbRegEnum.Location = new System.Drawing.Point(85, 48);
            this.cmbRegEnum.Name = "cmbRegEnum";
            this.cmbRegEnum.Size = new System.Drawing.Size(164, 21);
            this.cmbRegEnum.TabIndex = 28;
            this.cmbRegEnum.SelectedIndexChanged += new System.EventHandler(this.cmbRegEnum_SelectedIndexChanged);
            // 
            // ntxRegValue
            // 
            this.ntxRegValue.Location = new System.Drawing.Point(255, 48);
            this.ntxRegValue.Name = "ntxRegValue";
            this.ntxRegValue.Size = new System.Drawing.Size(83, 20);
            this.ntxRegValue.TabIndex = 26;
            this.ntxRegValue.TextChanged += new System.EventHandler(this.ntxRegValue_TextChanged);
            this.ntxRegValue.Validating += new System.ComponentModel.CancelEventHandler(this.ntxRegValue_Validating);
            // 
            // cmbSelEnum
            // 
            this.cmbSelEnum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelEnum.FormattingEnabled = true;
            this.cmbSelEnum.Location = new System.Drawing.Point(85, 48);
            this.cmbSelEnum.Name = "cmbSelEnum";
            this.cmbSelEnum.Size = new System.Drawing.Size(164, 21);
            this.cmbSelEnum.TabIndex = 36;
            this.cmbSelEnum.SelectedIndexChanged += new System.EventHandler(this.cmbSelEnum_SelectedIndexChanged);
            // 
            // ntxSelValue
            // 
            this.ntxSelValue.Location = new System.Drawing.Point(255, 48);
            this.ntxSelValue.Name = "ntxSelValue";
            this.ntxSelValue.Size = new System.Drawing.Size(83, 20);
            this.ntxSelValue.TabIndex = 37;
            this.ntxSelValue.TextChanged += new System.EventHandler(this.ntxSelValue_TextChanged);
            this.ntxSelValue.Validating += new System.ComponentModel.CancelEventHandler(this.ntxSelValue_Validating);
            // 
            // CtrlObjStatePropertyComboTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyComboTextBox";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbRegEnum;
        private NumericTextBox ntxRegValue;
        private System.Windows.Forms.ComboBox cmbSelEnum;
        private NumericTextBox ntxSelValue;
    }
}
