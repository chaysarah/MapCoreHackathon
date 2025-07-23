namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyUint
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
            this.ntbSelProperty = new MCTester.Controls.NumericTextBox();
            this.ntbRegProperty = new MCTester.Controls.NumericTextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ntbRegProperty);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntbRegProperty, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ntbSelProperty);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntbSelProperty, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // ntbSelProperty
            // 
            this.ntbSelProperty.Location = new System.Drawing.Point(76, 48);
            this.ntbSelProperty.Name = "ntbSelProperty";
            this.ntbSelProperty.Size = new System.Drawing.Size(100, 20);
            this.ntbSelProperty.TabIndex = 35;
            this.ntbSelProperty.Validating += new System.ComponentModel.CancelEventHandler(this.ntbSelProperty_Validating);
            // 
            // ntbRegProperty
            // 
            this.ntbRegProperty.Location = new System.Drawing.Point(76, 48);
            this.ntbRegProperty.Name = "ntbRegProperty";
            this.ntbRegProperty.Size = new System.Drawing.Size(100, 20);
            this.ntbRegProperty.TabIndex = 24;
            this.ntbRegProperty.Validating += new System.ComponentModel.CancelEventHandler(this.ntbRegProperty_Validating);
            // 
            // CtrlObjStatePropertyUint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyUint";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private NumericTextBox ntbSelProperty;
        private NumericTextBox ntbRegProperty;
    }
}
