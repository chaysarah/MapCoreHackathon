using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public partial class frmCameraList : Form
    {
        private IDNMcMapViewport m_Viewport;
        private IDNMcMapCamera m_SelectedCamera;

        public frmCameraList(IDNMcMapViewport viewport)
        {
            InitializeComponent();
            m_Viewport = viewport;
            LoadForm();
        }

        private void LoadForm()
        {
            try
            {
                IDNMcMapCamera[] cameras = m_Viewport.GetCameras();

                foreach (IDNMcMapCamera cam in cameras)
                {
                    lstCameras.Items.Add(cam);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetCameras", McEx);
            }

            try
            {
                IDNMcMapCamera CurrActiveCamera = m_Viewport.ActiveCamera;

                for (int i=0; i<lstCameras.Items.Count; i++)
                {
                    if (CurrActiveCamera == lstCameras.Items[i])
                        lstCameras.SelectedItem = lstCameras.Items[i];
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ActiveCamera", McEx);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedCamera = (IDNMcMapCamera)lstCameras.SelectedItem;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public IDNMcMapCamera SelectedCamera
        {
            get { return m_SelectedCamera; }
            set { m_SelectedCamera = value; }
        }
    }
}