using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmSectionMapPolygonItem : Form
    {
        IDNMcSectionMapViewport m_CurrSectionMapViewport;
        IDNMcPolygonItem m_SelectedPolygon = null;

        public frmSectionMapPolygonItem(IDNMcSectionMapViewport currViewport)
        {
            InitializeComponent();
            m_CurrSectionMapViewport = currViewport;
        }

        private void btnSectionMapPolygonItemOK_Click(object sender, EventArgs e)
        {
            m_SelectedPolygon = (IDNMcPolygonItem)lstSectionMapPolygonItem.SelectedItem;
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmSectionMapPolygonItem_Load(object sender, EventArgs e)
        {
            Dictionary<object, uint> standaloneItems = MCTester.Managers.ObjectWorld.Manager_MCObjectSchemeItem.GetStandaloneItems();

            try
            {
                m_SelectedPolygon = m_CurrSectionMapViewport.SectionPolygonItem;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SectionPolygonItem", McEx);
            }

            int index = 0;
            foreach (IDNMcObjectSchemeItem item in standaloneItems.Keys)
            {
                if (item.GetNodeType() == DNEObjectSchemeNodeType._POLYGON_ITEM)
                {
                    lstSectionMapPolygonItem.Items.Add(item);

                    if (m_SelectedPolygon == item)
                        lstSectionMapPolygonItem.SetSelected(index, true);
                    index++;
                }

            }
        }

        public IDNMcPolygonItem SelectedPolygon
        {
            get { return m_SelectedPolygon; }
            set { m_SelectedPolygon = value; }
        }

        private void btnClearViewportSelection_Click(object sender, EventArgs e)
        {
            lstSectionMapPolygonItem.ClearSelected();
        }
    }
}