namespace MCTester.Controls
{
    partial class CtrlPropertyEnumColor
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
            this.picbRegColor = new System.Windows.Forms.PictureBox();
            this.lblRegAlpha = new System.Windows.Forms.Label();
            this.numUDRegAlphaColor = new System.Windows.Forms.NumericUpDown();
            this.picbSelColor = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numUDSelAlphaColor = new System.Windows.Forms.NumericUpDown();
            this.btnSetRegColor = new System.Windows.Forms.Button();
            this.btnGetRegColor = new System.Windows.Forms.Button();
            this.btnSetSelColor = new System.Windows.Forms.Button();
            this.btnGetSelColor = new System.Windows.Forms.Button();
            this.cbExistSelEnumColorValue = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbRegColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRegAlphaColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbSelColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSelAlphaColor)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(402, 159);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(396, 140);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.btnGetRegColor);
            this.tpRegular.Controls.Add(this.btnSetRegColor);
            this.tpRegular.Controls.Add(this.picbRegColor);
            this.tpRegular.Controls.Add(this.lblRegAlpha);
            this.tpRegular.Controls.Add(this.numUDRegAlphaColor);
            this.tpRegular.Size = new System.Drawing.Size(388, 114);
            this.tpRegular.Controls.SetChildIndex(this.cmbRegEnum, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            this.tpRegular.Controls.SetChildIndex(this.numUDRegAlphaColor, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegAlpha, 0);
            this.tpRegular.Controls.SetChildIndex(this.picbRegColor, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnSetRegColor, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnGetRegColor, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.cbExistSelEnumColorValue);
            this.tpSelection.Controls.Add(this.btnGetSelColor);
            this.tpSelection.Controls.Add(this.btnSetSelColor);
            this.tpSelection.Controls.Add(this.picbSelColor);
            this.tpSelection.Controls.Add(this.label2);
            this.tpSelection.Controls.Add(this.numUDSelAlphaColor);
            this.tpSelection.Size = new System.Drawing.Size(388, 114);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.cmbSelEnum, 0);
            this.tpSelection.Controls.SetChildIndex(this.numUDSelAlphaColor, 0);
            this.tpSelection.Controls.SetChildIndex(this.label2, 0);
            this.tpSelection.Controls.SetChildIndex(this.picbSelColor, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSetSelColor, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnGetSelColor, 0);
            this.tpSelection.Controls.SetChildIndex(this.cbExistSelEnumColorValue, 0);
            // 
            // picbRegColor
            // 
            this.picbRegColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbRegColor.Location = new System.Drawing.Point(240, 27);
            this.picbRegColor.Name = "picbRegColor";
            this.picbRegColor.Size = new System.Drawing.Size(25, 22);
            this.picbRegColor.TabIndex = 27;
            this.picbRegColor.TabStop = false;
            this.picbRegColor.Click += new System.EventHandler(this.picbRegColor_Click);
            // 
            // lblRegAlpha
            // 
            this.lblRegAlpha.AutoSize = true;
            this.lblRegAlpha.Location = new System.Drawing.Point(271, 31);
            this.lblRegAlpha.Name = "lblRegAlpha";
            this.lblRegAlpha.Size = new System.Drawing.Size(37, 13);
            this.lblRegAlpha.TabIndex = 26;
            this.lblRegAlpha.Text = "Alpha:";
            // 
            // numUDRegAlphaColor
            // 
            this.numUDRegAlphaColor.Location = new System.Drawing.Point(314, 29);
            this.numUDRegAlphaColor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDRegAlphaColor.Name = "numUDRegAlphaColor";
            this.numUDRegAlphaColor.Size = new System.Drawing.Size(57, 20);
            this.numUDRegAlphaColor.TabIndex = 25;
            this.numUDRegAlphaColor.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDRegAlphaColor.ValueChanged += new System.EventHandler(this.RegNumUDAlphaColor_ValueChanged);
            // 
            // picbSelColor
            // 
            this.picbSelColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbSelColor.Location = new System.Drawing.Point(240, 27);
            this.picbSelColor.Name = "picbSelColor";
            this.picbSelColor.Size = new System.Drawing.Size(25, 22);
            this.picbSelColor.TabIndex = 31;
            this.picbSelColor.TabStop = false;
            this.picbSelColor.Click += new System.EventHandler(this.picbSelColor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Alpha:";
            // 
            // numUDSelAlphaColor
            // 
            this.numUDSelAlphaColor.Location = new System.Drawing.Point(314, 29);
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
            // btnSetRegColor
            // 
            this.btnSetRegColor.Location = new System.Drawing.Point(154, 78);
            this.btnSetRegColor.Name = "btnSetRegColor";
            this.btnSetRegColor.Size = new System.Drawing.Size(64, 23);
            this.btnSetRegColor.TabIndex = 28;
            this.btnSetRegColor.Text = "Set";
            this.btnSetRegColor.UseVisualStyleBackColor = true;
            // 
            // btnGetRegColor
            // 
            this.btnGetRegColor.Location = new System.Drawing.Point(240, 78);
            this.btnGetRegColor.Name = "btnGetRegColor";
            this.btnGetRegColor.Size = new System.Drawing.Size(68, 23);
            this.btnGetRegColor.TabIndex = 29;
            this.btnGetRegColor.Text = "Get";
            this.btnGetRegColor.UseVisualStyleBackColor = true;
            // 
            // btnSetSelColor
            // 
            this.btnSetSelColor.Location = new System.Drawing.Point(154, 78);
            this.btnSetSelColor.Name = "btnSetSelColor";
            this.btnSetSelColor.Size = new System.Drawing.Size(64, 23);
            this.btnSetSelColor.TabIndex = 32;
            this.btnSetSelColor.Text = "Set";
            this.btnSetSelColor.UseVisualStyleBackColor = true;
            // 
            // btnGetSelColor
            // 
            this.btnGetSelColor.Location = new System.Drawing.Point(240, 78);
            this.btnGetSelColor.Name = "btnGetSelColor";
            this.btnGetSelColor.Size = new System.Drawing.Size(68, 23);
            this.btnGetSelColor.TabIndex = 33;
            this.btnGetSelColor.Text = "Get";
            this.btnGetSelColor.UseVisualStyleBackColor = true;
            // 
            // cbExistSelEnumColorValue
            // 
            this.cbExistSelEnumColorValue.AutoSize = true;
            this.cbExistSelEnumColorValue.Location = new System.Drawing.Point(7, 6);
            this.cbExistSelEnumColorValue.Name = "cbExistSelEnumColorValue";
            this.cbExistSelEnumColorValue.Size = new System.Drawing.Size(193, 17);
            this.cbExistSelEnumColorValue.TabIndex = 34;
            this.cbExistSelEnumColorValue.Text = "Is Exist Selection Enum Color Value";
            this.cbExistSelEnumColorValue.UseVisualStyleBackColor = true;
            // 
            // CtrlPropertyEnumColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyEnumColor";
            this.Size = new System.Drawing.Size(402, 159);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbRegColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDRegAlphaColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbSelColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDSelAlphaColor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picbRegColor;
        private System.Windows.Forms.Label lblRegAlpha;
        private System.Windows.Forms.NumericUpDown numUDRegAlphaColor;
        private System.Windows.Forms.PictureBox picbSelColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numUDSelAlphaColor;
        public System.Windows.Forms.Button btnGetRegColor;
        public System.Windows.Forms.Button btnSetRegColor;
        public System.Windows.Forms.Button btnSetSelColor;
        public System.Windows.Forms.Button btnGetSelColor;
        public System.Windows.Forms.CheckBox cbExistSelEnumColorValue;
    }
}
