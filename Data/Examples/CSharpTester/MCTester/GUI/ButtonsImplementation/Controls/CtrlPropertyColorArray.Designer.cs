namespace MCTester.Controls
{
    partial class CtrlPropertyColorArray
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
            this.RegPropertyColorArray = new MCTester.Controls.ColorArray();
            this.SelPropertyColorArray = new MCTester.Controls.ColorArray();
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
            this.groupBox1.Size = new System.Drawing.Size(236, 267);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(230, 248);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.RegPropertyColorArray);
            this.tpRegular.Controls.Add(this.btnRegReset);
            this.tpRegular.Size = new System.Drawing.Size(222, 222);
            this.tpRegular.Controls.SetChildIndex(this.btnRegReset, 0);
            this.tpRegular.Controls.SetChildIndex(this.RegPropertyColorArray, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.SelPropertyColorArray);
            this.tpSelection.Controls.Add(this.btnSelReset);
            this.tpSelection.Size = new System.Drawing.Size(222, 222);
            this.tpSelection.Controls.SetChildIndex(this.btnSelReset, 0);
            this.tpSelection.Controls.SetChildIndex(this.SelPropertyColorArray, 0);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            // 
            // RegPropertyColorArray
            // 
            this.RegPropertyColorArray.Location = new System.Drawing.Point(6, 75);
            this.RegPropertyColorArray.Name = "RegPropertyColorArray";
            this.RegPropertyColorArray.Size = new System.Drawing.Size(210, 141);
            this.RegPropertyColorArray.TabIndex = 25;
            // 
            // SelPropertyColorArray
            // 
            this.SelPropertyColorArray.Location = new System.Drawing.Point(6, 75);
            this.SelPropertyColorArray.Name = "SelPropertyColorArray";
            this.SelPropertyColorArray.Size = new System.Drawing.Size(201, 124);
            this.SelPropertyColorArray.TabIndex = 27;
            // 
            // btnRegReset
            // 
            this.btnRegReset.Location = new System.Drawing.Point(130, 49);
            this.btnRegReset.Name = "btnRegReset";
            this.btnRegReset.Size = new System.Drawing.Size(77, 23);
            this.btnRegReset.TabIndex = 26;
            this.btnRegReset.Text = "Clear Table";
            this.btnRegReset.UseVisualStyleBackColor = true;
            this.btnRegReset.Click += new System.EventHandler(this.btnRegReset_Click);
            // 
            // btnSelReset
            // 
            this.btnSelReset.Location = new System.Drawing.Point(130, 49);
            this.btnSelReset.Name = "btnSelReset";
            this.btnSelReset.Size = new System.Drawing.Size(77, 23);
            this.btnSelReset.TabIndex = 28;
            this.btnSelReset.Text = "Clear Table";
            this.btnSelReset.UseVisualStyleBackColor = true;
            this.btnSelReset.Click += new System.EventHandler(this.btnSelReset_Click);
            // 
            // CtrlPropertyColorArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyColorArray";
            this.Size = new System.Drawing.Size(236, 267);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public ColorArray RegPropertyColorArray;
        public ColorArray SelPropertyColorArray;
        private System.Windows.Forms.Button btnRegReset;
        private System.Windows.Forms.Button btnSelReset;
    }
}