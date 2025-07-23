using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnmanagedWrapper;
using MapCore;
using System.Windows;
using MCTester.Managers.ObjectWorld;

namespace MCTester.ButtonsImplementation
{
    public class btnCameraCornersAndCenter : IDNAsyncQueryCallback
    {
        bool m_CalcHorizon;

        public void ExecuteAction(bool calcHorizon, bool isAsync)
        {
            m_CalcHorizon = calcHorizon;

            if (MainForm.CameraCornersAndCenterObject != null)
            {
                MainForm.CameraCornersAndCenterObject.Dispose();
                MainForm.CameraCornersAndCenterObject.Remove();
                MainForm.CameraCornersAndCenterObject = null;
            }
            else
            {
                IDNMcMapViewport mapViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;
                if (mapViewport.GetImageCalc() != null && mapViewport.GetImageCalc().GetImageType() == DNEImageType._EIT_FRAME)
                {
                    IDNMcFrameImageCalc frameImageCalc = (IDNMcFrameImageCalc)mapViewport.GetImageCalc();
                    IDNAsyncQueryCallback asyncQueryCallback = isAsync ? this : null;
                    DNSMcVector3D[] arrCenterAndCorners;
                    DNERayStatus[] arrRayStatus;
                    try
                    {
                        frameImageCalc.GetCameraCornersAndCenter(calcHorizon, out arrCenterAndCorners, out arrRayStatus, asyncQueryCallback);
                        if (!isAsync)
                            GetCameraCornersAndCenter(arrCenterAndCorners, arrRayStatus);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("GetCameraCornersAndCenter()", McEx);
                    }
                }
            }
        }

        private void GetCameraCornersAndCenter(DNSMcVector3D[] arrCenterAndCorners, DNERayStatus[] arrRayStatus)
        {
            List<DNSMcVector3D> points = new List<DNSMcVector3D>();
            List<int> horizonIndexes = new List<int>();
            if (arrCenterAndCorners.Length == arrRayStatus.Length && arrRayStatus.Length == 5)
            {

                // center - 0
                if (arrRayStatus[0] != DNERayStatus._ERS_NoIntersection)
                {
                    if (arrRayStatus[0] == DNERayStatus._ERS_HorizonPoint)
                        horizonIndexes.Add(0);
                    points.Add(arrCenterAndCorners[0]);
                }
                // Bottom left - 4
                if (arrRayStatus[4] != DNERayStatus._ERS_NoIntersection)
                {
                    if (arrRayStatus[4] == DNERayStatus._ERS_HorizonPoint)
                        horizonIndexes.Add(1);
                    points.Add(arrCenterAndCorners[4]);
                }
                // Bottom right - 3
                if (arrRayStatus[3] != DNERayStatus._ERS_NoIntersection)
                {
                    if (arrRayStatus[3] == DNERayStatus._ERS_HorizonPoint)
                        horizonIndexes.Add(2);
                    points.Add(arrCenterAndCorners[3]);
                }
                // center - 0
                if (arrRayStatus[0] != DNERayStatus._ERS_NoIntersection)
                {
                    points.Add(arrCenterAndCorners[0]);
                }
                // Top right - 2
                if (arrRayStatus[2] != DNERayStatus._ERS_NoIntersection)
                {
                    if (arrRayStatus[2] == DNERayStatus._ERS_HorizonPoint)
                        horizonIndexes.Add(4);
                    points.Add(arrCenterAndCorners[2]);
                }
                // Top left - 1
                if (arrRayStatus[1] != DNERayStatus._ERS_NoIntersection)
                {
                    if (arrRayStatus[1] == DNERayStatus._ERS_HorizonPoint)
                        horizonIndexes.Add(5);
                    points.Add(arrCenterAndCorners[1]);
                }

            }
            IDNMcMapViewport mapViewport = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport;

            if (mapViewport.OverlayManager == null)
            {
                MessageBox.Show("Missing Overlay Manager");
                return;
            }
            if(Manager_MCOverlayManager.ActiveOverlay == null)
            {
                MessageBox.Show("Missing Overlay");
                return;
            }
            if (points.Count > 0)
            {
                try
                {
                    IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                    DNSMcVector3D[] locationPoints = points.ToArray();

                    IDNMcPolygonItem polygonItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_WORLD | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                    DNELineStyle._ELS_SOLID,
                                                                                    DNSMcBColor.bcBlackOpaque,
                                                                                    2f,
                                                                                    null,
                                                                                    new DNSMcFVector2D(0, -1),
                                                                                    1f,
                                                                                    DNEFillStyle._EFS_SOLID,
                                                                                    new DNSMcBColor(238, 130, 238, 100),   //135, 206, 250
                                                                                    null,
                                                                                    new DNSMcFVector2D(0, 0));

                    IDNMcObject polygonObj = DNMcObject.Create(activeOverlay,
                                                        polygonItem,
                                                        DNEMcPointCoordSystem._EPCS_WORLD,
                                                        locationPoints,
                                                        false);


                    polygonObj.SetVisibilityOption(DNEActionOptions._EAO_FORCE_FALSE);
                    polygonObj.SetVisibilityOption(DNEActionOptions._EAO_FORCE_TRUE, Managers.MapWorld.MCTMapFormManager.MapForm.Viewport);

                    MainForm.CameraCornersAndCenterObject = polygonObj;
                 
                    foreach (int index in horizonIndexes)
                    {
                        DNSMcVector3D horizonPoint = points[index];
                        IDNMcTexture m_DefaultTexture = null;
                        try
                        {
                            m_DefaultTexture = DNMcBitmapHandleTexture.Create(MCTester.Icons.horizon_down.GetHbitmap(), false, false, new DNSMcBColor(0, 0, 0, 255));

                            IDNMcPictureItem picItem = DNMcPictureItem.Create(DNEItemSubTypeFlags._EISTF_SCREEN, DNEMcPointCoordSystem._EPCS_SCREEN, m_DefaultTexture);
                            picItem.SetDrawPriorityGroup(DNEDrawPriorityGroup._EDPG_TOP_MOST, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            picItem.SetRectAlignment(DNEBoundingRectanglePoint._EBRP_BOTTOM_MIDDLE, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, false);
                            picItem.Connect(polygonItem);
                            picItem.SetAttachPointType(0, DNEAttachPointType._EAPT_INDEX_POINTS, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID);
                            picItem.SetNumAttachPoints(0, 1, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, 0);
                            picItem.SetAttachPointIndex(0, index, (uint)DNEPredefinedPropertyIDs._EPPI_SHARED_PROPERTY_ID, 0);
                        }
                        catch (MapCoreException McEx)
                        {
                            MapCore.Common.Utilities.ShowErrorMessage("DNMcObject.Create", McEx);
                        }
                    }
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Failed in creating polygon item", McEx);
                }
            }
        }

