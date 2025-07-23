namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyTextureScroll
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
            this.lblRegMeshTextureID = new System.Windows.Forms.Label();
            this.ctrl2DRegFVector = new MCTester.Controls.Ctrl2DFVector();
            this.ctrl2DSelFVector = new MCTester.Controls.Ctrl2DFVector();
            this.ntxRegMeshTextureID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxRegMeshTextureID);
            this.groupBox1.Controls.Add(this.lblRegMeshTextureID);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(395, 154);
            this.groupBox1.Controls.SetChildIndex(this.tcProperty, 0);
            this.groupBox1.Controls.SetChildIndex(this.lblRegMeshTextureID, 0);
            this.groupBox1.Controls.SetChildIndex(this.ntxRegMeshTextureID, 0);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 41);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4);
            this.tcProperty.Size = new System.Drawing.Size(387, 109);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrl2DRegFVector);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Size = new System.Drawing.Size(379, 83);
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
            this.tpObjectState.Size = new System.Drawing.Size(379, 83);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrl2DSelFVector, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lblRegMeshTextureID
            // 
            this.lblRegMeshTextureID.AutoSize = true;
            this.lblRegMeshTextureID.Location = new System.Drawing.Point(4, 22);
            this.lblRegMeshTextureID.Name = "lblRegMeshTextureID";
            this.lblRegMeshTextureID.Size = new System.Drawing.Size(60, 13);
            this.lblRegMeshTextureID.TabIndex = 31;
            this.lblRegMeshTextureID.Text = "Texture ID:";
            // 
            // ctrl2DRegFVector
            // 
            this.ctrl2DRegFVector.Enabled = false;
            this.ctrl2DRegFVector.Location = new System.Drawing.Point(76, 45);
            this.ctrl2DRegFVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DRegFVector.Name = "ctrl2DRegFVector";
            this.ctrl2DRegFVector.Size = new System.Drawing.Size(154, 23);
            this.ctrl2DRegFVector.TabIndex = 23;
            this.ctrl2DRegFVector.X = 0F;
            this.ctrl2DRegFVector.Y = 0F;
            this.ctrl2DRegFVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl2DRegFVector_Validating);
            // 
            // ctrl2DSelFVector
            // 
            this.ctrl2DSelFVector.Enabled = false;
            this.ctrl2DSelFVector.Location = new System.Drawing.Point(76, 45);
            this.ctrl2DSelFVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DSelFVector.Name = "ctrl2DSelFVector";
            this.ctrl2DSelFVector.Size = new System.Drawing.Size(154, 23);
            this.ctrl2DSelFVector.TabIndex = 34;
            this.ctrl2DSelFVector.X = 0F;
            this.ctrl2DSelFVector.Y = 0F;
            this.ctrl2DSelFVector.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl2DSelFVector_Validating);
            // 
            // ntxRegMeshTextureID
            // 
            this.ntxRegMeshTextureID.Location = new System.Drawing.Point(70, 19);
            this.ntxRegMeshTextureID.Name = "ntxRegMeshTextureID";
            this.ntxRegMeshTextureID.Size = new System.Drawing.Size(76, 20);
            this.ntxRegMeshTextureID.TabIndex = 33;
            this.ntxRegMeshTextureID.TextChanged += new System.EventHandler(this.ntxRegMeshTextureID_TextChanged);
            // 
            // CtrlObjStatePropertyTextureScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyTextureScroll";
            this.Size = new System.Drawing.Size(395, 154);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblRegMeshTextureID;
        private Ctrl2DFVector ctrl2DRegFVector;
        private Ctrl2DFVector ctrl2DSelFVector;
        private System.Windows.Forms.TextBox ntxRegMeshTextureID;
    }
}
