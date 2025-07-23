namespace MCTester.General_Forms
{
    partial class RenderNeededForm
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
            this.lsvRenderNeeded = new System.Windows.Forms.ListView();
            this.NumRenderNeeded = new System.Windows.Forms.ColumnHeader();
            this.RenderNeededResult = new System.Windows.Forms.ColumnHeader();
            this.RenderNeededCounter = new System.Windows.Forms.ColumnHeader();
            this.btnClearEventList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvRenderNeeded
            // 
            this.lsvRenderNeeded.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumRenderNeeded,
            this.RenderNeededResult,
            this.RenderNeededCounter});
            this.lsvRenderNeeded.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsvRenderNeeded.Location = new System.Drawing.Point(0, 0);
            this.lsvRenderNeeded.Name = "lsvRenderNeeded";
            this.lsvRenderNeeded.Size = new System.Drawing.Size(282, 316);
            this.lsvRenderNeeded.TabIndex = 1;
            this.lsvRenderNeeded.UseCompatibleStateImageBehavior = false;
            this.lsvRenderNeeded.View = System.Windows.Forms.View.Details;
            // 
            // NumRenderNeeded
            // 
            this.NumRenderNeeded.Text = "Line Number";
            this.NumRenderNeeded.Width = 72;
            // 
            // RenderNeededResult
            // 
            this.RenderNeededResult.Text = "Render Needed Result";
            this.RenderNeededResult.Width = 150;
            // 
            // RenderNeededCounter
            // 
            this.RenderNeededCounter.Text = "Counter";
            this.RenderNeededCounter.Width = 100;
            // 
            // btnClearEventList
            // 
            this.btnClearEventList.Location = new System.Drawing.Point(199, 322);
            this.btnClearEventList.Name = "btnClearEventList";
            this.btnClearEventList.Size = new System.Drawing.Size(75, 23);
            this.btnClearEventList.TabIndex = 2;
            this.btnClearEventList.Text = "Clear";
            this.btnClearEventList.UseVisualStyleBackColor = true;
            this.btnClearEventList.Click += new System.EventHandler(this.btnClearEventList_Click);
            // 
            // RenderNeededForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(282, 350);
            this.Controls.Add(this.btnClearEventList);
            this.Controls.Add(this.lsvRenderNeeded);
            this.Name = "RenderNeededForm";
            this.Text = "RenderNeededForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RenderNeededForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvRenderNeeded;
        private System.Windows.Forms.ColumnHeader NumRenderNeeded;
        private System.Windows.Forms.ColumnHeader RenderNeededResult;
        private System.Windows.Forms.Button btnClearEventList;
        private System.Windows.Forms.ColumnHeader RenderNeededCounter;
    }
}