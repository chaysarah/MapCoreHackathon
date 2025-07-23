namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmBackgroundThreadIndex
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
            this.dgvLayerThreadIndex = new System.Windows.Forms.DataGridView();
            this.colLayer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThreadIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayerThreadIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLayerThreadIndex
            // 
            this.dgvLayerThreadIndex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayerThreadIndex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colLayer,
            this.colThreadIndex});
            this.dgvLayerThreadIndex.Location = new System.Drawing.Point(0, 1);
            this.dgvLayerThreadIndex.Name = "dgvLayerThreadIndex";
            this.dgvLayerThreadIndex.Size = new System.Drawing.Size(400, 280);
            this.dgvLayerThreadIndex.TabIndex = 1;
            // 
            // colLayer
            // 
            this.colLayer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colLayer.HeaderText = "Layer";
            this.colLayer.Name = "colLayer";
            this.colLayer.ReadOnly = true;
            this.colLayer.Width = 58;
            // 
            // colThreadIndex
            // 
            this.colThreadIndex.HeaderText = "Background Thread Index";
            this.colThreadIndex.Name = "colThreadIndex";
            this.colThreadIndex.ReadOnly = true;
            // 
            // frmBackgroundThreadIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(402, 283);
            this.Controls.Add(this.dgvLayerThreadIndex);
            this.Name = "frmBackgroundThreadIndex";
            this.Text = "Background Thread Index Layers";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayerThreadIndex)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLayerThreadIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colThreadIndex;
    }
}