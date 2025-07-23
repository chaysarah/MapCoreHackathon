namespace MCTester.MapWorld.WizardForms
{
    partial class LayersWizzardForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.ctrlLayers1 = new MCTester.MapWorld.WizardForms.CtrlLayers();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(242, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ctrlLayers1
            // 
            this.ctrlLayers1.Location = new System.Drawing.Point(7, 4);
            this.ctrlLayers1.Name = "ctrlLayers1";
            this.ctrlLayers1.Size = new System.Drawing.Size(270, 162);
            this.ctrlLayers1.TabIndex = 17;
            // 
            // LayersWizzardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 209);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrlLayers1);
            this.Name = "LayersWizzardForm";
            this.Text = "Layers Wizzard Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LayersWizzardForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlLayers ctrlLayers1;
        private System.Windows.Forms.Button button1;
    }
}