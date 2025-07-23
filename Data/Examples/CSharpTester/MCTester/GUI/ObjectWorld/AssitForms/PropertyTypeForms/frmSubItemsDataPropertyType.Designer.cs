namespace MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms
{
    partial class frmSubItemsDataPropertyType
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
            this.ctrlSubItemsData1 = new MCTester.Controls.CtrlSubItemsData();
            this.SuspendLayout();
            // 
            // ctrlSubItemsData1
            // 
            this.ctrlSubItemsData1.Location = new System.Drawing.Point(10, 78);
            this.ctrlSubItemsData1.Name = "ctrlSubItemsData1";
            this.ctrlSubItemsData1.Size = new System.Drawing.Size(303, 58);
            this.ctrlSubItemsData1.TabIndex = 7;
            // 
            // frmSubItemsDataPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 155);
            this.Controls.Add(this.ctrlSubItemsData1);
            this.Name = "frmSubItemsDataPropertyType";
            this.Text = "frmSubItemsDataPropertyType";
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.ctrlSubItemsData1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Controls.CtrlSubItemsData ctrlSubItemsData1;
    }
}