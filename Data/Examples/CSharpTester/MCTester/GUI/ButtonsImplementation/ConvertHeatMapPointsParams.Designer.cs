namespace MCTester.ButtonsImplementation
{
    partial class ConvertHeatMapPointsParams
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
            this.dgvPointsParams = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.colIntensity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNoPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoLocations = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPointsParams)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPointsParams
            // 
            this.dgvPointsParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPointsParams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIntensity,
            this.colNoPoints,
            this.NoLocations});
            this.dgvPointsParams.Location = new System.Drawing.Point(15, 38);
            this.dgvPointsParams.Name = "dgvPointsParams";
            this.dgvPointsParams.Size = new System.Drawing.Size(323, 183);
            this.dgvPointsParams.TabIndex = 0;
            this.dgvPointsParams.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPointsParams_CellContentClick);
            this.dgvPointsParams.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPointsParams_CellValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select intensity, no. points and no. locations:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(263, 227);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // colIntensity
            // 
            this.colIntensity.HeaderText = "Intensity(0-255)";
            this.colIntensity.Name = "colIntensity";
            this.colIntensity.Width = 85;
            // 
            // colNoPoints
            // 
            this.colNoPoints.HeaderText = "No. Points";
            this.colNoPoints.Name = "colNoPoints";
            this.colNoPoints.Width = 85;
            // 
            // NoLocations
            // 
            this.NoLocations.HeaderText = "No. Locations";
            this.NoLocations.Name = "NoLocations";
            // 
            // ConvertHeatMapPointsParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(350, 262);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPointsParams);
            this.Name = "ConvertHeatMapPointsParams";
            this.Text = "ConvertHeatMapPointsParams";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPointsParams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPointsParams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIntensity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNoPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoLocations;
    }
}