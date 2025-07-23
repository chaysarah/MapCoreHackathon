namespace MCTester.Controls
{
    partial class CtrlETexturePlacementFlags
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
            this.clsbTexturePlacementFlags = new MCTester.Controls.CtrlCheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clsbTexturePlacementFlags
            // 
            this.clsbTexturePlacementFlags.Location = new System.Drawing.Point(8, 23);
            this.clsbTexturePlacementFlags.Margin = new System.Windows.Forms.Padding(5);
            this.clsbTexturePlacementFlags.Name = "clsbTexturePlacementFlags";
            this.clsbTexturePlacementFlags.Size = new System.Drawing.Size(367, 179);
            this.clsbTexturePlacementFlags.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clsbTexturePlacementFlags);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 210);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Texture Placement Flags";
            // 
            // CtrlETexturePlacementFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CtrlETexturePlacementFlags";
            this.Size = new System.Drawing.Size(591, 330);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MCTester.Controls.CtrlCheckedListBox clsbTexturePlacementFlags;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
