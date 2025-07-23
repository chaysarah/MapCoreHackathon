namespace MCTester.Controls
{
    partial class CtrlPropertyVect3D
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
            this.lblRegVect = new System.Windows.Forms.Label();
            this.ctrl3DRegVector = new MCTester.Controls.Ctrl3DVector();
            this.lblSelVect = new System.Windows.Forms.Label();
            this.ctrlSel3DVector = new MCTester.Controls.Ctrl3DVector();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(400, 130);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(394, 111);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lblRegVect);
            this.tpRegular.Controls.Add(this.ctrl3DRegVector);
            this.tpRegular.Size = new System.Drawing.Size(386, 85);
            this.tpRegular.Controls.SetChildIndex(this.ctrl3DRegVector, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegVect, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lblSelVect);
            this.tpSelection.Controls.Add(this.ctrlSel3DVector);
            this.tpSelection.Controls.SetChildIndex(this.ctrlSel3DVector, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelVect, 0);
            // 
            // lblRegVect
            // 
            this.lblRegVect.AutoSize = true;
            this.lblRegVect.Location = new System.Drawing.Point(85, 54);
            this.lblRegVect.Name = "lblRegVect";
            this.lblRegVect.Size = new System.Drawing.Size(36, 13);
            this.lblRegVect.TabIndex = 19;
            this.lblRegVect.Text = "Lable:";
            // 
            // ctrl3DRegVector
            // 
            this.ctrl3DRegVector.Location = new System.Drawing.Point(155, 48);
            this.ctrl3DRegVector.Name = "ctrl3DRegVector";
            this.ctrl3DRegVector.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DRegVector.TabIndex = 20;
            this.ctrl3DRegVector.X = 0;
            this.ctrl3DRegVector.Y = 0;
            this.ctrl3DRegVector.Z = 0;
            // 
            // lblSelVect
            // 
            this.lblSelVect.AutoSize = true;
            this.lblSelVect.Location = new System.Drawing.Point(85, 54);
            this.lblSelVect.Name = "lblSelVect";
            this.lblSelVect.Size = new System.Drawing.Size(36, 13);
            this.lblSelVect.TabIndex = 27;
            this.lblSelVect.Text = "Lable:";
            // 
            // ctrlSel3DVector
            // 
            this.ctrlSel3DVector.Location = new System.Drawing.Point(155, 48);
            this.ctrlSel3DVector.Name = "ctrlSel3DVector";
            this.ctrlSel3DVector.Size = new System.Drawing.Size(232, 26);
            this.ctrlSel3DVector.TabIndex = 28;
            this.ctrlSel3DVector.X = 0;
            this.ctrlSel3DVector.Y = 0;
            this.ctrlSel3DVector.Z = 0;
            // 
            // CtrlPropertyVect3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyVect3D";
            this.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRegVect;
        private MCTester.Controls.Ctrl3DVector ctrl3DRegVector;
        private System.Windows.Forms.Label lblSelVect;
        private MCTester.Controls.Ctrl3DVector ctrlSel3DVector;
    }
}
