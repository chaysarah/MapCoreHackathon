namespace MCTester.General_Forms
{
    partial class CtrlNewGridCoordinateSystem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbGeneric = new System.Windows.Forms.GroupBox();
            this.btnGetInitString = new System.Windows.Forms.Button();
            this.txGenericInitString = new System.Windows.Forms.TextBox();
            this.rbGenericByInitString = new System.Windows.Forms.RadioButton();
            this.txGenericArgs = new System.Windows.Forms.TextBox();
            this.txGenericSrid = new System.Windows.Forms.TextBox();
            this.rbGenericByArgs = new System.Windows.Forms.RadioButton();
            this.rbGenericBySrid = new System.Windows.Forms.RadioButton();
            this.cmbGridCoordSysType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbUserDefined = new System.Windows.Forms.GroupBox();
            this.ntxZoneWidth = new MCTester.Controls.NumericTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ntxCentralMeridian = new MCTester.Controls.NumericTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.ntxScaleFactor = new MCTester.Controls.NumericTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.ntxLatitudeOfGridOrigin = new MCTester.Controls.NumericTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.ntxFalseNorthing = new MCTester.Controls.NumericTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.ntxFalseEasting = new MCTester.Controls.NumericTextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.cmbDatum = new System.Windows.Forms.ComboBox();
            this.lblDatum = new System.Windows.Forms.Label();
            this.ntxZone = new MCTester.Controls.NumericTextBox();
            this.cgbDatumParams = new MCTester.Controls.CheckGroupBox();
            this.ntx_S = new MCTester.Controls.NumericTextBox();
            this.ntx_Rz = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_S = new System.Windows.Forms.Label();
            this.lblGeocentric_DX = new System.Windows.Forms.Label();
            this.ntx_DX = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_Rz = new System.Windows.Forms.Label();
            this.ntx_Ry = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_Ry = new System.Windows.Forms.Label();
            this.ntx_Rx = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_Rx = new System.Windows.Forms.Label();
            this.ntx_DZ = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_DZ = new System.Windows.Forms.Label();
            this.ntx_DY = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_DY = new System.Windows.Forms.Label();
            this.ntx_F = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_F = new System.Windows.Forms.Label();
            this.ntx_A = new MCTester.Controls.NumericTextBox();
            this.lblGeocentric_A = new System.Windows.Forms.Label();
            this.gbGeneric.SuspendLayout();
            this.gbUserDefined.SuspendLayout();
            this.cgbDatumParams.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbGeneric
            // 
            this.gbGeneric.Controls.Add(this.btnGetInitString);
            this.gbGeneric.Controls.Add(this.txGenericInitString);
            this.gbGeneric.Controls.Add(this.rbGenericByInitString);
            this.gbGeneric.Controls.Add(this.txGenericArgs);
            this.gbGeneric.Controls.Add(this.txGenericSrid);
            this.gbGeneric.Controls.Add(this.rbGenericByArgs);
            this.gbGeneric.Controls.Add(this.rbGenericBySrid);
            this.gbGeneric.Location = new System.Drawing.Point(3, 90);
            this.gbGeneric.Name = "gbGeneric";
            this.gbGeneric.Size = new System.Drawing.Size(480, 152);
            this.gbGeneric.TabIndex = 71;
            this.gbGeneric.TabStop = false;
            this.gbGeneric.Text = "Generic Grid";
            this.gbGeneric.Visible = false;
            // 
            // btnGetInitString
            // 
            this.btnGetInitString.Location = new System.Drawing.Point(299, 24);
            this.btnGetInitString.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetInitString.Name = "btnGetInitString";
            this.btnGetInitString.Size = new System.Drawing.Size(75, 23);
            this.btnGetInitString.TabIndex = 6;
            this.btnGetInitString.Text = "&Get init string";
            this.btnGetInitString.UseVisualStyleBackColor = true;
            this.btnGetInitString.Click += new System.EventHandler(this.btnGetInitString_Click);
            // 
            // txGenericInitString
            // 
            this.txGenericInitString.Location = new System.Drawing.Point(184, 50);
            this.txGenericInitString.Margin = new System.Windows.Forms.Padding(2);
            this.txGenericInitString.Multiline = true;
            this.txGenericInitString.Name = "txGenericInitString";
            this.txGenericInitString.Size = new System.Drawing.Size(291, 61);
            this.txGenericInitString.TabIndex = 4;
            // 
            // rbGenericByInitString
            // 
            this.rbGenericByInitString.AutoSize = true;
            this.rbGenericByInitString.Location = new System.Drawing.Point(6, 80);
            this.rbGenericByInitString.Margin = new System.Windows.Forms.Padding(2);
            this.rbGenericByInitString.Name = "rbGenericByInitString";
            this.rbGenericByInitString.Size = new System.Drawing.Size(89, 17);
            this.rbGenericByInitString.TabIndex = 1;
            this.rbGenericByInitString.TabStop = true;
            this.rbGenericByInitString.Text = "Use Init string";
            this.rbGenericByInitString.UseVisualStyleBackColor = true;
            this.rbGenericByInitString.CheckedChanged += new System.EventHandler(this.rbGenericByInitString_CheckedChanged);
            // 
            // txGenericArgs
            // 
            this.txGenericArgs.Enabled = false;
            this.txGenericArgs.Location = new System.Drawing.Point(184, 115);
            this.txGenericArgs.Margin = new System.Windows.Forms.Padding(2);
            this.txGenericArgs.Name = "txGenericArgs";
            this.txGenericArgs.Size = new System.Drawing.Size(291, 20);
            this.txGenericArgs.TabIndex = 5;
            // 
            // txGenericSrid
            // 
            this.txGenericSrid.Location = new System.Drawing.Point(184, 26);
            this.txGenericSrid.Margin = new System.Windows.Forms.Padding(2);
            this.txGenericSrid.Name = "txGenericSrid";
            this.txGenericSrid.Size = new System.Drawing.Size(107, 20);
            this.txGenericSrid.TabIndex = 3;
            // 
            // rbGenericByArgs
            // 
            this.rbGenericByArgs.AutoSize = true;
            this.rbGenericByArgs.Location = new System.Drawing.Point(8, 116);
            this.rbGenericByArgs.Margin = new System.Windows.Forms.Padding(2);
            this.rbGenericByArgs.Name = "rbGenericByArgs";
            this.rbGenericByArgs.Size = new System.Drawing.Size(173, 17);
            this.rbGenericByArgs.TabIndex = 2;
            this.rbGenericByArgs.TabStop = true;
            this.rbGenericByArgs.Text = "Use Args  (seperated by space)";
            this.rbGenericByArgs.UseVisualStyleBackColor = true;
            this.rbGenericByArgs.CheckedChanged += new System.EventHandler(this.rbGenericByArgs_CheckedChanged);
            // 
            // rbGenericBySrid
            // 
            this.rbGenericBySrid.AutoSize = true;
            this.rbGenericBySrid.Checked = true;
            this.rbGenericBySrid.Location = new System.Drawing.Point(8, 25);
            this.rbGenericBySrid.Margin = new System.Windows.Forms.Padding(2);
            this.rbGenericBySrid.Name = "rbGenericBySrid";
            this.rbGenericBySrid.Size = new System.Drawing.Size(73, 17);
            this.rbGenericBySrid.TabIndex = 0;
            this.rbGenericBySrid.TabStop = true;
            this.rbGenericBySrid.Text = "Use SRID";
            this.rbGenericBySrid.UseVisualStyleBackColor = true;
            this.rbGenericBySrid.CheckedChanged += new System.EventHandler(this.rbGenericBySrid_CheckedChanged);
            // 
            // cmbGridCoordSysType
            // 
            this.cmbGridCoordSysType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGridCoordSysType.FormattingEnabled = true;
            this.cmbGridCoordSysType.Location = new System.Drawing.Point(110, 4);
            this.cmbGridCoordSysType.Name = "cmbGridCoordSysType";
            this.cmbGridCoordSysType.Size = new System.Drawing.Size(227, 21);
            this.cmbGridCoordSysType.TabIndex = 69;
            this.cmbGridCoordSysType.SelectedIndexChanged += new System.EventHandler(this.cmbGridCoordSysType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "Coordinate System:";
            // 
            // gbUserDefined
            // 
            this.gbUserDefined.Controls.Add(this.ntxZoneWidth);
            this.gbUserDefined.Controls.Add(this.label5);
            this.gbUserDefined.Controls.Add(this.ntxCentralMeridian);
            this.gbUserDefined.Controls.Add(this.label18);
            this.gbUserDefined.Controls.Add(this.ntxScaleFactor);
            this.gbUserDefined.Controls.Add(this.label19);
            this.gbUserDefined.Controls.Add(this.ntxLatitudeOfGridOrigin);
            this.gbUserDefined.Controls.Add(this.label20);
            this.gbUserDefined.Controls.Add(this.ntxFalseNorthing);
            this.gbUserDefined.Controls.Add(this.label21);
            this.gbUserDefined.Controls.Add(this.ntxFalseEasting);
            this.gbUserDefined.Controls.Add(this.label22);
            this.gbUserDefined.Location = new System.Drawing.Point(3, 200);
            this.gbUserDefined.Name = "gbUserDefined";
            this.gbUserDefined.Size = new System.Drawing.Size(480, 106);
            this.gbUserDefined.TabIndex = 64;
            this.gbUserDefined.TabStop = false;
            this.gbUserDefined.Text = "User Defined";
            this.gbUserDefined.Visible = false;
            // 
            // ntxZoneWidth
            // 
            this.ntxZoneWidth.Location = new System.Drawing.Point(357, 76);
            this.ntxZoneWidth.Name = "ntxZoneWidth";
            this.ntxZoneWidth.Size = new System.Drawing.Size(107, 20);
            this.ntxZoneWidth.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Central Meridian:";
            // 
            // ntxCentralMeridian
            // 
            this.ntxCentralMeridian.Location = new System.Drawing.Point(105, 26);
            this.ntxCentralMeridian.Name = "ntxCentralMeridian";
            this.ntxCentralMeridian.Size = new System.Drawing.Size(107, 20);
            this.ntxCentralMeridian.TabIndex = 52;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(237, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 13);
            this.label18.TabIndex = 61;
            this.label18.Text = "Zone Width:";
            // 
            // ntxScaleFactor
            // 
            this.ntxScaleFactor.Location = new System.Drawing.Point(357, 50);
            this.ntxScaleFactor.Name = "ntxScaleFactor";
            this.ntxScaleFactor.Size = new System.Drawing.Size(107, 20);
            this.ntxScaleFactor.TabIndex = 60;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(237, 53);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 13);
            this.label19.TabIndex = 59;
            this.label19.Text = "Scale Factor:";
            // 
            // ntxLatitudeOfGridOrigin
            // 
            this.ntxLatitudeOfGridOrigin.Location = new System.Drawing.Point(357, 24);
            this.ntxLatitudeOfGridOrigin.Name = "ntxLatitudeOfGridOrigin";
            this.ntxLatitudeOfGridOrigin.Size = new System.Drawing.Size(107, 20);
            this.ntxLatitudeOfGridOrigin.TabIndex = 58;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(237, 27);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(114, 13);
            this.label20.TabIndex = 57;
            this.label20.Text = "Latitude Of Grid Origin:";
            // 
            // ntxFalseNorthing
            // 
            this.ntxFalseNorthing.Location = new System.Drawing.Point(105, 78);
            this.ntxFalseNorthing.Name = "ntxFalseNorthing";
            this.ntxFalseNorthing.Size = new System.Drawing.Size(107, 20);
            this.ntxFalseNorthing.TabIndex = 56;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 81);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(78, 13);
            this.label21.TabIndex = 55;
            this.label21.Text = "False Northing:";
            // 
            // ntxFalseEasting
            // 
            this.ntxFalseEasting.Location = new System.Drawing.Point(105, 52);
            this.ntxFalseEasting.Name = "ntxFalseEasting";
            this.ntxFalseEasting.Size = new System.Drawing.Size(107, 20);
            this.ntxFalseEasting.TabIndex = 54;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 55);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 13);
            this.label22.TabIndex = 53;
            this.label22.Text = "False Easting:";
            // 
            // lblZone
            // 
            this.lblZone.AutoSize = true;
            this.lblZone.Location = new System.Drawing.Point(9, 68);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(35, 13);
            this.lblZone.TabIndex = 49;
            this.lblZone.Text = "Zone:";
            // 
            // cmbDatum
            // 
            this.cmbDatum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatum.FormattingEnabled = true;
            this.cmbDatum.Location = new System.Drawing.Point(109, 34);
            this.cmbDatum.Name = "cmbDatum";
            this.cmbDatum.Size = new System.Drawing.Size(228, 21);
            this.cmbDatum.TabIndex = 6;
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.Location = new System.Drawing.Point(9, 42);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(41, 13);
            this.lblDatum.TabIndex = 5;
            this.lblDatum.Text = "Datum:";
            // 
            // ntxZone
            // 
            this.ntxZone.Location = new System.Drawing.Point(109, 65);
            this.ntxZone.Name = "ntxZone";
            this.ntxZone.Size = new System.Drawing.Size(88, 20);
            this.ntxZone.TabIndex = 50;
            // 
            // cgbDatumParams
            // 
            this.cgbDatumParams.Controls.Add(this.ntx_S);
            this.cgbDatumParams.Controls.Add(this.ntx_Rz);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_S);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_DX);
            this.cgbDatumParams.Controls.Add(this.ntx_DX);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_Rz);
            this.cgbDatumParams.Controls.Add(this.ntx_Ry);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_Ry);
            this.cgbDatumParams.Controls.Add(this.ntx_Rx);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_Rx);
            this.cgbDatumParams.Controls.Add(this.ntx_DZ);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_DZ);
            this.cgbDatumParams.Controls.Add(this.ntx_DY);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_DY);
            this.cgbDatumParams.Controls.Add(this.ntx_F);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_F);
            this.cgbDatumParams.Controls.Add(this.ntx_A);
            this.cgbDatumParams.Controls.Add(this.lblGeocentric_A);
            this.cgbDatumParams.Location = new System.Drawing.Point(3, 91);
            this.cgbDatumParams.Name = "cgbDatumParams";
            this.cgbDatumParams.Size = new System.Drawing.Size(480, 104);
            this.cgbDatumParams.TabIndex = 73;
            this.cgbDatumParams.TabStop = false;
            this.cgbDatumParams.Text = "Datum Params";
            // 
            // ntx_S
            // 
            this.ntx_S.Location = new System.Drawing.Point(352, 21);
            this.ntx_S.Name = "ntx_S";
            this.ntx_S.Size = new System.Drawing.Size(105, 20);
            this.ntx_S.TabIndex = 66;
            // 
            // ntx_Rz
            // 
            this.ntx_Rz.Location = new System.Drawing.Point(352, 73);
            this.ntx_Rz.Name = "ntx_Rz";
            this.ntx_Rz.Size = new System.Drawing.Size(105, 20);
            this.ntx_Rz.TabIndex = 64;
            // 
            // lblGeocentric_S
            // 
            this.lblGeocentric_S.AutoSize = true;
            this.lblGeocentric_S.Location = new System.Drawing.Point(321, 24);
            this.lblGeocentric_S.Name = "lblGeocentric_S";
            this.lblGeocentric_S.Size = new System.Drawing.Size(17, 13);
            this.lblGeocentric_S.TabIndex = 65;
            this.lblGeocentric_S.Text = "S:";
            // 
            // lblGeocentric_DX
            // 
            this.lblGeocentric_DX.AutoSize = true;
            this.lblGeocentric_DX.Location = new System.Drawing.Point(8, 50);
            this.lblGeocentric_DX.Name = "lblGeocentric_DX";
            this.lblGeocentric_DX.Size = new System.Drawing.Size(25, 13);
            this.lblGeocentric_DX.TabIndex = 53;
            this.lblGeocentric_DX.Text = "DX:";
            // 
            // ntx_DX
            // 
            this.ntx_DX.Location = new System.Drawing.Point(36, 45);
            this.ntx_DX.Name = "ntx_DX";
            this.ntx_DX.Size = new System.Drawing.Size(107, 20);
            this.ntx_DX.TabIndex = 54;
            // 
            // lblGeocentric_Rz
            // 
            this.lblGeocentric_Rz.AutoSize = true;
            this.lblGeocentric_Rz.Location = new System.Drawing.Point(321, 76);
            this.lblGeocentric_Rz.Name = "lblGeocentric_Rz";
            this.lblGeocentric_Rz.Size = new System.Drawing.Size(23, 13);
            this.lblGeocentric_Rz.TabIndex = 63;
            this.lblGeocentric_Rz.Text = "Rz:";
            // 
            // ntx_Ry
            // 
            this.ntx_Ry.Location = new System.Drawing.Point(191, 74);
            this.ntx_Ry.Name = "ntx_Ry";
            this.ntx_Ry.Size = new System.Drawing.Size(107, 20);
            this.ntx_Ry.TabIndex = 62;
            // 
            // lblGeocentric_Ry
            // 
            this.lblGeocentric_Ry.AutoSize = true;
            this.lblGeocentric_Ry.Location = new System.Drawing.Point(163, 77);
            this.lblGeocentric_Ry.Name = "lblGeocentric_Ry";
            this.lblGeocentric_Ry.Size = new System.Drawing.Size(23, 13);
            this.lblGeocentric_Ry.TabIndex = 61;
            this.lblGeocentric_Ry.Text = "Ry:";
            // 
            // ntx_Rx
            // 
            this.ntx_Rx.Location = new System.Drawing.Point(36, 73);
            this.ntx_Rx.Name = "ntx_Rx";
            this.ntx_Rx.Size = new System.Drawing.Size(107, 20);
            this.ntx_Rx.TabIndex = 60;
            // 
            // lblGeocentric_Rx
            // 
            this.lblGeocentric_Rx.AutoSize = true;
            this.lblGeocentric_Rx.Location = new System.Drawing.Point(8, 75);
            this.lblGeocentric_Rx.Name = "lblGeocentric_Rx";
            this.lblGeocentric_Rx.Size = new System.Drawing.Size(23, 13);
            this.lblGeocentric_Rx.TabIndex = 59;
            this.lblGeocentric_Rx.Text = "Rx:";
            // 
            // ntx_DZ
            // 
            this.ntx_DZ.Location = new System.Drawing.Point(352, 47);
            this.ntx_DZ.Name = "ntx_DZ";
            this.ntx_DZ.Size = new System.Drawing.Size(105, 20);
            this.ntx_DZ.TabIndex = 58;
            // 
            // lblGeocentric_DZ
            // 
            this.lblGeocentric_DZ.AutoSize = true;
            this.lblGeocentric_DZ.Location = new System.Drawing.Point(320, 50);
            this.lblGeocentric_DZ.Name = "lblGeocentric_DZ";
            this.lblGeocentric_DZ.Size = new System.Drawing.Size(25, 13);
            this.lblGeocentric_DZ.TabIndex = 57;
            this.lblGeocentric_DZ.Text = "DZ:";
            // 
            // ntx_DY
            // 
            this.ntx_DY.Location = new System.Drawing.Point(191, 49);
            this.ntx_DY.Name = "ntx_DY";
            this.ntx_DY.Size = new System.Drawing.Size(107, 20);
            this.ntx_DY.TabIndex = 56;
            // 
            // lblGeocentric_DY
            // 
            this.lblGeocentric_DY.AutoSize = true;
            this.lblGeocentric_DY.Location = new System.Drawing.Point(163, 52);
            this.lblGeocentric_DY.Name = "lblGeocentric_DY";
            this.lblGeocentric_DY.Size = new System.Drawing.Size(25, 13);
            this.lblGeocentric_DY.TabIndex = 55;
            this.lblGeocentric_DY.Text = "DY:";
            // 
            // ntx_F
            // 
            this.ntx_F.Location = new System.Drawing.Point(191, 21);
            this.ntx_F.Name = "ntx_F";
            this.ntx_F.Size = new System.Drawing.Size(107, 20);
            this.ntx_F.TabIndex = 52;
            // 
            // lblGeocentric_F
            // 
            this.lblGeocentric_F.AutoSize = true;
            this.lblGeocentric_F.Location = new System.Drawing.Point(163, 26);
            this.lblGeocentric_F.Name = "lblGeocentric_F";
            this.lblGeocentric_F.Size = new System.Drawing.Size(16, 13);
            this.lblGeocentric_F.TabIndex = 51;
            this.lblGeocentric_F.Text = "F:";
            // 
            // ntx_A
            // 
            this.ntx_A.Location = new System.Drawing.Point(36, 19);
            this.ntx_A.Name = "ntx_A";
            this.ntx_A.Size = new System.Drawing.Size(107, 20);
            this.ntx_A.TabIndex = 50;
            // 
            // lblGeocentric_A
            // 
            this.lblGeocentric_A.AutoSize = true;
            this.lblGeocentric_A.Location = new System.Drawing.Point(8, 24);
            this.lblGeocentric_A.Name = "lblGeocentric_A";
            this.lblGeocentric_A.Size = new System.Drawing.Size(17, 13);
            this.lblGeocentric_A.TabIndex = 49;
            this.lblGeocentric_A.Text = "A:";
            // 
            // CtrlNewGridCoordinateSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cgbDatumParams);
            this.Controls.Add(this.gbGeneric);
            this.Controls.Add(this.cmbGridCoordSysType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.gbUserDefined);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.cmbDatum);
            this.Controls.Add(this.ntxZone);
            this.Controls.Add(this.lblZone);
            this.Name = "CtrlNewGridCoordinateSystem";
            this.Size = new System.Drawing.Size(495, 308);
            this.gbGeneric.ResumeLayout(false);
            this.gbGeneric.PerformLayout();
            this.gbUserDefined.ResumeLayout(false);
            this.gbUserDefined.PerformLayout();
            this.cgbDatumParams.ResumeLayout(false);
            this.cgbDatumParams.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGeneric;
        private System.Windows.Forms.Button btnGetInitString;
        private System.Windows.Forms.TextBox txGenericInitString;
        private System.Windows.Forms.RadioButton rbGenericByInitString;
        private System.Windows.Forms.TextBox txGenericArgs;
        private System.Windows.Forms.TextBox txGenericSrid;
        private System.Windows.Forms.RadioButton rbGenericByArgs;
        private System.Windows.Forms.RadioButton rbGenericBySrid;
        private System.Windows.Forms.ComboBox cmbGridCoordSysType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbUserDefined;
        private Controls.NumericTextBox ntxZoneWidth;
        private System.Windows.Forms.Label label5;
        private Controls.NumericTextBox ntxCentralMeridian;
        private System.Windows.Forms.Label label18;
        private Controls.NumericTextBox ntxScaleFactor;
        private System.Windows.Forms.Label label19;
        private Controls.NumericTextBox ntxLatitudeOfGridOrigin;
        private System.Windows.Forms.Label label20;
        private Controls.NumericTextBox ntxFalseNorthing;
        private System.Windows.Forms.Label label21;
        private Controls.NumericTextBox ntxFalseEasting;
        private System.Windows.Forms.Label label22;
        private Controls.NumericTextBox ntxZone;
        private System.Windows.Forms.Label lblZone;
        private System.Windows.Forms.ComboBox cmbDatum;
        private System.Windows.Forms.Label lblDatum;
        private Controls.CheckGroupBox cgbDatumParams;
        private Controls.NumericTextBox ntx_S;
        private Controls.NumericTextBox ntx_Rz;
        private System.Windows.Forms.Label lblGeocentric_S;
        private System.Windows.Forms.Label lblGeocentric_DX;
        private Controls.NumericTextBox ntx_DX;
        private System.Windows.Forms.Label lblGeocentric_Rz;
        private Controls.NumericTextBox ntx_Ry;
        private System.Windows.Forms.Label lblGeocentric_Ry;
        private Controls.NumericTextBox ntx_Rx;
        private System.Windows.Forms.Label lblGeocentric_Rx;
        private Controls.NumericTextBox ntx_DZ;
        private System.Windows.Forms.Label lblGeocentric_DZ;
        private Controls.NumericTextBox ntx_DY;
        private System.Windows.Forms.Label lblGeocentric_DY;
        private Controls.NumericTextBox ntx_F;
        private System.Windows.Forms.Label lblGeocentric_F;
        private Controls.NumericTextBox ntx_A;
        private System.Windows.Forms.Label lblGeocentric_A;
    }
}
