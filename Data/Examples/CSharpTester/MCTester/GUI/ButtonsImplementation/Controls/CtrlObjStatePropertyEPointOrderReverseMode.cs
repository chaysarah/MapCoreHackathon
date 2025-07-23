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
    public partial class CtrlObjStatePropertyEPointOrderReverseMode : CtrlObjStatePropertyDataHandlerEPointOrderReverseMode
    {
        private Type m_RegEnumType = null;
        private Type m_SelEnumType = null;
        private string m_EnumType = null;

        public CtrlObjStatePropertyEPointOrderReverseMode()
        {
            InitializeComponent();
            m_InitValue = DNEPointOrderReverseMode._EPORM_NONE;
            SetEnumList();
        }

        private void SetEnumList()
        {
            cmbRegEnum.Items.Clear();
            cmbSelEnum.Items.Clear();

            Type enumType = typeof(DNEPointOrderReverseMode);

            m_RegEnumType = enumType;
            m_SelEnumType = enumType;

            cmbRegEnum.Items.AddRange(Enum.GetNames(enumType));
            cmbSelEnum.Items.AddRange(Enum.GetNames(enumType));


            if (cmbRegEnum.Items.Count > 0)
                cmbRegEnum.Text = cmbRegEnum.Items[0].ToString();

            if (cmbSelEnum.Items.Count > 0)
                cmbSelEnum.Text = cmbSelEnum.Items[0].ToString();
        }

        public DNEPointOrderReverseMode RegEnumVal
        {
            get
            {
                if (cmbRegEnum.SelectedItem != null)
                    return (DNEPointOrderReverseMode)Enum.Parse(m_RegEnumType, cmbRegEnum.SelectedItem.ToString());
                else
                    return 0;
            }
            set
            {
                if (m_RegEnumType != null && cmbRegEnum.Items.Count > 0)
                    cmbRegEnum.Text = Enum.GetName(m_RegEnumType, value);
            }
        }

        public DNEPointOrderReverseMode SelEnumVal
        {
            get
            {
                if (cmbSelEnum.SelectedItem != null)
                    return (DNEPointOrderReverseMode)Enum.Parse(m_SelEnumType, cmbSelEnum.SelectedItem.ToString());
                else
                    return 0;
            }
            set
            {
                if (m_SelEnumType != null && cmbSelEnum.Items.Count > 0)
                    cmbSelEnum.Text = Enum.GetName(m_SelEnumType, value);
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

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelEnumVal = m_InitValue;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelEnumVal);

            if (IsExistObjectState(objectState))
            {
                SelEnumVal = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegEnumVal);
        }

        public new void Load(delegateLoad func)
        {
            SelEnumVal = m_InitValue;
            RegEnumVal = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegEnumVal, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelEnumVal, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_ENUM; }

        public override string GetPropertyEnumType() { return "DNEPointOrderReverseMode"; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegEnumVal = (DNEPointOrderReverseMode)value;
            if (SelPropertyID == propertyId)
                SelEnumVal = (DNEPointOrderReverseMode)value;
            SetStateValueByProperty(propertyId, (DNEPointOrderReverseMode)value);
        }

        private void cmbRegEnum_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegEnumVal, this, new DNSByteBool(0));
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

        private void cmbSelEnum_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelEnumVal, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerEPointOrderReverseMode : CtrlObjStatePropertyDataHandler<DNEPointOrderReverseMode>
    {
        
    }
}
