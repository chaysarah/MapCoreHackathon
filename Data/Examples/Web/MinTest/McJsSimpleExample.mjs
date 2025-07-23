/// <reference path="MapCore.d.ts"/>

let uMemUsageLoggingFrequency           = 0;                  // memory usage logging frequency in seconds (0 - no logging), will be overwritten during parsing configuration file

const dCamera3DClipMinDistance = 1;
const dCamera3DClipMaxDistance = 5000000;                     // 0 means infinity

const dCamera3DMinHeight = 3;
const dCamera3DMaxHeight = 5000000;

const dZoom3DMinDistance = 10;
const dZoom3DMaxDistance = 2000000;

const fMinPitch = -90;
const fMaxPitch = 0;

let mouseDownButtons = 0;

let lastRenderTime = (new Date).getTime();
let lastMemUsageLogTime = (new Date).getTime();

let device = null;
let lastCoordSys;
let aLastTerrainLayers = [];
let overlayManager = null;
let overlay;
let viewport;
let editMode;
let lineScheme = null;
let ellipseScheme = null;
let polygonScheme = null;
let arrowScheme = null;
let textScheme = null;
let testObjectsScheme = null;
let bEdit = false;
let aObjects = [];
let aPositions = [];
let nNumObjects = 10000;
let RotationCenter = null;
let nMousePrevX = null;
let nMousePrevY = null;
let layerCallback = null;
let uCameraUpdateCounter = 0;
let CAsyncQueryCallback;
let canvasParent = document.getElementsByTagName("Canvases")[0];
let activeViewport = -1;
let aViewports = [];

// viewport-related data
class SViewportData
{
    constructor(_viewport, _editMode)
    {
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

// import McStartMapCore from "./MapCore.mjs";

// start MapCore and load its API
// if .wasm and optional component's zip files should be loaded from different path or with different names, pass as a parameter to McStartMapCore(): 
// { locateFile : function(fileName, directory) { return "SubFolder/" + fileName; } });
await McStartMapCore();

// MapCore.__WebGL1 = true;                             // uncomment to force WebGL 1, default is WebGL 2
// MapCore.__TilingSchemeEps = 0.0001;                  // uncomment and change if needed
// MapCore.__VertexBufferSwap = false;                  // uncomment this line in order to store GPU vertex buffer data in WASM memory instead of swapping it with JS memory
// MapCore.IMcMapDevice.SetHeapSize(MapCore.UINT_MAX);  // uncomment to allocate the maximum possible heap size (4GB) to prevent heap resizing and copying

// fetch optional components
// await MapCore.IMcMapDevice.FetchOptionalComponents(MapCore.IMcMapDevice.EOptionalComponentFlags.EOCF_GEOID_MAGNETIC.value);

// add MapCore version to the title
document.title = MapCore.IMcMapDevice.GetVersion() + " " + document.URL;

// add controls' handlers
for (let i = 0; i < Controls.length; ++i)
{
    document.getElementById(Controls[i]).onclick = eval("Do" + Controls[i]);
}

// create map device (MapCore initialization)
let init = new MapCore.IMcMapDevice.SInitParams();
init.uNumTerrainTileRenderTargets = 100;
// init.eLoggingLevel = MapCore.IMcMapDevice.ELoggingLevel.ELL_HIGH;

// set FSAA, anything other than EAAL_NONE will be EAAL_4 inside emscripten library_egl.js.
//init.eViewportAntiAliasingLevel = MapCore.IMcMapDevice.EAntiAliasingLevel.EAAL_NONE;
init.eViewportAntiAliasingLevel = MapCore.IMcMapDevice.EAntiAliasingLevel.EAAL_4;

device = MapCore.IMcMapDevice.Create(init);
device.AddRef();

// create callback classes
CreateCallbackClasses();

// ask the browser to render or perform pending calculations once
requestAnimationFrame(RenderOrPerformPendingCalcultions);

DoOpenMaps();
 
// function defining map and viewport configuration according to user selection
function DoOpenMaps() 
{
    CreateMapLayersAndViewports();
}

// function creating map layers, terrain and viewports
function CreateMapLayersAndViewports()
{
    aLastTerrainLayers = [];
    // create coordinate system
    lastCoordSys = MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84);
    lastCoordSys.AddRef();
    
    // create map layers
    aLastTerrainLayers.push(MapCore.IMcNativeRasterMapLayer.Create("http:Maps/Raster/Swiss100K-GW", MapCore.UINT_MAX, false, 0, false, layerCallback));
    aLastTerrainLayers.push(MapCore.IMcNativeDtmMapLayer.Create("http:Maps/DTM/SwissDTM-GW", 0, layerCallback));
	// ...

    InitializeViewports();
}

// function creating terrain and viewport, starting rendering
function InitializeViewports()
{
    // create terrain
    let terrain = MapCore.IMcMapTerrain.Create(lastCoordSys, aLastTerrainLayers);
    terrain.AddRef();

    // create overlay manager
    if (overlayManager == null)
    {
        // create overlay manager
        if (lastCoordSys == null)
        {
            lastCoordSys = MapCore.IMcGridUTM.Create(36, MapCore.IMcGridCoordinateSystem.EDatumType.EDT_ED50_ISRAEL);
            lastCoordSys.AddRef();
        }
        overlayManager = MapCore.IMcOverlayManager.Create(lastCoordSys);
        overlayManager.AddRef();

        // create overlay for objects
        overlay = MapCore.IMcOverlay.Create(overlayManager);

    }

    // create map viewports
    let lastViewportConfiguration = "2D";
    switch (lastViewportConfiguration)
    {
        case "2D":
            CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_2D);
            break;
        case "3D":
            CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_3D);
            break;
    }
}

