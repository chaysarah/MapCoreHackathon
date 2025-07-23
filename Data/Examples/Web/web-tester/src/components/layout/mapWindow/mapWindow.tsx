import React, { useState, useEffect, useRef } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { DraggableData, ResizableDelta, Rnd } from "react-rnd";
import { ResizeDirection } from "re-resizable";
import { MenuItemCommandEvent } from 'primereact/menuitem';
import { DraggableEvent } from 'react-draggable';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faThumbtack } from '@fortawesome/free-solid-svg-icons'
import { faWindowMaximize } from '@fortawesome/free-solid-svg-icons'
import { faWindowRestore } from '@fortawesome/free-solid-svg-icons'

import {
    OpenMapService, ViewportService, MapCoreData, ScanService, ViewportData, ViewportWindow, StandardViewportWindow,
    JsonViewportWindow, DetailedViewportWindow, DetailedSectionMapViewportWindow, ViewportWindowType, OppositeDimensionViewportWindow,
    MapCoreService
} from 'mapcore-lib';
import './styles/mapWindow.css';
import TabMenuModel from '../../shared/models/tab-menu.model';
import { hideFormReasons } from '../../shared/models/tree-node.model';
import Tabmenu from '../../shared/tabs/tabmenu';
import { AppState } from '../../../redux/combineReducer';
import { closeViewport, setActiveCard, setCreateText, setCurrentViewport, setCursorPos, setCursorRgb, setMapScaleBox, setScaleBox, setScreenPos } from '../../../redux/mapWindow/mapWindowAction';
import { selectViewportPosition } from '../../../redux/mapWindow/mapWindowReducer';
import { closeDialog, setDialogType, setmaximaizeWindow } from '../../../redux/MapCore/mapCoreAction';
import addJsonViewportService from '../../../services/addJsonViewport.service';
import { DialogTypesEnum } from '../../../tools/enum/enums';
import generalService from '../../../services/general.service';
import { runAsyncCodeSafely, runCodeSafely } from '../../../common/services/error-handling/errorHandler';
import { setShowDialog } from '../../../redux/ObjectWorldTree/objectWorldTreeActions';
import { Id } from '../../../tools/enum/enumsOfScheme';
import symbolicItemsService from '../../../services/symbolicItems.service';
import store from '../../../redux/store';
import EditModeNavigation from '../../../services/editModeNavigation.service';


