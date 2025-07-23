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

namespace MCTester.General_Forms
{
    public partial class frmStandaloneItemsList : Form
    {
        DNEObjectSchemeNodeType mNodeType;
        private IDNMcObjectSchemeItem m_CurrItem;
        private List<string> m_lstItemText = new List<string>();
        private List<IDNMcObjectSchemeItem> m_lstItemValue = new List<IDNMcObjectSchemeItem>();

        public frmStandaloneItemsList(DNEObjectSchemeNodeType nodeType)
        {
            InitializeComponent();
            mNodeType = nodeType;
        }

        private void btnStandaloneItemsOK_Click(object sender, EventArgs e)
        {
            if (lstStandaloneItems.SelectedItem != null && lstStandaloneItems.SelectedItem.ToString() != "Default")
                CurrItem = ListValues[lstStandaloneItems.SelectedIndex];
            else
                CurrItem = null;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmStandaloneItemsList_Load(object sender, EventArgs e)
        {
            lstStandaloneItems.DisplayMember = "ListTexts";
            lstStandaloneItems.ValueMember = "ListValues";

            Dictionary<object, uint> dicItems = Manager_MCObjectSchemeItem.GetStandaloneItems();
            lstStandaloneItems.Items.Clear();

            switch (mNodeType)
            {
                case DNEObjectSchemeNodeType._LINE_ITEM:

                    foreach (IDNMcObjectSchemeItem item in dicItems.Keys)
                    {
                        if (item.GetNodeType() == mNodeType)
                        {
                            IDNMcLineItem currItem = (IDNMcLineItem)item;
                            m_lstItemText.Add(Manager_MCNames.GetNameByObject(currItem, " Line"));
                            m_lstItemValue.Add(currItem);
                        }
                    }               
                    break;
                case DNEObjectSchemeNodeType._TEXT_ITEM:
                    foreach (IDNMcObjectSchemeItem item in dicItems.Keys)
                    {
                        if (item.GetNodeType() == mNodeType)
                        {
                            IDNMcTextItem currItem = (IDNMcTextItem)item;
                            m_lstItemText.Add(Manager_MCNames.GetNameByObject(currItem," Text"));
                            m_lstItemValue.Add(currItem);
                        }
                    }
                    break;
                case DNEObjectSchemeNodeType._RECTANGLE_ITEM:
                    foreach (IDNMcObjectSchemeItem item in dicItems.Keys)
                    {
                        if (item.GetNodeType() == mNodeType)
                        {
                            IDNMcRectangleItem currItem = (IDNMcRectangleItem)item;
                            m_lstItemText.Add(Manager_MCNames.GetNameByObject(currItem, " Rectangle"));
                            m_lstItemValue.Add(currItem);
                        }
                    }
                    break;
            }

            lstStandaloneItems.Items.AddRange(m_lstItemText.ToArray());
            lstStandaloneItems.Items.Add("Default");
        }

        public List<string> ListTexts
        {
            get { return m_lstItemText; }
            set { m_lstItemText = value; }
        }

        public List<IDNMcObjectSchemeItem> ListValues
        {
            get { return m_lstItemValue; }
            set { m_lstItemValue = value; }
        }

        public IDNMcObjectSchemeItem CurrItem
        {
            get { return m_CurrItem; }
            set { m_CurrItem = value; }
        }
    }
}
