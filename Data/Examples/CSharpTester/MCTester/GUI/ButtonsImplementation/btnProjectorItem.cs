using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.ObjectWorld.ObjectsUserControls;
using UnmanagedWrapper;
using MCTester.GUI.Map;
using System.Drawing;
using MapCore;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public class btnProjectorItem
    {
        private DNSMcVector3D[] m_LocationPoints;
        private IDNMcTexture m_ProjectorTexture;
        private IDNMcObjectSchemeItem ObjSchemeItem;

        public btnProjectorItem()
        {
            m_LocationPoints = new DNSMcVector3D[1];
        }

        public void ExecuteAction()
        {
            try
            {
                CreateTextureForm frmCreatedTexture = new CreateTextureForm(MainForm.FontTextureSourceForm.CreateOnMap);

                frmCreatedTexture.TopMost = true;
                frmCreatedTexture.ShowDialog();
                m_ProjectorTexture = frmCreatedTexture.CurrentTexture;
                                
                ObjSchemeItem = DNMcProjectorItem.Create(m_ProjectorTexture,
                                                            ObjectPropertiesBase.ProjectorHalfFOVHorizAngle,
                                                            ObjectPropertiesBase.ProjectorAspectRatio,
                                                            ObjectPropertiesBase.ProjectorItemTargetTypeFlags);

                MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Create default Texture failed", McEx);
            }
        }

        void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            MCTMapForm.OnMapClicked -= new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);

            try
            {
                m_LocationPoints[0] = PointIn3D;

                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {
                    IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        //In case that object location is relative to DTM, z is equal to zero in order to be on the terrain.
                        if (ObjectPropertiesBase.LocationRelativeToDtm == true)
                        {
                            m_LocationPoints[0].z = 0;
                        }

                        IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                            ObjSchemeItem,
                                                            ObjectPropertiesBase.LocationCoordSys,
                                                            m_LocationPoints,
                                                            ObjectPropertiesBase.LocationRelativeToDtm);

                        if (ObjectPropertiesBase.ImageCalc != null)
                            obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                        // turn on all viewports render needed flags
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeOverlay.GetOverlayManager());
                    }
                    else
                        MessageBox.Show("There is no active overlay");
                }
                else
                    MessageBox.Show("There is no active overlay manager");
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
            }
        }
    }
}
