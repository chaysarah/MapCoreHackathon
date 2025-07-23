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
    public partial class CtrlPropertyTexture : CtrlPropertyButton
    {
        private IDNMcTexture m_RegTexture;
        private CreateTextureForm m_RegCreatedTexture;

        private IDNMcTexture m_SelTexture;
        private CreateTextureForm m_SelCreatedTexture;

        public CtrlPropertyTexture()
        {
            InitializeComponent();
        }

        protected override void btnRegFunction_Click(object sender, EventArgs e)
        {
            
            m_RegCreatedTexture = new CreateTextureForm();

            m_RegCreatedTexture.TopMost = true;
            if (m_RegCreatedTexture.ShowDialog() == DialogResult.OK)
            {
                m_RegTexture = m_RegCreatedTexture.CurrentTexture;
            }

            if (m_RegTexture != null)
            {
                btnRegEdit.Enabled = true;
            }
            else
            {
                btnSelEdit.Enabled = false;
            }
        }

        protected void btnRegEdit_Click(object sender, EventArgs e)
        {
           /* m_RegCreatedTexture = new CreateTextureForm(m_RegTexture);

            m_RegCreatedTexture.TopMost = true;
            if (m_RegCreatedTexture.ShowDialog() == DialogResult.OK)
            {
                m_RegTexture = m_RegCreatedTexture.CurrentTexture;
            }
            if (m_RegTexture != null)
            {
                btnRegEdit.Enabled = true;
            }
            else
            {
                btnSelEdit.Enabled = false;
            }*/
        }


        public IDNMcTexture RegPropertyTexture
        {
            get 
            {
                if (chxRegNone.Checked == true)
                {
                    m_RegTexture = null;
                }
                return m_RegTexture; 
            }
            set
            {
                RegButtonText = "&Create";
                m_RegTexture = value;
                btnRegEdit.Enabled = (m_RegTexture != null);
                if (value == null)
                    chxRegNone.Checked = true;
                else
                    chxRegNone.Checked = false;
            }
        }
        
        private void chxRegNone_ChackedChanged(object sender, EventArgs e)
        {
            if (chxRegNone.Checked == true)
            {
                this.RegButtonObj.Enabled = false;
                this.btnRegEdit.Enabled = false;
                RegPropertyTexture = null;
            }
            else
            {
                this.RegButtonObj.Enabled = true;
                if (m_RegCreatedTexture != null)
                {
                    this.btnRegEdit.Enabled = true;
                }
            }   
        }


        private void chxSelNone_ChackedChanged(object sender, EventArgs e)
        {
            if (chxSelNone.Checked == true)
            {
                this.SelButtonObj.Enabled = false;
                this.btnRegEdit.Enabled = false;
                SelPropertyTexture = null;
            }
            else
            {
                this.SelButtonObj.Enabled = true;
                if (m_SelCreatedTexture != null)
                {
                    this.btnSelEdit.Enabled = true;
                }
            }

        }

        protected override void btnSelFunction_Click(object sender, EventArgs e)
        {

            m_SelCreatedTexture = new CreateTextureForm();

            m_SelCreatedTexture.TopMost = true;
            if (m_SelCreatedTexture.ShowDialog() == DialogResult.OK)
            {
                m_SelTexture = m_SelCreatedTexture.CurrentTexture;
            }

            if (m_SelTexture != null)
            {
                btnSelEdit.Enabled = true;
            }
            else
            {
                btnSelEdit.Enabled = false;
            }
        }

        private void btnSelEdit_Click(object sender, EventArgs e)
        {
            /*m_SelCreatedTexture = new CreateTextureForm(m_SelTexture);

            m_SelCreatedTexture.TopMost = true;
            if (m_SelCreatedTexture.ShowDialog() == DialogResult.OK)
            {
                m_SelTexture = m_SelCreatedTexture.CurrentTexture;

            }

            if (m_SelTexture != null)
            {
                btnSelEdit.Enabled = true;
            }
            else
            {
                btnSelEdit.Enabled = false;
            }*/
        }

        public IDNMcTexture SelPropertyTexture
        {
            get 
            {
                if (chxSelNone.Checked == true)
                    m_SelTexture = null;
                return m_SelTexture; 
            }
            set
            {
                SelButtonText = "&Create";
                m_SelTexture = value;
                btnSelEdit.Enabled = (m_SelTexture != null);
                if (value == null)
                    chxSelNone.Checked = true;
                else
                    chxSelNone.Checked = false;
            }
        }

        protected override void chxSelectionProperty_CheckedChange(object sender, EventArgs e)
        {
            base.chxSelectionProperty_CheckedChange(sender, e);
        }

        private void tcProperty_Selected(object sender, TabControlEventArgs e)
        {
            if (e.Action == TabControlAction.Selected)
            {
                if (e.TabPage == tpRegular)
                {
                    this.RegButtonObj.Enabled = chxRegNone.Checked;
                    this.btnRegEdit.Enabled = ((m_RegTexture != null) && (!chxRegNone.Checked));
                }
                else if (e.TabPage == tpSelection)
                {
                    this.SelButtonObj.Enabled = chxSelNone.Checked;
                    this.btnSelEdit.Enabled = ((m_SelTexture != null) && (!chxSelNone.Checked));
                }
            }
        }

    }
}
