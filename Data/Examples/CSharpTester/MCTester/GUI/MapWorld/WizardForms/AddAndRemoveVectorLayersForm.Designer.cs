namespace MCTester.MapWorld.WizardForms
{
    partial class AddAndRemoveVectorLayersForm
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
            this.browseVectorLayer1 = new MCTester.Controls.CtrlBrowseControl();
            this.browseVectorLayer2 = new MCTester.Controls.CtrlBrowseControl();
            this.browseVectorLayer3 = new MCTester.Controls.CtrlBrowseControl();
            this.btnAddRemove = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // browseVectorLayer1
            // 
            this.browseVectorLayer1.AutoSize = true;
            this.browseVectorLayer1.FileName = "";
            this.browseVectorLayer1.Filter = "";
            this.browseVectorLayer1.IsFolderDialog = false;
            this.browseVectorLayer1.IsFullPath = true;
            this.browseVectorLayer1.IsSaveFile = false;
            this.browseVectorLayer1.LabelCaption = "Folder Vector Layer 1:";
            this.browseVectorLayer1.Location = new System.Drawing.Point(118, 26);
            this.browseVectorLayer1.MinimumSize = new System.Drawing.Size(300, 24);
            this.browseVectorLayer1.MultiFilesSelect = false;
            this.browseVectorLayer1.Name = "browseVectorLayer1";
            this.browseVectorLayer1.Prefix = "";
            this.browseVectorLayer1.Size = new System.Drawing.Size(407, 24);
            this.browseVectorLayer1.TabIndex = 0;
            // 
            // browseVectorLayer2
            // 
            this.browseVectorLayer2.AutoSize = true;
            this.browseVectorLayer2.FileName = "";
            this.browseVectorLayer2.Filter = "";
            this.browseVectorLayer2.IsFolderDialog = false;
            this.browseVectorLayer2.IsFullPath = true;
            this.browseVectorLayer2.IsSaveFile = false;
            this.browseVectorLayer2.LabelCaption = "Folder Vector Layer 2:";
            this.browseVectorLayer2.Location = new System.Drawing.Point(117, 56);
            this.browseVectorLayer2.MinimumSize = new System.Drawing.Size(300, 24);
            this.browseVectorLayer2.MultiFilesSelect = false;
            this.browseVectorLayer2.Name = "browseVectorLayer2";
            this.browseVectorLayer2.Prefix = "";
            this.browseVectorLayer2.Size = new System.Drawing.Size(408, 24);
            this.browseVectorLayer2.TabIndex = 1;
            // 
            // browseVectorLayer3
            // 
            this.browseVectorLayer3.AutoSize = true;
            this.browseVectorLayer3.FileName = "";
            this.browseVectorLayer3.Filter = "";
            this.browseVectorLayer3.IsFolderDialog = false;
            this.browseVectorLayer3.IsFullPath = true;
            this.browseVectorLayer3.IsSaveFile = false;
            this.browseVectorLayer3.LabelCaption = "Folder Vector Layer 3:";
            this.browseVectorLayer3.Location = new System.Drawing.Point(117, 86);
            this.browseVectorLayer3.MinimumSize = new System.Drawing.Size(300, 24);
            this.browseVectorLayer3.MultiFilesSelect = false;
            this.browseVectorLayer3.Name = "browseVectorLayer3";
            this.browseVectorLayer3.Prefix = "";
            this.browseVectorLayer3.Size = new System.Drawing.Size(408, 24);
            this.browseVectorLayer3.TabIndex = 2;
            // 
            // btnAddRemove
            // 
            this.btnAddRemove.Location = new System.Drawing.Point(411, 141);
            this.btnAddRemove.Name = "btnAddRemove";
            this.btnAddRemove.Size = new System.Drawing.Size(115, 23);
            this.btnAddRemove.TabIndex = 3;
            this.btnAddRemove.UseVisualStyleBackColor = true;
            this.btnAddRemove.Click += new System.EventHandler(this.btnAddRemove_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Folder Vector Layer 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Folder Vector Layer 3:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Folder Vector Layer 2:";
            // 
            // AddAndRemoveVectorLayersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(538, 176);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddRemove);
            this.Controls.Add(this.browseVectorLayer3);
            this.Controls.Add(this.browseVectorLayer2);
            this.Controls.Add(this.browseVectorLayer1);
            this.Name = "AddAndRemoveVectorLayersForm";
            this.Text = "Add And Remove Vector Layers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtrlBrowseControl browseVectorLayer1;
        private Controls.CtrlBrowseControl browseVectorLayer2;
        private Controls.CtrlBrowseControl browseVectorLayer3;
        private System.Windows.Forms.Button btnAddRemove;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}