namespace MCTester.General_Forms
{
    partial class frmCreateSymbology
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
            this.btnCreatePointlessFromSymbology = new System.Windows.Forms.Button();
            this.chxFlipped = new System.Windows.Forms.CheckBox();
            this.btnCreateFromSymbology = new System.Windows.Forms.Button();
            this.btnInitializeAmplifiers = new System.Windows.Forms.Button();
            this.btnInitObject = new System.Windows.Forms.Button();
            this.btnEditObject = new System.Windows.Forms.Button();
            this.btnShowAnchorPoints1 = new System.Windows.Forms.Button();
            this.btnShowAnchorPoints2 = new System.Windows.Forms.Button();
            this.btnEditObjectPointless = new System.Windows.Forms.Button();
            this.ctrlSymbologySymbolID1 = new MCTester.Controls.CtrlSymbologySymbolID();
            this.ctrlPointsGrid1 = new MCTester.Controls.CtrlPointsGrid();
            this.ctrlAmplifiers1 = new MCTester.Controls.CtrlAmplifiers();
            this.ctrlGeometricAmplifiers1 = new MCTester.Controls.CtrlGeometricAmplifiers();
            this.gbAnchorPoints = new System.Windows.Forms.GroupBox();
            this.gbAnchorPoints.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreatePointlessFromSymbology
            // 
            this.btnCreatePointlessFromSymbology.Location = new System.Drawing.Point(507, 211);
            this.btnCreatePointlessFromSymbology.Name = "btnCreatePointlessFromSymbology";
            this.btnCreatePointlessFromSymbology.Size = new System.Drawing.Size(188, 23);
            this.btnCreatePointlessFromSymbology.TabIndex = 36;
            this.btnCreatePointlessFromSymbology.Text = "Create Pointless From Symbology";
            this.btnCreatePointlessFromSymbology.UseVisualStyleBackColor = true;
            this.btnCreatePointlessFromSymbology.Click += new System.EventHandler(this.btnCreatePointlessFromSymbology_Click);
            // 
            // chxFlipped
            // 
            this.chxFlipped.AutoSize = true;
            this.chxFlipped.Location = new System.Drawing.Point(378, 75);
            this.chxFlipped.Name = "chxFlipped";
            this.chxFlipped.Size = new System.Drawing.Size(60, 17);
            this.chxFlipped.TabIndex = 37;
            this.chxFlipped.Text = "Flipped";
            this.chxFlipped.UseVisualStyleBackColor = true;
            // 
            // btnCreateFromSymbology
            // 
            this.btnCreateFromSymbology.Location = new System.Drawing.Point(552, 603);
            this.btnCreateFromSymbology.Name = "btnCreateFromSymbology";
            this.btnCreateFromSymbology.Size = new System.Drawing.Size(145, 23);
            this.btnCreateFromSymbology.TabIndex = 124;
            this.btnCreateFromSymbology.Text = "Create From Symbology";
            this.btnCreateFromSymbology.UseVisualStyleBackColor = true;
            this.btnCreateFromSymbology.Click += new System.EventHandler(this.btnCreateFromSymbology_Click);
            // 
            // btnInitializeAmplifiers
            // 
            this.btnInitializeAmplifiers.Location = new System.Drawing.Point(482, 80);
            this.btnInitializeAmplifiers.Name = "btnInitializeAmplifiers";
            this.btnInitializeAmplifiers.Size = new System.Drawing.Size(120, 23);
            this.btnInitializeAmplifiers.TabIndex = 129;
            this.btnInitializeAmplifiers.Text = "Initialize Amplifiers";
            this.btnInitializeAmplifiers.UseVisualStyleBackColor = true;
            this.btnInitializeAmplifiers.Click += new System.EventHandler(this.btnInitializeAmplifiers_Click);
            // 
            // btnInitObject
            // 
            this.btnInitObject.Enabled = false;
            this.btnInitObject.Location = new System.Drawing.Point(550, 240);
            this.btnInitObject.Name = "btnInitObject";
            this.btnInitObject.Size = new System.Drawing.Size(145, 23);
            this.btnInitObject.TabIndex = 131;
            this.btnInitObject.Text = "Start Init Object";
            this.btnInitObject.UseVisualStyleBackColor = true;
            this.btnInitObject.Click += new System.EventHandler(this.btnInitObject_Click);
            // 
            // btnEditObject
            // 
            this.btnEditObject.Enabled = false;
            this.btnEditObject.Location = new System.Drawing.Point(552, 661);
            this.btnEditObject.Name = "btnEditObject";
            this.btnEditObject.Size = new System.Drawing.Size(145, 23);
            this.btnEditObject.TabIndex = 132;
            this.btnEditObject.Text = "Start Edit Object";
            this.btnEditObject.UseVisualStyleBackColor = true;
            this.btnEditObject.Click += new System.EventHandler(this.btnEditObject_Click);
            // 
            // btnShowAnchorPoints1
            // 
            this.btnShowAnchorPoints1.Enabled = false;
            this.btnShowAnchorPoints1.Location = new System.Drawing.Point(550, 298);
            this.btnShowAnchorPoints1.Name = "btnShowAnchorPoints1";
            this.btnShowAnchorPoints1.Size = new System.Drawing.Size(145, 23);
            this.btnShowAnchorPoints1.TabIndex = 142;
            this.btnShowAnchorPoints1.Text = "Show Anchor Points";
            this.btnShowAnchorPoints1.UseVisualStyleBackColor = true;
            this.btnShowAnchorPoints1.Click += new System.EventHandler(this.btnShowAnchorPoints1_Click);
            // 
            // btnShowAnchorPoints2
            // 
            this.btnShowAnchorPoints2.Enabled = false;
            this.btnShowAnchorPoints2.Location = new System.Drawing.Point(552, 632);
            this.btnShowAnchorPoints2.Name = "btnShowAnchorPoints2";
            this.btnShowAnchorPoints2.Size = new System.Drawing.Size(145, 23);
            this.btnShowAnchorPoints2.TabIndex = 143;
            this.btnShowAnchorPoints2.Text = "Show Anchor Points";
            this.btnShowAnchorPoints2.UseVisualStyleBackColor = true;
            this.btnShowAnchorPoints2.Click += new System.EventHandler(this.btnShowAnchorPoints2_Click);
            // 
            // btnEditObjectPointless
            // 
            this.btnEditObjectPointless.Enabled = false;
            this.btnEditObjectPointless.Location = new System.Drawing.Point(550, 269);
            this.btnEditObjectPointless.Name = "btnEditObjectPointless";
            this.btnEditObjectPointless.Size = new System.Drawing.Size(145, 23);
            this.btnEditObjectPointless.TabIndex = 144;
            this.btnEditObjectPointless.Text = "Start Edit Object";
            this.btnEditObjectPointless.UseVisualStyleBackColor = true;
            this.btnEditObjectPointless.Click += new System.EventHandler(this.btnEditObjectPointless_Click);
            // 
            // ctrlSymbologySymbolID1
            // 
            this.ctrlSymbologySymbolID1.Location = new System.Drawing.Point(9, 2);
            this.ctrlSymbologySymbolID1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ctrlSymbologySymbolID1.Name = "ctrlSymbologySymbolID1";
            this.ctrlSymbologySymbolID1.Size = new System.Drawing.Size(467, 101);
            this.ctrlSymbologySymbolID1.TabIndex = 134;
            // 
            // ctrlPointsGrid1
            // 
            this.ctrlPointsGrid1.Location = new System.Drawing.Point(5, 20);
            this.ctrlPointsGrid1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ctrlPointsGrid1.Name = "ctrlPointsGrid1";
            this.ctrlPointsGrid1.Size = new System.Drawing.Size(361, 192);
            this.ctrlPointsGrid1.TabIndex = 133;
            // 
            // ctrlAmplifiers1
            // 
            this.ctrlAmplifiers1.Location = new System.Drawing.Point(-3, 103);
            this.ctrlAmplifiers1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ctrlAmplifiers1.Name = "ctrlAmplifiers1";
            this.ctrlAmplifiers1.Size = new System.Drawing.Size(431, 225);
            this.ctrlAmplifiers1.TabIndex = 130;
            // 
            // ctrlGeometricAmplifiers1
            // 
            this.ctrlGeometricAmplifiers1.Location = new System.Drawing.Point(423, 366);
            this.ctrlGeometricAmplifiers1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.ctrlGeometricAmplifiers1.Name = "ctrlGeometricAmplifiers1";
            this.ctrlGeometricAmplifiers1.Size = new System.Drawing.Size(274, 232);
            this.ctrlGeometricAmplifiers1.TabIndex = 126;
            // 
            // gbAnchorPoints
            // 
            this.gbAnchorPoints.Controls.Add(this.ctrlPointsGrid1);
            this.gbAnchorPoints.Location = new System.Drawing.Point(12, 366);
            this.gbAnchorPoints.Name = "gbAnchorPoints";
            this.gbAnchorPoints.Size = new System.Drawing.Size(400, 218);
            this.gbAnchorPoints.TabIndex = 146;
            this.gbAnchorPoints.TabStop = false;
            this.gbAnchorPoints.Text = "Anchor Points";
            // 
            // frmCreateSymbology
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(741, 445);
            this.Controls.Add(this.gbAnchorPoints);
            this.Controls.Add(this.btnEditObjectPointless);
            this.Controls.Add(this.btnShowAnchorPoints2);
            this.Controls.Add(this.btnShowAnchorPoints1);
            this.Controls.Add(this.ctrlSymbologySymbolID1);
            this.Controls.Add(this.btnEditObject);
            this.Controls.Add(this.btnInitObject);
            this.Controls.Add(this.ctrlAmplifiers1);
            this.Controls.Add(this.btnInitializeAmplifiers);
            this.Controls.Add(this.ctrlGeometricAmplifiers1);
            this.Controls.Add(this.btnCreateFromSymbology);
            this.Controls.Add(this.chxFlipped);
            this.Controls.Add(this.btnCreatePointlessFromSymbology);
            this.Name = "frmCreateSymbology";
            this.Text = "Create Symbology";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCreateSymbology_FormClosed);
            this.gbAnchorPoints.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCreatePointlessFromSymbology;
        private System.Windows.Forms.CheckBox chxFlipped;
        private System.Windows.Forms.Button btnCreateFromSymbology;
        private Controls.CtrlGeometricAmplifiers ctrlGeometricAmplifiers1;
        private System.Windows.Forms.Button btnInitializeAmplifiers;
        private Controls.CtrlAmplifiers ctrlAmplifiers1;
        private System.Windows.Forms.Button btnInitObject;
        private System.Windows.Forms.Button btnEditObject;
        private Controls.CtrlPointsGrid ctrlPointsGrid1;
        private Controls.CtrlSymbologySymbolID ctrlSymbologySymbolID1;
        private System.Windows.Forms.Button btnShowAnchorPoints1;
        private System.Windows.Forms.Button btnShowAnchorPoints2;
        private System.Windows.Forms.Button btnEditObjectPointless;
        private System.Windows.Forms.GroupBox gbAnchorPoints;
    }
}