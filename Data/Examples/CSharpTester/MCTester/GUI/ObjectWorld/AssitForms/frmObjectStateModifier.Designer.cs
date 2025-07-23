namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmObjectStateModifier
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbConditionalSelector = new System.Windows.Forms.ComboBox();
            this.chkActionOnResult = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbObjectState = new System.Windows.Forms.TextBox();
            this.bnOk = new System.Windows.Forms.Button();
            this.bnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Conditional Selector:";
            // 
            // cmbConditionalSelector
            // 
            this.cmbConditionalSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbConditionalSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConditionalSelector.FormattingEnabled = true;
            this.cmbConditionalSelector.Location = new System.Drawing.Point(157, 14);
            this.cmbConditionalSelector.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbConditionalSelector.Name = "cmbConditionalSelector";
            this.cmbConditionalSelector.Size = new System.Drawing.Size(324, 24);
            this.cmbConditionalSelector.TabIndex = 1;
            // 
            // chkActionOnResult
            // 
            this.chkActionOnResult.AutoSize = true;
            this.chkActionOnResult.Checked = true;
            this.chkActionOnResult.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActionOnResult.Location = new System.Drawing.Point(16, 50);
            this.chkActionOnResult.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkActionOnResult.Name = "chkActionOnResult";
            this.chkActionOnResult.Size = new System.Drawing.Size(128, 21);
            this.chkActionOnResult.TabIndex = 2;
            this.chkActionOnResult.Text = "Action on result";
            this.chkActionOnResult.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Object state:";
            // 
            // tbObjectState
            // 
            this.tbObjectState.Location = new System.Drawing.Point(157, 90);
            this.tbObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbObjectState.Name = "tbObjectState";
            this.tbObjectState.Size = new System.Drawing.Size(100, 22);
            this.tbObjectState.TabIndex = 4;
            // 
            // bnOk
            // 
            this.bnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bnOk.Location = new System.Drawing.Point(3, 167);
            this.bnOk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnOk.Name = "bnOk";
            this.bnOk.Size = new System.Drawing.Size(80, 30);
            this.bnOk.TabIndex = 5;
            this.bnOk.Text = "OK";
            this.bnOk.UseVisualStyleBackColor = true;
            this.bnOk.Click += new System.EventHandler(this.bnOk_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bnCancel.Location = new System.Drawing.Point(403, 167);
            this.bnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(80, 30);
            this.bnCancel.TabIndex = 6;
            this.bnCancel.Text = "Cancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // frmObjectStateModifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(493, 210);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnOk);
            this.Controls.Add(this.tbObjectState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkActionOnResult);
            this.Controls.Add(this.cmbConditionalSelector);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmObjectStateModifier";
            this.Text = "Object State Modifier";
            this.Load += new System.EventHandler(this.frmObjectStateModifier_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbConditionalSelector;
        private System.Windows.Forms.CheckBox chkActionOnResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbObjectState;
        private System.Windows.Forms.Button bnOk;
        private System.Windows.Forms.Button bnCancel;
    }
}