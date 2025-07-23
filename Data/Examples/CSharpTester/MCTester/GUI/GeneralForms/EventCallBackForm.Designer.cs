namespace MCTester.GUI.Forms
{
    partial class EventCallBackForm
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
            this.lsvEventCallBack = new System.Windows.Forms.ListView();
            this.NumEventCallBack = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EventDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClearEventList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvEventCallBack
            // 
            this.lsvEventCallBack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumEventCallBack,
            this.EventDescription});
            this.lsvEventCallBack.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsvEventCallBack.Location = new System.Drawing.Point(0, 0);
            this.lsvEventCallBack.Name = "lsvEventCallBack";
            this.lsvEventCallBack.Size = new System.Drawing.Size(895, 316);
            this.lsvEventCallBack.TabIndex = 0;
            this.lsvEventCallBack.UseCompatibleStateImageBehavior = false;
            this.lsvEventCallBack.View = System.Windows.Forms.View.Details;
            // 
            // NumEventCallBack
            // 
            this.NumEventCallBack.Text = "Number";
            // 
            // EventDescription
            // 
            this.EventDescription.Text = "Event Description";
            this.EventDescription.Width = 800;
            // 
            // btnClearEventList
            // 
            this.btnClearEventList.Location = new System.Drawing.Point(818, 321);
            this.btnClearEventList.Name = "btnClearEventList";
            this.btnClearEventList.Size = new System.Drawing.Size(75, 23);
            this.btnClearEventList.TabIndex = 1;
            this.btnClearEventList.Text = "Clear";
            this.btnClearEventList.UseVisualStyleBackColor = true;
            this.btnClearEventList.Click += new System.EventHandler(this.btnClearEventList_Click);
            // 
            // EventCallBackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 348);
            this.Controls.Add(this.btnClearEventList);
            this.Controls.Add(this.lsvEventCallBack);
            this.Name = "EventCallBackForm";
            this.Text = "Event CallBack Form";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EventCallBackForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvEventCallBack;
        private System.Windows.Forms.ColumnHeader NumEventCallBack;
        private System.Windows.Forms.ColumnHeader EventDescription;
        private System.Windows.Forms.Button btnClearEventList;
    }
}