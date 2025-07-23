namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmHeightLinesScaleStepColorList
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
            this.dgvColors = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.colColorValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColor = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvColors
            // 
            this.dgvColors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColorValue,
            this.colColor});
            this.dgvColors.Location = new System.Drawing.Point(2, 1);
            this.dgvColors.Name = "dgvColors";
            this.dgvColors.Size = new System.Drawing.Size(313, 278);
            this.dgvColors.TabIndex = 0;
            this.dgvColors.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColors_CellClick);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(240, 285);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // colColorValue
            // 
            this.colColorValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colColorValue.HeaderText = "Value";
            this.colColorValue.Name = "colColorValue";
            this.colColorValue.ReadOnly = true;
            // 
            // colColor
            // 
            this.colColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colColor.HeaderText = "Color";
            this.colColor.Name = "colColor";
            this.colColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colColor.Width = 70;
            // 
            // frmHeightLinesScaleStepColorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(315, 311);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvColors);
            this.Name = "frmHeightLinesScaleStepColorList";
            this.Text = "frmHeightLinesScaleStepColorList";
            this.Load += new System.EventHandler(this.frmHeightLinesScaleStepColorList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvColors;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColorValue;
        private System.Windows.Forms.DataGridViewButtonColumn colColor;
    }
}