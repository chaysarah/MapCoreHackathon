namespace MCTester.MapWorld.WizardForms
{
    partial class AddLayerCtrl
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
            this.btnRequestParams = new System.Windows.Forms.Button();
            this.btnWCSOpenServerLayers = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lblWCSServerLayersCount = new System.Windows.Forms.Label();
            this.btnWMTSOpenServerLayers = new System.Windows.Forms.Button();
            this.lblMapCoreServerLayersCount = new System.Windows.Forms.Label();
            this.lblWMTSServerLayersCount = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtWCSServerPath = new System.Windows.Forms.TextBox();
            this.btnMCOpenServerLayers = new System.Windows.Forms.Button();
            this.txtMapCoreServerPath = new System.Windows.Forms.TextBox();
            this.txtWMTSServerPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbSelectFromServer = new System.Windows.Forms.GroupBox();
            this.btnWMSOpenServerLayers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWMSServerLayersCount = new System.Windows.Forms.Label();
            this.txtWMSServerPath = new System.Windows.Forms.TextBox();
            this.m_radLoadFromFile = new System.Windows.Forms.RadioButton();
            this.gbUseExisting = new System.Windows.Forms.GroupBox();
            this.lstLayers = new System.Windows.Forms.ListBox();
            this.txtBaseDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoadBaseDir = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLoadFileName = new System.Windows.Forms.Button();
            this.m_radCreateNew = new System.Windows.Forms.RadioButton();
            this.gbLoadFromFile = new System.Windows.Forms.GroupBox();
            this.gbCreateNewLayer = new System.Windows.Forms.GroupBox();
            this.btnDeleteAllRows = new System.Windows.Forms.Button();
            this.btnAddNewRow = new System.Windows.Forms.Button();
            this.btnDeleteSelectedRows = new System.Windows.Forms.Button();
            this.dgvLayers = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSaveSelected = new System.Windows.Forms.Button();
            this.ucLayerParams1 = new MCTester.Controls.ucLayerParams();
            this.m_radUseExisting = new System.Windows.Forms.RadioButton();
            this.m_radSelectFromServer = new System.Windows.Forms.RadioButton();
            this.lblSelectFromServer = new System.Windows.Forms.Label();
            this.lblLoadFromFile = new System.Windows.Forms.Label();
            this.lblUseExisting = new System.Windows.Forms.Label();
            this.lblCreateNewLayer = new System.Windows.Forms.Label();
            this.gbSelectFromServer.SuspendLayout();
            this.gbUseExisting.SuspendLayout();
            this.gbLoadFromFile.SuspendLayout();
            this.gbCreateNewLayer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRequestParams
            // 
            this.btnRequestParams.Location = new System.Drawing.Point(10, 273);
            this.btnRequestParams.Name = "btnRequestParams";
            this.btnRequestParams.Size = new System.Drawing.Size(102, 23);
            this.btnRequestParams.TabIndex = 104;
            this.btnRequestParams.Text = "Server Request Params";
            this.btnRequestParams.UseVisualStyleBackColor = true;
            this.btnRequestParams.Click += new System.EventHandler(this.btnRequestParams_Click);
            // 
            // btnWCSOpenServerLayers
            // 
            this.btnWCSOpenServerLayers.Location = new System.Drawing.Point(242, 164);
            this.btnWCSOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnWCSOpenServerLayers.Name = "btnWCSOpenServerLayers";
            this.btnWCSOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnWCSOpenServerLayers.TabIndex = 103;
            this.btnWCSOpenServerLayers.Text = "...";
            this.btnWCSOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnWCSOpenServerLayers.Click += new System.EventHandler(this.btnWCSOpenServerLayers_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 145);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 100;
            this.label12.Text = "WCS Layer Server:";
            // 
            // lblWCSServerLayersCount
            // 
            this.lblWCSServerLayersCount.AutoSize = true;
            this.lblWCSServerLayersCount.Location = new System.Drawing.Point(195, 188);
            this.lblWCSServerLayersCount.Name = "lblWCSServerLayersCount";
            this.lblWCSServerLayersCount.Size = new System.Drawing.Size(43, 13);
            this.lblWCSServerLayersCount.TabIndex = 102;
            this.lblWCSServerLayersCount.Text = "0 layers";
            // 
            // btnWMTSOpenServerLayers
            // 
            this.btnWMTSOpenServerLayers.Location = new System.Drawing.Point(242, 104);
            this.btnWMTSOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnWMTSOpenServerLayers.Name = "btnWMTSOpenServerLayers";
            this.btnWMTSOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnWMTSOpenServerLayers.TabIndex = 99;
            this.btnWMTSOpenServerLayers.Text = "...";
            this.btnWMTSOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnWMTSOpenServerLayers.Click += new System.EventHandler(this.btnWMTSOpenServerLayers_Click);
            // 
            // lblMapCoreServerLayersCount
            // 
            this.lblMapCoreServerLayersCount.AutoSize = true;
            this.lblMapCoreServerLayersCount.Location = new System.Drawing.Point(195, 64);
            this.lblMapCoreServerLayersCount.Name = "lblMapCoreServerLayersCount";
            this.lblMapCoreServerLayersCount.Size = new System.Drawing.Size(43, 13);
            this.lblMapCoreServerLayersCount.TabIndex = 92;
            this.lblMapCoreServerLayersCount.Text = "0 layers";
            // 
            // lblWMTSServerLayersCount
            // 
            this.lblWMTSServerLayersCount.AutoSize = true;
            this.lblWMTSServerLayersCount.Location = new System.Drawing.Point(195, 126);
            this.lblWMTSServerLayersCount.Name = "lblWMTSServerLayersCount";
            this.lblWMTSServerLayersCount.Size = new System.Drawing.Size(43, 13);
            this.lblWMTSServerLayersCount.TabIndex = 98;
            this.lblWMTSServerLayersCount.Text = "0 layers";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 23);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(119, 13);
            this.label28.TabIndex = 89;
            this.label28.Text = "Map Core Layer Server:";
            // 
            // txtWCSServerPath
            // 
            this.txtWCSServerPath.Location = new System.Drawing.Point(10, 164);
            this.txtWCSServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtWCSServerPath.Name = "txtWCSServerPath";
            this.txtWCSServerPath.Size = new System.Drawing.Size(230, 20);
            this.txtWCSServerPath.TabIndex = 101;
            // 
            // btnMCOpenServerLayers
            // 
            this.btnMCOpenServerLayers.Location = new System.Drawing.Point(242, 43);
            this.btnMCOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnMCOpenServerLayers.Name = "btnMCOpenServerLayers";
            this.btnMCOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnMCOpenServerLayers.TabIndex = 91;
            this.btnMCOpenServerLayers.Text = "...";
            this.btnMCOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnMCOpenServerLayers.Click += new System.EventHandler(this.btnMCOpenServerLayers_Click);
            // 
            // txtMapCoreServerPath
            // 
            this.txtMapCoreServerPath.Location = new System.Drawing.Point(10, 43);
            this.txtMapCoreServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtMapCoreServerPath.Name = "txtMapCoreServerPath";
            this.txtMapCoreServerPath.Size = new System.Drawing.Size(230, 20);
            this.txtMapCoreServerPath.TabIndex = 90;
            // 
            // txtWMTSServerPath
            // 
            this.txtWMTSServerPath.Location = new System.Drawing.Point(10, 104);
            this.txtWMTSServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtWMTSServerPath.Name = "txtWMTSServerPath";
            this.txtWMTSServerPath.Size = new System.Drawing.Size(230, 20);
            this.txtWMTSServerPath.TabIndex = 97;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 96;
            this.label3.Text = "WMTS Layer Server:";
            // 
            // gbSelectFromServer
            // 
            this.gbSelectFromServer.Controls.Add(this.btnWMSOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.label1);
            this.gbSelectFromServer.Controls.Add(this.lblWMSServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.txtWMSServerPath);
            this.gbSelectFromServer.Controls.Add(this.btnRequestParams);
            this.gbSelectFromServer.Controls.Add(this.btnWCSOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.label12);
            this.gbSelectFromServer.Controls.Add(this.lblWCSServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.btnWMTSOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.lblMapCoreServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.lblWMTSServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.label28);
            this.gbSelectFromServer.Controls.Add(this.txtWCSServerPath);
            this.gbSelectFromServer.Controls.Add(this.btnMCOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.txtMapCoreServerPath);
            this.gbSelectFromServer.Controls.Add(this.txtWMTSServerPath);
            this.gbSelectFromServer.Controls.Add(this.label3);
            this.gbSelectFromServer.Location = new System.Drawing.Point(6, 437);
            this.gbSelectFromServer.Name = "gbSelectFromServer";
            this.gbSelectFromServer.Size = new System.Drawing.Size(275, 302);
            this.gbSelectFromServer.TabIndex = 26;
            this.gbSelectFromServer.TabStop = false;
            // 
            // btnWMSOpenServerLayers
            // 
            this.btnWMSOpenServerLayers.Location = new System.Drawing.Point(244, 226);
            this.btnWMSOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnWMSOpenServerLayers.Name = "btnWMSOpenServerLayers";
            this.btnWMSOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnWMSOpenServerLayers.TabIndex = 112;
            this.btnWMSOpenServerLayers.Text = "...";
            this.btnWMSOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnWMSOpenServerLayers.Click += new System.EventHandler(this.btnWMSOpenServerLayers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 109;
            this.label1.Text = "WMS Layer Server:";
            // 
            // lblWMSServerLayersCount
            // 
            this.lblWMSServerLayersCount.AutoSize = true;
            this.lblWMSServerLayersCount.Location = new System.Drawing.Point(195, 249);
            this.lblWMSServerLayersCount.Name = "lblWMSServerLayersCount";
            this.lblWMSServerLayersCount.Size = new System.Drawing.Size(43, 13);
            this.lblWMSServerLayersCount.TabIndex = 111;
            this.lblWMSServerLayersCount.Text = "0 layers";
            // 
            // txtWMSServerPath
            // 
            this.txtWMSServerPath.Location = new System.Drawing.Point(11, 226);
            this.txtWMSServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtWMSServerPath.Name = "txtWMSServerPath";
            this.txtWMSServerPath.Size = new System.Drawing.Size(230, 20);
            this.txtWMSServerPath.TabIndex = 110;
            // 
            // m_radLoadFromFile
            // 
            this.m_radLoadFromFile.AutoSize = true;
            this.m_radLoadFromFile.Location = new System.Drawing.Point(7, 303);
            this.m_radLoadFromFile.Name = "m_radLoadFromFile";
            this.m_radLoadFromFile.Size = new System.Drawing.Size(94, 17);
            this.m_radLoadFromFile.TabIndex = 22;
            this.m_radLoadFromFile.Text = "Load From File";
            this.m_radLoadFromFile.UseVisualStyleBackColor = true;
            // 
            // gbUseExisting
            // 
            this.gbUseExisting.Controls.Add(this.lstLayers);
            this.gbUseExisting.Location = new System.Drawing.Point(2, 16);
            this.gbUseExisting.Margin = new System.Windows.Forms.Padding(2);
            this.gbUseExisting.Name = "gbUseExisting";
            this.gbUseExisting.Padding = new System.Windows.Forms.Padding(2);
            this.gbUseExisting.Size = new System.Drawing.Size(279, 279);
            this.gbUseExisting.TabIndex = 25;
            this.gbUseExisting.TabStop = false;
            // 
            // lstLayers
            // 
            this.lstLayers.FormattingEnabled = true;
            this.lstLayers.Location = new System.Drawing.Point(2, 12);
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstLayers.Size = new System.Drawing.Size(273, 264);
            this.lstLayers.TabIndex = 12;
            this.lstLayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstLayers_MouseDoubleClick);
            // 
            // txtBaseDirectory
            // 
            this.txtBaseDirectory.Location = new System.Drawing.Point(88, 45);
            this.txtBaseDirectory.Name = "txtBaseDirectory";
            this.txtBaseDirectory.Size = new System.Drawing.Size(145, 20);
            this.txtBaseDirectory.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Base Directory:";
            // 
            // btnLoadBaseDir
            // 
            this.btnLoadBaseDir.Location = new System.Drawing.Point(237, 42);
            this.btnLoadBaseDir.Name = "btnLoadBaseDir";
            this.btnLoadBaseDir.Size = new System.Drawing.Size(26, 23);
            this.btnLoadBaseDir.TabIndex = 9;
            this.btnLoadBaseDir.Text = "...";
            this.btnLoadBaseDir.UseVisualStyleBackColor = true;
            this.btnLoadBaseDir.Click += new System.EventHandler(this.btnLoadBaseDir_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(88, 19);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(145, 20);
            this.txtFileName.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "File Name:";
            // 
            // btnLoadFileName
            // 
            this.btnLoadFileName.Location = new System.Drawing.Point(237, 17);
            this.btnLoadFileName.Name = "btnLoadFileName";
            this.btnLoadFileName.Size = new System.Drawing.Size(26, 23);
            this.btnLoadFileName.TabIndex = 6;
            this.btnLoadFileName.Text = "...";
            this.btnLoadFileName.UseVisualStyleBackColor = true;
            this.btnLoadFileName.Click += new System.EventHandler(this.btnLoadFileName_Click);
            // 
            // m_radCreateNew
            // 
            this.m_radCreateNew.AutoSize = true;
            this.m_radCreateNew.Location = new System.Drawing.Point(288, 0);
            this.m_radCreateNew.Name = "m_radCreateNew";
            this.m_radCreateNew.Size = new System.Drawing.Size(110, 17);
            this.m_radCreateNew.TabIndex = 20;
            this.m_radCreateNew.Text = "Create New Layer";
            this.m_radCreateNew.UseVisualStyleBackColor = true;
            // 
            // gbLoadFromFile
            // 
            this.gbLoadFromFile.Controls.Add(this.txtBaseDirectory);
            this.gbLoadFromFile.Controls.Add(this.label4);
            this.gbLoadFromFile.Controls.Add(this.btnLoadBaseDir);
            this.gbLoadFromFile.Controls.Add(this.txtFileName);
            this.gbLoadFromFile.Controls.Add(this.label5);
            this.gbLoadFromFile.Controls.Add(this.btnLoadFileName);
            this.gbLoadFromFile.Location = new System.Drawing.Point(6, 316);
            this.gbLoadFromFile.Name = "gbLoadFromFile";
            this.gbLoadFromFile.Size = new System.Drawing.Size(273, 88);
            this.gbLoadFromFile.TabIndex = 24;
            this.gbLoadFromFile.TabStop = false;
            // 
            // gbCreateNewLayer
            // 
            this.gbCreateNewLayer.Controls.Add(this.btnDeleteAllRows);
            this.gbCreateNewLayer.Controls.Add(this.btnAddNewRow);
            this.gbCreateNewLayer.Controls.Add(this.btnDeleteSelectedRows);
            this.gbCreateNewLayer.Controls.Add(this.dgvLayers);
            this.gbCreateNewLayer.Controls.Add(this.btnSaveSelected);
            this.gbCreateNewLayer.Controls.Add(this.ucLayerParams1);
            this.gbCreateNewLayer.Location = new System.Drawing.Point(286, 17);
            this.gbCreateNewLayer.Name = "gbCreateNewLayer";
            this.gbCreateNewLayer.Size = new System.Drawing.Size(798, 810);
            this.gbCreateNewLayer.TabIndex = 21;
            this.gbCreateNewLayer.TabStop = false;
            // 
            // btnDeleteAllRows
            // 
            this.btnDeleteAllRows.Location = new System.Drawing.Point(701, 19);
            this.btnDeleteAllRows.Name = "btnDeleteAllRows";
            this.btnDeleteAllRows.Size = new System.Drawing.Size(91, 23);
            this.btnDeleteAllRows.TabIndex = 75;
            this.btnDeleteAllRows.Text = "Delete All Rows";
            this.btnDeleteAllRows.UseVisualStyleBackColor = true;
            // 
            // btnAddNewRow
            // 
            this.btnAddNewRow.Location = new System.Drawing.Point(712, 77);
            this.btnAddNewRow.Name = "btnAddNewRow";
            this.btnAddNewRow.Size = new System.Drawing.Size(80, 23);
            this.btnAddNewRow.TabIndex = 74;
            this.btnAddNewRow.Text = "Create New Row";
            this.btnAddNewRow.UseVisualStyleBackColor = true;
            this.btnAddNewRow.Click += new System.EventHandler(this.btnAddNewRow_Click);
            // 
            // btnDeleteSelectedRows
            // 
            this.btnDeleteSelectedRows.Enabled = false;
            this.btnDeleteSelectedRows.Location = new System.Drawing.Point(669, 48);
            this.btnDeleteSelectedRows.Name = "btnDeleteSelectedRows";
            this.btnDeleteSelectedRows.Size = new System.Drawing.Size(123, 23);
            this.btnDeleteSelectedRows.TabIndex = 73;
            this.btnDeleteSelectedRows.Text = "Delete Selected Rows";
            this.btnDeleteSelectedRows.UseVisualStyleBackColor = true;
            // 
            // dgvLayers
            // 
            this.dgvLayers.AllowUserToAddRows = false;
            this.dgvLayers.AllowUserToDeleteRows = false;
            this.dgvLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column5});
            this.dgvLayers.Location = new System.Drawing.Point(4, 15);
            this.dgvLayers.MultiSelect = false;
            this.dgvLayers.Name = "dgvLayers";
            this.dgvLayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLayers.Size = new System.Drawing.Size(659, 113);
            this.dgvLayers.TabIndex = 71;
            this.dgvLayers.SelectionChanged += new System.EventHandler(this.dgvLayers_SelectionChanged);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.Width = 40;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Layer Data";
            this.Column5.Name = "Column5";
            // 
            // btnSaveSelected
            // 
            this.btnSaveSelected.Enabled = false;
            this.btnSaveSelected.Location = new System.Drawing.Point(702, 106);
            this.btnSaveSelected.Name = "btnSaveSelected";
            this.btnSaveSelected.Size = new System.Drawing.Size(90, 23);
            this.btnSaveSelected.TabIndex = 72;
            this.btnSaveSelected.Text = "Save Selected";
            this.btnSaveSelected.UseVisualStyleBackColor = true;
            this.btnSaveSelected.Click += new System.EventHandler(this.btnSaveSelected_Click);
            // 
            // ucLayerParams1
            // 
            this.ucLayerParams1.Location = new System.Drawing.Point(6, 135);
            this.ucLayerParams1.Name = "ucLayerParams1";
            this.ucLayerParams1.Size = new System.Drawing.Size(786, 667);
            this.ucLayerParams1.TabIndex = 70;
            // 
            // m_radUseExisting
            // 
            this.m_radUseExisting.AutoSize = true;
            this.m_radUseExisting.Checked = true;
            this.m_radUseExisting.Location = new System.Drawing.Point(3, 0);
            this.m_radUseExisting.Name = "m_radUseExisting";
            this.m_radUseExisting.Size = new System.Drawing.Size(83, 17);
            this.m_radUseExisting.TabIndex = 19;
            this.m_radUseExisting.TabStop = true;
            this.m_radUseExisting.Text = "Use Existing";
            this.m_radUseExisting.UseVisualStyleBackColor = true;
            // 
            // m_radSelectFromServer
            // 
            this.m_radSelectFromServer.AutoSize = true;
            this.m_radSelectFromServer.Location = new System.Drawing.Point(7, 413);
            this.m_radSelectFromServer.Name = "m_radSelectFromServer";
            this.m_radSelectFromServer.Size = new System.Drawing.Size(115, 17);
            this.m_radSelectFromServer.TabIndex = 23;
            this.m_radSelectFromServer.Text = "Select From Server";
            this.m_radSelectFromServer.UseVisualStyleBackColor = true;
            // 
            // lblSelectFromServer
            // 
            this.lblSelectFromServer.AutoSize = true;
            this.lblSelectFromServer.Location = new System.Drawing.Point(4, 415);
            this.lblSelectFromServer.Name = "lblSelectFromServer";
            this.lblSelectFromServer.Size = new System.Drawing.Size(97, 13);
            this.lblSelectFromServer.TabIndex = 76;
            this.lblSelectFromServer.Text = "Select From Server";
            // 
            // lblLoadFromFile
            // 
            this.lblLoadFromFile.AutoSize = true;
            this.lblLoadFromFile.Location = new System.Drawing.Point(4, 305);
            this.lblLoadFromFile.Name = "lblLoadFromFile";
            this.lblLoadFromFile.Size = new System.Drawing.Size(76, 13);
            this.lblLoadFromFile.TabIndex = 77;
            this.lblLoadFromFile.Text = "Load From File";
            // 
            // lblUseExisting
            // 
            this.lblUseExisting.AutoSize = true;
            this.lblUseExisting.Location = new System.Drawing.Point(4, 2);
            this.lblUseExisting.Name = "lblUseExisting";
            this.lblUseExisting.Size = new System.Drawing.Size(65, 13);
            this.lblUseExisting.TabIndex = 78;
            this.lblUseExisting.Text = "Use Existing";
            // 
            // lblCreateNewLayer
            // 
            this.lblCreateNewLayer.AutoSize = true;
            this.lblCreateNewLayer.Location = new System.Drawing.Point(289, 2);
            this.lblCreateNewLayer.Name = "lblCreateNewLayer";
            this.lblCreateNewLayer.Size = new System.Drawing.Size(92, 13);
            this.lblCreateNewLayer.TabIndex = 77;
            this.lblCreateNewLayer.Text = "Create New Layer";
            // 
            // AddLayerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCreateNewLayer);
            this.Controls.Add(this.lblUseExisting);
            this.Controls.Add(this.lblSelectFromServer);
            this.Controls.Add(this.lblLoadFromFile);
            this.Controls.Add(this.gbSelectFromServer);
            this.Controls.Add(this.m_radLoadFromFile);
            this.Controls.Add(this.gbUseExisting);
            this.Controls.Add(this.m_radCreateNew);
            this.Controls.Add(this.gbLoadFromFile);
            this.Controls.Add(this.gbCreateNewLayer);
            this.Controls.Add(this.m_radUseExisting);
            this.Controls.Add(this.m_radSelectFromServer);
            this.Name = "AddLayerCtrl";
            this.Size = new System.Drawing.Size(1089, 830);
            this.Load += new System.EventHandler(this.AddLayerCtrl_Load);
            this.gbSelectFromServer.ResumeLayout(false);
            this.gbSelectFromServer.PerformLayout();
            this.gbUseExisting.ResumeLayout(false);
            this.gbLoadFromFile.ResumeLayout(false);
            this.gbLoadFromFile.PerformLayout();
            this.gbCreateNewLayer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ucLayerParams ucLayerParams1;
        private System.Windows.Forms.Button btnRequestParams;
        private System.Windows.Forms.Button btnWCSOpenServerLayers;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblWCSServerLayersCount;
        private System.Windows.Forms.Button btnWMTSOpenServerLayers;
        private System.Windows.Forms.Label lblMapCoreServerLayersCount;
        private System.Windows.Forms.Label lblWMTSServerLayersCount;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtWCSServerPath;
        private System.Windows.Forms.Button btnMCOpenServerLayers;
        private System.Windows.Forms.TextBox txtMapCoreServerPath;
        private System.Windows.Forms.TextBox txtWMTSServerPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbSelectFromServer;
        private System.Windows.Forms.RadioButton m_radLoadFromFile;
        private System.Windows.Forms.GroupBox gbUseExisting;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoadBaseDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoadFileName;
        private System.Windows.Forms.RadioButton m_radCreateNew;
        private System.Windows.Forms.GroupBox gbLoadFromFile;
        private System.Windows.Forms.GroupBox gbCreateNewLayer;
        private System.Windows.Forms.RadioButton m_radUseExisting;
        private System.Windows.Forms.RadioButton m_radSelectFromServer;
        private System.Windows.Forms.DataGridView dgvLayers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button btnSaveSelected;
        private System.Windows.Forms.Button btnWMSOpenServerLayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWMSServerLayersCount;
        private System.Windows.Forms.TextBox txtWMSServerPath;
        private System.Windows.Forms.Button btnDeleteAllRows;
        private System.Windows.Forms.Button btnAddNewRow;
        private System.Windows.Forms.Button btnDeleteSelectedRows;
        private System.Windows.Forms.Label lblSelectFromServer;
        private System.Windows.Forms.Label lblLoadFromFile;
        private System.Windows.Forms.Label lblUseExisting;
        private System.Windows.Forms.Label lblCreateNewLayer;
        public System.Windows.Forms.TextBox txtBaseDirectory;
        public System.Windows.Forms.TextBox txtFileName;
        public System.Windows.Forms.ListBox lstLayers;
    }
}
