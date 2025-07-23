namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmConnectedList
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
            this.btnOK = new System.Windows.Forms.Button();
            this.lstSchemes = new System.Windows.Forms.ListBox();
            this.dgvParentNode = new System.Windows.Forms.DataGridView();
            this.colParentNode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConnectTo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colConnectOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chxCheckErrorStatusInsteadOfException = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParentNode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(785, 251);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lstSchemes
            // 
            this.lstSchemes.FormattingEnabled = true;
            this.lstSchemes.HorizontalScrollbar = true;
            this.lstSchemes.ItemHeight = 16;
            this.lstSchemes.Location = new System.Drawing.Point(16, 15);
            this.lstSchemes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstSchemes.Name = "lstSchemes";
            this.lstSchemes.Size = new System.Drawing.Size(259, 228);
            this.lstSchemes.TabIndex = 3;
            this.lstSchemes.SelectedIndexChanged += new System.EventHandler(this.lstSchemes_SelectedIndexChanged);
            // 
            // dgvParentNode
            // 
            this.dgvParentNode.AllowUserToAddRows = false;
            this.dgvParentNode.AllowUserToDeleteRows = false;
            this.dgvParentNode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParentNode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParentNode,
            this.colConnectTo,
            this.colConnectOrder});
            this.dgvParentNode.Location = new System.Drawing.Point(284, 15);
            this.dgvParentNode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvParentNode.Name = "dgvParentNode";
            this.dgvParentNode.Size = new System.Drawing.Size(601, 229);
            this.dgvParentNode.TabIndex = 4;
            // 
            // colParentNode
            // 
            this.colParentNode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colParentNode.HeaderText = "Parent Node";
            this.colParentNode.Name = "colParentNode";
            this.colParentNode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colParentNode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colConnectTo
            // 
            this.colConnectTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colConnectTo.HeaderText = "Connect";
            this.colConnectTo.Name = "colConnectTo";
            this.colConnectTo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colConnectTo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colConnectTo.Width = 89;
            // 
            // colConnectOrder
            // 
            this.colConnectOrder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colConnectOrder.HeaderText = "Connect Order";
            this.colConnectOrder.Name = "colConnectOrder";
            this.colConnectOrder.Width = 119;
            // 
            // chxCheckErrorStatusInsteadOfException
            // 
            this.chxCheckErrorStatusInsteadOfException.AutoSize = true;
            this.chxCheckErrorStatusInsteadOfException.Location = new System.Drawing.Point(16, 258);
            this.chxCheckErrorStatusInsteadOfException.Name = "chxCheckErrorStatusInsteadOfException";
            this.chxCheckErrorStatusInsteadOfException.Size = new System.Drawing.Size(276, 21);
            this.chxCheckErrorStatusInsteadOfException.TabIndex = 5;
            this.chxCheckErrorStatusInsteadOfException.Text = "Check error status instead of exception";
            this.chxCheckErrorStatusInsteadOfException.UseVisualStyleBackColor = true;
            // 
            // frmConnectedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(895, 289);
            this.Controls.Add(this.chxCheckErrorStatusInsteadOfException);
            this.Controls.Add(this.dgvParentNode);
            this.Controls.Add(this.lstSchemes);
            this.Controls.Add(this.btnOK);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmConnectedList";
            this.Text = "frmConnectedList";
            ((System.ComponentModel.ISupportInitialize)(this.dgvParentNode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ListBox lstSchemes;
        private System.Windows.Forms.DataGridView dgvParentNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParentNode;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colConnectTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConnectOrder;
        private System.Windows.Forms.CheckBox chxCheckErrorStatusInsteadOfException;
    }
}