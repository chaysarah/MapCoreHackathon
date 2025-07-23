using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.Linq;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyVect3D : CtrlObjStatePropertyDataHandlerVector3D
    {
        public CtrlObjStatePropertyVect3D()
        {
            InitializeComponent();
        }

        public DNSMcVector3D GetRegVector3DVal()
        {
            return ctrlReg3DVector.GetVector3D();
        }

        public void SetRegVector3DVal(DNSMcVector3D value)
        {
            ctrlReg3DVector.SetVector3D(value);
        }

        public DNSMcVector3D GetSelVector3DVal() { return ctrlSel3DVector.GetVector3D(); }

        public void SetSelVector3DVal(DNSMcVector3D value){ ctrlSel3DVector.SetVector3D( value); }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, GetSelVector3DVal());

            if (IsExistObjectState(objectState))
            {
                SetSelVector3DVal(GetSelValByObjectState(objectState));
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, GetRegVector3DVal());
        }

        public new void Load(delegateLoad func)
        {
            SetSelVector3DVal(m_InitValue);
            SetRegVector3DVal(base.Load(func));
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegVector3DVal(), this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelVector3DVal(), this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_VECTOR3D; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetRegVector3DVal((DNSMcVector3D)value);
            if (SelPropertyID == propertyId)
                SetSelVector3DVal((DNSMcVector3D)value);
            SetStateValueByProperty(propertyId, (DNSMcVector3D)value);
        }

        private void ctrlReg3DVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegVector3DVal(), this, new DNSByteBool(0));
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

        private void ctrlSel3DVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelVector3DVal(), this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerVector3D : CtrlObjStatePropertyDataHandler<DNSMcVector3D> { }
}
