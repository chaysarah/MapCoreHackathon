namespace MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms
{
    partial class frmVector2DArrayPropertyType
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
            this.dgvVector2D = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVector2D)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVector2D
            // 
            this.dgvVector2D.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVector2D.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colX,
            this.colY});
            this.dgvVector2D.Location = new System.Drawing.Point(15, 87);
            this.dgvVector2D.Name = "dgvVector2D";
            this.dgvVector2D.Size = new System.Drawing.Size(236, 150);
            this.dgvVector2D.TabIndex = 3;
            this.dgvVector2D.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvVector2D_RowPostPaint);
            // 
            // colNo
            // 
            this.colNo.HeaderText = "No.";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Width = 30;
            // 
            // colX
            // 
            this.colX.HeaderText = "X";
            this.colX.Name = "colX";
            this.colX.Width = 70;
            // 
            // colY
            // 
            this.colY.HeaderText = "Y";
            this.colY.Name = "colY";
            this.colY.Width = 70;
            // 
            // frmVector2DArrayPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(426, 324);
            this.Controls.Add(this.dgvVector2D);
            this.Name = "frmVector2DArrayPropertyType";
            this.Text = "frmVector2DArrayPropertyType";
            this.Controls.SetChildIndex(this.dgvVector2D, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVector2D)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVector2D;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colY;
    }
}