using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using MCTester.ObjectWorld.ObjectsUserControls;

namespace MCTester.Controls
{
    public partial class CtrlPropertyMesh : CtrlPropertyButton
    {
        private IDNMcMesh m_RegMesh;

        private IDNMcMesh m_SelMesh;    

        public CtrlPropertyMesh()
        {
            InitializeComponent();
        }

        protected override void btnRegFunction_Click(object sender, EventArgs e)
        {
            /*m_RegCreatedMesh = new frmCreateMesh(m_RegMesh);
            if (m_RegCreatedMesh.ShowDialog() == DialogResult.OK)
            {
                RegPropertyMesh = m_RegCreatedMesh.CurrentMesh;
            }*/
        }

        protected override void btnSelFunction_Click(object sender, EventArgs e)
        {
            /*m_SelCreatedMesh = new frmCreateMesh(m_SelMesh);
            if (m_SelCreatedMesh.ShowDialog() == DialogResult.OK)
            {
                m_SelMesh = m_SelCreatedMesh.CurrentMesh;
            }*/
        }

        public IDNMcMesh RegPropertyMesh
        {
            get
            {
                if (chxRegNone.Checked == true)
                    m_RegMesh = null;
                return m_RegMesh;
            }
            set
            {
                m_RegMesh = value;
                if (value == null)
                    chxRegNone.Checked = true;
                else
                {
                    chxRegNone.Checked = false;
                    //m_RegCreatedMesh = new frmCreateMesh(m_RegMesh);
                }
            }
        }

        public IDNMcMesh SelPropertyMesh
        {
            get
            {
                if (chxSelNone.Checked == true)
                    m_SelMesh = null;
                return m_SelMesh;
            }
            set
            {
                m_SelMesh = value;
                if (value == null)
                    chxSelNone.Checked = true;
                else
                {
                    chxSelNone.Checked = false;
                    //m_SelCreatedMesh = new frmCreateMesh(m_SelMesh);
                }
            }
        }

        private void chxRegNone_CheckedChanged(object sender, EventArgs e)
        {
            if (chxRegNone.Checked == true)
            {
                this.RegButtonObj.Enabled = false;
                RegPropertyMesh = null;
            }
            else
            {
                this.RegButtonObj.Enabled = true;
            }   
        }

        private void chxSelNone_CheckedChanged(object sender, EventArgs e)
        {
            if (chxSelNone.Checked == true)
            {
                this.SelButtonObj.Enabled = false;
                SelPropertyMesh = null;
            }
            else
            {
                this.SelButtonObj.Enabled = true;
            }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }
    }
}
