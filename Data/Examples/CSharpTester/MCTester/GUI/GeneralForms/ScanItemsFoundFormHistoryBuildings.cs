using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapCore;
using MapCore.Common;
using MCTester.GUI.Forms;
using MCTester.GUI.Trees;
using MCTester.Managers.MapWorld;
using MCTester.Managers.ObjectWorld;
using MCTester.ObjectWorld.OverlayManagerWorld;
using UnmanagedWrapper;
using static System.Net.Mime.MediaTypeNames;
using static MCTester.General_Forms.ScanItemsFoundFormHistoryBuildings;

namespace MCTester.General_Forms
{
    public partial class ScanItemsFoundFormHistoryBuildings : Form, IDNMapLayerAsyncOperationCallback
    {
        DNSTargetFound m_ItemFound;
        DNSSmartRealityBuildingHistory[] m_aBuildingHistory;
        Source m_Source;
        int m_colMove = 5;
        int m_colSurface = 6;
        bool m_isLoad = true;
        IDNMcObjectSchemeItem m_mcPolygonItem = null;
        IDNMcObjectScheme m_mcContourObjectScheme = null;
        IDNMcTextItem m_mcTextItem = null;
        IDNMcObjectScheme m_mcTextObjectScheme = null;
        Dictionary<int, List<IDNMcObject>> m_BuildingSurfaceObjects = new Dictionary<int, List<IDNMcObject>>();

        uint COLOR_PROPERTY = 1;
        uint PITCH_PROPERTY = 20;
        uint YAW_PROPERTY = 30;
        uint TEXT_PROPERTY = 40;        

        public ScanItemsFoundFormHistoryBuildings()
        {
            InitializeComponent();
            this.Text = "Smart Reality Building History";
            m_Source = Source.JumpToBuilding;
            txtURL.Text = Manager_MCLayers.UrlFlightHistoryServer.ToString();
        }

        public enum Source { Scan, JumpToBuilding, ShowSurfaces }

        public ScanItemsFoundFormHistoryBuildings(DNSTargetFound itemFound) : this()
        {
            m_ItemFound = itemFound;
            tbTargetId.Text = itemFound.uTargetID.IsEmpty() ? "" : m_ItemFound.uTargetID.Get128bitAsUUIDString();
            m_Source = Source.Scan;
        }

        public ScanItemsFoundFormHistoryBuildings(Source source) : this()
        {
            m_Source = source;
            dgvHeights.Visible = false;
            if (m_Source == Source.JumpToBuilding)
            {
                btnGetBuildingHeights.Text = "Jump To Building";
            }
            else
            {
                btnGetBuildingHeights.Text = "Show Surfaces";
            }
            tbTargetId.ReadOnly = false;
            this.Size = new System.Drawing.Size(700, 150);
        }

