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
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.Managers.ObjectWorld;

namespace MCTester.Controls
{
    public partial class CtrlObjStatePropertyMesh : CtrlObjStatePropertyDataHandlerMesh
    {
        public CtrlObjStatePropertyMesh()
        {
            InitializeComponent();
            m_InitValue = null;

            ctrlRegMeshButtons.SetUpdateDelegate(FocusRegPropertyID);
            ctrlSelMeshButtons.SetUpdateDelegate(FocusSelPropertyID);

          //  AddControl(btnSelEdit);
          //  AddControl(btnSelDelete);
        }

        public IDNMcMesh GetRegPropertyMesh()
        {
            return ctrlRegMeshButtons.GetMesh();
        }

        public void SetRegPropertyMesh(IDNMcMesh mcMesh)
        {
            ctrlRegMeshButtons.SetMesh(mcMesh);
            ctrlRegMeshButtons.SetMeshButtonEnabled();
        }

        public IDNMcMesh GetSelPropertyMesh()
        {
            return ctrlSelMeshButtons.GetMesh();
        }

        public void SetSelPropertyMesh(IDNMcMesh mcMesh)
        {
            ctrlSelMeshButtons.SetMesh(mcMesh);
            ctrlSelMeshButtons.SetMeshButtonEnabled();
        }
        
        public override void ObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            OnObjectStateAddData(objectState);
            /*if (bInitCtrl)
                SelPropertyMesh = null;
            else
                EnabledButtons(false);*/
        }

        public override void ObjectStateChanged(byte objectState, byte previousState)
        {
            OnObjectStateChangedData(previousState, GetSelPropertyMesh());

            if (IsExistObjectState(objectState))
            {
                SetSelPropertyMesh(GetSelValByObjectState(objectState));
            }
        }
        public void Save(delegateSave func)
        {
            base.Save(func, GetRegPropertyMesh());
        }

        public new void Load(delegateLoad func)
        {
            SetSelPropertyMesh(m_InitValue);
            SetRegPropertyMesh(base.Load(func));
        }

        public override bool PropertyIdChanged()
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetRegPropertyMesh(), this, new DNSByteBool(0));
                SetRegPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override bool SelPropertyIdChanged(uint propertyId)
        {
            if (CurrentObjectSchemeNode != null)
            {
                MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(SelPropertyID, GetSelPropertyMesh(), this, ObjectState);
                SetSelPrivatePropertyValidationResult(validationResult);
                return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
            }
            return false;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_MESH; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetRegPropertyMesh((IDNMcMesh)value);
            if (SelPropertyID == propertyId)
                SetSelPropertyMesh((IDNMcMesh)value);
            SetStateValueByProperty(propertyId, (IDNMcMesh)value);
        }
    }

    public class CtrlObjStatePropertyDataHandlerMesh : CtrlObjStatePropertyDataHandler<IDNMcMesh> { }
}