// function creating map viewport
function CreateViewport(terrain, eMapTypeToOpen)
{
    // create canvas if needed
    let currCanvas;
    // create canvas
    currCanvas = document.createElement('Canvas');
    currCanvas.style.border = "thick solid #FFFFFF"; 

    // add mouse event listeners
    currCanvas.addEventListener("wheel", MouseWheelHandler, false);
    currCanvas.addEventListener("mousemove", MouseMoveHandler, false);
    currCanvas.addEventListener("mousedown", MouseDownHandler, false);
    currCanvas.addEventListener("mouseup", MouseUpHandler, false);
    currCanvas.addEventListener("dblclick", MouseDblClickHandler, false);
   
    // create viewport
    let vpCreateData = new MapCore.IMcMapViewport.SCreateData(eMapTypeToOpen);
    vpCreateData.pDevice = device;
    vpCreateData.pCoordinateSystem = (terrain != null ? terrain.GetCoordinateSystem() : overlayManager.GetCoordinateSystemDefinition());
    vpCreateData.pOverlayManager = overlayManager;
    vpCreateData.hWnd = currCanvas;
    vpCreateData.bShowGeoInMetricProportion = true;
    viewport = MapCore.IMcMapViewport.Create(/*Camera*/null, vpCreateData, terrain != null ? [terrain] : null);

    // create Edit Mode
    editMode = MapCore.IMcEditMode.Create(viewport);
    
    if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D)
    {
        viewport.SetScreenSizeTerrainObjectsFactor(1.5);
        viewport.SetCameraRelativeHeightLimits(dCamera3DMinHeight, dCamera3DMaxHeight, true);
        viewport.SetCameraClipDistances(dCamera3DClipMinDistance, dCamera3DClipMaxDistance, true);
        viewport.SetCameraOrientation(0, fMaxPitch, 0);
    }
    else
    {
        viewport.SetVector3DExtrusionVisibilityMaxScale(50);
    }
    
    viewport.SetBackgroundColor(MapCore.SMcBColor(70, 70, 70, 255));

    // set object delays for optimazing rendering objects
    viewport.SetObjectsDelay(MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_UPDATE, true, 50);
    viewport.SetObjectsDelay(MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_CONDITION, true, 50);
    viewport.SetObjectsDelay(MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_SIZE, true, 5);
    viewport.SetObjectsDelay(MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_CHANGE_OBJECT_HEIGHT, true, 50);
    viewport.SetObjectsDelay(MapCore.IMcMapViewport.EObjectDelayType.EODT_VIEWPORT_DEFRAG_OBJECT_BATCHES, true, 20);

    // set objects movement threshold
    viewport.SetObjectsMovementThreshold(1);

    // set terrain cache
    if (terrain != null)
    {
        viewport.SetTerrainNumCacheTiles(terrain, false, 300);
        viewport.SetTerrainNumCacheTiles(terrain, true, 300);
    }

    let viewportData = new SViewportData(viewport, editMode);
    viewportData.terrain = terrain;

    aViewports.push(viewportData);
    canvasParent.appendChild(currCanvas);
    activeViewport = aViewports.length - 1;

    ResizeCanvases();
}

// 1. compute terrain bounding box, terrrain center and camera position if all layers have been initialized;
// 2. compute terrain height and set camera height for 3D viewport
function TrySetTerainBox()
{
    for (let j = 0; j < aViewports.length; j++)
    {
        if (aViewports[j].terrainBox == null)
        {
            let aViewportLayers = aViewports[j].aLayers;

            if (aViewportLayers.length != 0)
            {
                aViewports[j].terrainBox = new MapCore.SMcBox(-MapCore.DBL_MAX, -MapCore.DBL_MAX, 0, MapCore.DBL_MAX, MapCore.DBL_MAX, 0);
                for (let i = 0; i < aViewportLayers.length; ++i)
                {
                    if (aViewports[j].bSetTerrainBoxByStaticLayerOnly && !(aViewportLayers[i] instanceof MapCore.IMcStaticObjectsMapLayer))
                    {
                        continue;
                    }

                    if (!aViewportLayers[i].IsInitialized())
                    {
                        aViewports[j].terrainBox = null;
                        return;
                    }

                    let intersectionBox = new MapCore.SMcBox();
                    MapCore.SMcBox.Intersect(intersectionBox, aViewports[j].terrainBox, aViewportLayers[i].GetBoundingBox());
                    aViewports[j].terrainBox = intersectionBox;
                }
            }
            else
            {
                aViewports[j].terrainBox = new MapCore.SMcBox(0, 0, 0, 0, 0, 0);
            }

            aViewports[j].terrainCenter = MapCore.SMcVector3D((aViewports[j].terrainBox.MinVertex.x + aViewports[j].terrainBox.MaxVertex.x) / 2, (aViewports[j].terrainBox.MinVertex.y + aViewports[j].terrainBox.MaxVertex.y) / 2, 0);
            aViewports[j].terrainCenter.z = 10000;
        }

        if (!aViewports[j].bCameraPositionSet)
        {
            if (aViewports[j].viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D)
            {
                aViewports[j].viewport.SetCameraPosition(aViewports[j].terrainCenter);
                aViewports[j].bCameraPositionSet = true;
            }
            else // 3D
            {
                let height = {};
                let currViewportData = aViewports[j];
                currViewportData.terrainCenter.z = 1000;
                currViewportData.viewport.SetCameraPosition(aViewports[j].terrainCenter);
                currViewportData.bCameraPositionSet = true;
                let params = new MapCore.IMcSpatialQueries.SQueryParams();
                params.eTerrainPrecision = MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGH;
                params.pAsyncQueryCallback = new CAsyncQueryCallback(
                    (bHeightFound, height, normal) =>
                    {
                        currViewportData.terrainCenter.z = (bHeightFound ? height : 100) + 20;
                        if (currViewportData.viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D)
                        {
                            currViewportData.viewport.SetCameraPosition(currViewportData.terrainCenter);
                        }
                    }
                );
                currViewportData.viewport.GetTerrainHeight(aViewports[j].terrainCenter, height, null, params); // async, wait for OnTerrainHeightResults()
            }
        }
    }
}

