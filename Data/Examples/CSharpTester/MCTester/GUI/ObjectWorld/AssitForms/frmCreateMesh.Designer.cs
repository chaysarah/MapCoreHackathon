namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmCreateMesh
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMeshType = new System.Windows.Forms.TextBox();
            this.gbCreateUpdateMesh = new System.Windows.Forms.GroupBox();
            this.btnMeshList = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.rdbUpdateMesh = new System.Windows.Forms.RadioButton();
            this.rdbCreateMesh = new System.Windows.Forms.RadioButton();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.chxExistingUsed = new System.Windows.Forms.CheckBox();
            this.txtMeshPath = new System.Windows.Forms.TextBox();
            this.chxUseExisting = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDesirableMeshType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbAttachPointIDs = new System.Windows.Forms.GroupBox();
            this.btnGetNumAttachPints = new System.Windows.Forms.Button();
            this.ntxNumAttachPoints = new MCTester.Controls.NumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lstAttachPointChildren = new System.Windows.Forms.ListBox();
            this.btnGetAttachPointChildren = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.ntxParentIndex = new MCTester.Controls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ntxAttachPointIndex = new MCTester.Controls.NumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbMappedName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbMappedNameType = new System.Windows.Forms.ComboBox();
            this.btnRemoveID = new System.Windows.Forms.Button();
            this.dgvMappedIDs = new System.Windows.Forms.DataGridView();
            this.MappedID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MappedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ntxMappedID = new MCTester.Controls.NumericTextBox();
            this.btnSetMappedID = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCloseForm = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTransparentColor = new System.Windows.Forms.Button();
            this.gbTransparentColor = new System.Windows.Forms.GroupBox();
            this.chxTransparentColorEnabled = new System.Windows.Forms.CheckBox();
            this.rdbRecreateMesh = new System.Windows.Forms.RadioButton();
            this.gbCreateUpdateMesh.SuspendLayout();
            this.gbAttachPointIDs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappedIDs)).BeginInit();
            this.gbTransparentColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mesh Type:";
            // 
            // txtMeshType
            // 
            this.txtMeshType.Enabled = false;
            this.txtMeshType.Location = new System.Drawing.Point(75, 12);
            this.txtMeshType.Name = "txtMeshType";
            this.txtMeshType.Size = new System.Drawing.Size(272, 20);
            this.txtMeshType.TabIndex = 1;
            // 
            // gbCreateUpdateMesh
            // 
            this.gbCreateUpdateMesh.Controls.Add(this.rdbRecreateMesh);
            this.gbCreateUpdateMesh.Controls.Add(this.btnMeshList);
            this.gbCreateUpdateMesh.Controls.Add(this.btnApply);
            this.gbCreateUpdateMesh.Controls.Add(this.rdbUpdateMesh);
            this.gbCreateUpdateMesh.Controls.Add(this.rdbCreateMesh);
            this.gbCreateUpdateMesh.Controls.Add(this.btnFilePath);
            this.gbCreateUpdateMesh.Controls.Add(this.chxExistingUsed);
            this.gbCreateUpdateMesh.Controls.Add(this.txtMeshPath);
            this.gbCreateUpdateMesh.Controls.Add(this.chxUseExisting);
            this.gbCreateUpdateMesh.Controls.Add(this.label3);
            this.gbCreateUpdateMesh.Controls.Add(this.cmbDesirableMeshType);
            this.gbCreateUpdateMesh.Controls.Add(this.label2);
            this.gbCreateUpdateMesh.Location = new System.Drawing.Point(0, 38);
            this.gbCreateUpdateMesh.Name = "gbCreateUpdateMesh";
            this.gbCreateUpdateMesh.Size = new System.Drawing.Size(389, 126);
            this.gbCreateUpdateMesh.TabIndex = 3;
            this.gbCreateUpdateMesh.TabStop = false;
            this.gbCreateUpdateMesh.Text = "Mesh File";
            // 
            // btnMeshList
            // 
            this.btnMeshList.Location = new System.Drawing.Point(345, 48);
            this.btnMeshList.Name = "btnMeshList";
            this.btnMeshList.Size = new System.Drawing.Size(31, 20);
            this.btnMeshList.TabIndex = 53;
            this.btnMeshList.Tag = "X";
            this.btnMeshList.Text = "List";
            this.btnMeshList.UseVisualStyleBackColor = true;
            this.btnMeshList.Click += new System.EventHandler(this.btnMeshList_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(301, 97);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 52;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // rdbUpdateMesh
            // 
            this.rdbUpdateMesh.AutoSize = true;
            this.rdbUpdateMesh.Location = new System.Drawing.Point(162, 19);
            this.rdbUpdateMesh.Name = "rdbUpdateMesh";
            this.rdbUpdateMesh.Size = new System.Drawing.Size(60, 17);
            this.rdbUpdateMesh.TabIndex = 51;
            this.rdbUpdateMesh.TabStop = true;
            this.rdbUpdateMesh.Text = "Update";
            this.rdbUpdateMesh.UseVisualStyleBackColor = true;
            this.rdbUpdateMesh.Visible = false;
            // 
            // rdbCreateMesh
            // 
            this.rdbCreateMesh.AutoSize = true;
            this.rdbCreateMesh.Location = new System.Drawing.Point(9, 19);
            this.rdbCreateMesh.Name = "rdbCreateMesh";
            this.rdbCreateMesh.Size = new System.Drawing.Size(56, 17);
            this.rdbCreateMesh.TabIndex = 50;
            this.rdbCreateMesh.TabStop = true;
            this.rdbCreateMesh.Text = "Create";
            this.rdbCreateMesh.UseVisualStyleBackColor = true;
            this.rdbCreateMesh.Visible = false;
            this.rdbCreateMesh.CheckedChanged += new System.EventHandler(this.rdbCreateMesh_CheckedChanged);
            // 
            // btnFilePath
            // 
            this.btnFilePath.Location = new System.Drawing.Point(315, 48);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(24, 20);
            this.btnFilePath.TabIndex = 48;
            this.btnFilePath.Tag = "X";
            this.btnFilePath.Text = "...";
            this.btnFilePath.UseVisualStyleBackColor = true;
            this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
            // 
            // chxExistingUsed
            // 
            this.chxExistingUsed.AutoSize = true;
            this.chxExistingUsed.Enabled = false;
            this.chxExistingUsed.Location = new System.Drawing.Point(99, 78);
            this.chxExistingUsed.Name = "chxExistingUsed";
            this.chxExistingUsed.Size = new System.Drawing.Size(90, 17);
            this.chxExistingUsed.TabIndex = 14;
            this.chxExistingUsed.Text = "Existing Used";
            this.chxExistingUsed.UseVisualStyleBackColor = true;
            // 
            // txtMeshPath
            // 
            this.txtMeshPath.Location = new System.Drawing.Point(38, 48);
            this.txtMeshPath.Name = "txtMeshPath";
            this.txtMeshPath.Size = new System.Drawing.Size(272, 20);
            this.txtMeshPath.TabIndex = 4;
            // 
            // chxUseExisting
            // 
            this.chxUseExisting.AutoSize = true;
            this.chxUseExisting.Location = new System.Drawing.Point(9, 78);
            this.chxUseExisting.Name = "chxUseExisting";
            this.chxUseExisting.Size = new System.Drawing.Size(84, 17);
            this.chxUseExisting.TabIndex = 13;
            this.chxUseExisting.Text = "Use Existing";
            this.chxUseExisting.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "File:";
            // 
            // cmbDesirableMeshType
            // 
            this.cmbDesirableMeshType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDesirableMeshType.FormattingEnabled = true;
            this.cmbDesirableMeshType.Location = new System.Drawing.Point(122, 21);
            this.cmbDesirableMeshType.Name = "cmbDesirableMeshType";
            this.cmbDesirableMeshType.Size = new System.Drawing.Size(254, 21);
            this.cmbDesirableMeshType.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Desirable Mesh Type:";
            // 
            // gbAttachPointIDs
            // 
            this.gbAttachPointIDs.Controls.Add(this.btnGetNumAttachPints);
            this.gbAttachPointIDs.Controls.Add(this.ntxNumAttachPoints);
            this.gbAttachPointIDs.Controls.Add(this.label11);
            this.gbAttachPointIDs.Controls.Add(this.lstAttachPointChildren);
            this.gbAttachPointIDs.Controls.Add(this.btnGetAttachPointChildren);
            this.gbAttachPointIDs.Controls.Add(this.label10);
            this.gbAttachPointIDs.Controls.Add(this.ntxParentIndex);
            this.gbAttachPointIDs.Controls.Add(this.label9);
            this.gbAttachPointIDs.Controls.Add(this.ntxAttachPointIndex);
            this.gbAttachPointIDs.Controls.Add(this.label8);
            this.gbAttachPointIDs.Controls.Add(this.cmbMappedName);
            this.gbAttachPointIDs.Controls.Add(this.label7);
            this.gbAttachPointIDs.Controls.Add(this.cmbMappedNameType);
            this.gbAttachPointIDs.Controls.Add(this.btnRemoveID);
            this.gbAttachPointIDs.Controls.Add(this.dgvMappedIDs);
            this.gbAttachPointIDs.Controls.Add(this.ntxMappedID);
            this.gbAttachPointIDs.Controls.Add(this.btnSetMappedID);
            this.gbAttachPointIDs.Controls.Add(this.label5);
            this.gbAttachPointIDs.Controls.Add(this.label4);
            this.gbAttachPointIDs.Location = new System.Drawing.Point(0, 170);
            this.gbAttachPointIDs.Name = "gbAttachPointIDs";
            this.gbAttachPointIDs.Size = new System.Drawing.Size(389, 450);
            this.gbAttachPointIDs.TabIndex = 4;
            this.gbAttachPointIDs.TabStop = false;
            this.gbAttachPointIDs.Text = "Mapped Names And Ids";
            // 
            // btnGetNumAttachPints
            // 
            this.btnGetNumAttachPints.Location = new System.Drawing.Point(308, 340);
            this.btnGetNumAttachPints.Name = "btnGetNumAttachPints";
            this.btnGetNumAttachPints.Size = new System.Drawing.Size(75, 23);
            this.btnGetNumAttachPints.TabIndex = 25;
            this.btnGetNumAttachPints.Text = "Get";
            this.btnGetNumAttachPints.UseVisualStyleBackColor = true;
            this.btnGetNumAttachPints.Click += new System.EventHandler(this.btnGetNumAttachPints_Click);
            // 
            // ntxNumAttachPoints
            // 
            this.ntxNumAttachPoints.Location = new System.Drawing.Point(110, 342);
            this.ntxNumAttachPoints.Name = "ntxNumAttachPoints";
            this.ntxNumAttachPoints.Size = new System.Drawing.Size(60, 20);
            this.ntxNumAttachPoints.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 345);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Num Attach Points:";
            // 
            // lstAttachPointChildren
            // 
            this.lstAttachPointChildren.FormattingEnabled = true;
            this.lstAttachPointChildren.Location = new System.Drawing.Point(60, 409);
            this.lstAttachPointChildren.Name = "lstAttachPointChildren";
            this.lstAttachPointChildren.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstAttachPointChildren.Size = new System.Drawing.Size(192, 30);
            this.lstAttachPointChildren.TabIndex = 22;
            // 
            // btnGetAttachPointChildren
            // 
            this.btnGetAttachPointChildren.Location = new System.Drawing.Point(308, 383);
            this.btnGetAttachPointChildren.Name = "btnGetAttachPointChildren";
            this.btnGetAttachPointChildren.Size = new System.Drawing.Size(75, 56);
            this.btnGetAttachPointChildren.TabIndex = 21;
            this.btnGetAttachPointChildren.Text = "Get Attach Point Children";
            this.btnGetAttachPointChildren.UseVisualStyleBackColor = true;
            this.btnGetAttachPointChildren.Click += new System.EventHandler(this.btnGetAttachPointChildren_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 409);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Children:";
            // 
            // ntxParentIndex
            // 
            this.ntxParentIndex.Location = new System.Drawing.Point(85, 380);
            this.ntxParentIndex.Name = "ntxParentIndex";
            this.ntxParentIndex.Size = new System.Drawing.Size(60, 20);
            this.ntxParentIndex.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 383);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Parent Index:";
            // 
            // ntxAttachPointIndex
            // 
            this.ntxAttachPointIndex.Location = new System.Drawing.Point(48, 252);
            this.ntxAttachPointIndex.Name = "ntxAttachPointIndex";
            this.ntxAttachPointIndex.Size = new System.Drawing.Size(60, 20);
            this.ntxAttachPointIndex.TabIndex = 16;
            this.ntxAttachPointIndex.EnterKeyPress += new System.EventHandler(this.ntxAttachPointIndex_EnterKeyPress);
            this.ntxAttachPointIndex.Leave += new System.EventHandler(this.ntxAttachPointIndex_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Index:";
            // 
            // cmbMappedName
            // 
            this.cmbMappedName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMappedName.FormattingEnabled = true;
            this.cmbMappedName.Location = new System.Drawing.Point(203, 252);
            this.cmbMappedName.Name = "cmbMappedName";
            this.cmbMappedName.Size = new System.Drawing.Size(180, 21);
            this.cmbMappedName.TabIndex = 14;
            this.cmbMappedName.SelectedIndexChanged += new System.EventHandler(this.cmbMappedName_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Mapped Name Type:";
            // 
            // cmbMappedNameType
            // 
            this.cmbMappedNameType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMappedNameType.FormattingEnabled = true;
            this.cmbMappedNameType.Location = new System.Drawing.Point(122, 22);
            this.cmbMappedNameType.Name = "cmbMappedNameType";
            this.cmbMappedNameType.Size = new System.Drawing.Size(261, 21);
            this.cmbMappedNameType.TabIndex = 12;
            this.cmbMappedNameType.SelectedIndexChanged += new System.EventHandler(this.cmbMappedNameType_SelectedIndexChanged);
            // 
            // btnRemoveID
            // 
            this.btnRemoveID.Location = new System.Drawing.Point(227, 279);
            this.btnRemoveID.Name = "btnRemoveID";
            this.btnRemoveID.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveID.TabIndex = 11;
            this.btnRemoveID.Text = "Remove ID";
            this.btnRemoveID.UseVisualStyleBackColor = true;
            this.btnRemoveID.Click += new System.EventHandler(this.btnRemoveID_Click);
            // 
            // dgvMappedIDs
            // 
            this.dgvMappedIDs.AllowUserToAddRows = false;
            this.dgvMappedIDs.AllowUserToDeleteRows = false;
            this.dgvMappedIDs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMappedIDs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MappedID,
            this.MappedName});
            this.dgvMappedIDs.Location = new System.Drawing.Point(6, 49);
            this.dgvMappedIDs.MultiSelect = false;
            this.dgvMappedIDs.Name = "dgvMappedIDs";
            this.dgvMappedIDs.ReadOnly = true;
            this.dgvMappedIDs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMappedIDs.Size = new System.Drawing.Size(377, 197);
            this.dgvMappedIDs.TabIndex = 10;
            this.dgvMappedIDs.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgvAttachPointIDs_RowsRemoved);
            this.dgvMappedIDs.SelectionChanged += new System.EventHandler(this.dgvAttachPointIDs_SelectionChanged);
            // 
            // MappedID
            // 
            this.MappedID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MappedID.Frozen = true;
            this.MappedID.HeaderText = "Mapped ID";
            this.MappedID.Name = "MappedID";
            this.MappedID.ReadOnly = true;
            this.MappedID.Width = 170;
            // 
            // MappedName
            // 
            this.MappedName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MappedName.Frozen = true;
            this.MappedName.HeaderText = "Attach Point Name";
            this.MappedName.Name = "MappedName";
            this.MappedName.ReadOnly = true;
            this.MappedName.Width = 170;
            // 
            // ntxMappedID
            // 
            this.ntxMappedID.Location = new System.Drawing.Point(48, 281);
            this.ntxMappedID.Name = "ntxMappedID";
            this.ntxMappedID.Size = new System.Drawing.Size(60, 20);
            this.ntxMappedID.TabIndex = 9;
            // 
            // btnSetMappedID
            // 
            this.btnSetMappedID.Location = new System.Drawing.Point(308, 279);
            this.btnSetMappedID.Name = "btnSetMappedID";
            this.btnSetMappedID.Size = new System.Drawing.Size(75, 23);
            this.btnSetMappedID.TabIndex = 8;
            this.btnSetMappedID.Text = "Set ID";
            this.btnSetMappedID.UseVisualStyleBackColor = true;
            this.btnSetMappedID.Click += new System.EventHandler(this.btnSetMappedID_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 255);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Mapped Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "ID:";
            // 
            // btnCloseForm
            // 
            this.btnCloseForm.Location = new System.Drawing.Point(308, 673);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(75, 23);
            this.btnCloseForm.TabIndex = 10;
            this.btnCloseForm.Text = "Close";
            this.btnCloseForm.UseVisualStyleBackColor = true;
            this.btnCloseForm.Click += new System.EventHandler(this.btnCloseForm_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Transparent Color:";
            // 
            // btnTransparentColor
            // 
            this.btnTransparentColor.Location = new System.Drawing.Point(106, 11);
            this.btnTransparentColor.Name = "btnTransparentColor";
            this.btnTransparentColor.Size = new System.Drawing.Size(75, 23);
            this.btnTransparentColor.TabIndex = 12;
            this.btnTransparentColor.Text = "Color";
            this.btnTransparentColor.UseVisualStyleBackColor = true;
            this.btnTransparentColor.Click += new System.EventHandler(this.btnTransparentColor_Click);
            // 
            // gbTransparentColor
            // 
            this.gbTransparentColor.Controls.Add(this.chxTransparentColorEnabled);
            this.gbTransparentColor.Controls.Add(this.label6);
            this.gbTransparentColor.Controls.Add(this.btnTransparentColor);
            this.gbTransparentColor.Location = new System.Drawing.Point(0, 626);
            this.gbTransparentColor.Name = "gbTransparentColor";
            this.gbTransparentColor.Size = new System.Drawing.Size(389, 41);
            this.gbTransparentColor.TabIndex = 15;
            this.gbTransparentColor.TabStop = false;
            this.gbTransparentColor.Text = "Transparent Color";
            // 
            // chxTransparentColorEnabled
            // 
            this.chxTransparentColorEnabled.AutoSize = true;
            this.chxTransparentColorEnabled.Location = new System.Drawing.Point(187, 15);
            this.chxTransparentColorEnabled.Name = "chxTransparentColorEnabled";
            this.chxTransparentColorEnabled.Size = new System.Drawing.Size(65, 17);
            this.chxTransparentColorEnabled.TabIndex = 15;
            this.chxTransparentColorEnabled.Text = "Enabled";
            this.chxTransparentColorEnabled.UseVisualStyleBackColor = true;
            // 
            // rdbRecreateMesh
            // 
            this.rdbRecreateMesh.AutoSize = true;
            this.rdbRecreateMesh.Location = new System.Drawing.Point(75, 19);
            this.rdbRecreateMesh.Name = "rdbRecreateMesh";
            this.rdbRecreateMesh.Size = new System.Drawing.Size(72, 17);
            this.rdbRecreateMesh.TabIndex = 54;
            this.rdbRecreateMesh.TabStop = true;
            this.rdbRecreateMesh.Text = "Re-create";
            this.rdbRecreateMesh.UseVisualStyleBackColor = true;
            this.rdbRecreateMesh.Visible = false;
            this.rdbRecreateMesh.CheckedChanged += new System.EventHandler(this.rdbRecreateMesh_CheckedChanged);
            // 
            // frmCreateMesh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(418, 708);
            this.Controls.Add(this.gbTransparentColor);
            this.Controls.Add(this.btnCloseForm);
            this.Controls.Add(this.gbAttachPointIDs);
            this.Controls.Add(this.gbCreateUpdateMesh);
            this.Controls.Add(this.txtMeshType);
            this.Controls.Add(this.label1);
            this.Name = "frmCreateMesh";
            this.Text = "frmCreateMesh";
            this.Load += new System.EventHandler(this.frmCreateMesh_Load);
            this.gbCreateUpdateMesh.ResumeLayout(false);
            this.gbCreateUpdateMesh.PerformLayout();
            this.gbAttachPointIDs.ResumeLayout(false);
            this.gbAttachPointIDs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappedIDs)).EndInit();
            this.gbTransparentColor.ResumeLayout(false);
            this.gbTransparentColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMeshType;
        private System.Windows.Forms.GroupBox gbCreateUpdateMesh;
        private System.Windows.Forms.TextBox txtMeshPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDesirableMeshType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.GroupBox gbAttachPointIDs;
        private MCTester.Controls.NumericTextBox ntxMappedID;
        private System.Windows.Forms.Button btnSetMappedID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCloseForm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnTransparentColor;
        private System.Windows.Forms.CheckBox chxUseExisting;
        private System.Windows.Forms.CheckBox chxExistingUsed;
        private System.Windows.Forms.GroupBox gbTransparentColor;
        private System.Windows.Forms.CheckBox chxTransparentColorEnabled;
        private System.Windows.Forms.DataGridView dgvMappedIDs;
        private System.Windows.Forms.Button btnRemoveID;
        private System.Windows.Forms.RadioButton rdbUpdateMesh;
        private System.Windows.Forms.RadioButton rdbCreateMesh;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnMeshList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbMappedNameType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MappedID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MappedName;
        private System.Windows.Forms.ComboBox cmbMappedName;
        private MCTester.Controls.NumericTextBox ntxAttachPointIndex;
        private System.Windows.Forms.Label label8;
        private MCTester.Controls.NumericTextBox ntxParentIndex;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGetAttachPointChildren;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox lstAttachPointChildren;
        private MCTester.Controls.NumericTextBox ntxNumAttachPoints;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnGetNumAttachPints;
        private System.Windows.Forms.RadioButton rdbRecreateMesh;
    }
}