namespace MCTester.GUI.Forms
{
    partial class SpatialQueriesForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gbRayIntersectionTargetResult = new System.Windows.Forms.GroupBox();
            this.lblTargetCounter = new System.Windows.Forms.Label();
            this.txtIntersectionTargetType = new System.Windows.Forms.TextBox();
            this.txtIntersectionCoordSystem = new System.Windows.Forms.TextBox();
            this.btnNextTarget = new System.Windows.Forms.Button();
            this.btnPreviousTarget = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtItemPart = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ntxPartIndex = new MCTester.Controls.NumericTextBox();
            this.ntxObjectHashCode = new MCTester.Controls.NumericTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.ntxItemHashCode = new MCTester.Controls.NumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ntxTargetIndex = new MCTester.Controls.NumericTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ntxLayerHashCode = new MCTester.Controls.NumericTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.ntxTerrainHashCode = new MCTester.Controls.NumericTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ctrl3DVectorIntersectionPoint = new MCTester.Controls.Ctrl3DVector();
            this.label7 = new System.Windows.Forms.Label();
            this.gbRayIntersectionResult = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrl3DIntersectionPt = new MCTester.Controls.Ctrl3DVector();
            this.ntxDistance = new MCTester.Controls.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ctrl3DNormalRayIntersection = new MCTester.Controls.Ctrl3DVector();
            this.chxIsIntersectionFound = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnGetRayIntersectionTarget = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetRayIntersection = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTerrainNumCacheTilesOK = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.gbTerrainQueriesNumCacheTiles = new System.Windows.Forms.GroupBox();
            this.btnTerrainNumCacheTilesGet = new System.Windows.Forms.Button();
            this.label98 = new System.Windows.Forms.Label();
            this.cmbLayerTypes = new System.Windows.Forms.ComboBox();
            this.lstTerrain = new System.Windows.Forms.ListBox();
            this.ntxNumTiles = new MCTester.Controls.NumericTextBox();
            this.tcSpatialQueries = new System.Windows.Forms.TabControl();
            this.tabPageQueryParams = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.btnShowSelectedDTMLayerData = new System.Windows.Forms.Button();
            this.lstQuerySecondaryDtmLayers = new System.Windows.Forms.ListBox();
            this.ctrlQueryParams1 = new MCTester.Controls.CtrlQueryParams();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.LocationFromTwoDistancesAndAzimuth = new System.Windows.Forms.GroupBox();
            this.cbLocationFromDistancesAndAzimuthFoundHeight = new System.Windows.Forms.CheckBox();
            this.ntxDifferenceHeightsFromResultAndTerrain = new MCTester.Controls.NumericTextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.ntxDistanceFromSecondLocationAndResult = new MCTester.Controls.NumericTextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.ntxDistanceFromFirstLocationAndResult = new MCTester.Controls.NumericTextBox();
            this.label101 = new System.Windows.Forms.Label();
            this.cbLocationFromTwoDistancesAndAzimuthAsync = new System.Windows.Forms.CheckBox();
            this.btnGetLocation = new System.Windows.Forms.Button();
            this.ctrl3DLocationResult = new MCTester.Controls.Ctrl3DVector();
            this.label97 = new System.Windows.Forms.Label();
            this.ntxTargetHeightAboveGround = new MCTester.Controls.NumericTextBox();
            this.label94 = new System.Windows.Forms.Label();
            this.ntxSecondDistance = new MCTester.Controls.NumericTextBox();
            this.label95 = new System.Windows.Forms.Label();
            this.ctrl3DLocationScndOrgnPt = new MCTester.Controls.Ctrl3DVector();
            this.ctrlLocationScndOrgnPt = new MCTester.Controls.CtrlSamplePoint();
            this.label96 = new System.Windows.Forms.Label();
            this.ntxFirstAzimut = new MCTester.Controls.NumericTextBox();
            this.label93 = new System.Windows.Forms.Label();
            this.ntxFirstDistance = new MCTester.Controls.NumericTextBox();
            this.label92 = new System.Windows.Forms.Label();
            this.ctrl3DLocationFrstOrgnPt = new MCTester.Controls.Ctrl3DVector();
            this.ctrlLocationFrstOrgnPt = new MCTester.Controls.CtrlSamplePoint();
            this.label91 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.label90 = new System.Windows.Forms.Label();
            this.ntxSmoothDistance = new MCTester.Controls.NumericTextBox();
            this.btnGetSmoothedPath = new System.Windows.Forms.Button();
            this.btnDrawPath = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label69 = new System.Windows.Forms.Label();
            this.chxThrowException = new System.Windows.Forms.CheckBox();
            this.ntxNumTestingPt = new MCTester.Controls.NumericTextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.ntxDurationTime = new MCTester.Controls.NumericTextBox();
            this.cbxIsLogThreadTest = new System.Windows.Forms.CheckBox();
            this.ctrl3DVectorFirstPoint = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSamplePointFirstPoint = new MCTester.Controls.CtrlSamplePoint();
            this.ntxNumOfThread = new MCTester.Controls.NumericTextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.btnStartMultiThreadTest = new System.Windows.Forms.Button();
            this.label85 = new System.Windows.Forms.Label();
            this.gbTerrainAngles = new System.Windows.Forms.GroupBox();
            this.cbTerrainAnglesAsync = new System.Windows.Forms.CheckBox();
            this.btnGetTerrainAngles = new System.Windows.Forms.Button();
            this.txtPitch = new System.Windows.Forms.TextBox();
            this.txtRoll = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.ntxTerrainAnglesAzimuth = new MCTester.Controls.NumericTextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.ctrl3DVectorTerrainAnglesPt = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSamplePointTerrainAngles = new MCTester.Controls.CtrlSamplePoint();
            this.btnTerrainAnglesDrawLine = new System.Windows.Forms.Button();
            this.tpHeights = new System.Windows.Forms.TabPage();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.ntxNumHorizontalPoints = new MCTester.Controls.NumericTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.ntxNumVerticalPoints = new MCTester.Controls.NumericTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.ntxHorizontalResolution = new MCTester.Controls.NumericTextBox();
            this.cbTerrainMatrixAsync = new System.Windows.Forms.CheckBox();
            this.ctrl3DLowerLeftPoint = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSamplePoint3 = new MCTester.Controls.CtrlSamplePoint();
            this.btnTerrainHeightMatrix = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ntxVerticalResolution = new MCTester.Controls.NumericTextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.ctrlExtremeHeightPoints = new MCTester.Controls.CtrlPointsGrid();
            this.btnExtremeUpdatePolygon = new System.Windows.Forms.Button();
            this.btnExtremeHeightPointsInPolygon = new System.Windows.Forms.Button();
            this.cbExtremeHeightPointsInPolygonAsync = new System.Windows.Forms.CheckBox();
            this.chxPointsFound = new System.Windows.Forms.CheckBox();
            this.ctrl3DVectorLowestPt = new MCTester.Controls.Ctrl3DVector();
            this.btnDrawPolygon = new System.Windows.Forms.Button();
            this.ctrl3DVectorHighestPt = new MCTester.Controls.Ctrl3DVector();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnGetHeightAlongLine = new System.Windows.Forms.Button();
            this.cbTerrainHeightAlongLineAsync = new System.Windows.Forms.CheckBox();
            this.ntxHeightDelta = new MCTester.Controls.NumericTextBox();
            this.ntxMinSlope = new MCTester.Controls.NumericTextBox();
            this.ntxMaxSlope = new MCTester.Controls.NumericTextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.btnDrawLine = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.cbTerrainHeightAsync = new System.Windows.Forms.CheckBox();
            this.chxHeightFound = new System.Windows.Forms.CheckBox();
            this.ctrl3DTerrainHeightPt = new MCTester.Controls.Ctrl3DVector();
            this.ctrlRelativeHeightPt = new MCTester.Controls.CtrlSamplePoint();
            this.btnGetTerrainHeight3 = new System.Windows.Forms.Button();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.ctrl3DRequestedNormal = new MCTester.Controls.Ctrl3DVector();
            this.ntxHeight = new MCTester.Controls.NumericTextBox();
            this.label106 = new System.Windows.Forms.Label();
            this.tpRayIntersection = new System.Windows.Forms.TabPage();
            this.cbRayIntersectionAsync = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbRayDirection = new System.Windows.Forms.RadioButton();
            this.ctrlRayDestination = new MCTester.Controls.CtrlSamplePoint();
            this.rbRayDestination = new System.Windows.Forms.RadioButton();
            this.ctrl3DRayDestination = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DRayDirection = new MCTester.Controls.Ctrl3DVector();
            this.ctrlRayOrigin = new MCTester.Controls.CtrlSamplePoint();
            this.ctrl3DRayOrigin = new MCTester.Controls.Ctrl3DVector();
            this.ntxMaxDistance = new MCTester.Controls.NumericTextBox();
            this.tpSightLines = new System.Windows.Forms.TabPage();
            this.tcSightLines = new System.Windows.Forms.TabControl();
            this.tpLineOfSight = new System.Windows.Forms.TabPage();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.chxMinimalScouterHeight = new System.Windows.Forms.CheckBox();
            this.chxMinimalTargetHeight = new System.Windows.Forms.CheckBox();
            this.cbGetLineOfSightAsync = new System.Windows.Forms.CheckBox();
            this.cbGetPointVisibilityAsync = new System.Windows.Forms.CheckBox();
            this.label100 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.ntxCrestClearanceDistance = new MCTester.Controls.NumericTextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.gbLineOfSightGeneralParams = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.chxLOSIsTargetHeightAbsolute = new System.Windows.Forms.CheckBox();
            this.chxLOSIsScouterHeightAbsolute = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.ctrlSamplePointLOSTarget = new MCTester.Controls.CtrlSamplePoint();
            this.label32 = new System.Windows.Forms.Label();
            this.ntxLOSMinPitchAngle = new MCTester.Controls.NumericTextBox();
            this.ctrl3DVectorLOSTarget = new MCTester.Controls.Ctrl3DVector();
            this.ntxLOSMaxPitchAngle = new MCTester.Controls.NumericTextBox();
            this.ctrl3DVectorLOSScouter = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSamplePointLOSScouter = new MCTester.Controls.CtrlSamplePoint();
            this.label33 = new System.Windows.Forms.Label();
            this.ntxCrestClearanceAngle = new MCTester.Controls.NumericTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label44 = new System.Windows.Forms.Label();
            this.chxLOSIsTargetVisible = new System.Windows.Forms.CheckBox();
            this.btnGetLineOfSightPoints = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.ntxLOSMinimalTargetHeightForVisibility = new MCTester.Controls.NumericTextBox();
            this.btnGetLineOfSight = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.ntxLOSMinimalScouterHeightForVisibility = new MCTester.Controls.NumericTextBox();
            this.tpAreaOfSight = new System.Windows.Forms.TabPage();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.btnAOSExport = new System.Windows.Forms.Button();
            this.btnAOSImport = new System.Windows.Forms.Button();
            this.rgbSightObjectRect = new MCTester.Controls.CtrlRadioGroupBox();
            this.lblRectHeight = new System.Windows.Forms.Label();
            this.ntxRectHeight = new MCTester.Controls.NumericTextBox();
            this.lblRectWidth = new System.Windows.Forms.Label();
            this.ntxRectWidth = new MCTester.Controls.NumericTextBox();
            this.rgbSightObjectEllipse = new MCTester.Controls.CtrlRadioGroupBox();
            this.ntxAOSRadiusY = new MCTester.Controls.NumericTextBox();
            this.ntxAOSRadiusX = new MCTester.Controls.NumericTextBox();
            this.ntxAOSStartAngle = new MCTester.Controls.NumericTextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.ntxAOSEndAngle = new MCTester.Controls.NumericTextBox();
            this.btnUpdateEllipse = new System.Windows.Forms.Button();
            this.ctrlPointsGridSightObject = new MCTester.Controls.CtrlPointsGrid();
            this.btnDrawEllipseAreaOfSight = new System.Windows.Forms.Button();
            this.rbSightObjectPolygon = new System.Windows.Forms.RadioButton();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.gbAOSColors = new System.Windows.Forms.GroupBox();
            this.cmbAOSColors = new System.Windows.Forms.ComboBox();
            this.label121 = new System.Windows.Forms.Label();
            this.picbColor = new System.Windows.Forms.PictureBox();
            this.label117 = new System.Windows.Forms.Label();
            this.numUDAlphaColor = new System.Windows.Forms.NumericUpDown();
            this.label131 = new System.Windows.Forms.Label();
            this.ntxNumCallingFunction = new MCTester.Controls.NumericTextBox();
            this.cbIsMultipleScouters = new System.Windows.Forms.CheckBox();
            this.gbMultipleScouters = new System.Windows.Forms.GroupBox();
            this.ctrlPointsGridMultipleScouters = new MCTester.Controls.CtrlPointsGrid();
            this.cbCalcBestPointsAsync = new System.Windows.Forms.CheckBox();
            this.label83 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.ctrl3DScouterCenterPoint = new MCTester.Controls.Ctrl3DVector();
            this.cmbEScoutersSumType = new System.Windows.Forms.ComboBox();
            this.label52 = new System.Windows.Forms.Label();
            this.ctrlSamplePointMultipleScouter = new MCTester.Controls.CtrlSamplePoint();
            this.ntxMaxNumOfScouters = new MCTester.Controls.NumericTextBox();
            this.btnCalcBestPoints = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.ctrlSamplePointScouter = new MCTester.Controls.CtrlSamplePoint();
            this.label51 = new System.Windows.Forms.Label();
            this.ntxScouterRadiusX = new MCTester.Controls.NumericTextBox();
            this.ntxScouterRadiusY = new MCTester.Controls.NumericTextBox();
            this.ctrl3DVectorAOSScouter = new MCTester.Controls.Ctrl3DVector();
            this.label84 = new System.Windows.Forms.Label();
            this.ntxRotationAzimuthDeg = new MCTester.Controls.NumericTextBox();
            this.ctrlSamplePointAOSScouter = new MCTester.Controls.CtrlSamplePoint();
            this.label119 = new System.Windows.Forms.Label();
            this.cbAreaOfSightAsync = new System.Windows.Forms.CheckBox();
            this.tbNameCalc = new System.Windows.Forms.TextBox();
            this.btnCalcAOS2 = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.chxCalcStaticObjects = new System.Windows.Forms.CheckBox();
            this.chxCalcUnseenPolygons = new System.Windows.Forms.CheckBox();
            this.chxCalcSeenPolygons = new System.Windows.Forms.CheckBox();
            this.chxCalcLineOfSight = new System.Windows.Forms.CheckBox();
            this.chxCalcAreaOfSight = new System.Windows.Forms.CheckBox();
            this.ntxNumberOfRayes = new MCTester.Controls.NumericTextBox();
            this.chxGPU_Based = new System.Windows.Forms.CheckBox();
            this.chxAOSIsScouterHeightAbsolute = new System.Windows.Forms.CheckBox();
            this.label122 = new System.Windows.Forms.Label();
            this.ntxAOSMaxPitchAngle = new MCTester.Controls.NumericTextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.ntxAOSMinPitchAngle = new MCTester.Controls.NumericTextBox();
            this.chxAOSIsTargetsHeightAbsolute = new System.Windows.Forms.CheckBox();
            this.label124 = new System.Windows.Forms.Label();
            this.ntxAOSTargetHeight = new MCTester.Controls.NumericTextBox();
            this.label125 = new System.Windows.Forms.Label();
            this.ntxAOSGPUTargetResolution = new MCTester.Controls.NumericTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.chxAreSameMatrices = new System.Windows.Forms.CheckBox();
            this.btnTestSaveAndLoad = new System.Windows.Forms.Button();
            this.btnAOSLoad = new System.Windows.Forms.Button();
            this.btnAOSSave = new System.Windows.Forms.Button();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.btnSaveMatrixToFile = new System.Windows.Forms.Button();
            this.chxFillPointsVisibility = new System.Windows.Forms.CheckBox();
            this.chxIsCreateAndDrawMatrixAutomatic = new System.Windows.Forms.CheckBox();
            this.btnCreateMatrix = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label67 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.btnAOSGetPointVisibility = new System.Windows.Forms.Button();
            this.txtAOSPointVisibilityAnswer = new System.Windows.Forms.TextBox();
            this.ctrl3DVectorAOSPointVisibility = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSamplePointAOSPointVisibility = new MCTester.Controls.CtrlSamplePoint();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.btnCalcCoverage = new System.Windows.Forms.Button();
            this.label77 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.cmbQualityParamsTargetTypes = new System.Windows.Forms.ComboBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.btnQualityParamsDrawLine = new System.Windows.Forms.Button();
            this.label74 = new System.Windows.Forms.Label();
            this.ntxCoverageQuality = new MCTester.Controls.NumericTextBox();
            this.ntxWalkingRadius = new MCTester.Controls.NumericTextBox();
            this.ntxVehicleRadius = new MCTester.Controls.NumericTextBox();
            this.ntxStandingRadius = new MCTester.Controls.NumericTextBox();
            this.ntxCellFactor = new MCTester.Controls.NumericTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbHexadecimelNumber = new System.Windows.Forms.CheckBox();
            this.btnGetPointVisibilityColorsSurrounding = new System.Windows.Forms.Button();
            this.dgvSurroundingResults = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label79 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.ntxSurroundingRadiusX = new MCTester.Controls.NumericTextBox();
            this.ntxSurroundingRadiusY = new MCTester.Controls.NumericTextBox();
            this.ctrl3DSurroundingPoint = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSamplePoint4 = new MCTester.Controls.CtrlSamplePoint();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label36 = new System.Windows.Forms.Label();
            this.cmbEScoutersSumTypeMatrices = new System.Windows.Forms.ComboBox();
            this.cbCloneMatrixFillPoints = new System.Windows.Forms.CheckBox();
            this.cbAreSameRect = new System.Windows.Forms.CheckBox();
            this.btnAreSameRectAreaOfSightMatrices = new System.Windows.Forms.Button();
            this.btnSumAreaOfSightMatrices = new System.Windows.Forms.Button();
            this.btnCloneAreaOfSightMatrix = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.cbLstAreaOfSightMatrix = new System.Windows.Forms.CheckedListBox();
            this.grpShowOperations = new System.Windows.Forms.GroupBox();
            this.chxDrawSeenStaticObjects = new System.Windows.Forms.CheckBox();
            this.chxDrawObject = new System.Windows.Forms.CheckBox();
            this.chxDrawAreaOfSight = new System.Windows.Forms.CheckBox();
            this.chxDrawUnseenPolygons = new System.Windows.Forms.CheckBox();
            this.chxDrawSeenPolygons = new System.Windows.Forms.CheckBox();
            this.chxDrawLineOfSight = new System.Windows.Forms.CheckBox();
            this.tpRasterAndTraversability = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ctrlTraversabilityAlongLinePoints = new MCTester.Controls.CtrlPointsGrid();
            this.btnTALUpdateLine = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.lbTraversabilityMapLayers = new System.Windows.Forms.ListBox();
            this.btnGetTraversabilityAlongLine = new System.Windows.Forms.Button();
            this.cbGetTraversabilityAlongLineAsync = new System.Windows.Forms.CheckBox();
            this.btnDrawLineTraversability = new System.Windows.Forms.Button();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.btnGetTraversabilityFromColorCode = new System.Windows.Forms.Button();
            this.gbGetTraversabilityFromColorCode = new System.Windows.Forms.GroupBox();
            this.dgvGetTraversabilityFromColorCode = new System.Windows.Forms.DataGridView();
            this.DirectionAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label130 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.pbGetRasterColor = new System.Windows.Forms.PictureBox();
            this.ctrlLocationGetRaster = new MCTester.Controls.CtrlSamplePoint();
            this.ntbGetRasterColorR = new MCTester.Controls.NumericTextBox();
            this.ntbGetRasterColorB = new MCTester.Controls.NumericTextBox();
            this.label128 = new System.Windows.Forms.Label();
            this.ntbGetRasterColorA = new MCTester.Controls.NumericTextBox();
            this.label127 = new System.Windows.Forms.Label();
            this.ntbGetRasterColorG = new MCTester.Controls.NumericTextBox();
            this.label126 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.cbNearestPixel = new System.Windows.Forms.CheckBox();
            this.btnGetRasterLayerColorByPoint = new System.Windows.Forms.Button();
            this.cbGetRasterAsync = new System.Windows.Forms.CheckBox();
            this.lbRasterMapLayer = new System.Windows.Forms.ListBox();
            this.ntbLOD = new MCTester.Controls.NumericTextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.ctrl3DGetRasterPoint = new MCTester.Controls.Ctrl3DVector();
            this.label50 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.dgvAsyncQueryResults = new System.Windows.Forms.DataGridView();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameFunction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Async = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ErrorCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label54 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label56 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label57 = new System.Windows.Forms.Label();
            this.numericTextBox3 = new MCTester.Controls.NumericTextBox();
            this.ctrlSamplePoint1 = new MCTester.Controls.CtrlSamplePoint();
            this.label58 = new System.Windows.Forms.Label();
            this.ctrl3DVector1 = new MCTester.Controls.Ctrl3DVector();
            this.numericTextBox4 = new MCTester.Controls.NumericTextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label61 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label62 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label63 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.chxDrawSeenStaticObjects2 = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnCancelAsyncQuery = new System.Windows.Forms.Button();
            this.numericTextBox1 = new MCTester.Controls.NumericTextBox();
            this.numericTextBox2 = new MCTester.Controls.NumericTextBox();
            this.numericTextBox5 = new MCTester.Controls.NumericTextBox();
            this.numericTextBox6 = new MCTester.Controls.NumericTextBox();
            this.numericTextBox7 = new MCTester.Controls.NumericTextBox();
            this.numericTextBox8 = new MCTester.Controls.NumericTextBox();
            this.numericTextBox9 = new MCTester.Controls.NumericTextBox();
            this.numericTextBox10 = new MCTester.Controls.NumericTextBox();
            this.ctrlSamplePoint2 = new MCTester.Controls.CtrlSamplePoint();
            this.numericTextBox11 = new MCTester.Controls.NumericTextBox();
            this.gbRayIntersectionTargetResult.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbRayIntersectionResult.SuspendLayout();
            this.gbTerrainQueriesNumCacheTiles.SuspendLayout();
            this.tcSpatialQueries.SuspendLayout();
            this.tabPageQueryParams.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.LocationFromTwoDistancesAndAzimuth.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbTerrainAngles.SuspendLayout();
            this.tpHeights.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tpRayIntersection.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tpSightLines.SuspendLayout();
            this.tcSightLines.SuspendLayout();
            this.tpLineOfSight.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.gbLineOfSightGeneralParams.SuspendLayout();
            this.tpAreaOfSight.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.rgbSightObjectRect.SuspendLayout();
            this.rgbSightObjectEllipse.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.gbAOSColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlphaColor)).BeginInit();
            this.gbMultipleScouters.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSurroundingResults)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.grpShowOperations.SuspendLayout();
            this.tpRasterAndTraversability.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.gbGetTraversabilityFromColorCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGetTraversabilityFromColorCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGetRasterColor)).BeginInit();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsyncQueryResults)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbRayIntersectionTargetResult
            // 
            this.gbRayIntersectionTargetResult.Controls.Add(this.lblTargetCounter);
            this.gbRayIntersectionTargetResult.Controls.Add(this.txtIntersectionTargetType);
            this.gbRayIntersectionTargetResult.Controls.Add(this.txtIntersectionCoordSystem);
            this.gbRayIntersectionTargetResult.Controls.Add(this.btnNextTarget);
            this.gbRayIntersectionTargetResult.Controls.Add(this.btnPreviousTarget);
            this.gbRayIntersectionTargetResult.Controls.Add(this.label20);
            this.gbRayIntersectionTargetResult.Controls.Add(this.panel1);
            this.gbRayIntersectionTargetResult.Controls.Add(this.ntxTargetIndex);
            this.gbRayIntersectionTargetResult.Controls.Add(this.label15);
            this.gbRayIntersectionTargetResult.Controls.Add(this.ntxLayerHashCode);
            this.gbRayIntersectionTargetResult.Controls.Add(this.label14);
            this.gbRayIntersectionTargetResult.Controls.Add(this.ntxTerrainHashCode);
            this.gbRayIntersectionTargetResult.Controls.Add(this.label13);
            this.gbRayIntersectionTargetResult.Controls.Add(this.label12);
            this.gbRayIntersectionTargetResult.Controls.Add(this.label11);
            this.gbRayIntersectionTargetResult.Controls.Add(this.ctrl3DVectorIntersectionPoint);
            this.gbRayIntersectionTargetResult.Controls.Add(this.label7);
            this.gbRayIntersectionTargetResult.Location = new System.Drawing.Point(8, 176);
            this.gbRayIntersectionTargetResult.Name = "gbRayIntersectionTargetResult";
            this.gbRayIntersectionTargetResult.Size = new System.Drawing.Size(382, 342);
            this.gbRayIntersectionTargetResult.TabIndex = 80;
            this.gbRayIntersectionTargetResult.TabStop = false;
            this.gbRayIntersectionTargetResult.Text = "Ray Intersection Target Result";
            // 
            // lblTargetCounter
            // 
            this.lblTargetCounter.AutoSize = true;
            this.lblTargetCounter.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.lblTargetCounter.Enabled = false;
            this.lblTargetCounter.Location = new System.Drawing.Point(135, 321);
            this.lblTargetCounter.Name = "lblTargetCounter";
            this.lblTargetCounter.Size = new System.Drawing.Size(36, 13);
            this.lblTargetCounter.TabIndex = 100;
            this.lblTargetCounter.Text = "<0/0>";
            // 
            // txtIntersectionTargetType
            // 
            this.txtIntersectionTargetType.Location = new System.Drawing.Point(138, 16);
            this.txtIntersectionTargetType.Name = "txtIntersectionTargetType";
            this.txtIntersectionTargetType.ReadOnly = true;
            this.txtIntersectionTargetType.Size = new System.Drawing.Size(185, 20);
            this.txtIntersectionTargetType.TabIndex = 81;
            // 
            // txtIntersectionCoordSystem
            // 
            this.txtIntersectionCoordSystem.Location = new System.Drawing.Point(138, 75);
            this.txtIntersectionCoordSystem.Name = "txtIntersectionCoordSystem";
            this.txtIntersectionCoordSystem.ReadOnly = true;
            this.txtIntersectionCoordSystem.Size = new System.Drawing.Size(185, 20);
            this.txtIntersectionCoordSystem.TabIndex = 82;
            // 
            // btnNextTarget
            // 
            this.btnNextTarget.Location = new System.Drawing.Point(330, 316);
            this.btnNextTarget.Name = "btnNextTarget";
            this.btnNextTarget.Size = new System.Drawing.Size(40, 23);
            this.btnNextTarget.TabIndex = 99;
            this.btnNextTarget.Text = ">>";
            this.btnNextTarget.UseVisualStyleBackColor = true;
            this.btnNextTarget.Click += new System.EventHandler(this.btnNextTarget_Click);
            // 
            // btnPreviousTarget
            // 
            this.btnPreviousTarget.Location = new System.Drawing.Point(283, 316);
            this.btnPreviousTarget.Name = "btnPreviousTarget";
            this.btnPreviousTarget.Size = new System.Drawing.Size(40, 23);
            this.btnPreviousTarget.TabIndex = 98;
            this.btnPreviousTarget.Text = "<<";
            this.btnPreviousTarget.UseVisualStyleBackColor = true;
            this.btnPreviousTarget.Click += new System.EventHandler(this.btnPreviousTarget_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label20.Location = new System.Drawing.Point(6, 178);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 13);
            this.label20.TabIndex = 97;
            this.label20.Text = "Object Item Found:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtItemPart);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.ntxPartIndex);
            this.panel1.Controls.Add(this.ntxObjectHashCode);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.ntxItemHashCode);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Location = new System.Drawing.Point(9, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 118);
            this.panel1.TabIndex = 96;
            // 
            // txtItemPart
            // 
            this.txtItemPart.Location = new System.Drawing.Point(129, 58);
            this.txtItemPart.Name = "txtItemPart";
            this.txtItemPart.ReadOnly = true;
            this.txtItemPart.Size = new System.Drawing.Size(185, 20);
            this.txtItemPart.TabIndex = 83;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(97, 13);
            this.label16.TabIndex = 88;
            this.label16.Text = "Object Hash Code:";
            // 
            // ntxPartIndex
            // 
            this.ntxPartIndex.Location = new System.Drawing.Point(129, 85);
            this.ntxPartIndex.Name = "ntxPartIndex";
            this.ntxPartIndex.ReadOnly = true;
            this.ntxPartIndex.Size = new System.Drawing.Size(121, 20);
            this.ntxPartIndex.TabIndex = 95;
            this.ntxPartIndex.Text = "0";
            // 
            // ntxObjectHashCode
            // 
            this.ntxObjectHashCode.Location = new System.Drawing.Point(129, 7);
            this.ntxObjectHashCode.Name = "ntxObjectHashCode";
            this.ntxObjectHashCode.ReadOnly = true;
            this.ntxObjectHashCode.Size = new System.Drawing.Size(121, 20);
            this.ntxObjectHashCode.TabIndex = 89;
            this.ntxObjectHashCode.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 88);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 13);
            this.label19.TabIndex = 94;
            this.label19.Text = "Part Index:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 35);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 13);
            this.label17.TabIndex = 90;
            this.label17.Text = "Item Hash Code:";
            // 
            // ntxItemHashCode
            // 
            this.ntxItemHashCode.Location = new System.Drawing.Point(129, 32);
            this.ntxItemHashCode.Name = "ntxItemHashCode";
            this.ntxItemHashCode.ReadOnly = true;
            this.ntxItemHashCode.Size = new System.Drawing.Size(121, 20);
            this.ntxItemHashCode.TabIndex = 91;
            this.ntxItemHashCode.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(52, 13);
            this.label18.TabIndex = 92;
            this.label18.Text = "Item Part:";
            // 
            // ntxTargetIndex
            // 
            this.ntxTargetIndex.Location = new System.Drawing.Point(138, 154);
            this.ntxTargetIndex.Name = "ntxTargetIndex";
            this.ntxTargetIndex.ReadOnly = true;
            this.ntxTargetIndex.Size = new System.Drawing.Size(121, 20);
            this.ntxTargetIndex.TabIndex = 87;
            this.ntxTargetIndex.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 157);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 13);
            this.label15.TabIndex = 86;
            this.label15.Text = "Target Index:";
            // 
            // ntxLayerHashCode
            // 
            this.ntxLayerHashCode.Location = new System.Drawing.Point(138, 128);
            this.ntxLayerHashCode.Name = "ntxLayerHashCode";
            this.ntxLayerHashCode.ReadOnly = true;
            this.ntxLayerHashCode.Size = new System.Drawing.Size(121, 20);
            this.ntxLayerHashCode.TabIndex = 85;
            this.ntxLayerHashCode.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 84;
            this.label14.Text = "Layer Hash Code:";
            // 
            // ntxTerrainHashCode
            // 
            this.ntxTerrainHashCode.Location = new System.Drawing.Point(138, 102);
            this.ntxTerrainHashCode.Name = "ntxTerrainHashCode";
            this.ntxTerrainHashCode.ReadOnly = true;
            this.ntxTerrainHashCode.Size = new System.Drawing.Size(121, 20);
            this.ntxTerrainHashCode.TabIndex = 83;
            this.ntxTerrainHashCode.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 13);
            this.label13.TabIndex = 82;
            this.label13.Text = "Terrain Hash Code:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 78);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 75;
            this.label12.Text = "Coordinate System:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 74;
            this.label11.Text = "Intersection Point:";
            // 
            // ctrl3DVectorIntersectionPoint
            // 
            this.ctrl3DVectorIntersectionPoint.IsReadOnly = true;
            this.ctrl3DVectorIntersectionPoint.Location = new System.Drawing.Point(138, 43);
            this.ctrl3DVectorIntersectionPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorIntersectionPoint.Name = "ctrl3DVectorIntersectionPoint";
            this.ctrl3DVectorIntersectionPoint.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorIntersectionPoint.TabIndex = 73;
            this.ctrl3DVectorIntersectionPoint.X = 0D;
            this.ctrl3DVectorIntersectionPoint.Y = 0D;
            this.ctrl3DVectorIntersectionPoint.Z = 0D;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Intersection Target Type:";
            // 
            // gbRayIntersectionResult
            // 
            this.gbRayIntersectionResult.Controls.Add(this.label1);
            this.gbRayIntersectionResult.Controls.Add(this.label6);
            this.gbRayIntersectionResult.Controls.Add(this.ctrl3DIntersectionPt);
            this.gbRayIntersectionResult.Controls.Add(this.ntxDistance);
            this.gbRayIntersectionResult.Controls.Add(this.label5);
            this.gbRayIntersectionResult.Controls.Add(this.ctrl3DNormalRayIntersection);
            this.gbRayIntersectionResult.Controls.Add(this.chxIsIntersectionFound);
            this.gbRayIntersectionResult.Location = new System.Drawing.Point(396, 176);
            this.gbRayIntersectionResult.Name = "gbRayIntersectionResult";
            this.gbRayIntersectionResult.Size = new System.Drawing.Size(324, 111);
            this.gbRayIntersectionResult.TabIndex = 81;
            this.gbRayIntersectionResult.TabStop = false;
            this.gbRayIntersectionResult.Text = "Ray Intersection Result";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "Normal:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Distance:";
            // 
            // ctrl3DIntersectionPt
            // 
            this.ctrl3DIntersectionPt.IsReadOnly = true;
            this.ctrl3DIntersectionPt.Location = new System.Drawing.Point(88, 52);
            this.ctrl3DIntersectionPt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DIntersectionPt.Name = "ctrl3DIntersectionPt";
            this.ctrl3DIntersectionPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DIntersectionPt.TabIndex = 79;
            this.ctrl3DIntersectionPt.X = 0D;
            this.ctrl3DIntersectionPt.Y = 0D;
            this.ctrl3DIntersectionPt.Z = 0D;
            // 
            // ntxDistance
            // 
            this.ntxDistance.Location = new System.Drawing.Point(88, 84);
            this.ntxDistance.Name = "ntxDistance";
            this.ntxDistance.ReadOnly = true;
            this.ntxDistance.Size = new System.Drawing.Size(80, 20);
            this.ntxDistance.TabIndex = 81;
            this.ntxDistance.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 78;
            this.label5.Text = "Intersection:";
            // 
            // ctrl3DNormalRayIntersection
            // 
            this.ctrl3DNormalRayIntersection.IsReadOnly = true;
            this.ctrl3DNormalRayIntersection.Location = new System.Drawing.Point(88, 20);
            this.ctrl3DNormalRayIntersection.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DNormalRayIntersection.Name = "ctrl3DNormalRayIntersection";
            this.ctrl3DNormalRayIntersection.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DNormalRayIntersection.TabIndex = 86;
            this.ctrl3DNormalRayIntersection.X = 0D;
            this.ctrl3DNormalRayIntersection.Y = 0D;
            this.ctrl3DNormalRayIntersection.Z = 0D;
            // 
            // chxIsIntersectionFound
            // 
            this.chxIsIntersectionFound.AutoSize = true;
            this.chxIsIntersectionFound.Enabled = false;
            this.chxIsIntersectionFound.Location = new System.Drawing.Point(174, 86);
            this.chxIsIntersectionFound.Name = "chxIsIntersectionFound";
            this.chxIsIntersectionFound.Size = new System.Drawing.Size(128, 17);
            this.chxIsIntersectionFound.TabIndex = 77;
            this.chxIsIntersectionFound.Text = "Is Intersection Found:";
            this.chxIsIntersectionFound.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 87;
            this.label8.Text = "Ray Destination:";
            // 
            // btnGetRayIntersectionTarget
            // 
            this.btnGetRayIntersectionTarget.Location = new System.Drawing.Point(8, 146);
            this.btnGetRayIntersectionTarget.Name = "btnGetRayIntersectionTarget";
            this.btnGetRayIntersectionTarget.Size = new System.Drawing.Size(158, 23);
            this.btnGetRayIntersectionTarget.TabIndex = 85;
            this.btnGetRayIntersectionTarget.Text = "Ray Intersection Targets";
            this.btnGetRayIntersectionTarget.UseVisualStyleBackColor = true;
            this.btnGetRayIntersectionTarget.Click += new System.EventHandler(this.btnGetRayIntersectionTarget_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "Ray Direction:";
            // 
            // btnGetRayIntersection
            // 
            this.btnGetRayIntersection.Location = new System.Drawing.Point(404, 146);
            this.btnGetRayIntersection.Name = "btnGetRayIntersection";
            this.btnGetRayIntersection.Size = new System.Drawing.Size(128, 23);
            this.btnGetRayIntersection.TabIndex = 82;
            this.btnGetRayIntersection.Text = "Ray Intersection";
            this.btnGetRayIntersection.UseVisualStyleBackColor = true;
            this.btnGetRayIntersection.Click += new System.EventHandler(this.btnGetRayIntersection_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(392, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Max Distance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Ray Origin:";
            // 
            // btnTerrainNumCacheTilesOK
            // 
            this.btnTerrainNumCacheTilesOK.Location = new System.Drawing.Point(185, 121);
            this.btnTerrainNumCacheTilesOK.Name = "btnTerrainNumCacheTilesOK";
            this.btnTerrainNumCacheTilesOK.Size = new System.Drawing.Size(33, 22);
            this.btnTerrainNumCacheTilesOK.TabIndex = 92;
            this.btnTerrainNumCacheTilesOK.Text = "Set";
            this.btnTerrainNumCacheTilesOK.UseVisualStyleBackColor = true;
            this.btnTerrainNumCacheTilesOK.Click += new System.EventHandler(this.btnTerrainNumCacheTilesOK_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(10, 125);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(57, 13);
            this.label21.TabIndex = 91;
            this.label21.Text = "Num Tiles:";
            // 
            // gbTerrainQueriesNumCacheTiles
            // 
            this.gbTerrainQueriesNumCacheTiles.Controls.Add(this.btnTerrainNumCacheTilesGet);
            this.gbTerrainQueriesNumCacheTiles.Controls.Add(this.label98);
            this.gbTerrainQueriesNumCacheTiles.Controls.Add(this.cmbLayerTypes);
            this.gbTerrainQueriesNumCacheTiles.Controls.Add(this.lstTerrain);
            this.gbTerrainQueriesNumCacheTiles.Controls.Add(this.btnTerrainNumCacheTilesOK);
            this.gbTerrainQueriesNumCacheTiles.Controls.Add(this.label21);
            this.gbTerrainQueriesNumCacheTiles.Controls.Add(this.ntxNumTiles);
            this.gbTerrainQueriesNumCacheTiles.Location = new System.Drawing.Point(408, 7);
            this.gbTerrainQueriesNumCacheTiles.Name = "gbTerrainQueriesNumCacheTiles";
            this.gbTerrainQueriesNumCacheTiles.Size = new System.Drawing.Size(225, 152);
            this.gbTerrainQueriesNumCacheTiles.TabIndex = 94;
            this.gbTerrainQueriesNumCacheTiles.TabStop = false;
            this.gbTerrainQueriesNumCacheTiles.Text = "Terrain Queries Num Cache Tiles";
            // 
            // btnTerrainNumCacheTilesGet
            // 
            this.btnTerrainNumCacheTilesGet.Location = new System.Drawing.Point(146, 121);
            this.btnTerrainNumCacheTilesGet.Name = "btnTerrainNumCacheTilesGet";
            this.btnTerrainNumCacheTilesGet.Size = new System.Drawing.Size(33, 22);
            this.btnTerrainNumCacheTilesGet.TabIndex = 98;
            this.btnTerrainNumCacheTilesGet.Text = "Get";
            this.btnTerrainNumCacheTilesGet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTerrainNumCacheTilesGet.UseVisualStyleBackColor = true;
            this.btnTerrainNumCacheTilesGet.Visible = false;
            this.btnTerrainNumCacheTilesGet.Click += new System.EventHandler(this.btnTerrainNumCacheTilesGet_Click);
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Location = new System.Drawing.Point(10, 98);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(60, 13);
            this.label98.TabIndex = 97;
            this.label98.Text = "Layer Kind:";
            // 
            // cmbLayerTypes
            // 
            this.cmbLayerTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayerTypes.FormattingEnabled = true;
            this.cmbLayerTypes.Location = new System.Drawing.Point(78, 95);
            this.cmbLayerTypes.Margin = new System.Windows.Forms.Padding(2);
            this.cmbLayerTypes.Name = "cmbLayerTypes";
            this.cmbLayerTypes.Size = new System.Drawing.Size(141, 21);
            this.cmbLayerTypes.TabIndex = 96;
            this.cmbLayerTypes.SelectedIndexChanged += new System.EventHandler(this.cmbLayerTypes_SelectedIndexChanged);
            // 
            // lstTerrain
            // 
            this.lstTerrain.FormattingEnabled = true;
            this.lstTerrain.Location = new System.Drawing.Point(11, 27);
            this.lstTerrain.Name = "lstTerrain";
            this.lstTerrain.Size = new System.Drawing.Size(169, 56);
            this.lstTerrain.TabIndex = 95;
            this.lstTerrain.SelectedIndexChanged += new System.EventHandler(this.lstTerrain_SelectedValueChanged);
            // 
            // ntxNumTiles
            // 
            this.ntxNumTiles.Location = new System.Drawing.Point(78, 123);
            this.ntxNumTiles.Name = "ntxNumTiles";
            this.ntxNumTiles.Size = new System.Drawing.Size(44, 20);
            this.ntxNumTiles.TabIndex = 90;
            this.ntxNumTiles.Text = "0";
            // 
            // tcSpatialQueries
            // 
            this.tcSpatialQueries.Controls.Add(this.tabPageQueryParams);
            this.tcSpatialQueries.Controls.Add(this.tpGeneral);
            this.tcSpatialQueries.Controls.Add(this.tpHeights);
            this.tcSpatialQueries.Controls.Add(this.tpRayIntersection);
            this.tcSpatialQueries.Controls.Add(this.tpSightLines);
            this.tcSpatialQueries.Controls.Add(this.tpRasterAndTraversability);
            this.tcSpatialQueries.Location = new System.Drawing.Point(0, 0);
            this.tcSpatialQueries.Name = "tcSpatialQueries";
            this.tcSpatialQueries.SelectedIndex = 0;
            this.tcSpatialQueries.Size = new System.Drawing.Size(943, 626);
            this.tcSpatialQueries.TabIndex = 95;
            this.tcSpatialQueries.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcSpatialQueries_Selected);
            // 
            // tabPageQueryParams
            // 
            this.tabPageQueryParams.Controls.Add(this.groupBox23);
            this.tabPageQueryParams.Controls.Add(this.ctrlQueryParams1);
            this.tabPageQueryParams.Location = new System.Drawing.Point(4, 22);
            this.tabPageQueryParams.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageQueryParams.Name = "tabPageQueryParams";
            this.tabPageQueryParams.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageQueryParams.Size = new System.Drawing.Size(935, 600);
            this.tabPageQueryParams.TabIndex = 6;
            this.tabPageQueryParams.Text = "Params";
            this.tabPageQueryParams.UseVisualStyleBackColor = true;
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.btnShowSelectedDTMLayerData);
            this.groupBox23.Controls.Add(this.lstQuerySecondaryDtmLayers);
            this.groupBox23.Location = new System.Drawing.Point(439, 6);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(273, 174);
            this.groupBox23.TabIndex = 1;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Query Secondary Dtm Layers";
            // 
            // btnShowSelectedDTMLayerData
            // 
            this.btnShowSelectedDTMLayerData.Location = new System.Drawing.Point(5, 144);
            this.btnShowSelectedDTMLayerData.Name = "btnShowSelectedDTMLayerData";
            this.btnShowSelectedDTMLayerData.Size = new System.Drawing.Size(185, 23);
            this.btnShowSelectedDTMLayerData.TabIndex = 1;
            this.btnShowSelectedDTMLayerData.Text = "Show Selected DTM Layer Data";
            this.btnShowSelectedDTMLayerData.UseVisualStyleBackColor = true;
            this.btnShowSelectedDTMLayerData.Click += new System.EventHandler(this.btnShowSelectedDTMLayerData_Click);
            // 
            // lstQuerySecondaryDtmLayers
            // 
            this.lstQuerySecondaryDtmLayers.FormattingEnabled = true;
            this.lstQuerySecondaryDtmLayers.Location = new System.Drawing.Point(6, 19);
            this.lstQuerySecondaryDtmLayers.Name = "lstQuerySecondaryDtmLayers";
            this.lstQuerySecondaryDtmLayers.Size = new System.Drawing.Size(261, 121);
            this.lstQuerySecondaryDtmLayers.TabIndex = 0;
            this.lstQuerySecondaryDtmLayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstQuerySecondaryDtmLayers_MouseDoubleClick);
            // 
            // ctrlQueryParams1
            // 
            this.ctrlQueryParams1.Location = new System.Drawing.Point(1, 0);
            this.ctrlQueryParams1.Margin = new System.Windows.Forms.Padding(6);
            this.ctrlQueryParams1.Name = "ctrlQueryParams1";
            this.ctrlQueryParams1.Size = new System.Drawing.Size(414, 524);
            this.ctrlQueryParams1.TabIndex = 0;
            // 
            // tpGeneral
            // 
            this.tpGeneral.AutoScroll = true;
            this.tpGeneral.Controls.Add(this.LocationFromTwoDistancesAndAzimuth);
            this.tpGeneral.Controls.Add(this.groupBox2);
            this.tpGeneral.Controls.Add(this.groupBox1);
            this.tpGeneral.Controls.Add(this.gbTerrainAngles);
            this.tpGeneral.Controls.Add(this.gbTerrainQueriesNumCacheTiles);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(935, 600);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // LocationFromTwoDistancesAndAzimuth
            // 
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.cbLocationFromDistancesAndAzimuthFoundHeight);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ntxDifferenceHeightsFromResultAndTerrain);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label103);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ntxDistanceFromSecondLocationAndResult);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label102);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ntxDistanceFromFirstLocationAndResult);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label101);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.cbLocationFromTwoDistancesAndAzimuthAsync);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.btnGetLocation);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ctrl3DLocationResult);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label97);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ntxTargetHeightAboveGround);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label94);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ntxSecondDistance);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label95);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ctrl3DLocationScndOrgnPt);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ctrlLocationScndOrgnPt);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label96);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ntxFirstAzimut);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label93);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ntxFirstDistance);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label92);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ctrl3DLocationFrstOrgnPt);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.ctrlLocationFrstOrgnPt);
            this.LocationFromTwoDistancesAndAzimuth.Controls.Add(this.label91);
            this.LocationFromTwoDistancesAndAzimuth.Location = new System.Drawing.Point(3, 7);
            this.LocationFromTwoDistancesAndAzimuth.Name = "LocationFromTwoDistancesAndAzimuth";
            this.LocationFromTwoDistancesAndAzimuth.Size = new System.Drawing.Size(399, 295);
            this.LocationFromTwoDistancesAndAzimuth.TabIndex = 97;
            this.LocationFromTwoDistancesAndAzimuth.TabStop = false;
            this.LocationFromTwoDistancesAndAzimuth.Text = "Location From Two Distances And Azimuth";
            // 
            // cbLocationFromDistancesAndAzimuthFoundHeight
            // 
            this.cbLocationFromDistancesAndAzimuthFoundHeight.AutoSize = true;
            this.cbLocationFromDistancesAndAzimuthFoundHeight.Enabled = false;
            this.cbLocationFromDistancesAndAzimuthFoundHeight.Location = new System.Drawing.Point(288, 264);
            this.cbLocationFromDistancesAndAzimuthFoundHeight.Margin = new System.Windows.Forms.Padding(2);
            this.cbLocationFromDistancesAndAzimuthFoundHeight.Name = "cbLocationFromDistancesAndAzimuthFoundHeight";
            this.cbLocationFromDistancesAndAzimuthFoundHeight.Size = new System.Drawing.Size(90, 17);
            this.cbLocationFromDistancesAndAzimuthFoundHeight.TabIndex = 100;
            this.cbLocationFromDistancesAndAzimuthFoundHeight.Text = "Height Found";
            this.cbLocationFromDistancesAndAzimuthFoundHeight.UseVisualStyleBackColor = true;
            // 
            // ntxDifferenceHeightsFromResultAndTerrain
            // 
            this.ntxDifferenceHeightsFromResultAndTerrain.Location = new System.Drawing.Point(221, 262);
            this.ntxDifferenceHeightsFromResultAndTerrain.Name = "ntxDifferenceHeightsFromResultAndTerrain";
            this.ntxDifferenceHeightsFromResultAndTerrain.ReadOnly = true;
            this.ntxDifferenceHeightsFromResultAndTerrain.Size = new System.Drawing.Size(57, 20);
            this.ntxDifferenceHeightsFromResultAndTerrain.TabIndex = 99;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(4, 265);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(204, 13);
            this.label103.TabIndex = 98;
            this.label103.Text = "Difference Height From Result To Terrain:";
            // 
            // ntxDistanceFromSecondLocationAndResult
            // 
            this.ntxDistanceFromSecondLocationAndResult.Location = new System.Drawing.Point(220, 236);
            this.ntxDistanceFromSecondLocationAndResult.Name = "ntxDistanceFromSecondLocationAndResult";
            this.ntxDistanceFromSecondLocationAndResult.ReadOnly = true;
            this.ntxDistanceFromSecondLocationAndResult.Size = new System.Drawing.Size(57, 20);
            this.ntxDistanceFromSecondLocationAndResult.TabIndex = 97;
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(3, 239);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(211, 13);
            this.label102.TabIndex = 96;
            this.label102.Text = "Distance From Second Location To Result:";
            // 
            // ntxDistanceFromFirstLocationAndResult
            // 
            this.ntxDistanceFromFirstLocationAndResult.Location = new System.Drawing.Point(220, 211);
            this.ntxDistanceFromFirstLocationAndResult.Name = "ntxDistanceFromFirstLocationAndResult";
            this.ntxDistanceFromFirstLocationAndResult.ReadOnly = true;
            this.ntxDistanceFromFirstLocationAndResult.Size = new System.Drawing.Size(57, 20);
            this.ntxDistanceFromFirstLocationAndResult.TabIndex = 95;
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(3, 214);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(193, 13);
            this.label101.TabIndex = 94;
            this.label101.Text = "Distance From First Location To Result:";
            // 
            // cbLocationFromTwoDistancesAndAzimuthAsync
            // 
            this.cbLocationFromTwoDistancesAndAzimuthAsync.AutoSize = true;
            this.cbLocationFromTwoDistancesAndAzimuthAsync.Location = new System.Drawing.Point(288, 129);
            this.cbLocationFromTwoDistancesAndAzimuthAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbLocationFromTwoDistancesAndAzimuthAsync.Name = "cbLocationFromTwoDistancesAndAzimuthAsync";
            this.cbLocationFromTwoDistancesAndAzimuthAsync.Size = new System.Drawing.Size(55, 17);
            this.cbLocationFromTwoDistancesAndAzimuthAsync.TabIndex = 93;
            this.cbLocationFromTwoDistancesAndAzimuthAsync.Text = "Async";
            this.cbLocationFromTwoDistancesAndAzimuthAsync.UseVisualStyleBackColor = true;
            // 
            // btnGetLocation
            // 
            this.btnGetLocation.Location = new System.Drawing.Point(288, 152);
            this.btnGetLocation.Name = "btnGetLocation";
            this.btnGetLocation.Size = new System.Drawing.Size(105, 23);
            this.btnGetLocation.TabIndex = 92;
            this.btnGetLocation.Text = "Get Location";
            this.btnGetLocation.UseVisualStyleBackColor = true;
            this.btnGetLocation.Click += new System.EventHandler(this.btnGetLocation_Click);
            // 
            // ctrl3DLocationResult
            // 
            this.ctrl3DLocationResult.IsReadOnly = true;
            this.ctrl3DLocationResult.Location = new System.Drawing.Point(82, 180);
            this.ctrl3DLocationResult.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DLocationResult.Name = "ctrl3DLocationResult";
            this.ctrl3DLocationResult.Size = new System.Drawing.Size(226, 26);
            this.ctrl3DLocationResult.TabIndex = 91;
            this.ctrl3DLocationResult.X = 0D;
            this.ctrl3DLocationResult.Y = 0D;
            this.ctrl3DLocationResult.Z = 0D;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Location = new System.Drawing.Point(4, 185);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(84, 13);
            this.label97.TabIndex = 90;
            this.label97.Text = "Location Result:";
            // 
            // ntxTargetHeightAboveGround
            // 
            this.ntxTargetHeightAboveGround.Location = new System.Drawing.Point(174, 153);
            this.ntxTargetHeightAboveGround.Name = "ntxTargetHeightAboveGround";
            this.ntxTargetHeightAboveGround.Size = new System.Drawing.Size(56, 20);
            this.ntxTargetHeightAboveGround.TabIndex = 89;
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Location = new System.Drawing.Point(3, 157);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(147, 13);
            this.label94.TabIndex = 88;
            this.label94.Text = "Target Height Above Ground:";
            // 
            // ntxSecondDistance
            // 
            this.ntxSecondDistance.Location = new System.Drawing.Point(99, 130);
            this.ntxSecondDistance.Name = "ntxSecondDistance";
            this.ntxSecondDistance.Size = new System.Drawing.Size(57, 20);
            this.ntxSecondDistance.TabIndex = 87;
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Location = new System.Drawing.Point(4, 132);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(92, 13);
            this.label95.TabIndex = 86;
            this.label95.Text = "Second Distance:";
            // 
            // ctrl3DLocationScndOrgnPt
            // 
            this.ctrl3DLocationScndOrgnPt.IsReadOnly = false;
            this.ctrl3DLocationScndOrgnPt.Location = new System.Drawing.Point(81, 101);
            this.ctrl3DLocationScndOrgnPt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DLocationScndOrgnPt.Name = "ctrl3DLocationScndOrgnPt";
            this.ctrl3DLocationScndOrgnPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DLocationScndOrgnPt.TabIndex = 85;
            this.ctrl3DLocationScndOrgnPt.X = 0D;
            this.ctrl3DLocationScndOrgnPt.Y = 0D;
            this.ctrl3DLocationScndOrgnPt.Z = 0D;
            // 
            // ctrlLocationScndOrgnPt
            // 
            this.ctrlLocationScndOrgnPt._DgvControlName = null;
            this.ctrlLocationScndOrgnPt._IsRelativeToDTM = false;
            this.ctrlLocationScndOrgnPt._PointInOverlayManagerCoordSys = true;
            this.ctrlLocationScndOrgnPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrlLocationScndOrgnPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlLocationScndOrgnPt._SampleOnePoint = true;
            this.ctrlLocationScndOrgnPt._UserControlName = "ctrl3DLocationScndOrgnPt";
            this.ctrlLocationScndOrgnPt.IsAsync = false;
            this.ctrlLocationScndOrgnPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlLocationScndOrgnPt.Location = new System.Drawing.Point(318, 101);
            this.ctrlLocationScndOrgnPt.Name = "ctrlLocationScndOrgnPt";
            this.ctrlLocationScndOrgnPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlLocationScndOrgnPt.Size = new System.Drawing.Size(33, 23);
            this.ctrlLocationScndOrgnPt.TabIndex = 84;
            this.ctrlLocationScndOrgnPt.Text = "...";
            this.ctrlLocationScndOrgnPt.UseVisualStyleBackColor = true;
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Location = new System.Drawing.Point(4, 106);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(77, 13);
            this.label96.TabIndex = 83;
            this.label96.Text = "Second Origin:";
            // 
            // ntxFirstAzimut
            // 
            this.ntxFirstAzimut.Location = new System.Drawing.Point(99, 78);
            this.ntxFirstAzimut.Name = "ntxFirstAzimut";
            this.ntxFirstAzimut.Size = new System.Drawing.Size(57, 20);
            this.ntxFirstAzimut.TabIndex = 82;
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Location = new System.Drawing.Point(4, 80);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(69, 13);
            this.label93.TabIndex = 81;
            this.label93.Text = "First Azimuth:";
            // 
            // ntxFirstDistance
            // 
            this.ntxFirstDistance.Location = new System.Drawing.Point(99, 54);
            this.ntxFirstDistance.Name = "ntxFirstDistance";
            this.ntxFirstDistance.Size = new System.Drawing.Size(57, 20);
            this.ntxFirstDistance.TabIndex = 80;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Location = new System.Drawing.Point(3, 56);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(74, 13);
            this.label92.TabIndex = 79;
            this.label92.Text = "First Distance:";
            // 
            // ctrl3DLocationFrstOrgnPt
            // 
            this.ctrl3DLocationFrstOrgnPt.IsReadOnly = false;
            this.ctrl3DLocationFrstOrgnPt.Location = new System.Drawing.Point(80, 23);
            this.ctrl3DLocationFrstOrgnPt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DLocationFrstOrgnPt.Name = "ctrl3DLocationFrstOrgnPt";
            this.ctrl3DLocationFrstOrgnPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DLocationFrstOrgnPt.TabIndex = 78;
            this.ctrl3DLocationFrstOrgnPt.X = 0D;
            this.ctrl3DLocationFrstOrgnPt.Y = 0D;
            this.ctrl3DLocationFrstOrgnPt.Z = 0D;
            // 
            // ctrlLocationFrstOrgnPt
            // 
            this.ctrlLocationFrstOrgnPt._DgvControlName = null;
            this.ctrlLocationFrstOrgnPt._IsRelativeToDTM = false;
            this.ctrlLocationFrstOrgnPt._PointInOverlayManagerCoordSys = true;
            this.ctrlLocationFrstOrgnPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrlLocationFrstOrgnPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlLocationFrstOrgnPt._SampleOnePoint = true;
            this.ctrlLocationFrstOrgnPt._UserControlName = "ctrl3DLocationFrstOrgnPt";
            this.ctrlLocationFrstOrgnPt.IsAsync = false;
            this.ctrlLocationFrstOrgnPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlLocationFrstOrgnPt.Location = new System.Drawing.Point(318, 24);
            this.ctrlLocationFrstOrgnPt.Name = "ctrlLocationFrstOrgnPt";
            this.ctrlLocationFrstOrgnPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlLocationFrstOrgnPt.Size = new System.Drawing.Size(33, 23);
            this.ctrlLocationFrstOrgnPt.TabIndex = 77;
            this.ctrlLocationFrstOrgnPt.Text = "...";
            this.ctrlLocationFrstOrgnPt.UseVisualStyleBackColor = true;
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Location = new System.Drawing.Point(3, 29);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(59, 13);
            this.label91.TabIndex = 76;
            this.label91.Text = "First Origin:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddPoint);
            this.groupBox2.Controls.Add(this.label90);
            this.groupBox2.Controls.Add(this.ntxSmoothDistance);
            this.groupBox2.Controls.Add(this.btnGetSmoothedPath);
            this.groupBox2.Controls.Add(this.btnDrawPath);
            this.groupBox2.Location = new System.Drawing.Point(642, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 152);
            this.groupBox2.TabIndex = 96;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Track Smoother";
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Location = new System.Drawing.Point(16, 49);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(75, 23);
            this.btnAddPoint.TabIndex = 4;
            this.btnAddPoint.Text = "Add Points";
            this.btnAddPoint.UseVisualStyleBackColor = true;
            this.btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.Location = new System.Drawing.Point(16, 88);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(91, 13);
            this.label90.TabIndex = 3;
            this.label90.Text = "Smooth Distance:";
            // 
            // ntxSmoothDistance
            // 
            this.ntxSmoothDistance.Location = new System.Drawing.Point(111, 85);
            this.ntxSmoothDistance.Name = "ntxSmoothDistance";
            this.ntxSmoothDistance.Size = new System.Drawing.Size(37, 20);
            this.ntxSmoothDistance.TabIndex = 2;
            this.ntxSmoothDistance.Text = "1";
            // 
            // btnGetSmoothedPath
            // 
            this.btnGetSmoothedPath.Location = new System.Drawing.Point(18, 110);
            this.btnGetSmoothedPath.Name = "btnGetSmoothedPath";
            this.btnGetSmoothedPath.Size = new System.Drawing.Size(106, 24);
            this.btnGetSmoothedPath.TabIndex = 1;
            this.btnGetSmoothedPath.Text = "Get Smoothed Path";
            this.btnGetSmoothedPath.UseVisualStyleBackColor = true;
            this.btnGetSmoothedPath.Click += new System.EventHandler(this.btnGetSmoothedPath_Click);
            // 
            // btnDrawPath
            // 
            this.btnDrawPath.Location = new System.Drawing.Point(16, 20);
            this.btnDrawPath.Name = "btnDrawPath";
            this.btnDrawPath.Size = new System.Drawing.Size(75, 23);
            this.btnDrawPath.TabIndex = 0;
            this.btnDrawPath.Text = "Draw Path";
            this.btnDrawPath.UseVisualStyleBackColor = true;
            this.btnDrawPath.Click += new System.EventHandler(this.btnDrawPath_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label69);
            this.groupBox1.Controls.Add(this.chxThrowException);
            this.groupBox1.Controls.Add(this.ntxNumTestingPt);
            this.groupBox1.Controls.Add(this.label89);
            this.groupBox1.Controls.Add(this.label86);
            this.groupBox1.Controls.Add(this.label88);
            this.groupBox1.Controls.Add(this.ntxDurationTime);
            this.groupBox1.Controls.Add(this.cbxIsLogThreadTest);
            this.groupBox1.Controls.Add(this.ctrl3DVectorFirstPoint);
            this.groupBox1.Controls.Add(this.ctrlSamplePointFirstPoint);
            this.groupBox1.Controls.Add(this.ntxNumOfThread);
            this.groupBox1.Controls.Add(this.label87);
            this.groupBox1.Controls.Add(this.btnStartMultiThreadTest);
            this.groupBox1.Controls.Add(this.label85);
            this.groupBox1.Location = new System.Drawing.Point(3, 307);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 158);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Multi Thread Testing";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.ForeColor = System.Drawing.Color.Black;
            this.label69.Location = new System.Drawing.Point(6, 22);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(321, 13);
            this.label69.TabIndex = 108;
            this.label69.Text = "Compatibility with currently displayed in the viewport is unsupported";
            // 
            // chxThrowException
            // 
            this.chxThrowException.AutoSize = true;
            this.chxThrowException.Checked = true;
            this.chxThrowException.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxThrowException.Location = new System.Drawing.Point(287, 104);
            this.chxThrowException.Name = "chxThrowException";
            this.chxThrowException.Size = new System.Drawing.Size(106, 17);
            this.chxThrowException.TabIndex = 107;
            this.chxThrowException.Text = "Throw Exception";
            this.chxThrowException.UseVisualStyleBackColor = true;
            // 
            // ntxNumTestingPt
            // 
            this.ntxNumTestingPt.Location = new System.Drawing.Point(137, 75);
            this.ntxNumTestingPt.Name = "ntxNumTestingPt";
            this.ntxNumTestingPt.Size = new System.Drawing.Size(55, 20);
            this.ntxNumTestingPt.TabIndex = 106;
            this.ntxNumTestingPt.Text = "100";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(6, 76);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(129, 13);
            this.label89.TabIndex = 105;
            this.label89.Text = "Number of Testing Points:";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(194, 128);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(23, 13);
            this.label86.TabIndex = 104;
            this.label86.Text = "min";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.Location = new System.Drawing.Point(6, 128);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(100, 13);
            this.label88.TabIndex = 103;
            this.label88.Text = "Test Duration Time:";
            // 
            // ntxDurationTime
            // 
            this.ntxDurationTime.Location = new System.Drawing.Point(137, 127);
            this.ntxDurationTime.Name = "ntxDurationTime";
            this.ntxDurationTime.Size = new System.Drawing.Size(55, 20);
            this.ntxDurationTime.TabIndex = 102;
            this.ntxDurationTime.Text = "1";
            this.ntxDurationTime.TextChanged += new System.EventHandler(this.ntxDurationTime_TextChanged);
            // 
            // cbxIsLogThreadTest
            // 
            this.cbxIsLogThreadTest.AutoSize = true;
            this.cbxIsLogThreadTest.Location = new System.Drawing.Point(287, 80);
            this.cbxIsLogThreadTest.Name = "cbxIsLogThreadTest";
            this.cbxIsLogThreadTest.Size = new System.Drawing.Size(64, 17);
            this.cbxIsLogThreadTest.TabIndex = 101;
            this.cbxIsLogThreadTest.Text = "Log test";
            this.cbxIsLogThreadTest.UseVisualStyleBackColor = true;
            // 
            // ctrl3DVectorFirstPoint
            // 
            this.ctrl3DVectorFirstPoint.IsReadOnly = false;
            this.ctrl3DVectorFirstPoint.Location = new System.Drawing.Point(78, 40);
            this.ctrl3DVectorFirstPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorFirstPoint.Name = "ctrl3DVectorFirstPoint";
            this.ctrl3DVectorFirstPoint.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorFirstPoint.TabIndex = 99;
            this.ctrl3DVectorFirstPoint.X = 0D;
            this.ctrl3DVectorFirstPoint.Y = 0D;
            this.ctrl3DVectorFirstPoint.Z = 0D;
            // 
            // ctrlSamplePointFirstPoint
            // 
            this.ctrlSamplePointFirstPoint._DgvControlName = null;
            this.ctrlSamplePointFirstPoint._IsRelativeToDTM = false;
            this.ctrlSamplePointFirstPoint._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointFirstPoint._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointFirstPoint._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointFirstPoint._SampleOnePoint = true;
            this.ctrlSamplePointFirstPoint._UserControlName = "ctrl3DVectorFirstPoint";
            this.ctrlSamplePointFirstPoint.IsAsync = false;
            this.ctrlSamplePointFirstPoint.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointFirstPoint.Location = new System.Drawing.Point(323, 40);
            this.ctrlSamplePointFirstPoint.Name = "ctrlSamplePointFirstPoint";
            this.ctrlSamplePointFirstPoint.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointFirstPoint.Size = new System.Drawing.Size(39, 23);
            this.ctrlSamplePointFirstPoint.TabIndex = 97;
            this.ctrlSamplePointFirstPoint.Text = "...";
            this.ctrlSamplePointFirstPoint.UseVisualStyleBackColor = true;
            // 
            // ntxNumOfThread
            // 
            this.ntxNumOfThread.Location = new System.Drawing.Point(137, 101);
            this.ntxNumOfThread.Name = "ntxNumOfThread";
            this.ntxNumOfThread.Size = new System.Drawing.Size(55, 20);
            this.ntxNumOfThread.TabIndex = 96;
            this.ntxNumOfThread.Text = "3";
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.Location = new System.Drawing.Point(6, 102);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(101, 13);
            this.label87.TabIndex = 5;
            this.label87.Text = "Number of Threads:";
            // 
            // btnStartMultiThreadTest
            // 
            this.btnStartMultiThreadTest.BackColor = System.Drawing.Color.Green;
            this.btnStartMultiThreadTest.Location = new System.Drawing.Point(287, 128);
            this.btnStartMultiThreadTest.Name = "btnStartMultiThreadTest";
            this.btnStartMultiThreadTest.Size = new System.Drawing.Size(95, 23);
            this.btnStartMultiThreadTest.TabIndex = 4;
            this.btnStartMultiThreadTest.Text = "Start";
            this.btnStartMultiThreadTest.UseVisualStyleBackColor = false;
            this.btnStartMultiThreadTest.Click += new System.EventHandler(this.btnStartMultiThreadTest_Click);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Location = new System.Drawing.Point(6, 47);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(56, 13);
            this.label85.TabIndex = 2;
            this.label85.Text = "First Point:";
            // 
            // gbTerrainAngles
            // 
            this.gbTerrainAngles.Controls.Add(this.cbTerrainAnglesAsync);
            this.gbTerrainAngles.Controls.Add(this.btnGetTerrainAngles);
            this.gbTerrainAngles.Controls.Add(this.txtPitch);
            this.gbTerrainAngles.Controls.Add(this.txtRoll);
            this.gbTerrainAngles.Controls.Add(this.label47);
            this.gbTerrainAngles.Controls.Add(this.label46);
            this.gbTerrainAngles.Controls.Add(this.ntxTerrainAnglesAzimuth);
            this.gbTerrainAngles.Controls.Add(this.label45);
            this.gbTerrainAngles.Controls.Add(this.ctrl3DVectorTerrainAnglesPt);
            this.gbTerrainAngles.Controls.Add(this.ctrlSamplePointTerrainAngles);
            this.gbTerrainAngles.Controls.Add(this.btnTerrainAnglesDrawLine);
            this.gbTerrainAngles.Location = new System.Drawing.Point(409, 164);
            this.gbTerrainAngles.Name = "gbTerrainAngles";
            this.gbTerrainAngles.Size = new System.Drawing.Size(396, 138);
            this.gbTerrainAngles.TabIndex = 95;
            this.gbTerrainAngles.TabStop = false;
            this.gbTerrainAngles.Text = "Terrain Angles";
            // 
            // cbTerrainAnglesAsync
            // 
            this.cbTerrainAnglesAsync.AutoSize = true;
            this.cbTerrainAnglesAsync.Location = new System.Drawing.Point(334, 83);
            this.cbTerrainAnglesAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbTerrainAnglesAsync.Name = "cbTerrainAnglesAsync";
            this.cbTerrainAnglesAsync.Size = new System.Drawing.Size(55, 17);
            this.cbTerrainAnglesAsync.TabIndex = 93;
            this.cbTerrainAnglesAsync.Text = "Async";
            this.cbTerrainAnglesAsync.UseVisualStyleBackColor = true;
            // 
            // btnGetTerrainAngles
            // 
            this.btnGetTerrainAngles.Location = new System.Drawing.Point(290, 105);
            this.btnGetTerrainAngles.Name = "btnGetTerrainAngles";
            this.btnGetTerrainAngles.Size = new System.Drawing.Size(95, 23);
            this.btnGetTerrainAngles.TabIndex = 88;
            this.btnGetTerrainAngles.Text = "Get";
            this.btnGetTerrainAngles.UseVisualStyleBackColor = true;
            this.btnGetTerrainAngles.Click += new System.EventHandler(this.btnGetTerrainAngles_Click);
            // 
            // txtPitch
            // 
            this.txtPitch.Enabled = false;
            this.txtPitch.Location = new System.Drawing.Point(59, 81);
            this.txtPitch.Name = "txtPitch";
            this.txtPitch.Size = new System.Drawing.Size(100, 20);
            this.txtPitch.TabIndex = 84;
            // 
            // txtRoll
            // 
            this.txtRoll.Enabled = false;
            this.txtRoll.Location = new System.Drawing.Point(59, 107);
            this.txtRoll.Name = "txtRoll";
            this.txtRoll.Size = new System.Drawing.Size(100, 20);
            this.txtRoll.TabIndex = 85;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(6, 110);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(28, 13);
            this.label47.TabIndex = 85;
            this.label47.Text = "Roll:";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(6, 85);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(34, 13);
            this.label46.TabIndex = 84;
            this.label46.Text = "Pitch:";
            // 
            // ntxTerrainAnglesAzimuth
            // 
            this.ntxTerrainAnglesAzimuth.Location = new System.Drawing.Point(59, 55);
            this.ntxTerrainAnglesAzimuth.Name = "ntxTerrainAnglesAzimuth";
            this.ntxTerrainAnglesAzimuth.Size = new System.Drawing.Size(100, 20);
            this.ntxTerrainAnglesAzimuth.TabIndex = 83;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(6, 58);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(47, 13);
            this.label45.TabIndex = 82;
            this.label45.Text = "Azimuth:";
            // 
            // ctrl3DVectorTerrainAnglesPt
            // 
            this.ctrl3DVectorTerrainAnglesPt.IsReadOnly = false;
            this.ctrl3DVectorTerrainAnglesPt.Location = new System.Drawing.Point(9, 20);
            this.ctrl3DVectorTerrainAnglesPt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorTerrainAnglesPt.Name = "ctrl3DVectorTerrainAnglesPt";
            this.ctrl3DVectorTerrainAnglesPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorTerrainAnglesPt.TabIndex = 81;
            this.ctrl3DVectorTerrainAnglesPt.X = 0D;
            this.ctrl3DVectorTerrainAnglesPt.Y = 0D;
            this.ctrl3DVectorTerrainAnglesPt.Z = 0D;
            // 
            // ctrlSamplePointTerrainAngles
            // 
            this.ctrlSamplePointTerrainAngles._DgvControlName = null;
            this.ctrlSamplePointTerrainAngles._IsRelativeToDTM = false;
            this.ctrlSamplePointTerrainAngles._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointTerrainAngles._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointTerrainAngles._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointTerrainAngles._SampleOnePoint = true;
            this.ctrlSamplePointTerrainAngles._UserControlName = "ctrl3DVectorTerrainAnglesPt";
            this.ctrlSamplePointTerrainAngles.IsAsync = false;
            this.ctrlSamplePointTerrainAngles.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointTerrainAngles.Location = new System.Drawing.Point(247, 22);
            this.ctrlSamplePointTerrainAngles.Name = "ctrlSamplePointTerrainAngles";
            this.ctrlSamplePointTerrainAngles.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointTerrainAngles.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePointTerrainAngles.TabIndex = 80;
            this.ctrlSamplePointTerrainAngles.Text = "...";
            this.ctrlSamplePointTerrainAngles.UseVisualStyleBackColor = true;
            // 
            // btnTerrainAnglesDrawLine
            // 
            this.btnTerrainAnglesDrawLine.Location = new System.Drawing.Point(290, 22);
            this.btnTerrainAnglesDrawLine.Name = "btnTerrainAnglesDrawLine";
            this.btnTerrainAnglesDrawLine.Size = new System.Drawing.Size(95, 23);
            this.btnTerrainAnglesDrawLine.TabIndex = 79;
            this.btnTerrainAnglesDrawLine.Text = "Draw Line";
            this.btnTerrainAnglesDrawLine.UseVisualStyleBackColor = true;
            this.btnTerrainAnglesDrawLine.Click += new System.EventHandler(this.btnTerrainAnglesDrawLine_Click);
            // 
            // tpHeights
            // 
            this.tpHeights.Controls.Add(this.groupBox15);
            this.tpHeights.Controls.Add(this.groupBox14);
            this.tpHeights.Controls.Add(this.groupBox13);
            this.tpHeights.Controls.Add(this.groupBox12);
            this.tpHeights.Location = new System.Drawing.Point(4, 22);
            this.tpHeights.Margin = new System.Windows.Forms.Padding(2);
            this.tpHeights.Name = "tpHeights";
            this.tpHeights.Size = new System.Drawing.Size(935, 600);
            this.tpHeights.TabIndex = 3;
            this.tpHeights.Text = "Heights";
            this.tpHeights.UseVisualStyleBackColor = true;
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.ntxNumHorizontalPoints);
            this.groupBox15.Controls.Add(this.label23);
            this.groupBox15.Controls.Add(this.ntxNumVerticalPoints);
            this.groupBox15.Controls.Add(this.label24);
            this.groupBox15.Controls.Add(this.ntxHorizontalResolution);
            this.groupBox15.Controls.Add(this.cbTerrainMatrixAsync);
            this.groupBox15.Controls.Add(this.ctrl3DLowerLeftPoint);
            this.groupBox15.Controls.Add(this.ctrlSamplePoint3);
            this.groupBox15.Controls.Add(this.btnTerrainHeightMatrix);
            this.groupBox15.Controls.Add(this.label9);
            this.groupBox15.Controls.Add(this.label10);
            this.groupBox15.Controls.Add(this.ntxVerticalResolution);
            this.groupBox15.Controls.Add(this.label22);
            this.groupBox15.Location = new System.Drawing.Point(3, 232);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(395, 155);
            this.groupBox15.TabIndex = 81;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Terrain Height Matrix";
            // 
            // ntxNumHorizontalPoints
            // 
            this.ntxNumHorizontalPoints.Location = new System.Drawing.Point(315, 55);
            this.ntxNumHorizontalPoints.Name = "ntxNumHorizontalPoints";
            this.ntxNumHorizontalPoints.Size = new System.Drawing.Size(68, 20);
            this.ntxNumHorizontalPoints.TabIndex = 80;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(200, 58);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(114, 13);
            this.label23.TabIndex = 81;
            this.label23.Text = "Num Horizontal Points:";
            // 
            // ntxNumVerticalPoints
            // 
            this.ntxNumVerticalPoints.Location = new System.Drawing.Point(315, 80);
            this.ntxNumVerticalPoints.Name = "ntxNumVerticalPoints";
            this.ntxNumVerticalPoints.Size = new System.Drawing.Size(68, 20);
            this.ntxNumVerticalPoints.TabIndex = 81;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(200, 83);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(102, 13);
            this.label24.TabIndex = 79;
            this.label24.Text = "Num Vertical Points:";
            // 
            // ntxHorizontalResolution
            // 
            this.ntxHorizontalResolution.Location = new System.Drawing.Point(118, 55);
            this.ntxHorizontalResolution.Name = "ntxHorizontalResolution";
            this.ntxHorizontalResolution.Size = new System.Drawing.Size(68, 20);
            this.ntxHorizontalResolution.TabIndex = 78;
            // 
            // cbTerrainMatrixAsync
            // 
            this.cbTerrainMatrixAsync.AutoSize = true;
            this.cbTerrainMatrixAsync.Location = new System.Drawing.Point(331, 104);
            this.cbTerrainMatrixAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbTerrainMatrixAsync.Name = "cbTerrainMatrixAsync";
            this.cbTerrainMatrixAsync.Size = new System.Drawing.Size(55, 17);
            this.cbTerrainMatrixAsync.TabIndex = 82;
            this.cbTerrainMatrixAsync.Text = "Async";
            this.cbTerrainMatrixAsync.UseVisualStyleBackColor = true;
            // 
            // ctrl3DLowerLeftPoint
            // 
            this.ctrl3DLowerLeftPoint.IsReadOnly = false;
            this.ctrl3DLowerLeftPoint.Location = new System.Drawing.Point(111, 21);
            this.ctrl3DLowerLeftPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DLowerLeftPoint.Name = "ctrl3DLowerLeftPoint";
            this.ctrl3DLowerLeftPoint.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DLowerLeftPoint.TabIndex = 75;
            this.ctrl3DLowerLeftPoint.X = 0D;
            this.ctrl3DLowerLeftPoint.Y = 0D;
            this.ctrl3DLowerLeftPoint.Z = 0D;
            // 
            // ctrlSamplePoint3
            // 
            this.ctrlSamplePoint3._DgvControlName = null;
            this.ctrlSamplePoint3._IsRelativeToDTM = false;
            this.ctrlSamplePoint3._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePoint3._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePoint3._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePoint3._SampleOnePoint = true;
            this.ctrlSamplePoint3._UserControlName = "ctrl3DLowerLeftPoint";
            this.ctrlSamplePoint3.IsAsync = false;
            this.ctrlSamplePoint3.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePoint3.Location = new System.Drawing.Point(347, 20);
            this.ctrlSamplePoint3.Name = "ctrlSamplePoint3";
            this.ctrlSamplePoint3.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePoint3.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePoint3.TabIndex = 74;
            this.ctrlSamplePoint3.Text = "...";
            this.ctrlSamplePoint3.UseVisualStyleBackColor = true;
            // 
            // btnTerrainHeightMatrix
            // 
            this.btnTerrainHeightMatrix.Location = new System.Drawing.Point(289, 127);
            this.btnTerrainHeightMatrix.Name = "btnTerrainHeightMatrix";
            this.btnTerrainHeightMatrix.Size = new System.Drawing.Size(95, 23);
            this.btnTerrainHeightMatrix.TabIndex = 83;
            this.btnTerrainHeightMatrix.Text = "Get Height Matrix";
            this.btnTerrainHeightMatrix.UseVisualStyleBackColor = true;
            this.btnTerrainHeightMatrix.Click += new System.EventHandler(this.btnTerrainHeightMatrix_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 71;
            this.label9.Text = "Lower Left Point:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 70;
            this.label10.Text = "Horizontal Resolution: ";
            // 
            // ntxVerticalResolution
            // 
            this.ntxVerticalResolution.Location = new System.Drawing.Point(118, 80);
            this.ntxVerticalResolution.Name = "ntxVerticalResolution";
            this.ntxVerticalResolution.Size = new System.Drawing.Size(68, 20);
            this.ntxVerticalResolution.TabIndex = 79;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(3, 83);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(98, 13);
            this.label22.TabIndex = 67;
            this.label22.Text = "Vertical Resolution:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.ctrlExtremeHeightPoints);
            this.groupBox14.Controls.Add(this.btnExtremeUpdatePolygon);
            this.groupBox14.Controls.Add(this.btnExtremeHeightPointsInPolygon);
            this.groupBox14.Controls.Add(this.cbExtremeHeightPointsInPolygonAsync);
            this.groupBox14.Controls.Add(this.chxPointsFound);
            this.groupBox14.Controls.Add(this.ctrl3DVectorLowestPt);
            this.groupBox14.Controls.Add(this.btnDrawPolygon);
            this.groupBox14.Controls.Add(this.ctrl3DVectorHighestPt);
            this.groupBox14.Controls.Add(this.label110);
            this.groupBox14.Controls.Add(this.label111);
            this.groupBox14.Location = new System.Drawing.Point(404, 12);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(469, 514);
            this.groupBox14.TabIndex = 80;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Extreme Height Points In Polygon";
            // 
            // ctrlExtremeHeightPoints
            // 
            this.ctrlExtremeHeightPoints.Location = new System.Drawing.Point(4, 19);
            this.ctrlExtremeHeightPoints.Name = "ctrlExtremeHeightPoints";
            this.ctrlExtremeHeightPoints.Size = new System.Drawing.Size(345, 196);
            this.ctrlExtremeHeightPoints.TabIndex = 142;
            // 
            // btnExtremeUpdatePolygon
            // 
            this.btnExtremeUpdatePolygon.Location = new System.Drawing.Point(368, 131);
            this.btnExtremeUpdatePolygon.Name = "btnExtremeUpdatePolygon";
            this.btnExtremeUpdatePolygon.Size = new System.Drawing.Size(96, 23);
            this.btnExtremeUpdatePolygon.TabIndex = 138;
            this.btnExtremeUpdatePolygon.Text = "Update Polygon";
            this.btnExtremeUpdatePolygon.UseVisualStyleBackColor = true;
            this.btnExtremeUpdatePolygon.Click += new System.EventHandler(this.btnExtremeUpdatePolygon_Click);
            // 
            // btnExtremeHeightPointsInPolygon
            // 
            this.btnExtremeHeightPointsInPolygon.Location = new System.Drawing.Point(256, 223);
            this.btnExtremeHeightPointsInPolygon.Name = "btnExtremeHeightPointsInPolygon";
            this.btnExtremeHeightPointsInPolygon.Size = new System.Drawing.Size(208, 23);
            this.btnExtremeHeightPointsInPolygon.TabIndex = 93;
            this.btnExtremeHeightPointsInPolygon.Text = "Get Extreme Height Points In Polygon";
            this.btnExtremeHeightPointsInPolygon.UseVisualStyleBackColor = true;
            this.btnExtremeHeightPointsInPolygon.Click += new System.EventHandler(this.btnExtremeHeightPointsInPolygon_Click);
            // 
            // cbExtremeHeightPointsInPolygonAsync
            // 
            this.cbExtremeHeightPointsInPolygonAsync.AutoSize = true;
            this.cbExtremeHeightPointsInPolygonAsync.Location = new System.Drawing.Point(409, 197);
            this.cbExtremeHeightPointsInPolygonAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbExtremeHeightPointsInPolygonAsync.Name = "cbExtremeHeightPointsInPolygonAsync";
            this.cbExtremeHeightPointsInPolygonAsync.Size = new System.Drawing.Size(55, 17);
            this.cbExtremeHeightPointsInPolygonAsync.TabIndex = 92;
            this.cbExtremeHeightPointsInPolygonAsync.Text = "Async";
            this.cbExtremeHeightPointsInPolygonAsync.UseVisualStyleBackColor = true;
            // 
            // chxPointsFound
            // 
            this.chxPointsFound.AutoSize = true;
            this.chxPointsFound.Enabled = false;
            this.chxPointsFound.Location = new System.Drawing.Point(6, 313);
            this.chxPointsFound.Margin = new System.Windows.Forms.Padding(2);
            this.chxPointsFound.Name = "chxPointsFound";
            this.chxPointsFound.Size = new System.Drawing.Size(88, 17);
            this.chxPointsFound.TabIndex = 91;
            this.chxPointsFound.Text = "Points Found";
            this.chxPointsFound.UseVisualStyleBackColor = true;
            // 
            // ctrl3DVectorLowestPt
            // 
            this.ctrl3DVectorLowestPt.IsReadOnly = true;
            this.ctrl3DVectorLowestPt.Location = new System.Drawing.Point(73, 280);
            this.ctrl3DVectorLowestPt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorLowestPt.Name = "ctrl3DVectorLowestPt";
            this.ctrl3DVectorLowestPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorLowestPt.TabIndex = 90;
            this.ctrl3DVectorLowestPt.X = 0D;
            this.ctrl3DVectorLowestPt.Y = 0D;
            this.ctrl3DVectorLowestPt.Z = 0D;
            // 
            // btnDrawPolygon
            // 
            this.btnDrawPolygon.Location = new System.Drawing.Point(368, 160);
            this.btnDrawPolygon.Name = "btnDrawPolygon";
            this.btnDrawPolygon.Size = new System.Drawing.Size(96, 23);
            this.btnDrawPolygon.TabIndex = 83;
            this.btnDrawPolygon.Text = "Draw Polygon";
            this.btnDrawPolygon.UseVisualStyleBackColor = true;
            this.btnDrawPolygon.Click += new System.EventHandler(this.btnDrawPolygon_Click);
            // 
            // ctrl3DVectorHighestPt
            // 
            this.ctrl3DVectorHighestPt.IsReadOnly = true;
            this.ctrl3DVectorHighestPt.Location = new System.Drawing.Point(73, 253);
            this.ctrl3DVectorHighestPt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorHighestPt.Name = "ctrl3DVectorHighestPt";
            this.ctrl3DVectorHighestPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorHighestPt.TabIndex = 84;
            this.ctrl3DVectorHighestPt.X = 0D;
            this.ctrl3DVectorHighestPt.Y = 0D;
            this.ctrl3DVectorHighestPt.Z = 0D;
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(3, 286);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(71, 13);
            this.label110.TabIndex = 81;
            this.label110.Text = "Lowest Point:";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(4, 258);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(73, 13);
            this.label111.TabIndex = 79;
            this.label111.Text = "Highest Point:";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnGetHeightAlongLine);
            this.groupBox13.Controls.Add(this.cbTerrainHeightAlongLineAsync);
            this.groupBox13.Controls.Add(this.ntxHeightDelta);
            this.groupBox13.Controls.Add(this.ntxMinSlope);
            this.groupBox13.Controls.Add(this.ntxMaxSlope);
            this.groupBox13.Controls.Add(this.label107);
            this.groupBox13.Controls.Add(this.label108);
            this.groupBox13.Controls.Add(this.label109);
            this.groupBox13.Controls.Add(this.btnDrawLine);
            this.groupBox13.Location = new System.Drawing.Point(3, 124);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(395, 102);
            this.groupBox13.TabIndex = 79;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Terrain Height Along Line";
            // 
            // btnGetHeightAlongLine
            // 
            this.btnGetHeightAlongLine.Location = new System.Drawing.Point(267, 69);
            this.btnGetHeightAlongLine.Name = "btnGetHeightAlongLine";
            this.btnGetHeightAlongLine.Size = new System.Drawing.Size(118, 23);
            this.btnGetHeightAlongLine.TabIndex = 86;
            this.btnGetHeightAlongLine.Text = "Get Height Along Line";
            this.btnGetHeightAlongLine.UseVisualStyleBackColor = true;
            this.btnGetHeightAlongLine.Click += new System.EventHandler(this.btnGetHeightAlongLine_Click);
            // 
            // cbTerrainHeightAlongLineAsync
            // 
            this.cbTerrainHeightAlongLineAsync.AutoSize = true;
            this.cbTerrainHeightAlongLineAsync.Location = new System.Drawing.Point(334, 15);
            this.cbTerrainHeightAlongLineAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbTerrainHeightAlongLineAsync.Name = "cbTerrainHeightAlongLineAsync";
            this.cbTerrainHeightAlongLineAsync.Size = new System.Drawing.Size(55, 17);
            this.cbTerrainHeightAlongLineAsync.TabIndex = 85;
            this.cbTerrainHeightAlongLineAsync.Text = "Async";
            this.cbTerrainHeightAlongLineAsync.UseVisualStyleBackColor = true;
            // 
            // ntxHeightDelta
            // 
            this.ntxHeightDelta.Location = new System.Drawing.Point(81, 71);
            this.ntxHeightDelta.Name = "ntxHeightDelta";
            this.ntxHeightDelta.ReadOnly = true;
            this.ntxHeightDelta.Size = new System.Drawing.Size(100, 20);
            this.ntxHeightDelta.TabIndex = 84;
            // 
            // ntxMinSlope
            // 
            this.ntxMinSlope.Location = new System.Drawing.Point(81, 45);
            this.ntxMinSlope.Name = "ntxMinSlope";
            this.ntxMinSlope.ReadOnly = true;
            this.ntxMinSlope.Size = new System.Drawing.Size(100, 20);
            this.ntxMinSlope.TabIndex = 83;
            // 
            // ntxMaxSlope
            // 
            this.ntxMaxSlope.Location = new System.Drawing.Point(81, 19);
            this.ntxMaxSlope.Name = "ntxMaxSlope";
            this.ntxMaxSlope.ReadOnly = true;
            this.ntxMaxSlope.Size = new System.Drawing.Size(100, 20);
            this.ntxMaxSlope.TabIndex = 82;
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(6, 74);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(69, 13);
            this.label107.TabIndex = 81;
            this.label107.Text = "Height Delta:";
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(6, 48);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(57, 13);
            this.label108.TabIndex = 80;
            this.label108.Text = "Min Slope:";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(6, 22);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(60, 13);
            this.label109.TabIndex = 79;
            this.label109.Text = "Max Slope:";
            // 
            // btnDrawLine
            // 
            this.btnDrawLine.Location = new System.Drawing.Point(267, 39);
            this.btnDrawLine.Name = "btnDrawLine";
            this.btnDrawLine.Size = new System.Drawing.Size(118, 23);
            this.btnDrawLine.TabIndex = 78;
            this.btnDrawLine.Text = "Draw Line";
            this.btnDrawLine.UseVisualStyleBackColor = true;
            this.btnDrawLine.Click += new System.EventHandler(this.btnDrawLine_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.cbTerrainHeightAsync);
            this.groupBox12.Controls.Add(this.chxHeightFound);
            this.groupBox12.Controls.Add(this.ctrl3DTerrainHeightPt);
            this.groupBox12.Controls.Add(this.ctrlRelativeHeightPt);
            this.groupBox12.Controls.Add(this.btnGetTerrainHeight3);
            this.groupBox12.Controls.Add(this.label104);
            this.groupBox12.Controls.Add(this.label105);
            this.groupBox12.Controls.Add(this.ctrl3DRequestedNormal);
            this.groupBox12.Controls.Add(this.ntxHeight);
            this.groupBox12.Controls.Add(this.label106);
            this.groupBox12.Location = new System.Drawing.Point(3, 15);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(395, 106);
            this.groupBox12.TabIndex = 77;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Terrain Height";
            // 
            // cbTerrainHeightAsync
            // 
            this.cbTerrainHeightAsync.AutoSize = true;
            this.cbTerrainHeightAsync.Location = new System.Drawing.Point(338, 54);
            this.cbTerrainHeightAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbTerrainHeightAsync.Name = "cbTerrainHeightAsync";
            this.cbTerrainHeightAsync.Size = new System.Drawing.Size(55, 17);
            this.cbTerrainHeightAsync.TabIndex = 77;
            this.cbTerrainHeightAsync.Text = "Async";
            this.cbTerrainHeightAsync.UseVisualStyleBackColor = true;
            // 
            // chxHeightFound
            // 
            this.chxHeightFound.AutoSize = true;
            this.chxHeightFound.Enabled = false;
            this.chxHeightFound.Location = new System.Drawing.Point(199, 80);
            this.chxHeightFound.Margin = new System.Windows.Forms.Padding(2);
            this.chxHeightFound.Name = "chxHeightFound";
            this.chxHeightFound.Size = new System.Drawing.Size(90, 17);
            this.chxHeightFound.TabIndex = 76;
            this.chxHeightFound.Text = "Height Found";
            this.chxHeightFound.UseVisualStyleBackColor = true;
            // 
            // ctrl3DTerrainHeightPt
            // 
            this.ctrl3DTerrainHeightPt.IsReadOnly = false;
            this.ctrl3DTerrainHeightPt.Location = new System.Drawing.Point(111, 21);
            this.ctrl3DTerrainHeightPt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DTerrainHeightPt.Name = "ctrl3DTerrainHeightPt";
            this.ctrl3DTerrainHeightPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DTerrainHeightPt.TabIndex = 75;
            this.ctrl3DTerrainHeightPt.X = 0D;
            this.ctrl3DTerrainHeightPt.Y = 0D;
            this.ctrl3DTerrainHeightPt.Z = 0D;
            // 
            // ctrlRelativeHeightPt
            // 
            this.ctrlRelativeHeightPt._DgvControlName = null;
            this.ctrlRelativeHeightPt._IsRelativeToDTM = false;
            this.ctrlRelativeHeightPt._PointInOverlayManagerCoordSys = true;
            this.ctrlRelativeHeightPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrlRelativeHeightPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlRelativeHeightPt._SampleOnePoint = true;
            this.ctrlRelativeHeightPt._UserControlName = "ctrl3DTerrainHeightPt";
            this.ctrlRelativeHeightPt.IsAsync = false;
            this.ctrlRelativeHeightPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlRelativeHeightPt.Location = new System.Drawing.Point(347, 20);
            this.ctrlRelativeHeightPt.Name = "ctrlRelativeHeightPt";
            this.ctrlRelativeHeightPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlRelativeHeightPt.Size = new System.Drawing.Size(33, 23);
            this.ctrlRelativeHeightPt.TabIndex = 74;
            this.ctrlRelativeHeightPt.Text = "...";
            this.ctrlRelativeHeightPt.UseVisualStyleBackColor = true;
            // 
            // btnGetTerrainHeight3
            // 
            this.btnGetTerrainHeight3.Location = new System.Drawing.Point(290, 77);
            this.btnGetTerrainHeight3.Name = "btnGetTerrainHeight3";
            this.btnGetTerrainHeight3.Size = new System.Drawing.Size(95, 23);
            this.btnGetTerrainHeight3.TabIndex = 72;
            this.btnGetTerrainHeight3.Text = "Get Height";
            this.btnGetTerrainHeight3.UseVisualStyleBackColor = true;
            this.btnGetTerrainHeight3.Click += new System.EventHandler(this.btnGetTerrainHeight_Click);
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(4, 25);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(110, 13);
            this.label104.TabIndex = 71;
            this.label104.Text = "Relative Height Point:";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(3, 55);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(101, 13);
            this.label105.TabIndex = 70;
            this.label105.Text = "Requested Normal: ";
            // 
            // ctrl3DRequestedNormal
            // 
            this.ctrl3DRequestedNormal.IsReadOnly = true;
            this.ctrl3DRequestedNormal.Location = new System.Drawing.Point(111, 49);
            this.ctrl3DRequestedNormal.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DRequestedNormal.Name = "ctrl3DRequestedNormal";
            this.ctrl3DRequestedNormal.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DRequestedNormal.TabIndex = 69;
            this.ctrl3DRequestedNormal.X = 0D;
            this.ctrl3DRequestedNormal.Y = 0D;
            this.ctrl3DRequestedNormal.Z = 0D;
            // 
            // ntxHeight
            // 
            this.ntxHeight.Location = new System.Drawing.Point(79, 79);
            this.ntxHeight.Name = "ntxHeight";
            this.ntxHeight.ReadOnly = true;
            this.ntxHeight.Size = new System.Drawing.Size(116, 20);
            this.ntxHeight.TabIndex = 68;
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(3, 80);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(74, 13);
            this.label106.TabIndex = 67;
            this.label106.Text = "Result Height:";
            // 
            // tpRayIntersection
            // 
            this.tpRayIntersection.AutoScroll = true;
            this.tpRayIntersection.Controls.Add(this.cbRayIntersectionAsync);
            this.tpRayIntersection.Controls.Add(this.groupBox5);
            this.tpRayIntersection.Controls.Add(this.gbRayIntersectionTargetResult);
            this.tpRayIntersection.Controls.Add(this.gbRayIntersectionResult);
            this.tpRayIntersection.Controls.Add(this.btnGetRayIntersectionTarget);
            this.tpRayIntersection.Controls.Add(this.btnGetRayIntersection);
            this.tpRayIntersection.Controls.Add(this.label2);
            this.tpRayIntersection.Controls.Add(this.label4);
            this.tpRayIntersection.Controls.Add(this.ctrlRayOrigin);
            this.tpRayIntersection.Controls.Add(this.ctrl3DRayOrigin);
            this.tpRayIntersection.Controls.Add(this.ntxMaxDistance);
            this.tpRayIntersection.Location = new System.Drawing.Point(4, 22);
            this.tpRayIntersection.Name = "tpRayIntersection";
            this.tpRayIntersection.Padding = new System.Windows.Forms.Padding(3);
            this.tpRayIntersection.Size = new System.Drawing.Size(935, 600);
            this.tpRayIntersection.TabIndex = 1;
            this.tpRayIntersection.Text = "Ray Intersection";
            this.tpRayIntersection.UseVisualStyleBackColor = true;
            // 
            // cbRayIntersectionAsync
            // 
            this.cbRayIntersectionAsync.AutoSize = true;
            this.cbRayIntersectionAsync.Location = new System.Drawing.Point(394, 50);
            this.cbRayIntersectionAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbRayIntersectionAsync.Name = "cbRayIntersectionAsync";
            this.cbRayIntersectionAsync.Size = new System.Drawing.Size(55, 17);
            this.cbRayIntersectionAsync.TabIndex = 91;
            this.cbRayIntersectionAsync.Text = "Async";
            this.cbRayIntersectionAsync.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbRayDirection);
            this.groupBox5.Controls.Add(this.ctrlRayDestination);
            this.groupBox5.Controls.Add(this.rbRayDestination);
            this.groupBox5.Controls.Add(this.ctrl3DRayDestination);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.ctrl3DRayDirection);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox5.Location = new System.Drawing.Point(8, 39);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(375, 98);
            this.groupBox5.TabIndex = 90;
            this.groupBox5.TabStop = false;
            // 
            // rbRayDirection
            // 
            this.rbRayDirection.AutoSize = true;
            this.rbRayDirection.Location = new System.Drawing.Point(173, 11);
            this.rbRayDirection.Margin = new System.Windows.Forms.Padding(2);
            this.rbRayDirection.Name = "rbRayDirection";
            this.rbRayDirection.Size = new System.Drawing.Size(122, 17);
            this.rbRayDirection.TabIndex = 1;
            this.rbRayDirection.Text = "Select Ray Direction";
            this.rbRayDirection.UseVisualStyleBackColor = true;
            // 
            // ctrlRayDestination
            // 
            this.ctrlRayDestination._DgvControlName = null;
            this.ctrlRayDestination._IsRelativeToDTM = false;
            this.ctrlRayDestination._PointInOverlayManagerCoordSys = true;
            this.ctrlRayDestination._PointZValue = 1.7976931348623157E+308D;
            this.ctrlRayDestination._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlRayDestination._SampleOnePoint = true;
            this.ctrlRayDestination._UserControlName = "ctrl3DRayDestination";
            this.ctrlRayDestination.IsAsync = false;
            this.ctrlRayDestination.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlRayDestination.Location = new System.Drawing.Point(318, 34);
            this.ctrlRayDestination.Name = "ctrlRayDestination";
            this.ctrlRayDestination.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlRayDestination.Size = new System.Drawing.Size(33, 23);
            this.ctrlRayDestination.TabIndex = 89;
            this.ctrlRayDestination.Text = "...";
            this.ctrlRayDestination.UseVisualStyleBackColor = true;
            // 
            // rbRayDestination
            // 
            this.rbRayDestination.AutoSize = true;
            this.rbRayDestination.Checked = true;
            this.rbRayDestination.Location = new System.Drawing.Point(4, 11);
            this.rbRayDestination.Margin = new System.Windows.Forms.Padding(2);
            this.rbRayDestination.Name = "rbRayDestination";
            this.rbRayDestination.Size = new System.Drawing.Size(133, 17);
            this.rbRayDestination.TabIndex = 0;
            this.rbRayDestination.TabStop = true;
            this.rbRayDestination.Text = "Select Ray Destination";
            this.rbRayDestination.UseVisualStyleBackColor = true;
            this.rbRayDestination.CheckedChanged += new System.EventHandler(this.rbRayDestination_CheckedChanged);
            // 
            // ctrl3DRayDestination
            // 
            this.ctrl3DRayDestination.IsReadOnly = false;
            this.ctrl3DRayDestination.Location = new System.Drawing.Point(87, 34);
            this.ctrl3DRayDestination.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DRayDestination.Name = "ctrl3DRayDestination";
            this.ctrl3DRayDestination.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DRayDestination.TabIndex = 88;
            this.ctrl3DRayDestination.X = 0D;
            this.ctrl3DRayDestination.Y = 0D;
            this.ctrl3DRayDestination.Z = 0D;
            // 
            // ctrl3DRayDirection
            // 
            this.ctrl3DRayDirection.IsReadOnly = false;
            this.ctrl3DRayDirection.Location = new System.Drawing.Point(87, 67);
            this.ctrl3DRayDirection.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DRayDirection.Name = "ctrl3DRayDirection";
            this.ctrl3DRayDirection.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DRayDirection.TabIndex = 74;
            this.ctrl3DRayDirection.X = 0D;
            this.ctrl3DRayDirection.Y = 0D;
            this.ctrl3DRayDirection.Z = 0D;
            // 
            // ctrlRayOrigin
            // 
            this.ctrlRayOrigin._DgvControlName = null;
            this.ctrlRayOrigin._IsRelativeToDTM = false;
            this.ctrlRayOrigin._PointInOverlayManagerCoordSys = true;
            this.ctrlRayOrigin._PointZValue = 1.7976931348623157E+308D;
            this.ctrlRayOrigin._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlRayOrigin._SampleOnePoint = true;
            this.ctrlRayOrigin._UserControlName = "ctrl3DRayOrigin";
            this.ctrlRayOrigin.IsAsync = false;
            this.ctrlRayOrigin.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlRayOrigin.Location = new System.Drawing.Point(325, 9);
            this.ctrlRayOrigin.Name = "ctrlRayOrigin";
            this.ctrlRayOrigin.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlRayOrigin.Size = new System.Drawing.Size(33, 23);
            this.ctrlRayOrigin.TabIndex = 83;
            this.ctrlRayOrigin.Text = "...";
            this.ctrlRayOrigin.UseVisualStyleBackColor = true;
            // 
            // ctrl3DRayOrigin
            // 
            this.ctrl3DRayOrigin.IsReadOnly = false;
            this.ctrl3DRayOrigin.Location = new System.Drawing.Point(87, 8);
            this.ctrl3DRayOrigin.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DRayOrigin.Name = "ctrl3DRayOrigin";
            this.ctrl3DRayOrigin.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DRayOrigin.TabIndex = 72;
            this.ctrl3DRayOrigin.X = 0D;
            this.ctrl3DRayOrigin.Y = 0D;
            this.ctrl3DRayOrigin.Z = 0D;
            // 
            // ntxMaxDistance
            // 
            this.ntxMaxDistance.Location = new System.Drawing.Point(473, 14);
            this.ntxMaxDistance.Name = "ntxMaxDistance";
            this.ntxMaxDistance.Size = new System.Drawing.Size(79, 20);
            this.ntxMaxDistance.TabIndex = 76;
            this.ntxMaxDistance.Text = "10000";
            // 
            // tpSightLines
            // 
            this.tpSightLines.AutoScroll = true;
            this.tpSightLines.Controls.Add(this.tcSightLines);
            this.tpSightLines.Location = new System.Drawing.Point(4, 22);
            this.tpSightLines.Name = "tpSightLines";
            this.tpSightLines.Padding = new System.Windows.Forms.Padding(3);
            this.tpSightLines.Size = new System.Drawing.Size(935, 600);
            this.tpSightLines.TabIndex = 2;
            this.tpSightLines.Text = "Sight";
            this.tpSightLines.UseVisualStyleBackColor = true;
            // 
            // tcSightLines
            // 
            this.tcSightLines.Controls.Add(this.tpLineOfSight);
            this.tcSightLines.Controls.Add(this.tpAreaOfSight);
            this.tcSightLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSightLines.Location = new System.Drawing.Point(3, 3);
            this.tcSightLines.Name = "tcSightLines";
            this.tcSightLines.SelectedIndex = 0;
            this.tcSightLines.Size = new System.Drawing.Size(929, 594);
            this.tcSightLines.TabIndex = 139;
            // 
            // tpLineOfSight
            // 
            this.tpLineOfSight.Controls.Add(this.groupBox18);
            this.tpLineOfSight.Controls.Add(this.cbGetLineOfSightAsync);
            this.tpLineOfSight.Controls.Add(this.cbGetPointVisibilityAsync);
            this.tpLineOfSight.Controls.Add(this.label100);
            this.tpLineOfSight.Controls.Add(this.label99);
            this.tpLineOfSight.Controls.Add(this.ntxCrestClearanceDistance);
            this.tpLineOfSight.Controls.Add(this.label48);
            this.tpLineOfSight.Controls.Add(this.gbLineOfSightGeneralParams);
            this.tpLineOfSight.Controls.Add(this.ntxCrestClearanceAngle);
            this.tpLineOfSight.Controls.Add(this.groupBox4);
            this.tpLineOfSight.Controls.Add(this.label44);
            this.tpLineOfSight.Controls.Add(this.chxLOSIsTargetVisible);
            this.tpLineOfSight.Controls.Add(this.btnGetLineOfSightPoints);
            this.tpLineOfSight.Controls.Add(this.label28);
            this.tpLineOfSight.Controls.Add(this.ntxLOSMinimalTargetHeightForVisibility);
            this.tpLineOfSight.Controls.Add(this.btnGetLineOfSight);
            this.tpLineOfSight.Controls.Add(this.label29);
            this.tpLineOfSight.Controls.Add(this.ntxLOSMinimalScouterHeightForVisibility);
            this.tpLineOfSight.Location = new System.Drawing.Point(4, 22);
            this.tpLineOfSight.Name = "tpLineOfSight";
            this.tpLineOfSight.Padding = new System.Windows.Forms.Padding(3);
            this.tpLineOfSight.Size = new System.Drawing.Size(921, 568);
            this.tpLineOfSight.TabIndex = 0;
            this.tpLineOfSight.Text = "Line Of Sight";
            this.tpLineOfSight.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.chxMinimalScouterHeight);
            this.groupBox18.Controls.Add(this.chxMinimalTargetHeight);
            this.groupBox18.Location = new System.Drawing.Point(6, 175);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(158, 67);
            this.groupBox18.TabIndex = 179;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Calculation Options";
            // 
            // chxMinimalScouterHeight
            // 
            this.chxMinimalScouterHeight.AutoSize = true;
            this.chxMinimalScouterHeight.Location = new System.Drawing.Point(6, 19);
            this.chxMinimalScouterHeight.Name = "chxMinimalScouterHeight";
            this.chxMinimalScouterHeight.Size = new System.Drawing.Size(138, 17);
            this.chxMinimalScouterHeight.TabIndex = 176;
            this.chxMinimalScouterHeight.Text = "Minimal Scouter Height ";
            this.chxMinimalScouterHeight.UseVisualStyleBackColor = true;
            this.chxMinimalScouterHeight.CheckedChanged += new System.EventHandler(this.chxMinimalScouterHeight_CheckedChanged);
            // 
            // chxMinimalTargetHeight
            // 
            this.chxMinimalTargetHeight.AutoSize = true;
            this.chxMinimalTargetHeight.Location = new System.Drawing.Point(6, 42);
            this.chxMinimalTargetHeight.Name = "chxMinimalTargetHeight";
            this.chxMinimalTargetHeight.Size = new System.Drawing.Size(129, 17);
            this.chxMinimalTargetHeight.TabIndex = 177;
            this.chxMinimalTargetHeight.Text = "Minimal Target Height";
            this.chxMinimalTargetHeight.UseVisualStyleBackColor = true;
            this.chxMinimalTargetHeight.CheckedChanged += new System.EventHandler(this.chxMinimalTargetHeight_CheckedChanged);
            // 
            // cbGetLineOfSightAsync
            // 
            this.cbGetLineOfSightAsync.AutoSize = true;
            this.cbGetLineOfSightAsync.Location = new System.Drawing.Point(12, 366);
            this.cbGetLineOfSightAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbGetLineOfSightAsync.Name = "cbGetLineOfSightAsync";
            this.cbGetLineOfSightAsync.Size = new System.Drawing.Size(55, 17);
            this.cbGetLineOfSightAsync.TabIndex = 140;
            this.cbGetLineOfSightAsync.Text = "Async";
            this.cbGetLineOfSightAsync.UseVisualStyleBackColor = true;
            // 
            // cbGetPointVisibilityAsync
            // 
            this.cbGetPointVisibilityAsync.AutoSize = true;
            this.cbGetPointVisibilityAsync.Location = new System.Drawing.Point(217, 189);
            this.cbGetPointVisibilityAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbGetPointVisibilityAsync.Name = "cbGetPointVisibilityAsync";
            this.cbGetPointVisibilityAsync.Size = new System.Drawing.Size(55, 17);
            this.cbGetPointVisibilityAsync.TabIndex = 139;
            this.cbGetPointVisibilityAsync.Text = "Async";
            this.cbGetPointVisibilityAsync.UseVisualStyleBackColor = true;
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label100.Location = new System.Drawing.Point(9, 344);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(109, 13);
            this.label100.TabIndex = 138;
            this.label100.Text = "Get Line Of Sight:";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label99.Location = new System.Drawing.Point(9, 157);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(114, 13);
            this.label99.TabIndex = 137;
            this.label99.Text = "Get Point Visibility:";
            // 
            // ntxCrestClearanceDistance
            // 
            this.ntxCrestClearanceDistance.Location = new System.Drawing.Point(145, 422);
            this.ntxCrestClearanceDistance.Name = "ntxCrestClearanceDistance";
            this.ntxCrestClearanceDistance.ReadOnly = true;
            this.ntxCrestClearanceDistance.Size = new System.Drawing.Size(85, 20);
            this.ntxCrestClearanceDistance.TabIndex = 136;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(9, 425);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(130, 13);
            this.label48.TabIndex = 135;
            this.label48.Text = "Crest Clearance Distance:";
            // 
            // gbLineOfSightGeneralParams
            // 
            this.gbLineOfSightGeneralParams.Controls.Add(this.label26);
            this.gbLineOfSightGeneralParams.Controls.Add(this.chxLOSIsTargetHeightAbsolute);
            this.gbLineOfSightGeneralParams.Controls.Add(this.chxLOSIsScouterHeightAbsolute);
            this.gbLineOfSightGeneralParams.Controls.Add(this.label27);
            this.gbLineOfSightGeneralParams.Controls.Add(this.ctrlSamplePointLOSTarget);
            this.gbLineOfSightGeneralParams.Controls.Add(this.label32);
            this.gbLineOfSightGeneralParams.Controls.Add(this.ntxLOSMinPitchAngle);
            this.gbLineOfSightGeneralParams.Controls.Add(this.ctrl3DVectorLOSTarget);
            this.gbLineOfSightGeneralParams.Controls.Add(this.ntxLOSMaxPitchAngle);
            this.gbLineOfSightGeneralParams.Controls.Add(this.ctrl3DVectorLOSScouter);
            this.gbLineOfSightGeneralParams.Controls.Add(this.ctrlSamplePointLOSScouter);
            this.gbLineOfSightGeneralParams.Controls.Add(this.label33);
            this.gbLineOfSightGeneralParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbLineOfSightGeneralParams.Location = new System.Drawing.Point(3, 3);
            this.gbLineOfSightGeneralParams.Name = "gbLineOfSightGeneralParams";
            this.gbLineOfSightGeneralParams.Size = new System.Drawing.Size(915, 145);
            this.gbLineOfSightGeneralParams.TabIndex = 132;
            this.gbLineOfSightGeneralParams.TabStop = false;
            this.gbLineOfSightGeneralParams.Text = "General Params";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 13);
            this.label26.TabIndex = 100;
            this.label26.Text = "Scouter:";
            // 
            // chxLOSIsTargetHeightAbsolute
            // 
            this.chxLOSIsTargetHeightAbsolute.AutoSize = true;
            this.chxLOSIsTargetHeightAbsolute.Location = new System.Drawing.Point(362, 53);
            this.chxLOSIsTargetHeightAbsolute.Name = "chxLOSIsTargetHeightAbsolute";
            this.chxLOSIsTargetHeightAbsolute.Size = new System.Drawing.Size(146, 17);
            this.chxLOSIsTargetHeightAbsolute.TabIndex = 102;
            this.chxLOSIsTargetHeightAbsolute.Text = "Is Target Height Absolute";
            this.chxLOSIsTargetHeightAbsolute.UseVisualStyleBackColor = true;
            this.chxLOSIsTargetHeightAbsolute.CheckedChanged += new System.EventHandler(this.chxLOSIsTargetHeightAbsolute_CheckedChanged);
            // 
            // chxLOSIsScouterHeightAbsolute
            // 
            this.chxLOSIsScouterHeightAbsolute.AutoSize = true;
            this.chxLOSIsScouterHeightAbsolute.Location = new System.Drawing.Point(362, 24);
            this.chxLOSIsScouterHeightAbsolute.Name = "chxLOSIsScouterHeightAbsolute";
            this.chxLOSIsScouterHeightAbsolute.Size = new System.Drawing.Size(152, 17);
            this.chxLOSIsScouterHeightAbsolute.TabIndex = 98;
            this.chxLOSIsScouterHeightAbsolute.Text = "Is Scouter Height Absolute";
            this.chxLOSIsScouterHeightAbsolute.UseVisualStyleBackColor = true;
            this.chxLOSIsScouterHeightAbsolute.CheckedChanged += new System.EventHandler(this.chxLOSIsScouterHeightAbsolute_CheckedChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 54);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(41, 13);
            this.label27.TabIndex = 101;
            this.label27.Text = "Target:";
            // 
            // ctrlSamplePointLOSTarget
            // 
            this.ctrlSamplePointLOSTarget._DgvControlName = null;
            this.ctrlSamplePointLOSTarget._IsRelativeToDTM = false;
            this.ctrlSamplePointLOSTarget._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointLOSTarget._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointLOSTarget._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointLOSTarget._SampleOnePoint = true;
            this.ctrlSamplePointLOSTarget._UserControlName = "ctrl3DVectorLOSTarget";
            this.ctrlSamplePointLOSTarget.IsAsync = false;
            this.ctrlSamplePointLOSTarget.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointLOSTarget.Location = new System.Drawing.Point(297, 49);
            this.ctrlSamplePointLOSTarget.Name = "ctrlSamplePointLOSTarget";
            this.ctrlSamplePointLOSTarget.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointLOSTarget.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePointLOSTarget.TabIndex = 99;
            this.ctrlSamplePointLOSTarget.Text = "...";
            this.ctrlSamplePointLOSTarget.UseVisualStyleBackColor = true;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 88);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(87, 13);
            this.label32.TabIndex = 108;
            this.label32.Text = "Max Pitch Angle:";
            // 
            // ntxLOSMinPitchAngle
            // 
            this.ntxLOSMinPitchAngle.Location = new System.Drawing.Point(99, 111);
            this.ntxLOSMinPitchAngle.Name = "ntxLOSMinPitchAngle";
            this.ntxLOSMinPitchAngle.Size = new System.Drawing.Size(100, 20);
            this.ntxLOSMinPitchAngle.TabIndex = 111;
            this.ntxLOSMinPitchAngle.Text = "-90";
            // 
            // ctrl3DVectorLOSTarget
            // 
            this.ctrl3DVectorLOSTarget.IsReadOnly = false;
            this.ctrl3DVectorLOSTarget.Location = new System.Drawing.Point(59, 49);
            this.ctrl3DVectorLOSTarget.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorLOSTarget.Name = "ctrl3DVectorLOSTarget";
            this.ctrl3DVectorLOSTarget.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorLOSTarget.TabIndex = 97;
            this.ctrl3DVectorLOSTarget.X = 0D;
            this.ctrl3DVectorLOSTarget.Y = 0D;
            this.ctrl3DVectorLOSTarget.Z = 0D;
            // 
            // ntxLOSMaxPitchAngle
            // 
            this.ntxLOSMaxPitchAngle.Location = new System.Drawing.Point(99, 85);
            this.ntxLOSMaxPitchAngle.Name = "ntxLOSMaxPitchAngle";
            this.ntxLOSMaxPitchAngle.Size = new System.Drawing.Size(100, 20);
            this.ntxLOSMaxPitchAngle.TabIndex = 109;
            this.ntxLOSMaxPitchAngle.Text = "90";
            // 
            // ctrl3DVectorLOSScouter
            // 
            this.ctrl3DVectorLOSScouter.IsReadOnly = false;
            this.ctrl3DVectorLOSScouter.Location = new System.Drawing.Point(59, 17);
            this.ctrl3DVectorLOSScouter.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorLOSScouter.Name = "ctrl3DVectorLOSScouter";
            this.ctrl3DVectorLOSScouter.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorLOSScouter.TabIndex = 95;
            this.ctrl3DVectorLOSScouter.X = 0D;
            this.ctrl3DVectorLOSScouter.Y = 0D;
            this.ctrl3DVectorLOSScouter.Z = 0D;
            // 
            // ctrlSamplePointLOSScouter
            // 
            this.ctrlSamplePointLOSScouter._DgvControlName = null;
            this.ctrlSamplePointLOSScouter._IsRelativeToDTM = false;
            this.ctrlSamplePointLOSScouter._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointLOSScouter._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointLOSScouter._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointLOSScouter._SampleOnePoint = true;
            this.ctrlSamplePointLOSScouter._UserControlName = "ctrl3DVectorLOSScouter";
            this.ctrlSamplePointLOSScouter.IsAsync = false;
            this.ctrlSamplePointLOSScouter.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointLOSScouter.Location = new System.Drawing.Point(297, 20);
            this.ctrlSamplePointLOSScouter.Name = "ctrlSamplePointLOSScouter";
            this.ctrlSamplePointLOSScouter.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointLOSScouter.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePointLOSScouter.TabIndex = 96;
            this.ctrlSamplePointLOSScouter.Text = "...";
            this.ctrlSamplePointLOSScouter.UseVisualStyleBackColor = true;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 114);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(84, 13);
            this.label33.TabIndex = 110;
            this.label33.Text = "Min Pitch Angle:";
            // 
            // ntxCrestClearanceAngle
            // 
            this.ntxCrestClearanceAngle.Location = new System.Drawing.Point(145, 396);
            this.ntxCrestClearanceAngle.Name = "ntxCrestClearanceAngle";
            this.ntxCrestClearanceAngle.ReadOnly = true;
            this.ntxCrestClearanceAngle.Size = new System.Drawing.Size(85, 20);
            this.ntxCrestClearanceAngle.TabIndex = 134;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(3, 333);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(838, 2);
            this.groupBox4.TabIndex = 131;
            this.groupBox4.TabStop = false;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(9, 399);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(115, 13);
            this.label44.TabIndex = 133;
            this.label44.Text = "Crest Clearance Angle:";
            // 
            // chxLOSIsTargetVisible
            // 
            this.chxLOSIsTargetVisible.AutoSize = true;
            this.chxLOSIsTargetVisible.Enabled = false;
            this.chxLOSIsTargetVisible.Location = new System.Drawing.Point(12, 250);
            this.chxLOSIsTargetVisible.Name = "chxLOSIsTargetVisible";
            this.chxLOSIsTargetVisible.Size = new System.Drawing.Size(101, 17);
            this.chxLOSIsTargetVisible.TabIndex = 103;
            this.chxLOSIsTargetVisible.Text = "Is Target Visible";
            this.chxLOSIsTargetVisible.UseVisualStyleBackColor = true;
            // 
            // btnGetLineOfSightPoints
            // 
            this.btnGetLineOfSightPoints.Location = new System.Drawing.Point(83, 362);
            this.btnGetLineOfSightPoints.Name = "btnGetLineOfSightPoints";
            this.btnGetLineOfSightPoints.Size = new System.Drawing.Size(75, 23);
            this.btnGetLineOfSightPoints.TabIndex = 113;
            this.btnGetLineOfSightPoints.Text = "Get";
            this.btnGetLineOfSightPoints.UseVisualStyleBackColor = true;
            this.btnGetLineOfSightPoints.Click += new System.EventHandler(this.btnGetLineOfSightPoints_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(9, 303);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(170, 13);
            this.label28.TabIndex = 104;
            this.label28.Text = "Minimal Target Height For Visibility:";
            // 
            // ntxLOSMinimalTargetHeightForVisibility
            // 
            this.ntxLOSMinimalTargetHeightForVisibility.Location = new System.Drawing.Point(191, 300);
            this.ntxLOSMinimalTargetHeightForVisibility.Name = "ntxLOSMinimalTargetHeightForVisibility";
            this.ntxLOSMinimalTargetHeightForVisibility.ReadOnly = true;
            this.ntxLOSMinimalTargetHeightForVisibility.Size = new System.Drawing.Size(100, 20);
            this.ntxLOSMinimalTargetHeightForVisibility.TabIndex = 105;
            // 
            // btnGetLineOfSight
            // 
            this.btnGetLineOfSight.Location = new System.Drawing.Point(217, 211);
            this.btnGetLineOfSight.Name = "btnGetLineOfSight";
            this.btnGetLineOfSight.Size = new System.Drawing.Size(75, 23);
            this.btnGetLineOfSight.TabIndex = 112;
            this.btnGetLineOfSight.Text = "Get";
            this.btnGetLineOfSight.UseVisualStyleBackColor = true;
            this.btnGetLineOfSight.Click += new System.EventHandler(this.btnGetLineOfSight_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(9, 277);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(176, 13);
            this.label29.TabIndex = 106;
            this.label29.Text = "Minimal Scouter Height For Visibility:";
            // 
            // ntxLOSMinimalScouterHeightForVisibility
            // 
            this.ntxLOSMinimalScouterHeightForVisibility.Location = new System.Drawing.Point(191, 274);
            this.ntxLOSMinimalScouterHeightForVisibility.Name = "ntxLOSMinimalScouterHeightForVisibility";
            this.ntxLOSMinimalScouterHeightForVisibility.ReadOnly = true;
            this.ntxLOSMinimalScouterHeightForVisibility.Size = new System.Drawing.Size(100, 20);
            this.ntxLOSMinimalScouterHeightForVisibility.TabIndex = 107;
            // 
            // tpAreaOfSight
            // 
            this.tpAreaOfSight.Controls.Add(this.groupBox22);
            this.tpAreaOfSight.Controls.Add(this.groupBox16);
            this.tpAreaOfSight.Controls.Add(this.tabControl2);
            this.tpAreaOfSight.Controls.Add(this.grpShowOperations);
            this.tpAreaOfSight.Location = new System.Drawing.Point(4, 22);
            this.tpAreaOfSight.Name = "tpAreaOfSight";
            this.tpAreaOfSight.Padding = new System.Windows.Forms.Padding(3);
            this.tpAreaOfSight.Size = new System.Drawing.Size(921, 568);
            this.tpAreaOfSight.TabIndex = 1;
            this.tpAreaOfSight.Text = "Area Of Sight";
            this.tpAreaOfSight.UseVisualStyleBackColor = true;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.btnAOSExport);
            this.groupBox22.Controls.Add(this.btnAOSImport);
            this.groupBox22.Controls.Add(this.rgbSightObjectRect);
            this.groupBox22.Controls.Add(this.rgbSightObjectEllipse);
            this.groupBox22.Controls.Add(this.btnUpdateEllipse);
            this.groupBox22.Controls.Add(this.ctrlPointsGridSightObject);
            this.groupBox22.Controls.Add(this.btnDrawEllipseAreaOfSight);
            this.groupBox22.Controls.Add(this.rbSightObjectPolygon);
            this.groupBox22.Location = new System.Drawing.Point(6, 6);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(909, 146);
            this.groupBox22.TabIndex = 180;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Sight Object";
            // 
            // btnAOSExport
            // 
            this.btnAOSExport.Location = new System.Drawing.Point(794, 112);
            this.btnAOSExport.Name = "btnAOSExport";
            this.btnAOSExport.Size = new System.Drawing.Size(110, 23);
            this.btnAOSExport.TabIndex = 142;
            this.btnAOSExport.Text = "Export Sight Object";
            this.btnAOSExport.UseVisualStyleBackColor = true;
            this.btnAOSExport.Click += new System.EventHandler(this.btnAOSExport_Click);
            // 
            // btnAOSImport
            // 
            this.btnAOSImport.Location = new System.Drawing.Point(794, 83);
            this.btnAOSImport.Name = "btnAOSImport";
            this.btnAOSImport.Size = new System.Drawing.Size(110, 23);
            this.btnAOSImport.TabIndex = 143;
            this.btnAOSImport.Text = "Import Sight Object";
            this.btnAOSImport.UseVisualStyleBackColor = true;
            this.btnAOSImport.Click += new System.EventHandler(this.btnAOSImport_Click);
            // 
            // rgbSightObjectRect
            // 
            this.rgbSightObjectRect.Controls.Add(this.lblRectHeight);
            this.rgbSightObjectRect.Controls.Add(this.ntxRectHeight);
            this.rgbSightObjectRect.Controls.Add(this.lblRectWidth);
            this.rgbSightObjectRect.Controls.Add(this.ntxRectWidth);
            this.rgbSightObjectRect.Location = new System.Drawing.Point(5, 71);
            this.rgbSightObjectRect.Name = "rgbSightObjectRect";
            this.rgbSightObjectRect.Size = new System.Drawing.Size(264, 45);
            this.rgbSightObjectRect.TabIndex = 138;
            this.rgbSightObjectRect.TabStop = false;
            this.rgbSightObjectRect.Text = "Rectangle";
            this.rgbSightObjectRect.CheckedChanged += new System.EventHandler(this.rgbSightObjectRect_CheckedChanged);
            // 
            // lblRectHeight
            // 
            this.lblRectHeight.AutoSize = true;
            this.lblRectHeight.Location = new System.Drawing.Point(12, 22);
            this.lblRectHeight.Name = "lblRectHeight";
            this.lblRectHeight.Size = new System.Drawing.Size(41, 13);
            this.lblRectHeight.TabIndex = 169;
            this.lblRectHeight.Text = "Height:";
            // 
            // ntxRectHeight
            // 
            this.ntxRectHeight.Location = new System.Drawing.Point(69, 19);
            this.ntxRectHeight.Name = "ntxRectHeight";
            this.ntxRectHeight.Size = new System.Drawing.Size(58, 20);
            this.ntxRectHeight.TabIndex = 170;
            this.ntxRectHeight.Text = "1";
            // 
            // lblRectWidth
            // 
            this.lblRectWidth.AutoSize = true;
            this.lblRectWidth.Location = new System.Drawing.Point(136, 22);
            this.lblRectWidth.Name = "lblRectWidth";
            this.lblRectWidth.Size = new System.Drawing.Size(38, 13);
            this.lblRectWidth.TabIndex = 172;
            this.lblRectWidth.Text = "Width:";
            // 
            // ntxRectWidth
            // 
            this.ntxRectWidth.Location = new System.Drawing.Point(190, 19);
            this.ntxRectWidth.Name = "ntxRectWidth";
            this.ntxRectWidth.Size = new System.Drawing.Size(58, 20);
            this.ntxRectWidth.TabIndex = 171;
            this.ntxRectWidth.Text = "1";
            // 
            // rgbSightObjectEllipse
            // 
            this.rgbSightObjectEllipse.Checked = true;
            this.rgbSightObjectEllipse.Controls.Add(this.ntxAOSRadiusY);
            this.rgbSightObjectEllipse.Controls.Add(this.ntxAOSRadiusX);
            this.rgbSightObjectEllipse.Controls.Add(this.ntxAOSStartAngle);
            this.rgbSightObjectEllipse.Controls.Add(this.label39);
            this.rgbSightObjectEllipse.Controls.Add(this.label40);
            this.rgbSightObjectEllipse.Controls.Add(this.label37);
            this.rgbSightObjectEllipse.Controls.Add(this.label41);
            this.rgbSightObjectEllipse.Controls.Add(this.ntxAOSEndAngle);
            this.rgbSightObjectEllipse.Location = new System.Drawing.Point(5, 19);
            this.rgbSightObjectEllipse.Name = "rgbSightObjectEllipse";
            this.rgbSightObjectEllipse.Size = new System.Drawing.Size(504, 46);
            this.rgbSightObjectEllipse.TabIndex = 137;
            this.rgbSightObjectEllipse.TabStop = false;
            this.rgbSightObjectEllipse.Text = "Ellipse";
            this.rgbSightObjectEllipse.CheckedChanged += new System.EventHandler(this.rgbSightObjectEllipse_CheckedChanged);
            // 
            // ntxAOSRadiusY
            // 
            this.ntxAOSRadiusY.Location = new System.Drawing.Point(190, 19);
            this.ntxAOSRadiusY.Name = "ntxAOSRadiusY";
            this.ntxAOSRadiusY.Size = new System.Drawing.Size(58, 20);
            this.ntxAOSRadiusY.TabIndex = 120;
            this.ntxAOSRadiusY.Text = "1";
            // 
            // ntxAOSRadiusX
            // 
            this.ntxAOSRadiusX.Location = new System.Drawing.Point(69, 19);
            this.ntxAOSRadiusX.Name = "ntxAOSRadiusX";
            this.ntxAOSRadiusX.Size = new System.Drawing.Size(58, 20);
            this.ntxAOSRadiusX.TabIndex = 118;
            this.ntxAOSRadiusX.Text = "1";
            // 
            // ntxAOSStartAngle
            // 
            this.ntxAOSStartAngle.Location = new System.Drawing.Point(322, 19);
            this.ntxAOSStartAngle.Name = "ntxAOSStartAngle";
            this.ntxAOSStartAngle.Size = new System.Drawing.Size(43, 20);
            this.ntxAOSStartAngle.TabIndex = 122;
            this.ntxAOSStartAngle.Text = "0";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(131, 22);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(53, 13);
            this.label39.TabIndex = 119;
            this.label39.Text = "Radius Y:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(378, 22);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(59, 13);
            this.label40.TabIndex = 123;
            this.label40.Text = "End Angle:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(10, 22);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(53, 13);
            this.label37.TabIndex = 117;
            this.label37.Text = "Radius X:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(254, 22);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(62, 13);
            this.label41.TabIndex = 121;
            this.label41.Text = "Start Angle:";
            // 
            // ntxAOSEndAngle
            // 
            this.ntxAOSEndAngle.Location = new System.Drawing.Point(443, 19);
            this.ntxAOSEndAngle.Name = "ntxAOSEndAngle";
            this.ntxAOSEndAngle.Size = new System.Drawing.Size(45, 20);
            this.ntxAOSEndAngle.TabIndex = 124;
            this.ntxAOSEndAngle.Text = "360";
            // 
            // btnUpdateEllipse
            // 
            this.btnUpdateEllipse.Location = new System.Drawing.Point(793, 53);
            this.btnUpdateEllipse.Name = "btnUpdateEllipse";
            this.btnUpdateEllipse.Size = new System.Drawing.Size(79, 23);
            this.btnUpdateEllipse.TabIndex = 135;
            this.btnUpdateEllipse.Text = "Update";
            this.btnUpdateEllipse.UseVisualStyleBackColor = true;
            this.btnUpdateEllipse.Click += new System.EventHandler(this.btnUpdateSightObject_Click);
            // 
            // ctrlPointsGridSightObject
            // 
            this.ctrlPointsGridSightObject.Location = new System.Drawing.Point(515, 8);
            this.ctrlPointsGridSightObject.Name = "ctrlPointsGridSightObject";
            this.ctrlPointsGridSightObject.Size = new System.Drawing.Size(255, 198);
            this.ctrlPointsGridSightObject.TabIndex = 136;
            // 
            // btnDrawEllipseAreaOfSight
            // 
            this.btnDrawEllipseAreaOfSight.Location = new System.Drawing.Point(794, 26);
            this.btnDrawEllipseAreaOfSight.Name = "btnDrawEllipseAreaOfSight";
            this.btnDrawEllipseAreaOfSight.Size = new System.Drawing.Size(79, 23);
            this.btnDrawEllipseAreaOfSight.TabIndex = 134;
            this.btnDrawEllipseAreaOfSight.Text = "Draw";
            this.btnDrawEllipseAreaOfSight.UseVisualStyleBackColor = true;
            this.btnDrawEllipseAreaOfSight.Click += new System.EventHandler(this.btnDrawSightObject_Click);
            // 
            // rbSightObjectPolygon
            // 
            this.rbSightObjectPolygon.AutoSize = true;
            this.rbSightObjectPolygon.Location = new System.Drawing.Point(15, 122);
            this.rbSightObjectPolygon.Name = "rbSightObjectPolygon";
            this.rbSightObjectPolygon.Size = new System.Drawing.Size(63, 17);
            this.rbSightObjectPolygon.TabIndex = 1;
            this.rbSightObjectPolygon.TabStop = true;
            this.rbSightObjectPolygon.Text = "Polygon";
            this.rbSightObjectPolygon.UseVisualStyleBackColor = true;
            this.rbSightObjectPolygon.CheckedChanged += new System.EventHandler(this.rbSightObjectPolygon_CheckedChanged);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.gbAOSColors);
            this.groupBox16.Controls.Add(this.label131);
            this.groupBox16.Controls.Add(this.ntxNumCallingFunction);
            this.groupBox16.Controls.Add(this.cbIsMultipleScouters);
            this.groupBox16.Controls.Add(this.gbMultipleScouters);
            this.groupBox16.Controls.Add(this.ctrl3DVectorAOSScouter);
            this.groupBox16.Controls.Add(this.label84);
            this.groupBox16.Controls.Add(this.ntxRotationAzimuthDeg);
            this.groupBox16.Controls.Add(this.ctrlSamplePointAOSScouter);
            this.groupBox16.Controls.Add(this.label119);
            this.groupBox16.Controls.Add(this.cbAreaOfSightAsync);
            this.groupBox16.Controls.Add(this.tbNameCalc);
            this.groupBox16.Controls.Add(this.btnCalcAOS2);
            this.groupBox16.Controls.Add(this.label31);
            this.groupBox16.Controls.Add(this.label115);
            this.groupBox16.Controls.Add(this.groupBox17);
            this.groupBox16.Controls.Add(this.ntxNumberOfRayes);
            this.groupBox16.Controls.Add(this.chxGPU_Based);
            this.groupBox16.Controls.Add(this.chxAOSIsScouterHeightAbsolute);
            this.groupBox16.Controls.Add(this.label122);
            this.groupBox16.Controls.Add(this.ntxAOSMaxPitchAngle);
            this.groupBox16.Controls.Add(this.label123);
            this.groupBox16.Controls.Add(this.ntxAOSMinPitchAngle);
            this.groupBox16.Controls.Add(this.chxAOSIsTargetsHeightAbsolute);
            this.groupBox16.Controls.Add(this.label124);
            this.groupBox16.Controls.Add(this.ntxAOSTargetHeight);
            this.groupBox16.Controls.Add(this.label125);
            this.groupBox16.Controls.Add(this.ntxAOSGPUTargetResolution);
            this.groupBox16.Location = new System.Drawing.Point(6, 152);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(909, 252);
            this.groupBox16.TabIndex = 129;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "General Params";
            // 
            // gbAOSColors
            // 
            this.gbAOSColors.Controls.Add(this.cmbAOSColors);
            this.gbAOSColors.Controls.Add(this.label121);
            this.gbAOSColors.Controls.Add(this.picbColor);
            this.gbAOSColors.Controls.Add(this.label117);
            this.gbAOSColors.Controls.Add(this.numUDAlphaColor);
            this.gbAOSColors.Location = new System.Drawing.Point(609, 53);
            this.gbAOSColors.Name = "gbAOSColors";
            this.gbAOSColors.Size = new System.Drawing.Size(167, 79);
            this.gbAOSColors.TabIndex = 193;
            this.gbAOSColors.TabStop = false;
            this.gbAOSColors.Text = "Define Colors";
            // 
            // cmbAOSColors
            // 
            this.cmbAOSColors.FormattingEnabled = true;
            this.cmbAOSColors.Location = new System.Drawing.Point(5, 18);
            this.cmbAOSColors.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAOSColors.Name = "cmbAOSColors";
            this.cmbAOSColors.Size = new System.Drawing.Size(153, 21);
            this.cmbAOSColors.TabIndex = 192;
            this.cmbAOSColors.SelectedIndexChanged += new System.EventHandler(this.cmbAOSColors_SelectedIndexChanged);
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Location = new System.Drawing.Point(5, 51);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(34, 13);
            this.label121.TabIndex = 164;
            this.label121.Text = "Color:";
            // 
            // picbColor
            // 
            this.picbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbColor.Location = new System.Drawing.Point(39, 46);
            this.picbColor.Name = "picbColor";
            this.picbColor.Size = new System.Drawing.Size(25, 22);
            this.picbColor.TabIndex = 156;
            this.picbColor.TabStop = false;
            this.picbColor.Click += new System.EventHandler(this.picbColor_Click);
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Location = new System.Drawing.Point(69, 49);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(37, 13);
            this.label117.TabIndex = 155;
            this.label117.Text = "Alpha:";
            // 
            // numUDAlphaColor
            // 
            this.numUDAlphaColor.Location = new System.Drawing.Point(107, 47);
            this.numUDAlphaColor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDAlphaColor.Name = "numUDAlphaColor";
            this.numUDAlphaColor.Size = new System.Drawing.Size(53, 20);
            this.numUDAlphaColor.TabIndex = 154;
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Location = new System.Drawing.Point(589, 199);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(110, 13);
            this.label131.TabIndex = 190;
            this.label131.Text = "Num Calling Function:";
            // 
            // ntxNumCallingFunction
            // 
            this.ntxNumCallingFunction.Location = new System.Drawing.Point(705, 195);
            this.ntxNumCallingFunction.Name = "ntxNumCallingFunction";
            this.ntxNumCallingFunction.Size = new System.Drawing.Size(30, 20);
            this.ntxNumCallingFunction.TabIndex = 191;
            this.ntxNumCallingFunction.Text = "1";
            this.toolTip1.SetToolTip(this.ntxNumCallingFunction, "Relative for ellipse object and async operation");
            // 
            // cbIsMultipleScouters
            // 
            this.cbIsMultipleScouters.AutoSize = true;
            this.cbIsMultipleScouters.Location = new System.Drawing.Point(8, 39);
            this.cbIsMultipleScouters.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsMultipleScouters.Name = "cbIsMultipleScouters";
            this.cbIsMultipleScouters.Size = new System.Drawing.Size(233, 17);
            this.cbIsMultipleScouters.TabIndex = 189;
            this.cbIsMultipleScouters.Text = "Is Multiple Scouters (Only For Ellipse Object)";
            this.cbIsMultipleScouters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbIsMultipleScouters.UseVisualStyleBackColor = true;
            this.cbIsMultipleScouters.CheckedChanged += new System.EventHandler(this.cbIsMultipleScouters_CheckedChanged);
            // 
            // gbMultipleScouters
            // 
            this.gbMultipleScouters.Controls.Add(this.ctrlPointsGridMultipleScouters);
            this.gbMultipleScouters.Controls.Add(this.cbCalcBestPointsAsync);
            this.gbMultipleScouters.Controls.Add(this.label83);
            this.gbMultipleScouters.Controls.Add(this.label68);
            this.gbMultipleScouters.Controls.Add(this.ctrl3DScouterCenterPoint);
            this.gbMultipleScouters.Controls.Add(this.cmbEScoutersSumType);
            this.gbMultipleScouters.Controls.Add(this.label52);
            this.gbMultipleScouters.Controls.Add(this.ctrlSamplePointMultipleScouter);
            this.gbMultipleScouters.Controls.Add(this.ntxMaxNumOfScouters);
            this.gbMultipleScouters.Controls.Add(this.btnCalcBestPoints);
            this.gbMultipleScouters.Controls.Add(this.label35);
            this.gbMultipleScouters.Controls.Add(this.ctrlSamplePointScouter);
            this.gbMultipleScouters.Controls.Add(this.label51);
            this.gbMultipleScouters.Controls.Add(this.ntxScouterRadiusX);
            this.gbMultipleScouters.Controls.Add(this.ntxScouterRadiusY);
            this.gbMultipleScouters.Location = new System.Drawing.Point(3, 53);
            this.gbMultipleScouters.Margin = new System.Windows.Forms.Padding(2);
            this.gbMultipleScouters.Name = "gbMultipleScouters";
            this.gbMultipleScouters.Padding = new System.Windows.Forms.Padding(2);
            this.gbMultipleScouters.Size = new System.Drawing.Size(601, 137);
            this.gbMultipleScouters.TabIndex = 188;
            this.gbMultipleScouters.TabStop = false;
            // 
            // ctrlPointsGridMultipleScouters
            // 
            this.ctrlPointsGridMultipleScouters.Location = new System.Drawing.Point(2, 33);
            this.ctrlPointsGridMultipleScouters.Name = "ctrlPointsGridMultipleScouters";
            this.ctrlPointsGridMultipleScouters.Size = new System.Drawing.Size(345, 168);
            this.ctrlPointsGridMultipleScouters.TabIndex = 194;
            this.ctrlPointsGridMultipleScouters.Load += new System.EventHandler(this.ctrlPointsGridMultipleScouters_Load);
            // 
            // cbCalcBestPointsAsync
            // 
            this.cbCalcBestPointsAsync.AutoSize = true;
            this.cbCalcBestPointsAsync.Location = new System.Drawing.Point(530, 65);
            this.cbCalcBestPointsAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbCalcBestPointsAsync.Name = "cbCalcBestPointsAsync";
            this.cbCalcBestPointsAsync.Size = new System.Drawing.Size(55, 17);
            this.cbCalcBestPointsAsync.TabIndex = 193;
            this.cbCalcBestPointsAsync.Text = "Async";
            this.cbCalcBestPointsAsync.UseVisualStyleBackColor = true;
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Location = new System.Drawing.Point(1, 11);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(113, 13);
            this.label83.TabIndex = 192;
            this.label83.Text = "Scouters Center Point:";
            // 
            // label68
            // 
            this.label68.Location = new System.Drawing.Point(364, 105);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(103, 20);
            this.label68.TabIndex = 191;
            this.label68.Text = "Scouters Sum Type:";
            // 
            // ctrl3DScouterCenterPoint
            // 
            this.ctrl3DScouterCenterPoint.IsReadOnly = false;
            this.ctrl3DScouterCenterPoint.Location = new System.Drawing.Point(113, 6);
            this.ctrl3DScouterCenterPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DScouterCenterPoint.Name = "ctrl3DScouterCenterPoint";
            this.ctrl3DScouterCenterPoint.Size = new System.Drawing.Size(231, 26);
            this.ctrl3DScouterCenterPoint.TabIndex = 182;
            this.ctrl3DScouterCenterPoint.X = 0D;
            this.ctrl3DScouterCenterPoint.Y = 0D;
            this.ctrl3DScouterCenterPoint.Z = 0D;
            // 
            // cmbEScoutersSumType
            // 
            this.cmbEScoutersSumType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEScoutersSumType.FormattingEnabled = true;
            this.cmbEScoutersSumType.Location = new System.Drawing.Point(471, 103);
            this.cmbEScoutersSumType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEScoutersSumType.Name = "cmbEScoutersSumType";
            this.cmbEScoutersSumType.Size = new System.Drawing.Size(107, 21);
            this.cmbEScoutersSumType.TabIndex = 190;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(364, 39);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(114, 13);
            this.label52.TabIndex = 188;
            this.label52.Text = "Max Num Of Scouters:";
            // 
            // ctrlSamplePointMultipleScouter
            // 
            this.ctrlSamplePointMultipleScouter._DgvControlName = "dgvMultipleScouterPoints";
            this.ctrlSamplePointMultipleScouter._IsRelativeToDTM = false;
            this.ctrlSamplePointMultipleScouter._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointMultipleScouter._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointMultipleScouter._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointMultipleScouter._SampleOnePoint = false;
            this.ctrlSamplePointMultipleScouter._UserControlName = null;
            this.ctrlSamplePointMultipleScouter.IsAsync = false;
            this.ctrlSamplePointMultipleScouter.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointMultipleScouter.Location = new System.Drawing.Point(300, 106);
            this.ctrlSamplePointMultipleScouter.Name = "ctrlSamplePointMultipleScouter";
            this.ctrlSamplePointMultipleScouter.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointMultipleScouter.Size = new System.Drawing.Size(32, 23);
            this.ctrlSamplePointMultipleScouter.TabIndex = 135;
            this.ctrlSamplePointMultipleScouter.Text = "...";
            this.ctrlSamplePointMultipleScouter.UseVisualStyleBackColor = true;
            // 
            // ntxMaxNumOfScouters
            // 
            this.ntxMaxNumOfScouters.Location = new System.Drawing.Point(480, 36);
            this.ntxMaxNumOfScouters.Name = "ntxMaxNumOfScouters";
            this.ntxMaxNumOfScouters.Size = new System.Drawing.Size(33, 20);
            this.ntxMaxNumOfScouters.TabIndex = 189;
            this.ntxMaxNumOfScouters.Text = "32";
            // 
            // btnCalcBestPoints
            // 
            this.btnCalcBestPoints.Location = new System.Drawing.Point(367, 60);
            this.btnCalcBestPoints.Name = "btnCalcBestPoints";
            this.btnCalcBestPoints.Size = new System.Drawing.Size(158, 24);
            this.btnCalcBestPoints.TabIndex = 180;
            this.btnCalcBestPoints.Text = "Get Best Scouters Locations";
            this.btnCalcBestPoints.UseVisualStyleBackColor = true;
            this.btnCalcBestPoints.Click += new System.EventHandler(this.btnCalcBestPoints_Click);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(489, 13);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(53, 13);
            this.label35.TabIndex = 186;
            this.label35.Text = "Radius Y:";
            // 
            // ctrlSamplePointScouter
            // 
            this.ctrlSamplePointScouter._DgvControlName = null;
            this.ctrlSamplePointScouter._IsRelativeToDTM = false;
            this.ctrlSamplePointScouter._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointScouter._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointScouter._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointScouter._SampleOnePoint = true;
            this.ctrlSamplePointScouter._UserControlName = "ctrl3DScouterCenterPoint";
            this.ctrlSamplePointScouter.IsAsync = false;
            this.ctrlSamplePointScouter.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointScouter.Location = new System.Drawing.Point(343, 8);
            this.ctrlSamplePointScouter.Name = "ctrlSamplePointScouter";
            this.ctrlSamplePointScouter.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointScouter.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePointScouter.TabIndex = 183;
            this.ctrlSamplePointScouter.Text = "...";
            this.ctrlSamplePointScouter.UseVisualStyleBackColor = true;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(392, 13);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(53, 13);
            this.label51.TabIndex = 184;
            this.label51.Text = "Radius X:";
            // 
            // ntxScouterRadiusX
            // 
            this.ntxScouterRadiusX.Location = new System.Drawing.Point(445, 10);
            this.ntxScouterRadiusX.Name = "ntxScouterRadiusX";
            this.ntxScouterRadiusX.Size = new System.Drawing.Size(34, 20);
            this.ntxScouterRadiusX.TabIndex = 185;
            this.ntxScouterRadiusX.Text = "1";
            // 
            // ntxScouterRadiusY
            // 
            this.ntxScouterRadiusY.Location = new System.Drawing.Point(543, 10);
            this.ntxScouterRadiusY.Name = "ntxScouterRadiusY";
            this.ntxScouterRadiusY.Size = new System.Drawing.Size(34, 20);
            this.ntxScouterRadiusY.TabIndex = 187;
            this.ntxScouterRadiusY.Text = "1";
            // 
            // ctrl3DVectorAOSScouter
            // 
            this.ctrl3DVectorAOSScouter.IsReadOnly = false;
            this.ctrl3DVectorAOSScouter.Location = new System.Drawing.Point(106, 13);
            this.ctrl3DVectorAOSScouter.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorAOSScouter.Name = "ctrl3DVectorAOSScouter";
            this.ctrl3DVectorAOSScouter.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorAOSScouter.TabIndex = 95;
            this.ctrl3DVectorAOSScouter.X = 0D;
            this.ctrl3DVectorAOSScouter.Y = 0D;
            this.ctrl3DVectorAOSScouter.Z = 0D;
            // 
            // label84
            // 
            this.label84.Location = new System.Drawing.Point(705, 18);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(133, 17);
            this.label84.TabIndex = 168;
            this.label84.Text = "Rotation Azimuth Degree:";
            this.label84.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ntxRotationAzimuthDeg
            // 
            this.ntxRotationAzimuthDeg.Location = new System.Drawing.Point(844, 15);
            this.ntxRotationAzimuthDeg.Name = "ntxRotationAzimuthDeg";
            this.ntxRotationAzimuthDeg.Size = new System.Drawing.Size(60, 20);
            this.ntxRotationAzimuthDeg.TabIndex = 167;
            this.ntxRotationAzimuthDeg.Text = "0";
            // 
            // ctrlSamplePointAOSScouter
            // 
            this.ctrlSamplePointAOSScouter._DgvControlName = null;
            this.ctrlSamplePointAOSScouter._IsRelativeToDTM = false;
            this.ctrlSamplePointAOSScouter._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointAOSScouter._PointZValue = 1.7D;
            this.ctrlSamplePointAOSScouter._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointAOSScouter._SampleOnePoint = true;
            this.ctrlSamplePointAOSScouter._UserControlName = "ctrl3DVectorAOSScouter";
            this.ctrlSamplePointAOSScouter.IsAsync = false;
            this.ctrlSamplePointAOSScouter.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointAOSScouter.Location = new System.Drawing.Point(345, 14);
            this.ctrlSamplePointAOSScouter.Name = "ctrlSamplePointAOSScouter";
            this.ctrlSamplePointAOSScouter.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointAOSScouter.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePointAOSScouter.TabIndex = 96;
            this.ctrlSamplePointAOSScouter.Text = "...";
            this.ctrlSamplePointAOSScouter.UseVisualStyleBackColor = true;
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Location = new System.Drawing.Point(7, 19);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(47, 13);
            this.label119.TabIndex = 100;
            this.label119.Text = "Scouter:";
            // 
            // cbAreaOfSightAsync
            // 
            this.cbAreaOfSightAsync.AutoSize = true;
            this.cbAreaOfSightAsync.Location = new System.Drawing.Point(168, 220);
            this.cbAreaOfSightAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbAreaOfSightAsync.Name = "cbAreaOfSightAsync";
            this.cbAreaOfSightAsync.Size = new System.Drawing.Size(55, 17);
            this.cbAreaOfSightAsync.TabIndex = 184;
            this.cbAreaOfSightAsync.Text = "Async";
            this.cbAreaOfSightAsync.UseVisualStyleBackColor = true;
            // 
            // tbNameCalc
            // 
            this.tbNameCalc.Location = new System.Drawing.Point(511, 218);
            this.tbNameCalc.Name = "tbNameCalc";
            this.tbNameCalc.Size = new System.Drawing.Size(69, 20);
            this.tbNameCalc.TabIndex = 136;
            // 
            // btnCalcAOS2
            // 
            this.btnCalcAOS2.Location = new System.Drawing.Point(748, 223);
            this.btnCalcAOS2.Name = "btnCalcAOS2";
            this.btnCalcAOS2.Size = new System.Drawing.Size(156, 23);
            this.btnCalcAOS2.TabIndex = 179;
            this.btnCalcAOS2.Text = "Calc And Draw Area Of Sight";
            this.btnCalcAOS2.UseVisualStyleBackColor = true;
            this.btnCalcAOS2.Click += new System.EventHandler(this.btnCalcAOS_Click);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(412, 221);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(62, 13);
            this.label31.TabIndex = 135;
            this.label31.Text = "Name Calc:";
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(276, 221);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(88, 13);
            this.label115.TabIndex = 173;
            this.label115.Text = "Number Of Rays:";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.chxCalcStaticObjects);
            this.groupBox17.Controls.Add(this.chxCalcUnseenPolygons);
            this.groupBox17.Controls.Add(this.chxCalcSeenPolygons);
            this.groupBox17.Controls.Add(this.chxCalcLineOfSight);
            this.groupBox17.Controls.Add(this.chxCalcAreaOfSight);
            this.groupBox17.Location = new System.Drawing.Point(780, 53);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(112, 132);
            this.groupBox17.TabIndex = 178;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Calculation Options";
            // 
            // chxCalcStaticObjects
            // 
            this.chxCalcStaticObjects.AutoSize = true;
            this.chxCalcStaticObjects.Checked = true;
            this.chxCalcStaticObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxCalcStaticObjects.Location = new System.Drawing.Point(6, 111);
            this.chxCalcStaticObjects.Name = "chxCalcStaticObjects";
            this.chxCalcStaticObjects.Size = new System.Drawing.Size(92, 17);
            this.chxCalcStaticObjects.TabIndex = 180;
            this.chxCalcStaticObjects.Text = "Static Objects";
            this.chxCalcStaticObjects.UseVisualStyleBackColor = true;
            // 
            // chxCalcUnseenPolygons
            // 
            this.chxCalcUnseenPolygons.AutoSize = true;
            this.chxCalcUnseenPolygons.Checked = true;
            this.chxCalcUnseenPolygons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxCalcUnseenPolygons.Location = new System.Drawing.Point(6, 88);
            this.chxCalcUnseenPolygons.Name = "chxCalcUnseenPolygons";
            this.chxCalcUnseenPolygons.Size = new System.Drawing.Size(109, 17);
            this.chxCalcUnseenPolygons.TabIndex = 179;
            this.chxCalcUnseenPolygons.Text = "Unseen Polygons";
            this.chxCalcUnseenPolygons.UseVisualStyleBackColor = true;
            // 
            // chxCalcSeenPolygons
            // 
            this.chxCalcSeenPolygons.AutoSize = true;
            this.chxCalcSeenPolygons.Checked = true;
            this.chxCalcSeenPolygons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxCalcSeenPolygons.Location = new System.Drawing.Point(6, 65);
            this.chxCalcSeenPolygons.Name = "chxCalcSeenPolygons";
            this.chxCalcSeenPolygons.Size = new System.Drawing.Size(97, 17);
            this.chxCalcSeenPolygons.TabIndex = 178;
            this.chxCalcSeenPolygons.Text = "Seen Polygons";
            this.chxCalcSeenPolygons.UseVisualStyleBackColor = true;
            // 
            // chxCalcLineOfSight
            // 
            this.chxCalcLineOfSight.AutoSize = true;
            this.chxCalcLineOfSight.Checked = true;
            this.chxCalcLineOfSight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxCalcLineOfSight.Location = new System.Drawing.Point(6, 19);
            this.chxCalcLineOfSight.Name = "chxCalcLineOfSight";
            this.chxCalcLineOfSight.Size = new System.Drawing.Size(87, 17);
            this.chxCalcLineOfSight.TabIndex = 176;
            this.chxCalcLineOfSight.Text = "Line Of Sight";
            this.chxCalcLineOfSight.UseVisualStyleBackColor = true;
            // 
            // chxCalcAreaOfSight
            // 
            this.chxCalcAreaOfSight.AutoSize = true;
            this.chxCalcAreaOfSight.Checked = true;
            this.chxCalcAreaOfSight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxCalcAreaOfSight.Location = new System.Drawing.Point(6, 42);
            this.chxCalcAreaOfSight.Name = "chxCalcAreaOfSight";
            this.chxCalcAreaOfSight.Size = new System.Drawing.Size(89, 17);
            this.chxCalcAreaOfSight.TabIndex = 177;
            this.chxCalcAreaOfSight.Text = "Area Of Sight";
            this.chxCalcAreaOfSight.UseVisualStyleBackColor = true;
            this.chxCalcAreaOfSight.CheckedChanged += new System.EventHandler(this.chxCalcAreaOfSight_CheckedChanged);
            // 
            // ntxNumberOfRayes
            // 
            this.ntxNumberOfRayes.Location = new System.Drawing.Point(364, 218);
            this.ntxNumberOfRayes.Name = "ntxNumberOfRayes";
            this.ntxNumberOfRayes.Size = new System.Drawing.Size(31, 20);
            this.ntxNumberOfRayes.TabIndex = 174;
            this.ntxNumberOfRayes.Text = "64";
            // 
            // chxGPU_Based
            // 
            this.chxGPU_Based.AutoSize = true;
            this.chxGPU_Based.Location = new System.Drawing.Point(168, 198);
            this.chxGPU_Based.Name = "chxGPU_Based";
            this.chxGPU_Based.Size = new System.Drawing.Size(93, 17);
            this.chxGPU_Based.TabIndex = 151;
            this.chxGPU_Based.Text = "Is GPU Based";
            this.chxGPU_Based.UseVisualStyleBackColor = true;
            // 
            // chxAOSIsScouterHeightAbsolute
            // 
            this.chxAOSIsScouterHeightAbsolute.AutoSize = true;
            this.chxAOSIsScouterHeightAbsolute.Location = new System.Drawing.Point(6, 220);
            this.chxAOSIsScouterHeightAbsolute.Name = "chxAOSIsScouterHeightAbsolute";
            this.chxAOSIsScouterHeightAbsolute.Size = new System.Drawing.Size(152, 17);
            this.chxAOSIsScouterHeightAbsolute.TabIndex = 98;
            this.chxAOSIsScouterHeightAbsolute.Text = "Is Scouter Height Absolute";
            this.chxAOSIsScouterHeightAbsolute.UseVisualStyleBackColor = true;
            this.chxAOSIsScouterHeightAbsolute.CheckedChanged += new System.EventHandler(this.chxAOSIsScouterHeightAbsolute_CheckedChanged);
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Location = new System.Drawing.Point(543, 19);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(87, 13);
            this.label122.TabIndex = 108;
            this.label122.Text = "Max Pitch Angle:";
            // 
            // ntxAOSMaxPitchAngle
            // 
            this.ntxAOSMaxPitchAngle.Location = new System.Drawing.Point(636, 15);
            this.ntxAOSMaxPitchAngle.Name = "ntxAOSMaxPitchAngle";
            this.ntxAOSMaxPitchAngle.Size = new System.Drawing.Size(59, 20);
            this.ntxAOSMaxPitchAngle.TabIndex = 109;
            this.ntxAOSMaxPitchAngle.Text = "90";
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Location = new System.Drawing.Point(390, 19);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(84, 13);
            this.label123.TabIndex = 110;
            this.label123.Text = "Min Pitch Angle:";
            // 
            // ntxAOSMinPitchAngle
            // 
            this.ntxAOSMinPitchAngle.Location = new System.Drawing.Point(476, 16);
            this.ntxAOSMinPitchAngle.Name = "ntxAOSMinPitchAngle";
            this.ntxAOSMinPitchAngle.Size = new System.Drawing.Size(54, 20);
            this.ntxAOSMinPitchAngle.TabIndex = 111;
            this.ntxAOSMinPitchAngle.Text = "-90";
            // 
            // chxAOSIsTargetsHeightAbsolute
            // 
            this.chxAOSIsTargetsHeightAbsolute.AutoSize = true;
            this.chxAOSIsTargetsHeightAbsolute.Location = new System.Drawing.Point(6, 198);
            this.chxAOSIsTargetsHeightAbsolute.Name = "chxAOSIsTargetsHeightAbsolute";
            this.chxAOSIsTargetsHeightAbsolute.Size = new System.Drawing.Size(151, 17);
            this.chxAOSIsTargetsHeightAbsolute.TabIndex = 102;
            this.chxAOSIsTargetsHeightAbsolute.Text = "Is Targets Height Absolute";
            this.chxAOSIsTargetsHeightAbsolute.UseVisualStyleBackColor = true;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Location = new System.Drawing.Point(276, 199);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(75, 13);
            this.label124.TabIndex = 136;
            this.label124.Text = "Target Height:";
            // 
            // ntxAOSTargetHeight
            // 
            this.ntxAOSTargetHeight.Location = new System.Drawing.Point(364, 195);
            this.ntxAOSTargetHeight.Name = "ntxAOSTargetHeight";
            this.ntxAOSTargetHeight.Size = new System.Drawing.Size(31, 20);
            this.ntxAOSTargetHeight.TabIndex = 137;
            this.ntxAOSTargetHeight.Text = "1.7";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(412, 199);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(94, 13);
            this.label125.TabIndex = 138;
            this.label125.Text = "Target Resolution:";
            // 
            // ntxAOSGPUTargetResolution
            // 
            this.ntxAOSGPUTargetResolution.Location = new System.Drawing.Point(511, 195);
            this.ntxAOSGPUTargetResolution.Name = "ntxAOSGPUTargetResolution";
            this.ntxAOSGPUTargetResolution.Size = new System.Drawing.Size(69, 20);
            this.ntxAOSGPUTargetResolution.TabIndex = 139;
            this.ntxAOSGPUTargetResolution.Text = "10";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(145, 413);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(605, 149);
            this.tabControl2.TabIndex = 150;
            this.tabControl2.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl2_Selecting);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox21);
            this.tabPage4.Controls.Add(this.groupBox20);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(597, 123);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "AOS Operation";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.chxAreSameMatrices);
            this.groupBox21.Controls.Add(this.btnTestSaveAndLoad);
            this.groupBox21.Controls.Add(this.btnAOSLoad);
            this.groupBox21.Controls.Add(this.btnAOSSave);
            this.groupBox21.Location = new System.Drawing.Point(306, 4);
            this.groupBox21.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox21.Size = new System.Drawing.Size(250, 118);
            this.groupBox21.TabIndex = 182;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Save/Load Area Of Sight";
            // 
            // chxAreSameMatrices
            // 
            this.chxAreSameMatrices.AutoSize = true;
            this.chxAreSameMatrices.Enabled = false;
            this.chxAreSameMatrices.Location = new System.Drawing.Point(132, 87);
            this.chxAreSameMatrices.Name = "chxAreSameMatrices";
            this.chxAreSameMatrices.Size = new System.Drawing.Size(115, 17);
            this.chxAreSameMatrices.TabIndex = 185;
            this.chxAreSameMatrices.Text = "Are Same Matrices";
            this.chxAreSameMatrices.UseVisualStyleBackColor = true;
            // 
            // btnTestSaveAndLoad
            // 
            this.btnTestSaveAndLoad.Enabled = false;
            this.btnTestSaveAndLoad.Location = new System.Drawing.Point(13, 83);
            this.btnTestSaveAndLoad.Name = "btnTestSaveAndLoad";
            this.btnTestSaveAndLoad.Size = new System.Drawing.Size(112, 23);
            this.btnTestSaveAndLoad.TabIndex = 138;
            this.btnTestSaveAndLoad.Text = "Test Save and Load";
            this.btnTestSaveAndLoad.UseVisualStyleBackColor = true;
            this.btnTestSaveAndLoad.Click += new System.EventHandler(this.btnTestSaveAndLoad_Click);
            // 
            // btnAOSLoad
            // 
            this.btnAOSLoad.Location = new System.Drawing.Point(28, 54);
            this.btnAOSLoad.Name = "btnAOSLoad";
            this.btnAOSLoad.Size = new System.Drawing.Size(74, 23);
            this.btnAOSLoad.TabIndex = 137;
            this.btnAOSLoad.Text = "Load";
            this.btnAOSLoad.UseVisualStyleBackColor = true;
            this.btnAOSLoad.Click += new System.EventHandler(this.btnAOSLoad_Click);
            // 
            // btnAOSSave
            // 
            this.btnAOSSave.Enabled = false;
            this.btnAOSSave.Location = new System.Drawing.Point(28, 24);
            this.btnAOSSave.Name = "btnAOSSave";
            this.btnAOSSave.Size = new System.Drawing.Size(74, 23);
            this.btnAOSSave.TabIndex = 136;
            this.btnAOSSave.Text = "Save";
            this.btnAOSSave.UseVisualStyleBackColor = true;
            this.btnAOSSave.Click += new System.EventHandler(this.btnAOSSave_Click);
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.btnSaveMatrixToFile);
            this.groupBox20.Controls.Add(this.chxFillPointsVisibility);
            this.groupBox20.Controls.Add(this.chxIsCreateAndDrawMatrixAutomatic);
            this.groupBox20.Controls.Add(this.btnCreateMatrix);
            this.groupBox20.Location = new System.Drawing.Point(5, 3);
            this.groupBox20.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox20.Size = new System.Drawing.Size(270, 118);
            this.groupBox20.TabIndex = 181;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "Draw Matrix";
            // 
            // btnSaveMatrixToFile
            // 
            this.btnSaveMatrixToFile.Location = new System.Drawing.Point(8, 91);
            this.btnSaveMatrixToFile.Name = "btnSaveMatrixToFile";
            this.btnSaveMatrixToFile.Size = new System.Drawing.Size(110, 23);
            this.btnSaveMatrixToFile.TabIndex = 181;
            this.btnSaveMatrixToFile.Text = "Save Matrix To File";
            this.btnSaveMatrixToFile.UseVisualStyleBackColor = true;
            this.btnSaveMatrixToFile.Click += new System.EventHandler(this.btnSaveMatrixToFile_Click);
            // 
            // chxFillPointsVisibility
            // 
            this.chxFillPointsVisibility.AutoSize = true;
            this.chxFillPointsVisibility.Checked = true;
            this.chxFillPointsVisibility.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxFillPointsVisibility.Location = new System.Drawing.Point(9, 43);
            this.chxFillPointsVisibility.Name = "chxFillPointsVisibility";
            this.chxFillPointsVisibility.Size = new System.Drawing.Size(109, 17);
            this.chxFillPointsVisibility.TabIndex = 138;
            this.chxFillPointsVisibility.Text = "Fill Points Visibility";
            this.chxFillPointsVisibility.UseVisualStyleBackColor = true;
            // 
            // chxIsCreateAndDrawMatrixAutomatic
            // 
            this.chxIsCreateAndDrawMatrixAutomatic.AutoSize = true;
            this.chxIsCreateAndDrawMatrixAutomatic.Checked = true;
            this.chxIsCreateAndDrawMatrixAutomatic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxIsCreateAndDrawMatrixAutomatic.Location = new System.Drawing.Point(9, 20);
            this.chxIsCreateAndDrawMatrixAutomatic.Name = "chxIsCreateAndDrawMatrixAutomatic";
            this.chxIsCreateAndDrawMatrixAutomatic.Size = new System.Drawing.Size(199, 17);
            this.chxIsCreateAndDrawMatrixAutomatic.TabIndex = 180;
            this.chxIsCreateAndDrawMatrixAutomatic.Text = "Is Create And Draw Matrix Automatic";
            this.chxIsCreateAndDrawMatrixAutomatic.UseVisualStyleBackColor = true;
            // 
            // btnCreateMatrix
            // 
            this.btnCreateMatrix.Enabled = false;
            this.btnCreateMatrix.Location = new System.Drawing.Point(8, 66);
            this.btnCreateMatrix.Name = "btnCreateMatrix";
            this.btnCreateMatrix.Size = new System.Drawing.Size(86, 23);
            this.btnCreateMatrix.TabIndex = 135;
            this.btnCreateMatrix.Text = "Create Matrix";
            this.btnCreateMatrix.UseVisualStyleBackColor = true;
            this.btnCreateMatrix.Click += new System.EventHandler(this.btnCreateMatrixAreaOfSight_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label67);
            this.tabPage5.Controls.Add(this.label70);
            this.tabPage5.Controls.Add(this.btnAOSGetPointVisibility);
            this.tabPage5.Controls.Add(this.txtAOSPointVisibilityAnswer);
            this.tabPage5.Controls.Add(this.ctrl3DVectorAOSPointVisibility);
            this.tabPage5.Controls.Add(this.ctrlSamplePointAOSPointVisibility);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(597, 123);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Point Visibility Color";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Location = new System.Drawing.Point(6, 17);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(34, 13);
            this.label67.TabIndex = 147;
            this.label67.Text = "Point:";
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Location = new System.Drawing.Point(6, 47);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(100, 13);
            this.label70.TabIndex = 154;
            this.label70.Text = "Point Visibility Color:";
            // 
            // btnAOSGetPointVisibility
            // 
            this.btnAOSGetPointVisibility.Location = new System.Drawing.Point(274, 69);
            this.btnAOSGetPointVisibility.Name = "btnAOSGetPointVisibility";
            this.btnAOSGetPointVisibility.Size = new System.Drawing.Size(50, 23);
            this.btnAOSGetPointVisibility.TabIndex = 144;
            this.btnAOSGetPointVisibility.Text = "Get";
            this.btnAOSGetPointVisibility.UseVisualStyleBackColor = true;
            this.btnAOSGetPointVisibility.Click += new System.EventHandler(this.btnGetPointVisibilityAreaOfSight_Click);
            // 
            // txtAOSPointVisibilityAnswer
            // 
            this.txtAOSPointVisibilityAnswer.Enabled = false;
            this.txtAOSPointVisibilityAnswer.Location = new System.Drawing.Point(108, 44);
            this.txtAOSPointVisibilityAnswer.Name = "txtAOSPointVisibilityAnswer";
            this.txtAOSPointVisibilityAnswer.Size = new System.Drawing.Size(216, 20);
            this.txtAOSPointVisibilityAnswer.TabIndex = 153;
            // 
            // ctrl3DVectorAOSPointVisibility
            // 
            this.ctrl3DVectorAOSPointVisibility.IsReadOnly = false;
            this.ctrl3DVectorAOSPointVisibility.Location = new System.Drawing.Point(41, 12);
            this.ctrl3DVectorAOSPointVisibility.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorAOSPointVisibility.Name = "ctrl3DVectorAOSPointVisibility";
            this.ctrl3DVectorAOSPointVisibility.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorAOSPointVisibility.TabIndex = 145;
            this.ctrl3DVectorAOSPointVisibility.X = 0D;
            this.ctrl3DVectorAOSPointVisibility.Y = 0D;
            this.ctrl3DVectorAOSPointVisibility.Z = 0D;
            // 
            // ctrlSamplePointAOSPointVisibility
            // 
            this.ctrlSamplePointAOSPointVisibility._DgvControlName = null;
            this.ctrlSamplePointAOSPointVisibility._IsRelativeToDTM = false;
            this.ctrlSamplePointAOSPointVisibility._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointAOSPointVisibility._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointAOSPointVisibility._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointAOSPointVisibility._SampleOnePoint = true;
            this.ctrlSamplePointAOSPointVisibility._UserControlName = "ctrl3DVectorAOSPointVisibility";
            this.ctrlSamplePointAOSPointVisibility.IsAsync = false;
            this.ctrlSamplePointAOSPointVisibility.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointAOSPointVisibility.Location = new System.Drawing.Point(291, 15);
            this.ctrlSamplePointAOSPointVisibility.Name = "ctrlSamplePointAOSPointVisibility";
            this.ctrlSamplePointAOSPointVisibility.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointAOSPointVisibility.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePointAOSPointVisibility.TabIndex = 146;
            this.ctrlSamplePointAOSPointVisibility.Text = "...";
            this.ctrlSamplePointAOSPointVisibility.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnCalcCoverage);
            this.tabPage6.Controls.Add(this.label77);
            this.tabPage6.Controls.Add(this.label71);
            this.tabPage6.Controls.Add(this.label73);
            this.tabPage6.Controls.Add(this.label76);
            this.tabPage6.Controls.Add(this.cmbQualityParamsTargetTypes);
            this.tabPage6.Controls.Add(this.label75);
            this.tabPage6.Controls.Add(this.label72);
            this.tabPage6.Controls.Add(this.btnQualityParamsDrawLine);
            this.tabPage6.Controls.Add(this.label74);
            this.tabPage6.Controls.Add(this.ntxCoverageQuality);
            this.tabPage6.Controls.Add(this.ntxWalkingRadius);
            this.tabPage6.Controls.Add(this.ntxVehicleRadius);
            this.tabPage6.Controls.Add(this.ntxStandingRadius);
            this.tabPage6.Controls.Add(this.ntxCellFactor);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(597, 123);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "Coverage Quality";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // btnCalcCoverage
            // 
            this.btnCalcCoverage.Location = new System.Drawing.Point(242, 89);
            this.btnCalcCoverage.Name = "btnCalcCoverage";
            this.btnCalcCoverage.Size = new System.Drawing.Size(87, 23);
            this.btnCalcCoverage.TabIndex = 166;
            this.btnCalcCoverage.Text = "Calc Coverage";
            this.btnCalcCoverage.UseVisualStyleBackColor = true;
            this.btnCalcCoverage.Click += new System.EventHandler(this.btnCalcCoverageAreaOfSight_Click);
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.Location = new System.Drawing.Point(6, 94);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(124, 13);
            this.label77.TabIndex = 165;
            this.label77.Text = "Coverage Quality Result:";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(6, 12);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(88, 13);
            this.label71.TabIndex = 151;
            this.label71.Text = "Standing Radius:";
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(298, 14);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(81, 13);
            this.label73.TabIndex = 155;
            this.label73.Text = "Vehicle Radius:";
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(6, 64);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(126, 13);
            this.label76.TabIndex = 163;
            this.label76.Text = "Draw Line (2 points only):";
            // 
            // cmbQualityParamsTargetTypes
            // 
            this.cmbQualityParamsTargetTypes.FormattingEnabled = true;
            this.cmbQualityParamsTargetTypes.Location = new System.Drawing.Point(242, 34);
            this.cmbQualityParamsTargetTypes.Name = "cmbQualityParamsTargetTypes";
            this.cmbQualityParamsTargetTypes.Size = new System.Drawing.Size(186, 21);
            this.cmbQualityParamsTargetTypes.TabIndex = 162;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(153, 37);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(68, 13);
            this.label75.TabIndex = 161;
            this.label75.Text = "Target Type:";
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Location = new System.Drawing.Point(151, 13);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(85, 13);
            this.label72.TabIndex = 153;
            this.label72.Text = "Walking Radius:";
            // 
            // btnQualityParamsDrawLine
            // 
            this.btnQualityParamsDrawLine.Location = new System.Drawing.Point(143, 61);
            this.btnQualityParamsDrawLine.Name = "btnQualityParamsDrawLine";
            this.btnQualityParamsDrawLine.Size = new System.Drawing.Size(87, 23);
            this.btnQualityParamsDrawLine.TabIndex = 160;
            this.btnQualityParamsDrawLine.Text = "Draw";
            this.btnQualityParamsDrawLine.UseVisualStyleBackColor = true;
            this.btnQualityParamsDrawLine.Click += new System.EventHandler(this.btnQualityParamsDrawLineAreaOfSight_Click);
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(6, 37);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(60, 13);
            this.label74.TabIndex = 157;
            this.label74.Text = "Cell Factor:";
            // 
            // ntxCoverageQuality
            // 
            this.ntxCoverageQuality.Enabled = false;
            this.ntxCoverageQuality.Location = new System.Drawing.Point(143, 90);
            this.ntxCoverageQuality.Name = "ntxCoverageQuality";
            this.ntxCoverageQuality.Size = new System.Drawing.Size(88, 20);
            this.ntxCoverageQuality.TabIndex = 164;
            // 
            // ntxWalkingRadius
            // 
            this.ntxWalkingRadius.Location = new System.Drawing.Point(242, 10);
            this.ntxWalkingRadius.Name = "ntxWalkingRadius";
            this.ntxWalkingRadius.Size = new System.Drawing.Size(44, 20);
            this.ntxWalkingRadius.TabIndex = 154;
            this.ntxWalkingRadius.Text = "0";
            // 
            // ntxVehicleRadius
            // 
            this.ntxVehicleRadius.Location = new System.Drawing.Point(383, 11);
            this.ntxVehicleRadius.Name = "ntxVehicleRadius";
            this.ntxVehicleRadius.Size = new System.Drawing.Size(44, 20);
            this.ntxVehicleRadius.TabIndex = 156;
            this.ntxVehicleRadius.Text = "0";
            // 
            // ntxStandingRadius
            // 
            this.ntxStandingRadius.Location = new System.Drawing.Point(100, 9);
            this.ntxStandingRadius.Name = "ntxStandingRadius";
            this.ntxStandingRadius.Size = new System.Drawing.Size(43, 20);
            this.ntxStandingRadius.TabIndex = 152;
            this.ntxStandingRadius.Text = "0";
            // 
            // ntxCellFactor
            // 
            this.ntxCellFactor.Location = new System.Drawing.Point(98, 34);
            this.ntxCellFactor.Name = "ntxCellFactor";
            this.ntxCellFactor.Size = new System.Drawing.Size(45, 20);
            this.ntxCellFactor.TabIndex = 158;
            this.ntxCellFactor.Text = "1";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbHexadecimelNumber);
            this.tabPage1.Controls.Add(this.btnGetPointVisibilityColorsSurrounding);
            this.tabPage1.Controls.Add(this.dgvSurroundingResults);
            this.tabPage1.Controls.Add(this.label79);
            this.tabPage1.Controls.Add(this.label82);
            this.tabPage1.Controls.Add(this.label78);
            this.tabPage1.Controls.Add(this.ntxSurroundingRadiusX);
            this.tabPage1.Controls.Add(this.ntxSurroundingRadiusY);
            this.tabPage1.Controls.Add(this.ctrl3DSurroundingPoint);
            this.tabPage1.Controls.Add(this.ctrlSamplePoint4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(597, 123);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Get Point Visibility Colors Surrounding";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cbHexadecimelNumber
            // 
            this.cbHexadecimelNumber.AutoSize = true;
            this.cbHexadecimelNumber.Location = new System.Drawing.Point(8, 81);
            this.cbHexadecimelNumber.Margin = new System.Windows.Forms.Padding(2);
            this.cbHexadecimelNumber.Name = "cbHexadecimelNumber";
            this.cbHexadecimelNumber.Size = new System.Drawing.Size(130, 17);
            this.cbHexadecimelNumber.TabIndex = 194;
            this.cbHexadecimelNumber.Text = "Hexadecimel Number ";
            this.cbHexadecimelNumber.UseVisualStyleBackColor = true;
            // 
            // btnGetPointVisibilityColorsSurrounding
            // 
            this.btnGetPointVisibilityColorsSurrounding.Location = new System.Drawing.Point(32, 105);
            this.btnGetPointVisibilityColorsSurrounding.Name = "btnGetPointVisibilityColorsSurrounding";
            this.btnGetPointVisibilityColorsSurrounding.Size = new System.Drawing.Size(196, 23);
            this.btnGetPointVisibilityColorsSurrounding.TabIndex = 193;
            this.btnGetPointVisibilityColorsSurrounding.Text = "Get Point Visibility Colors Surrounding";
            this.btnGetPointVisibilityColorsSurrounding.UseVisualStyleBackColor = true;
            this.btnGetPointVisibilityColorsSurrounding.Click += new System.EventHandler(this.btnGetPointVisibilityColorsSurrounding_Click);
            // 
            // dgvSurroundingResults
            // 
            this.dgvSurroundingResults.AllowUserToAddRows = false;
            this.dgvSurroundingResults.AllowUserToDeleteRows = false;
            this.dgvSurroundingResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSurroundingResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSurroundingResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSurroundingResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSurroundingResults.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSurroundingResults.Location = new System.Drawing.Point(303, 6);
            this.dgvSurroundingResults.Name = "dgvSurroundingResults";
            this.dgvSurroundingResults.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSurroundingResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSurroundingResults.Size = new System.Drawing.Size(291, 121);
            this.dgvSurroundingResults.TabIndex = 192;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "No.";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 50;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.Location = new System.Drawing.Point(5, 59);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(113, 13);
            this.label79.TabIndex = 190;
            this.label79.Text = "Num Visibility Colors Y:";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.Location = new System.Drawing.Point(5, 37);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(113, 13);
            this.label82.TabIndex = 188;
            this.label82.Text = "Num Visibility Colors X:";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.Location = new System.Drawing.Point(4, 11);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(34, 13);
            this.label78.TabIndex = 150;
            this.label78.Text = "Point:";
            // 
            // ntxSurroundingRadiusX
            // 
            this.ntxSurroundingRadiusX.Location = new System.Drawing.Point(128, 35);
            this.ntxSurroundingRadiusX.Name = "ntxSurroundingRadiusX";
            this.ntxSurroundingRadiusX.Size = new System.Drawing.Size(58, 20);
            this.ntxSurroundingRadiusX.TabIndex = 189;
            this.ntxSurroundingRadiusX.Text = "1";
            // 
            // ntxSurroundingRadiusY
            // 
            this.ntxSurroundingRadiusY.Location = new System.Drawing.Point(128, 57);
            this.ntxSurroundingRadiusY.Name = "ntxSurroundingRadiusY";
            this.ntxSurroundingRadiusY.Size = new System.Drawing.Size(58, 20);
            this.ntxSurroundingRadiusY.TabIndex = 191;
            this.ntxSurroundingRadiusY.Text = "1";
            // 
            // ctrl3DSurroundingPoint
            // 
            this.ctrl3DSurroundingPoint.IsReadOnly = false;
            this.ctrl3DSurroundingPoint.Location = new System.Drawing.Point(33, 6);
            this.ctrl3DSurroundingPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DSurroundingPoint.Name = "ctrl3DSurroundingPoint";
            this.ctrl3DSurroundingPoint.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DSurroundingPoint.TabIndex = 148;
            this.ctrl3DSurroundingPoint.X = 0D;
            this.ctrl3DSurroundingPoint.Y = 0D;
            this.ctrl3DSurroundingPoint.Z = 0D;
            // 
            // ctrlSamplePoint4
            // 
            this.ctrlSamplePoint4._DgvControlName = null;
            this.ctrlSamplePoint4._IsRelativeToDTM = false;
            this.ctrlSamplePoint4._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePoint4._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePoint4._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePoint4._SampleOnePoint = true;
            this.ctrlSamplePoint4._UserControlName = "ctrl3DSurroundingPoint";
            this.ctrlSamplePoint4.IsAsync = false;
            this.ctrlSamplePoint4.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePoint4.Location = new System.Drawing.Point(264, 6);
            this.ctrlSamplePoint4.Name = "ctrlSamplePoint4";
            this.ctrlSamplePoint4.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePoint4.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePoint4.TabIndex = 149;
            this.ctrlSamplePoint4.Text = "...";
            this.ctrlSamplePoint4.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label36);
            this.tabPage2.Controls.Add(this.cmbEScoutersSumTypeMatrices);
            this.tabPage2.Controls.Add(this.cbCloneMatrixFillPoints);
            this.tabPage2.Controls.Add(this.cbAreSameRect);
            this.tabPage2.Controls.Add(this.btnAreSameRectAreaOfSightMatrices);
            this.tabPage2.Controls.Add(this.btnSumAreaOfSightMatrices);
            this.tabPage2.Controls.Add(this.btnCloneAreaOfSightMatrix);
            this.tabPage2.Controls.Add(this.label34);
            this.tabPage2.Controls.Add(this.cbLstAreaOfSightMatrix);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(597, 123);
            this.tabPage2.TabIndex = 4;
            this.tabPage2.Text = "Matrix Operations";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(248, 59);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(103, 20);
            this.label36.TabIndex = 193;
            this.label36.Text = "Scouters Sum Type:";
            // 
            // cmbEScoutersSumTypeMatrices
            // 
            this.cmbEScoutersSumTypeMatrices.FormattingEnabled = true;
            this.cmbEScoutersSumTypeMatrices.Location = new System.Drawing.Point(356, 57);
            this.cmbEScoutersSumTypeMatrices.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEScoutersSumTypeMatrices.Name = "cmbEScoutersSumTypeMatrices";
            this.cmbEScoutersSumTypeMatrices.Size = new System.Drawing.Size(86, 21);
            this.cmbEScoutersSumTypeMatrices.TabIndex = 192;
            // 
            // cbCloneMatrixFillPoints
            // 
            this.cbCloneMatrixFillPoints.AutoSize = true;
            this.cbCloneMatrixFillPoints.Checked = true;
            this.cbCloneMatrixFillPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCloneMatrixFillPoints.Location = new System.Drawing.Point(253, 28);
            this.cbCloneMatrixFillPoints.Name = "cbCloneMatrixFillPoints";
            this.cbCloneMatrixFillPoints.Size = new System.Drawing.Size(109, 17);
            this.cbCloneMatrixFillPoints.TabIndex = 185;
            this.cbCloneMatrixFillPoints.Text = "Fill Points Visibility";
            this.cbCloneMatrixFillPoints.UseVisualStyleBackColor = true;
            // 
            // cbAreSameRect
            // 
            this.cbAreSameRect.AutoSize = true;
            this.cbAreSameRect.Enabled = false;
            this.cbAreSameRect.Location = new System.Drawing.Point(464, 96);
            this.cbAreSameRect.Name = "cbAreSameRect";
            this.cbAreSameRect.Size = new System.Drawing.Size(98, 17);
            this.cbAreSameRect.TabIndex = 184;
            this.cbAreSameRect.Text = "Are Same Rect";
            this.cbAreSameRect.UseVisualStyleBackColor = true;
            // 
            // btnAreSameRectAreaOfSightMatrices
            // 
            this.btnAreSameRectAreaOfSightMatrices.Location = new System.Drawing.Point(253, 92);
            this.btnAreSameRectAreaOfSightMatrices.Name = "btnAreSameRectAreaOfSightMatrices";
            this.btnAreSameRectAreaOfSightMatrices.Size = new System.Drawing.Size(195, 24);
            this.btnAreSameRectAreaOfSightMatrices.TabIndex = 183;
            this.btnAreSameRectAreaOfSightMatrices.Text = "Are Same Rect Area Of Sight Matrices";
            this.btnAreSameRectAreaOfSightMatrices.UseVisualStyleBackColor = true;
            this.btnAreSameRectAreaOfSightMatrices.Click += new System.EventHandler(this.btnAreSameRectAreaOfSightMatrices_Click);
            // 
            // btnSumAreaOfSightMatrices
            // 
            this.btnSumAreaOfSightMatrices.Location = new System.Drawing.Point(446, 54);
            this.btnSumAreaOfSightMatrices.Name = "btnSumAreaOfSightMatrices";
            this.btnSumAreaOfSightMatrices.Size = new System.Drawing.Size(147, 24);
            this.btnSumAreaOfSightMatrices.TabIndex = 182;
            this.btnSumAreaOfSightMatrices.Text = "Sum Area Of Sight Matrices";
            this.btnSumAreaOfSightMatrices.UseVisualStyleBackColor = true;
            this.btnSumAreaOfSightMatrices.Click += new System.EventHandler(this.btnSumAreaOfSightMatrices_Click);
            // 
            // btnCloneAreaOfSightMatrix
            // 
            this.btnCloneAreaOfSightMatrix.Location = new System.Drawing.Point(446, 24);
            this.btnCloneAreaOfSightMatrix.Name = "btnCloneAreaOfSightMatrix";
            this.btnCloneAreaOfSightMatrix.Size = new System.Drawing.Size(147, 24);
            this.btnCloneAreaOfSightMatrix.TabIndex = 181;
            this.btnCloneAreaOfSightMatrix.Text = "Clone Area Of Sight Matrix";
            this.btnCloneAreaOfSightMatrix.UseVisualStyleBackColor = true;
            this.btnCloneAreaOfSightMatrix.Click += new System.EventHandler(this.btnCloneAreaOfSightMatrix_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(5, 6);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(123, 13);
            this.label34.TabIndex = 137;
            this.label34.Text = "Area Of Sight Matrix List:";
            // 
            // cbLstAreaOfSightMatrix
            // 
            this.cbLstAreaOfSightMatrix.FormattingEnabled = true;
            this.cbLstAreaOfSightMatrix.Location = new System.Drawing.Point(3, 22);
            this.cbLstAreaOfSightMatrix.Margin = new System.Windows.Forms.Padding(2);
            this.cbLstAreaOfSightMatrix.Name = "cbLstAreaOfSightMatrix";
            this.cbLstAreaOfSightMatrix.Size = new System.Drawing.Size(246, 64);
            this.cbLstAreaOfSightMatrix.TabIndex = 0;
            // 
            // grpShowOperations
            // 
            this.grpShowOperations.Controls.Add(this.chxDrawSeenStaticObjects);
            this.grpShowOperations.Controls.Add(this.chxDrawObject);
            this.grpShowOperations.Controls.Add(this.chxDrawAreaOfSight);
            this.grpShowOperations.Controls.Add(this.chxDrawUnseenPolygons);
            this.grpShowOperations.Controls.Add(this.chxDrawSeenPolygons);
            this.grpShowOperations.Controls.Add(this.chxDrawLineOfSight);
            this.grpShowOperations.Location = new System.Drawing.Point(8, 408);
            this.grpShowOperations.Name = "grpShowOperations";
            this.grpShowOperations.Size = new System.Drawing.Size(129, 141);
            this.grpShowOperations.TabIndex = 179;
            this.grpShowOperations.TabStop = false;
            this.grpShowOperations.Text = "Show Options";
            // 
            // chxDrawSeenStaticObjects
            // 
            this.chxDrawSeenStaticObjects.AutoSize = true;
            this.chxDrawSeenStaticObjects.Enabled = false;
            this.chxDrawSeenStaticObjects.Location = new System.Drawing.Point(6, 121);
            this.chxDrawSeenStaticObjects.Name = "chxDrawSeenStaticObjects";
            this.chxDrawSeenStaticObjects.Size = new System.Drawing.Size(120, 17);
            this.chxDrawSeenStaticObjects.TabIndex = 182;
            this.chxDrawSeenStaticObjects.Text = "Seen Static Objects";
            this.chxDrawSeenStaticObjects.UseVisualStyleBackColor = true;
            this.chxDrawSeenStaticObjects.CheckedChanged += new System.EventHandler(this.chxSeenStaticObjects_CheckedChanged);
            // 
            // chxDrawObject
            // 
            this.chxDrawObject.AutoSize = true;
            this.chxDrawObject.Enabled = false;
            this.chxDrawObject.Location = new System.Drawing.Point(6, 16);
            this.chxDrawObject.Name = "chxDrawObject";
            this.chxDrawObject.Size = new System.Drawing.Size(85, 17);
            this.chxDrawObject.TabIndex = 181;
            this.chxDrawObject.Text = "Draw Object";
            this.chxDrawObject.UseVisualStyleBackColor = true;
            this.chxDrawObject.CheckedChanged += new System.EventHandler(this.chxDrawObject_CheckedChanged);
            // 
            // chxDrawAreaOfSight
            // 
            this.chxDrawAreaOfSight.AutoSize = true;
            this.chxDrawAreaOfSight.Enabled = false;
            this.chxDrawAreaOfSight.Location = new System.Drawing.Point(6, 57);
            this.chxDrawAreaOfSight.Name = "chxDrawAreaOfSight";
            this.chxDrawAreaOfSight.Size = new System.Drawing.Size(89, 17);
            this.chxDrawAreaOfSight.TabIndex = 180;
            this.chxDrawAreaOfSight.Text = "Area Of Sight";
            this.chxDrawAreaOfSight.UseVisualStyleBackColor = true;
            this.chxDrawAreaOfSight.CheckedChanged += new System.EventHandler(this.chxDrawAreaOfSight_CheckedChanged);
            // 
            // chxDrawUnseenPolygons
            // 
            this.chxDrawUnseenPolygons.AutoSize = true;
            this.chxDrawUnseenPolygons.Enabled = false;
            this.chxDrawUnseenPolygons.Location = new System.Drawing.Point(6, 101);
            this.chxDrawUnseenPolygons.Name = "chxDrawUnseenPolygons";
            this.chxDrawUnseenPolygons.Size = new System.Drawing.Size(109, 17);
            this.chxDrawUnseenPolygons.TabIndex = 179;
            this.chxDrawUnseenPolygons.Text = "Unseen Polygons";
            this.chxDrawUnseenPolygons.UseVisualStyleBackColor = true;
            this.chxDrawUnseenPolygons.CheckedChanged += new System.EventHandler(this.chxDrawUnseenPolygons_CheckedChanged);
            // 
            // chxDrawSeenPolygons
            // 
            this.chxDrawSeenPolygons.AutoSize = true;
            this.chxDrawSeenPolygons.Enabled = false;
            this.chxDrawSeenPolygons.Location = new System.Drawing.Point(6, 79);
            this.chxDrawSeenPolygons.Name = "chxDrawSeenPolygons";
            this.chxDrawSeenPolygons.Size = new System.Drawing.Size(97, 17);
            this.chxDrawSeenPolygons.TabIndex = 178;
            this.chxDrawSeenPolygons.Text = "Seen Polygons";
            this.chxDrawSeenPolygons.UseVisualStyleBackColor = true;
            this.chxDrawSeenPolygons.CheckedChanged += new System.EventHandler(this.chxDrawSeenPolygons_CheckedChanged);
            // 
            // chxDrawLineOfSight
            // 
            this.chxDrawLineOfSight.AutoSize = true;
            this.chxDrawLineOfSight.Enabled = false;
            this.chxDrawLineOfSight.Location = new System.Drawing.Point(6, 36);
            this.chxDrawLineOfSight.Name = "chxDrawLineOfSight";
            this.chxDrawLineOfSight.Size = new System.Drawing.Size(87, 17);
            this.chxDrawLineOfSight.TabIndex = 176;
            this.chxDrawLineOfSight.Text = "Line Of Sight";
            this.chxDrawLineOfSight.UseVisualStyleBackColor = true;
            this.chxDrawLineOfSight.CheckedChanged += new System.EventHandler(this.chxDrawLineOfSight_CheckedChanged);
            // 
            // tpRasterAndTraversability
            // 
            this.tpRasterAndTraversability.Controls.Add(this.groupBox3);
            this.tpRasterAndTraversability.Controls.Add(this.groupBox19);
            this.tpRasterAndTraversability.Location = new System.Drawing.Point(4, 22);
            this.tpRasterAndTraversability.Margin = new System.Windows.Forms.Padding(2);
            this.tpRasterAndTraversability.Name = "tpRasterAndTraversability";
            this.tpRasterAndTraversability.Padding = new System.Windows.Forms.Padding(2);
            this.tpRasterAndTraversability.Size = new System.Drawing.Size(935, 600);
            this.tpRasterAndTraversability.TabIndex = 5;
            this.tpRasterAndTraversability.Text = "Raster And Traversability";
            this.tpRasterAndTraversability.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ctrlTraversabilityAlongLinePoints);
            this.groupBox3.Controls.Add(this.btnTALUpdateLine);
            this.groupBox3.Controls.Add(this.label38);
            this.groupBox3.Controls.Add(this.lbTraversabilityMapLayers);
            this.groupBox3.Controls.Add(this.btnGetTraversabilityAlongLine);
            this.groupBox3.Controls.Add(this.cbGetTraversabilityAlongLineAsync);
            this.groupBox3.Controls.Add(this.btnDrawLineTraversability);
            this.groupBox3.Location = new System.Drawing.Point(364, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(461, 391);
            this.groupBox3.TabIndex = 81;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Traversability Along Line";
            // 
            // ctrlTraversabilityAlongLinePoints
            // 
            this.ctrlTraversabilityAlongLinePoints.Location = new System.Drawing.Point(8, 135);
            this.ctrlTraversabilityAlongLinePoints.Name = "ctrlTraversabilityAlongLinePoints";
            this.ctrlTraversabilityAlongLinePoints.Size = new System.Drawing.Size(345, 196);
            this.ctrlTraversabilityAlongLinePoints.TabIndex = 145;
            // 
            // btnTALUpdateLine
            // 
            this.btnTALUpdateLine.Location = new System.Drawing.Point(364, 246);
            this.btnTALUpdateLine.Name = "btnTALUpdateLine";
            this.btnTALUpdateLine.Size = new System.Drawing.Size(91, 23);
            this.btnTALUpdateLine.TabIndex = 144;
            this.btnTALUpdateLine.Text = "Update Line";
            this.btnTALUpdateLine.UseVisualStyleBackColor = true;
            this.btnTALUpdateLine.Click += new System.EventHandler(this.btnTALUpdateLine_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(11, 15);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(155, 13);
            this.label38.TabIndex = 116;
            this.label38.Text = "Select Traversability Map Layer";
            // 
            // lbTraversabilityMapLayers
            // 
            this.lbTraversabilityMapLayers.FormattingEnabled = true;
            this.lbTraversabilityMapLayers.Location = new System.Drawing.Point(11, 35);
            this.lbTraversabilityMapLayers.Margin = new System.Windows.Forms.Padding(2);
            this.lbTraversabilityMapLayers.Name = "lbTraversabilityMapLayers";
            this.lbTraversabilityMapLayers.Size = new System.Drawing.Size(221, 95);
            this.lbTraversabilityMapLayers.TabIndex = 115;
            // 
            // btnGetTraversabilityAlongLine
            // 
            this.btnGetTraversabilityAlongLine.Location = new System.Drawing.Point(300, 354);
            this.btnGetTraversabilityAlongLine.Name = "btnGetTraversabilityAlongLine";
            this.btnGetTraversabilityAlongLine.Size = new System.Drawing.Size(155, 23);
            this.btnGetTraversabilityAlongLine.TabIndex = 86;
            this.btnGetTraversabilityAlongLine.Text = "Get Traversability Along Line";
            this.btnGetTraversabilityAlongLine.UseVisualStyleBackColor = true;
            this.btnGetTraversabilityAlongLine.Click += new System.EventHandler(this.btnGetTraversabilityAlongLine_Click);
            // 
            // cbGetTraversabilityAlongLineAsync
            // 
            this.cbGetTraversabilityAlongLineAsync.AutoSize = true;
            this.cbGetTraversabilityAlongLineAsync.Location = new System.Drawing.Point(400, 332);
            this.cbGetTraversabilityAlongLineAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbGetTraversabilityAlongLineAsync.Name = "cbGetTraversabilityAlongLineAsync";
            this.cbGetTraversabilityAlongLineAsync.Size = new System.Drawing.Size(55, 17);
            this.cbGetTraversabilityAlongLineAsync.TabIndex = 85;
            this.cbGetTraversabilityAlongLineAsync.Text = "Async";
            this.cbGetTraversabilityAlongLineAsync.UseVisualStyleBackColor = true;
            // 
            // btnDrawLineTraversability
            // 
            this.btnDrawLineTraversability.Location = new System.Drawing.Point(364, 275);
            this.btnDrawLineTraversability.Name = "btnDrawLineTraversability";
            this.btnDrawLineTraversability.Size = new System.Drawing.Size(91, 23);
            this.btnDrawLineTraversability.TabIndex = 78;
            this.btnDrawLineTraversability.Text = "Draw Line ";
            this.btnDrawLineTraversability.UseVisualStyleBackColor = true;
            this.btnDrawLineTraversability.Click += new System.EventHandler(this.btnDrawLineTraversability_Click);
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.btnGetTraversabilityFromColorCode);
            this.groupBox19.Controls.Add(this.gbGetTraversabilityFromColorCode);
            this.groupBox19.Controls.Add(this.label130);
            this.groupBox19.Controls.Add(this.label129);
            this.groupBox19.Controls.Add(this.pbGetRasterColor);
            this.groupBox19.Controls.Add(this.ctrlLocationGetRaster);
            this.groupBox19.Controls.Add(this.ntbGetRasterColorR);
            this.groupBox19.Controls.Add(this.ntbGetRasterColorB);
            this.groupBox19.Controls.Add(this.label128);
            this.groupBox19.Controls.Add(this.ntbGetRasterColorA);
            this.groupBox19.Controls.Add(this.label127);
            this.groupBox19.Controls.Add(this.ntbGetRasterColorG);
            this.groupBox19.Controls.Add(this.label126);
            this.groupBox19.Controls.Add(this.label81);
            this.groupBox19.Controls.Add(this.cbNearestPixel);
            this.groupBox19.Controls.Add(this.btnGetRasterLayerColorByPoint);
            this.groupBox19.Controls.Add(this.cbGetRasterAsync);
            this.groupBox19.Controls.Add(this.lbRasterMapLayer);
            this.groupBox19.Controls.Add(this.ntbLOD);
            this.groupBox19.Controls.Add(this.label80);
            this.groupBox19.Controls.Add(this.ctrl3DGetRasterPoint);
            this.groupBox19.Controls.Add(this.label50);
            this.groupBox19.Location = new System.Drawing.Point(4, 5);
            this.groupBox19.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox19.Size = new System.Drawing.Size(354, 522);
            this.groupBox19.TabIndex = 0;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Get Raster Layer Color By Point";
            // 
            // btnGetTraversabilityFromColorCode
            // 
            this.btnGetTraversabilityFromColorCode.Location = new System.Drawing.Point(94, 227);
            this.btnGetTraversabilityFromColorCode.Name = "btnGetTraversabilityFromColorCode";
            this.btnGetTraversabilityFromColorCode.Size = new System.Drawing.Size(178, 23);
            this.btnGetTraversabilityFromColorCode.TabIndex = 133;
            this.btnGetTraversabilityFromColorCode.Text = "Get Traversability From Color Code";
            this.btnGetTraversabilityFromColorCode.UseVisualStyleBackColor = true;
            this.btnGetTraversabilityFromColorCode.Click += new System.EventHandler(this.btnGetTraversabilityFromColorCode_Click);
            // 
            // gbGetTraversabilityFromColorCode
            // 
            this.gbGetTraversabilityFromColorCode.Controls.Add(this.dgvGetTraversabilityFromColorCode);
            this.gbGetTraversabilityFromColorCode.Location = new System.Drawing.Point(0, 255);
            this.gbGetTraversabilityFromColorCode.Margin = new System.Windows.Forms.Padding(2);
            this.gbGetTraversabilityFromColorCode.Name = "gbGetTraversabilityFromColorCode";
            this.gbGetTraversabilityFromColorCode.Padding = new System.Windows.Forms.Padding(2);
            this.gbGetTraversabilityFromColorCode.Size = new System.Drawing.Size(330, 260);
            this.gbGetTraversabilityFromColorCode.TabIndex = 1;
            this.gbGetTraversabilityFromColorCode.TabStop = false;
            this.gbGetTraversabilityFromColorCode.Text = "Get Traversability From Color Code";
            // 
            // dgvGetTraversabilityFromColorCode
            // 
            this.dgvGetTraversabilityFromColorCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGetTraversabilityFromColorCode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DirectionAngle,
            this.Column2});
            this.dgvGetTraversabilityFromColorCode.Location = new System.Drawing.Point(4, 20);
            this.dgvGetTraversabilityFromColorCode.Margin = new System.Windows.Forms.Padding(2);
            this.dgvGetTraversabilityFromColorCode.Name = "dgvGetTraversabilityFromColorCode";
            this.dgvGetTraversabilityFromColorCode.RowTemplate.Height = 24;
            this.dgvGetTraversabilityFromColorCode.Size = new System.Drawing.Size(315, 236);
            this.dgvGetTraversabilityFromColorCode.TabIndex = 134;
            // 
            // DirectionAngle
            // 
            this.DirectionAngle.HeaderText = "Direction Angle";
            this.DirectionAngle.Name = "DirectionAngle";
            this.DirectionAngle.ReadOnly = true;
            this.DirectionAngle.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Traversable";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Location = new System.Drawing.Point(296, 201);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(17, 13);
            this.label130.TabIndex = 132;
            this.label130.Text = "A:";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(233, 201);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(17, 13);
            this.label129.TabIndex = 131;
            this.label129.Text = "B:";
            // 
            // pbGetRasterColor
            // 
            this.pbGetRasterColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGetRasterColor.Location = new System.Drawing.Point(72, 198);
            this.pbGetRasterColor.Margin = new System.Windows.Forms.Padding(2);
            this.pbGetRasterColor.Name = "pbGetRasterColor";
            this.pbGetRasterColor.Size = new System.Drawing.Size(22, 23);
            this.pbGetRasterColor.TabIndex = 130;
            this.pbGetRasterColor.TabStop = false;
            // 
            // ctrlLocationGetRaster
            // 
            this.ctrlLocationGetRaster._DgvControlName = null;
            this.ctrlLocationGetRaster._IsRelativeToDTM = false;
            this.ctrlLocationGetRaster._PointInOverlayManagerCoordSys = true;
            this.ctrlLocationGetRaster._PointZValue = 1.7976931348623157E+308D;
            this.ctrlLocationGetRaster._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlLocationGetRaster._SampleOnePoint = true;
            this.ctrlLocationGetRaster._UserControlName = "ctrl3DGetRasterPoint";
            this.ctrlLocationGetRaster.IsAsync = false;
            this.ctrlLocationGetRaster.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlLocationGetRaster.Location = new System.Drawing.Point(286, 114);
            this.ctrlLocationGetRaster.Name = "ctrlLocationGetRaster";
            this.ctrlLocationGetRaster.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlLocationGetRaster.Size = new System.Drawing.Size(33, 23);
            this.ctrlLocationGetRaster.TabIndex = 129;
            this.ctrlLocationGetRaster.Text = "...";
            this.ctrlLocationGetRaster.UseVisualStyleBackColor = true;
            // 
            // ntbGetRasterColorR
            // 
            this.ntbGetRasterColorR.Location = new System.Drawing.Point(122, 198);
            this.ntbGetRasterColorR.Name = "ntbGetRasterColorR";
            this.ntbGetRasterColorR.Size = new System.Drawing.Size(32, 20);
            this.ntbGetRasterColorR.TabIndex = 128;
            // 
            // ntbGetRasterColorB
            // 
            this.ntbGetRasterColorB.Location = new System.Drawing.Point(251, 198);
            this.ntbGetRasterColorB.Name = "ntbGetRasterColorB";
            this.ntbGetRasterColorB.Size = new System.Drawing.Size(32, 20);
            this.ntbGetRasterColorB.TabIndex = 126;
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(99, 201);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(18, 13);
            this.label128.TabIndex = 125;
            this.label128.Text = "R:";
            // 
            // ntbGetRasterColorA
            // 
            this.ntbGetRasterColorA.Location = new System.Drawing.Point(315, 198);
            this.ntbGetRasterColorA.Name = "ntbGetRasterColorA";
            this.ntbGetRasterColorA.Size = new System.Drawing.Size(34, 20);
            this.ntbGetRasterColorA.TabIndex = 124;
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Location = new System.Drawing.Point(164, 201);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(18, 13);
            this.label127.TabIndex = 123;
            this.label127.Text = "G:";
            // 
            // ntbGetRasterColorG
            // 
            this.ntbGetRasterColorG.Location = new System.Drawing.Point(187, 198);
            this.ntbGetRasterColorG.Name = "ntbGetRasterColorG";
            this.ntbGetRasterColorG.Size = new System.Drawing.Size(36, 20);
            this.ntbGetRasterColorG.TabIndex = 122;
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(3, 201);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(68, 13);
            this.label126.TabIndex = 121;
            this.label126.Text = "Raster Color:";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.Location = new System.Drawing.Point(5, 18);
            this.label81.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(124, 13);
            this.label81.TabIndex = 120;
            this.label81.Text = "Select Raster Map Layer";
            // 
            // cbNearestPixel
            // 
            this.cbNearestPixel.AutoSize = true;
            this.cbNearestPixel.Location = new System.Drawing.Point(148, 143);
            this.cbNearestPixel.Margin = new System.Windows.Forms.Padding(2);
            this.cbNearestPixel.Name = "cbNearestPixel";
            this.cbNearestPixel.Size = new System.Drawing.Size(88, 17);
            this.cbNearestPixel.TabIndex = 119;
            this.cbNearestPixel.Text = "Nearest Pixel";
            this.cbNearestPixel.UseVisualStyleBackColor = true;
            // 
            // btnGetRasterLayerColorByPoint
            // 
            this.btnGetRasterLayerColorByPoint.Location = new System.Drawing.Point(94, 169);
            this.btnGetRasterLayerColorByPoint.Name = "btnGetRasterLayerColorByPoint";
            this.btnGetRasterLayerColorByPoint.Size = new System.Drawing.Size(182, 23);
            this.btnGetRasterLayerColorByPoint.TabIndex = 118;
            this.btnGetRasterLayerColorByPoint.Text = "Get Raster Layer Color By Point";
            this.btnGetRasterLayerColorByPoint.UseVisualStyleBackColor = true;
            this.btnGetRasterLayerColorByPoint.Click += new System.EventHandler(this.btnGetRasterLayerColorByPoint_Click);
            // 
            // cbGetRasterAsync
            // 
            this.cbGetRasterAsync.AutoSize = true;
            this.cbGetRasterAsync.Location = new System.Drawing.Point(247, 143);
            this.cbGetRasterAsync.Margin = new System.Windows.Forms.Padding(2);
            this.cbGetRasterAsync.Name = "cbGetRasterAsync";
            this.cbGetRasterAsync.Size = new System.Drawing.Size(55, 17);
            this.cbGetRasterAsync.TabIndex = 117;
            this.cbGetRasterAsync.Text = "Async";
            this.cbGetRasterAsync.UseVisualStyleBackColor = true;
            // 
            // lbRasterMapLayer
            // 
            this.lbRasterMapLayer.FormattingEnabled = true;
            this.lbRasterMapLayer.Location = new System.Drawing.Point(7, 39);
            this.lbRasterMapLayer.Margin = new System.Windows.Forms.Padding(2);
            this.lbRasterMapLayer.Name = "lbRasterMapLayer";
            this.lbRasterMapLayer.Size = new System.Drawing.Size(221, 69);
            this.lbRasterMapLayer.TabIndex = 116;
            // 
            // ntbLOD
            // 
            this.ntbLOD.Location = new System.Drawing.Point(72, 144);
            this.ntbLOD.Name = "ntbLOD";
            this.ntbLOD.Size = new System.Drawing.Size(57, 20);
            this.ntbLOD.TabIndex = 89;
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.Location = new System.Drawing.Point(4, 146);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(32, 13);
            this.label80.TabIndex = 88;
            this.label80.Text = "LOD:";
            // 
            // ctrl3DGetRasterPoint
            // 
            this.ctrl3DGetRasterPoint.IsReadOnly = false;
            this.ctrl3DGetRasterPoint.Location = new System.Drawing.Point(52, 114);
            this.ctrl3DGetRasterPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DGetRasterPoint.Name = "ctrl3DGetRasterPoint";
            this.ctrl3DGetRasterPoint.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DGetRasterPoint.TabIndex = 77;
            this.ctrl3DGetRasterPoint.X = 0D;
            this.ctrl3DGetRasterPoint.Y = 0D;
            this.ctrl3DGetRasterPoint.Z = 0D;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(4, 119);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(34, 13);
            this.label50.TabIndex = 76;
            this.label50.Text = "Point:";
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox11.Controls.Add(this.dgvAsyncQueryResults);
            this.groupBox11.Location = new System.Drawing.Point(3, 625);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox11.Size = new System.Drawing.Size(874, 140);
            this.groupBox11.TabIndex = 98;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Query Results";
            // 
            // dgvAsyncQueryResults
            // 
            this.dgvAsyncQueryResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvAsyncQueryResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAsyncQueryResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.NameFunction,
            this.Async,
            this.ErrorCode,
            this.Column1});
            this.dgvAsyncQueryResults.Location = new System.Drawing.Point(2, 17);
            this.dgvAsyncQueryResults.Margin = new System.Windows.Forms.Padding(2);
            this.dgvAsyncQueryResults.Name = "dgvAsyncQueryResults";
            this.dgvAsyncQueryResults.RowTemplate.Height = 24;
            this.dgvAsyncQueryResults.Size = new System.Drawing.Size(867, 119);
            this.dgvAsyncQueryResults.TabIndex = 0;
            this.dgvAsyncQueryResults.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAsyncQueryResults_CellClick);
            // 
            // Number
            // 
            this.Number.HeaderText = "No.";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            this.Number.Width = 50;
            // 
            // NameFunction
            // 
            this.NameFunction.HeaderText = "Function Name";
            this.NameFunction.Name = "NameFunction";
            this.NameFunction.ReadOnly = true;
            this.NameFunction.Width = 200;
            // 
            // Async
            // 
            this.Async.HeaderText = "Is Async";
            this.Async.Name = "Async";
            this.Async.ReadOnly = true;
            this.Async.Width = 70;
            // 
            // ErrorCode
            // 
            this.ErrorCode.HeaderText = "Error Message (if return)";
            this.ErrorCode.Name = "ErrorCode";
            this.ErrorCode.ReadOnly = true;
            this.ErrorCode.Width = 450;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Area Of Sight Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 170;
            // 
            // label42
            // 
            this.label42.Location = new System.Drawing.Point(463, 164);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(66, 34);
            this.label42.TabIndex = 183;
            this.label42.Text = "Seen Static Objects:";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(566, 169);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(37, 13);
            this.label43.TabIndex = 181;
            this.label43.Text = "Alpha:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(6, 272);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(157, 13);
            this.label53.TabIndex = 143;
            this.label53.Text = "Get Area Of Sight (GPU-based):";
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(6, 206);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(359, 2);
            this.groupBox6.TabIndex = 142;
            this.groupBox6.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 23);
            this.button2.TabIndex = 141;
            this.button2.Text = "Get";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Location = new System.Drawing.Point(6, 182);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(93, 13);
            this.label54.TabIndex = 140;
            this.label54.Text = "Get Area Of Sight:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.listBox1);
            this.groupBox7.Controls.Add(this.button3);
            this.groupBox7.Controls.Add(this.button4);
            this.groupBox7.Location = new System.Drawing.Point(689, 384);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(354, 243);
            this.groupBox7.TabIndex = 138;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Areo Of Sight GPU-based";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(330, 69);
            this.listBox1.TabIndex = 116;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 161);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 23);
            this.button3.TabIndex = 137;
            this.button3.Text = "Remove Matrix";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(211, 210);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 23);
            this.button4.TabIndex = 136;
            this.button4.Text = "Get AOS (GPU-based)";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Location = new System.Drawing.Point(6, 248);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(94, 13);
            this.label55.TabIndex = 138;
            this.label55.Text = "Target Resolution:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label56);
            this.groupBox8.Controls.Add(this.checkBox1);
            this.groupBox8.Controls.Add(this.label57);
            this.groupBox8.Controls.Add(this.numericTextBox3);
            this.groupBox8.Controls.Add(this.ctrlSamplePoint1);
            this.groupBox8.Controls.Add(this.label58);
            this.groupBox8.Controls.Add(this.ctrl3DVector1);
            this.groupBox8.Controls.Add(this.numericTextBox4);
            this.groupBox8.Controls.Add(this.checkBox2);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox8.Location = new System.Drawing.Point(3, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(1058, 154);
            this.groupBox8.TabIndex = 129;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "General Params";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(6, 22);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(47, 13);
            this.label56.TabIndex = 100;
            this.label56.Text = "Scouter:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 46);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(152, 17);
            this.checkBox1.TabIndex = 98;
            this.checkBox1.Text = "Is Scouter Height Absolute";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Location = new System.Drawing.Point(3, 98);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(87, 13);
            this.label57.TabIndex = 108;
            this.label57.Text = "Max Pitch Angle:";
            // 
            // numericTextBox3
            // 
            this.numericTextBox3.Location = new System.Drawing.Point(96, 95);
            this.numericTextBox3.Name = "numericTextBox3";
            this.numericTextBox3.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox3.TabIndex = 109;
            this.numericTextBox3.Text = "90";
            // 
            // ctrlSamplePoint1
            // 
            this.ctrlSamplePoint1._DgvControlName = null;
            this.ctrlSamplePoint1._IsRelativeToDTM = false;
            this.ctrlSamplePoint1._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePoint1._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePoint1._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePoint1._SampleOnePoint = true;
            this.ctrlSamplePoint1._UserControlName = "ctrl3DVectorAOSScouter";
            this.ctrlSamplePoint1.IsAsync = false;
            this.ctrlSamplePoint1.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePoint1.Location = new System.Drawing.Point(297, 17);
            this.ctrlSamplePoint1.Name = "ctrlSamplePoint1";
            this.ctrlSamplePoint1.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePoint1.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePoint1.TabIndex = 96;
            this.ctrlSamplePoint1.Text = "...";
            this.ctrlSamplePoint1.UseVisualStyleBackColor = true;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Location = new System.Drawing.Point(3, 124);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(84, 13);
            this.label58.TabIndex = 110;
            this.label58.Text = "Min Pitch Angle:";
            // 
            // ctrl3DVector1
            // 
            this.ctrl3DVector1.IsReadOnly = false;
            this.ctrl3DVector1.Location = new System.Drawing.Point(59, 14);
            this.ctrl3DVector1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVector1.Name = "ctrl3DVector1";
            this.ctrl3DVector1.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVector1.TabIndex = 95;
            this.ctrl3DVector1.X = 0D;
            this.ctrl3DVector1.Y = 0D;
            this.ctrl3DVector1.Z = 0D;
            // 
            // numericTextBox4
            // 
            this.numericTextBox4.Location = new System.Drawing.Point(96, 121);
            this.numericTextBox4.Name = "numericTextBox4";
            this.numericTextBox4.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox4.TabIndex = 111;
            this.numericTextBox4.Text = "-90";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(6, 69);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(151, 17);
            this.checkBox2.TabIndex = 102;
            this.checkBox2.Text = "Is Targets Height Absolute";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(6, 222);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(75, 13);
            this.label59.TabIndex = 136;
            this.label59.Text = "Target Height:";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(9, 463);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(53, 13);
            this.label60.TabIndex = 117;
            this.label60.Text = "Radius X:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(230, 306);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(103, 23);
            this.button5.TabIndex = 135;
            this.button5.Text = "Create Matrix";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(9, 616);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 23);
            this.button6.TabIndex = 134;
            this.button6.Text = "Draw Ellipse";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(9, 489);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(53, 13);
            this.label61.TabIndex = 119;
            this.label61.Text = "Radius Y:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(524, 291);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(87, 23);
            this.button7.TabIndex = 133;
            this.button7.Text = "Draw Polygon";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(278, 616);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 132;
            this.button8.Text = "Get";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(9, 515);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(62, 13);
            this.label62.TabIndex = 121;
            this.label62.Text = "Start Angle:";
            // 
            // groupBox9
            // 
            this.groupBox9.Location = new System.Drawing.Point(0, 413);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(359, 2);
            this.groupBox9.TabIndex = 131;
            this.groupBox9.TabStop = false;
            // 
            // groupBox10
            // 
            this.groupBox10.Location = new System.Drawing.Point(6, 163);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(359, 2);
            this.groupBox10.TabIndex = 130;
            this.groupBox10.TabStop = false;
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Location = new System.Drawing.Point(9, 541);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(59, 13);
            this.label63.TabIndex = 123;
            this.label63.Text = "End Angle:";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(300, 177);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(33, 23);
            this.button9.TabIndex = 112;
            this.button9.Text = "Get";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridView1.Location = new System.Drawing.Point(485, 180);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(347, 105);
            this.dataGridView1.TabIndex = 113;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "X";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Y";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Z";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Location = new System.Drawing.Point(9, 593);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(166, 13);
            this.label64.TabIndex = 127;
            this.label64.Text = "Number Of Rays In Complete Arc:";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Location = new System.Drawing.Point(9, 567);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(104, 13);
            this.label65.TabIndex = 125;
            this.label65.Text = "Yaw Rotation Angle:";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Location = new System.Drawing.Point(9, 436);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(75, 13);
            this.label66.TabIndex = 115;
            this.label66.Text = "Target Height:";
            // 
            // chxDrawSeenStaticObjects2
            // 
            this.chxDrawSeenStaticObjects2.Location = new System.Drawing.Point(0, 0);
            this.chxDrawSeenStaticObjects2.Name = "chxDrawSeenStaticObjects2";
            this.chxDrawSeenStaticObjects2.Size = new System.Drawing.Size(104, 24);
            this.chxDrawSeenStaticObjects2.TabIndex = 0;
            // 
            // btnCancelAsyncQuery
            // 
            this.btnCancelAsyncQuery.Location = new System.Drawing.Point(878, 629);
            this.btnCancelAsyncQuery.Name = "btnCancelAsyncQuery";
            this.btnCancelAsyncQuery.Size = new System.Drawing.Size(58, 132);
            this.btnCancelAsyncQuery.TabIndex = 99;
            this.btnCancelAsyncQuery.Text = "Cancel All Open Async Queries";
            this.btnCancelAsyncQuery.UseVisualStyleBackColor = true;
            this.btnCancelAsyncQuery.Click += new System.EventHandler(this.btnCancelAsyncQuery_Click);
            // 
            // numericTextBox1
            // 
            this.numericTextBox1.Location = new System.Drawing.Point(106, 244);
            this.numericTextBox1.Name = "numericTextBox1";
            this.numericTextBox1.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox1.TabIndex = 139;
            this.numericTextBox1.Text = "10";
            // 
            // numericTextBox2
            // 
            this.numericTextBox2.Location = new System.Drawing.Point(106, 218);
            this.numericTextBox2.Name = "numericTextBox2";
            this.numericTextBox2.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox2.TabIndex = 137;
            this.numericTextBox2.Text = "1";
            // 
            // numericTextBox5
            // 
            this.numericTextBox5.Location = new System.Drawing.Point(68, 460);
            this.numericTextBox5.Name = "numericTextBox5";
            this.numericTextBox5.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox5.TabIndex = 118;
            this.numericTextBox5.Text = "1";
            // 
            // numericTextBox6
            // 
            this.numericTextBox6.Location = new System.Drawing.Point(68, 486);
            this.numericTextBox6.Name = "numericTextBox6";
            this.numericTextBox6.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox6.TabIndex = 120;
            this.numericTextBox6.Text = "1";
            // 
            // numericTextBox7
            // 
            this.numericTextBox7.Location = new System.Drawing.Point(77, 512);
            this.numericTextBox7.Name = "numericTextBox7";
            this.numericTextBox7.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox7.TabIndex = 122;
            this.numericTextBox7.Text = "0";
            // 
            // numericTextBox8
            // 
            this.numericTextBox8.Location = new System.Drawing.Point(181, 590);
            this.numericTextBox8.Name = "numericTextBox8";
            this.numericTextBox8.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox8.TabIndex = 128;
            this.numericTextBox8.Text = "360";
            // 
            // numericTextBox9
            // 
            this.numericTextBox9.Location = new System.Drawing.Point(77, 538);
            this.numericTextBox9.Name = "numericTextBox9";
            this.numericTextBox9.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox9.TabIndex = 124;
            this.numericTextBox9.Text = "360";
            // 
            // numericTextBox10
            // 
            this.numericTextBox10.Location = new System.Drawing.Point(90, 433);
            this.numericTextBox10.Name = "numericTextBox10";
            this.numericTextBox10.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox10.TabIndex = 116;
            this.numericTextBox10.Text = "1";
            // 
            // ctrlSamplePoint2
            // 
            this.ctrlSamplePoint2._DgvControlName = "dgvAOSTargetPoints";
            this.ctrlSamplePoint2._IsRelativeToDTM = false;
            this.ctrlSamplePoint2._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePoint2._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePoint2._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePoint2._SampleOnePoint = false;
            this.ctrlSamplePoint2._UserControlName = "";
            this.ctrlSamplePoint2.IsAsync = false;
            this.ctrlSamplePoint2.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePoint2.Location = new System.Drawing.Point(485, 291);
            this.ctrlSamplePoint2.Name = "ctrlSamplePoint2";
            this.ctrlSamplePoint2.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePoint2.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePoint2.TabIndex = 114;
            this.ctrlSamplePoint2.Text = "...";
            this.ctrlSamplePoint2.UseVisualStyleBackColor = true;
            // 
            // numericTextBox11
            // 
            this.numericTextBox11.Location = new System.Drawing.Point(119, 564);
            this.numericTextBox11.Name = "numericTextBox11";
            this.numericTextBox11.Size = new System.Drawing.Size(100, 20);
            this.numericTextBox11.TabIndex = 126;
            this.numericTextBox11.Text = "0";
            // 
            // SpatialQueriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(945, 763);
            this.Controls.Add(this.btnCancelAsyncQuery);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.tcSpatialQueries);
            this.Name = "SpatialQueriesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpatialQueriesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpatialQueriesForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SpatialQueriesForm_FormClosed);
            this.Load += new System.EventHandler(this.SpatialQueriesForm_Load);
            this.gbRayIntersectionTargetResult.ResumeLayout(false);
            this.gbRayIntersectionTargetResult.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbRayIntersectionResult.ResumeLayout(false);
            this.gbRayIntersectionResult.PerformLayout();
            this.gbTerrainQueriesNumCacheTiles.ResumeLayout(false);
            this.gbTerrainQueriesNumCacheTiles.PerformLayout();
            this.tcSpatialQueries.ResumeLayout(false);
            this.tabPageQueryParams.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.LocationFromTwoDistancesAndAzimuth.ResumeLayout(false);
            this.LocationFromTwoDistancesAndAzimuth.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbTerrainAngles.ResumeLayout(false);
            this.gbTerrainAngles.PerformLayout();
            this.tpHeights.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tpRayIntersection.ResumeLayout(false);
            this.tpRayIntersection.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tpSightLines.ResumeLayout(false);
            this.tcSightLines.ResumeLayout(false);
            this.tpLineOfSight.ResumeLayout(false);
            this.tpLineOfSight.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.gbLineOfSightGeneralParams.ResumeLayout(false);
            this.gbLineOfSightGeneralParams.PerformLayout();
            this.tpAreaOfSight.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.rgbSightObjectRect.ResumeLayout(false);
            this.rgbSightObjectRect.PerformLayout();
            this.rgbSightObjectEllipse.ResumeLayout(false);
            this.rgbSightObjectEllipse.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.gbAOSColors.ResumeLayout(false);
            this.gbAOSColors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlphaColor)).EndInit();
            this.gbMultipleScouters.ResumeLayout(false);
            this.gbMultipleScouters.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSurroundingResults)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.grpShowOperations.ResumeLayout(false);
            this.grpShowOperations.PerformLayout();
            this.tpRasterAndTraversability.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.gbGetTraversabilityFromColorCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGetTraversabilityFromColorCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGetRasterColor)).EndInit();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsyncQueryResults)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

       

        #endregion
        private System.Windows.Forms.Button btnGetRayIntersectionTarget;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.Ctrl3DVector ctrl3DNormalRayIntersection;
        private MCTester.Controls.CtrlSamplePoint ctrlRayOrigin;
        private System.Windows.Forms.CheckBox chxIsIntersectionFound;
        private System.Windows.Forms.Label label5;
        private MCTester.Controls.Ctrl3DVector ctrl3DRayOrigin;
        private MCTester.Controls.NumericTextBox ntxDistance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetRayIntersection;
        private MCTester.Controls.Ctrl3DVector ctrl3DIntersectionPt;
        private MCTester.Controls.NumericTextBox ntxMaxDistance;
        private System.Windows.Forms.Label label6;
        private MCTester.Controls.Ctrl3DVector ctrl3DRayDirection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.CtrlSamplePoint ctrlRayDestination;
        private System.Windows.Forms.Label label8;
        private MCTester.Controls.Ctrl3DVector ctrl3DRayDestination;
        private System.Windows.Forms.GroupBox gbRayIntersectionTargetResult;
        private System.Windows.Forms.Label label11;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorIntersectionPoint;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label16;
        private MCTester.Controls.NumericTextBox ntxPartIndex;
        private MCTester.Controls.NumericTextBox ntxObjectHashCode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label17;
        private MCTester.Controls.NumericTextBox ntxItemHashCode;
        private System.Windows.Forms.Label label18;
        private MCTester.Controls.NumericTextBox ntxTargetIndex;
        private System.Windows.Forms.Label label15;
        private MCTester.Controls.NumericTextBox ntxLayerHashCode;
        private System.Windows.Forms.Label label14;
        private MCTester.Controls.NumericTextBox ntxTerrainHashCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnNextTarget;
        private System.Windows.Forms.Button btnPreviousTarget;
        private System.Windows.Forms.TextBox txtIntersectionTargetType;
        private System.Windows.Forms.TextBox txtIntersectionCoordSystem;
        private System.Windows.Forms.TextBox txtItemPart;
        private System.Windows.Forms.Label lblTargetCounter;
        private System.Windows.Forms.GroupBox gbRayIntersectionResult;
        private System.Windows.Forms.Button btnTerrainNumCacheTilesOK;
        private System.Windows.Forms.Label label21;
        private MCTester.Controls.NumericTextBox ntxNumTiles;
        private System.Windows.Forms.GroupBox gbTerrainQueriesNumCacheTiles;
        private System.Windows.Forms.ListBox lstTerrain;
        private System.Windows.Forms.TabControl tcSpatialQueries;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpRayIntersection;
        private System.Windows.Forms.TabPage tpSightLines;
        private MCTester.Controls.NumericTextBox ntxAOSEndAngle;
        private System.Windows.Forms.Label label40;
        private MCTester.Controls.NumericTextBox ntxAOSStartAngle;
        private System.Windows.Forms.Label label41;
        private MCTester.Controls.NumericTextBox ntxAOSRadiusY;
        private System.Windows.Forms.Label label39;
        private MCTester.Controls.NumericTextBox ntxAOSRadiusX;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Button btnGetLineOfSight;
        private System.Windows.Forms.Label label26;
        private MCTester.Controls.NumericTextBox ntxLOSMinimalScouterHeightForVisibility;
        private MCTester.Controls.NumericTextBox ntxLOSMinPitchAngle;
        private System.Windows.Forms.Label label29;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorLOSScouter;
        private MCTester.Controls.NumericTextBox ntxLOSMinimalTargetHeightForVisibility;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label28;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePointLOSScouter;
        private System.Windows.Forms.CheckBox chxLOSIsTargetVisible;
        private MCTester.Controls.NumericTextBox ntxLOSMaxPitchAngle;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorLOSTarget;
        private System.Windows.Forms.Label label32;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePointLOSTarget;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.CheckBox chxLOSIsScouterHeightAbsolute;
        private System.Windows.Forms.CheckBox chxLOSIsTargetHeightAbsolute;
        private System.Windows.Forms.Button btnGetLineOfSightPoints;
        private System.Windows.Forms.GroupBox gbLineOfSightGeneralParams;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label44;
        private MCTester.Controls.NumericTextBox ntxCrestClearanceAngle;
        private System.Windows.Forms.GroupBox gbTerrainAngles;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorTerrainAnglesPt;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePointTerrainAngles;
        private System.Windows.Forms.Button btnTerrainAnglesDrawLine;
        private MCTester.Controls.NumericTextBox ntxTerrainAnglesAzimuth;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Button btnGetTerrainAngles;
        private System.Windows.Forms.TextBox txtPitch;
        private System.Windows.Forms.TextBox txtRoll;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button btnDrawEllipseAreaOfSight;
        private MCTester.Controls.NumericTextBox ntxCrestClearanceDistance;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button btnCreateMatrix;
        private System.Windows.Forms.TabControl tcSightLines;
        private System.Windows.Forms.TabPage tpLineOfSight;
        private System.Windows.Forms.TabPage tpAreaOfSight;
        private System.Windows.Forms.Button btnAOSGetPointVisibility;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label54;
        private MCTester.Controls.NumericTextBox numericTextBox1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label55;
        private MCTester.Controls.NumericTextBox numericTextBox2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label57;
        private MCTester.Controls.NumericTextBox numericTextBox3;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePoint1;
        private System.Windows.Forms.Label label58;
        private MCTester.Controls.Ctrl3DVector ctrl3DVector1;
        private MCTester.Controls.NumericTextBox numericTextBox4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Button button5;
        private MCTester.Controls.NumericTextBox numericTextBox5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Button button7;
        private MCTester.Controls.NumericTextBox numericTextBox6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.GroupBox groupBox9;
        private MCTester.Controls.NumericTextBox numericTextBox7;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Button button9;
        private MCTester.Controls.NumericTextBox numericTextBox8;
        private MCTester.Controls.NumericTextBox numericTextBox9;
        private MCTester.Controls.NumericTextBox numericTextBox10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePoint2;
        private MCTester.Controls.NumericTextBox numericTextBox11;
        private System.Windows.Forms.Label label67;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePointAOSPointVisibility;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorAOSPointVisibility;
        private System.Windows.Forms.TextBox txtAOSPointVisibilityAnswer;
        private System.Windows.Forms.Label label70;
        private MCTester.Controls.NumericTextBox ntxCellFactor;
        private System.Windows.Forms.Label label74;
        private MCTester.Controls.NumericTextBox ntxVehicleRadius;
        private System.Windows.Forms.Label label73;
        private MCTester.Controls.NumericTextBox ntxWalkingRadius;
        private System.Windows.Forms.Label label72;
        private MCTester.Controls.NumericTextBox ntxStandingRadius;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Button btnQualityParamsDrawLine;
        private System.Windows.Forms.ComboBox cmbQualityParamsTargetTypes;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private MCTester.Controls.NumericTextBox ntxCoverageQuality;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnCalcCoverage;
        private System.Windows.Forms.CheckBox chxFillPointsVisibility;
        private System.Windows.Forms.Label label84;
        private Controls.NumericTextBox ntxRotationAzimuthDeg;
        private System.Windows.Forms.Label lblRectWidth;
        private Controls.NumericTextBox ntxRectWidth;
        private System.Windows.Forms.Label lblRectHeight;
        private Controls.NumericTextBox ntxRectHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.NumericTextBox ntxNumOfThread;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Button btnStartMultiThreadTest;
        private System.Windows.Forms.Label label85;
        private Controls.CtrlSamplePoint ctrlSamplePointFirstPoint;
        private Controls.Ctrl3DVector ctrl3DVectorFirstPoint;
        private System.Windows.Forms.CheckBox cbxIsLogThreadTest;
        private System.Windows.Forms.Label label88;
        private Controls.NumericTextBox ntxDurationTime;
        private System.Windows.Forms.Label label86;
        private Controls.NumericTextBox ntxNumTestingPt;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.CheckBox chxThrowException;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDrawPath;
        private System.Windows.Forms.Button btnGetSmoothedPath;
        private System.Windows.Forms.Label label90;
        private Controls.NumericTextBox ntxSmoothDistance;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.GroupBox LocationFromTwoDistancesAndAzimuth;
        private Controls.Ctrl3DVector ctrl3DLocationFrstOrgnPt;
        private Controls.CtrlSamplePoint ctrlLocationFrstOrgnPt;
        private System.Windows.Forms.Label label91;
        private Controls.NumericTextBox ntxFirstDistance;
        private System.Windows.Forms.Label label92;
        private Controls.NumericTextBox ntxFirstAzimut;
        private System.Windows.Forms.Label label93;
        private Controls.NumericTextBox ntxTargetHeightAboveGround;
        private System.Windows.Forms.Label label94;
        private Controls.NumericTextBox ntxSecondDistance;
        private System.Windows.Forms.Label label95;
        private Controls.Ctrl3DVector ctrl3DLocationScndOrgnPt;
        private Controls.CtrlSamplePoint ctrlLocationScndOrgnPt;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Button btnGetLocation;
        private Controls.Ctrl3DVector ctrl3DLocationResult;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.GroupBox grpShowOperations;
        private System.Windows.Forms.CheckBox chxDrawUnseenPolygons;
        private System.Windows.Forms.CheckBox chxDrawSeenPolygons;
        private System.Windows.Forms.CheckBox chxDrawLineOfSight;
        private System.Windows.Forms.CheckBox chxDrawAreaOfSight;
        private System.Windows.Forms.CheckBox chxIsCreateAndDrawMatrixAutomatic;
        private System.Windows.Forms.Button btnUpdateEllipse;
        private System.Windows.Forms.CheckBox chxDrawObject;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.CheckBox chxDrawSeenStaticObjects2;
        private System.Windows.Forms.CheckBox chxDrawSeenStaticObjects;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbRayDirection;
        private System.Windows.Forms.RadioButton rbRayDestination;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.DataGridView dgvAsyncQueryResults;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.ComboBox cmbLayerTypes;
        private System.Windows.Forms.Button btnTerrainNumCacheTilesGet;
        private System.Windows.Forms.CheckBox cbTerrainAnglesAsync;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.CheckBox cbGetLineOfSightAsync;
        private System.Windows.Forms.CheckBox cbGetPointVisibilityAsync;
        private System.Windows.Forms.CheckBox cbRayIntersectionAsync;
        private System.Windows.Forms.CheckBox cbLocationFromTwoDistancesAndAzimuthAsync;
        private Controls.NumericTextBox ntxDistanceFromSecondLocationAndResult;
        private System.Windows.Forms.Label label102;
        private Controls.NumericTextBox ntxDistanceFromFirstLocationAndResult;
        private System.Windows.Forms.Label label101;
        private Controls.NumericTextBox ntxDifferenceHeightsFromResultAndTerrain;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.CheckBox cbLocationFromDistancesAndAzimuthFoundHeight;
        private System.Windows.Forms.TabPage tpHeights;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.CheckBox cbTerrainHeightAsync;
        private System.Windows.Forms.CheckBox chxHeightFound;
        private Controls.Ctrl3DVector ctrl3DTerrainHeightPt;
        private Controls.CtrlSamplePoint ctrlRelativeHeightPt;
        private System.Windows.Forms.Button btnGetTerrainHeight3;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label105;
        private Controls.Ctrl3DVector ctrl3DRequestedNormal;
        private Controls.NumericTextBox ntxHeight;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.CheckBox cbTerrainHeightAlongLineAsync;
        private Controls.NumericTextBox ntxHeightDelta;
        private Controls.NumericTextBox ntxMinSlope;
        private Controls.NumericTextBox ntxMaxSlope;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Button btnDrawLine;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.CheckBox cbExtremeHeightPointsInPolygonAsync;
        private System.Windows.Forms.CheckBox chxPointsFound;
        private Controls.Ctrl3DVector ctrl3DVectorLowestPt;
        private System.Windows.Forms.Button btnDrawPolygon;
        private Controls.Ctrl3DVector ctrl3DVectorHighestPt;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.CheckBox cbTerrainMatrixAsync;
        private Controls.Ctrl3DVector ctrl3DLowerLeftPoint;
        private Controls.CtrlSamplePoint ctrlSamplePoint3;
        private System.Windows.Forms.Button btnTerrainHeightMatrix;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private Controls.NumericTextBox ntxVerticalResolution;
        private System.Windows.Forms.Label label22;
        private Controls.NumericTextBox ntxHorizontalResolution;
        private Controls.NumericTextBox ntxNumHorizontalPoints;
        private System.Windows.Forms.Label label23;
        private Controls.NumericTextBox ntxNumVerticalPoints;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox16;
        private Controls.Ctrl3DVector ctrl3DVectorAOSScouter;
        private Controls.CtrlSamplePoint ctrlSamplePointAOSScouter;
        private System.Windows.Forms.Label label119;
        private Controls.CtrlSamplePoint ctrlSamplePointMultipleScouter;
        private System.Windows.Forms.CheckBox cbAreaOfSightAsync;
        private System.Windows.Forms.TextBox tbNameCalc;
        private System.Windows.Forms.Button btnCalcAOS2;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown numUDAlphaColor;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.CheckBox chxCalcStaticObjects;
        private System.Windows.Forms.CheckBox chxCalcUnseenPolygons;
        private System.Windows.Forms.CheckBox chxCalcSeenPolygons;
        private System.Windows.Forms.CheckBox chxCalcLineOfSight;
        private System.Windows.Forms.CheckBox chxCalcAreaOfSight;
        private System.Windows.Forms.PictureBox picbColor;
        private Controls.NumericTextBox ntxNumberOfRayes;
        private System.Windows.Forms.CheckBox chxGPU_Based;
        private System.Windows.Forms.CheckBox chxAOSIsScouterHeightAbsolute;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.Label label122;
        private Controls.NumericTextBox ntxAOSMaxPitchAngle;
        private System.Windows.Forms.Label label123;
        private Controls.NumericTextBox ntxAOSMinPitchAngle;
        private System.Windows.Forms.CheckBox chxAOSIsTargetsHeightAbsolute;
        private System.Windows.Forms.Label label124;
        private Controls.NumericTextBox ntxAOSTargetHeight;
        private System.Windows.Forms.Label label125;
        private Controls.NumericTextBox ntxAOSGPUTargetResolution;
        private System.Windows.Forms.Button btnCalcBestPoints;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label51;
        private Controls.NumericTextBox ntxScouterRadiusY;
        private Controls.NumericTextBox ntxScouterRadiusX;
        private Controls.CtrlSamplePoint ctrlSamplePointScouter;
        private Controls.Ctrl3DVector ctrl3DScouterCenterPoint;
        private System.Windows.Forms.Label label52;
        private Controls.NumericTextBox ntxMaxNumOfScouters;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.ComboBox cmbEScoutersSumType;
        private System.Windows.Forms.CheckBox cbIsMultipleScouters;
        private System.Windows.Forms.GroupBox gbMultipleScouters;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label82;
        private Controls.NumericTextBox ntxSurroundingRadiusX;
        private Controls.NumericTextBox ntxSurroundingRadiusY;
        private System.Windows.Forms.Label label78;
        private Controls.Ctrl3DVector ctrl3DSurroundingPoint;
        private Controls.CtrlSamplePoint ctrlSamplePoint4;
        private System.Windows.Forms.Button btnGetPointVisibilityColorsSurrounding;
        private System.Windows.Forms.DataGridView dgvSurroundingResults;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.CheckBox cbHexadecimelNumber;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.CheckedListBox cbLstAreaOfSightMatrix;
        private System.Windows.Forms.Button btnSumAreaOfSightMatrices;
        private System.Windows.Forms.Button btnCloneAreaOfSightMatrix;
        private System.Windows.Forms.Button btnAreSameRectAreaOfSightMatrices;
        private System.Windows.Forms.CheckBox cbAreSameRect;
        private System.Windows.Forms.CheckBox cbCloneMatrixFillPoints;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cmbEScoutersSumTypeMatrices;
        private System.Windows.Forms.Button btnGetHeightAlongLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameFunction;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Async;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button btnExtremeHeightPointsInPolygon;
        private System.Windows.Forms.CheckBox cbCalcBestPointsAsync;
        private System.Windows.Forms.TabPage tpRasterAndTraversability;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.ListBox lbRasterMapLayer;
        private Controls.NumericTextBox ntbLOD;
        private System.Windows.Forms.Label label80;
        private Controls.Ctrl3DVector ctrl3DGetRasterPoint;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.CheckBox cbNearestPixel;
        private System.Windows.Forms.Button btnGetRasterLayerColorByPoint;
        private System.Windows.Forms.CheckBox cbGetRasterAsync;
        private Controls.NumericTextBox ntbGetRasterColorR;
        private Controls.NumericTextBox ntbGetRasterColorB;
        private System.Windows.Forms.Label label128;
        private Controls.NumericTextBox ntbGetRasterColorA;
        private System.Windows.Forms.Label label127;
        private Controls.NumericTextBox ntbGetRasterColorG;
        private System.Windows.Forms.Label label126;
        private Controls.CtrlSamplePoint ctrlLocationGetRaster;
        private System.Windows.Forms.PictureBox pbGetRasterColor;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.DataGridView dgvGetTraversabilityFromColorCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DirectionAngle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.GroupBox gbGetTraversabilityFromColorCode;
        private System.Windows.Forms.Button btnGetTraversabilityFromColorCode;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ListBox lbTraversabilityMapLayers;
        private System.Windows.Forms.Button btnGetTraversabilityAlongLine;
        private System.Windows.Forms.CheckBox cbGetTraversabilityAlongLineAsync;
        private System.Windows.Forms.Button btnDrawLineTraversability;
        private System.Windows.Forms.Button btnExtremeUpdatePolygon;
        private System.Windows.Forms.Label label131;
        private Controls.NumericTextBox ntxNumCallingFunction;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Button btnAOSLoad;
        private System.Windows.Forms.Button btnAOSSave;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.Button btnTestSaveAndLoad;
        private System.Windows.Forms.CheckBox chxAreSameMatrices;
        private System.Windows.Forms.Button btnTALUpdateLine;
        private System.Windows.Forms.Button btnCancelAsyncQuery;
        private System.Windows.Forms.TabPage tabPageQueryParams;
        private Controls.CtrlQueryParams ctrlQueryParams1;
        private System.Windows.Forms.Button btnSaveMatrixToFile;
        private System.Windows.Forms.ComboBox cmbAOSColors;
        private System.Windows.Forms.GroupBox gbAOSColors;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.CheckBox chxMinimalScouterHeight;
        private System.Windows.Forms.CheckBox chxMinimalTargetHeight;
        private Controls.CtrlPointsGrid ctrlExtremeHeightPoints;
        private Controls.CtrlPointsGrid ctrlTraversabilityAlongLinePoints;
        private Controls.CtrlPointsGrid ctrlPointsGridSightObject;
        private Controls.CtrlPointsGrid ctrlPointsGridMultipleScouters;
        private System.Windows.Forms.GroupBox groupBox22;
        private Controls.CtrlRadioGroupBox rgbSightObjectRect;
        private Controls.CtrlRadioGroupBox rgbSightObjectEllipse;
        private System.Windows.Forms.RadioButton rbSightObjectPolygon;
        private System.Windows.Forms.Button btnAOSExport;
        private System.Windows.Forms.Button btnAOSImport;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.ListBox lstQuerySecondaryDtmLayers;
        private System.Windows.Forms.Button btnShowSelectedDTMLayerData;
    }
}