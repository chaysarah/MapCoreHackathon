namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyFVector2DArray
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
            this.ctrlRegVector2DArray = new MCTester.Controls.Vector2DArray();
            this.ctrlSelVector2DArray = new MCTester.Controls.Vector2DArray();
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
            this.groupBox1.Size = new System.Drawing.Size(544, 314);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(5, 16);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(5);
            this.tcProperty.Size = new System.Drawing.Size(534, 293);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.ctrlRegVector2DArray);
            this.tpRegular.Controls.Add(this.btnRegReset);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(5);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(5);
            this.tpRegular.Size = new System.Drawing.Size(507, 264);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.btnRegReset, 0);
            this.tpRegular.Controls.SetChildIndex(this.ctrlRegVector2DArray, 0);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.ctrlSelVector2DArray);
            this.tpObjectState.Controls.Add(this.btnSelReset);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tpObjectState.Size = new System.Drawing.Size(526, 264);
            this.tpObjectState.Controls.SetChildIndex(this.btnSelReset, 0);
            this.tpObjectState.Controls.SetChildIndex(this.ctrlSelVector2DArray, 0);
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
            // ctrlRegVector2DArray
            // 
            this.ctrlRegVector2DArray.Location = new System.Drawing.Point(8, 102);
            this.ctrlRegVector2DArray.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlRegVector2DArray.Name = "ctrlRegVector2DArray";
            this.ctrlRegVector2DArray.Size = new System.Drawing.Size(324, 130);
            this.ctrlRegVector2DArray.TabIndex = 24;
            this.ctrlRegVector2DArray.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlRegVector2DArray_Validating);
            // 
            // ctrlSelVector2DArray
            // 
            this.ctrlSelVector2DArray.Location = new System.Drawing.Point(9, 100);
            this.ctrlSelVector2DArray.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlSelVector2DArray.Name = "ctrlSelVector2DArray";
            this.ctrlSelVector2DArray.Size = new System.Drawing.Size(324, 132);
            this.ctrlSelVector2DArray.TabIndex = 35;
            this.ctrlSelVector2DArray.Validating += new System.ComponentModel.CancelEventHandler(this.ctrlSelVector2DArray_Validating);
            // 
            // CtrlObjStatePropertyFVector2DArray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "CtrlObjStatePropertyFVector2DArray";
            this.Size = new System.Drawing.Size(544, 314);
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
        private Vector2DArray ctrlRegVector2DArray;
        private Vector2DArray ctrlSelVector2DArray;

    }
}
