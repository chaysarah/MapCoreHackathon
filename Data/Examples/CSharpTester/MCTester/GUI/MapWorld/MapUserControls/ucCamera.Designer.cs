namespace MCTester.MapWorld.MapUserControls
{
    partial class ucCamera
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
            this.gbCoordinateConversionsScreen = new System.Windows.Forms.GroupBox();
            this.lblToIC = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.ctrl3DCameraConPlaneNormal = new MCTester.Controls.Ctrl3DVector();
            this.ctrScreenPt = new MCTester.Controls.CtrlSamplePoint();
            this.ctrWorldPt = new MCTester.Controls.CtrlSamplePoint();
            this.chxIsIntersectionFound = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ntxPlaneLocation = new MCTester.Controls.NumericTextBox();
            this.btnConScreenToWorldOnTerrain = new System.Windows.Forms.Button();
            this.btnConScreenToWorldOnPlane = new System.Windows.Forms.Button();
            this.btnConWorldToScreen = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrl3DCameraConScreenPoint = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DCameraConWorldPoint = new MCTester.Controls.Ctrl3DVector();
            this.m_CameraTabCtrl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tpAnyMapType = new System.Windows.Forms.TabPage();
            this.gbRotateCameraAroundWorldPoint = new System.Windows.Forms.GroupBox();
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt = new MCTester.Controls.CtrlSamplePoint();
            this.btnRotateCameraAroundWorldPoint = new System.Windows.Forms.Button();
            this.label42 = new System.Windows.Forms.Label();
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint = new MCTester.Controls.Ctrl3DVector();
            this.chxRotateCameraAroundWorldPointRelativeToOrientation = new System.Windows.Forms.CheckBox();
            this.ntxRotateCameraAroundWorldPointDeltaRoll = new MCTester.Controls.NumericTextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.ntxRotateCameraAroundWorldPointDeltaPitch = new MCTester.Controls.NumericTextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.ntxRotateCameraAroundWorldPointDeltaYaw = new MCTester.Controls.NumericTextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCameraCenterOffset = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.ctrl2DFVectorCenterOffset = new MCTester.Controls.Ctrl2DFVector();
            this.gbCameraWorldVisibleArea = new System.Windows.Forms.GroupBox();
            this.boxWorldVisibleArea = new MCTester.Controls.CtrlSMcBox();
            this.ntxRectangleYaw = new MCTester.Controls.NumericTextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.btnGetWorldVisibleArea = new System.Windows.Forms.Button();
            this.ntxWorldVisibleAreaMargin = new MCTester.Controls.NumericTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.btnCameraWorldVisibleAreaOK = new System.Windows.Forms.Button();
            this.btnMoveRelativeToOrientationOK = new System.Windows.Forms.Button();
            this.gbSetScreenVisibleArea = new System.Windows.Forms.GroupBox();
            this.ctrlSamplePointBRScreenVisibleArea = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlSamplePointTLScreenVisibleArea = new MCTester.Controls.CtrlSamplePoint();
            this.label36 = new System.Windows.Forms.Label();
            this.cmbScreenVisibleAreaOperation = new System.Windows.Forms.ComboBox();
            this.btnScreenVisibleAreaOk = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.ctrl2DTLScreenVisibleArea = new MCTester.Controls.Ctrl2DFVector();
            this.ctrl2DBRScreenVisibleArea = new MCTester.Controls.Ctrl2DFVector();
            this.gbCameraScale = new System.Windows.Forms.GroupBox();
            this.pnlCameraScale3D = new System.Windows.Forms.Panel();
            this.ctrl3DCameraScaleWorldPoint = new MCTester.Controls.Ctrl3DVector();
            this.btnGetCameraScale3D = new System.Windows.Forms.Button();
            this.ctrlCameraScaleWorldPt = new MCTester.Controls.CtrlSamplePoint();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxCameraScale = new MCTester.Controls.NumericTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnCameraScaleOK = new System.Windows.Forms.Button();
            this.btnCameraPositionOK = new System.Windows.Forms.Button();
            this.btnCameraOrientationOK = new System.Windows.Forms.Button();
            this.txtMapType = new System.Windows.Forms.TextBox();
            this.btnCameraUpVectorOK = new System.Windows.Forms.Button();
            this.chxOrientationRelative = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.chxPositionRelative = new System.Windows.Forms.CheckBox();
            this.chxXYDirectionOnly = new System.Windows.Forms.CheckBox();
            this.chxUpVectorRelativeToOrientation = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.ctrlCameraPositionPt = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlCameraOrientation = new MCTester.Controls.Ctrl3DOrientation();
            this.ctrl3DCameraUpVector = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DMoveCameraRelativeToOrientation = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DCameraPosition = new MCTester.Controls.Ctrl3DVector();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnScrollCameraOK = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ntxScrollCameraDeltaX = new MCTester.Controls.NumericTextBox();
            this.ntxScrollCameraDeltaY = new MCTester.Controls.NumericTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.chxFootprintIsDefined = new System.Windows.Forms.CheckBox();
            this.btnCameraFootPrintOK = new System.Windows.Forms.Button();
            this.chxRenderInTwoSessions = new System.Windows.Forms.CheckBox();
            this.btnLookAtPointOK = new System.Windows.Forms.Button();
            this.btnForwardVectorOK = new System.Windows.Forms.Button();
            this.btnFieldOfViewOK = new System.Windows.Forms.Button();
            this.btnRotateCameraRelativeToOrientationOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHeightLimitsOK = new System.Windows.Forms.Button();
            this.BtnCameraClipDistancesOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chxCameraRelativeHeightLimits = new System.Windows.Forms.CheckBox();
            this.chxFVectorRelative = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.ctrl3DLookAt = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DForwardVector = new MCTester.Controls.Ctrl3DVector();
            this.ntxFieldOfView = new MCTester.Controls.NumericTextBox();
            this.ctrlLookAtPt = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlRotateCamera = new MCTester.Controls.Ctrl3DOrientation();
            this.ntxMaxRelativeHeightLimits = new MCTester.Controls.NumericTextBox();
            this.ntxMinRelativeHeightLimits = new MCTester.Controls.NumericTextBox();
            this.ntxMinCameraClipDistances = new MCTester.Controls.NumericTextBox();
            this.ntxMaxCameraClipDistances = new MCTester.Controls.NumericTextBox();
            this.tpCameraAttachment = new System.Windows.Forms.TabPage();
            this.btnCameraAttachmentEnabled = new System.Windows.Forms.Button();
            this.chxCameraAttachmentEnabled = new System.Windows.Forms.CheckBox();
            this.btnCameraAttachmentOK = new System.Windows.Forms.Button();
            this.splitContainerCameraAttachment = new System.Windows.Forms.SplitContainer();
            this.lsvCameraAttachmentSrc = new System.Windows.Forms.ListView();
            this.colID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbLookAt = new System.Windows.Forms.GroupBox();
            this.gbAdditionalOrientation = new System.Windows.Forms.GroupBox();
            this.ntxAdditionalRoll = new MCTester.Controls.NumericTextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.ntxAdditionalPitch = new MCTester.Controls.NumericTextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.ntxAdditionalYaw = new MCTester.Controls.NumericTextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.chxAttachOrientation = new System.Windows.Forms.CheckBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.ntxCameraAttachmentSrcAPIndex = new MCTester.Controls.NumericTextBox();
            this.ctrl3DVectorAttachmentCameraOffset = new MCTester.Controls.Ctrl3DVector();
            this.chxLookAt = new System.Windows.Forms.CheckBox();
            this.treeViewCameraAttachmentSrc = new System.Windows.Forms.TreeView();
            this.lsvCameraAttachmentLookAt = new System.Windows.Forms.ListView();
            this.LookAtColID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LookAtColName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label38 = new System.Windows.Forms.Label();
            this.ntxCameraAttachmentLookAtAPIndex = new MCTester.Controls.NumericTextBox();
            this.treeViewCameraAttachmentlookAt = new System.Windows.Forms.TreeView();
            this.gbCoordinateConversionsScreen.SuspendLayout();
            this.m_CameraTabCtrl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tpAnyMapType.SuspendLayout();
            this.gbRotateCameraAroundWorldPoint.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbCameraWorldVisibleArea.SuspendLayout();
            this.gbSetScreenVisibleArea.SuspendLayout();
            this.gbCameraScale.SuspendLayout();
            this.pnlCameraScale3D.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tpCameraAttachment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCameraAttachment)).BeginInit();
            this.splitContainerCameraAttachment.Panel1.SuspendLayout();
            this.splitContainerCameraAttachment.Panel2.SuspendLayout();
            this.splitContainerCameraAttachment.SuspendLayout();
            this.gbLookAt.SuspendLayout();
            this.gbAdditionalOrientation.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCoordinateConversionsScreen
            // 
            this.gbCoordinateConversionsScreen.Controls.Add(this.lblToIC);
            this.gbCoordinateConversionsScreen.Controls.Add(this.label37);
            this.gbCoordinateConversionsScreen.Controls.Add(this.ctrl3DCameraConPlaneNormal);
            this.gbCoordinateConversionsScreen.Controls.Add(this.ctrScreenPt);
            this.gbCoordinateConversionsScreen.Controls.Add(this.ctrWorldPt);
            this.gbCoordinateConversionsScreen.Controls.Add(this.chxIsIntersectionFound);
            this.gbCoordinateConversionsScreen.Controls.Add(this.label9);
            this.gbCoordinateConversionsScreen.Controls.Add(this.ntxPlaneLocation);
            this.gbCoordinateConversionsScreen.Controls.Add(this.btnConScreenToWorldOnTerrain);
            this.gbCoordinateConversionsScreen.Controls.Add(this.btnConScreenToWorldOnPlane);
            this.gbCoordinateConversionsScreen.Controls.Add(this.btnConWorldToScreen);
            this.gbCoordinateConversionsScreen.Controls.Add(this.label5);
            this.gbCoordinateConversionsScreen.Controls.Add(this.label6);
            this.gbCoordinateConversionsScreen.Controls.Add(this.ctrl3DCameraConScreenPoint);
            this.gbCoordinateConversionsScreen.Controls.Add(this.ctrl3DCameraConWorldPoint);
            this.gbCoordinateConversionsScreen.Location = new System.Drawing.Point(3, 6);
            this.gbCoordinateConversionsScreen.Name = "gbCoordinateConversionsScreen";
            this.gbCoordinateConversionsScreen.Size = new System.Drawing.Size(843, 238);
            this.gbCoordinateConversionsScreen.TabIndex = 33;
            this.gbCoordinateConversionsScreen.TabStop = false;
            this.gbCoordinateConversionsScreen.Text = "Coordinate Conversions (To And From Screen) And Footprint";
            // 
            // lblToIC
            // 
            this.lblToIC.AutoSize = true;
            this.lblToIC.ForeColor = System.Drawing.Color.Red;
            this.lblToIC.Location = new System.Drawing.Point(10, 19);
            this.lblToIC.Name = "lblToIC";
            this.lblToIC.Size = new System.Drawing.Size(247, 13);
            this.lblToIC.TabIndex = 70;
            this.lblToIC.Text = "Image Calc viewport: here \"World\" means \"Image\"";
            this.lblToIC.Visible = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(11, 121);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(71, 13);
            this.label37.TabIndex = 69;
            this.label37.Text = "Plane normal:";
            // 
            // ctrl3DCameraConPlaneNormal
            // 
            this.ctrl3DCameraConPlaneNormal.IsReadOnly = false;
            this.ctrl3DCameraConPlaneNormal.Location = new System.Drawing.Point(88, 115);
            this.ctrl3DCameraConPlaneNormal.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DCameraConPlaneNormal.Name = "ctrl3DCameraConPlaneNormal";
            this.ctrl3DCameraConPlaneNormal.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DCameraConPlaneNormal.TabIndex = 68;
            this.ctrl3DCameraConPlaneNormal.X = 0D;
            this.ctrl3DCameraConPlaneNormal.Y = 0D;
            this.ctrl3DCameraConPlaneNormal.Z = 1D;
            // 
            // ctrScreenPt
            // 
            this.ctrScreenPt._DgvControlName = null;
            this.ctrScreenPt._IsRelativeToDTM = false;
            this.ctrScreenPt._PointInOverlayManagerCoordSys = true;
            this.ctrScreenPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrScreenPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrScreenPt._SampleOnePoint = true;
            this.ctrScreenPt._UserControlName = "ctrl3DCameraConScreenPoint";
            this.ctrScreenPt.IsAsync = false;
            this.ctrScreenPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrScreenPt.Location = new System.Drawing.Point(316, 81);
            this.ctrScreenPt.Name = "ctrScreenPt";
            this.ctrScreenPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_SCREEN;
            this.ctrScreenPt.Size = new System.Drawing.Size(41, 23);
            this.ctrScreenPt.TabIndex = 66;
            this.ctrScreenPt.Text = "...";
            this.ctrScreenPt.UseVisualStyleBackColor = true;
            // 
            // ctrWorldPt
            // 
            this.ctrWorldPt._DgvControlName = null;
            this.ctrWorldPt._IsRelativeToDTM = false;
            this.ctrWorldPt._PointInOverlayManagerCoordSys = false;
            this.ctrWorldPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrWorldPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrWorldPt._SampleOnePoint = true;
            this.ctrWorldPt._UserControlName = "ctrl3DCameraConWorldPoint";
            this.ctrWorldPt.IsAsync = false;
            this.ctrWorldPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrWorldPt.Location = new System.Drawing.Point(316, 47);
            this.ctrWorldPt.Name = "ctrWorldPt";
            this.ctrWorldPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrWorldPt.Size = new System.Drawing.Size(41, 23);
            this.ctrWorldPt.TabIndex = 65;
            this.ctrWorldPt.Text = "...";
            this.ctrWorldPt.UseVisualStyleBackColor = true;
            // 
            // chxIsIntersectionFound
            // 
            this.chxIsIntersectionFound.AutoSize = true;
            this.chxIsIntersectionFound.Enabled = false;
            this.chxIsIntersectionFound.Location = new System.Drawing.Point(708, 178);
            this.chxIsIntersectionFound.Name = "chxIsIntersectionFound";
            this.chxIsIntersectionFound.Size = new System.Drawing.Size(125, 17);
            this.chxIsIntersectionFound.TabIndex = 64;
            this.chxIsIntersectionFound.Text = "Is Intersection Found";
            this.chxIsIntersectionFound.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 63;
            this.label9.Text = "Plane location:";
            // 
            // ntxPlaneLocation
            // 
            this.ntxPlaneLocation.Location = new System.Drawing.Point(97, 150);
            this.ntxPlaneLocation.Name = "ntxPlaneLocation";
            this.ntxPlaneLocation.Size = new System.Drawing.Size(93, 20);
            this.ntxPlaneLocation.TabIndex = 62;
            // 
            // btnConScreenToWorldOnTerrain
            // 
            this.btnConScreenToWorldOnTerrain.Location = new System.Drawing.Point(596, 159);
            this.btnConScreenToWorldOnTerrain.Name = "btnConScreenToWorldOnTerrain";
            this.btnConScreenToWorldOnTerrain.Size = new System.Drawing.Size(106, 36);
            this.btnConScreenToWorldOnTerrain.TabIndex = 61;
            this.btnConScreenToWorldOnTerrain.Text = "Screen To World On Terrain";
            this.btnConScreenToWorldOnTerrain.UseVisualStyleBackColor = true;
            this.btnConScreenToWorldOnTerrain.Click += new System.EventHandler(this.btnConScreenToWorldOnTerrain_Click);
            // 
            // btnConScreenToWorldOnPlane
            // 
            this.btnConScreenToWorldOnPlane.Location = new System.Drawing.Point(596, 115);
            this.btnConScreenToWorldOnPlane.Name = "btnConScreenToWorldOnPlane";
            this.btnConScreenToWorldOnPlane.Size = new System.Drawing.Size(106, 38);
            this.btnConScreenToWorldOnPlane.TabIndex = 56;
            this.btnConScreenToWorldOnPlane.Text = "Screen To World On Plane";
            this.btnConScreenToWorldOnPlane.UseVisualStyleBackColor = true;
            this.btnConScreenToWorldOnPlane.Click += new System.EventHandler(this.btnConScreenToWorldOnPlane_Click);
            // 
            // btnConWorldToScreen
            // 
            this.btnConWorldToScreen.Location = new System.Drawing.Point(596, 35);
            this.btnConWorldToScreen.Name = "btnConWorldToScreen";
            this.btnConWorldToScreen.Size = new System.Drawing.Size(106, 23);
            this.btnConWorldToScreen.TabIndex = 55;
            this.btnConWorldToScreen.Text = "World To Screen";
            this.btnConWorldToScreen.UseVisualStyleBackColor = true;
            this.btnConWorldToScreen.Click += new System.EventHandler(this.btnConWorldToScreen_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "World:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "Screen:";
            // 
            // ctrl3DCameraConScreenPoint
            // 
            this.ctrl3DCameraConScreenPoint.IsReadOnly = false;
            this.ctrl3DCameraConScreenPoint.Location = new System.Drawing.Point(88, 80);
            this.ctrl3DCameraConScreenPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DCameraConScreenPoint.Name = "ctrl3DCameraConScreenPoint";
            this.ctrl3DCameraConScreenPoint.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DCameraConScreenPoint.TabIndex = 35;
            this.ctrl3DCameraConScreenPoint.X = 0D;
            this.ctrl3DCameraConScreenPoint.Y = 0D;
            this.ctrl3DCameraConScreenPoint.Z = 0D;
            // 
            // ctrl3DCameraConWorldPoint
            // 
            this.ctrl3DCameraConWorldPoint.IsReadOnly = false;
            this.ctrl3DCameraConWorldPoint.Location = new System.Drawing.Point(88, 46);
            this.ctrl3DCameraConWorldPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DCameraConWorldPoint.Name = "ctrl3DCameraConWorldPoint";
            this.ctrl3DCameraConWorldPoint.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DCameraConWorldPoint.TabIndex = 34;
            this.ctrl3DCameraConWorldPoint.X = 0D;
            this.ctrl3DCameraConWorldPoint.Y = 0D;
            this.ctrl3DCameraConWorldPoint.Z = 0D;
            // 
            // m_CameraTabCtrl
            // 
            this.m_CameraTabCtrl.Controls.Add(this.tabPage2);
            this.m_CameraTabCtrl.Controls.Add(this.tpAnyMapType);
            this.m_CameraTabCtrl.Controls.Add(this.tabPage4);
            this.m_CameraTabCtrl.Controls.Add(this.tabPage5);
            this.m_CameraTabCtrl.Controls.Add(this.tpCameraAttachment);
            this.m_CameraTabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_CameraTabCtrl.Location = new System.Drawing.Point(0, 0);
            this.m_CameraTabCtrl.Name = "m_CameraTabCtrl";
            this.m_CameraTabCtrl.SelectedIndex = 0;
            this.m_CameraTabCtrl.Size = new System.Drawing.Size(860, 579);
            this.m_CameraTabCtrl.TabIndex = 34;
            this.m_CameraTabCtrl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.m_CameraTabCtrl_Selecting);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbCoordinateConversionsScreen);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(852, 553);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Camera Conversions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tpAnyMapType
            // 
            this.tpAnyMapType.Controls.Add(this.gbRotateCameraAroundWorldPoint);
            this.tpAnyMapType.Controls.Add(this.groupBox1);
            this.tpAnyMapType.Controls.Add(this.gbCameraWorldVisibleArea);
            this.tpAnyMapType.Controls.Add(this.btnMoveRelativeToOrientationOK);
            this.tpAnyMapType.Controls.Add(this.gbSetScreenVisibleArea);
            this.tpAnyMapType.Controls.Add(this.gbCameraScale);
            this.tpAnyMapType.Controls.Add(this.btnCameraPositionOK);
            this.tpAnyMapType.Controls.Add(this.btnCameraOrientationOK);
            this.tpAnyMapType.Controls.Add(this.txtMapType);
            this.tpAnyMapType.Controls.Add(this.btnCameraUpVectorOK);
            this.tpAnyMapType.Controls.Add(this.chxOrientationRelative);
            this.tpAnyMapType.Controls.Add(this.label21);
            this.tpAnyMapType.Controls.Add(this.label20);
            this.tpAnyMapType.Controls.Add(this.chxPositionRelative);
            this.tpAnyMapType.Controls.Add(this.chxXYDirectionOnly);
            this.tpAnyMapType.Controls.Add(this.chxUpVectorRelativeToOrientation);
            this.tpAnyMapType.Controls.Add(this.label4);
            this.tpAnyMapType.Controls.Add(this.label18);
            this.tpAnyMapType.Controls.Add(this.label17);
            this.tpAnyMapType.Controls.Add(this.ctrlCameraPositionPt);
            this.tpAnyMapType.Controls.Add(this.ctrlCameraOrientation);
            this.tpAnyMapType.Controls.Add(this.ctrl3DCameraUpVector);
            this.tpAnyMapType.Controls.Add(this.ctrl3DMoveCameraRelativeToOrientation);
            this.tpAnyMapType.Controls.Add(this.ctrl3DCameraPosition);
            this.tpAnyMapType.Location = new System.Drawing.Point(4, 22);
            this.tpAnyMapType.Name = "tpAnyMapType";
            this.tpAnyMapType.Padding = new System.Windows.Forms.Padding(3);
            this.tpAnyMapType.Size = new System.Drawing.Size(852, 553);
            this.tpAnyMapType.TabIndex = 2;
            this.tpAnyMapType.Text = "Any Map Type";
            this.tpAnyMapType.UseVisualStyleBackColor = true;
            this.tpAnyMapType.Enter += new System.EventHandler(this.tpAnyMapType_Enter);
            this.tpAnyMapType.Leave += new System.EventHandler(this.tpAnyMapType_Leave);
            // 
            // gbRotateCameraAroundWorldPoint
            // 
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.btnRotateCameraAroundWorldPoint);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.label42);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.ctrl3DRotateCameraAroundWorldPointPivotPoint);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.chxRotateCameraAroundWorldPointRelativeToOrientation);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.ntxRotateCameraAroundWorldPointDeltaRoll);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.label41);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.ntxRotateCameraAroundWorldPointDeltaPitch);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.label40);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.ntxRotateCameraAroundWorldPointDeltaYaw);
            this.gbRotateCameraAroundWorldPoint.Controls.Add(this.label39);
            this.gbRotateCameraAroundWorldPoint.Location = new System.Drawing.Point(3, 470);
            this.gbRotateCameraAroundWorldPoint.Name = "gbRotateCameraAroundWorldPoint";
            this.gbRotateCameraAroundWorldPoint.Size = new System.Drawing.Size(642, 77);
            this.gbRotateCameraAroundWorldPoint.TabIndex = 106;
            this.gbRotateCameraAroundWorldPoint.TabStop = false;
            this.gbRotateCameraAroundWorldPoint.Text = "Rotate Camera Around World Point";
            // 
            // ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt
            // 
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt._DgvControlName = null;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt._IsRelativeToDTM = false;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt._PointInOverlayManagerCoordSys = false;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt._SampleOnePoint = true;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt._UserControlName = "ctrl3DRotateCameraAroundWorldPointPivotPoint";
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.IsAsync = false;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.Location = new System.Drawing.Point(311, 20);
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.Name = "ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt";
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.Size = new System.Drawing.Size(33, 23);
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.TabIndex = 84;
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.Text = "...";
            this.ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt.UseVisualStyleBackColor = true;
            // 
            // btnRotateCameraAroundWorldPoint
            // 
            this.btnRotateCameraAroundWorldPoint.Location = new System.Drawing.Point(603, 48);
            this.btnRotateCameraAroundWorldPoint.Name = "btnRotateCameraAroundWorldPoint";
            this.btnRotateCameraAroundWorldPoint.Size = new System.Drawing.Size(33, 23);
            this.btnRotateCameraAroundWorldPoint.TabIndex = 83;
            this.btnRotateCameraAroundWorldPoint.Text = "Set";
            this.btnRotateCameraAroundWorldPoint.UseVisualStyleBackColor = true;
            this.btnRotateCameraAroundWorldPoint.Click += new System.EventHandler(this.btnRotateCameraAroundWorldPoint_Click);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(6, 25);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(61, 13);
            this.label42.TabIndex = 8;
            this.label42.Text = "Pivot Point:";
            // 
            // ctrl3DRotateCameraAroundWorldPointPivotPoint
            // 
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.IsReadOnly = false;
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.Location = new System.Drawing.Point(73, 19);
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.Name = "ctrl3DRotateCameraAroundWorldPointPivotPoint";
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.TabIndex = 7;
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.X = 0D;
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.Y = 0D;
            this.ctrl3DRotateCameraAroundWorldPointPivotPoint.Z = 0D;
            // 
            // chxRotateCameraAroundWorldPointRelativeToOrientation
            // 
            this.chxRotateCameraAroundWorldPointRelativeToOrientation.AutoSize = true;
            this.chxRotateCameraAroundWorldPointRelativeToOrientation.Location = new System.Drawing.Point(392, 24);
            this.chxRotateCameraAroundWorldPointRelativeToOrientation.Name = "chxRotateCameraAroundWorldPointRelativeToOrientation";
            this.chxRotateCameraAroundWorldPointRelativeToOrientation.Size = new System.Drawing.Size(135, 17);
            this.chxRotateCameraAroundWorldPointRelativeToOrientation.TabIndex = 6;
            this.chxRotateCameraAroundWorldPointRelativeToOrientation.Text = "Relative To Orientation";
            this.chxRotateCameraAroundWorldPointRelativeToOrientation.UseVisualStyleBackColor = true;
            // 
            // ntxRotateCameraAroundWorldPointDeltaRoll
            // 
            this.ntxRotateCameraAroundWorldPointDeltaRoll.Location = new System.Drawing.Point(454, 49);
            this.ntxRotateCameraAroundWorldPointDeltaRoll.Name = "ntxRotateCameraAroundWorldPointDeltaRoll";
            this.ntxRotateCameraAroundWorldPointDeltaRoll.Size = new System.Drawing.Size(100, 20);
            this.ntxRotateCameraAroundWorldPointDeltaRoll.TabIndex = 5;
            this.ntxRotateCameraAroundWorldPointDeltaRoll.Text = "0";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(389, 52);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(56, 13);
            this.label41.TabIndex = 4;
            this.label41.Text = "Delta Roll:";
            // 
            // ntxRotateCameraAroundWorldPointDeltaPitch
            // 
            this.ntxRotateCameraAroundWorldPointDeltaPitch.Location = new System.Drawing.Point(245, 49);
            this.ntxRotateCameraAroundWorldPointDeltaPitch.Name = "ntxRotateCameraAroundWorldPointDeltaPitch";
            this.ntxRotateCameraAroundWorldPointDeltaPitch.Size = new System.Drawing.Size(100, 20);
            this.ntxRotateCameraAroundWorldPointDeltaPitch.TabIndex = 3;
            this.ntxRotateCameraAroundWorldPointDeltaPitch.Text = "0";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(180, 52);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(62, 13);
            this.label40.TabIndex = 2;
            this.label40.Text = "Delta Pitch:";
            // 
            // ntxRotateCameraAroundWorldPointDeltaYaw
            // 
            this.ntxRotateCameraAroundWorldPointDeltaYaw.Location = new System.Drawing.Point(71, 49);
            this.ntxRotateCameraAroundWorldPointDeltaYaw.Name = "ntxRotateCameraAroundWorldPointDeltaYaw";
            this.ntxRotateCameraAroundWorldPointDeltaYaw.Size = new System.Drawing.Size(100, 20);
            this.ntxRotateCameraAroundWorldPointDeltaYaw.TabIndex = 1;
            this.ntxRotateCameraAroundWorldPointDeltaYaw.Text = "0";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(6, 52);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(59, 13);
            this.label39.TabIndex = 0;
            this.label39.Text = "Delta Yaw:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCameraCenterOffset);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.ctrl2DFVectorCenterOffset);
            this.groupBox1.Location = new System.Drawing.Point(3, 415);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 49);
            this.groupBox1.TabIndex = 105;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera Center Offset";
            // 
            // btnCameraCenterOffset
            // 
            this.btnCameraCenterOffset.Location = new System.Drawing.Point(603, 22);
            this.btnCameraCenterOffset.Name = "btnCameraCenterOffset";
            this.btnCameraCenterOffset.Size = new System.Drawing.Size(33, 23);
            this.btnCameraCenterOffset.TabIndex = 82;
            this.btnCameraCenterOffset.Text = "Set";
            this.btnCameraCenterOffset.UseVisualStyleBackColor = true;
            this.btnCameraCenterOffset.Click += new System.EventHandler(this.btnCameraCenterOffset_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(3, 26);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(38, 13);
            this.label29.TabIndex = 103;
            this.label29.Text = "Offset:";
            // 
            // ctrl2DFVectorCenterOffset
            // 
            this.ctrl2DFVectorCenterOffset.Location = new System.Drawing.Point(47, 19);
            this.ctrl2DFVectorCenterOffset.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DFVectorCenterOffset.Name = "ctrl2DFVectorCenterOffset";
            this.ctrl2DFVectorCenterOffset.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DFVectorCenterOffset.TabIndex = 104;
            this.ctrl2DFVectorCenterOffset.X = 0F;
            this.ctrl2DFVectorCenterOffset.Y = 0F;
            // 
            // gbCameraWorldVisibleArea
            // 
            this.gbCameraWorldVisibleArea.Controls.Add(this.boxWorldVisibleArea);
            this.gbCameraWorldVisibleArea.Controls.Add(this.ntxRectangleYaw);
            this.gbCameraWorldVisibleArea.Controls.Add(this.label43);
            this.gbCameraWorldVisibleArea.Controls.Add(this.btnGetWorldVisibleArea);
            this.gbCameraWorldVisibleArea.Controls.Add(this.ntxWorldVisibleAreaMargin);
            this.gbCameraWorldVisibleArea.Controls.Add(this.label35);
            this.gbCameraWorldVisibleArea.Controls.Add(this.btnCameraWorldVisibleAreaOK);
            this.gbCameraWorldVisibleArea.Location = new System.Drawing.Point(3, 297);
            this.gbCameraWorldVisibleArea.Name = "gbCameraWorldVisibleArea";
            this.gbCameraWorldVisibleArea.Size = new System.Drawing.Size(642, 112);
            this.gbCameraWorldVisibleArea.TabIndex = 102;
            this.gbCameraWorldVisibleArea.TabStop = false;
            this.gbCameraWorldVisibleArea.Text = "Camera World Visible Area";
            // 
            // boxWorldVisibleArea
            // 
            this.boxWorldVisibleArea.GroupBoxText = "World Visible Area";
            this.boxWorldVisibleArea.Location = new System.Drawing.Point(3, 18);
            this.boxWorldVisibleArea.Name = "boxWorldVisibleArea";
            this.boxWorldVisibleArea.Size = new System.Drawing.Size(304, 85);
            this.boxWorldVisibleArea.TabIndex = 89;
            // 
            // ntxRectangleYaw
            // 
            this.ntxRectangleYaw.Location = new System.Drawing.Point(415, 34);
            this.ntxRectangleYaw.Name = "ntxRectangleYaw";
            this.ntxRectangleYaw.Size = new System.Drawing.Size(94, 20);
            this.ntxRectangleYaw.TabIndex = 87;
            this.ntxRectangleYaw.Text = "0";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(330, 37);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(83, 13);
            this.label43.TabIndex = 88;
            this.label43.Text = "Rectangle Yaw:";
            // 
            // btnGetWorldVisibleArea
            // 
            this.btnGetWorldVisibleArea.Location = new System.Drawing.Point(566, 80);
            this.btnGetWorldVisibleArea.Name = "btnGetWorldVisibleArea";
            this.btnGetWorldVisibleArea.Size = new System.Drawing.Size(33, 23);
            this.btnGetWorldVisibleArea.TabIndex = 84;
            this.btnGetWorldVisibleArea.Text = "Get";
            this.btnGetWorldVisibleArea.UseVisualStyleBackColor = true;
            this.btnGetWorldVisibleArea.Click += new System.EventHandler(this.btnGetWorldVisibleArea_Click);
            // 
            // ntxWorldVisibleAreaMargin
            // 
            this.ntxWorldVisibleAreaMargin.Location = new System.Drawing.Point(415, 62);
            this.ntxWorldVisibleAreaMargin.Name = "ntxWorldVisibleAreaMargin";
            this.ntxWorldVisibleAreaMargin.Size = new System.Drawing.Size(94, 20);
            this.ntxWorldVisibleAreaMargin.TabIndex = 82;
            this.ntxWorldVisibleAreaMargin.Text = "0";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(330, 65);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(79, 13);
            this.label35.TabIndex = 83;
            this.label35.Text = "Screen Margin:";
            // 
            // btnCameraWorldVisibleAreaOK
            // 
            this.btnCameraWorldVisibleAreaOK.Location = new System.Drawing.Point(603, 80);
            this.btnCameraWorldVisibleAreaOK.Name = "btnCameraWorldVisibleAreaOK";
            this.btnCameraWorldVisibleAreaOK.Size = new System.Drawing.Size(33, 23);
            this.btnCameraWorldVisibleAreaOK.TabIndex = 81;
            this.btnCameraWorldVisibleAreaOK.Text = "Set";
            this.btnCameraWorldVisibleAreaOK.UseVisualStyleBackColor = true;
            this.btnCameraWorldVisibleAreaOK.Click += new System.EventHandler(this.btnCameraWorldVisibleAreaOK_Click);
            // 
            // btnMoveRelativeToOrientationOK
            // 
            this.btnMoveRelativeToOrientationOK.Location = new System.Drawing.Point(596, 123);
            this.btnMoveRelativeToOrientationOK.Name = "btnMoveRelativeToOrientationOK";
            this.btnMoveRelativeToOrientationOK.Size = new System.Drawing.Size(33, 23);
            this.btnMoveRelativeToOrientationOK.TabIndex = 100;
            this.btnMoveRelativeToOrientationOK.Text = "Set";
            this.btnMoveRelativeToOrientationOK.UseVisualStyleBackColor = true;
            this.btnMoveRelativeToOrientationOK.Click += new System.EventHandler(this.btnMoveRelativeToOrientationOK_Click);
            // 
            // gbSetScreenVisibleArea
            // 
            this.gbSetScreenVisibleArea.Controls.Add(this.ctrlSamplePointBRScreenVisibleArea);
            this.gbSetScreenVisibleArea.Controls.Add(this.ctrlSamplePointTLScreenVisibleArea);
            this.gbSetScreenVisibleArea.Controls.Add(this.label36);
            this.gbSetScreenVisibleArea.Controls.Add(this.cmbScreenVisibleAreaOperation);
            this.gbSetScreenVisibleArea.Controls.Add(this.btnScreenVisibleAreaOk);
            this.gbSetScreenVisibleArea.Controls.Add(this.label26);
            this.gbSetScreenVisibleArea.Controls.Add(this.label27);
            this.gbSetScreenVisibleArea.Controls.Add(this.ctrl2DTLScreenVisibleArea);
            this.gbSetScreenVisibleArea.Controls.Add(this.ctrl2DBRScreenVisibleArea);
            this.gbSetScreenVisibleArea.Location = new System.Drawing.Point(3, 228);
            this.gbSetScreenVisibleArea.Name = "gbSetScreenVisibleArea";
            this.gbSetScreenVisibleArea.Size = new System.Drawing.Size(642, 64);
            this.gbSetScreenVisibleArea.TabIndex = 101;
            this.gbSetScreenVisibleArea.TabStop = false;
            this.gbSetScreenVisibleArea.Text = "Screen Visible Area";
            // 
            // ctrlSamplePointBRScreenVisibleArea
            // 
            this.ctrlSamplePointBRScreenVisibleArea._DgvControlName = null;
            this.ctrlSamplePointBRScreenVisibleArea._IsRelativeToDTM = false;
            this.ctrlSamplePointBRScreenVisibleArea._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointBRScreenVisibleArea._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointBRScreenVisibleArea._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointBRScreenVisibleArea._SampleOnePoint = true;
            this.ctrlSamplePointBRScreenVisibleArea._UserControlName = "ctrl2DBRScreenVisibleArea";
            this.ctrlSamplePointBRScreenVisibleArea.IsAsync = false;
            this.ctrlSamplePointBRScreenVisibleArea.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointBRScreenVisibleArea.Location = new System.Drawing.Point(231, 38);
            this.ctrlSamplePointBRScreenVisibleArea.Name = "ctrlSamplePointBRScreenVisibleArea";
            this.ctrlSamplePointBRScreenVisibleArea.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_SCREEN;
            this.ctrlSamplePointBRScreenVisibleArea.Size = new System.Drawing.Size(41, 23);
            this.ctrlSamplePointBRScreenVisibleArea.TabIndex = 84;
            this.ctrlSamplePointBRScreenVisibleArea.Text = "...";
            this.ctrlSamplePointBRScreenVisibleArea.UseVisualStyleBackColor = true;
            // 
            // ctrlSamplePointTLScreenVisibleArea
            // 
            this.ctrlSamplePointTLScreenVisibleArea._DgvControlName = null;
            this.ctrlSamplePointTLScreenVisibleArea._IsRelativeToDTM = false;
            this.ctrlSamplePointTLScreenVisibleArea._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointTLScreenVisibleArea._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointTLScreenVisibleArea._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointTLScreenVisibleArea._SampleOnePoint = true;
            this.ctrlSamplePointTLScreenVisibleArea._UserControlName = "ctrl2DTLScreenVisibleArea";
            this.ctrlSamplePointTLScreenVisibleArea.IsAsync = false;
            this.ctrlSamplePointTLScreenVisibleArea.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointTLScreenVisibleArea.Location = new System.Drawing.Point(231, 12);
            this.ctrlSamplePointTLScreenVisibleArea.Name = "ctrlSamplePointTLScreenVisibleArea";
            this.ctrlSamplePointTLScreenVisibleArea.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_SCREEN;
            this.ctrlSamplePointTLScreenVisibleArea.Size = new System.Drawing.Size(41, 23);
            this.ctrlSamplePointTLScreenVisibleArea.TabIndex = 83;
            this.ctrlSamplePointTLScreenVisibleArea.Text = "...";
            this.ctrlSamplePointTLScreenVisibleArea.UseVisualStyleBackColor = true;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(286, 41);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(56, 13);
            this.label36.TabIndex = 82;
            this.label36.Text = "Operation:";
            // 
            // cmbScreenVisibleAreaOperation
            // 
            this.cmbScreenVisibleAreaOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScreenVisibleAreaOperation.FormattingEnabled = true;
            this.cmbScreenVisibleAreaOperation.Location = new System.Drawing.Point(348, 38);
            this.cmbScreenVisibleAreaOperation.Name = "cmbScreenVisibleAreaOperation";
            this.cmbScreenVisibleAreaOperation.Size = new System.Drawing.Size(236, 21);
            this.cmbScreenVisibleAreaOperation.TabIndex = 81;
            // 
            // btnScreenVisibleAreaOk
            // 
            this.btnScreenVisibleAreaOk.Location = new System.Drawing.Point(592, 36);
            this.btnScreenVisibleAreaOk.Name = "btnScreenVisibleAreaOk";
            this.btnScreenVisibleAreaOk.Size = new System.Drawing.Size(33, 23);
            this.btnScreenVisibleAreaOk.TabIndex = 80;
            this.btnScreenVisibleAreaOk.Text = "Set";
            this.btnScreenVisibleAreaOk.UseVisualStyleBackColor = true;
            this.btnScreenVisibleAreaOk.Click += new System.EventHandler(this.btnScreenVisibleAreaOk_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(3, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(50, 13);
            this.label26.TabIndex = 40;
            this.label26.Text = "Top Left:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(3, 41);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(71, 13);
            this.label27.TabIndex = 41;
            this.label27.Text = "Bottom Right:";
            // 
            // ctrl2DTLScreenVisibleArea
            // 
            this.ctrl2DTLScreenVisibleArea.Location = new System.Drawing.Point(74, 12);
            this.ctrl2DTLScreenVisibleArea.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DTLScreenVisibleArea.Name = "ctrl2DTLScreenVisibleArea";
            this.ctrl2DTLScreenVisibleArea.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DTLScreenVisibleArea.TabIndex = 43;
            this.ctrl2DTLScreenVisibleArea.X = 0F;
            this.ctrl2DTLScreenVisibleArea.Y = 0F;
            // 
            // ctrl2DBRScreenVisibleArea
            // 
            this.ctrl2DBRScreenVisibleArea.Location = new System.Drawing.Point(74, 36);
            this.ctrl2DBRScreenVisibleArea.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DBRScreenVisibleArea.Name = "ctrl2DBRScreenVisibleArea";
            this.ctrl2DBRScreenVisibleArea.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DBRScreenVisibleArea.TabIndex = 44;
            this.ctrl2DBRScreenVisibleArea.X = 0F;
            this.ctrl2DBRScreenVisibleArea.Y = 0F;
            // 
            // gbCameraScale
            // 
            this.gbCameraScale.Controls.Add(this.pnlCameraScale3D);
            this.gbCameraScale.Controls.Add(this.ntxCameraScale);
            this.gbCameraScale.Controls.Add(this.label19);
            this.gbCameraScale.Controls.Add(this.btnCameraScaleOK);
            this.gbCameraScale.Location = new System.Drawing.Point(3, 153);
            this.gbCameraScale.Name = "gbCameraScale";
            this.gbCameraScale.Size = new System.Drawing.Size(642, 75);
            this.gbCameraScale.TabIndex = 97;
            this.gbCameraScale.TabStop = false;
            this.gbCameraScale.Text = "Camera Scale";
            // 
            // pnlCameraScale3D
            // 
            this.pnlCameraScale3D.Controls.Add(this.ctrl3DCameraScaleWorldPoint);
            this.pnlCameraScale3D.Controls.Add(this.btnGetCameraScale3D);
            this.pnlCameraScale3D.Controls.Add(this.ctrlCameraScaleWorldPt);
            this.pnlCameraScale3D.Controls.Add(this.label3);
            this.pnlCameraScale3D.Location = new System.Drawing.Point(6, 34);
            this.pnlCameraScale3D.Name = "pnlCameraScale3D";
            this.pnlCameraScale3D.Size = new System.Drawing.Size(502, 35);
            this.pnlCameraScale3D.TabIndex = 79;
            // 
            // ctrl3DCameraScaleWorldPoint
            // 
            this.ctrl3DCameraScaleWorldPoint.IsReadOnly = false;
            this.ctrl3DCameraScaleWorldPoint.Location = new System.Drawing.Point(129, 1);
            this.ctrl3DCameraScaleWorldPoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DCameraScaleWorldPoint.Name = "ctrl3DCameraScaleWorldPoint";
            this.ctrl3DCameraScaleWorldPoint.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DCameraScaleWorldPoint.TabIndex = 34;
            this.ctrl3DCameraScaleWorldPoint.X = 0D;
            this.ctrl3DCameraScaleWorldPoint.Y = 0D;
            this.ctrl3DCameraScaleWorldPoint.Z = 0D;
            // 
            // btnGetCameraScale3D
            // 
            this.btnGetCameraScale3D.Location = new System.Drawing.Point(422, 3);
            this.btnGetCameraScale3D.Name = "btnGetCameraScale3D";
            this.btnGetCameraScale3D.Size = new System.Drawing.Size(33, 23);
            this.btnGetCameraScale3D.TabIndex = 78;
            this.btnGetCameraScale3D.Text = "Get";
            this.btnGetCameraScale3D.UseVisualStyleBackColor = true;
            this.btnGetCameraScale3D.Click += new System.EventHandler(this.btnGetCameraScale3D_Click);
            // 
            // ctrlCameraScaleWorldPt
            // 
            this.ctrlCameraScaleWorldPt._DgvControlName = null;
            this.ctrlCameraScaleWorldPt._IsRelativeToDTM = false;
            this.ctrlCameraScaleWorldPt._PointInOverlayManagerCoordSys = true;
            this.ctrlCameraScaleWorldPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrlCameraScaleWorldPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlCameraScaleWorldPt._SampleOnePoint = true;
            this.ctrlCameraScaleWorldPt._UserControlName = "ctrl3DCameraScaleWorldPoint";
            this.ctrlCameraScaleWorldPt.IsAsync = false;
            this.ctrlCameraScaleWorldPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlCameraScaleWorldPt.Location = new System.Drawing.Point(366, 3);
            this.ctrlCameraScaleWorldPt.Name = "ctrlCameraScaleWorldPt";
            this.ctrlCameraScaleWorldPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlCameraScaleWorldPt.Size = new System.Drawing.Size(41, 23);
            this.ctrlCameraScaleWorldPt.TabIndex = 66;
            this.ctrlCameraScaleWorldPt.Text = "...";
            this.ctrlCameraScaleWorldPt.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "In 3D  Enter World Point:";
            // 
            // ntxCameraScale
            // 
            this.ntxCameraScale.Location = new System.Drawing.Point(88, 11);
            this.ntxCameraScale.Name = "ntxCameraScale";
            this.ntxCameraScale.Size = new System.Drawing.Size(94, 20);
            this.ntxCameraScale.TabIndex = 14;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 13);
            this.label19.TabIndex = 31;
            this.label19.Text = "Camera Scale: ";
            // 
            // btnCameraScaleOK
            // 
            this.btnCameraScaleOK.Location = new System.Drawing.Point(592, 39);
            this.btnCameraScaleOK.Name = "btnCameraScaleOK";
            this.btnCameraScaleOK.Size = new System.Drawing.Size(33, 23);
            this.btnCameraScaleOK.TabIndex = 77;
            this.btnCameraScaleOK.Text = "Set";
            this.btnCameraScaleOK.UseVisualStyleBackColor = true;
            this.btnCameraScaleOK.Click += new System.EventHandler(this.btnCameraScaleOK_Click);
            // 
            // btnCameraPositionOK
            // 
            this.btnCameraPositionOK.Location = new System.Drawing.Point(595, 37);
            this.btnCameraPositionOK.Name = "btnCameraPositionOK";
            this.btnCameraPositionOK.Size = new System.Drawing.Size(33, 23);
            this.btnCameraPositionOK.TabIndex = 96;
            this.btnCameraPositionOK.Text = "Set";
            this.btnCameraPositionOK.UseVisualStyleBackColor = true;
            this.btnCameraPositionOK.Click += new System.EventHandler(this.btnCameraPositionOK_Click);
            // 
            // btnCameraOrientationOK
            // 
            this.btnCameraOrientationOK.Location = new System.Drawing.Point(596, 95);
            this.btnCameraOrientationOK.Name = "btnCameraOrientationOK";
            this.btnCameraOrientationOK.Size = new System.Drawing.Size(33, 23);
            this.btnCameraOrientationOK.TabIndex = 99;
            this.btnCameraOrientationOK.Text = "Set";
            this.btnCameraOrientationOK.UseVisualStyleBackColor = true;
            this.btnCameraOrientationOK.Click += new System.EventHandler(this.btnCameraOrientationOK_Click);
            // 
            // txtMapType
            // 
            this.txtMapType.Enabled = false;
            this.txtMapType.Location = new System.Drawing.Point(87, 9);
            this.txtMapType.Name = "txtMapType";
            this.txtMapType.Size = new System.Drawing.Size(78, 20);
            this.txtMapType.TabIndex = 88;
            // 
            // btnCameraUpVectorOK
            // 
            this.btnCameraUpVectorOK.Location = new System.Drawing.Point(595, 66);
            this.btnCameraUpVectorOK.Name = "btnCameraUpVectorOK";
            this.btnCameraUpVectorOK.Size = new System.Drawing.Size(33, 23);
            this.btnCameraUpVectorOK.TabIndex = 98;
            this.btnCameraUpVectorOK.Text = "Set";
            this.btnCameraUpVectorOK.UseVisualStyleBackColor = true;
            this.btnCameraUpVectorOK.Click += new System.EventHandler(this.btnCameraUpVectorOK_Click);
            // 
            // chxOrientationRelative
            // 
            this.chxOrientationRelative.AutoSize = true;
            this.chxOrientationRelative.Location = new System.Drawing.Point(484, 99);
            this.chxOrientationRelative.Name = "chxOrientationRelative";
            this.chxOrientationRelative.Size = new System.Drawing.Size(79, 17);
            this.chxOrientationRelative.TabIndex = 93;
            this.chxOrientationRelative.Text = "Is Relative:";
            this.chxOrientationRelative.UseVisualStyleBackColor = true;
            this.chxOrientationRelative.CheckedChanged += new System.EventHandler(this.chxOrientationRelative_CheckedChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 128);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(149, 13);
            this.label21.TabIndex = 94;
            this.label21.Text = "Move Relative To Orientation:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 100);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(97, 13);
            this.label20.TabIndex = 89;
            this.label20.Text = "Camera Orientation";
            // 
            // chxPositionRelative
            // 
            this.chxPositionRelative.AutoSize = true;
            this.chxPositionRelative.Location = new System.Drawing.Point(399, 41);
            this.chxPositionRelative.Name = "chxPositionRelative";
            this.chxPositionRelative.Size = new System.Drawing.Size(79, 17);
            this.chxPositionRelative.TabIndex = 81;
            this.chxPositionRelative.Text = "Is Relative:";
            this.chxPositionRelative.UseVisualStyleBackColor = true;
            this.chxPositionRelative.CheckedChanged += new System.EventHandler(this.chxPositionRelative_CheckedChanged);
            // 
            // chxXYDirectionOnly
            // 
            this.chxXYDirectionOnly.AutoSize = true;
            this.chxXYDirectionOnly.Location = new System.Drawing.Point(398, 127);
            this.chxXYDirectionOnly.Name = "chxXYDirectionOnly";
            this.chxXYDirectionOnly.Size = new System.Drawing.Size(112, 17);
            this.chxXYDirectionOnly.TabIndex = 84;
            this.chxXYDirectionOnly.Text = "XY Direction Only:";
            this.chxXYDirectionOnly.UseVisualStyleBackColor = true;
            // 
            // chxUpVectorRelativeToOrientation
            // 
            this.chxUpVectorRelativeToOrientation.AutoSize = true;
            this.chxUpVectorRelativeToOrientation.Location = new System.Drawing.Point(348, 70);
            this.chxUpVectorRelativeToOrientation.Name = "chxUpVectorRelativeToOrientation";
            this.chxUpVectorRelativeToOrientation.Size = new System.Drawing.Size(138, 17);
            this.chxUpVectorRelativeToOrientation.TabIndex = 82;
            this.chxUpVectorRelativeToOrientation.Text = "Relative To Orientation:";
            this.chxUpVectorRelativeToOrientation.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(6, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Map Type:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 71);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(97, 13);
            this.label18.TabIndex = 87;
            this.label18.Text = "Camera Up Vector:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 42);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 13);
            this.label17.TabIndex = 86;
            this.label17.Text = "Camera Position:";
            // 
            // ctrlCameraPositionPt
            // 
            this.ctrlCameraPositionPt._DgvControlName = null;
            this.ctrlCameraPositionPt._IsRelativeToDTM = false;
            this.ctrlCameraPositionPt._PointInOverlayManagerCoordSys = true;
            this.ctrlCameraPositionPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrlCameraPositionPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlCameraPositionPt._SampleOnePoint = true;
            this.ctrlCameraPositionPt._UserControlName = "ctrl3DCameraPosition";
            this.ctrlCameraPositionPt.IsAsync = false;
            this.ctrlCameraPositionPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlCameraPositionPt.Location = new System.Drawing.Point(348, 37);
            this.ctrlCameraPositionPt.Name = "ctrlCameraPositionPt";
            this.ctrlCameraPositionPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlCameraPositionPt.Size = new System.Drawing.Size(41, 23);
            this.ctrlCameraPositionPt.TabIndex = 95;
            this.ctrlCameraPositionPt.Text = "...";
            this.ctrlCameraPositionPt.UseVisualStyleBackColor = true;
            // 
            // ctrlCameraOrientation
            // 
            this.ctrlCameraOrientation.Location = new System.Drawing.Point(111, 96);
            this.ctrlCameraOrientation.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlCameraOrientation.Name = "ctrlCameraOrientation";
            this.ctrlCameraOrientation.Pitch = 0F;
            this.ctrlCameraOrientation.Roll = 0F;
            this.ctrlCameraOrientation.Size = new System.Drawing.Size(367, 25);
            this.ctrlCameraOrientation.TabIndex = 92;
            this.ctrlCameraOrientation.Yaw = 0F;
            // 
            // ctrl3DCameraUpVector
            // 
            this.ctrl3DCameraUpVector.IsReadOnly = false;
            this.ctrl3DCameraUpVector.Location = new System.Drawing.Point(111, 65);
            this.ctrl3DCameraUpVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DCameraUpVector.Name = "ctrl3DCameraUpVector";
            this.ctrl3DCameraUpVector.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DCameraUpVector.TabIndex = 91;
            this.ctrl3DCameraUpVector.X = 0D;
            this.ctrl3DCameraUpVector.Y = 0D;
            this.ctrl3DCameraUpVector.Z = 0D;
            // 
            // ctrl3DMoveCameraRelativeToOrientation
            // 
            this.ctrl3DMoveCameraRelativeToOrientation.IsReadOnly = false;
            this.ctrl3DMoveCameraRelativeToOrientation.Location = new System.Drawing.Point(161, 124);
            this.ctrl3DMoveCameraRelativeToOrientation.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DMoveCameraRelativeToOrientation.Name = "ctrl3DMoveCameraRelativeToOrientation";
            this.ctrl3DMoveCameraRelativeToOrientation.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DMoveCameraRelativeToOrientation.TabIndex = 85;
            this.ctrl3DMoveCameraRelativeToOrientation.X = 0D;
            this.ctrl3DMoveCameraRelativeToOrientation.Y = 0D;
            this.ctrl3DMoveCameraRelativeToOrientation.Z = 0D;
            // 
            // ctrl3DCameraPosition
            // 
            this.ctrl3DCameraPosition.IsReadOnly = false;
            this.ctrl3DCameraPosition.Location = new System.Drawing.Point(111, 35);
            this.ctrl3DCameraPosition.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DCameraPosition.Name = "ctrl3DCameraPosition";
            this.ctrl3DCameraPosition.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DCameraPosition.TabIndex = 83;
            this.ctrl3DCameraPosition.X = 0D;
            this.ctrl3DCameraPosition.Y = 0D;
            this.ctrl3DCameraPosition.Z = 0D;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnScrollCameraOK);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.ntxScrollCameraDeltaX);
            this.tabPage4.Controls.Add(this.ntxScrollCameraDeltaY);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(852, 553);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "2D Map";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnScrollCameraOK
            // 
            this.btnScrollCameraOK.Location = new System.Drawing.Point(609, 8);
            this.btnScrollCameraOK.Name = "btnScrollCameraOK";
            this.btnScrollCameraOK.Size = new System.Drawing.Size(33, 23);
            this.btnScrollCameraOK.TabIndex = 70;
            this.btnScrollCameraOK.Text = "Set";
            this.btnScrollCameraOK.UseVisualStyleBackColor = true;
            this.btnScrollCameraOK.Click += new System.EventHandler(this.btnScrollCameraOK_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Scroll Camera:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(94, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "DeltaX:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(201, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "DeltaY:";
            // 
            // ntxScrollCameraDeltaX
            // 
            this.ntxScrollCameraDeltaX.Location = new System.Drawing.Point(142, 10);
            this.ntxScrollCameraDeltaX.Name = "ntxScrollCameraDeltaX";
            this.ntxScrollCameraDeltaX.Size = new System.Drawing.Size(56, 20);
            this.ntxScrollCameraDeltaX.TabIndex = 19;
            this.ntxScrollCameraDeltaX.Text = "0";
            // 
            // ntxScrollCameraDeltaY
            // 
            this.ntxScrollCameraDeltaY.Location = new System.Drawing.Point(246, 10);
            this.ntxScrollCameraDeltaY.Name = "ntxScrollCameraDeltaY";
            this.ntxScrollCameraDeltaY.Size = new System.Drawing.Size(56, 20);
            this.ntxScrollCameraDeltaY.TabIndex = 20;
            this.ntxScrollCameraDeltaY.Text = "0";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.chxFootprintIsDefined);
            this.tabPage5.Controls.Add(this.btnCameraFootPrintOK);
            this.tabPage5.Controls.Add(this.chxRenderInTwoSessions);
            this.tabPage5.Controls.Add(this.btnLookAtPointOK);
            this.tabPage5.Controls.Add(this.btnForwardVectorOK);
            this.tabPage5.Controls.Add(this.btnFieldOfViewOK);
            this.tabPage5.Controls.Add(this.btnRotateCameraRelativeToOrientationOK);
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Controls.Add(this.btnHeightLimitsOK);
            this.tabPage5.Controls.Add(this.BtnCameraClipDistancesOK);
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Controls.Add(this.chxCameraRelativeHeightLimits);
            this.tabPage5.Controls.Add(this.chxFVectorRelative);
            this.tabPage5.Controls.Add(this.label13);
            this.tabPage5.Controls.Add(this.label12);
            this.tabPage5.Controls.Add(this.label24);
            this.tabPage5.Controls.Add(this.label23);
            this.tabPage5.Controls.Add(this.label22);
            this.tabPage5.Controls.Add(this.label15);
            this.tabPage5.Controls.Add(this.label16);
            this.tabPage5.Controls.Add(this.label14);
            this.tabPage5.Controls.Add(this.ctrl3DLookAt);
            this.tabPage5.Controls.Add(this.ctrl3DForwardVector);
            this.tabPage5.Controls.Add(this.ntxFieldOfView);
            this.tabPage5.Controls.Add(this.ctrlLookAtPt);
            this.tabPage5.Controls.Add(this.ctrlRotateCamera);
            this.tabPage5.Controls.Add(this.ntxMaxRelativeHeightLimits);
            this.tabPage5.Controls.Add(this.ntxMinRelativeHeightLimits);
            this.tabPage5.Controls.Add(this.ntxMinCameraClipDistances);
            this.tabPage5.Controls.Add(this.ntxMaxCameraClipDistances);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(852, 553);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "3D Map";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // chxFootprintIsDefined
            // 
            this.chxFootprintIsDefined.AutoSize = true;
            this.chxFootprintIsDefined.Location = new System.Drawing.Point(9, 204);
            this.chxFootprintIsDefined.Name = "chxFootprintIsDefined";
            this.chxFootprintIsDefined.Size = new System.Drawing.Size(110, 17);
            this.chxFootprintIsDefined.TabIndex = 77;
            this.chxFootprintIsDefined.Text = "Defined Footprint:";
            this.chxFootprintIsDefined.UseVisualStyleBackColor = true;
            // 
            // btnCameraFootPrintOK
            // 
            this.btnCameraFootPrintOK.Location = new System.Drawing.Point(609, 198);
            this.btnCameraFootPrintOK.Name = "btnCameraFootPrintOK";
            this.btnCameraFootPrintOK.Size = new System.Drawing.Size(33, 23);
            this.btnCameraFootPrintOK.TabIndex = 76;
            this.btnCameraFootPrintOK.Text = "Set";
            this.btnCameraFootPrintOK.UseVisualStyleBackColor = true;
            this.btnCameraFootPrintOK.Click += new System.EventHandler(this.btnCameraFootPrintOK_Click);
            // 
            // chxRenderInTwoSessions
            // 
            this.chxRenderInTwoSessions.AutoSize = true;
            this.chxRenderInTwoSessions.Location = new System.Drawing.Point(320, 173);
            this.chxRenderInTwoSessions.Name = "chxRenderInTwoSessions";
            this.chxRenderInTwoSessions.Size = new System.Drawing.Size(145, 17);
            this.chxRenderInTwoSessions.TabIndex = 75;
            this.chxRenderInTwoSessions.Text = "Render In Two Sessions:";
            this.chxRenderInTwoSessions.UseVisualStyleBackColor = true;
            // 
            // btnLookAtPointOK
            // 
            this.btnLookAtPointOK.Location = new System.Drawing.Point(609, 10);
            this.btnLookAtPointOK.Name = "btnLookAtPointOK";
            this.btnLookAtPointOK.Size = new System.Drawing.Size(33, 23);
            this.btnLookAtPointOK.TabIndex = 74;
            this.btnLookAtPointOK.Text = "Set";
            this.btnLookAtPointOK.UseVisualStyleBackColor = true;
            this.btnLookAtPointOK.Click += new System.EventHandler(this.btnLookAtPointOK_Click);
            // 
            // btnForwardVectorOK
            // 
            this.btnForwardVectorOK.Location = new System.Drawing.Point(609, 44);
            this.btnForwardVectorOK.Name = "btnForwardVectorOK";
            this.btnForwardVectorOK.Size = new System.Drawing.Size(33, 23);
            this.btnForwardVectorOK.TabIndex = 73;
            this.btnForwardVectorOK.Text = "Set";
            this.btnForwardVectorOK.UseVisualStyleBackColor = true;
            this.btnForwardVectorOK.Click += new System.EventHandler(this.btnForwardVectorOK_Click);
            // 
            // btnFieldOfViewOK
            // 
            this.btnFieldOfViewOK.Location = new System.Drawing.Point(609, 74);
            this.btnFieldOfViewOK.Name = "btnFieldOfViewOK";
            this.btnFieldOfViewOK.Size = new System.Drawing.Size(33, 23);
            this.btnFieldOfViewOK.TabIndex = 72;
            this.btnFieldOfViewOK.Text = "Set";
            this.btnFieldOfViewOK.UseVisualStyleBackColor = true;
            this.btnFieldOfViewOK.Click += new System.EventHandler(this.btnFieldOfViewOK_Click);
            // 
            // btnRotateCameraRelativeToOrientationOK
            // 
            this.btnRotateCameraRelativeToOrientationOK.Location = new System.Drawing.Point(609, 105);
            this.btnRotateCameraRelativeToOrientationOK.Name = "btnRotateCameraRelativeToOrientationOK";
            this.btnRotateCameraRelativeToOrientationOK.Size = new System.Drawing.Size(33, 23);
            this.btnRotateCameraRelativeToOrientationOK.TabIndex = 71;
            this.btnRotateCameraRelativeToOrientationOK.Text = "Set";
            this.btnRotateCameraRelativeToOrientationOK.UseVisualStyleBackColor = true;
            this.btnRotateCameraRelativeToOrientationOK.Click += new System.EventHandler(this.btnRotateCameraRelativeToOrientationOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Forward Vector:";
            // 
            // btnHeightLimitsOK
            // 
            this.btnHeightLimitsOK.Location = new System.Drawing.Point(609, 135);
            this.btnHeightLimitsOK.Name = "btnHeightLimitsOK";
            this.btnHeightLimitsOK.Size = new System.Drawing.Size(33, 23);
            this.btnHeightLimitsOK.TabIndex = 70;
            this.btnHeightLimitsOK.Text = "Set";
            this.btnHeightLimitsOK.UseVisualStyleBackColor = true;
            this.btnHeightLimitsOK.Click += new System.EventHandler(this.btnHeightLimitsOK_Click);
            // 
            // BtnCameraClipDistancesOK
            // 
            this.BtnCameraClipDistancesOK.Location = new System.Drawing.Point(609, 169);
            this.BtnCameraClipDistancesOK.Name = "BtnCameraClipDistancesOK";
            this.BtnCameraClipDistancesOK.Size = new System.Drawing.Size(33, 23);
            this.BtnCameraClipDistancesOK.TabIndex = 69;
            this.BtnCameraClipDistancesOK.Text = "Set";
            this.BtnCameraClipDistancesOK.UseVisualStyleBackColor = true;
            this.BtnCameraClipDistancesOK.Click += new System.EventHandler(this.BtnCameraClipDistancesOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Field Of View:";
            // 
            // chxCameraRelativeHeightLimits
            // 
            this.chxCameraRelativeHeightLimits.AutoSize = true;
            this.chxCameraRelativeHeightLimits.Location = new System.Drawing.Point(275, 139);
            this.chxCameraRelativeHeightLimits.Name = "chxCameraRelativeHeightLimits";
            this.chxCameraRelativeHeightLimits.Size = new System.Drawing.Size(65, 17);
            this.chxCameraRelativeHeightLimits.TabIndex = 36;
            this.chxCameraRelativeHeightLimits.Text = "Enabled";
            this.chxCameraRelativeHeightLimits.UseVisualStyleBackColor = true;
            // 
            // chxFVectorRelative
            // 
            this.chxFVectorRelative.AutoSize = true;
            this.chxFVectorRelative.Location = new System.Drawing.Point(328, 48);
            this.chxFVectorRelative.Name = "chxFVectorRelative";
            this.chxFVectorRelative.Size = new System.Drawing.Size(138, 17);
            this.chxFVectorRelative.TabIndex = 55;
            this.chxFVectorRelative.Text = "Relative To Orientation:";
            this.chxFVectorRelative.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(82, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(27, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Min:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(177, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "Max:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 15);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(47, 13);
            this.label24.TabIndex = 53;
            this.label24.Text = "Look At:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 140);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 13);
            this.label23.TabIndex = 52;
            this.label23.Text = "Height Limits:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 110);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(193, 13);
            this.label22.TabIndex = 51;
            this.label22.Text = "Rotate Camera Relative To Orientation:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(127, 174);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 47;
            this.label15.Text = "Min:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 174);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(116, 13);
            this.label16.TabIndex = 49;
            this.label16.Text = "Camera Clip Distances:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(222, 174);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 13);
            this.label14.TabIndex = 48;
            this.label14.Text = "Max:";
            // 
            // ctrl3DLookAt
            // 
            this.ctrl3DLookAt.IsReadOnly = false;
            this.ctrl3DLookAt.Location = new System.Drawing.Point(91, 8);
            this.ctrl3DLookAt.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DLookAt.Name = "ctrl3DLookAt";
            this.ctrl3DLookAt.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DLookAt.TabIndex = 16;
            this.ctrl3DLookAt.X = 0D;
            this.ctrl3DLookAt.Y = 0D;
            this.ctrl3DLookAt.Z = 0D;
            // 
            // ctrl3DForwardVector
            // 
            this.ctrl3DForwardVector.IsReadOnly = false;
            this.ctrl3DForwardVector.Location = new System.Drawing.Point(91, 42);
            this.ctrl3DForwardVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DForwardVector.Name = "ctrl3DForwardVector";
            this.ctrl3DForwardVector.Size = new System.Drawing.Size(231, 28);
            this.ctrl3DForwardVector.TabIndex = 20;
            this.ctrl3DForwardVector.X = 0D;
            this.ctrl3DForwardVector.Y = 0D;
            this.ctrl3DForwardVector.Z = 0D;
            // 
            // ntxFieldOfView
            // 
            this.ntxFieldOfView.Location = new System.Drawing.Point(96, 76);
            this.ntxFieldOfView.Name = "ntxFieldOfView";
            this.ntxFieldOfView.Size = new System.Drawing.Size(99, 20);
            this.ntxFieldOfView.TabIndex = 22;
            this.ntxFieldOfView.Text = "0";
            // 
            // ctrlLookAtPt
            // 
            this.ctrlLookAtPt._DgvControlName = null;
            this.ctrlLookAtPt._IsRelativeToDTM = false;
            this.ctrlLookAtPt._PointInOverlayManagerCoordSys = true;
            this.ctrlLookAtPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrlLookAtPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlLookAtPt._SampleOnePoint = true;
            this.ctrlLookAtPt._UserControlName = "ctrl3DLookAt";
            this.ctrlLookAtPt.IsAsync = false;
            this.ctrlLookAtPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlLookAtPt.Location = new System.Drawing.Point(328, 10);
            this.ctrlLookAtPt.Name = "ctrlLookAtPt";
            this.ctrlLookAtPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlLookAtPt.Size = new System.Drawing.Size(41, 23);
            this.ctrlLookAtPt.TabIndex = 68;
            this.ctrlLookAtPt.Text = "...";
            this.ctrlLookAtPt.UseVisualStyleBackColor = true;
            // 
            // ctrlRotateCamera
            // 
            this.ctrlRotateCamera.Location = new System.Drawing.Point(202, 106);
            this.ctrlRotateCamera.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlRotateCamera.Name = "ctrlRotateCamera";
            this.ctrlRotateCamera.Pitch = 0F;
            this.ctrlRotateCamera.Roll = 0F;
            this.ctrlRotateCamera.Size = new System.Drawing.Size(364, 27);
            this.ctrlRotateCamera.TabIndex = 54;
            this.ctrlRotateCamera.Yaw = 0F;
            // 
            // ntxMaxRelativeHeightLimits
            // 
            this.ntxMaxRelativeHeightLimits.Location = new System.Drawing.Point(213, 137);
            this.ntxMaxRelativeHeightLimits.Name = "ntxMaxRelativeHeightLimits";
            this.ntxMaxRelativeHeightLimits.Size = new System.Drawing.Size(56, 20);
            this.ntxMaxRelativeHeightLimits.TabIndex = 38;
            this.ntxMaxRelativeHeightLimits.Text = "0";
            // 
            // ntxMinRelativeHeightLimits
            // 
            this.ntxMinRelativeHeightLimits.Location = new System.Drawing.Point(115, 137);
            this.ntxMinRelativeHeightLimits.Name = "ntxMinRelativeHeightLimits";
            this.ntxMinRelativeHeightLimits.Size = new System.Drawing.Size(56, 20);
            this.ntxMinRelativeHeightLimits.TabIndex = 37;
            this.ntxMinRelativeHeightLimits.Text = "0";
            // 
            // ntxMinCameraClipDistances
            // 
            this.ntxMinCameraClipDistances.Location = new System.Drawing.Point(160, 171);
            this.ntxMinCameraClipDistances.Name = "ntxMinCameraClipDistances";
            this.ntxMinCameraClipDistances.Size = new System.Drawing.Size(56, 20);
            this.ntxMinCameraClipDistances.TabIndex = 45;
            this.ntxMinCameraClipDistances.Text = "0";
            // 
            // ntxMaxCameraClipDistances
            // 
            this.ntxMaxCameraClipDistances.Location = new System.Drawing.Point(258, 171);
            this.ntxMaxCameraClipDistances.Name = "ntxMaxCameraClipDistances";
            this.ntxMaxCameraClipDistances.Size = new System.Drawing.Size(56, 20);
            this.ntxMaxCameraClipDistances.TabIndex = 46;
            this.ntxMaxCameraClipDistances.Text = "0";
            // 
            // tpCameraAttachment
            // 
            this.tpCameraAttachment.Controls.Add(this.btnCameraAttachmentEnabled);
            this.tpCameraAttachment.Controls.Add(this.chxCameraAttachmentEnabled);
            this.tpCameraAttachment.Controls.Add(this.btnCameraAttachmentOK);
            this.tpCameraAttachment.Controls.Add(this.splitContainerCameraAttachment);
            this.tpCameraAttachment.Location = new System.Drawing.Point(4, 22);
            this.tpCameraAttachment.Name = "tpCameraAttachment";
            this.tpCameraAttachment.Padding = new System.Windows.Forms.Padding(3);
            this.tpCameraAttachment.Size = new System.Drawing.Size(852, 553);
            this.tpCameraAttachment.TabIndex = 5;
            this.tpCameraAttachment.Text = "Camera Attachment";
            this.tpCameraAttachment.UseVisualStyleBackColor = true;
            // 
            // btnCameraAttachmentEnabled
            // 
            this.btnCameraAttachmentEnabled.Location = new System.Drawing.Point(181, 3);
            this.btnCameraAttachmentEnabled.Name = "btnCameraAttachmentEnabled";
            this.btnCameraAttachmentEnabled.Size = new System.Drawing.Size(36, 23);
            this.btnCameraAttachmentEnabled.TabIndex = 105;
            this.btnCameraAttachmentEnabled.Text = "OK";
            this.btnCameraAttachmentEnabled.UseVisualStyleBackColor = true;
            this.btnCameraAttachmentEnabled.Click += new System.EventHandler(this.btnCameraAttachmentEnabled_Click);
            // 
            // chxCameraAttachmentEnabled
            // 
            this.chxCameraAttachmentEnabled.AutoSize = true;
            this.chxCameraAttachmentEnabled.Location = new System.Drawing.Point(14, 7);
            this.chxCameraAttachmentEnabled.Name = "chxCameraAttachmentEnabled";
            this.chxCameraAttachmentEnabled.Size = new System.Drawing.Size(161, 17);
            this.chxCameraAttachmentEnabled.TabIndex = 103;
            this.chxCameraAttachmentEnabled.Text = "Camera Attachment Enabled";
            this.chxCameraAttachmentEnabled.UseVisualStyleBackColor = true;
            // 
            // btnCameraAttachmentOK
            // 
            this.btnCameraAttachmentOK.Location = new System.Drawing.Point(685, 524);
            this.btnCameraAttachmentOK.Name = "btnCameraAttachmentOK";
            this.btnCameraAttachmentOK.Size = new System.Drawing.Size(75, 23);
            this.btnCameraAttachmentOK.TabIndex = 104;
            this.btnCameraAttachmentOK.Text = "OK";
            this.btnCameraAttachmentOK.UseVisualStyleBackColor = true;
            this.btnCameraAttachmentOK.Click += new System.EventHandler(this.btnCameraAttachmentOK_Click);
            // 
            // splitContainerCameraAttachment
            // 
            this.splitContainerCameraAttachment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerCameraAttachment.Location = new System.Drawing.Point(3, 30);
            this.splitContainerCameraAttachment.Name = "splitContainerCameraAttachment";
            // 
            // splitContainerCameraAttachment.Panel1
            // 
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.lsvCameraAttachmentSrc);
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.gbLookAt);
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.label34);
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.label33);
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.ntxCameraAttachmentSrcAPIndex);
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.ctrl3DVectorAttachmentCameraOffset);
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.chxLookAt);
            this.splitContainerCameraAttachment.Panel1.Controls.Add(this.treeViewCameraAttachmentSrc);
            // 
            // splitContainerCameraAttachment.Panel2
            // 
            this.splitContainerCameraAttachment.Panel2.Controls.Add(this.lsvCameraAttachmentLookAt);
            this.splitContainerCameraAttachment.Panel2.Controls.Add(this.label38);
            this.splitContainerCameraAttachment.Panel2.Controls.Add(this.ntxCameraAttachmentLookAtAPIndex);
            this.splitContainerCameraAttachment.Panel2.Controls.Add(this.treeViewCameraAttachmentlookAt);
            this.splitContainerCameraAttachment.Panel2.Enabled = false;
            this.splitContainerCameraAttachment.Size = new System.Drawing.Size(843, 488);
            this.splitContainerCameraAttachment.SplitterDistance = 393;
            this.splitContainerCameraAttachment.TabIndex = 82;
            // 
            // lsvCameraAttachmentSrc
            // 
            this.lsvCameraAttachmentSrc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colID,
            this.colName});
            this.lsvCameraAttachmentSrc.Enabled = false;
            this.lsvCameraAttachmentSrc.HideSelection = false;
            this.lsvCameraAttachmentSrc.Location = new System.Drawing.Point(213, 3);
            this.lsvCameraAttachmentSrc.MultiSelect = false;
            this.lsvCameraAttachmentSrc.Name = "lsvCameraAttachmentSrc";
            this.lsvCameraAttachmentSrc.Size = new System.Drawing.Size(140, 262);
            this.lsvCameraAttachmentSrc.TabIndex = 102;
            this.lsvCameraAttachmentSrc.UseCompatibleStateImageBehavior = false;
            this.lsvCameraAttachmentSrc.View = System.Windows.Forms.View.Details;
            this.lsvCameraAttachmentSrc.Visible = false;
            // 
            // colID
            // 
            this.colID.Text = "ID";
            this.colID.Width = 35;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 100;
            // 
            // gbLookAt
            // 
            this.gbLookAt.Controls.Add(this.gbAdditionalOrientation);
            this.gbLookAt.Controls.Add(this.chxAttachOrientation);
            this.gbLookAt.Location = new System.Drawing.Point(3, 351);
            this.gbLookAt.Name = "gbLookAt";
            this.gbLookAt.Size = new System.Drawing.Size(350, 129);
            this.gbLookAt.TabIndex = 101;
            this.gbLookAt.TabStop = false;
            // 
            // gbAdditionalOrientation
            // 
            this.gbAdditionalOrientation.Controls.Add(this.ntxAdditionalRoll);
            this.gbAdditionalOrientation.Controls.Add(this.label30);
            this.gbAdditionalOrientation.Controls.Add(this.ntxAdditionalPitch);
            this.gbAdditionalOrientation.Controls.Add(this.label31);
            this.gbAdditionalOrientation.Controls.Add(this.ntxAdditionalYaw);
            this.gbAdditionalOrientation.Controls.Add(this.label32);
            this.gbAdditionalOrientation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbAdditionalOrientation.Location = new System.Drawing.Point(3, 35);
            this.gbAdditionalOrientation.Name = "gbAdditionalOrientation";
            this.gbAdditionalOrientation.Size = new System.Drawing.Size(344, 91);
            this.gbAdditionalOrientation.TabIndex = 102;
            this.gbAdditionalOrientation.TabStop = false;
            // 
            // ntxAdditionalRoll
            // 
            this.ntxAdditionalRoll.Location = new System.Drawing.Point(89, 64);
            this.ntxAdditionalRoll.Name = "ntxAdditionalRoll";
            this.ntxAdditionalRoll.Size = new System.Drawing.Size(90, 20);
            this.ntxAdditionalRoll.TabIndex = 92;
            this.ntxAdditionalRoll.Text = "0";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(3, 16);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(80, 13);
            this.label30.TabIndex = 82;
            this.label30.Text = "Additional Yaw:";
            // 
            // ntxAdditionalPitch
            // 
            this.ntxAdditionalPitch.Location = new System.Drawing.Point(89, 38);
            this.ntxAdditionalPitch.Name = "ntxAdditionalPitch";
            this.ntxAdditionalPitch.Size = new System.Drawing.Size(90, 20);
            this.ntxAdditionalPitch.TabIndex = 91;
            this.ntxAdditionalPitch.Text = "0";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(3, 41);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(83, 13);
            this.label31.TabIndex = 86;
            this.label31.Text = "Additional Pitch:";
            // 
            // ntxAdditionalYaw
            // 
            this.ntxAdditionalYaw.Location = new System.Drawing.Point(89, 13);
            this.ntxAdditionalYaw.Name = "ntxAdditionalYaw";
            this.ntxAdditionalYaw.Size = new System.Drawing.Size(90, 20);
            this.ntxAdditionalYaw.TabIndex = 90;
            this.ntxAdditionalYaw.Text = "0";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(3, 67);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(77, 13);
            this.label32.TabIndex = 87;
            this.label32.Text = "Additional Roll:";
            // 
            // chxAttachOrientation
            // 
            this.chxAttachOrientation.AutoSize = true;
            this.chxAttachOrientation.Location = new System.Drawing.Point(6, 19);
            this.chxAttachOrientation.Name = "chxAttachOrientation";
            this.chxAttachOrientation.Size = new System.Drawing.Size(344, 17);
            this.chxAttachOrientation.TabIndex = 85;
            this.chxAttachOrientation.Text = "Attach Orientation(Additional angle relevant only when it\'s checked)";
            this.chxAttachOrientation.UseVisualStyleBackColor = true;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 274);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(97, 13);
            this.label34.TabIndex = 89;
            this.label34.Text = "Attach Point Index:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 300);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(38, 13);
            this.label33.TabIndex = 88;
            this.label33.Text = "Offset:";
            // 
            // ntxCameraAttachmentSrcAPIndex
            // 
            this.ntxCameraAttachmentSrcAPIndex.Location = new System.Drawing.Point(109, 271);
            this.ntxCameraAttachmentSrcAPIndex.Name = "ntxCameraAttachmentSrcAPIndex";
            this.ntxCameraAttachmentSrcAPIndex.Size = new System.Drawing.Size(90, 20);
            this.ntxCameraAttachmentSrcAPIndex.TabIndex = 83;
            this.ntxCameraAttachmentSrcAPIndex.Text = "0";
            // 
            // ctrl3DVectorAttachmentCameraOffset
            // 
            this.ctrl3DVectorAttachmentCameraOffset.IsReadOnly = false;
            this.ctrl3DVectorAttachmentCameraOffset.Location = new System.Drawing.Point(48, 296);
            this.ctrl3DVectorAttachmentCameraOffset.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DVectorAttachmentCameraOffset.Name = "ctrl3DVectorAttachmentCameraOffset";
            this.ctrl3DVectorAttachmentCameraOffset.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorAttachmentCameraOffset.TabIndex = 84;
            this.ctrl3DVectorAttachmentCameraOffset.X = 0D;
            this.ctrl3DVectorAttachmentCameraOffset.Y = 0D;
            this.ctrl3DVectorAttachmentCameraOffset.Z = 0D;
            // 
            // chxLookAt
            // 
            this.chxLookAt.AutoSize = true;
            this.chxLookAt.Location = new System.Drawing.Point(9, 328);
            this.chxLookAt.Name = "chxLookAt";
            this.chxLookAt.Size = new System.Drawing.Size(63, 17);
            this.chxLookAt.TabIndex = 81;
            this.chxLookAt.Text = "Look-At";
            this.chxLookAt.UseVisualStyleBackColor = true;
            this.chxLookAt.CheckedChanged += new System.EventHandler(this.chxLookAt_CheckedChanged);
            // 
            // treeViewCameraAttachmentSrc
            // 
            this.treeViewCameraAttachmentSrc.HideSelection = false;
            this.treeViewCameraAttachmentSrc.Location = new System.Drawing.Point(3, 3);
            this.treeViewCameraAttachmentSrc.Name = "treeViewCameraAttachmentSrc";
            this.treeViewCameraAttachmentSrc.Size = new System.Drawing.Size(209, 262);
            this.treeViewCameraAttachmentSrc.TabIndex = 80;
            this.treeViewCameraAttachmentSrc.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCameraAttachmentSrc_AfterSelect);
            this.treeViewCameraAttachmentSrc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewCameraAttachmentSrc_MouseDown);
            // 
            // lsvCameraAttachmentLookAt
            // 
            this.lsvCameraAttachmentLookAt.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LookAtColID,
            this.LookAtColName});
            this.lsvCameraAttachmentLookAt.Enabled = false;
            this.lsvCameraAttachmentLookAt.HideSelection = false;
            this.lsvCameraAttachmentLookAt.Location = new System.Drawing.Point(216, 3);
            this.lsvCameraAttachmentLookAt.Name = "lsvCameraAttachmentLookAt";
            this.lsvCameraAttachmentLookAt.Size = new System.Drawing.Size(142, 262);
            this.lsvCameraAttachmentLookAt.TabIndex = 103;
            this.lsvCameraAttachmentLookAt.UseCompatibleStateImageBehavior = false;
            this.lsvCameraAttachmentLookAt.View = System.Windows.Forms.View.Details;
            this.lsvCameraAttachmentLookAt.Visible = false;
            // 
            // LookAtColID
            // 
            this.LookAtColID.Text = "ID";
            this.LookAtColID.Width = 35;
            // 
            // LookAtColName
            // 
            this.LookAtColName.Text = "Name";
            this.LookAtColName.Width = 100;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(10, 274);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(97, 13);
            this.label38.TabIndex = 100;
            this.label38.Text = "Attach Point Index:";
            // 
            // ntxCameraAttachmentLookAtAPIndex
            // 
            this.ntxCameraAttachmentLookAtAPIndex.Location = new System.Drawing.Point(113, 271);
            this.ntxCameraAttachmentLookAtAPIndex.Name = "ntxCameraAttachmentLookAtAPIndex";
            this.ntxCameraAttachmentLookAtAPIndex.Size = new System.Drawing.Size(90, 20);
            this.ntxCameraAttachmentLookAtAPIndex.TabIndex = 99;
            this.ntxCameraAttachmentLookAtAPIndex.Text = "0";
            // 
            // treeViewCameraAttachmentlookAt
            // 
            this.treeViewCameraAttachmentlookAt.HideSelection = false;
            this.treeViewCameraAttachmentlookAt.Location = new System.Drawing.Point(13, 3);
            this.treeViewCameraAttachmentlookAt.Name = "treeViewCameraAttachmentlookAt";
            this.treeViewCameraAttachmentlookAt.Size = new System.Drawing.Size(199, 262);
            this.treeViewCameraAttachmentlookAt.TabIndex = 81;
            this.treeViewCameraAttachmentlookAt.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCameraAttachmentlookAt_AfterSelect);
            // 
            // ucCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.m_CameraTabCtrl);
            this.Name = "ucCamera";
            this.Size = new System.Drawing.Size(860, 579);
            this.gbCoordinateConversionsScreen.ResumeLayout(false);
            this.gbCoordinateConversionsScreen.PerformLayout();
            this.m_CameraTabCtrl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tpAnyMapType.ResumeLayout(false);
            this.tpAnyMapType.PerformLayout();
            this.gbRotateCameraAroundWorldPoint.ResumeLayout(false);
            this.gbRotateCameraAroundWorldPoint.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbCameraWorldVisibleArea.ResumeLayout(false);
            this.gbCameraWorldVisibleArea.PerformLayout();
            this.gbSetScreenVisibleArea.ResumeLayout(false);
            this.gbSetScreenVisibleArea.PerformLayout();
            this.gbCameraScale.ResumeLayout(false);
            this.gbCameraScale.PerformLayout();
            this.pnlCameraScale3D.ResumeLayout(false);
            this.pnlCameraScale3D.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tpCameraAttachment.ResumeLayout(false);
            this.tpCameraAttachment.PerformLayout();
            this.splitContainerCameraAttachment.Panel1.ResumeLayout(false);
            this.splitContainerCameraAttachment.Panel1.PerformLayout();
            this.splitContainerCameraAttachment.Panel2.ResumeLayout(false);
            this.splitContainerCameraAttachment.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCameraAttachment)).EndInit();
            this.splitContainerCameraAttachment.ResumeLayout(false);
            this.gbLookAt.ResumeLayout(false);
            this.gbLookAt.PerformLayout();
            this.gbAdditionalOrientation.ResumeLayout(false);
            this.gbAdditionalOrientation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCoordinateConversionsScreen;
        private System.Windows.Forms.Button btnConScreenToWorldOnPlane;
        private System.Windows.Forms.Button btnConWorldToScreen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private MCTester.Controls.Ctrl3DVector ctrl3DCameraConScreenPoint;
        private MCTester.Controls.Ctrl3DVector ctrl3DCameraConWorldPoint;
        private System.Windows.Forms.Button btnConScreenToWorldOnTerrain;
        private System.Windows.Forms.Label label9;
        private MCTester.Controls.NumericTextBox ntxPlaneLocation;
        private System.Windows.Forms.CheckBox chxIsIntersectionFound;
        private System.Windows.Forms.TabControl m_CameraTabCtrl;
        private System.Windows.Forms.TabPage tabPage2;
        private MCTester.Controls.CtrlSamplePoint ctrScreenPt;
        private MCTester.Controls.CtrlSamplePoint ctrWorldPt;
        private System.Windows.Forms.TabPage tpAnyMapType;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label8;
        private MCTester.Controls.NumericTextBox ntxScrollCameraDeltaX;
        private MCTester.Controls.NumericTextBox ntxScrollCameraDeltaY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private MCTester.Controls.CtrlSamplePoint ctrlLookAtPt;
        private System.Windows.Forms.CheckBox chxFVectorRelative;
        private MCTester.Controls.Ctrl3DOrientation ctrlRotateCamera;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label16;
        private MCTester.Controls.NumericTextBox ntxMinCameraClipDistances;
        private MCTester.Controls.NumericTextBox ntxMaxCameraClipDistances;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private MCTester.Controls.NumericTextBox ntxMinRelativeHeightLimits;
        private MCTester.Controls.NumericTextBox ntxMaxRelativeHeightLimits;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chxCameraRelativeHeightLimits;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxFieldOfView;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.Ctrl3DVector ctrl3DForwardVector;
        private MCTester.Controls.Ctrl3DVector ctrl3DLookAt;
        private System.Windows.Forms.Button btnScrollCameraOK;
        private System.Windows.Forms.Button btnLookAtPointOK;
        private System.Windows.Forms.Button btnForwardVectorOK;
        private System.Windows.Forms.Button btnFieldOfViewOK;
        private System.Windows.Forms.Button btnRotateCameraRelativeToOrientationOK;
        private System.Windows.Forms.Button btnHeightLimitsOK;
        private System.Windows.Forms.Button BtnCameraClipDistancesOK;
        private System.Windows.Forms.Button btnMoveRelativeToOrientationOK;
        private System.Windows.Forms.GroupBox gbSetScreenVisibleArea;
        private System.Windows.Forms.Button btnScreenVisibleAreaOk;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private MCTester.Controls.Ctrl2DFVector ctrl2DTLScreenVisibleArea;
        private MCTester.Controls.Ctrl2DFVector ctrl2DBRScreenVisibleArea;
        private System.Windows.Forms.GroupBox gbCameraScale;
        private MCTester.Controls.NumericTextBox ntxCameraScale;
        private System.Windows.Forms.Label label19;
        private MCTester.Controls.Ctrl3DVector ctrl3DCameraScaleWorldPoint;
        private System.Windows.Forms.Button btnCameraScaleOK;
        private System.Windows.Forms.Label label3;
        private MCTester.Controls.CtrlSamplePoint ctrlCameraScaleWorldPt;
        private System.Windows.Forms.Button btnCameraPositionOK;
        private System.Windows.Forms.Button btnCameraOrientationOK;
        private System.Windows.Forms.TextBox txtMapType;
        private MCTester.Controls.CtrlSamplePoint ctrlCameraPositionPt;
        private System.Windows.Forms.Button btnCameraUpVectorOK;
        private System.Windows.Forms.CheckBox chxOrientationRelative;
        private System.Windows.Forms.Label label21;
        private MCTester.Controls.Ctrl3DOrientation ctrlCameraOrientation;
        private MCTester.Controls.Ctrl3DVector ctrl3DCameraUpVector;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox chxPositionRelative;
        private MCTester.Controls.Ctrl3DVector ctrl3DMoveCameraRelativeToOrientation;
        private System.Windows.Forms.CheckBox chxXYDirectionOnly;
        private System.Windows.Forms.CheckBox chxUpVectorRelativeToOrientation;
        private MCTester.Controls.Ctrl3DVector ctrl3DCameraPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chxRenderInTwoSessions;
        private System.Windows.Forms.Button btnCameraFootPrintOK;
        private System.Windows.Forms.CheckBox chxFootprintIsDefined;
        private System.Windows.Forms.GroupBox gbCameraWorldVisibleArea;
        private System.Windows.Forms.Button btnCameraWorldVisibleAreaOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCameraCenterOffset;
        private System.Windows.Forms.Label label29;
        private MCTester.Controls.Ctrl2DFVector ctrl2DFVectorCenterOffset;
        private System.Windows.Forms.TabPage tpCameraAttachment;
        private System.Windows.Forms.TreeView treeViewCameraAttachmentlookAt;
        private System.Windows.Forms.TreeView treeViewCameraAttachmentSrc;
        private System.Windows.Forms.SplitContainer splitContainerCameraAttachment;
        private System.Windows.Forms.CheckBox chxAttachOrientation;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorAttachmentCameraOffset;
        private MCTester.Controls.NumericTextBox ntxCameraAttachmentSrcAPIndex;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox chxLookAt;
        private MCTester.Controls.NumericTextBox ntxAdditionalRoll;
        private MCTester.Controls.NumericTextBox ntxAdditionalPitch;
        private MCTester.Controls.NumericTextBox ntxAdditionalYaw;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label38;
        private MCTester.Controls.NumericTextBox ntxCameraAttachmentLookAtAPIndex;
        private System.Windows.Forms.GroupBox gbAdditionalOrientation;
        private System.Windows.Forms.GroupBox gbLookAt;
        private System.Windows.Forms.ListView lsvCameraAttachmentSrc;
        private System.Windows.Forms.ColumnHeader colID;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ListView lsvCameraAttachmentLookAt;
        private System.Windows.Forms.ColumnHeader LookAtColID;
        private System.Windows.Forms.ColumnHeader LookAtColName;
        private System.Windows.Forms.Button btnCameraAttachmentOK;
        private System.Windows.Forms.CheckBox chxCameraAttachmentEnabled;
        private System.Windows.Forms.Button btnCameraAttachmentEnabled;
        private System.Windows.Forms.Button btnGetCameraScale3D;
        private System.Windows.Forms.Button btnGetWorldVisibleArea;
        private MCTester.Controls.NumericTextBox ntxWorldVisibleAreaMargin;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cmbScreenVisibleAreaOperation;
        private System.Windows.Forms.Label label37;
        private MCTester.Controls.Ctrl3DVector ctrl3DCameraConPlaneNormal;
        private System.Windows.Forms.GroupBox gbRotateCameraAroundWorldPoint;
        private System.Windows.Forms.Label label42;
        private MCTester.Controls.Ctrl3DVector ctrl3DRotateCameraAroundWorldPointPivotPoint;
        private System.Windows.Forms.CheckBox chxRotateCameraAroundWorldPointRelativeToOrientation;
        private MCTester.Controls.NumericTextBox ntxRotateCameraAroundWorldPointDeltaRoll;
        private System.Windows.Forms.Label label41;
        private MCTester.Controls.NumericTextBox ntxRotateCameraAroundWorldPointDeltaPitch;
        private System.Windows.Forms.Label label40;
        private MCTester.Controls.NumericTextBox ntxRotateCameraAroundWorldPointDeltaYaw;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button btnRotateCameraAroundWorldPoint;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePointctrl3DRotateCameraAroundWorldPointPivotPt;
        private Controls.NumericTextBox ntxRectangleYaw;
        private System.Windows.Forms.Label label43;
        private Controls.CtrlSamplePoint ctrlSamplePointBRScreenVisibleArea;
        private Controls.CtrlSamplePoint ctrlSamplePointTLScreenVisibleArea;
        private System.Windows.Forms.Panel pnlCameraScale3D;
        private System.Windows.Forms.Label lblToIC;
        private Controls.CtrlSMcBox boxWorldVisibleArea;
    }
}
