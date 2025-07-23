namespace MCTester.MapWorld.MapUserControls
{
    partial class ucVectorRawLayer
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbIsRasterizedVectorLayer = new System.Windows.Forms.CheckBox();
            this.tpVectorial.SuspendLayout();
            this.tcLayer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpVectorial
            // 
            this.tpVectorial.Controls.Add(this.groupBox2);
            this.tpVectorial.Margin = new System.Windows.Forms.Padding(5);
            this.tpVectorial.Padding = new System.Windows.Forms.Padding(5);
            this.tpVectorial.Size = new System.Drawing.Size(985, 867);
            this.tpVectorial.Controls.SetChildIndex(this.groupBox2, 0);
            // 
            // tpQueries
            // 
            this.tpQueries.Margin = new System.Windows.Forms.Padding(5);
            this.tpQueries.Padding = new System.Windows.Forms.Padding(5);
            this.tpQueries.Size = new System.Drawing.Size(964, 867);
            // 
            // lbOverlayStateViewport
            // 
            this.lbOverlayStateViewport.Location = new System.Drawing.Point(135, 10);
            this.lbOverlayStateViewport.Margin = new System.Windows.Forms.Padding(5);
            this.lbOverlayStateViewport.Size = new System.Drawing.Size(335, 132);
            // 
            // tbBrightness
            // 
            this.tbBrightness.Margin = new System.Windows.Forms.Padding(5);
            // 
            // tcLayer
            // 
            this.tcLayer.Margin = new System.Windows.Forms.Padding(5);
            this.tcLayer.Size = new System.Drawing.Size(993, 896);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Size = new System.Drawing.Size(985, 867);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbIsRasterizedVectorLayer);
            this.groupBox2.Location = new System.Drawing.Point(494, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 57);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Is Rasterized";
            // 
            // cbIsRasterizedVectorLayer
            // 
            this.cbIsRasterizedVectorLayer.AutoSize = true;
            this.cbIsRasterizedVectorLayer.Enabled = false;
            this.cbIsRasterizedVectorLayer.Location = new System.Drawing.Point(8, 22);
            this.cbIsRasterizedVectorLayer.Name = "cbIsRasterizedVectorLayer";
            this.cbIsRasterizedVectorLayer.Size = new System.Drawing.Size(112, 21);
            this.cbIsRasterizedVectorLayer.TabIndex = 0;
            this.cbIsRasterizedVectorLayer.Text = "Is Rasterized";
            this.cbIsRasterizedVectorLayer.UseVisualStyleBackColor = true;
            // 
            // ucVectorRawLayer
            // 
            this.AutoScroll = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucVectorRawLayer";
            this.Size = new System.Drawing.Size(993, 933);
            this.tpVectorial.ResumeLayout(false);
            this.tcLayer.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbIsRasterizedVectorLayer;
    }
}
