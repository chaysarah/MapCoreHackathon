namespace MCTester.Controls
{
    partial class CtrlPropertyString
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
            this.lblRegString = new System.Windows.Forms.Label();
            this.txtRegString = new System.Windows.Forms.TextBox();
            this.lblSelString = new System.Windows.Forms.Label();
            this.chxRegIsUnicode = new System.Windows.Forms.CheckBox();
            this.chxSelIsUnicode = new System.Windows.Forms.CheckBox();
            this.lblRegStringNum = new System.Windows.Forms.Label();
            this.btnRegNextString = new System.Windows.Forms.Button();
            this.btnRegPrvString = new System.Windows.Forms.Button();
            this.btnRegAdd = new System.Windows.Forms.Button();
            this.btnRegRemove = new System.Windows.Forms.Button();
            this.btnSelRemove = new System.Windows.Forms.Button();
            this.btnSelAdd = new System.Windows.Forms.Button();
            this.btnSelPrvString = new System.Windows.Forms.Button();
            this.btnSelNextString = new System.Windows.Forms.Button();
            this.lblSelStringNum = new System.Windows.Forms.Label();
            this.txtSelString = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(400, 171);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(394, 152);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.btnRegRemove);
            this.tpRegular.Controls.Add(this.btnRegAdd);
            this.tpRegular.Controls.Add(this.btnRegPrvString);
            this.tpRegular.Controls.Add(this.btnRegNextString);
            this.tpRegular.Controls.Add(this.lblRegStringNum);
            this.tpRegular.Controls.Add(this.lblRegString);
            this.tpRegular.Controls.Add(this.chxRegIsUnicode);
            this.tpRegular.Controls.Add(this.txtRegString);
            this.tpRegular.Size = new System.Drawing.Size(386, 126);
            this.tpRegular.Controls.SetChildIndex(this.txtRegString, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxRegIsUnicode, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegString, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegStringNum, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegNextString, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegPrvString, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegAdd, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegRemove, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.btnSelRemove);
            this.tpSelection.Controls.Add(this.btnSelAdd);
            this.tpSelection.Controls.Add(this.btnSelPrvString);
            this.tpSelection.Controls.Add(this.btnSelNextString);
            this.tpSelection.Controls.Add(this.lblSelStringNum);
            this.tpSelection.Controls.Add(this.txtSelString);
            this.tpSelection.Controls.Add(this.chxSelIsUnicode);
            this.tpSelection.Controls.Add(this.lblSelString);
            this.tpSelection.Size = new System.Drawing.Size(386, 126);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelString, 0);
            this.tpSelection.Controls.SetChildIndex(this.chxSelIsUnicode, 0);
            this.tpSelection.Controls.SetChildIndex(this.txtSelString, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelStringNum, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelNextString, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelPrvString, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelAdd, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelRemove, 0);
            // 
            // lblRegString
            // 
            this.lblRegString.AutoSize = true;
            this.lblRegString.Location = new System.Drawing.Point(85, 54);
            this.lblRegString.Name = "lblRegString";
            this.lblRegString.Size = new System.Drawing.Size(60, 13);
            this.lblRegString.TabIndex = 21;
            this.lblRegString.Text = "Text Lable:";
            // 
            // txtRegString
            // 
            this.txtRegString.Location = new System.Drawing.Point(154, 72);
            this.txtRegString.Multiline = true;
            this.txtRegString.Name = "txtRegString";
            this.txtRegString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRegString.Size = new System.Drawing.Size(217, 49);
            this.txtRegString.TabIndex = 22;
            this.txtRegString.TextChanged += new System.EventHandler(this.txtRegString_TextChanged);
            // 
            // lblSelString
            // 
            this.lblSelString.AutoSize = true;
            this.lblSelString.Location = new System.Drawing.Point(85, 54);
            this.lblSelString.Name = "lblSelString";
            this.lblSelString.Size = new System.Drawing.Size(60, 13);
            this.lblSelString.TabIndex = 27;
            this.lblSelString.Text = "Text Lable:";
            // 
            // chxRegIsUnicode
            // 
            this.chxRegIsUnicode.AutoSize = true;
            this.chxRegIsUnicode.Location = new System.Drawing.Point(256, 30);
            this.chxRegIsUnicode.Name = "chxRegIsUnicode";
            this.chxRegIsUnicode.Size = new System.Drawing.Size(77, 17);
            this.chxRegIsUnicode.TabIndex = 25;
            this.chxRegIsUnicode.Text = "Is Unicode";
            this.chxRegIsUnicode.UseVisualStyleBackColor = true;
            this.chxRegIsUnicode.CheckedChanged += new System.EventHandler(this.chxRegIsUnicode_CheckedChanged);
            // 
            // chxSelIsUnicode
            // 
            this.chxSelIsUnicode.AutoSize = true;
            this.chxSelIsUnicode.Location = new System.Drawing.Point(256, 30);
            this.chxSelIsUnicode.Name = "chxSelIsUnicode";
            this.chxSelIsUnicode.Size = new System.Drawing.Size(77, 17);
            this.chxSelIsUnicode.TabIndex = 29;
            this.chxSelIsUnicode.Text = "Is Unicode";
            this.chxSelIsUnicode.UseVisualStyleBackColor = true;
            // 
            // lblRegStringNum
            // 
            this.lblRegStringNum.AutoSize = true;
            this.lblRegStringNum.Location = new System.Drawing.Point(132, 72);
            this.lblRegStringNum.Name = "lblRegStringNum";
            this.lblRegStringNum.Size = new System.Drawing.Size(16, 13);
            this.lblRegStringNum.TabIndex = 26;
            this.lblRegStringNum.Text = "0:";
            this.lblRegStringNum.Click += new System.EventHandler(this.lblRegStringNum_Click);
            // 
            // btnRegNextString
            // 
            this.btnRegNextString.Location = new System.Drawing.Point(135, 99);
            this.btnRegNextString.Name = "btnRegNextString";
            this.btnRegNextString.Size = new System.Drawing.Size(15, 23);
            this.btnRegNextString.TabIndex = 31;
            this.btnRegNextString.Text = ">";
            this.btnRegNextString.UseVisualStyleBackColor = true;
            this.btnRegNextString.Click += new System.EventHandler(this.btnRegNextString_Click);
            // 
            // btnRegPrvString
            // 
            this.btnRegPrvString.Location = new System.Drawing.Point(114, 99);
            this.btnRegPrvString.Name = "btnRegPrvString";
            this.btnRegPrvString.Size = new System.Drawing.Size(15, 23);
            this.btnRegPrvString.TabIndex = 32;
            this.btnRegPrvString.Text = "<";
            this.btnRegPrvString.UseVisualStyleBackColor = true;
            this.btnRegPrvString.Click += new System.EventHandler(this.btnRegPrvString_Click);
            // 
            // btnRegAdd
            // 
            this.btnRegAdd.Location = new System.Drawing.Point(315, 49);
            this.btnRegAdd.Name = "btnRegAdd";
            this.btnRegAdd.Size = new System.Drawing.Size(56, 23);
            this.btnRegAdd.TabIndex = 33;
            this.btnRegAdd.Text = "Add";
            this.btnRegAdd.UseVisualStyleBackColor = true;
            this.btnRegAdd.Click += new System.EventHandler(this.btnRegAdd_Click);
            // 
            // btnRegRemove
            // 
            this.btnRegRemove.Location = new System.Drawing.Point(254, 49);
            this.btnRegRemove.Name = "btnRegRemove";
            this.btnRegRemove.Size = new System.Drawing.Size(55, 23);
            this.btnRegRemove.TabIndex = 34;
            this.btnRegRemove.Text = "Remove";
            this.btnRegRemove.UseVisualStyleBackColor = true;
            this.btnRegRemove.Click += new System.EventHandler(this.btnRegRemove_Click);
            // 
            // btnSelRemove
            // 
            this.btnSelRemove.Location = new System.Drawing.Point(254, 49);
            this.btnSelRemove.Name = "btnSelRemove";
            this.btnSelRemove.Size = new System.Drawing.Size(55, 23);
            this.btnSelRemove.TabIndex = 40;
            this.btnSelRemove.Text = "Remove";
            this.btnSelRemove.UseVisualStyleBackColor = true;
            this.btnSelRemove.Click += new System.EventHandler(this.btnSelRemove_Click);
            // 
            // btnSelAdd
            // 
            this.btnSelAdd.Location = new System.Drawing.Point(315, 49);
            this.btnSelAdd.Name = "btnSelAdd";
            this.btnSelAdd.Size = new System.Drawing.Size(56, 23);
            this.btnSelAdd.TabIndex = 39;
            this.btnSelAdd.Text = "Add";
            this.btnSelAdd.UseVisualStyleBackColor = true;
            this.btnSelAdd.Click += new System.EventHandler(this.btnSelAdd_Click);
            // 
            // btnSelPrvString
            // 
            this.btnSelPrvString.Location = new System.Drawing.Point(114, 99);
            this.btnSelPrvString.Name = "btnSelPrvString";
            this.btnSelPrvString.Size = new System.Drawing.Size(15, 23);
            this.btnSelPrvString.TabIndex = 38;
            this.btnSelPrvString.Text = "<";
            this.btnSelPrvString.UseVisualStyleBackColor = true;
            this.btnSelPrvString.Click += new System.EventHandler(this.btnSelPrvString_Click);
            // 
            // btnSelNextString
            // 
            this.btnSelNextString.Location = new System.Drawing.Point(135, 99);
            this.btnSelNextString.Name = "btnSelNextString";
            this.btnSelNextString.Size = new System.Drawing.Size(15, 23);
            this.btnSelNextString.TabIndex = 37;
            this.btnSelNextString.Text = ">";
            this.btnSelNextString.UseVisualStyleBackColor = true;
            this.btnSelNextString.Click += new System.EventHandler(this.btnSelNextString_Click);
            // 
            // lblSelStringNum
            // 
            this.lblSelStringNum.AutoSize = true;
            this.lblSelStringNum.Location = new System.Drawing.Point(132, 72);
            this.lblSelStringNum.Name = "lblSelStringNum";
            this.lblSelStringNum.Size = new System.Drawing.Size(16, 13);
            this.lblSelStringNum.TabIndex = 36;
            this.lblSelStringNum.Text = "0:";
            // 
            // txtSelString
            // 
            this.txtSelString.Location = new System.Drawing.Point(154, 72);
            this.txtSelString.Multiline = true;
            this.txtSelString.Name = "txtSelString";
            this.txtSelString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSelString.Size = new System.Drawing.Size(217, 49);
            this.txtSelString.TabIndex = 35;
            // 
            // CtrlPropertyString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyString";
            this.Size = new System.Drawing.Size(400, 171);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRegString;
        private System.Windows.Forms.TextBox txtRegString;
        private System.Windows.Forms.Label lblSelString;
        private System.Windows.Forms.CheckBox chxRegIsUnicode;
        private System.Windows.Forms.CheckBox chxSelIsUnicode;
        private System.Windows.Forms.Label lblRegStringNum;
        private System.Windows.Forms.Button btnRegRemove;
        private System.Windows.Forms.Button btnRegAdd;
        private System.Windows.Forms.Button btnRegPrvString;
        private System.Windows.Forms.Button btnRegNextString;
        private System.Windows.Forms.Button btnSelRemove;
        private System.Windows.Forms.Button btnSelAdd;
        private System.Windows.Forms.Button btnSelPrvString;
        private System.Windows.Forms.Button btnSelNextString;
        private System.Windows.Forms.Label lblSelStringNum;
        private System.Windows.Forms.TextBox txtSelString;
    }
}
