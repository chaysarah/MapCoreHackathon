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
    public partial class CtrlObjStatePropertyDictionaryBool : CtrlObjStatePropertyDataHandlerDicBool
    {
        private IDNMcMeshItem m_currentMesh;
        uint m_AttachPointID;
        private delegateLoadDicWithoutState delFuncLoad;

        public IDNMcMeshItem GetCurrentMesh(){ return m_currentMesh; }

        public void SetCurrentMesh(IDNMcMeshItem value)
        {
            m_currentMesh = value;
            if (m_currentMesh != null)
                delFuncLoad = m_currentMesh.GetSubPartInheritsParentRotation;
        }

        public CtrlObjStatePropertyDictionaryBool()
        {
            InitializeComponent();
            m_InitValue = false;
            HideObjStateTab();
        }

        public uint AttachPointID
        {
            get
            {
                return m_AttachPointID;
            }
            set
            {
                m_AttachPointID = value;
            }
        }

        public bool RegInheritsMeshRotation
        {
            get
            {
                return chkRegInheritsMeshRotation.Checked;
            }
            set
            {
                chkRegInheritsMeshRotation.Checked = value;
            }
        }

        public new void Save(delegateSaveDicWithoutState func)
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
                SaveCurrentAttachPointID();

            base.Save(func);
        }

        public new void Save(delegateSaveDic func)
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
                SaveCurrentAttachPointID();

            base.Save(func);
        }

        private void SaveCurrentAttachPointID()
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
            {
                SaveCurrentValue(AttachPointID, RegInheritsMeshRotation, false);
            }
        }

        public uint GetAttachPointID()
        {
            try
            {
                return uint.Parse(ntxRegAttachPointID.Text);
            }
            catch
            {
                return DNMcConstants._MC_EMPTY_ID;
            }
        }

        private void ntxRegAttachPointID_TextChanged(object sender, EventArgs e)
        {
            SaveCurrentAttachPointID();
            AttachPointID = GetAttachPointID();

            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
            {
                if (!IsExistRegKey(AttachPointID))
                {
                    List<uint> list = new List<uint>();
                    list.Add(AttachPointID);

                    LoadKeyList(list);
                    try
                    {
                        LoadRegular(delFuncLoad);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetTextureScrollSpeed", McEx);
                    }
                }
                RegInheritsMeshRotation = GetRegularCurrentValue(AttachPointID);
                LoadRegularPropertyID(AttachPointID);
            }
            else
            {
                RegInheritsMeshRotation = m_InitValue;
            }
        }

        public override bool PropertyIdChanged()
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                if (CurrentObjectSchemeNode != null)
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegInheritsMeshRotation, this, new DNSByteBool(0));
                    SetRegPrivatePropertyValidationResult(validationResult);
                    return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
                }
                return false;
            }
            else
                return true;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_BOOL; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                RegInheritsMeshRotation = (bool)value;
        }

        private void chkRegInheritsMeshRotation_Validating(object sender, CancelEventArgs e)
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                SaveCurrentAttachPointID();
                if (CheckIsNeedToDoValidation())
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, RegInheritsMeshRotation, this, new DNSByteBool(0));
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
        }

        private void lblRegMeshTextureID_Click(object sender, EventArgs e)
        {

        }
    }

    public class CtrlObjStatePropertyDataHandlerDicBool : CtrlObjStatePropertyDictionaryDataHandler<uint, bool> { }
}
