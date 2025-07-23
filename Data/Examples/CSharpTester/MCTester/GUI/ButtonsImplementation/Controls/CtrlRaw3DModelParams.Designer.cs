namespace MCTester.Controls
{
    partial class CtrlRaw3DModelParams
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
            this.chxOrthometricHeights = new System.Windows.Forms.CheckBox();
            this.chxWithIndexing = new System.Windows.Forms.CheckBox();
            this.btnServerRequestParams = new System.Windows.Forms.Button();
            this.ctrlBuildIndexingDataParams1 = new MCTester.Controls.CtrlBuildIndexingDataParams();
            this.pnlPositionOffset = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlPositionOffset = new MCTester.Controls.Ctrl3DVector();
            this.pnlPositionOffset.SuspendLayout();
            this.SuspendLayout();
            // 
            // chxOrthometricHeights
            // 
            this.chxOrthometricHeights.AutoSize = true;
            this.chxOrthometricHeights.Location = new System.Drawing.Point(5, 9);
            this.chxOrthometricHeights.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.chxOrthometricHeights.Name = "chxOrthometricHeights";
            this.chxOrthometricHeights.Size = new System.Drawing.Size(119, 17);
            this.chxOrthometricHeights.TabIndex = 84;
            this.chxOrthometricHeights.Text = "Orthometric Heights";
            this.chxOrthometricHeights.UseVisualStyleBackColor = true;
            // 
            // chxWithIndexing
            // 
            this.chxWithIndexing.AutoSize = true;
            this.chxWithIndexing.Location = new System.Drawing.Point(5, 35);
            this.chxWithIndexing.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.chxWithIndexing.Name = "chxWithIndexing";
            this.chxWithIndexing.Size = new System.Drawing.Size(91, 17);
            this.chxWithIndexing.TabIndex = 92;
            this.chxWithIndexing.Text = "With Indexing";
            this.chxWithIndexing.UseVisualStyleBackColor = true;
            this.chxWithIndexing.CheckedChanged += new System.EventHandler(this.chxWithIndexing_CheckedChanged);
            // 
            // btnServerRequestParams
            // 
            this.btnServerRequestParams.Location = new System.Drawing.Point(488, 247);
            this.btnServerRequestParams.Name = "btnServerRequestParams";
            this.btnServerRequestParams.Size = new System.Drawing.Size(167, 23);
            this.btnServerRequestParams.TabIndex = 93;
            this.btnServerRequestParams.Text = "Server Request Params";
            this.btnServerRequestParams.UseVisualStyleBackColor = true;
            this.btnServerRequestParams.Click += new System.EventHandler(this.btnServerRequestParams_Click);
            // 
            // ctrlBuildIndexingDataParams1
            // 
            this.ctrlBuildIndexingDataParams1.Location = new System.Drawing.Point(121, -3);
            this.ctrlBuildIndexingDataParams1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlBuildIndexingDataParams1.Name = "ctrlBuildIndexingDataParams1";
            this.ctrlBuildIndexingDataParams1.Size = new System.Drawing.Size(610, 359);
            this.ctrlBuildIndexingDataParams1.TabIndex = 89;
            // 
            // pnlPositionOffset
            // 
            this.pnlPositionOffset.Controls.Add(this.label2);
            this.pnlPositionOffset.Controls.Add(this.ctrlPositionOffset);
            this.pnlPositionOffset.Location = new System.Drawing.Point(5, 361);
            this.pnlPositionOffset.Name = "pnlPositionOffset";
            this.pnlPositionOffset.Size = new System.Drawing.Size(327, 33);
            this.pnlPositionOffset.TabIndex = 94;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Position Offset";
            // 
            // ctrlPositionOffset
            // 
            this.ctrlPositionOffset.IsReadOnly = false;
            this.ctrlPositionOffset.Location = new System.Drawing.Point(92, 3);
            this.ctrlPositionOffset.Name = "ctrlPositionOffset";
            this.ctrlPositionOffset.Size = new System.Drawing.Size(232, 26);
            this.ctrlPositionOffset.TabIndex = 34;
            this.ctrlPositionOffset.X = 0D;
            this.ctrlPositionOffset.Y = 0D;
            this.ctrlPositionOffset.Z = 0D;
            // 
            // CtrlRaw3DModelParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlPositionOffset);
            this.Controls.Add(this.btnServerRequestParams);
            this.Controls.Add(this.chxWithIndexing);
            this.Controls.Add(this.ctrlBuildIndexingDataParams1);
            this.Controls.Add(this.chxOrthometricHeights);
            this.Name = "CtrlRaw3DModelParams";
            this.Size = new System.Drawing.Size(729, 404);
            this.pnlPositionOffset.ResumeLayout(false);
            this.pnlPositionOffset.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chxOrthometricHeights;
        private CtrlBuildIndexingDataParams ctrlBuildIndexingDataParams1;
        private System.Windows.Forms.CheckBox chxWithIndexing;
        private System.Windows.Forms.Button btnServerRequestParams;
        private System.Windows.Forms.Panel pnlPositionOffset;
        private System.Windows.Forms.Label label2;
        private Ctrl3DVector ctrlPositionOffset;
    }
}
