namespace MCTester.Controls
{
    partial class CtrlEditModePermissions
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
            this.gbHiddenIconsPerPermission = new System.Windows.Forms.GroupBox();
            this.chxShowIconsScreenPositions = new System.Windows.Forms.CheckBox();
            this.btnSetHiddenIcons = new System.Windows.Forms.Button();
            this.chxIsFieldEnableHiddenIcons = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtHiddenIconPerPermission = new System.Windows.Forms.TextBox();
            this.cmbPermissionType = new System.Windows.Forms.ComboBox();
            this.gbPermissions = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chxFinishTextStringByKey = new System.Windows.Forms.CheckBox();
            this.chxDrag = new System.Windows.Forms.CheckBox();
            this.chxResize = new System.Windows.Forms.CheckBox();
            this.chxRotate = new System.Windows.Forms.CheckBox();
            this.chxMoveVertex = new System.Windows.Forms.CheckBox();
            this.chxBreakEdge = new System.Windows.Forms.CheckBox();
            this.chxIsFieldEnablePermissions = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbHiddenIconsPerPermission.SuspendLayout();
            this.gbPermissions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbHiddenIconsPerPermission
            // 
            this.gbHiddenIconsPerPermission.Controls.Add(this.chxShowIconsScreenPositions);
            this.gbHiddenIconsPerPermission.Controls.Add(this.btnSetHiddenIcons);
            this.gbHiddenIconsPerPermission.Controls.Add(this.chxIsFieldEnableHiddenIcons);
            this.gbHiddenIconsPerPermission.Controls.Add(this.label16);
            this.gbHiddenIconsPerPermission.Controls.Add(this.label15);
            this.gbHiddenIconsPerPermission.Controls.Add(this.txtHiddenIconPerPermission);
            this.gbHiddenIconsPerPermission.Controls.Add(this.cmbPermissionType);
            this.gbHiddenIconsPerPermission.Location = new System.Drawing.Point(3, 188);
            this.gbHiddenIconsPerPermission.Name = "gbHiddenIconsPerPermission";
            this.gbHiddenIconsPerPermission.Size = new System.Drawing.Size(444, 158);
            this.gbHiddenIconsPerPermission.TabIndex = 4;
            this.gbHiddenIconsPerPermission.TabStop = false;
            this.gbHiddenIconsPerPermission.Text = "Icons Per Permission";
            // 
            // chxShowIconsScreenPositions
            // 
            this.chxShowIconsScreenPositions.AutoSize = true;
            this.chxShowIconsScreenPositions.Location = new System.Drawing.Point(14, 135);
            this.chxShowIconsScreenPositions.Name = "chxShowIconsScreenPositions";
            this.chxShowIconsScreenPositions.Size = new System.Drawing.Size(164, 17);
            this.chxShowIconsScreenPositions.TabIndex = 66;
            this.chxShowIconsScreenPositions.Text = "Show Icons Screen Positions";
            this.chxShowIconsScreenPositions.UseVisualStyleBackColor = true;
            this.chxShowIconsScreenPositions.CheckedChanged += new System.EventHandler(this.chxShowIconsScreenPositions_CheckedChanged);
            // 
            // btnSetHiddenIcons
            // 
            this.btnSetHiddenIcons.Location = new System.Drawing.Point(362, 112);
            this.btnSetHiddenIcons.Name = "btnSetHiddenIcons";
            this.btnSetHiddenIcons.Size = new System.Drawing.Size(75, 23);
            this.btnSetHiddenIcons.TabIndex = 50;
            this.btnSetHiddenIcons.Text = "Set";
            this.btnSetHiddenIcons.UseVisualStyleBackColor = true;
            this.btnSetHiddenIcons.Click += new System.EventHandler(this.btnSetHiddenIcons_Click);
            // 
            // chxIsFieldEnableHiddenIcons
            // 
            this.chxIsFieldEnableHiddenIcons.AutoSize = true;
            this.chxIsFieldEnableHiddenIcons.Location = new System.Drawing.Point(14, 77);
            this.chxIsFieldEnableHiddenIcons.Name = "chxIsFieldEnableHiddenIcons";
            this.chxIsFieldEnableHiddenIcons.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnableHiddenIcons.TabIndex = 65;
            this.chxIsFieldEnableHiddenIcons.UseVisualStyleBackColor = true;
            this.chxIsFieldEnableHiddenIcons.CheckedChanged += new System.EventHandler(this.chxIsFieldEnableHiddenIcons_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label16.Location = new System.Drawing.Point(33, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(230, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Hidden Icons Indices List (separate by spaces):";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(33, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Permission Type:";
            // 
            // txtHiddenIconPerPermission
            // 
            this.txtHiddenIconPerPermission.Location = new System.Drawing.Point(36, 66);
            this.txtHiddenIconPerPermission.Multiline = true;
            this.txtHiddenIconPerPermission.Name = "txtHiddenIconPerPermission";
            this.txtHiddenIconPerPermission.Size = new System.Drawing.Size(401, 40);
            this.txtHiddenIconPerPermission.TabIndex = 51;
            // 
            // cmbPermissionType
            // 
            this.cmbPermissionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPermissionType.FormattingEnabled = true;
            this.cmbPermissionType.Location = new System.Drawing.Point(126, 19);
            this.cmbPermissionType.Name = "cmbPermissionType";
            this.cmbPermissionType.Size = new System.Drawing.Size(215, 21);
            this.cmbPermissionType.TabIndex = 0;
            this.cmbPermissionType.SelectedIndexChanged += new System.EventHandler(this.cmbPermissionType_SelectedIndexChanged);
            // 
            // gbPermissions
            // 
            this.gbPermissions.Controls.Add(this.label1);
            this.gbPermissions.Controls.Add(this.label2);
            this.gbPermissions.Controls.Add(this.label3);
            this.gbPermissions.Controls.Add(this.label4);
            this.gbPermissions.Controls.Add(this.label5);
            this.gbPermissions.Controls.Add(this.label7);
            this.gbPermissions.Controls.Add(this.chxFinishTextStringByKey);
            this.gbPermissions.Controls.Add(this.chxDrag);
            this.gbPermissions.Controls.Add(this.chxResize);
            this.gbPermissions.Controls.Add(this.chxRotate);
            this.gbPermissions.Controls.Add(this.chxMoveVertex);
            this.gbPermissions.Controls.Add(this.chxBreakEdge);
            this.gbPermissions.Location = new System.Drawing.Point(59, 14);
            this.gbPermissions.Name = "gbPermissions";
            this.gbPermissions.Size = new System.Drawing.Size(200, 170);
            this.gbPermissions.TabIndex = 3;
            this.gbPermissions.TabStop = false;
            this.gbPermissions.Text = "Permissions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Finish text string by key";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Drag";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Rotate";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Resize";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Break edge";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "Move vertex";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // chxFinishTextStringByKey
            // 
            this.chxFinishTextStringByKey.AutoSize = true;
            this.chxFinishTextStringByKey.Location = new System.Drawing.Point(6, 134);
            this.chxFinishTextStringByKey.Name = "chxFinishTextStringByKey";
            this.chxFinishTextStringByKey.Size = new System.Drawing.Size(15, 14);
            this.chxFinishTextStringByKey.TabIndex = 7;
            this.chxFinishTextStringByKey.UseVisualStyleBackColor = true;
            // 
            // chxDrag
            // 
            this.chxDrag.AutoSize = true;
            this.chxDrag.Location = new System.Drawing.Point(6, 111);
            this.chxDrag.Name = "chxDrag";
            this.chxDrag.Size = new System.Drawing.Size(15, 14);
            this.chxDrag.TabIndex = 6;
            this.chxDrag.UseVisualStyleBackColor = true;
            // 
            // chxResize
            // 
            this.chxResize.AutoSize = true;
            this.chxResize.Location = new System.Drawing.Point(6, 65);
            this.chxResize.Name = "chxResize";
            this.chxResize.Size = new System.Drawing.Size(15, 14);
            this.chxResize.TabIndex = 4;
            this.chxResize.UseVisualStyleBackColor = true;
            // 
            // chxRotate
            // 
            this.chxRotate.AutoSize = true;
            this.chxRotate.Location = new System.Drawing.Point(6, 88);
            this.chxRotate.Name = "chxRotate";
            this.chxRotate.Size = new System.Drawing.Size(15, 14);
            this.chxRotate.TabIndex = 5;
            this.chxRotate.UseVisualStyleBackColor = true;
            // 
            // chxMoveVertex
            // 
            this.chxMoveVertex.AutoSize = true;
            this.chxMoveVertex.Location = new System.Drawing.Point(6, 19);
            this.chxMoveVertex.Name = "chxMoveVertex";
            this.chxMoveVertex.Size = new System.Drawing.Size(15, 14);
            this.chxMoveVertex.TabIndex = 0;
            this.chxMoveVertex.UseVisualStyleBackColor = true;
            // 
            // chxBreakEdge
            // 
            this.chxBreakEdge.AutoSize = true;
            this.chxBreakEdge.Location = new System.Drawing.Point(6, 42);
            this.chxBreakEdge.Name = "chxBreakEdge";
            this.chxBreakEdge.Size = new System.Drawing.Size(15, 14);
            this.chxBreakEdge.TabIndex = 3;
            this.chxBreakEdge.UseVisualStyleBackColor = true;
            // 
            // chxIsFieldEnablePermissions
            // 
            this.chxIsFieldEnablePermissions.AutoSize = true;
            this.chxIsFieldEnablePermissions.Location = new System.Drawing.Point(17, 89);
            this.chxIsFieldEnablePermissions.Name = "chxIsFieldEnablePermissions";
            this.chxIsFieldEnablePermissions.Size = new System.Drawing.Size(15, 14);
            this.chxIsFieldEnablePermissions.TabIndex = 64;
            this.chxIsFieldEnablePermissions.UseVisualStyleBackColor = true;
            this.chxIsFieldEnablePermissions.CheckedChanged += new System.EventHandler(this.chxIsFieldEnablePermissions_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-2, -1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "To change";
            // 
            // CtrlEditModePermissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chxIsFieldEnablePermissions);
            this.Controls.Add(this.gbHiddenIconsPerPermission);
            this.Controls.Add(this.gbPermissions);
            this.Name = "CtrlEditModePermissions";
            this.Size = new System.Drawing.Size(516, 378);
            this.gbHiddenIconsPerPermission.ResumeLayout(false);
            this.gbHiddenIconsPerPermission.PerformLayout();
            this.gbPermissions.ResumeLayout(false);
            this.gbPermissions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbHiddenIconsPerPermission;
        private System.Windows.Forms.TextBox txtHiddenIconPerPermission;
        private System.Windows.Forms.Button btnSetHiddenIcons;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbPermissionType;
        private System.Windows.Forms.GroupBox gbPermissions;
        private System.Windows.Forms.CheckBox chxFinishTextStringByKey;
        private System.Windows.Forms.CheckBox chxDrag;
        private System.Windows.Forms.CheckBox chxResize;
        private System.Windows.Forms.CheckBox chxRotate;
        private System.Windows.Forms.CheckBox chxMoveVertex;
        private System.Windows.Forms.CheckBox chxBreakEdge;
        private System.Windows.Forms.CheckBox chxIsFieldEnablePermissions;
        private System.Windows.Forms.CheckBox chxIsFieldEnableHiddenIcons;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chxShowIconsScreenPositions;
    }
}
