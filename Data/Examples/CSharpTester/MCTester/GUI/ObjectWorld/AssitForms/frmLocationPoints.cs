using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers;
using MapCore.Common;

namespace MCTester.ObjectWorld.ObjectsUserControls
{
    public partial class frmLocationPoints : Form
    {
        private DNSMcVector3D [] m_ArrayLocationPoints;
        private IDNMcOverlay m_SelectedOverlay;
        private IDNMcOverlayManager m_SelectedOverlayManager;
        private IDNMcObjectScheme m_SelectedScheme;
        private IDNMcObjectSchemeItem m_SelectedItem;
        private string m_ObjectCreateType;

        private List<string> m_lstOverlayManagerText = new List<string>();
        private List<IDNMcOverlayManager> m_lstOverlayManagerValue = new List<IDNMcOverlayManager>();

        private List<string> m_lstOverlayText = new List<string>();
        private List<IDNMcOverlay> m_lstOverlayValue = new List<IDNMcOverlay>();

        private List<string> m_lstSchemeText = new List<string>();
        private List<IDNMcObjectScheme> m_lstSchemeValue = new List<IDNMcObjectScheme>();

        private List<string> m_lstItemText = new List<string>();
        private List<IDNMcObjectSchemeItem> m_lstItemValue = new List<IDNMcObjectSchemeItem>();
 
        public frmLocationPoints(object sender)
        {
            InitializeComponent();
            cmbLocationCoordinateSystem.Items.AddRange(Enum.GetNames(typeof(DNEMcPointCoordSystem)));

            lstOverlayManagers.DisplayMember = "OverlayManagersTextList";
            lstOverlayManagers.ValueMember = "OverlayManagersValueList";

            lstOverlays.DisplayMember = "OverlaysTextList";
            lstOverlays.ValueMember = "OverlaysValueList";

            lstSchemes.DisplayMember = "SchemesTextList";
            lstSchemes.ValueMember = "SchemesValueList";

            lstItems.DisplayMember = "ItemsTextList";
            lstItems.ValueMember = "ItemsValueList";
                        
            dgvItemLocationPoints.Rows.Clear();
            cmbLocationCoordinateSystem.Text = DNEMcPointCoordSystem._EPCS_WORLD.ToString();
            m_ObjectCreateType = sender.ToString();
        }
        
