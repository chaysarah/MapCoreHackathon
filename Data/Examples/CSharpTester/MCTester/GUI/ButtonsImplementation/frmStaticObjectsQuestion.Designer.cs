namespace MCTester.ButtonsImplementation
{
    partial class frmStaticObjectsQuestion
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
            this.lblUserMsg = new System.Windows.Forms.Label();
            this.btnNativeServer3DModel = new System.Windows.Forms.Button();
            this.btnNativeServerVector3DExtrusion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUserMsg
            // 
            this.lblUserMsg.AutoSize = true;
            this.lblUserMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblUserMsg.Location = new System.Drawing.Point(20, 20);
            this.lblUserMsg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserMsg.Name = "lblUserMsg";
            this.lblUserMsg.Size = new System.Drawing.Size(8, 18);
            this.lblUserMsg.TabIndex = 0;
            this.lblUserMsg.Text = "\r\n";
            // 
            // btnNativeServer3DModel
            // 
            this.btnNativeServer3DModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNativeServer3DModel.Location = new System.Drawing.Point(55, 69);
            this.btnNativeServer3DModel.Name = "btnNativeServer3DModel";
            this.btnNativeServer3DModel.Size = new System.Drawing.Size(207, 27);
            this.btnNativeServer3DModel.TabIndex = 1;
            this.btnNativeServer3DModel.Text = "Native Server 3D Model";
            this.btnNativeServer3DModel.UseVisualStyleBackColor = true;
            this.btnNativeServer3DModel.Click += new System.EventHandler(this.btnNativeServer3DModel_Click);
            // 
            // btnNativeServerVector3DExtrusion
            // 
            this.btnNativeServerVector3DExtrusion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNativeServerVector3DExtrusion.Location = new System.Drawing.Point(317, 69);
            this.btnNativeServerVector3DExtrusion.Name = "btnNativeServerVector3DExtrusion";
            this.btnNativeServerVector3DExtrusion.Size = new System.Drawing.Size(207, 27);
            this.btnNativeServerVector3DExtrusion.TabIndex = 2;
            this.btnNativeServerVector3DExtrusion.Text = "Native Server Vector 3D Extrusion";
            this.btnNativeServerVector3DExtrusion.UseVisualStyleBackColor = true;
            this.btnNativeServerVector3DExtrusion.Click += new System.EventHandler(this.btnNativeServerVector3DExtrusion_Click);
            // 
            // frmStaticObjectsQuestion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(626, 115);
            this.Controls.Add(this.btnNativeServerVector3DExtrusion);
            this.Controls.Add(this.btnNativeServer3DModel);
            this.Controls.Add(this.lblUserMsg);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmStaticObjectsQuestion";
            this.Text = " Load Static Objects Map Layer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserMsg;
        private System.Windows.Forms.Button btnNativeServer3DModel;
        private System.Windows.Forms.Button btnNativeServerVector3DExtrusion;
    }
}