// function creating WMTS raster map layer
function CreateWMTSRasterLayer(serverURL, layersList, coordSystemName, imageFormat, boundingBox, bUseServerTilingScheme, bTransparent)
{
    let wmtsParams = new MapCore.IMcMapLayer.SWMTSParams;
    wmtsParams.bUseServerTilingScheme = (bUseServerTilingScheme != undefined ? bUseServerTilingScheme : true);
    wmtsParams.pCoordinateSystem = lastCoordSys;
    wmtsParams.pReadCallback = layerCallback;

    wmtsParams.strServerURL = serverURL;
    wmtsParams.strLayersList = layersList;
    wmtsParams.BoundingBox = (boundingBox  ? boundingBox : new MapCore.SMcBox(0, 0, 0, 0, 0, 0));
    wmtsParams.strServerCoordinateSystem = coordSystemName;
    wmtsParams.strImageFormat = (imageFormat  ? imageFormat : "jpeg");
    wmtsParams.bTransparent = (bTransparent  ? bTransparent : false);
    return MapCore.IMcWebServiceRasterMapLayer.Create(wmtsParams);
}

// function creating WMS raster map layer
function CreateWMSRasterLayer(serverURL, layersList, coordSystemName, imageFormat, boundingBox,  bTransparent)
{
    let wmsParams = new MapCore.IMcMapLayer.SWMSParams;
    wmsParams.pCoordinateSystem = lastCoordSys;
    wmsParams.pReadCallback = layerCallback;

    wmsParams.strServerURL = serverURL
    wmsParams.strLayersList = layersList;
    wmsParams.BoundingBox = (boundingBox  ? boundingBox : new MapCore.SMcBox(0, 0, 0, 0, 0, 0));
    wmsParams.strServerCoordinateSystem = coordSystemName;
    wmsParams.strImageFormat = (imageFormat ? imageFormat : "jpeg");
    wmsParams.bTransparent = (bTransparent != undefined  ? bTransparent : false);
    return MapCore.IMcWebServiceRasterMapLayer.Create(wmsParams);
}

// function creating callbacks
function CreateCallbackClasses()
{
    // create callback class and instance implementing MapCore.IMcMapLayer.IReadCallback interface
    let CLayerReadCallback = MapCore.IMcMapLayer.IReadCallback.extend("IMcMapLayer.IReadCallback", 
    {
        // mandatory
        OnInitialized : function(pLayer, eStatus, strAdditionalDataString)
        {
            if (eStatus != MapCore.IMcErrors.ECode.SUCCESS && eStatus != MapCore.IMcErrors.ECode.NATIVE_SERVER_LAYER_NOT_VALID)
            {
                for (let i = 0; i < aViewports.length; i++)
                {
                    let nLayer = aViewports[i].aLayers.indexOf(pLayer);
                    if (nLayer >= 0)
                    {
                        aViewports[i].aLayers.splice(nLayer, 1);
                    }
                }
                alert("Layer initialization: " + MapCore.IMcErrors.ErrorCodeToString(eStatus) + " (" + strAdditionalDataString + ")");
                pLayer.RemoveLayerAsync();
            }
            TrySetTerainBox();                
        },

        // mandatory
        OnReadError: function(pLayer, eErrorCode, strAdditionalDataString)
        {
            alert("Layer read error: " + MapCore.IMcErrors.ErrorCodeToString(eErrorCode) + " (" + strAdditionalDataString + ")");
        },

        // mandatory
        OnNativeServerLayerNotValid: function(pLayer, bLayerVersionUpdated)
        {
            if (bLayerVersionUpdated)
            {
                if (confirm("The layer's version was updated by a server. Do you want to replace the layer?"))
                {
                    pLayer.ReplaceNativeServerLayerAsync(layerCallback);
                }
            }
            else
            {
                if (bConfigurationFilesLoaded)
                {
                    LoadConfigurationFiles();
                }
                else
                {
                    bReloadConfigurationFiles = true; // configuration files are been loaded: wait until they are loaded and reload again
                }

                if (confirm("The layer's ID was not found by a server. Do you want to remove the layer?"))
                {
                    pLayer.RemoveLayerAsync();
                }
            }
        },

        // optional
        OnRemoved(pLayer, eStatus, strAdditionalDataString)
        {
            alert("Map layer has been removed");
            TrySetTerainBox();                
        },

        // optional
        OnReplaced(pOldLayer, pNewLayer, eStatus, strAdditionalDataString)
        {
            alert("Map layer has been replaced");
            TrySetTerainBox();                
        },

        // optional, needed if to be deleted by MapCore when no longer used
        Release: function()
        {
            // don't delete here because we use one global callback class
        },
    });
    
    layerCallback = new CLayerReadCallback();

    // create callback class implementing MapCore.IMcSpatialQueries.IAsyncQueryCallback interface
    CAsyncQueryCallback = MapCore.IMcSpatialQueries.IAsyncQueryCallback.extend("IMcSpatialQueries.IAsyncQueryCallback",
    {
        //  // optional
        __construct: function(OnResults, OnError)
        {
            this.__parent.__construct.call(this);
            this.OnResults = OnResults;
            this.OnError = OnError;
        },

        OnTerrainHeightResults: function(bHeightFound, height, normal)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnTerrainHeightsAlongLineResults: function(aPointsWithHeights, aSlopes, pSlopesData)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnTerrainHeightMatrixResults: function(adHeightMatrix)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnExtremeHeightPointsInPolygonResults: function(bPointsFound, pHighestPoint, pLowestPoint)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnTerrainAnglesResults: function(dPitch, dRoll)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnRayIntersectionResults: function(bIntersectionFound, Intersection, Normal, dDistance)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnRayIntersectionTargetsResults: function(aIntersections)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnLineOfSightResults: function(aPoints, dCrestClearanceAngle, dCrestClearanceDistance)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnPointVisibilityResults: function(bIsTargetVisible, pdMinimalTargetHeightForVisibility, pdMinimalScouterHeightForVisibility)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },
        
        OnAreaOfSightResults: function(pAreaOfSight, aLinesOfSight, pSeenPolygons, pUnseenPolygons, aSeenStaticObjects)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnLocationFromTwoDistancesAndAzimuthResults: function(Target)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        // mandatory
        OnError: function(eErrorCode)
        {
            if (this.OnError)
            {
                this.OnError(eErrorCode);
            }
            else
            {
                alert("Spatial query error: " + MapCore.IMcErrors.ErrorCodeToString(eErrorCode.value));
            }
            this.delete();
        }
    });
}

