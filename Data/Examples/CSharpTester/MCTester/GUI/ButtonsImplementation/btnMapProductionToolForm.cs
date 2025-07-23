using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.General_Forms;
using System.IO;
using MCTester.MapWorld.Assist_Forms;

namespace MCTester.ButtonsImplementation
{
    public partial class btnMapProductionToolForm : Form, IDNMcProgressCallback
    {
        private IDNMcMapProduction m_MapProduction;
        private DNSSourceFileParams[] mRasterSourceFiles = null;
        private DNSSourceFileParams[] mDTMSourceFiles = null;
        private IDNMcObjectSchemeItem m_ClippingSchemeItem;
        private IDNMcObject m_CliipingRectObj;
        private string[] pastrFilesname;

        private DNSTilingScheme mcTilingScheme = null;

        public btnMapProductionToolForm()
        {
            InitializeComponent();

            cmbRasterOverlapMode.Items.AddRange(Enum.GetNames(typeof(DNERasterOverlapMode)));
            cmbRasterOverlapMode.Text = DNERasterOverlapMode._EROM_OVERRIDE_ALWAYS.ToString();
            
            cmbJpegQuality.Items.AddRange(Enum.GetNames(typeof(DNERasterCompressionQuality)));
            cmbJpegQuality.Text = DNERasterCompressionQuality._ERCQ_NORMAL.ToString();

            cbItemType.Items.AddRange(Enum.GetNames(typeof(DNEObjectSchemeNodeType)));
            cbItemType.Text = DNEObjectSchemeNodeType._ELLIPSE_ITEM.ToString();

            cmbVersionCompatibility.Items.AddRange(Enum.GetNames(typeof(DNEVersionCompatibility)));
            cmbVersionCompatibility.Text = DNEVersionCompatibility._EVC_LATEST_VERSION.ToString();

            ctrlGridCoordinateSystemSource.SetSelected(0);
        }

        private void frmMapProduction_Load(object sender, EventArgs e)
        {
            try
            {
                m_MapProduction = DNMcMapProduction.Create();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("DNMcMapProduction.Create()", McEx);
                return;
            }
        }
         
