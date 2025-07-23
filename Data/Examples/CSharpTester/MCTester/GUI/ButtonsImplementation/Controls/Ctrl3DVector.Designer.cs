namespace MCTester.Controls
{
    partial class Ctrl3DVector
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
            this.txtZValue = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYValue = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXValue = new MCTester.Controls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtZValue
            // 
            this.txtZValue.Location = new System.Drawing.Point(171, 3);
            this.txtZValue.Name = "txtZValue";
            this.txtZValue.Size = new System.Drawing.Size(54, 20);
            this.txtZValue.TabIndex = 12;
            this.txtZValue.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Z:";
            // 
            // txtYValue
            // 
            this.txtYValue.Location = new System.Drawing.Point(95, 3);
            this.txtYValue.Name = "txtYValue";
            this.txtYValue.Size = new System.Drawing.Size(54, 20);
            this.txtYValue.TabIndex = 10;
            this.txtYValue.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y:";
            // 
            // txtXValue
            // 
            this.txtXValue.Location = new System.Drawing.Point(20, 3);
            this.txtXValue.Name = "txtXValue";
            this.txtXValue.Size = new System.Drawing.Size(54, 20);
            this.txtXValue.TabIndex = 8;
            this.txtXValue.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "X:";
            // 
            // Ctrl3DVector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtZValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtYValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtXValue);
            this.Controls.Add(this.label1);
            this.Name = "Ctrl3DVector";
            this.Size = new System.Drawing.Size(232, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.NumericTextBox txtZValue;
        private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox txtYValue;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox txtXValue;
        private System.Windows.Forms.Label label1;
    }
}
