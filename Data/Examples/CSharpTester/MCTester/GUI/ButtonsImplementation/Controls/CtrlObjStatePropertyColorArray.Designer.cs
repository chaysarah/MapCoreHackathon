namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyColorArray
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
            this.btnRegReset = new System.Windows.Forms.Button();
            this.btnSelReset = new System.Windows.Forms.Button();
            this.SelPropertyColorArray = new MCTester.Controls.ColorArray();
            this.RegPropertyColorArray = new MCTester.Controls.ColorArray();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(541, 316);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(5, 19);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(5);
            this.tcProperty.Size = new System.Drawing.Size(531, 292);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.RegPropertyColorArray);
            this.tpRegular.Controls.Add(this.btnRegReset);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(5);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(5);
            this.tpRegular.Size = new System.Drawing.Size(504, 263);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegReset, 0);
            this.tpRegular.Controls.SetChildIndex(this.RegPropertyColorArray, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.SelPropertyColorArray);
            this.tpObjectState.Controls.Add(this.btnSelReset);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tpObjectState.Size = new System.Drawing.Size(523, 263);
            this.tpObjectState.Controls.SetChildIndex(this.btnSelReset, 0);
            this.tpObjectState.Controls.SetChildIndex(this.SelPropertyColorArray, 0);
            // 
            // btnRegReset
            // 
            this.btnRegReset.Location = new System.Drawing.Point(176, 66);
            this.btnRegReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegReset.Name = "btnRegReset";
            this.btnRegReset.Size = new System.Drawing.Size(100, 28);
            this.btnRegReset.TabIndex = 23;
            this.btnRegReset.Text = "Clear Table";
            this.btnRegReset.UseVisualStyleBackColor = true;
            this.btnRegReset.Click += new System.EventHandler(this.btnRegReset_Click);
            // 
            // btnSelReset
            // 
            this.btnSelReset.Location = new System.Drawing.Point(173, 60);
            this.btnSelReset.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelReset.Name = "btnSelReset";
            this.btnSelReset.Size = new System.Drawing.Size(100, 28);
            this.btnSelReset.TabIndex = 34;
            this.btnSelReset.Text = "Clear Table";
            this.btnSelReset.UseVisualStyleBackColor = true;
            this.btnSelReset.Click += new System.EventHandler(this.btnSelReset_Click);
            // 
            // SelPropertyColorArray
            // 
            this.SelPropertyColorArray.Location = new System.Drawing.Point(3, 104);
            this.SelPropertyColorArray.Margin = new System.Windows.Forms.Padding(5);
            this.SelPropertyColorArray.Name = "SelPropertyColorArray";
            this.SelPropertyColorArray.Size = new System.Drawing.Size(268, 131);
            this.SelPropertyColorArray.TabIndex = 35;
            this.SelPropertyColorArray.Validating += new System.ComponentModel.CancelEventHandler(this.SelPropertyColorArray_Validating);
            // 
            // RegPropertyColorArray
            // 
            this.RegPropertyColorArray.Location = new System.Drawing.Point(8, 94);
            this.RegPropertyColorArray.Margin = new System.Windows.Forms.Padding(5);
            this.RegPropertyColorArray.Name = "RegPropertyColorArray";
            this.RegPropertyColorArray.Size = new System.Drawing.Size(268, 134);
            this.RegPropertyColorArray.TabIndex = 24;
            this.RegPropertyColorArray.Validating += new System.ComponentModel.CancelEventHandler(this.RegPropertyColorArray_Validating);
            // 
            // CtrlObjStatePropertyColorArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "CtrlObjStatePropertyColorArray";
            this.Size = new System.Drawing.Size(541, 316);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ColorArray RegPropertyColorArray;
        private System.Windows.Forms.Button btnRegReset;
        private ColorArray SelPropertyColorArray;
        private System.Windows.Forms.Button btnSelReset;

    }
}
