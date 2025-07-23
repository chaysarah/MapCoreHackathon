namespace MCTester.Controls
{
    partial class CtrlPropertySubPartOrientation
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
            this.lblRegAttachPointID = new System.Windows.Forms.Label();
            this.ntxRegAttachPointID = new MCTester.Controls.NumericTextBox();
            this.lblSelAttachPointID = new System.Windows.Forms.Label();
            this.ntxSelAttachPointID = new MCTester.Controls.NumericTextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click_1);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ntxRegAttachPointID);
            this.tpRegular.Controls.Add(this.lblRegAttachPointID);
            this.tpRegular.Controls.SetChildIndex(this.chkRegRelativeToCurrOrientation, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrl3DRegOrientation, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnApply, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegAttachPointID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegAttachPointID, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.lblSelAttachPointID);
            this.tpSelection.Controls.Add(this.ntxSelAttachPointID);
            this.tpSelection.Controls.SetChildIndex(this.rdbSelShared, 0);
            this.tpSelection.Controls.SetChildIndex(this.ctrl3DSelOrientation, 0);
            this.tpSelection.Controls.SetChildIndex(this.chkSelRelativeToCurrOrientation, 0);
            this.tpSelection.Controls.SetChildIndex(this.ntxSelAttachPointID, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelAttachPointID, 0);
            // 
            // lblRegAttachPointID
            // 
            this.lblRegAttachPointID.AutoSize = true;
            this.lblRegAttachPointID.Location = new System.Drawing.Point(224, 31);
            this.lblRegAttachPointID.Name = "lblRegAttachPointID";
            this.lblRegAttachPointID.Size = new System.Drawing.Size(32, 13);
            this.lblRegAttachPointID.TabIndex = 28;
            this.lblRegAttachPointID.Text = "label:";
            // 
            // ntxRegAttachPointID
            // 
            this.ntxRegAttachPointID.Location = new System.Drawing.Point(310, 28);
            this.ntxRegAttachPointID.Name = "ntxRegAttachPointID";
            this.ntxRegAttachPointID.Size = new System.Drawing.Size(64, 20);
            this.ntxRegAttachPointID.TabIndex = 29;
            // 
            // lblSelAttachPointID
            // 
            this.lblSelAttachPointID.AutoSize = true;
            this.lblSelAttachPointID.Location = new System.Drawing.Point(224, 31);
            this.lblSelAttachPointID.Name = "lblSelAttachPointID";
            this.lblSelAttachPointID.Size = new System.Drawing.Size(32, 13);
            this.lblSelAttachPointID.TabIndex = 30;
            this.lblSelAttachPointID.Text = "label:";
            // 
            // ntxSelAttachPointID
            // 
            this.ntxSelAttachPointID.Location = new System.Drawing.Point(310, 28);
            this.ntxSelAttachPointID.Name = "ntxSelAttachPointID";
            this.ntxSelAttachPointID.Size = new System.Drawing.Size(64, 20);
            this.ntxSelAttachPointID.TabIndex = 31;
            // 
            // CtrlPropertySubPartOrientation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertySubPartOrientation";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public NumericTextBox ntxRegAttachPointID;
        private System.Windows.Forms.Label lblRegAttachPointID;
        private System.Windows.Forms.Label lblSelAttachPointID;
        public NumericTextBox ntxSelAttachPointID;
    }
}
