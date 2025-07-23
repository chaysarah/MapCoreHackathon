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
    public partial class CtrlObjStatePropertyFVect2D : CtrlObjStatePropertyDataHandlerFVector2D
    {
        public CtrlObjStatePropertyFVect2D()
        {
            InitializeComponent();
        }

        public DNSMcFVector2D GetRegFVector2DVal()
        {
            return this.ctrl2DRegFVector.GetVector2D();
        }
    
        public void SetRegFVector2DVal(DNSMcFVector2D value)
        {
            this.ctrl2DRegFVector.SetVector2D(value); 
        }

        public DNSMcFVector2D GetSelFVector2DVal()
        {
            return this.ctrl2DSelFVector.GetVector2D();
        }

        public void SetSelFVector2DVal(DNSMcFVector2D value)
        {
            this.ctrl2DSelFVector.SetVector2D(value); 
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, GetSelFVector2DVal());

            if (IsExistObjectState(objectState))
            {
                SetSelFVector2DVal(GetSelValByObjectState(objectState));
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, GetRegFVector2DVal());
        }

        public new void Load(delegateLoad func)
        {
            SetSelFVector2DVal(m_InitValue);
            SetRegFVector2DVal(base.Load(func));
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegFVector2DVal(), this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelFVector2DVal(), this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FVECTOR2D; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetRegFVector2DVal((DNSMcFVector2D)value);
            if (SelPropertyID == propertyId)
                SetSelFVector2DVal((DNSMcFVector2D)value);
            SetStateValueByProperty(propertyId, (DNSMcFVector2D)value);
        }

        private void ctrl2DRegFVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegFVector2DVal(), this, new DNSByteBool(0));
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

        private void ctrl2DSelFVector_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelFVector2DVal(), this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerFVector2D : CtrlObjStatePropertyDataHandler<DNSMcFVector2D> { }
}
