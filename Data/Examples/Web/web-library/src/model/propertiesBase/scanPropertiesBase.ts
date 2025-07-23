
class ScanPropertiesBase {

        scanType: string = 'PolyScan';
        SpecificPointX: number = 0; SpecificPointY: number = 0; SpecificPointZ: number = 0;
        CompletelyInside: boolean = false;
        Tolerance: number = 0;
        coordSys: MapCore.EMcPointCoordSystem = MapCore.EMcPointCoordSystem.EPCS_SCREEN;
        SpatialQueryParams: MapCore.IMcSpatialQueries.SQueryParams;
        constructor() {
                this.SpatialQueryParams = new MapCore.IMcSpatialQueries.SQueryParams()
        }
}
export default ScanPropertiesBase;
