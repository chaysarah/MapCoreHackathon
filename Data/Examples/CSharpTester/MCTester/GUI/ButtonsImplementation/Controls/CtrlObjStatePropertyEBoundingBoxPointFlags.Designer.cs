namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyEBoundingBoxPointFlags
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
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(400, 318);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 19);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcProperty.Size = new System.Drawing.Size(392, 295);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.lstRegEnum);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Size = new System.Drawing.Size(384, 269);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.lstRegEnum, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.lstSelEnum);
            this.tpObjectState.Controls.Add(this.label6);
            this.tpObjectState.Controls.Add(this.label4);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(384, 269);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label4, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label6, 0);
            this.tpObjectState.Controls.SetChildIndex(this.lstSelEnum, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(101, -11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "label4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(112, -11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 35;
            this.label6.Text = "label6";
            // 
            // lstRegEnum
            // 
            this.lstRegEnum.FormattingEnabled = true;
            this.lstRegEnum.Location = new System.Drawing.Point(76, 49);
            this.lstRegEnum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstRegEnum.Name = "lstRegEnum";
            this.lstRegEnum.Size = new System.Drawing.Size(228, 199);
            this.lstRegEnum.TabIndex = 27;
            this.lstRegEnum.UseCompatibleTextRendering = true;
            this.lstRegEnum.Validating += new System.ComponentModel.CancelEventHandler(this.lstRegEnum_Validating);
            // 
            // lstSelEnum
            // 
            this.lstSelEnum.FormattingEnabled = true;
            this.lstSelEnum.Location = new System.Drawing.Point(76, 49);
            this.lstSelEnum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstSelEnum.Name = "lstSelEnum";
            this.lstSelEnum.Size = new System.Drawing.Size(228, 199);
            this.lstSelEnum.TabIndex = 29;
            this.lstSelEnum.UseCompatibleTextRendering = true;
            this.lstSelEnum.Validating += new System.ComponentModel.CancelEventHandler(this.lstSelEnum_Validating);
            // 
            // CtrlObjStatePropertyEBoundingBoxPointFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyEBoundingBoxPointFlags";
            this.Size = new System.Drawing.Size(400, 318);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox lstRegEnum;
        private System.Windows.Forms.CheckedListBox lstSelEnum;
    }
}
