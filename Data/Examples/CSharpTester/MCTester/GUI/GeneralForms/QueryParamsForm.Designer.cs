namespace MCTester.GUI.Forms
{
    partial class QueryParamsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryParamsForm));
            this.ctrlSpatialQueriesParams = new MCTester.Controls.CtrlQueryParams();
            this.btnStandaloneSQ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlSpatialQueriesParams
            // 
            this.ctrlSpatialQueriesParams.Location = new System.Drawing.Point(1, 1);
            this.ctrlSpatialQueriesParams.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ctrlSpatialQueriesParams.Name = "ctrlSpatialQueriesParams";
            this.ctrlSpatialQueriesParams.Size = new System.Drawing.Size(554, 659);
            this.ctrlSpatialQueriesParams.TabIndex = 4;
            // 
            // btnStandaloneSQ
            // 
            this.btnStandaloneSQ.Location = new System.Drawing.Point(564, 632);
            this.btnStandaloneSQ.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStandaloneSQ.Name = "btnStandaloneSQ";
            this.btnStandaloneSQ.Size = new System.Drawing.Size(119, 28);
            this.btnStandaloneSQ.TabIndex = 5;
            this.btnStandaloneSQ.Text = "Standalone SQ";
            this.btnStandaloneSQ.UseVisualStyleBackColor = true;
            this.btnStandaloneSQ.Click += new System.EventHandler(this.btnStandaloneSQ_Click);
            // 
            // QueryParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(834, 697);
            this.Controls.Add(this.btnStandaloneSQ);
            this.Controls.Add(this.ctrlSpatialQueriesParams);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "QueryParamsForm";
            this.Text = "QueryParamsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MCTester.Controls.CtrlQueryParams ctrlSpatialQueriesParams;
        private System.Windows.Forms.Button btnStandaloneSQ;
    }
}