export default function MapWindow(props: { viewport: ViewportWindow, index: number }) {
    const dispatch = useDispatch();
    const canvasRef = useRef(null);
    const inputText = useRef(null);
    const showDialog: { hideFormReason: hideFormReasons, dialogType: DialogTypesEnum } = useSelector((state: AppState) => state.objectWorldTreeReducer.showDialog);
    const viewportPosition = useSelector((state: AppState) => selectViewportPosition(state, props.viewport.id));
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const sizeviewPorts = useSelector((state: AppState) => state.mapWindowReducer.size);
    const newGeneratedObject = useSelector((state: AppState) => state.mapWindowReducer.newGeneratedObject);
    const activeCard = useSelector((state: AppState) => state.mapWindowReducer.activeCard);
    const maximaizeWindow = useSelector((state: AppState) => state.mapCoreReducer.maximaizeWindow);
    const [currentViewportSize, setCurrentViewportSize] = useState(sizeviewPorts);
    const [viewportSizeOnStartResize, setViewportSizeOnStartResize] = useState({ width: 0, height: 0 });
    const [currentViewportPosition, setCurrentViewportPosition] = useState(viewportPosition);
    const [lastCanvasWidth, setLastCanvasWidth] = useState<number>(0);
    const [lastCanvasHeight, setLastCanvasHeight] = useState<number>(0);
    const [lastCanvasSizeOnStartResize, setLastCanvasSizeOnStartResize] = useState({ width: 0, height: 0 });
    const [FlagPinMapMenu, setFlagPinMapMenu] = useState(true);
    const [flagOpenNav, setFlagOpenNav] = useState<boolean>(true);
    const [allowMoveMap, setAllowMoveMap] = useState<boolean>(false);
    const [activeItem, setActiveItem] = useState<number>(null);
    const [newOpenMapService, setNewOpenMapService] = useState(null);
    const createText = useSelector((state: AppState) => state.mapWindowReducer.createText);
    const dialogTypesArr: DialogTypesEnum[] = useSelector((state: AppState) => state.mapCoreReducer.dialogTypesArr);
    const num = useRef<number>(0);

    useEffect(() => {
        num.current = dialogTypesArr.length
    }, [dialogTypesArr])

    let editModeNavigationService = new EditModeNavigation();

    const grids = [
        {
            label: 'UTM Grid', icon: activeItem == 0 ? 'pi pi-check-square' : 'pi pi-stop', command: (e: MenuItemCommandEvent) => { setGridType(e, 0) },
        },
        {
            label: 'Geo Grid', icon: activeItem == 1 ? 'pi pi-check-square' : 'pi pi-stop', command: (e: MenuItemCommandEvent) => { setGridType(e, 1) },
        },
        {
            label: 'MGRS Grid', icon: activeItem == 2 ? 'pi pi-check-square' : 'pi pi-stop', command: (e: MenuItemCommandEvent) => { setGridType(e, 2) },
        },
        {
            label: 'GEOREF Grid', icon: activeItem == 3 ? 'pi pi-check-square' : 'pi pi-stop', command: (e: MenuItemCommandEvent) => { setGridType(e, 3) },
        }
    ]

    const setGridType = (e: MenuItemCommandEvent, index: number) => {
        runCodeSafely(() => {
            { activeItem == index ? setActiveItem(null) : setActiveItem(index) }
            ViewportService.doCreateGrid(props.viewport.id, e.item.label);
        }, "mapWindow.setGrid");
    }

    const onKeyMap = (e: any) => {
        if (num.current == 0)
            runCodeSafely(() => {
                if (activeCard == props.viewport.id)
                    ViewportService.KeyDownOnMap(e.code, e.shiftKey, e.ctrlKey, props.viewport.id, generalService.EditModePropertiesBase.OneOperationOnly)
            }, "mapWindow.KeyDownOnMap");
    }
    const KeyUpOnMap = (e: React.KeyboardEvent<HTMLDivElement>) => {
        if (num.current == 0)
            runCodeSafely(() => {
                ViewportService.KeyUpOnMap(e.code, props.viewport.id, generalService.EditModePropertiesBase.OneOperationOnly)
            }, "mapWindow.KeyUpOnMap");
    }

    // #region create map
    const [hiddenViewport, setHiddenViewport] = useState<boolean>(true);
    const [activateUseEffect2, setActivateUseEffect2] = useState<boolean>(false);
    const errorFlag = useRef<boolean>(false);

    useEffect(() => {
        const localOpenMapService = new OpenMapService();

        runAsyncCodeSafely(async () => {
            if (!MapCoreData.device)
                MapCoreService.initDevice(generalService.mapCorePropertiesBase.deviceInitParams);
            resetCurrentViewpSize()
            generalService.mapCorePropertiesBase.SCreateData.hWnd = canvasRef.current;
            generalService.mapCorePropertiesBase.SCreateData.pDevice = MapCoreData.device;
            switch (props.viewport.type) {
                case ViewportWindowType.fromUI:
                    const standardVp = props.viewport as StandardViewportWindow;
                    localOpenMapService.addNewViewportStandard(standardVp.id, standardVp.layerslist, canvasRef.current!, standardVp.ViewportParams, generalService.calcSizeAndPositionCanvases);
                    break;
                case ViewportWindowType.fromJson:
                    const jsonVp = props.viewport as JsonViewportWindow;
                    await addJsonViewportService.addNewViewportFromJson(jsonVp, canvasRef.current!);
                    break;
                case ViewportWindowType.detailedWindow:
                    const detailedVp = props.viewport as DetailedViewportWindow;
                    localOpenMapService.addNewViewportDetailed(detailedVp, generalService.mapCorePropertiesBase.SCreateData, generalService.calcSizeAndPositionCanvases)
                    break;
                case ViewportWindowType.detailedSectionMapViewport:
                    const detailedSectionMapVp = props.viewport as DetailedSectionMapViewportWindow;
                    localOpenMapService.addNewViewportDetailed(detailedSectionMapVp, generalService.mapCorePropertiesBase.SCreateData, generalService.calcSizeAndPositionCanvases)
                    break;
                case ViewportWindowType.oppositeDimensionViewport:
                    const oppositeDimensionVp = props.viewport as OppositeDimensionViewportWindow;
                    localOpenMapService.addNewViewportOppositeDimention(oppositeDimensionVp, canvasRef.current, generalService.calcSizeAndPositionCanvases)
            }
            setActivateUseEffect2(true)
            if (errorFlag.current == false && !props.viewport.waitForTheLayersToInitialize) {
                dispatch(closeDialog(DialogTypesEnum.chooseLayers))
                dispatch(closeDialog(DialogTypesEnum.addMapManuly))
                // dispatch(closeDialog(DialogTypesEnum.addJsonViewport))
            }
        }, 'MapWindow.useEffect', () => {
            closethisViewport();
            errorFlag.current = true;
        });

        setNewOpenMapService(localOpenMapService);
    }, [])

    useEffect(() => {
        if (!activateUseEffect2)
            return;

        runCodeSafely(() => {
            let interval = setInterval(() => {
                runCodeSafely(() => {
                    let viewportData: ViewportData = MapCoreData.findViewport(props.viewport.id);
                    if (!viewportData || !newOpenMapService.areAllLayersInitialized())
                        return;
                    clearInterval(interval)

                    if (props.viewport.waitForTheLayersToInitialize) {
                        finishViewportCreation();
                        if (errorFlag.current == false) {
                            dispatch(closeDialog(DialogTypesEnum.chooseLayers))
                            dispatch(closeDialog(DialogTypesEnum.addMapManuly))
                        }
                    }
                    else {
                        alert(" All layers are initialized! - Open viewport without waiting until all layers are initialized")
                    }

                    const shouldCenter = [ViewportWindowType.fromUI, ViewportWindowType.detailedWindow, ViewportWindowType.oppositeDimensionViewport].includes(props.viewport.type);
                    if (shouldCenter)
                        ViewportService.doCenterPoint(props.viewport.id);

                }, "MapWindow.useEffect.setInterval", handleOpenStandardViewportErrorCatch)
            }, 1000);

            if (!props.viewport.waitForTheLayersToInitialize)
                finishViewportCreation();

        }, "MapWindow.useEffect", handleOpenStandardViewportErrorCatch)
    }, [activateUseEffect2])
    //#endregion

    const finishViewportCreation = () => {
        runCodeSafely(() => {
            setHiddenViewport(false);
            if (props.viewport.type == ViewportWindowType.fromUI)
                newOpenMapService.continueCreating();
        }, 'MapWindow.finishViewportCreation')
    }
    const handleOpenStandardViewportErrorCatch = () => {
        runCodeSafely(() => {
            closethisViewport();
            errorFlag.current = true;
            //In case of created terrain, whose layers were not properly loaded and therefore remain without layers, it must be removed from standAloneTerrains Array.
            let mapCoreServiceTerrains = newOpenMapService.terrains as MapCore.IMcMapTerrain[];
            let layers = mapCoreServiceTerrains.length > 0 ? mapCoreServiceTerrains[0].GetLayers() : [];
            if (layers.length == 0) {
                MapCoreService.releaseStandAloneTerrains(newOpenMapService.terrains);
                newOpenMapService.terrains = [];
            }
        }, 'MapWindow.handleOpenStandardViewportErrorCatch')
    }
    const setAllowMoveMapFalse = () => {
        runCodeSafely(() => {
            setAllowMoveMap(false);
        }, 'MapWindow.setAllowMoveMap');
    }
    const setAllowMoveMapTrue = () => {
        runCodeSafely(() => {
            setAllowMoveMap(true);
        }, 'MapWindow.setAllowMoveMap');
    }
    useEffect(() => {
        runCodeSafely(() => {
            if (canvasRef.current) {
                document.addEventListener("keydown", onKeyMap)
                canvasRef.current.addEventListener('wheel', mouseWheelHandler, { passive: false });
                // בגלילת העכבר מחוץ למפה move מניעת םרוע 
                document.addEventListener("mousedown", setAllowMoveMapFalse)
                document.addEventListener("mouseup", setAllowMoveMapTrue)
            }
        }, "MapWindow.UseEffect of activeCard")
        return () => {
            if (canvasRef.current) {
                document.removeEventListener("keydown", onKeyMap)
                document.removeEventListener("mousedown", setAllowMoveMapFalse)
                document.removeEventListener("mouseup", setAllowMoveMapTrue)
                canvasRef.current?.removeEventListener('wheel', mouseWheelHandler);
            }
        }
    }, [activeCard])

    useEffect(() => {
        runCodeSafely(() => {
            if (canvasRef.current) {
                setLastCanvasWidth((canvasRef.current as HTMLCanvasElement).clientWidth);
                setLastCanvasHeight((canvasRef.current as HTMLCanvasElement).clientHeight);
            }
        }, "MapWindow.UseEffect of canvasRef")
    }, [canvasRef]);

    useEffect(() => {
        setActiveItem(null)
        return () => {
            setActiveItem(null)
        }
    }, []);
    useEffect(() => {
        runCodeSafely(() => {
            setCurrentViewportSize(sizeviewPorts);
            resize(sizeviewPorts.width, sizeviewPorts.height);
        }, "MapWindow.UseEffect of sizeviewPorts and viewportPosition")
    }, [sizeviewPorts, viewportPosition]);

    useEffect(() => {
        runCodeSafely(() => {
            setCurrentViewportPosition(viewportPosition);
        }, "MapWindow.UseEffect of viewportPosition")
    }, [viewportPosition]);

    useEffect(() => {
        runCodeSafely(() => {
            if (maximaizeWindow == props.viewport.id) {
                setCurrentViewportPosition({ x: 0, y: 0 });
                setCurrentViewportSize({ width: window.innerWidth, height: window.innerHeight - 110 });
                resize(window.innerWidth, window.innerHeight - 110);
                dispatch(setActiveCard(props.viewport.id))
            }
            else {
                setCurrentViewportSize(sizeviewPorts);
                resize(sizeviewPorts.width, sizeviewPorts.height);
                setCurrentViewportPosition(viewportPosition)
            }
        }, "MapWindow.UseEffect of maximaizeWindow")
    }, [maximaizeWindow]);

    const resetCurrentViewpSize = () => {
        runCodeSafely(() => {
            dispatch(setmaximaizeWindow(0))
            setCurrentViewportSize(sizeviewPorts);
            resize(sizeviewPorts.width, sizeviewPorts.height);
            setCurrentViewportPosition(viewportPosition);
        }, "mapWindow.resetCurrentViewpSize");
    }

    const onResize = (event: MouseEvent | TouchEvent, e1: ResizeDirection, e2: HTMLElement, delta: ResizableDelta) => {
        runCodeSafely(() => {
            setCurrentViewportSize({ width: viewportSizeOnStartResize.width + delta.width, height: viewportSizeOnStartResize.height + delta.height })
            const newWidth = lastCanvasSizeOnStartResize.width + delta.width;
            const newHeight = lastCanvasSizeOnStartResize.height + delta.height;
            resize(newWidth, newHeight);
        }, "mapWindow.onResize");
    }
    const resize = (width: number, height: number) => {
        runCodeSafely(() => {
            ViewportService.resizeCanvas(props.viewport.id, width, height);
            setLastCanvasWidth(width);
            setLastCanvasHeight(height);
        }, "mapWindow.resize");
    }
    const setHeightLines = () => {
        runCodeSafely(() => {
            ViewportService.doHeighLinesVisualization(props.viewport.id);
        }, "mapWindow.setHeightLines");
    }
    const setDtmVisualization = () => {
        runCodeSafely(() => {
            ViewportService.doDtmVisualization(props.viewport.id);
        }, "mapWindow.setDtmVisualization");
    }
    const closethisViewport = () => {
        runCodeSafely(() => {
            dispatch(closeViewport(props.viewport.id));
            ViewportService.closeViewport(props.viewport.id, generalService.calcSizeAndPositionCanvases);
        }, "mapWindow.closethisViewport");
    }
    const maximizeViewport = () => {
        runCodeSafely(() => {
            if (maximaizeWindow == 0) {
                setCurrentViewportPosition({ x: 0, y: 0 });
                setCurrentViewportSize({ width: window.innerWidth, height: window.innerHeight - 110 });
                resize(window.innerWidth, window.innerHeight - 110);
                dispatch(setmaximaizeWindow(props.viewport.id))
            }
            else {
                resetCurrentViewpSize()
            }
        }, "mapWindow.maximizeViewport")
    }

    const onDragStop = (e: DraggableEvent, data: DraggableData) => {
        runCodeSafely(() => {
            setCurrentViewportPosition({ x: data.lastX, y: data.lastY })
        }, "mapWindow.onDragStop");
    }
    const onScan = () => {
        setActiveCard(props.viewport.id)
        runCodeSafely(() => {
            dispatch(setDialogType(DialogTypesEnum.scanGeometry));
        }, "mapWindow.onScan");
    }
    const onPrint = () => {
        runCodeSafely(() => {
            setActiveCard(props.viewport.id)
            dispatch(setDialogType(DialogTypesEnum.printMap));
        }, "mapWindow.onPrint");
    }
    // #region mouse event
    const mouseMoveHandler = (e: React.MouseEvent<HTMLCanvasElement, MouseEvent>) => {
        runCodeSafely(() => {
            if (allowMoveMap) {
                setCursorInCss(ViewportService.mouseMoveHandler(e, activeCard))
            }
        }, "mapWindow.mouseMoveHandler");
    }

    function mouseWheelHandler(e: React.WheelEvent<HTMLCanvasElement>) {
        runCodeSafely(() => {
            setCursorInCss(
                ViewportService.mouseWheelHandler(e, props.viewport.id)
            )
        }, "mapWindow.mouseWheelHandler");
    }

    const mouseDownHandler = (e: React.MouseEvent<HTMLCanvasElement, MouseEvent>) => {
        runCodeSafely(() => {
            dispatch(setActiveCard(props.viewport.id))
            setCursorInCss(
                ViewportService.mouseDownHandler(e, props.viewport.id)
            )
            editModeNavigationService.editModeNavigationMouseDownHandler();
        }, "mapWindow.mouseDownHandler");
    }
    const getCursorRgb = (cursorPos) => {
        const canvas: HTMLCanvasElement = canvasRef.current;
        const context = canvas.getContext('2d', { willReadFrequently: true });
        const canvasWidth = canvas.width;
        const canvasHeight = canvas.height;
        // בדיקה שהנקודה נמצםת בתוך גבולות הקנבס
        let rgbObj = null;
        if (cursorPos.x >= 0 && cursorPos.x < canvasWidth && cursorPos.y >= 0 && cursorPos.y < canvasHeight) {
            const imageData = context.getImageData(cursorPos.x, cursorPos.y, canvasWidth, canvasHeight);
            rgbObj = { R: imageData.data[0], G: imageData.data[1], B: imageData.data[2] };
        }
        return rgbObj;
    }
    const showCursorPosition = (e: React.MouseEvent<HTMLCanvasElement, MouseEvent>) => {
        runCodeSafely(() => {
            let obj = ViewportService.showCursorPosition(e, props.viewport.id);

            if (obj) {
                dispatch(setCursorPos(obj.worldPos));
                dispatch(setScaleBox(obj.ScaleBox));
                dispatch(setMapScaleBox(obj.MapScaleBox));
                dispatch(setScreenPos(obj.screenPos));
                dispatch(setCurrentViewport(obj.viewportId));
                ScanService.bPointScan = false;
            }
            if (showDialog.hideFormReason == hideFormReasons.CHOOSE_POINT && showDialog.dialogType == DialogTypesEnum.mapWorldTree && obj) {
                let cursorRgb = getCursorRgb(obj.screenPos);
                dispatch(setCursorRgb(cursorRgb));
            }
        }, "mapWindow.showCursorPosition");
    }
    const mouseUpHandler = (e: React.MouseEvent<HTMLCanvasElement, MouseEvent>) => {
        runCodeSafely(() => {
            setCursorInCss(
                ViewportService.mouseUpHandler(e, props.viewport.id)
            )
            editModeNavigationService.editModeNavigationMouseUpHandler();
        }, "mapWindow.mouseUpHandler");
    }
    const mouseDblClickHandler = (e: React.MouseEvent<HTMLCanvasElement, MouseEvent>) => {
        runCodeSafely(() => {
            setCursorInCss(
                ViewportService.mouseDblClickHandler(e, props.viewport.id)
            )
        }, "MapWindow.mouseDblClickHandler");
    }
    const setCursorInCss = (Ecursor: any) => {
        runCodeSafely(() => {
            if (Ecursor) {
                let cssCursor = "auto"
                let numCursor: number = Ecursor.Value.value;
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
                canvasRef.current.style.cursor = cssCursor;
            }
        }, "MapWindow.setCursorInCss")
    }
    const handleEditOrInitObject = (initOrEdit: DialogTypesEnum) => {
        runCodeSafely(() => {
            dispatch(setActiveCard(props.viewport.id))
            dispatch(setDialogType(initOrEdit));
        }, "mapWindow.handleEditObject");
    }
    const handleExitCurrentActionClick = () => {
        runCodeSafely(() => {
            let activeViewport = MapCoreData.findViewport(activeCard);
            activeViewport.editMode.ExitCurrentAction(generalService.EditModePropertiesBase.DiscardChanges);
        }, 'mapWindow.handleExitCurrentActionClick')
    }

    const handleCheckRenderNeeded = () => {
        dispatch(setDialogType(DialogTypesEnum.checkRenderNeeded));
    }
    const [inputPosition, setInputPosition] = useState({ x: 0, y: 0 });
    const [showInput, setShowInput] = useState(false);

    const handleCanvasClick = (e: any) => {
        const rect = e.target.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;
        setInputPosition({ x, y });
        setShowInput(true);
    };

    const handleInputBlur = () => {
        let val = inputText.current.value
        setShowInput(false);
        let strValue: MapCore.SMcVariantString = new MapCore.SMcVariantString(val, generalService.ObjectProperties.TextIsUnicode);
        newGeneratedObject.SetStringProperty(Id.Item_Text_Text, strValue);
    };

    //#endregion
    const toolbar: TabMenuModel =
    {
        items:
            [
                symbolicItemsService.getSymbolicItemsToolBar(MapCoreData.findViewport(props.viewport.id)),
                {
                    header: 'Physical Items',
                    menuItems: [
                        { label: "Mesh item", icon: "http:mctester icons/tsbMeshItem.Image.png" },
                        { label: "Practical effect item", icon: "http:mctester icons/tsbParticalEffectItem.Image.png" },
                        { label: "Light item", icon: "http:mctester icons/tsbLightItem.Image.png" },
                        { label: "Projector item", icon: "http:mctester icons/tsbProjectorItem.Image.png" },
                        { label: "Sound item", icon: "http:mctester icons/tsbSoundItem.Image.png" },
                    ]
                },
                {
                    header: 'Navigate',
                    menuItems: [
                        {
                            label: "Zoom In", icon: "http:mctester icons/tsbZoomIn.Image.png", action: () => {
                                runCodeSafely(() => {
                                    let obj = ViewportService.zoomIn(props.viewport.id)
                                    dispatch(setMapScaleBox(obj?.MapScaleBox))
                                    dispatch(setScaleBox(obj?.ScaleBox))
                                }, "mapWindow.zoomIn");
                            }
                        },
                        {
                            label: "Zoom Out", icon: "http:mctester icons/tsbZoomOut.Image.png", action: () => {
                                runCodeSafely(() => {
                                    let obj = ViewportService.zoomOut(props.viewport.id)
                                    dispatch(setMapScaleBox(obj?.MapScaleBox))
                                    dispatch(setScaleBox(obj?.ScaleBox))
                                }, "mapWindow.zoomOut")
                            }
                        },
                        { label: "Center point", icon: "http:mctester icons/center_point.png", action: () => { runCodeSafely(() => { ViewportService.doCenterPoint(props.viewport.id) }, "mapWindow.doCenterPoint") } },
                        {
                            label: "Dynamic zoom", icon: "http:mctester icons/tsbDynamicZoom.Image.png", action: () => {
                                runCodeSafely(() => {
                                    console.log(props.viewport.id, generalService.EditModePropertiesBase.DynamicZoomMinScale, generalService.EditModePropertiesBase.DynamicZoomRectangle, generalService.EditModePropertiesBase.DynamicZoomOperation);

                                    ViewportService.dynamicZoom(props.viewport.id, generalService.EditModePropertiesBase.DynamicZoomMinScale, generalService.EditModePropertiesBase.DynamicZoomRectangle, generalService.EditModePropertiesBase.DynamicZoomOperation)
                                }, "mapWindow.dynamicZoom")
                            }
                        },
                        { label: "Navigate map", icon: "http:mctester icons/tsbNavigateMap.Image.png" },
                    ]
                },
                {
                    header: 'Map Operations',
                    menuItems: [
                        { label: "Map grid", icon: "http:mctester icons/tsbMapGrid.Image.png", menu: grids },
                        { label: "Map height lines", icon: "http:mctester icons/tsbMapHeightLines.Image.png", action: setHeightLines },
                        { label: "Map DTM", icon: "http:mctester icons/tsbMapHeightMap.Image.png", action: setDtmVisualization },
                        { label: "Footprint point", icon: "http:mctester icons/tsbDropDownFootprintPoints.Image.png" },
                        { label: "Print", icon: "http:mctester icons/tsbPrint.Image.png", action: onPrint },
                        { label: "Scan", icon: "http:mctester icons/tsbScan.Image.png", action: onScan },
                        { label: "Environment", icon: "http:mctester icons/tsbEnvironment.Image.png" },
                    ]
                },
                {
                    header: 'Edit Mode',
                    menuItems: [
                        { label: "Edit object", icon: "http:mctester icons/tsbEditObject.Image.png", action: () => handleEditOrInitObject(DialogTypesEnum.editObject) },
                        { label: "Init object", icon: "http:mctester icons/tsbInitObject.Image.png", action: () => handleEditOrInitObject(DialogTypesEnum.initObject) },
                        { label: "Distance direction measure", icon: "http:mctester icons/tsbDistanceDirectionMeasure.Image.png", action: () => { runCodeSafely(() => { ViewportService.distanceDirection(props.viewport.id, generalService.EditModePropertiesBase.ShowResult) }, "mapWindow.distancedirection") } },
                        { label: "Exit current action", icon: "http:mctester icons/tsbExitCurrentAction.Image.png", action: handleExitCurrentActionClick },
                        {
                            label: "Edit mode properties", icon: "http:mctester icons/tsbEditModeProperties.Image.png", action: () => {
                                runCodeSafely(() => {
                                    dispatch(setDialogType(DialogTypesEnum.editModeProperties));
                                }, "mapWindow.editModeProperties")
                            }
                        },
                        { label: "Edit mode navigation", icon: "", action: editModeNavigationService.handleEditModeNavigationClick },
                        { label: "Check render needed", icon: "", action: handleCheckRenderNeeded },
                        { label: "Event callback", icon: "", action: () => { dispatch(setDialogType(DialogTypesEnum.eventCallback)) } },
                    ]
                },
                {
                    header: 'Utilities',
                    menuItems: [
                        { label: "Performance tester", icon: "http:mctester icons/tsbPerformanceTester.Image.png" },
                        { label: "Stress tester", icon: "http:mctester icons/tsbStressTester.Image.png" },
                        { label: "Object loader", icon: "http:mctester icons/tsbObjectLoader.Image.png" },
                        { label: "Object auto animatation", icon: "http:mctester icons/tsbObjectAutoAnimatation.Image.png" },
                        { label: "Animation state", icon: "http:mctester icons/tsbAnimationState.Image.png" },
                        { label: "Nav path", icon: "http:mctester icons/tsbNavPath.Image.png" },
                    ]
                },
            ]
    };
    const Sidepanel = useRef(null)
    const openAndCloseNav = () => {
        if (flagOpenNav) {
            setFlagOpenNav(false);
            (Sidepanel.current as unknown as HTMLDivElement).style.height = `${globalSizeFactor * 8.4}vh`;
        }
        else {
            if (FlagPinMapMenu) {
                setFlagOpenNav(true);
                (Sidepanel.current as unknown as HTMLDivElement).style.height = "0"
            }
        }
    }
    const closeNav = () => {
        if (FlagPinMapMenu) {
            setFlagOpenNav(true);
            (Sidepanel.current as unknown as HTMLDivElement).style.height = "0"
        }
    }
    const openNav = () => {
        setFlagOpenNav(false);
        (Sidepanel.current as unknown as HTMLDivElement).style.height = `${globalSizeFactor * 8.4}vh`;
    }
    // 
    return (<div hidden={hiddenViewport}>
        <Rnd onMouseDown={() => dispatch(setActiveCard(props.viewport.id))}
            bounds="parent"
            onDragStop={onDragStop}
            className={`map-window__rng-min ${activeCard === props.viewport.id && "map-window__rng-min-active"}`}
            size={currentViewportSize}
            position={currentViewportPosition}
            onResize={(event, e1, e2, delta) => { onResize(event, e1, e2, delta) }}
            disableDragging={maximaizeWindow == props.viewport.id}
            onResizeStart={() => {
                setViewportSizeOnStartResize(currentViewportSize);
                setLastCanvasSizeOnStartResize({ width: lastCanvasWidth, height: lastCanvasHeight });
            }}>

            <div className={`map-window__viewport ${activeCard === props.viewport.id && "map-window__viewport-active"} ${maximaizeWindow == props.viewport.id && "map-window__maximize"} `}
                onMouseDown={() => dispatch(setActiveCard(props.viewport.id))}
                onKeyDown={onKeyMap}
                onKeyUp={KeyUpOnMap}>
                <div className={`map-window__header ${maximaizeWindow == props.viewport.id && "map-window__header-maximize"}`} style={{ width: (currentViewportSize.width - 5) }}>
                    <div>
                        <button onClick={openAndCloseNav} style={{ marginRight: 2 }}> ☰ </button>
                        <button onClick={() => { setFlagPinMapMenu(!FlagPinMapMenu); if (flagOpenNav) openNav() }} style={{ marginRight: 2 }}>
                            {FlagPinMapMenu ? <FontAwesomeIcon icon={faThumbtack} rotation={90} /> : <FontAwesomeIcon icon={faThumbtack} />}
                        </button>
                        <label dir='ltr'>({props.viewport.id}) Map viewport </label>
                        {ViewportService.viewport3D(props.viewport.id) ? <label>[3D]</label> : <label>[2D]</label>}
                    </div>
                    <div className='divIcons'>
                        <i className='pi pi-minus map-window__icon' style={{ paddingRight: `${globalSizeFactor * 1.2}vh` }} onClick={resetCurrentViewpSize}></i>
                        <i style={{ paddingRight: `${globalSizeFactor * 1.2}vh`, cursor: 'pointer' }} onClick={maximizeViewport}>
                            {maximaizeWindow == props.viewport.id ? <FontAwesomeIcon icon={faWindowRestore} /> :
                                <FontAwesomeIcon icon={faWindowMaximize} />}
                        </i>
                        <i className='pi pi-times map-window__icon' onClick={closethisViewport}></i>
                    </div>
                </div>
                <div ref={Sidepanel} id="mySidepanel" className="map-window__side-panel">
                    <Tabmenu flagMenu={false} toolbar={toolbar} closeNav={closeNav} />
                </div>
                <canvas ref={canvasRef} style={{ backgroundColor: 'black', position: 'relative' }}
                    onMouseMove={(e) => { mouseMoveHandler(e) }}
                    onMouseDown={(e) => { mouseDownHandler(e) }}
                    onClick={(e) => {
                        showCursorPosition(e);
                        if (showDialog.hideFormReason == hideFormReasons.CHOOSE_POINT && showDialog.dialogType !== DialogTypesEnum.editObject && showDialog.dialogType !== DialogTypesEnum.initObject) {
                            dispatch(setShowDialog({ hideFormReason: null, dialogType: null }))
                        }
                        if ((createText == true)) {
                            handleCanvasClick(e);
                            store.dispatch(setCreateText(false));
                        }
                    }}
                    onMouseUp={(e) => { { mouseUpHandler(e) } }}
                    onDoubleClick={(e) => { { mouseDblClickHandler(e) } }} >
                </canvas>
            </div>
        </Rnd >
        {showInput && (<div style={{ zIndex: 3, position: 'absolute', left: inputPosition.x, top: inputPosition.y, display: 'flex' }}>
            <textarea ref={inputText} style={{ whiteSpace: 'nowrap' }}
                onKeyDown={(e) => {
                    let currentViewport: ViewportData = MapCoreData.findViewport(props.viewport.id);
                    if (currentViewport.editMode.GetPermissions() & (MapCore.IMcEditMode.EPermission.EEMP_FINISH_TEXT_STRING_BY_KEY as any).value)
                        if (e.code == "Enter")
                            handleInputBlur()
                }}
                autoFocus />
            <button onClick={handleInputBlur}>OK</button>
        </div>
        )}
    </div>
    );
}