using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTester.Managers.MapWorld;
using UnmanagedWrapper;
using MapCore;
using MapCore.Common;
using System.Windows.Forms;

namespace MCTester.ButtonsImplementation
{
    public class btnDtmVisualization : IDNAsyncQueryCallback
    {
        IDNMcMapViewport activeVP;

        public btnDtmVisualization()
        {

        }


        public void ExecuteAction()
        {
            activeVP = MCTMapFormManager.MapForm.Viewport;

            try
            {
                DNSDtmVisualizationParams DtmVisualizationParams;
                bool isEnabled;
                activeVP.GetDtmVisualization(out isEnabled, out DtmVisualizationParams);
                if (isEnabled)
                {
                    activeVP.SetDtmVisualization(false);
                }
                else
                {
                    try
                    {
                        activeVP.GetCameraFootprint(this);
                    }
                    catch (MapCoreException McEx)
                    {
                        MapCore.Common.Utilities.ShowErrorMessage("Get min and max height of viewport", McEx);
                    }
                }
            }
            catch (MapCoreException McEx)
            {
                MapCore.Common.Utilities.ShowErrorMessage("GetDtmVisualization", McEx);
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
            throw new NotImplementedException();
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? pHighestPoint, DNSMcVector3D? pLowestPoint)
        {
           
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
            if (aIntersections != null && aIntersections.Length == 5 &&
                 aIntersections[0].eTargetType != DNEIntersectionTargetType._EITT_NONE &&
                 aIntersections[1].eTargetType != DNEIntersectionTargetType._EITT_NONE &&
                 aIntersections[2].eTargetType != DNEIntersectionTargetType._EITT_NONE &&
                 aIntersections[3].eTargetType != DNEIntersectionTargetType._EITT_NONE)
            {
                DNSMcVector3D[] points = new DNSMcVector3D[4];
                points[0] = aIntersections[0].IntersectionPoint;
                points[1] = aIntersections[1].IntersectionPoint;
                points[2] = aIntersections[2].IntersectionPoint;
                points[3] = aIntersections[3].IntersectionPoint;

                try
                {

                    DNSMcVector3D pHighestPoint = new DNSMcVector3D();
                    DNSMcVector3D pLowestPoint = new DNSMcVector3D();
                    bool pbPointsFound = false;
                    DNSQueryParams pParams = new DNSQueryParams();
                    pParams.pAsyncQueryCallback = new MCTAsyncQueryCallback(activeVP);
                    pParams.eTerrainPrecision = DNEQueryPrecision._EQP_HIGHEST;
                    activeVP.GetExtremeHeightPointsInPolygon(points, out pbPointsFound, out pHighestPoint, out pLowestPoint, pParams);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("Get min and max height of viewport", McEx);
                }
            }
            else
            {

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
