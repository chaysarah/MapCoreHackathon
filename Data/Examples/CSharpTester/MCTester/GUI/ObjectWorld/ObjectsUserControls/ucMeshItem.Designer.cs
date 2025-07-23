using MapCore;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucMeshItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMeshItem));
            this.Tab_MeshItem = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.Tab_MeshGeneral = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertyMesh = new MCTester.Controls.CtrlObjStatePropertyMesh();
            this.ctrlObjStatePropertyTextureScroll = new MCTester.Controls.CtrlObjStatePropertyTextureScroll();
            this.ctrlObjStatePropertyAnimation = new MCTester.Controls.CtrlObjStatePropertyAnimation();
            this.ctrlObjStatePropertyCastShadows = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.ctrlObjStatePropertyBasePointAlignment = new MCTester.Controls.CtrlObjStatePropertyEBasePointAlignment();
            this.chxDisplayItemsAttachedToTerrain = new System.Windows.Forms.CheckBox();
            this.chxParticipationInTerrainHeight = new System.Windows.Forms.CheckBox();
            this.Tab_MeshSubPartTransform = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertySubPartOffset = new MCTester.Controls.CtrlObjStatePropertySubPartOffset();
            this.ctrlObjStateSubPartInheritsParentRotation = new MCTester.Controls.CtrlObjStatePropertyDictionaryBool();
            this.ctrlObjStatePropertySubPartRotation = new MCTester.Controls.CtrlObjStatePropertySubPartRotation();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSelectedObject = new System.Windows.Forms.Label();
            this.btnGetSubPartTransformCurrentRotation = new System.Windows.Forms.Button();
            this.chxRelativeToParentSubPart = new System.Windows.Forms.CheckBox();
            this.ntxPropertyIDSubPart = new MCTester.Controls.NumericTextBox();
            this.rdbAttachPointID = new System.Windows.Forms.RadioButton();
            this.ntxAttachPointID = new MCTester.Controls.NumericTextBox();
            this.btnSubPartCurrRotationObjectList = new System.Windows.Forms.Button();
            this.rdbPropertyID = new System.Windows.Forms.RadioButton();
            this.ctrl3DOrientationSubPartCurrRotation = new MCTester.Controls.Ctrl3DOrientation();
            this.ntxPropertyID = new MCTester.Controls.NumericTextBox();
            this.chxRelativeToParent = new System.Windows.Forms.CheckBox();
            this.chxRelativeToCurrOrientation = new System.Windows.Forms.CheckBox();
            this.chxStatic = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.Tab_MeshItem.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.Tab_MeshGeneral.SuspendLayout();
            this.Tab_MeshSubPartTransform.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_MeshItem);
            this.tabControl1.Controls.SetChildIndex(this.Tab_MeshItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_MeshItem
            // 
            this.Tab_MeshItem.Controls.Add(this.tabControl2);
            this.Tab_MeshItem.Location = new System.Drawing.Point(4, 25);
            this.Tab_MeshItem.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_MeshItem.Name = "Tab_MeshItem";
            this.Tab_MeshItem.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_MeshItem.Size = new System.Drawing.Size(1109, 857);
            this.Tab_MeshItem.TabIndex = 3;
            this.Tab_MeshItem.Text = "Mesh Item";
            this.Tab_MeshItem.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.Tab_MeshGeneral);
            this.tabControl2.Controls.Add(this.Tab_MeshSubPartTransform);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(4, 4);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1101, 849);
            this.tabControl2.TabIndex = 3;
            // 
            // Tab_MeshGeneral
            // 
            this.Tab_MeshGeneral.Controls.Add(this.chxStatic);
            this.Tab_MeshGeneral.Controls.Add(this.ctrlObjStatePropertyMesh);
            this.Tab_MeshGeneral.Controls.Add(this.ctrlObjStatePropertyTextureScroll);
            this.Tab_MeshGeneral.Controls.Add(this.ctrlObjStatePropertyAnimation);
            this.Tab_MeshGeneral.Controls.Add(this.ctrlObjStatePropertyCastShadows);
            this.Tab_MeshGeneral.Controls.Add(this.ctrlObjStatePropertyBasePointAlignment);
            this.Tab_MeshGeneral.Controls.Add(this.chxDisplayItemsAttachedToTerrain);
            this.Tab_MeshGeneral.Controls.Add(this.chxParticipationInTerrainHeight);
            this.Tab_MeshGeneral.Location = new System.Drawing.Point(4, 25);
            this.Tab_MeshGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_MeshGeneral.Name = "Tab_MeshGeneral";
            this.Tab_MeshGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_MeshGeneral.Size = new System.Drawing.Size(1093, 820);
            this.Tab_MeshGeneral.TabIndex = 0;
            this.Tab_MeshGeneral.Text = "General";
            this.Tab_MeshGeneral.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertyMesh
            // 
            this.ctrlObjStatePropertyMesh.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyMesh.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyMesh.IsClickApply = false;
            this.ctrlObjStatePropertyMesh.IsClickOK = false;
            this.ctrlObjStatePropertyMesh.Location = new System.Drawing.Point(7, 6);
            this.ctrlObjStatePropertyMesh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyMesh.Name = "ctrlObjStatePropertyMesh";
            this.ctrlObjStatePropertyMesh.PropertyName = "Mesh";
            this.ctrlObjStatePropertyMesh.PropertyValidationResult = null;
            this.ctrlObjStatePropertyMesh.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyMesh.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyMesh.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyMesh.TabIndex = 13;
            // 
            // ctrlObjStatePropertyTextureScroll
            // 
            this.ctrlObjStatePropertyTextureScroll.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyTextureScroll.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyTextureScroll.IsClickApply = false;
            this.ctrlObjStatePropertyTextureScroll.IsClickOK = false;
            this.ctrlObjStatePropertyTextureScroll.Location = new System.Drawing.Point(545, 176);
            this.ctrlObjStatePropertyTextureScroll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyTextureScroll.MeshTextureID = ((uint)(DNMcConstants._MC_EMPTY_ID));
            this.ctrlObjStatePropertyTextureScroll.Name = "ctrlObjStatePropertyTextureScroll";
            this.ctrlObjStatePropertyTextureScroll.PropertyName = "Texture Scroll Speed";
            this.ctrlObjStatePropertyTextureScroll.PropertyValidationResult = null;
            this.ctrlObjStatePropertyTextureScroll.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyTextureScroll.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyTextureScroll.Size = new System.Drawing.Size(527, 186);
            this.ctrlObjStatePropertyTextureScroll.TabIndex = 12;
            // 
            // ctrlObjStatePropertyAnimation
            // 
            this.ctrlObjStatePropertyAnimation.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyAnimation.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyAnimation.IsClickApply = false;
            this.ctrlObjStatePropertyAnimation.IsClickOK = false;
            this.ctrlObjStatePropertyAnimation.Location = new System.Drawing.Point(7, 176);
            this.ctrlObjStatePropertyAnimation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyAnimation.Name = "ctrlObjStatePropertyAnimation";
            this.ctrlObjStatePropertyAnimation.PropertyName = "Animation";
            this.ctrlObjStatePropertyAnimation.PropertyValidationResult = null;
            this.ctrlObjStatePropertyAnimation.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyAnimation.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyAnimation.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyAnimation.TabIndex = 11;
            // 
            // ctrlObjStatePropertyCastShadows
            // 
            this.ctrlObjStatePropertyCastShadows.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyCastShadows.BoolLabel = "Cast Shadows";
            this.ctrlObjStatePropertyCastShadows.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyCastShadows.IsClickApply = false;
            this.ctrlObjStatePropertyCastShadows.IsClickOK = false;
            this.ctrlObjStatePropertyCastShadows.Location = new System.Drawing.Point(7, 341);
            this.ctrlObjStatePropertyCastShadows.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyCastShadows.Name = "ctrlObjStatePropertyCastShadows";
            this.ctrlObjStatePropertyCastShadows.PropertyName = "Cast Shadows";
            this.ctrlObjStatePropertyCastShadows.PropertyValidationResult = null;
            this.ctrlObjStatePropertyCastShadows.RegBoolVal = false;
            this.ctrlObjStatePropertyCastShadows.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyCastShadows.SelBoolVal = false;
            this.ctrlObjStatePropertyCastShadows.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyCastShadows.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyCastShadows.TabIndex = 10;
            // 
            // ctrlObjStatePropertyBasePointAlignment
            // 
            this.ctrlObjStatePropertyBasePointAlignment.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyBasePointAlignment.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyBasePointAlignment.EnumType = "";
            this.ctrlObjStatePropertyBasePointAlignment.IsClickApply = false;
            this.ctrlObjStatePropertyBasePointAlignment.IsClickOK = false;
            this.ctrlObjStatePropertyBasePointAlignment.Location = new System.Drawing.Point(545, 6);
            this.ctrlObjStatePropertyBasePointAlignment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertyBasePointAlignment.Name = "ctrlObjStatePropertyBasePointAlignment";
            this.ctrlObjStatePropertyBasePointAlignment.PropertyName = "Base Point Alignment";
            this.ctrlObjStatePropertyBasePointAlignment.PropertyValidationResult = null;
            this.ctrlObjStatePropertyBasePointAlignment.RegEnumVal = MapCore.DNEBasePointAlignment._EBPA_MESH_ZERO;
            this.ctrlObjStatePropertyBasePointAlignment.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyBasePointAlignment.SelEnumVal = MapCore.DNEBasePointAlignment._EBPA_MESH_ZERO;
            this.ctrlObjStatePropertyBasePointAlignment.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyBasePointAlignment.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertyBasePointAlignment.TabIndex = 9;
            // 
            // chxDisplayItemsAttachedToTerrain
            // 
            this.chxDisplayItemsAttachedToTerrain.AutoSize = true;
            this.chxDisplayItemsAttachedToTerrain.Enabled = false;
            this.chxDisplayItemsAttachedToTerrain.Location = new System.Drawing.Point(23, 538);
            this.chxDisplayItemsAttachedToTerrain.Margin = new System.Windows.Forms.Padding(4);
            this.chxDisplayItemsAttachedToTerrain.Name = "chxDisplayItemsAttachedToTerrain";
            this.chxDisplayItemsAttachedToTerrain.Size = new System.Drawing.Size(244, 21);
            this.chxDisplayItemsAttachedToTerrain.TabIndex = 8;
            this.chxDisplayItemsAttachedToTerrain.Text = "Display Items Attached To Terrain";
            this.chxDisplayItemsAttachedToTerrain.UseVisualStyleBackColor = true;
            // 
            // chxParticipationInTerrainHeight
            // 
            this.chxParticipationInTerrainHeight.AutoSize = true;
            this.chxParticipationInTerrainHeight.Enabled = false;
            this.chxParticipationInTerrainHeight.Location = new System.Drawing.Point(23, 510);
            this.chxParticipationInTerrainHeight.Margin = new System.Windows.Forms.Padding(4);
            this.chxParticipationInTerrainHeight.Name = "chxParticipationInTerrainHeight";
            this.chxParticipationInTerrainHeight.Size = new System.Drawing.Size(218, 21);
            this.chxParticipationInTerrainHeight.TabIndex = 5;
            this.chxParticipationInTerrainHeight.Text = "Participation In Terrain Height";
            this.chxParticipationInTerrainHeight.UseVisualStyleBackColor = true;
            // 
            // Tab_MeshSubPartTransform
            // 
            this.Tab_MeshSubPartTransform.Controls.Add(this.ctrlObjStatePropertySubPartOffset);
            this.Tab_MeshSubPartTransform.Controls.Add(this.ctrlObjStateSubPartInheritsParentRotation);
            this.Tab_MeshSubPartTransform.Controls.Add(this.ctrlObjStatePropertySubPartRotation);
            this.Tab_MeshSubPartTransform.Controls.Add(this.groupBox1);
            this.Tab_MeshSubPartTransform.Location = new System.Drawing.Point(4, 25);
            this.Tab_MeshSubPartTransform.Margin = new System.Windows.Forms.Padding(4);
            this.Tab_MeshSubPartTransform.Name = "Tab_MeshSubPartTransform";
            this.Tab_MeshSubPartTransform.Padding = new System.Windows.Forms.Padding(4);
            this.Tab_MeshSubPartTransform.Size = new System.Drawing.Size(1093, 820);
            this.Tab_MeshSubPartTransform.TabIndex = 1;
            this.Tab_MeshSubPartTransform.Text = "Sub-Part Transform";
            this.Tab_MeshSubPartTransform.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertySubPartOffset
            // 
            this.ctrlObjStatePropertySubPartOffset.AttachPointID = (DNMcConstants._MC_EMPTY_ID);
            this.ctrlObjStatePropertySubPartOffset.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySubPartOffset.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySubPartOffset.IsClickApply = false;
            this.ctrlObjStatePropertySubPartOffset.IsClickOK = false;
            this.ctrlObjStatePropertySubPartOffset.Location = new System.Drawing.Point(11, 174);
            this.ctrlObjStatePropertySubPartOffset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySubPartOffset.Name = "ctrlObjStatePropertySubPartOffset";
            this.ctrlObjStatePropertySubPartOffset.PropertyName = "Sub Part Offset";
            this.ctrlObjStatePropertySubPartOffset.PropertyValidationResult = null;
            this.ctrlObjStatePropertySubPartOffset.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySubPartOffset.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertySubPartOffset.Size = new System.Drawing.Size(527, 186);
            this.ctrlObjStatePropertySubPartOffset.TabIndex = 6;
            // 
            // ctrlObjStateSubPartInheritsParentRotation
            // 
            this.ctrlObjStateSubPartInheritsParentRotation.AttachPointID = (DNMcConstants._MC_EMPTY_ID);
            this.ctrlObjStateSubPartInheritsParentRotation.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStateSubPartInheritsParentRotation.CurrentObjectSchemeNode = null;
            this.ctrlObjStateSubPartInheritsParentRotation.IsClickApply = false;
            this.ctrlObjStateSubPartInheritsParentRotation.IsClickOK = false;
            this.ctrlObjStateSubPartInheritsParentRotation.Location = new System.Drawing.Point(551, 199);
            this.ctrlObjStateSubPartInheritsParentRotation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStateSubPartInheritsParentRotation.Name = "ctrlObjStateSubPartInheritsParentRotation";
            this.ctrlObjStateSubPartInheritsParentRotation.PropertyName = "Sub Part Inherits Parent Rotation";
            this.ctrlObjStateSubPartInheritsParentRotation.PropertyValidationResult = null;
            this.ctrlObjStateSubPartInheritsParentRotation.RegInheritsMeshRotation = false;
            this.ctrlObjStateSubPartInheritsParentRotation.RegPropertyID = ((uint)(0u));
            this.ctrlObjStateSubPartInheritsParentRotation.SelPropertyID = ((uint)(0u));
            this.ctrlObjStateSubPartInheritsParentRotation.Size = new System.Drawing.Size(527, 175);
            this.ctrlObjStateSubPartInheritsParentRotation.TabIndex = 5;
            // 
            // ctrlObjStatePropertySubPartRotation
            // 
            this.ctrlObjStatePropertySubPartRotation.AttachPointID = (DNMcConstants._MC_EMPTY_ID);
            this.ctrlObjStatePropertySubPartRotation.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySubPartRotation.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySubPartRotation.IsClickApply = false;
            this.ctrlObjStatePropertySubPartRotation.IsClickOK = false;
            this.ctrlObjStatePropertySubPartRotation.Location = new System.Drawing.Point(551, 7);
            this.ctrlObjStatePropertySubPartRotation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySubPartRotation.Name = "ctrlObjStatePropertySubPartRotation";
            this.ctrlObjStatePropertySubPartRotation.PropertyName = "Sub Part Rotation";
            this.ctrlObjStatePropertySubPartRotation.PropertyValidationResult = null;
            this.ctrlObjStatePropertySubPartRotation.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySubPartRotation.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySubPartRotation.Size = new System.Drawing.Size(527, 187);
            this.ctrlObjStatePropertySubPartRotation.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSelectedObject);
            this.groupBox1.Controls.Add(this.btnGetSubPartTransformCurrentRotation);
            this.groupBox1.Controls.Add(this.chxRelativeToParentSubPart);
            this.groupBox1.Controls.Add(this.ntxPropertyIDSubPart);
            this.groupBox1.Controls.Add(this.rdbAttachPointID);
            this.groupBox1.Controls.Add(this.ntxAttachPointID);
            this.groupBox1.Controls.Add(this.btnSubPartCurrRotationObjectList);
            this.groupBox1.Controls.Add(this.rdbPropertyID);
            this.groupBox1.Controls.Add(this.ctrl3DOrientationSubPartCurrRotation);
            this.groupBox1.Location = new System.Drawing.Point(11, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(533, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sub-Part Current Rotation";
            // 
            // lblSelectedObject
            // 
            this.lblSelectedObject.AutoSize = true;
            this.lblSelectedObject.Location = new System.Drawing.Point(120, 30);
            this.lblSelectedObject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedObject.Name = "lblSelectedObject";
            this.lblSelectedObject.Size = new System.Drawing.Size(89, 17);
            this.lblSelectedObject.TabIndex = 14;
            this.lblSelectedObject.Text = "Not Selected";
            // 
            // btnGetSubPartTransformCurrentRotation
            // 
            this.btnGetSubPartTransformCurrentRotation.Enabled = false;
            this.btnGetSubPartTransformCurrentRotation.Location = new System.Drawing.Point(456, 126);
            this.btnGetSubPartTransformCurrentRotation.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetSubPartTransformCurrentRotation.Name = "btnGetSubPartTransformCurrentRotation";
            this.btnGetSubPartTransformCurrentRotation.Size = new System.Drawing.Size(69, 28);
            this.btnGetSubPartTransformCurrentRotation.TabIndex = 12;
            this.btnGetSubPartTransformCurrentRotation.Text = "Get";
            this.btnGetSubPartTransformCurrentRotation.UseVisualStyleBackColor = true;
            this.btnGetSubPartTransformCurrentRotation.Click += new System.EventHandler(this.btnGetSubPartTransformCurrentRotation_Click);
            // 
            // chxRelativeToParentSubPart
            // 
            this.chxRelativeToParentSubPart.AutoSize = true;
            this.chxRelativeToParentSubPart.Location = new System.Drawing.Point(12, 97);
            this.chxRelativeToParentSubPart.Margin = new System.Windows.Forms.Padding(4);
            this.chxRelativeToParentSubPart.Name = "chxRelativeToParentSubPart";
            this.chxRelativeToParentSubPart.Size = new System.Drawing.Size(148, 21);
            this.chxRelativeToParentSubPart.TabIndex = 11;
            this.chxRelativeToParentSubPart.Text = "Relative To Parent";
            this.chxRelativeToParentSubPart.UseVisualStyleBackColor = true;
            // 
            // ntxPropertyIDSubPart
            // 
            this.ntxPropertyIDSubPart.Enabled = false;
            this.ntxPropertyIDSubPart.Location = new System.Drawing.Point(128, 60);
            this.ntxPropertyIDSubPart.Margin = new System.Windows.Forms.Padding(4);
            this.ntxPropertyIDSubPart.Name = "ntxPropertyIDSubPart";
            this.ntxPropertyIDSubPart.Size = new System.Drawing.Size(69, 22);
            this.ntxPropertyIDSubPart.TabIndex = 9;
            // 
            // rdbAttachPointID
            // 
            this.rdbAttachPointID.AutoSize = true;
            this.rdbAttachPointID.Location = new System.Drawing.Point(236, 62);
            this.rdbAttachPointID.Margin = new System.Windows.Forms.Padding(4);
            this.rdbAttachPointID.Name = "rdbAttachPointID";
            this.rdbAttachPointID.Size = new System.Drawing.Size(126, 21);
            this.rdbAttachPointID.TabIndex = 2;
            this.rdbAttachPointID.TabStop = true;
            this.rdbAttachPointID.Text = "Attach Point ID:";
            this.rdbAttachPointID.UseVisualStyleBackColor = true;
            // 
            // ntxAttachPointID
            // 
            this.ntxAttachPointID.Enabled = false;
            this.ntxAttachPointID.Location = new System.Drawing.Point(377, 60);
            this.ntxAttachPointID.Margin = new System.Windows.Forms.Padding(4);
            this.ntxAttachPointID.Name = "ntxAttachPointID";
            this.ntxAttachPointID.Size = new System.Drawing.Size(69, 22);
            this.ntxAttachPointID.TabIndex = 8;
            // 
            // btnSubPartCurrRotationObjectList
            // 
            this.btnSubPartCurrRotationObjectList.Location = new System.Drawing.Point(12, 23);
            this.btnSubPartCurrRotationObjectList.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubPartCurrRotationObjectList.Name = "btnSubPartCurrRotationObjectList";
            this.btnSubPartCurrRotationObjectList.Size = new System.Drawing.Size(100, 28);
            this.btnSubPartCurrRotationObjectList.TabIndex = 3;
            this.btnSubPartCurrRotationObjectList.Text = "Object";
            this.btnSubPartCurrRotationObjectList.UseVisualStyleBackColor = true;
            this.btnSubPartCurrRotationObjectList.Click += new System.EventHandler(this.btnOpenObjList_Click);
            // 
            // rdbPropertyID
            // 
            this.rdbPropertyID.AutoSize = true;
            this.rdbPropertyID.Location = new System.Drawing.Point(12, 62);
            this.rdbPropertyID.Margin = new System.Windows.Forms.Padding(4);
            this.rdbPropertyID.Name = "rdbPropertyID";
            this.rdbPropertyID.Size = new System.Drawing.Size(104, 21);
            this.rdbPropertyID.TabIndex = 1;
            this.rdbPropertyID.TabStop = true;
            this.rdbPropertyID.Text = "Property ID:";
            this.rdbPropertyID.UseVisualStyleBackColor = true;
            // 
            // ctrl3DOrientationSubPartCurrRotation
            // 
            this.ctrl3DOrientationSubPartCurrRotation.Enabled = false;
            this.ctrl3DOrientationSubPartCurrRotation.Location = new System.Drawing.Point(12, 126);
            this.ctrl3DOrientationSubPartCurrRotation.Margin = new System.Windows.Forms.Padding(5);
            this.ctrl3DOrientationSubPartCurrRotation.Name = "ctrl3DOrientationSubPartCurrRotation";
            this.ctrl3DOrientationSubPartCurrRotation.Pitch = 0F;
            this.ctrl3DOrientationSubPartCurrRotation.Roll = 0F;
            this.ctrl3DOrientationSubPartCurrRotation.Size = new System.Drawing.Size(385, 27);
            this.ctrl3DOrientationSubPartCurrRotation.TabIndex = 0;
            this.ctrl3DOrientationSubPartCurrRotation.Yaw = 0F;
            // 
            // ntxPropertyID
            // 
            this.ntxPropertyID.Location = new System.Drawing.Point(96, 19);
            this.ntxPropertyID.Name = "ntxPropertyID";
            this.ntxPropertyID.Size = new System.Drawing.Size(53, 22);
            this.ntxPropertyID.TabIndex = 5;
            // 
            // chxRelativeToParent
            // 
            this.chxRelativeToParent.AutoSize = true;
            this.chxRelativeToParent.Location = new System.Drawing.Point(9, 45);
            this.chxRelativeToParent.Name = "chxRelativeToParent";
            this.chxRelativeToParent.Size = new System.Drawing.Size(115, 17);
            this.chxRelativeToParent.TabIndex = 2;
            this.chxRelativeToParent.Text = "Relative To Parent";
            this.chxRelativeToParent.UseVisualStyleBackColor = true;
            // 
            // chxRelativeToCurrOrientation
            // 
            this.chxRelativeToCurrOrientation.AutoSize = true;
            this.chxRelativeToCurrOrientation.Location = new System.Drawing.Point(9, 68);
            this.chxRelativeToCurrOrientation.Name = "chxRelativeToCurrOrientation";
            this.chxRelativeToCurrOrientation.Size = new System.Drawing.Size(157, 17);
            this.chxRelativeToCurrOrientation.TabIndex = 1;
            this.chxRelativeToCurrOrientation.Text = "Relative To Curr Orientation";
            this.chxRelativeToCurrOrientation.UseVisualStyleBackColor = true;
            // 
            // chxStatic
            // 
            this.chxStatic.AutoSize = true;
            this.chxStatic.Enabled = false;
            this.chxStatic.Location = new System.Drawing.Point(23, 567);
            this.chxStatic.Margin = new System.Windows.Forms.Padding(4);
            this.chxStatic.Name = "chxStatic";
            this.chxStatic.Size = new System.Drawing.Size(65, 21);
            this.chxStatic.TabIndex = 14;
            this.chxStatic.Text = "Static";
            this.chxStatic.UseVisualStyleBackColor = true;
            // 
            // ucMeshItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ucMeshItem";
            this.Load += new System.EventHandler(this.ucMeshItem_Load);
            this.tabControl1.ResumeLayout(false);
            this.Tab_MeshItem.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.Tab_MeshGeneral.ResumeLayout(false);
            this.Tab_MeshGeneral.PerformLayout();
            this.Tab_MeshSubPartTransform.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage Tab_MeshGeneral;
        private System.Windows.Forms.TabPage Tab_MeshSubPartTransform;
        protected System.Windows.Forms.TabPage Tab_MeshItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private MCTester.Controls.NumericTextBox ntxPropertyID;
        private System.Windows.Forms.Button btnSubPartCurrRotationObjectList;
        private System.Windows.Forms.CheckBox chxRelativeToParent;
        private System.Windows.Forms.CheckBox chxRelativeToCurrOrientation;
        private MCTester.Controls.Ctrl3DOrientation ctrl3DOrientationSubPartCurrRotation;
        private System.Windows.Forms.RadioButton rdbAttachPointID;
        private MCTester.Controls.NumericTextBox ntxAttachPointID;
        private System.Windows.Forms.RadioButton rdbPropertyID;
        private MCTester.Controls.NumericTextBox ntxPropertyIDSubPart;
        private System.Windows.Forms.CheckBox chxRelativeToParentSubPart;
        private System.Windows.Forms.Button btnGetSubPartTransformCurrentRotation;
        private System.Windows.Forms.Label lblSelectedObject;
        private System.Windows.Forms.CheckBox chxParticipationInTerrainHeight;
        private System.Windows.Forms.CheckBox chxDisplayItemsAttachedToTerrain;
        private Controls.CtrlObjStatePropertyEBasePointAlignment ctrlObjStatePropertyBasePointAlignment;
        private Controls.CtrlObjStatePropertyBool ctrlObjStatePropertyCastShadows;
        private Controls.CtrlObjStatePropertyAnimation ctrlObjStatePropertyAnimation;
        private Controls.CtrlObjStatePropertyTextureScroll ctrlObjStatePropertyTextureScroll;
        private Controls.CtrlObjStatePropertyMesh ctrlObjStatePropertyMesh;
        private Controls.CtrlObjStatePropertySubPartRotation ctrlObjStatePropertySubPartRotation;
        private Controls.CtrlObjStatePropertyDictionaryBool ctrlObjStateSubPartInheritsParentRotation;
        private Controls.CtrlObjStatePropertySubPartOffset ctrlObjStatePropertySubPartOffset;
        private System.Windows.Forms.CheckBox chxStatic;
    }
}
