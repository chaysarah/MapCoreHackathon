﻿namespace MCTester.General_Forms
{
    partial class frmLayerParams
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
            this.ucLayerParams1 = new MCTester.Controls.ucLayerParams();
            this.SuspendLayout();
            // 
            // ucLayerParams1
            // 
            this.ucLayerParams1.Location = new System.Drawing.Point(3, 3);
            this.ucLayerParams1.Name = "ucLayerParams1";
            this.ucLayerParams1.Size = new System.Drawing.Size(845, 735);
            this.ucLayerParams1.TabIndex = 0;
            // 
            // frmLayerParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(860, 750);
            this.Controls.Add(this.ucLayerParams1);
            this.Name = "frmLayerParams";
            this.Text = "Layer Params";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ucLayerParams ucLayerParams1;
    }
}