import React, { PureComponent } from 'react';
import { ViewportService, MapCoreData, Layerslist, MapCoreService } from 'mapcore-lib';
import { enums } from 'mapcore-lib';
import ApplicationContext from '../../context/applicationContext';
import cn from './MapContainer.module.css';
import config, { mapActions } from '../../config';
import { connect } from "react-redux";
import { runCodeSafely } from '../../errorHandler';
import { LayerNameEnum } from 'mapcore-lib';
import { SaveMapToPreview, SetActionDtmMapPreview, SetCameraData, SetErrorInPreview } from '../../redux/MapContainer/MapContainerAction';

class MapContainer extends PureComponent {
  static contextType = ApplicationContext;
  constructor(props) {
    super(props)

    this.isViewPortHasPendingUpdates = null;
    this.flagCloseMap = false;

    this.state = {
      prevCameraData: { prevScale: null, prevPosition: null, Yaw: 0, Pitch: 0, Roll: 0 }
    }
  }
  componentDidMount() {
    this.props.SetActionDtmMapPreview(false)
    this.canvasRef = React.createRef();
    MapCoreService.loadServerLayers((serverLayers) => {
      this.openMap(window.MapCore.IMcMapCamera.EMapType.EMT_2D, false);
    }, config.urls.getCapabilities, window.MapCore.IMcMapLayer.EWebMapServiceType.EWMS_MAPCORE);
  }
  componentDidUpdate(prevProps) {
    if (prevProps.mapToPreview !== this.props.mapToPreview) {
      this.props.SetActionDtmMapPreview(false)
      this.openMap(window.MapCore.IMcMapCamera.EMapType.EMT_2D, false);
    }
  }
  componentWillUnmount() {
    if (!this.flagCloseMap) {
      this.closeMap();
    }
  }
  openMap = async (mapType, isSetCameraDetails, isDtmActivated) => {
    this.closeMap();
    this.flagCloseMap = false;
    this.context.setMapCoreSDKLoaded();
    let layerArr = this.createLayerToPreview()
    try {
      await this.props.openMapService.addNewViewportStandard
        (1,
          new Layerslist([], layerArr),
          this.canvasRef.current,
          { mapType: mapType, terrainFactor: 1, showGeo: false },
          () => {
            this.ViewportResized(mapType, isSetCameraDetails, isDtmActivated)
          },
          isSetCameraDetails
        )
    } catch (error) {
      this.closeMap()
    }

    let interval1 = setInterval(() => {
      if (this.props.openMapService.coordinateSystem != null) {
        clearInterval(interval1)
        this.props.openMapService.continueCreating();
      }
    }, 100);

    let interval2 = setInterval(() => {
      if (this.props.openMapService.areAllLayersInitialized() && MapCoreData.findViewport(1)?.viewport != null) {
        clearInterval(interval2)
        ViewportService.doCenterPoint(1);
      }
    }, 100);
  }

