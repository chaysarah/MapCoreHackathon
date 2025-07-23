namespace MCTester.GUI.Map
{
    partial class MCTMapForm
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
            this.components = new System.ComponentModel.Container();
            this.MapMenuAddToSchemes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddAllMapFormsToScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddMapToScheme = new System.Windows.Forms.ToolStripMenuItem();
            this.MapMenuAddToSchemes.SuspendLayout();
            this.SuspendLayout();
            // 
            // MapMenuAddToSchemes
            // 
            this.MapMenuAddToSchemes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MapMenuAddToSchemes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddAllMapFormsToScheme,
            this.tsmiAddMapToScheme});
            this.MapMenuAddToSchemes.Name = "MapMenuAddToSchemes";
            this.MapMenuAddToSchemes.Size = new System.Drawing.Size(243, 48);
            // 
            // tsmiAddAllMapFormsToScheme
            // 
            this.tsmiAddAllMapFormsToScheme.Name = "tsmiAddAllMapFormsToScheme";
            this.tsmiAddAllMapFormsToScheme.Size = new System.Drawing.Size(242, 22);
            this.tsmiAddAllMapFormsToScheme.Text = "Add All Map Forms To Schemes";
            this.tsmiAddAllMapFormsToScheme.Click += new System.EventHandler(this.AddAllMapFormsToScheme_Click);
            // 
            // tsmiAddMapToScheme
            // 
            this.tsmiAddMapToScheme.Name = "tsmiAddMapToScheme";
            this.tsmiAddMapToScheme.Size = new System.Drawing.Size(242, 22);
            this.tsmiAddMapToScheme.Text = "Add Map To Schemes";
            this.tsmiAddMapToScheme.Click += new System.EventHandler(this.AddMapToScheme_Click);
            // 
            // MCTMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(384, 379);
            this.Name = "MCTMapForm";
            this.Text = "MCTMapForm";
            this.Activated += new System.EventHandler(this.MCTMapForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MCTMapForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MCTMapForm_FormClosed);
            this.Load += new System.EventHandler(this.MCTMapForm_Load);
            this.MapMenuAddToSchemes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip MapMenuAddToSchemes;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddMapToScheme;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddAllMapFormsToScheme;

    }
}