import { Checkbox } from "primereact/checkbox";
import { InputText } from "primereact/inputtext";
import { useDispatch, useSelector } from "react-redux";
import { Fieldset } from "primereact/fieldset";
import { useEffect } from "react";
import { Button } from "primereact/button";
import { InputNumber } from "primereact/inputnumber";
import { ListBox } from "primereact/listbox";

import { MapCoreData, TypeToStringService, ViewportData } from 'mapcore-lib';
import UserDataCtrl from "../../objectWorldTree/ctrls/userDataCtrl";
import RectFromMapCtrl, { RectFromMapCtrlInfo } from "../../../shared/RectFromMapCtrl";
import { Properties } from "../../../dialog";
import { TabInfo } from "../../../shared/tabCtrls/tabModels";
import GridCoordinateSystemDataset from "../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/gridCoordinateSystemDataset";
import { mapWorldNodeType } from "../../../../shared/models/map-tree-node.model";
import InputMaxNumber from "../../../../shared/inputMaxNumber";
import ThreeStateCheckbox from "../../../../shared/threeStateCheckbox";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { setSelectedNodeInTree } from "../../../../../redux/MapWorldTree/mapWorldTreeActions";
import generalService from "../../../../../services/general.service";
import { TreeNodeModel } from "../../../../../services/tree.service";
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";
import { AppState } from "../../../../../redux/combineReducer";

export class MapLayerPropertiesState implements Properties {
    id: number;
    userData: Uint8Array;
    numTiles: number;
    isDefaultVisibility: boolean;
    localCacheSubFolder: string;
    backgroundThreadIndex: number;

