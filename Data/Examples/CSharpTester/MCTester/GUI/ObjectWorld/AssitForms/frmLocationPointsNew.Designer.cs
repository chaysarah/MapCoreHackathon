namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmLocationPointsNew
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
            this.lblLocationCoordSystem = new System.Windows.Forms.Label();
            this.cmbLocationCoordinateSystem = new System.Windows.Forms.ComboBox();
            this.chxLocationRelativeToDTM = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbSchemes = new System.Windows.Forms.GroupBox();
            this.lstSchemes = new System.Windows.Forms.ListBox();
            this.ctrlPointsGrid1 = new MCTester.Controls.CtrlPointsGrid();
            this.gbSchemes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLocationCoordSystem
            // 
            this.lblLocationCoordSystem.AutoSize = true;
            this.lblLocationCoordSystem.Location = new System.Drawing.Point(199, 38);
            this.lblLocationCoordSystem.Name = "lblLocationCoordSystem";
            this.lblLocationCoordSystem.Size = new System.Drawing.Size(142, 13);
            this.lblLocationCoordSystem.TabIndex = 18;
            this.lblLocationCoordSystem.Text = "Location Coordinate System:";
            // 
            // cmbLocationCoordinateSystem
            // 
            this.cmbLocationCoordinateSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationCoordinateSystem.FormattingEnabled = true;
            this.cmbLocationCoordinateSystem.Location = new System.Drawing.Point(347, 35);
            this.cmbLocationCoordinateSystem.Name = "cmbLocationCoordinateSystem";
            this.cmbLocationCoordinateSystem.Size = new System.Drawing.Size(214, 21);
            this.cmbLocationCoordinateSystem.TabIndex = 17;
            this.cmbLocationCoordinateSystem.SelectedIndexChanged += new System.EventHandler(this.cmbLocationCoordinateSystem_SelectedIndexChanged);
            // 
            // chxLocationRelativeToDTM
            // 
            this.chxLocationRelativeToDTM.AutoSize = true;
            this.chxLocationRelativeToDTM.Location = new System.Drawing.Point(202, 12);
            this.chxLocationRelativeToDTM.Name = "chxLocationRelativeToDTM";
            this.chxLocationRelativeToDTM.Size = new System.Drawing.Size(152, 17);
            this.chxLocationRelativeToDTM.TabIndex = 16;
            this.chxLocationRelativeToDTM.Text = "Location Relative To DTM";
            this.chxLocationRelativeToDTM.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(496, 257);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbSchemes
            // 
            this.gbSchemes.Controls.Add(this.lstSchemes);
            this.gbSchemes.Location = new System.Drawing.Point(8, 8);
            this.gbSchemes.Name = "gbSchemes";
            this.gbSchemes.Size = new System.Drawing.Size(178, 258);
            this.gbSchemes.TabIndex = 12;
            this.gbSchemes.TabStop = false;
            this.gbSchemes.Text = "Schemes";
            // 
            // lstSchemes
            // 
            this.lstSchemes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSchemes.FormattingEnabled = true;
            this.lstSchemes.HorizontalScrollbar = true;
            this.lstSchemes.Location = new System.Drawing.Point(3, 16);
            this.lstSchemes.Name = "lstSchemes";
            this.lstSchemes.Size = new System.Drawing.Size(172, 239);
            this.lstSchemes.TabIndex = 1;
            this.lstSchemes.SelectedIndexChanged += new System.EventHandler(this.lstSchemes_SelectedIndexChanged);
            // 
            // ctrlPointsGrid1
            // 
            this.ctrlPointsGrid1.Location = new System.Drawing.Point(199, 55);
            this.ctrlPointsGrid1.Name = "ctrlPointsGrid1";
            this.ctrlPointsGrid1.Size = new System.Drawing.Size(345, 196);
            this.ctrlPointsGrid1.TabIndex = 19;
            // 
            // frmLocationPointsNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 284);
            this.Controls.Add(this.ctrlPointsGrid1);
            this.Controls.Add(this.lblLocationCoordSystem);
            this.Controls.Add(this.cmbLocationCoordinateSystem);
            this.Controls.Add(this.chxLocationRelativeToDTM);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbSchemes);
            this.Name = "frmLocationPointsNew";
            this.Text = "frmLocationPointsNew";
            this.Load += new System.EventHandler(this.frmLocationPointsNew_Load);
            this.gbSchemes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblLocationCoordSystem;
        private System.Windows.Forms.ComboBox cmbLocationCoordinateSystem;
        private System.Windows.Forms.CheckBox chxLocationRelativeToDTM;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.GroupBox gbSchemes;
        private System.Windows.Forms.ListBox lstSchemes;
        private Controls.CtrlPointsGrid ctrlPointsGrid1;
    }
}