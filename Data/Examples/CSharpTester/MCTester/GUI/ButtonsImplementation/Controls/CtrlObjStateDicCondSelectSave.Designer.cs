namespace MCTester.Controls
{
    partial class CtrlObjStateDicCondSelectSave
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
            this.btnSet = new System.Windows.Forms.Button();
            this.ctrlObjStatePropertyDicConditionalSelector = new MCTester.Controls.CtrlObjStatePropertyDicConditionalSelector();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(327, 238);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 1;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // ctrlObjStatePropertyDicConditionalSelector
            // 
            this.ctrlObjStatePropertyDicConditionalSelector.ActionTypeKey = MapCore.DNEActionType._EAT_VISIBILITY;
            this.ctrlObjStatePropertyDicConditionalSelector.CurrObject = null;
            this.ctrlObjStatePropertyDicConditionalSelector.Location = new System.Drawing.Point(5, 9);
            this.ctrlObjStatePropertyDicConditionalSelector.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyDicConditionalSelector.Name = "ctrlObjStatePropertyDicConditionalSelector";
            this.ctrlObjStatePropertyDicConditionalSelector.PropertyName = "Conditional Selector";
            this.ctrlObjStatePropertyDicConditionalSelector.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyDicConditionalSelector.SelectorsArr = null;
            this.ctrlObjStatePropertyDicConditionalSelector.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyDicConditionalSelector.Size = new System.Drawing.Size(397, 224);
            this.ctrlObjStatePropertyDicConditionalSelector.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctrlObjStatePropertyDicConditionalSelector);
            this.groupBox1.Controls.Add(this.btnSet);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 266);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // CtrlObjStateDicCondSelectSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CtrlObjStateDicCondSelectSave";
            this.Size = new System.Drawing.Size(417, 273);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MCTester.Controls.CtrlObjStatePropertyDicConditionalSelector ctrlObjStatePropertyDicConditionalSelector;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
