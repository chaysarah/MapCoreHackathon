using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class CtrlRawComponents : UserControl
    {
        private object m_CurrentObject;
       
        public CtrlRawComponents()
        {
            InitializeComponent();
        }

        public void SetMCObject(object mcObject, bool isBtnVisible = true)
        {
            m_CurrentObject = mcObject;
            btnGetComponents.Visible = isBtnVisible;
            btnGetComponents_Click(null, null);
        }

        private void btnGetComponents_Click(object sender, EventArgs e)
        {
            if (m_CurrentObject != null)
            {
                lstGetComponentParams.Items.Clear();
                try
                {
                    DNSComponentParams[] componentParams = null;
                    if (m_CurrentObject is IDNMcRawRasterMapLayer)
                        componentParams = ((IDNMcRawRasterMapLayer)m_CurrentObject).GetComponents();
                    else if (m_CurrentObject is IDNMcRawMaterialMapLayer)
                        componentParams = ((IDNMcRawMaterialMapLayer)m_CurrentObject).GetComponents();
                    else if (m_CurrentObject is IDNMcRawTraversabilityMapLayer)
                        componentParams = ((IDNMcRawTraversabilityMapLayer)m_CurrentObject).GetComponents();
                    foreach (DNSComponentParams component in componentParams)
                    {
                        lstGetComponentParams.Items.Add(component.eType.ToString() + "-" + component.strName);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetComponents", McEx);
                }
            }
        }
    }
}
