namespace MCTester.General_Forms
{
    partial class ScanItemsFoundFormDetails
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvVectorItems = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ntxNumLocationPoints = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlUnifiedVectorItemsPoints = new MCTester.Controls.CtrlPointsGrid();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVectorItems)).BeginInit();
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
            this.label1.Text = "Target ID:";
            // 
            // tbTargetId
            // 
            this.tbTargetId.Location = new System.Drawing.Point(69, 16);
            this.tbTargetId.Margin = new System.Windows.Forms.Padding(2);
            this.tbTargetId.Name = "tbTargetId";
            this.tbTargetId.ReadOnly = true;
            this.tbTargetId.Size = new System.Drawing.Size(417, 20);
            this.tbTargetId.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(6, 176);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "Unified Vector Items Points";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(7, 53);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 115;
            this.label5.Text = "Vector Items";
            // 
            // dgvVectorItems
            // 
            this.dgvVectorItems.AllowDrop = true;
            this.dgvVectorItems.AllowUserToAddRows = false;
            this.dgvVectorItems.AllowUserToDeleteRows = false;
            this.dgvVectorItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVectorItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvVectorItems.Location = new System.Drawing.Point(9, 69);
            this.dgvVectorItems.Name = "dgvVectorItems";
            this.dgvVectorItems.ReadOnly = true;
            this.dgvVectorItems.Size = new System.Drawing.Size(478, 90);
            this.dgvVectorItems.TabIndex = 114;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "No.";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Vector Item ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Vector Item First Point Index";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "Vector Item Last Point Index";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // ntxNumLocationPoints
            // 
            this.ntxNumLocationPoints.Enabled = false;
            this.ntxNumLocationPoints.Location = new System.Drawing.Point(104, 395);
            this.ntxNumLocationPoints.Name = "ntxNumLocationPoints";
            this.ntxNumLocationPoints.Size = new System.Drawing.Size(73, 20);
            this.ntxNumLocationPoints.TabIndex = 122;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(7, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 123;
            this.label6.Text = "Number of points:";
            // 
            // ctrlUnifiedVectorItemsPoints
            // 
            this.ctrlUnifiedVectorItemsPoints.Location = new System.Drawing.Point(10, 193);
            this.ctrlUnifiedVectorItemsPoints.Name = "ctrlUnifiedVectorItemsPoints";
            this.ctrlUnifiedVectorItemsPoints.Size = new System.Drawing.Size(345, 196);
            this.ctrlUnifiedVectorItemsPoints.TabIndex = 124;
            // 
            // ScanItemsFoundFormDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 444);
            this.Controls.Add(this.ctrlUnifiedVectorItemsPoints);
            this.Controls.Add(this.ntxNumLocationPoints);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvVectorItems);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbTargetId);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ScanItemsFoundFormDetails";
            this.Text = "Scan Item Details";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVectorItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTargetId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvVectorItems;
        private Controls.NumericTextBox ntxNumLocationPoints;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Controls.CtrlPointsGrid ctrlUnifiedVectorItemsPoints;
    }
}