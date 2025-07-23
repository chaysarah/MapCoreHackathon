namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmPrivatePropertiesDescription
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
            this.dgvPropertyDesc = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyDesc)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPropertyDesc
            // 
            this.dgvPropertyDesc.AllowUserToAddRows = false;
            this.dgvPropertyDesc.AllowUserToDeleteRows = false;
            this.dgvPropertyDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPropertyDesc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropertyDesc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colNode,
            this.colDesc,
            this.Column1});
            this.dgvPropertyDesc.Location = new System.Drawing.Point(1, 1);
            this.dgvPropertyDesc.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPropertyDesc.Name = "dgvPropertyDesc";
            this.dgvPropertyDesc.ReadOnly = true;
            this.dgvPropertyDesc.RowTemplate.Height = 24;
            this.dgvPropertyDesc.Size = new System.Drawing.Size(630, 202);
            this.dgvPropertyDesc.TabIndex = 0;
            this.dgvPropertyDesc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPropertyDesc_CellContentClick);
            this.dgvPropertyDesc.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvPropertyDesc_RowPostPaint);
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Width = 50;
            // 
            // colNode
            // 
            this.colNode.HeaderText = "Node";
            this.colNode.Name = "colNode";
            this.colNode.ReadOnly = true;
            this.colNode.Width = 200;
            // 
            // colDesc
            // 
            this.colDesc.HeaderText = "Property Name";
            this.colDesc.Name = "colDesc";
            this.colDesc.ReadOnly = true;
            this.colDesc.Width = 200;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Jump To Node";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // frmPrivatePropertiesDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(642, 207);
            this.Controls.Add(this.dgvPropertyDesc);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPrivatePropertiesDescription";
            this.Text = "PrivatePropertiesDescription";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyDesc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPropertyDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDesc;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}