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
    public class btnArcItem
    {
        public btnArcItem()
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

                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcArcItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                    ObjectPropertiesBase.ArcCoordSys,
                                                                                    ObjectPropertiesBase.ArcEllipseType,
                                                                                    1,
                                                                                    1,
                                                                                    ObjectPropertiesBase.ArcStartAngle,
                                                                                    ObjectPropertiesBase.ArcEndAngle,
                                                                                    ObjectPropertiesBase.LineStyle,
                                                                                    ObjectPropertiesBase.LineColor,
                                                                                    ObjectPropertiesBase.LineWidth,
                                                                                    ObjectPropertiesBase.LineTexture,
                                                                                    ObjectPropertiesBase.LineTextureHeightRange,
                                                                                    1f);


                        IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                            ObjSchemeItem,
                                                            ObjectPropertiesBase.LocationCoordSys,
                                                            locationPoints,
                                                            ObjectPropertiesBase.LocationRelativeToDtm);

                        ((IDNMcArcItem)ObjSchemeItem).SetEllipseDefinition(ObjectPropertiesBase.ArcEllipseDefinition);
                        ((IDNMcArcItem)ObjSchemeItem).SetNumSmoothingLevels(ObjectPropertiesBase.LineBasedSmoothingLevels, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
                        ((IDNMcArcItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.LineOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetOutlineWidth(ObjectPropertiesBase.LineOutlineWidth, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetShapeType(ObjectPropertiesBase.ShapeType);
                        ((IDNMcArcItem)ObjSchemeItem).SetVerticalHeight(ObjectPropertiesBase.VerticalHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetSidesFillStyle(ObjectPropertiesBase.SidesFillStyle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetSidesFillColor(ObjectPropertiesBase.SidesFillColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.SidesFillTexture, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArcItem)ObjSchemeItem).SetPointOrderReverseMode(ObjectPropertiesBase.PointOrderReverseMode, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

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
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating arc item", McEx);
            }
        }
    }
}
