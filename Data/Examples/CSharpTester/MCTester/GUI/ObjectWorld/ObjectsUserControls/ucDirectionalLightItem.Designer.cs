namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucDirectionalLightItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDirectionalLightItem));
            this.ctrlPropertyDirection = new MCTester.Controls.CtrlObjStatePropertyFVect3D();
            this.Tab_DirectionalItem = new System.Windows.Forms.TabPage();
            this.ctrlPropertyDirection1 = new MCTester.Controls.CtrlObjStatePropertyFVect3D();
            this.tabControl1.SuspendLayout();
            this.Tab_DirectionalItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab_LightBaseItem
            // 
            this.Tab_LightBaseItem.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_LightBaseItem.Padding = new System.Windows.Forms.Padding(5);
            // 
            // Tab_ObjectSchemeItem
            // 
            this.Tab_ObjectSchemeItem.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectSchemeItem.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectScehemeNode.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_DirectionalItem);
            this.tabControl1.Controls.SetChildIndex(this.Tab_DirectionalItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LightBaseItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // ctrlPropertyDirection
            // 
            this.ctrlPropertyDirection.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlPropertyDirection.CurrentObjectSchemeNode = null;
            this.ctrlPropertyDirection.Location = new System.Drawing.Point(8, 7);
            this.ctrlPropertyDirection.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPropertyDirection.Name = "ctrlPropertyDirection";
            this.ctrlPropertyDirection.PropertyName = "Direction";
            this.ctrlPropertyDirection.RegPropertyID = ((uint)(0u));
            this.ctrlPropertyDirection.SelPropertyID = ((uint)(0u));
            this.ctrlPropertyDirection.Size = new System.Drawing.Size(533, 160);
            this.ctrlPropertyDirection.TabIndex = 0;
            // 
            // Tab_DirectionalItem
            // 
            this.Tab_DirectionalItem.Controls.Add(this.ctrlPropertyDirection1);
            this.Tab_DirectionalItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_DirectionalItem.Name = "Tab_DirectionalItem";
            this.Tab_DirectionalItem.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_DirectionalItem.Size = new System.Drawing.Size(1109, 857);
            this.Tab_DirectionalItem.TabIndex = 4;
            this.Tab_DirectionalItem.Text = "Directional Item";
            this.Tab_DirectionalItem.UseVisualStyleBackColor = true;
            // 
            // ctrlPropertyDirection1
            // 
            this.ctrlPropertyDirection1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlPropertyDirection1.CurrentObjectSchemeNode = null;
            this.ctrlPropertyDirection1.Location = new System.Drawing.Point(8, 8);
            this.ctrlPropertyDirection1.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPropertyDirection1.Name = "ctrlPropertyDirection1";
            this.ctrlPropertyDirection1.PropertyName = "Direction";
            this.ctrlPropertyDirection1.RegPropertyID = ((uint)(0u));
            this.ctrlPropertyDirection1.SelPropertyID = ((uint)(4294967295u));
            this.ctrlPropertyDirection1.Size = new System.Drawing.Size(533, 160);
            this.ctrlPropertyDirection1.TabIndex = 1;
            // 
            // ucDirectionalLightItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "ucDirectionalLightItem";
            this.tabControl1.ResumeLayout(false);
            this.Tab_DirectionalItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.CtrlObjStatePropertyFVect3D ctrlPropertyDirection;
        private System.Windows.Forms.TabPage Tab_DirectionalItem;
        private Controls.CtrlObjStatePropertyFVect3D ctrlPropertyDirection1;
    }
}
