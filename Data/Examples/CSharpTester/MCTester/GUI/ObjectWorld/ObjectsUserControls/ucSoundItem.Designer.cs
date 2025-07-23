namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ucSoundItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSoundItem));
            this.Tab_Sound = new System.Windows.Forms.TabPage();
            this.tcSound = new System.Windows.Forms.TabControl();
            this.tpSoundBasic = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertySoundName = new MCTester.Controls.CtrlObjStatePropertySelectFile();
            this.ctrlObjStatePropertySoundLoop = new MCTester.Controls.CtrlObjStatePropertyBool();
            this.ctrlObjStatePropertySoundState = new MCTester.Controls.CtrlObjStatePropertyESoundState();
            this.ctrlObjStatePropertySoundStartingTimePoint = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundVolume = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.tpSoundVolume = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertySoundHalfInnerAngle = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundHalfVolumeDistance = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundMaxVolume = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundRollOffFactor = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundOuterAngleVolume = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundMinVolume = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundMaxDistance = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlObjStatePropertySoundHalfOuterAngle = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.tpSoundVelocityPitchFade = new System.Windows.Forms.TabPage();
            this.ctrlObjStatePropertySoundPitch = new MCTester.Controls.CtrlObjStatePropertyFloat();
            this.ctrlPropertySoundVelocity = new MCTester.Controls.CtrlObjStatePropertyFVect3D();
            this.tabControl1.SuspendLayout();
            this.Tab_Sound.SuspendLayout();
            this.tcSound.SuspendLayout();
            this.tpSoundBasic.SuspendLayout();
            this.tpSoundVolume.SuspendLayout();
            this.tpSoundVelocityPitchFade.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab_ObjectSchemeItem
            // 
            this.Tab_ObjectSchemeItem.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectSchemeItem.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            // 
            // Tab_ObjectScehemeNode
            // 
            this.Tab_ObjectScehemeNode.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Tab_ObjectScehemeNode.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Tab_Sound);
            this.tabControl1.Controls.SetChildIndex(this.Tab_Sound, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectSchemeItem, 0);
            this.tabControl1.Controls.SetChildIndex(this.Tab_ObjectScehemeNode, 0);
            // 
            // Tab_Sound
            // 
            this.Tab_Sound.AutoScroll = true;
            this.Tab_Sound.Controls.Add(this.tcSound);
            this.Tab_Sound.Location = new System.Drawing.Point(4, 25);
            this.Tab_Sound.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Sound.Name = "Tab_Sound";
            this.Tab_Sound.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Tab_Sound.Size = new System.Drawing.Size(1109, 857);
            this.Tab_Sound.TabIndex = 3;
            this.Tab_Sound.Text = "Sound";
            this.Tab_Sound.UseVisualStyleBackColor = true;
            // 
            // tcSound
            // 
            this.tcSound.Controls.Add(this.tpSoundBasic);
            this.tcSound.Controls.Add(this.tpSoundVolume);
            this.tcSound.Controls.Add(this.tpSoundVelocityPitchFade);
            this.tcSound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSound.Location = new System.Drawing.Point(4, 4);
            this.tcSound.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tcSound.Name = "tcSound";
            this.tcSound.SelectedIndex = 0;
            this.tcSound.Size = new System.Drawing.Size(1101, 849);
            this.tcSound.TabIndex = 16;
            // 
            // tpSoundBasic
            // 
            this.tpSoundBasic.Controls.Add(this.ctrlObjStatePropertySoundName);
            this.tpSoundBasic.Controls.Add(this.ctrlObjStatePropertySoundLoop);
            this.tpSoundBasic.Controls.Add(this.ctrlObjStatePropertySoundState);
            this.tpSoundBasic.Controls.Add(this.ctrlObjStatePropertySoundStartingTimePoint);
            this.tpSoundBasic.Controls.Add(this.ctrlObjStatePropertySoundVolume);
            this.tpSoundBasic.Location = new System.Drawing.Point(4, 25);
            this.tpSoundBasic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSoundBasic.Name = "tpSoundBasic";
            this.tpSoundBasic.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSoundBasic.Size = new System.Drawing.Size(1093, 820);
            this.tpSoundBasic.TabIndex = 0;
            this.tpSoundBasic.Text = "Basic";
            this.tpSoundBasic.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertySoundName
            // 
            this.ctrlObjStatePropertySoundName.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundName.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundName.Location = new System.Drawing.Point(5, 6);
            this.ctrlObjStatePropertySoundName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundName.Name = "ctrlObjStatePropertySoundName";
            this.ctrlObjStatePropertySoundName.PropertyName = "Sound Name";
            this.ctrlObjStatePropertySoundName.RegFileVal = "";
            this.ctrlObjStatePropertySoundName.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundName.SelFileVal = "";
            this.ctrlObjStatePropertySoundName.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundName.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundName.TabIndex = 22;
            // 
            // ctrlObjStatePropertySoundLoop
            // 
            this.ctrlObjStatePropertySoundLoop.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundLoop.BoolLabel = "Loop";
            this.ctrlObjStatePropertySoundLoop.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundLoop.Location = new System.Drawing.Point(544, 6);
            this.ctrlObjStatePropertySoundLoop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundLoop.Name = "ctrlObjStatePropertySoundLoop";
            this.ctrlObjStatePropertySoundLoop.PropertyName = "Loop";
            this.ctrlObjStatePropertySoundLoop.RegBoolVal = false;
            this.ctrlObjStatePropertySoundLoop.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundLoop.SelBoolVal = false;
            this.ctrlObjStatePropertySoundLoop.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundLoop.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundLoop.TabIndex = 21;
            // 
            // ctrlObjStatePropertySoundState
            // 
            this.ctrlObjStatePropertySoundState.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundState.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundState.EnumType = "";   
            this.ctrlObjStatePropertySoundState.Location = new System.Drawing.Point(545, 170);
            this.ctrlObjStatePropertySoundState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundState.Name = "ctrlObjStatePropertySoundState";
            this.ctrlObjStatePropertySoundState.PropertyName = "State";
            this.ctrlObjStatePropertySoundState.RegEnumVal = MapCore.DNESoundState._ES_STOPPED;
            this.ctrlObjStatePropertySoundState.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundState.SelEnumVal = MapCore.DNESoundState._ES_STOPPED;
            this.ctrlObjStatePropertySoundState.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundState.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundState.TabIndex = 20;
            // 
            // ctrlObjStatePropertySoundStartingTimePoint
            // 
            this.ctrlObjStatePropertySoundStartingTimePoint.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundStartingTimePoint.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundStartingTimePoint.Location = new System.Drawing.Point(4, 170);
            this.ctrlObjStatePropertySoundStartingTimePoint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundStartingTimePoint.Name = "ctrlObjStatePropertySoundStartingTimePoint";
            this.ctrlObjStatePropertySoundStartingTimePoint.PropertyName = "Starting Time Point";
            this.ctrlObjStatePropertySoundStartingTimePoint.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundStartingTimePoint.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundStartingTimePoint.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundStartingTimePoint.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundStartingTimePoint.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundStartingTimePoint.TabIndex = 19;
            // 
            // ctrlObjStatePropertySoundVolume
            // 
            this.ctrlObjStatePropertySoundVolume.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundVolume.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundVolume.Location = new System.Drawing.Point(5, 335);
            this.ctrlObjStatePropertySoundVolume.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundVolume.Name = "ctrlObjStatePropertySoundVolume";
            this.ctrlObjStatePropertySoundVolume.PropertyName = "Volume";
            this.ctrlObjStatePropertySoundVolume.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundVolume.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundVolume.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundVolume.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundVolume.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundVolume.TabIndex = 18;
            // 
            // tpSoundVolume
            // 
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundHalfInnerAngle);
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundHalfVolumeDistance);
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundMaxVolume);
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundRollOffFactor);
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundOuterAngleVolume);
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundMinVolume);
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundMaxDistance);
            this.tpSoundVolume.Controls.Add(this.ctrlObjStatePropertySoundHalfOuterAngle);
            this.tpSoundVolume.Location = new System.Drawing.Point(4, 25);
            this.tpSoundVolume.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSoundVolume.Name = "tpSoundVolume";
            this.tpSoundVolume.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSoundVolume.Size = new System.Drawing.Size(1093, 820);
            this.tpSoundVolume.TabIndex = 1;
            this.tpSoundVolume.Text = "Volume";
            this.tpSoundVolume.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertySoundHalfInnerAngle
            // 
            this.ctrlObjStatePropertySoundHalfInnerAngle.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundHalfInnerAngle.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundHalfInnerAngle.Location = new System.Drawing.Point(543, 341);
            this.ctrlObjStatePropertySoundHalfInnerAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundHalfInnerAngle.Name = "ctrlObjStatePropertySoundHalfInnerAngle";
            this.ctrlObjStatePropertySoundHalfInnerAngle.PropertyName = "Half Inner Angle";
            this.ctrlObjStatePropertySoundHalfInnerAngle.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundHalfInnerAngle.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundHalfInnerAngle.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundHalfInnerAngle.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundHalfInnerAngle.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundHalfInnerAngle.TabIndex = 29;
            // 
            // ctrlObjStatePropertySoundHalfVolumeDistance
            // 
            this.ctrlObjStatePropertySoundHalfVolumeDistance.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundHalfVolumeDistance.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundHalfVolumeDistance.Location = new System.Drawing.Point(543, 176);
            this.ctrlObjStatePropertySoundHalfVolumeDistance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundHalfVolumeDistance.Name = "ctrlObjStatePropertySoundHalfVolumeDistance";
            this.ctrlObjStatePropertySoundHalfVolumeDistance.PropertyName = "Half Volume Distance";
            this.ctrlObjStatePropertySoundHalfVolumeDistance.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundHalfVolumeDistance.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundHalfVolumeDistance.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundHalfVolumeDistance.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundHalfVolumeDistance.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundHalfVolumeDistance.TabIndex = 28;
            // 
            // ctrlObjStatePropertySoundMaxVolume
            // 
            this.ctrlObjStatePropertySoundMaxVolume.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundMaxVolume.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundMaxVolume.Location = new System.Drawing.Point(543, 9);
            this.ctrlObjStatePropertySoundMaxVolume.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundMaxVolume.Name = "ctrlObjStatePropertySoundMaxVolume";
            this.ctrlObjStatePropertySoundMaxVolume.PropertyName = "Max Volume";
            this.ctrlObjStatePropertySoundMaxVolume.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundMaxVolume.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundMaxVolume.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundMaxVolume.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundMaxVolume.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundMaxVolume.TabIndex = 27;
            // 
            // ctrlObjStatePropertySoundRollOffFactor
            // 
            this.ctrlObjStatePropertySoundRollOffFactor.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundRollOffFactor.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundRollOffFactor.Location = new System.Drawing.Point(543, 506);
            this.ctrlObjStatePropertySoundRollOffFactor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundRollOffFactor.Name = "ctrlObjStatePropertySoundRollOffFactor";
            this.ctrlObjStatePropertySoundRollOffFactor.PropertyName = "Roll Off Factor";
            this.ctrlObjStatePropertySoundRollOffFactor.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundRollOffFactor.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundRollOffFactor.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundRollOffFactor.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundRollOffFactor.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundRollOffFactor.TabIndex = 25;
            // 
            // ctrlObjStatePropertySoundOuterAngleVolume
            // 
            this.ctrlObjStatePropertySoundOuterAngleVolume.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundOuterAngleVolume.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundOuterAngleVolume.Location = new System.Drawing.Point(7, 503);
            this.ctrlObjStatePropertySoundOuterAngleVolume.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundOuterAngleVolume.Name = "ctrlObjStatePropertySoundOuterAngleVolume";
            this.ctrlObjStatePropertySoundOuterAngleVolume.PropertyName = "Outer Angle Volume";
            this.ctrlObjStatePropertySoundOuterAngleVolume.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundOuterAngleVolume.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundOuterAngleVolume.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundOuterAngleVolume.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundOuterAngleVolume.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundOuterAngleVolume.TabIndex = 23;
            // 
            // ctrlObjStatePropertySoundMinVolume
            // 
            this.ctrlObjStatePropertySoundMinVolume.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundMinVolume.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundMinVolume.Location = new System.Drawing.Point(4, 9);
            this.ctrlObjStatePropertySoundMinVolume.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundMinVolume.Name = "ctrlObjStatePropertySoundMinVolume";
            this.ctrlObjStatePropertySoundMinVolume.PropertyName = "Min Volume";
            this.ctrlObjStatePropertySoundMinVolume.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundMinVolume.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundMinVolume.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundMinVolume.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundMinVolume.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundMinVolume.TabIndex = 22;
            // 
            // ctrlObjStatePropertySoundMaxDistance
            // 
            this.ctrlObjStatePropertySoundMaxDistance.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundMaxDistance.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundMaxDistance.Location = new System.Drawing.Point(4, 174);
            this.ctrlObjStatePropertySoundMaxDistance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundMaxDistance.Name = "ctrlObjStatePropertySoundMaxDistance";
            this.ctrlObjStatePropertySoundMaxDistance.PropertyName = "Max Distance";
            this.ctrlObjStatePropertySoundMaxDistance.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundMaxDistance.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundMaxDistance.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundMaxDistance.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundMaxDistance.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundMaxDistance.TabIndex = 21;
            // 
            // ctrlObjStatePropertySoundHalfOuterAngle
            // 
            this.ctrlObjStatePropertySoundHalfOuterAngle.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundHalfOuterAngle.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundHalfOuterAngle.Location = new System.Drawing.Point(4, 338);
            this.ctrlObjStatePropertySoundHalfOuterAngle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundHalfOuterAngle.Name = "ctrlObjStatePropertySoundHalfOuterAngle";
            this.ctrlObjStatePropertySoundHalfOuterAngle.PropertyName = "Half Outer Angle";
            this.ctrlObjStatePropertySoundHalfOuterAngle.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundHalfOuterAngle.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundHalfOuterAngle.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundHalfOuterAngle.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundHalfOuterAngle.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundHalfOuterAngle.TabIndex = 20;
            // 
            // tpSoundVelocityPitchFade
            // 
            this.tpSoundVelocityPitchFade.Controls.Add(this.ctrlObjStatePropertySoundPitch);
            this.tpSoundVelocityPitchFade.Controls.Add(this.ctrlPropertySoundVelocity);
            this.tpSoundVelocityPitchFade.Location = new System.Drawing.Point(4, 25);
            this.tpSoundVelocityPitchFade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSoundVelocityPitchFade.Name = "tpSoundVelocityPitchFade";
            this.tpSoundVelocityPitchFade.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tpSoundVelocityPitchFade.Size = new System.Drawing.Size(1093, 820);
            this.tpSoundVelocityPitchFade.TabIndex = 2;
            this.tpSoundVelocityPitchFade.Text = "Velocity\\Pitch\\Fade";
            this.tpSoundVelocityPitchFade.UseVisualStyleBackColor = true;
            // 
            // ctrlObjStatePropertySoundPitch
            // 
            this.ctrlObjStatePropertySoundPitch.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlObjStatePropertySoundPitch.CurrentObjectSchemeNode = null;
            this.ctrlObjStatePropertySoundPitch.Location = new System.Drawing.Point(543, 9);
            this.ctrlObjStatePropertySoundPitch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrlObjStatePropertySoundPitch.Name = "ctrlObjStatePropertySoundPitch";
            this.ctrlObjStatePropertySoundPitch.PropertyName = "Pitch";
            this.ctrlObjStatePropertySoundPitch.RegFloatVal = 0F;
            this.ctrlObjStatePropertySoundPitch.RegPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundPitch.SelFloatVal = 0F;
            this.ctrlObjStatePropertySoundPitch.SelPropertyID = ((uint)(0u));
            this.ctrlObjStatePropertySoundPitch.Size = new System.Drawing.Size(533, 160);
            this.ctrlObjStatePropertySoundPitch.TabIndex = 16;
            // 
            // ctrlPropertySoundVelocity
            // 
            this.ctrlPropertySoundVelocity.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ctrlPropertySoundVelocity.CurrentObjectSchemeNode = null;
            this.ctrlPropertySoundVelocity.Location = new System.Drawing.Point(4, 7);
            this.ctrlPropertySoundVelocity.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ctrlPropertySoundVelocity.Name = "ctrlPropertySoundVelocity";
            this.ctrlPropertySoundVelocity.PropertyName = "Velocity";
            this.ctrlPropertySoundVelocity.RegPropertyID = ((uint)(0u));
            this.ctrlPropertySoundVelocity.SelPropertyID = ((uint)(0u));
            this.ctrlPropertySoundVelocity.Size = new System.Drawing.Size(533, 160);
            this.ctrlPropertySoundVelocity.TabIndex = 14;
            // 
            // ucSoundItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "ucSoundItem";
            this.tabControl1.ResumeLayout(false);
            this.Tab_Sound.ResumeLayout(false);
            this.tcSound.ResumeLayout(false);
            this.tpSoundBasic.ResumeLayout(false);
            this.tpSoundVolume.ResumeLayout(false);
            this.tpSoundVelocityPitchFade.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage Tab_Sound;
        private System.Windows.Forms.TabControl tcSound;
        private System.Windows.Forms.TabPage tpSoundBasic;
        private System.Windows.Forms.TabPage tpSoundVolume;
        private System.Windows.Forms.TabPage tpSoundVelocityPitchFade;
        private MCTester.Controls.CtrlObjStatePropertyFVect3D ctrlPropertySoundVelocity;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundStartingTimePoint;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundVolume;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundMinVolume;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundMaxDistance;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundHalfOuterAngle;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundHalfInnerAngle;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundHalfVolumeDistance;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundMaxVolume;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundRollOffFactor;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundOuterAngleVolume;
        private Controls.CtrlObjStatePropertyFloat ctrlObjStatePropertySoundPitch;
        private Controls.CtrlObjStatePropertyESoundState ctrlObjStatePropertySoundState;
        private Controls.CtrlObjStatePropertyBool ctrlObjStatePropertySoundLoop;
        private Controls.CtrlObjStatePropertySelectFile ctrlObjStatePropertySoundName;
    }
}
