namespace MCTester.Controls
{
    partial class CtrlPropertyBoolSubPart
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
            this.ntxRegAttachPointID = new MCTester.Controls.NumericTextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblRegAttachPoint = new System.Windows.Forms.Label();
            this.ntxSelAttachPointID = new MCTester.Controls.NumericTextBox();
            this.lblSelAttachPoint = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ntxRegAttachPointID);
            this.tpRegular.Controls.Add(this.btnApply);
            this.tpRegular.Controls.Add(this.lblRegAttachPoint);
            this.tpRegular.Controls.SetChildIndex(this.chxRegBool, 0);
            this.tpRegular.Controls.SetChildIndex(this.chxSelectionProperty, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegAttachPoint, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnApply, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegAttachPointID, 0);
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.ntxSelAttachPointID);
            this.tpSelection.Controls.Add(this.lblSelAttachPoint);
            this.tpSelection.Controls.SetChildIndex(this.chxSelBool, 0);
            this.tpSelection.Controls.SetChildIndex(this.lblSelAttachPoint, 0);
            this.tpSelection.Controls.SetChildIndex(this.ntxSelAttachPointID, 0);
            // 
            // ntxRegAttachPointID
            // 
            this.ntxRegAttachPointID.Location = new System.Drawing.Point(316, 28);
            this.ntxRegAttachPointID.Name = "ntxRegAttachPointID";
            this.ntxRegAttachPointID.Size = new System.Drawing.Size(64, 20);
            this.ntxRegAttachPointID.TabIndex = 29;
            this.ntxRegAttachPointID.TextChanged += new System.EventHandler(this.ntxRegAttachPointID_TextChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(316, 54);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(64, 23);
            this.btnApply.TabIndex = 30;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Visible = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblRegAttachPoint
            // 
            this.lblRegAttachPoint.AutoSize = true;
            this.lblRegAttachPoint.Location = new System.Drawing.Point(249, 31);
            this.lblRegAttachPoint.Name = "lblRegAttachPoint";
            this.lblRegAttachPoint.Size = new System.Drawing.Size(32, 13);
            this.lblRegAttachPoint.TabIndex = 28;
            this.lblRegAttachPoint.Text = "label:";
            // 
            // ntxSelAttachPointID
            // 
            this.ntxSelAttachPointID.Location = new System.Drawing.Point(316, 28);
            this.ntxSelAttachPointID.Name = "ntxSelAttachPointID";
            this.ntxSelAttachPointID.Size = new System.Drawing.Size(64, 20);
            this.ntxSelAttachPointID.TabIndex = 32;
            this.ntxSelAttachPointID.TextChanged += new System.EventHandler(this.ntxSelAttachPointID_TextChanged);
            // 
            // lblSelAttachPoint
            // 
            this.lblSelAttachPoint.AutoSize = true;
            this.lblSelAttachPoint.Location = new System.Drawing.Point(251, 31);
            this.lblSelAttachPoint.Name = "lblSelAttachPoint";
            this.lblSelAttachPoint.Size = new System.Drawing.Size(32, 13);
            this.lblSelAttachPoint.TabIndex = 31;
            this.lblSelAttachPoint.Text = "label:";
            // 
            // CtrlPropertyBoolSubPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CtrlPropertyBoolSubPart";
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
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblRegAttachPoint;
        public NumericTextBox ntxSelAttachPointID;
        private System.Windows.Forms.Label lblSelAttachPoint;
    }
}
