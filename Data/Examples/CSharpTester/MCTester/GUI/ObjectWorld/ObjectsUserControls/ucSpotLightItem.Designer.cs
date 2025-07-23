namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucSpotLightItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSpotLightItem));
            this.Tab_SpotLightItem = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyHalfOuterAngle = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertyHalfInnerAngle = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlPropertySpotDirection = new MCTester.Controls.CtrlObjStatePropertyFVect3D();
            this.Tab_LightBaseItem.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Tab_SpotLightItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab_LocationBasedLightItem
            // 
            this.Tab_LocationBasedLightItem.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Tab_LocationBasedLightItem.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            // 
            // Tab_LightBaseItem
            // 
            this.Tab_LightBaseItem.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_LightBaseItem.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_LightBaseItem.Click += new System.EventHandler(this.Tab_LightBaseItem_Click);
            // 
            // Tab_ObjectSchemeItem
            // 
            this.Tab_ObjectSchemeItem.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.Tab_ObjectSchemeItem.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.Tab_ObjectScehemeNode.Padding = new System.Windows.Forms.Padding(9, 7, 9, 7);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_SpotLightItem);
            this.tabControl1.Controls.SetChildIndex(this.Tab_SpotLightItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_LightBaseItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_SpotLightItem
            // 
            this.Tab_SpotLightItem.Controls.Add(this.ctrlObjStatePropertyHalfOuterAngle);
            this.Tab_SpotLightItem.Controls.Add(this.ctrlObjStatePropertyHalfInnerAngle);
            this.Tab_SpotLightItem.Controls.Add(this.ctrlPropertySpotDirection);
            this.Tab_SpotLightItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_SpotLightItem.Name = "Tab_SpotLightItem";
            this.Tab_SpotLightItem.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_SpotLightItem.Size = new System.Drawing.Size(1109, 857);
            this.Tab_SpotLightItem.TabIndex = 5;
            this.Tab_SpotLightItem.Text = "Spot Light Item";
            this.Tab_SpotLightItem.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyHalfOuterAngle
            // 
            this.ctrlObjStatePropertyHalfOuterAngle.Location = new System.Drawing.Point(5, 175);
            this.ctrlObjStatePropertyHalfOuterAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyHalfOuterAngle.Name = "ctrlObjStatePropertyHalfOuterAngle";
            this.ctrlObjStatePropertyHalfOuterAngle.PropertyName = "Half Outer Angle";
            this.ctrlObjStatePropertyHalfOuterAngle.RegFloatVal = 0F;
            this.ctrlObjStatePropertyHalfOuterAngle.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyHalfOuterAngle.SelFloatVal = 0F;
            this.ctrlObjStatePropertyHalfOuterAngle.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyHalfOuterAngle.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyHalfOuterAngle.TabIndex = 8;
            // 
            // ctrlObjStatePropertyHalfInnerAngle
            // 
            this.ctrlObjStatePropertyHalfInnerAngle.Location = new System.Drawing.Point(551, 175);
            this.ctrlObjStatePropertyHalfInnerAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyHalfInnerAngle.Name = "ctrlObjStatePropertyHalfInnerAngle";
            this.ctrlObjStatePropertyHalfInnerAngle.PropertyName = "Half Inner Angle";
            this.ctrlObjStatePropertyHalfInnerAngle.RegFloatVal = 0F;
            this.ctrlObjStatePropertyHalfInnerAngle.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyHalfInnerAngle.SelFloatVal = 0F;
            this.ctrlObjStatePropertyHalfInnerAngle.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyHalfInnerAngle.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyHalfInnerAngle.TabIndex = 7;
            // 
            // ctrlPropertySpotDirection
            // 
            this.ctrlPropertySpotDirection.Location = new System.Drawing.Point(5, 8);
            this.ctrlPropertySpotDirection.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPropertySpotDirection.Name = "ctrlPropertySpotDirection";
            this.ctrlPropertySpotDirection.PropertyName = "Direction";
            this.ctrlPropertySpotDirection.RegPropertyID = ((uint)(0u));
            this.ctrlPropertySpotDirection.SelPropertyID = ((uint)(0u));
            this.ctrlPropertySpotDirection.Size = new System.Drawing.Size(533, 160);
            this.ctrlPropertySpotDirection.TabIndex = 6;
            // 
            // ucSpotLightItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            this.Name = "ucSpotLightItem";
            this.Tab_LightBaseItem.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.Tab_SpotLightItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabPage Tab_SpotLightItem;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertyHalfOuterAngle;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertyHalfInnerAngle;
        private Controls.CtrlObjStatePropertyFVect3D ctrlPropertySpotDirection;
    }
}
