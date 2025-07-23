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

namespace MCTester.Controls
{
    public partial class CtrlPropertyFVector2DTextureScroll : CtrlPropertyFVect2D
    {
        private IDNMcMeshItem m_currentMesh;
        private uint m_PropId;
        private DNSMcFVector2D m_FVecotr2D;

        public CtrlPropertyFVector2DTextureScroll()
        {
            InitializeComponent();
            RegPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID;
        }

        public IDNMcMeshItem CurrentMesh
        {
            get { return m_currentMesh; }
            set
            {
                m_currentMesh = value;
                if (value != null)
                    btnRegApply.Visible = true;
            }
        }

        public string RegMeshTextureIDLable
        {
            get { return lblRegMeshTextureID.Text; }
            set { lblRegMeshTextureID.Text = value; }
        }

        public string SelMeshTextureIDLable
        {
            get { return lblSelMeshTextureID.Text; }
            set { lblSelMeshTextureID.Text = value; }
        }

        public uint RegMeshTextureID
        {
            get { return ntxRegMeshTextureID.GetUInt32(); }
            set { ntxRegMeshTextureID.SetUInt32(value); }
        }

        public uint SelMeshTextureID
        {
            get { return ntxSelMeshTextureID.GetUInt32(); }
            set { ntxRegMeshTextureID.SetUInt32(value); }
        }

        private void btnRegApply_Click(object sender, EventArgs e)
        {
            if (CurrentMesh == null)
                return;

            try
            {
                if (RegMeshTextureID != 0)
                {
                    CurrentMesh.SetTextureScrollSpeed(RegMeshTextureID,
                                                        RegFVector2DVal,
                                                        RegPropertyID,
                                                        false);
                }

                if (!chxSelectionProperty.Checked)
                {
                    if (SelMeshTextureID != 0)
                    {
                        SelPropertyID = (uint)DNEPredefinedPropertyIDs._EPPI_NO_MORE_STATE_PROPERTIES_ID;

                        CurrentMesh.SetTextureScrollSpeed(SelMeshTextureID,
                                                             SelFVector2DVal,
                                                             SelPropertyID,
                                                             true);
                    }
                }

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTextureScrollSpeed", McEx);
            }
        }

        private void btnRegGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentMesh == null)
                    return;

                if (RegMeshTextureID != 0)
                {
                    CurrentMesh.GetTextureScrollSpeed(RegMeshTextureID,
                                                        out m_FVecotr2D,
                                                        out m_PropId,
                                                        false);

                    RegFVector2DVal = m_FVecotr2D;
                    RegPropertyID = m_PropId;
                }

                SetCtrlRegRadioButtonState();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTextureScrollSpeed", McEx);
            }
        }

        private void btnSelApply_Click(object sender, EventArgs e)
        {
            if (CurrentMesh == null)
                return;

            try
            {
                if (chxSelectionProperty.Checked)
                {
                    if (SelMeshTextureID != 0)
                    {
                        CurrentMesh.SetTextureScrollSpeed(SelMeshTextureID,
                                                             SelFVector2DVal,
                                                             SelPropertyID,
                                                             true);
                    }
                }

                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTextureScrollSpeed", McEx);
            }
        }

        private void btnSelGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentMesh == null)
                    return;

                if (SelMeshTextureID != 0)
                {
                    CurrentMesh.GetTextureScrollSpeed(SelMeshTextureID,
                                                        out m_FVecotr2D,
                                                        out m_PropId,
                                                        true);

                    SelFVector2DVal = m_FVecotr2D;
                    SelPropertyID = m_PropId;
                }

                SetSelectionPropertyCheckBox();
                SetCtrlSelRadioButtonState();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTextureScrollSpeed", McEx);
            }
        }
    }
}
