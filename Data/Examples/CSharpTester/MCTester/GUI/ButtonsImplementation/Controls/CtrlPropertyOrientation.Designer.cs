namespace MCTester.Controls
{
    partial class CtrlPropertyOrientation
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
            this.ctrl3DRegOrientation = new MCTester.Controls.Ctrl3DOrientation();
            this.chkRegRelativeToCurrOrientation = new System.Windows.Forms.CheckBox();
            this.ctrl3DSelOrientation = new MCTester.Controls.Ctrl3DOrientation();
            this.chkSelRelativeToCurrOrientation = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(400, 165);
            // 
            // tcProperty
            // 
            this.tcProperty.Size = new System.Drawing.Size(394, 146);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrl3DRegOrientation);
            this.tpRegular.Controls.Add(this.btnApply);
            this.tpRegular.Controls.Add(this.chkRegRelativeToCurrOrientation);
            this.tpRegular.Size = new System.Drawing.Size(386, 120);
            this.tpRegular.Controls.SetChildIndex(this.chkRegRelativeToCurrOrientation, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnApply, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrl3DRegOrientation, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.chkSelRelativeToCurrOrientation);
            this.tpSelection.Controls.Add(this.ctrl3DSelOrientation);
            this.tpSelection.Size = new System.Drawing.Size(386, 120);
            this.tpSelection.Controls.SetChildIndex(this.ctrl3DSelOrientation, 0);
            this.tpSelection.Controls.SetChildIndex(this.chkSelRelativeToCurrOrientation, 0);
            // 
            // ctrl3DRegOrientation
            // 
            this.ctrl3DRegOrientation.Location = new System.Drawing.Point(88, 48);
            this.ctrl3DRegOrientation.Name = "ctrl3DRegOrientation";
            this.ctrl3DRegOrientation.Pitch = 0F;
            this.ctrl3DRegOrientation.Roll = 0F;
            this.ctrl3DRegOrientation.Size = new System.Drawing.Size(289, 22);
            this.ctrl3DRegOrientation.TabIndex = 25;
            this.ctrl3DRegOrientation.Yaw = 0F;
            // 
            // chkRegRelativeToCurrOrientation
            // 
            this.chkRegRelativeToCurrOrientation.AutoSize = true;
            this.chkRegRelativeToCurrOrientation.Location = new System.Drawing.Point(88, 76);
            this.chkRegRelativeToCurrOrientation.Name = "chkRegRelativeToCurrOrientation";
            this.chkRegRelativeToCurrOrientation.Size = new System.Drawing.Size(157, 17);
            this.chkRegRelativeToCurrOrientation.TabIndex = 26;
            this.chkRegRelativeToCurrOrientation.Text = "Relative To Curr Orientation";
            this.chkRegRelativeToCurrOrientation.UseVisualStyleBackColor = true;
            // 
            // ctrl3DSelOrientation
            // 
            this.ctrl3DSelOrientation.Location = new System.Drawing.Point(88, 49);
            this.ctrl3DSelOrientation.Name = "ctrl3DSelOrientation";
            this.ctrl3DSelOrientation.Pitch = 0F;
            this.ctrl3DSelOrientation.Roll = 0F;
            this.ctrl3DSelOrientation.Size = new System.Drawing.Size(289, 22);
            this.ctrl3DSelOrientation.TabIndex = 27;
            this.ctrl3DSelOrientation.Yaw = 0F;
            // 
            // chkSelRelativeToCurrOrientation
            // 
            this.chkSelRelativeToCurrOrientation.AutoSize = true;
            this.chkSelRelativeToCurrOrientation.Location = new System.Drawing.Point(88, 77);
            this.chkSelRelativeToCurrOrientation.Name = "chkSelRelativeToCurrOrientation";
            this.chkSelRelativeToCurrOrientation.Size = new System.Drawing.Size(157, 17);
            this.chkSelRelativeToCurrOrientation.TabIndex = 28;
            this.chkSelRelativeToCurrOrientation.Text = "Relative To Curr Orientation";
            this.chkSelRelativeToCurrOrientation.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(305, 89);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 25);
            this.btnApply.TabIndex = 28;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // CtrlPropertyOrientation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyOrientation";
            this.Size = new System.Drawing.Size(400, 165);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MCTester.Controls.Ctrl3DOrientation ctrl3DRegOrientation;
        public System.Windows.Forms.CheckBox chkRegRelativeToCurrOrientation;
        public System.Windows.Forms.CheckBox chkSelRelativeToCurrOrientation;
        public MCTester.Controls.Ctrl3DOrientation ctrl3DSelOrientation;
        protected System.Windows.Forms.Button btnApply;

    }
}
