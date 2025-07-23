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
    public partial class CtrlObjStatePropertySubPartRotation : CtrlObjStatePropertyDataHandlerSubPartRotation
    {
        private IDNMcMeshItem m_currentMesh;
        uint m_AttachPointID;
        private delegateLoadDicWithoutState delFuncLoad;

        public IDNMcMeshItem GetCurrentMesh(){ return m_currentMesh; }

        public void SetCurrentMesh(IDNMcMeshItem value)
        {
            m_currentMesh = value;
            if (m_currentMesh != null)
            {
                delFuncLoad = m_currentMesh.GetSubPartRotation;
                CurrentObjectSchemeNode = m_currentMesh;
            }
        }

        public CtrlObjStatePropertySubPartRotation()
        {
            InitializeComponent();
            m_InitValue = new DNSMcRotation();
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

        public DNSMcRotation GetReg3DRegOrientation()
        {
            return new DNSMcRotation(ctrl3DRegOrientation.Yaw,
                                        ctrl3DRegOrientation.Pitch,
                                        ctrl3DRegOrientation.Roll,
                                        chkRegRelativeToCurrOrientation.Checked);
        }

        public void SetReg3DRegOrientation(DNSMcRotation value)
        {
            ctrl3DRegOrientation.Yaw = value.fYaw;
            ctrl3DRegOrientation.Pitch = value.fPitch;
            ctrl3DRegOrientation.Roll = value.fRoll;
            chkRegRelativeToCurrOrientation.Checked = value.bRelativeToCurrOrientation;
        }

        public new void Save(delegateSaveDic func)
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
                SaveCurrentAttachPointID();

            base.Save(func);
        }

        public new void Save(delegateSaveDicWithoutState func)
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
                SaveCurrentAttachPointID();

            base.Save(func);
        }

        private void SaveCurrentAttachPointID()
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID)
            {
                SaveCurrentValue(AttachPointID, GetReg3DRegOrientation(), false);
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
                SetReg3DRegOrientation(GetRegularCurrentValue(AttachPointID));
                LoadRegularPropertyID(AttachPointID);
            }
            else
            {
                SetReg3DRegOrientation(m_InitValue);
            }
        }

        public override bool PropertyIdChanged()
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                SaveCurrentAttachPointID();
                if (CurrentObjectSchemeNode != null)
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetReg3DRegOrientation(), this, new DNSByteBool(0), AttachPointID);
                    SetRegPrivatePropertyValidationResult(validationResult);
                    return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
                }
                return false;
            }
            else
                return true;
        }

        public override DNEPropertyType GetPropertyType() { return DNEPropertyType._EPT_ROTATION; }

        public override void SetValue(uint propertyId, object value)
        {
            if (RegPropertyID == propertyId)
                SetReg3DRegOrientation((DNSMcRotation)value);
        }

        private bool CheckRegRelativeToCurrOrientation()
        {
            if (AttachPointID != DNMcConstants._MC_EMPTY_ID && rdbRegPrivate.Checked)
            {
                SaveCurrentAttachPointID();
                if (CheckIsNeedToDoValidation() && AttachPointID != DNMcConstants._MC_EMPTY_ID)
                {
                    MCTPrivatePropertyValidationResult validationResult = MCTPrivatePropertiesData.GetValueOfExistPrivatePropertyNew(RegPropertyID, GetReg3DRegOrientation(), this, new DNSByteBool(0), AttachPointID);
                    SetRegPrivatePropertyValidationResult(validationResult);

                    return (validationResult.ErrorType == MCTPrivatePropertyValidationResult.EErrorType.None);
                }
                return false;
            }
            else
                return true;
        }

        private void chkRegRelativeToCurrOrientation_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckRegRelativeToCurrOrientation())
                e.Cancel = true;
        }

        private void ctrl3DRegOrientation_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckRegRelativeToCurrOrientation())
                e.Cancel = true;
        }
    }

    public class CtrlObjStatePropertyDataHandlerSubPartRotation : CtrlObjStatePropertyDictionaryDataHandler<uint, DNSMcRotation> { }
}
