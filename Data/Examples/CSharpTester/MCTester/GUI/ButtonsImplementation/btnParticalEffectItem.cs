using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MCTester.Managers.ObjectWorld;
using MCTester.GUI.Forms;
using MCTester.GUI.Map;
using UnmanagedWrapper;
using System.Windows.Forms;
using MapCore;
using System.Drawing;
using MCTester.Managers.MapWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnParticalEffectItem
    {
        private string m_EffectName;
        
        public btnParticalEffectItem()
        {
        }

        public void ExecuteAction()
        {
            OpenParticalEffectNameDialogForm OpenParticalEffectNameDialog = new OpenParticalEffectNameDialogForm();

            if (OpenParticalEffectNameDialog.ShowDialog() == DialogResult.OK)
            {
                m_EffectName = OpenParticalEffectNameDialog.EffectName;
                MCTMapForm.OnMapClicked +=new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
            }
        }

        void MCTMapForm_OnMapClicked(Point PointOnMap, DNSMcVector3D PointIn3D, DNSMcVector3D PointInImage, bool IsHasIntersection)
        {
            MCTester.GUI.Map.MCTMapForm.OnMapClicked -= new MCTester.GUI.Map.ClickOnMapEventArgs(MCTMapForm_OnMapClicked);

            try
            {
                DNSMcVector3D[] m_LocationPoints = new DNSMcVector3D[1];
                m_LocationPoints[0] = PointIn3D;

                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {
                    IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcParticleEffectItem.Create(m_EffectName);
                        ((IDNMcParticleEffectItem)ObjSchemeItem).SetState(ObjectPropertiesBase.ParticalEffectState, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcParticleEffectItem)ObjSchemeItem).SetStartingTimePoint(ObjectPropertiesBase.ParticalEffectStartingTimePoint, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcParticleEffectItem)ObjSchemeItem).SetStartingDelay(ObjectPropertiesBase.ParticalEffectSartingDelay, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

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
