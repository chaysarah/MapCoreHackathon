using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.MapWorld;
using MapCore;

namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    public partial class frmColorArrayPropertyType : frmBasePropertyType
    {
        public frmColorArrayPropertyType()
            : base()
        {
            InitializeComponent();
           
        }

        public frmColorArrayPropertyType(uint id)
            : this()
        {
            base.ID = id;
        }

        public frmColorArrayPropertyType(uint id, DNSArrayProperty<DNSMcBColor> value)
            : this(id)
        {
            BColorsPropertyValue = value;
        }

        public DNSArrayProperty<DNSMcBColor> BColorsPropertyValue
        {
            get
            {
                return ctrlColorArray.BColorsPropertyValue;
            }
            set
            {
                ctrlColorArray.BColorsPropertyValue = value;
            }
        }     
    }
}
