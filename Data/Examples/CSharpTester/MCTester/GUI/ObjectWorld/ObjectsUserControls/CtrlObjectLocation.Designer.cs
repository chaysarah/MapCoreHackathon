namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class CtrlObjectLocation
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
            this.label11 = new System.Windows.Forms.Label();
            this.ntxLocationIndex = new MCTester.Controls.NumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtLocationCoordSys = new System.Windows.Forms.TextBox();
            this.Tab_ObjectLocation = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyMaxNumOfPoints = new MCTester.Controls.CtrlObjStatePropertyUint();
            this.ctrlObjStatePropertyRelativeToDTM = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.tabControl1.SuspendLayout();
            this.Tab_ObjectLocation.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Size = new System.Drawing.Size(802, 824);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_ObjectLocation);
            this.tabControl1.Size = new System.Drawing.Size(810, 850);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectLocation, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 60;
            this.label11.Text = "Coordinate System:";
            // 
            // ntxLocationIndex
            // 
            this.ntxLocationIndex.Enabled = false;
            this.ntxLocationIndex.Location = new System.Drawing.Point(115, 9);
            this.ntxLocationIndex.Name = "ntxLocationIndex";
            this.ntxLocationIndex.Size = new System.Drawing.Size(71, 20);
            this.ntxLocationIndex.TabIndex = 61;
            this.ntxLocationIndex.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 12);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 62;
            this.label12.Text = "Location Index:";
            // 
            // txtLocationCoordSys
            // 
            this.txtLocationCoordSys.Enabled = false;
            this.txtLocationCoordSys.Location = new System.Drawing.Point(115, 37);
            this.txtLocationCoordSys.Name = "txtLocationCoordSys";
            this.txtLocationCoordSys.Size = new System.Drawing.Size(197, 20);
            this.txtLocationCoordSys.TabIndex = 63;
            // 
            // Tab_ObjectLocation
            // 
            this.Tab_ObjectLocation.Controls.Add(this.ctrlObjStatePropertyRelativeToDTM);
            this.Tab_ObjectLocation.Controls.Add(this.ctrlObjStatePropertyMaxNumOfPoints);
            this.Tab_ObjectLocation.Controls.Add(this.txtLocationCoordSys);
            this.Tab_ObjectLocation.Controls.Add(this.label12);
            this.Tab_ObjectLocation.Controls.Add(this.ntxLocationIndex);
            this.Tab_ObjectLocation.Controls.Add(this.label11);
            this.Tab_ObjectLocation.Location = new System.Drawing.Point(4, 22);
            this.Tab_ObjectLocation.Name = "Tab_ObjectLocation";
            this.Tab_ObjectLocation.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_ObjectLocation.Size = new System.Drawing.Size(802, 824);
            this.Tab_ObjectLocation.TabIndex = 1;
            this.Tab_ObjectLocation.Text = "Object Location";
            this.Tab_ObjectLocation.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyMaxNumOfPoints
            // 
            this.ctrlObjStatePropertyMaxNumOfPoints.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyMaxNumOfPoints.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyMaxNumOfPoints.IsClickApply = false;
            this.ctrlObjStatePropertyMaxNumOfPoints.IsClickOK = false;
            this.ctrlObjStatePropertyMaxNumOfPoints.Location = new System.Drawing.Point(9, 214);
            this.ctrlObjStatePropertyMaxNumOfPoints.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ctrlObjStatePropertyMaxNumOfPoints.Name = "ctrlObjStatePropertyMaxNumOfPoints";
            this.ctrlObjStatePropertyMaxNumOfPoints.PropertyName = "Max Num Of Points";
            this.ctrlObjStatePropertyMaxNumOfPoints.PropertyValidationResult = null;
            this.ctrlObjStatePropertyMaxNumOfPoints.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyMaxNumOfPoints.RegUintVal = ((uint)(0u));
            this.ctrlObjStatePropertyMaxNumOfPoints.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyMaxNumOfPoints.SelUintVal = ((uint)(0u));
            this.ctrlObjStatePropertyMaxNumOfPoints.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyMaxNumOfPoints.TabIndex = 69;
            // 
            // ctrlObjStatePropertyRelativeToDTM
            // 
            this.ctrlObjStatePropertyRelativeToDTM.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyRelativeToDTM.BoolLabel = "Is Relative To DTM";
            this.ctrlObjStatePropertyRelativeToDTM.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyRelativeToDTM.IsClickApply = false;
            this.ctrlObjStatePropertyRelativeToDTM.IsClickOK = false;
            this.ctrlObjStatePropertyRelativeToDTM.Location = new System.Drawing.Point(9, 71);
            this.ctrlObjStatePropertyRelativeToDTM.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.ctrlObjStatePropertyRelativeToDTM.Name = "ctrlObjStatePropertyRelativeToDTM";
            this.ctrlObjStatePropertyRelativeToDTM.PropertyName = "Relative To DTM";
            this.ctrlObjStatePropertyRelativeToDTM.PropertyValidationResult = null;
            this.ctrlObjStatePropertyRelativeToDTM.RegBoolVal = false;
            this.ctrlObjStatePropertyRelativeToDTM.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyRelativeToDTM.SelBoolVal = false;
            this.ctrlObjStatePropertyRelativeToDTM.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyRelativeToDTM.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyRelativeToDTM.TabIndex = 70;
            // 
            // CtrlObjectLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "CtrlObjectLocation";
            this.Size = new System.Drawing.Size(753, 720);
            this.tabControl1.ResumeLayout(false);
            this.Tab_ObjectLocation.ResumeLayout(false);
            this.Tab_ObjectLocation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private MCTester.Controls.NumericTextBox ntxLocationIndex;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtLocationCoordSys;
        protected System.Windows.Forms.TabPage Tab_ObjectLocation;
        private Controls.CtrlObjStatePropertyUint ctrlObjStatePropertyMaxNumOfPoints;
        private Controls.CtrlObjStatePropertyBool ctrlObjStatePropertyRelativeToDTM;
    }
}
