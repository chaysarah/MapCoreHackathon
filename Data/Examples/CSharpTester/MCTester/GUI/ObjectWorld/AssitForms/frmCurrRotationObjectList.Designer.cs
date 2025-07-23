namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmCurrRotationObjectList
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
            this.lstObject = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstObject
            // 
            this.lstObject.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstObject.FormattingEnabled = true;
            this.lstObject.Location = new System.Drawing.Point(0, 0);
            this.lstObject.Name = "lstObject";
            this.lstObject.Size = new System.Drawing.Size(292, 212);
            this.lstObject.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(217, 218);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmSubPartCurrRotationObjectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(292, 246);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lstObject);
            this.Name = "frmSubPartCurrRotationObjectList";
            this.Text = "frmSubPartCurrRotationObjectList";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstObject;
        private System.Windows.Forms.Button btnOK;
    }
}