// fuction creating / hiding map grid
function DoCreateGrid()
{
    if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D)
    {
        // grid is not supported in 3D
        return;
    }

    if (!viewport.GetGridVisibility() || viewport.GetGrid() == null)
    {
        if (viewport.GetGrid() == null)
        {
            let gridRegion = new MapCore.IMcMapGrid.SGridRegion();

            gridRegion.pGridLine = MapCore.IMcLineItem.Create(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN.value, MapCore.IMcLineBasedItem.ELineStyle.ELS_SOLID, MapCore.bcBlackOpaque, 2);

            gridRegion.pGridText = MapCore.IMcTextItem.Create(MapCore.IMcObjectSchemeItem.EItemSubTypeFlags.EISTF_SCREEN.value, MapCore.EMcPointCoordSystem.EPCS_SCREEN, null, new MapCore.SMcFVector2D(12, 12));
            gridRegion.pGridText.SetTextColor(new MapCore.SMcBColor(255, 0, 0, 255));

            gridRegion.pCoordinateSystem = viewport.GetCoordinateSystem();
            gridRegion.GeoLimit.MinVertex = MapCore.SMcVector3D(0,0,0);
            gridRegion.GeoLimit.MaxVertex = MapCore.SMcVector3D(0,0,0);

            let basicStep = 2000.0;
            let currentStep = basicStep;

            let scaleStep = [];
            scaleStep[0] = new MapCore.IMcMapGrid.SScaleStep();
            
            scaleStep[0].fMaxScale = 80;
            scaleStep[0].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
            scaleStep[0].NextLineGap = MapCore.SMcVector2D(currentStep,currentStep);
            scaleStep[0].uNumOfLinesBetweenDifferentTextX = 2;
            scaleStep[0].uNumOfLinesBetweenDifferentTextY = 2;
            scaleStep[0].uNumOfLinesBetweenSameTextX = 2;
            scaleStep[0].uNumOfLinesBetweenSameTextY = 2;
            scaleStep[0].uNumMetricDigitsToTruncate = 3;

            currentStep *= 2;

            scaleStep[1] = new MapCore.IMcMapGrid.SScaleStep();
            scaleStep[1].fMaxScale = 160;
            scaleStep[1].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
            scaleStep[1].NextLineGap = MapCore.SMcVector2D(currentStep,currentStep);
            scaleStep[1].uNumOfLinesBetweenDifferentTextX = 2;
            scaleStep[1].uNumOfLinesBetweenDifferentTextY = 2;
            scaleStep[1].uNumOfLinesBetweenSameTextX = 2;
            scaleStep[1].uNumOfLinesBetweenSameTextY = 2;
            scaleStep[1].uNumMetricDigitsToTruncate = 3;

            currentStep *= 2;

            scaleStep[2] = new MapCore.IMcMapGrid.SScaleStep();
            scaleStep[2].fMaxScale = 320;
            scaleStep[2].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
            scaleStep[2].NextLineGap = MapCore.SMcVector2D(currentStep,currentStep);
            scaleStep[2].uNumOfLinesBetweenDifferentTextX = 2;
            scaleStep[2].uNumOfLinesBetweenDifferentTextY = 2;
            scaleStep[2].uNumOfLinesBetweenSameTextX = 2;
            scaleStep[2].uNumOfLinesBetweenSameTextY = 2;
            scaleStep[2].uNumMetricDigitsToTruncate = 3;

            currentStep *= 2;

            scaleStep[3] = new MapCore.IMcMapGrid.SScaleStep();
            scaleStep[3].fMaxScale = 640;
            scaleStep[3].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
            scaleStep[3].NextLineGap = MapCore.SMcVector2D(currentStep,currentStep);
            scaleStep[3].uNumOfLinesBetweenDifferentTextX = 2;
            scaleStep[3].uNumOfLinesBetweenDifferentTextY = 2;
            scaleStep[3].uNumOfLinesBetweenSameTextX = 2;
            scaleStep[3].uNumOfLinesBetweenSameTextY = 2;
            scaleStep[3].uNumMetricDigitsToTruncate = 3;

            currentStep *= 2;

            scaleStep[4] = new MapCore.IMcMapGrid.SScaleStep();
            scaleStep[4].fMaxScale = 1280;
            scaleStep[4].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
            scaleStep[4].NextLineGap = MapCore.SMcVector2D(currentStep,currentStep);
            scaleStep[4].uNumOfLinesBetweenDifferentTextX = 2;
            scaleStep[4].uNumOfLinesBetweenDifferentTextY = 2;
            scaleStep[4].uNumOfLinesBetweenSameTextX = 2;
            scaleStep[4].uNumOfLinesBetweenSameTextY = 2;
            scaleStep[4].uNumMetricDigitsToTruncate = 3;

            currentStep *= 2;

            scaleStep[5] = new MapCore.IMcMapGrid.SScaleStep();
            scaleStep[5].fMaxScale = 2560;
            scaleStep[5].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
            scaleStep[5].NextLineGap = MapCore.SMcVector2D(currentStep,currentStep);
            scaleStep[5].uNumOfLinesBetweenDifferentTextX = 2;
            scaleStep[5].uNumOfLinesBetweenDifferentTextY = 2;
            scaleStep[5].uNumOfLinesBetweenSameTextX = 2;
            scaleStep[5].uNumOfLinesBetweenSameTextY = 2;
            scaleStep[5].uNumMetricDigitsToTruncate = 3;

            currentStep *= 2;

            scaleStep[6] = new MapCore.IMcMapGrid.SScaleStep();
            scaleStep[6].fMaxScale = MapCore.FLT_MAX;
            scaleStep[6].eAngleValuesFormat = MapCore.IMcMapGrid.EAngleFormat.EAF_DECIMAL_DEG;
            scaleStep[6].NextLineGap = MapCore.SMcVector2D(currentStep,currentStep);
            scaleStep[6].uNumOfLinesBetweenDifferentTextX = 2;
            scaleStep[6].uNumOfLinesBetweenDifferentTextY = 2;
            scaleStep[6].uNumOfLinesBetweenSameTextX = 2;
            scaleStep[6].uNumOfLinesBetweenSameTextY = 2;
            scaleStep[6].uNumMetricDigitsToTruncate = 3;

            let grid = MapCore.IMcMapGrid.Create([gridRegion],scaleStep);
            viewport.SetGrid(grid);
        }
        viewport.SetGridVisibility(true);
    }
    else
    {
        viewport.SetGridVisibility(false);
    }

}

