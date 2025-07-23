namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyBool
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
            this.chxRegBool = new System.Windows.Forms.CheckBox();
            this.chxSelBool = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.chxRegBool);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxRegBool, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.chxSelBool);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.chxSelBool, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // chxRegBool
            // 
            this.chxRegBool.AutoSize = true;
            this.chxRegBool.Location = new System.Drawing.Point(78, 50);
            this.chxRegBool.Name = "chxRegBool";
            this.chxRegBool.Size = new System.Drawing.Size(47, 17);
            this.chxRegBool.TabIndex = 24;
            this.chxRegBool.Text = "Text";
            this.chxRegBool.UseVisualStyleBackColor = true;
            this.chxRegBool.Validating += new System.ComponentModel.CancelEventHandler(this.chxRegBool_Validating);
            // 
            // chxSelBool
            // 
            this.chxSelBool.AutoSize = true;
            this.chxSelBool.Location = new System.Drawing.Point(78, 50);
            this.chxSelBool.Name = "chxSelBool";
            this.chxSelBool.Size = new System.Drawing.Size(47, 17);
            this.chxSelBool.TabIndex = 34;
            this.chxSelBool.Text = "Text";
            this.chxSelBool.UseVisualStyleBackColor = true;
            this.chxSelBool.Validating += new System.ComponentModel.CancelEventHandler(this.chxSelBool_Validating);
            // 
            // CtrlObjStatePropertyBool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyBool";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chxRegBool;
        private System.Windows.Forms.CheckBox chxSelBool;

    }
}
