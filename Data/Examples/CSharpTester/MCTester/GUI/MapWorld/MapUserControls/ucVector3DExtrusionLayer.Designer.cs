namespace MCTester.MapWorld.MapUserControls
{
    partial class ucVector3DExtrusionLayer
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chxIsExtrusionHeightChangeSupported = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tcLayer.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcLayer
            // 
            this.tcLayer.Controls.Add(this.tabPage2);
            this.tcLayer.Controls.Add(this.tabPage3);
            this.tcLayer.Controls.SetChildIndex(this.tabPage3, 0);
            this.tcLayer.Controls.SetChildIndex(this.tabPage2, 0);
            this.tcLayer.Controls.SetChildIndex(this.tpGeneral, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chxIsExtrusionHeightChangeSupported);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(985, 735);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "Vector 3D Extrusion";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chxIsExtrusionHeightChangeSupported
            // 
            this.chxIsExtrusionHeightChangeSupported.AutoSize = true;
            this.chxIsExtrusionHeightChangeSupported.Enabled = false;
            this.chxIsExtrusionHeightChangeSupported.Location = new System.Drawing.Point(19, 24);
            this.chxIsExtrusionHeightChangeSupported.Margin = new System.Windows.Forms.Padding(4);
            this.chxIsExtrusionHeightChangeSupported.Name = "chxIsExtrusionHeightChangeSupported";
            this.chxIsExtrusionHeightChangeSupported.Size = new System.Drawing.Size(270, 21);
            this.chxIsExtrusionHeightChangeSupported.TabIndex = 89;
            this.chxIsExtrusionHeightChangeSupported.Text = "Is Extrusion Height Change Supported";
            this.chxIsExtrusionHeightChangeSupported.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(985, 735);
            this.tabPage3.TabIndex = 6;
            this.tabPage3.Text = "Vector Based";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ucVector3DExtrusionLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Name = "ucVector3DExtrusionLayer";
            this.tcLayer.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chxIsExtrusionHeightChangeSupported;
        private System.Windows.Forms.TabPage tabPage3;
    }
}
