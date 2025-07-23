import React, { PureComponent, Component } from 'react';
import cn from './MapContainer.module.css';
import config, { mapActions } from '../../config';
import ApplicationContext from '../../context/applicationContext';
// import CreateCallbackService from '../../services/createCallbackService'

class SLayerGroup {
    constructor(coordSystemString, bShowGeoInMetricProportion, bSetTerrainBoxByStaticLayerOnly, InitialScale2D) {
        this.aLayerCreateStrings = [];
        this.aLayerParams = [];
        this.coordSystemString = coordSystemString;
        this.bShowGeoInMetricProportion = bShowGeoInMetricProportion;
        this.bSetTerrainBoxByStaticLayerOnly = bSetTerrainBoxByStaticLayerOnly;
        this.InitialScale2D = InitialScale2D;
    }
}

class SViewportData {
    constructor(_viewport, _editMode) {
        this.viewport = _viewport;
        this.editMode = _editMode;
        this.canvas = _viewport.GetWindowHandle();
        let aViewportTerrains = _viewport.GetTerrains();
        this.aLayers = (aViewportTerrains != null && aViewportTerrains.length > 0 ? aViewportTerrains[0].GetLayers() : null);
        this.terrainBox = null;
        this.terrainCenter = null;
        this.rotationCenter = null;
        this.bCameraPositionSet = false;
        this.bSetTerrainBoxByStaticLayerOnly = false;
    }
}

const WithContext = Warpped => (props) => {
    const { compRef, device } = props;
    return (

        <ApplicationContext.Consumer>
            {value => <Warpped contextValue={value} {...props} ref={compRef} device={device} />}
        </ApplicationContext.Consumer>
    )
}

class MapContainer extends PureComponent {

    static contextType = ApplicationContext;

    constructor(props) {
        super(props);
        this.state = {
            mapLayerGroups: new Map(),
            lastTerrainConfiguration: null,
            lastViewportConfiguration: null /*  2D/3D, 3D/2D, 2D, 3D */,
            bSameCanvas: true
        }
    }
    device = null
    //callbacks classes from mapCore
    CLayerReadCallback;
    CCameraUpdateCallback;
    CAsyncQueryCallback;
    CAsyncOperationCallback;
    viewportData = null;
    uCameraUpdateCounter = 0;
    aLastTerrainLayers = [];
    lastCoordSys = null;
    overlayManager = null;
    activeViewport = -1;
    aViewports = [];
    viewport;
    editMode;
    lastRenderTime = (new Date).getTime();
    lastMemUsageLogTime = (new Date).getTime();
    uMemUsageLoggingFrequency = 0;
    nMousePrevX = 0;
    nMousePrevY = 0;
    mouseDownButtons = 0;
    bEdit = false;
    requestAnimationFrameId = -1;
    layerCallback = null;
    _device;
    dZoom3DMinDistance = 10;
    dZoom3DMaxDistance = 2000000;
    fMinPitch = -90;
    fMaxPitch = 0;
    RotationCenter = null;

    componentDidMount() {
        let mapScaleFactor = JSON.parse(localStorage.getItem("mapScaleFactor"));
        this.setState({ mapScaleFactor })
        // let rawParams = new window.MapCore.IMcMapLayer.SRawParams();
        // let params = new window.MapCore.IMcSpatialQueries.SQueryParams();
        window.addEventListener('resize', this.resizeCanvases);
        //this.callGetCapabilitiesApi();
        this.openMap(this.context.mapToPreview.title)
        // .then(()=>{
        //    this.setState({loaded:true})
        //     }).catch((e)=>{
        //         console.log("error")
        //     })       
    }

    componentWillUnmount() {
        //Todo -> un-register events and all the map core object
        window.removeEventListener('resize', this.resizeCanvases)
        cancelAnimationFrame(this.requestAnimationFrameId);
        this.requestAnimationFrameId = null;
        this.closeMap();
    }

    componentDidUpdate(prevProps, prevState) {
        if (this.state.mapScaleFactor !== prevState.mapScaleFactor) {
            if (this.overlayManager) {
                this.overlayManager.SetScaleFactor(this.state.mapScaleFactor)
            }
        }
    }


