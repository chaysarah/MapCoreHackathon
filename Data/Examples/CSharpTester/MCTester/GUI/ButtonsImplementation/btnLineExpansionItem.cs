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
    public class btnLineExpansionItem
    {
        public btnLineExpansionItem()
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

                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineExpansionItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                            ObjectPropertiesBase.LineExpansionCoordinateSystem,
                                                                                            ObjectPropertiesBase.LineExpansionType,
                                                                                            ObjectPropertiesBase.LineExpansionRadius,
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

                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetNumSmoothingLevels(ObjectPropertiesBase.LineBasedSmoothingLevels, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetShapeType(ObjectPropertiesBase.ShapeType);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetVerticalHeight(ObjectPropertiesBase.VerticalHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetSidesFillStyle(ObjectPropertiesBase.SidesFillStyle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetSidesFillColor(ObjectPropertiesBase.SidesFillColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.SidesFillTexture, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.LineOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetOutlineWidth(ObjectPropertiesBase.LineOutlineWidth, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcLineExpansionItem)ObjSchemeItem).SetPointOrderReverseMode(ObjectPropertiesBase.PointOrderReverseMode, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        if (ObjectPropertiesBase.ImageCalc != null)
                            obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                        MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem, EditModePropertiesBase.EnableDistanceDirectionMeasureForMultiPointItem );

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
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating line Expansion item", McEx);
            }
        }
    }
}
