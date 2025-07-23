namespace MCTester.Controls
{
    partial class CtrlAnimation
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
            this.lblString = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.chxBool = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblString
            // 
            this.lblString.AutoSize = true;
            this.lblString.Location = new System.Drawing.Point(4, 4);
            this.lblString.Name = "lblString";
            this.lblString.Size = new System.Drawing.Size(38, 13);
            this.lblString.TabIndex = 28;
            this.lblString.Text = "Name:";
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(48, 1);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(175, 20);
            this.txtText.TabIndex = 30;
            // 
            // chxBool
            // 
            this.chxBool.AutoSize = true;
            this.chxBool.Location = new System.Drawing.Point(229, 3);
            this.chxBool.Name = "chxBool";
            this.chxBool.Size = new System.Drawing.Size(50, 17);
            this.chxBool.TabIndex = 29;
            this.chxBool.Text = "Loop";
            this.chxBool.UseVisualStyleBackColor = true;
            // 
            // CtrlAnimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblString);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.chxBool);
            this.Name = "CtrlAnimation";
            this.Size = new System.Drawing.Size(282, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblString;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.CheckBox chxBool;


    }
}
