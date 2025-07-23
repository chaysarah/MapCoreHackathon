namespace MCTester.Controls
{
    partial class CtrlPropertyNumericUpDown
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
            this.nudVal = new System.Windows.Forms.NumericUpDown();
            this.lblNumericUpDown = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVal)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(400, 130);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(394, 111);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lblNumericUpDown);
            this.tpRegular.Controls.Add(this.nudVal);
            this.tpRegular.Size = new System.Drawing.Size(386, 85);
            this.tpRegular.Controls.SetChildIndex(this.nudVal, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblNumericUpDown, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // nudVal
            // 
            this.nudVal.Location = new System.Drawing.Point(169, 52);
            this.nudVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudVal.Name = "nudVal";
            this.nudVal.Size = new System.Drawing.Size(67, 20);
            this.nudVal.TabIndex = 18;
            this.nudVal.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // lblNumericUpDown
            // 
            this.lblNumericUpDown.AutoSize = true;
            this.lblNumericUpDown.Location = new System.Drawing.Point(85, 54);
            this.lblNumericUpDown.Name = "lblNumericUpDown";
            this.lblNumericUpDown.Size = new System.Drawing.Size(78, 13);
            this.lblNumericUpDown.TabIndex = 19;
            this.lblNumericUpDown.Text = "UpDownLable:";
            // 
            // CtrlPropertyNumericUpDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyNumericUpDown";
            this.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudVal;
        private System.Windows.Forms.Label lblNumericUpDown;
    }
}