// function creating randomly distributed objects after ensuring testObjectsScheme has been loaded
async function DoCreateObjects()
{
    if (aObjects.length != 0)
    {
        DoRemoveObjects();
        return;
    }
    
    if (testObjectsScheme == null)
    {
        testObjectsScheme = await FetchObjectScheme("http:ObjectWorld/Schemes/ScreenPicture-Scheme.mcsch");
        if (testObjectsScheme == null)
        {
            return;
        }
    }

    let dRadius = 30000;
    let pos = MapCore.SMcVector3D(aViewports[activeViewport].terrainCenter.x - dRadius, aViewports[activeViewport].terrainCenter.y - dRadius, 0);
    let numExistingObjects = aObjects.length;
    aObjects.length = numExistingObjects + nNumObjects;
    
    for (let idx = numExistingObjects; idx < aObjects.length; ++idx)
    {
        aPositions[idx] = MapCore.SMcVector3D(pos.x + dRadius * 2 * Math.random(), pos.y + dRadius * 2 * Math.random(), pos.z);
        aObjects[idx] = MapCore.IMcObject.Create(overlay, testObjectsScheme, [aPositions[idx]]);
        aObjects[idx].SetState([idx % 8]);
    }
}

// function removing randomly distributed objects
function DoRemoveObjects()
{
    if (aObjects.length != 0)
    {
        for (let idx = 0; idx < aObjects.length; ++idx)
        {
            aObjects[idx].Remove();
        }
        aObjects.length = 0;
        aPositions.length = 0;
    }
}

// function moving randomly distributed objects (called before each render if moving-objects is on)
function DoMoveObjects()
{
    if (aObjects.length != 0)
    {
        for (let idx = 0; idx < 100; ++idx)
        {
            let val = Math.round(Math.random() * aObjects.length);
            if (val >= aObjects.length)
            {
                val = aObjects.length - 1;
            }
            aPositions[val].x += Math.random() * 20 - 10;
            aPositions[val].y += Math.random() * 20 - 10;
            aObjects[val].UpdateLocationPoints([aPositions[val]]);
        }
    }
}

// function calculating height range of viewport's area
function CalcMinMaxHeights()
{
    let minHeight = 0;
    let maxHeight = 700;
        let fp = viewport.GetCameraFootprint();
    if (fp.bUpperLeftFound && fp.bUpperRightFound && fp.bLowerRightFound && fp.bLowerLeftFound)
    {
        let minPoint = {}, maxPoint = {};
        if (viewport.GetExtremeHeightPointsInPolygon([fp.UpperLeft, fp.UpperRight, fp.LowerRight, fp.LowerLeft], maxPoint, minPoint))
        {
            minHeight = minPoint.Value.z;
            maxHeight = maxPoint.Value.z;
            if (maxHeight <= minHeight + 1)
            {
                maxHeight = minHeight + 1;
            }
        }
    }
    return { minHeight : minHeight, maxHeight : maxHeight};
}

// render function (called by the browser)
function RenderOrPerformPendingCalcultions()
{
    let currtRenderTime = (new Date).getTime();
    let bRendered = false;
    if (aViewports.length != 0)
    {
        // render viewport
        if (viewport != null && viewport.HasPendingUpdates())
        {
            viewport.Render();
            bRendered = true;
        }

        // move objects if they exist
        DoMoveObjects();

        lastRenderTime = currtRenderTime;
    }
    if (!bRendered)
    {
        MapCore.IMcMapDevice.PerformPendingCalculations();
    }    

    // log memory usage and heap size
    if (uMemUsageLoggingFrequency != 0 && currtRenderTime >= lastMemUsageLogTime + uMemUsageLoggingFrequency * 1000)
    {
        console.log("Mem=" + MapCore.IMcMapDevice.GetMemorySize().toLocaleString() +", max=" + MapCore.IMcMapDevice.GetMaxMemoryUsage().toLocaleString() + 
            ", heap=" + MapCore.IMcMapDevice.GetHeapSize().toLocaleString());
        lastMemUsageLogTime = currtRenderTime;
    }

    // ask the browser to render or perform pending calculations again
    requestAnimationFrame(RenderOrPerformPendingCalcultions);
}

