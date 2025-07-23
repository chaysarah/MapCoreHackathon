namespace MCTester.GUI.Forms
{
    partial class ScanItemsFoundForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanItemsFoundForm));
            this.dgvFoundItemsData = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSetColor = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveColor = new System.Windows.Forms.ToolStripButton();
            this.tsbBlink = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRemoveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chxRemoveOnClosure = new System.Windows.Forms.CheckBox();
            this.btnChooseColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudAlpha = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbStaticObjectsContours = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudChooseStaticObjectsContoursAlpha = new System.Windows.Forms.NumericUpDown();
            this.btnChooseStaticObjectsContoursColor = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRemoveAllHeights = new System.Windows.Forms.Button();
            this.btnRemoveHeight = new System.Windows.Forms.Button();
            this.chxRemoveHeightsOnClosure = new System.Windows.Forms.CheckBox();
            this.btnSetHeight = new System.Windows.Forms.Button();
            this.ntbHeight = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.scanTargetFoundBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gbVectoryFields = new System.Windows.Forms.GroupBox();
            this.cbVectorFields = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoundItemsData)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbStaticObjectsContours.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChooseStaticObjectsContoursAlpha)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scanTargetFoundBindingSource)).BeginInit();
            this.gbVectoryFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvFoundItemsData
            // 
            this.dgvFoundItemsData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFoundItemsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFoundItemsData.Location = new System.Drawing.Point(0, 59);
            this.dgvFoundItemsData.Name = "dgvFoundItemsData";
            this.dgvFoundItemsData.ReadOnly = true;
            this.dgvFoundItemsData.Size = new System.Drawing.Size(1813, 180);
            this.dgvFoundItemsData.TabIndex = 1;
            this.dgvFoundItemsData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFoundItemsData_CellContentClick);
            this.dgvFoundItemsData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgvFoundItemsData_RowsAdded);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator3,
            this.tsbSetColor,
            this.tsbRemoveColor,
            this.tsbBlink,
            this.toolStripSeparator1,
            this.tsbRemoveAll,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1813, 54);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(135, 51);
            this.toolStripLabel1.Text = "Static map objects only";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbSetColor
            // 
            this.tsbSetColor.Enabled = false;
            this.tsbSetColor.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetColor.Image")));
            this.tsbSetColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetColor.Name = "tsbSetColor";
            this.tsbSetColor.Size = new System.Drawing.Size(57, 51);
            this.tsbSetColor.Text = "Set color";
            this.tsbSetColor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbSetColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbSetColor.Click += new System.EventHandler(this.tsbSetColor_Click);
            // 
            // tsbRemoveColor
            // 
            this.tsbRemoveColor.Enabled = false;
            this.tsbRemoveColor.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveColor.Image")));
            this.tsbRemoveColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveColor.Name = "tsbRemoveColor";
            this.tsbRemoveColor.Size = new System.Drawing.Size(69, 51);
            this.tsbRemoveColor.Text = "Reset color";
            this.tsbRemoveColor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbRemoveColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRemoveColor.Click += new System.EventHandler(this.tsbRemoveColor_Click);
            // 
            // tsbBlink
            // 
            this.tsbBlink.CheckOnClick = true;
            this.tsbBlink.Enabled = false;
            this.tsbBlink.Image = ((System.Drawing.Image)(resources.GetObject("tsbBlink.Image")));
            this.tsbBlink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBlink.Name = "tsbBlink";
            this.tsbBlink.Size = new System.Drawing.Size(37, 51);
            this.tsbBlink.Text = "Blink";
            this.tsbBlink.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.tsbBlink.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbBlink.Click += new System.EventHandler(this.tsbBlink_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbRemoveAll
            // 
            this.tsbRemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemoveAll.Image")));
            this.tsbRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveAll.Name = "tsbRemoveAll";
            this.tsbRemoveAll.Size = new System.Drawing.Size(89, 51);
            this.tsbRemoveAll.Text = "Reset all colors";
            this.tsbRemoveAll.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbRemoveAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRemoveAll.Click += new System.EventHandler(this.tsbRemoveAll_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 54);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chxRemoveOnClosure
            // 
            this.chxRemoveOnClosure.AutoSize = true;
            this.chxRemoveOnClosure.Checked = true;
            this.chxRemoveOnClosure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxRemoveOnClosure.Enabled = false;
            this.chxRemoveOnClosure.Location = new System.Drawing.Point(785, 3);
            this.chxRemoveOnClosure.Margin = new System.Windows.Forms.Padding(2);
            this.chxRemoveOnClosure.Name = "chxRemoveOnClosure";
            this.chxRemoveOnClosure.Size = new System.Drawing.Size(75, 43);
            this.chxRemoveOnClosure.TabIndex = 3;
            this.chxRemoveOnClosure.Text = "Reset \r\ncolor \r\non closure";
            this.chxRemoveOnClosure.UseVisualStyleBackColor = true;
            // 
            // btnChooseColor
            // 
            this.btnChooseColor.BackColor = System.Drawing.Color.Red;
            this.btnChooseColor.Location = new System.Drawing.Point(13, 15);
            this.btnChooseColor.Name = "btnChooseColor";
            this.btnChooseColor.Size = new System.Drawing.Size(53, 23);
            this.btnChooseColor.TabIndex = 4;
            this.btnChooseColor.UseVisualStyleBackColor = false;
            this.btnChooseColor.Click += new System.EventHandler(this.btnChooseColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Alpha:";
            // 
            // nudAlpha
            // 
            this.nudAlpha.Location = new System.Drawing.Point(115, 18);
            this.nudAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAlpha.Name = "nudAlpha";
            this.nudAlpha.Size = new System.Drawing.Size(45, 20);
            this.nudAlpha.TabIndex = 6;
            this.nudAlpha.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudAlpha.ValueChanged += new System.EventHandler(this.nudAlpha_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudAlpha);
            this.groupBox1.Controls.Add(this.btnChooseColor);
            this.groupBox1.Location = new System.Drawing.Point(419, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(167, 44);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color Properties:";
            // 
            // gbStaticObjectsContours
            // 
            this.gbStaticObjectsContours.Controls.Add(this.label2);
            this.gbStaticObjectsContours.Controls.Add(this.nudChooseStaticObjectsContoursAlpha);
            this.gbStaticObjectsContours.Controls.Add(this.btnChooseStaticObjectsContoursColor);
            this.gbStaticObjectsContours.Location = new System.Drawing.Point(592, 3);
            this.gbStaticObjectsContours.Name = "gbStaticObjectsContours";
            this.gbStaticObjectsContours.Size = new System.Drawing.Size(187, 44);
            this.gbStaticObjectsContours.TabIndex = 8;
            this.gbStaticObjectsContours.TabStop = false;
            this.gbStaticObjectsContours.Text = "Static Objects Contours Properties:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Alpha:";
            // 
            // nudChooseStaticObjectsContoursAlpha
            // 
            this.nudChooseStaticObjectsContoursAlpha.Location = new System.Drawing.Point(115, 18);
            this.nudChooseStaticObjectsContoursAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudChooseStaticObjectsContoursAlpha.Name = "nudChooseStaticObjectsContoursAlpha";
            this.nudChooseStaticObjectsContoursAlpha.Size = new System.Drawing.Size(45, 20);
            this.nudChooseStaticObjectsContoursAlpha.TabIndex = 6;
            this.nudChooseStaticObjectsContoursAlpha.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudChooseStaticObjectsContoursAlpha.ValueChanged += new System.EventHandler(this.nudChooseStaticObjectsContoursAlpha_ValueChanged);
            // 
            // btnChooseStaticObjectsContoursColor
            // 
            this.btnChooseStaticObjectsContoursColor.BackColor = System.Drawing.Color.Black;
            this.btnChooseStaticObjectsContoursColor.Location = new System.Drawing.Point(6, 15);
            this.btnChooseStaticObjectsContoursColor.Name = "btnChooseStaticObjectsContoursColor";
            this.btnChooseStaticObjectsContoursColor.Size = new System.Drawing.Size(53, 23);
            this.btnChooseStaticObjectsContoursColor.TabIndex = 4;
            this.btnChooseStaticObjectsContoursColor.UseVisualStyleBackColor = false;
            this.btnChooseStaticObjectsContoursColor.Click += new System.EventHandler(this.btnChooseVector3DExtrusionContoursColor_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRemoveAllHeights);
            this.groupBox2.Controls.Add(this.btnRemoveHeight);
            this.groupBox2.Controls.Add(this.chxRemoveHeightsOnClosure);
            this.groupBox2.Controls.Add(this.btnSetHeight);
            this.groupBox2.Controls.Add(this.ntbHeight);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(893, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 55);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Static Objects Heights:";
            // 
            // btnRemoveAllHeights
            // 
            this.btnRemoveAllHeights.Location = new System.Drawing.Point(106, 34);
            this.btnRemoveAllHeights.Name = "btnRemoveAllHeights";
            this.btnRemoveAllHeights.Size = new System.Drawing.Size(95, 22);
            this.btnRemoveAllHeights.TabIndex = 10;
            this.btnRemoveAllHeights.Text = "Reset all";
            this.btnRemoveAllHeights.UseVisualStyleBackColor = true;
            this.btnRemoveAllHeights.Click += new System.EventHandler(this.btnRemoveAllHeights_Click);
            // 
            // btnRemoveHeight
            // 
            this.btnRemoveHeight.Location = new System.Drawing.Point(145, 11);
            this.btnRemoveHeight.Name = "btnRemoveHeight";
            this.btnRemoveHeight.Size = new System.Drawing.Size(56, 23);
            this.btnRemoveHeight.TabIndex = 9;
            this.btnRemoveHeight.Text = "Reset";
            this.btnRemoveHeight.UseVisualStyleBackColor = true;
            this.btnRemoveHeight.Click += new System.EventHandler(this.btnRemoveHeight_Click);
            // 
            // chxRemoveHeightsOnClosure
            // 
            this.chxRemoveHeightsOnClosure.AutoSize = true;
            this.chxRemoveHeightsOnClosure.Checked = true;
            this.chxRemoveHeightsOnClosure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxRemoveHeightsOnClosure.Location = new System.Drawing.Point(206, 11);
            this.chxRemoveHeightsOnClosure.Margin = new System.Windows.Forms.Padding(2);
            this.chxRemoveHeightsOnClosure.Name = "chxRemoveHeightsOnClosure";
            this.chxRemoveHeightsOnClosure.Size = new System.Drawing.Size(75, 43);
            this.chxRemoveHeightsOnClosure.TabIndex = 8;
            this.chxRemoveHeightsOnClosure.Text = "Reset\r\nheight \r\non closure";
            this.chxRemoveHeightsOnClosure.UseVisualStyleBackColor = true;
            // 
            // btnSetHeight
            // 
            this.btnSetHeight.Location = new System.Drawing.Point(106, 11);
            this.btnSetHeight.Name = "btnSetHeight";
            this.btnSetHeight.Size = new System.Drawing.Size(38, 23);
            this.btnSetHeight.TabIndex = 7;
            this.btnSetHeight.Text = "Set";
            this.btnSetHeight.UseVisualStyleBackColor = true;
            this.btnSetHeight.Click += new System.EventHandler(this.btnSetHeight_Click);
            // 
            // ntbHeight
            // 
            this.ntbHeight.Location = new System.Drawing.Point(46, 13);
            this.ntbHeight.Name = "ntbHeight";
            this.ntbHeight.Size = new System.Drawing.Size(54, 20);
            this.ntbHeight.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Height:";
            // 
            // scanTargetFoundBindingSource
            // 
            this.scanTargetFoundBindingSource.DataSource = typeof(MCTester.GUI.Forms.ScanTargetFound);
            // 
            // gbVectoryFields
            // 
            this.gbVectoryFields.Controls.Add(this.cbVectorFields);
            this.gbVectoryFields.Location = new System.Drawing.Point(1192, 3);
            this.gbVectoryFields.Name = "gbVectoryFields";
            this.gbVectoryFields.Size = new System.Drawing.Size(181, 55);
            this.gbVectoryFields.TabIndex = 11;
            this.gbVectoryFields.TabStop = false;
            this.gbVectoryFields.Text = "Vector Field To Add";
            // 
            // cbVectorFields
            // 
            this.cbVectorFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVectorFields.FormattingEnabled = true;
            this.cbVectorFields.Location = new System.Drawing.Point(7, 16);
            this.cbVectorFields.Name = "cbVectorFields";
            this.cbVectorFields.Size = new System.Drawing.Size(163, 21);
            this.cbVectorFields.TabIndex = 8;
            this.cbVectorFields.SelectedIndexChanged += new System.EventHandler(this.cbVectorFields_SelectedIndexChanged);
            // 
            // ScanItemsFoundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1813, 240);
            this.Controls.Add(this.gbVectoryFields);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbStaticObjectsContours);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chxRemoveOnClosure);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvFoundItemsData);
            this.Name = "ScanItemsFoundForm";
            this.Text = "ScanItemsFoundForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanItemsFoundForm_FormClosing);
            this.Load += new System.EventHandler(this.ScanItemsFoundForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFoundItemsData)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbStaticObjectsContours.ResumeLayout(false);
            this.gbStaticObjectsContours.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudChooseStaticObjectsContoursAlpha)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scanTargetFoundBindingSource)).EndInit();
            this.gbVectoryFields.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource scanTargetFoundBindingSource;
        private System.Windows.Forms.DataGridView dgvFoundItemsData;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSetColor;
        private System.Windows.Forms.ToolStripButton tsbRemoveColor;
        private System.Windows.Forms.ToolStripButton tsbBlink;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox chxRemoveOnClosure;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbRemoveAll;
        private System.Windows.Forms.Button btnChooseColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAlpha;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbStaticObjectsContours;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudChooseStaticObjectsContoursAlpha;
        private System.Windows.Forms.Button btnChooseStaticObjectsContoursColor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetHeight;
        private Controls.NumericTextBox ntbHeight;
        private System.Windows.Forms.Button btnRemoveHeight;
        private System.Windows.Forms.CheckBox chxRemoveHeightsOnClosure;
        private System.Windows.Forms.Button btnRemoveAllHeights;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.GroupBox gbVectoryFields;
        private System.Windows.Forms.ComboBox cbVectorFields;
    }
}