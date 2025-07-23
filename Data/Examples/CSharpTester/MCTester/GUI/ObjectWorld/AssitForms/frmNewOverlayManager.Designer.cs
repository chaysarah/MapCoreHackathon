namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmNewOverlayManager
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
            this.btnOK = new System.Windows.Forms.Button();
            this.ctrlOMGridCoordinateSystem = new MCTester.Controls.CtrlGridCoordinateSystem();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(262, 163);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ctrlOMGridCoordinateSystem
            // 
            this.ctrlOMGridCoordinateSystem.GridCoordinateSystem = null;
            this.ctrlOMGridCoordinateSystem.Location = new System.Drawing.Point(1, 2);
            this.ctrlOMGridCoordinateSystem.Name = "ctrlOMGridCoordinateSystem";
            this.ctrlOMGridCoordinateSystem.Size = new System.Drawing.Size(336, 155);
            this.ctrlOMGridCoordinateSystem.TabIndex = 3;
            // 
            // frmNewOverlayManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(339, 190);
            this.Controls.Add(this.ctrlOMGridCoordinateSystem);
            this.Controls.Add(this.btnOK);
            this.Name = "frmNewOverlayManager";
            this.Text = "frmNewOverlayManager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private MCTester.Controls.CtrlGridCoordinateSystem ctrlOMGridCoordinateSystem;
    }
}