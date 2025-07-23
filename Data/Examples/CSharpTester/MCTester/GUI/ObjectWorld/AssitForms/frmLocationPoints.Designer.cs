namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmLocationPoints
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
            this.dgvItemLocationPoints = new System.Windows.Forms.DataGridView();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lstSchemes = new System.Windows.Forms.ListBox();
            this.gbSchemes = new System.Windows.Forms.GroupBox();
            this.gbItems = new System.Windows.Forms.GroupBox();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.chxLocationRelativeToDTM = new System.Windows.Forms.CheckBox();
            this.cmbLocationCoordinateSystem = new System.Windows.Forms.ComboBox();
            this.lblLocationCoordSystem = new System.Windows.Forms.Label();
            this.gbOverlayManager = new System.Windows.Forms.GroupBox();
            this.lstOverlayManagers = new System.Windows.Forms.ListBox();
            this.lstOverlays = new System.Windows.Forms.ListBox();
            this.gbOverlays = new System.Windows.Forms.GroupBox();
            this.ctrlSamplePointLocation = new MCTester.Controls.CtrlSamplePoint();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemLocationPoints)).BeginInit();
            this.gbSchemes.SuspendLayout();
            this.gbItems.SuspendLayout();
            this.gbOverlayManager.SuspendLayout();
            this.gbOverlays.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvItemLocationPoints
            // 
            this.dgvItemLocationPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemLocationPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.X,
            this.Y,
            this.Z});
            this.dgvItemLocationPoints.Location = new System.Drawing.Point(11, 452);
            this.dgvItemLocationPoints.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvItemLocationPoints.Name = "dgvItemLocationPoints";
            this.dgvItemLocationPoints.Size = new System.Drawing.Size(479, 215);
            this.dgvItemLocationPoints.TabIndex = 0;
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
            // lstSchemes
            // 
            this.lstSchemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSchemes.FormattingEnabled = true;
            this.lstSchemes.HorizontalScrollbar = true;
            this.lstSchemes.ItemHeight = 16;
            this.lstSchemes.Location = new System.Drawing.Point(4, 19);
            this.lstSchemes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstSchemes.Name = "lstSchemes";
            this.lstSchemes.Size = new System.Drawing.Size(229, 233);
            this.lstSchemes.TabIndex = 1;
            this.lstSchemes.SelectedIndexChanged += new System.EventHandler(this.lstSchemes_SelectedIndexChanged);
            // 
            // gbSchemes
            // 
            this.gbSchemes.Controls.Add(this.lstSchemes);
            this.gbSchemes.Location = new System.Drawing.Point(7, 127);
            this.gbSchemes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbSchemes.Name = "gbSchemes";
            this.gbSchemes.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbSchemes.Size = new System.Drawing.Size(237, 256);
            this.gbSchemes.TabIndex = 2;
            this.gbSchemes.TabStop = false;
            this.gbSchemes.Text = "Schemes";
            // 
            // gbItems
            // 
            this.gbItems.Controls.Add(this.lstItems);
            this.gbItems.Location = new System.Drawing.Point(252, 127);
            this.gbItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbItems.Name = "gbItems";
            this.gbItems.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbItems.Size = new System.Drawing.Size(237, 256);
            this.gbItems.TabIndex = 4;
            this.gbItems.TabStop = false;
            this.gbItems.Text = "Standalone Items";
            // 
            // lstItems
            // 
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.FormattingEnabled = true;
            this.lstItems.HorizontalScrollbar = true;
            this.lstItems.ItemHeight = 16;
            this.lstItems.Location = new System.Drawing.Point(4, 19);
            this.lstItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(229, 233);
            this.lstItems.TabIndex = 1;
            this.lstItems.SelectedIndexChanged += new System.EventHandler(this.lstItems_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(389, 674);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chxLocationRelativeToDTM
            // 
            this.chxLocationRelativeToDTM.AutoSize = true;
            this.chxLocationRelativeToDTM.Location = new System.Drawing.Point(11, 390);
            this.chxLocationRelativeToDTM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chxLocationRelativeToDTM.Name = "chxLocationRelativeToDTM";
            this.chxLocationRelativeToDTM.Size = new System.Drawing.Size(194, 21);
            this.chxLocationRelativeToDTM.TabIndex = 6;
            this.chxLocationRelativeToDTM.Text = "Location Relative To DTM";
            this.chxLocationRelativeToDTM.UseVisualStyleBackColor = true;
            // 
            // cmbLocationCoordinateSystem
            // 
            this.cmbLocationCoordinateSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationCoordinateSystem.FormattingEnabled = true;
            this.cmbLocationCoordinateSystem.Location = new System.Drawing.Point(204, 418);
            this.cmbLocationCoordinateSystem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLocationCoordinateSystem.Name = "cmbLocationCoordinateSystem";
            this.cmbLocationCoordinateSystem.Size = new System.Drawing.Size(284, 24);
            this.cmbLocationCoordinateSystem.TabIndex = 7;
            this.cmbLocationCoordinateSystem.SelectedIndexChanged += new System.EventHandler(this.cmbLocationCoordinateSystem_SelectedIndexChanged);
            // 
            // lblLocationCoordSystem
            // 
            this.lblLocationCoordSystem.AutoSize = true;
            this.lblLocationCoordSystem.Location = new System.Drawing.Point(7, 422);
            this.lblLocationCoordSystem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocationCoordSystem.Name = "lblLocationCoordSystem";
            this.lblLocationCoordSystem.Size = new System.Drawing.Size(189, 17);
            this.lblLocationCoordSystem.TabIndex = 8;
            this.lblLocationCoordSystem.Text = "Location Coordinate System:";
            // 
            // gbOverlayManager
            // 
            this.gbOverlayManager.Controls.Add(this.lstOverlayManagers);
            this.gbOverlayManager.Location = new System.Drawing.Point(7, 15);
            this.gbOverlayManager.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOverlayManager.Name = "gbOverlayManager";
            this.gbOverlayManager.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOverlayManager.Size = new System.Drawing.Size(237, 105);
            this.gbOverlayManager.TabIndex = 10;
            this.gbOverlayManager.TabStop = false;
            this.gbOverlayManager.Text = "Overlay Manager";
            // 
            // lstOverlayManagers
            // 
            this.lstOverlayManagers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOverlayManagers.FormattingEnabled = true;
            this.lstOverlayManagers.HorizontalScrollbar = true;
            this.lstOverlayManagers.ItemHeight = 16;
            this.lstOverlayManagers.Location = new System.Drawing.Point(4, 19);
            this.lstOverlayManagers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstOverlayManagers.Name = "lstOverlayManagers";
            this.lstOverlayManagers.Size = new System.Drawing.Size(229, 82);
            this.lstOverlayManagers.TabIndex = 1;
            this.lstOverlayManagers.SelectedIndexChanged += new System.EventHandler(this.lstOverlayManagers_SelectedIndexChanged);
            // 
            // lstOverlays
            // 
            this.lstOverlays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOverlays.FormattingEnabled = true;
            this.lstOverlays.HorizontalScrollbar = true;
            this.lstOverlays.ItemHeight = 16;
            this.lstOverlays.Location = new System.Drawing.Point(4, 19);
            this.lstOverlays.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstOverlays.Name = "lstOverlays";
            this.lstOverlays.Size = new System.Drawing.Size(229, 82);
            this.lstOverlays.TabIndex = 1;
            this.lstOverlays.SelectedIndexChanged += new System.EventHandler(this.lstOverlays_SelectedIndexChanged);
            // 
            // gbOverlays
            // 
            this.gbOverlays.Controls.Add(this.lstOverlays);
            this.gbOverlays.Location = new System.Drawing.Point(252, 15);
            this.gbOverlays.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOverlays.Name = "gbOverlays";
            this.gbOverlays.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOverlays.Size = new System.Drawing.Size(237, 105);
            this.gbOverlays.TabIndex = 3;
            this.gbOverlays.TabStop = false;
            this.gbOverlays.Text = "Destination Overlay";
            // 
            // ctrlSamplePointLocation
            // 
            this.ctrlSamplePointLocation._DgvControlName = "dgvItemLocationPoints";
            this.ctrlSamplePointLocation._PointInOverlayManagerCoordSys = true;
            this.ctrlSamplePointLocation._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointLocation._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointLocation._SampleOnePoint = false;
            this.ctrlSamplePointLocation._UserControlName = null;
            this.ctrlSamplePointLocation.Location = new System.Drawing.Point(11, 674);
            this.ctrlSamplePointLocation.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlSamplePointLocation.Name = "ctrlSamplePointLocation";
            this.ctrlSamplePointLocation.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointLocation.Size = new System.Drawing.Size(49, 28);
            this.ctrlSamplePointLocation.TabIndex = 9;
            this.ctrlSamplePointLocation.Text = "...";
            this.ctrlSamplePointLocation.UseVisualStyleBackColor = true;
            // 
            // frmLocationPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(499, 711);
            this.Controls.Add(this.gbOverlayManager);
            this.Controls.Add(this.ctrlSamplePointLocation);
            this.Controls.Add(this.lblLocationCoordSystem);
            this.Controls.Add(this.cmbLocationCoordinateSystem);
            this.Controls.Add(this.chxLocationRelativeToDTM);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbOverlays);
            this.Controls.Add(this.gbItems);
            this.Controls.Add(this.gbSchemes);
            this.Controls.Add(this.dgvItemLocationPoints);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmLocationPoints";
            this.Text = "frmLocationPoints";
            this.Load += new System.EventHandler(this.frmLocationPoints_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemLocationPoints)).EndInit();
            this.gbSchemes.ResumeLayout(false);
            this.gbItems.ResumeLayout(false);
            this.gbOverlayManager.ResumeLayout(false);
            this.gbOverlays.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvItemLocationPoints;
        private System.Windows.Forms.ListBox lstSchemes;
        public System.Windows.Forms.GroupBox gbSchemes;
        public System.Windows.Forms.GroupBox gbItems;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chxLocationRelativeToDTM;
        private System.Windows.Forms.ComboBox cmbLocationCoordinateSystem;
        private System.Windows.Forms.Label lblLocationCoordSystem;
        private MCTester.Controls.CtrlSamplePoint ctrlSamplePointLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        public System.Windows.Forms.GroupBox gbOverlayManager;
        private System.Windows.Forms.ListBox lstOverlayManagers;
        private System.Windows.Forms.ListBox lstOverlays;
        public System.Windows.Forms.GroupBox gbOverlays;
    }
}