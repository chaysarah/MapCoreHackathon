using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.ObjectWorld;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public class btnPolygonItem
    {
        public btnPolygonItem()
        {
        }

        public void ExecuteAction()
        {
            try
            {
                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {
                    IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];


                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcPolygonItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                        ObjectPropertiesBase.LineStyle,
                                                                                        ObjectPropertiesBase.LineColor,
                                                                                        ObjectPropertiesBase.LineWidth,
                                                                                        ObjectPropertiesBase.LineTexture,
                                                                                        ObjectPropertiesBase.LineTextureHeightRange,
                                                                                        1f,
                                                                                        ObjectPropertiesBase.FillStyle,
                                                                                        ObjectPropertiesBase.FillColor,
                                                                                        ObjectPropertiesBase.FillTexture,
                                                                                        new DNSMcFVector2D(1, 1));

                        IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                            ObjSchemeItem,
                                                            ObjectPropertiesBase.LocationCoordSys,
                                                            locationPoints,
                                                            ObjectPropertiesBase.LocationRelativeToDtm);

                        ((IDNMcPolygonItem)ObjSchemeItem).SetNumSmoothingLevels(ObjectPropertiesBase.LineBasedSmoothingLevels, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetShapeType(ObjectPropertiesBase.ShapeType);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetVerticalHeight(ObjectPropertiesBase.VerticalHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetSidesFillStyle(ObjectPropertiesBase.SidesFillStyle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetSidesFillColor(ObjectPropertiesBase.SidesFillColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.SidesFillTexture, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.LineOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetOutlineWidth(ObjectPropertiesBase.LineOutlineWidth, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcPolygonItem)ObjSchemeItem).SetPointOrderReverseMode(ObjectPropertiesBase.PointOrderReverseMode, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        if (ObjectPropertiesBase.ImageCalc != null)
                            obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                        MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem, EditModePropertiesBase.EnableDistanceDirectionMeasureForMultiPointItem);

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
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating polygon item", McEx);
            }
        }
    }
}
