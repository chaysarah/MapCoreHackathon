using MapCore;
using MCTester.GUI.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnmanagedWrapper;

namespace MCTester.MapWorld.MapUserControls
{
    public class MCTImageCalcCallback : IDNAsyncQueryCallback
    {
        ucViewport mViewportCtrl;
        List<Object> mListInputsQuery;
        AsyncImageCalcQueryFunctionName mFunctionName;

        static List<MCTImageCalcCallback> asyncQueryResultsCallbacks = new List<MCTImageCalcCallback>();

        public static List<MCTImageCalcCallback> AsyncQueryResultsCallbacks
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

        public MCTImageCalcCallback(ucViewport viewportCtrl,
            List<Object> listInputsQuery,
            AsyncImageCalcQueryFunctionName functionName)
        {
            mViewportCtrl = viewportCtrl;
            mListInputsQuery = listInputsQuery;
            mFunctionName = functionName;

            AsyncQueryResultsCallbacks.Add(this);
        }

        public void OnTerrainHeightResults(bool bHeightFound, double dHeight, DNSMcVector3D? pNormal)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
           // listResults.Add(bHeightFound);
            listResults.Add(dHeight);
           // listResults.Add(pNormal);
            MessageBox.Show("Function: " + mFunctionName + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);

            mViewportCtrl.AddResultToList(mFunctionName, listResults, true, mListInputsQuery);
        }

        public void OnTerrainHeightsAlongLineResults(DNSMcVector3D[] aPointsWithHeights, float[] aSlopes, DNSSlopesData? pSlopesData)
        {
            RemoveAsyncQueryCallback();
            
        }

        public void OnExtremeHeightPointsInPolygonResults(bool bPointsFound, DNSMcVector3D? HighestPt, DNSMcVector3D? LowestPt)
        {
            RemoveAsyncQueryCallback();
        }

        public void OnTerrainHeightMatrixResults(double[] adHeightMatrix)
        {
            RemoveAsyncQueryCallback();
        }

        public void OnTerrainAnglesResults(double dPitch, double dRoll)
        {
            RemoveAsyncQueryCallback();
        }

        public void OnRayIntersectionResults(bool bIntersectionFound, DNSMcVector3D? pIntersection, DNSMcVector3D? pNormal, double? pdDistance)
        {
            RemoveAsyncQueryCallback();
            if (bIntersectionFound)
            {
                List<object> listResults = new List<object>();
                listResults.Add(bIntersectionFound);
                listResults.Add(pIntersection);
                listResults.Add(pdDistance);

                MessageBox.Show("Function: " + mFunctionName + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mViewportCtrl.AddResultToList(mFunctionName, listResults, true, mListInputsQuery);
            }
            else
            {
                DNEMcErrorCode eErrorCode = DNEMcErrorCode.FAILURE;
               // MessageBox.Show("Function: " + mFunctionName.ToString() + ", Error: " + IDNMcErrors.ErrorCodeToString(eErrorCode),
               //     "Error From Async Query Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);

                mViewportCtrl.AddErrorToList(mFunctionName, eErrorCode, true, mListInputsQuery, false);
            }
        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            RemoveAsyncQueryCallback();

        }

        public void OnLineOfSightResults(DNSLineOfSightPoint[] aPoints, double dCrestClearanceAngle, double dCrestClearanceDistance)
        {
            RemoveAsyncQueryCallback();

        }

        public void OnPointVisibilityResults(bool bIsTargetVisible, double? pdMinimalTargetHeightForVisibility, double? pdMinimalScouterHeightForVisibility)
        {
            RemoveAsyncQueryCallback();
            List<object> listResults = new List<object>();
            listResults.Add(bIsTargetVisible);
            
            MessageBox.Show("Function: " + mFunctionName + " Finished", "Return From Async Function", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mViewportCtrl.AddResultToList(mFunctionName, listResults, true, mListInputsQuery);
        }

        public void OnAreaOfSightResults(IDNAreaOfSight pAreaOfSight, DNSLineOfSightPoint[][] aLinesOfSight,
            DNSPolygonsOfSight? pSeenPolygons, DNSPolygonsOfSight? pUnseenPolygons, DNSStaticObjectsIDs[] aSeenStaticObjects)
        {
            RemoveAsyncQueryCallback();
        }

        public void OnBestScoutersLocationsResults(DNSMcVector3D[] aScouters)
        {
            RemoveAsyncQueryCallback();
        }

        public void OnLocationFromTwoDistancesAndAzimuthResults(DNSMcVector3D Target)
        {
            RemoveAsyncQueryCallback();
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
        }

        public void OnTraversabilityAlongLineResults(DNSTraversabilityPoint[] aPoints)
        {
            RemoveAsyncQueryCallback();

        }

        public void OnError(DNEMcErrorCode eErrorCode)
        {
            RemoveAsyncQueryCallback();

            MessageBox.Show("Function: " + mFunctionName.ToString() + ", Error: " + IDNMcErrors.ErrorCodeToString(eErrorCode),
                     "Error From Async Query Callback", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            mViewportCtrl.AddErrorToList(mFunctionName, eErrorCode, true, mListInputsQuery, false);
        }
    }

    public enum AsyncImageCalcQueryFunctionName
    {
        IsWorldCoordVisible,
        WorldCoordToImagePixel,
        WorldCoordToImagePixelWithCache,
        ImagePixelToCoordWorld,
        ImagePixelToCoordWorldWithCache,
        ImagePixelToCoordWorldOnHorzPlane,
        GetHeight
    }

}
