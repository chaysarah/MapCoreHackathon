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
    public partial class frmBoolPropertyType : frmBasePropertyType
    {
        public frmBoolPropertyType():base()
        {
            InitializeComponent();
        }
        public frmBoolPropertyType(uint id):base(id)
        {
            InitializeComponent();
            base.ID = id;
        }

        public frmBoolPropertyType(uint id, bool value)
            : this(id)
        {
            IsChecked = value;
        }

        public bool IsChecked
        {
            get { return chxBoolPropertyValue.Checked; }
            set { chxBoolPropertyValue.Checked = value; }
        }
                
    }
}