        public void On3DModelSmartRealityDataResults(DNEMcErrorCode eStatus, string strServerURL, DNSMcVariantID uObjectID, DNSSmartRealityBuildingHistory[] aBuildingHistory)
        {
            m_isLoad = true;
            m_aBuildingHistory = aBuildingHistory;
            dgvHeights.Rows.Clear();

            if (eStatus == DNEMcErrorCode.SUCCESS)
            {
                if (aBuildingHistory != null && aBuildingHistory.Length > 0)
                {
                    if (m_Source == Source.Scan)
                    {
                        for (int i = 0; i < aBuildingHistory.Length; i++)
                        {
                            string strCoordinateSystem = Manager_MCLayers.GetGCSStr(aBuildingHistory[i].pCoordinateSystem);
                            string strBoundingBox = "";
                            if (aBuildingHistory[i].BoundingBox.MaxVertex != DNSMcVector3D.v3Zero || aBuildingHistory[i].BoundingBox.MinVertex != DNSMcVector3D.v3Zero)
                                strBoundingBox = Manager_MCLayers.GetBBStr(aBuildingHistory[i].BoundingBox);
                            dgvHeights.Rows.Add(i, aBuildingHistory[i].FlightDate.ToString(), aBuildingHistory[i].dHeight, strBoundingBox, strCoordinateSystem);

                            if (aBuildingHistory[i].pCoordinateSystem == null)
                            {
                                dgvHeights.Rows[i].Cells[m_colMove] = new DataGridViewTextBoxCell();
                                dgvHeights.Rows[i].Cells[m_colMove].Value = ""; // Optional: set empty text
                            }
                            if(aBuildingHistory[i].aBuildingSurfaces == null)
                            {
                                dgvHeights.Rows[i].Cells[m_colSurface] = new DataGridViewTextBoxCell();
                                dgvHeights.Rows[i].Cells[m_colSurface].Value = ""; // Optional: set empty text
                            }
                        }
                    }
                    else
                    {
                        int i = 0;
                        bool bIsClose = false;
                        for (; i < aBuildingHistory.Length; i++)
                        {
                            if (aBuildingHistory[i].pCoordinateSystem != null)
                            {
                                if (m_Source == Source.JumpToBuilding && aBuildingHistory[i].BoundingBox.CenterPoint() != DNSMcVector3D.v3Zero)
                                {
                                    JumpToBuilding(i);
                                    bIsClose = true;
                                }
                                else if (m_Source == Source.ShowSurfaces && aBuildingHistory[i].aBuildingSurfaces != null)
                                {
                                    ShowSurfaces(i);
                                    bIsClose = true;
                                }
                            }
                            if (bIsClose)
                            {
                                this.Close();
                                this.DialogResult = DialogResult.OK;
                                return;
                            }
                        }
                        if(i == aBuildingHistory.Length)
                        {
                            MessageBox.Show("For this 'Object ID' don't exist building location", "Get 3D Model Smart Reality Building History Data Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("For this 'Object ID' don't exist building history", "Get 3D Model Smart Reality Building History Data Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(IDNMcErrors.ErrorCodeToString(eStatus), "Get 3D Model Smart Reality Data Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            m_isLoad = false;
        }

        public void OnFieldUniqueValuesResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNEVectorFieldReturnedType eReturnedType, object paUniqueValues)
        {
            throw new NotImplementedException();
        }

        public void OnScanExtendedDataResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNSVectorItemFound[] aVectorItems, DNSMcVector3D[] aUnifiedVectorItemsPoints)
        {
            throw new NotImplementedException();
        }

        public void OnVectorItemFieldValueResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNEVectorFieldReturnedType eReturnedType, object pValue)
        {
            throw new NotImplementedException();
        }

        public void OnVectorItemPointsResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, DNSMcVector3D[][] aaPoints)
        {
            throw new NotImplementedException();
        }

        public void OnVectorQueryResult(IDNMcMapLayer pLayer, DNEMcErrorCode eStatus, ulong[] auVectorItemsID)
        {
            throw new NotImplementedException();
        }

        public void OnWebServerLayersResults(DNEMcErrorCode eStatus, string strServerURL, DNEWebMapServiceType eWebMapServiceType, DNSServerLayerInfo[] aLayers, string[] astrServiceMetadataURLs, string strServiceProviderName)
        {
            throw new NotImplementedException();
        }

        private void btnGetBuildingHeights_Click(object sender, EventArgs e)
        {
          
            Manager_MCLayers.UrlFlightHistoryServer =  txtURL.Text ;

            try
            {
                DNSMcVariantID uObjectID;
                if (m_Source == Source.Scan)
                    uObjectID = m_ItemFound.uTargetID;
                else
                {
                    uObjectID = new DNSMcVariantID();
                    uObjectID.Set128bitAsUUIDString(tbTargetId.Text);
                }
                DNMcMapDevice.Get3DModelSmartRealityData(txtURL.Text, DNESmartRealityQuery._ESRQ_BUILDING_HISTORY, uObjectID, this);
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Get3DModelSmartRealityData", McEx);
            }
        }


        private void dgvHeights_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0 && e.RowIndex < m_aBuildingHistory.Length)  // move to location
            {
                if(e.ColumnIndex == m_colMove)
                    JumpToBuilding(e.RowIndex);
                else if(e.ColumnIndex == m_colSurface)
                {
                    dgvHeights.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        private void dgvHeights_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == m_colSurface && !m_isLoad)
            {
                object colValue = dgvHeights[e.ColumnIndex, e.RowIndex].Value;
                if (colValue != null)
                {
                    if ((bool)colValue == true)
                    {
                        if (m_aBuildingHistory[e.RowIndex].pCoordinateSystem == null)
                        {
                            MessageBox.Show("Can't show building surface because 'Grid Coordinate System' null.", "Show Surfaces");
                            dgvHeights[e.ColumnIndex, e.RowIndex].Value = false;
                            return;
                        }
                        if (m_aBuildingHistory[e.RowIndex].aBuildingSurfaces != null)
                        {
                            ShowSurfaces(e.RowIndex);
                        }
                    }
                    else
                    {
                        RemoveTempPoints(e.RowIndex);
                    }
                }
            }
        }

