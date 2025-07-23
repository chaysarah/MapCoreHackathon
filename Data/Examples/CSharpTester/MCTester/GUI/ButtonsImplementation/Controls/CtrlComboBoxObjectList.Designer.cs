namespace MCTester.Controls
{
    partial class CtrlComboBoxObjectList
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
            this.lblComboBox = new System.Windows.Forms.Label();
            this.cmbObjectsList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblComboBox
            // 
            this.lblComboBox.AutoSize = true;
            this.lblComboBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblComboBox.Location = new System.Drawing.Point(0, 0);
            this.lblComboBox.Name = "lblComboBox";
            this.lblComboBox.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblComboBox.Size = new System.Drawing.Size(46, 16);
            this.lblComboBox.TabIndex = 0;
            this.lblComboBox.Text = "Objects:";
            // 
            // cmbObjectsList
            // 
            this.cmbObjectsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbObjectsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjectsList.FormattingEnabled = true;
            this.cmbObjectsList.Location = new System.Drawing.Point(46, 0);
            this.cmbObjectsList.Name = "cmbObjectsList";
            this.cmbObjectsList.Size = new System.Drawing.Size(193, 21);
            this.cmbObjectsList.TabIndex = 1;
            // 
            // CtrlComboBoxObjectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbObjectsList);
            this.Controls.Add(this.lblComboBox);
            this.Name = "CtrlComboBoxObjectList";
            this.Size = new System.Drawing.Size(239, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblComboBox;
        public System.Windows.Forms.ComboBox cmbObjectsList;
    }
}
