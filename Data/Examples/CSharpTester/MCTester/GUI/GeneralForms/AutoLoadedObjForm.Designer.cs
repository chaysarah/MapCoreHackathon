namespace MCTester.GUI.Forms
{
    partial class AutoLoadedObjForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoLoadedObjForm));
            this.dgvLoadedObj = new System.Windows.Forms.DataGridView();
            this.AutoLodededObjOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chxIsClonedSchemes = new System.Windows.Forms.CheckBox();
            this.rbSelectRect = new System.Windows.Forms.RadioButton();
            this.gbCenterPointAndRadius = new System.Windows.Forms.GroupBox();
            this.ntxRadiusY = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrl3DFVectorCenterPoint = new MCTester.Controls.Ctrl3DFVector();
            this.ctrlCenterPoint = new MCTester.Controls.CtrlSamplePoint();
            this.ntxRadiusX = new MCTester.Controls.NumericTextBox();
            this.rbVisibleArea = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteObjects = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ctrl2DVectorOffset = new MCTester.Controls.Ctrl2DVector();
            this.rbFixedOffset = new System.Windows.Forms.RadioButton();
            this.rbRandom = new System.Windows.Forms.RadioButton();
            this.grpIterationPrm = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chxRandom = new System.Windows.Forms.CheckBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.ntxTotalIteration = new MCTester.Controls.NumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ntxPropertyId = new MCTester.Controls.NumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStartIterations = new System.Windows.Forms.Button();
            this.ntxAngle = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ntxNoIterationNotCount = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrl2DVectorMovement = new MCTester.Controls.Ctrl2DVector();
            this.ObjectName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadedObj)).BeginInit();
            this.gbCenterPointAndRadius.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpIterationPrm.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLoadedObj
            // 
            this.dgvLoadedObj.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoadedObj.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoadedObj.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ObjectName,
            this.Amount});
            this.dgvLoadedObj.Location = new System.Drawing.Point(4, 15);
            this.dgvLoadedObj.Margin = new System.Windows.Forms.Padding(4);
            this.dgvLoadedObj.Name = "dgvLoadedObj";
            this.dgvLoadedObj.Size = new System.Drawing.Size(438, 58);
            this.dgvLoadedObj.TabIndex = 1;
            this.dgvLoadedObj.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLoadedObj_CellClick);
            // 
            // AutoLodededObjOK
            // 
            this.AutoLodededObjOK.Location = new System.Drawing.Point(180, 321);
            this.AutoLodededObjOK.Margin = new System.Windows.Forms.Padding(4);
            this.AutoLodededObjOK.Name = "AutoLodededObjOK";
            this.AutoLodededObjOK.Size = new System.Drawing.Size(123, 32);
            this.AutoLodededObjOK.TabIndex = 2;
            this.AutoLodededObjOK.Text = "Create Objects";
            this.AutoLodededObjOK.UseVisualStyleBackColor = true;
            this.AutoLodededObjOK.Click += new System.EventHandler(this.AutoLodededObjOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Radius X";
            // 
            // chxIsClonedSchemes
            // 
            this.chxIsClonedSchemes.AutoSize = true;
            this.chxIsClonedSchemes.Location = new System.Drawing.Point(12, 25);
            this.chxIsClonedSchemes.Name = "chxIsClonedSchemes";
            this.chxIsClonedSchemes.Size = new System.Drawing.Size(143, 21);
            this.chxIsClonedSchemes.TabIndex = 7;
            this.chxIsClonedSchemes.Text = "Is Cloned Scheme";
            this.chxIsClonedSchemes.UseVisualStyleBackColor = true;
            // 
            // rbSelectRect
            // 
            this.rbSelectRect.AutoSize = true;
            this.rbSelectRect.Location = new System.Drawing.Point(6, 55);
            this.rbSelectRect.Name = "rbSelectRect";
            this.rbSelectRect.Size = new System.Drawing.Size(317, 21);
            this.rbSelectRect.TabIndex = 11;
            this.rbSelectRect.Text = "Choose Location By Center Point and Radius:";
            this.rbSelectRect.UseVisualStyleBackColor = true;
            this.rbSelectRect.CheckedChanged += new System.EventHandler(this.rbSelectRadius_CheckedChanged);
            // 
            // gbCenterPointAndRadius
            // 
            this.gbCenterPointAndRadius.Controls.Add(this.ntxRadiusY);
            this.gbCenterPointAndRadius.Controls.Add(this.label6);
            this.gbCenterPointAndRadius.Controls.Add(this.ctrl3DFVectorCenterPoint);
            this.gbCenterPointAndRadius.Controls.Add(this.ctrlCenterPoint);
            this.gbCenterPointAndRadius.Controls.Add(this.ntxRadiusX);
            this.gbCenterPointAndRadius.Controls.Add(this.label1);
            this.gbCenterPointAndRadius.Enabled = false;
            this.gbCenterPointAndRadius.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbCenterPointAndRadius.Location = new System.Drawing.Point(6, 80);
            this.gbCenterPointAndRadius.Name = "gbCenterPointAndRadius";
            this.gbCenterPointAndRadius.Size = new System.Drawing.Size(395, 84);
            this.gbCenterPointAndRadius.TabIndex = 12;
            this.gbCenterPointAndRadius.TabStop = false;
            // 
            // ntxRadiusY
            // 
            this.ntxRadiusY.Location = new System.Drawing.Point(238, 46);
            this.ntxRadiusY.Margin = new System.Windows.Forms.Padding(4);
            this.ntxRadiusY.Name = "ntxRadiusY";
            this.ntxRadiusY.Size = new System.Drawing.Size(68, 22);
            this.ntxRadiusY.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Radius Y";
            // 
            // ctrl3DFVectorCenterPoint
            // 
            this.ctrl3DFVectorCenterPoint.Location = new System.Drawing.Point(7, 13);
            this.ctrl3DFVectorCenterPoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl3DFVectorCenterPoint.Name = "ctrl3DFVectorCenterPoint";
            this.ctrl3DFVectorCenterPoint.Size = new System.Drawing.Size(309, 32);
            this.ctrl3DFVectorCenterPoint.TabIndex = 10;
            this.ctrl3DFVectorCenterPoint.X = 0F;
            this.ctrl3DFVectorCenterPoint.Y = 0F;
            this.ctrl3DFVectorCenterPoint.Z = 0F;
            // 
            // ctrlCenterPoint
            // 
            this.ctrlCenterPoint._DgvControlName = null;
            this.ctrlCenterPoint._PointInOverlayManagerCoordSys = true;
            this.ctrlCenterPoint._PointZValue = 1.7976931348623157E+308D;
            this.ctrlCenterPoint._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlCenterPoint._SampleOnePoint = true;
            this.ctrlCenterPoint._UserControlName = "ctrl3DFVectorCenterPoint";
            this.ctrlCenterPoint.Location = new System.Drawing.Point(323, 14);
            this.ctrlCenterPoint.Name = "ctrlCenterPoint";
            this.ctrlCenterPoint.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlCenterPoint.Size = new System.Drawing.Size(46, 31);
            this.ctrlCenterPoint.TabIndex = 9;
            this.ctrlCenterPoint.Text = "...";
            this.ctrlCenterPoint.UseVisualStyleBackColor = true;
            // 
            // ntxRadiusX
            // 
            this.ntxRadiusX.Location = new System.Drawing.Point(78, 46);
            this.ntxRadiusX.Margin = new System.Windows.Forms.Padding(4);
            this.ntxRadiusX.Name = "ntxRadiusX";
            this.ntxRadiusX.Size = new System.Drawing.Size(65, 22);
            this.ntxRadiusX.TabIndex = 3;
            // 
            // rbVisibleArea
            // 
            this.rbVisibleArea.AutoSize = true;
            this.rbVisibleArea.Checked = true;
            this.rbVisibleArea.Location = new System.Drawing.Point(5, 26);
            this.rbVisibleArea.Name = "rbVisibleArea";
            this.rbVisibleArea.Size = new System.Drawing.Size(104, 21);
            this.rbVisibleArea.TabIndex = 13;
            this.rbVisibleArea.TabStop = true;
            this.rbVisibleArea.Text = "Visible Area";
            this.rbVisibleArea.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbSelectRect);
            this.groupBox2.Controls.Add(this.rbVisibleArea);
            this.groupBox2.Controls.Add(this.gbCenterPointAndRadius);
            this.groupBox2.Location = new System.Drawing.Point(6, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 173);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Area";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteObjects);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.chxIsClonedSchemes);
            this.groupBox1.Controls.Add(this.AutoLodededObjOK);
            this.groupBox1.Location = new System.Drawing.Point(4, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 361);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Objects Parameters";
            // 
            // btnDeleteObjects
            // 
            this.btnDeleteObjects.Location = new System.Drawing.Point(311, 321);
            this.btnDeleteObjects.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteObjects.Name = "btnDeleteObjects";
            this.btnDeleteObjects.Size = new System.Drawing.Size(123, 32);
            this.btnDeleteObjects.TabIndex = 16;
            this.btnDeleteObjects.Text = "Remove Objects";
            this.btnDeleteObjects.UseVisualStyleBackColor = true;
            this.btnDeleteObjects.Click += new System.EventHandler(this.btnDeleteObjects_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ctrl2DVectorOffset);
            this.groupBox3.Controls.Add(this.rbFixedOffset);
            this.groupBox3.Controls.Add(this.rbRandom);
            this.groupBox3.Location = new System.Drawing.Point(6, 231);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(427, 80);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Points";
            // 
            // ctrl2DVectorOffset
            // 
            this.ctrl2DVectorOffset.Location = new System.Drawing.Point(217, 44);
            this.ctrl2DVectorOffset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl2DVectorOffset.Name = "ctrl2DVectorOffset";
            this.ctrl2DVectorOffset.Size = new System.Drawing.Size(205, 32);
            this.ctrl2DVectorOffset.TabIndex = 2;
            this.ctrl2DVectorOffset.X = 1000D;
            this.ctrl2DVectorOffset.Y = 1000D;
            // 
            // rbFixedOffset
            // 
            this.rbFixedOffset.AutoSize = true;
            this.rbFixedOffset.Checked = true;
            this.rbFixedOffset.Location = new System.Drawing.Point(13, 49);
            this.rbFixedOffset.Name = "rbFixedOffset";
            this.rbFixedOffset.Size = new System.Drawing.Size(197, 21);
            this.rbFixedOffset.TabIndex = 1;
            this.rbFixedOffset.TabStop = true;
            this.rbFixedOffset.Text = "Fixed Offset From Top Left";
            this.rbFixedOffset.UseVisualStyleBackColor = true;
            this.rbFixedOffset.CheckedChanged += new System.EventHandler(this.rbFixedOffset_CheckedChanged);
            // 
            // rbRandom
            // 
            this.rbRandom.AutoSize = true;
            this.rbRandom.Location = new System.Drawing.Point(13, 22);
            this.rbRandom.Name = "rbRandom";
            this.rbRandom.Size = new System.Drawing.Size(82, 21);
            this.rbRandom.TabIndex = 0;
            this.rbRandom.Text = "Random";
            this.rbRandom.UseVisualStyleBackColor = true;
            // 
            // grpIterationPrm
            // 
            this.grpIterationPrm.Controls.Add(this.label5);
            this.grpIterationPrm.Controls.Add(this.chxRandom);
            this.grpIterationPrm.Controls.Add(this.tbOutput);
            this.grpIterationPrm.Controls.Add(this.ntxTotalIteration);
            this.grpIterationPrm.Controls.Add(this.label8);
            this.grpIterationPrm.Controls.Add(this.ntxPropertyId);
            this.grpIterationPrm.Controls.Add(this.label7);
            this.grpIterationPrm.Controls.Add(this.btnStop);
            this.grpIterationPrm.Controls.Add(this.btnStartIterations);
            this.grpIterationPrm.Controls.Add(this.ntxAngle);
            this.grpIterationPrm.Controls.Add(this.label4);
            this.grpIterationPrm.Controls.Add(this.ntxNoIterationNotCount);
            this.grpIterationPrm.Controls.Add(this.label3);
            this.grpIterationPrm.Controls.Add(this.label2);
            this.grpIterationPrm.Controls.Add(this.ctrl2DVectorMovement);
            this.grpIterationPrm.Enabled = false;
            this.grpIterationPrm.Location = new System.Drawing.Point(5, 447);
            this.grpIterationPrm.Name = "grpIterationPrm";
            this.grpIterationPrm.Size = new System.Drawing.Size(437, 298);
            this.grpIterationPrm.TabIndex = 16;
            this.grpIterationPrm.TabStop = false;
            this.grpIterationPrm.Text = "Iteration Parameters";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Output";
            // 
            // chxRandom
            // 
            this.chxRandom.AutoSize = true;
            this.chxRandom.Location = new System.Drawing.Point(314, 27);
            this.chxRandom.Name = "chxRandom";
            this.chxRandom.Size = new System.Drawing.Size(83, 21);
            this.chxRandom.TabIndex = 16;
            this.chxRandom.Text = "Random";
            this.chxRandom.UseVisualStyleBackColor = true;
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(90, 161);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutput.Size = new System.Drawing.Size(342, 130);
            this.tbOutput.TabIndex = 17;
            // 
            // ntxTotalIteration
            // 
            this.ntxTotalIteration.Location = new System.Drawing.Point(217, 120);
            this.ntxTotalIteration.Margin = new System.Windows.Forms.Padding(4);
            this.ntxTotalIteration.Name = "ntxTotalIteration";
            this.ntxTotalIteration.Size = new System.Drawing.Size(70, 22);
            this.ntxTotalIteration.TabIndex = 14;
            this.ntxTotalIteration.Text = "100";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "Total Iteration";
            // 
            // ntxPropertyId
            // 
            this.ntxPropertyId.Location = new System.Drawing.Point(116, 53);
            this.ntxPropertyId.Margin = new System.Windows.Forms.Padding(4);
            this.ntxPropertyId.Name = "ntxPropertyId";
            this.ntxPropertyId.Size = new System.Drawing.Size(69, 22);
            this.ntxPropertyId.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Property ID ";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(314, 113);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(118, 32);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStartIterations
            // 
            this.btnStartIterations.Location = new System.Drawing.Point(314, 73);
            this.btnStartIterations.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartIterations.Name = "btnStartIterations";
            this.btnStartIterations.Size = new System.Drawing.Size(119, 32);
            this.btnStartIterations.TabIndex = 9;
            this.btnStartIterations.Text = "Start";
            this.btnStartIterations.UseVisualStyleBackColor = true;
            this.btnStartIterations.Click += new System.EventHandler(this.btnStartIterations_Click);
            // 
            // ntxAngle
            // 
            this.ntxAngle.Location = new System.Drawing.Point(243, 55);
            this.ntxAngle.Margin = new System.Windows.Forms.Padding(4);
            this.ntxAngle.Name = "ntxAngle";
            this.ntxAngle.Size = new System.Drawing.Size(43, 22);
            this.ntxAngle.TabIndex = 8;
            this.ntxAngle.TextChanged += new System.EventHandler(this.ntxAngle_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(192, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Angle";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // ntxNoIterationNotCount
            // 
            this.ntxNoIterationNotCount.Location = new System.Drawing.Point(217, 90);
            this.ntxNoIterationNotCount.Margin = new System.Windows.Forms.Padding(4);
            this.ntxNoIterationNotCount.Name = "ntxNoIterationNotCount";
            this.ntxNoIterationNotCount.Size = new System.Drawing.Size(70, 22);
            this.ntxNoIterationNotCount.TabIndex = 6;
            this.ntxNoIterationNotCount.Text = "30";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "No. Iteration Not Count Times";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Movement";
            // 
            // ctrl2DVectorMovement
            // 
            this.ctrl2DVectorMovement.Location = new System.Drawing.Point(88, 19);
            this.ctrl2DVectorMovement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl2DVectorMovement.Name = "ctrl2DVectorMovement";
            this.ctrl2DVectorMovement.Size = new System.Drawing.Size(205, 32);
            this.ctrl2DVectorMovement.TabIndex = 3;
            this.ctrl2DVectorMovement.X = 100D;
            this.ctrl2DVectorMovement.Y = 100D;
            // 
            // ObjectName
            // 
            this.ObjectName.HeaderText = "Select Object File(.m)";
            this.ObjectName.MinimumWidth = 50;
            this.ObjectName.Name = "ObjectName";
            this.ObjectName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Amount
            // 
            this.Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Amount.HeaderText = "Amount";
            this.Amount.MinimumWidth = 50;
            this.Amount.Name = "Amount";
            this.Amount.Width = 85;
            // 
            // AutoLoadedObjForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(572, 750);
            this.Controls.Add(this.grpIterationPrm);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvLoadedObj);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AutoLoadedObjForm";
            this.Text = "AutoLodedObjForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoLoadedObjForm_FormClosing);
            this.Load += new System.EventHandler(this.AutoLoadedObjForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoadedObj)).EndInit();
            this.gbCenterPointAndRadius.ResumeLayout(false);
            this.gbCenterPointAndRadius.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpIterationPrm.ResumeLayout(false);
            this.grpIterationPrm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLoadedObj;
        private System.Windows.Forms.Button AutoLodededObjOK;
        private MCTester.Controls.NumericTextBox ntxRadiusX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chxIsClonedSchemes;
        private Controls.CtrlSamplePoint ctrlCenterPoint;
        private Controls.Ctrl3DFVector ctrl3DFVectorCenterPoint;
        private System.Windows.Forms.RadioButton rbSelectRect;
        private System.Windows.Forms.GroupBox gbCenterPointAndRadius;
        private System.Windows.Forms.RadioButton rbVisibleArea;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbFixedOffset;
        private System.Windows.Forms.RadioButton rbRandom;
        private Controls.Ctrl2DVector ctrl2DVectorOffset;
        private System.Windows.Forms.GroupBox grpIterationPrm;
        private Controls.NumericTextBox ntxAngle;
        private System.Windows.Forms.Label label4;
        private Controls.NumericTextBox ntxNoIterationNotCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Controls.Ctrl2DVector ctrl2DVectorMovement;
        private System.Windows.Forms.Button btnStartIterations;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStop;
        private Controls.NumericTextBox ntxRadiusY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Controls.NumericTextBox ntxPropertyId;
        private System.Windows.Forms.Button btnDeleteObjects;
        private Controls.NumericTextBox ntxTotalIteration;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chxRandom;
        private System.Windows.Forms.DataGridViewButtonColumn ObjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}