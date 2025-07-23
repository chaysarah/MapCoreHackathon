namespace MCTester.ObjectWorld.OverlayManagerWorld.WizardForms
{
    partial class OverlayManagerWizardForm
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
            this.m_radUseExisting = new System.Windows.Forms.RadioButton();
            this.m_radCreateNew = new System.Windows.Forms.RadioButton();
            this.lstOverlayManagers = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstOverlays = new System.Windows.Forms.ListBox();
            this.btnRemoveOverlay = new System.Windows.Forms.Button();
            this.btnAddOverlay = new System.Windows.Forms.Button();
            this.ctrlOMGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.gbCreateNew = new System.Windows.Forms.GroupBox();
            this.ntxScaleFactor = new MCTester.Controls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbCreateNew.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_radUseExisting
            // 
            this.m_radUseExisting.AutoSize = true;
            this.m_radUseExisting.Location = new System.Drawing.Point(12, 12);
            this.m_radUseExisting.Name = "m_radUseExisting";
            this.m_radUseExisting.Size = new System.Drawing.Size(86, 17);
            this.m_radUseExisting.TabIndex = 0;
            this.m_radUseExisting.Text = "Use Existing:";
            this.m_radUseExisting.UseVisualStyleBackColor = true;
            // 
            // m_radCreateNew
            // 
            this.m_radCreateNew.AutoSize = true;
            this.m_radCreateNew.Location = new System.Drawing.Point(12, 214);
            this.m_radCreateNew.Name = "m_radCreateNew";
            this.m_radCreateNew.Size = new System.Drawing.Size(81, 17);
            this.m_radCreateNew.TabIndex = 1;
            this.m_radCreateNew.Text = "Create New";
            this.m_radCreateNew.UseVisualStyleBackColor = true;
            // 
            // lstOverlayManagers
            // 
            this.lstOverlayManagers.FormattingEnabled = true;
            this.lstOverlayManagers.Location = new System.Drawing.Point(12, 35);
            this.lstOverlayManagers.Name = "lstOverlayManagers";
            this.lstOverlayManagers.Size = new System.Drawing.Size(592, 173);
            this.lstOverlayManagers.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(529, 457);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(344, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 155);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Overlays";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstOverlays);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnRemoveOverlay);
            this.splitContainer1.Panel2.Controls.Add(this.btnAddOverlay);
            this.splitContainer1.Size = new System.Drawing.Size(240, 136);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 0;
            // 
            // lstOverlays
            // 
            this.lstOverlays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOverlays.FormattingEnabled = true;
            this.lstOverlays.Location = new System.Drawing.Point(0, 0);
            this.lstOverlays.Name = "lstOverlays";
            this.lstOverlays.Size = new System.Drawing.Size(165, 136);
            this.lstOverlays.TabIndex = 10;
            // 
            // btnRemoveOverlay
            // 
            this.btnRemoveOverlay.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRemoveOverlay.Location = new System.Drawing.Point(0, 21);
            this.btnRemoveOverlay.Name = "btnRemoveOverlay";
            this.btnRemoveOverlay.Size = new System.Drawing.Size(71, 24);
            this.btnRemoveOverlay.TabIndex = 1;
            this.btnRemoveOverlay.Text = "Remove";
            this.btnRemoveOverlay.UseVisualStyleBackColor = true;
            this.btnRemoveOverlay.Click += new System.EventHandler(this.btnRemoveOverlay_Click);
            // 
            // btnAddOverlay
            // 
            this.btnAddOverlay.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddOverlay.Location = new System.Drawing.Point(0, 0);
            this.btnAddOverlay.Name = "btnAddOverlay";
            this.btnAddOverlay.Size = new System.Drawing.Size(71, 21);
            this.btnAddOverlay.TabIndex = 0;
            this.btnAddOverlay.Text = "Add";
            this.btnAddOverlay.UseVisualStyleBackColor = true;
            this.btnAddOverlay.Click += new System.EventHandler(this.btnAddOverlay_Click);
            // 
            // ctrlOMGridCoordinateSystem
            // 
            this.ctrlOMGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlOMGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlOMGridCoordinateSystem.GroupBoxText = "Grid Coordinate System";
            this.ctrlOMGridCoordinateSystem.IsEditable = false;
            this.ctrlOMGridCoordinateSystem.Location = new System.Drawing.Point(5, 16);
            this.ctrlOMGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlOMGridCoordinateSystem.Name = "ctrlOMGridCoordinateSystem";
            this.ctrlOMGridCoordinateSystem.Size = new System.Drawing.Size(335, 155);
            this.ctrlOMGridCoordinateSystem.TabIndex = 13;
            // 
            // gbCreateNew
            // 
            this.gbCreateNew.Controls.Add(this.ntxScaleFactor);
            this.gbCreateNew.Controls.Add(this.label9);
            this.gbCreateNew.Controls.Add(this.groupBox1);
            this.gbCreateNew.Controls.Add(this.ctrlOMGridCoordinateSystem);
            this.gbCreateNew.Location = new System.Drawing.Point(12, 234);
            this.gbCreateNew.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCreateNew.Name = "gbCreateNew";
            this.gbCreateNew.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbCreateNew.Size = new System.Drawing.Size(592, 207);
            this.gbCreateNew.TabIndex = 76;
            this.gbCreateNew.TabStop = false;
            // 
            // ntxScaleFactor
            // 
            this.ntxScaleFactor.Location = new System.Drawing.Point(82, 179);
            this.ntxScaleFactor.Name = "ntxScaleFactor";
            this.ntxScaleFactor.Size = new System.Drawing.Size(47, 20);
            this.ntxScaleFactor.TabIndex = 73;
            this.ntxScaleFactor.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 181);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 74;
            this.label9.Text = "Scale Factor:";
            // 
            // OverlayManagerWizardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(617, 483);
            this.Controls.Add(this.gbCreateNew);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstOverlayManagers);
            this.Controls.Add(this.m_radUseExisting);
            this.Controls.Add(this.m_radCreateNew);
            this.Name = "OverlayManagerWizardForm";
            this.Text = "OverlayManagerWizardForm";
            this.VisibleChanged += new System.EventHandler(this.OverlayManagerWizardForm_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbCreateNew.ResumeLayout(false);
            this.gbCreateNew.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton m_radUseExisting;
        private System.Windows.Forms.RadioButton m_radCreateNew;
        private System.Windows.Forms.ListBox lstOverlayManagers;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstOverlays;
        private System.Windows.Forms.Button btnRemoveOverlay;
        private System.Windows.Forms.Button btnAddOverlay;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlOMGridCoordinateSystem;
        private System.Windows.Forms.GroupBox gbCreateNew;
        private Controls.NumericTextBox ntxScaleFactor;
        private System.Windows.Forms.Label label9;
    }
}