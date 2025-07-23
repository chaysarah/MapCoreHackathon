namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmLoadObjectsFromRawVectorData
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
            this.btnLoadObjectsFromRawVectorData = new System.Windows.Forms.Button();
            this.rawVectorParams1 = new MCTester.Controls.CtrlRawVectorParams();
            this.ctrlLayerGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.browseLayerCtrl = new MCTester.Controls.CtrlBrowseControl();
            this.chxClearObjectSchemesCache = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLoadObjectsFromRawVectorData
            // 
            this.btnLoadObjectsFromRawVectorData.Location = new System.Drawing.Point(245, 661);
            this.btnLoadObjectsFromRawVectorData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLoadObjectsFromRawVectorData.Name = "btnLoadObjectsFromRawVectorData";
            this.btnLoadObjectsFromRawVectorData.Size = new System.Drawing.Size(202, 28);
            this.btnLoadObjectsFromRawVectorData.TabIndex = 1;
            this.btnLoadObjectsFromRawVectorData.Text = "Load Objects From Raw Vector Data";
            this.btnLoadObjectsFromRawVectorData.UseVisualStyleBackColor = true;
            this.btnLoadObjectsFromRawVectorData.Click += new System.EventHandler(this.btnLoadObjectsFromRawVectorData_Click);
            // 
            // rawVectorParams1
            // 
            this.rawVectorParams1.Location = new System.Drawing.Point(1, 170);
            this.rawVectorParams1.Margin = new System.Windows.Forms.Padding(2);
            this.rawVectorParams1.Name = "rawVectorParams1";
            this.rawVectorParams1.Size = new System.Drawing.Size(686, 487);
            this.rawVectorParams1.TabIndex = 0;
            this.rawVectorParams1.TargetGridCoordinateSystem = null;
            // 
            // ctrlLayerGridCoordinateSystem
            // 
            this.ctrlLayerGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlLayerGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlLayerGridCoordinateSystem.GroupBoxText = "Source Grid Coordinate System";
            this.ctrlLayerGridCoordinateSystem.IsEditable = false;
            this.ctrlLayerGridCoordinateSystem.Location = new System.Drawing.Point(4, 35);
            this.ctrlLayerGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlLayerGridCoordinateSystem.Name = "ctrlLayerGridCoordinateSystem";
            this.ctrlLayerGridCoordinateSystem.Size = new System.Drawing.Size(330, 132);
            this.ctrlLayerGridCoordinateSystem.TabIndex = 56;
            // 
            // browseLayerCtrl
            // 
            this.browseLayerCtrl.AutoSize = true;
            this.browseLayerCtrl.FileName = "";
            this.browseLayerCtrl.Filter = "";
            this.browseLayerCtrl.IsFolderDialog = false;
            this.browseLayerCtrl.IsFullPath = true;
            this.browseLayerCtrl.IsSaveFile = false;
            this.browseLayerCtrl.LabelCaption = "File Name:   ";
            this.browseLayerCtrl.Location = new System.Drawing.Point(76, 11);
            this.browseLayerCtrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.browseLayerCtrl.MinimumSize = new System.Drawing.Size(100, 24);
            this.browseLayerCtrl.MultiFilesSelect = false;
            this.browseLayerCtrl.Name = "browseLayerCtrl";
            this.browseLayerCtrl.Prefix = "";
            this.browseLayerCtrl.Size = new System.Drawing.Size(388, 24);
            this.browseLayerCtrl.TabIndex = 55;
            // 
            // chxClearObjectSchemesCache
            // 
            this.chxClearObjectSchemesCache.AutoSize = true;
            this.chxClearObjectSchemesCache.Location = new System.Drawing.Point(483, 13);
            this.chxClearObjectSchemesCache.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chxClearObjectSchemesCache.Name = "chxClearObjectSchemesCache";
            this.chxClearObjectSchemesCache.Size = new System.Drawing.Size(165, 17);
            this.chxClearObjectSchemesCache.TabIndex = 57;
            this.chxClearObjectSchemesCache.Text = "Clear Object Schemes Cache";
            this.chxClearObjectSchemesCache.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "File Name:";
            // 
            // frmLoadObjectsFromRawVectorData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(688, 699);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chxClearObjectSchemesCache);
            this.Controls.Add(this.ctrlLayerGridCoordinateSystem);
            this.Controls.Add(this.browseLayerCtrl);
            this.Controls.Add(this.btnLoadObjectsFromRawVectorData);
            this.Controls.Add(this.rawVectorParams1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmLoadObjectsFromRawVectorData";
            this.Text = "Load Objects From Raw Vector Data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtrlRawVectorParams rawVectorParams1;
        private System.Windows.Forms.Button btnLoadObjectsFromRawVectorData;
        private Controls.CtrlGridCoordinateSystem ctrlLayerGridCoordinateSystem;
        private Controls.CtrlBrowseControl browseLayerCtrl;
        private System.Windows.Forms.CheckBox chxClearObjectSchemesCache;
        private System.Windows.Forms.Label label1;
    }
}