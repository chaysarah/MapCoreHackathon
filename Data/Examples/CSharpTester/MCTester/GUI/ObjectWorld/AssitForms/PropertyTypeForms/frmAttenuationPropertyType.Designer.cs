namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmAttenuationPropertyType
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
            this.ctrlAttenuationType = new MCTester.Controls.CtrlAttenuation();
            this.SuspendLayout();
            // 
            // ctrlAttenuationType
            // 
            this.ctrlAttenuationType.Const = 0F;
            this.ctrlAttenuationType.Linear = 0F;
            this.ctrlAttenuationType.Location = new System.Drawing.Point(12, 67);
            this.ctrlAttenuationType.Name = "ctrlAttenuationType";
            this.ctrlAttenuationType.Range = 0F;
            this.ctrlAttenuationType.Size = new System.Drawing.Size(309, 47);
            this.ctrlAttenuationType.Square = 0F;
            this.ctrlAttenuationType.TabIndex = 3;
            // 
            // frmAttenuationPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 121);
            this.Controls.Add(this.ctrlAttenuationType);
            this.Name = "frmAttenuationPropertyType";
            this.Text = "frmAttenuationPropertyType";
            this.Controls.SetChildIndex(this.ctrlAttenuationType, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.CtrlAttenuation ctrlAttenuationType;
    }
}