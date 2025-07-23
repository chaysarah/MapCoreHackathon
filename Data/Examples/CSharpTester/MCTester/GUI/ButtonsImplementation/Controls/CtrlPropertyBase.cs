using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.Reflection;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlPropertyBase : UserControl
    {
        public CtrlPropertyBase()
        {
            InitializeComponent();
            chxSelectionProperty_CheckedChange(null, new EventArgs());
            chxSelectionProperty.Checked = false;
        }


        public string PropertyName
        {
            get { return this.groupBox1.Text; }
            set {this.groupBox1.Text = value; }
        }

        public void HideSelectionTab()
        {
            tcProperty.TabPages.Remove(tpSelection);
            chxSelectionProperty.Visible = false;
        }

        public bool RegVerifiedId()
        {
            bool validId = true;
            ntxRegPropertyID.ForeColor = Color.Black;
            if (ObjectPropertiesBase.VerifyPrivatePropertiesId && this.rdbRegPrivate.Checked == true)
            {
                validId = VerificationId(ntxRegPropertyID.GetUInt32());
                if (validId == false)
                    ntxRegPropertyID.ForeColor = Color.Red;
            }

            return validId;
        }

        public bool SelVerifiedId()
        {
            bool validId = true;
            ntxSelPropertyID.ForeColor = Color.Black;
            if (ObjectPropertiesBase.VerifyPrivatePropertiesId && this.rdbSelPrivate.Checked == true)
            {
                validId = VerificationId(ntxSelPropertyID.GetUInt32());
                if (validId == false)
                    ntxSelPropertyID.ForeColor = Color.Red;
            }

            return validId;
        }

        public bool VerificationId(uint id)
        {
            int currId = (int)id;
            int result = 0;
            bool verified = false;

            Math.DivRem(currId, 100, out result);

            switch (this.Name)
            {
                case "ctrlPropertyVisibilityOption": // Object Scheme Node
                    if (result == 96)
                        verified = true;
                    break;
                case "ctrlPropertyActivityOption":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyTransformOption":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyConditionalSelectorSchemeNode":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyBlockedTransparency": // Object Scheme Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyTopMost": // Symbolic Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyMoveIfBlockedHeightAboveObstacle":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyMoveIfBlockedMaxChange":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyAPIndex":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyNumAttachPoints":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyAPInterpRatio":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyVectorTransfSegment":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyVectorOffsetDistance":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyOffset":
                    if (result == 99)
                        verified = true;
                    break;
                case "ctrlPropertyRotationYaw":
                    if (result == 92)
                        verified = true;
                    break;
                case "ctrlPropertyRotationPitch":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyRotationRoll":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyLineStyle": // Line Based Item
                    if (result == 2)
                        verified = true;
                    break;
                case "ctrlPropertyLineColor":
                    if (result == 3)
                        verified = true;
                    break;
                case "ctrlPropertyLineWidth":
                    if (result == 1)
                        verified = true;
                    break;
                case "ctrlPropertyLineTexture":
                    if (result == 4)
                        verified = true;
                    break;
                case "ctrlPropertyTextureScale":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyTextureHeightRange":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyArcStartAngle": // Arc Item
                    if (result == 41)
                        verified = true;
                    break;
                case "ctrlPropertyArcEndAngle":
                    if (result == 42)
                        verified = true;
                    break;
                case "ctrlPropertyArcRadiusX":
                    if (result == 43)
                        verified = true;
                    break;
                case "ctrlPropertyArcRadiusY":
                    if (result == 44)
                        verified = true;
                    break;
                case "ctrlPropertyFillStyle": //Closed Shape Item
                    if (result == 38)
                        verified = true;
                    break;
                case "ctrlPropertyFillColor":
                    if (result == 37)
                        verified = true;
                    break;
                case "ctrlPropertyFillTexture":
                    if (result == 39)
                        verified = true;
                    break;
                case "ctrlPropertyFillTextureScale":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySidesFillStyle":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyVerticalHeight":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySidesFillTexture":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySidesFillColor":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySidesFillTextureScale":
                    if (result == 0)
                        verified = true;
                    break;
				case "ctrlPropertyHeadSize":
                    if (result == 36)
                        verified = true;
                    break;
                case "ctrlPropertyHeadAngle": // Arrow Item
                    if (result == 35)
                        verified = true;
                    break;
                case "ctrlPropertyGapSize":
                    if (result == 34)
                        verified = true;
                    break;
                case "ctrlPropertyStartAngle": // Ellipse Item
                    if (result == 41)
                        verified = true;
                    break;
                case "ctrlPropertyEndAngle":
                    if (result == 42)
                        verified = true;
                    break;
                case "ctrlPropertyRadiusX":
                    if (result == 43)
                        verified = true;
                    break;
                case "ctrlPropertyRadiusY":
                    if (result == 44)
                        verified = true;
                    break;
                case "ctrlPropertyInnerRadiusFactor":
                    if (result == 45)
                        verified = true;
                    break;
                case "ctrlPropertyRadius": //Line Expansion
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyWidth": // Picture Item
                    if (result == 52)
                        verified = true;
                    break;
                case "ctrlPropertyHeight":
                    if (result == 53)
                        verified = true;
                    break;
                case "ctrlPropertyPictureTexture":
                    if (result == 50)
                        verified = true;
                    break;
                case "ctrlPropertyTextureColor":
                    if (result == 51)
                        verified = true;
                    break;
                case "ctrlPropertyText": // Text Item
                    if (result == 10)
                        verified = true;
                    break;
                case "ctrlPropertyFont":
                    if (result == 11)
                        verified = true;
                    break;
                case "ctrlPropertyTextAlignment":
                    if (result == 14)
                        verified = true;
                    break;
                case "ctrlPropertyRightToLeftReadingOrder":
                    if (result == 17)
                        verified = true;
                    break;
                case "ctrlPropertyTextColor":
                    if (result == 19)
                        verified = true;
                    break;
                case "ctrlPropertyScale":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyMargin":
                    if (result == 18)
                        verified = true;
                    break;
                case "ctrlPropertyBackgroundColor":
                    if (result == 20)
                        verified = true;
                    break;
                case "ctrlPropertyAP": //Physical Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyParallelToTerrain":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyPhysicalOffset":
                    if (result == 99)
                        verified = true;
                    break;
                case "ctrlPropertyRotation":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyInheritsParentRotation":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyDiffuseColor": // Light Base Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySpecularColor":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyDirection": // Directional Light Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyAttenuation": // Location Based Light Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySpotDirection": // Spot Light Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyHalfOuterAngle":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyHalfInnerAngle":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyMesh": // Mesh Item
                    if (result == 60)
                        verified = true;
                    break;
                case "ctrlPropertyBasePointAlignment":
                    if (result == 63)
                        verified = true;
                    break;
                case "ctrlPropertyModulationColor":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyAnimation":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyFVector3DSubPartOffset":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySubPartRotation":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySubPartInheritsParentRotation":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyEffectName": // Particle Effect Item
                    if (result == 70)
                        verified = true;
                    break;
                case "ctrlPropertyState":
                    if (result == 71)
                        verified = true;
                    break;
                case "ctrlPropertyStartingTimePoint":
                    if (result == 72)
                        verified = true;
                    break;
                case "ctrlPropertyStartingDelay":
                    if (result == 73)
                        verified = true;
                    break;
                case "ctrlPropertySamplingStep":
                    if (result == 74)
                        verified = true;
                    break;
                case "ctrlPropertyTimeFactor":
                    if (result == 75)
                        verified = true;
                    break;
                case "ctrlPropertyParticleVelocity":
                    if (result == 76)
                        verified = true;
                    break;
                case "ctrlPropertyParticleDirection":
                    if (result == 77)
                        verified = true;
                    break;
                case "ctrlPropertyParticleAngle":
                    if (result == 78)
                        verified = true;
                    break;
                case "ctrlPropertyParticleEmissionRate":
                    if (result == 79)
                        verified = true;
                    break;
                case "ctrlPropertyTimeToLive":
                    if (result == 80)
                        verified = true;
                    break;
                case "ctrlPropertyManualGeometryRenderingMode": //Manual Geometry Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyManualGeometryTexture":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyProjectorFOV":  // Projector Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyProjectorTexture":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyProjectorAspectRatio":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyProjectorTargetTypes":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySoundName": // Sound Item
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySoundLoop":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySoundrStartingTimePoint":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySoundState":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertyShowSightPresentation": // Sight Presentation
                    if (result == 24)
                        verified = true;
                    break;
                case "ctrlPropertySightConsiderStaticMeshes":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySightObserverHeight":
                    if (result == 26)
                        verified = true;
                    break;
                case "ctrlPropertySightObservedHeight":
                    if (result == 25)
                        verified = true;
                    break;
                case "ctrlPropertySightVisibleColor":
                    if (result == 29)
                        verified = true;
                    break;
                case "ctrlPropertySightHiddenColor":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySightObserverMinPitch":
                    if (result == 28)
                        verified = true;
                    break;
                case "ctrlPropertySightObserverMaxPitch":
                    if (result == 27)
                        verified = true;
                    break;
                case "ctrlPropertySightObservedHeightAbsolute":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySightNumEllipseRays":
                    if (result == 0)
                        verified = true;
                    break;
                case "ctrlPropertySightQueryPrecision":
                    if (result == 0)
                        verified = true;
                    break;
                default:
                    break;
            }

            if (verified == false)
            {
                char [] param = new char [] {'c', 't', 'r', 'l', 'P', 'r', 'o', 'p', 'e', 'r', 't', 'y'};
                MessageBox.Show(this.Name.TrimStart(param) + " property id is not in the correct format, Please correct it and click on the Apply button again", "Wrong Property Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
            return verified;
        }

        public uint RegPropertyID
        {
            get
            {
                return ntxRegPropertyID.GetUInt32();
            }
            set
            {
                ntxRegPropertyID.SetUInt32(value);
            }
        }

        public uint SelPropertyID
        {
            get
            {
                return ntxSelPropertyID.GetUInt32();
            }
            set
            {
                ntxSelPropertyID.SetUInt32(value);
            }
        }

        public bool IsSelectionProperty
        {
            get
            {
                return chxSelectionProperty.Checked;
            }
            set
            {
                chxSelectionProperty.Checked = value;
            }
        }

        protected virtual void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            if (chxSelectionProperty.Checked == false)
                this.SelPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID;
            else
            {
                if (rdbSelShared.Checked == true)
                    this.SelPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID;
            }

            if (tcProperty.TabPages.Count > 1)
                foreach (Control ctr in tcProperty.TabPages[1].Controls)
                {
                    ctr.Enabled = chxSelectionProperty.Checked;
                }

            SetCtrlSelRadioButtonState();
        }

        public void GetCtrlGui()
        {
            SetCtrlSelRadioButtonState();
            SetSelectionPropertyCheckBox();
            SetCtrlRegRadioButtonState();
            
        }

        protected void SetSelectionPropertyCheckBox()
        {
            if ( this.SelPropertyID == (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                this.chxSelectionProperty.Checked = false;
            else
                this.chxSelectionProperty.Checked = true;
        }

        protected void SetCtrlRegRadioButtonState()
        {
            if (this.ntxRegPropertyID.GetUInt32() == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
            {
                this.rdbRegShared.Checked = true;
                this.ntxRegPropertyID.Enabled = false;
            }
            else
            {
                this.rdbRegPrivate.Checked = true;
                this.ntxRegPropertyID.Enabled = true;
            }   
        }

        protected void SetCtrlSelRadioButtonState()
        {
            if (this.SelPropertyID != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
            {
                if (this.ntxSelPropertyID.GetUInt32() == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
                {
                    this.rdbSelShared.Checked = true;
                    this.ntxSelPropertyID.Enabled = false;
                }
                else
                {
                    this.rdbSelPrivate.Checked = true;
                    this.ntxSelPropertyID.Enabled = true;
                }
            }
            else
            {
                this.rdbSelShared.Checked = true;
            }            
        }

        private void btnRegPropertyTable_Click(object sender, EventArgs e)
        {
            Form treeForm = GetParentForm(this);
            


            MethodInfo MI = this.Parent.GetType().GetMethod("GetScheme");

            if(MI != null)
            {
                object[] paramsArr = null;
                object currScheme = MI.Invoke(this.Parent, paramsArr);

                MCTester.ObjectWorld.ObjectsUserControls.frmPropertiesIDList frmConSelector = new MCTester.ObjectWorld.ObjectsUserControls.frmPropertiesIDList((IDNMcObjectScheme)currScheme);
                frmConSelector.Show();
            
            }
        }

        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        private void rdbRegPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRegPrivate.Checked == true)
                this.ntxRegPropertyID.Enabled = true;
        }

        private void rdbRegShared_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRegShared.Checked == true)
            {
                this.ntxRegPropertyID.Enabled = false;
                this.ntxRegPropertyID.SetUInt32((uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
            }
                
        }

        private void rdbSelPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSelPrivate.Checked == true)
                this.ntxSelPropertyID.Enabled = true;
        }

        private void rdbSelShared_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSelShared.Checked == true)
            {
                this.ntxSelPropertyID.Enabled = false;
                this.ntxSelPropertyID.SetUInt32((uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
            }
                
        }
     }
}
