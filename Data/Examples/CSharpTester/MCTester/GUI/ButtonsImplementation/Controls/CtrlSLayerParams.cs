using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.Controls
{
    public partial class CtrlSLayerParams : UserControl
    {
        public CtrlSLayerParams()
        {
            InitializeComponent();
        }

        public DNSLayerParams LayerParams
        {
            get
            {
                DNSLayerParams m_LayerParams = new DNSLayerParams();
                m_LayerParams.bVisibility = chxVisibility.Checked;
                m_LayerParams.byTransparency = ntxTransparency.GetByte();
                m_LayerParams.fMaxScale = ntxMaxScaleVisibility.GetFloat();
                m_LayerParams.fMinScale = ntxMinScaleVisibility.GetFloat();
                m_LayerParams.nDrawPriority = ntxDrawPriority.GetSByte();
                m_LayerParams.bNearestPixelMagFilter = chbNearestPixelMagFilter.Checked;
                return m_LayerParams;
            }
            set 
            {
                chxVisibility.Checked = value.bVisibility;
                ntxTransparency.SetByte(value.byTransparency);
                ntxMaxScaleVisibility.SetFloat(value.fMaxScale);
                ntxMinScaleVisibility.SetFloat(value.fMinScale);
                ntxDrawPriority.SetSByte(value.nDrawPriority);
                chbNearestPixelMagFilter.Checked = value.bNearestPixelMagFilter;
            }
        }        
    }
}
