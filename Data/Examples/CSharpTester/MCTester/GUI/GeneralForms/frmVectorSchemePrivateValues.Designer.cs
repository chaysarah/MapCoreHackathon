namespace MCTester.General_Forms
{
    partial class frmVectorSchemePrivateValues
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPropertyList = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ntxScaleRangeMax = new MCTester.Controls.NumericTextBox();
            this.ntxScaleRangeMin = new MCTester.Controls.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblKeyFieldName = new System.Windows.Forms.Label();
            this.txtKeyFieldName = new System.Windows.Forms.TextBox();
            this.ctrlBrowseObjectScheme = new MCTester.Controls.CtrlBrowseControl();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvPropertyList
            // 
            this.dgvPropertyList.AllowUserToAddRows = false;
            this.dgvPropertyList.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPropertyList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPropertyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPropertyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colType,
            this.colName,
            this.colValue1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPropertyList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPropertyList.Location = new System.Drawing.Point(12, 182);
            this.dgvPropertyList.Name = "dgvPropertyList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPropertyList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPropertyList.Size = new System.Drawing.Size(446, 250);
            this.dgvPropertyList.TabIndex = 1;
            this.dgvPropertyList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPropertyList_CellContentClick);
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            // 
            // colType
            // 
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            // 
            // colName
            // 
            this.colName.HeaderText = "Property Name";
            this.colName.Name = "colName";
            // 
            // colValue1
            // 
            this.colValue1.HeaderText = "Values";
            this.colValue1.Name = "colValue1";
            this.colValue1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colValue1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colValue1.Text = "Null";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(383, 438);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ntxScaleRangeMax);
            this.groupBox1.Controls.Add(this.ntxScaleRangeMin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 80);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scale Range";
            // 
            // ntxScaleRangeMax
            // 
            this.ntxScaleRangeMax.Location = new System.Drawing.Point(50, 45);
            this.ntxScaleRangeMax.Name = "ntxScaleRangeMax";
            this.ntxScaleRangeMax.Size = new System.Drawing.Size(100, 20);
            this.ntxScaleRangeMax.TabIndex = 13;
            this.ntxScaleRangeMax.Text = "MAX";
            // 
            // ntxScaleRangeMin
            // 
            this.ntxScaleRangeMin.Location = new System.Drawing.Point(50, 19);
            this.ntxScaleRangeMin.Name = "ntxScaleRangeMin";
            this.ntxScaleRangeMin.Size = new System.Drawing.Size(100, 20);
            this.ntxScaleRangeMin.TabIndex = 12;
            this.ntxScaleRangeMin.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Min:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Max:";
            // 
            // lblKeyFieldName
            // 
            this.lblKeyFieldName.AutoSize = true;
            this.lblKeyFieldName.Location = new System.Drawing.Point(12, 45);
            this.lblKeyFieldName.Name = "lblKeyFieldName";
            this.lblKeyFieldName.Size = new System.Drawing.Size(84, 13);
            this.lblKeyFieldName.TabIndex = 14;
            this.lblKeyFieldName.Text = "Key Field Name:";
            this.lblKeyFieldName.Visible = false;
            // 
            // txtKeyFieldName
            // 
            this.txtKeyFieldName.Location = new System.Drawing.Point(102, 42);
            this.txtKeyFieldName.Name = "txtKeyFieldName";
            this.txtKeyFieldName.Size = new System.Drawing.Size(207, 20);
            this.txtKeyFieldName.TabIndex = 15;
            this.txtKeyFieldName.Visible = false;
            // 
            // ctrlBrowseObjectScheme
            // 
            this.ctrlBrowseObjectScheme.AutoSize = true;
            this.ctrlBrowseObjectScheme.FileName = "";
            this.ctrlBrowseObjectScheme.Filter = "";
            this.ctrlBrowseObjectScheme.IsFolderDialog = false;
            this.ctrlBrowseObjectScheme.IsFullPath = true;
            this.ctrlBrowseObjectScheme.IsSaveFile = false;
            this.ctrlBrowseObjectScheme.LabelCaption = "Scheme:";
            this.ctrlBrowseObjectScheme.Location = new System.Drawing.Point(99, 12);
            this.ctrlBrowseObjectScheme.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseObjectScheme.MultiFilesSelect = false;
            this.ctrlBrowseObjectScheme.Name = "ctrlBrowseObjectScheme";
            this.ctrlBrowseObjectScheme.Prefix = "";
            this.ctrlBrowseObjectScheme.Size = new System.Drawing.Size(356, 24);
            this.ctrlBrowseObjectScheme.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Scheme:";
            this.label1.Visible = false;
            // 
            // frmVectorSchemePrivateValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(472, 473);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKeyFieldName);
            this.Controls.Add(this.lblKeyFieldName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvPropertyList);
            this.Controls.Add(this.ctrlBrowseObjectScheme);
            this.Name = "frmVectorSchemePrivateValues";
            this.Text = "frmVectorSchemePrivateValues";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPropertyList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtrlBrowseControl ctrlBrowseObjectScheme;
        private System.Windows.Forms.DataGridView dgvPropertyList;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.NumericTextBox ntxScaleRangeMax;
        private Controls.NumericTextBox ntxScaleRangeMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewButtonColumn colValue1;
        private System.Windows.Forms.Label lblKeyFieldName;
        private System.Windows.Forms.TextBox txtKeyFieldName;
        private System.Windows.Forms.Label label1;
    }
}