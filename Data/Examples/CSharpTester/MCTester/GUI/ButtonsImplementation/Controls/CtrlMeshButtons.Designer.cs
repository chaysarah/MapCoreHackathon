namespace MCTester.Controls
{
    partial class CtrlMeshButtons
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
            this.btnDeleteMesh = new System.Windows.Forms.Button();
            this.btnUpdateMesh = new System.Windows.Forms.Button();
            this.btnCreateMesh = new System.Windows.Forms.Button();
            this.btnRecreateMesh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDeleteMesh
            // 
            this.btnDeleteMesh.Location = new System.Drawing.Point(204, -1);
            this.btnDeleteMesh.Name = "btnDeleteMesh";
            this.btnDeleteMesh.Size = new System.Drawing.Size(59, 23);
            this.btnDeleteMesh.TabIndex = 74;
            this.btnDeleteMesh.Text = "Delete";
            this.btnDeleteMesh.UseVisualStyleBackColor = true;
            this.btnDeleteMesh.Click += new System.EventHandler(this.btnDeleteMesh_Click);
            // 
            // btnUpdateInReplaceMesh
            // 
            this.btnUpdateMesh.Location = new System.Drawing.Point(133, -1);
            this.btnUpdateMesh.Name = "btnUpdateInReplaceMesh";
            this.btnUpdateMesh.Size = new System.Drawing.Size(69, 23);
            this.btnUpdateMesh.TabIndex = 73;
            this.btnUpdateMesh.Text = "Update";
            this.btnUpdateMesh.UseVisualStyleBackColor = true;
            this.btnUpdateMesh.Click += new System.EventHandler(this.btnUpdateMesh_Click);
            // 
            // btnCreateMesh
            // 
            this.btnCreateMesh.Location = new System.Drawing.Point(-1, -1);
            this.btnCreateMesh.Name = "btnCreateMesh";
            this.btnCreateMesh.Size = new System.Drawing.Size(56, 23);
            this.btnCreateMesh.TabIndex = 72;
            this.btnCreateMesh.Text = "Create";
            this.btnCreateMesh.UseVisualStyleBackColor = true;
            this.btnCreateMesh.Click += new System.EventHandler(this.btnCreateMesh_Click);
            // 
            // btnUpdateMeshNew
            // 
            this.btnRecreateMesh.Location = new System.Drawing.Point(57, -1);
            this.btnRecreateMesh.Name = "btnRecreateMesh";
            this.btnRecreateMesh.Size = new System.Drawing.Size(74, 23);
            this.btnRecreateMesh.TabIndex = 75;
            this.btnRecreateMesh.Text = "Re-create";
            this.btnRecreateMesh.UseVisualStyleBackColor = true;
            this.btnRecreateMesh.Click += new System.EventHandler(this.btnRecreateMesh_Click);
            // 
            // CtrlMeshButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRecreateMesh);
            this.Controls.Add(this.btnDeleteMesh);
            this.Controls.Add(this.btnUpdateMesh);
            this.Controls.Add(this.btnCreateMesh);
            this.Name = "CtrlMeshButtons";
            this.Size = new System.Drawing.Size(268, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDeleteMesh;
        private System.Windows.Forms.Button btnUpdateMesh;
        private System.Windows.Forms.Button btnCreateMesh;
        private System.Windows.Forms.Button btnRecreateMesh;
    }
}
