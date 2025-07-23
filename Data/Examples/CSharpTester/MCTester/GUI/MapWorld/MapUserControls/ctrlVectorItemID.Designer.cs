namespace MCTester.MapWorld.MapUserControls
{
    partial class CtrlVectorItemID
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ntxDataSourceAsName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbDataSourceAsName = new System.Windows.Forms.RadioButton();
            this.rbDataSourceAsID = new System.Windows.Forms.RadioButton();
            this.rbGlobalID = new System.Windows.Forms.RadioButton();
            this.ntxDataSourceAsId = new MCTester.Controls.NumericTextBox();
            this.ntxVectorItemId = new MCTester.Controls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.ntxDataSourceAsName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.rbDataSourceAsName);
            this.groupBox3.Controls.Add(this.ntxDataSourceAsId);
            this.groupBox3.Controls.Add(this.rbDataSourceAsID);
            this.groupBox3.Controls.Add(this.rbGlobalID);
            this.groupBox3.Controls.Add(this.ntxVectorItemId);
            this.groupBox3.Location = new System.Drawing.Point(0, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 189);
            this.groupBox3.TabIndex = 77;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Vector Item ID";
            // 
            // ntxDataSourceAsName
            // 
            this.ntxDataSourceAsName.Location = new System.Drawing.Point(6, 159);
            this.ntxDataSourceAsName.Name = "ntxDataSourceAsName";
            this.ntxDataSourceAsName.Size = new System.Drawing.Size(290, 22);
            this.ntxDataSourceAsName.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 17);
            this.label1.TabIndex = 53;
            this.label1.Text = "ID:";
            // 
            // rbDataSourceAsName
            // 
            this.rbDataSourceAsName.AutoSize = true;
            this.rbDataSourceAsName.Location = new System.Drawing.Point(6, 132);
            this.rbDataSourceAsName.Name = "rbDataSourceAsName";
            this.rbDataSourceAsName.Size = new System.Drawing.Size(245, 21);
            this.rbDataSourceAsName.TabIndex = 53;
            this.rbDataSourceAsName.Text = "Original ID with data source name:";
            this.rbDataSourceAsName.UseVisualStyleBackColor = true;
            this.rbDataSourceAsName.CheckedChanged += new System.EventHandler(this.rbDataSourceAsName_CheckedChanged);
            // 
            // rbDataSourceAsID
            // 
            this.rbDataSourceAsID.AutoSize = true;
            this.rbDataSourceAsID.Location = new System.Drawing.Point(6, 104);
            this.rbDataSourceAsID.Name = "rbDataSourceAsID";
            this.rbDataSourceAsID.Size = new System.Drawing.Size(223, 21);
            this.rbDataSourceAsID.TabIndex = 52;
            this.rbDataSourceAsID.Text = "Original ID with data source ID:";
            this.rbDataSourceAsID.UseVisualStyleBackColor = true;
            this.rbDataSourceAsID.CheckedChanged += new System.EventHandler(this.rbDataSourceAsID_CheckedChanged);
            // 
            // rbGlobalID
            // 
            this.rbGlobalID.AutoSize = true;
            this.rbGlobalID.Location = new System.Drawing.Point(6, 78);
            this.rbGlobalID.Name = "rbGlobalID";
            this.rbGlobalID.Size = new System.Drawing.Size(87, 21);
            this.rbGlobalID.TabIndex = 0;
            this.rbGlobalID.Text = "Global ID";
            this.rbGlobalID.UseVisualStyleBackColor = true;
            this.rbGlobalID.CheckedChanged += new System.EventHandler(this.rbUint64_CheckedChanged);
            // 
            // ntxDataSourceAsId
            // 
            this.ntxDataSourceAsId.Location = new System.Drawing.Point(242, 103);
            this.ntxDataSourceAsId.Margin = new System.Windows.Forms.Padding(4);
            this.ntxDataSourceAsId.Name = "ntxDataSourceAsId";
            this.ntxDataSourceAsId.Size = new System.Drawing.Size(54, 22);
            this.ntxDataSourceAsId.TabIndex = 49;
            this.ntxDataSourceAsId.Text = "0";
            // 
            // ntxVectorItemId
            // 
            this.ntxVectorItemId.Location = new System.Drawing.Point(67, 22);
            this.ntxVectorItemId.Margin = new System.Windows.Forms.Padding(4);
            this.ntxVectorItemId.Name = "ntxVectorItemId";
            this.ntxVectorItemId.Size = new System.Drawing.Size(112, 22);
            this.ntxVectorItemId.TabIndex = 37;
            this.ntxVectorItemId.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 54;
            this.label2.Text = "ID meaning:";
            // 
            // CtrlVectorItemID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "CtrlVectorItemID";
            this.Size = new System.Drawing.Size(307, 195);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private Controls.NumericTextBox ntxDataSourceAsId;
        private System.Windows.Forms.RadioButton rbGlobalID;
        private Controls.NumericTextBox ntxVectorItemId;
        private System.Windows.Forms.RadioButton rbDataSourceAsName;
        private System.Windows.Forms.RadioButton rbDataSourceAsID;
        private System.Windows.Forms.TextBox ntxDataSourceAsName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
