using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using System.Linq;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyFVect3D : CtrlObjStatePropertyDataHandlerDNSMcFVector3D 
    {
        public CtrlObjStatePropertyFVect3D()
        {
            InitializeComponent();
            m_InitValue = DNSMcFVector3D.v3Zero;
        }

        public DNSMcFVector3D GetRegFVector3DVal()
        {
            return ctrl3DRegFVector.GetVector3D();
        }

        public void SetRegFVector3DVal(DNSMcFVector3D value)
        {
            ctrl3DRegFVector.SetVector3D(value);
        }

        public DNSMcFVector3D GetSelFVector3DVal()
        {
            return ctrl3DSelFVector.GetVector3D();
        }

        public void SetSelFVector3DVal(DNSMcFVector3D value)
        {
            ctrl3DSelFVector.SetVector3D(value);
        }

        public DNSMcFVector3D GetSelFVector3DVal(byte objectState)
        {
            return GetSelValByObjectState(objectState);
        }

        public void SetSelFVector3DVal(byte objectState, DNSMcFVector3D value)
        {
            SetSelValByObjectState(objectState, value);
            ctrl3DSelFVector.SetVector3D( value);
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState );
            if (bInitCtrl)
                SetSelFVector3DVal(m_InitValue);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, ctrl3DSelFVector.GetVector3D());

            if (IsExistObjectState(objectState))
            {
                ctrl3DSelFVector.SetVector3D( GetSelValByObjectState(objectState));
            }
            else
            {
                ctrl3DSelFVector.SetVector3D(m_InitValue);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, GetRegFVector3DVal());
        }

        public new void Load(delegateLoad func)
        {
            SetSelFVector3DVal(m_InitValue);
            SetRegFVector3DVal(base.Load(func));
        }    

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegFVector3DVal(), this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelFVector3DVal(), this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FVECTOR3D; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetRegFVector3DVal((DNSMcFVector3D)value);
            if (SelPropertyID == propertyId)
                SetSelFVector3DVal((DNSMcFVector3D)value);
            SetStateValueByProperty(propertyId, (DNSMcFVector3D)value);
        }

        private void ctrl3DRegFVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegFVector3DVal(), this, new DNSByteBool(0));
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

        private void ctrl3DSelFVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelFVector3DVal(), this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerDNSMcFVector3D : CtrlObjStatePropertyDataHandler<DNSMcFVector3D> { }
}
