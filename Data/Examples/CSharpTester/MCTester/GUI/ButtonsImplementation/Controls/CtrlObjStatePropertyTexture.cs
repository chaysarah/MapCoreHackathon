using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyTexture : CtrlObjStatePropertyDataHandlerTexture
    {

        public CtrlObjStatePropertyTexture()
        {
            InitializeComponent();
            m_InitValue = null;
            ctrlRegTextureButtons.SetUpdateDelegate(FocusRegPropertyID);
            ctrlSelTextureButtons.SetUpdateDelegate(FocusSelPropertyID);
        }

        public IDNMcTexture GetRegPropertyTexture()
        {
            return ctrlRegTextureButtons.GetTexture();
        }

        public void SetRegPropertyTexture(IDNMcTexture Texture)
        {
            ctrlRegTextureButtons.SetTexture(Texture);
        }

        public IDNMcTexture GetSelPropertyTexture()
        {
            return ctrlSelTextureButtons.GetTexture();
        }

        public void SetSelPropertyTexture(IDNMcTexture Texture)
        {
            ctrlSelTextureButtons.SetTexture(Texture);
        }

        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            if (bInitCtrl)
                ctrlSelTextureButtons.SetTexture(null);
            else
                ctrlSelTextureButtons.ChangeButtonsEnabled();
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, ctrlSelTextureButtons.GetTexture());

            if (IsExistObjectState(objectState))
            {
                SetSelPropertyTexture(GetSelValByObjectState(objectState));
            }
            else
                ctrlSelTextureButtons.ChangeButtonsEnabled();
        }

        public void Save(delegateSave func)
        {
            base.Save(func, ctrlRegTextureButtons.GetTexture());
        }

        public new void Load(delegateLoad func)
        {
            ctrlSelTextureButtons.SetTexture(m_InitValue);
            ctrlRegTextureButtons.SetTexture(base.Load(func));
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, ctrlRegTextureButtons.GetTexture() , this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, ctrlSelTextureButtons.GetTexture() , this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_TEXTURE; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                ctrlRegTextureButtons.SetTexture((IDNMcTexture)value);
            if (SelPropertyID == propertyId)
                ctrlSelTextureButtons.SetTexture((IDNMcTexture)value);
            SetStateValueByProperty(propertyId, (IDNMcTexture)value);
        }
    }


    public class CtrlObjStatePropertyDataHandlerTexture : CtrlObjStatePropertyDataHandler<IDNMcTexture> { }
}
