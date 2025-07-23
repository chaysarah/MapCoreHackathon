namespace MCTester.Controls
{
    partial class Ctrl3DFVector
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtXValue = new MCTester.Controls.NumericTextBox();
            this.txtYValue = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZValue = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            //ITAY++((System.ComponentModel.ISupportInitialize)(this.txtXValue)).BeginInit();
            //ITAY++((System.ComponentModel.ISupportInitialize)(this.txtYValue)).BeginInit();
            //ITAY++((System.ComponentModel.ISupportInitialize)(this.txtZValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // txtXValue
            // 
            this.txtXValue.Location = new System.Drawing.Point(21, 3);
            this.txtXValue.Name = "txtXValue";
            this.txtXValue.Size = new System.Drawing.Size(54, 20);
            this.txtXValue.TabIndex = 2;
            this.txtXValue.Tag = null;
            this.txtXValue.Text = "0";
            // 
            // txtYValue
            // 
            this.txtYValue.Location = new System.Drawing.Point(96, 3);
            this.txtYValue.Name = "txtYValue";
            this.txtYValue.Size = new System.Drawing.Size(54, 20);
            this.txtYValue.TabIndex = 4;
            this.txtYValue.Tag = null;
            this.txtYValue.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y:";
            // 
            // txtZValue
            // 
            this.txtZValue.Location = new System.Drawing.Point(172, 3);
            this.txtZValue.Name = "txtZValue";
            this.txtZValue.Size = new System.Drawing.Size(54, 20);
            this.txtZValue.TabIndex = 6;
            this.txtZValue.Tag = null;
            this.txtZValue.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(154, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z:";
            // 
            // Ctrl3DFVector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtZValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtYValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtXValue);
            this.Controls.Add(this.label1);
            this.Name = "Ctrl3DFVector";
            this.Size = new System.Drawing.Size(232, 26);
            //ITAY++((System.ComponentModel.ISupportInitialize)(this.txtXValue)).EndInit();
            //ITAY++((System.ComponentModel.ISupportInitialize)(this.txtYValue)).EndInit();
            //ITAY++((System.ComponentModel.ISupportInitialize)(this.txtZValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MCTester.Controls.NumericTextBox txtXValue;
        private MCTester.Controls.NumericTextBox txtYValue;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox txtZValue;
        private System.Windows.Forms.Label label3;
    }
}
