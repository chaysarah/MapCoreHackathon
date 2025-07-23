namespace MCTester.ButtonsImplementation
{
    partial class btnMapProductionToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(btnMapProductionToolForm));
            this.label2 = new System.Windows.Forms.Label();
            this.btnConvertRasterLayer = new System.Windows.Forms.Button();
            this.cmbRasterOverlapMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chxUpdateExisting = new System.Windows.Forms.CheckBox();
            this.btnTransparentColor = new System.Windows.Forms.Button();
            this.gbGeneral = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cmbVersionCompatibility = new System.Windows.Forms.ComboBox();
            this.btnTilingScheme = new System.Windows.Forms.Button();
            this.ntxNumTilesInFileEdge = new MCTester.Controls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chxOneResolutionOnly = new System.Windows.Forms.CheckBox();
            this.ntxReprojectionPrecision = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chxShowClipping = new System.Windows.Forms.CheckBox();
            this.lstProgressMessage = new System.Windows.Forms.ListBox();
            this.chxImageCoordinateSystem = new System.Windows.Forms.CheckBox();
            this.gbConversionOptions = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbTargetHighestResolutionCustom = new System.Windows.Forms.RadioButton();
            this.rdbTargetHighestResolutionSource = new System.Windows.Forms.RadioButton();
            this.ntxTargetHighestResolution = new MCTester.Controls.NumericTextBox();
            this.ctrlGridCoordinateSystemSource = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.ctrlBrowseControlDestinationDir = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlGridCoordinateSystemDest = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.ctrlSMcBoxClipRect = new MCTester.Controls.CtrlSMcBox();
            this.gbGetRasterOrDtmLayersFiles = new System.Windows.Forms.GroupBox();
            this.cbxIsRecUse = new System.Windows.Forms.CheckBox();
            this.ctrlBrowseControlPath = new MCTester.Controls.CtrlBrowseControl();
            this.label8 = new System.Windows.Forms.Label();
            this.ntxResolutionIndex = new MCTester.Controls.NumericTextBox();
            this.btnGetRasterOrDtmLayersFiles = new System.Windows.Forms.Button();
            this.gbRaster = new System.Windows.Forms.GroupBox();
            this.cgbNonStandardCompression = new MCTester.Controls.CheckGroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ntxPngColors = new MCTester.Controls.NumericTextBox();
            this.rdbJpeg = new System.Windows.Forms.RadioButton();
            this.cmbJpegQuality = new System.Windows.Forms.ComboBox();
            this.rdbPNG = new System.Windows.Forms.RadioButton();
            this.rdbNone = new System.Windows.Forms.RadioButton();
            this.rdbColorful = new System.Windows.Forms.RadioButton();
            this.gbColor = new System.Windows.Forms.GroupBox();
            this.chxGrayScale = new System.Windows.Forms.CheckBox();
            this.pbTransparentColor = new System.Windows.Forms.PictureBox();
            this.ntxTransparentColorPrecision = new MCTester.Controls.NumericTextBox();
            this.rdbGrayScale = new System.Windows.Forms.RadioButton();
            this.btnBuldRasterPARFile = new System.Windows.Forms.Button();
            this.chxIsImageSourceType = new System.Windows.Forms.CheckBox();
            this.gbRemoveBorders = new System.Windows.Forms.GroupBox();
            this.ctrlColorArray = new MCTester.Controls.CtrlColorArray();
            this.btnRemoveBorders = new System.Windows.Forms.Button();
            this.cbIsSetAlpha = new System.Windows.Forms.CheckBox();
            this.ntbNearColorDist = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ntbNearColorDistance = new MCTester.Controls.NumericTextBox();
            this.ntbMaxNonBorderColor = new MCTester.Controls.NumericTextBox();
            this.ctrlBrowserRmvBrdrInputFile = new MCTester.Controls.CtrlBrowseControl();
            this.btnConvertDTMLayer = new System.Windows.Forms.Button();
            this.gbDTM = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ntxNumSmoothingLevels = new MCTester.Controls.NumericTextBox();
            this.chxFillNoHeightAreas = new System.Windows.Forms.CheckBox();
            this.btnBuildDTMPARFile = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tpRaster = new System.Windows.Forms.TabPage();
            this.tpDTM = new System.Windows.Forms.TabPage();
            this.tpHeatMap = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntbMinVal = new MCTester.Controls.NumericTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ntbMaxVal = new MCTester.Controls.NumericTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ntbMinValThreshold = new MCTester.Controls.NumericTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ntbMaxValThreshold = new MCTester.Controls.NumericTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.chxGPUBased = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbItemType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ntbTargetLowestResolution = new MCTester.Controls.NumericTextBox();
            this.bCalcAveragePerPoint = new System.Windows.Forms.CheckBox();
            this.btnConvertHeatMap = new System.Windows.Forms.Button();
            this.ntbPointInfluenceRadius = new MCTester.Controls.NumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chxIsGradient = new System.Windows.Forms.CheckBox();
            this.chxIsRadiusInPixels = new System.Windows.Forms.CheckBox();
            this.dgvHeatMapPoints = new System.Windows.Forms.DataGridView();
            this.SelectRect = new System.Windows.Forms.DataGridViewButtonColumn();
            this.PointsDetails = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpGetRasterOrDTMFiles = new System.Windows.Forms.TabPage();
            this.tpRemoveBorders = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.gbGeneral.SuspendLayout();
            this.gbConversionOptions.SuspendLayout();
            this.gbGetRasterOrDtmLayersFiles.SuspendLayout();
            this.gbRaster.SuspendLayout();
            this.cgbNonStandardCompression.SuspendLayout();
            this.gbColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTransparentColor)).BeginInit();
            this.gbRemoveBorders.SuspendLayout();
            this.gbDTM.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tpRaster.SuspendLayout();
            this.tpDTM.SuspendLayout();
            this.tpHeatMap.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeatMapPoints)).BeginInit();
            this.tpGetRasterOrDTMFiles.SuspendLayout();
            this.tpRemoveBorders.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Raster Overlap Mode:";
            // 
            // btnConvertRasterLayer
            // 
            this.btnConvertRasterLayer.Enabled = false;
            this.btnConvertRasterLayer.Location = new System.Drawing.Point(588, 183);
            this.btnConvertRasterLayer.Name = "btnConvertRasterLayer";
            this.btnConvertRasterLayer.Size = new System.Drawing.Size(87, 23);
            this.btnConvertRasterLayer.TabIndex = 4;
            this.btnConvertRasterLayer.Text = "Convert Raster";
            this.btnConvertRasterLayer.UseVisualStyleBackColor = true;
            this.btnConvertRasterLayer.Click += new System.EventHandler(this.btnConvertRasterLayer_Click);
            // 
            // cmbRasterOverlapMode
            // 
            this.cmbRasterOverlapMode.FormattingEnabled = true;
            this.cmbRasterOverlapMode.Location = new System.Drawing.Point(152, 18);
            this.cmbRasterOverlapMode.Name = "cmbRasterOverlapMode";
            this.cmbRasterOverlapMode.Size = new System.Drawing.Size(242, 21);
            this.cmbRasterOverlapMode.TabIndex = 5;
            this.cmbRasterOverlapMode.SelectedIndexChanged += new System.EventHandler(this.cmbRasterOverlapMode_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transparent Color:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Transparent Color Precision:";
            // 
            // chxUpdateExisting
            // 
            this.chxUpdateExisting.AutoSize = true;
            this.chxUpdateExisting.Location = new System.Drawing.Point(9, 82);
            this.chxUpdateExisting.Name = "chxUpdateExisting";
            this.chxUpdateExisting.Size = new System.Drawing.Size(103, 17);
            this.chxUpdateExisting.TabIndex = 14;
            this.chxUpdateExisting.Text = "Update Existing ";
            this.chxUpdateExisting.UseVisualStyleBackColor = true;
            // 
            // btnTransparentColor
            // 
            this.btnTransparentColor.Location = new System.Drawing.Point(152, 45);
            this.btnTransparentColor.Name = "btnTransparentColor";
            this.btnTransparentColor.Size = new System.Drawing.Size(34, 23);
            this.btnTransparentColor.TabIndex = 18;
            this.btnTransparentColor.Text = "...";
            this.btnTransparentColor.UseVisualStyleBackColor = true;
            this.btnTransparentColor.Click += new System.EventHandler(this.btnTransparentColor_Click);
            // 
            // gbGeneral
            // 
            this.gbGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.gbGeneral.Controls.Add(this.label19);
            this.gbGeneral.Controls.Add(this.label18);
            this.gbGeneral.Controls.Add(this.cmbVersionCompatibility);
            this.gbGeneral.Controls.Add(this.btnTilingScheme);
            this.gbGeneral.Controls.Add(this.ntxNumTilesInFileEdge);
            this.gbGeneral.Controls.Add(this.label9);
            this.gbGeneral.Controls.Add(this.chxOneResolutionOnly);
            this.gbGeneral.Controls.Add(this.ntxReprojectionPrecision);
            this.gbGeneral.Controls.Add(this.label6);
            this.gbGeneral.Controls.Add(this.chxShowClipping);
            this.gbGeneral.Controls.Add(this.lstProgressMessage);
            this.gbGeneral.Controls.Add(this.chxImageCoordinateSystem);
            this.gbGeneral.Controls.Add(this.gbConversionOptions);
            this.gbGeneral.Controls.Add(this.ctrlGridCoordinateSystemSource);
            this.gbGeneral.Controls.Add(this.ctrlBrowseControlDestinationDir);
            this.gbGeneral.Controls.Add(this.ctrlGridCoordinateSystemDest);
            this.gbGeneral.Controls.Add(this.ctrlSMcBoxClipRect);
            this.gbGeneral.Location = new System.Drawing.Point(6, 9);
            this.gbGeneral.Name = "gbGeneral";
            this.gbGeneral.Size = new System.Drawing.Size(714, 471);
            this.gbGeneral.TabIndex = 28;
            this.gbGeneral.TabStop = false;
            this.gbGeneral.Text = "General";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(355, 310);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(106, 13);
            this.label18.TabIndex = 72;
            this.label18.Text = "Version Compatibility:";
            // 
            // cmbVersionCompatibility
            // 
            this.cmbVersionCompatibility.FormattingEnabled = true;
            this.cmbVersionCompatibility.Location = new System.Drawing.Point(528, 308);
            this.cmbVersionCompatibility.Name = "cmbVersionCompatibility";
            this.cmbVersionCompatibility.Size = new System.Drawing.Size(157, 21);
            this.cmbVersionCompatibility.TabIndex = 71;
            // 
            // btnTilingScheme
            // 
            this.btnTilingScheme.Location = new System.Drawing.Point(256, 305);
            this.btnTilingScheme.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTilingScheme.Name = "btnTilingScheme";
            this.btnTilingScheme.Size = new System.Drawing.Size(86, 24);
            this.btnTilingScheme.TabIndex = 70;
            this.btnTilingScheme.Text = "Tiling Scheme";
            this.btnTilingScheme.UseVisualStyleBackColor = true;
            this.btnTilingScheme.Click += new System.EventHandler(this.btnTilingScheme_Click);
            // 
            // ntxNumTilesInFileEdge
            // 
            this.ntxNumTilesInFileEdge.Location = new System.Drawing.Point(128, 359);
            this.ntxNumTilesInFileEdge.Name = "ntxNumTilesInFileEdge";
            this.ntxNumTilesInFileEdge.Size = new System.Drawing.Size(100, 20);
            this.ntxNumTilesInFileEdge.TabIndex = 25;
            this.ntxNumTilesInFileEdge.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 362);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Num Tiles In File Edge:";
            // 
            // chxOneResolutionOnly
            // 
            this.chxOneResolutionOnly.AutoSize = true;
            this.chxOneResolutionOnly.Location = new System.Drawing.Point(9, 333);
            this.chxOneResolutionOnly.Name = "chxOneResolutionOnly";
            this.chxOneResolutionOnly.Size = new System.Drawing.Size(123, 17);
            this.chxOneResolutionOnly.TabIndex = 34;
            this.chxOneResolutionOnly.Text = "One Resolution Only";
            this.chxOneResolutionOnly.UseVisualStyleBackColor = true;
            // 
            // ntxReprojectionPrecision
            // 
            this.ntxReprojectionPrecision.Location = new System.Drawing.Point(128, 307);
            this.ntxReprojectionPrecision.Name = "ntxReprojectionPrecision";
            this.ntxReprojectionPrecision.Size = new System.Drawing.Size(54, 20);
            this.ntxReprojectionPrecision.TabIndex = 33;
            this.ntxReprojectionPrecision.Text = "508";
            this.ntxReprojectionPrecision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 310);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Reprojection Precision:";
            // 
            // chxShowClipping
            // 
            this.chxShowClipping.Appearance = System.Windows.Forms.Appearance.Button;
            this.chxShowClipping.Location = new System.Drawing.Point(268, 263);
            this.chxShowClipping.Name = "chxShowClipping";
            this.chxShowClipping.Size = new System.Drawing.Size(70, 23);
            this.chxShowClipping.TabIndex = 31;
            this.chxShowClipping.Text = "Show";
            this.chxShowClipping.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chxShowClipping.UseVisualStyleBackColor = true;
            this.chxShowClipping.CheckedChanged += new System.EventHandler(this.chxShowClipping_CheckedChanged);
            // 
            // lstProgressMessage
            // 
            this.lstProgressMessage.FormattingEnabled = true;
            this.lstProgressMessage.Location = new System.Drawing.Point(6, 415);
            this.lstProgressMessage.Name = "lstProgressMessage";
            this.lstProgressMessage.Size = new System.Drawing.Size(678, 43);
            this.lstProgressMessage.TabIndex = 30;
            // 
            // chxImageCoordinateSystem
            // 
            this.chxImageCoordinateSystem.AutoSize = true;
            this.chxImageCoordinateSystem.Location = new System.Drawing.Point(187, 31);
            this.chxImageCoordinateSystem.Name = "chxImageCoordinateSystem";
            this.chxImageCoordinateSystem.Size = new System.Drawing.Size(146, 17);
            this.chxImageCoordinateSystem.TabIndex = 24;
            this.chxImageCoordinateSystem.Text = "Image Coordinate System";
            this.chxImageCoordinateSystem.UseVisualStyleBackColor = true;
            this.chxImageCoordinateSystem.CheckedChanged += new System.EventHandler(this.chxIsImage_CheckedChanged);
            // 
            // gbConversionOptions
            // 
            this.gbConversionOptions.Controls.Add(this.label1);
            this.gbConversionOptions.Controls.Add(this.rdbTargetHighestResolutionCustom);
            this.gbConversionOptions.Controls.Add(this.rdbTargetHighestResolutionSource);
            this.gbConversionOptions.Controls.Add(this.ntxTargetHighestResolution);
            this.gbConversionOptions.Controls.Add(this.chxUpdateExisting);
            this.gbConversionOptions.Location = new System.Drawing.Point(348, 180);
            this.gbConversionOptions.Name = "gbConversionOptions";
            this.gbConversionOptions.Size = new System.Drawing.Size(336, 117);
            this.gbConversionOptions.TabIndex = 27;
            this.gbConversionOptions.TabStop = false;
            this.gbConversionOptions.Text = "Conversion Options";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Highest Resolution:";
            // 
            // rdbTargetHighestResolutionCustom
            // 
            this.rdbTargetHighestResolutionCustom.AutoSize = true;
            this.rdbTargetHighestResolutionCustom.Location = new System.Drawing.Point(8, 57);
            this.rdbTargetHighestResolutionCustom.Name = "rdbTargetHighestResolutionCustom";
            this.rdbTargetHighestResolutionCustom.Size = new System.Drawing.Size(60, 17);
            this.rdbTargetHighestResolutionCustom.TabIndex = 2;
            this.rdbTargetHighestResolutionCustom.Text = "Custom";
            this.rdbTargetHighestResolutionCustom.UseVisualStyleBackColor = true;
            // 
            // rdbTargetHighestResolutionSource
            // 
            this.rdbTargetHighestResolutionSource.AutoSize = true;
            this.rdbTargetHighestResolutionSource.Checked = true;
            this.rdbTargetHighestResolutionSource.Location = new System.Drawing.Point(9, 34);
            this.rdbTargetHighestResolutionSource.Name = "rdbTargetHighestResolutionSource";
            this.rdbTargetHighestResolutionSource.Size = new System.Drawing.Size(59, 17);
            this.rdbTargetHighestResolutionSource.TabIndex = 0;
            this.rdbTargetHighestResolutionSource.TabStop = true;
            this.rdbTargetHighestResolutionSource.Text = "Source";
            this.rdbTargetHighestResolutionSource.UseVisualStyleBackColor = true;
            // 
            // ntxTargetHighestResolution
            // 
            this.ntxTargetHighestResolution.Location = new System.Drawing.Point(74, 56);
            this.ntxTargetHighestResolution.Name = "ntxTargetHighestResolution";
            this.ntxTargetHighestResolution.Size = new System.Drawing.Size(54, 20);
            this.ntxTargetHighestResolution.TabIndex = 22;
            // 
            // ctrlGridCoordinateSystemSource
            // 
            this.ctrlGridCoordinateSystemSource.EnableNewCoordSysCreation = true;
            this.ctrlGridCoordinateSystemSource.GridCoordinateSystem = null;
            this.ctrlGridCoordinateSystemSource.GroupBoxText = "Source Coordinate System";
            this.ctrlGridCoordinateSystemSource.IsEditable = false;
            this.ctrlGridCoordinateSystemSource.Location = new System.Drawing.Point(6, 19);
            this.ctrlGridCoordinateSystemSource.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlGridCoordinateSystemSource.Name = "ctrlGridCoordinateSystemSource";
            this.ctrlGridCoordinateSystemSource.Size = new System.Drawing.Size(336, 155);
            this.ctrlGridCoordinateSystemSource.TabIndex = 16;
            // 
            // ctrlBrowseControlDestinationDir
            // 
            this.ctrlBrowseControlDestinationDir.AutoSize = true;
            this.ctrlBrowseControlDestinationDir.FileName = "";
            this.ctrlBrowseControlDestinationDir.Filter = "";
            this.ctrlBrowseControlDestinationDir.IsFolderDialog = true;
            this.ctrlBrowseControlDestinationDir.IsFullPath = true;
            this.ctrlBrowseControlDestinationDir.IsSaveFile = false;
            this.ctrlBrowseControlDestinationDir.LabelCaption = "Destination Directory:";
            this.ctrlBrowseControlDestinationDir.Location = new System.Drawing.Point(123, 384);
            this.ctrlBrowseControlDestinationDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseControlDestinationDir.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseControlDestinationDir.MultiFilesSelect = false;
            this.ctrlBrowseControlDestinationDir.Name = "ctrlBrowseControlDestinationDir";
            this.ctrlBrowseControlDestinationDir.Prefix = "";
            this.ctrlBrowseControlDestinationDir.Size = new System.Drawing.Size(560, 24);
            this.ctrlBrowseControlDestinationDir.TabIndex = 15;
            // 
            // ctrlGridCoordinateSystemDest
            // 
            this.ctrlGridCoordinateSystemDest.EnableNewCoordSysCreation = true;
            this.ctrlGridCoordinateSystemDest.GridCoordinateSystem = null;
            this.ctrlGridCoordinateSystemDest.GroupBoxText = "Destination Coordinate System";
            this.ctrlGridCoordinateSystemDest.IsEditable = false;
            this.ctrlGridCoordinateSystemDest.Location = new System.Drawing.Point(348, 19);
            this.ctrlGridCoordinateSystemDest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlGridCoordinateSystemDest.Name = "ctrlGridCoordinateSystemDest";
            this.ctrlGridCoordinateSystemDest.Size = new System.Drawing.Size(336, 155);
            this.ctrlGridCoordinateSystemDest.TabIndex = 26;
            // 
            // ctrlSMcBoxClipRect
            // 
            this.ctrlSMcBoxClipRect.GroupBoxText = "Clip Rect";
            this.ctrlSMcBoxClipRect.Location = new System.Drawing.Point(6, 180);
            this.ctrlSMcBoxClipRect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlSMcBoxClipRect.Name = "ctrlSMcBoxClipRect";
            this.ctrlSMcBoxClipRect.Size = new System.Drawing.Size(336, 117);
            this.ctrlSMcBoxClipRect.TabIndex = 21;
            // 
            // gbGetRasterOrDtmLayersFiles
            // 
            this.gbGetRasterOrDtmLayersFiles.Controls.Add(this.label20);
            this.gbGetRasterOrDtmLayersFiles.Controls.Add(this.cbxIsRecUse);
            this.gbGetRasterOrDtmLayersFiles.Controls.Add(this.ctrlBrowseControlPath);
            this.gbGetRasterOrDtmLayersFiles.Controls.Add(this.label8);
            this.gbGetRasterOrDtmLayersFiles.Controls.Add(this.ntxResolutionIndex);
            this.gbGetRasterOrDtmLayersFiles.Controls.Add(this.btnGetRasterOrDtmLayersFiles);
            this.gbGetRasterOrDtmLayersFiles.Location = new System.Drawing.Point(6, 6);
            this.gbGetRasterOrDtmLayersFiles.Name = "gbGetRasterOrDtmLayersFiles";
            this.gbGetRasterOrDtmLayersFiles.Size = new System.Drawing.Size(355, 117);
            this.gbGetRasterOrDtmLayersFiles.TabIndex = 35;
            this.gbGetRasterOrDtmLayersFiles.TabStop = false;
            this.gbGetRasterOrDtmLayersFiles.Text = "Get Raster or DTM Layer Files";
            // 
            // cbxIsRecUse
            // 
            this.cbxIsRecUse.AutoSize = true;
            this.cbxIsRecUse.Location = new System.Drawing.Point(9, 86);
            this.cbxIsRecUse.Name = "cbxIsRecUse";
            this.cbxIsRecUse.Size = new System.Drawing.Size(71, 17);
            this.cbxIsRecUse.TabIndex = 4;
            this.cbxIsRecUse.Text = "Use Rec.";
            this.cbxIsRecUse.UseVisualStyleBackColor = true;
            // 
            // ctrlBrowseControlPath
            // 
            this.ctrlBrowseControlPath.AutoSize = true;
            this.ctrlBrowseControlPath.FileName = "";
            this.ctrlBrowseControlPath.Filter = "";
            this.ctrlBrowseControlPath.IsFolderDialog = true;
            this.ctrlBrowseControlPath.IsFullPath = true;
            this.ctrlBrowseControlPath.IsSaveFile = false;
            this.ctrlBrowseControlPath.LabelCaption = "Path:";
            this.ctrlBrowseControlPath.Location = new System.Drawing.Point(45, 20);
            this.ctrlBrowseControlPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseControlPath.MinimumSize = new System.Drawing.Size(0, 24);
            this.ctrlBrowseControlPath.MultiFilesSelect = false;
            this.ctrlBrowseControlPath.Name = "ctrlBrowseControlPath";
            this.ctrlBrowseControlPath.Prefix = "";
            this.ctrlBrowseControlPath.Size = new System.Drawing.Size(303, 24);
            this.ctrlBrowseControlPath.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Resolution Index:";
            // 
            // ntxResolutionIndex
            // 
            this.ntxResolutionIndex.Location = new System.Drawing.Point(101, 53);
            this.ntxResolutionIndex.Name = "ntxResolutionIndex";
            this.ntxResolutionIndex.Size = new System.Drawing.Size(65, 20);
            this.ntxResolutionIndex.TabIndex = 1;
            // 
            // btnGetRasterOrDtmLayersFiles
            // 
            this.btnGetRasterOrDtmLayersFiles.Location = new System.Drawing.Point(101, 82);
            this.btnGetRasterOrDtmLayersFiles.Name = "btnGetRasterOrDtmLayersFiles";
            this.btnGetRasterOrDtmLayersFiles.Size = new System.Drawing.Size(65, 23);
            this.btnGetRasterOrDtmLayersFiles.TabIndex = 0;
            this.btnGetRasterOrDtmLayersFiles.Text = "Get";
            this.btnGetRasterOrDtmLayersFiles.UseVisualStyleBackColor = true;
            this.btnGetRasterOrDtmLayersFiles.Click += new System.EventHandler(this.btnGetRasterOrDtmLayersFiles_Click);
            // 
            // gbRaster
            // 
            this.gbRaster.BackColor = System.Drawing.SystemColors.Control;
            this.gbRaster.Controls.Add(this.cgbNonStandardCompression);
            this.gbRaster.Controls.Add(this.rdbColorful);
            this.gbRaster.Controls.Add(this.gbColor);
            this.gbRaster.Controls.Add(this.rdbGrayScale);
            this.gbRaster.Controls.Add(this.btnBuldRasterPARFile);
            this.gbRaster.Controls.Add(this.btnConvertRasterLayer);
            this.gbRaster.Controls.Add(this.chxIsImageSourceType);
            this.gbRaster.Location = new System.Drawing.Point(6, 6);
            this.gbRaster.Name = "gbRaster";
            this.gbRaster.Size = new System.Drawing.Size(691, 215);
            this.gbRaster.TabIndex = 29;
            this.gbRaster.TabStop = false;
            this.gbRaster.Text = "Raster";
            // 
            // cgbNonStandardCompression
            // 
            this.cgbNonStandardCompression.Checked = false;
            this.cgbNonStandardCompression.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.cgbNonStandardCompression.Controls.Add(this.label7);
            this.cgbNonStandardCompression.Controls.Add(this.ntxPngColors);
            this.cgbNonStandardCompression.Controls.Add(this.rdbJpeg);
            this.cgbNonStandardCompression.Controls.Add(this.cmbJpegQuality);
            this.cgbNonStandardCompression.Controls.Add(this.rdbPNG);
            this.cgbNonStandardCompression.Controls.Add(this.rdbNone);
            this.cgbNonStandardCompression.Location = new System.Drawing.Point(447, 77);
            this.cgbNonStandardCompression.Name = "cgbNonStandardCompression";
            this.cgbNonStandardCompression.Size = new System.Drawing.Size(238, 100);
            this.cgbNonStandardCompression.TabIndex = 27;
            this.cgbNonStandardCompression.TabStop = false;
            this.cgbNonStandardCompression.Text = "Non - Standard Compression";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(126, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "colors";
            // 
            // ntxPngColors
            // 
            this.ntxPngColors.Enabled = false;
            this.ntxPngColors.Location = new System.Drawing.Point(67, 46);
            this.ntxPngColors.Name = "ntxPngColors";
            this.ntxPngColors.Size = new System.Drawing.Size(53, 20);
            this.ntxPngColors.TabIndex = 30;
            this.ntxPngColors.Text = "15";
            // 
            // rdbJpeg
            // 
            this.rdbJpeg.AutoSize = true;
            this.rdbJpeg.Checked = true;
            this.rdbJpeg.Location = new System.Drawing.Point(9, 23);
            this.rdbJpeg.Name = "rdbJpeg";
            this.rdbJpeg.Size = new System.Drawing.Size(52, 17);
            this.rdbJpeg.TabIndex = 26;
            this.rdbJpeg.TabStop = true;
            this.rdbJpeg.Text = "JPEG";
            this.rdbJpeg.UseVisualStyleBackColor = true;
            this.rdbJpeg.CheckedChanged += new System.EventHandler(this.rdbJpeg_CheckedChanged);
            // 
            // cmbJpegQuality
            // 
            this.cmbJpegQuality.FormattingEnabled = true;
            this.cmbJpegQuality.Location = new System.Drawing.Point(67, 19);
            this.cmbJpegQuality.Name = "cmbJpegQuality";
            this.cmbJpegQuality.Size = new System.Drawing.Size(165, 21);
            this.cmbJpegQuality.TabIndex = 29;
            // 
            // rdbPNG
            // 
            this.rdbPNG.AutoSize = true;
            this.rdbPNG.Location = new System.Drawing.Point(9, 47);
            this.rdbPNG.Name = "rdbPNG";
            this.rdbPNG.Size = new System.Drawing.Size(48, 17);
            this.rdbPNG.TabIndex = 27;
            this.rdbPNG.Text = "PNG";
            this.rdbPNG.UseVisualStyleBackColor = true;
            this.rdbPNG.CheckedChanged += new System.EventHandler(this.rdbPNG_CheckedChanged);
            // 
            // rdbNone
            // 
            this.rdbNone.AutoSize = true;
            this.rdbNone.Location = new System.Drawing.Point(9, 71);
            this.rdbNone.Name = "rdbNone";
            this.rdbNone.Size = new System.Drawing.Size(51, 17);
            this.rdbNone.TabIndex = 28;
            this.rdbNone.Text = "None";
            this.rdbNone.UseVisualStyleBackColor = true;
            // 
            // rdbColorful
            // 
            this.rdbColorful.AutoSize = true;
            this.rdbColorful.Checked = true;
            this.rdbColorful.Location = new System.Drawing.Point(11, 77);
            this.rdbColorful.Name = "rdbColorful";
            this.rdbColorful.Size = new System.Drawing.Size(14, 13);
            this.rdbColorful.TabIndex = 24;
            this.rdbColorful.TabStop = true;
            this.rdbColorful.UseVisualStyleBackColor = true;
            this.rdbColorful.CheckedChanged += new System.EventHandler(this.rdbColorful_CheckedChanged);
            // 
            // gbColor
            // 
            this.gbColor.Controls.Add(this.chxGrayScale);
            this.gbColor.Controls.Add(this.label2);
            this.gbColor.Controls.Add(this.btnTransparentColor);
            this.gbColor.Controls.Add(this.pbTransparentColor);
            this.gbColor.Controls.Add(this.label4);
            this.gbColor.Controls.Add(this.ntxTransparentColorPrecision);
            this.gbColor.Controls.Add(this.label3);
            this.gbColor.Controls.Add(this.cmbRasterOverlapMode);
            this.gbColor.Location = new System.Drawing.Point(28, 77);
            this.gbColor.Name = "gbColor";
            this.gbColor.Size = new System.Drawing.Size(405, 100);
            this.gbColor.TabIndex = 23;
            this.gbColor.TabStop = false;
            this.gbColor.Text = "Color";
            // 
            // chxGrayScale
            // 
            this.chxGrayScale.AutoSize = true;
            this.chxGrayScale.Location = new System.Drawing.Point(250, 49);
            this.chxGrayScale.Name = "chxGrayScale";
            this.chxGrayScale.Size = new System.Drawing.Size(73, 17);
            this.chxGrayScale.TabIndex = 21;
            this.chxGrayScale.Text = "Grayscale";
            this.chxGrayScale.UseVisualStyleBackColor = true;
            // 
            // pbTransparentColor
            // 
            this.pbTransparentColor.BackColor = System.Drawing.Color.Black;
            this.pbTransparentColor.Location = new System.Drawing.Point(192, 45);
            this.pbTransparentColor.Name = "pbTransparentColor";
            this.pbTransparentColor.Size = new System.Drawing.Size(36, 24);
            this.pbTransparentColor.TabIndex = 19;
            this.pbTransparentColor.TabStop = false;
            // 
            // ntxTransparentColorPrecision
            // 
            this.ntxTransparentColorPrecision.Location = new System.Drawing.Point(153, 74);
            this.ntxTransparentColorPrecision.Name = "ntxTransparentColorPrecision";
            this.ntxTransparentColorPrecision.Size = new System.Drawing.Size(49, 20);
            this.ntxTransparentColorPrecision.TabIndex = 20;
            this.ntxTransparentColorPrecision.Text = "0";
            // 
            // rdbGrayScale
            // 
            this.rdbGrayScale.AutoSize = true;
            this.rdbGrayScale.Location = new System.Drawing.Point(9, 52);
            this.rdbGrayScale.Name = "rdbGrayScale";
            this.rdbGrayScale.Size = new System.Drawing.Size(88, 17);
            this.rdbGrayScale.TabIndex = 22;
            this.rdbGrayScale.Text = "To Grayscale";
            this.rdbGrayScale.UseVisualStyleBackColor = true;
            // 
            // btnBuldRasterPARFile
            // 
            this.btnBuldRasterPARFile.Location = new System.Drawing.Point(585, 25);
            this.btnBuldRasterPARFile.Name = "btnBuldRasterPARFile";
            this.btnBuldRasterPARFile.Size = new System.Drawing.Size(87, 23);
            this.btnBuldRasterPARFile.TabIndex = 21;
            this.btnBuldRasterPARFile.Text = "Build PAR File";
            this.btnBuldRasterPARFile.UseVisualStyleBackColor = true;
            this.btnBuldRasterPARFile.Click += new System.EventHandler(this.btnBuildRasterPARFile_Click);
            // 
            // chxIsImageSourceType
            // 
            this.chxIsImageSourceType.AutoSize = true;
            this.chxIsImageSourceType.Location = new System.Drawing.Point(9, 29);
            this.chxIsImageSourceType.Name = "chxIsImageSourceType";
            this.chxIsImageSourceType.Size = new System.Drawing.Size(130, 17);
            this.chxIsImageSourceType.TabIndex = 18;
            this.chxIsImageSourceType.Text = "Is Image Source Type";
            this.chxIsImageSourceType.UseVisualStyleBackColor = true;
            // 
            // gbRemoveBorders
            // 
            this.gbRemoveBorders.Controls.Add(this.label21);
            this.gbRemoveBorders.Controls.Add(this.ctrlColorArray);
            this.gbRemoveBorders.Controls.Add(this.btnRemoveBorders);
            this.gbRemoveBorders.Controls.Add(this.cbIsSetAlpha);
            this.gbRemoveBorders.Controls.Add(this.ntbNearColorDist);
            this.gbRemoveBorders.Controls.Add(this.label10);
            this.gbRemoveBorders.Controls.Add(this.ntbNearColorDistance);
            this.gbRemoveBorders.Controls.Add(this.ntbMaxNonBorderColor);
            this.gbRemoveBorders.Controls.Add(this.ctrlBrowserRmvBrdrInputFile);
            this.gbRemoveBorders.Location = new System.Drawing.Point(3, 7);
            this.gbRemoveBorders.Name = "gbRemoveBorders";
            this.gbRemoveBorders.Size = new System.Drawing.Size(691, 178);
            this.gbRemoveBorders.TabIndex = 32;
            this.gbRemoveBorders.TabStop = false;
            this.gbRemoveBorders.Text = "Remove Borders";
            // 
            // ctrlColorArray
            // 
            this.ctrlColorArray.Location = new System.Drawing.Point(315, 29);
            this.ctrlColorArray.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlColorArray.Name = "ctrlColorArray";
            this.ctrlColorArray.Size = new System.Drawing.Size(351, 143);
            this.ctrlColorArray.TabIndex = 10;
            // 
            // btnRemoveBorders
            // 
            this.btnRemoveBorders.Location = new System.Drawing.Point(189, 138);
            this.btnRemoveBorders.Name = "btnRemoveBorders";
            this.btnRemoveBorders.Size = new System.Drawing.Size(120, 23);
            this.btnRemoveBorders.TabIndex = 9;
            this.btnRemoveBorders.Text = "Remove Borders";
            this.btnRemoveBorders.UseVisualStyleBackColor = true;
            this.btnRemoveBorders.Click += new System.EventHandler(this.btnRemoveBorders_Click);
            // 
            // cbIsSetAlpha
            // 
            this.cbIsSetAlpha.AutoSize = true;
            this.cbIsSetAlpha.Location = new System.Drawing.Point(13, 118);
            this.cbIsSetAlpha.Name = "cbIsSetAlpha";
            this.cbIsSetAlpha.Size = new System.Drawing.Size(83, 17);
            this.cbIsSetAlpha.TabIndex = 8;
            this.cbIsSetAlpha.Text = "Is Set Alpha";
            this.cbIsSetAlpha.UseVisualStyleBackColor = true;
            // 
            // ntbNearColorDist
            // 
            this.ntbNearColorDist.AutoSize = true;
            this.ntbNearColorDist.Location = new System.Drawing.Point(10, 92);
            this.ntbNearColorDist.Name = "ntbNearColorDist";
            this.ntbNearColorDist.Size = new System.Drawing.Size(105, 13);
            this.ntbNearColorDist.TabIndex = 6;
            this.ntbNearColorDist.Text = "Near Color Distance:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Max Non Border Color:";
            // 
            // ntbNearColorDistance
            // 
            this.ntbNearColorDistance.Location = new System.Drawing.Point(131, 85);
            this.ntbNearColorDistance.Name = "ntbNearColorDistance";
            this.ntbNearColorDistance.Size = new System.Drawing.Size(100, 20);
            this.ntbNearColorDistance.TabIndex = 3;
            // 
            // ntbMaxNonBorderColor
            // 
            this.ntbMaxNonBorderColor.Location = new System.Drawing.Point(131, 59);
            this.ntbMaxNonBorderColor.Name = "ntbMaxNonBorderColor";
            this.ntbMaxNonBorderColor.Size = new System.Drawing.Size(100, 20);
            this.ntbMaxNonBorderColor.TabIndex = 2;
            // 
            // ctrlBrowserRmvBrdrInputFile
            // 
            this.ctrlBrowserRmvBrdrInputFile.AutoSize = true;
            this.ctrlBrowserRmvBrdrInputFile.FileName = "";
            this.ctrlBrowserRmvBrdrInputFile.Filter = "";
            this.ctrlBrowserRmvBrdrInputFile.IsFolderDialog = false;
            this.ctrlBrowserRmvBrdrInputFile.IsFullPath = true;
            this.ctrlBrowserRmvBrdrInputFile.IsSaveFile = false;
            this.ctrlBrowserRmvBrdrInputFile.LabelCaption = "Input File Name:";
            this.ctrlBrowserRmvBrdrInputFile.Location = new System.Drawing.Point(99, 31);
            this.ctrlBrowserRmvBrdrInputFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowserRmvBrdrInputFile.MinimumSize = new System.Drawing.Size(200, 24);
            this.ctrlBrowserRmvBrdrInputFile.MultiFilesSelect = false;
            this.ctrlBrowserRmvBrdrInputFile.Name = "ctrlBrowserRmvBrdrInputFile";
            this.ctrlBrowserRmvBrdrInputFile.Prefix = "";
            this.ctrlBrowserRmvBrdrInputFile.Size = new System.Drawing.Size(210, 24);
            this.ctrlBrowserRmvBrdrInputFile.TabIndex = 0;
            // 
            // btnConvertDTMLayer
            // 
            this.btnConvertDTMLayer.Enabled = false;
            this.btnConvertDTMLayer.Location = new System.Drawing.Point(591, 44);
            this.btnConvertDTMLayer.Name = "btnConvertDTMLayer";
            this.btnConvertDTMLayer.Size = new System.Drawing.Size(87, 23);
            this.btnConvertDTMLayer.TabIndex = 30;
            this.btnConvertDTMLayer.Text = "Convert DTM";
            this.btnConvertDTMLayer.UseVisualStyleBackColor = true;
            this.btnConvertDTMLayer.Click += new System.EventHandler(this.btnConvertDTMLayer_Click);
            // 
            // gbDTM
            // 
            this.gbDTM.Controls.Add(this.label5);
            this.gbDTM.Controls.Add(this.ntxNumSmoothingLevels);
            this.gbDTM.Controls.Add(this.chxFillNoHeightAreas);
            this.gbDTM.Controls.Add(this.btnBuildDTMPARFile);
            this.gbDTM.Controls.Add(this.btnConvertDTMLayer);
            this.gbDTM.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbDTM.Location = new System.Drawing.Point(2, 6);
            this.gbDTM.Name = "gbDTM";
            this.gbDTM.Size = new System.Drawing.Size(691, 75);
            this.gbDTM.TabIndex = 31;
            this.gbDTM.TabStop = false;
            this.gbDTM.Text = "DTM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Num Smoothing Levels:";
            // 
            // ntxNumSmoothingLevels
            // 
            this.ntxNumSmoothingLevels.Location = new System.Drawing.Point(131, 46);
            this.ntxNumSmoothingLevels.Name = "ntxNumSmoothingLevels";
            this.ntxNumSmoothingLevels.Size = new System.Drawing.Size(100, 20);
            this.ntxNumSmoothingLevels.TabIndex = 32;
            this.ntxNumSmoothingLevels.Text = "0";
            // 
            // chxFillNoHeightAreas
            // 
            this.chxFillNoHeightAreas.AutoSize = true;
            this.chxFillNoHeightAreas.Location = new System.Drawing.Point(9, 19);
            this.chxFillNoHeightAreas.Name = "chxFillNoHeightAreas";
            this.chxFillNoHeightAreas.Size = new System.Drawing.Size(119, 17);
            this.chxFillNoHeightAreas.TabIndex = 31;
            this.chxFillNoHeightAreas.Text = "Fill No Height Areas";
            this.chxFillNoHeightAreas.UseVisualStyleBackColor = true;
            // 
            // btnBuildDTMPARFile
            // 
            this.btnBuildDTMPARFile.Location = new System.Drawing.Point(591, 15);
            this.btnBuildDTMPARFile.Name = "btnBuildDTMPARFile";
            this.btnBuildDTMPARFile.Size = new System.Drawing.Size(87, 23);
            this.btnBuildDTMPARFile.TabIndex = 22;
            this.btnBuildDTMPARFile.Text = "Build PAR File";
            this.btnBuildDTMPARFile.UseVisualStyleBackColor = true;
            this.btnBuildDTMPARFile.Click += new System.EventHandler(this.btnBuildDTMPARFile_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGeneral);
            this.tabControl1.Controls.Add(this.tpGetRasterOrDTMFiles);
            this.tabControl1.Controls.Add(this.tpRemoveBorders);
            this.tabControl1.Location = new System.Drawing.Point(3, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(755, 780);
            this.tabControl1.TabIndex = 36;
            // 
            // tpGeneral
            // 
            this.tpGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.tpGeneral.Controls.Add(this.tabControl2);
            this.tpGeneral.Controls.Add(this.gbGeneral);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpGeneral.Size = new System.Drawing.Size(747, 754);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tpRaster);
            this.tabControl2.Controls.Add(this.tpDTM);
            this.tabControl2.Controls.Add(this.tpHeatMap);
            this.tabControl2.Location = new System.Drawing.Point(6, 486);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(718, 259);
            this.tabControl2.TabIndex = 32;
            // 
            // tpRaster
            // 
            this.tpRaster.BackColor = System.Drawing.SystemColors.Control;
            this.tpRaster.Controls.Add(this.gbRaster);
            this.tpRaster.Location = new System.Drawing.Point(4, 22);
            this.tpRaster.Name = "tpRaster";
            this.tpRaster.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpRaster.Size = new System.Drawing.Size(710, 233);
            this.tpRaster.TabIndex = 0;
            this.tpRaster.Text = "Raster";
            // 
            // tpDTM
            // 
            this.tpDTM.BackColor = System.Drawing.SystemColors.Control;
            this.tpDTM.Controls.Add(this.gbDTM);
            this.tpDTM.Location = new System.Drawing.Point(4, 22);
            this.tpDTM.Name = "tpDTM";
            this.tpDTM.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpDTM.Size = new System.Drawing.Size(710, 233);
            this.tpDTM.TabIndex = 1;
            this.tpDTM.Text = "DTM";
            // 
            // tpHeatMap
            // 
            this.tpHeatMap.BackColor = System.Drawing.SystemColors.Control;
            this.tpHeatMap.Controls.Add(this.groupBox1);
            this.tpHeatMap.Location = new System.Drawing.Point(4, 22);
            this.tpHeatMap.Name = "tpHeatMap";
            this.tpHeatMap.Size = new System.Drawing.Size(710, 233);
            this.tpHeatMap.TabIndex = 2;
            this.tpHeatMap.Text = "Heat Map";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntbMinVal);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.ntbMaxVal);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.ntbMinValThreshold);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.ntbMaxValThreshold);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.chxGPUBased);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cbItemType);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.ntbTargetLowestResolution);
            this.groupBox1.Controls.Add(this.bCalcAveragePerPoint);
            this.groupBox1.Controls.Add(this.btnConvertHeatMap);
            this.groupBox1.Controls.Add(this.ntbPointInfluenceRadius);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.chxIsGradient);
            this.groupBox1.Controls.Add(this.chxIsRadiusInPixels);
            this.groupBox1.Controls.Add(this.dgvHeatMapPoints);
            this.groupBox1.Location = new System.Drawing.Point(5, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(702, 230);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Params";
            // 
            // ntbMinVal
            // 
            this.ntbMinVal.Location = new System.Drawing.Point(361, 129);
            this.ntbMinVal.Name = "ntbMinVal";
            this.ntbMinVal.Size = new System.Drawing.Size(78, 20);
            this.ntbMinVal.TabIndex = 20;
            this.ntbMinVal.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(259, 134);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 19;
            this.label16.Text = "Min Val:";
            // 
            // ntbMaxVal
            // 
            this.ntbMaxVal.Location = new System.Drawing.Point(361, 154);
            this.ntbMaxVal.Name = "ntbMaxVal";
            this.ntbMaxVal.Size = new System.Drawing.Size(78, 20);
            this.ntbMaxVal.TabIndex = 18;
            this.ntbMaxVal.Text = "MAX";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(259, 159);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(48, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "Max Val:";
            // 
            // ntbMinValThreshold
            // 
            this.ntbMinValThreshold.Location = new System.Drawing.Point(358, 78);
            this.ntbMinValThreshold.Name = "ntbMinValThreshold";
            this.ntbMinValThreshold.Size = new System.Drawing.Size(78, 20);
            this.ntbMinValThreshold.TabIndex = 16;
            this.ntbMinValThreshold.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(256, 83);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 13);
            this.label15.TabIndex = 15;
            this.label15.Text = "Min Val Threshold:";
            // 
            // ntbMaxValThreshold
            // 
            this.ntbMaxValThreshold.Location = new System.Drawing.Point(358, 103);
            this.ntbMaxValThreshold.Name = "ntbMaxValThreshold";
            this.ntbMaxValThreshold.Size = new System.Drawing.Size(78, 20);
            this.ntbMaxValThreshold.TabIndex = 14;
            this.ntbMaxValThreshold.Text = "MAX";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(256, 108);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(98, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Max Val Threshold:";
            // 
            // chxGPUBased
            // 
            this.chxGPUBased.AutoSize = true;
            this.chxGPUBased.Location = new System.Drawing.Point(6, 47);
            this.chxGPUBased.Name = "chxGPUBased";
            this.chxGPUBased.Size = new System.Drawing.Size(82, 17);
            this.chxGPUBased.TabIndex = 12;
            this.chxGPUBased.Text = "GPU Based";
            this.chxGPUBased.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Item Type:";
            // 
            // cbItemType
            // 
            this.cbItemType.FormattingEnabled = true;
            this.cbItemType.Location = new System.Drawing.Point(87, 17);
            this.cbItemType.Name = "cbItemType";
            this.cbItemType.Size = new System.Drawing.Size(140, 21);
            this.cbItemType.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Target Lowest Resolution:";
            // 
            // ntbTargetLowestResolution
            // 
            this.ntbTargetLowestResolution.Location = new System.Drawing.Point(140, 167);
            this.ntbTargetLowestResolution.Name = "ntbTargetLowestResolution";
            this.ntbTargetLowestResolution.Size = new System.Drawing.Size(100, 20);
            this.ntbTargetLowestResolution.TabIndex = 7;
            this.ntbTargetLowestResolution.Text = "MAX";
            this.ntbTargetLowestResolution.TextChanged += new System.EventHandler(this.numericTextBox1_TextChanged);
            // 
            // bCalcAveragePerPoint
            // 
            this.bCalcAveragePerPoint.AutoSize = true;
            this.bCalcAveragePerPoint.Location = new System.Drawing.Point(6, 93);
            this.bCalcAveragePerPoint.Name = "bCalcAveragePerPoint";
            this.bCalcAveragePerPoint.Size = new System.Drawing.Size(136, 17);
            this.bCalcAveragePerPoint.TabIndex = 6;
            this.bCalcAveragePerPoint.Text = "Calc Average Per Point";
            this.bCalcAveragePerPoint.UseVisualStyleBackColor = true;
            // 
            // btnConvertHeatMap
            // 
            this.btnConvertHeatMap.Location = new System.Drawing.Point(621, 195);
            this.btnConvertHeatMap.Name = "btnConvertHeatMap";
            this.btnConvertHeatMap.Size = new System.Drawing.Size(75, 23);
            this.btnConvertHeatMap.TabIndex = 5;
            this.btnConvertHeatMap.Text = "Convert";
            this.btnConvertHeatMap.UseVisualStyleBackColor = true;
            this.btnConvertHeatMap.Click += new System.EventHandler(this.btnConvertHeatMap_Click);
            // 
            // ntbPointInfluenceRadius
            // 
            this.ntbPointInfluenceRadius.Location = new System.Drawing.Point(140, 143);
            this.ntbPointInfluenceRadius.Name = "ntbPointInfluenceRadius";
            this.ntbPointInfluenceRadius.Size = new System.Drawing.Size(100, 20);
            this.ntbPointInfluenceRadius.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 146);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Point Influence Radius:";
            // 
            // chxIsGradient
            // 
            this.chxIsGradient.AutoSize = true;
            this.chxIsGradient.Location = new System.Drawing.Point(6, 116);
            this.chxIsGradient.Name = "chxIsGradient";
            this.chxIsGradient.Size = new System.Drawing.Size(77, 17);
            this.chxIsGradient.TabIndex = 2;
            this.chxIsGradient.Text = "Is Gradient";
            this.chxIsGradient.UseVisualStyleBackColor = true;
            // 
            // chxIsRadiusInPixels
            // 
            this.chxIsRadiusInPixels.AutoSize = true;
            this.chxIsRadiusInPixels.Location = new System.Drawing.Point(6, 70);
            this.chxIsRadiusInPixels.Name = "chxIsRadiusInPixels";
            this.chxIsRadiusInPixels.Size = new System.Drawing.Size(112, 17);
            this.chxIsRadiusInPixels.TabIndex = 1;
            this.chxIsRadiusInPixels.Text = "Is Radius In Pixels";
            this.chxIsRadiusInPixels.UseVisualStyleBackColor = true;
            // 
            // dgvHeatMapPoints
            // 
            this.dgvHeatMapPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHeatMapPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectRect,
            this.PointsDetails});
            this.dgvHeatMapPoints.Location = new System.Drawing.Point(446, 15);
            this.dgvHeatMapPoints.Name = "dgvHeatMapPoints";
            this.dgvHeatMapPoints.Size = new System.Drawing.Size(250, 174);
            this.dgvHeatMapPoints.TabIndex = 0;
            this.dgvHeatMapPoints.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHeatMapPoints_CellContentClick);
            // 
            // SelectRect
            // 
            this.SelectRect.HeaderText = "Select Rect";
            this.SelectRect.Name = "SelectRect";
            // 
            // PointsDetails
            // 
            this.PointsDetails.HeaderText = "Points Details";
            this.PointsDetails.Name = "PointsDetails";
            // 
            // tpGetRasterOrDTMFiles
            // 
            this.tpGetRasterOrDTMFiles.BackColor = System.Drawing.SystemColors.Control;
            this.tpGetRasterOrDTMFiles.Controls.Add(this.gbGetRasterOrDtmLayersFiles);
            this.tpGetRasterOrDTMFiles.Location = new System.Drawing.Point(4, 22);
            this.tpGetRasterOrDTMFiles.Name = "tpGetRasterOrDTMFiles";
            this.tpGetRasterOrDTMFiles.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpGetRasterOrDTMFiles.Size = new System.Drawing.Size(747, 754);
            this.tpGetRasterOrDTMFiles.TabIndex = 1;
            this.tpGetRasterOrDTMFiles.Text = "Get Raster Or DTM Files";
            // 
            // tpRemoveBorders
            // 
            this.tpRemoveBorders.BackColor = System.Drawing.SystemColors.Control;
            this.tpRemoveBorders.Controls.Add(this.gbRemoveBorders);
            this.tpRemoveBorders.Location = new System.Drawing.Point(4, 22);
            this.tpRemoveBorders.Name = "tpRemoveBorders";
            this.tpRemoveBorders.Size = new System.Drawing.Size(747, 754);
            this.tpRemoveBorders.TabIndex = 2;
            this.tpRemoveBorders.Text = "Remove Borders";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 388);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 13);
            this.label19.TabIndex = 73;
            this.label19.Text = "Destination Directory:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "Path:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 35);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(84, 13);
            this.label21.TabIndex = 11;
            this.label21.Text = "Input File Name:";
            // 
            // btnMapProductionToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(775, 789);
            this.Controls.Add(this.tabControl1);
            this.Name = "btnMapProductionToolForm";
            this.Text = "Map Production";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMapProduction_FormClosing);
            this.Load += new System.EventHandler(this.frmMapProduction_Load);
            this.gbGeneral.ResumeLayout(false);
            this.gbGeneral.PerformLayout();
            this.gbConversionOptions.ResumeLayout(false);
            this.gbConversionOptions.PerformLayout();
            this.gbGetRasterOrDtmLayersFiles.ResumeLayout(false);
            this.gbGetRasterOrDtmLayersFiles.PerformLayout();
            this.gbRaster.ResumeLayout(false);
            this.gbRaster.PerformLayout();
            this.cgbNonStandardCompression.ResumeLayout(false);
            this.cgbNonStandardCompression.PerformLayout();
            this.gbColor.ResumeLayout(false);
            this.gbColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTransparentColor)).EndInit();
            this.gbRemoveBorders.ResumeLayout(false);
            this.gbRemoveBorders.PerformLayout();
            this.gbDTM.ResumeLayout(false);
            this.gbDTM.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tpRaster.ResumeLayout(false);
            this.tpDTM.ResumeLayout(false);
            this.tpHeatMap.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeatMapPoints)).EndInit();
            this.tpGetRasterOrDTMFiles.ResumeLayout(false);
            this.tpRemoveBorders.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConvertRasterLayer;
        private System.Windows.Forms.ComboBox cmbRasterOverlapMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chxUpdateExisting;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseControlDestinationDir;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlGridCoordinateSystemSource;
        private System.Windows.Forms.Button btnTransparentColor;
        private System.Windows.Forms.PictureBox pbTransparentColor;
        private MCTester.Controls.NumericTextBox ntxTransparentColorPrecision;
        private MCTester.Controls.CtrlSMcBox ctrlSMcBoxClipRect;
        private MCTester.Controls.NumericTextBox ntxTargetHighestResolution;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlGridCoordinateSystemDest;
        private System.Windows.Forms.GroupBox gbGeneral;
        private System.Windows.Forms.GroupBox gbRaster;
        private System.Windows.Forms.GroupBox gbConversionOptions;
        private System.Windows.Forms.RadioButton rdbTargetHighestResolutionCustom;
        private System.Windows.Forms.RadioButton rdbTargetHighestResolutionSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chxImageCoordinateSystem;
        private System.Windows.Forms.Button btnBuldRasterPARFile;
        private System.Windows.Forms.ListBox lstProgressMessage;
        private System.Windows.Forms.CheckBox chxShowClipping;
        private System.Windows.Forms.Button btnConvertDTMLayer;
        private System.Windows.Forms.GroupBox gbDTM;
        private System.Windows.Forms.CheckBox chxIsImageSourceType;
        private System.Windows.Forms.Button btnBuildDTMPARFile;
        private System.Windows.Forms.CheckBox chxFillNoHeightAreas;
        private System.Windows.Forms.Label label5;
        private MCTester.Controls.NumericTextBox ntxNumSmoothingLevels;
        private System.Windows.Forms.RadioButton rdbColorful;
        private System.Windows.Forms.GroupBox gbColor;
        private System.Windows.Forms.RadioButton rdbGrayScale;
        private System.Windows.Forms.CheckBox chxGrayScale;
        private System.Windows.Forms.CheckBox chxOneResolutionOnly;
        private MCTester.Controls.NumericTextBox ntxReprojectionPrecision;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Controls.NumericTextBox ntxPngColors;
        private System.Windows.Forms.ComboBox cmbJpegQuality;
        private System.Windows.Forms.RadioButton rdbNone;
        private System.Windows.Forms.RadioButton rdbPNG;
        private System.Windows.Forms.RadioButton rdbJpeg;
        private Controls.CheckGroupBox cgbNonStandardCompression;
        private System.Windows.Forms.GroupBox gbGetRasterOrDtmLayersFiles;
        private System.Windows.Forms.Label label8;
        private Controls.NumericTextBox ntxResolutionIndex;
        private System.Windows.Forms.Button btnGetRasterOrDtmLayersFiles;
        private Controls.CtrlBrowseControl ctrlBrowseControlPath;
        private System.Windows.Forms.CheckBox cbxIsRecUse;
        private Controls.NumericTextBox ntxNumTilesInFileEdge;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbRemoveBorders;
        private Controls.CtrlBrowseControl ctrlBrowserRmvBrdrInputFile;
        private System.Windows.Forms.Label ntbNearColorDist;
        private System.Windows.Forms.Label label10;
        private Controls.NumericTextBox ntbNearColorDistance;
        private Controls.NumericTextBox ntbMaxNonBorderColor;
        private System.Windows.Forms.Button btnRemoveBorders;
        private System.Windows.Forms.CheckBox cbIsSetAlpha;
        private Controls.CtrlColorArray ctrlColorArray;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tpRaster;
        private System.Windows.Forms.TabPage tpDTM;
        private System.Windows.Forms.TabPage tpHeatMap;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tpGetRasterOrDTMFiles;
        private System.Windows.Forms.TabPage tpRemoveBorders;
        private System.Windows.Forms.Button btnConvertHeatMap;
        private Controls.NumericTextBox ntbPointInfluenceRadius;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chxIsGradient;
        private System.Windows.Forms.CheckBox chxIsRadiusInPixels;
        private System.Windows.Forms.DataGridView dgvHeatMapPoints;
        private System.Windows.Forms.DataGridViewButtonColumn SelectRect;
        private System.Windows.Forms.DataGridViewButtonColumn PointsDetails;
        private System.Windows.Forms.CheckBox bCalcAveragePerPoint;
        private System.Windows.Forms.Label label12;
        private Controls.NumericTextBox ntbTargetLowestResolution;
        private System.Windows.Forms.CheckBox chxGPUBased;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbItemType;
        private Controls.NumericTextBox ntbMinValThreshold;
        private System.Windows.Forms.Label label15;
        private Controls.NumericTextBox ntbMaxValThreshold;
        private System.Windows.Forms.Label label14;
        private Controls.NumericTextBox ntbMinVal;
        private System.Windows.Forms.Label label16;
        private Controls.NumericTextBox ntbMaxVal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnTilingScheme;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cmbVersionCompatibility;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
    }
}