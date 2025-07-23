namespace MCTester.Controls
{
    partial class CtrlSMcBoxArray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlSMcBoxArray));
            this.ctrl3DPoint = new MCTester.Controls.Ctrl3DVector();
            this.ctrlSamplePoint = new MCTester.Controls.CtrlSamplePoint();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrl3DPoint
            // 
            this.ctrl3DPoint.IsReadOnly = false;
            this.ctrl3DPoint.Location = new System.Drawing.Point(73, 117);
            this.ctrl3DPoint.Margin = new System.Windows.Forms.Padding(5);
            this.ctrl3DPoint.Name = "ctrl3DPoint";
            this.ctrl3DPoint.Size = new System.Drawing.Size(308, 34);
            this.ctrl3DPoint.TabIndex = 93;
            this.ctrl3DPoint.Visible = false;
            this.ctrl3DPoint.X = 0D;
            this.ctrl3DPoint.Y = 0D;
            this.ctrl3DPoint.Z = 0D;
            this.ctrl3DPoint.EnabledChanged += new System.EventHandler(this.ctrl3DPoint_EnabledChanged);
            // 
            // ctrlSamplePoint
            // 
            this.ctrlSamplePoint._DgvControlName = "";
            this.ctrlSamplePoint._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePoint._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePoint._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePoint._SampleOnePoint = true;
            this.ctrlSamplePoint._UserControlName = "ctrl3DPoint";
            this.ctrlSamplePoint.IsChangeEnableWhenUserSelectPoint = true;
            this.ctrlSamplePoint.Location = new System.Drawing.Point(525, 123);
            this.ctrlSamplePoint.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSamplePoint.Name = "ctrlSamplePoint";
            this.ctrlSamplePoint.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePoint.Size = new System.Drawing.Size(44, 28);
            this.ctrlSamplePoint.TabIndex = 92;
            this.ctrlSamplePoint.Text = "...";
            this.ctrlSamplePoint.UseVisualStyleBackColor = true;
            this.ctrlSamplePoint.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column1,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(6, 32);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(731, 143);
            this.dataGridView1.TabIndex = 91;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "X";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Y";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Z";
            this.Column5.Name = "Column5";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "...";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Text = "...";
            this.Column1.Width = 30;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "X";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Y";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Z";
            this.Column8.Name = "Column8";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "...";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column2.Text = "...";
            this.Column2.Width = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 94;
            this.label1.Text = "Bottom Left (MIN)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 95;
            this.label2.Text = "Top Right (MAX)";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(639, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 24);
            this.btnClear.TabIndex = 96;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // CtrlSMcBoxArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrl3DPoint);
            this.Controls.Add(this.ctrlSamplePoint);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CtrlSMcBoxArray";
            this.Size = new System.Drawing.Size(742, 180);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.Ctrl3DVector ctrl3DPoint;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePoint;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClear;
    }
}
