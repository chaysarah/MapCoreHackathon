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

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyDicConditionalSelector : CtrlObjStatePropertyDicDataHandlerConditionalSelector
    {
        //MCTConditionalSelectorProperty hh;
        Array arrEnumValues;
        List<DNEActionType> listEnumValues;
        Type enumKey = typeof(DNEActionType);

        DNSByteBool currState = false;
        DNEActionType selectedKey = DNEActionType._EAT_VISIBILITY;
        private IDNMcConditionalSelector[] mSelectorsArr;
        private IDNMcObjectSchemeNode m_currObject;
        private bool m_IsInObjectLocation = false;

        public CtrlObjStatePropertyDicConditionalSelector()
        {
            InitializeComponent();
        }

        public void InitControl(bool bInObjectLocation)
        {
            IsInObjectLocation = bInObjectLocation;
            SetEnumList();
            m_InitValue = new MCTConditionalSelectorProperty(null, false);
            selectedKey = DNEActionType._EAT_VISIBILITY;
        }

        public bool IsInObjectLocation
        {
            get { return m_IsInObjectLocation; }
            set
            {
                m_IsInObjectLocation = value;

            }
        }
        public IDNMcConditionalSelector[] SelectorsArr
        {
            get
            {
                return mSelectorsArr;
            }
            set
            {
                mSelectorsArr = value;
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        string name = Manager_MCNames.GetNameByObject(value[i], value[i].ConditionalSelectorType.ToString());

                        clstRegConditionalSelector.Items.Add(name);
                        clstSelConditionalSelector.Items.Add(name);
                    }
                }
            }
        }

        public DNEActionType ActionTypeKey
        {
            get
            {
                if (cbActionType.SelectedItem != null)
                    return (DNEActionType)Enum.Parse(enumKey, cbActionType.SelectedItem.ToString());
                else
                    return 0;
            }
            set
            {
                if (enumKey != null && cbActionType.Items.Count > 0)
                    cbActionType.Text = Enum.GetName(enumKey, value);
            }
        }

        public IDNMcObjectSchemeNode CurrObject
        {
            get { return m_currObject; }
            set
            {
                m_currObject = value;
                if (value != null)
                {
                    MCTConditionalSelectorProperty.delegateLoad = value.GetConditionalSelector;
                    MCTConditionalSelectorProperty.delegateSave = value.SetConditionalSelector;
                }
            }
        }

        private MCTConditionalSelectorProperty CurrRegularValue
        {
            get
            {
                // regular tab
                IDNMcConditionalSelector selectedSelector;
                if (clstRegConditionalSelector.CheckedItems.Count == 0)
                    selectedSelector = null;
                else
                    selectedSelector = SelectorsArr[clstRegConditionalSelector.CheckedIndices[0]];

                MCTConditionalSelectorProperty currValue = new MCTConditionalSelectorProperty(selectedSelector, chxActionOnResult.Checked);
                return currValue;
            }
        }

        private MCTConditionalSelectorProperty CurrStateValue
        {
            get
            {
                IDNMcConditionalSelector selectedSelector;
                if (clstSelConditionalSelector.CheckedItems.Count == 0)
                    selectedSelector = null;
                else
                    selectedSelector = SelectorsArr[clstSelConditionalSelector.CheckedIndices[0]];

                MCTConditionalSelectorProperty currValue = new MCTConditionalSelectorProperty(selectedSelector, chxActionOnResult.Checked);
                return currValue;
            }
        }

        public void SetEnumList()
        {
            cbActionType.Items.Clear();

            Type enumKey = typeof(DNEActionType);
            List<string> listEnumNames = new List<string>();

            arrEnumValues = Enum.GetValues(enumKey);
            listEnumValues = new List<DNEActionType>();
            foreach (DNEActionType value in arrEnumValues)
            {
                if ((value != DNEActionType._EAT_NUM && value != DNEActionType._EAT_ACTIVITY))
                {
                    if (m_IsInObjectLocation == false || m_IsInObjectLocation && value != DNEActionType._EAT_TRANSFORM)
                    {
                        listEnumValues.Add(value);
                        listEnumNames.Add(value.ToString());
                    }
                }
            }
            cbActionType.Items.AddRange(listEnumNames.ToArray());
            if (listEnumNames.Count == 1)
                cbActionType.Enabled = false;
            cbActionType.SelectedIndex = 0;
            LoadKeyList(listEnumValues);

            clstRegConditionalSelector.Enabled = true;
            clstSelConditionalSelector.Enabled = true;
        }

        private void SaveCurrentValue(DNEActionType currKey, IDNMcConditionalSelector selectedSelector, bool bActionOnResult, DNSByteBool state)
        {
            if (currKey == DNEActionType._EAT_NUM)
                return;

            MCTConditionalSelectorProperty currValue = new MCTConditionalSelectorProperty((IDNMcConditionalSelector)selectedSelector, bActionOnResult);
            base.SaveCurrentValue(currKey, currValue, state);
        }

        public new void Load(IDNMcObjectSchemeNode objScheme)
        {
            CurrObject = objScheme;

            if (CurrObject.GetScheme() != null)
                SelectorsArr = CurrObject.GetScheme().GetOverlayManager().GetConditionalSelectors();
            else
                SelectorsArr = new IDNMcConditionalSelector[0];

            // cbActionType.SelectedIndex = 1;
            base.Load(MCTConditionalSelectorProperty.Load);
            CurrentObjectSchemeNode = (IDNMcObjectSchemeNode)MCTConditionalSelectorProperty.delegateLoad.Target;

            LoadRegularValue(base.GetRegularCurrentValue(selectedKey));
            LoadRegularPropertyID(selectedKey);

            LoadStatesByKey(selectedKey);
            SelectFirstTab();
            if (ObjectStates.Length > 0)
            {
                SetStateControlsEnabled(true);
            }
        }

        private void LoadRegularValue(MCTConditionalSelectorProperty currValue)
        {
            if (currValue != null)
            {
                clstRegConditionalSelector.ClearSelected();
                IDNMcConditionalSelector condSelector = currValue.ConditionalSelector;

                for (int i = 0; i < clstRegConditionalSelector.Items.Count; i++)
                {
                    if (condSelector == SelectorsArr[i])
                    {
                        
                        clstRegConditionalSelector.SetItemChecked(i, true);
                    }
                    else
                        clstRegConditionalSelector.SetItemChecked(i, false);

                }

                chxActionOnResult.Checked = currValue.ActionOnResult;
            }
        }

        private void LoadStateValue(MCTConditionalSelectorProperty currValue)
        {
            if (currValue != null)
            {
                IDNMcConditionalSelector condSelector = currValue.ConditionalSelector;

                for (int i = 0; i < clstSelConditionalSelector.Items.Count; i++)
                {
                    if (condSelector == SelectorsArr[i])
                        clstSelConditionalSelector.SetItemChecked(i, true);
                    else
                        clstSelConditionalSelector.SetItemChecked(i, false);
                }
            }
        }

        private new void LoadStatesByKey(DNEActionType selectedSelKey)
        {
            base.LoadStatesByKey(selectedSelKey);

            // load first value if exist
            if (ObjectStates.Length > 0)
            {
                LoadStateValue(base.GetSelValByKeyAndObjectState(selectedSelKey, CurrState.AsByte));
            }
            else
            {
                LoadStateValue(m_InitValue);
            }
        }

        public void Save()
        {
            SaveCurrentKey();

            base.Save(MCTConditionalSelectorProperty.Save);
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(ActionTypeKey, objectState, bInitCtrl);
            EnabledStateButtons(true);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(ActionTypeKey, previousState, CurrStateValue, objectState);

            if (IsExistObjectStateByKey(ActionTypeKey, objectState))
            {
                LoadStateValue(GetSelValByKeyAndObjectState(ActionTypeKey, objectState));
            }

            bool isEnabled = false;
            if (ObjectStates != null && ObjectStates.Length > 0)
                isEnabled = true;

            EnabledStateButtons(isEnabled);
        }

        public override void ObjectStateRemove(byte objectState)
        {
            RemoveValueByKeyAndObjectState(ActionTypeKey, objectState);
        }

        private void EnabledStateButtons(bool isEnabled)
        {
            clstSelConditionalSelector.Enabled = isEnabled;
            chxActionOnResult.Enabled = isEnabled;
        }

        private void clstSelConditionalSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < clstSelConditionalSelector.Items.Count; i++)
                clstSelConditionalSelector.SetItemChecked(i, false);

            int selectedIndex = clstSelConditionalSelector.SelectedIndex;

            if (selectedIndex >= 0)
                clstSelConditionalSelector.SetItemChecked(selectedIndex, true);
        }

        private void clstRegConditionalSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < clstRegConditionalSelector.Items.Count; i++)
                clstRegConditionalSelector.SetItemChecked(i, false);

            int selectedIndex = clstRegConditionalSelector.SelectedIndex;

            if (selectedIndex >= 0)
                clstRegConditionalSelector.SetItemChecked(selectedIndex, true);
        }

        private void cbActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveCurrentKey();
            LoadNewKey();
        }

        private void SaveCurrentKey()
        {
            if (selectedKey != DNEActionType._EAT_NUM && selectedKey != DNEActionType._EAT_ACTIVITY)
            {
                SaveCurrentValue(selectedKey, CurrRegularValue, false);
                SaveCurrentState();
            }
        }

        private void SaveCurrentState()
        {
            if (ObjectState.AsBool == true)
                SaveCurrentValue(selectedKey, CurrStateValue, ObjectState);
            base.SaveCurrentKey(selectedKey);
        }

        private void LoadNewKey()
        {
            selectedKey = (DNEActionType)Enum.Parse(typeof(DNEActionType), cbActionType.SelectedItem.ToString());
            RemoveAllStates();
            if (selectedKey != DNEActionType._EAT_NUM && selectedKey != DNEActionType._EAT_ACTIVITY)
            {
                LoadRegularValue(GetRegularCurrentValue(selectedKey));
                LoadRegularPropertyID(selectedKey);
                LoadStatesByKey(selectedKey);
                SetStateControlsEnabled(ObjectStates.Length > 0);
            }
        }

        public override void AddStateVisibleChanged(bool isVisible)
        {
            clstSelConditionalSelector.Visible = !isVisible;
        }

        public override bool PropertyIdChanged()
        {
            SaveCurrentKey();
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, CurrRegularValue, this, new DNSByteBool(0), selectedKey);
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return true;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            SaveCurrentKey();
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, CurrStateValue, this, ObjectState, selectedKey);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return true;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_CONDITIONALSELECTOR; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                LoadRegularValue(GetCSPropertyFromPPData(value));
            if (SelPropertyID == propertyId)
                LoadStateValue(GetCSPropertyFromPPData(value));
            SetStateValueByProperty(propertyId, GetCSPropertyFromPPData(value));
        }

        private MCTConditionalSelectorProperty GetCSPropertyFromPPData(object propertyValue)
        {
            IDNMcConditionalSelector mcConditionalSelector = null;
            MCTConditionalSelectorProperty mctConditionalSelectorProperty = null;
            if (propertyValue is MCTConditionalSelectorProperty)
                mctConditionalSelectorProperty = (MCTConditionalSelectorProperty)propertyValue;
            else if (propertyValue is IDNMcConditionalSelector)
            {
                mcConditionalSelector = (IDNMcConditionalSelector)propertyValue;
                mctConditionalSelectorProperty = new MCTConditionalSelectorProperty(mcConditionalSelector, chxActionOnResult.Checked);
            }
            return mctConditionalSelectorProperty;
        }

        private void clstRegConditionalSelector_Validating(object sender, CancelEventArgs e)
        {
            SaveCurrentKey();
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, CurrRegularValue, this, new DNSByteBool(0), selectedKey);
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

        private void clstSelConditionalSelector_Validating(object sender, CancelEventArgs e)
        {
            SaveCurrentKey();
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, CurrStateValue, this, ObjectState, selectedKey);
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

    public class CtrlObjStatePropertyDicDataHandlerConditionalSelector : CtrlObjStatePropertyDictionaryDataHandler<DNEActionType, MCTConditionalSelectorProperty> { }
}