    static getDefault(p: any): MapLayerPropertiesState {
        let { currentLayer, mapWorldTree } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcMapLayer;
        let id: number;
        runMapCoreSafely(() => {
            id = mcCurrentLayer.GetID();
        }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.GetID', true);

        let userData = null, mcUserData = null;
        runMapCoreSafely(() => {
            mcUserData = mcCurrentLayer.GetUserData();
        }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.GetUserData', true)
        userData = mcUserData ? mcUserData.getUserData() : new Uint8Array;

        let isDefaultVisibility;
        runMapCoreSafely(() => {
            isDefaultVisibility = mcCurrentLayer.GetVisibility();
        }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.GetVisibility', true);

        let numTiles: number;
        runMapCoreSafely(() => {
            numTiles = mcCurrentLayer.GetNumTilesInNativeServerRequest();
        }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.GetNumTilesInNativeServerRequest', true);

        let backgroundThreadIndex: number;
        runMapCoreSafely(() => {
            backgroundThreadIndex = mcCurrentLayer.GetBackgroundThreadIndex();
        }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.GetBackgroundThreadIndex', true);
        backgroundThreadIndex = backgroundThreadIndex != MapCore.MC_EMPTY_ID ? backgroundThreadIndex : 0;
        return {
            id: id,
            userData: userData,
            numTiles: numTiles,
            isDefaultVisibility: isDefaultVisibility,
            localCacheSubFolder: '',//ask if needed in web
            backgroundThreadIndex: backgroundThreadIndex,
        }
    }
}
export class MapLayerProperties extends MapLayerPropertiesState {
    mcCurrentLayer: MapCore.IMcMapLayer;
    isSpatialQueriesForm: boolean;
    layerType: string;
    isInitialized: boolean;
    minPoint: MapCore.SMcVector3D;
    maxPoint: MapCore.SMcVector3D;
    isLayerInitialized: boolean;
    terrainsArr: TreeNodeModel[];
    viewportsArr: TreeNodeModel[];
    selectedViewport: TreeNodeModel;
    selectedTerrain: TreeNodeModel;
    isEffectiveVisibilityChecked: boolean;
    showRectangle: boolean;

    static getDefault(p: any): MapLayerProperties {
        let stateDefaults = super.getDefault(p);
        let { currentLayer, mapWorldTree, isSpatialQueriesForm } = p;
        let mcCurrentLayer = currentLayer.nodeMcContent as MapCore.IMcMapLayer;

        let isInitialized = true;
        runMapCoreSafely(() => {
            isInitialized = mcCurrentLayer.IsInitialized();
        }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.IsInitialized', true);

        let layerType: number = null;
        runMapCoreSafely(() => {
            layerType = mcCurrentLayer.GetLayerType();
        }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.GetLayerType', true);
        let layerTypeStr = TypeToStringService.getLayerTypeByTypeNumber(layerType);

        let mcBox: MapCore.SMcBox;
        let isLayerInitialized = true;
        try {
            mcBox = mcCurrentLayer.GetBoundingBox();
        }
        catch (error) {
            if (error instanceof MapCore.CMcError && (error.name as any).value == MapCore.IMcErrors.ECode.NOT_INITIALIZED) {
                isLayerInitialized = false;
            }
            else {
                runMapCoreSafely(() => {
                    mcBox = mcCurrentLayer.GetBoundingBox();
                }, 'MapLayerForm/MapLayer.getDefault => IMcMapLayer.GetBoundingBox', true)
            }
        }
        let viewportsArr = MapCoreData.viewportsData.map(vp => mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, vp.viewport))

        return {
            ...stateDefaults,
            mcCurrentLayer: mcCurrentLayer,
            isSpatialQueriesForm: isSpatialQueriesForm ? true : false,
            layerType: layerTypeStr,
            isInitialized: isInitialized,
            minPoint: mcBox.MinVertex,
            maxPoint: mcBox.MaxVertex,
            isLayerInitialized: isLayerInitialized,
            terrainsArr: [],
            viewportsArr: viewportsArr,
            selectedViewport: null,
            selectedTerrain: null,
            isEffectiveVisibilityChecked: null,
            showRectangle: false,
        }
    }
}

export default function MapLayer(props: { tabInfo: TabInfo }) {
    let { tabProperties, setPropertiesCallback, applyCurrStatePropertiesCallback, setCurrStatePropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree)
    const mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree)
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));

    useEffect(() => {
        setApplyCallBack(applyAll);
    }, [tabProperties])
    const getUpdatedMaxInput = (value: number) => {
        runCodeSafely(() => {
            setPropertiesCallback('id', value)
            setCurrStatePropertiesCallback('id', value)
        }, 'MapLayerForm/MapLayer.getUpdatedMaxInput')
    }
    const getLayerCoordinateSystem = () => {
        let mcLayerCS: MapCore.IMcGridCoordinateSystem;
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                mcLayerCS = tabProperties.mcCurrentLayer.GetCoordinateSystem();
            }, 'MapLayerForm/MapLayer.getLayerCoordinateSystem => IMcMapLayer.GetCoordinateSystem', true)
        }, 'MapLayerForm/MapLayer.getLayerCoordinateSystem')
        return mcLayerCS;
    }

    //#region Handle Functions
    const applyAll = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['id', 'userData']);
            runMapCoreSafely(() => { tabProperties.mcCurrentLayer.SetID(tabProperties.id); }, 'MapLayerForm/MapLayer.applyAll => IMcMapLayer.SetID', true)
            // Set userData
            let testerUserData = new MapCoreData.iMcUserDataClass();
            testerUserData.setUserData(tabProperties.userData);
            runMapCoreSafely(() => { tabProperties.mcCurrentLayer.SetUserData(testerUserData); }, 'MapLayerForm/MapLayer.applyAll => IMcMapLayer.SetUserData', true)
            applyCurrStatePropertiesCallback(['numTiles']);
            applyCurrStatePropertiesCallback(['isDefaultVisibility'])
        }, 'MapLayerForm/MapLayer.applyAll')
    }
    const handleSetNumTilesClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['numTiles']);
            runMapCoreSafely(() => {
                tabProperties.mcCurrentLayer.SetNumTilesInNativeServerRequest(tabProperties.numTiles);
            }, 'MapLayerForm/MapLayer.handleSetNumTilesClick => IMcMapLayer.SetNumTilesInNativeServerRequest', true)
        }, 'MapLayerForm/MapLayer.handleSetNumTilesClick')
    }
    const handleViewportSelected = (e) => {
        runCodeSafely(() => {
            saveData(e);
            let layerVisibility = false;
            runMapCoreSafely(() => {
                layerVisibility = e.value ? tabProperties.mcCurrentLayer.GetVisibility(e.value.nodeMcContent) : tabProperties.mcCurrentLayer.GetVisibility();
            }, 'MapLayerForm/MapLayer.handleViewportSelected => IMcMapLayer.GetVisibility', true)
            setPropertiesCallback('isDefaultVisibility', layerVisibility)
            //getTerrains
            let mcTerrains: MapCore.IMcMapTerrain[] = [];
            runMapCoreSafely(() => {
                mcTerrains = e.value ? e.value.nodeMcContent.GetTerrains() : [];
            }, 'MapLayerForm/MapLayer.handleViewportSelected => IMcMapViewport.GetTerrains', true)
            let terrainsArr = mcTerrains.map(terrain => {
                let x = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, terrain)
                return x;
            })
            setPropertiesCallback('terrainsArr', terrainsArr)
        }, 'MapLayerForm/MapLayer.handleViewportSelected')
    }
    const handleTerrainSelected = (e) => {
        runCodeSafely(() => {
            saveData(e);
            let effectiveVisibility = null;
            runMapCoreSafely(() => {
                effectiveVisibility = e.value ? tabProperties.mcCurrentLayer.GetEffectiveVisibility(tabProperties.selectedViewport.nodeMcContent, e.value.nodeMcContent) : null;
            }, 'MapLayerForm/MapLayer.handleTerrainSelected => IMcMapLayer.GetEffectiveVisibility', true)
            setPropertiesCallback('isEffectiveVisibilityChecked', effectiveVisibility)
        }, 'MapLayerForm/MapLayer.handleTerrainSelected')
    }
    const handleApplyVisibilityClick = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['isDefaultVisibility'])
            let mcSelectedViewport = tabProperties.selectedViewport?.nodeMcContent;
            runMapCoreSafely(() => {
                if (mcSelectedViewport) {
                    tabProperties.mcCurrentLayer.SetVisibility(tabProperties.isDefaultVisibility, mcSelectedViewport)
                } else {
                    tabProperties.mcCurrentLayer.SetVisibility(tabProperties.isDefaultVisibility);
                }
            }, 'MapLayerForm/MapLayer.handleApplyVisibilityClick => IMcMapLayer.SetVisibility', true)
        }, 'MapLayerForm/MapLayer.handleApplyVisibilityClick')
    }
    const handleReplaceNativeServerLayerClick = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                tabProperties.mcCurrentLayer.ReplaceNativeServerLayerAsync(generalService.layerPropertiesBase.layerIReadCallback);
            }, 'MapLayerForm/MapLayer.handleReplaceNativeServerLayerClick => IMcMapLayer.ReplaceNativeServerLayerAsync', true)
            dispatch(setSelectedNodeInTree(null))
        }, 'MapLayerForm/MapLayer.handleReplaceNativeServerLayerClick')
    }
    const handleRemoveLayerAsyncClick = () => {
        runCodeSafely(() => {
            runMapCoreSafely(() => {
                tabProperties.mcCurrentLayer.RemoveLayerAsync();
            }, 'MapLayerForm/MapLayer.handleRemoveLayerAsyncClick => IMcMapLayer.RemoveLayerAsync', true)
            dispatch(setSelectedNodeInTree(null))
        }, 'MapLayerForm/MapLayer.handleRemoveLayerAsyncClick')
    }
    //#endregion
    //#region DOM Function
    const getBoundingBoxRectFieldset = () => {
        let currentViewport = selectedNodeInTree?.key ? mapWorldTreeService.getParentByChildKey(mapWorldTree, selectedNodeInTree.key.slice(0, -2)) : null;
        let mcCurrentViewport = currentViewport?.nodeType == mapWorldNodeType.MAP_VIEWPORT ? currentViewport.nodeMcContent : activeViewport?.viewport;
        let rectBox = new MapCore.SMcBox(tabProperties.minPoint, tabProperties.maxPoint)
        const rectInfo: RectFromMapCtrlInfo = {
            rectangleBox: rectBox,
            showRectangle: tabProperties.showRectangle,
            setProperty: (name: string, value: any) => { setPropertiesCallback(name, value) },
            readOnly: true,
            currViewport: mcCurrentViewport,
            rectCoordSystem: MapCore.EMcPointCoordSystem.EPCS_WORLD,
        }
        return <Fieldset style={{ width: '70%' }} legend={tabProperties.isLayerInitialized ? `World Bounding Box` : 'World Bounding Box (Not Initialized)'}>
            <RectFromMapCtrl info={rectInfo} />
        </Fieldset>
    }
    const getCoordinateSystemFieldset = () => {
        return <GridCoordinateSystemDataset gridCoordinateSystem={getLayerCoordinateSystem()} useLocalDialog={true} />
    }
    const getUserData = () => {
        const getUserDataBuffer = (userDataBuffer: Uint8Array) => {
            setPropertiesCallback('userData', userDataBuffer);
            setCurrStatePropertiesCallback('userData', userDataBuffer)
        }

        return <UserDataCtrl ctrlHeight={13} userData={tabProperties.userData} getUserDataBuffer={getUserDataBuffer} />
    }
    const getNumTilesFieldset = () => {
        return <Fieldset style={{ width: '30%' }} legend='Num Tiles In Native Server Request' className="form__column-fieldset">
            <div className="form__flex-and-row-between">
                <label htmlFor="numTiles">Num Tiles: </label>
                <InputNumber className="form__medium-width-input" id='numTiles' value={tabProperties.numTiles} name="numTiles" onValueChange={saveData} />
            </div>
            <div className="form__flex-and-row form__justify-end">
                <Button label='Set' onClick={handleSetNumTilesClick} />
            </div>
        </Fieldset>
    }
    const getLayerVisibilityFieldset = () => {
        return <Fieldset className="form__column-fieldset" legend='Layer Visibility'>
            <div className="form__flex-and-row form__items-center">
                <Checkbox name='isDefaultVisibility' inputId="isDefaultVisibility" onChange={saveData} checked={tabProperties.isDefaultVisibility} />
                <label htmlFor="isDefaultVisibility">Default Visibility</label>
            </div>
            <div className="form__flex-and-row-between">
                <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh`, width: `${globalSizeFactor * 30}vh` }} name='selectedViewport' optionLabel="label" value={tabProperties.selectedViewport} onChange={handleViewportSelected} options={tabProperties.viewportsArr} />
                <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh`, width: `${globalSizeFactor * 30}vh` }} name='selectedTerrain' optionLabel="label" value={tabProperties.selectedTerrain} onChange={handleTerrainSelected} options={tabProperties.terrainsArr} />
            </div>
            <div className="form__flex-and-row-between form__items-center">
                <span className="form__flex-and-row form__items-center">
                    <ThreeStateCheckbox value={tabProperties.isEffectiveVisibilityChecked} disabled={true} />
                    <label>Effective visibility in viewport and terrain</label>
                </span>
                <Button label="Apply" onClick={handleApplyVisibilityClick} />
            </div>
        </Fieldset>
    }
    //#endregion
    return <div className="form__column-container">
        <div style={{ width: '50%' }} className="form__flex-and-row-between form__items-center">
            <div style={{ width: '62%' }} className="form__flex-and-row-between form__items-center">
                <label htmlFor="id">Layer ID:</label>
                <InputMaxNumber style={{ width: `${globalSizeFactor * 10}vh` }} value={tabProperties.id} maxValue={MapCore.UINT_MAX} getUpdatedMaxInput={getUpdatedMaxInput} id='id' name='id' />
            </div>
            <div style={{ width: '30%' }} className="form__flex-and-row form__items-center form__disabled">
                <Checkbox checked={tabProperties.isInitialized} />
                <label style={{ paddingLeft: `${globalSizeFactor * 0.2}vh` }}>Is Initialized</label>
            </div>
        </div>
        <div style={{ width: '50%' }} className='form__flex-and-row-between form__items-center form__disabled'>
            <label htmlFor="layerType">Layer Type:</label>
            <InputText style={{ width: `${globalSizeFactor * 23}vh` }} id='layerType' value={tabProperties.layerType} />
        </div>
        <div className="form__flex-and-row">
            <div style={{ width: '60%' }}>{getCoordinateSystemFieldset()}</div>
            <div style={{ width: '40%' }}>{getUserData()}</div>
        </div>
        {!tabProperties.isSpatialQueriesForm && getLayerVisibilityFieldset()}
        <div className="form__flex-and-row-between">
            {getBoundingBoxRectFieldset()}
            {getNumTilesFieldset()}
        </div>
        <div style={{ width: '57.5%', padding: `${globalSizeFactor * 1}vh` }} className="form__flex-and-row-between form__disabled">
            <label htmlFor="backgroundThreadIndex">Background Thread Index: </label>
            <InputNumber id='backgroundThreadIndex' value={tabProperties.backgroundThreadIndex} name="backgroundThreadIndex" onValueChange={saveData} />
        </div>
        {!tabProperties.isSpatialQueriesForm && <div className="form__flex-and-row form__justify-space-around">
            <Button style={{ width: '40%' }} label='Replace Native Server Layer Async' onClick={handleReplaceNativeServerLayerClick} />
            <Button style={{ width: '40%' }} label='Remove Layer Async' onClick={handleRemoveLayerAsyncClick} />
            {/* <Button label='Get Create Layer Params' /> */}
        </div>}
    </div>
}