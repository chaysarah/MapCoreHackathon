using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyVect2DPoint : CtrlObjStatePropertyDataHandlerVector2D
    {
        private bool mIsInRegValidation = true;
        private bool mIsInSelValidation = true;

        public CtrlObjStatePropertyVect2DPoint()
        {
            InitializeComponent();
        }

        public DNSMcVector2D GetRegVector2DVal(){ return ctrlReg2DVector.GetVector2D(); }

        public void SetRegVector2DVal(DNSMcVector2D value){ ctrlReg2DVector.SetVector2D(value); }

        public DNSMcVector2D GetSelVector2DVal(){ return this.ctrlSel2DVector.GetVector2D(); }
        public void SetSelVector2DVal(DNSMcVector2D value){ this.ctrlSel2DVector.SetVector2D(value); }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, GetSelVector2DVal());

            if (IsExistObjectState(objectState))
            {
                SetSelVector2DVal(GetSelValByObjectState(objectState));
            }

        }

        public void Save(delegateSave func)
        {
            base.Save(func, GetRegVector2DVal());
        }

        public new void Load(delegateLoad func)
        {
            SetSelVector2DVal(m_InitValue);
            SetRegVector2DVal(base.Load(func));
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null && mIsInRegValidation)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegVector2DVal(), this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null && mIsInSelValidation)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelVector2DVal(), this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_VECTOR2D; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetRegVector2DVal((DNSMcVector2D)value);
            if (SelPropertyID == propertyId)
                SetSelVector2DVal((DNSMcVector2D)value);
            SetStateValueByProperty(propertyId, (DNSMcVector2D)value);
        }

        private void ctrlRegSamplePoint_Leave(object sender, EventArgs e)
        {
            mIsInSelValidation = false;
            FocusRegPropertyID();
            mIsInSelValidation = true;
        }

        private void ctrlSelSamplePoint_Leave(object sender, EventArgs e)
        {
            mIsInRegValidation = false;
            FocusSelPropertyID();
            mIsInRegValidation = true;
        }

        private void ctrlReg2DVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation() && mIsInRegValidation)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegVector2DVal(), this, new DNSByteBool(0));
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

        private void ctrlSel2DVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation() && mIsInSelValidation)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelVector2DVal(), this, ObjectState);
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

    
}
