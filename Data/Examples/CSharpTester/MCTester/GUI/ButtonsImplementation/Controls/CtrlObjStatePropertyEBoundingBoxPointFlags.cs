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
    public partial class CtrlObjStatePropertyEBoundingBoxPointFlags : CtrlObjStatePropertyDataHandlerEBoundingBoxPointFlags
    {
        private Type m_RegEnumType = null;
        private Type m_SelEnumType = null;
        private string m_EnumType = null;

        public CtrlObjStatePropertyEBoundingBoxPointFlags()
        {
            InitializeComponent();
            m_InitValue = DNEBoundingBoxPointFlags._EBBPF_CENTER;
            SetEnumList();
        }

        private void SetEnumList()
        {
            Type enumType = typeof(DNEBoundingBoxPointFlags);

            lstRegEnum.Items.Clear();
            lstSelEnum.Items.Clear();
            m_SelEnumType = enumType;
            m_RegEnumType = enumType;

            lstRegEnum.Items.AddRange(Enum.GetNames(enumType));
            lstSelEnum.Items.AddRange(Enum.GetNames(enumType));
        }

        public DNEBoundingBoxPointFlags RegEnumVal
        {
            get
            {
                DNEBoundingBoxPointFlags currValue = 0;
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
                }
            }
        }

        public DNEBoundingBoxPointFlags SelEnumVal
        {
            get
            {
                DNEBoundingBoxPointFlags currValue = 0;
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

        public void Save(delegateSaveWithParentParam func, uint numTypeParentIndex)
        {
            base.Save(func, numTypeParentIndex, RegEnumVal);
        }

        public new void Load(delegateLoad func)
        {
            SelEnumVal = m_InitValue;
            RegEnumVal = base.Load(func);
        }

        public new void Load(delegateLoadWithParentParam func, uint numAttachPointParentIndex)
        {
            SelEnumVal = m_InitValue;
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

        public override string GetPropertyEnumType() { return "DNEBoundingBoxPointFlags"; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegEnumVal = (DNEBoundingBoxPointFlags)value;
            if (SelPropertyID == propertyId)
                SelEnumVal = (DNEBoundingBoxPointFlags)value;
            SetStateValueByProperty(propertyId, (DNEBoundingBoxPointFlags)value);
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

    public class CtrlObjStatePropertyDataHandlerEBoundingBoxPointFlags : CtrlObjStatePropertyDataHandler<DNEBoundingBoxPointFlags>
    {
        
    }
}
