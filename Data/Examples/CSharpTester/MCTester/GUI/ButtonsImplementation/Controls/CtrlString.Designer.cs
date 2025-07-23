namespace MCTester.Controls
{
    partial class CtrlString
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
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnPrvString = new System.Windows.Forms.Button();
            this.btnNextString = new System.Windows.Forms.Button();
            this.lblStringNum = new System.Windows.Forms.Label();
            this.chxIsUnicode = new System.Windows.Forms.CheckBox();
            this.txtString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(205, 36);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(55, 23);
            this.btnRemove.TabIndex = 41;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(142, 36);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 40;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPrvString
            // 
            this.btnPrvString.Location = new System.Drawing.Point(18, 11);
            this.btnPrvString.Name = "btnPrvString";
            this.btnPrvString.Size = new System.Drawing.Size(15, 23);
            this.btnPrvString.TabIndex = 39;
            this.btnPrvString.Text = "<";
            this.btnPrvString.UseVisualStyleBackColor = true;
            this.btnPrvString.Click += new System.EventHandler(this.btnPrvString_Click);
            // 
            // btnNextString
            // 
            this.btnNextString.Location = new System.Drawing.Point(38, 11);
            this.btnNextString.Name = "btnNextString";
            this.btnNextString.Size = new System.Drawing.Size(14, 23);
            this.btnNextString.TabIndex = 38;
            this.btnNextString.Text = ">";
            this.btnNextString.UseVisualStyleBackColor = true;
            this.btnNextString.Click += new System.EventHandler(this.btnNextString_Click);
            // 
            // lblStringNum
            // 
            this.lblStringNum.AutoSize = true;
            this.lblStringNum.Location = new System.Drawing.Point(39, -2);
            this.lblStringNum.Name = "lblStringNum";
            this.lblStringNum.Size = new System.Drawing.Size(16, 13);
            this.lblStringNum.TabIndex = 37;
            this.lblStringNum.Text = "0:";
            // 
            // chxIsUnicode
            // 
            this.chxIsUnicode.AutoSize = true;
            this.chxIsUnicode.Location = new System.Drawing.Point(56, 39);
            this.chxIsUnicode.Name = "chxIsUnicode";
            this.chxIsUnicode.Size = new System.Drawing.Size(77, 17);
            this.chxIsUnicode.TabIndex = 36;
            this.chxIsUnicode.Text = "Is Unicode";
            this.chxIsUnicode.UseVisualStyleBackColor = true;
            // 
            // txtString
            // 
            this.txtString.Location = new System.Drawing.Point(56, 0);
            this.txtString.Multiline = true;
            this.txtString.Name = "txtString";
            this.txtString.Size = new System.Drawing.Size(204, 35);
            this.txtString.TabIndex = 35;
            this.txtString.WordWrap = false;
            // 
            // CtrlString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnPrvString);
            this.Controls.Add(this.btnNextString);
            this.Controls.Add(this.lblStringNum);
            this.Controls.Add(this.chxIsUnicode);
            this.Controls.Add(this.txtString);
            this.Name = "CtrlString";
            this.Size = new System.Drawing.Size(260, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnPrvString;
        private System.Windows.Forms.Button btnNextString;
        private System.Windows.Forms.CheckBox chxIsUnicode;
        private System.Windows.Forms.TextBox txtString;
        public System.Windows.Forms.Label lblStringNum;
    }
}
