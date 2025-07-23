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

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmLocalMapFootprintItem : Form
    {
        IDNMcMapViewport m_CurrViewport;
        IDNMcLineItem m_ActiveLine = null;
        IDNMcLineItem m_InactiveLine = null;

        List<IDNMcObjectSchemeItem> m_lineItems = new List<IDNMcObjectSchemeItem>();
        List<string> m_lineItemsNames = new List<string>();

        List<string> m_mapViewportNames = new List<string>();

        IDNMcMapViewport[] m_registeresMaps;
        Dictionary<object, uint>.KeyCollection m_StandaloneItems;

        public frmLocalMapFootprintItem(IDNMcMapViewport currViewport)
        {
            InitializeComponent();
            m_CurrViewport = currViewport;
            m_StandaloneItems = Manager_MCObjectSchemeItem.GetStandaloneItems().Keys;
        }

        private void GetLocalMapFootprintItem()
        {
            btnClearList_Click(null, null);

            try
            {
                m_CurrViewport.GetLocalMapFootprintItem(out m_InactiveLine, out m_ActiveLine, GetSelectedLocalMapViewport());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetLocalMapFootprintItem", McEx);
            }

            int index = 0;
            
            foreach (IDNMcObjectSchemeItem item in m_StandaloneItems)
            {
                if (item.GetNodeType() == DNEObjectSchemeNodeType._LINE_ITEM)
                {
                    if (ActiveLine == item)
                        lstActiveLine.SetSelected(index, true);
                    if (InactiveLine == item)
                        lstInactiveLine.SetSelected(index, true);
                    
                    index++;
                }
            }
        }

        private void frmLocalMapFootprintItem_Load(object sender, EventArgs e)
        {
            lstActiveLine.Items.Clear();
            lstInactiveLine.Items.Clear();
            
            try
            {
                m_registeresMaps = m_CurrViewport.GetRegisteredLocalMaps();
                foreach (IDNMcMapViewport mcMapViewport in m_registeresMaps)
                {
                    m_mapViewportNames.Add(Manager_MCNames.GetNameByObject(mcMapViewport));
                }
                lstViewports.Items.AddRange(m_mapViewportNames.ToArray());
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRegisteredLocalMaps", McEx);
            }

            

            int index = 0;
            foreach (IDNMcObjectSchemeItem item in m_StandaloneItems)
            {
                if (item.GetNodeType() == DNEObjectSchemeNodeType._LINE_ITEM)
                {
                    m_lineItems.Add(item);
                    m_lineItemsNames.Add(Manager_MCNames.GetNameByObject(item)); 
                }
                index++;
            }

            lstActiveLine.Items.AddRange(m_lineItemsNames.ToArray());
            lstInactiveLine.Items.AddRange(m_lineItemsNames.ToArray());

            GetLocalMapFootprintItem();
        }

        public IDNMcLineItem ActiveLine
        {
            get { return m_ActiveLine; }
            set { m_ActiveLine = value; }
        }

        public IDNMcLineItem InactiveLine
        {
            get { return m_InactiveLine; }
            set { m_InactiveLine = value; }
        }

        public IDNMcMapViewport GetSelectedLocalMapViewport()
        {
            for (int i = 0; i < m_registeresMaps.Length; i++)
            {
                if (lstViewports.GetSelected(i) == true)
                    return m_registeresMaps[i];
            }
            return null;

        }
            

        private void btnOK_Click(object sender, EventArgs e)
        {
            if( lstActiveLine.SelectedIndex >= 0 )
                ActiveLine = (IDNMcLineItem)m_lineItems[lstActiveLine.SelectedIndex];
            else
                ActiveLine = null;

            if (lstInactiveLine.SelectedIndex >= 0)
                InactiveLine = (IDNMcLineItem)m_lineItems[lstInactiveLine.SelectedIndex];
            else
                InactiveLine = null;

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            lstActiveLine.ClearSelected();
            ActiveLine = null;

            lstInactiveLine.ClearSelected();
            InactiveLine = null;
        }

        private void btnClearViewportSelection_Click(object sender, EventArgs e)
        {
            lstViewports.ClearSelected();
        }

        private void lstViewports_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLocalMapFootprintItem();
        }
    }
}