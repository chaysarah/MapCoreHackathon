namespace MCTester.General_Forms
{
    partial class frmVectorXMLCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVectorXMLCreator));
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLoadXMLFile = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.chxbIsLiteVectorLayer = new System.Windows.Forms.CheckBox();
            this.btnConvertVectorLayer = new System.Windows.Forms.Button();
            this.btnTilingScheme = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.strMetaDataFormat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.strConvertedLayerName = new System.Windows.Forms.TextBox();
            this.chxShowClipping = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbMaxSizeFactor = new MCTester.Controls.NumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMinSizeFactor = new MCTester.Controls.NumericTextBox();
            this.btnAddVectorLayerTilesData = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cbSavingVersionCompatibility = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlVectorLayerSettings = new System.Windows.Forms.PropertyGrid();
            this.ctrlBrowseInputXML = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlBrowseOutputXMLFile = new MCTester.Controls.CtrlBrowseControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gbAttrs = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lstAttributes = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbSaveLayerAttributes = new System.Windows.Forms.CheckBox();
            this.tbLayerAttributesIdsFilter = new System.Windows.Forms.TextBox();
            this.cgbIsCreateMetaData = new MCTester.Controls.CheckGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstFieldIds = new System.Windows.Forms.ComboBox();
            this.cbIsUseFilter = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbFieldIdsFilter = new System.Windows.Forms.TextBox();
            this.ctrlRawVectorParams1 = new MCTester.Controls.CtrlRawVectorParams();
            this.ctrlBrowseFileConvertVector = new MCTester.Controls.CtrlBrowseControl();
            this.ntxNumTilesInFileEdge = new MCTester.Controls.NumericTextBox();
            this.ctrlBrowseFolderConvertVector = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlSMcBoxClipRect = new MCTester.Controls.CtrlSMcBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chxSelectAll = new System.Windows.Forms.CheckBox();
            this.dgvRawVectorLayers = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.layer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbAttrs.SuspendLayout();
            this.cgbIsCreateMetaData.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRawVectorLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(516, 298);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(114, 24);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate XML";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnLoadXMLFile
            // 
            this.btnLoadXMLFile.Location = new System.Drawing.Point(516, 17);
            this.btnLoadXMLFile.Name = "btnLoadXMLFile";
            this.btnLoadXMLFile.Size = new System.Drawing.Size(114, 23);
            this.btnLoadXMLFile.TabIndex = 16;
            this.btnLoadXMLFile.Text = "Load XML";
            this.btnLoadXMLFile.UseVisualStyleBackColor = true;
            this.btnLoadXMLFile.Click += new System.EventHandler(this.btnLoadXMLFile_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(868, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = resources.GetString("label6.Text");
            // 
            // chxbIsLiteVectorLayer
            // 
            this.chxbIsLiteVectorLayer.AutoSize = true;
            this.chxbIsLiteVectorLayer.Location = new System.Drawing.Point(13, 359);
            this.chxbIsLiteVectorLayer.Name = "chxbIsLiteVectorLayer";
            this.chxbIsLiteVectorLayer.Size = new System.Drawing.Size(117, 17);
            this.chxbIsLiteVectorLayer.TabIndex = 26;
            this.chxbIsLiteVectorLayer.Text = "Is Lite Vector Layer";
            this.chxbIsLiteVectorLayer.UseVisualStyleBackColor = true;
            // 
            // btnConvertVectorLayer
            // 
            this.btnConvertVectorLayer.Location = new System.Drawing.Point(511, 859);
            this.btnConvertVectorLayer.Name = "btnConvertVectorLayer";
            this.btnConvertVectorLayer.Size = new System.Drawing.Size(109, 23);
            this.btnConvertVectorLayer.TabIndex = 29;
            this.btnConvertVectorLayer.Text = "Convert Vector";
            this.btnConvertVectorLayer.UseVisualStyleBackColor = true;
            this.btnConvertVectorLayer.Click += new System.EventHandler(this.btnConvertVectorLayer_Click);
            // 
            // btnTilingScheme
            // 
            this.btnTilingScheme.Location = new System.Drawing.Point(330, 301);
            this.btnTilingScheme.Margin = new System.Windows.Forms.Padding(2);
            this.btnTilingScheme.Name = "btnTilingScheme";
            this.btnTilingScheme.Size = new System.Drawing.Size(86, 24);
            this.btnTilingScheme.TabIndex = 99;
            this.btnTilingScheme.Text = "Tiling Scheme";
            this.btnTilingScheme.UseVisualStyleBackColor = true;
            this.btnTilingScheme.Click += new System.EventHandler(this.btnTilingScheme_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 330);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 13);
            this.label12.TabIndex = 97;
            this.label12.Text = "Num Tiles In File Edge:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 93;
            this.label5.Text = "Meta Data Format:";
            // 
            // strMetaDataFormat
            // 
            this.strMetaDataFormat.Location = new System.Drawing.Point(145, 301);
            this.strMetaDataFormat.Name = "strMetaDataFormat";
            this.strMetaDataFormat.Size = new System.Drawing.Size(168, 20);
            this.strMetaDataFormat.TabIndex = 92;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 91;
            this.label4.Text = "Converted Layer Name:";
            // 
            // strConvertedLayerName
            // 
            this.strConvertedLayerName.Location = new System.Drawing.Point(139, 75);
            this.strConvertedLayerName.Name = "strConvertedLayerName";
            this.strConvertedLayerName.Size = new System.Drawing.Size(279, 20);
            this.strConvertedLayerName.TabIndex = 90;
            // 
            // chxShowClipping
            // 
            this.chxShowClipping.Appearance = System.Windows.Forms.Appearance.Button;
            this.chxShowClipping.Location = new System.Drawing.Point(353, 181);
            this.chxShowClipping.Name = "chxShowClipping";
            this.chxShowClipping.Size = new System.Drawing.Size(45, 23);
            this.chxShowClipping.TabIndex = 88;
            this.chxShowClipping.Text = "Show";
            this.chxShowClipping.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chxShowClipping.UseVisualStyleBackColor = true;
            this.chxShowClipping.CheckedChanged += new System.EventHandler(this.chxShowClipping_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbMaxSizeFactor);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbMinSizeFactor);
            this.groupBox1.Location = new System.Drawing.Point(210, 330);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 46);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size Factor";
            // 
            // tbMaxSizeFactor
            // 
            this.tbMaxSizeFactor.Location = new System.Drawing.Point(144, 19);
            this.tbMaxSizeFactor.Name = "tbMaxSizeFactor";
            this.tbMaxSizeFactor.Size = new System.Drawing.Size(56, 20);
            this.tbMaxSizeFactor.TabIndex = 81;
            this.tbMaxSizeFactor.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Min:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "Max:";
            // 
            // tbMinSizeFactor
            // 
            this.tbMinSizeFactor.Location = new System.Drawing.Point(38, 19);
            this.tbMinSizeFactor.Name = "tbMinSizeFactor";
            this.tbMinSizeFactor.Size = new System.Drawing.Size(56, 20);
            this.tbMinSizeFactor.TabIndex = 82;
            this.tbMinSizeFactor.Text = "1";
            // 
            // btnAddVectorLayerTilesData
            // 
            this.btnAddVectorLayerTilesData.Location = new System.Drawing.Point(353, 7);
            this.btnAddVectorLayerTilesData.Name = "btnAddVectorLayerTilesData";
            this.btnAddVectorLayerTilesData.Size = new System.Drawing.Size(68, 37);
            this.btnAddVectorLayerTilesData.TabIndex = 51;
            this.btnAddVectorLayerTilesData.Text = "Add Vector Layer Tiles";
            this.btnAddVectorLayerTilesData.UseVisualStyleBackColor = true;
            this.btnAddVectorLayerTilesData.Click += new System.EventHandler(this.btnAddVectorLayerTilesData_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 13);
            this.label11.TabIndex = 46;
            this.label11.Text = "Saving Version Compatibility:";
            // 
            // cbSavingVersionCompatibility
            // 
            this.cbSavingVersionCompatibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSavingVersionCompatibility.FormattingEnabled = true;
            this.cbSavingVersionCompatibility.Location = new System.Drawing.Point(263, 103);
            this.cbSavingVersionCompatibility.Name = "cbSavingVersionCompatibility";
            this.cbSavingVersionCompatibility.Size = new System.Drawing.Size(157, 21);
            this.cbSavingVersionCompatibility.TabIndex = 45;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.ctrlVectorLayerSettings);
            this.groupBox3.Controls.Add(this.ctrlBrowseInputXML);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.btnLoadXMLFile);
            this.groupBox3.Controls.Add(this.btnGenerate);
            this.groupBox3.Controls.Add(this.ctrlBrowseOutputXMLFile);
            this.groupBox3.Location = new System.Drawing.Point(7, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(948, 322);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vector XML Creator";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Settings";
            // 
            // ctrlVectorLayerSettings
            // 
            this.ctrlVectorLayerSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ctrlVectorLayerSettings.Location = new System.Drawing.Point(97, 49);
            this.ctrlVectorLayerSettings.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlVectorLayerSettings.Name = "ctrlVectorLayerSettings";
            this.ctrlVectorLayerSettings.Size = new System.Drawing.Size(769, 245);
            this.ctrlVectorLayerSettings.TabIndex = 18;
            // 
            // ctrlBrowseInputXML
            // 
            this.ctrlBrowseInputXML.AutoSize = true;
            this.ctrlBrowseInputXML.FileName = "";
            this.ctrlBrowseInputXML.Filter = "XML File|*.Xml";
            this.ctrlBrowseInputXML.IsFolderDialog = false;
            this.ctrlBrowseInputXML.IsFullPath = true;
            this.ctrlBrowseInputXML.IsSaveFile = false;
            this.ctrlBrowseInputXML.LabelCaption = "Load Input XML:";
            this.ctrlBrowseInputXML.Location = new System.Drawing.Point(136, 19);
            this.ctrlBrowseInputXML.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseInputXML.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseInputXML.MultiFilesSelect = false;
            this.ctrlBrowseInputXML.Name = "ctrlBrowseInputXML";
            this.ctrlBrowseInputXML.Prefix = "";
            this.ctrlBrowseInputXML.Size = new System.Drawing.Size(372, 24);
            this.ctrlBrowseInputXML.TabIndex = 9;
            // 
            // ctrlBrowseOutputXMLFile
            // 
            this.ctrlBrowseOutputXMLFile.AutoSize = true;
            this.ctrlBrowseOutputXMLFile.FileName = "";
            this.ctrlBrowseOutputXMLFile.Filter = "XML File|*.Xml";
            this.ctrlBrowseOutputXMLFile.IsFolderDialog = false;
            this.ctrlBrowseOutputXMLFile.IsFullPath = true;
            this.ctrlBrowseOutputXMLFile.IsSaveFile = true;
            this.ctrlBrowseOutputXMLFile.LabelCaption = "Output XML:";
            this.ctrlBrowseOutputXMLFile.Location = new System.Drawing.Point(93, 299);
            this.ctrlBrowseOutputXMLFile.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseOutputXMLFile.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseOutputXMLFile.MultiFilesSelect = false;
            this.ctrlBrowseOutputXMLFile.Name = "ctrlBrowseOutputXMLFile";
            this.ctrlBrowseOutputXMLFile.Prefix = "";
            this.ctrlBrowseOutputXMLFile.Size = new System.Drawing.Size(412, 24);
            this.ctrlBrowseOutputXMLFile.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.gbAttrs);
            this.groupBox2.Controls.Add(this.cgbIsCreateMetaData);
            this.groupBox2.Controls.Add(this.ctrlRawVectorParams1);
            this.groupBox2.Controls.Add(this.btnTilingScheme);
            this.groupBox2.Controls.Add(this.ctrlBrowseFileConvertVector);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.btnAddVectorLayerTilesData);
            this.groupBox2.Controls.Add(this.ntxNumTilesInFileEdge);
            this.groupBox2.Controls.Add(this.chxbIsLiteVectorLayer);
            this.groupBox2.Controls.Add(this.chxShowClipping);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ctrlBrowseFolderConvertVector);
            this.groupBox2.Controls.Add(this.cbSavingVersionCompatibility);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.strMetaDataFormat);
            this.groupBox2.Controls.Add(this.strConvertedLayerName);
            this.groupBox2.Controls.Add(this.ctrlSMcBoxClipRect);
            this.groupBox2.Location = new System.Drawing.Point(5, 362);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1124, 493);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Convert Vector Layer Params";
            // 
            // gbAttrs
            // 
            this.gbAttrs.Controls.Add(this.label10);
            this.gbAttrs.Controls.Add(this.lstAttributes);
            this.gbAttrs.Controls.Add(this.label7);
            this.gbAttrs.Controls.Add(this.cbSaveLayerAttributes);
            this.gbAttrs.Controls.Add(this.tbLayerAttributesIdsFilter);
            this.gbAttrs.Enabled = false;
            this.gbAttrs.Location = new System.Drawing.Point(1, 382);
            this.gbAttrs.Name = "gbAttrs";
            this.gbAttrs.Size = new System.Drawing.Size(420, 79);
            this.gbAttrs.TabIndex = 101;
            this.gbAttrs.TabStop = false;
            this.gbAttrs.Text = "Layer Attributes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(141, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 98;
            this.label10.Text = "Attributes Ids:";
            // 
            // lstAttributes
            // 
            this.lstAttributes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstAttributes.FormattingEnabled = true;
            this.lstAttributes.Location = new System.Drawing.Point(244, 46);
            this.lstAttributes.Name = "lstAttributes";
            this.lstAttributes.Size = new System.Drawing.Size(172, 21);
            this.lstAttributes.TabIndex = 97;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(141, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 13);
            this.label7.TabIndex = 94;
            this.label7.Text = "Layer Attributes Ids Filter(1,2,3...):";
            // 
            // cbSaveLayerAttributes
            // 
            this.cbSaveLayerAttributes.AutoSize = true;
            this.cbSaveLayerAttributes.Location = new System.Drawing.Point(10, 21);
            this.cbSaveLayerAttributes.Name = "cbSaveLayerAttributes";
            this.cbSaveLayerAttributes.Size = new System.Drawing.Size(127, 17);
            this.cbSaveLayerAttributes.TabIndex = 96;
            this.cbSaveLayerAttributes.Text = "Save Layer Attributes";
            this.cbSaveLayerAttributes.UseVisualStyleBackColor = true;
            this.cbSaveLayerAttributes.CheckedChanged += new System.EventHandler(this.cbIsUseLayerAttributesFilter_CheckedChanged);
            // 
            // tbLayerAttributesIdsFilter
            // 
            this.tbLayerAttributesIdsFilter.Enabled = false;
            this.tbLayerAttributesIdsFilter.Location = new System.Drawing.Point(316, 20);
            this.tbLayerAttributesIdsFilter.Name = "tbLayerAttributesIdsFilter";
            this.tbLayerAttributesIdsFilter.Size = new System.Drawing.Size(100, 20);
            this.tbLayerAttributesIdsFilter.TabIndex = 95;
            // 
            // cgbIsCreateMetaData
            // 
            this.cgbIsCreateMetaData.Checked = false;
            this.cgbIsCreateMetaData.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.cgbIsCreateMetaData.Controls.Add(this.label2);
            this.cgbIsCreateMetaData.Controls.Add(this.lstFieldIds);
            this.cgbIsCreateMetaData.Controls.Add(this.cbIsUseFilter);
            this.cgbIsCreateMetaData.Controls.Add(this.label9);
            this.cgbIsCreateMetaData.Controls.Add(this.tbFieldIdsFilter);
            this.cgbIsCreateMetaData.Location = new System.Drawing.Point(7, 220);
            this.cgbIsCreateMetaData.Name = "cgbIsCreateMetaData";
            this.cgbIsCreateMetaData.Size = new System.Drawing.Size(414, 77);
            this.cgbIsCreateMetaData.TabIndex = 100;
            this.cgbIsCreateMetaData.TabStop = false;
            this.cgbIsCreateMetaData.Text = "Is Create Meta Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(100, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Field Ids:";
            // 
            // lstFieldIds
            // 
            this.lstFieldIds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lstFieldIds.FormattingEnabled = true;
            this.lstFieldIds.Location = new System.Drawing.Point(219, 45);
            this.lstFieldIds.Name = "lstFieldIds";
            this.lstFieldIds.Size = new System.Drawing.Size(172, 21);
            this.lstFieldIds.TabIndex = 46;
            // 
            // cbIsUseFilter
            // 
            this.cbIsUseFilter.AutoSize = true;
            this.cbIsUseFilter.Location = new System.Drawing.Point(8, 22);
            this.cbIsUseFilter.Name = "cbIsUseFilter";
            this.cbIsUseFilter.Size = new System.Drawing.Size(81, 17);
            this.cbIsUseFilter.TabIndex = 30;
            this.cbIsUseFilter.Text = "Is Use Filter";
            this.cbIsUseFilter.UseVisualStyleBackColor = true;
            this.cbIsUseFilter.CheckedChanged += new System.EventHandler(this.cbIsUseFilter_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(100, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Field Ids Filter(1,2,3...):";
            // 
            // tbFieldIdsFilter
            // 
            this.tbFieldIdsFilter.Enabled = false;
            this.tbFieldIdsFilter.Location = new System.Drawing.Point(219, 19);
            this.tbFieldIdsFilter.Name = "tbFieldIdsFilter";
            this.tbFieldIdsFilter.Size = new System.Drawing.Size(172, 20);
            this.tbFieldIdsFilter.TabIndex = 25;
            // 
            // ctrlRawVectorParams1
            // 
            this.ctrlRawVectorParams1.Location = new System.Drawing.Point(430, 9);
            this.ctrlRawVectorParams1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlRawVectorParams1.Name = "ctrlRawVectorParams1";
            this.ctrlRawVectorParams1.Size = new System.Drawing.Size(682, 484);
            this.ctrlRawVectorParams1.TabIndex = 31;
            this.ctrlRawVectorParams1.TargetGridCoordinateSystem = null;
            // 
            // ctrlBrowseFileConvertVector
            // 
            this.ctrlBrowseFileConvertVector.AutoSize = true;
            this.ctrlBrowseFileConvertVector.FileName = "";
            this.ctrlBrowseFileConvertVector.Filter = "";
            this.ctrlBrowseFileConvertVector.IsFolderDialog = false;
            this.ctrlBrowseFileConvertVector.IsFullPath = true;
            this.ctrlBrowseFileConvertVector.IsSaveFile = false;
            this.ctrlBrowseFileConvertVector.LabelCaption = "Data Source:             ";
            this.ctrlBrowseFileConvertVector.Location = new System.Drawing.Point(119, 20);
            this.ctrlBrowseFileConvertVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseFileConvertVector.MinimumSize = new System.Drawing.Size(100, 24);
            this.ctrlBrowseFileConvertVector.MultiFilesSelect = false;
            this.ctrlBrowseFileConvertVector.Name = "ctrlBrowseFileConvertVector";
            this.ctrlBrowseFileConvertVector.Prefix = "";
            this.ctrlBrowseFileConvertVector.Size = new System.Drawing.Size(234, 24);
            this.ctrlBrowseFileConvertVector.TabIndex = 9;
            // 
            // ntxNumTilesInFileEdge
            // 
            this.ntxNumTilesInFileEdge.Location = new System.Drawing.Point(145, 327);
            this.ntxNumTilesInFileEdge.Name = "ntxNumTilesInFileEdge";
            this.ntxNumTilesInFileEdge.Size = new System.Drawing.Size(56, 20);
            this.ntxNumTilesInFileEdge.TabIndex = 98;
            this.ntxNumTilesInFileEdge.Text = "8";
            // 
            // ctrlBrowseFolderConvertVector
            // 
            this.ctrlBrowseFolderConvertVector.AutoSize = true;
            this.ctrlBrowseFolderConvertVector.FileName = "";
            this.ctrlBrowseFolderConvertVector.Filter = "";
            this.ctrlBrowseFolderConvertVector.IsFolderDialog = true;
            this.ctrlBrowseFolderConvertVector.IsFullPath = true;
            this.ctrlBrowseFolderConvertVector.IsSaveFile = false;
            this.ctrlBrowseFolderConvertVector.LabelCaption = "Destination Directory:";
            this.ctrlBrowseFolderConvertVector.Location = new System.Drawing.Point(119, 47);
            this.ctrlBrowseFolderConvertVector.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseFolderConvertVector.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseFolderConvertVector.MultiFilesSelect = false;
            this.ctrlBrowseFolderConvertVector.Name = "ctrlBrowseFolderConvertVector";
            this.ctrlBrowseFolderConvertVector.Prefix = "";
            this.ctrlBrowseFolderConvertVector.Size = new System.Drawing.Size(300, 24);
            this.ctrlBrowseFolderConvertVector.TabIndex = 21;
            // 
            // ctrlSMcBoxClipRect
            // 
            this.ctrlSMcBoxClipRect.GroupBoxText = "Clip Rectangle";
            this.ctrlSMcBoxClipRect.Location = new System.Drawing.Point(11, 130);
            this.ctrlSMcBoxClipRect.Name = "ctrlSMcBoxClipRect";
            this.ctrlSMcBoxClipRect.Size = new System.Drawing.Size(410, 84);
            this.ctrlSMcBoxClipRect.TabIndex = 86;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1124, 361);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1116, 335);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "XML Converter";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.chxSelectAll);
            this.tabPage2.Controls.Add(this.dgvRawVectorLayers);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1116, 335);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Exists Raw Vector Layers";
            // 
            // chxSelectAll
            // 
            this.chxSelectAll.AutoSize = true;
            this.chxSelectAll.Location = new System.Drawing.Point(71, 23);
            this.chxSelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.chxSelectAll.Name = "chxSelectAll";
            this.chxSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chxSelectAll.TabIndex = 100;
            this.chxSelectAll.UseVisualStyleBackColor = true;
            this.chxSelectAll.CheckedChanged += new System.EventHandler(this.chxSelectAll_CheckedChanged);
            // 
            // dgvRawVectorLayers
            // 
            this.dgvRawVectorLayers.AllowUserToAddRows = false;
            this.dgvRawVectorLayers.AllowUserToDeleteRows = false;
            this.dgvRawVectorLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRawVectorLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.layer});
            this.dgvRawVectorLayers.Location = new System.Drawing.Point(16, 18);
            this.dgvRawVectorLayers.MultiSelect = false;
            this.dgvRawVectorLayers.Name = "dgvRawVectorLayers";
            this.dgvRawVectorLayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvRawVectorLayers.Size = new System.Drawing.Size(1062, 302);
            this.dgvRawVectorLayers.TabIndex = 0;
            this.dgvRawVectorLayers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRawVectorLayers_CellContentClick);
            this.dgvRawVectorLayers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRawVectorLayers_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // layer
            // 
            this.layer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.layer.HeaderText = "Layer";
            this.layer.Name = "layer";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 13);
            this.label13.TabIndex = 92;
            this.label13.Text = "Converted Layer Name:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 304);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 93;
            this.label14.Text = "Output XML:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 13);
            this.label15.TabIndex = 94;
            this.label15.Text = "Destination Directory:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 13);
            this.label17.TabIndex = 103;
            this.label17.Text = "Data Source:";
            // 
            // frmVectorXMLCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1141, 884);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnConvertVectorLayer);
            this.Name = "frmVectorXMLCreator";
            this.Text = "Vector Production";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbAttrs.ResumeLayout(false);
            this.gbAttrs.PerformLayout();
            this.cgbIsCreateMetaData.ResumeLayout(false);
            this.cgbIsCreateMetaData.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRawVectorLayers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private Controls.CtrlBrowseControl ctrlBrowseOutputXMLFile;
        private Controls.CtrlBrowseControl ctrlBrowseInputXML;
        private System.Windows.Forms.Button btnLoadXMLFile;
        private System.Windows.Forms.Label label6;
        private Controls.CtrlBrowseControl ctrlBrowseFileConvertVector;
        private Controls.CtrlBrowseControl ctrlBrowseFolderConvertVector;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbFieldIdsFilter;
        private System.Windows.Forms.CheckBox chxbIsLiteVectorLayer;
        private System.Windows.Forms.Button btnConvertVectorLayer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbIsUseFilter;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbSavingVersionCompatibility;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PropertyGrid ctrlVectorLayerSettings;
        private System.Windows.Forms.Button btnAddVectorLayerTilesData;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.NumericTextBox tbMaxSizeFactor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private Controls.NumericTextBox tbMinSizeFactor;
        private Controls.CtrlSMcBox ctrlSMcBoxClipRect;
        private System.Windows.Forms.CheckBox chxShowClipping;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox strMetaDataFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox strConvertedLayerName;
        private System.Windows.Forms.Label label12;
        private Controls.NumericTextBox ntxNumTilesInFileEdge;
        private System.Windows.Forms.Button btnTilingScheme;
        private Controls.CtrlRawVectorParams ctrlRawVectorParams1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvRawVectorLayers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn layer;
        private System.Windows.Forms.CheckBox chxSelectAll;
        private Controls.CheckGroupBox cgbIsCreateMetaData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox lstFieldIds;
        private System.Windows.Forms.GroupBox gbAttrs;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox lstAttributes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbSaveLayerAttributes;
        private System.Windows.Forms.TextBox tbLayerAttributesIdsFilter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
    }
}