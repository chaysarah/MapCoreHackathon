namespace MCTester.Controls
{
    partial class CtrlPropertyVector3DArray
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
            this.RegPropertyVector3DArray = new MCTester.Controls.Vector3DArray();
            this.SelPropertyVector3DArray = new MCTester.Controls.Vector3DArray();
            this.btnRegReset = new System.Windows.Forms.Button();
            this.btnSelReset = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(344, 341);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(338, 322);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.RegPropertyVector3DArray);
            this.tpRegular.Controls.Add(this.btnRegReset);
            this.tpRegular.Size = new System.Drawing.Size(330, 296);
            this.tpRegular.Controls.SetChildIndex(this.btnRegReset, 0);
            this.tpRegular.Controls.SetChildIndex(this.RegPropertyVector3DArray, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.SelPropertyVector3DArray);
            this.tpSelection.Controls.Add(this.btnSelReset);
            this.tpSelection.Size = new System.Drawing.Size(330, 296);
            this.tpSelection.Controls.SetChildIndex(this.btnSelReset, 0);
            this.tpSelection.Controls.SetChildIndex(this.SelPropertyVector3DArray, 0);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            // 
            // RegPropertyVector3DArray
            // 
            this.RegPropertyVector3DArray.Location = new System.Drawing.Point(6, 75);
            this.RegPropertyVector3DArray.Name = "RegPropertyVector3DArray";
            this.RegPropertyVector3DArray.Size = new System.Drawing.Size(318, 218);
            this.RegPropertyVector3DArray.TabIndex = 25;
            // 
            // SelPropertyVector3DArray
            // 
            this.SelPropertyVector3DArray.Location = new System.Drawing.Point(7, 76);
            this.SelPropertyVector3DArray.Name = "SelPropertyVector3DArray";
            this.SelPropertyVector3DArray.Size = new System.Drawing.Size(318, 217);
            this.SelPropertyVector3DArray.TabIndex = 27;
            // 
            // btnRegReset
            // 
            this.btnRegReset.Location = new System.Drawing.Point(239, 46);
            this.btnRegReset.Name = "btnRegReset";
            this.btnRegReset.Size = new System.Drawing.Size(85, 23);
            this.btnRegReset.TabIndex = 26;
            this.btnRegReset.Text = "Clear Table";
            this.btnRegReset.UseVisualStyleBackColor = true;
            this.btnRegReset.Click += new System.EventHandler(this.btnRegReset_Click);
            // 
            // btnSelReset
            // 
            this.btnSelReset.Location = new System.Drawing.Point(249, 47);
            this.btnSelReset.Name = "btnSelReset";
            this.btnSelReset.Size = new System.Drawing.Size(75, 23);
            this.btnSelReset.TabIndex = 28;
            this.btnSelReset.Text = "Clear Table";
            this.btnSelReset.UseVisualStyleBackColor = true;
            this.btnSelReset.Click += new System.EventHandler(this.btnSelReset_Click);
            // 
            // CtrlPropertyVector3DArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyVector3DArray";
            this.Size = new System.Drawing.Size(344, 341);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

      
        private MCTester.Controls.Vector3DArray RegPropertyVector3DArray;
        private MCTester.Controls.Vector3DArray SelPropertyVector3DArray;
        private System.Windows.Forms.Button btnRegReset;
        private System.Windows.Forms.Button btnSelReset;

    }
}
