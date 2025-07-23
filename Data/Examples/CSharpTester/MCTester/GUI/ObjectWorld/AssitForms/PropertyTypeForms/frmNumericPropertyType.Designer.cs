namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmNumericPropertyType
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.ntxPropertyValue = new MCTester.Controls.NumericTextBox();
            this.cmbEnumOptions = new System.Windows.Forms.ComboBox();
            this.lstEnumFlags = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // ntxPropertyID
            // 
            this.ntxPropertyID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Value:";
            // 
            // ntxPropertyValue
            // 
            this.ntxPropertyValue.Location = new System.Drawing.Point(81, 12);
            this.ntxPropertyValue.Name = "ntxPropertyValue";
            this.ntxPropertyValue.Size = new System.Drawing.Size(100, 20);
            this.ntxPropertyValue.TabIndex = 4;
            this.ntxPropertyValue.TextChanged += new System.EventHandler(this.ntxPropertyValue_TextChanged);
            // 
            // cmbEnumOptions
            // 
            this.cmbEnumOptions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEnumOptions.FormattingEnabled = true;
            this.cmbEnumOptions.Location = new System.Drawing.Point(192, 12);
            this.cmbEnumOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbEnumOptions.Name = "cmbEnumOptions";
            this.cmbEnumOptions.Size = new System.Drawing.Size(174, 21);
            this.cmbEnumOptions.TabIndex = 5;
            this.cmbEnumOptions.Visible = false;
            this.cmbEnumOptions.SelectedIndexChanged += new System.EventHandler(this.cmbEnumOptions_SelectedIndexChanged);
            // 
            // lstEnumFlags
            // 
            this.lstEnumFlags.FormattingEnabled = true;
            this.lstEnumFlags.Location = new System.Drawing.Point(94, 70);
            this.lstEnumFlags.Margin = new System.Windows.Forms.Padding(2);
            this.lstEnumFlags.Name = "lstEnumFlags";
            this.lstEnumFlags.Size = new System.Drawing.Size(228, 199);
            this.lstEnumFlags.TabIndex = 28;
            this.lstEnumFlags.UseCompatibleTextRendering = true;
            this.lstEnumFlags.Visible = false;
            // 
            // frmNumericPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 339);
            this.Controls.Add(this.lstEnumFlags);
            this.Controls.Add(this.cmbEnumOptions);
            this.Controls.Add(this.ntxPropertyValue);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmNumericPropertyType";
            this.Text = "frmNumericPropertyType";
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ntxPropertyValue, 0);
            this.Controls.SetChildIndex(this.cmbEnumOptions, 0);
            this.Controls.SetChildIndex(this.lstEnumFlags, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxPropertyValue;
        private System.Windows.Forms.ComboBox cmbEnumOptions;
        private System.Windows.Forms.CheckedListBox lstEnumFlags;
    }
}