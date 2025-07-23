namespace MCTester.Controls
{
    partial class CreateDataSQControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSelectOverlayManager = new System.Windows.Forms.Button();
            this.lblOverlayManagerSelectionStatus = new System.Windows.Forms.Label();
            this.gbGeneral = new System.Windows.Forms.GroupBox();
            this.btnRemoveOM = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpCreateDataSQ = new System.Windows.Forms.TabPage();
            this.ntbViewportID = new MCTester.Controls.NumericTextBox();
            this.ctrlCreateDataSQGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.tpCreateDevice = new System.Windows.Forms.TabPage();
            this.btnCreateDevice = new System.Windows.Forms.Button();
            this.ctrlDeviceParams1 = new MCTester.Controls.CtrlDeviceParams();
            this.tpDeviceOperations = new System.Windows.Forms.TabPage();
            this.gbUnloadResource = new System.Windows.Forms.GroupBox();
            this.btnUnloadResource = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.tbUnloadGroupName = new System.Windows.Forms.TextBox();
            this.gbLoadResource = new System.Windows.Forms.GroupBox();
            this.localPathArray1 = new MCTester.Controls.LocalPathArray();
            this.btnLoadResource = new System.Windows.Forms.Button();
            this.cmbResourceLocationType = new System.Windows.Forms.ComboBox();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tpLocalCache = new System.Windows.Forms.TabPage();
            this.ntbLocalCacheCurrentSizeParams = new MCTester.Controls.NumericTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.tbLocalCacheFolder = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtLocalCacheSubFolder = new System.Windows.Forms.TextBox();
            this.dgvLocalCacheLayersParams = new System.Windows.Forms.DataGridView();
            this.colSubFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOriginalFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGetLocalCacheParams = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.btnSetMapLayersLocalCacheSize = new System.Windows.Forms.Button();
            this.btnRemoveMapLayerFromLocalCache = new System.Windows.Forms.Button();
            this.btnRemoveMapLayersLocalCache = new System.Windows.Forms.Button();
            this.ntbLocalCacheMaxSizeParams = new MCTester.Controls.NumericTextBox();
            this.gbGeneral.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpCreateDataSQ.SuspendLayout();
            this.tpCreateDevice.SuspendLayout();
            this.tpDeviceOperations.SuspendLayout();
            this.gbUnloadResource.SuspendLayout();
            this.gbLoadResource.SuspendLayout();
            this.tpLocalCache.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalCacheLayersParams)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Viewport ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Overlay Manager selection";
            // 
            // btnSelectOverlayManager
            // 
            this.btnSelectOverlayManager.Location = new System.Drawing.Point(149, 35);
            this.btnSelectOverlayManager.Name = "btnSelectOverlayManager";
            this.btnSelectOverlayManager.Size = new System.Drawing.Size(51, 23);
            this.btnSelectOverlayManager.TabIndex = 71;
            this.btnSelectOverlayManager.Text = "Select";
            this.btnSelectOverlayManager.UseVisualStyleBackColor = true;
            this.btnSelectOverlayManager.Click += new System.EventHandler(this.btnSelectOverlayManager_Click);
            // 
            // lblOverlayManagerSelectionStatus
            // 
            this.lblOverlayManagerSelectionStatus.AutoSize = true;
            this.lblOverlayManagerSelectionStatus.Location = new System.Drawing.Point(263, 40);
            this.lblOverlayManagerSelectionStatus.Name = "lblOverlayManagerSelectionStatus";
            this.lblOverlayManagerSelectionStatus.Size = new System.Drawing.Size(69, 13);
            this.lblOverlayManagerSelectionStatus.TabIndex = 72;
            this.lblOverlayManagerSelectionStatus.Text = "Not Selected";
            // 
            // gbGeneral
            // 
            this.gbGeneral.Controls.Add(this.btnRemoveOM);
            this.gbGeneral.Controls.Add(this.label1);
            this.gbGeneral.Controls.Add(this.lblOverlayManagerSelectionStatus);
            this.gbGeneral.Controls.Add(this.btnSelectOverlayManager);
            this.gbGeneral.Controls.Add(this.label4);
            this.gbGeneral.Location = new System.Drawing.Point(3, 164);
            this.gbGeneral.Name = "gbGeneral";
            this.gbGeneral.Size = new System.Drawing.Size(336, 64);
            this.gbGeneral.TabIndex = 75;
            this.gbGeneral.TabStop = false;
            // 
            // btnRemoveOM
            // 
            this.btnRemoveOM.Enabled = false;
            this.btnRemoveOM.Location = new System.Drawing.Point(206, 35);
            this.btnRemoveOM.Name = "btnRemoveOM";
            this.btnRemoveOM.Size = new System.Drawing.Size(57, 23);
            this.btnRemoveOM.TabIndex = 73;
            this.btnRemoveOM.Text = "Remove";
            this.btnRemoveOM.UseVisualStyleBackColor = true;
            this.btnRemoveOM.Click += new System.EventHandler(this.btnRemoveOM_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpCreateDevice);
            this.tabControl1.Controls.Add(this.tpCreateDataSQ);
            this.tabControl1.Controls.Add(this.tpDeviceOperations);
            this.tabControl1.Controls.Add(this.tpLocalCache);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(350, 261);
            this.tabControl1.TabIndex = 78;
            // 
            // tpCreateDataSQ
            // 
            this.tpCreateDataSQ.Controls.Add(this.ntbViewportID);
            this.tpCreateDataSQ.Controls.Add(this.ctrlCreateDataSQGridCoordinateSystem);
            this.tpCreateDataSQ.Controls.Add(this.gbGeneral);
            this.tpCreateDataSQ.Location = new System.Drawing.Point(4, 22);
            this.tpCreateDataSQ.Name = "tpCreateDataSQ";
            this.tpCreateDataSQ.Padding = new System.Windows.Forms.Padding(3);
            this.tpCreateDataSQ.Size = new System.Drawing.Size(342, 235);
            this.tpCreateDataSQ.TabIndex = 0;
            this.tpCreateDataSQ.Text = "Create Data SQ";
            this.tpCreateDataSQ.UseVisualStyleBackColor = true;
            // 
            // ntbViewportID
            // 
            this.ntbViewportID.Location = new System.Drawing.Point(77, 178);
            this.ntbViewportID.Name = "ntbViewportID";
            this.ntbViewportID.Size = new System.Drawing.Size(77, 20);
            this.ntbViewportID.TabIndex = 77;
            this.ntbViewportID.Text = "MAX";
            // 
            // ctrlCreateDataSQGridCoordinateSystem
            // 
            this.ctrlCreateDataSQGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlCreateDataSQGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlCreateDataSQGridCoordinateSystem.GroupBoxText = "Grid Coordinate System";
            this.ctrlCreateDataSQGridCoordinateSystem.IsEditable = false;
            this.ctrlCreateDataSQGridCoordinateSystem.Location = new System.Drawing.Point(3, 6);
            this.ctrlCreateDataSQGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlCreateDataSQGridCoordinateSystem.Name = "ctrlCreateDataSQGridCoordinateSystem";
            this.ctrlCreateDataSQGridCoordinateSystem.Size = new System.Drawing.Size(336, 155);
            this.ctrlCreateDataSQGridCoordinateSystem.TabIndex = 76;
            // 
            // tpCreateDevice
            // 
            this.tpCreateDevice.AutoScroll = true;
            this.tpCreateDevice.Controls.Add(this.btnCreateDevice);
            this.tpCreateDevice.Controls.Add(this.ctrlDeviceParams1);
            this.tpCreateDevice.Location = new System.Drawing.Point(4, 22);
            this.tpCreateDevice.Name = "tpCreateDevice";
            this.tpCreateDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tpCreateDevice.Size = new System.Drawing.Size(342, 235);
            this.tpCreateDevice.TabIndex = 1;
            this.tpCreateDevice.Text = "Device";
            this.tpCreateDevice.UseVisualStyleBackColor = true;
            // 
            // btnCreateDevice
            // 
            this.btnCreateDevice.Location = new System.Drawing.Point(213, 6);
            this.btnCreateDevice.Name = "btnCreateDevice";
            this.btnCreateDevice.Size = new System.Drawing.Size(107, 23);
            this.btnCreateDevice.TabIndex = 120;
            this.btnCreateDevice.Text = "Create Device";
            this.btnCreateDevice.UseVisualStyleBackColor = true;
            this.btnCreateDevice.Click += new System.EventHandler(this.btnCreateDevice_Click);
            // 
            // ctrlDeviceParams1
            // 
            this.ctrlDeviceParams1.Location = new System.Drawing.Point(3, 30);
            this.ctrlDeviceParams1.Name = "ctrlDeviceParams1";
            this.ctrlDeviceParams1.Size = new System.Drawing.Size(323, 700);
            this.ctrlDeviceParams1.TabIndex = 119;
            // 
            // tpDeviceOperations
            // 
            this.tpDeviceOperations.Controls.Add(this.gbUnloadResource);
            this.tpDeviceOperations.Controls.Add(this.gbLoadResource);
            this.tpDeviceOperations.Location = new System.Drawing.Point(4, 22);
            this.tpDeviceOperations.Name = "tpDeviceOperations";
            this.tpDeviceOperations.Size = new System.Drawing.Size(342, 235);
            this.tpDeviceOperations.TabIndex = 2;
            this.tpDeviceOperations.Text = "Device Operations";
            this.tpDeviceOperations.UseVisualStyleBackColor = true;
            // 
            // gbUnloadResource
            // 
            this.gbUnloadResource.Controls.Add(this.btnUnloadResource);
            this.gbUnloadResource.Controls.Add(this.label16);
            this.gbUnloadResource.Controls.Add(this.tbUnloadGroupName);
            this.gbUnloadResource.Location = new System.Drawing.Point(3, 257);
            this.gbUnloadResource.Name = "gbUnloadResource";
            this.gbUnloadResource.Size = new System.Drawing.Size(353, 50);
            this.gbUnloadResource.TabIndex = 6;
            this.gbUnloadResource.TabStop = false;
            this.gbUnloadResource.Text = "Unload Resource Group ";
            // 
            // btnUnloadResource
            // 
            this.btnUnloadResource.Location = new System.Drawing.Point(278, 17);
            this.btnUnloadResource.Name = "btnUnloadResource";
            this.btnUnloadResource.Size = new System.Drawing.Size(56, 23);
            this.btnUnloadResource.TabIndex = 4;
            this.btnUnloadResource.Text = "Unload";
            this.btnUnloadResource.UseVisualStyleBackColor = true;
            this.btnUnloadResource.Click += new System.EventHandler(this.btnUnloadResource_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Group Name:";
            // 
            // tbUnloadGroupName
            // 
            this.tbUnloadGroupName.Location = new System.Drawing.Point(83, 19);
            this.tbUnloadGroupName.Name = "tbUnloadGroupName";
            this.tbUnloadGroupName.Size = new System.Drawing.Size(156, 20);
            this.tbUnloadGroupName.TabIndex = 0;
            // 
            // gbLoadResource
            // 
            this.gbLoadResource.Controls.Add(this.localPathArray1);
            this.gbLoadResource.Controls.Add(this.btnLoadResource);
            this.gbLoadResource.Controls.Add(this.cmbResourceLocationType);
            this.gbLoadResource.Controls.Add(this.txtGroupName);
            this.gbLoadResource.Controls.Add(this.label15);
            this.gbLoadResource.Controls.Add(this.label14);
            this.gbLoadResource.Location = new System.Drawing.Point(3, 3);
            this.gbLoadResource.Name = "gbLoadResource";
            this.gbLoadResource.Size = new System.Drawing.Size(353, 248);
            this.gbLoadResource.TabIndex = 5;
            this.gbLoadResource.TabStop = false;
            this.gbLoadResource.Text = "Load Resource Group";
            // 
            // localPathArray1
            // 
            this.localPathArray1.Filter = null;
            this.localPathArray1.IsFolder = true;
            this.localPathArray1.Location = new System.Drawing.Point(0, 72);
            this.localPathArray1.Margin = new System.Windows.Forms.Padding(4);
            this.localPathArray1.Name = "localPathArray1";
            this.localPathArray1.PathArray = new string[0];
            this.localPathArray1.Size = new System.Drawing.Size(347, 141);
            this.localPathArray1.TabIndex = 6;
            // 
            // btnLoadResource
            // 
            this.btnLoadResource.Location = new System.Drawing.Point(278, 219);
            this.btnLoadResource.Name = "btnLoadResource";
            this.btnLoadResource.Size = new System.Drawing.Size(56, 23);
            this.btnLoadResource.TabIndex = 5;
            this.btnLoadResource.Text = "Load";
            this.btnLoadResource.UseVisualStyleBackColor = true;
            this.btnLoadResource.Click += new System.EventHandler(this.btnLoadResource_Click);
            // 
            // cmbResourceLocationType
            // 
            this.cmbResourceLocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResourceLocationType.FormattingEnabled = true;
            this.cmbResourceLocationType.Location = new System.Drawing.Point(140, 19);
            this.cmbResourceLocationType.Name = "cmbResourceLocationType";
            this.cmbResourceLocationType.Size = new System.Drawing.Size(182, 21);
            this.cmbResourceLocationType.TabIndex = 0;
            this.cmbResourceLocationType.SelectedIndexChanged += new System.EventHandler(this.cbResourceLocationType_SelectedIndexChanged);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(140, 46);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(182, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(127, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "Resource Location Type:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Group Name:";
            // 
            // tpLocalCache
            // 
            this.tpLocalCache.Controls.Add(this.ntbLocalCacheCurrentSizeParams);
            this.tpLocalCache.Controls.Add(this.label21);
            this.tpLocalCache.Controls.Add(this.tbLocalCacheFolder);
            this.tpLocalCache.Controls.Add(this.label22);
            this.tpLocalCache.Controls.Add(this.txtLocalCacheSubFolder);
            this.tpLocalCache.Controls.Add(this.dgvLocalCacheLayersParams);
            this.tpLocalCache.Controls.Add(this.btnGetLocalCacheParams);
            this.tpLocalCache.Controls.Add(this.label20);
            this.tpLocalCache.Controls.Add(this.btnSetMapLayersLocalCacheSize);
            this.tpLocalCache.Controls.Add(this.btnRemoveMapLayerFromLocalCache);
            this.tpLocalCache.Controls.Add(this.btnRemoveMapLayersLocalCache);
            this.tpLocalCache.Controls.Add(this.ntbLocalCacheMaxSizeParams);
            this.tpLocalCache.Location = new System.Drawing.Point(4, 22);
            this.tpLocalCache.Name = "tpLocalCache";
            this.tpLocalCache.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocalCache.Size = new System.Drawing.Size(342, 235);
            this.tpLocalCache.TabIndex = 3;
            this.tpLocalCache.Text = "Local Cache";
            this.tpLocalCache.UseVisualStyleBackColor = true;
            this.tpLocalCache.Click += new System.EventHandler(this.tpLocalCache_Click);
            // 
            // ntbLocalCacheCurrentSizeParams
            // 
            this.ntbLocalCacheCurrentSizeParams.Location = new System.Drawing.Point(78, 81);
            this.ntbLocalCacheCurrentSizeParams.Name = "ntbLocalCacheCurrentSizeParams";
            this.ntbLocalCacheCurrentSizeParams.Size = new System.Drawing.Size(57, 20);
            this.ntbLocalCacheCurrentSizeParams.TabIndex = 13;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 84);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 13);
            this.label21.TabIndex = 12;
            this.label21.Text = "Current Size:";
            // 
            // tbLocalCacheFolder
            // 
            this.tbLocalCacheFolder.Location = new System.Drawing.Point(142, 102);
            this.tbLocalCacheFolder.Name = "tbLocalCacheFolder";
            this.tbLocalCacheFolder.Size = new System.Drawing.Size(194, 20);
            this.tbLocalCacheFolder.TabIndex = 11;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 105);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(102, 13);
            this.label22.TabIndex = 10;
            this.label22.Text = "Local Cache Folder:";
            // 
            // txtLocalCacheSubFolder
            // 
            this.txtLocalCacheSubFolder.Location = new System.Drawing.Point(9, 11);
            this.txtLocalCacheSubFolder.Name = "txtLocalCacheSubFolder";
            this.txtLocalCacheSubFolder.Size = new System.Drawing.Size(126, 20);
            this.txtLocalCacheSubFolder.TabIndex = 9;
            // 
            // dgvLocalCacheLayersParams
            // 
            this.dgvLocalCacheLayersParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalCacheLayersParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSubFolder,
            this.colOriginalFolder});
            this.dgvLocalCacheLayersParams.Location = new System.Drawing.Point(9, 133);
            this.dgvLocalCacheLayersParams.Name = "dgvLocalCacheLayersParams";
            this.dgvLocalCacheLayersParams.Size = new System.Drawing.Size(327, 124);
            this.dgvLocalCacheLayersParams.TabIndex = 7;
            // 
            // colSubFolder
            // 
            this.colSubFolder.HeaderText = "Sub Folder";
            this.colSubFolder.Name = "colSubFolder";
            this.colSubFolder.ReadOnly = true;
            this.colSubFolder.Width = 130;
            // 
            // colOriginalFolder
            // 
            this.colOriginalFolder.HeaderText = "Original Folder";
            this.colOriginalFolder.Name = "colOriginalFolder";
            this.colOriginalFolder.ReadOnly = true;
            this.colOriginalFolder.Width = 130;
            // 
            // btnGetLocalCacheParams
            // 
            this.btnGetLocalCacheParams.Location = new System.Drawing.Point(139, 263);
            this.btnGetLocalCacheParams.Name = "btnGetLocalCacheParams";
            this.btnGetLocalCacheParams.Size = new System.Drawing.Size(197, 23);
            this.btnGetLocalCacheParams.TabIndex = 5;
            this.btnGetLocalCacheParams.Text = "Get Local Cache Params";
            this.btnGetLocalCacheParams.UseVisualStyleBackColor = true;
            this.btnGetLocalCacheParams.Click += new System.EventHandler(this.btnGetLocalCacheParams_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 58);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(53, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "Max Size:";
            // 
            // btnSetMapLayersLocalCacheSize
            // 
            this.btnSetMapLayersLocalCacheSize.Location = new System.Drawing.Point(142, 53);
            this.btnSetMapLayersLocalCacheSize.Name = "btnSetMapLayersLocalCacheSize";
            this.btnSetMapLayersLocalCacheSize.Size = new System.Drawing.Size(197, 23);
            this.btnSetMapLayersLocalCacheSize.TabIndex = 2;
            this.btnSetMapLayersLocalCacheSize.Text = "Set Map Layers Local Cache Size";
            this.btnSetMapLayersLocalCacheSize.UseVisualStyleBackColor = true;
            this.btnSetMapLayersLocalCacheSize.Click += new System.EventHandler(this.btnSetMapLayersLocalCacheSize_Click);
            // 
            // btnRemoveMapLayerFromLocalCache
            // 
            this.btnRemoveMapLayerFromLocalCache.Location = new System.Drawing.Point(142, 9);
            this.btnRemoveMapLayerFromLocalCache.Name = "btnRemoveMapLayerFromLocalCache";
            this.btnRemoveMapLayerFromLocalCache.Size = new System.Drawing.Size(197, 23);
            this.btnRemoveMapLayerFromLocalCache.TabIndex = 1;
            this.btnRemoveMapLayerFromLocalCache.Text = "Remove Map Layer From Local Cache";
            this.btnRemoveMapLayerFromLocalCache.UseVisualStyleBackColor = true;
            this.btnRemoveMapLayerFromLocalCache.Click += new System.EventHandler(this.btnRemoveMapLayerFromLocalCache_Click);
            // 
            // btnRemoveMapLayersLocalCache
            // 
            this.btnRemoveMapLayersLocalCache.Location = new System.Drawing.Point(139, 292);
            this.btnRemoveMapLayersLocalCache.Name = "btnRemoveMapLayersLocalCache";
            this.btnRemoveMapLayersLocalCache.Size = new System.Drawing.Size(197, 23);
            this.btnRemoveMapLayersLocalCache.TabIndex = 0;
            this.btnRemoveMapLayersLocalCache.Text = "Remove Map Layers Local Cache";
            this.btnRemoveMapLayersLocalCache.UseVisualStyleBackColor = true;
            this.btnRemoveMapLayersLocalCache.Click += new System.EventHandler(this.btnRemoveMapLayersLocalCache_Click);
            // 
            // ntbLocalCacheMaxSizeParams
            // 
            this.ntbLocalCacheMaxSizeParams.Location = new System.Drawing.Point(78, 55);
            this.ntbLocalCacheMaxSizeParams.Name = "ntbLocalCacheMaxSizeParams";
            this.ntbLocalCacheMaxSizeParams.Size = new System.Drawing.Size(57, 20);
            this.ntbLocalCacheMaxSizeParams.TabIndex = 4;
            // 
            // CreateDataSQControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tabControl1);
            this.Name = "CreateDataSQControl";
            this.Size = new System.Drawing.Size(350, 263);
            this.gbGeneral.ResumeLayout(false);
            this.gbGeneral.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpCreateDataSQ.ResumeLayout(false);
            this.tpCreateDataSQ.PerformLayout();
            this.tpCreateDevice.ResumeLayout(false);
            this.tpDeviceOperations.ResumeLayout(false);
            this.gbUnloadResource.ResumeLayout(false);
            this.gbUnloadResource.PerformLayout();
            this.gbLoadResource.ResumeLayout(false);
            this.gbLoadResource.PerformLayout();
            this.tpLocalCache.ResumeLayout(false);
            this.tpLocalCache.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalCacheLayersParams)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSelectOverlayManager;
        private System.Windows.Forms.Label lblOverlayManagerSelectionStatus;
        private System.Windows.Forms.GroupBox gbGeneral;
        private CtrlGridCoordinateSystem ctrlCreateDataSQGridCoordinateSystem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpCreateDataSQ;
        private System.Windows.Forms.TabPage tpCreateDevice;
        private System.Windows.Forms.TabPage tpDeviceOperations;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.ComboBox cmbResourceLocationType;
        private System.Windows.Forms.GroupBox gbUnloadResource;
        private System.Windows.Forms.Button btnUnloadResource;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbUnloadGroupName;
        private System.Windows.Forms.GroupBox gbLoadResource;
        private System.Windows.Forms.Button btnLoadResource;
        private LocalPathArray localPathArray1;
        private System.Windows.Forms.TabPage tpLocalCache;
        private System.Windows.Forms.Button btnRemoveMapLayerFromLocalCache;
        private System.Windows.Forms.Button btnRemoveMapLayersLocalCache;
        private System.Windows.Forms.Button btnSetMapLayersLocalCacheSize;
        private System.Windows.Forms.DataGridView dgvLocalCacheLayersParams;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOriginalFolder;
        private System.Windows.Forms.Button btnGetLocalCacheParams;
        private NumericTextBox ntbLocalCacheMaxSizeParams;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtLocalCacheSubFolder;
        private System.Windows.Forms.TextBox tbLocalCacheFolder;
        private System.Windows.Forms.Label label22;
        private NumericTextBox ntbLocalCacheCurrentSizeParams;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnRemoveOM;
        private CtrlDeviceParams ctrlDeviceParams1;
        private System.Windows.Forms.Button btnCreateDevice;
        private NumericTextBox ntbViewportID;
    }
}
