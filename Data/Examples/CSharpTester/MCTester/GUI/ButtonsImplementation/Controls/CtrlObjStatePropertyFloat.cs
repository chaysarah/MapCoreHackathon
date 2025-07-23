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
    public partial class CtrlObjStatePropertyFloat : CtrlObjStatePropertyDataHandlerFloat
    {
        public CtrlObjStatePropertyFloat()
        {
            InitializeComponent();
            m_InitValue = 0.0f;
        }

        public float RegFloatVal
        {
            get { return ntbRegProperty.GetFloat(); }
            set { ntbRegProperty.SetFloat(value); }
        }

        public float SelFloatVal
        {
            get { return ntbSelProperty.GetFloat(); }
            set { ntbSelProperty.SetFloat(value); }
        }


        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelFloatVal = m_InitValue;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelFloatVal);

            if (IsExistObjectState(objectState))
            {
                SelFloatVal = GetSelValByObjectState(objectState);
            }
        }

        //Special implement for IMcSymolicItem.SetAttachPointPositionValue 
        public void Save(delegateSaveWithParentParam func, uint numTypeParentIndex)
        {
            base.Save(func,numTypeParentIndex, RegFloatVal);
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegFloatVal);
        }

        public new void Load(delegateLoad func)
        {
            SelFloatVal = m_InitValue;
            RegFloatVal = base.Load(func);
        }

        //Special implement for IMcSymolicItem.GetAttachPointPositionValue  
        public new void Load(delegateLoadWithParentParam func, uint numAttachPointParentIndex)
        {
            RegFloatVal = base.Load(func, numAttachPointParentIndex);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFloatVal, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFloatVal, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FLOAT; }

        public override void SetValue(uint propertyId, object value) {
            if(RegPropertyID == propertyId)
                RegFloatVal = (float)value;
            if (SelPropertyID == propertyId)
                SelFloatVal = (float)value;
            SetStateValueByProperty(propertyId, (float)value);
        }

        private void ntbRegProperty_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFloatVal, this, new DNSByteBool(0));
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

        private void ntbSelProperty_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFloatVal, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerFloat : CtrlObjStatePropertyDataHandler<float>
    {
        
    }
}
