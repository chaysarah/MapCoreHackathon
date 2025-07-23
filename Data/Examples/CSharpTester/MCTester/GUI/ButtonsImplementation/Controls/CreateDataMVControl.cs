using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.General_Forms;
using MCTester.GUI.Map;

namespace MCTester.Controls
{
    public partial class CreateDataMVControl : CreateDataSQControl
    {
        private MCTMapForm m_mapForm;

        public CreateDataMVControl()
            : base()
        {
            InitializeComponent();
            
            cmbMapType.Items.AddRange(Enum.GetNames(typeof(DNEMapType)));
            cmbMapType.Text = DNEMapType._EMT_2D.ToString();
            cmbDtmUsageAndPrecision.Items.AddRange(Enum.GetNames(typeof(DNEQueryPrecision)));
            cmbDtmUsageAndPrecision.Text = DNEQueryPrecision._EQP_DEFAULT.ToString();
        }

        public new DNSCreateDataMV CreateData
        {
            get
            {
                m_mapForm = new MCTMapForm(chxIsWpfMapWin.Checked);
                DNSCreateDataMV ret = new DNSCreateDataMV((DNEMapType)Enum.Parse(typeof(DNEMapType), cmbMapType.Text));
                
                ret.pDevice = base.CreateData.pDevice;
                ret.pOverlayManager = base.CreateData.pOverlayManager;
                ret.CoordinateSystem = base.CreateData.CoordinateSystem;
                ret.uViewportID = base.CreateData.uViewportID;
                
                ret.hWnd = MapForm.MapPointer;
                if (ret.hWnd == IntPtr.Zero)
                {
                    ret.uWidth = (uint)MapForm.Width;
                    ret.uHeight = (uint)MapForm.Height;
                }

                ret.ePixelFormat = DNEPixelFormat._EPF_UNKNOWN;
                ret.pGrid = null;
                ret.pImageCalc = ctrlImageCalcExistingList.ImageCalc;
                ret.pStereoSlaveImageCalc = ctrlSlaveImageCalcExistingList.ImageCalc;
                ret.pStereoRenderCallback = m_mapForm;
                ret.bShowGeoInMetricProportion = chxShowGeoInMetricProportion.Checked;
                ret.eDtmUsageAndPrecision = (DNEQueryPrecision)Enum.Parse(typeof(DNEQueryPrecision), cmbDtmUsageAndPrecision.Text);
                ret.fTerrainObjectsBestResolution = ntxTerrainObjectBestResolution.GetFloat();
                ret.fTerrainResolutionFactor = ntxTerrainResolutionFactor.GetFloat();
                ret.bEnableGLQuadBufferStereo = chxMVEnableGLQuadBufferStereo.Checked;
            //    ret.bTerrainObjectsCache = chxTerrainObjectsCache.Checked;

                return ret;
            }
        }

        public MCTMapForm MapForm
        {
            get { return m_mapForm; }
        }
    }
}
