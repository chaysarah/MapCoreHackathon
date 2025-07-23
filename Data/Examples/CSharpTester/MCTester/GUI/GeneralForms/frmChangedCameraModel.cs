using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.IO;
using MCTester.Managers.MapWorld;

namespace MCTester.General_Forms
{
    public partial class frmChangedCameraModel : Form
    {
        private Timer tmr;
        private IDNMcObject m_ChangedCameraModelObject;
        private IDNMcPictureItem m_PictureItem;
        private string[] BMPFilesNames;
        private string[] TMEFilesNames;
        private List<DNSCameraParams> m_lCameraParams =new List<DNSCameraParams>();
        private IDNMcTexture [] m_ArrTexture;
        private int m_FrameIndex;
        private IDNMcImageCalc m_ImageCalc;

        public frmChangedCameraModel(IDNMcImageCalc imageCalc)
        {
            InitializeComponent();
            tmr = new Timer();
            tmr.Interval = 60;
            tmr.Tick += new EventHandler(tmr_Tick);
                
            m_FrameIndex = 0;
            m_ImageCalc = imageCalc;
            
        }

        void tmr_Tick(object sender, EventArgs e)
        {

            try
            {
                m_PictureItem.SetTexture(m_ArrTexture[m_FrameIndex],
                                            (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID,
                                            false);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTexture", McEx);
            }
            
            try
            {
                ((IDNMcFrameImageCalc)m_ImageCalc).SetCameraParams(m_lCameraParams[m_FrameIndex]);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetCameraParams", McEx);
            }

            if (m_FrameIndex < m_ArrTexture.Length - 1)
                m_FrameIndex++;
            else
                m_FrameIndex = 0;

                
        }

        private void frmChangedCameraModel_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmr.Tick -= new EventHandler(tmr_Tick);
            tmr.Stop();
        }

        private void chxStart_CheckedChanged(object sender, EventArgs e)
        {
            if (chxStart.Checked == true)
            {
                chxStart.Text = "STOP";
                if (ctrlBrowseTextureFilesDirectory.FileName != "")
                {
                    IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null && m_ImageCalc.GetImageType() == DNEImageType._EIT_FRAME)
                    {
                        BMPFilesNames = Directory.GetFiles(ctrlBrowseTextureFilesDirectory.FileName, "*.bmp");
                        m_ArrTexture = new IDNMcTexture[BMPFilesNames.Length];

                        for (int i = 0; i < BMPFilesNames.Length; i++)
                            m_ArrTexture[i] = DNMcImageFileTexture.Create(new DNSMcFileSource(BMPFilesNames[i]), false);

                        TMEFilesNames = Directory.GetFiles(ctrlBrowseTextureFilesDirectory.FileName, "*.cap");

                        for (int i = 0; i < TMEFilesNames.Length; i++)
                        {
                            // create reader & open file
                            System.IO.TextReader tr = new StreamReader(TMEFilesNames[i]);

                            DNSCameraParams imageCameraParams = new DNSCameraParams();
                            imageCameraParams.CameraPosition.x = double.Parse(tr.ReadLine().Trim());
                            imageCameraParams.CameraPosition.y = double.Parse(tr.ReadLine().Trim());
                            imageCameraParams.CameraPosition.z = double.Parse(tr.ReadLine().Trim());
                            imageCameraParams.dCameraYaw = double.Parse(tr.ReadLine().Trim());
                            imageCameraParams.dCameraPitch = double.Parse(tr.ReadLine().Trim());
                            imageCameraParams.dCameraRoll = double.Parse(tr.ReadLine().Trim());
                            imageCameraParams.dCameraOpeningAngleX = double.Parse(tr.ReadLine().Trim());
                            tr.ReadLine(); //CameraOpeningAngleY is unnecessary
                            imageCameraParams.nPixelesNumX = int.Parse(tr.ReadLine().Trim());
                            imageCameraParams.nPixelesNumY = int.Parse(tr.ReadLine().Trim());

                            m_lCameraParams.Add(imageCameraParams);

                            // close the stream
                            tr.Close();
                        }

                        
                        try
                        {
                            m_PictureItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_WORLD, DNEMcPointCoordSystem._EPCS_WORLD,
                                                                    null,
                                                                    1,
                                                                    1,
                                                                    true,
                                                                    DNSMcBColor.bcWhiteTransparent,
                                                                    DNEBoundingRectanglePoint._EBRP_TOP_LEFT);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcPictureItem.Create", McEx);
                        }

                        IDNMcObjectLocation objLocation = null;
                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[1];
                        locationPoints[0].x = 0;
                        locationPoints[0].y = 0;
                        locationPoints[0].z = 0;
                        try
                        {
                            m_ChangedCameraModelObject = DNMcObject.Create(ref objLocation,
                                                                            activeOverlay,
                                                                            DNEMcPointCoordSystem._EPCS_IMAGE,
                                                                            locationPoints,
                                                                            false);
                            
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }

                        try
                        {
                            m_ChangedCameraModelObject.SetImageCalc(m_ImageCalc);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("SetImageCalc", McEx);
                        }

                        try
                        {
                            m_PictureItem.Connect(objLocation);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("Connect", McEx);
                        }


                        this.Opacity = 50;
                        tmr.Start();
                    }
                }
                else
                    MessageBox.Show("Directory file name is missing", "directory file name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                chxStart.Text = "START";
                tmr.Stop();
                this.Opacity = 100;
            }                
        }
    }
}