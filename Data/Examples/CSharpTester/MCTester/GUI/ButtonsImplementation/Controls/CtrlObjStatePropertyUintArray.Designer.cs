namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyUintArray
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
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RegNumberArray = new MCTester.Controls.NumberArray();
            this.SelNumberArray = new MCTester.Controls.NumberArray();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(403, 159);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 16);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4);
            this.tcProperty.Size = new System.Drawing.Size(395, 139);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.RegNumberArray);
            this.tpRegular.Controls.Add(this.label8);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4);
            this.tpRegular.Size = new System.Drawing.Size(387, 113);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.label8, 0);
            this.tpRegular.Controls.SetChildIndex(this.RegNumberArray, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.SelNumberArray);
            this.tpObjectState.Controls.Add(this.label6);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(387, 113);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.label6, 0);
            this.tpObjectState.Controls.SetChildIndex(this.SelNumberArray, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(76, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Separate input by space:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(76, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 13);
            this.label6.TabIndex = 47;
            this.label6.Text = "Separate input by space:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // RegNumberArray
            // 
            this.RegNumberArray.IsSeperateWithTwiceSpace = true;
            this.RegNumberArray.Location = new System.Drawing.Point(77, 65);
            this.RegNumberArray.mNumbersType = MCTester.Controls.NumberArray.NumbersType.Int;
            this.RegNumberArray.Name = "RegNumberArray";
            this.RegNumberArray.SeperateNumbers = 1;
            this.RegNumberArray.Size = new System.Drawing.Size(260, 21);
            this.RegNumberArray.TabIndex = 46;
            this.RegNumberArray.Validating += new System.ComponentModel.CancelEventHandler(this.RegNumberArray_Validating);
            // 
            // SelNumberArray
            // 
            this.SelNumberArray.IsSeperateWithTwiceSpace = true;
            this.SelNumberArray.Location = new System.Drawing.Point(77, 65);
            this.SelNumberArray.mNumbersType = MCTester.Controls.NumberArray.NumbersType.Int;
            this.SelNumberArray.Name = "SelNumberArray";
            this.SelNumberArray.SeperateNumbers = 1;
            this.SelNumberArray.Size = new System.Drawing.Size(260, 19);
            this.SelNumberArray.TabIndex = 48;
            this.SelNumberArray.Validating += new System.ComponentModel.CancelEventHandler(this.SelNumberArray_Validating);
            // 
            // CtrlObjStatePropertyUintArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyUintArray";
            this.Size = new System.Drawing.Size(403, 159);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private NumberArray RegNumberArray;
        private NumberArray SelNumberArray;
    }
}