// function converting screen point to world point (on 3D: with z if possible, on 2D: without z) 
function ViewportScreenToWorld(Viewport, ScreenPoint)
{
    let bIntersection = false;
    let WorldCenter = {};
    if (Viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D)
    {
        bIntersection = Viewport.ScreenToWorldOnTerrain(ScreenPoint, WorldCenter);
    }
    if (!bIntersection) // EMT_2D || !bIntersection
    {
        bIntersection = Viewport.ScreenToWorldOnPlane(ScreenPoint, WorldCenter);
    }
    return (bIntersection ? WorldCenter.Value : null);
}

// function starting object drawing by EditMode (called by DoLine(), DoText(), etc.)
function DoStartInitObject(pScheme)
{
    if (pScheme != null)
    {
        // create object
        let pObject = MapCore.IMcObject.Create(overlay, pScheme);

        // start EditMode action
        try
        {
            editMode.StartInitObject(pObject); // default edit mode item is defined in the scheme (otherwise it should be specified as the second parameter)
        }
        catch (ex)
        {
            if (ex instanceof MapCore.CMcError)
            {
                alert(ex.message);
            }
            else
            {
                throw ex;
            }
        }
    }
}

// function starting line drawing by EditMode
async function DoLine()
{
    if (lineScheme == null)
    {
        lineScheme = await FetchObjectScheme("http:ObjectWorld/Schemes/LineScheme.mcsch");
    }
    DoStartInitObject(lineScheme); 
}

// function starting CPU-based area-of-sight ellipse drawing by EditMode
async function DoEllipse()
{
    if (ellipseScheme == null)
    {
        ellipseScheme = await FetchObjectScheme("http:ObjectWorld/Schemes/EllipseSchemeCPU.mcsch");
    }
    DoStartInitObject(ellipseScheme);
}

// function starting polygon drawing by EditMode
async function DoPolygon()
{
    if (polygonScheme == null)
    {
        polygonScheme = await FetchObjectScheme("http:ObjectWorld/Schemes/PolygonScheme.mcsch");
    }
    DoStartInitObject(polygonScheme); 
}

// function starting arrow drawing by EditMode
async function DoArrow()
{
    if (arrowScheme == null)
    {
        arrowScheme = await FetchObjectScheme("http:ObjectWorld/Schemes/ArrowScheme.mcsch");
    }
    DoStartInitObject(arrowScheme); 
}

// function starting text drawing by EditMode
async function DoText()
{
    if (textScheme == null)
    {
        textScheme = await FetchObjectScheme("http:ObjectWorld/Schemes/TextScheme.mcsch");
    }
    DoStartInitObject(textScheme); 
}

// function switching object editing on/off
function DoEdit()
{
    bEdit = !bEdit;
}

// function resizing canvases when their number is changed or Maximize/Restore button is pressed
function ResizeCanvases()
{
    let width =  (window.innerWidth - 40) - 10;
    let height = (window.innerHeight - 80) - 15;
    
    for (let i = 0; i < aViewports.length ; i++)
    {
        aViewports[i].canvas.width = width;
        aViewports[i].canvas.height = height;
        aViewports[i].viewport.ViewportResized();
    }
}    

// mouse handlers

