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
    public partial class CtrlObjStatePropertyComboTextBox : CtrlObjStatePropertyDataHandlerComboTextBox
    {
        private Type m_RegEnumType = null;
        private Type m_SelEnumType = null;
        private string m_EnumType = null;
        private Type m_tEnumType;
        private Array m_arrEnumValue;
        bool IsInSetValue = false;
        bool IsInRegValidation = true;
        bool IsInSelValidation = true;

        public Array ArrEnumValue
        {
            get
            {
                if (m_arrEnumValue == null && TEnumType != null)
                    m_arrEnumValue = Enum.GetValues(TEnumType);
                return m_arrEnumValue;
            }
            set { m_arrEnumValue = value; }
        }

        public uint RegUintVal
        {
            get { return ntxRegValue.GetUInt32(); }
            set { ntxRegValue.SetUInt32(value); }
        }

        public uint SelUintVal
        {
            get { return ntxSelValue.GetUInt32(); }
            set { ntxSelValue.SetUInt32(value); }
        }

        public Type TEnumType
        {
            get
            {
                return m_tEnumType;
            }
            set
            {
                m_tEnumType = value;
            }
        }

        public string EnumType
        {
            get
            {
                if (m_EnumType == null)
                    m_EnumType = "";
                return m_EnumType;
            }
            set
            {
                m_EnumType = value;
            }
        }

        public CtrlObjStatePropertyComboTextBox()
        {
            InitializeComponent();
            m_InitValue = 0;
        }

        bool bSetEnumList = false;
        public void SetEnumList(Type enumType)
        {
            bSetEnumList = true;
            cmbRegEnum.Items.Clear();
            cmbSelEnum.Items.Clear();

            m_RegEnumType = enumType;
            m_SelEnumType = enumType;

            TEnumType = enumType;
            if (enumType != null)
            {
                ArrEnumValue = Enum.GetValues(enumType);
            }

            cmbRegEnum.Items.AddRange(Enum.GetNames(enumType));
            cmbSelEnum.Items.AddRange(Enum.GetNames(enumType));

            if (cmbRegEnum.Items.Count > 0)
                cmbRegEnum.Text = cmbRegEnum.Items[0].ToString();

            if (cmbSelEnum.Items.Count > 0)
                cmbSelEnum.Text = cmbSelEnum.Items[0].ToString();
            bSetEnumList = false;
        }

        private void SelectValueInCombo(NumericTextBox numTextBox, ComboBox comboBox)
        {
            uint value = numTextBox.GetUInt32();
            if (numTextBox.Text == "" && comboBox.SelectedIndex == -1)
                return;
            else if (comboBox.SelectedIndex > -1)
            {
                if (value == (uint)ArrEnumValue.GetValue(comboBox.SelectedIndex))
                    return;
                else
                {
                    int index = 0;
                    foreach (object arrValue in ArrEnumValue)
                    {
                        if (value == (uint)arrValue)
                        {
                            comboBox.SelectedIndex = index;
                            return;
                        }
                        index++;
                    }
                }
            }
        }

        private void SetValueInTextBox(NumericTextBox numTextBox, ComboBox comboBox)
        {
            if (comboBox.SelectedIndex > -1)
            {
                uint value = (uint)ArrEnumValue.GetValue(comboBox.SelectedIndex);
                numTextBox.SetUInt32(value);
            }
            else
            {
                numTextBox.Text = "";
            }
        }

        private void cmbRegEnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetValueInTextBox(ntxRegValue, cmbRegEnum);
            if (!bSetEnumList)
            {
                IsInRegValidation =false ;
                FocusRegPropertyID();
                IsInRegValidation = true;
            }
        }

        private void ntxRegValue_TextChanged(object sender, EventArgs e)
        {
            SelectValueInCombo(ntxRegValue, cmbRegEnum);
        }

        private void cmbSelEnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetValueInTextBox(ntxSelValue, cmbSelEnum);
            if (!bSetEnumList)
            {
                IsInSelValidation =false ;
                FocusSelPropertyID();
                IsInSelValidation = true;
            }
        }

        private void ntxSelValue_TextChanged(object sender, EventArgs e)
        {
            SelectValueInCombo(ntxSelValue, cmbSelEnum);
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelUintVal = m_InitValue;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelUintVal);

            if (IsExistObjectState(objectState))
            {
                SelUintVal = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegUintVal);
        }

        public new void Load(delegateLoad func)
        {
            SelUintVal = m_InitValue;
            RegUintVal = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null && !IsInSetValue && IsInSelValidation) 
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegUintVal, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null && !IsInSetValue && IsInRegValidation)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelUintVal, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }


        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_UINT; }

        public override void SetValue(uint propertyId, object value)
        {
            IsInSetValue = true;
            if (RegPropertyID == propertyId)
                RegUintVal = (uint)value;
            if (SelPropertyID == propertyId)
                SelUintVal = (uint)value;
            SetStateValueByProperty(propertyId, (uint)value);
            IsInSetValue = false;
        }

        private void ntxRegValue_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation() && !IsInSetValue && IsInSelValidation)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegUintVal, this, new DNSByteBool(0));
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

        private void ntxSelValue_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation() && !IsInSetValue && IsInRegValidation)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelUintVal, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerComboTextBox : CtrlObjStatePropertyDataHandler<uint> { }
}
