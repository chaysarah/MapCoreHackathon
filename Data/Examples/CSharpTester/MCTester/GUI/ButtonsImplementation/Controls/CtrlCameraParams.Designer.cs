namespace MCTester.Controls
{
    partial class CtrlCameraParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlCameraParams));
            this.gbFrameImageCalcCameraParams = new System.Windows.Forms.GroupBox();
            this.ctrl2DFVectorOffsetCenterPixel = new MCTester.Controls.Ctrl2DVector();
            this.ctrl2DVectorNumPixeles = new MCTester.Controls.Ctrl2DVector();
            this.CameraOrientation = new MCTester.Controls.Ctrl3DOrientation();
            this.label21 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlCameraLocation = new MCTester.Controls.Ctrl3DVector();
            this.label8 = new System.Windows.Forms.Label();
            this.ntxCameraOpeningAngleX = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxPixelRatio = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbFrameImageCalcCameraParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFrameImageCalcCameraParams
            // 
            this.gbFrameImageCalcCameraParams.Controls.Add(this.ctrl2DFVectorOffsetCenterPixel);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.ctrl2DVectorNumPixeles);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.CameraOrientation);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.label21);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.label13);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.label1);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.ctrlCameraLocation);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.label8);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.ntxCameraOpeningAngleX);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.label3);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.ntxPixelRatio);
            this.gbFrameImageCalcCameraParams.Controls.Add(this.label4);
            this.gbFrameImageCalcCameraParams.Location = new System.Drawing.Point(3, -4);
            this.gbFrameImageCalcCameraParams.Name = "gbFrameImageCalcCameraParams";
            this.gbFrameImageCalcCameraParams.Size = new System.Drawing.Size(359, 221);
            this.gbFrameImageCalcCameraParams.TabIndex = 133;
            this.gbFrameImageCalcCameraParams.TabStop = false;
            // 
            // ctrl2DFVectorOffsetCenterPixel
            // 
            this.ctrl2DFVectorOffsetCenterPixel.Location = new System.Drawing.Point(109, 12);
            this.ctrl2DFVectorOffsetCenterPixel.Name = "ctrl2DFVectorOffsetCenterPixel";
            this.ctrl2DFVectorOffsetCenterPixel.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DFVectorOffsetCenterPixel.TabIndex = 124;
            this.ctrl2DFVectorOffsetCenterPixel.X = 0D;
            this.ctrl2DFVectorOffsetCenterPixel.Y = 0D;
            // 
            // ctrl2DVectorNumPixeles
            // 
            this.ctrl2DVectorNumPixeles.Location = new System.Drawing.Point(109, 40);
            this.ctrl2DVectorNumPixeles.Name = "ctrl2DVectorNumPixeles";
            this.ctrl2DVectorNumPixeles.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DVectorNumPixeles.TabIndex = 122;
            this.ctrl2DVectorNumPixeles.X = 0D;
            this.ctrl2DVectorNumPixeles.Y = 0D;
            // 
            // CameraOrientation
            // 
            this.CameraOrientation.Location = new System.Drawing.Point(63, 145);
            this.CameraOrientation.Margin = new System.Windows.Forms.Padding(4);
            this.CameraOrientation.Name = "CameraOrientation";
            this.CameraOrientation.Pitch = 0F;
            this.CameraOrientation.Roll = 0F;
            this.CameraOrientation.Size = new System.Drawing.Size(289, 22);
            this.CameraOrientation.TabIndex = 121;
            this.CameraOrientation.Yaw = 0F;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 126);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(100, 13);
            this.label21.TabIndex = 120;
            this.label21.Text = "Camera Orientation:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 18);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 13);
            this.label13.TabIndex = 117;
            this.label13.Text = "Offset Center Pixel:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 101;
            this.label1.Text = "Num Pixeles:";
            // 
            // ctrlCameraLocation
            // 
            this.ctrlCameraLocation.IsReadOnly = false;
            this.ctrlCameraLocation.Location = new System.Drawing.Point(78, 192);
            this.ctrlCameraLocation.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlCameraLocation.Name = "ctrlCameraLocation";
            this.ctrlCameraLocation.Size = new System.Drawing.Size(232, 26);
            this.ctrlCameraLocation.TabIndex = 115;
            this.ctrlCameraLocation.X = 0D;
            this.ctrlCameraLocation.Y = 0D;
            this.ctrlCameraLocation.Z = 0D;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 114;
            this.label8.Text = "Camera Position:";
            // 
            // ntxCameraOpeningAngleX
            // 
            this.ntxCameraOpeningAngleX.Location = new System.Drawing.Point(200, 71);
            this.ntxCameraOpeningAngleX.Name = "ntxCameraOpeningAngleX";
            this.ntxCameraOpeningAngleX.Size = new System.Drawing.Size(58, 20);
            this.ntxCameraOpeningAngleX.TabIndex = 104;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 13);
            this.label3.TabIndex = 105;
            this.label3.Text = "Camera Opening Angle X:";
            // 
            // ntxPixelRatio
            // 
            this.ntxPixelRatio.Location = new System.Drawing.Point(200, 100);
            this.ntxPixelRatio.Name = "ntxPixelRatio";
            this.ntxPixelRatio.Size = new System.Drawing.Size(58, 20);
            this.ntxPixelRatio.TabIndex = 106;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 107;
            this.label4.Text = "Pixel Ratio:";
            // 
            // CtrlCameraParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFrameImageCalcCameraParams);
            this.Name = "CtrlCameraParams";
            this.Size = new System.Drawing.Size(365, 221);
            this.gbFrameImageCalcCameraParams.ResumeLayout(false);
            this.gbFrameImageCalcCameraParams.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbFrameImageCalcCameraParams;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.Ctrl3DVector ctrlCameraLocation;
        private System.Windows.Forms.Label label8;
        private MCTester.Controls.NumericTextBox ntxCameraOpeningAngleX;
        private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox ntxPixelRatio;
        private System.Windows.Forms.Label label4;
        private MCTester.Controls.Ctrl2DVector ctrl2DVectorNumPixeles;
        private MCTester.Controls.Ctrl3DOrientation CameraOrientation;
        private System.Windows.Forms.Label label21;
        private MCTester.Controls.Ctrl2DVector ctrl2DFVectorOffsetCenterPixel;
    }
}
