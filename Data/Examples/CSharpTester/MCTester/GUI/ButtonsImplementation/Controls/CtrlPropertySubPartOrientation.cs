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
    public partial class CtrlPropertySubPartOrientation : CtrlPropertyOrientation
    {
        private IDNMcMeshItem m_CurrentMesh;
        
        public IDNMcMeshItem CurrentMesh
        {
            get { return m_CurrentMesh; }
            set
            {
                m_CurrentMesh = value;
                if(m_CurrentMesh != null)
                {
                    btnApply.Visible = true;
                }
            }
        }

        public CtrlPropertySubPartOrientation()
        {
            InitializeComponent();
            RegPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID;
            HideSelectionTab();
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

        public string RegLableAttachPointID
        {
            get {return lblRegAttachPointID.Text;}
            set {lblRegAttachPointID.Text = value;}
        }

        public string SelLableAttachPointID
        {
            get { return lblSelAttachPointID.Text; }
            set {lblSelAttachPointID.Text = value;}
        }

        protected override void btnApply_Click(object sender, EventArgs e)
        {
            if (CurrentMesh == null)
                return;

            try
            {
                CurrentMesh.SetSubPartRotation(RegAttachPointID,
                                               GetRegRotationVal(),
                                               RegPropertyID);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetSubPartRotation", McEx);
            }
        }

        private void btnApply_Click_1(object sender, EventArgs e)
        {

        }
    }
}
