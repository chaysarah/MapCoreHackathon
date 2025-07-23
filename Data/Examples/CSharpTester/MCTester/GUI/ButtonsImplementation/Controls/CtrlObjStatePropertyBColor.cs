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
    public partial class CtrlObjStatePropertyBColor : CtrlObjStatePropertyDataHandlerColor
    {
        public CtrlObjStatePropertyBColor()
        {
            InitializeComponent();
            m_InitValue = DNSMcBColor.bcBlackOpaque;

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

        public DNSMcBColor RegBColor
        {
            get
            {
                return ctrlRegColor.BColor;
            }

            set
            {
                ctrlRegColor.BColor = value;
            }
        }

        public Color SelAlphaColor
        {
            set
            {
                ctrlSelColor.AlphaColor = value;
            }
        }

        public DNSMcBColor SelBColor
        {
            get
            {
                return ctrlSelColor.BColor;
            }

            set
            {
                ctrlSelColor.BColor = value;
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
            OnObjectStateChangedData(previousState, SelBColor);

            if (IsExistObjectState(objectState))
            {
                SelBColor = GetSelValByObjectState(objectState);
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
            base.Save(func, RegBColor);
        }

        public new void Load(delegateLoad func)
        {
            SelBColor = m_InitValue;
            RegBColor = base.Load(func);
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegBColor, this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelBColor, this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_BCOLOR; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegBColor = (DNSMcBColor)value;
            if (SelPropertyID == propertyId)
                SelBColor = (DNSMcBColor)value;
            SetStateValueByProperty(propertyId, (DNSMcBColor)value);
        }

        private void ctrlRegColor_Validating(object sender, CancelEventArgs e)
        {
            if (CheckIsNeedToDoValidation())
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegBColor, this, new DNSByteBool(0));
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
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, SelBColor, this, ObjectState);
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

    public class CtrlObjStatePropertyDataHandlerColor : CtrlObjStatePropertyDataHandler<DNSMcBColor> { }
}
