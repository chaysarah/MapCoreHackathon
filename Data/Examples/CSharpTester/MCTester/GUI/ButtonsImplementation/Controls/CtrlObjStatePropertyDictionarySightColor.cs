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
    public partial class CtrlObjStatePropertyDictionarySightColor : CtrlObjStatePropertyDictionaryDataHandlerSightColor
    {
        Array arrEnumValues;
        List<DNEPointVisibility> listEnumValues;
        Type enumKey = typeof(DNEPointVisibility);
        
        DNSByteBool currState = false;
        
        DNEPointVisibility selectedKey;

        public CtrlObjStatePropertyDictionarySightColor()
        {
            InitializeComponent();
            SetEnumList();

            ctrlSelSelectColor.picbColor.Click += new EventHandler(picbColorSel_Click);
            ctrlSelSelectColor.nudAlpha.ValueChanged += new EventHandler(nudAlphaSel_ValueChanged);

            ctrlRegSelectColor.picbColor.Click += new EventHandler(picbColorReg_Click);
            ctrlRegSelectColor.nudAlpha.ValueChanged += new EventHandler(nudAlphaReg_ValueChanged);

            m_InitValue = DNSMcBColor.bcBlackOpaque;

            selectedKey = DNEPointVisibility._EPV_SEEN;

        }

        public DNEPointVisibility EnumKey
        {
            get
            {
                if (cbRegEnumColor.SelectedItem != null)
                    return (DNEPointVisibility)Enum.Parse(enumKey, cbRegEnumColor.SelectedItem.ToString());
                else
                    return 0;
            }
            set
            {
                if (enumKey != null && cbRegEnumColor.Items.Count > 0)
                    cbRegEnumColor.Text = Enum.GetName(enumKey, value);
            }
        }

        public void SetEnumList()
        {
            cbRegEnumColor.Items.Clear();

            Type enumKey = typeof(DNEPointVisibility);

            cbRegEnumColor.Items.AddRange(Enum.GetNames(enumKey));
            arrEnumValues = Enum.GetValues(enumKey);
            listEnumValues = new List<DNEPointVisibility>();
            foreach (DNEPointVisibility value in arrEnumValues)
                if (value != DNEPointVisibility._EPV_NUM)
                    listEnumValues.Add(value);
           
            LoadKeyList(listEnumValues);

            ctrlRegSelectColor.EnabledButtons(true);
            ctrlSelSelectColor.EnabledButtons(false);
        }

        private void nudAlphaReg_ValueChanged(object sender, EventArgs e)
        {
            SaveCurrentColor(EnumKey, ctrlRegSelectColor.BColor, false);
        }

        private void picbColorReg_Click(object sender, EventArgs e)
        {
            SaveCurrentColor(EnumKey, ctrlRegSelectColor.BColor, false);
        }

        private void SaveCurrentKey()
        {
            SaveCurrentColor(selectedKey, ctrlRegSelectColor.BColor, false);
            
            SaveCurrentKey(selectedKey);
        }

        private void SaveCurrentColor(DNEPointVisibility currKey, DNSMcBColor currColor, DNSByteBool state)
        {
            if (currKey == DNEPointVisibility._EPV_NUM)
                return;
            SaveCurrentValue(currKey, currColor, state);
        }

        void nudAlphaSel_ValueChanged(object sender, EventArgs e)
        {
            SaveCurrentColor(EnumKey, ctrlSelSelectColor.BColor, CurrState);
        }

        void picbColorSel_Click(object sender, EventArgs e)
        {
            SaveCurrentColor(EnumKey, ctrlSelSelectColor.BColor, CurrState);
        }

        private void cbSelEnumColor_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbRegEnumColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // regular tab
            MCTPrivatePropertiesData.bIsChangeDicKey = true;

            SaveCurrentKey();
            RemoveAllStates();
            selectedKey = (DNEPointVisibility)arrEnumValues.GetValue(cbRegEnumColor.SelectedIndex);
            LoadRegularValue();
            LoadStatesByKey(selectedKey);
            SetStateControlsEnabled(ObjectStates.Length > 0);

            MCTPrivatePropertiesData.bIsChangeDicKey = false;
        }

        private void LoadRegularValue()
        {
            LoadRegularPropertyID(selectedKey);
            ctrlRegSelectColor.BColor = GetRegularCurrentValue(selectedKey);
        }

        public new void Load(delegateLoadDic func)
        {
            base.Load(func);
            LoadRegularValue();

            cbRegEnumColor.SelectedIndex = 0;

            LoadStatesByKey(DNEPointVisibility._EPV_SEEN);
            SelectFirstTab();
            if (ObjectStates.Length > 0)
            {
                SetStateControlsEnabled(true);
            }
        }

        private new void LoadStatesByKey(DNEPointVisibility selectedSelKey)
        {
            base.LoadStatesByKey(selectedSelKey);

            // load first value if exist
            if (ObjectStates.Length > 0)
            {
                ctrlSelSelectColor.BColor = base.GetSelValByKeyAndObjectState(selectedSelKey,CurrState.AsByte);
            }
            else
            {
                ctrlSelSelectColor.BColor = m_InitValue;
            }
        }   

        public new void Save(delegateSaveDic func)
        {
            //SaveCurrentColor(selectedRegKey, ctrlRegSelectColor.BColor, false);
            //SaveCurrentKey(selectedRegKey);
            SaveCurrentKey();
            base.Save(func);
        }

        
        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(EnumKey, objectState, bInitCtrl);
            EnabledStateButtons(true);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(EnumKey, previousState, ctrlSelSelectColor.BColor, objectState);

            if (IsExistObjectStateByKey(EnumKey, objectState))
            {
                ctrlSelSelectColor.BColor = GetSelValByKeyAndObjectState(EnumKey, objectState);
            }

            bool isEnabled = false;
            if (ObjectStates != null && ObjectStates.Length > 0)
                isEnabled = true;

            EnabledStateButtons(isEnabled);
        }

        public override void ObjectStateRemove(byte objectState)
        {
            RemoveValueByKeyAndObjectState(EnumKey, objectState);
        }

        private void EnabledStateButtons(bool isEnabled)
        {
            ctrlSelSelectColor.EnabledButtons(isEnabled);
        }

        public override bool PropertyIdChanged()
        {
            SaveCurrentKey();
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, ctrlRegSelectColor.BColor, this, new DNSByteBool(0), EnumKey);
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            SaveCurrentKey();
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, ctrlSelSelectColor.BColor, this, ObjectState, EnumKey);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_BCOLOR; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                ctrlRegSelectColor.BColor = (DNSMcBColor)value;
            if (SelPropertyID == propertyId)
                ctrlSelSelectColor.BColor = (DNSMcBColor)value;
            SetPropertyById(propertyId, (DNSMcBColor)value);
        }

        private void ctrlRegSelectColor_Validating(object sender, CancelEventArgs e)
        {
            SaveCurrentKey();
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, ctrlRegSelectColor.BColor, this, new DNSByteBool(0), EnumKey);
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

        private void ctrlSelSelectColor_Validating(object sender, CancelEventArgs e)
        {
            SaveCurrentKey();
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, ctrlSelSelectColor.BColor, this, ObjectState, EnumKey);
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

    public class CtrlObjStatePropertyDictionaryDataHandlerSightColor : CtrlObjStatePropertyDictionaryDataHandler<DNEPointVisibility, DNSMcBColor> { }
}
