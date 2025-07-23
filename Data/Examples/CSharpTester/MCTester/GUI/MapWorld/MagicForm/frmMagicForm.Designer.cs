namespace MCTester.ButtonsImplementation
{
    partial class frmMagicForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvLayers = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.btnRemoveLayers = new System.Windows.Forms.Button();
            this.btnSelectFromExistLayers = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ctrlDeviceParams1 = new MCTester.Controls.CtrlDeviceParams();
            this.ctrlSmallDeviceParams1 = new MCTester.ButtonsImplementation.ctrlSmallDeviceParams();
            this.ctrlSmallViewportParams1 = new MCTester.MapWorld.MagicForm.ctrlSmallViewportParams();
            this.ctrlGridCoordinateSystemRawLayers = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucLayerParams1 = new MCTester.Controls.ucLayerParams();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.addLayerCtrl1 = new MCTester.MapWorld.WizardForms.AddLayerCtrl();
            this.btnCreateMap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(160, 629);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(228, 46);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 400;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            this.Column3.Text = "...";
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Text = "Params";
            // 
            // dgvLayers
            // 
            this.dgvLayers.AllowUserToAddRows = false;
            this.dgvLayers.AllowUserToDeleteRows = false;
            this.dgvLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column5});
            this.dgvLayers.Location = new System.Drawing.Point(15, 17);
            this.dgvLayers.MultiSelect = false;
            this.dgvLayers.Name = "dgvLayers";
            this.dgvLayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLayers.Size = new System.Drawing.Size(925, 113);
            this.dgvLayers.TabIndex = 19;
            this.dgvLayers.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView2_RowPostPaint);
            this.dgvLayers.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.Width = 40;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Layer Data";
            this.Column5.Name = "Column5";
            // 
            // btnAddLayer
            // 
            this.btnAddLayer.Location = new System.Drawing.Point(957, 40);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(75, 23);
            this.btnAddLayer.TabIndex = 20;
            this.btnAddLayer.Text = "Add";
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);
            // 
            // btnRemoveLayers
            // 
            this.btnRemoveLayers.Location = new System.Drawing.Point(957, 69);
            this.btnRemoveLayers.Name = "btnRemoveLayers";
            this.btnRemoveLayers.Size = new System.Drawing.Size(110, 23);
            this.btnRemoveLayers.TabIndex = 21;
            this.btnRemoveLayers.Text = "Remove Selected";
            this.btnRemoveLayers.UseVisualStyleBackColor = true;
            this.btnRemoveLayers.Click += new System.EventHandler(this.btnRemoveLayers_Click);
            // 
            // btnSelectFromExistLayers
            // 
            this.btnSelectFromExistLayers.Location = new System.Drawing.Point(957, 98);
            this.btnSelectFromExistLayers.Name = "btnSelectFromExistLayers";
            this.btnSelectFromExistLayers.Size = new System.Drawing.Size(147, 23);
            this.btnSelectFromExistLayers.TabIndex = 22;
            this.btnSelectFromExistLayers.Text = "Select From Exist Layers";
            this.btnSelectFromExistLayers.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1233, 871);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrlDeviceParams1);
            this.tabPage1.Controls.Add(this.ctrlSmallDeviceParams1);
            this.tabPage1.Controls.Add(this.ctrlSmallViewportParams1);
            this.tabPage1.Controls.Add(this.ctrlGridCoordinateSystemRawLayers);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1225, 845);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General Params";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrlDeviceParams1
            // 
            this.ctrlDeviceParams1.Location = new System.Drawing.Point(6, 7);
            this.ctrlDeviceParams1.Name = "ctrlDeviceParams1";
            this.ctrlDeviceParams1.Size = new System.Drawing.Size(322, 717);
            this.ctrlDeviceParams1.TabIndex = 18;
            // 
            // ctrlSmallDeviceParams1
            // 
            this.ctrlSmallDeviceParams1.Location = new System.Drawing.Point(334, 7);
            this.ctrlSmallDeviceParams1.Name = "ctrlSmallDeviceParams1";
            this.ctrlSmallDeviceParams1.Size = new System.Drawing.Size(468, 133);
            this.ctrlSmallDeviceParams1.TabIndex = 16;
            // 
            // ctrlSmallViewportParams1
            // 
            this.ctrlSmallViewportParams1.Location = new System.Drawing.Point(334, 146);
            this.ctrlSmallViewportParams1.Name = "ctrlSmallViewportParams1";
            this.ctrlSmallViewportParams1.Size = new System.Drawing.Size(472, 225);
            this.ctrlSmallViewportParams1.TabIndex = 17;
            // 
            // ctrlGridCoordinateSystemRawLayers
            // 
            this.ctrlGridCoordinateSystemRawLayers.EnableNewCoordSysCreation = true;
            this.ctrlGridCoordinateSystemRawLayers.GridCoordinateSystem = null;
            this.ctrlGridCoordinateSystemRawLayers.GroupBoxText = "Grid Coordinate System For Raw Layers And Viewport (Mandatory Without Native Laye" +
    "rs)";
            this.ctrlGridCoordinateSystemRawLayers.IsEditable = false;
            this.ctrlGridCoordinateSystemRawLayers.Location = new System.Drawing.Point(334, 378);
            this.ctrlGridCoordinateSystemRawLayers.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlGridCoordinateSystemRawLayers.Name = "ctrlGridCoordinateSystemRawLayers";
            this.ctrlGridCoordinateSystemRawLayers.Size = new System.Drawing.Size(468, 132);
            this.ctrlGridCoordinateSystemRawLayers.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucLayerParams1);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.btnSelectFromExistLayers);
            this.tabPage2.Controls.Add(this.dgvLayers);
            this.tabPage2.Controls.Add(this.btnRemoveLayers);
            this.tabPage2.Controls.Add(this.btnAddLayer);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1225, 845);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Layers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucLayerParams1
            // 
            this.ucLayerParams1.Location = new System.Drawing.Point(6, 147);
            this.ucLayerParams1.Name = "ucLayerParams1";
            this.ucLayerParams1.Size = new System.Drawing.Size(770, 389);
            this.ucLayerParams1.TabIndex = 18;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.addLayerCtrl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1225, 845);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Layers 2";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // addLayerCtrl1
            // 
            this.addLayerCtrl1.Location = new System.Drawing.Point(6, 4);
            this.addLayerCtrl1.Name = "addLayerCtrl1";
            this.addLayerCtrl1.Size = new System.Drawing.Size(1132, 826);
            this.addLayerCtrl1.TabIndex = 0;
            this.addLayerCtrl1.UserAction = MCTester.MapWorld.WizardForms.AddLayerCtrl.EUserAction.CreateNewLayer;
            // 
            // btnCreateMap
            // 
            this.btnCreateMap.Location = new System.Drawing.Point(554, 886);
            this.btnCreateMap.Name = "btnCreateMap";
            this.btnCreateMap.Size = new System.Drawing.Size(137, 23);
            this.btnCreateMap.TabIndex = 24;
            this.btnCreateMap.Text = "Create Map";
            this.btnCreateMap.UseVisualStyleBackColor = true;
            this.btnCreateMap.Click += new System.EventHandler(this.btnCreateMap_Click);
            // 
            // frmMagicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 921);
            this.Controls.Add(this.btnCreateMap);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMagicForm";
            this.Text = "btnCreateLayerForm2";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayers)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private Controls.CtrlGridCoordinateSystem ctrlGridCoordinateSystemRawLayers;
        private ctrlSmallDeviceParams ctrlSmallDeviceParams1;
        private MapWorld.MagicForm.ctrlSmallViewportParams ctrlSmallViewportParams1;
        private Controls.ucLayerParams ucLayerParams1;
        private System.Windows.Forms.DataGridView dgvLayers;
        private System.Windows.Forms.Button btnAddLayer;
        private System.Windows.Forms.Button btnRemoveLayers;
        private System.Windows.Forms.Button btnSelectFromExistLayers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Controls.CtrlDeviceParams ctrlDeviceParams1;
        private System.Windows.Forms.TabPage tabPage3;
        private MapWorld.WizardForms.AddLayerCtrl addLayerCtrl1;
        private System.Windows.Forms.Button btnCreateMap;
    }
}