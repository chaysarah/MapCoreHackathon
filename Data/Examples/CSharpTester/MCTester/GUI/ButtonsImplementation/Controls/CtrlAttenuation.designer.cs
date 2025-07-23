namespace MCTester.Controls
{
	partial class CtrlAttenuation
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
            this.ntxSquareValue = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxLinearValue = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxConstValue = new MCTester.Controls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxRange = new MCTester.Controls.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ntxSquareValue
            // 
            this.ntxSquareValue.Location = new System.Drawing.Point(253, 0);
            this.ntxSquareValue.Name = "ntxSquareValue";
            this.ntxSquareValue.Size = new System.Drawing.Size(54, 20);
            this.ntxSquareValue.TabIndex = 12;
            this.ntxSquareValue.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Square:";
            // 
            // ntxLinearValue
            // 
            this.ntxLinearValue.Location = new System.Drawing.Point(146, 0);
            this.ntxLinearValue.Name = "ntxLinearValue";
            this.ntxLinearValue.Size = new System.Drawing.Size(54, 20);
            this.ntxLinearValue.TabIndex = 10;
            this.ntxLinearValue.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Linear:";
            // 
            // ntxConstValue
            // 
            this.ntxConstValue.Location = new System.Drawing.Point(44, 0);
            this.ntxConstValue.Name = "ntxConstValue";
            this.ntxConstValue.Size = new System.Drawing.Size(54, 20);
            this.ntxConstValue.TabIndex = 8;
            this.ntxConstValue.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Const:";
            // 
            // ntxRange
            // 
            this.ntxRange.Location = new System.Drawing.Point(44, 26);
            this.ntxRange.Name = "ntxRange";
            this.ntxRange.Size = new System.Drawing.Size(99, 20);
            this.ntxRange.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Range:";
            // 
            // CtrlAttenuation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ntxRange);
            this.Controls.Add(this.ntxSquareValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ntxLinearValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ntxConstValue);
            this.Controls.Add(this.label1);
            this.Name = "CtrlAttenuation";
            this.Size = new System.Drawing.Size(309, 47);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private MCTester.Controls.NumericTextBox ntxSquareValue;
		private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox ntxLinearValue;
		private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxConstValue;
		private System.Windows.Forms.Label label1;
        private MCTester.Controls.NumericTextBox ntxRange;
        private System.Windows.Forms.Label label4;
	}
}
