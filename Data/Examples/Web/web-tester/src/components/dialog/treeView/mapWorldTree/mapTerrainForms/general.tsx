import { InputNumber } from "primereact/inputnumber";
import { Properties } from "../../../dialog";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { Button } from "primereact/button";
import { useEffect } from "react";
import { TabInfo } from "../../../shared/tabCtrls/tabModels";
import { mapWorldNodeType } from "../../../../shared/models/map-tree-node.model";
import { MapCoreData, ViewportData } from 'mapcore-lib';
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";
import { TreeNodeModel } from "../../../../../services/tree.service";
import { ListBox } from "primereact/listbox";
import { useDispatch, useSelector } from "react-redux";
import { AppState } from "../../../../../redux/combineReducer";
import { Checkbox } from "primereact/checkbox";
import { Fieldset } from "primereact/fieldset";
import RectFromMapCtrl, { RectFromMapCtrlInfo } from "../../../shared/RectFromMapCtrl";
import { setSelectedNodeInTree } from "../../../../../redux/MapWorldTree/mapWorldTreeActions";
import UserDataCtrl from "../../objectWorldTree/ctrls/userDataCtrl";
import InputMaxNumber from "../../../../shared/inputMaxNumber";
import GridCoordinateSystemDataset from "../../../shared/ControlsForMapcoreObjects/coordinateSystemCtrl/gridCoordinateSystemDataset";

export class GeneralPropertiesState implements Properties {
    id: number;
    isDefaultVisibility: boolean;
    layerID: number;
    userData: Uint8Array;

    static getDefault(p: any): GeneralPropertiesState {
        let { currentTerrain, mapWorldTree } = p;
        let mcCurrentTerrain = currentTerrain.nodeMcContent as MapCore.IMcMapTerrain;

        let id = 0;
        runMapCoreSafely(() => {
            id = mcCurrentTerrain.GetID();
        }, 'MapTerrainForm/General.getDefault => IMcMapTerrain.GetID', true)
        let visibility = false;
        runMapCoreSafely(() => {
            visibility = mcCurrentTerrain.GetVisibility();
        }, 'MapTerrainForm/General.getDefault => IMcMapTerrain.GetID', true)
        let userData = null, mcUserData = null;
        runMapCoreSafely(() => {
            mcUserData = currentTerrain.nodeMcContent.GetUserData();
        }, 'MapTerrainForm/General.getDefault => IMcMapTerrain.GetID', true)
        userData = mcUserData ? mcUserData.getUserData() : ''
        return {
            isDefaultVisibility: visibility,
            id: id,
            layerID: null,
            userData: userData,
        }
    }
}
export class GeneralProperties extends GeneralPropertiesState {
    viewportsArr: TreeNodeModel[];
    foundedLayerNode: MapCore.IMcMapLayer;
    selectedViewport: MapCore.IMcMapViewport;
    maxPoint: MapCore.SMcVector3D;
    minPoint: MapCore.SMcVector3D;
    isInitialized: boolean;
    showRectangle: boolean;

    static getDefault(p: any): GeneralProperties {
        let stateDefaults = super.getDefault(p);
        let { currentTerrain, mapWorldTree } = p;
        let mcCurrentTerrain = currentTerrain.nodeMcContent as MapCore.IMcMapTerrain;

        let viewportsArr = MapCoreData.viewportsData.map(vp => mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, vp.viewport))
        let mcBox = new MapCore.SMcBox();
        let isInitialized = true;
        try {
            mcBox = mcCurrentTerrain.GetBoundingBox();
        }
        catch (error) {
            if (error instanceof MapCore.CMcError && (error.name as any).value == MapCore.IMcErrors.ECode.NOT_INITIALIZED) {
                isInitialized = false;
            }
            else {
                runMapCoreSafely(() => {
                    mcBox = mcCurrentTerrain.GetBoundingBox();
                }, 'MapTerrainForm/General.getDefault => IMcMapTerrain.GetBoundingBox', true)
            }
        }


        let defaults: GeneralProperties = {
            ...stateDefaults,
            viewportsArr: viewportsArr,
            foundedLayerNode: null,
            selectedViewport: null,
            minPoint: mcBox.MinVertex,
            maxPoint: mcBox.MaxVertex,
            isInitialized: isInitialized,
            showRectangle: false,
        }
        return defaults;
    }
};

