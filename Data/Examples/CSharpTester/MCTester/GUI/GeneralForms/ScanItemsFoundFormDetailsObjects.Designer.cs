namespace MCTester.General_Forms
{
    partial class ScanItemsFoundFormDetailsObjects
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
            this.tbItemId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbObjectId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGeometryCoordinateSystem = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chxIsRelativeToDTM = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbLocationIndex = new System.Windows.Forms.ComboBox();
            this.ntxNumLocationPoints = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlObjectLocations = new MCTester.Controls.CtrlPointsGrid();
            this.SuspendLayout();
            // 
            // tbItemId
            // 
            this.tbItemId.Location = new System.Drawing.Point(83, 43);
            this.tbItemId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbItemId.Name = "tbItemId";
            this.tbItemId.ReadOnly = true;
            this.tbItemId.Size = new System.Drawing.Size(228, 20);
            this.tbItemId.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 109;
            this.label2.Text = "Item ID:";
            // 
            // tbObjectId
            // 
            this.tbObjectId.Location = new System.Drawing.Point(83, 17);
            this.tbObjectId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbObjectId.Name = "tbObjectId";
            this.tbObjectId.ReadOnly = true;
            this.tbObjectId.Size = new System.Drawing.Size(228, 20);
            this.tbObjectId.TabIndex = 112;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 111;
            this.label3.Text = "Object ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.142858F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(5, 136);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "Object Locations";
            // 
            // txtGeometryCoordinateSystem
            // 
            this.txtGeometryCoordinateSystem.Enabled = false;
            this.txtGeometryCoordinateSystem.Location = new System.Drawing.Point(116, 109);
            this.txtGeometryCoordinateSystem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGeometryCoordinateSystem.Name = "txtGeometryCoordinateSystem";
            this.txtGeometryCoordinateSystem.Size = new System.Drawing.Size(122, 20);
            this.txtGeometryCoordinateSystem.TabIndex = 120;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 113);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 13);
            this.label9.TabIndex = 119;
            this.label9.Text = "Coordinate System:";
            // 
            // chxIsRelativeToDTM
            // 
            this.chxIsRelativeToDTM.AutoSize = true;
            this.chxIsRelativeToDTM.Enabled = false;
            this.chxIsRelativeToDTM.Location = new System.Drawing.Point(284, 109);
            this.chxIsRelativeToDTM.Name = "chxIsRelativeToDTM";
            this.chxIsRelativeToDTM.Size = new System.Drawing.Size(119, 17);
            this.chxIsRelativeToDTM.TabIndex = 116;
            this.chxIsRelativeToDTM.Text = "Is Relative To DTM";
            this.chxIsRelativeToDTM.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(62, 82);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 118;
            this.label8.Text = "Location Index:";
            // 
            // cbLocationIndex
            // 
            this.cbLocationIndex.FormattingEnabled = true;
            this.cbLocationIndex.Location = new System.Drawing.Point(154, 80);
            this.cbLocationIndex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbLocationIndex.Name = "cbLocationIndex";
            this.cbLocationIndex.Size = new System.Drawing.Size(122, 21);
            this.cbLocationIndex.TabIndex = 117;
            this.cbLocationIndex.SelectedIndexChanged += new System.EventHandler(this.cbLocationIndex_SelectedIndexChanged);
            // 
            // ntxNumLocationPoints
            // 
            this.ntxNumLocationPoints.Enabled = false;
            this.ntxNumLocationPoints.Location = new System.Drawing.Point(104, 356);
            this.ntxNumLocationPoints.Name = "ntxNumLocationPoints";
            this.ntxNumLocationPoints.Size = new System.Drawing.Size(73, 20);
            this.ntxNumLocationPoints.TabIndex = 122;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(7, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 123;
            this.label6.Text = "Number of points:";
            // 
            // ctrlPointsGrid1
            // 
            this.ctrlObjectLocations.Location = new System.Drawing.Point(8, 153);
            this.ctrlObjectLocations.Name = "ctrlPointsGrid1";
            this.ctrlObjectLocations.Size = new System.Drawing.Size(345, 196);
            this.ctrlObjectLocations.TabIndex = 124;
            // 
            // ScanItemsFoundFormDetailsObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 410);
            this.Controls.Add(this.ctrlObjectLocations);
            this.Controls.Add(this.cbLocationIndex);
            this.Controls.Add(this.ntxNumLocationPoints);
            this.Controls.Add(this.txtGeometryCoordinateSystem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chxIsRelativeToDTM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbObjectId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbItemId);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ScanItemsFoundFormDetailsObjects";
            this.Text = "Scan Item Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbItemId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbObjectId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGeometryCoordinateSystem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chxIsRelativeToDTM;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbLocationIndex;
        private Controls.NumericTextBox ntxNumLocationPoints;
        private System.Windows.Forms.Label label6;
        private Controls.CtrlPointsGrid ctrlObjectLocations;
    }
}