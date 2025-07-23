namespace MCTester.MapWorld.MapUserControls
{
    partial class CtrlNonNativeParams
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
            this.chxFillEmptyTilesByLowerResolutionTiles = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.chxResolveOverlapConflicts = new System.Windows.Forms.CheckBox();
            this.label24 = new System.Windows.Forms.Label();
            this.chxEnhanceBorderOverlap = new System.Windows.Forms.CheckBox();
            this.btnTilingScheme1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ntbMaxScale = new MCTester.Controls.NumericTextBox();
            this.ctrlSelectTransparentColor = new MCTester.Controls.SelectColor();
            this.ntbTransparentColorPrecision = new MCTester.Controls.NumericTextBox();
            this.ctrlLayerGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chxFillEmptyTilesByLowerResolutionTiles
            // 
            this.chxFillEmptyTilesByLowerResolutionTiles.AutoSize = true;
            this.chxFillEmptyTilesByLowerResolutionTiles.Location = new System.Drawing.Point(8, 24);
            this.chxFillEmptyTilesByLowerResolutionTiles.Name = "chxFillEmptyTilesByLowerResolutionTiles";
            this.chxFillEmptyTilesByLowerResolutionTiles.Size = new System.Drawing.Size(220, 17);
            this.chxFillEmptyTilesByLowerResolutionTiles.TabIndex = 76;
            this.chxFillEmptyTilesByLowerResolutionTiles.Text = "Fill Empty Tiles By Lower Resolution Tiles";
            this.chxFillEmptyTilesByLowerResolutionTiles.UseVisualStyleBackColor = true;
            this.chxFillEmptyTilesByLowerResolutionTiles.CheckedChanged += new System.EventHandler(this.chxFillEmptyTilesByLowerResolutionTiles_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctrlSelectTransparentColor);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.ntbTransparentColorPrecision);
            this.groupBox1.Location = new System.Drawing.Point(1, 64);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(252, 45);
            this.groupBox1.TabIndex = 75;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transparent Color";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(135, 19);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 13);
            this.label27.TabIndex = 64;
            this.label27.Text = "Precision:";
            // 
            // chxResolveOverlapConflicts
            // 
            this.chxResolveOverlapConflicts.AutoSize = true;
            this.chxResolveOverlapConflicts.Location = new System.Drawing.Point(8, 47);
            this.chxResolveOverlapConflicts.Name = "chxResolveOverlapConflicts";
            this.chxResolveOverlapConflicts.Size = new System.Drawing.Size(148, 17);
            this.chxResolveOverlapConflicts.TabIndex = 74;
            this.chxResolveOverlapConflicts.Text = "Resolve Overlap Conflicts";
            this.chxResolveOverlapConflicts.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(162, 48);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(60, 13);
            this.label24.TabIndex = 73;
            this.label24.Text = "Max Scale:";
            // 
            // chxEnhanceBorderOverlap
            // 
            this.chxEnhanceBorderOverlap.AutoSize = true;
            this.chxEnhanceBorderOverlap.Location = new System.Drawing.Point(8, 1);
            this.chxEnhanceBorderOverlap.Name = "chxEnhanceBorderOverlap";
            this.chxEnhanceBorderOverlap.Size = new System.Drawing.Size(143, 17);
            this.chxEnhanceBorderOverlap.TabIndex = 71;
            this.chxEnhanceBorderOverlap.Text = "Enhance Border Overlap";
            this.chxEnhanceBorderOverlap.UseVisualStyleBackColor = true;
            // 
            // btnTilingScheme1
            // 
            this.btnTilingScheme1.Location = new System.Drawing.Point(343, 118);
            this.btnTilingScheme1.Margin = new System.Windows.Forms.Padding(2);
            this.btnTilingScheme1.Name = "btnTilingScheme1";
            this.btnTilingScheme1.Size = new System.Drawing.Size(86, 24);
            this.btnTilingScheme1.TabIndex = 77;
            this.btnTilingScheme1.Text = "Tiling Scheme";
            this.btnTilingScheme1.UseVisualStyleBackColor = true;
            this.btnTilingScheme1.Click += new System.EventHandler(this.btnTilingScheme1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.chxEnhanceBorderOverlap);
            this.panel1.Controls.Add(this.chxFillEmptyTilesByLowerResolutionTiles);
            this.panel1.Controls.Add(this.ntbMaxScale);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.chxResolveOverlapConflicts);
            this.panel1.Location = new System.Drawing.Point(343, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 114);
            this.panel1.TabIndex = 78;
            // 
            // ntbMaxScale
            // 
            this.ntbMaxScale.Location = new System.Drawing.Point(228, 45);
            this.ntbMaxScale.Name = "ntbMaxScale";
            this.ntbMaxScale.Size = new System.Drawing.Size(60, 20);
            this.ntbMaxScale.TabIndex = 72;
            this.ntbMaxScale.Text = "MAX";
            // 
            // ctrlSelectTransparentColor
            // 
            this.ctrlSelectTransparentColor.Location = new System.Drawing.Point(2, 14);
            this.ctrlSelectTransparentColor.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSelectTransparentColor.Name = "ctrlSelectTransparentColor";
            this.ctrlSelectTransparentColor.Size = new System.Drawing.Size(129, 28);
            this.ctrlSelectTransparentColor.TabIndex = 62;
            // 
            // ntbTransparentColorPrecision
            // 
            this.ntbTransparentColorPrecision.Location = new System.Drawing.Point(194, 16);
            this.ntbTransparentColorPrecision.Name = "ntbTransparentColorPrecision";
            this.ntbTransparentColorPrecision.Size = new System.Drawing.Size(55, 20);
            this.ntbTransparentColorPrecision.TabIndex = 63;
            // 
            // ctrlLayerGridCoordinateSystem
            // 
            this.ctrlLayerGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlLayerGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlLayerGridCoordinateSystem.GroupBoxText = "Source Grid Coordinate System";
            this.ctrlLayerGridCoordinateSystem.IsEditable = false;
            this.ctrlLayerGridCoordinateSystem.Location = new System.Drawing.Point(0, 4);
            this.ctrlLayerGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlLayerGridCoordinateSystem.Name = "ctrlLayerGridCoordinateSystem";
            this.ctrlLayerGridCoordinateSystem.Size = new System.Drawing.Size(336, 137);
            this.ctrlLayerGridCoordinateSystem.TabIndex = 70;
            // 
            // CtrlNonNativeParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnTilingScheme1);
            this.Controls.Add(this.ctrlLayerGridCoordinateSystem);
            this.Name = "CtrlNonNativeParams";
            this.Size = new System.Drawing.Size(644, 145);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chxFillEmptyTilesByLowerResolutionTiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.SelectColor ctrlSelectTransparentColor;
        private System.Windows.Forms.Label label27;
        private Controls.NumericTextBox ntbTransparentColorPrecision;
        private System.Windows.Forms.CheckBox chxResolveOverlapConflicts;
        private System.Windows.Forms.Label label24;
        private Controls.NumericTextBox ntbMaxScale;
        private System.Windows.Forms.CheckBox chxEnhanceBorderOverlap;
        private Controls.CtrlGridCoordinateSystem ctrlLayerGridCoordinateSystem;
        private System.Windows.Forms.Button btnTilingScheme1;
        private System.Windows.Forms.Panel panel1;
    }
}
