namespace MCTester.ObjectWorld.OverlayManagerWorld
{
	partial class ucCollection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCollection));
            this.chkCollectionVisibility = new System.Windows.Forms.CheckBox();
            this.btnCollectionVisibilityApply = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.clstCollectionVisibility = new System.Windows.Forms.CheckedListBox();
            this.gbObjects = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbState = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnStateApply = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnClearObjectsViewportSelection = new System.Windows.Forms.Button();
            this.lstObjectsViewports = new System.Windows.Forms.ListBox();
            this.btnObjectsVisbilityApply = new System.Windows.Forms.Button();
            this.btnMoveAllObjOK = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbObjectsVisibility = new System.Windows.Forms.ComboBox();
            this.ctrl3DVectorMoveAllObj = new MCTester.Controls.Ctrl3DVector();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRemoveObjectsFromTheirOverlays = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSelectAllObjects = new System.Windows.Forms.CheckBox();
            this.btnRemoveObj = new System.Windows.Forms.Button();
            this.lstCollectionObjects = new System.Windows.Forms.ListBox();
            this.btnAddObject = new System.Windows.Forms.Button();
            this.lstObjects = new System.Windows.Forms.ListBox();
            this.btnCollectionClose = new System.Windows.Forms.Button();
            this.gbOverlays = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClearOverlaysViewportSelection = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lstOverlaysViewports = new System.Windows.Forms.ListBox();
            this.btnOverlaysVisbilityApply = new System.Windows.Forms.Button();
            this.cmbOverlaysVisibility = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRemoveOverlaysFromOM = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRemoveOverlay = new System.Windows.Forms.Button();
            this.lstCollectionOverlay = new System.Windows.Forms.ListBox();
            this.btnAddOverlay = new System.Windows.Forms.Button();
            this.lstOverlay = new System.Windows.Forms.ListBox();
            this.tcCollection = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.tpCollection = new System.Windows.Forms.TabPage();
            this.btnClearCollection = new System.Windows.Forms.Button();
            this.gbObjects.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbOverlays.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tcCollection.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpCollection.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkCollectionVisibility
            // 
            this.chkCollectionVisibility.AutoSize = true;
            this.chkCollectionVisibility.Location = new System.Drawing.Point(6, 15);
            this.chkCollectionVisibility.Name = "chkCollectionVisibility";
            this.chkCollectionVisibility.Size = new System.Drawing.Size(111, 17);
            this.chkCollectionVisibility.TabIndex = 0;
            this.chkCollectionVisibility.Text = "Collection Visibility";
            this.chkCollectionVisibility.UseVisualStyleBackColor = true;
            // 
            // btnCollectionVisibilityApply
            // 
            this.btnCollectionVisibilityApply.Location = new System.Drawing.Point(123, 11);
            this.btnCollectionVisibilityApply.Name = "btnCollectionVisibilityApply";
            this.btnCollectionVisibilityApply.Size = new System.Drawing.Size(42, 23);
            this.btnCollectionVisibilityApply.TabIndex = 110;
            this.btnCollectionVisibilityApply.Text = "Apply";
            this.btnCollectionVisibilityApply.UseVisualStyleBackColor = true;
            this.btnCollectionVisibilityApply.Click += new System.EventHandler(this.btnCollectionVisibilityApply_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label10.Location = new System.Drawing.Point(3, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 13);
            this.label10.TabIndex = 30;
            this.label10.Text = "Collection Visibility:";
            // 
            // clstCollectionVisibility
            // 
            this.clstCollectionVisibility.CheckOnClick = true;
            this.clstCollectionVisibility.FormattingEnabled = true;
            this.clstCollectionVisibility.HorizontalScrollbar = true;
            this.clstCollectionVisibility.Location = new System.Drawing.Point(6, 65);
            this.clstCollectionVisibility.Name = "clstCollectionVisibility";
            this.clstCollectionVisibility.Size = new System.Drawing.Size(205, 139);
            this.clstCollectionVisibility.TabIndex = 29;
            this.clstCollectionVisibility.SelectedValueChanged += new System.EventHandler(this.clstCollectionVisibilityInViewport_SelectedValueChanged);
            // 
            // gbObjects
            // 
            this.gbObjects.Controls.Add(this.groupBox1);
            this.gbObjects.Controls.Add(this.btnRemoveObjectsFromTheirOverlays);
            this.gbObjects.Controls.Add(this.label8);
            this.gbObjects.Controls.Add(this.label7);
            this.gbObjects.Controls.Add(this.chkSelectAllObjects);
            this.gbObjects.Controls.Add(this.btnRemoveObj);
            this.gbObjects.Controls.Add(this.lstCollectionObjects);
            this.gbObjects.Controls.Add(this.btnAddObject);
            this.gbObjects.Controls.Add(this.lstObjects);
            this.gbObjects.Location = new System.Drawing.Point(3, 32);
            this.gbObjects.Name = "gbObjects";
            this.gbObjects.Size = new System.Drawing.Size(400, 559);
            this.gbObjects.TabIndex = 8;
            this.gbObjects.TabStop = false;
            this.gbObjects.Text = "Objects";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbState);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btnStateApply);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnClearObjectsViewportSelection);
            this.groupBox1.Controls.Add(this.lstObjectsViewports);
            this.groupBox1.Controls.Add(this.btnObjectsVisbilityApply);
            this.groupBox1.Controls.Add(this.btnMoveAllObjOK);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbObjectsVisibility);
            this.groupBox1.Controls.Add(this.ctrl3DVectorMoveAllObj);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(-3, 311);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(398, 232);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Objects operations";
            // 
            // tbState
            // 
            this.tbState.Location = new System.Drawing.Point(62, 84);
            this.tbState.Name = "tbState";
            this.tbState.Size = new System.Drawing.Size(111, 20);
            this.tbState.TabIndex = 133;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 86);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 13);
            this.label13.TabIndex = 132;
            this.label13.Text = "State:";
            // 
            // btnStateApply
            // 
            this.btnStateApply.Location = new System.Drawing.Point(178, 81);
            this.btnStateApply.Name = "btnStateApply";
            this.btnStateApply.Size = new System.Drawing.Size(41, 23);
            this.btnStateApply.TabIndex = 131;
            this.btnStateApply.Text = "Apply";
            this.btnStateApply.UseVisualStyleBackColor = true;
            this.btnStateApply.Click += new System.EventHandler(this.btnStateApply_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label11.Location = new System.Drawing.Point(227, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 28);
            this.label11.TabIndex = 127;
            this.label11.Text = "Viewports (Clear selection to set for all viewport)";
            // 
            // btnClearObjectsViewportSelection
            // 
            this.btnClearObjectsViewportSelection.Location = new System.Drawing.Point(179, 147);
            this.btnClearObjectsViewportSelection.Name = "btnClearObjectsViewportSelection";
            this.btnClearObjectsViewportSelection.Size = new System.Drawing.Size(40, 23);
            this.btnClearObjectsViewportSelection.TabIndex = 126;
            this.btnClearObjectsViewportSelection.Text = "Clear";
            this.btnClearObjectsViewportSelection.UseVisualStyleBackColor = true;
            this.btnClearObjectsViewportSelection.Click += new System.EventHandler(this.btnClearObjectsViewportSelection_Click);
            // 
            // lstObjectsViewports
            // 
            this.lstObjectsViewports.FormattingEnabled = true;
            this.lstObjectsViewports.Location = new System.Drawing.Point(226, 50);
            this.lstObjectsViewports.Name = "lstObjectsViewports";
            this.lstObjectsViewports.Size = new System.Drawing.Size(170, 121);
            this.lstObjectsViewports.TabIndex = 125;
            this.lstObjectsViewports.SelectedIndexChanged += new System.EventHandler(this.lstObjectsViewports_SelectedIndexChanged);
            // 
            // btnObjectsVisbilityApply
            // 
            this.btnObjectsVisbilityApply.Location = new System.Drawing.Point(178, 51);
            this.btnObjectsVisbilityApply.Name = "btnObjectsVisbilityApply";
            this.btnObjectsVisbilityApply.Size = new System.Drawing.Size(42, 23);
            this.btnObjectsVisbilityApply.TabIndex = 123;
            this.btnObjectsVisbilityApply.Text = "Apply";
            this.btnObjectsVisbilityApply.UseVisualStyleBackColor = true;
            this.btnObjectsVisbilityApply.Click += new System.EventHandler(this.btnObjectsVisbilityApply_Click);
            // 
            // btnMoveAllObjOK
            // 
            this.btnMoveAllObjOK.Location = new System.Drawing.Point(344, 201);
            this.btnMoveAllObjOK.Name = "btnMoveAllObjOK";
            this.btnMoveAllObjOK.Size = new System.Drawing.Size(32, 21);
            this.btnMoveAllObjOK.TabIndex = 118;
            this.btnMoveAllObjOK.Text = "OK";
            this.btnMoveAllObjOK.UseVisualStyleBackColor = true;
            this.btnMoveAllObjOK.Click += new System.EventHandler(this.btnMoveAllObjOK_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 117;
            this.label3.Text = "Move All Objects:";
            // 
            // cmbObjectsVisibility
            // 
            this.cmbObjectsVisibility.FormattingEnabled = true;
            this.cmbObjectsVisibility.Location = new System.Drawing.Point(62, 53);
            this.cmbObjectsVisibility.Name = "cmbObjectsVisibility";
            this.cmbObjectsVisibility.Size = new System.Drawing.Size(111, 21);
            this.cmbObjectsVisibility.TabIndex = 120;
            // 
            // ctrl3DVectorMoveAllObj
            // 
            this.ctrl3DVectorMoveAllObj.IsReadOnly = false;
            this.ctrl3DVectorMoveAllObj.Location = new System.Drawing.Point(106, 199);
            this.ctrl3DVectorMoveAllObj.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ctrl3DVectorMoveAllObj.Name = "ctrl3DVectorMoveAllObj";
            this.ctrl3DVectorMoveAllObj.Size = new System.Drawing.Size(232, 26);
            this.ctrl3DVectorMoveAllObj.TabIndex = 116;
            this.ctrl3DVectorMoveAllObj.X = 0D;
            this.ctrl3DVectorMoveAllObj.Y = 0D;
            this.ctrl3DVectorMoveAllObj.Z = 0D;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "Visiblility:";
            // 
            // btnRemoveObjectsFromTheirOverlays
            // 
            this.btnRemoveObjectsFromTheirOverlays.Location = new System.Drawing.Point(7, 256);
            this.btnRemoveObjectsFromTheirOverlays.Name = "btnRemoveObjectsFromTheirOverlays";
            this.btnRemoveObjectsFromTheirOverlays.Size = new System.Drawing.Size(196, 23);
            this.btnRemoveObjectsFromTheirOverlays.TabIndex = 29;
            this.btnRemoveObjectsFromTheirOverlays.Text = "Remove Objects From Their Overlays";
            this.btnRemoveObjectsFromTheirOverlays.UseVisualStyleBackColor = true;
            this.btnRemoveObjectsFromTheirOverlays.Click += new System.EventHandler(this.btnRemoveObjectsFromTheirOverlays_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(4, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Collection Objects (Multi Select):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(224, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Object List (Multi Select):";
            // 
            // chkSelectAllObjects
            // 
            this.chkSelectAllObjects.AutoSize = true;
            this.chkSelectAllObjects.Location = new System.Drawing.Point(7, 285);
            this.chkSelectAllObjects.Name = "chkSelectAllObjects";
            this.chkSelectAllObjects.Size = new System.Drawing.Size(109, 17);
            this.chkSelectAllObjects.TabIndex = 19;
            this.chkSelectAllObjects.Text = "Select All Objects";
            this.chkSelectAllObjects.UseVisualStyleBackColor = true;
            this.chkSelectAllObjects.CheckedChanged += new System.EventHandler(this.chkSelectAllObjects_CheckedChanged);
            // 
            // btnRemoveObj
            // 
            this.btnRemoveObj.Location = new System.Drawing.Point(176, 142);
            this.btnRemoveObj.Name = "btnRemoveObj";
            this.btnRemoveObj.Size = new System.Drawing.Size(45, 21);
            this.btnRemoveObj.TabIndex = 14;
            this.btnRemoveObj.Text = ">";
            this.btnRemoveObj.UseVisualStyleBackColor = true;
            this.btnRemoveObj.Click += new System.EventHandler(this.btnRemoveObj_Click);
            // 
            // lstCollectionObjects
            // 
            this.lstCollectionObjects.FormattingEnabled = true;
            this.lstCollectionObjects.HorizontalScrollbar = true;
            this.lstCollectionObjects.Location = new System.Drawing.Point(7, 41);
            this.lstCollectionObjects.Name = "lstCollectionObjects";
            this.lstCollectionObjects.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstCollectionObjects.Size = new System.Drawing.Size(164, 199);
            this.lstCollectionObjects.TabIndex = 11;
            // 
            // btnAddObject
            // 
            this.btnAddObject.Location = new System.Drawing.Point(176, 115);
            this.btnAddObject.Name = "btnAddObject";
            this.btnAddObject.Size = new System.Drawing.Size(45, 21);
            this.btnAddObject.TabIndex = 10;
            this.btnAddObject.Text = "<";
            this.btnAddObject.UseVisualStyleBackColor = true;
            this.btnAddObject.Click += new System.EventHandler(this.btnAddObject_Click);
            // 
            // lstObjects
            // 
            this.lstObjects.FormattingEnabled = true;
            this.lstObjects.HorizontalScrollbar = true;
            this.lstObjects.Location = new System.Drawing.Point(226, 41);
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstObjects.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstObjects.Size = new System.Drawing.Size(169, 199);
            this.lstObjects.TabIndex = 9;
            // 
            // btnCollectionClose
            // 
            this.btnCollectionClose.Location = new System.Drawing.Point(745, 701);
            this.btnCollectionClose.Name = "btnCollectionClose";
            this.btnCollectionClose.Size = new System.Drawing.Size(75, 23);
            this.btnCollectionClose.TabIndex = 10;
            this.btnCollectionClose.Text = "Close";
            this.btnCollectionClose.UseVisualStyleBackColor = true;
            this.btnCollectionClose.Click += new System.EventHandler(this.btnCollectionClose_Click);
            // 
            // gbOverlays
            // 
            this.gbOverlays.Controls.Add(this.groupBox2);
            this.gbOverlays.Controls.Add(this.btnRemoveOverlaysFromOM);
            this.gbOverlays.Controls.Add(this.label9);
            this.gbOverlays.Controls.Add(this.label6);
            this.gbOverlays.Controls.Add(this.btnRemoveOverlay);
            this.gbOverlays.Controls.Add(this.lstCollectionOverlay);
            this.gbOverlays.Controls.Add(this.btnAddOverlay);
            this.gbOverlays.Controls.Add(this.lstOverlay);
            this.gbOverlays.Location = new System.Drawing.Point(410, 32);
            this.gbOverlays.Name = "gbOverlays";
            this.gbOverlays.Size = new System.Drawing.Size(401, 559);
            this.gbOverlays.TabIndex = 13;
            this.gbOverlays.TabStop = false;
            this.gbOverlays.Text = "Overlays";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClearOverlaysViewportSelection);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lstOverlaysViewports);
            this.groupBox2.Controls.Add(this.btnOverlaysVisbilityApply);
            this.groupBox2.Controls.Add(this.cmbOverlaysVisibility);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(5, 311);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(391, 232);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Overlays operations";
            // 
            // btnClearOverlaysViewportSelection
            // 
            this.btnClearOverlaysViewportSelection.Location = new System.Drawing.Point(161, 147);
            this.btnClearOverlaysViewportSelection.Name = "btnClearOverlaysViewportSelection";
            this.btnClearOverlaysViewportSelection.Size = new System.Drawing.Size(42, 23);
            this.btnClearOverlaysViewportSelection.TabIndex = 127;
            this.btnClearOverlaysViewportSelection.Text = "Clear";
            this.btnClearOverlaysViewportSelection.UseVisualStyleBackColor = true;
            this.btnClearOverlaysViewportSelection.Click += new System.EventHandler(this.btnClearOverlaysViewportSelection_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label12.Location = new System.Drawing.Point(214, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(169, 28);
            this.label12.TabIndex = 126;
            this.label12.Text = "Viewports (Clear selection to set for all viewport)";
            // 
            // lstOverlaysViewports
            // 
            this.lstOverlaysViewports.FormattingEnabled = true;
            this.lstOverlaysViewports.Location = new System.Drawing.Point(214, 52);
            this.lstOverlaysViewports.Name = "lstOverlaysViewports";
            this.lstOverlaysViewports.Size = new System.Drawing.Size(170, 121);
            this.lstOverlaysViewports.TabIndex = 125;
            this.lstOverlaysViewports.SelectedIndexChanged += new System.EventHandler(this.btnClearOverlaysViewportSelection_Click);
            // 
            // btnOverlaysVisbilityApply
            // 
            this.btnOverlaysVisbilityApply.Location = new System.Drawing.Point(161, 57);
            this.btnOverlaysVisbilityApply.Name = "btnOverlaysVisbilityApply";
            this.btnOverlaysVisbilityApply.Size = new System.Drawing.Size(42, 23);
            this.btnOverlaysVisbilityApply.TabIndex = 121;
            this.btnOverlaysVisbilityApply.Text = "Apply";
            this.btnOverlaysVisbilityApply.UseVisualStyleBackColor = true;
            this.btnOverlaysVisbilityApply.Click += new System.EventHandler(this.btnOverlaysVisbilityApply_Click);
            // 
            // cmbOverlaysVisibility
            // 
            this.cmbOverlaysVisibility.FormattingEnabled = true;
            this.cmbOverlaysVisibility.Location = new System.Drawing.Point(45, 56);
            this.cmbOverlaysVisibility.Name = "cmbOverlaysVisibility";
            this.cmbOverlaysVisibility.Size = new System.Drawing.Size(111, 21);
            this.cmbOverlaysVisibility.TabIndex = 120;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 119;
            this.label4.Text = "Visibility:";
            // 
            // btnRemoveOverlaysFromOM
            // 
            this.btnRemoveOverlaysFromOM.Location = new System.Drawing.Point(6, 256);
            this.btnRemoveOverlaysFromOM.Name = "btnRemoveOverlaysFromOM";
            this.btnRemoveOverlaysFromOM.Size = new System.Drawing.Size(146, 23);
            this.btnRemoveOverlaysFromOM.TabIndex = 28;
            this.btnRemoveOverlaysFromOM.Text = "Remove Overlays From OM";
            this.btnRemoveOverlaysFromOM.UseVisualStyleBackColor = true;
            this.btnRemoveOverlaysFromOM.Click += new System.EventHandler(this.btnRemoveOverlaysFromOM_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(3, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(171, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Collection Overlays (Single Select):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(204, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Overlay List (Multi Select):";
            // 
            // btnRemoveOverlay
            // 
            this.btnRemoveOverlay.Location = new System.Drawing.Point(156, 143);
            this.btnRemoveOverlay.Name = "btnRemoveOverlay";
            this.btnRemoveOverlay.Size = new System.Drawing.Size(45, 21);
            this.btnRemoveOverlay.TabIndex = 14;
            this.btnRemoveOverlay.Text = ">";
            this.btnRemoveOverlay.UseVisualStyleBackColor = true;
            this.btnRemoveOverlay.Click += new System.EventHandler(this.btnRemoveOverlay_Click);
            // 
            // lstCollectionOverlay
            // 
            this.lstCollectionOverlay.FormattingEnabled = true;
            this.lstCollectionOverlay.HorizontalScrollbar = true;
            this.lstCollectionOverlay.Location = new System.Drawing.Point(6, 41);
            this.lstCollectionOverlay.Name = "lstCollectionOverlay";
            this.lstCollectionOverlay.Size = new System.Drawing.Size(144, 199);
            this.lstCollectionOverlay.TabIndex = 11;
            // 
            // btnAddOverlay
            // 
            this.btnAddOverlay.Location = new System.Drawing.Point(156, 115);
            this.btnAddOverlay.Name = "btnAddOverlay";
            this.btnAddOverlay.Size = new System.Drawing.Size(45, 21);
            this.btnAddOverlay.TabIndex = 10;
            this.btnAddOverlay.Text = "<";
            this.btnAddOverlay.UseVisualStyleBackColor = true;
            this.btnAddOverlay.Click += new System.EventHandler(this.btnAddOverlay_Click);
            // 
            // lstOverlay
            // 
            this.lstOverlay.FormattingEnabled = true;
            this.lstOverlay.HorizontalScrollbar = true;
            this.lstOverlay.Location = new System.Drawing.Point(207, 40);
            this.lstOverlay.Name = "lstOverlay";
            this.lstOverlay.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lstOverlay.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstOverlay.Size = new System.Drawing.Size(144, 199);
            this.lstOverlay.TabIndex = 9;
            // 
            // tcCollection
            // 
            this.tcCollection.Controls.Add(this.tpGeneral);
            this.tcCollection.Controls.Add(this.tpCollection);
            this.tcCollection.Dock = System.Windows.Forms.DockStyle.Top;
            this.tcCollection.Location = new System.Drawing.Point(0, 0);
            this.tcCollection.Name = "tcCollection";
            this.tcCollection.SelectedIndex = 0;
            this.tcCollection.Size = new System.Drawing.Size(823, 695);
            this.tcCollection.TabIndex = 14;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.btnCollectionVisibilityApply);
            this.tpGeneral.Controls.Add(this.chkCollectionVisibility);
            this.tpGeneral.Controls.Add(this.label10);
            this.tpGeneral.Controls.Add(this.clstCollectionVisibility);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpGeneral.Size = new System.Drawing.Size(815, 669);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // tpCollection
            // 
            this.tpCollection.Controls.Add(this.btnClearCollection);
            this.tpCollection.Controls.Add(this.gbObjects);
            this.tpCollection.Controls.Add(this.gbOverlays);
            this.tpCollection.Location = new System.Drawing.Point(4, 22);
            this.tpCollection.Name = "tpCollection";
            this.tpCollection.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tpCollection.Size = new System.Drawing.Size(815, 669);
            this.tpCollection.TabIndex = 1;
            this.tpCollection.Text = "Collection";
            this.tpCollection.UseVisualStyleBackColor = true;
            // 
            // btnClearCollection
            // 
            this.btnClearCollection.Location = new System.Drawing.Point(9, 6);
            this.btnClearCollection.Name = "btnClearCollection";
            this.btnClearCollection.Size = new System.Drawing.Size(94, 23);
            this.btnClearCollection.TabIndex = 29;
            this.btnClearCollection.Text = "Clear Collection";
            this.btnClearCollection.UseVisualStyleBackColor = true;
            this.btnClearCollection.Click += new System.EventHandler(this.btnClearCollection_Click);
            // 
            // ucCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.tcCollection);
            this.Controls.Add(this.btnCollectionClose);
            this.Name = "ucCollection";
            this.Size = new System.Drawing.Size(823, 727);
            this.gbObjects.ResumeLayout(false);
            this.gbObjects.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbOverlays.ResumeLayout(false);
            this.gbOverlays.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tcCollection.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.tpCollection.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.CheckBox chkCollectionVisibility;
        private System.Windows.Forms.GroupBox gbObjects;
        private System.Windows.Forms.ListBox lstObjects;
        private System.Windows.Forms.Button btnAddObject;
        private System.Windows.Forms.Button btnRemoveObj;
        private System.Windows.Forms.ListBox lstCollectionObjects;
        private System.Windows.Forms.CheckBox chkSelectAllObjects;
        private System.Windows.Forms.Button btnCollectionClose;
        private System.Windows.Forms.GroupBox gbOverlays;
        private System.Windows.Forms.Button btnRemoveOverlay;
        private System.Windows.Forms.ListBox lstCollectionOverlay;
        private System.Windows.Forms.Button btnAddOverlay;
        private System.Windows.Forms.ListBox lstOverlay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRemoveOverlaysFromOM;
        private System.Windows.Forms.Button btnRemoveObjectsFromTheirOverlays;
        private System.Windows.Forms.CheckedListBox clstCollectionVisibility;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnCollectionVisibilityApply;
        private System.Windows.Forms.TabControl tcCollection;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpCollection;
        private System.Windows.Forms.Button btnClearCollection;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClearObjectsViewportSelection;
        private System.Windows.Forms.ListBox lstObjectsViewports;
        private System.Windows.Forms.Button btnObjectsVisbilityApply;
        private System.Windows.Forms.Button btnMoveAllObjOK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbObjectsVisibility;
        private Controls.Ctrl3DVector ctrl3DVectorMoveAllObj;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClearOverlaysViewportSelection;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox lstOverlaysViewports;
        private System.Windows.Forms.Button btnOverlaysVisbilityApply;
        private System.Windows.Forms.ComboBox cmbOverlaysVisibility;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbState;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnStateApply;

    }
}
