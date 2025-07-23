namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmVector3DPropertyType
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
            this.ctrl3DVectorPropertyValue = new MCTester.Controls.Ctrl3DVector();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "3D Vector:";
            // 
            // ctrl3DVectorPropertyValue
            // 
            this.ctrl3DVectorPropertyValue.Location = new System.Drawing.Point(81, 9);
            this.ctrl3DVectorPropertyValue.Name = "ctrl3DVectorPropertyValue";
            this.ctrl3DVectorPropertyValue.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorPropertyValue.TabIndex = 4;
            this.ctrl3DVectorPropertyValue.X = 0;
            this.ctrl3DVectorPropertyValue.Y = 0;
            this.ctrl3DVectorPropertyValue.Z = 0;
            // 
            // frmVector3DPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.ctrl3DVectorPropertyValue);
            this.Controls.Add(this.label2);
            this.Name = "frmVector3DPropertyType";
            this.Text = "frmVector3DPropertyType";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ctrl3DVectorPropertyValue, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private MCTester.Controls.Ctrl3DVector ctrl3DVectorPropertyValue;
    }
}