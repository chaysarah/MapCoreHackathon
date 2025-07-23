using MapCore;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.MapWorld.WizardForms;
using System;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.ButtonsImplementation
{
    public partial class btnFileProductionForm : Form
    {
        private IDNMcDtmMapLayer m_DtmLayer = null;
        private Random randX;
        private Random randY;

        public btnFileProductionForm()
        {
            InitializeComponent();
            cmbSourceResamplingMethod.Items.AddRange(Enum.GetNames(typeof(DNEResamplingMethod)));
            cmbOrthophotoSourceResamplingMethod.Items.AddRange(Enum.GetNames(typeof(DNEResamplingMethod)));

            cmbSourceResamplingMethod.Text = DNEResamplingMethod._ERM_NEAREST_NEIGHBORHOOD.ToString();
            cmbOrthophotoSourceResamplingMethod.Text = DNEResamplingMethod._ERM_NEAREST_NEIGHBORHOOD.ToString();

            ctrlBrowseSourceOrthophotoTifFiles.MultiFilesSelect = true;
            ctrlBrowseSourceOrthophotoTfwFiles.MultiFilesSelect = true;

            randX = new Random();
            randY = new Random();
        }

        private void btnGetFileParameters_Click(object sender, EventArgs e)
        {
            try
            {
                uint imageWidth = 0;
                uint imageHeight = 0;
                bool isTiled = false;
                uint tileWidth = 0;
                uint tileHeight = 0;
                uint stripHeight = 0;

                IDNMcFileProductions.GetFileParameters(ctrlBrowseSourceFileFullPath.FileName,
                                                        ref imageWidth,
                                                        ref imageHeight,
                                                        ref isTiled,
                                                        ref tileWidth,
                                                        ref tileHeight,
                                                        ref stripHeight);


                ntxImageWidth.SetUInt32(imageWidth);
                ntxImageHeight.SetUInt32(imageHeight);
                cbxIsTiled.Checked = isTiled;
                ntxTileWidth.SetUInt32(tileWidth);
                ntxTileHeight.SetUInt32(tileHeight);
                ntxStripHeight.SetUInt32(stripHeight);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetFileParameters", McEx);
            }
        }

        private void btnGenerateDTMFromPointsCloud_Click(object sender, EventArgs e)
        {
            DNSMcVector3D [] arrCloudPoints = new DNSMcVector3D[dgvCloudPoints.Rows.Count-1];
            bool [] arrUsedCloudPoints = new bool[dgvCloudPoints.Rows.Count-1];


            for (int i = 0; i < arrCloudPoints.Length; i++)
            {
                arrCloudPoints[i].x = double.Parse(dgvCloudPoints[0, i].Value.ToString());
                arrCloudPoints[i].y = double.Parse(dgvCloudPoints[1, i].Value.ToString());
                arrCloudPoints[i].z = double.Parse(dgvCloudPoints[2, i].Value.ToString());
                arrUsedCloudPoints[i] = (dgvCloudPoints[3, i].Value == null) ? false : true;
            }

            try
            {
                IDNMcFileProductions.GenerateDTMFromPointsCloud(arrCloudPoints,
                                                                    arrUsedCloudPoints,
                                                                    arrCloudPoints.Length,
                                                                    ctrlBrowseDTMPath.FileName,
                                                                    null,
                                                                    ntxDTMResolution.GetInt32(),
                                                                    ctrl2DVectorDTMMinPoint.GetVector2D(),
                                                                    ctrl2DVectorDTMMaxPoint.GetVector2D(),
                                                                    ntxMaxSearchRadiusForHeightCalculation.GetDouble(),
                                                                    ntxMinCloudPointsForHeightCalculation.GetInt32());

                MessageBox.Show("Generate DTM From Points Cloud ended successfully", "Generate DTM From Points Cloud", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GenerateDTMFromPointsCloud", McEx);
            }
        }

        private void btnGenerateMosaic_Click(object sender, EventArgs e)
        {
            try
            {
                DNEResamplingMethod cresamplingMethodType = (DNEResamplingMethod)Enum.Parse(typeof(DNEResamplingMethod), cmbSourceResamplingMethod.Text);
                
                DNGeoReferencingParams geoReferencingParams = new DNGeoReferencingParams();
                geoReferencingParams.FirstCornerInDiagonal = ctrl3DVectorFirstCornerInDiagonal.GetVector3D();
                geoReferencingParams.SecondCornerInDiagonal = ctrl3DVectorSecondCornerInDiagonal.GetVector3D();
                geoReferencingParams.dAzimDeg = ntxAzimDeg.GetDouble();
                geoReferencingParams.dGSD = ntxGSD.GetDouble();


                IDNMcFileProductions.GenerateMosaicFromOrthophotos(ctrlBrowseSourceOrthophotoTifFiles.FileNames,
                                                                    ctrlBrowseSourceOrthophotoTfwFiles.FileNames,
                                                                    ctrlGridCoordinateSystemSourceOrthophotoWorldParams.GridCoordinateSystem,
                                                                    (uint)ctrlBrowseSourceOrthophotoTifFiles.FileNames.Length,
                                                                    cresamplingMethodType,
                                                                    geoReferencingParams,
                                                                    ntxMosaicTileSize.GetInt32(),
                                                                    ntxMosaicBackgroundGrayLevel.GetByte(),
                                                                    ctrlGridCoordinateSystemMosaicWorldParams.GridCoordinateSystem,
                                                                    ctrlBrowseMosaicTifFile.FileName,
                                                                    ctrlBrowseControlMosaicTfwFile.FileName);

                MessageBox.Show("Generate Mosaic From Orthophotos ended successfully", "Generate Mosaic From Orthophotos", MessageBoxButtons.OK, MessageBoxIcon.Information);
	
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GenerateMosaicFromOrthophotos", McEx);
            }
        }

        private void btnGenerateOrthophoto_Click(object sender, EventArgs e)
        {
            try
            {
                DNEResamplingMethod cresamplingMethodType = (DNEResamplingMethod)Enum.Parse(typeof(DNEResamplingMethod), cmbOrthophotoSourceResamplingMethod.Text);
                
                DNGeoReferencingParams geoReferencingParams = new DNGeoReferencingParams();
                geoReferencingParams.FirstCornerInDiagonal = ctrl3DVectorOrthophotoFirstCornerInDiagonal.GetVector3D();
                geoReferencingParams.SecondCornerInDiagonal = ctrl3DVectorOrthophotoSecondCornerInDiagonal.GetVector3D();
                geoReferencingParams.dAzimDeg = ntxOrthophotoAzimDeg.GetDouble();
                geoReferencingParams.dGSD = ntxOrthophotoAzimDeg.GetDouble();
                
                IDNMcFileProductions.GenerateOrthophotoFromLoropImage(ctrlBrowseSourceLoropTifFile.FileName,
                                                                        (IDNMcLoropImageCalc)ctrlImageCalcSourceLorop.ImageCalc,
                                                                        cresamplingMethodType,
                                                                        geoReferencingParams,
                                                                        ntxOrthopothoTileSize.GetInt32(),
                                                                        ntxOrthophotoBackgroundGrayLevel.GetByte(),
                                                                        m_DtmLayer,
                                                                        ntxOrthophotoDefaultHeight.GetDouble(),
                                                                        ctrlGridCoordinateSystemOrthophoto.GridCoordinateSystem,
                                                                        ctrlBrowseOrthophotoTifFile.FileName,
                                                                        ctrlBrowseOrthophotoTfwFile.FileName);
            }
            catch (MapCoreException McEx)
            {
            	MapCore.Common.Utilities.ShowErrorMessage("", McEx);
            }
        }

        private void btnAddDTMMapLayer_Click(object sender, EventArgs e)
        {
            AddLayerForm addDtmLayer = new AddLayerForm();
            Manager_MCLayers.AddLayerFormToList(addDtmLayer);

            if (addDtmLayer.ShowDialog() == DialogResult.OK)
            {
                if (addDtmLayer.NewLayer.LayerType == DNELayerType._ELT_NATIVE_DTM || addDtmLayer.NewLayer.LayerType == DNELayerType._ELT_RAW_DTM)
                {
                    btnAddDTMMapLayer.Text = "O.K";
                    cbxDTMMapLayer.Checked = false;
                    m_DtmLayer = (IDNMcDtmMapLayer)addDtmLayer.NewLayer;
                }
                else
                    MessageBox.Show("Wrong Layer Type", "You have to choose DTM layer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Manager_MCLayers.RemoveLayerFormFromList(addDtmLayer);

        }

        private void cbxDTMMapLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDTMMapLayer.Checked == false)
            {
                if (btnAddDTMMapLayer.Text == "Add")
                {
                    m_DtmLayer = null;
                    cbxDTMMapLayer.Checked = true;
                }
            }
            else
            {
                btnAddDTMMapLayer.Text = "Add";
            }
        }

        private void btnDrawPtCloudArea_Click(object sender, EventArgs e)
        {
            this.Hide();
            MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(EditModeManagerCallback_InitItemResultsEvent);

            try
            {
                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {
                    IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                        IDNMcObjectSchemeItem ObjSchemeItem = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                                            DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                                                            0f,
                                                                                            0f,
                                                                                            DNELineStyle._ELS_DASH_DOT,
                                                                                            new DNSMcBColor(255, 0, 0, 255),
                                                                                            3f,
                                                                                            null,
                                                                                            new DNSMcFVector2D(0, -1),
                                                                                            1f,
                                                                                            DNEFillStyle._EFS_NONE);



                        IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                            ObjSchemeItem,
                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                            locationPoints,
                                                            false);

                        MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem);

                        // turn on all viewports render needed flags
                        Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(activeOverlay.GetOverlayManager());
                    }
                    else
                        MessageBox.Show("There is no active overlay");
                }
                else
                    MessageBox.Show("There is no active overlay manager");
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating rectangle item", McEx);
            }
        }

        void EditModeManagerCallback_InitItemResultsEvent(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            if (pItem.GetNodeType() == DNEObjectSchemeNodeType._RECTANGLE_ITEM)
            {
                DNSMcVector3D [] rectLocation = pObject.GetLocationPoints(0);
                double xMin, xMax, yMin, yMax;

                if (rectLocation[0].x < rectLocation[1].x)
                {
                    xMin = rectLocation[0].x;
                    xMax = rectLocation[1].x;
                }
                else
                {
                    xMin = rectLocation[1].x;
                    xMax = rectLocation[0].x;
                }

                if (rectLocation[0].y < rectLocation[1].y)
                {
                    yMin = rectLocation[0].y;
                    yMax = rectLocation[1].y;
                }
                else
                {
                    yMin = rectLocation[1].y;
                    yMax = rectLocation[0].y;
                }

                IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;
                FontDialog Fd = new FontDialog();
                DNSMcLogFont logFont = new DNSMcLogFont();
                Fd.Font.ToLogFont(logFont);
                IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));
                IDNMcObjectSchemeItem ObjSchemeItem;
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[1];

                dgvCloudPoints.RowCount = ntxNumPtToCastLots.GetInt32();

                for (int i = 0; i < ntxNumPtToCastLots.GetInt32(); i++ )
                {
                    locationPoints[0].x = randX.Next((int)xMin, (int)xMax);
                    locationPoints[0].y = randX.Next((int)yMin, (int)yMax);
                    
                    dgvCloudPoints[0, i].Value = locationPoints[0].x;
                    dgvCloudPoints[1, i].Value = locationPoints[0].y;
                    dgvCloudPoints[3, i].Value = true;

                    ObjSchemeItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                            DefaultFont);

                    ((IDNMcTextItem)ObjSchemeItem).SetText(new DNMcVariantString(i.ToString(), false), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                        ObjSchemeItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);
                }

                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
                MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new MCTester.Managers.ObjectWorld.InitItemResultsEventArgs(EditModeManagerCallback_InitItemResultsEvent);

                this.Show();
            }
        }

        private void tpGetFileParameters_Click(object sender, EventArgs e)
        {

        }
    }
}
