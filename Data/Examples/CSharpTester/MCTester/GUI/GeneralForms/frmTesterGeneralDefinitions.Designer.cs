namespace MCTester.General_Forms
{
    partial class frmTesterGeneralDefinitions
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
            this.chxTERRAIN_LOAD = new System.Windows.Forms.CheckBox();
            this.chxOBJECT_DELAY = new System.Windows.Forms.CheckBox();
            this.chxGLOBAL_MAP = new System.Windows.Forms.CheckBox();
            this.chxANY_UPDATE = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbPendingUpdateTypes = new System.Windows.Forms.GroupBox();
            this.chxIMAGEPROCESS = new System.Windows.Forms.CheckBox();
            this.chxEPUT_GRID = new System.Windows.Forms.CheckBox();
            this.gbMsgDisplay = new System.Windows.Forms.GroupBox();
            this.rdbMsgPendingUpdates = new System.Windows.Forms.RadioButton();
            this.rdbMsgNone = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxRenderInterval = new MCTester.Controls.NumericTextBox();
            this.chxGetRenderStatistics = new System.Windows.Forms.CheckBox();
            this.ntxCharacterSpacing = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxNumAntialiasingAlphaLevels = new MCTester.Controls.NumericTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvLogFontToTtfFileMap = new System.Windows.Forms.DataGridView();
            this.SelectFont = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FontDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SelectPath = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFontDialog = new System.Windows.Forms.FontDialog();
            this.chxUseBasicItemPropertiesOnly = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPendingUpdateTypes.SuspendLayout();
            this.gbMsgDisplay.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogFontToTtfFileMap)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chxTERRAIN_LOAD
            // 
            this.chxTERRAIN_LOAD.AutoSize = true;
            this.chxTERRAIN_LOAD.Location = new System.Drawing.Point(6, 29);
            this.chxTERRAIN_LOAD.Name = "chxTERRAIN_LOAD";
            this.chxTERRAIN_LOAD.Size = new System.Drawing.Size(74, 17);
            this.chxTERRAIN_LOAD.TabIndex = 5;
            this.chxTERRAIN_LOAD.Text = "TERRAIN";
            this.chxTERRAIN_LOAD.UseVisualStyleBackColor = true;
            // 
            // chxOBJECT_DELAY
            // 
            this.chxOBJECT_DELAY.AutoSize = true;
            this.chxOBJECT_DELAY.Location = new System.Drawing.Point(6, 52);
            this.chxOBJECT_DELAY.Name = "chxOBJECT_DELAY";
            this.chxOBJECT_DELAY.Size = new System.Drawing.Size(74, 17);
            this.chxOBJECT_DELAY.TabIndex = 6;
            this.chxOBJECT_DELAY.Text = "OBJECTS";
            this.chxOBJECT_DELAY.UseVisualStyleBackColor = true;
            // 
            // chxGLOBAL_MAP
            // 
            this.chxGLOBAL_MAP.AutoSize = true;
            this.chxGLOBAL_MAP.Location = new System.Drawing.Point(6, 75);
            this.chxGLOBAL_MAP.Name = "chxGLOBAL_MAP";
            this.chxGLOBAL_MAP.Size = new System.Drawing.Size(97, 17);
            this.chxGLOBAL_MAP.TabIndex = 7;
            this.chxGLOBAL_MAP.Text = "GLOBAL_MAP";
            this.chxGLOBAL_MAP.UseVisualStyleBackColor = true;
            // 
            // chxANY_UPDATE
            // 
            this.chxANY_UPDATE.AutoSize = true;
            this.chxANY_UPDATE.Location = new System.Drawing.Point(6, 144);
            this.chxANY_UPDATE.Name = "chxANY_UPDATE";
            this.chxANY_UPDATE.Size = new System.Drawing.Size(98, 17);
            this.chxANY_UPDATE.TabIndex = 8;
            this.chxANY_UPDATE.Text = "ANY_UPDATE";
            this.chxANY_UPDATE.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(846, 388);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbPendingUpdateTypes
            // 
            this.gbPendingUpdateTypes.Controls.Add(this.chxIMAGEPROCESS);
            this.gbPendingUpdateTypes.Controls.Add(this.chxEPUT_GRID);
            this.gbPendingUpdateTypes.Controls.Add(this.chxTERRAIN_LOAD);
            this.gbPendingUpdateTypes.Controls.Add(this.chxOBJECT_DELAY);
            this.gbPendingUpdateTypes.Controls.Add(this.chxANY_UPDATE);
            this.gbPendingUpdateTypes.Controls.Add(this.chxGLOBAL_MAP);
            this.gbPendingUpdateTypes.Location = new System.Drawing.Point(34, 42);
            this.gbPendingUpdateTypes.Name = "gbPendingUpdateTypes";
            this.gbPendingUpdateTypes.Size = new System.Drawing.Size(196, 174);
            this.gbPendingUpdateTypes.TabIndex = 10;
            this.gbPendingUpdateTypes.TabStop = false;
            this.gbPendingUpdateTypes.Text = "Pending Update Types";
            // 
            // chxIMAGEPROCESS
            // 
            this.chxIMAGEPROCESS.AutoSize = true;
            this.chxIMAGEPROCESS.Location = new System.Drawing.Point(6, 121);
            this.chxIMAGEPROCESS.Name = "chxIMAGEPROCESS";
            this.chxIMAGEPROCESS.Size = new System.Drawing.Size(117, 17);
            this.chxIMAGEPROCESS.TabIndex = 10;
            this.chxIMAGEPROCESS.Text = "IMAGE_PROCESS";
            this.chxIMAGEPROCESS.UseVisualStyleBackColor = true;
            // 
            // chxEPUT_GRID
            // 
            this.chxEPUT_GRID.AutoSize = true;
            this.chxEPUT_GRID.Location = new System.Drawing.Point(6, 98);
            this.chxEPUT_GRID.Name = "chxEPUT_GRID";
            this.chxEPUT_GRID.Size = new System.Drawing.Size(53, 17);
            this.chxEPUT_GRID.TabIndex = 9;
            this.chxEPUT_GRID.Text = "GRID";
            this.chxEPUT_GRID.UseVisualStyleBackColor = true;
            // 
            // gbMsgDisplay
            // 
            this.gbMsgDisplay.Controls.Add(this.rdbMsgPendingUpdates);
            this.gbMsgDisplay.Controls.Add(this.rdbMsgNone);
            this.gbMsgDisplay.Controls.Add(this.gbPendingUpdateTypes);
            this.gbMsgDisplay.Location = new System.Drawing.Point(4, 32);
            this.gbMsgDisplay.Name = "gbMsgDisplay";
            this.gbMsgDisplay.Size = new System.Drawing.Size(237, 222);
            this.gbMsgDisplay.TabIndex = 11;
            this.gbMsgDisplay.TabStop = false;
            this.gbMsgDisplay.Text = "Message Display in Status Bar";
            // 
            // rdbMsgPendingUpdates
            // 
            this.rdbMsgPendingUpdates.AutoSize = true;
            this.rdbMsgPendingUpdates.Location = new System.Drawing.Point(14, 42);
            this.rdbMsgPendingUpdates.Name = "rdbMsgPendingUpdates";
            this.rdbMsgPendingUpdates.Size = new System.Drawing.Size(14, 13);
            this.rdbMsgPendingUpdates.TabIndex = 12;
            this.rdbMsgPendingUpdates.UseVisualStyleBackColor = true;
            this.rdbMsgPendingUpdates.CheckedChanged += new System.EventHandler(this.rdbMsgPendingUpdates_CheckedChanged);
            // 
            // rdbMsgNone
            // 
            this.rdbMsgNone.AutoSize = true;
            this.rdbMsgNone.Checked = true;
            this.rdbMsgNone.Location = new System.Drawing.Point(14, 19);
            this.rdbMsgNone.Name = "rdbMsgNone";
            this.rdbMsgNone.Size = new System.Drawing.Size(51, 17);
            this.rdbMsgNone.TabIndex = 11;
            this.rdbMsgNone.TabStop = true;
            this.rdbMsgNone.Text = "None";
            this.rdbMsgNone.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Render Interval:";
            // 
            // ntxRenderInterval
            // 
            this.ntxRenderInterval.Location = new System.Drawing.Point(101, 6);
            this.ntxRenderInterval.Name = "ntxRenderInterval";
            this.ntxRenderInterval.Size = new System.Drawing.Size(63, 20);
            this.ntxRenderInterval.TabIndex = 13;
            this.ntxRenderInterval.Text = "60";
            // 
            // chxGetRenderStatistics
            // 
            this.chxGetRenderStatistics.AutoSize = true;
            this.chxGetRenderStatistics.Location = new System.Drawing.Point(15, 260);
            this.chxGetRenderStatistics.Name = "chxGetRenderStatistics";
            this.chxGetRenderStatistics.Size = new System.Drawing.Size(148, 17);
            this.chxGetRenderStatistics.TabIndex = 14;
            this.chxGetRenderStatistics.Text = "Activate Render Statistics";
            this.chxGetRenderStatistics.UseVisualStyleBackColor = true;
            // 
            // ntxCharacterSpacing
            // 
            this.ntxCharacterSpacing.Location = new System.Drawing.Point(110, 23);
            this.ntxCharacterSpacing.Name = "ntxCharacterSpacing";
            this.ntxCharacterSpacing.Size = new System.Drawing.Size(53, 20);
            this.ntxCharacterSpacing.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Character Spacing:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Num Antialiasing Alpha Levels (2-256):";
            // 
            // ntxNumAntialiasingAlphaLevels
            // 
            this.ntxNumAntialiasingAlphaLevels.Location = new System.Drawing.Point(408, 23);
            this.ntxNumAntialiasingAlphaLevels.Name = "ntxNumAntialiasingAlphaLevels";
            this.ntxNumAntialiasingAlphaLevels.Size = new System.Drawing.Size(57, 20);
            this.ntxNumAntialiasingAlphaLevels.TabIndex = 52;
            this.ntxNumAntialiasingAlphaLevels.Text = "2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dgvLogFontToTtfFileMap);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.ntxNumAntialiasingAlphaLevels);
            this.groupBox1.Controls.Add(this.ntxCharacterSpacing);
            this.groupBox1.Location = new System.Drawing.Point(261, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 242);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font Properties";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(9, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Log Font To TTF Files";
            // 
            // dgvLogFontToTtfFileMap
            // 
            this.dgvLogFontToTtfFileMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogFontToTtfFileMap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectFont,
            this.FontDesc,
            this.Column1,
            this.Column2,
            this.SelectPath,
            this.Path});
            this.dgvLogFontToTtfFileMap.Location = new System.Drawing.Point(9, 85);
            this.dgvLogFontToTtfFileMap.Name = "dgvLogFontToTtfFileMap";
            this.dgvLogFontToTtfFileMap.Size = new System.Drawing.Size(645, 151);
            this.dgvLogFontToTtfFileMap.TabIndex = 54;
            this.dgvLogFontToTtfFileMap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogFontToTtfFileMap_CellContentClick);
            this.dgvLogFontToTtfFileMap.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLogFontToTtfFileMap_CellValueChanged);
            // 
            // SelectFont
            // 
            this.SelectFont.HeaderText = "Select Font";
            this.SelectFont.Name = "SelectFont";
            this.SelectFont.Width = 70;
            // 
            // FontDesc
            // 
            this.FontDesc.HeaderText = "Font Name";
            this.FontDesc.Name = "FontDesc";
            this.FontDesc.Width = 160;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Bold";
            this.Column1.Name = "Column1";
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Italic";
            this.Column2.Name = "Column2";
            this.Column2.Width = 40;
            // 
            // SelectPath
            // 
            this.SelectPath.HeaderText = "Select Path";
            this.SelectPath.Name = "SelectPath";
            this.SelectPath.Width = 70;
            // 
            // Path
            // 
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.Width = 160;
            // 
            // chxUseBasicItemPropertiesOnly
            // 
            this.chxUseBasicItemPropertiesOnly.AutoSize = true;
            this.chxUseBasicItemPropertiesOnly.Checked = true;
            this.chxUseBasicItemPropertiesOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxUseBasicItemPropertiesOnly.Location = new System.Drawing.Point(6, 19);
            this.chxUseBasicItemPropertiesOnly.Name = "chxUseBasicItemPropertiesOnly";
            this.chxUseBasicItemPropertiesOnly.Size = new System.Drawing.Size(174, 17);
            this.chxUseBasicItemPropertiesOnly.TabIndex = 55;
            this.chxUseBasicItemPropertiesOnly.Text = "Use Basic Item Properties Only ";
            this.chxUseBasicItemPropertiesOnly.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chxUseBasicItemPropertiesOnly);
            this.groupBox2.Location = new System.Drawing.Point(13, 305);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 41);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Map Grid";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Location = new System.Drawing.Point(261, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(482, 151);
            this.groupBox4.TabIndex = 74;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Supported SRIDs";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colKey,
            this.colValue});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(470, 124);
            this.dataGridView1.TabIndex = 74;
            // 
            // colKey
            // 
            this.colKey.HeaderText = "Key";
            this.colKey.Name = "colKey";
            // 
            // colValue
            // 
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.Width = 300;
            // 
            // frmTesterGeneralDefinitions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(969, 416);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chxGetRenderStatistics);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ntxRenderInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbMsgDisplay);
            this.Controls.Add(this.btnOK);
            this.Name = "frmTesterGeneralDefinitions";
            this.Text = "General Definitions";
            this.Load += new System.EventHandler(this.frmTesterGeneralDefinitions_Load);
            this.gbPendingUpdateTypes.ResumeLayout(false);
            this.gbPendingUpdateTypes.PerformLayout();
            this.gbMsgDisplay.ResumeLayout(false);
            this.gbMsgDisplay.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogFontToTtfFileMap)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chxTERRAIN_LOAD;
        private System.Windows.Forms.CheckBox chxOBJECT_DELAY;
        private System.Windows.Forms.CheckBox chxGLOBAL_MAP;
        private System.Windows.Forms.CheckBox chxANY_UPDATE;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbPendingUpdateTypes;
        private System.Windows.Forms.GroupBox gbMsgDisplay;
        private System.Windows.Forms.RadioButton rdbMsgPendingUpdates;
        private System.Windows.Forms.RadioButton rdbMsgNone;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.NumericTextBox ntxRenderInterval;
        private System.Windows.Forms.CheckBox chxGetRenderStatistics;
        private MCTester.Controls.NumericTextBox ntxCharacterSpacing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chxEPUT_GRID;
        private System.Windows.Forms.CheckBox chxIMAGEPROCESS;
        private System.Windows.Forms.Label label2;
        private Controls.NumericTextBox ntxNumAntialiasingAlphaLevels;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvLogFontToTtfFileMap;
        private System.Windows.Forms.FontDialog openFontDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chxUseBasicItemPropertiesOnly;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewButtonColumn SelectFont;
        private System.Windows.Forms.DataGridViewTextBoxColumn FontDesc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn SelectPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
    }
}