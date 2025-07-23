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
    public partial class frmVector3DPropertyType : frmBasePropertyType
    {
        public frmVector3DPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmVector3DPropertyType(uint id)
            : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmVector3DPropertyType(uint id,DNSMcVector3D value)
            : this(id)
        {
            SetVector3D(value);
        }

        public DNSMcVector3D GetVector3D(){ return ctrl3DVectorPropertyValue.GetVector3D(); }

        public void SetVector3D(DNSMcVector3D value) { ctrl3DVectorPropertyValue.SetVector3D(value); }
      
    }
}