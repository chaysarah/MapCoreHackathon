namespace MCTester.Controls
{
    partial class CtrlTilingSchemeParams
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
            this.ctrl2DVectorTilingOrigin = new MCTester.Controls.Ctrl2DVector();
            this.ntxRasterTileMarginInPixels = new MCTester.Controls.NumericTextBox();
            this.ntxTileSizeInPixels = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxLargestTileSizeInMapUnits = new MCTester.Controls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxNumLargestTilesX = new MCTester.Controls.NumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ntxNumLargestTilesY = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbETilingSchemeType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrl2DVectorTilingOrigin
            // 
            this.ctrl2DVectorTilingOrigin.Location = new System.Drawing.Point(79, 15);
            this.ctrl2DVectorTilingOrigin.Name = "ctrl2DVectorTilingOrigin";
            this.ctrl2DVectorTilingOrigin.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DVectorTilingOrigin.TabIndex = 0;
            this.ctrl2DVectorTilingOrigin.X = 0D;
            this.ctrl2DVectorTilingOrigin.Y = 0D;
            this.ctrl2DVectorTilingOrigin.Leave += new System.EventHandler(this.ctrl2DVectorTilingOrigin_Leave);
            // 
            // ntxRasterTileMarginInPixels
            // 
            this.ntxRasterTileMarginInPixels.Location = new System.Drawing.Point(387, 67);
            this.ntxRasterTileMarginInPixels.Name = "ntxRasterTileMarginInPixels";
            this.ntxRasterTileMarginInPixels.Size = new System.Drawing.Size(63, 20);
            this.ntxRasterTileMarginInPixels.TabIndex = 46;
            this.ntxRasterTileMarginInPixels.Text = "0";
            this.ntxRasterTileMarginInPixels.Leave += new System.EventHandler(this.ntxRasterTileMarginInPixels_Leave);
            // 
            // ntxTileSizeInPixels
            // 
            this.ntxTileSizeInPixels.Location = new System.Drawing.Point(166, 71);
            this.ntxTileSizeInPixels.Name = "ntxTileSizeInPixels";
            this.ntxTileSizeInPixels.Size = new System.Drawing.Size(63, 20);
            this.ntxTileSizeInPixels.TabIndex = 45;
            this.ntxTileSizeInPixels.Text = "0";
            this.ntxTileSizeInPixels.Leave += new System.EventHandler(this.ntxTileSizeInPixels_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Largest Tile Size In Map Units:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Tile Size In Pixels:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Raster Tile Margin In Pixels:";
            // 
            // ntxLargestTileSizeInMapUnits
            // 
            this.ntxLargestTileSizeInMapUnits.Location = new System.Drawing.Point(166, 45);
            this.ntxLargestTileSizeInMapUnits.Name = "ntxLargestTileSizeInMapUnits";
            this.ntxLargestTileSizeInMapUnits.Size = new System.Drawing.Size(63, 20);
            this.ntxLargestTileSizeInMapUnits.TabIndex = 41;
            this.ntxLargestTileSizeInMapUnits.Text = "0";
            this.ntxLargestTileSizeInMapUnits.Leave += new System.EventHandler(this.ntxLargestTileSizeInMapUnits_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tiling Origin:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxNumLargestTilesX);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ntxNumLargestTilesY);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ntxRasterTileMarginInPixels);
            this.groupBox1.Controls.Add(this.ntxLargestTileSizeInMapUnits);
            this.groupBox1.Controls.Add(this.ntxTileSizeInPixels);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ctrl2DVectorTilingOrigin);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(0, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(456, 96);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tiling Scheme";
            // 
            // ntxNumLargestTilesX
            // 
            this.ntxNumLargestTilesX.Location = new System.Drawing.Point(387, 18);
            this.ntxNumLargestTilesX.Name = "ntxNumLargestTilesX";
            this.ntxNumLargestTilesX.Size = new System.Drawing.Size(63, 20);
            this.ntxNumLargestTilesX.TabIndex = 50;
            this.ntxNumLargestTilesX.Text = "0";
            this.ntxNumLargestTilesX.Leave += new System.EventHandler(this.ntxNumLargestTilesX_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Num Largest Tiles X:";
            // 
            // ntxNumLargestTilesY
            // 
            this.ntxNumLargestTilesY.Location = new System.Drawing.Point(387, 42);
            this.ntxNumLargestTilesY.Name = "ntxNumLargestTilesY";
            this.ntxNumLargestTilesY.Size = new System.Drawing.Size(63, 20);
            this.ntxNumLargestTilesY.TabIndex = 48;
            this.ntxNumLargestTilesY.Text = "0";
            this.ntxNumLargestTilesY.Leave += new System.EventHandler(this.ntxNumLargestTilesY_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(242, 45);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "Num Largest Tiles Y:";
            // 
            // cmbETilingSchemeType
            // 
            this.cmbETilingSchemeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbETilingSchemeType.FormattingEnabled = true;
            this.cmbETilingSchemeType.Location = new System.Drawing.Point(114, 7);
            this.cmbETilingSchemeType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbETilingSchemeType.Name = "cmbETilingSchemeType";
            this.cmbETilingSchemeType.Size = new System.Drawing.Size(234, 21);
            this.cmbETilingSchemeType.TabIndex = 48;
            this.cmbETilingSchemeType.SelectedIndexChanged += new System.EventHandler(this.cmbETilingSchemeType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 50;
            this.label5.Text = "Tiling Scheme Type:";
            // 
            // CtrlTilingSchemeParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbETilingSchemeType);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrlTilingSchemeParams";
            this.Size = new System.Drawing.Size(456, 136);
            this.Load += new System.EventHandler(this.CtrlTilingSchemeParams_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ctrl2DVector ctrl2DVectorTilingOrigin;
        private System.Windows.Forms.Label label1;
        private NumericTextBox ntxLargestTileSizeInMapUnits;
        private NumericTextBox ntxRasterTileMarginInPixels;
        private NumericTextBox ntxTileSizeInPixels;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbETilingSchemeType;
        private System.Windows.Forms.Label label5;
        private NumericTextBox ntxNumLargestTilesX;
        private System.Windows.Forms.Label label7;
        private NumericTextBox ntxNumLargestTilesY;
        private System.Windows.Forms.Label label6;
    }
}
