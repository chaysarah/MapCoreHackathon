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
    public partial class frmStringPropertyType : frmBasePropertyType
    {
        public frmStringPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmStringPropertyType(uint id)
            : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmStringPropertyType(uint id, DNMcVariantString value)
            : this(id)
        {
            StringPropertyValue = value;
        }

        public DNMcVariantString StringPropertyValue
        {
            get 
            {
                return ctrlString1.StringVal; 
            }
            set
            {
                ctrlString1.StringVal = value;
            }
        }
    }
}