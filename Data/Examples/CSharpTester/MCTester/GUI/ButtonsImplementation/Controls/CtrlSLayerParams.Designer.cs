namespace MCTester.Controls
{
    partial class CtrlSLayerParams
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chxVisibility = new System.Windows.Forms.CheckBox();
            this.ntxTransparency = new MCTester.Controls.NumericTextBox();
            this.ntxDrawPriority = new MCTester.Controls.NumericTextBox();
            this.ntxMaxScaleVisibility = new MCTester.Controls.NumericTextBox();
            this.ntxMinScaleVisibility = new MCTester.Controls.NumericTextBox();
            this.chbNearestPixelMagFilter = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Min Scale Visibility:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max Scale Visibility:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Draw Priority:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 135);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Transparency:";
            // 
            // chxVisibility
            // 
            this.chxVisibility.AutoSize = true;
            this.chxVisibility.Location = new System.Drawing.Point(8, 4);
            this.chxVisibility.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chxVisibility.Name = "chxVisibility";
            this.chxVisibility.Size = new System.Drawing.Size(80, 21);
            this.chxVisibility.TabIndex = 8;
            this.chxVisibility.Text = "Visibility";
            this.chxVisibility.UseVisualStyleBackColor = true;
            // 
            // ntxTransparency
            // 
            this.ntxTransparency.Location = new System.Drawing.Point(144, 132);
            this.ntxTransparency.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxTransparency.Name = "ntxTransparency";
            this.ntxTransparency.Size = new System.Drawing.Size(132, 22);
            this.ntxTransparency.TabIndex = 6;
            // 
            // ntxDrawPriority
            // 
            this.ntxDrawPriority.Location = new System.Drawing.Point(144, 100);
            this.ntxDrawPriority.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxDrawPriority.Name = "ntxDrawPriority";
            this.ntxDrawPriority.Size = new System.Drawing.Size(132, 22);
            this.ntxDrawPriority.TabIndex = 2;
            // 
            // ntxMaxScaleVisibility
            // 
            this.ntxMaxScaleVisibility.Location = new System.Drawing.Point(144, 68);
            this.ntxMaxScaleVisibility.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxMaxScaleVisibility.Name = "ntxMaxScaleVisibility";
            this.ntxMaxScaleVisibility.Size = new System.Drawing.Size(132, 22);
            this.ntxMaxScaleVisibility.TabIndex = 1;
            // 
            // ntxMinScaleVisibility
            // 
            this.ntxMinScaleVisibility.Location = new System.Drawing.Point(144, 36);
            this.ntxMinScaleVisibility.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ntxMinScaleVisibility.Name = "ntxMinScaleVisibility";
            this.ntxMinScaleVisibility.Size = new System.Drawing.Size(132, 22);
            this.ntxMinScaleVisibility.TabIndex = 0;
            // 
            // chbNearestPixelMagFilter
            // 
            this.chbNearestPixelMagFilter.AutoSize = true;
            this.chbNearestPixelMagFilter.Location = new System.Drawing.Point(8, 165);
            this.chbNearestPixelMagFilter.Margin = new System.Windows.Forms.Padding(4);
            this.chbNearestPixelMagFilter.Name = "chbNearestPixelMagFilter";
            this.chbNearestPixelMagFilter.Size = new System.Drawing.Size(183, 21);
            this.chbNearestPixelMagFilter.TabIndex = 9;
            this.chbNearestPixelMagFilter.Text = "Nearest Pixel Mag Filter ";
            this.chbNearestPixelMagFilter.UseVisualStyleBackColor = true;
            // 
            // CtrlSLayerParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbNearestPixelMagFilter);
            this.Controls.Add(this.chxVisibility);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ntxTransparency);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ntxDrawPriority);
            this.Controls.Add(this.ntxMaxScaleVisibility);
            this.Controls.Add(this.ntxMinScaleVisibility);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CtrlSLayerParams";
            this.Size = new System.Drawing.Size(285, 191);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.NumericTextBox ntxMinScaleVisibility;
        private MCTester.Controls.NumericTextBox ntxMaxScaleVisibility;
        private MCTester.Controls.NumericTextBox ntxDrawPriority;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private MCTester.Controls.NumericTextBox ntxTransparency;
        private System.Windows.Forms.CheckBox chxVisibility;
        private System.Windows.Forms.CheckBox chbNearestPixelMagFilter;
    }
}
