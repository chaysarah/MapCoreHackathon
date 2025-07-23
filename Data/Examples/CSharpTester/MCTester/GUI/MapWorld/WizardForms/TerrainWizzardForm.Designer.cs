namespace MCTester.MapWorld.WizardForms
{
    partial class TerrainWizzardForm
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
            this.lstTerrains = new System.Windows.Forms.ListBox();
            this.m_radCreateNew = new System.Windows.Forms.RadioButton();
            this.m_radUseExisting = new System.Windows.Forms.RadioButton();
            this.m_radLoadFromFile = new System.Windows.Forms.RadioButton();
            this.gbLoadFromFile = new System.Windows.Forms.GroupBox();
            this.txtBaseDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenSaveBaseDir = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenSaveFileName = new System.Windows.Forms.Button();
            this.ctrlTerrainGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.gbCreateNew = new System.Windows.Forms.GroupBox();
            this.ctrlLayers1 = new MCTester.MapWorld.WizardForms.CtrlLayers();
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm = new System.Windows.Forms.CheckBox();
            this.gbLoadFromFile.SuspendLayout();
            this.gbCreateNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(546, 426);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstTerrains
            // 
            this.lstTerrains.FormattingEnabled = true;
            this.lstTerrains.Location = new System.Drawing.Point(12, 31);
            this.lstTerrains.Name = "lstTerrains";
            this.lstTerrains.Size = new System.Drawing.Size(264, 173);
            this.lstTerrains.TabIndex = 7;
            // 
            // m_radCreateNew
            // 
            this.m_radCreateNew.AutoSize = true;
            this.m_radCreateNew.Location = new System.Drawing.Point(12, 206);
            this.m_radCreateNew.Name = "m_radCreateNew";
            this.m_radCreateNew.Size = new System.Drawing.Size(81, 17);
            this.m_radCreateNew.TabIndex = 6;
            this.m_radCreateNew.TabStop = true;
            this.m_radCreateNew.Text = "Create New";
            this.m_radCreateNew.UseVisualStyleBackColor = true;
            this.m_radCreateNew.CheckedChanged += new System.EventHandler(this.m_radCreateNew_CheckedChanged);
            // 
            // m_radUseExisting
            // 
            this.m_radUseExisting.AutoSize = true;
            this.m_radUseExisting.Location = new System.Drawing.Point(12, 8);
            this.m_radUseExisting.Name = "m_radUseExisting";
            this.m_radUseExisting.Size = new System.Drawing.Size(83, 17);
            this.m_radUseExisting.TabIndex = 5;
            this.m_radUseExisting.TabStop = true;
            this.m_radUseExisting.Text = "Use Existing";
            this.m_radUseExisting.UseVisualStyleBackColor = true;
            this.m_radUseExisting.CheckedChanged += new System.EventHandler(this.m_radUseExisting_CheckedChanged);
            // 
            // m_radLoadFromFile
            // 
            this.m_radLoadFromFile.AutoSize = true;
            this.m_radLoadFromFile.Location = new System.Drawing.Point(282, 8);
            this.m_radLoadFromFile.Name = "m_radLoadFromFile";
            this.m_radLoadFromFile.Size = new System.Drawing.Size(94, 17);
            this.m_radLoadFromFile.TabIndex = 12;
            this.m_radLoadFromFile.TabStop = true;
            this.m_radLoadFromFile.Text = "Load From File";
            this.m_radLoadFromFile.UseVisualStyleBackColor = true;
            this.m_radLoadFromFile.CheckedChanged += new System.EventHandler(this.m_radLoadFromFile_CheckedChanged);
            // 
            // gbLoadFromFile
            // 
            this.gbLoadFromFile.Controls.Add(this.txtBaseDirectory);
            this.gbLoadFromFile.Controls.Add(this.label2);
            this.gbLoadFromFile.Controls.Add(this.btnOpenSaveBaseDir);
            this.gbLoadFromFile.Controls.Add(this.txtFileName);
            this.gbLoadFromFile.Controls.Add(this.label1);
            this.gbLoadFromFile.Controls.Add(this.btnOpenSaveFileName);
            this.gbLoadFromFile.Location = new System.Drawing.Point(282, 25);
            this.gbLoadFromFile.Name = "gbLoadFromFile";
            this.gbLoadFromFile.Size = new System.Drawing.Size(323, 179);
            this.gbLoadFromFile.TabIndex = 13;
            this.gbLoadFromFile.TabStop = false;
            // 
            // txtBaseDirectory
            // 
            this.txtBaseDirectory.Location = new System.Drawing.Point(88, 45);
            this.txtBaseDirectory.Name = "txtBaseDirectory";
            this.txtBaseDirectory.Size = new System.Drawing.Size(197, 20);
            this.txtBaseDirectory.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Base Directory:";
            // 
            // btnOpenSaveBaseDir
            // 
            this.btnOpenSaveBaseDir.Location = new System.Drawing.Point(291, 42);
            this.btnOpenSaveBaseDir.Name = "btnOpenSaveBaseDir";
            this.btnOpenSaveBaseDir.Size = new System.Drawing.Size(26, 23);
            this.btnOpenSaveBaseDir.TabIndex = 9;
            this.btnOpenSaveBaseDir.Text = "...";
            this.btnOpenSaveBaseDir.UseVisualStyleBackColor = true;
            this.btnOpenSaveBaseDir.Click += new System.EventHandler(this.btnOpenSaveBaseDir_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(88, 19);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(197, 20);
            this.txtFileName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "File Name:";
            // 
            // btnOpenSaveFileName
            // 
            this.btnOpenSaveFileName.Location = new System.Drawing.Point(291, 17);
            this.btnOpenSaveFileName.Name = "btnOpenSaveFileName";
            this.btnOpenSaveFileName.Size = new System.Drawing.Size(26, 23);
            this.btnOpenSaveFileName.TabIndex = 6;
            this.btnOpenSaveFileName.Text = "...";
            this.btnOpenSaveFileName.UseVisualStyleBackColor = true;
            this.btnOpenSaveFileName.Click += new System.EventHandler(this.btnOpenSaveFileName_Click);
            // 
            // ctrlTerrainGridCoordinateSystem
            // 
            this.ctrlTerrainGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlTerrainGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlTerrainGridCoordinateSystem.GroupBoxText = "Grid Coordinate System";
            this.ctrlTerrainGridCoordinateSystem.IsEditable = false;
            this.ctrlTerrainGridCoordinateSystem.Location = new System.Drawing.Point(2, 12);
            this.ctrlTerrainGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlTerrainGridCoordinateSystem.Name = "ctrlTerrainGridCoordinateSystem";
            this.ctrlTerrainGridCoordinateSystem.Size = new System.Drawing.Size(336, 155);
            this.ctrlTerrainGridCoordinateSystem.TabIndex = 14;
            // 
            // gbCreateNew
            // 
            this.gbCreateNew.Controls.Add(this.chxDisplayItemsAttachedTo3DModelWithoutDtm);
            this.gbCreateNew.Controls.Add(this.ctrlLayers1);
            this.gbCreateNew.Controls.Add(this.ctrlTerrainGridCoordinateSystem);
            this.gbCreateNew.Enabled = false;
            this.gbCreateNew.Location = new System.Drawing.Point(10, 223);
            this.gbCreateNew.Margin = new System.Windows.Forms.Padding(2);
            this.gbCreateNew.Name = "gbCreateNew";
            this.gbCreateNew.Padding = new System.Windows.Forms.Padding(2);
            this.gbCreateNew.Size = new System.Drawing.Size(611, 198);
            this.gbCreateNew.TabIndex = 15;
            this.gbCreateNew.TabStop = false;
            // 
            // ctrlLayers1
            // 
            this.ctrlLayers1.Location = new System.Drawing.Point(340, 9);
            this.ctrlLayers1.Name = "ctrlLayers1";
            this.ctrlLayers1.Size = new System.Drawing.Size(270, 162);
            this.ctrlLayers1.TabIndex = 16;
            // 
            // chxDisplayItemsAttachedTo3DModelWithoutDtm
            // 
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.AutoSize = true;
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Location = new System.Drawing.Point(5, 174);
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Name = "chxDisplayItemsAttachedTo3DModelWithoutDtm";
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Size = new System.Drawing.Size(261, 17);
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.TabIndex = 16;
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.Text = "Display Items Attached To 3D Model Without Dtm";
            this.chxDisplayItemsAttachedTo3DModelWithoutDtm.UseVisualStyleBackColor = true;
            // 
            // TerrainWizzardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(626, 453);
            this.Controls.Add(this.gbCreateNew);
            this.Controls.Add(this.gbLoadFromFile);
            this.Controls.Add(this.m_radLoadFromFile);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstTerrains);
            this.Controls.Add(this.m_radCreateNew);
            this.Controls.Add(this.m_radUseExisting);
            this.Name = "TerrainWizzardForm";
            this.Text = "Terrain Wizzard Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TerrainWizzardForm_FormClosed);
            this.Load += new System.EventHandler(this.TerrainWizzardForm_Load);
            this.gbLoadFromFile.ResumeLayout(false);
            this.gbLoadFromFile.PerformLayout();
            this.gbCreateNew.ResumeLayout(false);
            this.gbCreateNew.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstTerrains;
        private System.Windows.Forms.RadioButton m_radCreateNew;
        private System.Windows.Forms.RadioButton m_radUseExisting;
        private System.Windows.Forms.RadioButton m_radLoadFromFile;
        private System.Windows.Forms.GroupBox gbLoadFromFile;
        private System.Windows.Forms.TextBox txtBaseDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenSaveBaseDir;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenSaveFileName;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlTerrainGridCoordinateSystem;
        private System.Windows.Forms.GroupBox gbCreateNew;
        private CtrlLayers ctrlLayers1;
        private System.Windows.Forms.CheckBox chxDisplayItemsAttachedTo3DModelWithoutDtm;
    }
}