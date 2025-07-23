using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    public partial class frmConditionalSelectorPropertyType : frmBasePropertyType
    {
        private IDNMcConditionalSelector m_Selector;
        private IDNMcObjectScheme m_Scheme;

        public frmConditionalSelectorPropertyType():base()
        {
            InitializeComponent();
        }

        public frmConditionalSelectorPropertyType(uint id, IDNMcObjectScheme scheme)
            : base(id)
        {
            base.ID = id;
            m_Scheme = scheme;
            InitializeComponent();
        }

        public frmConditionalSelectorPropertyType(uint id, IDNMcObjectScheme scheme, IDNMcConditionalSelector selector)
            : this(id,scheme)
        {
            PropertySelectorValue = selector;
        }

        private void btnSelectorList_Click(object sender, EventArgs e)
        {
            frmConditionalSelectorList ConditionalSelectorList = new frmConditionalSelectorList(m_Scheme, PropertySelectorValue);
            if (ConditionalSelectorList.ShowDialog() == DialogResult.OK)
            {
                btnSelectorList.Text = "Selected";
                PropertySelectorValue = ConditionalSelectorList.ChosenSelector;
            }
        }

        public IDNMcConditionalSelector PropertySelectorValue
        {
            get {return m_Selector; }
            set { m_Selector = value; }
        }   
    }
}