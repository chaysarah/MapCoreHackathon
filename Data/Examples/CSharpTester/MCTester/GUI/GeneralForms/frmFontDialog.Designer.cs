namespace MCTester.General_Forms
{
    partial class frmFontDialog
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
            this.NewFontDialog = new System.Windows.Forms.FontDialog();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUseSelected = new System.Windows.Forms.Button();
            this.lstExistingFont = new System.Windows.Forms.ListBox();
            this.btnDeleteFont = new System.Windows.Forms.Button();
            this.btnNewFont = new System.Windows.Forms.Button();
            this.lblSelectNewFont = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chxStaticFont = new System.Windows.Forms.CheckBox();
            this.chxIsUnicode = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvLetterCharactersRanges = new System.Windows.Forms.DataGridView();
            this.colCharactersRangeFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCharactersRangeTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chxUseExisting = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvNumericCharactersRanges = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabFonts = new System.Windows.Forms.TabControl();
            this.tpLogFont = new System.Windows.Forms.TabPage();
            this.chxIsEmbedded = new System.Windows.Forms.CheckBox();
            this.lblStyle = new System.Windows.Forms.Label();
            this.lblSizeInPoints = new System.Windows.Forms.Label();
            this.lblLogName = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tpFileFont = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.cbIsMemoryBuffer = new System.Windows.Forms.CheckBox();
            this.ctrlBrowseFileFont = new MCTester.Controls.CtrlBrowseControl();
            this.ntbFontHeight = new MCTester.Controls.NumericTextBox();
            this.tpFromList = new System.Windows.Forms.TabPage();
            this.gbFontParams = new System.Windows.Forms.GroupBox();
            this.dgvSpecialChars = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ntxMaxNumCharsInDynamicAtlas = new MCTester.Controls.NumericTextBox();
            this.ntxOutlineWidth = new MCTester.Controls.NumericTextBox();
            this.ntxNumAntialiasingAlphaLevels = new MCTester.Controls.NumericTextBox();
            this.ntxCharacterSpacing = new MCTester.Controls.NumericTextBox();
            this.chxSetAsDefault = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chxUseSpecialCharsColors = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLetterCharactersRanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumericCharactersRanges)).BeginInit();
            this.tabFonts.SuspendLayout();
            this.tpLogFont.SuspendLayout();
            this.tpFileFont.SuspendLayout();
            this.tpFromList.SuspendLayout();
            this.gbFontParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecialChars)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(608, 533);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(94, 23);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnUseSelected
            // 
            this.btnUseSelected.Location = new System.Drawing.Point(508, 533);
            this.btnUseSelected.Name = "btnUseSelected";
            this.btnUseSelected.Size = new System.Drawing.Size(94, 23);
            this.btnUseSelected.TabIndex = 37;
            this.btnUseSelected.Text = "Use Selected";
            this.btnUseSelected.UseVisualStyleBackColor = true;
            this.btnUseSelected.Click += new System.EventHandler(this.btnUseSelected_Click);
            // 
            // lstExistingFont
            // 
            this.lstExistingFont.FormattingEnabled = true;
            this.lstExistingFont.Location = new System.Drawing.Point(5, 13);
            this.lstExistingFont.Name = "lstExistingFont";
            this.lstExistingFont.Size = new System.Drawing.Size(310, 82);
            this.lstExistingFont.TabIndex = 3;
            this.lstExistingFont.SelectedIndexChanged += new System.EventHandler(this.lstExistingFont_SelectedIndexChanged);
            this.lstExistingFont.DoubleClick += new System.EventHandler(this.lstExistingFont_DoubleClick);
            this.lstExistingFont.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstExistingFont_MouseMove);
            // 
            // btnDeleteFont
            // 
            this.btnDeleteFont.Location = new System.Drawing.Point(320, 72);
            this.btnDeleteFont.Name = "btnDeleteFont";
            this.btnDeleteFont.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteFont.TabIndex = 9;
            this.btnDeleteFont.Text = "Delete";
            this.btnDeleteFont.UseVisualStyleBackColor = true;
            this.btnDeleteFont.Click += new System.EventHandler(this.btnDeleteFont_Click);
            // 
            // btnNewFont
            // 
            this.btnNewFont.Location = new System.Drawing.Point(126, 10);
            this.btnNewFont.Name = "btnNewFont";
            this.btnNewFont.Size = new System.Drawing.Size(29, 23);
            this.btnNewFont.TabIndex = 30;
            this.btnNewFont.Text = "...";
            this.btnNewFont.UseVisualStyleBackColor = true;
            this.btnNewFont.Click += new System.EventHandler(this.btnNewFont_Click);
            // 
            // lblSelectNewFont
            // 
            this.lblSelectNewFont.AutoSize = true;
            this.lblSelectNewFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblSelectNewFont.Location = new System.Drawing.Point(161, 14);
            this.lblSelectNewFont.Name = "lblSelectNewFont";
            this.lblSelectNewFont.Size = new System.Drawing.Size(75, 13);
            this.lblSelectNewFont.TabIndex = 29;
            this.lblSelectNewFont.Text = "(Not Selected)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Font Height:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(108, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Num Antialiasing Alpha Levels (2-256):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(108, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 58;
            this.label8.Text = "Outline Width:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(108, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 54;
            this.label7.Text = "Character Spacing:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Max Num Chars In Dynamic Atlas:";
            // 
            // chxStaticFont
            // 
            this.chxStaticFont.AutoSize = true;
            this.chxStaticFont.Checked = true;
            this.chxStaticFont.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxStaticFont.Location = new System.Drawing.Point(9, 54);
            this.chxStaticFont.Name = "chxStaticFont";
            this.chxStaticFont.Size = new System.Drawing.Size(88, 17);
            this.chxStaticFont.TabIndex = 34;
            this.chxStaticFont.Text = "Is Static Font";
            this.chxStaticFont.UseVisualStyleBackColor = true;
            // 
            // chxIsUnicode
            // 
            this.chxIsUnicode.AutoSize = true;
            this.chxIsUnicode.Location = new System.Drawing.Point(257, 13);
            this.chxIsUnicode.Name = "chxIsUnicode";
            this.chxIsUnicode.Size = new System.Drawing.Size(132, 17);
            this.chxIsUnicode.TabIndex = 3;
            this.chxIsUnicode.Text = "Font Name Is Unicode";
            this.chxIsUnicode.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(572, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Ranges using decimal value:";
            // 
            // dgvLetterCharactersRanges
            // 
            this.dgvLetterCharactersRanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLetterCharactersRanges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCharactersRangeFrom,
            this.colCharactersRangeTo});
            this.dgvLetterCharactersRanges.Location = new System.Drawing.Point(393, 54);
            this.dgvLetterCharactersRanges.Name = "dgvLetterCharactersRanges";
            this.dgvLetterCharactersRanges.Size = new System.Drawing.Size(176, 103);
            this.dgvLetterCharactersRanges.TabIndex = 4;
            this.dgvLetterCharactersRanges.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLetterCharactersRanges_CellEndEdit);
            this.dgvLetterCharactersRanges.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvLetterCharactersRanges_UserAddedRow);
            this.dgvLetterCharactersRanges.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvLetterCharactersRanges_UserDeletedRow);
            // 
            // colCharactersRangeFrom
            // 
            this.colCharactersRangeFrom.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCharactersRangeFrom.HeaderText = "From";
            this.colCharactersRangeFrom.Name = "colCharactersRangeFrom";
            // 
            // colCharactersRangeTo
            // 
            this.colCharactersRangeTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCharactersRangeTo.HeaderText = "To";
            this.colCharactersRangeTo.Name = "colCharactersRangeTo";
            // 
            // chxUseExisting
            // 
            this.chxUseExisting.AutoSize = true;
            this.chxUseExisting.Checked = true;
            this.chxUseExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxUseExisting.Location = new System.Drawing.Point(9, 24);
            this.chxUseExisting.Name = "chxUseExisting";
            this.chxUseExisting.Size = new System.Drawing.Size(84, 17);
            this.chxUseExisting.TabIndex = 1;
            this.chxUseExisting.Text = "Use Existing";
            this.chxUseExisting.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(393, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Ranges using letters:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(393, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Characters Ranges (Insert range accordingly to the table type):";
            // 
            // dgvNumericCharactersRanges
            // 
            this.dgvNumericCharactersRanges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNumericCharactersRanges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.dgvNumericCharactersRanges.Location = new System.Drawing.Point(575, 54);
            this.dgvNumericCharactersRanges.Name = "dgvNumericCharactersRanges";
            this.dgvNumericCharactersRanges.Size = new System.Drawing.Size(176, 103);
            this.dgvNumericCharactersRanges.TabIndex = 31;
            this.dgvNumericCharactersRanges.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNumericCharactersRanges_CellEndEdit);
            this.dgvNumericCharactersRanges.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvNumericCharactersRanges_UserAddedRow);
            this.dgvNumericCharactersRanges.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvNumericCharactersRanges_UserDeletedRow);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "From";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "To";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // tabFonts
            // 
            this.tabFonts.Controls.Add(this.tpLogFont);
            this.tabFonts.Controls.Add(this.tpFileFont);
            this.tabFonts.Controls.Add(this.tpFromList);
            this.tabFonts.Location = new System.Drawing.Point(9, 10);
            this.tabFonts.Name = "tabFonts";
            this.tabFonts.SelectedIndex = 0;
            this.tabFonts.Size = new System.Drawing.Size(503, 137);
            this.tabFonts.TabIndex = 40;
            this.tabFonts.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabFonts_Selecting);
            this.tabFonts.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabFonts_Selected);
            // 
            // tpLogFont
            // 
            this.tpLogFont.Controls.Add(this.chxIsEmbedded);
            this.tpLogFont.Controls.Add(this.lblStyle);
            this.tpLogFont.Controls.Add(this.chxIsUnicode);
            this.tpLogFont.Controls.Add(this.lblSizeInPoints);
            this.tpLogFont.Controls.Add(this.lblLogName);
            this.tpLogFont.Controls.Add(this.label13);
            this.tpLogFont.Controls.Add(this.label12);
            this.tpLogFont.Controls.Add(this.label11);
            this.tpLogFont.Controls.Add(this.label10);
            this.tpLogFont.Controls.Add(this.label5);
            this.tpLogFont.Controls.Add(this.btnNewFont);
            this.tpLogFont.Controls.Add(this.lblSelectNewFont);
            this.tpLogFont.Location = new System.Drawing.Point(4, 22);
            this.tpLogFont.Name = "tpLogFont";
            this.tpLogFont.Padding = new System.Windows.Forms.Padding(3);
            this.tpLogFont.Size = new System.Drawing.Size(495, 111);
            this.tpLogFont.TabIndex = 0;
            this.tpLogFont.Text = "Log Font";
            this.tpLogFont.UseVisualStyleBackColor = true;
            // 
            // chxIsEmbedded
            // 
            this.chxIsEmbedded.AutoSize = true;
            this.chxIsEmbedded.Location = new System.Drawing.Point(401, 13);
            this.chxIsEmbedded.Name = "chxIsEmbedded";
            this.chxIsEmbedded.Size = new System.Drawing.Size(88, 17);
            this.chxIsEmbedded.TabIndex = 39;
            this.chxIsEmbedded.Text = "Is Embedded";
            this.chxIsEmbedded.UseVisualStyleBackColor = true;
            // 
            // lblStyle
            // 
            this.lblStyle.AutoSize = true;
            this.lblStyle.Enabled = false;
            this.lblStyle.Location = new System.Drawing.Point(206, 85);
            this.lblStyle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStyle.Name = "lblStyle";
            this.lblStyle.Size = new System.Drawing.Size(0, 13);
            this.lblStyle.TabIndex = 38;
            // 
            // lblSizeInPoints
            // 
            this.lblSizeInPoints.AutoSize = true;
            this.lblSizeInPoints.Enabled = false;
            this.lblSizeInPoints.Location = new System.Drawing.Point(206, 62);
            this.lblSizeInPoints.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSizeInPoints.Name = "lblSizeInPoints";
            this.lblSizeInPoints.Size = new System.Drawing.Size(0, 13);
            this.lblSizeInPoints.TabIndex = 37;
            // 
            // lblLogName
            // 
            this.lblLogName.AutoSize = true;
            this.lblLogName.Enabled = false;
            this.lblLogName.Location = new System.Drawing.Point(206, 40);
            this.lblLogName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogName.Name = "lblLogName";
            this.lblLogName.Size = new System.Drawing.Size(0, 13);
            this.lblLogName.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(128, 85);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "Style:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(128, 62);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Size In Points:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(128, 40);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(6, 40);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Details of selected font:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(6, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Click To Select Font";
            // 
            // tpFileFont
            // 
            this.tpFileFont.Controls.Add(this.label14);
            this.tpFileFont.Controls.Add(this.cbIsMemoryBuffer);
            this.tpFileFont.Controls.Add(this.label9);
            this.tpFileFont.Controls.Add(this.ctrlBrowseFileFont);
            this.tpFileFont.Controls.Add(this.ntbFontHeight);
            this.tpFileFont.Location = new System.Drawing.Point(4, 22);
            this.tpFileFont.Name = "tpFileFont";
            this.tpFileFont.Padding = new System.Windows.Forms.Padding(3);
            this.tpFileFont.Size = new System.Drawing.Size(495, 111);
            this.tpFileFont.TabIndex = 1;
            this.tpFileFont.Text = "File Font";
            this.tpFileFont.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(5, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 63;
            this.label14.Text = "File Name:";
            // 
            // cbIsMemoryBuffer
            // 
            this.cbIsMemoryBuffer.AutoSize = true;
            this.cbIsMemoryBuffer.Checked = true;
            this.cbIsMemoryBuffer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIsMemoryBuffer.Location = new System.Drawing.Point(10, 74);
            this.cbIsMemoryBuffer.Name = "cbIsMemoryBuffer";
            this.cbIsMemoryBuffer.Size = new System.Drawing.Size(105, 17);
            this.cbIsMemoryBuffer.TabIndex = 62;
            this.cbIsMemoryBuffer.Text = "Is Memory Buffer";
            this.cbIsMemoryBuffer.UseVisualStyleBackColor = true;
            // 
            // ctrlBrowseFileFont
            // 
            this.ctrlBrowseFileFont.AutoSize = true;
            this.ctrlBrowseFileFont.FileName = "";
            this.ctrlBrowseFileFont.Filter = "";
            this.ctrlBrowseFileFont.IsFolderDialog = false;
            this.ctrlBrowseFileFont.IsFullPath = true;
            this.ctrlBrowseFileFont.IsSaveFile = false;
            this.ctrlBrowseFileFont.LabelCaption = "File Name:      ";
            this.ctrlBrowseFileFont.Location = new System.Drawing.Point(68, 14);
            this.ctrlBrowseFileFont.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseFileFont.MultiFilesSelect = false;
            this.ctrlBrowseFileFont.Name = "ctrlBrowseFileFont";
            this.ctrlBrowseFileFont.Prefix = "";
            this.ctrlBrowseFileFont.Size = new System.Drawing.Size(323, 24);
            this.ctrlBrowseFileFont.TabIndex = 10;
            // 
            // ntbFontHeight
            // 
            this.ntbFontHeight.Location = new System.Drawing.Point(72, 44);
            this.ntbFontHeight.Name = "ntbFontHeight";
            this.ntbFontHeight.Size = new System.Drawing.Size(74, 20);
            this.ntbFontHeight.TabIndex = 61;
            this.ntbFontHeight.Text = "10";
            // 
            // tpFromList
            // 
            this.tpFromList.Controls.Add(this.lstExistingFont);
            this.tpFromList.Controls.Add(this.btnDeleteFont);
            this.tpFromList.Location = new System.Drawing.Point(4, 22);
            this.tpFromList.Name = "tpFromList";
            this.tpFromList.Padding = new System.Windows.Forms.Padding(3);
            this.tpFromList.Size = new System.Drawing.Size(495, 111);
            this.tpFromList.TabIndex = 2;
            this.tpFromList.Text = "From List";
            this.tpFromList.UseVisualStyleBackColor = true;
            // 
            // gbFontParams
            // 
            this.gbFontParams.Controls.Add(this.groupBox1);
            this.gbFontParams.Controls.Add(this.label7);
            this.gbFontParams.Controls.Add(this.label3);
            this.gbFontParams.Controls.Add(this.dgvLetterCharactersRanges);
            this.gbFontParams.Controls.Add(this.label6);
            this.gbFontParams.Controls.Add(this.chxStaticFont);
            this.gbFontParams.Controls.Add(this.label8);
            this.gbFontParams.Controls.Add(this.chxUseExisting);
            this.gbFontParams.Controls.Add(this.ntxMaxNumCharsInDynamicAtlas);
            this.gbFontParams.Controls.Add(this.ntxOutlineWidth);
            this.gbFontParams.Controls.Add(this.label2);
            this.gbFontParams.Controls.Add(this.label4);
            this.gbFontParams.Controls.Add(this.label1);
            this.gbFontParams.Controls.Add(this.ntxNumAntialiasingAlphaLevels);
            this.gbFontParams.Controls.Add(this.ntxCharacterSpacing);
            this.gbFontParams.Controls.Add(this.dgvNumericCharactersRanges);
            this.gbFontParams.Location = new System.Drawing.Point(9, 152);
            this.gbFontParams.Name = "gbFontParams";
            this.gbFontParams.Size = new System.Drawing.Size(1131, 375);
            this.gbFontParams.TabIndex = 60;
            this.gbFontParams.TabStop = false;
            this.gbFontParams.Text = "Font Params";
            // 
            // dgvSpecialChars
            // 
            this.dgvSpecialChars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSpecialChars.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7,
            this.Column1,
            this.Column2,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn3,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvSpecialChars.Location = new System.Drawing.Point(6, 42);
            this.dgvSpecialChars.Name = "dgvSpecialChars";
            this.dgvSpecialChars.Size = new System.Drawing.Size(1106, 155);
            this.dgvSpecialChars.TabIndex = 61;
            this.dgvSpecialChars.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecialChars_CellContentClick);
            this.dgvSpecialChars.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecialChars_CellEndEdit);
            this.dgvSpecialChars.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSpecialChars_DataError);
            this.dgvSpecialChars.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvSpecialChars_RowPostPaint);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Char Code (letter)";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Char Code (decimal value)";
            this.Column7.Name = "Column7";
            this.Column7.Width = 140;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Image";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Size Param Meaning";
            this.Column2.Name = "Column2";
            this.Column2.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Char Width Params";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Char Height Params";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Vertical Offset";
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Left Spacing";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Right Spacing";
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // ntxMaxNumCharsInDynamicAtlas
            // 
            this.ntxMaxNumCharsInDynamicAtlas.Location = new System.Drawing.Point(300, 79);
            this.ntxMaxNumCharsInDynamicAtlas.Name = "ntxMaxNumCharsInDynamicAtlas";
            this.ntxMaxNumCharsInDynamicAtlas.Size = new System.Drawing.Size(57, 20);
            this.ntxMaxNumCharsInDynamicAtlas.TabIndex = 35;
            this.ntxMaxNumCharsInDynamicAtlas.Text = "0";
            // 
            // ntxOutlineWidth
            // 
            this.ntxOutlineWidth.Location = new System.Drawing.Point(300, 107);
            this.ntxOutlineWidth.Name = "ntxOutlineWidth";
            this.ntxOutlineWidth.Size = new System.Drawing.Size(57, 20);
            this.ntxOutlineWidth.TabIndex = 59;
            // 
            // ntxNumAntialiasingAlphaLevels
            // 
            this.ntxNumAntialiasingAlphaLevels.Enabled = false;
            this.ntxNumAntialiasingAlphaLevels.Location = new System.Drawing.Point(300, 51);
            this.ntxNumAntialiasingAlphaLevels.Margin = new System.Windows.Forms.Padding(4);
            this.ntxNumAntialiasingAlphaLevels.Name = "ntxNumAntialiasingAlphaLevels";
            this.ntxNumAntialiasingAlphaLevels.Size = new System.Drawing.Size(57, 20);
            this.ntxNumAntialiasingAlphaLevels.TabIndex = 56;
            this.ntxNumAntialiasingAlphaLevels.Text = "2";
            // 
            // ntxCharacterSpacing
            // 
            this.ntxCharacterSpacing.Enabled = false;
            this.ntxCharacterSpacing.Location = new System.Drawing.Point(300, 22);
            this.ntxCharacterSpacing.Margin = new System.Windows.Forms.Padding(4);
            this.ntxCharacterSpacing.Name = "ntxCharacterSpacing";
            this.ntxCharacterSpacing.Size = new System.Drawing.Size(57, 20);
            this.ntxCharacterSpacing.TabIndex = 55;
            // 
            // chxSetAsDefault
            // 
            this.chxSetAsDefault.AutoSize = true;
            this.chxSetAsDefault.Location = new System.Drawing.Point(715, 537);
            this.chxSetAsDefault.Name = "chxSetAsDefault";
            this.chxSetAsDefault.Size = new System.Drawing.Size(94, 17);
            this.chxSetAsDefault.TabIndex = 61;
            this.chxSetAsDefault.Text = "Set As Default";
            this.chxSetAsDefault.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chxUseSpecialCharsColors);
            this.groupBox1.Controls.Add(this.dgvSpecialChars);
            this.groupBox1.Location = new System.Drawing.Point(6, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1118, 207);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Special Characters";
            // 
            // chxUseSpecialCharsColors
            // 
            this.chxUseSpecialCharsColors.AutoSize = true;
            this.chxUseSpecialCharsColors.Location = new System.Drawing.Point(9, 19);
            this.chxUseSpecialCharsColors.Name = "chxUseSpecialCharsColors";
            this.chxUseSpecialCharsColors.Size = new System.Drawing.Size(145, 17);
            this.chxUseSpecialCharsColors.TabIndex = 35;
            this.chxUseSpecialCharsColors.Text = "Use Special Chars Colors";
            this.chxUseSpecialCharsColors.UseVisualStyleBackColor = true;
            // 
            // frmFontDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1143, 564);
            this.Controls.Add(this.chxSetAsDefault);
            this.Controls.Add(this.gbFontParams);
            this.Controls.Add(this.tabFonts);
            this.Controls.Add(this.btnUseSelected);
            this.Controls.Add(this.btnCreate);
            this.Name = "frmFontDialog";
            this.Text = "Font Dialog";
            this.Load += new System.EventHandler(this.frmFontDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLetterCharactersRanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumericCharactersRanges)).EndInit();
            this.tabFonts.ResumeLayout(false);
            this.tpLogFont.ResumeLayout(false);
            this.tpLogFont.PerformLayout();
            this.tpFileFont.ResumeLayout(false);
            this.tpFileFont.PerformLayout();
            this.tpFromList.ResumeLayout(false);
            this.gbFontParams.ResumeLayout(false);
            this.gbFontParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecialChars)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FontDialog NewFontDialog;
        private System.Windows.Forms.CheckBox chxUseExisting;
        private System.Windows.Forms.ListBox lstExistingFont;
        private System.Windows.Forms.CheckBox chxIsUnicode;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnDeleteFont;
        private System.Windows.Forms.Button btnNewFont;
        private System.Windows.Forms.Label lblSelectNewFont;
        private System.Windows.Forms.DataGridView dgvLetterCharactersRanges;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCharactersRangeFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCharactersRangeTo;
        private System.Windows.Forms.DataGridView dgvNumericCharactersRanges;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chxStaticFont;
        private MCTester.Controls.NumericTextBox ntxMaxNumCharsInDynamicAtlas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUseSelected;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Controls.NumericTextBox ntxNumAntialiasingAlphaLevels;
        private Controls.NumericTextBox ntxCharacterSpacing;
        private System.Windows.Forms.Label label8;
        private Controls.NumericTextBox ntxOutlineWidth;
        private Controls.CtrlBrowseControl ctrlBrowseFileFont;
        private System.Windows.Forms.Label label9;
        private Controls.NumericTextBox ntbFontHeight;
        private System.Windows.Forms.TabControl tabFonts;
        private System.Windows.Forms.TabPage tpLogFont;
        private System.Windows.Forms.TabPage tpFileFont;
        private System.Windows.Forms.TabPage tpFromList;
        private System.Windows.Forms.GroupBox gbFontParams;
        private System.Windows.Forms.CheckBox cbIsMemoryBuffer;
        private System.Windows.Forms.Label lblStyle;
        private System.Windows.Forms.Label lblSizeInPoints;
        private System.Windows.Forms.Label lblLogName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
		private System.Windows.Forms.CheckBox chxSetAsDefault;
        private System.Windows.Forms.CheckBox chxIsEmbedded;
        private System.Windows.Forms.DataGridView dgvSpecialChars;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chxUseSpecialCharsColors;
    }
}