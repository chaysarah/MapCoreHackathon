namespace MCTester.MapWorld.MapUserControls
{
    partial class ucTerrain
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
            MapCore.DNSLayerParams dnsLayerParams1 = new MapCore.DNSLayerParams();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSetNumTilesInNativeServerRequest = new System.Windows.Forms.Button();
            this.ntxNumTilesInNativeServerRequest = new MCTester.Controls.NumericTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.ctrlBoundingBox = new MCTester.Controls.CtrlSMcBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lnkLayerName = new System.Windows.Forms.LinkLabel();
            this.label114 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.btnGetLayerByID = new System.Windows.Forms.Button();
            this.ntxGetLayerById = new MCTester.Controls.NumericTextBox();
            this.ctrlUserData = new MCTester.Controls.ctrlUserData();
            this.label4 = new System.Windows.Forms.Label();
            this.ntxTerrainID = new MCTester.Controls.NumericTextBox();
            this.ctrlGridCoordinateSystemDetails1 = new MCTester.Controls.CtrlGridCoordinateSystemDetails();
            this.chxDefaultVisibility = new System.Windows.Forms.CheckBox();
            this.clstViewports = new System.Windows.Forms.CheckedListBox();
            this.tpLayers = new System.Windows.Forms.TabPage();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoveAdd = new System.Windows.Forms.Button();
            this.lstLayersRemoveAdd = new System.Windows.Forms.ListBox();
            this.lstLayers = new System.Windows.Forms.ListBox();
            this.btnLayerOK = new System.Windows.Forms.Button();
            this.ctrlSLayerParams = new MCTester.Controls.CtrlSLayerParams();
            this.btnOK = new System.Windows.Forms.Button();
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tpLayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnSetNumTilesInNativeServerRequest);
            this.groupBox1.Controls.Add(this.ntxNumTilesInNativeServerRequest);
            this.groupBox1.Location = new System.Drawing.Point(11, 400);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 56);
            this.groupBox1.TabIndex = 94;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Num Tiles In Native Server Request For All Layers";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Num Tiles:";
            // 
            // btnSetNumTilesInNativeServerRequest
            // 
            this.btnSetNumTilesInNativeServerRequest.Location = new System.Drawing.Point(148, 25);
            this.btnSetNumTilesInNativeServerRequest.Name = "btnSetNumTilesInNativeServerRequest";
            this.btnSetNumTilesInNativeServerRequest.Size = new System.Drawing.Size(45, 20);
            this.btnSetNumTilesInNativeServerRequest.TabIndex = 2;
            this.btnSetNumTilesInNativeServerRequest.Text = "Set";
            this.btnSetNumTilesInNativeServerRequest.UseVisualStyleBackColor = true;
            this.btnSetNumTilesInNativeServerRequest.Click += new System.EventHandler(this.btnSetNumTilesInNativeServerRequest_Click);
            // 
            // ntxNumTilesInNativeServerRequest
            // 
            this.ntxNumTilesInNativeServerRequest.Location = new System.Drawing.Point(66, 25);
            this.ntxNumTilesInNativeServerRequest.Name = "ntxNumTilesInNativeServerRequest";
            this.ntxNumTilesInNativeServerRequest.Size = new System.Drawing.Size(77, 20);
            this.ntxNumTilesInNativeServerRequest.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGeneral);
            this.tabControl1.Controls.Add(this.tpLayers);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(495, 500);
            this.tabControl1.TabIndex = 59;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.chxDisplayItemsAttachedTo3DModelWithoutDtm);
            this.tpGeneral.Controls.Add(this.ctrlBoundingBox);
            this.tpGeneral.Controls.Add(this.groupBox6);
            this.tpGeneral.Controls.Add(this.ctrlUserData);
            this.tpGeneral.Controls.Add(this.label4);
            this.tpGeneral.Controls.Add(this.ntxTerrainID);
            this.tpGeneral.Controls.Add(this.ctrlGridCoordinateSystemDetails1);
            this.tpGeneral.Controls.Add(this.chxDefaultVisibility);
            this.tpGeneral.Controls.Add(this.clstViewports);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(487, 474);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            this.tpGeneral.Enter += new System.EventHandler(this.tpGeneral_Enter);
            this.tpGeneral.Leave += new System.EventHandler(this.tpGeneral_Leave);
            // 
            // ctrlBoundingBox
            // 
            this.ctrlBoundingBox.GroupBoxText = "World Bounding Box:";
            this.ctrlBoundingBox.Location = new System.Drawing.Point(5, 301);
            this.ctrlBoundingBox.Name = "ctrlBoundingBox";
            this.ctrlBoundingBox.Size = new System.Drawing.Size(304, 85);
            this.ctrlBoundingBox.TabIndex = 99;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lnkLayerName);
            this.groupBox6.Controls.Add(this.label114);
            this.groupBox6.Controls.Add(this.label115);
            this.groupBox6.Controls.Add(this.btnGetLayerByID);
            this.groupBox6.Controls.Add(this.ntxGetLayerById);
            this.groupBox6.Location = new System.Drawing.Point(6, 132);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(225, 74);
            this.groupBox6.TabIndex = 98;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Get Layer By ID";
            // 
            // lnkLayerName
            // 
            this.lnkLayerName.AutoSize = true;
            this.lnkLayerName.Location = new System.Drawing.Point(11, 47);
            this.lnkLayerName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lnkLayerName.Name = "lnkLayerName";
            this.lnkLayerName.Size = new System.Drawing.Size(0, 13);
            this.lnkLayerName.TabIndex = 23;
            this.lnkLayerName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLayerName_LinkClicked);
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(6, 74);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(0, 13);
            this.label114.TabIndex = 21;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(6, 23);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(21, 13);
            this.label115.TabIndex = 17;
            this.label115.Text = "ID:";
            // 
            // btnGetLayerByID
            // 
            this.btnGetLayerByID.Location = new System.Drawing.Point(128, 18);
            this.btnGetLayerByID.Name = "btnGetLayerByID";
            this.btnGetLayerByID.Size = new System.Drawing.Size(82, 23);
            this.btnGetLayerByID.TabIndex = 18;
            this.btnGetLayerByID.Text = "Get Layer";
            this.btnGetLayerByID.UseVisualStyleBackColor = true;
            this.btnGetLayerByID.Click += new System.EventHandler(this.btnGetLayerByID_Click);
            // 
            // ntxGetLayerById
            // 
            this.ntxGetLayerById.Location = new System.Drawing.Point(43, 20);
            this.ntxGetLayerById.Name = "ntxGetLayerById";
            this.ntxGetLayerById.Size = new System.Drawing.Size(80, 20);
            this.ntxGetLayerById.TabIndex = 16;
            // 
            // ctrlUserData
            // 
            this.ctrlUserData.Location = new System.Drawing.Point(238, 132);
            this.ctrlUserData.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlUserData.Name = "ctrlUserData";
            this.ctrlUserData.Size = new System.Drawing.Size(194, 74);
            this.ctrlUserData.TabIndex = 87;
            this.ctrlUserData.UserDataByte = new byte[0];
            this.ctrlUserData.UserDateText = "System.Byte[]";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Terrain ID:";
            // 
            // ntxTerrainID
            // 
            this.ntxTerrainID.Location = new System.Drawing.Point(68, 106);
            this.ntxTerrainID.Name = "ntxTerrainID";
            this.ntxTerrainID.Size = new System.Drawing.Size(56, 20);
            this.ntxTerrainID.TabIndex = 78;
            // 
            // ctrlGridCoordinateSystemDetails1
            // 
            this.ctrlGridCoordinateSystemDetails1.Location = new System.Drawing.Point(2, 212);
            this.ctrlGridCoordinateSystemDetails1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlGridCoordinateSystemDetails1.Name = "ctrlGridCoordinateSystemDetails1";
            this.ctrlGridCoordinateSystemDetails1.Size = new System.Drawing.Size(250, 92);
            this.ctrlGridCoordinateSystemDetails1.TabIndex = 76;
            // 
            // chxDefaultVisibility
            // 
            this.chxDefaultVisibility.AutoSize = true;
            this.chxDefaultVisibility.Location = new System.Drawing.Point(6, 84);
            this.chxDefaultVisibility.Name = "chxDefaultVisibility";
            this.chxDefaultVisibility.Size = new System.Drawing.Size(99, 17);
            this.chxDefaultVisibility.TabIndex = 75;
            this.chxDefaultVisibility.Text = "Default Visibility";
            this.chxDefaultVisibility.UseVisualStyleBackColor = true;
            this.chxDefaultVisibility.CheckedChanged += new System.EventHandler(this.chxDefaultVisibility_CheckedChanged);
            // 
            // clstViewports
            // 
            this.clstViewports.FormattingEnabled = true;
            this.clstViewports.Location = new System.Drawing.Point(6, 6);
            this.clstViewports.Name = "clstViewports";
            this.clstViewports.Size = new System.Drawing.Size(336, 64);
            this.clstViewports.TabIndex = 74;
            this.clstViewports.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clstViewports_ItemCheck);
            // 
            // tpLayers
            // 
            this.tpLayers.Controls.Add(this.groupBox1);
            this.tpLayers.Controls.Add(this.btnSelectAll);
            this.tpLayers.Controls.Add(this.label3);
            this.tpLayers.Controls.Add(this.btnRemoveAdd);
            this.tpLayers.Controls.Add(this.lstLayersRemoveAdd);
            this.tpLayers.Controls.Add(this.lstLayers);
            this.tpLayers.Controls.Add(this.btnLayerOK);
            this.tpLayers.Controls.Add(this.ctrlSLayerParams);
            this.tpLayers.Location = new System.Drawing.Point(4, 22);
            this.tpLayers.Name = "tpLayers";
            this.tpLayers.Padding = new System.Windows.Forms.Padding(3);
            this.tpLayers.Size = new System.Drawing.Size(487, 474);
            this.tpLayers.TabIndex = 1;
            this.tpLayers.Text = "Layers";
            this.tpLayers.UseVisualStyleBackColor = true;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(373, 304);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(99, 23);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(7, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Add and Remove Several Layers";
            // 
            // btnRemoveAdd
            // 
            this.btnRemoveAdd.Location = new System.Drawing.Point(373, 333);
            this.btnRemoveAdd.Name = "btnRemoveAdd";
            this.btnRemoveAdd.Size = new System.Drawing.Size(99, 23);
            this.btnRemoveAdd.TabIndex = 6;
            this.btnRemoveAdd.Text = "Remove Selected";
            this.btnRemoveAdd.UseVisualStyleBackColor = true;
            this.btnRemoveAdd.Click += new System.EventHandler(this.btnRemoveAdd_Click);
            // 
            // lstLayersRemoveAdd
            // 
            this.lstLayersRemoveAdd.FormattingEnabled = true;
            this.lstLayersRemoveAdd.Location = new System.Drawing.Point(6, 209);
            this.lstLayersRemoveAdd.Name = "lstLayersRemoveAdd";
            this.lstLayersRemoveAdd.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstLayersRemoveAdd.Size = new System.Drawing.Size(240, 147);
            this.lstLayersRemoveAdd.TabIndex = 5;
            // 
            // lstLayers
            // 
            this.lstLayers.FormattingEnabled = true;
            this.lstLayers.Location = new System.Drawing.Point(6, 6);
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.Size = new System.Drawing.Size(240, 160);
            this.lstLayers.TabIndex = 4;
            // 
            // btnLayerOK
            // 
            this.btnLayerOK.Location = new System.Drawing.Point(373, 168);
            this.btnLayerOK.Name = "btnLayerOK";
            this.btnLayerOK.Size = new System.Drawing.Size(99, 23);
            this.btnLayerOK.TabIndex = 3;
            this.btnLayerOK.Text = "OK";
            this.btnLayerOK.UseVisualStyleBackColor = true;
            this.btnLayerOK.Click += new System.EventHandler(this.btnLayerOK_Click);
            // 
            // ctrlSLayerParams
            // 
            this.ctrlSLayerParams.LayerParams = dnsLayerParams1;
            this.ctrlSLayerParams.Location = new System.Drawing.Point(267, 6);
            this.ctrlSLayerParams.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSLayerParams.Name = "ctrlSLayerParams";
            this.ctrlSLayerParams.Size = new System.Drawing.Size(214, 157);
            this.ctrlSLayerParams.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(422, 500);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 22);
            this.btnOK.TabIndex = 79;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chxDisplayItemsAttachedTo3DModelWithoutDtm
            // 
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.AutoSize = true;
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Enabled = false;
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Location = new System.Drawing.Point(9, 404);
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Name = "chxDisplayItemsAttachedTo3DModelWithoutDtm";
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Size = new System.Drawing.Size(281, 17);
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.TabIndex = 100;
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Text = "Display Items Attached To 3D Model Without Dtm";
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.UseVisualStyleBackColor = true;
            // 
            // ucTerrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnOK);
            this.Name = "ucTerrain";
            this.Size = new System.Drawing.Size(500, 600);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tpLayers.ResumeLayout(false);
            this.tpLayers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpLayers;
        private MCTester.Controls.CtrlSLayerParams ctrlSLayerParams;
        private System.Windows.Forms.Button btnLayerOK;
        private System.Windows.Forms.ListBox lstLayers;
        private System.Windows.Forms.CheckBox chxDefaultVisibility;
        private System.Windows.Forms.CheckedListBox clstViewports;
        private System.Windows.Forms.Button btnRemoveAdd;
        private System.Windows.Forms.ListBox lstLayersRemoveAdd;
        private System.Windows.Forms.Label label3;
        private Controls.CtrlGridCoordinateSystemDetails ctrlGridCoordinateSystemDetails1;
        private System.Windows.Forms.Label label4;
        private Controls.NumericTextBox ntxTerrainID;
        private System.Windows.Forms.Button btnOK;
        private Controls.ctrlUserData ctrlUserData;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.LinkLabel lnkLayerName;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Button btnGetLayerByID;
        private Controls.NumericTextBox ntxGetLayerById;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSetNumTilesInNativeServerRequest;
        private Controls.NumericTextBox ntxNumTilesInNativeServerRequest;
        private System.Windows.Forms.Label label6;
        private Controls.CtrlSMcBox ctrlBoundingBox;
        private System.Windows.Forms.CheckBox chxDisplayItemsAttachedTo3DModelWithoutDtm;
    }
}
