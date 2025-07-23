namespace MCTester.General_Forms
{
    partial class frmDeleteIndexingData
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
            this.btnDeleteIndexingData = new System.Windows.Forms.Button();
            this.ctrlBrowseControlRawData = new MCTester.Controls.CtrlBrowseControl();
            this.cbNonDefaultIndexDirectory = new System.Windows.Forms.CheckBox();
            this.ctrlBrowseIndexingDataDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDeleteIndexingData
            // 
            this.btnDeleteIndexingData.Location = new System.Drawing.Point(287, 92);
            this.btnDeleteIndexingData.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteIndexingData.Name = "btnDeleteIndexingData";
            this.btnDeleteIndexingData.Size = new System.Drawing.Size(122, 25);
            this.btnDeleteIndexingData.TabIndex = 21;
            this.btnDeleteIndexingData.Text = "Delete Indexing Data";
            this.btnDeleteIndexingData.UseVisualStyleBackColor = true;
            this.btnDeleteIndexingData.Click += new System.EventHandler(this.btnDeleteIndexingData_Click);
            // 
            // ctrlBrowseControlRawData
            // 
            this.ctrlBrowseControlRawData.AutoSize = true;
            this.ctrlBrowseControlRawData.FileName = "";
            this.ctrlBrowseControlRawData.Filter = "";
            this.ctrlBrowseControlRawData.IsFolderDialog = false;
            this.ctrlBrowseControlRawData.IsFullPath = true;
            this.ctrlBrowseControlRawData.IsSaveFile = false;
            this.ctrlBrowseControlRawData.LabelCaption = "Data Source:                    ";
            this.ctrlBrowseControlRawData.Location = new System.Drawing.Point(84, 23);
            this.ctrlBrowseControlRawData.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseControlRawData.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseControlRawData.MultiFilesSelect = false;
            this.ctrlBrowseControlRawData.Name = "ctrlBrowseControlRawData";
            this.ctrlBrowseControlRawData.Prefix = "";
            this.ctrlBrowseControlRawData.Size = new System.Drawing.Size(608, 24);
            this.ctrlBrowseControlRawData.TabIndex = 22;
            this.ctrlBrowseControlRawData.FileNameChanged += new System.EventHandler(this.ctrlBrowseControlRawData_FileNameChanged);
            // 
            // cbNonDefaultIndexDirectory
            // 
            this.cbNonDefaultIndexDirectory.AutoSize = true;
            this.cbNonDefaultIndexDirectory.Location = new System.Drawing.Point(7, 59);
            this.cbNonDefaultIndexDirectory.Name = "cbNonDefaultIndexDirectory";
            this.cbNonDefaultIndexDirectory.Size = new System.Drawing.Size(160, 17);
            this.cbNonDefaultIndexDirectory.TabIndex = 39;
            this.cbNonDefaultIndexDirectory.Text = "Non Default Index Directory:";
            this.cbNonDefaultIndexDirectory.UseVisualStyleBackColor = true;
            this.cbNonDefaultIndexDirectory.CheckedChanged += new System.EventHandler(this.cbNonDefaultIndexDirectory_CheckedChanged);
            // 
            // ctrlBrowseIndexingDataDirectory
            // 
            this.ctrlBrowseIndexingDataDirectory.AutoSize = true;
            this.ctrlBrowseIndexingDataDirectory.FileName = "";
            this.ctrlBrowseIndexingDataDirectory.Filter = "";
            this.ctrlBrowseIndexingDataDirectory.IsFolderDialog = true;
            this.ctrlBrowseIndexingDataDirectory.IsFullPath = true;
            this.ctrlBrowseIndexingDataDirectory.IsSaveFile = false;
            this.ctrlBrowseIndexingDataDirectory.LabelCaption = "";
            this.ctrlBrowseIndexingDataDirectory.Location = new System.Drawing.Point(167, 55);
            this.ctrlBrowseIndexingDataDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBrowseIndexingDataDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseIndexingDataDirectory.MultiFilesSelect = false;
            this.ctrlBrowseIndexingDataDirectory.Name = "ctrlBrowseIndexingDataDirectory";
            this.ctrlBrowseIndexingDataDirectory.Prefix = "";
            this.ctrlBrowseIndexingDataDirectory.Size = new System.Drawing.Size(525, 24);
            this.ctrlBrowseIndexingDataDirectory.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Data Source:";
            // 
            // frmDeleteIndexingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(705, 123);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNonDefaultIndexDirectory);
            this.Controls.Add(this.ctrlBrowseIndexingDataDirectory);
            this.Controls.Add(this.ctrlBrowseControlRawData);
            this.Controls.Add(this.btnDeleteIndexingData);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmDeleteIndexingData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDeleteIndexingData;
        private Controls.CtrlBrowseControl ctrlBrowseControlRawData;
        private System.Windows.Forms.CheckBox cbNonDefaultIndexDirectory;
        private Controls.CtrlBrowseControl ctrlBrowseIndexingDataDirectory;
        private System.Windows.Forms.Label label1;
    }
}