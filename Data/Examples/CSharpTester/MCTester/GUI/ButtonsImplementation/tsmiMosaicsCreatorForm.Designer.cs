namespace MCTester.ButtonsImplementation
{
    partial class tsmiMosaicsCreatorForm
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
            this.btnDisplayMosaics = new System.Windows.Forms.Button();
            this.ctrlBrowseDTMLayerDir = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.ctrlBrowseSourceFramesDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlBrowseDestinationMosaicsDir = new MCTester.Controls.CtrlBrowseControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxPixelNumX = new MCTester.Controls.NumericTextBox();
            this.ntxPixelNumY = new MCTester.Controls.NumericTextBox();
            this.ntxOverlapPercentage = new MCTester.Controls.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chxOpenOnWPFWindow = new System.Windows.Forms.CheckBox();
            this.ntxDefaultHeight = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxMaxCachSize = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDisplayMosaics
            // 
            this.btnDisplayMosaics.Location = new System.Drawing.Point(466, 403);
            this.btnDisplayMosaics.Name = "btnDisplayMosaics";
            this.btnDisplayMosaics.Size = new System.Drawing.Size(113, 23);
            this.btnDisplayMosaics.TabIndex = 0;
            this.btnDisplayMosaics.Text = "Display Mosaics";
            this.btnDisplayMosaics.UseVisualStyleBackColor = true;
            this.btnDisplayMosaics.Click += new System.EventHandler(this.btnDisplayMosaics_Click);
            // 
            // ctrlBrowseDTMLayerDir
            // 
            this.ctrlBrowseDTMLayerDir.AutoSize = true;
            this.ctrlBrowseDTMLayerDir.FileName = "";
            this.ctrlBrowseDTMLayerDir.Filter = "";
            this.ctrlBrowseDTMLayerDir.IsFolderDialog = true;
            this.ctrlBrowseDTMLayerDir.IsFullPath = true;
            this.ctrlBrowseDTMLayerDir.IsSaveFile = false;
            this.ctrlBrowseDTMLayerDir.LabelCaption = "DTM Directory:";
            this.ctrlBrowseDTMLayerDir.Location = new System.Drawing.Point(96, 12);
            this.ctrlBrowseDTMLayerDir.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseDTMLayerDir.MultiFilesSelect = false;
            this.ctrlBrowseDTMLayerDir.Name = "ctrlBrowseDTMLayerDir";
            this.ctrlBrowseDTMLayerDir.Prefix = "";
            this.ctrlBrowseDTMLayerDir.Size = new System.Drawing.Size(483, 24);
            this.ctrlBrowseDTMLayerDir.TabIndex = 1;
            // 
            // ctrlGridCoordinateSystem
            // 
            this.ctrlGridCoordinateSystem.EnableNewCoordSysCreation = true;
            this.ctrlGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlGridCoordinateSystem.GroupBoxText = "Grid Coordinate System";
            this.ctrlGridCoordinateSystem.IsEditable = false;
            this.ctrlGridCoordinateSystem.Location = new System.Drawing.Point(12, 42);
            this.ctrlGridCoordinateSystem.Name = "ctrlGridCoordinateSystem";
            this.ctrlGridCoordinateSystem.Size = new System.Drawing.Size(567, 155);
            this.ctrlGridCoordinateSystem.TabIndex = 2;
            // 
            // ctrlBrowseSourceFramesDirectory
            // 
            this.ctrlBrowseSourceFramesDirectory.AutoSize = true;
            this.ctrlBrowseSourceFramesDirectory.FileName = "";
            this.ctrlBrowseSourceFramesDirectory.Filter = "";
            this.ctrlBrowseSourceFramesDirectory.IsFolderDialog = true;
            this.ctrlBrowseSourceFramesDirectory.IsFullPath = true;
            this.ctrlBrowseSourceFramesDirectory.IsSaveFile = false;
            this.ctrlBrowseSourceFramesDirectory.LabelCaption = "Source Frames Directory:";
            this.ctrlBrowseSourceFramesDirectory.Location = new System.Drawing.Point(155, 203);
            this.ctrlBrowseSourceFramesDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseSourceFramesDirectory.MultiFilesSelect = false;
            this.ctrlBrowseSourceFramesDirectory.Name = "ctrlBrowseSourceFramesDirectory";
            this.ctrlBrowseSourceFramesDirectory.Prefix = "";
            this.ctrlBrowseSourceFramesDirectory.Size = new System.Drawing.Size(424, 24);
            this.ctrlBrowseSourceFramesDirectory.TabIndex = 3;
            // 
            // ctrlBrowseDestinationMosaicsDir
            // 
            this.ctrlBrowseDestinationMosaicsDir.AutoSize = true;
            this.ctrlBrowseDestinationMosaicsDir.FileName = "";
            this.ctrlBrowseDestinationMosaicsDir.Filter = "";
            this.ctrlBrowseDestinationMosaicsDir.IsFolderDialog = true;
            this.ctrlBrowseDestinationMosaicsDir.IsFullPath = true;
            this.ctrlBrowseDestinationMosaicsDir.IsSaveFile = false;
            this.ctrlBrowseDestinationMosaicsDir.LabelCaption = "Destination Mosaics Directory:";
            this.ctrlBrowseDestinationMosaicsDir.Location = new System.Drawing.Point(155, 233);
            this.ctrlBrowseDestinationMosaicsDir.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseDestinationMosaicsDir.MultiFilesSelect = false;
            this.ctrlBrowseDestinationMosaicsDir.Name = "ctrlBrowseDestinationMosaicsDir";
            this.ctrlBrowseDestinationMosaicsDir.Prefix = "";
            this.ctrlBrowseDestinationMosaicsDir.Size = new System.Drawing.Size(424, 24);
            this.ctrlBrowseDestinationMosaicsDir.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 319);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Pixel Num X:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 319);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pixel Num Y:";
            // 
            // ntxPixelNumX
            // 
            this.ntxPixelNumX.Location = new System.Drawing.Point(97, 316);
            this.ntxPixelNumX.Name = "ntxPixelNumX";
            this.ntxPixelNumX.Size = new System.Drawing.Size(51, 20);
            this.ntxPixelNumX.TabIndex = 7;
            this.ntxPixelNumX.Text = "8000";
            this.ntxPixelNumX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxPixelNumY
            // 
            this.ntxPixelNumY.Location = new System.Drawing.Point(280, 316);
            this.ntxPixelNumY.Name = "ntxPixelNumY";
            this.ntxPixelNumY.Size = new System.Drawing.Size(51, 20);
            this.ntxPixelNumY.TabIndex = 8;
            this.ntxPixelNumY.Text = "8000";
            this.ntxPixelNumY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxOverlapPercentage
            // 
            this.ntxOverlapPercentage.Location = new System.Drawing.Point(120, 342);
            this.ntxOverlapPercentage.Name = "ntxOverlapPercentage";
            this.ntxOverlapPercentage.Size = new System.Drawing.Size(51, 20);
            this.ntxOverlapPercentage.TabIndex = 12;
            this.ntxOverlapPercentage.Text = "1.0";
            this.ntxOverlapPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 345);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Overlap Percentage:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 345);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "%";
            // 
            // chxOpenOnWPFWindow
            // 
            this.chxOpenOnWPFWindow.AutoSize = true;
            this.chxOpenOnWPFWindow.Location = new System.Drawing.Point(12, 373);
            this.chxOpenOnWPFWindow.Name = "chxOpenOnWPFWindow";
            this.chxOpenOnWPFWindow.Size = new System.Drawing.Size(175, 17);
            this.chxOpenOnWPFWindow.TabIndex = 14;
            this.chxOpenOnWPFWindow.Text = "Open Frames On WPF Window";
            this.chxOpenOnWPFWindow.UseVisualStyleBackColor = true;
            // 
            // ntxDefaultHeight
            // 
            this.ntxDefaultHeight.Location = new System.Drawing.Point(96, 263);
            this.ntxDefaultHeight.Name = "ntxDefaultHeight";
            this.ntxDefaultHeight.Size = new System.Drawing.Size(51, 20);
            this.ntxDefaultHeight.TabIndex = 16;
            this.ntxDefaultHeight.Text = "0";
            this.ntxDefaultHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Default Height:";
            // 
            // ntxMaxCachSize
            // 
            this.ntxMaxCachSize.Location = new System.Drawing.Point(96, 289);
            this.ntxMaxCachSize.Name = "ntxMaxCachSize";
            this.ntxMaxCachSize.Size = new System.Drawing.Size(76, 20);
            this.ntxMaxCachSize.TabIndex = 18;
            this.ntxMaxCachSize.Text = "100000000";
            this.ntxMaxCachSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 292);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Max Cach Size:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "DTM Directory:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Destination Mosaics Directory:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Source Frames Directory:";
            // 
            // tsmiMosaicsCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(591, 431);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ntxMaxCachSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ntxDefaultHeight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chxOpenOnWPFWindow);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ntxOverlapPercentage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ntxPixelNumY);
            this.Controls.Add(this.ntxPixelNumX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlBrowseDestinationMosaicsDir);
            this.Controls.Add(this.ctrlBrowseSourceFramesDirectory);
            this.Controls.Add(this.ctrlGridCoordinateSystem);
            this.Controls.Add(this.ctrlBrowseDTMLayerDir);
            this.Controls.Add(this.btnDisplayMosaics);
            this.Name = "tsmiMosaicsCreatorForm";
            this.Text = "Mosaics Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDisplayMosaics;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseDTMLayerDir;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlGridCoordinateSystem;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseSourceFramesDirectory;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseDestinationMosaicsDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxPixelNumX;
        private MCTester.Controls.NumericTextBox ntxPixelNumY;
        private MCTester.Controls.NumericTextBox ntxOverlapPercentage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chxOpenOnWPFWindow;
        private MCTester.Controls.NumericTextBox ntxDefaultHeight;
        private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox ntxMaxCachSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}