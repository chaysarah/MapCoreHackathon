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
    public partial class frmGridRegionText : Form
    {
        private IDNMcTextItem m_CurrText;
        private bool m_SetAll;
        private List<string> m_lstTextText = new List<string>();
        private List<IDNMcTextItem> m_lstTextValue = new List<IDNMcTextItem>();

        public frmGridRegionText()
        {
            InitializeComponent();
            lstGridRegionText.DisplayMember = "TextListText";
            lstGridRegionText.ValueMember = "TextListValue";
        }

        public frmGridRegionText(IDNMcTextItem currText) : this()
        {
            m_CurrText = currText;
        }

        private void btnGridRegionTextOK_Click(object sender, EventArgs e)
        {
            if (lstGridRegionText.SelectedItem != null && lstGridRegionText.SelectedItem.ToString() != "Null")
            {
                CurrText = TextListValue[lstGridRegionText.SelectedIndex];
            }
            else
                CurrText = null;
            SetAll = chxSetAllGridLines.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmGridRegionText_Load(object sender, EventArgs e)
        {
            Dictionary<object, uint> TextItems = Manager_MCObjectSchemeItem.GetStandaloneItems();
            lstGridRegionText.Items.Clear();
            int selectedIndex = -1;
            int index = 0;
            foreach (IDNMcObjectSchemeItem item in TextItems.Keys)
            {
                if (item.GetNodeType() == DNEObjectSchemeNodeType._TEXT_ITEM)
                {
                    IDNMcTextItem currText = (IDNMcTextItem)item;
                    m_lstTextText.Add( Manager_MCNames.GetNameByObject(currText,"Text"));
                    m_lstTextValue.Add(currText);

                    if (m_CurrText != null && m_CurrText == currText)
                        selectedIndex = index;
                    index++;
                }
            }

            lstGridRegionText.Items.AddRange(m_lstTextText.ToArray());
            lstGridRegionText.Items.Add("Null");

            if (selectedIndex >= 0 && selectedIndex < lstGridRegionText.Items.Count)
                lstGridRegionText.SelectedIndex = selectedIndex;
        }

        public IDNMcTextItem CurrText
        {
            get { return m_CurrText; }
            set { m_CurrText = value; }
        }

        public bool SetAll
        {
            get { return m_SetAll; }
            set { m_SetAll = value; }
        }

        public List<string> TextListText
        {
            get { return m_lstTextText; }
            set { m_lstTextText = value; }
        }

        public List<IDNMcTextItem> TextListValue
        {
            get { return m_lstTextValue; }
            set { m_lstTextValue = value; }
        }
    }
}