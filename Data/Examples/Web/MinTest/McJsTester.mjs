/// <reference path="MapCore.d.ts"/>

let bUseDeprecatedPreloadingLayerFiles  = false;              // whether to use the DEPRECATED mode of pre-loading all layer files, will be overwritten during parsing configuration file
let uMemUsageLoggingFrequency           = 0;                  // memory usage logging frequency in seconds (0 - no logging), will be overwritten during parsing configuration file

const bTestLoadingFSDynamically         = false;              // for MapCore developers: whether to load FS files dynamically from FS-Dynamic via FS-Dynamic/FS.txt list into virtual FS/dynamically

const dCamera3DClipMinDistance = 1;
const dCamera3DClipMaxDistance = 5000000;                     // 0 means infinity

const dCamera3DMinHeight = 3;
const dCamera3DMaxHeight = 5000000;

const dZoom3DMinDistance = 10;
const dZoom3DMaxDistance = 2000000;

const fMinPitch = -90;
const fMaxPitch = 0;

let mouseDownButtons = 0;

let bCanvasesMaximized = false;
let lastRenderTime = (new Date).getTime();
let lastMemUsageLogTime = (new Date).getTime();
let lastLayersValidityCheckTime = (new Date).getTime();

let device = null;
let lastCoordSys;
let aLastTerrainLayers = [];
let overlayManager = null;
let overlay;
let viewport;
let editMode;
let lineScheme = null;
let ellipseScheme = null;
let ellipseSchemeGPU = null;
let polygonScheme = null;
let arrowScheme = null;
let textScheme = null;
let testObjectsScheme = null;
let bEdit = false;
let bChangeState = false;
let bRotateAroundCenter = false;
let aObjects = [];
let aPositions = [];
let aSketchObjects = [];
let nNumObjects = 10000;
let bMoveObjects = false;
let RotationCenter = null;
let nMousePrevX = null;
let nMousePrevY = null;
let layerCallback = null;
let uCameraUpdateCounter = 0;
let CCameraUpdateCallback;
let CEditModeCallback;
let CAsyncQueryCallback;
let CAsyncOperationCallback;
let CPrintCallback;
let mapTerrains = new Map;
let bSameCanvas = false;
let canvasParent = document.getElementsByTagName("Canvases")[0];
let activeViewport = -1;
let aViewports = [];

// selection of maps to load
let lastTerrainConfiguration;
let lastViewportConfiguration;
let mapLayerGroups = new Map;
let aServerLayerGroups = [];
let bConfigurationFilesLoaded = false;
let bReloadConfigurationFiles = false;

// viewport-related data
class SViewportData
{
    viewport;
    editMode;
    canvas;
    aLayers;
    terrainBox = null;
    terrainCenter = null;
    rotationCenter = null;
    bCameraPositionSet = false;
    bSetTerrainBoxByStaticLayerOnly = false;

    constructor(_viewport, _editMode)
    {
        this.viewport = _viewport;
        this.editMode = _editMode;
        this.canvas = _viewport.GetWindowHandle();
        let aViewportTerrains = _viewport.GetTerrains();
        this.aLayers = (aViewportTerrains != null && aViewportTerrains.length > 0 ? aViewportTerrains[0].GetLayers() : null);
    }
}

// map layer group creation parameters
class SLayerGroup
{
    aLayerCreateStrings = [];
    aLayerParams = [];
    aLayerBoundingBoxes = [];
    pCoordSystem;
    bShowGeoInMetricProportion;
    bSetTerrainBoxByStaticLayerOnly;
    InitialScale2D;
    strServerURL;
    eServerType;
    aRequestParams;
    tileMatrixSetFilter;

    constructor(pCoordSystem, bShowGeoInMetricProportion, bSetTerrainBoxByStaticLayerOnly, InitialScale2D)
    {
        this.pCoordSystem = pCoordSystem;
        this.bShowGeoInMetricProportion = bShowGeoInMetricProportion;
        this.bSetTerrainBoxByStaticLayerOnly = bSetTerrainBoxByStaticLayerOnly;
        this.InitialScale2D = InitialScale2D;
    }
}

// uncomment for using MapCore module
// import McStartMapCore from "./MapCore.mjs";

// start MapCore and load its API
// if .wasm and optional component's zip files should be loaded from different path or with different names, pass as a parameter to McStartMapCore(): 
// { locateFile : function(fileName, directory) { return "SubFolder/" + fileName; } });
await McStartMapCore();

// MapCore.__WMS_NoStreaming = true;                    // uncomment this line in order for WMS to work with no streaming
// MapCore.__WCS_NoStreaming = true;                    // uncomment this line in order for WCS to work with no streaming
// MapCore.__WebGL1 = true;                             // uncomment to force WebGL 1, default is WebGL 2
// MapCore.__TilingSchemeEps = 0.0001;                  // uncomment and change if needed
// MapCore.__VertexBufferSwap = false;                  // uncomment this line in order to store GPU vertex buffer data in WASM memory instead of swapping it with JS memory
// MapCore.__NoCloningAttachedToTerrain = false;        // uncomment this line in order to disable cloning attached-to-terrain render-items
// MapCore.__3DTilesProcessingMaxTimeInMS = 1000000;    // uncomment to disable 3DTiles processing max time or to change its value (the default is 20 ms)
// MapCore.__GeoErrorScaleFactor = 0;                   // uncomment and change to positive number to overwrite 3DTiles geometric error factor
// MapCore.IMcMapDevice.SetHeapSize(MapCore.UINT_MAX);  // uncomment to allocate the maximum possible heap size (4GB) to prevent heap resizing and copying

if (bTestLoadingFSDynamically)  // for MapCore developers
{
	await TestLoadingFSDynamically();
}

// add MapCore version to the title
document.title = MapCore.IMcMapDevice.GetVersion() + " " + document.URL;

// add controls' handlers
for (let i = 0; i < Controls.length; ++i)
{
    document.getElementById(Controls[i]).onclick = eval("Do" + Controls[i]);
}

// ImageProcessing Select event-handler 
document.getElementById("Ip_Type").addEventListener('change', ImageProcessTypeCB);
document.getElementById("Ip_FilterMode").addEventListener('change', ImageProcessTypeCB);
document.getElementById("Ip_CustomMode").addEventListener('change', ImageProcessTypeCB);

// create map device (MapCore initialization)
let init = new MapCore.IMcMapDevice.SInitParams();
init.uNumTerrainTileRenderTargets = 100;
// init.eLoggingLevel = MapCore.IMcMapDevice.ELoggingLevel.ELL_HIGH;

// set FSAA, anything other than EAAL_NONE will be EAAL_4 inside emscripten library_egl.js.
//init.eViewportAntiAliasingLevel = MapCore.IMcMapDevice.EAntiAliasingLevel.EAAL_NONE;
init.eViewportAntiAliasingLevel = MapCore.IMcMapDevice.EAntiAliasingLevel.EAAL_4;

// for MapCore developers
if (bTestLoadingFSDynamically)
{
    init.strConfigFilesDirectory = "FS/Dynamic";
}

device = MapCore.IMcMapDevice.Create(init);
device.AddRef();

// create callback classes
CreateCallbackClasses();

LoadConfigurationFiles();

// ask the browser to render or perform pending calculations once
requestAnimationFrame(RenderOrPerformPendingCalcultions);

// if configuration files have already fetched and parsed, initialize GUI for opening viewports
if (bConfigurationFilesLoaded)
{
    InitializeGUI();
}
 
// function initializing GUI for opening viewports
function InitializeGUI()
{
    // add layer groups to SelectTerrain combobox
    let selectTerrain = document.getElementById("SelectTerrain");
    for (let group of mapLayerGroups.keys())
    {
        let opt = document.createElement("option");
        opt.innerText = group;
        selectTerrain.add(opt);
    }

    // enable controls
    selectTerrain.disabled = false;
    document.getElementById("SelectViewportConfig").disabled = false;
    document.getElementById("RefreshMaps").disabled = false;
    document.getElementById("OpenMaps").disabled = false;
    document.getElementById("CloseMap").disabled = false;
    if (aViewports.length == 0)
    {
        document.getElementById("SameCanvas").disabled = false;
        document.getElementById("SameCanvasLabel").style.color = "black";
    }
}

// function disabling GUI during reloading configuration files
function DisableGUI()
{
    // clear SelectTerrain combobox
    let selectTerrain = document.getElementById("SelectTerrain");
    for (let i = selectTerrain.length - 1; i >= 0; --i)
    {
        selectTerrain.remove(i);
    }

    // disable controls
    selectTerrain.disabled = true;
    document.getElementById("SelectViewportConfig").disabled = true;
    document.getElementById("RefreshMaps").disabled = true;
    document.getElementById("OpenMaps").disabled = true;
    document.getElementById("CloseMap").disabled = true;
    document.getElementById("SameCanvas").disabled = true;
    document.getElementById("SameCanvasLabel").style.color = "gray";
}

// function defining map and viewport configuration according to user selection
function DoOpenMaps() 
{
     // define multi canvases or one canvas
     bSameCanvas = document.getElementById("SameCanvas").checked;
     document.getElementById("Resize").disabled = bSameCanvas;

    // define viewport configuration
    document.getElementById("SameCanvas").disabled = true;
    document.getElementById("SameCanvasLabel").style.color = "gray";

    lastTerrainConfiguration = document.getElementById("SelectTerrain").selectedOptions[0].innerHTML;
    lastViewportConfiguration = document.getElementById("SelectViewportConfig").selectedOptions[0].innerHTML;

    if (!bUseDeprecatedPreloadingLayerFiles)
    {
        CreateMapLayersAndViewports();
    }
    else
    {
        // Deprecated: non-streaming mode
        DeprecatedLoadMapLayerFilesAsync();
    }
}

// function creating map layers, terrain and viewports
function CreateMapLayersAndViewports()
{
    if (mapTerrains.get(lastTerrainConfiguration) == undefined) // if this terrain has not been created yet
    {
        aLastTerrainLayers = [];
        let group = mapLayerGroups.get(lastTerrainConfiguration);

        // create coordinate system by running a code string prepared during parsing configuration files (JSON configuration file and capabilities XML of MapCoreLayerServer)
        // e.g. MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84)
        lastCoordSys = group.pCoordSystem;
    
        for (let i = 0; i < group.aLayerCreateStrings.length; ++i)
        {
            // create map layer by running code string prepared during parsing configuration files (JSON configuration file and capabilities XML of MapCoreLayerServer)
            // e.g. MapCore.IMcNativeRasterMapLayer.Create('http:Maps/Raster/SwissOrtho-GW') or CreateWMTSRasterLayer(...) or CreateWMSRasterLayer(...)
            let BoundingBox = group.aLayerBoundingBoxes[i];
            let layer = eval(group.aLayerCreateStrings[i]);
            if (layer instanceof MapCore.IMc3DModelMapLayer)
            {
                layer.SetDisplayingItemsAttachedToTerrain(true);
                layer.SetDisplayingDtmVisualization(true);
            }
            aLastTerrainLayers.push(layer);
        }
    }

    InitializeViewports();
}

// function creating terrain and viewport, starting rendering
function InitializeViewports()
{
    let terrain = mapTerrains.get(lastTerrainConfiguration);
    if (terrain == undefined)
    {
        // create terrain
        if (aLastTerrainLayers.length > 0)
        {
            terrain = MapCore.IMcMapTerrain.Create(lastCoordSys, aLastTerrainLayers, null, null, true/*bDisplayItemsAttachedTo3DModelWithoutDtm*/);
            terrain.AddRef();
            let group = mapLayerGroups.get(lastTerrainConfiguration);
            for (let i = 0; i < group.aLayerParams.length; ++i)
            {
                if (group.aLayerParams[i])
                {
                    terrain.SetLayerParams(aLastTerrainLayers[i], group.aLayerParams[i]);
                }
            }
        }
        else
        {
            terrain = null;
        }
        mapTerrains.set(lastTerrainConfiguration, terrain);
    }

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
    switch (lastViewportConfiguration)
    {
        case "2D/3D":
            CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_2D);
            CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_3D);
            if (bCanvasesMaximized)
            {
                DoNextViewport();
            }
            else
            {
                DoPrevViewport();
            }
            break;
        case "3D/2D":
            if (bSameCanvas)
            {
                CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_2D);
                CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_3D);
            }
            else
            {
                CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_3D);
                CreateViewport(terrain, MapCore.IMcMapCamera.EMapType.EMT_2D);
                if (bCanvasesMaximized)
                {
                    DoNextViewport();
                }
                else
                {
                    DoPrevViewport();
                }
            }
            break;
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
    if (!bSameCanvas || aViewports.length == 0)
    {
        // create canvas
        currCanvas = document.createElement('Canvas');
        currCanvas.style.border = "thick solid #FFFFFF"; 

        // add mouse event listeners
        currCanvas.addEventListener("wheel", MouseWheelHandler, false);
        currCanvas.addEventListener("mousemove", MouseMoveHandler, false);
        currCanvas.addEventListener("mousedown", MouseDownHandler, false);
        currCanvas.addEventListener("mouseup", MouseUpHandler, false);
        currCanvas.addEventListener("dblclick", MouseDblClickHandler, false);
    }
    else
    {
        // use existing canvas
        currCanvas = aViewports[0].canvas;
    }
   
    // create viewport
    let layerGroup = mapLayerGroups.get(lastTerrainConfiguration);
    let vpCreateData = new MapCore.IMcMapViewport.SCreateData(eMapTypeToOpen);
    vpCreateData.pDevice = device;
    vpCreateData.pCoordinateSystem = (terrain != null ? terrain.GetCoordinateSystem() : overlayManager.GetCoordinateSystemDefinition());
    vpCreateData.pOverlayManager = overlayManager;
    vpCreateData.hWnd = currCanvas;
    if (layerGroup.bShowGeoInMetricProportion)
    {
        vpCreateData.bShowGeoInMetricProportion = true;
    }
    viewport = MapCore.IMcMapViewport.Create(/*Camera*/null, vpCreateData, terrain != null ? [terrain] : null);
    //viewport.SetDebugOption(106, 1);

    // create Edit Mode
    editMode = MapCore.IMcEditMode.Create(viewport);
    editMode.SetEventsCallback(new CEditModeCallback());
    
    // add camera-update callback
    let callback = new CCameraUpdateCallback();
    viewport.AddCameraUpdateCallback(callback);

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
        if (layerGroup.InitialScale2D)
        {
            viewport.SetCameraScale(layerGroup.InitialScale2D);
        }
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
    if (layerGroup.bSetTerrainBoxByStaticLayerOnly)
    {
        viewportData.bSetTerrainBoxByStaticLayerOnly = true;
    }

    if (!bCanvasesMaximized)
    {    
        aViewports.push(viewportData);
        canvasParent.appendChild(currCanvas);
        activeViewport = aViewports.length - 1;
    }
    else
    {
        canvasParent.insertBefore(currCanvas, canvasParent.firstChild);
        aViewports.splice(0, 0, viewportData)
        activeViewport = 0;
    }

    UpdateActiveViewport();
    ResizeCanvases();
    TrySetTerainBox();                
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

            if (aViewportLayers && aViewportLayers.length != 0)
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
function CreateWMTSRasterLayer(serverURL, layersList, coordSystemName, imageFormat, boundingBox, bUseServerTilingScheme, bTransparent, aRequestParams)
{
    let wmtsParams = new MapCore.IMcMapLayer.SWMTSParams;
    wmtsParams.bUseServerTilingScheme = (bUseServerTilingScheme ?? true);
    wmtsParams.pCoordinateSystem = lastCoordSys;
    wmtsParams.pReadCallback = layerCallback;

    wmtsParams.strServerURL = serverURL;
    wmtsParams.strLayersList = layersList;
    wmtsParams.BoundingBox = boundingBox ?? new MapCore.SMcBox(0, 0, 0, 0, 0, 0);
    wmtsParams.strServerCoordinateSystem = coordSystemName;
    wmtsParams.strImageFormat = imageFormat ?? "jpeg";
    wmtsParams.bTransparent = !!bTransparent;
    wmtsParams.aRequestParams = aRequestParams ?? null;
    return MapCore.IMcWebServiceRasterMapLayer.Create(wmtsParams);
}

