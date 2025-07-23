using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucPrint : UserControl
    {
        private IDNMcPrintMap m_CurrentObject;
        
        public ucPrint()
        {
            InitializeComponent();
        }

        public void LoadItem(MainForm m_mainForm, object aItem)
        {
            m_CurrentObject = (IDNMcPrintMap)aItem;
        }
    }
}
