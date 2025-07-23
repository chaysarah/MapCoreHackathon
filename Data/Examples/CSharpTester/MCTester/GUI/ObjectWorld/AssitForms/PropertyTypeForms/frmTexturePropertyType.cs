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
    public partial class frmTexturePropertyType : frmBasePropertyType
    {
        public frmTexturePropertyType():base()
        {
            InitializeComponent();
        }

        public frmTexturePropertyType(uint id):base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmTexturePropertyType(uint id, IDNMcTexture value)
            : this(id)
        {
            SelectedTexture = value;
            ctrlTextureButtons1.ChangeButtonsEnabled();
        }

        public frmTexturePropertyType(IDNMcTexture value) : this()
        {
            SelectedTexture = value;
            ctrlTextureButtons1.ChangeButtonsEnabled();
            base.HideIDControls();
            this.Text = "Texture Form";
        }

        public IDNMcTexture SelectedTexture
        {
            get { return ctrlTextureButtons1.GetTexture(); }
            set { ctrlTextureButtons1.SetTexture(value); }
        }


 
        
        
        
    }
}