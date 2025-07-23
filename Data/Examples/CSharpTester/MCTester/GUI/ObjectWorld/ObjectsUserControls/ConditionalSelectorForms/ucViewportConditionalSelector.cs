using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.ConditionalSelectorForms
{
    public partial class ucViewportConditionalSelector : ucBaseConditionalSelector, IUserControlItem
    {
        private IDNMcViewportConditionalSelector m_CurrentCondSelector;
        private uint[] viewportIDs;

        public ucViewportConditionalSelector()
        {
            InitializeComponent();
            viewportIDs = new uint[0];

            //Fill clstViewportType 
            DNEViewportType[] allVisibilties = (DNEViewportType[])Enum.GetValues(typeof(DNEViewportType));
            foreach (DNEViewportType currVisibility in allVisibilties)
            {
                if (currVisibility != DNEViewportType._EVT_NONE)
                    clstViewportType.Items.Add(currVisibility);
            }

            DNEViewportCoordinateSystem[] allCoordSystem = (DNEViewportCoordinateSystem[])Enum.GetValues(typeof(DNEViewportCoordinateSystem));
            foreach (DNEViewportCoordinateSystem currCoordSystem in allCoordSystem)
            {
                if (currCoordSystem != DNEViewportCoordinateSystem._EVCS_ALL_COORDINATE_SYSTEMS)
                    clstViewportCoordSys.Items.Add(currCoordSystem);
            }
        }

        #region IUserControlItem Members

        public new void LoadItem(object aItem)
        {
            m_CurrentCondSelector = (IDNMcViewportConditionalSelector)aItem;
            base.LoadItem(aItem);

            if (m_CurrentCondSelector != null)
            {
                try
                {
                    DNEViewportType viewportType = m_CurrentCondSelector.ViewportTypeBitField;
                    DNEViewportType currType = DNEViewportType._EVT_NONE;

                    if ((viewportType & DNEViewportType._EVT_2D_IMAGE_VIEWPORT) == DNEViewportType._EVT_2D_IMAGE_VIEWPORT)
                    {
                        currType = DNEViewportType._EVT_2D_IMAGE_VIEWPORT;
                        MarkViewportTypeCheckBox(currType);
                    }
                    if ((viewportType & DNEViewportType._EVT_2D_SECTION_VIEWPORT) == DNEViewportType._EVT_2D_SECTION_VIEWPORT)
                    {
                        currType = DNEViewportType._EVT_2D_SECTION_VIEWPORT;
                        MarkViewportTypeCheckBox(currType);
                    }
                    if ((viewportType & DNEViewportType._EVT_2D_REGULAR_VIEWPORT) == DNEViewportType._EVT_2D_REGULAR_VIEWPORT)
                    {
                        currType = DNEViewportType._EVT_2D_REGULAR_VIEWPORT;
                        MarkViewportTypeCheckBox(currType);
                    }
                    if ((viewportType & DNEViewportType._EVT_2D_VIEWPORT) == DNEViewportType._EVT_2D_VIEWPORT)
                    {
                        currType = DNEViewportType._EVT_2D_VIEWPORT;
                        MarkViewportTypeCheckBox(currType);
                    }
                    if ((viewportType & DNEViewportType._EVT_3D_VIEWPORT) == DNEViewportType._EVT_3D_VIEWPORT)
                    {
                        currType = DNEViewportType._EVT_3D_VIEWPORT;
                        MarkViewportTypeCheckBox(currType);
                    }
                    if ((viewportType & DNEViewportType._EVT_ALL_VIEWPORTS) == DNEViewportType._EVT_ALL_VIEWPORTS)
                    {
                        currType = DNEViewportType._EVT_ALL_VIEWPORTS;
                        MarkViewportTypeCheckBox(currType);
                    }
                    if ((viewportType & DNEViewportType._EVT_NONE) == DNEViewportType._EVT_NONE)
                    {
                        currType = DNEViewportType._EVT_NONE;
                        MarkViewportTypeCheckBox(currType);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ViewportTypeBitField", McEx);
                }

                try
                {
                    DNEViewportCoordinateSystem viewportCoordSys = m_CurrentCondSelector.ViewportCoordinateSystemBitField;
                    DNEViewportCoordinateSystem currCoordSys;

                    if ((viewportCoordSys & DNEViewportCoordinateSystem._EVCS_ALL_COORDINATE_SYSTEMS) == DNEViewportCoordinateSystem._EVCS_ALL_COORDINATE_SYSTEMS)
                    {
                        currCoordSys = DNEViewportCoordinateSystem._EVCS_ALL_COORDINATE_SYSTEMS;
                        MarkViewportCoordSysCheckBox(currCoordSys);
                    }
                    if ((viewportCoordSys & DNEViewportCoordinateSystem._EVCS_GEO_COORDINATE_SYSTEM) == DNEViewportCoordinateSystem._EVCS_GEO_COORDINATE_SYSTEM)
                    {
                        currCoordSys = DNEViewportCoordinateSystem._EVCS_GEO_COORDINATE_SYSTEM;
                        MarkViewportCoordSysCheckBox(currCoordSys);
                    }
                    if ((viewportCoordSys & DNEViewportCoordinateSystem._EVCS_UTM_COORDINATE_SYSTEM) == DNEViewportCoordinateSystem._EVCS_UTM_COORDINATE_SYSTEM)
                    {
                        currCoordSys = DNEViewportCoordinateSystem._EVCS_UTM_COORDINATE_SYSTEM;
                        MarkViewportCoordSysCheckBox(currCoordSys);
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("", McEx);
                }

                try
                {
                    bool inclusive;
                    uint[] viewportsIDs = m_CurrentCondSelector.GetSpecificViewports(out inclusive);

                    chxIDsInclusive.Checked = inclusive;
                    for (int i = 0; i < viewportsIDs.Length; i++)
                    {
                        txtViewportIDs.Text += viewportsIDs[i].ToString() + " ";
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetSpecificViewports", McEx);
                }
            }
        }

        #endregion

        protected override void Save()
        {
            base.Save();

            DNEViewportType bitField = 0;
            DNEViewportCoordinateSystem bitFieldCoordSys = 0;
            for (int i = 0; i < clstViewportType.CheckedItems.Count; i++)
            {
                DNEViewportType viewportType = (DNEViewportType)Enum.Parse(typeof(DNEViewportType), clstViewportType.CheckedItems[i].ToString());
                bitField |= viewportType;
            }

            for (int i = 0; i < clstViewportCoordSys.CheckedItems.Count; i++)
            {
                DNEViewportCoordinateSystem viewportCoorSys = (DNEViewportCoordinateSystem)Enum.Parse(typeof(DNEViewportCoordinateSystem), clstViewportCoordSys.CheckedItems[i].ToString());
                bitFieldCoordSys |= viewportCoorSys;
            }

            if (txtViewportIDs.Text != "")
            {
                string[] IDs = txtViewportIDs.Text.Trim().Split(' ');

                if (IDs.Length > 0)
                {
                    viewportIDs = new uint[IDs.Length];
                    int i = 0;

                    foreach (string strID in IDs)
                    {
                        viewportIDs[i] = Convert.ToUInt32(strID);
                        ++i;
                    }
                }
            }
            

            if (m_CurrentCondSelector == null)
            {
                //try
                //{
                //    m_CurrentCondSelector = DNMcViewportConditionalSelector.Create(null,
                //                                                                        bitField,
                //                                                                        bitFieldCoordSys,
                //                                                                        viewportIDs,
                //                                                                        chxIDsInclusive.Checked);


                //}
                //catch (MapCoreException McEx)
                //{
                //    MapCore.Common.Utilities.ShowErrorMessage("DNMcViewportConditionalSelector.Create", McEx);
                //}
            }
            else
            {
                try
                {
                    m_CurrentCondSelector.ViewportTypeBitField = bitField;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ViewportTypeBitField", McEx);
                }

                try
                {
                    m_CurrentCondSelector.ViewportCoordinateSystemBitField = bitFieldCoordSys;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ViewportCoordinateSystemBitField", McEx);
                }

                try
                {
                    m_CurrentCondSelector.SetSpecificViewports(viewportIDs,
                                                            chxIDsInclusive.Checked);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("SetSpecificViewports", McEx);
                }
            }

            MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags();
        }

        private void MarkViewportTypeCheckBox(DNEViewportType currType)
        {
            for (int i = 0; i < clstViewportType.Items.Count; i++)
            {
                if (currType == (DNEViewportType)clstViewportType.Items[i])
                {
                    clstViewportType.SetItemChecked(i, true);
                }
            }
        }

        private void MarkViewportCoordSysCheckBox(DNEViewportCoordinateSystem VPCoordSys)
        {
            for (int i = 0; i < clstViewportCoordSys.Items.Count; i++)
            {
                if (VPCoordSys == (DNEViewportCoordinateSystem)clstViewportCoordSys.Items[i])
                {
                    clstViewportCoordSys.SetItemChecked(i, true);
                }
            }
        }

        public IDNMcViewportConditionalSelector ViewportCondSelector
        {
            get { return m_CurrentCondSelector; }
            set { m_CurrentCondSelector = value; }
        }
    }
}