        private void lstOverlays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOverlays.SelectedItem != null)
            {
                SelectedOverlay = m_lstOverlayValue[lstOverlays.SelectedIndex];
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_ArrayLocationPoints = new DNSMcVector3D[dgvItemLocationPoints.Rows.Count-1];

            for (int i = 0; i < m_ArrayLocationPoints.Length; i++)
            {
                m_ArrayLocationPoints[i].x = double.Parse(dgvItemLocationPoints[0, i].Value.ToString());
                m_ArrayLocationPoints[i].y = double.Parse(dgvItemLocationPoints[1, i].Value.ToString());

                if (dgvItemLocationPoints[2, i].Value == null || dgvItemLocationPoints[2, i].Value.ToString() == "")
                    m_ArrayLocationPoints[i].z = 0;
                else
                    m_ArrayLocationPoints[i].z = double.Parse(dgvItemLocationPoints[2, i].Value.ToString());
            }
            IDNMcObject mcObject = null;
            switch (m_ObjectCreateType)
            {
                case "Based on a existing scheme":
                    try
                    {
                        mcObject = DNMcObject.Create(SelectedOverlay,
                                                SelectedScheme,
                                                ArrayLocationPoints);

                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
            	    break;
                case "With new scheme containing one location":
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
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                    break;
                case "With new scheme containing one location and one item":
                    MCTester.Managers.ObjectWorld.Manager_MCObjectSchemeItem.RemoveItem(SelectedItem);

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
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                    break;
            }

            if (mcObject != null && ObjectPropertiesBase.ImageCalc != null)
                mcObject.SetImageCalc(ObjectPropertiesBase.ImageCalc);
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void lstSchemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSchemes.SelectedItem != null)
            {
                SelectedScheme = m_lstSchemeValue[lstSchemes.SelectedIndex];
                IDNMcObjectSchemeNode[] items = SelectedScheme.GetNodes(DNENodeKindFlags._ENKF_ANY_ITEM);

                foreach (IDNMcObjectSchemeItem item in items)
                {
                    m_lstItemText.Add(Manager_MCNames.GetNameByObject(item, "Item"));
                    m_lstItemValue.Add(item);
                }

                lstItems.Items.AddRange(m_lstItemText.ToArray());

                Dictionary<object, uint> itemDictionary = Manager_MCObjectSchemeItem.AllParams;
                foreach (IDNMcObjectSchemeItem standaloneItem in itemDictionary.Keys)
                {
                    lstItems.Items.Add(standaloneItem);
                }

                DNEMcPointCoordSystem coordSys;
                //bool relativeToDTM;

                SelectedScheme.GetObjectLocation(0).GetCoordSystem(out coordSys);
                cmbLocationCoordinateSystem.Text = coordSys.ToString();

                try
                {
                    uint m_PropId;
                    bool m_bParam;
                    SelectedScheme.GetObjectLocation(0).GetRelativeToDTM(out m_bParam, out m_PropId);

                   

                    ctrlSamplePointLocation._IsRelativeToDTM = chxLocationRelativeToDTM.Checked;

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetRelativeToDTM", McEx);
                }

               /* uint PropId; // TODO: properly support RelativeToDTM as PrivateProperty!
                SelectedScheme.GetObjectLocation(0).GetRelativeToDTM(out relativeToDTM, out PropId);
                chxLocationRelativeToDTM.Checked = relativeToDTM;*/
            }            
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstItems.SelectedItem != null)
            {
                SelectedItem = m_lstItemValue[lstItems.SelectedIndex];
            }            
        }

        private void cmbLocationCoordinateSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            DNEMcPointCoordSystem PtCoordSys = (DNEMcPointCoordSystem)Enum.Parse(typeof(DNEMcPointCoordSystem),cmbLocationCoordinateSystem.Text);
            switch (PtCoordSys)
            {
                case DNEMcPointCoordSystem._EPCS_IMAGE:
                    ctrlSamplePointLocation.PointCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
                    break;
                case  DNEMcPointCoordSystem._EPCS_SCREEN:
                    ctrlSamplePointLocation.PointCoordSystem = DNEMcPointCoordSystem._EPCS_SCREEN;
                    break;
                case DNEMcPointCoordSystem._EPCS_WORLD:
                    ctrlSamplePointLocation.PointCoordSystem = DNEMcPointCoordSystem._EPCS_WORLD;
                    break;
            }

        }

        private void frmLocationPoints_Load(object sender, EventArgs e)
        {
            switch (m_ObjectCreateType)
            {
                case "Based on a existing scheme":
                    gbItems.Visible = false;
                    chxLocationRelativeToDTM.Enabled = false;
                    cmbLocationCoordinateSystem.Enabled = false;
                    break;
                case "With new scheme containing one location":
                    gbItems.Visible = false;
                    gbSchemes.Visible = false;
                    break;
                case "With new scheme containing one location and one item":
                    gbSchemes.Visible = false;
                    break;
            }


            foreach (IDNMcOverlayManager OM in Manager_MCOverlayManager.AllParams.Keys)
            {
                string name = Manager_MCNames.GetNameByObject(OM, "Overlay Manager");

                m_lstOverlayManagerText.Add(name);
                m_lstOverlayManagerValue.Add(OM);
            }

            lstOverlayManagers.Items.AddRange(m_lstOverlayManagerText.ToArray());            
        }       

        private void lstOverlayManagers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOverlayManagers.SelectedItem != null)
            {
                lstOverlays.Items.Clear();
                m_lstOverlayText.Clear();
                m_lstOverlayValue.Clear();

                lstSchemes.Items.Clear();
                m_lstSchemeText.Clear();
                m_lstSchemeValue.Clear();

                lstItems.Items.Clear();
                m_lstItemText.Clear();
                m_lstItemValue.Clear();                

                //Get all overlays that that belong to the selected overlay manager
                m_SelectedOverlayManager = m_lstOverlayManagerValue[lstOverlayManagers.SelectedIndex];
                IDNMcOverlay [] overlays = m_SelectedOverlayManager.GetOverlays();
                IDNMcObjectScheme[] schemes = Manager_MCObjectScheme.GetSchemesWithoutTempSchemes(m_SelectedOverlayManager.GetObjectSchemes());
                
                string name = string.Empty;
                foreach (IDNMcOverlay overlay in overlays)
                {
                    name = Manager_MCNames.GetNameByObject(overlay, "Overlay");

                    m_lstOverlayText.Add(name);
                    m_lstOverlayValue.Add(overlay);
                }

                foreach (IDNMcObjectScheme scheme in schemes)
                {
                    name = Manager_MCNames.GetNameByObject(scheme, "Scheme");

                    m_lstSchemeText.Add(name);
                    m_lstSchemeValue.Add(scheme);                                       
                }                

                lstOverlays.Items.AddRange(m_lstOverlayText.ToArray());
                lstSchemes.Items.AddRange(m_lstSchemeText.ToArray());

                Dictionary<object, uint> items = Manager_MCObjectSchemeItem.AllParams;

                foreach (IDNMcObjectSchemeNode item in items.Keys)
                {
                    name = Manager_MCNames.GetNameByObject(item, "Item");

                    m_lstItemText.Add(name);
                    m_lstItemValue.Add((IDNMcObjectSchemeItem)item);

                    lstItems.Items.Add(name);
                } 
            }
        }

        #region Public Property
        public List<string> OverlayManagersTextList
        {
            get { return m_lstOverlayManagerText; }
            set { m_lstOverlayManagerText = value; }
        }

        public List<IDNMcOverlayManager> OverlayManagersValueList
        {
            get { return m_lstOverlayManagerValue; }
            set { m_lstOverlayManagerValue = value; }
        }

        public List<string> OverlaysTextList
        {
            get { return m_lstOverlayText; }
            set { m_lstOverlayText = value; }
        }

        public List<IDNMcOverlay> OverlaysValueList
        {
            get { return m_lstOverlayValue; }
            set { m_lstOverlayValue = value; }
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

        #endregion
   
    }
}