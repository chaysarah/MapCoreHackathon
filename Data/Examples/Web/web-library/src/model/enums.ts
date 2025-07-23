export enum LayerNameEnum {
    NativeRaster = "Native Raster",
    NativeDtm = "Native DTM",
    NativeVector = "Native Vector",
    NativeVector3DExtrrusion = "Native Vector 3D Extrusion",
    NativeVectorListFile = "Native Vector List File",
    NativeHeat = "Native Heat",
    NativeMaterial = "Native Material",
    NativeTraversability = "Native Traversability",
    Native3DModel = "Native 3D Model",
    NativeServerRaster = "Native Server Raster",
    NativeServerDtm = "Native Server DTM",
    NativeServerVector = "Native Server Vector",
    NativeServerVector3DExtrusion = "Native Server Vector 3D Extrusion",
    NativeServerTraversability = "Native Server Traversability",
    NativeServer3DModel = "Native Server 3D Model",
    NativeServerMaterial = "Native Server Material",
    RawRaster = "Raw Raster",
    RawDtm = "Raw DTM",
    Raw3DModel = "Raw 3D Model",
    RawVector = "Raw Vector",
    RawVector3DExtrusion = "Raw Vector 3D Extrusion",
    RawTraversability = "Raw Traversability",
    RawMaterial = "Raw Material",
    WebServiceDTM = "Web Service DTM",
    WebServiceRaster = "Web Service Raster"
}
export enum LayerSourceEnum {
    MAPCORE = "MAPCORE",
    WMTS = "WMTS",
    WCS = "WCS",
    WMS = "WMS",
    CSW = "CSW",
    CSW_WMTS = "CSW_WMTS",
    Native = "Native",
    Raw = "Raw",
}
export enum LayerCreationTargets {
    StandAloneTarget,
    OpeningViewportTarget,
    QuerySecondaryDtmTarget
}

