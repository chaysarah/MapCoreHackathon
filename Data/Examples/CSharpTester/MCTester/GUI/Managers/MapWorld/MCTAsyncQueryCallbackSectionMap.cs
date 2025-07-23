using MapCore;
using MCTester.Automation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.Managers.MapWorld
{
    public class MCTAsyncQueryCallbackSectionMap : IDNAsyncQueryCallback
    {
        ICreateSectionMap m_CreateSectionMap;
       /* List<IDNMcMapTerrain> m_Terrains;
        DNSMcVector3D[] m_SectionRoutePoints;
        DNSCreateDataMV m_CreateData;*/

        /*public DNSCreateDataMV CreateData { set { m_CreateData = value; } }*/

        public MCTAsyncQueryCallbackSectionMap(ICreateSectionMap createSectionMap/*, List<IDNMcMapTerrain> terrains, DNSMcVector3D[] apSectionRoutePoints*/)
        {
            m_CreateSectionMap = createSectionMap;
           /* m_Terrains = terrains;
            m_SectionRoutePoints = apSectionRoutePoints;*/
        }

        public void OnAreaOfSightResults(IDNAreaOfSight pAreaOfSight, DNSLineOfSightPoint[][] aLinesOfSight, DNSPolygonsOfSight? pSeenPolygons, DNSPolygonsOfSight? pUnseenPolygons, DNSStaticObjectsIDs[] aSeenStaticObjects)
        {
            throw new NotImplementedException();
        }

        public void OnBestScoutersLocationsResults(DNSMcVector3D[] aScouters)
        {
            throw new NotImplementedException();
        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            MessageBox.Show("Function: OnTerrainHeightsAlongLine, Error: " + IDNMcErrors.ErrorCodeToString(eErrorCode),
                     "Error From Async Query Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        public void OnRayIntersectionResults(bool bIntersectionFound, DNSMcVector3D? pIntersection, DNSMcVector3D? pNormal, double? pdDistance)
        {
            throw new NotImplementedException();
        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            throw new NotImplementedException();
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
            m_CreateSectionMap.CreateSectionMap(true, aPointsWithHeights);
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

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aPoints)
        {
            // :::???!!!::: implement...
        }

        public void OnRasterLayerColorByPointResults(DNSMcBColor Color)
        {
            // :::???!!!::: implement...
        }
    }

}
