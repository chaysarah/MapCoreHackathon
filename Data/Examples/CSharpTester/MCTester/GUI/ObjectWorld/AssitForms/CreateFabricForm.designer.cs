namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class CreateFabricForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_colorSelection = new System.Windows.Forms.ColorDialog();
            this.m_pbxTransparentColor = new System.Windows.Forms.PictureBox();
            this.m_lilTransparentColor = new System.Windows.Forms.LinkLabel();
            this.m_chkUseExisting = new System.Windows.Forms.CheckBox();
            this.dgvColors = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColColor = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colAlpha = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_pbxTransparentColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pbxTransparentColor
            // 
            this.m_pbxTransparentColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_pbxTransparentColor.Enabled = false;
            this.m_pbxTransparentColor.ErrorImage = null;
            this.m_pbxTransparentColor.InitialImage = null;
            this.m_pbxTransparentColor.Location = new System.Drawing.Point(117, 350);
            this.m_pbxTransparentColor.Name = "m_pbxTransparentColor";
            this.m_pbxTransparentColor.Size = new System.Drawing.Size(57, 22);
            this.m_pbxTransparentColor.TabIndex = 28;
            this.m_pbxTransparentColor.TabStop = false;
            // 
            // m_lilTransparentColor
            // 
            this.m_lilTransparentColor.AutoSize = true;
            this.m_lilTransparentColor.Location = new System.Drawing.Point(15, 355);
            this.m_lilTransparentColor.Name = "m_lilTransparentColor";
            this.m_lilTransparentColor.Size = new System.Drawing.Size(91, 13);
            this.m_lilTransparentColor.TabIndex = 27;
            this.m_lilTransparentColor.TabStop = true;
            this.m_lilTransparentColor.Text = "Transparent Color";
            this.m_lilTransparentColor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Color_LinkClicked);
            // 
            // m_chkUseExisting
            // 
            this.m_chkUseExisting.AutoSize = true;
            this.m_chkUseExisting.Checked = true;
            this.m_chkUseExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkUseExisting.Location = new System.Drawing.Point(438, 354);
            this.m_chkUseExisting.Name = "m_chkUseExisting";
            this.m_chkUseExisting.Size = new System.Drawing.Size(84, 17);
            this.m_chkUseExisting.TabIndex = 40;
            this.m_chkUseExisting.Text = "Use Existing";
            this.m_chkUseExisting.UseVisualStyleBackColor = true;
            // 
            // dgvColors
            // 
            this.dgvColors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ColColor,
            this.colAlpha,
            this.Column1,
            this.Column2});
            this.dgvColors.Location = new System.Drawing.Point(12, 378);
            this.dgvColors.Name = "dgvColors";
            this.dgvColors.Size = new System.Drawing.Size(346, 120);
            this.dgvColors.TabIndex = 45;
            this.dgvColors.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvColors_CellContentClick);
            this.dgvColors.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvColors_RowPostPaint);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // ColColor
            // 
            this.ColColor.HeaderText = "Color To Sub";
            this.ColColor.Name = "ColColor";
            this.ColColor.Width = 80;
            // 
            // colAlpha
            // 
            this.colAlpha.HeaderText = "Alpha";
            this.colAlpha.Name = "colAlpha";
            this.colAlpha.Width = 50;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Sub Color";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Alpha";
            this.Column2.Name = "Column2";
            this.Column2.Width = 50;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(364, 475);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 23);
            this.btnClear.TabIndex = 46;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // CreateFabricForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(708, 507);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvColors);
            this.Controls.Add(this.m_chkUseExisting);
            this.Controls.Add(this.m_pbxTransparentColor);
            this.Controls.Add(this.m_lilTransparentColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateFabricForm";
            this.Text = "CreateFabricForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_pbxTransparentColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ColorDialog m_colorSelection;
        protected System.Windows.Forms.PictureBox m_pbxTransparentColor;
        protected System.Windows.Forms.LinkLabel m_lilTransparentColor;
        public System.Windows.Forms.CheckBox m_chkUseExisting;
        private System.Windows.Forms.DataGridView dgvColors;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn ColColor;
        private System.Windows.Forms.DataGridViewComboBoxColumn colAlpha;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column2;
        private System.Windows.Forms.Button btnClear;
    }
}