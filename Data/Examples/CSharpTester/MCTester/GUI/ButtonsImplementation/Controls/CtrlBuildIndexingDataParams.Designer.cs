namespace MCTester.Controls
{
    partial class CtrlBuildIndexingDataParams
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
            this.lblFirstLowerQualityLevel = new System.Windows.Forms.Label();
            this.chxUseExisting = new System.Windows.Forms.CheckBox();
            this.cbNonDefaultIndexDirectory = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlBrowseIndexingDataDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlLayerSourceGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.checkGroupBoxClipRect = new MCTester.Controls.CheckGroupBox();
            this.ctrlSMcBoxClipRect = new MCTester.Controls.CtrlSMcBox();
            this.checkTilingScheme = new MCTester.Controls.CheckGroupBox();
            this.ctrlTilingSchemeParams1 = new MCTester.Controls.CtrlTilingSchemeParams();
            this.ntxTargetHighestResolution = new MCTester.Controls.NumericTextBox();
            this.ctrlBrowseRawDataDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlLayerTargetGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.pnlNonIndexingParams = new System.Windows.Forms.Panel();
            this.checkGroupBoxClipRect.SuspendLayout();
            this.checkTilingScheme.SuspendLayout();
            this.pnlNonIndexingParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFirstLowerQualityLevel
            // 
            this.lblFirstLowerQualityLevel.AutoSize = true;
            this.lblFirstLowerQualityLevel.Location = new System.Drawing.Point(3, 4);
            this.lblFirstLowerQualityLevel.Name = "lblFirstLowerQualityLevel";
            this.lblFirstLowerQualityLevel.Size = new System.Drawing.Size(133, 13);
            this.lblFirstLowerQualityLevel.TabIndex = 28;
            this.lblFirstLowerQualityLevel.Text = "Target Highest Resolution:";
            // 
            // chxUseExisting
            // 
            this.chxUseExisting.AutoSize = true;
            this.chxUseExisting.Location = new System.Drawing.Point(608, 40);
            this.chxUseExisting.Name = "chxUseExisting";
            this.chxUseExisting.Size = new System.Drawing.Size(84, 17);
            this.chxUseExisting.TabIndex = 34;
            this.chxUseExisting.Text = "Use Existing";
            this.chxUseExisting.UseVisualStyleBackColor = true;
            // 
            // cbNonDefaultIndexDirectory
            // 
            this.cbNonDefaultIndexDirectory.AutoSize = true;
            this.cbNonDefaultIndexDirectory.Location = new System.Drawing.Point(4, 40);
            this.cbNonDefaultIndexDirectory.Name = "cbNonDefaultIndexDirectory";
            this.cbNonDefaultIndexDirectory.Size = new System.Drawing.Size(160, 17);
            this.cbNonDefaultIndexDirectory.TabIndex = 36;
            this.cbNonDefaultIndexDirectory.Text = "Non Default Index Directory:";
            this.cbNonDefaultIndexDirectory.UseVisualStyleBackColor = true;
            this.cbNonDefaultIndexDirectory.CheckedChanged += new System.EventHandler(this.cbNonDefaultIndexDirectory_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Data Source:";
            // 
            // ctrlBrowseIndexingDataDirectory
            // 
            this.ctrlBrowseIndexingDataDirectory.AutoSize = true;
            this.ctrlBrowseIndexingDataDirectory.FileName = "";
            this.ctrlBrowseIndexingDataDirectory.Filter = "";
            this.ctrlBrowseIndexingDataDirectory.IsFolderDialog = true;
            this.ctrlBrowseIndexingDataDirectory.IsFullPath = true;
            this.ctrlBrowseIndexingDataDirectory.IsSaveFile = false;
            this.ctrlBrowseIndexingDataDirectory.LabelCaption = "";
            this.ctrlBrowseIndexingDataDirectory.Location = new System.Drawing.Point(164, 36);
            this.ctrlBrowseIndexingDataDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseIndexingDataDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseIndexingDataDirectory.MultiFilesSelect = false;
            this.ctrlBrowseIndexingDataDirectory.Name = "ctrlBrowseIndexingDataDirectory";
            this.ctrlBrowseIndexingDataDirectory.Prefix = "";
            this.ctrlBrowseIndexingDataDirectory.Size = new System.Drawing.Size(441, 24);
            this.ctrlBrowseIndexingDataDirectory.TabIndex = 19;
            // 
            // ctrlLayerSourceGridCoordinateSystem
            // 
            this.ctrlLayerSourceGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlLayerSourceGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlLayerSourceGridCoordinateSystem.GroupBoxText = "Source Grid Coordinate System";
            this.ctrlLayerSourceGridCoordinateSystem.IsEditable = false;
            this.ctrlLayerSourceGridCoordinateSystem.Location = new System.Drawing.Point(5, 31);
            this.ctrlLayerSourceGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlLayerSourceGridCoordinateSystem.Name = "ctrlLayerSourceGridCoordinateSystem";
            this.ctrlLayerSourceGridCoordinateSystem.Size = new System.Drawing.Size(336, 137);
            this.ctrlLayerSourceGridCoordinateSystem.TabIndex = 33;
            // 
            // checkGroupBoxClipRect
            // 
            this.checkGroupBoxClipRect.Checked = false;
            this.checkGroupBoxClipRect.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkGroupBoxClipRect.Controls.Add(this.ctrlSMcBoxClipRect);
            this.checkGroupBoxClipRect.Location = new System.Drawing.Point(6, 184);
            this.checkGroupBoxClipRect.Margin = new System.Windows.Forms.Padding(2);
            this.checkGroupBoxClipRect.Name = "checkGroupBoxClipRect";
            this.checkGroupBoxClipRect.Padding = new System.Windows.Forms.Padding(2);
            this.checkGroupBoxClipRect.Size = new System.Drawing.Size(344, 106);
            this.checkGroupBoxClipRect.TabIndex = 32;
            this.checkGroupBoxClipRect.TabStop = false;
            this.checkGroupBoxClipRect.Text = "Clipping Rectangle (In Target Grid Coordinate System)";
            // 
            // ctrlSMcBoxClipRect
            // 
            this.ctrlSMcBoxClipRect.GroupBoxText = "";
            this.ctrlSMcBoxClipRect.Location = new System.Drawing.Point(4, 15);
            this.ctrlSMcBoxClipRect.Name = "ctrlSMcBoxClipRect";
            this.ctrlSMcBoxClipRect.Size = new System.Drawing.Size(336, 84);
            this.ctrlSMcBoxClipRect.TabIndex = 1;
            // 
            // checkTilingScheme
            // 
            this.checkTilingScheme.Checked = false;
            this.checkTilingScheme.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkTilingScheme.Controls.Add(this.ctrlTilingSchemeParams1);
            this.checkTilingScheme.Location = new System.Drawing.Point(0, 301);
            this.checkTilingScheme.Margin = new System.Windows.Forms.Padding(2);
            this.checkTilingScheme.Name = "checkTilingScheme";
            this.checkTilingScheme.Padding = new System.Windows.Forms.Padding(2);
            this.checkTilingScheme.Size = new System.Drawing.Size(482, 157);
            this.checkTilingScheme.TabIndex = 31;
            this.checkTilingScheme.TabStop = false;
            this.checkTilingScheme.Text = "Non - Default Tiling Scheme";
            this.checkTilingScheme.CheckedChanged += new System.EventHandler(this.checkTilingScheme_CheckedChanged);
            // 
            // ctrlTilingSchemeParams1
            // 
            this.ctrlTilingSchemeParams1.Location = new System.Drawing.Point(4, 20);
            this.ctrlTilingSchemeParams1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlTilingSchemeParams1.Name = "ctrlTilingSchemeParams1";
            this.ctrlTilingSchemeParams1.Size = new System.Drawing.Size(456, 132);
            this.ctrlTilingSchemeParams1.TabIndex = 29;
            // 
            // ntxTargetHighestResolution
            // 
            this.ntxTargetHighestResolution.Location = new System.Drawing.Point(164, 1);
            this.ntxTargetHighestResolution.Name = "ntxTargetHighestResolution";
            this.ntxTargetHighestResolution.Size = new System.Drawing.Size(54, 20);
            this.ntxTargetHighestResolution.TabIndex = 27;
            this.ntxTargetHighestResolution.WordWrap = false;
            // 
            // ctrlBrowseRawDataDirectory
            // 
            this.ctrlBrowseRawDataDirectory.AutoSize = true;
            this.ctrlBrowseRawDataDirectory.FileName = "";
            this.ctrlBrowseRawDataDirectory.Filter = "";
            this.ctrlBrowseRawDataDirectory.IsFolderDialog = true;
            this.ctrlBrowseRawDataDirectory.IsFullPath = true;
            this.ctrlBrowseRawDataDirectory.IsSaveFile = false;
            this.ctrlBrowseRawDataDirectory.LabelCaption = "Data Source:                        ";
            this.ctrlBrowseRawDataDirectory.Location = new System.Drawing.Point(75, 5);
            this.ctrlBrowseRawDataDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseRawDataDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseRawDataDirectory.MultiFilesSelect = false;
            this.ctrlBrowseRawDataDirectory.Name = "ctrlBrowseRawDataDirectory";
            this.ctrlBrowseRawDataDirectory.Prefix = "";
            this.ctrlBrowseRawDataDirectory.Size = new System.Drawing.Size(617, 24);
            this.ctrlBrowseRawDataDirectory.TabIndex = 20;
            this.ctrlBrowseRawDataDirectory.FileNameChanged += new System.EventHandler(this.ctrlBrowseRawDataDirectory_FileNameChanged);
            // 
            // ctrlLayerTargetGridCoordinateSystem
            // 
            this.ctrlLayerTargetGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlLayerTargetGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlLayerTargetGridCoordinateSystem.GroupBoxText = "Target Grid Coordinate System";
            this.ctrlLayerTargetGridCoordinateSystem.IsEditable = false;
            this.ctrlLayerTargetGridCoordinateSystem.Location = new System.Drawing.Point(346, 31);
            this.ctrlLayerTargetGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlLayerTargetGridCoordinateSystem.Name = "ctrlLayerTargetGridCoordinateSystem";
            this.ctrlLayerTargetGridCoordinateSystem.Size = new System.Drawing.Size(336, 137);
            this.ctrlLayerTargetGridCoordinateSystem.TabIndex = 17;
            // 
            // pnlNonIndexingParams
            // 
            this.pnlNonIndexingParams.Controls.Add(this.lblFirstLowerQualityLevel);
            this.pnlNonIndexingParams.Controls.Add(this.ntxTargetHighestResolution);
            this.pnlNonIndexingParams.Controls.Add(this.checkTilingScheme);
            this.pnlNonIndexingParams.Controls.Add(this.checkGroupBoxClipRect);
            this.pnlNonIndexingParams.Controls.Add(this.ctrlLayerSourceGridCoordinateSystem);
            this.pnlNonIndexingParams.Controls.Add(this.ctrlLayerTargetGridCoordinateSystem);
            this.pnlNonIndexingParams.Location = new System.Drawing.Point(4, 63);
            this.pnlNonIndexingParams.Name = "pnlNonIndexingParams";
            this.pnlNonIndexingParams.Size = new System.Drawing.Size(688, 459);
            this.pnlNonIndexingParams.TabIndex = 38;
            // 
            // CtrlBuildIndexingDataParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlNonIndexingParams);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNonDefaultIndexDirectory);
            this.Controls.Add(this.ctrlBrowseIndexingDataDirectory);
            this.Controls.Add(this.chxUseExisting);
            this.Controls.Add(this.ctrlBrowseRawDataDirectory);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrlBuildIndexingDataParams";
            this.Size = new System.Drawing.Size(697, 528);
            this.checkGroupBoxClipRect.ResumeLayout(false);
            this.checkGroupBoxClipRect.PerformLayout();
            this.checkTilingScheme.ResumeLayout(false);
            this.checkTilingScheme.PerformLayout();
            this.pnlNonIndexingParams.ResumeLayout(false);
            this.pnlNonIndexingParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlLayerTargetGridCoordinateSystem;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseIndexingDataDirectory;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseRawDataDirectory;
        private System.Windows.Forms.Label lblFirstLowerQualityLevel;
        private MCTester.Controls.NumericTextBox ntxTargetHighestResolution;
        private MCTester.Controls.CtrlTilingSchemeParams ctrlTilingSchemeParams1;
        private CheckGroupBox checkTilingScheme;
        private CheckGroupBox checkGroupBoxClipRect;
        private CtrlSMcBox ctrlSMcBoxClipRect;
        private CtrlGridCoordinateSystem ctrlLayerSourceGridCoordinateSystem;
        private System.Windows.Forms.CheckBox chxUseExisting;
        private System.Windows.Forms.CheckBox cbNonDefaultIndexDirectory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlNonIndexingParams;
    }
}
