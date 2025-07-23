namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucPhysicalItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPhysicalItem));
            this.Tab_PhysicalItem = new System.Windows.Forms.TabPage();
            this.tabControlPhysicalItem = new System.Windows.Forms.TabControl();
            this.Tab_AttachPoints = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyAttachPoint = new MCTester.Controls.CtrlObjStatePropertyUint();
            this.Tab_Transforms = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyParallelToTerrain = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.ctrlPropertyPhysicalScale = new MCTester.Controls.CtrlObjStatePropertyFVect3D();
            //this.btnRotationApply = new System.Windows.Forms.Button();
            this.gbWireFrameEffect = new System.Windows.Forms.GroupBox();
            this.ctrlComboBoxObjectListWireFrame = new MCTester.Controls.CtrlComboBoxObjectList();
            this.btnWireFrameEffectRemove = new System.Windows.Forms.Button();
            this.btnWireFrameEffectApply = new System.Windows.Forms.Button();
            this.chxWireFrameEffectOnly = new System.Windows.Forms.CheckBox();
            this.ntxWireFrameEffectFadeTimeMS = new MCTester.Controls.NumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnWireFrameEffectColor = new System.Windows.Forms.Button();
            this.picWireFrameEffectColor = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numUDWireFrameEffect = new System.Windows.Forms.NumericUpDown();
            this.chxWireFrameEffectEnabled = new System.Windows.Forms.CheckBox();
            this.gbColorModulateEffect = new System.Windows.Forms.GroupBox();
            this.ctrlComboBoxObjectListColorModulate = new MCTester.Controls.CtrlComboBoxObjectList();
            this.btnColorModulateEffectRemove = new System.Windows.Forms.Button();
            this.btnColorModulateEffectApply = new System.Windows.Forms.Button();
            this.ntxFadeTimeMS = new MCTester.Controls.NumericTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnColorDlg = new System.Windows.Forms.Button();
            this.picbColor = new System.Windows.Forms.PictureBox();
            this.lblColorAlpha = new System.Windows.Forms.Label();
            this.numUDAlphaColor = new System.Windows.Forms.NumericUpDown();
            this.chxColorModulateEffectEnable = new System.Windows.Forms.CheckBox();
            this.ctrlPropertyInheritsParentRotation = new MCTester.Controls.CtrlPropertyBool();
            this.gbCurrRotation = new System.Windows.Forms.GroupBox();
            this.rdbRotationBaseOnItem = new System.Windows.Forms.RadioButton();
            this.btnGetCurrRotation = new System.Windows.Forms.Button();
            this.ntxPropertyID = new MCTester.Controls.NumericTextBox();
            this.lblSelectedObj = new System.Windows.Forms.Label();
            this.rdbRotationBaseOnID = new System.Windows.Forms.RadioButton();
            this.btnCurrRotationObjectList = new System.Windows.Forms.Button();
            this.chxRelativeToParent = new System.Windows.Forms.CheckBox();
            this.ctrl3DOrientationCurrRotation = new MCTester.Controls.Ctrl3DOrientation();
            this.ctrlPropertyRotation = new MCTester.Controls.CtrlPropertyOrientation();
            this.ctrlPropertyPhysicalOffset = new MCTester.Controls.CtrlObjStatePropertyFVect3D();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.Tab_PhysicalItem.SuspendLayout();
            this.tabControlPhysicalItem.SuspendLayout();
            this.Tab_AttachPoints.SuspendLayout();
            this.Tab_Transforms.SuspendLayout();
            this.gbWireFrameEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWireFrameEffectColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDWireFrameEffect)).BeginInit();
            this.gbColorModulateEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlphaColor)).BeginInit();
            this.gbCurrRotation.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab_ObjectSchemeItem
            // 
            this.Tab_ObjectSchemeItem.Margin = new System.Windows.Forms.Padding(5);
            this.Tab_ObjectSchemeItem.Padding = new System.Windows.Forms.Padding(5);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_PhysicalItem);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.Controls.SetChildIndex(this.Tab_PhysicalItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_PhysicalItem
            // 
            this.Tab_PhysicalItem.Controls.Add(this.tabControlPhysicalItem);
            this.Tab_PhysicalItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_PhysicalItem.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_PhysicalItem.Name = "Tab_PhysicalItem";
            this.Tab_PhysicalItem.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_PhysicalItem.Size = new System.Drawing.Size(1109, 857);
            this.Tab_PhysicalItem.TabIndex = 2;
            this.Tab_PhysicalItem.Text = "Physical Item";
            this.Tab_PhysicalItem.UseVisualStyleBackColor = true;
            // 
            // tabControlPhysicalItem
            // 
            this.tabControlPhysicalItem.Controls.Add(this.Tab_AttachPoints);
            this.tabControlPhysicalItem.Controls.Add(this.Tab_Transforms);
            this.tabControlPhysicalItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPhysicalItem.Location = new System.Drawing.Point(4, 4);
            this.tabControlPhysicalItem.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlPhysicalItem.Name = "tabControlPhysicalItem";
            this.tabControlPhysicalItem.SelectedIndex = 0;
            this.tabControlPhysicalItem.Size = new System.Drawing.Size(1101, 849);
            this.tabControlPhysicalItem.TabIndex = 0;
            // 
            // Tab_AttachPoints
            // 
            this.Tab_AttachPoints.Controls.Add(this.ctrlObjStatePropertyAttachPoint);
            this.Tab_AttachPoints.Location = new System.Drawing.Point(4, 25);
            this.Tab_AttachPoints.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_AttachPoints.Name = "Tab_AttachPoints";
            this.Tab_AttachPoints.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_AttachPoints.Size = new System.Drawing.Size(1093, 820);
            this.Tab_AttachPoints.TabIndex = 0;
            this.Tab_AttachPoints.Text = "Attach Points";
            this.Tab_AttachPoints.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyAttachPoint
            // 
            this.ctrlObjStatePropertyAttachPoint.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyAttachPoint.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyAttachPoint.Location = new System.Drawing.Point(7, 6);
            this.ctrlObjStatePropertyAttachPoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyAttachPoint.Name = "ctrlObjStatePropertyAttachPoint";
            this.ctrlObjStatePropertyAttachPoint.PropertyName = "Attach Point";
            this.ctrlObjStatePropertyAttachPoint.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyAttachPoint.RegUintVal = ((uint)(0u));
            this.ctrlObjStatePropertyAttachPoint.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyAttachPoint.SelUintVal = ((uint)(0u));
            this.ctrlObjStatePropertyAttachPoint.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyAttachPoint.TabIndex = 1;
            // 
            // Tab_Transforms
            // 
            this.Tab_Transforms.Controls.Add(this.ctrlObjStatePropertyParallelToTerrain);
            this.Tab_Transforms.Controls.Add(this.ctrlPropertyPhysicalScale);
            //this.Tab_Transforms.Controls.Add(this.btnRotationApply);
            this.Tab_Transforms.Controls.Add(this.gbWireFrameEffect);
            this.Tab_Transforms.Controls.Add(this.gbColorModulateEffect);
            this.Tab_Transforms.Controls.Add(this.ctrlPropertyInheritsParentRotation);
            this.Tab_Transforms.Controls.Add(this.gbCurrRotation);
            this.Tab_Transforms.Controls.Add(this.ctrlPropertyRotation);
            this.Tab_Transforms.Controls.Add(this.ctrlPropertyPhysicalOffset);
            this.Tab_Transforms.Location = new System.Drawing.Point(4, 25);
            this.Tab_Transforms.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_Transforms.Name = "Tab_Transforms";
            this.Tab_Transforms.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_Transforms.Size = new System.Drawing.Size(1093, 820);
            this.Tab_Transforms.TabIndex = 1;
            this.Tab_Transforms.Text = "Transforms";
            this.Tab_Transforms.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyParallelToTerrain
            // 
            this.ctrlObjStatePropertyParallelToTerrain.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyParallelToTerrain.BoolLabel = "Parallel To Terrain";
            this.ctrlObjStatePropertyParallelToTerrain.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyParallelToTerrain.Location = new System.Drawing.Point(5, 459);
            this.ctrlObjStatePropertyParallelToTerrain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyParallelToTerrain.Name = "ctrlObjStatePropertyParallelToTerrain";
            this.ctrlObjStatePropertyParallelToTerrain.PropertyName = "Parallel To Terrain";
            this.ctrlObjStatePropertyParallelToTerrain.RegBoolVal = false;
            this.ctrlObjStatePropertyParallelToTerrain.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyParallelToTerrain.SelBoolVal = false;
            this.ctrlObjStatePropertyParallelToTerrain.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyParallelToTerrain.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyParallelToTerrain.TabIndex = 75;
            // 
            // ctrlPropertyPhysicalScale
            // 
            this.ctrlPropertyPhysicalScale.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlPropertyPhysicalScale.CurrentObjectSchemeNode = null;
            this.ctrlPropertyPhysicalScale.Location = new System.Drawing.Point(3, 0);
            this.ctrlPropertyPhysicalScale.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ctrlPropertyPhysicalScale.Name = "ctrlPropertyPhysicalScale";
            this.ctrlPropertyPhysicalScale.PropertyName = "Scale";
            this.ctrlPropertyPhysicalScale.RegPropertyID = ((uint)(0u));
            this.ctrlPropertyPhysicalScale.SelPropertyID = ((uint)(4294967295u));
            this.ctrlPropertyPhysicalScale.Size = new System.Drawing.Size(533, 160);
            this.ctrlPropertyPhysicalScale.TabIndex = 74;
            // 
            // btnRotationApply
            // 
           /* this.btnRotationApply.Location = new System.Drawing.Point(973, 202);
            this.btnRotationApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnRotationApply.Name = "btnRotationApply";
            this.btnRotationApply.Size = new System.Drawing.Size(100, 28);
            this.btnRotationApply.TabIndex = 73;
            this.btnRotationApply.Text = "Apply";
            this.btnRotationApply.UseVisualStyleBackColor = true;
            this.btnRotationApply.Click += new System.EventHandler(this.btnRotationApply_Click);*/
            // 
            // gbWireFrameEffect
            // 
            this.gbWireFrameEffect.Controls.Add(this.ctrlComboBoxObjectListWireFrame);
            this.gbWireFrameEffect.Controls.Add(this.btnWireFrameEffectRemove);
            this.gbWireFrameEffect.Controls.Add(this.btnWireFrameEffectApply);
            this.gbWireFrameEffect.Controls.Add(this.chxWireFrameEffectOnly);
            this.gbWireFrameEffect.Controls.Add(this.ntxWireFrameEffectFadeTimeMS);
            this.gbWireFrameEffect.Controls.Add(this.label7);
            this.gbWireFrameEffect.Controls.Add(this.label8);
            this.gbWireFrameEffect.Controls.Add(this.btnWireFrameEffectColor);
            this.gbWireFrameEffect.Controls.Add(this.picWireFrameEffectColor);
            this.gbWireFrameEffect.Controls.Add(this.label9);
            this.gbWireFrameEffect.Controls.Add(this.numUDWireFrameEffect);
            this.gbWireFrameEffect.Controls.Add(this.chxWireFrameEffectEnabled);
            this.gbWireFrameEffect.Location = new System.Drawing.Point(4, 629);
            this.gbWireFrameEffect.Margin = new System.Windows.Forms.Padding(4);
            this.gbWireFrameEffect.Name = "gbWireFrameEffect";
            this.gbWireFrameEffect.Padding = new System.Windows.Forms.Padding(4);
            this.gbWireFrameEffect.Size = new System.Drawing.Size(533, 183);
            this.gbWireFrameEffect.TabIndex = 72;
            this.gbWireFrameEffect.TabStop = false;
            this.gbWireFrameEffect.Text = "Wire Frame Effect";
            // 
            // ctrlComboBoxObjectListWireFrame
            // 
            this.ctrlComboBoxObjectListWireFrame.Location = new System.Drawing.Point(8, 23);
            this.ctrlComboBoxObjectListWireFrame.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlComboBoxObjectListWireFrame.Name = "ctrlComboBoxObjectListWireFrame";
            this.ctrlComboBoxObjectListWireFrame.Size = new System.Drawing.Size(319, 30);
            this.ctrlComboBoxObjectListWireFrame.TabIndex = 33;
            // 
            // btnWireFrameEffectRemove
            // 
            this.btnWireFrameEffectRemove.Location = new System.Drawing.Point(317, 149);
            this.btnWireFrameEffectRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnWireFrameEffectRemove.Name = "btnWireFrameEffectRemove";
            this.btnWireFrameEffectRemove.Size = new System.Drawing.Size(100, 28);
            this.btnWireFrameEffectRemove.TabIndex = 32;
            this.btnWireFrameEffectRemove.Text = "Remove";
            this.btnWireFrameEffectRemove.UseVisualStyleBackColor = true;
            this.btnWireFrameEffectRemove.Click += new System.EventHandler(this.btnWireFrameEffectRemove_Click);
            // 
            // btnWireFrameEffectApply
            // 
            this.btnWireFrameEffectApply.Location = new System.Drawing.Point(425, 149);
            this.btnWireFrameEffectApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnWireFrameEffectApply.Name = "btnWireFrameEffectApply";
            this.btnWireFrameEffectApply.Size = new System.Drawing.Size(100, 28);
            this.btnWireFrameEffectApply.TabIndex = 31;
            this.btnWireFrameEffectApply.Text = "Apply";
            this.btnWireFrameEffectApply.UseVisualStyleBackColor = true;
            this.btnWireFrameEffectApply.Click += new System.EventHandler(this.btnWireFrameEffectApply_Click);
            // 
            // chxWireFrameEffectOnly
            // 
            this.chxWireFrameEffectOnly.AutoSize = true;
            this.chxWireFrameEffectOnly.Location = new System.Drawing.Point(8, 154);
            this.chxWireFrameEffectOnly.Margin = new System.Windows.Forms.Padding(4);
            this.chxWireFrameEffectOnly.Name = "chxWireFrameEffectOnly";
            this.chxWireFrameEffectOnly.Size = new System.Drawing.Size(136, 21);
            this.chxWireFrameEffectOnly.TabIndex = 30;
            this.chxWireFrameEffectOnly.Text = "Wire Frame Only";
            this.chxWireFrameEffectOnly.UseVisualStyleBackColor = true;
            // 
            // ntxWireFrameEffectFadeTimeMS
            // 
            this.ntxWireFrameEffectFadeTimeMS.Location = new System.Drawing.Point(117, 122);
            this.ntxWireFrameEffectFadeTimeMS.Margin = new System.Windows.Forms.Padding(4);
            this.ntxWireFrameEffectFadeTimeMS.Name = "ntxWireFrameEffectFadeTimeMS";
            this.ntxWireFrameEffectFadeTimeMS.Size = new System.Drawing.Size(80, 22);
            this.ntxWireFrameEffectFadeTimeMS.TabIndex = 29;
            this.ntxWireFrameEffectFadeTimeMS.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 126);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Fade Time MS:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 92);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "Color:";
            // 
            // btnWireFrameEffectColor
            // 
            this.btnWireFrameEffectColor.Location = new System.Drawing.Point(57, 86);
            this.btnWireFrameEffectColor.Margin = new System.Windows.Forms.Padding(4);
            this.btnWireFrameEffectColor.Name = "btnWireFrameEffectColor";
            this.btnWireFrameEffectColor.Size = new System.Drawing.Size(100, 28);
            this.btnWireFrameEffectColor.TabIndex = 27;
            this.btnWireFrameEffectColor.Text = "Color";
            this.btnWireFrameEffectColor.UseVisualStyleBackColor = true;
            this.btnWireFrameEffectColor.Click += new System.EventHandler(this.btnWireFrameEffectColor_Click);
            // 
            // picWireFrameEffectColor
            // 
            this.picWireFrameEffectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWireFrameEffectColor.Location = new System.Drawing.Point(165, 87);
            this.picWireFrameEffectColor.Margin = new System.Windows.Forms.Padding(4);
            this.picWireFrameEffectColor.Name = "picWireFrameEffectColor";
            this.picWireFrameEffectColor.Size = new System.Drawing.Size(33, 27);
            this.picWireFrameEffectColor.TabIndex = 25;
            this.picWireFrameEffectColor.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(277, 92);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 17);
            this.label9.TabIndex = 24;
            this.label9.Text = "Alpha:";
            // 
            // numUDWireFrameEffect
            // 
            this.numUDWireFrameEffect.Location = new System.Drawing.Point(335, 90);
            this.numUDWireFrameEffect.Margin = new System.Windows.Forms.Padding(4);
            this.numUDWireFrameEffect.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDWireFrameEffect.Name = "numUDWireFrameEffect";
            this.numUDWireFrameEffect.Size = new System.Drawing.Size(76, 22);
            this.numUDWireFrameEffect.TabIndex = 23;
            // 
            // chxWireFrameEffectEnabled
            // 
            this.chxWireFrameEffectEnabled.AutoSize = true;
            this.chxWireFrameEffectEnabled.Enabled = false;
            this.chxWireFrameEffectEnabled.Location = new System.Drawing.Point(8, 60);
            this.chxWireFrameEffectEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.chxWireFrameEffectEnabled.Name = "chxWireFrameEffectEnabled";
            this.chxWireFrameEffectEnabled.Size = new System.Drawing.Size(74, 21);
            this.chxWireFrameEffectEnabled.TabIndex = 1;
            this.chxWireFrameEffectEnabled.Text = "Enable";
            this.chxWireFrameEffectEnabled.UseVisualStyleBackColor = true;
            // 
            // gbColorModulateEffect
            // 
            this.gbColorModulateEffect.Controls.Add(this.ctrlComboBoxObjectListColorModulate);
            this.gbColorModulateEffect.Controls.Add(this.btnColorModulateEffectRemove);
            this.gbColorModulateEffect.Controls.Add(this.btnColorModulateEffectApply);
            this.gbColorModulateEffect.Controls.Add(this.ntxFadeTimeMS);
            this.gbColorModulateEffect.Controls.Add(this.label6);
            this.gbColorModulateEffect.Controls.Add(this.lblColor);
            this.gbColorModulateEffect.Controls.Add(this.btnColorDlg);
            this.gbColorModulateEffect.Controls.Add(this.picbColor);
            this.gbColorModulateEffect.Controls.Add(this.lblColorAlpha);
            this.gbColorModulateEffect.Controls.Add(this.numUDAlphaColor);
            this.gbColorModulateEffect.Controls.Add(this.chxColorModulateEffectEnable);
            this.gbColorModulateEffect.Location = new System.Drawing.Point(552, 425);
            this.gbColorModulateEffect.Margin = new System.Windows.Forms.Padding(4);
            this.gbColorModulateEffect.Name = "gbColorModulateEffect";
            this.gbColorModulateEffect.Padding = new System.Windows.Forms.Padding(4);
            this.gbColorModulateEffect.Size = new System.Drawing.Size(533, 160);
            this.gbColorModulateEffect.TabIndex = 71;
            this.gbColorModulateEffect.TabStop = false;
            this.gbColorModulateEffect.Text = "Color Modulate Effect";
            // 
            // ctrlComboBoxObjectListColorModulate
            // 
            this.ctrlComboBoxObjectListColorModulate.Location = new System.Drawing.Point(8, 23);
            this.ctrlComboBoxObjectListColorModulate.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlComboBoxObjectListColorModulate.Name = "ctrlComboBoxObjectListColorModulate";
            this.ctrlComboBoxObjectListColorModulate.Size = new System.Drawing.Size(319, 30);
            this.ctrlComboBoxObjectListColorModulate.TabIndex = 32;
            // 
            // btnColorModulateEffectRemove
            // 
            this.btnColorModulateEffectRemove.Location = new System.Drawing.Point(313, 119);
            this.btnColorModulateEffectRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnColorModulateEffectRemove.Name = "btnColorModulateEffectRemove";
            this.btnColorModulateEffectRemove.Size = new System.Drawing.Size(100, 28);
            this.btnColorModulateEffectRemove.TabIndex = 31;
            this.btnColorModulateEffectRemove.Text = "Remove";
            this.btnColorModulateEffectRemove.UseVisualStyleBackColor = true;
            this.btnColorModulateEffectRemove.Click += new System.EventHandler(this.btnColorModulateEffectRemove_Click);
            // 
            // btnColorModulateEffectApply
            // 
            this.btnColorModulateEffectApply.Location = new System.Drawing.Point(421, 119);
            this.btnColorModulateEffectApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnColorModulateEffectApply.Name = "btnColorModulateEffectApply";
            this.btnColorModulateEffectApply.Size = new System.Drawing.Size(100, 28);
            this.btnColorModulateEffectApply.TabIndex = 30;
            this.btnColorModulateEffectApply.Text = "Apply";
            this.btnColorModulateEffectApply.UseVisualStyleBackColor = true;
            this.btnColorModulateEffectApply.Click += new System.EventHandler(this.btnColorModulateEffectApply_Click);
            // 
            // ntxFadeTimeMS
            // 
            this.ntxFadeTimeMS.Location = new System.Drawing.Point(117, 122);
            this.ntxFadeTimeMS.Margin = new System.Windows.Forms.Padding(4);
            this.ntxFadeTimeMS.Name = "ntxFadeTimeMS";
            this.ntxFadeTimeMS.Size = new System.Drawing.Size(80, 22);
            this.ntxFadeTimeMS.TabIndex = 29;
            this.ntxFadeTimeMS.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 17);
            this.label6.TabIndex = 28;
            this.label6.Text = "Fade Time MS:";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(4, 92);
            this.lblColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(45, 17);
            this.lblColor.TabIndex = 26;
            this.lblColor.Text = "Color:";
            // 
            // btnColorDlg
            // 
            this.btnColorDlg.Location = new System.Drawing.Point(57, 86);
            this.btnColorDlg.Margin = new System.Windows.Forms.Padding(4);
            this.btnColorDlg.Name = "btnColorDlg";
            this.btnColorDlg.Size = new System.Drawing.Size(100, 28);
            this.btnColorDlg.TabIndex = 27;
            this.btnColorDlg.Text = "Color";
            this.btnColorDlg.UseVisualStyleBackColor = true;
            this.btnColorDlg.Click += new System.EventHandler(this.btnColorDlg_Click);
            // 
            // picbColor
            // 
            this.picbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbColor.Location = new System.Drawing.Point(165, 86);
            this.picbColor.Margin = new System.Windows.Forms.Padding(4);
            this.picbColor.Name = "picbColor";
            this.picbColor.Size = new System.Drawing.Size(33, 27);
            this.picbColor.TabIndex = 25;
            this.picbColor.TabStop = false;
            // 
            // lblColorAlpha
            // 
            this.lblColorAlpha.AutoSize = true;
            this.lblColorAlpha.Location = new System.Drawing.Point(277, 92);
            this.lblColorAlpha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorAlpha.Name = "lblColorAlpha";
            this.lblColorAlpha.Size = new System.Drawing.Size(48, 17);
            this.lblColorAlpha.TabIndex = 24;
            this.lblColorAlpha.Text = "Alpha:";
            // 
            // numUDAlphaColor
            // 
            this.numUDAlphaColor.Location = new System.Drawing.Point(335, 90);
            this.numUDAlphaColor.Margin = new System.Windows.Forms.Padding(4);
            this.numUDAlphaColor.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUDAlphaColor.Name = "numUDAlphaColor";
            this.numUDAlphaColor.Size = new System.Drawing.Size(76, 22);
            this.numUDAlphaColor.TabIndex = 23;
            // 
            // chxColorModulateEffectEnable
            // 
            this.chxColorModulateEffectEnable.AutoSize = true;
            this.chxColorModulateEffectEnable.Enabled = false;
            this.chxColorModulateEffectEnable.Location = new System.Drawing.Point(8, 60);
            this.chxColorModulateEffectEnable.Margin = new System.Windows.Forms.Padding(4);
            this.chxColorModulateEffectEnable.Name = "chxColorModulateEffectEnable";
            this.chxColorModulateEffectEnable.Size = new System.Drawing.Size(74, 21);
            this.chxColorModulateEffectEnable.TabIndex = 1;
            this.chxColorModulateEffectEnable.Text = "Enable";
            this.chxColorModulateEffectEnable.UseVisualStyleBackColor = true;
            // 
            // ctrlPropertyInheritsParentRotation
            // 
            this.ctrlPropertyInheritsParentRotation.IsSelectionProperty = false;
            this.ctrlPropertyInheritsParentRotation.Location = new System.Drawing.Point(552, 257);
            this.ctrlPropertyInheritsParentRotation.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPropertyInheritsParentRotation.Name = "ctrlPropertyInheritsParentRotation";
            this.ctrlPropertyInheritsParentRotation.PropertyName = "Inherits Parent Rotation";
            this.ctrlPropertyInheritsParentRotation.RegBoolLable = "Inherits Parent";
            this.ctrlPropertyInheritsParentRotation.RegBoolVal = false;
            this.ctrlPropertyInheritsParentRotation.RegPropertyID = ((uint)(4294967295u));
            this.ctrlPropertyInheritsParentRotation.SelBoolLable = "Inherits Parent";
            this.ctrlPropertyInheritsParentRotation.SelBoolVal = false;
            this.ctrlPropertyInheritsParentRotation.SelPropertyID = ((uint)(4294967294u));
            this.ctrlPropertyInheritsParentRotation.Size = new System.Drawing.Size(533, 160);
            this.ctrlPropertyInheritsParentRotation.TabIndex = 66;
            // 
            // gbCurrRotation
            // 
            this.gbCurrRotation.Controls.Add(this.rdbRotationBaseOnItem);
            this.gbCurrRotation.Controls.Add(this.btnGetCurrRotation);
            this.gbCurrRotation.Controls.Add(this.ntxPropertyID);
            this.gbCurrRotation.Controls.Add(this.lblSelectedObj);
            this.gbCurrRotation.Controls.Add(this.rdbRotationBaseOnID);
            this.gbCurrRotation.Controls.Add(this.btnCurrRotationObjectList);
            this.gbCurrRotation.Controls.Add(this.chxRelativeToParent);
            this.gbCurrRotation.Controls.Add(this.ctrl3DOrientationCurrRotation);
            this.gbCurrRotation.Location = new System.Drawing.Point(5, 318);
            this.gbCurrRotation.Margin = new System.Windows.Forms.Padding(4);
            this.gbCurrRotation.Name = "gbCurrRotation";
            this.gbCurrRotation.Padding = new System.Windows.Forms.Padding(4);
            this.gbCurrRotation.Size = new System.Drawing.Size(533, 135);
            this.gbCurrRotation.TabIndex = 65;
            this.gbCurrRotation.TabStop = false;
            this.gbCurrRotation.Text = "Current Rotation";
            // 
            // rdbRotationBaseOnItem
            // 
            this.rdbRotationBaseOnItem.AutoSize = true;
            this.rdbRotationBaseOnItem.Location = new System.Drawing.Point(349, 28);
            this.rdbRotationBaseOnItem.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRotationBaseOnItem.Name = "rdbRotationBaseOnItem";
            this.rdbRotationBaseOnItem.Size = new System.Drawing.Size(171, 21);
            this.rdbRotationBaseOnItem.TabIndex = 9;
            this.rdbRotationBaseOnItem.TabStop = true;
            this.rdbRotationBaseOnItem.Text = "Rotation Base On Item";
            this.rdbRotationBaseOnItem.UseVisualStyleBackColor = true;
            // 
            // btnGetCurrRotation
            // 
            this.btnGetCurrRotation.Enabled = false;
            this.btnGetCurrRotation.Location = new System.Drawing.Point(400, 95);
            this.btnGetCurrRotation.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetCurrRotation.Name = "btnGetCurrRotation";
            this.btnGetCurrRotation.Size = new System.Drawing.Size(132, 28);
            this.btnGetCurrRotation.TabIndex = 7;
            this.btnGetCurrRotation.Text = "Get Curr Rotation";
            this.btnGetCurrRotation.UseVisualStyleBackColor = true;
            this.btnGetCurrRotation.Click += new System.EventHandler(this.btnGetCurrRotation_Click);
            // 
            // ntxPropertyID
            // 
            this.ntxPropertyID.Location = new System.Drawing.Point(240, 27);
            this.ntxPropertyID.Margin = new System.Windows.Forms.Padding(4);
            this.ntxPropertyID.Name = "ntxPropertyID";
            this.ntxPropertyID.Size = new System.Drawing.Size(69, 22);
            this.ntxPropertyID.TabIndex = 5;
            // 
            // lblSelectedObj
            // 
            this.lblSelectedObj.AutoSize = true;
            this.lblSelectedObj.Location = new System.Drawing.Point(345, 65);
            this.lblSelectedObj.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedObj.Name = "lblSelectedObj";
            this.lblSelectedObj.Size = new System.Drawing.Size(89, 17);
            this.lblSelectedObj.TabIndex = 6;
            this.lblSelectedObj.Text = "Not Selected";
            // 
            // rdbRotationBaseOnID
            // 
            this.rdbRotationBaseOnID.AutoSize = true;
            this.rdbRotationBaseOnID.Location = new System.Drawing.Point(15, 28);
            this.rdbRotationBaseOnID.Margin = new System.Windows.Forms.Padding(4);
            this.rdbRotationBaseOnID.Name = "rdbRotationBaseOnID";
            this.rdbRotationBaseOnID.Size = new System.Drawing.Size(220, 21);
            this.rdbRotationBaseOnID.TabIndex = 8;
            this.rdbRotationBaseOnID.TabStop = true;
            this.rdbRotationBaseOnID.Text = "Rotation Base On Property ID:";
            this.rdbRotationBaseOnID.UseVisualStyleBackColor = true;
            // 
            // btnCurrRotationObjectList
            // 
            this.btnCurrRotationObjectList.Location = new System.Drawing.Point(240, 59);
            this.btnCurrRotationObjectList.Margin = new System.Windows.Forms.Padding(4);
            this.btnCurrRotationObjectList.Name = "btnCurrRotationObjectList";
            this.btnCurrRotationObjectList.Size = new System.Drawing.Size(100, 28);
            this.btnCurrRotationObjectList.TabIndex = 3;
            this.btnCurrRotationObjectList.Text = "Object";
            this.btnCurrRotationObjectList.UseVisualStyleBackColor = true;
            this.btnCurrRotationObjectList.Click += new System.EventHandler(this.btnCurrRotationObjectList_Click);
            // 
            // chxRelativeToParent
            // 
            this.chxRelativeToParent.AutoSize = true;
            this.chxRelativeToParent.Location = new System.Drawing.Point(15, 64);
            this.chxRelativeToParent.Margin = new System.Windows.Forms.Padding(4);
            this.chxRelativeToParent.Name = "chxRelativeToParent";
            this.chxRelativeToParent.Size = new System.Drawing.Size(148, 21);
            this.chxRelativeToParent.TabIndex = 2;
            this.chxRelativeToParent.Text = "Relative To Parent";
            this.chxRelativeToParent.UseVisualStyleBackColor = true;
            // 
            // ctrl3DOrientationCurrRotation
            // 
            this.ctrl3DOrientationCurrRotation.Enabled = false;
            this.ctrl3DOrientationCurrRotation.Location = new System.Drawing.Point(15, 95);
            this.ctrl3DOrientationCurrRotation.Margin = new System.Windows.Forms.Padding(5);
            this.ctrl3DOrientationCurrRotation.Name = "ctrl3DOrientationCurrRotation";
            this.ctrl3DOrientationCurrRotation.Pitch = 0F;
            this.ctrl3DOrientationCurrRotation.Roll = 0F;
            this.ctrl3DOrientationCurrRotation.Size = new System.Drawing.Size(385, 27);
            this.ctrl3DOrientationCurrRotation.TabIndex = 0;
            this.ctrl3DOrientationCurrRotation.Yaw = 0F;
            // 
            // ctrlPropertyRotation
            // 
            this.ctrlPropertyRotation.IsSelectionProperty = false;
            this.ctrlPropertyRotation.Location = new System.Drawing.Point(552, 47);
            this.ctrlPropertyRotation.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ctrlPropertyRotation.Name = "ctrlPropertyRotation";
            this.ctrlPropertyRotation.PhisicalItem = null;
            this.ctrlPropertyRotation.PropertyName = "Rotation";
            this.ctrlPropertyRotation.RegPropertyID = ((uint)(0u));
            this.ctrlPropertyRotation.SelPropertyID = ((uint)(4294967294u));
            this.ctrlPropertyRotation.Size = new System.Drawing.Size(533, 203);
            this.ctrlPropertyRotation.TabIndex = 63;
            // 
            // ctrlPropertyPhysicalOffset
            // 
            this.ctrlPropertyPhysicalOffset.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlPropertyPhysicalOffset.CurrentObjectSchemeNode = null;
            this.ctrlPropertyPhysicalOffset.Location = new System.Drawing.Point(4, 158);
            this.ctrlPropertyPhysicalOffset.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.ctrlPropertyPhysicalOffset.Name = "ctrlPropertyPhysicalOffset";
            this.ctrlPropertyPhysicalOffset.PropertyName = "Offset";
            this.ctrlPropertyPhysicalOffset.RegPropertyID = ((uint)(0u));
            this.ctrlPropertyPhysicalOffset.SelPropertyID = ((uint)(4294967295u));
            this.ctrlPropertyPhysicalOffset.Size = new System.Drawing.Size(533, 160);
            this.ctrlPropertyPhysicalOffset.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = "Scale:";
            // 
            // ucPhysicalItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucPhysicalItem";
            this.Load += new System.EventHandler(this.ucPhysicalItem_Load);
            this.tabControl1.ResumeLayout(false);
            this.Tab_PhysicalItem.ResumeLayout(false);
            this.tabControlPhysicalItem.ResumeLayout(false);
            this.Tab_AttachPoints.ResumeLayout(false);
            this.Tab_Transforms.ResumeLayout(false);
            this.gbWireFrameEffect.ResumeLayout(false);
            this.gbWireFrameEffect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWireFrameEffectColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDWireFrameEffect)).EndInit();
            this.gbColorModulateEffect.ResumeLayout(false);
            this.gbColorModulateEffect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUDAlphaColor)).EndInit();
            this.gbCurrRotation.ResumeLayout(false);
            this.gbCurrRotation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage Tab_PhysicalItem;
        private System.Windows.Forms.TabControl tabControlPhysicalItem;
        private System.Windows.Forms.TabPage Tab_AttachPoints;
        private System.Windows.Forms.TabPage Tab_Transforms;
        private MCTester.Controls.CtrlObjStatePropertyFVect3D ctrlPropertyPhysicalOffset;
        private MCTester.Controls.CtrlPropertyOrientation ctrlPropertyRotation;
        private System.Windows.Forms.GroupBox gbCurrRotation;
        private System.Windows.Forms.Label lblSelectedObj;
        private System.Windows.Forms.Button btnCurrRotationObjectList;
        private System.Windows.Forms.CheckBox chxRelativeToParent;
        private MCTester.Controls.Ctrl3DOrientation ctrl3DOrientationCurrRotation;
        private System.Windows.Forms.Button btnGetCurrRotation;
        private MCTester.Controls.NumericTextBox ntxPropertyID;
        private System.Windows.Forms.RadioButton rdbRotationBaseOnItem;
        private System.Windows.Forms.RadioButton rdbRotationBaseOnID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbColorModulateEffect;
        private System.Windows.Forms.CheckBox chxColorModulateEffectEnable;
        private System.Windows.Forms.PictureBox picbColor;
        private System.Windows.Forms.Label lblColorAlpha;
        private System.Windows.Forms.NumericUpDown numUDAlphaColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnColorDlg;
        private MCTester.Controls.NumericTextBox ntxFadeTimeMS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbWireFrameEffect;
        private MCTester.Controls.NumericTextBox ntxWireFrameEffectFadeTimeMS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnWireFrameEffectColor;
        private System.Windows.Forms.PictureBox picWireFrameEffectColor;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numUDWireFrameEffect;
        private System.Windows.Forms.CheckBox chxWireFrameEffectEnabled;
        private System.Windows.Forms.CheckBox chxWireFrameEffectOnly;
        private System.Windows.Forms.Button btnWireFrameEffectApply;
        private System.Windows.Forms.Button btnColorModulateEffectApply;
        private System.Windows.Forms.Button btnWireFrameEffectRemove;
        private System.Windows.Forms.Button btnColorModulateEffectRemove;
       // private System.Windows.Forms.Button btnRotationApply;
        private Controls.CtrlComboBoxObjectList ctrlComboBoxObjectListColorModulate;
        private Controls.CtrlComboBoxObjectList ctrlComboBoxObjectListWireFrame;
        private Controls.CtrlObjStatePropertyFVect3D ctrlPropertyPhysicalScale;
        private Controls.CtrlObjStatePropertyUint ctrlObjStatePropertyAttachPoint;
        private Controls.CtrlObjStatePropertyBool ctrlObjStatePropertyParallelToTerrain;
        private Controls.CtrlPropertyBool ctrlPropertyInheritsParentRotation;
    }
}
