namespace MCTester.GUI.Map
{
    partial class MapLoaderDefinitionForm
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
            this.tcMapLoaderDefinition = new System.Windows.Forms.TabControl();
            this.tpSchemasList = new System.Windows.Forms.TabPage();
            this.ntxTerrainResolutionFactor = new MCTester.Controls.NumericTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.chxMultiThreadedDevice = new System.Windows.Forms.CheckBox();
            this.chxMultiScreenDevice = new System.Windows.Forms.CheckBox();
            this.chxIsWpfWindow = new System.Windows.Forms.CheckBox();
            this.ntxNumBackgroundThreads = new MCTester.Controls.NumericTextBox();
            this.lstMapSchemas = new System.Windows.Forms.ListView();
            this.colSchemaNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colArea = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMapType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOMCoordSys = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colComments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadSchema = new System.Windows.Forms.Button();
            this.tpSchemasDefine = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tcSchemesDefine = new System.Windows.Forms.TabControl();
            this.tpDevice = new System.Windows.Forms.TabPage();
            this.ctrlDeviceParams1 = new MCTester.Controls.CtrlDeviceParams();
            this.btnSaveDevice = new System.Windows.Forms.Button();
            this.tpScheme = new System.Windows.Forms.TabPage();
            this.cmbScheme_ID = new System.Windows.Forms.ComboBox();
            this.btnScheme_New = new System.Windows.Forms.Button();
            this.btnSaveSchema = new System.Windows.Forms.Button();
            this.txtScheme_Comment = new System.Windows.Forms.TextBox();
            this.txtScheme_MapType = new System.Windows.Forms.TextBox();
            this.txtScheme_Area = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tpViewport = new System.Windows.Forms.TabPage();
            this.cmbViewport_ID = new System.Windows.Forms.ComboBox();
            this.btnViewport_New = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbViewport_DtmUsageAndPrecision = new System.Windows.Forms.ComboBox();
            this.btnSaveViewport = new System.Windows.Forms.Button();
            this.txtViewport_Name = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chxViewport_ShowGeoInMetricProportion = new System.Windows.Forms.CheckBox();
            this.lblMapType = new System.Windows.Forms.Label();
            this.cmbViewport_MapType = new System.Windows.Forms.ComboBox();
            this.ntxViewport_TerrainObjectBestResolution = new MCTester.Controls.NumericTextBox();
            this.ctrlViewport_CameraPosition = new MCTester.Controls.Ctrl3DVector();
            this.tpTerrain = new System.Windows.Forms.TabPage();
            this.cmbTerrain_ID = new System.Windows.Forms.ComboBox();
            this.btnTerrain_New = new System.Windows.Forms.Button();
            this.btnSaveTerrain = new System.Windows.Forms.Button();
            this.txtTerrain_Name = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tpLayer = new System.Windows.Forms.TabPage();
            this.ucLayerParams1 = new MCTester.Controls.ucLayerParams();
            this.cmbLayer_ID = new System.Windows.Forms.ComboBox();
            this.btnLayer_New = new System.Windows.Forms.Button();
            this.btnSaveLayer = new System.Windows.Forms.Button();
            this.txtLayer_Name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tpOverlayManager = new System.Windows.Forms.TabPage();
            this.cmbOverlayManager_ID = new System.Windows.Forms.ComboBox();
            this.btnOverlayManager_New = new System.Windows.Forms.Button();
            this.txtOverlayManager_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveOverlayManager = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.tpCoordinateSystem = new System.Windows.Forms.TabPage();
            this.ctrlNewGridCoordinateSystem1 = new MCTester.General_Forms.CtrlNewGridCoordinateSystem();
            this.cmbGridCoordinateSystem_ID = new System.Windows.Forms.ComboBox();
            this.btnGridCoordinateSystem_New = new System.Windows.Forms.Button();
            this.btnSaveGridCoordinateSystem = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.tpImageCalc = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ctrlImageCalc_ctrlCameraParams1 = new MCTester.Controls.CtrlCameraParams();
            this.cmbImageCalc_ID = new System.Windows.Forms.ComboBox();
            this.btnImageCalc_New = new System.Windows.Forms.Button();
            this.txtImageCalc_Name = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.btnSaveImageCalc = new System.Windows.Forms.Button();
            this.chxImageCalc_IsFileName = new System.Windows.Forms.CheckBox();
            this.cmbImageCalc_Type = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.ctrlImageCalc_XmlPath = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlImageCalc_DtmMapLayerPath = new MCTester.Controls.CtrlBrowseControl();
            this.lstTargetGridCoordinateSystem = new System.Windows.Forms.ListBox();
            this.lblTargetGridCoordinateSystem = new System.Windows.Forms.Label();
            this.lstImageCalc = new System.Windows.Forms.ListBox();
            this.lblImageCalc = new System.Windows.Forms.Label();
            this.lstOverlayManager = new System.Windows.Forms.ListBox();
            this.lblOverlayManager = new System.Windows.Forms.Label();
            this.lstGridCoordinateSystem = new System.Windows.Forms.ListBox();
            this.lblGridCoordSys = new System.Windows.Forms.Label();
            this.clstChilds = new System.Windows.Forms.CheckedListBox();
            this.lblChild = new System.Windows.Forms.Label();
            this.tcMapLoaderDefinition.SuspendLayout();
            this.tpSchemasList.SuspendLayout();
            this.tpSchemasDefine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tcSchemesDefine.SuspendLayout();
            this.tpDevice.SuspendLayout();
            this.tpScheme.SuspendLayout();
            this.tpViewport.SuspendLayout();
            this.tpTerrain.SuspendLayout();
            this.tpLayer.SuspendLayout();
            this.tpOverlayManager.SuspendLayout();
            this.tpCoordinateSystem.SuspendLayout();
            this.tpImageCalc.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMapLoaderDefinition
            // 
            this.tcMapLoaderDefinition.Controls.Add(this.tpSchemasList);
            this.tcMapLoaderDefinition.Controls.Add(this.tpSchemasDefine);
            this.tcMapLoaderDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMapLoaderDefinition.Location = new System.Drawing.Point(0, 0);
            this.tcMapLoaderDefinition.Name = "tcMapLoaderDefinition";
            this.tcMapLoaderDefinition.SelectedIndex = 0;
            this.tcMapLoaderDefinition.Size = new System.Drawing.Size(1159, 499);
            this.tcMapLoaderDefinition.TabIndex = 0;
            this.tcMapLoaderDefinition.SelectedIndexChanged += new System.EventHandler(this.tcMapLoaderDefinition_SelectedIndexChanged);
            // 
            // tpSchemasList
            // 
            this.tpSchemasList.AutoScroll = true;
            this.tpSchemasList.Controls.Add(this.ntxTerrainResolutionFactor);
            this.tpSchemasList.Controls.Add(this.label23);
            this.tpSchemasList.Controls.Add(this.chxMultiThreadedDevice);
            this.tpSchemasList.Controls.Add(this.chxMultiScreenDevice);
            this.tpSchemasList.Controls.Add(this.chxIsWpfWindow);
            this.tpSchemasList.Controls.Add(this.ntxNumBackgroundThreads);
            this.tpSchemasList.Controls.Add(this.lstMapSchemas);
            this.tpSchemasList.Controls.Add(this.label2);
            this.tpSchemasList.Controls.Add(this.btnLoadSchema);
            this.tpSchemasList.Location = new System.Drawing.Point(4, 22);
            this.tpSchemasList.Name = "tpSchemasList";
            this.tpSchemasList.Padding = new System.Windows.Forms.Padding(3);
            this.tpSchemasList.Size = new System.Drawing.Size(1151, 473);
            this.tpSchemasList.TabIndex = 0;
            this.tpSchemasList.Text = "Schemas";
            this.tpSchemasList.UseVisualStyleBackColor = true;
            // 
            // ntxTerrainResolutionFactor
            // 
            this.ntxTerrainResolutionFactor.Location = new System.Drawing.Point(378, 447);
            this.ntxTerrainResolutionFactor.Name = "ntxTerrainResolutionFactor";
            this.ntxTerrainResolutionFactor.Size = new System.Drawing.Size(49, 20);
            this.ntxTerrainResolutionFactor.TabIndex = 11;
            this.ntxTerrainResolutionFactor.Text = "1";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(244, 450);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(129, 13);
            this.label23.TabIndex = 10;
            this.label23.Text = "Terrain Resolution Factor:";
            // 
            // chxMultiThreadedDevice
            // 
            this.chxMultiThreadedDevice.AutoSize = true;
            this.chxMultiThreadedDevice.Location = new System.Drawing.Point(533, 449);
            this.chxMultiThreadedDevice.Name = "chxMultiThreadedDevice";
            this.chxMultiThreadedDevice.Size = new System.Drawing.Size(97, 17);
            this.chxMultiThreadedDevice.TabIndex = 9;
            this.chxMultiThreadedDevice.Text = "Multi Threaded";
            this.chxMultiThreadedDevice.UseVisualStyleBackColor = true;
            // 
            // chxMultiScreenDevice
            // 
            this.chxMultiScreenDevice.AutoSize = true;
            this.chxMultiScreenDevice.Location = new System.Drawing.Point(640, 449);
            this.chxMultiScreenDevice.Name = "chxMultiScreenDevice";
            this.chxMultiScreenDevice.Size = new System.Drawing.Size(122, 17);
            this.chxMultiScreenDevice.TabIndex = 8;
            this.chxMultiScreenDevice.Text = "Multi Screen Device";
            this.chxMultiScreenDevice.UseVisualStyleBackColor = true;
            // 
            // chxIsWpfWindow
            // 
            this.chxIsWpfWindow.AutoSize = true;
            this.chxIsWpfWindow.Location = new System.Drawing.Point(788, 449);
            this.chxIsWpfWindow.Name = "chxIsWpfWindow";
            this.chxIsWpfWindow.Size = new System.Drawing.Size(131, 17);
            this.chxIsWpfWindow.TabIndex = 7;
            this.chxIsWpfWindow.Text = "Open as Wpf Window";
            this.chxIsWpfWindow.UseVisualStyleBackColor = true;
            this.chxIsWpfWindow.CheckedChanged += new System.EventHandler(this.chxIsWpfWindow_CheckedChanged);
            // 
            // ntxNumBackgroundThreads
            // 
            this.ntxNumBackgroundThreads.Location = new System.Drawing.Point(167, 447);
            this.ntxNumBackgroundThreads.Name = "ntxNumBackgroundThreads";
            this.ntxNumBackgroundThreads.Size = new System.Drawing.Size(49, 20);
            this.ntxNumBackgroundThreads.TabIndex = 6;
            this.ntxNumBackgroundThreads.Text = "1";
            // 
            // lstMapSchemas
            // 
            this.lstMapSchemas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSchemaNum,
            this.colArea,
            this.colMapType,
            this.colOMCoordSys,
            this.colComments});
            this.lstMapSchemas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstMapSchemas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstMapSchemas.FullRowSelect = true;
            this.lstMapSchemas.GridLines = true;
            this.lstMapSchemas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstMapSchemas.HideSelection = false;
            this.lstMapSchemas.Location = new System.Drawing.Point(3, 3);
            this.lstMapSchemas.Name = "lstMapSchemas";
            this.lstMapSchemas.Size = new System.Drawing.Size(1145, 436);
            this.lstMapSchemas.TabIndex = 5;
            this.lstMapSchemas.UseCompatibleStateImageBehavior = false;
            this.lstMapSchemas.View = System.Windows.Forms.View.Details;
            this.lstMapSchemas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstMapSchemas_MouseDoubleClick);
            // 
            // colSchemaNum
            // 
            this.colSchemaNum.Text = "Number";
            this.colSchemaNum.Width = 61;
            // 
            // colArea
            // 
            this.colArea.Text = "Area";
            this.colArea.Width = 129;
            // 
            // colMapType
            // 
            this.colMapType.Text = "Map Type";
            this.colMapType.Width = 134;
            // 
            // colOMCoordSys
            // 
            this.colOMCoordSys.Text = "OM Coordinates";
            this.colOMCoordSys.Width = 157;
            // 
            // colComments
            // 
            this.colComments.Text = "Comments";
            this.colComments.Width = 518;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 450);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number Background Threads:";
            // 
            // btnLoadSchema
            // 
            this.btnLoadSchema.Location = new System.Drawing.Point(1070, 445);
            this.btnLoadSchema.Name = "btnLoadSchema";
            this.btnLoadSchema.Size = new System.Drawing.Size(75, 23);
            this.btnLoadSchema.TabIndex = 2;
            this.btnLoadSchema.Text = "Load";
            this.btnLoadSchema.UseVisualStyleBackColor = true;
            this.btnLoadSchema.Click += new System.EventHandler(this.btnLoadSchema_Click);
            // 
            // tpSchemasDefine
            // 
            this.tpSchemasDefine.AutoScroll = true;
            this.tpSchemasDefine.Controls.Add(this.splitContainer1);
            this.tpSchemasDefine.Location = new System.Drawing.Point(4, 22);
            this.tpSchemasDefine.Name = "tpSchemasDefine";
            this.tpSchemasDefine.Padding = new System.Windows.Forms.Padding(3);
            this.tpSchemasDefine.Size = new System.Drawing.Size(1151, 473);
            this.tpSchemasDefine.TabIndex = 1;
            this.tpSchemasDefine.Text = "Define";
            this.tpSchemasDefine.UseVisualStyleBackColor = true;
            this.tpSchemasDefine.Leave += new System.EventHandler(this.tpSchemasDefine_Leave);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tcSchemesDefine);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lstTargetGridCoordinateSystem);
            this.splitContainer1.Panel2.Controls.Add(this.lblTargetGridCoordinateSystem);
            this.splitContainer1.Panel2.Controls.Add(this.lstImageCalc);
            this.splitContainer1.Panel2.Controls.Add(this.lblImageCalc);
            this.splitContainer1.Panel2.Controls.Add(this.lstOverlayManager);
            this.splitContainer1.Panel2.Controls.Add(this.lblOverlayManager);
            this.splitContainer1.Panel2.Controls.Add(this.lstGridCoordinateSystem);
            this.splitContainer1.Panel2.Controls.Add(this.lblGridCoordSys);
            this.splitContainer1.Panel2.Controls.Add(this.clstChilds);
            this.splitContainer1.Panel2.Controls.Add(this.lblChild);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(1145, 467);
            this.splitContainer1.SplitterDistance = 862;
            this.splitContainer1.TabIndex = 1;
            // 
            // tcSchemesDefine
            // 
            this.tcSchemesDefine.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tcSchemesDefine.Controls.Add(this.tpDevice);
            this.tcSchemesDefine.Controls.Add(this.tpScheme);
            this.tcSchemesDefine.Controls.Add(this.tpViewport);
            this.tcSchemesDefine.Controls.Add(this.tpTerrain);
            this.tcSchemesDefine.Controls.Add(this.tpLayer);
            this.tcSchemesDefine.Controls.Add(this.tpOverlayManager);
            this.tcSchemesDefine.Controls.Add(this.tpCoordinateSystem);
            this.tcSchemesDefine.Controls.Add(this.tpImageCalc);
            this.tcSchemesDefine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSchemesDefine.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tcSchemesDefine.ItemSize = new System.Drawing.Size(25, 100);
            this.tcSchemesDefine.Location = new System.Drawing.Point(0, 0);
            this.tcSchemesDefine.Multiline = true;
            this.tcSchemesDefine.Name = "tcSchemesDefine";
            this.tcSchemesDefine.SelectedIndex = 0;
            this.tcSchemesDefine.Size = new System.Drawing.Size(862, 467);
            this.tcSchemesDefine.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcSchemesDefine.TabIndex = 0;
            this.tcSchemesDefine.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tcSchemesDefine_DrawItem);
            this.tcSchemesDefine.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcSchemesDefine_Selected);
            // 
            // tpDevice
            // 
            this.tpDevice.AutoScroll = true;
            this.tpDevice.Controls.Add(this.ctrlDeviceParams1);
            this.tpDevice.Controls.Add(this.btnSaveDevice);
            this.tpDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tpDevice.Location = new System.Drawing.Point(104, 4);
            this.tpDevice.Name = "tpDevice";
            this.tpDevice.Size = new System.Drawing.Size(754, 459);
            this.tpDevice.TabIndex = 9;
            this.tpDevice.Text = "Device";
            this.tpDevice.UseVisualStyleBackColor = true;
            // 
            // ctrlDeviceParams1
            // 
            this.ctrlDeviceParams1.AutoScroll = true;
            this.ctrlDeviceParams1.Location = new System.Drawing.Point(3, 4);
            this.ctrlDeviceParams1.Name = "ctrlDeviceParams1";
            this.ctrlDeviceParams1.Size = new System.Drawing.Size(349, 452);
            this.ctrlDeviceParams1.TabIndex = 124;
            // 
            // btnSaveDevice
            // 
            this.btnSaveDevice.Location = new System.Drawing.Point(676, 433);
            this.btnSaveDevice.Name = "btnSaveDevice";
            this.btnSaveDevice.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDevice.TabIndex = 120;
            this.btnSaveDevice.Text = "Save";
            this.btnSaveDevice.UseVisualStyleBackColor = true;
            this.btnSaveDevice.Click += new System.EventHandler(this.btnSaveDevice_Click);
            // 
            // tpScheme
            // 
            this.tpScheme.AutoScroll = true;
            this.tpScheme.Controls.Add(this.cmbScheme_ID);
            this.tpScheme.Controls.Add(this.btnScheme_New);
            this.tpScheme.Controls.Add(this.btnSaveSchema);
            this.tpScheme.Controls.Add(this.txtScheme_Comment);
            this.tpScheme.Controls.Add(this.txtScheme_MapType);
            this.tpScheme.Controls.Add(this.txtScheme_Area);
            this.tpScheme.Controls.Add(this.label14);
            this.tpScheme.Controls.Add(this.label11);
            this.tpScheme.Controls.Add(this.label10);
            this.tpScheme.Controls.Add(this.label9);
            this.tpScheme.Location = new System.Drawing.Point(104, 4);
            this.tpScheme.Name = "tpScheme";
            this.tpScheme.Padding = new System.Windows.Forms.Padding(3);
            this.tpScheme.Size = new System.Drawing.Size(754, 459);
            this.tpScheme.TabIndex = 8;
            this.tpScheme.Text = "Scheme";
            // 
            // cmbScheme_ID
            // 
            this.cmbScheme_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScheme_ID.FormattingEnabled = true;
            this.cmbScheme_ID.Location = new System.Drawing.Point(75, 18);
            this.cmbScheme_ID.Name = "cmbScheme_ID";
            this.cmbScheme_ID.Size = new System.Drawing.Size(228, 21);
            this.cmbScheme_ID.TabIndex = 123;
            this.cmbScheme_ID.SelectedIndexChanged += new System.EventHandler(this.cmbScheme_ID_SelectedIndexChanged);
            // 
            // btnScheme_New
            // 
            this.btnScheme_New.Location = new System.Drawing.Point(309, 16);
            this.btnScheme_New.Name = "btnScheme_New";
            this.btnScheme_New.Size = new System.Drawing.Size(62, 23);
            this.btnScheme_New.TabIndex = 122;
            this.btnScheme_New.Text = "New";
            this.btnScheme_New.UseVisualStyleBackColor = true;
            this.btnScheme_New.Click += new System.EventHandler(this.btnScheme_New_Click);
            // 
            // btnSaveSchema
            // 
            this.btnSaveSchema.Location = new System.Drawing.Point(656, 441);
            this.btnSaveSchema.Name = "btnSaveSchema";
            this.btnSaveSchema.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSchema.TabIndex = 119;
            this.btnSaveSchema.Text = "Save";
            this.btnSaveSchema.UseVisualStyleBackColor = true;
            this.btnSaveSchema.Click += new System.EventHandler(this.btnSaveSchema_Click);
            // 
            // txtScheme_Comment
            // 
            this.txtScheme_Comment.Location = new System.Drawing.Point(75, 94);
            this.txtScheme_Comment.Multiline = true;
            this.txtScheme_Comment.Name = "txtScheme_Comment";
            this.txtScheme_Comment.Size = new System.Drawing.Size(296, 98);
            this.txtScheme_Comment.TabIndex = 118;
            // 
            // txtScheme_MapType
            // 
            this.txtScheme_MapType.Location = new System.Drawing.Point(75, 68);
            this.txtScheme_MapType.Name = "txtScheme_MapType";
            this.txtScheme_MapType.Size = new System.Drawing.Size(296, 20);
            this.txtScheme_MapType.TabIndex = 115;
            // 
            // txtScheme_Area
            // 
            this.txtScheme_Area.Location = new System.Drawing.Point(75, 42);
            this.txtScheme_Area.Name = "txtScheme_Area";
            this.txtScheme_Area.Size = new System.Drawing.Size(296, 20);
            this.txtScheme_Area.TabIndex = 114;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 13);
            this.label14.TabIndex = 113;
            this.label14.Text = "Comment:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 110;
            this.label11.Text = "Map Type:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 109;
            this.label10.Text = "Area:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 107;
            this.label9.Text = "ID:";
            // 
            // tpViewport
            // 
            this.tpViewport.AutoScroll = true;
            this.tpViewport.Controls.Add(this.cmbViewport_ID);
            this.tpViewport.Controls.Add(this.btnViewport_New);
            this.tpViewport.Controls.Add(this.label12);
            this.tpViewport.Controls.Add(this.label13);
            this.tpViewport.Controls.Add(this.cmbViewport_DtmUsageAndPrecision);
            this.tpViewport.Controls.Add(this.btnSaveViewport);
            this.tpViewport.Controls.Add(this.txtViewport_Name);
            this.tpViewport.Controls.Add(this.label7);
            this.tpViewport.Controls.Add(this.label6);
            this.tpViewport.Controls.Add(this.label4);
            this.tpViewport.Controls.Add(this.chxViewport_ShowGeoInMetricProportion);
            this.tpViewport.Controls.Add(this.lblMapType);
            this.tpViewport.Controls.Add(this.cmbViewport_MapType);
            this.tpViewport.Controls.Add(this.ntxViewport_TerrainObjectBestResolution);
            this.tpViewport.Controls.Add(this.ctrlViewport_CameraPosition);
            this.tpViewport.Location = new System.Drawing.Point(104, 4);
            this.tpViewport.Name = "tpViewport";
            this.tpViewport.Padding = new System.Windows.Forms.Padding(3);
            this.tpViewport.Size = new System.Drawing.Size(754, 459);
            this.tpViewport.TabIndex = 0;
            this.tpViewport.Text = "Viewport";
            this.tpViewport.UseVisualStyleBackColor = true;
            // 
            // cmbViewport_ID
            // 
            this.cmbViewport_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewport_ID.FormattingEnabled = true;
            this.cmbViewport_ID.Location = new System.Drawing.Point(67, 15);
            this.cmbViewport_ID.Name = "cmbViewport_ID";
            this.cmbViewport_ID.Size = new System.Drawing.Size(186, 21);
            this.cmbViewport_ID.TabIndex = 132;
            this.cmbViewport_ID.SelectedIndexChanged += new System.EventHandler(this.cmbViewport_ID_SelectedIndexChanged);
            // 
            // btnViewport_New
            // 
            this.btnViewport_New.Location = new System.Drawing.Point(259, 13);
            this.btnViewport_New.Name = "btnViewport_New";
            this.btnViewport_New.Size = new System.Drawing.Size(40, 23);
            this.btnViewport_New.TabIndex = 131;
            this.btnViewport_New.Text = "New";
            this.btnViewport_New.UseVisualStyleBackColor = true;
            this.btnViewport_New.Click += new System.EventHandler(this.btnViewport_New_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 176);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(154, 13);
            this.label12.TabIndex = 129;
            this.label12.Text = "Terrain Object Best Resolution:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 149);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(131, 13);
            this.label13.TabIndex = 127;
            this.label13.Text = "Dtm Usage And Precision:";
            // 
            // cmbViewport_DtmUsageAndPrecision
            // 
            this.cmbViewport_DtmUsageAndPrecision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewport_DtmUsageAndPrecision.FormattingEnabled = true;
            this.cmbViewport_DtmUsageAndPrecision.Location = new System.Drawing.Point(143, 146);
            this.cmbViewport_DtmUsageAndPrecision.Name = "cmbViewport_DtmUsageAndPrecision";
            this.cmbViewport_DtmUsageAndPrecision.Size = new System.Drawing.Size(232, 21);
            this.cmbViewport_DtmUsageAndPrecision.TabIndex = 125;
            // 
            // btnSaveViewport
            // 
            this.btnSaveViewport.Location = new System.Drawing.Point(676, 433);
            this.btnSaveViewport.Name = "btnSaveViewport";
            this.btnSaveViewport.Size = new System.Drawing.Size(75, 23);
            this.btnSaveViewport.TabIndex = 121;
            this.btnSaveViewport.Text = "Save";
            this.btnSaveViewport.UseVisualStyleBackColor = true;
            this.btnSaveViewport.Click += new System.EventHandler(this.btnSaveViewport_Click);
            // 
            // txtViewport_Name
            // 
            this.txtViewport_Name.Location = new System.Drawing.Point(67, 38);
            this.txtViewport_Name.Name = "txtViewport_Name";
            this.txtViewport_Name.Size = new System.Drawing.Size(232, 20);
            this.txtViewport_Name.TabIndex = 106;
            this.txtViewport_Name.Text = "Viewport";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 105;
            this.label7.Text = "Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 103;
            this.label6.Text = "ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 96;
            this.label4.Text = "Camera Position:";
            // 
            // chxViewport_ShowGeoInMetricProportion
            // 
            this.chxViewport_ShowGeoInMetricProportion.AutoSize = true;
            this.chxViewport_ShowGeoInMetricProportion.Checked = true;
            this.chxViewport_ShowGeoInMetricProportion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxViewport_ShowGeoInMetricProportion.Location = new System.Drawing.Point(9, 123);
            this.chxViewport_ShowGeoInMetricProportion.Name = "chxViewport_ShowGeoInMetricProportion";
            this.chxViewport_ShowGeoInMetricProportion.Size = new System.Drawing.Size(171, 17);
            this.chxViewport_ShowGeoInMetricProportion.TabIndex = 94;
            this.chxViewport_ShowGeoInMetricProportion.Text = "Show Geo In Metric Proportion";
            this.chxViewport_ShowGeoInMetricProportion.UseVisualStyleBackColor = true;
            // 
            // lblMapType
            // 
            this.lblMapType.AutoSize = true;
            this.lblMapType.Location = new System.Drawing.Point(6, 67);
            this.lblMapType.Name = "lblMapType";
            this.lblMapType.Size = new System.Drawing.Size(55, 13);
            this.lblMapType.TabIndex = 93;
            this.lblMapType.Text = "MapType:";
            // 
            // cmbViewport_MapType
            // 
            this.cmbViewport_MapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewport_MapType.FormattingEnabled = true;
            this.cmbViewport_MapType.Location = new System.Drawing.Point(67, 64);
            this.cmbViewport_MapType.Name = "cmbViewport_MapType";
            this.cmbViewport_MapType.Size = new System.Drawing.Size(232, 21);
            this.cmbViewport_MapType.TabIndex = 92;
            // 
            // ntxViewport_TerrainObjectBestResolution
            // 
            this.ntxViewport_TerrainObjectBestResolution.Location = new System.Drawing.Point(166, 173);
            this.ntxViewport_TerrainObjectBestResolution.Name = "ntxViewport_TerrainObjectBestResolution";
            this.ntxViewport_TerrainObjectBestResolution.Size = new System.Drawing.Size(50, 20);
            this.ntxViewport_TerrainObjectBestResolution.TabIndex = 128;
            this.ntxViewport_TerrainObjectBestResolution.Text = "0.125";
            this.ntxViewport_TerrainObjectBestResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ctrlViewport_CameraPosition
            // 
            this.ctrlViewport_CameraPosition.IsReadOnly = false;
            this.ctrlViewport_CameraPosition.Location = new System.Drawing.Point(94, 91);
            this.ctrlViewport_CameraPosition.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlViewport_CameraPosition.Name = "ctrlViewport_CameraPosition";
            this.ctrlViewport_CameraPosition.Size = new System.Drawing.Size(232, 26);
            this.ctrlViewport_CameraPosition.TabIndex = 95;
            this.ctrlViewport_CameraPosition.X = 0D;
            this.ctrlViewport_CameraPosition.Y = 0D;
            this.ctrlViewport_CameraPosition.Z = 0D;
            // 
            // tpTerrain
            // 
            this.tpTerrain.AutoScroll = true;
            this.tpTerrain.Controls.Add(this.cmbTerrain_ID);
            this.tpTerrain.Controls.Add(this.btnTerrain_New);
            this.tpTerrain.Controls.Add(this.btnSaveTerrain);
            this.tpTerrain.Controls.Add(this.txtTerrain_Name);
            this.tpTerrain.Controls.Add(this.label16);
            this.tpTerrain.Controls.Add(this.label15);
            this.tpTerrain.Location = new System.Drawing.Point(104, 4);
            this.tpTerrain.Name = "tpTerrain";
            this.tpTerrain.Padding = new System.Windows.Forms.Padding(3);
            this.tpTerrain.Size = new System.Drawing.Size(754, 459);
            this.tpTerrain.TabIndex = 1;
            this.tpTerrain.Text = "Terrain";
            this.tpTerrain.UseVisualStyleBackColor = true;
            // 
            // cmbTerrain_ID
            // 
            this.cmbTerrain_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTerrain_ID.FormattingEnabled = true;
            this.cmbTerrain_ID.Location = new System.Drawing.Point(50, 14);
            this.cmbTerrain_ID.Name = "cmbTerrain_ID";
            this.cmbTerrain_ID.Size = new System.Drawing.Size(186, 21);
            this.cmbTerrain_ID.TabIndex = 134;
            this.cmbTerrain_ID.SelectedIndexChanged += new System.EventHandler(this.cmbTerrain_ID_SelectedIndexChanged);
            // 
            // btnTerrain_New
            // 
            this.btnTerrain_New.Location = new System.Drawing.Point(242, 12);
            this.btnTerrain_New.Name = "btnTerrain_New";
            this.btnTerrain_New.Size = new System.Drawing.Size(40, 23);
            this.btnTerrain_New.TabIndex = 133;
            this.btnTerrain_New.Text = "New";
            this.btnTerrain_New.UseVisualStyleBackColor = true;
            this.btnTerrain_New.Click += new System.EventHandler(this.btnTerrain_New_Click);
            // 
            // btnSaveTerrain
            // 
            this.btnSaveTerrain.Location = new System.Drawing.Point(676, 433);
            this.btnSaveTerrain.Name = "btnSaveTerrain";
            this.btnSaveTerrain.Size = new System.Drawing.Size(75, 23);
            this.btnSaveTerrain.TabIndex = 123;
            this.btnSaveTerrain.Text = "Save";
            this.btnSaveTerrain.UseVisualStyleBackColor = true;
            this.btnSaveTerrain.Click += new System.EventHandler(this.btnSaveTerrain_Click);
            // 
            // txtTerrain_Name
            // 
            this.txtTerrain_Name.Location = new System.Drawing.Point(50, 38);
            this.txtTerrain_Name.Name = "txtTerrain_Name";
            this.txtTerrain_Name.Size = new System.Drawing.Size(232, 20);
            this.txtTerrain_Name.TabIndex = 108;
            this.txtTerrain_Name.Text = "Terrain";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 41);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 107;
            this.label16.Text = "Name:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 13);
            this.label15.TabIndex = 105;
            this.label15.Text = "ID:";
            // 
            // tpLayer
            // 
            this.tpLayer.AutoScroll = true;
            this.tpLayer.Controls.Add(this.ucLayerParams1);
            this.tpLayer.Controls.Add(this.cmbLayer_ID);
            this.tpLayer.Controls.Add(this.btnLayer_New);
            this.tpLayer.Controls.Add(this.btnSaveLayer);
            this.tpLayer.Controls.Add(this.txtLayer_Name);
            this.tpLayer.Controls.Add(this.label5);
            this.tpLayer.Controls.Add(this.label3);
            this.tpLayer.Location = new System.Drawing.Point(104, 4);
            this.tpLayer.Name = "tpLayer";
            this.tpLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tpLayer.Size = new System.Drawing.Size(754, 459);
            this.tpLayer.TabIndex = 3;
            this.tpLayer.Text = "Layer";
            this.tpLayer.UseVisualStyleBackColor = true;
            // 
            // ucLayerParams1
            // 
            this.ucLayerParams1.AutoScroll = true;
            this.ucLayerParams1.Location = new System.Drawing.Point(9, 32);
            this.ucLayerParams1.Name = "ucLayerParams1";
            this.ucLayerParams1.Size = new System.Drawing.Size(739, 395);
            this.ucLayerParams1.TabIndex = 144;
            // 
            // cmbLayer_ID
            // 
            this.cmbLayer_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayer_ID.FormattingEnabled = true;
            this.cmbLayer_ID.Location = new System.Drawing.Point(79, 8);
            this.cmbLayer_ID.Name = "cmbLayer_ID";
            this.cmbLayer_ID.Size = new System.Drawing.Size(173, 21);
            this.cmbLayer_ID.TabIndex = 136;
            this.cmbLayer_ID.SelectedIndexChanged += new System.EventHandler(this.cmbLayer_ID_SelectedIndexChanged);
            // 
            // btnLayer_New
            // 
            this.btnLayer_New.Location = new System.Drawing.Point(261, 6);
            this.btnLayer_New.Name = "btnLayer_New";
            this.btnLayer_New.Size = new System.Drawing.Size(40, 23);
            this.btnLayer_New.TabIndex = 135;
            this.btnLayer_New.Text = "New";
            this.btnLayer_New.UseVisualStyleBackColor = true;
            this.btnLayer_New.Click += new System.EventHandler(this.btnLayer_New_Click);
            // 
            // btnSaveLayer
            // 
            this.btnSaveLayer.Location = new System.Drawing.Point(673, 433);
            this.btnSaveLayer.Name = "btnSaveLayer";
            this.btnSaveLayer.Size = new System.Drawing.Size(75, 23);
            this.btnSaveLayer.TabIndex = 123;
            this.btnSaveLayer.Text = "Save";
            this.btnSaveLayer.UseVisualStyleBackColor = true;
            this.btnSaveLayer.Click += new System.EventHandler(this.btnSaveLayer_Click);
            // 
            // txtLayer_Name
            // 
            this.txtLayer_Name.Location = new System.Drawing.Point(473, 6);
            this.txtLayer_Name.Name = "txtLayer_Name";
            this.txtLayer_Name.Size = new System.Drawing.Size(232, 20);
            this.txtLayer_Name.TabIndex = 3;
            this.txtLayer_Name.Text = "Layer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(433, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "ID:";
            // 
            // tpOverlayManager
            // 
            this.tpOverlayManager.AutoScroll = true;
            this.tpOverlayManager.Controls.Add(this.cmbOverlayManager_ID);
            this.tpOverlayManager.Controls.Add(this.btnOverlayManager_New);
            this.tpOverlayManager.Controls.Add(this.txtOverlayManager_Name);
            this.tpOverlayManager.Controls.Add(this.label1);
            this.tpOverlayManager.Controls.Add(this.btnSaveOverlayManager);
            this.tpOverlayManager.Controls.Add(this.label22);
            this.tpOverlayManager.Location = new System.Drawing.Point(104, 4);
            this.tpOverlayManager.Name = "tpOverlayManager";
            this.tpOverlayManager.Padding = new System.Windows.Forms.Padding(3);
            this.tpOverlayManager.Size = new System.Drawing.Size(754, 459);
            this.tpOverlayManager.TabIndex = 5;
            this.tpOverlayManager.Text = "Overlay Manager";
            this.tpOverlayManager.UseVisualStyleBackColor = true;
            // 
            // cmbOverlayManager_ID
            // 
            this.cmbOverlayManager_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOverlayManager_ID.FormattingEnabled = true;
            this.cmbOverlayManager_ID.Location = new System.Drawing.Point(49, 15);
            this.cmbOverlayManager_ID.Name = "cmbOverlayManager_ID";
            this.cmbOverlayManager_ID.Size = new System.Drawing.Size(186, 21);
            this.cmbOverlayManager_ID.TabIndex = 138;
            this.cmbOverlayManager_ID.SelectedIndexChanged += new System.EventHandler(this.cmbOverlayManager_ID_SelectedIndexChanged);
            // 
            // btnOverlayManager_New
            // 
            this.btnOverlayManager_New.Location = new System.Drawing.Point(241, 13);
            this.btnOverlayManager_New.Name = "btnOverlayManager_New";
            this.btnOverlayManager_New.Size = new System.Drawing.Size(40, 23);
            this.btnOverlayManager_New.TabIndex = 137;
            this.btnOverlayManager_New.Text = "New";
            this.btnOverlayManager_New.UseVisualStyleBackColor = true;
            this.btnOverlayManager_New.Click += new System.EventHandler(this.btnOverlayManager_New_Click);
            // 
            // txtOverlayManager_Name
            // 
            this.txtOverlayManager_Name.Location = new System.Drawing.Point(49, 40);
            this.txtOverlayManager_Name.Name = "txtOverlayManager_Name";
            this.txtOverlayManager_Name.Size = new System.Drawing.Size(232, 20);
            this.txtOverlayManager_Name.TabIndex = 125;
            this.txtOverlayManager_Name.Text = "Overlay Manager";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 124;
            this.label1.Text = "Name:";
            // 
            // btnSaveOverlayManager
            // 
            this.btnSaveOverlayManager.Location = new System.Drawing.Point(676, 433);
            this.btnSaveOverlayManager.Name = "btnSaveOverlayManager";
            this.btnSaveOverlayManager.Size = new System.Drawing.Size(75, 23);
            this.btnSaveOverlayManager.TabIndex = 123;
            this.btnSaveOverlayManager.Text = "Save";
            this.btnSaveOverlayManager.UseVisualStyleBackColor = true;
            this.btnSaveOverlayManager.Click += new System.EventHandler(this.btnSaveOverlayManager_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 17);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(21, 13);
            this.label22.TabIndex = 0;
            this.label22.Text = "ID:";
            // 
            // tpCoordinateSystem
            // 
            this.tpCoordinateSystem.AutoScroll = true;
            this.tpCoordinateSystem.Controls.Add(this.ctrlNewGridCoordinateSystem1);
            this.tpCoordinateSystem.Controls.Add(this.cmbGridCoordinateSystem_ID);
            this.tpCoordinateSystem.Controls.Add(this.btnGridCoordinateSystem_New);
            this.tpCoordinateSystem.Controls.Add(this.btnSaveGridCoordinateSystem);
            this.tpCoordinateSystem.Controls.Add(this.label27);
            this.tpCoordinateSystem.Location = new System.Drawing.Point(104, 4);
            this.tpCoordinateSystem.Name = "tpCoordinateSystem";
            this.tpCoordinateSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tpCoordinateSystem.Size = new System.Drawing.Size(754, 459);
            this.tpCoordinateSystem.TabIndex = 6;
            this.tpCoordinateSystem.Text = "Coordinate System";
            this.tpCoordinateSystem.UseVisualStyleBackColor = true;
            // 
            // ctrlNewGridCoordinateSystem1
            // 
            this.ctrlNewGridCoordinateSystem1.Location = new System.Drawing.Point(9, 42);
            this.ctrlNewGridCoordinateSystem1.Name = "ctrlNewGridCoordinateSystem1";
            this.ctrlNewGridCoordinateSystem1.Size = new System.Drawing.Size(495, 327);
            this.ctrlNewGridCoordinateSystem1.TabIndex = 141;
            // 
            // cmbGridCoordinateSystem_ID
            // 
            this.cmbGridCoordinateSystem_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGridCoordinateSystem_ID.FormattingEnabled = true;
            this.cmbGridCoordinateSystem_ID.Location = new System.Drawing.Point(120, 10);
            this.cmbGridCoordinateSystem_ID.Name = "cmbGridCoordinateSystem_ID";
            this.cmbGridCoordinateSystem_ID.Size = new System.Drawing.Size(230, 21);
            this.cmbGridCoordinateSystem_ID.TabIndex = 140;
            this.cmbGridCoordinateSystem_ID.SelectedIndexChanged += new System.EventHandler(this.cmbGridCoordinateSystem_ID_SelectedIndexChanged);
            // 
            // btnGridCoordinateSystem_New
            // 
            this.btnGridCoordinateSystem_New.Location = new System.Drawing.Point(356, 8);
            this.btnGridCoordinateSystem_New.Name = "btnGridCoordinateSystem_New";
            this.btnGridCoordinateSystem_New.Size = new System.Drawing.Size(40, 23);
            this.btnGridCoordinateSystem_New.TabIndex = 139;
            this.btnGridCoordinateSystem_New.Text = "New";
            this.btnGridCoordinateSystem_New.UseVisualStyleBackColor = true;
            this.btnGridCoordinateSystem_New.Click += new System.EventHandler(this.btnGridCoordinateSystem_New_Click);
            // 
            // btnSaveGridCoordinateSystem
            // 
            this.btnSaveGridCoordinateSystem.Location = new System.Drawing.Point(673, 432);
            this.btnSaveGridCoordinateSystem.Name = "btnSaveGridCoordinateSystem";
            this.btnSaveGridCoordinateSystem.Size = new System.Drawing.Size(75, 23);
            this.btnSaveGridCoordinateSystem.TabIndex = 123;
            this.btnSaveGridCoordinateSystem.Text = "Save";
            this.btnSaveGridCoordinateSystem.UseVisualStyleBackColor = true;
            this.btnSaveGridCoordinateSystem.Click += new System.EventHandler(this.btnSaveGridCoordinateSystem_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 13);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(21, 13);
            this.label27.TabIndex = 3;
            this.label27.Text = "ID:";
            // 
            // tpImageCalc
            // 
            this.tpImageCalc.AutoScroll = true;
            this.tpImageCalc.Controls.Add(this.label19);
            this.tpImageCalc.Controls.Add(this.label8);
            this.tpImageCalc.Controls.Add(this.ctrlImageCalc_ctrlCameraParams1);
            this.tpImageCalc.Controls.Add(this.cmbImageCalc_ID);
            this.tpImageCalc.Controls.Add(this.btnImageCalc_New);
            this.tpImageCalc.Controls.Add(this.txtImageCalc_Name);
            this.tpImageCalc.Controls.Add(this.label32);
            this.tpImageCalc.Controls.Add(this.btnSaveImageCalc);
            this.tpImageCalc.Controls.Add(this.chxImageCalc_IsFileName);
            this.tpImageCalc.Controls.Add(this.cmbImageCalc_Type);
            this.tpImageCalc.Controls.Add(this.label18);
            this.tpImageCalc.Controls.Add(this.label17);
            this.tpImageCalc.Controls.Add(this.ctrlImageCalc_XmlPath);
            this.tpImageCalc.Controls.Add(this.ctrlImageCalc_DtmMapLayerPath);
            this.tpImageCalc.Location = new System.Drawing.Point(104, 4);
            this.tpImageCalc.Name = "tpImageCalc";
            this.tpImageCalc.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageCalc.Size = new System.Drawing.Size(754, 459);
            this.tpImageCalc.TabIndex = 7;
            this.tpImageCalc.Text = "Image Calc";
            this.tpImageCalc.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 96);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(141, 13);
            this.label19.TabIndex = 145;
            this.label19.Text = "Native Dtm Map Layer Path:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 144;
            this.label8.Text = "XML Path:";
            // 
            // ctrlImageCalc_ctrlCameraParams1
            // 
            this.ctrlImageCalc_ctrlCameraParams1.Location = new System.Drawing.Point(7, 144);
            this.ctrlImageCalc_ctrlCameraParams1.Name = "ctrlImageCalc_ctrlCameraParams1";
            this.ctrlImageCalc_ctrlCameraParams1.Size = new System.Drawing.Size(365, 241);
            this.ctrlImageCalc_ctrlCameraParams1.TabIndex = 143;
            // 
            // cmbImageCalc_ID
            // 
            this.cmbImageCalc_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageCalc_ID.FormattingEnabled = true;
            this.cmbImageCalc_ID.Location = new System.Drawing.Point(77, 11);
            this.cmbImageCalc_ID.Name = "cmbImageCalc_ID";
            this.cmbImageCalc_ID.Size = new System.Drawing.Size(186, 21);
            this.cmbImageCalc_ID.TabIndex = 142;
            this.cmbImageCalc_ID.SelectedIndexChanged += new System.EventHandler(this.cmbImageCalc_ID_SelectedIndexChanged);
            // 
            // btnImageCalc_New
            // 
            this.btnImageCalc_New.Location = new System.Drawing.Point(268, 10);
            this.btnImageCalc_New.Name = "btnImageCalc_New";
            this.btnImageCalc_New.Size = new System.Drawing.Size(40, 23);
            this.btnImageCalc_New.TabIndex = 141;
            this.btnImageCalc_New.Text = "New";
            this.btnImageCalc_New.UseVisualStyleBackColor = true;
            this.btnImageCalc_New.Click += new System.EventHandler(this.btnImageCalc_New_Click);
            // 
            // txtImageCalc_Name
            // 
            this.txtImageCalc_Name.Location = new System.Drawing.Point(77, 38);
            this.txtImageCalc_Name.Name = "txtImageCalc_Name";
            this.txtImageCalc_Name.Size = new System.Drawing.Size(232, 20);
            this.txtImageCalc_Name.TabIndex = 128;
            this.txtImageCalc_Name.Text = "Image Calc";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(6, 41);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(38, 13);
            this.label32.TabIndex = 127;
            this.label32.Text = "Name:";
            // 
            // btnSaveImageCalc
            // 
            this.btnSaveImageCalc.Location = new System.Drawing.Point(673, 430);
            this.btnSaveImageCalc.Name = "btnSaveImageCalc";
            this.btnSaveImageCalc.Size = new System.Drawing.Size(75, 23);
            this.btnSaveImageCalc.TabIndex = 125;
            this.btnSaveImageCalc.Text = "Save";
            this.btnSaveImageCalc.UseVisualStyleBackColor = true;
            this.btnSaveImageCalc.Click += new System.EventHandler(this.btnSaveImageCalc_Click);
            // 
            // chxImageCalc_IsFileName
            // 
            this.chxImageCalc_IsFileName.AutoSize = true;
            this.chxImageCalc_IsFileName.Checked = true;
            this.chxImageCalc_IsFileName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxImageCalc_IsFileName.Location = new System.Drawing.Point(561, 126);
            this.chxImageCalc_IsFileName.Name = "chxImageCalc_IsFileName";
            this.chxImageCalc_IsFileName.Size = new System.Drawing.Size(84, 17);
            this.chxImageCalc_IsFileName.TabIndex = 117;
            this.chxImageCalc_IsFileName.Text = "Is File Name";
            this.chxImageCalc_IsFileName.UseVisualStyleBackColor = true;
            this.chxImageCalc_IsFileName.CheckedChanged += new System.EventHandler(this.chxImageCalc_IsFileName_CheckedChanged);
            // 
            // cmbImageCalc_Type
            // 
            this.cmbImageCalc_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageCalc_Type.FormattingEnabled = true;
            this.cmbImageCalc_Type.Location = new System.Drawing.Point(78, 64);
            this.cmbImageCalc_Type.Name = "cmbImageCalc_Type";
            this.cmbImageCalc_Type.Size = new System.Drawing.Size(232, 21);
            this.cmbImageCalc_Type.TabIndex = 3;
            this.cmbImageCalc_Type.SelectedIndexChanged += new System.EventHandler(this.cmbImageCalc_Type_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 67);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 13);
            this.label18.TabIndex = 2;
            this.label18.Text = "Image Type:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(21, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "ID:";
            // 
            // ctrlImageCalc_XmlPath
            // 
            this.ctrlImageCalc_XmlPath.AutoSize = true;
            this.ctrlImageCalc_XmlPath.FileName = "";
            this.ctrlImageCalc_XmlPath.Filter = "";
            this.ctrlImageCalc_XmlPath.IsFolderDialog = false;
            this.ctrlImageCalc_XmlPath.IsFullPath = true;
            this.ctrlImageCalc_XmlPath.IsSaveFile = false;
            this.ctrlImageCalc_XmlPath.LabelCaption = "XML Path:";
            this.ctrlImageCalc_XmlPath.Location = new System.Drawing.Point(77, 121);
            this.ctrlImageCalc_XmlPath.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlImageCalc_XmlPath.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlImageCalc_XmlPath.MultiFilesSelect = false;
            this.ctrlImageCalc_XmlPath.Name = "ctrlImageCalc_XmlPath";
            this.ctrlImageCalc_XmlPath.Prefix = "";
            this.ctrlImageCalc_XmlPath.Size = new System.Drawing.Size(478, 24);
            this.ctrlImageCalc_XmlPath.TabIndex = 118;
            // 
            // ctrlImageCalc_DtmMapLayerPath
            // 
            this.ctrlImageCalc_DtmMapLayerPath.AutoSize = true;
            this.ctrlImageCalc_DtmMapLayerPath.FileName = "";
            this.ctrlImageCalc_DtmMapLayerPath.Filter = "";
            this.ctrlImageCalc_DtmMapLayerPath.IsFolderDialog = true;
            this.ctrlImageCalc_DtmMapLayerPath.IsFullPath = true;
            this.ctrlImageCalc_DtmMapLayerPath.IsSaveFile = false;
            this.ctrlImageCalc_DtmMapLayerPath.LabelCaption = "Native Dtm Map Layer Path:";
            this.ctrlImageCalc_DtmMapLayerPath.Location = new System.Drawing.Point(145, 91);
            this.ctrlImageCalc_DtmMapLayerPath.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlImageCalc_DtmMapLayerPath.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlImageCalc_DtmMapLayerPath.MultiFilesSelect = false;
            this.ctrlImageCalc_DtmMapLayerPath.Name = "ctrlImageCalc_DtmMapLayerPath";
            this.ctrlImageCalc_DtmMapLayerPath.Prefix = "";
            this.ctrlImageCalc_DtmMapLayerPath.Size = new System.Drawing.Size(500, 24);
            this.ctrlImageCalc_DtmMapLayerPath.TabIndex = 116;
            // 
            // lstTargetGridCoordinateSystem
            // 
            this.lstTargetGridCoordinateSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstTargetGridCoordinateSystem.FormattingEnabled = true;
            this.lstTargetGridCoordinateSystem.Location = new System.Drawing.Point(0, 460);
            this.lstTargetGridCoordinateSystem.Name = "lstTargetGridCoordinateSystem";
            this.lstTargetGridCoordinateSystem.Size = new System.Drawing.Size(279, 69);
            this.lstTargetGridCoordinateSystem.TabIndex = 125;
            this.lstTargetGridCoordinateSystem.Visible = false;
            // 
            // lblTargetGridCoordinateSystem
            // 
            this.lblTargetGridCoordinateSystem.AutoSize = true;
            this.lblTargetGridCoordinateSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTargetGridCoordinateSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblTargetGridCoordinateSystem.Location = new System.Drawing.Point(0, 447);
            this.lblTargetGridCoordinateSystem.Name = "lblTargetGridCoordinateSystem";
            this.lblTargetGridCoordinateSystem.Size = new System.Drawing.Size(184, 13);
            this.lblTargetGridCoordinateSystem.TabIndex = 126;
            this.lblTargetGridCoordinateSystem.Text = "Target Grid Coordinate System:";
            this.lblTargetGridCoordinateSystem.Visible = false;
            // 
            // lstImageCalc
            // 
            this.lstImageCalc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstImageCalc.FormattingEnabled = true;
            this.lstImageCalc.Location = new System.Drawing.Point(0, 391);
            this.lstImageCalc.Name = "lstImageCalc";
            this.lstImageCalc.Size = new System.Drawing.Size(279, 56);
            this.lstImageCalc.TabIndex = 122;
            this.lstImageCalc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstImageCalc_MouseDoubleClick);
            this.lstImageCalc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstImageCalc_MouseDown);
            // 
            // lblImageCalc
            // 
            this.lblImageCalc.AutoSize = true;
            this.lblImageCalc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblImageCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblImageCalc.Location = new System.Drawing.Point(0, 378);
            this.lblImageCalc.Name = "lblImageCalc";
            this.lblImageCalc.Size = new System.Drawing.Size(74, 13);
            this.lblImageCalc.TabIndex = 124;
            this.lblImageCalc.Text = "Image Calc:";
            // 
            // lstOverlayManager
            // 
            this.lstOverlayManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstOverlayManager.FormattingEnabled = true;
            this.lstOverlayManager.Location = new System.Drawing.Point(0, 322);
            this.lstOverlayManager.Name = "lstOverlayManager";
            this.lstOverlayManager.Size = new System.Drawing.Size(279, 56);
            this.lstOverlayManager.TabIndex = 120;
            this.lstOverlayManager.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstOverlayManager_MouseDoubleClick);
            this.lstOverlayManager.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstOverlayManager_MouseDown);
            // 
            // lblOverlayManager
            // 
            this.lblOverlayManager.AutoSize = true;
            this.lblOverlayManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOverlayManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblOverlayManager.Location = new System.Drawing.Point(0, 309);
            this.lblOverlayManager.Name = "lblOverlayManager";
            this.lblOverlayManager.Size = new System.Drawing.Size(107, 13);
            this.lblOverlayManager.TabIndex = 123;
            this.lblOverlayManager.Text = "Overlay Manager:";
            // 
            // lstGridCoordinateSystem
            // 
            this.lstGridCoordinateSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstGridCoordinateSystem.FormattingEnabled = true;
            this.lstGridCoordinateSystem.Location = new System.Drawing.Point(0, 240);
            this.lstGridCoordinateSystem.Name = "lstGridCoordinateSystem";
            this.lstGridCoordinateSystem.Size = new System.Drawing.Size(279, 69);
            this.lstGridCoordinateSystem.TabIndex = 107;
            this.lstGridCoordinateSystem.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstGridCoordinateSystem_MouseDoubleClick);
            // 
            // lblGridCoordSys
            // 
            this.lblGridCoordSys.AutoSize = true;
            this.lblGridCoordSys.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridCoordSys.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblGridCoordSys.Location = new System.Drawing.Point(0, 227);
            this.lblGridCoordSys.Name = "lblGridCoordSys";
            this.lblGridCoordSys.Size = new System.Drawing.Size(143, 13);
            this.lblGridCoordSys.TabIndex = 122;
            this.lblGridCoordSys.Text = "Grid Coordinate System:";
            // 
            // clstChilds
            // 
            this.clstChilds.CheckOnClick = true;
            this.clstChilds.Dock = System.Windows.Forms.DockStyle.Top;
            this.clstChilds.FormattingEnabled = true;
            this.clstChilds.Location = new System.Drawing.Point(0, 13);
            this.clstChilds.Name = "clstChilds";
            this.clstChilds.Size = new System.Drawing.Size(279, 214);
            this.clstChilds.TabIndex = 0;
            this.clstChilds.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clstChilds_ItemCheck);
            this.clstChilds.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.clstChilds_MouseDoubleClick);
            // 
            // lblChild
            // 
            this.lblChild.AutoSize = true;
            this.lblChild.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblChild.Location = new System.Drawing.Point(0, 0);
            this.lblChild.Name = "lblChild";
            this.lblChild.Size = new System.Drawing.Size(81, 13);
            this.lblChild.TabIndex = 121;
            this.lblChild.Text = "Entity Childs:";
            // 
            // MapLoaderDefinitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1159, 499);
            this.Controls.Add(this.tcMapLoaderDefinition);
            this.Name = "MapLoaderDefinitionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapLoaderDefinitionForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapLoaderDefinitionForm_FormClosing);
            this.tcMapLoaderDefinition.ResumeLayout(false);
            this.tpSchemasList.ResumeLayout(false);
            this.tpSchemasList.PerformLayout();
            this.tpSchemasDefine.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tcSchemesDefine.ResumeLayout(false);
            this.tpDevice.ResumeLayout(false);
            this.tpScheme.ResumeLayout(false);
            this.tpScheme.PerformLayout();
            this.tpViewport.ResumeLayout(false);
            this.tpViewport.PerformLayout();
            this.tpTerrain.ResumeLayout(false);
            this.tpTerrain.PerformLayout();
            this.tpLayer.ResumeLayout(false);
            this.tpLayer.PerformLayout();
            this.tpOverlayManager.ResumeLayout(false);
            this.tpOverlayManager.PerformLayout();
            this.tpCoordinateSystem.ResumeLayout(false);
            this.tpCoordinateSystem.PerformLayout();
            this.tpImageCalc.ResumeLayout(false);
            this.tpImageCalc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMapLoaderDefinition;
        private System.Windows.Forms.TabPage tpSchemasList;
        private System.Windows.Forms.TabPage tpSchemasDefine;
        private System.Windows.Forms.Button btnLoadSchema;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lstMapSchemas;
        private System.Windows.Forms.ColumnHeader colArea;
        private System.Windows.Forms.ColumnHeader colSchemaNum;
        private System.Windows.Forms.ColumnHeader colMapType;
        private System.Windows.Forms.ColumnHeader colOMCoordSys;
        private System.Windows.Forms.ColumnHeader colComments;
        private MCTester.Controls.NumericTextBox ntxNumBackgroundThreads;
        private System.Windows.Forms.CheckBox chxIsWpfWindow;
        private System.Windows.Forms.TabControl tcSchemesDefine;
        private System.Windows.Forms.TabPage tpViewport;
        private System.Windows.Forms.TabPage tpTerrain;
        private System.Windows.Forms.TabPage tpLayer;
        private System.Windows.Forms.TabPage tpOverlayManager;
        private System.Windows.Forms.TabPage tpCoordinateSystem;
        private System.Windows.Forms.TabPage tpImageCalc;
        private System.Windows.Forms.CheckBox chxViewport_ShowGeoInMetricProportion;
        private System.Windows.Forms.Label lblMapType;
        private System.Windows.Forms.ComboBox cmbViewport_MapType;
        private System.Windows.Forms.Label label4;
        private MCTester.Controls.Ctrl3DVector ctrlViewport_CameraPosition;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tpScheme;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtViewport_Name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtScheme_Comment;
        private System.Windows.Forms.TextBox txtScheme_MapType;
        private System.Windows.Forms.TextBox txtScheme_Area;
        private System.Windows.Forms.TextBox txtTerrain_Name;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckedListBox clstChilds;
        private System.Windows.Forms.TextBox txtLayer_Name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbImageCalc_Type;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chxImageCalc_IsFileName;
        private MCTester.Controls.CtrlBrowseControl ctrlImageCalc_DtmMapLayerPath;
        private MCTester.Controls.CtrlBrowseControl ctrlImageCalc_XmlPath;
        private System.Windows.Forms.ListBox lstGridCoordinateSystem;
        private System.Windows.Forms.Button btnSaveSchema;
        private System.Windows.Forms.Button btnSaveViewport;
        private System.Windows.Forms.Button btnSaveTerrain;
        private System.Windows.Forms.Button btnSaveLayer;
        private System.Windows.Forms.Button btnSaveOverlayManager;
        private System.Windows.Forms.Button btnSaveGridCoordinateSystem;
        private System.Windows.Forms.Button btnSaveImageCalc;
        private System.Windows.Forms.ListBox lstImageCalc;
        private System.Windows.Forms.TabPage tpDevice;
        private System.Windows.Forms.Button btnSaveDevice;
        private System.Windows.Forms.ListBox lstOverlayManager;
        private System.Windows.Forms.TextBox txtOverlayManager_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private MCTester.Controls.NumericTextBox ntxViewport_TerrainObjectBestResolution;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbViewport_DtmUsageAndPrecision;
        private System.Windows.Forms.TextBox txtImageCalc_Name;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Button btnScheme_New;
        private System.Windows.Forms.Button btnViewport_New;
        private System.Windows.Forms.Button btnTerrain_New;
        private System.Windows.Forms.Button btnLayer_New;
        private System.Windows.Forms.Button btnOverlayManager_New;
        private System.Windows.Forms.Button btnGridCoordinateSystem_New;
        private System.Windows.Forms.Button btnImageCalc_New;
        private System.Windows.Forms.Label lblImageCalc;
        private System.Windows.Forms.Label lblOverlayManager;
        private System.Windows.Forms.Label lblGridCoordSys;
        private System.Windows.Forms.Label lblChild;
        private System.Windows.Forms.CheckBox chxMultiScreenDevice;
        private System.Windows.Forms.CheckBox chxMultiThreadedDevice;
        private Controls.NumericTextBox ntxTerrainResolutionFactor;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbScheme_ID;
        private System.Windows.Forms.ComboBox cmbViewport_ID;
        private System.Windows.Forms.ComboBox cmbTerrain_ID;
        private System.Windows.Forms.ComboBox cmbLayer_ID;
        private System.Windows.Forms.ComboBox cmbOverlayManager_ID;
        private System.Windows.Forms.ComboBox cmbGridCoordinateSystem_ID;
        private System.Windows.Forms.ComboBox cmbImageCalc_ID;
        private Controls.ucLayerParams ucLayerParams1;
        private General_Forms.CtrlNewGridCoordinateSystem ctrlNewGridCoordinateSystem1;
        private Controls.CtrlDeviceParams ctrlDeviceParams1;
        private System.Windows.Forms.ListBox lstTargetGridCoordinateSystem;
        private System.Windows.Forms.Label lblTargetGridCoordinateSystem;
        private Controls.CtrlCameraParams ctrlImageCalc_ctrlCameraParams1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label8;
    }
}