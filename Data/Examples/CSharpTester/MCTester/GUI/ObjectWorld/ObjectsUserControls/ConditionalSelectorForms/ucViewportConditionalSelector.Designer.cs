namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    partial class ucViewportConditionalSelector
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
            this.txtViewportIDs = new System.Windows.Forms.TextBox();
            this.chxIDsInclusive = new System.Windows.Forms.CheckBox();
            this.clstViewportType = new System.Windows.Forms.CheckedListBox();
            this.clstViewportCoordSys = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gbConditionalSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConditionalSelector
            // 
            this.gbConditionalSelector.Controls.Add(this.label3);
            this.gbConditionalSelector.Controls.Add(this.clstViewportType);
            this.gbConditionalSelector.Controls.Add(this.chxIDsInclusive);
            this.gbConditionalSelector.Controls.Add(this.clstViewportCoordSys);
            this.gbConditionalSelector.Controls.Add(this.txtViewportIDs);
            this.gbConditionalSelector.Controls.SetChildIndex(this.txtViewportIDs, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.clstViewportCoordSys, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.chxIDsInclusive, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.clstViewportType, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.label3, 0);
            // 
            // txtViewportIDs
            // 
            this.txtViewportIDs.Location = new System.Drawing.Point(9, 354);
            this.txtViewportIDs.Multiline = true;
            this.txtViewportIDs.Name = "txtViewportIDs";
            this.txtViewportIDs.Size = new System.Drawing.Size(316, 65);
            this.txtViewportIDs.TabIndex = 26;
            // 
            // chxIDsInclusive
            // 
            this.chxIDsInclusive.AutoSize = true;
            this.chxIDsInclusive.Location = new System.Drawing.Point(9, 425);
            this.chxIDsInclusive.Name = "chxIDsInclusive";
            this.chxIDsInclusive.Size = new System.Drawing.Size(87, 17);
            this.chxIDsInclusive.TabIndex = 25;
            this.chxIDsInclusive.Text = "IDs Inclusive";
            this.chxIDsInclusive.UseVisualStyleBackColor = true;
            // 
            // clstViewportType
            // 
            this.clstViewportType.FormattingEnabled = true;
            this.clstViewportType.Location = new System.Drawing.Point(9, 72);
            this.clstViewportType.Name = "clstViewportType";
            this.clstViewportType.Size = new System.Drawing.Size(316, 154);
            this.clstViewportType.TabIndex = 23;
            // 
            // clstViewportCoordSys
            // 
            this.clstViewportCoordSys.FormattingEnabled = true;
            this.clstViewportCoordSys.Location = new System.Drawing.Point(9, 232);
            this.clstViewportCoordSys.Name = "clstViewportCoordSys";
            this.clstViewportCoordSys.Size = new System.Drawing.Size(316, 94);
            this.clstViewportCoordSys.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(6, 338);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Viewport IDs (seperated by space):";
            // 
            // ucViewportConditionalSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucViewportConditionalSelector";
            this.gbConditionalSelector.ResumeLayout(false);
            this.gbConditionalSelector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtViewportIDs;
        private System.Windows.Forms.CheckBox chxIDsInclusive;
        private System.Windows.Forms.CheckedListBox clstViewportType;
        private System.Windows.Forms.CheckedListBox clstViewportCoordSys;
        private System.Windows.Forms.Label label3;
    }
}
