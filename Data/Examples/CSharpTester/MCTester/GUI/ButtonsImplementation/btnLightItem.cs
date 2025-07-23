using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using MCTester.Managers.ObjectWorld;
using UnmanagedWrapper;
using System.Windows.Forms;
using MCTester.GUI.Map;
using System.Drawing;
using MCTester.Managers.MapWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnLightItem
    {
        private IDNMcOverlay activeOverlay;
        private DNSMcVector3D[] locationPoints;
        private string m_LightType;
        private IDNMcObjectSchemeItem ObjSchemeItem;

        public btnLightItem(string lightType)
        {
            m_LightType = lightType;
        }

        public void ExecuteAction()
        {

            activeOverlay = Manager_MCOverlayManager.ActiveOverlay;
            locationPoints = new DNSMcVector3D[1];
            if (Manager_MCOverlayManager.ActiveOverlayManager != null)
            {
                if (activeOverlay != null)
                {
                    switch (m_LightType)
                    {
                        case "Directional Light":
                            try
                            {

                                ObjSchemeItem = DNMcDirectionalLightItem.Create(ObjectPropertiesBase.LightDiffuseColor,
                                                                                    ObjectPropertiesBase.LightSpecularColor,
                                                                                    ObjectPropertiesBase.LightDirection);


                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating Directional Light item", McEx);
                            }
                            break;
                        case "Point Light":
                            try
                            {

                                ObjSchemeItem = DNMcPointLightItem.Create(ObjectPropertiesBase.LightDiffuseColor,
                                                                            ObjectPropertiesBase.LightSpecularColor,
                                                                            ObjectPropertiesBase.LightAttenuation);


                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating Point Light item", McEx);
                            }
                            break;
                        case "Spot Light":
                            try
                            {

                                ObjSchemeItem = DNMcSpotLightItem.Create(ObjectPropertiesBase.LightDiffuseColor,
                                                                            ObjectPropertiesBase.LightSpecularColor,
                                                                            ObjectPropertiesBase.LightAttenuation,
                                                                            ObjectPropertiesBase.LightDirection,
                                                                            ObjectPropertiesBase.LightHalfOuterAngle,
                                                                            ObjectPropertiesBase.LightHalfInnerAngle);


                            }
                            catch (MapCoreException McEx)
                            {
                                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating Spot Light item", McEx);
                            }
                            break;
                    }
                }
                else
                    MessageBox.Show("There is no active overlay");
            }
            else
                MessageBox.Show("There is no active overlay manager");

            MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
        }

        void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            MCTMapForm.OnMapClicked -= new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
            
            try
            {
                locationPoints[0] = PointIn3D;

                //In case that object location is relative to DTM, z is equal to zero in order to be on the terrain.
                if (ObjectPropertiesBase.LocationRelativeToDtm == true)
                {
                    locationPoints[0].z = 0;
                }

                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    ObjectPropertiesBase.LocationCoordSys,
                                                    locationPoints,
                                                    ObjectPropertiesBase.LocationRelativeToDtm);

                if (ObjectPropertiesBase.ImageCalc != null)
                    obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                // turn on all viewports render needed flags
                Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeOverlay.GetOverlayManager());

                MessageBox.Show("Light created successfully", "Light creation", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
            }
        }
    }
}
