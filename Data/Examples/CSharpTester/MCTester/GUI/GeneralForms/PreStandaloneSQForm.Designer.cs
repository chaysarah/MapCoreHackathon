namespace MCTester.GUI.Forms
{
    partial class PreStandaloneSQForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripTerrainList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNewTerrain = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateStandaloneSQ = new System.Windows.Forms.Button();
            this.lstTerrains = new System.Windows.Forms.ListBox();
            this.btnCreateNewTerrain = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ctrlLayers1 = new MCTester.MapWorld.WizardForms.CtrlLayers();
            this.ucCreateData = new MCTester.Controls.CreateDataSQControl();
            this.contextMenuStripTerrainList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ContextMenuStrip = this.contextMenuStripTerrainList;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(-1, 271);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Terrain List (MultiSelect):";
            // 
            // contextMenuStripTerrainList
            // 
            this.contextMenuStripTerrainList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripTerrainList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewTerrain});
            this.contextMenuStripTerrainList.Name = "contextMenuStripTerrainList";
            this.contextMenuStripTerrainList.Size = new System.Drawing.Size(137, 26);
            this.contextMenuStripTerrainList.Text = "Create New Terrain";
            this.contextMenuStripTerrainList.Click += new System.EventHandler(this.contextMenuStripTerrainList_Click);
            // 
            // miNewTerrain
            // 
            this.miNewTerrain.Name = "miNewTerrain";
            this.miNewTerrain.Size = new System.Drawing.Size(136, 22);
            this.miNewTerrain.Text = "New Terrain";
            // 
            // btnCreateStandaloneSQ
            // 
            this.btnCreateStandaloneSQ.Location = new System.Drawing.Point(286, 612);
            this.btnCreateStandaloneSQ.Name = "btnCreateStandaloneSQ";
            this.btnCreateStandaloneSQ.Size = new System.Drawing.Size(82, 23);
            this.btnCreateStandaloneSQ.TabIndex = 4;
            this.btnCreateStandaloneSQ.Text = "Create";
            this.btnCreateStandaloneSQ.UseVisualStyleBackColor = true;
            this.btnCreateStandaloneSQ.Click += new System.EventHandler(this.btnCreateStandaloneSQ_Click);
            // 
            // lstTerrains
            // 
            this.lstTerrains.FormattingEnabled = true;
            this.lstTerrains.Location = new System.Drawing.Point(-1, 288);
            this.lstTerrains.Name = "lstTerrains";
            this.lstTerrains.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstTerrains.Size = new System.Drawing.Size(369, 95);
            this.lstTerrains.TabIndex = 6;
            // 
            // btnCreateNewTerrain
            // 
            this.btnCreateNewTerrain.Location = new System.Drawing.Point(1, 387);
            this.btnCreateNewTerrain.Name = "btnCreateNewTerrain";
            this.btnCreateNewTerrain.Size = new System.Drawing.Size(130, 23);
            this.btnCreateNewTerrain.TabIndex = 7;
            this.btnCreateNewTerrain.Text = "Create New Terrain";
            this.btnCreateNewTerrain.UseVisualStyleBackColor = true;
            this.btnCreateNewTerrain.Click += new System.EventHandler(this.btnCreateNewTerrain_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctrlLayers1);
            this.groupBox1.Location = new System.Drawing.Point(2, 416);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(285, 190);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Query Secondary Dtm Layers";
            // 
            // ctrlLayers1
            // 
            this.ctrlLayers1.Location = new System.Drawing.Point(6, 19);
            this.ctrlLayers1.Name = "ctrlLayers1";
            this.ctrlLayers1.Size = new System.Drawing.Size(278, 162);
            this.ctrlLayers1.TabIndex = 0;
            // 
            // ucCreateData
            // 
            this.ucCreateData.AutoScroll = true;
            this.ucCreateData.Location = new System.Drawing.Point(0, 0);
            this.ucCreateData.Margin = new System.Windows.Forms.Padding(4);
            this.ucCreateData.Name = "ucCreateData";
            this.ucCreateData.Size = new System.Drawing.Size(368, 267);
            this.ucCreateData.TabIndex = 1;
            // 
            // PreStandaloneSQForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(373, 641);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCreateNewTerrain);
            this.Controls.Add(this.ucCreateData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreateStandaloneSQ);
            this.Controls.Add(this.lstTerrains);
            this.Name = "PreStandaloneSQForm";
            this.Text = "PreStandaloneSQForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PreStandaloneSQForm_FormClosed);
            this.contextMenuStripTerrainList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateStandaloneSQ;
        private MCTester.Controls.CreateDataSQControl ucCreateData;
        private System.Windows.Forms.ListBox lstTerrains;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTerrainList;
        private System.Windows.Forms.ToolStripMenuItem miNewTerrain;
        private System.Windows.Forms.Button btnCreateNewTerrain;
        private System.Windows.Forms.GroupBox groupBox1;
        private MapWorld.WizardForms.CtrlLayers ctrlLayers1;
    }
}