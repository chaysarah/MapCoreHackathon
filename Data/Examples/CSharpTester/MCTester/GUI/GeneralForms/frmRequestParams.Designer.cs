namespace MCTester.General_Forms
{
    partial class frmRequestParams
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
            this.dgvKeyValueArray = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImportFromFile = new System.Windows.Forms.Button();
            this.txCSWBodyValue = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyValueArray)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvKeyValueArray
            // 
            this.dgvKeyValueArray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeyValueArray.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Col2});
            this.dgvKeyValueArray.Location = new System.Drawing.Point(3, 2);
            this.dgvKeyValueArray.Name = "dgvKeyValueArray";
            this.dgvKeyValueArray.Size = new System.Drawing.Size(270, 256);
            this.dgvKeyValueArray.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Key";
            this.Column1.Name = "Column1";
            // 
            // Col2
            // 
            this.Col2.HeaderText = "Value";
            this.Col2.Name = "Col2";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(283, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImportFromFile);
            this.groupBox1.Controls.Add(this.txCSWBodyValue);
            this.groupBox1.Location = new System.Drawing.Point(3, 264);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 180);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CSW Body";
            // 
            // btnImportFromFile
            // 
            this.btnImportFromFile.Location = new System.Drawing.Point(83, 15);
            this.btnImportFromFile.Name = "btnImportFromFile";
            this.btnImportFromFile.Size = new System.Drawing.Size(100, 23);
            this.btnImportFromFile.TabIndex = 4;
            this.btnImportFromFile.Text = "Import From File";
            this.btnImportFromFile.UseVisualStyleBackColor = true;
            this.btnImportFromFile.Click += new System.EventHandler(this.btnImportFromFile_Click);
            // 
            // txCSWBodyValue
            // 
            this.txCSWBodyValue.Location = new System.Drawing.Point(6, 44);
            this.txCSWBodyValue.Multiline = true;
            this.txCSWBodyValue.Name = "txCSWBodyValue";
            this.txCSWBodyValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txCSWBodyValue.Size = new System.Drawing.Size(257, 128);
            this.txCSWBodyValue.TabIndex = 2;
            this.txCSWBodyValue.WordWrap = false;
            // 
            // frmRequestParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(364, 448);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvKeyValueArray);
            this.Name = "frmRequestParams";
            this.Text = "Server Request Params";
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeyValueArray)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvKeyValueArray;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImportFromFile;
        private System.Windows.Forms.TextBox txCSWBodyValue;
    }
}