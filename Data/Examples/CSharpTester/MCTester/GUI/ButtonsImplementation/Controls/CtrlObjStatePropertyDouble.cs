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
    public partial class CtrlObjStatePropertyDouble : CtrlObjStatePropertyDataHandlerDouble
    {
        public CtrlObjStatePropertyDouble()
        {
            InitializeComponent();
            m_InitValue = 0.0;
        }

        public Double RegDoubleVal
        {
            get { return ntbRegProperty.GetDouble(); }
            set { ntbRegProperty.SetDouble(value); }
        }

        public Double SelDoubleVal
        {
            get { return ntbSelProperty.GetDouble(); }
            set { ntbSelProperty.SetDouble(value); }
        }


        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelDoubleVal = 0.0;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelDoubleVal);

            if (IsExistObjectState(objectState))
            {
                SelDoubleVal = GetSelValByObjectState(objectState);
            }
        }

        //Special implement for IMcSymolicItem.SetAttachPointPositionValue 
        public void Save(delegateSaveWithParentParam func, uint numTypeParentIndex)
        {
            base.Save(func,numTypeParentIndex, RegDoubleVal);
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegDoubleVal);
        }

        public new void Load(delegateLoad func)
        {
            SelDoubleVal = m_InitValue;
            RegDoubleVal = base.Load(func);
        }

        //Special implement for IMcSymolicItem.GetAttachPointPositionValue  
        public new void Load(delegateLoadWithParentParam func, uint numAttachPointParentIndex)
        {
            RegDoubleVal = base.Load(func, numAttachPointParentIndex);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegDoubleVal, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelDoubleVal, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_INT; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegDoubleVal = (int)value;
            if (SelPropertyID == propertyId)
                SelDoubleVal = (int)value;
            SetStateValueByProperty(propertyId, (int)value);
        }

        private void ntbRegProperty_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegDoubleVal, this, new DNSByteBool(0));
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
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelDoubleVal, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerDouble : CtrlObjStatePropertyDataHandler<Double>
    {
        
    }
}
