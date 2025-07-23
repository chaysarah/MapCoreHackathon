namespace MCTester.Controls
{
    partial class CtrlSymbologySymbolID
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
            this.txtFullSymbolID = new System.Windows.Forms.TextBox();
            this.txtSymbolID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSymbologyStandard = new System.Windows.Forms.ComboBox();
            this.txtColorID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Full Symbol ID:";
            // 
            // txtFullSymbolID
            // 
            this.txtFullSymbolID.Location = new System.Drawing.Point(85, 72);
            this.txtFullSymbolID.Name = "txtFullSymbolID";
            this.txtFullSymbolID.Size = new System.Drawing.Size(262, 20);
            this.txtFullSymbolID.TabIndex = 38;
            // 
            // txtSymbolID
            // 
            this.txtSymbolID.Location = new System.Drawing.Point(85, 41);
            this.txtSymbolID.Name = "txtSymbolID";
            this.txtSymbolID.Size = new System.Drawing.Size(262, 20);
            this.txtSymbolID.TabIndex = 37;
            this.txtSymbolID.TextChanged += new System.EventHandler(this.txtSymbolID_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Symbol ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Symbology Standard";
            // 
            // cmbSymbologyStandard
            // 
            this.cmbSymbologyStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSymbologyStandard.FormattingEnabled = true;
            this.cmbSymbologyStandard.Location = new System.Drawing.Point(180, 10);
            this.cmbSymbologyStandard.Name = "cmbSymbologyStandard";
            this.cmbSymbologyStandard.Size = new System.Drawing.Size(167, 21);
            this.cmbSymbologyStandard.TabIndex = 40;
            this.cmbSymbologyStandard.SelectedIndexChanged += new System.EventHandler(this.cmbSymbologyStandard_SelectedIndexChanged);
            // 
            // txtColorID
            // 
            this.txtColorID.Location = new System.Drawing.Point(417, 41);
            this.txtColorID.Name = "txtColorID";
            this.txtColorID.Size = new System.Drawing.Size(42, 20);
            this.txtColorID.TabIndex = 43;
            this.txtColorID.TextChanged += new System.EventHandler(this.txtColorID_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(363, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Color ID:";
            // 
            // CtrlSymbologySymbolID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtColorID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSymbologyStandard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFullSymbolID);
            this.Controls.Add(this.txtSymbolID);
            this.Controls.Add(this.label3);
            this.Name = "CtrlSymbologySymbolID";
            this.Size = new System.Drawing.Size(464, 107);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFullSymbolID;
        private System.Windows.Forms.TextBox txtSymbolID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSymbologyStandard;
        private System.Windows.Forms.TextBox txtColorID;
        private System.Windows.Forms.Label label1;
    }
}
