namespace MCTester.Controls
{
    partial class CtrlObjStatePropertySubPartOffset
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
            this.ctrlReg3DFVectorOffset = new MCTester.Controls.Ctrl3DFVector();
            this.ctrlSel3DFVectorOffset = new MCTester.Controls.Ctrl3DFVector();
            this.ntxAttachPointID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxAttachPointID);
            this.groupBox1.Controls.Add(this.lblRegMeshTextureID);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(395, 158);
            this.groupBox1.Controls.SetChildIndex(this.tcProperty, 0);
            this.groupBox1.Controls.SetChildIndex(this.lblRegMeshTextureID, 0);
            this.groupBox1.Controls.SetChildIndex(this.ntxAttachPointID, 0);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 44);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4);
            this.tcProperty.Size = new System.Drawing.Size(387, 110);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlReg3DFVectorOffset);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Size = new System.Drawing.Size(379, 84);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlReg3DFVectorOffset, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSel3DFVectorOffset);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(379, 84);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSel3DFVectorOffset, 0);
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
            this.lblRegMeshTextureID.Size = new System.Drawing.Size(82, 13);
            this.lblRegMeshTextureID.TabIndex = 31;
            this.lblRegMeshTextureID.Text = "Attach Point ID:";
            // 
            // ctrlReg3DFVectorOffset
            // 
            this.ctrlReg3DFVectorOffset.Location = new System.Drawing.Point(76, 45);
            this.ctrlReg3DFVectorOffset.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlReg3DFVectorOffset.Name = "ctrlReg3DFVectorOffset";
            this.ctrlReg3DFVectorOffset.Size = new System.Drawing.Size(232, 23);
            this.ctrlReg3DFVectorOffset.TabIndex = 25;
            this.ctrlReg3DFVectorOffset.X = 0F;
            this.ctrlReg3DFVectorOffset.Y = 0F;
            this.ctrlReg3DFVectorOffset.Z = 0F;
            this.ctrlReg3DFVectorOffset.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlReg3DFVectorOffset_Validating);
            // 
            // ctrlSel3DFVectorOffset
            // 
            this.ctrlSel3DFVectorOffset.Location = new System.Drawing.Point(76, 45);
            this.ctrlSel3DFVectorOffset.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSel3DFVectorOffset.Name = "ctrlSel3DFVectorOffset";
            this.ctrlSel3DFVectorOffset.Size = new System.Drawing.Size(232, 23);
            this.ctrlSel3DFVectorOffset.TabIndex = 36;
            this.ctrlSel3DFVectorOffset.X = 0F;
            this.ctrlSel3DFVectorOffset.Y = 0F;
            this.ctrlSel3DFVectorOffset.Z = 0F;
            this.ctrlSel3DFVectorOffset.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSel3DFVectorOffset_Validating);
            // 
            // ntxAttachPointID
            // 
            this.ntxAttachPointID.Location = new System.Drawing.Point(92, 19);
            this.ntxAttachPointID.Name = "ntxAttachPointID";
            this.ntxAttachPointID.Size = new System.Drawing.Size(70, 20);
            this.ntxAttachPointID.TabIndex = 33;
            this.ntxAttachPointID.TextChanged += new System.EventHandler(this.ntxAttachPointID_TextChanged);
            // 
            // CtrlObjStatePropertySubPartOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertySubPartOffset";
            this.Size = new System.Drawing.Size(395, 158);
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
        private Ctrl3DFVector ctrlReg3DFVectorOffset;
        private Ctrl3DFVector ctrlSel3DFVectorOffset;
        private System.Windows.Forms.TextBox ntxAttachPointID;
    }
}
