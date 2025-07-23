using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers;

namespace MCTester.General_Forms
{
    public partial class frmObjectListBasedOnSpecificSchemeNode : Form
    {
        private IDNMcObjectScheme m_Scheme;
        private IDNMcObject m_SelectedObject;
        private List<string> m_lstObjectsText = new List<string>();
        private List<IDNMcObject> m_lstObjectsValue = new List<IDNMcObject>();

        public frmObjectListBasedOnSpecificSchemeNode()
        {
            InitializeComponent();
            m_SelectedObject = null;
            lstObjects.DisplayMember = "ObjectsTextList";
            lstObjects.ValueMember = "ObjectsValueList";
        }

        public frmObjectListBasedOnSpecificSchemeNode(IDNMcObjectSchemeNode baseSchemeNode) : this()
        {
            
            m_Scheme = baseSchemeNode.GetScheme();
           
        }

        public frmObjectListBasedOnSpecificSchemeNode(IDNMcObjectScheme baseScheme):this()
        {
            m_Scheme = baseScheme;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lstObjects.SelectedItem != null)
            {
                SelectedObject = m_lstObjectsValue[lstObjects.SelectedIndex];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Please select an object from the list above", "Wrong Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmObjectListBasedOnSpecificSchemeNode_Load(object sender, EventArgs e)
        {
            IDNMcObject[] objs = m_Scheme.GetObjects();
            foreach (IDNMcObject obj in objs)
            {
                m_lstObjectsText.Add(Manager_MCNames.GetNameByObject(obj,"Object") );
                m_lstObjectsValue.Add(obj);
            }

            lstObjects.Items.AddRange(m_lstObjectsText.ToArray());
            
            if (lstObjects.Items.Count > 0)
                lstObjects.SetSelected(0, true);
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
