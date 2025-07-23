namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    partial class frmFVector2DPropertyType
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
            this.ctrl2DFVectorPropertyValue = new MCTester.Controls.Ctrl2DFVector();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "2D FVector:";
            // 
            // ctrl2DFVectorPropertyValue
            // 
            this.ctrl2DFVectorPropertyValue.Location = new System.Drawing.Point(81, 9);
            this.ctrl2DFVectorPropertyValue.Name = "ctrl2DFVectorPropertyValue";
            this.ctrl2DFVectorPropertyValue.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DFVectorPropertyValue.TabIndex = 4;
            this.ctrl2DFVectorPropertyValue.X = 0F;
            this.ctrl2DFVectorPropertyValue.Y = 0F;
            // 
            // frmFVector2DPropertyType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(417, 71);
            this.Controls.Add(this.ctrl2DFVectorPropertyValue);
            this.Controls.Add(this.label2);
            this.Name = "frmFVector2DPropertyType";
            this.Text = "frmFVector2DPropertyType";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ctrl2DFVectorPropertyValue, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private MCTester.Controls.Ctrl2DFVector ctrl2DFVectorPropertyValue;
    }
}