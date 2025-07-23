namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmTexturePropertyType
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
            this.ctrlTextureButtons1 = new MCTester.Controls.CtrlTextureButtons();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Texture:";
            // 
            // ctrlTextureButtons1
            // 
            this.ctrlTextureButtons1.Location = new System.Drawing.Point(81, 7);
            this.ctrlTextureButtons1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlTextureButtons1.Name = "ctrlTextureButtons1";
            this.ctrlTextureButtons1.objectPropertiesBase_TextureField = MCTester.Controls.CtrlTextureButtons.ObjectPropertiesBase_TextureField.DefaultTexture;
            this.ctrlTextureButtons1.Size = new System.Drawing.Size(262, 29);
            this.ctrlTextureButtons1.TabIndex = 5;
            // 
            // frmTexturePropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.ctrlTextureButtons1);
            this.Controls.Add(this.label2);
            this.Name = "frmTexturePropertyType";
            this.Text = "frmTexturePropertyType";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.ctrlTextureButtons1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private Controls.CtrlTextureButtons ctrlTextureButtons1;
    }
}