        private void AddSurfaceBuildingObject(int RowIndex, IDNMcObject mcObject )
        {
            if (!m_BuildingSurfaceObjects.ContainsKey(RowIndex))
                m_BuildingSurfaceObjects.Add(RowIndex, new List<IDNMcObject>());
            if(m_BuildingSurfaceObjects[RowIndex] == null)
                m_BuildingSurfaceObjects[RowIndex] = new List<IDNMcObject>();
            m_BuildingSurfaceObjects[RowIndex].Add(mcObject);
        }

        private void JumpToBuilding(int RowIndex)
        {
            try
            {
                DNSMcVector3D centerPoint = m_aBuildingHistory[RowIndex].BoundingBox.CenterPoint();
                if (m_aBuildingHistory[RowIndex].pCoordinateSystem != null)
                {
                    if (m_aBuildingHistory[RowIndex].pCoordinateSystem != MCTMapFormManager.MapForm.Viewport.CoordinateSystem)
                    {
                        IDNMcGridConverter gridCnvrt = DNMcGridConverter.Create(m_aBuildingHistory[RowIndex].pCoordinateSystem, MCTMapFormManager.MapForm.Viewport.CoordinateSystem);
                        DNSMcVector3D convertedCenterPoint = new DNSMcVector3D();
                        int zoneResult = 0;

                        gridCnvrt.ConvertAtoB(centerPoint,
                                                   out convertedCenterPoint,
                                                   out zoneResult);

                        centerPoint = convertedCenterPoint;
                    }
                    MCTAsyncQueryCallback.MoveToCenterPoint(centerPoint, MCTMapFormManager.MapForm.Viewport);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("JumpToBuilding", McEx);
            }
        }


        private void dgvHeights_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if(e.RowIndex < m_aBuildingHistory.Length && m_aBuildingHistory[e.RowIndex].pCoordinateSystem != null)
                this.dgvHeights.Rows[e.RowIndex].Cells[m_colMove].Value = "...";
        }

        private void RemoveTempPoints(int RowIndex)
        {
            if (m_BuildingSurfaceObjects.ContainsKey(RowIndex))
            {
                try
                {
                    foreach (IDNMcObject dNMcObject in m_BuildingSurfaceObjects[RowIndex])
                    {
                        dNMcObject.Remove();
                        dNMcObject.Dispose();
                    }
                    m_BuildingSurfaceObjects[RowIndex] = null;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("RemoveTempPoints", McEx);
                }


            }
        }

