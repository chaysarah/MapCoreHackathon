namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyDictionaryBool
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
            this.chkRegInheritsMeshRotation = new System.Windows.Forms.CheckBox();
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
            this.groupBox1.Size = new System.Drawing.Size(395, 141);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 21);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4);
            this.tcProperty.Size = new System.Drawing.Size(387, 116);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ntxRegAttachPointID);
            this.tpRegular.Controls.Add(this.chkRegInheritsMeshRotation);
            this.tpRegular.Controls.Add(this.lblRegMeshTextureID);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Size = new System.Drawing.Size(379, 90);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegMeshTextureID, 0);
            this.tpRegular.Controls.SetChildIndex(this.chkRegInheritsMeshRotation, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegAttachPointID, 0);
            // 
            // rdbSelPrivate
            // 
            this.rdbSelPrivate.Location = new System.Drawing.Point(5, 30);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(379, 90);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lblRegMeshTextureID
            // 
            this.lblRegMeshTextureID.AutoSize = true;
            this.lblRegMeshTextureID.Location = new System.Drawing.Point(78, 51);
            this.lblRegMeshTextureID.Name = "lblRegMeshTextureID";
            this.lblRegMeshTextureID.Size = new System.Drawing.Size(82, 13);
            this.lblRegMeshTextureID.TabIndex = 31;
            this.lblRegMeshTextureID.Text = "Attach Point ID:";
            this.lblRegMeshTextureID.Click += new System.EventHandler(this.lblRegMeshTextureID_Click);
            // 
            // chkRegInheritsMeshRotation
            // 
            this.chkRegInheritsMeshRotation.AutoSize = true;
            this.chkRegInheritsMeshRotation.Location = new System.Drawing.Point(246, 50);
            this.chkRegInheritsMeshRotation.Name = "chkRegInheritsMeshRotation";
            this.chkRegInheritsMeshRotation.Size = new System.Drawing.Size(132, 17);
            this.chkRegInheritsMeshRotation.TabIndex = 34;
            this.chkRegInheritsMeshRotation.Text = "Inherits Mesh Rotation";
            this.chkRegInheritsMeshRotation.UseVisualStyleBackColor = true;
            this.chkRegInheritsMeshRotation.Validating += new System.ComponentModel.CancelEventHandler(this.chkRegInheritsMeshRotation_Validating);
            // 
            // ntxRegAttachPointID
            // 
            this.ntxRegAttachPointID.Location = new System.Drawing.Point(166, 48);
            this.ntxRegAttachPointID.Name = "ntxRegAttachPointID";
            this.ntxRegAttachPointID.Size = new System.Drawing.Size(57, 20);
            this.ntxRegAttachPointID.TabIndex = 42;
            this.ntxRegAttachPointID.TextChanged += new System.EventHandler(this.ntxRegAttachPointID_TextChanged);
            // 
            // CtrlObjStatePropertyDictionaryBool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyDictionaryBool";
            this.Size = new System.Drawing.Size(395, 141);
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
        private System.Windows.Forms.CheckBox chkRegInheritsMeshRotation;
        private System.Windows.Forms.TextBox ntxRegAttachPointID;
    }
}
