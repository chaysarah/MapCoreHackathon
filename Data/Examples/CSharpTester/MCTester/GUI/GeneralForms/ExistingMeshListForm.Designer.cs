namespace MCTester.GUI.Forms
{
    partial class ExistingMeshListForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.lstMeshFiles = new System.Windows.Forms.ListBox();
            this.btnDeleteMesh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(265, 244);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstMeshFiles
            // 
            this.lstMeshFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstMeshFiles.FormattingEnabled = true;
            this.lstMeshFiles.Location = new System.Drawing.Point(0, 0);
            this.lstMeshFiles.Name = "lstMeshFiles";
            this.lstMeshFiles.Size = new System.Drawing.Size(340, 238);
            this.lstMeshFiles.TabIndex = 1;
            // 
            // btnDeleteMesh
            // 
            this.btnDeleteMesh.Location = new System.Drawing.Point(0, 244);
            this.btnDeleteMesh.Name = "btnDeleteMesh";
            this.btnDeleteMesh.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteMesh.TabIndex = 2;
            this.btnDeleteMesh.Text = "Delete";
            this.btnDeleteMesh.UseVisualStyleBackColor = true;
            this.btnDeleteMesh.Click += new System.EventHandler(this.btnDeleteMesh_Click);
            // 
            // ExistingMeshListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(340, 268);
            this.Controls.Add(this.btnDeleteMesh);
            this.Controls.Add(this.lstMeshFiles);
            this.Controls.Add(this.btnOK);
            this.Name = "ExistingMeshListForm";
            this.Text = "ExistingMeshListForm";
            this.Load += new System.EventHandler(this.ExistingMeshListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstMeshFiles;
        private System.Windows.Forms.Button btnDeleteMesh;
    }
}