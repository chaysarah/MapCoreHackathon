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
    public partial class frmFVector2DPropertyType : frmBasePropertyType
    {
        public frmFVector2DPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmFVector2DPropertyType(uint id)
            : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }
        public frmFVector2DPropertyType(uint id,DNSMcFVector2D value)
            : this(id)
        {
            FVector2D = value;
        }

        public DNSMcFVector2D FVector2D
        {
            get { return ctrl2DFVectorPropertyValue.GetVector2D(); }
            set { ctrl2DFVectorPropertyValue.SetVector2D(value); }
        }
    }
}