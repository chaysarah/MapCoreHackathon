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
    public partial class frmFVector3DPropertyType : frmBasePropertyType
    {
        public frmFVector3DPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmFVector3DPropertyType(uint id):base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmFVector3DPropertyType(uint id,DNSMcFVector3D value)
            : this(id)
        {
            FVector3D = value;
        }

        public DNSMcFVector3D FVector3D
        {
            get { return ctrl3DFVectorPropertyValue.GetVector3D(); }
            set { ctrl3DFVectorPropertyValue.SetVector3D( value); }
        }
    }
}