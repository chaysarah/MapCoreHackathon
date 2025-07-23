namespace MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms
{
    partial class frmNumericArrayPropertyType
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
            this.ctrlNumberArray = new MCTester.Controls.NumberArray();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ntxPropertyID
            // 
            this.ntxPropertyID.Margin = new System.Windows.Forms.Padding(5);
            // 
            // ctrlNumberArray
            // 
            this.ctrlNumberArray.IsSeperateWithTwiceSpace = false;
            this.ctrlNumberArray.Location = new System.Drawing.Point(108, 82);
            this.ctrlNumberArray.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlNumberArray.mNumbersType = MCTester.Controls.NumberArray.NumbersType.Int;
            this.ctrlNumberArray.Name = "ctrlNumberArray";
            this.ctrlNumberArray.Size = new System.Drawing.Size(354, 168);
            this.ctrlNumberArray.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value";
            // 
            // frmNumericArrayPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(556, 305);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlNumberArray);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmNumericArrayPropertyType";
            this.Text = "frmNumericArrayPropertyType";
            this.Controls.SetChildIndex(this.ctrlNumberArray, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.NumberArray ctrlNumberArray;
        private System.Windows.Forms.Label label2;
    }
}