namespace MCTester.Controls
{
    partial class CtrlColorArray
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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.picBxColor = new System.Windows.Forms.PictureBox();
            this.numUDAlpha = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectedColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectedAlpha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectedColor,
            this.SelectedAlpha});
            this.dataGridView2.Location = new System.Drawing.Point(15, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.Size = new System.Drawing.Size(186, 128);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView2_RowStateChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(220, 75);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add New Row";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(220, 106);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(88, 23);
            this.btnSet.TabIndex = 5;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // picBxColor
            // 
            this.picBxColor.BackColor = System.Drawing.Color.White;
            this.picBxColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBxColor.Location = new System.Drawing.Point(290, 4);
            this.picBxColor.Name = "picBxColor";
            this.picBxColor.Size = new System.Drawing.Size(39, 30);
            this.picBxColor.TabIndex = 6;
            this.picBxColor.TabStop = false;
            this.picBxColor.Click += new System.EventHandler(this.picBxColor_Click);
            // 
            // numUDAlpha
            // 
            this.numUDAlpha.Location = new System.Drawing.Point(290, 42);
            this.numUDAlpha.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDAlpha.Name = "numUDAlpha";
            this.numUDAlpha.Size = new System.Drawing.Size(39, 20);
            this.numUDAlpha.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select Color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Select Alpha:";
            // 
            // SelectedColor
            // 
            this.SelectedColor.FillWeight = 70F;
            this.SelectedColor.HeaderText = "Selected Color";
            this.SelectedColor.Name = "SelectedColor";
            this.SelectedColor.ReadOnly = true;
            this.SelectedColor.Width = 70;
            // 
            // SelectedAlpha
            // 
            this.SelectedAlpha.FillWeight = 70F;
            this.SelectedAlpha.HeaderText = "Selected Alpha";
            this.SelectedAlpha.Name = "SelectedAlpha";
            this.SelectedAlpha.ReadOnly = true;
            this.SelectedAlpha.Width = 70;
            // 
            // CtrlColorArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numUDAlpha);
            this.Controls.Add(this.picBxColor);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridView2);
            this.Name = "CtrlColorArray";
            this.Size = new System.Drawing.Size(339, 141);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBxColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.PictureBox picBxColor;
        private System.Windows.Forms.NumericUpDown numUDAlpha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectedColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectedAlpha;
    }
}
