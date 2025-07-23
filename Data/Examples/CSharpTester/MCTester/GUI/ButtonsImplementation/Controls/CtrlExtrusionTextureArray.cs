using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapCore;
using MCTester.General_Forms;
using System.Windows.Forms;
using MCTester.Managers;

namespace MCTester.Controls
{
    public partial class CtrlExtrusionTextureArray : UserControl
    {

        List<DNSExtrusionTexture> mExtrusionTextures = new List<DNSExtrusionTexture>();
        private bool m_isReadOnly = false;

        public CtrlExtrusionTextureArray()
        {
            InitializeComponent();
        }

        internal void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
            GeneralFuncs.SetControlsReadonly(this);

        }

        private void dgvSpecificTextures_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DNSExtrusionTexture extrusionTexture = new DNSExtrusionTexture();

                if (mExtrusionTextures.Count > e.RowIndex)
                    extrusionTexture = mExtrusionTextures[e.RowIndex];
                frmSExtrusionTexture frmExtrusionTexture = new frmSExtrusionTexture(extrusionTexture);
                if(m_isReadOnly)
                    GeneralFuncs.SetControlsReadonly(frmExtrusionTexture);

                if (frmExtrusionTexture.ShowDialog() == DialogResult.OK && !m_isReadOnly)
                {
                    extrusionTexture = frmExtrusionTexture.GetExtrusionTexture();
                    if (mExtrusionTextures.Count - 1 < e.RowIndex)
                        mExtrusionTextures.Add(extrusionTexture);
                    else
                        mExtrusionTextures[e.RowIndex] = extrusionTexture;
                    if (dgvSpecificTextures.RowCount == mExtrusionTextures.Count)
                        dgvSpecificTextures.Rows.Add();
                    dgvSpecificTextures[e.ColumnIndex, e.RowIndex].Value = "Value";
                }
            }
        }

        public void SetExtrusionTextures(DNSExtrusionTexture[] extrusionTextures)
        {
            if (extrusionTextures != null && extrusionTextures.Length > 0)
            {
                mExtrusionTextures = new List<DNSExtrusionTexture>(extrusionTextures);
                dgvSpecificTextures.Rows.Add(mExtrusionTextures.Count);
            }
            int rowIndex = 0;
            foreach (DNSExtrusionTexture extrusionTexture in mExtrusionTextures)
            {
                dgvSpecificTextures[0, rowIndex].Value = "Value";
                rowIndex++;
            }
        }

        public List<DNSExtrusionTexture> GetExtrusionTextures()
        {
            return mExtrusionTextures;
        }


    }
}
