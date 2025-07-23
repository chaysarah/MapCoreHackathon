import { useEffect, useState } from "react";
import { InputText } from "primereact/inputtext";
import { Fieldset } from "primereact/fieldset";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "primereact/button";
import { Checkbox } from "primereact/checkbox";
import { MapCoreData, ViewportData, } from 'mapcore-lib';
import { ConfirmDialog } from "primereact/confirmdialog";
import { InputNumber } from "primereact/inputnumber";
import _ from 'lodash';

import './styles/sectionMap.css';
import { MapViewportFormTabInfo } from "../mapViewportForm";
import Vector3DFromMap from "../../../objectWorldTree/shared/Vector3DFromMap";
import Vector3DGrid from "../../../objectWorldTree/shared/vector3DGrid";
import DrawLineCtrl from "../../../../shared/drawLineCtrl";
import SelectExistingItem from "../../../../shared/ControlsForMapcoreObjects/itemCtrl/selectExistingItem";
import { Properties } from "../../../../dialog";
import { AppState } from "../../../../../../redux/combineReducer";
import { runCodeSafely, runMapCoreSafely } from "../../../../../../common/services/error-handling/errorHandler";
import dialogStateService from "../../../../../../services/dialogStateService";
import { setTypeMapWorldDialogSecond } from "../../../../../../redux/MapWorldTree/mapWorldTreeActions";


export class SectionMapPropertiesState implements Properties {
    sectionRoutePointsArr: any[] = [];
    sectionHeightPointsArr: any[] = [];
    isCalculateSectionHeightPoints: boolean = false;
    polygonItem: MapCore.IMcPolygonItem = null;
    axesRatio: number = 0;
    xLeftLimit: number = 0;
    xRightLimit: number = 0;
    yUpperLimit: number = 0;
    yLowerLimit: number = 0;

    static getDefault(p: any): SectionMapPropertiesState {
        let { currentViewport } = p;
        let vpType = currentViewport.nodeMcContent.GetInterfaceType();
        let isSectionMap = vpType == MapCore.IMcSectionMapViewport.INTERFACE_TYPE;
        let mcSectionRoutePointsArr = [];
        let mcSectionHeightPointsArr = [];
        let mcXLeftLimit, mcXRightLimit, mcYUpperLimit, mcYLowerLimit, mcAxesRatio = 0;
        let mcPolygonItem = null;
        if (isSectionMap) {
            let mcCurrnetVp = currentViewport.nodeMcContent as MapCore.IMcSectionMapViewport;
            runMapCoreSafely(() => { mcCurrnetVp.GetSectionRoutePoints(mcSectionRoutePointsArr, mcSectionHeightPointsArr); }, 'SectionMapPropertiesState.getDefault => IMcSectionMapViewport.GetSectionRoutePoints', true)
            runMapCoreSafely(() => { mcAxesRatio = mcCurrnetVp.GetAxesRatio(); }, 'SectionMapPropertiesState.getDefault => IMcSectionMapViewport.GetAxesRatio', true)
            // runMapCoreSafely(() => { mcPolygonItem = mcCurrnetVp.GetSectionPolygonItem(); }, 'SectionMapPropertiesState.getDefault => IMcSectionMapViewport.GetSectionPolygonItem', true)
        }
        return {
            sectionRoutePointsArr: mcSectionRoutePointsArr,
            sectionHeightPointsArr: mcSectionHeightPointsArr,
            isCalculateSectionHeightPoints: false,
            polygonItem: mcPolygonItem,
            axesRatio: mcAxesRatio,
            xLeftLimit: mcXLeftLimit,
            xRightLimit: mcXRightLimit,
            yUpperLimit: mcYUpperLimit,
            yLowerLimit: mcYLowerLimit,
        }
    }
}
export class SectionMapProperties extends SectionMapPropertiesState {
    worldToSectionPoint: MapCore.SMcVector3D = new MapCore.SMcVector3D(0, 0, 0);
    sectionToWorldPoint: MapCore.SMcVector3D = new MapCore.SMcVector3D(0, 0, 0);
    xCoordinate: number;
    height: number;
    slope: number;

    isConfirmDialogVisible: boolean;
    confirmDialogHeader: string;
    confirmDialogMessage: string;

    static getDefault(p: any): SectionMapProperties {
        let stateDefaults = super.getDefault(p);

        let defaults: SectionMapProperties = {
            ...stateDefaults,

            worldToSectionPoint: new MapCore.SMcVector3D(0, 0, 0),
            sectionToWorldPoint: new MapCore.SMcVector3D(0, 0, 0),
            xCoordinate: 0,
            height: 0,
            slope: 0,

            isConfirmDialogVisible: false,
            confirmDialogHeader: '',
            confirmDialogMessage: '',
        }
        return defaults;
    }
};

