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
    public partial class CtrlObjStatePropertyFVector2DArray : CtrlObjStatePropertyDataHandlerFVector2DArray
    {
        public CtrlObjStatePropertyFVector2DArray()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcFVector2D> RegFVector2DArrValue
        {
            get
            {
                return ctrlRegVector2DArray.FVector2DArrayPropertyValue;
            }
            set
            {
                ctrlRegVector2DArray.FVector2DArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcFVector2D> SelFVector2DArrValue
        {
            get
            {
                return ctrlSelVector2DArray.FVector2DArrayPropertyValue;
            }
            set
            {
                ctrlSelVector2DArray.FVector2DArrayPropertyValue = value;
            }
        }

        private void btnSelReset_Click(object sender, EventArgs e)
        {
            ctrlSelVector2DArray.ResetGrid();
            FocusSelPropertyID();
        }

        private void btnRegReset_Click(object sender, EventArgs e)
        {
            ctrlSelVector2DArray.ResetGrid();
            FocusSelPropertyID();
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                ctrlSelVector2DArray.ResetGrid();
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelFVector2DArrValue);

            if (IsExistObjectState(objectState))
            {
                SelFVector2DArrValue = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegFVector2DArrValue);
        }

        public new void Load(delegateLoad func)
        {
            SelFVector2DArrValue = m_InitValue;
            RegFVector2DArrValue = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFVector2DArrValue, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFVector2DArrValue, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FVECTOR2D_ARRAY; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegFVector2DArrValue = (DNSArrayProperty<DNSMcFVector2D>)value;
            if (SelPropertyID == propertyId)
                SelFVector2DArrValue = (DNSArrayProperty<DNSMcFVector2D>)value;
            SetStateValueByProperty(propertyId, (DNSArrayProperty<DNSMcFVector2D>)value);
        }

        private void ctrlRegVector2DArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFVector2DArrValue, this, new DNSByteBool(0));
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

        private void ctrlSelVector2DArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFVector2DArrValue, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerFVector2DArray : CtrlObjStatePropertyDataHandler<DNSArrayProperty<DNSMcFVector2D>> { }
}
