using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyBool : CtrlObjStatePropertyDataHandlerBool
    {
        public CtrlObjStatePropertyBool()
        {
            InitializeComponent();
            m_InitValue = false;
        }

        public bool RegBoolVal
        {
            get { return this.chxRegBool.Checked; }
            set { this.chxRegBool.Checked = value; }
        }

        public string BoolLabel
        {
            get { return this.chxRegBool.Text; }
            set 
            { 
                this.chxRegBool.Text = value;
                this.chxSelBool.Text = value;
            }
        }

        public bool SelBoolVal
        {
            get { return this.chxSelBool.Checked; }
            set { this.chxSelBool.Checked = value; }
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                SelBoolVal = m_InitValue;
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelBoolVal);

            if (IsExistObjectState(objectState))
            {
                SelBoolVal = GetSelValByObjectState(objectState);
            }
        }

        public void Save(delegateSaveWithoutState func)
        {
            base.Save(func, RegBoolVal);
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegBoolVal);
        }

        public new void Load(delegateLoad func)
        {
            SelBoolVal = m_InitValue;
            RegBoolVal = base.Load(func);
        }

        public new void Load(delegateLoadWithoutState func)
        {
            RegBoolVal = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegBoolVal, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelBoolVal, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_BOOL; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegBoolVal = (bool)value;
            if (SelPropertyID == propertyId)
                SelBoolVal = (bool)value;
            SetStateValueByProperty(propertyId, (bool)value);
        }

        private void chxRegBool_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegBoolVal, this, new DNSByteBool(0));
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

        private void chxSelBool_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelBoolVal, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerBool : CtrlObjStatePropertyDataHandler<bool> { }

}