// function creating WMS raster map layer
function CreateWMSRasterLayer(serverURL, layersList, coordSystemName, imageFormat, boundingBox,  bTransparent, aRequestParams)
{
    let wmsParams = new MapCore.IMcMapLayer.SWMSParams;
    wmsParams.pCoordinateSystem = lastCoordSys;
    wmsParams.pReadCallback = layerCallback;

    wmsParams.strServerURL = serverURL
    wmsParams.strLayersList = layersList;
    wmtsParams.BoundingBox = boundingBox ?? new MapCore.SMcBox(0, 0, 0, 0, 0, 0);
    wmsParams.strServerCoordinateSystem = coordSystemName;
    wmtsParams.strImageFormat = imageFormat ?? "jpeg";
    wmtsParams.bTransparent = !!bTransparent;
    wmtsParams.aRequestParams = aRequestParams ?? null;
    return MapCore.IMcWebServiceRasterMapLayer.Create(wmsParams);
}

// function creating WCS raster map layer
function CreateWCSRasterLayer(serverURL, layersList, coordSystemName, imageFormat)
{
    let wcsParams = new MapCore.IMcMapLayer.SWCSParams;
    wcsParams.pCoordinateSystem = lastCoordSys;
    wcsParams.pReadCallback = layerCallback;

    wcsParams.strServerURL = serverURL;
    wcsParams.strLayersList = layersList;
    wcsParams.strServerCoordinateSystem = coordSystemName ?? "";
    wcsParams.strImageFormat = imageFormat ?? "";
    return MapCore.IMcWebServiceRasterMapLayer.Create(wcsParams);
}

