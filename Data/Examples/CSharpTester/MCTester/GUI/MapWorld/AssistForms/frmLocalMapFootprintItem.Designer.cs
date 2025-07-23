namespace MCTester.MapWorld.MapUserControls
{
    partial class frmLocalMapFootprintItem
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
            this.lstActiveLine = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lstInactiveLine = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnClearViewportSelection = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lstViewports = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstActiveLine
            // 
            this.lstActiveLine.FormattingEnabled = true;
            this.lstActiveLine.Location = new System.Drawing.Point(404, 21);
            this.lstActiveLine.Name = "lstActiveLine";
            this.lstActiveLine.Size = new System.Drawing.Size(151, 186);
            this.lstActiveLine.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(401, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Active Line:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(244, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Inactive Line:";
            // 
            // lstInactiveLine
            // 
            this.lstInactiveLine.FormattingEnabled = true;
            this.lstInactiveLine.Location = new System.Drawing.Point(247, 21);
            this.lstInactiveLine.Name = "lstInactiveLine";
            this.lstInactiveLine.Size = new System.Drawing.Size(151, 186);
            this.lstInactiveLine.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(247, 266);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClearList
            // 
            this.btnClearList.Location = new System.Drawing.Point(247, 213);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(308, 23);
            this.btnClearList.TabIndex = 5;
            this.btnClearList.Text = "Clear Selection";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.btnClearList_Click);
            // 
            // btnClearViewportSelection
            // 
            this.btnClearViewportSelection.Location = new System.Drawing.Point(12, 213);
            this.btnClearViewportSelection.Name = "btnClearViewportSelection";
            this.btnClearViewportSelection.Size = new System.Drawing.Size(94, 23);
            this.btnClearViewportSelection.TabIndex = 43;
            this.btnClearViewportSelection.Text = "Clear Selection";
            this.btnClearViewportSelection.UseVisualStyleBackColor = true;
            this.btnClearViewportSelection.Click += new System.EventHandler(this.btnClearViewportSelection_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(231, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Viewports (Clear selection to set for all viewport)";
            // 
            // lstViewports
            // 
            this.lstViewports.FormattingEnabled = true;
            this.lstViewports.HorizontalScrollbar = true;
            this.lstViewports.Location = new System.Drawing.Point(12, 21);
            this.lstViewports.Name = "lstViewports";
            this.lstViewports.Size = new System.Drawing.Size(229, 186);
            this.lstViewports.TabIndex = 42;
            this.lstViewports.SelectedIndexChanged += new System.EventHandler(this.lstViewports_SelectedIndexChanged);
            // 
            // frmLocalMapFootprintItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(564, 293);
            this.Controls.Add(this.btnClearViewportSelection);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstViewports);
            this.Controls.Add(this.btnClearList);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstInactiveLine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstActiveLine);
            this.Name = "frmLocalMapFootprintItem";
            this.Text = "Local Map Footprint Item";
            this.Load += new System.EventHandler(this.frmLocalMapFootprintItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstActiveLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstInactiveLine;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.Button btnClearViewportSelection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lstViewports;
    }
}