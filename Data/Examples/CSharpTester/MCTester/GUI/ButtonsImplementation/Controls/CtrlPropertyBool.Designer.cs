namespace MCTester.Controls
{
    partial class CtrlPropertyBool
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
            this.chxRegBool = new System.Windows.Forms.CheckBox();
            this.chxSelBool = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.chxRegBool);
            this.tpRegular.Controls.SetChildIndex(this.chxRegBool, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.chxSelBool);
            this.tpSelection.Controls.SetChildIndex(this.chxSelBool, 0);
            // 
            // chxRegBool
            // 
            this.chxRegBool.AutoSize = true;
            this.chxRegBool.Checked = true;
            this.chxRegBool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxRegBool.Location = new System.Drawing.Point(88, 54);
            this.chxRegBool.Name = "chxRegBool";
            this.chxRegBool.Size = new System.Drawing.Size(71, 17);
            this.chxRegBool.TabIndex = 18;
            this.chxRegBool.Text = "Bool Text";
            this.chxRegBool.UseVisualStyleBackColor = true;
            // 
            // chxSelBool
            // 
            this.chxSelBool.AutoSize = true;
            this.chxSelBool.Checked = true;
            this.chxSelBool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSelBool.Location = new System.Drawing.Point(88, 54);
            this.chxSelBool.Name = "chxSelBool";
            this.chxSelBool.Size = new System.Drawing.Size(71, 17);
            this.chxSelBool.TabIndex = 27;
            this.chxSelBool.Text = "Bool Text";
            this.chxSelBool.UseVisualStyleBackColor = true;
            // 
            // CtrlPropertyBool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyBool";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.CheckBox chxRegBool;
        protected System.Windows.Forms.CheckBox chxSelBool;
    }
}
