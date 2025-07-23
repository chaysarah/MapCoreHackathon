namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmNewObjectScheme
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
            this.chxLocationRelativeToDTM = new System.Windows.Forms.CheckBox();
            this.cmbLocationCoordSys = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lstItemsList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstTerrainObjectsConsiderationFlags = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location Coord System :";
            // 
            // chxLocationRelativeToDTM
            // 
            this.chxLocationRelativeToDTM.AutoSize = true;
            this.chxLocationRelativeToDTM.Location = new System.Drawing.Point(16, 44);
            this.chxLocationRelativeToDTM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chxLocationRelativeToDTM.Name = "chxLocationRelativeToDTM";
            this.chxLocationRelativeToDTM.Size = new System.Drawing.Size(194, 21);
            this.chxLocationRelativeToDTM.TabIndex = 2;
            this.chxLocationRelativeToDTM.Text = "Location Relative To DTM";
            this.chxLocationRelativeToDTM.UseVisualStyleBackColor = true;
            // 
            // cmbLocationCoordSys
            // 
            this.cmbLocationCoordSys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationCoordSys.FormattingEnabled = true;
            this.cmbLocationCoordSys.Location = new System.Drawing.Point(187, 7);
            this.cmbLocationCoordSys.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLocationCoordSys.Name = "cmbLocationCoordSys";
            this.cmbLocationCoordSys.Size = new System.Drawing.Size(235, 24);
            this.cmbLocationCoordSys.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(315, 311);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(207, 311);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lstItemsList
            // 
            this.lstItemsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstItemsList.FormattingEnabled = true;
            this.lstItemsList.HorizontalScrollbar = true;
            this.lstItemsList.ItemHeight = 16;
            this.lstItemsList.Location = new System.Drawing.Point(12, 199);
            this.lstItemsList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstItemsList.Name = "lstItemsList";
            this.lstItemsList.Size = new System.Drawing.Size(405, 100);
            this.lstItemsList.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(9, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Item To Connect:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(9, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Terrain objects consideration flags:";
            // 
            // lstTerrainObjectsConsiderationFlags
            // 
            this.lstTerrainObjectsConsiderationFlags.FormattingEnabled = true;
            this.lstTerrainObjectsConsiderationFlags.HorizontalScrollbar = true;
            this.lstTerrainObjectsConsiderationFlags.Location = new System.Drawing.Point(12, 89);
            this.lstTerrainObjectsConsiderationFlags.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstTerrainObjectsConsiderationFlags.Name = "lstTerrainObjectsConsiderationFlags";
            this.lstTerrainObjectsConsiderationFlags.Size = new System.Drawing.Size(403, 72);
            this.lstTerrainObjectsConsiderationFlags.TabIndex = 4;
            // 
            // frmNewObjectScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(435, 346);
            this.Controls.Add(this.lstTerrainObjectsConsiderationFlags);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstItemsList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbLocationCoordSys);
            this.Controls.Add(this.chxLocationRelativeToDTM);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmNewObjectScheme";
            this.Text = "New Object Scheme";
            this.Load += new System.EventHandler(this.frmNewObjectScheme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chxLocationRelativeToDTM;
        private System.Windows.Forms.ComboBox cmbLocationCoordSys;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lstItemsList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox lstTerrainObjectsConsiderationFlags;
    }
}