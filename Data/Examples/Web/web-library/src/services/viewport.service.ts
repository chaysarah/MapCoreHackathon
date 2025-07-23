
import { ViewportData } from '../model/viewportData';
import { runCodeLibrarySafely, runMapCoreSafely } from "./error-handler.service";
import scanService from './scan/scan.service';
import MapCoreData from '../mapcore-data';
import editModeService from './editMode/editMode.service';
import mapcoreData from '../mapcore-data';

const dZoom3DMinDistance: number = 10;
const dZoom3DMaxDistance: number = 2000000;

const fMinPitch: number = -90;
const fMaxPitch: number = 0;
let mouseDownButtons: number = 0;
let nMousePrevX: number = null;
let nMousePrevY: number = null;
let RotationCenter: MapCore.SMcVector3D = null;
let MovementSensitivity: number = 100;
const m_RotationFactor = 0.01

class ViewportService {
    terrainPrecisionFactor: number = 1;
    layersInitializedinteravl: NodeJS.Timer = null;


    public createViewport(createData: MapCore.IMcMapViewport.SCreateData, terrains: MapCore.IMcMapTerrain[], viewportData?: ViewportData, querySecondaryDtmLayers?: MapCore.IMcDtmMapLayer[])
        : MapCore.IMcMapViewport {
        let viewport: MapCore.IMcMapViewport = null;
        runMapCoreSafely(() => {
            viewport = querySecondaryDtmLayers ? MapCore.IMcMapViewport.Create(/*Camera*/null, createData, terrains, querySecondaryDtmLayers) :
                MapCore.IMcMapViewport.Create(/*Camera*/null, createData, terrains);
        }, "ViewportService.createViewport=> IMcMapViewport.Create", true)
        if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
            runMapCoreSafely(() => { viewport.SetCameraClipDistances(1, 0, true) }, "ViewportService.createViewport=> IMcMapViewport.IMcMapCamera.SetCameraClipDistances", true)
        }
        runMapCoreSafely(() => { viewport.AddRef() }, "ViewportService.createViewport=> viewport.AddRef", true)
        if (viewportData) {
            viewportData.viewport = viewport;
            viewportData.editMode = MapCore.IMcEditMode.Create(viewport)
        }
        return viewport;
    }

    public closeViewport(viewportId: number, calcSizeAndPositioinCanvases?: () => void) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            // delete Edit Mode
            runMapCoreSafely(() => { viewportData.editMode?.Destroy(); }, "ViewportService.closeViewport=> IMcDestroyable.Destroy", true)
            runMapCoreSafely(() => { viewportData.viewport?.Release(); }, "ViewportService.closeViewport=>IMcBase.Release()", true)
            // remove viewport from viewport data array
            MapCoreData.viewportsData = MapCoreData.viewportsData.filter(x => x.id != viewportId)
            if (this.layersInitializedinteravl !== null) {
                clearInterval(this.layersInitializedinteravl)
            }
            console.log(`Viewport id ${viewportId} is closed.`)
            calcSizeAndPositioinCanvases && calcSizeAndPositioinCanvases();

        }
    }
    public resizeCanvas(viewportId: number, width: number, height: number): void {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            viewportData.canvas.width = width;
            viewportData.canvas.height = height;
            runMapCoreSafely(() => { viewportData.viewport?.ViewportResized(); }, "ViewportService.resizeCanvas=>.IMcMapViewport.ViewportResized", true)
        }
    }
    public zoomIn(viewportId: number): any {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                let currScale: number = viewportData.viewport.GetCameraScale();
                let newScale: number = currScale / 2;
                viewportData.viewport.SetCameraScale(newScale);
                let ScaleBox: number = Math.round(newScale);
                let MapScaleBox: number = (newScale / viewportData.viewport.GetPixelPhysicalHeight());
                return { "MapScaleBox": MapScaleBox, "ScaleBox": ScaleBox }
            }
            if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                let currFOV: number = viewportData.viewport.GetCameraFieldOfView();
                viewportData.viewport.SetCameraFieldOfView(currFOV - 5);
            }
        }
        return null
    }
    public zoomOut(viewportId: number): any {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
                let currScale: number = viewportData.viewport.GetCameraScale();
                let newScale: number = currScale * 2;
                runMapCoreSafely(() => { viewportData.viewport.SetCameraScale(newScale) }, "viewportService.zoomOut => IMcMapCamera.SetCameraScale", true);
                let ScaleBox: number = Math.round(newScale);
                let MapScaleBox: number = (newScale / viewportData.viewport.GetPixelPhysicalHeight());
                return { "MapScaleBox": MapScaleBox, "ScaleBox": ScaleBox }
            }
        }
        if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
            let currFOV: number = viewportData.viewport.GetCameraFieldOfView();
            runMapCoreSafely(() => { viewportData.viewport.SetCameraFieldOfView(currFOV + 5); }, "viewportService.zoomOut => IMcMapCamera.SetCameraFieldOfView", true)
        }
        return null
    }
    public dynamicZoom(viewportId: number, EMPropDynamicZoomMinScale: number, EMPropDynamicZoomRectangle: MapCore.IMcRectangleItem, EMPropDynamicZoomOperation: MapCore.IMcMapCamera.ESetVisibleArea3DOperation): void {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            runMapCoreSafely(() => {
                viewportData.editMode.StartDynamicZoom(EMPropDynamicZoomMinScale,
                    true,
                    new MapCore.SMcPoint(),
                    EMPropDynamicZoomRectangle,
                    EMPropDynamicZoomOperation);
            }, "viewportService.dynamicZoom => IMcEditMode.StartDynamicZoom", true)
        }
    }
    public distanceDirection(viewportId: number, EMPropShowResult: boolean): void {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            runMapCoreSafely(() => {
                viewportData.editMode.StartDistanceDirectionMeasure(EMPropShowResult,
                    true,
                    new MapCore.SMcPoint());
            }, "viewportService.distanceDirection => IMcEditMode.StartDistanceDirectionMeasure", true)
        }
    }

    public doCenterPoint(viewportId: number): void {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            const viewport: MapCore.IMcMapViewport = MapCoreData.findViewport(viewportId).viewport;
            const layers: MapCore.IMcMapLayer[] = this.getLayers(viewport);
            let terrainBox: MapCore.SMcBox;
            if (layers.length != 0) {
                runMapCoreSafely(() => {
                    terrainBox = new MapCore.SMcBox(-MapCore.DBL_MAX, -MapCore.DBL_MAX, 0, MapCore.DBL_MAX, MapCore.DBL_MAX, 0);
                }, "ViewportService.doCenterPoint => SMcBox", true)
                for (let i = 0; i < layers.length; ++i) {
                    let intersectionBox: MapCore.SMcBox = new MapCore.SMcBox();
                    let layerBox: MapCore.SMcBox = null;
                    runMapCoreSafely(() => {
                        layerBox = layers[i].GetBoundingBox();
                    }, "ViewportService.doCenterPoint => IMcMapLayer.GetBoundingBox", true)
                    runMapCoreSafely(() => {
                        MapCore.SMcBox.Intersect(intersectionBox, terrainBox, layerBox);
                    }, "ViewportService.doCenterPoint => SMcBox.Intersect", true)
                    terrainBox = intersectionBox;
                }
            }
            else {
                terrainBox = new MapCore.SMcBox(0, 0, 0, 0, 0, 0);
            }
            const terrainCenter: MapCore.SMcVector3D = new MapCore.SMcVector3D((terrainBox.MinVertex.x + terrainBox.MaxVertex.x) / 2, (terrainBox.MinVertex.y + terrainBox.MaxVertex.y) / 2, 0);
            terrainCenter.z = 10000;
            this.setCameraPosition(viewport, terrainCenter)
        }
    }
    //#region mouse event
    public mouseUpHandler(e: any, viewportId: number) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            let x: number = e.offsetX ? e.offsetX : e.nativeEvent.offsetX;
            let y: number = e.offsetY ? e.offsetY : e.nativeEvent.offsetY;
            let EventPixel: MapCore.SMcPoint = new MapCore.SMcPoint(x, y)
            let buttons: number = mouseDownButtons & ~e.buttons; let eCursor: any = {};
            if (buttons == 1) {
                let bHandled: any = {};
                runMapCoreSafely(() => {
                    viewportData.editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_RELEASED, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
                }, "viewportService.mouseUpHandler => IMcEditMode.OnMouseEvent", true)
                editModeService.AddRenderToList(bHandled.Value)
                if (bHandled.Value) {
                    e.preventDefault();
                    if (e.stopPropagation) e.stopPropagation();
                    return eCursor
                }
                nMousePrevX = null;
                nMousePrevY = null;
            }
            else if (e.buttons == 4) {
                RotationCenter = null;

            }
        }
    }
    public mouseWheelHandler(e: React.WheelEvent<HTMLCanvasElement>, viewportId: number) {
        e.preventDefault();
        if (e.stopPropagation) e.stopPropagation();
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            let bHandled: any = {};
            let eCursor: any = {};
            let wheelDelta: number = - e.deltaY;
            runMapCoreSafely(() => {
                viewportData.editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_WHEEL, new MapCore.SMcPoint(0, 0), e.ctrlKey, wheelDelta, bHandled, eCursor);
            }, "viewportService.mouseWheelHandler => IMcEditMode.OnMouseEvent", true)
            editModeService.AddRenderToList(bHandled.Value)
            if (bHandled.Value) {
                return eCursor
            }
            let fFactor: number = (e.shiftKey ? 5 : 1);
            let fScalefactor: number = Math.pow(1.25, fFactor);
            if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                if (e.ctrlKey) {
                    runMapCoreSafely(() => {
                        viewportData.viewport.MoveCameraRelativeToOrientation(new MapCore.SMcVector3D(0, 0, -Math.sign(wheelDelta) * 12.5 * fFactor), true);
                    }, "viewportService.mouseWheelHandler => IMcMapCamera.MoveCameraRelativeToOrientation", true)
                }
                else {
                    let CameraPosition: MapCore.SMcVector3D = viewportData.viewport.GetCameraPosition();
                    let dDistance: any = {};
                    if (!viewportData.viewport.GetRayIntersection(CameraPosition, viewportData.viewport.GetCameraForwardVector(), MapCore.DBL_MAX, null, null, dDistance)) {
                        let GeoCalc: MapCore.IMcGeographicCalculations = null;
                        runMapCoreSafely(() => {
                            GeoCalc = MapCore.IMcGeographicCalculations.Create(viewportData.viewport.GetCoordinateSystem());
                        }, "viewportService.mouseWheelHandler => IMcGeographicCalculations.Create", true)

                        let uWidth: any = {}, uHeight: any = {}, IntersectionPoint: any = {};
                        runMapCoreSafely(() => {
                            viewportData.viewport.GetViewportSize(uWidth, uHeight);
                        }, "viewportService.mouseWheelHandler => IMcMapViewport.GetViewportSize", true)
                        let ScreenCenter: MapCore.SMcVector3D = new MapCore.SMcVector3D(uWidth.Value / 2, uHeight.Value / 2, 0);
                        if (viewportData.viewport.GetTerrainsBoundingBox() != null) {
                            let dDistanceX: any = {}, dDistanceY: any = {}, dToPLaneeDist: any = {};
                            let Normal: MapCore.SMcVector3D = new MapCore.SMcVector3D(1, 0, 0);
                            let ScreenToWorldOnPlane = null;
                            runMapCoreSafely(() => {
                                ScreenToWorldOnPlane = viewportData.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, viewportData.viewport.GetTerrainsBoundingBox().MinVertex.x, Normal)
                            }, "viewportService.mouseWheelHandler => IMcMapCamera.ScreenToWorldOnPlane", true)
                            let minus: any = null;
                            runMapCoreSafely(() => { minus = MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value); }, "viewportService.mouseWheelHandler => SMcVector3D.Minus", true)
                            let len: any = null;
                            runMapCoreSafely(() => { len = MapCore.SMcVector3D.Length(minus) }, "viewportService.mouseWheelHandler => SMcVector3D.Length", true)
                            if (ScreenToWorldOnPlane && len < dZoom3DMaxDistance) {
                                runMapCoreSafely(() => {
                                    GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                                }, "viewportService.mouseWheelHandler => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations", true)
                                dDistanceX.Value = dToPLaneeDist.Value;
                            }
                            let screenToWorldOnPlane: any = null;
                            runMapCoreSafely(() => {
                                screenToWorldOnPlane = viewportData.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, viewportData.viewport.GetTerrainsBoundingBox().MaxVertex.x, Normal)
                            }, "viewportService.mouseWheelHandler =>IMcMapCamera.ScreenToWorldOnPlane", true)
                            let leng: any = null;
                            runMapCoreSafely(() => { leng = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value)) }, "viewportService.mouseWheelHandler =>SMcVector3D.Length", true)
                            if (screenToWorldOnPlane && leng < dZoom3DMaxDistance) {
                                runMapCoreSafely(() => {
                                    GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                                }, "viewportService.mouseWheelHandler => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations", true)
                                if (!dDistanceX.Value || dToPLaneeDist.Value > dDistanceX.Value) {
                                    dDistanceX.Value = dToPLaneeDist.Value;
                                }
                            }
                            Normal = new MapCore.SMcVector3D(0, 1, 0);
                            let screenToWorldOnPlane2: any = null;
                            runMapCoreSafely(() => {
                                screenToWorldOnPlane2 = viewportData.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, viewportData.viewport.GetTerrainsBoundingBox().MinVertex.y, Normal)
                            }, "viewportService.mouseWheelHandler =>IMcMapCamera.ScreenToWorldOnPlane", true)
                            let len2: any = null;
                            runMapCoreSafely(() => {
                                len2 = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value))
                            }, "viewportService.mouseWheelHandler =>SMcVector3D.Length", true)
                            if (screenToWorldOnPlane2 && len2 < dZoom3DMaxDistance) {
                                runMapCoreSafely(() => {
                                    GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                                }, "viewportService.mouseWheelHandler => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations", true)
                                dDistanceY.Value = dToPLaneeDist.Value;
                            }
                            let screenToWorldOnPlane3: any = null;
                            runMapCoreSafely(() => {
                                screenToWorldOnPlane3 = viewportData.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint, viewportData.viewport.GetTerrainsBoundingBox().MaxVertex.y, Normal)
                            }, "viewportService.mouseWheelHandler => IMcMapCamera.ScreenToWorldOnPlane", true)
                            let len3: any = null;
                            runMapCoreSafely(() => {
                                len3 = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value));
                            }, "viewportService.mouseWheelHandler =>SMcVector3D.Length", true)
                            if (screenToWorldOnPlane3 && len3 < dZoom3DMaxDistance) {
                                runMapCoreSafely(() => {
                                    GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dToPLaneeDist, true);
                                }, "viewportService.mouseWheelHandler => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations", true)
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
                            let screenToWorldOnPlane4: any = null;
                            runMapCoreSafely(() => {
                                screenToWorldOnPlane4 = viewportData.viewport.ScreenToWorldOnPlane(ScreenCenter, IntersectionPoint)
                            }, "viewportService.mouseWheelHandler => IMcMapCamera.ScreenToWorldOnPlane", true)
                            let len4: any = null;
                            runMapCoreSafely(() => {
                                len4 = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value))
                            }, "viewportService.mouseWheelHandler => SMcVector3D.Length", true)
                            if (screenToWorldOnPlane4 && len4 < dZoom3DMaxDistance) {
                                runMapCoreSafely(() => {
                                    GeoCalc.AzimuthAndDistanceBetweenTwoLocations(CameraPosition, IntersectionPoint.Value, {}, dDistance, true);
                                }, "viewportService.mouseWheelHandler => IMcGeographicCalculations.AzimuthAndDistanceBetweenTwoLocations", true)
                            }
                        }
                        GeoCalc.Destroy();
                    }
                    if (dDistance.Value) {
                        let dNewDistance: number;
                        if (wheelDelta > 0) {
                            dNewDistance = Math.max(dDistance.Value / fScalefactor, dZoom3DMinDistance);
                        }
                        else {
                            dNewDistance = Math.min(dDistance.Value * fScalefactor, dZoom3DMaxDistance);
                        }
                        runMapCoreSafely(() => {
                            viewportData.viewport.MoveCameraRelativeToOrientation(new MapCore.SMcVector3D(0, dDistance.Value - dNewDistance, 0), false);
                        }, "viewportService.mouseWheelHandler => IMcMapCamera.MoveCameraRelativeToOrientation", true)
                    }
                }
            }
            else {
                let fScale: number = null;
                runMapCoreSafely(() => { fScale = viewportData.viewport.GetCameraScale(); }, "viewportService.mouseWheelHandler => IMcMapCamera.GetCameraScale", true)
                if (wheelDelta > 0) {
                    runMapCoreSafely(() => { viewportData.viewport.SetCameraScale(fScale / fScalefactor); }, "viewportService.mouseWheelHandler => IMcMapCamera.SetCameraScale", true)
                }
                else {
                    runMapCoreSafely(() => { viewportData.viewport.SetCameraScale(fScale * fScalefactor); }, "viewportService.mouseWheelHandler => IMcMapCamera.SetCameraScale", true)
                }
            }
        }
    }
    public mouseMoveHandler(e: any, viewportId: number) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            let eCursor: any = {};
            let x: number = e.offsetX ? e.offsetX : e.nativeEvent.offsetX;
            let y: number = e.offsetY ? e.offsetY : e.nativeEvent.offsetY;
            let EventPixel: MapCore.SMcPoint = new MapCore.SMcPoint(x, y);
            if (e.buttons <= 1) {
                let bHandled: any = {};
                if (e.buttons !== 1)
                    runMapCoreSafely(() => {
                        viewportData.editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_UP,
                            EventPixel, e.ctrlKey, 0, bHandled, eCursor);
                    }, "viewportService.mouseMoveHandler => IMcEditMode.OnMouseEvent", true)
                else
                    runMapCoreSafely(() => {
                        viewportData.editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_DOWN,
                            EventPixel, e.ctrlKey, 0, bHandled, eCursor);
                    }, "viewportService.mouseMoveHandler => IMcEditMode.OnMouseEvent", true)
                editModeService.AddRenderToList(bHandled.Value)
                if (bHandled.Value == true) {
                    e.preventDefault();
                    if (e.stopPropagation) e.stopPropagation();
                    return eCursor;
                }
            }
            if (nMousePrevX != null) {
                let uWidth: any = {}, uHeight: any = {};
                runMapCoreSafely(() => { viewportData.viewport.GetViewportSize(uWidth, uHeight); }, "", true)
                let ViewportCenter: MapCore.SMcPoint = new MapCore.SMcVector2D(uWidth.Value / 2, uHeight.Value / 2);
                let MousePrev: MapCore.SMcVector2D = new MapCore.SMcVector2D(nMousePrevX, nMousePrevY!);
                let MouseCurr: MapCore.SMcVector2D = new MapCore.SMcVector2D(EventPixel.x, EventPixel.y);
                let MousePrevFromCenter: MapCore.SMcVector2D = null;
                runMapCoreSafely(() => { MousePrevFromCenter = MapCore.SMcVector2D.Minus(MousePrev, ViewportCenter); }, "viewportService.mouseMoveHandler => SMcVector2D.Minus", true)
                let MouseCurrFromCenter: MapCore.SMcVector2D = null;
                runMapCoreSafely(() => { MouseCurrFromCenter = MapCore.SMcVector2D.Minus(MouseCurr, ViewportCenter); }, "viewportService.mouseMoveHandler => SMcVector2D.Minus", true)
                let MouseDellta: MapCore.SMcVector2D = null;
                runMapCoreSafely(() => { MouseDellta = MapCore.SMcVector2D.Minus(MouseCurr, MousePrev); }, "viewportService.mouseMoveHandler => SMcVector2D.Minus", true)
                if (e.buttons == 1) {
                    if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                        let MouseCurrWorld: any = this.viewportScreenToWorld(viewportData.viewport, new MapCore.SMcVector3D(MouseCurr));
                        if (e.ctrlKey) {
                            let MousePrevWorld: any = this.viewportScreenToWorld(viewportData.viewport, new MapCore.SMcVector3D(MousePrev));
                            if (MousePrevWorld != null && MouseCurrWorld != null) {
                                let minus: any = null;
                                runMapCoreSafely(() => { minus = MapCore.SMcVector3D.Minus(MousePrevWorld, MouseCurrWorld); }, "viewportService.mouseMoveHandler => SMcVector3D.Minus", true)
                                runMapCoreSafely(() => {
                                    viewportData.viewport.SetCameraPosition(minus, true);
                                }, "viewportService.mouseMoveHandler => IMcMapCamera.SetCameraPosition", true)
                            }
                        }
                        else {
                            let fPitch: any = {}, fRoll: any = {};
                            runMapCoreSafely(() => { viewportData.viewport.GetCameraOrientation(null, fPitch, fRoll); }, "viewportService.mouseMoveHandler => IMcMapCamera.GetCameraOrientation", true);
                            let fFOV: number = null;
                            runMapCoreSafely(() => { fFOV = viewportData.viewport.GetCameraFieldOfView(); }, "viewportService.mouseMoveHandler => IMcMapCamera.GetCameraFieldOfView", true);
                            let dCameraDist: number = ViewportCenter.x / Math.tan(fFOV / 2 * Math.PI / 180);
                            let fDeltaYaw: number = null;
                            runMapCoreSafely(() => {
                                fDeltaYaw = MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MousePrevFromCenter.x, dCameraDist)) -
                                    MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MouseCurrFromCenter.x, dCameraDist));
                            }, "viewportService.mouseMoveHandler => SMcVector2D.GetYawAngleDegrees", true);
                            let fDeltaPitch: number = null;
                            runMapCoreSafely(() => {
                                fDeltaPitch = MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MouseCurrFromCenter.y, dCameraDist)) -
                                    MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MousePrevFromCenter.y, dCameraDist));
                            }, "viewportService.mouseMoveHandler => SMcVector2D.GetYawAngleDegrees", true);
                            runMapCoreSafely(() => {
                                viewportData.viewport.RotateCameraRelativeToOrientation(fDeltaYaw, Math.min(Math.max(fDeltaPitch, fMinPitch - fPitch.Value), fMaxPitch - fPitch.Value), 0, false);
                            }, "viewportService.mouseMoveHandler => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            let fNewRoll: any = {};
                            runMapCoreSafely(() => { viewportData.viewport.GetCameraOrientation(null, null, fNewRoll); }, "viewportService.mouseMoveHandler => IMcMapCamera.GetCameraOrientation", true);
                            let fRollDiff: number = fRoll.Value - fNewRoll.Value;
                            if (fRollDiff != 0) {
                                if (MouseCurrWorld != null) {
                                    runMapCoreSafely(() => {
                                        viewportData.viewport.RotateCameraAroundWorldPoint(MouseCurrWorld, 0, 0, fRollDiff, false);
                                    }, "viewportService.mouseMoveHandler => IMcMapCamera.RotateCameraAroundWorldPoint", true);
                                }
                                else {
                                    runMapCoreSafely(() => {
                                        viewportData.viewport.SetCameraOrientation(0, 0, fRollDiff, true);
                                    }, "viewportService.mouseMoveHandler => IMcMapCamera.SetCameraOrientation", true);
                                }
                            }
                        }
                    }
                    else {
                        if (e.ctrlKey) {
                            let fDeltaYaw: number = null;
                            runMapCoreSafely(() => {
                                fDeltaYaw = MapCore.SMcVector2D.GetYawAngleDegrees(MouseCurrFromCenter) - MapCore.SMcVector2D.GetYawAngleDegrees(MousePrevFromCenter);
                            }, "viewportService.mouseMoveHandler => SMcVector2D.GetYawAngleDegrees", true);
                            runMapCoreSafely(() => {
                                viewportData.viewport.SetCameraOrientation(fDeltaYaw, 0, 0, true);
                            }, "viewportService.mouseMoveHandler => IMcMapCamera.SetCameraOrientation", true);
                        }
                        else {
                            runMapCoreSafely(() => {
                                viewportData.viewport.ScrollCamera(-MouseDellta.x, -MouseDellta.y);
                            }, "viewportService.mouseMoveHandler => IMcMapCamera.ScrollCamera", true);
                        }
                    }
                    if (e.stopPropagation) e.stopPropagation(); e.preventDefault();
                }
                else if (e.buttons == 4 && RotationCenter != null) {
                    runMapCoreSafely(() => {
                        viewportData.viewport.RotateCameraAroundWorldPoint(RotationCenter, MouseDellta.x * 360 / uWidth.Value, 0, 0, false);
                    }, "viewportService.mouseMoveHandler => IMcMapCamera.RotateCameraAroundWorldPoint", true);
                    if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                        let fPitch: any = {};
                        runMapCoreSafely(() => { viewportData.viewport.GetCameraOrientation(null, fPitch, null); }, "viewportService.mouseMoveHandler => IMcMapCamera.GetCameraOrientation", true);
                        runMapCoreSafely(() => {
                            viewportData.viewport.RotateCameraAroundWorldPoint(RotationCenter, 0, Math.min(Math.max(-MouseDellta.y * 180 / uWidth.Value, fMinPitch - fPitch.Value), fMaxPitch - fPitch.Value), 0, true);
                        }, "viewportService.mouseMoveHandler => IMcMapCamera.RotateCameraAroundWorldPoint", true);
                    }
                }
            }
            nMousePrevX = EventPixel.x;
            nMousePrevY = EventPixel.y;
        }
    }
    public mouseDownHandler(e: any, viewportId: number) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            let eCursor: any = {};
            if (viewportData.editMode.IsEditingActive()) {
                // EditMode is active: don't change active viewport, but ignore click on non-active one
                if (viewportData.viewport.GetWindowHandle() != e.target) {
                    return;
                }
            }
            let x: number = e.offsetX ? e.offsetX : e.nativeEvent.offsetX;
            let y: number = e.offsetY ? e.offsetY : e.nativeEvent.offsetY;
            let EventPixel: MapCore.SMcPoint = new MapCore.SMcPoint(x, y)
            mouseDownButtons = e.buttons;
            if (e.buttons == 1) {
                let bHandled: any = {};
                runMapCoreSafely(() => {
                    viewportData.editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_PRESSED, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
                }, "viewportService.mouseDownHandler => IMcEditMode.OnMouseEvent", true);
                editModeService.AddRenderToList(bHandled?.Value)
                if (bHandled.Value) {
                    if (e.stopPropagation) e.stopPropagation();
                    e.preventDefault();
                    return eCursor
                }
                nMousePrevX = EventPixel.x;
                nMousePrevY = EventPixel.y;
            }
            else if (e.buttons == 4) {
                RotationCenter = this.viewportScreenToWorld(viewportData.viewport, new MapCore.SMcVector3D(EventPixel.x, EventPixel.y, 0));
            }
            if (e.stopPropagation) e.stopPropagation(); e.preventDefault();
        }
    }
    public mouseDblClickHandler(e: any, viewportId: number) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            let eCursor: any = {};
            if (viewportData.viewport.GetWindowHandle() != e.target) {
                return;
            }
            let x: number = e.offsetX ? e.offsetX : e.nativeEvent.offsetX;
            let y: number = e.offsetY ? e.offsetY : e.nativeEvent.offsetY;
            let EventPixel: MapCore.SMcPoint = new MapCore.SMcPoint(x, y);
            let bHandled: any = {};
            runMapCoreSafely(() => {
                viewportData.editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_DOUBLE_CLICK, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
            }, "viewportService.mouseDblClickHandler => IMcEditMode.OnMouseEvent", true);
            e.preventDefault();
            if (e.stopPropagation) e.stopPropagation();
            editModeService.AddRenderToList(bHandled.Value)
            if (bHandled.Value) {
                return eCursor
            }
        }
    }
    public showCursorPosition = (e: any, viewportId: number) => {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            let worldPos: any = {}
            let x: number = e.offsetX ? e.offsetX : e.nativeEvent.offsetX;
            let y: number = e.offsetY ? e.offsetY : e.nativeEvent.offsetY;
            let screenPos: MapCore.SMcVector3D = new MapCore.SMcVector3D(x, y, 0);
            let bIntersect: boolean = false;
            runMapCoreSafely(() => {
                bIntersect = viewportData.viewport.ScreenToWorldOnTerrain(screenPos, worldPos);
            }, "viewportService.showCursorPosition => IMcMapCamera.ScreenToWorldOnTerrain", true);
            if (bIntersect == false)
                runMapCoreSafely(() => {
                    bIntersect = viewportData.viewport.ScreenToWorldOnPlane(screenPos, worldPos);
                }, "viewportService.showCursorPosition => IMcMapCamera.ScreenToWorldOnPlane", true);
            let newScale: number = 0.0
            if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                if (bIntersect) {
                    newScale = viewportData.viewport.GetCameraScale(worldPos.Value);
                }
            }
            else {
                newScale = viewportData.viewport.GetCameraScale();
            }
            let ScaleBox: number = Math.round(newScale);
            let MapScaleBox: number = (newScale / viewportData.viewport.GetPixelPhysicalHeight());
            if (scanService.bPointScan)
                scanService.pointScan.StartPointScan(viewportData, screenPos);
            if (bIntersect) {
                return { "worldPos": worldPos, "ScaleBox": ScaleBox, "MapScaleBox": MapScaleBox, "screenPos": screenPos, "viewportId": viewportId }

            }
        }
    }
    //#endregion

    //#region grid
    public doCreateGrid(viewportId: number, gridType: string): void {

        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            if (viewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                alert("grid is not supported in 3D");
                return;
            }
            if (gridType == viewportData.gridType) {
                // MapCoreData.gridArr = MapCoreData.gridArr.filter(grid => grid != viewportData.viewport.GetGrid()); // TODO !!!!!!!!!!!
                runMapCoreSafely(() => { viewportData.viewport.SetGrid(null) }, "ViewportService.doCreateGrid=>IMcMapViewport.SetGrid", true)
                viewportData.gridType = null;
                return;
            }
            let regionAndScale: { region: MapCore.IMcMapGrid.SGridRegion[], scale: MapCore.IMcMapGrid.SScaleStep[] } = null;
            switch (gridType) {
                case "Geo Grid":
                    regionAndScale = this.GetGeoMapGrid()
                    break;
                case "UTM Grid":
                    regionAndScale = this.GetUTMMapGrid(viewportData)
                    break;
                case "MGRS Grid":
                    regionAndScale = this.GetMGRSMapGrid()
                    break;
                case "NZMG Grid":
                    regionAndScale = this.GetNZMGMapGrid()
                    break;
                case "GEOREF Grid":
                    regionAndScale = this.GetGeoRefMapGrid()
                    break;
                default:
                    break;
            }

            let grid: MapCore.IMcMapGrid
            runMapCoreSafely(() => {
                grid = MapCore.IMcMapGrid.Create(regionAndScale.region,
                    regionAndScale.scale);
            }, "ViewportService.doCreateGrid=> IMcMapGrid.Create", true)
            runMapCoreSafely(() => { viewportData.viewport.SetGrid(grid); }, "ViewportService.doCreateGrid=> IMcMapViewport.SetGrid", true)
            runMapCoreSafely(() => { viewportData.viewport.SetGridVisibility(true); }, "ViewportService.doCreateGrid=> IMcMapViewport.SetGridVisibility", true)
            viewportData.gridType = gridType;
            runMapCoreSafely(() => { grid.AddRef() }, "ViewportService.doCreateGrid=> IMcBase.AddRef", true)
            MapCoreData.gridArr.push(grid);
        }
    }
    public GetUTMMapGrid(viewportData: ViewportData) {

        let gridRegion: MapCore.IMcMapGrid.SGridRegion[] = [];
        gridRegion[0] = new MapCore.IMcMapGrid.SGridRegion();
        runMapCoreSafely(() => {
            gridRegion[0].pGridLine = MapCore.IMcLineItem.Create((MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, MapCore.bcBlackOpaque, 2);
        }, "ViewportService.GetUTMMapGrid=>IMcLineItem.Create", true)
        runMapCoreSafely(() => {
            gridRegion[0].pGridText = MapCore.IMcTextItem.Create((MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN as any).value, MapCore.EMcPointCoordSystem.EPCS_SCREEN, null as any, new MapCore.SMcFVector2D(12, 12));
        }, "ViewportService.GetUTMMapGrid=>IMcTextItem.Create", true)
        gridRegion[0].pGridText.SetTextColor(new MapCore.SMcBColor(255, 0, 0, 255));
        runMapCoreSafely(() => {
            gridRegion[0].pCoordinateSystem = viewportData.viewport.GetCoordinateSystem();
        }, "ViewportService.GetUTMMapGrid=> IMcSpatialQueries.GetCoordinateSystem", true)
        gridRegion[0].GeoLimit.MinVertex = new MapCore.SMcVector3D(0, 0, 0);
        gridRegion[0].GeoLimit.MaxVertex = new MapCore.SMcVector3D(0, 0, 0);

        let basicStep: number = 2000.0;
        let currentStep: number = basicStep;

        let scaleStep: MapCore.IMcMapGrid.SScaleStep[] = [];
        scaleStep[0] = new MapCore.IMcMapGrid.SScaleStep();

        scaleStep[0].fMaxScale = 80;
        scaleStep[0].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        scaleStep[0].NextLineGap = new MapCore.SMcVector2D(currentStep, currentStep);
        scaleStep[0].uNumOfLinesBetweenDifferentTextX = 2;
        scaleStep[0].uNumOfLinesBetweenDifferentTextY = 2;
        scaleStep[0].uNumOfLinesBetweenSameTextX = 2;
        scaleStep[0].uNumOfLinesBetweenSameTextY = 2;
        scaleStep[0].uNumMetricDigitsToTruncate = 3;
        currentStep *= 2;

        scaleStep[1] = new MapCore.IMcMapGrid.SScaleStep();
        scaleStep[1].fMaxScale = 160;
        scaleStep[1].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        scaleStep[1].NextLineGap = new MapCore.SMcVector2D(currentStep, currentStep);
        scaleStep[1].uNumOfLinesBetweenDifferentTextX = 2;
        scaleStep[1].uNumOfLinesBetweenDifferentTextY = 2;
        scaleStep[1].uNumOfLinesBetweenSameTextX = 2;
        scaleStep[1].uNumOfLinesBetweenSameTextY = 2;
        scaleStep[1].uNumMetricDigitsToTruncate = 3;
        currentStep *= 2;

        scaleStep[2] = new MapCore.IMcMapGrid.SScaleStep();
        scaleStep[2].fMaxScale = 320;
        scaleStep[2].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        scaleStep[2].NextLineGap = new MapCore.SMcVector2D(currentStep, currentStep);
        scaleStep[2].uNumOfLinesBetweenDifferentTextX = 2;
        scaleStep[2].uNumOfLinesBetweenDifferentTextY = 2;
        scaleStep[2].uNumOfLinesBetweenSameTextX = 2;
        scaleStep[2].uNumOfLinesBetweenSameTextY = 2;
        scaleStep[2].uNumMetricDigitsToTruncate = 3;
        currentStep *= 2;

        scaleStep[3] = new MapCore.IMcMapGrid.SScaleStep();
        scaleStep[3].fMaxScale = 640;
        scaleStep[3].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        scaleStep[3].NextLineGap = new MapCore.SMcVector2D(currentStep, currentStep);
        scaleStep[3].uNumOfLinesBetweenDifferentTextX = 2;
        scaleStep[3].uNumOfLinesBetweenDifferentTextY = 2;
        scaleStep[3].uNumOfLinesBetweenSameTextX = 2;
        scaleStep[3].uNumOfLinesBetweenSameTextY = 2;
        scaleStep[3].uNumMetricDigitsToTruncate = 3;
        currentStep *= 2;

        scaleStep[4] = new MapCore.IMcMapGrid.SScaleStep();
        scaleStep[4].fMaxScale = 1280;
        scaleStep[4].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        scaleStep[4].NextLineGap = new MapCore.SMcVector2D(currentStep, currentStep);
        scaleStep[4].uNumOfLinesBetweenDifferentTextX = 2;
        scaleStep[4].uNumOfLinesBetweenDifferentTextY = 2;
        scaleStep[4].uNumOfLinesBetweenSameTextX = 2;
        scaleStep[4].uNumOfLinesBetweenSameTextY = 2;
        scaleStep[4].uNumMetricDigitsToTruncate = 3;
        currentStep *= 2;

        scaleStep[5] = new MapCore.IMcMapGrid.SScaleStep();
        scaleStep[5].fMaxScale = 2560;
        scaleStep[5].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        scaleStep[5].NextLineGap = new MapCore.SMcVector2D(currentStep, currentStep);
        scaleStep[5].uNumOfLinesBetweenDifferentTextX = 2;
        scaleStep[5].uNumOfLinesBetweenDifferentTextY = 2;
        scaleStep[5].uNumOfLinesBetweenSameTextX = 2;
        scaleStep[5].uNumOfLinesBetweenSameTextY = 2;
        scaleStep[5].uNumMetricDigitsToTruncate = 3;
        currentStep *= 2;

        scaleStep[6] = new MapCore.IMcMapGrid.SScaleStep();
        scaleStep[6].fMaxScale = MapCore.FLT_MAX;
        scaleStep[6].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        scaleStep[6].NextLineGap = new MapCore.SMcVector2D(currentStep, currentStep);
        scaleStep[6].uNumOfLinesBetweenDifferentTextX = 2;
        scaleStep[6].uNumOfLinesBetweenDifferentTextY = 2;
        scaleStep[6].uNumOfLinesBetweenSameTextX = 2;
        scaleStep[6].uNumOfLinesBetweenSameTextY = 2;
        scaleStep[6].uNumMetricDigitsToTruncate = 3;

        return { region: gridRegion, scale: scaleStep }
    }
    public GetGeoRefMapGrid() {

        let region: MapCore.IMcMapGrid.SGridRegion[] = [];
        let scale: MapCore.IMcMapGrid.SScaleStep[] = [];

        let gridCoordSys: MapCore.IMcGridCoordinateSystem
        runMapCoreSafely(() => { gridCoordSys = MapCore.IMcGridGEOREF.Create(); }, "ViewportService.GetGeoRefMapGrid=> IMcGridGEOREF.Create", true)

        region[0] = new MapCore.IMcMapGrid.SGridRegion();
        region[0].pCoordinateSystem = gridCoordSys;
        region[0].GeoLimit.MinVertex.x = 0;
        region[0].GeoLimit.MinVertex.y = 0;
        region[0].GeoLimit.MaxVertex.x = 0;
        region[0].GeoLimit.MaxVertex.y = 0;
        region[0].uFirstScaleStepIndex = 0;
        region[0].uLastScaleStepIndex = 5;
        region[0].pGridLine = null;
        region[0].pGridText = null;

        scale[0] = new MapCore.IMcMapGrid.SScaleStep();
        scale[0].fMaxScale = 14.64;
        scale[0].NextLineGap = new MapCore.SMcVector2D(833, 833);
        scale[0].uNumOfLinesBetweenDifferentTextX = 2;
        scale[0].uNumOfLinesBetweenDifferentTextY = 2;
        scale[0].uNumOfLinesBetweenSameTextX = 5;
        scale[0].uNumOfLinesBetweenSameTextY = 5;
        scale[0].uNumMetricDigitsToTruncate = 3;
        scale[0].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[1] = new MapCore.IMcMapGrid.SScaleStep();
        scale[1].fMaxScale = 29.296;
        scale[1].NextLineGap = new MapCore.SMcVector2D(833, 833);
        scale[1].uNumOfLinesBetweenDifferentTextX = 2;
        scale[1].uNumOfLinesBetweenDifferentTextY = 2;
        scale[1].uNumOfLinesBetweenSameTextX = 10;
        scale[1].uNumOfLinesBetweenSameTextY = 10;
        scale[1].uNumMetricDigitsToTruncate = 3;
        scale[1].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[2] = new MapCore.IMcMapGrid.SScaleStep();
        scale[2].fMaxScale = 146.48;
        scale[2].NextLineGap = new MapCore.SMcVector2D(20000, 20000);
        scale[2].uNumOfLinesBetweenDifferentTextX = 2;
        scale[2].uNumOfLinesBetweenDifferentTextY = 2;
        scale[2].uNumOfLinesBetweenSameTextX = 2;
        scale[2].uNumOfLinesBetweenSameTextY = 2;
        scale[2].uNumMetricDigitsToTruncate = 3;
        scale[2].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[3] = new MapCore.IMcMapGrid.SScaleStep();
        scale[3].fMaxScale = 723.42;
        scale[3].NextLineGap = new MapCore.SMcVector2D(50000, 50000);
        scale[3].uNumOfLinesBetweenDifferentTextX = 2;
        scale[3].uNumOfLinesBetweenDifferentTextY = 2;
        scale[3].uNumOfLinesBetweenSameTextX = 2;
        scale[3].uNumOfLinesBetweenSameTextY = 2;
        scale[3].uNumMetricDigitsToTruncate = 3;
        scale[3].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[4] = new MapCore.IMcMapGrid.SScaleStep();
        scale[4].fMaxScale = 1464;
        scale[4].NextLineGap = new MapCore.SMcVector2D(100000, 100000);
        scale[4].uNumOfLinesBetweenDifferentTextX = 2;
        scale[4].uNumOfLinesBetweenDifferentTextY = 2;
        scale[4].uNumOfLinesBetweenSameTextX = 2;
        scale[4].uNumOfLinesBetweenSameTextY = 2;
        scale[4].uNumMetricDigitsToTruncate = 3;
        scale[4].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[5] = new MapCore.IMcMapGrid.SScaleStep();
        scale[5].fMaxScale = 2929;
        scale[5].NextLineGap = new MapCore.SMcVector2D(200000, 200000);
        scale[5].uNumOfLinesBetweenDifferentTextX = 2;
        scale[5].uNumOfLinesBetweenDifferentTextY = 2;
        scale[5].uNumOfLinesBetweenSameTextX = 2;
        scale[5].uNumOfLinesBetweenSameTextY = 2;
        scale[5].uNumMetricDigitsToTruncate = 3;
        scale[5].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;
        return { region: region, scale: scale }
    }
    public GetGeoMapGrid() {
        let region: MapCore.IMcMapGrid.SGridRegion[] = [];
        let scale: MapCore.IMcMapGrid.SScaleStep[] = [];

        let gridCoordSys: MapCore.IMcGridCoordinateSystem = MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84);
        region[0] = new MapCore.IMcMapGrid.SGridRegion();
        region[0].pCoordinateSystem = gridCoordSys;
        region[0].GeoLimit.MinVertex.x = 0;
        region[0].GeoLimit.MinVertex.y = 0;
        region[0].GeoLimit.MaxVertex.x = 0;
        region[0].GeoLimit.MaxVertex.y = 0;
        region[0].uFirstScaleStepIndex = 0;
        region[0].uLastScaleStepIndex = 5;
        region[0].pGridLine = null;
        region[0].pGridText = null;

        scale[0] = new MapCore.IMcMapGrid.SScaleStep();
        scale[0].fMaxScale = 14.64;
        scale[0].NextLineGap = new MapCore.SMcVector2D(833, 833);
        scale[0].uNumOfLinesBetweenDifferentTextX = 2;
        scale[0].uNumOfLinesBetweenDifferentTextY = 2;
        scale[0].uNumOfLinesBetweenSameTextX = 5;
        scale[0].uNumOfLinesBetweenSameTextY = 5;
        scale[0].uNumMetricDigitsToTruncate = 3;
        scale[0].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[1] = new MapCore.IMcMapGrid.SScaleStep();
        scale[1].fMaxScale = 29.296;
        scale[1].NextLineGap = new MapCore.SMcVector2D(833, 833);
        scale[1].uNumOfLinesBetweenDifferentTextX = 2;
        scale[1].uNumOfLinesBetweenDifferentTextY = 2;
        scale[1].uNumOfLinesBetweenSameTextX = 10;
        scale[1].uNumOfLinesBetweenSameTextY = 10;
        scale[1].uNumMetricDigitsToTruncate = 3;
        scale[1].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[2] = new MapCore.IMcMapGrid.SScaleStep();
        scale[2].fMaxScale = 146.48;
        scale[2].NextLineGap = new MapCore.SMcVector2D(20000, 20000);
        scale[2].uNumOfLinesBetweenDifferentTextX = 2;
        scale[2].uNumOfLinesBetweenDifferentTextY = 2;
        scale[2].uNumOfLinesBetweenSameTextX = 2;
        scale[2].uNumOfLinesBetweenSameTextY = 2;
        scale[2].uNumMetricDigitsToTruncate = 3;
        scale[2].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[3] = new MapCore.IMcMapGrid.SScaleStep();
        scale[3].fMaxScale = 723.42;
        scale[3].NextLineGap = new MapCore.SMcVector2D(50000, 50000);
        scale[3].uNumOfLinesBetweenDifferentTextX = 2;
        scale[3].uNumOfLinesBetweenDifferentTextY = 2;
        scale[3].uNumOfLinesBetweenSameTextX = 2;
        scale[3].uNumOfLinesBetweenSameTextY = 2;
        scale[3].uNumMetricDigitsToTruncate = 3;
        scale[3].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[4] = new MapCore.IMcMapGrid.SScaleStep();
        scale[4].fMaxScale = 1464;
        scale[4].NextLineGap = new MapCore.SMcVector2D(100000, 100000);
        scale[4].uNumOfLinesBetweenDifferentTextX = 2;
        scale[4].uNumOfLinesBetweenDifferentTextY = 2;
        scale[4].uNumOfLinesBetweenSameTextX = 2;
        scale[4].uNumOfLinesBetweenSameTextY = 2;
        scale[4].uNumMetricDigitsToTruncate = 3;
        scale[4].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;

        scale[5] = new MapCore.IMcMapGrid.SScaleStep();
        scale[5].fMaxScale = 2929;
        scale[5].NextLineGap = new MapCore.SMcVector2D(200000, 200000);
        scale[5].uNumOfLinesBetweenDifferentTextX = 2;
        scale[5].uNumOfLinesBetweenDifferentTextY = 2;
        scale[5].uNumOfLinesBetweenSameTextX = 2;
        scale[5].uNumOfLinesBetweenSameTextY = 2;
        scale[5].uNumMetricDigitsToTruncate = 3;
        scale[5].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DEG_MIN;
        return { region: region, scale: scale }
    }
    public GetMGRSMapGrid() {

        let region: MapCore.IMcMapGrid.SGridRegion[] = [];
        let scale: MapCore.IMcMapGrid.SScaleStep[] = [];
        let gridCoordSys: MapCore.IMcGridCoordinateSystem
        runMapCoreSafely(() => { gridCoordSys = MapCore.IMcGridMGRS.Create(); }, "ViewportService.GetMGRSMapGrid=>IMcGridMGRS.Create", true)
        region[0] = new MapCore.IMcMapGrid.SGridRegion();
        region[0].pCoordinateSystem = gridCoordSys;
        region[0].GeoLimit.MinVertex.x = 0;
        region[0].GeoLimit.MinVertex.y = 0;
        region[0].GeoLimit.MaxVertex.x = 0;
        region[0].GeoLimit.MaxVertex.y = 0;
        region[0].uFirstScaleStepIndex = 0;
        region[0].uLastScaleStepIndex = 0;
        region[0].pGridLine = null;
        region[0].pGridText = null;
        region[1] = new MapCore.IMcMapGrid.SGridRegion();

        region[1].pCoordinateSystem = gridCoordSys;
        region[1].GeoLimit.MinVertex.x = 0;
        region[1].GeoLimit.MinVertex.y = 0;
        region[1].GeoLimit.MaxVertex.x = 0;
        region[1].GeoLimit.MaxVertex.y = 0;
        region[1].uFirstScaleStepIndex = 1;
        region[1].uLastScaleStepIndex = 1;
        region[1].pGridLine = null;
        region[1].pGridText = null;

        scale[0] = new MapCore.IMcMapGrid.SScaleStep();
        scale[0].fMaxScale = 1000;
        scale[0].NextLineGap = new MapCore.SMcVector2D(100000, 100000);
        scale[0].uNumOfLinesBetweenDifferentTextX = 2;
        scale[0].uNumOfLinesBetweenDifferentTextY = 2;
        scale[0].uNumOfLinesBetweenSameTextX = 2;
        scale[0].uNumOfLinesBetweenSameTextY = 2;
        scale[0].uNumMetricDigitsToTruncate = 2;
        scale[0].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;

        scale[1] = new MapCore.IMcMapGrid.SScaleStep();
        scale[1].fMaxScale = 1000;
        scale[1].NextLineGap = new MapCore.SMcVector2D(20000, 20000);
        scale[1].uNumOfLinesBetweenDifferentTextX = 2;
        scale[1].uNumOfLinesBetweenDifferentTextY = 2;
        scale[1].uNumOfLinesBetweenSameTextX = 2;
        scale[1].uNumOfLinesBetweenSameTextY = 2;
        scale[1].uNumMetricDigitsToTruncate = 2;
        scale[1].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
        return { region: region, scale: scale }
    }
    public GetNZMGMapGrid() {
        let region: MapCore.IMcMapGrid.SGridRegion[] = [];
        let scale: MapCore.IMcMapGrid.SScaleStep[] = [];
        let gridCoordSys: MapCore.IMcGridCoordinateSystem = MapCore.IMcGridNZMG.Create();

        region[0] = new MapCore.IMcMapGrid.SGridRegion();
        region[0].pCoordinateSystem = gridCoordSys;
        region[0].GeoLimit.MinVertex.x = 0;
        region[0].GeoLimit.MinVertex.y = 0;
        region[0].GeoLimit.MaxVertex.x = 0;
        region[0].GeoLimit.MaxVertex.y = 0;
        region[0].uFirstScaleStepIndex = 0;
        region[0].uLastScaleStepIndex = 0;
        region[0].pGridLine = null;
        region[0].pGridText = null;

        scale[0] = new MapCore.IMcMapGrid.SScaleStep();
        scale[0].fMaxScale = 100000;
        scale[0].NextLineGap = new MapCore.SMcVector2D(10000, 10000);
        scale[0].uNumOfLinesBetweenDifferentTextX = 2;
        scale[0].uNumOfLinesBetweenDifferentTextY = 2;
        scale[0].uNumOfLinesBetweenSameTextX = 2;
        scale[0].uNumOfLinesBetweenSameTextY = 2;
        scale[0].uNumMetricDigitsToTruncate = 2;
        scale[0].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;

        return { region: region, scale: scale }
    }
    //#endregion
    public doDtmVisualization(viewportId: number): void {

        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            if (!viewportData.viewport.GetDtmVisualization()) {
                let result: { minHeight: number; maxHeight: number; };
                result = this.calcMinMaxHeights(viewportData);
                let DtmVisualization: MapCore.IMcMapViewport.SDtmVisualizationParams = new MapCore.IMcMapViewport.SDtmVisualizationParams();
                if (result) {
                    runMapCoreSafely(() => {
                        MapCore.IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors(DtmVisualization, result.minHeight, result.maxHeight);
                    }, "ViewportService.doDtmVisualization=>IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors", true)
                }
                else {
                    runMapCoreSafely(() => {
                        MapCore.IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors(DtmVisualization);
                    }, "ViewportService.doDtmVisualization=>IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors", true)
                }
                DtmVisualization.bDtmVisualizationAboveRaster = true;
                DtmVisualization.uHeightColorsTransparency = 120;
                DtmVisualization.uShadingTransparency = 255;
                runMapCoreSafely(() => {
                    viewportData.viewport.SetDtmVisualization(true, DtmVisualization);
                }, "ViewportService.doDtmVisualization=>IMcMapViewport.SetDtmVisualization", true)

            }
            else {
                runMapCoreSafely(() => { viewportData.viewport.SetDtmVisualization(false); }, "ViewportService.doDtmVisualization=>IMcMapViewport.SetDtmVisualization", true)
            }
        }
    }
    public doHeighLinesVisualization(viewportId: number): void {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        let mapHeightLines: MapCore.IMcMapHeightLines = viewportData.viewport.GetHeightLines();
        if (viewportData) {
            if (!viewportData.viewport.GetHeightLinesVisibility() || mapHeightLines == null) {
                if (mapHeightLines == null) {
                    // create height lines scale step and populate colors

                    // 1 color
                    // McBColor[] scaleColorsFirst
                    let scaleColorsFirst: MapCore.SMcBColor[] = [];
                    scaleColorsFirst[0] = new MapCore.SMcBColor(255, 0, 0, 255);
                    scaleColorsFirst[1] = new MapCore.SMcBColor(0, 255, 0, 255);
                    scaleColorsFirst[2] = new MapCore.SMcBColor(0, 0, 255, 255);

                    // 2 color 
                    let scaleColorsSecond: MapCore.SMcBColor[] = [];
                    scaleColorsSecond[0] = new MapCore.SMcBColor(255, 255, 0, 255);
                    scaleColorsSecond[1] = new MapCore.SMcBColor(0, 255, 255, 255);
                    scaleColorsSecond[2] = new MapCore.SMcBColor(255, 0, 255, 255);

                    // height-lines scale step
                    let heightLinesScaleStep: MapCore.IMcMapHeightLines.SScaleStep[] = [];
                    heightLinesScaleStep[0] = new MapCore.IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[0].fMaxScale = 10;
                    heightLinesScaleStep[0].fLineHeightGap = 10 as any;
                    heightLinesScaleStep[0].aColors = scaleColorsFirst;

                    heightLinesScaleStep[1] = new MapCore.IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[1].fMaxScale = 25;
                    heightLinesScaleStep[1].fLineHeightGap = 25 as any;
                    heightLinesScaleStep[1].aColors = scaleColorsSecond;

                    heightLinesScaleStep[2] = new MapCore.IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[2].fMaxScale = 50;
                    heightLinesScaleStep[2].fLineHeightGap = 50 as any;
                    heightLinesScaleStep[2].aColors = scaleColorsFirst;

                    heightLinesScaleStep[3] = new MapCore.IMcMapHeightLines.SScaleStep();
                    heightLinesScaleStep[3].fMaxScale = 300;
                    heightLinesScaleStep[3].fLineHeightGap = 100 as any;
                    heightLinesScaleStep[3].aColors = scaleColorsSecond;

                    let fLineWidth: number = 1.0;
                    let uNumScaleSteps: number = heightLinesScaleStep.length;

                    // create IMcMapHeightLines mapHeightLines = null;
                    mapHeightLines = MapCore.IMcMapHeightLines.Create(heightLinesScaleStep as any, fLineWidth);//to do Argument of type 'SScaleStep[]' is not assignable to parameter of type 'SScaleStep'.
                    // mapHeightLines.AddRef();
                    mapcoreData.heightLinesArr.push(mapHeightLines);
                    //let curr_scale_step = mapHeightLines.GetScaleSteps();
                    runMapCoreSafely(() => {
                        viewportData.viewport.SetHeightLines(mapHeightLines);
                    }, "IMcMapViewport.SetHeightLines=>IMcMapViewport.SetHeightLines", true)
                    //let curr_height_lines = viewport.GetHeightLines();
                    //let line_width = mapHeightLines.GetLineWidth();
                }

                runMapCoreSafely(() => {
                    viewportData.viewport.SetHeightLinesVisibility(true);
                }, "IMcMapViewport.SetHeightLines=>IMcMapViewport.SetHeightLinesVisibility", true)
            }
            else {
                runMapCoreSafely(() => {
                    viewportData.viewport.SetHeightLines(null);
                    viewportData.viewport.SetHeightLinesVisibility(false);
                }, "IMcMapViewport.SetHeightLines=>IMcMapViewport.SetHeightLinesVisibility", true)
            }
        }
    }
    private getLayers(viewport: MapCore.IMcMapViewport): MapCore.IMcMapLayer[] {
        const terrains: MapCore.IMcMapTerrain[] = viewport.GetTerrains();
        return terrains.length > 0
            ? terrains[0].GetLayers() : [];
    }
    public viewport3D(viewportId: number): boolean {
        let viewportData: ViewportData = null
        viewportData = MapCoreData.findViewport(viewportId);
        if (viewportData)
            if (viewportData.viewport?.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                return true
            }
        return false;
    }
    public KeyDownOnMap(keyboard: string, IsShiftPressed: boolean, IsControlPressed: boolean, viewportId: number, EMPropOneOperationOnly: boolean) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            let pressedKey: string = keyboard
            // this.EditMode != null &&
            if (viewportData.editMode.IsEditingActive() == true) {
                switch (pressedKey) {
                    case "Escape":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_ABORT, viewportData, EMPropOneOperationOnly);
                        break;
                    case "NumpadEnter":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_CONFIRM, viewportData, EMPropOneOperationOnly);
                        break;
                    case "Enter":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_CONFIRM, viewportData, EMPropOneOperationOnly);
                        break;
                    case "Delete":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_DELETE_VERTEX, viewportData, EMPropOneOperationOnly);
                        break;
                    case "PageDown":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_LOWER, viewportData, EMPropOneOperationOnly);
                        break;
                    case "NumpadAdd":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_NEXT_ICON, viewportData, EMPropOneOperationOnly);
                        break;
                    case "NumpadSubtract":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_PREV_ICON, viewportData, EMPropOneOperationOnly);
                        break;
                    case "PageUp":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_RAISE, viewportData, EMPropOneOperationOnly);
                        break;
                    case "Numpad4":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_ROTATE_LEFT, viewportData, EMPropOneOperationOnly);
                        break;
                    case "Numpad6":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_ROTATE_RIGHT, viewportData, EMPropOneOperationOnly);
                        break;
                    case "Numpad8":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_ROTATE_UP, viewportData, EMPropOneOperationOnly);
                        break;
                    case "Numpad2":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_ROTATE_DOWN, viewportData, EMPropOneOperationOnly);
                        break;
                }
            }
            else {
                switch (pressedKey) {
                    case "ArrowLeft":
                        this.MoveCamera(-MovementSensitivity, 0, 0, viewportData);
                        break;
                    case "ArrowRight":
                        this.MoveCamera(MovementSensitivity, 0, 0, viewportData);
                        break;
                    case "ArrowUp":
                        this.MoveCamera(0, MovementSensitivity, 0, viewportData);
                        break;
                    case "ArrowDown":
                        this.MoveCamera(0, -MovementSensitivity, 0, viewportData);
                        break;
                }

                // keys that work only on 3D map
                if (viewportData.mapType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                    let RotationFactor = MovementSensitivity * m_RotationFactor;
                    switch (pressedKey) {
                        case "Numpad4":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(-RotationFactor, 0, 0); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            break;
                        case "Numpad6":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(RotationFactor, 0, 0); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            break;
                        case "Numpad2":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(0, -RotationFactor, 0); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            break;
                        case "Numpad8":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(0, RotationFactor, 0); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            break;
                        case "Numpad3":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(0, 0, RotationFactor); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            break;
                        case "Numpad1":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(0, 0, -RotationFactor); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            break;
                        case "Numpad7":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(-RotationFactor, 0, 0); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            this.MoveCamera(0, MovementSensitivity, 0, viewportData);
                            break;
                        case "Numpad9":
                            runMapCoreSafely(() => { viewportData.viewport.RotateCameraRelativeToOrientation(RotationFactor, 0, 0); }, "viewportService.KeyDownOnMap => IMcMapCamera.RotateCameraRelativeToOrientation", true);
                            this.MoveCamera(0, MovementSensitivity, 0, viewportData);
                            break;
                        case "PageUp":
                            this.MoveCamera(0, 0, MovementSensitivity, viewportData);
                            break;
                        case "PageDown":
                            this.MoveCamera(0, 0, -MovementSensitivity, viewportData);
                            break;
                        case "Numpad0":
                            runMapCoreSafely(() => { viewportData.viewport.SetCameraOrientation(0, -90, 0, false); }, "viewportService.KeyDownOnMap => IMcMapCamera.SetCameraOrientation", true);
                            break;
                    }
                }

                // Show/Hide map tiles 
                if (IsShiftPressed == true) {
                    let debugOptionsKey: number = MapCore.UINT_MAX;
                    let debugOptionsVal: number = 0;
                    if (pressedKey == "KeyD") {

                        debugOptionsKey = 0/*ELO_BOX_DRAW_MODE*/;
                        runMapCoreSafely(() => { debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey); }, "viewportService.KeyDownOnMap => IMcSpatialQueries.GetDebugOption", true);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == "F1") {
                        alert(IsShiftPressed + "jjj" + pressedKey)

                        debugOptionsKey = 9/*ELO_RENDER_ONLY_SELECTED_PASS*/;
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == "KeyB") {
                        debugOptionsKey = 2/*ELO_OVERLAY_OBJECTS_BOX_DRAW_MODE*/;
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);

                        if (debugOptionsVal == 0) {
                            debugOptionsVal = 1;
                        }
                        else {
                            debugOptionsVal = 0;
                        }
                    }

                    if (pressedKey == "KeyW") {
                        debugOptionsKey = 21; //ELO_WIREFRAME_MODE
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == "KeyF") {
                        debugOptionsKey = 3; //ELO_FREEZE_TREE
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }

                    if (pressedKey == "KeyS") {
                        debugOptionsKey = 24; //ELO_VIEWPORT_STATS
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }

                    if (pressedKey == "KeyV") {
                        debugOptionsKey = 25; //ELO_BOX_DRAW_MODE_TYPE
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 3;
                    }

                    if (pressedKey == "KeyT") {
                        debugOptionsKey = 26; //ELO_TEXT_SHADER
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }
                    if ((pressedKey == "KeyS") && (IsControlPressed == true)) {
                        debugOptionsKey = 4; //ELO_SAVE_INTERSECTING_TILE
                        debugOptionsVal = viewportData.viewport.GetDebugOption(debugOptionsKey);
                        debugOptionsVal = (debugOptionsVal + 1) % 2;
                    }
                    if (debugOptionsKey != MapCore.UINT_MAX) {
                        runMapCoreSafely(() => { viewportData.viewport.SetDebugOption(debugOptionsKey, debugOptionsVal); }, "viewportService.KeyDownOnMap => IMcSpatialQueries.SetDebugOption", true);
                    }
                }
                // Change movement sensitivity
                if (IsControlPressed == true && (pressedKey == "Digit1" ||
                    pressedKey == "Digit2" ||
                    pressedKey == "Digit3" ||
                    pressedKey == "Digit4" ||
                    pressedKey == "Digit5")) {
                    switch (pressedKey) {
                        case "Digit1":
                            MovementSensitivity = 50;
                            break;
                        case "Digit2":
                            MovementSensitivity = 75;
                            break;
                        case "Digit3":
                            MovementSensitivity = 100;
                            break;
                        case "Digit4":
                            MovementSensitivity = 250;
                            break;
                        case "Digit5":
                            MovementSensitivity = 750;
                            break;
                    }
                }
            }
        }
    }
    public KeyUpOnMap(pressedKey: string, viewportId: number, EMPropOneOperationOnly: boolean) {
        const viewportData: ViewportData = MapCoreData.findViewport(viewportId);
        if (viewportData) {
            if (viewportData.editMode != null && viewportData.editMode.IsEditingActive() == true) {
                //The arrow keys are special keys that catch only on key up. 
                switch (pressedKey) {
                    case "ArrowUp":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_MOVE_UP, viewportData, EMPropOneOperationOnly);
                        break;
                    case "ArrowDown":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_MOVE_DOWN, viewportData, EMPropOneOperationOnly);
                        break;
                    case "ArrowLeft":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_MOVE_LEFT, viewportData, EMPropOneOperationOnly);
                        break;
                    case "ArrowRight":
                        this.EditModeOnKeyEventCall(MapCore.IMcEditMode.EKeyEvent.EKE_MOVE_RIGHT, viewportData, EMPropOneOperationOnly);
                        break;
                }
            }
        }
    }
    public MoveCamera(deltaX: number, deltaY: number, deltaZ: number, viewportData: ViewportData) {
        let DeltaPosition: MapCore.SMcVector3D = new MapCore.SMcVector3D(deltaX, deltaY, deltaZ);
        if (viewportData.viewport == null) {
            return;
        }
        if (viewportData.mapType == MapCore.IMcMapCamera.EMapType.EMT_2D) {
            runMapCoreSafely(() => { viewportData.viewport.MoveCameraRelativeToOrientation(DeltaPosition, false); }, "ViewportService.MoveCamera=> IMcMapCamera.MoveCameraRelativeToOrientation", true)
        }
        if (viewportData.mapType == MapCore.IMcMapCamera.EMapType.EMT_3D) {
            runMapCoreSafely(() => { viewportData.viewport.MoveCameraRelativeToOrientation(DeltaPosition, false); }, "ViewportService.MoveCamera=> IMcMapCamera.MoveCameraRelativeToOrientation", true)
        }
    }
    public EditModeOnKeyEventCall(keyEvent: MapCore.IMcEditMode.EKeyEvent, viewportData: ViewportData, EMPropOneOperationOnly: boolean) {
        let RenderNeeded: any = {}
        runMapCoreSafely(() => { viewportData.editMode.OnKeyEvent(keyEvent, RenderNeeded); }, "ViewportService.EditModeOnKeyEventCall=> IMcEditMode.OnKeyEvent", true);

        // only if edit mode operation was happened and it is oneOperationOnly
        // if (m_RenderNeeded == true) {
        // if (EMPropOneOperationOnly == true || EditMode.IsEditingActive == false)
        //         Program.AppMainForm.EditModeNavigationButtonState = false;

        // }

        if (RenderNeeded != null)
            editModeService.AddRenderToList(RenderNeeded.Value);
    }
    setCameraPosition(viewport: MapCore.IMcMapViewport, position: MapCore.SMcVector3D) {

        if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D) {
            runMapCoreSafely(() => { viewport.SetCameraPosition(position); }, "ViewportService.setCameraPosition=> IMcMapCamera.SetCameraPosition", true)
        }
        else // 3D
        {
            let height: any = {};
            runMapCoreSafely(() => { viewport.SetCameraPosition(position); }, "ViewportService.setCameraPosition=> IMcMapCamera.SetCameraPosition", true)
            let params: MapCore.IMcSpatialQueries.SQueryParams = new MapCore.IMcSpatialQueries.SQueryParams();
            params.eTerrainPrecision = MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST;
            params.pAsyncQueryCallback = new mapcoreData.iMcQueryCallbackClass(
                (bHeightFound: any, height: any, normal: any) => {
                    position.z = (bHeightFound ? height : 100) + 20;
                    if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
                        runMapCoreSafely(() => { viewport.SetCameraPosition(position); }, "ViewportService.setCameraPosition=>IMcMapCamera.SetCameraPosition", true)
                        runMapCoreSafely(() => { viewport.SetCameraClipDistances(1, 0, true) }, "ViewportService.setCameraPosition=> IMcMapCamera.SetCameraClipDistances", true)
                        runMapCoreSafely(() => { viewport.SetCameraOrientation(0, 0, 0, false) }, "ViewportService.setCameraPosition=> IMcMapCamera.SetCameraOrientation", true)
                    }
                }
            );
            runMapCoreSafely(() => {
                viewport.GetTerrainHeight(position, height, null, params);
            }, "ViewportService.setCameraPosition=> IMcSpatialQueries.GetTerrainHeight", true)
        }
    }
    private viewportScreenToWorld(Viewport: MapCore.IMcMapViewport, ScreenPoint: MapCore.SMcVector3D) {
        let bIntersection: boolean = false;
        let WorldCenter: any = {};
        if (Viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D) {
            runMapCoreSafely(() => {
                bIntersection = Viewport.ScreenToWorldOnTerrain(ScreenPoint, WorldCenter);
            }, "ViewportService.viewportScreenToWorld=> IMcMapCamera.ScreenToWorldOnTerrain", true)
        }
        if (!bIntersection) // EMT_2D || !bIntersection
        {
            runMapCoreSafely(() => {
                bIntersection = Viewport.ScreenToWorldOnPlane(ScreenPoint, WorldCenter);
            }, "ViewportService.viewportScreenToWorld=> IMcMapCamera.ScreenToWorldOnTerrain", true)
        }
        return (bIntersection ? WorldCenter.Value : null);
    }
    private calcMinMaxHeights(viewportData: ViewportData) {
        let minHeight: number = undefined;
        let maxHeight: number = undefined;
        let fp: MapCore.IMcMapCamera.SCameraFootprintPoints;
        runMapCoreSafely(() => {
            fp = viewportData.viewport.GetCameraFootprint();
        }, "ViewportService.calcMinMaxHeights=> IMcMapCamera.GetCameraFootprint", true)

        if (fp.bUpperLeftFound && fp.bUpperRightFound && fp.bLowerRightFound && fp.bLowerLeftFound) {
            let minPoint: any = {}, maxPoint: any = {};
            let ExtremeHeightPoints: boolean;
            runMapCoreSafely(() => {
                ExtremeHeightPoints = viewportData.viewport.GetExtremeHeightPointsInPolygon([fp.UpperLeft, fp.UpperRight, fp.LowerRight, fp.LowerLeft], maxPoint, minPoint)
            }, "ViewportService.calcMinMaxHeights=> IMcSpatialQueries.GetExtremeHeightPointsInPolygon", true)

            if (ExtremeHeightPoints) {
                minHeight = minPoint.Value.z;
                maxHeight = maxPoint.Value.z;
                if (maxHeight <= minHeight + 1) {
                    maxHeight = minHeight + 1;
                }
            }
        }
        return minHeight ? { minHeight: minHeight, maxHeight: maxHeight } : undefined;
    }
}

export default new ViewportService();
