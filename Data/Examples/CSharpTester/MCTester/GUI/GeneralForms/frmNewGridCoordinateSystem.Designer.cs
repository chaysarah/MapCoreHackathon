namespace MCTester.General_Forms
{
    //MCTester.Controls.NumericTextBox()
    partial class frmNewGridCoordinateSystem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewGridCoordinateSystem));
            this.btnOK = new System.Windows.Forms.Button();
            this.gbGridCoordinateSystem = new System.Windows.Forms.GroupBox();
            this.gpOgcCrsCode = new System.Windows.Forms.GroupBox();
            this.chxIsExist = new System.Windows.Forms.CheckBox();
            this.txtOgcCrsCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ctrWorldPt = new MCTester.Controls.CtrlSamplePoint();
            this.ntbZone = new MCTester.Controls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chxIsLocationLegal = new System.Windows.Forms.CheckBox();
            this.chxIsGeographicLocationLegal = new System.Windows.Forms.CheckBox();
            this.btnGetDefaultZoneFromGeographicLocation = new System.Windows.Forms.Button();
            this.ctrlLocation = new MCTester.Controls.Ctrl3DVector();
            this.btnIsGeographicLocationLegal = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnIsLocationLegal = new System.Windows.Forms.Button();
            this.btnSetLegalValuesForGridCoordinates = new System.Windows.Forms.Button();
            this.btnSetLegalValuesForGeographicCoordinates = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstExistingGridCoordSys = new System.Windows.Forms.ListBox();
            this.chxIsEqual = new System.Windows.Forms.CheckBox();
            this.boxLegalValuesForGridCoordinates = new MCTester.Controls.CtrlSMcBox();
            this.boxLegalValuesForGeographicCoordinates = new MCTester.Controls.CtrlSMcBox();
            this.chxIsUtm = new System.Windows.Forms.CheckBox();
            this.chxIsGeographical = new System.Windows.Forms.CheckBox();
            this.chxIsMultyZoneGrid = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCloneAsGeneric = new System.Windows.Forms.Button();
            this.btnCloneAsNonGeneric = new System.Windows.Forms.Button();
            this.chxAddToGridCoordinate = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ctrlNewGridCoordinateSystem1 = new MCTester.General_Forms.CtrlNewGridCoordinateSystem();
            this.gbGridCoordinateSystem.SuspendLayout();
            this.gpOgcCrsCode.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(509, 291);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 39;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbGridCoordinateSystem
            // 
            this.gbGridCoordinateSystem.Controls.Add(this.gpOgcCrsCode);
            this.gbGridCoordinateSystem.Controls.Add(this.groupBox2);
            this.gbGridCoordinateSystem.Controls.Add(this.btnSetLegalValuesForGridCoordinates);
            this.gbGridCoordinateSystem.Controls.Add(this.btnSetLegalValuesForGeographicCoordinates);
            this.gbGridCoordinateSystem.Controls.Add(this.groupBox1);
            this.gbGridCoordinateSystem.Controls.Add(this.boxLegalValuesForGridCoordinates);
            this.gbGridCoordinateSystem.Controls.Add(this.boxLegalValuesForGeographicCoordinates);
            this.gbGridCoordinateSystem.Controls.Add(this.chxIsUtm);
            this.gbGridCoordinateSystem.Controls.Add(this.chxIsGeographical);
            this.gbGridCoordinateSystem.Controls.Add(this.chxIsMultyZoneGrid);
            this.gbGridCoordinateSystem.Enabled = false;
            this.gbGridCoordinateSystem.Location = new System.Drawing.Point(7, 333);
            this.gbGridCoordinateSystem.Name = "gbGridCoordinateSystem";
            this.gbGridCoordinateSystem.Size = new System.Drawing.Size(733, 289);
            this.gbGridCoordinateSystem.TabIndex = 64;
            this.gbGridCoordinateSystem.TabStop = false;
            this.gbGridCoordinateSystem.Text = "Grid Coordinate System";
            // 
            // gpOgcCrsCode
            // 
            this.gpOgcCrsCode.Controls.Add(this.chxIsExist);
            this.gpOgcCrsCode.Controls.Add(this.txtOgcCrsCode);
            this.gpOgcCrsCode.Location = new System.Drawing.Point(5, 42);
            this.gpOgcCrsCode.Name = "gpOgcCrsCode";
            this.gpOgcCrsCode.Size = new System.Drawing.Size(401, 58);
            this.gpOgcCrsCode.TabIndex = 69;
            this.gpOgcCrsCode.TabStop = false;
            this.gpOgcCrsCode.Text = "Ogc Crs Code";
            // 
            // chxIsExist
            // 
            this.chxIsExist.AutoSize = true;
            this.chxIsExist.Enabled = false;
            this.chxIsExist.Location = new System.Drawing.Point(6, 22);
            this.chxIsExist.Name = "chxIsExist";
            this.chxIsExist.Size = new System.Drawing.Size(56, 17);
            this.chxIsExist.TabIndex = 70;
            this.chxIsExist.Text = "Found";
            this.chxIsExist.UseVisualStyleBackColor = true;
            // 
            // txtOgcCrsCode
            // 
            this.txtOgcCrsCode.Location = new System.Drawing.Point(75, 20);
            this.txtOgcCrsCode.Name = "txtOgcCrsCode";
            this.txtOgcCrsCode.ReadOnly = true;
            this.txtOgcCrsCode.Size = new System.Drawing.Size(256, 20);
            this.txtOgcCrsCode.TabIndex = 69;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ctrWorldPt);
            this.groupBox2.Controls.Add(this.ntbZone);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chxIsLocationLegal);
            this.groupBox2.Controls.Add(this.chxIsGeographicLocationLegal);
            this.groupBox2.Controls.Add(this.btnGetDefaultZoneFromGeographicLocation);
            this.groupBox2.Controls.Add(this.ctrlLocation);
            this.groupBox2.Controls.Add(this.btnIsGeographicLocationLegal);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnIsLocationLegal);
            this.groupBox2.Location = new System.Drawing.Point(409, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 129);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            // 
            // ctrWorldPt
            // 
            this.ctrWorldPt._DgvControlName = null;
            this.ctrWorldPt._IsRelativeToDTM = false;
            this.ctrWorldPt._PointInOverlayManagerCoordSys = false;
            this.ctrWorldPt._PointZValue = 1.7976931348623157E+308D;
            this.ctrWorldPt._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrWorldPt._SampleOnePoint = true;
            this.ctrWorldPt._UserControlName = "ctrlLocation";
            this.ctrWorldPt.IsAsync = false;
            this.ctrWorldPt.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrWorldPt.Location = new System.Drawing.Point(279, 13);
            this.ctrWorldPt.Name = "ctrWorldPt";
            this.ctrWorldPt.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrWorldPt.Size = new System.Drawing.Size(32, 23);
            this.ctrWorldPt.TabIndex = 66;
            this.ctrWorldPt.Text = "...";
            this.ctrWorldPt.UseVisualStyleBackColor = true;
            // 
            // ntbZone
            // 
            this.ntbZone.Enabled = false;
            this.ntbZone.Location = new System.Drawing.Point(275, 102);
            this.ntbZone.Name = "ntbZone";
            this.ntbZone.Size = new System.Drawing.Size(38, 20);
            this.ntbZone.TabIndex = 67;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(235, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 66;
            this.label9.Text = "Zone:";
            // 
            // chxIsLocationLegal
            // 
            this.chxIsLocationLegal.AutoSize = true;
            this.chxIsLocationLegal.Enabled = false;
            this.chxIsLocationLegal.Location = new System.Drawing.Point(237, 46);
            this.chxIsLocationLegal.Name = "chxIsLocationLegal";
            this.chxIsLocationLegal.Size = new System.Drawing.Size(63, 17);
            this.chxIsLocationLegal.TabIndex = 71;
            this.chxIsLocationLegal.Text = "Is Legal";
            this.chxIsLocationLegal.UseVisualStyleBackColor = true;
            // 
            // chxIsGeographicLocationLegal
            // 
            this.chxIsGeographicLocationLegal.AutoSize = true;
            this.chxIsGeographicLocationLegal.Enabled = false;
            this.chxIsGeographicLocationLegal.Location = new System.Drawing.Point(237, 75);
            this.chxIsGeographicLocationLegal.Name = "chxIsGeographicLocationLegal";
            this.chxIsGeographicLocationLegal.Size = new System.Drawing.Size(63, 17);
            this.chxIsGeographicLocationLegal.TabIndex = 70;
            this.chxIsGeographicLocationLegal.Text = "Is Legal";
            this.chxIsGeographicLocationLegal.UseVisualStyleBackColor = true;
            // 
            // btnGetDefaultZoneFromGeographicLocation
            // 
            this.btnGetDefaultZoneFromGeographicLocation.Location = new System.Drawing.Point(4, 100);
            this.btnGetDefaultZoneFromGeographicLocation.Name = "btnGetDefaultZoneFromGeographicLocation";
            this.btnGetDefaultZoneFromGeographicLocation.Size = new System.Drawing.Size(227, 23);
            this.btnGetDefaultZoneFromGeographicLocation.TabIndex = 69;
            this.btnGetDefaultZoneFromGeographicLocation.Text = "Get Default Zone From Geographic Location";
            this.btnGetDefaultZoneFromGeographicLocation.UseVisualStyleBackColor = true;
            this.btnGetDefaultZoneFromGeographicLocation.Click += new System.EventHandler(this.btnGetDefaultZoneFromGeographicLocation_Click);
            // 
            // ctrlLocation
            // 
            this.ctrlLocation.IsReadOnly = false;
            this.ctrlLocation.Location = new System.Drawing.Point(51, 11);
            this.ctrlLocation.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlLocation.Name = "ctrlLocation";
            this.ctrlLocation.Size = new System.Drawing.Size(232, 26);
            this.ctrlLocation.TabIndex = 42;
            this.ctrlLocation.X = 0D;
            this.ctrlLocation.Y = 0D;
            this.ctrlLocation.Z = 0D;
            // 
            // btnIsGeographicLocationLegal
            // 
            this.btnIsGeographicLocationLegal.Location = new System.Drawing.Point(4, 71);
            this.btnIsGeographicLocationLegal.Name = "btnIsGeographicLocationLegal";
            this.btnIsGeographicLocationLegal.Size = new System.Drawing.Size(155, 23);
            this.btnIsGeographicLocationLegal.TabIndex = 68;
            this.btnIsGeographicLocationLegal.Text = "Is Geographic Location Legal";
            this.btnIsGeographicLocationLegal.UseVisualStyleBackColor = true;
            this.btnIsGeographicLocationLegal.Click += new System.EventHandler(this.btnIsGeographicLocationLegal_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 66;
            this.label8.Text = "Location";
            // 
            // btnIsLocationLegal
            // 
            this.btnIsLocationLegal.Location = new System.Drawing.Point(4, 42);
            this.btnIsLocationLegal.Name = "btnIsLocationLegal";
            this.btnIsLocationLegal.Size = new System.Drawing.Size(101, 23);
            this.btnIsLocationLegal.TabIndex = 67;
            this.btnIsLocationLegal.Text = "Is Location Legal";
            this.btnIsLocationLegal.UseVisualStyleBackColor = true;
            this.btnIsLocationLegal.Click += new System.EventHandler(this.btnIsLocationLegal_Click);
            // 
            // btnSetLegalValuesForGridCoordinates
            // 
            this.btnSetLegalValuesForGridCoordinates.Location = new System.Drawing.Point(343, 247);
            this.btnSetLegalValuesForGridCoordinates.Name = "btnSetLegalValuesForGridCoordinates";
            this.btnSetLegalValuesForGridCoordinates.Size = new System.Drawing.Size(59, 23);
            this.btnSetLegalValuesForGridCoordinates.TabIndex = 41;
            this.btnSetLegalValuesForGridCoordinates.Text = "Set";
            this.btnSetLegalValuesForGridCoordinates.UseVisualStyleBackColor = true;
            this.btnSetLegalValuesForGridCoordinates.Click += new System.EventHandler(this.btnSetLegalValuesForGridCoordinates_Click);
            // 
            // btnSetLegalValuesForGeographicCoordinates
            // 
            this.btnSetLegalValuesForGeographicCoordinates.Location = new System.Drawing.Point(342, 157);
            this.btnSetLegalValuesForGeographicCoordinates.Name = "btnSetLegalValuesForGeographicCoordinates";
            this.btnSetLegalValuesForGeographicCoordinates.Size = new System.Drawing.Size(59, 23);
            this.btnSetLegalValuesForGeographicCoordinates.TabIndex = 40;
            this.btnSetLegalValuesForGeographicCoordinates.Text = "Set";
            this.btnSetLegalValuesForGeographicCoordinates.UseVisualStyleBackColor = true;
            this.btnSetLegalValuesForGeographicCoordinates.Click += new System.EventHandler(this.btnSetLegalValuesForGeographicCoordinates_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstExistingGridCoordSys);
            this.groupBox1.Controls.Add(this.chxIsEqual);
            this.groupBox1.Location = new System.Drawing.Point(409, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(318, 139);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Is Equal";
            // 
            // lstExistingGridCoordSys
            // 
            this.lstExistingGridCoordSys.FormattingEnabled = true;
            this.lstExistingGridCoordSys.Location = new System.Drawing.Point(8, 19);
            this.lstExistingGridCoordSys.Name = "lstExistingGridCoordSys";
            this.lstExistingGridCoordSys.Size = new System.Drawing.Size(306, 95);
            this.lstExistingGridCoordSys.TabIndex = 41;
            this.lstExistingGridCoordSys.SelectedIndexChanged += new System.EventHandler(this.lstExistingGridCoordSys_SelectedIndexChanged);
            this.lstExistingGridCoordSys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstExistingGridCoordSys_MouseDoubleClick);
            // 
            // chxIsEqual
            // 
            this.chxIsEqual.AutoSize = true;
            this.chxIsEqual.Enabled = false;
            this.chxIsEqual.Location = new System.Drawing.Point(8, 118);
            this.chxIsEqual.Name = "chxIsEqual";
            this.chxIsEqual.Size = new System.Drawing.Size(63, 17);
            this.chxIsEqual.TabIndex = 7;
            this.chxIsEqual.Text = "Is equal";
            this.chxIsEqual.UseVisualStyleBackColor = true;
            // 
            // boxLegalValuesForGridCoordinates
            // 
            this.boxLegalValuesForGridCoordinates.GroupBoxText = "Legal Values For Grid Coordinates";
            this.boxLegalValuesForGridCoordinates.Location = new System.Drawing.Point(6, 196);
            this.boxLegalValuesForGridCoordinates.Margin = new System.Windows.Forms.Padding(4);
            this.boxLegalValuesForGridCoordinates.Name = "boxLegalValuesForGridCoordinates";
            this.boxLegalValuesForGridCoordinates.Size = new System.Drawing.Size(400, 84);
            this.boxLegalValuesForGridCoordinates.TabIndex = 5;
            // 
            // boxLegalValuesForGeographicCoordinates
            // 
            this.boxLegalValuesForGeographicCoordinates.GroupBoxText = "Legal Values For Geographic Coordinates";
            this.boxLegalValuesForGeographicCoordinates.Location = new System.Drawing.Point(5, 106);
            this.boxLegalValuesForGeographicCoordinates.Margin = new System.Windows.Forms.Padding(4);
            this.boxLegalValuesForGeographicCoordinates.Name = "boxLegalValuesForGeographicCoordinates";
            this.boxLegalValuesForGeographicCoordinates.Size = new System.Drawing.Size(400, 84);
            this.boxLegalValuesForGeographicCoordinates.TabIndex = 4;
            // 
            // chxIsUtm
            // 
            this.chxIsUtm.AutoSize = true;
            this.chxIsUtm.Enabled = false;
            this.chxIsUtm.Location = new System.Drawing.Point(259, 19);
            this.chxIsUtm.Name = "chxIsUtm";
            this.chxIsUtm.Size = new System.Drawing.Size(54, 17);
            this.chxIsUtm.TabIndex = 3;
            this.chxIsUtm.Text = "Is utm";
            this.chxIsUtm.UseVisualStyleBackColor = true;
            // 
            // chxIsGeographical
            // 
            this.chxIsGeographical.AutoSize = true;
            this.chxIsGeographical.Enabled = false;
            this.chxIsGeographical.Location = new System.Drawing.Point(135, 19);
            this.chxIsGeographical.Name = "chxIsGeographical";
            this.chxIsGeographical.Size = new System.Drawing.Size(98, 17);
            this.chxIsGeographical.TabIndex = 1;
            this.chxIsGeographical.Text = "Is geographical";
            this.chxIsGeographical.UseVisualStyleBackColor = true;
            // 
            // chxIsMultyZoneGrid
            // 
            this.chxIsMultyZoneGrid.AutoSize = true;
            this.chxIsMultyZoneGrid.Enabled = false;
            this.chxIsMultyZoneGrid.Location = new System.Drawing.Point(6, 19);
            this.chxIsMultyZoneGrid.Name = "chxIsMultyZoneGrid";
            this.chxIsMultyZoneGrid.Size = new System.Drawing.Size(107, 17);
            this.chxIsMultyZoneGrid.TabIndex = 0;
            this.chxIsMultyZoneGrid.Text = "Is multy zone grid";
            this.chxIsMultyZoneGrid.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(76, 14);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 65;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCloneAsGeneric
            // 
            this.btnCloneAsGeneric.Location = new System.Drawing.Point(10, 57);
            this.btnCloneAsGeneric.Name = "btnCloneAsGeneric";
            this.btnCloneAsGeneric.Size = new System.Drawing.Size(125, 23);
            this.btnCloneAsGeneric.TabIndex = 66;
            this.btnCloneAsGeneric.Text = "As Generic";
            this.btnCloneAsGeneric.UseVisualStyleBackColor = true;
            this.btnCloneAsGeneric.Click += new System.EventHandler(this.btnCloneAsGeneric_Click);
            // 
            // btnCloneAsNonGeneric
            // 
            this.btnCloneAsNonGeneric.Location = new System.Drawing.Point(10, 86);
            this.btnCloneAsNonGeneric.Name = "btnCloneAsNonGeneric";
            this.btnCloneAsNonGeneric.Size = new System.Drawing.Size(125, 23);
            this.btnCloneAsNonGeneric.TabIndex = 67;
            this.btnCloneAsNonGeneric.Text = "As Non Generic";
            this.btnCloneAsNonGeneric.UseVisualStyleBackColor = true;
            this.btnCloneAsNonGeneric.Click += new System.EventHandler(this.btnCloneAsNonGeneric_Click);
            // 
            // chxAddToGridCoordinate
            // 
            this.chxAddToGridCoordinate.AutoSize = true;
            this.chxAddToGridCoordinate.Location = new System.Drawing.Point(10, 19);
            this.chxAddToGridCoordinate.Name = "chxAddToGridCoordinate";
            this.chxAddToGridCoordinate.Size = new System.Drawing.Size(140, 30);
            this.chxAddToGridCoordinate.TabIndex = 70;
            this.chxAddToGridCoordinate.Text = "Add To Grid Coordinate \r\nSystem List";
            this.chxAddToGridCoordinate.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Location = new System.Drawing.Point(561, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(166, 165);
            this.panel1.TabIndex = 71;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chxAddToGridCoordinate);
            this.groupBox3.Controls.Add(this.btnCloneAsNonGeneric);
            this.groupBox3.Controls.Add(this.btnCloneAsGeneric);
            this.groupBox3.Location = new System.Drawing.Point(5, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 117);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clone";
            // 
            // ctrlNewGridCoordinateSystem1
            // 
            this.ctrlNewGridCoordinateSystem1.Location = new System.Drawing.Point(7, 12);
            this.ctrlNewGridCoordinateSystem1.Name = "ctrlNewGridCoordinateSystem1";
            this.ctrlNewGridCoordinateSystem1.Size = new System.Drawing.Size(496, 315);
            this.ctrlNewGridCoordinateSystem1.TabIndex = 7;
            // 
            // frmNewGridCoordinateSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(743, 626);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ctrlNewGridCoordinateSystem1);
            this.Controls.Add(this.gbGridCoordinateSystem);
            this.Controls.Add(this.btnOK);
            this.Name = "frmNewGridCoordinateSystem";
            this.Text = "frmNewGridCoordinateSystem";
            this.gbGridCoordinateSystem.ResumeLayout(false);
            this.gbGridCoordinateSystem.PerformLayout();
            this.gpOgcCrsCode.ResumeLayout(false);
            this.gpOgcCrsCode.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbGridCoordinateSystem;
        private System.Windows.Forms.CheckBox chxIsMultyZoneGrid;
        private System.Windows.Forms.CheckBox chxIsUtm;
        private System.Windows.Forms.CheckBox chxIsGeographical;
        private Controls.CtrlSMcBox boxLegalValuesForGridCoordinates;
        private Controls.CtrlSMcBox boxLegalValuesForGeographicCoordinates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chxIsEqual;
        public System.Windows.Forms.ListBox lstExistingGridCoordSys;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSetLegalValuesForGridCoordinates;
        private System.Windows.Forms.Button btnSetLegalValuesForGeographicCoordinates;
        private System.Windows.Forms.Button btnIsGeographicLocationLegal;
        private System.Windows.Forms.Button btnIsLocationLegal;
        private System.Windows.Forms.Label label8;
        private Controls.Ctrl3DVector ctrlLocation;
        private System.Windows.Forms.Button btnGetDefaultZoneFromGeographicLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chxIsLocationLegal;
        private System.Windows.Forms.CheckBox chxIsGeographicLocationLegal;
        private Controls.NumericTextBox ntbZone;
        private System.Windows.Forms.Label label9;
        private Controls.CtrlSamplePoint ctrWorldPt;
        private CtrlNewGridCoordinateSystem ctrlNewGridCoordinateSystem1;
        private System.Windows.Forms.Button btnCloneAsGeneric;
        private System.Windows.Forms.Button btnCloneAsNonGeneric;
        private System.Windows.Forms.CheckBox chxAddToGridCoordinate;
        private System.Windows.Forms.TextBox txtOgcCrsCode;
        private System.Windows.Forms.CheckBox chxIsExist;
        private System.Windows.Forms.GroupBox gpOgcCrsCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}