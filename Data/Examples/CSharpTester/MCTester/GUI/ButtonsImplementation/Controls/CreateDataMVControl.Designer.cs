namespace MCTester.Controls
{
    partial class CreateDataMVControl
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
            this.cmbMapType = new System.Windows.Forms.ComboBox();
            this.gbDataMVGeneral = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.ntxTerrainResolutionFactor = new MCTester.Controls.NumericTextBox();
            this.chxMVEnableGLQuadBufferStereo = new System.Windows.Forms.CheckBox();
            this.chxTerrainObjectsCache = new System.Windows.Forms.CheckBox();
            this.ntxTerrainObjectBestResolution = new MCTester.Controls.NumericTextBox();
            this.chxIsWpfMapWin = new System.Windows.Forms.CheckBox();
            this.cmbDtmUsageAndPrecision = new System.Windows.Forms.ComboBox();
            this.chxShowGeoInMetricProportion = new System.Windows.Forms.CheckBox();
            this.lblMapType = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.ctrlImageCalcExistingList = new MCTester.Controls.CtrlImageCalc();
            this.label12 = new System.Windows.Forms.Label();
            this.ctrlSlaveImageCalcExistingList = new MCTester.Controls.CtrlImageCalc();
            this.label15 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.gbDataMVGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbMapType
            // 
            this.cmbMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapType.FormattingEnabled = true;
            this.cmbMapType.Location = new System.Drawing.Point(70, 10);
            this.cmbMapType.Name = "cmbMapType";
            this.cmbMapType.Size = new System.Drawing.Size(203, 21);
            this.cmbMapType.TabIndex = 5;
            // 
            // gbDataMVGeneral
            // 
            this.gbDataMVGeneral.Controls.Add(this.label26);
            this.gbDataMVGeneral.Controls.Add(this.label25);
            this.gbDataMVGeneral.Controls.Add(this.label24);
            this.gbDataMVGeneral.Controls.Add(this.ntxTerrainResolutionFactor);
            this.gbDataMVGeneral.Controls.Add(this.chxMVEnableGLQuadBufferStereo);
            this.gbDataMVGeneral.Controls.Add(this.chxTerrainObjectsCache);
            this.gbDataMVGeneral.Controls.Add(this.ntxTerrainObjectBestResolution);
            this.gbDataMVGeneral.Controls.Add(this.chxIsWpfMapWin);
            this.gbDataMVGeneral.Controls.Add(this.cmbDtmUsageAndPrecision);
            this.gbDataMVGeneral.Controls.Add(this.chxShowGeoInMetricProportion);
            this.gbDataMVGeneral.Controls.Add(this.lblMapType);
            this.gbDataMVGeneral.Controls.Add(this.cmbMapType);
            this.gbDataMVGeneral.Location = new System.Drawing.Point(1, 257);
            this.gbDataMVGeneral.Name = "gbDataMVGeneral";
            this.gbDataMVGeneral.Size = new System.Drawing.Size(363, 169);
            this.gbDataMVGeneral.TabIndex = 88;
            this.gbDataMVGeneral.TabStop = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 139);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(129, 13);
            this.label26.TabIndex = 115;
            this.label26.Text = "Terrain Resolution Factor:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 116);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(154, 13);
            this.label25.TabIndex = 114;
            this.label25.Text = "Terrain Object Best Resolution:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 40);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(136, 13);
            this.label24.TabIndex = 113;
            this.label24.Text = "DTM Usage And Precision:";
            // 
            // ntxTerrainResolutionFactor
            // 
            this.ntxTerrainResolutionFactor.Location = new System.Drawing.Point(164, 136);
            this.ntxTerrainResolutionFactor.Name = "ntxTerrainResolutionFactor";
            this.ntxTerrainResolutionFactor.Size = new System.Drawing.Size(54, 20);
            this.ntxTerrainResolutionFactor.TabIndex = 99;
            this.ntxTerrainResolutionFactor.Text = "1";
            // 
            // chxMVEnableGLQuadBufferStereo
            // 
            this.chxMVEnableGLQuadBufferStereo.AutoSize = true;
            this.chxMVEnableGLQuadBufferStereo.Location = new System.Drawing.Point(185, 87);
            this.chxMVEnableGLQuadBufferStereo.Name = "chxMVEnableGLQuadBufferStereo";
            this.chxMVEnableGLQuadBufferStereo.Size = new System.Drawing.Size(170, 17);
            this.chxMVEnableGLQuadBufferStereo.TabIndex = 98;
            this.chxMVEnableGLQuadBufferStereo.Text = "Enable GL Quad Buffer Stereo";
            this.chxMVEnableGLQuadBufferStereo.UseVisualStyleBackColor = true;
            // 
            // chxTerrainObjectsCache
            // 
            this.chxTerrainObjectsCache.AutoSize = true;
            this.chxTerrainObjectsCache.Checked = true;
            this.chxTerrainObjectsCache.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTerrainObjectsCache.Location = new System.Drawing.Point(185, 64);
            this.chxTerrainObjectsCache.Name = "chxTerrainObjectsCache";
            this.chxTerrainObjectsCache.Size = new System.Drawing.Size(132, 17);
            this.chxTerrainObjectsCache.TabIndex = 97;
            this.chxTerrainObjectsCache.Text = "Terrain Objects Cache";
            this.chxTerrainObjectsCache.UseVisualStyleBackColor = true;
            // 
            // ntxTerrainObjectBestResolution
            // 
            this.ntxTerrainObjectBestResolution.Location = new System.Drawing.Point(164, 113);
            this.ntxTerrainObjectBestResolution.Name = "ntxTerrainObjectBestResolution";
            this.ntxTerrainObjectBestResolution.Size = new System.Drawing.Size(54, 20);
            this.ntxTerrainObjectBestResolution.TabIndex = 94;
            this.ntxTerrainObjectBestResolution.Text = "0.125";
            // 
            // chxIsWpfMapWin
            // 
            this.chxIsWpfMapWin.AutoSize = true;
            this.chxIsWpfMapWin.Location = new System.Drawing.Point(7, 87);
            this.chxIsWpfMapWin.Name = "chxIsWpfMapWin";
            this.chxIsWpfMapWin.Size = new System.Drawing.Size(136, 17);
            this.chxIsWpfMapWin.TabIndex = 92;
            this.chxIsWpfMapWin.Text = "Open as Wpf Windows";
            this.chxIsWpfMapWin.UseVisualStyleBackColor = true;
            // 
            // cmbDtmUsageAndPrecision
            // 
            this.cmbDtmUsageAndPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDtmUsageAndPrecision.FormattingEnabled = true;
            this.cmbDtmUsageAndPrecision.Location = new System.Drawing.Point(141, 37);
            this.cmbDtmUsageAndPrecision.Name = "cmbDtmUsageAndPrecision";
            this.cmbDtmUsageAndPrecision.Size = new System.Drawing.Size(187, 21);
            this.cmbDtmUsageAndPrecision.TabIndex = 92;
            // 
            // chxShowGeoInMetricProportion
            // 
            this.chxShowGeoInMetricProportion.AutoSize = true;
            this.chxShowGeoInMetricProportion.Checked = true;
            this.chxShowGeoInMetricProportion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxShowGeoInMetricProportion.Location = new System.Drawing.Point(7, 64);
            this.chxShowGeoInMetricProportion.Name = "chxShowGeoInMetricProportion";
            this.chxShowGeoInMetricProportion.Size = new System.Drawing.Size(171, 17);
            this.chxShowGeoInMetricProportion.TabIndex = 91;
            this.chxShowGeoInMetricProportion.Text = "Show Geo In Metric Proportion";
            this.chxShowGeoInMetricProportion.UseVisualStyleBackColor = true;
            // 
            // lblMapType
            // 
            this.lblMapType.AutoSize = true;
            this.lblMapType.Location = new System.Drawing.Point(6, 13);
            this.lblMapType.Name = "lblMapType";
            this.lblMapType.Size = new System.Drawing.Size(55, 13);
            this.lblMapType.TabIndex = 90;
            this.lblMapType.Text = "MapType:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 43);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 13);
            this.label14.TabIndex = 96;
            this.label14.Text = "Dtm Usage And Precision:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 119);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(154, 13);
            this.label13.TabIndex = 95;
            this.label13.Text = "Terrain Object Best Resolution:";
            // 
            // ctrlImageCalcExistingList
            // 
            this.ctrlImageCalcExistingList.EnableNewCoordSysCreation = true;
            this.ctrlImageCalcExistingList.ImageCalc = null;
            this.ctrlImageCalcExistingList.Location = new System.Drawing.Point(0, 433);
            this.ctrlImageCalcExistingList.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlImageCalcExistingList.Name = "ctrlImageCalcExistingList";
            this.ctrlImageCalcExistingList.Size = new System.Drawing.Size(364, 122);
            this.ctrlImageCalcExistingList.TabIndex = 89;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 13);
            this.label12.TabIndex = 96;
            this.label12.Text = "Terrain Object Best Resolution:";
            // 
            // ctrlSlaveImageCalcExistingList
            // 
            this.ctrlSlaveImageCalcExistingList.EnableNewCoordSysCreation = true;
            this.ctrlSlaveImageCalcExistingList.ImageCalc = null;
            this.ctrlSlaveImageCalcExistingList.Location = new System.Drawing.Point(1, 574);
            this.ctrlSlaveImageCalcExistingList.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSlaveImageCalcExistingList.Name = "ctrlSlaveImageCalcExistingList";
            this.ctrlSlaveImageCalcExistingList.Size = new System.Drawing.Size(363, 122);
            this.ctrlSlaveImageCalcExistingList.TabIndex = 90;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label15.Location = new System.Drawing.Point(7, 558);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(139, 13);
            this.label15.TabIndex = 97;
            this.label15.Text = "Slave Image calc for stereo:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(297, 146);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(96, 17);
            this.label23.TabIndex = 100;
            this.label23.Text = "Ter. Precision";
            // 
            // CreateDataMVControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label15);
            this.Controls.Add(this.ctrlSlaveImageCalcExistingList);
            this.Controls.Add(this.ctrlImageCalcExistingList);
            this.Controls.Add(this.gbDataMVGeneral);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CreateDataMVControl";
            this.Size = new System.Drawing.Size(368, 700);
            this.Controls.SetChildIndex(this.gbDataMVGeneral, 0);
            this.Controls.SetChildIndex(this.ctrlImageCalcExistingList, 0);
            this.Controls.SetChildIndex(this.ctrlSlaveImageCalcExistingList, 0);
            this.Controls.SetChildIndex(this.label15, 0);
            this.gbDataMVGeneral.ResumeLayout(false);
            this.gbDataMVGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMapType;
        private System.Windows.Forms.GroupBox gbDataMVGeneral;
        private System.Windows.Forms.Label lblMapType;
        private CtrlImageCalc ctrlImageCalcExistingList;
        private System.Windows.Forms.CheckBox chxShowGeoInMetricProportion;
        private System.Windows.Forms.ComboBox cmbDtmUsageAndPrecision;
		private System.Windows.Forms.CheckBox chxIsWpfMapWin;
        private NumericTextBox ntxTerrainObjectBestResolution;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chxTerrainObjectsCache;
        private CtrlImageCalc ctrlSlaveImageCalcExistingList;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chxMVEnableGLQuadBufferStereo;
        private System.Windows.Forms.Label label23;
        private NumericTextBox ntxTerrainResolutionFactor;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
    }
}
