namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyNumericUpDown
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
            this.lblSelFVect2D = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudRegVal = new System.Windows.Forms.NumericUpDown();
            this.nudSelVal = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelVal)).BeginInit();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.nudRegVal);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.nudRegVal, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.label4);
            this.tpObjectState.Controls.Add(this.nudSelVal);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.nudSelVal, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label4, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // lblSelFVect2D
            // 
            this.lblSelFVect2D.Location = new System.Drawing.Point(0, 0);
            this.lblSelFVect2D.Name = "lblSelFVect2D";
            this.lblSelFVect2D.Size = new System.Drawing.Size(100, 23);
            this.lblSelFVect2D.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 35;
            this.label4.Text = "label4";
            // 
            // nudRegVal
            // 
            this.nudRegVal.Location = new System.Drawing.Point(78, 48);
            this.nudRegVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRegVal.Name = "nudRegVal";
            this.nudRegVal.Size = new System.Drawing.Size(67, 20);
            this.nudRegVal.TabIndex = 23;
            this.nudRegVal.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRegVal.Validating += new System.ComponentModel.CancelEventHandler(this.nudRegVal_Validating);
            // 
            // nudSelVal
            // 
            this.nudSelVal.Location = new System.Drawing.Point(169, 52);
            this.nudSelVal.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSelVal.Name = "nudSelVal";
            this.nudSelVal.Size = new System.Drawing.Size(67, 20);
            this.nudSelVal.TabIndex = 18;
            this.nudSelVal.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudSelVal.Validating += new System.ComponentModel.CancelEventHandler(this.nudSelVal_Validating);
            // 
            // CtrlObjStatePropertyNumericUpDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyNumericUpDown";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRegVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelVal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblSelFVect2D;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudRegVal;
        private System.Windows.Forms.NumericUpDown nudSelVal;
    }
}
