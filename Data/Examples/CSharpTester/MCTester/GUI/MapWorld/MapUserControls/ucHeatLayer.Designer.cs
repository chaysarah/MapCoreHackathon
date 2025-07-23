namespace MCTester.MapWorld.MapUserControls
{
    partial class ucHeatLayer
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
            this.tpHeat = new System.Windows.Forms.TabPage();
            this.btnGetMinMaxValues = new System.Windows.Forms.Button();
            this.ntbScale = new MCTester.Controls.NumericTextBox();
            this.btnGetColorTable = new System.Windows.Forms.Button();
            this.dgvColorTable = new System.Windows.Forms.DataGridView();
            this.colColorValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnSetColorTable = new System.Windows.Forms.Button();
            this.lblColorRange = new System.Windows.Forms.Label();
            this.cmbColorRange = new System.Windows.Forms.ComboBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtMin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tcLayer.SuspendLayout();
            this.tpHeat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColorTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tcLayer
            // 
            this.tcLayer.Controls.Add(this.tpHeat);
            this.tcLayer.Controls.SetChildIndex(this.tpHeat, 0);
            this.tcLayer.Controls.SetChildIndex(this.tpGeneral, 0);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // tpHeat
            // 
            this.tpHeat.Controls.Add(this.label7);
            this.tpHeat.Controls.Add(this.btnGetMinMaxValues);
            this.tpHeat.Controls.Add(this.ntbScale);
            this.tpHeat.Controls.Add(this.btnGetColorTable);
            this.tpHeat.Controls.Add(this.dgvColorTable);
            this.tpHeat.Controls.Add(this.btnSetColorTable);
            this.tpHeat.Controls.Add(this.lblColorRange);
            this.tpHeat.Controls.Add(this.cmbColorRange);
            this.tpHeat.Controls.Add(this.lblMax);
            this.tpHeat.Controls.Add(this.lblMin);
            this.tpHeat.Controls.Add(this.txtMax);
            this.tpHeat.Controls.Add(this.txtMin);
            this.tpHeat.Location = new System.Drawing.Point(4, 22);
            this.tpHeat.Name = "tpHeat";
            this.tpHeat.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpHeat.Size = new System.Drawing.Size(737, 595);
            this.tpHeat.TabIndex = 4;
            this.tpHeat.Text = "Heat";
            this.tpHeat.UseVisualStyleBackColor = true;
            this.tpHeat.Controls.SetChildIndex(this.txtMin, 0);
            this.tpHeat.Controls.SetChildIndex(this.txtMax, 0);
            this.tpHeat.Controls.SetChildIndex(this.lblMin, 0);
            this.tpHeat.Controls.SetChildIndex(this.lblMax, 0);
            this.tpHeat.Controls.SetChildIndex(this.cmbColorRange, 0);
            this.tpHeat.Controls.SetChildIndex(this.lblColorRange, 0);
            this.tpHeat.Controls.SetChildIndex(this.btnSetColorTable, 0);
            this.tpHeat.Controls.SetChildIndex(this.dgvColorTable, 0);
            this.tpHeat.Controls.SetChildIndex(this.btnGetColorTable, 0);
            this.tpHeat.Controls.SetChildIndex(this.ntbScale, 0);
            this.tpHeat.Controls.SetChildIndex(this.btnGetMinMaxValues, 0);
            this.tpHeat.Controls.SetChildIndex(this.label7, 0);
            // 
            // btnGetMinMaxValues
            // 
            this.btnGetMinMaxValues.Location = new System.Drawing.Point(219, 40);
            this.btnGetMinMaxValues.Name = "btnGetMinMaxValues";
            this.btnGetMinMaxValues.Size = new System.Drawing.Size(133, 23);
            this.btnGetMinMaxValues.TabIndex = 12;
            this.btnGetMinMaxValues.Text = "Get Min Max Values";
            this.btnGetMinMaxValues.UseVisualStyleBackColor = true;
            this.btnGetMinMaxValues.Click += new System.EventHandler(this.btnGetMinMaxValues_Click);
            // 
            // ntbScale
            // 
            this.ntbScale.Location = new System.Drawing.Point(85, 42);
            this.ntbScale.Name = "ntbScale";
            this.ntbScale.Size = new System.Drawing.Size(100, 20);
            this.ntbScale.TabIndex = 10;
            // 
            // btnGetColorTable
            // 
            this.btnGetColorTable.Location = new System.Drawing.Point(387, 137);
            this.btnGetColorTable.Name = "btnGetColorTable";
            this.btnGetColorTable.Size = new System.Drawing.Size(106, 23);
            this.btnGetColorTable.TabIndex = 8;
            this.btnGetColorTable.Text = "Get Color Table";
            this.btnGetColorTable.UseVisualStyleBackColor = true;
            this.btnGetColorTable.Click += new System.EventHandler(this.btnGetColorTable_Click);
            // 
            // dgvColorTable
            // 
            this.dgvColorTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColorTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColorValue,
            this.colColor});
            this.dgvColorTable.Location = new System.Drawing.Point(39, 181);
            this.dgvColorTable.Name = "dgvColorTable";
            this.dgvColorTable.Size = new System.Drawing.Size(313, 278);
            this.dgvColorTable.TabIndex = 7;
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
            // btnSetColorTable
            // 
            this.btnSetColorTable.Location = new System.Drawing.Point(262, 137);
            this.btnSetColorTable.Name = "btnSetColorTable";
            this.btnSetColorTable.Size = new System.Drawing.Size(106, 23);
            this.btnSetColorTable.TabIndex = 6;
            this.btnSetColorTable.Text = "Set Color Table ";
            this.btnSetColorTable.UseVisualStyleBackColor = true;
            this.btnSetColorTable.Click += new System.EventHandler(this.btnSetColorTable_Click);
            // 
            // lblColorRange
            // 
            this.lblColorRange.AutoSize = true;
            this.lblColorRange.Location = new System.Drawing.Point(36, 142);
            this.lblColorRange.Name = "lblColorRange";
            this.lblColorRange.Size = new System.Drawing.Size(66, 13);
            this.lblColorRange.TabIndex = 5;
            this.lblColorRange.Text = "Color Range";
            // 
            // cmbColorRange
            // 
            this.cmbColorRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColorRange.FormattingEnabled = true;
            this.cmbColorRange.Items.AddRange(new object[] {
            "Green-Red",
            "Blue-Red",
            "Green-Blue",
            "None"});
            this.cmbColorRange.Location = new System.Drawing.Point(108, 139);
            this.cmbColorRange.Name = "cmbColorRange";
            this.cmbColorRange.Size = new System.Drawing.Size(121, 21);
            this.cmbColorRange.TabIndex = 4;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(41, 97);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(27, 13);
            this.lblMax.TabIndex = 3;
            this.lblMax.Text = "Max";
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(41, 71);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(24, 13);
            this.lblMin.TabIndex = 2;
            this.lblMin.Text = "Min";
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(85, 94);
            this.txtMax.Name = "txtMax";
            this.txtMax.ReadOnly = true;
            this.txtMax.Size = new System.Drawing.Size(100, 20);
            this.txtMax.TabIndex = 1;
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(85, 68);
            this.txtMin.Name = "txtMin";
            this.txtMin.ReadOnly = true;
            this.txtMin.Size = new System.Drawing.Size(100, 20);
            this.txtMin.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Heat Map Params:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Scale:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Scale";
            // 
            // ucHeatLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucHeatLayer";
            this.tcLayer.ResumeLayout(false);
            this.tpHeat.ResumeLayout(false);
            this.tpHeat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColorTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpHeat;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtMin;
        private System.Windows.Forms.ComboBox cmbColorRange;
        private System.Windows.Forms.Button btnSetColorTable;
        private System.Windows.Forms.Label lblColorRange;
        private System.Windows.Forms.Button btnGetColorTable;
        private System.Windows.Forms.DataGridView dgvColorTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColorValue;
        private System.Windows.Forms.DataGridViewButtonColumn colColor;
        private System.Windows.Forms.Button btnGetMinMaxValues;
        private System.Windows.Forms.Label label6;
        private Controls.NumericTextBox ntbScale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
    }
}
