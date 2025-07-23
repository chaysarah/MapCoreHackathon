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
    public partial class CtrlObjStatePropertyETargetTypesFlags : CtrlObjStatePropertyDataHandlerETargetTypesFlags
    {
        private Type m_RegEnumType = null;
        private Type m_SelEnumType = null;
        private string m_EnumType = null;
        //bool m_blockEvent = false;

        public CtrlObjStatePropertyETargetTypesFlags()
        {
            InitializeComponent();
            m_InitValue = DNETargetTypesFlags._ETTF_DTM;
            SetEnumList();
        }

        private void SetEnumList()
        {
            Type enumType = typeof(DNETargetTypesFlags);

            lstRegEnum.Items.Clear();
            lstSelEnum.Items.Clear();
            m_SelEnumType = enumType;
            m_RegEnumType = enumType;

            lstRegEnum.Items.AddRange(Enum.GetNames(enumType));
            lstSelEnum.Items.AddRange(Enum.GetNames(enumType));
        }

        public DNETargetTypesFlags RegEnumVal
        {
            get
            {
                DNETargetTypesFlags currValue = 0;
                for (int i = 0; i < lstRegEnum.Items.Count; i++)
                {
                    if (lstRegEnum.GetItemChecked(i))
                    {
                        currValue += (int)Enum.Parse(m_RegEnumType, lstRegEnum.Items[i].ToString());
                    }
                }

                return currValue;
            }
            set
            {
                if (m_RegEnumType != null)
                {
                    int checkedValue = (int)value;
                    //m_blockEvent = true;
                    for (int i = 0; i < lstRegEnum.Items.Count; i++)
                    {
                        int currEnum = (int)Enum.Parse(m_RegEnumType, lstRegEnum.Items[i].ToString());
                        if (currEnum == 0 && checkedValue == 0)
                        {
                            lstRegEnum.SetItemChecked(i, true);
                        }
                        else
                        {
                            lstRegEnum.SetItemChecked(i, (currEnum & checkedValue) != 0);
                        }
                    }
                    //m_blockEvent = false;
                }
            }
        }

        public DNETargetTypesFlags SelEnumVal
        {
            get
            {
                DNETargetTypesFlags currValue = 0;
                for (int i = 0; i < lstSelEnum.Items.Count; i++)
                {
                    if (lstSelEnum.GetItemChecked(i))
                    {
                        currValue += (int)Enum.Parse(m_SelEnumType, lstSelEnum.Items[i].ToString());
                    }
                }

                return currValue;
            }
            set
            {
                if (m_SelEnumType != null)
                {
                    int checkedValue = (int)value;
                    //m_blockEvent = true;
                    for (int i = 0; i < lstSelEnum.Items.Count; i++)
                    {
                        int currEnum = (int)Enum.Parse(m_SelEnumType, lstSelEnum.Items[i].ToString());
                        if (currEnum == 0 && checkedValue == 0)
                        {
                            lstSelEnum.SetItemChecked(i, true);
                        }
                        else
                        {
                            lstSelEnum.SetItemChecked(i, (currEnum & checkedValue) != 0);
                        }
                    }
                    //m_blockEvent = false;
                }
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

        public override string GetPropertyEnumType() { return "DNETargetTypesFlags"; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegEnumVal = (DNETargetTypesFlags)value;
            if (SelPropertyID == propertyId)
                SelEnumVal = (DNETargetTypesFlags)value;
            SetStateValueByProperty(propertyId, (DNETargetTypesFlags)value);
        }

        private void lstRegEnum_Validating(object sender, CancelEventArgs e)
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

        private void lstSelEnum_Validating(object sender, CancelEventArgs e)
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

    public class CtrlObjStatePropertyDataHandlerETargetTypesFlags : CtrlObjStatePropertyDataHandler<DNETargetTypesFlags>
    {
        
    }
}
