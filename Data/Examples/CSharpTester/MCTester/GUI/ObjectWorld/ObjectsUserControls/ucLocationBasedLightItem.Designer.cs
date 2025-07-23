namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucLocationBasedLightItem
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
            this.Tab_LocationBasedLightItem = new System.Windows.Forms.TabPage();
            this.Tab_LocationBasedItem = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyAttenuation1 = new MCTester.Controls.CtrlObjStatePropertyAttenuation();
            this.tabControl1.SuspendLayout();
            this.Tab_LocationBasedItem.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.Tab_LocationBasedItem);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LocationBasedItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LightBaseItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_LocationBasedLightItem
            // 
            this.Tab_LocationBasedLightItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_LocationBasedLightItem.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_LocationBasedLightItem.Name = "Tab_LocationBasedLightItem";
            this.Tab_LocationBasedLightItem.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_LocationBasedLightItem.Size = new System.Drawing.Size(1074, 582);
            this.Tab_LocationBasedLightItem.TabIndex = 1;
            this.Tab_LocationBasedLightItem.Text = "Location Based Light";
            this.Tab_LocationBasedLightItem.UseVisualStyleBackColor = true;
            // 
            // Tab_LocationBasedItem
            // 
            this.Tab_LocationBasedItem.Controls.Add(this.ctrlObjStatePropertyAttenuation1);
            this.Tab_LocationBasedItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_LocationBasedItem.Name = "Tab_LocationBasedItem";
            this.Tab_LocationBasedItem.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_LocationBasedItem.Size = new System.Drawing.Size(1109, 857);
            this.Tab_LocationBasedItem.TabIndex = 4;
            this.Tab_LocationBasedItem.Text = "Location Based Light Item";
            this.Tab_LocationBasedItem.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyAttenuation1
            // 
            this.ctrlObjStatePropertyAttenuation1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyAttenuation1.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyAttenuation1.Location = new System.Drawing.Point(6, 5);
            this.ctrlObjStatePropertyAttenuation1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyAttenuation1.Name = "ctrlObjStatePropertyAttenuation1";
            this.ctrlObjStatePropertyAttenuation1.PropertyName = "Attenuation";
            this.ctrlObjStatePropertyAttenuation1.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyAttenuation1.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyAttenuation1.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyAttenuation1.TabIndex = 2;
            // 
            // ucLocationBasedLightItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "ucLocationBasedLightItem";
            this.tabControl1.ResumeLayout(false);
            this.Tab_LocationBasedItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TabPage Tab_LocationBasedLightItem;
        private System.Windows.Forms.TabPage Tab_LocationBasedItem;
        private Controls.CtrlObjStatePropertyAttenuation ctrlObjStatePropertyAttenuation1;
    }
}
