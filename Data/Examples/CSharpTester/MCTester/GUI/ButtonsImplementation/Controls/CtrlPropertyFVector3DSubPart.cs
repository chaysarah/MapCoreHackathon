using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlPropertyFVector3DSubPart : CtrlObjStatePropertyFVect3D
    {
        private IDNMcMeshItem m_currentMesh;
        private uint m_PropId;
        private DNSMcFVector3D m_FVecotr3D;

        private Dictionary<byte,uint> m_attachPoints = new Dictionary<byte,uint>();

        public IDNMcMeshItem CurrentMesh
        {
            get { return m_currentMesh; }
            set 
            { 
                m_currentMesh = value;
                //if (value != null)
                    //btnApply.Visible = true;
            }
        }
	
        public CtrlPropertyFVector3DSubPart()
        {
            InitializeComponent();
            RegPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID;
        }

        public string RegAttachPointLable
        {
            get { return lblRegAttachPoint.Text; }
            set 
            { 
                lblRegAttachPoint.Text = value;
                lblSelAttachPoint.Text = value;
            }
        }

        public uint RegAttachPointID
        {
            get { return ntxRegAttachPointID.GetUInt32(); }
            set { ntxRegAttachPointID.SetUInt32(value); }
        }

        public uint SelAttachPointID
        {
            get { return ntxSelAttachPointID.GetUInt32(); }
            set { ntxSelAttachPointID.SetUInt32(value); }
        }

        public uint GetAttachPointID(byte objectState)
        {
            if (m_attachPoints.ContainsKey(objectState))
            {
                return m_attachPoints[objectState];
            }
            else
            {
                return 0;
            }
        }

        public void SetAttachPointID(byte objectState, uint value)
        {
            m_attachPoints[objectState] = value;
            if (objectState == ObjectState.AsByte)
            {
                SelAttachPointID = value;
            }
        }

        public uint AttachPointID
        {
            get
            {
                NumericTextBox ctrl = (NumericTextBox)tcProperty.SelectedTab.Controls.Find("ntxRegAttachPointID",true)[0];
                return ctrl.GetUInt32();
            }
            set
            {
                NumericTextBox ctrl = (NumericTextBox)tcProperty.SelectedTab.Controls.Find("ntxRegAttachPointID", true)[0];
                ctrl.SetUInt32(value);
            }
        }


        private void btnRegApply_Click(object sender, EventArgs e)
        {
            if (CurrentMesh == null)
                return;

            try
            {
                if (RegAttachPointID != 0)
                {
                    CurrentMesh.SetSubPartOffset(RegAttachPointID,
                                                GetRegFVector3DVal(),
                                                RegPropertyID,
                                                false);
                }
                else
                {
                    uint propId = (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID;
                    CurrentMesh.SetSubPartOffset(RegAttachPointID, GetRegFVector3DVal(), propId, false);
                }

                DeselectTab();
                byte[] objectStates = ObjectStates;
                byte nLastState = 1;
                foreach (byte objectState in objectStates)
                {
                    while (nLastState < objectState)
                    {
                        CurrentMesh.SetSubPartOffset(1,DNSMcFVector3D.v3Zero, (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID, nLastState);
                        nLastState++;
                    }
                    CurrentMesh.SetSubPartOffset(
                        GetAttachPointID(objectState), 
                        GetSelFVector3DVal(objectState), 
                        GetSelPropertyId(objectState), 
                        objectState);
                }
                if (nLastState < byte.MaxValue)
                {
                    nLastState++;
                    CurrentMesh.SetSubPartOffset(1,DNSMcFVector3D.v3Zero, (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID, nLastState);
                }

                SelectFirstTab();

            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSubPartOffset", McEx);
            }
        }


        private void btnRegGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentMesh == null)
                    return;

                if (RegAttachPointID != 0)
                {
                    CurrentMesh.GetSubPartOffset(RegAttachPointID,
                                                        out m_FVecotr3D,
                                                        out m_PropId,
                                                        false);

                    SetRegFVector3DVal(m_FVecotr3D);
                    RegPropertyID = m_PropId;
                }

                byte objectState = 1;
                uint propId = (uint)DNEPredefinedPropertyIDs._EPPI_FIRST_RESERVED_ID;
                DNSMcFVector3D vect;
                uint attachPointId;

                while (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID)
                {
                    attachPointId = GetAttachPointID(objectState);
                    CurrentMesh.GetSubPartOffset(attachPointId, out vect, out propId, objectState);
                    if (propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID &&
                        propId != (uint)DNEPredefinedPropertyIDs._EPPI_NO_STATE_PROPERTY_ID)
                    {
                        AddObjectState(objectState, propId);
                        SetSelPropertyId(objectState, propId);
                        SetSelFVector3DVal(objectState, vect);
                        SetAttachPointID(objectState, attachPointId);
                    }

                    if (objectState == byte.MaxValue)
                    {
                        break;
                    }
                    objectState++;
                }

                SelectFirstTab();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetSubPartOffset", McEx);
            }
        }

        private void CtrlPropertyFVector3DSubPart_OnObjectStateAdd(byte objectState, bool bInitCtrl)
        {
            SetAttachPointID(objectState,0);
            if (bInitCtrl)
            {
                if (objectState == ObjectState.AsByte)
                {
                    SelAttachPointID = objectState;
                }
                else
                {
                    ntxSelAttachPointID.SetUInt32(objectState);
                }
            }

        }

        private void CtrlPropertyFVector3DSubPart_OnObjectStateChanged(byte objectState, byte previousState)
        {
            if (previousState > 0)
            {
                SetAttachPointID(previousState, ntxSelAttachPointID.GetUInt32());
            }

            if (objectState > 0)
            {
                SelAttachPointID = GetAttachPointID(objectState);
            }
        }

        private void CtrlPropertyFVector3DSubPart_OnObjectStateSave(byte objectState)
        {
            SetAttachPointID(objectState, ntxSelAttachPointID.GetUInt32());
        }
    }

}
