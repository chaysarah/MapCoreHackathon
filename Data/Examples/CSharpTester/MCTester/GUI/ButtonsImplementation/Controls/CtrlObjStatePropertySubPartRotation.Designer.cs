namespace MCTester.Controls
{
    partial class CtrlObjStatePropertySubPartRotation
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
            this.lblRegMeshTextureID = new System.Windows.Forms.Label();
            this.ctrl3DRegOrientation = new MCTester.Controls.Ctrl3DOrientation();
            this.chkRegRelativeToCurrOrientation = new System.Windows.Forms.CheckBox();
            this.ntxRegAttachPointID = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(395, 157);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 20);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4);
            this.tcProperty.Size = new System.Drawing.Size(387, 133);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ntxRegAttachPointID);
            this.tpRegular.Controls.Add(this.chkRegRelativeToCurrOrientation);
            this.tpRegular.Controls.Add(this.ctrl3DRegOrientation);
            this.tpRegular.Controls.Add(this.lblRegMeshTextureID);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Size = new System.Drawing.Size(379, 107);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegMeshTextureID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrl3DRegOrientation, 0);
            this.tpRegular.Controls.SetChildIndex(this.chkRegRelativeToCurrOrientation, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegAttachPointID, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(379, 121);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lblRegMeshTextureID
            // 
            this.lblRegMeshTextureID.AutoSize = true;
            this.lblRegMeshTextureID.Location = new System.Drawing.Point(59, 8);
            this.lblRegMeshTextureID.Name = "lblRegMeshTextureID";
            this.lblRegMeshTextureID.Size = new System.Drawing.Size(82, 13);
            this.lblRegMeshTextureID.TabIndex = 31;
            this.lblRegMeshTextureID.Text = "Attach Point ID:";
            // 
            // ctrl3DRegOrientation
            // 
            this.ctrl3DRegOrientation.Location = new System.Drawing.Point(80, 47);
            this.ctrl3DRegOrientation.Margin = new System.Windows.Forms.Padding(4);
            this.ctrl3DRegOrientation.Name = "ctrl3DRegOrientation";
            this.ctrl3DRegOrientation.Pitch = 0F;
            this.ctrl3DRegOrientation.Roll = 0F;
            this.ctrl3DRegOrientation.Size = new System.Drawing.Size(289, 22);
            this.ctrl3DRegOrientation.TabIndex = 33;
            this.ctrl3DRegOrientation.Yaw = 0F;
            this.ctrl3DRegOrientation.Validating += new System.ComponentModel.CancelEventHandler(this.ctrl3DRegOrientation_Validating);
            // 
            // chkRegRelativeToCurrOrientation
            // 
            this.chkRegRelativeToCurrOrientation.AutoSize = true;
            this.chkRegRelativeToCurrOrientation.Location = new System.Drawing.Point(83, 72);
            this.chkRegRelativeToCurrOrientation.Name = "chkRegRelativeToCurrOrientation";
            this.chkRegRelativeToCurrOrientation.Size = new System.Drawing.Size(157, 17);
            this.chkRegRelativeToCurrOrientation.TabIndex = 34;
            this.chkRegRelativeToCurrOrientation.Text = "Relative To Curr Orientation";
            this.chkRegRelativeToCurrOrientation.UseVisualStyleBackColor = true;
            this.chkRegRelativeToCurrOrientation.Validating += new System.ComponentModel.CancelEventHandler(this.chkRegRelativeToCurrOrientation_Validating);
            // 
            // ntxRegAttachPointID
            // 
            this.ntxRegAttachPointID.Location = new System.Drawing.Point(147, 3);
            this.ntxRegAttachPointID.Name = "ntxRegAttachPointID";
            this.ntxRegAttachPointID.Size = new System.Drawing.Size(64, 20);
            this.ntxRegAttachPointID.TabIndex = 42;
            this.ntxRegAttachPointID.TextChanged += new System.EventHandler(this.ntxRegAttachPointID_TextChanged);
            // 
            // CtrlObjStatePropertySubPartRotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertySubPartRotation";
            this.Size = new System.Drawing.Size(395, 157);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblRegMeshTextureID;
        private Ctrl3DOrientation ctrl3DRegOrientation;
        private System.Windows.Forms.CheckBox chkRegRelativeToCurrOrientation;
        private System.Windows.Forms.TextBox ntxRegAttachPointID;
    }
}
