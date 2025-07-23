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
    public partial class frmVector2DPropertyType : frmBasePropertyType
    {
        public frmVector2DPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmVector2DPropertyType(uint id):base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmVector2DPropertyType(uint id, DNSMcVector2D value)
            : this(id)
        {
            SetVector2D(value);
        }

        public DNSMcVector2D GetVector2D()
        {
            return ctrl2DVectorPropertyValue.GetVector2D();
        }

        public void SetVector2D(DNSMcVector2D value)
        {
            ctrl2DVectorPropertyValue.SetVector2D(value);
        }

    }
}