function MouseWheelHandler(e) 
{
    if (viewport.GetWindowHandle() != e.target)
    {
            return;
    }
    
    let EventPixel = MapCore.SMcPoint(e.offsetX, e.offsetY);
    let MouseScreenPoint = new MapCore.SMcVector3D(EventPixel.x, EventPixel.y, 0);

    let bHandled = {};
    let eCursor = {};
    let wheelDelta = - e.deltaY;
    editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_WHEEL, EventPixel, e.ctrlKey, wheelDelta, bHandled, eCursor);
    if (bHandled.Value)
    {
        return;
    }

    let fFactor = (e.shiftKey ? 5 : 1);
    let fScalefactor = Math.pow(1.25, fFactor);

    if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D)
    {
        if (e.ctrlKey)
        {
            // down-up
            viewport.MoveCameraRelativeToOrientation(MapCore.SMcVector3D(0, 0, -Math.sign(wheelDelta) * 12.5 * fFactor), true);
        }
        else
        {
            // forward-backward
            let CameraPosition = viewport.GetCameraPosition();
            let MouseWorldPoint = {};
            let dDistance = 0;
            // intersect terrain
            if (viewport.ScreenToWorldOnTerrain(MouseScreenPoint, MouseWorldPoint))
            {
                dDistance = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, MouseWorldPoint.Value));
            }
            else
            {
                // intersect bounding box
                let IntersectionPoint = {};
                if (aViewports[activeViewport].terrainBox != null)
                {
                    let dDistanceX = 0, dDistanceY = 0;
                    let IntersectionPointX, IntersectionPointY;

                    // intersect bounding box's far X-plane
                    let Normal = new MapCore.SMcVector3D(1, 0, 0);
                    if (viewport.ScreenToWorldOnPlane(MouseScreenPoint, IntersectionPoint, aViewports[activeViewport].terrainBox.MinVertex.x, Normal))
                    {
                        let dDist = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value));
                        if (dDist < dZoom3DMaxDistance)
                        {
                            dDistanceX = dDist;
                            IntersectionPointX = IntersectionPoint.Value;
                        }
                    } 
                    if (viewport.ScreenToWorldOnPlane(MouseScreenPoint, IntersectionPoint, aViewports[activeViewport].terrainBox.MaxVertex.x, Normal))
                    {
                        let dDist = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value));
                        if (dDist < dZoom3DMaxDistance && dDist > dDistanceX)
                        {
                            dDistanceX = dDist;
                            IntersectionPointX = IntersectionPoint.Value;
                        }
                    } 
                    // intersect bounding box's far Y-plane
                    Normal = new MapCore.SMcVector3D(0, 1, 0);
                    if (viewport.ScreenToWorldOnPlane(MouseScreenPoint, IntersectionPoint, aViewports[activeViewport].terrainBox.MinVertex.y, Normal))
                    {
                        let dDist = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value));
                        if (dDist < dZoom3DMaxDistance)
                        {
                            dDistanceY = dDist;
                            IntersectionPointY = IntersectionPoint.Value;
                        }
                    } 
                    if (viewport.ScreenToWorldOnPlane(MouseScreenPoint, IntersectionPoint, aViewports[activeViewport].terrainBox.MaxVertex.y, Normal))
                    {
                        let dDist = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value));
                        if (dDist < dZoom3DMaxDistance && dDist > dDistanceY)
                        {
                            dDistanceY = dDist;
                            IntersectionPointY = IntersectionPoint.Value;
                        }
                    } 
                    
                    // take the nearest existing intersection
                    if (dDistanceX != 0 && dDistanceX < dDistanceY)
                    {
                        dDistance = dDistanceX;
                        MouseWorldPoint.Value = IntersectionPointX;
                    }
                    else
                    {
                        dDistance = dDistanceY;
                        MouseWorldPoint.Value = IntersectionPointY;
                    }
                }
                if (dDistance == 0)
                {
                    // intersect zero-height plane
                    if (viewport.ScreenToWorldOnPlane(MouseScreenPoint, IntersectionPoint))
                    {
                        let dDist = MapCore.SMcVector3D.Length(MapCore.SMcVector3D.Minus(CameraPosition, IntersectionPoint.Value));
                        if (dDist < dZoom3DMaxDistance)
                        {
                            dDistance = dDist;
                            MouseWorldPoint.Value = IntersectionPoint.Value;
                        }
                    }
                }
            }
            if (dDistance != 0)
            {
                // calculate the new distance
                let dNewDistance;
                if (wheelDelta > 0)
                {
                    dNewDistance = Math.max(dDistance / fScalefactor, dZoom3DMinDistance);
                }
                else
                {
                    dNewDistance = Math.min(dDistance * fScalefactor, dZoom3DMaxDistance);
                }

                // move camera to the new distance
                viewport.SetCameraPosition(MapCore.SMcVector3D.GetLinearInterpolationWith(CameraPosition, MouseWorldPoint.Value, (dDistance - dNewDistance) / dDistance));
            }
        }
    }
    else
    {
        let MouseWorldPoint = {};
        viewport.ScreenToWorldOnPlane(MouseScreenPoint, MouseWorldPoint);

        let fScale = viewport.GetCameraScale();
        if (wheelDelta > 0)
        {
            viewport.SetCameraScale(fScale / fScalefactor);
        }
        else
        {
            viewport.SetCameraScale(fScale * fScalefactor);
        }

        let MouseScreenPointNew = viewport.WorldToScreen(MouseWorldPoint.Value);
        viewport.ScrollCamera(MouseScreenPointNew.x - MouseScreenPoint.x, MouseScreenPointNew.y - MouseScreenPoint.y)
    }

    e.preventDefault();
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();
}

function MouseMoveHandler(e) 
{
    let EventPixel = MapCore.SMcPoint(e.offsetX, e.offsetY);
    if (e.buttons <= 1)
    {
        let bHandled = {};
        let eCursor = {};
        editMode.OnMouseEvent(e.buttons == 1 ? MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_DOWN : MapCore.IMcEditMode.EMouseEvent.EME_MOUSE_MOVED_BUTTON_UP, 
            EventPixel, e.ctrlKey, 0, bHandled, eCursor);
        if (bHandled.Value)
        {
            e.preventDefault();
            e.cancelBubble = true;
            if (e.stopPropagation) e.stopPropagation();
            return;
        }
    }

    if (nMousePrevX != null)
    {
        let uWidth = {}, uHeight = {};
        viewport.GetViewportSize(uWidth, uHeight);
        let ViewportCenter = new MapCore.SMcVector2D(uWidth.Value / 2, uHeight.Value / 2);
        let MousePrev = new MapCore.SMcVector2D(nMousePrevX, nMousePrevY);
        let MouseCurr = new MapCore.SMcVector2D(EventPixel.x, EventPixel.y);
        let MousePrevFromCenter = MapCore.SMcVector2D.Minus(MousePrev, ViewportCenter);
        let MouseCurrFromCenter = MapCore.SMcVector2D.Minus(MouseCurr, ViewportCenter);
        let MouseDellta =  MapCore.SMcVector2D.Minus(MouseCurr, MousePrev);

        if (e.buttons == 1)
        {
            if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D)
            {
                let MouseCurrWorld = ViewportScreenToWorld(viewport, new MapCore.SMcVector3D(MouseCurr));
                if (e.ctrlKey)
                {
                    let MousePrevWorld = ViewportScreenToWorld(viewport, new MapCore.SMcVector3D(MousePrev));
                    if (MousePrevWorld != null && MouseCurrWorld != null)
                    {
                        viewport.SetCameraPosition(MapCore.SMcVector3D.Minus(MousePrevWorld, MouseCurrWorld), true);
                    }
                }
                else
                {
                    let fPitch = {}, fRoll = {};
                    viewport.GetCameraOrientation(null, fPitch, fRoll);
                    let fFOV = viewport.GetCameraFieldOfView();
                    let dCameraDist = ViewportCenter.x / Math.tan(fFOV / 2 * Math.PI / 180);
                    let fDeltaYaw = MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MousePrevFromCenter.x, dCameraDist)) - 
                                    MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MouseCurrFromCenter.x, dCameraDist));
                    let fDeltaPitch = MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MouseCurrFromCenter.y, dCameraDist)) - 
                                      MapCore.SMcVector2D.GetYawAngleDegrees(new MapCore.SMcVector2D(MousePrevFromCenter.y, dCameraDist));
                    viewport.RotateCameraRelativeToOrientation(fDeltaYaw, Math.min(Math.max(fDeltaPitch, fMinPitch - fPitch.Value), fMaxPitch - fPitch.Value), 0, false);
                    
                    // roll could be altered by RotateCameraRelativeToOrientation(), return it to the previous value
                    let fNewRoll = {};
                    viewport.GetCameraOrientation(null, null, fNewRoll);
                    let fRollDiff = fRoll.Value - fNewRoll.Value;
                    if (fRollDiff != 0)
                    {
                        if (MouseCurrWorld != null)
                        {
                            viewport.RotateCameraAroundWorldPoint(MouseCurrWorld, 0, 0, fRollDiff, false);
                        }
                        else
                        {
                            viewport.SetCameraOrientation(0, 0, fRollDiff, true);
                        }
                    }
                }
            }
            else
            {
                if (e.ctrlKey)
                {
                    let fDeltaYaw = MapCore.SMcVector2D.GetYawAngleDegrees(MouseCurrFromCenter) - MapCore.SMcVector2D.GetYawAngleDegrees(MousePrevFromCenter);
                    viewport.SetCameraOrientation(fDeltaYaw, 0, 0, true);
                }
                else
                {
                    viewport.ScrollCamera(-MouseDellta.x, -MouseDellta.y);
                }
            }

            e.preventDefault();
            e.cancelBubble = true;
            if (e.stopPropagation) e.stopPropagation();
        }
        else if (e.buttons == 4 && RotationCenter != null)
        {
            viewport.RotateCameraAroundWorldPoint(RotationCenter, MouseDellta.x * 360 / uWidth.Value, 0, 0, false);
            if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_3D)
            {
                let fPitch = {};
                viewport.GetCameraOrientation(null, fPitch, null);
                viewport.RotateCameraAroundWorldPoint(RotationCenter, 0, Math.min(Math.max(-MouseDellta.y * 180 / uWidth.Value, fMinPitch - fPitch.Value), fMaxPitch - fPitch.Value), 0, true);
            }
        }
    }

    nMousePrevX = EventPixel.x;
    nMousePrevY = EventPixel.y;
}

