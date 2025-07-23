namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmPropertiesIDList
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
            this.dgvPropertyList = new System.Windows.Forms.DataGridView();
            this.btnResetEachProperty = new System.Windows.Forms.Button();
            this.btnResetAllProperties = new System.Windows.Forms.Button();
            this.btnSetEachChangedPropertyAsVarient = new System.Windows.Forms.Button();
            this.btnSetEachChangedPropertyAsType = new System.Windows.Forms.Button();
            this.btnSetAllChangedPropertiesAsVarient = new System.Windows.Forms.Button();
            this.btnSetEachChangedNames = new System.Windows.Forms.Button();
            this.btnUpdatePropertiesAndLocationPoints = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCloseWithoutSave = new System.Windows.Forms.Button();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetPropertyIdByName = new System.Windows.Forms.Button();
            this.llPropertyId = new System.Windows.Forms.LinkLabel();
            this.gbPropertiesNames = new System.Windows.Forms.GroupBox();
            this.btnGetPropertiesAndLocationPoints = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlUpdateProperties = new System.Windows.Forms.Panel();
            this.ntxLocationIndex = new MCTester.Controls.NumericTextBox();
            this.chxByNameIfExists = new System.Windows.Forms.CheckBox();
            this.btnGetProperties = new System.Windows.Forms.Button();
            this.pnlObjects = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.colPropertyID = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colPropertyType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPropertyValue = new System.Windows.Forms.DataGridViewButtonColumn();
            this.UseDefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSetPropertyIndicator = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsChanged = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ToReset = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyList)).BeginInit();
            this.gbPropertiesNames.SuspendLayout();
            this.pnlUpdateProperties.SuspendLayout();
            this.pnlObjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPropertyList
            // 
            this.dgvPropertyList.AllowUserToAddRows = false;
            this.dgvPropertyList.AllowUserToDeleteRows = false;
            this.dgvPropertyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropertyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPropertyID,
            this.colPropertyType,
            this.colName,
            this.colPropertyValue,
            this.UseDefault,
            this.colSetPropertyIndicator,
            this.Value,
            this.IsChanged,
            this.ToReset});
            this.dgvPropertyList.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvPropertyList.Location = new System.Drawing.Point(0, 0);
            this.dgvPropertyList.Name = "dgvPropertyList";
            this.dgvPropertyList.Size = new System.Drawing.Size(852, 406);
            this.dgvPropertyList.TabIndex = 0;
            this.dgvPropertyList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPropertyList_CellContentClick);
            this.dgvPropertyList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPropertyList_CellValueChanged);
            this.dgvPropertyList.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvPropertyList_CurrentCellDirtyStateChanged);
            // 
            // btnResetEachProperty
            // 
            this.btnResetEachProperty.Location = new System.Drawing.Point(3, 37);
            this.btnResetEachProperty.Name = "btnResetEachProperty";
            this.btnResetEachProperty.Size = new System.Drawing.Size(162, 23);
            this.btnResetEachProperty.TabIndex = 22;
            this.btnResetEachProperty.Text = "Reset Each Selected Property";
            this.btnResetEachProperty.UseVisualStyleBackColor = true;
            this.btnResetEachProperty.Click += new System.EventHandler(this.btnResetEachProperty_Click);
            // 
            // btnResetAllProperties
            // 
            this.btnResetAllProperties.Location = new System.Drawing.Point(3, 66);
            this.btnResetAllProperties.Name = "btnResetAllProperties";
            this.btnResetAllProperties.Size = new System.Drawing.Size(162, 23);
            this.btnResetAllProperties.TabIndex = 2;
            this.btnResetAllProperties.Text = "Reset All Properties";
            this.btnResetAllProperties.UseVisualStyleBackColor = true;
            this.btnResetAllProperties.Click += new System.EventHandler(this.btnResetAllProperties_Click);
            // 
            // btnSetEachChangedPropertyAsVarient
            // 
            this.btnSetEachChangedPropertyAsVarient.Location = new System.Drawing.Point(12, 486);
            this.btnSetEachChangedPropertyAsVarient.Name = "btnSetEachChangedPropertyAsVarient";
            this.btnSetEachChangedPropertyAsVarient.Size = new System.Drawing.Size(215, 23);
            this.btnSetEachChangedPropertyAsVarient.TabIndex = 0;
            this.btnSetEachChangedPropertyAsVarient.Text = "Set Each Changed Property As Variant";
            this.btnSetEachChangedPropertyAsVarient.UseVisualStyleBackColor = true;
            this.btnSetEachChangedPropertyAsVarient.Click += new System.EventHandler(this.btnSetEachChangedPropertyAsVarient_Click);
            // 
            // btnSetEachChangedPropertyAsType
            // 
            this.btnSetEachChangedPropertyAsType.Location = new System.Drawing.Point(12, 457);
            this.btnSetEachChangedPropertyAsType.Name = "btnSetEachChangedPropertyAsType";
            this.btnSetEachChangedPropertyAsType.Size = new System.Drawing.Size(215, 23);
            this.btnSetEachChangedPropertyAsType.TabIndex = 1;
            this.btnSetEachChangedPropertyAsType.Text = "Set Each Changed Property As Type";
            this.btnSetEachChangedPropertyAsType.UseVisualStyleBackColor = true;
            this.btnSetEachChangedPropertyAsType.Click += new System.EventHandler(this.btnSetEachChangedPropertyAsType_Click);
            // 
            // btnSetAllChangedPropertiesAsVarient
            // 
            this.btnSetAllChangedPropertiesAsVarient.Location = new System.Drawing.Point(12, 515);
            this.btnSetAllChangedPropertiesAsVarient.Name = "btnSetAllChangedPropertiesAsVarient";
            this.btnSetAllChangedPropertiesAsVarient.Size = new System.Drawing.Size(215, 23);
            this.btnSetAllChangedPropertiesAsVarient.TabIndex = 2;
            this.btnSetAllChangedPropertiesAsVarient.Text = "Set All Changed Properties As Variant";
            this.btnSetAllChangedPropertiesAsVarient.UseVisualStyleBackColor = true;
            this.btnSetAllChangedPropertiesAsVarient.Click += new System.EventHandler(this.btnSetAllChangedPropertiesAsVarient_Click);
            // 
            // btnSetEachChangedNames
            // 
            this.btnSetEachChangedNames.Location = new System.Drawing.Point(86, 22);
            this.btnSetEachChangedNames.Name = "btnSetEachChangedNames";
            this.btnSetEachChangedNames.Size = new System.Drawing.Size(133, 23);
            this.btnSetEachChangedNames.TabIndex = 23;
            this.btnSetEachChangedNames.Text = "Set All Names";
            this.btnSetEachChangedNames.UseVisualStyleBackColor = true;
            this.btnSetEachChangedNames.Click += new System.EventHandler(this.btnSetEachChangedNames_Click);
            // 
            // btnUpdatePropertiesAndLocationPoints
            // 
            this.btnUpdatePropertiesAndLocationPoints.Location = new System.Drawing.Point(3, 3);
            this.btnUpdatePropertiesAndLocationPoints.Name = "btnUpdatePropertiesAndLocationPoints";
            this.btnUpdatePropertiesAndLocationPoints.Size = new System.Drawing.Size(215, 23);
            this.btnUpdatePropertiesAndLocationPoints.TabIndex = 25;
            this.btnUpdatePropertiesAndLocationPoints.Text = "Update Properties And Location Points";
            this.btnUpdatePropertiesAndLocationPoints.UseVisualStyleBackColor = true;
            this.btnUpdatePropertiesAndLocationPoints.Click += new System.EventHandler(this.btnUpdatePropertiesAndLocationPoints_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(687, 587);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.TabIndex = 26;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCloseWithoutSave
            // 
            this.btnCloseWithoutSave.Location = new System.Drawing.Point(769, 587);
            this.btnCloseWithoutSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseWithoutSave.Name = "btnCloseWithoutSave";
            this.btnCloseWithoutSave.Size = new System.Drawing.Size(75, 22);
            this.btnCloseWithoutSave.TabIndex = 27;
            this.btnCloseWithoutSave.Text = "OK";
            this.btnCloseWithoutSave.UseVisualStyleBackColor = true;
            this.btnCloseWithoutSave.Click += new System.EventHandler(this.btnCloseWithoutSave_Click);
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(86, 59);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(133, 20);
            this.txtPropertyName.TabIndex = 49;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "Property Name:";
            // 
            // btnGetPropertyIdByName
            // 
            this.btnGetPropertyIdByName.Location = new System.Drawing.Point(86, 82);
            this.btnGetPropertyIdByName.Name = "btnGetPropertyIdByName";
            this.btnGetPropertyIdByName.Size = new System.Drawing.Size(133, 22);
            this.btnGetPropertyIdByName.TabIndex = 47;
            this.btnGetPropertyIdByName.Text = "Get Property Id By Name";
            this.btnGetPropertyIdByName.UseVisualStyleBackColor = true;
            this.btnGetPropertyIdByName.Click += new System.EventHandler(this.btnGetPropertyIdByName_Click);
            // 
            // llPropertyId
            // 
            this.llPropertyId.AutoSize = true;
            this.llPropertyId.Location = new System.Drawing.Point(224, 62);
            this.llPropertyId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llPropertyId.Name = "llPropertyId";
            this.llPropertyId.Size = new System.Drawing.Size(0, 13);
            this.llPropertyId.TabIndex = 50;
            this.llPropertyId.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llPropertyId_LinkClicked);
            // 
            // gbPropertiesNames
            // 
            this.gbPropertiesNames.Controls.Add(this.btnSetEachChangedNames);
            this.gbPropertiesNames.Controls.Add(this.llPropertyId);
            this.gbPropertiesNames.Controls.Add(this.btnGetPropertyIdByName);
            this.gbPropertiesNames.Controls.Add(this.txtPropertyName);
            this.gbPropertiesNames.Controls.Add(this.label3);
            this.gbPropertiesNames.Location = new System.Drawing.Point(542, 420);
            this.gbPropertiesNames.Margin = new System.Windows.Forms.Padding(2);
            this.gbPropertiesNames.Name = "gbPropertiesNames";
            this.gbPropertiesNames.Padding = new System.Windows.Forms.Padding(2);
            this.gbPropertiesNames.Size = new System.Drawing.Size(300, 118);
            this.gbPropertiesNames.TabIndex = 51;
            this.gbPropertiesNames.TabStop = false;
            this.gbPropertiesNames.Text = "Properties Names";
            // 
            // btnGetPropertiesAndLocationPoints
            // 
            this.btnGetPropertiesAndLocationPoints.Location = new System.Drawing.Point(158, 32);
            this.btnGetPropertiesAndLocationPoints.Name = "btnGetPropertiesAndLocationPoints";
            this.btnGetPropertiesAndLocationPoints.Size = new System.Drawing.Size(236, 23);
            this.btnGetPropertiesAndLocationPoints.TabIndex = 52;
            this.btnGetPropertiesAndLocationPoints.Text = "Get Properties And Location Points";
            this.btnGetPropertiesAndLocationPoints.UseVisualStyleBackColor = true;
            this.btnGetPropertiesAndLocationPoints.Click += new System.EventHandler(this.btnGetPropertiesAndLocationPoints_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(8, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Location Index:";
            // 
            // pnlUpdateProperties
            // 
            this.pnlUpdateProperties.Controls.Add(this.btnGetPropertiesAndLocationPoints);
            this.pnlUpdateProperties.Controls.Add(this.ntxLocationIndex);
            this.pnlUpdateProperties.Controls.Add(this.label4);
            this.pnlUpdateProperties.Controls.Add(this.btnUpdatePropertiesAndLocationPoints);
            this.pnlUpdateProperties.Location = new System.Drawing.Point(9, 545);
            this.pnlUpdateProperties.Margin = new System.Windows.Forms.Padding(2);
            this.pnlUpdateProperties.Name = "pnlUpdateProperties";
            this.pnlUpdateProperties.Size = new System.Drawing.Size(400, 60);
            this.pnlUpdateProperties.TabIndex = 55;
            // 
            // ntxLocationIndex
            // 
            this.ntxLocationIndex.Location = new System.Drawing.Point(94, 33);
            this.ntxLocationIndex.Name = "ntxLocationIndex";
            this.ntxLocationIndex.Size = new System.Drawing.Size(58, 20);
            this.ntxLocationIndex.TabIndex = 53;
            this.ntxLocationIndex.Text = "0";
            // 
            // chxByNameIfExists
            // 
            this.chxByNameIfExists.AutoSize = true;
            this.chxByNameIfExists.Location = new System.Drawing.Point(12, 438);
            this.chxByNameIfExists.Name = "chxByNameIfExists";
            this.chxByNameIfExists.Size = new System.Drawing.Size(111, 17);
            this.chxByNameIfExists.TabIndex = 58;
            this.chxByNameIfExists.Text = "By Name If Exists ";
            this.chxByNameIfExists.UseVisualStyleBackColor = true;
            // 
            // btnGetProperties
            // 
            this.btnGetProperties.Location = new System.Drawing.Point(3, 9);
            this.btnGetProperties.Name = "btnGetProperties";
            this.btnGetProperties.Size = new System.Drawing.Size(162, 22);
            this.btnGetProperties.TabIndex = 51;
            this.btnGetProperties.Text = "Get Properties";
            this.btnGetProperties.UseVisualStyleBackColor = true;
            this.btnGetProperties.Click += new System.EventHandler(this.btnGetProperties_Click);
            // 
            // pnlObjects
            // 
            this.pnlObjects.Controls.Add(this.btnGetProperties);
            this.pnlObjects.Controls.Add(this.btnResetEachProperty);
            this.pnlObjects.Controls.Add(this.btnResetAllProperties);
            this.pnlObjects.Location = new System.Drawing.Point(238, 449);
            this.pnlObjects.Margin = new System.Windows.Forms.Padding(2);
            this.pnlObjects.Name = "pnlObjects";
            this.pnlObjects.Size = new System.Drawing.Size(171, 93);
            this.pnlObjects.TabIndex = 59;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 411);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "* Click to show properties using this id";
            // 
            // colPropertyID
            // 
            this.colPropertyID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colPropertyID.HeaderText = "ID*";
            this.colPropertyID.Name = "colPropertyID";
            this.colPropertyID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPropertyID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colPropertyID.ToolTipText = "Get Nodes";
            this.colPropertyID.Width = 47;
            // 
            // colPropertyType
            // 
            this.colPropertyType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colPropertyType.HeaderText = "Type";
            this.colPropertyType.Name = "colPropertyType";
            this.colPropertyType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPropertyType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colPropertyType.Width = 37;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            // 
            // colPropertyValue
            // 
            this.colPropertyValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colPropertyValue.HeaderText = "Value";
            this.colPropertyValue.Name = "colPropertyValue";
            this.colPropertyValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colPropertyValue.Width = 40;
            // 
            // UseDefault
            // 
            this.UseDefault.HeaderText = "Use Default";
            this.UseDefault.Name = "UseDefault";
            this.UseDefault.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UseDefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.UseDefault.Visible = false;
            this.UseDefault.Width = 88;
            // 
            // colSetPropertyIndicator
            // 
            this.colSetPropertyIndicator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSetPropertyIndicator.HeaderText = "Set/UnSet";
            this.colSetPropertyIndicator.Name = "colSetPropertyIndicator";
            this.colSetPropertyIndicator.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colSetPropertyIndicator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colSetPropertyIndicator.Visible = false;
            this.colSetPropertyIndicator.Width = 83;
            // 
            // Value
            // 
            this.Value.HeaderText = "Property Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 250;
            // 
            // IsChanged
            // 
            this.IsChanged.HeaderText = "Is Changed";
            this.IsChanged.Name = "IsChanged";
            // 
            // ToReset
            // 
            this.ToReset.HeaderText = "To Reset";
            this.ToReset.Name = "ToReset";
            // 
            // frmPropertiesIDList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(852, 614);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlObjects);
            this.Controls.Add(this.chxByNameIfExists);
            this.Controls.Add(this.pnlUpdateProperties);
            this.Controls.Add(this.gbPropertiesNames);
            this.Controls.Add(this.btnCloseWithoutSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSetEachChangedPropertyAsVarient);
            this.Controls.Add(this.btnSetEachChangedPropertyAsType);
            this.Controls.Add(this.btnSetAllChangedPropertiesAsVarient);
            this.Controls.Add(this.dgvPropertyList);
            this.Name = "frmPropertiesIDList";
            this.Text = "Property List";
            this.Load += new System.EventHandler(this.frmPropertiesIDList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyList)).EndInit();
            this.gbPropertiesNames.ResumeLayout(false);
            this.gbPropertiesNames.PerformLayout();
            this.pnlUpdateProperties.ResumeLayout(false);
            this.pnlUpdateProperties.PerformLayout();
            this.pnlObjects.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPropertyList;
        private System.Windows.Forms.Button btnResetEachProperty;
        private System.Windows.Forms.Button btnSetAllChangedPropertiesAsVarient;
        private System.Windows.Forms.Button btnSetEachChangedPropertyAsType;
        private System.Windows.Forms.Button btnSetEachChangedPropertyAsVarient;
        private System.Windows.Forms.Button btnResetAllProperties;
        private System.Windows.Forms.Button btnSetEachChangedNames;
        private System.Windows.Forms.Button btnUpdatePropertiesAndLocationPoints;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCloseWithoutSave;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetPropertyIdByName;
        private System.Windows.Forms.LinkLabel llPropertyId;
        private System.Windows.Forms.GroupBox gbPropertiesNames;
        private System.Windows.Forms.Button btnGetPropertiesAndLocationPoints;
        private Controls.NumericTextBox ntxLocationIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlUpdateProperties;
        private System.Windows.Forms.CheckBox chxByNameIfExists;
        private System.Windows.Forms.Button btnGetProperties;
        private System.Windows.Forms.Panel pnlObjects;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewButtonColumn colPropertyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewButtonColumn colPropertyValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UseDefault;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSetPropertyIndicator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsChanged;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ToReset;
    }
}