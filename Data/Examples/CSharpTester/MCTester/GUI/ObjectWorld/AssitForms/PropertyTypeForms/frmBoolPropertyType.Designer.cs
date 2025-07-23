namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmBoolPropertyType
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chxBoolPropertyValue = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chxBoolPropertyValue
            // 
            this.chxBoolPropertyValue.AutoSize = true;
            this.chxBoolPropertyValue.Location = new System.Drawing.Point(15, 18);
            this.chxBoolPropertyValue.Name = "chxBoolPropertyValue";
            this.chxBoolPropertyValue.Size = new System.Drawing.Size(80, 17);
            this.chxBoolPropertyValue.TabIndex = 3;
            this.chxBoolPropertyValue.Text = "Is Checked";
            this.chxBoolPropertyValue.UseVisualStyleBackColor = true;
            // 
            // frmBoolPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.chxBoolPropertyValue);
            this.Name = "frmBoolPropertyType";
            this.Text = "frmBoolPropertyType";
            this.Controls.SetChildIndex(this.chxBoolPropertyValue, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chxBoolPropertyValue;
    }
}