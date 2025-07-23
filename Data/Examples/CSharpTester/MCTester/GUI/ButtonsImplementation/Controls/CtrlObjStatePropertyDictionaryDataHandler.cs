using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyDictionaryDataHandler<TKey, TProperty> : CtrlObjStatePropertyDataHandler<TProperty>
    {
        protected Dictionary<TKey, Dictionary<byte, TProperty>> m_dicKeyStatesValues = new Dictionary<TKey, Dictionary<byte, TProperty>>();
        protected Dictionary<TKey, Dictionary<byte, uint>> m_dicSelPropIds = new Dictionary<TKey, Dictionary<byte, uint>>();
        protected Dictionary<TKey, TProperty> m_dicKeyRegValues = new Dictionary<TKey, TProperty>();
        protected Dictionary<TKey, uint> m_dicKeyRegPropertyIdValues = new Dictionary<TKey, uint>();

        protected List<TKey> m_listKeyValues;
        protected uint selectedStateKey;
        protected DNSByteBool CurrState = false;

        public delegate void delegateLoadDic(TKey key, out TProperty value, out uint puPropertyID, DNSByteBool uObjectStateToServe);
        public delegate void delegateLoadDicWithoutState(TKey key, out TProperty value, out uint puPropertyID);
        public delegate void delegateSaveDic(TKey key, TProperty value, uint uPropertyID, DNSByteBool uObjectStateToServe);
        public delegate void delegateSaveDicWithoutState(TKey key, TProperty value, uint uPropertyID);

        protected CtrlObjStatePropertyDictionaryDataHandler()
        {
            InitializeComponent();
            ResetRegularDic();
        }

        protected void LoadKeyList(List<TKey> listKeyValues)
        {
            m_listKeyValues = listKeyValues;
        }

        protected void AddKeyToList(TKey newKey)
        {
            if (!m_listKeyValues.Contains(newKey))
                m_listKeyValues.Add(newKey);
        }

        protected void SaveCurrentValue(TKey selectedKey, TProperty value, DNSByteBool state)
        {
            if (state.AsBool == false)   // save regular value
            {
                m_dicKeyRegValues[selectedKey] = value;
                m_dicKeyRegPropertyIdValues[selectedKey] = RegPropertyID;
            }
            else                        // save state value
            {
                if (!IsExistKey(selectedKey))
                    m_dicKeyStatesValues[selectedKey] = new Dictionary<byte, TProperty>();
                SetSelValByKeyAndObjectState(selectedKey, state.AsByte, value);
            }
        }

        protected bool IsExistRegKey(TKey selectedKeyReg)
        {
            return m_dicKeyRegValues.ContainsKey(selectedKeyReg);
        }

        protected TProperty GetRegularCurrentValue(TKey selectedKeyReg)
        {
            if (IsExistRegKey(selectedKeyReg))
                return m_dicKeyRegValues[selectedKeyReg];
            return default(TProperty);
        }

        protected void LoadRegularPropertyID(TKey selectedKey)
        {
            RegPropertyID = GetRegularPropertyId(selectedKey);
            SetCtrlRegRadioButtonState();
        }

        protected uint GetRegularPropertyId(TKey selectedKeyReg)
        {
            if (IsExistRegKey(selectedKeyReg))
                return m_dicKeyRegPropertyIdValues[selectedKeyReg];
            return (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID;
        }

        protected void ResetRegularDic()
        {
            m_dicKeyRegValues = new Dictionary<TKey, TProperty>();
            m_dicKeyRegPropertyIdValues = new Dictionary<TKey, uint>();
        }

        protected void AddValueRegularDic(TKey key, TProperty value, uint propId)
        {
            if (m_dicKeyRegValues.ContainsKey(key))
            {
                m_dicKeyRegPropertyIdValues[key] = propId;
                m_dicKeyRegValues[key] = value;
            }
            else
            {
                m_dicKeyRegPropertyIdValues.Add(key, propId);
                m_dicKeyRegValues.Add(key, value);
            }
        }

        protected Dictionary<byte, TProperty> GetSelStatesDicByKey(TKey Key)
        {
            return m_dicKeyStatesValues[Key];
        }

        protected byte[] GetSelStatesArrByKey(TKey Key)
        {
            return m_dicKeyStatesValues[Key].Keys.ToArray();
        }

        protected void SetSelStatesDicByKey(TKey Key, Dictionary<byte, TProperty> value)
        {
            m_dicKeyStatesValues[Key] = value;
        }

        protected bool IsExistKey(TKey Key)
        {
            return m_dicKeyStatesValues != null && m_dicKeyStatesValues.ContainsKey(Key);
        }

        protected void OnKeyAddData(TKey Key)
        {
            m_dicKeyStatesValues[Key] = new Dictionary<byte, TProperty>();
        }

        protected void OnObjectStateAddData(TKey Key, byte objectState, bool bInitCtrl)
        {
            if (bInitCtrl)
                m_dicKeyStatesValues[Key][objectState] = m_InitValue;
            CurrState = objectState;
        }

        protected void OnObjectStateChangedData(TKey prevKey, byte prevObjectState, TProperty value, byte objectState)
        {
            if (IsExistObjectStateByKey(prevKey, prevObjectState))
            {
                SetSelValByKeyAndObjectState(prevKey, prevObjectState, value);
            }
            CurrState = objectState;
        }

        protected TProperty GetSelValByKeyAndObjectState(TKey Key, byte objectState)
        {
            if (IsExistObjectStateByKey(Key, objectState))
                return m_dicKeyStatesValues[Key][objectState];
            else
                return m_InitValue;

        }

        protected void RemoveValueByKeyAndObjectState(TKey Key, byte objectState)
        {
            if (IsExistObjectStateByKey(Key, objectState))
            {
                m_dicKeyStatesValues[Key].Remove(objectState);
            }
        }

        protected uint GetSelPropertyIdByKey(TKey Key, byte objectState)
        {
            return m_dicSelPropIds[Key][objectState];
        }

        private void SetSelValByKeyAndObjectState(TKey Key, byte objectState, TProperty value)
        {
            m_dicKeyStatesValues[Key][objectState] = value;
        }

        public override bool IsExistIdInDic(object dicKey, uint propertyId, DNSByteBool state)
        {
            
            if (!state.AsBool && m_dicKeyRegPropertyIdValues.Any(x => (x.Key.ToString() != dicKey.ToString()) && (x.Value == propertyId)))
                return true;
            if (state.AsBool && m_dicKeyRegPropertyIdValues.Any(x => (x.Value == propertyId)))
                return true;
            else
            {
                foreach (TKey key in m_dicSelPropIds.Keys)
                {
                    if (!state.AsBool && m_dicSelPropIds[key].ContainsValue(propertyId))
                        return true;
                    if (state.AsBool && m_dicSelPropIds[key].ContainsValue(propertyId))
                    {
                        if (key.ToString() == dicKey.ToString())
                        {
                            // select the states contains the specific id
                            List<byte> listStates = m_dicSelPropIds[key].Where(x => x.Value == propertyId).Select(x => x.Key).ToList();
                            if (listStates != null && listStates.Any(x => x != state.AsByte))
                                return true;
                        }
                        else
                            return true;
                    }
                }
            }
            return false;
        }

        protected bool IsExistObjectStateByKey(TKey Key, byte objectState)
        {
            if (IsExistKey(Key))
                return m_dicKeyStatesValues[Key] != null && m_dicKeyStatesValues[Key].ContainsKey(objectState);
            return false;
        }

        protected void SaveCurrentKey(TKey Key)
        {
            SetCurrentSelPropertyId(ObjectState.AsByte, SelPropertyID);
            m_dicSelPropIds[Key] = m_selPropIds;
        }

        protected void LoadStatesByKey(TKey Key)
        {
            // load states and propId dics
            bool isContainKey = false;
            if (m_dicKeyStatesValues.Count >= 0 && m_dicKeyStatesValues.ContainsKey(Key))
            {
                isContainKey = true;
                LoadStatesBySet(m_dicKeyStatesValues[Key]);
            }
            if (m_dicSelPropIds.Count >= 0 && m_dicSelPropIds.ContainsKey(Key))
                LoadPropIdBySet(m_dicSelPropIds[Key]);

            if (isContainKey && m_dicKeyStatesValues[Key].Keys != null)
                LoadItemsToListStates(m_dicKeyStatesValues[Key].Keys.ToList());

            // load first value if exist
            if (ObjectStates.Length > 0)
            {
                CurrState = ObjectStates.First();
                SelectFirstObjectState();
            }
            else
            {
                CurrState = false;
                ChangeTabText();
            }
        }

        public new void Load(delegateLoadDic func)
        {
            RegPropertyID = DNMcConstants._MC_EMPTY_ID;
            SetSelPropertyID(DNMcConstants._MC_EMPTY_ID);
            RemoveAllDicStates();

            LoadRegular(func);
            LoadStates(func);
        }

        public void LoadRegular(delegateLoadDicWithoutState func)
        {
            RegPropertyID = DNMcConstants._MC_EMPTY_ID;
            RemoveAllDicStates();

            uint m_PropID = 0;
            TProperty m_PropertyParam;

            foreach (TKey key in m_listKeyValues)
            {
                func(key, out m_PropertyParam, out m_PropID);
                AddValueRegularDic(key, m_PropertyParam, m_PropID);
                base.SavePrivateProperties(func.Target, m_PropID, func.Method.Name);
            }
        }

        protected void RemoveAllDicStates()
        {
            List<uint> ids = m_dicKeyRegPropertyIdValues.Values.ToList();
            m_dicKeyRegPropertyIdValues.Clear();
            foreach (uint id in ids)
            {
                MCTPrivatePropertiesData.RemovePropertyId(id, this);
            }
            foreach(TKey key in m_dicSelPropIds.Keys)
            {
                List<uint> selIds = m_dicSelPropIds[key].Values.ToList();
                m_dicSelPropIds[key].Clear();
                foreach (uint id in selIds)
                {
                    MCTPrivatePropertiesData.RemovePropertyId(id, this);
                }
            }
            RemoveAllStates();
        }

        public void LoadRegular(delegateLoadDic func)
        {
            RegPropertyID = DNMcConstants._MC_EMPTY_ID;
            
            uint m_PropID = 0;
            TProperty m_PropertyParam;

            byte objectState = 0;
            DNSByteBool dnObjectState = objectState;

            foreach (TKey enumValue in m_listKeyValues)
            {
                func(enumValue, out m_PropertyParam, out m_PropID, dnObjectState);
                AddValueRegularDic(enumValue, m_PropertyParam, m_PropID);
                base.SavePrivateProperties(func.Target, m_PropID, func.Method.Name);
                MCTPrivatePropertiesData.AddPrivatePropertyControls(m_PropID, m_PropertyParam, this);
            }
        }

        public void LoadStates(delegateLoadDic func)
        {
            SelPropertyID = DNMcConstants._MC_EMPTY_ID;

            byte objectState = 0;
            DNSByteBool dnObjectState = objectState;
            Dictionary<byte, TProperty> dicSelObjectStateValues;
            Dictionary<byte, uint> dicSelPropIds;
            uint propId;
            TProperty m_StatePropertyParam;

            foreach (TKey enumValue in m_listKeyValues)
            {
                dnObjectState = 1;
                propId = (uint)DNEPredefinedPropertyIDs._EPPI_FIRST_RESERVED_ID;
                dicSelObjectStateValues = new Dictionary<byte, TProperty>();
                dicSelPropIds = new Dictionary<byte, uint>();

                while (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                {
                    func(enumValue, out m_StatePropertyParam, out propId, dnObjectState);
                    if (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID &&
                        propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID)
                    {
                        dicSelObjectStateValues.Add(dnObjectState.AsByte, m_StatePropertyParam);
                        dicSelPropIds.Add(dnObjectState.AsByte, propId);
                        base.SavePrivateProperties(func.Target, propId, func.Method.Name);
                        MCTPrivatePropertiesData.AddPrivatePropertyControls(propId, m_StatePropertyParam, this);
                    }

                    if (dnObjectState.AsByte == byte.MaxValue)
                    {
                        break;
                    }

                    dnObjectState.AsByte++;
                }
                m_dicKeyStatesValues[enumValue] = dicSelObjectStateValues;
                m_dicSelPropIds[enumValue] = dicSelPropIds;
            }
        }

        public void Save(delegateSaveDicWithoutState func)
        {
            if (RegVerifiedId())
            {
                foreach (TKey keyKey in m_dicKeyRegValues.Keys)
                {
                    uint propertyId = m_dicKeyRegPropertyIdValues[keyKey];
                    func(keyKey, m_dicKeyRegValues[keyKey], propertyId);
                    base.SavePrivateProperties(func.Target, propertyId, func.Method.Name);
                    MCTPrivatePropertiesData.AddPrivatePropertyControls(propertyId, m_dicKeyRegValues[keyKey], this);
                }
            }
            SelectFirstTab();
        }


        public void Save(delegateSaveDic func)
        {
            if (RegVerifiedId())
            {
                foreach (TKey keyKey in m_dicKeyRegValues.Keys)
                {
                    uint propertyId = m_dicKeyRegPropertyIdValues[keyKey];
                    func(keyKey, m_dicKeyRegValues[keyKey], m_dicKeyRegPropertyIdValues[keyKey], false);
                    base.SavePrivateProperties(func.Target, propertyId, func.Method.Name);
                }
            }

            DeselectTab();

            byte nLastState = 0;
            byte[] objectStates;

            foreach (TKey keyKey in m_dicKeyStatesValues.Keys)
            {
                nLastState = 0;
                objectStates = GetSelStatesArrByKey(keyKey);

                foreach (byte objectState in objectStates)
                {
                    while (++nLastState < objectState)
                    {
                        func(keyKey, m_InitValue, (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID, nLastState);
                    }
                    TProperty value = GetSelValByKeyAndObjectState(keyKey, objectState);
                    uint propertyId = GetSelPropertyIdByKey(keyKey, objectState);
                    func(keyKey, value, propertyId, objectState);
                    base.SavePrivateProperties(func.Target, propertyId, func.Method.Name);
                }
                if (nLastState < byte.MaxValue)
                {
                    nLastState++;
                    func(keyKey, m_InitValue, (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID, nLastState);
                }
            }

            SelectFirstTab();
        }

        public void SetPropertyById(uint propertyId, TProperty propertyValue)
        {
            foreach (TKey key in m_dicSelPropIds.Keys)
            {
                if (m_dicSelPropIds[key].ContainsValue(propertyId))
                {
                    foreach (byte state in m_dicSelPropIds[key].Keys)
                    {
                        if (m_dicSelPropIds[key][state] == propertyId)
                        {
                            m_dicKeyStatesValues[key][state] = propertyValue;
                        }
                    }
                }
            }
            foreach(TKey key in m_dicKeyRegPropertyIdValues.Keys)
            {
                if (m_dicKeyRegPropertyIdValues[key] == propertyId)
                    m_dicKeyRegValues[key] = propertyValue;
            }
        }
    }

}
