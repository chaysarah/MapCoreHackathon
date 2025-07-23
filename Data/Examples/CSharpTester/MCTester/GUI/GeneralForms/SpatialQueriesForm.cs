using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapCore;
using UnmanagedWrapper;
using MCTester.Managers.ObjectWorld;
using MCTester.Managers.MapWorld;
using System.Threading;
using MCTester.General_Forms;
using System.Linq;
using System.IO;
using System.Diagnostics;
using MCTester.Managers;
using MapCore.Common;
using MCTester.GUI.Trees;
using MCTester.Controls;
using System.Runtime.Serialization;
using MCTester.MCTPackages;
using MCTester.MapWorld.Assist_Forms;

namespace MCTester.GUI.Forms
{
    public partial class SpatialQueriesForm : Form
    {
        private IDNMcSpatialQueries m_currSQ;
        private IDNMcEditMode m_EditMode;
        private List<DNSTargetFound> m_lTargetsFound;
        private int m_IdxTargetFound;
        private List<string> m_lstTerrainText = new List<string>();
        private List<IDNMcMapTerrain> m_lstTerrainValue = new List<IDNMcMapTerrain>();
        private IDNMcObject m_ReturnObject;
        private IDNMcObjectSchemeItem m_ReturnItem;
        private int m_ExitStatus = 0;

        private int m_numTestingPt;
        private MultiThreadParams testRunningParams;
        private System.Windows.Forms.Timer m_StopWatchTimer;
        private Stopwatch m_SW;
        private bool m_ThrowException;

        private IDNMcOverlay m_currActiveOverlay;
        private IDNMcGridCoordinateSystem m_CurrCoordSysOM;
        private IDNMcGeographicCalculations mGeographicCalculations;
        private IDNMcMapViewport m_CurrentObject;
        private DNSMcVector3D[] m_ResultPoints;
        private IDNMcTrackSmoother ts;
        private DNSMcVector3D[] SmoothedTrack = new DNSMcVector3D[0];
        private bool IsAddPoint = false;

        private DNMcNullableOut<DNSPolygonsOfSight> m_SeenPolygonOfSight = new DNMcNullableOut<DNSPolygonsOfSight>();
        private DNMcNullableOut<DNSPolygonsOfSight> m_UnseenPolygonOfSight = new DNMcNullableOut<DNSPolygonsOfSight>();
        private DNMcNullableOut<IDNAreaOfSight> m_AreaOfSight = new DNMcNullableOut<IDNAreaOfSight>();
        private DNMcNullableOut<DNSLineOfSightPoint[][]> m_LineOfSight = new DNMcNullableOut<DNSLineOfSightPoint[][]>();
        private DNMcNullableOut<DNSStaticObjectsIDs[]> m_SeenStaticObjects = new DNMcNullableOut<DNSStaticObjectsIDs[]>();

        private IDNMcObjectSchemeItem m_unseenPolygonItemAreaOfSight = null;
        private IDNMcObjectSchemeItem m_seenPolygonItemAreaOfSight = null;
        private IDNMcObjectScheme m_seenPolygonSchemeAreaOfSight = null;
        private IDNMcObjectScheme m_unseenPolygonSchemeAreaOfSight = null;

        private IDNMcObjectSchemeItem m_visibleLineSchemeItemAreaOfSight = null;
        private IDNMcObjectSchemeItem m_nonVisibleLineSchemeItemAreaOfSight = null;
        private IDNMcObjectScheme m_visibleLineSchemeAreaOfSight = null;
        private IDNMcObjectScheme m_nonVisibleLineSchemeAreaOfSight = null;

        private IDNMcObjectSchemeItem m_SightSchemeItemAOS = null;

        private List<MCTObjectsAreaOfSight> m_lstCalcParamsAreaOfSight = new List<MCTObjectsAreaOfSight>();
        private List<string> m_lstCalcNamesAreaOfSight = new List<string>();
        private MCTObjectsAreaOfSight m_currCalcParams;
        private List<IDNAreaOfSight> m_lstIAreaOfSight = new List<IDNAreaOfSight>();
        private IDNAreaOfSight m_AreaOfSightForMultipleScouter;
        private DNSMcBColor[] m_colors = new DNSMcBColor[(int)DNEPointVisibility._EPV_NUM];
        private DNEPointVisibility m_currPointVisibility = DNEPointVisibility._EPV_NUM;
        private bool m_IsSetAreaOfSightUserParams = false;

        private List<MCTSAreaOfSightMatrixData> m_lstSAreaOfSightMatrix = new List<MCTSAreaOfSightMatrixData>();
        private DNSAreaOfSightMatrix m_currSAreaOfSightMatrix;

        private MCTAreaOfSightDrawObjectParams m_currDrawObjectParams;
        private bool m_isUpdateSightObject = false;
        private IDNMcObject m_SightObject;
        private bool m_rbSightObject = false;

        private bool bIsDrawNewResult = false;

        private bool m_isExtremeUpdatePolygon = false;
        private bool m_isTALUpdateLine = false;

        private IDNMcObject objRayIntersectionLine;
        private int mCounterAreaOfSight = 0;
        private Dictionary<int, MCTAreaOfSightSelectUserShowResults> m_dicAreaOfSightSelectUserShowResults = new Dictionary<int, MCTAreaOfSightSelectUserShowResults>();
        private IDNMcMapViewport m_currViewport;
        private DNSMcVector3D m_CurrScouterPoint;

        private IDNMcGridConverter dNMcGridConverterOMToVP;

        // Height Along Line
        private IDNMcObject m_HALReturnObject;
        private IDNMcObjectSchemeItem m_HALReturnItem;

        private IDNMcObject m_HPReturnObject;
        private IDNMcObjectSchemeItem m_HPReturnItem;
        private bool m_IsStandaloneSQ = false;

        // Traversability Along Line
        private IDNMcObject m_TALReturnObject;
        private IDNMcObjectSchemeItem m_TALReturnItem;
        private List<IDNMcTraversabilityMapLayer> m_lstTraversabilityMapLayers = new List<IDNMcTraversabilityMapLayer>();
        private List<string> m_lstTraversabilityMapLayersText = new List<string>();
        private List<DNSTraversabilityPoint> m_lstTraversabilityPoint = new List<DNSTraversabilityPoint>();

        // Get Raster Layer Color By Point
        private List<IDNMcRasterMapLayer> m_lstRasterMapLayers = new List<IDNMcRasterMapLayer>();
        private List<string> m_lstRasterMapLayersText = new List<string>();
        private DNSCreateDataSQ m_StandaloneCreateData;
        private IDNMcMapTerrain[] m_StandaloneMapTerrains;
        private StreamWriter stw = null;

        private MCMapWorldTreeViewForm m_TreeViewDisplayForm;
        private bool m_ExtremeHeightPoints_IsRelativeToDtm = true;
        private IDNMcDtmMapLayer[] m_QuerySecondaryDtmLayers;

        public SpatialQueriesForm(IDNMcSpatialQueries SQItem,
            DNSCreateDataSQ CreateData,
            IDNMcMapTerrain[] apTerrains,
            IDNMcMapViewport currentViewport)
        {
            m_currViewport = currentViewport;
            m_IsStandaloneSQ = true;
            m_StandaloneCreateData = CreateData;
            m_StandaloneMapTerrains = apTerrains;
            m_currSQ = SQItem;

            LoadForm();

        }

        public SpatialQueriesForm(IDNMcSpatialQueries SQItem, MCMapWorldTreeViewForm treeViewDisplayForm)
        {
            m_currSQ = SQItem;
            m_IsStandaloneSQ = false;
            m_currViewport = (IDNMcMapViewport)SQItem;

            LoadForm();
            m_TreeViewDisplayForm = treeViewDisplayForm;


        }

        private void LoadForm()
        {
            m_IdxTargetFound = 1;
            m_lTargetsFound = new List<DNSTargetFound>();
            if (m_currViewport != null && m_currViewport.OverlayManager != null)
            {
                m_currActiveOverlay = Manager_MCTSymbology.GetTempOverlay(m_currViewport.OverlayManager);
            }

            InitializeComponent();

            ctrlTraversabilityAlongLinePoints.SetUpdateDelegate(UpdateTALPoints);
            ctrlExtremeHeightPoints.SetUpdateDelegate(UpdateEHPPoints);
            ctrlExtremeHeightPoints.SetIsRelativeToDTM(m_ExtremeHeightPoints_IsRelativeToDtm);

            dgvAsyncQueryResults.Rows.Clear();
          
            CheckAOSIsScouterHeightAbsolute();

            lstTerrain.DisplayMember = "TerrainNameList";
            lstTerrain.ValueMember = "TerrainValueList";

            ctrlRelativeHeightPt._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointTerrainAngles._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlRayOrigin._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlRayDestination._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointAOSScouter._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            //ctrlSamplePointAOSTargetPoints._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointLOSScouter._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointLOSTarget._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlLocationFrstOrgnPt._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePoint1._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePoint2._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePoint3._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePoint4._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointAOSPointVisibility._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            //ctrlSamplePointAOSScouter._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT; ;
            ctrlSamplePointFirstPoint._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            //ctrlSamplePointAOSTargetPoints._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointFirstPoint._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT; ;
            ctrlSamplePointLOSScouter._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT; ;
            ctrlSamplePointLOSTarget._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointMultipleScouter._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            ctrlSamplePointScouter._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;
            //ctrlSamplePointTerrainAngles._QueryPrecision = DNEQueryPrecision._EQP_DEFAULT;

            chxLOSIsTargetHeightAbsolute_CheckedChanged(null, null);
            chxLOSIsScouterHeightAbsolute_CheckedChanged(null, null);
            chxMinimalScouterHeight_CheckedChanged(null, null);
            chxMinimalTargetHeight_CheckedChanged(null, null);

            if (MCTMapFormManager.MapForm != null)
            {
                m_EditMode = MCTMapFormManager.MapForm.EditMode;
            }
            cmbQualityParamsTargetTypes.Items.AddRange(Enum.GetNames(typeof(DNETargetType)));
            cmbQualityParamsTargetTypes.Text = DNETargetType._ETT_STANDING.ToString();

            SetAreaOfSightColors();

            try
            {
                m_QuerySecondaryDtmLayers = m_currSQ.GetQuerySecondaryDtmLayers();
                foreach(IDNMcDtmMapLayer mcDtmMapLayer in m_QuerySecondaryDtmLayers)
                {
                    lstQuerySecondaryDtmLayers.Items.Add(Manager_MCNames.GetNameByObject(mcDtmMapLayer));
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetQuerySecondaryDtmLayers", McEx);
            }

            if (m_currActiveOverlay != null)
            {
                try
                {
                    m_CurrCoordSysOM = m_currActiveOverlay.GetOverlayManager().GetCoordinateSystemDefinition();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetOverlayManager", McEx);
                }
                try
                {
                    mGeographicCalculations = DNMcGeographicCalculations.Create(m_CurrCoordSysOM);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcGeographicCalculations.Create", McEx);
                }
                
                try
                {
                    dNMcGridConverterOMToVP = DNMcGridConverter.Create(m_CurrCoordSysOM, m_currSQ.CoordinateSystem, false);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcGridConverter.Create", McEx);
                }

            }
            List<string> ls = new List<string>();
            ls.Add(DNELayerKind._ELK_DTM.ToString());
            ls.Add(DNELayerKind._ELK_RASTER.ToString());
            ls.Add(DNELayerKind._ELK_STATIC_OBJECTS.ToString());
            cmbLayerTypes.Items.AddRange(ls.ToArray());
            cmbLayerTypes.Text = DNELayerKind._ELK_DTM.ToString();

            cmbEScoutersSumType.Items.AddRange(Enum.GetNames(typeof(DNEScoutersSumType)));
            cmbEScoutersSumType.Text = DNEScoutersSumType._ESST_ALL.ToString();

            cmbEScoutersSumTypeMatrices.Items.AddRange(Enum.GetNames(typeof(DNEScoutersSumType)));
            cmbEScoutersSumTypeMatrices.Text = DNEScoutersSumType._ESST_ALL.ToString();

            cbIsMultipleScouters_CheckedChanged(null, null);
            //m_IdxTraversabilityResult = 1;

            if(!m_IsStandaloneSQ)
                ctrlQueryParams1.LoadItem(m_currViewport.OverlayManager);
            else if(m_StandaloneCreateData.pOverlayManager != null)
                ctrlQueryParams1.LoadItem(m_StandaloneCreateData.pOverlayManager);

            ctrlPointsGridMultipleScouters.ChangeSize(2);
            ctrlPointsGridSightObject.ChangeSize(4);
            ctrlPointsGridSightObject.HideZ();
            ctrlTraversabilityAlongLinePoints.HideZ();
            ctrlExtremeHeightPoints.HideZ();

            rgbSightObjectEllipse.DisableChildrenIfUnchecked = true;
            rgbSightObjectRect.DisableChildrenIfUnchecked = true;
            rgbSightObjectRect.Checked = true;
            rgbSightObjectRect.Checked = false;
            rgbSightObjectEllipse.Checked = true;

            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void CheckMultiThreadEnabled()
        {
            bool enabled = !(!m_IsStandaloneSQ && QueryParams.eTerrainPrecision == DNEQueryPrecision._EQP_DEFAULT);

            foreach (Control ctrl in groupBox1.Controls)
                if (ctrl != label69)
                    ctrl.Enabled = enabled;

            if (!enabled)
                label69.ForeColor = Color.Red;

            label69.Visible = enabled ? false : true;
        }

        private void CheckIsChangeCurrentViewport()
        {
            if (m_IsStandaloneSQ)
            {
                if (m_currViewport != null &&
                    (MCTMapFormManager.MapForm == null || (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport == null)))
                {
                    RemoveObjectList();
                    m_currViewport = null;
                    m_currActiveOverlay = null;
                    m_EditMode = null;
                }
                else if (MCTMapFormManager.MapForm != null && MCTMapFormManager.MapForm.Viewport != null && m_currViewport == null)
                {
                    m_currViewport = MCTMapFormManager.MapForm.Viewport;
                    m_currActiveOverlay = Manager_MCOverlayManager.GetActiveOverlayOfOverlayManager(m_currViewport.OverlayManager);
                    m_EditMode = MCTMapFormManager.MapForm.EditMode;
                }
            }
        }

        private void SetAreaOfSightColors()
        {
            List<string> names = Enum.GetNames(typeof(DNEPointVisibility)).ToList();
            names.Remove(DNEPointVisibility._EPV_ASYNC_CALCULATING.ToString());
            names.Remove(DNEPointVisibility._EPV_NUM.ToString());

            cmbAOSColors.Items.AddRange(names.ToArray());

            SetAreaOfSightColors(new DNSMcBColor(0, 255, 0, 192),
                new DNSMcBColor(255, 0, 0, 192),
                new DNSMcBColor(255, 0, 255, 192),
                new DNSMcBColor(128, 128, 128, 0),
                new DNSMcBColor(255, 255, 0, 192));
        }
        private void SetAreaOfSightColors(DNSMcBColor seenColor, DNSMcBColor unseenColor,
            DNSMcBColor unknownColor, DNSMcBColor outOfQueryAreaColor, DNSMcBColor seenStaticObjectsColor)
        {
             m_colors[(int)DNEPointVisibility._EPV_SEEN] = seenColor; 
            m_colors[(int)DNEPointVisibility._EPV_UNSEEN] = unseenColor; 
            m_colors[(int)DNEPointVisibility._EPV_UNKNOWN] = unknownColor;
            m_colors[(int)DNEPointVisibility._EPV_OUT_OF_QUERY_AREA] = outOfQueryAreaColor; 
            m_colors[(int)DNEPointVisibility._EPV_SEEN_STATIC_OBJECT] = seenStaticObjectsColor; 
            //m_colors[(int)DNEPointVisibility._EPV_ASYNC_CALCULATING] = new DNSMcBColor(0, 150, 150, 192); 

            cmbAOSColors.Text = DNEPointVisibility._EPV_SEEN.ToString();

            picbColor.BackColor = Color.FromArgb(255, seenColor.r, seenColor.g, seenColor.b);
            numUDAlphaColor.Value = seenColor.a;
            m_currPointVisibility = DNEPointVisibility._EPV_SEEN;
        }

        private void cmbAOSColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveAOSColor();
        }

        private void SaveAOSColor()
        {
            try
            {
                if (cmbAOSColors.Text != "" && !m_IsSetAreaOfSightUserParams)
                {
                    if (m_currPointVisibility != DNEPointVisibility._EPV_NUM)
                    {
                        DNSMcBColor mcCurrBColor = new DNSMcBColor(picbColor.BackColor.R, picbColor.BackColor.G, picbColor.BackColor.B, (byte)numUDAlphaColor.Value);
                        m_colors[(int)m_currPointVisibility] = mcCurrBColor;
                    }

                    m_currPointVisibility = (DNEPointVisibility)Enum.Parse(typeof(DNEPointVisibility), cmbAOSColors.Text); ;
                    DNSMcBColor mcBColor = m_colors[(int)m_currPointVisibility];
                    picbColor.BackColor = Color.FromArgb(255, mcBColor.r, mcBColor.g, mcBColor.b);
                    numUDAlphaColor.Value = mcBColor.a;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occurred when save area of sight color");
            }
        }

        private Form GetParentForm(Control ctr)
        {
            if (ctr.Parent is Form)
                return ctr.Parent as Form;
            else
                return GetParentForm(ctr.Parent);
        }

        public DNSQueryParams QueryParams
        {
            get { return ctrlQueryParams1.GetQueryParams(); }
        }

        private void btnGetTerrainHeight_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            RemoveObjectList();
            bool pbHeightFound = false;
            double pHeight = 0;
            DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl3DTerrainHeightPt.GetVector3D());
            if (cbTerrainHeightAsync.Checked)
                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetTerrainHeight);
            else
                QueryParams.pAsyncQueryCallback = null;
            try
            {
                m_currSQ.GetTerrainHeight(ConvertPointFromOMtoVP(ctrl3DTerrainHeightPt.GetVector3D()),
                                                    out pbHeightFound,
                                                    out pHeight,
                                                    normal,
                                                    QueryParams);
                if (!cbTerrainHeightAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(pbHeightFound);
                    listResults.Add(pHeight);
                    listResults.Add(normal.Value);

                    AddResultToList(AsyncQueryFunctionName.GetTerrainHeight, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetTerrainHeight, McEx.ErrorCode, cbTerrainHeightAsync.Checked, listInputs);
                MapCore.Common.Utilities.ShowErrorMessage("GetTerrainHeight", McEx);
            }
        }

        private void GetTerrainHeightResult(bool isAsync, bool pbHeightFound, double pHeight, DNSMcVector3D? normal, DNSMcVector3D TerrainHeightPt)
        {
            chxHeightFound.Checked = pbHeightFound;
            if (pbHeightFound)
            {
                ntxHeight.SetDouble(pHeight);
                ctrl3DRequestedNormal.SetVector3D( normal.Value);
            }
            else
            {
                ntxHeight.Text = "";
                ctrl3DRequestedNormal.SetVector3D( new DNSMcVector3D());
            }
        }

        private void btnGetRayIntersectionTarget_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            RayIntersectionCalcDirection();

            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl3DRayOrigin.GetVector3D());
            listInputs.Add(ctrl3DRayDirection.GetVector3D());
            listInputs.Add(ntxMaxDistance.GetDouble());
            listInputs.Add(ctrl3DRayDestination.GetVector3D());

