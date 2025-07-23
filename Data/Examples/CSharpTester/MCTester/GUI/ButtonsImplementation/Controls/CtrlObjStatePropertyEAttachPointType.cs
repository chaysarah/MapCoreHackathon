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
    public partial class CtrlObjStatePropertyEAttachPointType : CtrlObjStatePropertyDataHandlerEAttachPointType
    {
        private Type m_RegEnumType = null;
        private string m_EnumType = null;

        public CtrlObjStatePropertyEAttachPointType()
        {
            InitializeComponent();
            m_InitValue = DNEAttachPointType._EAPT_NONE;
            SetEnumList();
        }

        private void SetEnumList()
        {
            cmbRegEnum.Items.Clear();

            Type enumType = typeof(DNEAttachPointType);
           
            m_RegEnumType = enumType;

            cmbRegEnum.Items.AddRange(Enum.GetNames(enumType));

            if (cmbRegEnum.Items.Count > 0)
                cmbRegEnum.Text = cmbRegEnum.Items[0].ToString();

        }

        public DNEAttachPointType RegEnumVal
        {
            get
            {
                if (cmbRegEnum.SelectedItem != null)
                    return (DNEAttachPointType)Enum.Parse(m_RegEnumType, cmbRegEnum.SelectedItem.ToString());
                else
                    return 0;
            }
            set
            {
                if (m_RegEnumType != null && cmbRegEnum.Items.Count > 0)
                    cmbRegEnum.Text = Enum.GetName(m_RegEnumType, value);
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

        public void Save(delegateSaveWithParentParamWithoutState func, uint numAttachPointParentIndex)
        {
            base.Save(func, numAttachPointParentIndex, RegEnumVal);
        }

        public new void Load(delegateLoadWithParentParamWithoutState func, uint numAttachPointParentIndex)
        {
            RegEnumVal = base.Load(func, numAttachPointParentIndex);
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

        

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_ENUM; }

        public override string GetPropertyEnumType() { return "DNEAttachPointType"; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegEnumVal = (DNEAttachPointType)value;
           
            SetStateValueByProperty(propertyId, (DNEAttachPointType)value);
        }

        private void cmbRegEnum_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                //CheckUserClickSave();
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

       
    }

    public class CtrlObjStatePropertyDataHandlerEAttachPointType : CtrlObjStatePropertyDataHandler<DNEAttachPointType>
    {
        
    }
}
