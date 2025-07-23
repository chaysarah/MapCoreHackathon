using MapCore;
using MapCore.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.MapWorld
{
    public static class MCTMapClick
    {

       /* public static void ConvertMousePointToScreenWorldImageLocation(IDNMcMapViewport Viewport, Point mousePoint, out DNSMcVector3D screenLocation, out DNSMcVector3D worldLocation, out DNSMcVector3D imageLocation, DNEMcPointCoordSystem locationCoordSystem = DNEMcPointCoordSystem._EPCS_WORLD)
        {
            screenLocation = new DNSMcVector3D(mousePoint.X, mousePoint.Y, 0);

            bool isIntersect;
            worldLocation = new DNSMcVector3D();
            imageLocation = new DNSMcVector3D();

            Viewport.ScreenToWorldOnTerrain(screenLocation, out worldLocation, out isIntersect);

            if (isIntersect == false)
                Viewport.ScreenToWorldOnPlane(screenLocation, out worldLocation, out isIntersect);
            if (isIntersect == false)
            {
                worldLocation.x = 0;
                worldLocation.y = 0;
                worldLocation.z = 0;
            }

            bool isUseCallback = false;

            if (Viewport.GetImageCalc() != null)
            {
                try
                {
                    imageLocation = worldLocation;
                    imageLocation.z = 0;

                    if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_WORLD)
                    {
                        isUseCallback = true;
                        bool isDTM;
                        DNEMcErrorCode errorCode;

                        Viewport.GetImageCalc().ImagePixelToCoordWorld(new DNSMcVector2D(imageLocation.x, imageLocation.y), out isDTM, out errorCode, this);

                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("ImagePixelToCoordWorld", McEx);
                }
            }
        }
*/

        public static void ConvertScreenLocationToObjectLocation(IDNMcMapViewport Viewport, DNSMcVector3D screenPoint, DNEMcPointCoordSystem locationCoordSystem, IDNMcImageCalc objectImageCalc, IOnMapClick onMapClick, bool isRelativeToDTM)
        {
            bool isIntersect;
            DNSMcVector3D cameraPoint = new DNSMcVector3D();

            Viewport.ScreenToWorldOnTerrain(screenPoint, out cameraPoint, out isIntersect);

            if (isIntersect == false)
                Viewport.ScreenToWorldOnPlane(screenPoint, out cameraPoint, out isIntersect);
            if (isIntersect == false)
            {
                cameraPoint.x = 0;
                cameraPoint.y = 0;
                cameraPoint.z = 0;
            }

            ConvertCameraLocationToObjectLocation(Viewport, cameraPoint, locationCoordSystem, objectImageCalc, onMapClick, isRelativeToDTM);
        }

        public static void ConvertCameraLocationToObjectLocation(IDNMcMapViewport Viewport, DNSMcVector3D cameraPoint, DNEMcPointCoordSystem locationCoordSystem, IDNMcImageCalc objectImageCalc, IOnMapClick onMapClick, bool isRelativeToDTM)
        {
            DNSMcVector3D locationPoint = new DNSMcVector3D();

            bool isUseCallback = false;

            if (Viewport != null && locationCoordSystem == DNEMcPointCoordSystem._EPCS_SCREEN)
            {
                locationPoint = Viewport.WorldToScreen(cameraPoint);
            }
            else if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_WORLD)
            {
                if (Viewport != null && Viewport.GetImageCalc() == null)   
                    locationPoint = Viewport.ViewportToOverlayManagerWorld(cameraPoint, Viewport.OverlayManager);
                else
                {
                    isUseCallback = true;
                    // ic can be in server - need to support async operation
                    DNSMcVector2D imagePoint = new DNSMcVector2D(cameraPoint.x, cameraPoint.y);
                    MCTMapClickAsyncQueryCallback clickAsyncQueryCallback = new MCTMapClickAsyncQueryCallback(
                        MCTMapClickAsyncQueryCallback.MCTSourceFunction.ConvertCameraLocationToObjectLocation,
                        Viewport,
                        imagePoint,
                        null,
                        onMapClick,
                        isRelativeToDTM);

                    Viewport.OverlayManager.ConvertImageToWorld(imagePoint, Viewport.GetImageCalc(), null, clickAsyncQueryCallback);
                }
            }
            else if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_IMAGE)
            {
                if (objectImageCalc != null)   // convert to object ic
                {
                    if (Viewport.GetImageCalc() == null)  // vp is reg and obj is ic - need to convert from vp world to ic image
                    {
                        DNSMcVector3D worldPoint = Viewport.ViewportToImageCalcWorld(cameraPoint, objectImageCalc); // convert world vp to world ic of obj , only do convert coord sys
                        locationPoint = objectImageCalc.WorldCoordToImagePixel(worldPoint);
                    }
                    else                                 // vp is ic and obj is ic - need to convert from vp image to ic image if not equal
                    {
                        if (objectImageCalc.IsEqual(Viewport.GetImageCalc()))
                            locationPoint = cameraPoint;
                        else                                            // ic of vp != ic of object
                        {
                            isUseCallback = true;

                            DNSMcVector2D imagePoint = new DNSMcVector2D(cameraPoint.x, cameraPoint.y);
                            MCTMapClickAsyncQueryCallback clickAsyncQueryCallback = new MCTMapClickAsyncQueryCallback(
                                MCTMapClickAsyncQueryCallback.MCTSourceFunction.ConvertCameraLocationToObjectLocation, 
                                Viewport, 
                                imagePoint, 
                                objectImageCalc, 
                                onMapClick,
                                isRelativeToDTM);

                            DNMcNullableOut<bool> isDTM = new DNMcNullableOut<bool>();
                            
                            /* move to callback DNSMcVector3D worldPoint = */
                            Viewport.GetImageCalc().ImagePixelToCoordWorld(imagePoint, isDTM, null, clickAsyncQueryCallback);
                            /*
                             * move to callback
                            worldPoint = Viewport.ViewportToImageCalcWorld(worldPoint, objectImageCalc);
                            locationPoint = objectImageCalc.WorldCoordToImagePixel(worldPoint);
                            */
                        }
                    }
                }
            }
            if (!isUseCallback)
            {
                onMapClick.OnMapClick(locationPoint, locationPoint, locationCoordSystem, isRelativeToDTM);
            }
        }


        public static void ConvertImageToWorld(IDNMcOverlayManager mcOverlayManager, DNSMcVector3D imagePoint, IDNMcImageCalc imageCalc, IOnMapClick onMapClick, bool requestSeparateIntersectionStatus)
        {
            // ic can be in server - need to support async operation
            MCTMapClickAsyncQueryCallback clickAsyncQueryCallback = new MCTMapClickAsyncQueryCallback(
                MCTMapClickAsyncQueryCallback.MCTSourceFunction.ConvertImageToWorld,
                null,
                imagePoint,
                null,
                onMapClick,
                false);

            DNMcNullableOut<DNEMcErrorCode> peIntersectionStatus = new DNMcNullableOut<DNEMcErrorCode>();

            mcOverlayManager.ConvertImageToWorld(imagePoint,
                imageCalc,
                requestSeparateIntersectionStatus ? peIntersectionStatus : null,
                clickAsyncQueryCallback);

        }


        public static void ConvertObjectLocationToCameraLocation(IDNMcMapViewport Viewport, DNSMcVector3D objectPoint, IDNMcObject mcObject, uint locationIndex, bool isNeedToRenderVp)
        {
            DNEMcPointCoordSystem locationCoordSystem;
            IDNMcObjectLocation objLocation = mcObject.GetScheme().GetObjectLocation(locationIndex);
            objLocation.GetCoordSystem(out locationCoordSystem);

            DNSMcVector3D cameraPoint = new DNSMcVector3D();
            bool isConvert = true;
            IDNMcImageCalc mcViewportImageCalc = Viewport.GetImageCalc();
            bool isUseCallback = false;

            if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_WORLD)
            {
                // if viewport is regular do this
                if (mcViewportImageCalc == null)
                    cameraPoint = Viewport.OverlayManagerToViewportWorld(objectPoint);
                else // else viewport is image so do om convert worldtoimage
                {
                    if (Viewport.OverlayManager != null && objectPoint != null)
                    {
                        cameraPoint = Viewport.OverlayManager.ConvertWorldToImage(objectPoint, mcViewportImageCalc, null);
                    }
                    else
                        isConvert = false;
                }
            }
            else if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_SCREEN)
            {
                bool isIntersect;
                Viewport.ScreenToWorldOnTerrain(objectPoint, out cameraPoint, out isIntersect);
                if (isIntersect == false)
                    Viewport.ScreenToWorldOnPlane(objectPoint, out cameraPoint, out isIntersect);
                if (isIntersect == false)
                {
                    cameraPoint.x = 0;
                    cameraPoint.y = 0;
                    cameraPoint.z = 0;
                }
            }
            else if (locationCoordSystem == DNEMcPointCoordSystem._EPCS_IMAGE)
            {
                try
                {
                    IDNMcImageCalc objImageCalc = mcObject.GetImageCalc();
                    if (objImageCalc == null)
                    {
                        isConvert = false;
                    }
                    // need to convert points from imagecalc to world viewport.
                    else if (mcViewportImageCalc != null || !objImageCalc.IsEqual(mcViewportImageCalc))
                    {
                        DNMcNullableOut<bool> isDTM = new DNMcNullableOut<bool>();

                        isUseCallback = true;
                        MCTMapClickAsyncQueryCallback clickAsyncQueryCallback = new MCTMapClickAsyncQueryCallback(
                            MCTMapClickAsyncQueryCallback.MCTSourceFunction.ConvertObjectLocationToCameraLocation,
                            Viewport,
                            cameraPoint,
                            objImageCalc,
                            objLocation,
                            isNeedToRenderVp);

                        cameraPoint = objImageCalc.ImagePixelToCoordWorld(new DNSMcVector2D(objectPoint), isDTM, null, clickAsyncQueryCallback);
                        /* move to callback 
                           cameraPoint = Viewport.ImageCalcWorldToViewport(cameraPoint, objImageCalc);

                           // if image calc of viewport is diff from image calc of object  ( not is equal return true)
                           if (mcViewportImageCalc != null)
                           {
                               cameraPoint = mcViewportImageCalc.WorldCoordToImagePixel(cameraPoint);
                           }*/
                    }
                    else
                    {
                        cameraPoint = objectPoint;
                    }
                }
                catch (MapCoreException McEx)
                {
                    Utilities.ShowErrorMessage("ImagePixelToCoordWorld/ImageCalcWorldToViewport", McEx);
                    isConvert = false;
                }
            }

            if (!isUseCallback && isConvert)
            {
                ConvertObjectLocationToCameraLocationResult(Viewport, cameraPoint, objLocation, isNeedToRenderVp);
            }
        }
        
        public static void ConvertObjectLocationToCameraLocationResult(IDNMcMapViewport Viewport, DNSMcVector3D cameraPoint ,IDNMcObjectLocation objLocation, bool isNeedToRenderVp)
        {
            bool ifFoundHeight = true;
            if (Viewport.MapType == DNEMapType._EMT_2D)
            {
                Viewport.SetCameraPosition(cameraPoint, false);
            }
            else if (Viewport.MapType == DNEMapType._EMT_3D)
            {
                bool isRelative = false;
                uint PropId;
                objLocation.GetRelativeToDTM(out isRelative, out PropId);

                if (isRelative == true)
                {
                    bool pbHeightFound = false;
                    double height;
                    DNMcNullableOut<DNSMcVector3D> normal = null;

                    Viewport.GetTerrainHeight(cameraPoint, out pbHeightFound, out height, normal);
                    ifFoundHeight = pbHeightFound;
                    if (ifFoundHeight)
                        cameraPoint.z = height;
                    else
                        cameraPoint.z = 0;
                }
                
                cameraPoint.z = cameraPoint.z + 500;
                Viewport.SetCameraPosition(cameraPoint, false);
                Viewport.SetCameraOrientation(0, -90, 0, false);
                
            }
            if (ifFoundHeight && isNeedToRenderVp)
            {
                // turn on all viewports render needed flags
                MCTester.Managers.MapWorld.Manager_MCViewports.TurnOnRelevantViewportsRenderingFlags(Viewport);
            }
        }
    }

    public class MCTMapClickAsyncQueryCallback : IDNAsyncQueryCallback
    {
        public enum MCTSourceFunction
        {
            ConvertMousePointToScreenWorldImageLocation,
            ConvertScreenLocationToObjectLocation,
            ConvertCameraLocationToObjectLocation,
            ConvertObjectLocationToCameraLocation,
            ConvertImageToWorld
        }
        MCTSourceFunction m_sourceFunction;
        IDNMcMapViewport m_Viewport;
        DNSMcVector3D m_location;
        IOnMapClick m_onMapClick;
        IDNMcImageCalc m_objectImageCalc;
        DNEMcPointCoordSystem m_locationCoordSystem = DNEMcPointCoordSystem._EPCS_IMAGE;
        private IDNMcObjectLocation m_objLocation;
        private bool m_isNeedToRenderVp;
        private bool m_isRelativeToDTM = false;

        public MCTMapClickAsyncQueryCallback(MCTSourceFunction sourceFunction, IDNMcMapViewport viewport, DNSMcVector3D screenLocation, IDNMcImageCalc objectImageCalc, IOnMapClick onMapClick, bool isRelativeToDTM)
        {
            m_sourceFunction = sourceFunction;
            m_Viewport = viewport;
            m_location = screenLocation;
            m_objectImageCalc = objectImageCalc;
            m_onMapClick = onMapClick;
            m_isRelativeToDTM = isRelativeToDTM;
        }

        public MCTMapClickAsyncQueryCallback(MCTSourceFunction convertObjectLocationToCameraLocation, IDNMcMapViewport viewport, DNSMcVector3D cameraPoint, IDNMcImageCalc objImageCalc, IDNMcObjectLocation objLocation, bool isNeedToRenderVp)
        {
            m_sourceFunction = convertObjectLocationToCameraLocation;
            m_Viewport = viewport;
            m_location = cameraPoint;
            m_objectImageCalc = objImageCalc;
            m_objLocation = objLocation;
            m_isNeedToRenderVp = isNeedToRenderVp;
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
            if (bIntersectionFound &&
              pIntersection != null &&
              pIntersection.HasValue)
            {
                if (m_sourceFunction == MCTSourceFunction.ConvertCameraLocationToObjectLocation)
                {
                    DNSMcVector3D locationPoint = new DNSMcVector3D();
                    if (m_objectImageCalc != null &&  m_Viewport != null)
                    {
                        DNSMcVector3D worldPoint = m_Viewport.ViewportToImageCalcWorld(pIntersection.Value, m_objectImageCalc);
                        locationPoint = m_objectImageCalc.WorldCoordToImagePixel(worldPoint);
                        m_onMapClick.OnMapClick(m_location, locationPoint, m_locationCoordSystem, m_isRelativeToDTM);
                    }
                    else
                    {
                        m_onMapClick.OnMapClick(m_location, pIntersection.Value, m_locationCoordSystem, m_isRelativeToDTM);
                    }
                }
                else if (m_sourceFunction == MCTSourceFunction.ConvertImageToWorld)
                {
                    m_onMapClick.OnMapClick(m_location, pIntersection.Value, m_locationCoordSystem, m_isRelativeToDTM);
                }
                else if (m_sourceFunction == MCTSourceFunction.ConvertObjectLocationToCameraLocation)
                {
                    DNSMcVector3D cameraPoint = m_Viewport.ImageCalcWorldToViewport(pIntersection.Value, m_objectImageCalc);

                    // if image calc of viewport is diff from image calc of object  ( not is equal return true)
                    if (m_Viewport.GetImageCalc() != null)
                    {
                        cameraPoint = m_Viewport.GetImageCalc().WorldCoordToImagePixel(cameraPoint);
                    }

                    MCTMapClick.ConvertObjectLocationToCameraLocationResult(m_Viewport, cameraPoint, m_objLocation, m_isNeedToRenderVp);
                }
            }
            else
                m_onMapClick.OnMapClickError(DNEMcErrorCode.FAILURE, m_sourceFunction.ToString());
        }

        public void OnRayIntersectionTargetsResults(DNSTargetFound[] aIntersections)
        {
            throw new NotImplementedException();
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

        public void OnRasterLayerTileBitmapByKeyResults(DNEPixelFormat eBitmapPixelFormat, bool bBitmapFromTopToBottom, DNSMcSize BitmapSize, DNSMcSize BitmapMargins, byte[] aBitmapBits)
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
            if(m_onMapClick != null)
                m_onMapClick.OnMapClickError(eErrorCode, m_sourceFunction.ToString());
        }
    }
}
