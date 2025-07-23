using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using System.IO;
using MCTester.GUI.Map;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers;

namespace MCTester.ButtonsImplementation
{
    public partial class tsmiMosaicsCreatorForm : Form
    {
        public tsmiMosaicsCreatorForm()
        {
            InitializeComponent();
        }        

        private void btnDisplayMosaics_Click(object sender, EventArgs e)
        {
            if (ctrlBrowseDTMLayerDir.FileName == "" ||
                    ctrlBrowseSourceFramesDirectory.FileName == "" ||
                    ctrlBrowseDestinationMosaicsDir.FileName == "" ||
                    ctrlGridCoordinateSystem.GridCoordinateSystem == null)
            {
                MessageBox.Show("Please fill all parameters", "Missing Parameters", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            IDNMcDtmMapLayer mosaicsDTM = DNMcNativeDtmMapLayer.Create(ctrlBrowseDTMLayerDir.FileName);
            IDNMcOverlayManager overlayManager = Manager_MCOverlayManager.CreateOverlayManager(ctrlGridCoordinateSystem.GridCoordinateSystem);

            string[] allFileNames = Directory.GetFiles(ctrlBrowseSourceFramesDirectory.FileName);
            List<string> lImageNames = new List<string>();

            foreach (string fileName in allFileNames)
            {
                if (Path.GetExtension(fileName) != ".txt")
                    lImageNames.Add(fileName);                
            }            

//             Neutralized (removed from API)
//             IDNMcCTACMosaicCreator CTACMosaicCreator = null;
//             try
//             {
//                 CTACMosaicCreator = DNMcCTACMosaicCreator.Create(mosaicsDTM,
//                                                                  ctrlGridCoordinateSystem.GridCoordinateSystem);
//             }
//             catch (MapCoreException McEx)
//             {
//                 MapCore.Common.Utilities.ShowErrorMessage("DNMcCTACMosaicCreator.Create", McEx);
//                 return;
//             }
// 
//             try
//             {
//                 if (CTACMosaicCreator != null)
//                 {
//                     CTACMosaicCreator.CreateMosaic(ctrlBrowseDestinationMosaicsDir.FileName,
//                                                     ntxPixelNumX.GetUInt32(),
//                                                     ntxPixelNumY.GetUInt32(),
//                                                     ntxOverlapPercentage.GetDouble());
//                 }
//             }
//             catch (MapCoreException McEx)
//             {
//                 MapCore.Common.Utilities.ShowErrorMessage("CreateMosaics", McEx);
//                 return;
//             }

            // open new viewport for each created frame picture 
            string[] mosaicParamsFileNames = Directory.GetFiles(ctrlBrowseDestinationMosaicsDir.FileName, "*.txt");

            IDNMcMapViewport frameViewport = null;
            IDNMcMapCamera mapCamera = null;
            MCTMapForm NewMapForm = null;
            StreamReader str = null;
            DNSCameraParams cameraParams;
            string readData = "";
            IDNMcMapTerrain[] terrains = new IDNMcMapTerrain[1];
            IDNMcMapLayer[] layers = new IDNMcMapLayer[1];
            
            char[] trimChar = new char[3];
            trimChar[0] = 't';
            trimChar[1] = 'x';
            trimChar[2] = 't';

            DNSCreateDataMV createData = new DNSCreateDataMV(DNEMapType._EMT_2D);
            createData.hWnd = NewMapForm.MapPointer;
            if (createData.hWnd == IntPtr.Zero)
            {
                createData.uWidth = (uint)NewMapForm.Width;
                createData.uHeight = (uint)NewMapForm.Height;
            }
            createData.bFullScreen = false;
            createData.CoordinateSystem = ctrlGridCoordinateSystem.GridCoordinateSystem;
            createData.pGrid = null;
            createData.pDevice = DNMcMapDevice.Create(new DNSInitParams());
            createData.pOverlayManager = overlayManager;

            IDNMcImageCalc imageCalc = null;
            string layerPath = "";
            uint vpID = 0;
            foreach (string mosaicParamsFile in mosaicParamsFileNames)
            {
                NewMapForm = new MCTMapForm(chxOpenOnWPFWindow.Checked);


                str = new StreamReader(mosaicParamsFile);
                cameraParams = new DNSCameraParams();

                readData = str.ReadLine();
                cameraParams.CameraPosition.x = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.CameraPosition.y = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.CameraPosition.z = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.dCameraOpeningAngleX = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.dCameraYaw = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.dCameraPitch = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.dCameraRoll = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.dPixelRatio = double.Parse(readData);

                readData = str.ReadLine();
                cameraParams.nPixelesNumX = int.Parse(readData);

                readData = str.ReadLine();
                cameraParams.nPixelesNumY = int.Parse(readData);

                cameraParams.dOffsetCenterPixelX = 0;
                cameraParams.dOffsetCenterPixelY = 0;

                try
                {
                    imageCalc = DNMcFrameImageCalc.Create(ref cameraParams,
                                                            mosaicsDTM,
                                                            ctrlGridCoordinateSystem.GridCoordinateSystem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcFrameImageCalc.Create", McEx);
                    return;
                }

                Manager_MCImageCalc.AddToDictionary(imageCalc);

                createData.uViewportID = vpID++;
                createData.pImageCalc = imageCalc;

                layerPath = mosaicParamsFile.TrimEnd(trimChar) + "tif";

                List<DNSComponentParams> lComponentParams = new List<DNSComponentParams>(1);
                uint[] pyramidResolution = new uint[] { 0 };

                DNSComponentParams compParam = new DNSComponentParams();
                compParam.eType = DNEComponentType._ECT_FILE;
                compParam.strName = layerPath;
                compParam.WorldLimit = new DNSMcBox(0, 0, 0, 0, 0, 0);

                lComponentParams.Add(compParam);
                DNSRawParams dnParams = new DNSRawParams();
                dnParams.pCoordinateSystem = ctrlGridCoordinateSystem.GridCoordinateSystem;
                dnParams.strDirectory = "";
                dnParams.aComponents = lComponentParams.ToArray();
                dnParams.auPyramidResolutions = pyramidResolution;

                layers[0] = Manager_MCLayers.CreateRawRasterLayer(dnParams, true,null);
                terrains[0] = Manager_MCTerrain.CreateTerrain(ctrlGridCoordinateSystem.GridCoordinateSystem,
                                                                  layers);
                try
                {
                    DNMcMapViewport.Create(ref frameViewport,
                                                ref mapCamera,
                                                createData,
                                                terrains);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcMapViewport.Create", McEx);
                    return;
                }

                Manager_MCViewports.AddViewport(frameViewport);
            }
        }
    }
}
