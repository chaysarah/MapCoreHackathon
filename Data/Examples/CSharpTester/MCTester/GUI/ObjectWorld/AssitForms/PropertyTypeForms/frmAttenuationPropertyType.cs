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
    public partial class frmAttenuationPropertyType : frmBasePropertyType
    {
        public frmAttenuationPropertyType():base()
        {
            InitializeComponent();
        }

        public frmAttenuationPropertyType(uint id)
            : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmAttenuationPropertyType(uint id,DNSMcAttenuation value)
            : this(id)
        {
            AttenuationPropertyValue = value;
        }

        public DNSMcAttenuation AttenuationPropertyValue
        {
            get
            {
                DNSMcAttenuation Attenuation = new DNSMcAttenuation(ctrlAttenuationType.Attenuation);
                return Attenuation;
            }
            set
            {
                ctrlAttenuationType.Attenuation = value;
            }
        }
    }
}