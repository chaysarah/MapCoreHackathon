namespace MCTester.Controls
{
    partial class CtrlExtrusionTextureArray
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
            this.dgvSpecificTextures = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecificTextures)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSpecificTextures
            // 
            this.dgvSpecificTextures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSpecificTextures.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dgvSpecificTextures.Location = new System.Drawing.Point(0, 0);
            this.dgvSpecificTextures.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvSpecificTextures.Name = "dgvSpecificTextures";
            this.dgvSpecificTextures.RowTemplate.Height = 24;
            this.dgvSpecificTextures.Size = new System.Drawing.Size(170, 127);
            this.dgvSpecificTextures.TabIndex = 1;
            this.dgvSpecificTextures.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSpecificTextures_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Specific Texture";
            this.Column1.Name = "Column1";
            // 
            // CtrlExtrusionTextureArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvSpecificTextures);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "CtrlExtrusionTextureArray";
            this.Size = new System.Drawing.Size(172, 128);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpecificTextures)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSpecificTextures;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
    }
}
