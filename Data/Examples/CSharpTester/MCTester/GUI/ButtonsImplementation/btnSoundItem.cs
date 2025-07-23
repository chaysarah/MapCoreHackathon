using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MCTester.GUI.Map;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnSoundItem
    {
        private DNSMcVector3D[] m_LocationPoints;
        private string m_SoundName;

        public btnSoundItem()
        {
            m_LocationPoints = new DNSMcVector3D[1];
        }

        public void ExecuteAction()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "all files *.*|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                m_SoundName = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                MCTMapForm.OnMapClicked += new ClickOnMapEventArgs(MCTMapForm_OnMapClicked);
            }
            else
            {
                MessageBox.Show("Aborted");
                return;
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
                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcSoundItem.Create(m_SoundName);
                        ((IDNMcSoundItem)ObjSchemeItem).SetLoop(ObjectPropertiesBase.SoundLoop, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        ((IDNMcSoundItem)ObjSchemeItem).SetState(ObjectPropertiesBase.SoundState, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

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
