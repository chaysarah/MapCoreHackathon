using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MCTester.Managers.MapWorld;
using MCTester.General_Forms;
using MCTester.Managers;

namespace MCTester.Controls
{
    public partial class CtrlRaw3DModelParams : UserControl
    {
        private List<DNSMcKeyStringValue> m_RequestParams;
        private bool m_isReadOnly = false;

        public CtrlRaw3DModelParams()
        {
            InitializeComponent();
            chxWithIndexing_CheckedChanged(null, null);
            SetRaw3DModelParams(new MCTRaw3DModelParams());

            chxWithIndexing.Checked = false;

        }

        internal void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
            ctrlBuildIndexingDataParams1.IsReadOnly(isReadOnly);
        }

        public bool CheckRaw3DModelValidity()
        {
            return ctrlBuildIndexingDataParams1.CheckValidity();
        }

        public MCTRaw3DModelParams GetRaw3DModelParams()
        {
            MCTRaw3DModelParams raw3DModelParams = ctrlBuildIndexingDataParams1.GetRaw3DModelParams();
            raw3DModelParams.IsUseIndexing = chxWithIndexing.Checked;
            raw3DModelParams.OrthometricHeights = chxOrthometricHeights.Checked;
            raw3DModelParams.PositionOffset = ctrlPositionOffset.GetVector3D();
            raw3DModelParams.aRequestParams = m_RequestParams == null? null : m_RequestParams.ToArray();
            return raw3DModelParams;
        }

        public void SetRaw3DModelParams(MCTRaw3DModelParams raw3DModelParams)
        {
            if (raw3DModelParams != null)
            {
                chxOrthometricHeights.Checked = raw3DModelParams.OrthometricHeights;
                chxWithIndexing.Checked = raw3DModelParams.IsUseIndexing;
                ctrlPositionOffset.SetVector3D(raw3DModelParams.PositionOffset);
                ctrlBuildIndexingDataParams1.SetRaw3DModelParams(raw3DModelParams);
                m_RequestParams = raw3DModelParams.aRequestParams == null ? null : raw3DModelParams.aRequestParams.ToList();

            }
        }

        private void chxWithIndexing_CheckedChanged(object sender, EventArgs e)
        {
            ctrlBuildIndexingDataParams1.SetUIIndexing(chxWithIndexing.Checked);
            btnServerRequestParams.Enabled = !chxWithIndexing.Checked;

        }

        private void btnServerRequestParams_Click(object sender, EventArgs e)
        {
            frmRequestParams frmKeyValueArray1 = new frmRequestParams(m_RequestParams, "Server Request Params");
            if (m_isReadOnly)
                GeneralFuncs.SetControlsReadonly(frmKeyValueArray1);
            frmKeyValueArray1.VisibleCSWParams(false);
            if (frmKeyValueArray1.ShowDialog() == DialogResult.OK)
            {
                m_RequestParams = frmKeyValueArray1.GetMcKeyStringValues();
            }
        }
    }
}
