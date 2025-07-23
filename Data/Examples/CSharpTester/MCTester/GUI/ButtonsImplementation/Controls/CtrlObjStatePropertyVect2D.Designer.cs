namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyVect2D
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
            this.lblSelFVect2D = new System.Windows.Forms.Label();
            this.ctrlReg2DVector = new MCTester.Controls.Ctrl2DVector();
            this.ctrlSel2DVector = new MCTester.Controls.Ctrl2DVector();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlReg2DVector);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlReg2DVector, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSel2DVector);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSel2DVector, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // lblSelFVect2D
            // 
            this.lblSelFVect2D.Location = new System.Drawing.Point(0, 0);
            this.lblSelFVect2D.Name = "lblSelFVect2D";
            this.lblSelFVect2D.Size = new System.Drawing.Size(100, 23);
            this.lblSelFVect2D.TabIndex = 0;
            // 
            // ctrlReg2DVector
            // 
            this.ctrlReg2DVector.Location = new System.Drawing.Point(77, 46);
            this.ctrlReg2DVector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlReg2DVector.Name = "ctrlReg2DVector";
            this.ctrlReg2DVector.Size = new System.Drawing.Size(154, 23);
            this.ctrlReg2DVector.TabIndex = 23;
            this.ctrlReg2DVector.X = 0D;
            this.ctrlReg2DVector.Y = 0D;
            this.ctrlReg2DVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlReg2DVector_Validating);
            // 
            // ctrlSel2DVector
            // 
            this.ctrlSel2DVector.Location = new System.Drawing.Point(77, 46);
            this.ctrlSel2DVector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlSel2DVector.Name = "ctrlSel2DVector";
            this.ctrlSel2DVector.Size = new System.Drawing.Size(154, 23);
            this.ctrlSel2DVector.TabIndex = 36;
            this.ctrlSel2DVector.X = 0D;
            this.ctrlSel2DVector.Y = 0D;
            this.ctrlSel2DVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSel2DVector_Validating);
            // 
            // CtrlObjStatePropertyVect2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyVect2D";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblSelFVect2D;
        private Ctrl2DVector ctrlReg2DVector;
        private Ctrl2DVector ctrlSel2DVector;
    }
}
