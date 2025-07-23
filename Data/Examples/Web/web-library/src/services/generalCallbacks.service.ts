import mapcoreData from "../mapcore-data";

class GeneralCallbacksService {
    public initCallbackService() {
        mapcoreData.iMcQueryCallbackClass = this.createCAsyncQueryCallback();
        mapcoreData.iPrintCallbackClass = this.createIPrintCallbackClass();
        mapcoreData.iMcEditModeCallbacksClass = this.createEditModeCallbackClass();
        mapcoreData.iMcUserDataClass = this.createIMcUserDataCallback();
        mapcoreData.iMcOverlayManagerAsyncOperationCallbackClass = this.createIMcOverlayManagerIAsyncOperationCallbackClass();
        mapcoreData.asyncOperationCallBacksClass = this.createIAsyncOperationCallbacksClass();
    }
    private createCAsyncQueryCallback(): MapCore.IMcSpatialQueries.IAsyncQueryCallback {
        return MapCore.IMcSpatialQueries.IAsyncQueryCallback.extend("IMcSpatialQueries.IAsyncQueryCallback",
            {
                // optional
                __construct: function (OnResults: any, OnError: any, queryFunctionName: string) {
                    this.__parent.__construct.call(this);
                    this.OnResults = OnResults;
                    this.OnError = OnError;
                    this.queryFunctionName = queryFunctionName;
                },

                OnTerrainHeightResults: function (bHeightFound: boolean, dHeight: number, pNormal: MapCore.SMcVector3D): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnTerrainHeightsAlongLineResults: function (aPointsWithHeights: MapCore.SMcVector3D[], afSlopes: Float32Array, pSlopesData: MapCore.IMcSpatialQueries.SSlopesData): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnTerrainHeightMatrixResults: function (adHeightMatrix: Float64Array): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnExtremeHeightPointsInPolygonResults: function (bPointsFound: boolean, pHighestPoint: MapCore.SMcVector3D, pLowestPoint: MapCore.SMcVector3D): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnTerrainAnglesResults: function (dPitch: number, dRoll: number): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnRayIntersectionResults: function (bIntersectionFound: boolean, pIntersection: MapCore.SMcVector3D, pNormal: MapCore.SMcVector3D, pdDistance: number): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnRayIntersectionTargetsResults: function (aIntersections: MapCore.IMcSpatialQueries.STargetFound[]): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnLineOfSightResults: function (aPoints: MapCore.IMcSpatialQueries.SLineOfSightPoint[], dCrestClearanceAngle: number, dCrestClearanceDistance: number): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnPointVisibilityResults: function (bIsTargetVisible: boolean, pdMinimalTargetHeightForVisibility: number, pdMinimalScouterHeightForVisibility: number): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnAreaOfSightResults: function (pAreaOfSight: MapCore.IMcSpatialQueries.IAreaOfSight, aLinesOfSight: MapCore.IMcSpatialQueries.SLineOfSightPoint[][], pSeenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight, pUnseenPolygons: MapCore.IMcSpatialQueries.SPolygonsOfSight, aSeenStaticObjects: MapCore.IMcSpatialQueries.SStaticObjectsIDs[]): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                OnBestScoutersLocationsResults: function (aScouters: MapCore.SMcVector3D[]): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                OnLocationFromTwoDistancesAndAzimuthResults: function (Target: MapCore.SMcVector3D): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                OnRasterLayerColorByPointResults: function (Color: MapCore.SMcBColor): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                OnTraversabilityAlongLineResults: function (aTraversabilitySegments: MapCore.IMcSpatialQueries.STraversabilityPoint[]): void {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                // mandatory
                OnError: function (eErrorCode: MapCore.IMcErrors.ECode) {
                    if (this.OnError) {
                        this.OnError(eErrorCode);
                    }
                    //In Additional
                    if (this.queryFunctionName) {
                        console.error(`MapCore Error at ${this.queryFunctionName}`, MapCore.IMcErrors.ErrorCodeToString(eErrorCode));
                        alert(`MapCore Error: ${MapCore.IMcErrors.ErrorCodeToString(eErrorCode)}, \n Error code: ${eErrorCode}, \n funcName: ${this.queryFunctionName}`)
                    }
                    else {
                        console.error("Spatial query callBack error: " + MapCore.IMcErrors.ErrorCodeToString(eErrorCode));
                        alert("Spatial query callBack error: " + MapCore.IMcErrors.ErrorCodeToString(eErrorCode));
                    }
                    this.delete();
                }
            })
    };
    private createIMcOverlayManagerIAsyncOperationCallbackClass() {
        return MapCore.IMcOverlayManager.IAsyncOperationCallback.extend("IMcMapLayer.IAsyncOperationCallback",
            {
                // optional
                __construct: function (OnResults: any) {
                    this.__parent.__construct.call(this);
                    this.OnResults = OnResults;
                },

                OnSaveObjectsAsRawVectorToFileResult: function (strFileName: string, eStatus: MapCore.IMcErrors.ECode, aAdditionalFiles: string[]) {
                    //Logic - do what i need

                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                OnSaveObjectsAsRawVectorToBufferResult: function (strFileName: string, eStatus: MapCore.IMcErrors.ECode, auFileMemoryBuffer: Uint8Array, aAdditionalFiles: MapCore.SMcFileInMemory[]) {
                    //Logic - do what i need

                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
            }
        );
    }
    private createIPrintCallbackClass() {
        return MapCore.IMcPrintMap.IPrintCallback.extend("IMcPrintMap.IPrintCallback",
            {
                // optional
                __construct: function (OnResults: any) {
                    this.__parent.__construct.call(this);
                    this.OnResults = OnResults;
                },

                OnPrintFinished: function (eStatus: MapCore.IMcErrors.ECode, strFileNameOrRasterDataFormat: string, auFileMemoryBuffer: Uint8Array, auWorldFileMemoryBuffer: Uint8Array) {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
            }
        );
    }
    private createEditModeCallbackClass(): MapCore.IMcEditMode.ICallback {
        return MapCore.IMcEditMode.ICallback.extend("IMcEditMode.ICallback",
            {
                __construct: function (OnResults: any) {
                    this.__parent.__construct.call(this);//ensure all parents needed constructors were called
                    this.OnResults = OnResults;
                },

                // optional
                Release: function () {
                    // this.delete();
                },

                NewVertex(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, WorldVertex: MapCore.SMcVector3D, ScreenVertex: MapCore.SMcVector3D, uVertexIndex: number, dAngle: number) {
                    if (this.OnResults.NewVertex) {
                        this.OnResults.NewVertex.apply(null, arguments);
                    }
                },
                /** Optional */
                PointDeleted(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, WorldVertex: MapCore.SMcVector3D, ScreenVertex: MapCore.SMcVector3D, uVertexIndex: number) {
                    if (this.OnResults.PointDeleted) {
                        this.OnResults.PointDeleted.apply(null, arguments);
                    }
                },
                /** Optional */
                PointNewPos(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, WorldVertex: MapCore.SMcVector3D, ScreenVertex: MapCore.SMcVector3D, uVertexIndex: number, dAngle: number, bDownOnHeadPoint: boolean) {
                    if (this.OnResults.PointNewPos) {
                        this.OnResults.PointNewPos.apply(null, arguments);
                    }
                },
                /** Optional */
                ActiveIconChanged(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, eIconPermission: MapCore.IMcEditMode.EPermission, uIconIndex: number) {
                    if (this.OnResults.ActiveIconChanged) {
                        this.OnResults.ActiveIconChanged.apply(null, arguments);
                    }
                },
                /** Optional */
                InitItemResults(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) {
                    if (nExitCode != 0 && this.OnResults.InitItemResults) {
                        this.OnResults.InitItemResults.apply(null, arguments);
                    }
                },
                /** Optional */
                EditItemResults(pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) {
                    if (this.OnResults.EditItemResults) {
                        this.OnResults.EditItemResults.apply(null, arguments);
                    }
                },
                /** Optional */
                DragMapResults(pViewport: MapCore.IMcMapViewport, NewCenter: MapCore.SMcVector3D) {
                    if (this.OnResults.DragMapResults) {
                        this.OnResults.DragMapResults.apply(null, arguments);
                    }
                },
                /** Optional */
                RotateMapResults(pViewport: MapCore.IMcMapViewport, fNewYaw: number, fNewPitch: number) {
                    if (this.OnResults.RotateMapResults) {
                        this.OnResults.RotateMapResults.apply(null, arguments);
                    }
                },
                /** Optional */
                DynamicZoomResults(pViewport: MapCore.IMcMapViewport, fNewScale: number, NewCenter: MapCore.SMcVector3D) {
                    if (this.OnResults.DynamicZoomResults) {
                        this.OnResults.DynamicZoomResults.apply(null, arguments);
                    }
                },
                /** Optional */
                DistanceDirectionMeasureResults(pViewport: MapCore.IMcMapViewport, WorldVertex1: MapCore.SMcVector3D, WorldVertex2: MapCore.SMcVector3D, dDistance: number, dAngle: number) {
                    if (this.OnResults.DistanceDirectionMeasureResults) {
                        this.OnResults.DistanceDirectionMeasureResults.apply(null, arguments);
                    }
                },
                /** Optional */
                CalculateHeightResults(pViewport: MapCore.IMcMapViewport, dHeight: number, aCoords: MapCore.SMcVector3D[], nStatus: number) {
                    if (this.OnResults.CalculateHeightResults) {
                        this.OnResults.CalculateHeightResults.apply(null, arguments);
                    }
                },
                /** Optional */
                CalculateVolumeResults(pViewport: MapCore.IMcMapViewport, dVolume: number, aCoords: MapCore.SMcVector3D[], nStatus: number) {
                    if (this.OnResults.CalculateVolumeResults) {
                        this.OnResults.CalculateVolumeResults.apply(null, arguments);
                    }
                }
            });
    }
    private createIMcUserDataCallback(): MapCore.IMcUserData {
        let userDataClass: any = MapCore.IMcUserData.extend("IMcUserData", {
            /** Optional */
            __construct: function (buffer: any) {
                this.__parent.__construct.call(this);
                this.userDataBuffer = buffer;
            },
            setUserData: function (buffer: Uint8Array) {
                this.userDataBuffer = buffer;
            },
            getUserData: function () {
                return this.userDataBuffer;
            },
            /** Mandatory */
            Release: function () {
                this.delete();
            },
            /** Optional */
            Clone: function () {
                let newUserData = new userDataClass();
                newUserData.setUserData(new Uint8Array(this.userDataBuffer));
                return newUserData;
            },
            GetSaveBufferSize: function () {
                return this.userDataBuffer.length;
            },
            IsSavedBufferUTF8Bytes: function () {
                return true;
            },
            SaveToBuffer: function (aBuffer: Uint8Array) {//this func doesnt work, to ask orit what to do with it
                aBuffer = this.userDataBuffer;
            }
        });
        return userDataClass;
    }
    private createIAsyncOperationCallbacksClass() {
        return MapCore.IMcMapLayer.IAsyncOperationCallback.extend("IMcMapLayer.IAsyncOperationCallback",
            {
                // optional
                __construct: function (OnResults: any) {
                    this.__parent.__construct.call(this);
                    this.OnResults = OnResults;
                },
                // OnScanExtendedDataResult(pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode,
                //     VectorItems: MapCore.IMcMapLayer.SVectorItemFound[], aUnifiedVectorItemsPoints: MapCore.SMcVector3D[]) {
                //     // runCodeLibrarySafely(() => {
                //         VectorItems.forEach((vectorItem: any) => {
                //             let line: MapCore.IMcLineItem = MapCore.IMcLineItem.Create(
                //                 MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN,
                //                 MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID,
                //                 new MapCore.SMcBColor(10, 250, 255, 10));

                //             let unifiedItem = MapCore.IMcLineItem.Create(
                //                 MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN,
                //                 MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, new MapCore.SMcBColor(0, 0, 255, 255), 2);

                //             // colorDelta = (byte)(((uint)colorDelta + 64) % 256);
                //             line.Connect(unifiedItem);
                //             line.SetAttachPointType(0, MapCore.IMcSymbolicItem.EAttachPointType.EAPT_INDEX_POINTS);
                //             line.SetAttachPointIndex(0, vectorItem.uVectorItemFirstPointIndex)
                //             // ,MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID, false;
                //             line.SetNumAttachPoints(0, vectorItem.uVectorItemLastPointIndex + 1 - vectorItem.uVectorItemFirstPointIndex);
                //             //   ,  MapCore.IMcProperty.EPredefinedPropertyIDs.EPPI_SHARED_PROPERTY_ID, false

                //         })
                //     // }, "IMcMapLayer.IAsyncOperationCallback.OnScanExtendedDataResult")
                // },
                OnScanExtendedDataResult: function (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode,
                    VectorItems: MapCore.IMcMapLayer.SVectorItemFound[], aUnifiedVectorItemsPoints: MapCore.SMcVector3D[]) {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                OnFieldUniqueValuesResult: function (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, eReturnedType: MapCore.IMcMapLayer.EVectorFieldReturnedType, paUniqueValues: any) {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },

                OnVectorItemFieldValueResult: function (pLayer: MapCore.IMcMapLayer, eStatus: MapCore.IMcErrors.ECode, eReturnedType: MapCore.IMcMapLayer.EVectorFieldReturnedType, pValue: any) {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                OnWebServerLayersResults: function (eStatus: MapCore.IMcErrors.ECode, strServerURL: string, eWebMapServiceType: MapCore.IMcMapLayer.EWebMapServiceType, aLayers: MapCore.IMcMapLayer.SServerLayerInfo[], astrServiceMetadataURLs: string, strServiceProviderName: string) {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
                On3DModelSmartRealityDataResults: function (sStatus: MapCore.IMcErrors.ECode, strServerURL: string, uObjectID: MapCore.SMcVariantID,
                    buildingHistory: MapCore.IMcMapLayer.SSmartRealityBuildingHistory[]) {
                    this.OnResults.apply(null, arguments);
                    this.delete();
                },
            }
        );
    }
}
export default new GeneralCallbacksService;