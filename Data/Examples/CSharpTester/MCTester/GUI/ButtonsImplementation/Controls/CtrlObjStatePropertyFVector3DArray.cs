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
    public partial class CtrlObjStatePropertyFVector3DArray : CtrlObjStatePropertyDataHandlerFVector3DArray
    {
        public CtrlObjStatePropertyFVector3DArray()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcFVector3D> RegFVector3DArrValue
        {
            get
            {
                return ctrlRegVector3DArray.FVector3DArrayPropertyValue;
            }
            set
            {
                ctrlRegVector3DArray.FVector3DArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcFVector3D> SelFVector3DArrValue
        {
            get
            {
                return ctrlSelVector3DArray.FVector3DArrayPropertyValue;
            }
            set
            {
                ctrlSelVector3DArray.FVector3DArrayPropertyValue = value;
            }
        }

        private void btnSelReset_Click(object sender, EventArgs e)
        {
            ctrlSelVector3DArray.ResetGrid();
            FocusSelPropertyID();
        }

        private void btnRegReset_Click(object sender, EventArgs e)
        {
            ctrlRegVector3DArray.ResetGrid();
           FocusRegPropertyID();
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                ctrlSelVector3DArray.ResetGrid();
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelFVector3DArrValue);

            if (IsExistObjectState(objectState))
            {
                SelFVector3DArrValue = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegFVector3DArrValue);
        }

        public new void Load(delegateLoad func)
        {
            SelFVector3DArrValue = m_InitValue;
            RegFVector3DArrValue = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFVector3DArrValue, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFVector3DArrValue, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FVECTOR3D_ARRAY; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegFVector3DArrValue = (DNSArrayProperty<DNSMcFVector3D>)value;
            if (SelPropertyID == propertyId)
                SelFVector3DArrValue = (DNSArrayProperty<DNSMcFVector3D>)value;
            SetStateValueByProperty(propertyId, (DNSArrayProperty<DNSMcFVector3D>)value);
        }

        private void ctrlRegFVector3DArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFVector3DArrValue, this, new DNSByteBool(0));
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

        private void ctrlSelFVector3DArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFVector3DArrValue, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerFVector3DArray : CtrlObjStatePropertyDataHandler<DNSArrayProperty<DNSMcFVector3D>> { }
}
