using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms;

namespace MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms
{
    public partial class frmSubItemsDataPropertyType : frmBasePropertyType
    {
        public frmSubItemsDataPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmSubItemsDataPropertyType(uint id)
            : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmSubItemsDataPropertyType(uint id, DNSArrayProperty<DNSMcSubItemData> value)
            : this(id)
        {
            SubItemsDataPropertyValue = value;
        }

        public DNSArrayProperty<DNSMcSubItemData> SubItemsDataPropertyValue
        {
            get
            {
                return ctrlSubItemsData1.SubItemsData;
            }
            set
            {
                ctrlSubItemsData1.SubItemsData = value;
            }
        }
    }
}
