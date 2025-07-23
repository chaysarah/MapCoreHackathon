namespace MCTester.Controls
{
    partial class CtrlPropertyColor
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
            this.numUDRegAlphaColor = new System.Windows.Forms.NumericUpDown();
            this.lblRegAlpha = new System.Windows.Forms.Label();
            this.picbRegColor = new System.Windows.Forms.PictureBox();
            this.picbSelColor = new System.Windows.Forms.PictureBox();
            this.lblSelAlpha = new System.Windows.Forms.Label();
            this.numUDSelAlphaColor = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRegAlphaColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbRegColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbSelColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSelAlphaColor)).BeginInit();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.picbRegColor);
            this.tpRegular.Controls.Add(this.lblRegAlpha);
            this.tpRegular.Controls.Add(this.numUDRegAlphaColor);
            this.tpRegular.Controls.SetChildIndex(this.numUDRegAlphaColor, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegAlpha, 0);
            this.tpRegular.Controls.SetChildIndex(this.picbRegColor, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.picbSelColor);
            this.tpSelection.Controls.Add(this.lblSelAlpha);
            this.tpSelection.Controls.Add(this.numUDSelAlphaColor);
            this.tpSelection.Controls.SetChildIndex(this.numUDSelAlphaColor, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelAlpha, 0);
            this.tpSelection.Controls.SetChildIndex(this.picbSelColor, 0);
            // 
            // numUDRegAlphaColor
            // 
            this.numUDRegAlphaColor.Location = new System.Drawing.Point(316, 54);
            this.numUDRegAlphaColor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDRegAlphaColor.Name = "numUDRegAlphaColor";
            this.numUDRegAlphaColor.Size = new System.Drawing.Size(57, 20);
            this.numUDRegAlphaColor.TabIndex = 20;
            this.numUDRegAlphaColor.ValueChanged += new System.EventHandler(this.RegNumUDAlphaColor_ValueChanged);
            // 
            // lblRegAlpha
            // 
            this.lblRegAlpha.AutoSize = true;
            this.lblRegAlpha.Location = new System.Drawing.Point(273, 56);
            this.lblRegAlpha.Name = "lblRegAlpha";
            this.lblRegAlpha.Size = new System.Drawing.Size(37, 13);
            this.lblRegAlpha.TabIndex = 21;
            this.lblRegAlpha.Text = "Alpha:";
            // 
            // picbRegColor
            // 
            this.picbRegColor.Location = new System.Drawing.Point(242, 52);
            this.picbRegColor.Name = "picbRegColor";
            this.picbRegColor.Size = new System.Drawing.Size(25, 22);
            this.picbRegColor.TabIndex = 22;
            this.picbRegColor.TabStop = false;
            // 
            // picbSelColor
            // 
            this.picbSelColor.Location = new System.Drawing.Point(242, 52);
            this.picbSelColor.Name = "picbSelColor";
            this.picbSelColor.Size = new System.Drawing.Size(25, 22);
            this.picbSelColor.TabIndex = 31;
            this.picbSelColor.TabStop = false;
            // 
            // lblSelAlpha
            // 
            this.lblSelAlpha.AutoSize = true;
            this.lblSelAlpha.Location = new System.Drawing.Point(273, 56);
            this.lblSelAlpha.Name = "lblSelAlpha";
            this.lblSelAlpha.Size = new System.Drawing.Size(37, 13);
            this.lblSelAlpha.TabIndex = 30;
            this.lblSelAlpha.Text = "Alpha:";
            // 
            // numUDSelAlphaColor
            // 
            this.numUDSelAlphaColor.Location = new System.Drawing.Point(316, 54);
            this.numUDSelAlphaColor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDSelAlphaColor.Name = "numUDSelAlphaColor";
            this.numUDSelAlphaColor.Size = new System.Drawing.Size(57, 20);
            this.numUDSelAlphaColor.TabIndex = 29;
            this.numUDSelAlphaColor.ValueChanged += new System.EventHandler(this.SelNumUDAlphaColor_ValueChanged);
            // 
            // CtrlPropertyColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyColor";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRegAlphaColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbRegColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbSelColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSelAlphaColor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numUDRegAlphaColor;
        private System.Windows.Forms.PictureBox picbRegColor;
        private System.Windows.Forms.Label lblRegAlpha;
        private System.Windows.Forms.PictureBox picbSelColor;
        private System.Windows.Forms.Label lblSelAlpha;
        private System.Windows.Forms.NumericUpDown numUDSelAlphaColor;
    }
}
