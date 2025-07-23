namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    partial class ucObjectStateConditionalSelector
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
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.ntxObjectState = new MCTester.Controls.NumericTextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbConditionalSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbConditionalSelector
            // 
            this.gbConditionalSelector.Controls.Add(this.ntxObjectState);
            this.gbConditionalSelector.Controls.Add(this.label3);
            this.gbConditionalSelector.Controls.SetChildIndex(this.label3, 0);
            this.gbConditionalSelector.Controls.SetChildIndex(this.ntxObjectState, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Object state:";
            // 
            // ntxObjectState
            // 
            this.ntxObjectState.Location = new System.Drawing.Point(104, 85);
            this.ntxObjectState.Name = "ntxObjectState";
            this.ntxObjectState.Size = new System.Drawing.Size(66, 22);
            this.ntxObjectState.TabIndex = 16;
            this.ntxObjectState.Validating += new System.ComponentModel.CancelEventHandler(this.ntxObjectState_Validating);
            this.ntxObjectState.Validated += new System.EventHandler(this.ntxObjectState_Validated);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ucObjectStateConditionalSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucObjectStateConditionalSelector";
            this.gbConditionalSelector.ResumeLayout(false);
            this.gbConditionalSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private MCTester.Controls.NumericTextBox ntxObjectState;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
