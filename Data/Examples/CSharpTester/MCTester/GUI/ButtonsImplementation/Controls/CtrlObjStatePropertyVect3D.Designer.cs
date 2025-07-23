namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyVect3D
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
            this.ctrlSel3DVector = new MCTester.Controls.Ctrl3DVector();
            this.ctrlReg3DVector = new MCTester.Controls.Ctrl3DVector();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlReg3DVector);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlReg3DVector, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSel3DVector);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSel3DVector, 0);
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
            // ctrlSel3DVector
            // 
            this.ctrlSel3DVector.IsReadOnly = false;
            this.ctrlSel3DVector.Location = new System.Drawing.Point(76, 45);
            this.ctrlSel3DVector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlSel3DVector.Name = "ctrlSel3DVector";
            this.ctrlSel3DVector.Size = new System.Drawing.Size(232, 23);
            this.ctrlSel3DVector.TabIndex = 36;
            this.ctrlSel3DVector.X = 0D;
            this.ctrlSel3DVector.Y = 0D;
            this.ctrlSel3DVector.Z = 0D;
            this.ctrlSel3DVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSel3DVector_Validating);
            // 
            // ctrlReg3DVector
            // 
            this.ctrlReg3DVector.IsReadOnly = false;
            this.ctrlReg3DVector.Location = new System.Drawing.Point(76, 45);
            this.ctrlReg3DVector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlReg3DVector.Name = "ctrlReg3DVector";
            this.ctrlReg3DVector.Size = new System.Drawing.Size(232, 23);
            this.ctrlReg3DVector.TabIndex = 23;
            this.ctrlReg3DVector.X = 0D;
            this.ctrlReg3DVector.Y = 0D;
            this.ctrlReg3DVector.Z = 0D;
            this.ctrlReg3DVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlReg3DVector_Validating);
            // 
            // CtrlObjStatePropertyVect3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyVect3D";
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
        private Ctrl3DVector ctrlReg3DVector;
        private Ctrl3DVector ctrlSel3DVector;
    }
}
