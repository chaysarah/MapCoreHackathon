namespace MCTester.MapWorld.WebMapLayers
{
    partial class CtrlWebServiceLayerParams
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
            this.txImageFormat = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.tcWebServiceLayerParams = new System.Windows.Forms.TabControl();
            this.tpWMSParams = new System.Windows.Forms.TabPage();
            this.cmbWMSCoordinateSystem = new System.Windows.Forms.ComboBox();
            this.tbWMSCoordinateSystem = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tbMinScale = new MCTester.Controls.NumericTextBox();
            this.ntbBlockHeight = new MCTester.Controls.NumericTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbWMSVersion = new System.Windows.Forms.TextBox();
            this.ntbBlockWidth = new MCTester.Controls.NumericTextBox();
            this.tpWMTSParams = new System.Windows.Forms.TabPage();
            this.chxUsedServerTilingScheme = new System.Windows.Forms.CheckBox();
            this.cmbTileMatrixSet = new System.Windows.Forms.ComboBox();
            this.cbCapabilitiesBoundingBoxAxesOrder = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbInfoFormat = new System.Windows.Forms.TextBox();
            this.cbUseServerTilingScheme = new System.Windows.Forms.CheckBox();
            this.tbTileMatrixSet = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.bExtendBeyondDateLine = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tpWCSParams = new System.Windows.Forms.TabPage();
            this.label31 = new System.Windows.Forms.Label();
            this.tbWCSVersion = new System.Windows.Forms.TextBox();
            this.chxDontUseServerInterpolation = new System.Windows.Forms.CheckBox();
            this.tpCSWParams = new System.Windows.Forms.TabPage();
            this.lblFirstLowerQualityLevel = new System.Windows.Forms.Label();
            this.ntxTargetHighestResolution = new MCTester.Controls.NumericTextBox();
            this.chxOrthometricHeights = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbWMSTypes = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbStylesList = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tbZeroBlockHttpCodes = new System.Windows.Forms.TextBox();
            this.tbServerURL = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbOptionalUserAndPassword = new System.Windows.Forms.TextBox();
            this.bZeroBlockOnServerException = new System.Windows.Forms.CheckBox();
            this.tbLayers = new System.Windows.Forms.TextBox();
            this.chxTransparent = new System.Windows.Forms.CheckBox();
            this.chxSkipSSLCertificateVerification = new System.Windows.Forms.CheckBox();
            this.ntbTimeoutInSec = new MCTester.Controls.NumericTextBox();
            this.ctrlBoundingBox = new MCTester.Controls.CtrlSMcBox();
            this.cmbImageFormat = new System.Windows.Forms.ComboBox();
            this.btnSelectStyle = new System.Windows.Forms.Button();
            this.btnRequestParams = new System.Windows.Forms.Button();
            this.lblBBConverted = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tcWebServiceLayerParams.SuspendLayout();
            this.tpWMSParams.SuspendLayout();
            this.tpWMTSParams.SuspendLayout();
            this.tpWCSParams.SuspendLayout();
            this.tpCSWParams.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txImageFormat
            // 
            this.txImageFormat.Location = new System.Drawing.Point(576, 3);
            this.txImageFormat.Name = "txImageFormat";
            this.txImageFormat.Size = new System.Drawing.Size(108, 20);
            this.txImageFormat.TabIndex = 54;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(505, 7);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(71, 13);
            this.label30.TabIndex = 53;
            this.label30.Text = "Image Format";
            // 
            // tcWebServiceLayerParams
            // 
            this.tcWebServiceLayerParams.Controls.Add(this.tpWMSParams);
            this.tcWebServiceLayerParams.Controls.Add(this.tpWMTSParams);
            this.tcWebServiceLayerParams.Controls.Add(this.tpWCSParams);
            this.tcWebServiceLayerParams.Controls.Add(this.tpCSWParams);
            this.tcWebServiceLayerParams.Location = new System.Drawing.Point(2, 268);
            this.tcWebServiceLayerParams.Margin = new System.Windows.Forms.Padding(2);
            this.tcWebServiceLayerParams.Name = "tcWebServiceLayerParams";
            this.tcWebServiceLayerParams.SelectedIndex = 0;
            this.tcWebServiceLayerParams.Size = new System.Drawing.Size(687, 105);
            this.tcWebServiceLayerParams.TabIndex = 45;
            this.tcWebServiceLayerParams.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcWebServiceLayerParams_Selecting);
            // 
            // tpWMSParams
            // 
            this.tpWMSParams.Controls.Add(this.cmbWMSCoordinateSystem);
            this.tpWMSParams.Controls.Add(this.tbWMSCoordinateSystem);
            this.tpWMSParams.Controls.Add(this.label14);
            this.tpWMSParams.Controls.Add(this.label25);
            this.tpWMSParams.Controls.Add(this.label18);
            this.tpWMSParams.Controls.Add(this.tbMinScale);
            this.tpWMSParams.Controls.Add(this.ntbBlockHeight);
            this.tpWMSParams.Controls.Add(this.label21);
            this.tpWMSParams.Controls.Add(this.label10);
            this.tpWMSParams.Controls.Add(this.tbWMSVersion);
            this.tpWMSParams.Controls.Add(this.ntbBlockWidth);
            this.tpWMSParams.Location = new System.Drawing.Point(4, 22);
            this.tpWMSParams.Margin = new System.Windows.Forms.Padding(2);
            this.tpWMSParams.Name = "tpWMSParams";
            this.tpWMSParams.Padding = new System.Windows.Forms.Padding(2);
            this.tpWMSParams.Size = new System.Drawing.Size(679, 79);
            this.tpWMSParams.TabIndex = 0;
            this.tpWMSParams.Text = "WMS Params";
            this.tpWMSParams.UseVisualStyleBackColor = true;
            // 
            // cmbWMSCoordinateSystem
            // 
            this.cmbWMSCoordinateSystem.FormattingEnabled = true;
            this.cmbWMSCoordinateSystem.Location = new System.Drawing.Point(213, 33);
            this.cmbWMSCoordinateSystem.Name = "cmbWMSCoordinateSystem";
            this.cmbWMSCoordinateSystem.Size = new System.Drawing.Size(191, 21);
            this.cmbWMSCoordinateSystem.TabIndex = 36;
            this.cmbWMSCoordinateSystem.Visible = false;
            this.cmbWMSCoordinateSystem.SelectedIndexChanged += new System.EventHandler(this.cmbWMSCoordinateSystem_SelectedIndexChanged);
            this.cmbWMSCoordinateSystem.TextUpdate += new System.EventHandler(this.cmbWMSCoordinateSystem_TextUpdate);
            // 
            // tbWMSCoordinateSystem
            // 
            this.tbWMSCoordinateSystem.Location = new System.Drawing.Point(213, 33);
            this.tbWMSCoordinateSystem.Name = "tbWMSCoordinateSystem";
            this.tbWMSCoordinateSystem.Size = new System.Drawing.Size(191, 20);
            this.tbWMSCoordinateSystem.TabIndex = 13;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "Block Width";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(5, 37);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(194, 13);
            this.label25.TabIndex = 12;
            this.label25.Text = "WMS Coordinate System (EPSG:XXXX)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(140, 11);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "Block Height";
            // 
            // tbMinScale
            // 
            this.tbMinScale.Location = new System.Drawing.Point(347, 8);
            this.tbMinScale.Name = "tbMinScale";
            this.tbMinScale.Size = new System.Drawing.Size(57, 20);
            this.tbMinScale.TabIndex = 20;
            // 
            // ntbBlockHeight
            // 
            this.ntbBlockHeight.Location = new System.Drawing.Point(213, 8);
            this.ntbBlockHeight.Name = "ntbBlockHeight";
            this.ntbBlockHeight.Size = new System.Drawing.Size(57, 20);
            this.ntbBlockHeight.TabIndex = 16;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(287, 11);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 19;
            this.label21.Text = "Min Scale";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(417, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "WMS Version ";
            // 
            // tbWMSVersion
            // 
            this.tbWMSVersion.Location = new System.Drawing.Point(498, 8);
            this.tbWMSVersion.Name = "tbWMSVersion";
            this.tbWMSVersion.Size = new System.Drawing.Size(57, 20);
            this.tbWMSVersion.TabIndex = 7;
            // 
            // ntbBlockWidth
            // 
            this.ntbBlockWidth.Location = new System.Drawing.Point(76, 8);
            this.ntbBlockWidth.Name = "ntbBlockWidth";
            this.ntbBlockWidth.Size = new System.Drawing.Size(57, 20);
            this.ntbBlockWidth.TabIndex = 15;
            // 
            // tpWMTSParams
            // 
            this.tpWMTSParams.Controls.Add(this.chxUsedServerTilingScheme);
            this.tpWMTSParams.Controls.Add(this.cmbTileMatrixSet);
            this.tpWMTSParams.Controls.Add(this.cbCapabilitiesBoundingBoxAxesOrder);
            this.tpWMTSParams.Controls.Add(this.label22);
            this.tpWMTSParams.Controls.Add(this.tbInfoFormat);
            this.tpWMTSParams.Controls.Add(this.cbUseServerTilingScheme);
            this.tpWMTSParams.Controls.Add(this.tbTileMatrixSet);
            this.tpWMTSParams.Controls.Add(this.label29);
            this.tpWMTSParams.Controls.Add(this.bExtendBeyondDateLine);
            this.tpWMTSParams.Controls.Add(this.label16);
            this.tpWMTSParams.Location = new System.Drawing.Point(4, 22);
            this.tpWMTSParams.Margin = new System.Windows.Forms.Padding(2);
            this.tpWMTSParams.Name = "tpWMTSParams";
            this.tpWMTSParams.Padding = new System.Windows.Forms.Padding(2);
            this.tpWMTSParams.Size = new System.Drawing.Size(679, 79);
            this.tpWMTSParams.TabIndex = 1;
            this.tpWMTSParams.Text = "WMTS Params";
            this.tpWMTSParams.UseVisualStyleBackColor = true;
            // 
            // chxUsedServerTilingScheme
            // 
            this.chxUsedServerTilingScheme.AutoSize = true;
            this.chxUsedServerTilingScheme.Checked = true;
            this.chxUsedServerTilingScheme.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.chxUsedServerTilingScheme.Enabled = false;
            this.chxUsedServerTilingScheme.Location = new System.Drawing.Point(303, 60);
            this.chxUsedServerTilingScheme.Name = "chxUsedServerTilingScheme";
            this.chxUsedServerTilingScheme.Size = new System.Drawing.Size(206, 17);
            this.chxUsedServerTilingScheme.TabIndex = 36;
            this.chxUsedServerTilingScheme.Text = "Is Server Tiling Scheme Actually Used";
            this.chxUsedServerTilingScheme.UseVisualStyleBackColor = true;
            this.chxUsedServerTilingScheme.Visible = false;
            // 
            // cmbTileMatrixSet
            // 
            this.cmbTileMatrixSet.FormattingEnabled = true;
            this.cmbTileMatrixSet.Location = new System.Drawing.Point(90, 33);
            this.cmbTileMatrixSet.Name = "cmbTileMatrixSet";
            this.cmbTileMatrixSet.Size = new System.Drawing.Size(187, 21);
            this.cmbTileMatrixSet.TabIndex = 35;
            this.cmbTileMatrixSet.Visible = false;
            this.cmbTileMatrixSet.SelectedIndexChanged += new System.EventHandler(this.cmbTileMatrixSet_SelectedIndexChanged);
            // 
            // cbCapabilitiesBoundingBoxAxesOrder
            // 
            this.cbCapabilitiesBoundingBoxAxesOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCapabilitiesBoundingBoxAxesOrder.FormattingEnabled = true;
            this.cbCapabilitiesBoundingBoxAxesOrder.Location = new System.Drawing.Point(520, 9);
            this.cbCapabilitiesBoundingBoxAxesOrder.Name = "cbCapabilitiesBoundingBoxAxesOrder";
            this.cbCapabilitiesBoundingBoxAxesOrder.Size = new System.Drawing.Size(141, 21);
            this.cbCapabilitiesBoundingBoxAxesOrder.TabIndex = 13;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(5, 9);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "Info Format";
            // 
            // tbInfoFormat
            // 
            this.tbInfoFormat.Location = new System.Drawing.Point(90, 6);
            this.tbInfoFormat.Name = "tbInfoFormat";
            this.tbInfoFormat.Size = new System.Drawing.Size(187, 20);
            this.tbInfoFormat.TabIndex = 1;
            // 
            // cbUseServerTilingScheme
            // 
            this.cbUseServerTilingScheme.AutoSize = true;
            this.cbUseServerTilingScheme.Location = new System.Drawing.Point(302, 37);
            this.cbUseServerTilingScheme.Name = "cbUseServerTilingScheme";
            this.cbUseServerTilingScheme.Size = new System.Drawing.Size(149, 17);
            this.cbUseServerTilingScheme.TabIndex = 34;
            this.cbUseServerTilingScheme.Text = "Use Server Tiling Scheme";
            this.cbUseServerTilingScheme.UseVisualStyleBackColor = true;
            // 
            // tbTileMatrixSet
            // 
            this.tbTileMatrixSet.Location = new System.Drawing.Point(90, 34);
            this.tbTileMatrixSet.Name = "tbTileMatrixSet";
            this.tbTileMatrixSet.Size = new System.Drawing.Size(187, 20);
            this.tbTileMatrixSet.TabIndex = 11;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(299, 14);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(184, 13);
            this.label29.TabIndex = 12;
            this.label29.Text = "Capabilities Bounding Box Axes Order";
            // 
            // bExtendBeyondDateLine
            // 
            this.bExtendBeyondDateLine.AutoSize = true;
            this.bExtendBeyondDateLine.Location = new System.Drawing.Point(520, 37);
            this.bExtendBeyondDateLine.Name = "bExtendBeyondDateLine";
            this.bExtendBeyondDateLine.Size = new System.Drawing.Size(147, 17);
            this.bExtendBeyondDateLine.TabIndex = 2;
            this.bExtendBeyondDateLine.Text = "Extend Beyond Date Line";
            this.bExtendBeyondDateLine.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 37);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "Tile Matrix Set";
            // 
            // tpWCSParams
            // 
            this.tpWCSParams.Controls.Add(this.label31);
            this.tpWCSParams.Controls.Add(this.tbWCSVersion);
            this.tpWCSParams.Controls.Add(this.chxDontUseServerInterpolation);
            this.tpWCSParams.Location = new System.Drawing.Point(4, 22);
            this.tpWCSParams.Margin = new System.Windows.Forms.Padding(2);
            this.tpWCSParams.Name = "tpWCSParams";
            this.tpWCSParams.Padding = new System.Windows.Forms.Padding(2);
            this.tpWCSParams.Size = new System.Drawing.Size(679, 79);
            this.tpWCSParams.TabIndex = 2;
            this.tpWCSParams.Text = "WCS Params";
            this.tpWCSParams.UseVisualStyleBackColor = true;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(5, 15);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(73, 13);
            this.label31.TabIndex = 8;
            this.label31.Text = "WCS Version ";
            // 
            // tbWCSVersion
            // 
            this.tbWCSVersion.Location = new System.Drawing.Point(85, 12);
            this.tbWCSVersion.Name = "tbWCSVersion";
            this.tbWCSVersion.Size = new System.Drawing.Size(57, 20);
            this.tbWCSVersion.TabIndex = 9;
            // 
            // chxDontUseServerInterpolation
            // 
            this.chxDontUseServerInterpolation.AutoSize = true;
            this.chxDontUseServerInterpolation.Location = new System.Drawing.Point(264, 14);
            this.chxDontUseServerInterpolation.Name = "chxDontUseServerInterpolation";
            this.chxDontUseServerInterpolation.Size = new System.Drawing.Size(166, 17);
            this.chxDontUseServerInterpolation.TabIndex = 2;
            this.chxDontUseServerInterpolation.Text = "Dont Use Server Interpolation";
            this.chxDontUseServerInterpolation.UseVisualStyleBackColor = true;
            // 
            // tpCSWParams
            // 
            this.tpCSWParams.Controls.Add(this.lblFirstLowerQualityLevel);
            this.tpCSWParams.Controls.Add(this.ntxTargetHighestResolution);
            this.tpCSWParams.Controls.Add(this.chxOrthometricHeights);
            this.tpCSWParams.Location = new System.Drawing.Point(4, 22);
            this.tpCSWParams.Name = "tpCSWParams";
            this.tpCSWParams.Padding = new System.Windows.Forms.Padding(3);
            this.tpCSWParams.Size = new System.Drawing.Size(679, 79);
            this.tpCSWParams.TabIndex = 3;
            this.tpCSWParams.Text = "CSW Params";
            this.tpCSWParams.UseVisualStyleBackColor = true;
            // 
            // lblFirstLowerQualityLevel
            // 
            this.lblFirstLowerQualityLevel.AutoSize = true;
            this.lblFirstLowerQualityLevel.Location = new System.Drawing.Point(16, 48);
            this.lblFirstLowerQualityLevel.Name = "lblFirstLowerQualityLevel";
            this.lblFirstLowerQualityLevel.Size = new System.Drawing.Size(133, 13);
            this.lblFirstLowerQualityLevel.TabIndex = 93;
            this.lblFirstLowerQualityLevel.Text = "Target Highest Resolution:";
            // 
            // ntxTargetHighestResolution
            // 
            this.ntxTargetHighestResolution.Location = new System.Drawing.Point(168, 45);
            this.ntxTargetHighestResolution.Name = "ntxTargetHighestResolution";
            this.ntxTargetHighestResolution.Size = new System.Drawing.Size(54, 20);
            this.ntxTargetHighestResolution.TabIndex = 92;
            this.ntxTargetHighestResolution.WordWrap = false;
            // 
            // chxOrthometricHeights
            // 
            this.chxOrthometricHeights.AutoSize = true;
            this.chxOrthometricHeights.Location = new System.Drawing.Point(19, 15);
            this.chxOrthometricHeights.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.chxOrthometricHeights.Name = "chxOrthometricHeights";
            this.chxOrthometricHeights.Size = new System.Drawing.Size(119, 17);
            this.chxOrthometricHeights.TabIndex = 89;
            this.chxOrthometricHeights.Text = "Orthometric Heights";
            this.chxOrthometricHeights.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(2, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(123, 13);
            this.label26.TabIndex = 52;
            this.label26.Text = "Web Map Service Type:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1, 83);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 13);
            this.label17.TabIndex = 37;
            this.label17.Text = "Server URL ";
            // 
            // cbWMSTypes
            // 
            this.cbWMSTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWMSTypes.Enabled = false;
            this.cbWMSTypes.FormattingEnabled = true;
            this.cbWMSTypes.Location = new System.Drawing.Point(160, 3);
            this.cbWMSTypes.Name = "cbWMSTypes";
            this.cbWMSTypes.Size = new System.Drawing.Size(146, 21);
            this.cbWMSTypes.TabIndex = 51;
            this.cbWMSTypes.SelectedIndexChanged += new System.EventHandler(this.cbWMSTypes_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(262, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Optional User And Password (in user:password format)";
            // 
            // tbStylesList
            // 
            this.tbStylesList.Location = new System.Drawing.Point(159, 30);
            this.tbStylesList.Name = "tbStylesList";
            this.tbStylesList.Size = new System.Drawing.Size(257, 20);
            this.tbStylesList.TabIndex = 50;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(152, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "Layers (Seperated by commas)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(2, 33);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(149, 13);
            this.label20.TabIndex = 49;
            this.label20.Text = "Styles (Seperated by commas)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 13);
            this.label15.TabIndex = 36;
            this.label15.Text = "Timeout In Sec ";
            // 
            // tbZeroBlockHttpCodes
            // 
            this.tbZeroBlockHttpCodes.Location = new System.Drawing.Point(422, 4);
            this.tbZeroBlockHttpCodes.Name = "tbZeroBlockHttpCodes";
            this.tbZeroBlockHttpCodes.Size = new System.Drawing.Size(72, 20);
            this.tbZeroBlockHttpCodes.TabIndex = 48;
            // 
            // tbServerURL
            // 
            this.tbServerURL.Location = new System.Drawing.Point(159, 80);
            this.tbServerURL.Name = "tbServerURL";
            this.tbServerURL.Size = new System.Drawing.Size(335, 20);
            this.tbServerURL.TabIndex = 38;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(271, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(115, 13);
            this.label19.TabIndex = 47;
            this.label19.Text = "Zero Block Http Codes";
            // 
            // tbOptionalUserAndPassword
            // 
            this.tbOptionalUserAndPassword.Location = new System.Drawing.Point(270, 105);
            this.tbOptionalUserAndPassword.Name = "tbOptionalUserAndPassword";
            this.tbOptionalUserAndPassword.Size = new System.Drawing.Size(224, 20);
            this.tbOptionalUserAndPassword.TabIndex = 39;
            // 
            // bZeroBlockOnServerException
            // 
            this.bZeroBlockOnServerException.AutoSize = true;
            this.bZeroBlockOnServerException.Location = new System.Drawing.Point(507, 56);
            this.bZeroBlockOnServerException.Name = "bZeroBlockOnServerException";
            this.bZeroBlockOnServerException.Size = new System.Drawing.Size(179, 17);
            this.bZeroBlockOnServerException.TabIndex = 46;
            this.bZeroBlockOnServerException.Text = "Zero Block On Server Exception";
            this.bZeroBlockOnServerException.UseVisualStyleBackColor = true;
            // 
            // tbLayers
            // 
            this.tbLayers.Location = new System.Drawing.Point(159, 54);
            this.tbLayers.Name = "tbLayers";
            this.tbLayers.Size = new System.Drawing.Size(335, 20);
            this.tbLayers.TabIndex = 40;
            // 
            // chxTransparent
            // 
            this.chxTransparent.AutoSize = true;
            this.chxTransparent.Location = new System.Drawing.Point(507, 33);
            this.chxTransparent.Name = "chxTransparent";
            this.chxTransparent.Size = new System.Drawing.Size(86, 17);
            this.chxTransparent.TabIndex = 44;
            this.chxTransparent.Text = "Transparent ";
            this.chxTransparent.UseVisualStyleBackColor = true;
            // 
            // chxSkipSSLCertificateVerification
            // 
            this.chxSkipSSLCertificateVerification.AutoSize = true;
            this.chxSkipSSLCertificateVerification.Location = new System.Drawing.Point(507, 78);
            this.chxSkipSSLCertificateVerification.Name = "chxSkipSSLCertificateVerification";
            this.chxSkipSSLCertificateVerification.Size = new System.Drawing.Size(178, 17);
            this.chxSkipSSLCertificateVerification.TabIndex = 41;
            this.chxSkipSSLCertificateVerification.Text = "Skip SSL Certificate Verification ";
            this.chxSkipSSLCertificateVerification.UseVisualStyleBackColor = true;
            // 
            // ntbTimeoutInSec
            // 
            this.ntbTimeoutInSec.Location = new System.Drawing.Point(159, 3);
            this.ntbTimeoutInSec.Name = "ntbTimeoutInSec";
            this.ntbTimeoutInSec.Size = new System.Drawing.Size(83, 20);
            this.ntbTimeoutInSec.TabIndex = 42;
            // 
            // ctrlBoundingBox
            // 
            this.ctrlBoundingBox.GroupBoxText = "Bounding Box";
            this.ctrlBoundingBox.Location = new System.Drawing.Point(4, 166);
            this.ctrlBoundingBox.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBoundingBox.Name = "ctrlBoundingBox";
            this.ctrlBoundingBox.Size = new System.Drawing.Size(302, 100);
            this.ctrlBoundingBox.TabIndex = 55;
            // 
            // cmbImageFormat
            // 
            this.cmbImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageFormat.FormattingEnabled = true;
            this.cmbImageFormat.Location = new System.Drawing.Point(576, 3);
            this.cmbImageFormat.Name = "cmbImageFormat";
            this.cmbImageFormat.Size = new System.Drawing.Size(108, 21);
            this.cmbImageFormat.TabIndex = 36;
            this.cmbImageFormat.Visible = false;
            this.cmbImageFormat.VisibleChanged += new System.EventHandler(this.cmbImageFormat_VisibleChanged);
            // 
            // btnSelectStyle
            // 
            this.btnSelectStyle.Enabled = false;
            this.btnSelectStyle.Location = new System.Drawing.Point(422, 28);
            this.btnSelectStyle.Name = "btnSelectStyle";
            this.btnSelectStyle.Size = new System.Drawing.Size(74, 23);
            this.btnSelectStyle.TabIndex = 56;
            this.btnSelectStyle.Text = "Select Style";
            this.btnSelectStyle.UseVisualStyleBackColor = true;
            this.btnSelectStyle.Click += new System.EventHandler(this.btnSelectStyle_Click);
            // 
            // btnRequestParams
            // 
            this.btnRequestParams.Location = new System.Drawing.Point(536, 214);
            this.btnRequestParams.Name = "btnRequestParams";
            this.btnRequestParams.Size = new System.Drawing.Size(131, 23);
            this.btnRequestParams.TabIndex = 57;
            this.btnRequestParams.Text = "Server Request Params";
            this.btnRequestParams.UseVisualStyleBackColor = true;
            this.btnRequestParams.Click += new System.EventHandler(this.btnRequestParams_Click);
            // 
            // lblBBConverted
            // 
            this.lblBBConverted.AutoSize = true;
            this.lblBBConverted.ForeColor = System.Drawing.Color.Red;
            this.lblBBConverted.Location = new System.Drawing.Point(10, 249);
            this.lblBBConverted.Name = "lblBBConverted";
            this.lblBBConverted.Size = new System.Drawing.Size(175, 13);
            this.lblBBConverted.TabIndex = 58;
            this.lblBBConverted.Text = "Bounding Box cannot be converted";
            this.lblBBConverted.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbOptionalUserAndPassword);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.ntbTimeoutInSec);
            this.panel1.Controls.Add(this.chxSkipSSLCertificateVerification);
            this.panel1.Controls.Add(this.chxTransparent);
            this.panel1.Controls.Add(this.tbLayers);
            this.panel1.Controls.Add(this.btnSelectStyle);
            this.panel1.Controls.Add(this.bZeroBlockOnServerException);
            this.panel1.Controls.Add(this.cmbImageFormat);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.tbServerURL);
            this.panel1.Controls.Add(this.txImageFormat);
            this.panel1.Controls.Add(this.tbZeroBlockHttpCodes);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.tbStylesList);
            this.panel1.Location = new System.Drawing.Point(1, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 130);
            this.panel1.TabIndex = 59;
            // 
            // CtrlWebServiceLayerParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label26);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbWMSTypes);
            this.Controls.Add(this.lblBBConverted);
            this.Controls.Add(this.btnRequestParams);
            this.Controls.Add(this.ctrlBoundingBox);
            this.Controls.Add(this.tcWebServiceLayerParams);
            this.Name = "CtrlWebServiceLayerParams";
            this.Size = new System.Drawing.Size(691, 375);
            this.tcWebServiceLayerParams.ResumeLayout(false);
            this.tpWMSParams.ResumeLayout(false);
            this.tpWMSParams.PerformLayout();
            this.tpWMTSParams.ResumeLayout(false);
            this.tpWMTSParams.PerformLayout();
            this.tpWCSParams.ResumeLayout(false);
            this.tpWCSParams.PerformLayout();
            this.tpCSWParams.ResumeLayout(false);
            this.tpCSWParams.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txImageFormat;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TabControl tcWebServiceLayerParams;
        private System.Windows.Forms.TabPage tpWMSParams;
        private System.Windows.Forms.TextBox tbWMSCoordinateSystem;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label18;
        private Controls.NumericTextBox tbMinScale;
        private Controls.NumericTextBox ntbBlockHeight;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbWMSVersion;
        private Controls.NumericTextBox ntbBlockWidth;
        private System.Windows.Forms.TabPage tpWMTSParams;
        private System.Windows.Forms.ComboBox cbCapabilitiesBoundingBoxAxesOrder;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbInfoFormat;
        private System.Windows.Forms.CheckBox cbUseServerTilingScheme;
        private System.Windows.Forms.TextBox tbTileMatrixSet;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.CheckBox bExtendBeyondDateLine;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TabPage tpWCSParams;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox tbWCSVersion;
        private System.Windows.Forms.CheckBox chxDontUseServerInterpolation;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbWMSTypes;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbStylesList;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbZeroBlockHttpCodes;
        private System.Windows.Forms.TextBox tbServerURL;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tbOptionalUserAndPassword;
        private System.Windows.Forms.CheckBox bZeroBlockOnServerException;
        private System.Windows.Forms.TextBox tbLayers;
        private System.Windows.Forms.CheckBox chxTransparent;
        private System.Windows.Forms.CheckBox chxSkipSSLCertificateVerification;
        private Controls.NumericTextBox ntbTimeoutInSec;
        private Controls.CtrlSMcBox ctrlBoundingBox;
        private System.Windows.Forms.ComboBox cmbTileMatrixSet;
        private System.Windows.Forms.ComboBox cmbImageFormat;
        private System.Windows.Forms.Button btnSelectStyle;
        private System.Windows.Forms.Button btnRequestParams;
        private System.Windows.Forms.ComboBox cmbWMSCoordinateSystem;
        private System.Windows.Forms.Label lblBBConverted;
        private System.Windows.Forms.CheckBox chxUsedServerTilingScheme;
        private System.Windows.Forms.TabPage tpCSWParams;
        private System.Windows.Forms.CheckBox chxOrthometricHeights;
        private System.Windows.Forms.Label lblFirstLowerQualityLevel;
        private Controls.NumericTextBox ntxTargetHighestResolution;
        private System.Windows.Forms.Panel panel1;
    }
}
