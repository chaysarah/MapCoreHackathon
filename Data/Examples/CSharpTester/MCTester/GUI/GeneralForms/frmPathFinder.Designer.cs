namespace MCTester.General_Forms
{
    partial class frmPathFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPathFinder));
            this.btnPathFinderCreate = new System.Windows.Forms.Button();
            this.dgvObstaclesTables = new System.Windows.Forms.DataGridView();
            this.ObstacleTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ntxPointTolerance = new MCTester.Controls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbShortestPath = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReverseCostField = new System.Windows.Forms.TextBox();
            this.txtCostField = new System.Windows.Forms.TextBox();
            this.chxConsiderObstacles = new System.Windows.Forms.CheckBox();
            this.ctrlSamplePointShortestPathTarget = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlSamplePointShortestPathSource = new MCTester.Controls.CtrlSamplePoint();
            this.ctrl3DFindShortestPathTargetPt = new MCTester.Controls.Ctrl3DVector();
            this.label9 = new System.Windows.Forms.Label();
            this.ctrl3DFindShortestPathSourcePt = new MCTester.Controls.Ctrl3DVector();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUpdateTables = new System.Windows.Forms.Button();
            this.txtVectorData = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.dgvCostFields = new System.Windows.Forms.DataGridView();
            this.CostFields = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btrnFindShortestPath = new System.Windows.Forms.Button();
            this.txtEdgeIds = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvLocationPoints = new System.Windows.Forms.DataGridView();
            this.LocationPtX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationPtY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationPtZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvObstaclesTables)).BeginInit();
            this.gbShortestPath.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPathFinderCreate
            // 
            this.btnPathFinderCreate.Location = new System.Drawing.Point(326, 257);
            this.btnPathFinderCreate.Name = "btnPathFinderCreate";
            this.btnPathFinderCreate.Size = new System.Drawing.Size(75, 23);
            this.btnPathFinderCreate.TabIndex = 0;
            this.btnPathFinderCreate.Text = "Create";
            this.btnPathFinderCreate.UseVisualStyleBackColor = true;
            this.btnPathFinderCreate.Click += new System.EventHandler(this.btnPathFinderCreate_Click);
            // 
            // dgvObstaclesTables
            // 
            this.dgvObstaclesTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvObstaclesTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ObstacleTableName});
            this.dgvObstaclesTables.Location = new System.Drawing.Point(15, 84);
            this.dgvObstaclesTables.Name = "dgvObstaclesTables";
            this.dgvObstaclesTables.Size = new System.Drawing.Size(392, 78);
            this.dgvObstaclesTables.TabIndex = 3;
            // 
            // ObstacleTableName
            // 
            this.ObstacleTableName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ObstacleTableName.HeaderText = "Obstacle Table Name";
            this.ObstacleTableName.Name = "ObstacleTableName";
            // 
            // ntxPointTolerance
            // 
            this.ntxPointTolerance.Location = new System.Drawing.Point(103, 58);
            this.ntxPointTolerance.Name = "ntxPointTolerance";
            this.ntxPointTolerance.Size = new System.Drawing.Size(100, 20);
            this.ntxPointTolerance.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Point Tolerance:";
            // 
            // gbShortestPath
            // 
            this.gbShortestPath.Controls.Add(this.dgvLocationPoints);
            this.gbShortestPath.Controls.Add(this.label6);
            this.gbShortestPath.Controls.Add(this.txtEdgeIds);
            this.gbShortestPath.Controls.Add(this.btrnFindShortestPath);
            this.gbShortestPath.Controls.Add(this.label2);
            this.gbShortestPath.Controls.Add(this.label3);
            this.gbShortestPath.Controls.Add(this.txtReverseCostField);
            this.gbShortestPath.Controls.Add(this.txtCostField);
            this.gbShortestPath.Controls.Add(this.chxConsiderObstacles);
            this.gbShortestPath.Controls.Add(this.ctrlSamplePointShortestPathTarget);
            this.gbShortestPath.Controls.Add(this.ctrlSamplePointShortestPathSource);
            this.gbShortestPath.Controls.Add(this.ctrl3DFindShortestPathTargetPt);
            this.gbShortestPath.Controls.Add(this.label9);
            this.gbShortestPath.Controls.Add(this.ctrl3DFindShortestPathSourcePt);
            this.gbShortestPath.Controls.Add(this.label8);
            this.gbShortestPath.Enabled = false;
            this.gbShortestPath.Location = new System.Drawing.Point(15, 307);
            this.gbShortestPath.Name = "gbShortestPath";
            this.gbShortestPath.Size = new System.Drawing.Size(392, 316);
            this.gbShortestPath.TabIndex = 6;
            this.gbShortestPath.TabStop = false;
            this.gbShortestPath.Text = "Find Shortest Path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Reverse Cost Field:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "Cost Field:";
            // 
            // txtReverseCostField
            // 
            this.txtReverseCostField.Location = new System.Drawing.Point(111, 133);
            this.txtReverseCostField.Name = "txtReverseCostField";
            this.txtReverseCostField.Size = new System.Drawing.Size(100, 20);
            this.txtReverseCostField.TabIndex = 8;
            // 
            // txtCostField
            // 
            this.txtCostField.Location = new System.Drawing.Point(111, 107);
            this.txtCostField.Name = "txtCostField";
            this.txtCostField.Size = new System.Drawing.Size(100, 20);
            this.txtCostField.TabIndex = 78;
            // 
            // chxConsiderObstacles
            // 
            this.chxConsiderObstacles.AutoSize = true;
            this.chxConsiderObstacles.Location = new System.Drawing.Point(9, 84);
            this.chxConsiderObstacles.Name = "chxConsiderObstacles";
            this.chxConsiderObstacles.Size = new System.Drawing.Size(117, 17);
            this.chxConsiderObstacles.TabIndex = 77;
            this.chxConsiderObstacles.Text = "Consider Obstacles";
            this.chxConsiderObstacles.UseVisualStyleBackColor = true;
            // 
            // ctrlSamplePointShortestPathTarget
            // 
            this.ctrlSamplePointShortestPathTarget._DgvControlName = null;
            this.ctrlSamplePointShortestPathTarget._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointShortestPathTarget._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointShortestPathTarget._SampleOnePoint = true;
            this.ctrlSamplePointShortestPathTarget._UserControlName = "ctrl3DFindShortestPathTargetPt";
            this.ctrlSamplePointShortestPathTarget.Location = new System.Drawing.Point(302, 54);
            this.ctrlSamplePointShortestPathTarget.Name = "ctrlSamplePointShortestPathTarget";
            this.ctrlSamplePointShortestPathTarget.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointShortestPathTarget.Size = new System.Drawing.Size(27, 23);
            this.ctrlSamplePointShortestPathTarget.TabIndex = 76;
            this.ctrlSamplePointShortestPathTarget.Text = "...";
            this.ctrlSamplePointShortestPathTarget.UseVisualStyleBackColor = true;
            // 
            // ctrlSamplePointShortestPathSource
            // 
            this.ctrlSamplePointShortestPathSource._DgvControlName = null;
            this.ctrlSamplePointShortestPathSource._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointShortestPathSource._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointShortestPathSource._SampleOnePoint = true;
            this.ctrlSamplePointShortestPathSource._UserControlName = "ctrl3DFindShortestPathSourcePt";
            this.ctrlSamplePointShortestPathSource.Location = new System.Drawing.Point(302, 20);
            this.ctrlSamplePointShortestPathSource.Name = "ctrlSamplePointShortestPathSource";
            this.ctrlSamplePointShortestPathSource.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointShortestPathSource.Size = new System.Drawing.Size(27, 23);
            this.ctrlSamplePointShortestPathSource.TabIndex = 75;
            this.ctrlSamplePointShortestPathSource.Text = "...";
            this.ctrlSamplePointShortestPathSource.UseVisualStyleBackColor = true;
            // 
            // ctrl3DFindShortestPathTargetPt
            // 
            this.ctrl3DFindShortestPathTargetPt.Location = new System.Drawing.Point(69, 51);
            this.ctrl3DFindShortestPathTargetPt.Name = "ctrl3DFindShortestPathTargetPt";
            this.ctrl3DFindShortestPathTargetPt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DFindShortestPathTargetPt.TabIndex = 74;
            this.ctrl3DFindShortestPathTargetPt.X = 0D;
            this.ctrl3DFindShortestPathTargetPt.Y = 0D;
            this.ctrl3DFindShortestPathTargetPt.Z = 0D;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 73;
            this.label9.Text = "Target Pt:";
            // 
            // ctrl3DFindShortestPathSourcePt
            // 
            this.ctrl3DFindShortestPathSourcePt.Location = new System.Drawing.Point(69, 19);
            this.ctrl3DFindShortestPathSourcePt.Name = "ctrl3DFindShortestPathSourcePt";
            this.ctrl3DFindShortestPathSourcePt.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DFindShortestPathSourcePt.TabIndex = 72;
            this.ctrl3DFindShortestPathSourcePt.X = 0D;
            this.ctrl3DFindShortestPathSourcePt.Y = 0D;
            this.ctrl3DFindShortestPathSourcePt.Z = 0D;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 71;
            this.label8.Text = "Source Pt:";
            // 
            // btnUpdateTables
            // 
            this.btnUpdateTables.Enabled = false;
            this.btnUpdateTables.Location = new System.Drawing.Point(15, 629);
            this.btnUpdateTables.Name = "btnUpdateTables";
            this.btnUpdateTables.Size = new System.Drawing.Size(392, 23);
            this.btnUpdateTables.TabIndex = 7;
            this.btnUpdateTables.Text = "Update Tables";
            this.btnUpdateTables.UseVisualStyleBackColor = true;
            this.btnUpdateTables.Click += new System.EventHandler(this.btnUpdateTables_Click);
            // 
            // txtVectorData
            // 
            this.txtVectorData.Location = new System.Drawing.Point(85, 6);
            this.txtVectorData.Name = "txtVectorData";
            this.txtVectorData.Size = new System.Drawing.Size(322, 20);
            this.txtVectorData.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Vector Data:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Table Name:";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(85, 32);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(322, 20);
            this.txtTableName.TabIndex = 10;
            // 
            // dgvCostFields
            // 
            this.dgvCostFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCostFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CostFields});
            this.dgvCostFields.Location = new System.Drawing.Point(15, 168);
            this.dgvCostFields.Name = "dgvCostFields";
            this.dgvCostFields.Size = new System.Drawing.Size(392, 78);
            this.dgvCostFields.TabIndex = 12;
            // 
            // CostFields
            // 
            this.CostFields.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CostFields.HeaderText = "Cost Fields";
            this.CostFields.Name = "CostFields";
            // 
            // btrnFindShortestPath
            // 
            this.btrnFindShortestPath.Enabled = false;
            this.btrnFindShortestPath.Location = new System.Drawing.Point(311, 131);
            this.btrnFindShortestPath.Name = "btrnFindShortestPath";
            this.btrnFindShortestPath.Size = new System.Drawing.Size(75, 23);
            this.btrnFindShortestPath.TabIndex = 80;
            this.btrnFindShortestPath.Text = "Find";
            this.btrnFindShortestPath.UseVisualStyleBackColor = true;
            this.btrnFindShortestPath.Click += new System.EventHandler(this.btrnFindShortestPath_Click);
            // 
            // txtEdgeIds
            // 
            this.txtEdgeIds.Enabled = false;
            this.txtEdgeIds.Location = new System.Drawing.Point(64, 285);
            this.txtEdgeIds.Name = "txtEdgeIds";
            this.txtEdgeIds.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEdgeIds.Size = new System.Drawing.Size(322, 20);
            this.txtEdgeIds.TabIndex = 81;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 82;
            this.label6.Text = "Edge Ids:";
            // 
            // dgvLocationPoints
            // 
            this.dgvLocationPoints.AllowUserToAddRows = false;
            this.dgvLocationPoints.AllowUserToDeleteRows = false;
            this.dgvLocationPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocationPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocationPtX,
            this.LocationPtY,
            this.LocationPtZ});
            this.dgvLocationPoints.Location = new System.Drawing.Point(9, 178);
            this.dgvLocationPoints.Name = "dgvLocationPoints";
            this.dgvLocationPoints.ReadOnly = true;
            this.dgvLocationPoints.Size = new System.Drawing.Size(377, 96);
            this.dgvLocationPoints.TabIndex = 83;
            // 
            // LocationPtX
            // 
            this.LocationPtX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocationPtX.HeaderText = "X";
            this.LocationPtX.Name = "LocationPtX";
            this.LocationPtX.ReadOnly = true;
            // 
            // LocationPtY
            // 
            this.LocationPtY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocationPtY.HeaderText = "Y";
            this.LocationPtY.Name = "LocationPtY";
            this.LocationPtY.ReadOnly = true;
            // 
            // LocationPtZ
            // 
            this.LocationPtZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LocationPtZ.HeaderText = "Z";
            this.LocationPtZ.Name = "LocationPtZ";
            this.LocationPtZ.ReadOnly = true;
            // 
            // frmPathFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(418, 658);
            this.Controls.Add(this.dgvCostFields);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTableName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtVectorData);
            this.Controls.Add(this.btnUpdateTables);
            this.Controls.Add(this.gbShortestPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ntxPointTolerance);
            this.Controls.Add(this.dgvObstaclesTables);
            this.Controls.Add(this.btnPathFinderCreate);
            this.Name = "frmPathFinder";
            this.Text = "frmPathFinder";
            ((System.ComponentModel.ISupportInitialize)(this.dgvObstaclesTables)).EndInit();
            this.gbShortestPath.ResumeLayout(false);
            this.gbShortestPath.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPathFinderCreate;
        private System.Windows.Forms.DataGridView dgvObstaclesTables;
        private Controls.NumericTextBox ntxPointTolerance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbShortestPath;
        private System.Windows.Forms.Button btnUpdateTables;
        private Controls.CtrlSamplePoint ctrlSamplePointShortestPathTarget;
        private Controls.CtrlSamplePoint ctrlSamplePointShortestPathSource;
        private Controls.Ctrl3DVector ctrl3DFindShortestPathTargetPt;
        private System.Windows.Forms.Label label9;
        private Controls.Ctrl3DVector ctrl3DFindShortestPathSourcePt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chxConsiderObstacles;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReverseCostField;
        private System.Windows.Forms.TextBox txtCostField;
        private System.Windows.Forms.TextBox txtVectorData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ObstacleTableName;
        private System.Windows.Forms.DataGridView dgvCostFields;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostFields;
        private System.Windows.Forms.Button btrnFindShortestPath;
        private System.Windows.Forms.DataGridView dgvLocationPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationPtX;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationPtY;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationPtZ;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEdgeIds;
    }
}