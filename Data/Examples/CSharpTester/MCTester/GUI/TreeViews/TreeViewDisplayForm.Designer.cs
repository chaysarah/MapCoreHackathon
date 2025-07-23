namespace MCTester.GUI.Trees
{
    partial class TreeViewDisplayForm
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
            this.m_splitter = new System.Windows.Forms.SplitContainer();
            this.m_TreeView = new System.Windows.Forms.TreeView();
            this.miSaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadObjects = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitter)).BeginInit();
            this.m_splitter.Panel1.SuspendLayout();
            this.m_splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitter
            // 
            this.m_splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitter.Location = new System.Drawing.Point(0, 0);
            this.m_splitter.Name = "m_splitter";
            // 
            // m_splitter.Panel1
            // 
            this.m_splitter.Panel1.Controls.Add(this.m_TreeView);
            // 
            // m_splitter.Panel2
            // 
            this.m_splitter.Panel2.AutoScroll = true;
            this.m_splitter.Size = new System.Drawing.Size(1119, 786);
            this.m_splitter.SplitterDistance = 273;
            this.m_splitter.TabIndex = 0;
            // 
            // m_TreeView
            // 
            this.m_TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TreeView.HideSelection = false;
            this.m_TreeView.HotTracking = true;
            this.m_TreeView.Location = new System.Drawing.Point(0, 0);
            this.m_TreeView.Name = "m_TreeView";
            this.m_TreeView.Size = new System.Drawing.Size(273, 786);
            this.m_TreeView.TabIndex = 0;
            this.m_TreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.m_TreeView_AfterCollapse);
            this.m_TreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.m_TreeView_BeforeExpand);
            this.m_TreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_TreeView_NodeMouseClick);
            // 
            // miSaveToFile
            // 
            this.miSaveToFile.Name = "miSaveToFile";
            this.miSaveToFile.Size = new System.Drawing.Size(32, 19);
            // 
            // miLoadObjects
            // 
            this.miLoadObjects.Name = "miLoadObjects";
            this.miLoadObjects.Size = new System.Drawing.Size(32, 19);
            // 
            // TreeViewDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1119, 786);
            this.Controls.Add(this.m_splitter);
            this.Name = "TreeViewDisplayForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TreeViewDisplayForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TreeViewDisplayForm_FormClosing);
            this.Load += new System.EventHandler(this.TreeViewDisplayForm_Load);
            this.m_splitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitter)).EndInit();
            this.m_splitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TreeView m_TreeView;
        private System.Windows.Forms.ToolStripMenuItem miSaveToFile;
        private System.Windows.Forms.ToolStripMenuItem miLoadObjects;
        protected System.Windows.Forms.SplitContainer m_splitter;
    }
}