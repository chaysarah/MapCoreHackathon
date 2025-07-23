namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucLineItem
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
            this.Tab_LineItem = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyShowSlopePresentation = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.ctrlObjStatePropertyLineSlopeQueryPrecision = new MCTester.Controls.CtrlObjStatePropertyEQueryPrecision();
            this.gbSlopPresentationColorTable = new System.Windows.Forms.GroupBox();
            this.dgvColors = new System.Windows.Forms.DataGridView();
            this.colColorValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colMaxSlop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1 = new MCTester.Controls.CtrlObjStatePropertyDictionaryTraversabilityColor();
            this.ctrlShowTraversabilityPresentation = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.TabControl_SymbolicItemGeneral.SuspendLayout();
            this.Tab_Symbolic.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Tab_LineItem.SuspendLayout();
            this.gbSlopPresentationColorTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).BeginInit();
            this.SuspendLayout();
            // 
            // Tab_LineBased
            // 
            this.Tab_LineBased.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_LineBased.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_LineBased.Size = new System.Drawing.Size(820, 829);
            // 
            // Tab_SightPresentation
            // 
            this.Tab_SightPresentation.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_SightPresentation.Size = new System.Drawing.Size(820, 829);
            // 
            // TabControl_SymbolicItemGeneral
            // 
            this.TabControl_SymbolicItemGeneral.Location = new System.Drawing.Point(5, 5);
            this.TabControl_SymbolicItemGeneral.Margin = new System.Windows.Forms.Padding(5);
            this.TabControl_SymbolicItemGeneral.Size = new System.Drawing.Size(810, 819);
            // 
            // Tab_Symbolic_General
            // 
            this.Tab_Symbolic_General.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_General.Padding = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_General.Size = new System.Drawing.Size(802, 793);
            // 
            // Tab_Symbolic_TransformParams
            // 
            this.Tab_Symbolic_TransformParams.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_TransformParams.Padding = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_TransformParams.Size = new System.Drawing.Size(802, 794);
            // 
            // Tab_Symbolic_Transforms
            // 
            this.Tab_Symbolic_Transforms.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_Transforms.Padding = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_Transforms.Size = new System.Drawing.Size(802, 794);
            // 
            // Tab_Symbolic
            // 
            this.Tab_Symbolic.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic.Padding = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic.Size = new System.Drawing.Size(820, 829);
            // 
            // Tab_Symbolic_AttachPoint
            // 
            this.Tab_Symbolic_AttachPoint.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_AttachPoint.Padding = new System.Windows.Forms.Padding(5);
            this.Tab_Symbolic_AttachPoint.Size = new System.Drawing.Size(802, 794);
            // 
            // Tab_ObjectSchemeItem
            // 
            this.Tab_ObjectSchemeItem.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectSchemeItem.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectSchemeItem.Size = new System.Drawing.Size(820, 829);
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectScehemeNode.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectScehemeNode.Size = new System.Drawing.Size(819, 829);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_LineItem);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tabControl1.Size = new System.Drawing.Size(827, 855);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LineItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_SightPresentation, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LineBased, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_Symbolic, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_LineItem
            // 
            this.Tab_LineItem.Controls.Add(this.ctrlObjStatePropertyDictionaryTraversabilityColor1);
            this.Tab_LineItem.Controls.Add(this.ctrlShowTraversabilityPresentation);
            this.Tab_LineItem.Controls.Add(this.ctrlObjStatePropertyShowSlopePresentation);
            this.Tab_LineItem.Controls.Add(this.ctrlObjStatePropertyLineSlopeQueryPrecision);
            this.Tab_LineItem.Controls.Add(this.gbSlopPresentationColorTable);
            this.Tab_LineItem.Location = new System.Drawing.Point(4, 22);
            this.Tab_LineItem.Name = "Tab_LineItem";
            this.Tab_LineItem.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_LineItem.Size = new System.Drawing.Size(819, 829);
            this.Tab_LineItem.TabIndex = 5;
            this.Tab_LineItem.Text = "Line Item";
            this.Tab_LineItem.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyShowSlopePresentation
            // 
            this.ctrlObjStatePropertyShowSlopePresentation.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyShowSlopePresentation.BoolLabel = "Slope Presentation";
            this.ctrlObjStatePropertyShowSlopePresentation.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyShowSlopePresentation.IsClickApply = false;
            this.ctrlObjStatePropertyShowSlopePresentation.IsClickOK = false;
            this.ctrlObjStatePropertyShowSlopePresentation.Location = new System.Drawing.Point(5, 7);
            this.ctrlObjStatePropertyShowSlopePresentation.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyShowSlopePresentation.Name = "ctrlObjStatePropertyShowSlopePresentation";
            this.ctrlObjStatePropertyShowSlopePresentation.PropertyName = "Show Slope Presentation";
            this.ctrlObjStatePropertyShowSlopePresentation.PropertyValidationResult = null;
            this.ctrlObjStatePropertyShowSlopePresentation.RegBoolVal = false;
            this.ctrlObjStatePropertyShowSlopePresentation.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyShowSlopePresentation.SelBoolVal = false;
            this.ctrlObjStatePropertyShowSlopePresentation.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyShowSlopePresentation.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyShowSlopePresentation.TabIndex = 11;
            // 
            // ctrlObjStatePropertyLineSlopeQueryPrecision
            // 
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.EnumType = "";
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.IsClickApply = false;
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.IsClickOK = false;
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.Location = new System.Drawing.Point(5, 141);
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.Name = "ctrlObjStatePropertyLineSlopeQueryPrecision";
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.PropertyName = "Slope Query Precision";
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.PropertyValidationResult = null;
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.RegEnumVal = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.SelEnumVal = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyLineSlopeQueryPrecision.TabIndex = 10;
            // 
            // gbSlopPresentationColorTable
            // 
            this.gbSlopPresentationColorTable.Controls.Add(this.dgvColors);
            this.gbSlopPresentationColorTable.Location = new System.Drawing.Point(412, 6);
            this.gbSlopPresentationColorTable.Name = "gbSlopPresentationColorTable";
            this.gbSlopPresentationColorTable.Size = new System.Drawing.Size(400, 232);
            this.gbSlopPresentationColorTable.TabIndex = 8;
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
            // ctrlObjStatePropertyDictionaryTraversabilityColor1
            // 
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.EnumKey = MapCore.DNEPointTraversability._EPT_TRAVERSABLE;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.IsClickApply = false;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.IsClickOK = false;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Location = new System.Drawing.Point(5, 275);
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Name = "ctrlObjStatePropertyDictionaryTraversabilityColor1";
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.PropertyName = "Traversability Color";
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.PropertyValidationResult = null;
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.Size = new System.Drawing.Size(395, 164);
            this.ctrlObjStatePropertyDictionaryTraversabilityColor1.TabIndex = 22;
            // 
            // ctrlShowTraversabilityPresentation
            // 
            this.ctrlShowTraversabilityPresentation.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlShowTraversabilityPresentation.BoolLabel = "Show Traversability Presentation";
            this.ctrlShowTraversabilityPresentation.CurrentObjectSchemeNode = null;
            this.ctrlShowTraversabilityPresentation.IsClickApply = false;
            this.ctrlShowTraversabilityPresentation.IsClickOK = false;
            this.ctrlShowTraversabilityPresentation.Location = new System.Drawing.Point(408, 275);
            this.ctrlShowTraversabilityPresentation.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlShowTraversabilityPresentation.Name = "ctrlShowTraversabilityPresentation";
            this.ctrlShowTraversabilityPresentation.PropertyName = "Show Traversability Presentation";
            this.ctrlShowTraversabilityPresentation.PropertyValidationResult = null;
            this.ctrlShowTraversabilityPresentation.RegBoolVal = false;
            this.ctrlShowTraversabilityPresentation.RegPropertyID = ((uint)(0u));
            this.ctrlShowTraversabilityPresentation.SelBoolVal = false;
            this.ctrlShowTraversabilityPresentation.SelPropertyID = ((uint)(4294967295u));
            this.ctrlShowTraversabilityPresentation.Size = new System.Drawing.Size(400, 130);
            this.ctrlShowTraversabilityPresentation.TabIndex = 21;
            // 
            // ucLineItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "ucLineItem";
            this.Size = new System.Drawing.Size(827, 910);
            this.TabControl_SymbolicItemGeneral.ResumeLayout(false);
            this.Tab_Symbolic.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Tab_LineItem.ResumeLayout(false);
            this.gbSlopPresentationColorTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage Tab_LineItem;
        private System.Windows.Forms.GroupBox gbSlopPresentationColorTable;
        private System.Windows.Forms.DataGridView dgvColors;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColorValue;
        private System.Windows.Forms.DataGridViewButtonColumn colColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxSlop;
        private Controls.CtrlObjStatePropertyEQueryPrecision ctrlObjStatePropertyLineSlopeQueryPrecision;
        private Controls.CtrlObjStatePropertyBool ctrlObjStatePropertyShowSlopePresentation;
        private Controls.CtrlObjStatePropertyDictionaryTraversabilityColor ctrlObjStatePropertyDictionaryTraversabilityColor1;
        private Controls.CtrlObjStatePropertyBool ctrlShowTraversabilityPresentation;
    }
}
