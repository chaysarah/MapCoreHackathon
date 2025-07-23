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
    public partial class frmAnimationPropertyType : frmBasePropertyType
    {
        public frmAnimationPropertyType():base()
        {
            InitializeComponent();
        }

        public frmAnimationPropertyType(uint id):base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmAnimationPropertyType(uint id,DNSMcAnimation value)
            : this(id)
        {
            AnimationPropertyValue = value;
        }


        public DNSMcAnimation AnimationPropertyValue
        {
            get 
            {
                DNSMcAnimation animation = new DNSMcAnimation(txtAnimationName.ToString(), chxAnimationLoop.Checked);
                return animation;

            }
            set
            {
                txtAnimationName.Text = value.strAnimationName;
                chxAnimationLoop.Checked = value.bLoop;
            }
        }
        
    }
}