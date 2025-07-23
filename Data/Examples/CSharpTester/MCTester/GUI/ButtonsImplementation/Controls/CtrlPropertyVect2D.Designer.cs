namespace MCTester.Controls
{
    partial class CtrlPropertyVect2D
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
            this.lblRegVect2D = new System.Windows.Forms.Label();
            this.ctrl2DRegVector = new MCTester.Controls.Ctrl2DVector();
            this.lblSelVect2D = new System.Windows.Forms.Label();
            this.ctrl2DSelVector = new MCTester.Controls.Ctrl2DVector();
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
            this.tpRegular.Controls.Add(this.lblRegVect2D);
            this.tpRegular.Controls.Add(this.ctrl2DRegVector);
            this.tpRegular.Size = new System.Drawing.Size(386, 85);
            this.tpRegular.Controls.SetChildIndex(this.ctrl2DRegVector, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegVect2D, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lblSelVect2D);
            this.tpSelection.Controls.Add(this.ctrl2DSelVector);
            this.tpSelection.Controls.SetChildIndex(this.ctrl2DSelVector, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelVect2D, 0);
            // 
            // lblRegVect2D
            // 
            this.lblRegVect2D.AutoSize = true;
            this.lblRegVect2D.Location = new System.Drawing.Point(85, 54);
            this.lblRegVect2D.Name = "lblRegVect2D";
            this.lblRegVect2D.Size = new System.Drawing.Size(36, 13);
            this.lblRegVect2D.TabIndex = 18;
            this.lblRegVect2D.Text = "Lable:";
            // 
            // ctrl2DRegVector
            // 
            this.ctrl2DRegVector.Location = new System.Drawing.Point(154, 49);
            this.ctrl2DRegVector.Name = "ctrl2DRegVector";
            this.ctrl2DRegVector.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DRegVector.TabIndex = 19;
            this.ctrl2DRegVector.X = 0;
            this.ctrl2DRegVector.Y = 0;
            // 
            // lblSelVect2D
            // 
            this.lblSelVect2D.AutoSize = true;
            this.lblSelVect2D.Location = new System.Drawing.Point(85, 54);
            this.lblSelVect2D.Name = "lblSelVect2D";
            this.lblSelVect2D.Size = new System.Drawing.Size(36, 13);
            this.lblSelVect2D.TabIndex = 27;
            this.lblSelVect2D.Text = "Lable:";
            // 
            // ctrl2DSelVector
            // 
            this.ctrl2DSelVector.Location = new System.Drawing.Point(154, 49);
            this.ctrl2DSelVector.Name = "ctrl2DSelVector";
            this.ctrl2DSelVector.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DSelVector.TabIndex = 28;
            this.ctrl2DSelVector.X = 0;
            this.ctrl2DSelVector.Y = 0;
            // 
            // CtrlPropertyVect2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyVect2D";
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

        private System.Windows.Forms.Label lblRegVect2D;
        private MCTester.Controls.Ctrl2DVector ctrl2DRegVector;
        private System.Windows.Forms.Label lblSelVect2D;
        private MCTester.Controls.Ctrl2DVector ctrl2DSelVector;
    }
}
