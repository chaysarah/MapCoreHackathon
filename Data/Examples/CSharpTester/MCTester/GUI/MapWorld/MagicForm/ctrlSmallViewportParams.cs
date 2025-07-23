using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCTester.Managers;
using MapCore;

namespace MCTester.MapWorld.MagicForm
{
    public partial class ctrlSmallViewportParams : UserControl
    {
        public ctrlSmallViewportParams()
        {
            InitializeComponent();

            rdb2D.Checked = true;
            ntxTerrainResolutionFactor.SetFloat(Manager_MCGeneralDefinitions.m_TerrainResolutionFactor);

        }

        public bool GetShowGeoInMetricProportion()
        {
            return cbShowGeoInMetricProportion.Checked;
        }

        public float GetScaleFactor()
        {
            return ntxScaleFactor.GetFloat();
        }
        public float GetTerrainResolutionFactor()
        {
            return ntxTerrainResolutionFactor.GetFloat();
        }

        public IDNMcImageCalc GetImageCalc()
        {
            return ctrlImageCalc1.ImageCalc;
        }

        public bool GetIs2DChecked()
        {
            return rdb2D.Checked;
        }

        public bool GetIs2D3DChecked()
        {
            return rdb2D3D.Checked;
        }

        public bool GetIsWpfWindow()
        {
            return chxIsWpfWindow.Checked;
        }

        public bool GetIsOpenMapWithoutWaitAllLayersInit()
        {
            return chxIsOpenMapWithoutWaitAllLayersInit.Checked;
        }
    }
}
