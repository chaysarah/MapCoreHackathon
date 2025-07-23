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
    public partial class CtrlObjStatePropertyVector3DArray : CtrlObjStatePropertyDataHandlerVector3DArray
    {
        public CtrlObjStatePropertyVector3DArray()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcVector3D> RegVector3DArrValue
        {
            get
            {
                return ctrlRegVector3DArray.Vector3DArrayPropertyValue;
            }
            set
            {
                ctrlRegVector3DArray.Vector3DArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcVector3D> SelVector3DArrValue
        {
            get
            {
                return ctrlSelVector3DArray.Vector3DArrayPropertyValue;
            }
            set
            {
                ctrlSelVector3DArray.Vector3DArrayPropertyValue = value;
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
            OnObjectStateChangedData(previousState, SelVector3DArrValue);

            if (IsExistObjectState(objectState))
            {
                SelVector3DArrValue = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegVector3DArrValue);
        }

        public new void Load(delegateLoad func)
        {
            SelVector3DArrValue = m_InitValue;
            RegVector3DArrValue = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegVector3DArrValue, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelVector3DArrValue, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_VECTOR3D_ARRAY; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegVector3DArrValue = (DNSArrayProperty<DNSMcVector3D>)value;
            if (SelPropertyID == propertyId)
                SelVector3DArrValue = (DNSArrayProperty<DNSMcVector3D>)value;
            SetStateValueByProperty(propertyId, (DNSArrayProperty<DNSMcVector3D>)value);
        }

        private void ctrlRegVector3DArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegVector3DArrValue, this, new DNSByteBool(0));
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

        private void ctrlSelVector3DArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelVector3DArrValue, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerVector3DArray : CtrlObjStatePropertyDataHandler<DNSArrayProperty<DNSMcVector3D>> { }
}
