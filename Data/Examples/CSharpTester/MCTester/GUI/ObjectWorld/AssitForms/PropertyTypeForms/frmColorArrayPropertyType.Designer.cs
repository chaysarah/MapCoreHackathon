namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmColorArrayPropertyType
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
            this.ctrlColorArray = new MCTester.Controls.ColorArray();
            this.SuspendLayout();
            // 
            // ctrlColorArray
            // 
            this.ctrlColorArray.Location = new System.Drawing.Point(15, 67);
            this.ctrlColorArray.Name = "ctrlColorArray";
            this.ctrlColorArray.Size = new System.Drawing.Size(194, 248);
            this.ctrlColorArray.TabIndex = 3;
            // 
            // frmColorArrayPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(434, 320);
            this.Controls.Add(this.ctrlColorArray);
            this.Name = "frmColorArrayPropertyType";
            this.Text = "frmColorArrayPropertyType";
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.ctrlColorArray, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ColorArray ctrlColorArray;

    }
}