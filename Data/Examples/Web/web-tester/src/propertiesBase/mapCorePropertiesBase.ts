class MapCorePropertiesBase {
    deviceInitParams: MapCore.IMcMapDevice.SInitParams;
    SCreateData:MapCore.IMcMapViewport.SCreateData;

    constructor() {
        this.deviceInitParams = new MapCore.IMcMapDevice.SInitParams();
        this.SCreateData=new MapCore.IMcMapViewport.SCreateData(MapCore.IMcMapCamera.EMapType.EMT_2D);
        
    }
}
export default MapCorePropertiesBase 