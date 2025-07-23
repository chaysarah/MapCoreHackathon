import { MapCoreData, ViewportData } from 'mapcore-lib';
import { Button } from "primereact/button";
import { TabInfo } from "../../../shared/tabCtrls/tabModels";
import { ListBox } from "primereact/listbox";
import { useSelector } from "react-redux";
import { useEffect, useState } from 'react';

import { Properties } from "../../../dialog";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import { TreeNodeModel } from "../../../../../services/tree.service";
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";
import ThreeStateCheckbox from '../../../../shared/threeStateCheckbox';

export class StaticObjectsPropertiesState implements Properties {
    isDisplayingDtmVisualization: boolean;
    isDisplayingItems: boolean;

    static getDefault(p: any): StaticObjectsPropertiesState {
        let { currentLayer, mapWorldTree } = p;

        return {
            isDisplayingDtmVisualization: null,
            isDisplayingItems: null,
        }
    }
}
export class StaticObjectsProperties extends StaticObjectsPropertiesState {
    mcCurrentStaticObjectsLayer: MapCore.IMcStaticObjectsMapLayer;
    viewportsArr: TreeNodeModel[];
    selectedViewport: TreeNodeModel;

    static getDefault(p: any): StaticObjectsProperties {
        let { currentLayer, mapWorldTree } = p;
        let stateDefaults = super.getDefault(p);
        let mcCurrentStaticObjectsLayer = currentLayer.nodeMcContent as MapCore.IMcStaticObjectsMapLayer;

        let viewportsArr = MapCoreData.viewportsData.map(vp => mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, vp.viewport))

        return {
            ...stateDefaults,
            mcCurrentStaticObjectsLayer: mcCurrentStaticObjectsLayer,
            viewportsArr: viewportsArr,
            selectedViewport: null,
        }
    }
}

export default function StaticObjects(props: { tabInfo: TabInfo }) {
    let { tabProperties, setPropertiesCallback, applyCurrStatePropertiesCallback, setCurrStatePropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    const [value, setValue] = useState('public');
    const options = [
        { value: 'public', icon: 'pi pi-globe' },
        { value: 'protected', icon: 'pi pi-lock-open' },
        { value: 'private', icon: 'pi pi-lock' }
    ];
    useEffect(() => {
        runCodeSafely(() => {
            setApplyCallBack(applyAll);
        }, 'MapLayerForm/StaticObjects.useEffect')
    }, [tabProperties])

    const applyAll = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['isDisplayingItems', 'isDisplayingDtmVisualization'])
        }, 'MapLayerForm/StaticObjects.applyAll')
    }

    const handleApplyDisplayingItemsClick = (e) => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['isDisplayingItems'])
            let mcCurrentStaticObjectsLayer = tabProperties.mcCurrentStaticObjectsLayer as MapCore.IMcStaticObjectsMapLayer;
            let mcSelectedViewport = tabProperties.selectedViewport?.nodeMcContent;
            runMapCoreSafely(() => {
                mcSelectedViewport ?
                    mcCurrentStaticObjectsLayer.SetDisplayingItemsAttachedToTerrain(tabProperties.isDisplayingItems, mcSelectedViewport) :
                    mcCurrentStaticObjectsLayer.SetDisplayingItemsAttachedToTerrain(tabProperties.isDisplayingItems);
            }, 'MapLayerForm/StaticObjects.handleApplyDisplayingItemsClick => IMcStaticObjectsMapLayer.SetDisplayingItemsAttachedToTerrain', true)
        }, 'MapLayerForm/StaticObjects.handleApplyDisplayingItemsClick')
    }
    const handleApplyDisplayingDtmVisualizationClick = (e) => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['isDisplayingDtmVisualization'])
            let mcCurrentStaticObjectsLayer = tabProperties.mcCurrentStaticObjectsLayer as MapCore.IMcStaticObjectsMapLayer;
            let mcSelectedViewport = tabProperties.selectedViewport?.nodeMcContent;
            runMapCoreSafely(() => {
                mcSelectedViewport ?
                    mcCurrentStaticObjectsLayer.SetDisplayingDtmVisualization(tabProperties.isDisplayingDtmVisualization, mcSelectedViewport) :
                    mcCurrentStaticObjectsLayer.SetDisplayingDtmVisualization(tabProperties.isDisplayingDtmVisualization);
            }, 'MapLayerForm/StaticObjects.handleApplyDisplayingDtmVisualizationClick => IMcStaticObjectsMapLayer.SetDisplayingDtmVisualization', true)
        }, 'MapLayerForm/StaticObjects.handleApplyDisplayingDtmVisualizationClick')
    }

    return <div className="form__flex-and-row">
        <div style={{ width: '60%' }} className="form__column-container">
            <div className="form__row-container form__items-center">
                <Button label="Apply" onClick={handleApplyDisplayingItemsClick} />
                <div className="form__flex-and-row form__items-center">
                    <ThreeStateCheckbox id='isDisplayingItems' name='isDisplayingItems' value={tabProperties.isDisplayingItems} onChange={saveData} />
                    <label htmlFor="isDisplayingItems">Displaying Items Attached To Terrain</label>
                </div>
            </div>
            <div className="form__row-container form__items-center">
                <Button label="Apply" onClick={handleApplyDisplayingDtmVisualizationClick} />
                <div className="form__flex-and-row form__items-center">
                    <ThreeStateCheckbox id='isDisplayingDtmVisualization' name='isDisplayingDtmVisualization' value={tabProperties.isDisplayingDtmVisualization} onChange={saveData} />
                    <label htmlFor="isDisplayingDtmVisualization">Set Displaying Dtm Visualization</label>
                </div>
            </div>
        </div>
        <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh` }} name='selectedViewport' optionLabel="label" value={tabProperties.selectedViewport} onChange={saveData} options={tabProperties.viewportsArr} />
    </div>
}
