namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyVector3DArray
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
            this.ctrlRegVector3DArray = new MCTester.Controls.Vector3DArray();
            this.ctrlSelVector3DArray = new MCTester.Controls.Vector3DArray();
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
            this.groupBox1.Size = new System.Drawing.Size(523, 315);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(5, 17);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(5);
            this.tcProperty.Size = new System.Drawing.Size(513, 293);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegVector3DArray);
            this.tpRegular.Controls.Add(this.btnRegReset);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(5);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(5);
            this.tpRegular.Size = new System.Drawing.Size(505, 264);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegReset, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegVector3DArray, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelVector3DArray);
            this.tpObjectState.Controls.Add(this.btnSelReset);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tpObjectState.Size = new System.Drawing.Size(505, 264);
            this.tpObjectState.Controls.SetChildIndex(this.btnSelReset, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelVector3DArray, 0);
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
            // ctrlRegVector3DArray
            // 
            this.ctrlRegVector3DArray.Location = new System.Drawing.Point(5, 103);
            this.ctrlRegVector3DArray.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlRegVector3DArray.Name = "ctrlRegVector3DArray";
            this.ctrlRegVector3DArray.Size = new System.Drawing.Size(424, 134);
            this.ctrlRegVector3DArray.TabIndex = 24;
            this.ctrlRegVector3DArray.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRegVector3DArray_Validating);
            // 
            // ctrlSelVector3DArray
            // 
            this.ctrlSelVector3DArray.Location = new System.Drawing.Point(5, 106);
            this.ctrlSelVector3DArray.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlSelVector3DArray.Name = "ctrlSelVector3DArray";
            this.ctrlSelVector3DArray.Size = new System.Drawing.Size(424, 132);
            this.ctrlSelVector3DArray.TabIndex = 35;
            this.ctrlSelVector3DArray.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSelVector3DArray_Validating);
            // 
            // CtrlObjStatePropertyVector3DArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "CtrlObjStatePropertyVector3DArray";
            this.Size = new System.Drawing.Size(523, 315);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegReset;
        private System.Windows.Forms.Button btnSelReset;
        private Vector3DArray ctrlRegVector3DArray;
        private Vector3DArray ctrlSelVector3DArray;

    }
}
