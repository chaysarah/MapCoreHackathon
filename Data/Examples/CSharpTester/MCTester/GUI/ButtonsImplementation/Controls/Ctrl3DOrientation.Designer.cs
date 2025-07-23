namespace MCTester.Controls
{
    partial class Ctrl3DOrientation
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxYaw = new MCTester.Controls.NumericTextBox();
            this.ntxRoll = new MCTester.Controls.NumericTextBox();
            this.ntxPitch = new MCTester.Controls.NumericTextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Roll:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Yaw:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Pitch:";
            // 
            // ntxYaw
            // 
            this.ntxYaw.Location = new System.Drawing.Point(38, 1);
            this.ntxYaw.Name = "ntxYaw";
            this.ntxYaw.Size = new System.Drawing.Size(54, 20);
            this.ntxYaw.TabIndex = 15;
            this.ntxYaw.Text = "0";
            // 
            // ntxRoll
            // 
            this.ntxRoll.Location = new System.Drawing.Point(232, 2);
            this.ntxRoll.Name = "ntxRoll";
            this.ntxRoll.Size = new System.Drawing.Size(54, 20);
            this.ntxRoll.TabIndex = 17;
            this.ntxRoll.Text = "0";
            // 
            // ntxPitch
            // 
            this.ntxPitch.Location = new System.Drawing.Point(138, 1);
            this.ntxPitch.Name = "ntxPitch";
            this.ntxPitch.Size = new System.Drawing.Size(54, 20);
            this.ntxPitch.TabIndex = 16;
            this.ntxPitch.Text = "0";
            // 
            // Ctrl3DOrientation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ntxYaw);
            this.Controls.Add(this.ntxRoll);
            this.Controls.Add(this.ntxPitch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Name = "Ctrl3DOrientation";
            this.Size = new System.Drawing.Size(289, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MCTester.Controls.NumericTextBox ntxYaw;
        private MCTester.Controls.NumericTextBox ntxRoll;
        private MCTester.Controls.NumericTextBox ntxPitch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
