namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyFVect3D
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
            this.ctrl3DRegFVector = new MCTester.Controls.Ctrl3DFVector();
            this.ctrl3DSelFVector = new MCTester.Controls.Ctrl3DFVector();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(5, 15);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4);
            this.tcProperty.Size = new System.Drawing.Size(390, 110);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrl3DRegFVector);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Size = new System.Drawing.Size(382, 84);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrl3DRegFVector, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrl3DSelFVector);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(4);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(4);
            this.tpObjectState.Size = new System.Drawing.Size(382, 84);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrl3DSelFVector, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // ctrl3DRegFVector
            // 
            this.ctrl3DRegFVector.Location = new System.Drawing.Point(76, 45);
            this.ctrl3DRegFVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DRegFVector.Name = "ctrl3DRegFVector";
            this.ctrl3DRegFVector.Size = new System.Drawing.Size(232, 23);
            this.ctrl3DRegFVector.TabIndex = 19;
            this.ctrl3DRegFVector.X = 0F;
            this.ctrl3DRegFVector.Y = 0F;
            this.ctrl3DRegFVector.Z = 0F;
            this.ctrl3DRegFVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl3DRegFVector_Validating);
            // 
            // ctrl3DSelFVector
            // 
            this.ctrl3DSelFVector.Location = new System.Drawing.Point(76, 45);
            this.ctrl3DSelFVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DSelFVector.Name = "ctrl3DSelFVector";
            this.ctrl3DSelFVector.Size = new System.Drawing.Size(232, 23);
            this.ctrl3DSelFVector.TabIndex = 21;
            this.ctrl3DSelFVector.X = 0F;
            this.ctrl3DSelFVector.Y = 0F;
            this.ctrl3DSelFVector.Z = 0F;
            this.ctrl3DSelFVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl3DSelFVector_Validating);
            // 
            // CtrlObjStatePropertyFVect3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "CtrlObjStatePropertyFVect3D";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public MCTester.Controls.Ctrl3DFVector ctrl3DRegFVector;
        public MCTester.Controls.Ctrl3DFVector ctrl3DSelFVector;
    }
}
