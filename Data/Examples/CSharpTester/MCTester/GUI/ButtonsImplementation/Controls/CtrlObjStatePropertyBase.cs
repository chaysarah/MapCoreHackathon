using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.Managers.ObjectWorld;
using MapCore;
using MapCore.Common;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.ObjectWorld.Assit_Forms;
using MCTester.Managers;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyBase : UserControl
    {
        public virtual void ObjectStateAdd(byte objectState, bool bInitCtrl) { }
        public virtual void ObjectStateRemove(byte objectState) { }
        public virtual void ObjectStateChanged(byte objectState, byte previousState) { }
        public virtual void AddStateVisibleChanged(bool isVisible) { }

        public virtual void SetValue(uint propertyId, object value) { }

        public virtual DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_UINT; }

        public virtual string GetPropertyEnumType() { return ""; }

        public virtual bool PropertyIdChanged() { return true;  }

        public virtual bool SelPropertyIdChanged(uint propertyId) { return true; }

        private MCTPrivatePropertyValidationResult m_PropertyValidationResult;

        public MCTPrivatePropertyValidationResult PropertyValidationResult
        {
            get
            {
                return m_PropertyValidationResult;
            }
            set
            {
                m_PropertyValidationResult = value;
            }
        }

        public bool IsClickApply { set; get; }

        public bool IsClickOK { set; get; }

        IDNMcObjectSchemeNode m_CurrentObjectSchemeNode;

        public IDNMcObjectSchemeNode CurrentObjectSchemeNode
        {
            get { return m_CurrentObjectSchemeNode; }
            set { m_CurrentObjectSchemeNode = value; }
        }

        uint lastRegPropertyId, lastSelPropertyId;
        protected List<Control> m_lstExcludeEnablesContols = new List<Control>();
        protected Dictionary<byte,uint> m_selPropIds = new Dictionary<byte,uint>();

        List<object> lstStatesNames = new List<object>();
        List<byte> lstStateNum = new List<byte>();

        Dictionary<byte, string> dicStatesNames = new Dictionary<byte, string>();

        string m_lastTab;
        int m_nLastIndex = -1;
        bool m_bNoRdbCheck = false;

        public CtrlObjStatePropertyBase()
        {
            InitializeComponent();
            
            // Move the object state group to be a stand alone form
            tcProperty.SelectedTab.Controls.Remove(addObjectState);
            SetStateControlsEnabled(false);
            m_lastTab = tcProperty.TabPages[0].Text;
            dicStatesNames = Manager_MCObjectScheme.DicStatesNames;
            if (dicStatesNames != null && dicStatesNames.Values != null)
                cbNewTabNum.Items.AddRange(dicStatesNames.Values.ToArray());
        }

        public void SetRegPrivatePropertyValidationResult(MCTPrivatePropertyValidationResult propertyValidationResult)
        {
            if (propertyValidationResult.ErrorType != MCTPrivatePropertyValidationResult.EErrorType.None)
            {
                PropertyValidationResult = propertyValidationResult;
                if (PropertyValidationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.InvalidValue)
                {
                    if (IsClickApply)
                        PropertyValidationResult.ErrorMessage = PropertyValidationResult.ErrorMessage.Replace("values.", "values before Apply.");
                    if (IsClickOK)
                        PropertyValidationResult.ErrorMessage = PropertyValidationResult.ErrorMessage.Replace("values.", "values before OK.");
                }
                lblErrorValidation.Text = PropertyValidationResult.ErrorMessage;
                lblErrorValidation.Visible = true;
                //llWarningID.Visible = false;
            }
            else
            {
                SetRegValidationControlVisible(false);
                lblErrorValidation.Visible = false;
               // CheckLinkWarningIDVisibility(llWarningID, RegPropertyID);
            }

            if (propertyValidationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.InvalidValue)
            {
                string strValve1 = MCTPrivatePropertiesData.ConvertPropertyValueToStringByType(GetPropertyType(), PropertyValidationResult.ResultValidation1);
                string strValve2 = MCTPrivatePropertiesData.ConvertPropertyValueToStringByType(GetPropertyType(), PropertyValidationResult.ResultValidation2);
                ttValidationValue1.SetToolTip(btnValidation1, strValve1);
                ttValidationValue2.SetToolTip(btnValidation2, strValve2);
                SetRegValidationControlVisible(true);
            }
            
        }

        public void SetRegValidationControlVisible(bool isVisible)
        {
            isChangeVisibility = true;
            lblErrorValidation.Visible = isVisible;
            btnValidation1.Visible = isVisible;
            btnValidation2.Visible = isVisible;
            if(!isVisible)
            {
                ttValidationValue1.SetToolTip(btnValidation1, "");
                ttValidationValue2.SetToolTip(btnValidation2, "");

            }
            isChangeVisibility = false;
        }

        public void SetSelPrivatePropertyValidationResult(MCTPrivatePropertyValidationResult propertyValidationResult)
        {
            if (propertyValidationResult.ErrorType != MCTPrivatePropertyValidationResult.EErrorType.None)
            {
                PropertyValidationResult = propertyValidationResult;
                if (PropertyValidationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.InvalidValue)
                {
                    if (IsClickApply)
                        PropertyValidationResult.ErrorMessage = PropertyValidationResult.ErrorMessage.Replace("values.", "values before Apply.");
                    if (IsClickOK)
                        PropertyValidationResult.ErrorMessage = PropertyValidationResult.ErrorMessage.Replace("values.", "values before OK.");
                }
                lblSelErrorValidation.Text = PropertyValidationResult.ErrorMessage;
                lblSelErrorValidation.Visible = true;
            }
            else
            {
                SetSelValidationControlVisible(false);
                lblSelErrorValidation.Visible = false;
            }

            if (propertyValidationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.InvalidValue)
            {
                string strValve1 = MCTPrivatePropertiesData.ConvertPropertyValueToStringByType(GetPropertyType(), PropertyValidationResult.ResultValidation1);
                string strValve2 = MCTPrivatePropertiesData.ConvertPropertyValueToStringByType(GetPropertyType(), PropertyValidationResult.ResultValidation2);
                ttValidationValue1.SetToolTip(btnSelValidation1, strValve1);
                ttValidationValue2.SetToolTip(btnSelValidation2, strValve2);
                SetSelValidationControlVisible(true);
            }
        }

        bool isChangeVisibility = false;
        public void SetSelValidationControlVisible(bool isVisible)
        {
            isChangeVisibility = true;
            lblSelErrorValidation.Visible = isVisible;
            btnSelValidation1.Visible = isVisible;
            btnSelValidation2.Visible = isVisible;
            if (!isVisible)
            {
                ttValidationValue1.SetToolTip(btnSelValidation1, "");
                ttValidationValue2.SetToolTip(btnSelValidation2, "");
            }
            isChangeVisibility = false;
        }

        public void SetSelValidationMismatchTypesControlVisible(bool isVisible)
        {
            lblSelErrorValidation.Visible = isVisible;
            if(isVisible)
                lblSelErrorValidation.Text = "";
        }

        public void CheckLinkWarningIDVisibility(LinkLabel linkWarning, uint propertyId)
        {
            try
            {
                if (m_CurrentObjectSchemeNode != null && propertyId != DNMcConstants._MC_EMPTY_ID && propertyId > 0)
                {
                    IDNMcObjectSchemeNode[] nodes = m_CurrentObjectSchemeNode.GetScheme().GetNodesByPropertyID(propertyId);
                    if (nodes != null && nodes.Length > 1)
                        linkWarning.Visible = true;
                    linkWarning.Visible = false;
                }
            }
            catch (MapCoreException )
            { linkWarning.Visible = false; }
        }

        public void HideObjStateTab()
        {
            tcProperty.TabPages.Remove(tpObjectState);
        }

        protected void AddControl(Control ctr)
        {
            m_lstExcludeEnablesContols.Add(ctr);
        }

        protected void SetStateControlsEnabled(bool isEnabled,bool isCanAddState = true)
        {
            foreach (Control ctr in tcProperty.TabPages[1].Controls)
            {
                if (ctr == btnSelAdd || ctr == addObjectState || ctr == ntxSelPropertyID || m_lstExcludeEnablesContols.Contains(ctr))
                    continue;

                ctr.Enabled = isEnabled;
            }
            btnSelAdd.Enabled = isCanAddState;
            if (isEnabled == false)
                rdbSelShared.Checked = true;
        }

        public bool IsExistIdInStates(uint id)
        {
            return (m_selPropIds.Values.Contains(id) || ((SelPropertyID == id) && (rdbSelPrivate.Checked)));
        }

        /*public virtual bool IsExistIdInDicSel(object dicKey, uint id)
        {
            return false;
        }*/

        public virtual bool IsExistIdInDic(object dicKey, uint propertyId, DNSByteBool state)
        {
            return false;
        }

        public List<byte> GetStatesOfId(uint id)
        {
            List<byte> lstStates = new List<byte>();
            foreach (KeyValuePair<byte,uint> stateId in m_selPropIds)
            {
                if (stateId.Value == id)
                    lstStates.Add(stateId.Key);
            }
            if(SelPropertyID == id && !lstStates.Contains(ObjectState.AsByte))
                lstStates.Add(ObjectState.AsByte);
            return lstStates;
        }

        public string PropertyName
        {
            get { return this.groupBox1.Text; }
            set { this.groupBox1.Text = value; }
        }

        public DNSByteBool ObjectState
        {
            get
            {
                byte val = 0;
                if (cmbCurrentObjectState.SelectedIndex >= 0)
                    val = lstStateNum[cmbCurrentObjectState.SelectedIndex];
                return new DNSByteBool(val);
            }
        }

        public bool RegVerifiedId()
        {
            tcProperty.SelectedIndex = 0;
            bool validId = true;
            ntxRegPropertyID.ForeColor = Color.Black;
            if (ObjectPropertiesBase.VerifyPrivatePropertiesId)
            {
                validId = VerificationId(ntxRegPropertyID.GetUInt32());
                if (validId == false)
                    ntxRegPropertyID.ForeColor = Color.Red;
            }

            return validId;
        }

        public bool VerifiedId(int index)
        {
            bool validId = true;
            if (index == -1)
            {
                index = tcProperty.SelectedIndex;
            }

            TabPage tp = tcProperty.TabPages[index];
            NumericTextBox ntxProp = (NumericTextBox)tp.Controls.Find("ntxRegPropertyID", true)[0];
            ntxProp.ForeColor = Color.Black;
            if (ObjectPropertiesBase.VerifyPrivatePropertiesId)
            {
                validId = VerificationId(ntxProp.GetUInt32());
                if (!validId)
                {
                    ntxProp.ForeColor = Color.Red;
                }
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
                char[] param = new char[] { 'c', 't', 'r', 'l', 'P', 'r', 'o', 'p', 'e', 'r', 't', 'y' };
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
               // CheckLinkWarningIDVisibility(llWarningID, RegPropertyID);
            }
        }

        public uint SelPropertyID
        {
            get
            {
                //uint val = 0;
//                 if (m_selPropIds.ContainsKey(ObjectState.AsByte))
//                 {
//                     val = m_selPropIds[ObjectState.AsByte];
//                     ntxSelPropertyID.SetUInt32(val);
//                     return val;
//                 }
//                 else
//                 {
                    return ntxSelPropertyID.GetUInt32();
                //}
            }
            set
            {
                ntxSelPropertyID.SetUInt32(value);
                if (m_selPropIds.ContainsKey(ObjectState.AsByte))
                {
                    m_selPropIds[ObjectState.AsByte] = value;
                }
            }
        }

        public void SetSelPropertyID(uint id)
        {
            ntxSelPropertyID.SetUInt32(id);
        }

        protected void SetCtrlRegRadioButtonState()
        {
            if (RegPropertyID == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
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

        public uint GetSelPropertyId(byte objectState)
        {
            return m_selPropIds[objectState];
        }

        public void SetCurrentSelPropertyId(byte objectState, uint value)
        {
            if(objectState != 0 && value != 0)
                m_selPropIds[objectState] = value;
        }

        public void SetSelPropertyId(byte objectState, uint value)
        {
            m_selPropIds[objectState] = value;
            if (objectState == ObjectState.AsByte)
            {
                SelPropertyID = value;
            }
            else
            {
                ntxSelPropertyID.SetUInt32(value);
            }
        }

        /*public uint PropertyID
        {
            get
            {
                return ntxRegPropertyID.GetUInt32();
            }
            set
            {
                ntxRegPropertyID.SetUInt32(value);
                
            }
        }*/

        public void LoadPropIdBySet(Dictionary<byte,uint> selPropIds)
        {
            m_selPropIds = new Dictionary<byte, uint>(selPropIds);
            foreach (byte state in selPropIds.Keys)
            {
                uint value = selPropIds[state];
                AddObjectState(state, value);
                SetSelPropertyId(state, value);
            }
        }

        public void AddObjectState(byte objectState, uint val)
        {
            //m_selPropIds.Add(objectState, val);
            m_selPropIds[objectState] = val;
            lstStateNum.Add(objectState);
            if(dicStatesNames != null && dicStatesNames.Keys.Contains(objectState))
                cmbCurrentObjectState.Items.Add(dicStatesNames[objectState]);

            //cmbCurrentObjectState.Items.Add(objectState.ToString());

            ObjectStateAdd(objectState, false);
        }

        public void SelectFirstObjectState()
        {
            if (cmbCurrentObjectState.Items.Count > 0)
            {
                cmbCurrentObjectState.SelectedIndex = 0;
            }
            else
            {
                cmbCurrentObjectState.SelectedIndex = -1;
            }
        }

        public void SelectFirstTab()
        {
            cmbCurrentObjectState.SelectedIndex = -1;
            btnSelRemove.Enabled = false;

            if (cmbCurrentObjectState.Items.Count > 0)
            {
                cmbCurrentObjectState.SelectedIndex = 0;
                btnSelRemove.Enabled = true;
            }
            else
            {
                DeselectTab();
            }
        }

        public void DeselectTab()
        {
            cmbCurrentObjectState.SelectedIndex = -1;
            btnSelRemove.Enabled = false;

           // rdbSelShared.Enabled = true;
        }

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            tcProperty.SelectedTab = tpObjectState;

            DeselectTab();

            addObjectState.Visible = true;
            addObjectState.BringToFront();
        }

        private void btnRemoveTab_Click(object sender, EventArgs e)
        {

            uint currObjectState = ObjectState.AsByte;
            uint propertyId = SelPropertyID;

            m_selPropIds.Remove((byte)currObjectState);
            lstStateNum.RemoveAt(cmbCurrentObjectState.SelectedIndex);
            cmbCurrentObjectState.Items.RemoveAt(cmbCurrentObjectState.SelectedIndex);

            byte value, next_value;
            if (currObjectState > 0)
            {
                for (int i = 0; i < (cbNewTabNum.Items.Count - 1); i++)
                {
                    value = GetStateNumFromCB(cbNewTabNum.Items[i]);
                    next_value = GetStateNumFromCB(cbNewTabNum.Items[i + 1]);

                    if (value > currObjectState)
                    {
                        cbNewTabNum.Items.Insert(i, dicStatesNames[(byte)currObjectState]);
                        break;
                    }
                    else if (value < currObjectState && next_value > currObjectState)
                    {
                        cbNewTabNum.Items.Insert(i + 1, dicStatesNames[(byte)currObjectState]);
                        break;
                    }
                    else if (next_value < currObjectState && i == cbNewTabNum.Items.Count - 2)
                    {
                        cbNewTabNum.Items.Insert(i + 2, dicStatesNames[(byte)currObjectState]);
                        break;
                    }
                }
            }

            m_nLastIndex = -1;

            if (cmbCurrentObjectState.Items.Count == 0)
            {
                SetStateControlsEnabled(false);
                cmbCurrentObjectState_SelectedIndexChanged(this, new EventArgs());
            }
            else
            {
                SelectFirstTab();
            }

            ObjectStateRemove((byte)currObjectState);
            MCTPrivatePropertiesData.RemovePropertyId(propertyId, this);

        }

        protected void RemovePPFromControl()
        {
            List<uint> ids = m_selPropIds.Values.ToList();
            m_selPropIds.Clear();
            foreach (uint id in ids)
            {
                MCTPrivatePropertiesData.RemovePropertyId(id, this);
            }
            MCTPrivatePropertiesData.RemovePropertyId(RegPropertyID, this);
        }

        protected void RemoveAllStates()
        {
            cmbCurrentObjectState.Items.Clear();
            cmbCurrentObjectState.SelectedIndex = -1;
            cmbCurrentObjectState.Text = "";
            cmbCurrentObjectState.SelectedText = "";
            m_nLastIndex = -1;
            lstStateNum.Clear();
            ChangeTabText();
            tcProperty.SelectedIndex = 0;
        }

        private byte GetNextAvailableObjectStateId()
        {
            byte nObjectState = 0;

            foreach (byte state in lstStateNum)
            {
                if (state > nObjectState)
                {
                    nObjectState = state;
                }
            }

            return nObjectState++;
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
                MCTPrivatePropertiesData.RemovePropertyId(lastRegPropertyId, this);
                SetRegValidationControlVisible(false);
            }
        }

        private void rdbSelPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bNoRdbCheck)
            {
                return;
            }
            else if (rdbSelPrivate.Checked)
            {
                ntxSelPropertyID.Enabled = true;
            }
        }

        private void rdbSelShared_CheckedChanged(object sender, EventArgs e)
        {
            if (m_bNoRdbCheck)
            {
                return;
            }
            else if (rdbSelShared.Checked)
            {
                this.ntxSelPropertyID.Enabled = false;
                SelPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID;
                MCTPrivatePropertiesData.RemovePropertyId(ntxSelPropertyID.GetUInt32(), this); // if change private property to shared
                SetSelValidationControlVisible(false);
            }
        }

        private int FindInsertIndex(byte desiredState)
        {
            for (int i = 0; i < cmbCurrentObjectState.Items.Count; i++ )
            {
                byte currState = lstStateNum[i];
                if (currState > desiredState)
                {
                    return i;
                }
            }
            return -1;
        }

        private byte GetStateNumFromCB(object SelectedItem)
        {
            byte newNumNumber = 0;
            string selectText;
            string[] texts;
            if (SelectedItem != null && SelectedItem.ToString() != "")
            {
                selectText = SelectedItem.ToString();

                if (selectText.Contains('('))
                {
                    texts = selectText.Split('(');
                    if (texts != null && texts.Length > 0)
                        newNumNumber = byte.Parse(texts[0]);
                }
                else
                    newNumNumber = byte.Parse(selectText.Trim());
            }
            return newNumNumber;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            byte newNumNumber = GetStateNumFromCB(cbNewTabNum.SelectedItem);

            if (newNumNumber > 0)
            {
                if (m_selPropIds.ContainsKey(newNumNumber))
                {
                    MessageBox.Show("Number already exist", "Invalid Number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                addObjectState.Visible = false;
                m_selPropIds.Add(newNumNumber, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                int index = FindInsertIndex(newNumNumber);
                if (index >= 0)
                {
                    lstStateNum.Insert(index, newNumNumber);
                    cmbCurrentObjectState.Items.Insert(index, dicStatesNames[newNumNumber]);
                }
                else
                {
                    lstStateNum.Add(newNumNumber);
                    cmbCurrentObjectState.Items.Add(dicStatesNames[newNumNumber]);
                    index = cmbCurrentObjectState.Items.Count - 1;
                }
                m_bNoRdbCheck = true;
                if (SelPropertyID != (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
                {
                    rdbSelPrivate.Checked = true;
                    ntxSelPropertyID.Enabled = false;
                }
                else
                {
                    rdbSelShared.Checked = false;
                    ntxSelPropertyID.Enabled = true;
                }

                rdbSelShared.Checked = true;
                ntxSelPropertyID.Enabled = false;

                m_bNoRdbCheck = false;

                if (cmbCurrentObjectState.Items.Count > 0)
                    SetStateControlsEnabled(true);

                ObjectStateAdd(newNumNumber, true);
                cmbCurrentObjectState.SelectedIndex = index;

                cbNewTabNum.Items.RemoveAt(cbNewTabNum.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Select Value From List", "Missing Value", MessageBoxButtons.OK);
                cbNewTabNum.Select();
            }

        }

        protected void LoadItemsToListStates(List<byte> lstStatesToRemove)
        {
            cbNewTabNum.Items.Clear();
            if (dicStatesNames != null && dicStatesNames.Keys != null)
            {
                Dictionary<byte, string> dicTempStates = new Dictionary<byte,string>(dicStatesNames);
                foreach (byte state in lstStatesToRemove)
                {
                    dicTempStates.Remove(state);
                }
                cbNewTabNum.Items.AddRange(dicTempStates.Values.ToArray());
            }
        }

        protected void ChangeTabText()
        {
            tpObjectState.Text = "Special states (" + cmbCurrentObjectState.Items.Count.ToString() + ")";
        }

        private void cmbCurrentObjectState_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint currObjectState = 0;
            uint prevObjectState = 0;

            if (m_nLastIndex >= 0)
            {
                prevObjectState = lstStateNum[m_nLastIndex];
                m_selPropIds[(byte)prevObjectState] = ntxSelPropertyID.GetUInt32();
            }

            ChangeTabText();
            int index = cmbCurrentObjectState.SelectedIndex;
            if (index >= 0)
            {
                currObjectState = lstStateNum[index];
                SelPropertyID = m_selPropIds[(byte)currObjectState];
                btnSelRemove.Enabled = true;
            }
            else
            {
                cmbCurrentObjectState.Text = "";
                btnSelRemove.Enabled = false;
                ntxSelPropertyID.SetUInt32((uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                currObjectState = 255;
            }


            m_bNoRdbCheck = true;
            if (SelPropertyID == (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID)
            {
                rdbSelShared.Checked = true;
                ntxSelPropertyID.Enabled = false;
            }
            else
            {
                rdbSelPrivate.Checked = true;
                ntxSelPropertyID.Enabled = true;
            }
            m_bNoRdbCheck = false;


            m_nLastIndex = cmbCurrentObjectState.SelectedIndex;
            uint currPropId = SelPropertyID;

            ObjectStateChanged((byte)currObjectState, (byte)prevObjectState);
        }

        public byte[] ObjectStates 
        {
            get
            {
                List<byte> result = new List<byte>();
                foreach (byte item in lstStateNum)
                {
                    result.Add(item);
                }
                return result.ToArray();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            addObjectState.Visible = false;
        }

        private void addObjectState_VisibleChanged(object sender, EventArgs e)
        {
            AddStateVisibleChanged(addObjectState.Visible);
        }

        public bool CheckIsNeedToDoValidation()
        {
            CheckUserClickSave();

            if (isChangeVisibility)
                return false;

            // check if user click cancel - not need to do validation.
            Control control = this.Parent.Parent.Parent;
            Form activeForm = FindForm();
            if (activeForm != null)
            {
                Control[] cancel_controls = activeForm.Controls.Find("btnCancel", true);
                if (cancel_controls != null && cancel_controls.Length > 0)
                {
                    return (!cancel_controls[0].Focused);
                }
            }
            return false;
        }

        public void AfterValidationSucceed()
        {

        }

        public void FocusRegPropertyID()
        {
            ntxRegPropertyID.Focus();
        }

        public void FocusSelPropertyID()
        {
            ntxSelPropertyID.Focus();
        }

        public void CheckUserClickSave()
        {
            // check if user click save - click save.
            Control control = this.Parent.Parent.Parent;
            Form activeForm = FindForm();
            if (activeForm != null)
            {
                Control[] ok_controls = activeForm.Controls.Find("btnOK", true);
                Control[] apply_controls = activeForm.Controls.Find("btnApply", true);
                 
                if (ok_controls != null && ok_controls.Length > 0)
                {
                    IsClickOK = ok_controls[0].Focused;
                }
                if (apply_controls != null && apply_controls.Length > 0)
                {
                    IsClickApply = apply_controls[0].Focused;
                }
            }
        }

        private void ntxRegPropertyID_Enter(object sender, EventArgs e)
        {
            lastRegPropertyId = ntxRegPropertyID.GetUInt32();
        }

        private void cbNewTabNum_TextChanged(object sender, EventArgs e)
        {
            if(cbNewTabNum.Text != "" && cbNewTabNum.SelectedIndex == -1)
            {
                MessageBox.Show("Select Value From List", "Missing Value", MessageBoxButtons.OK);
                cbNewTabNum.Text = "";
            }
        }

        private void ntxRegPropertyID_Validating(object sender, CancelEventArgs e)
        {
            // remove this control from private property dic
            MCTPrivatePropertiesData.RemovePropertyId(lastRegPropertyId, RegPropertyID, this);
            
            if (CheckIsNeedToDoValidation())
            {
                if (PropertyIdChanged())
                {
                    AfterValidationSucceed();
                    lastRegPropertyId = RegPropertyID;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void ntxSelPropertyID_Validating(object sender, CancelEventArgs e)
        {
            // save current value
            SetCurrentSelPropertyId(ObjectState.AsByte, SelPropertyID);

            // remove this control from private property dic
            MCTPrivatePropertiesData.RemovePropertyId(lastSelPropertyId, SelPropertyID, this);

            if (CheckIsNeedToDoValidation())
            {
                if (SelPropertyIdChanged(SelPropertyID))
                {
                    AfterValidationSucceed();
                    lastSelPropertyId = SelPropertyID;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnValidation1_Click(object sender, EventArgs e)
        {
            SetValue(RegPropertyID, PropertyValidationResult.ResultValidation1);
            MCTPrivatePropertiesData.AddPrivatePropertyControls(RegPropertyID, PropertyValidationResult.ResultValidation1, this);
            SetRegValidationControlVisible(false);
        }

        private void btnValidation2_Click(object sender, EventArgs e)
        {
            MCTPrivatePropertiesData.SetControlsValue(RegPropertyID, PropertyValidationResult.ResultValidation2, PropertyValidationResult.IsAddIdToDiffMCList, this);
            SetRegValidationControlVisible(false);
        }

        private void btnSelValidation1_Click(object sender, EventArgs e)
        {
            SetValue(SelPropertyID, PropertyValidationResult.ResultValidation1);
            MCTPrivatePropertiesData.AddPrivatePropertyControls(SelPropertyID, PropertyValidationResult.ResultValidation1, this);
            SetSelValidationControlVisible(false);
        }

        private void btnSelValidation2_Click(object sender, EventArgs e)
        {
            MCTPrivatePropertiesData.SetControlsValue(SelPropertyID, PropertyValidationResult.ResultValidation2, PropertyValidationResult.IsAddIdToDiffMCList, this);
            SetSelValidationControlVisible(false);
        }

        private void btnWarningID_Click(object sender, EventArgs e)
        {
            OpenPropertyIdListForm(RegPropertyID);
        }

        private void btnRegWarningID_Click(object sender, EventArgs e)
        {
            OpenPropertyIdListForm(SelPropertyID);
        }

        private void OpenPropertyIdListForm(uint propertyId)
        {
            frmPrivatePropertiesDescription privatePropertiesDescription = null;
            try
            {
                privatePropertiesDescription = new frmPrivatePropertiesDescription(m_CurrentObjectSchemeNode.GetScheme(), RegPropertyID, "Properties using this id");
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetNodesByPropertyID", McEx);
                return;
            }

            if (Manager_MCPropertyDescription.FrmPrivatePropertiesDescription != null)
                Manager_MCPropertyDescription.FrmPrivatePropertiesDescription.Dispose();
            Manager_MCPropertyDescription.FrmPrivatePropertiesDescription = privatePropertiesDescription;
            privatePropertiesDescription.Show();
        }

        private void llWarningID_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenPropertyIdListForm(RegPropertyID);
        }

        private void ntxSelPropertyID_Enter(object sender, EventArgs e)
        {
            lastSelPropertyId = SelPropertyID;
        }
    }
}
