namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    partial class ucBaseConditionalSelector
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
            this.cmbConditionalSelectorType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbConditionalSelector = new System.Windows.Forms.GroupBox();
            this.ntxConditionalSelectorID = new MCTester.Controls.NumericTextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.gbConditionalSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbConditionalSelectorType
            // 
            this.cmbConditionalSelectorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConditionalSelectorType.Enabled = false;
            this.cmbConditionalSelectorType.FormattingEnabled = true;
            this.cmbConditionalSelectorType.Location = new System.Drawing.Point(78, 43);
            this.cmbConditionalSelectorType.Name = "cmbConditionalSelectorType";
            this.cmbConditionalSelectorType.Size = new System.Drawing.Size(193, 21);
            this.cmbConditionalSelectorType.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "ID:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(88, 498);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(250, 498);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gbConditionalSelector
            // 
            this.gbConditionalSelector.Controls.Add(this.btnApply);
            this.gbConditionalSelector.Controls.Add(this.ntxConditionalSelectorID);
            this.gbConditionalSelector.Controls.Add(this.btnCancel);
            this.gbConditionalSelector.Controls.Add(this.label1);
            this.gbConditionalSelector.Controls.Add(this.btnOk);
            this.gbConditionalSelector.Controls.Add(this.label2);
            this.gbConditionalSelector.Controls.Add(this.cmbConditionalSelectorType);
            this.gbConditionalSelector.Location = new System.Drawing.Point(3, 3);
            this.gbConditionalSelector.Name = "gbConditionalSelector";
            this.gbConditionalSelector.Size = new System.Drawing.Size(331, 527);
            this.gbConditionalSelector.TabIndex = 15;
            this.gbConditionalSelector.TabStop = false;
            this.gbConditionalSelector.Text = "Conditional Selector Parameters";
            // 
            // ntxConditionalSelectorID
            // 
            this.ntxConditionalSelectorID.Location = new System.Drawing.Point(78, 19);
            this.ntxConditionalSelectorID.Name = "ntxConditionalSelectorID";
            this.ntxConditionalSelectorID.Size = new System.Drawing.Size(100, 20);
            this.ntxConditionalSelectorID.TabIndex = 10;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(169, 498);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 15;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // ucBaseConditionalSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbConditionalSelector);
            this.MinimumSize = new System.Drawing.Size(338, 533);
            this.Name = "ucBaseConditionalSelector";
            this.Size = new System.Drawing.Size(344, 533);
            this.gbConditionalSelector.ResumeLayout(false);
            this.gbConditionalSelector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbConditionalSelectorType;
        private System.Windows.Forms.Label label2;
        private MCTester.Controls.NumericTextBox ntxConditionalSelectorID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        protected System.Windows.Forms.GroupBox gbConditionalSelector;
        private System.Windows.Forms.Button btnApply;
    }
}
