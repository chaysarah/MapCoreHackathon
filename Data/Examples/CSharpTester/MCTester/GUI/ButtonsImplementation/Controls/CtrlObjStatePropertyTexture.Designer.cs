namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyTexture
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
            this.ctrlRegTextureButtons = new MCTester.Controls.CtrlTextureButtons();
            this.ctrlSelTextureButtons = new MCTester.Controls.CtrlTextureButtons();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegTextureButtons);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegTextureButtons, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelTextureButtons);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelTextureButtons, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(4);
            // 
            // ctrlRegTextureButtons
            // 
            this.ctrlRegTextureButtons.Location = new System.Drawing.Point(76, 46);
            this.ctrlRegTextureButtons.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlRegTextureButtons.Name = "ctrlRegTextureButtons";
            this.ctrlRegTextureButtons.objectPropertiesBase_TextureField = MCTester.Controls.CtrlTextureButtons.ObjectPropertiesBase_TextureField.DefaultTexture;
            this.ctrlRegTextureButtons.Size = new System.Drawing.Size(262, 23);
            this.ctrlRegTextureButtons.TabIndex = 42;
            // 
            // ctrlSelTextureButtons
            // 
            this.ctrlSelTextureButtons.Location = new System.Drawing.Point(76, 46);
            this.ctrlSelTextureButtons.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlSelTextureButtons.Name = "ctrlSelTextureButtons";
            this.ctrlSelTextureButtons.objectPropertiesBase_TextureField = MCTester.Controls.CtrlTextureButtons.ObjectPropertiesBase_TextureField.DefaultTexture;
            this.ctrlSelTextureButtons.Size = new System.Drawing.Size(258, 24);
            this.ctrlSelTextureButtons.TabIndex = 37;
            // 
            // CtrlObjStatePropertyTexture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyTexture";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlTextureButtons ctrlRegTextureButtons;
        private CtrlTextureButtons ctrlSelTextureButtons;
    }
}
