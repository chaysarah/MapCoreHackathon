namespace MCTester.General_Forms
{
    partial class frmVectorial
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
            this.gbMapProductionParams = new System.Windows.Forms.GroupBox();
            this.gbGetVectorLayerMinMaxScale = new System.Windows.Forms.GroupBox();
            this.ctrlBrowseVectorLayer = new MCTester.Controls.CtrlBrowseControl();
            this.btnGetVectorLayerMinMaxScale = new System.Windows.Forms.Button();
            this.ntxVectorLayerMaxScale = new MCTester.Controls.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ntxVectorLayerMinScale = new MCTester.Controls.NumericTextBox();
            this.btnMapProductionRemove = new System.Windows.Forms.Button();
            this.btnMapProductionCreate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnNxtMapProduction = new System.Windows.Forms.Button();
            this.btnPrvMapProduction = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbMapProductionParams.SuspendLayout();
            this.gbGetVectorLayerMinMaxScale.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMapProductionParams
            // 
            this.gbMapProductionParams.Controls.Add(this.gbGetVectorLayerMinMaxScale);
            this.gbMapProductionParams.Location = new System.Drawing.Point(1, 33);
            this.gbMapProductionParams.Name = "gbMapProductionParams";
            this.gbMapProductionParams.Size = new System.Drawing.Size(494, 118);
            this.gbMapProductionParams.TabIndex = 10;
            this.gbMapProductionParams.TabStop = false;
            this.gbMapProductionParams.Text = "Map Production Params";
            // 
            // gbGetVectorLayerMinMaxScale
            // 
            this.gbGetVectorLayerMinMaxScale.Controls.Add(this.label1);
            this.gbGetVectorLayerMinMaxScale.Controls.Add(this.ctrlBrowseVectorLayer);
            this.gbGetVectorLayerMinMaxScale.Controls.Add(this.btnGetVectorLayerMinMaxScale);
            this.gbGetVectorLayerMinMaxScale.Controls.Add(this.ntxVectorLayerMaxScale);
            this.gbGetVectorLayerMinMaxScale.Controls.Add(this.label9);
            this.gbGetVectorLayerMinMaxScale.Controls.Add(this.label10);
            this.gbGetVectorLayerMinMaxScale.Controls.Add(this.ntxVectorLayerMinScale);
            this.gbGetVectorLayerMinMaxScale.Location = new System.Drawing.Point(6, 24);
            this.gbGetVectorLayerMinMaxScale.Name = "gbGetVectorLayerMinMaxScale";
            this.gbGetVectorLayerMinMaxScale.Size = new System.Drawing.Size(479, 82);
            this.gbGetVectorLayerMinMaxScale.TabIndex = 20;
            this.gbGetVectorLayerMinMaxScale.TabStop = false;
            this.gbGetVectorLayerMinMaxScale.Text = "Get Vector Layer Min Max Scale";
            // 
            // ctrlBrowseVectorLayer
            // 
            this.ctrlBrowseVectorLayer.AutoSize = true;
            this.ctrlBrowseVectorLayer.FileName = "";
            this.ctrlBrowseVectorLayer.Filter = "";
            this.ctrlBrowseVectorLayer.IsFolderDialog = true;
            this.ctrlBrowseVectorLayer.IsFullPath = true;
            this.ctrlBrowseVectorLayer.IsSaveFile = false;
            this.ctrlBrowseVectorLayer.LabelCaption = "Vector Layer Dir:";
            this.ctrlBrowseVectorLayer.Location = new System.Drawing.Point(93, 21);
            this.ctrlBrowseVectorLayer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseVectorLayer.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseVectorLayer.MultiFilesSelect = false;
            this.ctrlBrowseVectorLayer.Name = "ctrlBrowseVectorLayer";
            this.ctrlBrowseVectorLayer.Prefix = "";
            this.ctrlBrowseVectorLayer.Size = new System.Drawing.Size(366, 24);
            this.ctrlBrowseVectorLayer.TabIndex = 18;
            // 
            // btnGetVectorLayerMinMaxScale
            // 
            this.btnGetVectorLayerMinMaxScale.Location = new System.Drawing.Point(332, 49);
            this.btnGetVectorLayerMinMaxScale.Name = "btnGetVectorLayerMinMaxScale";
            this.btnGetVectorLayerMinMaxScale.Size = new System.Drawing.Size(130, 23);
            this.btnGetVectorLayerMinMaxScale.TabIndex = 19;
            this.btnGetVectorLayerMinMaxScale.Text = "Get Layer Scale";
            this.btnGetVectorLayerMinMaxScale.UseVisualStyleBackColor = true;
            this.btnGetVectorLayerMinMaxScale.Click += new System.EventHandler(this.btnGetVectorLayerMinMaxScale_Click);
            // 
            // ntxVectorLayerMaxScale
            // 
            this.ntxVectorLayerMaxScale.Enabled = false;
            this.ntxVectorLayerMaxScale.Location = new System.Drawing.Point(45, 51);
            this.ntxVectorLayerMaxScale.Name = "ntxVectorLayerMaxScale";
            this.ntxVectorLayerMaxScale.Size = new System.Drawing.Size(100, 20);
            this.ntxVectorLayerMaxScale.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Max:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(164, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Min:";
            // 
            // ntxVectorLayerMinScale
            // 
            this.ntxVectorLayerMinScale.Enabled = false;
            this.ntxVectorLayerMinScale.Location = new System.Drawing.Point(197, 51);
            this.ntxVectorLayerMinScale.Name = "ntxVectorLayerMinScale";
            this.ntxVectorLayerMinScale.Size = new System.Drawing.Size(100, 20);
            this.ntxVectorLayerMinScale.TabIndex = 16;
            // 
            // btnMapProductionRemove
            // 
            this.btnMapProductionRemove.Location = new System.Drawing.Point(313, 4);
            this.btnMapProductionRemove.Name = "btnMapProductionRemove";
            this.btnMapProductionRemove.Size = new System.Drawing.Size(75, 23);
            this.btnMapProductionRemove.TabIndex = 26;
            this.btnMapProductionRemove.Text = "Remove";
            this.btnMapProductionRemove.UseVisualStyleBackColor = true;
            this.btnMapProductionRemove.Click += new System.EventHandler(this.btnMapProductionRemove_Click);
            // 
            // btnMapProductionCreate
            // 
            this.btnMapProductionCreate.Location = new System.Drawing.Point(394, 4);
            this.btnMapProductionCreate.Name = "btnMapProductionCreate";
            this.btnMapProductionCreate.Size = new System.Drawing.Size(75, 23);
            this.btnMapProductionCreate.TabIndex = 25;
            this.btnMapProductionCreate.Text = "Create";
            this.btnMapProductionCreate.UseVisualStyleBackColor = true;
            this.btnMapProductionCreate.Click += new System.EventHandler(this.btnMapProductionCreate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Map Production:";
            // 
            // btnNxtMapProduction
            // 
            this.btnNxtMapProduction.Location = new System.Drawing.Point(181, 4);
            this.btnNxtMapProduction.Name = "btnNxtMapProduction";
            this.btnNxtMapProduction.Size = new System.Drawing.Size(75, 23);
            this.btnNxtMapProduction.TabIndex = 23;
            this.btnNxtMapProduction.Text = "Next>>";
            this.btnNxtMapProduction.UseVisualStyleBackColor = true;
            this.btnNxtMapProduction.Click += new System.EventHandler(this.btnNxtMapProduction_Click);
            // 
            // btnPrvMapProduction
            // 
            this.btnPrvMapProduction.Location = new System.Drawing.Point(100, 4);
            this.btnPrvMapProduction.Name = "btnPrvMapProduction";
            this.btnPrvMapProduction.Size = new System.Drawing.Size(75, 23);
            this.btnPrvMapProduction.TabIndex = 22;
            this.btnPrvMapProduction.Text = "<<Prev";
            this.btnPrvMapProduction.UseVisualStyleBackColor = true;
            this.btnPrvMapProduction.Click += new System.EventHandler(this.btnPrvMapProduction_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Vector Layer Dir:";
            // 
            // frmVectorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(502, 165);
            this.Controls.Add(this.btnMapProductionRemove);
            this.Controls.Add(this.btnMapProductionCreate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnNxtMapProduction);
            this.Controls.Add(this.btnPrvMapProduction);
            this.Controls.Add(this.gbMapProductionParams);
            this.Name = "frmVectorial";
            this.Text = "frmVectorial";
            this.Load += new System.EventHandler(this.frmVectorial_Load);
            this.gbMapProductionParams.ResumeLayout(false);
            this.gbGetVectorLayerMinMaxScale.ResumeLayout(false);
            this.gbGetVectorLayerMinMaxScale.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMapProductionParams;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseVectorLayer;
        private System.Windows.Forms.Label label10;
        private MCTester.Controls.NumericTextBox ntxVectorLayerMinScale;
        private System.Windows.Forms.Label label9;
        private MCTester.Controls.NumericTextBox ntxVectorLayerMaxScale;
        private System.Windows.Forms.Button btnGetVectorLayerMinMaxScale;
        private System.Windows.Forms.GroupBox gbGetVectorLayerMinMaxScale;
        private System.Windows.Forms.Button btnMapProductionRemove;
        private System.Windows.Forms.Button btnMapProductionCreate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnNxtMapProduction;
        private System.Windows.Forms.Button btnPrvMapProduction;
        private System.Windows.Forms.Label label1;
    }
}