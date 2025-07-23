namespace MCTester.MapWorld.MapUserControls
{
    partial class ucMapHeightLines
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
            this.dgvScaleStep = new System.Windows.Forms.DataGridView();
            this.colMaxScale = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLineHeightGap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColors = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbColorInterpolationMode = new System.Windows.Forms.GroupBox();
            this.ntxMinHeight = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxMaxHeight = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chxIsColorInterpolationEnabled = new System.Windows.Forms.CheckBox();
            this.ntxLineWidth = new MCTester.Controls.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaleStep)).BeginInit();
            this.gbColorInterpolationMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvScaleStep
            // 
            this.dgvScaleStep.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScaleStep.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaxScale,
            this.colLineHeightGap,
            this.colColors});
            this.dgvScaleStep.Location = new System.Drawing.Point(3, 3);
            this.dgvScaleStep.Name = "dgvScaleStep";
            this.dgvScaleStep.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgvScaleStep.Size = new System.Drawing.Size(315, 226);
            this.dgvScaleStep.TabIndex = 17;
            this.dgvScaleStep.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvScaleStep_UserDeletingRow);
            this.dgvScaleStep.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvScaleStep_CellClick);
            // 
            // colMaxScale
            // 
            this.colMaxScale.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colMaxScale.FillWeight = 34.89305F;
            this.colMaxScale.HeaderText = "Max Scale";
            this.colMaxScale.Name = "colMaxScale";
            this.colMaxScale.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMaxScale.Width = 76;
            // 
            // colLineHeightGap
            // 
            this.colLineHeightGap.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLineHeightGap.FillWeight = 46.59824F;
            this.colLineHeightGap.HeaderText = "Line Height Gap";
            this.colLineHeightGap.Name = "colLineHeightGap";
            // 
            // colColors
            // 
            this.colColors.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colColors.FillWeight = 110.2803F;
            this.colColors.HeaderText = "Colors";
            this.colColors.Name = "colColors";
            this.colColors.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColors.Text = "Colors List";
            this.colColors.UseColumnTextForButtonValue = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(711, 611);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Line Width:";
            // 
            // gbColorInterpolationMode
            // 
            this.gbColorInterpolationMode.Controls.Add(this.ntxMinHeight);
            this.gbColorInterpolationMode.Controls.Add(this.label3);
            this.gbColorInterpolationMode.Controls.Add(this.ntxMaxHeight);
            this.gbColorInterpolationMode.Controls.Add(this.label2);
            this.gbColorInterpolationMode.Controls.Add(this.chxIsColorInterpolationEnabled);
            this.gbColorInterpolationMode.Location = new System.Drawing.Point(3, 235);
            this.gbColorInterpolationMode.Name = "gbColorInterpolationMode";
            this.gbColorInterpolationMode.Size = new System.Drawing.Size(315, 94);
            this.gbColorInterpolationMode.TabIndex = 21;
            this.gbColorInterpolationMode.TabStop = false;
            this.gbColorInterpolationMode.Text = "Color Interpolation Mode";
            // 
            // ntxMinHeight
            // 
            this.ntxMinHeight.Enabled = false;
            this.ntxMinHeight.Location = new System.Drawing.Point(73, 42);
            this.ntxMinHeight.Name = "ntxMinHeight";
            this.ntxMinHeight.Size = new System.Drawing.Size(100, 20);
            this.ntxMinHeight.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Min Height:";
            // 
            // ntxMaxHeight
            // 
            this.ntxMaxHeight.Enabled = false;
            this.ntxMaxHeight.Location = new System.Drawing.Point(73, 68);
            this.ntxMaxHeight.Name = "ntxMaxHeight";
            this.ntxMaxHeight.Size = new System.Drawing.Size(100, 20);
            this.ntxMaxHeight.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Max Height:";
            // 
            // chxIsColorInterpolationEnabled
            // 
            this.chxIsColorInterpolationEnabled.AutoSize = true;
            this.chxIsColorInterpolationEnabled.Location = new System.Drawing.Point(6, 19);
            this.chxIsColorInterpolationEnabled.Name = "chxIsColorInterpolationEnabled";
            this.chxIsColorInterpolationEnabled.Size = new System.Drawing.Size(164, 17);
            this.chxIsColorInterpolationEnabled.TabIndex = 0;
            this.chxIsColorInterpolationEnabled.Text = "Is Color Interpolation Enabled";
            this.chxIsColorInterpolationEnabled.UseVisualStyleBackColor = true;
            this.chxIsColorInterpolationEnabled.CheckedChanged += new System.EventHandler(this.chxIsColorInterpolationEnabled_CheckedChanged);
            // 
            // ntxLineWidth
            // 
            this.ntxLineWidth.Location = new System.Drawing.Point(76, 341);
            this.ntxLineWidth.Name = "ntxLineWidth";
            this.ntxLineWidth.Size = new System.Drawing.Size(100, 20);
            this.ntxLineWidth.TabIndex = 20;
            // 
            // ucMapHeightLines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.gbColorInterpolationMode);
            this.Controls.Add(this.ntxLineWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvScaleStep);
            this.Name = "ucMapHeightLines";
            this.Size = new System.Drawing.Size(789, 637);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScaleStep)).EndInit();
            this.gbColorInterpolationMode.ResumeLayout(false);
            this.gbColorInterpolationMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvScaleStep;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.NumericTextBox ntxLineWidth;
        private System.Windows.Forms.GroupBox gbColorInterpolationMode;
        private System.Windows.Forms.CheckBox chxIsColorInterpolationEnabled;
        private MCTester.Controls.NumericTextBox ntxMinHeight;
        private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox ntxMaxHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaxScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLineHeightGap;
        private System.Windows.Forms.DataGridViewButtonColumn colColors;
    }
}
