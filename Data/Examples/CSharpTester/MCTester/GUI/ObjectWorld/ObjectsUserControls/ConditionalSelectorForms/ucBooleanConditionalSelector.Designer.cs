namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    partial class ucBooleanConditionalSelector
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
            this.label3 = new System.Windows.Forms.Label();
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.clstCondSelectors = new System.Windows.Forms.CheckedListBox();
            this.gbConditionalSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConditionalSelector
            // 
            this.gbConditionalSelector.Controls.Add(this.label3);
            this.gbConditionalSelector.Controls.Add(this.clstCondSelectors);
            this.gbConditionalSelector.Controls.Add(this.cmbOperation);
            this.gbConditionalSelector.Controls.SetChildIndex(this.cmbOperation, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.clstCondSelectors, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.label3, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Operation:";
            // 
            // cmbOperation
            // 
            this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperation.FormattingEnabled = true;
            this.cmbOperation.Location = new System.Drawing.Point(104, 95);
            this.cmbOperation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Size = new System.Drawing.Size(221, 24);
            this.cmbOperation.TabIndex = 16;
            // 
            // clstCondSelectors
            // 
            this.clstCondSelectors.FormattingEnabled = true;
            this.clstCondSelectors.Location = new System.Drawing.Point(12, 140);
            this.clstCondSelectors.Margin = new System.Windows.Forms.Padding(4);
            this.clstCondSelectors.Name = "clstCondSelectors";
            this.clstCondSelectors.Size = new System.Drawing.Size(341, 395);
            this.clstCondSelectors.TabIndex = 15;
            // 
            // ucBooleanConditionalSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(601, 807);
            this.Name = "ucBooleanConditionalSelector";
            this.Size = new System.Drawing.Size(601, 807);
            this.gbConditionalSelector.ResumeLayout(false);
            this.gbConditionalSelector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbOperation;
        private System.Windows.Forms.CheckedListBox clstCondSelectors;
    }
}
