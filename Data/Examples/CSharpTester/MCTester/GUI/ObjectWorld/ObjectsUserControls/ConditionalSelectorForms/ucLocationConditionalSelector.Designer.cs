namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    partial class ucLocationConditionalSelector
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
            this.btnDrawPolygon = new System.Windows.Forms.Button();
            this.ctrlPolygonPoints = new MCTester.Controls.CtrlPointsGrid();
            this.gbConditionalSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConditionalSelector
            // 
            this.gbConditionalSelector.Controls.Add(this.ctrlPolygonPoints);
            this.gbConditionalSelector.Controls.Add(this.btnDrawPolygon);
            this.gbConditionalSelector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbConditionalSelector.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbConditionalSelector.Size = new System.Drawing.Size(416, 527);
            this.gbConditionalSelector.Controls.SetChildIndex(this.btnDrawPolygon, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.ctrlPolygonPoints, 0);
            // 
            // btnDrawPolygon
            // 
            this.btnDrawPolygon.Location = new System.Drawing.Point(78, 77);
            this.btnDrawPolygon.Name = "btnDrawPolygon";
            this.btnDrawPolygon.Size = new System.Drawing.Size(106, 23);
            this.btnDrawPolygon.TabIndex = 135;
            this.btnDrawPolygon.Text = "Draw Polygon";
            this.btnDrawPolygon.UseVisualStyleBackColor = true;
            this.btnDrawPolygon.Click += new System.EventHandler(this.btnDrawPolygon_Click);
            // 
            // ctrlPointsGrid1
            // 
            this.ctrlPolygonPoints.Location = new System.Drawing.Point(9, 118);
            this.ctrlPolygonPoints.Name = "ctrlPointsGrid1";
            this.ctrlPolygonPoints.Size = new System.Drawing.Size(345, 196);
            this.ctrlPolygonPoints.TabIndex = 136;
            // 
            // ucLocationConditionalSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(254, 433);
            this.Name = "ucLocationConditionalSelector";
            this.Size = new System.Drawing.Size(436, 533);
            this.Leave += new System.EventHandler(this.ucLocationConditionalSelector_Leave);
            this.gbConditionalSelector.ResumeLayout(false);
            this.gbConditionalSelector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDrawPolygon;
        private Controls.CtrlPointsGrid ctrlPolygonPoints;
    }
}
