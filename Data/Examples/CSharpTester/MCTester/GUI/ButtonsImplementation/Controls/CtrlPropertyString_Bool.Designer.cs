namespace MCTester.Controls
{
    partial class CtrlPropertyString_Bool
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
            this.lblSelString = new System.Windows.Forms.Label();
            this.txtSelText = new System.Windows.Forms.TextBox();
            this.chxSelBool = new System.Windows.Forms.CheckBox();
            this.lblRegString = new System.Windows.Forms.Label();
            this.chxRegBool = new System.Windows.Forms.CheckBox();
            this.txtRegText = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lblRegString);
            this.tpRegular.Controls.Add(this.txtRegText);
            this.tpRegular.Controls.Add(this.chxRegBool);
            this.tpRegular.Controls.SetChildIndex(this.chxRegBool, 0);
            this.tpRegular.Controls.SetChildIndex(this.txtRegText, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegString, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lblSelString);
            this.tpSelection.Controls.Add(this.txtSelText);
            this.tpSelection.Controls.Add(this.chxSelBool);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.chxSelBool, 0);
            this.tpSelection.Controls.SetChildIndex(this.txtSelText, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelString, 0);
            // 
            // lblSelString
            // 
            this.lblSelString.AutoSize = true;
            this.lblSelString.Location = new System.Drawing.Point(85, 54);
            this.lblSelString.Name = "lblSelString";
            this.lblSelString.Size = new System.Drawing.Size(60, 13);
            this.lblSelString.TabIndex = 28;
            this.lblSelString.Text = "Text Lable:";
            // 
            // txtSelText
            // 
            this.txtSelText.Location = new System.Drawing.Point(154, 51);
            this.txtSelText.Name = "txtSelText";
            this.txtSelText.Size = new System.Drawing.Size(198, 20);
            this.txtSelText.TabIndex = 30;
            // 
            // chxSelBool
            // 
            this.chxSelBool.AutoSize = true;
            this.chxSelBool.Location = new System.Drawing.Point(224, 30);
            this.chxSelBool.Name = "chxSelBool";
            this.chxSelBool.Size = new System.Drawing.Size(71, 17);
            this.chxSelBool.TabIndex = 29;
            this.chxSelBool.Text = "Bool Text";
            this.chxSelBool.UseVisualStyleBackColor = true;
            // 
            // lblRegString
            // 
            this.lblRegString.AutoSize = true;
            this.lblRegString.Location = new System.Drawing.Point(85, 54);
            this.lblRegString.Name = "lblRegString";
            this.lblRegString.Size = new System.Drawing.Size(60, 13);
            this.lblRegString.TabIndex = 25;
            this.lblRegString.Text = "Text Lable:";
            // 
            // chxRegBool
            // 
            this.chxRegBool.AutoSize = true;
            this.chxRegBool.Location = new System.Drawing.Point(224, 30);
            this.chxRegBool.Name = "chxRegBool";
            this.chxRegBool.Size = new System.Drawing.Size(71, 17);
            this.chxRegBool.TabIndex = 26;
            this.chxRegBool.Text = "Bool Text";
            this.chxRegBool.UseVisualStyleBackColor = true;
            // 
            // txtRegText
            // 
            this.txtRegText.Location = new System.Drawing.Point(154, 51);
            this.txtRegText.Name = "txtRegText";
            this.txtRegText.Size = new System.Drawing.Size(198, 20);
            this.txtRegText.TabIndex = 27;
            // 
            // CtrlPropertyString_Bool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyString_Bool";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSelString;
        private System.Windows.Forms.TextBox txtSelText;
        private System.Windows.Forms.CheckBox chxSelBool;
        private System.Windows.Forms.Label lblRegString;
        private System.Windows.Forms.TextBox txtRegText;
        private System.Windows.Forms.CheckBox chxRegBool;

    }
}
