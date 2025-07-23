namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmRotationPropertyType
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrl3DOrientation = new MCTester.Controls.Ctrl3DOrientation();
            this.chkRelativeToCurrOrientation = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ctrl3DOrientation
            // 
            this.ctrl3DOrientation.Location = new System.Drawing.Point(15, 3);
            this.ctrl3DOrientation.Name = "ctrl3DOrientation";
            this.ctrl3DOrientation.Pitch = 0F;
            this.ctrl3DOrientation.Roll = 0F;
            this.ctrl3DOrientation.Size = new System.Drawing.Size(289, 22);
            this.ctrl3DOrientation.TabIndex = 3;
            this.ctrl3DOrientation.Yaw = 0F;
            // 
            // chkRelativeToCurrOrientation
            // 
            this.chkRelativeToCurrOrientation.AutoSize = true;
            this.chkRelativeToCurrOrientation.Location = new System.Drawing.Point(153, 31);
            this.chkRelativeToCurrOrientation.Name = "chkRelativeToCurrOrientation";
            this.chkRelativeToCurrOrientation.Size = new System.Drawing.Size(157, 17);
            this.chkRelativeToCurrOrientation.TabIndex = 4;
            this.chkRelativeToCurrOrientation.Text = "Relative To Curr Orientation";
            this.chkRelativeToCurrOrientation.UseVisualStyleBackColor = true;
            // 
            // frmRotationPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.chkRelativeToCurrOrientation);
            this.Controls.Add(this.ctrl3DOrientation);
            this.Name = "frmRotationPropertyType";
            this.Text = "frmRotationPropertyType";
            this.Controls.SetChildIndex(this.ctrl3DOrientation, 0);
            this.Controls.SetChildIndex(this.chkRelativeToCurrOrientation, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.Ctrl3DOrientation ctrl3DOrientation;
        private System.Windows.Forms.CheckBox chkRelativeToCurrOrientation;

    }
}