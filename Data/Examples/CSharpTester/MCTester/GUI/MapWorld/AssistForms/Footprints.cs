using System;
using System.Collections.Generic;
using System.Text;
using MapCore;
using MCTester.GUI.Map;
using UnmanagedWrapper;
using System.Windows.Forms;

namespace MCTester
{
    public class Footprints : IDNAsyncQueryCallback
    {
        public void DrawCameraFootprint(bool isAsync)
        {
            if (MCTMapForm.mcFootprintObject != null)
            {
                MCTMapForm.mcFootprintObject.Dispose();
                MCTMapForm.mcFootprintObject.Remove();
                MCTMapForm.mcFootprintObject = null;
            }
            else
            {
                IDNMcMapCamera mapCamera = MCTester.Managers.MapWorld.MCTMapFormManager.MapForm.Viewport.ActiveCamera;
                IDNAsyncQueryCallback asyncQueryCallback = isAsync ? this : null;
                try
                {
                    DNSCameraFootprintPoints FootprintPoints = mapCamera.GetCameraFootprint(asyncQueryCallback);
                    if(!isAsync)
                        FootprintCallback(FootprintPoints);
                }
                catch (MapCoreException McEx)
                {
                    MapCore.Common.Utilities.ShowErrorMessage("Failed in creating polygon item", McEx);
                }
            }
        }

        public void FootprintCallback(DNSCameraFootprintPoints FootprintPoints)
        {
            List<DNSMcVector3D> FootprintList = new List<DNSMcVector3D>();

            if (FootprintPoints.bCenterFound)
            {
                FootprintList.Add(FootprintPoints.Center);
            }
            if (FootprintPoints.bLowerLeftFound)
            {
                FootprintList.Add(FootprintPoints.LowerLeft);
            }
            if (FootprintPoints.bLowerRightFound)
            {
                FootprintList.Add(FootprintPoints.LowerRight);
            }
            if (FootprintPoints.bCenterFound)
            {
                FootprintList.Add(FootprintPoints.Center);
            }
            if (FootprintPoints.bUpperRightFound)
            {
                FootprintList.Add(FootprintPoints.UpperRight);
            }
            if (FootprintPoints.bUpperLeftFound)
            {
                FootprintList.Add(FootprintPoints.UpperLeft);
            }

            try
            {
                IDNMcOverlay activeOverlay = MCTester.Managers.ObjectWorld.Manager_MCOverlayManager.ActiveOverlay;
                DNSMcVector3D[] locationPoints = FootprintList.ToArray();


                IDNMcObjectSchemeItem ObjSchemeItem = DNMcPolygonItem.Create(DNEItemSubTypeFlags._EISTF_WORLD | DNEItemSubTypeFlags._EISTF_ATTACHED_TO_TERRAIN,
                                                                                DNELineStyle._ELS_SOLID,
                                                                                DNSMcBColor.bcBlackOpaque,
                                                                                2f,
                                                                                null,
                                                                                new DNSMcFVector2D(0, -1),
                                                                                1f,
                                                                                DNEFillStyle._EFS_SOLID,
                                                                                new DNSMcBColor(0, 255, 0, 100),
                                                                                null,
                                                                                new DNSMcFVector2D(0, 0));

                IDNMcObject obj = DNMcObject.Create(activeOverlay,
                                                    ObjSchemeItem,
                                                    DNEMcPointCoordSystem._EPCS_WORLD,
                                                    locationPoints,
                                                    false);


                obj.SetVisibilityOption(DNEActionOptions._EAO_FORCE_FALSE);
                obj.SetVisibilityOption(DNEActionOptions._EAO_FORCE_TRUE, Managers.MapWorld.MCTMapFormManager.MapForm.Viewport);

              /*  IDNMcViewportConditionalSelector viewportConditionalSelector = DNMcViewportConditionalSelector.Create(activeOverlay.GetOverlayManager());
                uint[] ids = new uint[1] { Managers.MapWorld.MCTMapFormManager.MapForm.Viewport.ViewportID };
                
                viewportConditionalSelector.SetSpecificViewports(ids, true);
                obj.SetConditionalSelector(DNEActionType._EAT_VISIBILITY, true, viewportConditionalSelector);*/

                MCTMapForm.mcFootprintObject = obj;
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("Failed in creating polygon item", McEx);
            }
        }

