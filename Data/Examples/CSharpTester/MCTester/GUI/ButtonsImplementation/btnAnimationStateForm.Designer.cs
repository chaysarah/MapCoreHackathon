namespace MCTester.ButtonsImplementation
{
    partial class btnAnimationStateForm
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
            this.btnCreateAnimationStateUsingMeshItem = new System.Windows.Forms.Button();
            this.cmbMeshItems = new System.Windows.Forms.ComboBox();
            this.cmbObjects = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateAnimationStateUsingMeshID = new System.Windows.Forms.Button();
            this.lstAnimationState = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chxEnabled = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chxHasEnded = new System.Windows.Forms.CheckBox();
            this.btnAttachToAnimationOK = new System.Windows.Forms.Button();
            this.btnEnabledOK = new System.Windows.Forms.Button();
            this.btnTimePointOK = new System.Windows.Forms.Button();
            this.btnWeightOK = new System.Windows.Forms.Button();
            this.btnLengthOK = new System.Windows.Forms.Button();
            this.btnLoopOK = new System.Windows.Forms.Button();
            this.chxLoop = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbCreateUsingMeshItemID = new System.Windows.Forms.RadioButton();
            this.rdbCreateUsingMeshItem = new System.Windows.Forms.RadioButton();
            this.ntxMeshItemID = new MCTester.Controls.NumericTextBox();
            this.btnRemoveAnimationState = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSpeedFactorOK = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbAttachedAnimationName = new System.Windows.Forms.ComboBox();
            this.dgvAttachPointsWeights = new System.Windows.Forms.DataGridView();
            this.colAttachPointsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAttachPointsWeights = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAttachPointsWeights = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxDuration = new MCTester.Controls.NumericTextBox();
            this.ntxTimeDelay = new MCTester.Controls.NumericTextBox();
            this.ntxAPDuration = new MCTester.Controls.NumericTextBox();
            this.ntxSpeedFactor = new MCTester.Controls.NumericTextBox();
            this.ntxLength = new MCTester.Controls.NumericTextBox();
            this.ntxWeight = new MCTester.Controls.NumericTextBox();
            this.ntxTimePoint = new MCTester.Controls.NumericTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachPointsWeights)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreateAnimationStateUsingMeshItem
            // 
            this.btnCreateAnimationStateUsingMeshItem.Location = new System.Drawing.Point(687, 90);
            this.btnCreateAnimationStateUsingMeshItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreateAnimationStateUsingMeshItem.Name = "btnCreateAnimationStateUsingMeshItem";
            this.btnCreateAnimationStateUsingMeshItem.Size = new System.Drawing.Size(100, 28);
            this.btnCreateAnimationStateUsingMeshItem.TabIndex = 0;
            this.btnCreateAnimationStateUsingMeshItem.Text = "Create";
            this.btnCreateAnimationStateUsingMeshItem.UseVisualStyleBackColor = true;
            this.btnCreateAnimationStateUsingMeshItem.Click += new System.EventHandler(this.btnCreateAnimationStateUsingMeshItem_Click);
            // 
            // cmbMeshItems
            // 
            this.cmbMeshItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMeshItems.FormattingEnabled = true;
            this.cmbMeshItems.Location = new System.Drawing.Point(128, 92);
            this.cmbMeshItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbMeshItems.Name = "cmbMeshItems";
            this.cmbMeshItems.Size = new System.Drawing.Size(205, 24);
            this.cmbMeshItems.TabIndex = 1;
            this.cmbMeshItems.SelectedIndexChanged += new System.EventHandler(this.cmbMeshItems_SelectedIndexChanged);
            // 
            // cmbObjects
            // 
            this.cmbObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjects.FormattingEnabled = true;
            this.cmbObjects.Location = new System.Drawing.Point(76, 30);
            this.cmbObjects.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbObjects.Name = "cmbObjects";
            this.cmbObjects.Size = new System.Drawing.Size(205, 24);
            this.cmbObjects.TabIndex = 2;
            this.cmbObjects.SelectedIndexChanged += new System.EventHandler(this.cmbObjects_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Object:";
            // 
            // btnCreateAnimationStateUsingMeshID
            // 
            this.btnCreateAnimationStateUsingMeshID.Enabled = false;
            this.btnCreateAnimationStateUsingMeshID.Location = new System.Drawing.Point(687, 123);
            this.btnCreateAnimationStateUsingMeshID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreateAnimationStateUsingMeshID.Name = "btnCreateAnimationStateUsingMeshID";
            this.btnCreateAnimationStateUsingMeshID.Size = new System.Drawing.Size(100, 28);
            this.btnCreateAnimationStateUsingMeshID.TabIndex = 7;
            this.btnCreateAnimationStateUsingMeshID.Text = "Create";
            this.btnCreateAnimationStateUsingMeshID.UseVisualStyleBackColor = true;
            this.btnCreateAnimationStateUsingMeshID.Click += new System.EventHandler(this.btnCreateAnimationStateUsingMeshID_Click);
            // 
            // lstAnimationState
            // 
            this.lstAnimationState.FormattingEnabled = true;
            this.lstAnimationState.ItemHeight = 16;
            this.lstAnimationState.Location = new System.Drawing.Point(20, 210);
            this.lstAnimationState.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstAnimationState.Name = "lstAnimationState";
            this.lstAnimationState.Size = new System.Drawing.Size(229, 308);
            this.lstAnimationState.TabIndex = 12;
            this.lstAnimationState.SelectedIndexChanged += new System.EventHandler(this.lstAnimationState_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(16, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Animation State List:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(259, 222);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Attached Animation:";
            // 
            // chxEnabled
            // 
            this.chxEnabled.AutoSize = true;
            this.chxEnabled.Location = new System.Drawing.Point(263, 258);
            this.chxEnabled.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chxEnabled.Name = "chxEnabled";
            this.chxEnabled.Size = new System.Drawing.Size(96, 21);
            this.chxEnabled.TabIndex = 16;
            this.chxEnabled.Text = "Is Enabled";
            this.chxEnabled.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(259, 295);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Time Point:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(259, 331);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Weight:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(259, 367);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 17);
            this.label10.TabIndex = 23;
            this.label10.Text = "Length:";
            // 
            // chxHasEnded
            // 
            this.chxHasEnded.AutoSize = true;
            this.chxHasEnded.Location = new System.Drawing.Point(20, 529);
            this.chxHasEnded.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chxHasEnded.Name = "chxHasEnded";
            this.chxHasEnded.Size = new System.Drawing.Size(100, 21);
            this.chxHasEnded.TabIndex = 27;
            this.chxHasEnded.Text = "Has Ended";
            this.chxHasEnded.UseVisualStyleBackColor = true;
            // 
            // btnAttachToAnimationOK
            // 
            this.btnAttachToAnimationOK.Location = new System.Drawing.Point(737, 218);
            this.btnAttachToAnimationOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAttachToAnimationOK.Name = "btnAttachToAnimationOK";
            this.btnAttachToAnimationOK.Size = new System.Drawing.Size(55, 28);
            this.btnAttachToAnimationOK.TabIndex = 28;
            this.btnAttachToAnimationOK.Text = "OK";
            this.btnAttachToAnimationOK.UseVisualStyleBackColor = true;
            this.btnAttachToAnimationOK.Click += new System.EventHandler(this.btnAttachToAnimationOK_Click);
            // 
            // btnEnabledOK
            // 
            this.btnEnabledOK.Location = new System.Drawing.Point(737, 254);
            this.btnEnabledOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnabledOK.Name = "btnEnabledOK";
            this.btnEnabledOK.Size = new System.Drawing.Size(55, 28);
            this.btnEnabledOK.TabIndex = 29;
            this.btnEnabledOK.Text = "OK";
            this.btnEnabledOK.UseVisualStyleBackColor = true;
            this.btnEnabledOK.Click += new System.EventHandler(this.btnEnabledOK_Click);
            // 
            // btnTimePointOK
            // 
            this.btnTimePointOK.Location = new System.Drawing.Point(737, 289);
            this.btnTimePointOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTimePointOK.Name = "btnTimePointOK";
            this.btnTimePointOK.Size = new System.Drawing.Size(55, 28);
            this.btnTimePointOK.TabIndex = 30;
            this.btnTimePointOK.Text = "OK";
            this.btnTimePointOK.UseVisualStyleBackColor = true;
            this.btnTimePointOK.Click += new System.EventHandler(this.btnTimePointOK_Click);
            // 
            // btnWeightOK
            // 
            this.btnWeightOK.Location = new System.Drawing.Point(737, 325);
            this.btnWeightOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnWeightOK.Name = "btnWeightOK";
            this.btnWeightOK.Size = new System.Drawing.Size(55, 28);
            this.btnWeightOK.TabIndex = 32;
            this.btnWeightOK.Text = "OK";
            this.btnWeightOK.UseVisualStyleBackColor = true;
            this.btnWeightOK.Click += new System.EventHandler(this.btnWeightOK_Click);
            // 
            // btnLengthOK
            // 
            this.btnLengthOK.Location = new System.Drawing.Point(737, 361);
            this.btnLengthOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLengthOK.Name = "btnLengthOK";
            this.btnLengthOK.Size = new System.Drawing.Size(55, 28);
            this.btnLengthOK.TabIndex = 33;
            this.btnLengthOK.Text = "OK";
            this.btnLengthOK.UseVisualStyleBackColor = true;
            this.btnLengthOK.Click += new System.EventHandler(this.btnLengthOK_Click);
            // 
            // btnLoopOK
            // 
            this.btnLoopOK.Location = new System.Drawing.Point(737, 432);
            this.btnLoopOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoopOK.Name = "btnLoopOK";
            this.btnLoopOK.Size = new System.Drawing.Size(55, 28);
            this.btnLoopOK.TabIndex = 34;
            this.btnLoopOK.Text = "OK";
            this.btnLoopOK.UseVisualStyleBackColor = true;
            this.btnLoopOK.Click += new System.EventHandler(this.btnLoopOK_Click);
            // 
            // chxLoop
            // 
            this.chxLoop.AutoSize = true;
            this.chxLoop.Checked = true;
            this.chxLoop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLoop.Location = new System.Drawing.Point(263, 437);
            this.chxLoop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chxLoop.Name = "chxLoop";
            this.chxLoop.Size = new System.Drawing.Size(76, 21);
            this.chxLoop.TabIndex = 35;
            this.chxLoop.Text = "Is Loop";
            this.chxLoop.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(551, 96);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 17);
            this.label11.TabIndex = 36;
            this.label11.Text = "(Using Mesh Item)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(551, 129);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 17);
            this.label12.TabIndex = 37;
            this.label12.Text = "(Using Mesh ID)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbCreateUsingMeshItemID);
            this.groupBox1.Controls.Add(this.rdbCreateUsingMeshItem);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btnCreateAnimationStateUsingMeshItem);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbMeshItems);
            this.groupBox1.Controls.Add(this.cmbObjects);
            this.groupBox1.Controls.Add(this.ntxMeshItemID);
            this.groupBox1.Controls.Add(this.btnCreateAnimationStateUsingMeshID);
            this.groupBox1.Location = new System.Drawing.Point(3, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(803, 171);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Animation State";
            // 
            // rdbCreateUsingMeshItemID
            // 
            this.rdbCreateUsingMeshItemID.AutoSize = true;
            this.rdbCreateUsingMeshItemID.Location = new System.Drawing.Point(17, 127);
            this.rdbCreateUsingMeshItemID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbCreateUsingMeshItemID.Name = "rdbCreateUsingMeshItemID";
            this.rdbCreateUsingMeshItemID.Size = new System.Drawing.Size(114, 21);
            this.rdbCreateUsingMeshItemID.TabIndex = 39;
            this.rdbCreateUsingMeshItemID.Text = "Mesh Item ID:";
            this.rdbCreateUsingMeshItemID.UseVisualStyleBackColor = true;
            // 
            // rdbCreateUsingMeshItem
            // 
            this.rdbCreateUsingMeshItem.AutoSize = true;
            this.rdbCreateUsingMeshItem.Checked = true;
            this.rdbCreateUsingMeshItem.Location = new System.Drawing.Point(17, 94);
            this.rdbCreateUsingMeshItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rdbCreateUsingMeshItem.Name = "rdbCreateUsingMeshItem";
            this.rdbCreateUsingMeshItem.Size = new System.Drawing.Size(97, 21);
            this.rdbCreateUsingMeshItem.TabIndex = 38;
            this.rdbCreateUsingMeshItem.TabStop = true;
            this.rdbCreateUsingMeshItem.Text = "Mesh Item:";
            this.rdbCreateUsingMeshItem.UseVisualStyleBackColor = true;
            this.rdbCreateUsingMeshItem.CheckedChanged += new System.EventHandler(this.rdbCreateUsingMeshItem_CheckedChanged);
            // 
            // ntxMeshItemID
            // 
            this.ntxMeshItemID.Location = new System.Drawing.Point(147, 126);
            this.ntxMeshItemID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxMeshItemID.Name = "ntxMeshItemID";
            this.ntxMeshItemID.Size = new System.Drawing.Size(81, 22);
            this.ntxMeshItemID.TabIndex = 6;
            this.ntxMeshItemID.Text = "0";
            this.ntxMeshItemID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ntxMeshItemID.TextChanged += new System.EventHandler(this.ntxMeshItemID_TextChanged);
            // 
            // btnRemoveAnimationState
            // 
            this.btnRemoveAnimationState.Location = new System.Drawing.Point(151, 524);
            this.btnRemoveAnimationState.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemoveAnimationState.Name = "btnRemoveAnimationState";
            this.btnRemoveAnimationState.Size = new System.Drawing.Size(100, 28);
            this.btnRemoveAnimationState.TabIndex = 39;
            this.btnRemoveAnimationState.Text = "Remove";
            this.btnRemoveAnimationState.UseVisualStyleBackColor = true;
            this.btnRemoveAnimationState.Click += new System.EventHandler(this.btnRemoveAnimationState_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 402);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 17);
            this.label5.TabIndex = 40;
            this.label5.Text = "Speed Factor:";
            // 
            // btnSpeedFactorOK
            // 
            this.btnSpeedFactorOK.Location = new System.Drawing.Point(737, 396);
            this.btnSpeedFactorOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSpeedFactorOK.Name = "btnSpeedFactorOK";
            this.btnSpeedFactorOK.Size = new System.Drawing.Size(55, 28);
            this.btnSpeedFactorOK.TabIndex = 42;
            this.btnSpeedFactorOK.Text = "OK";
            this.btnSpeedFactorOK.UseVisualStyleBackColor = true;
            this.btnSpeedFactorOK.Click += new System.EventHandler(this.btnSpeedFactorOK_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(259, 679);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(119, 17);
            this.label13.TabIndex = 47;
            this.label13.Text = "Change Duration:";
            // 
            // cmbAttachedAnimationName
            // 
            this.cmbAttachedAnimationName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttachedAnimationName.FormattingEnabled = true;
            this.cmbAttachedAnimationName.Location = new System.Drawing.Point(403, 218);
            this.cmbAttachedAnimationName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAttachedAnimationName.Name = "cmbAttachedAnimationName";
            this.cmbAttachedAnimationName.Size = new System.Drawing.Size(325, 24);
            this.cmbAttachedAnimationName.TabIndex = 48;
            // 
            // dgvAttachPointsWeights
            // 
            this.dgvAttachPointsWeights.AllowUserToAddRows = false;
            this.dgvAttachPointsWeights.AllowUserToDeleteRows = false;
            this.dgvAttachPointsWeights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttachPointsWeights.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAttachPointsName,
            this.colAttachPointsWeights});
            this.dgvAttachPointsWeights.Location = new System.Drawing.Point(263, 562);
            this.dgvAttachPointsWeights.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAttachPointsWeights.Name = "dgvAttachPointsWeights";
            this.dgvAttachPointsWeights.RowHeadersVisible = false;
            this.dgvAttachPointsWeights.Size = new System.Drawing.Size(527, 103);
            this.dgvAttachPointsWeights.TabIndex = 50;
            // 
            // colAttachPointsName
            // 
            this.colAttachPointsName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAttachPointsName.HeaderText = "Attach Points Name";
            this.colAttachPointsName.Name = "colAttachPointsName";
            // 
            // colAttachPointsWeights
            // 
            this.colAttachPointsWeights.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colAttachPointsWeights.HeaderText = "Weight";
            this.colAttachPointsWeights.Name = "colAttachPointsWeights";
            this.colAttachPointsWeights.Width = 81;
            // 
            // btnAttachPointsWeights
            // 
            this.btnAttachPointsWeights.Location = new System.Drawing.Point(737, 673);
            this.btnAttachPointsWeights.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAttachPointsWeights.Name = "btnAttachPointsWeights";
            this.btnAttachPointsWeights.Size = new System.Drawing.Size(52, 28);
            this.btnAttachPointsWeights.TabIndex = 51;
            this.btnAttachPointsWeights.Text = "Set";
            this.btnAttachPointsWeights.UseVisualStyleBackColor = true;
            this.btnAttachPointsWeights.Click += new System.EventHandler(this.btnAttachPointsWeights_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 480);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "Time Delay:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(473, 331);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 17);
            this.label3.TabIndex = 55;
            this.label3.Text = "Change Duration:";
            // 
            // ntxDuration
            // 
            this.ntxDuration.Location = new System.Drawing.Point(601, 327);
            this.ntxDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxDuration.Name = "ntxDuration";
            this.ntxDuration.Size = new System.Drawing.Size(81, 22);
            this.ntxDuration.TabIndex = 54;
            this.ntxDuration.Text = "0";
            this.ntxDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxTimeDelay
            // 
            this.ntxTimeDelay.Location = new System.Drawing.Point(367, 476);
            this.ntxTimeDelay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxTimeDelay.Name = "ntxTimeDelay";
            this.ntxTimeDelay.Size = new System.Drawing.Size(81, 22);
            this.ntxTimeDelay.TabIndex = 53;
            this.ntxTimeDelay.Text = "0";
            this.ntxTimeDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxAPDuration
            // 
            this.ntxAPDuration.Location = new System.Drawing.Point(387, 676);
            this.ntxAPDuration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxAPDuration.Name = "ntxAPDuration";
            this.ntxAPDuration.Size = new System.Drawing.Size(81, 22);
            this.ntxAPDuration.TabIndex = 46;
            this.ntxAPDuration.Text = "0";
            this.ntxAPDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxSpeedFactor
            // 
            this.ntxSpeedFactor.Location = new System.Drawing.Point(365, 399);
            this.ntxSpeedFactor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxSpeedFactor.Name = "ntxSpeedFactor";
            this.ntxSpeedFactor.Size = new System.Drawing.Size(81, 22);
            this.ntxSpeedFactor.TabIndex = 41;
            this.ntxSpeedFactor.Text = "1";
            this.ntxSpeedFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxLength
            // 
            this.ntxLength.Location = new System.Drawing.Point(365, 363);
            this.ntxLength.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxLength.Name = "ntxLength";
            this.ntxLength.Size = new System.Drawing.Size(81, 22);
            this.ntxLength.TabIndex = 24;
            this.ntxLength.Text = "0";
            this.ntxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxWeight
            // 
            this.ntxWeight.Location = new System.Drawing.Point(365, 327);
            this.ntxWeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxWeight.Name = "ntxWeight";
            this.ntxWeight.Size = new System.Drawing.Size(81, 22);
            this.ntxWeight.TabIndex = 22;
            this.ntxWeight.Text = "1";
            this.ntxWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ntxTimePoint
            // 
            this.ntxTimePoint.Location = new System.Drawing.Point(365, 292);
            this.ntxTimePoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxTimePoint.Name = "ntxTimePoint";
            this.ntxTimePoint.Size = new System.Drawing.Size(81, 22);
            this.ntxTimePoint.TabIndex = 18;
            this.ntxTimePoint.Text = "0";
            this.ntxTimePoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAnimationStateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(805, 705);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ntxDuration);
            this.Controls.Add(this.ntxTimeDelay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAttachPointsWeights);
            this.Controls.Add(this.dgvAttachPointsWeights);
            this.Controls.Add(this.cmbAttachedAnimationName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.ntxAPDuration);
            this.Controls.Add(this.btnSpeedFactorOK);
            this.Controls.Add(this.ntxSpeedFactor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnRemoveAnimationState);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chxLoop);
            this.Controls.Add(this.btnLoopOK);
            this.Controls.Add(this.btnLengthOK);
            this.Controls.Add(this.btnWeightOK);
            this.Controls.Add(this.btnTimePointOK);
            this.Controls.Add(this.btnEnabledOK);
            this.Controls.Add(this.btnAttachToAnimationOK);
            this.Controls.Add(this.chxHasEnded);
            this.Controls.Add(this.ntxLength);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ntxWeight);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ntxTimePoint);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chxEnabled);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstAnimationState);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "btnAnimationStateForm";
            this.Text = "btnAnimationStateForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.btnAnimationStateForm_FormClosing);
            this.Load += new System.EventHandler(this.btnAnimationStateForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachPointsWeights)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAnimationStateUsingMeshItem;
        private System.Windows.Forms.ComboBox cmbMeshItems;
        private System.Windows.Forms.ComboBox cmbObjects;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxMeshItemID;
        private System.Windows.Forms.Button btnCreateAnimationStateUsingMeshID;
        private System.Windows.Forms.ListBox lstAnimationState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chxEnabled;
        private MCTester.Controls.NumericTextBox ntxTimePoint;
        private System.Windows.Forms.Label label7;
        private MCTester.Controls.NumericTextBox ntxWeight;
        private System.Windows.Forms.Label label9;
        private MCTester.Controls.NumericTextBox ntxLength;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chxHasEnded;
        private System.Windows.Forms.Button btnAttachToAnimationOK;
        private System.Windows.Forms.Button btnEnabledOK;
        private System.Windows.Forms.Button btnTimePointOK;
        private System.Windows.Forms.Button btnWeightOK;
        private System.Windows.Forms.Button btnLengthOK;
        private System.Windows.Forms.Button btnLoopOK;
        private System.Windows.Forms.CheckBox chxLoop;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveAnimationState;
        private MCTester.Controls.NumericTextBox ntxSpeedFactor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSpeedFactorOK;
        private MCTester.Controls.NumericTextBox ntxAPDuration;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbAttachedAnimationName;
        private System.Windows.Forms.RadioButton rdbCreateUsingMeshItemID;
        private System.Windows.Forms.RadioButton rdbCreateUsingMeshItem;
        private System.Windows.Forms.DataGridView dgvAttachPointsWeights;
        private System.Windows.Forms.Button btnAttachPointsWeights;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttachPointsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttachPointsWeights;
        private MCTester.Controls.NumericTextBox ntxTimeDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox ntxDuration;
    }
}