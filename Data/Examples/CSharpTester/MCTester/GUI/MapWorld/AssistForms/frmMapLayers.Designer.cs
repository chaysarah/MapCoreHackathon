namespace MCTester.MapWorld.Assist_Forms
{
    partial class frmMapLayers
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
            this.ucLayer1 = new MCTester.MapWorld.MapUserControls.ucLayer();
            this.SuspendLayout();
            // 
            // ucLayer1
            // 
            this.ucLayer1.AutoScroll = true;
            this.ucLayer1.Location = new System.Drawing.Point(-1, 1);
            this.ucLayer1.Name = "ucLayer1";
            this.ucLayer1.Size = new System.Drawing.Size(745, 647);
            this.ucLayer1.TabIndex = 0;
            // 
            // frmMapLayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(751, 656);
            this.Controls.Add(this.ucLayer1);
            this.Name = "frmMapLayers";
            this.Text = "Map Layer";
            this.ResumeLayout(false);

        }

        #endregion

        private MapUserControls.ucLayer ucLayer1;
    }
}