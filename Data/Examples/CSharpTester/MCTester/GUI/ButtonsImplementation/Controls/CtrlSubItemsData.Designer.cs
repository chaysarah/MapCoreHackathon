namespace MCTester.Controls
{
    partial class CtrlSubItemsData
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
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPointsStartIndex = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSubItemID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(5, -1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(298, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Separate input by space(MAX,MAX-1 can be used in ID field):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Start Index:";
            // 
            // txtPointsStartIndex
            // 
            this.txtPointsStartIndex.Location = new System.Drawing.Point(71, 37);
            this.txtPointsStartIndex.Name = "txtPointsStartIndex";
            this.txtPointsStartIndex.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtPointsStartIndex.Size = new System.Drawing.Size(226, 20);
            this.txtPointsStartIndex.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "ID:";
            // 
            // txtSubItemID
            // 
            this.txtSubItemID.Location = new System.Drawing.Point(71, 15);
            this.txtSubItemID.Name = "txtSubItemID";
            this.txtSubItemID.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtSubItemID.Size = new System.Drawing.Size(226, 20);
            this.txtSubItemID.TabIndex = 33;
            // 
            // CtrlSubItemsData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPointsStartIndex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSubItemID);
            this.Name = "CtrlSubItemsData";
            this.Size = new System.Drawing.Size(303, 58);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPointsStartIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSubItemID;
    }
}
