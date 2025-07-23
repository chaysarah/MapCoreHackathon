namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class frmCalculatedCoordinates
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
            this.lstObjects = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCalcCoordOK = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbPointCoordSys = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.chxWithAddedPoints = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNoPoints = new System.Windows.Forms.TextBox();
            this.ctrlPointsGrid1 = new MCTester.Controls.CtrlPointsGrid();
            this.SuspendLayout();
            // 
            // lstObjects
            // 
            this.lstObjects.FormattingEnabled = true;
            this.lstObjects.Location = new System.Drawing.Point(5, 34);
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.Size = new System.Drawing.Size(234, 199);
            this.lstObjects.TabIndex = 34;
            this.lstObjects.SelectedIndexChanged += new System.EventHandler(this.lstObjects_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(2, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Select Objects:";
            // 
            // btnCalcCoordOK
            // 
            this.btnCalcCoordOK.Location = new System.Drawing.Point(437, 243);
            this.btnCalcCoordOK.Name = "btnCalcCoordOK";
            this.btnCalcCoordOK.Size = new System.Drawing.Size(75, 23);
            this.btnCalcCoordOK.TabIndex = 41;
            this.btnCalcCoordOK.Text = "OK";
            this.btnCalcCoordOK.UseVisualStyleBackColor = true;
            this.btnCalcCoordOK.Visible = false;
            this.btnCalcCoordOK.Click += new System.EventHandler(this.btnCalcCoordOK_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(244, 11);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(99, 13);
            this.label16.TabIndex = 40;
            this.label16.Text = "Point coord system:";
            // 
            // cmbPointCoordSys
            // 
            this.cmbPointCoordSys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointCoordSys.FormattingEnabled = true;
            this.cmbPointCoordSys.Location = new System.Drawing.Point(349, 7);
            this.cmbPointCoordSys.Name = "cmbPointCoordSys";
            this.cmbPointCoordSys.Size = new System.Drawing.Size(244, 21);
            this.cmbPointCoordSys.TabIndex = 39;
            this.cmbPointCoordSys.SelectedIndexChanged += new System.EventHandler(this.cmbPointCoordSys_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(518, 243);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chxWithAddedPoints
            // 
            this.chxWithAddedPoints.AutoSize = true;
            this.chxWithAddedPoints.Location = new System.Drawing.Point(130, 10);
            this.chxWithAddedPoints.Margin = new System.Windows.Forms.Padding(2);
            this.chxWithAddedPoints.Name = "chxWithAddedPoints";
            this.chxWithAddedPoints.Size = new System.Drawing.Size(112, 17);
            this.chxWithAddedPoints.TabIndex = 43;
            this.chxWithAddedPoints.Text = "With added points";
            this.chxWithAddedPoints.UseVisualStyleBackColor = true;
            this.chxWithAddedPoints.CheckedChanged += new System.EventHandler(this.chxIndices_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(244, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "No. Points:";
            // 
            // txtNoPoints
            // 
            this.txtNoPoints.Enabled = false;
            this.txtNoPoints.Location = new System.Drawing.Point(305, 243);
            this.txtNoPoints.Name = "txtNoPoints";
            this.txtNoPoints.Size = new System.Drawing.Size(73, 20);
            this.txtNoPoints.TabIndex = 45;
            // 
            // ctrlPointsGrid1
            // 
            this.ctrlPointsGrid1.Location = new System.Drawing.Point(247, 34);
            this.ctrlPointsGrid1.Name = "ctrlPointsGrid1";
            this.ctrlPointsGrid1.Size = new System.Drawing.Size(345, 203);
            this.ctrlPointsGrid1.TabIndex = 46;
            // 
            // frmCalculatedCoordinates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(595, 274);
            this.Controls.Add(this.ctrlPointsGrid1);
            this.Controls.Add(this.txtNoPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chxWithAddedPoints);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCalcCoordOK);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.cmbPointCoordSys);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstObjects);
            this.Name = "frmCalculatedCoordinates";
            this.Text = "frmCalculatedCoordinates";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstObjects;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCalcCoordOK;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbPointCoordSys;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chxWithAddedPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNoPoints;
        private Controls.CtrlPointsGrid ctrlPointsGrid1;
    }
}