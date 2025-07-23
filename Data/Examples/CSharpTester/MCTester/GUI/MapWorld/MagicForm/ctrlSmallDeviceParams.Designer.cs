namespace MCTester.ButtonsImplementation
{
    partial class ctrlSmallDeviceParams
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxLocalCacheMaxSize = new MCTester.Controls.NumericTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.ctrlBrowseLocalCacheFolder = new MCTester.Controls.CtrlBrowseControl();
            this.chxMultiThreads = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbViewportAntiAliasingLevel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTerrainObjectsAntiAliasingLevel = new System.Windows.Forms.ComboBox();
            this.chxMultiScreenDevice = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxNumBackgroundThreads = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ntxLocalCacheMaxSize);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.ctrlBrowseLocalCacheFolder);
            this.groupBox1.Controls.Add(this.chxMultiThreads);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbViewportAntiAliasingLevel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbTerrainObjectsAntiAliasingLevel);
            this.groupBox1.Controls.Add(this.chxMultiScreenDevice);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ntxNumBackgroundThreads);
            this.groupBox1.Location = new System.Drawing.Point(2, -2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(463, 133);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device";
            // 
            // ntxLocalCacheMaxSize
            // 
            this.ntxLocalCacheMaxSize.Location = new System.Drawing.Point(179, 84);
            this.ntxLocalCacheMaxSize.Name = "ntxLocalCacheMaxSize";
            this.ntxLocalCacheMaxSize.Size = new System.Drawing.Size(48, 20);
            this.ntxLocalCacheMaxSize.TabIndex = 112;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 86);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(147, 13);
            this.label19.TabIndex = 111;
            this.label19.Text = "Local Cache Max Size In MB:";
            // 
            // ctrlBrowseLocalCacheFolder
            // 
            this.ctrlBrowseLocalCacheFolder.AutoSize = true;
            this.ctrlBrowseLocalCacheFolder.FileName = "";
            this.ctrlBrowseLocalCacheFolder.Filter = "";
            this.ctrlBrowseLocalCacheFolder.IsFolderDialog = true;
            this.ctrlBrowseLocalCacheFolder.IsFullPath = true;
            this.ctrlBrowseLocalCacheFolder.IsSaveFile = false;
            this.ctrlBrowseLocalCacheFolder.LabelCaption = "Local Cache Folder:";
            this.ctrlBrowseLocalCacheFolder.Location = new System.Drawing.Point(106, 107);
            this.ctrlBrowseLocalCacheFolder.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseLocalCacheFolder.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseLocalCacheFolder.MultiFilesSelect = false;
            this.ctrlBrowseLocalCacheFolder.Name = "ctrlBrowseLocalCacheFolder";
            this.ctrlBrowseLocalCacheFolder.Prefix = "";
            this.ctrlBrowseLocalCacheFolder.Size = new System.Drawing.Size(347, 24);
            this.ctrlBrowseLocalCacheFolder.TabIndex = 110;
            // 
            // chxMultiThreads
            // 
            this.chxMultiThreads.AutoSize = true;
            this.chxMultiThreads.Location = new System.Drawing.Point(356, 36);
            this.chxMultiThreads.Name = "chxMultiThreads";
            this.chxMultiThreads.Size = new System.Drawing.Size(97, 17);
            this.chxMultiThreads.TabIndex = 28;
            this.chxMultiThreads.Text = "Multi Threaded";
            this.chxMultiThreads.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Viewport Anti Aliasing Level:";
            // 
            // cmbViewportAntiAliasingLevel
            // 
            this.cmbViewportAntiAliasingLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewportAntiAliasingLevel.FormattingEnabled = true;
            this.cmbViewportAntiAliasingLevel.Location = new System.Drawing.Point(179, 10);
            this.cmbViewportAntiAliasingLevel.Name = "cmbViewportAntiAliasingLevel";
            this.cmbViewportAntiAliasingLevel.Size = new System.Drawing.Size(143, 21);
            this.cmbViewportAntiAliasingLevel.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Terrain Objects Anti Aliasing Level:";
            // 
            // cmbTerrainObjectsAntiAliasingLevel
            // 
            this.cmbTerrainObjectsAntiAliasingLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTerrainObjectsAntiAliasingLevel.FormattingEnabled = true;
            this.cmbTerrainObjectsAntiAliasingLevel.Location = new System.Drawing.Point(179, 35);
            this.cmbTerrainObjectsAntiAliasingLevel.Name = "cmbTerrainObjectsAntiAliasingLevel";
            this.cmbTerrainObjectsAntiAliasingLevel.Size = new System.Drawing.Size(143, 21);
            this.cmbTerrainObjectsAntiAliasingLevel.TabIndex = 27;
            // 
            // chxMultiScreenDevice
            // 
            this.chxMultiScreenDevice.AutoSize = true;
            this.chxMultiScreenDevice.Location = new System.Drawing.Point(356, 13);
            this.chxMultiScreenDevice.Name = "chxMultiScreenDevice";
            this.chxMultiScreenDevice.Size = new System.Drawing.Size(85, 17);
            this.chxMultiScreenDevice.TabIndex = 15;
            this.chxMultiScreenDevice.Text = "Multi Screen";
            this.chxMultiScreenDevice.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Number Background Threads:";
            // 
            // ntxNumBackgroundThreads
            // 
            this.ntxNumBackgroundThreads.Location = new System.Drawing.Point(179, 60);
            this.ntxNumBackgroundThreads.Name = "ntxNumBackgroundThreads";
            this.ntxNumBackgroundThreads.Size = new System.Drawing.Size(48, 20);
            this.ntxNumBackgroundThreads.TabIndex = 16;
            this.ntxNumBackgroundThreads.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "Local Cache Folder:";
            // 
            // ctrlSmallDeviceParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlSmallDeviceParams";
            this.Size = new System.Drawing.Size(469, 133);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.NumericTextBox ntxLocalCacheMaxSize;
        private System.Windows.Forms.Label label19;
        private Controls.CtrlBrowseControl ctrlBrowseLocalCacheFolder;
        private System.Windows.Forms.CheckBox chxMultiThreads;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbViewportAntiAliasingLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTerrainObjectsAntiAliasingLevel;
        private System.Windows.Forms.CheckBox chxMultiScreenDevice;
        private System.Windows.Forms.Label label2;
        private Controls.NumericTextBox ntxNumBackgroundThreads;
        private System.Windows.Forms.Label label4;
    }
}
