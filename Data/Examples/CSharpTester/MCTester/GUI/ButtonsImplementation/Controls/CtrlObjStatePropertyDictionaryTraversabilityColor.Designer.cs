namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyDictionaryTraversabilityColor
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
            this.cbRegEnumColor = new System.Windows.Forms.ComboBox();
            this.ctrlRegSelectColor = new MCTester.Controls.SelectColor();
            this.ctrlSelSelectColor = new MCTester.Controls.SelectColor();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbRegEnumColor);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(395, 174);
            this.groupBox1.Controls.SetChildIndex(this.cbRegEnumColor, 0);
            this.groupBox1.Controls.SetChildIndex(this.tcProperty, 0);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 47);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4);
            this.tcProperty.Size = new System.Drawing.Size(387, 123);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegSelectColor);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Size = new System.Drawing.Size(379, 97);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegSelectColor, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelSelectColor);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(379, 97);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelSelectColor, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // cbRegEnumColor
            // 
            this.cbRegEnumColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegEnumColor.FormattingEnabled = true;
            this.cbRegEnumColor.Location = new System.Drawing.Point(3, 19);
            this.cbRegEnumColor.Name = "cbRegEnumColor";
            this.cbRegEnumColor.Size = new System.Drawing.Size(175, 21);
            this.cbRegEnumColor.TabIndex = 27;
            this.cbRegEnumColor.SelectedIndexChanged += new System.EventHandler(this.cbRegEnumColor_SelectedIndexChanged);
            // 
            // ctrlRegSelectColor
            // 
            this.ctrlRegSelectColor.Location = new System.Drawing.Point(79, 47);
            this.ctrlRegSelectColor.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlRegSelectColor.Name = "ctrlRegSelectColor";
            this.ctrlRegSelectColor.Size = new System.Drawing.Size(129, 24);
            this.ctrlRegSelectColor.TabIndex = 26;
            this.ctrlRegSelectColor.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRegSelectColor_Validating);
            // 
            // ctrlSelSelectColor
            // 
            this.ctrlSelSelectColor.Location = new System.Drawing.Point(79, 47);
            this.ctrlSelSelectColor.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSelSelectColor.Name = "ctrlSelSelectColor";
            this.ctrlSelSelectColor.Size = new System.Drawing.Size(129, 23);
            this.ctrlSelSelectColor.TabIndex = 34;
            this.ctrlSelSelectColor.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSelSelectColor_Validating);
            // 
            // CtrlObjStatePropertyDictionaryTraversabilityColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyDictionaryTraversabilityColor";
            this.Size = new System.Drawing.Size(395, 174);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRegEnumColor;
        private SelectColor ctrlRegSelectColor;
        private SelectColor ctrlSelSelectColor;
    }
}
