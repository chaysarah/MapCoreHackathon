namespace MCTester.Controls
{
	partial class ParentChildManagerControl
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
            this.m_splitter = new System.Windows.Forms.SplitContainer();
            this.grpElements = new System.Windows.Forms.GroupBox();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.m_splitter.Panel1.SuspendLayout();
            this.m_splitter.Panel2.SuspendLayout();
            this.m_splitter.SuspendLayout();
            this.grpElements.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitter
            // 
            this.m_splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitter.Location = new System.Drawing.Point(0, 0);
            this.m_splitter.Name = "m_splitter";
            this.m_splitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitter.Panel1
            // 
            this.m_splitter.Panel1.Controls.Add(this.grpElements);
            // 
            // m_splitter.Panel2
            // 
            this.m_splitter.Panel2.Controls.Add(this.btnAdd);
            this.m_splitter.Panel2.Controls.Add(this.btnRemove);
            this.m_splitter.Size = new System.Drawing.Size(247, 430);
            this.m_splitter.SplitterDistance = 367;
            this.m_splitter.TabIndex = 0;
            // 
            // grpElements
            // 
            this.grpElements.Controls.Add(this.lstItems);
            this.grpElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpElements.Location = new System.Drawing.Point(0, 0);
            this.grpElements.Name = "grpElements";
            this.grpElements.Size = new System.Drawing.Size(247, 367);
            this.grpElements.TabIndex = 0;
            this.grpElements.TabStop = false;
            this.grpElements.Text = "Elements";
            // 
            // lstItems
            // 
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.FormattingEnabled = true;
            this.lstItems.Location = new System.Drawing.Point(3, 16);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(241, 342);
            this.lstItems.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAdd.Location = new System.Drawing.Point(0, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(247, 27);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRemove.Location = new System.Drawing.Point(0, 32);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(247, 27);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // ParentChildManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_splitter);
            this.Name = "ParentChildManagerControl";
            this.Size = new System.Drawing.Size(247, 430);
            this.m_splitter.Panel1.ResumeLayout(false);
            this.m_splitter.Panel2.ResumeLayout(false);
            this.m_splitter.ResumeLayout(false);
            this.grpElements.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer m_splitter;
		private System.Windows.Forms.GroupBox grpElements;
		public System.Windows.Forms.ListBox lstItems;
		private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
	}
}
