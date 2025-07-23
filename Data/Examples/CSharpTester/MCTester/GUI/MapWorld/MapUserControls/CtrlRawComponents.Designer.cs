namespace MCTester.MapWorld.MapUserControls
{
    partial class CtrlRawComponents
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGetComponents = new System.Windows.Forms.Button();
            this.lstGetComponentParams = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetComponents);
            this.groupBox1.Controls.Add(this.lstGetComponentParams);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 148);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Get Components";
            // 
            // btnGetComponents
            // 
            this.btnGetComponents.Location = new System.Drawing.Point(566, 117);
            this.btnGetComponents.Name = "btnGetComponents";
            this.btnGetComponents.Size = new System.Drawing.Size(99, 23);
            this.btnGetComponents.TabIndex = 64;
            this.btnGetComponents.Text = "Get Components";
            this.btnGetComponents.UseVisualStyleBackColor = true;
            this.btnGetComponents.Click += new System.EventHandler(this.btnGetComponents_Click);
            // 
            // lstGetComponentParams
            // 
            this.lstGetComponentParams.FormattingEnabled = true;
            this.lstGetComponentParams.HorizontalScrollbar = true;
            this.lstGetComponentParams.Location = new System.Drawing.Point(6, 19);
            this.lstGetComponentParams.Name = "lstGetComponentParams";
            this.lstGetComponentParams.Size = new System.Drawing.Size(554, 121);
            this.lstGetComponentParams.TabIndex = 63;
            // 
            // CtrlRawComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CtrlRawComponents";
            this.Size = new System.Drawing.Size(683, 153);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGetComponents;
        private System.Windows.Forms.ListBox lstGetComponentParams;
    }
}
