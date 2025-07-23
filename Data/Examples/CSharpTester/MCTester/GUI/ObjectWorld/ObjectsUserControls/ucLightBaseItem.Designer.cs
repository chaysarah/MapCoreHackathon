namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucLightBaseItem
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
            this.Tab_LightBaseItem = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertySpecularColor = new MCTester.Controls.CtrlObjStatePropertyFColor();
            this.ctrlObjStatePropertyDiffuseColor = new MCTester.Controls.CtrlObjStatePropertyFColor();
            this.tabControl1.SuspendLayout();
            this.Tab_LightBaseItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_LightBaseItem);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LightBaseItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_LightBaseItem
            // 
            this.Tab_LightBaseItem.Controls.Add(this.ctrlObjStatePropertySpecularColor);
            this.Tab_LightBaseItem.Controls.Add(this.ctrlObjStatePropertyDiffuseColor);
            this.Tab_LightBaseItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_LightBaseItem.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_LightBaseItem.Name = "Tab_LightBaseItem";
            this.Tab_LightBaseItem.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_LightBaseItem.Size = new System.Drawing.Size(1109, 857);
            this.Tab_LightBaseItem.TabIndex = 3;
            this.Tab_LightBaseItem.Text = "Light Item";
            this.Tab_LightBaseItem.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertySpecularColor
            // 
            this.ctrlObjStatePropertySpecularColor.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySpecularColor.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySpecularColor.IsClickApply = false;
            this.ctrlObjStatePropertySpecularColor.IsClickOK = false;
            this.ctrlObjStatePropertySpecularColor.Location = new System.Drawing.Point(542, 6);
            this.ctrlObjStatePropertySpecularColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySpecularColor.Name = "ctrlObjStatePropertySpecularColor";
            this.ctrlObjStatePropertySpecularColor.PropertyName = "Specular Color";
            this.ctrlObjStatePropertySpecularColor.PropertyValidationResult = null;
            this.ctrlObjStatePropertySpecularColor.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySpecularColor.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertySpecularColor.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySpecularColor.TabIndex = 3;
            // 
            // ctrlObjStatePropertyDiffuseColor
            // 
            this.ctrlObjStatePropertyDiffuseColor.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyDiffuseColor.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyDiffuseColor.IsClickApply = false;
            this.ctrlObjStatePropertyDiffuseColor.IsClickOK = false;
            this.ctrlObjStatePropertyDiffuseColor.Location = new System.Drawing.Point(3, 6);
            this.ctrlObjStatePropertyDiffuseColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyDiffuseColor.Name = "ctrlObjStatePropertyDiffuseColor";
            this.ctrlObjStatePropertyDiffuseColor.PropertyName = "Diffuse Color";
            this.ctrlObjStatePropertyDiffuseColor.PropertyValidationResult = null;
            this.ctrlObjStatePropertyDiffuseColor.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyDiffuseColor.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyDiffuseColor.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyDiffuseColor.TabIndex = 2;
            // 
            // ucLightBaseItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucLightBaseItem";
            this.tabControl1.ResumeLayout(false);
            this.Tab_LightBaseItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TabPage Tab_LightBaseItem;
        private Controls.CtrlObjStatePropertyFColor ctrlObjStatePropertySpecularColor;
        private Controls.CtrlObjStatePropertyFColor ctrlObjStatePropertyDiffuseColor;
    }
}
