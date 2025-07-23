using System;

namespace MCTester
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tsDrawingSymbolicItems = new System.Windows.Forms.ToolStrip();
            this.tsbLineItem = new System.Windows.Forms.ToolStripButton();
            this.tsbArrowItem = new System.Windows.Forms.ToolStripButton();
            this.tsbLineExpansionItem = new System.Windows.Forms.ToolStripButton();
            this.tsbPolygonItem = new System.Windows.Forms.ToolStripButton();
            this.tsbEllipseItem = new System.Windows.Forms.ToolStripButton();
            this.tsbArcItem = new System.Windows.Forms.ToolStripButton();
            this.tsbRectangleItem = new System.Windows.Forms.ToolStripButton();
            this.tsbTextItem = new System.Windows.Forms.ToolStripButton();
            this.tsbPictureItem = new System.Windows.Forms.ToolStripButton();
            this.tsDrawingPhysicalItems = new System.Windows.Forms.ToolStrip();
            this.tsbMeshItem = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbNativeMesh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbNativeLODMesh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbParticalEffectItem = new System.Windows.Forms.ToolStripButton();
            this.tsbLightItem = new System.Windows.Forms.ToolStripSplitButton();
            this.tsbDirectionalLight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbPointLight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSpotLight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbProjectorItem = new System.Windows.Forms.ToolStripButton();
            this.tsbSoundItem = new System.Windows.Forms.ToolStripButton();
            this.tsEditMode = new System.Windows.Forms.ToolStrip();
            this.tsbEditObject = new System.Windows.Forms.ToolStripButton();
            this.tsbInitObject = new System.Windows.Forms.ToolStripButton();
            this.tsbEditModeProperties = new System.Windows.Forms.ToolStripButton();
            this.tsAttendantTools = new System.Windows.Forms.ToolStrip();
            this.tsbMapProductionTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPerformanceTester = new System.Windows.Forms.ToolStripButton();
            this.tsbStressTester = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbObjectLoader = new System.Windows.Forms.ToolStripButton();
            this.tsbObjectAutoAnimatation = new System.Windows.Forms.ToolStripButton();
            this.tsbAnimationState = new System.Windows.Forms.ToolStripButton();
            this.tsCalculations = new System.Windows.Forms.ToolStrip();
            this.tsbGeographicCalculations = new System.Windows.Forms.ToolStripButton();
            this.tsbGeometricCalculations = new System.Windows.Forms.ToolStripButton();
            this.tsbStandaloneSQ = new System.Windows.Forms.ToolStripButton();
            this.tsExtendedEditMode = new System.Windows.Forms.ToolStrip();
            this.tsbDistanceDirectionMeasure = new System.Windows.Forms.ToolStripButton();
            this.tsbDynamicZoom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEditModeNavigation = new System.Windows.Forms.ToolStripButton();
            this.tsbExitCurrentAction = new System.Windows.Forms.ToolStripButton();
            this.tsbCheckRenderNeeded = new System.Windows.Forms.ToolStripButton();
            this.tsbEventCallBack = new System.Windows.Forms.ToolStripButton();
            this.tsbNavPath = new System.Windows.Forms.ToolStripButton();
            this.tsbSightEllipseItem = new System.Windows.Forms.ToolStripButton();
            this.tsbCollectGC = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveViewportToSession = new System.Windows.Forms.ToolStripButton();
            this.tsbLoadViewport = new System.Windows.Forms.ToolStripButton();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.MainStatusBarStatistics = new System.Windows.Forms.StatusStrip();
            this.sbLastFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbLastFPSBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbAvgFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbAvgFPSBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbBestFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbBestFPSBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbWorstFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbWorstFPSBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbNumLastFrameTriangles = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbNumLastFrameTrianglesBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbNumLastFrameBatches = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbNumLastFrameBatchesBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extendedEditModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symbolicItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.physicalItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attendantToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vectorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadAndSaveSchemesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mosaicsCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rollingShutterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointingFingerDBGeneratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vectorXMLCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signToSecurityServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sbWorldCoordX = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbWorldCoordXBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbWorldCoordY = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbWorldCoordYBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbWorldCoordZ = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbWorldCoordZBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbScreenCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbScreenCoordBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbImageCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbImageCoordBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbViewportID = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbViewportIDBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbScaleBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbMapScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbMapScaleBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbAverageFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbAvrFPSBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbMsgBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainStatusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripPanel3 = new System.Windows.Forms.ToolStripPanel();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbMapWorld = new System.Windows.Forms.ToolStripButton();
            this.tsbObjectWorld = new System.Windows.Forms.ToolStripButton();
            this.tsVectorial = new System.Windows.Forms.ToolStrip();
            this.tsbVectorial = new System.Windows.Forms.ToolStripButton();
            this.tsbPathFinder = new System.Windows.Forms.ToolStripButton();
            this.tsMapOperations = new System.Windows.Forms.ToolStrip();
            this.tsbNavigateMap = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.tsbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbCenterMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbMapGrid = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbUTMMapGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbGeoMapGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMGRSMapGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbNZMGMapGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbGeoRefMapGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbMapHeightLines = new System.Windows.Forms.ToolStripButton();
            this.tsbDtmVisualization = new System.Windows.Forms.ToolStripButton();
            this.tsbDropDownFootprintPoints = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbFootprintPointsSync = new System.Windows.Forms.ToolStripButton();
            this.tsbFootprintPointsAsync = new System.Windows.Forms.ToolStripButton();
            this.tsbCameraCornersSync = new System.Windows.Forms.ToolStripButton();
            this.tsbCameraCornersAsync = new System.Windows.Forms.ToolStripButton();
            this.tsbCameraCornersWithHorizonsSync = new System.Windows.Forms.ToolStripButton();
            this.tsbCameraCornersWithHorizonsAsync = new System.Windows.Forms.ToolStripButton();
            this.tsbFileProductions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.tsbScan = new System.Windows.Forms.ToolStripButton();
            this.tsbEnvironment = new System.Windows.Forms.ToolStripButton();
            this.tsb2DTo3D = new System.Windows.Forms.ToolStripButton();
            this.tsGeneral = new System.Windows.Forms.ToolStrip();
            this.tsbOpenMapManually = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenMapScheme = new System.Windows.Forms.ToolStripButton();
            this.tbsOpenLayers = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbTileWindows = new System.Windows.Forms.ToolStripButton();
            this.tsbRenderType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGeneralProperties = new System.Windows.Forms.ToolStripButton();
            this.tsbObjectProperties = new System.Windows.Forms.ToolStripButton();
            this.tspMainToolsBar = new System.Windows.Forms.ToolStripPanel();
            this.tsManagments = new System.Windows.Forms.ToolStrip();
            this.tsDrawingSymbolicItems.SuspendLayout();
            this.tsDrawingPhysicalItems.SuspendLayout();
            this.tsEditMode.SuspendLayout();
            this.tsAttendantTools.SuspendLayout();
            this.tsCalculations.SuspendLayout();
            this.tsExtendedEditMode.SuspendLayout();
            this.MainStatusBarStatistics.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.MainStatusBar.SuspendLayout();
            this.toolStripPanel3.SuspendLayout();
            this.tsVectorial.SuspendLayout();
            this.tsMapOperations.SuspendLayout();
            this.tsGeneral.SuspendLayout();
            this.tspMainToolsBar.SuspendLayout();
            this.tsManagments.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsDrawingSymbolicItems
            // 
            this.tsDrawingSymbolicItems.BackColor = System.Drawing.SystemColors.Menu;
            this.tsDrawingSymbolicItems.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDrawingSymbolicItems.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDrawingSymbolicItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLineItem,
            this.tsbArrowItem,
            this.tsbLineExpansionItem,
            this.tsbPolygonItem,
            this.tsbEllipseItem,
            this.tsbArcItem,
            this.tsbRectangleItem,
            this.tsbTextItem,
            this.tsbPictureItem});
            this.tsDrawingSymbolicItems.Location = new System.Drawing.Point(3, 0);
            this.tsDrawingSymbolicItems.Name = "tsDrawingSymbolicItems";
            this.tsDrawingSymbolicItems.Size = new System.Drawing.Size(264, 31);
            this.tsDrawingSymbolicItems.TabIndex = 10;
            this.tsDrawingSymbolicItems.Text = "Drawing Symbolic Items Panel";
            // 
            // tsbLineItem
            // 
            this.tsbLineItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLineItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbLineItem.Image")));
            this.tsbLineItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLineItem.Name = "tsbLineItem";
            this.tsbLineItem.Size = new System.Drawing.Size(28, 28);
            this.tsbLineItem.Text = "Line Item";
            this.tsbLineItem.Click += new System.EventHandler(this.tsbLineItem_Click);
            // 
            // tsbArrowItem
            // 
            this.tsbArrowItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArrowItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbArrowItem.Image")));
            this.tsbArrowItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArrowItem.Name = "tsbArrowItem";
            this.tsbArrowItem.Size = new System.Drawing.Size(28, 28);
            this.tsbArrowItem.Text = "Arrow Item";
            this.tsbArrowItem.Click += new System.EventHandler(this.tsbArrowItem_Click);
            // 
            // tsbLineExpansionItem
            // 
            this.tsbLineExpansionItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLineExpansionItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbLineExpansionItem.Image")));
            this.tsbLineExpansionItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLineExpansionItem.Name = "tsbLineExpansionItem";
            this.tsbLineExpansionItem.Size = new System.Drawing.Size(28, 28);
            this.tsbLineExpansionItem.Text = "Line Expansion Item";
            this.tsbLineExpansionItem.Click += new System.EventHandler(this.tsbLineExpansionItem_Click);
            // 
            // tsbPolygonItem
            // 
            this.tsbPolygonItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPolygonItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbPolygonItem.Image")));
            this.tsbPolygonItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPolygonItem.Name = "tsbPolygonItem";
            this.tsbPolygonItem.Size = new System.Drawing.Size(28, 28);
            this.tsbPolygonItem.Text = "Polygon Item";
            this.tsbPolygonItem.Click += new System.EventHandler(this.tsbPolygonItem_Click);
            // 
            // tsbEllipseItem
            // 
            this.tsbEllipseItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEllipseItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbEllipseItem.Image")));
            this.tsbEllipseItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEllipseItem.Name = "tsbEllipseItem";
            this.tsbEllipseItem.Size = new System.Drawing.Size(28, 28);
            this.tsbEllipseItem.Text = "Ellipse Item";
            this.tsbEllipseItem.Click += new System.EventHandler(this.tsbEllipseItem_Click);
            // 
            // tsbArcItem
            // 
            this.tsbArcItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbArcItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbArcItem.Image")));
            this.tsbArcItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbArcItem.Name = "tsbArcItem";
            this.tsbArcItem.Size = new System.Drawing.Size(28, 28);
            this.tsbArcItem.Text = "Arc Item";
            this.tsbArcItem.Click += new System.EventHandler(this.tsbArcItem_Click);
            // 
            // tsbRectangleItem
            // 
            this.tsbRectangleItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRectangleItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbRectangleItem.Image")));
            this.tsbRectangleItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRectangleItem.Name = "tsbRectangleItem";
            this.tsbRectangleItem.Size = new System.Drawing.Size(28, 28);
            this.tsbRectangleItem.Text = "Rectangle Item";
            this.tsbRectangleItem.Click += new System.EventHandler(this.tsbRectangleItem_Click);
            // 
            // tsbTextItem
            // 
            this.tsbTextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTextItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbTextItem.Image")));
            this.tsbTextItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTextItem.Name = "tsbTextItem";
            this.tsbTextItem.Size = new System.Drawing.Size(28, 28);
            this.tsbTextItem.Text = "Text Item";
            this.tsbTextItem.Click += new System.EventHandler(this.tsbTextItem_Click);
            // 
            // tsbPictureItem
            // 
            this.tsbPictureItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPictureItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbPictureItem.Image")));
            this.tsbPictureItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPictureItem.Name = "tsbPictureItem";
            this.tsbPictureItem.Size = new System.Drawing.Size(28, 28);
            this.tsbPictureItem.Text = "Picture Item";
            this.tsbPictureItem.Click += new System.EventHandler(this.tsbPictureItem_Click);
            // 
            // tsDrawingPhysicalItems
            // 
            this.tsDrawingPhysicalItems.BackColor = System.Drawing.SystemColors.Menu;
            this.tsDrawingPhysicalItems.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDrawingPhysicalItems.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDrawingPhysicalItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMeshItem,
            this.tsbParticalEffectItem,
            this.tsbLightItem,
            this.tsbProjectorItem,
            this.tsbSoundItem});
            this.tsDrawingPhysicalItems.Location = new System.Drawing.Point(267, 0);
            this.tsDrawingPhysicalItems.Name = "tsDrawingPhysicalItems";
            this.tsDrawingPhysicalItems.Size = new System.Drawing.Size(173, 31);
            this.tsDrawingPhysicalItems.TabIndex = 11;
            this.tsDrawingPhysicalItems.Text = "Drawing Physical Items Panel";
            // 
            // tsbMeshItem
            // 
            this.tsbMeshItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMeshItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNativeMesh,
            this.tsbNativeLODMesh});
            this.tsbMeshItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbMeshItem.Image")));
            this.tsbMeshItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMeshItem.Name = "tsbMeshItem";
            this.tsbMeshItem.Size = new System.Drawing.Size(37, 28);
            this.tsbMeshItem.Text = "Mesh Item";
            // 
            // tsbNativeMesh
            // 
            this.tsbNativeMesh.Name = "tsbNativeMesh";
            this.tsbNativeMesh.Size = new System.Drawing.Size(140, 22);
            this.tsbNativeMesh.Text = "Native Mesh";
            this.tsbNativeMesh.ToolTipText = "NativeMesh";
            this.tsbNativeMesh.Click += new System.EventHandler(this.tsbNativeMesh_Click);
            // 
            // tsbNativeLODMesh
            // 
            this.tsbNativeLODMesh.Name = "tsbNativeLODMesh";
            this.tsbNativeLODMesh.Size = new System.Drawing.Size(140, 22);
            this.tsbNativeLODMesh.Text = "Native LOD";
            this.tsbNativeLODMesh.ToolTipText = "Native LOD";
            this.tsbNativeLODMesh.Click += new System.EventHandler(this.tsbNativeLODMesh_Click);
            // 
            // tsbParticalEffectItem
            // 
            this.tsbParticalEffectItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbParticalEffectItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbParticalEffectItem.Image")));
            this.tsbParticalEffectItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbParticalEffectItem.Name = "tsbParticalEffectItem";
            this.tsbParticalEffectItem.Size = new System.Drawing.Size(28, 28);
            this.tsbParticalEffectItem.Text = "Partical Effect Item";
            this.tsbParticalEffectItem.Click += new System.EventHandler(this.tsbParticalEffectItem_Click);
            // 
            // tsbLightItem
            // 
            this.tsbLightItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLightItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDirectionalLight,
            this.tsbPointLight,
            this.tsbSpotLight});
            this.tsbLightItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbLightItem.Image")));
            this.tsbLightItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLightItem.Name = "tsbLightItem";
            this.tsbLightItem.Size = new System.Drawing.Size(40, 28);
            this.tsbLightItem.Text = "Light Item";
            // 
            // tsbDirectionalLight
            // 
            this.tsbDirectionalLight.Image = ((System.Drawing.Image)(resources.GetObject("tsbDirectionalLight.Image")));
            this.tsbDirectionalLight.Name = "tsbDirectionalLight";
            this.tsbDirectionalLight.Size = new System.Drawing.Size(161, 22);
            this.tsbDirectionalLight.Text = "Directional Light";
            this.tsbDirectionalLight.ToolTipText = "Directional Light";
            this.tsbDirectionalLight.Click += new System.EventHandler(this.tsbDirectionalLight_Click);
            // 
            // tsbPointLight
            // 
            this.tsbPointLight.Image = ((System.Drawing.Image)(resources.GetObject("tsbPointLight.Image")));
            this.tsbPointLight.Name = "tsbPointLight";
            this.tsbPointLight.Size = new System.Drawing.Size(161, 22);
            this.tsbPointLight.Text = "Point Light";
            this.tsbPointLight.ToolTipText = "Point Light";
            this.tsbPointLight.Click += new System.EventHandler(this.tsbPointLight_Click);
            // 
            // tsbSpotLight
            // 
            this.tsbSpotLight.Image = ((System.Drawing.Image)(resources.GetObject("tsbSpotLight.Image")));
            this.tsbSpotLight.Name = "tsbSpotLight";
            this.tsbSpotLight.Size = new System.Drawing.Size(161, 22);
            this.tsbSpotLight.Text = "Spot Light";
            this.tsbSpotLight.ToolTipText = "Spot Light";
            this.tsbSpotLight.Click += new System.EventHandler(this.tsbSpotLight_Click);
            // 
            // tsbProjectorItem
            // 
            this.tsbProjectorItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProjectorItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbProjectorItem.Image")));
            this.tsbProjectorItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProjectorItem.Name = "tsbProjectorItem";
            this.tsbProjectorItem.Size = new System.Drawing.Size(28, 28);
            this.tsbProjectorItem.Text = "Projector Item";
            this.tsbProjectorItem.Click += new System.EventHandler(this.tsbProjectorItem_Click);
            // 
            // tsbSoundItem
            // 
            this.tsbSoundItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSoundItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbSoundItem.Image")));
            this.tsbSoundItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSoundItem.Name = "tsbSoundItem";
            this.tsbSoundItem.Size = new System.Drawing.Size(28, 28);
            this.tsbSoundItem.Text = "Sound Item";
            this.tsbSoundItem.Click += new System.EventHandler(this.tsbSoundItem_Click);
            // 
            // tsEditMode
            // 
            this.tsEditMode.BackColor = System.Drawing.SystemColors.Menu;
            this.tsEditMode.Dock = System.Windows.Forms.DockStyle.None;
            this.tsEditMode.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsEditMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbEditObject,
            this.tsbInitObject,
            this.tsbEditModeProperties});
            this.tsEditMode.Location = new System.Drawing.Point(440, 0);
            this.tsEditMode.Name = "tsEditMode";
            this.tsEditMode.Size = new System.Drawing.Size(96, 31);
            this.tsEditMode.TabIndex = 12;
            this.tsEditMode.Text = "Edit Mode Panel";
            // 
            // tsbEditObject
            // 
            this.tsbEditObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditObject.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditObject.Image")));
            this.tsbEditObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditObject.Name = "tsbEditObject";
            this.tsbEditObject.Size = new System.Drawing.Size(28, 28);
            this.tsbEditObject.Text = "Edit Object";
            this.tsbEditObject.Click += new System.EventHandler(this.tsbEditObject_Click);
            // 
            // tsbInitObject
            // 
            this.tsbInitObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInitObject.Image = ((System.Drawing.Image)(resources.GetObject("tsbInitObject.Image")));
            this.tsbInitObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInitObject.Name = "tsbInitObject";
            this.tsbInitObject.Size = new System.Drawing.Size(28, 28);
            this.tsbInitObject.Text = "Init Object";
            this.tsbInitObject.Click += new System.EventHandler(this.tsbInitObject_Click);
            // 
            // tsbEditModeProperties
            // 
            this.tsbEditModeProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEditModeProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditModeProperties.Image")));
            this.tsbEditModeProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditModeProperties.Name = "tsbEditModeProperties";
            this.tsbEditModeProperties.Size = new System.Drawing.Size(28, 28);
            this.tsbEditModeProperties.Text = "Edit Mode Properties";
            this.tsbEditModeProperties.Click += new System.EventHandler(this.tsbEditModeProperties_Click);
            // 
            // tsAttendantTools
            // 
            this.tsAttendantTools.BackColor = System.Drawing.SystemColors.Menu;
            this.tsAttendantTools.Dock = System.Windows.Forms.DockStyle.None;
            this.tsAttendantTools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsAttendantTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMapProductionTool,
            this.toolStripSeparator14,
            this.tsbPerformanceTester,
            this.tsbStressTester,
            this.toolStripSeparator15,
            this.tsbObjectLoader,
            this.tsbObjectAutoAnimatation,
            this.tsbAnimationState});
            this.tsAttendantTools.Location = new System.Drawing.Point(536, 0);
            this.tsAttendantTools.Name = "tsAttendantTools";
            this.tsAttendantTools.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tsAttendantTools.Size = new System.Drawing.Size(249, 31);
            this.tsAttendantTools.TabIndex = 13;
            this.tsAttendantTools.Text = "Attendant Tools Panel";
            // 
            // tsbMapProductionTool
            // 
            this.tsbMapProductionTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapProductionTool.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapProductionTool.Image")));
            this.tsbMapProductionTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapProductionTool.Name = "tsbMapProductionTool";
            this.tsbMapProductionTool.Size = new System.Drawing.Size(28, 28);
            this.tsbMapProductionTool.Text = "Map Production Tool";
            this.tsbMapProductionTool.Click += new System.EventHandler(this.tsbMapProductionTool_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbPerformanceTester
            // 
            this.tsbPerformanceTester.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPerformanceTester.Image = ((System.Drawing.Image)(resources.GetObject("tsbPerformanceTester.Image")));
            this.tsbPerformanceTester.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPerformanceTester.Name = "tsbPerformanceTester";
            this.tsbPerformanceTester.Size = new System.Drawing.Size(28, 28);
            this.tsbPerformanceTester.Text = "Performance Tester";
            this.tsbPerformanceTester.Click += new System.EventHandler(this.tsbPerformanceTester_Click);
            // 
            // tsbStressTester
            // 
            this.tsbStressTester.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStressTester.Image = ((System.Drawing.Image)(resources.GetObject("tsbStressTester.Image")));
            this.tsbStressTester.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStressTester.Name = "tsbStressTester";
            this.tsbStressTester.Size = new System.Drawing.Size(28, 28);
            this.tsbStressTester.Text = "Stress Tester";
            this.tsbStressTester.Click += new System.EventHandler(this.tsbStressTester_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbObjectLoader
            // 
            this.tsbObjectLoader.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbObjectLoader.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjectLoader.Image")));
            this.tsbObjectLoader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjectLoader.Name = "tsbObjectLoader";
            this.tsbObjectLoader.Size = new System.Drawing.Size(85, 28);
            this.tsbObjectLoader.Text = "Object Loader";
            this.tsbObjectLoader.Click += new System.EventHandler(this.tsbObjectLoader_Click);
            // 
            // tsbObjectAutoAnimatation
            // 
            this.tsbObjectAutoAnimatation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbObjectAutoAnimatation.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjectAutoAnimatation.Image")));
            this.tsbObjectAutoAnimatation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjectAutoAnimatation.Name = "tsbObjectAutoAnimatation";
            this.tsbObjectAutoAnimatation.Size = new System.Drawing.Size(28, 28);
            this.tsbObjectAutoAnimatation.Text = "Object Auto Animatation";
            this.tsbObjectAutoAnimatation.Click += new System.EventHandler(this.tsbObjectAutoAnimatation_Click);
            // 
            // tsbAnimationState
            // 
            this.tsbAnimationState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAnimationState.Image = ((System.Drawing.Image)(resources.GetObject("tsbAnimationState.Image")));
            this.tsbAnimationState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAnimationState.Name = "tsbAnimationState";
            this.tsbAnimationState.Size = new System.Drawing.Size(28, 28);
            this.tsbAnimationState.Text = "Animation State";
            this.tsbAnimationState.Click += new System.EventHandler(this.tsbAnimationState_Click);
            // 
            // tsCalculations
            // 
            this.tsCalculations.BackColor = System.Drawing.SystemColors.Menu;
            this.tsCalculations.Dock = System.Windows.Forms.DockStyle.None;
            this.tsCalculations.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsCalculations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbGeographicCalculations,
            this.tsbGeometricCalculations,
            this.tsbStandaloneSQ});
            this.tsCalculations.Location = new System.Drawing.Point(712, 0);
            this.tsCalculations.Name = "tsCalculations";
            this.tsCalculations.Size = new System.Drawing.Size(314, 31);
            this.tsCalculations.TabIndex = 15;
            this.tsCalculations.Text = "Calculations Panel";
            // 
            // tsbGeographicCalculations
            // 
            this.tsbGeographicCalculations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbGeographicCalculations.Image = ((System.Drawing.Image)(resources.GetObject("tsbGeographicCalculations.Image")));
            this.tsbGeographicCalculations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGeographicCalculations.Name = "tsbGeographicCalculations";
            this.tsbGeographicCalculations.Size = new System.Drawing.Size(140, 28);
            this.tsbGeographicCalculations.Text = "Geographic Calculations";
            this.tsbGeographicCalculations.Click += new System.EventHandler(this.tsbGeographicCalculations_Click);
            // 
            // tsbGeometricCalculations
            // 
            this.tsbGeometricCalculations.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbGeometricCalculations.Image = ((System.Drawing.Image)(resources.GetObject("tsbGeometricCalculations.Image")));
            this.tsbGeometricCalculations.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGeometricCalculations.Name = "tsbGeometricCalculations";
            this.tsbGeometricCalculations.Size = new System.Drawing.Size(134, 28);
            this.tsbGeometricCalculations.Text = "Geometric Calculations";
            this.tsbGeometricCalculations.Click += new System.EventHandler(this.tsbGeometricCalculations_Click);
            // 
            // tsbStandaloneSQ
            // 
            this.tsbStandaloneSQ.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStandaloneSQ.Image = ((System.Drawing.Image)(resources.GetObject("tsbStandaloneSQ.Image")));
            this.tsbStandaloneSQ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStandaloneSQ.Name = "tsbStandaloneSQ";
            this.tsbStandaloneSQ.Size = new System.Drawing.Size(28, 28);
            this.tsbStandaloneSQ.Text = "Spatial Queries";
            this.tsbStandaloneSQ.Click += new System.EventHandler(this.tsbStandaloneSQ_Click);
            // 
            // tsExtendedEditMode
            // 
            this.tsExtendedEditMode.BackColor = System.Drawing.SystemColors.Menu;
            this.tsExtendedEditMode.Dock = System.Windows.Forms.DockStyle.None;
            this.tsExtendedEditMode.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsExtendedEditMode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDistanceDirectionMeasure,
            this.tsbDynamicZoom,
            this.toolStripSeparator13,
            this.tsbEditModeNavigation,
            this.tsbExitCurrentAction,
            this.tsbCheckRenderNeeded,
            this.tsbEventCallBack,
            this.tsbNavPath,
            this.tsbSightEllipseItem,
            this.tsbCollectGC});
            this.tsExtendedEditMode.Location = new System.Drawing.Point(785, 0);
            this.tsExtendedEditMode.Name = "tsExtendedEditMode";
            this.tsExtendedEditMode.Size = new System.Drawing.Size(538, 31);
            this.tsExtendedEditMode.TabIndex = 16;
            // 
            // tsbDistanceDirectionMeasure
            // 
            this.tsbDistanceDirectionMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDistanceDirectionMeasure.Image = ((System.Drawing.Image)(resources.GetObject("tsbDistanceDirectionMeasure.Image")));
            this.tsbDistanceDirectionMeasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDistanceDirectionMeasure.Name = "tsbDistanceDirectionMeasure";
            this.tsbDistanceDirectionMeasure.Size = new System.Drawing.Size(28, 28);
            this.tsbDistanceDirectionMeasure.Text = "P2P";
            this.tsbDistanceDirectionMeasure.ToolTipText = "Distance Direction Measure";
            this.tsbDistanceDirectionMeasure.Click += new System.EventHandler(this.tsbDistanceDirectionMeasure_Click);
            // 
            // tsbDynamicZoom
            // 
            this.tsbDynamicZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDynamicZoom.Image = ((System.Drawing.Image)(resources.GetObject("tsbDynamicZoom.Image")));
            this.tsbDynamicZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDynamicZoom.Name = "tsbDynamicZoom";
            this.tsbDynamicZoom.Size = new System.Drawing.Size(28, 28);
            this.tsbDynamicZoom.Text = "Dynamic Zoom";
            this.tsbDynamicZoom.Click += new System.EventHandler(this.tsbDynamicZoom_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbEditModeNavigation
            // 
            this.tsbEditModeNavigation.CheckOnClick = true;
            this.tsbEditModeNavigation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEditModeNavigation.Image = ((System.Drawing.Image)(resources.GetObject("tsbEditModeNavigation.Image")));
            this.tsbEditModeNavigation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEditModeNavigation.Name = "tsbEditModeNavigation";
            this.tsbEditModeNavigation.Size = new System.Drawing.Size(126, 28);
            this.tsbEditModeNavigation.Text = "Edit Mode Navigation";
            this.tsbEditModeNavigation.Click += new System.EventHandler(this.tsbEditModeNavigation_Click);
            // 
            // tsbExitCurrentAction
            // 
            this.tsbExitCurrentAction.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExitCurrentAction.Image = ((System.Drawing.Image)(resources.GetObject("tsbExitCurrentAction.Image")));
            this.tsbExitCurrentAction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExitCurrentAction.Name = "tsbExitCurrentAction";
            this.tsbExitCurrentAction.Size = new System.Drawing.Size(28, 28);
            this.tsbExitCurrentAction.Text = "Exit Current Action";
            this.tsbExitCurrentAction.Click += new System.EventHandler(this.tsbExitCurrentAction_Click);
            // 
            // tsbCheckRenderNeeded
            // 
            this.tsbCheckRenderNeeded.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCheckRenderNeeded.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheckRenderNeeded.Image")));
            this.tsbCheckRenderNeeded.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheckRenderNeeded.Name = "tsbCheckRenderNeeded";
            this.tsbCheckRenderNeeded.Size = new System.Drawing.Size(128, 28);
            this.tsbCheckRenderNeeded.Text = "Check Render Needed";
            this.tsbCheckRenderNeeded.Click += new System.EventHandler(this.tsbCheckRenderNeeded_Click);
            // 
            // tsbEventCallBack
            // 
            this.tsbEventCallBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEventCallBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbEventCallBack.Image")));
            this.tsbEventCallBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEventCallBack.Name = "tsbEventCallBack";
            this.tsbEventCallBack.Size = new System.Drawing.Size(88, 28);
            this.tsbEventCallBack.Text = "Event CallBack";
            this.tsbEventCallBack.Click += new System.EventHandler(this.tsbEventCallBack_Click);
            // 
            // tsbNavPath
            // 
            this.tsbNavPath.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNavPath.Image = ((System.Drawing.Image)(resources.GetObject("tsbNavPath.Image")));
            this.tsbNavPath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNavPath.Name = "tsbNavPath";
            this.tsbNavPath.Size = new System.Drawing.Size(28, 28);
            this.tsbNavPath.Text = "Nav Path";
            this.tsbNavPath.Click += new System.EventHandler(this.tsbNavPath_Click);
            // 
            // tsbSightEllipseItem
            // 
            this.tsbSightEllipseItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSightEllipseItem.Image = ((System.Drawing.Image)(resources.GetObject("tsbSightEllipseItem.Image")));
            this.tsbSightEllipseItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSightEllipseItem.Name = "tsbSightEllipseItem";
            this.tsbSightEllipseItem.Size = new System.Drawing.Size(38, 28);
            this.tsbSightEllipseItem.Text = "Sight";
            this.tsbSightEllipseItem.Click += new System.EventHandler(this.tsbSightEllipseItem_Click);
            // 
            // tsbCollectGC
            // 
            this.tsbCollectGC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCollectGC.Image = ((System.Drawing.Image)(resources.GetObject("tsbCollectGC.Image")));
            this.tsbCollectGC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollectGC.Name = "tsbCollectGC";
            this.tsbCollectGC.Size = new System.Drawing.Size(28, 28);
            this.tsbCollectGC.ToolTipText = "Collect to Garbage Collector";
            this.tsbCollectGC.Click += new System.EventHandler(this.tsbCollectGC_Click);
            // 
            // tsbSaveViewportToSession
            // 
            this.tsbSaveViewportToSession.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveViewportToSession.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveViewportToSession.Image")));
            this.tsbSaveViewportToSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveViewportToSession.Name = "tsbSaveViewportToSession";
            this.tsbSaveViewportToSession.Size = new System.Drawing.Size(28, 28);
            this.tsbSaveViewportToSession.Text = "Save Session To Folder";
            this.tsbSaveViewportToSession.Click += new System.EventHandler(this.tsbSaveViewportToSession_Click);
            // 
            // tsbLoadViewport
            // 
            this.tsbLoadViewport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoadViewport.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadViewport.Image")));
            this.tsbLoadViewport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadViewport.Name = "tsbLoadViewport";
            this.tsbLoadViewport.Size = new System.Drawing.Size(28, 28);
            this.tsbLoadViewport.ToolTipText = "Load Session From Folder";
            this.tsbLoadViewport.Click += new System.EventHandler(this.tsbLoadViewport_Click);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // MainStatusBarStatistics
            // 
            this.MainStatusBarStatistics.BackColor = System.Drawing.SystemColors.Menu;
            this.MainStatusBarStatistics.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainStatusBarStatistics.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbLastFPS,
            this.sbLastFPSBox,
            this.sbAvgFPS,
            this.sbAvgFPSBox,
            this.sbBestFPS,
            this.sbBestFPSBox,
            this.sbWorstFPS,
            this.sbWorstFPSBox,
            this.sbNumLastFrameTriangles,
            this.sbNumLastFrameTrianglesBox,
            this.sbNumLastFrameBatches,
            this.sbNumLastFrameBatchesBox});
            this.MainStatusBarStatistics.Location = new System.Drawing.Point(0, 409);
            this.MainStatusBarStatistics.Name = "MainStatusBarStatistics";
            this.MainStatusBarStatistics.Size = new System.Drawing.Size(1256, 24);
            this.MainStatusBarStatistics.TabIndex = 6;
            this.MainStatusBarStatistics.Text = "statusStrip1";
            this.MainStatusBarStatistics.Visible = false;
            // 
            // sbLastFPS
            // 
            this.sbLastFPS.Name = "sbLastFPS";
            this.sbLastFPS.Size = new System.Drawing.Size(53, 19);
            this.sbLastFPS.Text = "Last FPS:";
            // 
            // sbLastFPSBox
            // 
            this.sbLastFPSBox.AutoSize = false;
            this.sbLastFPSBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbLastFPSBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbLastFPSBox.Name = "sbLastFPSBox";
            this.sbLastFPSBox.Size = new System.Drawing.Size(80, 19);
            // 
            // sbAvgFPS
            // 
            this.sbAvgFPS.Name = "sbAvgFPS";
            this.sbAvgFPS.Size = new System.Drawing.Size(75, 19);
            this.sbAvgFPS.Text = "Average FPS:";
            // 
            // sbAvgFPSBox
            // 
            this.sbAvgFPSBox.AutoSize = false;
            this.sbAvgFPSBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbAvgFPSBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbAvgFPSBox.Name = "sbAvgFPSBox";
            this.sbAvgFPSBox.Size = new System.Drawing.Size(80, 19);
            // 
            // sbBestFPS
            // 
            this.sbBestFPS.Name = "sbBestFPS";
            this.sbBestFPS.Size = new System.Drawing.Size(54, 19);
            this.sbBestFPS.Text = "Best FPS:";
            // 
            // sbBestFPSBox
            // 
            this.sbBestFPSBox.AutoSize = false;
            this.sbBestFPSBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbBestFPSBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbBestFPSBox.Name = "sbBestFPSBox";
            this.sbBestFPSBox.Size = new System.Drawing.Size(80, 19);
            // 
            // sbWorstFPS
            // 
            this.sbWorstFPS.Name = "sbWorstFPS";
            this.sbWorstFPS.Size = new System.Drawing.Size(63, 19);
            this.sbWorstFPS.Text = "Worst FPS:";
            // 
            // sbWorstFPSBox
            // 
            this.sbWorstFPSBox.AutoSize = false;
            this.sbWorstFPSBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbWorstFPSBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbWorstFPSBox.Name = "sbWorstFPSBox";
            this.sbWorstFPSBox.Size = new System.Drawing.Size(80, 19);
            // 
            // sbNumLastFrameTriangles
            // 
            this.sbNumLastFrameTriangles.Name = "sbNumLastFrameTriangles";
            this.sbNumLastFrameTriangles.Size = new System.Drawing.Size(146, 19);
            this.sbNumLastFrameTriangles.Text = "Num Last Frame Triangles:";
            // 
            // sbNumLastFrameTrianglesBox
            // 
            this.sbNumLastFrameTrianglesBox.AutoSize = false;
            this.sbNumLastFrameTrianglesBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbNumLastFrameTrianglesBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbNumLastFrameTrianglesBox.Name = "sbNumLastFrameTrianglesBox";
            this.sbNumLastFrameTrianglesBox.Size = new System.Drawing.Size(80, 19);
            // 
            // sbNumLastFrameBatches
            // 
            this.sbNumLastFrameBatches.Name = "sbNumLastFrameBatches";
            this.sbNumLastFrameBatches.Size = new System.Drawing.Size(144, 19);
            this.sbNumLastFrameBatches.Text = "Num Last Frame Batches: ";
            // 
            // sbNumLastFrameBatchesBox
            // 
            this.sbNumLastFrameBatchesBox.AutoSize = false;
            this.sbNumLastFrameBatchesBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbNumLastFrameBatchesBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbNumLastFrameBatchesBox.Name = "sbNumLastFrameBatchesBox";
            this.sbNumLastFrameBatchesBox.Size = new System.Drawing.Size(80, 19);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.AutoScroll = true;
            this.ContentPanel.Size = new System.Drawing.Size(1195, 176);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Menu;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewMenu,
            this.windowsMenu,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1358, 24);
            this.menuStrip.TabIndex = 17;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator6,
            this.toolStripMenuItem5});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(143, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem5.Text = "E&xit";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(44, 20);
            this.viewMenu.Text = "&View";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalToolStripMenuItem,
            this.mapOperationToolStripMenuItem,
            this.managmentsToolStripMenuItem,
            this.editModeToolStripMenuItem,
            this.extendedEditModeToolStripMenuItem,
            this.symbolicItemsToolStripMenuItem,
            this.physicalItemsToolStripMenuItem,
            this.calculationsToolStripMenuItem,
            this.attendantToolsToolStripMenuItem,
            this.vectorialToolStripMenuItem});
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.toolBarToolStripMenuItem.Text = "&Toolbar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.toolBarToolStripMenuItem_Click);
            // 
            // generalToolStripMenuItem
            // 
            this.generalToolStripMenuItem.Checked = true;
            this.generalToolStripMenuItem.CheckOnClick = true;
            this.generalToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            this.generalToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.generalToolStripMenuItem.Text = "General";
            this.generalToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.generalToolStripMenuItem_CheckStateChanged);
            // 
            // mapOperationToolStripMenuItem
            // 
            this.mapOperationToolStripMenuItem.Checked = true;
            this.mapOperationToolStripMenuItem.CheckOnClick = true;
            this.mapOperationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mapOperationToolStripMenuItem.Name = "mapOperationToolStripMenuItem";
            this.mapOperationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mapOperationToolStripMenuItem.Text = "Map Operation";
            this.mapOperationToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.mapOperationToolStripMenuItem_CheckStateChanged);
            // 
            // managmentsToolStripMenuItem
            // 
            this.managmentsToolStripMenuItem.Checked = true;
            this.managmentsToolStripMenuItem.CheckOnClick = true;
            this.managmentsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.managmentsToolStripMenuItem.Name = "managmentsToolStripMenuItem";
            this.managmentsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.managmentsToolStripMenuItem.Text = "Managments";
            this.managmentsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.managmentsToolStripMenuItem_CheckStateChanged);
            // 
            // editModeToolStripMenuItem
            // 
            this.editModeToolStripMenuItem.Checked = true;
            this.editModeToolStripMenuItem.CheckOnClick = true;
            this.editModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.editModeToolStripMenuItem.Name = "editModeToolStripMenuItem";
            this.editModeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editModeToolStripMenuItem.Text = "Edit Mode";
            this.editModeToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.editModeToolStripMenuItem_CheckStateChanged);
            // 
            // extendedEditModeToolStripMenuItem
            // 
            this.extendedEditModeToolStripMenuItem.Checked = true;
            this.extendedEditModeToolStripMenuItem.CheckOnClick = true;
            this.extendedEditModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.extendedEditModeToolStripMenuItem.Name = "extendedEditModeToolStripMenuItem";
            this.extendedEditModeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extendedEditModeToolStripMenuItem.Text = "Extended Edit Mode";
            this.extendedEditModeToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.extendedEditModeToolStripMenuItem_CheckStateChanged);
            // 
            // symbolicItemsToolStripMenuItem
            // 
            this.symbolicItemsToolStripMenuItem.Checked = true;
            this.symbolicItemsToolStripMenuItem.CheckOnClick = true;
            this.symbolicItemsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.symbolicItemsToolStripMenuItem.Name = "symbolicItemsToolStripMenuItem";
            this.symbolicItemsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.symbolicItemsToolStripMenuItem.Text = "Symbolic Items";
            this.symbolicItemsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.symbolicItemsToolStripMenuItem_CheckStateChanged);
            // 
            // physicalItemsToolStripMenuItem
            // 
            this.physicalItemsToolStripMenuItem.Checked = true;
            this.physicalItemsToolStripMenuItem.CheckOnClick = true;
            this.physicalItemsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.physicalItemsToolStripMenuItem.Name = "physicalItemsToolStripMenuItem";
            this.physicalItemsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.physicalItemsToolStripMenuItem.Text = "Physical Items";
            this.physicalItemsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.physicalItemsToolStripMenuItem_CheckStateChanged);
            // 
            // calculationsToolStripMenuItem
            // 
            this.calculationsToolStripMenuItem.Checked = true;
            this.calculationsToolStripMenuItem.CheckOnClick = true;
            this.calculationsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.calculationsToolStripMenuItem.Name = "calculationsToolStripMenuItem";
            this.calculationsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.calculationsToolStripMenuItem.Text = "Calculations";
            this.calculationsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.calculationsToolStripMenuItem_CheckStateChanged);
            // 
            // attendantToolsToolStripMenuItem
            // 
            this.attendantToolsToolStripMenuItem.Checked = true;
            this.attendantToolsToolStripMenuItem.CheckOnClick = true;
            this.attendantToolsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.attendantToolsToolStripMenuItem.Name = "attendantToolsToolStripMenuItem";
            this.attendantToolsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.attendantToolsToolStripMenuItem.Text = "AttendantTools";
            this.attendantToolsToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.attendantToolsToolStripMenuItem_CheckStateChanged);
            // 
            // vectorialToolStripMenuItem
            // 
            this.vectorialToolStripMenuItem.Checked = true;
            this.vectorialToolStripMenuItem.CheckOnClick = true;
            this.vectorialToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vectorialToolStripMenuItem.Name = "vectorialToolStripMenuItem";
            this.vectorialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vectorialToolStripMenuItem.Text = "Vectorial";
            this.vectorialToolStripMenuItem.CheckStateChanged += new System.EventHandler(this.vectorialToolStripMenuItem_CheckStateChanged);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.statusBarToolStripMenuItem.Text = "&Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(68, 20);
            this.windowsMenu.Text = "&Windows";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.tileVerticalToolStripMenuItem.Text = "Tile &Vertical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.closeAllToolStripMenuItem.Text = "C&lose All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadAndSaveSchemesToolStripMenuItem,
            this.mosaicsCreatorToolStripMenuItem,
            this.rollingShutterToolStripMenuItem,
            this.pointingFingerDBGeneratorToolStripMenuItem,
            this.vectorXMLCreatorToolStripMenuItem,
            this.signToSecurityServerToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // LoadAndSaveSchemesToolStripMenuItem
            // 
            this.LoadAndSaveSchemesToolStripMenuItem.Name = "LoadAndSaveSchemesToolStripMenuItem";
            this.LoadAndSaveSchemesToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.LoadAndSaveSchemesToolStripMenuItem.Text = "Load and Save Schemes";
            this.LoadAndSaveSchemesToolStripMenuItem.Click += new System.EventHandler(this.LoadAndSaveSchemes_Click);
            // 
            // mosaicsCreatorToolStripMenuItem
            // 
            this.mosaicsCreatorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("mosaicsCreatorToolStripMenuItem.Image")));
            this.mosaicsCreatorToolStripMenuItem.Name = "mosaicsCreatorToolStripMenuItem";
            this.mosaicsCreatorToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.mosaicsCreatorToolStripMenuItem.Text = "Mosaics Creator";
            this.mosaicsCreatorToolStripMenuItem.Click += new System.EventHandler(this.mosaicsCreatorToolStripMenuItem_Click);
            // 
            // rollingShutterToolStripMenuItem
            // 
            this.rollingShutterToolStripMenuItem.Name = "rollingShutterToolStripMenuItem";
            this.rollingShutterToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.rollingShutterToolStripMenuItem.Text = "Rolling Shutter";
            this.rollingShutterToolStripMenuItem.Click += new System.EventHandler(this.rollingShutterToolStripMenuItem_Click);
            // 
            // pointingFingerDBGeneratorToolStripMenuItem
            // 
            this.pointingFingerDBGeneratorToolStripMenuItem.Name = "pointingFingerDBGeneratorToolStripMenuItem";
            this.pointingFingerDBGeneratorToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.pointingFingerDBGeneratorToolStripMenuItem.Text = "Pointing Finger DB Generator";
            this.pointingFingerDBGeneratorToolStripMenuItem.Click += new System.EventHandler(this.pointingFingerDBGeneratorToolStripMenuItem_Click);
            // 
            // vectorXMLCreatorToolStripMenuItem
            // 
            this.vectorXMLCreatorToolStripMenuItem.Name = "vectorXMLCreatorToolStripMenuItem";
            this.vectorXMLCreatorToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.vectorXMLCreatorToolStripMenuItem.Text = "Vector Production";
            this.vectorXMLCreatorToolStripMenuItem.Click += new System.EventHandler(this.vectorXMLCreatorToolStripMenuItem_Click);
            // 
            // signToSecurityServerToolStripMenuItem
            // 
            this.signToSecurityServerToolStripMenuItem.Name = "signToSecurityServerToolStripMenuItem";
            this.signToSecurityServerToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.signToSecurityServerToolStripMenuItem.Text = "Sign to Security Server";
            this.signToSecurityServerToolStripMenuItem.Click += new System.EventHandler(this.signToSecurityServerToolStripMenuItem_Click);
            // 
            // sbWorldCoordX
            // 
            this.sbWorldCoordX.Name = "sbWorldCoordX";
            this.sbWorldCoordX.Size = new System.Drawing.Size(17, 20);
            this.sbWorldCoordX.Text = "X:";
            // 
            // sbWorldCoordXBox
            // 
            this.sbWorldCoordXBox.AutoSize = false;
            this.sbWorldCoordXBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbWorldCoordXBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbWorldCoordXBox.Name = "sbWorldCoordXBox";
            this.sbWorldCoordXBox.Size = new System.Drawing.Size(80, 20);
            // 
            // sbWorldCoordY
            // 
            this.sbWorldCoordY.Name = "sbWorldCoordY";
            this.sbWorldCoordY.Size = new System.Drawing.Size(17, 20);
            this.sbWorldCoordY.Text = "Y:";
            // 
            // sbWorldCoordYBox
            // 
            this.sbWorldCoordYBox.AutoSize = false;
            this.sbWorldCoordYBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbWorldCoordYBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbWorldCoordYBox.Name = "sbWorldCoordYBox";
            this.sbWorldCoordYBox.Size = new System.Drawing.Size(80, 20);
            // 
            // sbWorldCoordZ
            // 
            this.sbWorldCoordZ.Name = "sbWorldCoordZ";
            this.sbWorldCoordZ.Size = new System.Drawing.Size(17, 20);
            this.sbWorldCoordZ.Text = "Z:";
            // 
            // sbWorldCoordZBox
            // 
            this.sbWorldCoordZBox.AutoSize = false;
            this.sbWorldCoordZBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbWorldCoordZBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbWorldCoordZBox.Name = "sbWorldCoordZBox";
            this.sbWorldCoordZBox.Size = new System.Drawing.Size(60, 20);
            // 
            // sbScreenCoord
            // 
            this.sbScreenCoord.Name = "sbScreenCoord";
            this.sbScreenCoord.Size = new System.Drawing.Size(45, 20);
            this.sbScreenCoord.Text = "Screen:";
            // 
            // sbScreenCoordBox
            // 
            this.sbScreenCoordBox.AutoSize = false;
            this.sbScreenCoordBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbScreenCoordBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbScreenCoordBox.Name = "sbScreenCoordBox";
            this.sbScreenCoordBox.Size = new System.Drawing.Size(70, 20);
            // 
            // sbImageCoord
            // 
            this.sbImageCoord.Name = "sbImageCoord";
            this.sbImageCoord.Size = new System.Drawing.Size(43, 20);
            this.sbImageCoord.Text = "Image:";
            // 
            // sbImageCoordBox
            // 
            this.sbImageCoordBox.AutoSize = false;
            this.sbImageCoordBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbImageCoordBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbImageCoordBox.Name = "sbImageCoordBox";
            this.sbImageCoordBox.Size = new System.Drawing.Size(70, 20);
            // 
            // sbViewportID
            // 
            this.sbViewportID.Name = "sbViewportID";
            this.sbViewportID.Size = new System.Drawing.Size(71, 20);
            this.sbViewportID.Text = "Viewport ID:";
            // 
            // sbViewportIDBox
            // 
            this.sbViewportIDBox.AutoSize = false;
            this.sbViewportIDBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbViewportIDBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbViewportIDBox.Name = "sbViewportIDBox";
            this.sbViewportIDBox.Size = new System.Drawing.Size(30, 20);
            // 
            // sbScale
            // 
            this.sbScale.Name = "sbScale";
            this.sbScale.Size = new System.Drawing.Size(81, 20);
            this.sbScale.Text = "Camera Scale:";
            // 
            // sbScaleBox
            // 
            this.sbScaleBox.AutoSize = false;
            this.sbScaleBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbScaleBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbScaleBox.Name = "sbScaleBox";
            this.sbScaleBox.Size = new System.Drawing.Size(80, 20);
            // 
            // sbMapScale
            // 
            this.sbMapScale.Name = "sbMapScale";
            this.sbMapScale.Size = new System.Drawing.Size(64, 20);
            this.sbMapScale.Text = "Map Scale:";
            // 
            // sbMapScaleBox
            // 
            this.sbMapScaleBox.AutoSize = false;
            this.sbMapScaleBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbMapScaleBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbMapScaleBox.Name = "sbMapScaleBox";
            this.sbMapScaleBox.Size = new System.Drawing.Size(80, 20);
            // 
            // sbAverageFPS
            // 
            this.sbAverageFPS.Name = "sbAverageFPS";
            this.sbAverageFPS.Size = new System.Drawing.Size(53, 20);
            this.sbAverageFPS.Text = "Avg FPS:";
            // 
            // sbAvrFPSBox
            // 
            this.sbAvrFPSBox.AutoSize = false;
            this.sbAvrFPSBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbAvrFPSBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbAvrFPSBox.Name = "sbAvrFPSBox";
            this.sbAvrFPSBox.Size = new System.Drawing.Size(35, 20);
            // 
            // sbMsg
            // 
            this.sbMsg.Name = "sbMsg";
            this.sbMsg.Size = new System.Drawing.Size(33, 20);
            this.sbMsg.Text = "Msg:";
            // 
            // sbMsgBox
            // 
            this.sbMsgBox.AutoSize = false;
            this.sbMsgBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.sbMsgBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sbMsgBox.Name = "sbMsgBox";
            this.sbMsgBox.Size = new System.Drawing.Size(317, 20);
            this.sbMsgBox.Spring = true;
            // 
            // MainStatusBar
            // 
            this.MainStatusBar.BackColor = System.Drawing.SystemColors.Menu;
            this.MainStatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbWorldCoordX,
            this.sbWorldCoordXBox,
            this.sbWorldCoordY,
            this.sbWorldCoordYBox,
            this.sbWorldCoordZ,
            this.sbWorldCoordZBox,
            this.sbScreenCoord,
            this.sbScreenCoordBox,
            this.sbImageCoord,
            this.sbImageCoordBox,
            this.sbViewportID,
            this.sbViewportIDBox,
            this.sbScale,
            this.sbScaleBox,
            this.sbMapScale,
            this.sbMapScaleBox,
            this.sbAverageFPS,
            this.sbAvrFPSBox,
            this.sbMsg,
            this.sbMsgBox});
            this.MainStatusBar.Location = new System.Drawing.Point(0, 428);
            this.MainStatusBar.Name = "MainStatusBar";
            this.MainStatusBar.Size = new System.Drawing.Size(1358, 25);
            this.MainStatusBar.TabIndex = 2;
            this.MainStatusBar.Text = "StatusStrip";
            // 
            // toolStripPanel3
            // 
            this.toolStripPanel3.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStripPanel3.Controls.Add(this.tsDrawingSymbolicItems);
            this.toolStripPanel3.Controls.Add(this.tsDrawingPhysicalItems);
            this.toolStripPanel3.Controls.Add(this.tsEditMode);
            this.toolStripPanel3.Controls.Add(this.tsAttendantTools);
            this.toolStripPanel3.Controls.Add(this.tsExtendedEditMode);
            this.toolStripPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripPanel3.Location = new System.Drawing.Point(0, 55);
            this.toolStripPanel3.Name = "toolStripPanel3";
            this.toolStripPanel3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.toolStripPanel3.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripPanel3.Size = new System.Drawing.Size(1358, 31);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AccessibleName = "New item selection";
            this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.SystemColors.Menu;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.miniToolStrip.Location = new System.Drawing.Point(436, 3);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(439, 31);
            this.miniToolStrip.TabIndex = 7;
            // 
            // tsbMapWorld
            // 
            this.tsbMapWorld.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapWorld.Image")));
            this.tsbMapWorld.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapWorld.Name = "tsbMapWorld";
            this.tsbMapWorld.Size = new System.Drawing.Size(94, 28);
            this.tsbMapWorld.Text = "Map World";
            this.tsbMapWorld.Click += new System.EventHandler(this.tsbMapWorld_Click);
            // 
            // tsbObjectWorld
            // 
            this.tsbObjectWorld.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjectWorld.Image")));
            this.tsbObjectWorld.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjectWorld.Name = "tsbObjectWorld";
            this.tsbObjectWorld.Size = new System.Drawing.Size(105, 28);
            this.tsbObjectWorld.Text = "Object World";
            this.tsbObjectWorld.Click += new System.EventHandler(this.tsbObjectWorld_Click);
            // 
            // tsVectorial
            // 
            this.tsVectorial.BackColor = System.Drawing.SystemColors.Menu;
            this.tsVectorial.Dock = System.Windows.Forms.DockStyle.None;
            this.tsVectorial.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsVectorial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbVectorial,
            this.tsbPathFinder});
            this.tsVectorial.Location = new System.Drawing.Point(433, 0);
            this.tsVectorial.Name = "tsVectorial";
            this.tsVectorial.Size = new System.Drawing.Size(68, 31);
            this.tsVectorial.TabIndex = 14;
            this.tsVectorial.Text = "Vectorial Panel";
            // 
            // tsbVectorial
            // 
            this.tsbVectorial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbVectorial.Image = ((System.Drawing.Image)(resources.GetObject("tsbVectorial.Image")));
            this.tsbVectorial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbVectorial.Name = "tsbVectorial";
            this.tsbVectorial.Size = new System.Drawing.Size(28, 28);
            this.tsbVectorial.Text = "Vectorial";
            this.tsbVectorial.Click += new System.EventHandler(this.tsbVectorial_Click);
            // 
            // tsbPathFinder
            // 
            this.tsbPathFinder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPathFinder.Image = ((System.Drawing.Image)(resources.GetObject("tsbPathFinder.Image")));
            this.tsbPathFinder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPathFinder.Name = "tsbPathFinder";
            this.tsbPathFinder.Size = new System.Drawing.Size(28, 28);
            this.tsbPathFinder.Text = "Path Finder";
            this.tsbPathFinder.Click += new System.EventHandler(this.tsbPathFinder_Click);
            // 
            // tsMapOperations
            // 
            this.tsMapOperations.BackColor = System.Drawing.SystemColors.Menu;
            this.tsMapOperations.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMapOperations.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMapOperations.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNavigateMap,
            this.tsbZoomIn,
            this.tsbZoomOut,
            this.tsbCenterMap,
            this.toolStripSeparator9,
            this.tsbMapGrid,
            this.tsbMapHeightLines,
            this.tsbDtmVisualization,
            this.tsbDropDownFootprintPoints,
            this.tsbFileProductions,
            this.toolStripSeparator10,
            this.tsbPrint,
            this.tsbScan,
            this.tsbEnvironment,
            this.tsb2DTo3D});
            this.tsMapOperations.Location = new System.Drawing.Point(1026, 0);
            this.tsMapOperations.Name = "tsMapOperations";
            this.tsMapOperations.Size = new System.Drawing.Size(332, 31);
            this.tsMapOperations.TabIndex = 9;
            this.tsMapOperations.Text = "Map Operations Panel";
            // 
            // tsbNavigateMap
            // 
            this.tsbNavigateMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNavigateMap.Image = ((System.Drawing.Image)(resources.GetObject("tsbNavigateMap.Image")));
            this.tsbNavigateMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNavigateMap.Name = "tsbNavigateMap";
            this.tsbNavigateMap.Size = new System.Drawing.Size(28, 28);
            this.tsbNavigateMap.Text = "Navigate Map";
            this.tsbNavigateMap.Click += new System.EventHandler(this.tsbNavigateMap_Click);
            // 
            // tsbZoomIn
            // 
            this.tsbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomIn.Image")));
            this.tsbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomIn.Name = "tsbZoomIn";
            this.tsbZoomIn.Size = new System.Drawing.Size(28, 28);
            this.tsbZoomIn.Text = "Zoom In";
            this.tsbZoomIn.Click += new System.EventHandler(this.tsbZoomIn_Click);
            // 
            // tsbZoomOut
            // 
            this.tsbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("tsbZoomOut.Image")));
            this.tsbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZoomOut.Name = "tsbZoomOut";
            this.tsbZoomOut.Size = new System.Drawing.Size(28, 28);
            this.tsbZoomOut.Text = "Zoom Out";
            this.tsbZoomOut.Click += new System.EventHandler(this.tsbZoomOut_Click);
            // 
            // tsbCenterMap
            // 
            this.tsbCenterMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCenterMap.Image = ((System.Drawing.Image)(resources.GetObject("tsbCenterMap.Image")));
            this.tsbCenterMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCenterMap.Name = "tsbCenterMap";
            this.tsbCenterMap.Size = new System.Drawing.Size(28, 28);
            this.tsbCenterMap.Click += new System.EventHandler(this.tsbCenterMap_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbMapGrid
            // 
            this.tsbMapGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapGrid.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUTMMapGrid,
            this.tsbGeoMapGrid,
            this.tsbMGRSMapGrid,
            this.tsbNZMGMapGrid,
            this.tsbGeoRefMapGrid});
            this.tsbMapGrid.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapGrid.Image")));
            this.tsbMapGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbMapGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapGrid.Name = "tsbMapGrid";
            this.tsbMapGrid.Size = new System.Drawing.Size(37, 28);
            this.tsbMapGrid.Text = "Map Grid";
            this.tsbMapGrid.DropDownOpened += new System.EventHandler(this.tsbMapGrid_DropDownOpened);
            // 
            // tsbUTMMapGrid
            // 
            this.tsbUTMMapGrid.CheckOnClick = true;
            this.tsbUTMMapGrid.Name = "tsbUTMMapGrid";
            this.tsbUTMMapGrid.Size = new System.Drawing.Size(141, 22);
            this.tsbUTMMapGrid.Text = "UTM Grid";
            this.tsbUTMMapGrid.Click += new System.EventHandler(this.tsbUTMMapGrid_Click);
            // 
            // tsbGeoMapGrid
            // 
            this.tsbGeoMapGrid.CheckOnClick = true;
            this.tsbGeoMapGrid.Name = "tsbGeoMapGrid";
            this.tsbGeoMapGrid.Size = new System.Drawing.Size(141, 22);
            this.tsbGeoMapGrid.Text = "Geo Grid";
            this.tsbGeoMapGrid.Click += new System.EventHandler(this.tsbGeoMapGrid_Click);
            // 
            // tsbMGRSMapGrid
            // 
            this.tsbMGRSMapGrid.CheckOnClick = true;
            this.tsbMGRSMapGrid.Name = "tsbMGRSMapGrid";
            this.tsbMGRSMapGrid.Size = new System.Drawing.Size(141, 22);
            this.tsbMGRSMapGrid.Text = "MGRS Grid";
            this.tsbMGRSMapGrid.Click += new System.EventHandler(this.tsbMGRSMapGrid_Click);
            // 
            // tsbNZMGMapGrid
            // 
            this.tsbNZMGMapGrid.CheckOnClick = true;
            this.tsbNZMGMapGrid.Name = "tsbNZMGMapGrid";
            this.tsbNZMGMapGrid.Size = new System.Drawing.Size(141, 22);
            this.tsbNZMGMapGrid.Text = "NZMG Grid";
            this.tsbNZMGMapGrid.Click += new System.EventHandler(this.tsbNZMGMapGrid_Click);
            // 
            // tsbGeoRefMapGrid
            // 
            this.tsbGeoRefMapGrid.CheckOnClick = true;
            this.tsbGeoRefMapGrid.Name = "tsbGeoRefMapGrid";
            this.tsbGeoRefMapGrid.Size = new System.Drawing.Size(141, 22);
            this.tsbGeoRefMapGrid.Text = "GEOREF Grid";
            this.tsbGeoRefMapGrid.Click += new System.EventHandler(this.tsbGeoRefMapGrid_Click);
            // 
            // tsbMapHeightLines
            // 
            this.tsbMapHeightLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapHeightLines.Image = ((System.Drawing.Image)(resources.GetObject("tsbMapHeightLines.Image")));
            this.tsbMapHeightLines.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapHeightLines.Name = "tsbMapHeightLines";
            this.tsbMapHeightLines.Size = new System.Drawing.Size(28, 28);
            this.tsbMapHeightLines.Text = "Map Height Lines";
            this.tsbMapHeightLines.Click += new System.EventHandler(this.tsbMapHeightLines_Click);
            // 
            // tsbDtmVisualization
            // 
            this.tsbDtmVisualization.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDtmVisualization.Image = ((System.Drawing.Image)(resources.GetObject("tsbDtmVisualization.Image")));
            this.tsbDtmVisualization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDtmVisualization.Name = "tsbDtmVisualization";
            this.tsbDtmVisualization.Size = new System.Drawing.Size(28, 28);
            this.tsbDtmVisualization.Text = "Dtm Visualization";
            this.tsbDtmVisualization.Click += new System.EventHandler(this.tsbDtmVisualization_Click);
            // 
            // tsbDropDownFootprintPoints
            // 
            this.tsbDropDownFootprintPoints.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDropDownFootprintPoints.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFootprintPointsSync,
            this.tsbFootprintPointsAsync,
            this.tsbCameraCornersSync,
            this.tsbCameraCornersAsync,
            this.tsbCameraCornersWithHorizonsSync,
            this.tsbCameraCornersWithHorizonsAsync});
            this.tsbDropDownFootprintPoints.Image = ((System.Drawing.Image)(resources.GetObject("tsbDropDownFootprintPoints.Image")));
            this.tsbDropDownFootprintPoints.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDropDownFootprintPoints.Name = "tsbDropDownFootprintPoints";
            this.tsbDropDownFootprintPoints.Size = new System.Drawing.Size(37, 28);
            // 
            // tsbFootprintPointsSync
            // 
            this.tsbFootprintPointsSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFootprintPointsSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFootprintPointsSync.Name = "tsbFootprintPointsSync";
            this.tsbFootprintPointsSync.Size = new System.Drawing.Size(124, 19);
            this.tsbFootprintPointsSync.Text = "Footprint Points Sync";
            this.tsbFootprintPointsSync.Click += new System.EventHandler(this.tsbFootprintPointsSync_Click);
            // 
            // tsbFootprintPointsAsync
            // 
            this.tsbFootprintPointsAsync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFootprintPointsAsync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFootprintPointsAsync.Name = "tsbFootprintPointsAsync";
            this.tsbFootprintPointsAsync.Size = new System.Drawing.Size(131, 19);
            this.tsbFootprintPointsAsync.Text = "Footprint Points Async";
            this.tsbFootprintPointsAsync.Click += new System.EventHandler(this.tsbFootprintPointsAsync_Click);
            // 
            // tsbCameraCornersSync
            // 
            this.tsbCameraCornersSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCameraCornersSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCameraCornersSync.Name = "tsbCameraCornersSync";
            this.tsbCameraCornersSync.Size = new System.Drawing.Size(124, 19);
            this.tsbCameraCornersSync.Text = "Camera Corners Sync";
            this.tsbCameraCornersSync.Click += new System.EventHandler(this.tsbCameraCornersSync_Click);
            // 
            // tsbCameraCornersAsync
            // 
            this.tsbCameraCornersAsync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCameraCornersAsync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCameraCornersAsync.Name = "tsbCameraCornersAsync";
            this.tsbCameraCornersAsync.Size = new System.Drawing.Size(131, 19);
            this.tsbCameraCornersAsync.Text = "Camera Corners Async";
            this.tsbCameraCornersAsync.Click += new System.EventHandler(this.tsbCameraCornersAsync_Click);
            // 
            // tsbCameraCornersWithHorizonsSync
            // 
            this.tsbCameraCornersWithHorizonsSync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCameraCornersWithHorizonsSync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCameraCornersWithHorizonsSync.Name = "tsbCameraCornersWithHorizonsSync";
            this.tsbCameraCornersWithHorizonsSync.Size = new System.Drawing.Size(197, 19);
            this.tsbCameraCornersWithHorizonsSync.Text = "Camera Corners With Horizon Sync";
            this.tsbCameraCornersWithHorizonsSync.Click += new System.EventHandler(this.tsbCameraCornersWithHorizonsSync_Click);
            // 
            // tsbCameraCornersWithHorizonsAsync
            // 
            this.tsbCameraCornersWithHorizonsAsync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCameraCornersWithHorizonsAsync.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCameraCornersWithHorizonsAsync.Name = "tsbCameraCornersWithHorizonsAsync";
            this.tsbCameraCornersWithHorizonsAsync.Size = new System.Drawing.Size(204, 19);
            this.tsbCameraCornersWithHorizonsAsync.Text = "Camera Corners With Horizon Async";
            this.tsbCameraCornersWithHorizonsAsync.Click += new System.EventHandler(this.tsbCameraCornersWithHorizonsAsync_Click);
            // 
            // tsbFileProductions
            // 
            this.tsbFileProductions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbFileProductions.Image = ((System.Drawing.Image)(resources.GetObject("tsbFileProductions.Image")));
            this.tsbFileProductions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileProductions.Name = "tsbFileProductions";
            this.tsbFileProductions.Size = new System.Drawing.Size(96, 19);
            this.tsbFileProductions.Text = "File Productions";
            this.tsbFileProductions.Click += new System.EventHandler(this.tsbFileProductions_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrint.Image")));
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(28, 28);
            this.tsbPrint.Text = "Print";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // tsbScan
            // 
            this.tsbScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScan.Image = ((System.Drawing.Image)(resources.GetObject("tsbScan.Image")));
            this.tsbScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScan.Name = "tsbScan";
            this.tsbScan.Size = new System.Drawing.Size(28, 28);
            this.tsbScan.Text = "Scan";
            this.tsbScan.Click += new System.EventHandler(this.tsbScan_Click);
            // 
            // tsbEnvironment
            // 
            this.tsbEnvironment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEnvironment.Image = ((System.Drawing.Image)(resources.GetObject("tsbEnvironment.Image")));
            this.tsbEnvironment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEnvironment.Name = "tsbEnvironment";
            this.tsbEnvironment.Size = new System.Drawing.Size(28, 28);
            this.tsbEnvironment.Text = "Environment";
            this.tsbEnvironment.Click += new System.EventHandler(this.tsbEnvironment_Click);
            // 
            // tsb2DTo3D
            // 
            this.tsb2DTo3D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsb2DTo3D.Image = ((System.Drawing.Image)(resources.GetObject("tsb2DTo3D.Image")));
            this.tsb2DTo3D.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb2DTo3D.Name = "tsb2DTo3D";
            this.tsb2DTo3D.Size = new System.Drawing.Size(44, 19);
            this.tsb2DTo3D.Text = "2D/3D";
            this.tsb2DTo3D.Click += new System.EventHandler(this.tsb2DTo3D_Click);
            // 
            // tsGeneral
            // 
            this.tsGeneral.BackColor = System.Drawing.SystemColors.Menu;
            this.tsGeneral.Dock = System.Windows.Forms.DockStyle.None;
            this.tsGeneral.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsGeneral.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpenMapManually,
            this.tsbOpenMapScheme,
            this.tbsOpenLayers,
            this.tsbLoadViewport,
            this.toolStripSeparator1,
            this.tsbSaveViewportToSession,
            this.tsbTileWindows,
            this.tsbRenderType,
            this.toolStripSeparator2,
            this.tsbGeneralProperties,
            this.tsbObjectProperties});
            this.tsGeneral.Location = new System.Drawing.Point(3, 0);
            this.tsGeneral.Name = "tsGeneral";
            this.tsGeneral.Size = new System.Drawing.Size(430, 31);
            this.tsGeneral.TabIndex = 8;
            this.tsGeneral.Text = "Maps Panel";
            // 
            // tsbOpenMapManually
            // 
            this.tsbOpenMapManually.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenMapManually.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenMapManually.Image")));
            this.tsbOpenMapManually.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenMapManually.Name = "tsbOpenMapManually";
            this.tsbOpenMapManually.Size = new System.Drawing.Size(28, 28);
            this.tsbOpenMapManually.Text = "Open Map Manually";
            this.tsbOpenMapManually.Click += new System.EventHandler(this.tsbOpenMapManually_Click);
            // 
            // tsbOpenMapScheme
            // 
            this.tsbOpenMapScheme.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpenMapScheme.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenMapScheme.Image")));
            this.tsbOpenMapScheme.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenMapScheme.Name = "tsbOpenMapScheme";
            this.tsbOpenMapScheme.Size = new System.Drawing.Size(28, 28);
            this.tsbOpenMapScheme.Text = "Open Map Scheme";
            this.tsbOpenMapScheme.Click += new System.EventHandler(this.tsbOpenMapScheme_Click);
            // 
            // tbsOpenLayers
            // 
            this.tbsOpenLayers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbsOpenLayers.Image = ((System.Drawing.Image)(resources.GetObject("tbsOpenLayers.Image")));
            this.tbsOpenLayers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbsOpenLayers.Name = "tbsOpenLayers";
            this.tbsOpenLayers.Size = new System.Drawing.Size(28, 28);
            this.tbsOpenLayers.Text = "Open";
            this.tbsOpenLayers.ToolTipText = "Open Viewport with Several Layers";
            this.tbsOpenLayers.Click += new System.EventHandler(this.tbsOpenLayers_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbTileWindows
            // 
            this.tsbTileWindows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbTileWindows.Image = ((System.Drawing.Image)(resources.GetObject("tsbTileWindows.Image")));
            this.tsbTileWindows.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbTileWindows.Name = "tsbTileWindows";
            this.tsbTileWindows.Size = new System.Drawing.Size(28, 28);
            this.tsbTileWindows.Text = "Tile Windows";
            this.tsbTileWindows.Click += new System.EventHandler(this.tsbTileWindows_Click);
            // 
            // tsbRenderType
            // 
            this.tsbRenderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbRenderType.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.tsbRenderType.Items.AddRange(new object[] {
            "Pending - Updates Base",
            "Flag Base Render",
            "Render All",
            "Render",
            "Manual Render"});
            this.tsbRenderType.Name = "tsbRenderType";
            this.tsbRenderType.Size = new System.Drawing.Size(180, 31);
            this.tsbRenderType.SelectedIndexChanged += new System.EventHandler(this.tsbRenderType_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbGeneralProperties
            // 
            this.tsbGeneralProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGeneralProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsbGeneralProperties.Image")));
            this.tsbGeneralProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGeneralProperties.Name = "tsbGeneralProperties";
            this.tsbGeneralProperties.Size = new System.Drawing.Size(28, 28);
            this.tsbGeneralProperties.Text = "General Properties";
            this.tsbGeneralProperties.Click += new System.EventHandler(this.tsbGeneralProperties_Click);
            // 
            // tsbObjectProperties
            // 
            this.tsbObjectProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbObjectProperties.Image = ((System.Drawing.Image)(resources.GetObject("tsbObjectProperties.Image")));
            this.tsbObjectProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbObjectProperties.Name = "tsbObjectProperties";
            this.tsbObjectProperties.Size = new System.Drawing.Size(28, 28);
            this.tsbObjectProperties.Text = "Object Properties";
            this.tsbObjectProperties.Click += new System.EventHandler(this.tsbObjectProperties_Click);
            // 
            // tspMainToolsBar
            // 
            this.tspMainToolsBar.BackColor = System.Drawing.SystemColors.Menu;
            this.tspMainToolsBar.Controls.Add(this.tsGeneral);
            this.tspMainToolsBar.Controls.Add(this.tsVectorial);
            this.tspMainToolsBar.Controls.Add(this.tsManagments);
            this.tspMainToolsBar.Controls.Add(this.tsCalculations);
            this.tspMainToolsBar.Controls.Add(this.tsMapOperations);
            this.tspMainToolsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.tspMainToolsBar.Location = new System.Drawing.Point(0, 24);
            this.tspMainToolsBar.Name = "tspMainToolsBar";
            this.tspMainToolsBar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspMainToolsBar.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.tspMainToolsBar.Size = new System.Drawing.Size(1358, 31);
            // 
            // tsManagments
            // 
            this.tsManagments.BackColor = System.Drawing.SystemColors.Menu;
            this.tsManagments.Dock = System.Windows.Forms.DockStyle.None;
            this.tsManagments.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsManagments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbMapWorld,
            this.tsbObjectWorld});
            this.tsManagments.Location = new System.Drawing.Point(501, 0);
            this.tsManagments.Name = "tsManagments";
            this.tsManagments.Size = new System.Drawing.Size(211, 31);
            this.tsManagments.TabIndex = 7;
            this.tsManagments.Text = "Managments Panel";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1358, 453);
            this.Controls.Add(this.toolStripPanel3);
            this.Controls.Add(this.tspMainToolsBar);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.MainStatusBarStatistics);
            this.Controls.Add(this.MainStatusBar);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tsDrawingSymbolicItems.ResumeLayout(false);
            this.tsDrawingSymbolicItems.PerformLayout();
            this.tsDrawingPhysicalItems.ResumeLayout(false);
            this.tsDrawingPhysicalItems.PerformLayout();
            this.tsEditMode.ResumeLayout(false);
            this.tsEditMode.PerformLayout();
            this.tsAttendantTools.ResumeLayout(false);
            this.tsAttendantTools.PerformLayout();
            this.tsCalculations.ResumeLayout(false);
            this.tsCalculations.PerformLayout();
            this.tsExtendedEditMode.ResumeLayout(false);
            this.tsExtendedEditMode.PerformLayout();
            this.MainStatusBarStatistics.ResumeLayout(false);
            this.MainStatusBarStatistics.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.MainStatusBar.ResumeLayout(false);
            this.MainStatusBar.PerformLayout();
            this.toolStripPanel3.ResumeLayout(false);
            this.toolStripPanel3.PerformLayout();
            this.tsVectorial.ResumeLayout(false);
            this.tsVectorial.PerformLayout();
            this.tsMapOperations.ResumeLayout(false);
            this.tsMapOperations.PerformLayout();
            this.tsGeneral.ResumeLayout(false);
            this.tsGeneral.PerformLayout();
            this.tspMainToolsBar.ResumeLayout(false);
            this.tspMainToolsBar.PerformLayout();
            this.tsManagments.ResumeLayout(false);
            this.tsManagments.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       


        #endregion

        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStrip tsDrawingSymbolicItems;
        private System.Windows.Forms.ToolStrip tsDrawingPhysicalItems;
        private System.Windows.Forms.ToolStripButton tsbParticalEffectItem;
        private System.Windows.Forms.ToolStripButton tsbProjectorItem;
        private System.Windows.Forms.ToolStripButton tsbSoundItem;
        private System.Windows.Forms.ToolStripSplitButton tsbLightItem;
        private System.Windows.Forms.ToolStrip tsEditMode;
        private System.Windows.Forms.ToolStrip tsAttendantTools;
        private System.Windows.Forms.ToolStripButton tsbMapProductionTool;
        private System.Windows.Forms.ToolStripButton tsbPerformanceTester;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton tsbStressTester;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton tsbObjectLoader;
        private System.Windows.Forms.ToolStripButton tsbObjectAutoAnimatation;
        private System.Windows.Forms.ToolStrip tsCalculations;
        private System.Windows.Forms.ToolStripButton tsbGeographicCalculations;
        private System.Windows.Forms.ToolStripButton tsbGeometricCalculations;
        private System.Windows.Forms.ToolStripMenuItem tsbDirectionalLight;
        private System.Windows.Forms.ToolStripMenuItem tsbPointLight;
        private System.Windows.Forms.ToolStripMenuItem tsbSpotLight;
        private System.Windows.Forms.ToolStrip tsExtendedEditMode;
        private System.Windows.Forms.ToolStripButton tsbDistanceDirectionMeasure;
        private System.Windows.Forms.ToolStripButton tsbDynamicZoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripButton tsbEditModeNavigation;
        private System.Windows.Forms.ToolStripButton tsbExitCurrentAction;
        private System.Windows.Forms.ToolStripButton tsbEventCallBack;
        private System.Windows.Forms.ToolStripButton tsbCheckRenderNeeded;
        private System.Windows.Forms.ToolStripStatusLabel sbLastFPS;
        private System.Windows.Forms.ToolStripStatusLabel sbLastFPSBox;
        private System.Windows.Forms.ToolStripStatusLabel sbAvgFPS;
        private System.Windows.Forms.ToolStripStatusLabel sbAvgFPSBox;
        private System.Windows.Forms.ToolStripStatusLabel sbBestFPS;
        private System.Windows.Forms.ToolStripStatusLabel sbBestFPSBox;
        private System.Windows.Forms.ToolStripStatusLabel sbWorstFPS;
        private System.Windows.Forms.ToolStripStatusLabel sbWorstFPSBox;
        private System.Windows.Forms.ToolStripStatusLabel sbNumLastFrameTriangles;
        private System.Windows.Forms.ToolStripStatusLabel sbNumLastFrameTrianglesBox;
        private System.Windows.Forms.ToolStripStatusLabel sbNumLastFrameBatches;
        private System.Windows.Forms.ToolStripStatusLabel sbNumLastFrameBatchesBox;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.StatusStrip MainStatusBarStatistics;
        private System.Windows.Forms.ToolStripButton tsbLineItem;
        private System.Windows.Forms.ToolStripButton tsbArrowItem;
        private System.Windows.Forms.ToolStripButton tsbLineExpansionItem;
        private System.Windows.Forms.ToolStripButton tsbPolygonItem;
        private System.Windows.Forms.ToolStripButton tsbEllipseItem;
        private System.Windows.Forms.ToolStripButton tsbArcItem;
        private System.Windows.Forms.ToolStripButton tsbRectangleItem;
        private System.Windows.Forms.ToolStripButton tsbTextItem;
        private System.Windows.Forms.ToolStripButton tsbPictureItem;
        private System.Windows.Forms.ToolStripStatusLabel sbWorldCoordX;
        private System.Windows.Forms.ToolStripStatusLabel sbWorldCoordXBox;
        private System.Windows.Forms.ToolStripStatusLabel sbWorldCoordY;
        private System.Windows.Forms.ToolStripStatusLabel sbWorldCoordYBox;
        private System.Windows.Forms.ToolStripStatusLabel sbWorldCoordZ;
        private System.Windows.Forms.ToolStripStatusLabel sbWorldCoordZBox;
        private System.Windows.Forms.ToolStripStatusLabel sbScreenCoord;
        private System.Windows.Forms.ToolStripStatusLabel sbScreenCoordBox;
        private System.Windows.Forms.ToolStripStatusLabel sbImageCoord;
        private System.Windows.Forms.ToolStripStatusLabel sbImageCoordBox;
        private System.Windows.Forms.ToolStripStatusLabel sbViewportID;
        private System.Windows.Forms.ToolStripStatusLabel sbViewportIDBox;
        private System.Windows.Forms.ToolStripStatusLabel sbScale;
        private System.Windows.Forms.ToolStripStatusLabel sbScaleBox;
        private System.Windows.Forms.ToolStripStatusLabel sbMapScale;
        private System.Windows.Forms.ToolStripStatusLabel sbMapScaleBox;
        private System.Windows.Forms.ToolStripStatusLabel sbAverageFPS;
        private System.Windows.Forms.ToolStripStatusLabel sbAvrFPSBox;
        private System.Windows.Forms.ToolStripStatusLabel sbMsg;
        private System.Windows.Forms.ToolStripStatusLabel sbMsgBox;
        private System.Windows.Forms.StatusStrip MainStatusBar;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbEditObject;
        private System.Windows.Forms.ToolStripButton tsbInitObject;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapOperationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extendedEditModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symbolicItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem physicalItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calculationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attendantToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vectorialToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbStandaloneSQ;
        private System.Windows.Forms.ToolStripButton tsbEditModeProperties;
        private System.Windows.Forms.ToolStripPanel toolStripPanel3;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoadAndSaveSchemesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mosaicsCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbAnimationState;
        private System.Windows.Forms.ToolStripDropDownButton tsbMeshItem;
        private System.Windows.Forms.ToolStripMenuItem tsbNativeMesh;
        private System.Windows.Forms.ToolStripMenuItem tsbNativeLODMesh;
        private System.Windows.Forms.ToolStripMenuItem rollingShutterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointingFingerDBGeneratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vectorXMLCreatorToolStripMenuItem;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStripButton tsbMapWorld;
        private System.Windows.Forms.ToolStripButton tsbObjectWorld;
        private System.Windows.Forms.ToolStrip tsVectorial;
        private System.Windows.Forms.ToolStripButton tsbVectorial;
        private System.Windows.Forms.ToolStripButton tsbPathFinder;
        private System.Windows.Forms.ToolStrip tsMapOperations;
        private System.Windows.Forms.ToolStripButton tsbNavigateMap;
        private System.Windows.Forms.ToolStripButton tsbZoomIn;
        private System.Windows.Forms.ToolStripButton tsbZoomOut;
        private System.Windows.Forms.ToolStripButton tsbCenterMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripDropDownButton tsbMapGrid;
        private System.Windows.Forms.ToolStripMenuItem tsbUTMMapGrid;
        private System.Windows.Forms.ToolStripMenuItem tsbGeoMapGrid;
        private System.Windows.Forms.ToolStripMenuItem tsbMGRSMapGrid;
        private System.Windows.Forms.ToolStripMenuItem tsbNZMGMapGrid;
        private System.Windows.Forms.ToolStripButton tsbMapHeightLines;
        private System.Windows.Forms.ToolStripButton tsbDtmVisualization;
        private System.Windows.Forms.ToolStripButton tsbFileProductions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripButton tsbScan;
        private System.Windows.Forms.ToolStripButton tsbEnvironment;
        private System.Windows.Forms.ToolStrip tsGeneral;
        private System.Windows.Forms.ToolStripButton tsbOpenMapManually;
        private System.Windows.Forms.ToolStripButton tsbOpenMapScheme;
        private System.Windows.Forms.ToolStripButton tbsOpenLayers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbTileWindows;
        private System.Windows.Forms.ToolStripComboBox tsbRenderType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbGeneralProperties;
        private System.Windows.Forms.ToolStripButton tsbObjectProperties;
        private System.Windows.Forms.ToolStripPanel tspMainToolsBar;
        private System.Windows.Forms.ToolStrip tsManagments;
        private System.Windows.Forms.ToolStripButton tsbFootprintPointsSync;
        private System.Windows.Forms.ToolStripButton tsbFootprintPointsAsync;
        private System.Windows.Forms.ToolStripButton tsbCameraCornersSync;
        private System.Windows.Forms.ToolStripButton tsbCameraCornersAsync;
        private System.Windows.Forms.ToolStripButton tsbCameraCornersWithHorizonsSync;
        private System.Windows.Forms.ToolStripButton tsbCameraCornersWithHorizonsAsync;

        private System.Windows.Forms.ToolStripMenuItem signToSecurityServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsbNavPath;
        private System.Windows.Forms.ToolStripMenuItem tsbGeoRefMapGrid;
        private System.Windows.Forms.ToolStripDropDownButton tsbDropDownFootprintPoints;
        private System.Windows.Forms.ToolStripButton tsbSaveViewportToSession;
        private System.Windows.Forms.ToolStripButton tsbLoadViewport;
        private System.Windows.Forms.ToolStripButton tsbSightEllipseItem;
        private System.Windows.Forms.ToolStripButton tsbCollectGC;
        private System.Windows.Forms.ToolStripButton tsb2DTo3D;
    }
}



