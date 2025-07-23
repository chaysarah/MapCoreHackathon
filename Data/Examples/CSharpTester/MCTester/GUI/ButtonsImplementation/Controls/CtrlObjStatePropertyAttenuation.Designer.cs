namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyAttenuation
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
            this.label4 = new System.Windows.Forms.Label();
            this.ntxRegRange = new MCTester.Controls.NumericTextBox();
            this.ntxRegSquare = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ntxRegLinear = new MCTester.Controls.NumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ntxRegConst = new MCTester.Controls.NumericTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.ntxSelRange = new MCTester.Controls.NumericTextBox();
            this.ntxSelConst = new MCTester.Controls.NumericTextBox();
            this.ntxSelLinear = new MCTester.Controls.NumericTextBox();
            this.ntxSelSquare = new MCTester.Controls.NumericTextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.label4);
            this.tpRegular.Controls.Add(this.ntxRegRange);
            this.tpRegular.Controls.Add(this.ntxRegSquare);
            this.tpRegular.Controls.Add(this.label6);
            this.tpRegular.Controls.Add(this.ntxRegLinear);
            this.tpRegular.Controls.Add(this.label7);
            this.tpRegular.Controls.Add(this.ntxRegConst);
            this.tpRegular.Controls.Add(this.label8);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.label8, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegConst, 0);
            this.tpRegular.Controls.SetChildIndex(this.label7, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegLinear, 0);
            this.tpRegular.Controls.SetChildIndex(this.label6, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegSquare, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegRange, 0);
            this.tpRegular.Controls.SetChildIndex(this.label4, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ntxSelSquare);
            this.tpObjectState.Controls.Add(this.ntxSelLinear);
            this.tpObjectState.Controls.Add(this.ntxSelConst);
            this.tpObjectState.Controls.Add(this.ntxSelRange);
            this.tpObjectState.Controls.Add(this.label12);
            this.tpObjectState.Controls.Add(this.label11);
            this.tpObjectState.Controls.Add(this.label10);
            this.tpObjectState.Controls.Add(this.label9);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label9, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label10, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label11, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label12, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelRange, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelConst, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelLinear, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelSquare, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(298, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "Range:";
            // 
            // ntxRegRange
            // 
            this.ntxRegRange.Location = new System.Drawing.Point(341, 48);
            this.ntxRegRange.Name = "ntxRegRange";
            this.ntxRegRange.Size = new System.Drawing.Size(40, 20);
            this.ntxRegRange.TabIndex = 39;
            this.ntxRegRange.Validating += new System.ComponentModel.CancelEventHandler(this.ntxRegRange_Validating);
            // 
            // ntxRegSquare
            // 
            this.ntxRegSquare.Location = new System.Drawing.Point(259, 48);
            this.ntxRegSquare.Name = "ntxRegSquare";
            this.ntxRegSquare.Size = new System.Drawing.Size(37, 20);
            this.ntxRegSquare.TabIndex = 38;
            this.ntxRegSquare.Text = "0";
            this.ntxRegSquare.Validating += new System.ComponentModel.CancelEventHandler(this.ntxRegSquare_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Square:";
            // 
            // ntxRegLinear
            // 
            this.ntxRegLinear.Location = new System.Drawing.Point(180, 48);
            this.ntxRegLinear.Name = "ntxRegLinear";
            this.ntxRegLinear.Size = new System.Drawing.Size(36, 20);
            this.ntxRegLinear.TabIndex = 36;
            this.ntxRegLinear.Text = "0";
            this.ntxRegLinear.Validating += new System.ComponentModel.CancelEventHandler(this.ntxRegLinear_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(144, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Linear:";
            // 
            // ntxRegConst
            // 
            this.ntxRegConst.Location = new System.Drawing.Point(108, 48);
            this.ntxRegConst.Name = "ntxRegConst";
            this.ntxRegConst.Size = new System.Drawing.Size(35, 20);
            this.ntxRegConst.TabIndex = 34;
            this.ntxRegConst.Text = "0";
            this.ntxRegConst.Validating += new System.ComponentModel.CancelEventHandler(this.ntxRegConst_Validating);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(74, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Const:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(304, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Range:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(223, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Square:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(146, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Linear:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(74, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "Const:";
            // 
            // ntxSelRange
            // 
            this.ntxSelRange.Location = new System.Drawing.Point(344, 47);
            this.ntxSelRange.Name = "ntxSelRange";
            this.ntxSelRange.Size = new System.Drawing.Size(36, 20);
            this.ntxSelRange.TabIndex = 38;
            this.ntxSelRange.Validating += new System.ComponentModel.CancelEventHandler(this.ntxSelRange_Validating);
            // 
            // ntxSelConst
            // 
            this.ntxSelConst.Location = new System.Drawing.Point(108, 46);
            this.ntxSelConst.Name = "ntxSelConst";
            this.ntxSelConst.Size = new System.Drawing.Size(38, 20);
            this.ntxSelConst.TabIndex = 39;
            this.ntxSelConst.Validating += new System.ComponentModel.CancelEventHandler(this.ntxSelConst_Validating);
            // 
            // ntxSelLinear
            // 
            this.ntxSelLinear.Location = new System.Drawing.Point(184, 46);
            this.ntxSelLinear.Name = "ntxSelLinear";
            this.ntxSelLinear.Size = new System.Drawing.Size(38, 20);
            this.ntxSelLinear.TabIndex = 40;
            this.ntxSelLinear.Validating += new System.ComponentModel.CancelEventHandler(this.ntxSelLinear_Validating);
            // 
            // ntxSelSquare
            // 
            this.ntxSelSquare.Location = new System.Drawing.Point(264, 46);
            this.ntxSelSquare.Name = "ntxSelSquare";
            this.ntxSelSquare.Size = new System.Drawing.Size(38, 20);
            this.ntxSelSquare.TabIndex = 41;
            this.ntxSelSquare.Validating += new System.ComponentModel.CancelEventHandler(this.ntxSelSquare_Validating);
            // 
            // CtrlObjStatePropertyAttenuation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyAttenuation";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private NumericTextBox ntxRegRange;
        private NumericTextBox ntxRegSquare;
        private System.Windows.Forms.Label label6;
        private NumericTextBox ntxRegLinear;
        private System.Windows.Forms.Label label7;
        private NumericTextBox ntxRegConst;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private NumericTextBox ntxSelRange;
        private NumericTextBox ntxSelConst;
        private NumericTextBox ntxSelLinear;
        private NumericTextBox ntxSelSquare;
    }
}
