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
    public partial class CtrlObjStatePropertyAttenuation : CtrlObjStatePropertyDataHandlerAttenuation
    {
        public CtrlObjStatePropertyAttenuation()
        {
            InitializeComponent();
            m_InitValue = new DNSMcAttenuation(0, 0, 0, 0);
        }

        public DNSMcAttenuation RegAttenuation
        {
            get
            {
                return new DNSMcAttenuation(ntxRegConst.GetFloat(), ntxRegLinear.GetFloat(), ntxRegSquare.GetFloat(), ntxRegRange.GetFloat());
            }
            set
            {
                ntxRegConst.SetFloat(value.fConst);
                ntxRegLinear.SetFloat(value.fLinear);
                ntxRegSquare.SetFloat(value.fSquare);
                ntxRegRange.SetFloat(value.fRange);
            }
        }

        public DNSMcAttenuation SelAttenuation
        {
            get
            {
                return new DNSMcAttenuation(ntxSelConst.GetFloat(), ntxSelLinear.GetFloat(), ntxSelSquare.GetFloat(), ntxSelRange.GetFloat());
            }
            set
            {
                ntxSelConst.SetFloat(value.fConst);
                ntxSelLinear.SetFloat(value.fLinear);
                ntxSelSquare.SetFloat(value.fSquare);
                ntxSelRange.SetFloat(value.fRange);
            }
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelAttenuation = m_InitValue;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelAttenuation);

            if (IsExistObjectState(objectState))
            {
                SelAttenuation = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegAttenuation);
        }

        public new void Load(delegateLoad func)
        {
            RegAttenuation = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegAttenuation, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelAttenuation, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_ATTENUATION; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegAttenuation = (DNSMcAttenuation)value;
            if (SelPropertyID == propertyId)
                SelAttenuation = (DNSMcAttenuation)value;
            SetStateValueByProperty(propertyId, (DNSMcAttenuation)value);
        }

        private void ntxRegRange_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegAttenuation, this, new DNSByteBool(0));
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

        private void ntxRegConst_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegAttenuation, this, new DNSByteBool(0));
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

        private void ntxRegLinear_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegAttenuation, this, new DNSByteBool(0));
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

        private void ntxRegSquare_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegAttenuation, this, new DNSByteBool(0));
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

        private void ntxSelConst_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelAttenuation, this, ObjectState);
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

        private void ntxSelRange_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelAttenuation, this, ObjectState);
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

        private void ntxSelLinear_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelAttenuation, this, ObjectState);
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

        private void ntxSelSquare_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelAttenuation, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerAttenuation : CtrlObjStatePropertyDataHandler<DNSMcAttenuation> { }
}
