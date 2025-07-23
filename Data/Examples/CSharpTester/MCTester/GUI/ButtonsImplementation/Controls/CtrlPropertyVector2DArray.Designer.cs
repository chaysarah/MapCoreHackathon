namespace MCTester.Controls
{
    partial class CtrlPropertyVector2DArray
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
            this.RegPropertyVector2DArray = new MCTester.Controls.Vector2DArray();
            this.SelPropertyVector2DArray = new MCTester.Controls.Vector2DArray();
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
            this.groupBox1.Size = new System.Drawing.Size(262, 351);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(256, 332);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.RegPropertyVector2DArray);
            this.tpRegular.Controls.Add(this.btnRegReset);
            this.tpRegular.Size = new System.Drawing.Size(248, 306);
            this.tpRegular.Controls.SetChildIndex(this.btnRegReset, 0);
            this.tpRegular.Controls.SetChildIndex(this.RegPropertyVector2DArray, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.btnSelReset);
            this.tpSelection.Controls.Add(this.SelPropertyVector2DArray);
            this.tpSelection.Size = new System.Drawing.Size(248, 306);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.SelPropertyVector2DArray, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelReset, 0);
            // 
            // RegPropertyVector2DArray
            // 
            this.RegPropertyVector2DArray.Location = new System.Drawing.Point(2, 81);
            this.RegPropertyVector2DArray.Name = "RegPropertyVector2DArray";
            this.RegPropertyVector2DArray.Size = new System.Drawing.Size(243, 131);
            this.RegPropertyVector2DArray.TabIndex = 25;
            // 
            // SelPropertyVector2DArray
            // 
            this.SelPropertyVector2DArray.Location = new System.Drawing.Point(3, 78);
            this.SelPropertyVector2DArray.Name = "SelPropertyVector2DArray";
            this.SelPropertyVector2DArray.Size = new System.Drawing.Size(242, 129);
            this.SelPropertyVector2DArray.TabIndex = 27;
            // 
            // btnRegReset
            // 
            this.btnRegReset.Location = new System.Drawing.Point(167, 54);
            this.btnRegReset.Name = "btnRegReset";
            this.btnRegReset.Size = new System.Drawing.Size(75, 23);
            this.btnRegReset.TabIndex = 26;
            this.btnRegReset.Text = "Clear Table";
            this.btnRegReset.UseVisualStyleBackColor = true;
            this.btnRegReset.Click += new System.EventHandler(this.btnRegReset_Click);
            // 
            // btnSelReset
            // 
            this.btnSelReset.Location = new System.Drawing.Point(167, 52);
            this.btnSelReset.Name = "btnSelReset";
            this.btnSelReset.Size = new System.Drawing.Size(75, 23);
            this.btnSelReset.TabIndex = 28;
            this.btnSelReset.Text = "Clear Table";
            this.btnSelReset.UseVisualStyleBackColor = true;
            this.btnSelReset.Click += new System.EventHandler(this.btnSelReset_Click);
            // 
            // CtrlPropertyVector2DArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyVector2DArray";
            this.Size = new System.Drawing.Size(262, 351);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

      
        private Vector2DArray RegPropertyVector2DArray;
        private Vector2DArray SelPropertyVector2DArray;
        private System.Windows.Forms.Button btnRegReset;
        private System.Windows.Forms.Button btnSelReset;

    }
}
