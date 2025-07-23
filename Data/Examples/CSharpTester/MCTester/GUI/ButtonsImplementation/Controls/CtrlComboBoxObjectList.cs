using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.Reflection;
using MCTester.Managers;

namespace MCTester.Controls
{
    public partial class CtrlComboBoxObjectList : UserControl
    {
        //IDNMcObjectSchemeNode m_CurrNode;
        private List<string> m_ObjectText = new List<string>();
        private List<IDNMcObject> m_ObjectValue = new List<IDNMcObject>();

        public CtrlComboBoxObjectList()
        {
            InitializeComponent();
        }

        public void InitializeControlParams(IDNMcObjectSchemeNode schemeNode)
        {
            cmbObjectsList.DisplayMember = "ObjectText";
            cmbObjectsList.ValueMember = "ObjectValue";

            if (schemeNode != null && schemeNode.GetScheme() != null)
            {
                IDNMcObjectScheme scheme = schemeNode.GetScheme();
                IDNMcObject[] objects = scheme.GetObjects();
                foreach (IDNMcObject obj in objects)
                {
                    m_ObjectText.Add(Manager_MCNames.GetNameByObject(obj,"Object"));
                    m_ObjectValue.Add(obj);
                }

                cmbObjectsList.Items.AddRange(m_ObjectText.ToArray());
            } 
        }

        public IDNMcObject GetSelectedObject()
        {
            if (cmbObjectsList.SelectedIndex != -1)
                return m_ObjectValue[cmbObjectsList.SelectedIndex];
            else
                return null;
        }

        public List<string> GetObjectText()
        {
            return m_ObjectText;
        }

        public void ObjectText (List<string> value)
        {
            m_ObjectText = value; 
        }

        public List<IDNMcObject> GetObjectValue()
        {
            return m_ObjectValue;
        }
        public void SetObjectValue(List<IDNMcObject> value)
        {
            m_ObjectValue = value; 
        }

        //public void SetObjectValue(List<IDNMcObject> obj)
        //{
        //    m_ObjectValue = obj;
        //}
    }
}
