namespace MCTester.Controls
{
    partial class CtrlBoundingRect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlBoundingRect));
            this.gbBoundingRect = new System.Windows.Forms.GroupBox();
            this.ctrl3DVectorMinVertex = new MCTester.Controls.Ctrl3DVector();
            this.ctrl3DVectorMaxVertex = new MCTester.Controls.Ctrl3DVector();
            this.btnGetBoundingBox = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbBoundingRect.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBoundingRect
            // 
            this.gbBoundingRect.Controls.Add(this.ctrl3DVectorMinVertex);
            this.gbBoundingRect.Controls.Add(this.ctrl3DVectorMaxVertex);
            this.gbBoundingRect.Controls.Add(this.btnGetBoundingBox);
            this.gbBoundingRect.Controls.Add(this.label2);
            this.gbBoundingRect.Controls.Add(this.label1);
            this.gbBoundingRect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBoundingRect.Location = new System.Drawing.Point(0, 0);
            this.gbBoundingRect.Name = "gbBoundingRect";
            this.gbBoundingRect.Size = new System.Drawing.Size(400, 80);
            this.gbBoundingRect.TabIndex = 2;
            this.gbBoundingRect.TabStop = false;
            this.gbBoundingRect.Text = "Group Box Title";
            // 
            // ctrl3DVectorMinVertex
            // 
            this.ctrl3DVectorMinVertex.Location = new System.Drawing.Point(72, 17);
            this.ctrl3DVectorMinVertex.Name = "ctrl3DVectorMinVertex";
            this.ctrl3DVectorMinVertex.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorMinVertex.TabIndex = 8;
            this.ctrl3DVectorMinVertex.X = 0;
            this.ctrl3DVectorMinVertex.Y = 0;
            this.ctrl3DVectorMinVertex.Z = 0;
            // 
            // ctrl3DVectorMaxVertex
            // 
            this.ctrl3DVectorMaxVertex.Location = new System.Drawing.Point(72, 49);
            this.ctrl3DVectorMaxVertex.Name = "ctrl3DVectorMaxVertex";
            this.ctrl3DVectorMaxVertex.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorMaxVertex.TabIndex = 7;
            this.ctrl3DVectorMaxVertex.X = 0;
            this.ctrl3DVectorMaxVertex.Y = 0;
            this.ctrl3DVectorMaxVertex.Z = 0;
            // 
            // btnGetBoundingBox
            // 
            this.btnGetBoundingBox.Location = new System.Drawing.Point(319, 52);
            this.btnGetBoundingBox.Name = "btnGetBoundingBox";
            this.btnGetBoundingBox.Size = new System.Drawing.Size(75, 23);
            this.btnGetBoundingBox.TabIndex = 6;
            this.btnGetBoundingBox.Text = "Get";
            this.btnGetBoundingBox.UseVisualStyleBackColor = true;
            this.btnGetBoundingBox.Click += new System.EventHandler(this.btnGetBoundingBox_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Min Vertex:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max Vertex:";
            // 
            // CtrlBoundingRect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbBoundingRect);
            this.Name = "CtrlBoundingRect";
            this.Size = new System.Drawing.Size(400, 80);
            this.gbBoundingRect.ResumeLayout(false);
            this.gbBoundingRect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbBoundingRect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetBoundingBox;
        private Ctrl3DVector ctrl3DVectorMinVertex;
        private Ctrl3DVector ctrl3DVectorMaxVertex;

    }
}
