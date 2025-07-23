using MapCore;
using MapCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    public class MCTAsyncQueryCallback : IDNAsyncQueryCallback
    {

        public static void MoveToCenterPoint(DNSMcVector3D centerPoint, IDNMcMapViewport mapCurrViewport )
        {
            try
            {
                if (mapCurrViewport.MapType == DNEMapType._EMT_2D)
                    mapCurrViewport.SetCameraPosition(centerPoint, false);
                else
                {
                    DNSQueryParams queryParams = new DNSQueryParams();
                    queryParams.eTerrainPrecision = DNEQueryPrecision._EQP_HIGHEST;
                    queryParams.pAsyncQueryCallback = new MCTAsyncQueryCallback(centerPoint, mapCurrViewport);
                    bool pbHeightFound = false;
                    double pHeight = 0;
                    DNMcNullableOut<DNSMcVector3D> normal = new DNMcNullableOut<DNSMcVector3D>();

                    mapCurrViewport.GetTerrainHeight(centerPoint, out pbHeightFound, out pHeight, normal, queryParams);
                }
            }
            catch (MapCoreException McEx)
            {
                Utilities.ShowErrorMessage("GetTerrainHeight", McEx);
            }
        }

        DNSMcVector3D mCenterPoint;
        IDNMcMapViewport mCurrVP;

        public MCTAsyncQueryCallback(DNSMcVector3D centerPoint, IDNMcMapViewport currVP) : this(currVP)
        {
            mCenterPoint = centerPoint;
        }

        public MCTAsyncQueryCallback(IDNMcMapViewport currVP)
        {
            mCurrVP = currVP;
        }

        public void OnTerrainHeightResults(bool bHeightFound, double dHeight, DNSMcVector3D? pNormal)
        {
            if (bHeightFound)
            {
                try
                {
                    mCenterPoint.z = dHeight + 100;
                    mCurrVP.SetCameraPosition(mCenterPoint, false);
                    mCurrVP.SetCameraClipDistances(1, 0, true);
                    mCurrVP.SetCameraOrientation(0, -90, 0, false);

                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(mCurrVP);
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("SetCameraPosition", McEx);
                }
            }
        }

        public void OnTerrainHeightsAlongLineResults(DNSMcVector3D[] aPointsWithHeights, float[] afSlopes, DNSSlopesData? pSlopesData)
        {
            throw new NotImplementedException();
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? pHighestPoint, DNSMcVector3D? pLowestPoint)
        {
            float minHeight = -500, maxHeight = 500;

            if (mCurrVP != null)
            {
                if (bPointsFound)
                {
                    minHeight = (float)pLowestPoint.Value.z;
                    maxHeight = (float)pHighestPoint.Value.z;
                    if (maxHeight <= minHeight + 1)
                    {
                        maxHeight = minHeight + 1;
                    }
                }

                try
                {
                    DNSDtmVisualizationParams DtmVisualizationParams;
                    bool isEnabled;
                    mCurrVP.GetDtmVisualization(out isEnabled, out DtmVisualizationParams);
                    if (!isEnabled)
                    {
                        DtmVisualizationParams = new DNSDtmVisualizationParams();
                        if (bPointsFound)
                            DtmVisualizationParams.SetDefaultHeightColors(minHeight, maxHeight);
                        else
                            DtmVisualizationParams.SetDefaultHeightColors();

                        DtmVisualizationParams.bDtmVisualizationAboveRaster = true;
                        DtmVisualizationParams.uHeightColorsTransparency = 128;
                        DtmVisualizationParams.uShadingTransparency = 255;

                        mCurrVP.SetDtmVisualization(true, DtmVisualizationParams);
                    }

                    Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(mCurrVP);

                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("GetDtmVisualization", McEx);
                }
            }
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
            // :::???!!!::: implement...
        }

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aPoints)
        {
            // :::???!!!::: implement...
        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            MessageBox.Show("Function: OnTerrainHeights, Error: " + IDNMcErrors.ErrorCodeToString(eErrorCode),
                    "Error From Async Query Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
