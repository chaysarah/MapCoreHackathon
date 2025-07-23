using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers;

namespace MCTester.General_Forms
{
    public partial class frmGetObjectListBaseOnObjectSchemeItem : Form
    {
        private IDNMcObjectSchemeItem m_SchemeItem;
        private IDNMcObject m_SelectedObject;
        private List<string> m_lstObjectsText = new List<string>();
        private List<IDNMcObject> m_lstObjectsValue = new List<IDNMcObject>();

        public frmGetObjectListBaseOnObjectSchemeItem(IDNMcObjectSchemeItem schemeItem)
        {
            InitializeComponent();
            m_SchemeItem = schemeItem;
            m_SelectedObject = null;

            lstObjectsList.DisplayMember = "ObjectsTextList";
            lstObjectsList.ValueMember = "ObjectsValueList";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstObjectsList.SelectedItem != null)
            {
                SelectedObject = m_lstObjectsValue[lstObjectsList.SelectedIndex];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Please select an object from the list above", "Wrong Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmGetObjectListBaseOnObjectSchemeItem_Load(object sender, EventArgs e)
        {
            if (m_SchemeItem.GetScheme() != null)
            {
                IDNMcObject[] objs = m_SchemeItem.GetScheme().GetObjects();
                foreach (IDNMcObject obj in objs)
                {
                    m_lstObjectsText.Add(Manager_MCNames.GetNameByObject(obj, "Object"));
                    m_lstObjectsValue.Add(obj);
                }

                lstObjectsList.Items.AddRange(m_lstObjectsText.ToArray());
            }
        }

        public List<string> ObjectsTextList
        {
            get { return m_lstObjectsText; }
            set { m_lstObjectsText = value; }
        }

        public List<IDNMcObject> ObjectsValueList
        {
            get { return m_lstObjectsValue; }
            set { m_lstObjectsValue = value; }
        }

        public IDNMcObject SelectedObject
        {
            get { return m_SelectedObject; }
            set { m_SelectedObject = value; }
        }
    }
}
