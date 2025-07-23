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
using MCTester.MapWorld.Assist_Forms;
using MCTester.MapWorld.WizardForms;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class CtrlNonNativeParams : UserControl
    {
        private DNSTilingScheme mcTilingScheme = null;
        private ISetTilingScheme m_SetTilingSchemeForm;
        private bool m_isReadOnly = false;

        public void SetTilingSchemeForm(ISetTilingScheme form)
        {
            m_SetTilingSchemeForm = form;
        }

        public CtrlNonNativeParams()
        {
            InitializeComponent();

            ctrlSelectTransparentColor.EnabledButtons(true);
            ctrlSelectTransparentColor.nudAlpha.Value = 0;
        }

        public void SetNonNativeParamsFromWeb(DNSNonNativeParams nonNativeParams)
        {
            //ctrlLayerGridCoordinateSystem.Enabled = false;
            SetNonNativeParams(nonNativeParams);
        }

        public void GetNonNativeParams(DNSNonNativeParams nonNativeParams)
        {
            nonNativeParams.pCoordinateSystem = ctrlLayerGridCoordinateSystem.GridCoordinateSystem;
            nonNativeParams.fMaxScale = ntbMaxScale.GetFloat();
            nonNativeParams.bResolveOverlapConflicts = chxResolveOverlapConflicts.Checked;
            nonNativeParams.bEnhanceBorderOverlap = chxEnhanceBorderOverlap.Checked;
            nonNativeParams.TransparentColor = ctrlSelectTransparentColor.BColor;
            nonNativeParams.byTransparentColorPrecision = ntbTransparentColorPrecision.GetByte();
            nonNativeParams.bFillEmptyTilesByLowerResolutionTiles = chxFillEmptyTilesByLowerResolutionTiles.Checked;
            nonNativeParams.pTilingScheme = mcTilingScheme;

            // return nonNativeParams;
        }

       
        /*public void SetNonNativeParams()
        {
            SetNonNativeParams(Manager_MCLayers.mcCurrSNonNativeParams);
        }*/

        public void SetNonNativeParams(DNSNonNativeParams nonNativeParams)
        {
            ntbMaxScale.SetFloat(nonNativeParams.fMaxScale);
            chxResolveOverlapConflicts.Checked = nonNativeParams.bResolveOverlapConflicts;
            chxEnhanceBorderOverlap.Checked = nonNativeParams.bEnhanceBorderOverlap;
            chxFillEmptyTilesByLowerResolutionTiles.Checked = nonNativeParams.bFillEmptyTilesByLowerResolutionTiles;
            ctrlSelectTransparentColor.BColor = nonNativeParams.TransparentColor;
            ntbTransparentColorPrecision.SetByte(nonNativeParams.byTransparentColorPrecision);
            ctrlLayerGridCoordinateSystem.GridCoordinateSystem = nonNativeParams.pCoordinateSystem;
            mcTilingScheme = nonNativeParams.pTilingScheme;
        }

        internal void IsReadOnly(bool isReadOnly)
        {
            m_isReadOnly = isReadOnly;
        }

        internal void SetUI(bool isCSWMapLayerType)
        {
            btnTilingScheme1.Visible = panel1.Visible = !isCSWMapLayerType;
        }

        public IDNMcGridCoordinateSystem GetGridCoordinateSystem()
        {
            return ctrlLayerGridCoordinateSystem.GridCoordinateSystem;
        }

        public DNSTilingScheme GetTilingScheme()
        {
            return mcTilingScheme;
        }

        public void TransparentColorEnabledButtons(bool isEnabled)
        {
            ctrlSelectTransparentColor.EnabledButtons(isEnabled);
        }

        public void TransparentColorPrecisionEnabled(bool isEnabled)
        {
            ntbTransparentColorPrecision.Enabled = isEnabled;
        }

        private void chxFillEmptyTilesByLowerResolutionTiles_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnTilingScheme1_Click(object sender, EventArgs e)
        {
            frmSTilingSchemeParams frmSTilingSchemeParams = new frmSTilingSchemeParams();
            if (mcTilingScheme != null)
                frmSTilingSchemeParams.STilingScheme = mcTilingScheme;
            frmSTilingSchemeParams.SetControlsReadonly(m_isReadOnly);

            if (frmSTilingSchemeParams.ShowDialog() == DialogResult.OK)
            {
                mcTilingScheme = frmSTilingSchemeParams.STilingScheme;

                if (m_SetTilingSchemeForm != null)
                {
                    m_SetTilingSchemeForm.SetTilingScheme(mcTilingScheme);
                }
            }
        }

        public void SetTilingScheme(DNSTilingScheme tilingScheme)
        {
            mcTilingScheme = tilingScheme;
        }

        internal void SetGridCoordinateSystem(IDNMcGridCoordinateSystem gridCoordinateSystem)
        {
            ctrlLayerGridCoordinateSystem.GridCoordinateSystem = gridCoordinateSystem;
        }

        internal void SetTilingSchemeVisiblity(bool visiblity)
        {
            btnTilingScheme1.Visible = visiblity;
        }
    }
}
