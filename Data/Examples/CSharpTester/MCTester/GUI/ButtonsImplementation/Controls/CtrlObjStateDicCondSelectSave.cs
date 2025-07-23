using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;

namespace MCTester.Controls
{
    public partial class CtrlObjStateDicCondSelectSave : UserControl
    {
        public CtrlObjStateDicCondSelectSave()
        {
            InitializeComponent();
        }

        public IDNMcObjectSchemeNode CurrObject
        {
            set
            {
                ctrlObjStatePropertyDicConditionalSelector.CurrObject = value;
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            ctrlObjStatePropertyDicConditionalSelector.Save();
        }

        public new void Load(IDNMcObjectSchemeNode CurrObject)
        {
            ctrlObjStatePropertyDicConditionalSelector.Load(CurrObject);
        }
    }
}
