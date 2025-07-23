namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmStringPropertyType
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
            this.ctrlString1 = new MCTester.Controls.CtrlString();
            this.SuspendLayout();
            // 
            // ntxPropertyID
            // 
            this.ntxPropertyID.Location = new System.Drawing.Point(81, 40);
            this.ntxPropertyID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // ctrlString1
            // 
            this.ctrlString1.IsVariantString = true;
            this.ctrlString1.Location = new System.Drawing.Point(14, 65);
            this.ctrlString1.Name = "ctrlString1";
            this.ctrlString1.Size = new System.Drawing.Size(267, 67);
            this.ctrlString1.StringNum = -1;
            this.ctrlString1.TabIndex = 3;
            this.ctrlString1.TextValue = "";
            // 
            // frmStringPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 136);
            this.Controls.Add(this.ctrlString1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmStringPropertyType";
            this.Text = "frmStringPropertyType";
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.ctrlString1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtrlString ctrlString1;
    }
}