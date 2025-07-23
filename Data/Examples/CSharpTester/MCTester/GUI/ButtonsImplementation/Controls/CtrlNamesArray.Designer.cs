namespace MCTester.Controls
{
    partial class CtrlNamesArray
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
            this.dgvPropertiesList = new System.Windows.Forms.DataGridView();
            this.colPropertyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertiesList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPropertiesList
            // 
            this.dgvPropertiesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropertiesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPropertyID,
            this.colPropertyName});
            this.dgvPropertiesList.Location = new System.Drawing.Point(0, 0);
            this.dgvPropertiesList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPropertiesList.MultiSelect = false;
            this.dgvPropertiesList.Name = "dgvPropertiesList";
            this.dgvPropertiesList.RowHeadersVisible = false;
            this.dgvPropertiesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPropertiesList.Size = new System.Drawing.Size(293, 170);
            this.dgvPropertiesList.TabIndex = 47;
            this.dgvPropertiesList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPropertiesList_CellValueChanged);
            // 
            // colPropertyID
            // 
            this.colPropertyID.HeaderText = "State";
            this.colPropertyID.Name = "colPropertyID";
            // 
            // colPropertyName
            // 
            this.colPropertyName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colPropertyName.HeaderText = "Name";
            this.colPropertyName.Name = "colPropertyName";
            // 
            // CtrlNamesArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPropertiesList);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CtrlNamesArray";
            this.Size = new System.Drawing.Size(303, 182);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertiesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPropertiesList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyName;
    }
}
