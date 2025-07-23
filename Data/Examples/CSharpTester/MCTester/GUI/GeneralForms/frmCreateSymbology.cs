using MapCore;
using MapCore.Common;
using MCTester.Managers;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.ObjectWorld.ObjectsUserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.General_Forms
{
    public partial class frmCreateSymbology : Form
    {
        private IDNMcOverlay m_SelectedOverlay;
        private IDNMcOverlayManager m_SelectedOverlayManager;
        IDNMcObject m_ObjectEmptySymbology = null;
        IDNMcObject m_ObjectFromSymbology = null;
        ActionType m_ActionType;
        public enum ActionType { CreateEmpty, CreateFrom }
        bool m_IsClickCreate = false;
        private char[] m_sDummySIDC = { 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X', 'X' };

        List<IDNMcObject> m_lstTempMcObjects = new List<IDNMcObject>();
        Dictionary<IDNMcOverlayManager, IDNMcOverlay> m_TempOverlays = new Dictionary<IDNMcOverlayManager, IDNMcOverlay>();       

        public frmCreateSymbology(ActionType actionType, IDNMcOverlay mcOverlay)
        {
            InitializeComponent();

            m_ActionType = actionType;
            m_SelectedOverlay = mcOverlay;
            m_SelectedOverlayManager = mcOverlay.GetOverlayManager();

            ctrlSymbologySymbolID1.SetSymbologyStandard(Manager_MCTSymbology.GetSymbologyStandard(m_SelectedOverlayManager));

            if (m_ActionType == ActionType.CreateEmpty)
            {
                Size = new Size(725, 380);
                ctrlGeometricAmplifiers1.Visible = false;
                chxFlipped.BringToFront();
                gbAnchorPoints.Visible = false;
                ctrlPointsGrid1.Visible = false;
                btnCreateFromSymbology.Visible = false;
                btnEditObject.Visible = false;
                btnShowAnchorPoints2.Visible = false;
            }
            else
            {
                Size = new Size(725, 740);
                btnCreatePointlessFromSymbology.Visible = false;
                btnInitObject.Visible = false;
                btnEditObjectPointless.Visible = false;
                chxFlipped.Visible = false;
                ctrlGeometricAmplifiers1.SetIsInCreateObject(true);
                btnShowAnchorPoints1.Visible = false;

            }

            this.ctrlSymbologySymbolID1.SymbolIDUpdated += new EventHandler(MyEventHandlerFunction_SymbolIDUpdated);
            this.ctrlSymbologySymbolID1.SymbologyStandardUpdated += new EventHandler(MyEventHandlerFunction_SymbologyStandardUpdated);


        }

        public void MyEventHandlerFunction_SymbolIDUpdated(object sender, EventArgs e)
        {
            ctrlAmplifiers1.ResetAmplifiers();
            ctrlGeometricAmplifiers1.ResetGeometricAmplifiers();
            ctrlPointsGrid1.ClearDGV();
            ctrlSymbologySymbolID1.Focus();
        }

        public void MyEventHandlerFunction_SymbologyStandardUpdated(object sender, EventArgs e)
        {
        }

        private void btnCreatePointlessFromSymbology_Click(object sender, EventArgs e)
        {
            try
            {

                m_ObjectEmptySymbology = CreatePointlessFromSymbologyObject();
                Manager_MCTSymbology.SetSymbologyStandard(m_SelectedOverlayManager, ctrlSymbologySymbolID1.GetSymbologyStandard());

                Manager_MCTSymbology.AddFormCreateSymbologyObjects(this, m_ObjectEmptySymbology);
                if (m_ObjectEmptySymbology != null)
                {
                    m_IsClickCreate = true;

                    btnInitObject.Enabled = true;
                    btnEditObjectPointless.Enabled = false;
                    btnShowAnchorPoints1.Enabled = false;
                }

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreatePointlessFromSymbology", McEx);
            }
        }

        private IDNMcObject CreatePointlessFromSymbologyObject(bool isObjectTemp = false)
        {
            try
            {
                IDNMcOverlay overlay = m_SelectedOverlay;
                if (isObjectTemp)
                    overlay = Manager_MCTSymbology.GetTempOverlay(m_SelectedOverlayManager);
                return DNMcObject.CreatePointlessFromSymbology(
                    overlay,
                    ctrlSymbologySymbolID1.GetSymbologyStandard(),
                    ctrlSymbologySymbolID1.GetSymbolID(),
                    ctrlAmplifiers1.GetAmplifiers(),
                    chxFlipped.Checked);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreatePointlessFromSymbology", McEx);
            }
            return null;
        }

        private void btnCreateFromSymbology_Click(object sender, EventArgs e)
        {
            try
            {
                DNSMcVector3D[] locationPoints;
                bool result = ctrlPointsGrid1.GetPoints(out locationPoints);
                if (result)
                {
                    m_ObjectFromSymbology = DNMcObject.CreateFromSymbology(
                            m_SelectedOverlay,
                            ctrlSymbologySymbolID1.GetSymbologyStandard(),
                            ctrlSymbologySymbolID1.GetSymbolID(),
                            locationPoints,
                            ctrlGeometricAmplifiers1.GetGeometricAmplifiers(),
                            ctrlAmplifiers1.GetAmplifiers());

                    Manager_MCTSymbology.SetSymbologyStandard(m_SelectedOverlayManager, ctrlSymbologySymbolID1.GetSymbologyStandard());

                    if (m_ObjectFromSymbology != null)
                    {
                        Manager_MCTSymbology.AddFormCreateSymbologyObjects(this, m_ObjectFromSymbology);

                        btnEditObject.Enabled = true;
                        btnShowAnchorPoints2.Enabled = true;
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CreateFromSymbology", McEx);
                return;
            }
        }
        
        private void btnInitializeAmplifiers_Click(object sender, EventArgs e)
        {
            try
            {
                ctrlAmplifiers1.ResetAmplifiers();

                IDNMcObject m_tempMcObject = CreatePointlessFromSymbologyObject(true);
                if (m_tempMcObject != null)
                {
                    String pstrSymbolID = null;
                    DNSKeyVariantValue[] paAmplifiers = null;

                    m_tempMcObject.GetSymbologyGraphicalProperties(out pstrSymbolID, out paAmplifiers);

                    ctrlAmplifiers1.SetAmplifiers(m_tempMcObject, paAmplifiers);

                    btnInitObject.Enabled = false;
                    btnEditObjectPointless.Enabled = false;
                    btnShowAnchorPoints1.Enabled = false;

                    m_lstTempMcObjects.Add(m_tempMcObject);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSymbologyGraphicalProperties", McEx);
                return;
            }
           try
            {
                
                    DNSMultiKeyName[] paGeometricAmplifiers;
                    string[] paAmplifiers;
                    m_SelectedOverlayManager.GetSymbologyStandardNames(
                        ctrlSymbologySymbolID1.GetSymbologyStandard(),
                        ctrlSymbologySymbolID1.GetSymbolID(),
                            out paGeometricAmplifiers,
                            out paAmplifiers);

                    ctrlGeometricAmplifiers1.SetGeometricAmplifiersNames(paGeometricAmplifiers);

            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetSymbologyStandardNames", McEx);
                return;
            }
        }

        private void btnInitObject_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ObjectEmptySymbology != null)
                {
                    this.Hide();
                    MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitEmpty);

                    IDNMcObjectScheme mcObjectScheme = m_ObjectEmptySymbology.GetScheme();
                    IDNMcObjectSchemeNode node = mcObjectScheme.GetNodeByID(1);
                    if (node == null)
                        node = mcObjectScheme.GetNodeByID(2);
                    if (node != null)
                    {
                        MCTMapFormManager.MapForm.EditMode.StartInitObject(m_ObjectEmptySymbology, (IDNMcObjectSchemeItem)node);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("StartInitObject", McEx);
                return;
            }
        }

        private void InitEmpty(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitEmpty);
            
            if(nExitCode == 1)
            {
                btnEditObjectPointless.Enabled = true;
                if (!m_IsClickCreate)
                {
                    Manager_MCTSymbology.RemoveTempAnchorPoints(pObject);
                }
                btnShowAnchorPoints1.Enabled = true;
                m_IsClickCreate = false;
            }
            this.Show();
        }

        private void btnEditObject_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ObjectFromSymbology != null)
                {
                    this.Hide();
                    MCTMapFormManager.MapForm.EditModeManagerCallback.EditItemResultsEvent += new EditItemResultsEventArgs(EditSymbology);

                    IDNMcObjectScheme mcObjectScheme = m_ObjectFromSymbology.GetScheme();
                    IDNMcObjectSchemeNode node = mcObjectScheme.GetNodeByID(1);
                    if (node == null)
                        node = mcObjectScheme.GetNodeByID(2);
                    if (node != null)
                    {
                        MCTMapFormManager.MapForm.EditMode.StartEditObject(m_ObjectFromSymbology, (IDNMcObjectSchemeItem)node);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("StartEditObject", McEx);
                return;
            }
        }

        private void EditSymbology(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.EditItemResultsEvent -= new EditItemResultsEventArgs(EditSymbology);
            
            if(nExitCode == 1)
                Manager_MCTSymbology.RemoveTempAnchorPoints(pObject);
            this.Show();
        }



        private void frmCreateSymbology_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void btnShowAnchorPoints1_Click(object sender, EventArgs e)
        {
            Manager_MCTSymbology.ShowAnchorPoints(m_ObjectEmptySymbology);
        }

       
        private void btnShowAnchorPoints2_Click(object sender, EventArgs e)
        {
            Manager_MCTSymbology.ShowAnchorPoints(m_ObjectFromSymbology);
        }

        private void btnEditObjectPointless_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ObjectEmptySymbology != null)
                {
                    this.Hide();
                    MCTMapFormManager.MapForm.EditModeManagerCallback.EditItemResultsEvent += new EditItemResultsEventArgs(EditSymbology);

                    IDNMcObjectScheme mcObjectScheme = m_ObjectEmptySymbology.GetScheme();
                    IDNMcObjectSchemeNode node = mcObjectScheme.GetNodeByID(1);
                    if (node == null)
                        node = mcObjectScheme.GetNodeByID(2);
                    if (node != null)
                    {
                        MCTMapFormManager.MapForm.EditMode.StartEditObject(m_ObjectEmptySymbology, (IDNMcObjectSchemeItem)node);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("StartEditObject", McEx);
                return;
            }
        }
    }
}
