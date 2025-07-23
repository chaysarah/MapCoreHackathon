namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyEBoundingRectanglePoint
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
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRegEnum = new System.Windows.Forms.ComboBox();
            this.cmbSelEnum = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.cmbRegEnum);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.cmbRegEnum, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.cmbSelEnum);
            this.tpObjectState.Controls.Add(this.label6);
            this.tpObjectState.Controls.Add(this.label4);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label4, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label6, 0);
            this.tpObjectState.Controls.SetChildIndex(this.cmbSelEnum, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, -11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "label4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(112, -11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "label6";
            // 
            // cmbRegEnum
            // 
            this.cmbRegEnum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegEnum.FormattingEnabled = true;
            this.cmbRegEnum.Location = new System.Drawing.Point(76, 46);
            this.cmbRegEnum.Name = "cmbRegEnum";
            this.cmbRegEnum.Size = new System.Drawing.Size(226, 21);
            this.cmbRegEnum.TabIndex = 24;
            this.cmbRegEnum.Validating += new System.ComponentModel.CancelEventHandler(this.cmbRegEnum_Validating);
            // 
            // cmbSelEnum
            // 
            this.cmbSelEnum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelEnum.FormattingEnabled = true;
            this.cmbSelEnum.Location = new System.Drawing.Point(76, 46);
            this.cmbSelEnum.Name = "cmbSelEnum";
            this.cmbSelEnum.Size = new System.Drawing.Size(226, 21);
            this.cmbSelEnum.TabIndex = 37;
            this.cmbSelEnum.Validating += new System.ComponentModel.CancelEventHandler(this.cmbSelEnum_Validating);
            // 
            // CtrlObjStatePropertyEBoundingRectanglePoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyEBoundingRectanglePoint";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRegEnum;
        private System.Windows.Forms.ComboBox cmbSelEnum;
    }
}
