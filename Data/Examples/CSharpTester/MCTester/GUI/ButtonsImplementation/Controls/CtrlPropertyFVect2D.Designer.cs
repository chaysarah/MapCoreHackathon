namespace MCTester.Controls
{
    partial class CtrlPropertyFVect2D
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
            this.lblRegFVec2D = new System.Windows.Forms.Label();
            this.lblSelFVect2D = new System.Windows.Forms.Label();
            this.ctrl2DSelFVector = new MCTester.Controls.Ctrl2DFVector();
            this.ctrl2DRegFVector = new MCTester.Controls.Ctrl2DFVector();
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
            this.tpRegular.Controls.Add(this.lblRegFVec2D);
            this.tpRegular.Controls.Add(this.ctrl2DRegFVector);
            this.tpRegular.Size = new System.Drawing.Size(386, 85);
            this.tpRegular.Controls.SetChildIndex(this.ctrl2DRegFVector, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegFVec2D, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lblSelFVect2D);
            this.tpSelection.Controls.Add(this.ctrl2DSelFVector);
            this.tpSelection.Controls.SetChildIndex(this.ctrl2DSelFVector, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelFVect2D, 0);
            // 
            // lblRegFVec2D
            // 
            this.lblRegFVec2D.AutoSize = true;
            this.lblRegFVec2D.Location = new System.Drawing.Point(85, 54);
            this.lblRegFVec2D.Name = "lblRegFVec2D";
            this.lblRegFVec2D.Size = new System.Drawing.Size(81, 13);
            this.lblRegFVec2D.TabIndex = 19;
            this.lblRegFVec2D.Text = "FVect2D Lable:";
            // 
            // lblSelFVect2D
            // 
            this.lblSelFVect2D.AutoSize = true;
            this.lblSelFVect2D.Location = new System.Drawing.Point(85, 54);
            this.lblSelFVect2D.Name = "lblSelFVect2D";
            this.lblSelFVect2D.Size = new System.Drawing.Size(81, 13);
            this.lblSelFVect2D.TabIndex = 28;
            this.lblSelFVect2D.Text = "FVect2D Lable:";
            // 
            // ctrl2DSelFVector
            // 
            this.ctrl2DSelFVector.Location = new System.Drawing.Point(172, 48);
            this.ctrl2DSelFVector.Name = "ctrl2DSelFVector";
            this.ctrl2DSelFVector.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DSelFVector.TabIndex = 27;
            this.ctrl2DSelFVector.X = 0F;
            this.ctrl2DSelFVector.Y = 0F;
            // 
            // ctrl2DRegFVector
            // 
            this.ctrl2DRegFVector.Location = new System.Drawing.Point(172, 48);
            this.ctrl2DRegFVector.Name = "ctrl2DRegFVector";
            this.ctrl2DRegFVector.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DRegFVector.TabIndex = 18;
            this.ctrl2DRegFVector.X = 0F;
            this.ctrl2DRegFVector.Y = 0F;
            // 
            // CtrlPropertyFVect2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyFVect2D";
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

        private System.Windows.Forms.Label lblRegFVec2D;
        private System.Windows.Forms.Label lblSelFVect2D;
        private MCTester.Controls.Ctrl2DFVector ctrl2DSelFVector;
        private MCTester.Controls.Ctrl2DFVector ctrl2DRegFVector;
    }
}
