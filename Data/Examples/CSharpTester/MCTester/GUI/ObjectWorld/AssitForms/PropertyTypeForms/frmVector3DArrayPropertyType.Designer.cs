namespace MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms
{
    partial class frmVector3DArrayPropertyType
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
            this.ctrlVector3DArray = new MCTester.Controls.Vector3DArray();
            this.SuspendLayout();
            // 
            // ctrlVector3DArray
            // 
            this.ctrlVector3DArray.Location = new System.Drawing.Point(12, 67);
            this.ctrlVector3DArray.Name = "ctrlVector3DArray";
            this.ctrlVector3DArray.Size = new System.Drawing.Size(318, 247);
            this.ctrlVector3DArray.TabIndex = 3;
            // 
            // frmVector3DArrayPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 342);
            this.Controls.Add(this.ctrlVector3DArray);
            this.Name = "frmVector3DArrayPropertyType";
            this.Text = "frmVector3DArrayPropertyType";
            this.Controls.SetChildIndex(this.ctrlVector3DArray, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.Vector3DArray ctrlVector3DArray;

    }
}