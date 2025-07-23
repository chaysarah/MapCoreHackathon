namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyBitFlagEnum
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
            this.lstRegEnum = new System.Windows.Forms.CheckedListBox();
            this.lstSelEnum = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(399, 177);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(393, 158);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lstRegEnum);
            this.tpRegular.Size = new System.Drawing.Size(385, 132);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.lstRegEnum, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.lstSelEnum);
            this.tpObjectState.Size = new System.Drawing.Size(385, 132);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.lstSelEnum, 0);
            // 
            // lstRegEnum
            // 
            this.lstRegEnum.FormattingEnabled = true;
            this.lstRegEnum.Location = new System.Drawing.Point(79, 49);
            this.lstRegEnum.Name = "lstRegEnum";
            this.lstRegEnum.Size = new System.Drawing.Size(228, 64);
            this.lstRegEnum.TabIndex = 24;
            // 
            // lstSelEnum
            // 
            this.lstSelEnum.FormattingEnabled = true;
            this.lstSelEnum.Location = new System.Drawing.Point(79, 49);
            this.lstSelEnum.Name = "lstSelEnum";
            this.lstSelEnum.Size = new System.Drawing.Size(228, 64);
            this.lstSelEnum.TabIndex = 34;
            // 
            // CtrlObjStatePropertyBitFlagEnum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlObjStatePropertyBitFlagEnum";
            this.Size = new System.Drawing.Size(399, 177);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckedListBox lstRegEnum;
        private System.Windows.Forms.CheckedListBox lstSelEnum;
    }
}
