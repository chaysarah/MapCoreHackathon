namespace MCTester.General_Forms
{
    partial class frmSaveSessionToFolderDef
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
            this.btnSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUserComment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.checkGroupBoxMapsBaseDirectory = new MCTester.Controls.CheckGroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWebMaps = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAndroidMaps = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlBrowseControlMapsBaseDir = new MCTester.Controls.CtrlBrowseControl();
            this.checkGroupBoxSaveToFile = new MCTester.Controls.CheckGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImgFileName = new System.Windows.Forms.TextBox();
            this.cmbImgFileExt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlBrowseControlDestFolder = new MCTester.Controls.CtrlBrowseControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnSettingSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ctrlBrowseControlDestFolderSettings = new MCTester.Controls.CtrlBrowseControl();
            this.checkGroupBoxMapsBaseDirectory.SuspendLayout();
            this.checkGroupBoxSaveToFile.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(535, 258);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Destination Folder:";
            // 
            // txtUserComment
            // 
            this.txtUserComment.Location = new System.Drawing.Point(99, 161);
            this.txtUserComment.Name = "txtUserComment";
            this.txtUserComment.Size = new System.Drawing.Size(511, 20);
            this.txtUserComment.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "User Comment: ";
            // 
            // checkGroupBoxMapsBaseDirectory
            // 
            this.checkGroupBoxMapsBaseDirectory.Checked = false;
            this.checkGroupBoxMapsBaseDirectory.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkGroupBoxMapsBaseDirectory.Controls.Add(this.label5);
            this.checkGroupBoxMapsBaseDirectory.Controls.Add(this.txtWebMaps);
            this.checkGroupBoxMapsBaseDirectory.Controls.Add(this.label4);
            this.checkGroupBoxMapsBaseDirectory.Controls.Add(this.txtAndroidMaps);
            this.checkGroupBoxMapsBaseDirectory.Controls.Add(this.label3);
            this.checkGroupBoxMapsBaseDirectory.Controls.Add(this.ctrlBrowseControlMapsBaseDir);
            this.checkGroupBoxMapsBaseDirectory.Location = new System.Drawing.Point(7, 40);
            this.checkGroupBoxMapsBaseDirectory.Name = "checkGroupBoxMapsBaseDirectory";
            this.checkGroupBoxMapsBaseDirectory.Size = new System.Drawing.Size(603, 115);
            this.checkGroupBoxMapsBaseDirectory.TabIndex = 8;
            this.checkGroupBoxMapsBaseDirectory.TabStop = false;
            this.checkGroupBoxMapsBaseDirectory.Text = "Maps Base Directory";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Windows:";
            // 
            // txtWebMaps
            // 
            this.txtWebMaps.Location = new System.Drawing.Point(63, 81);
            this.txtWebMaps.Name = "txtWebMaps";
            this.txtWebMaps.Size = new System.Drawing.Size(150, 20);
            this.txtWebMaps.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Web:";
            // 
            // txtAndroidMaps
            // 
            this.txtAndroidMaps.Location = new System.Drawing.Point(63, 53);
            this.txtAndroidMaps.Name = "txtAndroidMaps";
            this.txtAndroidMaps.Size = new System.Drawing.Size(150, 20);
            this.txtAndroidMaps.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Android:";
            // 
            // ctrlBrowseControlMapsBaseDir
            // 
            this.ctrlBrowseControlMapsBaseDir.AutoSize = true;
            this.ctrlBrowseControlMapsBaseDir.FileName = "";
            this.ctrlBrowseControlMapsBaseDir.Filter = "";
            this.ctrlBrowseControlMapsBaseDir.IsFolderDialog = true;
            this.ctrlBrowseControlMapsBaseDir.IsFullPath = true;
            this.ctrlBrowseControlMapsBaseDir.IsSaveFile = false;
            this.ctrlBrowseControlMapsBaseDir.LabelCaption = "Windows:";
            this.ctrlBrowseControlMapsBaseDir.Location = new System.Drawing.Point(59, 26);
            this.ctrlBrowseControlMapsBaseDir.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseControlMapsBaseDir.MultiFilesSelect = false;
            this.ctrlBrowseControlMapsBaseDir.Name = "ctrlBrowseControlMapsBaseDir";
            this.ctrlBrowseControlMapsBaseDir.Prefix = "";
            this.ctrlBrowseControlMapsBaseDir.Size = new System.Drawing.Size(534, 24);
            this.ctrlBrowseControlMapsBaseDir.TabIndex = 1;
            // 
            // checkGroupBoxSaveToFile
            // 
            this.checkGroupBoxSaveToFile.Controls.Add(this.label2);
            this.checkGroupBoxSaveToFile.Controls.Add(this.txtImgFileName);
            this.checkGroupBoxSaveToFile.Controls.Add(this.cmbImgFileExt);
            this.checkGroupBoxSaveToFile.Controls.Add(this.label1);
            this.checkGroupBoxSaveToFile.Location = new System.Drawing.Point(7, 194);
            this.checkGroupBoxSaveToFile.Name = "checkGroupBoxSaveToFile";
            this.checkGroupBoxSaveToFile.Size = new System.Drawing.Size(603, 49);
            this.checkGroupBoxSaveToFile.TabIndex = 7;
            this.checkGroupBoxSaveToFile.TabStop = false;
            this.checkGroupBoxSaveToFile.Text = "Save To File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Image File Extension:";
            // 
            // txtImgFileName
            // 
            this.txtImgFileName.Location = new System.Drawing.Point(118, 17);
            this.txtImgFileName.Name = "txtImgFileName";
            this.txtImgFileName.Size = new System.Drawing.Size(150, 20);
            this.txtImgFileName.TabIndex = 2;
            // 
            // cmbImgFileExt
            // 
            this.cmbImgFileExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImgFileExt.FormattingEnabled = true;
            this.cmbImgFileExt.Location = new System.Drawing.Point(472, 16);
            this.cmbImgFileExt.Name = "cmbImgFileExt";
            this.cmbImgFileExt.Size = new System.Drawing.Size(125, 21);
            this.cmbImgFileExt.TabIndex = 5;
            this.cmbImgFileExt.SelectedIndexChanged += new System.EventHandler(this.cmbImgFileExt_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Image File Name:";
            // 
            // ctrlBrowseControlDestFolder
            // 
            this.ctrlBrowseControlDestFolder.AutoSize = true;
            this.ctrlBrowseControlDestFolder.FileName = "";
            this.ctrlBrowseControlDestFolder.Filter = "";
            this.ctrlBrowseControlDestFolder.IsFolderDialog = true;
            this.ctrlBrowseControlDestFolder.IsFullPath = true;
            this.ctrlBrowseControlDestFolder.IsSaveFile = false;
            this.ctrlBrowseControlDestFolder.LabelCaption = "Destination Folder:            ";
            this.ctrlBrowseControlDestFolder.Location = new System.Drawing.Point(107, 10);
            this.ctrlBrowseControlDestFolder.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseControlDestFolder.MultiFilesSelect = false;
            this.ctrlBrowseControlDestFolder.Name = "ctrlBrowseControlDestFolder";
            this.ctrlBrowseControlDestFolder.Prefix = "";
            this.ctrlBrowseControlDestFolder.Size = new System.Drawing.Size(490, 24);
            this.ctrlBrowseControlDestFolder.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 318);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtUserComment);
            this.tabPage1.Controls.Add(this.ctrlBrowseControlDestFolder);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.checkGroupBoxSaveToFile);
            this.tabPage1.Controls.Add(this.checkGroupBoxMapsBaseDirectory);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(619, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnSettingSave);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.ctrlBrowseControlDestFolderSettings);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(619, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSettingSave
            // 
            this.btnSettingSave.Location = new System.Drawing.Point(580, 10);
            this.btnSettingSave.Name = "btnSettingSave";
            this.btnSettingSave.Size = new System.Drawing.Size(36, 23);
            this.btnSettingSave.TabIndex = 12;
            this.btnSettingSave.Text = "OK";
            this.btnSettingSave.UseVisualStyleBackColor = true;
            this.btnSettingSave.Click += new System.EventHandler(this.btnSettingSave_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Base Destination Folder:";
            // 
            // ctrlBrowseControlDestFolderSettings
            // 
            this.ctrlBrowseControlDestFolderSettings.AutoSize = true;
            this.ctrlBrowseControlDestFolderSettings.FileName = "";
            this.ctrlBrowseControlDestFolderSettings.Filter = "";
            this.ctrlBrowseControlDestFolderSettings.IsFolderDialog = true;
            this.ctrlBrowseControlDestFolderSettings.IsFullPath = true;
            this.ctrlBrowseControlDestFolderSettings.IsSaveFile = false;
            this.ctrlBrowseControlDestFolderSettings.LabelCaption = "Destination Folder:            ";
            this.ctrlBrowseControlDestFolderSettings.Location = new System.Drawing.Point(124, 10);
            this.ctrlBrowseControlDestFolderSettings.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseControlDestFolderSettings.MultiFilesSelect = false;
            this.ctrlBrowseControlDestFolderSettings.Name = "ctrlBrowseControlDestFolderSettings";
            this.ctrlBrowseControlDestFolderSettings.Prefix = "";
            this.ctrlBrowseControlDestFolderSettings.Size = new System.Drawing.Size(454, 24);
            this.ctrlBrowseControlDestFolderSettings.TabIndex = 10;
            // 
            // frmSaveSessionToFolderDef
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(635, 323);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSaveSessionToFolderDef";
            this.Text = "Save Session To Folder Definitions";
            this.checkGroupBoxMapsBaseDirectory.ResumeLayout(false);
            this.checkGroupBoxMapsBaseDirectory.PerformLayout();
            this.checkGroupBoxSaveToFile.ResumeLayout(false);
            this.checkGroupBoxSaveToFile.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtrlBrowseControl ctrlBrowseControlDestFolder;
        private Controls.CtrlBrowseControl ctrlBrowseControlMapsBaseDir;
        private System.Windows.Forms.TextBox txtImgFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbImgFileExt;
        private System.Windows.Forms.Label label2;
        private Controls.CheckGroupBox checkGroupBoxSaveToFile;
        private Controls.CheckGroupBox checkGroupBoxMapsBaseDirectory;
        private System.Windows.Forms.TextBox txtWebMaps;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAndroidMaps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUserComment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label8;
        private Controls.CtrlBrowseControl ctrlBrowseControlDestFolderSettings;
        private System.Windows.Forms.Button btnSettingSave;
    }
}