        public void OnTerrainHeightResults(bool bHeightFound, double dHeight, DNSMcVector3D? pNormal)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightsAlongLineResults(DNSMcVector3D[] aPointsWithHeights, float[] afSlopes, DNSSlopesData? pSlopesData)
        {
            throw new NotImplementedException();
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? pHighestPoint, DNSMcVector3D? pLowestPoint)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightMatrixResults(double[] adHeightMatrix)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainAnglesResults(double dPitch, double dRoll)
        {
            throw new NotImplementedException();
        }

        public void OnRayIntersectionResults(bool bIntersectionFound, DNSMcVector3D? pIntersection, DNSMcVector3D? pNormal, double? pdDistance)
        {
            throw new NotImplementedException();
        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            if (aIntersections != null && aIntersections.Length == 5)
            {
                DNSMcVector3D[] arrCenterAndCorners = new DNSMcVector3D[5];
                DNERayStatus[] arrRayStatus = new DNERayStatus[5];
                for (int i = 0; i < 5; i++)
                {
                    arrCenterAndCorners[i] = aIntersections[i].IntersectionPoint;
                    ///		- IMcSpatialQueries::EITT_NONE in case of IMcFrameIC::ERS_NoIntersection,
                    ///		- IMcSpatialQueries::EITT_DTM_LAYER in case of IMcFrameIC::ERS_Intersection, 
                    ///		- IMcSpatialQueries::EITT_STATIC_OBJECTS_LAYER in case of IMcFrameIC::ERS_HorizonPoint,
                    switch (aIntersections[i].eTargetType)
                    {
                        case DNEIntersectionTargetType._EITT_DTM_LAYER:
                            arrRayStatus[i] = DNERayStatus._ERS_Intersection;
                            break;
                        case DNEIntersectionTargetType._EITT_STATIC_OBJECTS_LAYER:
                            arrRayStatus[i] = DNERayStatus._ERS_HorizonPoint;
                            break;
                        default:
                            arrRayStatus[i] = DNERayStatus._ERS_NoIntersection;
                            break;
                    }
                }
                GetCameraCornersAndCenter(arrCenterAndCorners, arrRayStatus);
            }
        }

        public void OnLineOfSightResults(DNSLineOfSightPoint[] aPoints, double dCrestClearanceAngle, double dCrestClearanceDistance)
        {
            throw new NotImplementedException();
        }

        public void OnPointVisibilityResults(bool bIsTargetVisible, double? pdMinimalTargetHeightForVisibility, double? pdMinimalScouterHeightForVisibility)
        {
            throw new NotImplementedException();
        }

        public void OnAreaOfSightResults(IDNAreaOfSight pAreaOfSight, DNSLineOfSightPoint[][] aLinesOfSight, DNSPolygonsOfSight? pSeenPolygons, DNSPolygonsOfSight? pUnseenPolygons, DNSStaticObjectsIDs[] aSeenStaticObjects)
        {
            throw new NotImplementedException();
        }

        public void OnBestScoutersLocationsResults(DNSMcVector3D[] aScouters)
        {
            throw new NotImplementedException();
        }

        public void OnLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D Target)
        {
            throw new NotImplementedException();
        }

        public void OnDtmLayerTileGeometryByKeyResults(DNSTileGeometry TileGeometry)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerTileBitmapByKeyResults(DNEPixelFormat eBitmapPixelFormat, bool bBitmapFromTopToBottom, DNSMcSize BitmapSize, DNSMcSize BitmapMargins, Byte[] aBitmapBits)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerColorByPointResults(DNSMcBColor Color)
        {
            throw new NotImplementedException();
        }

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aTraversabilitySegments)
        {
            throw new NotImplementedException();
        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            throw new NotImplementedException();
        }
    }
}
