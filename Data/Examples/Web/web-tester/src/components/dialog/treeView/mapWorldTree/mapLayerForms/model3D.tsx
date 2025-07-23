import { MapCoreData, ViewportData } from 'mapcore-lib';
import { Button } from "primereact/button";
import { TabInfo } from "../../../shared/tabCtrls/tabModels";
import { Checkbox } from "primereact/checkbox";
import { ListBox } from "primereact/listbox";
import { useSelector } from "react-redux";
import { useEffect } from 'react';
import { InputNumber } from 'primereact/inputnumber';

import { Properties } from "../../../dialog";
import { runCodeSafely, runMapCoreSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import { TreeNodeModel } from "../../../../../services/tree.service";
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";

export class Model3DPropertiesState implements Properties {
    isResolvingConflicts: boolean;
    resolutionFactor: number;

    static getDefault(p: any): Model3DPropertiesState {
        let { currentLayer, mapWorldTree } = p;

        return {
            isResolvingConflicts: false,
            resolutionFactor: 1,
        }
    }
}
export class Model3DProperties extends Model3DPropertiesState {
    mcCurrent3DModelLayer: MapCore.IMc3DModelMapLayer;
    viewportsArr: TreeNodeModel[];
    selectedViewport: TreeNodeModel;

    static getDefault(p: any): Model3DProperties {
        let { currentLayer, mapWorldTree } = p;
        let stateDefaults = super.getDefault(p);
        let mcCurrent3DModelLayer = currentLayer.nodeMcContent as MapCore.IMc3DModelMapLayer;

        let viewportsArr = MapCoreData.viewportsData.map(vp => mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, vp.viewport))

        return {
            ...stateDefaults,
            mcCurrent3DModelLayer: mcCurrent3DModelLayer,
            viewportsArr: viewportsArr,
            selectedViewport: null,
        }
    }
}

export default function Model3D(props: { tabInfo: TabInfo }) {
    let { tabProperties, setPropertiesCallback, applyCurrStatePropertiesCallback, setCurrStatePropertiesCallback, setApplyCallBack, saveData } = props.tabInfo;
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)

    useEffect(() => {
        runCodeSafely(() => {
            setApplyCallBack(applyAll);
        }, 'MapLayerForm/Model3D.useEffect')
    }, [tabProperties])

    const applyAll = () => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['resolutionFactor', 'isResolvingConflicts'])
        }, 'MapLayerForm/Model3D.applyAll')
    }

    const handleApplyResolvingConflictsClick = (e) => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['isResolvingConflicts'])
            let mcCurrent3DModelLayer = tabProperties.mcCurrent3DModelLayer as MapCore.IMc3DModelMapLayer;
            let mcSelectedViewport = tabProperties.selectedViewport?.nodeMcContent;
            runMapCoreSafely(() => {
                mcSelectedViewport ?
                    mcCurrent3DModelLayer.SetResolvingConflictsWithDtmAndRaster(tabProperties.isResolvingConflicts, mcSelectedViewport) :
                    mcCurrent3DModelLayer.SetResolvingConflictsWithDtmAndRaster(tabProperties.isResolvingConflicts);
            }, 'MapLayerForm/Model3D.handleApplyResolvingConflictsClick => IMc3DModelMapLayer.SetResolvingConflictsWithDtmAndRaster', true)
        }, 'MapLayerForm/Model3D.handleApplyResolvingConflictsClick')
    }
    const handleApplyResolutionFactorClick = (e) => {
        runCodeSafely(() => {
            applyCurrStatePropertiesCallback(['resolutionFactor'])
            let mcCurrent3DModelLayer = tabProperties.mcCurrent3DModelLayer as MapCore.IMc3DModelMapLayer;
            let mcSelectedViewport = tabProperties.selectedViewport?.nodeMcContent;
            runMapCoreSafely(() => {
                mcSelectedViewport ?
                    mcCurrent3DModelLayer.SetResolutionFactor(tabProperties.resolutionFactor, mcSelectedViewport) :
                    mcCurrent3DModelLayer.SetResolutionFactor(tabProperties.resolutionFactor);
            }, 'MapLayerForm/Model3D.handleApplyResolutionFactorClick => IMc3DModelMapLayer.SetResolutionFactor', true)
        }, 'MapLayerForm/Model3D.handleApplyResolutionFactorClick')
    }

    return <div className="form__flex-and-row">
        <div style={{ width: '60%' }} className="form__column-container">
            <div className="form__row-container form__items-center">
                <Button label="Apply" onClick={handleApplyResolvingConflictsClick} />
                <div className="form__flex-and-row form__items-center">
                    <Checkbox name='isResolvingConflicts' inputId="isResolvingConflicts" onChange={saveData} checked={tabProperties.isResolvingConflicts} />
                    <label htmlFor="isResolvingConflicts">Displaying Items Attached To Terrain</label>
                </div>
            </div>
            <div className="form__row-container form__items-center">
                <Button label="Apply" onClick={handleApplyResolutionFactorClick} />
                <div style={{ width: '70%' }} className="form__flex-and-row-between form__items-center">
                    <label htmlFor="resolutionFactor">Resoltion Factor</label>
                    <InputNumber id='resolutionFactor' value={tabProperties.resolutionFactor} name="resolutionFactor" onValueChange={saveData} />
                </div>
            </div>
        </div>
        <ListBox emptyMessage={() => { return <div></div> }} listStyle={{ minHeight: `${globalSizeFactor * 10}vh`, maxHeight: `${globalSizeFactor * 10}vh` }} name='selectedViewport' optionLabel="label" value={tabProperties.selectedViewport} onChange={saveData} options={tabProperties.viewportsArr} />
    </div>
}
