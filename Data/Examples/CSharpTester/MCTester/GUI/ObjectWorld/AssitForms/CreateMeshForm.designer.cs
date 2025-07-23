namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class CreateMeshForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_btnCreateMesh = new System.Windows.Forms.Button();
            this.btnXFile = new System.Windows.Forms.Button();
            this.m_lblXfile = new System.Windows.Forms.Label();
            this.txtXFile = new System.Windows.Forms.TextBox();
            this.tcMeshTypes = new System.Windows.Forms.TabControl();
            this.Tab_XFileMesh = new System.Windows.Forms.TabPage();
            this.Tab_NativeMeshFile = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNativeMeshFile = new System.Windows.Forms.TextBox();
            this.btnNativeMesh = new System.Windows.Forms.Button();
            this.Tab_NativeLODMeshFile = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNativeLODFile = new System.Windows.Forms.TextBox();
            this.btnNativeLOD = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbxTransparentColor)).BeginInit();
            this.tcMeshTypes.SuspendLayout();
            this.Tab_XFileMesh.SuspendLayout();
            this.Tab_NativeMeshFile.SuspendLayout();
            this.Tab_NativeLODMeshFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pbxTransparentColor
            // 
            this.m_pbxTransparentColor.Location = new System.Drawing.Point(103, 145);
            // 
            // m_lilTransparentColor
            // 
            this.m_lilTransparentColor.Location = new System.Drawing.Point(1, 150);
            // 
            // m_chkUseExisting
            // 
            this.m_chkUseExisting.Location = new System.Drawing.Point(184, 149);
            // 
            // m_btnCreateMesh
            // 
            this.m_btnCreateMesh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCreateMesh.Location = new System.Drawing.Point(206, 228);
            this.m_btnCreateMesh.Name = "m_btnCreateMesh";
            this.m_btnCreateMesh.Size = new System.Drawing.Size(80, 22);
            this.m_btnCreateMesh.TabIndex = 35;
            this.m_btnCreateMesh.Text = "Create";
            this.m_btnCreateMesh.UseVisualStyleBackColor = true;
            this.m_btnCreateMesh.Click += new System.EventHandler(this.m_btnCreateMesh_Click);
            // 
            // btnXFile
            // 
            this.btnXFile.Location = new System.Drawing.Point(250, 9);
            this.btnXFile.Name = "btnXFile";
            this.btnXFile.Size = new System.Drawing.Size(24, 20);
            this.btnXFile.TabIndex = 47;
            this.btnXFile.Tag = "X";
            this.btnXFile.Text = "...";
            this.btnXFile.UseVisualStyleBackColor = true;
            this.btnXFile.Click += new System.EventHandler(this.m_btnFile_Click);
            // 
            // m_lblXfile
            // 
            this.m_lblXfile.AutoSize = true;
            this.m_lblXfile.Location = new System.Drawing.Point(8, 12);
            this.m_lblXfile.Name = "m_lblXfile";
            this.m_lblXfile.Size = new System.Drawing.Size(36, 13);
            this.m_lblXfile.TabIndex = 46;
            this.m_lblXfile.Text = "X File:";
            // 
            // txtXFile
            // 
            this.txtXFile.Enabled = false;
            this.txtXFile.Location = new System.Drawing.Point(50, 9);
            this.txtXFile.Name = "txtXFile";
            this.txtXFile.Size = new System.Drawing.Size(191, 20);
            this.txtXFile.TabIndex = 45;
            // 
            // tcMeshTypes
            // 
            this.tcMeshTypes.Controls.Add(this.Tab_XFileMesh);
            this.tcMeshTypes.Controls.Add(this.Tab_NativeMeshFile);
            this.tcMeshTypes.Controls.Add(this.Tab_NativeLODMeshFile);
            this.tcMeshTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcMeshTypes.Location = new System.Drawing.Point(0, 0);
            this.tcMeshTypes.Name = "tcMeshTypes";
            this.tcMeshTypes.SelectedIndex = 0;
            this.tcMeshTypes.Size = new System.Drawing.Size(308, 114);
            this.tcMeshTypes.TabIndex = 48;
            // 
            // Tab_XFileMesh
            // 
            this.Tab_XFileMesh.Controls.Add(this.m_lblXfile);
            this.Tab_XFileMesh.Controls.Add(this.txtXFile);
            this.Tab_XFileMesh.Controls.Add(this.btnXFile);
            this.Tab_XFileMesh.Location = new System.Drawing.Point(4, 22);
            this.Tab_XFileMesh.Name = "Tab_XFileMesh";
            this.Tab_XFileMesh.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_XFileMesh.Size = new System.Drawing.Size(300, 88);
            this.Tab_XFileMesh.TabIndex = 0;
            this.Tab_XFileMesh.Text = "X File";
            this.Tab_XFileMesh.UseVisualStyleBackColor = true;
            // 
            // Tab_NativeMeshFile
            // 
            this.Tab_NativeMeshFile.Controls.Add(this.label1);
            this.Tab_NativeMeshFile.Controls.Add(this.txtNativeMeshFile);
            this.Tab_NativeMeshFile.Controls.Add(this.btnNativeMesh);
            this.Tab_NativeMeshFile.Location = new System.Drawing.Point(4, 22);
            this.Tab_NativeMeshFile.Name = "Tab_NativeMeshFile";
            this.Tab_NativeMeshFile.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_NativeMeshFile.Size = new System.Drawing.Size(290, 88);
            this.Tab_NativeMeshFile.TabIndex = 1;
            this.Tab_NativeMeshFile.Text = "Native Mesh";
            this.Tab_NativeMeshFile.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "Native Mesh:";
            // 
            // txtNativeMeshFile
            // 
            this.txtNativeMeshFile.Enabled = false;
            this.txtNativeMeshFile.Location = new System.Drawing.Point(83, 10);
            this.txtNativeMeshFile.Name = "txtNativeMeshFile";
            this.txtNativeMeshFile.Size = new System.Drawing.Size(161, 20);
            this.txtNativeMeshFile.TabIndex = 48;
            // 
            // btnNativeMesh
            // 
            this.btnNativeMesh.Location = new System.Drawing.Point(250, 10);
            this.btnNativeMesh.Name = "btnNativeMesh";
            this.btnNativeMesh.Size = new System.Drawing.Size(24, 20);
            this.btnNativeMesh.TabIndex = 50;
            this.btnNativeMesh.Tag = "mesh";
            this.btnNativeMesh.Text = "...";
            this.btnNativeMesh.UseVisualStyleBackColor = true;
            this.btnNativeMesh.Click += new System.EventHandler(this.m_btnFile_Click);
            // 
            // Tab_NativeLODMeshFile
            // 
            this.Tab_NativeLODMeshFile.Controls.Add(this.label2);
            this.Tab_NativeLODMeshFile.Controls.Add(this.txtNativeLODFile);
            this.Tab_NativeLODMeshFile.Controls.Add(this.btnNativeLOD);
            this.Tab_NativeLODMeshFile.Location = new System.Drawing.Point(4, 22);
            this.Tab_NativeLODMeshFile.Name = "Tab_NativeLODMeshFile";
            this.Tab_NativeLODMeshFile.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_NativeLODMeshFile.Size = new System.Drawing.Size(290, 88);
            this.Tab_NativeLODMeshFile.TabIndex = 2;
            this.Tab_NativeLODMeshFile.Text = "Native LOD";
            this.Tab_NativeLODMeshFile.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Native LOD:";
            // 
            // txtNativeLODFile
            // 
            this.txtNativeLODFile.Enabled = false;
            this.txtNativeLODFile.Location = new System.Drawing.Point(80, 9);
            this.txtNativeLODFile.Name = "txtNativeLODFile";
            this.txtNativeLODFile.Size = new System.Drawing.Size(164, 20);
            this.txtNativeLODFile.TabIndex = 48;
            // 
            // btnNativeLOD
            // 
            this.btnNativeLOD.Location = new System.Drawing.Point(250, 9);
            this.btnNativeLOD.Name = "btnNativeLOD";
            this.btnNativeLOD.Size = new System.Drawing.Size(24, 20);
            this.btnNativeLOD.TabIndex = 50;
            this.btnNativeLOD.Tag = "NativeLOD";
            this.btnNativeLOD.Text = "...";
            this.btnNativeLOD.UseVisualStyleBackColor = true;
            this.btnNativeLOD.Click += new System.EventHandler(this.m_btnFile_Click);
            // 
            // CreateMeshForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 444);
            this.Controls.Add(this.tcMeshTypes);
            this.Controls.Add(this.m_btnCreateMesh);
            this.Name = "CreateMeshForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Mesh Form";
            this.TopMost = true;
            this.Controls.SetChildIndex(this.m_chkUseExisting, 0);
            this.Controls.SetChildIndex(this.m_lilTransparentColor, 0);
            this.Controls.SetChildIndex(this.m_pbxTransparentColor, 0);
            this.Controls.SetChildIndex(this.m_btnCreateMesh, 0);
            this.Controls.SetChildIndex(this.tcMeshTypes, 0);
            ((System.ComponentModel.ISupportInitialize)(this.m_pbxTransparentColor)).EndInit();
            this.tcMeshTypes.ResumeLayout(false);
            this.Tab_XFileMesh.ResumeLayout(false);
            this.Tab_XFileMesh.PerformLayout();
            this.Tab_NativeMeshFile.ResumeLayout(false);
            this.Tab_NativeMeshFile.PerformLayout();
            this.Tab_NativeLODMeshFile.ResumeLayout(false);
            this.Tab_NativeLODMeshFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_btnCreateMesh;
        private System.Windows.Forms.Button btnXFile;
        private System.Windows.Forms.Label m_lblXfile;
        private System.Windows.Forms.TextBox txtXFile;
        private System.Windows.Forms.TabControl tcMeshTypes;
        private System.Windows.Forms.TabPage Tab_XFileMesh;
        private System.Windows.Forms.TabPage Tab_NativeMeshFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNativeMeshFile;
        private System.Windows.Forms.Button btnNativeMesh;
        private System.Windows.Forms.TabPage Tab_NativeLODMeshFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNativeLODFile;
        private System.Windows.Forms.Button btnNativeLOD;
    }
}