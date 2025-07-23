namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucProjectorItem
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
            this.Tab_ProjectorItem = new System.Windows.Forms.TabPage();
            this.chxIsUsingTextureMetadata = new System.Windows.Forms.CheckBox();
            this.ctrlObjStatePropertyProjectorTargetTypes = new MCTester.Controls.CtrlObjStatePropertyETargetTypesFlags();
            this.ctrlObjStatePropertyAspectRatio = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertyProjectorFOV = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertyTexture = new MCTester.Controls.CtrlObjStatePropertyTexture();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ctrlBrowseDomoFilesDirectory = new MCTester.Controls.CtrlBrowseControl();
            this.chxProjectorDemo = new System.Windows.Forms.CheckBox();
            this.gbProjectionBorders = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ntxProjectorBorderBottom = new MCTester.Controls.NumericTextBox();
            this.ntxProjectorBorderTop = new MCTester.Controls.NumericTextBox();
            this.ntxProjectorBorderLeft = new MCTester.Controls.NumericTextBox();
            this.ntxProjectorBorderRight = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.Tab_ProjectorItem.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbProjectionBorders.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_ProjectorItem);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ProjectorItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_ProjectorItem
            // 
            this.Tab_ProjectorItem.Controls.Add(this.chxIsUsingTextureMetadata);
            this.Tab_ProjectorItem.Controls.Add(this.ctrlObjStatePropertyProjectorTargetTypes);
            this.Tab_ProjectorItem.Controls.Add(this.ctrlObjStatePropertyAspectRatio);
            this.Tab_ProjectorItem.Controls.Add(this.ctrlObjStatePropertyProjectorFOV);
            this.Tab_ProjectorItem.Controls.Add(this.ctrlObjStatePropertyTexture);
            this.Tab_ProjectorItem.Controls.Add(this.groupBox1);
            this.Tab_ProjectorItem.Controls.Add(this.gbProjectionBorders);
            this.Tab_ProjectorItem.Location = new System.Drawing.Point(4, 22);
            this.Tab_ProjectorItem.Name = "Tab_ProjectorItem";
            this.Tab_ProjectorItem.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Tab_ProjectorItem.Size = new System.Drawing.Size(830, 824);
            this.Tab_ProjectorItem.TabIndex = 3;
            this.Tab_ProjectorItem.Text = "Projector";
            this.Tab_ProjectorItem.UseVisualStyleBackColor = true;
            // 
            // chxIsUsingTextureMetadata
            // 
            this.chxIsUsingTextureMetadata.AutoSize = true;
            this.chxIsUsingTextureMetadata.Enabled = false;
            this.chxIsUsingTextureMetadata.Location = new System.Drawing.Point(412, 278);
            this.chxIsUsingTextureMetadata.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chxIsUsingTextureMetadata.Name = "chxIsUsingTextureMetadata";
            this.chxIsUsingTextureMetadata.Size = new System.Drawing.Size(151, 17);
            this.chxIsUsingTextureMetadata.TabIndex = 17;
            this.chxIsUsingTextureMetadata.Text = "Is Using Texture Metadata";
            this.chxIsUsingTextureMetadata.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyProjectorTargetTypes
            // 
            this.ctrlObjStatePropertyProjectorTargetTypes.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyProjectorTargetTypes.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyProjectorTargetTypes.EnumType = "";
            this.ctrlObjStatePropertyProjectorTargetTypes.IsClickApply = false;
            this.ctrlObjStatePropertyProjectorTargetTypes.IsClickOK = false;
            this.ctrlObjStatePropertyProjectorTargetTypes.Location = new System.Drawing.Point(9, 273);
            this.ctrlObjStatePropertyProjectorTargetTypes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlObjStatePropertyProjectorTargetTypes.Name = "ctrlObjStatePropertyProjectorTargetTypes";
            this.ctrlObjStatePropertyProjectorTargetTypes.PropertyName = "Target Types";
            this.ctrlObjStatePropertyProjectorTargetTypes.PropertyValidationResult = null;
            this.ctrlObjStatePropertyProjectorTargetTypes.RegEnumVal = MapCore.DNETargetTypesFlags._ETTF_NONE;
            this.ctrlObjStatePropertyProjectorTargetTypes.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyProjectorTargetTypes.SelEnumVal = MapCore.DNETargetTypesFlags._ETTF_NONE;
            this.ctrlObjStatePropertyProjectorTargetTypes.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyProjectorTargetTypes.Size = new System.Drawing.Size(400, 171);
            this.ctrlObjStatePropertyProjectorTargetTypes.TabIndex = 16;
            // 
            // ctrlObjStatePropertyAspectRatio
            // 
            this.ctrlObjStatePropertyAspectRatio.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyAspectRatio.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyAspectRatio.IsClickApply = false;
            this.ctrlObjStatePropertyAspectRatio.IsClickOK = false;
            this.ctrlObjStatePropertyAspectRatio.Location = new System.Drawing.Point(9, 139);
            this.ctrlObjStatePropertyAspectRatio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlObjStatePropertyAspectRatio.Name = "ctrlObjStatePropertyAspectRatio";
            this.ctrlObjStatePropertyAspectRatio.PropertyName = "Aspect Ratio";
            this.ctrlObjStatePropertyAspectRatio.PropertyValidationResult = null;
            this.ctrlObjStatePropertyAspectRatio.RegFloatVal = 0F;
            this.ctrlObjStatePropertyAspectRatio.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyAspectRatio.SelFloatVal = 0F;
            this.ctrlObjStatePropertyAspectRatio.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyAspectRatio.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyAspectRatio.TabIndex = 15;
            // 
            // ctrlObjStatePropertyProjectorFOV
            // 
            this.ctrlObjStatePropertyProjectorFOV.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyProjectorFOV.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyProjectorFOV.IsClickApply = false;
            this.ctrlObjStatePropertyProjectorFOV.IsClickOK = false;
            this.ctrlObjStatePropertyProjectorFOV.Location = new System.Drawing.Point(9, 5);
            this.ctrlObjStatePropertyProjectorFOV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlObjStatePropertyProjectorFOV.Name = "ctrlObjStatePropertyProjectorFOV";
            this.ctrlObjStatePropertyProjectorFOV.PropertyName = "Half Field Of View";
            this.ctrlObjStatePropertyProjectorFOV.PropertyValidationResult = null;
            this.ctrlObjStatePropertyProjectorFOV.RegFloatVal = 0F;
            this.ctrlObjStatePropertyProjectorFOV.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyProjectorFOV.SelFloatVal = 0F;
            this.ctrlObjStatePropertyProjectorFOV.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyProjectorFOV.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyProjectorFOV.TabIndex = 14;
            // 
            // ctrlObjStatePropertyTexture
            // 
            this.ctrlObjStatePropertyTexture.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyTexture.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyTexture.IsClickApply = false;
            this.ctrlObjStatePropertyTexture.IsClickOK = false;
            this.ctrlObjStatePropertyTexture.Location = new System.Drawing.Point(413, 7);
            this.ctrlObjStatePropertyTexture.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctrlObjStatePropertyTexture.Name = "ctrlObjStatePropertyTexture";
            this.ctrlObjStatePropertyTexture.PropertyName = "Texture";
            this.ctrlObjStatePropertyTexture.PropertyValidationResult = null;
            this.ctrlObjStatePropertyTexture.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyTexture.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyTexture.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyTexture.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ctrlBrowseDomoFilesDirectory);
            this.groupBox1.Controls.Add(this.chxProjectorDemo);
            this.groupBox1.Location = new System.Drawing.Point(6, 636);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(806, 52);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Projector Demo";
            // 
            // ctrlBrowseDomoFilesDirectory
            // 
            this.ctrlBrowseDomoFilesDirectory.AutoSize = true;
            this.ctrlBrowseDomoFilesDirectory.FileName = "";
            this.ctrlBrowseDomoFilesDirectory.Filter = "";
            this.ctrlBrowseDomoFilesDirectory.IsFolderDialog = true;
            this.ctrlBrowseDomoFilesDirectory.IsFullPath = true;
            this.ctrlBrowseDomoFilesDirectory.IsSaveFile = false;
            this.ctrlBrowseDomoFilesDirectory.LabelCaption = "Files Directory:";
            this.ctrlBrowseDomoFilesDirectory.Location = new System.Drawing.Point(84, 18);
            this.ctrlBrowseDomoFilesDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrlBrowseDomoFilesDirectory.MinimumSize = new System.Drawing.Size(300, 24);
            this.ctrlBrowseDomoFilesDirectory.MultiFilesSelect = false;
            this.ctrlBrowseDomoFilesDirectory.Name = "ctrlBrowseDomoFilesDirectory";
            this.ctrlBrowseDomoFilesDirectory.Prefix = "";
            this.ctrlBrowseDomoFilesDirectory.Size = new System.Drawing.Size(635, 24);
            this.ctrlBrowseDomoFilesDirectory.TabIndex = 17;
            // 
            // chxProjectorDemo
            // 
            this.chxProjectorDemo.Appearance = System.Windows.Forms.Appearance.Button;
            this.chxProjectorDemo.AutoSize = true;
            this.chxProjectorDemo.Location = new System.Drawing.Point(731, 19);
            this.chxProjectorDemo.Name = "chxProjectorDemo";
            this.chxProjectorDemo.Size = new System.Drawing.Size(68, 23);
            this.chxProjectorDemo.TabIndex = 11;
            this.chxProjectorDemo.Text = "Run Demo";
            this.chxProjectorDemo.UseVisualStyleBackColor = true;
            this.chxProjectorDemo.CheckedChanged += new System.EventHandler(this.chxProjectorDemo_CheckedChanged);
            // 
            // gbProjectionBorders
            // 
            this.gbProjectionBorders.Controls.Add(this.label15);
            this.gbProjectionBorders.Controls.Add(this.label13);
            this.gbProjectionBorders.Controls.Add(this.label12);
            this.gbProjectionBorders.Controls.Add(this.label10);
            this.gbProjectionBorders.Controls.Add(this.ntxProjectorBorderBottom);
            this.gbProjectionBorders.Controls.Add(this.ntxProjectorBorderTop);
            this.gbProjectionBorders.Controls.Add(this.ntxProjectorBorderLeft);
            this.gbProjectionBorders.Controls.Add(this.ntxProjectorBorderRight);
            this.gbProjectionBorders.Location = new System.Drawing.Point(412, 142);
            this.gbProjectionBorders.Name = "gbProjectionBorders";
            this.gbProjectionBorders.Size = new System.Drawing.Size(400, 130);
            this.gbProjectionBorders.TabIndex = 8;
            this.gbProjectionBorders.TabStop = false;
            this.gbProjectionBorders.Text = "Projection Borders";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(196, 76);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "Bottom:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(196, 43);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Top:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 76);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Right:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 43);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Left:";
            // 
            // ntxProjectorBorderBottom
            // 
            this.ntxProjectorBorderBottom.Location = new System.Drawing.Point(255, 76);
            this.ntxProjectorBorderBottom.Name = "ntxProjectorBorderBottom";
            this.ntxProjectorBorderBottom.Size = new System.Drawing.Size(44, 20);
            this.ntxProjectorBorderBottom.TabIndex = 7;
            // 
            // ntxProjectorBorderTop
            // 
            this.ntxProjectorBorderTop.Location = new System.Drawing.Point(255, 43);
            this.ntxProjectorBorderTop.Name = "ntxProjectorBorderTop";
            this.ntxProjectorBorderTop.Size = new System.Drawing.Size(44, 20);
            this.ntxProjectorBorderTop.TabIndex = 3;
            // 
            // ntxProjectorBorderLeft
            // 
            this.ntxProjectorBorderLeft.Location = new System.Drawing.Point(57, 43);
            this.ntxProjectorBorderLeft.Name = "ntxProjectorBorderLeft";
            this.ntxProjectorBorderLeft.Size = new System.Drawing.Size(44, 20);
            this.ntxProjectorBorderLeft.TabIndex = 1;
            // 
            // ntxProjectorBorderRight
            // 
            this.ntxProjectorBorderRight.Location = new System.Drawing.Point(57, 76);
            this.ntxProjectorBorderRight.Name = "ntxProjectorBorderRight";
            this.ntxProjectorBorderRight.Size = new System.Drawing.Size(44, 20);
            this.ntxProjectorBorderRight.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Top:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(206, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Bottom:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Right:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Left:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Files Directory:";
            // 
            // ucProjectorItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucProjectorItem";
            this.tabControl1.ResumeLayout(false);
            this.Tab_ProjectorItem.ResumeLayout(false);
            this.Tab_ProjectorItem.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbProjectionBorders.ResumeLayout(false);
            this.gbProjectionBorders.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage Tab_ProjectorItem;
        private System.Windows.Forms.GroupBox gbProjectionBorders;
        private MCTester.Controls.NumericTextBox ntxProjectorBorderBottom;
        private System.Windows.Forms.Label label8;
        private MCTester.Controls.NumericTextBox ntxProjectorBorderRight;
        private System.Windows.Forms.Label label7;
        private MCTester.Controls.NumericTextBox ntxProjectorBorderTop;
        private System.Windows.Forms.Label label6;
        private MCTester.Controls.NumericTextBox ntxProjectorBorderLeft;
        private System.Windows.Forms.CheckBox chxProjectorDemo;
        private System.Windows.Forms.GroupBox groupBox1;
        private MCTester.Controls.CtrlBrowseControl ctrlBrowseDomoFilesDirectory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private Controls.CtrlObjStatePropertyTexture ctrlObjStatePropertyTexture;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertyProjectorFOV;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertyAspectRatio;
        private Controls.CtrlObjStatePropertyETargetTypesFlags ctrlObjStatePropertyProjectorTargetTypes;
        private System.Windows.Forms.CheckBox chxIsUsingTextureMetadata;
        private System.Windows.Forms.Label label2;
    }
}
