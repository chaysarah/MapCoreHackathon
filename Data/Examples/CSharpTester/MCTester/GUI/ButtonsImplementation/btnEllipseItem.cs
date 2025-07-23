using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using System.Windows.Forms;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnEllipseItem
    {
        public btnEllipseItem()
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


                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcEllipseItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                        ObjectPropertiesBase.EllipseCoordSys,
                                                                                        ObjectPropertiesBase.EllipseType,
                                                                                        1,
                                                                                        1,
                                                                                        ObjectPropertiesBase.EllipseStartAngle,
                                                                                        ObjectPropertiesBase.EllipseEndAngle,
                                                                                        ObjectPropertiesBase.EllipseInnerRadiusFactor,
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


                        ((IDNMcEllipseItem)ObjSchemeItem).SetEllipseDefinition(ObjectPropertiesBase.EllipseDefinition);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetNumSmoothingLevels(ObjectPropertiesBase.LineBasedSmoothingLevels, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetShapeType(ObjectPropertiesBase.ShapeType);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetVerticalHeight(ObjectPropertiesBase.VerticalHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetSidesFillStyle(ObjectPropertiesBase.SidesFillStyle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetSidesFillColor(ObjectPropertiesBase.SidesFillColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.SidesFillTexture, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.LineOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetOutlineWidth(ObjectPropertiesBase.LineOutlineWidth, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcEllipseItem)ObjSchemeItem).SetPointOrderReverseMode(ObjectPropertiesBase.PointOrderReverseMode, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        if (ObjectPropertiesBase.ImageCalc != null)
                            obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

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
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating ellipse item", McEx);
            }
        }
    }
}
