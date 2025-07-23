using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms;
using MapCore;

namespace MCTester.ObjectWorld.Assit_Forms.PropertyTypeForms
{
    public partial class frmVector3DArrayPropertyType : frmBasePropertyType
    {
        public frmVector3DArrayPropertyType()
        {
            InitializeComponent();
        }
        public frmVector3DArrayPropertyType(uint id):base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

         public frmVector3DArrayPropertyType(uint id, DNSArrayProperty<DNSMcVector3D> value)
            : this(id)
        {
            Vector3DArrayPropertyValue = value;
        }

         public frmVector3DArrayPropertyType(uint id, DNSArrayProperty<DNSMcFVector3D> value)
             : this(id)
         {
             FVector3DArrayPropertyValue = value;
         }

         public DNSArrayProperty<DNSMcVector3D> Vector3DArrayPropertyValue
         {
             get
             {
                 return ctrlVector3DArray.Vector3DArrayPropertyValue;
             }
             set
             {
                 ctrlVector3DArray.Vector3DArrayPropertyValue = value;
             }
         }

         public DNSArrayProperty<DNSMcFVector3D> FVector3DArrayPropertyValue
         {
             get
             {
                 return ctrlVector3DArray.FVector3DArrayPropertyValue;
             }
             set
             {
                 ctrlVector3DArray.FVector3DArrayPropertyValue = value;
             }
         }
    }
}
