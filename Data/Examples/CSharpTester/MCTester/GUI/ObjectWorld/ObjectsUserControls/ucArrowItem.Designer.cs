namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucArrowItem
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
            this.Tab_Arrow = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1 = new MCTester.Controls.CtrlObjStatePropertyDictionaryTraversabilityColor();
            this.ctrlShowTraversabilityPresentation = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.txtArrowCoordinateSystem = new System.Windows.Forms.TextBox();
            this.ctrlObjStatePropertyShowSlopePresentation = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.ctrlObjStatePropertySlopeQueryPrecision = new MCTester.Controls.CtrlObjStatePropertyEQueryPrecision();
            this.ctrlObjStatePropertyGapSize = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertyHeadAngle = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertyHeadSize = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.gbSlopPresentationColorTable = new System.Windows.Forms.GroupBox();
            this.dgvColors = new System.Windows.Forms.DataGridView();
            this.colColorValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colMaxSlop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.TabControl_SymbolicItemGeneral.SuspendLayout();
            this.Tab_Symbolic.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Tab_Arrow.SuspendLayout();
            this.gbSlopPresentationColorTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).BeginInit();
            this.SuspendLayout();
            // 
            // Tab_ClosedShape
            // 
            this.Tab_ClosedShape.Location = new System.Drawing.Point(4, 22);
            this.Tab_ClosedShape.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_ClosedShape.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_ClosedShape.Size = new System.Drawing.Size(815, 824);
            // 
            // Tab_LineBased
            // 
            this.Tab_LineBased.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_LineBased.Padding = new System.Windows.Forms.Padding(5);
            this.Tab_LineBased.Size = new System.Drawing.Size(815, 824);
            // 
            // Tab_SightPresentation
            // 
            this.Tab_SightPresentation.Margin = new System.Windows.Forms.Padding(5);
            // 
            // TabControl_SymbolicItemGeneral
            // 
            this.TabControl_SymbolicItemGeneral.Location = new System.Drawing.Point(7, 6);
            this.TabControl_SymbolicItemGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.TabControl_SymbolicItemGeneral.Size = new System.Drawing.Size(801, 812);
            // 
            // Tab_Symbolic_General
            // 
            this.Tab_Symbolic_General.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_Symbolic_General.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_Symbolic_General.Size = new System.Drawing.Size(793, 786);
            // 
            // Tab_Symbolic_TransformParams
            // 
            this.Tab_Symbolic_TransformParams.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_Symbolic_TransformParams.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_Symbolic_TransformParams.Size = new System.Drawing.Size(793, 786);
            // 
            // Tab_Symbolic_Transforms
            // 
            this.Tab_Symbolic_Transforms.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_Symbolic_Transforms.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_Symbolic_Transforms.Size = new System.Drawing.Size(793, 786);
            // 
            // Tab_Symbolic
            // 
            this.Tab_Symbolic.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_Symbolic.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            // 
            // Tab_Symbolic_AttachPoint
            // 
            this.Tab_Symbolic_AttachPoint.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_Symbolic_AttachPoint.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_Symbolic_AttachPoint.Size = new System.Drawing.Size(793, 786);
            // 
            // Tab_ObjectSchemeItem
            // 
            this.Tab_ObjectSchemeItem.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.Tab_ObjectSchemeItem.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.Tab_ObjectScehemeNode.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.Tab_ObjectScehemeNode.Size = new System.Drawing.Size(813, 824);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_Arrow);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.tabControl1.Size = new System.Drawing.Size(821, 850);
            this.tabControl1.Controls.SetChildIndex(this.Tab_Arrow, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ClosedShape, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_SightPresentation, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LineBased, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_Symbolic, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_Arrow
            // 
            this.Tab_Arrow.Controls.Add(this.label26);
            this.Tab_Arrow.Controls.Add(this.ctrlObjStatePropertyDictionaryTraversabilityColor1);
            this.Tab_Arrow.Controls.Add(this.ctrlShowTraversabilityPresentation);
            this.Tab_Arrow.Controls.Add(this.txtArrowCoordinateSystem);
            this.Tab_Arrow.Controls.Add(this.ctrlObjStatePropertyShowSlopePresentation);
            this.Tab_Arrow.Controls.Add(this.ctrlObjStatePropertySlopeQueryPrecision);
            this.Tab_Arrow.Controls.Add(this.ctrlObjStatePropertyGapSize);
            this.Tab_Arrow.Controls.Add(this.ctrlObjStatePropertyHeadAngle);
            this.Tab_Arrow.Controls.Add(this.ctrlObjStatePropertyHeadSize);
            this.Tab_Arrow.Controls.Add(this.gbSlopPresentationColorTable);
            this.Tab_Arrow.Location = new System.Drawing.Point(4, 22);
            this.Tab_Arrow.Name = "Tab_Arrow";
            this.Tab_Arrow.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_Arrow.Size = new System.Drawing.Size(813, 824);
            this.Tab_Arrow.TabIndex = 5;
            this.Tab_Arrow.Text = "Arrow";
            this.Tab_Arrow.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyDictionaryTraversabilityColor1
            // 
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.EnumKey = MapCore.DNEPointTraversability._EPT_TRAVERSABLE;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.IsClickApply = false;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.IsClickOK = false;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Location = new System.Drawing.Point(9, 542);
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Name = "ctrlObjStatePropertyDictionaryTraversabilityColor1";
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.PropertyName = "Traversability Color";
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.PropertyValidationResult = null;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Size = new System.Drawing.Size(395, 164);
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.TabIndex = 20;
            // 
            // ctrlShowTraversabilityPresentation
            // 
            this.ctrlShowTraversabilityPresentation.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlShowTraversabilityPresentation.BoolLabel = "Show Traversability Presentation";
            this.ctrlShowTraversabilityPresentation.CurrentObjectSchemeNode = null;
            this.ctrlShowTraversabilityPresentation.IsClickApply = false;
            this.ctrlShowTraversabilityPresentation.IsClickOK = false;
            this.ctrlShowTraversabilityPresentation.Location = new System.Drawing.Point(412, 542);
            this.ctrlShowTraversabilityPresentation.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlShowTraversabilityPresentation.Name = "ctrlShowTraversabilityPresentation";
            this.ctrlShowTraversabilityPresentation.PropertyName = "Show Traversability Presentation";
            this.ctrlShowTraversabilityPresentation.PropertyValidationResult = null;
            this.ctrlShowTraversabilityPresentation.RegBoolVal = false;
            this.ctrlShowTraversabilityPresentation.RegPropertyID = ((uint)(0u));
            this.ctrlShowTraversabilityPresentation.SelBoolVal = false;
            this.ctrlShowTraversabilityPresentation.SelPropertyID = ((uint)(4294967295u));
            this.ctrlShowTraversabilityPresentation.Size = new System.Drawing.Size(400, 130);
            this.ctrlShowTraversabilityPresentation.TabIndex = 19;
            // 
            // txtArrowCoordinateSystem
            // 
            this.txtArrowCoordinateSystem.Enabled = false;
            this.txtArrowCoordinateSystem.Location = new System.Drawing.Point(139, 8);
            this.txtArrowCoordinateSystem.Name = "txtArrowCoordinateSystem";
            this.txtArrowCoordinateSystem.Size = new System.Drawing.Size(141, 20);
            this.txtArrowCoordinateSystem.TabIndex = 18;
            // 
            // ctrlObjStatePropertyShowSlopePresentation
            // 
            this.ctrlObjStatePropertyShowSlopePresentation.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyShowSlopePresentation.BoolLabel = " Slope Presentation";
            this.ctrlObjStatePropertyShowSlopePresentation.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyShowSlopePresentation.IsClickApply = false;
            this.ctrlObjStatePropertyShowSlopePresentation.IsClickOK = false;
            this.ctrlObjStatePropertyShowSlopePresentation.Location = new System.Drawing.Point(412, 168);
            this.ctrlObjStatePropertyShowSlopePresentation.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyShowSlopePresentation.Name = "ctrlObjStatePropertyShowSlopePresentation";
            this.ctrlObjStatePropertyShowSlopePresentation.PropertyName = "Show Slope Presentation";
            this.ctrlObjStatePropertyShowSlopePresentation.PropertyValidationResult = null;
            this.ctrlObjStatePropertyShowSlopePresentation.RegBoolVal = false;
            this.ctrlObjStatePropertyShowSlopePresentation.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyShowSlopePresentation.SelBoolVal = false;
            this.ctrlObjStatePropertyShowSlopePresentation.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyShowSlopePresentation.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyShowSlopePresentation.TabIndex = 15;
            // 
            // ctrlObjStatePropertySlopeQueryPrecision
            // 
            this.ctrlObjStatePropertySlopeQueryPrecision.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySlopeQueryPrecision.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySlopeQueryPrecision.EnumType = "";
            this.ctrlObjStatePropertySlopeQueryPrecision.IsClickApply = false;
            this.ctrlObjStatePropertySlopeQueryPrecision.IsClickOK = false;
            this.ctrlObjStatePropertySlopeQueryPrecision.Location = new System.Drawing.Point(412, 305);
            this.ctrlObjStatePropertySlopeQueryPrecision.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertySlopeQueryPrecision.Name = "ctrlObjStatePropertySlopeQueryPrecision";
            this.ctrlObjStatePropertySlopeQueryPrecision.PropertyName = "Slope Query Precision";
            this.ctrlObjStatePropertySlopeQueryPrecision.PropertyValidationResult = null;
            this.ctrlObjStatePropertySlopeQueryPrecision.RegEnumVal = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlObjStatePropertySlopeQueryPrecision.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySlopeQueryPrecision.SelEnumVal = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlObjStatePropertySlopeQueryPrecision.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertySlopeQueryPrecision.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertySlopeQueryPrecision.TabIndex = 14;
            // 
            // ctrlObjStatePropertyGapSize
            // 
            this.ctrlObjStatePropertyGapSize.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyGapSize.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyGapSize.IsClickApply = false;
            this.ctrlObjStatePropertyGapSize.IsClickOK = false;
            this.ctrlObjStatePropertyGapSize.Location = new System.Drawing.Point(6, 170);
            this.ctrlObjStatePropertyGapSize.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyGapSize.Name = "ctrlObjStatePropertyGapSize";
            this.ctrlObjStatePropertyGapSize.PropertyName = "Gap Size";
            this.ctrlObjStatePropertyGapSize.PropertyValidationResult = null;
            this.ctrlObjStatePropertyGapSize.RegFloatVal = 0F;
            this.ctrlObjStatePropertyGapSize.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyGapSize.SelFloatVal = 0F;
            this.ctrlObjStatePropertyGapSize.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyGapSize.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyGapSize.TabIndex = 13;
            // 
            // ctrlObjStatePropertyHeadAngle
            // 
            this.ctrlObjStatePropertyHeadAngle.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyHeadAngle.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyHeadAngle.IsClickApply = false;
            this.ctrlObjStatePropertyHeadAngle.IsClickOK = false;
            this.ctrlObjStatePropertyHeadAngle.Location = new System.Drawing.Point(412, 34);
            this.ctrlObjStatePropertyHeadAngle.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyHeadAngle.Name = "ctrlObjStatePropertyHeadAngle";
            this.ctrlObjStatePropertyHeadAngle.PropertyName = "Head Angle";
            this.ctrlObjStatePropertyHeadAngle.PropertyValidationResult = null;
            this.ctrlObjStatePropertyHeadAngle.RegFloatVal = 0F;
            this.ctrlObjStatePropertyHeadAngle.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyHeadAngle.SelFloatVal = 0F;
            this.ctrlObjStatePropertyHeadAngle.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyHeadAngle.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyHeadAngle.TabIndex = 12;
            // 
            // ctrlObjStatePropertyHeadSize
            // 
            this.ctrlObjStatePropertyHeadSize.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyHeadSize.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyHeadSize.IsClickApply = false;
            this.ctrlObjStatePropertyHeadSize.IsClickOK = false;
            this.ctrlObjStatePropertyHeadSize.Location = new System.Drawing.Point(6, 34);
            this.ctrlObjStatePropertyHeadSize.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyHeadSize.Name = "ctrlObjStatePropertyHeadSize";
            this.ctrlObjStatePropertyHeadSize.PropertyName = "Head Size";
            this.ctrlObjStatePropertyHeadSize.PropertyValidationResult = null;
            this.ctrlObjStatePropertyHeadSize.RegFloatVal = 0F;
            this.ctrlObjStatePropertyHeadSize.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyHeadSize.SelFloatVal = 0F;
            this.ctrlObjStatePropertyHeadSize.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyHeadSize.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyHeadSize.TabIndex = 11;
            // 
            // gbSlopPresentationColorTable
            // 
            this.gbSlopPresentationColorTable.Controls.Add(this.dgvColors);
            this.gbSlopPresentationColorTable.Location = new System.Drawing.Point(6, 305);
            this.gbSlopPresentationColorTable.Name = "gbSlopPresentationColorTable";
            this.gbSlopPresentationColorTable.Size = new System.Drawing.Size(400, 232);
            this.gbSlopPresentationColorTable.TabIndex = 7;
            this.gbSlopPresentationColorTable.TabStop = false;
            this.gbSlopPresentationColorTable.Text = "Slop Presentation Color Table";
            // 
            // dgvColors
            // 
            this.dgvColors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColorValue,
            this.colColor,
            this.colMaxSlop});
            this.dgvColors.Location = new System.Drawing.Point(6, 19);
            this.dgvColors.Name = "dgvColors";
            this.dgvColors.Size = new System.Drawing.Size(388, 204);
            this.dgvColors.TabIndex = 1;
            this.dgvColors.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColors_CellClick);
            // 
            // colColorValue
            // 
            this.colColorValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colColorValue.HeaderText = "Value";
            this.colColorValue.Name = "colColorValue";
            this.colColorValue.ReadOnly = true;
            // 
            // colColor
            // 
            this.colColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colColor.HeaderText = "Color";
            this.colColor.Name = "colColor";
            this.colColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colColor.Width = 70;
            // 
            // colMaxSlop
            // 
            this.colMaxSlop.HeaderText = "Max Slop";
            this.colMaxSlop.Name = "colMaxSlop";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Grid Coordinate System:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(20, 9);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Coordinate System:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 11);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(128, 13);
            this.label26.TabIndex = 86;
            this.label26.Text = "Arrow Coordinate System:";
            // 
            // ucArrowItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.Name = "ucArrowItem";
            this.Size = new System.Drawing.Size(821, 910);
            this.TabControl_SymbolicItemGeneral.ResumeLayout(false);
            this.Tab_Symbolic.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Tab_Arrow.ResumeLayout(false);
            this.Tab_Arrow.PerformLayout();
            this.gbSlopPresentationColorTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TabPage Tab_Arrow;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox gbSlopPresentationColorTable;
        private System.Windows.Forms.DataGridView dgvColors;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColorValue;
        private System.Windows.Forms.DataGridViewButtonColumn colColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxSlop;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertyGapSize;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertyHeadAngle;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertyHeadSize;
        private Controls.CtrlObjStatePropertyEQueryPrecision ctrlObjStatePropertySlopeQueryPrecision;
        private Controls.CtrlObjStatePropertyBool ctrlObjStatePropertyShowSlopePresentation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArrowCoordinateSystem;
        private Controls.CtrlObjStatePropertyBool ctrlShowTraversabilityPresentation;
        private Controls.CtrlObjStatePropertyDictionaryTraversabilityColor ctrlObjStatePropertyDictionaryTraversabilityColor1;
        private System.Windows.Forms.Label label26;
    }
}
