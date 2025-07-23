namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyMesh
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
            this.ctrlRegMeshButtons = new MCTester.Controls.CtrlMeshButtons();
            this.ctrlSelMeshButtons = new MCTester.Controls.CtrlMeshButtons();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Text = "Mesh";
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegMeshButtons);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegMeshButtons, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelMeshButtons);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelMeshButtons, 0);
            // 
            // ctrlRegMeshButtons
            // 
            this.ctrlRegMeshButtons.Location = new System.Drawing.Point(82, 47);
            this.ctrlRegMeshButtons.Name = "ctrlRegMeshButtons";
            this.ctrlRegMeshButtons.Size = new System.Drawing.Size(268, 21);
            this.ctrlRegMeshButtons.TabIndex = 43;
            // 
            // ctrlSelMeshButtons
            // 
            this.ctrlSelMeshButtons.Location = new System.Drawing.Point(82, 47);
            this.ctrlSelMeshButtons.Name = "ctrlSelMeshButtons";
            this.ctrlSelMeshButtons.Size = new System.Drawing.Size(268, 21);
            this.ctrlSelMeshButtons.TabIndex = 44;
            // 
            // CtrlObjStatePropertyMesh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlObjStatePropertyMesh";
            this.PropertyName = "Mesh";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private CtrlMeshButtons ctrlRegMeshButtons;
        private CtrlMeshButtons ctrlSelMeshButtons;
    }
}
