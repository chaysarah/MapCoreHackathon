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
    public partial class frmGridRegionLine : Form
    {
        private IDNMcLineItem m_CurrLine;
        private bool m_SetAll;
        private List<string> m_lstLineText = new List<string>();
        private List<IDNMcLineItem> m_lstLineValue = new List<IDNMcLineItem>();

        public frmGridRegionLine()
        {
            InitializeComponent();
            lstGridRegionLine.DisplayMember = "LineListText";
            lstGridRegionLine.ValueMember = "LineListValue";

        }

        public frmGridRegionLine(IDNMcLineItem currLine) : this()
        {
            m_CurrLine = currLine;
        }


        private void btnGridRegionLineOK_Click(object sender, EventArgs e)
        {
            if (lstGridRegionLine.SelectedItem != null && lstGridRegionLine.SelectedItem.ToString() != "Null")
                CurrLine = LineListValue[lstGridRegionLine.SelectedIndex];
            else
                CurrLine = null;
            SetAll = chxSetAllGridLines.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
                
        public IDNMcLineItem CurrLine
        {
            get { return m_CurrLine;}
            set {m_CurrLine = value;}
        }

        public bool SetAll
        {
            get { return m_SetAll; }
            set { m_SetAll = value; }
        }

        private void frmGridRegionLine_Load(object sender, EventArgs e)
        {
            Dictionary<object, uint> LineItems = Manager_MCObjectSchemeItem.GetStandaloneItems();
            lstGridRegionLine.Items.Clear();

            int selectedIndex = -1;
            int index = 0;
            foreach (IDNMcObjectSchemeItem item in LineItems.Keys)
            {
                if (item.GetNodeType() == DNEObjectSchemeNodeType._LINE_ITEM)
                {
                    IDNMcLineItem currLine = (IDNMcLineItem)item;
                    m_lstLineText.Add( Manager_MCNames.GetNameByObject(currLine,"Line"));
                    m_lstLineValue.Add(currLine);

                    if (m_CurrLine != null && m_CurrLine == currLine)
                        selectedIndex = index;
                    index++;
                }                                
            }

            lstGridRegionLine.Items.AddRange(m_lstLineText.ToArray());
            lstGridRegionLine.Items.Add("Null");

            if(selectedIndex >= 0 && selectedIndex < lstGridRegionLine.Items.Count)
                lstGridRegionLine.SelectedIndex = selectedIndex;
        }

        public List<string> LineListText
        {
            get { return m_lstLineText; }
            set { m_lstLineText = value; }
        }

        public List<IDNMcLineItem> LineListValue
        {
            get { return m_lstLineValue; }
            set { m_lstLineValue = value; }
        }

      
    }
}