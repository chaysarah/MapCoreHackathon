using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MapCore;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class ucViewportHandler : UserControl, IUserControlItem
    {
        IDNMcMapViewport m_Viewport;

        public ucViewportHandler()
        {
            InitializeComponent();
        }

        #region IUserControlItem Members

        public void LoadItem(object aItem)
        {
            this.m_Viewport = (IDNMcMapViewport)aItem;
            this.ViewportCtrl.LoadItem(aItem);
            this.CameraCtrl.LoadItem(aItem);
        }


        #endregion

        
    }
}
