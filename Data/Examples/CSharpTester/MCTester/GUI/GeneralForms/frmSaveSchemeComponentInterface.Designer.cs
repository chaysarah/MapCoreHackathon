namespace MCTester.General_Forms
{
    partial class frmSaveSchemeComponentInterface
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
            this.lstSchemeComponentKind = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.cbGetString = new System.Windows.Forms.CheckBox();
            this.lstComponentType = new System.Windows.Forms.ListBox();
            this.btnSaveAllToFolder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstSchemeComponentKind
            // 
            this.lstSchemeComponentKind.FormattingEnabled = true;
            this.lstSchemeComponentKind.Location = new System.Drawing.Point(7, 45);
            this.lstSchemeComponentKind.Margin = new System.Windows.Forms.Padding(2);
            this.lstSchemeComponentKind.Name = "lstSchemeComponentKind";
            this.lstSchemeComponentKind.Size = new System.Drawing.Size(255, 290);
            this.lstSchemeComponentKind.TabIndex = 1;
            this.lstSchemeComponentKind.SelectedIndexChanged += new System.EventHandler(this.lstSchemeComponentKind_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Object Scheme Node Component Kind && Type Lists:";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(150, 361);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(227, 28);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Save Node Type Interface To File";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // cbGetString
            // 
            this.cbGetString.AutoSize = true;
            this.cbGetString.Location = new System.Drawing.Point(11, 343);
            this.cbGetString.Margin = new System.Windows.Forms.Padding(2);
            this.cbGetString.Name = "cbGetString";
            this.cbGetString.Size = new System.Drawing.Size(73, 17);
            this.cbGetString.TabIndex = 3;
            this.cbGetString.Text = "Get String";
            this.cbGetString.UseVisualStyleBackColor = true;
            // 
            // lstComponentType
            // 
            this.lstComponentType.FormattingEnabled = true;
            this.lstComponentType.Location = new System.Drawing.Point(280, 45);
            this.lstComponentType.Margin = new System.Windows.Forms.Padding(2);
            this.lstComponentType.Name = "lstComponentType";
            this.lstComponentType.Size = new System.Drawing.Size(255, 290);
            this.lstComponentType.TabIndex = 4;
            // 
            // btnSaveAllToFolder
            // 
            this.btnSaveAllToFolder.Location = new System.Drawing.Point(150, 403);
            this.btnSaveAllToFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveAllToFolder.Name = "btnSaveAllToFolder";
            this.btnSaveAllToFolder.Size = new System.Drawing.Size(227, 28);
            this.btnSaveAllToFolder.TabIndex = 5;
            this.btnSaveAllToFolder.Text = "Save All To Folder";
            this.btnSaveAllToFolder.UseVisualStyleBackColor = true;
            this.btnSaveAllToFolder.Click += new System.EventHandler(this.btnSaveAllToFolder_Click);
            // 
            // frmSaveSchemeComponentInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(560, 442);
            this.Controls.Add(this.btnSaveAllToFolder);
            this.Controls.Add(this.lstComponentType);
            this.Controls.Add(this.cbGetString);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lstSchemeComponentKind);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmSaveSchemeComponentInterface";
            this.Text = "Save Scheme Component Interface";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.CheckBox cbGetString;
        private System.Windows.Forms.ListBox lstComponentType;
        private System.Windows.Forms.ListBox lstSchemeComponentKind;
        private System.Windows.Forms.Button btnSaveAllToFolder;
    }
}