        private void ScanItemsFoundFormHistoryBuildings_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!chxStay.Checked)
            {
                for (int RowIndex = 0; RowIndex < m_BuildingSurfaceObjects.Count; RowIndex++)
                    RemoveTempPoints(RowIndex);

            }
        }

        private void ShowSurfaces(int RowIndex)
        {
            try
            {
                IDNMcOverlayManager mcOverlayManager = MCTMapFormManager.MapForm.Viewport.OverlayManager;
                if (mcOverlayManager == null)
                {
                    MessageBox.Show("Missing Overlay Manager", "Show Surface");
                    return;
                }
                IDNMcGridConverter m_GridConverted = DNMcGridConverter.Create(m_aBuildingHistory[RowIndex].pCoordinateSystem, mcOverlayManager.GetCoordinateSystemDefinition());
                DNSMcVector3D convertedCenterPoint = new DNSMcVector3D();
                IDNMcOverlay mcOverlay = Manager_MCOverlayManager.ActiveOverlay;
                int zoneResult = 0;
                int countSurfaces = m_aBuildingHistory[RowIndex].aBuildingSurfaces.Length;

                if (m_mcPolygonItem == null)
                {
                    m_mcPolygonItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ACCURATE_3D_SCREEN_WIDTH,
                                                                   DNELineStyle._ELS_SOLID,
                                                                   new DNSMcBColor(255, 0, 0, 255),
                                                                   3f,
                                                                   null,
                                                                   new DNSMcFVector2D(0, -1),
                                                                   1f,
                                                                   DNEFillStyle._EFS_NONE);
                    ((IDNMcPolygonItem)m_mcPolygonItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    m_mcContourObjectScheme = DNMcObjectScheme.Create(mcOverlayManager, m_mcPolygonItem, DNEMcPointCoordSystem._EPCS_WORLD);
                    ((IDNMcLineBasedItem)m_mcPolygonItem).SetLineColor(new DNSMcBColor(255, 0, 0, 255), COLOR_PROPERTY, false);
                }
                for (int i = 0; i < countSurfaces; i++)
                {
                    DNSMcVector3D[] aConvertedCenterPoints = new DNSMcVector3D[1];

                    DNSSmartRealityBuildingSurface smartRealityBuildingSurface = m_aBuildingHistory[RowIndex].aBuildingSurfaces[i];
                    string surfaceType = smartRealityBuildingSurface.eSurfaceType.ToString().Replace("_EBST_", "");
                    string surfaceArea = surfaceType + Environment.NewLine + Math.Round(smartRealityBuildingSurface.dSurfaceArea, 1).ToString();
                   
                    double pdPitch, pdYaw;
                    DNSMcVector3D normal = (smartRealityBuildingSurface.eSurfaceType == DNEBuildingSurfaceType._EBST_GROUND) ? -smartRealityBuildingSurface.SurfaceNormal : smartRealityBuildingSurface.SurfaceNormal;
                    normal.GetDegreeYawPitchFromForwardVector(out pdYaw, out pdPitch);
                    pdPitch -= 90;

                    DNEMcPointCoordSystem eTextCoordinateSystem = chxTextOrientationFromNormal.Checked? DNEMcPointCoordSystem._EPCS_WORLD : DNEMcPointCoordSystem._EPCS_SCREEN;

                    DNSMcVector3D surfaceCenterPoint = smartRealityBuildingSurface.SurfaceCenter;

                    m_GridConverted.ConvertAtoB(surfaceCenterPoint, out convertedCenterPoint, out zoneResult);

                    aConvertedCenterPoints[0] = new DNSMcVector3D(convertedCenterPoint.x, convertedCenterPoint.y, surfaceCenterPoint.z);
                    DNSMcVector3D[] contours = smartRealityBuildingSurface.aSurfaceContour;
                    DNSMcVector3D[] convertedContours = new DNSMcVector3D[contours.Length];
                    if (contours != null)
                    {
                        for (int j = 0; j < contours.Length; j++)
                        {
                            DNSMcVector3D contourConvertedPoint = new DNSMcVector3D();

                            m_GridConverted.ConvertAtoB(contours[j],
                                      out contourConvertedPoint,
                                      out zoneResult);

                            convertedContours[j] = new DNSMcVector3D(contourConvertedPoint.x, contourConvertedPoint.y, contours[j].z);
                        }

                        IDNMcObject mcContourObject = DNMcObject.Create(mcOverlay, m_mcContourObjectScheme, convertedContours);
                        DNSMcBColor mcBColor = new DNSMcBColor(255, 0, 0, 255);
                        if (smartRealityBuildingSurface.eSurfaceType == DNEBuildingSurfaceType._EBST_WALL)
                            mcBColor = new DNSMcBColor(0, 0, 255, 255);
                        else if (smartRealityBuildingSurface.eSurfaceType == DNEBuildingSurfaceType._EBST_WINDOW)
                            mcBColor = new DNSMcBColor(0, 255, 0, 255);
                        else if (smartRealityBuildingSurface.eSurfaceType == DNEBuildingSurfaceType._EBST_GROUND)
                            mcBColor = DNSMcBColor.bcWhiteOpaque;
                        mcContourObject.SetBColorProperty(COLOR_PROPERTY, mcBColor);

                        AddSurfaceBuildingObject(RowIndex, mcContourObject);

                    }

                    if (i == 0)
                    {
                        Font font = new Font(FontFamily.GenericSansSerif, 16, System.Drawing.FontStyle.Bold);
                        DNSMcLogFont logFont = new DNSMcLogFont();
                        font.ToLogFont(logFont);

                        DNSCharactersRange[] aCharachterRanges = new DNSCharactersRange[2];
                        aCharachterRanges[0] = new DNSCharactersRange();
                        aCharachterRanges[0].nFrom = '.';
                        aCharachterRanges[0].nTo = '9';

                        aCharachterRanges[1] = new DNSCharactersRange();
                        aCharachterRanges[1].nFrom = 'A';
                        aCharachterRanges[1].nTo = 'Z';

                        IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false), false, aCharachterRanges);

                        m_mcTextItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, eTextCoordinateSystem, DefaultFont);
                        m_mcTextObjectScheme = DNMcObjectScheme.Create(mcOverlayManager, m_mcTextItem, DNEMcPointCoordSystem._EPCS_WORLD);

                        m_mcTextItem.SetTextColor(new DNSMcBColor(255, 0, 0, 255), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        m_mcTextItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        m_mcTextItem.SetRectAlignment(DNEBoundingRectanglePoint._EBRP_CENTER, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, 0);
                        if (chxTextOrientationFromNormal.Checked)
                        {
                            m_mcTextItem.SetNeverUpsideDownMode(DNENeverUpsideDownMode._ENUDM_ROTATE_TEXT, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            m_mcTextItem.SetRotationPitch(0, PITCH_PROPERTY, 0);
                            m_mcTextItem.SetRotationYaw(0, YAW_PROPERTY, 0);
                        }
                        m_mcTextItem.SetText(new DNMcVariantString("", true), TEXT_PROPERTY, false);
                    }

                    IDNMcObject mcTextObject = DNMcObject.Create(mcOverlay, m_mcTextObjectScheme, aConvertedCenterPoints);
                    if (chxTextOrientationFromNormal.Checked)
                    {
                        mcTextObject.SetFloatProperty(PITCH_PROPERTY, (float)pdPitch);
                        mcTextObject.SetFloatProperty(YAW_PROPERTY, (float)pdYaw);
                    }
                    mcTextObject.SetStringProperty(TEXT_PROPERTY, new DNMcVariantString(surfaceArea, true));

                    AddSurfaceBuildingObject(RowIndex, mcTextObject);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("ShowSurfaces", McEx);
            }
        }

        private void chxTextOrientationFromNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_isLoad)
            {
                for (int i = 0; i < dgvHeights.RowCount; i++)
                {
                    object colValue = dgvHeights[m_colSurface, i].Value;
                    if (colValue != null && (bool)colValue == true)
                    {
                        RemoveTempPoints(i);
                        ShowSurfaces(i);
                    }
                }
            }
        }
    }
}
