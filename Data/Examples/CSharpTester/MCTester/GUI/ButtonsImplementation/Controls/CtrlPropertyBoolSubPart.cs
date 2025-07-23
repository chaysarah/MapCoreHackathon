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
    public partial class CtrlPropertyBoolSubPart : CtrlPropertyBool
    {
        private IDNMcMeshItem m_CurrentMesh;
        private bool m_bParam;
        private uint m_PropId;

        public CtrlPropertyBoolSubPart()
        {
            InitializeComponent();
            
            HideSelectionTab();
        }
                
        public IDNMcMeshItem CurrentMesh
        {
            get { return m_CurrentMesh; }
            set
            {
                m_CurrentMesh = value;
                if (value != null)
                    btnApply.Visible = true;
            }
        }

        public string RegAttachPointLable
        {
            get { return lblRegAttachPoint.Text; }
            set { lblRegAttachPoint.Text = value; }
        }

        public string SelAttachPointLable
        {
            get { return lblSelAttachPoint.Text; }
            set { lblSelAttachPoint.Text = value; }
        }

        public uint RegAttachPointID
        {
            get{ return ntxRegAttachPointID.GetUInt32(); }
            set { ntxRegAttachPointID.SetUInt32(value); }
        }

        public uint SelAttachPointID
        {
            get { return ntxSelAttachPointID.GetUInt32();}
            set { ntxSelAttachPointID.SetUInt32(value); }
        }

        private void ntxRegAttachPointID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (CurrentMesh != null)
                {
					CurrentMesh.GetSubPartInheritsParentRotation(RegAttachPointID,
                                                                    out m_bParam,
                                                                    out m_PropId);

                    RegBoolVal = m_bParam;
                    RegPropertyID = m_PropId;
                }

                GetCtrlGui();                
            }
            catch (MapCoreException McEx)
            {
				MapCore.Common.Utilities.ShowErrorMessage("GetSubPartInheritsParentRotation", McEx);
            }
            
        }

        private void ntxSelAttachPointID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (CurrentMesh == null)
                return;

            try
            {
				CurrentMesh.SetSubPartInheritsParentRotation(RegAttachPointID,
                                                             RegBoolVal,
                                                             RegPropertyID);



                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
				MapCore.Common.Utilities.ShowErrorMessage("SetSubPartInheritsParentRotation", McEx);
            }
        }

    }
}
