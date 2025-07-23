namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmNewObjectLocation
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
            this.label3 = new System.Windows.Forms.Label();
            this.chxRelativeToDTM = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLocationCoordSystem = new System.Windows.Forms.ComboBox();
            this.btnAddNewLocation = new System.Windows.Forms.Button();
            this.ntxInsertAtIndex = new MCTester.Controls.NumericTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Insert At Index: ";
            // 
            // chxRelativeToDTM
            // 
            this.chxRelativeToDTM.AutoSize = true;
            this.chxRelativeToDTM.Location = new System.Drawing.Point(12, 12);
            this.chxRelativeToDTM.Name = "chxRelativeToDTM";
            this.chxRelativeToDTM.Size = new System.Drawing.Size(108, 17);
            this.chxRelativeToDTM.TabIndex = 9;
            this.chxRelativeToDTM.Text = "Relative To DTM";
            this.chxRelativeToDTM.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Coord System:";
            // 
            // cmbLocationCoordSystem
            // 
            this.cmbLocationCoordSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationCoordSystem.FormattingEnabled = true;
            this.cmbLocationCoordSystem.Location = new System.Drawing.Point(98, 35);
            this.cmbLocationCoordSystem.Name = "cmbLocationCoordSystem";
            this.cmbLocationCoordSystem.Size = new System.Drawing.Size(175, 21);
            this.cmbLocationCoordSystem.TabIndex = 10;
            // 
            // btnAddNewLocation
            // 
            this.btnAddNewLocation.Location = new System.Drawing.Point(198, 93);
            this.btnAddNewLocation.Name = "btnAddNewLocation";
            this.btnAddNewLocation.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewLocation.TabIndex = 14;
            this.btnAddNewLocation.Text = "Add";
            this.btnAddNewLocation.UseVisualStyleBackColor = true;
            this.btnAddNewLocation.Click += new System.EventHandler(this.btnAddNewLocation_Click);
            // 
            // ntxInsertAtIndex
            // 
            this.ntxInsertAtIndex.Location = new System.Drawing.Point(98, 62);
            this.ntxInsertAtIndex.Name = "ntxInsertAtIndex";
            this.ntxInsertAtIndex.Size = new System.Drawing.Size(100, 20);
            this.ntxInsertAtIndex.TabIndex = 12;
            this.ntxInsertAtIndex.Text = "MAX";
            // 
            // frmNewObjectLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(282, 123);
            this.Controls.Add(this.btnAddNewLocation);
            this.Controls.Add(this.ntxInsertAtIndex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chxRelativeToDTM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbLocationCoordSystem);
            this.Name = "frmNewObjectLocation";
            this.Text = "Add Object Location";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewObjectLocation_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.NumericTextBox ntxInsertAtIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chxRelativeToDTM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLocationCoordSystem;
        private System.Windows.Forms.Button btnAddNewLocation;
    }
}