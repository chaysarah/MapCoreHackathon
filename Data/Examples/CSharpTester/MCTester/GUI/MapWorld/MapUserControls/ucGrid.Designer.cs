namespace MCTester.MapWorld.MapUserControls
{
    partial class ucGrid
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.dgvScaleStep = new System.Windows.Forms.DataGridView();
            this.No2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NextLineGapX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NextLineGapY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumOfLinesBetweenDiffTextX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumOfLinesBetweenDiffTextY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumOfLinesBetweenSameTextX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumOfLinesBetweenSameTextY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumMetricDigitsToTrunmate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AngleCaluesFormat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgvGridRegion = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridLine = new System.Windows.Forms.DataGridViewButtonColumn();
            this.GridText = new System.Windows.Forms.DataGridViewButtonColumn();
            this.GridCoordinateSystem = new System.Windows.Forms.DataGridViewButtonColumn();
            this.MaxVertexX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxVertexY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinVertexX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinVertexY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFirstScaleStepIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastScaleStepIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chxIsUsingBasicItemPropertiesOnly = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaleStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridRegion)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(913, 546);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(751, 546);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(832, 546);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // dgvScaleStep
            // 
            this.dgvScaleStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScaleStep.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No2,
            this.MaxScale,
            this.NextLineGapX,
            this.NextLineGapY,
            this.NumOfLinesBetweenDiffTextX,
            this.NumOfLinesBetweenDiffTextY,
            this.NumOfLinesBetweenSameTextX,
            this.NumOfLinesBetweenSameTextY,
            this.NumMetricDigitsToTrunmate,
            this.AngleCaluesFormat});
            this.dgvScaleStep.Location = new System.Drawing.Point(14, 283);
            this.dgvScaleStep.Name = "dgvScaleStep";
            this.dgvScaleStep.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvScaleStep.Size = new System.Drawing.Size(974, 217);
            this.dgvScaleStep.TabIndex = 15;
            this.dgvScaleStep.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvScaleStep_DataError);
            // 
            // No2
            // 
            this.No2.FillWeight = 182.1227F;
            this.No2.HeaderText = "No";
            this.No2.Name = "No2";
            this.No2.ReadOnly = true;
            this.No2.Width = 40;
            // 
            // MaxScale
            // 
            this.MaxScale.FillWeight = 71.21101F;
            this.MaxScale.HeaderText = "Max Scale";
            this.MaxScale.Name = "MaxScale";
            this.MaxScale.Width = 70;
            // 
            // NextLineGapX
            // 
            this.NextLineGapX.FillWeight = 71.21101F;
            this.NextLineGapX.HeaderText = "Next Line Gap X";
            this.NextLineGapX.Name = "NextLineGapX";
            this.NextLineGapX.Width = 70;
            // 
            // NextLineGapY
            // 
            this.NextLineGapY.FillWeight = 71.21101F;
            this.NextLineGapY.HeaderText = "Next Line Gap Y";
            this.NextLineGapY.Name = "NextLineGapY";
            this.NextLineGapY.Width = 70;
            // 
            // NumOfLinesBetweenDiffTextX
            // 
            this.NumOfLinesBetweenDiffTextX.FillWeight = 71.21101F;
            this.NumOfLinesBetweenDiffTextX.HeaderText = "Lines Between Diff Text X";
            this.NumOfLinesBetweenDiffTextX.Name = "NumOfLinesBetweenDiffTextX";
            // 
            // NumOfLinesBetweenDiffTextY
            // 
            this.NumOfLinesBetweenDiffTextY.FillWeight = 71.21101F;
            this.NumOfLinesBetweenDiffTextY.HeaderText = "Lines Between Diff Text Y";
            this.NumOfLinesBetweenDiffTextY.Name = "NumOfLinesBetweenDiffTextY";
            // 
            // NumOfLinesBetweenSameTextX
            // 
            this.NumOfLinesBetweenSameTextX.FillWeight = 71.21101F;
            this.NumOfLinesBetweenSameTextX.HeaderText = "Lines Between Same Text X";
            this.NumOfLinesBetweenSameTextX.Name = "NumOfLinesBetweenSameTextX";
            // 
            // NumOfLinesBetweenSameTextY
            // 
            this.NumOfLinesBetweenSameTextY.FillWeight = 71.21101F;
            this.NumOfLinesBetweenSameTextY.HeaderText = "Lines Between Same Text Y";
            this.NumOfLinesBetweenSameTextY.Name = "NumOfLinesBetweenSameTextY";
            // 
            // NumMetricDigitsToTrunmate
            // 
            this.NumMetricDigitsToTrunmate.FillWeight = 71.21101F;
            this.NumMetricDigitsToTrunmate.HeaderText = "Num Metric Digits To Trunmate";
            this.NumMetricDigitsToTrunmate.Name = "NumMetricDigitsToTrunmate";
            // 
            // AngleCaluesFormat
            // 
            this.AngleCaluesFormat.FillWeight = 145.1435F;
            this.AngleCaluesFormat.HeaderText = "Angle Calues Format";
            this.AngleCaluesFormat.Items.AddRange(new object[] {
            "_EAF_DECIMAL_DEG",
            "_EAF_DEG_MIN_SEC",
            "_EAF_DEG_MIN"});
            this.AngleCaluesFormat.Name = "AngleCaluesFormat";
            this.AngleCaluesFormat.Width = 151;
            // 
            // dgvGridRegion
            // 
            this.dgvGridRegion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGridRegion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.GridLine,
            this.GridText,
            this.GridCoordinateSystem,
            this.MaxVertexX,
            this.MaxVertexY,
            this.MinVertexX,
            this.MinVertexY,
            this.colFirstScaleStepIndex,
            this.colLastScaleStepIndex});
            this.dgvGridRegion.Location = new System.Drawing.Point(14, 17);
            this.dgvGridRegion.Name = "dgvGridRegion";
            this.dgvGridRegion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvGridRegion.Size = new System.Drawing.Size(974, 226);
            this.dgvGridRegion.TabIndex = 16;
            this.dgvGridRegion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGridRegion_CellContentClick);
            this.dgvGridRegion.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvGridRegion_DataError);
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 40;
            // 
            // GridLine
            // 
            this.GridLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GridLine.FillWeight = 82.85955F;
            this.GridLine.HeaderText = "Grid Line";
            this.GridLine.Name = "GridLine";
            this.GridLine.Text = "Null";
            this.GridLine.Width = 90;
            // 
            // GridText
            // 
            this.GridText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GridText.FillWeight = 110.2803F;
            this.GridText.HeaderText = "GridText";
            this.GridText.Name = "GridText";
            this.GridText.Text = "Null";
            this.GridText.Width = 90;
            // 
            // GridCoordinateSystem
            // 
            this.GridCoordinateSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GridCoordinateSystem.FillWeight = 15.31664F;
            this.GridCoordinateSystem.HeaderText = "Grid Coordinate System";
            this.GridCoordinateSystem.Name = "GridCoordinateSystem";
            this.GridCoordinateSystem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GridCoordinateSystem.Width = 150;
            // 
            // MaxVertexX
            // 
            this.MaxVertexX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MaxVertexX.FillWeight = 14.77791F;
            this.MaxVertexX.HeaderText = "Max Vertex X";
            this.MaxVertexX.Name = "MaxVertexX";
            // 
            // MaxVertexY
            // 
            this.MaxVertexY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MaxVertexY.FillWeight = 19.64121F;
            this.MaxVertexY.HeaderText = "Max Vertex Y";
            this.MaxVertexY.Name = "MaxVertexY";
            // 
            // MinVertexX
            // 
            this.MinVertexX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MinVertexX.FillWeight = 34.89305F;
            this.MinVertexX.HeaderText = "Min Vertex X";
            this.MinVertexX.Name = "MinVertexX";
            // 
            // MinVertexY
            // 
            this.MinVertexY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MinVertexY.FillWeight = 46.59824F;
            this.MinVertexY.HeaderText = "Min Vertex Y";
            this.MinVertexY.Name = "MinVertexY";
            // 
            // colFirstScaleStepIndex
            // 
            this.colFirstScaleStepIndex.HeaderText = "First Scale Step Index";
            this.colFirstScaleStepIndex.Name = "colFirstScaleStepIndex";
            // 
            // colLastScaleStepIndex
            // 
            this.colLastScaleStepIndex.HeaderText = "Last Scale Step Index";
            this.colLastScaleStepIndex.Name = "colLastScaleStepIndex";
            // 
            // chxIsUsingBasicItemPropertiesOnly
            // 
            this.chxIsUsingBasicItemPropertiesOnly.AutoSize = true;
            this.chxIsUsingBasicItemPropertiesOnly.Enabled = false;
            this.chxIsUsingBasicItemPropertiesOnly.Location = new System.Drawing.Point(14, 515);
            this.chxIsUsingBasicItemPropertiesOnly.Name = "chxIsUsingBasicItemPropertiesOnly";
            this.chxIsUsingBasicItemPropertiesOnly.Size = new System.Drawing.Size(190, 17);
            this.chxIsUsingBasicItemPropertiesOnly.TabIndex = 17;
            this.chxIsUsingBasicItemPropertiesOnly.Text = "Is Using Basic Item Properties Only";
            this.chxIsUsingBasicItemPropertiesOnly.UseVisualStyleBackColor = true;
            // 
            // ucGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.chxIsUsingBasicItemPropertiesOnly);
            this.Controls.Add(this.dgvGridRegion);
            this.Controls.Add(this.dgvScaleStep);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Name = "ucGrid";
            this.Size = new System.Drawing.Size(999, 572);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaleStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGridRegion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.DataGridView dgvScaleStep;
        private System.Windows.Forms.DataGridView dgvGridRegion;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewButtonColumn GridLine;
        private System.Windows.Forms.DataGridViewButtonColumn GridText;
        private System.Windows.Forms.DataGridViewButtonColumn GridCoordinateSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxVertexX;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxVertexY;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinVertexX;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinVertexY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstScaleStepIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastScaleStepIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn No2;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn NextLineGapX;
        private System.Windows.Forms.DataGridViewTextBoxColumn NextLineGapY;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumOfLinesBetweenDiffTextX;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumOfLinesBetweenDiffTextY;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumOfLinesBetweenSameTextX;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumOfLinesBetweenSameTextY;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumMetricDigitsToTrunmate;
        private System.Windows.Forms.DataGridViewComboBoxColumn AngleCaluesFormat;
        private System.Windows.Forms.CheckBox chxIsUsingBasicItemPropertiesOnly;
    }
}
