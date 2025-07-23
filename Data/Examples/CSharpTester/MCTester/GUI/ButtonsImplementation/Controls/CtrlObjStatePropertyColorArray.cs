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
    // DNSArrayProperty<DNSMcBColor>
    public partial class CtrlObjStatePropertyColorArray : CtrlObjStatePropertyDataHandlerColorArray
    {
        public CtrlObjStatePropertyColorArray()
        {
            InitializeComponent();
        }

        public DNSArrayProperty<DNSMcBColor> RegBColorsPropertyValue
        {
            get
            {
                return RegPropertyColorArray.BColorsPropertyValue;
            }
            set
            {
                RegPropertyColorArray.BColorsPropertyValue = value;
            }
        }

        public DNSArrayProperty<DNSMcBColor> SelBColorsPropertyValue
        {
            get
            {
                return SelPropertyColorArray.BColorsPropertyValue;
            }
            set
            {
                SelPropertyColorArray.BColorsPropertyValue = value;
            }
        }

        private void btnSelReset_Click(object sender, EventArgs e)
        {
            SelPropertyColorArray.ResetGrid();
            FocusSelPropertyID();
        }

        private void btnRegReset_Click(object sender, EventArgs e)
        {
            RegPropertyColorArray.ResetGrid();
            FocusRegPropertyID();
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelPropertyColorArray.ResetGrid();
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelBColorsPropertyValue);

            if (IsExistObjectState(objectState))
            {
                SelBColorsPropertyValue = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegBColorsPropertyValue);
        }

        public new void Load(delegateLoad func)
        {
            SelBColorsPropertyValue = m_InitValue;
            RegBColorsPropertyValue = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegBColorsPropertyValue, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelBColorsPropertyValue, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_BCOLOR_ARRAY; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegBColorsPropertyValue = (DNSArrayProperty<DNSMcBColor>)value;
            if (SelPropertyID == propertyId)
                SelBColorsPropertyValue = (DNSArrayProperty<DNSMcBColor>)value;
            SetStateValueByProperty(propertyId, (DNSArrayProperty<DNSMcBColor>)value);
        }

       

        private void RegPropertyColorArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegBColorsPropertyValue, this, new DNSByteBool(0));
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

        private void SelPropertyColorArray_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelBColorsPropertyValue, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerColorArray : CtrlObjStatePropertyDataHandler<DNSArrayProperty<DNSMcBColor>> { }
}
