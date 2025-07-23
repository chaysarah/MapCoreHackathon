using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmAddOverlayManagerWorldPt : Form, IOnMapClick
    {
        private DNSMcVector3D m_ScreenLocation;
        
        public frmAddOverlayManagerWorldPt(Point ClickedLocation)
        {
            InitializeComponent();

            Point mapFormLocation = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Location;
            this.Location = new Point(ClickedLocation.X + mapFormLocation.X, ClickedLocation.Y + mapFormLocation.Y);
            
            m_ScreenLocation.x = ClickedLocation.X;
            m_ScreenLocation.y = ClickedLocation.Y;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditMode.AddOverlayManagerWorldPoint(ctrl3DOMWorldPt.GetVector3D());
                this.Close();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("AddOverlayManagerWorldPoint", McEx);
            }
        }

        private void frmAddOverlayManagerWorldPt_Load(object sender, EventArgs e)
        {
            IDNMcMapViewport viewport = MCTMapFormManager.MapForm.Viewport;
            MCTMapClick.ConvertScreenLocationToObjectLocation(viewport, m_ScreenLocation, DNEMcPointCoordSystem._EPCS_WORLD, null, this, true);
        }

        public void OnMapClick(DNSMcVector3D screenLocation, DNSMcVector3D location, DNEMcPointCoordSystem locationCoordSystem, bool isRelativeToDTM)
        {
            ctrl3DOMWorldPt.SetVector3D( location);
        }

        public void OnMapClickError(DNEMcErrorCode mcErrorCode, string functionName)
        {
            MessageBox.Show(IDNMcErrors.ErrorCodeToString(mcErrorCode), functionName , MessageBoxButtons.OK);
        }
    }
}
