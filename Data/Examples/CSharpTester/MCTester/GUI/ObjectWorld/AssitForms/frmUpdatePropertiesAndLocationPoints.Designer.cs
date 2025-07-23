namespace MCTester.ObjectWorld.Assit_Forms
{
    partial class frmUpdatePropertiesAndLocationPoints
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
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.ntxStartIndex = new MCTester.Controls.NumericTextBox();
            this.ntxLocationPointsIndex = new MCTester.Controls.NumericTextBox();
            this.btnGetLocationPoints = new System.Windows.Forms.Button();
            this.pnlLocations = new System.Windows.Forms.Panel();
            this.ctrlNewLocationPoints = new MCTester.Controls.CtrlPointsGrid();
            this.pnlLocations.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 91;
            this.label9.Text = "Start Index:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "Location Index:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 34);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 92;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ntxStartIndex
            // 
            this.ntxStartIndex.Location = new System.Drawing.Point(98, 11);
            this.ntxStartIndex.Name = "ntxStartIndex";
            this.ntxStartIndex.Size = new System.Drawing.Size(58, 20);
            this.ntxStartIndex.TabIndex = 90;
            this.ntxStartIndex.Text = "0";
            // 
            // ntxLocationPointsIndex
            // 
            this.ntxLocationPointsIndex.Location = new System.Drawing.Point(98, 37);
            this.ntxLocationPointsIndex.Name = "ntxLocationPointsIndex";
            this.ntxLocationPointsIndex.Size = new System.Drawing.Size(58, 20);
            this.ntxLocationPointsIndex.TabIndex = 88;
            this.ntxLocationPointsIndex.Text = "0";
            // 
            // btnGetLocationPoints
            // 
            this.btnGetLocationPoints.Location = new System.Drawing.Point(9, 60);
            this.btnGetLocationPoints.Name = "btnGetLocationPoints";
            this.btnGetLocationPoints.Size = new System.Drawing.Size(148, 23);
            this.btnGetLocationPoints.TabIndex = 94;
            this.btnGetLocationPoints.Text = "Get Location Points";
            this.btnGetLocationPoints.UseVisualStyleBackColor = true;
            this.btnGetLocationPoints.Click += new System.EventHandler(this.btnGetLocationPoints_Click);
            // 
            // pnlLocations
            // 
            this.pnlLocations.Controls.Add(this.label9);
            this.pnlLocations.Controls.Add(this.btnGetLocationPoints);
            this.pnlLocations.Controls.Add(this.btnOK);
            this.pnlLocations.Controls.Add(this.ntxLocationPointsIndex);
            this.pnlLocations.Controls.Add(this.label8);
            this.pnlLocations.Controls.Add(this.ntxStartIndex);
            this.pnlLocations.Location = new System.Drawing.Point(2, 208);
            this.pnlLocations.Margin = new System.Windows.Forms.Padding(2);
            this.pnlLocations.Name = "pnlLocations";
            this.pnlLocations.Size = new System.Drawing.Size(298, 83);
            this.pnlLocations.TabIndex = 95;
            // 
            // ctrlNewLocationPoints
            // 
            this.ctrlNewLocationPoints.Location = new System.Drawing.Point(2, 0);
            this.ctrlNewLocationPoints.Name = "ctrlNewLocationPoints";
            this.ctrlNewLocationPoints.Size = new System.Drawing.Size(345, 196);
            this.ctrlNewLocationPoints.TabIndex = 96;
            // 
            // frmUpdatePropertiesAndLocationPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(358, 299);
            this.Controls.Add(this.ctrlNewLocationPoints);
            this.Controls.Add(this.pnlLocations);
            this.Name = "frmUpdatePropertiesAndLocationPoints";
            this.Text = "frmUpdatePropertiesAndLocationPoints";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmUpdatePropertiesAndLocationPoints_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUpdatePropertiesAndLocationPoints_FormClosed);
            this.pnlLocations.ResumeLayout(false);
            this.pnlLocations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private MCTester.Controls.NumericTextBox ntxStartIndex;
        private System.Windows.Forms.Label label8;
        private MCTester.Controls.NumericTextBox ntxLocationPointsIndex;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnGetLocationPoints;
        private System.Windows.Forms.Panel pnlLocations;
        private Controls.CtrlPointsGrid ctrlNewLocationPoints;
    }
}