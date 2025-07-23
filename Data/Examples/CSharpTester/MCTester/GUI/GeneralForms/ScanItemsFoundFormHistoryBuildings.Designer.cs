namespace MCTester.General_Forms
{
    partial class ScanItemsFoundFormHistoryBuildings
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbTargetId = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetBuildingHeights = new System.Windows.Forms.Button();
            this.dgvHeights = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chxStay = new System.Windows.Forms.CheckBox();
            this.chxTextOrientationFromNormal = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeights)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Object ID:";
            // 
            // tbTargetId
            // 
            this.tbTargetId.Location = new System.Drawing.Point(142, 15);
            this.tbTargetId.Margin = new System.Windows.Forms.Padding(2);
            this.tbTargetId.Name = "tbTargetId";
            this.tbTargetId.ReadOnly = true;
            this.tbTargetId.Size = new System.Drawing.Size(415, 20);
            this.tbTargetId.TabIndex = 1;
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(142, 48);
            this.txtURL.Margin = new System.Windows.Forms.Padding(2);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(415, 20);
            this.txtURL.TabIndex = 126;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 125;
            this.label2.Text = "Smart Reality Server URL:";
            // 
            // btnGetBuildingHeights
            // 
            this.btnGetBuildingHeights.Location = new System.Drawing.Point(296, 82);
            this.btnGetBuildingHeights.Name = "btnGetBuildingHeights";
            this.btnGetBuildingHeights.Size = new System.Drawing.Size(107, 23);
            this.btnGetBuildingHeights.TabIndex = 127;
            this.btnGetBuildingHeights.Text = "Get";
            this.btnGetBuildingHeights.UseVisualStyleBackColor = true;
            this.btnGetBuildingHeights.Click += new System.EventHandler(this.btnGetBuildingHeights_Click);
            // 
            // dgvHeights
            // 
            this.dgvHeights.AllowUserToAddRows = false;
            this.dgvHeights.AllowUserToDeleteRows = false;
            this.dgvHeights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHeights.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column3,
            this.Column1,
            this.Column4,
            this.Column5});
            this.dgvHeights.Location = new System.Drawing.Point(3, 116);
            this.dgvHeights.MultiSelect = false;
            this.dgvHeights.Name = "dgvHeights";
            this.dgvHeights.Size = new System.Drawing.Size(917, 203);
            this.dgvHeights.TabIndex = 114;
            this.dgvHeights.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHeights_CellContentClick);
            this.dgvHeights.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHeights_CellValueChanged);
            this.dgvHeights.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvHeights_RowPostPaint);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "No.";
            this.Column2.Name = "Column2";
            this.Column2.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Date Time";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Height";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Bounding Box";
            this.Column3.Name = "Column3";
            this.Column3.Width = 260;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Grid Coordinate System";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Jump To Building";
            this.Column4.Name = "Column4";
            this.Column4.Text = "...";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Show Surface";
            this.Column5.Name = "Column5";
            // 
            // chxStay
            // 
            this.chxStay.AutoSize = true;
            this.chxStay.Checked = true;
            this.chxStay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxStay.Location = new System.Drawing.Point(739, 88);
            this.chxStay.Name = "chxStay";
            this.chxStay.Size = new System.Drawing.Size(181, 17);
            this.chxStay.TabIndex = 128;
            this.chxStay.Text = "Stay Surface Objects On Closure";
            this.chxStay.UseVisualStyleBackColor = true;
            // 
            // chxTextOrientationFromNormal
            // 
            this.chxTextOrientationFromNormal.AutoSize = true;
            this.chxTextOrientationFromNormal.Checked = true;
            this.chxTextOrientationFromNormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTextOrientationFromNormal.Location = new System.Drawing.Point(739, 65);
            this.chxTextOrientationFromNormal.Name = "chxTextOrientationFromNormal";
            this.chxTextOrientationFromNormal.Size = new System.Drawing.Size(163, 17);
            this.chxTextOrientationFromNormal.TabIndex = 129;
            this.chxTextOrientationFromNormal.Text = "Text Orientation From Normal";
            this.chxTextOrientationFromNormal.UseVisualStyleBackColor = true;
            this.chxTextOrientationFromNormal.CheckedChanged += new System.EventHandler(this.chxTextOrientationFromNormal_CheckedChanged);
            // 
            // ScanItemsFoundFormHistoryBuildings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 327);
            this.Controls.Add(this.chxTextOrientationFromNormal);
            this.Controls.Add(this.chxStay);
            this.Controls.Add(this.btnGetBuildingHeights);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvHeights);
            this.Controls.Add(this.tbTargetId);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ScanItemsFoundFormHistoryBuildings";
            this.Text = "3D Model Smart Reality Data";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScanItemsFoundFormHistoryBuildings_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeights)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTargetId;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetBuildingHeights;
        private System.Windows.Forms.DataGridView dgvHeights;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.CheckBox chxStay;
        private System.Windows.Forms.CheckBox chxTextOrientationFromNormal;
    }
}