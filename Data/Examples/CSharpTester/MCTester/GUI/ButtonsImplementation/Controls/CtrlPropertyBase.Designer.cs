namespace MCTester.Controls
{
    partial class CtrlPropertyBase
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
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tcProperty = new System.Windows.Forms.TabControl();
            this.tpRegular = new System.Windows.Forms.TabPage();
            this.chxSelectionProperty = new System.Windows.Forms.CheckBox();
            this.rdbRegPrivate = new System.Windows.Forms.RadioButton();
            this.rdbRegShared = new System.Windows.Forms.RadioButton();
            this.ntxRegPropertyID = new MCTester.Controls.NumericTextBox();
            this.tpSelection = new System.Windows.Forms.TabPage();
            this.rdbSelPrivate = new System.Windows.Forms.RadioButton();
            this.rdbSelShared = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ntxSelPropertyID = new MCTester.Controls.NumericTextBox();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Property ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tcProperty);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Property Name";
            // 
            // tcProperty
            // 
            this.tcProperty.Controls.Add(this.tpRegular);
            this.tcProperty.Controls.Add(this.tpSelection);
            this.tcProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcProperty.Location = new System.Drawing.Point(3, 16);
            this.tcProperty.Name = "tcProperty";
            this.tcProperty.SelectedIndex = 0;
            this.tcProperty.Size = new System.Drawing.Size(394, 111);
            this.tcProperty.TabIndex = 18;
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.chxSelectionProperty);
            this.tpRegular.Controls.Add(this.rdbRegPrivate);
            this.tpRegular.Controls.Add(this.rdbRegShared);
            this.tpRegular.Controls.Add(this.ntxRegPropertyID);
            this.tpRegular.Controls.Add(this.label5);
            this.tpRegular.Location = new System.Drawing.Point(4, 22);
            this.tpRegular.Name = "tpRegular";
            this.tpRegular.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegular.Size = new System.Drawing.Size(386, 85);
            this.tpRegular.TabIndex = 0;
            this.tpRegular.Text = "Regular";
            this.tpRegular.UseVisualStyleBackColor = true;
            // 
            // chxSelectionProperty
            // 
            this.chxSelectionProperty.AutoSize = true;
            this.chxSelectionProperty.Location = new System.Drawing.Point(6, 6);
            this.chxSelectionProperty.Name = "chxSelectionProperty";
            this.chxSelectionProperty.Size = new System.Drawing.Size(142, 17);
            this.chxSelectionProperty.TabIndex = 24;
            this.chxSelectionProperty.Text = "Selection Property Exists";
            this.chxSelectionProperty.UseVisualStyleBackColor = true;
            this.chxSelectionProperty.CheckedChanged += new System.EventHandler(this.chxSelectionProperty_CheckedChange);
            // 
            // rdbRegPrivate
            // 
            this.rdbRegPrivate.AutoSize = true;
            this.rdbRegPrivate.Location = new System.Drawing.Point(6, 29);
            this.rdbRegPrivate.Name = "rdbRegPrivate";
            this.rdbRegPrivate.Size = new System.Drawing.Size(58, 17);
            this.rdbRegPrivate.TabIndex = 20;
            this.rdbRegPrivate.Text = "Private";
            this.rdbRegPrivate.UseVisualStyleBackColor = true;
            this.rdbRegPrivate.CheckedChanged += new System.EventHandler(this.rdbRegPrivate_CheckedChanged);
            // 
            // rdbRegShared
            // 
            this.rdbRegShared.AutoSize = true;
            this.rdbRegShared.Checked = true;
            this.rdbRegShared.Location = new System.Drawing.Point(6, 52);
            this.rdbRegShared.Name = "rdbRegShared";
            this.rdbRegShared.Size = new System.Drawing.Size(59, 17);
            this.rdbRegShared.TabIndex = 19;
            this.rdbRegShared.TabStop = true;
            this.rdbRegShared.Text = "Shared";
            this.rdbRegShared.UseVisualStyleBackColor = true;
            this.rdbRegShared.CheckedChanged += new System.EventHandler(this.rdbRegShared_CheckedChanged);
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Location = new System.Drawing.Point(154, 28);
            this.ntxRegPropertyID.Name = "ntxRegPropertyID";
            this.ntxRegPropertyID.Size = new System.Drawing.Size(64, 20);
            this.ntxRegPropertyID.TabIndex = 16;
            this.ntxRegPropertyID.Text = "0";
            // 
            // tpSelection
            // 
            this.tpSelection.Controls.Add(this.rdbSelPrivate);
            this.tpSelection.Controls.Add(this.rdbSelShared);
            this.tpSelection.Controls.Add(this.label1);
            this.tpSelection.Controls.Add(this.ntxSelPropertyID);
            this.tpSelection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tpSelection.Location = new System.Drawing.Point(4, 22);
            this.tpSelection.Name = "tpSelection";
            this.tpSelection.Padding = new System.Windows.Forms.Padding(3);
            this.tpSelection.Size = new System.Drawing.Size(386, 85);
            this.tpSelection.TabIndex = 1;
            this.tpSelection.Text = "Selection";
            this.tpSelection.UseVisualStyleBackColor = true;
            // 
            // rdbSelPrivate
            // 
            this.rdbSelPrivate.AutoSize = true;
            this.rdbSelPrivate.Location = new System.Drawing.Point(6, 29);
            this.rdbSelPrivate.Name = "rdbSelPrivate";
            this.rdbSelPrivate.Size = new System.Drawing.Size(58, 17);
            this.rdbSelPrivate.TabIndex = 26;
            this.rdbSelPrivate.Text = "Private";
            this.rdbSelPrivate.UseVisualStyleBackColor = true;
            this.rdbSelPrivate.CheckedChanged += new System.EventHandler(this.rdbSelPrivate_CheckedChanged);
            // 
            // rdbSelShared
            // 
            this.rdbSelShared.AutoSize = true;
            this.rdbSelShared.Location = new System.Drawing.Point(6, 52);
            this.rdbSelShared.Name = "rdbSelShared";
            this.rdbSelShared.Size = new System.Drawing.Size(59, 17);
            this.rdbSelShared.TabIndex = 25;
            this.rdbSelShared.Text = "Shared";
            this.rdbSelShared.UseVisualStyleBackColor = true;
            this.rdbSelShared.CheckedChanged += new System.EventHandler(this.rdbSelShared_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Property ID:";
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Location = new System.Drawing.Point(154, 28);
            this.ntxSelPropertyID.Name = "ntxSelPropertyID";
            this.ntxSelPropertyID.Size = new System.Drawing.Size(64, 20);
            this.ntxSelPropertyID.TabIndex = 22;
            this.ntxSelPropertyID.Text = "0";
            // 
            // CtrlPropertyBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CtrlPropertyBase";
            this.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpSelection.ResumeLayout(false);
            this.tpSelection.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private NumericTextBox ntxRegPropertyID;
        protected System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdbRegPrivate;
        private System.Windows.Forms.RadioButton rdbRegShared;
        private System.Windows.Forms.RadioButton rdbSelPrivate;
        public System.Windows.Forms.RadioButton rdbSelShared;
        private System.Windows.Forms.Label label1;
        private NumericTextBox ntxSelPropertyID;
        public System.Windows.Forms.CheckBox chxSelectionProperty;
        public System.Windows.Forms.TabControl tcProperty;
        protected System.Windows.Forms.TabPage tpRegular;
        protected System.Windows.Forms.TabPage tpSelection;
    }
}
