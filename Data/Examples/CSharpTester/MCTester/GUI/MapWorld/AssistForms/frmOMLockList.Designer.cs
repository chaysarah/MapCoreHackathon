namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmOMLockList
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
            this.dgvLockList = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLockState = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLockList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLockList
            // 
            this.dgvLockList.AllowUserToAddRows = false;
            this.dgvLockList.AllowUserToDeleteRows = false;
            this.dgvLockList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLockList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colLockState});
            this.dgvLockList.Location = new System.Drawing.Point(12, 12);
            this.dgvLockList.Name = "dgvLockList";
            this.dgvLockList.Size = new System.Drawing.Size(427, 373);
            this.dgvLockList.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(364, 391);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            // 
            // colLockState
            // 
            this.colLockState.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colLockState.HeaderText = "Lock State";
            this.colLockState.Name = "colLockState";
            this.colLockState.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colLockState.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colLockState.Width = 84;
            // 
            // frmOMLockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(451, 419);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvLockList);
            this.Name = "frmOMLockList";
            this.Text = "frmOMLockList";
            this.Load += new System.EventHandler(this.frmOMLockList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLockList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLockList;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colLockState;
    }
}