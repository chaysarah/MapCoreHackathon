namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyDicConditionalSelector
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
            this.cbActionType = new System.Windows.Forms.ComboBox();
            this.chxActionOnResult = new System.Windows.Forms.CheckBox();
            this.clstRegConditionalSelector = new System.Windows.Forms.CheckedListBox();
            this.clstSelConditionalSelector = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.chxActionOnResult);
            this.groupBox1.Controls.Add(this.cbActionType);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(397, 225);
            this.groupBox1.Controls.SetChildIndex(this.cbActionType, 0);
            this.groupBox1.Controls.SetChildIndex(this.tcProperty, 0);
            this.groupBox1.Controls.SetChildIndex(this.chxActionOnResult, 0);
            this.groupBox1.Controls.SetChildIndex(this.label4, 0);
            // 
            // tcProperty
            // 
            this.tcProperty.Location = new System.Drawing.Point(4, 44);
            this.tcProperty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcProperty.Size = new System.Drawing.Size(389, 177);
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.clstRegConditionalSelector);
            this.tpRegular.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpRegular.Size = new System.Drawing.Size(381, 151);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegShared, 0);
            this.tpRegular.Controls.SetChildIndex(this.rdbRegPrivate, 0);
            this.tpRegular.Controls.SetChildIndex(this.ntxRegPropertyID, 0);
            this.tpRegular.Controls.SetChildIndex(this.clstRegConditionalSelector, 0);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.clstSelConditionalSelector);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tpObjectState.Size = new System.Drawing.Size(381, 151);
            this.tpObjectState.Controls.SetChildIndex(this.ntxSelPropertyID, 0);
            this.tpObjectState.Controls.SetChildIndex(this.rdbSelPrivate, 0);
            this.tpObjectState.Controls.SetChildIndex(this.clstSelConditionalSelector, 0);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // cbActionType
            // 
            this.cbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActionType.FormattingEnabled = true;
            this.cbActionType.Location = new System.Drawing.Point(76, 19);
            this.cbActionType.Name = "cbActionType";
            this.cbActionType.Size = new System.Drawing.Size(175, 21);
            this.cbActionType.TabIndex = 27;
            this.cbActionType.SelectedIndexChanged += new System.EventHandler(this.cbActionType_SelectedIndexChanged);
            // 
            // chxActionOnResult
            // 
            this.chxActionOnResult.AutoSize = true;
            this.chxActionOnResult.Location = new System.Drawing.Point(262, 21);
            this.chxActionOnResult.Name = "chxActionOnResult";
            this.chxActionOnResult.Size = new System.Drawing.Size(106, 17);
            this.chxActionOnResult.TabIndex = 23;
            this.chxActionOnResult.Text = "Action On Result";
            this.chxActionOnResult.UseVisualStyleBackColor = true;
            // 
            // clstRegConditionalSelector
            // 
            this.clstRegConditionalSelector.CheckOnClick = true;
            this.clstRegConditionalSelector.FormattingEnabled = true;
            this.clstRegConditionalSelector.HorizontalScrollbar = true;
            this.clstRegConditionalSelector.Location = new System.Drawing.Point(83, 50);
            this.clstRegConditionalSelector.Name = "clstRegConditionalSelector";
            this.clstRegConditionalSelector.Size = new System.Drawing.Size(260, 64);
            this.clstRegConditionalSelector.TabIndex = 76;
            this.clstRegConditionalSelector.SelectedIndexChanged += new System.EventHandler(this.clstRegConditionalSelector_SelectedIndexChanged);
            this.clstRegConditionalSelector.Validating += new System.ComponentModel.CancelEventHandler(this.clstRegConditionalSelector_Validating);
            // 
            // clstSelConditionalSelector
            // 
            this.clstSelConditionalSelector.CheckOnClick = true;
            this.clstSelConditionalSelector.FormattingEnabled = true;
            this.clstSelConditionalSelector.HorizontalScrollbar = true;
            this.clstSelConditionalSelector.Location = new System.Drawing.Point(83, 50);
            this.clstSelConditionalSelector.Name = "clstSelConditionalSelector";
            this.clstSelConditionalSelector.Size = new System.Drawing.Size(260, 79);
            this.clstSelConditionalSelector.TabIndex = 76;
            this.clstSelConditionalSelector.SelectedIndexChanged += new System.EventHandler(this.clstSelConditionalSelector_SelectedIndexChanged);
            this.clstSelConditionalSelector.Validating += new System.ComponentModel.CancelEventHandler(this.clstSelConditionalSelector_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Action Type:";
            // 
            // CtrlObjStatePropertyDicConditionalSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CtrlObjStatePropertyDicConditionalSelector";
            this.Size = new System.Drawing.Size(397, 225);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbActionType;
        private System.Windows.Forms.CheckBox chxActionOnResult;
        private System.Windows.Forms.CheckedListBox clstRegConditionalSelector;
        private System.Windows.Forms.CheckedListBox clstSelConditionalSelector;
        private System.Windows.Forms.Label label4;
    }
}
