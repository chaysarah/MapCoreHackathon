namespace MCTester.GUI.Forms
{
    partial class MovementFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MovementFrm));
            this.m_RotateAlignBtn = new System.Windows.Forms.Button();
            this.m_LoweringCameraBtn = new System.Windows.Forms.Button();
            this.m_RaisingCameraBtn = new System.Windows.Forms.Button();
            this.m_MoveLeftBtn = new System.Windows.Forms.Button();
            this.m_MoveRightBtn = new System.Windows.Forms.Button();
            this.m_Backward = new System.Windows.Forms.Button();
            this.m_RotateLeftBtn = new System.Windows.Forms.Button();
            this.m_Forward = new System.Windows.Forms.Button();
            this.m_RotateRightBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxMoveFactor = new MCTester.Controls.NumericTextBox();
            this.ntxRotateFactor = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_RotateAlignBtn
            // 
            this.m_RotateAlignBtn.BackColor = System.Drawing.SystemColors.Window;
            this.m_RotateAlignBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RotateAlignBtn.BackgroundImage")));
            this.m_RotateAlignBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_RotateAlignBtn.Location = new System.Drawing.Point(53, 53);
            this.m_RotateAlignBtn.Name = "m_RotateAlignBtn";
            this.m_RotateAlignBtn.Size = new System.Drawing.Size(45, 45);
            this.m_RotateAlignBtn.TabIndex = 0;
            this.m_RotateAlignBtn.Tag = "Align";
            this.m_RotateAlignBtn.UseVisualStyleBackColor = false;
            this.m_RotateAlignBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_RotateAlignBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_LoweringCameraBtn
            // 
            this.m_LoweringCameraBtn.Location = new System.Drawing.Point(2, 2);
            this.m_LoweringCameraBtn.Name = "m_LoweringCameraBtn";
            this.m_LoweringCameraBtn.Size = new System.Drawing.Size(45, 45);
            this.m_LoweringCameraBtn.TabIndex = 9;
            this.m_LoweringCameraBtn.Tag = "Lower";
            this.m_LoweringCameraBtn.Text = "Lower";
            this.m_LoweringCameraBtn.UseVisualStyleBackColor = true;
            this.m_LoweringCameraBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_LoweringCameraBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_RaisingCameraBtn
            // 
            this.m_RaisingCameraBtn.Location = new System.Drawing.Point(104, 2);
            this.m_RaisingCameraBtn.Name = "m_RaisingCameraBtn";
            this.m_RaisingCameraBtn.Size = new System.Drawing.Size(45, 45);
            this.m_RaisingCameraBtn.TabIndex = 10;
            this.m_RaisingCameraBtn.Tag = "Raise";
            this.m_RaisingCameraBtn.Text = "Raise";
            this.m_RaisingCameraBtn.UseVisualStyleBackColor = true;
            this.m_RaisingCameraBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_RaisingCameraBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_MoveLeftBtn
            // 
            this.m_MoveLeftBtn.BackColor = System.Drawing.SystemColors.Menu;
            this.m_MoveLeftBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_MoveLeftBtn.BackgroundImage")));
            this.m_MoveLeftBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_MoveLeftBtn.Location = new System.Drawing.Point(2, 53);
            this.m_MoveLeftBtn.Name = "m_MoveLeftBtn";
            this.m_MoveLeftBtn.Size = new System.Drawing.Size(45, 45);
            this.m_MoveLeftBtn.TabIndex = 8;
            this.m_MoveLeftBtn.Tag = "Left";
            this.m_MoveLeftBtn.UseVisualStyleBackColor = false;
            this.m_MoveLeftBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_MoveLeftBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_MoveRightBtn
            // 
            this.m_MoveRightBtn.BackColor = System.Drawing.SystemColors.Window;
            this.m_MoveRightBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_MoveRightBtn.BackgroundImage")));
            this.m_MoveRightBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_MoveRightBtn.Location = new System.Drawing.Point(104, 53);
            this.m_MoveRightBtn.Name = "m_MoveRightBtn";
            this.m_MoveRightBtn.Size = new System.Drawing.Size(45, 45);
            this.m_MoveRightBtn.TabIndex = 6;
            this.m_MoveRightBtn.Tag = "Right";
            this.m_MoveRightBtn.UseVisualStyleBackColor = false;
            this.m_MoveRightBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_MoveRightBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_Backward
            // 
            this.m_Backward.BackColor = System.Drawing.SystemColors.Window;
            this.m_Backward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_Backward.BackgroundImage")));
            this.m_Backward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_Backward.Location = new System.Drawing.Point(53, 104);
            this.m_Backward.Name = "m_Backward";
            this.m_Backward.Size = new System.Drawing.Size(45, 45);
            this.m_Backward.TabIndex = 4;
            this.m_Backward.Tag = "Backward";
            this.m_Backward.UseVisualStyleBackColor = false;
            this.m_Backward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_Backward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_RotateLeftBtn
            // 
            this.m_RotateLeftBtn.BackColor = System.Drawing.SystemColors.Window;
            this.m_RotateLeftBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RotateLeftBtn.BackgroundImage")));
            this.m_RotateLeftBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_RotateLeftBtn.Location = new System.Drawing.Point(2, 104);
            this.m_RotateLeftBtn.Name = "m_RotateLeftBtn";
            this.m_RotateLeftBtn.Size = new System.Drawing.Size(45, 45);
            this.m_RotateLeftBtn.TabIndex = 3;
            this.m_RotateLeftBtn.Tag = "RotateLeft";
            this.m_RotateLeftBtn.UseVisualStyleBackColor = false;
            this.m_RotateLeftBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_RotateLeftBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_Forward
            // 
            this.m_Forward.BackColor = System.Drawing.SystemColors.Window;
            this.m_Forward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_Forward.BackgroundImage")));
            this.m_Forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_Forward.Location = new System.Drawing.Point(53, 2);
            this.m_Forward.Name = "m_Forward";
            this.m_Forward.Size = new System.Drawing.Size(45, 45);
            this.m_Forward.TabIndex = 2;
            this.m_Forward.Tag = "Forward";
            this.m_Forward.UseVisualStyleBackColor = false;
            this.m_Forward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_Forward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // m_RotateRightBtn
            // 
            this.m_RotateRightBtn.BackColor = System.Drawing.SystemColors.Window;
            this.m_RotateRightBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_RotateRightBtn.BackgroundImage")));
            this.m_RotateRightBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.m_RotateRightBtn.Location = new System.Drawing.Point(104, 104);
            this.m_RotateRightBtn.Name = "m_RotateRightBtn";
            this.m_RotateRightBtn.Size = new System.Drawing.Size(45, 45);
            this.m_RotateRightBtn.TabIndex = 1;
            this.m_RotateRightBtn.Tag = "RotateRight";
            this.m_RotateRightBtn.UseVisualStyleBackColor = false;
            this.m_RotateRightBtn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseDown);
            this.m_RotateRightBtn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ContinuousBtn_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Move Factor:";
            // 
            // ntxMoveFactor
            // 
            this.ntxMoveFactor.Location = new System.Drawing.Point(81, 161);
            this.ntxMoveFactor.Name = "ntxMoveFactor";
            this.ntxMoveFactor.Size = new System.Drawing.Size(69, 20);
            this.ntxMoveFactor.TabIndex = 12;
            this.ntxMoveFactor.Text = "5";
            // 
            // ntxRotateFactor
            // 
            this.ntxRotateFactor.Location = new System.Drawing.Point(80, 187);
            this.ntxRotateFactor.Name = "ntxRotateFactor";
            this.ntxRotateFactor.Size = new System.Drawing.Size(69, 20);
            this.ntxRotateFactor.TabIndex = 14;
            this.ntxRotateFactor.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-1, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Rotate Factor:";
            // 
            // MovementFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(152, 216);
            this.Controls.Add(this.ntxRotateFactor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ntxMoveFactor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_RaisingCameraBtn);
            this.Controls.Add(this.m_LoweringCameraBtn);
            this.Controls.Add(this.m_MoveLeftBtn);
            this.Controls.Add(this.m_MoveRightBtn);
            this.Controls.Add(this.m_Backward);
            this.Controls.Add(this.m_RotateLeftBtn);
            this.Controls.Add(this.m_Forward);
            this.Controls.Add(this.m_RotateRightBtn);
            this.Controls.Add(this.m_RotateAlignBtn);
            this.Name = "MovementFrm";
            this.Text = "MovementFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_RotateAlignBtn;
        private System.Windows.Forms.Button m_RotateRightBtn;
        private System.Windows.Forms.Button m_Forward;
        private System.Windows.Forms.Button m_RotateLeftBtn;
        private System.Windows.Forms.Button m_Backward;
        private System.Windows.Forms.Button m_MoveRightBtn;
        private System.Windows.Forms.Button m_MoveLeftBtn;
        private System.Windows.Forms.Button m_LoweringCameraBtn;
        private System.Windows.Forms.Button m_RaisingCameraBtn;
        private System.Windows.Forms.Label label1;
        private MCTester.Controls.NumericTextBox ntxMoveFactor;
        private MCTester.Controls.NumericTextBox ntxRotateFactor;
        private System.Windows.Forms.Label label2;
    }
}