namespace MCTester.GUI.Forms
{
    partial class btnScanForm
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
            this.btnScanTypeOK = new System.Windows.Forms.Button();
            this.gbScanType = new System.Windows.Forms.GroupBox();
            this.rbManualPointScan = new System.Windows.Forms.RadioButton();
            this.rbPointScan = new System.Windows.Forms.RadioButton();
            this.rbRectScan = new System.Windows.Forms.RadioButton();
            this.rbPolyScan = new System.Windows.Forms.RadioButton();
            this.btnScanSQParams = new System.Windows.Forms.Button();
            this.lblSQParams = new System.Windows.Forms.Label();
            this.chxCompletelyInsideOnly = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCoordSys = new System.Windows.Forms.ComboBox();
            this.btnRestoreDefualtValues = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chxGetScanExtendedDataAsync = new System.Windows.Forms.CheckBox();
            this.ntxTolerance = new MCTester.Controls.NumericTextBox();
            this.ctrl3DVectorManualPointScan = new MCTester.Controls.Ctrl3DVector();
            this.gbScanType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnScanTypeOK
            // 
            this.btnScanTypeOK.Location = new System.Drawing.Point(208, 279);
            this.btnScanTypeOK.Name = "btnScanTypeOK";
            this.btnScanTypeOK.Size = new System.Drawing.Size(75, 23);
            this.btnScanTypeOK.TabIndex = 2;
            this.btnScanTypeOK.Text = "OK";
            this.btnScanTypeOK.UseVisualStyleBackColor = true;
            this.btnScanTypeOK.Click += new System.EventHandler(this.btnScanTypeOK_Click);
            // 
            // gbScanType
            // 
            this.gbScanType.Controls.Add(this.rbManualPointScan);
            this.gbScanType.Controls.Add(this.ctrl3DVectorManualPointScan);
            this.gbScanType.Controls.Add(this.rbPointScan);
            this.gbScanType.Controls.Add(this.rbRectScan);
            this.gbScanType.Controls.Add(this.rbPolyScan);
            this.gbScanType.Location = new System.Drawing.Point(2, 1);
            this.gbScanType.Name = "gbScanType";
            this.gbScanType.Size = new System.Drawing.Size(281, 114);
            this.gbScanType.TabIndex = 3;
            this.gbScanType.TabStop = false;
            this.gbScanType.Text = "Scan Type";
            // 
            // rbManualPointScan
            // 
            this.rbManualPointScan.AutoSize = true;
            this.rbManualPointScan.Location = new System.Drawing.Point(6, 44);
            this.rbManualPointScan.Name = "rbManualPointScan";
            this.rbManualPointScan.Size = new System.Drawing.Size(14, 13);
            this.rbManualPointScan.TabIndex = 4;
            this.rbManualPointScan.UseVisualStyleBackColor = true;
            // 
            // rbPointScan
            // 
            this.rbPointScan.AutoSize = true;
            this.rbPointScan.Checked = true;
            this.rbPointScan.Location = new System.Drawing.Point(6, 19);
            this.rbPointScan.Name = "rbPointScan";
            this.rbPointScan.Size = new System.Drawing.Size(74, 17);
            this.rbPointScan.TabIndex = 2;
            this.rbPointScan.TabStop = true;
            this.rbPointScan.Text = "PointScan";
            this.rbPointScan.UseVisualStyleBackColor = true;
            // 
            // rbRectScan
            // 
            this.rbRectScan.AutoSize = true;
            this.rbRectScan.Location = new System.Drawing.Point(6, 64);
            this.rbRectScan.Name = "rbRectScan";
            this.rbRectScan.Size = new System.Drawing.Size(73, 17);
            this.rbRectScan.TabIndex = 1;
            this.rbRectScan.Text = "RectScan";
            this.rbRectScan.UseVisualStyleBackColor = true;
            // 
            // rbPolyScan
            // 
            this.rbPolyScan.AutoSize = true;
            this.rbPolyScan.Location = new System.Drawing.Point(6, 86);
            this.rbPolyScan.Name = "rbPolyScan";
            this.rbPolyScan.Size = new System.Drawing.Size(70, 17);
            this.rbPolyScan.TabIndex = 0;
            this.rbPolyScan.Text = "PolyScan";
            this.rbPolyScan.UseVisualStyleBackColor = true;
            // 
            // btnScanSQParams
            // 
            this.btnScanSQParams.Location = new System.Drawing.Point(6, 156);
            this.btnScanSQParams.Name = "btnScanSQParams";
            this.btnScanSQParams.Size = new System.Drawing.Size(87, 23);
            this.btnScanSQParams.TabIndex = 4;
            this.btnScanSQParams.Text = "SQParams";
            this.btnScanSQParams.UseVisualStyleBackColor = true;
            this.btnScanSQParams.Click += new System.EventHandler(this.btnScanSQParams_Click);
            // 
            // lblSQParams
            // 
            this.lblSQParams.AutoSize = true;
            this.lblSQParams.Location = new System.Drawing.Point(229, 161);
            this.lblSQParams.Name = "lblSQParams";
            this.lblSQParams.Size = new System.Drawing.Size(56, 13);
            this.lblSQParams.TabIndex = 5;
            this.lblSQParams.Text = "Undefined";
            // 
            // chxCompletelyInsideOnly
            // 
            this.chxCompletelyInsideOnly.AutoSize = true;
            this.chxCompletelyInsideOnly.Location = new System.Drawing.Point(8, 185);
            this.chxCompletelyInsideOnly.Name = "chxCompletelyInsideOnly";
            this.chxCompletelyInsideOnly.Size = new System.Drawing.Size(167, 17);
            this.chxCompletelyInsideOnly.TabIndex = 6;
            this.chxCompletelyInsideOnly.Text = "Return Completely Inside Only";
            this.chxCompletelyInsideOnly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "PointScan Tolerance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Coordinate System:";
            // 
            // cmbCoordSys
            // 
            this.cmbCoordSys.FormattingEnabled = true;
            this.cmbCoordSys.Location = new System.Drawing.Point(109, 124);
            this.cmbCoordSys.Name = "cmbCoordSys";
            this.cmbCoordSys.Size = new System.Drawing.Size(174, 21);
            this.cmbCoordSys.TabIndex = 10;
            // 
            // btnRestoreDefualtValues
            // 
            this.btnRestoreDefualtValues.Location = new System.Drawing.Point(8, 231);
            this.btnRestoreDefualtValues.Name = "btnRestoreDefualtValues";
            this.btnRestoreDefualtValues.Size = new System.Drawing.Size(122, 23);
            this.btnRestoreDefualtValues.TabIndex = 11;
            this.btnRestoreDefualtValues.Text = "Restore Defualt Values";
            this.btnRestoreDefualtValues.UseVisualStyleBackColor = true;
            this.btnRestoreDefualtValues.Click += new System.EventHandler(this.btnRestoreDefualtValues_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chxGetScanExtendedDataAsync);
            this.groupBox1.Location = new System.Drawing.Point(6, 259);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(178, 42);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "For Get Scan Extended Data";
            // 
            // chxGetScanExtendedDataAsync
            // 
            this.chxGetScanExtendedDataAsync.AutoSize = true;
            this.chxGetScanExtendedDataAsync.Location = new System.Drawing.Point(5, 18);
            this.chxGetScanExtendedDataAsync.Name = "chxGetScanExtendedDataAsync";
            this.chxGetScanExtendedDataAsync.Size = new System.Drawing.Size(55, 17);
            this.chxGetScanExtendedDataAsync.TabIndex = 7;
            this.chxGetScanExtendedDataAsync.Text = "Async";
            this.chxGetScanExtendedDataAsync.UseVisualStyleBackColor = true;
            // 
            // ntxTolerance
            // 
            this.ntxTolerance.Location = new System.Drawing.Point(121, 206);
            this.ntxTolerance.Name = "ntxTolerance";
            this.ntxTolerance.Size = new System.Drawing.Size(84, 20);
            this.ntxTolerance.TabIndex = 7;
            this.ntxTolerance.Text = "0";
            // 
            // ctrl3DVectorManualPointScan
            // 
            this.ctrl3DVectorManualPointScan.IsReadOnly = false;
            this.ctrl3DVectorManualPointScan.Location = new System.Drawing.Point(21, 38);
            this.ctrl3DVectorManualPointScan.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorManualPointScan.Name = "ctrl3DVectorManualPointScan";
            this.ctrl3DVectorManualPointScan.Size = new System.Drawing.Size(232, 25);
            this.ctrl3DVectorManualPointScan.TabIndex = 3;
            this.ctrl3DVectorManualPointScan.X = 0D;
            this.ctrl3DVectorManualPointScan.Y = 0D;
            this.ctrl3DVectorManualPointScan.Z = 0D;
            // 
            // btnScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(288, 311);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRestoreDefualtValues);
            this.Controls.Add(this.cmbCoordSys);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ntxTolerance);
            this.Controls.Add(this.lblSQParams);
            this.Controls.Add(this.chxCompletelyInsideOnly);
            this.Controls.Add(this.btnScanSQParams);
            this.Controls.Add(this.gbScanType);
            this.Controls.Add(this.btnScanTypeOK);
            this.Name = "btnScanForm";
            this.Text = "ScanGeometryForm";
            this.gbScanType.ResumeLayout(false);
            this.gbScanType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScanTypeOK;
        private System.Windows.Forms.GroupBox gbScanType;
        private System.Windows.Forms.RadioButton rbPointScan;
        private System.Windows.Forms.RadioButton rbRectScan;
        private System.Windows.Forms.RadioButton rbPolyScan;
        private System.Windows.Forms.Button btnScanSQParams;
        private System.Windows.Forms.Label lblSQParams;
        private System.Windows.Forms.CheckBox chxCompletelyInsideOnly;
        private MCTester.Controls.NumericTextBox ntxTolerance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCoordSys;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorManualPointScan;
        private System.Windows.Forms.RadioButton rbManualPointScan;
        private System.Windows.Forms.Button btnRestoreDefualtValues;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chxGetScanExtendedDataAsync;
    }
}