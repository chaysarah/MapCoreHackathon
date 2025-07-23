namespace MCTester.MapWorld.MapUserControls
{
    partial class ucLayer
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
            this.btnLayerOK = new System.Windows.Forms.Button();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSetNumTilesInNativeServerRequest = new System.Windows.Forms.Button();
            this.btnGetCreateLayerParams = new System.Windows.Forms.Button();
            this.btnReplaceNativeServerLayerAsync = new System.Windows.Forms.Button();
            this.btnRemoveLayerAsync = new System.Windows.Forms.Button();
            this.cbIsInitialized = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.gbLayerVisibility = new System.Windows.Forms.GroupBox();
            this.chxDefaultVisibility = new System.Windows.Forms.CheckBox();
            this.lstTerrains = new System.Windows.Forms.ListBox();
            this.clstViewports = new System.Windows.Forms.CheckedListBox();
            this.chxLayerEffectiveVisibility = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tcLayer = new System.Windows.Forms.TabControl();
            this.ctrlBoundingBox = new MCTester.Controls.CtrlSMcBox();
            this.ntxNumTilesInNativeServerRequest = new MCTester.Controls.NumericTextBox();
            this.ctrlLocalCacheParams = new MCTester.Controls.LocalCacheParams();
            this.ctrlUserData = new MCTester.Controls.ctrlUserData();
            this.ctrlGridCoordinateSystemDetails1 = new MCTester.Controls.CtrlGridCoordinateSystemDetails();
            this.ntbGetBackgroundThreadIndex = new MCTester.Controls.NumericTextBox();
            this.ntxLayerID = new MCTester.Controls.NumericTextBox();
            this.ntxLayerType = new MCTester.Controls.NumericTextBox();
            this.tpGeneral.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.gbLayerVisibility.SuspendLayout();
            this.tcLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLayerOK
            // 
            this.btnLayerOK.Location = new System.Drawing.Point(670, 624);
            this.btnLayerOK.Name = "btnLayerOK";
            this.btnLayerOK.Size = new System.Drawing.Size(75, 23);
            this.btnLayerOK.TabIndex = 4;
            this.btnLayerOK.Text = "OK";
            this.btnLayerOK.UseVisualStyleBackColor = true;
            this.btnLayerOK.Click += new System.EventHandler(this.btnLayerOK_Click);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.ctrlBoundingBox);
            this.tpGeneral.Controls.Add(this.groupBox11);
            this.tpGeneral.Controls.Add(this.btnGetCreateLayerParams);
            this.tpGeneral.Controls.Add(this.btnReplaceNativeServerLayerAsync);
            this.tpGeneral.Controls.Add(this.btnRemoveLayerAsync);
            this.tpGeneral.Controls.Add(this.ctrlLocalCacheParams);
            this.tpGeneral.Controls.Add(this.ctrlUserData);
            this.tpGeneral.Controls.Add(this.ctrlGridCoordinateSystemDetails1);
            this.tpGeneral.Controls.Add(this.cbIsInitialized);
            this.tpGeneral.Controls.Add(this.ntbGetBackgroundThreadIndex);
            this.tpGeneral.Controls.Add(this.label5);
            this.tpGeneral.Controls.Add(this.gbLayerVisibility);
            this.tpGeneral.Controls.Add(this.label1);
            this.tpGeneral.Controls.Add(this.ntxLayerID);
            this.tpGeneral.Controls.Add(this.ntxLayerType);
            this.tpGeneral.Controls.Add(this.label2);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(737, 595);
            this.tpGeneral.TabIndex = 3;
            this.tpGeneral.Text = "MapLayer";
            this.tpGeneral.UseVisualStyleBackColor = true;
            this.tpGeneral.Enter += new System.EventHandler(this.tpGeneral_Enter);
            this.tpGeneral.Leave += new System.EventHandler(this.tpGeneral_Leave);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label6);
            this.groupBox11.Controls.Add(this.btnSetNumTilesInNativeServerRequest);
            this.groupBox11.Controls.Add(this.ntxNumTilesInNativeServerRequest);
            this.groupBox11.Location = new System.Drawing.Point(521, 66);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(200, 56);
            this.groupBox11.TabIndex = 94;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Num Tiles In Native Server Request";
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
            // btnGetCreateLayerParams
            // 
            this.btnGetCreateLayerParams.Location = new System.Drawing.Point(568, 537);
            this.btnGetCreateLayerParams.Name = "btnGetCreateLayerParams";
            this.btnGetCreateLayerParams.Size = new System.Drawing.Size(146, 26);
            this.btnGetCreateLayerParams.TabIndex = 93;
            this.btnGetCreateLayerParams.Text = "Get Create Layer Params";
            this.btnGetCreateLayerParams.UseVisualStyleBackColor = true;
            this.btnGetCreateLayerParams.Click += new System.EventHandler(this.btnGetCreateLayerParams_Click);
            // 
            // btnReplaceNativeServerLayerAsync
            // 
            this.btnReplaceNativeServerLayerAsync.Location = new System.Drawing.Point(9, 495);
            this.btnReplaceNativeServerLayerAsync.Name = "btnReplaceNativeServerLayerAsync";
            this.btnReplaceNativeServerLayerAsync.Size = new System.Drawing.Size(243, 34);
            this.btnReplaceNativeServerLayerAsync.TabIndex = 91;
            this.btnReplaceNativeServerLayerAsync.Text = "Replace Native Server Layer Async ";
            this.btnReplaceNativeServerLayerAsync.UseVisualStyleBackColor = true;
            this.btnReplaceNativeServerLayerAsync.Click += new System.EventHandler(this.btnReplaceNativeServerLayerAsync_Click);
            // 
            // btnRemoveLayerAsync
            // 
            this.btnRemoveLayerAsync.Location = new System.Drawing.Point(9, 535);
            this.btnRemoveLayerAsync.Name = "btnRemoveLayerAsync";
            this.btnRemoveLayerAsync.Size = new System.Drawing.Size(243, 34);
            this.btnRemoveLayerAsync.TabIndex = 90;
            this.btnRemoveLayerAsync.Text = "Remove Layer Async";
            this.btnRemoveLayerAsync.UseVisualStyleBackColor = true;
            this.btnRemoveLayerAsync.Click += new System.EventHandler(this.btnRemoveLayerAsync_Click);
            // 
            // cbIsInitialized
            // 
            this.cbIsInitialized.AutoSize = true;
            this.cbIsInitialized.Enabled = false;
            this.cbIsInitialized.Location = new System.Drawing.Point(232, 11);
            this.cbIsInitialized.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsInitialized.Name = "cbIsInitialized";
            this.cbIsInitialized.Size = new System.Drawing.Size(80, 17);
            this.cbIsInitialized.TabIndex = 79;
            this.cbIsInitialized.Text = "Is Initialized";
            this.cbIsInitialized.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 432);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 77;
            this.label5.Text = "Background Thread Index:";
            // 
            // gbLayerVisibility
            // 
            this.gbLayerVisibility.Controls.Add(this.chxDefaultVisibility);
            this.gbLayerVisibility.Controls.Add(this.lstTerrains);
            this.gbLayerVisibility.Controls.Add(this.clstViewports);
            this.gbLayerVisibility.Controls.Add(this.chxLayerEffectiveVisibility);
            this.gbLayerVisibility.Location = new System.Drawing.Point(316, 66);
            this.gbLayerVisibility.Name = "gbLayerVisibility";
            this.gbLayerVisibility.Size = new System.Drawing.Size(199, 259);
            this.gbLayerVisibility.TabIndex = 70;
            this.gbLayerVisibility.TabStop = false;
            this.gbLayerVisibility.Text = "Layer Visibility";
            // 
            // chxDefaultVisibility
            // 
            this.chxDefaultVisibility.AutoSize = true;
            this.chxDefaultVisibility.Location = new System.Drawing.Point(6, 19);
            this.chxDefaultVisibility.Name = "chxDefaultVisibility";
            this.chxDefaultVisibility.Size = new System.Drawing.Size(99, 17);
            this.chxDefaultVisibility.TabIndex = 73;
            this.chxDefaultVisibility.Text = "Default Visibility";
            this.chxDefaultVisibility.UseVisualStyleBackColor = true;
            this.chxDefaultVisibility.CheckedChanged += new System.EventHandler(this.chxDefaultVisibility_CheckedChanged);
            // 
            // lstTerrains
            // 
            this.lstTerrains.FormattingEnabled = true;
            this.lstTerrains.Location = new System.Drawing.Point(6, 127);
            this.lstTerrains.Name = "lstTerrains";
            this.lstTerrains.Size = new System.Drawing.Size(185, 82);
            this.lstTerrains.TabIndex = 72;
            this.lstTerrains.SelectedIndexChanged += new System.EventHandler(this.lstTerrains_SelectedIndexChanged);
            // 
            // clstViewports
            // 
            this.clstViewports.FormattingEnabled = true;
            this.clstViewports.Location = new System.Drawing.Point(6, 42);
            this.clstViewports.Name = "clstViewports";
            this.clstViewports.Size = new System.Drawing.Size(185, 79);
            this.clstViewports.TabIndex = 71;
            this.clstViewports.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clstViewports_ItemCheck);
            this.clstViewports.SelectedIndexChanged += new System.EventHandler(this.clstViewports_SelectedIndexChanged);
            // 
            // chxLayerEffectiveVisibility
            // 
            this.chxLayerEffectiveVisibility.Enabled = false;
            this.chxLayerEffectiveVisibility.Location = new System.Drawing.Point(6, 215);
            this.chxLayerEffectiveVisibility.Name = "chxLayerEffectiveVisibility";
            this.chxLayerEffectiveVisibility.Size = new System.Drawing.Size(187, 38);
            this.chxLayerEffectiveVisibility.TabIndex = 68;
            this.chxLayerEffectiveVisibility.Text = "Effective visibility in viewport and terrain";
            this.chxLayerEffectiveVisibility.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Layer ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Layer Type:";
            // 
            // tcLayer
            // 
            this.tcLayer.Controls.Add(this.tpGeneral);
            this.tcLayer.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcLayer.Location = new System.Drawing.Point(0, 0);
            this.tcLayer.Name = "tcLayer";
            this.tcLayer.SelectedIndex = 0;
            this.tcLayer.Size = new System.Drawing.Size(745, 621);
            this.tcLayer.TabIndex = 59;
            // 
            // ctrlBoundingBox
            // 
            this.ctrlBoundingBox.GroupBoxText = "SMcBox";
            this.ctrlBoundingBox.Location = new System.Drawing.Point(6, 66);
            this.ctrlBoundingBox.Name = "ctrlBoundingBox";
            this.ctrlBoundingBox.Size = new System.Drawing.Size(304, 85);
            this.ctrlBoundingBox.TabIndex = 95;
            // 
            // ntxNumTilesInNativeServerRequest
            // 
            this.ntxNumTilesInNativeServerRequest.Location = new System.Drawing.Point(66, 25);
            this.ntxNumTilesInNativeServerRequest.Name = "ntxNumTilesInNativeServerRequest";
            this.ntxNumTilesInNativeServerRequest.Size = new System.Drawing.Size(77, 20);
            this.ntxNumTilesInNativeServerRequest.TabIndex = 1;
            // 
            // ctrlLocalCacheParams
            // 
            this.ctrlLocalCacheParams.Location = new System.Drawing.Point(7, 332);
            this.ctrlLocalCacheParams.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlLocalCacheParams.Name = "ctrlLocalCacheParams";
            this.ctrlLocalCacheParams.Size = new System.Drawing.Size(488, 71);
            this.ctrlLocalCacheParams.SubFolderPath = "";
            this.ctrlLocalCacheParams.TabIndex = 74;
            // 
            // ctrlUserData
            // 
            this.ctrlUserData.Location = new System.Drawing.Point(9, 250);
            this.ctrlUserData.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlUserData.Name = "ctrlUserData";
            this.ctrlUserData.Size = new System.Drawing.Size(249, 74);
            this.ctrlUserData.TabIndex = 88;
            this.ctrlUserData.UserDataByte = new byte[0];
            this.ctrlUserData.UserDateText = "System.Byte[]";
            // 
            // ctrlGridCoordinateSystemDetails1
            // 
            this.ctrlGridCoordinateSystemDetails1.Location = new System.Drawing.Point(8, 155);
            this.ctrlGridCoordinateSystemDetails1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlGridCoordinateSystemDetails1.Name = "ctrlGridCoordinateSystemDetails1";
            this.ctrlGridCoordinateSystemDetails1.Size = new System.Drawing.Size(250, 92);
            this.ctrlGridCoordinateSystemDetails1.TabIndex = 80;
            // 
            // ntbGetBackgroundThreadIndex
            // 
            this.ntbGetBackgroundThreadIndex.Location = new System.Drawing.Point(144, 430);
            this.ntbGetBackgroundThreadIndex.Name = "ntbGetBackgroundThreadIndex";
            this.ntbGetBackgroundThreadIndex.ReadOnly = true;
            this.ntbGetBackgroundThreadIndex.Size = new System.Drawing.Size(117, 20);
            this.ntbGetBackgroundThreadIndex.TabIndex = 78;
            // 
            // ntxLayerID
            // 
            this.ntxLayerID.Location = new System.Drawing.Point(69, 10);
            this.ntxLayerID.Name = "ntxLayerID";
            this.ntxLayerID.Size = new System.Drawing.Size(100, 20);
            this.ntxLayerID.TabIndex = 0;
            // 
            // ntxLayerType
            // 
            this.ntxLayerType.Enabled = false;
            this.ntxLayerType.Location = new System.Drawing.Point(69, 40);
            this.ntxLayerType.Name = "ntxLayerType";
            this.ntxLayerType.Size = new System.Drawing.Size(236, 20);
            this.ntxLayerType.TabIndex = 2;
            // 
            // ucLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnLayerOK);
            this.Controls.Add(this.tcLayer);
            this.Name = "ucLayer";
            this.Size = new System.Drawing.Size(745, 647);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.gbLayerVisibility.ResumeLayout(false);
            this.gbLayerVisibility.PerformLayout();
            this.tcLayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLayerOK;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.NumericTextBox ntxLayerID;
        private MCTester.Controls.NumericTextBox ntxLayerType;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.TabControl tcLayer;
        protected System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.CheckBox chxLayerEffectiveVisibility;
        private System.Windows.Forms.GroupBox gbLayerVisibility;
        private System.Windows.Forms.CheckedListBox clstViewports;
        private System.Windows.Forms.ListBox lstTerrains;
        private System.Windows.Forms.CheckBox chxDefaultVisibility;
        private Controls.LocalCacheParams ctrlLocalCacheParams;
        private Controls.NumericTextBox ntbGetBackgroundThreadIndex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbIsInitialized;
        private Controls.CtrlGridCoordinateSystemDetails ctrlGridCoordinateSystemDetails1;
        private Controls.ctrlUserData ctrlUserData;
        private System.Windows.Forms.Button btnReplaceNativeServerLayerAsync;
        private System.Windows.Forms.Button btnRemoveLayerAsync;
        private System.Windows.Forms.Button btnGetCreateLayerParams;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button btnSetNumTilesInNativeServerRequest;
        private Controls.NumericTextBox ntxNumTilesInNativeServerRequest;
        private System.Windows.Forms.Label label6;
        private Controls.CtrlSMcBox ctrlBoundingBox;
    }
}
