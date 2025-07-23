namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucObjectSchemeNode
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Tab_ObjectScehemeNode = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbObjects = new System.Windows.Forms.ComboBox();
            this.txtGeometryCoordinateSystem = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnShowBasePointsCoordinates = new System.Windows.Forms.Button();
            this.ntxNodeID = new MCTester.Controls.NumericTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNodeType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNodeKind = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ctrlObjStatePropertyDicSelectorCond = new MCTester.Controls.CtrlObjStatePropertyDicConditionalSelector();
            this.ctrlObjStatePropertyTransformOption = new MCTester.Controls.CtrlObjStatePropertyEActionOptions();
            this.ctrlObjStatePropertyVisibilityOption = new MCTester.Controls.CtrlObjStatePropertyEActionOptions();
            this.ctrlBoundingRectWorld = new MCTester.Controls.CtrlBoundingRect();
            this.ctrlBoundingRectScreen = new MCTester.Controls.CtrlBoundingRect();
            this.gbEffectiveVisibilityInViewport = new System.Windows.Forms.GroupBox();
            this.chxEffectiveVisibilityInViewport = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnEffectiveVisibilityObject = new System.Windows.Forms.Button();
            this.ctrlUserData = new MCTester.Controls.ctrlUserData();
            this.btnIDList = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.ctrlSaveParams1 = new MCTester.ObjectWorld.ObjectsUserControls.ctrlSaveParams();
            this.tabControl1.SuspendLayout();
            this.Tab_ObjectScehemeNode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbEffectiveVisibilityInViewport.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_ObjectScehemeNode);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(838, 850);
            this.tabControl1.TabIndex = 61;
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Controls.Add(this.groupBox1);
            this.Tab_ObjectScehemeNode.Controls.Add(this.btnShowBasePointsCoordinates);
            this.Tab_ObjectScehemeNode.Controls.Add(this.ntxNodeID);
            this.Tab_ObjectScehemeNode.Controls.Add(this.label7);
            this.Tab_ObjectScehemeNode.Controls.Add(this.txtNodeType);
            this.Tab_ObjectScehemeNode.Controls.Add(this.label1);
            this.Tab_ObjectScehemeNode.Controls.Add(this.txtNodeKind);
            this.Tab_ObjectScehemeNode.Controls.Add(this.label6);
            this.Tab_ObjectScehemeNode.Controls.Add(this.ctrlObjStatePropertyDicSelectorCond);
            this.Tab_ObjectScehemeNode.Controls.Add(this.ctrlObjStatePropertyTransformOption);
            this.Tab_ObjectScehemeNode.Controls.Add(this.ctrlObjStatePropertyVisibilityOption);
            this.Tab_ObjectScehemeNode.Controls.Add(this.ctrlBoundingRectWorld);
            this.Tab_ObjectScehemeNode.Controls.Add(this.ctrlBoundingRectScreen);
            this.Tab_ObjectScehemeNode.Controls.Add(this.gbEffectiveVisibilityInViewport);
            this.Tab_ObjectScehemeNode.Controls.Add(this.ctrlUserData);
            this.Tab_ObjectScehemeNode.Location = new System.Drawing.Point(4, 22);
            this.Tab_ObjectScehemeNode.Name = "Tab_ObjectScehemeNode";
            this.Tab_ObjectScehemeNode.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_ObjectScehemeNode.Size = new System.Drawing.Size(830, 824);
            this.Tab_ObjectScehemeNode.TabIndex = 0;
            this.Tab_ObjectScehemeNode.Text = "Object Scheme Node";
            this.Tab_ObjectScehemeNode.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbObjects);
            this.groupBox1.Controls.Add(this.txtGeometryCoordinateSystem);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(412, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 73);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Geometry Coordinate System";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Object:";
            // 
            // cbObjects
            // 
            this.cbObjects.FormattingEnabled = true;
            this.cbObjects.Location = new System.Drawing.Point(175, 19);
            this.cbObjects.Name = "cbObjects";
            this.cbObjects.Size = new System.Drawing.Size(219, 21);
            this.cbObjects.TabIndex = 82;
            this.cbObjects.SelectedIndexChanged += new System.EventHandler(this.cbObjects_SelectedIndexChanged);
            // 
            // txtGeometryCoordinateSystem
            // 
            this.txtGeometryCoordinateSystem.Enabled = false;
            this.txtGeometryCoordinateSystem.Location = new System.Drawing.Point(175, 48);
            this.txtGeometryCoordinateSystem.Margin = new System.Windows.Forms.Padding(2);
            this.txtGeometryCoordinateSystem.Name = "txtGeometryCoordinateSystem";
            this.txtGeometryCoordinateSystem.Size = new System.Drawing.Size(219, 20);
            this.txtGeometryCoordinateSystem.TabIndex = 81;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 51);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 13);
            this.label8.TabIndex = 80;
            this.label8.Text = "Geometry Coordinate System:";
            // 
            // btnShowBasePointsCoordinates
            // 
            this.btnShowBasePointsCoordinates.Location = new System.Drawing.Point(7, 558);
            this.btnShowBasePointsCoordinates.Name = "btnShowBasePointsCoordinates";
            this.btnShowBasePointsCoordinates.Size = new System.Drawing.Size(178, 23);
            this.btnShowBasePointsCoordinates.TabIndex = 83;
            this.btnShowBasePointsCoordinates.Text = "Show base points’ coordinates                  ";
            this.btnShowBasePointsCoordinates.UseVisualStyleBackColor = true;
            this.btnShowBasePointsCoordinates.Click += new System.EventHandler(this.btnShowBasePointsCoordinates_Click);
            // 
            // ntxNodeID
            // 
            this.ntxNodeID.Location = new System.Drawing.Point(91, 65);
            this.ntxNodeID.Margin = new System.Windows.Forms.Padding(2);
            this.ntxNodeID.Name = "ntxNodeID";
            this.ntxNodeID.Size = new System.Drawing.Size(152, 20);
            this.ntxNodeID.TabIndex = 82;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 67);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 78;
            this.label7.Text = "Node ID:";
            // 
            // txtNodeType
            // 
            this.txtNodeType.Enabled = false;
            this.txtNodeType.Location = new System.Drawing.Point(91, 38);
            this.txtNodeType.Margin = new System.Windows.Forms.Padding(2);
            this.txtNodeType.Name = "txtNodeType";
            this.txtNodeType.Size = new System.Drawing.Size(152, 20);
            this.txtNodeType.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Node Type:";
            // 
            // txtNodeKind
            // 
            this.txtNodeKind.Enabled = false;
            this.txtNodeKind.Location = new System.Drawing.Point(91, 12);
            this.txtNodeKind.Margin = new System.Windows.Forms.Padding(2);
            this.txtNodeKind.Name = "txtNodeKind";
            this.txtNodeKind.Size = new System.Drawing.Size(152, 20);
            this.txtNodeKind.TabIndex = 75;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 74;
            this.label6.Text = "Node Kind:";
            // 
            // ctrlObjStatePropertyDicSelectorCond
            // 
            this.ctrlObjStatePropertyDicSelectorCond.ActionTypeKey = MapCore.DNEActionType._EAT_ACTIVITY;
            this.ctrlObjStatePropertyDicSelectorCond.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyDicSelectorCond.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyDicSelectorCond.CurrObject = null;
            this.ctrlObjStatePropertyDicSelectorCond.IsClickApply = false;
            this.ctrlObjStatePropertyDicSelectorCond.IsClickOK = false;
            this.ctrlObjStatePropertyDicSelectorCond.IsInObjectLocation = false;
            this.ctrlObjStatePropertyDicSelectorCond.Location = new System.Drawing.Point(412, 211);
            this.ctrlObjStatePropertyDicSelectorCond.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyDicSelectorCond.Name = "ctrlObjStatePropertyDicSelectorCond";
            this.ctrlObjStatePropertyDicSelectorCond.PropertyName = "Conditional Selector";
            this.ctrlObjStatePropertyDicSelectorCond.PropertyValidationResult = null;
            this.ctrlObjStatePropertyDicSelectorCond.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyDicSelectorCond.SelectorsArr = null;
            this.ctrlObjStatePropertyDicSelectorCond.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyDicSelectorCond.Size = new System.Drawing.Size(400, 229);
            this.ctrlObjStatePropertyDicSelectorCond.TabIndex = 72;
            // 
            // ctrlObjStatePropertyTransformOption
            // 
            this.ctrlObjStatePropertyTransformOption.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyTransformOption.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyTransformOption.EnumType = "";
            this.ctrlObjStatePropertyTransformOption.IsClickApply = false;
            this.ctrlObjStatePropertyTransformOption.IsClickOK = false;
            this.ctrlObjStatePropertyTransformOption.Location = new System.Drawing.Point(6, 250);
            this.ctrlObjStatePropertyTransformOption.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyTransformOption.Name = "ctrlObjStatePropertyTransformOption";
            this.ctrlObjStatePropertyTransformOption.PropertyName = "Transform Option";
            this.ctrlObjStatePropertyTransformOption.PropertyValidationResult = null;
            this.ctrlObjStatePropertyTransformOption.RegEnumVal = MapCore.DNEActionOptions._EAO_FORCE_FALSE;
            this.ctrlObjStatePropertyTransformOption.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyTransformOption.SelEnumVal = MapCore.DNEActionOptions._EAO_FORCE_FALSE;
            this.ctrlObjStatePropertyTransformOption.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyTransformOption.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyTransformOption.TabIndex = 70;
            // 
            // ctrlObjStatePropertyVisibilityOption
            // 
            this.ctrlObjStatePropertyVisibilityOption.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertyVisibilityOption.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertyVisibilityOption.EnumType = "";
            this.ctrlObjStatePropertyVisibilityOption.IsClickApply = false;
            this.ctrlObjStatePropertyVisibilityOption.IsClickOK = false;
            this.ctrlObjStatePropertyVisibilityOption.Location = new System.Drawing.Point(6, 117);
            this.ctrlObjStatePropertyVisibilityOption.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlObjStatePropertyVisibilityOption.Name = "ctrlObjStatePropertyVisibilityOption";
            this.ctrlObjStatePropertyVisibilityOption.PropertyName = "Visibility Option";
            this.ctrlObjStatePropertyVisibilityOption.PropertyValidationResult = null;
            this.ctrlObjStatePropertyVisibilityOption.RegEnumVal = MapCore.DNEActionOptions._EAO_FORCE_FALSE;
            this.ctrlObjStatePropertyVisibilityOption.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertyVisibilityOption.SelEnumVal = MapCore.DNEActionOptions._EAO_FORCE_FALSE;
            this.ctrlObjStatePropertyVisibilityOption.SelPropertyID = ((uint)(4294967295u));
            this.ctrlObjStatePropertyVisibilityOption.Size = new System.Drawing.Size(400, 130);
            this.ctrlObjStatePropertyVisibilityOption.TabIndex = 69;
            // 
            // ctrlBoundingRectWorld
            // 
            this.ctrlBoundingRectWorld.BoundingRectSchemeNode = null;
            this.ctrlBoundingRectWorld.IsScreenBoundingBox = false;
            this.ctrlBoundingRectWorld.Location = new System.Drawing.Point(6, 470);
            this.ctrlBoundingRectWorld.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBoundingRectWorld.Name = "ctrlBoundingRectWorld";
            this.ctrlBoundingRectWorld.Size = new System.Drawing.Size(400, 80);
            this.ctrlBoundingRectWorld.TabIndex = 66;
            this.ctrlBoundingRectWorld.Title = "World Bounding Box";
            // 
            // ctrlBoundingRectScreen
            // 
            this.ctrlBoundingRectScreen.BoundingRectSchemeNode = null;
            this.ctrlBoundingRectScreen.IsScreenBoundingBox = true;
            this.ctrlBoundingRectScreen.Location = new System.Drawing.Point(7, 386);
            this.ctrlBoundingRectScreen.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlBoundingRectScreen.Name = "ctrlBoundingRectScreen";
            this.ctrlBoundingRectScreen.Size = new System.Drawing.Size(400, 80);
            this.ctrlBoundingRectScreen.TabIndex = 65;
            this.ctrlBoundingRectScreen.Title = "Screen Bounding Rectangle";
            // 
            // gbEffectiveVisibilityInViewport
            // 
            this.gbEffectiveVisibilityInViewport.Controls.Add(this.chxEffectiveVisibilityInViewport);
            this.gbEffectiveVisibilityInViewport.Controls.Add(this.label4);
            this.gbEffectiveVisibilityInViewport.Controls.Add(this.btnEffectiveVisibilityObject);
            this.gbEffectiveVisibilityInViewport.Location = new System.Drawing.Point(412, 445);
            this.gbEffectiveVisibilityInViewport.Name = "gbEffectiveVisibilityInViewport";
            this.gbEffectiveVisibilityInViewport.Size = new System.Drawing.Size(400, 77);
            this.gbEffectiveVisibilityInViewport.TabIndex = 64;
            this.gbEffectiveVisibilityInViewport.TabStop = false;
            this.gbEffectiveVisibilityInViewport.Text = "Effective Visibility In Viewport";
            // 
            // chxEffectiveVisibilityInViewport
            // 
            this.chxEffectiveVisibilityInViewport.AutoSize = true;
            this.chxEffectiveVisibilityInViewport.Enabled = false;
            this.chxEffectiveVisibilityInViewport.Location = new System.Drawing.Point(9, 53);
            this.chxEffectiveVisibilityInViewport.Name = "chxEffectiveVisibilityInViewport";
            this.chxEffectiveVisibilityInViewport.Size = new System.Drawing.Size(67, 17);
            this.chxEffectiveVisibilityInViewport.TabIndex = 66;
            this.chxEffectiveVisibilityInViewport.Text = "Is Visible";
            this.chxEffectiveVisibilityInViewport.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 64;
            this.label4.Text = "Choose Object and Viewport:";
            // 
            // btnEffectiveVisibilityObject
            // 
            this.btnEffectiveVisibilityObject.Location = new System.Drawing.Point(157, 20);
            this.btnEffectiveVisibilityObject.Name = "btnEffectiveVisibilityObject";
            this.btnEffectiveVisibilityObject.Size = new System.Drawing.Size(32, 23);
            this.btnEffectiveVisibilityObject.TabIndex = 63;
            this.btnEffectiveVisibilityObject.Text = "...";
            this.btnEffectiveVisibilityObject.UseVisualStyleBackColor = true;
            this.btnEffectiveVisibilityObject.Click += new System.EventHandler(this.btnEffectiveVisibilityObject_Click);
            // 
            // ctrlUserData
            // 
            this.ctrlUserData.Location = new System.Drawing.Point(412, 116);
            this.ctrlUserData.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlUserData.Name = "ctrlUserData";
            this.ctrlUserData.Size = new System.Drawing.Size(400, 90);
            this.ctrlUserData.TabIndex = 6;
            this.ctrlUserData.UserDataByte = new byte[0];
            this.ctrlUserData.UserDateText = "System.Byte[]";
            // 
            // btnIDList
            // 
            this.btnIDList.Location = new System.Drawing.Point(148, 856);
            this.btnIDList.Name = "btnIDList";
            this.btnIDList.Size = new System.Drawing.Size(35, 23);
            this.btnIDList.TabIndex = 61;
            this.btnIDList.Text = "-->";
            this.btnIDList.UseVisualStyleBackColor = true;
            this.btnIDList.Click += new System.EventHandler(this.btnIDList_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(759, 882);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 62;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(678, 882);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 63;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.Location = new System.Drawing.Point(597, 882);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 64;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 861);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 13);
            this.label14.TabIndex = 63;
            this.label14.Text = "Scheme Properties ID List:";
            // 
            // ctrlSaveParams1
            // 
            this.ctrlSaveParams1.Location = new System.Drawing.Point(8, 880);
            this.ctrlSaveParams1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlSaveParams1.Name = "ctrlSaveParams1";
            this.ctrlSaveParams1.Size = new System.Drawing.Size(517, 28);
            this.ctrlSaveParams1.TabIndex = 65;
            // 
            // ucObjectSchemeNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.ctrlSaveParams1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnIDList);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.Name = "ucObjectSchemeNode";
            this.Size = new System.Drawing.Size(838, 910);
            this.tabControl1.ResumeLayout(false);
            this.Tab_ObjectScehemeNode.ResumeLayout(false);
            this.Tab_ObjectScehemeNode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEffectiveVisibilityInViewport.ResumeLayout(false);
            this.gbEffectiveVisibilityInViewport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MCTester.Controls.ctrlUserData ctrlUserData;
        protected System.Windows.Forms.TabPage Tab_ObjectScehemeNode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnIDList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox gbEffectiveVisibilityInViewport;
        private System.Windows.Forms.CheckBox chxEffectiveVisibilityInViewport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnEffectiveVisibilityObject;
        private MCTester.Controls.CtrlBoundingRect ctrlBoundingRectScreen;
        private MCTester.Controls.CtrlBoundingRect ctrlBoundingRectWorld;
        private Controls.CtrlObjStatePropertyEActionOptions ctrlObjStatePropertyVisibilityOption;
        private Controls.CtrlObjStatePropertyEActionOptions ctrlObjStatePropertyTransformOption;
        private Controls.CtrlObjStatePropertyDicConditionalSelector ctrlObjStatePropertyDicSelectorCond;
        private ctrlSaveParams ctrlSaveParams1;
        private System.Windows.Forms.TextBox txtNodeKind;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGeometryCoordinateSystem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNodeType;
        private System.Windows.Forms.Label label1;
        private Controls.NumericTextBox ntxNodeID;
        private System.Windows.Forms.Button btnShowBasePointsCoordinates;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbObjects;
    }
}