        private bool CheckDestinationDir()
        {
            if (ctrlBrowseControlDestinationDir.FileName == String.Empty)
            {
                MessageBox.Show("Missing Destination Directory", "MCTester Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private DialogResult AlertDestinationDir()
        {
            string msg =
           "The following destination directory contents will\n" +
           "be erased before starting the conversion:\n\n" +
           ctrlBrowseControlDestinationDir.FileName + "\n\n" +
           "Are you sure you want to continue?";
            return MessageBox.Show(msg, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            //     return MessageBox.Show("Destination Directory Content Will Deleted, Continue?", "Destination Directory Content", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void btnConvertRasterLayer_Click(object sender, EventArgs e)
        {
            if (!CheckDestinationDir())
                return;
            if (AlertDestinationDir() == DialogResult.Cancel)
                return;

            if (rdbGrayScale.Checked == true)
            {
                try
                {
                    m_MapProduction.AddProgressCallback(this);

                    m_MapProduction.ConvertRasterLayerToGrayscale(ctrlBrowseControlDestinationDir.FileName);

                    MessageBox.Show("Conversion ended successfully", "Conversion Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_MapProduction.RemoveProgressCallback(this);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ConvertRasterLayerToGrayscale", McEx);
                }
            }
            else
            {
                

                DNSRasterConvertParams rasterConvertParams = new DNSRasterConvertParams();

                rasterConvertParams.aSources = mRasterSourceFiles;
                rasterConvertParams.bGrayscale = chxGrayScale.Checked;
                rasterConvertParams.bImageCoordinateSystem = chxImageCoordinateSystem.Checked;
                rasterConvertParams.bUseRecoveryInfo = chxUpdateExisting.Checked;
                rasterConvertParams.byTransparentColorPrecision = ntxTransparentColorPrecision.GetByte();
                rasterConvertParams.aClipRects = new DNSMcBox[1];
                rasterConvertParams.aClipRects[0] = ctrlSMcBoxClipRect.GetBoxValue();
                rasterConvertParams.eOverlapMode = (DNERasterOverlapMode)Enum.Parse(typeof(DNERasterOverlapMode), cmbRasterOverlapMode.Text);
                rasterConvertParams.fTargetHighestResolution = (rdbTargetHighestResolutionSource.Checked == true) ? DNSConvertParams.MC_RESOLUTION_SOURCE : ntxTargetHighestResolution.GetFloat();
                rasterConvertParams.uNumTilesInFileEdge = ntxNumTilesInFileEdge.GetUInt32();
                rasterConvertParams.pDestCoordSys = ctrlGridCoordinateSystemDest.GridCoordinateSystem;
                rasterConvertParams.pSourceCoordSys = ctrlGridCoordinateSystemSource.GridCoordinateSystem;
                rasterConvertParams.strDestDir = ctrlBrowseControlDestinationDir.FileName;
                rasterConvertParams.TransparentColor = new DNSMcBColor(pbTransparentColor.BackColor.R, pbTransparentColor.BackColor.G, pbTransparentColor.BackColor.B, 255);
                rasterConvertParams.fReprojectionPrecision = ntxReprojectionPrecision.GetFloat();
                rasterConvertParams.bOneResolutionOnly = chxOneResolutionOnly.Checked;
                rasterConvertParams.eVersionCompatibility = (DNEVersionCompatibility)Enum.Parse(typeof(DNEVersionCompatibility), cmbVersionCompatibility.Text);
                rasterConvertParams.pTilingScheme = mcTilingScheme;

                if (cgbNonStandardCompression.Checked == false)
                {
                    rasterConvertParams.eCompression = DNERasterCompression._ERC_DXT1;
                }
                else
                {
                    if (rdbJpeg.Checked == true)
                    {
                        rasterConvertParams.eCompression = DNERasterCompression._ERC_JPEG;
                        rasterConvertParams.eCompressionQuality = (DNERasterCompressionQuality)Enum.Parse(typeof(DNERasterCompressionQuality), cmbJpegQuality.Text);
                    }

                    if (rdbPNG.Checked == true)
                    {
                        rasterConvertParams.eCompression = DNERasterCompression._ERC_PNG;
                        rasterConvertParams.uNumPngColors = ntxPngColors.GetUInt32();
                    }

                    if (rdbNone.Checked == true)
                    {
                        rasterConvertParams.eCompression = DNERasterCompression._ERC_NONE;
                    }
                }

                try
                {
                    m_MapProduction.AddProgressCallback(this);

                    m_MapProduction.ConvertRasterLayer(rasterConvertParams);

                    MessageBox.Show("Conversion ended successfully", "Conversion Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_MapProduction.RemoveProgressCallback(this);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("ConvertRasterLayer", McEx);
                    m_MapProduction.RemoveProgressCallback(this);
                }
            }
        }

        private void btnConvertDTMLayer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckDestinationDir())
                    return;
                if (AlertDestinationDir() == DialogResult.Cancel)
                    return;

                DNSDtmConvertParams dtmConvertParams = new DNSDtmConvertParams();

                dtmConvertParams.aSources = mDTMSourceFiles;
                dtmConvertParams.bFillNoHeightAreas = chxFillNoHeightAreas.Checked;
                dtmConvertParams.bIsDtmOrthoView = true; // <<???!!!>> support in McTester tester...
                dtmConvertParams.bUseRecoveryInfo = chxUpdateExisting.Checked;
                dtmConvertParams.aClipRects = new DNSMcBox[1];
                dtmConvertParams.aClipRects[0] = ctrlSMcBoxClipRect.GetBoxValue();
                dtmConvertParams.fTargetHighestResolution = (rdbTargetHighestResolutionSource.Checked == true) ? DNSConvertParams.MC_RESOLUTION_SOURCE : ntxTargetHighestResolution.GetFloat();
                dtmConvertParams.uNumTilesInFileEdge = ntxNumTilesInFileEdge.GetUInt32();
                dtmConvertParams.pDestCoordSys = ctrlGridCoordinateSystemDest.GridCoordinateSystem;
                dtmConvertParams.pSourceCoordSys = ctrlGridCoordinateSystemSource.GridCoordinateSystem;
                dtmConvertParams.strDestDir = ctrlBrowseControlDestinationDir.FileName;
                dtmConvertParams.uNumSmoothingLevels = ntxNumSmoothingLevels.GetUInt32();
                dtmConvertParams.fReprojectionPrecision = ntxReprojectionPrecision.GetFloat();
                dtmConvertParams.bOneResolutionOnly = chxOneResolutionOnly.Checked;
                dtmConvertParams.eVersionCompatibility = (DNEVersionCompatibility)Enum.Parse(typeof(DNEVersionCompatibility), cmbVersionCompatibility.Text);
                dtmConvertParams.pTilingScheme = mcTilingScheme;
                m_MapProduction.AddProgressCallback(this);

                m_MapProduction.ConvertDtmLayer(dtmConvertParams);

                MessageBox.Show("Conversion ended successfully", "Conversion Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_MapProduction.RemoveProgressCallback(this);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ConvertDtmLayer", McEx);
                m_MapProduction.RemoveProgressCallback(this);
            }
        }

        private void btnTransparentColor_Click(object sender, EventArgs e)
        {
            ColorDialog cdlgTransparentColor = new ColorDialog();
            if (cdlgTransparentColor.ShowDialog() == DialogResult.OK)
            {
                pbTransparentColor.BackColor = cdlgTransparentColor.Color;
            }
        }

        private void chxIsImage_CheckedChanged(object sender, EventArgs e)
        {
            if (chxImageCoordinateSystem.Checked == true)
            {
                ctrlGridCoordinateSystemSource.Enabled = false;
                ctrlGridCoordinateSystemSource.GridCoordinateSystem = null;
            }
            else
            {
                ctrlGridCoordinateSystemSource.Enabled = true;
            }
        }

        private void btnBuildRasterPARFile_Click(object sender, EventArgs e)
        {
            DNELayerKind sourceType = DNELayerKind._ELK_RASTER;
            bool bIsRasterInWorldCoordSys;
            if (chxIsImageSourceType.Checked == true)
                bIsRasterInWorldCoordSys = false; // ImageCoordSys
            else
                bIsRasterInWorldCoordSys = true;  // WorldCoordSys   


            frmImagesPARFiles ImagesPARFilesForm = new frmImagesPARFiles(m_MapProduction, sourceType, bIsRasterInWorldCoordSys);
            if (ImagesPARFilesForm.ShowDialog() == DialogResult.OK)
            {
                btnConvertRasterLayer.Enabled = true;
                mRasterSourceFiles = ImagesPARFilesForm.SourceFiles;
            }
        }

        private void btnBuildDTMPARFile_Click(object sender, EventArgs e)
        {
            frmImagesPARFiles ImagesPARFilesForm = new frmImagesPARFiles(m_MapProduction, DNELayerKind._ELK_DTM, false);
            if (ImagesPARFilesForm.ShowDialog() == DialogResult.OK)
            {
                btnConvertDTMLayer.Enabled = true;
                mDTMSourceFiles = ImagesPARFilesForm.SourceFiles;
            }
        }

        #region IDNMcProgressCallback Members

        public void OnProgressMessage(string strMessage, DNEProgressMessageType eMessageType)
        {
            string mess = eMessageType.ToString() + ":   " + strMessage;
            lstProgressMessage.Items.Insert(lstProgressMessage.Items.Count, mess);
            lstProgressMessage.TopIndex = lstProgressMessage.Items.Count -1;

            System.Windows.Forms.Application.DoEvents();
        }

        public bool OnFileError(string strFilePath, bool bRead, uint uTryCounter)
        {
	        uint uSubCounter = (uTryCounter + 1) % 6;
            if (uSubCounter != 0)
	        {
		        System.Threading.Thread.Sleep((int)uSubCounter * 10000);
		        return true;
	        }
	        DialogResult result = 
                MessageBox.Show(strFilePath, bRead ? "File read error" : "File write error", 
				    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
	        return (result != DialogResult.Cancel);
        }

        #endregion        

        private void chxShowClipping_CheckedChanged(object sender, EventArgs e)
        {
            if (chxShowClipping.Checked == true)
            {
                if (ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.x != 0 &&
                ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.y != 0 &&
                ctrlSMcBoxClipRect.GetBoxValue().MinVertex.x != 0 &&
                ctrlSMcBoxClipRect.GetBoxValue().MinVertex.y != 0)
                {
                    try
                    {
                        m_ClippingSchemeItem = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                            DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                            DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS, 
                                                                            0f,
                                                                            0f,
                                                                            DNELineStyle._ELS_DASH_DOT,
                                                                            new DNSMcBColor(0, 255, 0, 255),
                                                                            3f,
                                                                            null,
                                                                            new DNSMcFVector2D(0, -1),
                                                                            1f,
                                                                            DNEFillStyle._EFS_NONE);


                        DNSMcVector3D[] locationPoints = new DNSMcVector3D[2];
                        locationPoints[0].x = ctrlSMcBoxClipRect.GetBoxValue().MinVertex.x;
                        locationPoints[0].y = ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.y;
                        locationPoints[1].x = ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.x;
                        locationPoints[1].y = ctrlSMcBoxClipRect.GetBoxValue().MinVertex.y;

                        IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;

                        m_CliipingRectObj = DNMcObject.Create(activeOverlay,
                                                                m_ClippingSchemeItem,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPoints,
                                                                false);

                        chxShowClipping.Text = "Unshow";
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                    }
                }
            }
            else
            {
                try
                {
                    if (m_CliipingRectObj != null)
                    {
                        m_CliipingRectObj.Remove();
                        m_CliipingRectObj.Dispose();
                        m_CliipingRectObj = null;
                    }

                    chxShowClipping.Text = "Show";
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Remove", McEx);
                }
            }
            
        }

        private void frmMapProduction_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_CliipingRectObj != null)
            {
                try
                {
                    m_CliipingRectObj.Remove();
                    m_CliipingRectObj.Dispose();
                    m_CliipingRectObj = null;	
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Remove", McEx);
                }
            }            
        }

        private void rdbColorful_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbColorful.Checked == true)
            {
                gbColor.Enabled = true;
                cgbNonStandardCompression.Enabled = true;
            }
            else
            {
                gbColor.Enabled = false;
                cgbNonStandardCompression.Enabled = false;              
            }                    
        }

