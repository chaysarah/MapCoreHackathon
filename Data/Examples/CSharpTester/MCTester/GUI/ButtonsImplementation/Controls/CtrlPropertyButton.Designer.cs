namespace MCTester.Controls
{
    partial class CtrlPropertyButton
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
            this.lblRegButton = new System.Windows.Forms.Label();
            this.btnRegFunction = new System.Windows.Forms.Button();
            this.lblSelButton = new System.Windows.Forms.Label();
            this.btnSelFunction = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(400, 130);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(394, 111);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lblRegButton);
            this.tpRegular.Controls.Add(this.btnRegFunction);
            this.tpRegular.Size = new System.Drawing.Size(386, 85);
            this.tpRegular.Controls.SetChildIndex(this.btnRegFunction, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegButton, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lblSelButton);
            this.tpSelection.Controls.Add(this.btnSelFunction);
            this.tpSelection.Controls.SetChildIndex(this.btnSelFunction, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelButton, 0);
            // 
            // lblRegButton
            // 
            this.lblRegButton.AutoSize = true;
            this.lblRegButton.Location = new System.Drawing.Point(85, 56);
            this.lblRegButton.Name = "lblRegButton";
            this.lblRegButton.Size = new System.Drawing.Size(70, 13);
            this.lblRegButton.TabIndex = 18;
            this.lblRegButton.Text = "Lable Button:";
            // 
            // btnRegFunction
            // 
            this.btnRegFunction.Location = new System.Drawing.Point(161, 51);
            this.btnRegFunction.Name = "btnRegFunction";
            this.btnRegFunction.Size = new System.Drawing.Size(75, 23);
            this.btnRegFunction.TabIndex = 19;
            this.btnRegFunction.Text = "Function";
            this.btnRegFunction.UseVisualStyleBackColor = true;
            this.btnRegFunction.Click += new System.EventHandler(this.btnRegFunction_Click);
            // 
            // lblSelButton
            // 
            this.lblSelButton.AutoSize = true;
            this.lblSelButton.Location = new System.Drawing.Point(85, 56);
            this.lblSelButton.Name = "lblSelButton";
            this.lblSelButton.Size = new System.Drawing.Size(70, 13);
            this.lblSelButton.TabIndex = 27;
            this.lblSelButton.Text = "Lable Button:";
            // 
            // btnSelFunction
            // 
            this.btnSelFunction.Location = new System.Drawing.Point(161, 51);
            this.btnSelFunction.Name = "btnSelFunction";
            this.btnSelFunction.Size = new System.Drawing.Size(75, 23);
            this.btnSelFunction.TabIndex = 28;
            this.btnSelFunction.Text = "Function";
            this.btnSelFunction.UseVisualStyleBackColor = true;
            this.btnSelFunction.Click += new System.EventHandler(this.btnSelFunction_Click);
            // 
            // CtrlPropertyButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyButton";
            this.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRegButton;
        private System.Windows.Forms.Button btnRegFunction;
        private System.Windows.Forms.Label lblSelButton;
        private System.Windows.Forms.Button btnSelFunction;
    }
}
