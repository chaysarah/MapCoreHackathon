namespace MCTester.General_Forms
{
    partial class frmImagesPARFiles
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
            this.chxRecrusive = new System.Windows.Forms.CheckBox();
            this.dgvSourceFiles = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaxY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGeneratePARFile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumberOfImageRasterFiles = new System.Windows.Forms.TextBox();
            this.clstFileExtensions = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlBrowseControlSourceDir = new MCTester.Controls.CtrlBrowseControl();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSourceFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // chxRecrusive
            // 
            this.chxRecrusive.AutoSize = true;
            this.chxRecrusive.Location = new System.Drawing.Point(12, 42);
            this.chxRecrusive.Name = "chxRecrusive";
            this.chxRecrusive.Size = new System.Drawing.Size(139, 17);
            this.chxRecrusive.TabIndex = 17;
            this.chxRecrusive.Text = "Search in subdirectories";
            this.chxRecrusive.UseVisualStyleBackColor = true;
            // 
            // dgvSourceFiles
            // 
            this.dgvSourceFiles.AllowUserToAddRows = false;
            this.dgvSourceFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSourceFiles.BackgroundColor = System.Drawing.Color.White;
            this.dgvSourceFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSourceFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colResX,
            this.colResY,
            this.colMinX,
            this.colMinY,
            this.colMaxX,
            this.colMaxY});
            this.dgvSourceFiles.Location = new System.Drawing.Point(12, 138);
            this.dgvSourceFiles.Name = "dgvSourceFiles";
            this.dgvSourceFiles.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSourceFiles.Size = new System.Drawing.Size(658, 150);
            this.dgvSourceFiles.TabIndex = 24;
            // 
            // colFileName
            // 
            this.colFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colFileName.HeaderText = "File Name";
            this.colFileName.Name = "colFileName";
            // 
            // colResX
            // 
            this.colResX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colResX.HeaderText = "Res X";
            this.colResX.Name = "colResX";
            this.colResX.Width = 61;
            // 
            // colResY
            // 
            this.colResY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colResY.HeaderText = "Res Y";
            this.colResY.Name = "colResY";
            this.colResY.Width = 51;
            // 
            // colMinX
            // 
            this.colMinX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colMinX.HeaderText = "MinX";
            this.colMinX.Name = "colMinX";
            this.colMinX.Width = 56;
            // 
            // colMinY
            // 
            this.colMinY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colMinY.HeaderText = "MinY";
            this.colMinY.Name = "colMinY";
            this.colMinY.Width = 56;
            // 
            // colMaxX
            // 
            this.colMaxX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colMaxX.HeaderText = "Max X";
            this.colMaxX.Name = "colMaxX";
            this.colMaxX.Width = 58;
            // 
            // colMaxY
            // 
            this.colMaxY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colMaxY.HeaderText = "Max Y";
            this.colMaxY.Name = "colMaxY";
            this.colMaxY.Width = 58;
            // 
            // btnGeneratePARFile
            // 
            this.btnGeneratePARFile.Location = new System.Drawing.Point(418, 303);
            this.btnGeneratePARFile.Name = "btnGeneratePARFile";
            this.btnGeneratePARFile.Size = new System.Drawing.Size(115, 23);
            this.btnGeneratePARFile.TabIndex = 25;
            this.btnGeneratePARFile.Text = "Generate PAR File";
            this.btnGeneratePARFile.UseVisualStyleBackColor = true;
            this.btnGeneratePARFile.Click += new System.EventHandler(this.btnGeneratePARFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(539, 303);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 23);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Number of image Raster files:";
            // 
            // txtNumberOfImageRasterFiles
            // 
            this.txtNumberOfImageRasterFiles.Enabled = false;
            this.txtNumberOfImageRasterFiles.Location = new System.Drawing.Point(160, 112);
            this.txtNumberOfImageRasterFiles.Name = "txtNumberOfImageRasterFiles";
            this.txtNumberOfImageRasterFiles.Size = new System.Drawing.Size(61, 20);
            this.txtNumberOfImageRasterFiles.TabIndex = 28;
            // 
            // clstFileExtensions
            // 
            this.clstFileExtensions.FormattingEnabled = true;
            this.clstFileExtensions.Location = new System.Drawing.Point(561, 62);
            this.clstFileExtensions.Name = "clstFileExtensions";
            this.clstFileExtensions.Size = new System.Drawing.Size(93, 64);
            this.clstFileExtensions.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(558, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "File Extensions:";
            // 
            // ctrlBrowseControlSourceDir
            // 
            this.ctrlBrowseControlSourceDir.AutoSize = true;
            this.ctrlBrowseControlSourceDir.FileName = "";
            this.ctrlBrowseControlSourceDir.Filter = "";
            this.ctrlBrowseControlSourceDir.IsFolderDialog = true;
            this.ctrlBrowseControlSourceDir.IsFullPath = true;
            this.ctrlBrowseControlSourceDir.IsSaveFile = false;
            this.ctrlBrowseControlSourceDir.LabelCaption = "Source Directory:";
            this.ctrlBrowseControlSourceDir.Location = new System.Drawing.Point(101, 12);
            this.ctrlBrowseControlSourceDir.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseControlSourceDir.MultiFilesSelect = false;
            this.ctrlBrowseControlSourceDir.Name = "ctrlBrowseControlSourceDir";
            this.ctrlBrowseControlSourceDir.Prefix = "";
            this.ctrlBrowseControlSourceDir.Size = new System.Drawing.Size(553, 24);
            this.ctrlBrowseControlSourceDir.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Source Directory:";
            // 
            // frmImagesPARFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(674, 335);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clstFileExtensions);
            this.Controls.Add(this.txtNumberOfImageRasterFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnGeneratePARFile);
            this.Controls.Add(this.dgvSourceFiles);
            this.Controls.Add(this.chxRecrusive);
            this.Controls.Add(this.ctrlBrowseControlSourceDir);
            this.Name = "frmImagesPARFiles";
            this.Text = "frmImagesPARFiles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImagesPARFiles_FormClosing);
            this.Load += new System.EventHandler(this.frmImagesPARFiles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSourceFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chxRecrusive;
        private System.Windows.Forms.DataGridView dgvSourceFiles;
        private System.Windows.Forms.Button btnGeneratePARFile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumberOfImageRasterFiles;
        private System.Windows.Forms.CheckedListBox clstFileExtensions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxY;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseControlSourceDir;
        private System.Windows.Forms.Label label3;
    }
}