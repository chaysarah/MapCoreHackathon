namespace MCTester.Controls
{
    partial class CtrlPropertyMesh
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
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.chxRegNone);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxRegNone, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.chxSelNone);
            this.tpSelection.Controls.SetChildIndex(this.chxSelNone, 0);
            // 
            // chxRegNone
            // 
            this.chxRegNone.AutoSize = true;
            this.chxRegNone.Location = new System.Drawing.Point(256, 30);
            this.chxRegNone.Name = "chxRegNone";
            this.chxRegNone.Size = new System.Drawing.Size(52, 17);
            this.chxRegNone.TabIndex = 25;
            this.chxRegNone.Text = "None";
            this.chxRegNone.UseVisualStyleBackColor = true;
            this.chxRegNone.CheckedChanged += new System.EventHandler(this.chxRegNone_CheckedChanged);
            // 
            // chxSelNone
            // 
            this.chxSelNone.AutoSize = true;
            this.chxSelNone.Location = new System.Drawing.Point(256, 30);
            this.chxSelNone.Name = "chxSelNone";
            this.chxSelNone.Size = new System.Drawing.Size(52, 17);
            this.chxSelNone.TabIndex = 30;
            this.chxSelNone.Text = "None";
            this.chxSelNone.UseVisualStyleBackColor = true;
            this.chxSelNone.CheckedChanged += new System.EventHandler(this.chxSelNone_CheckedChanged);
            // 
            // CtrlPropertyMesh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyMesh";
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
    }
}
