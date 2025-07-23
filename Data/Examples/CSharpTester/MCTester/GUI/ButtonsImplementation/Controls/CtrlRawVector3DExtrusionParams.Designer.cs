namespace MCTester.Controls
{
    partial class CtrlRawVector3DExtrusionParams
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cgbUseBuiltIndexingData = new MCTester.Controls.CheckGroupBox();
            this.ctrlBrowseIndexingDataDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.cbNonDefaultIndexDirectory = new System.Windows.Forms.CheckBox();
            this.gbOtherParams = new System.Windows.Forms.GroupBox();
            this.ctrlSourceGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.ctrlTargetGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.checkGroupBoxClipRect = new MCTester.Controls.CheckGroupBox();
            this.ctrlSMcBox1 = new MCTester.Controls.CtrlSMcBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbObjectIDColumn = new System.Windows.Forms.ComboBox();
            this.cmbRoofTextureIndexColumn = new System.Windows.Forms.ComboBox();
            this.cmbSideTextureIndexColumn = new System.Windows.Forms.ComboBox();
            this.cmbHeightColumn = new System.Windows.Forms.ComboBox();
            this.ctrlExtrusionTextureArray1 = new MCTester.Controls.CtrlExtrusionTextureArray();
            this.btnRoofDefaultTexture = new System.Windows.Forms.Button();
            this.btnSideDefaultTexture = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.cgbUseBuiltIndexingData.SuspendLayout();
            this.gbOtherParams.SuspendLayout();
            this.checkGroupBoxClipRect.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cgbUseBuiltIndexingData);
            this.groupBox1.Controls.Add(this.gbOtherParams);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(717, 500);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Raw Vector 3D Extrusion Map Layer Parameters";
            // 
            // cgbUseBuiltIndexingData
            // 
            this.cgbUseBuiltIndexingData.Checked = false;
            this.cgbUseBuiltIndexingData.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.cgbUseBuiltIndexingData.Controls.Add(this.ctrlBrowseIndexingDataDirectory);
            this.cgbUseBuiltIndexingData.Controls.Add(this.cbNonDefaultIndexDirectory);
            this.cgbUseBuiltIndexingData.Location = new System.Drawing.Point(6, 18);
            this.cgbUseBuiltIndexingData.Name = "cgbUseBuiltIndexingData";
            this.cgbUseBuiltIndexingData.Size = new System.Drawing.Size(700, 42);
            this.cgbUseBuiltIndexingData.TabIndex = 84;
            this.cgbUseBuiltIndexingData.TabStop = false;
            this.cgbUseBuiltIndexingData.Text = "Use Built Indexing Data";
            this.cgbUseBuiltIndexingData.CheckedChanged += new System.EventHandler(this.cgbUseBuiltIndexingData_CheckedChanged);
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
            this.ctrlBrowseIndexingDataDirectory.Location = new System.Drawing.Point(167, 16);
            this.ctrlBrowseIndexingDataDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseIndexingDataDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseIndexingDataDirectory.MultiFilesSelect = false;
            this.ctrlBrowseIndexingDataDirectory.Name = "ctrlBrowseIndexingDataDirectory";
            this.ctrlBrowseIndexingDataDirectory.Prefix = "";
            this.ctrlBrowseIndexingDataDirectory.Size = new System.Drawing.Size(526, 24);
            this.ctrlBrowseIndexingDataDirectory.TabIndex = 19;
            // 
            // cbNonDefaultIndexDirectory
            // 
            this.cbNonDefaultIndexDirectory.AutoSize = true;
            this.cbNonDefaultIndexDirectory.Location = new System.Drawing.Point(8, 20);
            this.cbNonDefaultIndexDirectory.Name = "cbNonDefaultIndexDirectory";
            this.cbNonDefaultIndexDirectory.Size = new System.Drawing.Size(160, 17);
            this.cbNonDefaultIndexDirectory.TabIndex = 81;
            this.cbNonDefaultIndexDirectory.Text = "Non Default Index Directory:";
            this.cbNonDefaultIndexDirectory.UseVisualStyleBackColor = true;
            this.cbNonDefaultIndexDirectory.CheckedChanged += new System.EventHandler(this.cbNonDefaultIndexDirectory_CheckedChanged);
            // 
            // gbOtherParams
            // 
            this.gbOtherParams.Controls.Add(this.ctrlSourceGridCoordinateSystem);
            this.gbOtherParams.Controls.Add(this.ctrlTargetGridCoordinateSystem);
            this.gbOtherParams.Controls.Add(this.checkGroupBoxClipRect);
            this.gbOtherParams.Location = new System.Drawing.Point(6, 64);
            this.gbOtherParams.Name = "gbOtherParams";
            this.gbOtherParams.Size = new System.Drawing.Size(693, 266);
            this.gbOtherParams.TabIndex = 80;
            this.gbOtherParams.TabStop = false;
            this.gbOtherParams.Text = "Other Params";
            // 
            // ctrlSourceGridCoordinateSystem
            // 
            this.ctrlSourceGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlSourceGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlSourceGridCoordinateSystem.GroupBoxText = "Source Grid Coordinate System";
            this.ctrlSourceGridCoordinateSystem.IsEditable = false;
            this.ctrlSourceGridCoordinateSystem.Location = new System.Drawing.Point(6, 17);
            this.ctrlSourceGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(8);
            this.ctrlSourceGridCoordinateSystem.Name = "ctrlSourceGridCoordinateSystem";
            this.ctrlSourceGridCoordinateSystem.Size = new System.Drawing.Size(330, 133);
            this.ctrlSourceGridCoordinateSystem.TabIndex = 28;
            // 
            // ctrlTargetGridCoordinateSystem
            // 
            this.ctrlTargetGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlTargetGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlTargetGridCoordinateSystem.GroupBoxText = "Target Grid Coordinate System";
            this.ctrlTargetGridCoordinateSystem.IsEditable = false;
            this.ctrlTargetGridCoordinateSystem.Location = new System.Drawing.Point(350, 17);
            this.ctrlTargetGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(8);
            this.ctrlTargetGridCoordinateSystem.Name = "ctrlTargetGridCoordinateSystem";
            this.ctrlTargetGridCoordinateSystem.Size = new System.Drawing.Size(330, 133);
            this.ctrlTargetGridCoordinateSystem.TabIndex = 29;
            // 
            // checkGroupBoxClipRect
            // 
            this.checkGroupBoxClipRect.Controls.Add(this.ctrlSMcBox1);
            this.checkGroupBoxClipRect.Location = new System.Drawing.Point(6, 155);
            this.checkGroupBoxClipRect.Margin = new System.Windows.Forms.Padding(2);
            this.checkGroupBoxClipRect.Name = "checkGroupBoxClipRect";
            this.checkGroupBoxClipRect.Padding = new System.Windows.Forms.Padding(2);
            this.checkGroupBoxClipRect.Size = new System.Drawing.Size(349, 105);
            this.checkGroupBoxClipRect.TabIndex = 42;
            this.checkGroupBoxClipRect.TabStop = false;
            this.checkGroupBoxClipRect.Text = "Clip Rect (In Target Grid Coordinate System)";
            // 
            // ctrlSMcBox1
            // 
            this.ctrlSMcBox1.GroupBoxText = "";
            this.ctrlSMcBox1.Location = new System.Drawing.Point(5, 18);
            this.ctrlSMcBox1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ctrlSMcBox1.Name = "ctrlSMcBox1";
            this.ctrlSMcBox1.Size = new System.Drawing.Size(336, 81);
            this.ctrlSMcBox1.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbObjectIDColumn);
            this.groupBox2.Controls.Add(this.cmbRoofTextureIndexColumn);
            this.groupBox2.Controls.Add(this.cmbSideTextureIndexColumn);
            this.groupBox2.Controls.Add(this.cmbHeightColumn);
            this.groupBox2.Controls.Add(this.ctrlExtrusionTextureArray1);
            this.groupBox2.Controls.Add(this.btnRoofDefaultTexture);
            this.groupBox2.Controls.Add(this.btnSideDefaultTexture);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(6, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(695, 161);
            this.groupBox2.TabIndex = 79;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Graphical Params";
            // 
            // cmbObjectIDColumn
            // 
            this.cmbObjectIDColumn.FormattingEnabled = true;
            this.cmbObjectIDColumn.Location = new System.Drawing.Point(530, 45);
            this.cmbObjectIDColumn.Margin = new System.Windows.Forms.Padding(1);
            this.cmbObjectIDColumn.Name = "cmbObjectIDColumn";
            this.cmbObjectIDColumn.Size = new System.Drawing.Size(138, 21);
            this.cmbObjectIDColumn.TabIndex = 90;
            // 
            // cmbRoofTextureIndexColumn
            // 
            this.cmbRoofTextureIndexColumn.FormattingEnabled = true;
            this.cmbRoofTextureIndexColumn.Location = new System.Drawing.Point(530, 71);
            this.cmbRoofTextureIndexColumn.Margin = new System.Windows.Forms.Padding(1);
            this.cmbRoofTextureIndexColumn.Name = "cmbRoofTextureIndexColumn";
            this.cmbRoofTextureIndexColumn.Size = new System.Drawing.Size(138, 21);
            this.cmbRoofTextureIndexColumn.TabIndex = 89;
            // 
            // cmbSideTextureIndexColumn
            // 
            this.cmbSideTextureIndexColumn.FormattingEnabled = true;
            this.cmbSideTextureIndexColumn.Location = new System.Drawing.Point(530, 96);
            this.cmbSideTextureIndexColumn.Margin = new System.Windows.Forms.Padding(1);
            this.cmbSideTextureIndexColumn.Name = "cmbSideTextureIndexColumn";
            this.cmbSideTextureIndexColumn.Size = new System.Drawing.Size(138, 21);
            this.cmbSideTextureIndexColumn.TabIndex = 88;
            // 
            // cmbHeightColumn
            // 
            this.cmbHeightColumn.FormattingEnabled = true;
            this.cmbHeightColumn.Location = new System.Drawing.Point(530, 18);
            this.cmbHeightColumn.Margin = new System.Windows.Forms.Padding(1);
            this.cmbHeightColumn.Name = "cmbHeightColumn";
            this.cmbHeightColumn.Size = new System.Drawing.Size(138, 21);
            this.cmbHeightColumn.TabIndex = 87;
            // 
            // ctrlExtrusionTextureArray1
            // 
            this.ctrlExtrusionTextureArray1.Location = new System.Drawing.Point(151, 21);
            this.ctrlExtrusionTextureArray1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlExtrusionTextureArray1.Name = "ctrlExtrusionTextureArray1";
            this.ctrlExtrusionTextureArray1.Size = new System.Drawing.Size(172, 134);
            this.ctrlExtrusionTextureArray1.TabIndex = 33;
            // 
            // btnRoofDefaultTexture
            // 
            this.btnRoofDefaultTexture.Location = new System.Drawing.Point(19, 21);
            this.btnRoofDefaultTexture.Margin = new System.Windows.Forms.Padding(2);
            this.btnRoofDefaultTexture.Name = "btnRoofDefaultTexture";
            this.btnRoofDefaultTexture.Size = new System.Drawing.Size(117, 22);
            this.btnRoofDefaultTexture.TabIndex = 30;
            this.btnRoofDefaultTexture.Text = "Roof Default Texture";
            this.btnRoofDefaultTexture.UseVisualStyleBackColor = true;
            this.btnRoofDefaultTexture.Click += new System.EventHandler(this.btnRoofDefaultTexture_Click);
            // 
            // btnSideDefaultTexture
            // 
            this.btnSideDefaultTexture.Location = new System.Drawing.Point(19, 47);
            this.btnSideDefaultTexture.Margin = new System.Windows.Forms.Padding(2);
            this.btnSideDefaultTexture.Name = "btnSideDefaultTexture";
            this.btnSideDefaultTexture.Size = new System.Drawing.Size(117, 22);
            this.btnSideDefaultTexture.TabIndex = 31;
            this.btnSideDefaultTexture.Text = "Side Default Texture";
            this.btnSideDefaultTexture.UseVisualStyleBackColor = true;
            this.btnSideDefaultTexture.Click += new System.EventHandler(this.btnSideDefaultTexture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Object ID Column:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(368, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Height Column:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Side Texture Index Column:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(368, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Roof Texture Index Column:";
            // 
            // CtrlRawVector3DExtrusionParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrlRawVector3DExtrusionParams";
            this.Size = new System.Drawing.Size(729, 508);
            this.groupBox1.ResumeLayout(false);
            this.cgbUseBuiltIndexingData.ResumeLayout(false);
            this.cgbUseBuiltIndexingData.PerformLayout();
            this.gbOtherParams.ResumeLayout(false);
            this.checkGroupBoxClipRect.ResumeLayout(false);
            this.checkGroupBoxClipRect.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private CheckGroupBox checkGroupBoxClipRect;
        private CtrlSMcBox ctrlSMcBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private CtrlExtrusionTextureArray ctrlExtrusionTextureArray1;
        private System.Windows.Forms.Button btnSideDefaultTexture;
        private System.Windows.Forms.Button btnRoofDefaultTexture;
        private CtrlGridCoordinateSystem ctrlTargetGridCoordinateSystem;
        private CtrlGridCoordinateSystem ctrlSourceGridCoordinateSystem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbOtherParams;
        private System.Windows.Forms.CheckBox cbNonDefaultIndexDirectory;
        private CtrlBrowseControl ctrlBrowseIndexingDataDirectory;
        private CheckGroupBox cgbUseBuiltIndexingData;
        private System.Windows.Forms.ComboBox cmbObjectIDColumn;
        private System.Windows.Forms.ComboBox cmbRoofTextureIndexColumn;
        private System.Windows.Forms.ComboBox cmbSideTextureIndexColumn;
        private System.Windows.Forms.ComboBox cmbHeightColumn;
    }
}
