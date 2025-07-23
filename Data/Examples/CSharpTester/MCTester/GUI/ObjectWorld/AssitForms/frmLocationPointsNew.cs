using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers;
using MapCore.Common;

namespace MCTester.ObjectWorld.Assit_Forms
{
    public partial class frmLocationPointsNew : Form
    {
        private IDNMcOverlay m_SelectedOverlay;
        private IDNMcOverlayManager m_SelectedOverlayManager;

        private DNSMcVector3D[] m_ArrayLocationPoints;
        private IDNMcObjectScheme m_SelectedScheme;
        private IDNMcObjectSchemeItem m_SelectedItem;

        private List<string> m_lstSchemeText = new List<string>();
        private List<IDNMcObjectScheme> m_lstSchemeValue = new List<IDNMcObjectScheme>();

        private List<string> m_lstItemText = new List<string>();
        private List<IDNMcObjectSchemeItem> m_lstItemValue = new List<IDNMcObjectSchemeItem>();
        public enum FormType
        {
            ExistingScheme,
            NewScheme,
            NewSchemeAndOneItem
        }

        FormType m_FormType;


        public frmLocationPointsNew(IDNMcOverlay mcOverlay, FormType formType)
        {
            InitializeComponent();
            lstSchemes.DisplayMember = "SchemesTextList";
            lstSchemes.ValueMember = "SchemesValueList";

            // Standalone Items
            cmbLocationCoordinateSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));
            cmbLocationCoordinateSystem.Text = DNEMcPointCoordSystem._EPCS_WORLD.ToString();
            m_FormType = formType;

            m_SelectedOverlay = mcOverlay;
            m_SelectedOverlayManager = mcOverlay.GetOverlayManager();

