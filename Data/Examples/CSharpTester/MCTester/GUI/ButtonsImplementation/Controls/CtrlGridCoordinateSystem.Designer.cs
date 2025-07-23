namespace MCTester.Controls
{
    partial class CtrlGridCoordinateSystem
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
            this.m_gbGridCoordinateSystem = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnNewGridCoordSys = new System.Windows.Forms.Button();
            this.btnRefreshList = new System.Windows.Forms.Button();
            this.lstExistingGridCoordSys = new System.Windows.Forms.ListBox();
            this.m_gbGridCoordinateSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_gbGridCoordinateSystem
            // 
            this.m_gbGridCoordinateSystem.Controls.Add(this.label1);
            this.m_gbGridCoordinateSystem.Controls.Add(this.BtnNewGridCoordSys);
            this.m_gbGridCoordinateSystem.Controls.Add(this.btnRefreshList);
            this.m_gbGridCoordinateSystem.Controls.Add(this.lstExistingGridCoordSys);
            this.m_gbGridCoordinateSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gbGridCoordinateSystem.Location = new System.Drawing.Point(0, 0);
            this.m_gbGridCoordinateSystem.Margin = new System.Windows.Forms.Padding(4);
            this.m_gbGridCoordinateSystem.Name = "m_gbGridCoordinateSystem";
            this.m_gbGridCoordinateSystem.Padding = new System.Windows.Forms.Padding(4);
            this.m_gbGridCoordinateSystem.Size = new System.Drawing.Size(440, 164);
            this.m_gbGridCoordinateSystem.TabIndex = 16;
            this.m_gbGridCoordinateSystem.TabStop = false;
            this.m_gbGridCoordinateSystem.Text = "Grid Coordinate System";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 17);
            this.label1.TabIndex = 35;
            this.label1.Text = "Select From List:";
            // 
            // BtnNewGridCoordSys
            // 
            this.BtnNewGridCoordSys.Location = new System.Drawing.Point(335, 131);
            this.BtnNewGridCoordSys.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNewGridCoordSys.Name = "BtnNewGridCoordSys";
            this.BtnNewGridCoordSys.Size = new System.Drawing.Size(100, 28);
            this.BtnNewGridCoordSys.TabIndex = 34;
            this.BtnNewGridCoordSys.Text = "Create New";
            this.BtnNewGridCoordSys.UseVisualStyleBackColor = true;
            this.BtnNewGridCoordSys.Click += new System.EventHandler(this.BtnNewGridCoordSys_Click);
            // 
            // btnRefreshList
            // 
            this.btnRefreshList.Location = new System.Drawing.Point(227, 131);
            this.btnRefreshList.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshList.Name = "btnRefreshList";
            this.btnRefreshList.Size = new System.Drawing.Size(100, 28);
            this.btnRefreshList.TabIndex = 33;
            this.btnRefreshList.Text = "Refresh";
            this.btnRefreshList.UseVisualStyleBackColor = true;
            this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            // 
            // lstExistingGridCoordSys
            // 
            this.lstExistingGridCoordSys.FormattingEnabled = true;
            this.lstExistingGridCoordSys.ItemHeight = 16;
            this.lstExistingGridCoordSys.Location = new System.Drawing.Point(4, 39);
            this.lstExistingGridCoordSys.Margin = new System.Windows.Forms.Padding(4);
            this.lstExistingGridCoordSys.Name = "lstExistingGridCoordSys";
            this.lstExistingGridCoordSys.Size = new System.Drawing.Size(431, 84);
            this.lstExistingGridCoordSys.TabIndex = 32;
            this.lstExistingGridCoordSys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstExistingGridCoordSys_MouseDoubleClick);
            this.lstExistingGridCoordSys.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstExistingGridCoordSys_MouseDown);
            // 
            // CtrlGridCoordinateSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_gbGridCoordinateSystem);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CtrlGridCoordinateSystem";
            this.Size = new System.Drawing.Size(440, 164);
            this.m_gbGridCoordinateSystem.ResumeLayout(false);
            this.m_gbGridCoordinateSystem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox m_gbGridCoordinateSystem;
        private System.Windows.Forms.ListBox lstExistingGridCoordSys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnNewGridCoordSys;
        private System.Windows.Forms.Button btnRefreshList;
    }
}