    createCallbackClasses() {
        // this.CLayerReadCallback = window.MapCore.IMcMapLayer.IReadCallback.extend("IMcMapLayer.IReadCallback", {
        let CLayerReadCallback = new window.MapCore.IMcMapLayer.IReadCallback.extend("IMcMapLayer.IReadCallback",
            {
                // optional
                __construct: function (aViewports) {
                    this.__parent.__construct.call(this);
                    this.aViewports = aViewports;
                },

                // mandatory
                OnInitialized: function (pLayer, eStatus, strAdditionalDataString) {
                    if (eStatus != window.MapCore.IMcErrors.ECode.SUCCESS && eStatus != window.MapCore.IMcErrors.ECode.NATIVE_SERVER_LAYER_NOT_VALID) {
                        for (let i = 0; i < this.aViewports.length; i++) {
                            let nLayer = this.aViewports[i].aLayers.indexOf(pLayer);
                            if (nLayer >= 0) {
                                this.aViewports[i].aLayers.splice(nLayer, 1);
                            }
                        }
                        alert("Layer initialization: " + window.MapCore.IMcErrors.ErrorCodeToString(eStatus) + " (" + strAdditionalDataString + ")");
                        pLayer.RemoveLayerAsync();
                    }
                },
                // mandatory
                OnReadError: function (eErrorCode, strAdditionalDataString) {
                    alert("Layer read error: " + window.MapCore.IMcErrors.ErrorCodeToString(eErrorCode) + " (" + strAdditionalDataString + ")");
                },
                // mandatory
                OnNativeServerLayerNotValid: function (pLayer, bLayerVersionUpdated) { },

                // optional, needed if to be deleted by MapCore when no longer used
                Release: function () { this.delete(); },
            });
        this.layerCallback = new CLayerReadCallback(this.aViewports);

        this.CCameraUpdateCallback = window.MapCore.IMcMapViewport.ICameraUpdateCallback.extend("IMcMapViewport.ICameraUpdateCallback", {
            // mandatory
            OnActiveCameraUpdated: function (pViewport) {
                ++this.uCameraUpdateCounter
            },
            // optional
            Release: function () {
                this.delete()
            }
        });

        this.CAsyncQueryCallback = window.MapCore.IMcSpatialQueries.IAsyncQueryCallback.extend("IMcSpatialQueries.IAsyncQueryCallback", {
            // optional
            __construct: function (viewportData) {
                this.__parent.__construct.call(this);
                this.viewportData = viewportData;
            },

            OnTerrainHeightResults: function (bHeightFound, height, normal) {
                if (this.viewportData.viewport != null) {
                    this.viewportData.terrainCenter.z = height + 20;
                    if (this.viewportData.viewport.GetMapType() == window.MapCore.IMcMapCamera.EMapType.EMT_3D) {
                        this.viewportData.viewport.SetCameraPosition(this.viewportData.terrainCenter);
                    }
                }
                this.delete();
            },
            OnTerrainHeightMatrixResults: function (uNumHorizontalPoints, uNumVerticalPoints, adHeightMatrix) { },
            OnTerrainHeightsAlongLineResults: function (aPointsWithHeights, aSlopes, pSlopesData) { },
            OnExtremeHeightPointsInPolygonResults: function (bPointsFound, pHighestPoint, pLowestPoint) { },
            OnTerrainAnglesResults: function (dPitch, dRoll) { },

            // OnRayIntersectionResults
            OnLineOfSightResults: function (aPoints, dCrestClearanceAngle, dCrestClearanceDistance) { },
            OnPointVisibilityResults: function (bIsTargetVisible, pdMinimalTargetHeightForVisibility, pdMinimalScouterHeightForVisibility) { },
            OnAreaOfSightResults: function (pAreaOfSight, aLinesOfSight, pSeenPolygons, pUnseenPolygons, aSeenStaticObjects) { },
            OnLocationFromTwoDistancesAndAzimuthResults: function (Target) { },

            // mandatory
            OnError: function (eErrorCode) {
                alert('error ' + eErrorCode);
                this.delete();
            },
        });

        let CUserData = window.MapCore.IMcUserData.extend("IMcUserData", {
            // optional
            __construct: function (bToBeDeleted) {
                this.__parent.__construct.call(this);
                this.bToBeDeleted = bToBeDeleted;
                // ...
            },

            // optional
            __destruct: function () {
                this.__parent.__destruct.call(this);
                // ...
            },

            // mandatory
            Release: function () {
                if (this.bToBeDeleted) {
                    this.delete();
                }
            },

            // optional
            Clone: function () {
                if (this.bToBeDeleted) {
                    return new CUserData(this.bToBeDeleted);
                }
                return this;
            },
        });
        this.CAsyncOperationCallback = window.MapCore.IMcMapLayer.IAsyncOperationCallback.extend("IMcMapLayer.IAsyncOperationCallback",
            {
                // optional
                __construct: function (OnResults) {
                    this.__parent.__construct.call(this);
                    this.OnResults = OnResults;
                },

                OnWebServerLayersResults: function (eStatus, strServerURL, eWebMapServiceType, aLayers, strServiceProviderName) {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
            });
    }

    doMoveObjects() {
    }

    renderMapContinuously = () => {

        if (!this.requestAnimationFrameId) return;
        this.trySetTerainBox();
        let currtRenderTime = (new Date).getTime();

        // render viewport(s)
        if (!this.state.bSameCanvas) {
            window.MapCore.IMcMapViewport.RenderAll();
        } else if (this.viewport != null) {
            this.viewport.Render();
        }

        // move objects if they exist
        this.doMoveObjects();
        this.lastRenderTime = currtRenderTime;

        // log memory usage and heap size
        if (this.uMemUsageLoggingFrequency != 0 && currtRenderTime >= this.lastMemUsageLogTime + this.uMemUsageLoggingFrequency * 1000) {
            let usage = window.MapCore.IMcMapDevice.GetMaxMemoryUsage();
            this.lastMemUsageLogTime = currtRenderTime;
        }

        // ask the browser to render again
        this.requestAnimationFrameId = requestAnimationFrame(this.renderMapContinuously);


    }

    trySetTerainBox() {
        for (let j = 0; j < this.aViewports.length; j++) {
            if (this.aViewports[j].terrainBox == null) {
                let aViewportLayers = this.aViewports[j].aLayers;
                if (aViewportLayers.length != 0) {
                    this.aViewports[j].terrainBox = new window.MapCore.SMcBox(-window.MapCore.DBL_MAX, -window.MapCore.DBL_MAX, 0, window.MapCore.DBL_MAX, window.MapCore.DBL_MAX, 0);
                    for (let i = 0; i < aViewportLayers.length; ++i) {
                        if (this.aViewports[j].bSetTerrainBoxByStaticLayerOnly && aViewportLayers[i].GetLayerType() != window.MapCore.IMcNativeStaticObjectsMapLayer.LAYER_TYPE) {
                            continue;
                        }

                        if (!aViewportLayers[i].IsInitialized()) {

                            if (j == 1) {
                            }
                            this.aViewports[j].terrainBox = null;
                            return;
                        }

                        let layerBox = aViewportLayers[i].GetBoundingBox();
                        if (layerBox.MinVertex.x > this.aViewports[j].terrainBox.MinVertex.x) {
                            this.aViewports[j].terrainBox.MinVertex.x = layerBox.MinVertex.x;
                        }
                        if (layerBox.MaxVertex.x < this.aViewports[j].terrainBox.MaxVertex.x) {
                            this.aViewports[j].terrainBox.MaxVertex.x = layerBox.MaxVertex.x;
                        }
                        if (layerBox.MinVertex.y > this.aViewports[j].terrainBox.MinVertex.y) {
                            this.aViewports[j].terrainBox.MinVertex.y = layerBox.MinVertex.y;
                        }
                        if (layerBox.MaxVertex.y < this.aViewports[j].terrainBox.MaxVertex.y) {
                            this.aViewports[j].terrainBox.MaxVertex.y = layerBox.MaxVertex.y;
                        }
                    }
                }
                else {
                    this.aViewports[j].terrainBox = new window.MapCore.SMcBox(0, 0, 0, 0, 0, 0);
                }

                this.aViewports[j].terrainCenter = window.MapCore.SMcVector3D((this.aViewports[j].terrainBox.MinVertex.x + this.aViewports[j].terrainBox.MaxVertex.x) / 2, (this.aViewports[j].terrainBox.MinVertex.y + this.aViewports[j].terrainBox.MaxVertex.y) / 2, 0);
                this.aViewports[j].terrainCenter.z = 10000;
            }

            if (!this.aViewports[j].bCameraPositionSet) {
                if (this.aViewports[j].viewport.GetMapType() == window.MapCore.IMcMapCamera.EMapType.EMT_2D) {
                    this.aViewports[j].viewport.SetCameraPosition(this.aViewports[j].terrainCenter);
                    this.aViewports[j].bCameraPositionSet = true;
                }
                else // 3D
                {
                    let height = {};
                    this.aViewports[j].terrainCenter.z = 1000;
                    this.aViewports[j].viewport.SetCameraPosition(this.aViewports[j].terrainCenter);
                    let params = new window.MapCore.IMcSpatialQueries.SQueryParams();
                    params.eTerrainPrecision = window.MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGH;
                    this.aViewports[j].bCameraPositionSet = true;
                    params.pAsyncQueryCallback = new CreateCallbackService.CAsyncQueryCallback(this.aViewports[j]);
                    this.aViewports[j].viewport.GetTerrainHeight(this.aViewports[j].terrainCenter, height, null, params); // async, wait for OnTerrainHeightResults()
                }
            }
        }
    }

    resizeCanvases = () => {
        if (this.aViewports.length == 0) {
            return;
        }

        let CanvasesInRow, CanvasesInColumn;
        if (!this.state.bSameCanvas) {
            CanvasesInRow = Math.ceil(Math.sqrt(this.aViewports.length));
            CanvasesInColumn = Math.ceil(this.aViewports.length / CanvasesInRow);
        }
        else {
            CanvasesInRow = 1;
            CanvasesInColumn = 1;
        }
        //todo: use this instead: document.getElementById('id').getBoundingClientRect()
        //    let width =  (window.innerWidth - 40) / CanvasesInRow - 10;
        //    let height = (window.innerHeight - 80) / CanvasesInColumn - 15;
        let width = document.getElementById('canvasesContainer').getBoundingClientRect().width;
        let height = document.getElementById('canvasesContainer').getBoundingClientRect().height;

        for (let i = 0; i < this.aViewports.length; i++) {
            this.aViewports[i].canvas.width = width;
            this.aViewports[i].canvas.height = height;
            this.aViewports[i].viewport.ViewportResized();
        }
    }

    calcMinMaxHeights() {
        let minHeight = 0;
        let maxHeight = 700;
        let fp = this.viewport.GetCameraFootprint();

        if (fp.bUpperLeftFound && fp.bUpperRightFound && fp.bLowerRightFound && fp.bLowerLeftFound) {
            let minPoint = {}, maxPoint = {};
            if (this.viewport.GetExtremeHeightPointsInPolygon([fp.UpperLeft, fp.UpperRight, fp.LowerRight, fp.LowerLeft], maxPoint, minPoint)) {
                minHeight = minPoint.Value.z;
                maxHeight = maxPoint.Value.z;
            }
        }
        return { minHeight, maxHeight };
    }

    // function switching DTM-visualization (height map) on/off
    doDtmVisualization() {
        if (!this.viewport.GetDtmVisualization()) {
            let result = this.calcMinMaxHeights();
            let DtmVisualization = new window.MapCore.IMcMapViewport.SDtmVisualizationParams();
            window.MapCore.IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors(DtmVisualization, result.minHeight, result.maxHeight);
            DtmVisualization.bDtmVisualizationAboveRaster = true;
            DtmVisualization.uHeightColorsTransparency = 120;
            DtmVisualization.uShadingTransparency = 255;
            this.viewport.SetDtmVisualization(true, DtmVisualization);
        } else {
            this.viewport.SetDtmVisualization(false);
        }
    }

    mouseWheelHandler = e => {
        let bHandled = {};
        let eCursor = {};
        let wheelDelta = - e.deltaY;
        this.editMode.OnMouseEvent(window.MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_WHEEL, window.MapCore.SMcPoint(0, 0), e.ctrlKey, wheelDelta, bHandled, eCursor);
        if (bHandled.Value) {
            return;
        }
        let fFactor = (e.shiftKey ? 5 : 1);
        let factor = (e.shiftKey ? 10 : 1);
        let fScalefactor = Math.pow(1.25, fFactor);
        if (this.viewport.GetMapType() == window.MapCore.IMcMapCamera.EMapType.EMT_3D) {
            if (e.ctrlKey) {
                this.viewport.MoveCameraRelativeToOrientation(window.MapCore.SMcVector3D(0, 0, wheelDelta / 8.0 * factor), true);
                // viewport.MoveCameraRelativeToOrientation(window.MapCore.SMcVector3D(0, 0, -Math.sign(wheelDelta) * 12.5 * fFactor), true);
            }
            else {
                let CameraPosition = this.viewport.GetCameraPosition();
                let dDistance = {};
                if (!this.viewport.GetRayIntersection(CameraPosition, this.viewport.GetCameraForwardVector(), window.MapCore.DBL_MAX, null, null, dDistance)) {
                    let GeoCalc = window.MapCore.IMcGeographicCalculations.Create(this.viewport.GetCoordinateSystem());
                    let uWidth = {}, uHeight = {}, IntersectionPoint = {};
                    this.viewport.GetViewportSize(uWidth, uHeight);
                    let ScreenCenter = new window.MapCore.SMcVector3D(uWidth.Value / 2, uHeight.Value / 2, 0);
                    if (this.aViewports[this.activeViewport].terrainBox != null) {
                        let dDistanceX = {}, dDistanceY = {}, dToPLaneeDist = {};
                        let Normal = new window.MapCore.SMcVector3D(1, 0, 0);

                        // intersect bounding box's 4 planes
                        if (this.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, this.aViewports[this.activeViewport].terrainBox.MinVertex.x, Normal) &&
                            window.MapCore.SMcVector3D.Length(window.MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value)) < this.dZoom3DMaxDistance) {
                            GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                            dDistanceX.Value = dToPLaneeDist.Value;
                        }
                        if (this.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, this.aViewports[this.activeViewport].terrainBox.MaxVertex.x, Normal) &&
                            window.MapCore.SMcVector3D.Length(window.MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value)) < this.dZoom3DMaxDistance) {
                            GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                            if (!dDistanceX.Value || dToPLaneeDist.Value > dDistanceX.Value) {
                                dDistanceX.Value = dToPLaneeDist.Value;
                            }
                        }
                        Normal = new window.MapCore.SMcVector3D(0, 1, 0);
                        if (this.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, this.aViewports[this.activeViewport].terrainBox.MinVertex.y, Normal) &&
                            window.MapCore.SMcVector3D.Length(window.MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value)) < this.dZoom3DMaxDistance) {
                            GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                            dDistanceY.Value = dToPLaneeDist.Value;
                        }
                        if (this.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, this.aViewports[this.activeViewport].terrainBox.MaxVertex.y, Normal) &&
                            window.MapCore.SMcVector3D.Length(window.MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value)) < this.dZoom3DMaxDistance) {
                            GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                            if (!dDistanceY.Value || dToPLaneeDist.Value > dDistanceY.Value) {
                                dDistanceY.Value = dToPLaneeDist.Value;
                            }
                        }
                        if (!dDistanceX.Value) {
                            dDistance.Value = dDistanceY.Value;
                        }
                        else if (!dDistanceY.Value) {
                            dDistance.Value = dDistanceX.Value;
                        }
                        else {
                            dDistance.Value = Math.min(dDistanceX.Value, dDistanceY.Value);
                        }
                    }
                    if (!dDistance.Value) {
                        // intersect zero-height plane
                        if (this.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint) &&
                            window.MapCore.SMcVector3D.Length(window.MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value)) < this.dZoom3DMaxDistance) {
                            GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dDistance, true);
                        }
                    }
                    GeoCalc.Destroy();
                }
                if (dDistance.Value) {
                    let dNewDistance;
                    if (wheelDelta > 0) {
                        dNewDistance = Math.max(dDistance.Value / fScalefactor, this.dZoom3DMinDistance);
                    }
                    else {
                        dNewDistance = Math.min(dDistance.Value * fScalefactor, this.dZoom3DMaxDistance);
                    }
                    this.viewport.MoveCameraRelativeToOrientation(new window.MapCore.SMcVector3D(0, dDistance.Value - dNewDistance, 0), false);
                }
            }
        } else {
            let fScale = this.viewport.GetCameraScale();
            if (wheelDelta > 0) {
                this.viewport.SetCameraScale(fScale / 1.25);
            } else {
                this.viewport.SetCameraScale(fScale * 1.25);
            }

            if (this.viewport.GetDtmVisualization()) {
                this.doDtmVisualization();
                this.doDtmVisualization();
            }
        }

        e.preventDefault();
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();
    }


    mouseMoveHandler = e => {
        if (this.viewport.GetWindowHandle() != e.target) {
            return;
        }

        let EventPixel = window.MapCore.SMcPoint(e.offsetX, e.offsetY);
        if (e.buttons <= 1) {
            let bHandled = {};
            let eCursor = {};
            this.editMode.OnMouseEvent(e.buttons == 1 ? window.MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_DOWN : window.MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_UP,
                EventPixel, e.ctrlKey, 0, bHandled, eCursor);
            if (bHandled.Value) {
                e.preventDefault();
                e.cancelBubble = true;
                if (e.stopPropagation) e.stopPropagation();
                return;
            }
        }

        if (this.nMousePrevX != null) {
            let uWidth = {}, uHeight = {};
            this.viewport.GetViewportSize(uWidth, uHeight);
            let ViewportCenter = new window.MapCore.SMcVector2D(uWidth.Value / 2, uHeight.Value / 2);
            let MousePrev = new window.MapCore.SMcVector2D(this.nMousePrevX, this.nMousePrevY);
            let MouseCurr = new window.MapCore.SMcVector2D(EventPixel.x, EventPixel.y);
            let MousePrevFromCenter = window.MapCore.SMcVector2D.Minus(MousePrev, ViewportCenter);
            let MouseCurrFromCenter = window.MapCore.SMcVector2D.Minus(MouseCurr, ViewportCenter);
            let MouseDellta = window.MapCore.SMcVector2D.Minus(MouseCurr, MousePrev);

            if (e.buttons == 1) {
                if (this.viewport.GetMapType() == window.MapCore.IMcMapCamera.EMapType.EMT_3D) {
                    if (e.ctrlKey) {
                        let MousePrevWorld = this.ViewportScreenToWorld(this.viewport, new window.MapCore.SMcVector3D(MousePrev));
                        let MouseCurrWorld = this.ViewportScreenToWorld(this.viewport, new window.MapCore.SMcVector3D(MouseCurr));
                        if (MousePrevWorld != null && MouseCurrWorld != null) {
                            this.viewport.SetCameraPosition(window.MapCore.SMcVector3D.Minus(MousePrevWorld, MouseCurrWorld), true);
                        }
                    }
                    else {
                        let fPitch = {}, fRoll = {};
                        this.viewport.GetCameraOrientation(null, fPitch, fRoll);
                        let fFOV = this.viewport.GetCameraFieldOfView();
                        let dCameraDist = ViewportCenter.x / Math.tan(fFOV / 2 * Math.PI / 180);
                        let fDeltaYaw = window.MapCore.SMcVector2D.GetYawAngleDegrees(new window.MapCore.SMcVector2D(MousePrevFromCenter.x, dCameraDist)) -
                            window.MapCore.SMcVector2D.GetYawAngleDegrees(new window.MapCore.SMcVector2D(MouseCurrFromCenter.x, dCameraDist));
                        let fDeltaPitch = window.MapCore.SMcVector2D.GetYawAngleDegrees(new window.MapCore.SMcVector2D(MouseCurrFromCenter.y, dCameraDist)) -
                            window.MapCore.SMcVector2D.GetYawAngleDegrees(new window.MapCore.SMcVector2D(MousePrevFromCenter.y, dCameraDist));
                        this.viewport.RotateCameraRelativeToOrientation(fDeltaYaw, Math.min(Math.max(fDeltaPitch, this.fMinPitch - fPitch.Value), this.fMaxPitch - fPitch.Value), 0, false);

                        // roll could be altered by RotateCameraRelativeToOrientation(), return it to the previous value
                        let fNewRoll = {};
                        this.viewport.GetCameraOrientation(null, null, fNewRoll);
                        let fRollDiff = fRoll.Value - fNewRoll.Value;
                        let MouseCurrWorld = this.ViewportScreenToWorld(this.viewport, new window.MapCore.SMcVector3D(MouseCurr));
                        if (MouseCurrWorld != null) {
                            this.viewport.RotateCameraAroundWorldPoint(MouseCurrWorld, 0, 0, fRollDiff, false);
                        }
                        else {
                            this.viewport.SetCameraOrientation(0, 0, fRollDiff, true);
                        }
                    }
                }
                else {
                    if (e.ctrlKey) {
                        let fDeltaYaw = window.MapCore.SMcVector2D.GetYawAngleDegrees(MouseCurrFromCenter) - window.MapCore.SMcVector2D.GetYawAngleDegrees(MousePrevFromCenter);
                        this.viewport.SetCameraOrientation(fDeltaYaw, 0, 0, true);
                    }
                    else {
                        this.viewport.ScrollCamera(-MouseDellta.x, -MouseDellta.y);
                    }
                }

                e.preventDefault();
                e.cancelBubble = true;
                if (e.stopPropagation) e.stopPropagation();
            }
            else if (e.buttons == 4 && this.RotationCenter != null) {
                this.viewport.RotateCameraAroundWorldPoint(this.RotationCenter, MouseDellta.x * 360 / uWidth.Value, 0, 0, false);
                if (this.viewport.GetMapType() == window.MapCore.IMcMapCamera.EMapType.EMT_3D) {
                    let fPitch = {};
                    this.viewport.GetCameraOrientation(null, fPitch, null);
                    this.viewport.RotateCameraAroundWorldPoint(this.RotationCenter, 0, Math.min(Math.max(-MouseDellta.y * 180 / uWidth.Value, this.fMinPitch - fPitch.Value), this.fMaxPitch - fPitch.Value), 0, true);
                }
            }
        }

        this.nMousePrevX = EventPixel.x;
        this.nMousePrevY = EventPixel.y;
    }

    mouseDownHandler = e => {
        if (this.editMode.IsEditingActive()) {
            // EditMode is active: don't change active viewport, but ignore click on non-active one
            if (this.viewport.GetWindowHandle() != e.target) {
                return;
            }
        } else if (!this.state.bSameCanvas) {
            for (let i = 0; i < this.aViewports.length; i++) {
                if (e.target == this.aViewports[i].viewport.GetWindowHandle()) {
                    this.activeViewport = i;
                    this.updateActiveViewport();
                    break;
                }
            }
        }

        let EventPixel = window.MapCore.SMcPoint(e.offsetX, e.offsetY);
        this.mouseDownButtons = e.buttons;
        if (e.buttons == 1) {
            let bHandled = {};
            let eCursor = {};
            this.editMode.OnMouseEvent(window.MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_PRESSED, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
            if (bHandled.Value) {
                e.preventDefault();
                e.cancelBubble = true;
                if (e.stopPropagation) e.stopPropagation();
                return;
            }

            this.nMousePrevX = EventPixel.x;
            this.nMousePrevY = EventPixel.y;
        }

        e.preventDefault();
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();
    }

    mouseUpHandler = e => {
        if (this.viewport.GetWindowHandle() != e.target) {
            return;
        }

        let EventPixel = window.MapCore.SMcPoint(e.offsetX, e.offsetY);
        let buttons = this.mouseDownButtons & ~e.buttons;
        if (buttons == 1) {
            let bHandled = {};
            let eCursor = {};
            this.editMode.OnMouseEvent(window.MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_RELEASED, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
            if (bHandled.Value) {
                e.preventDefault();
                e.cancelBubble = true;
                if (e.stopPropagation) e.stopPropagation();
                return;
            }
        }
    }

    mouseDblClickHandler = e => {
        if (this.viewport.GetWindowHandle() != e.target) {
            return;
        }

        let EventPixel = window.MapCore.SMcPoint(e.offsetX, e.offsetY);
        let buttons = this.mouseDownButtons & ~e.buttons;
        if (this.bEdit) {
            let aTargets = this.viewport.ScanInGeometry(new window.MapCore.SMcScanPointGeometry(window.MapCore.EMcPointCoordSystem.EPCS_SCREEN, window.MapCore.SMcVector3D(EventPixel.x, EventPixel.y, 0), 20), false);
            for (let i = 0; i < aTargets.length; ++i) {
                if (aTargets[i].eTargetType == window.MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT) {
                    if (this.bEdit) {
                        this.editMode.StartEditObject(aTargets[i].ObjectItemData.pObject, aTargets[i].ObjectItemData.pItem);
                    }
                    break;
                }
            }
            this.bEdit = false;
            e.preventDefault();
            e.cancelBubble = true;
            if (e.stopPropagation) e.stopPropagation();
            return;
        }

        if (buttons == 1) {
            let bHandled = {};
            let eCursor = {};
            this.editMode.OnMouseEvent(window.MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_DOUBLE_CLICK, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
            if (bHandled.Value) {
                e.preventDefault();
                e.cancelBubble = true;
                if (e.stopPropagation) e.stopPropagation();
                return;
            }
        }
    }

    createViewport(terrain, eMapTypeToOpen) {
        // create canvas if needed
        let currCanvas;
        if (!this.state.bSameCanvas || this.aViewports.length == 0) {
            // create canvas
            currCanvas = document.createElement('canvas');
            //currCanvas.style.border = "thick solid #FFFFFF"; 

            currCanvas.addEventListener("wheel", this.mouseWheelHandler, false);
            currCanvas.addEventListener("mousemove", this.mouseMoveHandler, false);
            currCanvas.addEventListener("mousedown", this.mouseDownHandler, false);
            currCanvas.addEventListener("mouseup", this.mouseUpHandler, false);
            currCanvas.addEventListener("dblclick", this.mouseDblClickHandler, false);
        }
        else {
            // use existing canvas
            currCanvas = this.aViewports[0].canvas;
        }

        // create viewport
        let layerGroup = this.state.mapLayerGroups.get(this.state.lastTerrainConfiguration);
        let vpCreateData = new window.MapCore.IMcMapViewport.SCreateData(eMapTypeToOpen);
        vpCreateData.pDevice = this._device;
        vpCreateData.pCoordinateSystem = (terrain != null ? terrain.GetCoordinateSystem() : this.overlayManager.GetCoordinateSystemDefinition());
        vpCreateData.pOverlayManager = this.overlayManager;
        vpCreateData.hWnd = currCanvas;
        if (layerGroup.bShowGeoInMetricProportion) {
            vpCreateData.bShowGeoInMetricProportion = true;
        }
        this.viewport = window.MapCore.IMcMapViewport.Create(/*Camera*/null, vpCreateData, terrain != null ? [terrain] : null);
        this.editMode = window.MapCore.IMcEditMode.Create(this.viewport);

        // add camera-update callback
        let callback = new CreateCallbackService.CCameraUpdateCallback();
        this.viewport.AddCameraUpdateCallback(callback);

        if (this.viewport.GetMapType() == window.MapCore.IMcMapCamera.EMapType.EMT_3D) {
            this.viewport.SetScreenSizeTerrainObjectsFactor(1.5);
            this.viewport.SetCameraRelativeHeightLimits(3, 10000, true);
            this.viewport.SetCameraOrientation(0, this.fMaxPitch, 0);
        }
        else {
            this.viewport.SetVector3DExtrusionVisibilityMaxScale(50);
            if (layerGroup.InitialScale2D) {
                this.viewport.SetCameraScale(layerGroup.InitialScale2D);
            }
        }

        this.viewport.SetBackgroundColor(window.MapCore.SMcBColor(70, 70, 70, 255));

        // set object delays for optimazing rendering objects
        this.viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_UPDATE, true, 50);
        this.viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_CONDITION, true, 50);
        this.viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_SIZE, true, 5);
        this.viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_HEIGHT, true, 50);

        // set objects movement threshold
        this.viewport.SetObjectsMovementThreshold(1);

        // set terrain cache
        if (terrain != null) {
            this.viewport.SetTerrainNumCacheTiles(terrain, false, 300);
            this.viewport.SetTerrainNumCacheTiles(terrain, true, 300);
        }

        let viewportData = new SViewportData(this.viewport, this.editMode);
        viewportData.terrain = terrain;
        if (layerGroup.bSetTerrainBoxByStaticLayerOnly) {
            viewportData.bSetTerrainBoxByStaticLayerOnly = true;
        }

        this.aViewports.push(viewportData);
        const canvasParent = document.getElementById('canvasesContainer')
        canvasParent.appendChild(currCanvas);
        this.activeViewport = this.aViewports.length - 1;

        this.updateActiveViewport();
        this.resizeCanvases();
        this.trySetTerainBox();
    }

    ViewportScreenToWorld(Viewport, ScreenPoint) {
        let bIntersection = false;
        let WorldCenter = {};
        if (Viewport.GetMapType() == window.MapCore.IMcMapCamera.EMapType.EMT_3D) {
            bIntersection = Viewport.ScreenToWorldOnTerrain(ScreenPoint, WorldCenter);
        }
        if (!bIntersection) // EMT_2D || !bIntersection
        {
            bIntersection = Viewport.ScreenToWorldOnPlane(ScreenPoint, WorldCenter);
        }
        return (bIntersection ? WorldCenter.Value : null);
    }

    // function updating active viewport / Edit Mode and canvas borders
    updateActiveViewport() {
        if (this.activeViewport >= 0) {
            for (let i = 0; i < this.aViewports.length; ++i) {
                if (i == this.activeViewport) {
                    this.viewport = this.aViewports[i].viewport;
                    this.editMode = this.aViewports[i].editMode;
                    if (!this.state.bSameCanvas) {
                        //this.aViewports[i].canvas.style.borderColor = "blue";
                    }
                }
                else {
                    //this.aViewports[i].canvas.style.borderColor = "white";
                }
            }
        }
    }

    doPrevViewport() {
        if (this.aViewports.length > 1) {
            this.activeViewport = (this.activeViewport + this.aViewports.length - 1) % this.aViewports.length;
            this.updateActiveViewport();
        }
    }

    doNextViewport() {
        if (this.aViewports.length > 1) {
            this.activeViewport = (this.activeViewport + 1) % this.aViewports.length;
            this.updateActiveViewport();
        }
    }

    // function creating terrain overlayManager and viewport, starting rendering
    initializeViewports() {
        let terrain = null;
        if (this.aLastTerrainLayers.length > 0) {
            terrain = window.MapCore.IMcMapTerrain.Create(this.lastCoordSys, this.aLastTerrainLayers);
            terrain.AddRef();
            let group = this.state.mapLayerGroups.get(this.state.lastTerrainConfiguration);
            for (let i = 0; i < group.aLayerParams.length; ++i) {
                if (group.aLayerParams[i]) {
                    terrain.SetLayerParams(this.aLastTerrainLayers[i], group.aLayerParams[i]);
                }
            }
        }

        // create overlay manager
        if (this.overlayManager == null) {
            if (this.lastCoordSys == null) {
                this.lastCoordSys = window.MapCore.IMcGridUTM.Create(36, window.MapCore.IMcGridCoordinateSystem.EDatumType.EDT_ED50_ISRAEL);
                this.lastCoordSys.AddRef();
            }
            this.overlayManager = window.MapCore.IMcOverlayManager.Create(this.lastCoordSys);
            this.overlayManager.AddRef();
            this.overlayManager.SetScaleFactor(this.state.mapScaleFactor);
            // create overlay for objects
            this.overlay = window.MapCore.IMcOverlay.Create(this.overlayManager);
        }

        // create map viewports
        switch (this.state.lastViewportConfiguration) {
            case "2D/3D":
                this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_2D);
                this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_3D);
                this.DoPrevViewport();
                break;
            case "3D/2D":
                if (this.state.bSameCanvas) {
                    this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_2D);
                    this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_3D);
                }
                else {
                    this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_3D);
                    this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_2D);
                    this.doPrevViewport();
                }
                break;
            case "2D":
                this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_2D);
                break;
            case "3D":
                this.createViewport(terrain, window.MapCore.IMcMapCamera.EMapType.EMT_3D);
                break;
        }

        // example of try-catch MapCoreError
        try {
            // MapCore API function call
        }
        catch (ex) {
            if (ex instanceof window.MapCoreError) {
                alert("MapCore Error #" + ex.name + ": " + ex.message);
            }
            else {
                throw ex;
            }
        }

        // ask the browser to render once
        requestAnimationFrame(this.renderMapContinuously);
    }

    createMapLayersAndViewports() {
        this.aLastTerrainLayers = [];

        let group = this.state.mapLayerGroups.get(this.state.lastTerrainConfiguration);
        // create coordinate system by running a code string prepared during parsing configuration files (JSON configuration file and capabilities XML of MapCoreLayerServer)
        // e.g. MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84)
        this.lastCoordSys = group.coordSystemString;

        for (let i = 0; i < group.aLayerCreateStrings.length; ++i) {
            // create map layer by running code string prepared during parsing configuration files (JSON configuration file and capabilities XML of MapCoreLayerServer)
            // e.g. MapCore.IMcNativeRasterMapLayer.Create('http:Maps/Raster/SwissOrtho-GW') or CreateWMTSRasterLayer(...) or CreateWMSRasterLayer(...)
            const layer = eval(group.aLayerCreateStrings[i]);
            this.aLastTerrainLayers.push(layer);
        }
        this.initializeViewports();
    }
    openMap(title, is3d) {

        return new Promise((resolve, reject) => {
            try {
                this._device = this.context.getDevice()

                CreateCallbackService.initCallbacks()
                let pAsyncOperationCallback = new CreateCallbackService.CAsyncOperationCallback(
                    (eStatus, strServerURL, eWebMapServiceType, aLayers, astrServiceMetadataURLs, strServiceProviderName) => {
                        resolve(true);
                        this.ProcessWebServerLayersResults(eStatus, config.urls.getCapabilities, eWebMapServiceType, aLayers, astrServiceMetadataURLs)
                        this.state.mapLayerGroups.forEach((value, key) => {
                            if (key == title) {
                                this.setState({
                                    lastTerrainConfiguration: key,
                                    lastViewportConfiguration: is3d ? "3D" : "2D"
                                }, () => {
                                    this.createMapLayersAndViewports();
                                });
                            }
                        })
                    })

                window.MapCore.IMcMapDevice.GetWebServerLayers(config.urls.getCapabilities, window.MapCore.IMcMapLayer.EWebMapServiceType.EWMS_MAPCORE, null, pAsyncOperationCallback); // async, wait for OnWebServerLayersResults()

            } catch (e) {
                console.log('error when trying to call getCapabilities: ', e);
            }

        })

    }

    ProcessWebServerLayersResults(eStatus, strServerURL, eWebMapServiceType, aLayers, astrServiceMetadataURLs) {
        let MapLayerServerURL = astrServiceMetadataURLs[0];
        if (MapLayerServerURL == null || MapLayerServerURL == "") {
            MapLayerServerURL = strServerURL;
        }
        let lastSlashIndex = MapLayerServerURL.lastIndexOf("?");
        if (lastSlashIndex < 0) {
            lastSlashIndex = MapLayerServerURL.lastIndexOf("/");
        }
        if (lastSlashIndex < 0) {
            return;
        }
        let TrimmedMapLayerServerURL = MapLayerServerURL.substring(0, lastSlashIndex);

        /* 
           Ugly Hack for nginx environmet which we need to build the server url with the mc-layers-server name 
           and we dont get it in the get capabilities response in ServiceMetadataURL                   
        */
        const { protocol, port, pathname, host } = window.location;

        // our indication that we work under nginx and need to add the mc-layer-server service to the url path
        if (!port && pathname && pathname !== '/') {
            const trimmedPathname = pathname.endsWith('/') ? pathname.substring(0, pathname.length - 1) : pathname;
            TrimmedMapLayerServerURL = TrimmedMapLayerServerURL.replace(host, host + trimmedPathname);
        }

        if (protocol === 'https:') {
            TrimmedMapLayerServerURL = TrimmedMapLayerServerURL.replace('http:', 'https:');
        }
        for (let layer of aLayers) {
            // check here if its single layer preview. if yes put only this layer in the hashMap                    
            let layerID = layer.strLayerId;
            if (this.context.mapToPreview.type === config.nodesLevel.layer &&
                (this.context.mapToPreview.data.LayerId !== layerID && !(this.context.mapToPreview.data.LayerType == 'RAW_VECTOR_3D_EXTRUSION' && this.context.mapToPreview.data.RawLayerInfo.Vector3DExt.DtmLayerId == layerID)))
                continue;
            if (!layer.pCoordinateSystem) {
                layer.pCoordinateSystem = window.MapCore.IMcGridUTM.Create(36, window.MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84);
            }
            if (layer.pCoordinateSystem) {
                for (let i = 0; i < layer.astrGroups.length; ++i) {
                    let group = layer.astrGroups[i];

                    let pCoordSystem = null;

                    if (pCoordSystem == null) {
                        pCoordSystem = layer.pCoordinateSystem;
                    }
                    if (pCoordSystem != null) {
                        pCoordSystem.AddRef();
                    }
                    else {
                        continue;
                    }
                    let layerGroup = this.state.mapLayerGroups.get(group);
                    if (layerGroup == undefined) {
                        layerGroup = new SLayerGroup(pCoordSystem, true); // for MapCoreLayerServer only: bShowGeoInMetricProportion is true
                        this.setState({ mapLayerGroups: new Map(this.state.mapLayerGroups.set(group, layerGroup)) });
                    }

                    let layerCreateString;
                    this.layerCallback = new CreateCallbackService.CLayerReadCallback(this.aViewports);

                    layerCreateString = layer.strLayerType
                        .replace("MapCore", "MapCore.IMcNative")
                        .replace("DTM", "Dtm") + "MapLayer" + ".Create('" + TrimmedMapLayerServerURL + "/" + layerID + "',this.layerCallback)";

                    layerGroup.aLayerCreateStrings.push(layerCreateString);
                    let layerParams = null;
                    if (layer.nDrawPriority != 0) {
                        layerParams = new window.MapCore.IMcMapTerrain.SLayerParams();
                        layerParams.nDrawPriority = layer.nDrawPriority;
                    }
                    layerGroup.aLayerParams.push(layerParams);
                }
            }
        }
    }
    catch(e) {
        alert("Invalid Capabilities file");
    }
    doDtmVisualization() {
        if (!this.viewport.GetDtmVisualization()) {
            let result = this.calcMinMaxHeights();
            let DtmVisualization = new window.MapCore.IMcMapViewport.SDtmVisualizationParams();
            window.MapCore.IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors(DtmVisualization, result.minHeight, result.maxHeight);
            DtmVisualization.bDtmVisualizationAboveRaster = true;
            DtmVisualization.uHeightColorsTransparency = 120;
            DtmVisualization.uShadingTransparency = 255;
            this.viewport.SetDtmVisualization(true, DtmVisualization);
        }
        else {
            this.viewport.SetDtmVisualization(false);
        }
    }

    // function closing active viewport
    closeMap() {
        // delete Edit Mode
        if (this.editMode != null) {
            this.editMode.Destroy();
            this.editMode = null
        }
        // let terrains = this.viewport.GetTerrains();
        // delete viewport
        if (this.viewport != null) {
            this.viewport.Release();
            this.viewport = null
        }

        if (this.overlayManager != null) {
            this.overlayManager.Release();
            this.overlayManager = null
        }
        // terrains.forEach(t => {
        //     t.Release()
        // })

        if (!this.bSameCanvas || this.aViewports.length == 1) {
            // delete canvas
            let currCanvas = this.aViewports[this.activeViewport].canvas;
            currCanvas.removeEventListener("wheel", this.mouseWheelHandler, false);
            currCanvas.removeEventListener("mousemove", this.mouseMoveHandler, false);
            currCanvas.removeEventListener("mousedown", this.mouseDownHandler, false);
            currCanvas.removeEventListener("mouseup", this.mouseUpHandler, false);
            currCanvas.removeEventListener("dblclick", this.mouseDblClickHandler, false);
            let canvasParent = document.getElementById('canvasesContainer');
            canvasParent.removeChild(this.aViewports[this.activeViewport].canvas);
        }


        // remove viewport from viewport data array
        //this.activeViewport.viewport = this.activeViewport.viewport ? null :;
        this.aViewports.splice(this.activeViewport, 1);
        // if (this.aViewports.length == 0) {
        //     // no more viewports
        //     this.viewport = null;
        //     this.editMode = null;
        //     this.activeViewport = -1;
        //     // this.mapTerrains.clear();

        //     this.overlayManager = null;
        // }
        // else {
        //     // there are viewports: update active viewport
        //     if (this.activeViewport >= this.aViewports.length) {
        //         this.activeViewport = this.aViewports.length - 1;
        //     }
        //     this.updateActiveViewport();
        //     this.resizeCanvases();
        // }

        this.setState({
            mapLayerGroups: new Map(),
            //     lastTerrainConfiguration: null,
            //     lastViewportConfiguration: null /*  2D/3D, 3D/2D, 2D, 3D */,
            //     bSameCanvas: true
        });

        // this.mapTerrains = new Map;
        // this.device = null
        // this.viewportData = null;
        this.aLastTerrainLayers = [];
        // this.lastCoordSys = null;
        // this.overlayManager = null;
        // this.activeViewport = -1;
        // this.aViewports = [];
        // this.lastRenderTime = (new Date).getTime();
        // this.lastMemUsageLogTime = (new Date).getTime();
        // this.uMemUsageLoggingFrequency = 0;
        // this.nMousePrevX = 0;
        // this.nMousePrevY = 0;
        // this.mouseDownButtons = 0;
        // this.bEdit = false;
        // this.layerCallback.delete();
    }


    renderLoadingMessage() {
        return (
            <div className={cn.LoadingMessage}>
                Map core SDK is Loading...
            </div>
        )
    }

    renderRow(label, value) {
        return (
            <div className={cn.DescRow}>
                <span className={cn.DescLabel}>{label}:</span>
                <span className={cn.DescValue}>{value}</span>
            </div>
        )
    }

    renderMapToolbox() {

        return (
            <div className={`${cn.MapToolbox} ${this.context.activeMapPreview === "Description" ? cn.Active : ''}`}>
                {this.renderRow('Name', this.context.mapToPreview.data.Title)}
                {this.renderRow('Format', this.context.mapToPreview.data.LayerType)}
                {this.context.mapToPreview.data.RawLayerInfo ? this.renderRow('Coordinate System', this.context.mapToPreview.data.RawLayerInfo.CoordinateSystem.SRIDType) : null}
                {this.context.mapToPreview.data.RawLayerInfo ? this.renderRow('Extent', this.context.mapToPreview.data.RawLayerInfo.CoordinateSystem.Code) : null}
            </div>
        )
    }

    getCanvas() {
        return (
            <div className={cn.CanvasContainer} id='canvasesContainer'>
                {this.context.activeMapPreview === "Description" ? this.renderMapToolbox() : null}
            </div>);
    }

    render() {
        return (
            <div className={cn.Wrapper}>
                {this.context.isMapCoreSDKLoaded ? this.getCanvas() : this.renderLoadingMessage()}
            </div>
        );
    }
}


const CompWithContext = WithContext(MapContainer);
export default React.forwardRef((props, ref, device) => {
    return <CompWithContext {...props} compRef={ref} device={device} />
});


