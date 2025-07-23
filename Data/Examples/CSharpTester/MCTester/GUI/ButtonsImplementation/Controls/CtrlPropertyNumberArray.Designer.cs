namespace MCTester.Controls
{
    partial class CtrlPropertyNumberArray
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
            this.RegPropertyNumberArray = new MCTester.Controls.NumberArray();
            this.btnSelReset = new System.Windows.Forms.Button();
            this.SelPropertyNumberArray = new MCTester.Controls.NumberArray();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Size = new System.Drawing.Size(447, 318);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(5, 20);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tcProperty.Size = new System.Drawing.Size(437, 293);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.RegPropertyNumberArray);
            this.tpRegular.Location = new System.Drawing.Point(4, 25);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tpRegular.Size = new System.Drawing.Size(429, 264);
            this.tpRegular.Controls.SetChildIndex(this.RegPropertyNumberArray, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.SelPropertyNumberArray);
            this.tpSelection.Controls.Add(this.btnSelReset);
            this.tpSelection.Location = new System.Drawing.Point(4, 25);
            this.tpSelection.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tpSelection.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tpSelection.Size = new System.Drawing.Size(429, 264);
            this.tpSelection.Controls.SetChildIndex(this.btnSelReset, 0);
            this.tpSelection.Controls.SetChildIndex(this.SelPropertyNumberArray, 0);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            // 
            // RegPropertyNumberArray
            // 
            this.RegPropertyNumberArray.IsSeperateWithTwiceSpace = false;
            this.RegPropertyNumberArray.Location = new System.Drawing.Point(8, 96);
            this.RegPropertyNumberArray.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.RegPropertyNumberArray.Name = "RegPropertyNumberArray";
            this.RegPropertyNumberArray.Size = new System.Drawing.Size(283, 151);
            this.RegPropertyNumberArray.TabIndex = 25;
            // 
            // btnSelReset
            // 
            this.btnSelReset.Location = new System.Drawing.Point(191, 64);
            this.btnSelReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSelReset.Name = "btnSelReset";
            this.btnSelReset.Size = new System.Drawing.Size(100, 28);
            this.btnSelReset.TabIndex = 27;
            this.btnSelReset.Text = "Clear Table";
            this.btnSelReset.UseVisualStyleBackColor = true;
            this.btnSelReset.Click += new System.EventHandler(this.btnSelReset_Click);
            // 
            // SelPropertyNumberArray
            // 
            this.SelPropertyNumberArray.IsSeperateWithTwiceSpace = false;
            this.SelPropertyNumberArray.Location = new System.Drawing.Point(8, 103);
            this.SelPropertyNumberArray.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.SelPropertyNumberArray.Name = "SelPropertyNumberArray";
            this.SelPropertyNumberArray.Size = new System.Drawing.Size(272, 151);
            this.SelPropertyNumberArray.TabIndex = 28;
            // 
            // CtrlPropertyNumberArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "CtrlPropertyNumberArray";
            this.Size = new System.Drawing.Size(447, 318);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NumberArray RegPropertyNumberArray;
        private System.Windows.Forms.Button btnSelReset;
        private NumberArray SelPropertyNumberArray;
    }
}
