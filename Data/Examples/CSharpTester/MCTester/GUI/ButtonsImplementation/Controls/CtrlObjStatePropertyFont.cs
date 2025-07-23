using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.General_Forms;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyFont : CtrlObjStatePropertyDataHandlerFont
    { 
       
        public CtrlObjStatePropertyFont()
        {
            InitializeComponent();
            ctrlRegFontButtons.SetUpdateDelegate(FocusRegPropertyID);
            ctrlSelFontButtons.SetUpdateDelegate(FocusSelPropertyID);
        }

        public IDNMcFont GetRegPropertyFont()
        {
             return ctrlRegFontButtons.GetFont(); 
        }

        public void SetRegPropertyFont(IDNMcFont mcFont)
        {
            ctrlRegFontButtons.SetFont(mcFont);
            ctrlRegFontButtons.SetFontButtonEnabled();
        }

        public IDNMcFont GetSelPropertyFont()
        {
            return ctrlSelFontButtons.GetFont();
        }

        public void SetSelPropertyFont(IDNMcFont mcFont)
        {
            ctrlSelFontButtons.SetFont(mcFont);
            ctrlSelFontButtons.SetFontButtonEnabled();
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, GetSelPropertyFont());

            if (IsExistObjectState(objectState))
            {
                SetSelPropertyFont(GetSelValByObjectState(objectState));
            }
        }

        public void Save(delegateSave func)
        {
            base.Save(func, GetRegPropertyFont());
        }

        public new void Load(delegateLoad func)
        {
            SetSelPropertyFont(m_InitValue);
            SetRegPropertyFont(base.Load(func));
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegPropertyFont(), this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelPropertyFont(), this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_FONT; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetRegPropertyFont((IDNMcFont)value);
            if (SelPropertyID == propertyId)
                SetSelPropertyFont((IDNMcFont)value);
            SetStateValueByProperty(propertyId, (IDNMcFont)value);
        }
    }

    public class CtrlObjStatePropertyDataHandlerFont : CtrlObjStatePropertyDataHandler<IDNMcFont> { }
}
