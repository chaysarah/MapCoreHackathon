namespace MCTester.Controls
{
    partial class CtrlPropertyTexture
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
            this.chxRegNone = new System.Windows.Forms.CheckBox();
            this.chxSelNone = new System.Windows.Forms.CheckBox();
            this.btnRegEdit = new System.Windows.Forms.Button();
            this.btnSelEdit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(7, 21);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tcProperty.Size = new System.Drawing.Size(519, 133);
            this.tcProperty.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcProperty_Selected);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.btnRegEdit);
            this.tpRegular.Controls.Add(this.chxRegNone);
            this.tpRegular.Location = new System.Drawing.Point(4, 25);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tpRegular.Size = new System.Drawing.Size(511, 104);
            this.tpRegular.Controls.SetChildIndex(this.chxRegNone, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegEdit, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.btnSelEdit);
            this.tpSelection.Controls.Add(this.chxSelNone);
            this.tpSelection.Location = new System.Drawing.Point(4, 25);
            this.tpSelection.Margin = new System.Windows.Forms.Padding(5);
            this.tpSelection.Padding = new System.Windows.Forms.Padding(5);
            this.tpSelection.Size = new System.Drawing.Size(511, 104);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.chxSelNone, 0);
            this.tpSelection.Controls.SetChildIndex(this.btnSelEdit, 0);
            // 
            // chxRegNone
            // 
            this.chxRegNone.AutoSize = true;
            this.chxRegNone.Location = new System.Drawing.Point(358, 37);
            this.chxRegNone.Margin = new System.Windows.Forms.Padding(4);
            this.chxRegNone.Name = "chxRegNone";
            this.chxRegNone.Size = new System.Drawing.Size(64, 21);
            this.chxRegNone.TabIndex = 20;
            this.chxRegNone.Text = "None";
            this.chxRegNone.UseVisualStyleBackColor = true;
            this.chxRegNone.CheckedChanged += new System.EventHandler(this.chxRegNone_ChackedChanged);
            // 
            // chxSelNone
            // 
            this.chxSelNone.AutoSize = true;
            this.chxSelNone.Location = new System.Drawing.Point(341, 37);
            this.chxSelNone.Margin = new System.Windows.Forms.Padding(4);
            this.chxSelNone.Name = "chxSelNone";
            this.chxSelNone.Size = new System.Drawing.Size(64, 21);
            this.chxSelNone.TabIndex = 29;
            this.chxSelNone.Text = "None";
            this.chxSelNone.UseVisualStyleBackColor = true;
            this.chxSelNone.CheckedChanged += new System.EventHandler(this.chxSelNone_ChackedChanged);
            // 
            // btnRegEdit
            // 
            this.btnRegEdit.Location = new System.Drawing.Point(322, 63);
            this.btnRegEdit.Name = "btnRegEdit";
            this.btnRegEdit.Size = new System.Drawing.Size(100, 28);
            this.btnRegEdit.TabIndex = 25;
            this.btnRegEdit.Text = "&Edit";
            this.btnRegEdit.UseVisualStyleBackColor = true;
            this.btnRegEdit.Click += new System.EventHandler(this.btnRegEdit_Click);
            // 
            // btnSelEdit
            // 
            this.btnSelEdit.Location = new System.Drawing.Point(322, 63);
            this.btnSelEdit.Name = "btnSelEdit";
            this.btnSelEdit.Size = new System.Drawing.Size(100, 28);
            this.btnSelEdit.TabIndex = 30;
            this.btnSelEdit.Text = "&Edit";
            this.btnSelEdit.UseVisualStyleBackColor = true;
            this.btnSelEdit.Click += new System.EventHandler(this.btnSelEdit_Click);
            // 
            // CtrlPropertyTexture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "CtrlPropertyTexture";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chxRegNone;
        private System.Windows.Forms.CheckBox chxSelNone;
        private System.Windows.Forms.Button btnRegEdit;
        private System.Windows.Forms.Button btnSelEdit;
    }
}