            LoadOverlayManagersData();
        }

        private void SetErrorMsg(int row, string col)
        {
            MessageBox.Show("Invalid/Missing value in row " + row + " in column " + col + " , Fix it and try again", "Invalid/Missing value in location points");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_ArrayLocationPoints = null;

            if (ctrlPointsGrid1.GetPoints(out m_ArrayLocationPoints))
            {
                IDNMcObject mcObject = null;

                switch (m_FormType)
                {
                    case FormType.ExistingScheme:
                        try
                        {
                            mcObject = DNMcObject.Create(SelectedOverlay,
                                                    SelectedScheme,
                                                    ArrayLocationPoints);

                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }
                        break;
                    case FormType.NewScheme:
                        try
                        {
                            DNEMcPointCoordSystem LocationCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbLocationCoordinateSystem.Text);
                            IDNMcObjectLocation ObjectLocation = null;

                            mcObject = DNMcObject.Create(ref ObjectLocation,
                                                SelectedOverlay,
                                                LocationCoordSys,
                                                ArrayLocationPoints,
                                                RelativeToDTM);

                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }
                        break;
                    case FormType.NewSchemeAndOneItem:
                        Manager_MCObjectSchemeItem.RemoveItem(SelectedItem);

                        try
                        {
                            DNEMcPointCoordSystem LocationCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbLocationCoordinateSystem.Text);
                            mcObject = DNMcObject.Create(SelectedOverlay,
                                                SelectedItem,
                                                LocationCoordSys,
                                                ArrayLocationPoints,
                                                RelativeToDTM);
                            
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }
                        break;
                }
                if (mcObject != null)
                {
                    if (ObjectPropertiesBase.ImageCalc != null)
                        mcObject.SetImageCalc(ObjectPropertiesBase.ImageCalc);
                    this.DialogResult = DialogResult.OK;

                    this.Close();
                    MainForm.RebuildObjectWorldTree();
                }
            }

            
        }

        private void lstSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSchemes.SelectedItem != null)
            {
                if (m_FormType == FormType.ExistingScheme)
                    SelectedScheme = m_lstSchemeValue[lstSchemes.SelectedIndex];
                else if (m_FormType == FormType.NewSchemeAndOneItem)
                    SelectedItem = m_lstItemValue[lstSchemes.SelectedIndex];

                if (SelectedScheme != null)
                {
                    try
                    {
                        DNEMcPointCoordSystem coordSys;
                        SelectedScheme.GetObjectLocation(0).GetCoordSystem(out coordSys);
                        cmbLocationCoordinateSystem.Text = coordSys.ToString();

                        ctrlPointsGrid1.SetPointCoordSystem(coordSys);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetCoordSystem", McEx);
                    }

                    try
                    {
                        uint m_PropId;
                        bool m_bParam;
                        SelectedScheme.GetObjectLocation(0).GetRelativeToDTM(out m_bParam, out m_PropId);
                        chxLocationRelativeToDTM.Checked = m_bParam;    
                        ctrlPointsGrid1.SetIsRelativeToDTM(m_bParam);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("GetRelativeToDTM", McEx);
                    }
                }
            }
        }

        private void cmbLocationCoordinateSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNEMcPointCoordSystem PtCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem), cmbLocationCoordinateSystem.Text);
            ctrlPointsGrid1.SetPointCoordSystem(PtCoordSys);
        }
       
        private void frmLocationPointsNew_Load(object sender, EventArgs e)
        {
            switch (m_FormType)
            {
                case FormType.ExistingScheme:
                    gbSchemes.Text = "Schemes";
                    chxLocationRelativeToDTM.Enabled = false;
                    cmbLocationCoordinateSystem.Enabled = false;
                    break;
                case FormType.NewScheme:
                    gbSchemes.Visible = false;
                    break;
                case FormType.NewSchemeAndOneItem:
                     gbSchemes.Text = "Standalone Items";
                    // lstSchemes.Items.AddRange(m_lstItemText.ToArray());
                   break;
            }
        }

        private void LoadOverlayManagersData()
        {
            if (m_SelectedOverlayManager != null)
            {
                string name = string.Empty;
                if (m_FormType == FormType.ExistingScheme)
                {
                    IDNMcObjectScheme[] schemes = Manager_MCObjectScheme.GetSchemesWithoutTempSchemes(m_SelectedOverlayManager.GetObjectSchemes());

                    foreach (IDNMcObjectScheme scheme in schemes)
                    {
                        name = Manager_MCNames.GetNameByObject(scheme, "Scheme");

                        m_lstSchemeText.Add(name);
                        m_lstSchemeValue.Add(scheme);
                    }

                    lstSchemes.Items.AddRange(m_lstSchemeText.ToArray());
                }
                else if (m_FormType == FormType.NewSchemeAndOneItem)
                {
                    Dictionary<object, uint> items = Manager_MCObjectSchemeItem.AllParams;

                    foreach (IDNMcObjectSchemeNode item in items.Keys)
                    {
                        name = Manager_MCNames.GetNameByObject(item, "Item");

                        m_lstItemText.Add(name);
                        m_lstItemValue.Add((IDNMcObjectSchemeItem)item);
                    }

                    lstSchemes.Items.AddRange(m_lstItemText.ToArray());
                }
            }
        }


        public List<string> SchemesTextList
        {
            get { return m_lstSchemeText; }
            set { m_lstSchemeText = value; }
        }

        public List<IDNMcObjectScheme> SchemesValueList
        {
            get { return m_lstSchemeValue; }
            set { m_lstSchemeValue = value; }
        }

        public List<string> ItemsTextList
        {
            get { return m_lstItemText; }
            set { m_lstItemText = value; }
        }

        public List<IDNMcObjectSchemeItem> ItemsValueList
        {
            get { return m_lstItemValue; }
            set { m_lstItemValue = value; }
        }

        public IDNMcOverlay SelectedOverlay
        {
            get { return m_SelectedOverlay; }
            set { m_SelectedOverlay = value; }

        }

        public IDNMcObjectScheme SelectedScheme
        {
            get { return m_SelectedScheme; }
            set { m_SelectedScheme = value; }
        }

        public IDNMcObjectSchemeItem SelectedItem
        {
            get { return m_SelectedItem; }
            set { m_SelectedItem = value; }
        }

        public DNSMcVector3D[] ArrayLocationPoints
        {
            get { return m_ArrayLocationPoints; }
        }

        public string LocationCoordSys
        {
            get { return cmbLocationCoordinateSystem.Text; }
        }

        public bool RelativeToDTM
        {
            get { return chxLocationRelativeToDTM.Checked; }
        }
    }
}
