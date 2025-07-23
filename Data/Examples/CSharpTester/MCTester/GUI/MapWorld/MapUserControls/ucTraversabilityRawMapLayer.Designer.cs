namespace MCTester.MapWorld.MapUserControls
{
    partial class ucTraversabilityRawMapLayer
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
            this.txtNumTraversabilityDirections = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ctrlRawComponents1 = new MCTester.MapWorld.MapUserControls.CtrlRawComponents();
            this.ctrlRawResolutions1 = new MCTester.MapWorld.MapUserControls.CtrlRawResolutions();
            this.tabPage2.SuspendLayout();
            this.tcLayer.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtNumTraversabilityDirections);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.SetChildIndex(this.label10, 0);
            this.tabPage2.Controls.SetChildIndex(this.txtNumTraversabilityDirections, 0);
            // 
            // tcLayer
            // 
            this.tcLayer.Controls.Add(this.tabPage3);
            this.tcLayer.Controls.SetChildIndex(this.tabPage3, 0);
            this.tcLayer.Controls.SetChildIndex(this.tabPage2, 0);
            this.tcLayer.Controls.SetChildIndex(this.tpGeneral, 0);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Margin = new System.Windows.Forms.Padding(2);
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(2);
            // 
            // txtNumTraversabilityDirections
            // 
            this.txtNumTraversabilityDirections.Enabled = false;
            this.txtNumTraversabilityDirections.Location = new System.Drawing.Point(460, 39);
            this.txtNumTraversabilityDirections.Margin = new System.Windows.Forms.Padding(2);
            this.txtNumTraversabilityDirections.Name = "txtNumTraversabilityDirections";
            this.txtNumTraversabilityDirections.Size = new System.Drawing.Size(46, 20);
            this.txtNumTraversabilityDirections.TabIndex = 111;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 41);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(147, 13);
            this.label10.TabIndex = 110;
            this.label10.Text = "Num Traversability Directions:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ctrlRawResolutions1);
            this.tabPage3.Controls.Add(this.ctrlRawComponents1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(737, 592);
            this.tabPage3.TabIndex = 6;
            this.tabPage3.Text = "Raw Traversability";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ctrlRawComponents1
            // 
            this.ctrlRawComponents1.Location = new System.Drawing.Point(6, 6);
            this.ctrlRawComponents1.Name = "ctrlRawComponents1";
            this.ctrlRawComponents1.Size = new System.Drawing.Size(683, 153);
            this.ctrlRawComponents1.TabIndex = 0;
            // 
            // ctrlRawResolutions1
            // 
            this.ctrlRawResolutions1.Location = new System.Drawing.Point(6, 165);
            this.ctrlRawResolutions1.Name = "ctrlRawResolutions1";
            this.ctrlRawResolutions1.Size = new System.Drawing.Size(254, 57);
            this.ctrlRawResolutions1.TabIndex = 1;
            // 
            // ucTraversabilityRawMapLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3);
            this.Name = "ucTraversabilityRawMapLayer";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tcLayer.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumTraversabilityDirections;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPage3;
        private CtrlRawComponents ctrlRawComponents1;
        private CtrlRawResolutions ctrlRawResolutions1;
    }
}
