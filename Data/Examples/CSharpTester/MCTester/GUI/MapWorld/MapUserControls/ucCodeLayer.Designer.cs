namespace MCTester.MapWorld.MapUserControls
{
    partial class ucCodeLayer
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnGetColorTable = new System.Windows.Forms.Button();
            this.btnSetColorTable = new System.Windows.Forms.Button();
            this.dgvColorTable = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.ColCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tcLayer.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColorTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tcLayer
            // 
            this.tcLayer.Controls.Add(this.tabPage2);
            this.tcLayer.Controls.SetChildIndex(this.tabPage2, 0);
            this.tcLayer.Controls.SetChildIndex(this.tpGeneral, 0);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Margin = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(737, 592);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnClear);
            this.tabPage2.Controls.Add(this.btnGetColorTable);
            this.tabPage2.Controls.Add(this.btnSetColorTable);
            this.tabPage2.Controls.Add(this.dgvColorTable);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(737, 592);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "Code Map Layer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(284, 214);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(104, 22);
            this.btnClear.TabIndex = 115;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnGetColorTable
            // 
            this.btnGetColorTable.Location = new System.Drawing.Point(284, 240);
            this.btnGetColorTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetColorTable.Name = "btnGetColorTable";
            this.btnGetColorTable.Size = new System.Drawing.Size(104, 22);
            this.btnGetColorTable.TabIndex = 114;
            this.btnGetColorTable.Text = "Get Color Table";
            this.btnGetColorTable.UseVisualStyleBackColor = true;
            this.btnGetColorTable.Click += new System.EventHandler(this.btnGetColorTable_Click);
            // 
            // btnSetColorTable
            // 
            this.btnSetColorTable.Location = new System.Drawing.Point(284, 266);
            this.btnSetColorTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetColorTable.Name = "btnSetColorTable";
            this.btnSetColorTable.Size = new System.Drawing.Size(104, 22);
            this.btnSetColorTable.TabIndex = 113;
            this.btnSetColorTable.Text = "Set Color Table";
            this.btnSetColorTable.UseVisualStyleBackColor = true;
            this.btnSetColorTable.Click += new System.EventHandler(this.btnSetColorTable_Click);
            // 
            // dgvColorTable
            // 
            this.dgvColorTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColorTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColCode,
            this.ColColor});
            this.dgvColorTable.Location = new System.Drawing.Point(28, 41);
            this.dgvColorTable.Name = "dgvColorTable";
            this.dgvColorTable.Size = new System.Drawing.Size(251, 247);
            this.dgvColorTable.TabIndex = 109;
            this.dgvColorTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColorTable_CellContentClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 15);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Color Table";
            // 
            // ColCode
            // 
            this.ColCode.HeaderText = "Code";
            this.ColCode.Name = "ColCode";
            // 
            // ColColor
            // 
            this.ColColor.HeaderText = "Color";
            this.ColColor.Name = "ColColor";
            this.ColColor.Width = 50;
            // 
            // ucCodeLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucCodeLayer";
            this.tcLayer.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColorTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dgvColorTable;
        protected System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSetColorTable;
        private System.Windows.Forms.Button btnGetColorTable;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColCode;
        private System.Windows.Forms.DataGridViewButtonColumn ColColor;
    }
}
