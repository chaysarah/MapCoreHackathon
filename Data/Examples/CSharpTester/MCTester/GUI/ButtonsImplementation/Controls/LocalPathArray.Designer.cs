namespace MCTester.Controls
{
    partial class LocalPathArray
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
            this.dgvPathArray = new System.Windows.Forms.DataGridView();
            this.ChooseFolder = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPathArray)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPathArray
            // 
            this.dgvPathArray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPathArray.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChooseFolder,
            this.Path});
            this.dgvPathArray.Location = new System.Drawing.Point(0, 0);
            this.dgvPathArray.Name = "dgvPathArray";
            this.dgvPathArray.Size = new System.Drawing.Size(342, 139);
            this.dgvPathArray.TabIndex = 0;
            this.dgvPathArray.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPathArray_CellContentClick);
            // 
            // ChooseFolder
            // 
            this.ChooseFolder.HeaderText = "Choose";
            this.ChooseFolder.Name = "ChooseFolder";
            this.ChooseFolder.Width = 50;
            // 
            // Path
            // 
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.Width = 230;
            // 
            // LocalPathArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPathArray);
            this.Name = "LocalPathArray";
            this.Size = new System.Drawing.Size(345, 144);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPathArray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPathArray;
        private System.Windows.Forms.DataGridViewButtonColumn ChooseFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
    }
}
