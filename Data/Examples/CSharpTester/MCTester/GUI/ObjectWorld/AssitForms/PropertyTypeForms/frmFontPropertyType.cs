using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.General_Forms;
using UnmanagedWrapper;

namespace MCTester.ObjectWorld.ObjectsUserControls.PropertyTypeForms
{
    public partial class frmFontPropertyType : frmBasePropertyType
    {
        public frmFontPropertyType()
            : base()
        {
            InitializeComponent();
        }

        public frmFontPropertyType(uint id) : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmFontPropertyType(uint id, IDNMcFont value)
            : this(id)
        {
            McFont = value;
        }

        public IDNMcFont McFont
        {
            get { return ctrlFontButtons1.GetFont(); }
            set { ctrlFontButtons1.SetFont(value); }
        }

      
    }
}