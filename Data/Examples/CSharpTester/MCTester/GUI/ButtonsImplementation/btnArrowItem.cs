using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public class btnArrowItem
    {
        public btnArrowItem()
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

                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcArrowItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                    ObjectPropertiesBase.ArrowCoordSys,
                                                                                    ObjectPropertiesBase.ArrowHeadSize,
                                                                                    ObjectPropertiesBase.ArrowHeadAngle,
                                                                                    ObjectPropertiesBase.ArrowGapSize,
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

                        ((IDNMcArrowItem)ObjSchemeItem).SetNumSmoothingLevels(ObjectPropertiesBase.LineBasedSmoothingLevels, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
                        ((IDNMcArrowItem)ObjSchemeItem).SetShapeType(ObjectPropertiesBase.ShapeType);
                        ((IDNMcArrowItem)ObjSchemeItem).SetVerticalHeight(ObjectPropertiesBase.VerticalHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetSidesFillStyle(ObjectPropertiesBase.SidesFillStyle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetSidesFillColor(ObjectPropertiesBase.SidesFillColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.SidesFillTexture, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.LineOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetOutlineWidth(ObjectPropertiesBase.LineOutlineWidth, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcArrowItem)ObjSchemeItem).SetPointOrderReverseMode(ObjectPropertiesBase.PointOrderReverseMode, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);


                        if (ObjectPropertiesBase.ImageCalc != null)
                            obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                        MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem, EditModePropertiesBase.EnableDistanceDirectionMeasureForMultiPointItem);

                        // turn on all viewports render needed flags
                        MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeOverlay.GetOverlayManager());
                    }
                    else
                        MessageBox.Show("There is no active overlay");
                }
                else
                    MessageBox.Show("There is no active overlay manager");
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating arrow item", McEx);
            }
        }
    }
}
