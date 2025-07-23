using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;
using MCTester.ObjectWorld.ObjectsUserControls;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using System.Drawing;

namespace MCTester.ButtonsImplementation
{
    public class btnPictureItem
    {
        public btnPictureItem()
        {
        }

        public void ExecuteAction()
        {
            try
            {
                IDNMcTexture m_PicTexture = ObjectPropertiesBase.PictureTexture;
                if (m_PicTexture == null)
                {
                    CreateTextureForm frmCreatedTexture = new CreateTextureForm(MainForm.FontTextureSourceForm.CreateOnMap);
                    if (frmCreatedTexture.ShowDialog() == DialogResult.OK)
                    {
                        m_PicTexture = frmCreatedTexture.CurrentTexture;
                    }
                    else
                        return;
                }

                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {
                    IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcPictureItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                     ObjectPropertiesBase.PictureCoordSys,
                                                                                     m_PicTexture,
                                                                                     ObjectPropertiesBase.PicWidth,
                                                                                     ObjectPropertiesBase.PicHeight,
                                                                                     ObjectPropertiesBase.PictureIsSizeFactor,
                                                                                     new DNSMcBColor(255, 255, 255, 255),
                                                                                     DNEBoundingRectanglePoint._EBRP_CENTER,
                                                                                     ObjectPropertiesBase.PictureIsUseTextureGeoReferencing);

                        IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                            ObjSchemeItem,
                                                            ObjectPropertiesBase.LocationCoordSys,
                                                            locationPoints,
                                                            ObjectPropertiesBase.LocationRelativeToDtm);

                        ((IDNMcPictureItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPictureItem)ObjSchemeItem).SetNeverUpsideDown(ObjectPropertiesBase.PictureNeverUpsideDown, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        if (ObjectPropertiesBase.ImageCalc != null)
                            obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                        if (ObjectPropertiesBase.PictureIsUseTextureGeoReferencing == false)   // if PictureIsUseTextureGeoReferencing == true not need to point location on the map
                            MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem);

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
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating picture item", McEx);
            }
        }

        void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector2D PointInImage)
        {
            DNSMcVector3D PicPoint3D = PointIn3D;
            Point PicPoint2D = PointOnMap;
        }

    }
}
