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
using MapCore.Common;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class CtrlRawResolutions : UserControl
    {
        public CtrlRawResolutions()
        {
            InitializeComponent();
        }

        public void SetResolutions(object mcObject)
        {
            if (mcObject != null)
            {
                try
                {
                    uint[] PyramidResolutions = null;
                    float FirstPyramidResolution = 0;

                    if (mcObject is IDNMcRawMaterialMapLayer)
                        ((IDNMcRawMaterialMapLayer)mcObject).GetResolutions(out PyramidResolutions, out FirstPyramidResolution);
                    else if (mcObject is IDNMcRawTraversabilityMapLayer)
                        ((IDNMcRawTraversabilityMapLayer)mcObject).GetResolutions(out PyramidResolutions, out FirstPyramidResolution);

                    if (PyramidResolutions != null)
                    {
                        foreach (uint resolution in PyramidResolutions)
                            ntxPyramidResolutions.Text += (resolution + " ");
                    }
                    ntxFirstPyramidResolution.SetFloat(FirstPyramidResolution);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetResolutions", McEx);

                }
            }
        }
    }
}
