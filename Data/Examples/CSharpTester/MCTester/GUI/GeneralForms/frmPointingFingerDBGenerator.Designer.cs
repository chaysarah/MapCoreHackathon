namespace MCTester.General_Forms
{
    partial class frmPointingFingerDBGenerator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPointingFingerDBGenerator));
            this.label1 = new System.Windows.Forms.Label();
            this.gbDistance = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxDisGap = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxDisMin = new MCTester.Controls.NumericTextBox();
            this.ntxDisMax = new MCTester.Controls.NumericTextBox();
            this.gbRotationAngle = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ntxRotationAngleDelta = new MCTester.Controls.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ntxRotationAngleMin = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ntxRotationAngleMax = new MCTester.Controls.NumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRunPFGenerator = new System.Windows.Forms.Button();
            this.ctrl3DVectorLookAtPt = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSampleLookAtPoint = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlLoadTestModel = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlGenerateOutputToDir = new MCTester.Controls.CtrlBrowseControl();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.gbDistance.SuspendLayout();
            this.gbRotationAngle.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Max:";
            // 
            // gbDistance
            // 
            this.gbDistance.Controls.Add(this.label3);
            this.gbDistance.Controls.Add(this.ntxDisGap);
            this.gbDistance.Controls.Add(this.label2);
            this.gbDistance.Controls.Add(this.ntxDisMin);
            this.gbDistance.Controls.Add(this.label1);
            this.gbDistance.Controls.Add(this.ntxDisMax);
            this.gbDistance.Location = new System.Drawing.Point(12, 12);
            this.gbDistance.Name = "gbDistance";
            this.gbDistance.Size = new System.Drawing.Size(535, 100);
            this.gbDistance.TabIndex = 2;
            this.gbDistance.TabStop = false;
            this.gbDistance.Text = "Distance";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Gap:";
            // 
            // ntxDisGap
            // 
            this.ntxDisGap.Location = new System.Drawing.Point(89, 71);
            this.ntxDisGap.Name = "ntxDisGap";
            this.ntxDisGap.Size = new System.Drawing.Size(61, 20);
            this.ntxDisGap.TabIndex = 2;
            this.ntxDisGap.Text = "500";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Min:";
            // 
            // ntxDisMin
            // 
            this.ntxDisMin.Location = new System.Drawing.Point(89, 19);
            this.ntxDisMin.Name = "ntxDisMin";
            this.ntxDisMin.Size = new System.Drawing.Size(61, 20);
            this.ntxDisMin.TabIndex = 0;
            this.ntxDisMin.Text = "500";
            // 
            // ntxDisMax
            // 
            this.ntxDisMax.Location = new System.Drawing.Point(89, 45);
            this.ntxDisMax.Name = "ntxDisMax";
            this.ntxDisMax.Size = new System.Drawing.Size(61, 20);
            this.ntxDisMax.TabIndex = 1;
            this.ntxDisMax.Text = "3000";
            // 
            // gbRotationAngle
            // 
            this.gbRotationAngle.Controls.Add(this.label4);
            this.gbRotationAngle.Controls.Add(this.ntxRotationAngleDelta);
            this.gbRotationAngle.Controls.Add(this.label5);
            this.gbRotationAngle.Controls.Add(this.ntxRotationAngleMin);
            this.gbRotationAngle.Controls.Add(this.label6);
            this.gbRotationAngle.Controls.Add(this.ntxRotationAngleMax);
            this.gbRotationAngle.Location = new System.Drawing.Point(12, 118);
            this.gbRotationAngle.Name = "gbRotationAngle";
            this.gbRotationAngle.Size = new System.Drawing.Size(535, 100);
            this.gbRotationAngle.TabIndex = 3;
            this.gbRotationAngle.TabStop = false;
            this.gbRotationAngle.Text = "Rotation Angle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Delta:";
            // 
            // ntxRotationAngleDelta
            // 
            this.ntxRotationAngleDelta.Location = new System.Drawing.Point(89, 71);
            this.ntxRotationAngleDelta.Name = "ntxRotationAngleDelta";
            this.ntxRotationAngleDelta.Size = new System.Drawing.Size(61, 20);
            this.ntxRotationAngleDelta.TabIndex = 5;
            this.ntxRotationAngleDelta.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Min:";
            // 
            // ntxRotationAngleMin
            // 
            this.ntxRotationAngleMin.Location = new System.Drawing.Point(89, 19);
            this.ntxRotationAngleMin.Name = "ntxRotationAngleMin";
            this.ntxRotationAngleMin.Size = new System.Drawing.Size(61, 20);
            this.ntxRotationAngleMin.TabIndex = 3;
            this.ntxRotationAngleMin.Text = "-80";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Max:";
            // 
            // ntxRotationAngleMax
            // 
            this.ntxRotationAngleMax.Location = new System.Drawing.Point(89, 45);
            this.ntxRotationAngleMax.Name = "ntxRotationAngleMax";
            this.ntxRotationAngleMax.Size = new System.Drawing.Size(61, 20);
            this.ntxRotationAngleMax.TabIndex = 4;
            this.ntxRotationAngleMax.Text = "80";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 289);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Pivot Point:";
            // 
            // btnRunPFGenerator
            // 
            this.btnRunPFGenerator.Location = new System.Drawing.Point(472, 321);
            this.btnRunPFGenerator.Name = "btnRunPFGenerator";
            this.btnRunPFGenerator.Size = new System.Drawing.Size(75, 23);
            this.btnRunPFGenerator.TabIndex = 9;
            this.btnRunPFGenerator.Text = "GO!";
            this.btnRunPFGenerator.UseVisualStyleBackColor = true;
            this.btnRunPFGenerator.Click += new System.EventHandler(this.btnRunPFGenerator_Click);
            // 
            // ctrl3DVectorLookAtPt
            // 
            this.ctrl3DVectorLookAtPt.IsReadOnly = false;
            this.ctrl3DVectorLookAtPt.Location = new System.Drawing.Point(78, 284);
            this.ctrl3DVectorLookAtPt.Name = "ctrl3DVectorLookAtPt";
            this.ctrl3DVectorLookAtPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorLookAtPt.TabIndex = 8;
            this.ctrl3DVectorLookAtPt.X = 0D;
            this.ctrl3DVectorLookAtPt.Y = 0D;
            this.ctrl3DVectorLookAtPt.Z = 0D;
            // 
            // ctrlSampleLookAtPoint
            // 
            this.ctrlSampleLookAtPoint._DgvControlName = null;
            this.ctrlSampleLookAtPoint._IsRelativeToDTM = false;
            this.ctrlSampleLookAtPoint._PointInOverlayManagerCoordSys = true;
            this.ctrlSampleLookAtPoint._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSampleLookAtPoint._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSampleLookAtPoint._SampleOnePoint = true;
            this.ctrlSampleLookAtPoint._UserControlName = "ctrl3DVectorLookAtPt";
            this.ctrlSampleLookAtPoint.IsAsync = false;
            this.ctrlSampleLookAtPoint.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSampleLookAtPoint.Location = new System.Drawing.Point(316, 287);
            this.ctrlSampleLookAtPoint.Name = "ctrlSampleLookAtPoint";
            this.ctrlSampleLookAtPoint.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSampleLookAtPoint.Size = new System.Drawing.Size(33, 23);
            this.ctrlSampleLookAtPoint.TabIndex = 6;
            this.ctrlSampleLookAtPoint.Text = "...";
            this.ctrlSampleLookAtPoint.UseVisualStyleBackColor = true;
            // 
            // ctrlLoadTestModel
            // 
            this.ctrlLoadTestModel.AutoSize = true;
            this.ctrlLoadTestModel.FileName = "";
            this.ctrlLoadTestModel.Filter = "";
            this.ctrlLoadTestModel.IsFolderDialog = false;
            this.ctrlLoadTestModel.IsFullPath = true;
            this.ctrlLoadTestModel.IsSaveFile = false;
            this.ctrlLoadTestModel.LabelCaption = "Load Test Object:";
            this.ctrlLoadTestModel.Location = new System.Drawing.Point(124, 254);
            this.ctrlLoadTestModel.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlLoadTestModel.MultiFilesSelect = false;
            this.ctrlLoadTestModel.Name = "ctrlLoadTestModel";
            this.ctrlLoadTestModel.Prefix = "";
            this.ctrlLoadTestModel.Size = new System.Drawing.Size(425, 24);
            this.ctrlLoadTestModel.TabIndex = 7;
            // 
            // ctrlGenerateOutputToDir
            // 
            this.ctrlGenerateOutputToDir.AutoSize = true;
            this.ctrlGenerateOutputToDir.FileName = "";
            this.ctrlGenerateOutputToDir.Filter = "";
            this.ctrlGenerateOutputToDir.IsFolderDialog = true;
            this.ctrlGenerateOutputToDir.IsFullPath = true;
            this.ctrlGenerateOutputToDir.IsSaveFile = true;
            this.ctrlGenerateOutputToDir.LabelCaption = "Generate Output To:";
            this.ctrlGenerateOutputToDir.Location = new System.Drawing.Point(124, 224);
            this.ctrlGenerateOutputToDir.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlGenerateOutputToDir.MultiFilesSelect = false;
            this.ctrlGenerateOutputToDir.Name = "ctrlGenerateOutputToDir";
            this.ctrlGenerateOutputToDir.Prefix = "";
            this.ctrlGenerateOutputToDir.Size = new System.Drawing.Size(425, 24);
            this.ctrlGenerateOutputToDir.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Generate Output To:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 259);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "Load Test Object:";
            // 
            // frmPointingFingerDBGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(558, 357);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnRunPFGenerator);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ctrl3DVectorLookAtPt);
            this.Controls.Add(this.ctrlSampleLookAtPoint);
            this.Controls.Add(this.ctrlLoadTestModel);
            this.Controls.Add(this.ctrlGenerateOutputToDir);
            this.Controls.Add(this.gbRotationAngle);
            this.Controls.Add(this.gbDistance);
            this.Name = "frmPointingFingerDBGenerator";
            this.Text = "Pointing Finger DB Generator";
            this.gbDistance.ResumeLayout(false);
            this.gbDistance.PerformLayout();
            this.gbRotationAngle.ResumeLayout(false);
            this.gbRotationAngle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Controls.NumericTextBox ntxDisMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbDistance;
        private System.Windows.Forms.Label label3;
        public Controls.NumericTextBox ntxDisGap;
        private System.Windows.Forms.Label label2;
        public Controls.NumericTextBox ntxDisMin;
        private System.Windows.Forms.GroupBox gbRotationAngle;
        private System.Windows.Forms.Label label4;
        public Controls.NumericTextBox ntxRotationAngleDelta;
        private System.Windows.Forms.Label label5;
        public Controls.NumericTextBox ntxRotationAngleMin;
        private System.Windows.Forms.Label label6;
        public Controls.NumericTextBox ntxRotationAngleMax;
        public Controls.CtrlBrowseControl ctrlGenerateOutputToDir;
        private Controls.CtrlBrowseControl ctrlLoadTestModel;
        private Controls.CtrlSamplePoint ctrlSampleLookAtPoint;
        public Controls.Ctrl3DVector ctrl3DVectorLookAtPt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRunPFGenerator;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}