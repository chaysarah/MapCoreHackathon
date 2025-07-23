namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyShort
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
            this.ntbRegProperty = new MCTester.Controls.NumericTextBox();
            this.ntbSelProperty = new MCTester.Controls.NumericTextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ntbRegProperty);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntbRegProperty, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ntbSelProperty);
            this.tpObjectState.Controls.Add(this.label6);
            this.tpObjectState.Controls.Add(this.label4);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label4, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label6, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntbSelProperty, 0);
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
            // ntbRegProperty
            // 
            this.ntbRegProperty.Location = new System.Drawing.Point(76, 48);
            this.ntbRegProperty.Name = "ntbRegProperty";
            this.ntbRegProperty.Size = new System.Drawing.Size(100, 20);
            this.ntbRegProperty.TabIndex = 24;
            // 
            // ntbSelProperty
            // 
            this.ntbSelProperty.Location = new System.Drawing.Point(76, 48);
            this.ntbSelProperty.Name = "ntbSelProperty";
            this.ntbSelProperty.Size = new System.Drawing.Size(100, 20);
            this.ntbSelProperty.TabIndex = 37;
            // 
            // CtrlObjStatePropertyShort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlObjStatePropertyShort";
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
        private NumericTextBox ntbRegProperty;
        private NumericTextBox ntbSelProperty;
    }
}
