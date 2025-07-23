namespace MCTester.General_Forms
{
    partial class frmQuaryDemonstration
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
            this.btnGasInTLV = new System.Windows.Forms.Button();
            this.btnGasInSight = new System.Windows.Forms.Button();
            this.btnGasOnRoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGasInTLV
            // 
            this.btnGasInTLV.Location = new System.Drawing.Point(30, 12);
            this.btnGasInTLV.Name = "btnGasInTLV";
            this.btnGasInTLV.Size = new System.Drawing.Size(125, 23);
            this.btnGasInTLV.TabIndex = 0;
            this.btnGasInTLV.Text = "Gas St. In Haifa";
            this.btnGasInTLV.UseVisualStyleBackColor = true;
            this.btnGasInTLV.Click += new System.EventHandler(this.btnGasInTLV_Click);
            // 
            // btnGasInSight
            // 
            this.btnGasInSight.Location = new System.Drawing.Point(30, 58);
            this.btnGasInSight.Name = "btnGasInSight";
            this.btnGasInSight.Size = new System.Drawing.Size(125, 23);
            this.btnGasInSight.TabIndex = 1;
            this.btnGasInSight.Text = "Gas Station In Sight";
            this.btnGasInSight.UseVisualStyleBackColor = true;
            this.btnGasInSight.Click += new System.EventHandler(this.btnGasInSight_Click);
            // 
            // btnGasOnRoad
            // 
            this.btnGasOnRoad.Location = new System.Drawing.Point(30, 107);
            this.btnGasOnRoad.Name = "btnGasOnRoad";
            this.btnGasOnRoad.Size = new System.Drawing.Size(125, 23);
            this.btnGasOnRoad.TabIndex = 2;
            this.btnGasOnRoad.Text = "Gas On Road NO. 2";
            this.btnGasOnRoad.UseVisualStyleBackColor = true;
            this.btnGasOnRoad.Click += new System.EventHandler(this.btnGasOnRoad_Click);
            // 
            // frmQuaryDemonstration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(204, 195);
            this.Controls.Add(this.btnGasOnRoad);
            this.Controls.Add(this.btnGasInSight);
            this.Controls.Add(this.btnGasInTLV);
            this.Name = "frmQuaryDemonstration";
            this.Text = "frmQuaryDemonstration";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGasInTLV;
        private System.Windows.Forms.Button btnGasInSight;
        private System.Windows.Forms.Button btnGasOnRoad;
    }
}