            if (cbRayIntersectionAsync.Checked)
                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetRayIntersectionTargets);
            else
                QueryParams.pAsyncQueryCallback = null;
            try
            {
                m_lTargetsFound = new List<DNSTargetFound>(m_currSQ.GetRayIntersectionTargets(ConvertPointFromOMtoVP(ctrl3DRayOrigin.GetVector3D()),
                                                                        ctrl3DRayDirection.GetVector3D(),
                                                                        ntxMaxDistance.GetDouble(),
                                                                        QueryParams));

                if (!cbRayIntersectionAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(m_lTargetsFound);

                    AddResultToList(AsyncQueryFunctionName.GetRayIntersectionTargets, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetRayIntersectionTargets, McEx.ErrorCode, cbRayIntersectionAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetRayIntersectionTargets", McEx);
            }
        }

        private void GetRayIntersectionTargetsResult(List<DNSTargetFound> lTargetsFound)
        {
            m_lTargetsFound = lTargetsFound;
            if (m_lTargetsFound.Count > 0)
            {
                LoadIntersectionTergetFoundParams(0);
                UpdateCounterLable();
            }
            else
            {
                txtIntersectionTargetType.Text = "";
                ctrl3DVectorIntersectionPoint.SetVector3D( new DNSMcVector3D());
                txtIntersectionCoordSystem.Text = "";
                ntxTerrainHashCode.SetString("Null");
                ntxLayerHashCode.SetString("Null");
                ntxTargetIndex.SetUInt32(0);
                ntxObjectHashCode.SetString("Null");
                ntxItemHashCode.SetString("Null");

                txtItemPart.Text = "";
                ntxPartIndex.SetUInt32(0);
                lblTargetCounter.Text = "<0/0>";
            }
        }

        private void btnGetRayIntersection_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            RayIntersectionCalcDirection();
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl3DRayOrigin.GetVector3D());
            listInputs.Add(ctrl3DRayDirection.GetVector3D());
            listInputs.Add(ntxMaxDistance.GetDouble());
            listInputs.Add(ctrl3DRayDestination.GetVector3D());

            if (cbRayIntersectionAsync.Checked)
                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetRayIntersection);
            else
                QueryParams.pAsyncQueryCallback = null;
            try
            {
                DNMcNullableOut<DNSMcVector3D> intersectionPt = new DNMcNullableOut<DNSMcVector3D>();
                DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
                bool isIntersect = false;
                DNMcNullableOut<double> dist = new DNMcNullableOut<double>();
                m_currSQ.GetRayIntersection(ConvertPointFromOMtoVP(ctrl3DRayOrigin.GetVector3D()),
                                            ctrl3DRayDirection.GetVector3D(),
                                            ntxMaxDistance.GetDouble(),
                                            out isIntersect,
                                            intersectionPt,
                                            normal,
                                            dist,
                                            QueryParams);
                if (!cbRayIntersectionAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(isIntersect);
                    listResults.Add(intersectionPt.Value);
                    listResults.Add(normal.Value);
                    listResults.Add(dist.Value);

                    AddResultToList(AsyncQueryFunctionName.GetRayIntersection, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetRayIntersection, McEx.ErrorCode, cbRayIntersectionAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetRayIntersection", McEx);
            }
        }

        private void GetRayIntersectionResults(bool isIntersect, DNSMcVector3D? intersectionPt, DNSMcVector3D? normal, double? dist)
        {
            chxIsIntersectionFound.Checked = isIntersect;

            if (isIntersect)
            {
                ctrl3DIntersectionPt.SetVector3D( intersectionPt.Value);
                ctrl3DNormalRayIntersection.SetVector3D( normal.Value);
                ntxDistance.SetDouble((double)dist);
            }
            else
            {
                ctrl3DIntersectionPt.SetVector3D( new DNSMcVector3D());
                ctrl3DNormalRayIntersection.SetVector3D( new DNSMcVector3D());
                ntxDistance.SetDouble(0);
            }
        }

        private void GetRayIntersectionShowInputs(bool isAsync, List<Object> listInputs)
        {
            cbRayIntersectionAsync.Checked = isAsync;
            ctrl3DRayOrigin.SetVector3D( (DNSMcVector3D)listInputs[0]);
            ctrl3DRayDirection.SetVector3D( (DNSMcVector3D)listInputs[1]);
            ntxMaxDistance.SetDouble((double)listInputs[2]);
            ctrl3DRayDestination.SetVector3D( (DNSMcVector3D)listInputs[3]);

            DrawRay();
        }

        public void ExitAction(int nExitCode)
        {
            /*MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.ExitActionEvent -= new ExitActionEventArgs(ExitAction);

            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResultsGetHeightAlongLine);
*/

        }

        private void btnDrawLine_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                RemoveObject(m_HALReturnObject);
                RemoveObjectList();
                this.Hide();

                //MCTMapFormManager.MapForm.EditModeManagerCallback.ExitActionEvent += new ExitActionEventArgs(ExitAction);
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResultsGetHeightAlongLine);
                m_ExitStatus = 0;

                try
                {
                    IDNMcObjectSchemeItem ObjSchemeItem;
                    IDNMcObject obj;
                    CreateLine(out obj, out ObjSchemeItem);

                    m_EditMode.StartInitObject(obj, ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("StartInitObject", McEx);
                    this.Show();
                }
            }
        }

        private void btnGetHeightAlongLine_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_HALReturnItem != null)
            {
                DNSMcVector3D[] points = m_HALReturnObject.GetLocationPoints(0);
                List<object> listInput = new List<object>();
                listInput.Add(points);
                try
                {
                    float[] slops = new float[0];
                    DNSSlopesData SlopesData;
                    if (cbTerrainHeightAlongLineAsync.Checked)
                        QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInput, AsyncQueryFunctionName.GetTerrainHeightsAlongLine);
                    else
                        QueryParams.pAsyncQueryCallback = null;
                    //m_HALReturnObject.Remove();

                    DNSMcVector3D[] sqPoints = new DNSMcVector3D[points.Length];
                    for (int i = 0; i < points.Length; i++)
                    {
                        sqPoints[i] = ConvertPointFromOMtoVP(points[i]);
                    }

                    DNSMcVector3D[] resultPoints = m_currSQ.GetTerrainHeightsAlongLine(sqPoints,
                                                                                        out slops,
                                                                                        out SlopesData,
                                                                                        QueryParams);

                    if (!cbTerrainHeightAlongLineAsync.Checked)
                    {
                        List<object> listResults = new List<object>();
                        listResults.Add(slops);
                        listResults.Add(SlopesData);
                        listResults.Add(resultPoints);

                        AddResultToList(AsyncQueryFunctionName.GetTerrainHeightsAlongLine, listResults, false, listInput);
                    }


                }
                catch (MapCoreException McEx)
                {
                    AddErrorToList(AsyncQueryFunctionName.GetTerrainHeightsAlongLine, McEx.ErrorCode, cbTerrainHeightAlongLineAsync.Checked, listInput);
                    Utilities.ShowErrorMessage("GetTerrainHeightsAlongLine", McEx);
                    Show();
                }
            }
        }

        private void CreateLine(out IDNMcObject obj, out IDNMcObjectSchemeItem ObjSchemeItem, DNSMcVector3D[] points = null)
        {

            DNSMcVector3D[] locationPoints;
            if (points != null)
                locationPoints = points;
            else
                locationPoints = new DNSMcVector3D[0];

            DNEItemSubTypeFlags subTypeFlags = 0;
            subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
            subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;


            ObjSchemeItem = DNMcLineItem.Create(subTypeFlags,
                                                DNELineStyle._ELS_SOLID,
                                                DNSMcBColor.bcBlackOpaque,
                                                3f);
            obj = DNMcObject.Create(m_currActiveOverlay,
                                    ObjSchemeItem,
                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                    locationPoints,
                                    false);
        }

        private void GetTerrainHeightsAlongLineResult(bool isAsync, float[] slops, DNSSlopesData? SlopesData, DNSMcVector3D[] resultPoints)
        {
            if (m_currActiveOverlay != null)
            {
                cbTerrainHeightAlongLineAsync.Checked = isAsync;
                if (SlopesData.HasValue)
                {
                    ntxMaxSlope.SetFloat(SlopesData.Value.fMaxSlope);
                    ntxMinSlope.SetFloat(SlopesData.Value.fMinSlope);
                    ntxHeightDelta.SetFloat(SlopesData.Value.fHeightDelta);
                }
                //Draw the method result
                IDNMcLineItem ObjLineItem = DNMcLineItem.Create(
                    DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ACCURATE_3D_SCREEN_WIDTH,
                    DNELineStyle._ELS_SOLID,
                    DNSMcBColor.bcBlackOpaque,
                    3f,
                    null,
                    new DNSMcFVector2D(0, -1),
                    1f);

                DNSMcVector3D[] omPoints = new DNSMcVector3D[resultPoints.Length];
                for (int i = 0; i < resultPoints.Length; i++)
                    omPoints[i] = ConvertPointFromVPtoOM(resultPoints[i]);

                IDNMcObject objRes = DNMcObject.Create(m_currActiveOverlay,
                                                    ObjLineItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    omPoints,
                                                    false);
                mListObjects.Add(objRes);

                ObjLineItem.SetBlockedTransparency((byte)128, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                ObjLineItem.SetLineColor(new DNSMcBColor((byte)255, 0, 0, (byte)255), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                FontDialog Fd = new FontDialog();
                DNSMcLogFont logFont = new DNSMcLogFont();
                Fd.Font.ToLogFont(logFont);
                IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                IDNMcTextItem textItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                DefaultFont);

                IDNMcObjectScheme scheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(), textItem, DNEMcPointCoordSystem._EPCS_WORLD, false);
                textItem.SetText(new DNMcVariantString("slop", true), 1, false);
                textItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                DNSMcVector3D[] points = new DNSMcVector3D[1];
                for (int i = 0; i < resultPoints.Length; i++)
                {
                    points[0] = omPoints[i];

                    IDNMcObject slopObj = DNMcObject.Create(m_currActiveOverlay,
                                                            scheme,
                                                            points);

                    slopObj.SetStringProperty(1, new DNMcVariantString(slops[i].ToString(), true));
                    mListObjects.Add(slopObj);
                }
            }
        }

        internal IDNMcMapViewport GetActiveViewport()
        {
            return m_currViewport;
        }

        private void UpdateEHPPoints()
        {
            btnExtremeUpdatePolygon_Click(null, null);
        }

        private void btnExtremeUpdatePolygon_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            m_isExtremeUpdatePolygon = true;
            CreateExtremePolygon();
            m_isExtremeUpdatePolygon = false;
        }

        private void CreateExtremePolygon()
        {
            DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
            if (m_isExtremeUpdatePolygon)
            {
                if(ctrlExtremeHeightPoints.GetPoints(out locationPoints))
                    CreateExtremePolygon(locationPoints);
            
            }
     
        }

        private void RemoveExtremePolygon()
        {
            try
            {
                if (m_HPReturnObject != null)
                {
                    m_HPReturnObject.Dispose();
                    m_HPReturnObject.Remove();
                    m_HPReturnObject = null;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Dispose and Remove Object", McEx);
            }

        }

        private void CreateExtremePolygon(DNSMcVector3D[] locationPoints)
        {
            if (m_currActiveOverlay != null)
            {
                RemoveExtremePolygon();
                RemoveObjectList();

                try
                {
                    IDNMcObjectSchemeItem ObjSchemeItem;
                    IDNMcObject obj;
                    DrawPolygonToExtremeHeightPoints(out ObjSchemeItem, out obj, locationPoints);
                    m_HPReturnObject = obj;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DrawPolygon", McEx);
                    this.Show();
                }
            }
        }

        private void DrawPolygonToExtremeHeightPoints()
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                this.Hide();
                // RemoveObject(m_HPReturnObject);
                RemoveObjectList();
                this.Hide();
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResultsGetHeightPolygon);
                m_ExitStatus = 0;

                List<object> listInput = new List<object>();
                try
                {
                    IDNMcObjectSchemeItem ObjSchemeItem;
                    IDNMcObject obj;
                    DrawPolygonToExtremeHeightPoints(out ObjSchemeItem, out obj);
                    //mListObjects.Add(obj);

                    m_EditMode.StartInitObject(obj, ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DrawPolygon", McEx);
                    this.Show();
                }
            }
        }

        private void btnDrawPolygon_Click(object sender, EventArgs e)
        {
            DrawPolygonToExtremeHeightPoints();
        }

        private void DrawPolygonToExtremeHeightPoints(out IDNMcObjectSchemeItem ObjSchemeItem,
            out IDNMcObject obj,
            DNSMcVector3D[] plocationPoints = null)
        {
            if (m_currActiveOverlay != null)
            {
                RemoveExtremePolygon();

                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
                if (plocationPoints != null)
                    locationPoints = plocationPoints;

                DNEItemSubTypeFlags subTypeFlags = 0;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
                subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;

                ObjSchemeItem = DNMcPolygonItem.Create(subTypeFlags,
                                                        DNELineStyle._ELS_SOLID,
                                                        DNSMcBColor.bcBlackOpaque,
                                                        3f,
                                                        null,
                                                        new DNSMcFVector2D(0, -1),
                                                        1f,
                                                        DNEFillStyle._EFS_NONE,
                                                        DNSMcBColor.bcBlackOpaque,
                                                        null,
                                                        new DNSMcFVector2D(1, 1));

                obj = DNMcObject.Create(m_currActiveOverlay,
                                                    ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    locationPoints,
                                                    m_ExtremeHeightPoints_IsRelativeToDtm);
            }
            else
            {
                obj = null; ObjSchemeItem = null;
            }
        }

        private void GetExtremeHeightPointsInPolygonResults(bool isAsync, bool bPointsFound, DNSMcVector3D HighestPt, DNSMcVector3D LowestPt, DNSMcVector3D[] plocationPoints)
        {
            cbExtremeHeightPointsInPolygonAsync.Checked = isAsync;
            IDNMcObjectSchemeItem ObjSchemeItem;
            IDNMcObject obj;
            if (m_currActiveOverlay != null)
            {
                DrawPolygonToExtremeHeightPoints(out ObjSchemeItem, out obj, plocationPoints);
                mListObjects.Add(obj);
            }
            chxPointsFound.Checked = bPointsFound;

            if (!bPointsFound)
            {
                ctrl3DVectorHighestPt.SetVector3D( new DNSMcVector3D());
                ctrl3DVectorLowestPt.SetVector3D( new DNSMcVector3D());
            }
            else
            {
                ctrl3DVectorHighestPt.SetVector3D( HighestPt);
                ctrl3DVectorLowestPt.SetVector3D( LowestPt);

                //Convert points to OM coordinates
                DNSMcVector3D highest, lowest;
                if (m_currViewport != null)
                {
                    highest = m_currViewport.ViewportToOverlayManagerWorld(HighestPt);
                    lowest = m_currViewport.ViewportToOverlayManagerWorld(LowestPt);

                    //Placing flag in the founded points
                    DNSMcVector3D[] locationPointsHighest = new DNSMcVector3D[2];
                    locationPointsHighest[0] = highest;
                    locationPointsHighest[1].x = highest.x;
                    locationPointsHighest[1].y = highest.y + 1;
                    locationPointsHighest[1].z = highest.z + 30;

                    FontDialog Fd = new FontDialog();
                    DNSMcLogFont logFont = new DNSMcLogFont();
                    Fd.Font.ToLogFont(logFont);

                    IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, true));

                    IDNMcObjectSchemeItem ObjLineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                new DNSMcBColor(255, 0, 0, 255),
                                                                                3f,
                                                                                null,
                                                                                new DNSMcFVector2D(0, -1),
                                                                                1f);

                    IDNMcObject ObjLineItemHighest = DNMcObject.Create(m_currActiveOverlay,
                                                        ObjLineItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPointsHighest,
                                                        false);
                    mListObjects.Add(ObjLineItemHighest);
                    IDNMcTextItem ObjTextItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                DefaultFont,
                                                                                new DNSMcFVector2D(1, 1),
                                                                                DNENeverUpsideDownMode._ENUDM_NONE,
                                                                                DNEAxisXAlignment._EXA_CENTER,
                                                                                DNEBoundingRectanglePoint._EBRP_CENTER,
                                                                                true,
                                                                                0,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                new DNSMcBColor(0, 128, 0, 255));

                    ObjTextItem.SetText(new DNMcVariantString("Highest Pt", true), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ObjTextItem.Connect(ObjLineItem);
                    ObjTextItem.SetAttachPointType(0, DNEAttachPointType._EAPT_INDEX_POINTS, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID); 
                    ObjTextItem.SetAttachPointIndex(0, 1, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    DNSMcVector3D[] locationPointsLowest = new DNSMcVector3D[2];
                    locationPointsLowest[0] = lowest;
                    locationPointsLowest[1].x = lowest.x;
                    locationPointsLowest[1].y = lowest.y + 1;
                    locationPointsLowest[1].z = lowest.z + 30;

                    IDNMcObjectSchemeItem ObjLineItemLowest = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                        DNELineStyle._ELS_SOLID,
                                                                                        new DNSMcBColor(255, 0, 0, 255),
                                                                                        3f,
                                                                                        null,
                                                                                        new DNSMcFVector2D(0, -1),
                                                                                        1f);

                    IDNMcObject objLowest = DNMcObject.Create(m_currActiveOverlay,
                                                                ObjLineItemLowest,
                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                locationPointsLowest,
                                                                false);
                    mListObjects.Add(objLowest);
                    IDNMcTextItem ObjTextItemLowest = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                                DefaultFont,
                                                                                new DNSMcFVector2D(1, 1),
                                                                                DNENeverUpsideDownMode._ENUDM_NONE,
                                                                                DNEAxisXAlignment._EXA_CENTER,
                                                                                DNEBoundingRectanglePoint._EBRP_CENTER,
                                                                                true,
                                                                                0,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                new DNSMcBColor(0, 128, 0, 255));

                    ObjTextItemLowest.SetText(new DNMcVariantString("Lowest Pt", true), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ObjTextItemLowest.Connect(ObjLineItemLowest);
                    ObjTextItemLowest.SetAttachPointType(0, DNEAttachPointType._EAPT_INDEX_POINTS, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID); 
                    ObjTextItemLowest.SetAttachPointIndex(0, 1, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                }
            }
        }

        private void btnTerrainAnglesDrawLine_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                this.Hide();
                RemoveObjectList();

                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResults);
                m_ExitStatus = 0;

                try
                {
                    IDNMcObjectSchemeItem ObjSchemeItem;
                    IDNMcObject obj;
                    CreateLine(out obj, out ObjSchemeItem);

                    m_EditMode = MCTMapFormManager.MapForm.EditMode;
                    m_EditMode.StartInitObject(obj, ObjSchemeItem);

                    while (m_ExitStatus == 0)
                        Application.DoEvents();

                    if (m_ReturnObject != null)
                    {
                        try
                        {
                            DNSMcVector3D[] linePoints = m_ReturnObject.GetLocationPoints(0);
                            if (linePoints != null && linePoints.Length >= 2)
                            {
                                ctrl3DVectorTerrainAnglesPt.SetVector3D( linePoints[0]);

                                DNMcNullableOut<double> azimuth = new DNMcNullableOut<double>();
                                mGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations(linePoints[0], linePoints[1], azimuth);

                                ntxTerrainAnglesAzimuth.SetDouble(azimuth.Value);

                                obj.Dispose();
                                obj.Remove();
                            }
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("AzimuthAndDistanceBetweenTwoLocations", McEx);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Draw Line", McEx);
                }

                this.Show();
            }
        }

        private void btnGetTerrainAngles_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            RemoveObjectList();
            List<object> listInputs = new List<object>();
            try
            {
                double pitch, roll;

                listInputs.Add(ctrl3DVectorTerrainAnglesPt.GetVector3D());
                listInputs.Add(ntxTerrainAnglesAzimuth.GetDouble());

                if (cbTerrainAnglesAsync.Checked)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetTerrainAngles);
                else
                    QueryParams.pAsyncQueryCallback = null;

                m_currSQ.GetTerrainAngles(ConvertPointFromOMtoVP(ctrl3DVectorTerrainAnglesPt.GetVector3D()),
                                            ntxTerrainAnglesAzimuth.GetDouble(),
                                            out pitch,
                                            out roll,
                                            QueryParams);

                if (!cbTerrainAnglesAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(pitch);
                    listResults.Add(roll);

                    AddResultToList(AsyncQueryFunctionName.GetTerrainAngles, listResults, false, listInputs);
                }

            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetTerrainAngles, McEx.ErrorCode, cbTerrainAnglesAsync.Checked, listInputs);
                MapCore.Common.Utilities.ShowErrorMessage("GetTerrainAngles", McEx);
            }
        }

        private void btnGetLineOfSight_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            RemoveObjectList();
            List<object> listInputs = new List<object>();
            try
            {
                listInputs.Add(ctrl3DVectorLOSScouter.GetVector3D());
                listInputs.Add(chxLOSIsScouterHeightAbsolute.Checked);
                listInputs.Add(ctrl3DVectorLOSTarget.GetVector3D());
                listInputs.Add(chxLOSIsTargetHeightAbsolute.Checked);
                listInputs.Add(ntxLOSMaxPitchAngle.GetDouble());
                listInputs.Add(ntxLOSMinPitchAngle.GetDouble());
                listInputs.Add(chxMinimalScouterHeight.Checked);
                listInputs.Add(chxMinimalTargetHeight.Checked);
                bool isTargetVisible;
                DNMcNullableOut<double> minimalTargetHeightForVisibility = chxMinimalTargetHeight.Checked? new DNMcNullableOut<double>() : null;
                DNMcNullableOut<double> minimalScouterHeightForVisibility = chxMinimalScouterHeight.Checked? new DNMcNullableOut<double>() : null;

                if (cbGetPointVisibilityAsync.Checked)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetPointVisibility);
                else
                    QueryParams.pAsyncQueryCallback = null;

                m_currSQ.GetPointVisibility(ConvertPointFromOMtoVP(ctrl3DVectorLOSScouter.GetVector3D()),
                                            chxLOSIsScouterHeightAbsolute.Checked,
                                            ConvertPointFromOMtoVP(ctrl3DVectorLOSTarget.GetVector3D()),
                                            chxLOSIsTargetHeightAbsolute.Checked,
                                            out isTargetVisible,
                                            minimalTargetHeightForVisibility,
                                            minimalScouterHeightForVisibility,
                                            ntxLOSMaxPitchAngle.GetDouble(),
                                            ntxLOSMinPitchAngle.GetDouble(),
                                            QueryParams);

                if (!cbGetPointVisibilityAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(isTargetVisible);

                    listResults.Add(minimalTargetHeightForVisibility);
                    listResults.Add(minimalScouterHeightForVisibility);

                    AddResultToList(AsyncQueryFunctionName.GetPointVisibility, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetPointVisibility, McEx.ErrorCode, cbGetPointVisibilityAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetPointVisibility", McEx);
            }
        }

        private void GetPointVisibilityResults(bool isTargetVisible, DNMcNullableOut<double> minimalTargetHeightForVisibility, DNMcNullableOut<double> minimalScouterHeightForVisibility)
        {
            chxLOSIsTargetVisible.Checked = isTargetVisible;
            if (minimalTargetHeightForVisibility != null)
                ntxLOSMinimalTargetHeightForVisibility.SetDouble(minimalTargetHeightForVisibility.Value);
            else
                ntxLOSMinimalTargetHeightForVisibility.Text = "";

            if (minimalScouterHeightForVisibility != null)
                ntxLOSMinimalScouterHeightForVisibility.SetDouble(minimalScouterHeightForVisibility.Value);
            else
                ntxLOSMinimalScouterHeightForVisibility.Text = "";
        }

        private void GetPointVisibilityShowInputs(bool isAsync, List<Object> listInputs,
            AsyncQueryFunctionName asyncQueryFunctionName = AsyncQueryFunctionName.GetPointVisibility)
        {
            if (asyncQueryFunctionName == AsyncQueryFunctionName.GetPointVisibility)
                cbGetPointVisibilityAsync.Checked = isAsync;
            else
                cbGetLineOfSightAsync.Checked = isAsync;

            DNSMcVector3D scouter = (DNSMcVector3D)listInputs[0];
            ctrl3DVectorLOSScouter.SetVector3D(scouter);
            chxLOSIsScouterHeightAbsolute.Checked = (bool)listInputs[1];
            DNSMcVector3D target = (DNSMcVector3D)listInputs[2];
            ctrl3DVectorLOSTarget.SetVector3D(target);
            chxLOSIsTargetHeightAbsolute.Checked = (bool)listInputs[3];
            ntxLOSMaxPitchAngle.SetDouble((double)listInputs[4]);
            ntxLOSMinPitchAngle.SetDouble((double)listInputs[5]);

            if (asyncQueryFunctionName == AsyncQueryFunctionName.GetPointVisibility)
            {
                chxMinimalScouterHeight.Checked = (bool)listInputs[6];
                chxMinimalTargetHeight.Checked = (bool)listInputs[7];
            }
            DrawTextPointVisibility(scouter, "S", !chxLOSIsScouterHeightAbsolute.Checked);
            DrawTextPointVisibility(target, "T", !chxLOSIsTargetHeightAbsolute.Checked);
        }

        private void btnGetLineOfSightPoints_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            List<object> listInputs = new List<object>();
            try
            {
                listInputs.Add(ctrl3DVectorLOSScouter.GetVector3D());
                listInputs.Add(chxLOSIsScouterHeightAbsolute.Checked);
                listInputs.Add(ctrl3DVectorLOSTarget.GetVector3D());
                listInputs.Add(chxLOSIsTargetHeightAbsolute.Checked);
                listInputs.Add(ntxLOSMaxPitchAngle.GetDouble());
                listInputs.Add(ntxLOSMinPitchAngle.GetDouble());

                DNSLineOfSightPoint[] aPoints;
                double crestClearanceAngle;
                double crestClearanceDist;
                if (cbGetLineOfSightAsync.Checked)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetLineOfSight);
                else
                    QueryParams.pAsyncQueryCallback = null;

                m_currSQ.GetLineOfSight(ConvertPointFromOMtoVP(ctrl3DVectorLOSScouter.GetVector3D()),
                                        chxLOSIsScouterHeightAbsolute.Checked,
                                        ConvertPointFromOMtoVP(ctrl3DVectorLOSTarget.GetVector3D()),
                                        chxLOSIsTargetHeightAbsolute.Checked,
                                        out aPoints,
                                        out crestClearanceAngle,
                                        out crestClearanceDist,
                                        ntxLOSMaxPitchAngle.GetDouble(),
                                        ntxLOSMinPitchAngle.GetDouble(),
                                        QueryParams);

                if (!cbGetLineOfSightAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(aPoints);
                    listResults.Add(crestClearanceAngle);
                    listResults.Add(crestClearanceDist);

                    AddResultToList(AsyncQueryFunctionName.GetLineOfSight, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetLineOfSight, McEx.ErrorCode, cbGetLineOfSightAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetLineOfSight", McEx);
            }
        }

        private void GetLineOfSightResults(DNSLineOfSightPoint[] aPoints, double crestClearanceAngle, double crestClearanceDist)
        {
            ntxCrestClearanceAngle.SetDouble(crestClearanceAngle);
            ntxCrestClearanceDistance.SetDouble(crestClearanceDist);

            if (m_currActiveOverlay != null)
            {
                IDNMcObjectSchemeItem visibleLineScheme = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                new DNSMcBColor(0, 255, 0, 255),
                                                                                3);

                IDNMcObjectSchemeItem nonVisibleLineScheme = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                new DNSMcBColor(255, 0, 0, 255),
                                                                                3);

                IDNMcObjectScheme visibleScheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                visibleLineScheme,
                                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                false);

                IDNMcObjectScheme nonVisibleScheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                nonVisibleLineScheme,
                                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                false);

                ((IDNMcLineItem)visibleLineScheme).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);
                ((IDNMcLineItem)nonVisibleLineScheme).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);

                DNSMcVector3D[] locationPt = new DNSMcVector3D[2];

                for (int i = 1; i < aPoints.Length; i++)
                {
                    locationPt[0] = ConvertPointFromVPtoOM(aPoints[i - 1].Point);
                    locationPt[1] = ConvertPointFromVPtoOM(aPoints[i].Point);
                    IDNMcObject obj = null;
                    if (aPoints[i - 1].bVisible == true)
                    {
                        obj = DNMcObject.Create(m_currActiveOverlay,
                                                            visibleScheme,
                                                            locationPt);
                    }
                    else
                    {
                        obj = DNMcObject.Create(m_currActiveOverlay,
                                                            nonVisibleScheme,
                                                            locationPt);
                    }
                    mListObjects.Add(obj);
                }
            }
        }

        #region AOS

        private void ResetParamsAreaOfSight()
        {

            m_AreaOfSight = new DNMcNullableOut<IDNAreaOfSight>();
            m_LineOfSight = new DNMcNullableOut<DNSLineOfSightPoint[][]>();
            m_SeenPolygonOfSight = new DNMcNullableOut<DNSPolygonsOfSight>();
            m_UnseenPolygonOfSight = new DNMcNullableOut<DNSPolygonsOfSight>();
            m_SeenStaticObjects = new DNMcNullableOut<DNSStaticObjectsIDs[]>();

        }


        private void CreateSAreaOfSightMatrixList()
        {
            cbLstAreaOfSightMatrix.Items.Clear();
            foreach (MCTSAreaOfSightMatrixData areaOfSightMatrix in m_lstSAreaOfSightMatrix)
            {
                cbLstAreaOfSightMatrix.Items.Add(areaOfSightMatrix);
            }
        }



        private void SaveMatrixToFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File (*.csv) | *.csv";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    stw = new StreamWriter(sfd.FileName);
                    stw.WriteLine("uWidth: ," + m_currSAreaOfSightMatrix.uWidth.ToString() 
                        + ",uHeight:," + m_currSAreaOfSightMatrix.uHeight.ToString()
                        + ",fAngle:," + m_currSAreaOfSightMatrix.fAngle.ToString()
                        + ",fTargetResolutionInMeters:," + m_currSAreaOfSightMatrix.fTargetResolutionInMeters.ToString()
                        + ",fTargetResolutionInMapUnitsX:," + m_currSAreaOfSightMatrix.fTargetResolutionInMapUnitsX.ToString()
                        + ",fTargetResolutionInMapUnitsY:," + m_currSAreaOfSightMatrix.fTargetResolutionInMapUnitsY.ToString()
                        + ",LeftTopPoint:," + m_currSAreaOfSightMatrix.LeftTopPoint.x.ToString() +"," + m_currSAreaOfSightMatrix.LeftTopPoint.y.ToString() + "," + m_currSAreaOfSightMatrix.LeftTopPoint.z.ToString()
                        + ",RightTopPoint:," + m_currSAreaOfSightMatrix.RightTopPoint.x.ToString() + "," + m_currSAreaOfSightMatrix.RightTopPoint.y.ToString() + "," + m_currSAreaOfSightMatrix.RightTopPoint.z.ToString()
                        + ",LeftBottomPoint:," + m_currSAreaOfSightMatrix.LeftBottomPoint.x.ToString() + "," + m_currSAreaOfSightMatrix.LeftBottomPoint.y.ToString() + "," + m_currSAreaOfSightMatrix.LeftBottomPoint.z.ToString()
                        + ",RightBottomPoint:," + m_currSAreaOfSightMatrix.RightBottomPoint.x.ToString() + "," + m_currSAreaOfSightMatrix.RightBottomPoint.y.ToString() + "," + m_currSAreaOfSightMatrix.RightBottomPoint.z.ToString()
                        );

                    for (int i = 0; i < m_currSAreaOfSightMatrix.uHeight; i++)
                    {
                        string line = "";
                        for (int j = 0; j < m_currSAreaOfSightMatrix.uWidth; j++)
                        {
                            uint index = (uint)(i * m_currSAreaOfSightMatrix.uHeight + j);
                            if (index < m_currSAreaOfSightMatrix.aPointsVisibilityColors.Length)
                                line = line + m_currSAreaOfSightMatrix.aPointsVisibilityColors[index].dwRGBA.ToString("X") + ",";
                        }
                        stw.WriteLine(line);
                    }
                    stw.Close();
                }
                catch (UnauthorizedAccessException ex)
                {
                    ShowExpError(ex);
                    return;
                }
                catch (ArgumentNullException ex)
                {
                    ShowExpError(ex);
                    return;
                }
                catch (DirectoryNotFoundException ex)
                {
                    ShowExpError(ex);
                    return;
                }
                catch (PathTooLongException ex)
                {
                    ShowExpError(ex);
                    return;
                }
                catch (IOException ex)
                {
                    ShowExpError(ex);
                    return;
                }
                catch (EncoderFallbackException ex)
                {
                    ShowExpError(ex);
                    return;
                }
            }
        }

        private void ShowExpError(Exception ex)
        {
            MessageBox.Show("Error open/write to file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (stw != null)
                stw.Close();
        }

        private void btnCreateMatrixAreaOfSight_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (SelectedAreaOfSight != null)
            {
                try
                {
                    MCTSAreaOfSightMatrixData areaOfSightMatrixData = IsListMatrixContains(SelectedAreaOfSight);
                    if (areaOfSightMatrixData != null)
                    {
                        m_currSAreaOfSightMatrix = areaOfSightMatrixData.areaOfSightMatrix;
                    }
                    else
                    {
                        m_currSAreaOfSightMatrix = SelectedAreaOfSight.GetAreaOfSightMatrix(chxFillPointsVisibility.Checked);
                        areaOfSightMatrixData = new MCTSAreaOfSightMatrixData();
                        areaOfSightMatrixData.areaOfSight = SelectedAreaOfSight;
                        areaOfSightMatrixData.areaOfSightMatrix = m_currSAreaOfSightMatrix;
                        areaOfSightMatrixData.nameToShow = "Area Of Sight Matrix of " + m_currCalcParams.CalcName;
                        m_lstSAreaOfSightMatrix.Add(areaOfSightMatrixData);

                    }


                    CreateSAreaOfSightMatrixList();
                    DNSMcBColor[] matrixPtVisibility = m_currSAreaOfSightMatrix.aPointsVisibilityColors;

                    byte[] textureBuffer = new byte[matrixPtVisibility.Length * 4];
                    int i = 0;
                    foreach (DNSMcBColor pointType in matrixPtVisibility)
                    {
                        DNSMcBColor color = pointType;
                        if (m_currCalcParams.IsMultipleScouter)
                        {
                            uint maxNumOfScouters = m_currCalcParams.NumOfScouters; 
                                if (maxNumOfScouters == 0)
                                    maxNumOfScouters = 1;
                            if (m_AreaOfSightCalcMultipeScoutersSumType == DNEScoutersSumType._ESST_OR)
                            {
                                byte newColor = (byte)(color.dwRGBA * 255);
                                color.r = 0;
                                color.g = newColor;
                                color.b = 0;
                                
                                color.a = 192;
                            }
                            else if (m_AreaOfSightCalcMultipeScoutersSumType == DNEScoutersSumType._ESST_ADD)
                            {
                                uint numScouters = color.dwRGBA;
                                byte newColor = (byte)(numScouters * 255 / maxNumOfScouters);
                                color.r = 0;
                                color.g = newColor;
                                color.b = 0;
                                color.a = 192;
                            }
                            else if (m_AreaOfSightCalcMultipeScoutersSumType == DNEScoutersSumType._ESST_ALL)
                            {
                                uint numScouters = numberOfSetBits(color.dwRGBA);
                                byte newColor = (byte)(numScouters * 255 / maxNumOfScouters);
                                color.r = 0;
                                color.g = newColor;
                                color.b = newColor;
                                color.a = 192;
                            }
                        }
                        textureBuffer[i++] = color.r;
                        textureBuffer[i++] = color.g;
                        textureBuffer[i++] = color.b;
                        textureBuffer[i++] = color.a;
                    }
                    if (m_currViewport != null && m_currActiveOverlay != null)
                    {
                        try
                        {
                            IDNMcMemoryBufferTexture memoryBufferTexture = DNMcMemoryBufferTexture.Create(m_currSAreaOfSightMatrix.uWidth,
                                                                                                        m_currSAreaOfSightMatrix.uHeight,
                                                                                                        DNEPixelFormat._EPF_A8B8G8R8,
                                                                                                        DNETextureUsage._EU_STATIC,
                                                                                                        true,
                                                                                                        textureBuffer);


                            IDNMcPictureItem picItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_WORLD | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN, DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                memoryBufferTexture, m_currSAreaOfSightMatrix.uWidth * m_currSAreaOfSightMatrix.fTargetResolutionInMapUnitsX,
                                                                                m_currSAreaOfSightMatrix.uHeight * m_currSAreaOfSightMatrix.fTargetResolutionInMapUnitsY,
                                                                                false,
                                                                                new DNSMcBColor(255, 255, 255, 255),
                                                                                DNEBoundingRectanglePoint._EBRP_TOP_LEFT);

                            picItem.SetRotationYaw(m_currSAreaOfSightMatrix.fAngle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                            DNSMcVector3D[] locationPoints = new DNSMcVector3D[1];

                            locationPoints[0] = m_currViewport.ViewportToOverlayManagerWorld(m_currSAreaOfSightMatrix.LeftTopPoint);
                            IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                                picItem,
                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                locationPoints,
                                                false);

                            obj.GetScheme().SetName("Tester AOS Texture Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());
                            Manager_MCViewports.IndexSchemeAOS++;
                            if (m_currCalcParams.TextureGPU != null)
                            {
                                m_currCalcParams.TextureGPU.Dispose();
                                m_currCalcParams.TextureGPU.Remove();
                            }
                            m_currCalcParams.TextureGPU = obj;
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("DNMcMemoryBufferTexture.Create", McEx);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetAreaOfSightMatrix", McEx);
                }
            }
            else
            {
                MessageBox.Show("Please calc Area Of Sight first", "Calc Area Of Sight", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public MCTSAreaOfSightMatrixData IsListMatrixContains(IDNAreaOfSight dNAreaOfSight)
        {
            foreach (MCTSAreaOfSightMatrixData data in m_lstSAreaOfSightMatrix)
            {
                if (data.areaOfSight == dNAreaOfSight &&
                    data.isClone == false &&
                    data.isSum == false)
                    return data;
            }
            return null;
        }

        private void btnGetPointVisibilityAreaOfSight_Click(object sender, EventArgs e)
        {
            if (SelectedAreaOfSight != null)
            {
                try
                {
                    DNSMcVector3D selectedPoint;
                    if (m_currViewport != null)
                        selectedPoint = m_currViewport.OverlayManagerToViewportWorld(ctrl3DVectorAOSPointVisibility.GetVector3D());
                    else
                        selectedPoint = ctrl3DVectorAOSPointVisibility.GetVector3D();

                    DNSMcBColor pointColor = SelectedAreaOfSight.GetPointVisibilityColor(selectedPoint);
                    String color = "(" + pointColor.r + "," + pointColor.g + "," + pointColor.b + "," + pointColor.a + ")";
                    DNSMcBColor[] sightColors = GetSightColorParams();

                    if (sightColors[(int)DNEPointVisibility._EPV_SEEN] == pointColor)
                    {
                        txtAOSPointVisibilityAnswer.Text = "SEEN: " + color;
                    }
                    else if (sightColors[(int)DNEPointVisibility._EPV_UNSEEN] == pointColor)
                    {
                        txtAOSPointVisibilityAnswer.Text = "UNSEEN: " + color;
                    }
                    else if (sightColors[(int)DNEPointVisibility._EPV_UNKNOWN] == pointColor)
                    {
                        txtAOSPointVisibilityAnswer.Text = "UNKNOWN: " + color;
                    }
                    else if (sightColors[(int)DNEPointVisibility._EPV_OUT_OF_QUERY_AREA] == pointColor)
                    {
                        txtAOSPointVisibilityAnswer.Text = "OUT_OF_QUERY_AREA: " + color;
                    }
                    else
                    {
                        txtAOSPointVisibilityAnswer.Text = "ERROR!: " + color;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetPointVisibility", McEx);
                }
            }
            else
            {
                MessageBox.Show("Please calc Area Of Sight first", "Calc Area Of Sight", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnQualityParamsDrawLineAreaOfSight_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                this.Hide();
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResults);
                m_ExitStatus = 0;

                try
                {
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                    DNEItemSubTypeFlags subTypeFlags = 0;
                    subTypeFlags |= DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN;
                    subTypeFlags |= DNEItemSubTypeFlags._EISTF_SCREEN;


                    IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineItem.Create(subTypeFlags,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                3f);




                    IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                                        ObjSchemeItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);

                    m_EditMode.StartInitObject(obj, ObjSchemeItem);

                    while (m_ExitStatus == 0)
                        System.Windows.Forms.Application.DoEvents();
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("QualityParamsDrawLine", McEx);
                }

                this.Show();
            }
        }

        private void btnCalcCoverageAreaOfSight_Click(object sender, EventArgs e)
        {
            if (SelectedAreaOfSight != null)
            {
                if (m_ReturnItem != null)
                {
                    IDNCoverageQuality coverageQuality = null;

                    try
                    {
                        DNSQualityParams qualityParams = new DNSQualityParams();
                        qualityParams.fStandingRadius = ntxStandingRadius.GetFloat();
                        qualityParams.fWalkingRadius = ntxWalkingRadius.GetFloat();
                        qualityParams.fVehicleRadius = ntxVehicleRadius.GetFloat();
                        qualityParams.uCellFactor = ntxCellFactor.GetUInt32();

                        coverageQuality = DNCoverageQuality.Create(
                            SelectedAreaOfSight.GetAreaOfSightMatrix(true),
                            SelectedAreaOfSight.GetVisibilityColors(), qualityParams);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("DNCoverageQuality.Create", McEx);
                        return;
                    }

                    if (coverageQuality != null)
                    {
                        try
                        {
                            DNETargetType targetType = (DNETargetType)Enum.Parse(typeof(DNETargetType), cmbQualityParamsTargetTypes.Text);

                            DNSMcVector3D[] lineLocationPoints = m_ReturnObject.GetLocationPoints(0);
                            double movementAngle, dPitch;
                            (lineLocationPoints[1] - lineLocationPoints[0]).GetDegreeYawPitchFromForwardVector(out movementAngle, out dPitch);

                            ntxCoverageQuality.SetShort(coverageQuality.GetQuality(lineLocationPoints[0],
                                                                                        targetType,
                                                                                        (float)movementAngle));
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("GetQuality", McEx);
                            return;
                        }
                    }
                }
            }

            this.Show();
        }

        private void CreatePolygon()
        {
            DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
            bool isCreatePolygon = true;
            if (m_isUpdateSightObject)
            {
                isCreatePolygon = ctrlPointsGridSightObject.GetPoints(out locationPoints); 
            }
            if (isCreatePolygon)
                CreatePolygon(locationPoints);
        }

        private void CreatePolygon(DNSMcVector3D[] locationPoints)
        {
            if (m_currActiveOverlay != null)
            {
                try
                {
                    if (m_SightObject != null)
                    {
                        m_SightObject.Dispose();
                        m_SightObject.Remove();
                        m_SightObject = null;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Dispose and Remove Object", McEx);
                }

                try
                {

                    m_SightSchemeItemAOS = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    new DNSMcBColor(0, 0, 0, 255),
                                                                                    3f,
                                                                                    null,
                                                                                    new DNSMcFVector2D(1, 1),
                                                                                    1,
                                                                                    DNEFillStyle._EFS_NONE);

                    m_SightObject = DNMcObject.Create(m_currActiveOverlay,
                                                        m_SightSchemeItemAOS,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);

                    m_SightObject.GetScheme().SetName("Tester AOS Draw Polygon Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());
                    Manager_MCViewports.IndexSchemeAOS++;
                    ((IDNMcPolygonItem)m_SightSchemeItemAOS).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);


                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Create Item And Object", McEx);
                }
            }
            m_currDrawObjectParams = new MCTAreaOfSightDrawObjectParams();
            m_currDrawObjectParams.SetPolygonParams(locationPoints);
        }

        private void btnDrawPolygonAreaOfSight_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                RemoveObjectList();
                ResetCalculationOptionsCb();
                ctrl3DVectorAOSScouter.SetVector3D( new DNSMcVector3D());
                this.Hide();
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResults);
                m_ExitStatus = -1;

                string name = "Polygon " + mCounterAreaOfSight++;

                tbNameCalc.Text = name;

                try
                {
                    CreatePolygon();
                    m_EditMode = MCTMapFormManager.MapForm.EditMode;
                    m_EditMode.StartInitObject(m_SightObject, m_SightSchemeItemAOS);

                    while (m_ExitStatus == -1)
                        Application.DoEvents();

                    if (m_ReturnObject != null)
                    {
                        try
                        {
                            ctrlPointsGridSightObject.SetPoints(m_ReturnObject.GetLocationPoints(0));
                        }
                        catch (MapCoreException McEx)
                        {
                            Utilities.ShowErrorMessage("GetLocationPoints", McEx);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Draw Polygon", McEx);
                }

                this.Show();
            }
        }

        private void btnUpdatePolygon_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (sender != null)
                ResetCalculationOptionsCb();

            m_isUpdateSightObject = true;
            CreatePolygon();
            m_isUpdateSightObject = false;
        }

        private DNSMcVector3D[] GetPointsFromGrid(DataGridView gridPoints, bool isChangeCoordPoints = false)
        {
            DNSMcVector3D[] aTargetPoints = new DNSMcVector3D[gridPoints.RowCount - 1];
            double result = 0;
            int currRowNum = 0;

            while (!gridPoints.Rows[currRowNum].IsNewRow)
            {
                if(gridPoints[0, currRowNum].Value != null)
                    aTargetPoints[currRowNum].x = (double.TryParse(gridPoints[0, currRowNum].Value.ToString(), out result) == true) ? result : 0;
                else
                    aTargetPoints[currRowNum].x = 0;

                if (gridPoints[1, currRowNum].Value!= null)
                    aTargetPoints[currRowNum].y = (double.TryParse(gridPoints[1, currRowNum].Value.ToString(), out result) == true) ? result : 0;
                else
                    aTargetPoints[currRowNum].y = 0;

                if (gridPoints[2, currRowNum].Value != null)
                    aTargetPoints[currRowNum].z = (double.TryParse(gridPoints[2, currRowNum].Value.ToString(), out result) == true) ? result : 0;
                else
                    aTargetPoints[currRowNum].z = 0;

                if (isChangeCoordPoints)
                {
                    if (m_currViewport != null)
                        aTargetPoints[currRowNum] = m_currViewport.OverlayManagerToViewportWorld(aTargetPoints[currRowNum]);
                    else
                        aTargetPoints[currRowNum] = aTargetPoints[currRowNum];
                }
                currRowNum++;
            }

            // set the last point to be equal to the first one in order to close the polygon
            //if (!gridPoints.Rows[0].IsNewRow)
            //    aTargetPoints[gridPoints.RowCount - 1] = aTargetPoints[0];

            return aTargetPoints;
        }

        private void CalcPolygonAreaOfSight()
        {
            List<object> listInputs = new List<object>();
            try
            {
                if (ctrl3DVectorAOSScouter.GetVector3D().x == 0 && ctrl3DVectorAOSScouter.GetVector3D().y == 0)
                {
                    MessageBox.Show("Missing Scouter Value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                    btnUpdatePolygon_Click(null, null);
                    GetAreaOfSightUserParams(listInputs);

                    if (cbAreaOfSightAsync.Checked)
                        QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetAreaOfSight);
                    else
                        QueryParams.pAsyncQueryCallback = null;

                DNSMcVector3D[] aTargetPointsViewportCoord = null;

                 if (ctrlPointsGridSightObject.GetPoints(out aTargetPointsViewportCoord, m_currViewport))
                { 
                    m_currSQ.GetPolygonAreaOfSight(m_CurrScouterPoint,
                                                chxAOSIsScouterHeightAbsolute.Checked,
                                                aTargetPointsViewportCoord,
                                                ntxAOSTargetHeight.GetDouble(),
                                                chxAOSIsTargetsHeightAbsolute.Checked,
                                                ntxAOSGPUTargetResolution.GetFloat(),
                                                ntxRotationAzimuthDeg.GetDouble(),
                                                ntxNumberOfRayes.GetUInt32(),
                                                GetSightColorParams(),
                                                chxCalcAreaOfSight.Checked ? m_AreaOfSight : null,
                                                chxCalcLineOfSight.Checked ? m_LineOfSight : null,
                                                chxCalcSeenPolygons.Checked ? m_SeenPolygonOfSight : null,
                                                chxCalcUnseenPolygons.Checked ? m_UnseenPolygonOfSight : null,
                                                chxCalcStaticObjects.Checked ? m_SeenStaticObjects : null,
                                                ntxAOSMaxPitchAngle.GetDouble(),
                                                ntxAOSMinPitchAngle.GetDouble(),
                                                QueryParams,
                                                chxGPU_Based.Checked);
                    ConvertCoordToOM();

                    if (m_AreaOfSight.Value != null)
                        AddAreaOfSight(m_AreaOfSight.Value);

                    if (!cbAreaOfSightAsync.Checked)
                    {
                        List<object> listResults = new List<object>();
                        listResults.Add(m_AreaOfSight.Value);
                        listResults.Add(m_LineOfSight.Value);
                        listResults.Add(m_SeenPolygonOfSight.Value);
                        listResults.Add(m_UnseenPolygonOfSight.Value);
                        listResults.Add(m_SeenStaticObjects.Value);

                        AddResultToList(AsyncQueryFunctionName.GetAreaOfSight, listResults, false, listInputs);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetAreaOfSight, McEx.ErrorCode, cbAreaOfSightAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetPolygonAreaOfSight", McEx);
            }
        }

        private void btnUpdateRectangle_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (sender != null)
                ResetCalculationOptionsCb();
            m_isUpdateSightObject = true;
            CreateRectangle();
            m_isUpdateSightObject = false;
        }

        private bool CreateRectangle()
        {
            bool isCreateRect = !m_isUpdateSightObject;
            float width = 0, height = 0;
            DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
            if (m_isUpdateSightObject == true)
            {
                if (ctrlPointsGridSightObject.GetPoints(out locationPoints))
                {
                    if (locationPoints.Length != 1)
                    {
                        MessageBox.Show("Num points of rectangle object must be one", "Update Rectangle");
                        return false;
                    }
                    else
                    {
                        isCreateRect = true;
                        width = ntxRectWidth.GetFloat();
                        height = ntxRectHeight.GetFloat();
                    }
                }
            }
            if (isCreateRect)
            {
                CreateRectangle(width, height, locationPoints);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateRectangle(float rectWidth, float rectHeight, DNSMcVector3D[] locationPoints)
        {
            if (m_currActiveOverlay != null)
            {
                try
                {
                    if (m_SightObject != null)
                    {
                        m_SightObject.Dispose();
                        m_SightObject.Remove();
                        m_SightObject = null;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Dispose and Remove Object", McEx);
                }

                try
                {
                    m_SightSchemeItemAOS = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                            DNEItemGeometryType._EGT_GEOGRAPHIC,
                                                                                            DNERectangleDefinition._ERD_RECTANGLE_CENTER_DIMENSIONS,
                                                                                            rectWidth / 2,
                                                                                            rectHeight / 2,
                                                                                            DNELineStyle._ELS_SOLID,
                                                                                            new DNSMcBColor(0, 0, 0, 255),
                                                                                            2f, null,
                                                                                            new DNSMcFVector2D(), 0f,
                                                                                            DNEFillStyle._EFS_NONE);

                    m_SightObject = DNMcObject.Create(m_currActiveOverlay,
                                                        m_SightSchemeItemAOS,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);

                    ((IDNMcRectangleItem)m_SightSchemeItemAOS).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    m_SightObject.GetScheme().SetName("Tester AOS Rectangle Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());
                    Manager_MCViewports.IndexSchemeAOS++;

                    ((IDNMcRectangleItem)m_SightSchemeItemAOS).SetRotationYaw(ntxRotationAzimuthDeg.GetFloat(), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Create Item And Object", McEx);
                }
            }
            m_currDrawObjectParams = new MCTAreaOfSightDrawObjectParams();
            m_currDrawObjectParams.SetRectangleParams(locationPoints, rectWidth, rectHeight);
        }

        private void CalcRectangleAreaOfSight()
        {
            List<object> listInputs = new List<object>();
            try
            {
                btnUpdateRectangle_Click(null, null);
                GetAreaOfSightUserParams(listInputs);
                if (cbAreaOfSightAsync.Checked)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetAreaOfSight);
                else
                    QueryParams.pAsyncQueryCallback = null;

                m_currSQ.GetRectangleAreaOfSight(m_CurrScouterPoint,
                                        chxAOSIsScouterHeightAbsolute.Checked,
                                        ntxRectHeight.GetDouble(),
                                        ntxRectWidth.GetDouble(),
                                        ntxRotationAzimuthDeg.GetDouble(),
                                        ntxAOSTargetHeight.GetDouble(),
                                        chxAOSIsTargetsHeightAbsolute.Checked,
                                        ntxAOSGPUTargetResolution.GetFloat(),
                                        ntxNumberOfRayes.GetUInt32(),
                                        GetSightColorParams(),
                                        chxCalcAreaOfSight.Checked ? m_AreaOfSight : null,
                                        chxCalcLineOfSight.Checked ? m_LineOfSight : null,
                                        chxCalcSeenPolygons.Checked ? m_SeenPolygonOfSight : null,
                                        chxCalcUnseenPolygons.Checked ? m_UnseenPolygonOfSight : null,
                                        chxCalcStaticObjects.Checked ? m_SeenStaticObjects : null,
                                        ntxAOSMaxPitchAngle.GetDouble(),
                                        ntxAOSMinPitchAngle.GetDouble(),
                                        QueryParams,
                                        chxGPU_Based.Checked);
                ConvertCoordToOM();

                if (m_AreaOfSight.Value != null)
                    AddAreaOfSight(m_AreaOfSight.Value);

                if (!cbAreaOfSightAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(m_AreaOfSight.Value);
                    listResults.Add(m_LineOfSight.Value);
                    listResults.Add(m_SeenPolygonOfSight.Value);
                    listResults.Add(m_UnseenPolygonOfSight.Value);
                    listResults.Add(m_SeenStaticObjects.Value);

                    AddResultToList(AsyncQueryFunctionName.GetAreaOfSight, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetAreaOfSight, McEx.ErrorCode, cbAreaOfSightAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetRectangleAreaOfSight", McEx);
            }
        }



        private bool CreateEllipse()
        {
            bool isCreateEllipse = !m_isUpdateSightObject;
            float radiusX = 0, radiusY = 0, startAngle = 0, endAngle = 360;
            DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
            if (m_isUpdateSightObject == true) 
            {
                if (ctrlPointsGridSightObject.GetPoints(out locationPoints))
                {
                    if (locationPoints.Length != 1)
                    {
                        MessageBox.Show("Num points of ellipse object must be one", "Update Ellipse");
                        return false;
                    }
                    else
                    {
                        isCreateEllipse = true;
                        radiusX = ntxAOSRadiusX.GetFloat();
                        radiusY = ntxAOSRadiusY.GetFloat();
                        startAngle = ntxAOSStartAngle.GetFloat();
                        endAngle = ntxAOSEndAngle.GetFloat();
                    }
                }
            }

            if (isCreateEllipse)
            {
                CreateEllipse(radiusX,
                           radiusY,
                           startAngle,
                           endAngle,
                           locationPoints);
                return true;
            }
            else
                return false;
        }

        private void CreateEllipse(float radiusX, float radiusY, float startAngle, float endAngle, DNSMcVector3D[] locationPoints)
        {
            if (m_currActiveOverlay != null)
            {
                try
                {
                    if (m_SightObject != null)
                    {
                        m_SightObject.Dispose();
                        m_SightObject.Remove();
                        m_SightObject = null;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Dispose and Remove Object", McEx);
                }

                m_SightSchemeItemAOS = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                      DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                      DNEItemGeometryType._EGT_GEOGRAPHIC,
                                                                                      radiusX,
                                                                                      radiusY,
                                                                                      startAngle,
                                                                                      endAngle,
                                                                                      0,
                                                                                      DNELineStyle._ELS_SOLID,
                                                                                      new DNSMcBColor(0, 0, 0, 255),
                                                                                      3,
                                                                                      null,
                                                                                      new DNSMcFVector2D(1, 1),
                                                                                      1f,
                                                                                      DNEFillStyle._EFS_NONE);


                m_SightObject = DNMcObject.Create(m_currActiveOverlay,
                                                    m_SightSchemeItemAOS,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    locationPoints,
                                                    false);

                ((IDNMcEllipseItem)m_SightSchemeItemAOS).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                m_SightObject.GetScheme().SetName("Tester AOS Draw Ellipse Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());
                Manager_MCViewports.IndexSchemeAOS++;
                ((IDNMcEllipseItem)m_SightSchemeItemAOS).SetRotationYaw(ntxRotationAzimuthDeg.GetFloat(), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
            }
            m_currDrawObjectParams = new MCTAreaOfSightDrawObjectParams();
            m_currDrawObjectParams.SetEllipseParams(locationPoints, radiusX, radiusY, startAngle, endAngle);
        }

        private void btnUpdateSightObject_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (sender != null)
                ResetCalculationOptionsCb();
            m_isUpdateSightObject = true;

            if (rbSightObjectPolygon.Checked)
            {
                CreatePolygon();
            }
            else if (rgbSightObjectEllipse.Checked)
            {
                if(CreateEllipse())
                    SetAOSScouterPoint();
            }
            else
            {
                if(CreateRectangle())
                    SetAOSScouterPoint();
            }

            

            m_isUpdateSightObject = false;
        }

        private void btnDrawSightObject_Click(object sender, EventArgs e)
        {
            if (m_currActiveOverlay != null)
            {
                CheckIsChangeCurrentViewport();
                ResetCalculationOptionsCb();

                RemoveObjectList();
                this.Hide();
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResults);
                m_ExitStatus = -1;

                string name = "";

                try
                {
                    //ResetParamsAreaOfSight(); 
                    if (rbSightObjectPolygon.Checked)
                    {
                        name = "Polygon " + mCounterAreaOfSight++;
                        CreatePolygon();
                    }
                    else if (rgbSightObjectEllipse.Checked)
                    {
                        name = "Ellipse " + mCounterAreaOfSight++;
                        CreateEllipse();
                    }
                    else
                    {
                        name = "Rectangle " + mCounterAreaOfSight++;
                        CreateRectangle();
                    }

                    tbNameCalc.Text = name;

                    m_EditMode = MCTMapFormManager.MapForm.EditMode;
                    m_EditMode.StartInitObject(m_SightObject, m_SightSchemeItemAOS);

                    while (m_ExitStatus == -1)
                        Application.DoEvents();

                    if (m_SightObject != null)
                    {
                        SetAOSScouterPoint();

                        if (m_ReturnItem != null)
                        {
                            if (m_ReturnItem is IDNMcEllipseItem)
                            {
                                
                                try
                                {
                                    float fParam;
                                    uint propID;

                                    ((IDNMcEllipseItem)m_ReturnItem).GetRadiusX(out fParam, out propID, false);
                                    ntxAOSRadiusX.SetFloat(fParam);
                                    ((IDNMcEllipseItem)m_ReturnItem).GetRadiusY(out fParam, out propID, false);
                                    ntxAOSRadiusY.SetFloat(fParam);

                                    ((IDNMcEllipseItem)m_ReturnItem).GetStartAngle(out fParam, out propID, false);
                                    ntxAOSStartAngle.SetFloat(fParam);
                                    ((IDNMcEllipseItem)m_ReturnItem).GetEndAngle(out fParam, out propID, false);
                                    ntxAOSEndAngle.SetFloat(fParam);

                                    ((IDNMcEllipseItem)m_ReturnItem).GetRotationYaw(out fParam, out propID, false);
                                    ntxRotationAzimuthDeg.SetFloat(fParam);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("Get ellipse params", McEx);
                                }
                            }
                            else if (m_ReturnItem is IDNMcRectangleItem)
                            {

                                try
                                {
                                    IDNMcRectangleItem rect = (IDNMcRectangleItem)m_ReturnItem;

                                    float fParam = 0;
                                    uint propID = 0;

                                    rect.GetRadiusY(out fParam, out propID, false);
                                    ntxRectHeight.SetFloat(fParam * 2);

                                    rect.GetRadiusX(out fParam, out propID, false);
                                    ntxRectWidth.SetFloat(fParam * 2);

                                }
                                catch (MapCoreException McEx)
                                {
                                    Utilities.ShowErrorMessage("Get rectangle params", McEx);
                                }
                            }

                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Draw Sight Object", McEx);
                }

                this.Show();
            }
        }

        private void SetAOSScouterPoint()
        {
            if (m_SightObject != null)
            {
                try
                {
                    DNSMcVector3D[] objectLocation = m_SightObject.GetLocationPoints(0);
                    ctrlPointsGridSightObject.SetPoints(objectLocation);

                    if (objectLocation != null && objectLocation.Length > 0)
                    {
                        if (!chxAOSIsScouterHeightAbsolute.Checked)
                            objectLocation[0].z = 1.7;
                        ctrl3DVectorAOSScouter.SetVector3D(objectLocation[0]);

                        ctrl3DScouterCenterPoint.SetVector3D(objectLocation[0]);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetLocationPoints", McEx);
                }
            }
        }

        private bool GetAreaOfSightUserParams(List<object> listInputs, uint numScouters = 0)
        {
            listInputs.Add(GetAreaOfSightName());                // 0
            listInputs.Add(GetAreaOfSightStaticObjectsColor());
            listInputs.Add(m_currDrawObjectParams);

            listInputs.Add(ctrl3DVectorAOSScouter.GetVector3D());
            listInputs.Add(chxAOSIsScouterHeightAbsolute.Checked);
            listInputs.Add(ntxAOSTargetHeight.GetDouble());      // 5
            listInputs.Add(chxAOSIsTargetsHeightAbsolute.Checked);
            listInputs.Add(ntxAOSGPUTargetResolution.GetFloat());
            listInputs.Add(ntxRotationAzimuthDeg.GetFloat());
            listInputs.Add(ntxNumberOfRayes.GetUInt32());

            SaveAOSColor();
 /*           DNSMcBColor[] colors = m_colors.ToArray();

            colors[(int)DNEPointVisibility._EPV_SEEN] = m_colors[(int)DNEPointVisibility._EPV_SEEN];//  new DNSMcBColor(picbSeenColor.BackColor.R, picbSeenColor.BackColor.G, picbSeenColor.BackColor.B, (byte)numUDSeenAlphaColor.Value);
            colors[(int)DNEPointVisibility._EPV_UNSEEN] = m_colors[(int)DNEPointVisibility._EPV_UNSEEN]; // new DNSMcBColor(picbUnSeenColor.BackColor.R, picbUnSeenColor.BackColor.G, picbUnSeenColor.BackColor.B, (byte)numUDUnseenAlphaColor.Value);
            colors[(int)DNEPointVisibility._EPV_UNKNOWN] = m_colors[(int)DNEPointVisibility._EPV_UNKNOWN]; // new DNSMcBColor(picbUnknownColor.BackColor.R, picbUnknownColor.BackColor.G, picbUnknownColor.BackColor.B, (byte)numUDUnknownAlphaColor.Value);
            colors[(int)DNEPointVisibility._EPV_OUT_OF_QUERY_AREA] = m_colors[(int)DNEPointVisibility._EPV_OUT_OF_QUERY_AREA];// new DNSMcBColor(picbOutOfQueryAreaColor.BackColor.R, picbOutOfQueryAreaColor.BackColor.G, picbOutOfQueryAreaColor.BackColor.B, (byte)numUDOutOfQueryAreaAlphaColor.Value);
            colors[(int)DNEPointVisibility._EPV_SEEN_STATIC_OBJECT] = m_colors[(int)DNEPointVisibility._EPV_SEEN_STATIC_OBJECT]; // new DNSMcBColor(picbSeenStaticObjectsColor.BackColor.R, picbSeenStaticObjectsColor.BackColor.G, picbSeenStaticObjectsColor.BackColor.B, (byte)numUDSeenStaticObjectsAlphaColor.Value);
*/

            listInputs.Add(m_colors.ToArray());                             // 10                             
            listInputs.Add(ntxAOSMaxPitchAngle.GetDouble());
            listInputs.Add(ntxAOSMinPitchAngle.GetDouble());
            listInputs.Add(chxGPU_Based.Checked);

            listInputs.Add(chxCalcAreaOfSight.Checked);
            listInputs.Add(chxCalcLineOfSight.Checked);         // 15
            listInputs.Add(chxCalcSeenPolygons.Checked);
            listInputs.Add(chxCalcUnseenPolygons.Checked);
            listInputs.Add(chxCalcStaticObjects.Checked);
            listInputs.Add(numScouters);
            listInputs.Add(cbIsMultipleScouters.Checked);       // 20
            listInputs.Add(cmbEScoutersSumType.SelectedItem.ToString());
            listInputs.Add(ntxMaxNumOfScouters.GetUInt32());
            listInputs.Add(ctrl3DScouterCenterPoint.GetVector3D());
            listInputs.Add(ntxScouterRadiusX.GetFloat());
            listInputs.Add(ntxScouterRadiusY.GetFloat());       // 25
            listInputs.Add(cbCalcBestPointsAsync.Checked);
            DNSMcVector3D[] locationPoints;

            if (ctrlPointsGridMultipleScouters.GetPoints(out locationPoints))
            {
                listInputs.Add(locationPoints);
                return true;
            }
            else
                return false;

        }

        uint numberOfSetBits(uint i)
        {
            i = i - ((i >> 1) & 0x55555555);        // add pairs of bits
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);  // quads
            i = (i + (i >> 4)) & 0x0F0F0F0F;        // groups of 8
            return (i * 0x01010101) >> 24;          // horizontal sum of bytes
        }

        private void SetAreaOfSightUserParams(bool isAsync, List<object> listInputs)
        {
            m_IsSetAreaOfSightUserParams = true;
            ctrl3DVectorAOSScouter.SetVector3D( (DNSMcVector3D)listInputs[3]);
            chxAOSIsScouterHeightAbsolute.Checked = (bool)listInputs[4];
            ntxAOSTargetHeight.SetDouble((double)listInputs[5]);
            chxAOSIsTargetsHeightAbsolute.Checked = (bool)listInputs[6];
            ntxAOSGPUTargetResolution.SetFloat((float)listInputs[7]);
            ntxRotationAzimuthDeg.SetFloat((float)listInputs[8]);
            ntxNumberOfRayes.SetUInt32((uint)listInputs[9]);
            DNSMcBColor[] sightColors = (DNSMcBColor[])listInputs[10];

            m_currPointVisibility = DNEPointVisibility._EPV_NUM;
            SetAreaOfSightColors(sightColors[0],
                sightColors[1],
                sightColors[2],
                sightColors[3],
                sightColors[4]);

            ntxAOSMaxPitchAngle.SetDouble((double)listInputs[11]);
            ntxAOSMinPitchAngle.SetDouble((double)listInputs[12]);
            chxGPU_Based.Checked = (bool)listInputs[13];
            cbAreaOfSightAsync.Checked = isAsync;

            chxCalcAreaOfSight.Checked = (bool)listInputs[14];
            chxCalcLineOfSight.Checked = (bool)listInputs[15];
            chxCalcSeenPolygons.Checked = (bool)listInputs[16];
            chxCalcUnseenPolygons.Checked = (bool)listInputs[17];
            chxCalcStaticObjects.Checked = (bool)listInputs[18];
            cbIsMultipleScouters.Checked = (bool)listInputs[20];

            m_IsSetAreaOfSightUserParams = false;
        }

        private void ResetCalculationOptionsCb()
        {
            chxCalcAreaOfSight.Checked = true;
            chxCalcLineOfSight.Checked = true;
            chxCalcSeenPolygons.Checked = true;
            chxCalcUnseenPolygons.Checked = true;
            chxCalcStaticObjects.Checked = true;
        }

        DNEScoutersSumType m_AreaOfSightCalcMultipeScoutersSumType;

        private void CalcEllipseAreaOfSight()
        {
            List<object> listInputs = new List<object>();
            AsyncQueryFunctionName asyncQueryFunctionName = AsyncQueryFunctionName.GetAreaOfSight;
            if (cbIsMultipleScouters.Checked)
                asyncQueryFunctionName = AsyncQueryFunctionName.GetAreaOfSightForMultipleScouters;

            try
            {
                btnUpdateSightObject_Click(null, null);
               

                if (cbIsMultipleScouters.Checked)
                {
                    uint numScouters = 0;
                    DNSMcVector3D[] aTargetPoints;
                    if (ctrlPointsGridMultipleScouters.GetPoints(out aTargetPoints))
                    {
                        if (aTargetPoints != null)
                        {
                            numScouters = (uint)aTargetPoints.Length;
                        }
                        if (GetAreaOfSightUserParams(listInputs, numScouters))
                        {
                            if (cbAreaOfSightAsync.Checked)
                                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, asyncQueryFunctionName);
                            else
                                QueryParams.pAsyncQueryCallback = null;
                            m_AreaOfSightCalcMultipeScoutersSumType = (DNEScoutersSumType)Enum.Parse(typeof(DNEScoutersSumType), cmbEScoutersSumType.SelectedItem.ToString());
                            DNSMcVector3D scouterPoint;

                            if (m_currViewport != null)
                                scouterPoint = m_currViewport.OverlayManagerToViewportWorld(ctrl3DScouterCenterPoint.GetVector3D());
                            else
                                scouterPoint = ctrl3DScouterCenterPoint.GetVector3D();

                            m_currSQ.GetEllipseAreaOfSightForMultipleScouters(aTargetPoints,
                                                chxAOSIsScouterHeightAbsolute.Checked,
                                                ntxAOSTargetHeight.GetDouble(),
                                                chxAOSIsTargetsHeightAbsolute.Checked,
                                                ntxAOSGPUTargetResolution.GetFloat(),
                                                scouterPoint,
                                                ntxAOSRadiusX.GetFloat(),
                                                ntxAOSRadiusY.GetFloat(),
                                                ntxNumberOfRayes.GetUInt32(),
                                                m_AreaOfSightCalcMultipeScoutersSumType,
                                                out m_AreaOfSightForMultipleScouter,
                                                ntxAOSMaxPitchAngle.GetDouble(),
                                                ntxAOSMinPitchAngle.GetDouble(),
                                                QueryParams,
                                                chxGPU_Based.Checked);

                            if (m_AreaOfSightForMultipleScouter != null)
                                AddAreaOfSight(m_AreaOfSightForMultipleScouter);
                        }
                    }
                }
                else
                {
                    GetAreaOfSightUserParams(listInputs);

                    int numCalling = ntxNumCallingFunction.GetInt32();
                    for (int i = 0; i < numCalling; i++)
                    {
                        if (cbAreaOfSightAsync.Checked)
                            QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, asyncQueryFunctionName);
                        else
                            QueryParams.pAsyncQueryCallback = null;

                        m_currSQ.GetEllipseAreaOfSight(m_CurrScouterPoint,
                                           chxAOSIsScouterHeightAbsolute.Checked,
                                           ntxAOSTargetHeight.GetDouble(),
                                           chxAOSIsTargetsHeightAbsolute.Checked,
                                           ntxAOSGPUTargetResolution.GetFloat(),
                                           ntxAOSRadiusX.GetFloat(),
                                           ntxAOSRadiusY.GetFloat(),
                                           ntxAOSStartAngle.GetFloat(),
                                           ntxAOSEndAngle.GetFloat(),
                                           ntxRotationAzimuthDeg.GetFloat(),
                                           ntxNumberOfRayes.GetUInt32(),
                                           GetSightColorParams(),
                                           chxCalcAreaOfSight.Checked ? m_AreaOfSight : null,
                                           chxCalcLineOfSight.Checked ? m_LineOfSight : null,
                                           chxCalcSeenPolygons.Checked ? m_SeenPolygonOfSight : null,
                                           chxCalcUnseenPolygons.Checked ? m_UnseenPolygonOfSight : null,
                                           chxCalcStaticObjects.Checked ? m_SeenStaticObjects : null,
                                           ntxAOSMaxPitchAngle.GetDouble(),
                                           ntxAOSMinPitchAngle.GetDouble(),
                                           QueryParams,
                                           chxGPU_Based.Checked);

                        if (m_AreaOfSight.Value != null)
                            AddAreaOfSight(m_AreaOfSight.Value);
                    }
                }
                ConvertCoordToOM();

              

                if (!cbAreaOfSightAsync.Checked)
                {
                    List<object> listResults = new List<object>();

                    if (cbIsMultipleScouters.Checked)
                    {
                        listResults.Add(m_AreaOfSightForMultipleScouter);
                        AddResultToList(asyncQueryFunctionName, listResults, false, listInputs);
                    }
                    else
                    {
                        listResults.Add(m_AreaOfSight.Value);
                        listResults.Add(m_LineOfSight.Value);
                        listResults.Add(m_SeenPolygonOfSight.Value);
                        listResults.Add(m_UnseenPolygonOfSight.Value);
                        listResults.Add(m_SeenStaticObjects.Value);
                        AddResultToList(asyncQueryFunctionName, listResults, false, listInputs);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                string sFunctionName = "GetEllipseAreaOfSight";
                if (cbIsMultipleScouters.Checked)
                {
                    sFunctionName = "GetEllipseAreaOfSightForMultipleScouters";
                }
                AddErrorToList(asyncQueryFunctionName, McEx.ErrorCode, cbAreaOfSightAsync.Checked, listInputs);
                Utilities.ShowErrorMessage(sFunctionName, McEx);
            }
        }

        private void ConvertCoordToOM()
        {
            if (m_LineOfSight != null && m_LineOfSight.Value != null)
            {
                for (int i = 0; i < m_LineOfSight.Value.Length; i++)
                {
                    DNSLineOfSightPoint[] sLineOfSightPoints = m_LineOfSight.Value[i];
                    for (int j = 0; j < sLineOfSightPoints.Length; j++)
                    {
                        if (m_currViewport != null)
                            m_LineOfSight.Value[i][j].Point = m_currViewport.ViewportToOverlayManagerWorld(m_LineOfSight.Value[i][j].Point);
                        else
                            m_LineOfSight.Value[i][j].Point = m_LineOfSight.Value[i][j].Point;
                    }
                }
            }
            if (m_SeenPolygonOfSight != null)
            {
                ConvertCoordDNSPolygonsOfSightToOM(ref m_SeenPolygonOfSight);
            }

            if (m_UnseenPolygonOfSight != null)
            {
                ConvertCoordDNSPolygonsOfSightToOM(ref m_UnseenPolygonOfSight);
            }
        }

        private void ConvertCoordDNSPolygonsOfSightToOM(ref DNMcNullableOut<DNSPolygonsOfSight> polygonsOfSight)
        {
            if (polygonsOfSight.Value.aaContoursPoints != null)
            {
                for (int i = 0; i < polygonsOfSight.Value.aaContoursPoints.Length; i++)
                {
                    DNSMcVector3D[] points = polygonsOfSight.Value.aaContoursPoints[i];
                    for (int j = 0; j < points.Length; j++)
                    {
                        if (m_currViewport != null)
                            polygonsOfSight.Value.aaContoursPoints[i][j] = m_currViewport.ViewportToOverlayManagerWorld(polygonsOfSight.Value.aaContoursPoints[i][j]);
                        else
                            polygonsOfSight.Value.aaContoursPoints[i][j] = polygonsOfSight.Value.aaContoursPoints[i][j];
                    }
                }
            }
        }

        private void SetAreaOfSightDrawObjectParams()
        {
            if (m_currDrawObjectParams != null)
            {
                ctrlPointsGridSightObject.SetPoints(m_currDrawObjectParams.LocationPoints);
                
                switch (m_currDrawObjectParams.ObjectType)
                {
                    case MCTAreaOfSightDrawObjectParams.MCTAreaOfSightDrawObjectType.Ellipse:
                        rgbSightObjectEllipse.Checked = true;
                        ntxAOSStartAngle.SetFloat(m_currDrawObjectParams.StartAngle);
                        ntxAOSEndAngle.SetFloat(m_currDrawObjectParams.EndAngle);
                        ntxAOSRadiusX.SetFloat(m_currDrawObjectParams.RadiusX);
                        ntxAOSRadiusY.SetFloat(m_currDrawObjectParams.RadiusY);
                        break;
                    case MCTAreaOfSightDrawObjectParams.MCTAreaOfSightDrawObjectType.Rectangle:
                        rgbSightObjectRect.Checked = true;
                        ntxRectWidth.SetFloat(m_currDrawObjectParams.RadiusX);
                        ntxRectHeight.SetFloat(m_currDrawObjectParams.RadiusY);
                        break;
                    case MCTAreaOfSightDrawObjectParams.MCTAreaOfSightDrawObjectType.Polygon:
                        rbSightObjectPolygon.Checked = true;
                        break;
                }

                btnUpdateSightObject_Click(null, null);

            }
        }

        private void OnAreaOfSightResults(List<object> listResults, List<object> listInputs, int rowTableNumber)
        {
            DNSPolygonsOfSight seenPolygons = new DNSPolygonsOfSight();
            DNSPolygonsOfSight unSeenPolygons = new DNSPolygonsOfSight();
            if (listResults[2] != null)
                seenPolygons = (DNSPolygonsOfSight)listResults[2];
            if (listResults[3] != null)
                unSeenPolygons = (DNSPolygonsOfSight)listResults[3];

            m_currDrawObjectParams = (MCTAreaOfSightDrawObjectParams)listInputs[2];
            if (m_currDrawObjectParams != null)
            {
                SetAreaOfSightDrawObjectParams();

                DrawResultsAreaOfSightNew(m_SightObject,
                    (DNSMcBColor)listInputs[1],
                    (IDNAreaOfSight)listResults[0],
                    (DNSLineOfSightPoint[][])listResults[1],
                    seenPolygons,
                    unSeenPolygons,
                   (DNSStaticObjectsIDs[])listResults[4],
                   rowTableNumber,
                   (string)listInputs[0]);
            }
        }

        private void OnAreaOfSightForMultipleScoutersResults(List<object> listResults, List<object> listInputs,
            int rowTableNumber)
        {
            m_currDrawObjectParams = (MCTAreaOfSightDrawObjectParams)listInputs[2];

            CreateEllipse(m_currDrawObjectParams.RadiusX,
                            m_currDrawObjectParams.RadiusY,
                            m_currDrawObjectParams.StartAngle,
                            m_currDrawObjectParams.EndAngle,
                            m_currDrawObjectParams.LocationPoints);
           // drawObject = m_SightObject;

            DrawResultsAreaOfSightForMultipleScoutersNew(m_SightObject,
                (IDNAreaOfSight)listResults[0],
                (uint)listInputs[19],
                rowTableNumber,
                (string)listInputs[21],
               (string)listInputs[0]);
        }

        private void RemoveAreaOfSight()
        {
            m_currAreaOfSightSelectUserShowResults = null;

            bIsDrawNewResult = true;
            chxDrawLineOfSight.Checked = false;
            chxDrawAreaOfSight.Checked = false;
            chxDrawSeenPolygons.Checked = false;
            chxDrawUnseenPolygons.Checked = false;
            chxDrawSeenStaticObjects.Checked = false;
            bIsDrawNewResult = false;

            RemoveObject(m_SightObject);

            m_SightObject = null;

            if (m_currCalcParams != null)
            {
                DrawStaticObject(m_currCalcParams, false);
                m_currCalcParams.RemoveObjects();
            }

            m_currCalcParams = null;

            chxDrawObject.Enabled = chxDrawObject.Checked = false;
            chxDrawLineOfSight.Enabled = false;
            chxDrawAreaOfSight.Enabled = false;
            chxDrawSeenPolygons.Enabled = false;
            chxDrawUnseenPolygons.Enabled = false;
            btnCreateMatrix.Enabled = false;
            btnSaveMatrixToFile.Enabled = false;
        }

        private DNSMcBColor[] GetSightColorParams()
        {
            return m_colors.ToArray();

            /* DNSMcBColor[] sightColors = m_colors.ToArray();  
            return sightColors;*/
        }

        private void CreateShemesAreaOfSight()
        {
            if (m_currActiveOverlay != null)
            {

                try
                {
                    DNSMcBColor seenColor = m_colors[(int)DNEPointVisibility._EPV_SEEN];
                    DNSMcBColor unseenColor = m_colors[(int)DNEPointVisibility._EPV_UNSEEN];

                    // Seen Polygons
                    m_seenPolygonItemAreaOfSight = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                                DNELineStyle._ELS_SOLID,
                                                                                                seenColor,
                                                                                                3f,
                                                                                                null,
                                                                                                new DNSMcFVector2D(0, -1),
                                                                                                1f,
                                                                                                DNEFillStyle._EFS_SOLID,
                                                                                                seenColor);

                    m_seenPolygonSchemeAreaOfSight = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                m_seenPolygonItemAreaOfSight,
                                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                false);

                    ((IDNMcSymbolicItem)m_seenPolygonItemAreaOfSight).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);
                    m_seenPolygonSchemeAreaOfSight.SetName("Tester AOS Seen Polygon Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());

                    // Unseen Polygons
                    m_unseenPolygonItemAreaOfSight = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                       DNELineStyle._ELS_SOLID,
                                                                                       unseenColor,
                                                                                       3f,
                                                                                       null,
                                                                                       new DNSMcFVector2D(0, -1),
                                                                                       1f,
                                                                                       DNEFillStyle._EFS_SOLID,
                                                                                       unseenColor);

                    m_unseenPolygonSchemeAreaOfSight = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                           m_unseenPolygonItemAreaOfSight,
                                                                           DNEMcPointCoordSystem._EPCS_WORLD,
                                                                           false);
                    ((IDNMcSymbolicItem)m_unseenPolygonItemAreaOfSight).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);

                    m_unseenPolygonSchemeAreaOfSight.SetName("Tester AOS Unseen Polygon Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());

                    // Line Of Sight
                    m_visibleLineSchemeItemAreaOfSight = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                       DNELineStyle._ELS_SOLID,
                                                                                       seenColor,
                                                                                       3);

                    m_nonVisibleLineSchemeItemAreaOfSight = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    unseenColor,
                                                                                    3);

                    m_visibleLineSchemeAreaOfSight = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                m_visibleLineSchemeItemAreaOfSight,
                                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                false);
                    m_visibleLineSchemeAreaOfSight.SetName("Tester AOS Visible Line Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());

                    m_nonVisibleLineSchemeAreaOfSight = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                m_nonVisibleLineSchemeItemAreaOfSight,
                                                                                DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                false);
                    m_nonVisibleLineSchemeAreaOfSight.SetName("Tester AOS Non Visible Line Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());

                    ((IDNMcLineItem)m_visibleLineSchemeItemAreaOfSight).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);
                    ((IDNMcLineItem)m_nonVisibleLineSchemeItemAreaOfSight).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);

                    Manager_MCViewports.IndexSchemeAOS++;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcPolygonItem.Create Or DNMcLineItem.Create Or DNMcObjectScheme.Create ", McEx);
                }
            }
        }

        private string GetAreaOfSightName()
        {
            string name = tbNameCalc.Text;
            if (chxGPU_Based.Checked)
                name = name + " (GPU)";
            else
                name = name + " (CPU)";
            return name;
        }

        private DNSMcBColor GetAreaOfSightStaticObjectsColor()
        {
            return m_colors[(int)DNEPointVisibility._EPV_SEEN_STATIC_OBJECT];
        }

        private void DrawResultsAreaOfSightNew(IDNMcObject drawObject,
            DNSMcBColor staticObjectsColor,
            IDNAreaOfSight areaOfSight,
            DNSLineOfSightPoint[][] lineOfSight,
            DNSPolygonsOfSight seenPolygonOfSight,
            DNSPolygonsOfSight unseenPolygonOfSight,
            DNSStaticObjectsIDs[] seenStaticObjects,
            int rowTableNumber,
            string calcName)
        {
            MCTObjectsAreaOfSight saveCalcParams = new MCTObjectsAreaOfSight(
                drawObject,
                chxCalcAreaOfSight.Checked,
                chxCalcLineOfSight.Checked,
                chxCalcSeenPolygons.Checked,
                chxCalcUnseenPolygons.Checked,
                chxCalcStaticObjects.Checked,
                staticObjectsColor);

            saveCalcParams.CalcName = calcName;

            List<StaticObjectsColor> lst = new List<StaticObjectsColor>();

            if (seenStaticObjects != null && seenStaticObjects.Length > 0)
            {
                foreach (DNSStaticObjectsIDs info in seenStaticObjects)
                {
                    StaticObjectsColor obj = new StaticObjectsColor();
                    obj.layer = info.pMapLayer as IDNMcVector3DExtrusionMapLayer;
                    obj.lstVariantId = info.auIDs.ToList();
                    DNSMcBColor color = new DNSMcBColor();

                    if (info.auIDs.Length > 0)
                        color = obj.layer.GetObjectColor(info.auIDs[0]);
                    obj.color = color;
                    obj.aaStaticObjectsContours = info.aaStaticObjectsContours;

                    lst.Add(obj);

                    obj.layer.SetObjectsColor(staticObjectsColor, info.auIDs);
                }
                saveCalcParams.LstStaticObjectsColor = lst;
            }

            try
            {
                if (m_currActiveOverlay != null)
                {
                    CreateShemesAreaOfSight();
                    if (seenPolygonOfSight.aaContoursPoints != null)
                    {
                        foreach (DNSMcVector3D[] polygon in seenPolygonOfSight.aaContoursPoints)
                        {
                            saveCalcParams.ListSeenPolygon.Add(DNMcObject.Create(m_currActiveOverlay, m_seenPolygonSchemeAreaOfSight, polygon));
                        }
                    }

                    if (unseenPolygonOfSight.aaContoursPoints != null)
                    {
                        foreach (DNSMcVector3D[] polygon in unseenPolygonOfSight.aaContoursPoints)
                        {
                            saveCalcParams.ListUnseenPolygon.Add(DNMcObject.Create(m_currActiveOverlay, m_unseenPolygonSchemeAreaOfSight, polygon));
                        }
                    }

                    if (lineOfSight != null)
                    {
                        DNSMcVector3D[] locationPt = new DNSMcVector3D[2];
                        foreach (DNSLineOfSightPoint[] linesPt in lineOfSight)
                        {
                            if (linesPt != null)
                            {
                                for (int i = 1; i < linesPt.Length; i++)
                                {
                                    locationPt[0] = linesPt[i - 1].Point;
                                    locationPt[1] = linesPt[i].Point;

                                    if (linesPt[i - 1].bVisible == true)
                                    {
                                        saveCalcParams.ListLineOfSight.Add(DNMcObject.Create(m_currActiveOverlay, m_visibleLineSchemeAreaOfSight, locationPt));
                                    }
                                    else
                                    {
                                        saveCalcParams.ListLineOfSight.Add(DNMcObject.Create(m_currActiveOverlay, m_nonVisibleLineSchemeAreaOfSight, locationPt));
                                    }
                                }
                            }
                        }
                    }
                }

                saveCalcParams.AreaOfSight = areaOfSight;

                m_currCalcParams = saveCalcParams;

                if (areaOfSight != null && chxIsCreateAndDrawMatrixAutomatic.Checked)
                {
                    btnCreateMatrixAreaOfSight_Click(null, null);
                }

                ShowResultsAreaOfSight(rowTableNumber);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcObject.Create Or DNMcObjectScheme.Create ", McEx);
            }
        }
        // m_AreaOfSightCalcMultipeScoutersSumType

        private void DrawResultsAreaOfSightForMultipleScoutersNew(IDNMcObject drawObject,
            IDNAreaOfSight areaOfSight,
            uint numOfScouters,
            int rowTableNumber,
            string strScoutersSumType,
        string calcName)
        {
            MCTObjectsAreaOfSight saveCalcParams = new MCTObjectsAreaOfSight();
            saveCalcParams.DrawObject = drawObject;
            saveCalcParams.IsShowAreaOfSight = chxCalcAreaOfSight.Checked;
            saveCalcParams.IsMultipleScouter = true;
            saveCalcParams.NumOfScouters = numOfScouters;
            saveCalcParams.CalcName = calcName;

            List<DNSObjectColor> oldColors = new List<DNSObjectColor>();
 
            List<StaticObjectsColor> lst = new List<StaticObjectsColor>();

            try
            {
                saveCalcParams.AreaOfSight = areaOfSight;

                m_currCalcParams = saveCalcParams;

                m_currCalcParams.IsShowAreaOfSight = (m_currCalcParams.AreaOfSight != null);

                if (areaOfSight != null && chxIsCreateAndDrawMatrixAutomatic.Checked)
                {
                    m_AreaOfSightCalcMultipeScoutersSumType = (DNEScoutersSumType)Enum.Parse(typeof(DNEScoutersSumType), strScoutersSumType);
                    cmbEScoutersSumType.SelectedItem = m_AreaOfSightCalcMultipeScoutersSumType.ToString();
                    btnCreateMatrixAreaOfSight_Click(null, null);
                }

                ShowResultsAreaOfSight(rowTableNumber);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcObject.Create Or DNMcObjectScheme.Create ", McEx);
            }

        }

        MCTAreaOfSightSelectUserShowResults m_currAreaOfSightSelectUserShowResults;

        private void ShowResultsAreaOfSight(int rowTableNumber)
        {
            chxDrawObject.Enabled = (m_currCalcParams.DrawObject != null);
            chxDrawLineOfSight.Enabled = (m_currCalcParams.ListLineOfSight.Count > 0);
            btnSaveMatrixToFile.Enabled = btnCreateMatrix.Enabled = btnAOSSave.Enabled = btnTestSaveAndLoad.Enabled = chxDrawAreaOfSight.Enabled = (m_currCalcParams.AreaOfSight != null);
            chxDrawSeenPolygons.Enabled = (m_currCalcParams.ListSeenPolygon.Count > 0);
            chxDrawUnseenPolygons.Enabled = (m_currCalcParams.ListUnseenPolygon.Count > 0);
            chxDrawSeenStaticObjects.Enabled = (m_currCalcParams.LstStaticObjectsColor.Count > 0);

            if (m_dicAreaOfSightSelectUserShowResults.Keys.Contains(rowTableNumber))
            {
                m_currAreaOfSightSelectUserShowResults = m_dicAreaOfSightSelectUserShowResults[rowTableNumber];
            }
            else
            {
                m_currAreaOfSightSelectUserShowResults = new MCTAreaOfSightSelectUserShowResults();
                m_currAreaOfSightSelectUserShowResults.IsShowDrawObject = (m_currCalcParams.DrawObject != null);
                m_currAreaOfSightSelectUserShowResults.IsShowLineOfSight = (m_currCalcParams.ListLineOfSight.Count > 0);
                m_currAreaOfSightSelectUserShowResults.IsShowAreaOfSight = (m_currCalcParams.AreaOfSight != null);
                m_currAreaOfSightSelectUserShowResults.IsShowSeenPolygon = (m_currCalcParams.ListSeenPolygon.Count > 0);
                m_currAreaOfSightSelectUserShowResults.IsShowUnseenPolygon = (m_currCalcParams.ListUnseenPolygon.Count > 0);
                m_currAreaOfSightSelectUserShowResults.IsShowSeenStaticObjects = (m_currCalcParams.LstStaticObjectsColor.Count > 0);
                m_dicAreaOfSightSelectUserShowResults.Add(rowTableNumber, m_currAreaOfSightSelectUserShowResults);
            }

            chxDrawObject.Checked = ((m_currAreaOfSightSelectUserShowResults.IsShowDrawObject));
            chxDrawLineOfSight.Checked = ((m_currAreaOfSightSelectUserShowResults.IsShowLineOfSight));
            chxDrawAreaOfSight.Checked = ((m_currAreaOfSightSelectUserShowResults.IsShowAreaOfSight));
            chxDrawSeenPolygons.Checked = ((m_currAreaOfSightSelectUserShowResults.IsShowSeenPolygon));
            chxDrawUnseenPolygons.Checked = ((m_currAreaOfSightSelectUserShowResults.IsShowUnseenPolygon));
            chxDrawSeenStaticObjects.Checked = ((m_currAreaOfSightSelectUserShowResults.IsShowSeenStaticObjects));

            if (!chxDrawObject.Checked)
                chxDrawObject_CheckedChanged(null, null);
            if (!chxDrawLineOfSight.Checked)
                chxDrawLineOfSight_CheckedChanged(null, null);
            if (!chxDrawAreaOfSight.Checked)
                chxDrawAreaOfSight_CheckedChanged(null, null);
            if (!chxDrawSeenPolygons.Checked)
                chxDrawSeenPolygons_CheckedChanged(null, null);
            if (!chxDrawUnseenPolygons.Checked)
                chxDrawUnseenPolygons_CheckedChanged(null, null);
            if (!chxDrawSeenStaticObjects.Checked)
                chxSeenStaticObjects_CheckedChanged(null, null);
        }

        private void chxDrawObject_CheckedChanged(object sender, EventArgs e)
        {
            if (m_currCalcParams != null && bIsDrawNewResult == false)
            {
                if (m_currAreaOfSightSelectUserShowResults != null)
                    m_currAreaOfSightSelectUserShowResults.IsShowDrawObject = chxDrawObject.Checked;
                DNEActionOptions visibility = GetVisibility(chxDrawObject.Checked);
                if (m_currCalcParams.DrawObject != null)
                    m_currCalcParams.DrawObject.SetVisibilityOption(visibility);
            }
        }

        private void chxDrawLineOfSight_CheckedChanged(object sender, EventArgs e)
        {
            if (m_currCalcParams != null && bIsDrawNewResult == false && m_currCalcParams.ListLineOfSight != null)
            {
                if (m_currAreaOfSightSelectUserShowResults != null)
                    m_currAreaOfSightSelectUserShowResults.IsShowLineOfSight = chxDrawLineOfSight.Checked;
                DNEActionOptions visibility = GetVisibility(chxDrawLineOfSight.Checked);

                foreach (IDNMcObject obj in m_currCalcParams.ListLineOfSight)
                {
                    obj.SetVisibilityOption(visibility);
                }
            }
        }

        private void chxDrawAreaOfSight_CheckedChanged(object sender, EventArgs e)
        {
            if (m_currCalcParams != null && bIsDrawNewResult == false && m_currCalcParams.TextureGPU != null)
            {
                if (m_currAreaOfSightSelectUserShowResults != null)
                    m_currAreaOfSightSelectUserShowResults.IsShowAreaOfSight = chxDrawAreaOfSight.Checked;
                DNEActionOptions visibility = GetVisibility(chxDrawAreaOfSight.Checked);
                m_currCalcParams.TextureGPU.SetVisibilityOption(visibility);
            }
        }

        private void chxDrawSeenPolygons_CheckedChanged(object sender, EventArgs e)
        {
            if (m_currCalcParams != null && bIsDrawNewResult == false && m_currCalcParams.ListSeenPolygon != null)
            {
                if (m_currAreaOfSightSelectUserShowResults != null)
                    m_currAreaOfSightSelectUserShowResults.IsShowSeenPolygon = chxDrawSeenPolygons.Checked;
                DNEActionOptions visibility = GetVisibility(chxDrawSeenPolygons.Checked);

                foreach (IDNMcObject obj in m_currCalcParams.ListSeenPolygon)
                {
                    obj.SetVisibilityOption(visibility);
                }
            }
        }

        private void chxDrawUnseenPolygons_CheckedChanged(object sender, EventArgs e)
        {
            if (m_currCalcParams != null && bIsDrawNewResult == false && m_currCalcParams.ListUnseenPolygon != null)
            {
                if (m_currAreaOfSightSelectUserShowResults != null)
                    m_currAreaOfSightSelectUserShowResults.IsShowUnseenPolygon = chxDrawUnseenPolygons.Checked;
                DNEActionOptions visibility = GetVisibility(chxDrawUnseenPolygons.Checked);

                foreach (IDNMcObject obj in m_currCalcParams.ListUnseenPolygon)
                {
                    obj.SetVisibilityOption(visibility);
                }
            }
        }

        private void chxSeenStaticObjects_CheckedChanged(object sender, EventArgs e)
        {
            if (m_currAreaOfSightSelectUserShowResults != null)
                m_currAreaOfSightSelectUserShowResults.IsShowSeenStaticObjects = chxDrawSeenStaticObjects.Checked;
            DrawStaticObject(m_currCalcParams, chxDrawSeenStaticObjects.Checked);
        }

        private void DrawStaticObject(MCTObjectsAreaOfSight currCalcParams, bool isVisible)
        {
            if (m_currActiveOverlay != null)
            {
                try
                {
                    if (currCalcParams.LstStaticObjectsColor != null && currCalcParams.LstStaticObjectsColor.Count > 0)
                    {
                        foreach (StaticObjectsColor staticObjectsColor in currCalcParams.LstStaticObjectsColor)
                        {
                            if (isVisible)
                            {
                                staticObjectsColor.layer.SetObjectsColor(currCalcParams.StaticObjectsSelectedColor, staticObjectsColor.lstVariantId.ToArray());
                                if (staticObjectsColor.aaStaticObjectsContours != null)
                                {
                                    for (int i = 0; i < staticObjectsColor.aaStaticObjectsContours.GetLength(0); i++)
                                    {
                                        DNSStaticObjectContour[] aStaticObjectsContours = staticObjectsColor.aaStaticObjectsContours[i];
                                        for (int j = 0; j < aStaticObjectsContours.GetLength(0); j++)
                                        {
                                            DrawPolygonContours(aStaticObjectsContours[j].aPoints, staticObjectsColor);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                staticObjectsColor.layer.SetObjectsColor(staticObjectsColor.color, staticObjectsColor.lstVariantId.ToArray());

                                foreach (IDNMcObject obj in staticObjectsColor.PolygonContours)
                                {
                                    obj.Remove();
                                }
                                staticObjectsColor.PolygonContours.Clear();
                            }
                        }

                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetObjectsColor ", McEx);
                }
            }
        }

        private void DrawPolygonContours(DNSMcVector3D[] points, StaticObjectsColor staticObjectsColor)
        {
            try
            {
                DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                DNEItemSubTypeFlags subTypeFlags = DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ACCURATE_3D_SCREEN_WIDTH;

                IDNMcObjectSchemeItem ObjSchemeItem = DNMcPolygonItem.Create(subTypeFlags,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                3f,
                                                                                null,
                                                                                new DNSMcFVector2D(0, -1),
                                                                                1f,
                                                                                DNEFillStyle._EFS_NONE);
                IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                                    ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    points,
                                                    false);

                staticObjectsColor.PolygonContours.Add(obj);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
            }
        }
        private List<IDNMcObject> m_PolygonContours = new List<IDNMcObject>();

        private DNEActionOptions GetVisibility(bool isVisible)
        {
            DNEActionOptions visibility = DNEActionOptions._EAO_FORCE_TRUE;
            if (isVisible == false)
                visibility = DNEActionOptions._EAO_FORCE_FALSE;

            return visibility;
        }
        private void btnCalcAOS_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            RemoveObjectList();
            CreateShemesAreaOfSight();
            ResetParamsAreaOfSight();

            DNSMcVector3D emptyPoint = new DNSMcVector3D();
            if (ctrl3DVectorAOSScouter.GetVector3D() != emptyPoint)
            {
                if (m_currViewport != null)
                    m_CurrScouterPoint = m_currViewport.OverlayManagerToViewportWorld(ctrl3DVectorAOSScouter.GetVector3D());
                else
                    m_CurrScouterPoint = ctrl3DVectorAOSScouter.GetVector3D();

            }
           

            if (rbSightObjectPolygon.Checked)
            {
                CalcPolygonAreaOfSight();
            }
            else if (rgbSightObjectEllipse.Checked)
            {
                CalcEllipseAreaOfSight();
            }
            else
            {
                CalcRectangleAreaOfSight();
            }
           
        }

        private void chxCalcAreaOfSight_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbIsMultipleScouters.Checked)
                chxIsCreateAndDrawMatrixAutomatic.Checked = chxCalcAreaOfSight.Checked;
        }

        #endregion
        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            m_ReturnObject = pObject;
            m_ReturnItem = pItem;
            m_ExitStatus = nExitCode;

            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResults);
        }

        public void InitItemResultsGetHeightPolygon(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            m_HPReturnObject = pObject;
            m_HPReturnItem = pItem;
            m_ExitStatus = nExitCode;

            SetPointsToGrid(m_HPReturnObject, ctrlExtremeHeightPoints);

            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResultsGetHeightPolygon);
            this.Show();
        }

        private void SetPointsToGrid(IDNMcObject mcObject, DataGridView dataGridView)
        {
            if (mcObject != null)
            {
                try
                {
                    dataGridView.Rows.Clear();
                    DNSMcVector3D[] polygonPoints = mcObject.GetLocationPoints(0);
                    if (polygonPoints != null)
                    {
                        dataGridView.RowCount = polygonPoints.Length + 1;
                        for (int i = 0; i < polygonPoints.Length; i++)
                        {
                            dataGridView[0, i].Value = polygonPoints[i].x;
                            dataGridView[1, i].Value = polygonPoints[i].y;
                            dataGridView[2, i].Value = polygonPoints[i].z;
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetLocationPoints", McEx);
                }
            }

        }

        private void SetPointsToGrid(IDNMcObject mcObject, CtrlPointsGrid ctrlPointsGrid)
        {
            if (mcObject != null)
            {
                try
                {
                    DNSMcVector3D[] polygonPoints = mcObject.GetLocationPoints(0);
                    ctrlPointsGrid.SetPoints(polygonPoints);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetLocationPoints", McEx);
                }
            }

        }


        public void InitItemResultsGetHeightAlongLine(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            if (nExitCode == 1)
            {
                m_HALReturnObject = pObject;
                m_HALReturnItem = pItem;

                // mListObjects.Add(m_HALReturnObject);

                MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResultsGetHeightAlongLine);
                this.Show();
            }
        }

        private void btnPreviousTarget_Click(object sender, EventArgs e)
        {
            if (m_IdxTargetFound > 1)
            {
                m_IdxTargetFound--;
                LoadIntersectionTergetFoundParams(m_IdxTargetFound - 1);
                UpdateCounterLable();
            }
        }

        private void btnNextTarget_Click(object sender, EventArgs e)
        {
            if (m_IdxTargetFound < m_lTargetsFound.Count)
            {
                LoadIntersectionTergetFoundParams(m_IdxTargetFound);
                m_IdxTargetFound++;
                UpdateCounterLable();
            }
        }

        private void LoadIntersectionTergetFoundParams(int index)
        {
            DNSTargetFound target = m_lTargetsFound[index];
            txtIntersectionTargetType.Text = target.eTargetType.ToString();
            ctrl3DVectorIntersectionPoint.SetVector3D( target.IntersectionPoint);
            txtIntersectionCoordSystem.Text = target.eIntersectionCoordSystem.ToString();

            if (target.pTerrain != null)
                ntxTerrainHashCode.SetString(Manager_MCNames.GetIdByObject(target.pTerrain));
            else
                ntxTerrainHashCode.SetString("Null");

            if (target.pTerrainLayer != null)
                ntxLayerHashCode.SetString(Manager_MCNames.GetIdByObject(target.pTerrainLayer));
            else
                ntxLayerHashCode.SetString("Null");

            uint bitCount = 32;
            if (target.eTargetType == DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER && target.pTerrainLayer is IDNMcVector3DExtrusionMapLayer)
            {
                bitCount = ((IDNMcVector3DExtrusionMapLayer)target.pTerrainLayer).GetObjectIDBitCount();
            }
            ntxTargetIndex.Text = GetTargetIdByBitCount(target.uTargetID, bitCount);

            if (target.ObjectItemData.pObject != null)
                ntxObjectHashCode.SetString(Manager_MCNames.GetIdByObject(target.ObjectItemData.pObject));
            else
                ntxObjectHashCode.SetString("Null");

            if (target.ObjectItemData.pItem != null)
                ntxItemHashCode.SetString(Manager_MCNames.GetIdByObject(target.ObjectItemData.pItem));
            else
                ntxItemHashCode.SetString("Null");

            txtItemPart.Text = target.ObjectItemData.ePartFound.ToString();
            ntxPartIndex.SetUInt32(target.ObjectItemData.uPartIndex);
        }

        private string GetTargetIdByBitCount(DNSMcVariantID uTargetID, uint bitCount)
        {
            string sTargetID;
            switch (bitCount)
            {
                case 32:
                    sTargetID = uTargetID.u32Bit.ToString(); break;
                case 64:
                    sTargetID = uTargetID.u64Bit.ToString(); break;
                case 128:
                    sTargetID = uTargetID.Get128bitAsUUIDString(); break;
                default:
                    sTargetID = uTargetID.u32Bit.ToString(); break;
            }
            return sTargetID;
        }

        private void UpdateCounterLable()
        {
            lblTargetCounter.Text = "<" + m_IdxTargetFound.ToString() + "/" + m_lTargetsFound.Count.ToString() + ">";
        }

        private void RemoveRayIntersectionLine()
        {
            if (objRayIntersectionLine != null)
            {
                try
                {
                    objRayIntersectionLine.Dispose();
                    objRayIntersectionLine.Remove();
                    objRayIntersectionLine = null;
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Remove", McEx);
                }
            }
        }

        private void RayIntersectionCalcDirection()
        {
            DNSMcVector3D vector = ctrl3DRayDestination.GetVector3D() - ctrl3DRayOrigin.GetVector3D();
            DNSMcVector3D NormalizeVector = new DNSMcVector3D();

            if (rbRayDestination.Checked)
            {
                NormalizeVector = vector.GetNormalized();
                ctrl3DRayDirection.SetVector3D(NormalizeVector);
            }
            else
            {
                NormalizeVector = ctrl3DRayDirection.GetVector3D();
            }
        }

        private void DrawRay()
        {
            if (m_currActiveOverlay != null)
            {
                //Calculate ray end location point
                double dist = ntxMaxDistance.GetDouble();
                DNSMcVector3D locationB = (ctrl3DRayDirection.GetVector3D() * dist) + ctrl3DRayOrigin.GetVector3D();

                try
                {
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[2];
                    locationPoints[0] = ctrl3DRayOrigin.GetVector3D();
                    locationPoints[1] = locationB;


                    IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                new DNSMcBColor(255, 0, 0, 255),
                                                                                3f);

                    objRayIntersectionLine = DNMcObject.Create(m_currActiveOverlay,
                                                        ObjSchemeItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);
                    mListObjects.Add(objRayIntersectionLine);
                    ((IDNMcLineItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                }
            }
        }

        private void lstTerrain_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbLayerTypes_SelectedIndexChanged(null, null);
        }

        private void SpatialQueriesForm_Load(object sender, EventArgs e)
        {
            try
            {
                IDNMcSpatialQueries terrainsSQ = (m_currViewport != null && m_currViewport.GetImageCalc() != null ? m_currViewport.GetImageCalc().GetSpatialQueries() : m_currSQ);
                foreach (IDNMcMapTerrain terr in terrainsSQ.GetTerrains())
                {
                    m_lstTerrainText.Add(Manager_MCNames.GetNameByObject(terr, "Terrain"));
                    m_lstTerrainValue.Add(terr);

                    foreach (IDNMcMapLayer layer in terr.GetLayers())
                    {
                        string layerType = layer.LayerType.ToString().Replace("_ELT_", "");
                        if (layer is IDNMcTraversabilityMapLayer)
                        {
                            m_lstTraversabilityMapLayersText.Add(Manager_MCNames.GetNameByObject(layer, layerType));
                            m_lstTraversabilityMapLayers.Add(layer as IDNMcTraversabilityMapLayer);
                        }
                        if (layer is IDNMcRasterMapLayer)
                        {
                            m_lstRasterMapLayersText.Add(Manager_MCNames.GetNameByObject(layer, layerType));
                            m_lstRasterMapLayers.Add(layer as IDNMcRasterMapLayer);
                        }
                    }
                }

                lbTraversabilityMapLayers.Items.AddRange(m_lstTraversabilityMapLayersText.ToArray());
                if (lbTraversabilityMapLayers.Items.Count == 1)
                {
                    lbTraversabilityMapLayers.SelectedIndex = 0;
                }

                lbRasterMapLayer.Items.AddRange(m_lstRasterMapLayersText.ToArray());
                if (lbRasterMapLayer.Items.Count == 1)
                {
                    lbRasterMapLayer.SelectedIndex = 0;
                }

                lstTerrain.Items.AddRange(m_lstTerrainText.ToArray());
                if (lstTerrain.Items.Count == 1)
                {
                    lstTerrain.SelectedIndex = 0;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrains()/GetLayers()", McEx);
            }
            rbRayDestination_CheckedChanged(null, null);

            ctrlQueryParams1.SetQueryParams(new DNSQueryParams());
        }

        private void btnTerrainNumCacheTilesOK_Click(object sender, EventArgs e)
        {
            RemoveObjectList();
            try
            {
                if (lstTerrain.SelectedIndex != -1)
                {
                    IDNMcMapTerrain SelectedTerrain = TerrainValueList[lstTerrain.SelectedIndex];
                    if (SelectedTerrain != null)
                    {
                        DNELayerKind layerKind = (DNELayerKind)Enum.Parse(typeof(DNELayerKind), cmbLayerTypes.SelectedItem.ToString());

                        m_currSQ.SetTerrainQueriesNumCacheTiles(SelectedTerrain, layerKind, ntxNumTiles.GetUInt32());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose terrain from the list", "Unchosen Terrain", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("SetTerrainQueriesNumCacheTiles", McEx);
            }
        }

        private void btnTerrainNumCacheTilesGet_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstTerrain.SelectedItem != null)
                {
                    DNELayerKind layerKind = (DNELayerKind)Enum.Parse(typeof(DNELayerKind), cmbLayerTypes.SelectedItem.ToString());

                    ntxNumTiles.SetUInt32(m_currSQ.GetTerrainQueriesNumCacheTiles(TerrainValueList[lstTerrain.SelectedIndex], layerKind));
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetTerrainQueriesNumCacheTiles", McEx);
            }
        }

        public List<string> TerrainNameList
        {
            get { return m_lstTerrainText; }
            set { m_lstTerrainText = value; }
        }

        public List<IDNMcMapTerrain> TerrainValueList
        {
            get { return m_lstTerrainValue; }
            set { m_lstTerrainValue = value; }
        }

        public IDNAreaOfSight SelectedAreaOfSight
        {
            get
            {
                if (m_currCalcParams != null)
                    return m_currCalcParams.AreaOfSight;
                else
                    return null;
            }
            set
            {
                if (m_currCalcParams != null && value != null)
                    m_currCalcParams.AreaOfSight = value;
            }
        }

        private void picbColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = picbColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picbColor.BackColor = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
            }
        }

        private void btnStartMultiThreadTest_Click(object sender, EventArgs e)
        {

            if (btnStartMultiThreadTest.BackColor == Color.Green)
            {
                btnStartMultiThreadTest.Text = "Stop";
                btnStartMultiThreadTest.BackColor = Color.Red;

                testRunningParams = new MultiThreadParams();
                m_numTestingPt = ntxNumTestingPt.GetInt32();
                testRunningParams.m_NumTestingPoints = m_numTestingPt;
                testRunningParams.SetResultArraySize();

                testRunningParams.QueryParams = ctrlQueryParams1.GetQueryParams();
                testRunningParams.QueryParams.pAsyncQueryCallback = null;

                int threadQuantity = ntxNumOfThread.GetInt32();
                int durationTime = ntxDurationTime.GetInt32();

                testRunningParams.OriginPoint = ctrl3DVectorFirstPoint.GetVector3D();

                double[] ResGetTerrainHeight = new double[m_numTestingPt];
                bool[] ResGetTerrainHeightIsFoundHeight = new bool[m_numTestingPt];
                float[] ResGetTerrainHeightsAlongLine = new float[m_numTestingPt];
                DNSMcVector3D?[] ResGetRayIntersectionPoint = new DNSMcVector3D?[m_numTestingPt];
                DNSMcVector3D[] ResRayDirection = new DNSMcVector3D[m_numTestingPt];

                Random ptRand = new Random();

                testRunningParams.PrintToLog = cbxIsLogThreadTest.Checked;
                m_ThrowException = chxThrowException.Checked;

                if (cbxIsLogThreadTest.Checked)
                {
                    string currentPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
                    currentPath = System.IO.Path.GetDirectoryName(currentPath);
                    string fileName = currentPath + "\\" + "MultiThreadQueryTest" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + ".txt";
                    testRunningParams.m_STW = new StreamWriter(fileName);

                }

                int i;
                int pointShifting = 0;
                for (i = 0; i < m_numTestingPt; i++)
                {
                    pointShifting = ptRand.Next(-15000, 15000);
                    testRunningParams.TargetPoints[i].x = testRunningParams.OriginPoint.x + pointShifting;
                    testRunningParams.TargetPoints[i].y = testRunningParams.OriginPoint.y + pointShifting;
                }
                i = 0;

                //Calc Points and Result
                // Get Terrain Height Result
                for (i = 0; i < m_numTestingPt; i++)
                {
                    try
                    {
                        DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
                        QueryParams.pAsyncQueryCallback = null;
                        m_currSQ.GetTerrainHeight(ConvertPointFromOMtoVP(testRunningParams.TargetPoints[i]),
                                                    out ResGetTerrainHeightIsFoundHeight[i],
                                                    out ResGetTerrainHeight[i],
                                                    normal,
                                                    QueryParams);

                        testRunningParams.ResGetTerrainHeight[i] = ResGetTerrainHeight[i];
                        testRunningParams.ResGetTerrainHeightIsFoundHeight[i] = ResGetTerrainHeightIsFoundHeight[i];
                    }
                    catch (MapCoreException McEx)
                    {
                        testRunningParams.AddException(McEx.ErrorNumber, i, MultiThreadParams.FuncCalc.TerrainHeight);
                        //MapCore.Common.Utilities.ShowErrorMessage("GetTerrainHeight", McEx);
                        // return;
                    }
                }
                // Get Terrain Heights Along Line Result
                for (i = 0; i < m_numTestingPt; i++)
                {
                    try
                    {
                        DNSMcVector3D[] LineVertex = new DNSMcVector3D[2];
                        LineVertex[0] = ConvertPointFromOMtoVP(testRunningParams.OriginPoint);
                        LineVertex[1] = ConvertPointFromOMtoVP(testRunningParams.TargetPoints[i]);
                        float[] slopes = new float[0];
                        DNSSlopesData SlopesData = new DNSSlopesData();
                        QueryParams.pAsyncQueryCallback = null;

                        DNSMcVector3D[] resultPoints = m_currSQ.GetTerrainHeightsAlongLine(LineVertex,
                                                                                                out slopes,
                                                                                                out SlopesData,
                                                                                                QueryParams);

                        //sum slops
                        ResGetTerrainHeightsAlongLine[i] = slopes.Sum();
                    }
                    catch (MapCoreException McEx)
                    {
                        testRunningParams.AddException(McEx.ErrorNumber, i, MultiThreadParams.FuncCalc.TerrainHeightsAlongLine);
                    }
                }
                testRunningParams.ResGetTerrainHeightsAlongLine = ResGetTerrainHeightsAlongLine;

                //Get Ray Intersection
                DNSMcVector3D rayDirection = new DNSMcVector3D(0, 1, 0);
                Random randYaw = new Random();
                Random randPitch = new Random();

                for (i = 0; i < m_numTestingPt; i++)
                {
                    try
                    {
                        double Yaw = randYaw.Next(0, 360);
                        double Pitch = randPitch.Next(-45, 0);
                        rayDirection.RotateByDegreeYawPitchRoll(Yaw, Pitch, 0);
                        DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
                        DNMcNullableOut<DNSMcVector3D> IntersectionPoint = new DNMcNullableOut<DNSMcVector3D>();
                        bool isIntersect = false;
                        DNMcNullableOut<double> dist = new DNMcNullableOut<double>();

                        DNSMcVector3D rayOrigin = new DNSMcVector3D(testRunningParams.TargetPoints[i].x, testRunningParams.TargetPoints[i].y, testRunningParams.ResGetTerrainHeight[i] + 100);
                        QueryParams.pAsyncQueryCallback = null;
                        m_currSQ.GetRayIntersection(ConvertPointFromOMtoVP(rayOrigin),
                                                            rayDirection,
                                                            double.MaxValue,
                                                            out isIntersect,
                                                            IntersectionPoint,
                                                            normal,
                                                            dist,
                                                            QueryParams);
                        if (isIntersect == false)
                        {
                            ResGetRayIntersectionPoint[i] = new DNSMcVector3D(0, 0, 0);
                            testRunningParams.ResGetRayIntersectionPoint[i] = ResGetRayIntersectionPoint[i];
                        }
                        else
                            testRunningParams.ResGetRayIntersectionPoint[i] = IntersectionPoint.Value;

                        testRunningParams.ResRayDirection[i] = rayDirection;
                    }
                    catch (MapCoreException McEx)
                    {
                        testRunningParams.AddException(McEx.ErrorNumber, i, MultiThreadParams.FuncCalc.RayIntersectionPoint);
                    }
                }

                //Frame Image Calc
                IDNMcFrameImageCalc imageCalc;
                m_CurrentObject = MCTMapFormManager.MapForm.Viewport;
                IDNMcMapTerrain[] Terrains = MCTMapFormManager.MapForm.Viewport.GetTerrains();
                IDNMcDtmMapLayer DTMlayer = Terrains[0].DtmLayer;
                IDNMcGridCoordinateSystem coorSys = DTMlayer.CoordinateSystem;
                DNSMcVector3D originalCameraLocation = new DNSMcVector3D(testRunningParams.TargetPoints[0].x, testRunningParams.TargetPoints[0].y, testRunningParams.ResGetTerrainHeight[0] + 1000);
                DNSCameraParams originaFrameCameraParams = new DNSCameraParams();
                originaFrameCameraParams.CameraPosition = originalCameraLocation;
                originaFrameCameraParams.dCameraOpeningAngleX = -45;
                originaFrameCameraParams.dCameraPitch = 45;
                originaFrameCameraParams.dCameraRoll = 0;
                originaFrameCameraParams.dCameraYaw = 0;
                originaFrameCameraParams.dPixelRatio = 1;
                originaFrameCameraParams.nPixelesNumX = 500;
                originaFrameCameraParams.nPixelesNumY = 500;
                originaFrameCameraParams.dOffsetCenterPixelX = 0;
                originaFrameCameraParams.dOffsetCenterPixelY = 0;
                testRunningParams.DTMlayer = DTMlayer;
                testRunningParams.coorSys = coorSys;
                imageCalc = DNMcFrameImageCalc.Create(ref originaFrameCameraParams, DTMlayer, coorSys);
                testRunningParams.originalImageCalc = imageCalc;

                Random randCameraYaw = new Random();
                Random randCamerapitch = new Random();

                DNSMcVector3D imagePoint = new DNSMcVector3D(250, 250, 0);
                for (i = 0; i < m_numTestingPt; i++)
                {
                    DNSMcVector3D cameraLocation = new DNSMcVector3D(testRunningParams.TargetPoints[i].x, testRunningParams.TargetPoints[i].y, testRunningParams.ResGetTerrainHeight[i] + 100);
                    DNSCameraParams frameCameraParams = new DNSCameraParams();
                    frameCameraParams.CameraPosition = cameraLocation;
                    frameCameraParams.dCameraOpeningAngleX = 45;
                    frameCameraParams.dCameraPitch = randCamerapitch.Next(-45, 0);
                    frameCameraParams.dCameraRoll = 0;
                    frameCameraParams.dCameraYaw = randCameraYaw.Next(0, 360);
                    frameCameraParams.dPixelRatio = 1;
                    frameCameraParams.nPixelesNumX = 500;
                    frameCameraParams.nPixelesNumY = 500;
                    frameCameraParams.dOffsetCenterPixelX = 0;
                    frameCameraParams.dOffsetCenterPixelY = 0;

                    try
                    {
                        imageCalc.SetCameraParams(frameCameraParams);
                        testRunningParams.ResImageToWorld[i] = imageCalc.ImagePixelToCoordWorld(testRunningParams.imagePoint);

                    }
                    catch (MapCoreException ex)
                    {
                        DNSMcVector3D exeption = new DNSMcVector3D(0, 0, 0);
                        testRunningParams.ResImageToWorld[i] = exeption;
                        testRunningParams.AddException(ex.ErrorNumber, i, MultiThreadParams.FuncCalc.ImageToWorld);
                    }
                    testRunningParams.frameCameraParams[i] = frameCameraParams;
                    testRunningParams.ResFrameImageCalc[i] = imageCalc;

                    testRunningParams.ThreadsFrameImageCalc[i] = DNMcFrameImageCalc.Create(ref testRunningParams.OriginalframeCameraParams, testRunningParams.DTMlayer, testRunningParams.coorSys);
                }

                m_StopWatchTimer = new System.Windows.Forms.Timer();
                m_SW = new Stopwatch();

                m_StopWatchTimer.Enabled = true;
                m_StopWatchTimer.Interval = 1000;
                m_StopWatchTimer.Start();
                m_StopWatchTimer.Tick += new EventHandler(StopWatchTimer_Tick);
                m_SW.Start();
                thread_counter = 0;

                // initiate the number of threads wanted
                for (int z = 0; z < threadQuantity; z++)
                {
                    Thread TestThread = new Thread(SpatialQueriesFunctions);
                    TestThread.Start(testRunningParams);
                    thread_counter++;
                }
            }
            else
            {
                testRunningParams.ExitRun = true;
            }
        }

        List<IDNMcFrameImageCalc> mListFrameImageCalc = new List<IDNMcFrameImageCalc>();

        void StopWatchTimer_Tick(object sender, EventArgs e)
        {
            if (m_SW.Elapsed.TotalMinutes >= ntxDurationTime.GetDouble())
            {
                m_StopWatchTimer.Stop();
                MessageBox.Show("Timeout");

                testRunningParams.ExitRun = true;

            }
        }

        private void FinishTestMultiThread()
        {
            if (btnStartMultiThreadTest.BackColor == Color.Red)
            {
                btnStartMultiThreadTest.Text = "Start";
                btnStartMultiThreadTest.BackColor = Color.Green;

                MessageBox.Show("Test Stopped");

                if (m_StopWatchTimer != null)
                    m_StopWatchTimer.Stop();
                if (m_SW != null)
                    m_SW.Reset();

                Thread.Sleep(2000);
                if (cbxIsLogThreadTest.Checked)
                {
                    testRunningParams.m_STW.Close();
                }
            }
        }

        int thread_counter = 0;
        //select random function
        private void SpatialQueriesFunctions(object multiThreadParams)
        {
            MultiThreadParams runningParams = (MultiThreadParams)multiThreadParams;
            Random startPtRand = new Random();
            int checkPointPosition = startPtRand.Next(0, m_numTestingPt);
            /* IDNMcFrameImageCalc ImageCalc;
             lock (runningParams.ResFrameImageCalc)
             {
                 ImageCalc = DNMcFrameImageCalc.Create(ref runningParams.OriginalframeCameraParams, runningParams.DTMlayer, runningParams.coorSys);
             }*/
            while (runningParams.ExitRun == false)
            {
                Random functionRand = new Random();
                int functionNum = functionRand.Next(0, 4);
                if (functionNum == 0)
                {
                    Get_Terrain_Height(runningParams, checkPointPosition);
                }
                if (functionNum == 1)
                {
                    Get_Terrain_Heights_Along_Line(runningParams, checkPointPosition);
                }
                if (functionNum == 2)
                {
                    Get_Ray_Intersection(runningParams, checkPointPosition);
                }
                if (functionNum == 3)
                {
                    Create_Frame_Image_Calc(runningParams, checkPointPosition);
                }
            }
            System.Threading.Interlocked.Decrement(ref thread_counter);

            if (thread_counter == 0)
            {
                btnStartMultiThreadTest.BeginInvoke(new TestMultiThreadDelegate(TestMultiThreadFinishDelegate));
            }
        }

        public delegate void TestMultiThreadDelegate();

        public void TestMultiThreadFinishDelegate()
        {
            FinishTestMultiThread();
        }

        public void Get_Terrain_Height(object multiThreadParams, int checkPointPosition)
        {
            MultiThreadParams mtParams = (MultiThreadParams)multiThreadParams;

            DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
            bool pbHeightFound = false;
            double TerrainHeight = 0;
            try
            {
                m_currSQ.GetTerrainHeight(ConvertPointFromOMtoVP(mtParams.TargetPoints[checkPointPosition]),
                                                out pbHeightFound,
                                                out TerrainHeight,
                                                normal,
                                                mtParams.QueryParams);
                //compare Results
                if (mtParams.ResGetTerrainHeightIsFoundHeight[checkPointPosition] != pbHeightFound)
                {
                    if (mtParams.PrintToLog)
                        mtParams.GenerateLog("GetTerrainHeight (IsFoundHeight)", mtParams.TargetPoints[checkPointPosition].ToString(), pbHeightFound.ToString());
                    if (m_ThrowException == true)
                        throw new System.Exception("\nFunction:\tGetTerrainHeight (IsFoundHeight)\nAt Point:\t" + mtParams.TargetPoints[checkPointPosition].x.ToString() + ", " +
                                                                                                        mtParams.TargetPoints[checkPointPosition].y.ToString() + ", " +
                                                                                                        mtParams.TargetPoints[checkPointPosition].z.ToString() + ", " +
                                                                                                        "Is Found Height" + pbHeightFound.ToString());

                }
                if (mtParams.ResGetTerrainHeight[checkPointPosition] != TerrainHeight)
                {
                    if (mtParams.PrintToLog)
                        mtParams.GenerateLog("GetTerrainHeight (TerrainHeight)", mtParams.TargetPoints[checkPointPosition].ToString(), TerrainHeight.ToString());
                    if (m_ThrowException == true)
                        throw new System.Exception("\nFunction:\tGetTerrainHeight (TerrainHeight)\nAt Point:\t" + mtParams.TargetPoints[checkPointPosition].x.ToString() + ", " +
                                                                                                        mtParams.TargetPoints[checkPointPosition].y.ToString() + ", " +
                                                                                                        mtParams.TargetPoints[checkPointPosition].z.ToString() + ", " +
                                                                                                        "Terrain Height" + TerrainHeight.ToString());
                }

            }
            catch (MapCoreException ex)
            {
                if (mtParams.IsExistExceptionsThrows(ex.ErrorNumber, checkPointPosition, MultiThreadParams.FuncCalc.TerrainHeight) == false)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetTerrainHeight", ex);
                }
            }
            /* catch(System.Exception e)
             {
                 MessageBox.Show("Error");
             }*/

        }

        public void Get_Terrain_Heights_Along_Line(object multiThreadParams, int checkPointPosition)
        {

            MultiThreadParams mtParams = (MultiThreadParams)multiThreadParams;

            DNSMcVector3D[] LineVertex = new DNSMcVector3D[2];
            LineVertex[0] = ConvertPointFromOMtoVP(mtParams.OriginPoint);
            LineVertex[1] = ConvertPointFromOMtoVP(mtParams.TargetPoints[checkPointPosition]);

            float[] slopes = new float[0];
            DNSSlopesData SlopesData;
            try
            {
                DNSMcVector3D[] resultPoints = m_currSQ.GetTerrainHeightsAlongLine(LineVertex,
                                                                                    out slopes,
                                                                                    out SlopesData,
                                                                                    mtParams.QueryParams);

                if (mtParams.ResGetTerrainHeightsAlongLine[checkPointPosition] != slopes.Sum())
                {
                    if (mtParams.PrintToLog)
                        mtParams.GenerateLog("GetTerrainHeightsAlongLine", mtParams.ResGetTerrainHeightsAlongLine[checkPointPosition].ToString(), slopes.Sum().ToString());
                    if (m_ThrowException == true)
                        throw new System.Exception("\nFunction:\tGetTerrainHeightsAlongLine\n" + "First Point:\t" + LineVertex[0].x.ToString() + ", " +
                                                                                                        LineVertex[0].y.ToString() + ", " +
                                                                                                        LineVertex[0].z.ToString() +
                                                                                                        "\nSecond Point:\t" +
                                                                                                        LineVertex[1].x.ToString() + ", " +
                                                                                                        LineVertex[1].y.ToString() + ", " +
                                                                                                        LineVertex[1].z.ToString());
                }
            }
            catch (MapCoreException ex)
            {
                if (mtParams.IsExistExceptionsThrows(ex.ErrorNumber, checkPointPosition, MultiThreadParams.FuncCalc.TerrainHeightsAlongLine) == false)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetTerrainHeightsAlongLine", ex);
                }
            }
            /*  catch (System.Exception e)
              {
                  MessageBox.Show("Error");
              }*/
        }

        public void Get_Ray_Intersection(object multiThreadParams, int checkPointPosition)
        {

            MultiThreadParams mtParams = (MultiThreadParams)multiThreadParams;
            try
            {
                DNMcNullableOut<DNSMcVector3D> intersectionPt = new DNMcNullableOut<DNSMcVector3D>();
                DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
                bool isIntersect = false;
                DNMcNullableOut<double> dist = new DNMcNullableOut<double>();

                DNSMcVector3D rayDirection = new DNSMcVector3D();
                DNSMcVector3D rayOrigin = new DNSMcVector3D(mtParams.TargetPoints[checkPointPosition].x, mtParams.TargetPoints[checkPointPosition].y, mtParams.ResGetTerrainHeight[checkPointPosition] + 100);
                rayDirection = mtParams.ResRayDirection[checkPointPosition];
                m_currSQ.GetRayIntersection(ConvertPointFromOMtoVP(rayOrigin),
                                            ConvertPointFromOMtoVP(rayDirection),
                                            double.MaxValue,
                                            out isIntersect,
                                            intersectionPt,
                                            normal,
                                            dist,
                                            mtParams.QueryParams);

                if (isIntersect == false)
                {
                    intersectionPt.Value = new DNSMcVector3D(0, 0, 0);
                }
                if (mtParams.ResGetRayIntersectionPoint[checkPointPosition] != intersectionPt.Value)
                {
                    if (mtParams.PrintToLog)
                        mtParams.GenerateLog("GetRayIntersection", mtParams.TargetPoints[checkPointPosition].ToString(), intersectionPt.ToString());
                    if (m_ThrowException == true)

                        throw new System.Exception("\nFunction:\tGetRayIntersection\n" + "Ray Origin Point:\t" + rayOrigin.x.ToString() + ", " +
                                                                                                        rayOrigin.y.ToString() + ", " +
                                                                                                        rayOrigin.z.ToString());
                }
            }
            catch (MapCoreException ex)
            {
                if (mtParams.IsExistExceptionsThrows(ex.ErrorNumber, checkPointPosition, MultiThreadParams.FuncCalc.RayIntersectionPoint) == false)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("GetRayIntersection", ex);
                }
            }
        }

        public void Create_Frame_Image_Calc(object multiThreadParams, int checkPointPosition/*, IDNMcFrameImageCalc imageCalc*/)
        {

            MultiThreadParams mtParams = (MultiThreadParams)multiThreadParams;
            IDNMcFrameImageCalc imageCalc = mtParams.ThreadsFrameImageCalc[checkPointPosition];
            DNSMcVector3D ImagetoWorld;
            try
            {
                DNSCameraParams frameCameraParams = mtParams.frameCameraParams[checkPointPosition];

                //try
                //{
                imageCalc.SetCameraParams(frameCameraParams);
                ImagetoWorld = imageCalc.ImagePixelToCoordWorld(mtParams.imagePoint);
                //}
                //catch (MapCoreException McEx)
                //{
                //    DNSMcVector3D exeption = new DNSMcVector3D(0, 0, 0);
                //    ImagetoWorld = exeption;
                //}
                if (mtParams.ResImageToWorld[checkPointPosition] != ImagetoWorld)
                {
                    if (mtParams.PrintToLog)
                        mtParams.GenerateLog("FrameImageCalc", mtParams.TargetPoints[checkPointPosition].ToString(), imageCalc.ToString());
                    if (m_ThrowException == true)
                        throw new System.Exception("\nFunction:\tFrameImageCalc\n" + "frame Camera Params:\t" + mtParams.frameCameraParams[checkPointPosition].ToString());
                }
            }
            catch (MapCoreException ex)
            {
                if (mtParams.IsExistExceptionsThrows(ex.ErrorNumber, checkPointPosition, MultiThreadParams.FuncCalc.ImageToWorld) == false)
                {
                    Utilities.ShowErrorMessage("ImagePixelToCoordWorld", ex);
                }
            }
            /*  catch(Exception e)
              {
                  MessageBox.Show("Error");
              }*/
        }

        //Track Smoother

        private void btnDrawPath_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                RemoveObjectList();

                this.Hide();

                try
                {


                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                    IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                ObjectPropertiesBase.LineStyle,
                                                                                ObjectPropertiesBase.LineColor,
                                                                                ObjectPropertiesBase.LineWidth,
                                                                                ObjectPropertiesBase.LineTexture,
                                                                                ObjectPropertiesBase.LineTextureHeightRange,
                                                                                1f);

                    IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                                        ObjSchemeItem,
                                                        ObjectPropertiesBase.LocationCoordSys,
                                                        locationPoints,
                                                        ObjectPropertiesBase.LocationRelativeToDtm);
                    mListObjects.Add(obj);

                    ((IDNMcLineItem)ObjSchemeItem).SetNumSmoothingLevels(ObjectPropertiesBase.LineBasedSmoothingLevels, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
                    ((IDNMcLineItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.LineOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetOutlineWidth(ObjectPropertiesBase.LineOutlineWidth, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetShapeType(ObjectPropertiesBase.ShapeType);
                    ((IDNMcLineItem)ObjSchemeItem).SetVerticalHeight(ObjectPropertiesBase.VerticalHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetSidesFillStyle(ObjectPropertiesBase.SidesFillStyle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetSidesFillColor(ObjectPropertiesBase.SidesFillColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.SidesFillTexture, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    if (ObjectPropertiesBase.ImageCalc != null)
                        obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                    MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitRandomGPSPoint);
                    MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem);
                    //       m_ReturnObject.Remove();
                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_currActiveOverlay.GetOverlayManager());
                }

                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Failed in creating line item", McEx);
                }
            }
        }

        //Create Simulated GPS points

        public void InitRandomGPSPoint(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitRandomGPSPoint);

            try
            {
                float[] slops = new float[0];
                DNSSlopesData SlopesData;
                List<object> listInput = new List<object>();

                DNSMcVector3D[] sqPoints = pObject.GetLocationPoints(0);
                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInput, AsyncQueryFunctionName.GetTerrainHeightsAlongLine, AsyncQueryFunctionName.InitRandomGPSPoint);
                listInput.Add(sqPoints);

                for (int i = 0; i < sqPoints.Length; i++)
                {
                    sqPoints[i] = ConvertPointFromOMtoVP(sqPoints[i]);
                }
                m_currSQ.GetTerrainHeightsAlongLine(sqPoints,
                                                    out slops,
                                                    out SlopesData,
                                                    QueryParams);
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("InitRandomGPSPoint", McEx);
            }
            this.Show();
        }

        public void InitRandomGPSPointAfterAsync(DNSMcVector3D[] resultPoints)
        {
            try
            {
                m_ResultPoints = resultPoints;
                DNSMcVector3D[] simulatedPoints = new DNSMcVector3D[resultPoints.Length * 5];
                Random rnd = new Random();
                for (int i = 0; i <= resultPoints.Length - 1; i++)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        simulatedPoints[i + z].x = resultPoints[i].x + rnd.Next(-5, 5);
                        simulatedPoints[i + z].y = resultPoints[i].y + rnd.Next(-5, 5);

                        // Draw Points
                        IDNMcObjectSchemeItem objSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                     DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                     DNEItemGeometryType._EGT_GEOMETRIC_IN_OVERLAY_MANAGER,
                                                                                     2, 2, 0, 360, 0,
                                                                                     DNELineStyle._ELS_SOLID,
                                                                                     ObjectPropertiesBase.LineColor,
                                                                                     1, null, ObjectPropertiesBase.LineTextureHeightRange, 0,
                                                                                     DNEFillStyle._EFS_SOLID, ObjectPropertiesBase.FillColor, null);
                        DNSMcVector3D[] newpoint = new DNSMcVector3D[1];
                        newpoint[0] = simulatedPoints[i + z];

                        IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                                            objSchemeItem,
                                                            ObjectPropertiesBase.LocationCoordSys,
                                                            newpoint,
                                                            ObjectPropertiesBase.LocationRelativeToDtm);
                        mListObjects.Add(obj);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("InitRandomGPSPoint", McEx);
            }
            this.Show();
        }

        private void btnGetSmoothedPath_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            try
            {
                double SmoothDistance = ntxSmoothDistance.GetDouble();
                ts = DNMcTrackSmoother.Create(mGeographicCalculations, SmoothDistance);
                if (IsAddPoint == true)
                {
                    ts.AddPoints(m_ResultPoints);
                }
                SmoothedTrack = ts.GetSmoothedTrack();
                IsAddPoint = false;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in Add Point or Get Smoothed Track", McEx);
            }
            if (m_currActiveOverlay != null)
            {
                //Draw Smoothed Track
                IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                ObjectPropertiesBase.LineStyle,
                                                                                new DNSMcBColor(255, 0, 0, 255),
                                                                                3,
                                                                                ObjectPropertiesBase.LineTexture,
                                                                                ObjectPropertiesBase.LineTextureHeightRange,
                                                                                1f);

                IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                                    ObjSchemeItem,
                                                    ObjectPropertiesBase.LocationCoordSys, SmoothedTrack,
                                                    ObjectPropertiesBase.LocationRelativeToDtm);
                mListObjects.Add(obj);
            }
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                this.Hide();
                IsAddPoint = true;
                try
                {
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];

                    IDNMcObjectSchemeItem ObjSchemeItem = DNMcLineItem.Create(ObjectPropertiesBase.ItemSubTypeFlags,
                                                                                ObjectPropertiesBase.LineStyle,
                                                                                ObjectPropertiesBase.LineColor,
                                                                                ObjectPropertiesBase.LineWidth,
                                                                                ObjectPropertiesBase.LineTexture,
                                                                                ObjectPropertiesBase.LineTextureHeightRange,
                                                                                1f);

                    IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                                        ObjSchemeItem,
                                                        ObjectPropertiesBase.LocationCoordSys,
                                                        locationPoints,
                                                        ObjectPropertiesBase.LocationRelativeToDtm);
                    mListObjects.Add(obj);
                    ((IDNMcLineItem)ObjSchemeItem).SetNumSmoothingLevels(ObjectPropertiesBase.LineBasedSmoothingLevels, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetGreatCirclePrecision(ObjectPropertiesBase.LineBasedGreatCirclePrecision);
                    ((IDNMcLineItem)ObjSchemeItem).SetDrawPriorityGroup(ObjectPropertiesBase.DrawPriorityGroup, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetOutlineColor(ObjectPropertiesBase.LineOutlineColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetOutlineWidth(ObjectPropertiesBase.LineOutlineWidth, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetShapeType(ObjectPropertiesBase.ShapeType);
                    ((IDNMcLineItem)ObjSchemeItem).SetVerticalHeight(ObjectPropertiesBase.VerticalHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetSidesFillStyle(ObjectPropertiesBase.SidesFillStyle, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetSidesFillColor(ObjectPropertiesBase.SidesFillColor, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)ObjSchemeItem).SetSidesFillTexture(ObjectPropertiesBase.SidesFillTexture, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    if (ObjectPropertiesBase.ImageCalc != null)
                        obj.SetImageCalc(ObjectPropertiesBase.ImageCalc);

                    MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitRandomGPSPoint);
                    MCTMapFormManager.MapForm.EditMode.StartInitObject(obj, ObjSchemeItem);
                    //       m_ReturnObject.Remove();
                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(m_currActiveOverlay.GetOverlayManager());
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Failed in creating line item", McEx);
                }
            }
            else
                MessageBox.Show("There is no active overlay");
        }

        private void btnGetLocation_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            RemoveObjectList();
            DNSMcVector3D resultLocation = new DNSMcVector3D();
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl3DLocationFrstOrgnPt.GetVector3D());
            listInputs.Add(ntxFirstDistance.GetDouble());
            listInputs.Add(ntxFirstAzimut.GetDouble());
            listInputs.Add(ctrl3DLocationScndOrgnPt.GetVector3D());
            listInputs.Add(ntxSecondDistance.GetDouble());
            listInputs.Add(ntxTargetHeightAboveGround.GetDouble());

            if (cbLocationFromTwoDistancesAndAzimuthAsync.Checked)
                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth);
            else
                QueryParams.pAsyncQueryCallback = null;
            try
            {
                resultLocation = m_currSQ.LocationFromTwoDistancesAndAzimuth(
                                           ConvertPointFromOMtoVP(ctrl3DLocationFrstOrgnPt.GetVector3D()),
                                           ntxFirstDistance.GetDouble(),
                                           ntxFirstAzimut.GetDouble(),
                                           ConvertPointFromOMtoVP(ctrl3DLocationScndOrgnPt.GetVector3D()),
                                           ntxSecondDistance.GetDouble(),
                                           ntxTargetHeightAboveGround.GetDouble(),
                                           QueryParams);

                if (!cbLocationFromTwoDistancesAndAzimuthAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(resultLocation);

                    AddResultToList(AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth, McEx.ErrorCode, cbLocationFromTwoDistancesAndAzimuthAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("LocationFromTwoDistancesAndAzimuth", McEx);
            }
        }

        private DNSMcVector3D ConvertPointFromVPtoOM(DNSMcVector3D vpPoint)
        {

            DNSMcVector3D omPoint = new DNSMcVector3D();
            int zone = 0;
            if (dNMcGridConverterOMToVP != null)
            {
                dNMcGridConverterOMToVP.ConvertBtoA(vpPoint, out omPoint, out zone);
                return omPoint;
            }
            else
                return vpPoint;
        }

        private DNSMcVector3D ConvertPointFromOMtoVP(DNSMcVector3D omPoint)
        {
            DNSMcVector3D vpPoint = new DNSMcVector3D();
            int zone = 0;
            if (dNMcGridConverterOMToVP != null && m_currViewport.OverlayManager != null)
                dNMcGridConverterOMToVP.ConvertAtoB(omPoint, out vpPoint, out zone);
            else
                vpPoint = omPoint;
            return vpPoint;
        }

        private void OnLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D resultLocation, bool isAsync)
        {
            try
            {
                ctrl3DLocationResult.SetVector3D( ConvertPointFromVPtoOM(resultLocation));

                DrawPictureInPoint(ctrl3DLocationResult.GetVector3D());

                DNMcNullableOut<double> distance = new DNMcNullableOut<double>();
                if (mGeographicCalculations != null)
                {
                    mGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations(
                      ConvertPointFromOMtoVP(ctrl3DLocationFrstOrgnPt.GetVector3D()),
                      resultLocation,
                      null,
                      distance,
                      true);
                    ntxDistanceFromFirstLocationAndResult.SetDouble(distance.Value);
                    mGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations(
                        ConvertPointFromOMtoVP(ctrl3DLocationScndOrgnPt.GetVector3D()),
                        resultLocation,
                        null,
                        distance,
                        true);
                    ntxDistanceFromSecondLocationAndResult.SetDouble(distance.Value);
                }
                bool pbHeightFound = false;
                double pHeight = 0;
                DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
                List<object> listInputs = new List<object>();
                listInputs.Add(resultLocation);
                if (isAsync)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetTerrainHeight, AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth);
                else
                    QueryParams.pAsyncQueryCallback = null;
                try
                {
                    m_currSQ.GetTerrainHeight(resultLocation,
                                                out pbHeightFound,
                                                out pHeight,
                                                normal,
                                                QueryParams);

                    if (!isAsync)
                    {
                        cbLocationFromDistancesAndAzimuthFoundHeight.Checked = pbHeightFound;
                        ntxDifferenceHeightsFromResultAndTerrain.SetDouble(resultLocation.z - pHeight);
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTerrainHeight", McEx);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AzimuthAndDistanceBetweenTwoLocations", McEx);
            }
        }

        public void UpdateLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D resultLocation, bool heightFound, double height)
        {
            cbLocationFromDistancesAndAzimuthFoundHeight.Checked = heightFound;
            ntxDifferenceHeightsFromResultAndTerrain.SetDouble(resultLocation.z - height);
        }

        /*public void UpdateTerrainHeightMatrix(bool bHeightFound, double dHeight, float rectWidth, float rectHeight, DNSMcVector3D[] rectPoints)
        {
            rectPoints[1].z = dHeight;
            CreateRectangleMatrix(rectWidth, rectHeight, rectPoints);
        }*/

        private void DrawPictureInPoint(DNSMcVector3D location)
        {
            if (m_currActiveOverlay != null)
            {
                try
                {
                    DNSMcVector3D[] points = new DNSMcVector3D[1];
                    points[0] = location;

                    IDNMcTexture texture = DNMcBitmapHandleTexture.Create(Icons.TesterVertex.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
                    IDNMcObjectSchemeItem picItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, texture);
                    ((IDNMcSymbolicItem)picItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    mListObjects.Add(DNMcObject.Create(m_currActiveOverlay, picItem, DNEMcPointCoordSystem._EPCS_WORLD, points, false));
                    // turn on all viewports render needed flags
                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(MCTMapFormManager.MapForm.Viewport);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("create icons for node calculate location points", McEx);
                }
            }
        }

        private void OnLocationFromTwoDistancesAndAzimuthShowInputs(bool isAsync, List<object> listInputs)
        {
            cbLocationFromTwoDistancesAndAzimuthAsync.Checked = isAsync;
            ctrl3DLocationFrstOrgnPt.SetVector3D( (DNSMcVector3D)listInputs[0]);
            ntxFirstDistance.SetDouble((double)listInputs[1]);
            ntxFirstAzimut.SetDouble((double)listInputs[2]);
            ctrl3DLocationScndOrgnPt.SetVector3D( (DNSMcVector3D)listInputs[3]);
            ntxSecondDistance.SetDouble((double)listInputs[4]);
            ntxTargetHeightAboveGround.SetDouble((double)listInputs[5]);
            DrawPictureInPoint(ctrl3DLocationFrstOrgnPt.GetVector3D());
            DrawPictureInPoint(ctrl3DLocationScndOrgnPt.GetVector3D());
            try
            {
                if (m_currActiveOverlay != null)
                {
                    DNSMcVector3D[] locationPoints = new DNSMcVector3D[1];
                    locationPoints[0] = ctrl3DLocationFrstOrgnPt.GetVector3D();
                    DrawEllipse(locationPoints, ntxFirstDistance.GetFloat(), new DNSMcBColor(0, 0, 255, 150));
                    locationPoints[0] = ctrl3DLocationScndOrgnPt.GetVector3D();
                    DrawEllipse(locationPoints, ntxSecondDistance.GetFloat(), new DNSMcBColor(255, 255, 255, 150));
                }
                else
                    MessageBox.Show("There is no active overlay");
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Failed in creating ellipse item", McEx);
            }
        }

        private void DrawEllipse(DNSMcVector3D[] locationPoints, float redius, DNSMcBColor color)
        {
            if (m_currActiveOverlay != null)
            {
                try
                {
                    DNSMcBColor m_ItemColor = new DNSMcBColor(255, 255, 0, 255);
                    DNEFillStyle m_FillStyle = DNEFillStyle._EFS_CROSS;
                    DNELineStyle m_LineStyle = DNELineStyle._ELS_SOLID;

                    IDNMcObjectSchemeItem m_ObjSchemeItem = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                                             DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                                             DNEItemGeometryType._EGT_GEOGRAPHIC,
                                                                                                             redius,
                                                                                                             redius,
                                                                                                             0,
                                                                                                             360,
                                                                                                             0,
                                                                                                             m_LineStyle,
                                                                                                             color,
                                                                                                             2,
                                                                                                             null,
                                                                                                             new DNSMcFVector2D(1, 1),
                                                                                                             2,
                                                                                                             m_FillStyle,
                                                                                                             color);


                    ((IDNMcEllipseItem)m_ObjSchemeItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    IDNMcObject obj = DNMcObject.Create(m_currActiveOverlay,
                                               m_ObjSchemeItem,
                                               DNEMcPointCoordSystem._EPCS_WORLD,
                                              locationPoints,
                                              false);
                    mListObjects.Add(obj);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Failed in creating ellipse item", McEx);
                }
            }
        }

        private void SpatialQueriesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            RemoveObjectList();
            RemoveObject(m_HALReturnObject);
            m_HALReturnObject = null;

            foreach (IDNAreaOfSight areaOfSight in m_lstIAreaOfSight)
            {
                try
                {
                    areaOfSight.Dispose();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("IDNAreaOfSight.Dispose()", McEx);
                }
            }
            if (Manager_MCLayers.ListSpatialQueriesForms.Contains(this))
            {
                Manager_MCLayers.ListSpatialQueriesForms.Remove(this);
            }

            CancelAllCallbacks();

            if(m_TreeViewDisplayForm != null)
            {
                m_TreeViewDisplayForm.BuildTree();
            }
        }

        public void AddAreaOfSight(IDNAreaOfSight areaOfSight)
        {
            if(!m_lstIAreaOfSight.Contains(areaOfSight))
                m_lstIAreaOfSight.Add(areaOfSight);
        }

        private void RemoveObjectList()
        {
            try
            {
                IDNMcObject obj;
                for (int i = 0; i < mListObjects.Count; i++)
                {
                    obj = mListObjects[i];
                    if (obj != null)
                    {
                        obj.Dispose();
                        obj.Remove();
                        obj = null;
                    }
                }
                mListObjects.Clear();
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Remove Object", McEx);
            }

            /*   try
               {
                   if (m_HALReturnObject != null)
                   {
                       m_HALReturnObject.Dispose();
                       m_HALReturnObject.Remove();
                       m_HALReturnObject = null;
                   }
               }
               catch (MapCoreException McEx)
               {
                   Utilities.ShowErrorMessage("Remove Object", McEx);
               }*/

            RemoveAreaOfSight();
        }

        private void RemoveObject(IDNMcObject obj)
        {
            try
            {
                if (obj != null)
                {
                    obj.Dispose();
                    obj.Remove();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Remove Object", McEx);
            }
        }

        private void chxAOSIsScouterHeightAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            CheckAOSIsScouterHeightAbsolute();
        }

        private void CheckAOSIsScouterHeightAbsolute()
        {
            if (chxAOSIsScouterHeightAbsolute.Checked)
                ctrlSamplePointAOSScouter._PointZValue = double.MaxValue;
            else
                ctrlSamplePointAOSScouter._PointZValue = 1.7;
        }

        private void ntxDurationTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbRayDestination_CheckedChanged(object sender, EventArgs e)
        {
            ctrl3DRayDestination.SetVector3D( new DNSMcVector3D());
            ctrl3DRayDirection.SetVector3D( new DNSMcVector3D());
            if (rbRayDestination.Checked)
            {
                ctrlRayDestination.Enabled = true;
                ctrl3DRayDestination.IsReadOnly = false;
                ctrl3DRayDirection.IsReadOnly = true;
            }
            else
            {
                ctrlRayDestination.Enabled = false;
                ctrl3DRayDestination.IsReadOnly = true;
                ctrl3DRayDirection.IsReadOnly = false;
            }
        }

        public void AddResultToList(AsyncQueryFunctionName functionName, List<Object> listResults, bool isAsync, List<Object> listInputs)
        {
            int index = dgvAsyncQueryResults.Rows.Add();
            dgvAsyncQueryResults.Rows[index].Cells[0].Value = index + 1;
            dgvAsyncQueryResults.Rows[index].Cells[1].Value = functionName.ToString();
            dgvAsyncQueryResults.Rows[index].Cells[2].Value = isAsync;
            dgvAsyncQueryResults.Rows[index].Tag = listResults;
            dgvAsyncQueryResults.Rows[index].Cells[0].Tag = listInputs;
            if (functionName == AsyncQueryFunctionName.GetAreaOfSight || functionName == AsyncQueryFunctionName.GetAreaOfSightForMultipleScouters)
                dgvAsyncQueryResults.Rows[index].Cells[4].Value = listInputs[0].ToString();
            ClickShowResults(index);
        }

        public void AddErrorToList(AsyncQueryFunctionName functionName, DNEMcErrorCode eErrorCode, bool isAsync, List<Object> listInputs)
        {
            int index = dgvAsyncQueryResults.Rows.Add();
            dgvAsyncQueryResults.Rows[index].Cells[0].Value = index + 1;
            dgvAsyncQueryResults.Rows[index].Cells[1].Value = functionName.ToString();
            dgvAsyncQueryResults.Rows[index].Cells[2].Value = isAsync;
            dgvAsyncQueryResults.Rows[index].Cells[3].Value = IDNMcErrors.ErrorCodeToString(eErrorCode);
            dgvAsyncQueryResults.Rows[index].Cells[0].Tag = listInputs;
            ClickShowResults(index);
        }

        private void ClickShowResults(int rowIndex)
        {
            CheckIsChangeCurrentViewport();

            for (int i = 0; i < dgvAsyncQueryResults.Rows.Count; i++)
            {
                dgvAsyncQueryResults.Rows[i].Selected = false;
            }
            dgvAsyncQueryResults.Rows[rowIndex].Selected = true;
            RemoveObjectList();
            string functionName = dgvAsyncQueryResults.Rows[rowIndex].Cells[1].Value.ToString();
            AsyncQueryFunctionName asyncQueryFunctionName = (AsyncQueryFunctionName)Enum.Parse(typeof(AsyncQueryFunctionName), functionName);
            List<Object> listResults = (List<Object>)dgvAsyncQueryResults.Rows[rowIndex].Tag;
            List<Object> listInputs = (List<Object>)dgvAsyncQueryResults.Rows[rowIndex].Cells[0].Tag;
            bool isAsync = (bool)dgvAsyncQueryResults.Rows[rowIndex].Cells[2].Value;
            bool isError = (dgvAsyncQueryResults.Rows[rowIndex].Cells[3].Value != null);

            switch (asyncQueryFunctionName)
            {
                case AsyncQueryFunctionName.GetTerrainHeight:
                    GetTerrainHeightShowInputs(isAsync, (DNSMcVector3D)listInputs[0]);
                    if (!isError)
                        GetTerrainHeightResult(isAsync, (bool)listResults[0], (double)listResults[1], (DNSMcVector3D?)listResults[2], (DNSMcVector3D)listInputs[0]);
                    break;
                case AsyncQueryFunctionName.GetTerrainHeightsAlongLine:
                    if (isError)
                        GetTerrainHeightsAlongLineError(isAsync, (DNSMcVector3D[])listInputs[0]);
                    else
                        GetTerrainHeightsAlongLineResult(isAsync, (float[])listResults[0], (DNSSlopesData)listResults[1], (DNSMcVector3D[])listResults[2]);
                    break;
                case AsyncQueryFunctionName.GetExtremeHeightPointsInPolygon:
                    if (isError)
                        GetExtremeHeightPointsInPolygonError(isAsync, (DNSMcVector3D[])listInputs[0]);
                    else
                        GetExtremeHeightPointsInPolygonResults(isAsync, (bool)listResults[0], (DNSMcVector3D)listResults[1], (DNSMcVector3D)listResults[2], (DNSMcVector3D[])listInputs[0]);
                    break;
                case AsyncQueryFunctionName.GetTerrainAngles:
                    GetTerrainAnglesShowInputs(isAsync, (DNSMcVector3D)listInputs[0], (double)listInputs[1]);
                    if (!isError)
                        GetTerrainAnglesResults((double)listResults[0], (double)listResults[1]);
                    break;
                case AsyncQueryFunctionName.GetPointVisibility:
                    GetPointVisibilityShowInputs(isAsync, listInputs);
                    if (!isError)
                        GetPointVisibilityResults((bool)listResults[0], (DNMcNullableOut<double>)listResults[1], (DNMcNullableOut<double>)listResults[2]);
                    break;
                case AsyncQueryFunctionName.GetLineOfSight:
                    GetPointVisibilityShowInputs(isAsync, listInputs, AsyncQueryFunctionName.GetLineOfSight);
                    if (!isError)
                        GetLineOfSightResults((DNSLineOfSightPoint[])listResults[0], (double)listResults[1], (double)listResults[2]);
                    break;
                case AsyncQueryFunctionName.GetRayIntersection:
                    GetRayIntersectionShowInputs(isAsync, listInputs);
                    if (!isError)
                        GetRayIntersectionResults((bool)listResults[0], (DNSMcVector3D?)listResults[1], (DNSMcVector3D?)listResults[2], (double?)listResults[3]);
                    break;
                case AsyncQueryFunctionName.GetRayIntersectionTargets:
                    GetRayIntersectionShowInputs(isAsync, listInputs);
                    if (!isError)
                        GetRayIntersectionTargetsResult((List<DNSTargetFound>)listResults[0]);
                    break;
                case AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth:
                    OnLocationFromTwoDistancesAndAzimuthShowInputs(isAsync, listInputs);
                    if (!isError)
                        OnLocationFromTwoDistancesAndAzimuthResults((DNSMcVector3D)listResults[0], isAsync);
                    break;
                case AsyncQueryFunctionName.GetAreaOfSight:
                    SetAreaOfSightUserParams(isAsync, listInputs);
                    if (!isError)
                    {
                        int rowTableNumber = (int)dgvAsyncQueryResults.Rows[rowIndex].Cells[0].Value;
                        OnAreaOfSightResults(listResults, listInputs, rowTableNumber);
                    }
                    break;
                case AsyncQueryFunctionName.GetTerrainHeightMatrix:
                    if (!isError)
                        GetTerrainHeightMatrixResults(isAsync, listResults, listInputs);
                    break;
                case AsyncQueryFunctionName.GetAreaOfSightForMultipleScouters:
                    SetAreaOfSightUserParams(isAsync, listInputs);
                    if (!isError)
                    {
                        int rowTableNumber = (int)dgvAsyncQueryResults.Rows[rowIndex].Cells[0].Value;
                        OnAreaOfSightForMultipleScoutersResults(listResults, listInputs, rowTableNumber);
                    }
                    break;
                case AsyncQueryFunctionName.GetBestScoutersLocationsInEllipse:
                    OnBestScoutersLocationsInEllipseShowInputs(isAsync, listInputs);
                    if (!isError)
                    {
                        int rowTableNumber = (int)dgvAsyncQueryResults.Rows[rowIndex].Cells[0].Value;
                        OnBestScoutersLocationsInEllipseResults(listResults/*, listInputs, rowTableNumber*/);
                    }
                    break;
                case AsyncQueryFunctionName.GetTraversabilityAlongLine:
                    GetTraversabilityAlongLineShowInputs(isAsync, (IDNMcTraversabilityMapLayer)listInputs[0], (DNSMcVector3D[])listInputs[1]);
                    if (!isError)
                        GetTraversabilityAlongLineResults(isAsync, (DNSTraversabilityPoint[])listResults[0], (IDNMcTraversabilityMapLayer)listInputs[0], (DNSMcVector3D[])listInputs[1]);
                    break;
                case AsyncQueryFunctionName.GetRasterLayerColorByPoint:
                    GetRasterLayerColorByPointShowInputs(isAsync, listInputs);
                    if (!isError)
                        GetRasterLayerColorByPointResults((DNSMcBColor)listResults[0], (IDNMcRasterMapLayer)listInputs[0]);

                    break;
            }
        }



        private void GetTerrainHeightShowInputs(bool isAsync, DNSMcVector3D TerrainHeightPt)
        {
            ctrl3DTerrainHeightPt.SetVector3D( TerrainHeightPt);
            cbTerrainHeightAsync.Checked = isAsync;
            DrawPictureInPoint(TerrainHeightPt);
        }

        List<IDNMcObject> mListObjects = new List<IDNMcObject>();

        private void GetTerrainHeightsAlongLineError(bool isAsync, DNSMcVector3D[] inputLineError)
        {
            if (m_currActiveOverlay != null)
            {
                IDNMcObjectSchemeItem ObjSchemeItem;
                IDNMcObject obj;
                CreateLine(out obj, out ObjSchemeItem, inputLineError);
                mListObjects.Add(obj);
                cbTerrainHeightAlongLineAsync.Checked = isAsync;
            }
        }

        private void GetExtremeHeightPointsInPolygonError(bool isAsync, DNSMcVector3D[] inputPolygonPointsError)
        {
            IDNMcObjectSchemeItem ObjSchemeItem;
            IDNMcObject obj;
            if (m_currActiveOverlay != null)
            {
                DrawPolygonToExtremeHeightPoints(out ObjSchemeItem, out obj, inputPolygonPointsError);
                mListObjects.Add(obj);
            }
            ctrl3DVectorHighestPt.SetVector3D( new DNSMcVector3D());
            ctrl3DVectorLowestPt.SetVector3D( new DNSMcVector3D());
            chxPointsFound.Checked = false;
            cbTerrainHeightAlongLineAsync.Checked = isAsync;
        }

        private void GetTerrainAnglesResults(double dPitch, double dRoll)
        {
            txtPitch.Text = dPitch.ToString();
            txtRoll.Text = dRoll.ToString();
        }

        private void GetTerrainAnglesShowInputs(bool isAsync, DNSMcVector3D point, double azimuth)
        {
            cbTerrainAnglesAsync.Checked = isAsync;
            ctrl3DVectorTerrainAnglesPt.SetVector3D( point);
            ntxTerrainAnglesAzimuth.SetDouble(azimuth);
        }

        private void cmbLayerTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLayerTypes.SelectedIndex >= 0)
            {
                try
                {
                    if (lstTerrain.SelectedItem != null)
                    {
                        DNELayerKind layerKind = (DNELayerKind)Enum.Parse(typeof(DNELayerKind), cmbLayerTypes.SelectedItem.ToString());

                        ntxNumTiles.SetUInt32(m_currSQ.GetTerrainQueriesNumCacheTiles(TerrainValueList[lstTerrain.SelectedIndex], layerKind));
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetTerrainQueriesNumCacheTiles", McEx);
                }
            }
        }

        private void dgvAsyncQueryResults_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (!dgvAsyncQueryResults.Rows[e.RowIndex].IsNewRow)
                {
                    RemoveObjectList();
                    ClickShowResults(e.RowIndex);
                }
            }
        }

        private void btnTerrainHeightMatrix_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            RemoveObjectList();
            uint numVerticalPoints = ntxNumVerticalPoints.GetUInt32();
            uint numHorizontalPoints = ntxNumHorizontalPoints.GetUInt32();
            double dHorizontalResolution = ntxHorizontalResolution.GetDouble();
            double dVerticalResolution = ntxVerticalResolution.GetDouble();
            List<object> listInputs = new List<object>();
            listInputs.Add(ctrl3DLowerLeftPoint.GetVector3D());
            listInputs.Add(dHorizontalResolution);
            listInputs.Add(dVerticalResolution);
            listInputs.Add(numHorizontalPoints);
            listInputs.Add(numVerticalPoints);

            if (cbTerrainMatrixAsync.Checked)
                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetTerrainHeightMatrix);
            else
                QueryParams.pAsyncQueryCallback = null;
            try
            {
                double[] adHeightMatrix = m_currSQ.GetTerrainHeightMatrix(ConvertPointFromOMtoVP(ctrl3DLowerLeftPoint.GetVector3D()),
                                                 dHorizontalResolution,
                                                 dVerticalResolution,
                                                 numHorizontalPoints,
                                                 numVerticalPoints,
                                                 QueryParams);
                if (!cbTerrainMatrixAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(adHeightMatrix);

                    AddResultToList(AsyncQueryFunctionName.GetTerrainHeightMatrix, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetTerrainHeightMatrix, McEx.ErrorCode, cbTerrainMatrixAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetTerrainHeightMatrix", McEx);
            }
        }

        private void CreateRectangleMatrix(float rectWidth, float rectHeight, DNSMcVector3D[] locationPoints)
        {
            if (m_currActiveOverlay != null)
            {
                try
                {
                    m_SightSchemeItemAOS = DNMcRectangleItem.Create(DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN | DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                            DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                            DNEItemGeometryType._EGT_GEOMETRIC_IN_VIEWPORT,
                                                                                            DNERectangleDefinition._ERD_RECTANGLE_DIAGONAL_POINTS,
                                                                                            rectWidth / 2,
                                                                                            rectHeight / 2,
                                                                                            DNELineStyle._ELS_SOLID,
                                                                                            new DNSMcBColor(0, 0, 0, 255),
                                                                                            2f, null,
                                                                                            new DNSMcFVector2D(), 0f,
                                                                                            DNEFillStyle._EFS_NONE);

                    IDNMcObject rectObject = DNMcObject.Create(m_currActiveOverlay,
                                                        m_SightSchemeItemAOS,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);

                    ((IDNMcRectangleItem)m_SightSchemeItemAOS).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    rectObject.GetScheme().SetName("Tester Rectangle Scheme " + Manager_MCViewports.IndexSchemeAOS.ToString());
                    Manager_MCViewports.IndexSchemeAOS++;
                    mListObjects.Add(rectObject);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Create Item And Object", McEx);
                }
            }
        }

        private void GetTerrainHeightMatrixResults(bool isAsync, List<object> listResults, List<object> listInputs)
        {
            double[] heights = (double[])listResults[0];
            DNSMcVector3D lowerLeftPoint = (DNSMcVector3D)listInputs[0];
            double dHorizontalResolution = (double)listInputs[1];
            double dVerticalResolution = (double)listInputs[2];
            uint uNumHorizontalPoints = (uint)listInputs[3];
            uint uNumVerticalPoints = (uint)listInputs[4];
            cbTerrainMatrixAsync.Checked = isAsync;
            DNSMcVector3D currPoint = lowerLeftPoint;

            for (int i = 0; i < uNumVerticalPoints; i++)
            {
                currPoint.x = lowerLeftPoint.x;
                currPoint.y = lowerLeftPoint.y + i * dVerticalResolution;
                for (int j = 0; j < uNumHorizontalPoints; j++)
                {
                    currPoint.z = heights[(i * uNumHorizontalPoints) + j];
                    currPoint.x = lowerLeftPoint.x + j * dHorizontalResolution;
                    DrawText(ConvertPointFromVPtoOM(currPoint));
                }
            }

            float rectWidth = (float)(dHorizontalResolution * (uNumHorizontalPoints - 1));
            float rectHeight = (float)(dVerticalResolution * (uNumVerticalPoints - 1));
            DNSMcVector3D topRightPoint = new DNSMcVector3D();

            topRightPoint.x = ctrl3DLowerLeftPoint.GetVector3D().x + rectWidth;
            topRightPoint.y = ctrl3DLowerLeftPoint.GetVector3D().y + rectHeight;

            DNSMcVector3D[] rectPoints = new DNSMcVector3D[2];
            rectPoints[0] = ConvertPointFromVPtoOM(ctrl3DLowerLeftPoint.GetVector3D());
            rectPoints[1] = ConvertPointFromVPtoOM(topRightPoint);

           // CreateRectangleMatrix(rectWidth, rectHeight, rectPoints);

        }

        private void DrawText(DNSMcVector3D point)
        {
            if (m_currActiveOverlay != null)
            {
                Font font = new Font(FontFamily.GenericSansSerif, 20);
                DNSMcLogFont logFont = new DNSMcLogFont();
                font.ToLogFont(logFont);
                IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                IDNMcTextItem textItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN /*| DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN*/, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                DefaultFont);

                IDNMcObjectScheme scheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(), textItem, DNEMcPointCoordSystem._EPCS_WORLD, false);
                textItem.SetText(new DNMcVariantString("slop", true), 1, false);
                textItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                DNSMcVector3D[] points = new DNSMcVector3D[1];

                points[0] = point;

                IDNMcObject textObj = DNMcObject.Create(m_currActiveOverlay,
                                                        scheme,
                                                        points);

                textObj.SetStringProperty(1, new DNMcVariantString(Math.Round(point.z, 1).ToString(), true));
                mListObjects.Add(textObj);
            }

        }

        private void DrawTextPointVisibility(DNSMcVector3D point, string text, bool isRelative)
        {
            if (m_currActiveOverlay != null)
            {
                Font font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
                DNSMcLogFont logFont = new DNSMcLogFont();
                font.ToLogFont(logFont);
                IDNMcLogFont DefaultFont = DNMcLogFont.Create(new DNMcVariantLogFont(logFont, false));

                IDNMcTextItem textItem = DNMcTextItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN /*| DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN*/, DNEMcPointCoordSystem._EPCS_SCREEN,
                                                                DefaultFont);

                IDNMcObjectScheme scheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(), textItem, DNEMcPointCoordSystem._EPCS_WORLD, isRelative);
                textItem.SetText(new DNMcVariantString(text, true), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                textItem.SetOutlineColor( DNSMcBColor.bcWhiteOpaque, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                textItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                mListObjects.Add(DNMcObject.Create(m_currActiveOverlay, scheme, new DNSMcVector3D[1] { point }));
            }

        }


        private void btnCalcBestPoints_Click(object sender, EventArgs e)
        {
            RemoveObjectList();
            btnUpdateSightObject_Click(null, null);
            DNSMcVector3D[] scouterPoints = null;
            List<object> listInputs = new List<object>();
            try
            {
                DNSMcVector3D scouterPoint, ellipseCenterPoint;
                if (m_currViewport != null)
                {
                    scouterPoint = m_currViewport.OverlayManagerToViewportWorld(ctrl3DScouterCenterPoint.GetVector3D());
                    ellipseCenterPoint = m_currViewport.OverlayManagerToViewportWorld(ctrl3DVectorAOSScouter.GetVector3D());
                }
                else
                {
                    scouterPoint = ctrl3DScouterCenterPoint.GetVector3D();
                    ellipseCenterPoint = ctrl3DVectorAOSScouter.GetVector3D();
                }

                listInputs.Add(ctrl3DVectorAOSScouter.GetVector3D());
                listInputs.Add(ntxAOSTargetHeight.GetDouble());
                listInputs.Add(chxAOSIsTargetsHeightAbsolute.Checked);
                listInputs.Add(ntxAOSRadiusX.GetFloat());
                listInputs.Add(ntxAOSRadiusY.GetFloat());
                listInputs.Add(ctrl3DScouterCenterPoint.GetVector3D());
                listInputs.Add(ntxScouterRadiusX.GetFloat());
                listInputs.Add(ntxScouterRadiusY.GetFloat());
                listInputs.Add(ntxAOSTargetHeight.GetDouble());
                listInputs.Add(chxAOSIsScouterHeightAbsolute.Checked);
                listInputs.Add(ntxMaxNumOfScouters.GetUInt32());

                if (cbCalcBestPointsAsync.Checked)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetBestScoutersLocationsInEllipse);
                else
                    QueryParams.pAsyncQueryCallback = null;

                scouterPoints = m_currSQ.GetBestScoutersLocationsInEllipse(
                      ellipseCenterPoint,
                      ntxAOSTargetHeight.GetDouble(),
                      chxAOSIsTargetsHeightAbsolute.Checked,
                      ntxAOSRadiusX.GetFloat(),
                      ntxAOSRadiusY.GetFloat(),
                      ctrl3DScouterCenterPoint.GetVector3D(),
                      ntxScouterRadiusX.GetFloat(),
                      ntxScouterRadiusY.GetFloat(),
                      ntxAOSTargetHeight.GetDouble(),
                      chxAOSIsScouterHeightAbsolute.Checked,
                      ntxMaxNumOfScouters.GetUInt32(),
                      QueryParams);

                List<object> listResults = new List<object>();
                listResults.Add(scouterPoints);
                if (!cbCalcBestPointsAsync.Checked)
                {
                    AddResultToList(AsyncQueryFunctionName.GetBestScoutersLocationsInEllipse, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetBestScoutersLocationsInEllipse, McEx.ErrorCode, cbCalcBestPointsAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetBestScoutersLocationsInEllipse", McEx);
            }
        }

        private void OnBestScoutersLocationsInEllipseShowInputs(bool isAsync, List<object> listInputs)
        {
            cbCalcBestPointsAsync.Checked = isAsync;
            ctrl3DVectorAOSScouter.SetVector3D( (DNSMcVector3D)listInputs[0]);                 // 0
            ntxAOSTargetHeight.SetDouble((double)listInputs[1]);
            chxAOSIsTargetsHeightAbsolute.Checked = (bool)listInputs[2];
            ntxAOSRadiusX.SetFloat((float)listInputs[3]);
            ntxAOSRadiusY.SetFloat((float)listInputs[4]);
            ctrl3DScouterCenterPoint.SetVector3D( (DNSMcVector3D)listInputs[5]);               // 5
            ntxScouterRadiusX.SetFloat((float)listInputs[6]);
            ntxScouterRadiusY.SetFloat((float)listInputs[7]);
            ntxAOSTargetHeight.SetDouble((double)listInputs[8]);
            chxAOSIsScouterHeightAbsolute.Checked = (bool)listInputs[9];
            ntxMaxNumOfScouters.SetUInt32((uint)listInputs[10]);

            btnUpdateSightObject_Click(null, null);

            cbIsMultipleScouters.Checked = true;
        }

        private void OnBestScoutersLocationsInEllipseResults(List<object> listResults/*, List<object> listInputs, int rowTableNumber*/)
        {
            DNSMcVector3D[] scouterPoints = (DNSMcVector3D[])listResults[0];

            ctrlPointsGridMultipleScouters.SetPoints(scouterPoints);

            if (scouterPoints != null)
            {
                for (int i = 0; i < scouterPoints.Length; i++)
                {
                    DrawPictureInPoint(scouterPoints[i]);
                }
            }
        }

        private void cbIsMultipleScouters_CheckedChanged(object sender, EventArgs e)
        {
            bool isMultipleScoutersChecked = cbIsMultipleScouters.Checked;
            if (isMultipleScoutersChecked &&
                (m_SightObject == null || ((m_SightSchemeItemAOS != null) && (!(m_SightSchemeItemAOS is IDNMcEllipseItem))) || !rgbSightObjectEllipse.Checked) &&
               !rgbSightObjectEllipse.Checked &&
               !m_IsSetAreaOfSightUserParams)
            {
                MessageBox.Show("Multiple Scouter Available In Ellipse Object Only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbIsMultipleScouters.Checked = false;
                return;
            }
            gbMultipleScouters.Enabled = isMultipleScoutersChecked;

           /* cmbAOSColors.Enabled = !isMultipleScoutersChecked;
            numUDAlphaColor.Enabled = !isMultipleScoutersChecked;*/
            gbAOSColors.Enabled = !isMultipleScoutersChecked;
           

            label12.Enabled = !isMultipleScoutersChecked;
            label13.Enabled = !isMultipleScoutersChecked;
            label20.Enabled = !isMultipleScoutersChecked;
            label21.Enabled = !isMultipleScoutersChecked;
            //label25.Enabled = !isMultipleScoutersChecked;
            label14.Enabled = !isMultipleScoutersChecked;
            label16.Enabled = !isMultipleScoutersChecked;
            label18.Enabled = !isMultipleScoutersChecked;
            label17.Enabled = !isMultipleScoutersChecked;
            //label30.Enabled = !isMultipleScoutersChecked;

            chxCalcLineOfSight.Enabled = chxCalcLineOfSight.Checked = !isMultipleScoutersChecked;
            chxCalcAreaOfSight.Enabled = chxCalcAreaOfSight.Checked = !isMultipleScoutersChecked;
            chxCalcSeenPolygons.Enabled = chxCalcSeenPolygons.Checked = !isMultipleScoutersChecked;
            chxCalcUnseenPolygons.Enabled = chxCalcUnseenPolygons.Checked = !isMultipleScoutersChecked;
            chxCalcStaticObjects.Enabled = chxCalcStaticObjects.Checked = !isMultipleScoutersChecked;
        }

        private void btnGetPointVisibilityColorsSurrounding_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (SelectedAreaOfSight != null)
            {
                UInt32[] pointVisibilityColors = null;
                uint SurroundingRadiusX = ntxSurroundingRadiusX.GetUInt32();
                uint SurroundingRadiusY = ntxSurroundingRadiusY.GetUInt32();
                try
                {
                    DNSMcVector3D surroundingPoint;
                    if (m_currViewport != null)
                        surroundingPoint = m_currViewport.OverlayManagerToViewportWorld(ctrl3DSurroundingPoint.GetVector3D());
                    else
                        surroundingPoint = ctrl3DSurroundingPoint.GetVector3D();

                    pointVisibilityColors = SelectedAreaOfSight.GetPointVisibilityColorsSurrounding(
                        surroundingPoint, SurroundingRadiusX, SurroundingRadiusY);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetPointVisibilityColorsSurrounding", McEx);
                }

                if (pointVisibilityColors != null && pointVisibilityColors.Length > 0)
                {
                    dgvSurroundingResults.Rows.Clear();
                    dgvSurroundingResults.Columns.Clear();

                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = "No.";
                    col.ReadOnly = true;
                    col.Width = 40;
                    dgvSurroundingResults.Columns.Add(col);

                    for (int c = 0; c < SurroundingRadiusX; c++)
                    {
                        col = new DataGridViewTextBoxColumn();
                        col.HeaderText = (c + 1).ToString();
                        col.ReadOnly = true;
                        dgvSurroundingResults.Columns.Add(col);
                    }
                    int rowIndex = 0;
                    for (int i = 0; i < SurroundingRadiusY; i++)
                    {
                        dgvSurroundingResults.Rows.Add();
                        for (int j = 0; j < SurroundingRadiusX; j++)
                        {
                            int index = i * (int)SurroundingRadiusX + j;
                            string strPointVisibilityColor = pointVisibilityColors[index].ToString();
                            if (cbHexadecimelNumber.Checked)
                                strPointVisibilityColor = pointVisibilityColors[index].ToString("X");
                            dgvSurroundingResults.Rows[rowIndex].Cells[j + 1].Value = strPointVisibilityColor;
                        }
                        dgvSurroundingResults.Rows[rowIndex].Cells[0].Value = rowIndex + 1;
                        rowIndex++;
                    }
                }
            }
        }

        private List<MCTSAreaOfSightMatrixData> GetCheckedSAreaOfSightMatrixList()
        {
            List<MCTSAreaOfSightMatrixData> checkedSAreaOfSightMatrixList = new List<MCTSAreaOfSightMatrixData>();
            for (int i = 0; i < cbLstAreaOfSightMatrix.Items.Count; i++)
            {
                if (cbLstAreaOfSightMatrix.GetItemChecked(i))
                    checkedSAreaOfSightMatrixList.Add((MCTSAreaOfSightMatrixData)cbLstAreaOfSightMatrix.Items[i]);
            }

            return checkedSAreaOfSightMatrixList;
        }

        private void btnCloneAreaOfSightMatrix_Click(object sender, EventArgs e)
        {
            try
            {
                List<MCTSAreaOfSightMatrixData> checkedSAreaOfSightMatrixList = GetCheckedSAreaOfSightMatrixList();
                if (checkedSAreaOfSightMatrixList.Count != 1)
                {
                    MessageBox.Show("Need checked 1 item from list", "AreSameRectAreaOfSightMatrices", MessageBoxButtons.OK);
                    return;
                }
                MCTSAreaOfSightMatrixData selectedAreaOfSightMatrixData = checkedSAreaOfSightMatrixList[0];
                if (selectedAreaOfSightMatrixData != null)
                {
                    DNSAreaOfSightMatrix cloneMatrix = DNMcSpatialQueries.CloneAreaOfSightMatrix(selectedAreaOfSightMatrixData.areaOfSightMatrix, cbCloneMatrixFillPoints.Checked);
                    MCTSAreaOfSightMatrixData areaOfSightMatrixData = new MCTSAreaOfSightMatrixData();
                    areaOfSightMatrixData.areaOfSight = selectedAreaOfSightMatrixData.areaOfSight;
                    areaOfSightMatrixData.areaOfSightMatrix = cloneMatrix;
                    areaOfSightMatrixData.isClone = true;
                    areaOfSightMatrixData.nameToShow = "Clone of " + selectedAreaOfSightMatrixData.nameToShow;
                    m_lstSAreaOfSightMatrix.Add(areaOfSightMatrixData);
                    CreateSAreaOfSightMatrixList();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("CloneAreaOfSightMatrix", McEx);
            }
        }

        private void btnSumAreaOfSightMatrices_Click(object sender, EventArgs e)
        {
            List<MCTSAreaOfSightMatrixData> checkedSAreaOfSightMatrixList = GetCheckedSAreaOfSightMatrixList();
            if (checkedSAreaOfSightMatrixList.Count != 2)
            {
                MessageBox.Show("Need checked 2 items from list", "AreSameRectAreaOfSightMatrices", MessageBoxButtons.OK);
                return;
            }
            try
            {
                MCTSAreaOfSightMatrixData selectedAreaOfSightMatrixData1 = checkedSAreaOfSightMatrixList[0];
                MCTSAreaOfSightMatrixData selectedAreaOfSightMatrixData2 = checkedSAreaOfSightMatrixList[1];
                if (selectedAreaOfSightMatrixData1 != null && selectedAreaOfSightMatrixData2 != null)
                {
                    DNEScoutersSumType areaOfSightMatricesScoutersSumType = (DNEScoutersSumType)Enum.Parse(typeof(DNEScoutersSumType), cmbEScoutersSumTypeMatrices.SelectedItem.ToString());

                    DNMcSpatialQueries.SumAreaOfSightMatrices(ref selectedAreaOfSightMatrixData1.areaOfSightMatrix, selectedAreaOfSightMatrixData2.areaOfSightMatrix, areaOfSightMatricesScoutersSumType);
                    MCTSAreaOfSightMatrixData areaOfSightMatrixData = new MCTSAreaOfSightMatrixData();
                    areaOfSightMatrixData.areaOfSight = selectedAreaOfSightMatrixData1.areaOfSight;
                    areaOfSightMatrixData.areaOfSightMatrix = selectedAreaOfSightMatrixData1.areaOfSightMatrix;
                    areaOfSightMatrixData.isSum = true;
                    areaOfSightMatrixData.nameToShow = "Sum of " + selectedAreaOfSightMatrixData1.nameToShow + " And " + selectedAreaOfSightMatrixData2.nameToShow;
                    m_lstSAreaOfSightMatrix.Add(areaOfSightMatrixData);
                    CreateSAreaOfSightMatrixList();
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("SumAreaOfSightMatrices", McEx);
            }
        }

        private void btnAreSameRectAreaOfSightMatrices_Click(object sender, EventArgs e)
        {
            List<MCTSAreaOfSightMatrixData> checkedSAreaOfSightMatrixList = GetCheckedSAreaOfSightMatrixList();
            if (checkedSAreaOfSightMatrixList.Count != 2)
            {
                MessageBox.Show("Need checked 2 items from list", "AreSameRectAreaOfSightMatrices", MessageBoxButtons.OK);
                return;
            }
            try
            {
                MCTSAreaOfSightMatrixData selectedAreaOfSightMatrixData1 = checkedSAreaOfSightMatrixList[0];
                MCTSAreaOfSightMatrixData selectedAreaOfSightMatrixData2 = checkedSAreaOfSightMatrixList[1];
                if (selectedAreaOfSightMatrixData1 != null && selectedAreaOfSightMatrixData2 != null)
                {
                    cbAreSameRect.Checked = DNMcSpatialQueries.AreSameRectAreaOfSightMatrices(selectedAreaOfSightMatrixData1.areaOfSightMatrix, selectedAreaOfSightMatrixData2.areaOfSightMatrix);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("AreSameRectAreaOfSightMatrices", McEx);
            }
        }

        private void btnExtremeHeightPointsInPolygon_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            List<object> listInput = new List<object>();
            try
            {
                DNSMcVector3D[] points = null;
                if (ctrlExtremeHeightPoints.GetPoints(out points))
                {
                    if (m_IsStandaloneSQ)
                    {
                        ExtremeHeightPointsInPolygon(points, points);
                    }
                    else //if (m_HPReturnObject != null)
                    {
                        listInput.Add(points);

                        DNSMcVector3D[] sqPoints = new DNSMcVector3D[points.Length];
                        for (int i = 0; i < points.Length; i++)
                        {
                            sqPoints[i] = ConvertPointFromOMtoVP(points[i]);
                        }

                        ExtremeHeightPointsInPolygon(points, sqPoints);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetExtremeHeightPointsInPolygon", McEx);
            }
        }

        private void ExtremeHeightPointsInPolygon(DNSMcVector3D[] points, DNSMcVector3D[] convertedPoints)
        {
            List<object> listInput = new List<object>();
            try
            {
                bool bPointsFound = false;
                DNSMcVector3D HighestPt, LowestPt;
                listInput.Add(points);
                if (cbExtremeHeightPointsInPolygonAsync.Checked)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInput, AsyncQueryFunctionName.GetExtremeHeightPointsInPolygon);
                else
                    QueryParams.pAsyncQueryCallback = null;

                m_currSQ.GetExtremeHeightPointsInPolygon(convertedPoints,
                                                            out bPointsFound,
                                                            out HighestPt,
                                                            out LowestPt,
                                                            QueryParams);

                if (!cbExtremeHeightPointsInPolygonAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(bPointsFound);
                    listResults.Add(HighestPt);
                    listResults.Add(LowestPt);

                    AddResultToList(AsyncQueryFunctionName.GetExtremeHeightPointsInPolygon, listResults, false, listInput);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetExtremeHeightPointsInPolygon", McEx);
                AddErrorToList(AsyncQueryFunctionName.GetExtremeHeightPointsInPolygon, McEx.ErrorCode, cbExtremeHeightPointsInPolygonAsync.Checked, listInput);
            }
        }

        private void btnDrawLineTraversability_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();
            if (m_currActiveOverlay != null)
            {
                RemoveObject(m_TALReturnObject);
                m_TALReturnObject = null;

                RemoveObjectList();
                this.Hide();
                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResultsGetTraversabilityAlongLine);
                m_ExitStatus = 0;

                try
                {
                    IDNMcObjectSchemeItem ObjSchemeItem;
                    IDNMcObject obj;
                    CreateLine(out obj, out ObjSchemeItem);

                    m_EditMode.StartInitObject(obj, ObjSchemeItem);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("StartInitObject", McEx);
                    this.Show();
                }
            }
        }

        public void InitItemResultsGetTraversabilityAlongLine(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            m_TALReturnObject = pObject;
            m_TALReturnItem = pItem;

            SetPointsToGrid(m_TALReturnObject, ctrlTraversabilityAlongLinePoints);

            MCTMapFormManager.MapForm.EditModeManagerCallback.UnregisterEventsCallback(null);
            MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResultsGetTraversabilityAlongLine);

            this.Show();
        }

        private void btnGetTraversabilityAlongLine_Click(object sender, EventArgs e)
        {
            //DNSMcVector3D[] aTargetPoints = GetPointsFromGrid(dgvAOSTargetPoints, false);
            IDNMcTraversabilityMapLayer traversabilityMapLayer = null;

            if (lbTraversabilityMapLayers.Items.Count > 0 && lbTraversabilityMapLayers.SelectedIndex >= 0)
            {
                traversabilityMapLayer = m_lstTraversabilityMapLayers[lbTraversabilityMapLayers.SelectedIndex];
            }
            else
            {
                MessageBox.Show("Missing Traversability Map Layer");
                return;
            }
            /*if (m_TALReturnObject == null)
            {
                MessageBox.Show("Missing Traversability Object");
                return;
            }*/
            List<object> listInputs = new List<object>();
            DNSMcVector3D[] points = null;
            if (ctrlTraversabilityAlongLinePoints.GetPoints(out points))
            {
                listInputs.Add(traversabilityMapLayer);
                listInputs.Add(points);
                if (cbGetTraversabilityAlongLineAsync.Checked)
                    QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetTraversabilityAlongLine);
                else
                    QueryParams.pAsyncQueryCallback = null;

                try
                {
                    DNSMcVector3D[] sqPoints = new DNSMcVector3D[points.Length];
                    for (int i = 0; i < points.Length; i++)
                    {
                        sqPoints[i] = ConvertPointFromOMtoVP(points[i]);
                    }

                    DNSTraversabilityPoint[] traversabilityPoints = m_currSQ.GetTraversabilityAlongLine(traversabilityMapLayer, sqPoints, QueryParams);
                    if (!cbGetTraversabilityAlongLineAsync.Checked)
                    {
                        List<object> listResults = new List<object>();
                        listResults.Add(traversabilityPoints);

                        AddResultToList(AsyncQueryFunctionName.GetTraversabilityAlongLine, listResults, false, listInputs);
                    }
                }
                catch (MapCoreException McEx)
                {
                    AddErrorToList(AsyncQueryFunctionName.GetTraversabilityAlongLine, McEx.ErrorCode, cbGetTraversabilityAlongLineAsync.Checked, listInputs);
                    Utilities.ShowErrorMessage("GetTraversabilityAlongLine", McEx);
                }
            }
        }

        private void GetTraversabilityAlongLineShowInputs(bool isAsync, IDNMcTraversabilityMapLayer traversabilityMapLayer, DNSMcVector3D[] plocationPoints)
        {
            cbGetTraversabilityAlongLineAsync.Checked = isAsync;
            if (m_currActiveOverlay != null)
            {
                IDNMcObjectSchemeItem ObjSchemeItem;
                IDNMcObject obj;
                CreateLine(out obj, out ObjSchemeItem, plocationPoints);
                mListObjects.Add(obj);
            }
            if (traversabilityMapLayer != null && m_lstTraversabilityMapLayers.Contains(traversabilityMapLayer))
                lbTraversabilityMapLayers.SelectedIndex = m_lstTraversabilityMapLayers.IndexOf(traversabilityMapLayer);
        }

        private void GetTraversabilityAlongLineResults(bool isAsync, DNSTraversabilityPoint[] traversabilityPoints, IDNMcTraversabilityMapLayer traversabilityMapLayer, DNSMcVector3D[] plocationPoints)
        {
            if (traversabilityPoints != null && traversabilityPoints.Length > 0)
            {
                if (m_currActiveOverlay != null)
                {
                    IDNMcObjectSchemeItem visibleLineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    new DNSMcBColor(0, 255, 0, 255),
                                                                                    10);

                    IDNMcObjectSchemeItem nonVisibleLineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    new DNSMcBColor(255, 0, 0, 255),
                                                                                    10);

                    IDNMcObjectSchemeItem unknownLineItem = DNMcLineItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                                      DNELineStyle._ELS_SOLID,
                                                                                                      DNSMcBColor.bcWhiteOpaque,
                                                                                                      10);

                    IDNMcObjectScheme visibleScheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                    visibleLineItem,
                                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                    false);

                    IDNMcObjectScheme nonVisibleScheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                    nonVisibleLineItem,
                                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                    false);

                    IDNMcObjectScheme unknownScheme = DNMcObjectScheme.Create(m_currActiveOverlay.GetOverlayManager(),
                                                                                    unknownLineItem,
                                                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                                                    false);

                    ((IDNMcLineItem)visibleLineItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);
                    ((IDNMcLineItem)nonVisibleLineItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, 0, false);
                    ((IDNMcLineItem)visibleLineItem).SetOutlineWidth(2f, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)visibleLineItem).SetOutlineColor(DNSMcBColor.bcBlackOpaque, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)nonVisibleLineItem).SetOutlineWidth(2f, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)nonVisibleLineItem).SetOutlineColor(DNSMcBColor.bcBlackOpaque, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)unknownLineItem).SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)unknownLineItem).SetOutlineWidth(2f, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                    ((IDNMcLineItem)unknownLineItem).SetOutlineColor(DNSMcBColor.bcBlackOpaque, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                    DNSMcVector3D[] locationPt = new DNSMcVector3D[2];

                    for (int i = 1; i < traversabilityPoints.Length; i++)
                    {
                        locationPt[0] = ConvertPointFromVPtoOM(traversabilityPoints[i - 1].Point);
                        locationPt[1] = ConvertPointFromVPtoOM(traversabilityPoints[i].Point);
                        IDNMcObject traversabilityObj = null;
                        if (traversabilityPoints[i - 1].eTraversability == DNEPointTraversability._EPT_TRAVERSABLE)
                        {
                            traversabilityObj = DNMcObject.Create(m_currActiveOverlay,
                                                                visibleScheme,
                                                                locationPt);
                        }
                        else if (traversabilityPoints[i - 1].eTraversability == DNEPointTraversability._EPT_UNTRAVERSABLE)
                        {
                            traversabilityObj = DNMcObject.Create(m_currActiveOverlay,
                                                                nonVisibleScheme,
                                                                locationPt);
                        }
                        else if (traversabilityPoints[i - 1].eTraversability == DNEPointTraversability._EPT_UNKNOWN)
                        {
                            traversabilityObj = DNMcObject.Create(m_currActiveOverlay,
                                                                unknownScheme,
                                                                locationPt);
                        }
                        mListObjects.Add(traversabilityObj);
                    }
                }
            }
        }

        private void UpdateTraversabilityCounterLabel()
        {
            lblTargetCounter.Text = "<" + m_IdxTargetFound.ToString() + "/" + m_lstTraversabilityPoint.Count.ToString() + ">";
        }

        private void btnGetRasterLayerColorByPoint_Click(object sender, EventArgs e)
        {
            IDNMcRasterMapLayer rasterMapLayer = null;

            if (lbRasterMapLayer.Items.Count > 0 && lbRasterMapLayer.SelectedIndex >= 0)
            {
                rasterMapLayer = m_lstRasterMapLayers[lbRasterMapLayer.SelectedIndex];
            }
            else
            {
                MessageBox.Show("Missing Raster Map Layer");
                return;
            }

            List<object> listInputs = new List<object>();
            listInputs.Add(rasterMapLayer);
            listInputs.Add(ctrl3DGetRasterPoint.GetVector3D());
            listInputs.Add(ntbLOD.GetInt32());
            listInputs.Add(cbNearestPixel.Checked);
            if (cbGetRasterAsync.Checked)
                QueryParams.pAsyncQueryCallback = new AsyncQueryResultsCallback(this, listInputs, AsyncQueryFunctionName.GetRasterLayerColorByPoint);
            else
                QueryParams.pAsyncQueryCallback = null;

            try
            {
                DNSMcBColor color = m_currSQ.GetRasterLayerColorByPoint(rasterMapLayer,
                    ctrl3DGetRasterPoint.GetVector3D(),
                    ntbLOD.GetInt32(),
                    cbNearestPixel.Checked,
                    QueryParams);

                if (!cbGetRasterAsync.Checked)
                {
                    List<object> listResults = new List<object>();
                    listResults.Add(color);

                    AddResultToList(AsyncQueryFunctionName.GetRasterLayerColorByPoint, listResults, false, listInputs);
                }
            }
            catch (MapCoreException McEx)
            {
                AddErrorToList(AsyncQueryFunctionName.GetRasterLayerColorByPoint, McEx.ErrorCode, cbGetRasterAsync.Checked, listInputs);
                Utilities.ShowErrorMessage("GetRasterLayerColorByPoint", McEx);
            }
        }

        private void GetRasterLayerColorByPointShowInputs(bool isAsync, List<Object> listInputs)
        {
            IDNMcRasterMapLayer rasterMapLayer = (IDNMcRasterMapLayer)listInputs[0];
            lbRasterMapLayer.SelectedIndex = m_lstRasterMapLayers.IndexOf(rasterMapLayer);
            ctrl3DGetRasterPoint.SetVector3D( (DNSMcVector3D)listInputs[1]);
            ntbLOD.SetInt((int)listInputs[2]);
            cbNearestPixel.Checked = (bool)listInputs[3];
            dgvGetTraversabilityFromColorCode.Rows.Clear();
            gbGetTraversabilityFromColorCode.Enabled = (rasterMapLayer is IDNMcTraversabilityMapLayer);
        }

        private void GetRasterLayerColorByPointResults(DNSMcBColor color, IDNMcRasterMapLayer rasterMapLayer)
        {
            pbGetRasterColor.BackColor = Color.FromArgb(color.a, color.r, color.g, color.b);
            ntbGetRasterColorR.SetByte(color.r);
            ntbGetRasterColorG.SetByte(color.g);
            ntbGetRasterColorB.SetByte(color.b);
            ntbGetRasterColorA.SetByte(color.a);
            try
            {
                if (rasterMapLayer is IDNMcTraversabilityMapLayer)
                {
                    GetTraversabilityFromColorCode(color, rasterMapLayer);
                }
                /*IDNMcTraversabilityMapLayer traversabilityMapLayer = ;
                DNSTraversabilityDirection[] traversabilityDirections = traversabilityMapLayer.GetTraversabilityFromColorCode(color);

                foreach(DNSTraversabilityDirection traversabilityDirection in traversabilityDirections)
                {
                    dgvGetTraversabilityFromColorCode.Rows.Add(traversabilityDirection.fDirectionAngle, traversabilityDirection.bTraversable);
                }*/
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTraversabilityFromColorCode", McEx);
            }
        }

        private void btnGetTraversabilityFromColorCode_Click(object sender, EventArgs e)
        {
            IDNMcRasterMapLayer rasterMapLayer = null;

            if (lbRasterMapLayer.Items.Count > 0 && lbRasterMapLayer.SelectedIndex >= 0)
            {
                rasterMapLayer = m_lstRasterMapLayers[lbRasterMapLayer.SelectedIndex];
            }
            else
            {
                MessageBox.Show("Missing Raster Map Layer");
                return;
            }

            DNSMcBColor color = new DNSMcBColor(ntbGetRasterColorR.GetByte(), ntbGetRasterColorG.GetByte(), ntbGetRasterColorB.GetByte(), ntbGetRasterColorA.GetByte());
            pbGetRasterColor.BackColor = Color.FromArgb(color.a, color.r, color.g, color.b);
            GetTraversabilityFromColorCode(color, rasterMapLayer);
        }

        private void GetTraversabilityFromColorCode(DNSMcBColor color, IDNMcRasterMapLayer rasterMapLayer)
        {
            try
            {
                dgvGetTraversabilityFromColorCode.Rows.Clear();
                if (rasterMapLayer is IDNMcTraversabilityMapLayer)
                {
                    IDNMcTraversabilityMapLayer traversabilityMapLayer = (IDNMcTraversabilityMapLayer)rasterMapLayer;

                    gbGetTraversabilityFromColorCode.Enabled = true;

                    DNSTraversabilityDirection[] traversabilityDirections = traversabilityMapLayer.GetTraversabilityFromColorCode(color);

                    if (traversabilityDirections != null)
                    {
                        foreach (DNSTraversabilityDirection traversabilityDirection in traversabilityDirections)
                        {
                            dgvGetTraversabilityFromColorCode.Rows.Add(traversabilityDirection.fDirectionAngle, traversabilityDirection.bTraversable);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Get Traversability From Color Code return null or color is null");
                    }
                }
                else
                {
                    MessageBox.Show("The Map Layer Should Be Traversability Map Layer");
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTraversabilityFromColorCode", McEx);
            }
        }

        private void btnAOSSave_Click(object sender, EventArgs e)
        {
            if (SelectedAreaOfSight != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        IDNAreaOfSight selectedAreaOfSight = (IDNAreaOfSight)SelectedAreaOfSight;
                        SelectedAreaOfSight.Save(sfd.FileName);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("AreaOfSight.Save", McEx);
                    }
                }
            }
        }

        private string SaveAreaOfSight()
        {
            if (SelectedAreaOfSight != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        IDNAreaOfSight selectedAreaOfSight = SelectedAreaOfSight;
                        SelectedAreaOfSight.Save(sfd.FileName);
                    }
                    catch (MapCoreException McEx)
                    {
                        Utilities.ShowErrorMessage("AreaOfSight.Save", McEx);
                    }
                    return sfd.FileName;
                }
            }
            return "";
        }

        private void btnAOSLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SelectedAreaOfSight = DNAreaOfSight.Load(ofd.FileName);
                    
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("AreaOfSight.Load", McEx);
                }
            }
        }

        private IDNAreaOfSight LoadAreaOfSight()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    return DNAreaOfSight.Load(ofd.FileName);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("AreaOfSight.Load", McEx);
                }
            }
            return null;
        }

        private void btnTestSaveAndLoad_Click(object sender, EventArgs e)
        {

            bool isSame = false;
            try
            {
                string fileName = SaveAreaOfSight();
                if (fileName != "")
                {
                    IDNAreaOfSight areaOfSight = DNAreaOfSight.Load(fileName);
                    MCTSAreaOfSightMatrixData mctAreaOfSightMatrix = IsListMatrixContains(areaOfSight);
                    DNSAreaOfSightMatrix sAreaOfSightMatrix = new DNSAreaOfSightMatrix();
                    if (mctAreaOfSightMatrix != null)
                        sAreaOfSightMatrix = mctAreaOfSightMatrix.areaOfSightMatrix;
                    else
                        sAreaOfSightMatrix = areaOfSight.GetAreaOfSightMatrix(chxFillPointsVisibility.Checked);

                    if (DNMcSpatialQueries.AreSameRectAreaOfSightMatrices(m_currSAreaOfSightMatrix, sAreaOfSightMatrix))
                    {
                        if (m_currSAreaOfSightMatrix.aPointsVisibilityColors.Length == sAreaOfSightMatrix.aPointsVisibilityColors.Length)
                        {
                            isSame = true;
                            int len = m_currSAreaOfSightMatrix.aPointsVisibilityColors.Length;
                            for (int i = 0; i < len; i++)
                            {
                                if (m_currSAreaOfSightMatrix.aPointsVisibilityColors[i] != sAreaOfSightMatrix.aPointsVisibilityColors[i])
                                {
                                    isSame = false;
                                    break;
                                }
                            }
                        }
                    }
                    chxAreSameMatrices.Checked = isSame;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetAreaOfSightMatrix", McEx);
            }
        }

        private void btnAOSImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    m_currDrawObjectParams = new MCTAreaOfSightDrawObjectParams();
                    if (MCTPackageBase.Load(ofd.FileName, out m_currDrawObjectParams, typeof(MCTAreaOfSightDrawObjectParams)))
                    {
                        RemoveObjectList();

                        SetAreaOfSightDrawObjectParams();
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Import from file failed: " + ex.Message,
                                        "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAOSExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    btnUpdateSightObject_Click(null, null);
                    m_currDrawObjectParams.Save(sfd.FileName);
                }
                catch (Exception McEx)
                {
                    if (McEx is MapCoreException)
                        Utilities.ShowErrorMessage("Save To File - export was failed", McEx);
                    else
                        MessageBox.Show("Error open/write to file: " + McEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTALUpdateLine_Click(object sender, EventArgs e)
        {
            CheckIsChangeCurrentViewport();

            m_isTALUpdateLine = true;
            CreateTALLine();
            m_isTALUpdateLine = false;
        }

        private void UpdateTALPoints()
        {
            btnTALUpdateLine_Click(null, null);
        }

        private void CreateTALLine()
        {
            DNSMcVector3D[] locationPoints = new DNSMcVector3D[0];
            if (m_isTALUpdateLine && ctrlTraversabilityAlongLinePoints.GetPoints(out locationPoints))
            {
                CreateTALLine(locationPoints);
            }
        }

        private void CreateTALLine(DNSMcVector3D[] locationPoints)
        {
            if (m_currActiveOverlay != null)
            {
                RemoveObject(m_TALReturnObject);
                m_TALReturnObject = null;
                RemoveObjectList();

                try
                {
                    IDNMcObjectSchemeItem ObjSchemeItem;
                    IDNMcObject obj;
                    CreateLine(out obj, out ObjSchemeItem, locationPoints);
                    m_TALReturnObject = obj;
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("CreateLine", McEx);
                    this.Show();
                }
            }
        }

        public void NoticeLayerRemoved(IDNMcMapLayer layerToRemove)
        {
            if (layerToRemove is IDNMcStaticObjectsMapLayer)
            {
                if (m_currCalcParams != null && m_currCalcParams.LstStaticObjectsColor != null)
                {
                    List<StaticObjectsColor> staticObjectsColors = m_currCalcParams.LstStaticObjectsColor.FindAll(x => x.layer == layerToRemove);
                    foreach (StaticObjectsColor staticObjectsColor in staticObjectsColors)
                    {
                        foreach (IDNMcObject obj in staticObjectsColor.PolygonContours)
                        {
                            obj.Remove();
                        }
                        staticObjectsColor.PolygonContours.Clear();
                    }
                    m_currCalcParams.LstStaticObjectsColor = m_currCalcParams.LstStaticObjectsColor.FindAll(x => x.layer != layerToRemove);
                }
                for (int i = 0; i < dgvAsyncQueryResults.Rows.Count; i++)
                {
                    if (dgvAsyncQueryResults.Rows[i].Cells[1].Value != null)
                    {
                        string functionName = dgvAsyncQueryResults.Rows[i].Cells[1].Value.ToString();
                        AsyncQueryFunctionName asyncQueryFunctionName = (AsyncQueryFunctionName)Enum.Parse(typeof(AsyncQueryFunctionName), functionName);
                        if (asyncQueryFunctionName == AsyncQueryFunctionName.GetAreaOfSight)
                        {
                            List<Object> listResults = (List<Object>)dgvAsyncQueryResults.Rows[i].Tag;
                            if (listResults != null && listResults.Count > 3 && listResults[4] != null)
                            {
                                DNSStaticObjectsIDs[] aSeenStaticObjects = (DNSStaticObjectsIDs[])listResults[4];
                                List<DNSStaticObjectsIDs> newSeenStaticObjects = aSeenStaticObjects.ToList().FindAll(x => x.pMapLayer != layerToRemove);
                                (((List<Object>)dgvAsyncQueryResults.Rows[i].Tag)[4]) = newSeenStaticObjects.ToArray();
                            }
                        }
                    }
                 }
            }
        }

        private void CancelAllCallbacks()
        {
            foreach (AsyncQueryResultsCallback callback in AsyncQueryResultsCallback.AsyncQueryResultsCallbacks)
            {
                try
                {
                    m_currSQ.CancelAsyncQuery(callback);

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("CancelAsyncQuery", McEx);
                    this.Show();
                }
            }
            AsyncQueryResultsCallback.AsyncQueryResultsCallbacks.Clear();
        }

        private void btnCancelAsyncQuery_Click(object sender, EventArgs e)
        {
            CancelAllCallbacks();
        }

        private void tcSpatialQueries_Selected(object sender, TabControlEventArgs e)
        {
            if(e.TabPage == tpGeneral)
            {
                CheckMultiThreadEnabled();
            }
        }

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (m_currCalcParams == null || m_currCalcParams.AreaOfSight == null)
                e.Cancel = true;
        }

        private void btnSaveMatrixToFile_Click(object sender, EventArgs e)
        {
            if (SelectedAreaOfSight != null)
            {
                SaveMatrixToFile();
            }
        }

        private void chxLOSIsScouterHeightAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            if(chxLOSIsScouterHeightAbsolute.Checked)
                ctrlSamplePointLOSScouter._PointZValue = double.MaxValue; 
            else
                ctrlSamplePointLOSScouter._PointZValue = 1.7;
        }

        private void chxLOSIsTargetHeightAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            if (chxLOSIsTargetHeightAbsolute.Checked)
                ctrlSamplePointLOSTarget._PointZValue = double.MaxValue;
            else
                ctrlSamplePointLOSTarget._PointZValue = 1.7;
        }

        private void chxMinimalScouterHeight_CheckedChanged(object sender, EventArgs e)
        {
            label29.ForeColor = chxMinimalScouterHeight.Checked ? Color.Black : Color.Gray;
        }

        private void chxMinimalTargetHeight_CheckedChanged(object sender, EventArgs e)
        {
            label28.ForeColor = chxMinimalTargetHeight.Checked ? Color.Black : Color.Gray;

        }

        private void SpatialQueriesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(m_IsStandaloneSQ)
            {
                try
                {
                    if(m_lstTerrainValue != null)
                        m_lstTerrainValue.Clear();
                    m_StandaloneMapTerrains = null;
                    IDNMcSpatialQueries tempSQ = m_currSQ;
                    m_currSQ = null;
                    tempSQ.Dispose();
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("IDNMcSpatialQueries.Dispose", McEx);
                }
                MainForm.CollectGC();
            }
        }

        private void ctrlPointsGridMultipleScouters_Load(object sender, EventArgs e)
        {

        }

        private void rgbSightObjectEllipse_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_rbSightObject && rgbSightObjectEllipse.Checked)
            {
                m_rbSightObject = true;
                rbSightObjectPolygon.Checked = false;
                m_rbSightObject = false;
            }
            
        }

        private void rgbSightObjectRect_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_rbSightObject && rgbSightObjectRect.Checked)
            {
                m_rbSightObject = true;
                rbSightObjectPolygon.Checked = false;
                m_rbSightObject = false;
            }
            if (!rgbSightObjectEllipse.Checked)
                cbIsMultipleScouters.Checked = false;
        }

        private void rbSightObjectPolygon_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_rbSightObject && rbSightObjectPolygon.Checked)
            {
                m_rbSightObject = true;
                rgbSightObjectEllipse.Checked = rgbSightObjectRect.Checked = false;
                m_rbSightObject = false;
            }
            if (!rgbSightObjectEllipse.Checked)
                cbIsMultipleScouters.Checked = false;
        }

        private void lstQuerySecondaryDtmLayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           
        }

        private void btnShowSelectedDTMLayerData_Click(object sender, EventArgs e)
        {
            if (lstQuerySecondaryDtmLayers.SelectedItem != null && lstQuerySecondaryDtmLayers.SelectedIndex < m_QuerySecondaryDtmLayers.Length)
            {
                IDNMcMapLayer layer = m_QuerySecondaryDtmLayers[lstQuerySecondaryDtmLayers.SelectedIndex];
                if (layer != null)
                {
                    frmMapLayers frmMapLayers = new frmMapLayers();
                    frmMapLayers.SetLayer(layer);
                    frmMapLayers.Show();
                }
            }
        }
    }

    public class AsyncQueryResultsCallback : IDNAsyncQueryCallback
    {
        SpatialQueriesForm mSpatialQueriesForm;
        List<Object> mListInputsQuery;
        AsyncQueryFunctionName mFunctionName;
        string mAreaOfSightName = String.Empty;
        AsyncQueryFunctionName mCameFromOtherFunctionName = AsyncQueryFunctionName.None;

        static List<AsyncQueryResultsCallback> asyncQueryResultsCallbacks = new List<AsyncQueryResultsCallback>();

        public static List<AsyncQueryResultsCallback> AsyncQueryResultsCallbacks
        {
            get { return asyncQueryResultsCallbacks; }
            set { asyncQueryResultsCallbacks = value; }
        }

        public void RemoveAsyncQueryCallback()
        {
            if (AsyncQueryResultsCallbacks.Contains(this))
            {
                AsyncQueryResultsCallbacks.Remove(this);
            }
        }

        public AsyncQueryResultsCallback(SpatialQueriesForm spatialQueriesForm, 
            List<Object> listInputsQuery, 
            AsyncQueryFunctionName functionName, 
            AsyncQueryFunctionName cameFromOtherFunctionName = AsyncQueryFunctionName.None)
        {
            mSpatialQueriesForm = spatialQueriesForm;
            mListInputsQuery = listInputsQuery;
            mFunctionName = functionName;

            if (mFunctionName == AsyncQueryFunctionName.GetAreaOfSight)
                mAreaOfSightName = listInputsQuery[1].ToString();

            mCameFromOtherFunctionName = cameFromOtherFunctionName;

            AsyncQueryResultsCallbacks.Add(this);
        }

        public void OnTerrainHeightResults(bool bHeightFound, double dHeight, DNSMcVector3D? pNormal)
        {
            RemoveAsyncQueryCallback();
            if (mCameFromOtherFunctionName == AsyncQueryFunctionName.None)
            {
                List<object> listResults = new List<object>();
                listResults.Add(bHeightFound);
                listResults.Add(dHeight);
                listResults.Add(pNormal);
                MessageBox.Show("Function: " + AsyncQueryFunctionName.GetTerrainHeight.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);

                mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetTerrainHeight, listResults, true, mListInputsQuery);
            }
            else if(mCameFromOtherFunctionName == AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth)
            {
                mSpatialQueriesForm.UpdateLocationFromTwoDistancesAndAzimuthResults((DNSMcVector3D)mListInputsQuery[0], bHeightFound, dHeight);
            }
           /* else if (mCameFromOtherFunctionName == AsyncQueryFunctionName.GetTerrainHeightMatrix)
            {
                float rectWidth = (float)mListInputsQuery[1];
                float rectHeight = (float)mListInputsQuery[2];
                DNSMcVector3D[] rectPoints = (DNSMcVector3D[])mListInputsQuery[3];

                mSpatialQueriesForm.UpdateTerrainHeightMatrix(bHeightFound, dHeight, rectWidth, rectHeight, rectPoints);
            }*/
        }

        public void OnTerrainHeightsAlongLineResults(DNSMcVector3D[] aPointsWithHeights, float[] aSlopes, DNSSlopesData? pSlopesData)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(aSlopes);
            listResults.Add(pSlopesData);
            listResults.Add(aPointsWithHeights);

            if (mCameFromOtherFunctionName == AsyncQueryFunctionName.None)
            {
                MessageBox.Show("Function: " + AsyncQueryFunctionName.GetTerrainHeightsAlongLine.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetTerrainHeightsAlongLine, listResults, true, mListInputsQuery);
            }
            else if(mCameFromOtherFunctionName == AsyncQueryFunctionName.InitRandomGPSPoint)
            {
                mSpatialQueriesForm.InitRandomGPSPointAfterAsync(aPointsWithHeights);
            }
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? HighestPt, DNSMcVector3D? LowestPt)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(bPointsFound);
            listResults.Add(HighestPt);
            listResults.Add(LowestPt);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetExtremeHeightPointsInPolygon.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetExtremeHeightPointsInPolygon, listResults, true, mListInputsQuery);

        }

        public void OnTerrainHeightMatrixResults(double[] adHeightMatrix)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(adHeightMatrix);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetTerrainHeightMatrix.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetTerrainHeightMatrix, listResults, true, mListInputsQuery);
        }

        public void OnTerrainAnglesResults(double dPitch, double dRoll)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(dPitch);
            listResults.Add(dRoll);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetTerrainAngles.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetTerrainAngles, listResults, true, mListInputsQuery);
        }

        public void OnRayIntersectionResults(bool bIntersectionFound, DNSMcVector3D? pIntersection, DNSMcVector3D? pNormal, double? pdDistance)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(bIntersectionFound);
            listResults.Add(pIntersection);
            listResults.Add(pNormal);
            listResults.Add(pdDistance);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetRayIntersection.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetRayIntersection, listResults, true, mListInputsQuery);

        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(new List<DNSTargetFound>(aIntersections));

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetRayIntersectionTargets.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetRayIntersectionTargets, listResults, true, mListInputsQuery);

        }

        public void OnLineOfSightResults(DNSLineOfSightPoint[] aPoints, double dCrestClearanceAngle, double dCrestClearanceDistance)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(aPoints);
            listResults.Add(dCrestClearanceAngle);
            listResults.Add(dCrestClearanceDistance);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetLineOfSight.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetLineOfSight, listResults, true, mListInputsQuery);

        }

        public void OnPointVisibilityResults(bool bIsTargetVisible, double? pdMinimalTargetHeightForVisibility, double? pdMinimalScouterHeightForVisibility)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(bIsTargetVisible);
            if (pdMinimalTargetHeightForVisibility == null)
                listResults.Add(pdMinimalTargetHeightForVisibility);
            else
            {
                DNMcNullableOut<double> minimalTargetHeightForVisibility = new DNMcNullableOut<double>();
                minimalTargetHeightForVisibility.Value = pdMinimalTargetHeightForVisibility.Value;
                listResults.Add(minimalTargetHeightForVisibility);
            }
            if (pdMinimalScouterHeightForVisibility == null)
                listResults.Add(pdMinimalScouterHeightForVisibility);
            else
            {
                DNMcNullableOut<double> minimalScouterHeightForVisibility = new DNMcNullableOut<double>();
                minimalScouterHeightForVisibility.Value = pdMinimalScouterHeightForVisibility.Value;
                listResults.Add(minimalScouterHeightForVisibility);
            }

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetPointVisibility.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetPointVisibility, listResults, true, mListInputsQuery);
        }

        public void OnAreaOfSightResults(IDNAreaOfSight pAreaOfSight, DNSLineOfSightPoint[][] aLinesOfSight,
            DNSPolygonsOfSight? pSeenPolygons, DNSPolygonsOfSight? pUnseenPolygons, DNSStaticObjectsIDs[] aSeenStaticObjects)
        {
            RemoveAsyncQueryCallback();

            List<object> listResults = new List<object>();
            listResults.Add(pAreaOfSight);
            listResults.Add(aLinesOfSight);
            if (pSeenPolygons.HasValue)
                listResults.Add(pSeenPolygons.Value);
            else
                listResults.Add(null);
            if (pUnseenPolygons.HasValue)
                listResults.Add(pUnseenPolygons.Value);
            else
                listResults.Add(null);
            listResults.Add(aSeenStaticObjects);

            if(pAreaOfSight != null)
                mSpatialQueriesForm.AddAreaOfSight(pAreaOfSight);

            MessageBox.Show("Function: " + mFunctionName + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(mFunctionName, listResults, true, mListInputsQuery);

        }

        public void OnBestScoutersLocationsResults(DNSMcVector3D[] aScouters)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(aScouters);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetBestScoutersLocationsInEllipse.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetBestScoutersLocationsInEllipse, listResults, true, mListInputsQuery);
        }

        public void OnLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D Target)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(Target);
    
            MessageBox.Show("Function: " + AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.LocationFromTwoDistancesAndAzimuth, listResults, true, mListInputsQuery);
        }

        public void OnDtmLayerTileGeometryByKeyResults(DNSTileGeometry TileGeometry)
        {
            // :::???!!!::: implement...
        }

        public void OnRasterLayerTileBitmapByKeyResults(
            DNEPixelFormat eBitmapPixelFormat, bool bBitmapFromTopToBottom,
            DNSMcSize BitmapSize, DNSMcSize BitmapMargins, Byte[] aBitmapBits)
        {
            // :::???!!!::: implement...
        }

        public void OnRasterLayerColorByPointResults(DNSMcBColor Color)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(Color);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetRasterLayerColorByPoint.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetRasterLayerColorByPoint, listResults, true, mListInputsQuery);
        }

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aPoints)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(aPoints);

            MessageBox.Show("Function: " + AsyncQueryFunctionName.GetTraversabilityAlongLine.ToString() + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mSpatialQueriesForm.AddResultToList(AsyncQueryFunctionName.GetTraversabilityAlongLine, listResults, true, mListInputsQuery);

        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            if(eErrorCode != DNEMcErrorCode.ASYNC_OPERATION_CANCELED) // if user came from 'cancel all' , remove callback will be after cancel all calling. 
                RemoveAsyncQueryCallback();

            MessageBox.Show("Function: " + mFunctionName.ToString() + ", Error: " + IDNMcErrors.ErrorCodeToString(eErrorCode),
                     "Error From Async Query Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (mCameFromOtherFunctionName == AsyncQueryFunctionName.None)
            {
                mSpatialQueriesForm.AddErrorToList(mFunctionName, eErrorCode, true, mListInputsQuery);
            }
        }

        
    }

    public enum AsyncQueryFunctionName
    {
        GetTerrainHeight,
        GetTerrainHeightsAlongLine,
        GetExtremeHeightPointsInPolygon,
        GetTerrainAngles,
        GetLineOfSight,
        GetPointVisibility,
        GetRayIntersection,
        GetRayIntersectionTargets,
        LocationFromTwoDistancesAndAzimuth,
        GetAreaOfSight,
        GetTerrainHeightMatrix,
        GetAreaOfSightForMultipleScouters,
        GetBestScoutersLocationsInEllipse,
        None,
        InitRandomGPSPoint,
        GetTraversabilityAlongLine,
        GetRasterLayerColorByPoint
    }

    public class MCTObjectsAreaOfSight
    {
        IDNMcObject m_DrawObject;
        List<IDNMcObject> m_lstLineOfSight;
        IDNAreaOfSight m_AreaOfSight;
        List<IDNMcObject> m_lstSeenPolygon;
        List<IDNMcObject> m_lstUnseenPolygon;
        DNSMcBColor m_StaticObjectsSelectedColor;
        List<StaticObjectsColor> m_lstStaticObjectsColor;
        //List<IDNMcObject> m_PolygonContours;
        bool m_IsMultipleScouter = false;
        uint m_NumOfScouters;

        string m_CalcName;

        public string CalcName
        {
            get { return m_CalcName; }
            set { m_CalcName = value; }
        }

        public uint NumOfScouters
        {
            get { return m_NumOfScouters; }
            set { m_NumOfScouters = value; }
        }

        public bool IsMultipleScouter
        {
            get { return m_IsMultipleScouter; }
            set { m_IsMultipleScouter = value; }
        }

        

        public List<StaticObjectsColor> LstStaticObjectsColor
        {
            get { return m_lstStaticObjectsColor; }
            set { m_lstStaticObjectsColor = value; }
        }

        public DNSMcBColor StaticObjectsSelectedColor
        {
            get { return m_StaticObjectsSelectedColor; }
            set { m_StaticObjectsSelectedColor = value; }
        }

     
        bool m_isShowDrawObject;
        bool m_isShowAreaOfSight;
        bool m_isShowLineOfSight;
        bool m_isShowSeenPolygon;
        bool m_isShowUnseenPolygon;
        bool m_isShowSeenStaticObjects;

        IDNMcObject m_TextureGPU;

        public IDNMcObject DrawObject
        {
            get { return m_DrawObject; }
            set {
                m_DrawObject = value;
                IsShowDrawObject = (m_DrawObject != null);
            }
        }
        
        public bool IsShowDrawObject
        {
            get { return m_isShowDrawObject; }
            set { m_isShowDrawObject = value; }
        }

        public bool IsShowLineOfSight
        {
            get { return m_isShowLineOfSight; }
            set { m_isShowLineOfSight = value; }
        }
        public bool IsShowAreaOfSight
        {
            get { return m_isShowAreaOfSight; }
            set { m_isShowAreaOfSight = value; }
        }

        public bool IsShowSeenPolygon
        {
            get { return m_isShowSeenPolygon; }
            set { m_isShowSeenPolygon = value; }
        }

        public bool IsShowUnseenPolygon
        {
            get { return m_isShowUnseenPolygon; }
            set { m_isShowUnseenPolygon = value; }
        }

        public bool IsShowSeenStaticObjects
        {
            get { return m_isShowSeenStaticObjects; }
            set { m_isShowSeenStaticObjects = value; }
        }

        public List<IDNMcObject> ListLineOfSight
        {
            get { return m_lstLineOfSight; }
            set { m_lstLineOfSight = value; }
        }

        public IDNAreaOfSight AreaOfSight
        {
            get { return m_AreaOfSight; }
            set { m_AreaOfSight = value; }
        }
 
        public List<IDNMcObject> ListSeenPolygon
        {
            get { return m_lstSeenPolygon; }
            set { m_lstSeenPolygon = value; }
        }
 
        public List<IDNMcObject> ListUnseenPolygon
        {
            get { return m_lstUnseenPolygon; }
            set { m_lstUnseenPolygon = value; }
        }
 
        public IDNMcObject TextureGPU
        {
            get { return m_TextureGPU; }
            set { m_TextureGPU = value; }
        }

        public MCTObjectsAreaOfSight() {
            m_lstLineOfSight = new List<IDNMcObject>();
            m_AreaOfSight = null;
            m_lstSeenPolygon = new List<IDNMcObject>();
            m_lstUnseenPolygon = new List<IDNMcObject>();
            m_lstStaticObjectsColor = new List<StaticObjectsColor>();
            //m_PolygonContours = new List<IDNMcObject>();
        }

        public MCTObjectsAreaOfSight(
            IDNMcObject drawObject,
            bool bCalcAreaOfSight,
            bool bCalcLineOfSight,
            bool bCalcSeenPolygon,
            bool bCalcUnseenPolygon,
            bool bCalcStaticObjects,
            DNSMcBColor StaticObjectsColor)
        {
            m_DrawObject = drawObject;
            if (m_DrawObject != null)
                IsShowDrawObject = true;

            m_isShowAreaOfSight = bCalcAreaOfSight;
            m_isShowLineOfSight = bCalcLineOfSight;
            m_isShowSeenPolygon = bCalcSeenPolygon;
            m_isShowUnseenPolygon = bCalcUnseenPolygon;
            m_isShowSeenStaticObjects = bCalcStaticObjects;
            m_StaticObjectsSelectedColor = StaticObjectsColor;

            m_lstLineOfSight = new List<IDNMcObject>();
            m_AreaOfSight = null;
            m_lstSeenPolygon = new List<IDNMcObject>();
            m_lstUnseenPolygon = new List<IDNMcObject>();
            m_lstStaticObjectsColor = new List<StaticObjectsColor>();
           // m_PolygonContours = new List<IDNMcObject>();
        }

        public void RemoveObjects()
        {
            try
            {
                List<IDNMcObject> lstObjsToRemove = new List<IDNMcObject>();
                if (ListLineOfSight!= null)
                    lstObjsToRemove.AddRange(ListLineOfSight);
                if (ListSeenPolygon != null)
                    lstObjsToRemove.AddRange(ListSeenPolygon);
                if (ListUnseenPolygon != null)
                    lstObjsToRemove.AddRange(ListUnseenPolygon);

                foreach (IDNMcObject objToRemove in lstObjsToRemove)
                {
                    objToRemove.Dispose();
                    objToRemove.Remove();
                }
                ListLineOfSight = null;
                ListSeenPolygon = null;
                ListUnseenPolygon = null;
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Remove Object", McEx);
            }

            try
            {
                if (TextureGPU != null)
                {
                    TextureGPU.Dispose();
                    TextureGPU.Remove();
                    TextureGPU = null;
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("Remove Object", McEx);
            }
            
        }
    }

    public class StaticObjectsColor
    {
        public IDNMcVector3DExtrusionMapLayer layer;
        public DNSMcBColor color;
        public List<DNSMcVariantID> lstVariantId;
        public DNSStaticObjectContour[][] aaStaticObjectsContours;
        public List<IDNMcObject> PolygonContours;

        public StaticObjectsColor()
        {
            PolygonContours = new List<IDNMcObject>();
        }
    }

    public class MCTAreaOfSightSelectUserShowResults
    {
        bool m_isShowDrawObject;
        bool m_isShowAreaOfSight;
        bool m_isShowLineOfSight;
        bool m_isShowSeenPolygon;
        bool m_isShowUnseenPolygon;
        bool m_isShowSeenStaticObjects;

        public bool IsShowDrawObject
        {
            get { return m_isShowDrawObject; }
            set { m_isShowDrawObject = value; }
        }

        public bool IsShowLineOfSight
        {
            get { return m_isShowLineOfSight; }
            set { m_isShowLineOfSight = value; }
        }
        public bool IsShowAreaOfSight
        {
            get { return m_isShowAreaOfSight; }
            set { m_isShowAreaOfSight = value; }
        }

        public bool IsShowSeenPolygon
        {
            get { return m_isShowSeenPolygon; }
            set { m_isShowSeenPolygon = value; }
        }

        public bool IsShowUnseenPolygon
        {
            get { return m_isShowUnseenPolygon; }
            set { m_isShowUnseenPolygon = value; }
        }

        public bool IsShowSeenStaticObjects
        {
            get { return m_isShowSeenStaticObjects; }
            set { m_isShowSeenStaticObjects = value; }
        }

    }

    [DataContract]
    public class MCTAreaOfSightDrawObjectParams : MCTPackageBase
    {
        public enum MCTAreaOfSightDrawObjectType
        { Ellipse, Polygon, Rectangle }

        MCTAreaOfSightDrawObjectType mObjectType;
        string mObjectTypeName;
        DNSMcVector3D[] mLocationPoints;
        float mRadiusX;
        float mRadiusY;
        float mStartAngle;
        float mEndAngle;

        [DataMember(Order = 0)]
        public MCTAreaOfSightDrawObjectType ObjectType
        {
            get { return mObjectType; }
            set { mObjectType = value; }
        }

        [DataMember(Order = 1)]
        public string ObjectTypeName
        {
            get { return mObjectType.ToString(); }
            set { mObjectTypeName = value; }
        }


        [DataMember(Order = 2)]
        public float RadiusX
        {
            get { return mRadiusX; }
            set { mRadiusX = value; }
        }

        [DataMember(Order = 3)]
        public float RadiusY
        {
            get { return mRadiusY; }
            set { mRadiusY = value; }
        }

        [DataMember(Order = 4)]
        public float StartAngle
        {
            get { return mStartAngle; }
            set { mStartAngle = value; }
        }

        [DataMember(Order = 5)]
        public float EndAngle
        {
            get { return mEndAngle; }
            set { mEndAngle = value; }
        }

        [DataMember(Order = 6)]
        public DNSMcVector3D[] LocationPoints
        {
            get { return mLocationPoints; }
            set { mLocationPoints = value; }
        }

        public void SetEllipseParams(DNSMcVector3D[] locationPoints,
        float radiusX,
        float radiusY,
        float startAngle,
        float endAngle)
        {
            ObjectType = MCTAreaOfSightDrawObjectType.Ellipse;
            LocationPoints = locationPoints;
            RadiusX = radiusX;
            RadiusY = radiusY;
            StartAngle = startAngle;
            EndAngle = endAngle;
        }

        public void SetRectangleParams(DNSMcVector3D[] locationPoints,
        float radiusX,
        float radiusY)
        {
            ObjectType = MCTAreaOfSightDrawObjectType.Rectangle;
            LocationPoints = locationPoints;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        public void SetPolygonParams(DNSMcVector3D[] locationPoints)
        {
            ObjectType = MCTAreaOfSightDrawObjectType.Polygon;
            LocationPoints = locationPoints;
        }

        /*public static bool Load(string fileName, out MCTAreaOfSightDrawObjectParams AOSDrawObjectParams)
        {
            return Load(fileName, out AOSDrawObjectParams);
        }*/

       
    }

    public class MCTSAreaOfSightMatrixData
    {
        public IDNAreaOfSight areaOfSight;
        public DNSAreaOfSightMatrix areaOfSightMatrix;
        public string nameToShow;
        public bool isClone;
        public bool isSum;

        public override string ToString()
        {
            return nameToShow;
        }
    }
}




        
