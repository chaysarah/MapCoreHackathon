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
    public partial class frmRotationPropertyType : frmBasePropertyType
    {
        private DNSMcRotation m_Rotaition;

        public frmRotationPropertyType(): base()
        {
            InitializeComponent();
        }

        public frmRotationPropertyType(uint id)
            : base(id)
        {
            base.ID = id;
            InitializeComponent();
        }

        public frmRotationPropertyType(uint id,DNSMcRotation value)
            : this(id)
        {
            Rotation = value;
        }

        public DNSMcRotation Rotation
        {
            get 
            {
                m_Rotaition.fYaw = ctrl3DOrientation.Yaw;
                m_Rotaition.fPitch = ctrl3DOrientation.Pitch;
                m_Rotaition.fRoll = ctrl3DOrientation.Roll;
                m_Rotaition.bRelativeToCurrOrientation = chkRelativeToCurrOrientation.Checked;
                
                return m_Rotaition;
            }
            set
            {
                m_Rotaition = value;
                ctrl3DOrientation.Yaw = m_Rotaition.fYaw;
                ctrl3DOrientation.Pitch = m_Rotaition.fPitch;
                ctrl3DOrientation.Roll =m_Rotaition.fRoll ;
                chkRelativeToCurrOrientation.Checked=  m_Rotaition.bRelativeToCurrOrientation;
            }
        }
    }
}