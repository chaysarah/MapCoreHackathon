namespace MCTester.General_Forms
{
    partial class frmGetSpecificViewportList
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
            this.label1 = new System.Windows.Forms.Label();
            this.lstViewports = new System.Windows.Forms.ListBox();
            this.btnClearSelection = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Viewports List:";
            // 
            // lstViewports
            // 
            this.lstViewports.FormattingEnabled = true;
            this.lstViewports.Location = new System.Drawing.Point(12, 25);
            this.lstViewports.Name = "lstViewports";
            this.lstViewports.Size = new System.Drawing.Size(210, 212);
            this.lstViewports.TabIndex = 4;
            // 
            // btnClearSelection
            // 
            this.btnClearSelection.Location = new System.Drawing.Point(66, 243);
            this.btnClearSelection.Name = "btnClearSelection";
            this.btnClearSelection.Size = new System.Drawing.Size(75, 23);
            this.btnClearSelection.TabIndex = 7;
            this.btnClearSelection.Text = "Clear";
            this.btnClearSelection.UseVisualStyleBackColor = true;
            this.btnClearSelection.Click += new System.EventHandler(this.btnClearSelection_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(147, 243);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmGetSpecificViewportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(234, 273);
            this.Controls.Add(this.btnClearSelection);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstViewports);
            this.Name = "frmGetSpecificViewportList";
            this.Text = "GetSpecificViewportList";
            this.Load += new System.EventHandler(this.frmGetSpecificViewportList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstViewports;
        private System.Windows.Forms.Button btnClearSelection;
        private System.Windows.Forms.Button btnOK;
    }
}