// function creating WCS DTM map layer
function CreateWCSDtmLayer(serverURL, layersList, coordSystemName, imageFormat)
{
    let wcsParams = new MapCore.IMcMapLayer.SWCSParams;
    wcsParams.pCoordinateSystem = lastCoordSys;
    wcsParams.pReadCallback = layerCallback;

    wcsParams.strServerURL = serverURL;
    wcsParams.strLayersList = layersList;
    wcsParams.strServerCoordinateSystem = coordSystemName ?? "";
    wcsParams.strImageFormat = imageFormat ?? "";
    return MapCore.IMcWebServiceDtmMapLayer.Create(wcsParams);
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
                if (pLayer)
                {
                    pLayer.RemoveLayerAsync();
                }
            }
            TrySetTerainBox();                
        },

        // mandatory
        OnReadError: function(pLayer, eErrorCode, strAdditionalDataString)
        {
            console.error("Layer read error: " + MapCore.IMcErrors.ErrorCodeToString(eErrorCode) + " (" + strAdditionalDataString + ")");
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
    
    // create callback class implementing MapCore.IMcMapViewport.ICameraUpdateCallback interface
    CCameraUpdateCallback = MapCore.IMcMapViewport.ICameraUpdateCallback.extend("IMcMapViewport.ICameraUpdateCallback", 
    {
        // mandatory
        OnActiveCameraUpdated: function(pViewport)
        {
            ++uCameraUpdateCounter;
        },

        // optional
        Release: function()
        {
            this.delete();
        },
    });

    // create callback class implementing MapCore.IMcMapViewport.ICameraUpdateCallback interface
    CEditModeCallback = MapCore.IMcEditMode.ICallback.extend("IMcEditMode.ICallback", 
    {
        // optional
        ExitAction: function(nExitCode)
        {
            // EditMode operation finished
        },

        // optional
        Release: function()
        {
            this.delete();
        },
    });

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

    CAsyncOperationCallback = MapCore.IMcMapLayer.IAsyncOperationCallback.extend("IMcMapLayer.IAsyncOperationCallback",
    {
        // optional
        __construct: function(OnResults)
        {
            this.__parent.__construct.call(this);
            this.OnResults = OnResults;
        },

        OnScanExtendedDataResult: function(pLayer, eStatus, aVectorItems, aUnifiedVectorItemsPoints)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnVectorItemPointsResult: function(pLayer, eStatus, aaPoints)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnFieldUniqueValuesResult(pLayer, eStatus, eReturnedType, paUniqueValues)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },
        
        OnVectorItemFieldValueResult: function(pLayer, eStatus, eReturnedType, pValue)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },

        OnVectorQueryResult(pLayer, eStatus, auVectorItemsID)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        },
        
        OnWebServerLayersResults: function(eStatus, strServerURL, eWebMapServiceType, aLayers, astrServiceMetadataURLs, strServiceProviderName)
        {
            this.OnResults.apply(null, arguments);
            this.delete();
        }
    });

    CPrintCallback = MapCore.IMcPrintMap.IPrintCallback.extend("IMcPrintMap.IPrintCallback",
    {
        // optional
        __construct: function(OnResults)
        {
            this.__parent.__construct.call(this);
            this.OnResults = OnResults;
        },

        OnPrintFinished: function(eStatus, strFileNameOrRasterDataFormat, auFileMemoryBuffer)
        {
            this.OnResults.apply(null, arguments);
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

// function switching moving-objects on/off
function DoToggleMoveObjects()
{
    bMoveObjects = !bMoveObjects;
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

// function switching DTM-visualization (height map) on/off
function DoDtmVisualization()
{
    if (!viewport.GetDtmVisualization())
    {
        let result = CalcMinMaxHeights();
        let DtmVisualization = new MapCore.IMcMapViewport.SDtmVisualizationParams();
        MapCore.IMcMapViewport.SDtmVisualizationParams.SetDefaultHeightColors(DtmVisualization, result.minHeight, result.maxHeight);
        DtmVisualization.bDtmVisualizationAboveRaster = true;
        DtmVisualization.uHeightColorsTransparency = 120;
        DtmVisualization.uShadingTransparency = 255;
        viewport.SetDtmVisualization(true, DtmVisualization);
    }
    else
    {
        viewport.SetDtmVisualization(false);
    }
}

// function switching Heighlines-visualization on/off
function DoHeighLinesVisualization()
{

    if (!viewport.GetHeightLinesVisibility() || viewport.GetHeightLines() == null)
    {
        if (viewport.GetHeightLines() == null)
        {
            // create height lines scale step and populate colors

            // 1 color
            // McBColor[] scaleColorsFirst
            let scaleColorsFirst = [];
            scaleColorsFirst[0] = new MapCore.SMcBColor(255, 0, 0, 255);
            scaleColorsFirst[1] = new MapCore.SMcBColor(0, 255, 0, 255);
            scaleColorsFirst[2] = new MapCore.SMcBColor(0, 0, 255, 255);

            // 2 color 
            let scaleColorsSecond = [];
            scaleColorsSecond[0] = new MapCore.SMcBColor(255, 255, 0, 255);
            scaleColorsSecond[1] = new MapCore.SMcBColor(0, 255, 255, 255);
            scaleColorsSecond[2] = new MapCore.SMcBColor(255, 0, 255, 255);


            // height-lines scale step
            //let scaleStep = [];
            //scaleStep[0] = new MapCore.IMcMapGrid.SScaleStep();
            let heightLinesScaleStep = [];

            heightLinesScaleStep[0] = new MapCore.IMcMapHeightLines.SScaleStep();
            heightLinesScaleStep[0].fMaxScale = 10;
            heightLinesScaleStep[0].fLineHeightGap = 10;
            heightLinesScaleStep[0].aColors = scaleColorsFirst;

            heightLinesScaleStep[1] = new MapCore.IMcMapHeightLines.SScaleStep();
            heightLinesScaleStep[1].fMaxScale = 25;
            heightLinesScaleStep[1].fLineHeightGap = 25;
            heightLinesScaleStep[1].aColors = scaleColorsSecond;

            heightLinesScaleStep[2] = new MapCore.IMcMapHeightLines.SScaleStep();
            heightLinesScaleStep[2].fMaxScale = 50;
            heightLinesScaleStep[2].fLineHeightGap = 50;
            heightLinesScaleStep[2].aColors = scaleColorsFirst;

            heightLinesScaleStep[3] = new MapCore.IMcMapHeightLines.SScaleStep();
            heightLinesScaleStep[3].fMaxScale = 300;
            heightLinesScaleStep[3].fLineHeightGap = 100;
            heightLinesScaleStep[3].aColors = scaleColorsSecond;

            let fLineWidth = 1.0;
            let uNumScaleSteps = heightLinesScaleStep.length;

            // create IMcMapHeightLines mapHeightLines = null;
            let mapHeightLines = MapCore.IMcMapHeightLines.Create(heightLinesScaleStep, fLineWidth);
            //let curr_scale_step = mapHeightLines.GetScaleSteps();
            viewport.SetHeightLines(mapHeightLines);
            //let curr_height_lines = viewport.GetHeightLines();
            //let line_width = mapHeightLines.GetLineWidth();
        }
 
        viewport.SetHeightLinesVisibility(true);
    }
    else
    {
        viewport.SetHeightLinesVisibility(false);
    }
}

// render function (called by the browser)
function RenderOrPerformPendingCalcultions()
{
    let currtRenderTime = (new Date).getTime();
    let bRendered = false;
    if (aViewports.length != 0)
    {
        // render viewport(s)
        if (!bSameCanvas)
        {
            for (let i = 0; i < aViewports.length; ++i)
            {
                if (aViewports[i].viewport.HasPendingUpdates())
                {
                    aViewports[i].viewport.Render();
                    bRendered = true;
                }
            }
        }
        else if (viewport != null && viewport.HasPendingUpdates())
        {
            viewport.Render();
            bRendered = true;
        }

        // rotate viewport(s) around center
        if (bRotateAroundCenter)
        {
            let angle = (currtRenderTime - lastRenderTime) / 200;
            if (!bSameCanvas)
            {
                for (let i = 0; i < aViewports.length; ++i)
                {
                    CalcRotationCenter(aViewports[i]);
                    if (aViewports[i].rotationCenter != null)
                    {
                        aViewports[i].viewport.RotateCameraAroundWorldPoint(aViewports[i].rotationCenter, angle, 0, 0, false);
                    }
                }
            }
            else
            {
                CalcRotationCenter(viewport);
                if (viewport.rotationCenter != null)
                {
                    viewport.viewport.RotateCameraAroundWorldPoint(aViewports[i].rotationCenter, angle, 0, 0, false);
                }
            }	
        }

        // move objects
        if (bMoveObjects)
        {
            DoMoveObjects();
        }

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

    if (currtRenderTime >= lastLayersValidityCheckTime + 15000) // 15 seconds
    {
        MapCore.IMcMapLayer.CheckAllNativeServerLayersValidityAsync();
        lastLayersValidityCheckTime = currtRenderTime;
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

// function calculating viewport's rotation center
function CalcRotationCenter(viewportData)
{
    if (viewportData.rotationCenter == null)
    {
        let uWidth = {};
        let uHeight = {};
        viewportData.viewport.GetViewportSize(uWidth, uHeight);
        viewportData.rotationCenter = ViewportScreenToWorld(viewportData.viewport, new MapCore.SMcVector3D(uWidth.Value / 2, uHeight.Value / 2, 0));
    }
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

        // uncomment to test various functionalities
        // let pItem = pScheme.GetEditModeDefaultItem();
        // TestObjectAPI(pObject);
        // TestingText(pItem);
        // TestingIMcObjectSchemeOverloading(pItem);
        // TesingIMcGeometricCalculationsEG2DCirclesUnion();
    }
}

// function updating active viewport / Edit Mode and canvas borders
function UpdateActiveViewport()
{
    if (activeViewport >= 0)
    {
       for (let i = 0; i < aViewports.length; ++i)
        {
            if (i == activeViewport)
            {
                viewport = aViewports[i].viewport;
                editMode = aViewports[i].editMode;

                // update image-processing UI accoring to active-viewport
                UpdateImageProcessingUI(viewport);

                if (!bSameCanvas)
                {
                    aViewports[i].canvas.style.borderColor = "blue";
                }
            }
            else
            {
                aViewports[i].canvas.style.borderColor = "white";
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
async function DoEllipseCPU()
{
    if (ellipseScheme == null)
    {
        ellipseScheme = await FetchObjectScheme("http:ObjectWorld/Schemes/EllipseSchemeCPU.mcsch");
    }
    DoStartInitObject(ellipseScheme);
}

// function starting GPU-based area-of-sight ellipse drawing by EditMode
async function DoEllipseGPU()
{
    if (ellipseSchemeGPU == null)
    {
        ellipseSchemeGPU = await FetchObjectScheme("http:ObjectWorld/Schemes/EllipseSchemeGPU.mcsch");
    }
    DoStartInitObject(ellipseSchemeGPU);
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
    if (bEdit)
    {
        bChangeState = false;
    }
}

// function switching object-state changing on/off
function DoChangeState()
{
    bChangeState = !bChangeState;
    if (bChangeState)
    {
        bEdit = false;
    }
    //TestPrintToRawRasterData();
}

// function switching map rotation on/off
function DoRotateAroundCenter()
{
    bRotateAroundCenter = !bRotateAroundCenter;
    if (!bRotateAroundCenter)
    {
        for (let i = 0; i < aViewports.length; ++i)
        {
            aViewports[i].rotationCenter = null;
        }
    }
}

// function switching wireframe mode on/off (used for debugging purposes)
function DoWireframe()
{
    if (viewport.GetDebugOption(21) == 0)
    {
        viewport.SetDebugOption(21, 2);
    }
    else
    {
        viewport.SetDebugOption(21, 0);
    }
}


function DoVisibleEqualizationHistogram() {

    // set per layer 
    let aLayers = aViewports[activeViewport].aLayers;
    if (!aLayers)
    {
        return;
    }

    for (let layer of aLayers) {
        if (layer.GetLayerType() == MapCore.IMcNativeRasterMapLayer.LAYER_TYPE) {

            let visibleMode = viewport.GetVisibleAreaOriginalHistogram(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL);
            if (visibleMode == true) {
                // disable visible histogram
                //---------------------------

                // disable color-code 
                viewport.SetEnableColorTableImageProcessing(layer, false);

                // set "stub" original histogram -> {0 -> 256}
                let empty_histogram = new Array(256).fill(0.0);
                viewport.SetOriginalHistogram(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, empty_histogram);

                // disable HistogramEqualization
                viewport.SetHistogramEqualization(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, false);

                // disable SetVisibleAreaOriginalHistogram
                viewport.SetVisibleAreaOriginalHistogram(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, false);
            }
            else {
                // enable visible histogram
                //-------------------------

                // enable color-code
                viewport.SetEnableColorTableImageProcessing(layer, true);

                // enable "stub" original histogram -> {0 -> 256}
                let empty_histogram = new Array(256).fill(0.0);
                viewport.SetOriginalHistogram(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, empty_histogram);

                // enable HistogramEqualization
                viewport.SetHistogramEqualization(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, true);

                // enable SetVisibleAreaOriginalHistogram
                viewport.SetVisibleAreaOriginalHistogram(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, true);

            }
        }
    }
}

// viewport statistics (used for debugging purposes)
function DoViewportStatistics()
{
    // MapCore debug "magic" numbers 
    if (viewport.GetDebugOption(24) == 0) {
        viewport.SetDebugOption(24, 1);
    }
    else {
        viewport.SetDebugOption(24, 0);
    }
}

// function loading/removing reach sketches of objects
async function DoSketch()
{
    if (aSketchObjects.length == 0)
    {
        let objects1 = await FetchObjects("http:ObjectWorld/Objects/BattleMngOverlay.mcobj");
        if (objects1 != null)
        {
            aSketchObjects = aSketchObjects.concat(objects1);
        }
        let objects2 = await FetchObjects("http:ObjectWorld/Objects/IntelligenceOverlay.mcobj");
        if (objects2 != null)
        {
            aSketchObjects = aSketchObjects.concat(objects2);
        }
    }
    else
    {
        for (let idx = 0; idx < aSketchObjects.length; ++idx)
        {
            aSketchObjects[idx].Remove();
        }
        aSketchObjects.length = 0;
    }
}

// viewport brightness functions

function DoBrightnessPlus()
{
    viewport.SetBrightness(MapCore.IMcMapViewport.EImageProcessingStage.EIPS_RASTER_LAYERS, viewport.GetBrightness(MapCore.IMcMapViewport.EImageProcessingStage.EIPS_RASTER_LAYERS) + 0.05);
}

function DoBrightnessMinus()
{
    viewport.SetBrightness(MapCore.IMcMapViewport.EImageProcessingStage.EIPS_RASTER_LAYERS, viewport.GetBrightness(MapCore.IMcMapViewport.EImageProcessingStage.EIPS_RASTER_LAYERS) - 0.05);
}

function DoBrightnessZero()
{
    viewport.SetBrightness(MapCore.IMcMapViewport.EImageProcessingStage.EIPS_RASTER_LAYERS, 0);
}

// function maximizing/restoring canvases
function DoResize()
{
    if (!bSameCanvas)
    {
        bCanvasesMaximized = !bCanvasesMaximized;
        
        if (bCanvasesMaximized)
        {
            document.getElementById("Resize").innerHTML = "Restore";
            MoveActiveCanvasToTop();
        }
        else
        {
            document.getElementById("Resize").innerHTML = "Maximize";
        }
        ResizeCanvases();
    }
}

// function resizing canvases when their number is changed or Maximize/Restore button is pressed
function ResizeCanvases()
{
    if (aViewports.length == 0)
     {
         return;
     }

    let CanvasesInRow, CanvasesInColumn;
    if (!bCanvasesMaximized && !bSameCanvas)
    {
        CanvasesInRow = Math.ceil(Math.sqrt(aViewports.length));
        CanvasesInColumn = Math.ceil(aViewports.length / CanvasesInRow);
    }
    else
    {
        CanvasesInRow = 1;
        CanvasesInColumn = 1;
    }

    let width =  (window.innerWidth - 40) / CanvasesInRow - 10;
    let height = (window.innerHeight - 110) / CanvasesInColumn - 15;
    
    for (let i = 0; i < aViewports.length ; i++)
    {
        aViewports[i].canvas.width = width;
        aViewports[i].canvas.height = height;
        aViewports[i].viewport.ViewportResized();
    }
}    

// function moving active canvas to top when Maximize button is pressed
function MoveActiveCanvasToTop()
{
    let viewportData = aViewports[activeViewport];
    canvasParent.removeChild(viewportData.canvas);
    canvasParent.insertBefore(viewportData.canvas, canvasParent.firstChild);
    viewportData.canvas.width = 0;
    aViewports.splice(activeViewport, 1);
    aViewports.splice(0, 0, viewportData);
    activeViewport = 0;
}

// functions selecting active viewport

function DoPrevViewport()
{
    if (aViewports.length > 1)
    {
        activeViewport = (activeViewport + aViewports.length - 1) % aViewports.length;
        UpdateActiveViewport();
        if (bCanvasesMaximized)
        {
            MoveActiveCanvasToTop();
            ResizeCanvases();
        }
    }
}

function DoTransparencyOrderingMode()
{
    viewport.SetTransparencyOrderingMode(!viewport.GetTransparencyOrderingMode());
}

function DoNextViewport()
{
    if (aViewports.length > 1)
    {
        activeViewport = (activeViewport + 1) % aViewports.length;
        UpdateActiveViewport();
        if (bCanvasesMaximized)
        {
            MoveActiveCanvasToTop();
            ResizeCanvases();
        }
    }
}

// function switching between terrain bounding box modes (used for debugging purposes)
function DoTerrainBoundingBoxes()
{
    viewport.IncrementDebugOption(0);
}

// function resetting camera position/orientation
function DoResetCamera()
{
    viewport.SetCameraPosition(aViewports[activeViewport].terrainCenter);
    viewport.SetCameraOrientation(0, 0, 0);
}

// function closing active viewport
function DoCloseMap()
{
    if (activeViewport < 0) 
    {
        return;
    }

    // delete Edit Mode
    editMode.Destroy();
    // delete viewport
    viewport.Release();

    if (!bSameCanvas || aViewports.length == 1)
    {
        // delete canvas
        let currCanvas = aViewports[activeViewport].canvas;
        currCanvas.removeEventListener("wheel", MouseWheelHandler, false);
        currCanvas.removeEventListener("mousemove", MouseMoveHandler, false);
        currCanvas.removeEventListener("mousedown", MouseDownHandler, false);
        currCanvas.removeEventListener("mouseup", MouseUpHandler, false);
        currCanvas.removeEventListener("dblclick", MouseDblClickHandler, false);
        canvasParent.removeChild(aViewports[activeViewport].canvas);
    }

    // remove viewport from viewport data array
    aViewports.splice(activeViewport, 1);
    
    if (aViewports.length == 0)
    {
        // no more viewports

        viewport = null;
        editMode = null;
        activeViewport = -1;
        document.getElementById("SameCanvas").disabled = false;
        document.getElementById("SameCanvasLabel").style.color = "black";

        // delete terrain
        mapTerrains.forEach(terrain => { terrain.Release(); });
        mapTerrains.clear();

        // delete overlay manager
        DoRemoveObjects();
        aSketchObjects.length = 0;
        overlayManager.Release();
        overlayManager = null;
        if (lineScheme != null)
        {
            lineScheme.Release();
            lineScheme = null;
        }
        if (ellipseScheme != null)
        {
            ellipseScheme.Release();
            ellipseScheme = null;
        }
        if (ellipseSchemeGPU != null)
        {
            ellipseSchemeGPU.Release();
            ellipseSchemeGPU = null;
        }
        if (polygonScheme != null)
        {
            polygonScheme.Release();
            polygonScheme = null;
        }
        if (arrowScheme != null)
        {
            arrowScheme.Release();
            arrowScheme = null;
        }
        if (textScheme != null)
        {
            textScheme.Release();
            textScheme = null;
        }
        if (testObjectsScheme != null)
        {
            testObjectsScheme.Release();
            testObjectsScheme = null;
        }
    }
    else
    {
        // there are viewports: update active viewport
        if (activeViewport >= aViewports.length)
        {
            activeViewport = aViewports.length - 1;
        }
        
        UpdateActiveViewport();
        ResizeCanvases();
    }
}

// function refreshing map list
function DoRefreshMaps()
{
    LoadConfigurationFiles();
}

// functions executing image processing
function SetImageProcessingLayerFilter(viewport,isLaterFilter, filterOperation)
{
    if (isLaterFilter) {
        // set per layer 
        let aLayers = aViewports[activeViewport].aLayers;
        if (!aLayers)
        {
            return;
        }

        for (let layer of aLayers) {
            if (layer.GetLayerType() == MapCore.IMcNativeRasterMapLayer.LAYER_TYPE) {
                viewport.SetFilterImageProcessing(layer, filterOperation);
            }
        }
    }
    else {
        // set on viewport 
        viewport.SetFilterImageProcessing(null, filterOperation);
    }
    
}

function SetImageProcessingCustomFilter(viewport, isLayerFilter, uFilterXsize, uFilterYsize, aFilter, fBias, fDivider)
{
    if (isLayerFilter) {
        // set per layer 
        let aLayers = aViewports[activeViewport].aLayers;
        if (!aLayers)
        {
            return;
        }

        for (let layer of aLayers) {
            if (layer.GetLayerType() == MapCore.IMcNativeRasterMapLayer.LAYER_TYPE) {
                viewport.SetCustomFilter(layer, uFilterXsize, uFilterYsize, aFilter, fBias, fDivider);
                viewport.SetFilterImageProcessing(layer, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_CUSTOM_FILTER);
            }
        }
    }
    else {
        // set on viewport 
        viewport.SetCustomFilter(null, uFilterXsize, uFilterYsize, aFilter, fBias, fDivider);
        viewport.SetFilterImageProcessing(null, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_CUSTOM_FILTER);
    }
}

function SetImageProcessingLayerColorTableNegative(viewport,layerFilterMode,enableFlag)
{
    if (layerFilterMode) {
        // set per layer
        let aLayers = aViewports[activeViewport].aLayers;
        if (!aLayers)
        {
            return;
        }

        for (let layer of aLayers) {
            if (layer.GetLayerType() == MapCore.IMcNativeRasterMapLayer.LAYER_TYPE) {
                viewport.SetEnableColorTableImageProcessing(layer, enableFlag);
                viewport.SetNegative(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, enableFlag);
            }
        }
    }
    else {
        // set on viewport 
        viewport.SetEnableColorTableImageProcessing(null, enableFlag);
        viewport.SetNegative(null, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL, enableFlag);

    }
    
}

function ImageProcessTypeCB()
{
    let ip_type = document.getElementById("Ip_Type").value;
    let lp_filter = document.getElementById("Ip_FilterMode").value;
    
    let is_later_filter;
    if (ip_type == 'Type_Default') {
        // reset ImageProcessing on view-port
        is_later_filter = true;
        SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_NO_FILTER);
        SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
        is_later_filter = false;
        SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_NO_FILTER);
        SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
    }
    else {
        // reset current setting and apply new selection 
        is_later_filter = true;
        SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_NO_FILTER);
        SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
        is_later_filter = false;
        SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_NO_FILTER);
        SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);

        // Layer / Viewport ImageProcessing
        if (ip_type == 'Type_Layer')
            is_later_filter = true; // Layer ImageProcessing
        else
            is_later_filter = false; // Viewport ImageProcessing
        
        if (lp_filter != 'Custom_Mode') {
            switch (lp_filter) {
                case "Smooth_Low": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SMOOTH_LOW);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
                } break;
                case "Smooth_Low_Negative": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SMOOTH_LOW);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, true);
                } break;
                case "Smooth_Mid": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SMOOTH_MID);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
                } break;
                case "Smooth_Mid_Negative": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SMOOTH_MID);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, true);
                } break;
                case "Smooth_High": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SMOOTH_HIGH);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
                } break;
                case "Smooth_High_Negative": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SMOOTH_HIGH);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, true);
                } break;
                case "Sharp_Low": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SHARP_LOW);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
                } break;
                case "Sharp_Low_Negative": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SHARP_LOW);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, true);
                } break;
                case "Sharp_Mid": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SHARP_MID);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
                } break;
                case "Sharp_Mid_Negative": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SHARP_MID);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, true);
                } break;
                case "Sharp_High": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SHARP_HIGH);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, false);
                } break;
                case "Sharp_High_Negative": {
                    SetImageProcessingLayerFilter(viewport, is_later_filter, MapCore.IMcImageProcessing.EFilterProccessingOperation.EFPO_SHARP_HIGH);
                    SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, true);
                } break;
            }
        }
        else {
            document.getElementById("Ip_CustomMode").disabled = false;
            let ip_custom = document.getElementById("Ip_CustomMode").value;
            SelectCustomFilter(viewport, ip_custom, is_later_filter);
            //SetImageProcessingLayerColorTableNegative(viewport, is_later_filter, true);
        }
    }

    UpdateImageProcessingUI(viewport);
}

