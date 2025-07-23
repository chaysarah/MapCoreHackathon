namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyFVect2D
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
            this.ctrl2DRegFVector = new MCTester.Controls.Ctrl2DFVector();
            this.ctrl2DSelFVector = new MCTester.Controls.Ctrl2DFVector();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrl2DRegFVector);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrl2DRegFVector, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrl2DSelFVector);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrl2DSelFVector, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lblSelFVect2D
            // 
            this.lblSelFVect2D.Location = new System.Drawing.Point(0, 0);
            this.lblSelFVect2D.Name = "lblSelFVect2D";
            this.lblSelFVect2D.Size = new System.Drawing.Size(100, 23);
            this.lblSelFVect2D.TabIndex = 0;
            // 
            // ctrl2DRegFVector
            // 
            this.ctrl2DRegFVector.Location = new System.Drawing.Point(78, 45);
            this.ctrl2DRegFVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DRegFVector.Name = "ctrl2DRegFVector";
            this.ctrl2DRegFVector.Size = new System.Drawing.Size(154, 23);
            this.ctrl2DRegFVector.TabIndex = 18;
            this.ctrl2DRegFVector.X = 0F;
            this.ctrl2DRegFVector.Y = 0F;
            this.ctrl2DRegFVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl2DRegFVector_Validating);
            // 
            // ctrl2DSelFVector
            // 
            this.ctrl2DSelFVector.Location = new System.Drawing.Point(78, 45);
            this.ctrl2DSelFVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DSelFVector.Name = "ctrl2DSelFVector";
            this.ctrl2DSelFVector.Size = new System.Drawing.Size(154, 23);
            this.ctrl2DSelFVector.TabIndex = 34;
            this.ctrl2DSelFVector.X = 0F;
            this.ctrl2DSelFVector.Y = 0F;
            this.ctrl2DSelFVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl2DSelFVector_Validating);
            // 
            // CtrlObjStatePropertyFVect2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyFVect2D";
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
        private MCTester.Controls.Ctrl2DFVector ctrl2DRegFVector;
        private Ctrl2DFVector ctrl2DSelFVector;
    }
}
