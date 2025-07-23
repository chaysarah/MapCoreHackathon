namespace MCTester.Controls
{
    partial class CtrlPropertyFVector2DTextureScroll
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
            this.btnRegApply = new System.Windows.Forms.Button();
            this.ntxRegMeshTextureID = new MCTester.Controls.NumericTextBox();
            this.lblRegMeshTextureID = new System.Windows.Forms.Label();
            this.ntxSelMeshTextureID = new MCTester.Controls.NumericTextBox();
            this.lblSelMeshTextureID = new System.Windows.Forms.Label();
            this.btnRegGet = new System.Windows.Forms.Button();
            this.btnSelApply = new System.Windows.Forms.Button();
            this.btnSelGet = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.btnRegApply);
            this.tpRegular.Controls.Add(this.btnRegGet);
            this.tpRegular.Controls.Add(this.ntxRegMeshTextureID);
            this.tpRegular.Controls.Add(this.lblRegMeshTextureID);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegMeshTextureID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegMeshTextureID, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegGet, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegApply, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.btnSelApply);
            this.tpSelection.Controls.Add(this.btnSelGet);
            this.tpSelection.Controls.Add(this.ntxSelMeshTextureID);
            this.tpSelection.Controls.Add(this.lblSelMeshTextureID);
            this.tpSelection.Controls.SetChildIndex(this.lblSelMeshTextureID, 0);
            this.tpSelection.Controls.SetChildIndex(this.ntxSelMeshTextureID, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelGet, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelApply, 0);
            // 
            // btnRegApply
            // 
            this.btnRegApply.Location = new System.Drawing.Point(316, 3);
            this.btnRegApply.Name = "btnRegApply";
            this.btnRegApply.Size = new System.Drawing.Size(64, 23);
            this.btnRegApply.TabIndex = 28;
            this.btnRegApply.Text = "Apply";
            this.btnRegApply.UseVisualStyleBackColor = true;
            this.btnRegApply.Click += new System.EventHandler(this.btnRegApply_Click);
            // 
            // ntxRegMeshTextureID
            // 
            this.ntxRegMeshTextureID.Location = new System.Drawing.Point(316, 28);
            this.ntxRegMeshTextureID.Name = "ntxRegMeshTextureID";
            this.ntxRegMeshTextureID.Size = new System.Drawing.Size(64, 20);
            this.ntxRegMeshTextureID.TabIndex = 30;
            // 
            // lblRegMeshTextureID
            // 
            this.lblRegMeshTextureID.AutoSize = true;
            this.lblRegMeshTextureID.Location = new System.Drawing.Point(249, 31);
            this.lblRegMeshTextureID.Name = "lblRegMeshTextureID";
            this.lblRegMeshTextureID.Size = new System.Drawing.Size(32, 13);
            this.lblRegMeshTextureID.TabIndex = 29;
            this.lblRegMeshTextureID.Text = "label:";
            // 
            // ntxSelMeshTextureID
            // 
            this.ntxSelMeshTextureID.Location = new System.Drawing.Point(316, 28);
            this.ntxSelMeshTextureID.Name = "ntxSelMeshTextureID";
            this.ntxSelMeshTextureID.Size = new System.Drawing.Size(64, 20);
            this.ntxSelMeshTextureID.TabIndex = 30;
            // 
            // lblSelMeshTextureID
            // 
            this.lblSelMeshTextureID.AutoSize = true;
            this.lblSelMeshTextureID.Location = new System.Drawing.Point(249, 31);
            this.lblSelMeshTextureID.Name = "lblSelMeshTextureID";
            this.lblSelMeshTextureID.Size = new System.Drawing.Size(32, 13);
            this.lblSelMeshTextureID.TabIndex = 29;
            this.lblSelMeshTextureID.Text = "label:";
            // 
            // btnRegGet
            // 
            this.btnRegGet.Location = new System.Drawing.Point(246, 3);
            this.btnRegGet.Name = "btnRegGet";
            this.btnRegGet.Size = new System.Drawing.Size(64, 23);
            this.btnRegGet.TabIndex = 31;
            this.btnRegGet.Text = "Get";
            this.btnRegGet.UseVisualStyleBackColor = true;
            this.btnRegGet.Click += new System.EventHandler(this.btnRegGet_Click);
            // 
            // btnSelApply
            // 
            this.btnSelApply.Location = new System.Drawing.Point(316, 3);
            this.btnSelApply.Name = "btnSelApply";
            this.btnSelApply.Size = new System.Drawing.Size(64, 23);
            this.btnSelApply.TabIndex = 32;
            this.btnSelApply.Text = "Apply";
            this.btnSelApply.UseVisualStyleBackColor = true;
            this.btnSelApply.Click += new System.EventHandler(this.btnSelApply_Click);
            // 
            // btnSelGet
            // 
            this.btnSelGet.Location = new System.Drawing.Point(246, 3);
            this.btnSelGet.Name = "btnSelGet";
            this.btnSelGet.Size = new System.Drawing.Size(64, 23);
            this.btnSelGet.TabIndex = 33;
            this.btnSelGet.Text = "Get";
            this.btnSelGet.UseVisualStyleBackColor = true;
            this.btnSelGet.Click += new System.EventHandler(this.btnSelGet_Click);
            // 
            // CtrlPropertyFVector2DTextureScroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyFVector2DTextureScroll";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegApply;
        public NumericTextBox ntxRegMeshTextureID;
        private System.Windows.Forms.Label lblRegMeshTextureID;
        public NumericTextBox ntxSelMeshTextureID;
        private System.Windows.Forms.Label lblSelMeshTextureID;
        private System.Windows.Forms.Button btnRegGet;
        private System.Windows.Forms.Button btnSelApply;
        private System.Windows.Forms.Button btnSelGet;
    }
}