export default function SectionMap(props: { tabInfo: MapViewportFormTabInfo }) {
    const dispatch = useDispatch();
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));
    let treeRedux = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let [sectionMapLocalProperties, setSectionMapLocalProperties] = useState(props.tabInfo.properties.sectionMapProperties);
    let [enableForm, setEnableForm] = useState(true);

    const [isMountedUseEffect, setIsMountedUseEffect] = useState({
        currentViewport: false,
    })

    //UseEffects
    useEffect(() => {
        runCodeSafely(() => {
            let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
            let vpType = mcCurrentViewport.GetInterfaceType();
            vpType == MapCore.IMcSectionMapViewport.INTERFACE_TYPE ?
                setEnableForm(true) : setEnableForm(false);
        }, 'MapViewportForm/SectionMap.useEffect');
    }, [])

    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack("SectionMap", applyAll);
            setSectionMapLocalProperties(props.tabInfo.properties.sectionMapProperties);
        }, 'MapViewportForm/SectionMap.useEffect => sectionMapProperties');
    }, [props.tabInfo.properties.sectionMapProperties])

    useEffect(() => {
        runCodeSafely(() => {
            if (isMountedUseEffect.currentViewport) {
                props.tabInfo.setInitialStatePropertiesCallback("sectionMapProperties", null, SectionMapPropertiesState.getDefault({ currentViewport: props.tabInfo.currentViewport }));
                props.tabInfo.setPropertiesCallback("sectionMapProperties", null, SectionMapProperties.getDefault({ currentViewport: props.tabInfo.currentViewport }));
                let mcCurrentViewport = selectedNodeInTree.nodeMcContent as MapCore.IMcMapViewport;
                let vpType = mcCurrentViewport.GetInterfaceType();
                vpType == MapCore.IMcSectionMapViewport.INTERFACE_TYPE ?
                    setEnableForm(true) : setEnableForm(false);
            }
            else {
                setIsMountedUseEffect({ ...isMountedUseEffect, currentViewport: true })
            }
        }, 'MapViewportForm/SectionMap.useEffect => currentViewport');
    }, [props.tabInfo.currentViewport])

    const saveData = (event: any) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback("sectionMapProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            if (event.target.name in new SectionMapPropertiesState) {
                props.tabInfo.setCurrStatePropertiesCallback("sectionMapProperties", event.target.name, event.target.type === "checkbox" ? event.target.checked : event.target.value);
            }
        }, "MapViewportForm/SectionMap.saveData => onChange")
    }
    const applyAll = () => {
        runCodeSafely(() => {

        }, 'MapViewportForm/SectionMap.applyAll');
    }

    const saveSectionPointsTable = (...args) => {
        runCodeSafely(() => {
            const [locationPointsList, valid, selectedPoint, sectionPointsType] = args;
            if (!_.isEqual(sectionMapLocalProperties[sectionPointsType], locationPointsList)) {
                props.tabInfo.setPropertiesCallback("sectionMapProperties", sectionPointsType, locationPointsList);
            }
        }, 'MapViewportForm/SectionMap.saveLocationPointsTable');
    }
    const saveWorldSectionPoint = (...args) => {
        runCodeSafely(() => {
            const [point, flagNull, sectionPointType] = args;
            props.tabInfo.setPropertiesCallback("sectionMapProperties", sectionPointType, point);
        }, 'MapViewportForm/SectionMap.saveWorldSectionPoint');
    }
    const initLineResultsCB = (pObject: MapCore.IMcObject, pItem: MapCore.IMcObjectSchemeItem, nExitCode: number) => {
        runCodeSafely(() => {
            let locationPoints = pObject.GetLocationPoints();
            let vpType = activeViewport.viewport.GetInterfaceType();
            if (vpType == MapCore.IMcSectionMapViewport.INTERFACE_TYPE) {
                let sectionMap = activeViewport.viewport as MapCore.IMcSectionMapViewport;
                locationPoints = locationPoints.map(point => sectionMap.SectionToWorld(point)).flat();
            }
            props.tabInfo.setPropertiesCallback("sectionMapProperties", 'sectionRoutePointsArr', locationPoints);
            pObject.Remove();
        }, 'LocationConditionalSelector.startDrawOrEditPolygon => InitItemResults')
    }
    //Handle Functions
    const handleAxesRatioOK = () => {
        runCodeSafely(() => {
            let sectionVp = selectedNodeInTree.nodeMcContent as MapCore.IMcSectionMapViewport;
            runMapCoreSafely(() => {
                sectionVp.SetAxesRatio(sectionMapLocalProperties.axesRatio)
            }, 'MapViewportForm/SectionMap.handleAxesRatioOK => IMcSectionMapViewport.SetAxesRatio', true)
            dialogStateService.applyDialogState(["sectionMapProperties.axesRatio"])
        }, 'MapViewportForm/SectionMap.handleAxesRatioOK');
    }
    const setSelectedItem = (item: MapCore.IMcObjectSchemeNode) => {
        let sectionVp = selectedNodeInTree.nodeMcContent as MapCore.IMcSectionMapViewport;
        runMapCoreSafely(() => {
            sectionVp.SetSectionPolygonItem(item as MapCore.IMcPolygonItem)
        }, 'MapViewportForm/SectionMap.setSelectedItem => IMcSectionMapViewport.SetSectionPolygonItem', true)
    }
    const handlePolygonClick = () => {
        runCodeSafely(() => {
            dispatch(setTypeMapWorldDialogSecond({
                secondDialogHeader: 'Select Existing Items',
                secondDialogComponent: <SelectExistingItem itemType={MapCore.IMcPolygonItem.NODE_TYPE} finalActionButton handleOKClick={(selectedItem: MapCore.IMcObjectSchemeItem) => {
                    dispatch(setTypeMapWorldDialogSecond(undefined));
                    setSelectedItem(selectedItem);
                }} />
            }))
        }, 'MapViewportForm/SectionMap.handlePolygonClick');
    }
    const handleGetSectionHeightClick = () => {
        runCodeSafely(() => {
            let sectionVp = selectedNodeInTree.nodeMcContent as MapCore.IMcSectionMapViewport;
            let height: { Value?: number } = {};
            let slope: { Value?: number } = {};
            runMapCoreSafely(() => {
                sectionVp.GetSectionHeightAtPoint(sectionMapLocalProperties.xCoordinate, height, slope);
            }, 'MapViewportForm/SectionMap.handleGetSectionHeightClick => IMcSectionMapViewport.GetSectionHeightAtPoint', true)
            props.tabInfo.setPropertiesCallback("sectionMapProperties", 'height', height.Value);
            props.tabInfo.setPropertiesCallback("sectionMapProperties", 'slope', slope.Value);
        }, 'MapViewportForm/SectionMap.handleGetSectionHeightClick');
    }
    const handleGetHeightLimitsClick = () => {
        runCodeSafely(() => {
            let sectionVp = selectedNodeInTree.nodeMcContent as MapCore.IMcSectionMapViewport;
            let pMinY: { Value?: number } = {};
            let pMaxY: { Value?: number } = {};
            runMapCoreSafely(() => {
                sectionVp.GetHeightLimits(sectionMapLocalProperties.xLeftLimit, sectionMapLocalProperties.xRightLimit, pMinY, pMaxY);
            }, 'MapViewportForm/SectionMap.handleGetHeightLimitsClick => IMcSectionMapViewport.GetHeightLimits', true)
            props.tabInfo.setPropertiesCallback("sectionMapProperties", 'yLowerLimit', pMinY.Value);
            props.tabInfo.setPropertiesCallback("sectionMapProperties", 'yUpperLimit', pMaxY.Value);
        }, 'MapViewportForm/SectionMap.handleGetHeightLimitsClick');
    }
    const handleSetSectionRoutePoints = () => {
        runCodeSafely(() => {
            let sectionMapViewport: MapCore.IMcSectionMapViewport = selectedNodeInTree.nodeMcContent;
            if (sectionMapLocalProperties.isCalculateSectionHeightPoints) {
                let queryParams = new MapCore.IMcSpatialQueries.SQueryParams()
                queryParams.eTerrainPrecision = MapCore.IMcSpatialQueries.EQueryPrecision.EQP_HIGHEST;
                queryParams.pAsyncQueryCallback = new MapCoreData.iMcQueryCallbackClass((aPointsWithHeights: MapCore.SMcVector3D[], afSlopes: Float32Array, pSlopesData: MapCore.IMcSpatialQueries.SSlopesData) => {
                    sectionMapViewport.SetSectionRoutePoints(sectionMapLocalProperties.sectionRoutePointsArr, aPointsWithHeights);
                    props.tabInfo.setPropertiesCallback("sectionMapProperties", 'sectionHeightPointsArr', aPointsWithHeights);
                }, null, 'MapViewportForm/SectionMap.handleSetSectionRoutePoints => IMcSpatialQueries.GetTerrainHeightsAlongLine');
                let slopes: { Value?: Float32Array } = {};
                let slopesData: { Value?: MapCore.IMcSpatialQueries.SSlopesData } = {};
                runMapCoreSafely(() => { sectionMapViewport.GetTerrainHeightsAlongLine(sectionMapLocalProperties.sectionRoutePointsArr, slopes, slopesData, queryParams); }, 'MapViewportForm/SectionMap.handleSetSectionRoutePoints => IMcSpatialQueries.GetTerrainHeightsAlongLine', true)
            } else {
                runMapCoreSafely(() => { sectionMapViewport.SetSectionRoutePoints(sectionMapLocalProperties.sectionRoutePointsArr, sectionMapLocalProperties.sectionHeightPointsArr); }, 'MapViewportForm/SectionMap.handleSetSectionRoutePoints => IMcSectionMapViewport.SetSectionRoutePoints', true)
            }
        }, 'MapViewportForm/SectionMap.handleSetSectionRoutePoints');
    }
    const handleSectionToWorldClick = () => {
        runCodeSafely(() => {
            let sectionVp = selectedNodeInTree.nodeMcContent as MapCore.IMcSectionMapViewport;
            let worldToSectionPoint = sectionMapLocalProperties.worldToSectionPoint;
            runMapCoreSafely(() => {
                worldToSectionPoint = sectionVp.SectionToWorld(sectionMapLocalProperties.sectionToWorldPoint);
            }, 'MapViewportForm/SectionMap.handleSectionToWorldClick => IMcSectionMapViewport.SectionToWorld', true)
            props.tabInfo.setPropertiesCallback("sectionMapProperties", 'worldToSectionPoint', worldToSectionPoint);
        }, 'MapViewportForm/SectionMap.handleSectionToWorldClick');
    }
    const handleWorldToSectionClick = () => {
        runCodeSafely(() => {
            let sectionVp = selectedNodeInTree.nodeMcContent as MapCore.IMcSectionMapViewport;
            let xCoord = sectionMapLocalProperties.sectionToWorldPoint.x;
            runMapCoreSafely(() => {
                xCoord = sectionVp.WorldToSection(sectionMapLocalProperties.worldToSectionPoint);
            }, 'MapViewportForm/SectionMap.handleWorldToSectionClick => IMcSectionMapViewport.WorldToSection', true)
            let finalSectionToWorldPoint = { ...sectionMapLocalProperties.sectionToWorldPoint, x: xCoord };
            props.tabInfo.setPropertiesCallback("sectionMapProperties", 'sectionToWorldPoint', finalSectionToWorldPoint);
        }, 'MapViewportForm/SectionMap.handleWorldToSectionClick');
    }
    //DOM Functions
    const getUpdateSectionParametersFieldset = () => {
        return <Fieldset className="form__row-fieldset" legend='Update Section Parameters'>
            <div style={{ width: `${globalSizeFactor * 30}vh` }}>
                <span>Section Route Points:</span>
                <Vector3DGrid pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_SCREEN} sendPointList={(...args) => saveSectionPointsTable(...args, 'sectionRoutePointsArr')} initLocationPointsList={sectionMapLocalProperties.sectionRoutePointsArr} />
                <div>
                    <Checkbox name="isCalculateSectionHeightPoints" onChange={saveData} inputId="isCalculateSectionHeightPoints" checked={sectionMapLocalProperties.isCalculateSectionHeightPoints} />
                    <label style={{ color: 'grey' }} htmlFor="isCalculateSectionHeightPoints">Is Calculate Section Height Points</label>
                </div>
                <div className="form__flex-and-row-between form__items-center">
                    <DrawLineCtrl activeViewport={activeViewport} initItemResultsCB={initLineResultsCB} />
                    <Button label="Set Section Route Points" onClick={handleSetSectionRoutePoints} />
                </div>

                <div className="form__flex-and-row-between">
                    <label>Axes Ratio: </label>
                    <div className="form__flex-and-row-between">
                        <InputNumber maxFractionDigits={7} name="axesRatio" onChange={saveData} value={sectionMapLocalProperties.axesRatio} />
                        <Button onClick={handleAxesRatioOK}>OK</Button>
                    </div>
                </div>
            </div>
            <div style={{ width: `${globalSizeFactor * 30}vh` }}>
                <span>Section Height Points:</span>
                <Vector3DGrid pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_SCREEN} sendPointList={(...args) => saveSectionPointsTable(...args, 'sectionHeightPointsArr')} initLocationPointsList={sectionMapLocalProperties.sectionHeightPointsArr} />
                <div className="form__flex-and-row-between section-map__polygon-button">
                    <span>Section Polygon Item:</span>
                    <Button label="Polygon" onClick={handlePolygonClick}></Button>
                </div>
            </div>
        </Fieldset>
    }
    const getSectionCoordinateConversionsFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Section Coordinate Conversions'>
            <div className="section-map__section-coord-container">
                <div>
                    <div className="form__flex-and-row-between form__items-center">
                        <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={sectionMapLocalProperties.worldToSectionPoint} saveTheValue={(...args) => saveWorldSectionPoint(...args, 'worldToSectionPoint')} lastPoint={true} />
                        <Button label="World To Section [X]" onClick={handleWorldToSectionClick} />
                    </div>
                    <div className="form__flex-and-row-between form__items-center">
                        <Vector3DFromMap pointCoordSystem={MapCore.EMcPointCoordSystem.EPCS_WORLD} initValue={sectionMapLocalProperties.sectionToWorldPoint} saveTheValue={(...args) => saveWorldSectionPoint(...args, 'sectionToWorldPoint')} lastPoint={true} />
                        <Button label="Section To World" onClick={handleSectionToWorldClick} />
                    </div>
                </div>
                <div>
                    <div className="form__flex-and-row-between">
                        <label htmlFor="xCoordinate">X Coordinate: </label>
                        <InputNumber maxFractionDigits={16} id='xCoordinate' value={sectionMapLocalProperties.xCoordinate} name="xCoordinate" onChange={saveData} />
                    </div>
                    <div className="form__flex-and-row-between form__disabled">
                        <label htmlFor="height">Height: </label>
                        <InputNumber maxFractionDigits={16} id='height' value={sectionMapLocalProperties.height} name="height" />
                    </div>
                    <div className="form__flex-and-row-between">
                        <label className="form__disabled">Slope: </label>
                        <div className="form__flex-and-row-between">
                            <InputNumber maxFractionDigits={16} disabled value={sectionMapLocalProperties.slope} name="slope" />
                            <Button onClick={handleGetSectionHeightClick}>Get Section Height At Point</Button>
                        </div>
                    </div>
                </div>
                <div>
                    <div className="form__flex-and-row-between">
                        <div className="form__flex-and-row-between section-map__x-left-input">
                            <label htmlFor="xLeftLimit">X Left Limit: </label>
                            <InputNumber maxFractionDigits={16} id="xLeftLimit" value={sectionMapLocalProperties.xLeftLimit} name="xLeftLimit" onChange={saveData} />
                        </div>
                        <div className="form__flex-and-row section-map__y-upper-input form__disabled">
                            <label htmlFor="yUpperLimit">Y Upper Limit: </label>
                            <InputNumber maxFractionDigits={16} id='yUpperLimit' value={sectionMapLocalProperties.yUpperLimit} name="yUpperLimit" onChange={saveData} />
                        </div>
                    </div>
                    <div className="form__flex-and-row-between">
                        <div className="form__flex-and-row-between">
                            <label htmlFor="xRightLimit">X Right Limit: </label>
                            <InputNumber maxFractionDigits={16} id='xRightLimit' value={sectionMapLocalProperties.xRightLimit} name="xRightLimit" onChange={saveData} />
                        </div>
                        <div className="form__flex-and-row-between form__disabled">
                            <label htmlFor="yLowerLimit">Y Lower Limit: </label>
                            <InputNumber maxFractionDigits={16} id='yLowerLimit' value={sectionMapLocalProperties.yLowerLimit} name="yLowerLimit" onChange={saveData} />
                        </div>
                        <Button onClick={handleGetHeightLimitsClick}>Get Height Limits</Button>
                    </div>
                </div>
            </div>
        </Fieldset>
    }

    return (
        <span>
            <Fieldset className={`form__column-fieldset ${!enableForm && 'form__disabled-fieldset'}`} legend='Section Map Params'>
                {getUpdateSectionParametersFieldset()}
                {getSectionCoordinateConversionsFieldset()}
            </Fieldset>

            <ConfirmDialog
                contentClassName='form__confirm-dialog-content'
                message={sectionMapLocalProperties.confirmDialogMessage}
                header={sectionMapLocalProperties.confirmDialogHeader}
                footer={<div></div>}
                visible={sectionMapLocalProperties.isConfirmDialogVisible}
                onHide={e => {
                    props.tabInfo.setPropertiesCallback("sectionMapProperties", 'isConfirmDialogVisible', false);
                }}
            />
        </span>
    )
}
