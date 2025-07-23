namespace MCTester.General_Forms
{
    partial class PerformanceTestForm
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
            this.btnZoomTest = new System.Windows.Forms.Button();
            this.btnRotateTest = new System.Windows.Forms.Button();
            this.btnMoveTest = new System.Windows.Forms.Button();
            this.btnLayersVisibilityTest = new System.Windows.Forms.Button();
            this.btnOpenMapTest = new System.Windows.Forms.Button();
            this.btnClearTextBox = new System.Windows.Forms.Button();
            this.btnLoadObjectsTest = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ctrlOMGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.label1 = new System.Windows.Forms.Label();
            this.rdb3DMap = new System.Windows.Forms.RadioButton();
            this.ctrlBrowseOpenMapBaseDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlBrowseOpenMapFileName = new MCTester.Controls.CtrlBrowseControl();
            this.rdb2DMap = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvLoadObject = new System.Windows.Forms.DataGridView();
            this.colObjectFileName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colNumObjectToLoad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDrawObjLocationArea = new System.Windows.Forms.Button();
            this.ctrlBrowseAreaFile = new MCTester.Controls.CtrlBrowseControl();
            this.gbGeneralParams = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxCheckedValue = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxIterations = new MCTester.Controls.NumericTextBox();
            this.lstPerformanceResult = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadObject)).BeginInit();
            this.gbGeneralParams.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnZoomTest
            // 
            this.btnZoomTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZoomTest.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnZoomTest.Location = new System.Drawing.Point(223, 588);
            this.btnZoomTest.Name = "btnZoomTest";
            this.btnZoomTest.Size = new System.Drawing.Size(104, 40);
            this.btnZoomTest.TabIndex = 0;
            this.btnZoomTest.Text = "Zoom Test";
            this.btnZoomTest.UseVisualStyleBackColor = false;
            this.btnZoomTest.Click += new System.EventHandler(this.btnZoomTest_Click);
            // 
            // btnRotateTest
            // 
            this.btnRotateTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRotateTest.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnRotateTest.Location = new System.Drawing.Point(333, 588);
            this.btnRotateTest.Name = "btnRotateTest";
            this.btnRotateTest.Size = new System.Drawing.Size(104, 40);
            this.btnRotateTest.TabIndex = 1;
            this.btnRotateTest.Text = "Rotate Test";
            this.btnRotateTest.UseVisualStyleBackColor = false;
            this.btnRotateTest.Click += new System.EventHandler(this.btnRotateTest_Click);
            // 
            // btnMoveTest
            // 
            this.btnMoveTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoveTest.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnMoveTest.Location = new System.Drawing.Point(443, 588);
            this.btnMoveTest.Name = "btnMoveTest";
            this.btnMoveTest.Size = new System.Drawing.Size(104, 40);
            this.btnMoveTest.TabIndex = 2;
            this.btnMoveTest.Text = "Move Test";
            this.btnMoveTest.UseVisualStyleBackColor = false;
            this.btnMoveTest.Click += new System.EventHandler(this.btnMoveTest_Click);
            // 
            // btnLayersVisibilityTest
            // 
            this.btnLayersVisibilityTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLayersVisibilityTest.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnLayersVisibilityTest.Location = new System.Drawing.Point(553, 588);
            this.btnLayersVisibilityTest.Name = "btnLayersVisibilityTest";
            this.btnLayersVisibilityTest.Size = new System.Drawing.Size(104, 40);
            this.btnLayersVisibilityTest.TabIndex = 3;
            this.btnLayersVisibilityTest.Text = "Layers Visibility Test";
            this.btnLayersVisibilityTest.UseVisualStyleBackColor = false;
            this.btnLayersVisibilityTest.Click += new System.EventHandler(this.btnLayersVisibilityTest_Click);
            // 
            // btnOpenMapTest
            // 
            this.btnOpenMapTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenMapTest.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnOpenMapTest.Location = new System.Drawing.Point(3, 588);
            this.btnOpenMapTest.Name = "btnOpenMapTest";
            this.btnOpenMapTest.Size = new System.Drawing.Size(104, 40);
            this.btnOpenMapTest.TabIndex = 4;
            this.btnOpenMapTest.Text = "Open Map Test";
            this.btnOpenMapTest.UseVisualStyleBackColor = false;
            this.btnOpenMapTest.Click += new System.EventHandler(this.btnOpenMapTest_Click);
            // 
            // btnClearTextBox
            // 
            this.btnClearTextBox.Location = new System.Drawing.Point(6, 533);
            this.btnClearTextBox.Name = "btnClearTextBox";
            this.btnClearTextBox.Size = new System.Drawing.Size(316, 26);
            this.btnClearTextBox.TabIndex = 7;
            this.btnClearTextBox.Text = "Clear";
            this.btnClearTextBox.UseVisualStyleBackColor = true;
            this.btnClearTextBox.Click += new System.EventHandler(this.btnClearTextBox_Click);
            // 
            // btnLoadObjectsTest
            // 
            this.btnLoadObjectsTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadObjectsTest.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnLoadObjectsTest.Location = new System.Drawing.Point(113, 588);
            this.btnLoadObjectsTest.Name = "btnLoadObjectsTest";
            this.btnLoadObjectsTest.Size = new System.Drawing.Size(104, 40);
            this.btnLoadObjectsTest.TabIndex = 8;
            this.btnLoadObjectsTest.Text = "Load Objects Test";
            this.btnLoadObjectsTest.UseVisualStyleBackColor = false;
            this.btnLoadObjectsTest.Click += new System.EventHandler(this.btnLoadObjectsTest_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ctrlOMGridCoordinateSystem);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rdb3DMap);
            this.groupBox2.Controls.Add(this.ctrlBrowseOpenMapBaseDirectory);
            this.groupBox2.Controls.Add(this.ctrlBrowseOpenMapFileName);
            this.groupBox2.Controls.Add(this.rdb2DMap);
            this.groupBox2.Location = new System.Drawing.Point(3, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 274);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Open Map Params";
            // 
            // ctrlOMGridCoordinateSystem
            // 
            this.ctrlOMGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlOMGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlOMGridCoordinateSystem.GroupBoxText = "Grid Coordinate System";
            this.ctrlOMGridCoordinateSystem.IsEditable = false;
            this.ctrlOMGridCoordinateSystem.Location = new System.Drawing.Point(6, 117);
            this.ctrlOMGridCoordinateSystem.Name = "ctrlOMGridCoordinateSystem";
            this.ctrlOMGridCoordinateSystem.Size = new System.Drawing.Size(336, 155);
            this.ctrlOMGridCoordinateSystem.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(6, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Overlay Manager Coordinate System:";
            // 
            // rdb3DMap
            // 
            this.rdb3DMap.AutoSize = true;
            this.rdb3DMap.Location = new System.Drawing.Point(75, 21);
            this.rdb3DMap.Name = "rdb3DMap";
            this.rdb3DMap.Size = new System.Drawing.Size(63, 17);
            this.rdb3DMap.TabIndex = 19;
            this.rdb3DMap.Text = "3D Map";
            this.rdb3DMap.UseVisualStyleBackColor = true;
            // 
            // ctrlBrowseOpenMapBaseDirectory
            // 
            this.ctrlBrowseOpenMapBaseDirectory.AutoSize = true;
            this.ctrlBrowseOpenMapBaseDirectory.FileName = "";
            this.ctrlBrowseOpenMapBaseDirectory.Filter = "";
            this.ctrlBrowseOpenMapBaseDirectory.IsFolderDialog = true;
            this.ctrlBrowseOpenMapBaseDirectory.IsFullPath = true;
            this.ctrlBrowseOpenMapBaseDirectory.IsSaveFile = false;
            this.ctrlBrowseOpenMapBaseDirectory.LabelCaption = "Base Dir:    ";
            this.ctrlBrowseOpenMapBaseDirectory.Location = new System.Drawing.Point(95, 74);
            this.ctrlBrowseOpenMapBaseDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseOpenMapBaseDirectory.MultiFilesSelect = false;
            this.ctrlBrowseOpenMapBaseDirectory.Name = "ctrlBrowseOpenMapBaseDirectory";
            this.ctrlBrowseOpenMapBaseDirectory.Prefix = "";
            this.ctrlBrowseOpenMapBaseDirectory.Size = new System.Drawing.Size(388, 24);
            this.ctrlBrowseOpenMapBaseDirectory.TabIndex = 17;
            // 
            // ctrlBrowseOpenMapFileName
            // 
            this.ctrlBrowseOpenMapFileName.AutoSize = true;
            this.ctrlBrowseOpenMapFileName.FileName = "";
            this.ctrlBrowseOpenMapFileName.Filter = "";
            this.ctrlBrowseOpenMapFileName.IsFolderDialog = false;
            this.ctrlBrowseOpenMapFileName.IsFullPath = true;
            this.ctrlBrowseOpenMapFileName.IsSaveFile = false;
            this.ctrlBrowseOpenMapFileName.LabelCaption = "Terrain File:";
            this.ctrlBrowseOpenMapFileName.Location = new System.Drawing.Point(95, 44);
            this.ctrlBrowseOpenMapFileName.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseOpenMapFileName.MultiFilesSelect = false;
            this.ctrlBrowseOpenMapFileName.Name = "ctrlBrowseOpenMapFileName";
            this.ctrlBrowseOpenMapFileName.Prefix = "";
            this.ctrlBrowseOpenMapFileName.Size = new System.Drawing.Size(388, 24);
            this.ctrlBrowseOpenMapFileName.TabIndex = 16;
            // 
            // rdb2DMap
            // 
            this.rdb2DMap.AutoSize = true;
            this.rdb2DMap.Checked = true;
            this.rdb2DMap.Location = new System.Drawing.Point(6, 21);
            this.rdb2DMap.Name = "rdb2DMap";
            this.rdb2DMap.Size = new System.Drawing.Size(63, 17);
            this.rdb2DMap.TabIndex = 18;
            this.rdb2DMap.TabStop = true;
            this.rdb2DMap.Text = "2D Map";
            this.rdb2DMap.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.dgvLoadObject);
            this.groupBox3.Controls.Add(this.btnDrawObjLocationArea);
            this.groupBox3.Controls.Add(this.ctrlBrowseAreaFile);
            this.groupBox3.Location = new System.Drawing.Point(3, 370);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(489, 208);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Load Objects Params";
            // 
            // dgvLoadObject
            // 
            this.dgvLoadObject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoadObject.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colObjectFileName,
            this.colNumObjectToLoad});
            this.dgvLoadObject.Location = new System.Drawing.Point(9, 20);
            this.dgvLoadObject.Name = "dgvLoadObject";
            this.dgvLoadObject.Size = new System.Drawing.Size(474, 150);
            this.dgvLoadObject.TabIndex = 21;
            this.dgvLoadObject.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoadObject_CellClick);
            // 
            // colObjectFileName
            // 
            this.colObjectFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colObjectFileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colObjectFileName.HeaderText = "File Name";
            this.colObjectFileName.Name = "colObjectFileName";
            this.colObjectFileName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colObjectFileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colNumObjectToLoad
            // 
            this.colNumObjectToLoad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colNumObjectToLoad.HeaderText = "Amount";
            this.colNumObjectToLoad.Name = "colNumObjectToLoad";
            this.colNumObjectToLoad.Width = 68;
            // 
            // btnDrawObjLocationArea
            // 
            this.btnDrawObjLocationArea.Location = new System.Drawing.Point(382, 176);
            this.btnDrawObjLocationArea.Name = "btnDrawObjLocationArea";
            this.btnDrawObjLocationArea.Size = new System.Drawing.Size(101, 24);
            this.btnDrawObjLocationArea.TabIndex = 17;
            this.btnDrawObjLocationArea.Text = "Draw Area";
            this.btnDrawObjLocationArea.UseVisualStyleBackColor = true;
            this.btnDrawObjLocationArea.Click += new System.EventHandler(this.btnDrawObjLocationArea_Click);
            // 
            // ctrlBrowseAreaFile
            // 
            this.ctrlBrowseAreaFile.AutoSize = true;
            this.ctrlBrowseAreaFile.FileName = "";
            this.ctrlBrowseAreaFile.Filter = "";
            this.ctrlBrowseAreaFile.IsFolderDialog = false;
            this.ctrlBrowseAreaFile.IsFullPath = true;
            this.ctrlBrowseAreaFile.IsSaveFile = false;
            this.ctrlBrowseAreaFile.LabelCaption = "Area File:  ";
            this.ctrlBrowseAreaFile.Location = new System.Drawing.Point(61, 176);
            this.ctrlBrowseAreaFile.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseAreaFile.MultiFilesSelect = false;
            this.ctrlBrowseAreaFile.Name = "ctrlBrowseAreaFile";
            this.ctrlBrowseAreaFile.Prefix = "";
            this.ctrlBrowseAreaFile.Size = new System.Drawing.Size(300, 24);
            this.ctrlBrowseAreaFile.TabIndex = 16;
            // 
            // gbGeneralParams
            // 
            this.gbGeneralParams.Controls.Add(this.label3);
            this.gbGeneralParams.Controls.Add(this.ntxCheckedValue);
            this.gbGeneralParams.Controls.Add(this.label2);
            this.gbGeneralParams.Controls.Add(this.ntxIterations);
            this.gbGeneralParams.Location = new System.Drawing.Point(3, 12);
            this.gbGeneralParams.Name = "gbGeneralParams";
            this.gbGeneralParams.Size = new System.Drawing.Size(489, 72);
            this.gbGeneralParams.TabIndex = 19;
            this.gbGeneralParams.TabStop = false;
            this.gbGeneralParams.Text = "General Params";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Tested Value:";
            // 
            // ntxCheckedValue
            // 
            this.ntxCheckedValue.Location = new System.Drawing.Point(95, 45);
            this.ntxCheckedValue.Name = "ntxCheckedValue";
            this.ntxCheckedValue.Size = new System.Drawing.Size(100, 20);
            this.ntxCheckedValue.TabIndex = 17;
            this.ntxCheckedValue.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Iterations:";
            // 
            // ntxIterations
            // 
            this.ntxIterations.Location = new System.Drawing.Point(95, 19);
            this.ntxIterations.Name = "ntxIterations";
            this.ntxIterations.Size = new System.Drawing.Size(100, 20);
            this.ntxIterations.TabIndex = 15;
            this.ntxIterations.Text = "1";
            // 
            // lstPerformanceResult
            // 
            this.lstPerformanceResult.FormattingEnabled = true;
            this.lstPerformanceResult.Location = new System.Drawing.Point(6, 21);
            this.lstPerformanceResult.Name = "lstPerformanceResult";
            this.lstPerformanceResult.ScrollAlwaysVisible = true;
            this.lstPerformanceResult.Size = new System.Drawing.Size(316, 511);
            this.lstPerformanceResult.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstPerformanceResult);
            this.groupBox1.Controls.Add(this.btnClearTextBox);
            this.groupBox1.Location = new System.Drawing.Point(501, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 566);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Performance Result";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(3, 581);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(827, 3);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Terrain File:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Base Dir:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Area File: ";
            // 
            // PerformanceTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(832, 631);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbGeneralParams);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnLoadObjectsTest);
            this.Controls.Add(this.btnOpenMapTest);
            this.Controls.Add(this.btnLayersVisibilityTest);
            this.Controls.Add(this.btnMoveTest);
            this.Controls.Add(this.btnRotateTest);
            this.Controls.Add(this.btnZoomTest);
            this.Name = "PerformanceTestForm";
            this.Text = "PerformanceTestForm";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadObject)).EndInit();
            this.gbGeneralParams.ResumeLayout(false);
            this.gbGeneralParams.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnZoomTest;
        private System.Windows.Forms.Button btnRotateTest;
        private System.Windows.Forms.Button btnMoveTest;
        private System.Windows.Forms.Button btnLayersVisibilityTest;
        private System.Windows.Forms.Button btnOpenMapTest;
        private System.Windows.Forms.Button btnClearTextBox;
        private System.Windows.Forms.Button btnLoadObjectsTest;
        private System.Windows.Forms.GroupBox groupBox2;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseOpenMapBaseDirectory;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseOpenMapFileName;
        private System.Windows.Forms.RadioButton rdb3DMap;
        private System.Windows.Forms.RadioButton rdb2DMap;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDrawObjLocationArea;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseAreaFile;
        private System.Windows.Forms.GroupBox gbGeneralParams;
        private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox ntxCheckedValue;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxIterations;
        private System.Windows.Forms.ListBox lstPerformanceResult;
        private System.Windows.Forms.DataGridView dgvLoadObject;
        private System.Windows.Forms.DataGridViewButtonColumn colObjectFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumObjectToLoad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlOMGridCoordinateSystem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}