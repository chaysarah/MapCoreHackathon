using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapCore;
using MCTester.Managers.ObjectWorld;
using UnmanagedWrapper;
using System.Windows.Forms;
using MCTester.GUI.Map;
using System.Drawing;
using MCTester.Managers.MapWorld;
using MapCore.Common;

namespace MCTester.ButtonsImplementation
{
    public class btnSightEllipseItem
    {
        private DNEMcPointCoordSystem m_eLocationCoordSystem = DNEMcPointCoordSystem._EPCS_WORLD;
        private bool bLocationRelativeToDTM = false;
        private uint propertyId_RadiusX = 10;
        private uint propertyId_NonVisible_StartAngle = 20;
        private float NonVisible_StartAngle = 0;
        private float NonVisible_EndAngle = 45;
        private float Visible_StartAngle = -45;
        private float Visible_EndAngle = 45;
        private uint Id_Visible = 100;
        private uint ObjectLocationVisibleIndex = 0;
        private bool m_RenderNeeded;
        private bool m_IsInEditMode = false;
        private IDNMcObject m_pObject = null;
        private DNSMcVector3D m_SectorCenter;
        private float m_SightObservedHeight = 0f;
        private float m_SightObserverHeight = 0.5f;
        private double m_diff_x_y = 1.5;
        private double m_diff_z = 1.5;
        private double m_dInterpolationNumPoints = 4;
        private double m_dInterpolation = 0.25;
        private IDNMcGeographicCalculations m_mcGeographicCalculations = null;
        private double m_DistanceFromFirstPoint = 0.5;
        private double m_dHorizontalResolution = 1, m_dVerticalResolution = 1;

        public btnSightEllipseItem()
        {
        }

        public void ExecuteAction()
        {
            m_mcGeographicCalculations = DNMcGeographicCalculations.Create(Manager_MCOverlayManager.ActiveOverlayManager.GetCoordinateSystemDefinition());
            MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent += new General_Forms.MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);
            MCTMapFormManager.MapForm.MapObjectCtrl.MouseMoveEvent += new General_Forms.MouseMoveEventArgs(MapObjectCtrl_MouseMoveEvent);
        }

        DNSMcVector3D ChangePoint(double distance)
        {
            double dAzimuth = Convert.ToDouble(m_pObject.GetProperty(propertyId_NonVisible_StartAngle).Value);
            DNSMcVector3D point = m_pObject.GetLocationPoints(ObjectLocationVisibleIndex)[0];
            return m_mcGeographicCalculations.LocationFromAzimuthAndDistance(point, dAzimuth, distance, true);
        }


        // after user select point to start ellipse, we move this point 'm_DistanceFromFirstPoint' distance to get out from house.
        private void MapObjectCtrl_MouseMoveEvent(object sender, Point mouseLocation, MouseButtons mouseClickedButton, int mouseWheelDelta)
        {
            if (m_IsInEditMode && m_pObject != null)
            {
                MCTMapFormManager.MapForm.MapObjectCtrl.MouseMoveEvent -= new General_Forms.MouseMoveEventArgs(MapObjectCtrl_MouseMoveEvent);
                
                m_pObject.SetLocationPoints(new DNSMcVector3D[1] { ChangePoint(m_DistanceFromFirstPoint) }, ObjectLocationVisibleIndex);
                m_IsInEditMode = false;
            }
        }

        // this function handle to draw gpu ellipse with Definition = EED_CIRCLE_CENTER_RADIUS_ANGLES 
        // with start and end angle to get sector ellipse
        // and simulate mouse down event to start init object.

