namespace MCTester.General_Forms
{
    partial class frmBuildIndexingDataForRawStaticObjects
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
            this.btnBuildIndexingData = new System.Windows.Forms.Button();
            this.ctrlBuildIndexingDataParams1 = new MCTester.Controls.CtrlBuildIndexingDataParams();
            this.SuspendLayout();
            // 
            // btnBuildIndexingData
            // 
            this.btnBuildIndexingData.Location = new System.Drawing.Point(345, 544);
            this.btnBuildIndexingData.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuildIndexingData.Name = "btnBuildIndexingData";
            this.btnBuildIndexingData.Size = new System.Drawing.Size(122, 25);
            this.btnBuildIndexingData.TabIndex = 1;
            this.btnBuildIndexingData.Text = "Build Indexing Data";
            this.btnBuildIndexingData.UseVisualStyleBackColor = true;
            this.btnBuildIndexingData.Click += new System.EventHandler(this.btnBuildIndexingData_Click);
            // 
            // ctrlBuildIndexingDataParams1
            // 
            this.ctrlBuildIndexingDataParams1.Location = new System.Drawing.Point(9, 9);
            this.ctrlBuildIndexingDataParams1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlBuildIndexingDataParams1.Name = "ctrlBuildIndexingDataParams1";
            this.ctrlBuildIndexingDataParams1.Size = new System.Drawing.Size(785, 526);
            this.ctrlBuildIndexingDataParams1.TabIndex = 0;
            // 
            // frmBuildIndexingDataForRawStaticObjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(809, 574);
            this.Controls.Add(this.btnBuildIndexingData);
            this.Controls.Add(this.ctrlBuildIndexingDataParams1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmBuildIndexingDataForRawStaticObjects";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtrlBuildIndexingDataParams ctrlBuildIndexingDataParams1;
        private System.Windows.Forms.Button btnBuildIndexingData;
    }
}