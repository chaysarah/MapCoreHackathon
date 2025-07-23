namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmMeshPropertyType
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
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlMeshButtons1 = new MCTester.Controls.CtrlMeshButtons();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mesh:";
            // 
            // ctrlMeshButtons1
            // 
            this.ctrlMeshButtons1.Location = new System.Drawing.Point(54, 12);
            this.ctrlMeshButtons1.Name = "ctrlMeshButtons1";
            this.ctrlMeshButtons1.Size = new System.Drawing.Size(268, 21);
            this.ctrlMeshButtons1.TabIndex = 5;
            // 
            // frmMeshPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.ctrlMeshButtons1);
            this.Controls.Add(this.label2);
            this.Name = "frmMeshPropertyType";
            this.Text = "frmMeshPropertyType";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.ctrlMeshButtons1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private Controls.CtrlMeshButtons ctrlMeshButtons1;
    }
}