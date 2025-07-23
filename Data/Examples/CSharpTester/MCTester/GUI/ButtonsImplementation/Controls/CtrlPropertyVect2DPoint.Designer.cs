namespace MCTester.Controls
{
    partial class CtrlPropertyVect2DPoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlPropertyVect2DPoint));
            this.lblRegVect2D = new System.Windows.Forms.Label();
            this.ctrl2DRegVector = new MCTester.Controls.Ctrl2DVector();
            this.lblSelVect2D = new System.Windows.Forms.Label();
            this.ctrl2DSelVector = new MCTester.Controls.Ctrl2DVector();
            this.RegVector2DPoint = new MCTester.Controls.CtrlSamplePoint();
            this.SelVector2DPoint = new MCTester.Controls.CtrlSamplePoint();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lblRegVect2D);
            this.tpRegular.Controls.Add(this.ctrl2DRegVector);
            this.tpRegular.Controls.Add(this.RegVector2DPoint);
            this.tpRegular.Controls.SetChildIndex(this.RegVector2DPoint, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrl2DRegVector, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegVect2D, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.SelVector2DPoint);
            this.tpSelection.Controls.Add(this.lblSelVect2D);
            this.tpSelection.Controls.Add(this.ctrl2DSelVector);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.ctrl2DSelVector, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelVect2D, 0);
            this.tpSelection.Controls.SetChildIndex(this.SelVector2DPoint, 0);
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
            this.ctrl2DRegVector.X = 0D;
            this.ctrl2DRegVector.Y = 0D;
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
            this.ctrl2DSelVector.X = 0D;
            this.ctrl2DSelVector.Y = 0D;
            // 
            // RegVector2DPoint
            // 
            this.RegVector2DPoint._DgvControlName = null;
            this.RegVector2DPoint._PointInOverlayManagerCoordSys = true;
            this.RegVector2DPoint._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.RegVector2DPoint._SampleOnePoint = true;
            this.RegVector2DPoint._UserControlName = "ctrl2DRegVector";
            this.RegVector2DPoint.Location = new System.Drawing.Point(314, 52);
            this.RegVector2DPoint.Name = "RegVector2DPoint";
            this.RegVector2DPoint.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.RegVector2DPoint.Size = new System.Drawing.Size(39, 23);
            this.RegVector2DPoint.TabIndex = 25;
            this.RegVector2DPoint.Text = "...";
            this.RegVector2DPoint.UseVisualStyleBackColor = true;
            // 
            // SelVector2DPoint
            // 
            this.SelVector2DPoint._DgvControlName = null;
            this.SelVector2DPoint._PointInOverlayManagerCoordSys = true;
            this.SelVector2DPoint._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.SelVector2DPoint._SampleOnePoint = true;
            this.SelVector2DPoint._UserControlName = "ctrl2DSelVector";
            this.SelVector2DPoint.Location = new System.Drawing.Point(314, 49);
            this.SelVector2DPoint.Name = "SelVector2DPoint";
            this.SelVector2DPoint.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.SelVector2DPoint.Size = new System.Drawing.Size(38, 23);
            this.SelVector2DPoint.TabIndex = 29;
            this.SelVector2DPoint.Text = "...";
            this.SelVector2DPoint.UseVisualStyleBackColor = true;
            // 
            // CtrlPropertyVect2DPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyVect2DPoint";
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
        private CtrlSamplePoint RegVector2DPoint;
        private CtrlSamplePoint SelVector2DPoint;
    }
}
