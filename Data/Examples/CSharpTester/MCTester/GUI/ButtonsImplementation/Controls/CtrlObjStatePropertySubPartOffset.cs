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
    public partial class CtrlObjStatePropertySubPartOffset : CtrlObjStatePropertyDataHandlerSubPartOffset
    {
        private IDNMcMeshItem m_currentMesh;

        DNSByteBool currState = false;
        uint m_attachPointId;

        private delegateLoadDic delFuncLoad;

        public IDNMcMeshItem GetCurrentMesh(){ return m_currentMesh; }

        public void SetCurrentMesh(IDNMcMeshItem value)
        {
            m_currentMesh = value;
            if (m_currentMesh != null)
            {
                delFuncLoad = m_currentMesh.GetSubPartOffset;
                CurrentObjectSchemeNode = m_currentMesh;
            }
        }

        public CtrlObjStatePropertySubPartOffset()
        {
            InitializeComponent();
            m_InitValue = new DNSMcFVector3D();

            SetStateControlsEnabled(false, false);
        }

        public uint AttachPointID
        {
            get
            {
                return m_attachPointId;
            }
            set
            {
                m_attachPointId = value;
            }
        }

        public DNSMcFVector3D GetRegFvector3DSubPartOffset()
        {
            return ctrlReg3DFVectorOffset.GetVector3D();
        }

        public void SetRegFvector3DSubPartOffset(DNSMcFVector3D value)
        {
            ctrlReg3DFVectorOffset.SetVector3D(value);
        }

        public DNSMcFVector3D GetSelFvector3DSubPartOffset()
        {
            return ctrlSel3DFVectorOffset.GetVector3D();
        }

        public void SetSelFvector3DSubPartOffset(DNSMcFVector3D value)
        {
            ctrlSel3DFVectorOffset.SetVector3D(value);
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
                SetSelFvector3DSubPartOffset(base.GetSelValByKeyAndObjectState(selectedSelKey, CurrState.AsByte));
            }
            else
            {
                SetSelFvector3DSubPartOffset(m_InitValue);
            }
        }

        public new void Save(delegateSaveDic func)
        {
            if(AttachPointID!= DNMcConstants._MC_EMPTY_ID)
                SaveCurrentAttachPointID();

            base.Save(func);
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            if(AttachPointID != DNMcConstants._MC_EMPTY_ID)
                OnObjectStateAddData(AttachPointID, objectState, bInitCtrl);
            EnabledStateButtons(true);
            
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            bool isEnabled = false;
            OnObjectStateChangedData(AttachPointID, previousState, GetSelFvector3DSubPartOffset(), objectState);

            if (IsExistObjectStateByKey(AttachPointID, objectState))
                SetSelFvector3DSubPartOffset(GetSelValByKeyAndObjectState(AttachPointID, objectState));

            if (ObjectStates != null && ObjectStates.Length > 0)
                isEnabled = true;
            EnabledStateButtons(isEnabled);
        }

        public override void ObjectStateRemove(byte objectState)
        {
            RemoveValueByKeyAndObjectState(AttachPointID, objectState);
        }

        private void EnabledStateButtons(bool isEnabled)
        {
            ctrlSel3DFVectorOffset.Enabled = isEnabled;
            ctrlReg3DFVectorOffset.Enabled = isEnabled;

        }

        private void SaveCurrentAttachPointID()
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
            {
                SaveCurrentValue(AttachPointID, GetRegFvector3DSubPartOffset(), false);
                if (ObjectStates.Length > 0)
                {
                    SaveCurrentKey(AttachPointID);
                    if(CurrState.AsBool == true)
                        SaveCurrentValue(AttachPointID, GetSelFvector3DSubPartOffset(), CurrState);
                }
            }
        }

        public uint GetAttachPointID()
        {
            try
            {
                return uint.Parse(ntxAttachPointID.Text);
            }
            catch
            {
                return DNMcConstants._MC_EMPTY_ID;
            }
        }


        private void ntxAttachPointID_TextChanged(object sender, EventArgs e)
        {
            SaveCurrentAttachPointID();
            AttachPointID = GetAttachPointID();

            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
            {
                if (!IsExistRegKey(AttachPointID))
                {
                    List<uint> list = new List<uint>();
                    list.Add(AttachPointID);

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

                SetRegFvector3DSubPartOffset(GetRegularCurrentValue(AttachPointID));
                LoadRegularPropertyID(AttachPointID);

                LoadStatesByKey(AttachPointID);
                ctrlReg3DFVectorOffset.Enabled = true;
                SetStateControlsEnabled(ObjectStates.Length > 0);
            }
            else
            {
                EnabledStateButtons(false);
                SetStateControlsEnabled(false, false);
                SetRegFvector3DSubPartOffset(m_InitValue);
                SetSelFvector3DSubPartOffset(m_InitValue);
                ChangeTabText();
                RemoveAllStates();
            }
        }

        public override bool PropertyIdChanged()
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                SaveCurrentAttachPointID();
                if (CurrentObjectSchemeNode != null)
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegFvector3DSubPartOffset(), this, new DNSByteBool(0), AttachPointID);
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
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbSelPrivate.Checked)
            {
                SaveCurrentAttachPointID();
                if (CurrentObjectSchemeNode != null)
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelFvector3DSubPartOffset(), this, ObjectState, AttachPointID);
                    SetSelPrivatePropertyValidationResult(validationResult);
                    return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
                }
                return false;
            }
            else
                return true;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FVECTOR3D; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetRegFvector3DSubPartOffset((DNSMcFVector3D)value);
            if (SelPropertyID == propertyId)
                SetSelFvector3DSubPartOffset((DNSMcFVector3D)value);
            SetPropertyById(propertyId, (DNSMcFVector3D)value);
        }

        private void ctrlReg3DFVectorOffset_Validating(object sender, CancelEventArgs e)
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                SaveCurrentAttachPointID();
                if (CheckIsNeedToDoValidation())
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegFvector3DSubPartOffset(), this, new DNSByteBool(0), AttachPointID);
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

        private void ctrlSel3DFVectorOffset_Validating(object sender, CancelEventArgs e)
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbSelPrivate.Checked)
            {
                SaveCurrentAttachPointID();
                if (CheckIsNeedToDoValidation())
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelFvector3DSubPartOffset(), this, ObjectState, AttachPointID);
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

    public class CtrlObjStatePropertyDataHandlerSubPartOffset : CtrlObjStatePropertyDictionaryDataHandler<uint, DNSMcFVector3D> { }
}
