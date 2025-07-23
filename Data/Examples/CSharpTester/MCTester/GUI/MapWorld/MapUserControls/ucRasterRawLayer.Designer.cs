namespace MCTester.MapWorld.MapUserControls
{
    partial class ucRasterRawLayer
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbBChannel = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbGChannel = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbRChannel = new System.Windows.Forms.ComboBox();
            this.ntbRChannelIndex = new MCTester.Controls.NumericTextBox();
            this.btnColorChannelsSet = new System.Windows.Forms.Button();
            this.ntbBChannelIndex = new MCTester.Controls.NumericTextBox();
            this.ntbGChannelIndex = new MCTester.Controls.NumericTextBox();
            this.btnAddComponents = new System.Windows.Forms.Button();
            this.ctrlRawRasterComponentParams1 = new MCTester.Controls.CtrlRawRasterComponentParams();
            this.ctrlRawComponents1 = new MCTester.MapWorld.MapUserControls.CtrlRawComponents();
            this.tcLayer.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcLayer
            // 
            this.tcLayer.Controls.Add(this.tabPage2);
            this.tcLayer.Controls.SetChildIndex(this.tabPage2, 0);
            this.tcLayer.Controls.SetChildIndex(this.tpGeneral, 0);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Size = new System.Drawing.Size(737, 592);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ctrlRawComponents1);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.btnAddComponents);
            this.tabPage2.Controls.Add(this.ctrlRawRasterComponentParams1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(737, 592);
            this.tabPage2.TabIndex = 5;
            this.tabPage2.Text = "Raw Raster";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.cmbBChannel);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cmbGChannel);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbRChannel);
            this.groupBox2.Controls.Add(this.ntbRChannelIndex);
            this.groupBox2.Controls.Add(this.btnColorChannelsSet);
            this.groupBox2.Controls.Add(this.ntbBChannelIndex);
            this.groupBox2.Controls.Add(this.ntbGChannelIndex);
            this.groupBox2.Location = new System.Drawing.Point(6, 342);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 158);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color Channels";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "B:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(226, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Index:";
            // 
            // cmbBChannel
            // 
            this.cmbBChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBChannel.FormattingEnabled = true;
            this.cmbBChannel.Location = new System.Drawing.Point(34, 89);
            this.cmbBChannel.Name = "cmbBChannel";
            this.cmbBChannel.Size = new System.Drawing.Size(170, 21);
            this.cmbBChannel.TabIndex = 21;
            this.cmbBChannel.SelectedIndexChanged += new System.EventHandler(this.cmbBChannel_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "G:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(226, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Index:";
            // 
            // cmbGChannel
            // 
            this.cmbGChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGChannel.FormattingEnabled = true;
            this.cmbGChannel.Location = new System.Drawing.Point(34, 62);
            this.cmbGChannel.Name = "cmbGChannel";
            this.cmbGChannel.Size = new System.Drawing.Size(170, 21);
            this.cmbGChannel.TabIndex = 18;
            this.cmbGChannel.SelectedIndexChanged += new System.EventHandler(this.cmbGChannel_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "R:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(226, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Index:";
            // 
            // cmbRChannel
            // 
            this.cmbRChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRChannel.FormattingEnabled = true;
            this.cmbRChannel.Location = new System.Drawing.Point(34, 35);
            this.cmbRChannel.Name = "cmbRChannel";
            this.cmbRChannel.Size = new System.Drawing.Size(170, 21);
            this.cmbRChannel.TabIndex = 14;
            this.cmbRChannel.SelectedIndexChanged += new System.EventHandler(this.cmbRChannel_SelectedIndexChanged);
            // 
            // ntbRChannelIndex
            // 
            this.ntbRChannelIndex.Location = new System.Drawing.Point(268, 35);
            this.ntbRChannelIndex.Name = "ntbRChannelIndex";
            this.ntbRChannelIndex.Size = new System.Drawing.Size(100, 20);
            this.ntbRChannelIndex.TabIndex = 0;
            // 
            // btnColorChannelsSet
            // 
            this.btnColorChannelsSet.Location = new System.Drawing.Point(293, 125);
            this.btnColorChannelsSet.Name = "btnColorChannelsSet";
            this.btnColorChannelsSet.Size = new System.Drawing.Size(75, 23);
            this.btnColorChannelsSet.TabIndex = 6;
            this.btnColorChannelsSet.Text = "Set";
            this.btnColorChannelsSet.UseVisualStyleBackColor = true;
            this.btnColorChannelsSet.Click += new System.EventHandler(this.btnColorChannelsSet_Click);
            // 
            // ntbBChannelIndex
            // 
            this.ntbBChannelIndex.Location = new System.Drawing.Point(268, 90);
            this.ntbBChannelIndex.Name = "ntbBChannelIndex";
            this.ntbBChannelIndex.Size = new System.Drawing.Size(100, 20);
            this.ntbBChannelIndex.TabIndex = 5;
            // 
            // ntbGChannelIndex
            // 
            this.ntbGChannelIndex.Location = new System.Drawing.Point(268, 63);
            this.ntbGChannelIndex.Name = "ntbGChannelIndex";
            this.ntbGChannelIndex.Size = new System.Drawing.Size(100, 20);
            this.ntbGChannelIndex.TabIndex = 3;
            // 
            // btnAddComponents
            // 
            this.btnAddComponents.Location = new System.Drawing.Point(567, 144);
            this.btnAddComponents.Name = "btnAddComponents";
            this.btnAddComponents.Size = new System.Drawing.Size(104, 23);
            this.btnAddComponents.TabIndex = 1;
            this.btnAddComponents.Text = "Add Components";
            this.btnAddComponents.UseVisualStyleBackColor = true;
            this.btnAddComponents.Click += new System.EventHandler(this.btnAddComponents_Click);
            // 
            // ctrlRawRasterComponentParams1
            // 
            this.ctrlRawRasterComponentParams1.Location = new System.Drawing.Point(6, 6);
            this.ctrlRawRasterComponentParams1.Name = "ctrlRawRasterComponentParams1";
            this.ctrlRawRasterComponentParams1.Size = new System.Drawing.Size(687, 176);
            this.ctrlRawRasterComponentParams1.TabIndex = 0;
            // 
            // ctrlRawComponents1
            // 
            this.ctrlRawComponents1.Location = new System.Drawing.Point(6, 173);
            this.ctrlRawComponents1.Name = "ctrlRawComponents1";
            this.ctrlRawComponents1.Size = new System.Drawing.Size(683, 153);
            this.ctrlRawComponents1.TabIndex = 4;
            // 
            // ucRasterRawLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucRasterRawLayer";
            this.tcLayer.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage2;
        private Controls.CtrlRawRasterComponentParams ctrlRawRasterComponentParams1;
        private System.Windows.Forms.Button btnAddComponents;
        private System.Windows.Forms.GroupBox groupBox2;
        private Controls.NumericTextBox ntbBChannelIndex;
        private Controls.NumericTextBox ntbGChannelIndex;
        private Controls.NumericTextBox ntbRChannelIndex;
        private System.Windows.Forms.Button btnColorChannelsSet;
        private System.Windows.Forms.ComboBox cmbRChannel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbBChannel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbGChannel;
        private CtrlRawComponents ctrlRawComponents1;
    }
}
