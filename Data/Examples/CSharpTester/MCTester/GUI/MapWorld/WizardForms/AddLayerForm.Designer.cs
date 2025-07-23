namespace MCTester.MapWorld.WizardForms
{
    partial class AddLayerForm
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
            this.lstLayers = new System.Windows.Forms.ListBox();
            this.m_radCreateNew = new System.Windows.Forms.RadioButton();
            this.m_radUseExisting = new System.Windows.Forms.RadioButton();
            this.gbCreateNewLayer = new System.Windows.Forms.GroupBox();
            this.ucLayerParams1 = new MCTester.Controls.ucLayerParams();
            this.gbLoadFromFile = new System.Windows.Forms.GroupBox();
            this.txtBaseDirectory = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLoadBaseDir = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLoadFileName = new System.Windows.Forms.Button();
            this.m_radLoadFromFile = new System.Windows.Forms.RadioButton();
            this.gbUseExisting = new System.Windows.Forms.GroupBox();
            this.gbSelectFromServer = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.lblCSWServerLayersCount = new System.Windows.Forms.Label();
            this.btnCSWOpenServerLayers = new System.Windows.Forms.Button();
            this.txtCSWServerPath = new System.Windows.Forms.TextBox();
            this.btnWMSOpenServerLayers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWMSServerLayersCount = new System.Windows.Forms.Label();
            this.txtWMSServerPath = new System.Windows.Forms.TextBox();
            this.btnRequestParams = new System.Windows.Forms.Button();
            this.btnWCSOpenServerLayers = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lblWCSServerLayersCount = new System.Windows.Forms.Label();
            this.btnWMTSOpenServerLayers = new System.Windows.Forms.Button();
            this.lblMapCoreServerLayersCount = new System.Windows.Forms.Label();
            this.lblWMTSServerLayersCount = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtWCSServerPath = new System.Windows.Forms.TextBox();
            this.btnOpenServerLayers = new System.Windows.Forms.Button();
            this.txtMapCoreServerPath = new System.Windows.Forms.TextBox();
            this.txtWMTSServerPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_radSelectFromServer = new System.Windows.Forms.RadioButton();
            this.gbCreateNewLayer.SuspendLayout();
            this.gbLoadFromFile.SuspendLayout();
            this.gbUseExisting.SuspendLayout();
            this.gbSelectFromServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(1011, 766);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstLayers
            // 
            this.lstLayers.FormattingEnabled = true;
            this.lstLayers.Location = new System.Drawing.Point(2, 18);
            this.lstLayers.Name = "lstLayers";
            this.lstLayers.Size = new System.Drawing.Size(273, 251);
            this.lstLayers.TabIndex = 12;
            // 
            // m_radCreateNew
            // 
            this.m_radCreateNew.AutoSize = true;
            this.m_radCreateNew.Location = new System.Drawing.Point(290, 6);
            this.m_radCreateNew.Name = "m_radCreateNew";
            this.m_radCreateNew.Size = new System.Drawing.Size(251, 35);
            this.m_radCreateNew.TabIndex = 11;
            this.m_radCreateNew.Text = "Create New Layer";
            this.m_radCreateNew.UseVisualStyleBackColor = true;
            this.m_radCreateNew.CheckedChanged += new System.EventHandler(this.m_radCreateNew_CheckedChanged);
            // 
            // m_radUseExisting
            // 
            this.m_radUseExisting.AutoSize = true;
            this.m_radUseExisting.Checked = true;
            this.m_radUseExisting.Location = new System.Drawing.Point(5, 6);
            this.m_radUseExisting.Name = "m_radUseExisting";
            this.m_radUseExisting.Size = new System.Drawing.Size(191, 35);
            this.m_radUseExisting.TabIndex = 10;
            this.m_radUseExisting.TabStop = true;
            this.m_radUseExisting.Text = "Use Existing:";
            this.m_radUseExisting.UseVisualStyleBackColor = true;
            this.m_radUseExisting.CheckedChanged += new System.EventHandler(this.m_radUseExisting_CheckedChanged);
            // 
            // gbCreateNewLayer
            // 
            this.gbCreateNewLayer.Controls.Add(this.ucLayerParams1);
            this.gbCreateNewLayer.Location = new System.Drawing.Point(288, 23);
            this.gbCreateNewLayer.Name = "gbCreateNewLayer";
            this.gbCreateNewLayer.Size = new System.Drawing.Size(798, 737);
            this.gbCreateNewLayer.TabIndex = 13;
            this.gbCreateNewLayer.TabStop = false;
            // 
            // ucLayerParams1
            // 
            this.ucLayerParams1.Location = new System.Drawing.Point(6, 17);
            this.ucLayerParams1.Name = "ucLayerParams1";
            this.ucLayerParams1.Size = new System.Drawing.Size(786, 714);
            this.ucLayerParams1.TabIndex = 70;
            // 
            // gbLoadFromFile
            // 
            this.gbLoadFromFile.Controls.Add(this.txtBaseDirectory);
            this.gbLoadFromFile.Controls.Add(this.label4);
            this.gbLoadFromFile.Controls.Add(this.btnLoadBaseDir);
            this.gbLoadFromFile.Controls.Add(this.txtFileName);
            this.gbLoadFromFile.Controls.Add(this.label5);
            this.gbLoadFromFile.Controls.Add(this.btnLoadFileName);
            this.gbLoadFromFile.Location = new System.Drawing.Point(8, 324);
            this.gbLoadFromFile.Name = "gbLoadFromFile";
            this.gbLoadFromFile.Size = new System.Drawing.Size(273, 88);
            this.gbLoadFromFile.TabIndex = 15;
            this.gbLoadFromFile.TabStop = false;
            // 
            // txtBaseDirectory
            // 
            this.txtBaseDirectory.Location = new System.Drawing.Point(88, 45);
            this.txtBaseDirectory.Name = "txtBaseDirectory";
            this.txtBaseDirectory.Size = new System.Drawing.Size(145, 38);
            this.txtBaseDirectory.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 31);
            this.label4.TabIndex = 10;
            this.label4.Text = "Base Directory:";
            // 
            // btnLoadBaseDir
            // 
            this.btnLoadBaseDir.Location = new System.Drawing.Point(237, 42);
            this.btnLoadBaseDir.Name = "btnLoadBaseDir";
            this.btnLoadBaseDir.Size = new System.Drawing.Size(26, 23);
            this.btnLoadBaseDir.TabIndex = 9;
            this.btnLoadBaseDir.Text = "...";
            this.btnLoadBaseDir.UseVisualStyleBackColor = true;
            this.btnLoadBaseDir.Click += new System.EventHandler(this.btnLoadBaseDir_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(88, 19);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(145, 38);
            this.txtFileName.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 31);
            this.label5.TabIndex = 7;
            this.label5.Text = "File Name:";
            // 
            // btnLoadFileName
            // 
            this.btnLoadFileName.Location = new System.Drawing.Point(237, 17);
            this.btnLoadFileName.Name = "btnLoadFileName";
            this.btnLoadFileName.Size = new System.Drawing.Size(26, 23);
            this.btnLoadFileName.TabIndex = 6;
            this.btnLoadFileName.Text = "...";
            this.btnLoadFileName.UseVisualStyleBackColor = true;
            this.btnLoadFileName.Click += new System.EventHandler(this.btnbtnLoadFileName_Click);
            // 
            // m_radLoadFromFile
            // 
            this.m_radLoadFromFile.AutoSize = true;
            this.m_radLoadFromFile.Location = new System.Drawing.Point(9, 309);
            this.m_radLoadFromFile.Name = "m_radLoadFromFile";
            this.m_radLoadFromFile.Size = new System.Drawing.Size(213, 35);
            this.m_radLoadFromFile.TabIndex = 14;
            this.m_radLoadFromFile.Text = "Load From File";
            this.m_radLoadFromFile.UseVisualStyleBackColor = true;
            this.m_radLoadFromFile.CheckedChanged += new System.EventHandler(this.m_radLoadFromFile_CheckedChanged);
            // 
            // gbUseExisting
            // 
            this.gbUseExisting.Controls.Add(this.lstLayers);
            this.gbUseExisting.Location = new System.Drawing.Point(4, 22);
            this.gbUseExisting.Margin = new System.Windows.Forms.Padding(2);
            this.gbUseExisting.Name = "gbUseExisting";
            this.gbUseExisting.Padding = new System.Windows.Forms.Padding(2);
            this.gbUseExisting.Size = new System.Drawing.Size(279, 279);
            this.gbUseExisting.TabIndex = 16;
            this.gbUseExisting.TabStop = false;
            // 
            // gbSelectFromServer
            // 
            this.gbSelectFromServer.Controls.Add(this.label39);
            this.gbSelectFromServer.Controls.Add(this.lblCSWServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.btnCSWOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.txtCSWServerPath);
            this.gbSelectFromServer.Controls.Add(this.btnWMSOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.label1);
            this.gbSelectFromServer.Controls.Add(this.lblWMSServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.txtWMSServerPath);
            this.gbSelectFromServer.Controls.Add(this.btnRequestParams);
            this.gbSelectFromServer.Controls.Add(this.btnWCSOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.label12);
            this.gbSelectFromServer.Controls.Add(this.lblWCSServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.btnWMTSOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.lblMapCoreServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.lblWMTSServerLayersCount);
            this.gbSelectFromServer.Controls.Add(this.label28);
            this.gbSelectFromServer.Controls.Add(this.txtWCSServerPath);
            this.gbSelectFromServer.Controls.Add(this.btnOpenServerLayers);
            this.gbSelectFromServer.Controls.Add(this.txtMapCoreServerPath);
            this.gbSelectFromServer.Controls.Add(this.txtWMTSServerPath);
            this.gbSelectFromServer.Controls.Add(this.label3);
            this.gbSelectFromServer.Location = new System.Drawing.Point(8, 439);
            this.gbSelectFromServer.Name = "gbSelectFromServer";
            this.gbSelectFromServer.Size = new System.Drawing.Size(275, 321);
            this.gbSelectFromServer.TabIndex = 17;
            this.gbSelectFromServer.TabStop = false;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(7, 236);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(247, 31);
            this.label39.TabIndex = 148;
            this.label39.Text = "CSW Layer Server:";
            // 
            // lblCSWServerLayersCount
            // 
            this.lblCSWServerLayersCount.AutoSize = true;
            this.lblCSWServerLayersCount.Location = new System.Drawing.Point(197, 275);
            this.lblCSWServerLayersCount.Name = "lblCSWServerLayersCount";
            this.lblCSWServerLayersCount.Size = new System.Drawing.Size(109, 31);
            this.lblCSWServerLayersCount.TabIndex = 147;
            this.lblCSWServerLayersCount.Text = "0 layers";
            // 
            // btnCSWOpenServerLayers
            // 
            this.btnCSWOpenServerLayers.Location = new System.Drawing.Point(243, 253);
            this.btnCSWOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnCSWOpenServerLayers.Name = "btnCSWOpenServerLayers";
            this.btnCSWOpenServerLayers.Size = new System.Drawing.Size(22, 22);
            this.btnCSWOpenServerLayers.TabIndex = 146;
            this.btnCSWOpenServerLayers.Text = "...";
            this.btnCSWOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnCSWOpenServerLayers.Click += new System.EventHandler(this.btnCSWOpenServerLayers_Click);
            // 
            // txtCSWServerPath
            // 
            this.txtCSWServerPath.Location = new System.Drawing.Point(11, 253);
            this.txtCSWServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtCSWServerPath.Name = "txtCSWServerPath";
            this.txtCSWServerPath.Size = new System.Drawing.Size(229, 38);
            this.txtCSWServerPath.TabIndex = 145;
            // 
            // btnWMSOpenServerLayers
            // 
            this.btnWMSOpenServerLayers.Location = new System.Drawing.Point(243, 196);
            this.btnWMSOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnWMSOpenServerLayers.Name = "btnWMSOpenServerLayers";
            this.btnWMSOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnWMSOpenServerLayers.TabIndex = 108;
            this.btnWMSOpenServerLayers.Text = "...";
            this.btnWMSOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnWMSOpenServerLayers.Click += new System.EventHandler(this.btnWMSOpenServerLayers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 31);
            this.label1.TabIndex = 105;
            this.label1.Text = "WMS Layer Server:";
            // 
            // lblWMSServerLayersCount
            // 
            this.lblWMSServerLayersCount.AutoSize = true;
            this.lblWMSServerLayersCount.Location = new System.Drawing.Point(194, 219);
            this.lblWMSServerLayersCount.Name = "lblWMSServerLayersCount";
            this.lblWMSServerLayersCount.Size = new System.Drawing.Size(109, 31);
            this.lblWMSServerLayersCount.TabIndex = 107;
            this.lblWMSServerLayersCount.Text = "0 layers";
            // 
            // txtWMSServerPath
            // 
            this.txtWMSServerPath.Location = new System.Drawing.Point(10, 196);
            this.txtWMSServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtWMSServerPath.Name = "txtWMSServerPath";
            this.txtWMSServerPath.Size = new System.Drawing.Size(230, 38);
            this.txtWMSServerPath.TabIndex = 106;
            // 
            // btnRequestParams
            // 
            this.btnRequestParams.Location = new System.Drawing.Point(6, 292);
            this.btnRequestParams.Name = "btnRequestParams";
            this.btnRequestParams.Size = new System.Drawing.Size(134, 23);
            this.btnRequestParams.TabIndex = 104;
            this.btnRequestParams.Text = "Server Request Params";
            this.btnRequestParams.UseVisualStyleBackColor = true;
            this.btnRequestParams.Click += new System.EventHandler(this.btnRequestParams_Click);
            // 
            // btnWCSOpenServerLayers
            // 
            this.btnWCSOpenServerLayers.Location = new System.Drawing.Point(243, 141);
            this.btnWCSOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnWCSOpenServerLayers.Name = "btnWCSOpenServerLayers";
            this.btnWCSOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnWCSOpenServerLayers.TabIndex = 103;
            this.btnWCSOpenServerLayers.Text = "...";
            this.btnWCSOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnWCSOpenServerLayers.Click += new System.EventHandler(this.btnWCSOpenServerLayers_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(247, 31);
            this.label12.TabIndex = 100;
            this.label12.Text = "WCS Layer Server:";
            // 
            // lblWCSServerLayersCount
            // 
            this.lblWCSServerLayersCount.AutoSize = true;
            this.lblWCSServerLayersCount.Location = new System.Drawing.Point(195, 164);
            this.lblWCSServerLayersCount.Name = "lblWCSServerLayersCount";
            this.lblWCSServerLayersCount.Size = new System.Drawing.Size(109, 31);
            this.lblWCSServerLayersCount.TabIndex = 102;
            this.lblWCSServerLayersCount.Text = "0 layers";
            // 
            // btnWMTSOpenServerLayers
            // 
            this.btnWMTSOpenServerLayers.Location = new System.Drawing.Point(243, 89);
            this.btnWMTSOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnWMTSOpenServerLayers.Name = "btnWMTSOpenServerLayers";
            this.btnWMTSOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnWMTSOpenServerLayers.TabIndex = 99;
            this.btnWMTSOpenServerLayers.Text = "...";
            this.btnWMTSOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnWMTSOpenServerLayers.Click += new System.EventHandler(this.btnWMTSOpenServerLayers_Click);
            // 
            // lblMapCoreServerLayersCount
            // 
            this.lblMapCoreServerLayersCount.AutoSize = true;
            this.lblMapCoreServerLayersCount.Location = new System.Drawing.Point(194, 62);
            this.lblMapCoreServerLayersCount.Name = "lblMapCoreServerLayersCount";
            this.lblMapCoreServerLayersCount.Size = new System.Drawing.Size(109, 31);
            this.lblMapCoreServerLayersCount.TabIndex = 92;
            this.lblMapCoreServerLayersCount.Text = "0 layers";
            // 
            // lblWMTSServerLayersCount
            // 
            this.lblWMTSServerLayersCount.AutoSize = true;
            this.lblWMTSServerLayersCount.Location = new System.Drawing.Point(194, 112);
            this.lblWMTSServerLayersCount.Name = "lblWMTSServerLayersCount";
            this.lblWMTSServerLayersCount.Size = new System.Drawing.Size(109, 31);
            this.lblWMTSServerLayersCount.TabIndex = 98;
            this.lblWMTSServerLayersCount.Text = "0 layers";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(8, 20);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(302, 31);
            this.label28.TabIndex = 89;
            this.label28.Text = "Map Core Layer Server:";
            // 
            // txtWCSServerPath
            // 
            this.txtWCSServerPath.Location = new System.Drawing.Point(10, 141);
            this.txtWCSServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtWCSServerPath.Name = "txtWCSServerPath";
            this.txtWCSServerPath.Size = new System.Drawing.Size(230, 38);
            this.txtWCSServerPath.TabIndex = 101;
            // 
            // btnOpenServerLayers
            // 
            this.btnOpenServerLayers.Location = new System.Drawing.Point(243, 38);
            this.btnOpenServerLayers.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpenServerLayers.Name = "btnOpenServerLayers";
            this.btnOpenServerLayers.Size = new System.Drawing.Size(22, 20);
            this.btnOpenServerLayers.TabIndex = 91;
            this.btnOpenServerLayers.Text = "...";
            this.btnOpenServerLayers.UseVisualStyleBackColor = true;
            this.btnOpenServerLayers.Click += new System.EventHandler(this.btnOpenServerLayers_Click);
            // 
            // txtMapCoreServerPath
            // 
            this.txtMapCoreServerPath.Location = new System.Drawing.Point(10, 38);
            this.txtMapCoreServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtMapCoreServerPath.Name = "txtMapCoreServerPath";
            this.txtMapCoreServerPath.Size = new System.Drawing.Size(230, 38);
            this.txtMapCoreServerPath.TabIndex = 90;
            // 
            // txtWMTSServerPath
            // 
            this.txtWMTSServerPath.Location = new System.Drawing.Point(10, 89);
            this.txtWMTSServerPath.Margin = new System.Windows.Forms.Padding(2);
            this.txtWMTSServerPath.Name = "txtWMTSServerPath";
            this.txtWMTSServerPath.Size = new System.Drawing.Size(230, 38);
            this.txtWMTSServerPath.TabIndex = 97;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(266, 31);
            this.label3.TabIndex = 96;
            this.label3.Text = "WMTS Layer Server:";
            // 
            // m_radSelectFromServer
            // 
            this.m_radSelectFromServer.AutoSize = true;
            this.m_radSelectFromServer.Location = new System.Drawing.Point(9, 419);
            this.m_radSelectFromServer.Name = "m_radSelectFromServer";
            this.m_radSelectFromServer.Size = new System.Drawing.Size(265, 35);
            this.m_radSelectFromServer.TabIndex = 14;
            this.m_radSelectFromServer.Text = "Select From Server";
            this.m_radSelectFromServer.UseVisualStyleBackColor = true;
            this.m_radSelectFromServer.CheckedChanged += new System.EventHandler(this.m_radSelectFromServer_CheckedChanged);
            // 
            // AddLayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1098, 790);
            this.Controls.Add(this.gbSelectFromServer);
            this.Controls.Add(this.m_radUseExisting);
            this.Controls.Add(this.m_radLoadFromFile);
            this.Controls.Add(this.m_radSelectFromServer);
            this.Controls.Add(this.m_radCreateNew);
            this.Controls.Add(this.gbUseExisting);
            this.Controls.Add(this.gbLoadFromFile);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbCreateNewLayer);
            this.Name = "AddLayerForm";
            this.Text = "LayerWizardForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddLayerForm_FormClosed);
            this.Load += new System.EventHandler(this.AddLayerForm_Load);
            this.gbCreateNewLayer.ResumeLayout(false);
            this.gbLoadFromFile.ResumeLayout(false);
            this.gbLoadFromFile.PerformLayout();
            this.gbUseExisting.ResumeLayout(false);
            this.gbSelectFromServer.ResumeLayout(false);
            this.gbSelectFromServer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstLayers;
        private System.Windows.Forms.RadioButton m_radCreateNew;
        private System.Windows.Forms.RadioButton m_radUseExisting;
        private System.Windows.Forms.GroupBox gbCreateNewLayer;
        private System.Windows.Forms.GroupBox gbLoadFromFile;
        private System.Windows.Forms.TextBox txtBaseDirectory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoadBaseDir;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLoadFileName;
        private System.Windows.Forms.RadioButton m_radLoadFromFile;
        private System.Windows.Forms.GroupBox gbUseExisting;
        private System.Windows.Forms.GroupBox gbSelectFromServer;
        private System.Windows.Forms.RadioButton m_radSelectFromServer;
        private System.Windows.Forms.Label lblMapCoreServerLayersCount;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button btnOpenServerLayers;
        private System.Windows.Forms.TextBox txtMapCoreServerPath;
        private System.Windows.Forms.Button btnWMTSOpenServerLayers;
        private System.Windows.Forms.Label lblWMTSServerLayersCount;
        private System.Windows.Forms.TextBox txtWMTSServerPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnWCSOpenServerLayers;
        private System.Windows.Forms.Label lblWCSServerLayersCount;
        private System.Windows.Forms.TextBox txtWCSServerPath;
        private System.Windows.Forms.Label label12;
        private Controls.ucLayerParams ucLayerParams1;
        private System.Windows.Forms.Button btnRequestParams;
        private System.Windows.Forms.Button btnWMSOpenServerLayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWMSServerLayersCount;
        private System.Windows.Forms.TextBox txtWMSServerPath;
        private System.Windows.Forms.Button btnCSWOpenServerLayers;
        private System.Windows.Forms.TextBox txtCSWServerPath;
        private System.Windows.Forms.Label lblCSWServerLayersCount;
        private System.Windows.Forms.Label label39;
    }
}