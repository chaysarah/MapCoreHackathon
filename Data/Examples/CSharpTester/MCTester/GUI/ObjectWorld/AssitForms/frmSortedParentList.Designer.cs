namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmSortedParentList
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
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvSortedParents = new System.Windows.Forms.DataGridView();
            this.colParentNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortedParents)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(291, 168);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvSortedParents
            // 
            this.dgvSortedParents.AllowUserToAddRows = false;
            this.dgvSortedParents.AllowUserToDeleteRows = false;
            this.dgvSortedParents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSortedParents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParentNum,
            this.colParent});
            this.dgvSortedParents.Enabled = false;
            this.dgvSortedParents.Location = new System.Drawing.Point(12, 12);
            this.dgvSortedParents.Name = "dgvSortedParents";
            this.dgvSortedParents.ReadOnly = true;
            this.dgvSortedParents.Size = new System.Drawing.Size(354, 150);
            this.dgvSortedParents.TabIndex = 1;
            // 
            // colParentNum
            // 
            this.colParentNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colParentNum.HeaderText = "Number";
            this.colParentNum.Name = "colParentNum";
            this.colParentNum.ReadOnly = true;
            this.colParentNum.Width = 69;
            // 
            // colParent
            // 
            this.colParent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colParent.HeaderText = "Parent";
            this.colParent.Name = "colParent";
            this.colParent.ReadOnly = true;
            // 
            // frmSortedParentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 195);
            this.Controls.Add(this.dgvSortedParents);
            this.Controls.Add(this.btnClose);
            this.Name = "frmSortedParentList";
            this.Text = "frmSortedParentList";
            this.Load += new System.EventHandler(this.frmSortedParentList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortedParents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvSortedParents;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParentNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParent;
    }
}