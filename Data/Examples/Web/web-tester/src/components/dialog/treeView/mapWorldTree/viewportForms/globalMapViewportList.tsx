import { useEffect, useRef, useState } from "react";
import { ListBox, ListBoxChangeEvent } from "primereact/listbox";
import { Button } from "primereact/button";
import { useSelector } from "react-redux";
import _ from "lodash";

import { MapCoreData, ViewportData } from 'mapcore-lib'
import generalService from "../../../../../services/general.service";
import { runAsyncCodeSafely, runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import screenPositionService from "../../../../../services/screenPosition.service";

export type globalMapActionType = "Register" | "UnRegister" | "SetActiveLocalMap" | "ScreenPositions";

export default function GlobalMapViewportList(props: { viewport: MapCore.IMcMapViewport, action: globalMapActionType, closeDialog: () => void }) {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const [viewportList, setViewportList] = useState<{ name: string, viewport: ViewportData }[]>([]);
    const [clearSelectionButton, setClearSelectionButton] = useState(false);
    const [selectedViewport, setSelectedViewport] = useState<{ name: string, viewport: ViewportData }>(null)
    const internalUseOverlayRef = useRef<MapCore.IMcOverlay>(null);

    useEffect(() => {
        runCodeSafely(() => {
            setDialogWidth();


            let list: { name: string, viewport: ViewportData }[] = [];
            switch (props.action) {
                case "Register":
                    MapCoreData.viewportsData.forEach((v: ViewportData) => {
                        if (v.viewport != props.viewport)
                            list.push({ name: generalService.getObjectName(v, "Viewport"), viewport: v })
                    })
                    setViewportList(list)
                    break;
                case "ScreenPositions":
                    runMapCoreSafely(() => {
                        internalUseOverlayRef.current = MapCore.IMcOverlay.Create(props.viewport.GetOverlayManager(), true)
                    }, 'GlobalMapViewportList/UseEffect => IMcOverlay.Create', true);

                    setClearSelectionButton(true);
                    const mcRegisteredViewports = props.viewport.GetRegisteredLocalMaps();
                    mcRegisteredViewports.forEach(mcRegisteredVp => {
                        const viewportData = MapCoreData.viewportsData.find(vpData => vpData.viewport == mcRegisteredVp);
                        list.push({ name: generalService.getObjectName(viewportData, "Viewport"), viewport: viewportData })
                    });
                    setViewportList(list)
                    break;
                case "UnRegister":
                case "SetActiveLocalMap":
                    if (props.action === "SetActiveLocalMap") {
                        setClearSelectionButton(true);
                    }

                    let registeredMaps: MapCore.IMcMapViewport[]
                    runMapCoreSafely(() => {
                        registeredMaps = props.viewport.GetRegisteredLocalMaps();
                    }, "GlobalMapViewportList/UseEffect => GetRegisteredLocalMaps", true)

                    registeredMaps.forEach((registeredMap: MapCore.IMcMapViewport) => {
                        MapCoreData.viewportsData.forEach((v: ViewportData) => {
                            if (registeredMap == v.viewport) {
                                list.push({ name: generalService.getObjectName(v, "Viewport"), viewport: v })
                            }
                        })
                    })
                    setViewportList(list)

                    let activeLocalMap: MapCore.IMcMapViewport = props.viewport.GetActiveLocalMap();
                    if (activeLocalMap !== null) {
                        MapCoreData.viewportsData.forEach((v: ViewportData) => {
                            if (activeLocalMap == v.viewport) {
                                setSelectedViewport({ name: generalService.getObjectName(v, "Viewport"), viewport: v });
                            }
                        })
                    }
            }
        }, 'GlobalMapViewportList.UseEffect[mounting]')

        return () => {
            if (props.action == "ScreenPositions") {
                runMapCoreSafely(() => {
                    internalUseOverlayRef.current.Remove();
                }, 'GlobalMapViewportList.UseEffect[unmounting]', true)
            }
        }
    }, [])
    //#region Help Functions
    const setDialogWidth = () => {
        runCodeSafely(() => {
            const root = document.documentElement;
            let pixelWidth = window.innerHeight * 0.4 * globalSizeFactor;
            root.style.setProperty('--global-map-vp-list-dialog-width', `${pixelWidth}px`);
        }, 'GlobalMapViewportList.setDialogWidth')
    }
    const createScreenPointsTextObject = (scheme: MapCore.IMcObjectScheme, points: MapCore.SMcVector2D[], color: MapCore.SMcBColor) => {
        runCodeSafely(() => {
            points.forEach((point: MapCore.SMcVector2D, i: number) => {
                screenPositionService.createScreenPointObject(internalUseOverlayRef, scheme, point, color, i.toString())
            })
        }, 'GlobalMapViewportList.createScreenPointsText')
    }
    //#endregion
    //#region Handle Functions
    const handleOKClick = () => {
        runCodeSafely(() => {
            props.closeDialog();
            const finalViewportValue = selectedViewport ? selectedViewport.viewport.viewport : null;
            switch (props.action) {
                case "Register":
                    if (selectedViewport?.viewport.viewport) {
                        runMapCoreSafely(() => {
                            props.viewport.RegisterLocalMap(finalViewportValue);
                        }, 'globalMapViewportList.handleOKClick => IMcGlobalMap.RegisterLocalMap', true)
                    }
                    break;
                case "UnRegister":
                    if (selectedViewport?.viewport.viewport) {
                        runMapCoreSafely(() => {
                            props.viewport.UnRegisterLocalMap(finalViewportValue);
                        }, 'globalMapViewportList.handleOKClick => IMcGlobalMap.UnRegisterLocalMap', true)
                    }
                    break;
                case "SetActiveLocalMap":
                    runMapCoreSafely(() => {
                        props.viewport.SetActiveLocalMap(finalViewportValue);
                    }, 'globalMapViewportList.handleOKClick => IMcGlobalMap.SetActiveLocalMap', true)
            }
        }, 'globalMapViewportList.handleOKClick')
    }
    const handleListBoxChange = (e: ListBoxChangeEvent) => {
        runAsyncCodeSafely(async () => {
            setSelectedViewport(e.value);

            if (props.action == 'ScreenPositions') {
                //remove old objects
                const overlayObjects = internalUseOverlayRef.current.GetObjects();
                overlayObjects.map(object => {
                    runMapCoreSafely(() => { object.Remove(); }, 'GlobalMapViewportList.handleListBoxChange => IMcObject.Remove', true);
                })
                if (e.value) {
                    const mcCurrentLocalMap = e.value.viewport.viewport;
                    let polygonPointsArr: MapCore.SMcVector2D[] = [];
                    let arrowPointsArr: MapCore.SMcVector2D[] = [];
                    //generate updated objects
                    if (mcCurrentLocalMap.GetMapType() == MapCore.IMcMapCamera.EMapType.EMT_2D)
                        runMapCoreSafely(() => { props.viewport.GetLocalMapFootprintScreenPositions(mcCurrentLocalMap, polygonPointsArr); }, 'GlobalMapViewportList.handleListBoxChange => IMcGlobalMap.GetLocalMapFootprintScreenPositions', true);
                    else
                        runMapCoreSafely(() => { props.viewport.GetLocalMapFootprintScreenPositions(mcCurrentLocalMap, polygonPointsArr, arrowPointsArr); }, 'GlobalMapViewportList.handleListBoxChange => IMcGlobalMap.GetLocalMapFootprintScreenPositions', true);
                    if (props.viewport.GetOverlayManager()) {
                        const screenPointsObjectScheme: MapCore.IMcObjectScheme = await screenPositionService.getObjectSchemeScreenPoints(props.viewport, internalUseOverlayRef.current);
                        const activeColors = { polygon: new MapCore.SMcBColor(255, 0, 180, 255), arrow: new MapCore.SMcBColor(128, 0, 32, 255) }
                        const inActiveColors = { polygon: new MapCore.SMcBColor(144, 238, 144, 255), arrow: new MapCore.SMcBColor(0, 224, 209, 255) }
                        const isLocalMapActive = props.viewport.GetActiveLocalMap() == mcCurrentLocalMap;
                        polygonPointsArr.length && createScreenPointsTextObject(screenPointsObjectScheme, polygonPointsArr, isLocalMapActive ? activeColors.polygon : inActiveColors.polygon);
                        arrowPointsArr.length && createScreenPointsTextObject(screenPointsObjectScheme, arrowPointsArr, isLocalMapActive ? activeColors.arrow : inActiveColors.arrow);
                    }
                }
            }
        }, 'GlobalMapViewportList.handleListBoxChange')
    }
    const handleClearSelectionClick = () => {
        setSelectedViewport(null);

        if (props.action == 'ScreenPositions') {
            const overlayObjects = internalUseOverlayRef.current.GetObjects();
            overlayObjects.map(object => {
                runMapCoreSafely(() => { object.Remove(); }, 'GlobalMapViewportList.handleListBoxChange => IMcObject.Remove', true);
            })
        }
    }
    //#endregion

    return (
        <div>
            <ListBox listStyle={{ maxHeight: `${globalSizeFactor * 19.5}vh`, minHeight: `${globalSizeFactor * 19.5}vh` }} name="selectedViewport" options={viewportList} optionLabel="name" value={selectedViewport}
                onChange={handleListBoxChange} />
            <div className="form__apply-buttons-container">
                <Button style={{ display: clearSelectionButton ? 'inline-block' : 'none' }} onClick={handleClearSelectionClick} label='Clear Selection' />
                <Button label="OK" onClick={handleOKClick} />
            </div>
        </div>
    )
}
