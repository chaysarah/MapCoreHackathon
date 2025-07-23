namespace MCTester.Controls
{
    partial class CtrlSMcBox
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
            this.gbSMcBox = new System.Windows.Forms.GroupBox();
            this.btnPaintRect = new System.Windows.Forms.Button();
            this.chxIsShowBoundingBox = new System.Windows.Forms.CheckBox();
            this.ctrlSamplePointBottomRight = new MCTester.Controls.CtrlSamplePoint();
            this.ctrlSamplePointTopLeft = new MCTester.Controls.CtrlSamplePoint();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrl3DVectorMax = new MCTester.Controls.Ctrl3DVector();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrl3DVectorMin = new MCTester.Controls.Ctrl3DVector();
            this.gbSMcBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSMcBox
            // 
            this.gbSMcBox.Controls.Add(this.btnPaintRect);
            this.gbSMcBox.Controls.Add(this.chxIsShowBoundingBox);
            this.gbSMcBox.Controls.Add(this.ctrlSamplePointBottomRight);
            this.gbSMcBox.Controls.Add(this.ctrlSamplePointTopLeft);
            this.gbSMcBox.Controls.Add(this.label2);
            this.gbSMcBox.Controls.Add(this.ctrl3DVectorMax);
            this.gbSMcBox.Controls.Add(this.label1);
            this.gbSMcBox.Controls.Add(this.ctrl3DVectorMin);
            this.gbSMcBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSMcBox.Location = new System.Drawing.Point(0, 0);
            this.gbSMcBox.Name = "gbSMcBox";
            this.gbSMcBox.Size = new System.Drawing.Size(304, 85);
            this.gbSMcBox.TabIndex = 1;
            this.gbSMcBox.TabStop = false;
            this.gbSMcBox.Text = "SMcBox";
            // 
            // btnPaintRect
            // 
            this.btnPaintRect.Location = new System.Drawing.Point(233, 59);
            this.btnPaintRect.Name = "btnPaintRect";
            this.btnPaintRect.Size = new System.Drawing.Size(62, 23);
            this.btnPaintRect.TabIndex = 8;
            this.btnPaintRect.Text = "Rect...";
            this.btnPaintRect.UseVisualStyleBackColor = true;
            this.btnPaintRect.Click += new System.EventHandler(this.btnPaintRect_Click);
            // 
            // chxIsShowBoundingBox
            // 
            this.chxIsShowBoundingBox.AutoSize = true;
            this.chxIsShowBoundingBox.Location = new System.Drawing.Point(6, 63);
            this.chxIsShowBoundingBox.Name = "chxIsShowBoundingBox";
            this.chxIsShowBoundingBox.Size = new System.Drawing.Size(167, 17);
            this.chxIsShowBoundingBox.TabIndex = 7;
            this.chxIsShowBoundingBox.Text = "Show bounding box rectangle";
            this.chxIsShowBoundingBox.UseVisualStyleBackColor = true;
            this.chxIsShowBoundingBox.CheckedChanged += new System.EventHandler(this.chxIsShowBoundingBox_CheckedChanged);
            // 
            // ctrlSamplePointBottomRight
            // 
            this.ctrlSamplePointBottomRight._DgvControlName = null;
            this.ctrlSamplePointBottomRight._IsRelativeToDTM = false;
            this.ctrlSamplePointBottomRight._PointInOverlayManagerCoordSys = false;
            this.ctrlSamplePointBottomRight._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointBottomRight._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointBottomRight._SampleOnePoint = true;
            this.ctrlSamplePointBottomRight._UserControlName = null;
            this.ctrlSamplePointBottomRight.IsAsync = false;
            this.ctrlSamplePointBottomRight.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointBottomRight.Location = new System.Drawing.Point(267, 36);
            this.ctrlSamplePointBottomRight.Name = "ctrlSamplePointBottomRight";
            this.ctrlSamplePointBottomRight.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointBottomRight.Size = new System.Drawing.Size(28, 23);
            this.ctrlSamplePointBottomRight.TabIndex = 5;
            this.ctrlSamplePointBottomRight.Text = "...";
            this.ctrlSamplePointBottomRight.UseVisualStyleBackColor = true;
            // 
            // ctrlSamplePointTopLeft
            // 
            this.ctrlSamplePointTopLeft._DgvControlName = null;
            this.ctrlSamplePointTopLeft._IsRelativeToDTM = false;
            this.ctrlSamplePointTopLeft._PointInOverlayManagerCoordSys = false;
            this.ctrlSamplePointTopLeft._PointZValue = 1.7976931348623157E+308D;
            this.ctrlSamplePointTopLeft._QueryPrecision = MapCore.DNEQueryPrecision._EQP_DEFAULT;
            this.ctrlSamplePointTopLeft._SampleOnePoint = true;
            this.ctrlSamplePointTopLeft._UserControlName = null;
            this.ctrlSamplePointTopLeft.IsAsync = false;
            this.ctrlSamplePointTopLeft.IsChangeEnableWhenUserSelectPoint = false;
            this.ctrlSamplePointTopLeft.Location = new System.Drawing.Point(267, 12);
            this.ctrlSamplePointTopLeft.Name = "ctrlSamplePointTopLeft";
            this.ctrlSamplePointTopLeft.PointCoordSystem = MapCore.DNEMcPointCoordSystem._EPCS_WORLD;
            this.ctrlSamplePointTopLeft.Size = new System.Drawing.Size(28, 23);
            this.ctrlSamplePointTopLeft.TabIndex = 4;
            this.ctrlSamplePointTopLeft.Text = "...";
            this.ctrlSamplePointTopLeft.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "MAX:";
            // 
            // ctrl3DVectorMax
            // 
            this.ctrl3DVectorMax.IsReadOnly = false;
            this.ctrl3DVectorMax.Location = new System.Drawing.Point(39, 35);
            this.ctrl3DVectorMax.Name = "ctrl3DVectorMax";
            this.ctrl3DVectorMax.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorMax.TabIndex = 2;
            this.ctrl3DVectorMax.X = 0D;
            this.ctrl3DVectorMax.Y = 0D;
            this.ctrl3DVectorMax.Z = 0D;
            this.ctrl3DVectorMax.Leave += new System.EventHandler(this.ctrl3DVectorMax_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MIN:";
            // 
            // ctrl3DVectorMin
            // 
            this.ctrl3DVectorMin.IsReadOnly = false;
            this.ctrl3DVectorMin.Location = new System.Drawing.Point(39, 11);
            this.ctrl3DVectorMin.Name = "ctrl3DVectorMin";
            this.ctrl3DVectorMin.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorMin.TabIndex = 0;
            this.ctrl3DVectorMin.X = 0D;
            this.ctrl3DVectorMin.Y = 0D;
            this.ctrl3DVectorMin.Z = 0D;
            this.ctrl3DVectorMin.Leave += new System.EventHandler(this.ctrl3DVectorMin_Leave);
            // 
            // CtrlSMcBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSMcBox);
            this.Name = "CtrlSMcBox";
            this.Size = new System.Drawing.Size(304, 85);
            this.gbSMcBox.ResumeLayout(false);
            this.gbSMcBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Ctrl3DVector ctrl3DVectorMin;
        private System.Windows.Forms.GroupBox gbSMcBox;
        private System.Windows.Forms.Label label2;
        private Ctrl3DVector ctrl3DVectorMax;
        private System.Windows.Forms.Label label1;
        private CtrlSamplePoint ctrlSamplePointBottomRight;
        private CtrlSamplePoint ctrlSamplePointTopLeft;
        private System.Windows.Forms.Button btnPaintRect;
        private System.Windows.Forms.CheckBox chxIsShowBoundingBox;
    }
}
