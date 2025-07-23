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
    // DNSArrayProperty<DNSMcBNumber>
    public partial class CtrlObjStatePropertyUintArray : CtrlObjStatePropertyDataHandlerUintArray
    {
        public CtrlObjStatePropertyUintArray()
        {
            InitializeComponent();
        }

        public bool IsSeperateWithTwiceSpace
        {
            get;set;
        }

        public DNSArrayProperty<uint> RegNumberArrayPropertyValue
        {
            get
            {
                return RegNumberArray.UIntArrayPropertyValue;
            }
            set
            {
                RegNumberArray.UIntArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<uint> SelNumberArrayPropertyValue
        {
            get
            {
                return SelNumberArray.UIntArrayPropertyValue;
            }
            set
            {
                SelNumberArray.UIntArrayPropertyValue = value;
            }
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelNumberArrayPropertyValue = new DNSArrayProperty<uint>();
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelNumberArrayPropertyValue);

            if (IsExistObjectState(objectState))
            {
                SelNumberArrayPropertyValue = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegNumberArrayPropertyValue);
        }

        public new void Load(delegateLoad func)
        {
            SelNumberArrayPropertyValue = m_InitValue;
            RegNumberArrayPropertyValue = base.Load(func);
        }

        internal void SetRenderingMode(DNERenderingMode renderingMode)
        {
            switch(renderingMode)
            {
                case DNERenderingMode._ERM_POINTS:
                    RegNumberArray.SeperateNumbers = 1;break;
                case DNERenderingMode._ERM_LINES:
                    RegNumberArray.SeperateNumbers = 2; break;
                case DNERenderingMode._ERM_TRIANGLES:
                    RegNumberArray.SeperateNumbers = 3; break;
            }
            RegNumberArrayPropertyValue = RegNumberArrayPropertyValue;
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegNumberArrayPropertyValue, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelNumberArrayPropertyValue, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_UINT_ARRAY; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegNumberArrayPropertyValue = (DNSArrayProperty<uint>)value;
            if (SelPropertyID == propertyId)
                SelNumberArrayPropertyValue = (DNSArrayProperty<uint>)value;
            SetStateValueByProperty(propertyId, (DNSArrayProperty<uint>)value);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void RegNumberArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegNumberArrayPropertyValue, this, new DNSByteBool(0));
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

        private void SelNumberArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelNumberArrayPropertyValue, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerUintArray : CtrlObjStatePropertyDataHandler<DNSArrayProperty<uint>> { }
}
