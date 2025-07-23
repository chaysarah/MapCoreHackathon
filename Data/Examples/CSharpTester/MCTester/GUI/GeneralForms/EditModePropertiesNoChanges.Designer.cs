namespace MCTester.General_Forms
{
    partial class EditModePropertiesNoChanges
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditModePropertiesNoChanges));
            this.tcEditMode = new System.Windows.Forms.TabControl();
            this.tpEditModeGeneral = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.ntxMaxRadiusWorld = new MCTester.Controls.NumericTextBox();
            this.chxIsFieldEnableMaxRadiusScreen = new System.Windows.Forms.CheckBox();
            this.ntxMaxRadiusScreen = new MCTester.Controls.NumericTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.chxIsFieldEnableMaxRadiusImage = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.ntxMaxRadiusImage = new MCTester.Controls.NumericTextBox();
            this.chxIsFieldEnableMaxRadiusWorld = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chxIsFieldEnableRotatePicture = new System.Windows.Forms.CheckBox();
            this.chxIsFieldEnablePointAndLine = new System.Windows.Forms.CheckBox();
            this.chxIsFieldEnableRectangleResize = new System.Windows.Forms.CheckBox();
            this.gbEditMode3DFunctions = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chxIsFieldEnableUtilityItems = new System.Windows.Forms.CheckBox();
            this.chxIsFieldEnableKeepScale = new System.Windows.Forms.CheckBox();
            this.chxIsFieldEnableUseLocalAxes = new System.Windows.Forms.CheckBox();
            this.ntbUtilityItemsOptionalScreenSize = new MCTester.Controls.NumericTextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.chxKeepScaleRatio = new System.Windows.Forms.CheckBox();
            this.chxLocalAxes = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chxIsFieldEnableMouseMove = new System.Windows.Forms.CheckBox();
            this.chxIsFieldEnableForceMaxPoints = new System.Windows.Forms.CheckBox();
            this.chxIsFieldEnableMaxNumItems = new System.Windows.Forms.CheckBox();
            this.ntxMaxNumOfPoints = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chxForceFinishOnMaxPoints = new System.Windows.Forms.CheckBox();
            this.cmbMouseMoveUsageForMultiPointItem = new System.Windows.Forms.ComboBox();
            this.chxRectangleResizeRelativeToCenter = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.ntxPointAndLineClickTolerance = new MCTester.Controls.NumericTextBox();
            this.ntbRotatePictureOffset = new MCTester.Controls.NumericTextBox();
            this.tpPermissions = new System.Windows.Forms.TabPage();
            this.ctrlEditModePermissions1 = new MCTester.Controls.CtrlEditModePermissions();
            this.tpUtilities = new System.Windows.Forms.TabPage();
            this.ctrlEditModeUtility1 = new MCTester.Controls.CtrlEditModeUtility();
            this.btnChangeForOneOperation = new System.Windows.Forms.Button();
            this.btnGetObjectOperationsParams = new System.Windows.Forms.Button();
            this.btnChangeForAllOperation = new System.Windows.Forms.Button();
            this.btnSetEditModeParameters = new System.Windows.Forms.Button();
            this.btnGetEditModeParameters = new System.Windows.Forms.Button();
            this.tcEditMode.SuspendLayout();
            this.tpEditModeGeneral.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbEditMode3DFunctions.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tpPermissions.SuspendLayout();
            this.tpUtilities.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcEditMode
            // 
            this.tcEditMode.Controls.Add(this.tpEditModeGeneral);
            this.tcEditMode.Controls.Add(this.tpPermissions);
            this.tcEditMode.Controls.Add(this.tpUtilities);
            this.tcEditMode.Location = new System.Drawing.Point(1, 0);
            this.tcEditMode.Name = "tcEditMode";
            this.tcEditMode.SelectedIndex = 0;
            this.tcEditMode.Size = new System.Drawing.Size(517, 531);
            this.tcEditMode.TabIndex = 53;
            // 
            // tpEditModeGeneral
            // 
            this.tpEditModeGeneral.AutoScroll = true;
            this.tpEditModeGeneral.Controls.Add(this.label1);
            this.tpEditModeGeneral.Controls.Add(this.groupBox2);
            this.tpEditModeGeneral.Controls.Add(this.label6);
            this.tpEditModeGeneral.Controls.Add(this.chxIsFieldEnableRotatePicture);
            this.tpEditModeGeneral.Controls.Add(this.chxIsFieldEnablePointAndLine);
            this.tpEditModeGeneral.Controls.Add(this.chxIsFieldEnableRectangleResize);
            this.tpEditModeGeneral.Controls.Add(this.gbEditMode3DFunctions);
            this.tpEditModeGeneral.Controls.Add(this.groupBox4);
            this.tpEditModeGeneral.Controls.Add(this.chxRectangleResizeRelativeToCenter);
            this.tpEditModeGeneral.Controls.Add(this.label17);
            this.tpEditModeGeneral.Controls.Add(this.label28);
            this.tpEditModeGeneral.Controls.Add(this.ntxPointAndLineClickTolerance);
            this.tpEditModeGeneral.Controls.Add(this.ntbRotatePictureOffset);
            this.tpEditModeGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpEditModeGeneral.Name = "tpEditModeGeneral";
            this.tpEditModeGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditModeGeneral.Size = new System.Drawing.Size(509, 505);
            this.tpEditModeGeneral.TabIndex = 0;
            this.tpEditModeGeneral.Text = "General";
            this.tpEditModeGeneral.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Rectangle resize relative to center";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.ntxMaxRadiusWorld);
            this.groupBox2.Controls.Add(this.chxIsFieldEnableMaxRadiusScreen);
            this.groupBox2.Controls.Add(this.ntxMaxRadiusScreen);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.chxIsFieldEnableMaxRadiusImage);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.ntxMaxRadiusImage);
            this.groupBox2.Controls.Add(this.chxIsFieldEnableMaxRadiusWorld);
            this.groupBox2.Location = new System.Drawing.Point(12, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 94);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Max Radius";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(51, 18);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(103, 13);
            this.label20.TabIndex = 64;
            this.label20.Text = "World coord system:";
            // 
            // ntxMaxRadiusWorld
            // 
            this.ntxMaxRadiusWorld.Location = new System.Drawing.Point(160, 15);
            this.ntxMaxRadiusWorld.Name = "ntxMaxRadiusWorld";
            this.ntxMaxRadiusWorld.Size = new System.Drawing.Size(60, 20);
            this.ntxMaxRadiusWorld.TabIndex = 63;
            this.ntxMaxRadiusWorld.Text = "0";
            // 
            // chxIsFieldEnableMaxRadiusScreen
            // 
            this.chxIsFieldEnableMaxRadiusScreen.AutoSize = true;
            this.chxIsFieldEnableMaxRadiusScreen.Location = new System.Drawing.Point(9, 44);
            this.chxIsFieldEnableMaxRadiusScreen.Name = "chxIsFieldEnableMaxRadiusScreen";
            this.chxIsFieldEnableMaxRadiusScreen.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableMaxRadiusScreen.TabIndex = 68;
            this.chxIsFieldEnableMaxRadiusScreen.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableMaxRadiusScreen.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableMaxRadiusScreen_CheckedChanged);
            // 
            // ntxMaxRadiusScreen
            // 
            this.ntxMaxRadiusScreen.Location = new System.Drawing.Point(160, 41);
            this.ntxMaxRadiusScreen.Name = "ntxMaxRadiusScreen";
            this.ntxMaxRadiusScreen.Size = new System.Drawing.Size(59, 20);
            this.ntxMaxRadiusScreen.TabIndex = 67;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(50, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(104, 13);
            this.label16.TabIndex = 66;
            this.label16.Text = "Image coord system:";
            // 
            // chxIsFieldEnableMaxRadiusImage
            // 
            this.chxIsFieldEnableMaxRadiusImage.AutoSize = true;
            this.chxIsFieldEnableMaxRadiusImage.Location = new System.Drawing.Point(9, 70);
            this.chxIsFieldEnableMaxRadiusImage.Name = "chxIsFieldEnableMaxRadiusImage";
            this.chxIsFieldEnableMaxRadiusImage.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableMaxRadiusImage.TabIndex = 64;
            this.chxIsFieldEnableMaxRadiusImage.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableMaxRadiusImage.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableMaxRadiusImage_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(51, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(109, 13);
            this.label15.TabIndex = 68;
            this.label15.Text = "Screen coord system:";
            // 
            // ntxMaxRadiusImage
            // 
            this.ntxMaxRadiusImage.Location = new System.Drawing.Point(160, 68);
            this.ntxMaxRadiusImage.Name = "ntxMaxRadiusImage";
            this.ntxMaxRadiusImage.Size = new System.Drawing.Size(59, 20);
            this.ntxMaxRadiusImage.TabIndex = 65;
            // 
            // chxIsFieldEnableMaxRadiusWorld
            // 
            this.chxIsFieldEnableMaxRadiusWorld.AutoSize = true;
            this.chxIsFieldEnableMaxRadiusWorld.Location = new System.Drawing.Point(9, 18);
            this.chxIsFieldEnableMaxRadiusWorld.Name = "chxIsFieldEnableMaxRadiusWorld";
            this.chxIsFieldEnableMaxRadiusWorld.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableMaxRadiusWorld.TabIndex = 63;
            this.chxIsFieldEnableMaxRadiusWorld.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableMaxRadiusWorld.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableMaxRadiusWorld_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "To change";
            // 
            // chxIsFieldEnableRotatePicture
            // 
            this.chxIsFieldEnableRotatePicture.AutoSize = true;
            this.chxIsFieldEnableRotatePicture.Location = new System.Drawing.Point(21, 120);
            this.chxIsFieldEnableRotatePicture.Name = "chxIsFieldEnableRotatePicture";
            this.chxIsFieldEnableRotatePicture.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableRotatePicture.TabIndex = 67;
            this.chxIsFieldEnableRotatePicture.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableRotatePicture.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableRotatePicture_CheckedChanged);
            // 
            // chxIsFieldEnablePointAndLine
            // 
            this.chxIsFieldEnablePointAndLine.AutoSize = true;
            this.chxIsFieldEnablePointAndLine.Location = new System.Drawing.Point(21, 147);
            this.chxIsFieldEnablePointAndLine.Name = "chxIsFieldEnablePointAndLine";
            this.chxIsFieldEnablePointAndLine.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnablePointAndLine.TabIndex = 66;
            this.chxIsFieldEnablePointAndLine.UseVisualStyleBackColor = true;
            this.chxIsFieldEnablePointAndLine.CheckedChanged += new System.EventHandler(this.chxIsFieldEnablePointAndLine_CheckedChanged);
            // 
            // chxIsFieldEnableRectangleResize
            // 
            this.chxIsFieldEnableRectangleResize.AutoSize = true;
            this.chxIsFieldEnableRectangleResize.Location = new System.Drawing.Point(21, 170);
            this.chxIsFieldEnableRectangleResize.Name = "chxIsFieldEnableRectangleResize";
            this.chxIsFieldEnableRectangleResize.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableRectangleResize.TabIndex = 65;
            this.chxIsFieldEnableRectangleResize.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableRectangleResize.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableRectangleResize_CheckedChanged);
            // 
            // gbEditMode3DFunctions
            // 
            this.gbEditMode3DFunctions.Controls.Add(this.label5);
            this.gbEditMode3DFunctions.Controls.Add(this.label7);
            this.gbEditMode3DFunctions.Controls.Add(this.chxIsFieldEnableUtilityItems);
            this.gbEditMode3DFunctions.Controls.Add(this.chxIsFieldEnableKeepScale);
            this.gbEditMode3DFunctions.Controls.Add(this.chxIsFieldEnableUseLocalAxes);
            this.gbEditMode3DFunctions.Controls.Add(this.ntbUtilityItemsOptionalScreenSize);
            this.gbEditMode3DFunctions.Controls.Add(this.label27);
            this.gbEditMode3DFunctions.Controls.Add(this.chxKeepScaleRatio);
            this.gbEditMode3DFunctions.Controls.Add(this.chxLocalAxes);
            this.gbEditMode3DFunctions.Location = new System.Drawing.Point(12, 298);
            this.gbEditMode3DFunctions.Name = "gbEditMode3DFunctions";
            this.gbEditMode3DFunctions.Size = new System.Drawing.Size(413, 95);
            this.gbEditMode3DFunctions.TabIndex = 50;
            this.gbEditMode3DFunctions.TabStop = false;
            this.gbEditMode3DFunctions.Text = "3D Parameters";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Keep scale ratio along different axes at editing";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 13);
            this.label7.TabIndex = 75;
            this.label7.Text = "Use local axes at editing";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // chxIsFieldEnableUtilityItems
            // 
            this.chxIsFieldEnableUtilityItems.AutoSize = true;
            this.chxIsFieldEnableUtilityItems.Location = new System.Drawing.Point(9, 65);
            this.chxIsFieldEnableUtilityItems.Name = "chxIsFieldEnableUtilityItems";
            this.chxIsFieldEnableUtilityItems.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableUtilityItems.TabIndex = 71;
            this.chxIsFieldEnableUtilityItems.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableUtilityItems.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableUtilityItems_CheckedChanged);
            // 
            // chxIsFieldEnableKeepScale
            // 
            this.chxIsFieldEnableKeepScale.AutoSize = true;
            this.chxIsFieldEnableKeepScale.Location = new System.Drawing.Point(9, 42);
            this.chxIsFieldEnableKeepScale.Name = "chxIsFieldEnableKeepScale";
            this.chxIsFieldEnableKeepScale.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableKeepScale.TabIndex = 70;
            this.chxIsFieldEnableKeepScale.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableKeepScale.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableKeepScale_CheckedChanged);
            // 
            // chxIsFieldEnableUseLocalAxes
            // 
            this.chxIsFieldEnableUseLocalAxes.AutoSize = true;
            this.chxIsFieldEnableUseLocalAxes.Location = new System.Drawing.Point(9, 19);
            this.chxIsFieldEnableUseLocalAxes.Name = "chxIsFieldEnableUseLocalAxes";
            this.chxIsFieldEnableUseLocalAxes.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableUseLocalAxes.TabIndex = 69;
            this.chxIsFieldEnableUseLocalAxes.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableUseLocalAxes.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableUseLocalAxes_CheckedChanged);
            // 
            // ntbUtilityItemsOptionalScreenSize
            // 
            this.ntbUtilityItemsOptionalScreenSize.Location = new System.Drawing.Point(223, 62);
            this.ntbUtilityItemsOptionalScreenSize.Name = "ntbUtilityItemsOptionalScreenSize";
            this.ntbUtilityItemsOptionalScreenSize.Size = new System.Drawing.Size(53, 20);
            this.ntbUtilityItemsOptionalScreenSize.TabIndex = 57;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(55, 67);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(158, 13);
            this.label27.TabIndex = 56;
            this.label27.Text = "Utility items optional screen size:";
            // 
            // chxKeepScaleRatio
            // 
            this.chxKeepScaleRatio.AutoSize = true;
            this.chxKeepScaleRatio.Location = new System.Drawing.Point(55, 41);
            this.chxKeepScaleRatio.Name = "chxKeepScaleRatio";
            this.chxKeepScaleRatio.Size = new System.Drawing.Size(15, 14);
            this.chxKeepScaleRatio.TabIndex = 53;
            this.chxKeepScaleRatio.UseVisualStyleBackColor = true;
            // 
            // chxLocalAxes
            // 
            this.chxLocalAxes.AutoSize = true;
            this.chxLocalAxes.Location = new System.Drawing.Point(55, 18);
            this.chxLocalAxes.Name = "chxLocalAxes";
            this.chxLocalAxes.Size = new System.Drawing.Size(15, 14);
            this.chxLocalAxes.TabIndex = 52;
            this.chxLocalAxes.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.chxIsFieldEnableMouseMove);
            this.groupBox4.Controls.Add(this.chxIsFieldEnableForceMaxPoints);
            this.groupBox4.Controls.Add(this.chxIsFieldEnableMaxNumItems);
            this.groupBox4.Controls.Add(this.ntxMaxNumOfPoints);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.chxForceFinishOnMaxPoints);
            this.groupBox4.Controls.Add(this.cmbMouseMoveUsageForMultiPointItem);
            this.groupBox4.Location = new System.Drawing.Point(12, 191);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(413, 102);
            this.groupBox4.TabIndex = 58;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Multi Point Item";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "Force max points";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // chxIsFieldEnableMouseMove
            // 
            this.chxIsFieldEnableMouseMove.AutoSize = true;
            this.chxIsFieldEnableMouseMove.Location = new System.Drawing.Point(9, 71);
            this.chxIsFieldEnableMouseMove.Name = "chxIsFieldEnableMouseMove";
            this.chxIsFieldEnableMouseMove.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableMouseMove.TabIndex = 70;
            this.chxIsFieldEnableMouseMove.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableMouseMove.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableMouseMove_CheckedChanged);
            // 
            // chxIsFieldEnableForceMaxPoints
            // 
            this.chxIsFieldEnableForceMaxPoints.AutoSize = true;
            this.chxIsFieldEnableForceMaxPoints.Location = new System.Drawing.Point(9, 46);
            this.chxIsFieldEnableForceMaxPoints.Name = "chxIsFieldEnableForceMaxPoints";
            this.chxIsFieldEnableForceMaxPoints.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableForceMaxPoints.TabIndex = 71;
            this.chxIsFieldEnableForceMaxPoints.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableForceMaxPoints.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableForceMaxPoints_CheckedChanged);
            // 
            // chxIsFieldEnableMaxNumItems
            // 
            this.chxIsFieldEnableMaxNumItems.AutoSize = true;
            this.chxIsFieldEnableMaxNumItems.Location = new System.Drawing.Point(9, 21);
            this.chxIsFieldEnableMaxNumItems.Name = "chxIsFieldEnableMaxNumItems";
            this.chxIsFieldEnableMaxNumItems.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableMaxNumItems.TabIndex = 72;
            this.chxIsFieldEnableMaxNumItems.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableMaxNumItems.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableMaxNumItems_CheckedChanged);
            // 
            // ntxMaxNumOfPoints
            // 
            this.ntxMaxNumOfPoints.Location = new System.Drawing.Point(169, 18);
            this.ntxMaxNumOfPoints.Name = "ntxMaxNumOfPoints";
            this.ntxMaxNumOfPoints.Size = new System.Drawing.Size(100, 20);
            this.ntxMaxNumOfPoints.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Max num of item points:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 55;
            this.label2.Text = "Mouse move usage:";
            // 
            // chxForceFinishOnMaxPoints
            // 
            this.chxForceFinishOnMaxPoints.AutoSize = true;
            this.chxForceFinishOnMaxPoints.Location = new System.Drawing.Point(54, 45);
            this.chxForceFinishOnMaxPoints.Name = "chxForceFinishOnMaxPoints";
            this.chxForceFinishOnMaxPoints.Size = new System.Drawing.Size(15, 14);
            this.chxForceFinishOnMaxPoints.TabIndex = 34;
            this.chxForceFinishOnMaxPoints.UseVisualStyleBackColor = true;
            // 
            // cmbMouseMoveUsageForMultiPointItem
            // 
            this.cmbMouseMoveUsageForMultiPointItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMouseMoveUsageForMultiPointItem.FormattingEnabled = true;
            this.cmbMouseMoveUsageForMultiPointItem.Location = new System.Drawing.Point(163, 68);
            this.cmbMouseMoveUsageForMultiPointItem.Name = "cmbMouseMoveUsageForMultiPointItem";
            this.cmbMouseMoveUsageForMultiPointItem.Size = new System.Drawing.Size(206, 21);
            this.cmbMouseMoveUsageForMultiPointItem.TabIndex = 54;
            // 
            // chxRectangleResizeRelativeToCenter
            // 
            this.chxRectangleResizeRelativeToCenter.AutoSize = true;
            this.chxRectangleResizeRelativeToCenter.Location = new System.Drawing.Point(63, 170);
            this.chxRectangleResizeRelativeToCenter.Name = "chxRectangleResizeRelativeToCenter";
            this.chxRectangleResizeRelativeToCenter.Size = new System.Drawing.Size(15, 14);
            this.chxRectangleResizeRelativeToCenter.TabIndex = 53;
            this.chxRectangleResizeRelativeToCenter.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(61, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(146, 13);
            this.label17.TabIndex = 52;
            this.label17.Text = "Point and line click tolerance:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(61, 120);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(106, 13);
            this.label28.TabIndex = 57;
            this.label28.Text = "Rotate picture offset:";
            // 
            // ntxPointAndLineClickTolerance
            // 
            this.ntxPointAndLineClickTolerance.Location = new System.Drawing.Point(213, 144);
            this.ntxPointAndLineClickTolerance.Name = "ntxPointAndLineClickTolerance";
            this.ntxPointAndLineClickTolerance.Size = new System.Drawing.Size(100, 20);
            this.ntxPointAndLineClickTolerance.TabIndex = 53;
            // 
            // ntbRotatePictureOffset
            // 
            this.ntbRotatePictureOffset.Location = new System.Drawing.Point(175, 118);
            this.ntbRotatePictureOffset.Name = "ntbRotatePictureOffset";
            this.ntbRotatePictureOffset.Size = new System.Drawing.Size(94, 20);
            this.ntbRotatePictureOffset.TabIndex = 56;
            // 
            // tpPermissions
            // 
            this.tpPermissions.AutoScroll = true;
            this.tpPermissions.Controls.Add(this.ctrlEditModePermissions1);
            this.tpPermissions.Location = new System.Drawing.Point(4, 22);
            this.tpPermissions.Name = "tpPermissions";
            this.tpPermissions.Padding = new System.Windows.Forms.Padding(3);
            this.tpPermissions.Size = new System.Drawing.Size(509, 505);
            this.tpPermissions.TabIndex = 2;
            this.tpPermissions.Text = "Permissions";
            this.tpPermissions.UseVisualStyleBackColor = true;
            // 
            // ctrlEditModePermissions1
            // 
            this.ctrlEditModePermissions1.EditMode = null;
            this.ctrlEditModePermissions1.Location = new System.Drawing.Point(6, 4);
            this.ctrlEditModePermissions1.Name = "ctrlEditModePermissions1";
            this.ctrlEditModePermissions1.ParentFormTypeName = MCTester.Controls.CtrlEditModePermissions.ParentFormType.objectoperations;
            this.ctrlEditModePermissions1.PermissionsBitArray = MapCore.DNEPermission._EEMP_NONE;
            this.ctrlEditModePermissions1.Size = new System.Drawing.Size(492, 353);
            this.ctrlEditModePermissions1.TabIndex = 3;
            // 
            // tpUtilities
            // 
            this.tpUtilities.Controls.Add(this.ctrlEditModeUtility1);
            this.tpUtilities.Location = new System.Drawing.Point(4, 22);
            this.tpUtilities.Name = "tpUtilities";
            this.tpUtilities.Padding = new System.Windows.Forms.Padding(3);
            this.tpUtilities.Size = new System.Drawing.Size(509, 505);
            this.tpUtilities.TabIndex = 4;
            this.tpUtilities.Text = "Utility Items";
            this.tpUtilities.UseVisualStyleBackColor = true;
            // 
            // ctrlEditModeUtility1
            // 
            this.ctrlEditModeUtility1.AutoScroll = true;
            this.ctrlEditModeUtility1.EditMode = null;
            this.ctrlEditModeUtility1.Location = new System.Drawing.Point(6, 3);
            this.ctrlEditModeUtility1.Name = "ctrlEditModeUtility1";
            this.ctrlEditModeUtility1.ParentFormTypeName = MCTester.Controls.CtrlEditModeUtility.ParentFormType.objectoperations;
            this.ctrlEditModeUtility1.Size = new System.Drawing.Size(512, 666);
            this.ctrlEditModeUtility1.TabIndex = 0;
            this.ctrlEditModeUtility1.UtilityLineItem = null;
            // 
            // btnChangeForOneOperation
            // 
            this.btnChangeForOneOperation.Location = new System.Drawing.Point(529, 28);
            this.btnChangeForOneOperation.Name = "btnChangeForOneOperation";
            this.btnChangeForOneOperation.Size = new System.Drawing.Size(160, 26);
            this.btnChangeForOneOperation.TabIndex = 54;
            this.btnChangeForOneOperation.Text = "Change for one operation";
            this.btnChangeForOneOperation.UseVisualStyleBackColor = true;
            this.btnChangeForOneOperation.Click += new System.EventHandler(this.btnChangeForOneOperation_Click);
            // 
            // btnGetObjectOperationsParams
            // 
            this.btnGetObjectOperationsParams.Location = new System.Drawing.Point(529, 91);
            this.btnGetObjectOperationsParams.Name = "btnGetObjectOperationsParams";
            this.btnGetObjectOperationsParams.Size = new System.Drawing.Size(160, 26);
            this.btnGetObjectOperationsParams.TabIndex = 56;
            this.btnGetObjectOperationsParams.Text = "Get current parameters";
            this.btnGetObjectOperationsParams.UseVisualStyleBackColor = true;
            this.btnGetObjectOperationsParams.Click += new System.EventHandler(this.btnGetObjectOperationsParams_Click);
            // 
            // btnChangeForAllOperation
            // 
            this.btnChangeForAllOperation.Location = new System.Drawing.Point(529, 60);
            this.btnChangeForAllOperation.Name = "btnChangeForAllOperation";
            this.btnChangeForAllOperation.Size = new System.Drawing.Size(160, 26);
            this.btnChangeForAllOperation.TabIndex = 57;
            this.btnChangeForAllOperation.Text = "Change permanently";
            this.btnChangeForAllOperation.UseVisualStyleBackColor = true;
            this.btnChangeForAllOperation.Click += new System.EventHandler(this.btnChangeForAllOperation_Click);
            // 
            // btnSetEditModeParameters
            // 
            this.btnSetEditModeParameters.Location = new System.Drawing.Point(529, 179);
            this.btnSetEditModeParameters.Name = "btnSetEditModeParameters";
            this.btnSetEditModeParameters.Size = new System.Drawing.Size(160, 26);
            this.btnSetEditModeParameters.TabIndex = 59;
            this.btnSetEditModeParameters.Text = "Set edit mode parameters";
            this.btnSetEditModeParameters.UseVisualStyleBackColor = true;
            this.btnSetEditModeParameters.Click += new System.EventHandler(this.btnSetEditModeParameters_Click);
            // 
            // btnGetEditModeParameters
            // 
            this.btnGetEditModeParameters.Location = new System.Drawing.Point(529, 147);
            this.btnGetEditModeParameters.Name = "btnGetEditModeParameters";
            this.btnGetEditModeParameters.Size = new System.Drawing.Size(160, 26);
            this.btnGetEditModeParameters.TabIndex = 58;
            this.btnGetEditModeParameters.Text = "Get edit mode parameters";
            this.btnGetEditModeParameters.UseVisualStyleBackColor = true;
            this.btnGetEditModeParameters.Click += new System.EventHandler(this.btnGetEditModeParameters_Click);
            // 
            // EditModePropertiesNoChanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(700, 545);
            this.Controls.Add(this.btnSetEditModeParameters);
            this.Controls.Add(this.btnGetEditModeParameters);
            this.Controls.Add(this.btnChangeForAllOperation);
            this.Controls.Add(this.btnGetObjectOperationsParams);
            this.Controls.Add(this.btnChangeForOneOperation);
            this.Controls.Add(this.tcEditMode);
            this.Name = "EditModePropertiesNoChanges";
            this.Text = "Object Operations Parameters";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditModePropertiesNoChanges_FormClosing);
            this.tcEditMode.ResumeLayout(false);
            this.tpEditModeGeneral.ResumeLayout(false);
            this.tpEditModeGeneral.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbEditMode3DFunctions.ResumeLayout(false);
            this.gbEditMode3DFunctions.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tpPermissions.ResumeLayout(false);
            this.tpUtilities.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcEditMode;
        private System.Windows.Forms.TabPage tpEditModeGeneral;
        private System.Windows.Forms.CheckBox chxRectangleResizeRelativeToCenter;
        private System.Windows.Forms.GroupBox groupBox4;
        private Controls.NumericTextBox ntxMaxNumOfPoints;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chxForceFinishOnMaxPoints;
        private System.Windows.Forms.ComboBox cmbMouseMoveUsageForMultiPointItem;
        private System.Windows.Forms.Label label28;
        private Controls.NumericTextBox ntbRotatePictureOffset;
        private Controls.NumericTextBox ntxPointAndLineClickTolerance;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox gbEditMode3DFunctions;
        private Controls.NumericTextBox ntbUtilityItemsOptionalScreenSize;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.CheckBox chxKeepScaleRatio;
        private System.Windows.Forms.CheckBox chxLocalAxes;
        private System.Windows.Forms.TabPage tpPermissions;
        private System.Windows.Forms.Button btnChangeForOneOperation;
        private System.Windows.Forms.Button btnGetObjectOperationsParams;
        private Controls.CtrlEditModePermissions ctrlEditModePermissions1;
        private System.Windows.Forms.TabPage tpUtilities;
        private Controls.CtrlEditModeUtility ctrlEditModeUtility1;
        private System.Windows.Forms.Button btnChangeForAllOperation;
        private System.Windows.Forms.CheckBox chxIsFieldEnableMaxRadiusScreen;
        private System.Windows.Forms.CheckBox chxIsFieldEnableRotatePicture;
        private System.Windows.Forms.CheckBox chxIsFieldEnablePointAndLine;
        private System.Windows.Forms.CheckBox chxIsFieldEnableRectangleResize;
        private System.Windows.Forms.CheckBox chxIsFieldEnableMaxRadiusImage;
        private System.Windows.Forms.CheckBox chxIsFieldEnableMaxRadiusWorld;
        private System.Windows.Forms.CheckBox chxIsFieldEnableUtilityItems;
        private System.Windows.Forms.CheckBox chxIsFieldEnableKeepScale;
        private System.Windows.Forms.CheckBox chxIsFieldEnableUseLocalAxes;
        private System.Windows.Forms.CheckBox chxIsFieldEnableMouseMove;
        private System.Windows.Forms.CheckBox chxIsFieldEnableForceMaxPoints;
        private System.Windows.Forms.CheckBox chxIsFieldEnableMaxNumItems;
        private System.Windows.Forms.Button btnSetEditModeParameters;
        private System.Windows.Forms.Button btnGetEditModeParameters;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label20;
        private Controls.NumericTextBox ntxMaxRadiusWorld;
        private Controls.NumericTextBox ntxMaxRadiusScreen;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private Controls.NumericTextBox ntxMaxRadiusImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}