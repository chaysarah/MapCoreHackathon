namespace MCTester.Controls
{
    partial class ucLayerParams
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
            this.chxEnhanceBorderOverlap2 = new System.Windows.Forms.CheckBox();
            this.ctrlLocalCacheParams = new MCTester.Controls.LocalCacheParams();
            this.lblNoOfLayers = new System.Windows.Forms.Label();
            this.noudNoOfLayers = new System.Windows.Forms.NumericUpDown();
            this.ntxNumLevelsToIgnore = new MCTester.Controls.NumericTextBox();
            this.lblNoOfIgnore = new System.Windows.Forms.Label();
            this.cbxThereAreMissingFiles = new System.Windows.Forms.CheckBox();
            this.lblFirstLowerQualityLevel = new System.Windows.Forms.Label();
            this.ntxFirstLowerQualityLevel = new MCTester.Controls.NumericTextBox();
            this.cmbMapLayerType = new System.Windows.Forms.ComboBox();
            this.browseLayerCtrl = new MCTester.Controls.CtrlBrowseControl();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxHighestResolution = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbWorldLimit = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ctrl3DMaxWorldBoundingBox = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DMinWorldBoundingBox = new MCTester.Controls.Ctrl3DVector();
            this.tcLayerParams = new System.Windows.Forms.TabControl();
            this.tpRawParams = new System.Windows.Forms.TabPage();
            this.ctrlRawRasterComponentParams1 = new MCTester.Controls.CtrlRawRasterComponentParams();
            this.cbIgnoreRasterPalette = new System.Windows.Forms.CheckBox();
            this.txtPyramidResolution = new System.Windows.Forms.TextBox();
            this.ntbMaxNumOpenFiles = new MCTester.Controls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chxImageCoordSys = new System.Windows.Forms.CheckBox();
            this.ntxFirstPyramidResolution = new MCTester.Controls.NumericTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tpWMSLayerParams = new System.Windows.Forms.TabPage();
            this.ctrlWebServiceLayerParams1 = new MCTester.MapWorld.WebMapLayers.CtrlWebServiceLayerParams();
            this.tpRawLayerParams = new System.Windows.Forms.TabPage();
            this.ctrlNonNativeParams1 = new MCTester.MapWorld.MapUserControls.CtrlNonNativeParams();
            this.tcParams = new System.Windows.Forms.TabControl();
            this.tpVectorLayerParams = new System.Windows.Forms.TabPage();
            this.rawVectorParams1 = new MCTester.Controls.CtrlRawVectorParams();
            this.tpVector3DExtrusionParams = new System.Windows.Forms.TabPage();
            this.ctrlRawVector3DExtrusionParams1 = new MCTester.Controls.CtrlRawVector3DExtrusionParams();
            this.ntbExtrusionHeightMaxAddition = new MCTester.Controls.NumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tp3DModelParams = new System.Windows.Forms.TabPage();
            this.ctrlRaw3DModelParams1 = new MCTester.Controls.CtrlRaw3DModelParams();
            this.btnTilingScheme1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.chxPostProcessSourceData = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.noudNoOfLayers)).BeginInit();
            this.gbWorldLimit.SuspendLayout();
            this.tcLayerParams.SuspendLayout();
            this.tpRawParams.SuspendLayout();
            this.tpWMSLayerParams.SuspendLayout();
            this.tpRawLayerParams.SuspendLayout();
            this.tcParams.SuspendLayout();
            this.tpVectorLayerParams.SuspendLayout();
            this.tpVector3DExtrusionParams.SuspendLayout();
            this.tp3DModelParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // chxEnhanceBorderOverlap2
            // 
            this.chxEnhanceBorderOverlap2.AutoSize = true;
            this.chxEnhanceBorderOverlap2.Location = new System.Drawing.Point(487, 79);
            this.chxEnhanceBorderOverlap2.Name = "chxEnhanceBorderOverlap2";
            this.chxEnhanceBorderOverlap2.Size = new System.Drawing.Size(143, 17);
            this.chxEnhanceBorderOverlap2.TabIndex = 49;
            this.chxEnhanceBorderOverlap2.Text = "Enhance Border Overlap";
            this.chxEnhanceBorderOverlap2.UseVisualStyleBackColor = true;
            // 
            // ctrlLocalCacheParams
            // 
            this.ctrlLocalCacheParams.Location = new System.Drawing.Point(7, 55);
            this.ctrlLocalCacheParams.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlLocalCacheParams.Name = "ctrlLocalCacheParams";
            this.ctrlLocalCacheParams.Size = new System.Drawing.Size(473, 70);
            this.ctrlLocalCacheParams.SubFolderPath = "";
            this.ctrlLocalCacheParams.TabIndex = 48;
            // 
            // lblNoOfLayers
            // 
            this.lblNoOfLayers.AutoSize = true;
            this.lblNoOfLayers.Location = new System.Drawing.Point(313, 6);
            this.lblNoOfLayers.Name = "lblNoOfLayers";
            this.lblNoOfLayers.Size = new System.Drawing.Size(70, 13);
            this.lblNoOfLayers.TabIndex = 47;
            this.lblNoOfLayers.Text = "No. of Layers";
            // 
            // noudNoOfLayers
            // 
            this.noudNoOfLayers.Location = new System.Drawing.Point(401, 4);
            this.noudNoOfLayers.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.noudNoOfLayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.noudNoOfLayers.Name = "noudNoOfLayers";
            this.noudNoOfLayers.Size = new System.Drawing.Size(54, 20);
            this.noudNoOfLayers.TabIndex = 46;
            this.noudNoOfLayers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ntxNumLevelsToIgnore
            // 
            this.ntxNumLevelsToIgnore.Location = new System.Drawing.Point(618, 6);
            this.ntxNumLevelsToIgnore.Name = "ntxNumLevelsToIgnore";
            this.ntxNumLevelsToIgnore.Size = new System.Drawing.Size(54, 20);
            this.ntxNumLevelsToIgnore.TabIndex = 45;
            this.ntxNumLevelsToIgnore.Text = "0";
            // 
            // lblNoOfIgnore
            // 
            this.lblNoOfIgnore.AutoSize = true;
            this.lblNoOfIgnore.Location = new System.Drawing.Point(484, 9);
            this.lblNoOfIgnore.Name = "lblNoOfIgnore";
            this.lblNoOfIgnore.Size = new System.Drawing.Size(129, 13);
            this.lblNoOfIgnore.TabIndex = 44;
            this.lblNoOfIgnore.Text = "Num Of Levels To Ignore:";
            // 
            // cbxThereAreMissingFiles
            // 
            this.cbxThereAreMissingFiles.AutoSize = true;
            this.cbxThereAreMissingFiles.Location = new System.Drawing.Point(487, 55);
            this.cbxThereAreMissingFiles.Name = "cbxThereAreMissingFiles";
            this.cbxThereAreMissingFiles.Size = new System.Drawing.Size(135, 17);
            this.cbxThereAreMissingFiles.TabIndex = 43;
            this.cbxThereAreMissingFiles.Text = "There Are Missing Files";
            this.cbxThereAreMissingFiles.UseVisualStyleBackColor = true;
            // 
            // lblFirstLowerQualityLevel
            // 
            this.lblFirstLowerQualityLevel.AutoSize = true;
            this.lblFirstLowerQualityLevel.Location = new System.Drawing.Point(484, 33);
            this.lblFirstLowerQualityLevel.Name = "lblFirstLowerQualityLevel";
            this.lblFirstLowerQualityLevel.Size = new System.Drawing.Size(125, 13);
            this.lblFirstLowerQualityLevel.TabIndex = 42;
            this.lblFirstLowerQualityLevel.Text = "First Lower Quality Level:";
            this.lblFirstLowerQualityLevel.Visible = false;
            // 
            // ntxFirstLowerQualityLevel
            // 
            this.ntxFirstLowerQualityLevel.Location = new System.Drawing.Point(618, 30);
            this.ntxFirstLowerQualityLevel.Name = "ntxFirstLowerQualityLevel";
            this.ntxFirstLowerQualityLevel.Size = new System.Drawing.Size(54, 20);
            this.ntxFirstLowerQualityLevel.TabIndex = 41;
            this.ntxFirstLowerQualityLevel.Text = "MAX";
            this.ntxFirstLowerQualityLevel.WordWrap = false;
            // 
            // cmbMapLayerType
            // 
            this.cmbMapLayerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMapLayerType.FormattingEnabled = true;
            this.cmbMapLayerType.Location = new System.Drawing.Point(71, 3);
            this.cmbMapLayerType.Name = "cmbMapLayerType";
            this.cmbMapLayerType.Size = new System.Drawing.Size(221, 21);
            this.cmbMapLayerType.TabIndex = 39;
            this.cmbMapLayerType.SelectedIndexChanged += new System.EventHandler(this.cmbMapLayerType_SelectedIndexChanged);
            // 
            // browseLayerCtrl
            // 
            this.browseLayerCtrl.AutoSize = true;
            this.browseLayerCtrl.FileName = "";
            this.browseLayerCtrl.Filter = "";
            this.browseLayerCtrl.IsFolderDialog = true;
            this.browseLayerCtrl.IsFullPath = true;
            this.browseLayerCtrl.IsSaveFile = false;
            this.browseLayerCtrl.LabelCaption = "File Name:   ";
            this.browseLayerCtrl.Location = new System.Drawing.Point(67, 30);
            this.browseLayerCtrl.Margin = new System.Windows.Forms.Padding(4);
            this.browseLayerCtrl.MinimumSize = new System.Drawing.Size(300, 24);
            this.browseLayerCtrl.MultiFilesSelect = false;
            this.browseLayerCtrl.Name = "browseLayerCtrl";
            this.browseLayerCtrl.Prefix = "";
            this.browseLayerCtrl.Size = new System.Drawing.Size(389, 24);
            this.browseLayerCtrl.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Layer Type:";
            // 
            // ntxHighestResolution
            // 
            this.ntxHighestResolution.Location = new System.Drawing.Point(486, 197);
            this.ntxHighestResolution.Name = "ntxHighestResolution";
            this.ntxHighestResolution.Size = new System.Drawing.Size(64, 20);
            this.ntxHighestResolution.TabIndex = 64;
            this.ntxHighestResolution.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(381, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Highest Resolution:";
            // 
            // gbWorldLimit
            // 
            this.gbWorldLimit.Controls.Add(this.label6);
            this.gbWorldLimit.Controls.Add(this.label7);
            this.gbWorldLimit.Controls.Add(this.ctrl3DMaxWorldBoundingBox);
            this.gbWorldLimit.Controls.Add(this.ctrl3DMinWorldBoundingBox);
            this.gbWorldLimit.Location = new System.Drawing.Point(9, 224);
            this.gbWorldLimit.Name = "gbWorldLimit";
            this.gbWorldLimit.Size = new System.Drawing.Size(296, 81);
            this.gbWorldLimit.TabIndex = 62;
            this.gbWorldLimit.TabStop = false;
            this.gbWorldLimit.Text = "Max World Limit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Min Point:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Max Point:";
            // 
            // ctrl3DMaxWorldBoundingBox
            // 
            this.ctrl3DMaxWorldBoundingBox.IsReadOnly = false;
            this.ctrl3DMaxWorldBoundingBox.Location = new System.Drawing.Point(62, 48);
            this.ctrl3DMaxWorldBoundingBox.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DMaxWorldBoundingBox.Name = "ctrl3DMaxWorldBoundingBox";
            this.ctrl3DMaxWorldBoundingBox.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DMaxWorldBoundingBox.TabIndex = 9;
            this.ctrl3DMaxWorldBoundingBox.X = 0D;
            this.ctrl3DMaxWorldBoundingBox.Y = 0D;
            this.ctrl3DMaxWorldBoundingBox.Z = 0D;
            // 
            // ctrl3DMinWorldBoundingBox
            // 
            this.ctrl3DMinWorldBoundingBox.IsReadOnly = false;
            this.ctrl3DMinWorldBoundingBox.Location = new System.Drawing.Point(62, 16);
            this.ctrl3DMinWorldBoundingBox.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DMinWorldBoundingBox.Name = "ctrl3DMinWorldBoundingBox";
            this.ctrl3DMinWorldBoundingBox.Size = new System.Drawing.Size(232, 34);
            this.ctrl3DMinWorldBoundingBox.TabIndex = 8;
            this.ctrl3DMinWorldBoundingBox.X = 0D;
            this.ctrl3DMinWorldBoundingBox.Y = 0D;
            this.ctrl3DMinWorldBoundingBox.Z = 0D;
            // 
            // tcLayerParams
            // 
            this.tcLayerParams.Controls.Add(this.tpRawParams);
            this.tcLayerParams.Controls.Add(this.tpWMSLayerParams);
            this.tcLayerParams.Location = new System.Drawing.Point(5, 157);
            this.tcLayerParams.Name = "tcLayerParams";
            this.tcLayerParams.SelectedIndex = 0;
            this.tcLayerParams.Size = new System.Drawing.Size(746, 410);
            this.tcLayerParams.TabIndex = 12;
            this.tcLayerParams.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcLayerParams_Selecting);
            // 
            // tpRawParams
            // 
            this.tpRawParams.BackColor = System.Drawing.SystemColors.Control;
            this.tpRawParams.Controls.Add(this.chxPostProcessSourceData);
            this.tpRawParams.Controls.Add(this.ntxHighestResolution);
            this.tpRawParams.Controls.Add(this.label2);
            this.tpRawParams.Controls.Add(this.gbWorldLimit);
            this.tpRawParams.Controls.Add(this.ctrlRawRasterComponentParams1);
            this.tpRawParams.Controls.Add(this.cbIgnoreRasterPalette);
            this.tpRawParams.Controls.Add(this.txtPyramidResolution);
            this.tpRawParams.Controls.Add(this.ntbMaxNumOpenFiles);
            this.tpRawParams.Controls.Add(this.label9);
            this.tpRawParams.Controls.Add(this.chxImageCoordSys);
            this.tpRawParams.Controls.Add(this.ntxFirstPyramidResolution);
            this.tpRawParams.Controls.Add(this.label23);
            this.tpRawParams.Controls.Add(this.label8);
            this.tpRawParams.Location = new System.Drawing.Point(4, 22);
            this.tpRawParams.Name = "tpRawParams";
            this.tpRawParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpRawParams.Size = new System.Drawing.Size(738, 384);
            this.tpRawParams.TabIndex = 0;
            this.tpRawParams.Text = "Raw Layer Params";
            // 
            // ctrlRawRasterComponentParams1
            // 
            this.ctrlRawRasterComponentParams1.Location = new System.Drawing.Point(3, -2);
            this.ctrlRawRasterComponentParams1.Name = "ctrlRawRasterComponentParams1";
            this.ctrlRawRasterComponentParams1.Size = new System.Drawing.Size(690, 173);
            this.ctrlRawRasterComponentParams1.TabIndex = 18;
            // 
            // cbIgnoreRasterPalette
            // 
            this.cbIgnoreRasterPalette.AutoSize = true;
            this.cbIgnoreRasterPalette.Location = new System.Drawing.Point(219, 198);
            this.cbIgnoreRasterPalette.Name = "cbIgnoreRasterPalette";
            this.cbIgnoreRasterPalette.Size = new System.Drawing.Size(126, 17);
            this.cbIgnoreRasterPalette.TabIndex = 60;
            this.cbIgnoreRasterPalette.Text = "Ignore Raster Palette";
            this.cbIgnoreRasterPalette.UseVisualStyleBackColor = true;
            // 
            // txtPyramidResolution
            // 
            this.txtPyramidResolution.Location = new System.Drawing.Point(611, 171);
            this.txtPyramidResolution.Name = "txtPyramidResolution";
            this.txtPyramidResolution.Size = new System.Drawing.Size(99, 20);
            this.txtPyramidResolution.TabIndex = 59;
            // 
            // ntbMaxNumOpenFiles
            // 
            this.ntbMaxNumOpenFiles.Location = new System.Drawing.Point(135, 197);
            this.ntbMaxNumOpenFiles.Name = "ntbMaxNumOpenFiles";
            this.ntbMaxNumOpenFiles.Size = new System.Drawing.Size(64, 20);
            this.ntbMaxNumOpenFiles.TabIndex = 35;
            this.ntbMaxNumOpenFiles.Text = "5000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(381, 174);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(227, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Pyramid Resolution (separated by whitespace):";
            // 
            // chxImageCoordSys
            // 
            this.chxImageCoordSys.AutoSize = true;
            this.chxImageCoordSys.Location = new System.Drawing.Point(219, 173);
            this.chxImageCoordSys.Name = "chxImageCoordSys";
            this.chxImageCoordSys.Size = new System.Drawing.Size(146, 17);
            this.chxImageCoordSys.TabIndex = 16;
            this.chxImageCoordSys.Text = "Image Coordinate System";
            this.chxImageCoordSys.UseVisualStyleBackColor = true;
            this.chxImageCoordSys.Visible = false;
            // 
            // ntxFirstPyramidResolution
            // 
            this.ntxFirstPyramidResolution.Location = new System.Drawing.Point(135, 171);
            this.ntxFirstPyramidResolution.Name = "ntxFirstPyramidResolution";
            this.ntxFirstPyramidResolution.Size = new System.Drawing.Size(64, 20);
            this.ntxFirstPyramidResolution.TabIndex = 57;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 200);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(108, 13);
            this.label23.TabIndex = 34;
            this.label23.Text = "Max Num Open Files:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 56;
            this.label8.Text = "First Pyramid Resolution:";
            // 
            // tpWMSLayerParams
            // 
            this.tpWMSLayerParams.BackColor = System.Drawing.SystemColors.Control;
            this.tpWMSLayerParams.Controls.Add(this.ctrlWebServiceLayerParams1);
            this.tpWMSLayerParams.Location = new System.Drawing.Point(4, 22);
            this.tpWMSLayerParams.Name = "tpWMSLayerParams";
            this.tpWMSLayerParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpWMSLayerParams.Size = new System.Drawing.Size(738, 384);
            this.tpWMSLayerParams.TabIndex = 1;
            this.tpWMSLayerParams.Text = "Web Service Layer Params";
            // 
            // ctrlWebServiceLayerParams1
            // 
            this.ctrlWebServiceLayerParams1.Location = new System.Drawing.Point(3, 3);
            this.ctrlWebServiceLayerParams1.Name = "ctrlWebServiceLayerParams1";
            this.ctrlWebServiceLayerParams1.Size = new System.Drawing.Size(696, 375);
            this.ctrlWebServiceLayerParams1.TabIndex = 0;
            // 
            // tpRawLayerParams
            // 
            this.tpRawLayerParams.BackColor = System.Drawing.SystemColors.Control;
            this.tpRawLayerParams.Controls.Add(this.ctrlNonNativeParams1);
            this.tpRawLayerParams.Controls.Add(this.tcLayerParams);
            this.tpRawLayerParams.Location = new System.Drawing.Point(4, 22);
            this.tpRawLayerParams.Name = "tpRawLayerParams";
            this.tpRawLayerParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpRawLayerParams.Size = new System.Drawing.Size(754, 573);
            this.tpRawLayerParams.TabIndex = 0;
            this.tpRawLayerParams.Text = "Raw & Web Service Layers Params";
            // 
            // ctrlNonNativeParams1
            // 
            this.ctrlNonNativeParams1.Location = new System.Drawing.Point(6, 6);
            this.ctrlNonNativeParams1.Name = "ctrlNonNativeParams1";
            this.ctrlNonNativeParams1.Size = new System.Drawing.Size(648, 145);
            this.ctrlNonNativeParams1.TabIndex = 13;
            // 
            // tcParams
            // 
            this.tcParams.Controls.Add(this.tpRawLayerParams);
            this.tcParams.Controls.Add(this.tpVectorLayerParams);
            this.tcParams.Controls.Add(this.tpVector3DExtrusionParams);
            this.tcParams.Controls.Add(this.tp3DModelParams);
            this.tcParams.Location = new System.Drawing.Point(5, 124);
            this.tcParams.Name = "tcParams";
            this.tcParams.SelectedIndex = 0;
            this.tcParams.Size = new System.Drawing.Size(762, 599);
            this.tcParams.TabIndex = 50;
            this.tcParams.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcParams_Selecting);
            // 
            // tpVectorLayerParams
            // 
            this.tpVectorLayerParams.AutoScroll = true;
            this.tpVectorLayerParams.BackColor = System.Drawing.SystemColors.Control;
            this.tpVectorLayerParams.Controls.Add(this.rawVectorParams1);
            this.tpVectorLayerParams.Location = new System.Drawing.Point(4, 22);
            this.tpVectorLayerParams.Name = "tpVectorLayerParams";
            this.tpVectorLayerParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpVectorLayerParams.Size = new System.Drawing.Size(754, 573);
            this.tpVectorLayerParams.TabIndex = 1;
            this.tpVectorLayerParams.Text = "Vector Layer Params";
            // 
            // rawVectorParams1
            // 
            this.rawVectorParams1.AutoScroll = true;
            this.rawVectorParams1.Location = new System.Drawing.Point(5, 4);
            this.rawVectorParams1.Margin = new System.Windows.Forms.Padding(2);
            this.rawVectorParams1.Name = "rawVectorParams1";
            this.rawVectorParams1.Size = new System.Drawing.Size(689, 559);
            this.rawVectorParams1.TabIndex = 70;
            this.rawVectorParams1.TargetGridCoordinateSystem = null;
            // 
            // tpVector3DExtrusionParams
            // 
            this.tpVector3DExtrusionParams.Controls.Add(this.ctrlRawVector3DExtrusionParams1);
            this.tpVector3DExtrusionParams.Controls.Add(this.ntbExtrusionHeightMaxAddition);
            this.tpVector3DExtrusionParams.Controls.Add(this.label11);
            this.tpVector3DExtrusionParams.Location = new System.Drawing.Point(4, 22);
            this.tpVector3DExtrusionParams.Margin = new System.Windows.Forms.Padding(2);
            this.tpVector3DExtrusionParams.Name = "tpVector3DExtrusionParams";
            this.tpVector3DExtrusionParams.Padding = new System.Windows.Forms.Padding(2);
            this.tpVector3DExtrusionParams.Size = new System.Drawing.Size(754, 573);
            this.tpVector3DExtrusionParams.TabIndex = 2;
            this.tpVector3DExtrusionParams.Text = "Vector 3D Extrusion Params";
            // 
            // ctrlRawVector3DExtrusionParams1
            // 
            this.ctrlRawVector3DExtrusionParams1.Location = new System.Drawing.Point(4, 33);
            this.ctrlRawVector3DExtrusionParams1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlRawVector3DExtrusionParams1.Name = "ctrlRawVector3DExtrusionParams1";
            this.ctrlRawVector3DExtrusionParams1.Size = new System.Drawing.Size(732, 512);
            this.ctrlRawVector3DExtrusionParams1.TabIndex = 75;
            // 
            // ntbExtrusionHeightMaxAddition
            // 
            this.ntbExtrusionHeightMaxAddition.Location = new System.Drawing.Point(156, 8);
            this.ntbExtrusionHeightMaxAddition.Name = "ntbExtrusionHeightMaxAddition";
            this.ntbExtrusionHeightMaxAddition.Size = new System.Drawing.Size(48, 20);
            this.ntbExtrusionHeightMaxAddition.TabIndex = 73;
            this.ntbExtrusionHeightMaxAddition.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 13);
            this.label11.TabIndex = 74;
            this.label11.Text = "Extrusion Height Max Addition:";
            // 
            // tp3DModelParams
            // 
            this.tp3DModelParams.BackColor = System.Drawing.SystemColors.Control;
            this.tp3DModelParams.Controls.Add(this.ctrlRaw3DModelParams1);
            this.tp3DModelParams.Location = new System.Drawing.Point(4, 22);
            this.tp3DModelParams.Name = "tp3DModelParams";
            this.tp3DModelParams.Padding = new System.Windows.Forms.Padding(3);
            this.tp3DModelParams.Size = new System.Drawing.Size(754, 573);
            this.tp3DModelParams.TabIndex = 3;
            this.tp3DModelParams.Text = "3D Model Params";
            // 
            // ctrlRaw3DModelParams1
            // 
            this.ctrlRaw3DModelParams1.Location = new System.Drawing.Point(6, 6);
            this.ctrlRaw3DModelParams1.Name = "ctrlRaw3DModelParams1";
            this.ctrlRaw3DModelParams1.Size = new System.Drawing.Size(742, 561);
            this.ctrlRaw3DModelParams1.TabIndex = 0;
            // 
            // btnTilingScheme1
            // 
            this.btnTilingScheme1.Location = new System.Drawing.Point(487, 101);
            this.btnTilingScheme1.Margin = new System.Windows.Forms.Padding(2);
            this.btnTilingScheme1.Name = "btnTilingScheme1";
            this.btnTilingScheme1.Size = new System.Drawing.Size(86, 24);
            this.btnTilingScheme1.TabIndex = 72;
            this.btnTilingScheme1.Text = "Tiling Scheme";
            this.btnTilingScheme1.UseVisualStyleBackColor = true;
            this.btnTilingScheme1.Click += new System.EventHandler(this.btnTilingScheme1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "File Name:";
            // 
            // chxPostProcessSourceData
            // 
            this.chxPostProcessSourceData.AutoSize = true;
            this.chxPostProcessSourceData.Location = new System.Drawing.Point(383, 240);
            this.chxPostProcessSourceData.Name = "chxPostProcessSourceData";
            this.chxPostProcessSourceData.Size = new System.Drawing.Size(151, 17);
            this.chxPostProcessSourceData.TabIndex = 72;
            this.chxPostProcessSourceData.Text = "Post Process Source Data";
            this.chxPostProcessSourceData.UseVisualStyleBackColor = true;
            // 
            // ucLayerParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTilingScheme1);
            this.Controls.Add(this.tcParams);
            this.Controls.Add(this.chxEnhanceBorderOverlap2);
            this.Controls.Add(this.ctrlLocalCacheParams);
            this.Controls.Add(this.lblNoOfLayers);
            this.Controls.Add(this.noudNoOfLayers);
            this.Controls.Add(this.ntxNumLevelsToIgnore);
            this.Controls.Add(this.lblNoOfIgnore);
            this.Controls.Add(this.cbxThereAreMissingFiles);
            this.Controls.Add(this.lblFirstLowerQualityLevel);
            this.Controls.Add(this.ntxFirstLowerQualityLevel);
            this.Controls.Add(this.cmbMapLayerType);
            this.Controls.Add(this.browseLayerCtrl);
            this.Controls.Add(this.label1);
            this.Name = "ucLayerParams";
            this.Size = new System.Drawing.Size(770, 762);
            this.Load += new System.EventHandler(this.ucLayerParams_Load);
            ((System.ComponentModel.ISupportInitialize)(this.noudNoOfLayers)).EndInit();
            this.gbWorldLimit.ResumeLayout(false);
            this.gbWorldLimit.PerformLayout();
            this.tcLayerParams.ResumeLayout(false);
            this.tpRawParams.ResumeLayout(false);
            this.tpRawParams.PerformLayout();
            this.tpWMSLayerParams.ResumeLayout(false);
            this.tpRawLayerParams.ResumeLayout(false);
            this.tcParams.ResumeLayout(false);
            this.tpVectorLayerParams.ResumeLayout(false);
            this.tpVector3DExtrusionParams.ResumeLayout(false);
            this.tpVector3DExtrusionParams.PerformLayout();
            this.tp3DModelParams.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chxEnhanceBorderOverlap2;
        private LocalCacheParams ctrlLocalCacheParams;
        private System.Windows.Forms.Label lblNoOfLayers;
        private System.Windows.Forms.NumericUpDown noudNoOfLayers;
        private NumericTextBox ntxNumLevelsToIgnore;
        private System.Windows.Forms.Label lblNoOfIgnore;
        private System.Windows.Forms.CheckBox cbxThereAreMissingFiles;
        private System.Windows.Forms.Label lblFirstLowerQualityLevel;
        private NumericTextBox ntxFirstLowerQualityLevel;
        private System.Windows.Forms.ComboBox cmbMapLayerType;
        private CtrlBrowseControl browseLayerCtrl;
        private System.Windows.Forms.Label label1;
        private NumericTextBox ntxHighestResolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbWorldLimit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Ctrl3DVector ctrl3DMaxWorldBoundingBox;
        private Ctrl3DVector ctrl3DMinWorldBoundingBox;
        private System.Windows.Forms.TabControl tcLayerParams;
        private System.Windows.Forms.TabPage tpRawParams;
        private CtrlRawRasterComponentParams ctrlRawRasterComponentParams1;
        private System.Windows.Forms.CheckBox cbIgnoreRasterPalette;
        private System.Windows.Forms.TextBox txtPyramidResolution;
        private NumericTextBox ntbMaxNumOpenFiles;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chxImageCoordSys;
        private NumericTextBox ntxFirstPyramidResolution;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabPage tpWMSLayerParams;
        private System.Windows.Forms.TabPage tpRawLayerParams;
        private System.Windows.Forms.TabControl tcParams;
        private System.Windows.Forms.TabPage tpVectorLayerParams;
        private CtrlRawVectorParams rawVectorParams1;
        private System.Windows.Forms.TabPage tpVector3DExtrusionParams;
        private CtrlRawVector3DExtrusionParams ctrlRawVector3DExtrusionParams1;
        private NumericTextBox ntbExtrusionHeightMaxAddition;
        private System.Windows.Forms.Label label11;
        private MapWorld.WebMapLayers.CtrlWebServiceLayerParams ctrlWebServiceLayerParams1;
        private MapWorld.MapUserControls.CtrlNonNativeParams ctrlNonNativeParams1;
        private System.Windows.Forms.Button btnTilingScheme1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tp3DModelParams;
        private CtrlRaw3DModelParams ctrlRaw3DModelParams1;
        private System.Windows.Forms.CheckBox chxPostProcessSourceData;
    }
}
