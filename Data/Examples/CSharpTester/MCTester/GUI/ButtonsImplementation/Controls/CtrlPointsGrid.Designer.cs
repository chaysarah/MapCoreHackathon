namespace MCTester.Controls
{
    partial class CtrlPointsGrid
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
            this.components = new System.ComponentModel.Container();
            this.dgvLocationPoints = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClearDGV = new System.Windows.Forms.Button();
            this.btnExportLocationPointsFromCSV = new System.Windows.Forms.Button();
            this.btnImportLocationPointsFromCSV = new System.Windows.Forms.Button();
            this.ctrlSampleLocationPoints = new MCTester.Controls.CtrlSamplePoint();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationPoints)).BeginInit();
            this.dgvMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLocationPoints
            // 
            this.dgvLocationPoints.AllowDrop = true;
            this.dgvLocationPoints.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLocationPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocationPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.X,
            this.Y,
            this.Z});
            this.dgvLocationPoints.ContextMenuStrip = this.dgvMenu;
            this.dgvLocationPoints.Location = new System.Drawing.Point(3, 3);
            this.dgvLocationPoints.Name = "dgvLocationPoints";
            this.dgvLocationPoints.Size = new System.Drawing.Size(342, 158);
            this.dgvLocationPoints.TabIndex = 115;
            this.dgvLocationPoints.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvLocationPoints_RowPostPaint);
            this.dgvLocationPoints.SelectionChanged += new System.EventHandler(this.dgvLocationPoints_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 35;
            // 
            // X
            // 
            this.X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // Z
            // 
            this.Z.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            // 
            // dgvMenu
            // 
            this.dgvMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.dgvMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSelectAll,
            this.tsmiCopy,
            this.tsmiPaste,
            this.tsmiDelete});
            this.dgvMenu.Name = "dgvMenu";
            this.dgvMenu.Size = new System.Drawing.Size(123, 92);
            // 
            // tsmiSelectAll
            // 
            this.tsmiSelectAll.Name = "tsmiSelectAll";
            this.tsmiSelectAll.Size = new System.Drawing.Size(122, 22);
            this.tsmiSelectAll.Text = "Select All";
            this.tsmiSelectAll.Click += new System.EventHandler(this.tsmiSelectAll_Click);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(122, 22);
            this.tsmiCopy.Text = "Copy";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.Size = new System.Drawing.Size(122, 22);
            this.tsmiPaste.Text = "Paste";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(122, 22);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // btnClearDGV
            // 
            this.btnClearDGV.ForeColor = System.Drawing.Color.Black;
            this.btnClearDGV.Location = new System.Drawing.Point(39, 167);
            this.btnClearDGV.Name = "btnClearDGV";
            this.btnClearDGV.Size = new System.Drawing.Size(68, 23);
            this.btnClearDGV.TabIndex = 111;
            this.btnClearDGV.Text = "Clear";
            this.btnClearDGV.UseVisualStyleBackColor = true;
            this.btnClearDGV.Click += new System.EventHandler(this.btnClearDGV_Click);
            // 
            // btnExportLocationPointsFromCSV
            // 
            this.btnExportLocationPointsFromCSV.ForeColor = System.Drawing.Color.Black;
            this.btnExportLocationPointsFromCSV.Location = new System.Drawing.Point(180, 167);
            this.btnExportLocationPointsFromCSV.Name = "btnExportLocationPointsFromCSV";
            this.btnExportLocationPointsFromCSV.Size = new System.Drawing.Size(65, 23);
            this.btnExportLocationPointsFromCSV.TabIndex = 116;
            this.btnExportLocationPointsFromCSV.Text = "Export...";
            this.btnExportLocationPointsFromCSV.UseVisualStyleBackColor = true;
            this.btnExportLocationPointsFromCSV.Click += new System.EventHandler(this.btnExportLocationPointsToCSV_Click);
            // 
            // btnImportLocationPointsFromCSV
            // 
            this.btnImportLocationPointsFromCSV.ForeColor = System.Drawing.Color.Black;
            this.btnImportLocationPointsFromCSV.Location = new System.Drawing.Point(111, 167);
            this.btnImportLocationPointsFromCSV.Name = "btnImportLocationPointsFromCSV";
            this.btnImportLocationPointsFromCSV.Size = new System.Drawing.Size(65, 23);
            this.btnImportLocationPointsFromCSV.TabIndex = 113;
            this.btnImportLocationPointsFromCSV.Text = "Import...";
            this.btnImportLocationPointsFromCSV.UseVisualStyleBackColor = true;
            this.btnImportLocationPointsFromCSV.Click += new System.EventHandler(this.btnImportLocationPointsFromCSV_Click);
            // 
            // ctrlSampleLocationPoints
            // 
            this.ctrlSampleLocationPoints._DgvControlName = "dgvLocationPoints";
            this.ctrlSampleLocationPoints._IsRelativeToDTM = false;
            this.ctrlSampleLocationPoints._PointInOverlayManagerCoordSys = true;
            this.ctrlSampleLocationPoints._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSampleLocationPoints._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSampleLocationPoints._SampleOnePoint = false;
            this.ctrlSampleLocationPoints._UserControlName = null;
            this.ctrlSampleLocationPoints.IsAsync = false;
            this.ctrlSampleLocationPoints.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSampleLocationPoints.Location = new System.Drawing.Point(2, 167);
            this.ctrlSampleLocationPoints.Name = "ctrlSampleLocationPoints";
            this.ctrlSampleLocationPoints.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSampleLocationPoints.Size = new System.Drawing.Size(33, 23);
            this.ctrlSampleLocationPoints.TabIndex = 112;
            this.ctrlSampleLocationPoints.Text = "...";
            this.ctrlSampleLocationPoints.UseVisualStyleBackColor = true;
            // 
            // CtrlPointsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvLocationPoints);
            this.Controls.Add(this.btnClearDGV);
            this.Controls.Add(this.ctrlSampleLocationPoints);
            this.Controls.Add(this.btnImportLocationPointsFromCSV);
            this.Controls.Add(this.btnExportLocationPointsFromCSV);
            this.Name = "CtrlPointsGrid";
            this.Size = new System.Drawing.Size(345, 192);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocationPoints)).EndInit();
            this.dgvMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvLocationPoints;
        private System.Windows.Forms.Button btnClearDGV;
        private MCTester.Controls.CtrlSamplePoint ctrlSampleLocationPoints;
        private System.Windows.Forms.Button btnExportLocationPointsFromCSV;
        private System.Windows.Forms.Button btnImportLocationPointsFromCSV;
        private System.Windows.Forms.ContextMenuStrip dgvMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
    }
}
