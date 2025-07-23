namespace MCTester.Controls
{
    partial class SelectColor
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
            this.nudAlpha = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.picbColor = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbColor)).BeginInit();
            this.SuspendLayout();
            // 
            // nudAlpha
            // 
            this.nudAlpha.Location = new System.Drawing.Point(69, 3);
            this.nudAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAlpha.Name = "nudAlpha";
            this.nudAlpha.Size = new System.Drawing.Size(49, 20);
            this.nudAlpha.TabIndex = 29;
            this.nudAlpha.ValueChanged += new System.EventHandler(this.nudAlpha_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Alpha:";
            // 
            // picbColor
            // 
            this.picbColor.Location = new System.Drawing.Point(4, 1);
            this.picbColor.Name = "picbColor";
            this.picbColor.Size = new System.Drawing.Size(25, 22);
            this.picbColor.TabIndex = 30;
            this.picbColor.TabStop = false;
            this.picbColor.Click += new System.EventHandler(this.picbColor_Click);
            // 
            // SelectColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picbColor);
            this.Controls.Add(this.nudAlpha);
            this.Controls.Add(this.label4);
            this.Name = "SelectColor";
            this.Size = new System.Drawing.Size(129, 25);
            ((System.ComponentModel.ISupportInitialize)(this.nudAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.PictureBox picbColor;
        public System.Windows.Forms.NumericUpDown nudAlpha;
    }
}
