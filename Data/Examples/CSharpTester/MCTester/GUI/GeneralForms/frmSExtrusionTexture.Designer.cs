namespace MCTester.General_Forms
{
    partial class frmSExtrusionTexture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSExtrusionTexture));
            this.btnOK = new System.Windows.Forms.Button();
            this.ctrlStrTexturePath = new MCTester.Controls.CtrlBrowseControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlXPlacement = new MCTester.Controls.CtrlCheckedListBox();
            this.ctrlYPlacement = new MCTester.Controls.CtrlCheckedListBox();
            this.ctrlTextureScale = new MCTester.Controls.Ctrl2DFVector();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(216, 219);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ctrlStrTexturePath
            // 
            this.ctrlStrTexturePath.AutoSize = true;
            this.ctrlStrTexturePath.FileName = "";
            this.ctrlStrTexturePath.Filter = "";
            this.ctrlStrTexturePath.IsFolderDialog = false;
            this.ctrlStrTexturePath.IsFullPath = true;
            this.ctrlStrTexturePath.IsSaveFile = false;
            this.ctrlStrTexturePath.LabelCaption = "Data Source:";
            this.ctrlStrTexturePath.Location = new System.Drawing.Point(95, 14);
            this.ctrlStrTexturePath.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlStrTexturePath.MultiFilesSelect = false;
            this.ctrlStrTexturePath.Name = "ctrlStrTexturePath";
            this.ctrlStrTexturePath.Prefix = "";
            this.ctrlStrTexturePath.Size = new System.Drawing.Size(404, 24);
            this.ctrlStrTexturePath.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y Placement";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "X Placement";
            // 
            // ctrlXPlacement
            // 
            this.ctrlXPlacement.Location = new System.Drawing.Point(13, 100);
            this.ctrlXPlacement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlXPlacement.Name = "ctrlXPlacement";
            this.ctrlXPlacement.Size = new System.Drawing.Size(224, 112);
            this.ctrlXPlacement.TabIndex = 26;
            // 
            // ctrlYPlacement
            // 
            this.ctrlYPlacement.Location = new System.Drawing.Point(273, 100);
            this.ctrlYPlacement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlYPlacement.Name = "ctrlYPlacement";
            this.ctrlYPlacement.Size = new System.Drawing.Size(226, 112);
            this.ctrlYPlacement.TabIndex = 27;
            // 
            // ctrlTextureScale
            // 
            this.ctrlTextureScale.Location = new System.Drawing.Point(85, 44);
            this.ctrlTextureScale.Name = "ctrlTextureScale";
            this.ctrlTextureScale.Size = new System.Drawing.Size(154, 26);
            this.ctrlTextureScale.TabIndex = 28;
            this.ctrlTextureScale.X = 0F;
            this.ctrlTextureScale.Y = 0F;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 50);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Texture Scale";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Data Source:";
            // 
            // frmSExtrusionTexture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(510, 250);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.ctrlTextureScale);
            this.Controls.Add(this.ctrlYPlacement);
            this.Controls.Add(this.ctrlXPlacement);
            this.Controls.Add(this.ctrlStrTexturePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmSExtrusionTexture";
            this.Text = "Extrusion Texture";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private Controls.CtrlBrowseControl ctrlStrTexturePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Controls.CtrlCheckedListBox ctrlXPlacement;
        private Controls.CtrlCheckedListBox ctrlYPlacement;
        private Controls.Ctrl2DFVector ctrlTextureScale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}