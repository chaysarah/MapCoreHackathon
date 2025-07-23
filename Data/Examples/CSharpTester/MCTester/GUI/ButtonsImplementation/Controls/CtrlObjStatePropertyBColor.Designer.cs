namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyBColor
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
            this.ctrlRegColor = new MCTester.Controls.SelectColor();
            this.ctrlSelColor = new MCTester.Controls.SelectColor();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegColor);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegColor, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelColor);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelColor, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // ctrlRegColor
            // 
            this.ctrlRegColor.Location = new System.Drawing.Point(88, 46);
            this.ctrlRegColor.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlRegColor.Name = "ctrlRegColor";
            this.ctrlRegColor.Size = new System.Drawing.Size(121, 23);
            this.ctrlRegColor.TabIndex = 23;
            this.ctrlRegColor.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRegColor_Validating);
            // 
            // ctrlSelColor
            // 
            this.ctrlSelColor.Location = new System.Drawing.Point(88, 46);
            this.ctrlSelColor.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSelColor.Name = "ctrlSelColor";
            this.ctrlSelColor.Size = new System.Drawing.Size(121, 23);
            this.ctrlSelColor.TabIndex = 38;
            this.ctrlSelColor.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSelColor_Validating);
            // 
            // CtrlObjStatePropertyBColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyBColor";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SelectColor ctrlRegColor;
        private SelectColor ctrlSelColor;
    }
}
