namespace MCTester.General_Forms
{
    partial class frmCreateImageCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateImageCalc));
            this.btnOK = new System.Windows.Forms.Button();
            this.chxLoropIsFileName = new System.Windows.Forms.CheckBox();
            this.cmbImageType = new System.Windows.Forms.ComboBox();
            this.ctrlCameraParams1 = new MCTester.Controls.CtrlCameraParams();
            this.lstExistingImageCalc = new System.Windows.Forms.ListBox();
            this.lblImageType = new System.Windows.Forms.Label();
            this.chxPixelWorkingAreaValid = new System.Windows.Forms.CheckBox();
            this.gbPixelWorkingArea = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ctrl2DVectorPixelWorkingAreaUpperRight = new MCTester.Controls.Ctrl2DVector();
            this.label10 = new System.Windows.Forms.Label();
            this.ctrl2DVectorPixelWorkingAreaLowerLeft = new MCTester.Controls.Ctrl2DVector();
            this.chxIsInPixelWorkingArea = new System.Windows.Forms.CheckBox();
            this.gbCameraParams = new System.Windows.Forms.GroupBox();
            this.pnlTerrains = new System.Windows.Forms.Panel();
            this.lstTerrains = new System.Windows.Forms.ListBox();
            this.btnTerrainListClear = new System.Windows.Forms.Button();
            this.btnTerrainListAdd = new System.Windows.Forms.Button();
            this.btnTerrainListRemove = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbTerrain = new System.Windows.Forms.RadioButton();
            this.rbDtm = new System.Windows.Forms.RadioButton();
            this.ctrlGCSImageCalc = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.lblDtmMapLayer = new System.Windows.Forms.Label();
            this.btnAddDtmMapLayer = new System.Windows.Forms.Button();
            this.ctrlBrowseImageDataFileName = new MCTester.Controls.CtrlBrowseControl();
            this.gbImageWorkingArea = new System.Windows.Forms.GroupBox();
            this.btnPixelWorkingAreaSet = new System.Windows.Forms.Button();
            this.btnIsInPixelWorkingAreaGet = new System.Windows.Forms.Button();
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord = new MCTester.Controls.Ctrl2DVector();
            this.btnPixelWorkingAreaValidSet = new System.Windows.Forms.Button();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbPixelWorkingArea.SuspendLayout();
            this.gbCameraParams.SuspendLayout();
            this.pnlTerrains.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbImageWorkingArea.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(297, 726);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 23);
            this.btnOK.TabIndex = 117;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chxLoropIsFileName
            // 
            this.chxLoropIsFileName.AutoSize = true;
            this.chxLoropIsFileName.Checked = true;
            this.chxLoropIsFileName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLoropIsFileName.Location = new System.Drawing.Point(6, 303);
            this.chxLoropIsFileName.Name = "chxLoropIsFileName";
            this.chxLoropIsFileName.Size = new System.Drawing.Size(84, 17);
            this.chxLoropIsFileName.TabIndex = 129;
            this.chxLoropIsFileName.Text = "Is File Name";
            this.chxLoropIsFileName.UseVisualStyleBackColor = true;
            this.chxLoropIsFileName.Visible = false;
            this.chxLoropIsFileName.CheckedChanged += new System.EventHandler(this.chxLoropIsFileName_CheckedChanged);
            // 
            // cmbImageType
            // 
            this.cmbImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageType.FormattingEnabled = true;
            this.cmbImageType.Location = new System.Drawing.Point(75, 19);
            this.cmbImageType.Name = "cmbImageType";
            this.cmbImageType.Size = new System.Drawing.Size(262, 21);
            this.cmbImageType.TabIndex = 127;
            this.cmbImageType.SelectedIndexChanged += new System.EventHandler(this.cmbImageType_SelectedIndexChanged);
            // 
            // ctrlCameraParams1
            // 
            this.ctrlCameraParams1.Location = new System.Drawing.Point(6, 331);
            this.ctrlCameraParams1.Name = "ctrlCameraParams1";
            this.ctrlCameraParams1.Size = new System.Drawing.Size(369, 218);
            this.ctrlCameraParams1.TabIndex = 120;
            // 
            // lstExistingImageCalc
            // 
            this.lstExistingImageCalc.FormattingEnabled = true;
            this.lstExistingImageCalc.Location = new System.Drawing.Point(8, 19);
            this.lstExistingImageCalc.Name = "lstExistingImageCalc";
            this.lstExistingImageCalc.Size = new System.Drawing.Size(140, 95);
            this.lstExistingImageCalc.TabIndex = 120;
            this.lstExistingImageCalc.SelectedIndexChanged += new System.EventHandler(this.lstExistingImageCalc_SelectedIndexChanged);
            this.lstExistingImageCalc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstExistingImageCalc_MouseDown);
            // 
            // lblImageType
            // 
            this.lblImageType.AutoSize = true;
            this.lblImageType.Location = new System.Drawing.Point(9, 22);
            this.lblImageType.Name = "lblImageType";
            this.lblImageType.Size = new System.Drawing.Size(60, 13);
            this.lblImageType.TabIndex = 131;
            this.lblImageType.Text = "ImageType";
            // 
            // chxPixelWorkingAreaValid
            // 
            this.chxPixelWorkingAreaValid.AutoSize = true;
            this.chxPixelWorkingAreaValid.Location = new System.Drawing.Point(6, 47);
            this.chxPixelWorkingAreaValid.Name = "chxPixelWorkingAreaValid";
            this.chxPixelWorkingAreaValid.Size = new System.Drawing.Size(142, 17);
            this.chxPixelWorkingAreaValid.TabIndex = 135;
            this.chxPixelWorkingAreaValid.Text = "Pixel Working Area Valid";
            this.chxPixelWorkingAreaValid.UseVisualStyleBackColor = true;
            // 
            // gbPixelWorkingArea
            // 
            this.gbPixelWorkingArea.Controls.Add(this.label11);
            this.gbPixelWorkingArea.Controls.Add(this.ctrl2DVectorPixelWorkingAreaUpperRight);
            this.gbPixelWorkingArea.Controls.Add(this.label10);
            this.gbPixelWorkingArea.Controls.Add(this.ctrl2DVectorPixelWorkingAreaLowerLeft);
            this.gbPixelWorkingArea.Location = new System.Drawing.Point(2, 70);
            this.gbPixelWorkingArea.Name = "gbPixelWorkingArea";
            this.gbPixelWorkingArea.Size = new System.Drawing.Size(262, 83);
            this.gbPixelWorkingArea.TabIndex = 136;
            this.gbPixelWorkingArea.TabStop = false;
            this.gbPixelWorkingArea.Text = "Pixel Working Area ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Upper Right:";
            // 
            // ctrl2DVectorPixelWorkingAreaUpperRight
            // 
            this.ctrl2DVectorPixelWorkingAreaUpperRight.Location = new System.Drawing.Point(104, 51);
            this.ctrl2DVectorPixelWorkingAreaUpperRight.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DVectorPixelWorkingAreaUpperRight.Name = "ctrl2DVectorPixelWorkingAreaUpperRight";
            this.ctrl2DVectorPixelWorkingAreaUpperRight.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DVectorPixelWorkingAreaUpperRight.TabIndex = 2;
            this.ctrl2DVectorPixelWorkingAreaUpperRight.X = 0D;
            this.ctrl2DVectorPixelWorkingAreaUpperRight.Y = 0D;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Lower Left:";
            // 
            // ctrl2DVectorPixelWorkingAreaLowerLeft
            // 
            this.ctrl2DVectorPixelWorkingAreaLowerLeft.Location = new System.Drawing.Point(104, 20);
            this.ctrl2DVectorPixelWorkingAreaLowerLeft.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DVectorPixelWorkingAreaLowerLeft.Name = "ctrl2DVectorPixelWorkingAreaLowerLeft";
            this.ctrl2DVectorPixelWorkingAreaLowerLeft.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DVectorPixelWorkingAreaLowerLeft.TabIndex = 0;
            this.ctrl2DVectorPixelWorkingAreaLowerLeft.X = 0D;
            this.ctrl2DVectorPixelWorkingAreaLowerLeft.Y = 0D;
            // 
            // chxIsInPixelWorkingArea
            // 
            this.chxIsInPixelWorkingArea.AutoSize = true;
            this.chxIsInPixelWorkingArea.Enabled = false;
            this.chxIsInPixelWorkingArea.Location = new System.Drawing.Point(6, 19);
            this.chxIsInPixelWorkingArea.Name = "chxIsInPixelWorkingArea";
            this.chxIsInPixelWorkingArea.Size = new System.Drawing.Size(139, 17);
            this.chxIsInPixelWorkingArea.TabIndex = 0;
            this.chxIsInPixelWorkingArea.Text = "Is In Pixel Working Area";
            this.chxIsInPixelWorkingArea.UseVisualStyleBackColor = true;
            // 
            // gbCameraParams
            // 
            this.gbCameraParams.Controls.Add(this.pnlTerrains);
            this.gbCameraParams.Controls.Add(this.panel1);
            this.gbCameraParams.Controls.Add(this.ctrlCameraParams1);
            this.gbCameraParams.Controls.Add(this.ctrlGCSImageCalc);
            this.gbCameraParams.Controls.Add(this.lblDtmMapLayer);
            this.gbCameraParams.Controls.Add(this.btnAddDtmMapLayer);
            this.gbCameraParams.Controls.Add(this.lblImageType);
            this.gbCameraParams.Controls.Add(this.cmbImageType);
            this.gbCameraParams.Controls.Add(this.chxLoropIsFileName);
            this.gbCameraParams.Controls.Add(this.ctrlBrowseImageDataFileName);
            this.gbCameraParams.Location = new System.Drawing.Point(7, 6);
            this.gbCameraParams.Name = "gbCameraParams";
            this.gbCameraParams.Size = new System.Drawing.Size(394, 555);
            this.gbCameraParams.TabIndex = 138;
            this.gbCameraParams.TabStop = false;
            this.gbCameraParams.Text = "Camera Params";
            // 
            // pnlTerrains
            // 
            this.pnlTerrains.Controls.Add(this.lstTerrains);
            this.pnlTerrains.Controls.Add(this.btnTerrainListClear);
            this.pnlTerrains.Controls.Add(this.btnTerrainListAdd);
            this.pnlTerrains.Controls.Add(this.btnTerrainListRemove);
            this.pnlTerrains.Location = new System.Drawing.Point(128, 214);
            this.pnlTerrains.Name = "pnlTerrains";
            this.pnlTerrains.Size = new System.Drawing.Size(260, 85);
            this.pnlTerrains.TabIndex = 147;
            // 
            // lstTerrains
            // 
            this.lstTerrains.FormattingEnabled = true;
            this.lstTerrains.Location = new System.Drawing.Point(3, 1);
            this.lstTerrains.Name = "lstTerrains";
            this.lstTerrains.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstTerrains.Size = new System.Drawing.Size(140, 82);
            this.lstTerrains.TabIndex = 143;
            // 
            // btnTerrainListClear
            // 
            this.btnTerrainListClear.Location = new System.Drawing.Point(149, 61);
            this.btnTerrainListClear.Name = "btnTerrainListClear";
            this.btnTerrainListClear.Size = new System.Drawing.Size(59, 23);
            this.btnTerrainListClear.TabIndex = 146;
            this.btnTerrainListClear.Text = "Clear";
            this.btnTerrainListClear.UseVisualStyleBackColor = true;
            this.btnTerrainListClear.Click += new System.EventHandler(this.btnTerrainListClear_Click);
            // 
            // btnTerrainListAdd
            // 
            this.btnTerrainListAdd.Location = new System.Drawing.Point(149, 1);
            this.btnTerrainListAdd.Name = "btnTerrainListAdd";
            this.btnTerrainListAdd.Size = new System.Drawing.Size(59, 23);
            this.btnTerrainListAdd.TabIndex = 144;
            this.btnTerrainListAdd.Text = "Add";
            this.btnTerrainListAdd.UseVisualStyleBackColor = true;
            this.btnTerrainListAdd.Click += new System.EventHandler(this.btnTerrainListAdd_Click);
            // 
            // btnTerrainListRemove
            // 
            this.btnTerrainListRemove.Location = new System.Drawing.Point(149, 27);
            this.btnTerrainListRemove.Name = "btnTerrainListRemove";
            this.btnTerrainListRemove.Size = new System.Drawing.Size(59, 23);
            this.btnTerrainListRemove.TabIndex = 145;
            this.btnTerrainListRemove.Text = "Remove";
            this.btnTerrainListRemove.UseVisualStyleBackColor = true;
            this.btnTerrainListRemove.Click += new System.EventHandler(this.btnTerrainListRemove_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbTerrain);
            this.panel1.Controls.Add(this.rbDtm);
            this.panel1.Location = new System.Drawing.Point(3, 187);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(119, 47);
            this.panel1.TabIndex = 137;
            // 
            // rbTerrain
            // 
            this.rbTerrain.AutoSize = true;
            this.rbTerrain.Location = new System.Drawing.Point(6, 27);
            this.rbTerrain.Name = "rbTerrain";
            this.rbTerrain.Size = new System.Drawing.Size(63, 17);
            this.rbTerrain.TabIndex = 1;
            this.rbTerrain.TabStop = true;
            this.rbTerrain.Text = "Terrains";
            this.rbTerrain.UseVisualStyleBackColor = true;
            this.rbTerrain.CheckedChanged += new System.EventHandler(this.rbTerrain_CheckedChanged);
            // 
            // rbDtm
            // 
            this.rbDtm.AutoSize = true;
            this.rbDtm.Checked = true;
            this.rbDtm.Location = new System.Drawing.Point(6, 4);
            this.rbDtm.Name = "rbDtm";
            this.rbDtm.Size = new System.Drawing.Size(102, 17);
            this.rbDtm.TabIndex = 0;
            this.rbDtm.TabStop = true;
            this.rbDtm.Text = "DTM Map Layer";
            this.rbDtm.UseVisualStyleBackColor = true;
            this.rbDtm.CheckedChanged += new System.EventHandler(this.rbDtm_CheckedChanged);
            // 
            // ctrlGCSImageCalc
            // 
            this.ctrlGCSImageCalc.EnableNewCoordSysCreation = true;
            this.ctrlGCSImageCalc.GridCoordinateSystem = null;
            this.ctrlGCSImageCalc.GroupBoxText = "Grid Coordinate System";
            this.ctrlGCSImageCalc.IsEditable = false;
            this.ctrlGCSImageCalc.Location = new System.Drawing.Point(6, 46);
            this.ctrlGCSImageCalc.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlGCSImageCalc.Name = "ctrlGCSImageCalc";
            this.ctrlGCSImageCalc.Size = new System.Drawing.Size(331, 134);
            this.ctrlGCSImageCalc.TabIndex = 136;
            // 
            // lblDtmMapLayer
            // 
            this.lblDtmMapLayer.AutoSize = true;
            this.lblDtmMapLayer.Location = new System.Drawing.Point(175, 193);
            this.lblDtmMapLayer.Name = "lblDtmMapLayer";
            this.lblDtmMapLayer.Size = new System.Drawing.Size(25, 13);
            this.lblDtmMapLayer.TabIndex = 135;
            this.lblDtmMapLayer.Text = "Null";
            // 
            // btnAddDtmMapLayer
            // 
            this.btnAddDtmMapLayer.Location = new System.Drawing.Point(128, 187);
            this.btnAddDtmMapLayer.Name = "btnAddDtmMapLayer";
            this.btnAddDtmMapLayer.Size = new System.Drawing.Size(47, 26);
            this.btnAddDtmMapLayer.TabIndex = 134;
            this.btnAddDtmMapLayer.Text = "Create";
            this.btnAddDtmMapLayer.UseVisualStyleBackColor = true;
            this.btnAddDtmMapLayer.Click += new System.EventHandler(this.btnAddDtmMapLayer_Click);
            // 
            // ctrlBrowseImageDataFileName
            // 
            this.ctrlBrowseImageDataFileName.AutoSize = true;
            this.ctrlBrowseImageDataFileName.FileName = "";
            this.ctrlBrowseImageDataFileName.Filter = "";
            this.ctrlBrowseImageDataFileName.IsFolderDialog = false;
            this.ctrlBrowseImageDataFileName.IsFullPath = true;
            this.ctrlBrowseImageDataFileName.IsSaveFile = false;
            this.ctrlBrowseImageDataFileName.LabelCaption = "Data File Name:";
            this.ctrlBrowseImageDataFileName.Location = new System.Drawing.Point(88, 304);
            this.ctrlBrowseImageDataFileName.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseImageDataFileName.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseImageDataFileName.MultiFilesSelect = false;
            this.ctrlBrowseImageDataFileName.Name = "ctrlBrowseImageDataFileName";
            this.ctrlBrowseImageDataFileName.Prefix = "";
            this.ctrlBrowseImageDataFileName.Size = new System.Drawing.Size(300, 24);
            this.ctrlBrowseImageDataFileName.TabIndex = 128;
            this.ctrlBrowseImageDataFileName.Visible = false;
            // 
            // gbImageWorkingArea
            // 
            this.gbImageWorkingArea.Controls.Add(this.btnPixelWorkingAreaSet);
            this.gbImageWorkingArea.Controls.Add(this.btnIsInPixelWorkingAreaGet);
            this.gbImageWorkingArea.Controls.Add(this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord);
            this.gbImageWorkingArea.Controls.Add(this.gbPixelWorkingArea);
            this.gbImageWorkingArea.Controls.Add(this.btnPixelWorkingAreaValidSet);
            this.gbImageWorkingArea.Controls.Add(this.chxPixelWorkingAreaValid);
            this.gbImageWorkingArea.Controls.Add(this.chxIsInPixelWorkingArea);
            this.gbImageWorkingArea.Location = new System.Drawing.Point(7, 567);
            this.gbImageWorkingArea.Name = "gbImageWorkingArea";
            this.gbImageWorkingArea.Size = new System.Drawing.Size(369, 157);
            this.gbImageWorkingArea.TabIndex = 137;
            this.gbImageWorkingArea.TabStop = false;
            // 
            // btnPixelWorkingAreaSet
            // 
            this.btnPixelWorkingAreaSet.Location = new System.Drawing.Point(313, 130);
            this.btnPixelWorkingAreaSet.Name = "btnPixelWorkingAreaSet";
            this.btnPixelWorkingAreaSet.Size = new System.Drawing.Size(47, 23);
            this.btnPixelWorkingAreaSet.TabIndex = 140;
            this.btnPixelWorkingAreaSet.Text = "Set";
            this.btnPixelWorkingAreaSet.UseVisualStyleBackColor = true;
            this.btnPixelWorkingAreaSet.Click += new System.EventHandler(this.btnPixelWorkingAreaSet_Click);
            // 
            // btnIsInPixelWorkingAreaGet
            // 
            this.btnIsInPixelWorkingAreaGet.Location = new System.Drawing.Point(313, 15);
            this.btnIsInPixelWorkingAreaGet.Name = "btnIsInPixelWorkingAreaGet";
            this.btnIsInPixelWorkingAreaGet.Size = new System.Drawing.Size(47, 23);
            this.btnIsInPixelWorkingAreaGet.TabIndex = 141;
            this.btnIsInPixelWorkingAreaGet.Text = "Get";
            this.btnIsInPixelWorkingAreaGet.UseVisualStyleBackColor = true;
            this.btnIsInPixelWorkingAreaGet.Click += new System.EventHandler(this.btnIsInPixelWorkingAreaGet_Click);
            // 
            // ctrl2DVectorIsInPixelWorkingAreaPixelCoord
            // 
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord.Location = new System.Drawing.Point(152, 14);
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord.Name = "ctrl2DVectorIsInPixelWorkingAreaPixelCoord";
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord.TabIndex = 3;
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord.X = 0D;
            this.ctrl2DVectorIsInPixelWorkingAreaPixelCoord.Y = 0D;
            // 
            // btnPixelWorkingAreaValidSet
            // 
            this.btnPixelWorkingAreaValidSet.Location = new System.Drawing.Point(313, 43);
            this.btnPixelWorkingAreaValidSet.Name = "btnPixelWorkingAreaValidSet";
            this.btnPixelWorkingAreaValidSet.Size = new System.Drawing.Size(47, 23);
            this.btnPixelWorkingAreaValidSet.TabIndex = 139;
            this.btnPixelWorkingAreaValidSet.Text = "Set";
            this.btnPixelWorkingAreaValidSet.UseVisualStyleBackColor = true;
            this.btnPixelWorkingAreaValidSet.Click += new System.EventHandler(this.btnPixelWorkingAreaValidSet_Click);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(88, 120);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(59, 23);
            this.btnClearSelection.TabIndex = 142;
            this.btnClearSelection.Text = "Clear";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstExistingImageCalc);
            this.groupBox1.Controls.Add(this.btnClearSelection);
            this.groupBox1.Location = new System.Drawing.Point(407, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 147);
            this.groupBox1.TabIndex = 143;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Take Params From:";
            // 
            // frmCreateImageCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(576, 755);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbCameraParams);
            this.Controls.Add(this.gbImageWorkingArea);
            this.Controls.Add(this.btnOK);
            this.Name = "frmCreateImageCalc";
            this.Text = "Create Image Calc";
            this.gbPixelWorkingArea.ResumeLayout(false);
            this.gbPixelWorkingArea.PerformLayout();
            this.gbCameraParams.ResumeLayout(false);
            this.gbCameraParams.PerformLayout();
            this.pnlTerrains.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbImageWorkingArea.ResumeLayout(false);
            this.gbImageWorkingArea.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseImageDataFileName;
        private System.Windows.Forms.CheckBox chxLoropIsFileName;
        private System.Windows.Forms.ComboBox cmbImageType;
        private System.Windows.Forms.Label lblImageType;
        private System.Windows.Forms.CheckBox chxPixelWorkingAreaValid;
        private System.Windows.Forms.GroupBox gbPixelWorkingArea;
        private System.Windows.Forms.Label label11;
        private MCTester.Controls.Ctrl2DVector ctrl2DVectorPixelWorkingAreaUpperRight;
        private System.Windows.Forms.Label label10;
        private MCTester.Controls.Ctrl2DVector ctrl2DVectorPixelWorkingAreaLowerLeft;
        private MCTester.Controls.Ctrl2DVector ctrl2DVectorIsInPixelWorkingAreaPixelCoord;
        private System.Windows.Forms.CheckBox chxIsInPixelWorkingArea;
        private System.Windows.Forms.GroupBox gbCameraParams;
        private System.Windows.Forms.GroupBox gbImageWorkingArea;
        private System.Windows.Forms.Button btnPixelWorkingAreaValidSet;
        private System.Windows.Forms.Button btnPixelWorkingAreaSet;
        private System.Windows.Forms.Button btnIsInPixelWorkingAreaGet;
        private System.Windows.Forms.Label lblDtmMapLayer;
        private System.Windows.Forms.Button btnAddDtmMapLayer;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlGCSImageCalc;
        private System.Windows.Forms.ListBox lstExistingImageCalc;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.CtrlCameraParams ctrlCameraParams1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbTerrain;
        private System.Windows.Forms.RadioButton rbDtm;
        private System.Windows.Forms.Button btnTerrainListClear;
        private System.Windows.Forms.Button btnTerrainListRemove;
        private System.Windows.Forms.ListBox lstTerrains;
        private System.Windows.Forms.Button btnTerrainListAdd;
        private System.Windows.Forms.Panel pnlTerrains;
    }
}