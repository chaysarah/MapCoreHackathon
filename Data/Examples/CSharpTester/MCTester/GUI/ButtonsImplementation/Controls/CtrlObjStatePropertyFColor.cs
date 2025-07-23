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
    public partial class CtrlObjStatePropertyFColor : CtrlObjStatePropertyDataHandlerFColor
    {
        public CtrlObjStatePropertyFColor()
        {
            InitializeComponent();
            m_InitValue = DNSMcFColor.fcBlackOpaque;

            ctrlRegColor.EnabledButtons(true);
            EnabledStateButtons(false);
        }

        public Color RegAlphaColor
        {
            set
            {
                ctrlRegColor.AlphaColor = value;
            }
        }

        public DNSMcFColor RegFColor
        {
            get
            {
                return ctrlRegColor.FColor;
            }

            set
            {
                ctrlRegColor.FColor = value;
            }
        }

        public Color SelAlphaColor
        {
            set
            {
                ctrlSelColor.AlphaColor = value;
            }
        }

        public DNSMcFColor SelFColor
        {
            get
            {
                return ctrlSelColor.FColor;
            }

            set
            {
                ctrlSelColor.FColor = value;
            }
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            //if (bInitCtrl)
            //    m_SelSelectedColor = Color.Black;

            EnabledStateButtons(true);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, SelFColor);

            if (IsExistObjectState(objectState))
            {
                SelFColor = GetSelValByObjectState(objectState);
            }

            bool isEnabled = false;
            if (ObjectStates != null && ObjectStates.Length > 0)
                isEnabled = true;

            EnabledStateButtons(isEnabled);
        }

        private void EnabledStateButtons(bool isEnabled)
        {
            ctrlSelColor.EnabledButtons(isEnabled);
        }

        public void Save(delegateSave func)
        {
            base.Save(func, RegFColor);
        }

        public new void Load(delegateLoad func)
        {
            SelFColor = m_InitValue;
            RegFColor = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFColor, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFColor, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FCOLOR; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegFColor = (DNSMcFColor)value;
            if (SelPropertyID == propertyId)
                SelFColor = (DNSMcFColor)value;
            SetStateValueByProperty(propertyId, (DNSMcFColor)value);
        }

        private void ctrlRegColor_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegFColor, this, new DNSByteBool(0));
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

        private void ctrlSelColor_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelFColor, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerFColor : CtrlObjStatePropertyDataHandler<DNSMcFColor> { }
}
