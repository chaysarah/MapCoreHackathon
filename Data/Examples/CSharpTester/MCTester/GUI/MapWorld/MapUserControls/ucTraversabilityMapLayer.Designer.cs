namespace MCTester.MapWorld.MapUserControls
{
    partial class ucTraversabilityMapLayer
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
            this.txtNumTraversabilityDirections = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tcLayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtNumTraversabilityDirections);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.SetChildIndex(this.label10, 0);
            this.tabPage2.Controls.SetChildIndex(this.txtNumTraversabilityDirections, 0);
            // 
            // txtNumTraversabilityDirections
            // 
            this.txtNumTraversabilityDirections.Enabled = false;
            this.txtNumTraversabilityDirections.Location = new System.Drawing.Point(614, 48);
            this.txtNumTraversabilityDirections.Name = "txtNumTraversabilityDirections";
            this.txtNumTraversabilityDirections.Size = new System.Drawing.Size(60, 22);
            this.txtNumTraversabilityDirections.TabIndex = 111;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(411, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(197, 17);
            this.label10.TabIndex = 110;
            this.label10.Text = "Num Traversability Directions:";
            // 
            // ucTraversabilityMapLayer
            // 
            this.AutoScroll = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucTraversabilityMapLayer";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tcLayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumTraversabilityDirections;
        private System.Windows.Forms.Label label10;
    }
}