        private void rdbJpeg_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbJpeg.Checked == true)
            {
                cmbJpegQuality.Enabled = true;
                ntxPngColors.Enabled = false;
            }
            else
                cmbJpegQuality.Enabled = false;            
        }

        private void rdbPNG_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbPNG.Checked == true)
            {
                cmbJpegQuality.Enabled = false;
                ntxPngColors.Enabled = true;
            }
            else
                ntxPngColors.Enabled = false;       
        }

        // Get Raster Or DTM Layers Files
        private void btnGetRasterOrDtmLayersFiles_Click(object sender, EventArgs e)
        {
            uint ResolutionIndex=DNMcConstants._MC_EMPTY_ID;

            if (ntxResolutionIndex.Text == "" /*|| int.Parse(ntxResolutionIndex.Text) < 0*/)
            {
                ResolutionIndex = DNMcConstants._MC_EMPTY_ID;
            }
            else
            {
                try
                {
                    ResolutionIndex = uint.Parse(ntxResolutionIndex.Text);
                }
                catch
                {
                    ResolutionIndex = DNMcConstants._MC_EMPTY_ID;
                }
            }
            
            string path = ctrlBrowseControlPath.FileName;
            string fullpath = null;
                
            try
            {
                if (cbxIsRecUse.Checked)
                {
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[2];
                    locationPoints[0].x = ctrlSMcBoxClipRect.GetBoxValue().MinVertex.x;
                    locationPoints[0].y = ctrlSMcBoxClipRect.GetBoxValue().MinVertex.y;
                    locationPoints[1].x = ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.x;
                    locationPoints[1].y = ctrlSMcBoxClipRect.GetBoxValue().MaxVertex.y;
                    DNSMcBox rectofintrest = new DNSMcBox(locationPoints[0], locationPoints[1]);
                    m_MapProduction.GetRasterOrDtmLayerFiles(path, ResolutionIndex, out fullpath, out pastrFilesname, rectofintrest);
                }
                else
                    m_MapProduction.GetRasterOrDtmLayerFiles(path, ResolutionIndex, out fullpath, out pastrFilesname);
               
                Form frm = new frmRasterorDtmLayerFiles(fullpath, pastrFilesname);
                frm.Show();
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetRasterOrDtmLayersFiles", McEx);
            }

        }

        // dissable Btntransparent color, pbTransparentcolor & ntxTransparentColorPrecision when DNERasterOverlapMode is _EROM_OVERRIDE_ALWAYS 
        private void cmbRasterOverlapMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRasterOverlapMode.Text == DNERasterOverlapMode._EROM_OVERRIDE_ALWAYS.ToString())
            {
                btnTransparentColor.Enabled = false;
                pbTransparentColor.Enabled = false;
                ntxTransparentColorPrecision.Enabled = false;
            }
            else
            {
                btnTransparentColor.Enabled = true;
                pbTransparentColor.Enabled = true;
                ntxTransparentColorPrecision.Enabled = true;
            }
        }

        private void btnRemoveBorders_Click(object sender, EventArgs e)
        {
            string inputFilePath = ctrlBrowserRmvBrdrInputFile.FileName;
            string outputFilePath = inputFilePath + "_Output_" + Path.GetRandomFileName() + "_" + Path.GetExtension(inputFilePath);
           
            List<KeyValuePair<Color, int>> colors =  ctrlColorArray.UserColorsAndAlpha;
            DNSMcBColor[] aBorderColors = new DNSMcBColor[colors.Count];
            for (int i = 0; i < colors.Count; i++)
            {
                KeyValuePair<Color, int> color = colors[i];
                aBorderColors[i] = new DNSMcBColor(color.Key.R, color.Key.G, color.Key.B, (byte)color.Value);
            }
            int nMaxNonBorderColor = ntbMaxNonBorderColor.GetInt32();
            int nNearColorDist = ntbNearColorDistance.GetInt32();
            bool bSetAlpha = cbIsSetAlpha.Checked;

            try
            {
                m_MapProduction.RemoveBorders(inputFilePath,
                                                aBorderColors,
                                                outputFilePath,
                                                nMaxNonBorderColor,
                                                nNearColorDist,
                                                bSetAlpha);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("RemoveBorders", McEx);
            }

            MessageBox.Show("Remove Borders completed at output file - " + outputFilePath, "Remove Borders Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvHeatMapPoints_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MCTester.Managers.MapWorld.MCTMapFormManager.MapForm == null)
            {
                MessageBox.Show("Missing Viewport!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay == null)
            {
                MessageBox.Show("Missing Active Overlay!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (e.ColumnIndex == 0) // paint rect on map
            {
                InserNewRow(e.RowIndex);
                MCTester.ObjectWorld.ScanBoxGeometry BoxScan = new MCTester.ObjectWorld.ScanBoxGeometry(DNEMcPointCoordSystem._EPCS_WORLD, dgvHeatMapPoints,e.RowIndex);
                BoxScan.StartSelectRect(this);
                
                dgvHeatMapPoints[1, e.RowIndex].Selected = true;

            }
            else if (e.ColumnIndex == 1)
            {
                ConvertHeatMapPointsParams.HeatMapPointsParams[] PointsParams = null;

                if (dgvHeatMapPoints[1, e.RowIndex] != null && dgvHeatMapPoints[1, e.RowIndex].Tag != null)
                    PointsParams = (ConvertHeatMapPointsParams.HeatMapPointsParams[])dgvHeatMapPoints[1, e.RowIndex].Tag;

                ConvertHeatMapPointsParams frmConvertHeatMapPointsParams = new ConvertHeatMapPointsParams(PointsParams);
                if (frmConvertHeatMapPointsParams.ShowDialog() == DialogResult.OK)
                {
                    dgvHeatMapPoints[1, e.RowIndex].Tag = frmConvertHeatMapPointsParams.PointsParams;
                    dgvHeatMapPoints[1, e.RowIndex].Value = "Selected";
                    
                }
            }
        }

        private void InserNewRow(int rowIndex)
        {
            if (rowIndex == dgvHeatMapPoints.RowCount - 1 || dgvHeatMapPoints.Rows[dgvHeatMapPoints.RowCount - 1].IsNewRow == false)
            {
                dgvHeatMapPoints.Rows.Add();
                dgvHeatMapPoints[0, rowIndex + 1].Selected = false;
            }
            else
            {
                dgvHeatMapPoints[0, rowIndex].Selected = false;
            }        
        }

        private void btnConvertHeatMap_Click(object sender, EventArgs e)
        {
            if (!CheckDestinationDir())
                return;
            if (AlertDestinationDir() == DialogResult.Cancel)
                return;

            // rand points
            Random random = new Random(new System.DateTime().Millisecond);

            List<DNSHeatMapPoint> lstPoints = new List<DNSHeatMapPoint>();
            foreach (DataGridViewRow row in dgvHeatMapPoints.Rows)
            {
                if (row.IsNewRow == false)
                {
                    DNSMcBox boxCoord = (DNSMcBox)row.Cells[0].Tag;
                   
                    DNSMcVector3D rect_point = boxCoord.MinVertex;
                    double sizeX = boxCoord.SizeX();
                    double sizeY = boxCoord.SizeY();

                    ConvertHeatMapPointsParams.HeatMapPointsParams[] PointsParams = (ConvertHeatMapPointsParams.HeatMapPointsParams[])row.Cells[1].Tag;
                    foreach (ConvertHeatMapPointsParams.HeatMapPointsParams pointParams in PointsParams)
                    {
                        for (int i = 0; i < pointParams.noPoints; i++)
                        {
                            DNSHeatMapPoint pointParam = new DNSHeatMapPoint();
                            List<DNSMcVector2D> lstLocations = new List<DNSMcVector2D>();

                            int noLocations = pointParams.noLocations;
                            //if (false) // temp...
                            //{
                            //    noLocations = pointParams.noPoints;
                            //}

                            for (int j = 0; j < noLocations; j++)
                            {
                                double x = GetRandomNumber(random, rect_point.x, rect_point.x + sizeX);
                                double y = GetRandomNumber(random, rect_point.y, rect_point.y + sizeY);
                                DNSMcVector2D location = new DNSMcVector2D(x, y);

                                lstLocations.Add(location);
                            }
                            pointParam.aLocations = lstLocations.ToArray();
                            pointParam.uIntensity = pointParams.intensity;

                            //if (false) // for debug...
                            //{
                            //    if (i > 0)
                            //    {
                            //        pointParam.aLocations = lstPoints[0].aLocations;
                            //    }
                            //}

                            lstPoints.Add(pointParam);
                        }
                    }
                }
            }

            DNSHeatMapConvertParams heatMapConvertParams = new DNSHeatMapConvertParams();
            heatMapConvertParams.aPoints = lstPoints.ToArray();
            heatMapConvertParams.bIsGradient = chxIsGradient.Checked;
            heatMapConvertParams.bIsRadiusInPixels = chxIsRadiusInPixels.Checked;
            heatMapConvertParams.uItemInfluenceRadius = ntbPointInfluenceRadius.GetUInt32();
            heatMapConvertParams.bGPUBased = chxGPUBased.Checked;
            heatMapConvertParams.eItemType = (DNEObjectSchemeNodeType)Enum.Parse(typeof(DNEObjectSchemeNodeType), cbItemType.Text);
            heatMapConvertParams.dMinValThreshold = ntbMinValThreshold.GetDouble();
            heatMapConvertParams.dMaxValThreshold = ntbMaxValThreshold.GetDouble();
            heatMapConvertParams.dMinVal = ntbMinVal.GetDouble();
            heatMapConvertParams.dMaxVal = ntbMaxVal.GetDouble();

            heatMapConvertParams.aSources = null;
            heatMapConvertParams.bUseRecoveryInfo = chxUpdateExisting.Checked;
            heatMapConvertParams.aClipRects = new DNSMcBox[1];
            heatMapConvertParams.aClipRects[0] = ctrlSMcBoxClipRect.GetBoxValue();
            heatMapConvertParams.fTargetHighestResolution = (rdbTargetHighestResolutionSource.Checked == true) ? DNSConvertParams.MC_RESOLUTION_SOURCE : ntxTargetHighestResolution.GetFloat();
            heatMapConvertParams.uNumTilesInFileEdge = ntxNumTilesInFileEdge.GetUInt32();
            heatMapConvertParams.pDestCoordSys = ctrlGridCoordinateSystemDest.GridCoordinateSystem;
            heatMapConvertParams.pSourceCoordSys = ctrlGridCoordinateSystemSource.GridCoordinateSystem;
            heatMapConvertParams.strDestDir = ctrlBrowseControlDestinationDir.FileName;
            heatMapConvertParams.fReprojectionPrecision = ntxReprojectionPrecision.GetFloat();
            heatMapConvertParams.bOneResolutionOnly = chxOneResolutionOnly.Checked;
            heatMapConvertParams.fTargetLowestResolution = ntbTargetLowestResolution.GetFloat();
            heatMapConvertParams.bCalcAveragePerPoint = bCalcAveragePerPoint.Checked;
            heatMapConvertParams.pTilingScheme = mcTilingScheme;
            heatMapConvertParams.eVersionCompatibility = (DNEVersionCompatibility)Enum.Parse(typeof(DNEVersionCompatibility), cmbVersionCompatibility.Text);

            try
            {
                m_MapProduction.AddProgressCallback(this);

                m_MapProduction.ConvertHeatMapLayer(heatMapConvertParams);

                MessageBox.Show("Conversion ended successfully", "Conversion Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_MapProduction.RemoveProgressCallback(this);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ConvertRasterLayer", McEx);
                m_MapProduction.RemoveProgressCallback(this);
            }
        }

        public double GetRandomNumber(Random random, double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void numericTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTilingScheme_Click(object sender, EventArgs e)
        {
            frmSTilingSchemeParams frmSTilingSchemeParams = new frmSTilingSchemeParams();
            if (mcTilingScheme != null)
                frmSTilingSchemeParams.STilingScheme = mcTilingScheme;

            if (frmSTilingSchemeParams.ShowDialog() == DialogResult.OK)
            {
                mcTilingScheme = frmSTilingSchemeParams.STilingScheme;
            }
        }
    }
}