function MouseDownHandler(e) 
{
    let EventPixel = MapCore.SMcPoint(e.offsetX, e.offsetY);
    mouseDownButtons = e.buttons;
    if (e.buttons == 1)
    {
        let bHandled = {};
        let eCursor = {};
        editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_PRESSED, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
        if (bHandled.Value)
        {
            e.preventDefault();
            e.cancelBubble = true;
            if (e.stopPropagation) e.stopPropagation();
            return;
        }

        nMousePrevX = EventPixel.x;
        nMousePrevY = EventPixel.y;
    }
    else if (e.buttons == 4)
    {
        RotationCenter = ViewportScreenToWorld(viewport, new MapCore.SMcVector3D(EventPixel.x, EventPixel.y, 0));
    }

    e.preventDefault();
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();
}

function MouseUpHandler(e)
{
    let EventPixel = MapCore.SMcPoint(e.offsetX, e.offsetY);
    let buttons = mouseDownButtons & ~e.buttons;
    if (buttons == 1)
    {
        let bHandled = {};
        let eCursor = {};
        editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_RELEASED, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
        if (bHandled.Value)
        {
            e.preventDefault();
            e.cancelBubble = true;
            if (e.stopPropagation) e.stopPropagation();
            return;
        }
        nMousePrevX = null;
        nMousePrevY = null;
    }
    else if (e.buttons == 4)
    {
        RotationCenter = null;
    }
}

function MouseDblClickHandler(e)
{
    let EventPixel = MapCore.SMcPoint(e.offsetX, e.offsetY);
    let buttons = mouseDownButtons & ~e.buttons;
    if (bEdit)
    {
        let aTargets = viewport.ScanInGeometry(new MapCore.SMcScanPointGeometry(MapCore.EMcPointCoordSystem.EPCS_SCREEN, MapCore.SMcVector3D(EventPixel.x, EventPixel.y, 0), 20), false);
        for (let i = 0; i < aTargets.length; ++i)
        {
            if (aTargets[i].eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT)
            {
                if (bEdit)
                {
                    try
                    {
                        editMode.StartEditObject(aTargets[i].ObjectItemData.pObject, 
                            aTargets[i].ObjectItemData.pObject.GetScheme().GetEditModeDefaultItem() != null ? null : aTargets[i].ObjectItemData.pItem);
                    }
                    catch (ex)
                    {
                        if (ex instanceof MapCore.CMcError)
                        {
                            alert(ex.message);
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
                break;
            }
        }
        bEdit = false;
        e.preventDefault();
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();
        return;
    }

    if (buttons == 1)
    {
        let bHandled = {};
        let eCursor = {};
        editMode.OnMouseEvent(MapCore.IMcEditMode.EMouseEvent.EME_BUTTON_DOUBLE_CLICK, EventPixel, e.ctrlKey, 0, bHandled, eCursor);
        if (bHandled.Value)
        {
            e.preventDefault();
            e.cancelBubble = true;
            if (e.stopPropagation) e.stopPropagation();
            return;
        }
    }
}

// function fetching a file from server to byte-array
async function FetchFileToByteArray(uri) 
{
    try
    {
        let fetchResponse = await fetch(uri);
        let fileBuffer = (fetchResponse.ok ? await fetchResponse.arrayBuffer() : null);
        if (fileBuffer != null)
        {
            return new Uint8Array(fileBuffer);
        }
        else
        {
            alert("Cannot fetch " + uri);
            return null;
        }
    }
    catch (error)
    {
        alert("Network error in fetching " + uri);
        return null;
    }
}

async function FetchObjectScheme(uri)
{
    let bytes = await FetchFileToByteArray(uri);
    if (bytes != null)
    {
        let scheme = overlayManager.LoadObjectSchemes(bytes)[0];
        scheme.AddRef();
        return scheme;
    }
    else
    {
        return null;
    }
}
