using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.ObjectWorld;
using MCTester.General_Forms;
using MapCore;
using UnmanagedWrapper;
using System.Windows.Forms;
using MCTester.Managers.MapWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnTextItem
    {
        public btnTextItem()
        {
        }

        public void ExecuteAction()
        {
            IDNMcFont currFont = ObjectPropertiesBase.TextFont;
            if (currFont == null)
            {
                frmFontDialog FontDialogForm = new frmFontDialog(MainForm.FontTextureSourceForm.CreateOnMap);
                if (FontDialogForm.ShowDialog() == DialogResult.OK)
                {
                    currFont = FontDialogForm.CurrFont;
                }
            }
            try
            {
                if (currFont != null)
                {
                    if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                    {
                        IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                        if (activeOverlay != null)
                        {
                            DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
                            IDNMcObjectSchemeItem ObjSchemeItem = DNMcTextItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                        ObjectPropertiesBase.TextCoordSys,
                                                                                        currFont,
                                                                                        ObjectPropertiesBase.TextScale,
                                                                                        ObjectPropertiesBase.NeverUpsideDown,
                                                                                        DNEAxisXAlignment._EXA_CENTER,
                                                                                        DNEBoundingRectanglePoint._EBRP_CENTER,
                                                                                        false,
                                                                                        ObjectPropertiesBase.TextMargin,
                                                                                        ObjectPropertiesBase.TextColor,
                                                                                        ObjectPropertiesBase.TextBackgroundColor);


                            IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                                ObjSchemeItem,
                                                                ObjectPropertiesBase.LocationCoordSys,
                                                                locationPoints,
                                                                ObjectPropertiesBase.LocationRelativeToDtm);

                            ((IDNMcTextItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            DNMcVariantString varString = new DNMcVariantString("", ObjectPropertiesBase.TextIsUnicode);
                            ((IDNMcTextItem)ObjSchemeItem).SetText(varString, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            ((IDNMcTextItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.TextOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            ((IDNMcTextItem)ObjSchemeItem).SetMarginY(ObjectPropertiesBase.TextMarginY, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            ((IDNMcTextItem)ObjSchemeItem).SetBackgroundShape(ObjectPropertiesBase.BackgroundShape, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

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
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating text item", McEx);
            }

        }
    }
}
