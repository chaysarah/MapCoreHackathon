namespace MCTester.Controls
{
    partial class CtrlConditionalselectorOverlay
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
            this.label2 = new System.Windows.Forms.Label();
            this.clstConditionalSelector = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbActionType = new System.Windows.Forms.ComboBox();
            this.chkActionOnResult = new System.Windows.Forms.CheckBox();
            this.gbConditionalSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConditionalSelector
            // 
            this.gbConditionalSelector.Controls.Add(this.label2);
            this.gbConditionalSelector.Controls.Add(this.clstConditionalSelector);
            this.gbConditionalSelector.Controls.Add(this.label3);
            this.gbConditionalSelector.Controls.Add(this.cmbActionType);
            this.gbConditionalSelector.Controls.Add(this.chkActionOnResult);
            this.gbConditionalSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbConditionalSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.gbConditionalSelector.Location = new System.Drawing.Point(0, 0);
            this.gbConditionalSelector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConditionalSelector.Name = "gbConditionalSelector";
            this.gbConditionalSelector.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbConditionalSelector.Size = new System.Drawing.Size(372, 334);
            this.gbConditionalSelector.TabIndex = 59;
            this.gbConditionalSelector.TabStop = false;
            this.gbConditionalSelector.Text = "Conditional Selector";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(8, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 84;
            this.label2.Text = "Selector:";
            // 
            // clstConditionalSelector
            // 
            this.clstConditionalSelector.CheckOnClick = true;
            this.clstConditionalSelector.FormattingEnabled = true;
            this.clstConditionalSelector.HorizontalScrollbar = true;
            this.clstConditionalSelector.Location = new System.Drawing.Point(8, 95);
            this.clstConditionalSelector.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.clstConditionalSelector.Name = "clstConditionalSelector";
            this.clstConditionalSelector.Size = new System.Drawing.Size(345, 220);
            this.clstConditionalSelector.TabIndex = 85;
            this.clstConditionalSelector.SelectedIndexChanged += new System.EventHandler(this.clstConditionalSelector_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 17);
            this.label3.TabIndex = 83;
            this.label3.Text = "Action Type:";
            // 
            // cmbActionType
            // 
            this.cmbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbActionType.FormattingEnabled = true;
            this.cmbActionType.Location = new System.Drawing.Point(113, 44);
            this.cmbActionType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbActionType.Name = "cmbActionType";
            this.cmbActionType.Size = new System.Drawing.Size(240, 25);
            this.cmbActionType.TabIndex = 81;
            this.cmbActionType.SelectedIndexChanged += new System.EventHandler(this.cmbActionType_SelectedIndexChanged);
            // 
            // chkActionOnResult
            // 
            this.chkActionOnResult.AutoSize = true;
            this.chkActionOnResult.Location = new System.Drawing.Point(8, 23);
            this.chkActionOnResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkActionOnResult.Name = "chkActionOnResult";
            this.chkActionOnResult.Size = new System.Drawing.Size(136, 21);
            this.chkActionOnResult.TabIndex = 70;
            this.chkActionOnResult.Text = "Action On Result";
            this.chkActionOnResult.UseVisualStyleBackColor = true;
            // 
            // CtrlConditionalselectorOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbConditionalSelector);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CtrlConditionalselectorOverlay";
            this.Size = new System.Drawing.Size(372, 334);
            this.gbConditionalSelector.ResumeLayout(false);
            this.gbConditionalSelector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConditionalSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clstConditionalSelector;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbActionType;
        private System.Windows.Forms.CheckBox chkActionOnResult;
    }
}
