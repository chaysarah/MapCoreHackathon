namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmAnimationPropertyType
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtAnimationName = new System.Windows.Forms.TextBox();
            this.chxAnimationLoop = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Animation Name:";
            // 
            // txtAnimationName
            // 
            this.txtAnimationName.Location = new System.Drawing.Point(105, 6);
            this.txtAnimationName.Name = "txtAnimationName";
            this.txtAnimationName.Size = new System.Drawing.Size(251, 20);
            this.txtAnimationName.TabIndex = 4;
            // 
            // chxAnimationLoop
            // 
            this.chxAnimationLoop.AutoSize = true;
            this.chxAnimationLoop.Location = new System.Drawing.Point(362, 8);
            this.chxAnimationLoop.Name = "chxAnimationLoop";
            this.chxAnimationLoop.Size = new System.Drawing.Size(50, 17);
            this.chxAnimationLoop.TabIndex = 5;
            this.chxAnimationLoop.Text = "Loop";
            this.chxAnimationLoop.UseVisualStyleBackColor = true;
            // 
            // frmAnimationPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAnimationName);
            this.Controls.Add(this.chxAnimationLoop);
            this.Name = "frmAnimationPropertyType";
            this.Text = "frmAnimationPropertyType";
            this.Controls.SetChildIndex(this.chxAnimationLoop, 0);
            this.Controls.SetChildIndex(this.txtAnimationName, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ntxPropertyID, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAnimationName;
        private System.Windows.Forms.CheckBox chxAnimationLoop;
    }
}