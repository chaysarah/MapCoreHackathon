namespace MCTester.Controls
{
    partial class CtrlRawRasterComponentParams
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlRawRasterComponentParams));
            this.lstComponentParams = new System.Windows.Forms.ListBox();
            this.mnuComponentItems = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbComponentType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbWorldLimit = new System.Windows.Forms.GroupBox();
            this.ctrl2DMaxWorldBoundingBox = new MCTester.Controls.Ctrl2DVector();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrl2DMinWorldBoundingBox = new MCTester.Controls.Ctrl2DVector();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddComponentParams = new System.Windows.Forms.Button();
            this.browseLayerCtrl = new MCTester.Controls.CtrlBrowseControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mnuComponentItems.SuspendLayout();
            this.gbWorldLimit.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstComponentParams
            // 
            this.lstComponentParams.ContextMenuStrip = this.mnuComponentItems;
            this.lstComponentParams.FormattingEnabled = true;
            this.lstComponentParams.HorizontalScrollbar = true;
            this.lstComponentParams.Location = new System.Drawing.Point(424, 12);
            this.lstComponentParams.Name = "lstComponentParams";
            this.lstComponentParams.Size = new System.Drawing.Size(238, 121);
            this.lstComponentParams.TabIndex = 62;
            // 
            // mnuComponentItems
            // 
            this.mnuComponentItems.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuComponentItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItemRemove});
            this.mnuComponentItems.Name = "contextMenuStrip1";
            this.mnuComponentItems.Size = new System.Drawing.Size(118, 26);
            this.mnuComponentItems.Opening += new System.ComponentModel.CancelEventHandler(this.mnuComponentItems_Opening);
            // 
            // mnuItemRemove
            // 
            this.mnuItemRemove.Name = "mnuItemRemove";
            this.mnuItemRemove.Size = new System.Drawing.Size(117, 22);
            this.mnuItemRemove.Text = "&Remove";
            this.mnuItemRemove.Click += new System.EventHandler(this.mnuItemRemove_Click);
            // 
            // cmbComponentType
            // 
            this.cmbComponentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComponentType.FormattingEnabled = true;
            this.cmbComponentType.Location = new System.Drawing.Point(117, 15);
            this.cmbComponentType.Name = "cmbComponentType";
            this.cmbComponentType.Size = new System.Drawing.Size(183, 21);
            this.cmbComponentType.TabIndex = 56;
            this.cmbComponentType.SelectedIndexChanged += new System.EventHandler(this.cmbComponentType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 57;
            this.label2.Text = "Component Type:";
            // 
            // gbWorldLimit
            // 
            this.gbWorldLimit.Controls.Add(this.ctrl2DMaxWorldBoundingBox);
            this.gbWorldLimit.Controls.Add(this.label6);
            this.gbWorldLimit.Controls.Add(this.ctrl2DMinWorldBoundingBox);
            this.gbWorldLimit.Controls.Add(this.label7);
            this.gbWorldLimit.Location = new System.Drawing.Point(0, 71);
            this.gbWorldLimit.Name = "gbWorldLimit";
            this.gbWorldLimit.Size = new System.Drawing.Size(230, 88);
            this.gbWorldLimit.TabIndex = 61;
            this.gbWorldLimit.TabStop = false;
            this.gbWorldLimit.Text = "World Limit:";
            // 
            // ctrl2DMaxWorldBoundingBox
            // 
            this.ctrl2DMaxWorldBoundingBox.Location = new System.Drawing.Point(62, 48);
            this.ctrl2DMaxWorldBoundingBox.Name = "ctrl2DMaxWorldBoundingBox";
            this.ctrl2DMaxWorldBoundingBox.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DMaxWorldBoundingBox.TabIndex = 66;
            this.ctrl2DMaxWorldBoundingBox.X = 0D;
            this.ctrl2DMaxWorldBoundingBox.Y = 0D;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Min Point:";
            // 
            // ctrl2DMinWorldBoundingBox
            // 
            this.ctrl2DMinWorldBoundingBox.Location = new System.Drawing.Point(62, 16);
            this.ctrl2DMinWorldBoundingBox.Name = "ctrl2DMinWorldBoundingBox";
            this.ctrl2DMinWorldBoundingBox.Size = new System.Drawing.Size(154, 26);
            this.ctrl2DMinWorldBoundingBox.TabIndex = 65;
            this.ctrl2DMinWorldBoundingBox.X = 0D;
            this.ctrl2DMinWorldBoundingBox.Y = 0D;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Max Point:";
            // 
            // btnAddComponentParams
            // 
            this.btnAddComponentParams.Location = new System.Drawing.Point(344, 136);
            this.btnAddComponentParams.Name = "btnAddComponentParams";
            this.btnAddComponentParams.Size = new System.Drawing.Size(74, 23);
            this.btnAddComponentParams.TabIndex = 59;
            this.btnAddComponentParams.Text = "Add To List";
            this.btnAddComponentParams.UseVisualStyleBackColor = true;
            this.btnAddComponentParams.Click += new System.EventHandler(this.btnAddComponentParams_Click);
            // 
            // browseLayerCtrl
            // 
            this.browseLayerCtrl.AutoSize = true;
            this.browseLayerCtrl.FileName = "";
            this.browseLayerCtrl.Filter = "";
            this.browseLayerCtrl.IsFolderDialog = true;
            this.browseLayerCtrl.IsFullPath = true;
            this.browseLayerCtrl.IsSaveFile = false;
            this.browseLayerCtrl.LabelCaption = "Field/Directory Name:";
            this.browseLayerCtrl.Location = new System.Drawing.Point(113, 43);
            this.browseLayerCtrl.Margin = new System.Windows.Forms.Padding(4);
            this.browseLayerCtrl.MinimumSize = new System.Drawing.Size(300, 24);
            this.browseLayerCtrl.MultiFilesSelect = false;
            this.browseLayerCtrl.Name = "browseLayerCtrl";
            this.browseLayerCtrl.Prefix = "";
            this.browseLayerCtrl.Size = new System.Drawing.Size(300, 24);
            this.browseLayerCtrl.TabIndex = 63;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.lstComponentParams);
            this.groupBox1.Controls.Add(this.browseLayerCtrl);
            this.groupBox1.Controls.Add(this.btnAddComponentParams);
            this.groupBox1.Controls.Add(this.gbWorldLimit);
            this.groupBox1.Controls.Add(this.cmbComponentType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(668, 163);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add Components";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(423, 136);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(58, 23);
            this.btnClear.TabIndex = 64;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Field/Directory Name:";
            // 
            // CtrlRawRasterComponentParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "CtrlRawRasterComponentParams";
            this.Size = new System.Drawing.Size(678, 169);
            this.mnuComponentItems.ResumeLayout(false);
            this.gbWorldLimit.ResumeLayout(false);
            this.gbWorldLimit.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstComponentParams;
        private System.Windows.Forms.ComboBox cmbComponentType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbWorldLimit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddComponentParams;
        private CtrlBrowseControl browseLayerCtrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ContextMenuStrip mnuComponentItems;
        private System.Windows.Forms.ToolStripMenuItem mnuItemRemove;
        private Ctrl2DVector ctrl2DMinWorldBoundingBox;
        private Ctrl2DVector ctrl2DMaxWorldBoundingBox;
        private System.Windows.Forms.Label label1;
    }
}