function SelectCustomFilter(currViewport, customMode,layerFilterMode) {

    switch (customMode) {
        case "Custom_1x1": {
            let isLayerFilter = layerFilterMode;
            let x_size = 1;
            let y_size = 1;
            let f_bias = 0.0;
            let f_divider = 1;
            let curr_kernel = [1.0];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_2x2": {
            // G edge-detector
            let isLayerFilter = layerFilterMode;
            let x_size = 2;
            let y_size = 2;
            let f_bias = 0.0;
            let f_divider = 0.5;
            let curr_kernel = [1.0,0.0,0.0,-1.0];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_3x3": {
            // sobel edge detrector
            let isLayerFilter = layerFilterMode;
            let x_size = 3;
            let y_size = 3;
            let f_bias = 0.0;
            let f_divider = 0.5;
            let curr_kernel = [-1.0, 0.0, 1.0, -2.0, 0.0, 2.0, -1.0, 0.0, 1.0];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_4x4": {
            // gaussian blur
            let isLayerFilter = layerFilterMode;
            let x_size = 4;
            let y_size = 4;
            let f_bias = 0.0;
            let f_divider = 32;
            let curr_kernel = [
                1.0, 2.0, 2.0, 1.0,
                2.0, 4.0, 4.0, 2.0,
                2.0, 4.0, 4.0, 2.0,
                1.0, 2.0, 2.0, 1.0];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_5x5": {
            // gaussian blur
            let isLayerFilter = layerFilterMode;
            let x_size = 5;
            let y_size = 5;
            let f_bias = 0.0;
            let f_divider = 256.0;
            let curr_kernel = [
                1.0, 4.0, 6.0, 4.0, 1.0,
                4.0, 16.0, 24.0, 16.0, 4.0,
                6.0, 24.0, 36.0, 24.0, 6.0,
                4.0, 16.0, 24.0, 16.0, 4.0,
                1.0, 4.0, 6.0, 4.0, 1.0
            ];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_6x6": {
            let isLayerFilter = layerFilterMode;
            let x_size = 6;
            let y_size = 6;
            let f_bias = 0.0;
            let f_divider = 36.0;
            let curr_kernel = [
                1.0, 0.0, 1.0, 0.0,1.0,0.0,
                0.0, 1.0, 0.0, 1.0,0.0,1.0,
                1.0, 0.0, 0.0, 1.0,0.0,1.0,
                0.0, 1.0, 1.0, 1.0,1.0,1.0,
                1.0, 0.0, 1.0, 0.0,1.0,0.0,
                0.0, 1.0, 0.0, 1.0,0.0,1.0
            ];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_7x7": {
            // gaussian blur
            let isLayerFilter = layerFilterMode;
            let x_size = 7;
            let y_size = 7;
            let f_bias = 0.0;
            let f_divider = 1023.0;
            let curr_kernel = [
                0.0,0.0,1.0,2.0,1.0,0.0,0.0,
                0.0,3.0,13.0,22.0,13.0,3.0,0.0,
                1.0,13.0,59.0,97.0,59.0,13.0,1.0,
                2.0,22.0,97.0,159.0,97.0,22.0,2.0,
                1.0,13.0,59.0,97.0,59.0,13.0,1.0,
                0.0,3.0,13.0,22.0,13.0,3.0,0.0,
                0.0,0.0,1.0,2.0,1.0,0.0,0.0
            ];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_8x8": {
            // edge-detection 
            let isLayerFilter = layerFilterMode;
            let x_size = 8;
            let y_size = 8;
            let f_bias = 0.0;
            let f_divider = 16.0;
            let curr_kernel = [
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0,-1.0,
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0,-1.0,
                -1.0, -1.0, -1.0, 8.0, 8.0,8.0,-1.0,-1.0,
                -1.0, -1.0, -1.0, 8.0, 8.0, 8.0, -1.0, -1.0,
                -1.0, -1.0, -1.0, 8.0, 8.0, 8.0, -1.0, -1.0,
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0,
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0,
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0
            ];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
        case "Custom_9x9": {
            // edge-detection 
            let isLayerFilter = layerFilterMode;
            let x_size = 9;
            let y_size = 9;
            let f_bias = 0.0;
            let f_divider = 16.0;
            let curr_kernel = [
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0,
                -1.0, -1.0, -1.0, 8.0, 8.0, 8.0, -1.0, -1.0,-1,0,
                -1.0, -1.0, -1.0, 8.0, 8.0, 8.0, -1.0, -1.0,-1.0,
                -1.0, -1.0, -1.0, 8.0, 8.0, 8.0, -1.0, -1.0, -1.0,
                -1.0, -1.0, -1.0, 8.0, 8.0, 8.0, -1.0, -1.0, -1.0,
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0,
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0,
                -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0, -1.0,-1.0
            ];
            SetImageProcessingCustomFilter(currViewport, isLayerFilter, x_size, y_size, curr_kernel, f_bias, f_divider);
        } break;
    }

}

function UpdateImageProcessingFilterUI(filterType, negativeMode,customSize) {

    document.getElementById("Ip_CustomMode").disabled = true;
    switch (filterType) {
        case 1: {
            if (negativeMode)
                document.getElementById("Ip_FilterMode").value = "Smooth_Low_Negative";
            else
                document.getElementById("Ip_FilterMode").value = "Smooth_Low";
        } break;
        case 2: {
            if (negativeMode)
                document.getElementById("Ip_FilterMode").value = "Smooth_Mid_Negative";
            else
                document.getElementById("Ip_FilterMode").value = "Smooth_Mid";
        } break;
        case 3: {
            if (negativeMode)
                document.getElementById("Ip_FilterMode").value = "Smooth_High_Negative";
            else
                document.getElementById("Ip_FilterMode").value = "Smooth_High";
        } break;
        case 4: {
            if (negativeMode)
                document.getElementById("Ip_FilterMode").value = "Sharp_Low_Negative";
            else
                document.getElementById("Ip_FilterMode").value = "Sharp_Low";
        } break;
        case 5: {
            if (negativeMode)
                document.getElementById("Ip_FilterMode").value = "Sharp_Mid_Negative";
            else
                document.getElementById("Ip_FilterMode").value = "Sharp_Mid";
        } break;
        case 6: {
            if (negativeMode)
                document.getElementById("Ip_FilterMode").value = "Sharp_High_Negative";
            else
                document.getElementById("Ip_FilterMode").value = "Sharp_High";
        } break;
        case 7: {
            // custom kernel size 
            document.getElementById("Ip_FilterMode").value = "Custom_Mode";
            document.getElementById("Ip_CustomMode").disabled = false;
            switch (customSize) {
                case 1: {
                    document.getElementById("Ip_CustomMode").value = "Custom_1x1";
                } break;
                case 2: {
                    document.getElementById("Ip_CustomMode").value = "Custom_2x2";
                } break;
                case 3: {
                    document.getElementById("Ip_CustomMode").value = "Custom_3x3";
                } break;
                case 4: {
                    document.getElementById("Ip_CustomMode").value = "Custom_4x4";
                } break;
                case 5: {
                    document.getElementById("Ip_CustomMode").value = "Custom_5x5";
                } break;
                case 6: {
                    document.getElementById("Ip_CustomMode").value = "Custom_6x6";
                } break;
                case 7: {
                    document.getElementById("Ip_CustomMode").value = "Custom_7x7";
                } break;
                case 8: {
                    document.getElementById("Ip_CustomMode").value = "Custom_8x8";
                } break;
                case 9: {
                    document.getElementById("Ip_CustomMode").value = "Custom_9x9";
                } break;
            }
        }
    }
}

function UpdateImageProcessingUI(viewport) {

    // check if ImageProcessing is available on this platform
    try {
        viewport.GetFilterImageProcessing(null);
    }
    catch (ex) {
        if (ex instanceof MapCore.CMcError) {
            document.getElementById("Ip_Type").disabled = true;
            document.getElementById("Ip_FilterMode").disabled = true;
            document.getElementById("Ip_CustomMode").disabled = true;
            return;
        }
        else {
            throw ex;
        }
    }

    let ip_type = document.getElementById("Ip_Type").value;
    let lp_filter = document.getElementById("Ip_FilterMode").value;
    
    let type_filter = viewport.GetFilterImageProcessing(null);
    // view-port image-processing 
    if (type_filter.value >= 1) {
        document.getElementById("Ip_FilterMode").disabled = false;
        document.getElementById("Ip_CustomMode").disabled = true;
        document.getElementById("Ip_Type").value = "Type_Viewport";
        // negative mode 
        let negative_mode = viewport.GetNegative(null, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL);
        // custom type
        let x_size = [];
        let y_size = [];
        let f_bias = [];
        let f_dvider = [];
        let filter_buf = [];
        viewport.GetCustomFilter(null, x_size, y_size, filter_buf, f_bias, f_dvider);
        let custom_size = x_size.Value; // assume x_size=y_size
        // update filter UI 
        UpdateImageProcessingFilterUI(type_filter.value, negative_mode,custom_size);
    }
    else {
        // layer image-processing
        // start with default -> no-image processing
        document.getElementById("Ip_FilterMode").disabled = true;
        document.getElementById("Ip_CustomMode").disabled = true;
        document.getElementById("Ip_Type").value = "Type_Default";
        document.getElementById("Ip_FilterMode").value = "Smooth_Low";
        let aLayers = aViewports[activeViewport].aLayers;
        if (!aLayers)
        {
            return;
        }

        for (let layer of aLayers) {
            if (layer.GetLayerType() == MapCore.IMcNativeRasterMapLayer.LAYER_TYPE) {
                type_filter = viewport.GetFilterImageProcessing(layer);
                if (type_filter.value >= 1) {
                    document.getElementById("Ip_FilterMode").disabled = false;
                    document.getElementById("Ip_Type").value = "Type_Layer";
                    // negative mode 
                    let negative_mode = viewport.GetNegative(layer, MapCore.IMcImageProcessing.EColorChannel.ECC_MULTI_CHANNEL);
                    // custom type
                    let x_size = [];
                    let y_size = [];
                    let f_bias = [];
                    let f_dvider = [];
                    let filter_buf = [];
                    viewport.GetCustomFilter(layer, x_size, y_size, filter_buf, f_bias, f_dvider);
                    let custom_size = x_size.Value; // assume x_size=y_size
                    // update filter UI 
                    UpdateImageProcessingFilterUI(type_filter.value, negative_mode, custom_size);
                }
            }
        }
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

        if (viewport.GetDtmVisualization())
        {
            DoDtmVisualization();
            DoDtmVisualization();
        }
    }

    e.preventDefault();
    e.cancelBubble = true;
    if (e.stopPropagation) e.stopPropagation();
}

function MouseMoveHandler(e) 
{
    if (viewport.GetWindowHandle() != e.target)
    {
        return;
    }

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
    if (editMode.IsEditingActive())
    {
        // EditMode is active: don't change active viewport, but ignore click on non-active one
        if (viewport.GetWindowHandle() != e.target)
        {
           return;
        }
    }
    else if (!bSameCanvas)
    {
        for (let i = 0; i< aViewports.length; i++)
        {
            if (e.target ==  aViewports[i].viewport.GetWindowHandle())
            {
                activeViewport = i;
                UpdateActiveViewport();
                break;
            }
            
        }
    }

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
    if (viewport.GetWindowHandle() != e.target)
    {
        return;
    }
    
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
    if (viewport.GetWindowHandle() != e.target)
    {
        return;
    }
    
    let EventPixel = MapCore.SMcPoint(e.offsetX, e.offsetY);
    let buttons = mouseDownButtons & ~e.buttons;
    if (bChangeState || bEdit)
    {
        let ScanPointGeometry = new MapCore.SMcScanPointGeometry(MapCore.EMcPointCoordSystem.EPCS_SCREEN, MapCore.SMcVector3D(EventPixel.x, EventPixel.y, 0), 20);
        let aTargets = viewport.ScanInGeometry(ScanPointGeometry, false);
        for (let i = 0; i < aTargets.length; ++i)
        {
            if (aTargets[i].eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_OVERLAY_MANAGER_OBJECT)
            {
                if (bChangeState)
                {
                    let auStates = aTargets[i].ObjectItemData.pObject.GetState();
                    let uState = (auStates.length >= 1 ? auStates[0] : 0);
                    aTargets[i].ObjectItemData.pObject.SetState([(uState + 1) % 7]);

                    // TestSaveObjectsAsRawVectorData(aTargets[i].ObjectItemData.pObject);
                    // TestSaveObjectsAsRawVectorData();
                    // TestingSubItemPropertyScheme(aTargets[i].ObjectItemData.pObject);
                    // TestingSubItemProperty(aTargets[i].ObjectItemData.pObject);
                    // TestingSetterAndGetterVisibilityOption(aTargets[i].ObjectItemData.pObject)
                    // TestingAreaOfSightCalc(aTargets[i].ObjectItemData.pObject.GetLocationPoints()[0]);
                    // TestSaveToBuffer(aTargets[i].ObjectItemData.pObject);
                }

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
            else if(aTargets[i].eTargetType == MapCore.IMcSpatialQueries.EIntersectionTargetType.EITT_VISIBLE_VECTOR_LAYER)
            {
                //TestVectorItemCallbacks(aTargets[i], ScanPointGeometry);
            }
        }
        bChangeState = false;
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

// function fetching an object schemes' file from server and loading the scheme
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

// function fetching an objects' file from server and loading the objects
async function FetchObjects(uri)
{
    let bytes = await FetchFileToByteArray(uri);
    if (bytes != null)
    {
        return overlay.LoadObjects(bytes);
    }
    else
    {
        return null;
    }
}

// function fetching a JSON file from server to object
async function FetchJsonToObject(uri) 
{
    try
    {
        let fetchResponse = await fetch(uri);
        if (fetchResponse.ok)
        {
        return await fetchResponse.json();
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

//  function parsing layers params from JSON configuration and building MapCore.IMcMapTerrain.SLayerParams
function ParseLayerParams(jsonLayerGroup)
{
    if (jsonLayerGroup.layerParams)
    {
        let layerParams = new MapCore.IMcMapTerrain.SLayerParams();
        for (let field in jsonLayerGroup.layerParams)
        {
            if (layerParams.hasOwnProperty(field))
            {
                layerParams[field] = jsonLayerGroup.layerParams[field];
            }
            else
            {
                alert("Invalid parameter in layerParams");
                return undefined;
            }
        }
        return layerParams;
    }
    return null;
}

// function loading and processing JSON configuration file and optional capabilities XML of MapCoreLayerServer and/or WMTS servers listed in the configuration file
async function LoadConfigurationFiles()
{
    mapTerrains.forEach(terrain => { terrain.Release(); });
    mapTerrains.clear();
    mapLayerGroups.clear();
    aServerLayerGroups.length = 0;
    bConfigurationFilesLoaded = false;
    DisableGUI();
    
    // fetch main configuration file from URL defined in html file
    let jsonObj = await FetchJsonToObject(ConfigurationFileURL);

    // if it has been fetched and MapCoreLayerServerURL parameter exists, fetch MapCoreLayerServer's capabilites file to retrieve its layers
    if (jsonObj != null)
    {
        bUseDeprecatedPreloadingLayerFiles = (jsonObj.useDeprecatedPreloadingLayerFiles == true);
        uMemUsageLoggingFrequency = jsonObj.memUsageLoggingFrequency ?? 0;
        if (!bUseDeprecatedPreloadingLayerFiles && jsonObj.streamingLayers)
        {
            ParseLayersConfiguration(jsonObj.streamingLayers);
        }
        if (bUseDeprecatedPreloadingLayerFiles && jsonObj.deprecatedPreloadedLayers)
        {
            // Deprecated: non-streaming mode
            DeprecatedParseLayersConfiguration(jsonObj.deprecatedPreloadedLayers);
        }
        if (jsonObj.MapCoreLayerServerURL)
        {
            FetchAndParseCapabilitiesXMLs(jsonObj.MapCoreLayerServerURL, MapCore.IMcMapLayer.EWebMapServiceType.EWMS_MAPCORE);
        }
        else if (aServerLayerGroups.length != 0)
        {
            let group = aServerLayerGroups.shift();
            FetchAndParseCapabilitiesXMLs(group.strServerURL, group.eServerType, group);
        }
        else
        {
            if (bReloadConfigurationFiles)
            {
                bReloadConfigurationFiles = false;
                LoadConfigurationFiles();
            }
            else
            {
                bConfigurationFilesLoaded = true;

                // initialize GUI for opening viewports
                InitializeGUI();
            }
        }
    }
}

//  function parsing streaming layers' configuration and building layer groups
function ParseLayersConfiguration(jsonLayerGroups)
{
    try
    {
        for (let jsonGroup of jsonLayerGroups)
        {
            // coordinate system creation string: MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84) etc.
            let pCoordSystem = jsonGroup.coordSystemType ? eval("MapCore." + jsonGroup.coordSystemType + ".Create(" + jsonGroup.coordSystemParams + ")") : null;
            if (pCoordSystem)
            {
                pCoordSystem.AddRef();
            }

            let layerGroup = new SLayerGroup(pCoordSystem, jsonGroup.showGeoInMetricProportion, jsonGroup.centerByStaticObjectsLayerOnly, jsonGroup.InitialScale2D);

            if (jsonGroup.layers)
            {
                for (let layer of jsonGroup.layers)
                {
                    let layerCreateString = null;
                    switch (layer.type)
                    {
                        case "WMSRaster":
                            // WMS raster layer creation string: CreateWMSRasterLayer('http://wmsserver', 'layer', 'EPSG:4326', 'jpeg') etc.
                            layerCreateString = "Create" + layer.type + "Layer('" + layer.path + "'" + (layer.params ? ", " + layer.params : "") + ")";
                            break;
                        case "WCSRaster":
                            // WCS raster layer creation string: CreateWCSRasterLayer('http://wcsserver', 'layer') etc.
                            layerCreateString = "Create" + layer.type + "Layer('" + layer.path + "'" + (layer.params ? ", " + layer.params : "") + ")";
                            break;
                        case "WCSDtm":
                            // WCS DTM layer creation string: CreateWCSDtmLayer('http://wcsserver', 'layer') etc.
                            layerCreateString = "Create" + layer.type + "Layer('" + layer.path + "'" + (layer.params ? ", " + layer.params : "") + ")";
                            break;
                        case "IMcNativeRasterMapLayer":
                            layerCreateString = "MapCore.IMcNativeRasterMapLayer.Create('" + layer.path + "', " + (layer.params ?? "MapCore.UINT_MAX, false, 0, false") + ", layerCallback)";
                            break;
                        case "IMcNativeDtmMapLayer":
                            layerCreateString = "MapCore.IMcNativeDtmMapLayer.Create('" + layer.path + "', " + (layer.params ?? "0") + ", layerCallback)";
                            break;
                        case "IMcNativeVectorMapLayer":
                            layerCreateString = "MapCore.IMcNativeVectorMapLayer.Create('" + layer.path + "', " + (layer.params ?? "") + "layerCallback)";
                            break;
                        case "IMcNative3DModelMapLayer":
                            layerCreateString = "MapCore.IMcNative3DModelMapLayer.Create('" + layer.path +  "', " + (layer.params ?? "0") + ", layerCallback)";
                            break;
                        case "IMcNativeVector3DExtrusionMapLayer":
                            layerCreateString = "MapCore.IMcNativeVector3DExtrusionMapLayer.Create('" + layer.path +  "', " + (layer.params ?? "0, 10") + ", layerCallback)";
                            break;
                        case "IMcNativeTraversabilityMapLayer":
                            layerCreateString = "MapCore.IMcNativeTraversabilityMapLayer.Create('" + layer.path + "', " + (layer.params ?? "false") + ", layerCallback)";
                            break;
                        case "IMcNativeMaterialMapLayer":
                            layerCreateString = "MapCore.IMcNativeMaterialMapLayer.Create('" + layer.path + "', " + (layer.params ?? "false") + ", layerCallback)";
                            break;
                        case "IMcRaw3DModelMapLayer":
                            layerCreateString = "MapCore.IMcRaw3DModelMapLayer.Create('" + layer.path + "', group.pCoordSystem, false, null, 0.05, layerCallback, group.aRequestParams)";
                            break;
                        default:
                            alert("Invalid type of layer");
                            return;
                    }
                    layerGroup.aLayerCreateStrings.push(layerCreateString);
                    let layerParams = ParseLayerParams(layer);
                    if (layerParams === undefined)
                    {
                        return;
                    }
                    layerGroup.aLayerParams.push(layerParams);
                }
            }
            layerGroup.aRequestParams = (jsonGroup.serverParams ? JsonObjectToKeyValueArray(jsonGroup.serverParams) : null);
            if (jsonGroup.groupName)
            {
                mapLayerGroups.set(jsonGroup.groupName, layerGroup);
            }
            else
            {
                // check whether it is server group
                if (jsonGroup.wmtsServerURL)
                {
                    layerGroup.strServerURL = jsonGroup.wmtsServerURL;
                    layerGroup.eServerType = MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMTS;
                }
                else if (jsonGroup.wmsServerURL)
                {
                    layerGroup.strServerURL = jsonGroup.wmsServerURL;
                    layerGroup.eServerType = MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMS;
                }
                else if (jsonGroup.cswServerURL)
                {
                    layerGroup.strServerURL = jsonGroup.cswServerURL;
                    layerGroup.eServerType = MapCore.IMcMapLayer.EWebMapServiceType.EWMS_CSW;
                }
                else
                {
                    continue;
                }

                // server group
                layerGroup.tileMatrixSetFilter = jsonGroup.tileMatrixSetFilter;
                aServerLayerGroups.push(layerGroup);
            }
        }
    }
    catch (e)
    {
        alert("Invalid configuration JSON file");
    }
}

// function fetching, parsing and processing capabilities XML files of MapCoreLayerServer and/or WMTS server to retrieve layers
async function FetchAndParseCapabilitiesXMLs(strServerURL, eWebMapServiceType, ServerLayerGroup)
{
    let pAsyncOperationCallback = new CAsyncOperationCallback(
        (eStatus, strServerURL, eWebMapServiceType, aLayers, astrServiceMetadataURLs, strServiceProviderName) =>
        {
            ProcessWebServerLayersResults(eStatus, strServerURL, eWebMapServiceType, aLayers, ServerLayerGroup);

            if (aServerLayerGroups.length != 0)
            {
                let group = aServerLayerGroups.shift();
                setTimeout(()=> 
                    {
                        FetchAndParseCapabilitiesXMLs(group.strServerURL, group.eServerType, group); 
                    }, 1000);
//                FetchAndParseCapabilitiesXMLs(group.strServerURL, group.eServerType, group);
            }
            else
            {
                if (bReloadConfigurationFiles)
                {
                    bReloadConfigurationFiles = false;
                    LoadConfigurationFiles();
                }
                else
                {
                    bConfigurationFilesLoaded = true;

                    // initialize GUI for opening viewports
                    InitializeGUI();
                }
            }
        }
    );

    MapCore.IMcMapDevice.GetWebServerLayers(strServerURL, eWebMapServiceType, ServerLayerGroup.aRequestParams, pAsyncOperationCallback); // async, wait for OnWebServerLayersResults()
}

// function processing array of SServerLayerInfo of MapCoreLayerServer or WMTS server to retrieve layers
function ProcessWebServerLayersResults(eStatus, strServerURL, eWebMapServiceType, aLayers, ServerLayerGroup)
{
    if (eStatus == MapCore.IMcErrors.ECode.SUCCESS)
    {
        if (aLayers && aLayers.length > 0)
        {
            for (let layer of aLayers)
            {
                let aTileMatrixSets = layer.aTileMatrixSets;
                if (aTileMatrixSets.length == 0)
                {
                    aTileMatrixSets.push(null);
                }
                for (let i = 0; i < aTileMatrixSets.length; ++i)
                {
                    let matrixSet = aTileMatrixSets[i];
                    let pCoordSystem = null;
                    let BoundingBox = null;
                    if (matrixSet != null)
                    {
                        pCoordSystem = matrixSet.pCoordinateSystem;
                        if (!pCoordSystem && (eWebMapServiceType == MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMS || eWebMapServiceType == MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WCS))
                        {
                            try
                            {
                                pCoordSystem = MapCore.IMcGridGeneric.Create(matrixSet.strName);
                            }
                            catch (ex)
                            {
                                if (ex instanceof MapCore.CMcError)
                                {
                                    continue;
                                }
                                else
                                {
                                    throw ex;
                                }
                            }
                        }
                        if (matrixSet.bHasBoundingBox)
                        {
                            BoundingBox = matrixSet.BoundingBox;
                        }

                    }
                
                    if (!pCoordSystem && i == aTileMatrixSets.length - 1)
                    {
                        pCoordSystem = layer.pCoordinateSystem;
                        BoundingBox = layer.BoundingBox;
                    }
                    if (!pCoordSystem)
                    {
                        continue;
                    }

                    pCoordSystem.AddRef();

                    if (BoundingBox == null && layer.pCoordinateSystem)
                    {
                        if (pCoordSystem == layer.pCoordinateSystem)
                        {
                            BoundingBox = layer.BoundingBox;
                        }
                        else
                        {
                            try
                            {
                                let Converter = MapCore.IMcGridConverter.Create(layer.pCoordinateSystem, pCoordSystem);
                                let Box1 = new MapCore.SMcBox(Converter.ConvertAtoB(layer.BoundingBox.MinVertex), 
                                    Converter.ConvertAtoB(new MapCore.SMcVector3D(layer.BoundingBox.MinVertex.x, layer.BoundingBox.MaxVertex.y, 0)));
                                let Box2 = new MapCore.SMcBox(Converter.ConvertAtoB(layer.BoundingBox.MaxVertex), 
                                Converter.ConvertAtoB(new MapCore.SMcVector3D(layer.BoundingBox.MaxVertex.x, layer.BoundingBox.MinVertex.y, 0)));
                                MapCore.SMcBox.Normalize(Box1);
                                MapCore.SMcBox.Normalize(Box2);
                                BoundingBox = new MapCore.SMcBox;
                                MapCore.SMcBox.Union(BoundingBox, Box1, Box2);
                            }
                            catch (ex)
                            {
                                if (ex instanceof MapCore.CMcError)
                                {
                                    console.error(ex.message);
                                    continue;
                                }
                                else
                                {
                                    throw ex;
                                }
                            }
                        }
                    }

                    let coordSystemStr; 
                    if (pCoordSystem != null && pCoordSystem.GetGridCoorSysType() == MapCore.IMcGridGeneric.GRID_COOR_SYS_TYPE)
                    {
                        let astrCreateParams = [];
                        let bSRID = {};
                        pCoordSystem.GetCreateParams(astrCreateParams, bSRID);
                        if (astrCreateParams.length == 1 && bSRID.Value == true)
                        {
                            coordSystemStr = astrCreateParams[0];
                        }
                    }
                    if (matrixSet != null && ServerLayerGroup && ServerLayerGroup.tileMatrixSetFilter && coordSystemStr != ServerLayerGroup.tileMatrixSetFilter)
                    {
                        continue;
                    }

                    let aGroups = [];
                    if (eWebMapServiceType == MapCore.IMcMapLayer.EWebMapServiceType.EWMS_MAPCORE)
                    {
                        for (let strLayerGroup of layer.astrGroups)
                        {
                            aGroups.push(strLayerGroup + " (server " + coordSystemStr + ")");
                        }
                    }
                    else
                    {
                        let strServiceName = eWebMapServiceType.constructor.name;
                        strServiceName = strServiceName.slice(strServiceName.lastIndexOf("_") + 1);
                        let groupName = layer.strTitle;
                        if (groupName == null)
                        {
                            groupName = layer.strLayerId;
                        }

                        if (layer.astrImageFormats.length != 0)
                        {
                            for (let j = 0; j < layer.astrImageFormats.length; ++j)
                            {
                                layer.astrImageFormats[j] = layer.astrImageFormats[j].replace("image/", "");
                                aGroups.push(groupName  + " (" + strServiceName + ", " + layer.astrImageFormats[j] + ", " + matrixSet.strName + ")");
                            }
                        }
                        else
                        {
                            aGroups.push(groupName + " (" + strServiceName + ")");
                        }
                    }

                    for (let group of aGroups)
                    {
                        let layerGroup = mapLayerGroups.get(group);
                        let bGroupAdded = false;
                        if (layerGroup == undefined)
                        {
                            layerGroup = new SLayerGroup(pCoordSystem, true); // for MapCoreLayerServer only: bShowGeoInMetricProportion is true
                            mapLayerGroups.set(group, layerGroup);
                            bGroupAdded = true;
                        }
                        else if (!pCoordSystem.IsEqual(layerGroup.pCoordSystem))
                        {
                            alert("Layers' coordinate systems do not match");
                            return;
                        }

                        let layerCreateString;
                        if (eWebMapServiceType == MapCore.IMcMapLayer.EWebMapServiceType.EWMS_MAPCORE)
                        {
                            let layerUrlString = "'" + strServerURL + "/" + layer.strLayerId + "'";
                            switch (layer.strLayerType)
                            {
                                case "MapCoreServerRaster":
                                    layerCreateString = "MapCore.IMcNativeServerRasterMapLayer.Create(" + layerUrlString + ", layerCallback)";
                                    break;
                                case "MapCoreServerDTM":
                                    layerCreateString = "MapCore.IMcNativeServerDtmMapLayer.Create(" + layerUrlString + ", layerCallback)";
                                    break;
                                case "MapCoreServerVector":
                                    layerCreateString = "MapCore.IMcNativeServerVectorMapLayer.Create(" + layerUrlString + ", layerCallback)";
                                    break;
                                case "MapCoreServerStaticObjects":
                                case "MapCoreServerVector3DExtrusion":
                                    layerCreateString = "MapCore.IMcNativeServerVector3DExtrusionMapLayer.Create(" + layerUrlString +  ", layerCallback)";
                                    break;
                                case "MapCoreServer3DModel":
                                    layerCreateString = "MapCore.IMcNativeServer3DModelMapLayer.Create(" + layerUrlString +  ", layerCallback)";
                                    break;
                                case "MapCoreServerTraversability":
                                    layerCreateString = "MapCore.IMcNativeServerTraversabilityMapLayer.Create(" + layerUrlString + ", layerCallback)";
                                    break;
                                case "MapCoreServerMaterial":
                                    layerCreateString = "MapCore.IMcNativeServerMaterialMapLayer.Create(" + layerUrlString + ", layerCallback)";
                                    break;
                                default:
                                    alert("Invalid type of server layer");
                                    return;
                            }
                            layerGroup.aLayerCreateStrings.push(layerCreateString);
                            let layerParams = null;
                            if (layer.nDrawPriority != 0)
                            {
                                layerParams = new MapCore.IMcMapTerrain.SLayerParams();
                                layerParams.nDrawPriority = layer.nDrawPriority;
                            }
                            layerGroup.aLayerParams.push(layerParams);
                        }
                        else
                        {
                            // non-MapCore server
                            switch (eWebMapServiceType)
                            {
                                case MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMTS:
                                    // WMTS raster layer creation string: CreateWMTSRasterLayer('http://wmtsserver', 'layer', 'mat_4326_18102969075459406051_t2', 'jpeg') etc.
                                    layerCreateString = "CreateWMTSRasterLayer('" + strServerURL + "', '" + layer.strLayerId + "', '" + matrixSet.strName + "', '" + layer.astrImageFormats[i] + "', BoundingBox, undefined, undefined, group.aRequestParams)";
                                    break;
                                case MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMS:
                                    // WMS raster layer creation string: CreateWMSRasterLayer('http://wmsserver', 'layer', 'EPSG:4326', 'jpeg') etc.
                                    layerCreateString = "CreateWMSRasterLayer('" + strServerURL + "', '" + layer.strLayerId + "', '" + matrixSet.strName + "', '" + layer.astrImageFormats[i] + "', BoundingBox, true)";
                                    break;
                                case MapCore.IMcMapLayer.EWebMapServiceType.EWMS_CSW:
                                    if (layer.strLayerType == "RAW_3D_MODEL")
                                    {
                                        layerCreateString = "MapCore.IMcRaw3DModelMapLayer.Create('" + layer.strLayerId + "', group.pCoordSystem, false, BoundingBox ?? null, 0.05, layerCallback, group.aRequestParams)";
                                    }
                                    else if (layer.strLayerType == "WMTS_SERVER_URL")
                                    {
                                        // add new WMTS-server group
                                        let wmtsGroup = new SLayerGroup(ServerLayerGroup.pCoordSystem, ServerLayerGroup.bShowGeoInMetricProportion, 
                                                                        ServerLayerGroup.bSetTerrainBoxByStaticLayerOnly, ServerLayerGroup.InitialScale2D);
                                        wmtsGroup.strServerURL = layer.strLayerId;
                                        wmtsGroup.eServerType = MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMTS;
                                        wmtsGroup.aRequestParams = ServerLayerGroup.aRequestParams;
                                        aServerLayerGroups.push(wmtsGroup);

                                        // skip adding this "layer" to terrain group (and delete the terrain group if just created)
                                        if (bGroupAdded)
                                        {
                                            mapLayerGroups.delete(group);
                                        }
                                        continue;
                                    }
                                    else
                                    {
                                        alert("Unsupported CSW layer type");
                                    }
                                    break;
                                default:
                                    alert("Unsupported server type");
                                    return;
                            }

                            layerGroup.aLayerCreateStrings.push(layerCreateString);
                            layerGroup.aLayerBoundingBoxes.push(BoundingBox);

                            let layerParams = ParseLayerParams(layer);
                            if (layerParams === undefined)
                            {
                                return;
                            }
                            layerGroup.aLayerParams.push(layerParams);
        
                            if (ServerLayerGroup)
                            {
                                layerGroup.aLayerCreateStrings = layerGroup.aLayerCreateStrings.concat(ServerLayerGroup.aLayerCreateStrings);
                                layerGroup.bSetTerrainBoxByStaticLayerOnly = ServerLayerGroup.bSetTerrainBoxByStaticLayerOnly;
                                layerGroup.bShowGeoInMetricProportion = ServerLayerGroup.bShowGeoInMetricProportion;
                                layerGroup.InitialScale2D = ServerLayerGroup.InitialScale2D;
                                if (ServerLayerGroup.aRequestParams)
                                {
                                    for (let j = 0; j < ServerLayerGroup.aRequestParams.length; ++j)
                                    {
                                        if (ServerLayerGroup.aRequestParams[j].strKey == "McCswQueryBody")
                                        {
                                            ServerLayerGroup.aRequestParams.splice(j, 1);
                                        }
                                    }
                                }
                                layerGroup.aRequestParams = ServerLayerGroup.aRequestParams;
                            }
                        }
                    }
                }
            }
        }
    }
    else
    {
        alert(MapCore.IMcErrors.ErrorCodeToString(eStatus));
    }
}

function JsonObjectToKeyValueArray(obj)
{
    let aKeyValueArray = [];
    for (const [key, value] of Object.entries(obj))
    {
        let KeyValue = new MapCore.SMcKeyStringValue();
        KeyValue.strKey = key;
        KeyValue.strValue = value;
        aKeyValueArray.push(KeyValue);
    }
    return aKeyValueArray;
}

////////////////////////////////////////////////////////////////////////////////////////////////
// DEPRECATED FUNCTIONS FOR NON-STREAMING MODE ONLY

let CDeprecatedLoadFilesCallback;
let aLoadFilesLists = []; // array of lists of files to load dynamically

// Path class for list of files to be loaded dynamically
class CDeprecatedPath
{
    constructor(type, url, list)
    {
        this.type = type;                                           // map layer type or -1 for files other than map layer
        this.url = url;                                             // server URL with root durectory
        this.list = list + ".txt";                                  // list file name
        this.dir = list;                                            // map layer directory
    }
}

//  function parsing deprecated non-streaming layers' configuration and building layer groups
function DeprecatedParseLayersConfiguration(jsonLayerGroups)
{
    try
    {
        for (let jsonGroup of jsonLayerGroups)
        {
            // coordinate system creation string: "MapCore.IMcGridCoordSystemGeographic.Create(MapCore.IMcGridCoordinateSystem.EDatumType.EDT_WGS84)" etc.
            let pCoordSystem = jsonGroup.coordSystemType ? eval("MapCore." + jsonGroup.coordSystemType + ".Create(" + jsonGroup.coordSystemParams + ")") : null;
            if (pCoordSystem)
            {
                pCoordSystem.AddRef();
            }

            let layerGroup = new SLayerGroup(pCoordSystem, jsonGroup.centerByStaticObjectsLayerOnly, jsonGroup.showGeoInMetricProportion, jsonGroup.InitialScale2D);

            for (let layer of jsonGroup.layers)
            {
                let layerCreateString = null;
                switch (layer.type)
                {
                    case "WMSRaster":
                        // WMS raster layer creation string: CreateWMSRasterLayer('http://wmsserver', 'layer', 'EPSG:4326', 'jpeg') etc.
                        layerCreateString = "Create" + layer.type + "Layer('" + layer.path + "'";
                        break;
                    case "WCSRaster":
                        // WCS raster layer creation string: CreateWCSRasterLayer('http://wcsserver', 'layer') etc.
                        layerCreateString = "Create" + layer.type + "Layer('" + layer.path + "'";
                        break;
                    case "WCSDtm":
                        // WCS DTM layer creation string: CreateWCSDtmLayer('http://wcsserver', 'layer') etc.
                        layerCreateString = "Create" + layer.type + "Layer('" + layer.path + "'";
                        break;
                    default:
                    {
                        let lastSlashIndex = layer.path.lastIndexOf("/");
                        if (lastSlashIndex >= 0)
                        {
                            let path = layer.path.substring(0, lastSlashIndex);
                            let dir = layer.path.substring(lastSlashIndex + 1);
            
                            // native/raw layer creation string: new CDeprecatedPath(MapCore.IMcNativeRasterMapLayer.LAYER_TYPE, 'http:Maps/Raster', 'SwissOrtho-GW') etc.
                            layerCreateString = "new CDeprecatedPath(MapCore." + layer.type + ".LAYER_TYPE, '" + path + "', '" + dir + "'";
                        }
                        break;
                    }
                }

                if (layer.params)
                {
                    layerCreateString += ", " + layer.params;
                }
                layerCreateString += ")";
                layerGroup.aLayerCreateStrings.push(layerCreateString);

                let layerParams = ParseLayerParams(layer);
                if (layerParams === undefined)
                {
                    return;
                }
                layerGroup.aLayerParams.push(layerParams);
            }
            layerGroup.aRequestParams = (jsonGroup.serverParams ? JsonObjectToKeyValueArray(jsonGroup.serverParams) : null);
            if (jsonGroup.groupName)
            {
                mapLayerGroups.set(jsonGroup.groupName, layerGroup);
            }
            else
            {
                // check whether it is server group
                if (jsonGroup.wmtsServerURL)
                {
                    layerGroup.strServerURL = jsonGroup.wmtsServerURL;
                    layerGroup.eServerType = MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMTS;
                }
                else if (jsonGroup.wmsServerURL)
                {
                    layerGroup.strServerURL = jsonGroup.wmsServerURL;
                    layerGroup.eServerType = MapCore.IMcMapLayer.EWebMapServiceType.EWMS_WMS;
                }
                else if (jsonGroup.cswServerURL)
                {
                    layerGroup.strServerURL = jsonGroup.cswServerURL;
                    layerGroup.eServerType = MapCore.IMcMapLayer.EWebMapServiceType.EWMS_CSW;
                }
                else
                {
                    continue;
                }

                // server group
                layerGroup.tileMatrixSetFilter = jsonGroup.tileMatrixSetFilter;
                aServerLayerGroups.push(layerGroup);
            }
        }
    }
    catch (e)
    {
        alert("Invalid configuration JSON file");
    }
}

function DeprecatedLoadMapLayerFilesAsync() 
{
    if (mapTerrains.get(lastTerrainConfiguration) == undefined)
    {
        // create callback class implementing MapCore.IMcMapDevice.ICallback
        CDeprecatedLoadFilesCallback = MapCore.IMcMapDevice.ICallback.extend("IMcMapDevice.ICallback",
        {
            // mandatory
            OnFilesLoaded: function(bFilesListSuccess, bFilesSuccess)
            {
                // delete callback
                this.delete();

                // remove completed list from the array
                let path = aLoadFilesLists.shift();

                // in case of map layer: create it and add to array of created layers
                switch(path.type)
                {
                    case MapCore.IMcNativeRasterMapLayer.LAYER_TYPE:
                        aLastTerrainLayers.push(MapCore.IMcNativeRasterMapLayer.Create("FS/Dynamic/" + path.dir));
                        break;
                    case MapCore.IMcNativeDtmMapLayer.LAYER_TYPE:
                        aLastTerrainLayers.push(MapCore.IMcNativeDtmMapLayer.Create("FS/Dynamic/" + path.dir));
                        break;
                    case MapCore.IMcNativeVectorMapLayer.LAYER_TYPE:
                        aLastTerrainLayers.push(MapCore.IMcNativeVectorMapLayer.Create("FS/Dynamic/" + path.dir));
                        break;
                    case MapCore.IMcNative3DModelMapLayer.LAYER_TYPE:
                    {
                        let layer = MapCore.IMcNative3DModelMapLayer.Create("FS/Dynamic/" + path.dir);
                        pLayer.SetDisplayingItemsAttachedToTerrain(true);
                        pLayer.SetDisplayingDtmVisualization(true);
                        aLastTerrainLayers.push(layer);
                        break;
                    }
                    case MapCore.IMcRaw3DModelMapLayer.LAYER_TYPE:
                    {
                        let layer = MapCore.IMcRaw3DModelMapLayer.Create("FS/Dynamic/" + path.dir);
                        pLayer.SetDisplayingItemsAttachedToTerrain(true);
                        pLayer.SetDisplayingDtmVisualization(true);
                        aLastTerrainLayers.push(layer);
                        break;
                    }
                    case MapCore.IMcNativeVector3DExtrusionMapLayer.LAYER_TYPE:
                        aLastTerrainLayers.push(MapCore.IMcNativeVector3DExtrusionMapLayer.Create("FS/Dynamic/" + path.dir));
                        break;
                    case MapCore.IMcRawVector3DExtrusionMapLayer.LAYER_TYPE:
                        let Params = new MapCore.IMcRawVector3DExtrusionMapLayer.SGraphicalParams();
                        Params.strHeightColumn = "height";
                        aLastTerrainLayers.push(MapCore.IMcRawVector3DExtrusionMapLayer.Create("FS/Dynamic/" + path.dir, Params));
                        break;
                    case MapCore.IMcRawRasterMapLayer.LAYER_TYPE:
                    {
                        let component = new MapCore.IMcMapLayer.SComponentParams;
                        component.eType = MapCore.IMcMapLayer.EComponentType.ECT_DIRECTORY;
                        component.strName = "FS/Dynamic/" + path.dir;
                        component.WorldLimit = MapCore.SMcBox(MapCore.DBL_MAX, MapCore.DBL_MAX, MapCore.DBL_MAX, MapCore.DBL_MAX, MapCore.DBL_MAX, MapCore.DBL_MAX);

                        let rawParams = new MapCore.IMcMapLayer.SRawParams;
                        rawParams.pCoordinateSystem = lastCoordSys;
                        rawParams.strDirectory = "";
                        rawParams.aComponents = [component];
                        aLastTerrainLayers.push(MapCore.IMcRawRasterMapLayer.Create(rawParams));
                        break;
                    }
                    case MapCore.IMcRawDtmMapLayer.LAYER_TYPE:
                    {
                        let component = new MapCore.IMcMapLayer.SComponentParams;
                        component.eType = MapCore.IMcMapLayer.EComponentType.ECT_DIRECTORY;
                        component.strName = "";
                        
                        let rawParams = new MapCore.IMcMapLayer.SRawParams;
                        rawParams.pCoordinateSystem = path.coordSystem;
                        rawParams.strDirectory = "FS/Dynamic/" + path.dir;
                        rawParams.aComponents = [component];
                        aLastTerrainLayers.push(MapCore.IMcRawDtmMapLayer.Create(rawParams));
                        break;
                    }
                }
                
                // continue loading lists of files
                DeprecatedLoadFilesList();
            },
        });

        aLastTerrainLayers = [];

        // fill array of list of files with map layers
        let group = mapLayerGroups.get(lastTerrainConfiguration);
        lastCoordSys = group.pCoordSystem;
        for (let i = 0; i < group.aLayerCreateStrings.length; ++i)
        {
            let pathOrLayer = eval(group.aLayerCreateStrings[i]);
            if (pathOrLayer instanceof CDeprecatedPath)
            {
                aLoadFilesLists.push(pathOrLayer);
            }
            else
            {
                if (pathOrLayer instanceof MapCore.IMc3DModelMapLayer)
                {
                    pathOrLayer.SetDisplayingItemsAttachedToTerrain(true);
                    pathOrLayer.SetDisplayingDtmVisualization(true);
                }
                aLastTerrainLayers.push(pathOrLayer);
            }
        }
    }

    // load lists of files
    DeprecatedLoadFilesList();
}

// function loading lists of files
function DeprecatedLoadFilesList()
{
    if (aLoadFilesLists.length != 0)
    {
        // load first list of files
        let path = aLoadFilesLists[0];
        MapCore.IMcMapDevice.LoadFilesListAsync(path.url, path.list, new CDeprecatedLoadFilesCallback);
    }
    else
    {
        // create terrain and viewport, start rendering
        InitializeViewports();
        TrySetTerainBox();
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////
// TESTING FUNCTIONS

function TestObjectAPI(pObject)
{
    // testing user data
    let CUserData = MapCore.IMcUserData.extend("IMcUserData", 
    {
        // optional
        __construct: function(bToBeDeleted)
        {
            this.__parent.__construct.call(this);
            this.bToBeDeleted = bToBeDeleted;
            // ...
        },

        // optional
        __destruct: function()
        {
            this.__parent.__destruct.call(this);
            // ...
        },

        // mandatory
        Release: function()
        {
            if (this.bToBeDeleted)
            {
                this.delete();
            }
        },

        // optional
        Clone: function()
        {
            if (this.bToBeDeleted)
            {
                return new CUserData(this.bToBeDeleted);
            }
            return this;
        },

        GetSaveBufferSize: function()
        {
            return 1;
        },

        SaveToBuffer: function(aBuffer)
        {
            aBuffer[0] = 123;
        }

    });

    let userData1 = new CUserData(true);
    pObject.SetUserData(userData1);
    let userData2 = pObject.GetUserData();
    let pObject1 = pObject.Clone(overlay, false);
    userData2 = pObject1.GetUserData();
    pObject.SetUserData(null);
    pObject1.SetUserData(null);
    let userData3 = new CUserData(false);
    pObject.SetUserData(userData3);
    let userData4 = pObject.GetUserData();
    let pObject2 = pObject.Clone(overlay, false);
    userData4 = pObject2.GetUserData();
    pObject.SetUserData(null);
    pObject2.SetUserData(null);
    userData4.delete();

    // testing object properties
    let t = pObject.GetProperties();
    t[0].Value = new MapCore.SMcVariantString("1", false);
    t[1].Value = false;
    t[2].Value = null;
    pObject.SetProperties(t);
    let t0 = pObject.GetProperties();
    let prop = new MapCore.IMcProperty.SVariantProperty();
    prop.eType = MapCore.IMcProperty.EPropertyType.EPT_BOOL;
    prop.uID = 2;
    prop.Value = false;
    pObject.SetProperty(prop);
    let aProperties = [];
    let aPoints = [];
    pObject.GetPropertiesAndLocationPoints(aProperties, aPoints);
    aProperties[0].Value = new MapCore.SMcVariantString("2", false);
    pObject.UpdatePropertiesAndLocationPoints(aProperties, aPoints);
    t = pObject.GetProperties();
    aProperties[0].Value = new MapCore.SMcVariantString("3", false);
    MapCore.IMcObject.SetEachObjectProperty([pObject], [aProperties[0]]);
    t = pObject.GetProperties();

    let str;
    pObject.SetStringProperty(1, new MapCore.SMcVariantString(["1", "2"], false));
    str = pObject.GetStringProperty(1);
    pObject.SetStringProperty(1, new MapCore.SMcVariantString(["1"], false));
    str = pObject.GetStringProperty(1);
    pObject.SetStringProperty(1, new MapCore.SMcVariantString("1", false));
    str = pObject.GetStringProperty(1);
    // testing font creation
    let CharactersRange = MapCore.IMcFont.SCharactersRange();

    let font = MapCore.IMcFileFont.Create(new MapCore.SMcFileSource("FS/Resources/Default.ttf", false), 12, true);
    pObject.SetFontProperty(3, font);
    let font1 = pObject.GetFontProperty(3);
}

function TestingText(pItem)
{
    let text = MapCore.SMcVariantString("Hellllllo!", true);
    pItem.SetText(text);
    let color = MapCore.SMcBColor(255,174,201,255);
    pItem.SetBackgroundColor(color); 
}

function TestingIMcObjectSchemeOverloading(pItem)
{
    let pNewScheme = MapCore.IMcObjectScheme.Create(overlayManager, pItem, MapCore.EMcPointCoordSystem.EPCS_WORLD);
    let pObject = MapCore.IMcObject.Create(overlay, pNewScheme);
}  

function TesingIMcGeometricCalculationsEG2DCirclesUnion()
{
    let point1 = new MapCore.SMcVector3D(686781.5, 3482110, 0);
    let point2 = new MapCore.SMcVector3D(685131.5, 3481885, 0);
    let circle1 = new MapCore.STCircle();
    circle1.stCenter = point1;
    circle1.dRadius = 2000;
    let circle2 = new MapCore.STCircle();
    circle2.stCenter = point2;
    circle2.dRadius = 2000;
    let arcs = {};
    let shapes = {};
    let circles = [circle1, circle2];
    MapCore.IMcGeometricCalculations.EG2DCirclesUnion(circles, true, arcs, shapes);
}

function TestingSetterAndGetterImageFile()
{
    let texture = MapCore.IMcImageFileTexture.Create(MapCore.SMcFileSource("http:Images/Arrow-Left.png", false), false);
    texture.SetImageFile(texture);
    let t = texture.GetImageFile();
    let buffer = new Uint8Array(); // fill by image file contents
    let texture1 = MapCore.IMcImageFileTexture.Create(MapCore.SMcFileSource(buffer, true), true);
    texture1.SetImageFile(texture1);
    let t1 = texture1.GetImageFile();
}

function TestingSubItemPropertyScheme(pObject)
{
    let SubItemProperty = pObject.GetScheme();
    let s1 = new MapCore.SMcSubItemData(1, 0);
    let s2 = new MapCore.SMcSubItemData(2, 2);
    let ArrayProp = new MapCore.IMcProperty.SArrayPropertySMcSubItemData([s1, s2]);
    let prop = new MapCore.IMcProperty.SVariantProperty();
    prop.eType = MapCore.IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY;
    prop.uID = 10;
    prop.Value = ArrayProp;
    //pObject.SetProperty(prop);
    SubItemProperty = pObject.GetScheme();
    pObject.SetArraySMcSubItemDataProperty(prop.uID, prop.eType, ArrayProp);
    let testArrayProp = pObject.GetArraySMcSubItemDataProperty(prop.uID, prop.eType);
}

function TestingSubItemProperty(pObject)
{
    let SubItemProperty = pObject.GetProperty(10);
    let s1 = new MapCore.SMcSubItemData(1, 0);
    let s2 = new MapCore.SMcSubItemData(2, 2);
    let ArrayProp = new MapCore.IMcProperty.SArrayPropertySMcSubItemData([s1, s2]);
    let prop = new MapCore.IMcProperty.SVariantProperty();
    prop.eType = MapCore.IMcProperty.EPropertyType.EPT_SUBITEM_ARRAY;
    prop.uID = 10;
    prop.Value = ArrayProp;
    //pObject.SetProperty(prop);
    SubItemProperty = pObject.GetProperty(10);
    pObject.SetArraySMcSubItemDataProperty(prop.uID, prop.eType, ArrayProp);
    let testArrayProp = pObject.GetArraySMcSubItemDataProperty(prop.uID, prop.eType);
}

function TestingSetterAndGetterVisibilityOption(pObject)
{
    pObject.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_FALSE);
    pObject.SetVisibilityOption(MapCore.IMcConditionalSelector.EActionOptions.EAO_FORCE_FALSE, viewport);
    let aPoints = ObjectItemData.pObject.GetLocationPoints();
    let eCrossResult = new MapCore.PL_PL_STATUSRef;
    let ePolygonStatus = new MapCore.PG_PG_STATUSRef;
    let aResults = MapCore.IMcGeometricCalculations.EGPolyLinesRelation(aPoints, aPoints, eCrossResult, 2);
    aResults = MapCore.IMcGeometricCalculations.EG2DPolyGonsRelation(aPoints, aPoints, eCrossResult, ePolygonStatus);
}
        
function TestingAreaOfSightCalc(scouter)
{
    scouter.z = 1.7;
    let aVisibilityColors = [];
    for (let i = 0; i < MapCore.IMcSpatialQueries.EPointVisibility.EPV_NUM.value; ++i)
    {
        aVisibilityColors.push(new MapCore.SMcBColor(0, 0, 0, 0));
    }
    let areaOfSight = {}, linesOfSight = [], seenPolygons = {}, unseenPolygons = {}, staticObjects = [];
    let params = new MapCore.IMcSpatialQueries.SQueryParams();
    params.eTerrainPrecision = MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGH;
    params.pAsyncQueryCallback = new CAsyncQueryCallback(
        (pAreaOfSight, aLinesOfSight, pSeenPolygons, pUnseenPolygons, aSeenStaticObjects) =>
        {
            // ...
        }
    );
    viewport.GetEllipseAreaOfSight(scouter, false, 1.7, false, 1, 500, 500, 0, 360, 0, 64, aVisibilityColors, 
        areaOfSight, linesOfSight, seenPolygons, unseenPolygons, staticObjects, 90, -90, params, false);
}

////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTIONS FOR MAPCORE DEVELOPERS

async function TestLoadingFSDynamically()
{
    const promise = new Promise((resolve, reject) => 
    {
        let CLoadFilesCallback = MapCore.IMcMapDevice.ICallback.extend("IMcMapDevice.ICallback",
        {
            // mandatory
            OnFilesLoaded: function(bFilesListSuccess, bFilesSuccess)
            {
                // delete callback
                this.delete();

                if (!bFilesListSuccess)
                {
                    reject(new Error("FS.txt cannot be loaded"));
                }
                else if (!bFilesSuccess)
                {
                    reject(new Error("Some of the files from FS.txt cannot be loaded"));
                }
                else
                {
                    resolve();
                }
            },
        });
        MapCore.IMcMapDevice.LoadFilesListAsync("FS-Dynamic", "FS.txt", new CLoadFilesCallback);
    });
    return promise;
}

function TestSymbologyStandardSupport()
{
     overlayManager.InitializeSymbologyStandardSupport(
        MapCore.IMcObject.ESymbologyStandard.ESS_APP6D, true/*ShowGeoInMetricProportion*/);

    //overlayManager.SetScaleFactor(1.0); // to call internal debug code...
    //return;

    //let sSIDC = "XXXX25XXXX290500XXXXXXXXXXXXXX";
    let sSIDC = "XXXX25XXXX200202XXXXXXXXXXXXXX";

    let aGeometricAmplifiersNames = [];
    let aGraphicalAmplifiersNames = [];
    overlayManager.GetSymbologyStandardNames(
        MapCore.IMcObject.ESymbologyStandard.ESS_APP6D, sSIDC, aGeometricAmplifiersNames, aGraphicalAmplifiersNames);

    let aGraphicalAmplifiers = [];
    let pObject1 = MapCore.IMcObject.CreatePointlessFromSymbology(
        overlay, MapCore.IMcObject.ESymbologyStandard.ESS_APP6D, sSIDC, aGraphicalAmplifiers, false/*bIsFlipped*/);
    let aPoints = [];
    aPoints[0] = aViewports[0].terrainCenter;
    pObject1.SetLocationPoints(aPoints);

    // for "XXXX25XXXX290500XXXXXXXXXXXXXX", comment out these lines:
    //pObject1.SetFloatProperty(2011043, 250); // RadiusX
    //pObject1.SetFloatProperty(2011044, 500); // RadiusY
    //pObject1.SetFloatProperty(2011045, 25); // RotationYaw

    let aAnchorPoints = [];
    let aGeometricAmplifiers = [];
    pObject1.GetSymbologyAnchorPointsAndGeometricAmplifiers(aAnchorPoints, aGeometricAmplifiers);
    let sSIDC2 = {}; 
    pObject1.GetSymbologyGraphicalProperties(sSIDC2, aGraphicalAmplifiers);
    pObject1.MoveAllLocationsPoints(MapCore.SMcVector3D(0, 3000, 0));

    let pObject2 = MapCore.IMcObject.CreateFromSymbology(
		overlay, MapCore.IMcObject.ESymbologyStandard.ESS_APP6D, sSIDC,
        aAnchorPoints, aGeometricAmplifiers, aGraphicalAmplifiers);
    pObject2.MoveAllLocationsPoints(MapCore.SMcVector3D(0, 1500, 0));

    let pObject3 = MapCore.IMcObject.CreatePointlessFromSymbology(
        overlay, MapCore.IMcObject.ESymbologyStandard.ESS_APP6D, sSIDC, aGraphicalAmplifiers, false/*bIsFlipped*/);
    pObject3.SetSymbologyAnchorPointsAndGeometricAmplifiers(aAnchorPoints, aGeometricAmplifiers);
    pObject3.SetSymbologyGraphicalProperties(sSIDC2.Value, aGraphicalAmplifiers);
}

function TestSaveObjectsAsRawVectorData(pObject)
{
    let CAsyncOperationCallback = MapCore.IMcOverlayManager.IAsyncOperationCallback.extend("IMcOverlayManager.IAsyncOperationCallback",
    {
        OnSaveObjectsAsRawVectorToFileResult: function(strFileName, eStatus, aAdditionalFiles)
        {
            MapCore.IMcMapDevice.DownloadFileSystemFile(strFileName);
            for (let strAdditionalFile of aAdditionalFiles)
            {
                MapCore.IMcMapDevice.DownloadFileSystemFile(strAdditionalFile);
            }
        },

        OnSaveObjectsAsRawVectorToBufferResult: function(strFileName, eStatus, auFileMemoryBuffer, aAdditionalFiles)
        {
            MapCore.IMcMapDevice.DownloadBufferAsFile(auFileMemoryBuffer, strFileName);
            for (let additionalFile of aAdditionalFiles)
            {
                MapCore.IMcMapDevice.DownloadBufferAsFile(additionalFile.auMemoryBuffer, additionalFile.strFileName);
            }
        }

    });

    let fCameraYawAngle = 0.0;
    let fCameraScale = viewport.GetCameraScale();
    let strLayerName = "test";
    let strFileName;
    let aAdditionalFiles;
    let auFileMemoryBuffer; 
    let asyncOperationCallback;
    let apObjects;

    if(!pObject)
    {
        strFileName = "./AllObjectsFile.kmz";
        aAdditionalFiles = [];
        overlay.SaveAllObjectsAsRawVectorDataToFile(viewport, fCameraYawAngle, fCameraScale, strLayerName, strFileName, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadFileSystemFile(strFileName)

        strFileName = "./AllObjectsFile.kml";
        aAdditionalFiles = [];
        overlay.SaveAllObjectsAsRawVectorDataToFile(viewport, fCameraYawAngle, fCameraScale, strLayerName, strFileName, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadFileSystemFile(strFileName)
        for (let strAdditionalFile of aAdditionalFiles)
        {
            MapCore.IMcMapDevice.DownloadFileSystemFile(strAdditionalFile);
        }

        strFileName = "AllObjectsBuff.kmz";
        auFileMemoryBuffer = {}; 
        aAdditionalFiles = [];
        overlay.SaveAllObjectsAsRawVectorData(viewport,fCameraYawAngle, fCameraScale, strLayerName, strFileName, auFileMemoryBuffer, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadBufferAsFile(auFileMemoryBuffer.Value, strFileName);

        strFileName = "AllObjectsBuff.kml";
        auFileMemoryBuffer = {}; 
        aAdditionalFiles = [];
        overlay.SaveAllObjectsAsRawVectorData(viewport,fCameraYawAngle, fCameraScale, strLayerName, strFileName, auFileMemoryBuffer, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadBufferAsFile(auFileMemoryBuffer.Value, strFileName);
        for (let additionalFile of aAdditionalFiles)
        {
            MapCore.IMcMapDevice.DownloadBufferAsFile(additionalFile.auMemoryBuffer, additionalFile.strFileName);
        }

        // test async save
        strFileName = "./AsyncAllObjectsFile.kml";
        aAdditionalFiles = [];
        asyncOperationCallback = new CAsyncOperationCallback();
        overlay.SaveAllObjectsAsRawVectorDataToFile(viewport, fCameraYawAngle, fCameraScale, strLayerName, strFileName, aAdditionalFiles, asyncOperationCallback);

        strFileName = "AsyncAllObjectsBuff.kml";
        auFileMemoryBuffer = {}; 
        aAdditionalFiles = [];
        asyncOperationCallback = new CAsyncOperationCallback();
        overlay.SaveAllObjectsAsRawVectorData(viewport,fCameraYawAngle, fCameraScale, strLayerName, strFileName, auFileMemoryBuffer, aAdditionalFiles, asyncOperationCallback);
    }
    else
    {
        apObjects = [pObject];
        strFileName = "./File.kmz";
        aAdditionalFiles = [];
        overlay.SaveObjectsAsRawVectorDataToFile(apObjects, viewport, fCameraYawAngle, fCameraScale, strLayerName, strFileName, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadFileSystemFile(strFileName)

        strFileName = "./File.kml";
        aAdditionalFiles = [];
        overlay.SaveObjectsAsRawVectorDataToFile(apObjects, viewport, fCameraYawAngle, fCameraScale, strLayerName, strFileName, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadFileSystemFile(strFileName)
        for (let strAdditionalFile of aAdditionalFiles)
        {
            MapCore.IMcMapDevice.DownloadFileSystemFile(strAdditionalFile);
        }
        
        strFileName = "Buff.kmz";
        auFileMemoryBuffer = {}; 
        aAdditionalFiles = [];
        overlay.SaveObjectsAsRawVectorData(apObjects, viewport,fCameraYawAngle, fCameraScale, strLayerName, strFileName, auFileMemoryBuffer, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadBufferAsFile(auFileMemoryBuffer.Value, strFileName);

        strFileName = "Buff.kml";
        auFileMemoryBuffer = {}; 
        aAdditionalFiles = [];
        overlay.SaveObjectsAsRawVectorData(apObjects, viewport,fCameraYawAngle, fCameraScale, strLayerName, strFileName, auFileMemoryBuffer, aAdditionalFiles);
        MapCore.IMcMapDevice.DownloadBufferAsFile(auFileMemoryBuffer.Value, strFileName);
        for (let additionalFile of aAdditionalFiles)
        {
            MapCore.IMcMapDevice.DownloadBufferAsFile(additionalFile.auMemoryBuffer, additionalFile.strFileName);
        }

        // test async save
        strFileName = "./AsyncFile.kml";
        aAdditionalFiles = [];
        asyncOperationCallback = new CAsyncOperationCallback();
        overlay.SaveObjectsAsRawVectorDataToFile(apObjects, viewport, fCameraYawAngle, fCameraScale, strLayerName, strFileName, aAdditionalFiles, asyncOperationCallback);

        strFileName = "AsyncBuff.kml";
        auFileMemoryBuffer = {}; 
        aAdditionalFiles = [];
        asyncOperationCallback = new CAsyncOperationCallback();
        overlay.SaveObjectsAsRawVectorData(apObjects, viewport,fCameraYawAngle, fCameraScale, strLayerName, strFileName, auFileMemoryBuffer, aAdditionalFiles, asyncOperationCallback);
    
    }
}

let nPrintType = 0;

function TestPrintToRawRasterData()
{
    let strDataFormat = "pdf";
    let astrOptions = ["COMPRESS=JPEG", "JPEG_QUALITY=20"];
    let strFileName;

    let printCallback = new CPrintCallback(
        (eStatus, strFileNameOrRasterDataFormat, auFileMemoryBuffer, auWorldFileMemoryBuffer) =>
        {
            if (eStatus == MapCore.IMcErrors.ECode.SUCCESS)
            {
                if (auFileMemoryBuffer.length != 0)
                {
                    MapCore.IMcMapDevice.DownloadBufferAsFile(auFileMemoryBuffer, strFileName + strFileNameOrRasterDataFormat);
                    if (auWorldFileMemoryBuffer.length != 0)
                    {
                        MapCore.IMcMapDevice.DownloadBufferAsFile(auWorldFileMemoryBuffer, strFileName + "wld");
                    }
                }
                else
                {
                    MapCore.IMcMapDevice.DownloadFileSystemFile(strFileNameOrRasterDataFormat);
                    MapCore.IMcMapDevice.DownloadFileSystemFile(strFileName + "wld");
                }
            }
            else
            {
                alert(MapCore.IMcErrors.ErrorCodeToString(eStatus));
            }
        });

    let worldVisibleArea;
    let fPrintScale;
    if (viewport.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D)
    {
        worldVisibleArea = viewport.GetCameraWorldVisibleArea();
        fPrintScale = viewport.GetCameraScale();
    }
    else // 3D
    {
        let Footprint = viewport.GetCameraFootprint();
        if (!Footprint.bUpperLeftFound || !Footprint.bUpperRightFound || !Footprint.bLowerRightFound || !Footprint.bLowerLeftFound || !Footprint.bCenterFound)
        {
            alert("Camera footprint cannot be calculated");
            return;
        }
        worldVisibleArea = new MapCore.SMcBox();
        worldVisibleArea.MinVertex.x = Math.min(Footprint.UpperLeft.x, Footprint.UpperRight.x, Footprint.LowerRight.x, Footprint.LowerLeft.x);
        worldVisibleArea.MinVertex.y = Math.min(Footprint.UpperLeft.y, Footprint.UpperRight.y, Footprint.LowerRight.y, Footprint.LowerLeft.y);
        worldVisibleArea.MaxVertex.x = Math.max(Footprint.UpperLeft.x, Footprint.UpperRight.x, Footprint.LowerRight.x, Footprint.LowerLeft.x);
        worldVisibleArea.MaxVertex.y = Math.max(Footprint.UpperLeft.y, Footprint.UpperRight.y, Footprint.LowerRight.y, Footprint.LowerLeft.y);
        fPrintScale = viewport.GetCameraScale(Footprint.Center);
    }

    let PrintWorldRectSize = MapCore.SMcVector2D(MapCore.SMcBox.SizeX(worldVisibleArea), MapCore.SMcBox.SizeY(worldVisibleArea));
    let centerPoint3D = MapCore.SMcBox.CenterPoint(worldVisibleArea);
    let PrintWorldRectCenter = MapCore.SMcVector2D(centerPoint3D.x, centerPoint3D.y);

    switch (nPrintType)
    {
        case 0:
            strFileName = "PrintScreen.";
            viewport.PrintScreenToRawRasterData(1, strFileName + strDataFormat, null, null, printCallback, astrOptions);
            break;
        case 1:
            strFileName = "PrintScreenBuffer.";
            viewport.PrintScreenToRawRasterData(1, strDataFormat, {}, {}, printCallback, astrOptions);
            break;
        case 2:
            strFileName = "PrintRect2D.";
            viewport.PrintRect2DToRawRasterData(PrintWorldRectCenter, PrintWorldRectSize, 0, fPrintScale, 1, strFileName + strDataFormat,  null, null, printCallback, astrOptions);
            break;
        case 3:
            strFileName = "PrintRect2DBuffer.";
            viewport.PrintRect2DToRawRasterData(PrintWorldRectCenter, PrintWorldRectSize, 0, fPrintScale, 1, strDataFormat, {}, {}, printCallback, astrOptions);
            break;
    }
    nPrintType = (nPrintType + 1) % 4;
}

function TestSaveToBuffer(pObject)
{
    aObjects = [pObject];
    overlay.SaveObjects(aObjects)
}


function TestVectorItemCallbacks(Target, ScanPointGeometry)
{
    // Test IMcMapLayer::IAsyncOperationCallback::OnVectorItemPointsResult()
    let pOnVectorItemPointsResultCallback = new CAsyncOperationCallback(
        (pLayer, eStatus, aaPoints) =>
        {
            let i = 0;
            i = 5;
        });
    Target.pTerrainLayer.GetVectorItemPoints(MapCore.SMcVariantID.Get53Bit(Target.uTargetID), pOnVectorItemPointsResultCallback);

    // Test IMcMapLayer::IAsyncOperationCallback::OnScanExtendedDataResult()
    let pOnScanExtendedDataResultCallback = new CAsyncOperationCallback(
        (pLayer, eStatus, aVectorItems, aUnifiedVectorItemsPoints) =>
        {
            let i = 0;
            i = 5;
        });

    Target.pTerrainLayer.GetScanExtendedData(ScanPointGeometry, Target, viewport, {}, {}, pOnScanExtendedDataResultCallback);

    // Test IMcMapLayer::IAsyncOperationCallback::OnFieldUniqueValuesResult()
    let pOnFieldUniqueValuesResult = new CAsyncOperationCallback(
        (pLayer, eStatus, eReturnedType, pValue) =>
        {
            let i = 0;
            i = 5;
        });

    Target.pTerrainLayer.GetFieldUniqueValuesAsWString(4, pOnFieldUniqueValuesResult)
    // Test IMcMapLayer::IAsyncOperationCallback::OnVectorItemFieldValueResult()
    let pOnVectorItemFieldValueResultCallback = new CAsyncOperationCallback(
        (pLayer, eStatus, eReturnedType, pValue) =>
        {
            let i = 0;
            i = 5;
        });

    Target.pTerrainLayer.GetVectorItemFieldValueAsDouble(MapCore.SMcVariantID.Get53Bit(Target.uTargetID), 0, pOnVectorItemFieldValueResultCallback)

    // Test IMcMapLayer::IAsyncOperationCallback::OnVectorQueryResult()
    let pOnVectorQueryResultCallback = new CAsyncOperationCallback(
        (pLayer, eStatus, auVectorItemsID) =>
        {
            let i = 0;
            i = 5;
        });

    let strAttributeFilter = "aaa";
    Target.pTerrainLayer.Query(strAttributeFilter, pOnVectorQueryResultCallback);
}
