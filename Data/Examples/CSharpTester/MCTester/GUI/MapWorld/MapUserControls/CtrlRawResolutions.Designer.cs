namespace MCTester.MapWorld.MapUserControls
{
    partial class CtrlRawResolutions
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ntxPyramidResolutions = new System.Windows.Forms.TextBox();
            this.ntxFirstPyramidResolution = new MCTester.Controls.NumericTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Pyramid Resolution:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pyramid Resolutions:";
            // 
            // ntxPyramidResolutions
            // 
            this.ntxPyramidResolutions.Enabled = false;
            this.ntxPyramidResolutions.Location = new System.Drawing.Point(128, 1);
            this.ntxPyramidResolutions.Name = "ntxPyramidResolutions";
            this.ntxPyramidResolutions.ReadOnly = true;
            this.ntxPyramidResolutions.Size = new System.Drawing.Size(123, 20);
            this.ntxPyramidResolutions.TabIndex = 2;
            // 
            // ntxFirstPyramidResolution
            // 
            this.ntxFirstPyramidResolution.Enabled = false;
            this.ntxFirstPyramidResolution.Location = new System.Drawing.Point(128, 32);
            this.ntxFirstPyramidResolution.Name = "ntxFirstPyramidResolution";
            this.ntxFirstPyramidResolution.ReadOnly = true;
            this.ntxFirstPyramidResolution.Size = new System.Drawing.Size(123, 20);
            this.ntxFirstPyramidResolution.TabIndex = 4;
            // 
            // CtrlRawResolutions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ntxFirstPyramidResolution);
            this.Controls.Add(this.ntxPyramidResolutions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CtrlRawResolutions";
            this.Size = new System.Drawing.Size(254, 57);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ntxPyramidResolutions;
        private Controls.NumericTextBox ntxFirstPyramidResolution;
    }
}