        public void OnAreaOfSightResults(IDNAreaOfSight pAreaOfSight, DNSLineOfSightPoint[][] aLinesOfSight, DNSPolygonsOfSight? pSeenPolygons, DNSPolygonsOfSight? pUnseenPolygons, DNSStaticObjectsIDs[] aSeenStaticObjects)
        {
            throw new NotImplementedException();
        }

        public void OnBestScoutersLocationsResults(DNSMcVector3D[] aScouters)
        {
            throw new NotImplementedException();
        }

        public void OnDtmLayerTileGeometryByKeyResults(DNSTileGeometry TileGeometry)
        {
            throw new NotImplementedException();
        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            MessageBox.Show(IDNMcErrors.ErrorCodeToString(eErrorCode), "Error Get Footprint Points", MessageBoxButtons.OK);
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? pHighestPoint, DNSMcVector3D? pLowestPoint)
        {
            throw new NotImplementedException();
        }

        public void OnLineOfSightResults(DNSLineOfSightPoint[] aPoints, double dCrestClearanceAngle, double dCrestClearanceDistance)
        {
            throw new NotImplementedException();
        }

        public void OnLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D Target)
        {
            throw new NotImplementedException();
        }

        public void OnPointVisibilityResults(bool bIsTargetVisible, double? pdMinimalTargetHeightForVisibility, double? pdMinimalScouterHeightForVisibility)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerColorByPointResults(DNSMcBColor Color)
        {
            throw new NotImplementedException();
        }

        public void OnRasterLayerTileBitmapByKeyResults(DNEPixelFormat eBitmapPixelFormat, bool bBitmapFromTopToBottom, DNSMcSize BitmapSize, DNSMcSize BitmapMargins, byte[] aBitmapBits)
        {
            throw new NotImplementedException();
        }

        public void OnRayIntersectionResults(bool bIntersectionFound, DNSMcVector3D? pIntersection, DNSMcVector3D? pNormal, double? pdDistance)
        {
            throw new NotImplementedException();
        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            /*
                UpperLeft;    
                UpperRight;   
                LowerRight;   
                LowerLeft;    
                Center;
                .*/

            DNSCameraFootprintPoints cameraFootprintPoints = new DNSCameraFootprintPoints();
            if (aIntersections != null && aIntersections.Length == 5)
            {
                 cameraFootprintPoints.bUpperLeftFound = aIntersections[0].eTargetType != DNEIntersectionTargetType._EITT_NONE;
                if (cameraFootprintPoints.bUpperLeftFound)
                    cameraFootprintPoints.UpperLeft = aIntersections[0].IntersectionPoint;

                cameraFootprintPoints.bUpperRightFound = aIntersections[1].eTargetType != DNEIntersectionTargetType._EITT_NONE;
                if (cameraFootprintPoints.bUpperRightFound)
                    cameraFootprintPoints.UpperRight = aIntersections[1].IntersectionPoint;

                cameraFootprintPoints.bLowerRightFound = aIntersections[2].eTargetType != DNEIntersectionTargetType._EITT_NONE;
                if (cameraFootprintPoints.bLowerRightFound)
                    cameraFootprintPoints.LowerRight = aIntersections[2].IntersectionPoint;

                cameraFootprintPoints.bLowerLeftFound = aIntersections[3].eTargetType != DNEIntersectionTargetType._EITT_NONE;
                if (cameraFootprintPoints.bLowerLeftFound)
                    cameraFootprintPoints.LowerLeft = aIntersections[3].IntersectionPoint;

                cameraFootprintPoints.bCenterFound = aIntersections[4].eTargetType != DNEIntersectionTargetType._EITT_NONE;
                if (cameraFootprintPoints.bCenterFound)
                    cameraFootprintPoints.Center = aIntersections[4].IntersectionPoint;

                FootprintCallback(cameraFootprintPoints);
            }
            else
            {
                MessageBox.Show("Return Invalid Results", "Get Camera Footprint Async Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OnTerrainAnglesResults(double dPitch, double dRoll)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightMatrixResults(double[] adHeightMatrix)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightResults(bool bHeightFound, double dHeight, DNSMcVector3D? pNormal)
        {
            throw new NotImplementedException();
        }

        public void OnTerrainHeightsAlongLineResults(DNSMcVector3D[] aPointsWithHeights, float[] afSlopes, DNSSlopesData? pSlopesData)
        {
            throw new NotImplementedException();
        }

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aTraversabilitySegments)
        {
            throw new NotImplementedException();
        }
    }
}