export default function General(props: { tabInfo: TabInfo }) {
    const dispatch = useDispatch();
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    let selectedNodeInTree = useSelector((state: AppState) => state.mapWorldTreeReducer.selectedNodeInTree);
    let mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree);
    const activeViewport: ViewportData = useSelector((state: AppState) => MapCoreData.findViewport(state.mapWindowReducer.activeCard));

    //#region UseEffect
    useEffect(() => {
        runCodeSafely(() => {
            props.tabInfo.setApplyCallBack(applyAll)
        }, 'MapTerrainForm/General.useEffect')
    }, [props.tabInfo.tabProperties])
    //#endregion
    //#region  Help Functions
    const getTerrainCoordinateSystem = () => {
        let gridCoordinateSystem: MapCore.IMcGridCoordinateSystem = null;
        runCodeSafely(() => {
            let mcCurrentTerrrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            runMapCoreSafely(() => {
                gridCoordinateSystem = mcCurrentTerrrain.GetCoordinateSystem()
            }, 'MapTerrainForm/General.getTerrainCoordinateSystem => IMcMapTerrain.GetCoordinateSystem', true)
        }, 'MapTerrainForm/General.getTerrainCoordinateSystem')
        return gridCoordinateSystem;
    }
    //#endregion
    //#region Handle Functions
    const applyAll = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['userData', 'id'])
            let mcCurrentTerrrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            //SET ID
            runMapCoreSafely(() => { mcCurrentTerrrain.SetID(props.tabInfo.tabProperties.id); }, 'MapTerrainForm/General.applyAll => IMcMapTerrain.SetID', true)
            // Set userData
            let testerUserData = new MapCoreData.iMcUserDataClass();
            testerUserData.setUserData(props.tabInfo.tabProperties.userData);
            runMapCoreSafely(() => { mcCurrentTerrrain.SetUserData(testerUserData); }, 'MapTerrainForm/General.applyAll => IMcMapTerrain.SetUserData', true)
        }, 'MapTerrainForm/General.applyAll')
    }
    const handleGetLayerClick = () => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['layerID'])
            let mcCurrentTerrrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let mcFoundedLayer = null;
            runMapCoreSafely(() => {
                mcFoundedLayer = mcCurrentTerrrain.GetLayerByID(props.tabInfo.tabProperties.layerID);
            }, 'MapTerrainForm/General.handleGetLayerClick => IMcMapTerrain.GetLayerByID', true)
            if (mcFoundedLayer) {
                let foundedLayerNode = mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, mcFoundedLayer);
                props.tabInfo.setPropertiesCallback('foundedLayerNode', foundedLayerNode)
            }
        }, 'MapTerrainForm/General.handleGetLayerClick')
    }
    const handleLinkClick = () => {
        runCodeSafely(() => {
            dispatch(setSelectedNodeInTree(props.tabInfo.tabProperties.foundedLayerNode))
        }, 'MapTerrainForm/General.handleLinkClick')
    }
    const getUpdatedMaxInput = (value: number) => {
        runCodeSafely(() => {
            props.tabInfo.setPropertiesCallback('id', value)
            props.tabInfo.setCurrStatePropertiesCallback('id', value)
        }, 'MapTerrainForm/General.getUpdatedMaxInput')
    }
    const handleApplyVisibilityClick = (e) => {
        runCodeSafely(() => {
            props.tabInfo.applyCurrStatePropertiesCallback(['isDefaultVisibility'])
            let mcCurrentTerrrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let mcSelectedViewport = props.tabInfo.tabProperties.selectedViewport?.nodeMcContent;
            runMapCoreSafely(() => {
                mcSelectedViewport ?
                    mcCurrentTerrrain.SetVisibility(props.tabInfo.tabProperties.isDefaultVisibility, mcSelectedViewport) :
                    mcCurrentTerrrain.SetVisibility(props.tabInfo.tabProperties.isDefaultVisibility);
            }, 'MapTerrainForm/General.handleApplyVisibilityClick => IMcMapTerrain.SetVisibility', true)
        }, 'MapTerrainForm/General.handleApplyVisibilityClick')
    }
    const handleViewportSelected = (e) => {
        runCodeSafely(() => {
            props.tabInfo.saveData(e);
            let mcCurrentTerrrain = selectedNodeInTree.nodeMcContent as MapCore.IMcMapTerrain;
            let terrainVisibility = false;
            runMapCoreSafely(() => {
                terrainVisibility = e.value ? mcCurrentTerrrain.GetVisibility(e.value.nodeMcContent) : mcCurrentTerrrain.GetVisibility();
            }, 'MapTerrainForm/General.handleViewportSelected => IMcMapTerrain.GetVisibility', true)
            props.tabInfo.setPropertiesCallback('isDefaultVisibility', terrainVisibility)
        }, 'MapTerrainForm/General.handleViewportSelected')
    }
    //#endregion
    //#region  DOM Functions
    const getLayerByIDFieldset = () => {
        return <Fieldset legend='Get Layer By ID' className="form__column-fieldset">
            <div className="form__flex-and-row-between form__items-center">
                <div style={{ width: '40%' }} className="form__flex-and-row-between">
                    <label htmlFor="layerID">ID: </label>
                    <InputNumber id='layerID' value={props.tabInfo.tabProperties.layerID} name="layerID" onValueChange={props.tabInfo.saveData} />
                </div>
                <Button disabled={!props.tabInfo.tabProperties.layerID} label='Get Layer' onClick={handleGetLayerClick} />
            </div>
            <label onClick={handleLinkClick} style={{ color: "blue" }}>
                {props.tabInfo.tabProperties.foundedLayerNode?.label}
            </label>
        </Fieldset>
    }
    const getCoordinateSystem = () => {
        return <GridCoordinateSystemDataset gridCoordinateSystem={getTerrainCoordinateSystem()} />
    }
    const getWorldBoundingBox = () => {
        let currentViewport = mapWorldTreeService.getParentByChildKey(mapWorldTree, selectedNodeInTree.key);
        let mcCurrentViewport = currentViewport.nodeType == mapWorldNodeType.MAP_VIEWPORT ? currentViewport.nodeMcContent : activeViewport?.viewport
        let rectBox = new MapCore.SMcBox(props.tabInfo.tabProperties.minPoint, props.tabInfo.tabProperties.maxPoint)
        const rectInfo: RectFromMapCtrlInfo = {
            rectangleBox: rectBox,
            showRectangle: props.tabInfo.tabProperties.showRectangle,
            setProperty: (name: string, value: any) => { props.tabInfo.setPropertiesCallback(name, value) },
            readOnly: true,
            currViewport: mcCurrentViewport,
            rectCoordSystem: MapCore.EMcPointCoordSystem.EPCS_WORLD,
        }
        return <Fieldset legend={props.tabInfo.tabProperties.isInitialized ? `World Bounding Box` : 'World Bounding Box (Not Initialized)'}>
            <RectFromMapCtrl info={rectInfo} />
        </Fieldset>
    }
    const getUserData = () => {
        const getUserDataBuffer = (userDataBuffer: Uint8Array) => {
            props.tabInfo.setPropertiesCallback('userData', userDataBuffer);
            props.tabInfo.setCurrStatePropertiesCallback('userData', userDataBuffer)
        }

        return <UserDataCtrl ctrlHeight={13} userData={props.tabInfo.tabProperties.userData} getUserDataBuffer={getUserDataBuffer} />
    }
    //#endregion

    return <div className="form__column-container">
        <div style={{ width: '30%' }} className="form__flex-and-row-between form__items-center">
            <label htmlFor="id">Terrain ID:</label>
            <InputMaxNumber value={props.tabInfo.tabProperties.id} maxValue={MapCore.UINT_MAX} getUpdatedMaxInput={getUpdatedMaxInput} id='id' name='id' />
        </div>
        <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh` }} name='selectedViewport' optionLabel="label" value={props.tabInfo.tabProperties.selectedViewport} onChange={handleViewportSelected} options={props.tabInfo.tabProperties.viewportsArr} />
        <div className="form__flex-and-row-between form__items-center">
            <div className="form__flex-and-row form__items-center">
                <Checkbox name='isDefaultVisibility' inputId="isDefaultVisibility" onChange={props.tabInfo.saveData} checked={props.tabInfo.tabProperties.isDefaultVisibility} />
                <label htmlFor="isDefaultVisibility">Default Visibility</label>
            </div>
            <Button label="Apply" onClick={handleApplyVisibilityClick} />
        </div>
        <br />
        {getLayerByIDFieldset()}
        <div className="form__flex-and-row">
            <div style={{ width: '60%' }}>{getCoordinateSystem()}</div>
            <div style={{ width: '40%' }}>{getUserData()}</div>
        </div>
        {getWorldBoundingBox()}
    </div>
}


