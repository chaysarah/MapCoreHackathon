import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

import './styles/backgroundThreadIndex.css';
import { runCodeSafely } from "../../../../../common/services/error-handling/errorHandler";
import { AppState } from "../../../../../redux/combineReducer";
import mapWorldTreeService from "../../../../../services/mapWorldTreeService";
import { LayerService } from "mapcore-lib";

export default function BackgroundThreadIndex() {
    const globalSizeFactor = useSelector((state: AppState) => state.mapCoreReducer.globalSizeFactor)
    const mapWorldTree = useSelector((state: AppState) => state.mapWorldTreeReducer.mapWorldTree)
    const [layersThreadsArr, setLayersThreadsArr] = useState<{ index: number; layerLabel: string; backgroundThreadIndex: number }[]>([]);

    useEffect(() => {
        runCodeSafely(() => {
            const root = document.documentElement;
            const pixelWidth = window.innerHeight * 0.35 * globalSizeFactor;
            root.style.setProperty('--background-thread-index-dialog-width', `${pixelWidth}px`);

            const layersThreadsArray = LayerService.getAllLayers().map((layer: MapCore.IMcMapLayer, index) => ({
                index: index,
                layerLabel: mapWorldTreeService.convertMapcorObjectToTreeNodeModel(mapWorldTree, layer)?.label,
                backgroundThreadIndex: layer.GetBackgroundThreadIndex() == MapCore.UINT_MAX ? null : layer.GetBackgroundThreadIndex(),
            }));
            setLayersThreadsArr(layersThreadsArray);
        }, 'BackgroundThreadIndex.useEffect')
    }, [])

    return <DataTable style={{ overflowY: 'auto', height: `${globalSizeFactor * 25}vh` }} value={layersThreadsArr} showGridlines>
        <Column header=".No" field="index" />
        <Column header="Layer" field="layerLabel" />
        <Column header="Background Thread Index" field="backgroundThreadIndex" />
    </DataTable>
}