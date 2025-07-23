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
    public partial class frmMeshPropertyType : frmBasePropertyType
    {        
        public frmMeshPropertyType():base()
        {
            InitializeComponent();
        }

        public frmMeshPropertyType(uint id):base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmMeshPropertyType(uint id,IDNMcMesh value):this(id)
        {
            SelectedMesh = value;
        }
        
        public IDNMcMesh SelectedMesh
        {
            get { return ctrlMeshButtons1.GetMesh(); }
            set { ctrlMeshButtons1.SetMesh(value);}
        }


    }
}