namespace MCTester.MapWorld.MapUserControls
{
    partial class NewMapForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstTerrains = new System.Windows.Forms.ListBox();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.btnRemoveTerrain = new System.Windows.Forms.Button();
            this.btnAddTerrain = new System.Windows.Forms.Button();
            this.btnAddDTMLayers = new System.Windows.Forms.Button();
            this.gbWorldBoundingBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ctrl3DMaxBoundingBox = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DMinBoundingBox = new MCTester.Controls.Ctrl3DVector();
            this.chxIsOpenMapWithoutWaitAllLayersInit = new System.Windows.Forms.CheckBox();
            this.createDataMVCtrl = new MCTester.Controls.CreateDataMVControl();
            this.ctrlCheckGroupBoxSectionMap = new MCTester.Controls.CtrlCheckGroupBox();
            this.ctrlSectionMapPoints = new MCTester.Controls.CtrlPointsGrid();
            this.cbIsCalculateSectionHeightPoints = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSectionMapLine = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlCameraPosition = new MCTester.Controls.Ctrl3DVector();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbWorldBoundingBox.SuspendLayout();
            this.ctrlCheckGroupBoxSectionMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(628, 714);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(381, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Camera Position:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(378, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 176);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Terrains";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstTerrains);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnClearSelection);
            this.splitContainer1.Panel2.Controls.Add(this.btnRemoveTerrain);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddTerrain);
            this.splitContainer1.Size = new System.Drawing.Size(318, 157);
            this.splitContainer1.SplitterDistance = 243;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstTerrains
            // 
            this.lstTerrains.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstTerrains.FormattingEnabled = true;
            this.lstTerrains.HorizontalScrollbar = true;
            this.lstTerrains.Location = new System.Drawing.Point(0, 0);
            this.lstTerrains.Name = "lstTerrains";
            this.lstTerrains.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstTerrains.Size = new System.Drawing.Size(243, 157);
            this.lstTerrains.TabIndex = 10;
            this.lstTerrains.SelectedIndexChanged += new System.EventHandler(this.lstTerrains_SelectedIndexChanged);
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearSelection.Location = new System.Drawing.Point(0, 128);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(71, 29);
            this.btnClearSelection.TabIndex = 2;
            this.btnClearSelection.Text = "Clear";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // btnRemoveTerrain
            // 
            this.btnRemoveTerrain.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRemoveTerrain.Location = new System.Drawing.Point(0, 29);
            this.btnRemoveTerrain.Name = "btnRemoveTerrain";
            this.btnRemoveTerrain.Size = new System.Drawing.Size(71, 29);
            this.btnRemoveTerrain.TabIndex = 1;
            this.btnRemoveTerrain.Text = "Remove";
            this.btnRemoveTerrain.UseVisualStyleBackColor = true;
            this.btnRemoveTerrain.Click += new System.EventHandler(this.btnRemoveTerrain_Click);
            // 
            // btnAddTerrain
            // 
            this.btnAddTerrain.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddTerrain.Location = new System.Drawing.Point(0, 0);
            this.btnAddTerrain.Name = "btnAddTerrain";
            this.btnAddTerrain.Size = new System.Drawing.Size(71, 29);
            this.btnAddTerrain.TabIndex = 0;
            this.btnAddTerrain.Text = "Add";
            this.btnAddTerrain.UseVisualStyleBackColor = true;
            this.btnAddTerrain.Click += new System.EventHandler(this.btnAddTerrain_Click);
            // 
            // btnAddDTMLayers
            // 
            this.btnAddDTMLayers.Location = new System.Drawing.Point(384, 314);
            this.btnAddDTMLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddDTMLayers.Name = "btnAddDTMLayers";
            this.btnAddDTMLayers.Size = new System.Drawing.Size(196, 23);
            this.btnAddDTMLayers.TabIndex = 72;
            this.btnAddDTMLayers.Text = "Add Query Secondary Dtm Layers";
            this.btnAddDTMLayers.UseVisualStyleBackColor = true;
            this.btnAddDTMLayers.Click += new System.EventHandler(this.btnAddDTMLayers_Click);
            // 
            // gbWorldBoundingBox
            // 
            this.gbWorldBoundingBox.Controls.Add(this.label3);
            this.gbWorldBoundingBox.Controls.Add(this.label5);
            this.gbWorldBoundingBox.Controls.Add(this.ctrl3DMaxBoundingBox);
            this.gbWorldBoundingBox.Controls.Add(this.ctrl3DMinBoundingBox);
            this.gbWorldBoundingBox.Location = new System.Drawing.Point(378, 186);
            this.gbWorldBoundingBox.Name = "gbWorldBoundingBox";
            this.gbWorldBoundingBox.Size = new System.Drawing.Size(324, 79);
            this.gbWorldBoundingBox.TabIndex = 74;
            this.gbWorldBoundingBox.TabStop = false;
            this.gbWorldBoundingBox.Text = "World Bounding Box:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Min Point";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Max Point";
            // 
            // ctrl3DMaxBoundingBox
            // 
            this.ctrl3DMaxBoundingBox.IsReadOnly = false;
            this.ctrl3DMaxBoundingBox.Location = new System.Drawing.Point(75, 47);
            this.ctrl3DMaxBoundingBox.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DMaxBoundingBox.Name = "ctrl3DMaxBoundingBox";
            this.ctrl3DMaxBoundingBox.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DMaxBoundingBox.TabIndex = 5;
            this.ctrl3DMaxBoundingBox.X = 0D;
            this.ctrl3DMaxBoundingBox.Y = 0D;
            this.ctrl3DMaxBoundingBox.Z = 0D;
            // 
            // ctrl3DMinBoundingBox
            // 
            this.ctrl3DMinBoundingBox.IsReadOnly = false;
            this.ctrl3DMinBoundingBox.Location = new System.Drawing.Point(75, 19);
            this.ctrl3DMinBoundingBox.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DMinBoundingBox.Name = "ctrl3DMinBoundingBox";
            this.ctrl3DMinBoundingBox.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DMinBoundingBox.TabIndex = 6;
            this.ctrl3DMinBoundingBox.X = 0D;
            this.ctrl3DMinBoundingBox.Y = 0D;
            this.ctrl3DMinBoundingBox.Z = 0D;
            // 
            // chxIsOpenMapWithoutWaitAllLayersInit
            // 
            this.chxIsOpenMapWithoutWaitAllLayersInit.AutoSize = true;
            this.chxIsOpenMapWithoutWaitAllLayersInit.Location = new System.Drawing.Point(373, 682);
            this.chxIsOpenMapWithoutWaitAllLayersInit.Name = "chxIsOpenMapWithoutWaitAllLayersInit";
            this.chxIsOpenMapWithoutWaitAllLayersInit.Size = new System.Drawing.Size(299, 17);
            this.chxIsOpenMapWithoutWaitAllLayersInit.TabIndex = 75;
            this.chxIsOpenMapWithoutWaitAllLayersInit.Text = "Open viewport without waiting until all layers are initialized ";
            this.chxIsOpenMapWithoutWaitAllLayersInit.UseVisualStyleBackColor = true;
            // 
            // createDataMVCtrl
            // 
            this.createDataMVCtrl.AutoScroll = true;
            this.createDataMVCtrl.Location = new System.Drawing.Point(2, 5);
            this.createDataMVCtrl.Margin = new System.Windows.Forms.Padding(5);
            this.createDataMVCtrl.Name = "createDataMVCtrl";
            this.createDataMVCtrl.Size = new System.Drawing.Size(370, 710);
            this.createDataMVCtrl.TabIndex = 0;
            // 
            // ctrlCheckGroupBoxSectionMap
            // 
            this.ctrlCheckGroupBoxSectionMap.Checked = false;
            this.ctrlCheckGroupBoxSectionMap.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.ctrlCheckGroupBoxSectionMap.Controls.Add(this.ctrlSectionMapPoints);
            this.ctrlCheckGroupBoxSectionMap.Controls.Add(this.cbIsCalculateSectionHeightPoints);
            this.ctrlCheckGroupBoxSectionMap.Controls.Add(this.label2);
            this.ctrlCheckGroupBoxSectionMap.Controls.Add(this.btnSectionMapLine);
            this.ctrlCheckGroupBoxSectionMap.Controls.Add(this.label1);
            this.ctrlCheckGroupBoxSectionMap.Location = new System.Drawing.Point(375, 354);
            this.ctrlCheckGroupBoxSectionMap.Name = "ctrlCheckGroupBoxSectionMap";
            this.ctrlCheckGroupBoxSectionMap.Size = new System.Drawing.Size(327, 316);
            this.ctrlCheckGroupBoxSectionMap.TabIndex = 20;
            this.ctrlCheckGroupBoxSectionMap.TabStop = false;
            this.ctrlCheckGroupBoxSectionMap.Text = "Section Map Viewport";
            this.ctrlCheckGroupBoxSectionMap.CheckedChanged += new System.EventHandler(this.ctrlCheckGroupBoxSectionMap_CheckedChanged);
            // 
            // ctrlSectionMapPoints
            // 
            this.ctrlSectionMapPoints.Location = new System.Drawing.Point(6, 41);
            this.ctrlSectionMapPoints.Name = "ctrlSectionMapPoints";
            this.ctrlSectionMapPoints.Size = new System.Drawing.Size(314, 192);
            this.ctrlSectionMapPoints.TabIndex = 16;
            // 
            // cbIsCalculateSectionHeightPoints
            // 
            this.cbIsCalculateSectionHeightPoints.AutoSize = true;
            this.cbIsCalculateSectionHeightPoints.Location = new System.Drawing.Point(9, 287);
            this.cbIsCalculateSectionHeightPoints.Name = "cbIsCalculateSectionHeightPoints";
            this.cbIsCalculateSectionHeightPoints.Size = new System.Drawing.Size(186, 17);
            this.cbIsCalculateSectionHeightPoints.TabIndex = 15;
            this.cbIsCalculateSectionHeightPoints.Text = "Is Calculate Section Height Points";
            this.cbIsCalculateSectionHeightPoints.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Sample route points:";
            // 
            // btnSectionMapLine
            // 
            this.btnSectionMapLine.Location = new System.Drawing.Point(270, 246);
            this.btnSectionMapLine.Name = "btnSectionMapLine";
            this.btnSectionMapLine.Size = new System.Drawing.Size(51, 23);
            this.btnSectionMapLine.TabIndex = 12;
            this.btnSectionMapLine.Text = "Line...";
            this.btnSectionMapLine.UseVisualStyleBackColor = true;
            this.btnSectionMapLine.Click += new System.EventHandler(this.btnSectionMapLine_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Section Route Points:";
            // 
            // ctrlCameraPosition
            // 
            this.ctrlCameraPosition.IsReadOnly = false;
            this.ctrlCameraPosition.Location = new System.Drawing.Point(467, 270);
            this.ctrlCameraPosition.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlCameraPosition.Name = "ctrlCameraPosition";
            this.ctrlCameraPosition.Size = new System.Drawing.Size(232, 26);
            this.ctrlCameraPosition.TabIndex = 19;
            this.ctrlCameraPosition.X = 0D;
            this.ctrlCameraPosition.Y = 0D;
            this.ctrlCameraPosition.Z = 0D;
            // 
            // NewMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(706, 744);
            this.Controls.Add(this.chxIsOpenMapWithoutWaitAllLayersInit);
            this.Controls.Add(this.gbWorldBoundingBox);
            this.Controls.Add(this.btnAddDTMLayers);
            this.Controls.Add(this.createDataMVCtrl);
            this.Controls.Add(this.ctrlCheckGroupBoxSectionMap);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlCameraPosition);
            this.Controls.Add(this.btnOK);
            this.Name = "NewMapForm";
            this.Text = "NewMapForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewMapForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbWorldBoundingBox.ResumeLayout(false);
            this.gbWorldBoundingBox.PerformLayout();
            this.ctrlCheckGroupBoxSectionMap.ResumeLayout(false);
            this.ctrlCheckGroupBoxSectionMap.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private MCTester.Controls.Ctrl3DVector ctrlCameraPosition;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstTerrains;
        private System.Windows.Forms.Button btnRemoveTerrain;
        private System.Windows.Forms.Button btnAddTerrain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSectionMapLine;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.CreateDataMVControl createDataMVCtrl;
        private System.Windows.Forms.Button btnClearSelection;
        private MCTester.Controls.CtrlCheckGroupBox ctrlCheckGroupBoxSectionMap;
        private System.Windows.Forms.CheckBox cbIsCalculateSectionHeightPoints;
        private Controls.CtrlPointsGrid ctrlSectionMapPoints;
        private System.Windows.Forms.Button btnAddDTMLayers;
        private System.Windows.Forms.GroupBox gbWorldBoundingBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private Controls.Ctrl3DVector ctrl3DMaxBoundingBox;
        private Controls.Ctrl3DVector ctrl3DMinBoundingBox;
        private System.Windows.Forms.CheckBox chxIsOpenMapWithoutWaitAllLayersInit;
    }
}