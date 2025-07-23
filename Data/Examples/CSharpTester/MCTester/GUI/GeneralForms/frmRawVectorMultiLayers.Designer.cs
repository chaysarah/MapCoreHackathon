namespace MCTester.General_Forms
{
    partial class frmRawVectorMultiLayers
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
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvLayers = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chxSuffixByName = new System.Windows.Forms.CheckBox();
            this.chxSelectAll = new System.Windows.Forms.CheckBox();
            this.chxCreateSingleVectorLayerFromAllSublayers = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTakeWholeDataSource
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(1162, 306);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(83, 23);
            this.btnOK.TabIndex = 46;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgvLayers
            // 
            this.dgvLayers.AllowUserToAddRows = false;
            this.dgvLayers.AllowUserToDeleteRows = false;
            this.dgvLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvLayers.EnableHeadersVisualStyles = false;
            this.dgvLayers.Location = new System.Drawing.Point(3, 7);
            this.dgvLayers.Name = "dgvLayers";
            this.dgvLayers.Size = new System.Drawing.Size(1241, 293);
            this.dgvLayers.TabIndex = 47;
            this.dgvLayers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLayers_CellContentClick);
            this.dgvLayers.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvLayers_CellPainting);
            this.dgvLayers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLayers_CellValueChanged);
            this.dgvLayers.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvLayers_RowPostPaint);
            this.dgvLayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvLayers_MouseDoubleClick);
            // 
            // Column8
            // 
            this.Column8.HeaderText = "";
            this.Column8.Name = "Column8";
            this.Column8.Width = 40;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Sub-layer No.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Sub-layer Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Sub-layer Type";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Sub-layer Suffix";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "No. of Items";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Styling File Path";
            this.Column6.Name = "Column6";
            this.Column6.Width = 400;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "";
            this.Column7.Name = "Column7";
            this.Column7.Width = 30;
            // 
            // chxSuffixByName
            // 
            this.chxSuffixByName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chxSuffixByName.AutoSize = true;
            this.chxSuffixByName.Checked = true;
            this.chxSuffixByName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSuffixByName.Location = new System.Drawing.Point(3, 310);
            this.chxSuffixByName.Name = "chxSuffixByName";
            this.chxSuffixByName.Size = new System.Drawing.Size(98, 17);
            this.chxSuffixByName.TabIndex = 51;
            this.chxSuffixByName.Text = "Suffix By Name";
            this.chxSuffixByName.UseVisualStyleBackColor = true;
            this.chxSuffixByName.CheckedChanged += new System.EventHandler(this.chxSuffixByName_CheckedChanged);
            // 
            // chxSelectAll
            // 
            this.chxSelectAll.AutoSize = true;
            this.chxSelectAll.Location = new System.Drawing.Point(58, 11);
            this.chxSelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.chxSelectAll.Name = "chxSelectAll";
            this.chxSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chxSelectAll.TabIndex = 53;
            this.chxSelectAll.UseVisualStyleBackColor = true;
            this.chxSelectAll.CheckedChanged += new System.EventHandler(this.chxSelectAll_CheckedChanged);
            // 
            // chxRawVectorMapLayer
            // 
            this.chxCreateSingleVectorLayerFromAllSublayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chxCreateSingleVectorLayerFromAllSublayers.AutoSize = true;
            this.chxCreateSingleVectorLayerFromAllSublayers.Location = new System.Drawing.Point(912, 310);
            this.chxCreateSingleVectorLayerFromAllSublayers.Name = "chxCreateSingleVectorLayerFromAllSublayers";
            this.chxCreateSingleVectorLayerFromAllSublayers.Size = new System.Drawing.Size(241, 17);
            this.chxCreateSingleVectorLayerFromAllSublayers.TabIndex = 54;
            this.chxCreateSingleVectorLayerFromAllSublayers.Text = "Create Single Vector Layer from All Sub-layers";
            this.chxCreateSingleVectorLayerFromAllSublayers.UseVisualStyleBackColor = true;
            this.chxCreateSingleVectorLayerFromAllSublayers.CheckedChanged += new System.EventHandler(this.chxCreateSingleVectorLayerFromAllSublayers_CheckedChanged);
            // 
            // frmRawVectorMultiLayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1256, 331);
            this.Controls.Add(this.chxCreateSingleVectorLayerFromAllSublayers);
            this.Controls.Add(this.chxSuffixByName);
            this.Controls.Add(this.chxSelectAll);
            this.Controls.Add(this.dgvLayers);
            this.Controls.Add(this.btnOK);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmRawVectorMultiLayers";
            this.Text = "Data Source Sub-layers";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dgvLayers;
        private System.Windows.Forms.CheckBox chxSuffixByName;
        private System.Windows.Forms.CheckBox chxSelectAll;
        private System.Windows.Forms.CheckBox chxCreateSingleVectorLayerFromAllSublayers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
    }
}