using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers;
using MCTester.Managers.ObjectWorld;
using MCTester.ObjectWorld.Assit_Forms;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyDataHandler<TProperty> : CtrlObjStatePropertyBase
    {

        public Dictionary<byte, TProperty> m_dicStatesValues = new Dictionary<byte, TProperty>();
        public delegate void delegateLoad(out TProperty value, out uint ePredefinedPropertyIDs, DNSByteBool uObjectStateToServe);
        public delegate void delegateLoadWithParentParam(uint numTypeParentIndex, out TProperty value, out uint ePredefinedPropertyIDs, DNSByteBool uObjectStateToServe);
        public delegate void delegateLoadSightColor(DNEPointVisibility eVisibilityType, out DNSMcBColor pColor, out uint puPropertyID, DNSByteBool uObjectStateToServe);
        public delegate void delegateLoadWithoutState(out TProperty value, out uint ePredefinedPropertyIDs);
        public delegate void delegateLoadWithParentParamWithoutState(uint numTypeParentIndex, out TProperty value, out uint ePredefinedPropertyIDs);

        public delegate void delegateSave(TProperty value, uint uPropertyID, DNSByteBool uObjectStateToServe);
        public delegate void delegateSaveWithParentParam(uint numTypeParentIndex, TProperty value, uint uPropertyID, DNSByteBool uObjectStateToServe);
        public delegate void delegateSaveWithoutState(TProperty value, uint uPropertyID);
        public delegate void delegateSaveWithParentParamWithoutState(uint numTypeParentIndex, TProperty value, uint uPropertyID);

        protected TProperty m_InitValue;

        public CtrlObjStatePropertyDataHandler()
        {
            InitializeComponent();
        }

        public TProperty GetSelValByObjectState(byte objectState)
        {
            return m_dicStatesValues[objectState];
        }

        public void SetSelValByObjectState(byte objectState, TProperty value)
        {
            m_dicStatesValues[objectState] = value;
        }

        public bool IsExistObjectState(byte objectState)
        {
            return m_dicStatesValues.ContainsKey(objectState);
        }

        public void OnObjectStateAddData(byte objectState)
        {
            m_dicStatesValues[objectState] = m_InitValue;
        }

        public void OnObjectStateChangedData(byte prevObjectState, TProperty value)
        {
            if (IsExistObjectState(prevObjectState))
            {
                SetSelValByObjectState(prevObjectState, value);
            }
        }
        
       

        internal new TProperty Load(delegateLoadWithParentParam func, uint numAttachPointParentIndex)
        {
            uint m_PropID;
            TProperty m_PropertyParam, m_StatePropertyParam;
            RemoveAllStates();
            byte objectState = 0;
            DNSByteBool dnObjectState = objectState;

            func(numAttachPointParentIndex, out m_PropertyParam, out m_PropID, dnObjectState);
            SavePrivateProperties(func.Target, m_PropID, func.Method.Name);
            MCTPrivatePropertiesData.AddPrivatePropertyControls(m_PropID, m_PropertyParam, this);

            RegPropertyID = m_PropID;
            SetCtrlRegRadioButtonState();

            dnObjectState = 1;
            uint propId = (uint)DNEPredefinedPropertyIDs._EPPI_FIRST_RESERVED_ID;

            while (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
            {
                func(numAttachPointParentIndex, out m_StatePropertyParam, out propId, dnObjectState);
                if (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID &&
                    propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID)
                {
                    MCTPrivatePropertiesData.AddPrivatePropertyControls(propId, m_StatePropertyParam, this);
                    AddObjectState(dnObjectState.AsByte, propId);
                    SetSelPropertyId(dnObjectState.AsByte, propId);
                    SetSelValByObjectState(dnObjectState.AsByte, m_StatePropertyParam);
                    SavePrivateProperties(func.Target, propId, func.Method.Name);
                }

                if (dnObjectState.AsByte == byte.MaxValue)
                {
                    break;
                }

                dnObjectState.AsByte++;

            }
            // SetStateControlsEnabled must be before call function SelectFirstTab(); 
            // in texture/mesh (property with buttons controls (like edit btn, delete btn etc.)) property - 
            // if exist state with null value, the function SetStateControlsEnabled enabled buttons and the function SelectFirstTab() disable the buttons (becuse null value).

            SetStateControlsEnabled(ObjectStates.Length > 0);
            SelectFirstTab();
            if (ObjectStates.Length > 0)
            {
                LoadItemsToListStates(ObjectStates.ToList());
            }

            return m_PropertyParam;
        }

        public new TProperty Load(delegateLoad func)
        {
            uint m_PropID;
            TProperty m_PropertyParam, m_StatePropertyParam;
            RegPropertyID = DNMcConstants._MC_EMPTY_ID; 
            SelPropertyID = DNMcConstants._MC_EMPTY_ID;

            RemovePPFromControl();
            RemoveAllStates();

            byte objectState = 0;
            DNSByteBool dnObjectState = objectState;

            func(out m_PropertyParam, out m_PropID, dnObjectState);
            SavePrivateProperties(func.Target, m_PropID, func.Method.Name);
            MCTPrivatePropertiesData.AddPrivatePropertyControls(m_PropID, m_PropertyParam, this);

            RegPropertyID = m_PropID;
            SetCtrlRegRadioButtonState();

            dnObjectState = 1;
            uint propId = (uint)DNEPredefinedPropertyIDs._EPPI_FIRST_RESERVED_ID;

            while (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
            {
                func(out m_StatePropertyParam, out propId, dnObjectState);
                if (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID &&
                    propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID)
                {
                    MCTPrivatePropertiesData.AddPrivatePropertyControls(propId, m_StatePropertyParam, this);
                    AddObjectState(dnObjectState.AsByte, propId);
                    SetSelPropertyId(dnObjectState.AsByte, propId);
                    SetSelValByObjectState(dnObjectState.AsByte, m_StatePropertyParam);
                    SavePrivateProperties(func.Target, propId, func.Method.Name);
                }

                if (dnObjectState.AsByte == byte.MaxValue)
                {
                    break;
                }
                
                dnObjectState.AsByte++;

            }


            // SetStateControlsEnabled must be before call function SelectFirstTab(); 
            // in texture/mesh (property with buttons controls (like edit btn, delete btn etc.)) property - 
            // if exist state with null value, the function SetStateControlsEnabled enabled buttons and the function SelectFirstTab() disable the buttons (because null value).
            SetStateControlsEnabled(ObjectStates.Length > 0);

            SelectFirstTab();
            if (ObjectStates.Length > 0)
            {
                LoadItemsToListStates(ObjectStates.ToList());
            }    
            

            return m_PropertyParam;
        }

        public new TProperty Load(delegateLoadWithoutState func)
        {
            uint m_PropID;
            TProperty m_PropertyParam;
            RemoveAllStates();
            byte objectState = 0;
            DNSByteBool dnObjectState = objectState;

            func(out m_PropertyParam, out m_PropID);
            SavePrivateProperties(func.Target, m_PropID, func.Method.Name);
            MCTPrivatePropertiesData.AddPrivatePropertyControls(m_PropID, m_PropertyParam, this);
            RegPropertyID = m_PropID;
            SetCtrlRegRadioButtonState();

            SelectFirstTab();

            return m_PropertyParam;
        }

        public new TProperty Load(delegateLoadWithParentParamWithoutState func, uint numAttachPointParentIndex)
        {
            uint m_PropID;
            TProperty m_PropertyParam;
            RemoveAllStates();
            byte objectState = 0;
            DNSByteBool dnObjectState = objectState;

            func(numAttachPointParentIndex, out m_PropertyParam, out m_PropID);
            SavePrivateProperties(func.Target, m_PropID, func.Method.Name);
            MCTPrivatePropertiesData.AddPrivatePropertyControls(m_PropID, m_PropertyParam, this);
            RegPropertyID = m_PropID;
            SetCtrlRegRadioButtonState();

            SelectFirstTab();

            return m_PropertyParam;
        }

        public void LoadStatesBySet(Dictionary<byte, TProperty> dicStatesValues)
        {
            m_dicStatesValues = new Dictionary<byte, TProperty>(dicStatesValues);
        }

        public void SavePrivateProperties(object funcTarget, uint propertyId, string funcMethodName)
        {
            if (funcTarget != null)
            {
                Manager_MCPropertyDescription.InsertNode((IDNMcObjectSchemeNode)funcTarget, propertyId, funcMethodName);
                CurrentObjectSchemeNode = (IDNMcObjectSchemeNode)funcTarget;
            }
        }

        public void Save(delegateSaveWithParentParam func,
                        uint numTypeParentIndex, 
                        TProperty regValue)
        {
            if (RegVerifiedId())
            {
                func(numTypeParentIndex, regValue, RegPropertyID, false);
                SavePrivateProperties(func.Target, RegPropertyID, func.Method.Name);
            }

            DeselectTab();

            byte[] objectStates = ObjectStates;
            byte nLastState = 0;
            foreach (byte objectState in objectStates)
            {
                while (++nLastState < objectState)
                {
                    func(numTypeParentIndex, m_InitValue, (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID, nLastState);
                }
                uint propertyId = GetSelPropertyId(objectState);
                func(numTypeParentIndex, GetSelValByObjectState(objectState),
                    propertyId,
                    objectState);
                SavePrivateProperties(func.Target, propertyId, func.Method.Name);
            }
            if (nLastState < byte.MaxValue)
            {
                nLastState++;
                func(numTypeParentIndex, m_InitValue, (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID, nLastState);
            }

            SelectFirstTab();
        }

       
       

        public void Save(delegateSave func, TProperty regValue)
        {
            if (RegVerifiedId())
            {
                try
                {
                    func(regValue, RegPropertyID, false);
                    SavePrivateProperties(func.Target, RegPropertyID, func.Method.Name);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage(func.Method.Name, McEx);
                    CheckMapCoreException(McEx, RegPropertyID, func);
                }
            }

            DeselectTab();

            byte[] objectStates = ObjectStates;
            byte nLastState = 0;
            uint propertyId = 0;
            foreach (byte objectState in objectStates)
            {
                try
                {
                    while (++nLastState < objectState)
                    {
                        func(m_InitValue, (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID, nLastState);
                    }
                    propertyId = GetSelPropertyId(objectState);
                    TProperty selValByObjectState = GetSelValByObjectState(objectState);
                    func(selValByObjectState,
                        propertyId,
                        objectState);
                    SavePrivateProperties(func.Target, propertyId, func.Method.Name);
                 }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage(func.Method.Name, McEx);
                    CheckMapCoreException(McEx, propertyId, func);
                }
            }
            if (nLastState < byte.MaxValue)
            {
                nLastState++;
                func(m_InitValue, (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID, nLastState);
            }

            SelectFirstTab();
        }

        public void Save(delegateSaveWithoutState func, TProperty regValue)
        {
            if (RegVerifiedId())
            {
                func(regValue, RegPropertyID);
                SavePrivateProperties(func.Target, RegPropertyID, func.Method.Name);
            }

            DeselectTab();

            SelectFirstTab();

        }

        public void Save(delegateSaveWithParentParamWithoutState func,
                       uint numTypeParentIndex,
                       TProperty regValue)
        {
            if (RegVerifiedId())
            {
                func(numTypeParentIndex, regValue, RegPropertyID);
                SavePrivateProperties(func.Target, RegPropertyID, func.Method.Name);
            }

            DeselectTab();

            SelectFirstTab();
        }

        private void CheckMapCoreException(MapCoreException McEx, uint propertyId, delegateSave func)
        {
            if (McEx.ErrorCode == DNEMcErrorCode.PROPERTY_TYPE_MISMATCH)
            {
                frmPrivatePropertiesDescription privatePropertiesDescription = null;
                try
                {
                    IDNMcObjectSchemeNode node = (IDNMcObjectSchemeNode)func.Target;
                    IDNMcObjectScheme scheme = node.GetScheme();
                    privatePropertiesDescription = new frmPrivatePropertiesDescription(scheme, propertyId, "Existing properties with the same id");
                }
                catch (MapCoreException McExInner)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetNodesByPropertyID", McExInner);
                    return;
                }
                if (Manager_MCPropertyDescription.FrmPrivatePropertiesDescription != null)
                    Manager_MCPropertyDescription.FrmPrivatePropertiesDescription.Dispose();
                Manager_MCPropertyDescription.FrmPrivatePropertiesDescription = privatePropertiesDescription;

                privatePropertiesDescription.Show();
            }
        }

        public void SetStateValueByProperty(uint propertyId, TProperty propertyValue)
        {
            if (m_selPropIds.Values.Contains(propertyId))
            {
                foreach (byte state in m_selPropIds.Keys)
                {
                    if (m_selPropIds[state] == propertyId)
                        m_dicStatesValues[state] = propertyValue;
                }
            }
        }
    }
}
