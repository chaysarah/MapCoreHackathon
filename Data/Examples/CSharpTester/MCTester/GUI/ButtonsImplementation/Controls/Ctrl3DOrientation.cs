using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class Ctrl3DOrientation : UserControl
    {
        public Ctrl3DOrientation()
        {
            InitializeComponent();
        }

        public float Yaw
        {
            get { return ntxYaw.GetFloat(); }
            set { ntxYaw.SetFloat(value); }
        }

        public float Pitch
        {
            get { return ntxPitch.GetFloat(); }
            set { ntxPitch.SetFloat(value); }
        }

        public float Roll
        {
            get { return ntxRoll.GetFloat(); }
            set { ntxRoll.SetFloat(value); }
        }
    }
}
