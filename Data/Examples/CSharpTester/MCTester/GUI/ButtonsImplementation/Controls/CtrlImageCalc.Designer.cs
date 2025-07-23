namespace MCTester.Controls
{
    partial class CtrlImageCalc
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
            this.gbImageCalc = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnNewImageCalc = new System.Windows.Forms.Button();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.lstExistingImageCalc = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gbImageCalc.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbImageCalc
            // 
            this.gbImageCalc.Controls.Add(this.btnDelete);
            this.gbImageCalc.Controls.Add(this.label1);
            this.gbImageCalc.Controls.Add(this.BtnNewImageCalc);
            this.gbImageCalc.Controls.Add(this.btnRefreshList);
            this.gbImageCalc.Controls.Add(this.lstExistingImageCalc);
            this.gbImageCalc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbImageCalc.Location = new System.Drawing.Point(0, 0);
            this.gbImageCalc.Name = "gbImageCalc";
            this.gbImageCalc.Size = new System.Drawing.Size(336, 121);
            this.gbImageCalc.TabIndex = 16;
            this.gbImageCalc.TabStop = false;
            this.gbImageCalc.Text = "Image Calc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Select From List:";
            // 
            // BtnNewImageCalc
            // 
            this.BtnNewImageCalc.Location = new System.Drawing.Point(252, 94);
            this.BtnNewImageCalc.Name = "BtnNewImageCalc";
            this.BtnNewImageCalc.Size = new System.Drawing.Size(75, 23);
            this.BtnNewImageCalc.TabIndex = 34;
            this.BtnNewImageCalc.Text = "Create New";
            this.BtnNewImageCalc.UseVisualStyleBackColor = true;
            this.BtnNewImageCalc.Click += new System.EventHandler(this.BtnNewImageCalc_Click);
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Location = new System.Drawing.Point(171, 94);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshList.TabIndex = 33;
            this.btnRefreshList.Text = "Refresh";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // lstExistingImageCalc
            // 
            this.lstExistingImageCalc.FormattingEnabled = true;
            this.lstExistingImageCalc.Location = new System.Drawing.Point(3, 32);
            this.lstExistingImageCalc.Name = "lstExistingImageCalc";
            this.lstExistingImageCalc.Size = new System.Drawing.Size(324, 56);
            this.lstExistingImageCalc.TabIndex = 32;
            this.lstExistingImageCalc.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstExistingGridCoordSys_MouseDoubleClick);
            this.lstExistingImageCalc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstExistingImageCalc_MouseDown);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(3, 94);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 36;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // CtrlImageCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbImageCalc);
            this.Name = "CtrlImageCalc";
            this.Size = new System.Drawing.Size(336, 121);
            this.gbImageCalc.ResumeLayout(false);
            this.gbImageCalc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbImageCalc;
        private System.Windows.Forms.ListBox lstExistingImageCalc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnNewImageCalc;
        private System.Windows.Forms.Button btnRefreshList;
        private System.Windows.Forms.Button btnDelete;
    }
}
