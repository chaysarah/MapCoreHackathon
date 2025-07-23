namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmCloneObject
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
            this.cbCloneObjectScheme = new System.Windows.Forms.CheckBox();
            this.cbCloneLocationPoints = new System.Windows.Forms.CheckBox();
            this.btnClone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbCloneObjectScheme
            // 
            this.cbCloneObjectScheme.AutoSize = true;
            this.cbCloneObjectScheme.Location = new System.Drawing.Point(35, 23);
            this.cbCloneObjectScheme.Name = "cbCloneObjectScheme";
            this.cbCloneObjectScheme.Size = new System.Drawing.Size(129, 17);
            this.cbCloneObjectScheme.TabIndex = 0;
            this.cbCloneObjectScheme.Text = "Clone Object Scheme";
            this.cbCloneObjectScheme.UseVisualStyleBackColor = true;
            // 
            // cbCloneLocationPoints
            // 
            this.cbCloneLocationPoints.AutoSize = true;
            this.cbCloneLocationPoints.Checked = true;
            this.cbCloneLocationPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCloneLocationPoints.Location = new System.Drawing.Point(35, 57);
            this.cbCloneLocationPoints.Name = "cbCloneLocationPoints";
            this.cbCloneLocationPoints.Size = new System.Drawing.Size(132, 17);
            this.cbCloneLocationPoints.TabIndex = 1;
            this.cbCloneLocationPoints.Text = "Clone Location Points ";
            this.cbCloneLocationPoints.UseVisualStyleBackColor = true;
            // 
            // btnClone
            // 
            this.btnClone.Location = new System.Drawing.Point(35, 92);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(75, 23);
            this.btnClone.TabIndex = 2;
            this.btnClone.Text = "Clone";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(130, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmCloneObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(252, 127);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClone);
            this.Controls.Add(this.cbCloneLocationPoints);
            this.Controls.Add(this.cbCloneObjectScheme);
            this.Name = "frmCloneObject";
            this.Text = "Clone Object";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbCloneObjectScheme;
        private System.Windows.Forms.CheckBox cbCloneLocationPoints;
        private System.Windows.Forms.Button btnClone;
        private System.Windows.Forms.Button btnCancel;
    }
}