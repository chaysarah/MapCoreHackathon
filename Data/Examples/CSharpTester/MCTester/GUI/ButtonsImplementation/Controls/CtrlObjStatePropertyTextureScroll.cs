using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyTextureScroll : CtrlObjStatePropertyDataHandlerTextureScroll
    {
        private IDNMcMeshItem m_currentMesh;

        DNSByteBool currState = false;
        uint m_textureId;

        private delegateLoadDic delFuncLoad;

        public IDNMcMeshItem GetCurrentMesh(){ return m_currentMesh; }

        public void SetCurrentMesh(IDNMcMeshItem value)
        {
            m_currentMesh = value;
            if (m_currentMesh != null)
            {
                delFuncLoad = m_currentMesh.GetTextureScrollSpeed;
                CurrentObjectSchemeNode = m_currentMesh;
            }
        }

        public CtrlObjStatePropertyTextureScroll()
        {
            InitializeComponent();
            m_InitValue = new DNSMcFVector2D();

            SetStateControlsEnabled(false, false);
        }

        public uint MeshTextureID
        {
            get
            {
                return m_textureId;
            }
            set
            {
                m_textureId = value;
            }
        }

        public DNSMcFVector2D RegFvector2DScrollSpeed
        {
            get
            {
                return ctrl2DRegFVector.GetVector2D();
            }
            set
            {
                ctrl2DRegFVector.SetVector2D(value);
            }
        }

        public DNSMcFVector2D SelFvector2DScrollSpeed
        {
            get
            {
                return ctrl2DSelFVector.GetVector2D();
            }
            set
            {
                ctrl2DSelFVector.SetVector2D(value);
            }
        }

        private new void LoadStatesByKey(uint selectedSelKey)
        {
            RemoveAllStates();

            if (!IsExistKey(selectedSelKey))
                LoadStates(delFuncLoad);

            base.LoadStatesByKey(selectedSelKey);

            // load first value if exist
            if (ObjectStates.Length > 0)
            {
                SelFvector2DScrollSpeed = base.GetSelValByKeyAndObjectState(selectedSelKey, CurrState.AsByte);
            }
            else
            {
                SelFvector2DScrollSpeed = m_InitValue;
            }
        }

        public new void Save(delegateSaveDic func)
        {
            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID)
                SaveCurrentMeshTextureID();

            base.Save(func);
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID)
                OnObjectStateAddData(MeshTextureID, objectState, bInitCtrl);
            EnabledStateButtons(true);

        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            bool isEnabled = false;
            OnObjectStateChangedData(MeshTextureID, previousState, ctrl2DSelFVector.GetVector2D(), objectState);

            if (IsExistObjectStateByKey(MeshTextureID, objectState))
                SelFvector2DScrollSpeed = GetSelValByKeyAndObjectState(MeshTextureID, objectState);

            if (ObjectStates != null && ObjectStates.Length > 0)
                isEnabled = true;
            EnabledStateButtons(isEnabled);
        }

        public override void ObjectStateRemove(byte objectState)
        {
            RemoveValueByKeyAndObjectState(MeshTextureID, objectState);
        }

        private void EnabledStateButtons(bool isEnabled)
        {
            ctrl2DSelFVector.Enabled = isEnabled;
            ctrl2DRegFVector.Enabled = isEnabled;

        }

        private void SaveCurrentMeshTextureID()
        {
            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID)
            {
                SaveCurrentValue(MeshTextureID, RegFvector2DScrollSpeed, false);
                if (ObjectStates.Length > 0)
                {
                    SaveCurrentKey(MeshTextureID);
                    if (CurrState.AsBool == true)
                        SaveCurrentValue(MeshTextureID, SelFvector2DScrollSpeed, CurrState);
                }
            }
        }

        public uint GetAttachPointID()
        {
            try
            {
                return uint.Parse(ntxRegMeshTextureID.Text);
            }
            catch
            {
                return DNMcConstants._MC_EMPTY_ID;
            }
        }


        private void ntxRegMeshTextureID_TextChanged(object sender, EventArgs e)
        {
            SaveCurrentMeshTextureID();
            MeshTextureID = GetAttachPointID();

            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID)
            {
                if (!IsExistRegKey(MeshTextureID))
                {
                    List<uint> list = new List<uint>();
                    list.Add(MeshTextureID);

                    LoadKeyList(list);
                    try
                    {
                        LoadRegular(delFuncLoad);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetTextureScrollSpeed", McEx);
                    }
                }

                RegFvector2DScrollSpeed = GetRegularCurrentValue(MeshTextureID);
                LoadRegularPropertyID(MeshTextureID);

                LoadStatesByKey(MeshTextureID);
                ctrl2DRegFVector.Enabled = true;
                SetStateControlsEnabled(ObjectStates.Length > 0);
            }
            else
            {
                EnabledStateButtons(false);
                SetStateControlsEnabled(false, false);
                RegFvector2DScrollSpeed = m_InitValue;
                SelFvector2DScrollSpeed = m_InitValue;
                ChangeTabText();
                RemoveAllStates();
            }
        }

        public override bool PropertyIdChanged()
        {
            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                SaveCurrentMeshTextureID();
                if (CurrentObjectSchemeNode != null)
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFvector2DScrollSpeed, this, new DNSByteBool(0), MeshTextureID);
                    SetRegPrivatePropertyValidationResult(validationResult);
                    return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
                }
                return false;
            }
            else
                return true;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID && rdbSelPrivate.Checked)
            {
                SaveCurrentMeshTextureID();
                if (CurrentObjectSchemeNode != null)
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFvector2DScrollSpeed, this, ObjectState, MeshTextureID);
                    SetSelPrivatePropertyValidationResult(validationResult);
                    return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
                }
                return false;
            }
            else
                return true;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FVECTOR2D; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegFvector2DScrollSpeed = (DNSMcFVector2D)value;
            if (SelPropertyID == propertyId)
                SelFvector2DScrollSpeed = (DNSMcFVector2D)value;
            SetStateValueByProperty(propertyId, (DNSMcFVector2D)value);
        }

        private void ctrl2DRegFVector_Validating(object sender, CancelEventArgs e)
        {
            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                SaveCurrentMeshTextureID();
                if (CheckIsNeedToDoValidation())
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFvector2DScrollSpeed, this, new DNSByteBool(0), MeshTextureID);
                    SetRegPrivatePropertyValidationResult(validationResult);
                    if (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None)
                    {
                        AfterValidationSucceed();
                    }
                    else // user click cancel
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void ctrl2DSelFVector_Validating(object sender, CancelEventArgs e)
        {
            if (MeshTextureID != DNMcConstants._MC_EMPTY_ID && rdbSelPrivate.Checked)
            {
                SaveCurrentMeshTextureID();

                if (CheckIsNeedToDoValidation())
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFvector2DScrollSpeed, this, ObjectState, MeshTextureID);
                    SetSelPrivatePropertyValidationResult(validationResult);
                    if (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None)
                    {
                        AfterValidationSucceed();
                    }
                    else // user click cancel
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }

    public class CtrlObjStatePropertyDataHandlerTextureScroll : CtrlObjStatePropertyDictionaryDataHandler<uint, DNSMcFVector2D> { }
}
