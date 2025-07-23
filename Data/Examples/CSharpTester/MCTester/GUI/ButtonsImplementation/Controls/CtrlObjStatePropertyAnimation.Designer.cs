namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyAnimation
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
            this.ctrlRegAnimation = new MCTester.Controls.CtrlAnimation();
            this.ctrlSelAnimation = new MCTester.Controls.CtrlAnimation();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegAnimation);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegAnimation, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelAnimation);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelAnimation, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // ctrlRegAnimation
            // 
            this.ctrlRegAnimation.BoolValue = false;
            this.ctrlRegAnimation.Location = new System.Drawing.Point(75, 47);
            this.ctrlRegAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlRegAnimation.Name = "ctrlRegAnimation";
            this.ctrlRegAnimation.Size = new System.Drawing.Size(279, 21);
            this.ctrlRegAnimation.StringValue = "";
            this.ctrlRegAnimation.TabIndex = 23;
            this.ctrlRegAnimation.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRegAnimation_Validating);
            // 
            // ctrlSelAnimation
            // 
            this.ctrlSelAnimation.BoolValue = false;
            this.ctrlSelAnimation.Location = new System.Drawing.Point(75, 47);
            this.ctrlSelAnimation.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSelAnimation.Name = "ctrlSelAnimation";
            this.ctrlSelAnimation.Size = new System.Drawing.Size(279, 21);
            this.ctrlSelAnimation.StringValue = "";
            this.ctrlSelAnimation.TabIndex = 34;
            this.ctrlSelAnimation.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSelAnimation_Validating);
            // 
            // CtrlObjStatePropertyAnimation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyAnimation";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlAnimation ctrlRegAnimation;
        private CtrlAnimation ctrlSelAnimation;

    }
}
