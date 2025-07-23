namespace MCTester.ButtonsImplementation
{
    partial class btnFileProductionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(btnFileProductionForm));
            this.tcFileProductions = new System.Windows.Forms.TabControl();
            this.tpGetFileParameters = new System.Windows.Forms.TabPage();
            this.btnGetFileParameters = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxStripHeight = new MCTester.Controls.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ntxTileHeight = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ntxTileWidth = new MCTester.Controls.NumericTextBox();
            this.cbxIsTiled = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxImageHeight = new MCTester.Controls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxImageWidth = new MCTester.Controls.NumericTextBox();
            this.ctrlBrowseSourceFileFullPath = new MCTester.Controls.CtrlBrowseControl();
            this.tpDTMFromPointsCloud = new System.Windows.Forms.TabPage();
            this.label30 = new System.Windows.Forms.Label();
            this.btnDrawPtCloudArea = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.ntxNumPtToCastLots = new MCTester.Controls.NumericTextBox();
            this.btnGenerateDTMFromPointsCloud = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.ntxMinCloudPointsForHeightCalculation = new MCTester.Controls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ntxMaxSearchRadiusForHeightCalculation = new MCTester.Controls.NumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ctrl2DVectorDTMMaxPoint = new MCTester.Controls.Ctrl2DVector();
            this.ctrl2DVectorDTMMinPoint = new MCTester.Controls.Ctrl2DVector();
            this.label6 = new System.Windows.Forms.Label();
            this.ntxDTMResolution = new MCTester.Controls.NumericTextBox();
            this.ctrlBrowseDTMPath = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlSampleCloudPoints = new MCTester.Controls.CtrlSamplePoint();
            this.dgvCloudPoints = new System.Windows.Forms.DataGridView();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsePoint = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tpMosaicFromOrthophotos = new System.Windows.Forms.TabPage();
            this.btnGenerateMosaic = new System.Windows.Forms.Button();
            this.ntxCompressionFactor = new MCTester.Controls.NumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ctrlBrowseControlMosaicTfwFile = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlBrowseMosaicTifFile = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlGridCoordinateSystemMosaicWorldParams = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.ntxMosaicBackgroundGrayLevel = new MCTester.Controls.NumericTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ntxMosaicTileSize = new MCTester.Controls.NumericTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.gbGeoReferencingParams = new System.Windows.Forms.GroupBox();
            this.ctrlSampleMosaicFromOrthophotosSecondCorner = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlSampleMosaicFromOrthophotosFirstCorner = new MCTester.Controls.CtrlSamplePoint();
            this.ntxGSD = new MCTester.Controls.NumericTextBox();
            this.ntxAzimDeg = new MCTester.Controls.NumericTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ctrl3DVectorSecondCornerInDiagonal = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DVectorFirstCornerInDiagonal = new MCTester.Controls.Ctrl3DVector();
            this.cmbSourceResamplingMethod = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.ctrlBrowseSourceOrthophotoTfwFiles = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlBrowseSourceOrthophotoTifFiles = new MCTester.Controls.CtrlBrowseControl();
            this.tpOrthophotoFromLoropImage = new System.Windows.Forms.TabPage();
            this.cbxDTMMapLayer = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnAddDTMMapLayer = new System.Windows.Forms.Button();
            this.ntxOrthophotoCompressionFactor = new MCTester.Controls.NumericTextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.btnGenerateOrthophoto = new System.Windows.Forms.Button();
            this.ctrlBrowseOrthophotoTfwFile = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlBrowseOrthophotoTifFile = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlGridCoordinateSystemOrthophoto = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.ntxOrthophotoDefaultHeight = new MCTester.Controls.NumericTextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.ntxOrthophotoBackgroundGrayLevel = new MCTester.Controls.NumericTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.ntxOrthopothoTileSize = new MCTester.Controls.NumericTextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ctrlSampleOrthophotoFromLoropSecondCorner = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlSampleOrthophotoFromLoropFirstCorner = new MCTester.Controls.CtrlSamplePoint();
            this.ntOrthophotoGSD = new MCTester.Controls.NumericTextBox();
            this.ntxOrthophotoAzimDeg = new MCTester.Controls.NumericTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal = new MCTester.Controls.Ctrl3DVector();
            this.cmbOrthophotoSourceResamplingMethod = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.ctrlImageCalcSourceLorop = new MCTester.Controls.CtrlImageCalc();
            this.ctrlBrowseSourceLoropTifFile = new MCTester.Controls.CtrlBrowseControl();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.tcFileProductions.SuspendLayout();
            this.tpGetFileParameters.SuspendLayout();
            this.tpDTMFromPointsCloud.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCloudPoints)).BeginInit();
            this.tpMosaicFromOrthophotos.SuspendLayout();
            this.gbGeoReferencingParams.SuspendLayout();
            this.tpOrthophotoFromLoropImage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcFileProductions
            // 
            this.tcFileProductions.Controls.Add(this.tpGetFileParameters);
            this.tcFileProductions.Controls.Add(this.tpDTMFromPointsCloud);
            this.tcFileProductions.Controls.Add(this.tpMosaicFromOrthophotos);
            this.tcFileProductions.Controls.Add(this.tpOrthophotoFromLoropImage);
            this.tcFileProductions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFileProductions.Location = new System.Drawing.Point(0, 0);
            this.tcFileProductions.Name = "tcFileProductions";
            this.tcFileProductions.SelectedIndex = 0;
            this.tcFileProductions.Size = new System.Drawing.Size(793, 617);
            this.tcFileProductions.TabIndex = 0;
            // 
            // tpGetFileParameters
            // 
            this.tpGetFileParameters.Controls.Add(this.label31);
            this.tpGetFileParameters.Controls.Add(this.btnGetFileParameters);
            this.tpGetFileParameters.Controls.Add(this.label3);
            this.tpGetFileParameters.Controls.Add(this.ntxStripHeight);
            this.tpGetFileParameters.Controls.Add(this.label5);
            this.tpGetFileParameters.Controls.Add(this.ntxTileHeight);
            this.tpGetFileParameters.Controls.Add(this.label4);
            this.tpGetFileParameters.Controls.Add(this.ntxTileWidth);
            this.tpGetFileParameters.Controls.Add(this.cbxIsTiled);
            this.tpGetFileParameters.Controls.Add(this.label2);
            this.tpGetFileParameters.Controls.Add(this.ntxImageHeight);
            this.tpGetFileParameters.Controls.Add(this.label1);
            this.tpGetFileParameters.Controls.Add(this.ntxImageWidth);
            this.tpGetFileParameters.Controls.Add(this.ctrlBrowseSourceFileFullPath);
            this.tpGetFileParameters.Location = new System.Drawing.Point(4, 22);
            this.tpGetFileParameters.Name = "tpGetFileParameters";
            this.tpGetFileParameters.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpGetFileParameters.Size = new System.Drawing.Size(785, 591);
            this.tpGetFileParameters.TabIndex = 0;
            this.tpGetFileParameters.Text = "File Parameters";
            this.tpGetFileParameters.UseVisualStyleBackColor = true;
            this.tpGetFileParameters.Click += new System.EventHandler(this.tpGetFileParameters_Click);
            // 
            // btnGetFileParameters
            // 
            this.btnGetFileParameters.Location = new System.Drawing.Point(620, 560);
            this.btnGetFileParameters.Name = "btnGetFileParameters";
            this.btnGetFileParameters.Size = new System.Drawing.Size(116, 23);
            this.btnGetFileParameters.TabIndex = 13;
            this.btnGetFileParameters.Text = "Get File Parameters";
            this.btnGetFileParameters.UseVisualStyleBackColor = true;
            this.btnGetFileParameters.Click += new System.EventHandler(this.btnGetFileParameters_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Strip Height:";
            // 
            // ntxStripHeight
            // 
            this.ntxStripHeight.Enabled = false;
            this.ntxStripHeight.Location = new System.Drawing.Point(84, 185);
            this.ntxStripHeight.Name = "ntxStripHeight";
            this.ntxStripHeight.Size = new System.Drawing.Size(100, 20);
            this.ntxStripHeight.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tile Height:";
            // 
            // ntxTileHeight
            // 
            this.ntxTileHeight.Enabled = false;
            this.ntxTileHeight.Location = new System.Drawing.Point(84, 159);
            this.ntxTileHeight.Name = "ntxTileHeight";
            this.ntxTileHeight.Size = new System.Drawing.Size(100, 20);
            this.ntxTileHeight.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tile Width:";
            // 
            // ntxTileWidth
            // 
            this.ntxTileWidth.Enabled = false;
            this.ntxTileWidth.Location = new System.Drawing.Point(84, 133);
            this.ntxTileWidth.Name = "ntxTileWidth";
            this.ntxTileWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxTileWidth.TabIndex = 7;
            // 
            // cbxIsTiled
            // 
            this.cbxIsTiled.AutoSize = true;
            this.cbxIsTiled.Enabled = false;
            this.cbxIsTiled.Location = new System.Drawing.Point(11, 107);
            this.cbxIsTiled.Name = "cbxIsTiled";
            this.cbxIsTiled.Size = new System.Drawing.Size(60, 17);
            this.cbxIsTiled.TabIndex = 6;
            this.cbxIsTiled.Text = "Is Tiled";
            this.cbxIsTiled.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Image Height:";
            // 
            // ntxImageHeight
            // 
            this.ntxImageHeight.Enabled = false;
            this.ntxImageHeight.Location = new System.Drawing.Point(84, 76);
            this.ntxImageHeight.Name = "ntxImageHeight";
            this.ntxImageHeight.Size = new System.Drawing.Size(100, 20);
            this.ntxImageHeight.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Image Width:";
            // 
            // ntxImageWidth
            // 
            this.ntxImageWidth.Enabled = false;
            this.ntxImageWidth.Location = new System.Drawing.Point(84, 50);
            this.ntxImageWidth.Name = "ntxImageWidth";
            this.ntxImageWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxImageWidth.TabIndex = 1;
            // 
            // ctrlBrowseSourceFileFullPath
            // 
            this.ctrlBrowseSourceFileFullPath.AutoSize = true;
            this.ctrlBrowseSourceFileFullPath.FileName = "";
            this.ctrlBrowseSourceFileFullPath.Filter = "";
            this.ctrlBrowseSourceFileFullPath.IsFolderDialog = false;
            this.ctrlBrowseSourceFileFullPath.IsFullPath = true;
            this.ctrlBrowseSourceFileFullPath.IsSaveFile = false;
            this.ctrlBrowseSourceFileFullPath.LabelCaption = "Source File:";
            this.ctrlBrowseSourceFileFullPath.Location = new System.Drawing.Point(80, 19);
            this.ctrlBrowseSourceFileFullPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseSourceFileFullPath.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseSourceFileFullPath.MultiFilesSelect = false;
            this.ctrlBrowseSourceFileFullPath.Name = "ctrlBrowseSourceFileFullPath";
            this.ctrlBrowseSourceFileFullPath.Prefix = "";
            this.ctrlBrowseSourceFileFullPath.Size = new System.Drawing.Size(652, 24);
            this.ctrlBrowseSourceFileFullPath.TabIndex = 0;
            // 
            // tpDTMFromPointsCloud
            // 
            this.tpDTMFromPointsCloud.Controls.Add(this.label32);
            this.tpDTMFromPointsCloud.Controls.Add(this.label30);
            this.tpDTMFromPointsCloud.Controls.Add(this.btnDrawPtCloudArea);
            this.tpDTMFromPointsCloud.Controls.Add(this.label29);
            this.tpDTMFromPointsCloud.Controls.Add(this.ntxNumPtToCastLots);
            this.tpDTMFromPointsCloud.Controls.Add(this.btnGenerateDTMFromPointsCloud);
            this.tpDTMFromPointsCloud.Controls.Add(this.label10);
            this.tpDTMFromPointsCloud.Controls.Add(this.ntxMinCloudPointsForHeightCalculation);
            this.tpDTMFromPointsCloud.Controls.Add(this.label9);
            this.tpDTMFromPointsCloud.Controls.Add(this.ntxMaxSearchRadiusForHeightCalculation);
            this.tpDTMFromPointsCloud.Controls.Add(this.label8);
            this.tpDTMFromPointsCloud.Controls.Add(this.label7);
            this.tpDTMFromPointsCloud.Controls.Add(this.ctrl2DVectorDTMMaxPoint);
            this.tpDTMFromPointsCloud.Controls.Add(this.ctrl2DVectorDTMMinPoint);
            this.tpDTMFromPointsCloud.Controls.Add(this.label6);
            this.tpDTMFromPointsCloud.Controls.Add(this.ntxDTMResolution);
            this.tpDTMFromPointsCloud.Controls.Add(this.ctrlBrowseDTMPath);
            this.tpDTMFromPointsCloud.Controls.Add(this.ctrlSampleCloudPoints);
            this.tpDTMFromPointsCloud.Controls.Add(this.dgvCloudPoints);
            this.tpDTMFromPointsCloud.Location = new System.Drawing.Point(4, 22);
            this.tpDTMFromPointsCloud.Name = "tpDTMFromPointsCloud";
            this.tpDTMFromPointsCloud.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpDTMFromPointsCloud.Size = new System.Drawing.Size(785, 591);
            this.tpDTMFromPointsCloud.TabIndex = 1;
            this.tpDTMFromPointsCloud.Text = "DTM From Points Cloud";
            this.tpDTMFromPointsCloud.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(547, 73);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(106, 13);
            this.label30.TabIndex = 27;
            this.label30.Text = "Draw rectangle area:";
            // 
            // btnDrawPtCloudArea
            // 
            this.btnDrawPtCloudArea.Location = new System.Drawing.Point(697, 68);
            this.btnDrawPtCloudArea.Name = "btnDrawPtCloudArea";
            this.btnDrawPtCloudArea.Size = new System.Drawing.Size(75, 23);
            this.btnDrawPtCloudArea.TabIndex = 26;
            this.btnDrawPtCloudArea.Text = "Draw";
            this.btnDrawPtCloudArea.UseVisualStyleBackColor = true;
            this.btnDrawPtCloudArea.Click += new System.EventHandler(this.btnDrawPtCloudArea_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(547, 45);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(144, 13);
            this.label29.TabIndex = 25;
            this.label29.Text = "Number of points to cast lots:";
            // 
            // ntxNumPtToCastLots
            // 
            this.ntxNumPtToCastLots.Location = new System.Drawing.Point(697, 42);
            this.ntxNumPtToCastLots.Name = "ntxNumPtToCastLots";
            this.ntxNumPtToCastLots.Size = new System.Drawing.Size(75, 20);
            this.ntxNumPtToCastLots.TabIndex = 24;
            this.ntxNumPtToCastLots.Text = "1";
            // 
            // btnGenerateDTMFromPointsCloud
            // 
            this.btnGenerateDTMFromPointsCloud.Location = new System.Drawing.Point(643, 560);
            this.btnGenerateDTMFromPointsCloud.Name = "btnGenerateDTMFromPointsCloud";
            this.btnGenerateDTMFromPointsCloud.Size = new System.Drawing.Size(93, 23);
            this.btnGenerateDTMFromPointsCloud.TabIndex = 23;
            this.btnGenerateDTMFromPointsCloud.Text = "Generate DTM";
            this.btnGenerateDTMFromPointsCloud.UseVisualStyleBackColor = true;
            this.btnGenerateDTMFromPointsCloud.Click += new System.EventHandler(this.btnGenerateDTMFromPointsCloud_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 392);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(196, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Min Cloud Points For Height Calculation:";
            // 
            // ntxMinCloudPointsForHeightCalculation
            // 
            this.ntxMinCloudPointsForHeightCalculation.Location = new System.Drawing.Point(224, 389);
            this.ntxMinCloudPointsForHeightCalculation.Name = "ntxMinCloudPointsForHeightCalculation";
            this.ntxMinCloudPointsForHeightCalculation.Size = new System.Drawing.Size(100, 20);
            this.ntxMinCloudPointsForHeightCalculation.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 366);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(210, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Max Search Radius For Height Calculation:";
            // 
            // ntxMaxSearchRadiusForHeightCalculation
            // 
            this.ntxMaxSearchRadiusForHeightCalculation.Location = new System.Drawing.Point(224, 363);
            this.ntxMaxSearchRadiusForHeightCalculation.Name = "ntxMaxSearchRadiusForHeightCalculation";
            this.ntxMaxSearchRadiusForHeightCalculation.Size = new System.Drawing.Size(100, 20);
            this.ntxMaxSearchRadiusForHeightCalculation.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 332);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "DTM Max Point:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "DTM Min Point:";
            // 
            // ctrl2DVectorDTMMaxPoint
            // 
            this.ctrl2DVectorDTMMaxPoint.Location = new System.Drawing.Point(98, 329);
            this.ctrl2DVectorDTMMaxPoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl2DVectorDTMMaxPoint.Name = "ctrl2DVectorDTMMaxPoint";
            this.ctrl2DVectorDTMMaxPoint.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DVectorDTMMaxPoint.TabIndex = 16;
            this.ctrl2DVectorDTMMaxPoint.X = 0D;
            this.ctrl2DVectorDTMMaxPoint.Y = 0D;
            // 
            // ctrl2DVectorDTMMinPoint
            // 
            this.ctrl2DVectorDTMMinPoint.Location = new System.Drawing.Point(98, 297);
            this.ctrl2DVectorDTMMinPoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl2DVectorDTMMinPoint.Name = "ctrl2DVectorDTMMinPoint";
            this.ctrl2DVectorDTMMinPoint.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DVectorDTMMinPoint.TabIndex = 15;
            this.ctrl2DVectorDTMMinPoint.X = 0D;
            this.ctrl2DVectorDTMMinPoint.Y = 0D;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "DTM Resolution:";
            // 
            // ntxDTMResolution
            // 
            this.ntxDTMResolution.Location = new System.Drawing.Point(101, 271);
            this.ntxDTMResolution.Name = "ntxDTMResolution";
            this.ntxDTMResolution.Size = new System.Drawing.Size(100, 20);
            this.ntxDTMResolution.TabIndex = 13;
            // 
            // ctrlBrowseDTMPath
            // 
            this.ctrlBrowseDTMPath.AutoSize = true;
            this.ctrlBrowseDTMPath.FileName = "";
            this.ctrlBrowseDTMPath.Filter = "";
            this.ctrlBrowseDTMPath.IsFolderDialog = false;
            this.ctrlBrowseDTMPath.IsFullPath = true;
            this.ctrlBrowseDTMPath.IsSaveFile = true;
            this.ctrlBrowseDTMPath.LabelCaption = "DTM Path:";
            this.ctrlBrowseDTMPath.Location = new System.Drawing.Point(98, 241);
            this.ctrlBrowseDTMPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseDTMPath.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseDTMPath.MultiFilesSelect = false;
            this.ctrlBrowseDTMPath.Name = "ctrlBrowseDTMPath";
            this.ctrlBrowseDTMPath.Prefix = "";
            this.ctrlBrowseDTMPath.Size = new System.Drawing.Size(679, 24);
            this.ctrlBrowseDTMPath.TabIndex = 12;
            // 
            // ctrlSampleCloudPoints
            // 
            this.ctrlSampleCloudPoints._DgvControlName = "dgvCloudPoints";
            this.ctrlSampleCloudPoints._IsRelativeToDTM = false;
            this.ctrlSampleCloudPoints._PointInOverlayManagerCoordSys = true;
            this.ctrlSampleCloudPoints._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSampleCloudPoints._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSampleCloudPoints._SampleOnePoint = false;
            this.ctrlSampleCloudPoints._UserControlName = null;
            this.ctrlSampleCloudPoints.IsAsync = false;
            this.ctrlSampleCloudPoints.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSampleCloudPoints.Location = new System.Drawing.Point(8, 198);
            this.ctrlSampleCloudPoints.Name = "ctrlSampleCloudPoints";
            this.ctrlSampleCloudPoints.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSampleCloudPoints.Size = new System.Drawing.Size(418, 23);
            this.ctrlSampleCloudPoints.TabIndex = 11;
            this.ctrlSampleCloudPoints.Text = "Click For Sample Cloud Points From Map";
            this.ctrlSampleCloudPoints.UseVisualStyleBackColor = true;
            // 
            // dgvCloudPoints
            // 
            this.dgvCloudPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCloudPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.X,
            this.Y,
            this.Z,
            this.colUsePoint});
            this.dgvCloudPoints.Location = new System.Drawing.Point(8, 17);
            this.dgvCloudPoints.Name = "dgvCloudPoints";
            this.dgvCloudPoints.Size = new System.Drawing.Size(418, 175);
            this.dgvCloudPoints.TabIndex = 10;
            // 
            // X
            // 
            this.X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // Z
            // 
            this.Z.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            // 
            // colUsePoint
            // 
            this.colUsePoint.HeaderText = "Use Point";
            this.colUsePoint.Name = "colUsePoint";
            this.colUsePoint.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colUsePoint.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tpMosaicFromOrthophotos
            // 
            this.tpMosaicFromOrthophotos.Controls.Add(this.label34);
            this.tpMosaicFromOrthophotos.Controls.Add(this.label33);
            this.tpMosaicFromOrthophotos.Controls.Add(this.btnGenerateMosaic);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ntxCompressionFactor);
            this.tpMosaicFromOrthophotos.Controls.Add(this.label18);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ctrlBrowseControlMosaicTfwFile);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ctrlBrowseMosaicTifFile);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ctrlGridCoordinateSystemMosaicWorldParams);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ntxMosaicBackgroundGrayLevel);
            this.tpMosaicFromOrthophotos.Controls.Add(this.label17);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ntxMosaicTileSize);
            this.tpMosaicFromOrthophotos.Controls.Add(this.label16);
            this.tpMosaicFromOrthophotos.Controls.Add(this.gbGeoReferencingParams);
            this.tpMosaicFromOrthophotos.Controls.Add(this.cmbSourceResamplingMethod);
            this.tpMosaicFromOrthophotos.Controls.Add(this.label11);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ctrlBrowseSourceOrthophotoTfwFiles);
            this.tpMosaicFromOrthophotos.Controls.Add(this.ctrlBrowseSourceOrthophotoTifFiles);
            this.tpMosaicFromOrthophotos.Location = new System.Drawing.Point(4, 22);
            this.tpMosaicFromOrthophotos.Name = "tpMosaicFromOrthophotos";
            this.tpMosaicFromOrthophotos.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpMosaicFromOrthophotos.Size = new System.Drawing.Size(785, 591);
            this.tpMosaicFromOrthophotos.TabIndex = 2;
            this.tpMosaicFromOrthophotos.Text = "Mosaic From Orthophotos";
            this.tpMosaicFromOrthophotos.UseVisualStyleBackColor = true;
            // 
            // btnGenerateMosaic
            // 
            this.btnGenerateMosaic.Location = new System.Drawing.Point(621, 560);
            this.btnGenerateMosaic.Name = "btnGenerateMosaic";
            this.btnGenerateMosaic.Size = new System.Drawing.Size(111, 23);
            this.btnGenerateMosaic.TabIndex = 16;
            this.btnGenerateMosaic.Text = "Generate Mosaic";
            this.btnGenerateMosaic.UseVisualStyleBackColor = true;
            this.btnGenerateMosaic.Click += new System.EventHandler(this.btnGenerateMosaic_Click);
            // 
            // ntxCompressionFactor
            // 
            this.ntxCompressionFactor.Location = new System.Drawing.Point(537, 376);
            this.ntxCompressionFactor.Name = "ntxCompressionFactor";
            this.ntxCompressionFactor.Size = new System.Drawing.Size(100, 20);
            this.ntxCompressionFactor.TabIndex = 15;
            this.ntxCompressionFactor.Text = "-1";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(372, 379);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(103, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "Compression Factor:";
            // 
            // ctrlBrowseControlMosaicTfwFile
            // 
            this.ctrlBrowseControlMosaicTfwFile.AutoSize = true;
            this.ctrlBrowseControlMosaicTfwFile.FileName = "";
            this.ctrlBrowseControlMosaicTfwFile.Filter = "TFW File|*.Tfw";
            this.ctrlBrowseControlMosaicTfwFile.IsFolderDialog = false;
            this.ctrlBrowseControlMosaicTfwFile.IsFullPath = true;
            this.ctrlBrowseControlMosaicTfwFile.IsSaveFile = true;
            this.ctrlBrowseControlMosaicTfwFile.LabelCaption = "Output Mosaic Tfw File:";
            this.ctrlBrowseControlMosaicTfwFile.Location = new System.Drawing.Point(8, 520);
            this.ctrlBrowseControlMosaicTfwFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseControlMosaicTfwFile.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseControlMosaicTfwFile.MultiFilesSelect = false;
            this.ctrlBrowseControlMosaicTfwFile.Name = "ctrlBrowseControlMosaicTfwFile";
            this.ctrlBrowseControlMosaicTfwFile.Prefix = "";
            this.ctrlBrowseControlMosaicTfwFile.Size = new System.Drawing.Size(761, 24);
            this.ctrlBrowseControlMosaicTfwFile.TabIndex = 13;
            // 
            // ctrlBrowseMosaicTifFile
            // 
            this.ctrlBrowseMosaicTifFile.AutoSize = true;
            this.ctrlBrowseMosaicTifFile.FileName = "";
            this.ctrlBrowseMosaicTifFile.Filter = "TIF File|*.Tif";
            this.ctrlBrowseMosaicTifFile.IsFolderDialog = false;
            this.ctrlBrowseMosaicTifFile.IsFullPath = true;
            this.ctrlBrowseMosaicTifFile.IsSaveFile = true;
            this.ctrlBrowseMosaicTifFile.LabelCaption = "Output Mosaic Tif File:";
            this.ctrlBrowseMosaicTifFile.Location = new System.Drawing.Point(8, 490);
            this.ctrlBrowseMosaicTifFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseMosaicTifFile.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseMosaicTifFile.MultiFilesSelect = false;
            this.ctrlBrowseMosaicTifFile.Name = "ctrlBrowseMosaicTifFile";
            this.ctrlBrowseMosaicTifFile.Prefix = "";
            this.ctrlBrowseMosaicTifFile.Size = new System.Drawing.Size(761, 24);
            this.ctrlBrowseMosaicTifFile.TabIndex = 12;
            // 
            // ctrlGridCoordinateSystemMosaicWorldParams
            // 
            this.ctrlGridCoordinateSystemMosaicWorldParams.EnableNewCoordSysCreation = true;
            this.ctrlGridCoordinateSystemMosaicWorldParams.GridCoordinateSystem = null;
            this.ctrlGridCoordinateSystemMosaicWorldParams.GroupBoxText = "Mosaic World Params";
            this.ctrlGridCoordinateSystemMosaicWorldParams.IsEditable = false;
            this.ctrlGridCoordinateSystemMosaicWorldParams.Location = new System.Drawing.Point(11, 318);
            this.ctrlGridCoordinateSystemMosaicWorldParams.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlGridCoordinateSystemMosaicWorldParams.Name = "ctrlGridCoordinateSystemMosaicWorldParams";
            this.ctrlGridCoordinateSystemMosaicWorldParams.Size = new System.Drawing.Size(336, 155);
            this.ctrlGridCoordinateSystemMosaicWorldParams.TabIndex = 11;
            // 
            // ntxMosaicBackgroundGrayLevel
            // 
            this.ntxMosaicBackgroundGrayLevel.Location = new System.Drawing.Point(537, 350);
            this.ntxMosaicBackgroundGrayLevel.Name = "ntxMosaicBackgroundGrayLevel";
            this.ntxMosaicBackgroundGrayLevel.Size = new System.Drawing.Size(100, 20);
            this.ntxMosaicBackgroundGrayLevel.TabIndex = 10;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(372, 353);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(159, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Mosaic Background Gray Level:";
            // 
            // ntxMosaicTileSize
            // 
            this.ntxMosaicTileSize.Location = new System.Drawing.Point(537, 324);
            this.ntxMosaicTileSize.Name = "ntxMosaicTileSize";
            this.ntxMosaicTileSize.Size = new System.Drawing.Size(100, 20);
            this.ntxMosaicTileSize.TabIndex = 8;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(372, 327);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "Mosaic Tile Size:";
            // 
            // gbGeoReferencingParams
            // 
            this.gbGeoReferencingParams.Controls.Add(this.ctrlSampleMosaicFromOrthophotosSecondCorner);
            this.gbGeoReferencingParams.Controls.Add(this.ctrlSampleMosaicFromOrthophotosFirstCorner);
            this.gbGeoReferencingParams.Controls.Add(this.ntxGSD);
            this.gbGeoReferencingParams.Controls.Add(this.ntxAzimDeg);
            this.gbGeoReferencingParams.Controls.Add(this.label15);
            this.gbGeoReferencingParams.Controls.Add(this.label14);
            this.gbGeoReferencingParams.Controls.Add(this.label13);
            this.gbGeoReferencingParams.Controls.Add(this.label12);
            this.gbGeoReferencingParams.Controls.Add(this.ctrl3DVectorSecondCornerInDiagonal);
            this.gbGeoReferencingParams.Controls.Add(this.ctrl3DVectorFirstCornerInDiagonal);
            this.gbGeoReferencingParams.Location = new System.Drawing.Point(350, 75);
            this.gbGeoReferencingParams.Name = "gbGeoReferencingParams";
            this.gbGeoReferencingParams.Size = new System.Drawing.Size(427, 155);
            this.gbGeoReferencingParams.TabIndex = 5;
            this.gbGeoReferencingParams.TabStop = false;
            this.gbGeoReferencingParams.Text = "Geo Referencing Params";
            // 
            // ctrlSampleMosaicFromOrthophotosSecondCorner
            // 
            this.ctrlSampleMosaicFromOrthophotosSecondCorner._DgvControlName = null;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner._IsRelativeToDTM = false;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner._PointInOverlayManagerCoordSys = true;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner._SampleOnePoint = true;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner._UserControlName = "ctrl3DVectorSecondCornerInDiagonal";
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.IsAsync = false;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.Location = new System.Drawing.Point(388, 54);
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.Name = "ctrlSampleMosaicFromOrthophotosSecondCorner";
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.Size = new System.Drawing.Size(31, 23);
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.TabIndex = 10;
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.Text = "...";
            this.ctrlSampleMosaicFromOrthophotosSecondCorner.UseVisualStyleBackColor = true;
            // 
            // ctrlSampleMosaicFromOrthophotosFirstCorner
            // 
            this.ctrlSampleMosaicFromOrthophotosFirstCorner._DgvControlName = null;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner._IsRelativeToDTM = false;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner._PointInOverlayManagerCoordSys = true;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner._SampleOnePoint = true;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner._UserControlName = "ctrl3DVectorFirstCornerInDiagonal";
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.IsAsync = false;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.Location = new System.Drawing.Point(388, 23);
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.Name = "ctrlSampleMosaicFromOrthophotosFirstCorner";
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.Size = new System.Drawing.Size(31, 23);
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.TabIndex = 9;
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.Text = "...";
            this.ctrlSampleMosaicFromOrthophotosFirstCorner.UseVisualStyleBackColor = true;
            // 
            // ntxGSD
            // 
            this.ntxGSD.Location = new System.Drawing.Point(97, 111);
            this.ntxGSD.Name = "ntxGSD";
            this.ntxGSD.Size = new System.Drawing.Size(100, 20);
            this.ntxGSD.TabIndex = 7;
            // 
            // ntxAzimDeg
            // 
            this.ntxAzimDeg.Location = new System.Drawing.Point(97, 85);
            this.ntxAzimDeg.Name = "ntxAzimDeg";
            this.ntxAzimDeg.Size = new System.Drawing.Size(100, 20);
            this.ntxAzimDeg.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 111);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(33, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "GSD:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Azimuth Degree:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Second Corner In Diagonal:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "First Corner In Diagonal:";
            // 
            // ctrl3DVectorSecondCornerInDiagonal
            // 
            this.ctrl3DVectorSecondCornerInDiagonal.IsReadOnly = false;
            this.ctrl3DVectorSecondCornerInDiagonal.Location = new System.Drawing.Point(150, 54);
            this.ctrl3DVectorSecondCornerInDiagonal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl3DVectorSecondCornerInDiagonal.Name = "ctrl3DVectorSecondCornerInDiagonal";
            this.ctrl3DVectorSecondCornerInDiagonal.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorSecondCornerInDiagonal.TabIndex = 1;
            this.ctrl3DVectorSecondCornerInDiagonal.X = 0D;
            this.ctrl3DVectorSecondCornerInDiagonal.Y = 0D;
            this.ctrl3DVectorSecondCornerInDiagonal.Z = 0D;
            // 
            // ctrl3DVectorFirstCornerInDiagonal
            // 
            this.ctrl3DVectorFirstCornerInDiagonal.IsReadOnly = false;
            this.ctrl3DVectorFirstCornerInDiagonal.Location = new System.Drawing.Point(150, 22);
            this.ctrl3DVectorFirstCornerInDiagonal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl3DVectorFirstCornerInDiagonal.Name = "ctrl3DVectorFirstCornerInDiagonal";
            this.ctrl3DVectorFirstCornerInDiagonal.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorFirstCornerInDiagonal.TabIndex = 0;
            this.ctrl3DVectorFirstCornerInDiagonal.X = 0D;
            this.ctrl3DVectorFirstCornerInDiagonal.Y = 0D;
            this.ctrl3DVectorFirstCornerInDiagonal.Z = 0D;
            // 
            // cmbSourceResamplingMethod
            // 
            this.cmbSourceResamplingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSourceResamplingMethod.FormattingEnabled = true;
            this.cmbSourceResamplingMethod.Location = new System.Drawing.Point(155, 248);
            this.cmbSourceResamplingMethod.Name = "cmbSourceResamplingMethod";
            this.cmbSourceResamplingMethod.Size = new System.Drawing.Size(189, 21);
            this.cmbSourceResamplingMethod.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 251);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(141, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Source Resampling Method:";
            // 
            // ctrlGridCoordinateSystemSourceOrthophotoWorldParams
            // 
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.EnableNewCoordSysCreation = true;
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.GridCoordinateSystem = null;
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.GroupBoxText = "Source Orthophoto World Params";
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.IsEditable = false;
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.Location = new System.Drawing.Point(8, 75);
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.Name = "ctrlGridCoordinateSystemSourceOrthophotoWorldParams";
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.Size = new System.Drawing.Size(336, 155);
            this.ctrlGridCoordinateSystemSourceOrthophotoWorldParams.TabIndex = 2;
            // 
            // ctrlBrowseSourceOrthophotoTfwFiles
            // 
            this.ctrlBrowseSourceOrthophotoTfwFiles.AutoSize = true;
            this.ctrlBrowseSourceOrthophotoTfwFiles.FileName = "";
            this.ctrlBrowseSourceOrthophotoTfwFiles.Filter = "";
            this.ctrlBrowseSourceOrthophotoTfwFiles.IsFolderDialog = false;
            this.ctrlBrowseSourceOrthophotoTfwFiles.IsFullPath = true;
            this.ctrlBrowseSourceOrthophotoTfwFiles.IsSaveFile = false;
            this.ctrlBrowseSourceOrthophotoTfwFiles.LabelCaption = "Source Orthophoto Tfw Files:";
            this.ctrlBrowseSourceOrthophotoTfwFiles.Location = new System.Drawing.Point(151, 45);
            this.ctrlBrowseSourceOrthophotoTfwFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseSourceOrthophotoTfwFiles.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseSourceOrthophotoTfwFiles.MultiFilesSelect = false;
            this.ctrlBrowseSourceOrthophotoTfwFiles.Name = "ctrlBrowseSourceOrthophotoTfwFiles";
            this.ctrlBrowseSourceOrthophotoTfwFiles.Prefix = "";
            this.ctrlBrowseSourceOrthophotoTfwFiles.Size = new System.Drawing.Size(618, 24);
            this.ctrlBrowseSourceOrthophotoTfwFiles.TabIndex = 1;
            // 
            // ctrlBrowseSourceOrthophotoTifFiles
            // 
            this.ctrlBrowseSourceOrthophotoTifFiles.AutoSize = true;
            this.ctrlBrowseSourceOrthophotoTifFiles.FileName = "";
            this.ctrlBrowseSourceOrthophotoTifFiles.Filter = "";
            this.ctrlBrowseSourceOrthophotoTifFiles.IsFolderDialog = false;
            this.ctrlBrowseSourceOrthophotoTifFiles.IsFullPath = true;
            this.ctrlBrowseSourceOrthophotoTifFiles.IsSaveFile = false;
            this.ctrlBrowseSourceOrthophotoTifFiles.LabelCaption = "Source Orthophoto Tif Files:";
            this.ctrlBrowseSourceOrthophotoTifFiles.Location = new System.Drawing.Point(151, 15);
            this.ctrlBrowseSourceOrthophotoTifFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseSourceOrthophotoTifFiles.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseSourceOrthophotoTifFiles.MultiFilesSelect = false;
            this.ctrlBrowseSourceOrthophotoTifFiles.Name = "ctrlBrowseSourceOrthophotoTifFiles";
            this.ctrlBrowseSourceOrthophotoTifFiles.Prefix = "";
            this.ctrlBrowseSourceOrthophotoTifFiles.Size = new System.Drawing.Size(618, 24);
            this.ctrlBrowseSourceOrthophotoTifFiles.TabIndex = 0;
            // 
            // tpOrthophotoFromLoropImage
            // 
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label37);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label36);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label35);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.cbxDTMMapLayer);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label28);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.btnAddDTMMapLayer);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ntxOrthophotoCompressionFactor);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label27);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.btnGenerateOrthophoto);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ctrlBrowseOrthophotoTfwFile);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ctrlBrowseOrthophotoTifFile);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ctrlGridCoordinateSystemOrthophoto);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ntxOrthophotoDefaultHeight);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label26);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ntxOrthophotoBackgroundGrayLevel);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label24);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ntxOrthopothoTileSize);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label25);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.groupBox1);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.cmbOrthophotoSourceResamplingMethod);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.label19);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ctrlImageCalcSourceLorop);
            this.tpOrthophotoFromLoropImage.Controls.Add(this.ctrlBrowseSourceLoropTifFile);
            this.tpOrthophotoFromLoropImage.Location = new System.Drawing.Point(4, 22);
            this.tpOrthophotoFromLoropImage.Name = "tpOrthophotoFromLoropImage";
            this.tpOrthophotoFromLoropImage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpOrthophotoFromLoropImage.Size = new System.Drawing.Size(785, 591);
            this.tpOrthophotoFromLoropImage.TabIndex = 3;
            this.tpOrthophotoFromLoropImage.Text = "Orthophoto From Lorop Image";
            this.tpOrthophotoFromLoropImage.UseVisualStyleBackColor = true;
            // 
            // cbxDTMMapLayer
            // 
            this.cbxDTMMapLayer.AutoSize = true;
            this.cbxDTMMapLayer.Checked = true;
            this.cbxDTMMapLayer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxDTMMapLayer.Location = new System.Drawing.Point(158, 417);
            this.cbxDTMMapLayer.Name = "cbxDTMMapLayer";
            this.cbxDTMMapLayer.Size = new System.Drawing.Size(44, 17);
            this.cbxDTMMapLayer.TabIndex = 26;
            this.cbxDTMMapLayer.Text = "Null";
            this.cbxDTMMapLayer.UseVisualStyleBackColor = true;
            this.cbxDTMMapLayer.CheckedChanged += new System.EventHandler(this.cbxDTMMapLayer_CheckedChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 420);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(63, 13);
            this.label28.TabIndex = 25;
            this.label28.Text = "DTM Layer:";
            // 
            // btnAddDTMMapLayer
            // 
            this.btnAddDTMMapLayer.Location = new System.Drawing.Point(77, 413);
            this.btnAddDTMMapLayer.Name = "btnAddDTMMapLayer";
            this.btnAddDTMMapLayer.Size = new System.Drawing.Size(75, 23);
            this.btnAddDTMMapLayer.TabIndex = 24;
            this.btnAddDTMMapLayer.Text = "Add";
            this.btnAddDTMMapLayer.UseVisualStyleBackColor = true;
            this.btnAddDTMMapLayer.Click += new System.EventHandler(this.btnAddDTMMapLayer_Click);
            // 
            // ntxOrthophotoCompressionFactor
            // 
            this.ntxOrthophotoCompressionFactor.Location = new System.Drawing.Point(540, 342);
            this.ntxOrthophotoCompressionFactor.Name = "ntxOrthophotoCompressionFactor";
            this.ntxOrthophotoCompressionFactor.Size = new System.Drawing.Size(100, 20);
            this.ntxOrthophotoCompressionFactor.TabIndex = 23;
            this.ntxOrthophotoCompressionFactor.Text = "-1";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(356, 345);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(103, 13);
            this.label27.TabIndex = 22;
            this.label27.Text = "Compression Factor:";
            // 
            // btnGenerateOrthophoto
            // 
            this.btnGenerateOrthophoto.Location = new System.Drawing.Point(641, 560);
            this.btnGenerateOrthophoto.Name = "btnGenerateOrthophoto";
            this.btnGenerateOrthophoto.Size = new System.Drawing.Size(128, 23);
            this.btnGenerateOrthophoto.TabIndex = 21;
            this.btnGenerateOrthophoto.Text = "Generate Orthophoto";
            this.btnGenerateOrthophoto.UseVisualStyleBackColor = true;
            this.btnGenerateOrthophoto.Click += new System.EventHandler(this.btnGenerateOrthophoto_Click);
            // 
            // ctrlBrowseOrthophotoTfwFile
            // 
            this.ctrlBrowseOrthophotoTfwFile.AutoSize = true;
            this.ctrlBrowseOrthophotoTfwFile.FileName = "";
            this.ctrlBrowseOrthophotoTfwFile.Filter = "TFW File|*.Tfw";
            this.ctrlBrowseOrthophotoTfwFile.IsFolderDialog = false;
            this.ctrlBrowseOrthophotoTfwFile.IsFullPath = true;
            this.ctrlBrowseOrthophotoTfwFile.IsSaveFile = true;
            this.ctrlBrowseOrthophotoTfwFile.LabelCaption = "Output Orthophoto Tfw File:";
            this.ctrlBrowseOrthophotoTfwFile.Location = new System.Drawing.Point(139, 487);
            this.ctrlBrowseOrthophotoTfwFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseOrthophotoTfwFile.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseOrthophotoTfwFile.MultiFilesSelect = false;
            this.ctrlBrowseOrthophotoTfwFile.Name = "ctrlBrowseOrthophotoTfwFile";
            this.ctrlBrowseOrthophotoTfwFile.Prefix = "";
            this.ctrlBrowseOrthophotoTfwFile.Size = new System.Drawing.Size(630, 24);
            this.ctrlBrowseOrthophotoTfwFile.TabIndex = 19;
            // 
            // ctrlBrowseOrthophotoTifFile
            // 
            this.ctrlBrowseOrthophotoTifFile.AutoSize = true;
            this.ctrlBrowseOrthophotoTifFile.FileName = "";
            this.ctrlBrowseOrthophotoTifFile.Filter = "TIF File|*.Tif";
            this.ctrlBrowseOrthophotoTifFile.IsFolderDialog = false;
            this.ctrlBrowseOrthophotoTifFile.IsFullPath = true;
            this.ctrlBrowseOrthophotoTifFile.IsSaveFile = true;
            this.ctrlBrowseOrthophotoTifFile.LabelCaption = "Output Orthophoto Tif File:";
            this.ctrlBrowseOrthophotoTifFile.Location = new System.Drawing.Point(139, 455);
            this.ctrlBrowseOrthophotoTifFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseOrthophotoTifFile.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseOrthophotoTifFile.MultiFilesSelect = false;
            this.ctrlBrowseOrthophotoTifFile.Name = "ctrlBrowseOrthophotoTifFile";
            this.ctrlBrowseOrthophotoTifFile.Prefix = "";
            this.ctrlBrowseOrthophotoTifFile.Size = new System.Drawing.Size(630, 24);
            this.ctrlBrowseOrthophotoTifFile.TabIndex = 18;
            // 
            // ctrlGridCoordinateSystemOrthophoto
            // 
            this.ctrlGridCoordinateSystemOrthophoto.EnableNewCoordSysCreation = true;
            this.ctrlGridCoordinateSystemOrthophoto.GridCoordinateSystem = null;
            this.ctrlGridCoordinateSystemOrthophoto.GroupBoxText = "Orthophoto Coord Sys";
            this.ctrlGridCoordinateSystemOrthophoto.IsEditable = false;
            this.ctrlGridCoordinateSystemOrthophoto.Location = new System.Drawing.Point(8, 248);
            this.ctrlGridCoordinateSystemOrthophoto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlGridCoordinateSystemOrthophoto.Name = "ctrlGridCoordinateSystemOrthophoto";
            this.ctrlGridCoordinateSystemOrthophoto.Size = new System.Drawing.Size(336, 155);
            this.ctrlGridCoordinateSystemOrthophoto.TabIndex = 17;
            // 
            // ntxOrthophotoDefaultHeight
            // 
            this.ntxOrthophotoDefaultHeight.Location = new System.Drawing.Point(540, 316);
            this.ntxOrthophotoDefaultHeight.Name = "ntxOrthophotoDefaultHeight";
            this.ntxOrthophotoDefaultHeight.Size = new System.Drawing.Size(100, 20);
            this.ntxOrthophotoDefaultHeight.TabIndex = 16;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(356, 319);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(134, 13);
            this.label26.TabIndex = 15;
            this.label26.Text = "Orthophoto Default Height:";
            // 
            // ntxOrthophotoBackgroundGrayLevel
            // 
            this.ntxOrthophotoBackgroundGrayLevel.Location = new System.Drawing.Point(540, 290);
            this.ntxOrthophotoBackgroundGrayLevel.Name = "ntxOrthophotoBackgroundGrayLevel";
            this.ntxOrthophotoBackgroundGrayLevel.Size = new System.Drawing.Size(100, 20);
            this.ntxOrthophotoBackgroundGrayLevel.TabIndex = 14;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(356, 293);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(178, 13);
            this.label24.TabIndex = 13;
            this.label24.Text = "Orthophoto Background Gray Level:";
            // 
            // ntxOrthopothoTileSize
            // 
            this.ntxOrthopothoTileSize.Location = new System.Drawing.Point(540, 264);
            this.ntxOrthopothoTileSize.Name = "ntxOrthopothoTileSize";
            this.ntxOrthopothoTileSize.Size = new System.Drawing.Size(100, 20);
            this.ntxOrthopothoTileSize.TabIndex = 12;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(356, 267);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(106, 13);
            this.label25.TabIndex = 11;
            this.label25.Text = "Orthopotho Tile Size:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctrlSampleOrthophotoFromLoropSecondCorner);
            this.groupBox1.Controls.Add(this.ctrlSampleOrthophotoFromLoropFirstCorner);
            this.groupBox1.Controls.Add(this.ntOrthophotoGSD);
            this.groupBox1.Controls.Add(this.ntxOrthophotoAzimDeg);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.ctrl3DVectorOrthophotoSecondCornerInDiagonal);
            this.groupBox1.Controls.Add(this.ctrl3DVectorOrthophotoFirstCornerInDiagonal);
            this.groupBox1.Location = new System.Drawing.Point(350, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 157);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geo Referencing Params";
            // 
            // ctrlSampleOrthophotoFromLoropSecondCorner
            // 
            this.ctrlSampleOrthophotoFromLoropSecondCorner._DgvControlName = null;
            this.ctrlSampleOrthophotoFromLoropSecondCorner._IsRelativeToDTM = false;
            this.ctrlSampleOrthophotoFromLoropSecondCorner._PointInOverlayManagerCoordSys = true;
            this.ctrlSampleOrthophotoFromLoropSecondCorner._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSampleOrthophotoFromLoropSecondCorner._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSampleOrthophotoFromLoropSecondCorner._SampleOnePoint = true;
            this.ctrlSampleOrthophotoFromLoropSecondCorner._UserControlName = "ctrl3DVectorOrthophotoSecondCornerInDiagonal";
            this.ctrlSampleOrthophotoFromLoropSecondCorner.IsAsync = false;
            this.ctrlSampleOrthophotoFromLoropSecondCorner.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSampleOrthophotoFromLoropSecondCorner.Location = new System.Drawing.Point(388, 54);
            this.ctrlSampleOrthophotoFromLoropSecondCorner.Name = "ctrlSampleOrthophotoFromLoropSecondCorner";
            this.ctrlSampleOrthophotoFromLoropSecondCorner.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSampleOrthophotoFromLoropSecondCorner.Size = new System.Drawing.Size(31, 23);
            this.ctrlSampleOrthophotoFromLoropSecondCorner.TabIndex = 9;
            this.ctrlSampleOrthophotoFromLoropSecondCorner.Text = "...";
            this.ctrlSampleOrthophotoFromLoropSecondCorner.UseVisualStyleBackColor = true;
            // 
            // ctrlSampleOrthophotoFromLoropFirstCorner
            // 
            this.ctrlSampleOrthophotoFromLoropFirstCorner._DgvControlName = null;
            this.ctrlSampleOrthophotoFromLoropFirstCorner._IsRelativeToDTM = false;
            this.ctrlSampleOrthophotoFromLoropFirstCorner._PointInOverlayManagerCoordSys = true;
            this.ctrlSampleOrthophotoFromLoropFirstCorner._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSampleOrthophotoFromLoropFirstCorner._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSampleOrthophotoFromLoropFirstCorner._SampleOnePoint = true;
            this.ctrlSampleOrthophotoFromLoropFirstCorner._UserControlName = "ctrl3DVectorOrthophotoFirstCornerInDiagonal";
            this.ctrlSampleOrthophotoFromLoropFirstCorner.IsAsync = false;
            this.ctrlSampleOrthophotoFromLoropFirstCorner.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSampleOrthophotoFromLoropFirstCorner.Location = new System.Drawing.Point(388, 23);
            this.ctrlSampleOrthophotoFromLoropFirstCorner.Name = "ctrlSampleOrthophotoFromLoropFirstCorner";
            this.ctrlSampleOrthophotoFromLoropFirstCorner.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSampleOrthophotoFromLoropFirstCorner.Size = new System.Drawing.Size(31, 23);
            this.ctrlSampleOrthophotoFromLoropFirstCorner.TabIndex = 8;
            this.ctrlSampleOrthophotoFromLoropFirstCorner.Text = "...";
            this.ctrlSampleOrthophotoFromLoropFirstCorner.UseVisualStyleBackColor = true;
            // 
            // ntOrthophotoGSD
            // 
            this.ntOrthophotoGSD.Location = new System.Drawing.Point(97, 111);
            this.ntOrthophotoGSD.Name = "ntOrthophotoGSD";
            this.ntOrthophotoGSD.Size = new System.Drawing.Size(100, 20);
            this.ntOrthophotoGSD.TabIndex = 7;
            // 
            // ntxOrthophotoAzimDeg
            // 
            this.ntxOrthophotoAzimDeg.Location = new System.Drawing.Point(97, 85);
            this.ntxOrthophotoAzimDeg.Name = "ntxOrthophotoAzimDeg";
            this.ntxOrthophotoAzimDeg.Size = new System.Drawing.Size(100, 20);
            this.ntxOrthophotoAzimDeg.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 111);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(33, 13);
            this.label20.TabIndex = 5;
            this.label20.Text = "GSD:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 88);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(85, 13);
            this.label21.TabIndex = 4;
            this.label21.Text = "Azimuth Degree:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 59);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(138, 13);
            this.label22.TabIndex = 3;
            this.label22.Text = "Second Corner In Diagonal:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 28);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(120, 13);
            this.label23.TabIndex = 2;
            this.label23.Text = "First Corner In Diagonal:";
            // 
            // ctrl3DVectorOrthophotoSecondCornerInDiagonal
            // 
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.IsReadOnly = false;
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.Location = new System.Drawing.Point(150, 54);
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.Name = "ctrl3DVectorOrthophotoSecondCornerInDiagonal";
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.TabIndex = 1;
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.X = 0D;
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.Y = 0D;
            this.ctrl3DVectorOrthophotoSecondCornerInDiagonal.Z = 0D;
            // 
            // ctrl3DVectorOrthophotoFirstCornerInDiagonal
            // 
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.IsReadOnly = false;
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.Location = new System.Drawing.Point(150, 22);
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.Name = "ctrl3DVectorOrthophotoFirstCornerInDiagonal";
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.TabIndex = 0;
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.X = 0D;
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.Y = 0D;
            this.ctrl3DVectorOrthophotoFirstCornerInDiagonal.Z = 0D;
            // 
            // cmbOrthophotoSourceResamplingMethod
            // 
            this.cmbOrthophotoSourceResamplingMethod.FormattingEnabled = true;
            this.cmbOrthophotoSourceResamplingMethod.Location = new System.Drawing.Point(155, 183);
            this.cmbOrthophotoSourceResamplingMethod.Name = "cmbOrthophotoSourceResamplingMethod";
            this.cmbOrthophotoSourceResamplingMethod.Size = new System.Drawing.Size(189, 21);
            this.cmbOrthophotoSourceResamplingMethod.TabIndex = 6;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(8, 186);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(141, 13);
            this.label19.TabIndex = 5;
            this.label19.Text = "Source Resampling Method:";
            // 
            // ctrlImageCalcSourceLorop
            // 
            this.ctrlImageCalcSourceLorop.EnableNewCoordSysCreation = true;
            this.ctrlImageCalcSourceLorop.ImageCalc = null;
            this.ctrlImageCalcSourceLorop.Location = new System.Drawing.Point(8, 47);
            this.ctrlImageCalcSourceLorop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlImageCalcSourceLorop.Name = "ctrlImageCalcSourceLorop";
            this.ctrlImageCalcSourceLorop.Size = new System.Drawing.Size(336, 121);
            this.ctrlImageCalcSourceLorop.TabIndex = 2;
            // 
            // ctrlBrowseSourceLoropTifFile
            // 
            this.ctrlBrowseSourceLoropTifFile.AutoSize = true;
            this.ctrlBrowseSourceLoropTifFile.FileName = "";
            this.ctrlBrowseSourceLoropTifFile.Filter = "";
            this.ctrlBrowseSourceLoropTifFile.IsFolderDialog = false;
            this.ctrlBrowseSourceLoropTifFile.IsFullPath = true;
            this.ctrlBrowseSourceLoropTifFile.IsSaveFile = false;
            this.ctrlBrowseSourceLoropTifFile.LabelCaption = "Source Orthophoto Tif File:";
            this.ctrlBrowseSourceLoropTifFile.Location = new System.Drawing.Point(139, 17);
            this.ctrlBrowseSourceLoropTifFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseSourceLoropTifFile.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseSourceLoropTifFile.MultiFilesSelect = false;
            this.ctrlBrowseSourceLoropTifFile.Name = "ctrlBrowseSourceLoropTifFile";
            this.ctrlBrowseSourceLoropTifFile.Prefix = "";
            this.ctrlBrowseSourceLoropTifFile.Size = new System.Drawing.Size(630, 24);
            this.ctrlBrowseSourceLoropTifFile.TabIndex = 1;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(10, 24);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(63, 13);
            this.label31.TabIndex = 14;
            this.label31.Text = "Source File:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(8, 246);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(59, 13);
            this.label32.TabIndex = 28;
            this.label32.Text = "DTM Path:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(3, 20);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(139, 13);
            this.label33.TabIndex = 17;
            this.label33.Text = "Source Orthophoto Tif Files:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(4, 51);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(145, 13);
            this.label34.TabIndex = 18;
            this.label34.Text = "Source Orthophoto Tfw Files:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(8, 22);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(134, 13);
            this.label35.TabIndex = 27;
            this.label35.Text = "Source Orthophoto Tif File:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(5, 462);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(132, 13);
            this.label36.TabIndex = 28;
            this.label36.Text = "Output Orthophoto Tif File:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(3, 492);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(138, 13);
            this.label37.TabIndex = 29;
            this.label37.Text = "Output Orthophoto Tfw File:";
            // 
            // btnFileProductionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(793, 617);
            this.Controls.Add(this.tcFileProductions);
            this.Name = "btnFileProductionForm";
            this.Text = "frmFileProduction";
            this.tcFileProductions.ResumeLayout(false);
            this.tpGetFileParameters.ResumeLayout(false);
            this.tpGetFileParameters.PerformLayout();
            this.tpDTMFromPointsCloud.ResumeLayout(false);
            this.tpDTMFromPointsCloud.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCloudPoints)).EndInit();
            this.tpMosaicFromOrthophotos.ResumeLayout(false);
            this.tpMosaicFromOrthophotos.PerformLayout();
            this.gbGeoReferencingParams.ResumeLayout(false);
            this.gbGeoReferencingParams.PerformLayout();
            this.tpOrthophotoFromLoropImage.ResumeLayout(false);
            this.tpOrthophotoFromLoropImage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcFileProductions;
        private System.Windows.Forms.TabPage tpGetFileParameters;
        private System.Windows.Forms.TabPage tpDTMFromPointsCloud;
        private Controls.CtrlBrowseControl ctrlBrowseSourceFileFullPath;
        private System.Windows.Forms.Label label2;
        private Controls.NumericTextBox ntxImageHeight;
        private System.Windows.Forms.Label label1;
        private Controls.NumericTextBox ntxImageWidth;
        private System.Windows.Forms.CheckBox cbxIsTiled;
        private System.Windows.Forms.Label label5;
        private Controls.NumericTextBox ntxTileHeight;
        private System.Windows.Forms.Label label4;
        private Controls.NumericTextBox ntxTileWidth;
        private System.Windows.Forms.Label label3;
        private Controls.NumericTextBox ntxStripHeight;
        private System.Windows.Forms.Button btnGetFileParameters;
        private Controls.CtrlSamplePoint ctrlSampleCloudPoints;
        private System.Windows.Forms.DataGridView dgvCloudPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colUsePoint;
        private Controls.CtrlBrowseControl ctrlBrowseDTMPath;
        private System.Windows.Forms.Label label6;
        private Controls.NumericTextBox ntxDTMResolution;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Controls.Ctrl2DVector ctrl2DVectorDTMMaxPoint;
        private Controls.Ctrl2DVector ctrl2DVectorDTMMinPoint;
        private System.Windows.Forms.Label label9;
        private Controls.NumericTextBox ntxMaxSearchRadiusForHeightCalculation;
        private System.Windows.Forms.Label label10;
        private Controls.NumericTextBox ntxMinCloudPointsForHeightCalculation;
        private System.Windows.Forms.Button btnGenerateDTMFromPointsCloud;
        private System.Windows.Forms.TabPage tpMosaicFromOrthophotos;
        private Controls.CtrlBrowseControl ctrlBrowseSourceOrthophotoTfwFiles;
        private Controls.CtrlBrowseControl ctrlBrowseSourceOrthophotoTifFiles;
        private Controls.CtrlGridCoordinateSystem ctrlGridCoordinateSystemSourceOrthophotoWorldParams;
        private System.Windows.Forms.ComboBox cmbSourceResamplingMethod;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox gbGeoReferencingParams;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private Controls.Ctrl3DVector ctrl3DVectorSecondCornerInDiagonal;
        private Controls.Ctrl3DVector ctrl3DVectorFirstCornerInDiagonal;
        private Controls.NumericTextBox ntxGSD;
        private Controls.NumericTextBox ntxAzimDeg;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private Controls.NumericTextBox ntxMosaicTileSize;
        private System.Windows.Forms.Label label16;
        private Controls.NumericTextBox ntxMosaicBackgroundGrayLevel;
        private System.Windows.Forms.Label label17;
        private Controls.CtrlGridCoordinateSystem ctrlGridCoordinateSystemMosaicWorldParams;
        private Controls.CtrlBrowseControl ctrlBrowseMosaicTifFile;
        private Controls.CtrlBrowseControl ctrlBrowseControlMosaicTfwFile;
        private Controls.NumericTextBox ntxCompressionFactor;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnGenerateMosaic;
        private System.Windows.Forms.TabPage tpOrthophotoFromLoropImage;
        private Controls.CtrlBrowseControl ctrlBrowseSourceLoropTifFile;
        private Controls.CtrlImageCalc ctrlImageCalcSourceLorop;
        private System.Windows.Forms.ComboBox cmbOrthophotoSourceResamplingMethod;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.NumericTextBox ntOrthophotoGSD;
        private Controls.NumericTextBox ntxOrthophotoAzimDeg;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private Controls.Ctrl3DVector ctrl3DVectorOrthophotoSecondCornerInDiagonal;
        private Controls.Ctrl3DVector ctrl3DVectorOrthophotoFirstCornerInDiagonal;
        private Controls.NumericTextBox ntxOrthophotoBackgroundGrayLevel;
        private System.Windows.Forms.Label label24;
        private Controls.NumericTextBox ntxOrthopothoTileSize;
        private System.Windows.Forms.Label label25;
        private Controls.NumericTextBox ntxOrthophotoDefaultHeight;
        private System.Windows.Forms.Label label26;
        private Controls.CtrlGridCoordinateSystem ctrlGridCoordinateSystemOrthophoto;
        private Controls.CtrlBrowseControl ctrlBrowseOrthophotoTfwFile;
        private Controls.CtrlBrowseControl ctrlBrowseOrthophotoTifFile;
        private System.Windows.Forms.Button btnGenerateOrthophoto;
        private Controls.NumericTextBox ntxOrthophotoCompressionFactor;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.CheckBox cbxDTMMapLayer;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnAddDTMMapLayer;
        private Controls.CtrlSamplePoint ctrlSampleMosaicFromOrthophotosSecondCorner;
        private Controls.CtrlSamplePoint ctrlSampleMosaicFromOrthophotosFirstCorner;
        private Controls.CtrlSamplePoint ctrlSampleOrthophotoFromLoropSecondCorner;
        private Controls.CtrlSamplePoint ctrlSampleOrthophotoFromLoropFirstCorner;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnDrawPtCloudArea;
        private System.Windows.Forms.Label label29;
        private Controls.NumericTextBox ntxNumPtToCastLots;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
    }
}