namespace MCTester.Controls
{
    partial class frmModifier
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbNames = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTypes = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.rbFont = new System.Windows.Forms.RadioButton();
            this.rbTexture = new System.Windows.Forms.RadioButton();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnCreateTexture = new System.Windows.Forms.Button();
            this.btnCreateFont = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteTexture = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modifier Name:";
            // 
            // cmbNames
            // 
            this.cmbNames.FormattingEnabled = true;
            this.cmbNames.Location = new System.Drawing.Point(110, 16);
            this.cmbNames.Name = "cmbNames";
            this.cmbNames.Size = new System.Drawing.Size(121, 21);
            this.cmbNames.TabIndex = 1;
            this.cmbNames.SelectedIndexChanged += new System.EventHandler(this.cmbNames_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(267, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 20);
            this.txtName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Modifier Type:";
            // 
            // cmbTypes
            // 
            this.cmbTypes.FormattingEnabled = true;
            this.cmbTypes.Location = new System.Drawing.Point(110, 53);
            this.cmbTypes.Name = "cmbTypes";
            this.cmbTypes.Size = new System.Drawing.Size(121, 21);
            this.cmbTypes.TabIndex = 5;
            this.cmbTypes.SelectedIndexChanged += new System.EventHandler(this.cmbTypes_SelectedIndexChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(382, 163);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(100, 23);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            // 
            // rbFont
            // 
            this.rbFont.AutoSize = true;
            this.rbFont.Location = new System.Drawing.Point(11, 19);
            this.rbFont.Name = "rbFont";
            this.rbFont.Size = new System.Drawing.Size(46, 17);
            this.rbFont.TabIndex = 0;
            this.rbFont.TabStop = true;
            this.rbFont.Text = "Font";
            this.rbFont.UseVisualStyleBackColor = true;
            // 
            // rbTexture
            // 
            this.rbTexture.AutoSize = true;
            this.rbTexture.Location = new System.Drawing.Point(188, 19);
            this.rbTexture.Name = "rbTexture";
            this.rbTexture.Size = new System.Drawing.Size(61, 17);
            this.rbTexture.TabIndex = 1;
            this.rbTexture.TabStop = true;
            this.rbTexture.Text = "Texture";
            this.rbTexture.UseVisualStyleBackColor = true;
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(353, 22);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(51, 17);
            this.rbOther.TabIndex = 2;
            this.rbOther.TabStop = true;
            this.rbOther.Text = "Other";
            this.rbOther.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(353, 46);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // btnCreateTexture
            // 
            this.btnCreateTexture.Location = new System.Drawing.Point(188, 45);
            this.btnCreateTexture.Name = "btnCreateTexture";
            this.btnCreateTexture.Size = new System.Drawing.Size(59, 23);
            this.btnCreateTexture.TabIndex = 8;
            this.btnCreateTexture.Text = "Create";
            this.btnCreateTexture.UseVisualStyleBackColor = true;
            // 
            // btnCreateFont
            // 
            this.btnCreateFont.Location = new System.Drawing.Point(11, 42);
            this.btnCreateFont.Name = "btnCreateFont";
            this.btnCreateFont.Size = new System.Drawing.Size(68, 23);
            this.btnCreateFont.TabIndex = 9;
            this.btnCreateFont.Text = "Create";
            this.btnCreateFont.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnDeleteTexture);
            this.groupBox1.Controls.Add(this.rbTexture);
            this.groupBox1.Controls.Add(this.rbFont);
            this.groupBox1.Controls.Add(this.btnCreateFont);
            this.groupBox1.Controls.Add(this.rbOther);
            this.groupBox1.Controls.Add(this.btnCreateTexture);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Location = new System.Drawing.Point(12, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 77);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Value";
            // 
            // btnDeleteTexture
            // 
            this.btnDeleteTexture.Location = new System.Drawing.Point(253, 45);
            this.btnDeleteTexture.Name = "btnDeleteTexture";
            this.btnDeleteTexture.Size = new System.Drawing.Size(58, 23);
            this.btnDeleteTexture.TabIndex = 11;
            this.btnDeleteTexture.Text = "Delete";
            this.btnDeleteTexture.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(85, 42);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(58, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // frmModifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(490, 193);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cmbTypes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmbNames);
            this.Controls.Add(this.label1);
            this.Name = "frmModifier";
            this.Text = "Modifier";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbNames;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTypes;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCreateFont;
        private System.Windows.Forms.Button btnCreateTexture;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbTexture;
        private System.Windows.Forms.RadioButton rbFont;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteTexture;
        private System.Windows.Forms.Button btnDelete;
    }
}