        void MapObjectCtrl_MouseDownEvent(object sender, Point mouseLocation, MouseButtons mouseClickedButton, int mouseWheelDelta)
        {
            MCTMapFormManager.MapForm.MapObjectCtrl.MouseDownEvent -= new General_Forms.MouseDownEventArgs(MapObjectCtrl_MouseDownEvent);
            try
            {
               if (MCTMapFormManager.MapForm.Viewport.MapType == DNEMapType._EMT_3D)
                    MCTMapFormManager.MapForm.Viewport.SetTransparencyOrderingMode(true);

                IDNMcMapTerrain[] terrains = MCTMapFormManager.MapForm.Viewport.GetTerrains();
                foreach(IDNMcMapTerrain terrain in terrains)
                {
                    IDNMcMapLayer[] mcMapLayers = terrain.GetLayers();
                    foreach(IDNMcMapLayer layer in mcMapLayers)
                    {
                        if(layer is IDNMcStaticObjectsMapLayer)
                        {
                            (layer as IDNMcStaticObjectsMapLayer).SetDisplayingItemsAttachedToTerrain(true);
                        }
                    }
                }

                if (Manager_MCOverlayManager.ActiveOverlayManager != null)
                {

                    IDNMcOverlay activeOverlay = Manager_MCOverlayManager.ActiveOverlay;

                    if (activeOverlay != null)
                    {
                        DNSMcVector3D ScreenCenter = new DNSMcVector3D(mouseLocation.X, mouseLocation.Y, 0);
                        m_SectorCenter = new DNSMcVector3D();
                        bool bIntersection;
                        DNSQueryParams Params = new DNSQueryParams();
                        Params.eTerrainPrecision = DNEQueryPrecision._EQP_HIGHEST;
                        MCTMapFormManager.MapForm.Viewport.ScreenToWorldOnTerrain(ScreenCenter, out m_SectorCenter, out bIntersection, Params);

                        bool pbHeightFound = false;
                        double pHeight = 0;
                        DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();
                        MCTMapFormManager.MapForm.Viewport.GetTerrainHeight(m_SectorCenter, out pbHeightFound, out pHeight, normal, Params);
                        if (pbHeightFound)
                            m_SectorCenter.z = pHeight;

                       DNSMcVector3D[] locationPoints = new DNSMcVector3D[1] { m_SectorCenter };

                       IDNMcEllipseItem McEllipseItemNonVisible = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                        m_eLocationCoordSystem,
                                                                                        DNEItemGeometryType._EGT_GEOGRAPHIC,
                                                                                        1,
                                                                                        1,
                                                                                        NonVisible_StartAngle,
                                                                                        NonVisible_EndAngle);

                        McEllipseItemNonVisible.SetEllipseDefinition(DNEEllipseDefinition._EED_CIRCLE_CENTER_RADIUS_ANGLES);
                        McEllipseItemNonVisible.SetLineStyle(DNELineStyle._ELS_NO_LINE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemNonVisible.SetFillStyle(DNEFillStyle._EFS_NONE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        IDNMcEllipseItem McEllipseItemVisible = DNMcEllipseItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                        m_eLocationCoordSystem,
                                                                                        DNEItemGeometryType._EGT_GEOGRAPHIC,
                                                                                        1,
                                                                                        1,
                                                                                        Visible_StartAngle,
                                                                                        Visible_EndAngle);

                        McEllipseItemVisible.SetID(Id_Visible);
                        McEllipseItemVisible.SetEllipseDefinition(DNEEllipseDefinition._EED_CIRCLE_CENTER_RADIUS_ANGLES);
                        McEllipseItemVisible.SetFillColor(new DNSMcBColor(0 ,255, 0, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetBlockedTransparency(100, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetSightPresentationType(DNESightPresentationType._ESPT_GPU, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetIsSightObserverHeightAbsolute(true, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        McEllipseItemVisible.SetSightColor(DNEPointVisibility._EPV_SEEN, new DNSMcBColor(0, 255, 0, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetSightColor(DNEPointVisibility._EPV_UNSEEN, new DNSMcBColor(255, 0, 0, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetSightColor(DNEPointVisibility._EPV_UNKNOWN, new DNSMcBColor(255, 0, 255, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                       // McEllipseItemVisible.SetSightColor(DNEPointVisibility._EPV_OUT_OF_QUERY_AREA, new DNSMcBColor(128, 128, 128, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetSightColor(DNEPointVisibility._EPV_SEEN_STATIC_OBJECT, new DNSMcBColor(255, 255, 0, 125), (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetSightObserverHeight((float)m_SectorCenter.z + m_SightObserverHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetSightObservedHeight(m_SightObservedHeight, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                        McEllipseItemVisible.SetSightTextureResolution(0.1f, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);

                        IDNMcObjectLocation mcObjectLocation1 = null, mcObjectLocation2 = null;
                        IDNMcObjectScheme mcObjectScheme = DNMcObjectScheme.Create(ref mcObjectLocation1, Manager_MCOverlayManager.ActiveOverlayManager, m_eLocationCoordSystem);
                        McEllipseItemVisible.Connect(mcObjectLocation1);

                        uint puLocationIndex;
                        mcObjectScheme.AddObjectLocation(out mcObjectLocation2, m_eLocationCoordSystem, bLocationRelativeToDTM, out puLocationIndex, 1);
                        McEllipseItemNonVisible.Connect(mcObjectLocation2);

                        McEllipseItemNonVisible.SetRadiusX(1, propertyId_RadiusX, 0);
                        McEllipseItemNonVisible.SetStartAngle(0, propertyId_NonVisible_StartAngle, 0);

                        McEllipseItemVisible.SetRadiusX(1, propertyId_RadiusX, 0);
                        McEllipseItemVisible.SetRotationYaw(0, propertyId_NonVisible_StartAngle, 0);
                        
                        m_pObject = DNMcObject.Create(activeOverlay, mcObjectScheme, locationPoints);

                        MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent += new InitItemResultsEventArgs(InitItemResults);
                        MCTMapFormManager.MapForm.EditMode.StartInitObject(m_pObject, McEllipseItemNonVisible);
                        DNECursorTypeEditMode cursorTypeEditMode;
                        MCTMapFormManager.MapForm.EditMode.OnMouseEvent(DNEMouseEventEditMode._EME_EM_BUTTON_PRESSED, new DNSMcPoint(mouseLocation), false, 0, out m_RenderNeeded, out cursorTypeEditMode);
                        m_IsInEditMode = true;

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
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating ellipse item", McEx);
            }
        }

        // came here when user finish to draw the gpu ellipse.
        // now start the calculation:
        // 1. call to GetAllCalculatedPoints() to get the calculated points of the ellipse
        // 2. call to EG2DPolyBoundingRect to get the bounding box of the calculated points.
        // 3. call to GetTerrainHeightMatrix to get the heights for each point in the bounding box
        // 4. do loops to get each point in the bounding box .
        // 5. for each point check if it's in the sector ellipse:
        // 5.1. call to AzimuthAndDistanceBetweenTwoLocations to calculate the radius from the point to start sector, continue if new radius is smallest or equal to sector radius
        // 5.2. call to DNSMcVector2D.^ function to check if new angle if between the sector.
        // 6. for each point in the sector we calculated his neighbors in the height matrix
        ///   i2	     i1	        i0
        ///   i3     height_index   i7
        ///   i4        i5          i6
        // to find if the is height difference.
        // 7. if exist height difference, we calculated the number of points need to added to the max height.
        // 8. for all the relevant points - we call to GetPointVisibility(). if return true, we create green textre object, otherwise we create red textre object
        
        public void InitItemResults(IDNMcObject pObject, IDNMcObjectSchemeItem pItem, int nExitCode)
        {
            try
            {
               
                IDNMcTexture textureVisible = DNMcBitmapHandleTexture.Create(Icons.sight_green2.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
                IDNMcObjectSchemeItem picItemVisible = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, textureVisible);

                IDNMcTexture textureNonVisible = DNMcBitmapHandleTexture.Create(Icons.sight_red.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));
                IDNMcObjectSchemeItem picItemNonVisible = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, textureNonVisible);

                IDNMcObjectScheme mcObjectSchemeVisible = DNMcObjectScheme.Create(Manager_MCOverlayManager.ActiveOverlayManager, picItemVisible, m_eLocationCoordSystem);
                IDNMcObjectScheme mcObjectSchemeNonVisible = DNMcObjectScheme.Create(Manager_MCOverlayManager.ActiveOverlayManager, picItemNonVisible, m_eLocationCoordSystem);

                MCTMapFormManager.MapForm.EditModeManagerCallback.InitItemResultsEvent -= new InitItemResultsEventArgs(InitItemResults);

                if (nExitCode != 0 && pObject != null && pItem is IDNMcEllipseItem)
                {
                    DNSMcVector3D[] locationPoints = pObject.GetLocationPoints(ObjectLocationVisibleIndex);
                    if (locationPoints.Length > 0)
                    {
                        DNSMcVector3D WorldCenterPoint = locationPoints[0];
                        float radiusx, yaw;
                        radiusx = (float)pObject.GetProperty(propertyId_RadiusX).Value;
                        yaw = (float)pObject.GetProperty(propertyId_NonVisible_StartAngle).Value; ;

                        IDNMcEllipseItem mcEllipseItem = (IDNMcEllipseItem)pObject.GetNodeByID(Id_Visible);

                        DNSMcVector3D[] polypoints = new DNSMcVector3D[0];
                        DNEMcPointCoordSystem pointCoordSystem = DNEMcPointCoordSystem._EPCS_WORLD;

                        // 1. call to GetAllCalculatedPoints() to get the calculated points of the ellipse

                        mcEllipseItem.GetAllCalculatedPoints(MCTMapFormManager.MapForm.Viewport, pObject, out polypoints, out pointCoordSystem);
                       
                        // 2. call to EG2DPolyBoundingRect to get the bounding box of the calculated points.

                        double pdLeft = 0, pdRight = 0, pdDown = 0, pdUp = 0;

                        IDNMcGeometricCalculations.EG2DPolyBoundingRect(polypoints, DNGEOMETRIC_SHAPE._EG_POLYGON, ref pdLeft, ref pdRight, ref pdDown, ref pdUp);
                        DNSMcVector3D leftdown = new DNSMcVector3D(pdLeft, pdDown, 0);
                        DNSMcVector3D leftup = new DNSMcVector3D(pdLeft, pdUp, 0);
                        DNSMcVector3D rightdown = new DNSMcVector3D(pdRight, pdDown, 0);
                        DNSMcVector3D rightup = new DNSMcVector3D(pdRight, pdUp, 0);
                        DNSMcVector3D[] BoundingRect = new DNSMcVector3D[4] { leftdown, leftup, rightup, rightdown };

                        double lengthVertical = Math.Abs(pdUp - pdDown);
                        double lengthHorizontal = Math.Abs(pdRight - pdLeft);
                        uint uNumVerticalPoints = (uint)Math.Ceiling(lengthVertical / m_dVerticalResolution);
                        uint uNumHorizontalPoints = (uint)Math.Ceiling(lengthHorizontal / m_dHorizontalResolution);

                        DNSQueryParams Params = new DNSQueryParams();
                        Params.eTerrainPrecision = DNEQueryPrecision._EQP_HIGHEST;
                       
                        // 3. call to GetTerrainHeightMatrix to get the heights for each point in the bounding box

                        double[] heights = MCTMapFormManager.MapForm.Viewport.GetTerrainHeightMatrix(leftdown, m_dHorizontalResolution, m_dVerticalResolution, uNumHorizontalPoints, uNumVerticalPoints, Params);

                        if (heights != null)
                        {
                            // add to heights Observed Height
                            for (int i = 0; i < heights.Length; i++)
                            {
                                heights[i] += m_SightObservedHeight;
                            }

                            List<DNSMcVector3D> pointsmatrix = new List<DNSMcVector3D>();
                            List<DNSMcVector3D> pointsLinearInterpolationMatrix = new List<DNSMcVector3D>();
                            DNSMcVector2D P1 = new DNSMcVector2D(polypoints[0]);
                            DNSMcVector2D P2 = new DNSMcVector2D(polypoints[polypoints.Length - 2]);
                            DNSMcVector2D WorldCenterPoint2D = new DNSMcVector2D(WorldCenterPoint);
                            DNSMcVector2D diff_P1_from_Center = P1 - WorldCenterPoint2D;
                            DNSMcVector2D diff_P2_from_Center = P2 - WorldCenterPoint2D;

                            // 4. do loops to get each point in the bounding box .

                            for (int i_y = 0; i_y < uNumVerticalPoints; i_y++)
                            {
                                double point_y = leftdown.y + i_y * m_dVerticalResolution;
                                for (int j_x = 0; j_x < uNumHorizontalPoints; j_x++)
                                {
                                    double point_x = leftdown.x + j_x * m_dHorizontalResolution;
                                    int height_index = (int)(i_y * uNumHorizontalPoints) + j_x;
                                    double point_z = heights[height_index];
                                    DNSMcVector3D newPoint = new DNSMcVector3D(point_x, point_y, point_z);

                                    // 5. for each point check if it's in the sector ellipse:
                                    // 5.1. call to AzimuthAndDistanceBetweenTwoLocations to calculate the radius from the point to start sector, continue if new radius is smallest or equal to sector radius

                                    DNMcNullableOut<double> newDistance = new DNMcNullableOut<double>();
                                    m_mcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations(newPoint, WorldCenterPoint, null, newDistance);
                                    if (newDistance.Value <= radiusx)
                                    {
                                        // 5.2. call to DNSMcVector2D.^ function to check if new angle if between the sector.

                                        DNSMcVector2D newPoint2D = new DNSMcVector2D(newPoint);
                                        DNSMcVector2D diff_point_from_center = newPoint2D - WorldCenterPoint2D;

                                        double roof1 = diff_point_from_center ^ diff_P1_from_Center;
                                        double roof2 = diff_point_from_center ^ diff_P2_from_Center;

                                        if (roof1 >= 0 && roof2 <= 0)
                                        {
                                            // 6. for each point in the sector we calculated his neighbors in the height matrix
                                            ///   i2	     i1	        i0
                                            ///   i3     height_index   i7
                                            ///   i4        i5          i6
                                            // to find if the is height difference.

                                            pointsmatrix.Add(newPoint);

                                            int i0, i1, i2, i3, i4, i5, i6, i7;

                                            i0 = (int)(height_index - uNumHorizontalPoints - 1);
                                            i1 = (int)(height_index - uNumHorizontalPoints);     // i0 +1
                                            i2 = (int)(height_index - uNumHorizontalPoints + 1); // i1 +1
                                            i3 = height_index + 1;
                                            i4 = (int)(height_index + uNumHorizontalPoints + 1);
                                            i5 = (int)(height_index + uNumHorizontalPoints);
                                            i6 = (int)(height_index + uNumHorizontalPoints - 1);
                                            i7 = height_index - 1;

                                            if (i_y == 0)
                                                i0 = i1 = i2 = -1;
                                            if (i_y == uNumVerticalPoints - 1)
                                                i4 = i5 = i6 = -1;
                                            if (height_index % uNumHorizontalPoints == 0)
                                                i0 = i6 = i7 = -1;
                                            if ((height_index + 1) % uNumHorizontalPoints == 0)
                                                i2 = i3 = i4 = -1;
                                            DNSMcVector3D point0 = DNSMcVector3D.v3Zero, point1 = DNSMcVector3D.v3Zero, point2 = DNSMcVector3D.v3Zero, point3 = DNSMcVector3D.v3Zero, point4 = DNSMcVector3D.v3Zero, point5 = DNSMcVector3D.v3Zero, point6 = DNSMcVector3D.v3Zero, point7 = DNSMcVector3D.v3Zero;

                                            if (i0 >= 0) point0 = new DNSMcVector3D(point_x - m_dHorizontalResolution, point_y - m_dVerticalResolution, 0);
                                            if (i1 >= 0) point1 = new DNSMcVector3D(point_x, point_y - m_dVerticalResolution, 0);
                                            if (i2 >= 0) point2 = new DNSMcVector3D(point_x + m_dHorizontalResolution, point_y - m_dVerticalResolution, 0);
                                            if (i3 >= 0) point3 = new DNSMcVector3D(point_x + m_dHorizontalResolution, point_y, 0);
                                            if (i4 >= 0) point4 = new DNSMcVector3D(point_x + m_dHorizontalResolution, point_y + m_dVerticalResolution, 0);
                                            if (i5 >= 0) point5 = new DNSMcVector3D(point_x, point_y + m_dVerticalResolution, 0);
                                            if (i6 >= 0) point6 = new DNSMcVector3D(point_x - m_dHorizontalResolution, point_y + m_dVerticalResolution, 0);
                                            if (i7 >= 0) point7 = new DNSMcVector3D(point_x - m_dHorizontalResolution, point_y, 0);

                                            // remove indexes not in height array
                                            List<int> tmp_indexes = new List<int>() { i0, i1, i2, i3, i4, i5, i6, i7 };
                                            List<DNSMcVector3D> tmp_points = new List<DNSMcVector3D>() { point0, point1, point2, point3, point4, point5, point6, point7 };
                                            List<DNSMcVector3D> points = new List<DNSMcVector3D>();
                                            List<int> indexes = new List<int>();
                                            for (int i = 0; i < tmp_indexes.Count; i++)
                                            {
                                                int currIndex = tmp_indexes[i];
                                                if (currIndex >= 0 && currIndex < heights.Length)
                                                {
                                                    indexes.Add(currIndex);
                                                    DNSMcVector3D currPoint = tmp_points[i];
                                                    currPoint.z = heights[currIndex];
                                                    points.Add(currPoint);
                                                }
                                            }

                                            // get max height
                                            DNSMcVector3D max_point = new DNSMcVector3D(0, 0, m_diff_z);
                                            for (int i = 0; i < points.Count; i++)
                                            {
                                                DNSMcVector3D diffPoint = points[i] - newPoint;

                                                if (diffPoint.x < m_diff_x_y && diffPoint.y < m_diff_x_y && diffPoint.z > max_point.z)
                                                {
                                                    max_point = points[i];
                                                }
                                            }
                                            // 7. if exist height difference, we calculated the number of points need to added to the max height.

                                            if (max_point.z > m_diff_z)
                                            {
                                                DNSMcVector3D max_point2 = new DNSMcVector3D(newPoint.x, newPoint.y, max_point.z);
                                                m_dInterpolationNumPoints = Math.Ceiling(Math.Abs((max_point2.z - point_z) / m_diff_z)) - 1;
                                                m_dInterpolation = 1 / (m_dInterpolationNumPoints + 1);
                                                for (double j = m_dInterpolation; j < 1; j += m_dInterpolation)
                                                {
                                                    DNSMcVector3D linearInterpolationPoint = newPoint.GetLinearInterpolationWith(max_point2, j);
                                                    pointsLinearInterpolationMatrix.Add(new DNSMcVector3D(newPoint.x, newPoint.y, linearInterpolationPoint.z));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            pointsmatrix.AddRange(pointsLinearInterpolationMatrix);

                            double dMaxPitchAngle = 90, dMinPitchAngle = -90;
                            DNSMcVector3D WorldCenterPointWithHeight = WorldCenterPoint;
                            WorldCenterPointWithHeight.z += m_SightObserverHeight;

                            // 8. for all the relevant points - we call to GetPointVisibility(). if return true, we create green textre object, otherwise we create red textre object

                            foreach (DNSMcVector3D point in pointsmatrix)
                            {
                                bool isTargetVisible = false;
                                MCTMapFormManager.MapForm.Viewport.GetPointVisibility(WorldCenterPointWithHeight, true, point, true, out isTargetVisible, null, null, dMaxPitchAngle, dMinPitchAngle, Params);
                                if (isTargetVisible)
                                {
                                    DNMcObject.Create(Manager_MCOverlayManager.ActiveOverlay, mcObjectSchemeVisible, new DNSMcVector3D[1] { point });
                                }
                                else
                                {
                                    DNMcObject.Create(Manager_MCOverlayManager.ActiveOverlay, mcObjectSchemeNonVisible, new DNSMcVector3D[1] { point });
                                }
                            }
                        }
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("sight pre..", McEx);
            }
        }
    }
}
