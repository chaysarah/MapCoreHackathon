namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyString
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
            this.ctrlRegString = new MCTester.Controls.CtrlString();
            this.ctrlSelString = new MCTester.Controls.CtrlString();
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
            this.groupBox1.Size = new System.Drawing.Size(395, 174);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 18);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcProperty.Size = new System.Drawing.Size(387, 152);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegString);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Size = new System.Drawing.Size(379, 126);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegString, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelString);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(379, 126);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelString, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // ctrlRegString
            // 
            this.ctrlRegString.IsVariantString = true;
            this.ctrlRegString.Location = new System.Drawing.Point(99, 50);
            this.ctrlRegString.Name = "ctrlRegString";
            this.ctrlRegString.Size = new System.Drawing.Size(260, 58);
            this.ctrlRegString.StringNum = -1;
            this.ctrlRegString.TabIndex = 42;
            this.ctrlRegString.TextValue = "";
            this.ctrlRegString.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRegString_Validating);
            // 
            // ctrlSelString
            // 
            this.ctrlSelString.IsVariantString = true;
            this.ctrlSelString.Location = new System.Drawing.Point(99, 50);
            this.ctrlSelString.Name = "ctrlSelString";
            this.ctrlSelString.Size = new System.Drawing.Size(260, 58);
            this.ctrlSelString.StringNum = -1;
            this.ctrlSelString.TabIndex = 37;
            this.ctrlSelString.TextValue = "";
            this.ctrlSelString.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSelString_Validating);
            // 
            // CtrlObjStatePropertyString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyString";
            this.Size = new System.Drawing.Size(395, 174);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private CtrlString ctrlRegString;
        private CtrlString ctrlSelString;
    }
}
