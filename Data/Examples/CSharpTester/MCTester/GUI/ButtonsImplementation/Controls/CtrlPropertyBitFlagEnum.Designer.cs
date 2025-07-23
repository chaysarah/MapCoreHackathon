namespace MCTester.Controls
{
    partial class CtrlPropertyBitFlagEnum
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
            this.lblRegEnum = new System.Windows.Forms.Label();
            this.lblSelEnum = new System.Windows.Forms.Label();
            this.lstRegEnum = new System.Windows.Forms.CheckedListBox();
            this.lstSelEnum = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(533, 217);
            // 
            // chxSelectionProperty
            // 
            this.chxSelectionProperty.CheckedChanged += new System.EventHandler(this.chxSelectionProperty_CheckedChanged);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 19);
            this.tcProperty.Size = new System.Drawing.Size(525, 194);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lstRegEnum);
            this.tpRegular.Controls.Add(this.lblRegEnum);
            this.tpRegular.Location = new System.Drawing.Point(4, 25);
            this.tpRegular.Size = new System.Drawing.Size(517, 165);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegEnum, 0);
            this.tpRegular.Controls.SetChildIndex(this.lstRegEnum, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lstSelEnum);
            this.tpSelection.Controls.Add(this.lblSelEnum);
            this.tpSelection.Location = new System.Drawing.Point(4, 25);
            this.tpSelection.Size = new System.Drawing.Size(517, 165);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelEnum, 0);
            this.tpSelection.Controls.SetChildIndex(this.lstSelEnum, 0);
            // 
            // lblRegEnum
            // 
            this.lblRegEnum.AutoSize = true;
            this.lblRegEnum.Location = new System.Drawing.Point(113, 66);
            this.lblRegEnum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegEnum.Name = "lblRegEnum";
            this.lblRegEnum.Size = new System.Drawing.Size(87, 17);
            this.lblRegEnum.TabIndex = 25;
            this.lblRegEnum.Text = "Enum Lable:";
            // 
            // lblSelEnum
            // 
            this.lblSelEnum.AutoSize = true;
            this.lblSelEnum.Location = new System.Drawing.Point(113, 66);
            this.lblSelEnum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelEnum.Name = "lblSelEnum";
            this.lblSelEnum.Size = new System.Drawing.Size(87, 17);
            this.lblSelEnum.TabIndex = 28;
            this.lblSelEnum.Text = "Enum Lable:";
            // 
            // lstRegEnum
            // 
            this.lstRegEnum.FormattingEnabled = true;
            this.lstRegEnum.Location = new System.Drawing.Point(207, 69);
            this.lstRegEnum.Name = "lstRegEnum";
            this.lstRegEnum.Size = new System.Drawing.Size(303, 89);
            this.lstRegEnum.TabIndex = 26;
            this.lstRegEnum.UseCompatibleTextRendering = true;
            // 
            // lstSelEnum
            // 
            this.lstSelEnum.FormattingEnabled = true;
            this.lstSelEnum.Location = new System.Drawing.Point(205, 66);
            this.lstSelEnum.Name = "lstSelEnum";
            this.lstSelEnum.Size = new System.Drawing.Size(303, 89);
            this.lstSelEnum.TabIndex = 29;
            this.lstSelEnum.UseCompatibleTextRendering = true;
            // 
            // CtrlPropertyBitFlagEnum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyBitFlagEnum";
            this.Size = new System.Drawing.Size(533, 217);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRegEnum;
        private System.Windows.Forms.CheckedListBox lstRegEnum;
        private System.Windows.Forms.CheckedListBox lstSelEnum;
        private System.Windows.Forms.Label lblSelEnum;
    }
}
