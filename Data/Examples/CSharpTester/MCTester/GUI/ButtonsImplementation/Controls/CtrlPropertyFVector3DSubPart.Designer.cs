namespace MCTester.Controls
{
    partial class CtrlPropertyFVector3DSubPart
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
            this.lblRegAttachPoint = new System.Windows.Forms.Label();
            this.ntxRegAttachPointID = new MCTester.Controls.NumericTextBox();
            this.ntxSelAttachPointID = new MCTester.Controls.NumericTextBox();
            this.lblSelAttachPoint = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ntxRegAttachPointID);
            this.tpRegular.Controls.Add(this.lblRegAttachPoint);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrl3DRegFVector, 0);
            this.tpRegular.Controls.SetChildIndex(this.lblRegAttachPoint, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegAttachPointID, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.lblSelAttachPoint);
            this.tpObjectState.Controls.Add(this.ntxSelAttachPointID);
            this.tpObjectState.Controls.SetChildIndex(this.ctrl3DSelFVector, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelAttachPointID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.lblSelAttachPoint, 0);
            // 
            // lblRegAttachPoint
            // 
            this.lblRegAttachPoint.AutoSize = true;
            this.lblRegAttachPoint.Location = new System.Drawing.Point(332, 38);
            this.lblRegAttachPoint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegAttachPoint.Name = "lblRegAttachPoint";
            this.lblRegAttachPoint.Size = new System.Drawing.Size(42, 17);
            this.lblRegAttachPoint.TabIndex = 25;
            this.lblRegAttachPoint.Text = "label:";
            // 
            // ntxRegAttachPointID
            // 
            this.ntxRegAttachPointID.Location = new System.Drawing.Point(421, 34);
            this.ntxRegAttachPointID.Margin = new System.Windows.Forms.Padding(4);
            this.ntxRegAttachPointID.Name = "ntxRegAttachPointID";
            this.ntxRegAttachPointID.Size = new System.Drawing.Size(84, 22);
            this.ntxRegAttachPointID.TabIndex = 26;
            // 
            // ntxSelAttachPointID
            // 
            this.ntxSelAttachPointID.Location = new System.Drawing.Point(422, 35);
            this.ntxSelAttachPointID.Margin = new System.Windows.Forms.Padding(4);
            this.ntxSelAttachPointID.Name = "ntxSelAttachPointID";
            this.ntxSelAttachPointID.Size = new System.Drawing.Size(84, 22);
            this.ntxSelAttachPointID.TabIndex = 34;
            // 
            // lblSelAttachPoint
            // 
            this.lblSelAttachPoint.AutoSize = true;
            this.lblSelAttachPoint.Location = new System.Drawing.Point(337, 37);
            this.lblSelAttachPoint.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelAttachPoint.Name = "lblSelAttachPoint";
            this.lblSelAttachPoint.Size = new System.Drawing.Size(42, 17);
            this.lblSelAttachPoint.TabIndex = 35;
            this.lblSelAttachPoint.Text = "label:";
            // 
            // CtrlPropertyFVector3DSubPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CtrlPropertyFVector3DSubPart";
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRegAttachPoint;
        public NumericTextBox ntxRegAttachPointID;
        public NumericTextBox ntxSelAttachPointID;
        private System.Windows.Forms.Label lblSelAttachPoint;
    }
}
