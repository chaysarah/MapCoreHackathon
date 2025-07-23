using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.MapWorld;
using MCTester.Managers;
using MCTester.ObjectWorld.OverlayManagerWorld;
using MCTester.Managers.ObjectWorld;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmGlobalMapViewportPointsList : Form
    {
        private IDNMcMapViewport m_GlobalMapVP;
        private IDNMcMapViewport m_SelectedViewport;

        List<string> m_mapViewportNames = new List<string>();
        List<IDNMcMapViewport> m_mapViewports = new List<IDNMcMapViewport>();
        List<IDNMcObject> dTempPoints = new List<IDNMcObject>();


        public frmGlobalMapViewportPointsList(IDNMcMapViewport currViewport, string desirableAction)
        {
            m_GlobalMapVP = currViewport;
            this.DialogResult = DialogResult.Ignore;

            InitializeComponent();
            
        }

        private void frmGlobalMapViewportList_Load(object sender, EventArgs e)
        {


            try
            {
                IDNMcMapViewport[] registeresMaps = m_GlobalMapVP.GetRegisteredLocalMaps();
                foreach (IDNMcMapViewport mcMapViewport in registeresMaps)
                {
                    m_mapViewportNames.Add(Manager_MCNames.GetNameByObject(mcMapViewport));
                }
                lstViewports.Items.AddRange(m_mapViewportNames.ToArray());
                m_mapViewports.AddRange(registeresMaps);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRegisteredLocalMaps", McEx);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            // todo remove overlay
        }

        public IDNMcMapViewport SelectedViewport
        {
            get { return m_SelectedViewport; }
            set { m_SelectedViewport = value; }
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
        }

        private void lstViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstViewports.SelectedIndex >= 0)
            {
                RemoveTempPoints();
                DNSMcVector2D[] aPolygonPoints, aArrowPoints = null;
                SelectedViewport = m_mapViewports[lstViewports.SelectedIndex];
                try
                {
                    if (SelectedViewport.MapType == DNEMapType._EMT_2D)
                        m_GlobalMapVP.GetLocalMapFootprintScreenPositions(SelectedViewport, out aPolygonPoints);
                    else
                        m_GlobalMapVP.GetLocalMapFootprintScreenPositions(SelectedViewport, out aPolygonPoints, out aArrowPoints);
                    if (m_GlobalMapVP.OverlayManager != null)
                    {
                        DNSMcBColor bColorPolygonInactive = new DNSMcBColor(144,238,144, 255);
                        DNSMcBColor bColorArrowInactive = new DNSMcBColor(0, 224, 209, 255); 
                        DNSMcBColor bColorPolygonActive = new DNSMcBColor(255, 0, 180, 255);
                        DNSMcBColor bColorArrowActive = new DNSMcBColor(128, 0, 32, 255);
                       
                        bool isActive = m_GlobalMapVP.GetActiveLocalMap() == SelectedViewport;
                      
                        CreateTextObjects(aPolygonPoints, isActive? bColorPolygonActive : bColorPolygonInactive);
                        if (aArrowPoints != null)
                            CreateTextObjects(aArrowPoints, isActive ? bColorArrowActive : bColorArrowInactive );
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetLocalMapFootprintScreenPositions", McEx);
                }
            }
            else
                RemoveTempPoints();
        }

        private void RemoveTempPoints()
        {
            try
            {
                foreach (IDNMcObject dNMcObject in dTempPoints)
                {
                    dNMcObject.Remove();
                    dNMcObject.Dispose();
                }
                dTempPoints.Clear();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveTempPoints", McEx);
            }
        }


        private void CreateTextObjects(DNSMcVector2D[] points, DNSMcBColor textColor)
        {
            IDNMcOverlayManager pOverlayManager = m_GlobalMapVP.OverlayManager;
           
            IDNMcObjectScheme mcObjectScheme = Manager_MCObject.GetTempObjectSchemeScreenPoints(m_GlobalMapVP);

            if (points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[1];
                    locationPoints[0] = points[i];
                    IDNMcObject obj = DNMcObject.Create( Manager_MCTSymbology.GetTempOverlay(pOverlayManager), mcObjectScheme, locationPoints);
                    obj.SetStringProperty(Manager_MCObject.TEXT_PROPERTY_ID, new DNMcVariantString(i.ToString(), true));
                    obj.SetBColorProperty(Manager_MCObject.COLOR_PROPERTY_ID, textColor);
                    dTempPoints.Add(obj);
                }
            }
        }

        private void frmGlobalMapViewportPointsList_FormClosed(object sender, FormClosedEventArgs e)
        {
            RemoveTempPoints();
        }

       
    }
}