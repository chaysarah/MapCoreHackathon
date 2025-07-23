namespace MCTester.Controls
{
    partial class CtrlObjStatePropertyBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlObjStatePropertyBase));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tcProperty = new System.Windows.Forms.TabControl();
            this.tpRegular = new System.Windows.Forms.TabPage();
            this.llWarningID = new System.Windows.Forms.LinkLabel();
            this.lblErrorValidation = new System.Windows.Forms.Label();
            this.btnValidation2 = new System.Windows.Forms.Button();
            this.btnValidation1 = new System.Windows.Forms.Button();
            this.btnRemoveTab = new System.Windows.Forms.Button();
            this.btnAddTab = new System.Windows.Forms.Button();
            this.rdbRegPrivate = new System.Windows.Forms.RadioButton();
            this.rdbRegShared = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.tpObjectState = new System.Windows.Forms.TabPage();
            this.lblSelErrorValidation = new System.Windows.Forms.Label();
            this.btnSelValidation2 = new System.Windows.Forms.Button();
            this.btnSelValidation1 = new System.Windows.Forms.Button();
            this.addObjectState = new System.Windows.Forms.GroupBox();
            this.cbNewTabNum = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCurrentObjectState = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelRemove = new System.Windows.Forms.Button();
            this.btnSelAdd = new System.Windows.Forms.Button();
            this.rdbSelPrivate = new System.Windows.Forms.RadioButton();
            this.rdbSelShared = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ttValidationValue1 = new System.Windows.Forms.ToolTip(this.components);
            this.ttValidationValue2 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.ntxRegPropertyID = new MCTester.Controls.NumericTextBox();
            this.ntxSelPropertyID = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tcProperty.SuspendLayout();
            this.tpRegular.SuspendLayout();
            this.tpObjectState.SuspendLayout();
            this.addObjectState.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tcProperty);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Property Name";
            // 
            // tcProperty
            // 
            this.tcProperty.Controls.Add(this.tpRegular);
            this.tcProperty.Controls.Add(this.tpObjectState);
            this.tcProperty.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tcProperty.Location = new System.Drawing.Point(3, 16);
            this.tcProperty.Name = "tcProperty";
            this.tcProperty.SelectedIndex = 0;
            this.tcProperty.Size = new System.Drawing.Size(394, 111);
            this.tcProperty.TabIndex = 18;
            // 
            // tpRegular
            // 
            this.tpRegular.Controls.Add(this.label4);
            this.tpRegular.Controls.Add(this.llWarningID);
            this.tpRegular.Controls.Add(this.lblErrorValidation);
            this.tpRegular.Controls.Add(this.btnValidation2);
            this.tpRegular.Controls.Add(this.btnValidation1);
            this.tpRegular.Controls.Add(this.btnRemoveTab);
            this.tpRegular.Controls.Add(this.btnAddTab);
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
            // llWarningID
            // 
            this.llWarningID.ActiveLinkColor = System.Drawing.Color.Blue;
            this.llWarningID.AutoSize = true;
            this.llWarningID.Location = new System.Drawing.Point(5, 72);
            this.llWarningID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.llWarningID.Name = "llWarningID";
            this.llWarningID.Size = new System.Drawing.Size(0, 13);
            this.llWarningID.TabIndex = 41;
            this.llWarningID.Visible = false;
            this.llWarningID.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWarningID_LinkClicked);
            // 
            // lblErrorValidation
            // 
            this.lblErrorValidation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblErrorValidation.ForeColor = System.Drawing.Color.Red;
            this.lblErrorValidation.Location = new System.Drawing.Point(3, 69);
            this.lblErrorValidation.Margin = new System.Windows.Forms.Padding(0);
            this.lblErrorValidation.Name = "lblErrorValidation";
            this.lblErrorValidation.Size = new System.Drawing.Size(380, 13);
            this.lblErrorValidation.TabIndex = 25;
            this.lblErrorValidation.Visible = false;
            // 
            // btnValidation2
            // 
            this.btnValidation2.CausesValidation = false;
            this.btnValidation2.ForeColor = System.Drawing.Color.Red;
            this.btnValidation2.Location = new System.Drawing.Point(324, 24);
            this.btnValidation2.Margin = new System.Windows.Forms.Padding(2);
            this.btnValidation2.Name = "btnValidation2";
            this.btnValidation2.Size = new System.Drawing.Size(60, 22);
            this.btnValidation2.TabIndex = 24;
            this.btnValidation2.Text = "Use new";
            this.btnValidation2.UseVisualStyleBackColor = true;
            this.btnValidation2.Visible = false;
            this.btnValidation2.Click += new System.EventHandler(this.btnValidation2_Click);
            // 
            // btnValidation1
            // 
            this.btnValidation1.CausesValidation = false;
            this.btnValidation1.ForeColor = System.Drawing.Color.Red;
            this.btnValidation1.Location = new System.Drawing.Point(263, 24);
            this.btnValidation1.Margin = new System.Windows.Forms.Padding(2);
            this.btnValidation1.Name = "btnValidation1";
            this.btnValidation1.Size = new System.Drawing.Size(60, 22);
            this.btnValidation1.TabIndex = 23;
            this.btnValidation1.Text = "Keep old";
            this.btnValidation1.UseVisualStyleBackColor = true;
            this.btnValidation1.Visible = false;
            this.btnValidation1.Click += new System.EventHandler(this.btnValidation1_Click);
            // 
            // btnRemoveTab
            // 
            this.btnRemoveTab.Enabled = false;
            this.btnRemoveTab.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveTab.Image")));
            this.btnRemoveTab.Location = new System.Drawing.Point(354, 5);
            this.btnRemoveTab.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemoveTab.Name = "btnRemoveTab";
            this.btnRemoveTab.Size = new System.Drawing.Size(18, 20);
            this.btnRemoveTab.TabIndex = 22;
            this.toolTip1.SetToolTip(this.btnRemoveTab, "Remove current object state");
            this.btnRemoveTab.UseCompatibleTextRendering = true;
            this.btnRemoveTab.UseVisualStyleBackColor = true;
            this.btnRemoveTab.Visible = false;
            this.btnRemoveTab.Click += new System.EventHandler(this.btnRemoveTab_Click);
            // 
            // btnAddTab
            // 
            this.btnAddTab.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTab.Image")));
            this.btnAddTab.Location = new System.Drawing.Point(332, 5);
            this.btnAddTab.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddTab.Name = "btnAddTab";
            this.btnAddTab.Size = new System.Drawing.Size(18, 20);
            this.btnAddTab.TabIndex = 21;
            this.btnAddTab.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnAddTab, "Add object state");
            this.btnAddTab.UseVisualStyleBackColor = true;
            this.btnAddTab.Visible = false;
            this.btnAddTab.Click += new System.EventHandler(this.btnAddTab_Click);
            // 
            // rdbRegPrivate
            // 
            this.rdbRegPrivate.AutoSize = true;
            this.rdbRegPrivate.Location = new System.Drawing.Point(138, 27);
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
            this.rdbRegShared.CausesValidation = false;
            this.rdbRegShared.Checked = true;
            this.rdbRegShared.Location = new System.Drawing.Point(76, 27);
            this.rdbRegShared.Name = "rdbRegShared";
            this.rdbRegShared.Size = new System.Drawing.Size(59, 17);
            this.rdbRegShared.TabIndex = 19;
            this.rdbRegShared.TabStop = true;
            this.rdbRegShared.Text = "Shared";
            this.rdbRegShared.UseVisualStyleBackColor = true;
            this.rdbRegShared.CheckedChanged += new System.EventHandler(this.rdbRegShared_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Property ID:";
            // 
            // tpObjectState
            // 
            this.tpObjectState.Controls.Add(this.label6);
            this.tpObjectState.Controls.Add(this.lblSelErrorValidation);
            this.tpObjectState.Controls.Add(this.btnSelValidation2);
            this.tpObjectState.Controls.Add(this.btnSelValidation1);
            this.tpObjectState.Controls.Add(this.addObjectState);
            this.tpObjectState.Controls.Add(this.cmbCurrentObjectState);
            this.tpObjectState.Controls.Add(this.label3);
            this.tpObjectState.Controls.Add(this.btnSelRemove);
            this.tpObjectState.Controls.Add(this.btnSelAdd);
            this.tpObjectState.Controls.Add(this.rdbSelPrivate);
            this.tpObjectState.Controls.Add(this.rdbSelShared);
            this.tpObjectState.Controls.Add(this.label2);
            this.tpObjectState.Controls.Add(this.ntxSelPropertyID);
            this.tpObjectState.Location = new System.Drawing.Point(4, 22);
            this.tpObjectState.Margin = new System.Windows.Forms.Padding(2);
            this.tpObjectState.Name = "tpObjectState";
            this.tpObjectState.Padding = new System.Windows.Forms.Padding(2);
            this.tpObjectState.Size = new System.Drawing.Size(386, 85);
            this.tpObjectState.TabIndex = 1;
            this.tpObjectState.Text = "Special states (0)";
            this.tpObjectState.UseVisualStyleBackColor = true;
            // 
            // lblSelErrorValidation
            // 
            this.lblSelErrorValidation.AutoSize = true;
            this.lblSelErrorValidation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSelErrorValidation.ForeColor = System.Drawing.Color.Red;
            this.lblSelErrorValidation.Location = new System.Drawing.Point(2, 70);
            this.lblSelErrorValidation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelErrorValidation.Name = "lblSelErrorValidation";
            this.lblSelErrorValidation.Size = new System.Drawing.Size(0, 13);
            this.lblSelErrorValidation.TabIndex = 36;
            this.lblSelErrorValidation.Visible = false;
            // 
            // btnSelValidation2
            // 
            this.btnSelValidation2.CausesValidation = false;
            this.btnSelValidation2.ForeColor = System.Drawing.Color.Red;
            this.btnSelValidation2.Location = new System.Drawing.Point(324, 24);
            this.btnSelValidation2.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelValidation2.Name = "btnSelValidation2";
            this.btnSelValidation2.Size = new System.Drawing.Size(60, 22);
            this.btnSelValidation2.TabIndex = 35;
            this.btnSelValidation2.Text = "Use new value";
            this.btnSelValidation2.UseVisualStyleBackColor = true;
            this.btnSelValidation2.Visible = false;
            this.btnSelValidation2.Click += new System.EventHandler(this.btnSelValidation2_Click);
            // 
            // btnSelValidation1
            // 
            this.btnSelValidation1.CausesValidation = false;
            this.btnSelValidation1.ForeColor = System.Drawing.Color.Red;
            this.btnSelValidation1.Location = new System.Drawing.Point(263, 24);
            this.btnSelValidation1.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelValidation1.Name = "btnSelValidation1";
            this.btnSelValidation1.Size = new System.Drawing.Size(60, 22);
            this.btnSelValidation1.TabIndex = 34;
            this.btnSelValidation1.Text = "Keep old";
            this.btnSelValidation1.UseVisualStyleBackColor = true;
            this.btnSelValidation1.Visible = false;
            this.btnSelValidation1.Click += new System.EventHandler(this.btnSelValidation1_Click);
            // 
            // addObjectState
            // 
            this.addObjectState.Controls.Add(this.cbNewTabNum);
            this.addObjectState.Controls.Add(this.btnCancel);
            this.addObjectState.Controls.Add(this.btnAdd);
            this.addObjectState.Controls.Add(this.label1);
            this.addObjectState.Location = new System.Drawing.Point(2, 2);
            this.addObjectState.Margin = new System.Windows.Forms.Padding(2);
            this.addObjectState.Name = "addObjectState";
            this.addObjectState.Padding = new System.Windows.Forms.Padding(2);
            this.addObjectState.Size = new System.Drawing.Size(379, 81);
            this.addObjectState.TabIndex = 25;
            this.addObjectState.TabStop = false;
            this.addObjectState.Text = "Add Object State";
            this.addObjectState.Visible = false;
            this.addObjectState.VisibleChanged += new System.EventHandler(this.addObjectState_VisibleChanged);
            // 
            // cbNewTabNum
            // 
            this.cbNewTabNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNewTabNum.FormattingEnabled = true;
            this.cbNewTabNum.Location = new System.Drawing.Point(114, 24);
            this.cbNewTabNum.Name = "cbNewTabNum";
            this.cbNewTabNum.Size = new System.Drawing.Size(185, 21);
            this.cbNewTabNum.TabIndex = 27;
            this.cbNewTabNum.TextChanged += new System.EventHandler(this.cbNewTabNum_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(215, 51);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 24);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(114, 51);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(87, 24);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 26);
            this.label1.TabIndex = 24;
            this.label1.Text = "Select Object State:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbCurrentObjectState
            // 
            this.cmbCurrentObjectState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrentObjectState.FormattingEnabled = true;
            this.cmbCurrentObjectState.Location = new System.Drawing.Point(140, 3);
            this.cmbCurrentObjectState.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCurrentObjectState.Name = "cmbCurrentObjectState";
            this.cmbCurrentObjectState.Size = new System.Drawing.Size(161, 21);
            this.cmbCurrentObjectState.TabIndex = 33;
            this.cmbCurrentObjectState.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentObjectState_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Current State:";
            // 
            // btnSelRemove
            // 
            this.btnSelRemove.CausesValidation = false;
            this.btnSelRemove.Enabled = false;
            this.btnSelRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnSelRemove.Image")));
            this.btnSelRemove.Location = new System.Drawing.Point(31, 5);
            this.btnSelRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelRemove.Name = "btnSelRemove";
            this.btnSelRemove.Size = new System.Drawing.Size(20, 20);
            this.btnSelRemove.TabIndex = 31;
            this.toolTip1.SetToolTip(this.btnSelRemove, "Remove current object state");
            this.btnSelRemove.UseCompatibleTextRendering = true;
            this.btnSelRemove.UseVisualStyleBackColor = true;
            this.btnSelRemove.Click += new System.EventHandler(this.btnRemoveTab_Click);
            // 
            // btnSelAdd
            // 
            this.btnSelAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnSelAdd.Image")));
            this.btnSelAdd.Location = new System.Drawing.Point(6, 5);
            this.btnSelAdd.Margin = new System.Windows.Forms.Padding(0);
            this.btnSelAdd.Name = "btnSelAdd";
            this.btnSelAdd.Size = new System.Drawing.Size(20, 20);
            this.btnSelAdd.TabIndex = 30;
            this.btnSelAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnSelAdd, "Add object state");
            this.btnSelAdd.UseVisualStyleBackColor = true;
            this.btnSelAdd.Click += new System.EventHandler(this.btnAddTab_Click);
            // 
            // rdbSelPrivate
            // 
            this.rdbSelPrivate.AutoSize = true;
            this.rdbSelPrivate.Location = new System.Drawing.Point(138, 27);
            this.rdbSelPrivate.Name = "rdbSelPrivate";
            this.rdbSelPrivate.Size = new System.Drawing.Size(58, 17);
            this.rdbSelPrivate.TabIndex = 29;
            this.rdbSelPrivate.Text = "Private";
            this.rdbSelPrivate.UseVisualStyleBackColor = true;
            this.rdbSelPrivate.CheckedChanged += new System.EventHandler(this.rdbSelPrivate_CheckedChanged);
            // 
            // rdbSelShared
            // 
            this.rdbSelShared.AutoSize = true;
            this.rdbSelShared.CausesValidation = false;
            this.rdbSelShared.Checked = true;
            this.rdbSelShared.Location = new System.Drawing.Point(76, 27);
            this.rdbSelShared.Name = "rdbSelShared";
            this.rdbSelShared.Size = new System.Drawing.Size(59, 17);
            this.rdbSelShared.TabIndex = 28;
            this.rdbSelShared.TabStop = true;
            this.rdbSelShared.Text = "Shared";
            this.rdbSelShared.UseVisualStyleBackColor = true;
            this.rdbSelShared.CheckedChanged += new System.EventHandler(this.rdbSelShared_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Property ID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Property Value:";
            // 
            // ntxRegPropertyID
            // 
            this.ntxRegPropertyID.Enabled = false;
            this.ntxRegPropertyID.Location = new System.Drawing.Point(196, 25);
            this.ntxRegPropertyID.Name = "ntxRegPropertyID";
            this.ntxRegPropertyID.Size = new System.Drawing.Size(64, 20);
            this.ntxRegPropertyID.TabIndex = 16;
            this.ntxRegPropertyID.Text = "0";
            this.ntxRegPropertyID.Enter += new System.EventHandler(this.ntxRegPropertyID_Enter);
            this.ntxRegPropertyID.Validating += new System.ComponentModel.CancelEventHandler(this.ntxRegPropertyID_Validating);
            // 
            // ntxSelPropertyID
            // 
            this.ntxSelPropertyID.Enabled = false;
            this.ntxSelPropertyID.Location = new System.Drawing.Point(196, 25);
            this.ntxSelPropertyID.Name = "ntxSelPropertyID";
            this.ntxSelPropertyID.Size = new System.Drawing.Size(64, 20);
            this.ntxSelPropertyID.TabIndex = 27;
            this.ntxSelPropertyID.Text = "MAX";
            this.ntxSelPropertyID.Enter += new System.EventHandler(this.ntxSelPropertyID_Enter);
            this.ntxSelPropertyID.Validating += new System.ComponentModel.CancelEventHandler(this.ntxSelPropertyID_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Property Value:";
            // 
            // CtrlObjStatePropertyBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CtrlObjStatePropertyBase";
            this.Size = new System.Drawing.Size(400, 130);
            this.groupBox1.ResumeLayout(false);
            this.tcProperty.ResumeLayout(false);
            this.tpRegular.ResumeLayout(false);
            this.tpRegular.PerformLayout();
            this.tpObjectState.ResumeLayout(false);
            this.tpObjectState.PerformLayout();
            this.addObjectState.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TabControl tcProperty;
        protected System.Windows.Forms.TabPage tpRegular;
        protected System.Windows.Forms.RadioButton rdbRegPrivate;
        protected System.Windows.Forms.RadioButton rdbRegShared;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddTab;
        private System.Windows.Forms.Button btnRemoveTab;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox addObjectState;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelRemove;
        private System.Windows.Forms.Button btnSelAdd;
        protected System.Windows.Forms.RadioButton rdbSelPrivate;
        private System.Windows.Forms.RadioButton rdbSelShared;
        public NumericTextBox ntxSelPropertyID;
        private System.Windows.Forms.Label label2;
        protected System.Windows.Forms.TabPage tpObjectState;
        private System.Windows.Forms.ComboBox cmbCurrentObjectState;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbNewTabNum;
        public NumericTextBox ntxRegPropertyID;
        private System.Windows.Forms.Button btnValidation1;
        private System.Windows.Forms.Button btnValidation2;
        private System.Windows.Forms.Label lblErrorValidation;
        private System.Windows.Forms.ToolTip ttValidationValue1;
        private System.Windows.Forms.ToolTip ttValidationValue2;
        private System.Windows.Forms.Label lblSelErrorValidation;
        private System.Windows.Forms.Button btnSelValidation2;
        private System.Windows.Forms.Button btnSelValidation1;
        private System.Windows.Forms.LinkLabel llWarningID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}
