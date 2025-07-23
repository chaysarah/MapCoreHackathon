namespace MCTester.Controls
{
    partial class NumberArray
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
            this.txtNumbersArray = new System.Windows.Forms.TextBox();
            this.dgvNumbers = new System.Windows.Forms.DataGridView();
            this.NumRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumbers)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNumbersArray
            // 
            this.txtNumbersArray.Location = new System.Drawing.Point(2, 1);
            this.txtNumbersArray.Name = "txtNumbersArray";
            this.txtNumbersArray.Size = new System.Drawing.Size(291, 22);
            this.txtNumbersArray.TabIndex = 5;
            this.txtNumbersArray.TextChanged += new System.EventHandler(this.txtNumbersArray_TextChanged);
            this.txtNumbersArray.Enter += new System.EventHandler(this.txtNumbersArray_Enter);
            // 
            // dgvNumbers
            // 
            this.dgvNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNumbers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumRow,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvNumbers.Location = new System.Drawing.Point(3, 35);
            this.dgvNumbers.Name = "dgvNumbers";
            this.dgvNumbers.RowTemplate.Height = 24;
            this.dgvNumbers.Size = new System.Drawing.Size(327, 124);
            this.dgvNumbers.TabIndex = 6;
            this.dgvNumbers.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNumbers_CellValueChanged);
            this.dgvNumbers.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvNumbers_RowPostPaint);
            this.dgvNumbers.Enter += new System.EventHandler(this.dgvNumbers_Enter);
            // 
            // NumRow
            // 
            this.NumRow.HeaderText = "No.";
            this.NumRow.Name = "NumRow";
            this.NumRow.ReadOnly = true;
            this.NumRow.Width = 30;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column2";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column3";
            this.Column2.Name = "Column2";
            this.Column2.Width = 70;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column4";
            this.Column3.Name = "Column3";
            this.Column3.Width = 70;
            // 
            // NumberArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvNumbers);
            this.Controls.Add(this.txtNumbersArray);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NumberArray";
            this.Size = new System.Drawing.Size(333, 185);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNumbers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNumbersArray;
        private System.Windows.Forms.DataGridView dgvNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
