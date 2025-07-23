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
    public partial class frmNumericArrayPropertyType : frmBasePropertyType
    {
        public frmNumericArrayPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmNumericArrayPropertyType(uint id)
            : this()
        {
            base.ID = id;
        }

        public frmNumericArrayPropertyType(uint id, DNSArrayProperty<uint> value)
            : this(id)
        {
            UIntArrayPropertyValue = value;
        }

        public frmNumericArrayPropertyType(uint id, DNSArrayProperty<int> value)
           : this(id)
        {
            IntArrayPropertyValue = value;
        }

        public void SetHeadersColumns(int numColumns, string headerTextCol1, string headerTextCol2 = "", string headerTextCol3 = "")
        {
            ctrlNumberArray.SetHeadersColumns(numColumns, headerTextCol1, headerTextCol2, headerTextCol3);
        }

        public DNSArrayProperty<uint> UIntArrayPropertyValue
        {
            get
            {
                return ctrlNumberArray.UIntArrayPropertyValue;
            }
            set
            {
                ctrlNumberArray.UIntArrayPropertyValue = value;
            }
        }

        public DNSArrayProperty<int> IntArrayPropertyValue
        {
            get
            {
                return ctrlNumberArray.IntArrayPropertyValue;
            }
            set
            {
                ctrlNumberArray.IntArrayPropertyValue = value;
            }
        }

    }
}
