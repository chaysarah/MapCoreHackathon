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
    public partial class CtrlObjStatePropertyInt : CtrlObjStatePropertyDataHandlerInt
    {
        public CtrlObjStatePropertyInt()
        {
            InitializeComponent();
            base.m_InitValue = 0;
        }

        public int RegIntVal
        {
            get { return ntbRegProperty.GetInt32(); }
            set { ntbRegProperty.SetInt(value); }
        }

        public int SelIntVal
        {
            get { return ntbSelProperty.GetInt32(); }
            set { ntbSelProperty.SetInt(value); }
        }


        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelIntVal = 0;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelIntVal);

            if (IsExistObjectState(objectState))
            {
                SelIntVal = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegIntVal);
        }

        public new void Load(delegateLoad func)
        {
            SelIntVal = m_InitValue;
            RegIntVal = base.Load(func);
        }

        //Special implement for IMcSymolicItem.GetAttachPointPositionValue  
        public new void Load(delegateLoadWithParentParam func, uint numAttachPointParentIndex)
        {
            RegIntVal = base.Load(func, numAttachPointParentIndex);
        }

        //Special implement for IMcSymolicItem.SetAttachPointIndex\NumAttachPoints 
        public void Save(delegateSaveWithParentParam func, uint numTypeParentIndex)
        {
            base.Save(func, numTypeParentIndex, RegIntVal);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegIntVal, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelIntVal, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_INT; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegIntVal = (int)value;
            if (SelPropertyID == propertyId)
                SelIntVal = (int)value;
            SetStateValueByProperty(propertyId, (int)value);
        }

        private void ntbRegProperty_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegIntVal, this, new DNSByteBool(0));
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
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelIntVal, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerInt : CtrlObjStatePropertyDataHandler<Int32>
    {

    }
}
