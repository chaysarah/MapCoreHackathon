namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class CtrlObjectSchemeItem
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
            this.label11 = new System.Windows.Forms.Label();
            this.chxHiddenIfViewportOverloaded = new System.Windows.Forms.CheckBox();
            this.Tab_ObjectSchemeItem = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyBlockedTransparency = new MCTester.Controls.CtrlObjStatePropertyByte();
            this.chxParticipationInSightQueries = new System.Windows.Forms.CheckBox();
            this.clstItemSubTypeBitField = new System.Windows.Forms.CheckedListBox();
            this.chxIsDetectibility = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.Tab_ObjectSchemeItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_ObjectScehemeNode.Padding = new System.Windows.Forms.Padding(5);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_ObjectSchemeItem);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 18);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 17);
            this.label11.TabIndex = 60;
            this.label11.Text = "Item Sub Type:";
            // 
            // chxHiddenIfViewportOverloaded
            // 
            this.chxHiddenIfViewportOverloaded.AutoSize = true;
            this.chxHiddenIfViewportOverloaded.Location = new System.Drawing.Point(12, 105);
            this.chxHiddenIfViewportOverloaded.Margin = new System.Windows.Forms.Padding(4);
            this.chxHiddenIfViewportOverloaded.Name = "chxHiddenIfViewportOverloaded";
            this.chxHiddenIfViewportOverloaded.Size = new System.Drawing.Size(222, 21);
            this.chxHiddenIfViewportOverloaded.TabIndex = 61;
            this.chxHiddenIfViewportOverloaded.Text = "Hidden If Viewport Overloaded";
            this.chxHiddenIfViewportOverloaded.UseVisualStyleBackColor = true;
            // 
            // Tab_ObjectSchemeItem
            // 
            this.Tab_ObjectSchemeItem.Controls.Add(this.ctrlObjStatePropertyBlockedTransparency);
            this.Tab_ObjectSchemeItem.Controls.Add(this.chxParticipationInSightQueries);
            this.Tab_ObjectSchemeItem.Controls.Add(this.clstItemSubTypeBitField);
            this.Tab_ObjectSchemeItem.Controls.Add(this.chxIsDetectibility);
            this.Tab_ObjectSchemeItem.Controls.Add(this.chxHiddenIfViewportOverloaded);
            this.Tab_ObjectSchemeItem.Controls.Add(this.label11);
            this.Tab_ObjectSchemeItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_ObjectSchemeItem.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_ObjectSchemeItem.Name = "Tab_ObjectSchemeItem";
            this.Tab_ObjectSchemeItem.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_ObjectSchemeItem.Size = new System.Drawing.Size(1109, 857);
            this.Tab_ObjectSchemeItem.TabIndex = 1;
            this.Tab_ObjectSchemeItem.Text = "Object Scheme Item";
            this.Tab_ObjectSchemeItem.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyBlockedTransparency
            // 
            this.ctrlObjStatePropertyBlockedTransparency.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyBlockedTransparency.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyBlockedTransparency.Location = new System.Drawing.Point(12, 188);
            this.ctrlObjStatePropertyBlockedTransparency.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyBlockedTransparency.Name = "ctrlObjStatePropertyBlockedTransparency";
            this.ctrlObjStatePropertyBlockedTransparency.PropertyName = "Blocked Transparency";
            this.ctrlObjStatePropertyBlockedTransparency.RegByteVal = ((byte)(0));
            this.ctrlObjStatePropertyBlockedTransparency.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyBlockedTransparency.SelByteVal = ((byte)(0));
            this.ctrlObjStatePropertyBlockedTransparency.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyBlockedTransparency.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyBlockedTransparency.TabIndex = 112;
            // 
            // chxParticipationInSightQueries
            // 
            this.chxParticipationInSightQueries.AutoSize = true;
            this.chxParticipationInSightQueries.Location = new System.Drawing.Point(12, 161);
            this.chxParticipationInSightQueries.Margin = new System.Windows.Forms.Padding(4);
            this.chxParticipationInSightQueries.Name = "chxParticipationInSightQueries";
            this.chxParticipationInSightQueries.Size = new System.Drawing.Size(213, 21);
            this.chxParticipationInSightQueries.TabIndex = 111;
            this.chxParticipationInSightQueries.Text = "Participation In Sight Queries";
            this.chxParticipationInSightQueries.UseVisualStyleBackColor = true;
            // 
            // clstItemSubTypeBitField
            // 
            this.clstItemSubTypeBitField.Enabled = false;
            this.clstItemSubTypeBitField.FormattingEnabled = true;
            this.clstItemSubTypeBitField.Location = new System.Drawing.Point(121, 18);
            this.clstItemSubTypeBitField.Margin = new System.Windows.Forms.Padding(4);
            this.clstItemSubTypeBitField.Name = "clstItemSubTypeBitField";
            this.clstItemSubTypeBitField.Size = new System.Drawing.Size(419, 72);
            this.clstItemSubTypeBitField.TabIndex = 110;
            // 
            // chxIsDetectibility
            // 
            this.chxIsDetectibility.AutoSize = true;
            this.chxIsDetectibility.Location = new System.Drawing.Point(12, 132);
            this.chxIsDetectibility.Margin = new System.Windows.Forms.Padding(4);
            this.chxIsDetectibility.Name = "chxIsDetectibility";
            this.chxIsDetectibility.Size = new System.Drawing.Size(107, 21);
            this.chxIsDetectibility.TabIndex = 64;
            this.chxIsDetectibility.Text = "Is Detectible";
            this.chxIsDetectibility.UseVisualStyleBackColor = true;
            // 
            // CtrlObjectSchemeItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CtrlObjectSchemeItem";
            this.Load += new System.EventHandler(this.CtrlObjectSchemeItem_Load);
            this.tabControl1.ResumeLayout(false);
            this.Tab_ObjectSchemeItem.ResumeLayout(false);
            this.Tab_ObjectSchemeItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chxHiddenIfViewportOverloaded;
        private System.Windows.Forms.CheckBox chxIsDetectibility;
        protected System.Windows.Forms.TabPage Tab_ObjectSchemeItem;
        private System.Windows.Forms.CheckedListBox clstItemSubTypeBitField;
        private System.Windows.Forms.CheckBox chxParticipationInSightQueries;
        private Controls.CtrlObjStatePropertyByte ctrlObjStatePropertyBlockedTransparency;
    }
}
