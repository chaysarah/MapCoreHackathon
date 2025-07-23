namespace MCTester.General_Forms
{
    partial class frmImageDialog
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.rbCreateAsFile = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chxUseFileExtension = new System.Windows.Forms.CheckBox();
            this.rbCreateAsPixelBuffer = new System.Windows.Forms.RadioButton();
            this.rbCreateAsMemoryBuffer = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxWidth = new MCTester.Controls.NumericTextBox();
            this.ntxPixelFormat = new MCTester.Controls.NumericTextBox();
            this.ntxHeight = new MCTester.Controls.NumericTextBox();
            this.tcImages = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ctrlStrFileName = new MCTester.Controls.CtrlBrowseControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSelectFromExisting = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnCreateFromExisting = new System.Windows.Forms.Button();
            this.ntxExistsWidth = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ntxExistsHeight = new MCTester.Controls.NumericTextBox();
            this.cmbResizeFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chxFlipAroundY = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chxFlipAroundX = new System.Windows.Forms.CheckBox();
            this.lstImages = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tcImages.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(182, 252);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(94, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(15, 13);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(57, 13);
            this.label37.TabIndex = 138;
            this.label37.Text = "File Name:";
            // 
            // rbCreateAsFile
            // 
            this.rbCreateAsFile.AutoSize = true;
            this.rbCreateAsFile.Location = new System.Drawing.Point(4, 31);
            this.rbCreateAsFile.Name = "rbCreateAsFile";
            this.rbCreateAsFile.Size = new System.Drawing.Size(44, 17);
            this.rbCreateAsFile.TabIndex = 139;
            this.rbCreateAsFile.Text = "File ";
            this.rbCreateAsFile.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chxUseFileExtension);
            this.panel1.Controls.Add(this.rbCreateAsPixelBuffer);
            this.panel1.Controls.Add(this.rbCreateAsMemoryBuffer);
            this.panel1.Controls.Add(this.rbCreateAsFile);
            this.panel1.Location = new System.Drawing.Point(75, 105);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 83);
            this.panel1.TabIndex = 140;
            // 
            // chxUseFileExtension
            // 
            this.chxUseFileExtension.AutoSize = true;
            this.chxUseFileExtension.Checked = true;
            this.chxUseFileExtension.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxUseFileExtension.Location = new System.Drawing.Point(136, 9);
            this.chxUseFileExtension.Name = "chxUseFileExtension";
            this.chxUseFileExtension.Size = new System.Drawing.Size(113, 17);
            this.chxUseFileExtension.TabIndex = 142;
            this.chxUseFileExtension.Text = "Use File Extension";
            this.chxUseFileExtension.UseVisualStyleBackColor = true;
            // 
            // rbCreateAsPixelBuffer
            // 
            this.rbCreateAsPixelBuffer.AutoSize = true;
            this.rbCreateAsPixelBuffer.Location = new System.Drawing.Point(4, 54);
            this.rbCreateAsPixelBuffer.Name = "rbCreateAsPixelBuffer";
            this.rbCreateAsPixelBuffer.Size = new System.Drawing.Size(78, 17);
            this.rbCreateAsPixelBuffer.TabIndex = 141;
            this.rbCreateAsPixelBuffer.Text = "Pixel Buffer";
            this.rbCreateAsPixelBuffer.UseVisualStyleBackColor = true;
            // 
            // rbCreateAsMemoryBuffer
            // 
            this.rbCreateAsMemoryBuffer.AutoSize = true;
            this.rbCreateAsMemoryBuffer.Checked = true;
            this.rbCreateAsMemoryBuffer.Location = new System.Drawing.Point(4, 8);
            this.rbCreateAsMemoryBuffer.Name = "rbCreateAsMemoryBuffer";
            this.rbCreateAsMemoryBuffer.Size = new System.Drawing.Size(126, 17);
            this.rbCreateAsMemoryBuffer.TabIndex = 140;
            this.rbCreateAsMemoryBuffer.TabStop = true;
            this.rbCreateAsMemoryBuffer.Text = "File as Memory Buffer";
            this.rbCreateAsMemoryBuffer.UseVisualStyleBackColor = true;
            this.rbCreateAsMemoryBuffer.CheckedChanged += new System.EventHandler(this.rbCreateAsPixelBuffer_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(117, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 142;
            this.label18.Text = "Height:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 141;
            this.label10.Text = "Width:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(224, 25);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(67, 13);
            this.label25.TabIndex = 145;
            this.label25.Text = "Pixel Format:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 147;
            this.label1.Text = "Create From:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxWidth);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.ntxPixelFormat);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.ntxHeight);
            this.groupBox1.Location = new System.Drawing.Point(18, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 50);
            this.groupBox1.TabIndex = 148;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Data";
            // 
            // ntxWidth
            // 
            this.ntxWidth.Enabled = false;
            this.ntxWidth.Location = new System.Drawing.Point(57, 22);
            this.ntxWidth.Name = "ntxWidth";
            this.ntxWidth.Size = new System.Drawing.Size(44, 20);
            this.ntxWidth.TabIndex = 143;
            // 
            // ntxPixelFormat
            // 
            this.ntxPixelFormat.Enabled = false;
            this.ntxPixelFormat.Location = new System.Drawing.Point(297, 22);
            this.ntxPixelFormat.Name = "ntxPixelFormat";
            this.ntxPixelFormat.Size = new System.Drawing.Size(108, 20);
            this.ntxPixelFormat.TabIndex = 146;
            // 
            // ntxHeight
            // 
            this.ntxHeight.Enabled = false;
            this.ntxHeight.Location = new System.Drawing.Point(164, 22);
            this.ntxHeight.Name = "ntxHeight";
            this.ntxHeight.Size = new System.Drawing.Size(44, 20);
            this.ntxHeight.TabIndex = 144;
            // 
            // tcImages
            // 
            this.tcImages.Controls.Add(this.tabPage1);
            this.tcImages.Controls.Add(this.tabPage2);
            this.tcImages.Location = new System.Drawing.Point(3, 2);
            this.tcImages.Name = "tcImages";
            this.tcImages.SelectedIndex = 0;
            this.tcImages.Size = new System.Drawing.Size(451, 307);
            this.tcImages.TabIndex = 149;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrlStrFileName);
            this.tabPage1.Controls.Add(this.btnCreate);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label37);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(443, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Create New";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrlStrFileName
            // 
            this.ctrlStrFileName.AutoSize = true;
            this.ctrlStrFileName.FileName = "";
            this.ctrlStrFileName.Filter = "";
            this.ctrlStrFileName.IsFolderDialog = false;
            this.ctrlStrFileName.IsFullPath = true;
            this.ctrlStrFileName.IsSaveFile = false;
            this.ctrlStrFileName.LabelCaption = "                 ";
            this.ctrlStrFileName.Location = new System.Drawing.Point(79, 7);
            this.ctrlStrFileName.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlStrFileName.MinimumSize = new System.Drawing.Size(0, 24);
            this.ctrlStrFileName.MultiFilesSelect = false;
            this.ctrlStrFileName.Name = "ctrlStrFileName";
            this.ctrlStrFileName.Prefix = "";
            this.ctrlStrFileName.Size = new System.Drawing.Size(357, 24);
            this.ctrlStrFileName.TabIndex = 137;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tabControl1);
            this.tabPage2.Controls.Add(this.lstImages);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(443, 281);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Existing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(6, 159);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(431, 118);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSelectFromExisting);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(423, 92);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Select Existing";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSelectFromExisting
            // 
            this.btnSelectFromExisting.Location = new System.Drawing.Point(6, 6);
            this.btnSelectFromExisting.Name = "btnSelectFromExisting";
            this.btnSelectFromExisting.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFromExisting.TabIndex = 0;
            this.btnSelectFromExisting.Text = "Select";
            this.btnSelectFromExisting.UseVisualStyleBackColor = true;
            this.btnSelectFromExisting.Click += new System.EventHandler(this.btnSelectFromExisting_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnCreateFromExisting);
            this.tabPage4.Controls.Add(this.ntxExistsWidth);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.ntxExistsHeight);
            this.tabPage4.Controls.Add(this.cmbResizeFilter);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.chxFlipAroundY);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.chxFlipAroundX);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(423, 92);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Create New From Existing";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnCreateFromExisting
            // 
            this.btnCreateFromExisting.Location = new System.Drawing.Point(154, 65);
            this.btnCreateFromExisting.Name = "btnCreateFromExisting";
            this.btnCreateFromExisting.Size = new System.Drawing.Size(94, 23);
            this.btnCreateFromExisting.TabIndex = 152;
            this.btnCreateFromExisting.Text = "Create";
            this.btnCreateFromExisting.UseVisualStyleBackColor = true;
            this.btnCreateFromExisting.Click += new System.EventHandler(this.btnCreateFromExisting_Click);
            // 
            // ntxExistsWidth
            // 
            this.ntxExistsWidth.Location = new System.Drawing.Point(49, 6);
            this.ntxExistsWidth.Name = "ntxExistsWidth";
            this.ntxExistsWidth.Size = new System.Drawing.Size(44, 20);
            this.ntxExistsWidth.TabIndex = 147;
            this.ntxExistsWidth.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(222, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Resize Filter";
            // 
            // ntxExistsHeight
            // 
            this.ntxExistsHeight.Location = new System.Drawing.Point(156, 6);
            this.ntxExistsHeight.Name = "ntxExistsHeight";
            this.ntxExistsHeight.Size = new System.Drawing.Size(44, 20);
            this.ntxExistsHeight.TabIndex = 148;
            this.ntxExistsHeight.Text = "0";
            // 
            // cmbResizeFilter
            // 
            this.cmbResizeFilter.FormattingEnabled = true;
            this.cmbResizeFilter.Location = new System.Drawing.Point(296, 9);
            this.cmbResizeFilter.Name = "cmbResizeFilter";
            this.cmbResizeFilter.Size = new System.Drawing.Size(121, 21);
            this.cmbResizeFilter.TabIndex = 151;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 146;
            this.label3.Text = "Height:";
            // 
            // chxFlipAroundY
            // 
            this.chxFlipAroundY.AutoSize = true;
            this.chxFlipAroundY.Location = new System.Drawing.Point(99, 37);
            this.chxFlipAroundY.Name = "chxFlipAroundY";
            this.chxFlipAroundY.Size = new System.Drawing.Size(89, 17);
            this.chxFlipAroundY.TabIndex = 150;
            this.chxFlipAroundY.Text = "Flip Around Y";
            this.chxFlipAroundY.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 145;
            this.label2.Text = "Width:";
            // 
            // chxFlipAroundX
            // 
            this.chxFlipAroundX.AutoSize = true;
            this.chxFlipAroundX.Location = new System.Drawing.Point(4, 37);
            this.chxFlipAroundX.Name = "chxFlipAroundX";
            this.chxFlipAroundX.Size = new System.Drawing.Size(89, 17);
            this.chxFlipAroundX.TabIndex = 149;
            this.chxFlipAroundX.Text = "Flip Around X";
            this.chxFlipAroundX.UseVisualStyleBackColor = true;
            // 
            // lstImages
            // 
            this.lstImages.FormattingEnabled = true;
            this.lstImages.Location = new System.Drawing.Point(6, 6);
            this.lstImages.Name = "lstImages";
            this.lstImages.Size = new System.Drawing.Size(272, 147);
            this.lstImages.TabIndex = 0;
            this.lstImages.SelectedIndexChanged += new System.EventHandler(this.lstImages_SelectedIndexChanged);
            // 
            // frmImageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(458, 312);
            this.Controls.Add(this.tcImages);
            this.Name = "frmImageDialog";
            this.Text = "Image Dialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcImages.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label37;
        private Controls.CtrlBrowseControl ctrlStrFileName;
        private System.Windows.Forms.RadioButton rbCreateAsFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbCreateAsPixelBuffer;
        private System.Windows.Forms.RadioButton rbCreateAsMemoryBuffer;
        private Controls.NumericTextBox ntxWidth;
        private Controls.NumericTextBox ntxHeight;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label25;
        private Controls.NumericTextBox ntxPixelFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chxUseFileExtension;
        private System.Windows.Forms.TabControl tcImages;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lstImages;
        private Controls.NumericTextBox ntxExistsWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Controls.NumericTextBox ntxExistsHeight;
        private System.Windows.Forms.CheckBox chxFlipAroundY;
        private System.Windows.Forms.CheckBox chxFlipAroundX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbResizeFilter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnSelectFromExisting;
        private System.Windows.Forms.Button btnCreateFromExisting;
    }
}