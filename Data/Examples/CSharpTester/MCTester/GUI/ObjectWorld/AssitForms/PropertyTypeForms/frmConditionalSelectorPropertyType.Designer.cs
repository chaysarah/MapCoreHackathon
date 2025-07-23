namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmConditionalSelectorPropertyType
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
            this.btnSelectorList = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectorList
            // 
            this.btnSelectorList.Location = new System.Drawing.Point(85, 8);
            this.btnSelectorList.Name = "btnSelectorList";
            this.btnSelectorList.Size = new System.Drawing.Size(85, 23);
            this.btnSelectorList.TabIndex = 4;
            this.btnSelectorList.Text = "...";
            this.btnSelectorList.UseVisualStyleBackColor = true;
            this.btnSelectorList.Click += new System.EventHandler(this.btnSelectorList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Selector List:";
            // 
            // frmConditionalSelectorPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectorList);
            this.Name = "frmConditionalSelectorPropertyType";
            this.Text = "frmConditionalSelectorPropertyType";
            this.Controls.SetChildIndex(this.btnSelectorList, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectorList;
        private System.Windows.Forms.Label label2;
    }
}