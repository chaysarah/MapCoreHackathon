using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;
using MCTester.GUI.Forms;
using MCTester.GUI.Trees;

namespace MCTester.General_Forms
{
    public partial class ScanItemsFoundFormDetailsObjects : Form
    {
        DNSTargetFound m_ItemFound;
        IDNMcObject m_CurrentObject = null;
        private uint m_PropId;
        private bool m_bParam;
        private uint mSelectedLocationIndex;

        public ScanItemsFoundFormDetailsObjects()
        {
            InitializeComponent();
            ctrlObjectLocations.ChangeReadOnly();
        }

        public ScanItemsFoundFormDetailsObjects(DNSTargetFound itemFound, ScanTargetFound scanTargetFound) : this()
        {
            m_ItemFound = itemFound;
            if (scanTargetFound != null)
            {
                tbItemId.Text = scanTargetFound.ItemID;
                tbObjectId.Text = scanTargetFound.ObjectID;
            }

            if (itemFound.eTargetType == DNEIntersectionTargetType._EITT_OVERLAY_MANAGER_OBJECT)
            {
                label4.Text = "Object Locations";
                m_CurrentObject = itemFound.ObjectItemData.pObject;
                try
                {
                    uint numLocations = m_CurrentObject.GetNumLocations();
                    if (numLocations > 0)
                    {
                        for (int i = 0; i < numLocations; i++)
                            cbLocationIndex.Items.Add(i);
                        cbLocationIndex.SelectedIndex = 0;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetNumLocations", McEx);
                }
            }
            
        }

        private void cbLocationIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocationIndex.SelectedIndex >= 0)
            {
                mSelectedLocationIndex = (uint)cbLocationIndex.SelectedIndex;
                IDNMcObjectLocation mcObjectLocation = null;

                try
                {
                    mcObjectLocation = m_CurrentObject.GetScheme().GetObjectLocation(mSelectedLocationIndex);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetObjectLocation", McEx);
                }

                if (mcObjectLocation != null)
                {
                    try
                    {
                        mcObjectLocation.GetRelativeToDTM(out m_bParam, out m_PropId);

                        if (m_PropId == DNMcConstants._MC_EMPTY_ID) // shared
                            chxIsRelativeToDTM.Checked = m_bParam;
                        else
                            chxIsRelativeToDTM.Checked = m_CurrentObject.GetBoolProperty(m_PropId);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetRelativeToDTM", McEx);
                    }

                    try
                    {
                        DNEMcPointCoordSystem mcPointCoordSystem;
                        mcObjectLocation.GetCoordSystem(out mcPointCoordSystem);
                        txtGeometryCoordinateSystem.Text = mcPointCoordSystem.ToString();
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetCoordSystem", McEx);
                    }
                }
                try
                {
                    DNSMcVector3D[] locations = m_CurrentObject.GetLocationPoints(mSelectedLocationIndex);
                    if (locations != null)
                    {
                        ctrlObjectLocations.SetPoints(locations);
                        ntxNumLocationPoints.SetInt(locations.Length);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetLocationPoints", McEx);
                }
            }
        }
    }
}
