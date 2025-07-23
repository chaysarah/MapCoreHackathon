namespace MCTester.Controls
{
    partial class CtrlObjStatePropertySelectFile
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
            this.ctrlRegBrowseControl = new MCTester.Controls.CtrlBrowseControl();
            this.ctrlSelBrowseControl = new MCTester.Controls.CtrlBrowseControl();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.label7);
            this.tpRegular.Controls.Add(this.ctrlRegBrowseControl);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegBrowseControl, 0);
            this.tpRegular.Controls.SetChildIndex(this.label7, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.label8);
            this.tpObjectState.Controls.Add(this.ctrlSelBrowseControl);
            this.tpObjectState.Controls.Add(this.label6);
            this.tpObjectState.Controls.Add(this.label4);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label4, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label6, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelBrowseControl, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label8, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
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
            // ctrlRegBrowseControl
            // 
            this.ctrlRegBrowseControl.AutoSize = true;
            this.ctrlRegBrowseControl.FileName = "";
            this.ctrlRegBrowseControl.Filter = "";
            this.ctrlRegBrowseControl.IsFolderDialog = false;
            this.ctrlRegBrowseControl.IsFullPath = true;
            this.ctrlRegBrowseControl.IsSaveFile = false;
            this.ctrlRegBrowseControl.LabelCaption = "File Name:";
            this.ctrlRegBrowseControl.Location = new System.Drawing.Point(133, 46);
            this.ctrlRegBrowseControl.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlRegBrowseControl.MinimumSize = new System.Drawing.Size(200, 24);
            this.ctrlRegBrowseControl.MultiFilesSelect = false;
            this.ctrlRegBrowseControl.Name = "ctrlRegBrowseControl";
            this.ctrlRegBrowseControl.Prefix = "";
            this.ctrlRegBrowseControl.Size = new System.Drawing.Size(249, 24);
            this.ctrlRegBrowseControl.TabIndex = 24;
            this.ctrlRegBrowseControl.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRegBrowseControl_Validating);
            // 
            // ctrlSelBrowseControl
            // 
            this.ctrlSelBrowseControl.AutoSize = true;
            this.ctrlSelBrowseControl.FileName = "";
            this.ctrlSelBrowseControl.Filter = "";
            this.ctrlSelBrowseControl.IsFolderDialog = false;
            this.ctrlSelBrowseControl.IsFullPath = true;
            this.ctrlSelBrowseControl.IsSaveFile = false;
            this.ctrlSelBrowseControl.LabelCaption = "File Name:";
            this.ctrlSelBrowseControl.Location = new System.Drawing.Point(133, 46);
            this.ctrlSelBrowseControl.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSelBrowseControl.MinimumSize = new System.Drawing.Size(200, 24);
            this.ctrlSelBrowseControl.MultiFilesSelect = false;
            this.ctrlSelBrowseControl.Name = "ctrlSelBrowseControl";
            this.ctrlSelBrowseControl.Prefix = "";
            this.ctrlSelBrowseControl.Size = new System.Drawing.Size(249, 24);
            this.ctrlSelBrowseControl.TabIndex = 36;
            this.ctrlSelBrowseControl.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSelBrowseControl_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "File Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 42;
            this.label8.Text = "File Name:";
            // 
            // CtrlObjStatePropertySelectFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertySelectFile";
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
        private CtrlBrowseControl ctrlRegBrowseControl;
        private CtrlBrowseControl ctrlSelBrowseControl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
