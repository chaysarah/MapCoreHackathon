namespace MCTester.Controls
{
    partial class CtrlConditionalSelectorProperty
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
            this.gbConditionalSelector = new System.Windows.Forms.GroupBox();
            this.tcConditionalSelector = new System.Windows.Forms.TabControl();
            this.tpRegConditionalSelector = new System.Windows.Forms.TabPage();
            this.chxSelectionExists = new System.Windows.Forms.CheckBox();
            this.lblRegPropertyID = new System.Windows.Forms.Label();
            this.ntxRegPropertyID = new MCTester.Controls.NumericTextBox();
            this.rdbRegShared = new System.Windows.Forms.RadioButton();
            this.rdbRegPrivate = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.clstRegConditionalSelector = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRegActionType = new System.Windows.Forms.ComboBox();
            this.tpSelConditionalSelector = new System.Windows.Forms.TabPage();
            this.lblSelPropertyID = new System.Windows.Forms.Label();
            this.rdbSelShared = new System.Windows.Forms.RadioButton();
            this.rdbSelPrivate = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxSelPropertyID = new MCTester.Controls.NumericTextBox();
            this.clstSelConditionalSelector = new System.Windows.Forms.CheckedListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSelActionType = new System.Windows.Forms.ComboBox();
            this.chkActionOnResult = new System.Windows.Forms.CheckBox();
            this.btnSetSelector = new System.Windows.Forms.Button();
            this.gbConditionalSelector.SuspendLayout();
            this.tcConditionalSelector.SuspendLayout();
            this.tpRegConditionalSelector.SuspendLayout();
            this.tpSelConditionalSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConditionalSelector
            // 
            this.gbConditionalSelector.Controls.Add(this.tcConditionalSelector);
            this.gbConditionalSelector.Controls.Add(this.chkActionOnResult);
            this.gbConditionalSelector.Controls.Add(this.btnSetSelector);
            this.gbConditionalSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConditionalSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.gbConditionalSelector.Location = new System.Drawing.Point(0, 0);
            this.gbConditionalSelector.Name = "gbConditionalSelector";
            this.gbConditionalSelector.Size = new System.Drawing.Size(286, 317);
            this.gbConditionalSelector.TabIndex = 58;
            this.gbConditionalSelector.TabStop = false;
            this.gbConditionalSelector.Text = "Conditional Selector";
            // 
            // tcConditionalSelector
            // 
            this.tcConditionalSelector.Controls.Add(this.tpRegConditionalSelector);
            this.tcConditionalSelector.Controls.Add(this.tpSelConditionalSelector);
            this.tcConditionalSelector.Location = new System.Drawing.Point(3, 42);
            this.tcConditionalSelector.Multiline = true;
            this.tcConditionalSelector.Name = "tcConditionalSelector";
            this.tcConditionalSelector.SelectedIndex = 0;
            this.tcConditionalSelector.Size = new System.Drawing.Size(280, 243);
            this.tcConditionalSelector.TabIndex = 71;
            // 
            // tpRegConditionalSelector
            // 
            this.tpRegConditionalSelector.Controls.Add(this.chxSelectionExists);
            this.tpRegConditionalSelector.Controls.Add(this.lblRegPropertyID);
            this.tpRegConditionalSelector.Controls.Add(this.ntxRegPropertyID);
            this.tpRegConditionalSelector.Controls.Add(this.rdbRegShared);
            this.tpRegConditionalSelector.Controls.Add(this.rdbRegPrivate);
            this.tpRegConditionalSelector.Controls.Add(this.label2);
            this.tpRegConditionalSelector.Controls.Add(this.clstRegConditionalSelector);
            this.tpRegConditionalSelector.Controls.Add(this.label3);
            this.tpRegConditionalSelector.Controls.Add(this.cmbRegActionType);
            this.tpRegConditionalSelector.Location = new System.Drawing.Point(4, 22);
            this.tpRegConditionalSelector.Name = "tpRegConditionalSelector";
            this.tpRegConditionalSelector.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegConditionalSelector.Size = new System.Drawing.Size(272, 217);
            this.tpRegConditionalSelector.TabIndex = 0;
            this.tpRegConditionalSelector.Text = "Regular";
            this.tpRegConditionalSelector.UseVisualStyleBackColor = true;
            // 
            // chxSelectionExists
            // 
            this.chxSelectionExists.AutoSize = true;
            this.chxSelectionExists.Location = new System.Drawing.Point(9, 6);
            this.chxSelectionExists.Name = "chxSelectionExists";
            this.chxSelectionExists.Size = new System.Drawing.Size(142, 17);
            this.chxSelectionExists.TabIndex = 72;
            this.chxSelectionExists.Text = "Selection Property Exists";
            this.chxSelectionExists.UseVisualStyleBackColor = true;
            this.chxSelectionExists.CheckedChanged += new System.EventHandler(this.chxSelectionExists_CheckedChanged);
            // 
            // lblRegPropertyID
            // 
            this.lblRegPropertyID.AutoSize = true;
            this.lblRegPropertyID.Location = new System.Drawing.Point(97, 174);
            this.lblRegPropertyID.Name = "lblRegPropertyID";
            this.lblRegPropertyID.Size = new System.Drawing.Size(63, 13);
            this.lblRegPropertyID.TabIndex = 80;
            this.lblRegPropertyID.Text = "Property ID:";
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Enabled = false;
            this.ntxRegPropertyID.Location = new System.Drawing.Point(166, 171);
            this.ntxRegPropertyID.Name = "ntxRegPropertyID";
            this.ntxRegPropertyID.Size = new System.Drawing.Size(100, 20);
            this.ntxRegPropertyID.TabIndex = 79;
            // 
            // rdbRegShared
            // 
            this.rdbRegShared.AutoSize = true;
            this.rdbRegShared.Checked = true;
            this.rdbRegShared.Location = new System.Drawing.Point(9, 195);
            this.rdbRegShared.Name = "rdbRegShared";
            this.rdbRegShared.Size = new System.Drawing.Size(59, 17);
            this.rdbRegShared.TabIndex = 78;
            this.rdbRegShared.TabStop = true;
            this.rdbRegShared.Text = "Shared";
            this.rdbRegShared.UseVisualStyleBackColor = true;
            this.rdbRegShared.CheckedChanged += new System.EventHandler(this.rdbRegShared_CheckedChanged);
            // 
            // rdbRegPrivate
            // 
            this.rdbRegPrivate.AutoSize = true;
            this.rdbRegPrivate.Location = new System.Drawing.Point(9, 172);
            this.rdbRegPrivate.Name = "rdbRegPrivate";
            this.rdbRegPrivate.Size = new System.Drawing.Size(58, 17);
            this.rdbRegPrivate.TabIndex = 77;
            this.rdbRegPrivate.Text = "Private";
            this.rdbRegPrivate.UseVisualStyleBackColor = true;
            this.rdbRegPrivate.CheckedChanged += new System.EventHandler(this.rdbRegPrivate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 74;
            this.label2.Text = "Selector:";
            // 
            // clstRegConditionalSelector
            // 
            this.clstRegConditionalSelector.CheckOnClick = true;
            this.clstRegConditionalSelector.FormattingEnabled = true;
            this.clstRegConditionalSelector.HorizontalScrollbar = true;
            this.clstRegConditionalSelector.Location = new System.Drawing.Point(6, 72);
            this.clstRegConditionalSelector.Name = "clstRegConditionalSelector";
            this.clstRegConditionalSelector.Size = new System.Drawing.Size(260, 94);
            this.clstRegConditionalSelector.TabIndex = 75;
            this.clstRegConditionalSelector.SelectedIndexChanged += new System.EventHandler(this.clstRegConditionalSelector_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "Action Type:";
            // 
            // cmbRegActionType
            // 
            this.cmbRegActionType.FormattingEnabled = true;
            this.cmbRegActionType.Location = new System.Drawing.Point(79, 28);
            this.cmbRegActionType.Name = "cmbRegActionType";
            this.cmbRegActionType.Size = new System.Drawing.Size(187, 21);
            this.cmbRegActionType.TabIndex = 72;
            this.cmbRegActionType.SelectedIndexChanged += new System.EventHandler(this.cmbRegActionType_SelectedIndexChanged);
            // 
            // tpSelConditionalSelector
            // 
            this.tpSelConditionalSelector.Controls.Add(this.lblSelPropertyID);
            this.tpSelConditionalSelector.Controls.Add(this.rdbSelShared);
            this.tpSelConditionalSelector.Controls.Add(this.rdbSelPrivate);
            this.tpSelConditionalSelector.Controls.Add(this.label1);
            this.tpSelConditionalSelector.Controls.Add(this.ntxSelPropertyID);
            this.tpSelConditionalSelector.Controls.Add(this.clstSelConditionalSelector);
            this.tpSelConditionalSelector.Controls.Add(this.label8);
            this.tpSelConditionalSelector.Controls.Add(this.cmbSelActionType);
            this.tpSelConditionalSelector.Location = new System.Drawing.Point(4, 22);
            this.tpSelConditionalSelector.Name = "tpSelConditionalSelector";
            this.tpSelConditionalSelector.Padding = new System.Windows.Forms.Padding(3);
            this.tpSelConditionalSelector.Size = new System.Drawing.Size(272, 217);
            this.tpSelConditionalSelector.TabIndex = 1;
            this.tpSelConditionalSelector.Text = "Selection";
            this.tpSelConditionalSelector.UseVisualStyleBackColor = true;
            // 
            // lblSelPropertyID
            // 
            this.lblSelPropertyID.AutoSize = true;
            this.lblSelPropertyID.Location = new System.Drawing.Point(97, 174);
            this.lblSelPropertyID.Name = "lblSelPropertyID";
            this.lblSelPropertyID.Size = new System.Drawing.Size(63, 13);
            this.lblSelPropertyID.TabIndex = 84;
            this.lblSelPropertyID.Text = "Property ID:";
            // 
            // rdbSelShared
            // 
            this.rdbSelShared.AutoSize = true;
            this.rdbSelShared.Checked = true;
            this.rdbSelShared.Location = new System.Drawing.Point(9, 195);
            this.rdbSelShared.Name = "rdbSelShared";
            this.rdbSelShared.Size = new System.Drawing.Size(59, 17);
            this.rdbSelShared.TabIndex = 82;
            this.rdbSelShared.TabStop = true;
            this.rdbSelShared.Text = "Shared";
            this.rdbSelShared.UseVisualStyleBackColor = true;
            this.rdbSelShared.CheckedChanged += new System.EventHandler(this.rdbSelShared_CheckedChanged);
            // 
            // rdbSelPrivate
            // 
            this.rdbSelPrivate.AutoSize = true;
            this.rdbSelPrivate.Location = new System.Drawing.Point(9, 172);
            this.rdbSelPrivate.Name = "rdbSelPrivate";
            this.rdbSelPrivate.Size = new System.Drawing.Size(58, 17);
            this.rdbSelPrivate.TabIndex = 81;
            this.rdbSelPrivate.Text = "Private";
            this.rdbSelPrivate.UseVisualStyleBackColor = true;
            this.rdbSelPrivate.CheckedChanged += new System.EventHandler(this.rdbSelPrivate_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(6, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 70;
            this.label1.Text = "Selector:";
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Enabled = false;
            this.ntxSelPropertyID.Location = new System.Drawing.Point(166, 171);
            this.ntxSelPropertyID.Name = "ntxSelPropertyID";
            this.ntxSelPropertyID.Size = new System.Drawing.Size(100, 20);
            this.ntxSelPropertyID.TabIndex = 83;
            // 
            // clstSelConditionalSelector
            // 
            this.clstSelConditionalSelector.CheckOnClick = true;
            this.clstSelConditionalSelector.FormattingEnabled = true;
            this.clstSelConditionalSelector.Location = new System.Drawing.Point(6, 72);
            this.clstSelConditionalSelector.Name = "clstSelConditionalSelector";
            this.clstSelConditionalSelector.Size = new System.Drawing.Size(260, 94);
            this.clstSelConditionalSelector.TabIndex = 71;
            this.clstSelConditionalSelector.SelectedIndexChanged += new System.EventHandler(this.clstSelConditionalSelector_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Action Type:";
            // 
            // cmbSelActionType
            // 
            this.cmbSelActionType.FormattingEnabled = true;
            this.cmbSelActionType.Location = new System.Drawing.Point(79, 28);
            this.cmbSelActionType.Name = "cmbSelActionType";
            this.cmbSelActionType.Size = new System.Drawing.Size(187, 21);
            this.cmbSelActionType.TabIndex = 20;
            this.cmbSelActionType.SelectedIndexChanged += new System.EventHandler(this.cmbSelActionType_SelectedIndexChanged);
            // 
            // chkActionOnResult
            // 
            this.chkActionOnResult.AutoSize = true;
            this.chkActionOnResult.Location = new System.Drawing.Point(6, 19);
            this.chkActionOnResult.Name = "chkActionOnResult";
            this.chkActionOnResult.Size = new System.Drawing.Size(106, 17);
            this.chkActionOnResult.TabIndex = 70;
            this.chkActionOnResult.Text = "Action On Result";
            this.chkActionOnResult.UseVisualStyleBackColor = true;
            // 
            // btnSetSelector
            // 
            this.btnSetSelector.Location = new System.Drawing.Point(198, 287);
            this.btnSetSelector.Name = "btnSetSelector";
            this.btnSetSelector.Size = new System.Drawing.Size(75, 23);
            this.btnSetSelector.TabIndex = 76;
            this.btnSetSelector.Text = "Set";
            this.btnSetSelector.UseVisualStyleBackColor = true;
            this.btnSetSelector.Click += new System.EventHandler(this.btnSetSelector_Click);
            // 
            // CtrlConditionalSelectorProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbConditionalSelector);
            this.Name = "CtrlConditionalSelectorProperty";
            this.Size = new System.Drawing.Size(286, 317);
            this.gbConditionalSelector.ResumeLayout(false);
            this.gbConditionalSelector.PerformLayout();
            this.tcConditionalSelector.ResumeLayout(false);
            this.tpRegConditionalSelector.ResumeLayout(false);
            this.tpRegConditionalSelector.PerformLayout();
            this.tpSelConditionalSelector.ResumeLayout(false);
            this.tpSelConditionalSelector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConditionalSelector;
        private System.Windows.Forms.CheckBox chkActionOnResult;
        private System.Windows.Forms.TabControl tcConditionalSelector;
        private System.Windows.Forms.TabPage tpRegConditionalSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clstRegConditionalSelector;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRegActionType;
        private System.Windows.Forms.TabPage tpSelConditionalSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clstSelConditionalSelector;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbSelActionType;
        private System.Windows.Forms.Button btnSetSelector;
        private NumericTextBox ntxRegPropertyID;
        private System.Windows.Forms.RadioButton rdbRegShared;
        private System.Windows.Forms.RadioButton rdbRegPrivate;
        private System.Windows.Forms.Label lblRegPropertyID;
        private System.Windows.Forms.Label lblSelPropertyID;
        private NumericTextBox ntxSelPropertyID;
        private System.Windows.Forms.RadioButton rdbSelShared;
        private System.Windows.Forms.RadioButton rdbSelPrivate;
        private System.Windows.Forms.CheckBox chxSelectionExists;
    }
}