  convertLayerNameToEnum(str) {
    if (str.endsWith("_RASTER"))
      return LayerNameEnum.NativeServerRaster
    if (str.endsWith("_DTM"))
      return LayerNameEnum.NativeServerDtm
    if (str.endsWith("_3D_MODEL"))
      return LayerNameEnum.NativeServer3DModel
    if (str.endsWith("_VECTOR"))
      return LayerNameEnum.NativeServerVector
    if (str.endsWith("_VECTOR_3D_EXTRUSION"))
      return LayerNameEnum.NativeServerVector3DExtrusion
    if (str.endsWith("_TRAVERSABILITY"))
      return LayerNameEnum.NativeServerTraversability
    if (str.endsWith("_MATERIAL"))
      return LayerNameEnum.NativeServerMaterial;
    console.error("Layer Name Not Found");

  }
  ViewportResized(mapType, isSetCameraDetails, isDtmActivated) {
    this.canvasRef.current.addEventListener('wheel', this.mouseWheelHandler, { passive: false });
    MapCoreData.viewportsData[0].viewport.SetBackgroundColor(window.MapCore.SMcBColor(70, 70, 70, 255));
    let width = document.getElementById('canvasesContainer').getBoundingClientRect().width;
    let height = document.getElementById('canvasesContainer').getBoundingClientRect().height;
    MapCoreData.viewportsData[0].canvas.width = width;
    MapCoreData.viewportsData[0].canvas.height = height;
    MapCoreData.viewportsData[0].viewport.ViewportResized();

    this.SetDisplayingDtmVisualizationOn3Dlayer()
    // this.doDtmVisualization()
    if (mapType == window.MapCore.IMcMapCamera.EMapType.EMT_2D)
      MapCoreData.viewportsData[0].viewport.SetVector3DExtrusionVisibilityMaxScale(50);
    MapCoreData.viewportsData[0].viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_UPDATE, true, 50);
    MapCoreData.viewportsData[0].viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_CONDITION, true, 50);
    MapCoreData.viewportsData[0].viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_SIZE, true, 5);
    MapCoreData.viewportsData[0].viewport.SetObjectsDelay(window.MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_HEIGHT, true, 50);
    this.setCameraDetails(mapType, isSetCameraDetails)
    if (isDtmActivated && !this.isDtmLayerPreview()) {
      this.doDtmVisualization()
    }
    if (!this.isViewPortHasPendingUpdates) {
      this.isViewPortHasPendingUpdates = setInterval(() => {
        if (!MapCoreData?.viewportsData[0]?.viewport?.HasPendingUpdates()) {
          if (this.isDtmLayerPreview()) {//|| isDtmActivated 
            this.doDtmVisualization()
          }
          clearInterval(this.isViewPortHasPendingUpdates);
          this.isViewPortHasPendingUpdates = null;
        }
      }, 300);
    }
  }
  isDtmLayerPreview() {
    return this.props.mapToPreview.data?.LayerType?.includes('DTM') ||
      (this.props.mapToPreview.data?.childNodes?.length == 1 && this.props.mapToPreview.data?.childNodes[0].LayerType?.includes('DTM'));
  }
  SetDisplayingDtmVisualizationOn3Dlayer() {
    let terrains = MapCoreData.viewportsData[0].viewport.GetTerrains();
    terrains.forEach(terrain => {
      let layers = terrain.GetLayers()
      layers.forEach(
        layer => {
          if ((layer?.GetLayerType() == window.MapCore.IMcNativeVector3DExtrusionMapLayer.LAYER_TYPE) ||
            (layer?.GetLayerType() == window.MapCore.IMcNativeServerVector3DExtrusionMapLayer.LAYER_TYPE) ||
            (layer?.GetLayerType() == window.MapCore.IMcRawVector3DExtrusionMapLayer.LAYER_TYPE) ||
            (layer?.GetLayerType() == window.MapCore.IMcNative3DModelMapLayer.LAYER_TYPE) ||
            (layer?.GetLayerType() == window.MapCore.IMcNativeServer3DModelMapLayer.LAYER_TYPE) ||
            (layer?.GetLayerType() == window.MapCore.IMcRaw3DModelMapLayer.LAYER_TYPE)) {
            layer.SetDisplayingDtmVisualization(true)
          }
        })
    })
  }
  createLayerToPreview() {
    let layerArr = [];
    let MAP = {};
    let newlayer;
    MAP = this.props.mapToPreview;
    let layersCallback = new MapCoreData.layersCallback()
    //one group or one layer preview
    if (MAP.type == "layer" && MAP.data.LayerType == 'RAW_VECTOR_3D_EXTRUSION') {
      newlayer = {
        path: MAP.data.RawLayerInfo.Vector3DExt.DtmLayerId, layerSource: enums.LayerSourceEnum.MAPCORE,
        name: this.convertLayerNameToEnum(MAP.data.LayerType), layerParams: { urlServer: MapCoreData.astrServiceMetadataURLs },
        layerIReadCallback: layersCallback
      }
      layerArr.push(newlayer)
    }
    if (MAP.type == "layer" && MAP.data.LayerType.endsWith("_DTM"))
      this.props.SetActionDtmMapPreview(mapActions.SHOW_DTM_MAP)
    if (MAP.type == "group") {
      for (let index = 0; index < MAP.data.childNodes.length; index++) {
        newlayer = { path: MAP.data.childNodes[index].LayerId, layerSource: enums.LayerSourceEnum.MAPCORE, name: this.convertLayerNameToEnum(MAP.data.childNodes[index].LayerType), layerParams: { urlServer: MapCoreData.astrServiceMetadataURLs }, layerIReadCallback: layersCallback }
        layerArr.push(newlayer)
        if (MAP.data.childNodes.length == 1 && MAP.data.childNodes[0].LayerType.endsWith("_DTM"))
          this.props.SetActionDtmMapPreview(mapActions.SHOW_DTM_MAP)
      }
    }
    if (MAP.type == "layer") {
      let newlayer = { path: MAP.data.LayerId, layerSource: enums.LayerSourceEnum.MAPCORE, name: this.convertLayerNameToEnum(MAP.data.LayerType), layerParams: { urlServer: MapCoreData.astrServiceMetadataURLs }, layerIReadCallback: layersCallback }
      layerArr.push(newlayer)
    }
    if (MAP.type == "group and layer") {//for all other options of multi selection. MAP.data[0]=groups, MAP.data[1]=layers
      let flatedLayersToPreviewArr = [...MAP.data[0].map(g => g.childNodes).flat(), ...MAP.data[1]];
      for (let index = 0; index < flatedLayersToPreviewArr.length; index++) {
        newlayer = { path: flatedLayersToPreviewArr[index].LayerId, layerSource: enums.LayerSourceEnum.MAPCORE, name: this.convertLayerNameToEnum(flatedLayersToPreviewArr[index].LayerType), layerParams: { urlServer: MapCoreData.astrServiceMetadataURLs }, layerIReadCallback: layersCallback }
        layerArr.push(newlayer)
      }
    }
    return layerArr;
  }

  closeMap() {
    this.flagCloseMap = true;
    ViewportService.closeViewport(1);
    if (this.canvasRef.current)
      this.canvasRef.current.removeEventListener('wheel', this.mouseWheelHandler);
  }
  saveCameraDetails() {
    this.state.prevCameraData.prevPosition = MapCoreData.viewportsData[0]?.viewport?.GetCameraPosition();
    let Yaw = {}
    let Pitch = {}
    let Roll = {}
    if (MapCoreData.viewportsData[0].mapType == window.MapCore.IMcMapCamera.EMapType.EMT_3D) {

      MapCoreData.viewportsData[0].viewport.GetCameraOrientation(Yaw, Pitch, Roll);
      if (Pitch)
        this.state.prevCameraData.Pitch = Pitch.Value
      if (Roll)
        this.state.prevCameraData.Roll = Roll.Value
    }
    if (MapCoreData.viewportsData[0].mapType == window.MapCore.IMcMapCamera.EMapType.EMT_2D) {
      MapCoreData.viewportsData[0].viewport.GetCameraOrientation(Yaw);
      this.state.prevCameraData.prevScale = MapCoreData.viewportsData[0].viewport.GetCameraScale()
    }
    if (Yaw)
      this.state.prevCameraData.Yaw = Yaw.Value
    this.props.SetCameraData(this.state.prevCameraData)
  }

  setCameraDetails(mapType, isSetCameraDetails) {
    this.state.prevCameraData = this.props.prevCameraData;
    if (this.state.prevCameraData.prevPosition && isSetCameraDetails) {
      MapCoreData.viewportsData[0].viewport.SetCameraPosition(this.state.prevCameraData.prevPosition);
      MapCoreData.viewportsData[0].viewport.SetCameraOrientation(this.state.prevCameraData.Yaw,
        this.state.prevCameraData.Pitch, this.state.prevCameraData.Roll)
      let Yaw = {}
      let Pitch = {}
      let Roll = {}
      MapCoreData.viewportsData[0].viewport.GetCameraOrientation(Yaw, Pitch, Roll);
      if (mapType == window.MapCore.IMcMapCamera.EMapType.EMT_2D)
        MapCoreData.viewportsData[0].viewport.SetCameraScale(this.state.prevCameraData.prevScale);
    }
  }

  doDtmVisualization(activeDtmButton = false) {
    // if (this.props.activeDtmMapPreview != false || activeDtmButton) {
    ViewportService.doDtmVisualization(1);
    // }
  }
  // #region mouse event
  mouseMoveHandler = (e) => {
    runCodeSafely(() => {
      this.setCursorInCss(ViewportService.mouseMoveHandler(e, 1))
    }, "mapWindow.mouseMoveHandler");
  }

  mouseWheelHandler = (e) => {
    runCodeSafely(() => {
      this.setCursorInCss(
        ViewportService.mouseWheelHandler(e, 1)
      )
    }, "mapWindow.mouseWheelHandler");
  }

  mouseDownHandler = (e) => {
    runCodeSafely(() => {
      this.setCursorInCss(
        ViewportService.mouseDownHandler(e, 1)
      )
    }, "mapWindow.mouseDownHandler");
  }
  showCursorPosition = (e) => {
    runCodeSafely(() => {
      let obj = ViewportService.showCursorPosition(e, 1, false, false)
    }, "mapWindow.showCursorPosition");
  }
  mouseUpHandler = (e) => {
    runCodeSafely(() => {
      this.setCursorInCss(
        ViewportService.mouseUpHandler(e, 1)
      )
    }, "mapWindow.mouseUpHandler");
  }
  mouseDblClickHandler = (e) => {
    runCodeSafely(() => {
      this.setCursorInCss(
        ViewportService.mouseDblClickHandler(e, 1)
      )
    }, "mapWindow.mouseDblClickHandler");
  }
  setCursorInCss = (Ecursor) => {
    if (Ecursor) {
      let cssCursor = "auto"
      let numCursor = Ecursor.Value.value;
      switch (numCursor) {
        case 0:
          cssCursor = "auto";
          break;
        case 1:
          cssCursor = "grab";
          break;
        case 2:
          cssCursor = "move";
          break;
        case 3:
          cssCursor = "crosshair";
          break;
      }
      this.canvasRef.current.style.cursor = cssCursor;
    }
  }
  //#endregion
  renderLoadingMessage() {
    return (
      <div className={cn.LoadingMessage}>
        Map core SDK is Loading...
      </div>
    )
  }
  render() {
    const contextType = this.ApplicationContext;
    return (
      <div className={cn.Wrapper}>
        {this.context.isMapCoreSDKLoaded ?
          <div className={cn.CanvasContainer} id='canvasesContainer' >
            <canvas ref={this.canvasRef}
              onMouseMove={(e) => { this.mouseMoveHandler(e) }}
              onMouseDown={(e) => { this.mouseDownHandler(e) }}
              onClick={(e) => { this.showCursorPosition(e) }}
              onMouseUp={(e) => { { this.mouseUpHandler(e) } }}
              onDoubleClick={(e) => { { this.mouseDblClickHandler(e) } }}
            // onWheel={(e) => { { this.mouseWheelHandler(e) } }}
            >  </canvas>
          </div> :
          this.renderLoadingMessage()}
      </div>

    );
  }
}
const mapStateToProps = (state) => {
  return {
    mapToPreview: state.MapContainerReducer.mapToPreview,
    activeMapPreview: state.MapContainerReducer.activeMapPreview,
    activeDtmMapPreview: state.MapContainerReducer.activeDtmMapPreview,
    prevCameraData: state.MapContainerReducer.prevCameraData,
    errorInPreview: state.MapContainerReducer.errorInPreview,
    openMapService: state.MapContainerReducer.openMapService,
  }
};
const mapDispatchToProps = (dispacth) => {
  return {
    setMapPreview: () => dispacth(SaveMapToPreview({})),
    SetActionDtmMapPreview: (activeMapPreview) => dispacth(SetActionDtmMapPreview(activeMapPreview)),
    SetCameraData: (prevCameraData) => dispacth(SetCameraData(prevCameraData)),
    SetErrorInPreview: (error) => dispacth(SetErrorInPreview(error)),

  }
};
export default connect(mapStateToProps, mapDispatchToProps, null, { forwardRef: true })(MapContainer);
