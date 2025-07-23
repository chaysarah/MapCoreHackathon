namespace MCTester.MapWorld.WebMapLayers
{
    partial class FrmWebMapLayers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWebMapLayers));
            this.dgvWebLayers = new System.Windows.Forms.DataGridView();
            this.Num = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Metadata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOpenSelectedLayers = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnRefreshFromServer = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.chxSelectAll = new System.Windows.Forms.CheckBox();
            this.pbRefreshIcon = new System.Windows.Forms.PictureBox();
            this.chxOpenAllLayersAsOne = new System.Windows.Forms.CheckBox();
            this.gbSelectLayerType = new System.Windows.Forms.GroupBox();
            this.rbDTM = new System.Windows.Forms.RadioButton();
            this.rbRaster = new System.Windows.Forms.RadioButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pbSearch = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlWebServiceLayerParams1 = new MCTester.MapWorld.WebMapLayers.CtrlWebServiceLayerParams();
            this.ctrlNonNativeParams1 = new MCTester.MapWorld.MapUserControls.CtrlNonNativeParams();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWebLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshIcon)).BeginInit();
            this.gbSelectLayerType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvWebLayers
            // 
            this.dgvWebLayers.AllowUserToAddRows = false;
            this.dgvWebLayers.AllowUserToDeleteRows = false;
            this.dgvWebLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWebLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWebLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num,
            this.Column1,
            this.Select,
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column7,
            this.Column6,
            this.Metadata});
            this.dgvWebLayers.Location = new System.Drawing.Point(-1, -2);
            this.dgvWebLayers.Margin = new System.Windows.Forms.Padding(2);
            this.dgvWebLayers.MultiSelect = false;
            this.dgvWebLayers.Name = "dgvWebLayers";
            this.dgvWebLayers.RowTemplate.Height = 24;
            this.dgvWebLayers.Size = new System.Drawing.Size(805, 329);
            this.dgvWebLayers.TabIndex = 0;
            this.dgvWebLayers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWebLayers_CellContentClick);
            this.dgvWebLayers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWebLayers_CellValueChanged);
            this.dgvWebLayers.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvWebLayers_Paint);
            // 
            // Num
            // 
            this.Num.HeaderText = "";
            this.Num.Name = "Num";
            this.Num.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Num.Width = 50;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Group";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Select
            // 
            this.Select.HeaderText = "";
            this.Select.Name = "Select";
            this.Select.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Identifier";
            this.Column4.Name = "Column4";
            this.Column4.Width = 220;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Title";
            this.Column2.Name = "Column2";
            this.Column2.Width = 220;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Type";
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Coordinate System";
            this.Column5.Name = "Column5";
            this.Column5.Width = 170;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Bounding Box";
            this.Column7.Name = "Column7";
            this.Column7.Width = 300;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Draw Priority";
            this.Column6.Name = "Column6";
            this.Column6.Width = 90;
            // 
            // Metadata
            // 
            this.Metadata.HeaderText = "Metadata";
            this.Metadata.Name = "Metadata";
            this.Metadata.Width = 300;
            // 
            // btnOpenSelectedLayers
            // 
            this.btnOpenSelectedLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSelectedLayers.Location = new System.Drawing.Point(808, 2);
            this.btnOpenSelectedLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenSelectedLayers.Name = "btnOpenSelectedLayers";
            this.btnOpenSelectedLayers.Size = new System.Drawing.Size(84, 27);
            this.btnOpenSelectedLayers.TabIndex = 1;
            this.btnOpenSelectedLayers.Text = "OK";
            this.btnOpenSelectedLayers.UseVisualStyleBackColor = true;
            this.btnOpenSelectedLayers.Click += new System.EventHandler(this.btnOpenSelectedLayers_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(829, 285);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(61, 21);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "None";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Visible = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnRefreshFromServer
            // 
            this.btnRefreshFromServer.Location = new System.Drawing.Point(829, 311);
            this.btnRefreshFromServer.Margin = new System.Windows.Forms.Padding(2);
            this.btnRefreshFromServer.Name = "btnRefreshFromServer";
            this.btnRefreshFromServer.Size = new System.Drawing.Size(61, 21);
            this.btnRefreshFromServer.TabIndex = 3;
            this.btnRefreshFromServer.Text = "Refresh";
            this.btnRefreshFromServer.UseVisualStyleBackColor = true;
            this.btnRefreshFromServer.Visible = false;
            this.btnRefreshFromServer.Click += new System.EventHandler(this.btnRefreshFromServer_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(829, 259);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(61, 21);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Visible = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // chxSelectAll
            // 
            this.chxSelectAll.AutoSize = true;
            this.chxSelectAll.Location = new System.Drawing.Point(44, 2);
            this.chxSelectAll.Margin = new System.Windows.Forms.Padding(2);
            this.chxSelectAll.Name = "chxSelectAll";
            this.chxSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chxSelectAll.TabIndex = 5;
            this.chxSelectAll.UseVisualStyleBackColor = true;
            this.chxSelectAll.CheckedChanged += new System.EventHandler(this.chxSelectAll_CheckedChanged);
            // 
            // pbRefreshIcon
            // 
            this.pbRefreshIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbRefreshIcon.Image")));
            this.pbRefreshIcon.Location = new System.Drawing.Point(9, 0);
            this.pbRefreshIcon.Margin = new System.Windows.Forms.Padding(2);
            this.pbRefreshIcon.Name = "pbRefreshIcon";
            this.pbRefreshIcon.Size = new System.Drawing.Size(16, 16);
            this.pbRefreshIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbRefreshIcon.TabIndex = 6;
            this.pbRefreshIcon.TabStop = false;
            this.pbRefreshIcon.Click += new System.EventHandler(this.pbRefreshIcon_Click);
            // 
            // chxOpenAllLayersAsOne
            // 
            this.chxOpenAllLayersAsOne.AutoSize = true;
            this.chxOpenAllLayersAsOne.Location = new System.Drawing.Point(673, 346);
            this.chxOpenAllLayersAsOne.Name = "chxOpenAllLayersAsOne";
            this.chxOpenAllLayersAsOne.Size = new System.Drawing.Size(138, 17);
            this.chxOpenAllLayersAsOne.TabIndex = 9;
            this.chxOpenAllLayersAsOne.Text = "Open All Layers As One";
            this.chxOpenAllLayersAsOne.UseVisualStyleBackColor = true;
            this.chxOpenAllLayersAsOne.CheckedChanged += new System.EventHandler(this.chxOpenAllLayersAsOne_CheckedChanged);
            // 
            // gbSelectLayerType
            // 
            this.gbSelectLayerType.Controls.Add(this.rbDTM);
            this.gbSelectLayerType.Controls.Add(this.rbRaster);
            this.gbSelectLayerType.Location = new System.Drawing.Point(673, 377);
            this.gbSelectLayerType.Name = "gbSelectLayerType";
            this.gbSelectLayerType.Size = new System.Drawing.Size(126, 68);
            this.gbSelectLayerType.TabIndex = 10;
            this.gbSelectLayerType.TabStop = false;
            this.gbSelectLayerType.Text = "Select Layer Type";
            // 
            // rbDTM
            // 
            this.rbDTM.AutoSize = true;
            this.rbDTM.Location = new System.Drawing.Point(7, 43);
            this.rbDTM.Name = "rbDTM";
            this.rbDTM.Size = new System.Drawing.Size(49, 17);
            this.rbDTM.TabIndex = 1;
            this.rbDTM.TabStop = true;
            this.rbDTM.Text = "DTM";
            this.rbDTM.UseVisualStyleBackColor = true;
            // 
            // rbRaster
            // 
            this.rbRaster.AutoSize = true;
            this.rbRaster.Location = new System.Drawing.Point(7, 20);
            this.rbRaster.Name = "rbRaster";
            this.rbRaster.Size = new System.Drawing.Size(56, 17);
            this.rbRaster.TabIndex = 0;
            this.rbRaster.TabStop = true;
            this.rbRaster.Text = "Raster";
            this.rbRaster.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(808, 57);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(84, 20);
            this.txtSearch.TabIndex = 11;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(808, 216);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 27);
            this.btnSearch.TabIndex = 12;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // pbSearch
            // 
            this.pbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSearch.Image = ((System.Drawing.Image)(resources.GetObject("pbSearch.Image")));
            this.pbSearch.Location = new System.Drawing.Point(812, 181);
            this.pbSearch.Name = "pbSearch";
            this.pbSearch.Size = new System.Drawing.Size(24, 21);
            this.pbSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSearch.TabIndex = 13;
            this.pbSearch.TabStop = false;
            this.pbSearch.Visible = false;
            this.pbSearch.Click += new System.EventHandler(this.pbSearch_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(806, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Filter Layers:";
            // 
            // ctrlWebServiceLayerParams1
            // 
            this.ctrlWebServiceLayerParams1.Location = new System.Drawing.Point(4, 476);
            this.ctrlWebServiceLayerParams1.Name = "ctrlWebServiceLayerParams1";
            this.ctrlWebServiceLayerParams1.Size = new System.Drawing.Size(832, 377);
            this.ctrlWebServiceLayerParams1.TabIndex = 8;
            // 
            // ctrlNonNativeParams1
            // 
            this.ctrlNonNativeParams1.Location = new System.Drawing.Point(5, 332);
            this.ctrlNonNativeParams1.Name = "ctrlNonNativeParams1";
            this.ctrlNonNativeParams1.Size = new System.Drawing.Size(673, 145);
            this.ctrlNonNativeParams1.TabIndex = 7;
            // 
            // FrmWebMapLayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(923, 861);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.gbSelectLayerType);
            this.Controls.Add(this.chxOpenAllLayersAsOne);
            this.Controls.Add(this.ctrlWebServiceLayerParams1);
            this.Controls.Add(this.ctrlNonNativeParams1);
            this.Controls.Add(this.pbRefreshIcon);
            this.Controls.Add(this.chxSelectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnRefreshFromServer);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnOpenSelectedLayers);
            this.Controls.Add(this.dgvWebLayers);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmWebMapLayers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Map Layers";
            this.Load += new System.EventHandler(this.FrmWebMapLayers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWebLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRefreshIcon)).EndInit();
            this.gbSelectLayerType.ResumeLayout(false);
            this.gbSelectLayerType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvWebLayers;
        private System.Windows.Forms.Button btnOpenSelectedLayers;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnRefreshFromServer;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckBox chxSelectAll;
        private System.Windows.Forms.PictureBox pbRefreshIcon;
        private MapUserControls.CtrlNonNativeParams ctrlNonNativeParams1;
        private CtrlWebServiceLayerParams ctrlWebServiceLayerParams1;
        private System.Windows.Forms.CheckBox chxOpenAllLayersAsOne;
        private System.Windows.Forms.GroupBox gbSelectLayerType;
        private System.Windows.Forms.RadioButton rbDTM;
        private System.Windows.Forms.RadioButton rbRaster;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.PictureBox pbSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Num;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Metadata;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column10;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
    }
}